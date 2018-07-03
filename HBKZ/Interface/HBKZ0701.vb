Imports Common
Imports CommonHBK

''' <summary>
''' 機器検索一覧画面Interfaceクラス
''' </summary>
''' <remarks>機器検索一覧画面の設定を行う
''' <para>作成情報：2012/07/06 t.fukuo
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKZ0701

    Public dataHBKZ0701 As New DataHBKZ0701         'データクラス
    Private logicHBKZ0701 As New LogicHBKZ0701      'ロジッククラス
    Private commonLogic As New CommonLogic          '共通ロジッククラス
    Private commonLogicHBK As New CommonLogicHBK    'HBK共通ロジッククラス

    ''' <summary>
    ''' フォーム読み込み時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>フォームを読み込んだ際に行われる処理
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub HBKZ0701_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'データクラス初期設定
        With dataHBKZ0701
            .PropCmbKind = Me.cmbKind               '種別コンボボックス
            .PropTxtNum = Me.txtNum                 '番号テキストボックス
            .PropCmbCIStatus = Me.cmbCIStatus       'ステータスコンボボックス
            .PropTxtCINM = Me.txtCINM               '名称（機器）テキストボックス
            .PropLblCount = Me.lblCount             '件数
            .PropVwList = Me.vwList_Sheet1          '一覧シート
            .PropBtnAllCheck = Me.btnAllCheck       '全選択ボタン
            .PropBtnAllUnCheck = Me.btnAllUnCheck   '全解除ボタン
        End With

        'フォームの初期化
        If logicHBKZ0701.InitFormMain(dataHBKZ0701) = False Then
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>検索ボタンを押下した際の処理
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Cursors.WaitCursor

        '検索件数の取得
        If logicHBKZ0701.GetKikiCountMain(dataHBKZ0701) = False Then
            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Return
        End If

        '検索結果が閾値を超えているか
        If dataHBKZ0701.PropIntKikiCount > PropSearchMsgCount Then
            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default
            'キャンセルボタンクリック時は何も処理しない
            If MsgBox(String.Format(Z0701_W001, PropSearchMsgCount.ToString()), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
                Exit Sub
            End If
            'マウスポインタ変更(通常→砂時計)
            Me.Cursor = Cursors.WaitCursor
        End If

        '検索結果が１件以上存在するか
        If dataHBKZ0701.PropIntKikiCount <= 0 Then
            If logicHBKZ0701.CreateDataTableForVw(dataHBKZ0701) = False Then
                Exit Sub
            End If
            If logicHBKZ0701.SetSheet(dataHBKZ0701) = False Then
                Exit Sub
            End If
            '件数設定
            dataHBKZ0701.PropLblCount.Text = "0件"
            '1件も取得できなかった場合、メッセージ表示
            puErrMsg = Z0701_I001
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Information, TITLE_INFO)
            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default
            Exit Sub
        End If

        '検索開始
        If logicHBKZ0701.SearchMain(dataHBKZ0701) = False Then
            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Return
        End If

        'マウスポインタ変更(砂時計→通常)
        Me.Cursor = Windows.Forms.Cursors.Default

    End Sub

    ''' <summary>
    ''' 全選択ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>全選択ボタンを押下した際の処理
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAllCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllCheck.Click

        AllCheck(True)

    End Sub

    ''' <summary>
    ''' 全解除ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>全解除ボタンを押下した際の処理
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAllUnCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllUnCheck.Click

        AllCheck(False)

    End Sub

    ''' <summary>
    ''' 選択ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択ボタンを押下した際の処理
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click

        'チェックされた行のインデックス取得
        dataHBKZ0701.PropIntCheckIndex = GetCheckRowIndex(vwList)

        '選択ボタンクリック時入力チェック
        If logicHBKZ0701.CheckWhenBtnSelectClickMain(dataHBKZ0701) = False Then
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Return
        End If

        '戻り値をOKにする
        Me.DialogResult = Windows.Forms.DialogResult.OK

        'フォームを閉じる
        Me.Close()

    End Sub

    ''' <summary>
    ''' 閉じるボタン押下時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>閉じるボタンを押下した際の処理
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click

        ' 戻り値をキャンセルにする
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        'フォームを閉じる
        Me.Close()

    End Sub
    ''' <summary>
    ''' Spreadシートクリック
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>Spreadシートのセルをクリックした際の処理(単一選択時の疑似ラジオボックス処理）
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwList_CellClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwList.CellClick

        '複数選択モードではただちに処理を抜ける
        If dataHBKZ0701.PropStrMode = CommonDeclareHBKZ.SELECT_MODE_MULTI Then
            Return
        End If

        'ヘッダーをクリックした場合、処理を抜ける
        If e.RowHeader Or e.ColumnHeader Then
            Return
        End If

        Dim selectCells As Integer() = GetCheckRowIndex(vwList)
        '選択されていたチェックボックスのクリア
        For Each row As Integer In selectCells
            vwList_Sheet1.SetValue(row, 0, False)
        Next
        'クリックされたセルのチェックボックスをONにする
        vwList_Sheet1.SetValue(e.Row, 0, True)

    End Sub
    ''' <summary>
    ''' Spreadシートキー操作
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>Spreadシートのチェックのキー操作を行う
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwList_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles vwList.PreviewKeyDown

        '複数選択モードではただちに処理を抜ける
        If dataHBKZ0701.PropStrMode = CommonDeclareHBKZ.SELECT_MODE_MULTI Then
            Return
        End If

        If e.KeyCode = Keys.Up Then
            SelectRowCheck(vwList.ActiveSheet.ActiveRowIndex - 1)
        ElseIf e.KeyCode = Keys.Down Then
            SelectRowCheck(vwList.ActiveSheet.ActiveRowIndex + 1)
        End If

    End Sub

    ''' <summary>
    ''' Spreadシートクリック
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>Spreadシートのセルをクリックした際の処理(単一選択時の疑似ラジオボックス処理）
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwList_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwList.CellDoubleClick
        '複数選択モードではただちに処理を抜ける
        If dataHBKZ0701.PropStrMode = CommonDeclareHBKZ.SELECT_MODE_MULTI Then
            Return
        End If
        '選択ボタンクリック
        Me.btnSelect_Click(sender, e)

    End Sub

End Class