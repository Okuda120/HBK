Imports Common
Imports CommonHBK
Public Class HBKZ0201

    Public dataHBKZ0201 As New DataHBKZ0201
    Public logicHBKZ0201 As New LogicHBKZ0201
    Private commonLogicHBK As New CommonLogicHBK
    Public intSelect As Integer = 0 '選択の列は0番目

    Private Sub HBKZ0201_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try

            'データをセット
            With dataHBKZ0201
                .PropVwList = Me.vwList
            End With

            'シートは読み取り専用
            vwList.Sheets(0).DefaultStyle.Locked = True

            '' 選択モード
            'If dataHBKZ0201.PropMode = SELECT_MODE_SINGLE Then
            '    btnAllcheck.Enabled = False
            '    btnAllUnCheck.Enabled = False
            'Else
            '    btnAllcheck.Enabled = True
            '    btnAllUnCheck.Enabled = True
            'End If

            ' 選択モード
            If dataHBKZ0201.PropMode = SELECT_MODE_SINGLE Then
                btnAllcheck.Enabled = False
                btnAllUnCheck.Enabled = False
                btnAllcheck.Visible = False
                btnAllUnCheck.Visible = False
            Else
                btnAllcheck.Enabled = True
                btnAllUnCheck.Enabled = True
                btnAllcheck.Visible = True
                btnAllUnCheck.Visible = True
            End If


            '呼び出し元にて検索条件が設定されている場合は初期検索する
            If dataHBKZ0201.PropArgs.ToString <> "" Then

                ' 件数の取得
                If logicHBKZ0201.LoadCount(dataHBKZ0201) = False Then
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Return
                End If

                ' 設定値を超える場合
                If CommonHBK.PropSearchMsgCount < dataHBKZ0201.PropSearchCount Then
                    ' メッセージを表示する
                    '【MOD】2012/06/28 f.nakano START
                    'If CommonHBK.CommonLogicHBK.WarningMsgBox(HBKZ_W001, New String() {CommonHBK.PropSearchMsgCount.ToString()}) = MsgBoxResult.Cancel Then
                    If MsgBox(String.Format(Z0201_W001, PropSearchMsgCount.ToString()), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
                        '【MOD】2012/06/28 f.nakano END
                        Return
                    End If
                End If

                '検索一覧取得処理メインメソッド
                If logicHBKZ0201.LoadListMain(dataHBKZ0201) = False Then
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                End If
            Else
                'ロード時、検索を行わない場合
                If logicHBKZ0201.LoadNoSearchMain(dataHBKZ0201) = False Then
                    Exit Sub
                End If

            End If

            'チェックボックス型セルの設定(1列目)
            Dim ch As New FarPoint.Win.Spread.CellType.CheckBoxCellType()
            dataHBKZ0201.PropVwList.Sheets(0).Columns(0).CellType = ch

            If (dataHBKZ0201.PropVwList.Sheets(0).RowCount > 1 And dataHBKZ0201.PropMode = CommonDeclareHBKZ.SELECT_MODE_SINGLE) Then
                dataHBKZ0201.PropVwList.Sheets(0).Columns(0).Locked = True
            Else
                dataHBKZ0201.PropVwList.Sheets(0).Columns(0).Locked = False
            End If


            Me.lblCount.Text = Me.vwList.Sheets(0).RowCount & "件"

            '画面プロパティ設定
            SpreadConfig()
            Me.txtBusyo.Focus()

        Catch ex As Exception
            'エラーメッセージ表示
            MsgBox(HBK_E001 & ex.Message, MsgBoxStyle.Critical, TITLE_ERROR)
            Common.CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
        End Try

    End Sub

    ''' <summary>
    ''' 「検索」ボタン押下時処理
    ''' </summary>
    ''' <remarks>検索条件をセットし、一覧を表示する
    ''' <para>作成情報：2012/05/29 abe
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Try

            'データをセット
            With dataHBKZ0201
                .PropTxtBusyoName = Me.txtBusyo
                .PropTxtEndUsrId = Me.txtId
                .PropTxtEndUsrNm = Me.txtName
                .PropTxtEndUsrMail = Me.txtMail
                .PropVwList = Me.vwList
            End With


            ' 件数の取得
            If logicHBKZ0201.SearchCount(dataHBKZ0201) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If

            ' 設定値を超える場合
            If CommonHBK.PropSearchMsgCount < dataHBKZ0201.PropSearchCount Then
                ' メッセージを表示する
                '【MOD】2012/06/28 f.nakano START
                'If CommonHBK.CommonLogicHBK.WarningMsgBox(HBKZ_W001, New String() {CommonHBK.PropSearchMsgCount.ToString()}) = MsgBoxResult.Cancel Then
                If MsgBox(String.Format(Z0201_W001, PropSearchMsgCount.ToString()), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
                    '【MOD】2012/06/28 f.nakano END
                    logicHBKZ0201.ClearSpreadRow(dataHBKZ0201)
                    Me.lblCount.Text = "0件"
                    Return
                End If
            End If

            If (dataHBKZ0201.PropSearchCount = 0) Then
                dataHBKZ0201.PropVwList.Sheets(0).Rows.Remove(0, dataHBKZ0201.PropVwList.Sheets(0).Rows.Count)

                '画面プロパティ設定
                SpreadConfig()

                Me.lblCount.Text = "0件"
                'エラーメッセージ表示
                MsgBox(Z0201_I001, MsgBoxStyle.Information, TITLE_INFO)
                Return
            End If

            '検索一覧取得処理メインメソッド
            If logicHBKZ0201.SearchListMain(dataHBKZ0201) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            End If

            'チェックボックス型セルの設定(1列目)
            Dim ch As New FarPoint.Win.Spread.CellType.CheckBoxCellType()
            dataHBKZ0201.PropVwList.Sheets(0).Columns(0).CellType = ch

            If (dataHBKZ0201.PropVwList.Sheets(0).RowCount > 1 And dataHBKZ0201.PropMode = CommonDeclareHBKZ.SELECT_MODE_SINGLE) Then
                dataHBKZ0201.PropVwList.Sheets(0).Columns(0).Locked = True
            Else
                dataHBKZ0201.PropVwList.Sheets(0).Columns(0).Locked = False
            End If

            '画面プロパティ設定
            SpreadConfig()

            Me.lblCount.Text = Me.vwList.Sheets(0).RowCount & "件"

            'If (Me.vwList.Sheets(0).RowCount = 0) Then
            '    'エラーメッセージ表示
            '    MsgBox(Z0101_E003, MsgBoxStyle.Critical, TITLE_ERROR)
            'End If
            Me.txtBusyo.Focus()

        Catch ex As Exception
            'エラーメッセージ表示
            MsgBox(HBK_E001 & ex.Message, MsgBoxStyle.Critical, TITLE_ERROR)
            Common.CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
        End Try
    End Sub


    Private Sub btnAllcheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllcheck.Click

        AllCheck(True)

    End Sub



    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click

        Try

            'データをセット
            With dataHBKZ0201
                '.PropVwList = Me.fpList
                Me.vwList = .PropVwList
            End With


            ' チェックされた行のインデックス取得
            Dim index As Integer() = GetCheckRowIndex(vwList)

            ' 選択されていない場合
            If index.Length = 0 Then
                'エラーメッセージ表示
                MsgBox(Z0201_E001, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If

            '単一選択で複数行選択している場合
            If dataHBKZ0201.PropMode = SELECT_MODE_SINGLE AndAlso index.Length > 1 Then
                'エラーメッセージ表示
                MsgBox(Z0201_E002, MsgBoxStyle.Critical, TITLE_ERROR)
                Return

            Else

            End If

            ' 戻り値をOKにする
            Me.DialogResult = Windows.Forms.DialogResult.OK

        Catch ex As Exception
            'エラーメッセージ表示
            MsgBox(HBK_E001 & ex.Message, MsgBoxStyle.Critical, TITLE_ERROR)
            Common.CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
        End Try

        Me.Close()

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub fpList_CellClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwList.CellClick
        Try

            '複数選択モードではただちに処理を抜ける
            If (dataHBKZ0201.PropMode = CommonDeclareHBKZ.SELECT_MODE_MULTI) Then
                Return
            End If

            Dim index As Integer() = GetCheckRowIndex(vwList)

            'ヘッダーをクリックした場合
            If e.RowHeader = True OrElse e.ColumnHeader = True Then
                Return
            End If

            ' セルが選択されていない、または、ヘッダーをクリックした場合
            If e.Row < 0 OrElse e.Column < 0 Then
                Return
            End If

            ' 選択状態を解除する
            If (index.Length > 0) Then
                vwList.ActiveSheet.SetValue(index(0), 0, False)
            End If

            ' クリックされた行を選択する
            vwList.ActiveSheet.SetValue(e.Row, 0, True)

        Catch ex As Exception
            'エラーメッセージ表示
            MsgBox(HBK_E001 & ex.Message, MsgBoxStyle.Critical, TITLE_ERROR)
            Common.CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
        End Try
    End Sub

    ''' <summary>
    ''' 画面プロパティ関数
    ''' </summary>
    ''' <remarks>画面のプロパティを再設定する
    ''' <para>作成情報：2012/05/29 abe
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub SpreadConfig()

        ''ヘッダー幅の再セット
        'Me.vwList_Sheet1.Columns.Get(0).Width = "39"
        'Me.vwList_Sheet1.Columns.Get(1).Width = "92"
        'Me.vwList_Sheet1.Columns.Get(2).Width = "130"
        'Me.vwList_Sheet1.Columns.Get(3).Width = "120"
        'Me.vwList_Sheet1.Columns.Get(4).Width = "130"
        'Me.vwList_Sheet1.Columns.Get(5).Width = "160"


    End Sub


    Private Sub btnAllUnCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllUnCheck.Click
        AllCheck(False)
    End Sub

    Private Sub vwList_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles vwList.PreviewKeyDown
        If e.KeyCode = Keys.Up Then
            SelectRowCheck(vwList.ActiveSheet.ActiveRowIndex - 1)
        ElseIf e.KeyCode = Keys.Down Then
            SelectRowCheck(vwList.ActiveSheet.ActiveRowIndex + 1)
        End If
    End Sub

    ''' <summary>
    ''' スプレッドダブルクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>選択ボタン押下処理を呼び出す
    ''' <para>作成情報：2012/09/04 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Private Sub fpList_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwList.CellDoubleClick
        '複数選択モードではただちに処理を抜ける
        If (dataHBKZ0201.PropMode = CommonDeclareHBKZ.SELECT_MODE_MULTI) Then
            Return
        End If
        Me.btnSelect_Click(sender, e)
    End Sub

    Private Sub GroupBox1_Enter(sender As System.Object, e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub HBKZ0201_Load_1(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class