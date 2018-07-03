Imports Common
Imports CommonHBK

''' <summary>
''' セット選択画面Interfaceクラス
''' </summary>
''' <remarks>セット選択画面の設定を行う
''' <para>作成情報：2012/09/19 t.fukuo
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKC0701

    'インスタンス作成
    Public dataHBKC0701 As New DataHBKC0701         'データクラス
    Private logicHBKC0701 As New LogicHBKC0701      'ロジッククラス
    Private commonLogic As New CommonLogic          '共通ロジッククラス
    Private commonLogicHBK As New CommonLogicHBK    'HBK共通ロジッククラス


    ''' <summary>
    ''' フォーム読み込み時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>フォームを読み込んだ際に行われる処理
    ''' <para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub HBKC0701_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'データクラス初期設定
        With dataHBKC0701
            .PropCmbKind = Me.cmbKind               '種別コンボボックス
            .PropTxtNum = Me.txtNum                 '番号テキストボックス
            .PropLblCount = Me.lblCount             '件数
            .PropVwList = Me.vwList_Sheet1          '一覧シート
            .PropBtnSelect = Me.btnSelect           '決定ボタン
        End With

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'フォームの初期化
        If logicHBKC0701.InitFormMain(dataHBKC0701) = False Then
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
    ''' <para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Cursors.WaitCursor

        '検索件数の取得
        If logicHBKC0701.GetKikiCountMain(dataHBKC0701) = False Then
            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If

        '検索結果が閾値を超えているか
        If dataHBKC0701.PropIntKikiCount > PropSearchMsgCount Then
            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default
            'キャンセルボタンクリック時は何も処理しない
            If MsgBox(String.Format(C0701_W001, PropSearchMsgCount.ToString()), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
                Exit Sub
            End If
            'マウスポインタ変更(通常→砂時計)
            Me.Cursor = Cursors.WaitCursor
        End If

        '検索開始
        If logicHBKC0701.SearchMain(dataHBKC0701) = False Then
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
    ''' <para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click

        '戻り値作成
        If logicHBKC0701.CreateReturnDataMain(dataHBKC0701) = False Then
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
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
    ''' <para>作成情報：2012/09/19 t.fukuo
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
    ''' <remarks>スプレッドシートのセルをクリックした際の処理(単一選択時の疑似ラジオボックス処理）
    ''' <para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwList_CellClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwList.CellClick

        'ヘッダーをクリックした場合、処理を抜ける
        If e.RowHeader Or e.ColumnHeader Then
            Return
        End If

        '選択行番号取得
        dataHBKC0701.PropIntCheckIndex = e.Row

        '選択行にチェックをつけ、それ以外はチェックを外す
        If logicHBKC0701.ClickVwCellMain(dataHBKC0701) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' スプレッドシートダブルクリック
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>スプレッドシートのセルをクリックした際の処理(単一選択時の疑似ラジオボックス処理）
    ''' <para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwList_CellDoubleClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwList.CellDoubleClick

        '選択ボタンクリック処理へ
        Me.btnSelect_Click(sender, e)

    End Sub

    ''' <summary>
    ''' スプレッドシートキー操作時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>スプレッドシートのチェックのキー操作を行う
    ''' <para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles vwList.KeyDown

        'キー操作をデータクラスにセット
        dataHBKC0701.PropKeyCode = e.KeyCode

        'キー操作に応じてチェック状態を制御する
        If logicHBKC0701.ClickVwCellMain(dataHBKC0701) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If

    End Sub


End Class