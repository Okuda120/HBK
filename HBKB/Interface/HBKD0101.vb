Imports Common
Imports CommonHBK
Imports FarPoint.Win.Spread
Imports HBKZ

''' <summary>
''' 問題検索一覧画面Interfaceクラス
''' </summary>
''' <remarks>問題検索一覧画面の設定を行う
''' <para>作成情報：2012/07/31 y.ikushima
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKD0101
    'インスタンス作成
    Public dataHBKD0101 As New DataHBKD0101
    Private logicHBKD0101 As New LogicHBKD0101
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
    Private Sub HBKD0101_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        '設定を変更する
        With Settings.Instance
            'ウィンドウが最大化、最小化以外は保存
            If Me.WindowState = FormWindowState.Normal Then
                .propHBKD0101_Height = Me.Size.Height
                .propHBKD0101_Width = Me.Size.Width
                .propHBKD0101_Y = Me.Location.Y
                .propHBKD0101_X = Me.Location.X
                .propHBKD0101_WindowState = Me.WindowState
            ElseIf Me.WindowState = FormWindowState.Maximized Then
                '最大化は状態を保存
                .propHBKD0101_WindowState = Me.WindowState
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
    ''' <remarks>画面の初期設定を行う
    ''' <para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKD0101_load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'XMLファイルから設定を読み込む
        Settings.LoadFromXmlFile()
        '最大化とか
        Me.WindowState = Settings.Instance.propHBKD0101_WindowState
        'サイズが0だったら初期状態とみなし通常の表示をする。
        If Settings.Instance.propHBKD0101_Height <> 0 Then
            'Me.StartPosition = FormStartPosition.Manual
            'サイズ変更
            Me.Size = New Point(Settings.Instance.propHBKD0101_Width, Settings.Instance.propHBKD0101_Height)
            Me.Location = New Point(Settings.Instance.propHBKD0101_X, Settings.Instance.propHBKD0101_Y)
        End If

        'データクラスにオブジェクトをセット
        With dataHBKD0101
            .PropTxtPrbNmb = Me.txtPrbNmb                           '問題番号テキストボックス
            .PropLstProcessState = Me.lstProcessState               'ステータスリストボックス
            .PropLstTargetSys = Me.lstTargetSys                     '対象システムリストボックス
            .PropTxtTitle = Me.txtTitle                             'タイトルテキストボックス
            .PropTxtNaiyo = Me.txtNaiyo                             '内容テキストボックス
            .PropTxtTaisyo = Me.txtTaisyo                           '対処テキストボックス
            .PropTxtBiko = Me.txtBiko                               'フリーテキストテキストボックス
            .PropDtpStartDTFrom = Me.dtpStartDTFrom                 '開始日（From)DateTimePickerEx
            .PropDtpStartDTTo = Me.dtpStartDTTo                     '開始日（To)DateTimePickerEx
            .PropDtpKanryoDTFrom = Me.dtpKanryoDTFrom               '完了日（From)DateTimePickerEx
            .PropDtpKanryoDTTo = Me.dtpKanryoDTTo                   '完了日（To)DateTimePickerEx
            .PropDtpRegDTFrom = Me.dtpRegDTFrom                     '登録日（From)DateTimePickerEx
            .PropDtpRegDTTo = Me.dtpRegDTTo                         '登録日（To)DateTimePickerEx
            .PropDtpLastRegDTFrom = Me.dtpLastRegDTFrom             '最終更新日時（From)DateTimePickerEx
            .PropTxtLastRegTimeFrom = Me.txtLastRegTimeFrom         '最終更新日時時分（From)テキストボックス
            .PropDtpLastRegDTTo = Me.dtpLastRegDTTo                 '最終更新日時（To)DateTimePickerEx
            .PropTxtLastRegTimeTo = Me.txtLastRegTimeTo             '最終更新日時時分（To)テキストボックス
            .PropCmbPrbCase = Me.cmbPrbCase                         '発生原因コンボボックス
            .PropTxtCysprNmb = Me.txtCysprNmb                       'CYSPRテキストボックス
            .PropCmbFreeFlg1 = Me.cmbFreeFlg1                       'フリーフラグコンボボックス１
            .PropCmbFreeFlg2 = Me.cmbFreeFlg2                       'フリーフラグコンボボックス２
            .PropCmbFreeFlg3 = Me.cmbFreeFlg3                       'フリーフラグコンボボックス３
            .PropCmbFreeFlg4 = Me.cmbFreeFlg4                       'フリーフラグコンボボックス４
            .PropCmbFreeFlg5 = Me.cmbFreeFlg5                       'フリーフラグコンボボックス５
            .PropRdoDirect = Me.rdoDirect                           '直接ラジオボタン
            .PropRdoPartic = Me.rdoPartic                           '関与ラジオボタン
            .PropCmbTantoGrpCD = Me.cmbTantoGrpCD                   '担当者グループコンボボックス
            .PropTxtTantoID = Me.txtTantoID                         '担当者IDテキストボックス
            .PropTxtTantoNM = Me.txtTantoNM                         '担当者氏名テキストボックス
            .PropBtnTantoSearch = Me.btnTantoSearch                 '担当者検索ボタン
            .PropBtnMeTantoID = Me.btnMeTantoID                     '私担当者ボタン
            .PropDtpWorkSceDTFrom = Me.dtpWorkSceDTFrom             '作業予定日時（From）DateTimePickerEx
            .PropTxtWorkScetimeFrom = Me.txtWorkScetimeFrom         '作業予定日時時分（From）テキストボックス
            .PropDtpWorkSceDTTo = Me.dtpWorkSceDTTo                 '作業予定日時（To）DateTimePickerEx
            .PropTxtWorkScetimeTo = Me.txtWorkScetimeTo             '作業予定日時時分（To）テキストボックス
            .PropCmbSystemNmb = Me.cmbSystemNmb                     '対象システムコンボボックス
            .PropCmbKindCD = Me.cmbKindCD                           '種別コンボボックス
            .PropTxtNum = Me.txtNum                                 '番号テキストボックス
            .PropBtnProcessSearch = Me.btnProcessSearch             'プロセス検索ボタン
            .PropBtnClear = Me.btnClear                             'クリアボタン
            .PropBtnSearch = Me.btnSearch                           '検索ボタン
            .PropLblKensu = Me.lblKensu                             '件数ラベル
            .PropBtnDefaultsort = Me.btnDefaultsort                 'デフォルトソートボタン
            .PropBtnReg = Me.btnReg                                 '新規登録ボタン
            .PropBtnDetails = Me.btnDetails                         '詳細確認ボタン
            .PropBtnOutput = Me.btnOutput                           'Excel出力ボタン
            .PropBtnBack = Me.btnBack                               '戻るボタン
            .PropVwProblemSearch = Me.vwProblemSearch               '問題検索一覧スプレッド
            .PropGrpLoginUser = Me.grpLoginUser                     'ログイン情報グループボックス
        End With

        '問題番号にフォーカスセット
        Me.txtPrbNmb.Select()

        '画面初期表示処理
        If logicHBKD0101.InitFormMain(dataHBKD0101) = False Then
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
    ''' <remarks>検索条件をセットし、問題情報を検索する
    ''' <para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        '検索処理へ遷移
        If logicHBKD0101.SearchMain(dataHBKD0101) = False Then
            If puErrMsg = "" Then
                '件数0件メッセージ表示
                MsgBox(D0101_I001, MsgBoxStyle.Information, TITLE_INFO)
            Else
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [私]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>担当者情報にログイン者のユーザ情報を設定する
    ''' <para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnMeTantoID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMeTantoID.Click

        With dataHBKD0101
            .PropCmbTantoGrpCD.SelectedValue = PropWorkGroupCD    '担当者グループ
            .PropTxtTantoID.Text = PropUserId                '担当者ID
            .PropTxtTantoNM.Text = PropUserName              '担当者氏
        End With

    End Sub


    ''' <summary>
    ''' [クリア]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>検索条件を初期化する
    ''' <para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click

        '画面初期表示処理
        If logicHBKD0101.ClearFormMain(dataHBKD0101) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 問題検索結果ダブルクリック時処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>スプレッドで選択されている行の問題登録画面へ編集モードで遷移する
    ''' <para>作成情報：2012/08/13 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwProblemSearch_CellDoubleClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwProblemSearch.CellDoubleClick
        '変数宣言
        Dim intSelRow As Integer = e.Row    '選択行
        Dim intProNmb As Integer            '問題番号

        '列ヘッダーがクリックされた場合は処理しない
        If e.ColumnHeader = True Or e.RowHeader = True Then
            Exit Sub
        End If

        '問題登録
        Dim frmHBKD0201 As New HBKD0201

        '問題番号を取得
        intProNmb = dataHBKD0101.PropVwProblemSearch.Sheets(0).Cells(intSelRow, logicHBKD0101.COL_PRBNMB).Value

        'パラメータセット
        With frmHBKD0201.dataHBKD0201
            .PropIntOwner = SCR_CALLMOTO_ICHIRAN
            .PropStrProcMode = PROCMODE_EDIT
            .PropIntPrbNmb = intProNmb
        End With

        'ダブルクリックした行の「問題登録」画面へ編集モードで遷移する
        Me.Hide()
        frmHBKD0201.ShowDialog()
        Me.Show()
    End Sub

    ''' <summary>
    ''' [デフォルトソート]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>スプレッドの内容を初期ソートへ並び替える
    ''' <para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnDefaultsort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDefaultsort.Click

        '検索結果が0のときは処理を抜ける
        If dataHBKD0101.PropVwProblemSearch.Sheets(0).Rows.Count = 0 Then
            Exit Sub
        End If

        If logicHBKD0101.SortDefaultMain(dataHBKD0101) = False Then
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
    ''' <remarks>問題登録画面へ新規登録モードで遷移する
    ''' <para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click
        '問題登録
        Dim HBKD0201 As New HBKD0201

        'システム登録画面データクラスに対しプロパティ設定
        With HBKD0201.dataHBKD0201
            .PropStrProcMode = PROCMODE_NEW '処理モード：新規登録
        End With

        Me.Hide()
        HBKD0201.ShowDialog()
        Me.Show()
    End Sub

    ''' <summary>
    ''' [詳細確認]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>スプレッドで選択されている行の問題登録画面へ編集モードで遷移する
    ''' <para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetails.Click
        '変数宣言
        Dim intSelRow As Integer                            '選択行
        Dim intProNmb As Integer            '問題番号
        Dim intSelectedRowFrom As Integer                   '選択開始行番号
        Dim intSelectedRowTo As Integer                     '選択終了行番号

        intSelRow = dataHBKD0101.PropVwProblemSearch.Sheets(0).ActiveRowIndex

        '選択開始行、終了行取得
        intSelectedRowFrom = dataHBKD0101.PropVwProblemSearch.Sheets(0).Models.Selection.AnchorRow
        intSelectedRowTo = dataHBKD0101.PropVwProblemSearch.Sheets(0).Models.Selection.LeadRow

        '[Add] 2012/10/29 s.yamaguchi START
        '行選択を明示的に行う。
        With dataHBKD0101.PropVwProblemSearch
            .ActiveSheet.Models.Selection.AddSelection(.ActiveSheet.ActiveRowIndex, _
                                                       .ActiveSheet.ActiveColumnIndex, _
                                                       1, _
                                                       System.Math.Abs(intSelectedRowTo - intSelectedRowFrom) + 1)
        End With
        '[Add] 2012/10/29 s.yamaguchi END

        'マスター検索結果の選択数が一件以外の時はエラーメッセージ出力
        If dataHBKD0101.PropVwProblemSearch.Sheets(0).SelectionCount <> 1 _
            Or intSelectedRowTo - intSelectedRowFrom <> 0 _
            Or dataHBKD0101.PropVwProblemSearch.Sheets(0).RowCount = 0 Then
            puErrMsg = D0101_E001
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If

        '列ヘッダーがクリックされた場合は処理しない
        If intSelRow < 0 Then
            Exit Sub
        End If

        '問題登録
        Dim frmHBKD0201 As New HBKD0201

        '問題番号を取得
        intProNmb = dataHBKD0101.PropVwProblemSearch.Sheets(0).Cells(intSelRow, logicHBKD0101.COL_PRBNMB).Value

        'パラメータセット
        With frmHBKD0201.dataHBKD0201
            .PropIntOwner = SCR_CALLMOTO_ICHIRAN
            .PropStrProcMode = PROCMODE_EDIT
            .PropIntPrbNmb = intProNmb
        End With

        '行の「問題登録」画面へ編集モードで遷移する
        Me.Hide()
        frmHBKD0201.ShowDialog()
        Me.Show()


    End Sub

    ''' <summary>
    ''' [Excel出力]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>検索された検索条件をもとに、Excelを出力する
    ''' <para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOutput.Click


        'Excel出力インスタンス作成
        Dim logicHBKD0102 As New LogicHBKD0102
        Dim dataHBKD0102 As New DataHBKD0102

        'ファイルダイアログ
        Dim sfd As New SaveFileDialog()

        'ファイル名セット
        sfd.FileName = FILENM_PROBLEM_SEARCH & "_" & DateTime.Now.ToString("yyyyMMddHHmmss") & ".xlsx"

        'デフォルトで表示されるフォルダを指定
        sfd.InitialDirectory = ""

        'デフォルトで表示される[ファイルの種類]を選択する
        sfd.Filter = D0102_FILE_KIND

        'ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
        sfd.RestoreDirectory = True

        'ダイアログを表示する
        If sfd.ShowDialog() = DialogResult.OK Then

            'アイコンを砂時計に変更
            Me.Cursor = Cursors.WaitCursor
            '検索条件保存
            With dataHBKD0102
                'ファイル情報を格納
                .PropStrOutPutFilePath = sfd.FileName                                   '出力先ファイルパス
                .PropStrOutPutFileName = System.IO.Path.GetFileName(sfd.FileName)       '出力ファイル名

                '検索ボタン押下時に保存した検索条件をExcel出力へ渡す

                'ログイン情報--------------------------------------------------------------------------------------------------
                .PropStrLoginUserGrp = dataHBKD0101.PropStrLoginUserGrp                 'ログイン者所属グループ
                .PropStrLoginUserId = dataHBKD0101.PropStrLoginUserId                   'ログイン者ID

                '問題基本情報---------------------------------------------------------------------------------------------------
                .PropStrPrbNmb = dataHBKD0101.PropStrPrbNmb                             '問題番号
                .PropStrProcessState = dataHBKD0101.PropStrProcessState                 'ステータス
                .PropStrTargetSys = dataHBKD0101.PropStrTargetSys                       '対象システム
                .PropStrTitle = dataHBKD0101.PropStrTitle                               'タイトル
                .PropStrNaiyo = dataHBKD0101.PropStrNaiyo                               '内容
                .PropStrTaisyo = dataHBKD0101.PropStrTaisyo                             '対処
                .PropStrBiko = dataHBKD0101.PropStrBiko                                 'フリーテキスト
                .PropStrStartDTFrom = dataHBKD0101.PropStrStartDTFrom                   '開始日（From)
                .PropStrStartDTTo = dataHBKD0101.PropStrStartDTTo                       '開始日（To)
                .PropStrKanryoDTFrom = dataHBKD0101.PropStrKanryoDTFrom                 '完了日（From)
                .PropStrKanryoDTTo = dataHBKD0101.PropStrKanryoDTTo                     '完了日（To)
                .PropStrRegDTFrom = dataHBKD0101.PropStrRegDTFrom                       '登録日（From)
                .PropStrRegDTTo = dataHBKD0101.PropStrRegDTTo                           '登録日（To)
                .PropStrLastRegDTFrom = dataHBKD0101.PropStrLastRegDTFrom               '最終更新日時（From){YYYY/MM/DD HH24:MI}
                .PropStrLastRegDTTo = dataHBKD0101.PropStrLastRegDTTo                   '最終更新日時（To){YYYY/MM/DD HH24:MI}
                '[Add]2014/11/19 e.okamura 問題要望114 Start
                .PropStrLastRegTimeFrom = dataHBKD0101.PropStrLastRegTimeFrom           '最終更新日時（時刻From)
                .PropStrLastRegTimeTo = dataHBKD0101.PropStrLastRegTimeTo               '最終更新日時（時刻To)
                '[Add]2014/11/19 e.okamura 問題要望114 Ens
                .PropStrPrbCase = dataHBKD0101.PropStrPrbCase                           '発生原因
                .PropStrCysprNmb = dataHBKD0101.PropStrCysprNmb                         'CYSPR
                .PropStrFreeFlg1 = dataHBKD0101.PropStrFreeFlg1                         'フリーフラグ１
                .PropStrFreeFlg2 = dataHBKD0101.PropStrFreeFlg2                         'フリーフラグ２
                .PropStrFreeFlg3 = dataHBKD0101.PropStrFreeFlg3                         'フリーフラグ３
                .PropStrFreeFlg4 = dataHBKD0101.PropStrFreeFlg4                         'フリーフラグ４
                .PropStrFreeFlg5 = dataHBKD0101.PropStrFreeFlg5                         'フリーフラグ５
                '担当者情報------------------------------------------------------------------------------------------------------------
                'チェックボックスによってフラグを立てる
                .PropStrTantoRdoCheck = dataHBKD0101.PropStrTantoRdoCheck
                .PropStrTantoGrpCD = dataHBKD0101.PropStrTantoGrpCD                     '担当者グループコンボボックス
                .PropStrTantoID = dataHBKD0101.PropStrTantoID                           '担当者IDテキストボックス
                .PropStrTantoNM = dataHBKD0101.PropStrTantoNM                           '担当者氏名テキストボックス

                '作業情報------------------------------------------------------------------------------------------------------------------
                .PropStrWorkSceDTFrom = dataHBKD0101.PropStrWorkSceDTFrom               '作業予定日時(From){YYYY/MM/DD HH24:MI}
                .PropStrWorkSceDTTo = dataHBKD0101.PropStrWorkSceDTTo                   '作業予定日時(To){YYYY/MM/DD HH24:MI}
                '[Add]2014/11/19 e.okamura 問題要望114 Start
                .PropStrWorkSceTimeFrom = dataHBKD0101.PropStrWorkSceTimeFrom           '作業予定日時（時刻From)
                .PropStrWorkSceTimeTo = dataHBKD0101.PropStrWorkSceTimeTo               '作業予定日時（時刻To)
                '[Add]2014/11/19 e.okamura 問題要望114 End
                .PropStrSystemNmb = dataHBKD0101.PropStrSystemNmb                       '対象システムコンボボックス

                'プロセスリンク情報----------------------------------------------------------------------------------------------------------
                .PropStrKindCD = dataHBKD0101.PropStrProcessLinkNumAry                  'プロセスリンク情報：プロセスリンク番号（カンマ区切り文字列）
            End With

            'Excel出力処理へ遷移
            If logicHBKD0102.CreateOutPutFileMain(dataHBKD0102) = False Then
                'アイコンを元に戻す
                Me.Cursor = Cursors.Default
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Exit Sub
            End If

            'アイコンを元に戻す
            Me.Cursor = Cursors.Default

            '出力完了メッセージ表示
            MsgBox(D0101_I002, MsgBoxStyle.Information, TITLE_INFO)

        Else
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [戻る]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>当画面を閉じて呼び出し元画面へ遷移する
    ''' <para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        '画面を閉じる
        Me.Close()
    End Sub

    ''' <summary>
    ''' 検索条件：担当者情報[検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>[ひびきユーザ検索一覧]画面を立ち上げる
    ''' <para>作成情報：2012/08/10 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnTantoSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTantoSearch.Click
        'ひびきユーザ検索一覧画面を立ち上げる
        Dim frmHBKZ0101 As New HBKZ0101

        'パラメータセット
        With frmHBKZ0101.dataHBKZ0101
            .PropMode = SELECT_MODE_SINGLE
            .PropSplitMode = SPLIT_MODE_OR
            .PropArgs = dataHBKD0101.PropTxtTantoID.Text & dataHBKD0101.PropTxtTantoNM.Text
        End With

        With dataHBKD0101
            'ひびきユーザ検索画面を表示し、検索結果を取得
            .PropDtSubHibikiUser = frmHBKZ0101.ShowDialog()

            '検索結果の存在チェック
            If .PropDtSubHibikiUser IsNot Nothing Then
                '担当者グループは必要か確認
                .PropCmbTantoGrpCD.SelectedValue = .PropDtSubHibikiUser.Rows(0).Item(3) '担当者グループ
                .PropTxtTantoID.Text = .PropDtSubHibikiUser.Rows(0).Item(0) '担当者ID
                .PropTxtTantoNM.Text = .PropDtSubHibikiUser.Rows(0).Item(2) '担当者氏名
            End If
        End With
    End Sub

    ''' <summary>
    ''' 担当者IDEnterキー押下時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>担当者IDをキーに担当者氏名を設定する
    ''' <para>作成情報：2012/08/14 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub txtTantoID_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtTantoID.PreviewKeyDown
        If e.KeyValue = Keys.Enter Then

            If logicHBKD0101.GetIncTantoDataMain(dataHBKD0101) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            '取得できない場合ブランクをセット
            dataHBKD0101.PropTxtTantoNM.Text = ""

            If dataHBKD0101.PropDtResultSub IsNot Nothing Then
                If dataHBKD0101.PropDtResultSub.Rows.Count > 0 Then
                    dataHBKD0101.PropTxtTantoNM.Text = dataHBKD0101.PropDtResultSub.Rows(0).Item(0)           '担当者氏名
                    '複数ある場合はブランクを設定
                    If dataHBKD0101.PropDtResultSub.Rows.Count > 1 Then
                        dataHBKD0101.PropCmbTantoGrpCD.SelectedValue = ""
                    Else
                        dataHBKD0101.PropCmbTantoGrpCD.SelectedValue = dataHBKD0101.PropDtResultSub.Rows(0).Item(2)
                    End If
                End If
            End If

        End If
    End Sub

    ''' <summary>
    ''' プロセスリンク情報[検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>[プロセス検索一覧]画面を立ち上げる
    ''' <para>作成情報：2012/08/13 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnProcessSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcessSearch.Click
        'プロセス検索一覧画面を立ち上げる
        Dim frmHBKZ0401 As New HBKZ0401

        'パラメータセット
        With frmHBKZ0401.dataHBKZ0401
            .PropMode = SELECT_MODE_SINGLE
            .PropSplitMode = SPLIT_MODE_OR
            .PropArgs = ""
        End With

        With dataHBKD0101
            'プロセス検索画面を表示し、検索結果を取得
            .PropDtSubProcess = frmHBKZ0401.ShowDialog()

            '検索結果の存在チェック
            If .PropDtSubProcess IsNot Nothing Then
                '担当者グループは必要か確認
                .PropCmbKindCD.SelectedValue = .PropDtSubProcess.Rows(0).Item(7)   'プロセスリンク情報：種別
                .PropTxtNum.Text = .PropDtSubProcess.Rows(0).Item(1)             'プロセスリンク情報：番号
            End If
        End With
    End Sub

End Class
