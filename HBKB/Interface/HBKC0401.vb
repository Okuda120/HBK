Imports Common
Imports CommonHBK
Imports HBKZ
Imports System.Windows.Forms

''' <summary>
''' 会議記録登録Interfaceクラス
''' </summary>
''' <remarks>会議記録の登録を行う
''' <para>作成情報：2012/07/09 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKC0401

    'インスタンス作成
    Public dataHBKC0401 As New DataHBKC0401
    Private logicHBKC0401 As New LogicHBKC0401
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    '変数宣言
    Private blnDoRollBack As Boolean    'ロールバック実行フラグ

    ''' <summary>
    ''' フォーム終了時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>画面情報の保存を行う
    ''' <para>作成情報：2012/10/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKC0401_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        '設定を変更する
        With Settings.Instance
            'ウィンドウが最大化、最小化以外は保存
            If Me.WindowState = FormWindowState.Normal Then
                .propHBKC0401_Height = Me.Size.Height
                .propHBKC0401_Width = Me.Size.Width
                .propHBKC0401_Y = Me.Location.Y
                .propHBKC0401_X = Me.Location.X
                .propHBKC0401_WindowState = Me.WindowState
            ElseIf Me.WindowState = FormWindowState.Maximized Then
                '最大化は状態を保存
                .propHBKC0401_WindowState = Me.WindowState
            End If
        End With
        '現在の設定をXMLファイルに保存する
        Settings.SaveToXmlFile()
    End Sub

    ''' <summary>
    ''' 画面表示時の処理
    ''' </summary>
    ''' <remarks>フラグの制御と画面のポップアップ表示を行う
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Overloads Function ShowDialog() As Boolean

        'ロールバック実行フラグ初期化
        blnDoRollBack = False

        '当画面をポップアップ表示
        MyBase.ShowDialog()

        'ロールバックフラグを返す
        Return blnDoRollBack

    End Function

    ''' <summary>
    ''' フォームロード時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>会議記録登録画面の初期設定を行う
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKC0401_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'XMLファイルから設定を読み込む
        Settings.LoadFromXmlFile()
        '最大化とか
        Me.WindowState = Settings.Instance.propHBKC0401_WindowState
        'サイズが0だったら初期状態とみなし通常の表示をする。
        If Settings.Instance.propHBKC0401_Height <> 0 Then
            'Me.StartPosition = FormStartPosition.Manual
            'サイズ変更
            Me.Size = New Point(Settings.Instance.propHBKC0401_Width, Settings.Instance.propHBKC0401_Height)
            Me.Location = New Point(Settings.Instance.propHBKC0401_X, Settings.Instance.propHBKC0401_Y)
        End If

        'データクラスの初期設定を行う
        With dataHBKC0401

            .PropTxtMeetingNmb = Me.txtMeetingNmb               'ヘッダ：会議番号テキストボックス
            .PropLblRegInfo = Me.lblRegInfo                     'ヘッダ：登録情報ラベル
            .PropLblUpInfo = Me.lblUpInfo                       'ヘッダ：更新情報ラベル

            .PropDtpYoteiSTDT = Me.dtpYoteiSTDT                 '会議情報：実施予定開始日付テキストボックス
            .PropTxtYoteiSTTM = Me.txtYoteiSTTM                 '会議情報：実施予定開始時刻テキストボックス
            .PropDtpYoteiENDDT = Me.dtpYoteiENDDT               '会議情報：実施予定終了日付テキストボックス
            .PropTxtYoteiENDTM = Me.txtYoteiENDTM               '会議情報：実施予定終了時刻テキストボックス
            .PropDtpJisiSTDT = Me.dtpJisiSTDT                   '会議情報：実施開始日付テキストボックス
            .PropTxtJisiSTTM = Me.txtJisiSTTM                   '会議情報：実施開始時刻テキストボックス
            .PropDtpJisiENDDT = Me.dtpJisiENDDT                 '会議情報：実施終了日付テキストボックス
            .PropTxtJisiENDTM = Me.txtJisiENDTM                 '会議情報：実施終了時刻テキストボックス
            .PropTxtTitle = Me.txtTitle                         '会議情報：タイトルテキストボックス
            .PropCmbHostGrpCD = Me.cmbHostGrpCD                 '会議情報：主催者グループコンボボックス
            .PropTxtHostID = Me.txtHostID                       '会議情報：主催者IDテキストボックス
            .PropTxtHostNM = Me.txtHostNM                       '会議情報：主催者氏名テキストボックス
            .PropBtnSearchHost = Me.btnSearchHost               '会議情報：検索ボタン
            .ProptxtProceedings = Me.txtProceedings             '会議情報：議事録テキストボックス

            .PropVwProcessList = Me.vwProcessList               '会議結果情報：対象プロセス情報スプレッド
            .PropBtnAddRow_Prs = Me.btnAddRow_Prs               '会議結果情報：対象プロセス情報行追加ボタン
            .PropBtnRemoveRow_Prs = Me.btnRemoveRow_Prs         '会議結果情報：対象プロセス情報行削除ボタン
            .PropVwAttendList = Me.vwAttendList                 '会議出席者情報：出席者情報スプレッド
            .PropBtnAddRow_Atn = Me.btnAddRow_Atn               '会議出席者情報：出席者情報行追加ボタン
            .PropBtnRemoveRow_Atn = Me.btnRemoveRow_Atn         '会議出席者情報：出席者情報行削除ボタン
            .PropVwFileList = Me.vwFileList                     '会議関連ファイル情報：ファイル情報スプレッド
            .PropBtnAddRow_Fle = Me.btnAddRow_Fle               '会議関連ファイル情報：ファイル情報行追加ボタン
            .PropBtnRemoveRow_Fle = Me.btnRemoveRow_Fle         '会議関連ファイル情報：ファイル情報行削除ボタン
            .PropBtnFileOpen = Me.btnFileOpen                   '会議関連ファイル情報：「開く」ボタン
            .PropBtnFileDown = Me.btnFileDown                   '会議関連ファイル情報：「保存」ボタン
            .PropVwResultList = Me.vwResultList                 '会議結果情報：結果情報スプレッド
            .PropBtnReg = Me.btnReg                             'フッタ：登録ボタン


            '[add] 2012/09/06 y.ikushima 完了メッセージ表示修正 START
            .PropLblkanryoMsg = Me.lblkanryoMsg             '完了メッセージ
            '[add] 2012/09/06 y.ikushima 完了メッセージ表示修正 END

            '[add] 2012/09/24 y.ikushima 完了メッセージ表示修正 START
            'タイマーのインターバル設定
            Me.timKanryo.Interval = MSG_DISP_TIMER
            .PropLblkanryoMsg.Font = New Font(Me.Font.Name, Me.Font.Size, FontStyle.Bold)
            '[add] 2012/09/24 y.ikushima 完了メッセージ表示修正 END

        End With

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'システムエラー事前対応処理
        If logicHBKC0401.DoProcForErrorMain(dataHBKC0401) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        '画面初期表示メイン処理
        If logicHBKC0401.InitFormNewModeMain(dataHBKC0401) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0401.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If


    End Sub

    ''' <summary>
    ''' 主催者IDテキストボックスEnter時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>主催者IDテキストボックスEnter時にひびきユーザマスタを検索して、主催者氏名テキストボックスに氏名を入れる
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub txtHostID_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHostID.KeyPress

        'ENTERキー押下時のみ処理を行う
        If e.KeyChar = ChrW(Keys.Enter) Then

            'マウスポインタ変更(通常→砂時計)
            Me.Cursor = Windows.Forms.Cursors.WaitCursor

            'ひびきユーザーマスタを検索し、取得したユーザー情報を当画面にセットする
            If logicHBKC0401.CreateIDEnterMain(dataHBKC0401) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKC0401.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default

        End If

    End Sub

    ''' <summary>
    ''' [検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>ひびきユーザ検索一覧画面を表示し、選択されたユーザーを当画面にセットする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnSearchHost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchHost.Click

        'ひびきユーザ検索一覧画面のインスタンス
        Dim HBKZ0101 As New HBKZ0101

        'パラメータセット
        With HBKZ0101.dataHBKZ0101
            .PropMode = SELECT_MODE_SINGLE          'モード：単一選択
            .PropSplitMode = SPLIT_MODE_OR          '検索条件区切り：OR
            .PropArgs = Me.txtHostNM.Text           '検索条件：主催者氏名
        End With

        'ひびきユーザ検索画面を表示し、戻り値としてデータテーブルを取得
        DataHBKC0401.PropDtResultSub = HBKZ0101.ShowDialog()

        '主催者ID、主催者名を変更
        With dataHBKC0401
            If .PropDtResultSub IsNot Nothing Then
                .PropCmbHostGrpCD.SelectedValue = .PropDtResultSub.Rows(0).Item(3)
                .PropTxtHostID.Text = .PropDtResultSub.Rows(0).ItemArray(0)
                .PropTxtHostNM.Text = .PropDtResultSub.Rows(0).ItemArray(2)
            End If
        End With

    End Sub

    ''' <summary>
    ''' 対象プロセス：[＋]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>プロセス検索一覧画面を表示し、選択されたプロセスを当画面（対象プロセス）にセットする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnAddRow_Prs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_Prs.Click

        'プロセス検索一覧画面インスタンス作成
        Dim HBKZ0401 As New HBKZ0401

        'パラメータセット
        With HBKZ0401.dataHBKZ0401
            .PropMode = SELECT_MODE_MULTI       'モード：複数選択
            .PropArgs = String.Empty            '検索条件：なし
            .PropSplitMode = SPLIT_MODE_AND     '検索条件区切り：AND
        End With

        'プロセス検索画面を表示し、戻り値としてデータテーブルを取得
        DataHBKC0401.PropDtResultSub = HBKZ0401.ShowDialog()

        '対象プロセス一覧に取得データをセット
        If logicHBKC0401.SetProcessToVwProcessMain(dataHBKC0401) = False Then
            'エラーメッセージ表示()
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 対象プロセス：[－]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>対象プロセス一覧の選択行を削除する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_Prs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveRow_Prs.Click

        '対象プロセス一覧選択行削除処理
        If logicHBKC0401.RemoveRowProcessMain(dataHBKC0401) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0401.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 会議出席者：[＋]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>ひびきユーザ検索一覧画面を表示し、選択されたユーザーを当画面（会議出席者一覧）にセットする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnAddRow_Atn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_Atn.Click

        'ひびきユーザー検索画面インスタンス作成
        Dim HBKZ0101 As New HBKZ0101

        'パラメータセット
        With HBKZ0101.dataHBKZ0101
            .PropMode = SELECT_MODE_MULTI           'モード：複数選択
            .PropArgs = String.Empty                '検索条件：なし
            .PropSplitMode = SPLIT_MODE_AND         '検索条件区切り：AND
        End With

        'ひびきユーザー検索画面を表示し、戻り値としてデータテーブルを取得
        DataHBKC0401.PropDtResultSub = HBKZ0101.ShowDialog()

        '会議出席者情報一覧に取得データをセット
        If logicHBKC0401.SetUserToVwAttendMain(dataHBKC0401) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 会議出席者：[－]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>会議出席者一覧の選択行を削除する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_Atn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveRow_Atn.Click

        '会議出席者一覧選択行削除処理
        If logicHBKC0401.RemoveRowAttendMain(dataHBKC0401) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0401.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 会議関連ファイル：[＋]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>会議関連ファイル設定画面を表示し、設定されたファイルを当画面（会議関連ファイル一覧）にセットする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_Fle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_Fle.Click

        '関連ファイル設定画面インスタンス作成
        Dim HBKZ1101 As New HBKZ1101

        Dim blnDoSetFile As Boolean = HBKZ1101.ShowDialog()

        '設定画面で設定ボタン押下の場合
        If blnDoSetFile Then
            '会議関連ファイル一覧に取得データをセット
            If logicHBKC0401.SetFileToVwFileMain(dataHBKC0401, _
                                                 HBKZ1101.dataHBKZ1101.PropTxtFilePath.Text, _
                                                 HBKZ1101.dataHBKZ1101.PropTxtFileNaiyo.Text) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If
        End If

    End Sub

    ''' <summary>
    ''' 会議関連ファイル：[－]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>会議関連ファイル一覧の選択行を削除する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_Fle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveRow_Fle.Click

        '会議関連ファイル一覧選択行削除処理
        If logicHBKC0401.RemoveRowFileMain(dataHBKC0401) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0401.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 会議関連ファイル：[開く]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>会議関連ファイル一覧の選択したファイルを読み取り専用で開く
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnFileOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileOpen.Click

        If dataHBKC0401.PropStrProcMode = PROCMODE_EDIT Then        '編集モード  

            '[Del] 2012/10/30 s.yamaguchi START
            'If (Me.vwFileList.Visible = True) AndAlso (Me.vwFileList.Sheets(0).Rows.Count <> 0) Then

            '    Dim cr() As FarPoint.Win.Spread.Model.CellRange
            '    cr = vwFileList.ActiveSheet.GetSelections()

            '    ' 未選択の場合エラーメッセージを表示する
            '    If cr.Length = 0 Then
            '        'エラーメッセージ表示
            '        MsgBox(C0401_E007, MsgBoxStyle.Critical, TITLE_ERROR)
            '        Return
            '    End If

            '    'フォーカスが移動したときの行数を取得する（列ヘッダ＆全選択が選択されている場合はエラー）
            '    For i As Integer = 0 To cr.Length - 1

            '        '行数が１以外のときはエラー
            '        If (cr(i).RowCount() <> 1) Then
            '            'エラーメッセージ表示
            '            MsgBox(C0401_E007, MsgBoxStyle.Critical, TITLE_ERROR)
            '            Return
            '        ElseIf (cr(i).RowCount() = 1) Then

            '            '選択行番号をデータクラスにセット
            '            dataHBKC0401.PropIntSelectedRow = vwFileList.ActiveSheet.ActiveRowIndex

            '            'ファイルオープン処理
            '            If logicHBKC0401.FileOpenMain(dataHBKC0401) = False Then
            '                'システムエラー発生時はトランザクション系コントロールを非活性にする
            '                If puErrMsg.StartsWith(HBK_E001) Then
            '                    If commonLogicHBK.SetCtlUnabled(dataHBKC0401.PropAryTsxCtlList) = False Then
            '                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '                        Exit Sub
            '                    End If
            '                End If
            '                'エラーメッセージ表示
            '                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '                '処理終了
            '                Exit Sub
            '            End If
            '            Return
            '        End If
            '    Next
            'End If
            '[Del] 2012/10/30 s.yamaguchi END

            If (Me.vwFileList.Visible = True) AndAlso (Me.vwFileList.Sheets(0).Rows.Count <> 0) Then

                '[Add] 2012/10/30 s.yamaguchi START
                '変数宣言
                Dim intSelectedRowFrom As Integer                   '選択開始行番号
                Dim intSelectedRowTo As Integer                     '選択終了行番号

                '選択開始行、終了行取得
                intSelectedRowFrom = vwFileList.Sheets(0).Models.Selection.AnchorRow
                intSelectedRowTo = vwFileList.Sheets(0).Models.Selection.LeadRow

                '行選択を明示的に行う。
                With vwFileList
                    .ActiveSheet.Models.Selection.AddSelection(.ActiveSheet.ActiveRowIndex, _
                                                               .ActiveSheet.ActiveColumnIndex, _
                                                               1, _
                                                               System.Math.Abs(intSelectedRowTo - intSelectedRowFrom) + 1)
                End With

                '検索結果の選択数が一件以外の時はエラーメッセージ出力
                If vwFileList.Sheets(0).SelectionCount <> 1 _
                   Or intSelectedRowTo - intSelectedRowFrom <> 0 Then
                    puErrMsg = C0401_E007
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
                '[Add] 2012/10/30 s.yamaguchi END

                '選択行番号をデータクラスにセット
                dataHBKC0401.PropIntSelectedRow = vwFileList.ActiveSheet.ActiveRowIndex

                'ファイルオープン処理
                If logicHBKC0401.FileOpenMain(dataHBKC0401) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKC0401.PropAryTsxCtlList) = False Then
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            Exit Sub
                        End If
                    End If
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' 会議関連ファイル：[ダウンロード]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>会議関連ファイル一覧の選択ファイルを指定されたフォルダーにダウンロードする。
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnFileDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileDown.Click

        If dataHBKC0401.PropStrProcMode = PROCMODE_EDIT Then        '編集モード  

            '[Del] 2012/10/30 s.yamaguchi START
            'If (Me.vwFileList.Visible = True) AndAlso (Me.vwFileList.Sheets(0).Rows.Count <> 0) Then

            '    Dim cr() As FarPoint.Win.Spread.Model.CellRange
            '    cr = vwFileList.ActiveSheet.GetSelections()

            '    ' 未選択の場合エラーメッセージを表示する
            '    If cr.Length = 0 Then
            '        'エラーメッセージ表示
            '        MsgBox(C0401_E007, MsgBoxStyle.Critical, TITLE_ERROR)
            '        Return
            '    End If

            '    'フォーカスが移動したときの行数を取得する（列ヘッダ＆全選択が選択されている場合はエラー）
            '    For i As Integer = 0 To cr.Length - 1

            '        '行数が１以外のときはエラー
            '        If (cr(i).RowCount() <> 1) Then
            '            'エラーメッセージ表示
            '            MsgBox(C0401_E007, MsgBoxStyle.Critical, TITLE_ERROR)
            '            Return
            '        ElseIf (cr(i).RowCount() = 1) Then

            '            '選択行番号をデータクラスにセット
            '            dataHBKC0401.PropIntSelectedRow = vwFileList.ActiveSheet.ActiveRowIndex

            '            '編集モード画面処理
            '            If logicHBKC0401.FileDownLoadMain(dataHBKC0401) = False Then
            '                'システムエラー発生時はトランザクション系コントロールを非活性にする
            '                If puErrMsg.StartsWith(HBK_E001) Then
            '                    If commonLogicHBK.SetCtlUnabled(dataHBKC0401.PropAryTsxCtlList) = False Then
            '                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '                        Exit Sub
            '                    End If
            '                End If
            '                'エラーメッセージ表示
            '                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '                '処理終了
            '                Exit Sub
            '            End If
            '            Return
            '        End If
            '    Next
            'End If
            '[Del] 2012/10/30 s.yamaguchi END

            If (Me.vwFileList.Visible = True) AndAlso (Me.vwFileList.Sheets(0).Rows.Count <> 0) Then

                '[Add] 2012/10/30 s.yamaguchi START
                '変数宣言
                Dim intSelectedRowFrom As Integer                   '選択開始行番号
                Dim intSelectedRowTo As Integer                     '選択終了行番号

                '選択開始行、終了行取得
                intSelectedRowFrom = vwFileList.Sheets(0).Models.Selection.AnchorRow
                intSelectedRowTo = vwFileList.Sheets(0).Models.Selection.LeadRow

                '行選択を明示的に行う。
                With vwFileList
                    .ActiveSheet.Models.Selection.AddSelection(.ActiveSheet.ActiveRowIndex, _
                                                               .ActiveSheet.ActiveColumnIndex, _
                                                               1, _
                                                               System.Math.Abs(intSelectedRowTo - intSelectedRowFrom) + 1)
                End With

                '検索結果の選択数が一件以外の時はエラーメッセージ出力
                If vwFileList.Sheets(0).SelectionCount <> 1 _
                   Or intSelectedRowTo - intSelectedRowFrom <> 0 Then
                    puErrMsg = C0401_E007
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
                '[Add] 2012/10/30 s.yamaguchi END

                '選択行番号をデータクラスにセット
                dataHBKC0401.PropIntSelectedRow = vwFileList.ActiveSheet.ActiveRowIndex

                '編集モード画面処理
                If logicHBKC0401.FileDownLoadMain(dataHBKC0401) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKC0401.PropAryTsxCtlList) = False Then
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            Exit Sub
                        End If
                    End If
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' [登録]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>当画面を閉じて呼び出し元画面へ遷移する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        '[add] 2012/09/06 y.ikushima 完了メッセージ表示修正 START
        'メッセージを初期化
        dataHBKC0401.PropLblkanryoMsg.Text = ""
        '[add] 2012/09/06 y.ikushima 完了メッセージ表示修正 END

        '入力チェック処理
        If logicHBKC0401.CheckInputValueMain(dataHBKC0401) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0401.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        '処理モードに応じた登録処理を行う
        If dataHBKC0401.PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

            '新規登録処理
            If logicHBKC0401.RegistDataOnNewModeMain(dataHBKC0401) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKC0401.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

        ElseIf dataHBKC0401.PropStrProcMode = PROCMODE_EDIT Then        '編集モード

            '更新処理
            If logicHBKC0401.RegistDataOnEditModeMain(dataHBKC0401) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKC0401.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

        End If

        'マウスポインタ変更(砂時計→通常)
        Me.Cursor = Windows.Forms.Cursors.Default

        '[add] 2012/09/06 y.ikushima 完了メッセージ表示修正 START
        '登録完了メッセージ表示
        'MsgBox(C0401_I001, MsgBoxStyle.Information, TITLE_INFO)
        dataHBKC0401.PropLblkanryoMsg.Text = C0401_I001
        '[add] 2012/09/06 y.ikushima 完了メッセージ表示修正 END

        '編集モードで画面再描画
        dataHBKC0401.PropStrProcMode = PROCMODE_EDIT
        HBKC0401_Load(Me, New EventArgs)

        '[add] 2012/09/24 y.ikushima 完了メッセージ表示修正 START
        'タイマーを開始する
        Me.timKanryo.Start()
        '[add] 2012/09/24 y.ikushima 完了メッセージ表示修正 END

    End Sub

    ''' <summary>
    ''' [戻る]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>当画面を閉じて呼び出し元画面へ遷移する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReturn.Click
        '当画面を閉じる
        Me.Close()
    End Sub

    '[add] 2012/09/24 y.ikushima 完了メッセージ表示修正 START
    ''' <summary>
    ''' インタバール経過後の処理の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>当画面を閉じて呼び出し元画面へ遷移する
    ''' <para>作成情報：2012/09/24 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timKanryo.Tick
        '登録完了ラベルを初期化する 
        dataHBKC0401.PropLblkanryoMsg.Text = ""

        'タイマーを停止する
        Me.timKanryo.Stop()

    End Sub
    '[add] 2012/09/24 y.ikushima 完了メッセージ表示修正 END
End Class