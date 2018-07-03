Imports Common
Imports CommonHBK
Imports System.Diagnostics

''' <summary>
''' ノウハウURL選択画面Interfaceクラス
''' </summary>
''' <remarks>ノウハウURL選択画面の設定を行う
''' <para>作成情報：2012/07/23 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKC0501

    'インスタンス作成
    Public dataHBKC0501 As New DataHBKC0501
    Public logicHBKC0501 As New LogicHBKC0501
    Private commonLogicHBK As New CommonLogicHBK

    '変数宣言
    Private blnDoSetFile As Boolean    '設定フラグ

    ''' <summary>
    ''' 画面表示時の処理
    ''' </summary>
    ''' <remarks>フラグの制御と画面のポップアップ表示を行う
    ''' <para>作成情報：2012/07/23 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Overloads Function ShowDialog() As Boolean

        'ファイル設定フラグ初期化
        blnDoSetFile = False

        '当画面をポップアップ表示
        MyBase.ShowDialog()

        'ファイル設定フラグを返す
        Return blnDoSetFile

    End Function

    ''' <summary>
    ''' フォームロード時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>画面の初期設定を行う
    ''' <para>作成情報：2012/07/23 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKC0501_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'データクラス初期設定
        With dataHBKC0501
            .PropVwKnowhowUrlList = Me.vwKnowhowurlList
        End With

        'フォーム背景色設定
        Me.BackColor = CommonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        '画面初期表示処理
        If logicHBKC0501.InitFormMain(dataHBKC0501) = False Then
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
    ''' <remarks>ノウハウURL一覧の選択行のURLをブラウザ起動する
    ''' <para>作成情報：2012/07/23 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        'ノウハウURL一覧選択時処理
        If logicHBKC0501.SelectRowMain(dataHBKC0501) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' [ノウハウURL説明]ダブルクリック処理
    ''' </summary>
    ''' <remarks>ノウハウURL一覧の選択行のURLをブラウザ起動する
    ''' <para>作成情報：2012/07/23 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwKnowhowurlList_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwKnowhowurlList.CellDoubleClick
        btnSelect_Click(sender, e)
    End Sub

    ''' <summary>
    ''' [閉じる]ボタン押下時処理
    ''' </summary>
    ''' <remarks>当画面を閉じて呼び出し元画面へ戻る
    ''' <para>作成情報：2012/07/23 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReturn.Click
        Me.Close()
    End Sub

End Class