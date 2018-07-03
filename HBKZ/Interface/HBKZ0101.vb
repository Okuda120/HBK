Imports Common
Imports CommonHBK

''' <summary>
''' ひびきユーザー検索画面Interfaceクラス
''' </summary>
''' <remarks>ひびきユーザー検索画面の設定を行う
''' <para>作成情報：2012/06/04 matsuoka
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKZ0101

    Public dataHBKZ0101 As New DataHBKZ0101         'データクラス
    Private logicHBKZ0101 As New LogicHBKZ0101      'ロジッククラス
    Private commonLogic As New CommonLogic          '共通ロジッククラス
    Private commonLogicHBK As New CommonLogicHBK    'HBK共通ロジッククラス

    ''' <summary>
    ''' フォーム読み込み時の処理
    ''' </summary>
    ''' <remarks>フォームを読み込んだ際に行われる処理
    ''' <para>作成情報：2012/06/04 matsuoka
    ''' </para></remarks>
    Private Sub HBKZ0101_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            'データをセット
            With dataHBKZ0101
                .PropTxtSearchUserID = txtUserId
                .PropTxtSearchUserName = txtUserName
                .PropTxtSearchGroupCD = txtGroupCd
                .PropTxtSearchGroupName = txtGroupName
                .PropLblCount = lblCount
                .PropVwList = vwList_Sheet1
                .PropTxtSearchStringArray = commonLogicHBK.GetSearchStringList(.PropArgs, .PropSplitMode)
            End With

            'フォームの初期化
            If logicHBKZ0101.InitForm(dataHBKZ0101) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If

            ''全選択ボタン活性状態
            'If dataHBKZ0101.PropMode = CommonDeclareHBKZ.SELECT_MODE_MULTI Then
            '    '複数選択あり
            '    btnAllCheck.Enabled = True
            '    btnAllUnCheck.Enabled = True
            'Else
            '    '複数選択なし
            '    btnAllCheck.Enabled = False
            '    btnAllUnCheck.Enabled = False
            'End If

            '全選択ボタン活性状態
            If dataHBKZ0101.PropMode = CommonDeclareHBKZ.SELECT_MODE_MULTI Then
                '複数選択あり
                btnAllCheck.Enabled = True
                btnAllUnCheck.Enabled = True
                btnAllCheck.Visible = True
                btnAllUnCheck.Visible = True
            Else
                '複数選択なし
                btnAllCheck.Enabled = False
                btnAllUnCheck.Enabled = False
                btnAllCheck.Visible = False
                btnAllUnCheck.Visible = False
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
    ''' フォーム表示後の処理
    ''' </summary>
    ''' <remarks>フォームを表示した後に行われる処理
    ''' <para>作成情報：2012/06/15 matsuoka
    ''' </para></remarks>
    Private Sub HBKZ0101_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown

        Try
            '呼び出し元にて検索条件が設定されていない場合のみ初期検索する
            If dataHBKZ0101.PropTxtSearchStringArray.Length > 0 Then

                Me.Cursor = Cursors.WaitCursor

                '検索件数の取得
                If logicHBKZ0101.GetHbkUsrCountInit(dataHBKZ0101) = False Then
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Return
                End If

                '検索結果が閾値を超えているか
                If dataHBKZ0101.PropIntGroupCount > CommonHBK.CommonDeclareHBK.PropSearchMsgCount Then
                    If MsgBox(String.Format(Z0101_W001, PropSearchMsgCount.ToString()), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
                        'キャンセルボタン押下
                        commonLogic.WriteLog(LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                        Return
                    End If
                End If

                '検索開始
                If logicHBKZ0101.InitSearch(dataHBKZ0101) = False Then
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Return
                End If

            End If

            'PropDatatableに値があれば初期検索を行う
            If dataHBKZ0101.PropInitMode = 1 Then
                If dataHBKZ0101.PropDataTable.Rows.Count > 0 Then
                    '検索開始
                    If logicHBKZ0101.InitSearch_initMode1(dataHBKZ0101) = False Then
                        'エラーメッセージ表示
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Return
                    End If
                End If
            End If

            'シートの設定を行う
            If logicHBKZ0101.SetSheet(dataHBKZ0101) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
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
    ''' 検索ボタン押下時の処理
    ''' </summary>
    ''' <remarks>検索ボタンを押下した際の処理
    ''' <para>作成情報：2012/05/30 matsuoka
    ''' </para></remarks>
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Try

            Me.Cursor = Cursors.WaitCursor

            '検索件数の取得
            If logicHBKZ0101.GetHbkUserCount(dataHBKZ0101) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If

            '検索結果が１件以上存在するか
            If dataHBKZ0101.PropIntGroupCount <= 0 Then
                '検索結果が存在しない。
                logicHBKZ0101.InitForm(dataHBKZ0101)

                '【EDIT】2012/10/09 r.hoshino　課題No33障害対応：START
                'PropDatatableに値があれば初期検索を行う
                If dataHBKZ0101.PropInitMode = 1 Then
                    If dataHBKZ0101.PropDataTable.Rows.Count > 0 Then
                        '検索開始
                        If logicHBKZ0101.InitSearch_initMode1(dataHBKZ0101) = False Then
                            'エラーメッセージ表示
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            Return
                        End If
                    End If
                End If
                '【EDIT】2012/10/09 r.hoshino　課題No33障害対応：START

                'エラーメッセージ表示
                MsgBox(Z0101_I001, MsgBoxStyle.Information, TITLE_INFO)
                commonLogic.WriteLog(LogLevel.TRACE_Lv, "END", Nothing, Nothing)

                'デフォルトの表示は行う
                If dataHBKZ0101.PropInitMode = 1 Then
                    'シートの設定を行う
                    If logicHBKZ0101.SetSheet(dataHBKZ0101) = False Then
                        'エラーメッセージ表示
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Return
                    End If
                End If
                Return
            End If
            '検索結果が閾値を超えているか
            If dataHBKZ0101.PropIntGroupCount > CommonHBK.CommonDeclareHBK.PropSearchMsgCount Then
                If MsgBox(String.Format(Z0101_W001, PropSearchMsgCount.ToString()), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
                    'キャンセルボタン押下
                    logicHBKZ0101.InitForm(dataHBKZ0101)
                    commonLogic.WriteLog(LogLevel.TRACE_Lv, "END", Nothing, Nothing)

                    'デフォルトの表示は行う
                    If dataHBKZ0101.PropInitMode = 1 Then
                        'シートの設定を行う
                        If logicHBKZ0101.SetSheet(dataHBKZ0101) = False Then
                            'エラーメッセージ表示
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            Return
                        End If
                    End If
                    Return
                End If
            End If

            'DBから取得
            If logicHBKZ0101.Search(dataHBKZ0101) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If

            'シートの設定を行う
            If logicHBKZ0101.SetSheet(dataHBKZ0101) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
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
    ''' 全選択ボタン押下時の処理
    ''' </summary>
    ''' <remarks>全選択ボタンを押下した際の処理
    ''' <para>作成情報：2012/06/04 matsuoka
    ''' </para></remarks>
    Private Sub btnAllCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllCheck.Click

        AllCheck(True)

    End Sub
    ''' <summary>
    ''' 全解除ボタン押下時の処理
    ''' </summary>
    ''' <remarks>全解除ボタンを押下した際の処理
    ''' <para>作成情報：2012/06/08 matsuoka
    ''' </para></remarks>
    Private Sub btnAllUnCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllUnCheck.Click

        AllCheck(False)

    End Sub
    ''' <summary>
    ''' 選択ボタン押下時の処理
    ''' </summary>
    ''' <remarks>選択ボタンを押下した際の処理
    ''' <para>作成情報：2012/06/04 matsuoka
    ''' </para></remarks>
    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click

        Try

            ' チェックされた行のインデックス取得
            Dim index As Integer() = GetCheckRowIndex(vwList)

            ' 選択されていない場合
            If index.Length = 0 Then
                'エラーメッセージ表示
                MsgBox(Z0101_E001, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If

            '単一選択で複数行選択している場合
            If dataHBKZ0101.PropMode = SELECT_MODE_SINGLE AndAlso index.Length > 1 Then
                'エラーメッセージ表示
                MsgBox(Z0101_E002, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If

            ' 戻り値をOKにする
            Me.DialogResult = Windows.Forms.DialogResult.OK
            'フォームを閉じる
            Me.Close()

        Catch ex As Exception
            Common.CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'エラーメッセージ表示
            MsgBox(HBK_E001 & ex.Message, MsgBoxStyle.Critical, TITLE_ERROR)
        End Try

    End Sub
    ''' <summary>
    ''' 閉じるボタン押下時の処理
    ''' </summary>
    ''' <remarks>閉じるボタンを押下した際の処理
    ''' <para>作成情報：2012/05/30 matsuoka
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
    ''' <remarks>Spreadシートのセルをクリックした際の処理(単一選択時の疑似ラジオボックス処理）
    ''' <para>作成情報：2012/06/05 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwList_CellClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwList.CellClick

        Try

            '複数選択モードではただちに処理を抜ける
            If dataHBKZ0101.PropMode = CommonDeclareHBKZ.SELECT_MODE_MULTI Then
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

        Catch ex As Exception
            Common.CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'エラーメッセージ表示
            MsgBox(HBK_E001 & ex.Message, MsgBoxStyle.Critical, TITLE_ERROR)
        End Try

    End Sub
    ''' <summary>
    ''' Spreadシートキー操作
    ''' </summary>
    ''' <remarks>Spreadシートのチェックのキー操作を行う
    ''' <para>作成情報：2012/06/14 nakano
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwList_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles vwList.PreviewKeyDown

        '複数選択モードではただちに処理を抜ける
        If dataHBKZ0101.PropMode = CommonDeclareHBKZ.SELECT_MODE_MULTI Then
            Return
        End If

        If e.KeyCode = Keys.Up Then
            SelectRowCheck(vwList.ActiveSheet.ActiveRowIndex - 1)
        ElseIf e.KeyCode = Keys.Down Then
            SelectRowCheck(vwList.ActiveSheet.ActiveRowIndex + 1)
        End If

    End Sub

    ''' <summary>
    ''' Spreadシートダブルクリック時処理
    ''' </summary>
    ''' <remarks>Spreadシートのセルをダブルクリックされた時の処理
    ''' <para>作成情報：2012/09/04 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwList_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwList.CellDoubleClick
        '複数選択モードではただちに処理を抜ける
        If dataHBKZ0101.PropMode = CommonDeclareHBKZ.SELECT_MODE_MULTI Then
            Return
        End If
        '選択ボタンクリック
        Me.btnSelect_Click(sender, e)

    End Sub

End Class