Imports Common
Imports CommonHBK
Imports HBKZ

''' <summary>
''' 会議検索一覧Interfaceクラス
''' </summary>
''' <remarks>会議の検索を行う
''' <para>作成情報：2012/07/09 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKC0301

    'インスタンス生成
    Public dataHBKC0301 As New DataHBKC0301
    Private logicHBKC0301 As New LogicHBKC0301
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    ''' <summary>
    ''' フォーム終了時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>画面情報の保存を行う
    ''' <para>作成情報：2012/10/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKC0301_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        '設定を変更する
        With Settings.Instance
            'ウィンドウが最大化、最小化以外は保存
            If Me.WindowState = FormWindowState.Normal Then
                .propHBKC0301_Height = Me.Size.Height
                .propHBKC0301_Width = Me.Size.Width
                .propHBKC0301_Y = Me.Location.Y
                .propHBKC0301_X = Me.Location.X
                .propHBKC0301_WindowState = Me.WindowState
            ElseIf Me.WindowState = FormWindowState.Maximized Then
                '最大化は状態を保存
                .propHBKC0301_WindowState = Me.WindowState
            End If
        End With
        '現在の設定をXMLファイルに保存する
        Settings.SaveToXmlFile()
    End Sub

    ''' <summary>
    ''' フォームロード時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>会議検索一覧画面の初期設定を行う
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKC0301_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'XMLファイルから設定を読み込む
        Settings.LoadFromXmlFile()
        '最大化とか
        Me.WindowState = Settings.Instance.propHBKC0301_WindowState
        'サイズが0だったら初期状態とみなし通常の表示をする。
        If Settings.Instance.propHBKC0301_Height <> 0 Then
            'Me.StartPosition = FormStartPosition.Manual
            'サイズ変更
            Me.Size = New Point(Settings.Instance.propHBKC0301_Width, Settings.Instance.propHBKC0301_Height)
            Me.Location = New Point(Settings.Instance.propHBKC0301_X, Settings.Instance.propHBKC0301_Y)
        End If

        'プロパティセット
        With dataHBKC0301

            .PropTxtMeetingNmb = Me.txtMeetingNo
            .PropCmbProcessKbn = Me.cmbProcessKbn
            .PropTxtProcessNmb = Me.txtProcessNmb
            .PropDtpYoteiDTFrom = Me.dtpYoteiDTFrom
            .PropDtpYoteiDTTo = Me.dtpYoteiDTTo
            .PropDtpJisiDTFrom = Me.dtpJisiDTFrom
            .PropDtpJisiDTTo = Me.dtpJisiDTTo
            .PropTxtTitle = Me.txtTitle
            .PropCmbHostGrpCD = Me.cmbHostGrpCD
            .PropTxtHostID = Me.txtHostID
            .PropTxtHostNM = Me.txtHostNM
            .PropLblItemCount = Me.lblItemCount
            .PropVwMeetingList = Me.vwMeetingList
            .PropBtnAllcheck = Me.btnAllcheck
            .PropBtnAllrelease = Me.btnAllrelease
            .PropBtnSelect = Me.btnSelect
            .PropBtnClear = Me.btnClear
            .PropBtnSort = Me.btnSort
            .PropBtnReg = Me.btnReg
            .PropBtnDetails = Me.btnDetails
            .PropBtnReturn = Me.btnReturn

        End With

        '会議検索一覧画面初期表示メイン呼出
        If logicHBKC0301.InitFormMain(dataHBKC0301) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
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
            .PropMode = SELECT_MODE_SINGLE
            .PropSplitMode = SPLIT_MODE_AND
            .PropArgs = Me.txtHostNM.Text
        End With

        'ひびきユーザ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKC0301.PropDtResultSub = HBKZ0101.ShowDialog()

        '主催者ID、主催者名を変更
        With dataHBKC0301
            If .PropDtResultSub IsNot Nothing Then
                .PropCmbHostGrpCD.SelectedValue = .PropDtResultSub.Rows(0).Item(3)
                .PropTxtHostID.Text = .PropDtResultSub.Rows(0).ItemArray(0)
                .PropTxtHostNM.Text = .PropDtResultSub.Rows(0).ItemArray(2)
            End If
        End With

    End Sub

    ''' <summary>
    ''' [検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>検索条件に従って会議情報を検索する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        '会議検索結果表示処理メイン呼出
        If logicHBKC0301.SearchDataMain(dataHBKC0301) = False Then
            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default

            'エラーメッセージが設定されている場合は表示
            If puErrMsg <> "" Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Else
                'インフォメーションメッセージを表示
                MsgBox(C0301_I001, MsgBoxStyle.Information, TITLE_INFO)
            End If

            ''エラーメッセージ表示
            'MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        'マウスポインタ変更(砂時計→通常)
        Me.Cursor = Windows.Forms.Cursors.Default

    End Sub

    ''' <summary>
    ''' [全選択]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>会議検索一覧の選択チェックボックスをチェックする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnAllcheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllcheck.Click
        '全明細の選択チェックボックスをオンする
        For i As Integer = 0 To vwMeetingList.ActiveSheet.Rows.Count - 1
            vwMeetingList.ActiveSheet.SetValue(i, 0, True)
        Next
    End Sub

    ''' <summary>
    ''' [全解除]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>会議検索一覧の選択チェックボックスをチェック解除する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnAllrelease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllrelease.Click
        '全明細の選択チェックボックスをオフする
        For i As Integer = 0 To vwMeetingList.ActiveSheet.Rows.Count - 1
            vwMeetingList.ActiveSheet.SetValue(i, 0, False)
        Next
    End Sub

    ''' <summary>
    ''' [デフォルトソート]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>検索結果を検索時のソート順に並び替える
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnSort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSort.Click

        '検索結果が0件の場合、処理を抜ける
        If vwMeetingList.Sheets(0).Rows.Count = 0 Then
            Exit Sub
        End If

        'デフォルトソート
        If logicHBKC0301.SortDefaultMain(dataHBKC0301) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [選択]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>会議記録登録画面を編集モードで呼び出す
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click

        'チェックされた行のインデックス取得
        Dim index As Integer() = GetCheckRowIndex(vwMeetingList)

        ' 選択されていない場合
        If index.Length = 0 Then
            'エラーメッセージ表示
            MsgBox(C0301_E001, MsgBoxStyle.Critical, TITLE_ERROR)
            Return
        End If

        '選択された会議検索一覧DataTableを返す
        dataHBKC0301.PropDtReturnSub = setDataTable(vwMeetingList)

        '戻り値をOKにする
        Me.DialogResult = Windows.Forms.DialogResult.OK
        'フォームを閉じる
        Me.Close()

    End Sub

    ''' <summary>
    ''' 選択された会議情報のデータテーブル作成処理
    ''' </summary>
    ''' <remarks>選択された会議情報をデータテーブルに格納する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function setDataTable(ByVal spread As FarPoint.Win.Spread.FpSpread) As DataTable

        '返却用データテーブルの作成
        Dim dataTable As DataTable
        If spread.DataSource Is Nothing Then
            dataTable = New DataTable
        Else
            dataTable = DirectCast(spread.DataSource, DataTable).Clone()
        End If
        dataTable.Clear()

        '選択された行のインデックスを取得する
        Dim strIndex() As Integer = GetCheckRowIndex(spread)

        '選択された行のDataTableに格納する
        For Each index As Integer In strIndex
            dataTable.ImportRow(DirectCast(spread.DataSource, DataTable).Rows(index))
        Next

        dataTable.Columns.Remove(dataTable.Columns(0))

        Return dataTable

    End Function

    ''' <summary>
    ''' [新規登録]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>会議記録登録画面へ新規登録モードで呼び出す
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click

        '会議記録登録
        Dim HBKC0401 As New HBKC0401

        '会議記録登録画面データクラスに対しプロパティ設定
        With HBKC0401.dataHBKC0401
            .PropBlnTranFlg = dataHBKC0301.PropBlnTranFlg   'メニュー遷移フラグ
            .PropProcessKbn = dataHBKC0301.PropProcessKbn   'プロセス区分
            .PropProcessNmb = dataHBKC0301.PropProcessNmb   'プロセス番号
            .PropTitle = dataHBKC0301.PropTitle             'タイトル
            .PropStrProcMode = PROCMODE_NEW                 '処理モード：新規登録モード
        End With

        '当画面非表示
        Me.Hide()
        '会議記録登録画面表示()
        HBKC0401.ShowDialog()
        '当画面表示
        Me.Show()

    End Sub

    ''' <summary>
    ''' [詳細確認]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>会議記録登録画面を編集モードで呼び出す
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetails.Click

        '【ADD】 2012/09/27 r.hoshino START
        ' 選択データがない場合エラーメッセージを表示する
        If Me.vwMeetingList.Sheets(0).Rows.Count = 0 Then
            'エラーメッセージ表示
            MsgBox(C0301_E001, MsgBoxStyle.Critical, TITLE_ERROR)
            Return
        End If
        '【ADD】 2012/09/27 r.hoshino END

        '[Del] 2012/10/30 s.yamaguchi START
        'If (Me.vwMeetingList.Visible = True) AndAlso (Me.vwMeetingList.Sheets(0).Rows.Count <> 0) Then

        '    Dim cr() As FarPoint.Win.Spread.Model.CellRange
        '    cr = vwMeetingList.ActiveSheet.GetSelections()

        '    ' 未選択の場合エラーメッセージを表示する
        '    If cr.Length = 0 Then
        '        'エラーメッセージ表示
        '        MsgBox(C0301_E001, MsgBoxStyle.Critical, TITLE_ERROR)
        '        Return
        '    End If

        '    'フォーカスが移動したときの行数を取得する（列ヘッダ＆全選択が選択されている場合はエラー）
        '    For i As Integer = 0 To cr.Length - 1

        '        '行数が１以外のときはエラー
        '        If (cr(i).RowCount() <> 1) Then
        '            'エラーメッセージ表示
        '            MsgBox(C0301_E001, MsgBoxStyle.Critical, TITLE_ERROR)
        '            Return
        '        ElseIf (cr(i).RowCount() = 1) Then

        '            '会議記録登録
        '            Dim HBKC0401 As New HBKC0401

        '            '会議記録登録画面データクラスに対しプロパティ設定
        '            With HBKC0401.dataHBKC0401
        '                .PropBlnTranFlg = dataHBKC0301.PropBlnTranFlg   'メニュー遷移フラグ
        '                .PropProcessKbn = dataHBKC0301.PropProcessKbn   'プロセス区分
        '                .PropProcessNmb = dataHBKC0301.PropProcessNmb   'プロセス番号
        '                .PropStrProcMode = PROCMODE_EDIT                '処理モード：編集モード
        '                .PropIntMeetingNmb = vwMeetingList.ActiveSheet.GetValue(vwMeetingList.ActiveSheet.ActiveRowIndex, 1)
        '            End With

        '            '当画面非表示
        '            Me.Hide()
        '            '会議記録登録画面表示
        '            HBKC0401.ShowDialog()
        '            '当画面表示
        '            Me.Show()

        '            Return
        '        End If
        '    Next
        'End If
        '[Del] 2012/10/30 s.yamaguchi END

        If (Me.vwMeetingList.Visible = True) AndAlso (Me.vwMeetingList.Sheets(0).Rows.Count <> 0) Then

            '[Add] 2012/10/30 s.yamaguchi START
            '変数宣言
            Dim intSelectedRowFrom As Integer                   '選択開始行番号
            Dim intSelectedRowTo As Integer                     '選択終了行番号

            '選択開始行、終了行取得
            intSelectedRowFrom = vwMeetingList.Sheets(0).Models.Selection.AnchorRow
            intSelectedRowTo = vwMeetingList.Sheets(0).Models.Selection.LeadRow

            '行選択を明示的に行う。
            With vwMeetingList
                .ActiveSheet.Models.Selection.AddSelection(.ActiveSheet.ActiveRowIndex, _
                                                           .ActiveSheet.ActiveColumnIndex, _
                                                           1, _
                                                           System.Math.Abs(intSelectedRowTo - intSelectedRowFrom) + 1)
            End With

            '検索結果の選択数が一件以外の時はエラーメッセージ出力
            If vwMeetingList.Sheets(0).SelectionCount <> 1 _
               Or intSelectedRowTo - intSelectedRowFrom <> 0 Then
                puErrMsg = C0301_E001
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Exit Sub
            End If
            '[Add] 2012/10/30 s.yamaguchi END

            '会議記録登録
            Dim HBKC0401 As New HBKC0401

            '会議記録登録画面データクラスに対しプロパティ設定
            With HBKC0401.dataHBKC0401
                .PropBlnTranFlg = dataHBKC0301.PropBlnTranFlg   'メニュー遷移フラグ
                .PropProcessKbn = dataHBKC0301.PropProcessKbn   'プロセス区分
                .PropProcessNmb = dataHBKC0301.PropProcessNmb   'プロセス番号
                .PropStrProcMode = PROCMODE_EDIT                '処理モード：編集モード
                .PropIntMeetingNmb = vwMeetingList.ActiveSheet.GetValue(vwMeetingList.ActiveSheet.ActiveRowIndex, 1)
            End With

            '当画面非表示
            Me.Hide()
            '会議記録登録画面表示
            HBKC0401.ShowDialog()
            '当画面表示
            Me.Show()

        End If

    End Sub

    ''' <summary>
    ''' 会議検索一覧：セルダブルクリック時処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>会議記録登録画面へ編集モードで遷移する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub vwMeetingList_CellDoubleClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwMeetingList.CellDoubleClick
        'ヘッダーをクリックした場合は除外する
        If e.ColumnHeader = True OrElse e.RowHeader = True Then
            Exit Sub
        End If

        btnDetails_Click(sender, e)
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
        '選択された会議検索一覧DataTableを返す
        dataHBKC0301.PropDtReturnSub = Nothing

        '戻り値をキャンセルにする
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        'フォームを閉じる
        Me.Close()
    End Sub

    ''' <summary>
    ''' Spreadシートキー操作
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>Spreadシートのチェックのキー操作を行う
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwMeetingList_CellClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        Try

            'メニュー以外から遷移の場合、処理を抜ける
            If dataHBKC0301.PropBlnTranFlg = SELECT_MODE_NOTMENU Then
                Return
            End If

            'ヘッダーをクリックした場合、処理を抜ける
            If e.RowHeader Or e.ColumnHeader Then
                Return
            End If

            'チェックされている行のインデックスを取得する
            Dim selectCells As Integer() = GetCheckRowIndex(vwMeetingList)

            '選択されていたチェックボックスのクリア
            For Each row As Integer In selectCells
                vwMeetingList_Sheet1.SetValue(row, 0, False)
            Next

            'クリックされたセルのチェックボックスをONにする
            vwMeetingList_Sheet1.SetValue(e.Row, 0, True)

        Catch ex As Exception
            Common.CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'エラーメッセージ表示
            MsgBox(HBK_E001 & ex.Message, MsgBoxStyle.Critical, TITLE_ERROR)
        End Try

    End Sub

    ''' <summary>
    ''' チェックされている行のインデックス取得
    ''' </summary>
    ''' <param name="spread">[IN]</param>
    ''' <remarks>チェックされている行のインデックスを取得する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetCheckRowIndex(ByVal spread As FarPoint.Win.Spread.FpSpread) As Integer()

        Dim indexList As New List(Of Integer)

        For i As Integer = 0 To spread.ActiveSheet.RowCount - 1
            If spread.ActiveSheet.GetValue(i, 0) Then
                indexList.Add(i)
            End If
        Next

        Return indexList.ToArray()

    End Function

    ''' <summary>
    ''' [クリア]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>インシデント検索一覧画面で入力した検索条件を初期状態に戻す
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        '検索条件初期化処理メイン呼出
        If logicHBKC0301.ClearSearchFormMain(dataHBKC0301) = False Then
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
            If logicHBKC0301.CreateIDEnterMain(dataHBKC0301) = False Then
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

End Class