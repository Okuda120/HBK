Imports Common
Imports CommonHBK
'【ADD】2012/07/28 t.fukuo　サポセン機器情報タブ機能作成：START
Imports HBKB
'【ADD】2012/07/28 t.fukuo　サポセン機器情報タブ機能作成：END
Imports HBKZ
Imports FarPoint.Win.Spread

''' <summary>
''' インシデント登録画面Interfaceクラス
''' </summary>
''' <remarks>インシデント登録画面の設定を行う
''' <para>作成情報：2012/07/13 fukuo
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKC0201

    'インスタンス作成
    Public dataHBKC0201 As New DataHBKC0201
    Private logicHBKC0201 As New LogicHBKC0201
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：START
    Private intSelectedTabIdx As Integer = logicHBKC0201.TAB_KHN                    '前回選択タブ（初期値：基本情報タブ）
    Private intSelectedCellIdx As Integer                                           '前回選択セル
    '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：END

    Dim WithEvents datamodel As FarPoint.Win.Spread.Model.DefaultSheetDataModel     '作業履歴データモデル（更新判定用）
    Dim bln_chg_flg As Boolean                                                      '変更フラグ
    Dim add_row_cnt As Integer                                                      '新規追加をカウント、行削除はマイナスする
    Dim bln_update_flg As Boolean                                                   '更新フラグ
    Dim init_row_cnt As Integer                                                     '初期表示カウント
    Dim AryNotfrmCtlList As New ArrayList                                           '非活性対象ボタンリスト

    ''' <summary>
    ''' フォーム終了時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>画面情報の保存を行う
    ''' <para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKC0201_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        '設定を変更する
        With Settings.Instance
            'ウィンドウが最大化、最小化以外は保存
            If Me.WindowState = FormWindowState.Normal Then
                .propHBKC0201_Height = Me.Size.Height
                .propHBKC0201_Width = Me.Size.Width
                .propHBKC0201_Y = Me.Location.Y
                .propHBKC0201_X = Me.Location.X
                .propHBKC0201_WindowState = Me.WindowState
            ElseIf Me.WindowState = FormWindowState.Maximized Then
                '最大化は状態を保存
                .propHBKC0201_WindowState = Me.WindowState
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
    ''' <remarks>モードに応じて画面の初期設定を行う
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : 2017/08/17 e.okuda</p>
    ''' </para></remarks>
    Private Sub HBKC0201_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'インシデント画面を表示するにあたって以下の設定を呼び元側で行うこと。
        'dataHBKC0201.PropIntOwner              呼び元画面(1:変更検索一覧,0:それ以外)
        'dataHBKC0201.PropStrProcMode           表示モード

        'ボタンコントロール非活性対象リスト作成
        AryNotfrmCtlList.Clear()
        AryNotfrmCtlList.Add(btnOpenFile.Name)
        AryNotfrmCtlList.Add(btnSaveFile.Name)

        'XMLファイルから設定を読み込む
        Settings.LoadFromXmlFile()
        '最大化とか
        Me.WindowState = Settings.Instance.propHBKC0201_WindowState
        'サイズが0だったら初期状態とみなし通常の表示をする。
        If Settings.Instance.propHBKC0201_Height <> 0 Then
            'サイズ変更
            Me.Size = New Point(Settings.Instance.propHBKC0201_Width, Settings.Instance.propHBKC0201_Height)
            Me.Location = New Point(Settings.Instance.propHBKC0201_X, Settings.Instance.propHBKC0201_Y)
        End If
        '拡大処理
        kakudai(Settings.Instance.propHBKC0201_Expantion_wkRireki)

        'データクラスの初期設定を行う
        With dataHBKC0201
            .PropLblkanryoMsg = Me.LblkanryoMsg                     '完了メッセージ

            .PropGrpLoginUser = Me.grpLoginUser                     'ログイン：ログイン情報グループボックス
            .PropGrpIncCD = Me.grpIncCD                             'ヘッダー：グループ
            .PropTxtIncCD = Me.txtIncCD                             'ヘッダー：ユーザー
            .PropLblRegInfo = Me.lblRegInfo                         'ヘッダー：登録情報
            .PropLblUpdateInfo = Me.lblUpdateInfo                   'ヘッダー：最終更新情報
            .PropLblRegInfo_out = Me.lblRegInfo_out                 'ヘッダー：登録情報_出力
            .PropLblUpdateInfo_out = Me.lblUpdateInfo_out           'ヘッダー：最終更新情報_出力

            .PropTbInput = Me.tbInput                               'タブ
            .PropCmbUkeKbn = Me.cmbUkeKbnCD                         '基本情報：受付手段
            .PropDtpHasseiDT = Me.dtpHasseiDT                       '基本情報：発生日カレンダー
            .PropTxtHasseiDT_HM = Me.txtHasseiDT_HM                 '基本情報：発生日時
            .PropBtnHasseiDT_HM = Me.btnHasseiDT_HM                 '基本情報：発生日時ボタン
            .PropCmbIncKbnCD = Me.cmbIncKbnCD                       '基本情報：インシデント種別
            .PropCmbprocessStateCD = Me.cmbProcessStateCD           '基本情報：ステータス
            .PropCmbDomainCD = Me.cmbDomainCD                       '基本情報：ドメイン
            .PropCmbSystemNmb = Me.cmbSystemNmb                     '基本情報：対象システム
            '[ADD] 2012/10/24 s.yamaguchi START
            .PropBtnSearchTaisyouSystem = Me.btnSearchTaisyouSystem '基本情報：対象システム検索ボタン
            '[ADD] 2012/10/24 s.yamaguchi END
            .PropBtnKnowHow = Me.btnKnowHow                         '基本情報：ノウハウ
            .PropTxtOutSideToolNmb = Me.txtOutSideToolNmb           '基本情報：外部ツール
            .PropChkShijisyoFlg = Me.chkShijisyoFlg                 '基本情報：指示書フラグ
            .PropTxtTitle = Me.txtTitle                             '基本情報：タイトル
            .PropTbNaiyo = Me.tbNaiyo                               '基本情報：受付内容タブ
            .PropTxtUkeNaiyo = Me.txtUkeNaiyo                       '基本情報：受付内容
            .PropTxtPriority = Me.txtPriority                       '基本情報：重要度
            .PropTxtErrlevel = Me.txtErrlevel                       '基本情報：エラーレベル
            .PropTxtEventID = Me.txtEventID                         '基本情報：イベントID
            .PropTxtSource = Me.txtSource                           '基本情報：ソース
            .PropTxtOPCEventID = Me.txtOPCEventID                   '基本情報：OPCイベントID
            .PropTxtEventClass = Me.txtEventClass                   '基本情報：イベントクラス
            .PropTxtTaioKekka = Me.txtTaioKekka                     '基本情報：対応結果
            .PropDtpKaitoDT = Me.dtpKaitoDT                         '基本情報：回答日カレンダー
            .PropTxtKaitoDT_HM = Me.txtKaitoDT_HM                   '基本情報：回答日時
            .PropBtnKaitoDT_HM = Me.btnKaito_HM                     '基本情報：回答日時ボタン
            .PropDtpKanryoDT = Me.dtpKanryoDT                       '基本情報：完了日
            .PropTxtKanryoDT_HM = Me.txtKanryoDT_HM                 '基本情報：完了日時
            .PropBtnKanryoDT_HM = Me.btnKanryo_HM                   '基本情報：完了日時ボタン

            .PropTxtPartnerID = Me.txtPartnerID                     '基本情報：相手ID
            .PropTxtPartnerNM = Me.txtPartnerNM                     '基本情報：相手氏名
            .PropBtnPartnerSearch = Me.btnPartnerSearch             '基本情報：検索
            .PropTxtPartnerKana = Me.txtPartnerKana                 '基本情報：相手シメイ
            .PropTxtPartnerCompany = Me.txtPartnerCompany           '基本情報：相手会社
            .PropTxtPartnerKyokuNM = Me.txtPartnerKyokuNM           '基本情報：相手局
            .PropTxtPartnerBusyoNM = Me.txtPartnerBusyoNM           '基本情報：相手部署
            .PropTxtPartnerTel = Me.txtPartnerTel                   '基本情報：相手電話番号
            .PropTxtPartnerMailAdd = Me.txtPartnerMailAdd           '基本情報：相手メールアドレス
            .PropTxtPartnerContact = Me.txtPartnerContact           '基本情報：相手連絡先
            .PropTxtPartnerBase = Me.txtPartnerBase                 '基本情報：相手拠点
            .PropTxtPartnerRoom = Me.txtPartnerRoom                 '基本情報：相手番組／部屋
            .PropTxtKengen = Me.txtKengen                           '基本情報：権限
            .PropTxtRentalKiki = Me.txtRentalKiki                   '基本情報：借用物
            .PropBtnRentalKiki = Me.btnRentalKiki                   '基本情報：取得ボタン

            .PropCmbTantoGrpCD = Me.cmbTantoGrpCD                   '基本情報：グループ
            .PropTxtIncTantoCD = Me.txtIncTantoCD                   '基本情報：担当ID
            .PropBtnIncTantoMY = Me.btnMyInctantoID                 '基本情報：私ボタン
            .PropTxtIncTantoNM = Me.txtIncTantoNM                   '基本情報：担当氏名
            .PropBtnIncTantoSearch = Me.btnIncTantoSearch           '基本情報：検索ボタン
            .PropVwkikiInfo = Me.vwKikiInfo                         '機器情報：スプレッド
            .PropBtnAddRow_kiki = Me.btnAddRow_Kiki                 '機器情報：＋
            .PropBtnRemoveRow_kiki = Me.btnRemoveRow_Kiki           '機器情報：ー
            .PropBtnWeb = Me.btnWeb                                 '機器情報：WEB
            .PropBtnSSCM = Me.btnSCCM                               '機器情報：SCCM
            .PropBtnEnkaku = Me.btnEnkaku                           '機器情報：遠隔
            .PropVwIncRireki = Me.vwIncRireki                       '作業履歴：スプレッド
            .PropBtnAddRow_rireki = Me.btnAddRow_Rireki             '作業履歴：＋
            .PropBtnRemoveRow_rireki = Me.btnRemoveRow_Rireki       '作業履歴：ー
            .PropBtnkakudai = Me.btnKakudai                         '作業履歴：拡大
            .PropBtnRefresh = Me.btnRefresh                         '作業履歴：リフレッシュ

            '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：START
            .PropTxtPartnerID_Sap = Me.txtPartnerID_Sap             'サポセン機器情報：相手IDテキストボックス
            .PropTxtPartnerNM_Sap = Me.txtPartnerNM_Sap             'サポセン機器情報：相手氏名テキストボックス
            .PropTxtPartnerKana_Sap = Me.txtPartnerKana_Sap         'サポセン機器情報：相手シメイテキストボックス
            .PropTxtPartnerCompany_Sap = Me.txtPartnerCompany_Sap   'サポセン機器情報：相手会社テキストボックス
            .PropTxtPartnerKyokuNM_Sap = Me.txtPartnerKyokuNM_Sap   'サポセン機器情報：相手局テキストボックス
            .PropTxtPartnerBusyoNM_Sap = Me.txtPartnerBusyoNM_Sap   'サポセン機器情報：相手部署テキストボックス
            .PropTxtPartnerTel_Sap = Me.txtPartnerTel_Sap           'サポセン機器情報：相手電話番号テキストボックス
            .PropTxtPartnerMailAdd_Sap = Me.txtPartnerMailAdd_Sap   'サポセン機器情報：相手メールアドレステキストボックス
            .PropTxtPartnerContact_Sap = Me.txtPartnerContact_Sap   'サポセン機器情報：相手連絡先テキストボックス
            .PropTxtPartnerBase_Sap = Me.txtPartnerBase_Sap         'サポセン機器情報：相手拠点テキストボックス
            .PropTxtPartnerRoom_Sap = Me.txtPartnerRoom_Sap         'サポセン機器情報：相手番組／部屋テキストボックス
            .PropCmbWork = Me.cmbWork                               'サポセン機器情報：作業コンボボックス
            .PropBtnAddRow_SapMainte = Me.btnAddRow_SapMainte       'サポセン機器情報：作業追加ボタン
            .PropVwSapMainte = Me.vwSapMainte                       'サポセン機器情報：サポセン機器メンテナンススプレッド
            .PropBtnExchange = Me.btnExchange                       'サポセン機器情報：選択行を交換／解除ボタン
            .PropBtnSetPair = Me.btnSetPair                         'サポセン機器情報：選択行をセットにするボタン
            .PropBtnAddPair = Me.btnAddPair                         'サポセン機器情報：選択行を既存のセットまたは機器とセットにするボタン
            .PropBtnCepalatePair = Me.btnCepalatePair               'サポセン機器情報：選択行のセットをバラすボタン
            .PropBtnOutput_Kashidashi = Me.btnOutput_Kashidashi     'サポセン機器情報：貸出誓約書出力ボタン
            .PropBtnOutput_UpLimitDate = Me.btnOutput_UpLimitDate   'サポセン機器情報：期限更新誓約書出力ボタン
            .PropBtnOutput_Azukari = Me.btnOutput_Azukari           'サポセン機器情報：一時預託書出力ボタン
            .PropBtnOutput_Henkyaku = Me.btnOutput_Henkyaku         'サポセン機器情報：返却確認書出力ボタン
            .PropBtnOutput_Check = Me.btnOutput_Check               'サポセン機器情報：チェックシート出力ボタン
            .PropMcdSapMainte = Me.mcdSapMainte                     'サポセン機器情報：サポセン機器メンテナンスカレンダー
            .PropMcdSapMainte.Hide()                                'カレンダー初期隠す
            '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：END

            .PropVwMeeting = Me.vwMeeting                           '会議情報：スプレッド
            .PropBtnAddRow_meeting = Me.btnAddRow_meeting           '会議情報：＋
            .PropBtnRemoveRow_meeting = Me.btnRemoveRow_meeting     '会議情報：－

            .PropTxtBIko1 = Me.txtBIko1                             'フリー入力情報：テキスト１テキストボックス      
            .PropTxtBIko2 = Me.txtBIko2                             'フリー入力情報：テキスト２テキストボックス      
            .PropTxtBIko3 = Me.txtBIko3                             'フリー入力情報：テキスト３テキストボックス      
            .PropTxtBIko4 = Me.txtBIko4                             'フリー入力情報：テキスト４テキストボックス      
            .PropTxtBIko5 = Me.txtBIko5                             'フリー入力情報：テキスト５テキストボックス      
            .PropChkFreeFlg1 = Me.chkFreeFlg1                       'フリー入力情報：フリーフラグ１チェックボックス    
            .PropChkFreeFlg2 = Me.chkFreeFlg2                       'フリー入力情報：フリーフラグ２チェックボックス    
            .PropChkFreeFlg3 = Me.chkFreeFlg3                       'フリー入力情報：フリーフラグ３チェックボックス    
            .PropChkFreeFlg4 = Me.chkFreeFlg4                       'フリー入力情報：フリーフラグ４チェックボックス    
            .PropChkFreeFlg5 = Me.chkFreeFlg5                       'フリー入力情報：フリーフラグ５チェックボックス    

            .PropVwRelation = Me.vwRelationInfo                     '関係情報：関係者情報スプレッド
            .PropBtnAddRow_Grp = Me.btnAddRow_relaG                 '関係情報：グループ行追加ボタン
            .PropBtnAddRow_Usr = Me.btnAddRow_RelaU                 '関係情報：ユーザー行追加ボタン
            .PropBtnRemoveRow_Relation = Me.btnRemoveRow_Rela       '関係情報：関係者情報行削除ボタン

            .PropTxtGrpHistory = Me.txtGrpHistory                   '担当履歴情報：担当G
            .PropTxtTantoHistory = Me.txtTantoHistory               '担当履歴情報：担当U

            .PropVwprocessLinkInfo = Me.vwProcessLinkInfo           'プロセスリンク情報：プロセスリンクスプレッド
            .PropBtnAddRow_plink = Me.btnAddRow_plink               'プロセスリンク情報：＋
            .PropBtnRemoveRow_plink = Me.btnRemoveRow_Plink         'プロセスリンク情報：ー

            .PropVwFileInfo = Me.vwFileInfo                         '関連ファイル情報：関連ファイルスプレッド
            .PropBtnAddRow_File = Me.btnAddRow_File                 '関連ファイル情報：＋
            .PropBtnRemoveRow_File = Me.btnRemoveRow_File           '関連ファイル情報：ー
            .PropBtnOpenFile = Me.btnOpenFile                       '関連ファイル情報：開
            .PropBtnSaveFile = Me.btnSaveFile                       '関連ファイル情報：ダ

            .PropBtnReg = Me.btnReg                                 'フッタ：登録ボタン
            .PropBtnCopy = Me.btnCopy                               'フッタ：複製ボタン
            .PropBtnMail = Me.btnMail                               'フッタ：メール作成ボタン
            .PropBtnMondai = Me.BtnMondai                           'フッタ：問題登録ボタン
            .PropBtnPrint = Me.btnPrint                             'フッタ：単票出力ボタン
            .PropBtnBack = Me.btnBack                               'フッタ：戻るボタン
            .PropBtnSMRenkei = Me.btnSMRenkei                       'フッタ：連携処理実施ボタン
            .PropBtnSMShow = Me.btnSMShow                           'フッタ：連携最新情報を見るボタン

            'システムエラー事前対応処理
            If logicHBKC0201.DoProcForErrorMain(dataHBKC0201) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            '★初期化設定

            'スプレッド行のクリアを行う
            ' -- 2017/08/17 e.okuda 行数が0ではない条件付加 -- 
            If .PropVwIncRireki.Sheets(0).Rows.Count > 0 Then
                .PropVwIncRireki.Sheets(0).RemoveRows(0, .PropVwIncRireki.Sheets(0).Rows.Count)
            End If

            If .PropVwkikiInfo.Sheets(0).Rows.Count > 0 Then
                .PropVwkikiInfo.Sheets(0).RemoveRows(0, .PropVwkikiInfo.Sheets(0).Rows.Count)
            End If

            If .PropVwRelation.Sheets(0).Rows.Count > 0 Then
                .PropVwRelation.Sheets(0).RemoveRows(0, .PropVwRelation.Sheets(0).Rows.Count)
            End If

            If .PropVwprocessLinkInfo.Sheets(0).Rows.Count > 0 Then
                .PropVwprocessLinkInfo.Sheets(0).RemoveRows(0, .PropVwprocessLinkInfo.Sheets(0).Rows.Count)
            End If

            If .PropVwFileInfo.Sheets(0).Rows.Count > 0 Then
                .PropVwFileInfo.Sheets(0).RemoveRows(0, .PropVwFileInfo.Sheets(0).Rows.Count)
            End If

            If .PropVwMeeting.Sheets(0).Rows.Count > 0 Then
                .PropVwMeeting.Sheets(0).RemoveRows(0, .PropVwMeeting.Sheets(0).Rows.Count)
            End If

            '表示初期化（担当ID1の以降）
            For j As Integer = LogicHBKC0201.COL_RIREKI_HIDE_TANTOGP1 To LogicHBKC0201.COL_RIREKI_BTNTANTO - 1
                .PropVwIncRireki.Sheets(0).Columns(j).Visible = False
            Next

            'データテーブル初期化
            .PropDtTantoRireki = Nothing                            '担当履歴情報←
            .PropDtwkRireki = Nothing                               '作業履歴情報

            'メール関連
            .PropTxtkigencondcikbncd = ""                            '期限切れ条件CI種別  
            .PropTxtkigencondtypekbn = ""                            '期限切れ条件タイプ
            .PropTxtkigencondkigen = ""                              '期限切れ条件期限
            .PropTxtKigenCondUsrID = ""                              '期限切れ条件ユーザID
            .PropTxtRegGp = ""
            .PropTxtRegUsr = ""
            .PropTxtRegDT = ""
            .PropTxtUpdateGp = ""
            .PropTxtUpdateUsr = ""
            .PropTxtUpdateDT = ""

            ''拡大判定初期化(拡大ボタン押下時の処理を元に戻す）
            '.PropblnKakudaiFlg = True
            'btnKakudai_Click(Me, New EventArgs)

            '★プロパティ設定

            'タイマーのインターバル設定
            Me.timKanryo.Interval = MSG_DISP_TIMER
            .PropLblkanryoMsg.Font = New Font(Me.Font.Name, Me.Font.Size, FontStyle.Bold)

            '作業履歴の高さ設定
            .PropIntVwRirekiRowHeight = 40

            '機器情報の高さ設定
            .PropVwkikiInfo.Sheets(0).Rows.Default.Height = 40

            'コンボボックスMaxDrop取得設定
            Dim intMaxdrop As Integer
            If commonLogicHBK.ChangeListSize(.PropCmbUkeKbn.Font.Height, Screen.GetWorkingArea(Me).Height, intMaxdrop) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If
            .PropCmbUkeKbn.MaxDropDownItems = intMaxdrop         '受付手段
            .PropCmbSystemNmb.PropMaxDrop = intMaxdrop - 10      '対象システム

        End With

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)


        '処理モードに応じて画面初期表示を行う
        If dataHBKC0201.PropStrProcMode = PROCMODE_NEW Then
            Me.Text = "ひびき：インシデント登録"
            '新規モード画面初期表示メイン処理
            If logicHBKC0201.InitFormNewModeMain(dataHBKC0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

        Else

            '対応関係者チェック処理(dataHBKC0201.PropintChkKankei) 
            If logicHBKC0201.KankeiCheckMain(dataHBKC0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            '1.	呼出し元画面が「インシデント検索一覧」画面の場合
            If dataHBKC0201.PropIntOwner = SCR_CALLMOTO_ICHIRAN Or
                dataHBKC0201.PropIntOwner = SCR_CALLMOTO_MENU Then
                ' 2017/08/30 e.okuda メニュー画面からの遷移を条件追加（クイックアクセス対応）

                '(ア)	編集モード
                If dataHBKC0201.PropIntChkKankei = KANKEI_CHECK_EDIT Then

                    'ロック設定メイン処理(dataHBKC0201.PropBlnBeLockedFlg )
                    If logicHBKC0201.LockMain(dataHBKC0201) = False Then
                        'システムエラー発生時はトランザクション系コントロールを非活性にする
                        If puErrMsg.StartsWith(HBK_E001) Then
                            If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                                Exit Sub
                            End If
                        End If
                        'エラーメッセージ表示
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        '処理終了
                        Exit Sub
                    End If

                    If dataHBKC0201.PropBlnBeLockedFlg = False Then   '編集モード（ロックされていない）
                        '関係○、ロック○
                        Me.Text = "ひびき：インシデント登録"
                        '編集モード画面初期表示メイン処理
                        If logicHBKC0201.InitFormEditModeMain(dataHBKC0201) = False Then
                            'システムエラー発生時はトランザクション系コントロールを非活性にする
                            If puErrMsg.StartsWith(HBK_E001) Then
                                If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                                    Exit Sub
                                End If
                            End If
                            'エラーメッセージ表示
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            '処理終了
                            Exit Sub
                        End If

                    Else       '作業履歴モード（ロックされている）
                        '関係○、ロック×
                        Me.Text = "ひびき：インシデント作業履歴編集"
                        '作業履歴編集モード変更
                        dataHBKC0201.PropStrProcMode = PROCMODE_RIREKI

                        '参照モード画面初期表示メイン処理
                        If logicHBKC0201.InitFormRirekiModeMain(dataHBKC0201) = False Then
                            'システムエラー発生時はトランザクション系コントロールを非活性にする
                            If puErrMsg.StartsWith(HBK_E001) Then
                                If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                                    Exit Sub
                                End If
                            End If
                            'エラーメッセージ表示
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            '処理終了
                            Exit Sub
                        End If

                        'ロックメッセージ表示
                        MsgBox(dataHBKC0201.PropStrBeLockedMsg.Replace("参照画面", "作業履歴編集画面"), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, TITLE_INFO)

                    End If

                Else
                    '(イ)	参照モード
                    Me.Text = "ひびき：インシデント登録"
                    '参照モードに変更
                    dataHBKC0201.PropStrProcMode = PROCMODE_REF

                    '参照モード画面初期表示メイン処理
                    If logicHBKC0201.InitFormRefModeMain(dataHBKC0201) = False Then
                        'システムエラー発生時はトランザクション系コントロールを非活性にする
                        If puErrMsg.StartsWith(HBK_E001) Then
                            If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                                Exit Sub
                            End If
                        End If
                        'エラーメッセージ表示
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        '処理終了
                        Exit Sub
                    End If

                End If

            Else    '2.呼出し元画面が「インシデント検索一覧」画面以外の場合
                '(ア)	参照モード 
                If dataHBKC0201.PropIntChkKankei <> KANKEI_CHECK_NONE Then

                    Me.Text = "ひびき：インシデント登録"
                    'CLOSING処理の回避用に参照モードとする
                    dataHBKC0201.PropStrProcMode = PROCMODE_REF

                    '参照モード画面初期表示メイン処理
                    If logicHBKC0201.InitFormRefModeMain(dataHBKC0201) = False Then
                        'システムエラー発生時はトランザクション系コントロールを非活性にする
                        If puErrMsg.StartsWith(HBK_E001) Then
                            If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                                Exit Sub
                            End If
                        End If
                        'エラーメッセージ表示
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        '処理終了
                        Exit Sub
                    End If

                Else
                    '(イ)	参照不可

                    'CLOSING処理の回避用に参照モードとする
                    dataHBKC0201.PropStrProcMode = PROCMODE_REF
                    'エラーメッセージ設定
                    puErrMsg = C0201_E036
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '画面閉じる
                    Me.Close()
                    '処理終了
                    Exit Sub
                End If

            End If

        End If

        '初期カーソル位置
        dataHBKC0201.PropDtpHasseiDT.Focus()

        '変更判定用のデータを設定（作業履歴編集モード用）
        datamodel = vwIncRireki.ActiveSheet.Models.Data         '作業履歴スプレッドモデルデータ
        init_row_cnt = datamodel.RowCount                       '初期表示時のスプレッド表示数


    End Sub

    ''' <summary>
    ''' フォーム初期表示時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>カーソル位置の設定を行う
    ''' <para>作成情報：2012/09/20 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKC0201_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        '初期カーソル
        Me.dtpHasseiDT.txtDate.Focus()
        Me.dtpHasseiDT.txtDate.SelectAll()

    End Sub

    ''' <summary>
    ''' フォームクローズ時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>関係者＋編集モードの場合はロック解除を行う、別画面表示時は閉じない
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKC0201_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        '別画面が開いている場合、クローズ処理を行わない
        If dataHBKC0201.PropfrmInstance IsNot Nothing Then
            'クローズ処理キャンセル
            e.Cancel = True
            'エラーメッセージ設定
            puErrMsg = C0201_E002
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If

        '編集モードの場合はロック解除を行う
        If dataHBKC0201.PropStrProcMode = PROCMODE_EDIT And
            (dataHBKC0201.PropBlnBeLockedFlg = False And dataHBKC0201.PropIntChkKankei = KANKEI_CHECK_EDIT) Then

            '画面クローズ時ロック解除処理
            If logicHBKC0201.UnlockWhenCloseMain(dataHBKC0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

        End If

    End Sub



    ''' <summary>
    ''' [解除]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>当画面のロックを解除し、編集モードで表示する
    ''' <para>作成情報：2012/07/22 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnUnlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grpLoginUser.btnUnlockClick

        'ロック解除処理を行う
        If logicHBKC0201.UnlockWhenClickBtnUnlockMain(dataHBKC0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 基本情報：受付手段データソース変更時の処理
    ''' </summary>
    ''' <param name="sender">引数sender</param>
    ''' <param name="e">引数e</param>
    ''' <remarks>受付手段コンボボックスのサイズを計算し、設定する
    ''' <para>作成情報：
    ''' <p>改定情報：2010/08/08 r.hoshino</p>
    ''' </para></remarks>
    Private Sub cmbUkeKbnCD_DataSourceChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbUkeKbnCD.DataSourceChanged

        'コンボボックスサイズ変更メイン処理
        If logicHBKC0201.ComboBoxResizeMain(sender) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 基本情報：担当グループデータソース変更時の処理
    ''' </summary>
    ''' <param name="sender">引数sender</param>
    ''' <param name="e">引数e</param>
    ''' <remarks>担当グループコンボボックスのサイズを計算し、設定する
    ''' <para>作成情報：
    ''' <p>改定情報：2010/08/08 r.hoshino</p>
    ''' </para></remarks>
    Private Sub cmbTantoGrpCD_DataSourceChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTantoGrpCD.DataSourceChanged

        'コンボボックスサイズ変更メイン処理
        If logicHBKC0201.ComboBoxResizeMain(sender) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 基本情報：インシデント種別データソース変更時の処理
    ''' </summary>
    ''' <param name="sender">引数sender</param>
    ''' <param name="e">引数e</param>
    ''' <remarks>インシデント種別コンボボックスのサイズを計算し、設定する
    ''' <para>作成情報：
    ''' <p>改定情報：2010/08/14 r.hoshino</p>
    ''' </para></remarks>
    Private Sub cmbIncKbnCD_DataSourceChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbIncKbnCD.DataSourceChanged
        'コンボボックスサイズ変更メイン処理
        If logicHBKC0201.ComboBoxResizeMain(sender) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 基本情報：ステータスデータソース変更時の処理
    ''' </summary>
    ''' <param name="sender">引数sender</param>
    ''' <param name="e">引数e</param>
    ''' <remarks>ステータスコンボボックスのサイズを計算し、設定する
    ''' <para>作成情報：
    ''' <p>改定情報：2010/08/14 r.hoshino</p>
    ''' </para></remarks>
    Private Sub cmbProcessStateCD_DataSourceChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProcessStateCD.DataSourceChanged
        'コンボボックスサイズ変更メイン処理
        If logicHBKC0201.ComboBoxResizeMain(sender) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 基本情報：ドメインデータソース変更時の処理
    ''' </summary>
    ''' <param name="sender">引数sender</param>
    ''' <param name="e">引数e</param>
    ''' <remarks>ドメインコンボボックスのサイズを計算し、設定する
    ''' <para>作成情報：
    ''' <p>改定情報：2010/08/14 r.hoshino</p>
    ''' </para></remarks>
    Private Sub cmbDomainCD_DataSourceChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDomainCD.DataSourceChanged
        'コンボボックスサイズ変更メイン処理
        If logicHBKC0201.ComboBoxResizeMain(sender) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' サポセン機器情報：作業データソース変更時の処理
    ''' </summary>
    ''' <param name="sender">引数sender</param>
    ''' <param name="e">引数e</param>
    ''' <remarks>作業コンボボックスのサイズを計算し、設定する
    ''' <para>作成情報：
    ''' <p>改定情報：2010/08/08 r.hoshino</p>
    ''' </para></remarks>
    Private Sub cmbWork_DataSourceChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbWork.DataSourceChanged
        'コンボボックスサイズ変更メイン処理
        If logicHBKC0201.ComboBoxResizeMain(sender) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    '[Add] 2012/10/23 s.yamaguchi 対象システム検索一覧呼出対応 START
    ''' <summary>
    ''' 基本情報：対象システム「検索」ボタンクリック時
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>対象システム検索一覧画面へ遷移し、検索した対象システムをセットする
    ''' <para>作成情報：2012/10/23 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSearchTaisyouSystem_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchTaisyouSystem.Click

        Dim frmHBKZ1301 As New HBKZ1301

        With dataHBKC0201

            .PropDtResultSub = frmHBKZ1301.ShowDialog()

            '検索結果をもとに対象システムをセットする
            If .PropDtResultSub IsNot Nothing Then
                .PropCmbSystemNmb.PropCmbColumns.SelectedValue = .PropDtResultSub.Rows(0).Item(4)     'CI番号
            End If

        End With

    End Sub
    '[Add] 2012/10/23 s.yamaguchi 対象システム検索一覧呼出対応 END

    ''' <summary>
    ''' 基本情報：ノウハウボタンクリック時
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>ノウハウURL画面へ遷移する
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnKnowHow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKnowHow.Click
        Dim HBKC0501 As New HBKC0501

        'パラメータセット
        With HBKC0501.dataHBKC0501
            .PropCINmb = dataHBKC0201.PropCmbSystemNmb.PropCmbColumns.SelectedValue         '対象システムのCI番号
        End With

        '画面開く
        If HBKC0501.ShowDialog Then

        End If

    End Sub

    ''' <summary>
    ''' 発生[時]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>時間入力画面を表示する
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnHasseiDT_HM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHasseiDT_HM.Click
        Dim HBKZ0801 As New HBKZ0801

        'パラメータセット
        With HBKZ0801.dataHBKZ0801
            .PropStrDate = dataHBKC0201.PropDtpHasseiDT.txtDate.Text
            .PropStrTime = dataHBKC0201.PropTxtHasseiDT_HM.PropTxtTime.Text
        End With

        '画面開く
        If HBKZ0801.ShowDialog Then
            dataHBKC0201.PropDtpHasseiDT.txtDate.Text = HBKZ0801.dataHBKZ0801.PropDtpSetDate.txtDate.Text
            dataHBKC0201.PropTxtHasseiDT_HM.PropTxtTime.Text = HBKZ0801.dataHBKZ0801.PropTxtSetTime.Text
        End If

    End Sub

    ''' <summary>
    ''' 回答[時]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>時間入力画面を表示する
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnKaito_HM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKaito_HM.Click
        Dim HBKZ0801 As New HBKZ0801

        'パラメータセット
        With HBKZ0801.dataHBKZ0801
            .PropStrDate = dataHBKC0201.PropDtpKaitoDT.txtDate.Text
            .PropStrTime = dataHBKC0201.PropTxtKaitoDT_HM.PropTxtTime.Text
        End With

        '画面開く
        If HBKZ0801.ShowDialog Then
            dataHBKC0201.PropDtpKaitoDT.txtDate.Text = HBKZ0801.dataHBKZ0801.PropDtpSetDate.txtDate.Text
            dataHBKC0201.PropTxtKaitoDT_HM.PropTxtTime.Text = HBKZ0801.dataHBKZ0801.PropTxtSetTime.Text
        End If
    End Sub

    ''' <summary>
    ''' 完了[時]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>時間入力画面を表示する
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnKanryo_HM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKanryo_HM.Click
        Dim HBKZ0801 As New HBKZ0801

        'パラメータセット
        With HBKZ0801.dataHBKZ0801
            .PropStrDate = dataHBKC0201.PropDtpKanryoDT.txtDate.Text
            .PropStrTime = dataHBKC0201.PropTxtKanryoDT_HM.PropTxtTime.Text
        End With

        '画面開く
        If HBKZ0801.ShowDialog Then
            dataHBKC0201.PropDtpKanryoDT.txtDate.Text = HBKZ0801.dataHBKZ0801.PropDtpSetDate.txtDate.Text
            dataHBKC0201.PropTxtKanryoDT_HM.PropTxtTime.Text = HBKZ0801.dataHBKZ0801.PropTxtSetTime.Text
        End If
    End Sub


    ''' <summary>
    ''' 基本情報：相手IDの入力後Enter時
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>相手IDをキーに氏名、シメイ、会社、部署、メールアドレス、電話番号を取得し設定する
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub txtPartnerID_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtPartnerID.PreviewKeyDown

        'Enterキー押下時
        If e.KeyValue = Keys.Enter Then
            '検索用パラメータ設定
            dataHBKC0201.PropStrSeaKey = dataHBKC0201.PropTxtPartnerID.Text         '相手ID

            If logicHBKC0201.GetPartnerDataMain(dataHBKC0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            '取得できない場合ブランクをセット
            dataHBKC0201.PropTxtPartnerNM.Text = ""         '相手氏名
            dataHBKC0201.PropTxtPartnerKana.Text = ""       '相手シメイ
            dataHBKC0201.PropTxtPartnerCompany.Text = ""    '相手会社
            dataHBKC0201.PropTxtPartnerBusyoNM.Text = ""    '相手部署
            dataHBKC0201.PropTxtPartnerMailAdd.Text = ""    '相手メアド
            dataHBKC0201.PropTxtPartnerTel.Text = ""        '相手電話
            dataHBKC0201.PropTxtPartnerContact.Text = ""    '相手連絡先    '[Add] 2014/05/14 e.okamura 相手連絡先取得条件修正
            If dataHBKC0201.PropDtResultSub IsNot Nothing Then
                If dataHBKC0201.PropDtResultSub.Rows.Count > 0 Then
                    dataHBKC0201.PropTxtPartnerNM.Text = dataHBKC0201.PropDtResultSub.Rows(0).Item("endusrnm")              '相手氏名
                    dataHBKC0201.PropTxtPartnerKana.Text = dataHBKC0201.PropDtResultSub.Rows(0).Item("endusrnmkana")        '相手シメイ
                    dataHBKC0201.PropTxtPartnerCompany.Text = dataHBKC0201.PropDtResultSub.Rows(0).Item("endusrcompany")    '相手会社
                    dataHBKC0201.PropTxtPartnerBusyoNM.Text = dataHBKC0201.PropDtResultSub.Rows(0).Item("endusrbusyonm")    '相手部署
                    dataHBKC0201.PropTxtPartnerMailAdd.Text = dataHBKC0201.PropDtResultSub.Rows(0).Item("endusrmailadd")    '相手メールアドレス
                    dataHBKC0201.PropTxtPartnerTel.Text = dataHBKC0201.PropDtResultSub.Rows(0).Item("endusrtel")            '相手電話番号
                End If
            End If

            '相手連絡先を設定
            HBKC_0201_GetPartnerContact()

        End If
    End Sub

    ''' <summary>
    ''' 基本情報：相手先の検索ボタンクリック時
    ''' </summary>
    ''' <remarks>エンドユーザー検索一覧画面を表示し、戻り値を設定する
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKC_0201_GetPartnerContact()

        With dataHBKC0201
            '[Del] 2014/05/14 e.okamura 相手連絡先取得条件修正 START
            ''連絡先に何もない場合
            'If .PropTxtPartnerContact.Text.Length = 0 Then
            '[Del] 2014/05/14 e.okamura 相手連絡先取得条件修正 END

            If logicHBKC0201.GetPartnerContactMain(dataHBKC0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            'End If  '[Del] 2014/05/14 e.okamura 相手連絡先取得条件修正

        End With

    End Sub

    ' ''' <summary>
    ' ''' 基本情報：相手IDのテキスト変更時
    ' ''' </summary>
    ' ''' <param name="sender">[IN]</param>
    ' ''' <param name="e">[IN]</param>
    ' ''' <remarks>エンドユーザー検索一覧画面を表示し、戻り値を設定する
    ' ''' <para>作成情報：2012/07/13 r.hoshino
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Sub txtPartnerID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPartnerID.TextChanged
    '    '相手連絡先
    '    HBKC_0201_GetPartnerContact()
    'End Sub

    ''' <summary>
    ''' 基本情報：相手先の検索ボタンクリック時
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>エンドユーザー検索一覧画面を表示し、戻り値を設定する
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnPartnerSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPartnerSearch.Click
        Dim HBKZ0201 As New HBKZ0201

        'パラメータセット
        With HBKZ0201.dataHBKZ0201
            .PropMode = SELECT_MODE_SINGLE                             'モード：単一選択
            .PropArgs = dataHBKC0201.PropTxtPartnerNM.Text             '検索条件：相手氏名
            .PropSplitMode = SPLIT_MODE_AND                            '検索条件区切り：AND
        End With

        'エンドユーザ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKC0201.PropDtResultSub = HBKZ0201.ShowDialog()

        If dataHBKC0201.PropDtResultSub IsNot Nothing Then
            dataHBKC0201.PropTxtPartnerID.Text = dataHBKC0201.PropDtResultSub.Rows(0).Item("EndUsrID")                '相手ID
            dataHBKC0201.PropTxtPartnerCompany.Text = dataHBKC0201.PropDtResultSub.Rows(0).Item("EndUsrCompany")      '相手会社
            dataHBKC0201.PropTxtPartnerBusyoNM.Text = dataHBKC0201.PropDtResultSub.Rows(0).Item("EndUsrBusyoNM")      '相手部署
            dataHBKC0201.PropTxtPartnerNM.Text = dataHBKC0201.PropDtResultSub.Rows(0).Item("EndUsrNM")                '相手氏名
            dataHBKC0201.PropTxtPartnerMailAdd.Text = dataHBKC0201.PropDtResultSub.Rows(0).Item("EndUsrMailAdd")      '相手メールアドレス
            dataHBKC0201.PropTxtPartnerTel.Text = dataHBKC0201.PropDtResultSub.Rows(0).Item("EndUsrTel")              '相手電話番号
            dataHBKC0201.PropTxtPartnerKana.Text = dataHBKC0201.PropDtResultSub.Rows(0).Item("EndUsrNMKana")          '相手氏名カナ
            dataHBKC0201.PropTxtPartnerContact.Text = ""                                                              '相手連絡先    '[Add] 2014/05/14 e.okamura 相手連絡先取得条件修正
        End If

        '相手連絡先
        HBKC_0201_GetPartnerContact()

    End Sub

    ''' <summary>
    ''' 基本情報：取得ボタンクリック時
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>相手IDの権限、借用物を取得する
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRentalKiki_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRentalKiki.Click

        'グローバルグループ
        If logicHBKC0201.GetGlobalGroupMain(dataHBKC0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        '借用物
        If logicHBKC0201.GetSyakuyouMain(dataHBKC0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        Dim StrResults As String = ""
        Dim strLine As String = ""
        If dataHBKC0201.PropDtResultSub IsNot Nothing Then
            For i As Integer = 0 To dataHBKC0201.PropDtResultSub.Rows.Count - 1
                strLine = dataHBKC0201.PropDtResultSub.Rows(i).Item(0)
                '初回以降文字連結
                If i > 0 Then
                    strLine = "/" + strLine
                End If
                StrResults += strLine
            Next
        End If

        dataHBKC0201.PropTxtRentalKiki.Text = StrResults

    End Sub


    ''' <summary>
    ''' 基本情報：担当IDの入力後Enter時
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>担当IDをキーに担当名を取得し設定する
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub txtIncTantoCD_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtIncTantoCD.PreviewKeyDown
        If e.KeyValue = Keys.Enter Then
            '1.	担当氏名、を自動でセットする。

            '検索用パラメータ設定
            dataHBKC0201.PropStrSeaKey = dataHBKC0201.PropTxtIncTantoCD.Text         '担当ID

            If logicHBKC0201.GetIncTantoDataMain(dataHBKC0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            '取得できない場合ブランクをセット
            dataHBKC0201.PropTxtIncTantoNM.Text = ""
            dataHBKC0201.PropCmbTantoGrpCD.SelectedValue = ""
            If dataHBKC0201.PropDtResultSub IsNot Nothing Then
                If dataHBKC0201.PropDtResultSub.Rows.Count > 0 Then
                    dataHBKC0201.PropTxtIncTantoNM.Text = dataHBKC0201.PropDtResultSub.Rows(0).Item("hbkusrnm")
                End If
                If dataHBKC0201.PropDtResultSub.Rows.Count = 1 Then
                    dataHBKC0201.PropCmbTantoGrpCD.SelectedValue = dataHBKC0201.PropDtResultSub.Rows(0).Item("groupcd")
                End If
            End If

        End If
    End Sub

    ''' <summary>
    ''' [私]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>ログインユーザーID、氏名、グループ名を設定する
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnMyInctantoID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMyInctantoID.Click

        'パラメータセット
        With dataHBKC0201
            .PropCmbTantoGrpCD.SelectedValue = PropWorkGroupCD
            .PropTxtIncTantoCD.Text = PropUserId
            .PropTxtIncTantoNM.Text = PropUserName
        End With

    End Sub

    ''' <summary>
    ''' 担当者：[検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>「ひびきユーザー検索一覧」画面を表示し、戻り値を設定する。
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnIncTantoSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIncTantoSearch.Click

        '「ひびきユーザー検索一覧」インスタンス作成
        Dim HBKZ0101 As New HBKZ0101

        'パラメータセット
        With HBKZ0101.dataHBKZ0101
            .PropMode = SELECT_MODE_SINGLE                              'モード：単一選択
            .PropArgs = dataHBKC0201.PropTxtIncTantoNM.Text             '検索条件：担当氏名
            .PropSplitMode = SPLIT_MODE_AND                             '検索条件区切り
        End With

        'グループ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKC0201.PropDtResultSub = HBKZ0101.ShowDialog()

        'グループ名、ID、NMを更新
        If dataHBKC0201.PropDtResultSub IsNot Nothing Then
            dataHBKC0201.PropCmbTantoGrpCD.SelectedValue = dataHBKC0201.PropDtResultSub.Rows(0).Item(3)     'グループCD
            dataHBKC0201.PropTxtIncTantoCD.Text = dataHBKC0201.PropDtResultSub.Rows(0).Item(0)              'ユーザーID
            dataHBKC0201.PropTxtIncTantoNM.Text = dataHBKC0201.PropDtResultSub.Rows(0).Item(2)              'ユーザー氏名
        End If

    End Sub

    ''' <summary>
    ''' 機器情報：[＋]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>「機器情報一覧」画面を表示し、戻り値を設定する。
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_kiki_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_Kiki.Click
        Dim HBKZ0701 As New HBKZ0701

        'パラメータセット
        With HBKZ0701.dataHBKZ0701
            .PropStrMode = SELECT_MODE_MULTI                                'パラメータ：選択モード
            .PropStrCIKbnCD = String.Empty                                  'パラメータ：CI種別コード
            .PropStrCIStatusCD = String.Empty                               'パラメータ：CIステータスコード
        End With

        '機器一覧検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKC0201.PropDtResultSub = HBKZ0701.ShowDialog()

        '機器情報一覧に取得データをセット
        If logicHBKC0201.AddRowkikiinfoMain(dataHBKC0201) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 機器情報：[－]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>機器情報一覧の選択行を削除する
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_kiki_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveRow_Kiki.Click

        '機器情報一覧選択行削除処理
        If logicHBKC0201.RemoveRowkikiinfoMain(dataHBKC0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 機器情報：WEBボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>種別＋番号でWebを開く
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnWeb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWeb.Click
        '※「種別＋番号」
        Dim strKey As String = ""
        With dataHBKC0201.PropVwkikiInfo.Sheets(0)
            If .Rows.Count > 0 Then
                strKey = .GetText(.ActiveRowIndex, LogicHBKC0201.COL_KIKI_SBT) + .GetText(.ActiveRowIndex, LogicHBKC0201.COL_KIKI_NMB)
                System.Diagnostics.Process.Start(PATH_WEB + strKey)
            End If
        End With

    End Sub

    ''' <summary>
    ''' 機器情報：SCCMボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>種別＋番号で指定したアドレスのWebを開く
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSCCM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSCCM.Click
        '※「種別＋番号」
        Dim strKey As String = ""
        With dataHBKC0201.PropVwkikiInfo.Sheets(0)
            If .Rows.Count > 0 Then
                strKey = .GetText(.ActiveRowIndex, LogicHBKC0201.COL_KIKI_SBT) + .GetText(.ActiveRowIndex, LogicHBKC0201.COL_KIKI_NMB)
                System.Diagnostics.Process.Start(PATH_SCCM + strKey)
            End If
        End With
    End Sub

    ''' <summary>
    ''' 機器情報：遠隔ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>種別＋番号でpcAnyWhereを起動する
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnEnkaku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnkaku.Click
        '※「種別＋番号」
        Dim strKey As String = ""
        With dataHBKC0201.PropVwkikiInfo.Sheets(0)

            If .Rows.Count > 0 Then
                strKey = .GetText(.ActiveRowIndex, LogicHBKC0201.COL_KIKI_SBT) & .GetText(.ActiveRowIndex, LogicHBKC0201.COL_KIKI_NMB)
                dataHBKC0201.PropStrSeaKey = strKey

                '遠隔接続処理
                If logicHBKC0201.REMOTEDATAMANAGER(dataHBKC0201) = False Then
                    ''システムエラー発生時はトランザクション系コントロールを非活性にする
                    'If puErrMsg.StartsWith(HBK_E001) Then
                    '    If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                    '        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '        Exit Sub
                    '    End If
                    'End If
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If

            End If

        End With


    End Sub

    ''' <summary>
    ''' 機器情報：L遠隔ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>種別＋番号でLAPLINKを起動する
    ''' <para>作成情報：2016/03/08 e.okamura
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnLEnkaku_Click(sender As System.Object, e As System.EventArgs) Handles btnLEnkaku.Click
        '※「種別＋番号」
        Dim strKey As String = ""
        With dataHBKC0201.PropVwkikiInfo.Sheets(0)

            If .Rows.Count > 0 Then
                strKey = .GetText(.ActiveRowIndex, LogicHBKC0201.COL_KIKI_SBT) & .GetText(.ActiveRowIndex, LogicHBKC0201.COL_KIKI_NMB)
                dataHBKC0201.PropStrSeaKey = strKey

                '遠隔接続処理
                If logicHBKC0201.LAPLINK_REMOTEDATAMANAGER(dataHBKC0201) = False Then
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If
            End If

        End With
    End Sub


    ''' <summary>
    ''' 作業履歴一覧：データモデル変更時
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>作業履歴スプレッドのデータモデル変更時のイベント処理を行う。
    ''' <para>作成情報：2012/08/07 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub datamodel_Changed(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.Model.SheetDataModelEventArgs) Handles datamodel.Changed
        'セルの値が変更されたとき
        If e.Type = FarPoint.Win.Spread.Model.SheetDataModelEventType.CellsUpdated Then
            '変更フラグON
            bln_chg_flg = True
        End If
        '行追加されたとき
        If e.Type = FarPoint.Win.Spread.Model.SheetDataModelEventType.RowsAdded Then
            add_row_cnt += 1
        End If
        '行削除されたとき
        If e.Type = FarPoint.Win.Spread.Model.SheetDataModelEventType.RowsRemoved Then
            add_row_cnt -= 1
        End If

    End Sub

    ''' <summary>
    ''' 作業履歴一覧：編集モード解除時
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>内部フラグを参照し、変更されたかを確認する
    ''' <para>作成情報：2012/08/07 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub FpSpread1_EditModeOff(ByVal sender As Object, ByVal e As System.EventArgs) Handles vwIncRireki.EditModeOff
        '変更フラグONの場合
        If bln_chg_flg Then
            If add_row_cnt > 0 AndAlso add_row_cnt >= vwIncRireki.Sheets(0).ActiveRowIndex Then
                '新規追加行を変更した
            Else
                '更新フラグON
                bln_update_flg = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' 作業履歴一覧：編集モード開始時
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>内部フラグを初期化する、階層コンボボックスの非表示設定を行う。
    ''' <para>作成情報：2012/08/07 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub FpSpread1_EditModeOn(ByVal sender As Object, ByVal e As System.EventArgs) Handles vwIncRireki.EditModeOn
        '変更フラグ初期化
        bln_chg_flg = False

        'アクティブセルのセル型を判断します
        Dim row As Integer = vwIncRireki.ActiveSheet.ActiveRowIndex
        Dim col As Integer = vwIncRireki.ActiveSheet.ActiveColumnIndex

        'ヘッダーの場合
        If row < 0 AndAlso col < 0 Then
            Exit Sub
        End If

        ''フォームの位置を取得
        'Dim frmscrY As Integer = Me.PointToScreen(Me.Location).Y

        Select Case col
            Case LogicHBKC0201.COL_RIREKI_KEIKA     '経過種別
                ''セルの情報を取得
                'Dim cmb1 As FarPoint.Win.FpCombo = CType(vwIncRireki.EditingControl, FarPoint.Win.FpCombo)
                'Dim cmbscrY As Integer = cmb1.PointToScreen(cmb1.Location).Y
                ''画面にはみ出ないサイズを取得
                'Dim workH As Integer = cmbscrY - frmscrY
                ''項目のフォントサイズを取得
                'Dim fontH As Integer = cmb1.Font.Height

                ''コンボボックスMaxDrop取得設定
                'Dim intMaxdrop1 As Integer
                'If commonLogicHBK.ChangeListSize(fontH, workH, intMaxdrop1) = False Then
                '    'エラーメッセージ表示
                '    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '    Exit Sub
                'End If

                ''動的にセルタイプのプロパティを変更する。
                'Dim ctp1 As CellType.ComboBoxCellType = CType(vwIncRireki.ActiveSheet.GetCellType(row, col), CellType.ComboBoxCellType)
                'ctp1.MaxDrop = intMaxdrop1

            Case LogicHBKC0201.COL_RIREKI_SYSTEM     '対象システム
                'セルの情報を取得
                Dim cmb2 As CellType.GeneralEditor = CType(vwIncRireki.EditingControl, FarPoint.Win.Spread.CellType.GeneralEditor)
                'Dim cmbscrY As Integer = cmb2.PointToScreen(cmb2.Location).Y
                ''画面にはみ出ないサイズを取得
                'Dim workH As Integer = cmbscrY - frmscrY
                ''項目のフォントサイズを取得
                'Dim fontH As Integer = cmb2.Font.Height

                ''コンボボックスMaxDrop取得設定
                'Dim intMaxdrop2 As Integer
                'If commonLogicHBK.ChangeListSize(fontH, workH, intMaxdrop2) = False Then
                '    'エラーメッセージ表示
                '    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '    Exit Sub
                'End If

                ''動的にセルタイプのプロパティを変更する。
                'Dim ctp2 As CellType.MultiColumnComboBoxCellType = CType(vwIncRireki.ActiveSheet.GetCellType(row, col), CellType.MultiColumnComboBoxCellType)
                'ctp2.MaxDrop = intMaxdrop2

                ' ドロップダウンリストのオブジェクトを取得します
                Dim cmbSpread As FpSpread = CType(cmb2.SubEditor, FpSpread)

                ' ドロップダウンリストの 1,2列目を非表示にします
                cmbSpread.ActiveSheet.Columns(0).Visible = False
                cmbSpread.ActiveSheet.Columns(1).Visible = False

        End Select

    End Sub


    ''' <summary>
    ''' 作業履歴一覧：ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>作業履歴スプレッドのボタンに関する処理を行う。
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwIncRireki_ButtonClicked(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.EditorNotifyEventArgs) Handles vwIncRireki.ButtonClicked

        '選択されたRow,Colのindexを設定
        dataHBKC0201.PropIntRowSelect = e.Row
        dataHBKC0201.PropIntColSelect = e.Column

        Select Case e.Column
            Case LogicHBKC0201.COL_RIREKI_YOTEIJI, LogicHBKC0201.COL_RIREKI_KAISHIJI, LogicHBKC0201.COL_RIREKI_SYURYOJI
                '「日付設定画面」インスタンス作成
                Dim HBKZ0801 As New HBKZ0801

                With dataHBKC0201.PropVwIncRireki.Sheets(0)
                    'パラメータセット
                    If Not .GetText(e.Row, e.Column - 1).Equals("") Then
                        '日付、時間を設定
                        HBKZ0801.dataHBKZ0801.PropStrDate = Mid(.GetText(e.Row, e.Column - 1), 1, InStr(.GetText(e.Row, e.Column - 1), " ") - 1)
                        HBKZ0801.dataHBKZ0801.PropStrTime = Mid(.GetText(e.Row, e.Column - 1), InStr(.GetText(e.Row, e.Column - 1), " ") + 1)

                    End If

                    If HBKZ0801.ShowDialog Then
                        .SetValue(e.Row, e.Column - 1, HBKZ0801.dataHBKZ0801.PropDtpSetDate.txtDate.Text + " " + HBKZ0801.dataHBKZ0801.PropTxtSetTime.Text)
                        '更新フラグを立てる
                        dataHBKC0201.PropDtwkRireki.Rows(e.Row).EndEdit()
                        '変更フラグONの場合
                        If bln_chg_flg Then
                            If add_row_cnt > 0 AndAlso add_row_cnt >= vwIncRireki.Sheets(0).ActiveRowIndex Then
                                '新規追加行を変更した
                            Else
                                '更新フラグON
                                bln_update_flg = True
                            End If
                        End If
                    End If
                End With

            Case LogicHBKC0201.COL_RIREKI_BTNTANTO

                '「ひびきユーザー検索一覧」インスタンス作成
                Dim HBKZ0101 As New HBKZ0101

                '検索一覧受け渡し用データ作成
                If logicHBKC0201.CreateDtIncRirekiTantoMain(dataHBKC0201) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            Exit Sub
                        End If
                    End If
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If

                'パラメータセット
                With HBKZ0101.dataHBKZ0101
                    .PropMode = SELECT_MODE_MULTI                              'モード：複数選択
                    .PropArgs = String.Empty                                   '検索条件：選択されたユーザーID、GP？
                    .PropSplitMode = SPLIT_MODE_AND                            '検索条件区切り：AND
                    .PropDataTable = dataHBKC0201.PropDtResultSub              '設定済みデータ
                    .PropInitMode = 1                                          '専用フラグ
                End With

                'グループ検索画面を表示し、戻り値としてデータテーブルを取得
                dataHBKC0201.PropDtResultSub = HBKZ0101.ShowDialog()

                '選択件数が設定範囲を超える場合エラー
                If dataHBKC0201.PropDtResultSub IsNot Nothing AndAlso dataHBKC0201.PropDtResultSub.Rows.Count > INC_WKRIREKI_MAXTANTO Then
                    puErrMsg = String.Format(C0201_E041, INC_WKRIREKI_MAXTANTO)
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If

                '作業履歴 スプレッド内担当者追加処理
                If logicHBKC0201.AddIncRirekiTantoMain(dataHBKC0201) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            Exit Sub
                        End If
                    End If
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If

                '更新フラグを立てる
                dataHBKC0201.PropDtwkRireki.Rows(e.Row).EndEdit()
                '変更フラグONの場合
                If bln_chg_flg Then
                    If add_row_cnt > 0 AndAlso add_row_cnt >= vwIncRireki.Sheets(0).ActiveRowIndex Then
                        '新規追加行を変更した
                    Else
                        '更新フラグON
                        bln_update_flg = True
                    End If
                End If
        End Select

    End Sub


    ''' <summary>
    ''' [拡大]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>1.	作業履歴の入力枠を拡大する。
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnKakudai_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKakudai.Click


        If Settings.Instance.propHBKC0201_Expantion_wkRireki = False Then
            '設定を変更する
            With Settings.Instance
                .propHBKC0201_Expantion_wkRireki = True
            End With
            '拡大
            kakudai(True)
        Else
            '設定を変更する
            With Settings.Instance
                .propHBKC0201_Expantion_wkRireki = False
            End With
            '戻す
            kakudai(False)
        End If

    End Sub

    ''' <summary>
    ''' 拡大処理
    ''' </summary>
    ''' <param name="setFlg">[IN]</param>
    ''' <remarks>1.	作業履歴の入力枠を拡大する。
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub kakudai(ByVal setFlg As Boolean)


        If setFlg = True Then
            '増加サイズ（登録ボタンから上15px分まで)
            tbInput.Height = Me.btnReg.Location.Y - 15 - Me.tbInput.Location.Y - Me.vwIncRireki.Location.X
            tbInput.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Top

        Else
            'デフォルトに戻す（grpRelationから上2px分まで)
            tbInput.Anchor = AnchorStyles.Left + AnchorStyles.Top
            tbInput.Height = Me.grpRelation.Location.Y - 2 - Me.tbInput.Location.Y - Me.vwIncRireki.Location.X

        End If

    End Sub

    ''' <summary>
    ''' [リフレッシュ]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>作業履歴の表示を最新の状態に更新する。
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : 2017/07/17 e.okuda</p>
    ''' </para></remarks>
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        '作業履歴の表示を最新の状態に更新する。

        '新規登録モードの場合は処理しない
        If dataHBKC0201.PropStrProcMode = PROCMODE_NEW Then
            Exit Sub
        End If

        'データテーブルの内容をチェックする
        Dim bln_henkou_flg As Boolean = False
        For i As Integer = 0 To dataHBKC0201.PropDtwkRireki.Rows.Count - 1
            If dataHBKC0201.PropDtwkRireki.Rows(i).RowState = DataRowState.Modified Then
                bln_henkou_flg = True
                Exit For
            End If
            If dataHBKC0201.PropDtwkRireki.Rows(i).RowState = DataRowState.Added Then
                bln_henkou_flg = True
                Exit For
            End If
        Next

        '変更なしの場合
        If bln_henkou_flg = False Then
            Exit Sub
        End If

        'リフレッシュ実行の確認メッセージを表示
        If MsgBox(C0201_W001, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
            '処理終了
            Exit Sub
        End If


        'スプレッドのクリア
        With dataHBKC0201.PropVwIncRireki.Sheets(0)
            ' -- 2017/08/17 e.okuda 行数が0ではない条件付加 -- 
            If .Rows.Count > 0 Then
                .RemoveRows(0, .Rows.Count)
            End If
        End With

        If logicHBKC0201.RefrashIncwkRirekiMain(dataHBKC0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [＋]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>作業履歴一覧の一番上に一行追加する。
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_Rireki_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_Rireki.Click


        '作業履歴一覧空行追加処理
        If logicHBKC0201.AddRowIncwkRirekiMain(dataHBKC0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        'データRowAdd時はハンドラ解除
        RemoveHandler datamodel.Changed, AddressOf datamodel_Changed

        '追加した行にログインユーザーのグループとユーザー名と対象システムを設定する。
        With dataHBKC0201.PropVwIncRireki.Sheets(0)
            .SetValue(0, logicHBKC0201.COL_RIREKI_SYSTEM, dataHBKC0201.PropCmbSystemNmb.PropCmbColumns.SelectedValue)
            .SetValue(0, logicHBKC0201.COL_RIREKI_TANTOGP1, PropWorkGroupName)
            .SetValue(0, logicHBKC0201.COL_RIREKI_TANTOID1, PropUserName)
            .SetValue(0, logicHBKC0201.COL_RIREKI_HIDE_TANTOGP1, PropWorkGroupCD)
            .SetValue(0, logicHBKC0201.COL_RIREKI_HIDE_TANTOID1, PropUserId)
            .RowHeader.Cells(0, 0).Text = " "
        End With

        'ハンドラ元に戻す
        AddHandler datamodel.Changed, AddressOf datamodel_Changed

    End Sub

    ''' <summary>
    ''' [ー]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>新規の追加した場合のみ作業履歴一覧の選択行を削除する
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_Rireki_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveRow_Rireki.Click
        '作業履歴一覧選択行削除処理
        If logicHBKC0201.RemoveRowIncwkRirekiMain(dataHBKC0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If
    End Sub


    ''' <summary>
    ''' 関係者情報：[＋グループ]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>「グループ検索」画面を表示し、戻り値を設定する。
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_relaG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_relaG.Click

        'グループ検索画面インスタンス作成
        Dim HBKZ0301 As New HBKZ0301

        'パラメータセット
        With HBKZ0301.dataHBKZ0301
            .PropMode = SELECT_MODE_MULTI       'モード：複数選択
            .PropArgs = String.Empty            '検索条件：なし
            .PropSplitMode = SPLIT_MODE_AND     '検索条件区切り：AND
        End With

        'グループ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKC0201.PropDtResultSub = HBKZ0301.ShowDialog()

        '関係者情報一覧に取得データをセット
        If logicHBKC0201.SetGroupToVwRelationMain(dataHBKC0201) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 関係者情報：[＋ユーザー]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>「ひびきユーザー検索」画面を表示し、戻り値を設定する。
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_RelaU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_RelaU.Click

        'ひびきユーザー検索画面インスタンス作成
        Dim HBKZ0101 As New HBKZ0101

        'パラメータセット
        With HBKZ0101.dataHBKZ0101
            .PropMode = SELECT_MODE_MULTI           'モード：複数選択
            .PropArgs = String.Empty                '検索条件：なし
            .PropSplitMode = SPLIT_MODE_AND         '検索条件区切り：AND
        End With

        'ひびきユーザー検索画面を表示し、戻り値としてデータテーブルを取得
        '
        dataHBKC0201.PropDtResultSub = HBKZ0101.ShowDialog()

        '関係者情報一覧に取得データをセット
        If logicHBKC0201.SetUserToVwRelationMain(dataHBKC0201) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 関係者情報：[－]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>関係者情報一覧のスプレッド選択行を削除する
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_Rela_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveRow_Rela.Click

        '関係者情報一覧選択行削除処理
        If logicHBKC0201.RemoveRowRelationMain(dataHBKC0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' プロセスリンク情報：[＋]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>「プロセス検索」画面を表示し、戻り値を設定する。
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_plink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_plink.Click
        Dim HBKZ0401 As New HBKZ0401
        'パラメータセット
        With HBKZ0401.dataHBKZ0401
            .PropMode = SELECT_MODE_MULTI                               'モード：単一選択
            .PropArgs = String.Empty                                    '検索条件：なし
            .PropSplitMode = SPLIT_MODE_AND                             '検索条件区切り
            .PropStrFromProcessKbn = PROCESS_TYPE_INCIDENT              'プロセス区分
            .PropStrFromProcessNmb = dataHBKC0201.PropIntINCNmb         'プロセス番号
        End With

        'グループ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKC0201.PropDtResultSub = HBKZ0401.ShowDialog()

        'プロセスリンク一覧に取得データをセット
        If logicHBKC0201.AddRowpLinkMain(dataHBKC0201) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' プロセスリンク情報：[－]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>プロセスリンク情報一覧のスプレッド選択行を削除する
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_plink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveRow_Plink.Click

        'プロセスリンク情報一覧選択行削除処理
        If logicHBKC0201.RemoveRowpLinkMain(dataHBKC0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 関連ファイル：[+]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>関連ファイル設定画面を表示し、選択されたファイル情報をに当画面にセットする
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_File_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_File.Click
        Dim HBKZ1101 As New HBKZ1101
        'パラメータセット
        With HBKZ1101.dataHBKZ1101

        End With


        '関連ファイル検索画面を表示し、戻り値としてデータテーブルを取得
        If HBKZ1101.ShowDialog() Then

            dataHBKC0201.PropTxtFileNaiyo = HBKZ1101.dataHBKZ1101.PropTxtFileNaiyo.Text
            dataHBKC0201.PropTxtFilePath = HBKZ1101.dataHBKZ1101.PropTxtFilePath.Text

            '関係ファイル一覧に取得データをセット
            If logicHBKC0201.AddRowFileinfoMain(dataHBKC0201) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

        End If


    End Sub

    ''' <summary>
    ''' 関連ファイル：[-]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>関連ファイル情報一覧の選択行を削除する。
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_File_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveRow_File.Click
        '関係ファイル一覧選択行削除処理
        If logicHBKC0201.RemoveRowFileInfoMain(dataHBKC0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' [開]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>登録済みのファイルを読み取り専用で開く
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnOpenFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenFile.Click

        If dataHBKC0201.PropStrProcMode = PROCMODE_EDIT OrElse _
            dataHBKC0201.PropStrProcMode = PROCMODE_RIREKI OrElse _
            dataHBKC0201.PropStrProcMode = PROCMODE_REF Then        '編集モード  ,作業履歴モード、参照モード

            '[Del] 2012/10/30 s.yamaguchi START
            'If (Me.vwFileInfo.Visible = True) AndAlso (Me.vwFileInfo.Sheets(0).Rows.Count <> 0) Then

            '    'Dim cr() As FarPoint.Win.Spread.Model.CellRange
            '    'cr = Me.vwFileInfo.ActiveSheet.GetSelections()

            '    '' 未選択の場合エラーメッセージを表示する
            '    'If cr.Length = 0 Then
            '    '    'エラーメッセージ表示
            '    '    MsgBox(C0201_E037, MsgBoxStyle.Critical, TITLE_ERROR)
            '    '    Return
            '    'End If

            '    'フォーカスが移動したときの行数を取得する（列ヘッダ＆全選択が選択されている場合はエラー）
            '    For i As Integer = 0 To cr.Length - 1

            '        '行数が１以外のときはエラー
            '        If (cr(i).RowCount() <> 1) Then
            '            'エラーメッセージ表示
            '            MsgBox(C0201_E037, MsgBoxStyle.Critical, TITLE_ERROR)
            '            Return
            '        ElseIf (cr(i).RowCount() = 1) Then

            '            '選択行番号をデータクラスにセット
            '            dataHBKC0201.PropIntSelectedRow = Me.vwFileInfo.ActiveSheet.ActiveRowIndex

            '            'ファイルオープン処理
            '            If logicHBKC0201.FileOpenMain(dataHBKC0201) = False Then
            '                'システムエラー発生時はトランザクション系コントロールを非活性にする
            '                If puErrMsg.StartsWith(HBK_E001) Then
            '                    If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
            '                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '                        Exit Sub
            '                    End If
            '                End If
            '                'エラーメッセージ表示
            '                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '                '処理終了
            '                Exit Sub
            '            End If
            '            Return
            '        End If
            '    Next
            'End If
            '[Del] 2012/10/30 s.yamaguchi END

            If (Me.vwFileInfo.Visible = True) AndAlso (Me.vwFileInfo.Sheets(0).Rows.Count <> 0) Then

                '[Add] 2012/10/30 s.yamaguchi START
                '変数宣言
                Dim intSelectedRowFrom As Integer                   '選択開始行番号
                Dim intSelectedRowTo As Integer                     '選択終了行番号

                '選択開始行、終了行取得
                intSelectedRowFrom = Me.vwFileInfo.Sheets(0).Models.Selection.AnchorRow
                intSelectedRowTo = Me.vwFileInfo.Sheets(0).Models.Selection.LeadRow

                '行選択を明示的に行う。
                With Me.vwFileInfo
                    .ActiveSheet.Models.Selection.AddSelection(.ActiveSheet.ActiveRowIndex, _
                                                               .ActiveSheet.ActiveColumnIndex, _
                                                               1, _
                                                               System.Math.Abs(intSelectedRowTo - intSelectedRowFrom) + 1)
                End With

                '検索結果の選択数が一件以外の時はエラーメッセージ出力
                If Me.vwFileInfo.Sheets(0).SelectionCount <> 1 _
                   Or intSelectedRowTo - intSelectedRowFrom <> 0 Then
                    puErrMsg = C0201_E037
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
                '[Add] 2012/10/30 s.yamaguchi END

                '選択行番号をデータクラスにセット
                dataHBKC0201.PropIntSelectedRow = Me.vwFileInfo.ActiveSheet.ActiveRowIndex

                'ファイルオープン処理
                If logicHBKC0201.FileOpenMain(dataHBKC0201) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            Exit Sub
                        End If
                    End If
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' [ダ]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>登録済みのファイルをローカルのデスクトップにダウンロードする。
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSaveFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveFile.Click

        '[Del] 2012/10/30 s.yamaguchi START
        'If dataHBKC0201.PropStrProcMode = PROCMODE_EDIT OrElse _
        '    dataHBKC0201.PropStrProcMode = PROCMODE_RIREKI OrElse _
        '    dataHBKC0201.PropStrProcMode = PROCMODE_REF Then        '編集モード 、作業履歴モード、参照モード

        '    If (Me.vwFileInfo.Visible = True) AndAlso (Me.vwFileInfo.Sheets(0).Rows.Count <> 0) Then

        '        Dim cr() As FarPoint.Win.Spread.Model.CellRange
        '        cr = Me.vwFileInfo.ActiveSheet.GetSelections()

        '        ' 未選択の場合エラーメッセージを表示する
        '        If cr.Length = 0 Then
        '            'エラーメッセージ表示
        '            MsgBox(C0201_E037, MsgBoxStyle.Critical, TITLE_ERROR)
        '            Return
        '        End If

        '        'フォーカスが移動したときの行数を取得する（列ヘッダ＆全選択が選択されている場合はエラー）
        '        For i As Integer = 0 To cr.Length - 1

        '            '行数が１以外のときはエラー
        '            If (cr(i).RowCount() <> 1) Then
        '                'エラーメッセージ表示
        '                MsgBox(C0201_E037, MsgBoxStyle.Critical, TITLE_ERROR)
        '                Return
        '            ElseIf (cr(i).RowCount() = 1) Then

        '                '選択行番号をデータクラスにセット
        '                dataHBKC0201.PropIntSelectedRow = Me.vwFileInfo.ActiveSheet.ActiveRowIndex

        '                '編集モード画面処理
        '                If logicHBKC0201.FileDownLoadMain(dataHBKC0201) = False Then
        '                    'システムエラー発生時はトランザクション系コントロールを非活性にする
        '                    If puErrMsg.StartsWith(HBK_E001) Then
        '                        If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
        '                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
        '                            Exit Sub
        '                        End If
        '                    End If
        '                    'エラーメッセージ表示
        '                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
        '                    '処理終了
        '                    Exit Sub
        '                End If
        '                Return
        '            End If
        '        Next
        '    End If
        'End If
        '[Del] 2012/10/30 s.yamaguchi END

        If dataHBKC0201.PropStrProcMode = PROCMODE_EDIT OrElse _
            dataHBKC0201.PropStrProcMode = PROCMODE_RIREKI OrElse _
            dataHBKC0201.PropStrProcMode = PROCMODE_REF Then        '編集モード 、作業履歴モード、参照モード

            If (Me.vwFileInfo.Visible = True) AndAlso (Me.vwFileInfo.Sheets(0).Rows.Count <> 0) Then

                '[Add] 2012/10/30 s.yamaguchi START
                '変数宣言
                Dim intSelectedRowFrom As Integer                   '選択開始行番号
                Dim intSelectedRowTo As Integer                     '選択終了行番号

                '選択開始行、終了行取得
                intSelectedRowFrom = Me.vwFileInfo.Sheets(0).Models.Selection.AnchorRow
                intSelectedRowTo = Me.vwFileInfo.Sheets(0).Models.Selection.LeadRow

                '行選択を明示的に行う。
                With Me.vwFileInfo
                    .ActiveSheet.Models.Selection.AddSelection(.ActiveSheet.ActiveRowIndex, _
                                                               .ActiveSheet.ActiveColumnIndex, _
                                                               1, _
                                                               System.Math.Abs(intSelectedRowTo - intSelectedRowFrom) + 1)
                End With

                '検索結果の選択数が一件以外の時はエラーメッセージ出力
                If Me.vwFileInfo.Sheets(0).SelectionCount <> 1 _
                   Or intSelectedRowTo - intSelectedRowFrom <> 0 Then
                    puErrMsg = C0201_E037
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
                '[Add] 2012/10/30 s.yamaguchi END

                '選択行番号をデータクラスにセット
                dataHBKC0201.PropIntSelectedRow = Me.vwFileInfo.ActiveSheet.ActiveRowIndex

                '編集モード画面処理
                If logicHBKC0201.FileDownLoadMain(dataHBKC0201) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            Exit Sub
                        End If
                    End If
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If
            End If
        End If
    End Sub


    ''' <summary>
    ''' [登録]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>入力チェックを行い、モードに応じて登録処理を行う。
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        dataHBKC0201.PropLblkanryoMsg.Text = ""
        Application.DoEvents()


        '処理モードに応じた入力チェックを行う
        If dataHBKC0201.PropStrProcMode = PROCMODE_NEW Then                 '新規登録モード

            '入力チェック処理      
            If logicHBKC0201.CheckInputValueMain(dataHBKC0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

        ElseIf dataHBKC0201.PropStrProcMode = PROCMODE_EDIT Then            '編集モード

            'ロック解除チェック(dataHBKC0201.PropBlnBeLockedFlg)
            If logicHBKC0201.CheckBeUnlockedMain(dataHBKC0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            'ロック解除フラグに応じて処理を行う
            If dataHBKC0201.PropBlnBeLockedFlg = False Then             '編集モード

                'ロック解除されていない時、入力チェックを行う
                If logicHBKC0201.CheckInputValueMain(dataHBKC0201) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            Exit Sub
                        End If
                    End If
                    'マウスポインタ変更(砂時計→通常)
                    Me.Cursor = Windows.Forms.Cursors.Default
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If

                '【ADD】2014/04/07 e.okamura 作業取消時セット機器更新修正 START
                '作業取消する機器で、作業追加前と現在のセット機器が異なる場合、警告メッセージを表示する
                Dim strMsgKiki As String = ""
                If logicHBKC0201.CheckInputValueOnTabSapTorikeshi(dataHBKC0201, strMsgKiki) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            Exit Sub
                        End If
                    End If
                    'マウスポインタ変更(砂時計→通常)
                    Me.Cursor = Windows.Forms.Cursors.Default
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If
                If strMsgKiki.Equals("") = False Then
                    'セット機器取消不可警告メッセージを表示(OKボタンのみ)
                    MsgBox(String.Format(C0201_W005, strMsgKiki), MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, TITLE_WARNING)
                End If
                '【ADD】2014/04/07 e.okamura 作業取消時セット機器更新修正 END

        ElseIf dataHBKC0201.PropBlnBeLockedFlg = True Then         '参照モード

            'ロック解除時、ログ出力処理と画面の再描画を行う
            If logicHBKC0201.SetFormRefModeFromEditModeMain(dataHBKC0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default

            'セットされているメッセージによってメッセージボックスのスタイルを変更する
            If dataHBKC0201.PropStrBeUnlockedMsg.StartsWith(String.Format(C0201_W003, dataHBKC0201.PropStrLogFilePath)) Then
                'ロック解除メッセージ表示
                MsgBox(dataHBKC0201.PropStrBeUnlockedMsg, MsgBoxStyle.Exclamation, TITLE_WARNING)

            ElseIf dataHBKC0201.PropStrBeUnlockedMsg.StartsWith(String.Format(C0201_E035, dataHBKC0201.PropStrLogFilePath)) Then
                'データ更新エラーメッセージ
                MsgBox(dataHBKC0201.PropStrBeUnlockedMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            End If
            '編集モードで画面再描画
            dataHBKC0201.PropStrProcMode = PROCMODE_EDIT
            HBKC0201_Load(Me, New EventArgs)
            Exit Sub
        End If


        ElseIf dataHBKC0201.PropStrProcMode = PROCMODE_RIREKI Then             '作業履歴モード

            '更新有無チェック
            If add_row_cnt = 0 AndAlso bln_update_flg = False Then
                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default
                'エラーメッセージ設定
                puErrMsg = C0201_E034
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            '入力チェックを行う
            If logicHBKC0201.CheckInputValueMain(dataHBKC0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

        End If



        '処理モードに応じた登録処理を行う
        If dataHBKC0201.PropStrProcMode = PROCMODE_NEW Then                 '新規登録モード

            '新規登録処理
            If logicHBKC0201.RegistDataOnNewModeMain(dataHBKC0201) = False Then

                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)

                'ログ出力を行う
                If logicHBKC0201.SetFormRefModeFromEditModeMain(dataHBKC0201) = True Then

                    'セットされているメッセージによってメッセージボックスのスタイルを変更する
                    If dataHBKC0201.PropStrBeUnlockedMsg.StartsWith(String.Format(C0201_W003, dataHBKC0201.PropStrLogFilePath)) Then
                        'ロック解除メッセージ表示
                        MsgBox(dataHBKC0201.PropStrBeUnlockedMsg, MsgBoxStyle.Exclamation, TITLE_WARNING)

                    ElseIf dataHBKC0201.PropStrBeUnlockedMsg.StartsWith(String.Format(C0201_E035, dataHBKC0201.PropStrLogFilePath)) Then
                        'データ更新エラーメッセージ
                        MsgBox(dataHBKC0201.PropStrBeUnlockedMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    End If
                End If
                '処理終了
                Exit Sub
            End If

            '最終作業データ格納
            PropLastProcessKbn = PROCESS_TYPE_INCIDENT
            PropLastProcessNmb = dataHBKC0201.PropIntINCNmb

            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default

            '登録完了メッセージ表示
            'MsgBox(C0201_I001, MsgBoxStyle.Information, TITLE_INFO)
            dataHBKC0201.PropLblkanryoMsg.Text = C0201_I001

            '呼び出し元画面を検索一覧にする
            dataHBKC0201.PropIntOwner = SCR_CALLMOTO_ICHIRAN

            '編集モードで画面再描画
            dataHBKC0201.PropStrProcMode = PROCMODE_EDIT
            HBKC0201_Load(Me, New EventArgs)

        ElseIf dataHBKC0201.PropStrProcMode = PROCMODE_EDIT Then            '編集モード

            '更新処理を行う
            If logicHBKC0201.RegistDataOnEditModeMain(dataHBKC0201) = False Then

                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)

                'ログ出力を行う
                If logicHBKC0201.SetFormRefModeFromEditModeMain(dataHBKC0201) = True Then

                    'セットされているメッセージによってメッセージボックスのスタイルを変更する
                    If dataHBKC0201.PropStrBeUnlockedMsg.StartsWith(String.Format(C0201_W003, dataHBKC0201.PropStrLogFilePath)) Then
                        'ロック解除メッセージ表示
                        MsgBox(dataHBKC0201.PropStrBeUnlockedMsg, MsgBoxStyle.Exclamation, TITLE_WARNING)

                    ElseIf dataHBKC0201.PropStrBeUnlockedMsg.StartsWith(String.Format(C0201_E035, dataHBKC0201.PropStrLogFilePath)) Then
                        'データ更新エラーメッセージ
                        MsgBox(dataHBKC0201.PropStrBeUnlockedMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    End If

                End If

                '処理終了
                Exit Sub
            End If

            '最終作業データ格納
            PropLastProcessKbn = PROCESS_TYPE_INCIDENT
            PropLastProcessNmb = dataHBKC0201.PropIntINCNmb

            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default

            '登録完了メッセージ表示
            'MsgBox(C0201_I001, MsgBoxStyle.Information, TITLE_INFO)
            dataHBKC0201.PropLblkanryoMsg.Text = C0201_I001

            '編集モードで画面再描画
            HBKC0201_Load(Me, New EventArgs)


        ElseIf dataHBKC0201.PropStrProcMode = PROCMODE_RIREKI Then      '作業履歴モード

            '更新処理を行う
            If logicHBKC0201.RegistDataOnRirekiModeMain(dataHBKC0201) = False Then

                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)

                'ログ出力を行う
                If logicHBKC0201.SetFormRefModeFromEditModeMain(dataHBKC0201) = True Then
                    'セットされているメッセージによってメッセージボックスのスタイルを変更する
                    If dataHBKC0201.PropStrBeUnlockedMsg.StartsWith(String.Format(C0201_W003, dataHBKC0201.PropStrLogFilePath)) Then
                        'ロック解除メッセージ表示
                        MsgBox(dataHBKC0201.PropStrBeUnlockedMsg, MsgBoxStyle.Exclamation, TITLE_WARNING)

                    ElseIf dataHBKC0201.PropStrBeUnlockedMsg.StartsWith(String.Format(C0201_E035, dataHBKC0201.PropStrLogFilePath)) Then
                        'データ更新エラーメッセージ
                        MsgBox(dataHBKC0201.PropStrBeUnlockedMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    End If
                End If

                '処理終了
                Exit Sub
            End If

            '最終作業データ格納
            PropLastProcessKbn = PROCESS_TYPE_INCIDENT
            PropLastProcessNmb = dataHBKC0201.PropIntINCNmb

            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default

            '登録完了メッセージ表示
            'MsgBox(C0201_I001, MsgBoxStyle.Information, TITLE_INFO)
            dataHBKC0201.PropLblkanryoMsg.Text = C0201_I001

            '編集モードで画面再描画
            dataHBKC0201.PropStrProcMode = PROCMODE_EDIT
            HBKC0201_Load(Me, New EventArgs)

        End If

        'タイマーを開始する
        Me.timKanryo.Start()

    End Sub

    ''' <summary>
    ''' [戻る]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>当画面を閉じて呼び出し元画面へ遷移する
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click

        '当画面を閉じる
        Me.Close()

    End Sub

    ''' <summary>
    ''' [複製]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>複製対象の項目を残し新規登録モードで表示する。
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click

        '編集モードの場合、ロック解除処理を行う
        If dataHBKC0201.PropStrProcMode = PROCMODE_EDIT Then
            '複製時、ロック解除処理を行う
            If logicHBKC0201.UnlockDataMain(dataHBKC0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If
        End If

        '複製対象を一時退避
        Dim bakCmbUkeKbn As String      '受付手段
        Dim bakCmbIncKbn As String      'インシデント種別
        Dim bakTxtTitle As String       'タイトル
        Dim bakTxtUkeNaiyo As String    '受付内容
        Dim bakCmbSystemNmb As Integer  '対象システム

        'パラメータ設定
        With dataHBKC0201
            bakCmbUkeKbn = .PropCmbUkeKbn.SelectedValue
            bakCmbIncKbn = .PropCmbIncKbnCD.SelectedValue
            bakTxtTitle = .PropTxtTitle.Text
            bakTxtUkeNaiyo = .PropTxtUkeNaiyo.Text
            bakCmbSystemNmb = .PropCmbSystemNmb.PropCmbColumns.SelectedValue
        End With

        '新規作成モードで画面再描画
        dataHBKC0201.PropIntINCNmb = 0                  'インシデント番号初期化
        dataHBKC0201.PropStrProcMode = PROCMODE_NEW     '新規登録モード
        dataHBKC0201.PropIntChkKankei = 0               '関係者クリア
        HBKC0201_Load(Me, New EventArgs)

        '退避データ再設定
        With dataHBKC0201
            .PropLblkanryoMsg.Text = ""                     '完了のお知らせ
            .PropCmbUkeKbn.SelectedValue = bakCmbUkeKbn
            .PropCmbIncKbnCD.SelectedValue = bakCmbIncKbn
            .PropTxtTitle.Text = bakTxtTitle
            .PropTxtUkeNaiyo.Text = bakTxtUkeNaiyo
            .PropCmbSystemNmb.PropCmbColumns.SelectedValue = bakCmbSystemNmb
        End With

        'カーソル位置を設定
        HBKC0201_Shown(Me, New EventArgs)

    End Sub

    ''' <summary>
    ''' [メール作成]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>メール作成画面を開く
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnMail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMail.Click

        'メールテンプレート選択画面
        Dim HBKZ1001 As New HBKZ1001
        'インシデント登録（メール作成）処理インスタンス化
        Dim logicHBKC0209 As New LogicHBKC0209
        Dim dataHBKC0209 As New DataHBKC0209

        'パラメータ設定
        With HBKZ1001.dataHBKZ1001
            .PropStrGroupCD = PropWorkGroupCD                                     '前画面パラメータ：グループCD
            .PropStrGroupNM = PropWorkGroupName                                   '前画面パラメータ：グループ名
            .PropStrProcessKbn = PROCESS_TYPE_INCIDENT                            '前画面パラメータ：プロセス区分
            .PropStrKigenCondCIKbnCD = dataHBKC0201.PropTxtkigencondcikbncd       '前画面パラメータ：期限切れ条件CI種別
            .PropStrKigenCondTypeKbn = dataHBKC0201.PropTxtkigencondtypekbn       '前画面パラメータ：期限切れ条件タイプ
            .PropStrKigenCondKigen = dataHBKC0201.PropTxtkigencondkigen           '前画面パラメータ：期限切れ条件期限
            .PropStrKigenCondKbn = dataHBKC0201.PropTxtKigenCondUsrID             '前画面パラメータ：期限切れ条件区分
            .PropVwKiki = dataHBKC0201.PropVwkikiInfo                             '前画面パラメータ：機器情報スプレッド
            .PropIntColCINmb = logicHBKC0201.COL_KIKI_CINMB                       '前画面パラメータ：機器情報スプレッドのCI番号列インデックス
            .PropStrProcMode = dataHBKC0201.PropStrProcMode                       '前画面パラメータ：処理モード
        End With

        'メールフォーマット選択
        HBKZ1001.ShowDialog()

        'メールフォーマットデータ取得
        dataHBKC0209.PropDtReturnData = HBKZ1001.dataHBKZ1001.PropDtReturnData

        '当画面表示
        Me.Show()

        '戻り値のDataTableがNullだった場合、処理を中断
        If dataHBKC0209.PropDtReturnData Is Nothing Then
            Exit Sub
        End If

        '最終お知らせ日更新区分を取得
        Dim intUpdatelastInfoDtKbn As Integer = HBKZ1001.dataHBKZ1001.PropIntUpdateLastInfoDtKbn

        '入力項目をデータクラスにセット
        With dataHBKC0209
            .PropStrIncCD = dataHBKC0201.PropTxtIncCD.Text                                  'インシデント番号
            .PropStrUkeKbn = dataHBKC0201.PropCmbUkeKbn.Text                                '受付手段
            .PropStrIncKbnCD = dataHBKC0201.PropCmbIncKbnCD.Text                            'インシデント種別
            .PropStrProcessStateCD = dataHBKC0201.PropCmbprocessStateCD.Text                'ステータス
            .PropStrHasseiDT = dataHBKC0201.PropDtpHasseiDT.txtDate.Text                    '発生日時
            .PropStrHasseiDT_HM = dataHBKC0201.PropTxtHasseiDT_HM.PropTxtTime.Text          '発生日時時分
            .PropStrKaitoDT = dataHBKC0201.PropDtpKaitoDT.txtDate.Text                      '回答日時
            .PropStrKaitoDT_HM = dataHBKC0201.PropTxtKaitoDT_HM.PropTxtTime.Text            '回答日時時分
            .PropStrKanryoDT = dataHBKC0201.PropDtpKanryoDT.txtDate.Text                    '完了日時
            .PropStrKanryoDT_HM = dataHBKC0201.PropTxtKanryoDT_HM.PropTxtTime.Text          '完了日時時分
            .PropStrPriority = dataHBKC0201.PropTxtPriority.Text                            '重要度
            .PropStrErrlevel = dataHBKC0201.PropTxtErrlevel.Text                            '障害レベル
            .PropStrTitle = dataHBKC0201.PropTxtTitle.Text                                  'タイトル
            .PropStrUkeNaiyo = dataHBKC0201.PropTxtUkeNaiyo.Text                            '受付内容
            .PropStrTaioKekka = dataHBKC0201.PropTxtTaioKekka.Text                          '対応結果
            .PropStrRegGrpNM = dataHBKC0201.PropTxtRegGp                                    '登録者グループ名
            .PropStrRegNM = dataHBKC0201.PropTxtRegUsr                                      '登録者ユーザ名
            .PropStrRegDT = dataHBKC0201.PropTxtRegDT                                       '登録日時
            .PropStrUpdateGrpNM = dataHBKC0201.PropTxtUpdateGp                              '最終更新グループ名
            .PropStrUpdateNM = dataHBKC0201.PropTxtUpdateUsr                                '最終更新者
            .PropStrUpdateDT = dataHBKC0201.PropTxtUpdateDT                                 '最終更新日時
            '対象システム
            .PropStrSystemNmb = dataHBKC0201.PropCmbSystemNmb.PropCmbColumns.SelectedValue.ToString
            .PropStrOutSideToolNmb = dataHBKC0201.PropTxtOutSideToolNmb.Text                '外部ツール番号
            .PropStrKengen = dataHBKC0201.PropTxtKengen.Text                                '権限
            .PropStrRentalKiki = dataHBKC0201.PropTxtRentalKiki.Text                        '借用物
            .PropStrEventID = dataHBKC0201.PropTxtEventID.Text                              'イベントID
            .PropStrSource = dataHBKC0201.PropTxtSource.Text                                'ソース
            .PropStrOPCEventID = dataHBKC0201.PropTxtOPCEventID.Text                        'OPCイベントID
            .PropStrEventClass = dataHBKC0201.PropTxtEventClass.Text                        'イベントクラス
            .PropStrTantoGrpCD = dataHBKC0201.PropCmbTantoGrpCD.Text                        '担当グループ
            .PropStrIncTantoNM = dataHBKC0201.PropTxtIncTantoNM.Text                        '担当者名
            .PropStrDomainCD = dataHBKC0201.PropCmbDomainCD.Text                            'ドメイン
            .PropStrPartnerCompany = dataHBKC0201.PropTxtPartnerCompany.Text                '相手会社
            .PropStrPartnerID = dataHBKC0201.PropTxtPartnerID.Text                          '相手ID
            .PropStrPartnerNM = dataHBKC0201.PropTxtPartnerNM.Text                          '相手氏名
            .PropStrPartnerKana = dataHBKC0201.PropTxtPartnerKana.Text                      '相手シメイ
            .PropStrPartnerKyokuNM = dataHBKC0201.PropTxtPartnerKyokuNM.Text                '相手局
            .PropStrPartnerBusyoNM = dataHBKC0201.PropTxtPartnerBusyoNM.Text                '相手部署
            .PropStrPartnerTel = dataHBKC0201.PropTxtPartnerTel.Text                        '相手電話番号
            .PropStrPartnerMailAdd = dataHBKC0201.PropTxtPartnerMailAdd.Text                '相手メールアドレス
            .PropStrPartnerContact = dataHBKC0201.PropTxtPartnerContact.Text                '相手連絡先
            .PropStrPartnerBase = dataHBKC0201.PropTxtPartnerBase.Text                      '相手拠点
            .PropStrPartnerRoom = dataHBKC0201.PropTxtPartnerRoom.Text                      '相手番組/部屋
            '指示書
            If dataHBKC0201.PropChkShijisyoFlg.Checked = True Then
                .PropStrShijisyoFlg = SHIJISYO_FLG_ON
            Else
                .PropStrShijisyoFlg = SHIJISYO_FLG_OFF
            End If
            .PropVwkikiInfo = dataHBKC0201.PropVwkikiInfo                           '機器情報データ(機器種別,機器番号,機器情報)
            .PropVwFileInfo = dataHBKC0201.PropVwFileInfo                           '関連情報(ファイル,ファイル説明)
            .PropVwIncRireki = dataHBKC0201.PropVwIncRireki                         '作業履歴データ(経過種別,対象オブジェクト,作業予定日時,作業開始日時,作業終了日時,作業内容,作業担当者業務チーム,作業担当者)
            .PropVwSapMainte = dataHBKC0201.PropVwSapMainte                         'サポセン機器情報(作業,交換,種別,番号,分類2（メーカー）,名称（機種）,作業備考,作業予定日,作業完了日,完了,取消)
            .PropVwMeeting = dataHBKC0201.PropVwMeeting                             '会議情報データ(番号,実施日,タイトル,承認)
            .PropStrBIko1 = dataHBKC0201.PropTxtBIko1.Text                          'テキスト１
            .PropStrBIko2 = dataHBKC0201.PropTxtBIko2.Text                          'テキスト２
            .PropStrBIko3 = dataHBKC0201.PropTxtBIko3.Text                          'テキスト３
            .PropStrBIko4 = dataHBKC0201.PropTxtBIko4.Text                          'テキスト４
            .PropStrBIko5 = dataHBKC0201.PropTxtBIko5.Text                          'テキスト５
            'フリーフラグ1
            If dataHBKC0201.PropChkFreeFlg1.Checked = True Then
                .PropStrFreeFlg1 = FLG_ON_NM
            Else
                .PropStrFreeFlg1 = FLG_OFF_NM
            End If
            'フリーフラグ2
            If dataHBKC0201.PropChkFreeFlg2.Checked = True Then
                .PropStrFreeFlg2 = FLG_ON_NM
            Else
                .PropStrFreeFlg2 = FLG_OFF_NM
            End If
            'フリーフラグ3
            If dataHBKC0201.PropChkFreeFlg3.Checked = True Then
                .PropStrFreeFlg3 = FLG_ON_NM
            Else
                .PropStrFreeFlg3 = FLG_OFF_NM
            End If
            'フリーフラグ4
            If dataHBKC0201.PropChkFreeFlg4.Checked = True Then
                .PropStrFreeFlg4 = FLG_ON_NM
            Else
                .PropStrFreeFlg4 = FLG_OFF_NM
            End If
            'フリーフラグ5
            If dataHBKC0201.PropChkFreeFlg5.Checked = True Then
                .PropStrFreeFlg5 = FLG_ON_NM
            Else
                .PropStrFreeFlg5 = FLG_OFF_NM
            End If
            .PropVwRelation = dataHBKC0201.PropVwRelation                           '対応関係者情報データ(区分,ID,グループ名,ユーザー名)
            .PropStrGrpHistory = dataHBKC0201.PropTxtGrpHistory.Text                'グループ履歴
            .PropStrTantoHistory = dataHBKC0201.PropTxtTantoHistory.Text            '担当者履歴
            .PropVwprocessLinkInfo = dataHBKC0201.PropVwprocessLinkInfo             'プロセスリンク管理番号(区分,番号)


        End With

        'メール作成処理呼び出し
        If logicHBKC0209.CreateIncidentMailMain(dataHBKC0209) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        '編集モード時のみお知らせ表示
        If dataHBKC0201.PropStrProcMode = PROCMODE_EDIT Then
            '最終お知らせ日を更新する場合、更新処理を行う
            If intUpdatelastInfoDtKbn = UPDATE_LASTINFODT_KBN_UPDATE Then
                If logicHBKC0201.UpdateLastInfoDtWhenCreateMailMain(dataHBKC0201) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            Exit Sub
                        End If
                    End If
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If
            End If
        End If


    End Sub


    ''' <summary>
    ''' [問題登録]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>問題登録画面を開く
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub BtnMondai_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnMondai.Click

        '問題登録画面
        dataHBKC0201.PropfrmInstance = New HBKD0201

        'パラメータ設定
        With dataHBKC0201.PropfrmInstance.dataHBKD0201
            .PropStrProcMode = PROCMODE_NEW
            .PropBlnFromCheckFlg = True
            .PropIntIncNmb = dataHBKC0201.PropIntINCNmb
            .PropVwProcessLinkInfo_Save = dataHBKC0201.PropVwprocessLinkInfo
            .PropIntTSystemNmb = dataHBKC0201.PropCmbSystemNmb.PropCmbColumns.SelectedValue
            '★閉じるボタン表示用のフラグを渡す
            .PropIntOwner = SCR_CALLMOTO_REG
        End With

        'クローズ処理の追加
        AddHandler CType(dataHBKC0201.PropfrmInstance, Form).FormClosed, AddressOf frm_FormClosed

        '画面制御開始
        Scr_Enabled_Start()

        '別画面として表示
        dataHBKC0201.PropfrmInstance.Show()

    End Sub


    ''' <summary>
    ''' [別画面のフォーム]ボタン非活性処理
    ''' </summary>
    ''' <param name="Ctl">[IN]コントロール</param>
    ''' <param name="aryList">[IN/OUT]活性リスト</param>
    ''' <remarks>活性ボタンオブジェクトの活性リスト作成し非活性とする
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub SetButonEnabledFalse(ByVal Ctl As Control.ControlCollection, ByRef aryList As ArrayList)

        For Each c As Control In Ctl
            If c.HasChildren Then
                '再帰
                SetButonEnabledFalse(c.Controls, aryList)
            Else
                If TypeOf c Is Button Then
                    If CType(c, Button).Enabled = True Then
                        '非活性対象のボタンは除外
                        If AryNotfrmCtlList.IndexOf(c.Name) = -1 Then
                            '活性しているボタンを格納
                            aryList.Add(c.Name)
                            '非活性とする
                            c.Enabled = False
                        End If
                    End If
                End If
            End If
        Next

    End Sub

    ''' <summary>
    ''' [別画面のフォーム]クローズ後の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>閉じた後プロセスリンクを再取得する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub frm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs)

        'プロセスリンク再取得処理
        If logicHBKC0201.RefreshPLinkMain(dataHBKC0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        '画面制御終了
        Scr_Enabled_End()

    End Sub

    ''' <summary>
    ''' [別画面のフォーム]ボタン非活性解除処理
    ''' </summary>
    ''' <param name="Ctl">[IN]コントロール</param>
    ''' <param name="aryList">[IN/OUT]活性リスト</param>
    ''' <remarks>活性リストのボタンオブジェクトを活性にする
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub SetButonEnabledTrue(ByVal Ctl As Control.ControlCollection, ByRef aryList As ArrayList)

        For Each c As Control In Ctl
            If c.HasChildren Then
                '再帰
                SetButonEnabledTrue(c.Controls, aryList)
            Else
                If TypeOf c Is Button Then
                    '活性リストにあったものだけ活性とする
                    If CType(c, Button).Enabled = False AndAlso _
                       aryList.Contains(c.Name) Then
                        '活性とする
                        c.Enabled = True
                    End If
                End If
            End If
        Next

    End Sub

    ''' <summary>
    ''' [帳票出力]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>帳票出力画面を開く
    ''' <para>作成情報：2012/08/07 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        '出力形式選択画面
        Dim HBKZ0901 As New HBKZ0901

        Dim intOutputKbn As Integer = HBKZ0901.ShowDialog()
        If intOutputKbn <> OUTPUT_RETURN_CANCEL Then

            Dim logicHBKC0207 As New LogicHBKC0207
            If logicHBKC0207.InitMain(dataHBKC0201, intOutputKbn) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If
        End If


    End Sub

    ''' <summary>
    ''' 会議情報：[＋]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>会議検索一覧を表示し選択されたデータを画面に設定する
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : 2012/08/02 r.hoshino　会議情報変更に伴う修正</p>
    ''' </para></remarks>
    Private Sub btnAddRow_meeting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_meeting.Click

        Dim HBKC0301 As New HBKC0301
        'パラメータセット
        With HBKC0301.dataHBKC0301
            .PropBlnTranFlg = SELECT_MODE_NOTMENU                               '前画面パラメータ：メニュー遷移フラグ
            .PropProcessKbn = PROCESS_TYPE_INCIDENT                             'プロセス区分
            .PropProcessNmb = dataHBKC0201.PropIntINCNmb                        'プロセス番号
            .PropTitle = dataHBKC0201.PropTxtTitle.Text                         'タイトル
        End With

        'クローズ処理の追加
        AddHandler CType(HBKC0301, Form).FormClosed, AddressOf Meeting_FormClosed

        '会議情報検索画面を表示
        If HBKC0301.ShowDialog = DIALOG_RETURN_OK Then

            '検索結果を取得
            dataHBKC0201.PropDtResultSub = HBKC0301.dataHBKC0301.PropDtReturnSub

            '会議情報一覧に取得データをセット
            If logicHBKC0201.AddRowMeetingMain(dataHBKC0201) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

        End If

    End Sub

    ''' <summary>
    ''' [会議一覧]クローズ後の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>閉じた後会議結果を再取得する
    ''' <para>作成情報：2012/09/11 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub Meeting_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs)

        '会議結果情報再取得処理
        If logicHBKC0201.RefreshMeetingMain(dataHBKC0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub


    ''' <summary>
    ''' 会議情報：[ー]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択したスプレッド行を削除する
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_meeting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveRow_meeting.Click
        '会議情報一覧選択行削除処理
        If logicHBKC0201.RemoveRowMeetingMain(dataHBKC0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If
    End Sub


    ''' <summary>
    ''' タブページ切替時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>サポセン機器情報タブ選択時、基本情報タブの相手情報をサポセン機器情報タブの相手情報へコピーする
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub tbInput_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbInput.SelectedIndexChanged

        '【ADD】2012/08/08 r.hoshino　レイアウト制御用：START
        ''基本情報：拡大時、リサイズする
        'If dataHBKC0201.PropblnKakudaiFlg = True Then
        '    btnKakudai_Click(Me, New EventArgs)
        'End If
        ''【ADD】2012/08/08 r.hoshino　レイアウト制御用：END

        '前回選択タブが基本情報の場合は拡大をもどす
        Select Case intSelectedTabIdx
            Case logicHBKC0201.TAB_KHN
                kakudai(False)

            Case Else
                '基本情報タブに戻る場合
                If Me.tbInput.SelectedIndex = logicHBKC0201.TAB_KHN Then
                    kakudai(Settings.Instance.propHBKC0201_Expantion_wkRireki)
                End If

        End Select


        '基本情報タブからサポセン機器情報タブ選択時
        If intSelectedTabIdx = logicHBKC0201.TAB_KHN AndAlso Me.tbInput.SelectedIndex = logicHBKC0201.TAB_SAP Then
            '相手情報コピー処理
            If logicHBKC0201.SelectedTabSapMain(dataHBKC0201) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If
        End If

        '前回選択タブにカレントタブを設定
        intSelectedTabIdx = Me.tbInput.SelectedIndex

    End Sub

    ''' <summary>
    ''' 作業コンボボックス選択値変更確定時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>作業の選択状態に応じて[作業追加]ボタンの活性／非活性を切り替える
    ''' <para>作成情報：2012/08/16 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub cmbWork_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbWork.SelectionChangeCommitted

        '作業の選択状態に応じて[作業追加]ボタンの活性／非活性を切り替える
        If logicHBKC0201.ChangeBtnAddRowSapMainteEnabledMain(dataHBKC0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [作業追加]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>機器検索一覧を表示し、結果をサポセン機器メンテナンススプレッドに反映する
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_SapMainte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_SapMainte.Click

        '機器検索一覧画面用パラメータ作成
        If logicHBKC0201.CreateParamsForAddWorkMain(dataHBKC0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        '機器検索一覧画面インスタンス作成
        Dim HBKZ0701 As New HBKZ0701

        'パラメータセット
        With HBKZ0701.dataHBKZ0701
            .PropStrMode = SELECT_MODE_MULTI                            'モード：複数選択
            .PropStrCIKbnCD = CI_TYPE_SUPORT                            'CI種別：サポセン
            .PropStrCIStatusCD = dataHBKC0201.PropStrPlmCIStatusCD      'CIステータスコード
            .PropStrWorkCD = Me.cmbWork.SelectedValue                   '作業コード
        End With

        '機器検索一覧画面を表示し、戻り値としてデータテーブルを取得
        dataHBKC0201.PropDtResultSub = HBKZ0701.ShowDialog()

        'サポセン機器メンテナンススプレッドに取得データをセット
        If logicHBKC0201.AddRowVwSapMainteMain(dataHBKC0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' サポセン機器メンテナンス一覧：ボタンまたはチェックボックスセルクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>
    ''' <para>作成情報：2012/07/28 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwSapMainte_ButtonClicked(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.EditorNotifyEventArgs) Handles vwSapMainte.ButtonClicked

        dataHBKC0201.PropIntRowSelect = e.Row          'クリック行
        dataHBKC0201.PropIntColSelect = e.Column       'クリック列

        'クリックされたセルに応じて処理を行う
        Select Case dataHBKC0201.PropIntColSelect

            Case logicHBKC0201.COL_SAP_SELECT       '選択チェックボックスクリック時

                '各ボタンの活性／非活性の制御を行う
                If logicHBKC0201.ChangeBtnSapEnabledMain(dataHBKC0201) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            Exit Sub
                        End If
                    End If
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If

            Case logicHBKC0201.COL_SAP_BTN_EDIT      '編集ボタンクリック時

                'サポセン機器登録画面へ編集モードで遷移する
                Dim HBKB0601 As New HBKB0601
                With HBKB0601.dataHBKB0601
                    .PropStrProcMode = PROCMODE_EDIT                                                                        '処理モード：編集モード
                    .PropIntIncNmb = Integer.Parse(Me.txtIncCD.Text)                                                        'インシデント番号
                    .PropIntWorkNmb = _
                        Me.vwSapMainte.Sheets(0).Cells(dataHBKC0201.PropIntRowSelect, logicHBKC0201.COL_SAP_WORKNMB).Value  '作業番号
                    .PropIntCINmb = _
                        Me.vwSapMainte.Sheets(0).Cells(dataHBKC0201.PropIntRowSelect, logicHBKC0201.COL_SAP_CINMB).Value    'CI番号
                    .PropStrWorkCD = _
                        Me.vwSapMainte.Sheets(0).Cells(dataHBKC0201.PropIntRowSelect, logicHBKC0201.COL_SAP_WORKCD).Value   '作業コード
                End With
                Me.Hide()
                HBKB0601.ShowDialog()
                Me.Show()

            Case logicHBKC0201.COL_SAP_BTN_CEP      '分割ボタンクリック時

                '選択機器をセットから解除する
                If logicHBKC0201.CepalateSetKikiMain(dataHBKC0201) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            Exit Sub
                        End If
                    End If
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If

            Case logicHBKC0201.COL_SAP_COMPFLG      '完了チェックボックスクリック時

                '完了／取消チェックボックスの制御を行う
                If logicHBKC0201.ChangeCompCancelEnabledMain(dataHBKC0201) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            Exit Sub
                        End If
                    End If
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If

            Case logicHBKC0201.COL_SAP_CANCELFLG    '取消チェックボックスクリック時

                '完了／取消チェックボックスの制御を行う
                If logicHBKC0201.ChangeCompCancelEnabledMain(dataHBKC0201) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            Exit Sub
                        End If
                    End If
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If

        End Select

    End Sub

    ''' <summary>
    ''' サポセン機器メンテナンス一覧：セルクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>クリックされたセルに応じて処理を行う
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwSapMainte_CellClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwSapMainte.CellClick

        'データが1件もない場合、処理終了
        If Me.vwSapMainte.Sheets(0).RowCount = 0 Then
            Exit Sub
        End If

        '初期化
        mcdSapMainte.Hide()

        '選択された行番号・列番号をデータクラスにセット
        dataHBKC0201.PropIntRowSelect = e.Row
        dataHBKC0201.PropIntColSelect = e.Column

        'セルがロックされていない場合のみ処理
        If Me.vwSapMainte.Sheets(0).Cells(dataHBKC0201.PropIntRowSelect, dataHBKC0201.PropIntColSelect).Locked = False Then
            '作業開始日または完了日がクリックされた場合、カレンダーを表示する
            Select Case e.Column
                Case logicHBKC0201.COL_SAP_WORKSCEDT, logicHBKC0201.COL_SAP_WORKCOMPDT
                    mcdSapMainte.Location = New Point(System.Windows.Forms.Cursor.Position.X - Me.Location.X - Me.vwSapMainte.Sheets(0).Columns(e.Column).Width, _
                                                      System.Windows.Forms.Cursor.Position.Y - Me.Location.Y)
                    mcdSapMainte.Show()
            End Select
        End If

    End Sub

    ''' <summary>
    ''' サポセン機器メンテナンス一覧：セルダブルクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>完了または取消済の場合、履歴モードでサポセン登録画面へ遷移する
    ''' <para>作成情報：2012/10/02 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwSapMainte_CellDoubleClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwSapMainte.CellDoubleClick

        'ヘッダクリック時は処理を抜ける
        If e.RowHeader = True Or e.ColumnHeader = True Then
            Exit Sub
        End If

        '変数宣言
        Dim intRowIndex As Integer = e.Row
        Dim intColIndex As Integer = e.Column

        '完了／取消済フラグがONの場合のみ遷移する
        If Me.vwSapMainte.Sheets(0).Cells(intRowIndex, logicHBKC0201.COL_SAP_COMPCANCELZUMIFLG).Value = True Then

            'サポセン機器登録画面へ履歴モードで遷移する
            Dim HBKB0601 As New HBKB0601
            With HBKB0601.dataHBKB0601
                .PropStrProcMode = PROCMODE_RIREKI                                                              '処理モード：履歴モード
                .PropIntIncNmb = Integer.Parse(Me.txtIncCD.Text)                                                'インシデント番号
                .PropIntWorkNmb = _
                    Me.vwSapMainte.Sheets(0).Cells(intRowIndex, logicHBKC0201.COL_SAP_WORKNMB).Value            '作業番号
                .PropIntCINmb = _
                    Me.vwSapMainte.Sheets(0).Cells(intRowIndex, logicHBKC0201.COL_SAP_CINMB).Value              'CI番号
                .PropStrWorkCD = _
                    Me.vwSapMainte.Sheets(0).Cells(intRowIndex, logicHBKC0201.COL_SAP_WORKCD).Value             '作業コード
                .PropIntRirekiNo = _
                    Me.vwSapMainte.Sheets(0).Cells(intRowIndex, logicHBKC0201.COL_SAP_LASTUPRIREKINO).Value     '最終更新時履歴番号
            End With
            Me.Hide()
            HBKB0601.ShowDialog()
            Me.Show()

        End If

    End Sub

    ''' <summary>
    ''' サポセン機器メンテナンス一覧：カレンダー日付選択時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択した日付をスプレッドへ設定し、カレンダーは非表示とする
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub mcdSapMainte_DateSelected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DateRangeEventArgs) Handles mcdSapMainte.DateSelected
        'カーソルのある位置にデータを設定
        Me.vwSapMainte.Sheets(0).SetValue(dataHBKC0201.PropIntRowSelect, dataHBKC0201.PropIntColSelect, mcdSapMainte.SelectionStart)
        mcdSapMainte.Hide()
    End Sub

    ''' <summary>
    ''' サポセン機器メンテナンス一覧：ロストフォーカス時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>カレンダーは非表示とする
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwSapMainte_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles vwSapMainte.LostFocus
        mcdSapMainte.Hide()
    End Sub

    ''' <summary>
    ''' サポセン機器情報：[選択行を交換／解除]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択した機器の交換番号と作業備考をセットする
    ''' <para>作成情報：2012/09/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnExchange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExchange.Click

        '選択した機器の交換番号と作業備考をセットする
        If logicHBKC0201.DoExchangeMain(dataHBKC0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' サポセン機器情報：[選択行をセットにする]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択した機器同士をセットにする
    ''' <para>作成情報：2012/09/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSetPair_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetPair.Click

        '選択した機器同士をセットにする
        If logicHBKC0201.SetPairMain(dataHBKC0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' サポセン機器情報：[選択行を既存のセットまたは機器とセットにする]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択した機器を既存のセットまたは機器とセットにする
    ''' <para>作成情報：2012/09/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddPair_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddPair.Click

        '処理可能チェック
        If logicHBKC0201.CheckAddNewSetKikiEnableMain(dataHBKC0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        'セット選択画面インスタンス作成
        Dim HBKC0701 As New HBKC0701

        'セット選択画面表示
        HBKC0701.ShowDialog()

        '選択データを取得
        dataHBKC0201.PropDtResultSub = HBKC0701.dataHBKC0701.PropDtReturn

        'セットが選択されている場合、サポセン機器メンテナス一覧に選択されたセット機器を表示する
        If logicHBKC0201.AddNewSetKikiMain(dataHBKC0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' サポセン機器情報：[選択行のセットをバラす]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択した機器をセットからバラす
    ''' <para>作成情報：2012/09/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnCepalatePair_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCepalatePair.Click

        '選択した機器をセットからバラす
        If logicHBKC0201.CepalateFromPairMain(dataHBKC0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [貸出誓約書]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>出力形式選択画面を表示し、貸出誓約書出力ロジックを呼び出す
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnOutput_Kashidashi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOutput_Kashidashi.Click

        'インスタンス作成
        Dim dataHBKC0202 As New DataHBKC0202
        Dim logicHBKC0202 As New LogicHBKC0202

        '選択行番号取得
        Dim intTargetRow As Integer = dataHBKC0201.PropIntSelectedOutputSapRow

        'データクラスにパラメータ設定
        With dataHBKC0202
            .PropIntIncNmb = dataHBKC0201.PropIntINCNmb                                                     'インシデント番号
            .PropIntCINmb = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_CINMB).Value             'CI番号
            .PropIntRirekiNo = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_LASTUPRIREKINO).Value    '最終更新時履歴番号
            .PropStrKindNM = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_KINDNM).Value            '種別名
            .PropStrKikiNmb = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_NUM).Value               '番号
            .PropStrMaker = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_CLASS2).Value            '分類２（メーカー）
            .PropStrKisyuNM = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_CINM).Value              '名称（機種）
        End With

        If logicHBKC0202.InitMain(dataHBKC0202) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [期限更新誓約書]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>出力形式選択画面を表示し、期限更新誓約書出力ロジックを呼び出す
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnOutput_UpLimitDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOutput_UpLimitDate.Click

        'インスタンス作成
        Dim dataHBKC0203 As New DataHBKC0203
        Dim logicHBKC0203 As New LogicHBKC0203

        '選択行番号取得
        Dim intTargetRow As Integer = dataHBKC0201.PropIntSelectedOutputSapRow

        'データクラスにパラメータ設定
        With dataHBKC0203
            .PropIntIncNmb = dataHBKC0201.PropIntINCNmb                                                     'インシデント番号
            .PropIntCINmb = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_CINMB).Value             'CI番号
            .PropIntRirekiNo = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_LASTUPRIREKINO).Value    '最終更新時履歴番号                                                                        'タイトル
            .PropStrKindNM = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_KINDNM).Value            '種別名
            .PropStrKikiNmb = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_NUM).Value               '番号
            .PropStrMaker = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_CLASS2).Value            '分類２（メーカー）
            .PropStrKisyuNM = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_CINM).Value              '名称（機種）
        End With

        If logicHBKC0203.InitMain(dataHBKC0203) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [一時預託書]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>出力形式選択画面を表示し、一時預託書出力ロジックを呼び出す
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnOutput_Azukari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOutput_Azukari.Click

        'インスタンス作成
        Dim dataHBKC0204 As New DataHBKC0204
        Dim logicHBKC0204 As New LogicHBKC0204

        '選択行番号取得
        Dim intTargetRow As Integer = dataHBKC0201.PropIntSelectedOutputSapRow

        'データクラスにパラメータ設定
        With dataHBKC0204
            .PropIntIncNmb = dataHBKC0201.PropIntINCNmb                                                     'インシデント番号
            .PropIntCINmb = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_CINMB).Value             'CI番号
            .PropIntRirekiNo = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_LASTUPRIREKINO).Value    '最終更新時履歴番号                                                                          'タイトル
            .PropStrKindNM = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_KINDNM).Value            '種別名
            .PropStrKikiNmb = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_NUM).Value               '番号
            .PropStrMaker = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_CLASS2).Value            '分類２（メーカー）
            .PropStrKisyuNM = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_CINM).Value              '名称（機種）
        End With

        If logicHBKC0204.InitMain(dataHBKC0204) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [返却確認書]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>出力形式選択画面を表示し、返却確認書出力ロジックを呼び出す
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnOutput_Henkyaku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOutput_Henkyaku.Click

        'インスタンス作成
        Dim dataHBKC0205 As New DataHBKC0205
        Dim logicHBKC0205 As New LogicHBKC0205

        '選択行番号取得
        Dim intTargetRow As Integer = dataHBKC0201.PropIntSelectedOutputSapRow

        'データクラスにパラメータ設定
        With dataHBKC0205
            .PropIntIncNmb = dataHBKC0201.PropIntINCNmb                                                     'インシデント番号
            .PropIntCINmb = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_CINMB).Value             'CI番号
            .PropIntRirekiNo = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_LASTUPRIREKINO).Value    '最終更新時履歴番号                                                                         'タイトル
            .PropStrKindNM = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_KINDNM).Value            '種別名
            .PropStrKikiNmb = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_NUM).Value               '番号
            .PropStrMaker = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_CLASS2).Value            '分類２（メーカー）
            .PropStrKisyuNM = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_CINM).Value              '名称（機種）
        End With

        If logicHBKC0205.InitMain(dataHBKC0205) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [チェックシート]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>出力形式選択画面を表示し、チェックシート出力ロジックを呼び出す
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnOutput_Check_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOutput_Check.Click

        'インスタンス作成
        Dim dataHBKC0206 As New DataHBKC0206
        Dim logicHBKC0206 As New LogicHBKC0206

        '選択行番号取得
        Dim intTargetRow As Integer = dataHBKC0201.PropIntSelectedOutputSapRow

        'データクラスにパラメータ設定
        With dataHBKC0206
            .PropIntIncNmb = dataHBKC0201.PropIntINCNmb                                                     'インシデント番号
            .PropIntCINmb = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_CINMB).Value             'CI番号
            .PropIntRirekiNo = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_LASTUPRIREKINO).Value    '最終更新時履歴番号
            .PropStrTitle = Me.txtTitle.Text                                                                'タイトル
            .PropStrKindNM = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_KINDNM).Value            '種別名
            .PropStrKindCD = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_KINDCD).Value            '種別CD
            .PropStrKikiNmb = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_NUM).Value               '番号
            .PropStrMaker = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_CLASS2).Value            '分類２（メーカー）
            .PropStrKisyuNM = _
                Me.vwSapMainte.Sheets(0).Cells(intTargetRow, logicHBKC0201.COL_SAP_CINM).Value              '名称（機種）
        End With

        If logicHBKC0206.InitMain(dataHBKC0206) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' プロセスリンク一覧：セルダブルクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択した管理番号の登録画面へ参照モードで遷移する
    ''' <para>作成情報：2012/08/09 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwProcessLinkInfo_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwProcessLinkInfo.CellDoubleClick

        '列ヘッダーがクリックされた場合は処理しない
        If e.ColumnHeader = True Or e.RowHeader = True Then
            Exit Sub
        End If

        '変数宣言
        Dim strSelectKbn As String = _
            Me.vwProcessLinkInfo.Sheets(0).Cells(e.Row, logicHBKC0201.COL_processLINK_KBN).Value  '選択行の区分（コード）
        Dim strSelectNo As String = _
            Me.vwProcessLinkInfo.Sheets(0).Cells(e.Row, logicHBKC0201.COL_processLINK_NO).Value   '選択行の管理番号

        '関係者チェック_結果格納用
        Dim intchkkankei As Integer = 0

        'プロセスリンク対応関係者チェック処理(dataHBKC0201.PropintChkKankei) 
        If logicHBKC0201.PlinkKankeiCheckMain(intchkkankei, strSelectNo, strSelectKbn) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        '関係者でない場合エラーメッセージを表示
        If intchkkankei = KANKEI_CHECK_NONE Then
            'エラーメッセージ設定
            puErrMsg = C0201_E036
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        '区分に応じた登録画面へ参照モードで遷移する
        If strSelectKbn = PROCESS_TYPE_INCIDENT Then        '区分がインシデントの場合
            'インシデント登録画面インスタンス作成
            Dim HBKC0201 As New HBKC0201
            'インシデント登録画面データクラスにパラメータをセット
            With HBKC0201.dataHBKC0201
                .PropStrProcMode = PROCMODE_REF             '処理モード：参照
                .PropIntINCNmb = strSelectNo                'インシデント番号：管理番号
            End With
            '画面遷移
            Me.Hide()
            HBKC0201.ShowDialog()
            Me.Show()

        ElseIf strSelectKbn = PROCESS_TYPE_QUESTION Then

            '*********************************
            '* 区分が問題の場合
            '*********************************

            '問題登録画面インスタンス作成
            Dim HBKD0201 As New HBKD0201
            '問題登録画面データクラスにパラメータをセット
            With HBKD0201.dataHBKD0201
                .PropStrProcMode = PROCMODE_REF             '処理モード：参照
                .PropIntPrbNmb = strSelectNo                '管理番号
            End With
            '画面遷移
            Me.Hide()
            HBKD0201.ShowDialog()
            Me.Show()

        ElseIf strSelectKbn = PROCESS_TYPE_CHANGE Then

            '*********************************
            '* 区分が変更の場合
            '*********************************

            '変更登録画面インスタンス作成
            Dim HBKE0201 As New HBKE0201
            '変更登録画面データクラスにパラメータをセット
            With HBKE0201.dataHBKE0201
                .PropStrProcMode = PROCMODE_REF             '処理モード：参照
                .PropIntChgNmb = strSelectNo                '管理番号
            End With
            '画面遷移
            Me.Hide()
            HBKE0201.ShowDialog()
            Me.Show()


        ElseIf strSelectKbn = PROCESS_TYPE_RELEASE Then

            '*********************************
            '* 区分がリリースの場合
            '*********************************

            'リリース登録画面インスタンス作成
            Dim HBKF0201 As New HBKF0201
            'リリース登録画面データクラスにパラメータをセット
            With HBKF0201.dataHBKF0201
                .PropStrProcMode = PROCMODE_REF             '処理モード：参照
                .PropIntRelNmb = strSelectNo                '管理番号
            End With
            '画面遷移
            Me.Hide()
            HBKF0201.ShowDialog()
            Me.Show()

            'MsgBox("リリース登録画面に遷移します")

        End If

    End Sub

    ''' <summary>
    ''' 会議一覧：セルダブルクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択した管理番号の登録画面へ参照モードで遷移する
    ''' <para>作成情報：2012/08/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwMeeting_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwMeeting.CellDoubleClick

        '列ヘッダーがクリックされた場合は処理しない
        'また、履歴モード時も処理しない
        If e.ColumnHeader = True Or e.RowHeader = True Or dataHBKC0201.PropStrProcMode = PROCMODE_RIREKI Then
            Exit Sub
        End If

        '変数宣言
        Dim strSelectKbn As String = _
            Me.vwMeeting.Sheets(0).Cells(e.Row, logicHBKC0201.COL_MEETING_NINCD).Value  '選択行の区分（コード）
        Dim strSelectNo As String = _
            Me.vwMeeting.Sheets(0).Cells(e.Row, logicHBKC0201.COL_MEETING_NO).Value     '選択行の管理番号

        '会議記録登録
        Dim HBKC0401 As New HBKC0401

        '会議記録登録画面データクラスに対しプロパティ設定
        With HBKC0401.dataHBKC0401
            .PropBlnTranFlg = 0                             'メニュー遷移フラグ（0:メニュー以外から遷移、1:メニューから遷移）
            .PropProcessKbn = PROCESS_TYPE_INCIDENT         'プロセス区分
            .PropProcessNmb = dataHBKC0201.PropIntINCNmb    'プロセス番号
            .PropStrProcMode = PROCMODE_EDIT                '処理モード：編集モード
            .PropIntMeetingNmb = strSelectNo                '会議番号
        End With

        'クローズ処理の追加
        AddHandler CType(HBKC0401, Form).FormClosed, AddressOf Meeting_FormClosed

        '当画面非表示
        Me.Hide()
        '会議記録登録画面表示
        HBKC0401.ShowDialog()
        '当画面表示
        Me.Show()

    End Sub

    ''' <summary>
    ''' 機器一覧：セルダブルクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択した管理番号の登録画面へ参照モードで遷移する
    ''' <para>作成情報：2012/09/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwKikiInfo_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwKikiInfo.CellDoubleClick

        'アクティブ制御用
        e.Cancel = True

        '列ヘッダーがクリックされた場合は処理しない
        'また、履歴モード時も処理しない
        If e.ColumnHeader = True Or e.RowHeader = True Or dataHBKC0201.PropStrProcMode = PROCMODE_RIREKI Then
            Exit Sub
        End If

        If Me.vwKikiInfo.Sheets(0).Cells(e.Row, logicHBKC0201.COL_KIKI_CIKBNCD).Value = CI_TYPE_KIKI Then

            '部所有機器登録画面へ参照モードで遷移する
            dataHBKC0201.PropfrmInstance = New HBKB1301

            With dataHBKC0201.PropfrmInstance.dataHBKB1301
                '★参照モードがない！
                .PropStrProcMode = PROCMODE_REF                                                                         '処理モード：参照モード
                .PropIntCINmb = _
                    Me.vwKikiInfo.Sheets(0).Cells(e.Row, logicHBKC0201.COL_KIKI_CINMB).Value   '選択行のCI番号
                .PropBlnBeLockedFlg = True
            End With

        Else
            'サポセン機器登録画面へ参照モードで遷移する
            dataHBKC0201.PropfrmInstance = New HBKB0601

            With dataHBKC0201.PropfrmInstance.dataHBKB0601
                .PropStrProcMode = PROCMODE_REF                                                                         '処理モード：参照モード
                '.PropIntIncNmb = Integer.Parse(Me.txtIncCD.Text)                                                        'インシデント番号(編集時ロック用のため参照は不要）
                .PropIntWorkNmb = _
                    Me.vwKikiInfo.Sheets(0).Cells(e.Row, logicHBKC0201.COL_KIKI_NMB).Value     '選択行の管理番号
                .PropIntCINmb = _
                    Me.vwKikiInfo.Sheets(0).Cells(e.Row, logicHBKC0201.COL_KIKI_CINMB).Value   '選択行のCI番号
                .PropStrWorkCD = _
                    Me.vwKikiInfo.Sheets(0).Cells(e.Row, logicHBKC0201.COL_KIKI_CIKBNCD).Value  '選択行の区分
            End With

        End If

        'クローズ処理の追加
        AddHandler CType(dataHBKC0201.PropfrmInstance, Form).FormClosed, AddressOf frmkiki_FormClosed

        '画面制御開始
        Scr_Enabled_Start()

        '画面画面表示
        dataHBKC0201.PropfrmInstance.Show()




    End Sub

    ''' <summary>
    ''' [機器一覧の格詳細画面のフォーム]クローズ後の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>閉じた後画面制御終了処理を行う。
    ''' <para>作成情報：2012/09/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub frmkiki_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs)
        '画面制御終了
        Scr_Enabled_End()

    End Sub

    ''' <summary>
    ''' 共通_画面制御処理_別画面表示前処理
    ''' </summary>
    ''' <remarks>ボタン非活性など画面制御を行う。
    ''' <para>作成情報：2012/09/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub Scr_Enabled_Start()
        '親画面のすべてのボタンを非活性とする
        dataHBKC0201.PropAryfrmCtlList = New ArrayList
        SetButonEnabledFalse(Me.Controls, dataHBKC0201.PropAryfrmCtlList)

        'イベント停止
        RemoveHandler vwMeeting.CellDoubleClick, AddressOf vwMeeting_CellDoubleClick
        RemoveHandler vwProcessLinkInfo.CellDoubleClick, AddressOf vwProcessLinkInfo_CellDoubleClick
        RemoveHandler vwKikiInfo.CellDoubleClick, AddressOf vwKikiInfo_CellDoubleClick
        RemoveHandler vwIncRireki.ButtonClicked, AddressOf vwIncRireki_ButtonClicked                    '担当者
        cmbWork.Enabled = False                                                                         '作業コンボボックス
        vwSapMainte.Enabled = False


    End Sub

    ''' <summary>
    ''' 共通_画面制御処理_別画面表示終了後処理
    ''' </summary>
    ''' <remarks>画面制御を元に戻す
    ''' <para>作成情報：2012/09/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub Scr_Enabled_End()

        '別画面破棄(FromClosingのチェックで使用）
        dataHBKC0201.PropfrmInstance = Nothing

        '親画面の非活性の状態を戻す
        SetButonEnabledTrue(Me.Controls, dataHBKC0201.PropAryfrmCtlList)
        dataHBKC0201.PropAryfrmCtlList = Nothing

        'イベント復活
        AddHandler vwMeeting.CellDoubleClick, AddressOf vwMeeting_CellDoubleClick
        AddHandler vwProcessLinkInfo.CellDoubleClick, AddressOf vwProcessLinkInfo_CellDoubleClick
        AddHandler vwKikiInfo.CellDoubleClick, AddressOf vwKikiInfo_CellDoubleClick
        AddHandler vwIncRireki.ButtonClicked, AddressOf vwIncRireki_ButtonClicked

        '[Mod] 2012/10/10 s.yamaguchi 作業コンボボックス活性、非活性制御追加 START
        'モードによって活性、非活性を設定しなおす
        If dataHBKC0201.PropStrProcMode = PROCMODE_EDIT Then
            cmbWork.Enabled = True
        Else
            cmbWork.Enabled = False
        End If
        '[Mod] 2012/10/10 s.yamaguchi 作業コンボボックス活性、非活性制御追加 END
        vwSapMainte.Enabled = True

    End Sub



    ''' <summary>
    ''' 最新連携情報表示画面
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>連携処理実施処
    ''' <para>作成情報：2012/09/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSMShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSMShow.Click
        '最新連携情報表示画面
        Dim HBKC0210 As New HBKC0210

        '最新連携情報表示画面データクラスに対しプロパティ設定
        HBKC0210.dataHBKC0210.PropIntINCNmb = dataHBKC0201.PropIntINCNmb

        HBKC0210.Show()

    End Sub

    ''' <summary>
    ''' 連携処理実施処理呼び出し
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>連携処理実施処
    ''' <para>作成情報：2012/09/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSMRenkei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSMRenkei.Click

        '[Add] 2012/10/02 s.yamaguchi 連携処理中メッセージ出力タイミング修正対応 START
        'インスタンス作成
        Dim dataHBKC0211 As New DataHBKC0211
        Dim logicHBKC0211 As New LogicHBKC0211

        'データクラスにパラメータ設定
        dataHBKC0211.PropIntINCNmb = dataHBKC0201.PropIntINCNmb

        '連携待ちチェック処理
        If logicHBKC0211.IncidentSMrenkeiCheckMain(dataHBKC0211) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If
        '[Add] 2012/10/02 s.yamaguchi 連携処理中メッセージ出力タイミング修正対応 End

        'ServiceManagerにインシデント情報を連携します。宜しいですか？でYesを選んだ場合
        If MsgBox(String.Format(C0201_W002), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.Yes Then

            '[Del] 2012/10/02 s.yamaguchi 連携処理中メッセージ出力タイミング修正対応 START
            ''インスタンス作成
            'Dim dataHBKC0211 As New DataHBKC0211
            'Dim logicHBKC0211 As New LogicHBKC0211

            ''データクラスにパラメータ設定
            'dataHBKC0211.PropIntINCNmb = dataHBKC0201.PropIntINCNmb
            '[Del] 2012/10/02 s.yamaguchi 連携処理中メッセージ出力タイミング修正対応 End

            '連携処理実施処理呼び出し
            If logicHBKC0211.InitMain(dataHBKC0211) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

        End If
    End Sub

    ''' <summary>
    ''' インタバール経過後の処理の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>当画面を閉じて呼び出し元画面へ遷移する
    ''' <para>作成情報：2012/09/24 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timKanryo.Tick
        '登録完了ラベルを初期化する 
        dataHBKC0201.PropLblkanryoMsg.Text = ""

        'タイマーを停止する
        Me.timKanryo.Stop()

    End Sub

    ''' <summary>
    ''' 受付内容フォーカス遷移後の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>フォーカス遷移を行った時に入力チェックをする
    ''' <para>作成情報：2012/10/24 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub txtUkeNaiyo_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtUkeNaiyo.Validating


        With dataHBKC0201
            If txtUkeNaiyo.Text.Length > 3000 Then

                .PropStrLostFucs = .PropTxtUkeNaiyo.Text

                '桁数チェック
                If logicHBKC0201.CheckLostFocus(dataHBKC0201) = False Then
                    'フォーカス移動キャンセル
                    e.Cancel = True
                    '[DELETE]2013/03/21 t.fukuo 閾値超過の場合文字削除しないよう修正 START
                    ''3000文字以上の場合、先頭から3000文字だけ切り取って入れなおす
                    '.PropTxtUkeNaiyo.Text = .PropStrLostFucs.ToString.Substring(0, 3000)
                    '[DELETE]2013/03/21 t.fukuo 閾値超過の場合文字削除しないよう修正 END
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Exclamation, TITLE_INFO)
                    '処理終了
                    Exit Sub
                End If

            End If
        End With

    End Sub

    ''' <summary>
    ''' 対応結果フォーカス遷移後の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>フォーカス遷移を行った時に入力チェックをする
    ''' <para>作成情報：2012/10/24 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub txtTaioKekka_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtTaioKekka.Validating

        With dataHBKC0201

            If .PropTxtTaioKekka.Text.Length > 3000 Then

                .PropStrLostFucs = .PropTxtTaioKekka.Text

                '桁数チェック
                If logicHBKC0201.CheckLostFocus(dataHBKC0201) = False Then
                    'フォーカス移動キャンセル
                    e.Cancel = True
                    '[DELETE]2013/03/21 t.fukuo 閾値超過の場合文字削除しないよう修正 START
                    ''3000文字以上の場合、先頭から3000文字だけ切り取って入れなおす
                    '.PropTxtTaioKekka.Text = .PropStrLostFucs.ToString.Substring(0, 3000)
                    '[DELETE]2013/03/21 t.fukuo 閾値超過の場合文字削除しないよう修正 END
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Exclamation, TITLE_INFO)
                    '処理終了
                    Exit Sub
                End If

            End If
        End With

    End Sub

    ''' <summary>
    ''' 作業内容セルフォーカス遷移後の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>フォーカス遷移を行った時に入力チェックをする
    ''' <para>作成情報：2012/10/24 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub vwIncRireki_LeaveCell(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs) Handles vwIncRireki.LeaveCell

        With dataHBKC0201

            '作業内容セルからフォーカスが離れた時
            If e.Column = logicHBKC0201.COL_RIREKI_NAIYOU Then

                '桁数チェック
                If logicHBKC0201.CheckLostFocusSpread(dataHBKC0201) = False Then
                    'フォーカス移動キャンセル
                    e.Cancel = True
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Exclamation, TITLE_INFO)
                    '処理終了
                    Exit Sub
                End If

            End If

        End With
    End Sub

    ''' <summary>
    ''' スプレッドフォーカス遷移後の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>フォーカス遷移を行った時に入力チェックをする
    ''' <para>作成情報：2012/10/24 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub vwIncRireki_validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles vwIncRireki.Validating

        '桁数チェック
        If logicHBKC0201.CheckLostFocusSpread(dataHBKC0201) = False Then
            'フォーカス移動キャンセル
            e.Cancel = True
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Exclamation, TITLE_INFO)
            '処理終了
            Exit Sub
        End If
    End Sub

End Class