Imports Common
Imports CommonHBK
Imports FarPoint.Win.Spread
Imports HBKZ

''' <summary>
''' リリース検索一覧画面Interfaceクラス
''' </summary>
''' <remarks>リリース検索一覧画面の設定を行う
''' <para>作成情報：2012/08/20 y.ikushima
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKF0101
    'インスタンス作成
    Public dataHBKF0101 As New DataHBKF0101
    Private logicHBKF0101 As New LogicHBKF0101
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
    Private Sub HBKF0101_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        '設定を変更する
        With Settings.Instance
            'ウィンドウが最大化、最小化以外は保存
            If Me.WindowState = FormWindowState.Normal Then
                .propHBKF0101_Height = Me.Size.Height
                .propHBKF0101_Width = Me.Size.Width
                .propHBKF0101_Y = Me.Location.Y
                .propHBKF0101_X = Me.Location.X
                .propHBKF0101_WindowState = Me.WindowState
            ElseIf Me.WindowState = FormWindowState.Maximized Then
                '最大化は状態を保存
                .propHBKF0101_WindowState = Me.WindowState
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
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKF0101_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'XMLファイルから設定を読み込む
        Settings.LoadFromXmlFile()
        '最大化とか
        Me.WindowState = Settings.Instance.propHBKF0101_WindowState
        'サイズが0だったら初期状態とみなし通常の表示をする。
        If Settings.Instance.propHBKF0101_Height <> 0 Then
            'Me.StartPosition = FormStartPosition.Manual
            'サイズ変更
            Me.Size = New Point(Settings.Instance.propHBKF0101_Width, Settings.Instance.propHBKF0101_Height)
            Me.Location = New Point(Settings.Instance.propHBKF0101_X, Settings.Instance.propHBKF0101_Y)
        End If


        'データクラスにオブジェクトをセット
        With dataHBKF0101
            .PropTxtRelNmb = Me.txtRelNmb                   'リリース番号
            .PropTxtRelUkeNmb = Me.txtRelUkeNmb             'リリース受付番号テキストボックス
            .PropLstProcessState = Me.lstProcessState       'ステータスリストボックス
            .PropTxtTitle = Me.txtTitle                     'タイトルテキストボックス
            .PropTxtGaiyo = Me.txtGaiyo                     '概要テキストボックス
            .PropCmbUsrSyutiKbn = Me.cmbUsrSyutiKbn         'ユーザ周知有無コンボボックス
            .PropTxtBiko = Me.txtBiko                       'フリーテキストテキストボックス
            .PropDtpIraiDTFrom = Me.dtpIraiDTFrom           '依頼日（From)
            .PropDtpIraiDTTo = Me.dtpIraiDTTo               '依頼日（To)
            .PropDtpRelSceDTFrom = Me.dtpRelSceDTFrom       'リリース予定日（From)
            .PropDtpRelSceDTto = Me.dtpRelSceDTTo           'リリース予定日（To)
            .PropDtpRelStDTFrom = Me.dtpRelStDTFrom         'リリース着手日時（From)
            .PropDtpRelStDTTo = Me.dtpRelStDTTo             'リリース着手日時（From)
            .PropCmbFreeFlg1 = Me.cmbFreeFlg1               'フリーフラグコンボボックス１
            .PropCmbFreeFlg2 = Me.cmbFreeFlg2               'フリーフラグコンボボックス２
            .PropCmbFreeFlg3 = Me.cmbFreeFlg3               'フリーフラグコンボボックス３
            .PropCmbFreeFlg4 = Me.cmbFreeFlg4               'フリーフラグコンボボックス４
            .PropCmbFreeFlg5 = Me.cmbFreeFlg5               'フリーフラグコンボボックス５
            .PropCmbTantoGrpCD = Me.cmbTantoGrpCD           '担当者グループコンボボックス
            .PropTxtTantoID = Me.txtTantoID                 '担当者IDテキストボックス
            .PropTxtTantoNM = Me.txtTantoNM                 '担当者氏名テキストボックス
            .PropBtnTantoSearch = Me.btnTantoSearch         '担当者検索ボタン
            .PropBtnMeTantoID = Me.btnMeTantoID             '私担当者ボタン
            .PropCmbKindCD = Me.cmbKindCD                   '種別コンボボックス
            .PropTxtNum = Me.txtNum                         '番号テキストボックス
            .PropBtnProcessSearch = Me.btnProcessSearch     'プロセス検索ボタン
            .PropBtnClear = Me.btnClear                     'クリアボタン
            .PropBtnSearch = Me.btnSearch                   '検索ボタン
            .PropLblKensu = Me.lblKensu                     '件数ラベル
            .PropBtnDefaultsort = Me.btnDefaultsort         'デフォルトソートボタン
            .PropBtnReg = Me.btnReg                         '新規登録ボタン
            .PropBtnDetails = Me.btnDetails                 '詳細確認ボタン
            .PropBtnOutput = Me.btnOutput                   'Excel出力ボタン
            .PropBtnBack = Me.btnBack                       '戻るボタン
            .PropVwReleaseSearch = Me.vwReleaseSearch       'リリース検索一覧スプレッド
            .PropGrpLoginUser = Me.grpLoginUser             'ログイン情報グループボックス

            'リリース番号にフォーカスセット
            Me.txtRelNmb.Select()

            '画面初期表示処理
            If logicHBKF0101.InitFormMain(dataHBKF0101) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

        End With


    End Sub

    ''' <summary>
    ''' [検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>検索条件をセットし、リリース情報を検索する
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        '検索処理へ遷移
        If logicHBKF0101.SearchMain(dataHBKF0101) = False Then

            'エラーメッセージが設定されている場合は表示
            If puErrMsg <> "" Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Else
                'インフォメーションメッセージを表示
                MsgBox(F0101_I001, MsgBoxStyle.Information, TITLE_INFO)
            End If


            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [クリア]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>検索条件を初期化する
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        '画面初期表示処理
        If logicHBKF0101.ClearFormMain(dataHBKF0101) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' 担当者情報[検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>[ひびきユーザ検索一覧]画面を立ち上げる
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnTantoSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTantoSearch.Click
        'ひびきユーザ検索一覧画面を立ち上げる
        Dim frmHBKZ0101 As New HBKZ0101

        'パラメータセット
        With frmHBKZ0101.dataHBKZ0101
            .PropMode = SELECT_MODE_SINGLE
            .PropSplitMode = SPLIT_MODE_OR
            .PropArgs = dataHBKF0101.PropTxtTantoID.Text & dataHBKF0101.PropTxtTantoNM.Text
        End With

        With dataHBKF0101
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
    ''' [私]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>担当者情報にログイン者のユーザ情報を設定する
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnMeTantoID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMeTantoID.Click
        With dataHBKF0101
            .PropCmbTantoGrpCD.SelectedValue = PropWorkGroupCD    '担当者グループ
            .PropTxtTantoID.Text = PropUserId                '担当者ID
            .PropTxtTantoNM.Text = PropUserName              '担当者氏
        End With
    End Sub

    ''' <summary>
    ''' プロセスリンク情報[検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>[プロセス検索一覧]画面を立ち上げる
    ''' <para>作成情報：2012/08/20 y.ikushima
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

        With dataHBKF0101
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

    ''' <summary>
    ''' [デフォルトソート]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>スプレッドの内容を初期ソートへ並び替える
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnDefaultsort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDefaultsort.Click
        '検索結果が0のときは処理を抜ける
        If dataHBKF0101.PropVwReleaseSearch.Sheets(0).Rows.Count = 0 Then
            Exit Sub
        End If
        'デフォルトソートメイン処理
        If logicHBKF0101.SortDefaultMain(dataHBKF0101) = False Then
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
    ''' <remarks>リリース登録画面へ新規登録モードで遷移する
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click
        'リリース登録
        Dim HBKF0201 As New HBKF0201

        'リリース登録画面データクラスに対しプロパティ設定
        With HBKF0201.dataHBKF0201
            .PropStrProcMode = PROCMODE_NEW '処理モード：新規登録
        End With

        Me.Hide()
        HBKF0201.ShowDialog()
        Me.Show()
    End Sub

    ''' <summary>
    ''' [詳細確認]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>スプレッドで選択されている行のリリース登録画面へ編集モードで遷移する
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetails.Click
        '変数宣言
        Dim intSelRow As Integer                            '選択行
        Dim intRelNmb As Integer                            'リリース番号
        Dim intSelectedRowFrom As Integer                   '選択開始行番号
        Dim intSelectedRowTo As Integer                     '選択終了行番号

        intSelRow = dataHBKF0101.PropVwReleaseSearch.Sheets(0).ActiveRowIndex

        '選択開始行、終了行取得
        intSelectedRowFrom = dataHBKF0101.PropVwReleaseSearch.Sheets(0).Models.Selection.AnchorRow
        intSelectedRowTo = dataHBKF0101.PropVwReleaseSearch.Sheets(0).Models.Selection.LeadRow

        '[Add] 2012/10/29 s.yamaguchi START
        '行選択を明示的に行う。
        With dataHBKF0101.PropVwReleaseSearch
            .ActiveSheet.Models.Selection.AddSelection(.ActiveSheet.ActiveRowIndex, _
                                                       .ActiveSheet.ActiveColumnIndex, _
                                                       1, _
                                                       System.Math.Abs(intSelectedRowTo - intSelectedRowFrom) + 1)
        End With
        '[Add] 2012/10/29 s.yamaguchi END

        'マスター検索結果の選択数が一件以外の時はエラーメッセージ出力
        If dataHBKF0101.PropVwReleaseSearch.Sheets(0).SelectionCount <> 1 _
            Or intSelectedRowTo - intSelectedRowFrom <> 0 _
            Or dataHBKF0101.PropVwReleaseSearch.Sheets(0).RowCount = 0 Then
            puErrMsg = F0101_E001
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If

        '列ヘッダーがクリックされた場合は処理しない
        If intSelRow < 0 Then
            Exit Sub
        End If

        '問題登録
        Dim frmHBKF0201 As New HBKF0201

        'リリース番号を取得
        intRelNmb = dataHBKF0101.PropVwReleaseSearch.Sheets(0).Cells(intSelRow, logicHBKF0101.COL_RELMB).Value

        'パラメータセット
        With frmHBKF0201.dataHBKF0201
            .PropIntOwner = SCR_CALLMOTO_ICHIRAN
            .PropStrProcMode = PROCMODE_EDIT
            .PropIntRelNmb = intRelNmb
        End With

        'ダブルクリックした行の「問題登録」画面へ編集モードで遷移する
        Me.Hide()
        frmHBKF0201.ShowDialog()
        Me.Show()
    End Sub

    ''' <summary>
    ''' [Excel出力]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>検索された検索条件をもとに、Excelを出力する
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOutput.Click

        'Excel出力インスタンス作成
        Dim logicHBKF0102 As New LogicHBKF0102
        Dim dataHBKF0102 As New DataHBKF0102

        'ファイルダイアログ
        Dim sfd As New SaveFileDialog()

        'ファイル名セット
        sfd.FileName = FILENM_RELEASE_SEARCH & "_" & DateTime.Now.ToString("yyyyMMddHHmmss") & ".xlsx"

        'デフォルトで表示されるフォルダを指定
        sfd.InitialDirectory = ""

        'デフォルトで表示される[ファイルの種類]を選択する
        sfd.Filter = F0102_FILE_KIND

        'ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
        sfd.RestoreDirectory = True

        'ダイアログを表示する
        If sfd.ShowDialog() = DialogResult.OK Then

            'アイコンを砂時計に変更
            Me.Cursor = Cursors.WaitCursor

            With dataHBKF0102
                'ファイル情報を格納
                .PropStrOutPutFilePath = sfd.FileName                                   '出力先ファイルパス
                .PropStrOutPutFileName = System.IO.Path.GetFileName(sfd.FileName)       '出力ファイル名
                '検索条件保存
                'ログイン情報--------------------------------------------------------------------------------------------------
                .PropStrLoginUserGrp = dataHBKF0101.PropStrLoginUserGrp                 'ログイン者所属グループ
                .PropStrLoginUserId = dataHBKF0101.PropStrLoginUserId                   'ログイン者ID
                'リリース基本情報---------------------------------------------------------------------------------------------
                .PropStrRelNmb = dataHBKF0101.PropStrRelNmb                             'リリース番号
                .PropStrRelUkeNmb = dataHBKF0101.PropStrRelUkeNmb                       'リリース受付番号
                .PropStrProcessState = dataHBKF0101.PropStrProcessState                 'ステータス
                .PropStrTitle = dataHBKF0101.PropStrTitle                               'タイトル
                .PropStrGaiyo = dataHBKF0101.PropStrGaiyo                               '概要
                .PropStrUsrSyutiKbn = dataHBKF0101.PropStrUsrSyutiKbn                   'ユーザ周知必要有無
                .PropStrIraiDTFrom = dataHBKF0101.PropStrIraiDTFrom                     '依頼日(FROM)
                .PropStrIraiDTTo = dataHBKF0101.PropStrIraiDTTo                         '依頼日(TO)
                .PropStrRelSceDTFrom = dataHBKF0101.PropStrRelSceDTFrom                 'リリース予定日(FROM)
                .PropStrRelSceDTTo = dataHBKF0101.PropStrRelSceDTTo                     'リリース予定日(TO)
                .PropStrRelStDTFrom = dataHBKF0101.PropStrRelStDTFrom                   'リリース着手日(FROM)
                .PropStrRelStDTTo = dataHBKF0101.PropStrRelStDTTo                       'リリース着手日(TO)
                .PropStrBiko = dataHBKF0101.PropStrBiko                                 'フリーテキスト
                .PropStrFreeFlg1 = dataHBKF0101.PropStrFreeFlg1                         'フリーフラグ1
                .PropStrFreeFlg2 = dataHBKF0101.PropStrFreeFlg2                         'フリーフラグ2
                .PropStrFreeFlg3 = dataHBKF0101.PropStrFreeFlg3                         'フリーフラグ3
                .PropStrFreeFlg4 = dataHBKF0101.PropStrFreeFlg4                         'フリーフラグ4
                .PropStrFreeFlg5 = dataHBKF0101.PropStrFreeFlg5                         'フリーフラグ5
                '担当者情報-------------------------------------------------------------------------------------------------
                .PropStrTantoGrpCD = dataHBKF0101.PropStrTantoGrpCD                     '担当者グループ
                .PropStrTantoID = dataHBKF0101.PropStrTantoID                           '担当者ID
                .PropStrTantoNM = dataHBKF0101.PropStrTantoNM                           '担当者名
                'プロセスリンク情報------------------------------------------------------------------------------------------------
                .PropStrKindCD = dataHBKF0101.PropStrProcessLinkNumAry                  'プロセスリンク情報：プロセスリンク番号（カンマ区切り文字列）
                .PropStrNum = dataHBKF0102.PropStrNum                                   '番号

            End With
            'Excel出力処理へ遷移
            If logicHBKF0102.CreateOutPutFileMain(dataHBKF0102) = False Then
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
            MsgBox(F0101_I002, MsgBoxStyle.Information, TITLE_INFO)
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
    ''' <remarks>当画面を閉じて呼び出し元画面へ遷移する
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        '画面を閉じる
        Me.Close()
    End Sub


    ''' <summary>
    ''' 担当者IDEnterキー押下時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>担当者IDをキーに担当者氏名を設定する
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub txtTantoID_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtTantoID.PreviewKeyDown
        If e.KeyValue = Keys.Enter Then

            If logicHBKF0101.GetIncTantoDataMain(dataHBKF0101) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            '取得できない場合ブランクをセット
            dataHBKF0101.PropTxtTantoNM.Text = ""


            If dataHBKF0101.PropDtResultSub IsNot Nothing Then
                If dataHBKF0101.PropDtResultSub.Rows.Count > 0 Then
                    dataHBKF0101.PropTxtTantoNM.Text = dataHBKF0101.PropDtResultSub.Rows(0).Item(0)           '担当者氏名
                    '複数ある場合はブランクを設定
                    If dataHBKF0101.PropDtResultSub.Rows.Count > 1 Then
                        dataHBKF0101.PropCmbTantoGrpCD.SelectedValue = ""
                    Else
                        dataHBKF0101.PropCmbTantoGrpCD.SelectedValue = dataHBKF0101.PropDtResultSub.Rows(0).Item(2)
                    End If
                End If
            End If

        End If
    End Sub


    ''' <summary>
    ''' リリース検索結果ダブルクリック時処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>スプレッドで選択されている行のリリース登録画面へ編集モードで遷移する
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwReleaseSearchvwProblemSearch_CellDoubleClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwReleaseSearch.CellDoubleClick
        '変数宣言
        Dim intSelRow As Integer = e.Row    '選択行
        Dim intRelNmb As Integer            'リリース番号

        '列ヘッダーがクリックされた場合は処理しない
        If e.ColumnHeader = True Or e.RowHeader = True Then
            Exit Sub
        End If

        'リリース登録
        Dim frmHBKF0201 As New HBKF0201

        'リリース番号を取得
        intRelNmb = dataHBKF0101.PropVwReleaseSearch.Sheets(0).Cells(intSelRow, logicHBKF0101.COL_RELMB).Value

        'パラメータセット
        With frmHBKF0201.dataHBKF0201
            .PropIntOwner = SCR_CALLMOTO_ICHIRAN
            .PropStrProcMode = PROCMODE_EDIT
            .PropIntRelNmb = intRelNmb
        End With

        'ダブルクリックした行の「リリース登録」画面へ編集モードで遷移する
        Me.Hide()
        frmHBKF0201.ShowDialog()
        Me.Show()
    End Sub

End Class