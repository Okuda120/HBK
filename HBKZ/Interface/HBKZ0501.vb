Imports System.Text.RegularExpressions
Imports Common
Imports CommonHBK

''' <summary>
''' 設置情報検索一覧
''' </summary>
''' <remarks></remarks>
Public Class HBKZ0501
    ' 変数宣言
    Public dataHBKZ0501 As New DataHBKZ0501
    Private logicHBKZ0501 As New LogicHBKZ0501

    ''' <summary>
    ''' 初期処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>画面描画前処理を行う
    ''' <para>作成情報：2012/06/15 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Private Sub HBKZ0501_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        With dataHBKZ0501
            .PropVwList = vwList
        End With

        ' スプレッドの初期化処理
        If logicHBKZ0501.ViewColumn(dataHBKZ0501) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Return
        End If

    End Sub

    ''' <summary>
    ''' 初期処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>初期処理を行う
    ''' <para>作成情報：2012/06/11 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Private Sub HBKZ0501_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try

            ' データをセット
            With dataHBKZ0501
                ' 検索文字列の分割
                .PropBusyoArray = CommonHBK.CommonLogicHBK.GetSearchStringList(dataHBKZ0501.PropArgs, dataHBKZ0501.PropSplitMode)
                ' 設置情報一覧スプレッドシート
                .PropVwList = vwList
                ' 件数
                .PropCount = lblCount
                .PropVwList = vwList
            End With

            '呼び出し元にて検索条件が設定されていない場合は初期検索しない
            If dataHBKZ0501.PropBusyoArray.Length = 0 Then
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor

            ' 件数の取得
            If logicHBKZ0501.GetListCount(dataHBKZ0501) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If

            ' 設定値を超える場合
            If CommonHBK.PropSearchMsgCount < dataHBKZ0501.PropSearchCount Then
                ' メッセージを表示する
                '【MOD】2012/06/28 f.nakano START
                'If CommonHBK.CommonLogicHBK.WarningMsgBox(HBKZ_W001, New String() {CommonHBK.PropSearchMsgCount.ToString()}) = MsgBoxResult.Cancel Then
                If MsgBox(String.Format(Z0501_W001, PropSearchMsgCount.ToString()), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
                    '【MOD】2012/06/28 f.nakano END
                    Return
                End If
            End If

            ' 一覧の取得
            If logicHBKZ0501.InitFormMain(dataHBKZ0501) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If

            ' 一覧の表示処理
            If logicHBKZ0501.ViewColumn(dataHBKZ0501) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If

        Catch ex As Exception
            'エラーメッセージ表示
            MsgBox(HBK_E001 & ex.Message, MsgBoxStyle.Critical, TITLE_ERROR)
            Common.CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' 検索ボタン押下処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>検索ボタン押下処理
    ''' <para>作成情報：2012/06/11 nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try

            ' 引数の設定
            With dataHBKZ0501
                ' 局
                .PropKyoku = txtKyoku
                ' 部署
                .PropBusyo = txtBusyo
                ' 番組／部屋名
                .PropRoom = txtRoom
                ' 建物
                .PropBuil = txtBuilding
                ' フロア
                .PropFloor = txtFloor
                ' 一覧
                .PropVwList = vwList
                ' 件数
                .PropCount = lblCount
            End With

            Me.Cursor = Cursors.WaitCursor

            ' 件数の取得
            If logicHBKZ0501.SearchCountMain(dataHBKZ0501) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If

            ' 設定値を超える場合
            If CommonHBK.PropSearchMsgCount < dataHBKZ0501.PropSearchCount Then
                ' メッセージを表示する
                '【MOD】2012/06/28 f.nakano START
                'If CommonHBK.CommonLogicHBK.WarningMsgBox(HBKZ_W001, New String() {CommonHBK.PropSearchMsgCount.ToString()}) = MsgBoxResult.Cancel Then
                If MsgBox(String.Format(Z0501_W001, PropSearchMsgCount.ToString()), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
                    '【MOD】2012/06/28 f.nakano END
                    logicHBKZ0501.ClearSpreadRow(dataHBKZ0501)
                    Return
                End If
            End If

            ' 件数が0件の場合
            If dataHBKZ0501.PropSearchCount = 0L Then
                'エラーメッセージ表示
                MsgBox(Z0501_I001, MsgBoxStyle.Information, TITLE_INFO)
                Return
            End If

            ' 一覧の取得
            If logicHBKZ0501.SearchMain(dataHBKZ0501) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If

            ' 一覧の表示処理
            If logicHBKZ0501.ViewColumn(dataHBKZ0501) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If


        Catch ex As Exception
            'エラーメッセージ表示
            MsgBox(HBK_E001 & ex.Message, MsgBoxStyle.Critical, TITLE_ERROR)
            Common.CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' 選択ボタン押下処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>選択ボタン押下処理
    ''' <para>作成情報：2012/06/11 nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        Try

            ' チェックされた行のインデックス取得
            Dim index As Integer() = GetCheckRowIndex(vwList)

            ' 選択されていない場合
            If index.Length = 0 Then
                'エラーメッセージ表示
                MsgBox(Z0501_E001, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If

            '単一選択で複数行選択している場合
            If dataHBKZ0501.PropMode = SELECT_MODE_SINGLE AndAlso index.Length > 1 Then
                'エラーメッセージ表示
                MsgBox(Z0501_E002, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If

            ' 戻り値をOKにする
            Me.DialogResult = Windows.Forms.DialogResult.OK

        Catch ex As Exception
            'エラーメッセージ表示
            MsgBox(HBK_E001 & ex.Message, MsgBoxStyle.Critical, TITLE_ERROR)
            Common.CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            Return
        End Try

        Me.Close()

    End Sub

    ''' <summary>
    ''' 閉じるボタン押下処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>閉じるボタン押下処理
    ''' <para>作成情報：2012/06/11 nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' スプレッドダブルクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>選択ボタン押下処理を呼び出す
    ''' <para>作成情報：2012/06/11 nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Private Sub FpSpread7_CellDoubleClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwList.CellDoubleClick
        '複数選択モードではただちに処理を抜ける
        If dataHBKZ0501.PropMode = CommonDeclareHBKZ.SELECT_MODE_MULTI Then
            Return
        End If
        Me.btnSelect_Click(sender, e)
    End Sub

    ''' <summary>
    ''' スプレッドクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>クリックされた行を選択状態とする
    ''' <para>作成情報：2012/06/11 nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Private Sub FpSpread7_CellClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwList.CellClick
        Try

            SelectRowCheck(e.Row)

        Catch ex As Exception
            'エラーメッセージ表示
            MsgBox(HBK_E001 & ex.Message, MsgBoxStyle.Critical, TITLE_ERROR)
            Common.CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
        End Try
    End Sub

    ''' <summary>
    ''' ↑↓キー入力処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>選択行を変更する
    ''' <para>作成情報：2012/06/14 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Private Sub vwList_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles vwList.PreviewKeyDown
        If e.KeyCode = Keys.Up Then
            SelectRowCheck(vwList.ActiveSheet.ActiveRowIndex - 1)
        ElseIf e.KeyCode = Keys.Down Then
            SelectRowCheck(vwList.ActiveSheet.ActiveRowIndex + 1)
        End If
    End Sub

End Class