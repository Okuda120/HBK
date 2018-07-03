Imports Common
Imports CommonHBK

''' <summary>
''' ナレッジURL選択画面Interfaceクラス
''' </summary>
''' <remarks>ナレッジURL選択画面の設定を行う
''' <para>作成情報：2012/09/04 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKW0101

    'インスタンス作成
    Public dataHBKW0101 As New DataHBKW0101
    Public logicHBKW0101 As New LogicHBKW0101
    Private commonLogicHBK As New CommonLogicHBK

    ''' <summary>
    ''' フォームロード時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>画面の初期設定を行う
    ''' <para>作成情報：2012/09/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKW0101_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'データクラス初期設定
        With dataHBKW0101
            .PropLblItemCount = Me.lblItemCount
            .PropVwKnowledgeUrlList = Me.vwKnowledgeurlList
        End With

        'フォーム背景色設定
        Me.BackColor = CommonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        '画面初期表示処理
        If logicHBKW0101.InitFormMain(dataHBKW0101) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        'マウスポインタ変更(砂時計→通常)
        Me.Cursor = Windows.Forms.Cursors.Default

    End Sub

    ''' <summary>
    ''' [選択]ボタン押下時処理
    ''' </summary>
    ''' <remarks>ナレッジURL一覧の選択行のURLをブラウザ起動する
    ''' <para>作成情報：2012/09/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        'ナレッジURL一覧選択時処理
        If logicHBKW0101.SelectRowMain(dataHBKW0101) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' [ナレッジURL説明]ダブルクリック処理
    ''' </summary>
    ''' <remarks>ナレッジURL一覧の選択行のURLをブラウザ起動する
    ''' <para>作成情報：2012/09/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwKnowledgeurlList_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwKnowledgeurlList.CellDoubleClick
        'ヘッダがクリックされた場合はキャンセル
        If e.RowHeader = True Or e.ColumnHeader = True Then
            Exit Sub
        End If

        btnSelect_Click(sender, e)
    End Sub

    ''' <summary>
    ''' [閉じる]ボタン押下時処理
    ''' </summary>
    ''' <remarks>当画面を閉じて呼び出し元画面へ戻る
    ''' <para>作成情報：2012/09/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReturn.Click
        Me.Close()
    End Sub

End Class