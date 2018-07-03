Imports Common
Imports CommonHBK
Imports System.Diagnostics
Imports HBKZ

''' <summary>
''' 変更検索一覧Interfaceクラス
''' </summary>
''' <remarks>変更の検索を行う
''' <para>作成情報：2012/08/21 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKE0101

    'インスタンス生成
    Public dataHBKE0101 As New DataHBKE0101
    Private logicHBKE0101 As New LogicHBKE0101
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    ''' <summary>
    ''' フォーム終了時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>画面情報の保存を行う
    ''' <para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKE0101_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        '設定を変更する
        With Settings.Instance
            'ウィンドウが最大化、最小化以外は保存
            If Me.WindowState = FormWindowState.Normal Then
                .propHBKE0101_Height = Me.Size.Height
                .propHBKE0101_Width = Me.Size.Width
                .propHBKE0101_Y = Me.Location.Y
                .propHBKE0101_X = Me.Location.X
                .propHBKE0101_WindowState = Me.WindowState
            ElseIf Me.WindowState = FormWindowState.Maximized Then
                '最大化は状態を保存
                .propHBKE0101_WindowState = Me.WindowState
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
    ''' <remarks>変更検索一覧画面の初期設定を行う
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKE0101_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'XMLファイルから設定を読み込む
        Settings.LoadFromXmlFile()
        '最大化とか
        Me.WindowState = Settings.Instance.propHBKE0101_WindowState
        'サイズが0だったら初期状態とみなし通常の表示をする。
        If Settings.Instance.propHBKE0101_Height <> 0 Then
            'Me.StartPosition = FormStartPosition.Manual
            'サイズ変更
            Me.Size = New Point(Settings.Instance.propHBKE0101_Width, Settings.Instance.propHBKE0101_Height)
            Me.Location = New Point(Settings.Instance.propHBKE0101_X, Settings.Instance.propHBKE0101_Y)
        End If

        'プロパティセット
        With dataHBKE0101

            'フォームオブジェクト
            '検索条件
            .PropGrpLoginUser = Me.grpLoginUser                     'ログイン：ログイン情報グループボックス
            .PropTxtNum = Me.txtChgNmb                              '基本情報：番号
            .PropLstStatus = Me.lstStatus                           '基本情報：ステータス
            .PropLstTargetSystem = Me.lstTargetSystem               '基本情報：対象システム
            .PropTxtTitle = Me.txtTitle                             '基本情報：タイトル
            .PropTxtNaiyo = Me.txtNaiyo                             '基本情報：内容
            .PropTxtTaiosyo = Me.txtTaisyo                          '基本情報：対処
            .PropTxtCyspr = Me.txtCyspr                             '基本情報：Cyspr
            .PropDtpkaisidtFrom = Me.dtpStartDTFrom                '基本情報：開始日(From)
            .PropDtpkaisidtTo = Me.dtpStartDTTo                    '基本情報：開始日(To)
            .PropDtpkanryoDTFrom = Me.dtpKanryoDTFrom               '基本情報：完了日(From)
            .PropDtpkanryoDTTo = Me.dtpKanryoDTTo                   '基本情報：完了日(To)
            .PropDtpTorokuDTFrom = Me.dtpTorokuDTFrom               '基本情報：登録日(From)
            .PropDtpTorokuDTTo = Me.dtpTorokuDTTo                   '基本情報：登録日(To)
            .PropDtpUpdateDTFrom = Me.dtpUpdateDTFrom               '基本情報：最終更新日時(日付From)
            .PropTxtExUpdateTimeFrom = Me.txtExUpdateTimeFrom       '基本情報：最終更新日時(時刻From)
            .PropDtpUpdateDTTo = Me.dtpUpdateDTTo                   '基本情報：最終更新日時(日付To)
            .PropTxtExUpdateTimeTo = txtExUpdateTimeTo              '基本情報：最終更新日時(時刻To)
            .PropTxtFreeText = Me.txtFreeText                       '基本情報：フリーテキスト
            .PropCmbFreeFlg1 = Me.cmbFreeFlg1                       '基本情報：フリーフラグ1
            .PropCmbFreeFlg2 = Me.cmbFreeFlg2                       '基本情報：フリーフラグ2
            .PropCmbFreeFlg3 = Me.cmbFreeFlg3                       '基本情報：フリーフラグ3
            .PropCmbFreeFlg4 = Me.cmbFreeFlg4                       '基本情報：フリーフラグ4
            .PropCmbFreeFlg5 = Me.cmbFreeFlg5                       '基本情報：フリーフラグ5
            .PropCmbTantoGrp = Me.cmbTantoGrp                       '担当者情報情報：担当者グループ
            .PropTxtTantoID = Me.txtTantoID                         '担当者情報情報：担当者ID
            .PropTxtTantoNM = Me.txtTantoNM                         '担当者情報情報：担当者氏名
            .PropCmbProccesLinkKind = Me.cmbProccesLinkKind         'プロセスリンク情報：種別
            .PropTxtProcessLinkNum = Me.txtProcessLinkNum           'プロセスリンク情報：番号

            '検索結果
            .PropLblResultCounter = Me.lblResultCounter             '検索結果：件数
            .PropVwChangeList = Me.vwIncidentList                   '検索結果：結果一覧表示用スプレッド

            'フッター
            .PropBtnMakeExcel = Me.btnMakeExcel                     'フッター：「Excel出力」ボタン

        End With

        '変更番号にフォーカスセット
        Me.txtChgNmb.Select()

        '変更検索一覧画面初期表示メイン呼出
        If LogicHBKE0101.InitFormMain(dataHBKE0101) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If



    End Sub

    ''' <summary>
    ''' 検索条件：担当者情報[検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>[ひびきユーザ検索一覧]画面を立ち上げる
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnSearchHibikiUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchHibikiUser.Click

        'ひびきユーザ検索一覧画面を立ち上げる
        Dim HBKZ0101 As New HBKZ0101

        'パラメータセット
        With HBKZ0101.dataHBKZ0101
            .PropMode = SELECT_MODE_SINGLE
            .PropSplitMode = SPLIT_MODE_OR
            .PropArgs = dataHBKE0101.PropTxtTantoID.Text & dataHBKE0101.PropTxtTantoNM.Text
        End With

        With DataHBKE0101
            'ひびきユーザ検索画面を表示し、検索結果を取得
            .PropDtSubHibikiUser = HBKZ0101.ShowDialog()

            '検索結果の存在チェック
            If .PropDtSubHibikiUser IsNot Nothing Then
                '担当者グループは必要か確認
                .PropCmbTantoGrp.SelectedValue = .PropDtSubHibikiUser.Rows(0).Item(3)   '担当者グループ
                .PropTxtTantoID.Text = .PropDtSubHibikiUser.Rows(0).Item(0)             '担当者ID
                .PropTxtTantoNM.Text = .PropDtSubHibikiUser.Rows(0).Item(2)             '担当者氏名
            End If
        End With

    End Sub

    ''' <summary>
    ''' 検索条件：担当者情報[私]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>ログインユーザー情報を設定する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSetLoginUserNM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetLoginUserNM.Click
        'パラメータセット
        With DataHBKE0101
            .PropCmbTantoGrp.SelectedValue = PropWorkGroupCD    '担当者情報：グループ
            .PropTxtTantoID.Text = PropUserId                   '担当者情報：ユーザーID
            .PropTxtTantoNM.Text = PropUserName                 '担当者情報：ユーザー名
        End With
    End Sub

    ''' <summary>
    ''' 検索条件：担当者IDEnterキー押下時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>担当者IDをキーに担当者氏名を設定する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub txtTantoID_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtTantoID.PreviewKeyDown

        'ENTERキー押下時のみ処理を行う
        If e.KeyValue = Keys.Enter Then

            If logicHBKE0101.GetIncTantoDataMain(dataHBKE0101) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            '取得できない場合ブランクをセット
            dataHBKE0101.PropTxtTantoNM.Text = ""

            If dataHBKE0101.PropDtResultSub IsNot Nothing Then
                If dataHBKE0101.PropDtResultSub.Rows.Count > 0 Then
                    dataHBKE0101.PropTxtTantoNM.Text = dataHBKE0101.PropDtResultSub.Rows(0).Item(0)           '担当者氏名
                    '複数ある場合はブランクを設定
                    If dataHBKE0101.PropDtResultSub.Rows.Count > 1 Then
                        dataHBKE0101.PropCmbTantoGrp.SelectedValue = ""
                    Else
                        dataHBKE0101.PropCmbTantoGrp.SelectedValue = dataHBKE0101.PropDtResultSub.Rows(0).Item(2)
                    End If
                End If
            End If
        End If

    End Sub

    ''' <summary>
    ''' 検索条件：プロセスリンク情報[検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>[プロセス検索一覧]画面を立ち上げる
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnSearchProcessLink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchProcessLink.Click

        'プロセス検索一覧画面を立ち上げる
        Dim HBKZ0401 As New HBKZ0401

        'パラメータセット
        With HBKZ0401.dataHBKZ0401
            .PropMode = SELECT_MODE_SINGLE
            .PropSplitMode = SPLIT_MODE_OR
            .PropArgs = ""
        End With

        With DataHBKE0101
            'プロセス検索画面を表示し、検索結果を取得
            .PropDtSubProcess = HBKZ0401.ShowDialog()

            '検索結果の存在チェック
            If .PropDtSubProcess IsNot Nothing Then
                '担当者グループは必要か確認
                .PropCmbProccesLinkKind.SelectedValue = .PropDtSubProcess.Rows(0).Item(7)   'プロセスリンク情報：種別
                .PropTxtProcessLinkNum.Text = .PropDtSubProcess.Rows(0).Item(1)             'プロセスリンク情報：番号
            End If
        End With

    End Sub

    ''' <summary>
    ''' [クリア]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>変更検索一覧画面で入力した検索条件を初期状態に戻す
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        '検索条件初期化処理メイン呼出
        If logicHBKE0101.ClearSearchFormMain(dataHBKE0101) = False Then
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
    ''' <remarks>検索条件に従って変更情報を検索する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        'アイコンを砂時計に変更
        Me.Cursor = Cursors.WaitCursor

        '変更検索処理メイン呼出
        If logicHBKE0101.SearchIncidentMain(dataHBKE0101) = False Then
            'アイコンを元に戻す
            Me.Cursor = Cursors.Default
            'エラーメッセージが設定されている場合は表示
            If puErrMsg <> "" Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Else
                'インフォメーションメッセージを表示
                MsgBox(E0101_I001, MsgBoxStyle.Information, TITLE_INFO)
            End If
            '処理終了
            Exit Sub
        End If

        'With dataHBKE0101
        '    '「Excel出力」ボタンを活性状態にする
        '    .PropBtnMakeExcel.Enabled = True
        'End With

        'アイコンを元に戻す
        Me.Cursor = Cursors.Default

    End Sub

    ''' <summary>
    ''' [デフォルトソート]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>検索結果を検索時のソート順に並び替える
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnDefaultSort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDefaultSort.Click
        '検索結果が0のときは処理を抜ける
        If dataHBKE0101.PropVwChangeList.Sheets(0).Rows.Count = 0 Then
            Exit Sub
        End If

        If logicHBKE0101.SortDefaultMain(dataHBKE0101) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' [新規登録]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>［変更登録］へ新規登録モードで呼び出す
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click
        '「変更登録」画面へ新規モードで遷移する
        Dim HBKE0201 As New HBKE0201

        With HBKE0201.dataHBKE0201
            .PropStrProcMode = PROCMODE_NEW
        End With

        Me.Hide()
        HBKE0201.ShowDialog()
        Me.Show()
    End Sub

    ''' <summary>
    ''' [詳細確認]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>［変更登録］画面へ編集モードで遷移する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetails.Click

        '変数宣言
        Dim intSelRow As Integer                            '選択行
        Dim intChgNmb As Integer                            '変更番号
        Dim intSelectedRowFrom As Integer                   '選択開始行番号
        Dim intSelectedRowTo As Integer                     '選択終了行番号

        intSelRow = dataHBKE0101.PropVwChangeList.Sheets(0).ActiveRowIndex

        '選択開始行、終了行取得
        intSelectedRowFrom = dataHBKE0101.PropVwChangeList.Sheets(0).Models.Selection.AnchorRow
        intSelectedRowTo = dataHBKE0101.PropVwChangeList.Sheets(0).Models.Selection.LeadRow

        '[Add] 2012/10/29 s.yamaguchi START
        '行選択を明示的に行う。
        With dataHBKE0101.PropVwChangeList
            .ActiveSheet.Models.Selection.AddSelection(.ActiveSheet.ActiveRowIndex, _
                                                       .ActiveSheet.ActiveColumnIndex, _
                                                       1, _
                                                       System.Math.Abs(intSelectedRowTo - intSelectedRowFrom) + 1)
        End With
        '[Add] 2012/10/29 s.yamaguchi END

        'マスター検索結果の選択数が一件以外の時はエラーメッセージ出力
        If dataHBKE0101.PropVwChangeList.Sheets(0).SelectionCount <> 1 _
            Or intSelectedRowTo - intSelectedRowFrom <> 0 _
            Or dataHBKE0101.PropVwChangeList.Sheets(0).RowCount = 0 Then
            puErrMsg = E0101_E001
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If

        '列ヘッダーがクリックされた場合は処理しない
        If intSelRow < 0 Then
            Exit Sub
        End If

        '変更登録
        Dim HBKE0201 As New HBKE0201

        '変更番号を取得
        intChgNmb = dataHBKE0101.PropVwChangeList.Sheets(0).Cells(intSelRow, logicHBKE0101.COL_SEARCHLIST_CHGNMB).Value

        'パラメータセット
        With HBKE0201.dataHBKE0201
            .PropIntOwner = SCR_CALLMOTO_ICHIRAN
            .PropStrProcMode = PROCMODE_EDIT
            .PropIntChgNmb = intChgNmb
        End With

        '「変更登録」画面へ編集モードで遷移する
        Me.Hide()
        HBKE0201.ShowDialog()
        Me.Show()

    End Sub

    ''' <summary>
    ''' 変更検索結果ダブルクリック時処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>スプレッドで選択されている行の変更登録画面へ編集モードで遷移する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwIncidentList_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwIncidentList.CellDoubleClick
        'ヘッダーをクリックした場合は除外する
        If e.ColumnHeader = True OrElse e.RowHeader = True Then
            Exit Sub
        End If

        btnDetails_Click(sender, e)
    End Sub

    ''' <summary>
    ''' [Excel出力]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>検索結果をExcelファイルに出力
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnMakeExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMakeExcel.Click

        'Excel出力インスタンス作成
        Dim logicHBKE0102 As New LogicHBKE0102
        Dim dataHBKE0102 As New DataHBKE0102

        'ファイルダイアログ
        Dim sfd As New SaveFileDialog()

        'ファイル名セット
        sfd.FileName = FILENM_CHANGE_SEARCH & "_" & DateTime.Now.ToString("yyyyMMddHHmmss") & ".xlsx"

        'デフォルトで表示されるフォルダを指定
        sfd.InitialDirectory = ""

        'デフォルトで表示される[ファイルの種類]を選択する
        sfd.Filter = E0102_FILE_KIND

        'ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
        sfd.RestoreDirectory = True

        'ダイアログを表示する
        If sfd.ShowDialog() = DialogResult.OK Then

            'アイコンを砂時計に変更
            Me.Cursor = Cursors.WaitCursor

            '検索条件保存
            With DataHBKE0102

                'ファイル情報を格納
                .PropStrOutPutFilePath = sfd.FileName                                   '出力先ファイルパス
                .PropStrOutPutFileName = System.IO.Path.GetFileName(sfd.FileName)       '出力ファイル名

                '検索ボタン押下時に保存した検索条件をExcel出力へ渡す

                'ログイン情報--------------------------------------------------------------------------------------------------
                .PropStrLoginUserGrp = dataHBKE0101.PropStrLoginUserGrp                 'ログイン者所属グループ
                .PropStrLoginUserId = dataHBKE0101.PropStrLoginUserId                   'ログイン者ID

                '基本情報------------------------------------------------------------------------------------------------------
                .PropStrChgNmb = dataHBKE0101.PropStrChgNmb                             '変更番号
                .PropStrProcessState = dataHBKE0101.PropStrStatus                       'ステータス
                .PropStrTargetSys = dataHBKE0101.PropStrTargetSystem                    '対象システム
                .PropStrTitle = dataHBKE0101.PropStrTitle                               'タイトル
                .PropStrNaiyo = dataHBKE0101.PropStrNaiyo                               '内容
                .PropStrTaisyo = dataHBKE0101.PropStrTaisyo                             '対処
                .PropStrCysprNmb = dataHBKE0101.PropStrCyspr                            'CYSPR
                .PropStrStartDTFrom = dataHBKE0101.PropStrkaisidtFrom                  '開始日（From)
                .PropStrStartDTTo = dataHBKE0101.PropStrkaisidtTo                      '開始日（To)
                .PropStrKanryoDTFrom = dataHBKE0101.PropStrKanryoDTFrom                 '完了日（From)
                .PropStrKanryoDTTo = dataHBKE0101.PropStrKanryoDTTo                     '完了日（To)
                .PropStrRegDTFrom = dataHBKE0101.PropStrTorokuDTFrom                    '登録日（From)
                .PropStrRegDTTo = dataHBKE0101.PropStrTorokuDTTo                        '登録日（To)
                .PropStrLastRegDTFrom = dataHBKE0101.PropStrUpdateDTFrom                '最終更新日時（From){YYYY/MM/DD HH24:MI}
                .PropStrLastRegDTTo = dataHBKE0101.PropStrUpdateDTTo                    '最終更新日時（To){YYYY/MM/DD HH24:MI}
                '[Add]2014/11/19 e.okamura 問題要望114 Start
                .PropStrLastRegTimeFrom = dataHBKE0101.PropStrExUpdateTimeFrom          '最終更新日時（時刻From)
                .PropStrLastRegTimeTo = dataHBKE0101.PropStrExUpdateTimeTo              '最終更新日時（時刻To)
                '[Add]2014/11/19 e.okamura 問題要望114 End
                .PropStrBiko = dataHBKE0101.PropStrFreeText                             'フリーテキスト
                .PropStrFreeFlg1 = dataHBKE0101.PropStrFreeFlg1                         'フリーフラグ１
                .PropStrFreeFlg2 = dataHBKE0101.PropStrFreeFlg2                         'フリーフラグ２
                .PropStrFreeFlg3 = dataHBKE0101.PropStrFreeFlg3                         'フリーフラグ３
                .PropStrFreeFlg4 = dataHBKE0101.PropStrFreeFlg4                         'フリーフラグ４
                .PropStrFreeFlg5 = dataHBKE0101.PropStrFreeFlg5                         'フリーフラグ５

                '担当者情報----------------------------------------------------------------------------------------------------
                .PropStrTantoGrpCD = dataHBKE0101.PropStrTantoGrp                       '担当者グループコンボボックス
                .PropStrTantoID = dataHBKE0101.PropStrTantoID                           '担当者IDテキストボックス
                .PropStrTantoNM = dataHBKE0101.PropStrTantoNM                           '担当者氏名テキストボックス

            End With

            'Excel出力処理へ遷移
            If logicHBKE0102.CreateOutPutFileMain(dataHBKE0102) = False Then
                'アイコンを元に戻す
                Me.Cursor = Cursors.Default
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Exit Sub
            End If

            'アイコンを元に戻す
            Me.Cursor = Cursors.Default

            '[mod] 2012/09/06 y.ikushima Excel出力対応 START
            '出力完了メッセージ表示
            MsgBox(E0101_I002, MsgBoxStyle.Information, TITLE_INFO)
            '[mod] 2012/09/06 y.ikushima Excel出力対応 END
        Else
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [戻る]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>メニュー遷移する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReturn.Click
        Me.Close()
    End Sub
End Class