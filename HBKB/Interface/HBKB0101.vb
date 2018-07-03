Imports FarPoint.Win.Spread.Model
Imports System.Text
Imports Microsoft.Office.Interop
Imports CommonHBK
Imports Npgsql
Imports Common

''' <summary>
''' 共通検索一覧画面Interfaceクラス
''' </summary>
''' <remarks>共通検索一覧画面の設定を行う
''' <para>作成情報：2012/05/31 kuga
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKB0101

    'インスタンス作成
    Public dataHBKB0101 As New DataHBKB0101         'データクラス
    Private logicHBKB0101 As New LogicHBKB0101      'ロジッククラス
    Private commonLogic As New Common.CommonLogic   '共通ロジッククラス
    Private commonLogicHBK As New CommonLogicHBK    'HBK共通ロジッククラス

    Private blnGroupChangeFlg As Boolean = False

    Private blnRaiseEventCIKbn As Boolean = False     'CI種別リストボックス変更時イベント実行可否フラグ


    ''' <summary>
    ''' Spreadシート行全削除
    ''' </summary>
    ''' <remarks>cc
    ''' <para>作成情報：2012/06/08 kuga
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub SheetAllClearOther()

        If Me.vwOther.Sheets(0).RowCount > 0 Then
            Me.vwOther.Sheets(0).RowCount = 0
        End If

    End Sub


    ''' <summary>
    ''' フォームアクティブ時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>フォームがアクティブになった際に行われる処理</remarks>
    Private Sub HBKB0101_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If blnGroupChangeFlg Then
            gceGroup.SetGroupCD()
        End If

        blnGroupChangeFlg = False
    End Sub

    ''' <summary>
    ''' フォーム読み込み時処理
    ''' </summary>
    ''' 
    ''' <remarks>フォームを読み込んだ際に行われる処理
    ''' <para>作成情報：2012/05/31 kuga
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Sub HBKB0101_load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            '背景色変更
            MyBase.BackColor = commonLogicHBK.SetFormBackColor(CommonHBK.CommonDeclareHBK.PropConfigrationFlag)

            'データクラスにプロパティセット
            With dataHBKB0101
                'フォームオブジェクト
                .PropCmbGroupCD = Me.gceGroup.cmbGroup              'グループ名コンボボックス
                .PropLstCiClassCD = Me.lstCiClass                   'CI種別リストボックス
                .PropCmbClassCD = Me.cmbClass                       '種別コンボボックス
                .PropTxtNumberCD = Me.txtNumber                     '番号テキストボックス
                .PropCmbStatusCD = Me.cmbStatus                     'ステータスコンボボックス
                .PropCmbCiOwnerCD = Me.cmbCiOwner                   'CIオーナーコンボボックス
                .PropTxtCategory1CD = Me.txtCategory1               '分類１テキストボックス
                .PropTxtCategory2CD = Me.txtCategory2               '分類２テキストボックス
                .PropTxtNameCD = Me.txtName                         '名称テキストボックス
                .PropTxtFreeWordCD = Me.txtFreeWord                 'フリーワードテキストボックス
                .PropDtpStartDT = Me.dtpStart                       '最終更新日(FROM)DTPボックス
                .PropDtpEndDT = Me.dtpEnd                           '最終更新日(TO)DTPボックス
                .PropTxtFreeTextCD = Me.txtFreeText                 'フリーテキストテキストボックス
                .PropCmbFreeFlag1CD = Me.cmbFlag1                   'フリーフラグ1コンボボックス
                .PropCmbFreeFlag2CD = Me.cmbFlag2                   'フリーフラグ2コンボボックス
                .PropCmbFreeFlag3CD = Me.cmbFlag3                   'フリーフラグ3コンボボックス
                .PropCmbFreeFlag4CD = Me.cmbFlag4                   'フリーフラグ4コンボボックス
                .PropCmbFreeFlag5CD = Me.cmbFlag5                   'フリーフラグ5コンボボックス
                .PropTxtDocCD = Me.txtDoc                           '文書配付先テキストボックス
                .PropVwDoc = Me.vwDoc                               '文書一覧スプレッド
                .PropVwOther = Me.vwOther                           'その他一覧スプレッド
                .PropLblCount = Me.lblCount                         '件数ラベル
                .PropBtnNewReg = Me.btnNewReg                       '新規登録ボタン
                .PropBtnUpPack = Me.btnUpPack                       '一括登録ボタン
                .PropBtnOutput = Me.btnOutput                       'EXCEL出力ボタン
            End With

            'フォーム情報の初期化
            If logicHBKB0101.InitFormMain(dataHBKB0101) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If

            '共通コントロール初期化
            Me.lblCount.Text = "0 件"    '件数

            'CI種別コンボボックス変更時イベント実行可否フラグON
            blnRaiseEventCIKbn = True

        Catch ex As Exception
            Common.CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'エラーメッセージ表示
            MsgBox(HBK_E001 & ex.Message, MsgBoxStyle.Critical, TITLE_ERROR)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    ''' <summary>
    ''' リスト項目変更処理
    ''' </summary>
    ''' 
    ''' <remarks>CI種別リストの項目を変更した際に行われる処理
    ''' <para>作成情報：2012/05/31 kuga
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub lstCiClass_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstCiClass.SelectedIndexChanged

        'イベント実行フラグがONの場合のみ処理
        If blnRaiseEventCIKbn = True Then

            'フォーム情報の初期化
            If logicHBKB0101.InitFormList(dataHBKB0101) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Exit Sub
            End If

        End If

    End Sub

    ''' <summary>
    ''' クリアボタン押下時処理
    ''' </summary>
    ''' 
    ''' <remarks>クリアボタンを押下した際に行われる処理
    ''' <para>作成情報：2012/05/31 kuga
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click

        'クリアボタン処理
        If logicHBKB0101.ClearAll(dataHBKB0101) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Return
        End If

    End Sub

    ''' <summary>
    ''' 検索ボタン押下時処理
    ''' </summary>
    ''' 
    ''' <remarks>検索ボタンを押下した際に行われる処理
    ''' <para>作成情報：2012/05/31 kuga
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Try
            Dim com As New Common.CommonValidation

            ' 検索件数一覧
            If logicHBKB0101.GetCountMain(dataHBKB0101) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If

            '検索結果が閾値を超えているか
            If dataHBKB0101.PropIntResultCnt > PropSearchMsgCount Then
                If MsgBox(String.Format(B0101_W001, PropSearchMsgCount.ToString()), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then

                    '******************************************
                    'データの初期化及びボタンを非活性
                    '******************************************
                    If logicHBKB0101.IndicateNotResult(dataHBKB0101) = False Then
                        'エラーメッセージ表示
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Return
                    End If

                    dataHBKB0101.PropLblCount.Text = "0件"

                    Return
                End If
            ElseIf dataHBKB0101.PropIntResultCnt = 0 Then
                '0件の場合、処理を中断
                Return
            End If

            'データ検索処理
            If logicHBKB0101.SearchListMain(dataHBKB0101) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If

            '表示中のCI種別が文書の場合
            If Me.lstCiClass.SelectedValue = CI_TYPE_DOC Then

                'フォーカスが当たるセル
                Me.vwDoc.ActiveSheet.SetActiveCell(0, 0)


            Else    'CI種別がシステム・サポセン機器・部所有機器の場合

                Dim SearchStr As String = Trim(dataHBKB0101.PropCmbClassCD.Text)

                '手入力した値が項目にあるか確認(サポセン機器)
                If SearchStr <> "" Then
                    Me.cmbClass.SelectedIndex = Me.cmbClass.FindStringExact(SearchStr)
                    '手入力した値が存在しない場合、エラーを出すようにする
                    If (Me.cmbClass.SelectedIndex = -1) Then
                        Me.SheetAllClearOther()
                    End If
                End If

            End If

        Catch ex As Exception
            Common.CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'エラーメッセージ表示
            MsgBox(HBK_E001 & ex.Message, MsgBoxStyle.Critical, TITLE_ERROR)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    ''' <summary>
    ''' 開くボタンクリック時処理
    ''' </summary>
    ''' <remarks>Fpspread1結果セルの「開く」ボタンをクリックした際に行われる処理
    ''' <para>作成情報：2012/05/31 kuga
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwDoc_ButtonClicked(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.EditorNotifyEventArgs) Handles vwDoc.ButtonClicked

        'マウスポインタ変更
        Me.Cursor = Cursors.WaitCursor

        '選択行をデータクラスにセット
        dataHBKB0101.PropIntSelectedRow = e.Row

        '選択されたファイルを開く
        If logicHBKB0101.OpenFileMain(dataHBKB0101) = False Then
            'マウスポインタ変更
            Me.Cursor = Cursors.Default
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If

        'マウスポインタ変更
        Me.Cursor = Cursors.Default


    End Sub

    ''' <summary>
    ''' 結果ダブルクリック時処理
    ''' </summary>
    ''' 
    ''' <remarks>Fpspread1結果セルをダブルクリックした際に行われる処理
    ''' <para>作成情報：2012/05/31 kuga
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwDoc_CellDoubleClick1(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwDoc.CellDoubleClick, vwOther.CellDoubleClick

        'もしヘッダーがクリックされた場合はキャンセル
        If (e.RowHeader = True) OrElse (e.ColumnHeader = True) Then
            Return
        End If

        'CI種別が文書の場合、開くボタンがクリックされた場合はキャンセル
        If Me.vwDoc.Visible = True AndAlso e.Column = logicHBKB0101.COL_DOC_BTN_OPEN Then
            Exit Sub
        End If

        '詳細確認ボタンの処理へ
        btnConf_Click(sender, New System.EventArgs)

    End Sub

    ''' <summary>
    ''' デフォルトソートボタン押下時処理
    ''' </summary>
    ''' 
    ''' <remarks>デフォルトソートボタンを押下した際に行われる処理
    ''' <para>作成情報：2012/05/31 kuga
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSort.Click

        '一覧の表示順をデフォルトに戻す
        If logicHBKB0101.SortDefaultMain(dataHBKB0101) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 新規登録ボタン押下時処理
    ''' </summary>
    ''' 
    ''' <remarks>新規登録ボタンを押下した際に行われる処理
    ''' <para>作成情報：2012/05/31 kuga
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnNewReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewReg.Click

        '新規登録ボタンクリック時のCI種別により遷移先分岐
        Select Case lstCiClass.SelectedValue

            Case CI_TYPE_SYSTEM    'CI種別がシステムの場合

                'システム登録画面インスタンス作成
                Dim HBKB0401 As New HBKB0401

                'システム登録画面データクラスに対しプロパティ設定
                With HBKB0401.dataHBKB0401
                    .PropStrProcMode = PROCMODE_NEW '処理モード：新規登録
                End With

                '当画面非表示
                Me.Hide()
                blnGroupChangeFlg = True
                'システム登録画面表示
                HBKB0401.ShowDialog()
                '当画面表示
                Me.Show()


            Case CI_TYPE_DOC       'CI種別が文書の場合

                '文書登録画面インスタンス作成
                Dim HBKB0501 As New HBKB0501

                '文書登録画面データクラスに対しプロパティ設定
                With HBKB0501.dataHBKB0501
                    .PropStrProcMode = PROCMODE_NEW '処理モード：新規登録
                End With

                '当画面非表示
                Me.Hide()
                blnGroupChangeFlg = True
                '文書登録画面表示
                HBKB0501.ShowDialog()
                '当画面表示
                Me.Show()


            Case CI_TYPE_KIKI      'CI種別が部所有機器の場合

                '部所有機器登録画面インスタンス作成
                Dim HBKB1301 As New HBKB1301

                '部所有機器登録画面データクラスに対しプロパティ設定
                With HBKB1301.dataHBKB1301
                    .PropStrProcMode = PROCMODE_NEW '処理モード：新規登録
                End With

                '当画面非表示
                Me.Hide()
                blnGroupChangeFlg = True
                '部所有機器登録画面表示
                HBKB1301.ShowDialog()
                '当画面表示
                Me.Show()

        End Select

    End Sub

    ''' <summary>
    ''' 詳細確認ボタン押下時処理
    ''' </summary>
    ''' 
    ''' <remarks>詳細確認ボタンを押下した際に行われる処理
    ''' <para>作成情報：2012/05/31 kuga
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnConf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConf.Click

        '※詳細確認遷移先は製造対象ではないのでモックアップを使用※
        Dim sl As String = ""
        Dim num As String = ""
        Dim name As String = ""

        '【EDIT】 2012/09/27 r.hoshino START
        '' 選択データがない場合エラーメッセージを表示する
        ''表示行よりCI種別CDを取得
        'If Me.vwDoc.Visible = True AndAlso Me.vwDoc.Sheets(0).RowCount > 0 Then
        '    num = Me.vwDoc.Sheets(0).GetValue(0, logicHBKB0101.COL_DOC_CIKBNCD)
        'ElseIf Me.vwOther.Visible = True AndAlso Me.vwOther.Sheets(0).RowCount > 0 Then
        '    num = Me.vwOther.Sheets(0).GetValue(0, logicHBKB0101.COL_OTHER_CIKBNCD)
        'End If
        If Me.vwDoc.Visible = True Then
            If Me.vwDoc.Sheets(0).RowCount > 0 Then
                num = Me.vwDoc.Sheets(0).GetValue(0, logicHBKB0101.COL_DOC_CIKBNCD)
            Else
                'エラーメッセージ表示
                MsgBox(B0101_E001, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If
        ElseIf Me.vwOther.Visible = True Then
            If Me.vwOther.Sheets(0).RowCount > 0 Then
                num = Me.vwOther.Sheets(0).GetValue(0, logicHBKB0101.COL_OTHER_CIKBNCD)
            Else
                'エラーメッセージ表示
                MsgBox(B0101_E001, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If
        End If
        '【EDIT】 2012/09/27 r.hoshino END

        '[Del] 2012/10/30 s.yamaguchi START
        ''CI種別が文書の場合
        'If num = CI_TYPE_DOC Then

        '    'CI種別が文書の場合
        '    Dim cr() As FarPoint.Win.Spread.Model.CellRange
        '    cr = vwDoc.ActiveSheet.GetSelections()

        '    ' 未選択の場合エラーメッセージを表示する
        '    If cr.Length = 0 Then
        '        'エラーメッセージ表示
        '        MsgBox(B0101_E001, MsgBoxStyle.Critical, TITLE_ERROR)
        '        Return
        '    End If

        '    'フォーカスが移動したときの行数を取得する（列ヘッダ＆全選択が選択されている場合はエラー）
        '    For i As Integer = 0 To cr.Length - 1

        '        '行数が１以外のときはエラー
        '        If (cr(i).RowCount() <> 1) Then
        '            'エラーメッセージ表示
        '            MsgBox(B0101_E001, MsgBoxStyle.Critical, TITLE_ERROR)
        '            Return
        '        ElseIf (cr(i).RowCount() = 1) Then

        '            '文書登録(編集モード)へ遷移
        '            Dim HBKB0501 As New HBKB0501
        '            With HBKB0501.dataHBKB0501
        '                .PropStrProcMode = PROCMODE_EDIT                                                                '処理モード：編集
        '                .PropIntCINmb = _
        '                    vwDoc.ActiveSheet.GetValue(vwDoc.ActiveSheet.ActiveRowIndex, logicHBKB0101.COL_DOC_CINMB)   'CI番号
        '            End With
        '            Me.Hide()
        '            blnGroupChangeFlg = True
        '            HBKB0501.ShowDialog()
        '            Me.Show()
        '            Return

        '        End If
        '    Next

        '    Exit Sub


        'ElseIf num = CI_TYPE_SYSTEM Or num = CI_TYPE_SUPORT Or num = CI_TYPE_KIKI Then

        '    'CI種別がシステム・サポセン機器・部所有機器の場合
        '    Dim cr() As FarPoint.Win.Spread.Model.CellRange
        '    cr = vwOther.ActiveSheet.GetSelections()

        '    ' 未選択の場合エラーメッセージを表示する
        '    If cr.Length = 0 Then
        '        'エラーメッセージ表示
        '        MsgBox(B0101_E001, MsgBoxStyle.Critical, TITLE_ERROR)
        '        Return
        '    End If

        '    '種別(セル)の値がシステムの場合
        '    If num = CI_TYPE_SYSTEM Then

        '        'フォーカスが移動したときの行数を取得する（列ヘッダ＆全選択が選択されている場合はエラー）
        '        For i As Integer = 0 To cr.Length - 1

        '            '行数が１以外のときはエラー
        '            If (cr(i).RowCount() <> 1) Then
        '                'エラーメッセージ表示
        '                MsgBox(B0101_E001, MsgBoxStyle.Critical, TITLE_ERROR)
        '                Return
        '            ElseIf (cr(i).RowCount() = 1) Then

        '                'システム登録(編集モード)へ遷移
        '                Dim HBKB0401 As New HBKB0401

        '                With HBKB0401.dataHBKB0401
        '                    .PropStrProcMode = PROCMODE_EDIT                                                                       '処理モード：編集
        '                    .PropIntCINmb = _
        '                        vwOther.ActiveSheet.GetValue(vwOther.ActiveSheet.ActiveRowIndex, logicHBKB0101.COL_OTHER_CINMB)    'CI番号
        '                End With

        '                Me.Hide()
        '                blnGroupChangeFlg = True
        '                HBKB0401.ShowDialog()
        '                Me.Show()
        '                Return

        '            End If
        '        Next

        '        '種別(セル)の値がサポセン機器の場合
        '    ElseIf num = CI_TYPE_SUPORT Then

        '        'フォーカスが移動したときの行数を取得する（列ヘッダ＆全選択が選択されている場合はエラー）
        '        For i As Integer = 0 To cr.Length - 1

        '            '行数が１以外のときはエラー
        '            If (cr(i).RowCount() <> 1) Then
        '                'エラーメッセージ表示
        '                MsgBox(B0101_E001, MsgBoxStyle.Critical, TITLE_ERROR)
        '                Return
        '            ElseIf (cr(i).RowCount() = 1) Then

        '                'サポセン機器登録画面インスタンス作成
        '                Dim HBKB0601 As New HBKB0601

        '                'サポセン機器登録画面データクラスに対しプロパティ設定
        '                With HBKB0601.dataHBKB0601
        '                    .PropStrProcMode = PROCMODE_REF                                                                     '処理モード：参照モード
        '                    .PropIntCINmb = _
        '                       vwOther.ActiveSheet.GetValue(vwOther.ActiveSheet.ActiveRowIndex, logicHBKB0101.COL_OTHER_CINMB)  'CI番号
        '                End With

        '                '当画面非表示
        '                Me.Hide()
        '                blnGroupChangeFlg = True
        '                'サポセン機器登録画面表示
        '                HBKB0601.ShowDialog()
        '                '当画面表示
        '                Me.Show()
        '                Return

        '            End If
        '        Next

        '        '種別(セル)の値が部所有機器の場合
        '    ElseIf num = CI_TYPE_KIKI Then

        '        'フォーカスが移動したときの行数を取得する（列ヘッダ＆全選択が選択されている場合はエラー）
        '        For i As Integer = 0 To cr.Length - 1

        '            '行数が１以外のときはエラー
        '            If (cr(i).RowCount() <> 1) Then
        '                'エラーメッセージ表示
        '                MsgBox(B0101_E001, MsgBoxStyle.Critical, TITLE_ERROR)
        '                Return
        '            ElseIf (cr(i).RowCount() = 1) Then

        '                '部所有機器登録
        '                Dim HBKB1301 As New HBKB1301

        '                '部所有機器登録画面データクラスに対しプロパティ設定
        '                With HBKB1301.dataHBKB1301
        '                    .PropStrProcMode = PROCMODE_EDIT                                                                      '処理モード：編集
        '                    .PropIntCINmb = _
        '                       vwOther.ActiveSheet.GetValue(vwOther.ActiveSheet.ActiveRowIndex, logicHBKB0101.COL_OTHER_CINMB)    'CI番号
        '                End With

        '                Me.Hide()
        '                blnGroupChangeFlg = True
        '                HBKB1301.ShowDialog()
        '                Me.Show()

        '                Return
        '            End If
        '        Next

        '    End If
        'End If
        '[Del] 2012/10/30 s.yamaguchi END

        'CI種別が文書の場合
        If num = CI_TYPE_DOC Then

            '[Add] 2012/10/30 s.yamaguchi START
            '変数宣言
            Dim intSelectedRowFrom As Integer                   '選択開始行番号
            Dim intSelectedRowTo As Integer                     '選択終了行番号

            '選択開始行、終了行取得
            intSelectedRowFrom = vwDoc.Sheets(0).Models.Selection.AnchorRow
            intSelectedRowTo = vwDoc.Sheets(0).Models.Selection.LeadRow

            '行選択を明示的に行う。
            With vwDoc
                .ActiveSheet.Models.Selection.AddSelection(.ActiveSheet.ActiveRowIndex, _
                                                           .ActiveSheet.ActiveColumnIndex, _
                                                           1, _
                                                           System.Math.Abs(intSelectedRowTo - intSelectedRowFrom) + 1)
            End With

            '検索結果の選択数が一件以外の時はエラーメッセージ出力
            If vwDoc.Sheets(0).SelectionCount <> 1 _
               Or intSelectedRowTo - intSelectedRowFrom <> 0 Then
                puErrMsg = B0101_E001
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Exit Sub
            End If
            '[Add] 2012/10/30 s.yamaguchi END

            '文書登録(編集モード)へ遷移
            Dim HBKB0501 As New HBKB0501
            With HBKB0501.dataHBKB0501
                .PropStrProcMode = PROCMODE_EDIT                                                                '処理モード：編集
                .PropIntCINmb = _
                    vwDoc.ActiveSheet.GetValue(vwDoc.ActiveSheet.ActiveRowIndex, logicHBKB0101.COL_DOC_CINMB)   'CI番号
            End With
            Me.Hide()
            blnGroupChangeFlg = True
            HBKB0501.ShowDialog()
            Me.Show()
            Return

            Exit Sub

        ElseIf num = CI_TYPE_SYSTEM Or num = CI_TYPE_SUPORT Or num = CI_TYPE_KIKI Then

            '[Add] 2012/10/30 s.yamaguchi START
            '変数宣言
            Dim intSelectedRowFrom As Integer                   '選択開始行番号
            Dim intSelectedRowTo As Integer                     '選択終了行番号

            '選択開始行、終了行取得
            intSelectedRowFrom = vwOther.Sheets(0).Models.Selection.AnchorRow
            intSelectedRowTo = vwOther.Sheets(0).Models.Selection.LeadRow

            '行選択を明示的に行う。
            With vwOther
                .ActiveSheet.Models.Selection.AddSelection(.ActiveSheet.ActiveRowIndex, _
                                                           .ActiveSheet.ActiveColumnIndex, _
                                                           1, _
                                                           System.Math.Abs(intSelectedRowTo - intSelectedRowFrom) + 1)
            End With

            '検索結果の選択数が一件以外の時はエラーメッセージ出力
            If vwOther.Sheets(0).SelectionCount <> 1 _
               Or intSelectedRowTo - intSelectedRowFrom <> 0 Then
                puErrMsg = B0101_E001
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Exit Sub
            End If
            '[Add] 2012/10/30 s.yamaguchi END

            '種別(セル)の値がシステムの場合
            If num = CI_TYPE_SYSTEM Then

                'システム登録(編集モード)へ遷移
                Dim HBKB0401 As New HBKB0401

                With HBKB0401.dataHBKB0401
                    .PropStrProcMode = PROCMODE_EDIT                                                                       '処理モード：編集
                    .PropIntCINmb = _
                        vwOther.ActiveSheet.GetValue(vwOther.ActiveSheet.ActiveRowIndex, logicHBKB0101.COL_OTHER_CINMB)    'CI番号
                End With

                Me.Hide()
                blnGroupChangeFlg = True
                HBKB0401.ShowDialog()
                Me.Show()

                '種別(セル)の値がサポセン機器の場合
            ElseIf num = CI_TYPE_SUPORT Then

                'サポセン機器登録画面インスタンス作成
                Dim HBKB0601 As New HBKB0601

                'サポセン機器登録画面データクラスに対しプロパティ設定
                With HBKB0601.dataHBKB0601
                    .PropStrProcMode = PROCMODE_REF                                                                     '処理モード：参照モード
                    .PropIntCINmb = _
                       vwOther.ActiveSheet.GetValue(vwOther.ActiveSheet.ActiveRowIndex, logicHBKB0101.COL_OTHER_CINMB)  'CI番号
                End With

                '当画面非表示
                Me.Hide()
                blnGroupChangeFlg = True
                'サポセン機器登録画面表示
                HBKB0601.ShowDialog()
                '当画面表示
                Me.Show()

                '種別(セル)の値が部所有機器の場合
            ElseIf num = CI_TYPE_KIKI Then

                '部所有機器登録
                Dim HBKB1301 As New HBKB1301

                '部所有機器登録画面データクラスに対しプロパティ設定
                With HBKB1301.dataHBKB1301
                    .PropStrProcMode = PROCMODE_EDIT                                                                      '処理モード：編集
                    .PropIntCINmb = _
                       vwOther.ActiveSheet.GetValue(vwOther.ActiveSheet.ActiveRowIndex, logicHBKB0101.COL_OTHER_CINMB)    'CI番号
                End With

                Me.Hide()
                blnGroupChangeFlg = True
                HBKB1301.ShowDialog()
                Me.Show()

            End If
        End If
    End Sub

    ''' <summary>
    ''' 一括登録ボタン押下時処理
    ''' </summary>
    ''' 
    ''' <remarks>一括登録ボタンを押下した際に行われる処理
    ''' <para>作成情報：2012/05/31 kuga
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnUpPack_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpPack.Click

        '一括登録画面インスタンス作成
        Dim HBKB0201 As New HBKB0201

        'CI種別によりデータクラスにパラメータセット
        Select Case Me.lstCiClass.SelectedValue

            Case CI_TYPE_SYSTEM                     'システム

                With HBKB0201.dataHBKB0201
                    .PropStrCIKbnCd = CI_TYPE_SYSTEM
                    .PropStrCIKbnNm = CI_TYPE_SYSTEM_NM
                End With

            Case CI_TYPE_DOC                        '文書

                With HBKB0201.dataHBKB0201
                    .PropStrCIKbnCd = CI_TYPE_DOC
                    .PropStrCIKbnNm = CI_TYPE_DOC_NM
                End With

            Case CI_TYPE_KIKI                       '部所有機器

                With HBKB0201.dataHBKB0201
                    .PropStrCIKbnCd = CI_TYPE_KIKI
                    .PropStrCIKbnNm = CI_TYPE_KIKI_NM
                End With

        End Select

        '画面遷移
        Me.Hide()
        blnGroupChangeFlg = True
        HBKB0201.ShowDialog()
        Me.Show()

    End Sub

    ''' <summary>
    ''' Excel出力ボタン押下時処理
    ''' </summary>
    ''' 
    ''' <remarks>Excel出力ボタンを押下した際に行われる処理
    ''' <para>作成情報：2012/05/31 kuga
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOutput.Click

        Try
            '結果件数が１件以上の場合のみ実行
            If ((Me.vwDoc.Visible = True) And (Me.vwDoc.Sheets(0).Rows.Count <> 0)) Or _
               ((Me.vwOther.Visible = True) And (Me.vwOther.Sheets(0).Rows.Count <> 0)) Then

                '変数宣言
                Dim logicHBKB0102 As New LogicHBKB0102  'EXCEL出力ロジッククラス
                Dim dataHBKB0102 As New DataHBKB0102    'EXCEL出力データクラス
                Dim sfd As New SaveFileDialog()         'ファイルダイアログ
                Dim strOutputFileName As String = ""    'EXCEL出力ファイル名

                'デフォルトのファイル名をセット
                sfd.FileName = "構成一覧_" & DateTime.Now.ToString("yyyyMMddHHmmss") & ".xlsx"
                strOutputFileName = sfd.FileName

                'デフォルトで表示されるフォルダを指定
                sfd.InitialDirectory = ""

                'デフォルトで表示される[ファイルの種類]を選択する
                sfd.Filter = "Excel Files (*.xlsx)|*.xlsx"

                'ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
                sfd.RestoreDirectory = True

                'ダイアログを表示し、OKボタンクリック時にEXCEL出力処理を行う
                If sfd.ShowDialog() = DialogResult.OK Then

                    'EXCEL出力データクラスにプロパティセット
                    With dataHBKB0102
                        'EXCEL出力パス
                        .PropStrOutPutFileName = strOutputFileName
                        .PropStrOutPutFilePath = sfd.FileName
                        '検索条件
                        .PropStrGroupCD_Search = dataHBKB0101.PropStrGroupCD_Search                     'グループCD
                        .PropStrCiKbnCD_Search = dataHBKB0101.PropStrCiKbnCD_Search                     'CI種別CD
                        .PropStrKindCD_Search = dataHBKB0101.PropStrKindCD_Search                       '種別CD
                        .PropStrNum_Search = dataHBKB0101.PropStrNum_Search                             '番号
                        .PropStrStatusCD_Search = dataHBKB0101.PropStrStatusCD_Search                   'ステータスCD
                        .PropStrCiOwnerCD_Search = dataHBKB0101.PropStrCiOwnerCD_Search                 'CIオーナーCD
                        .PropStrClass1_Search = dataHBKB0101.PropStrClass1_Search                       '分類１
                        .PropStrClass2_Search = dataHBKB0101.PropStrClass2_Search                       '分類２
                        .PropStrCINM_Search = dataHBKB0101.PropStrCINM_Search                           '名称
                        .PropStrFreeWordAimai_Search = dataHBKB0101.PropStrFreeWordAimai_Search         'フリーワード
                        .PropStrUpdateDTFrom_Search = dataHBKB0101.PropStrUpdateDTFrom_Search           '最終更新日(FROM)
                        .PropStrUpdateDTTo_Search = dataHBKB0101.PropStrUpdateDTTo_Search               '最終更新日(TO)
                        .PropStrBikoAimai_Search = dataHBKB0101.PropStrBikoAimai_Search                 'フリーテキスト
                        .PropStrFreeFlg1_Search = dataHBKB0101.PropStrFreeFlg1_Search                   'フリーフラグ1
                        .PropStrFreeFlg2_Search = dataHBKB0101.PropStrFreeFlg2_Search                   'フリーフラグ2
                        .PropStrFreeFlg3_Search = dataHBKB0101.PropStrFreeFlg3_Search                   'フリーフラグ3
                        .PropStrFreeFlg4_Search = dataHBKB0101.PropStrFreeFlg4_Search                   'フリーフラグ4
                        .PropStrFreeFlg5_Search = dataHBKB0101.PropStrFreeFlg5_Search                   'フリーフラグ5
                        .PropStrShareteamNM_Search = dataHBKB0101.PropStrShareteamNM_Search             '文書配付先
                    End With


                    'マウスポインタ変更(通常→砂時計)
                    Me.Cursor = Windows.Forms.Cursors.WaitCursor

                    'エクセル出力
                    If (logicHBKB0102.ExcelExportMain(dataHBKB0102)) = False Then
                        'エラーメッセージ表示
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Return
                    End If


                    'マウスポインタ変更(砂時計→通常)
                    Me.Cursor = Windows.Forms.Cursors.Default

                    '登録完了メッセージ表示
                    MsgBox(B0101_I002, MsgBoxStyle.Information, TITLE_INFO)

                End If

            Else
                Return
            End If

        Catch ex As Exception
            Common.CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'エラーメッセージ表示
            MsgBox(HBK_E001 & ex.Message, MsgBoxStyle.Critical, TITLE_ERROR)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    ''' <summary>
    ''' 戻るボタン押下時処理
    ''' </summary>
    ''' 
    ''' <remarks>戻るボタンを押下した際に行われる処理
    ''' <para>作成情報：2012/05/31 kuga
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        'メニューへ遷移する
        Me.Close()
    End Sub


End Class