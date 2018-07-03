Imports Common
Imports CommonHBK

''' <summary>
''' 対象システム選択画面Interfaceクラス
''' </summary>
''' <remarks>対象システム選択画面の設定を行う
''' <para>作成情報：2012/10/23 s.yamaguchi
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKZ1301

    'インスタンス作成
    Public dataHBKZ1301 As New DataHBKZ1301         'データクラス
    Private logicHBKZ1301 As New LogicHBKZ1301      'ロジッククラス
    Private commonLogic As New CommonLogic          '共通ロジッククラス
    Private commonLogicHBK As New CommonLogicHBK    'HBK共通ロジッククラス

    ''' <summary>
    ''' フォーム読み込み時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>フォームを読み込んだ際に行われる処理
    ''' <para>作成情報：2012/10/23 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub HBKZ1301_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'フォームオブジェクトデータクラスセット
        With dataHBKZ1301
            .PropCmbStatus = Me.cmbStatus           'CIステータスコンボボックス
            .PropTxtClass1 = Me.txtClass1           '分類1テキストボックス
            .PropTxtClass2 = Me.txtClass2           '分類2テキストボックス
            .PropTxtCINm = Me.txtCINm               '名称テキストボックス
            .PropTxtFreeText = Me.txtFreeText       'フリーテキストテキストボックス
            .PropCmbFreeFlg1 = Me.cmbFreeFlg1       'フリーフラグ1
            .PropCmbFreeFlg2 = Me.cmbFreeFlg2       'フリーフラグ2
            .PropCmbFreeFlg3 = Me.cmbFreeFlg3       'フリーフラグ3
            .PropCmbFreeFlg4 = Me.cmbFreeFlg4       'フリーフラグ4
            .PropCmbFreeFlg5 = Me.cmbFreeFlg5       'フリーフラグ5
            .PropLblCount = Me.lblCount             '件数ラベル
            .PropVwList = Me.vwList                 '検索結果一覧スプレッド
        End With

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'フォームの初期化
        If logicHBKZ1301.InitFormMain(dataHBKZ1301) = False Then
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
    ''' <para>作成情報：2012/10/23 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Cursors.WaitCursor

        '検索件数の取得
        If logicHBKZ1301.GetTaisyouSystemCountMain(dataHBKZ1301) = False Then
            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If

        '検索結果が閾値を超えているか
        If dataHBKZ1301.PropIntTaisyouSystemCount > PropSearchMsgCount Then
            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default
            'キャンセルボタンクリック時は何も処理しない
            If MsgBox(String.Format(Z1301_W001, PropSearchMsgCount.ToString()), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
                Exit Sub
            End If
            'マウスポインタ変更(通常→砂時計)
            Me.Cursor = Cursors.WaitCursor
        End If

        '検索結果が１件以上存在するか
        If dataHBKZ1301.PropIntTaisyouSystemCount <= 0 Then
            If logicHBKZ1301.CreateDataTableForVw(dataHBKZ1301) = False Then
                Exit Sub
            End If
            If logicHBKZ1301.SetSheet(dataHBKZ1301) = False Then
                Exit Sub
            End If
            '件数設定
            dataHBKZ1301.PropLblCount.Text = "0件"
            '1件も取得できなかった場合、メッセージ表示
            puErrMsg = Z1301_I001
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Information, TITLE_INFO)
            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default
            Exit Sub
        End If

        '検索開始
        If logicHBKZ1301.SearchMain(dataHBKZ1301) = False Then
            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If

        'マウスポインタ変更(砂時計→通常)
        Me.Cursor = Windows.Forms.Cursors.Default

    End Sub

    ''' <summary>
    ''' 決定ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>決定ボタンを押下した際の処理
    ''' <para>作成情報：2012/10/24 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click

        'チェックされた行のインデックス取得
        dataHBKZ1301.PropIntCheckIndex = GetCheckRowIndex(vwList)

        '選択ボタンクリック時入力チェック
        If logicHBKZ1301.CheckWhenBtnSelectClickMain(dataHBKZ1301) = False Then
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
    ''' <para>作成情報：2012/10/24 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click

        ' 戻り値をキャンセルにする
        Me.DialogResult = Windows.Forms.DialogResult.Cancel

        'フォームを閉じる
        Me.Close()

    End Sub

    ''' <summary>
    ''' スプレッドシートクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>スプレッドシートのセルをクリックした際の処理
    ''' <para>作成情報：2012/10/24 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwList_CellClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwList.CellClick

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
    ''' スプレッドシートダブルクリック
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>スプレッドシートのセルをクリックした際の処理
    ''' <para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwList_CellDoubleClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwList.CellDoubleClick

        'ヘッダーをダブルクリックした場合、処理を抜ける
        If e.RowHeader Or e.ColumnHeader Then
            Return
        End If

        '選択ボタンクリック処理へ
        Me.btnSelect_Click(sender, e)

    End Sub

    ''' <summary>
    ''' スプレッドシートキー操作時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>スプレッドシートのチェックのキー操作を行う
    ''' <para>作成情報：2012/10/24 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles vwList.KeyDown

        If e.KeyCode = Keys.Up Then
            SelectRowCheck(vwList.ActiveSheet.ActiveRowIndex - 1)
        ElseIf e.KeyCode = Keys.Down Then
            SelectRowCheck(vwList.ActiveSheet.ActiveRowIndex + 1)
        End If

    End Sub

End Class