Imports Common
Imports CommonHBK
Imports FarPoint.Win.Spread

''' <summary>
''' インシデント登録画面Dataクラス
''' </summary>
''' <remarks>インシデント登録画面で使用するのプロパティセットを行う
''' <para>作成情報：2012/07/13 r.hoshino
''' <p>改訂情報:2012/07/19 r.hoshino</p>
''' </para></remarks>
Public Class DataHBKC0201


    '前画面からのパラメータ
    Private ppStrProcMode As String                     '前画面パラメータ：処理モード（1：新規登録、2：編集、3：参照、4：作業履歴）
    Private ppIntINCNmb As Integer                      '前画面パラメータ：インシデント番号 ※新規モード時には新規インシデント番号がセットされる
    Private ppIntRirekiNo As Integer                    '前画面パラメータ：履歴番号  
    Private ppIntMeetingNmb As Integer                  '前画面パラメータ：会議番号
    Private ppStrEdiTime As String                      'ロック解除判定用パラメータ：編集開始日時
    Private ppIntOwner As Integer                       '前画面パラメータ：呼び元画面(1:変更検索一覧,0:それ以外)
    Private ppfrmInstance As Object                     '別画面制御：呼び先画面
    Private ppAryfrmCtlList As ArrayList                '別画面制御：非活性対象コントロールリスト
    Private ppIntChkKankei As Integer                   '関係者チェック結果：（0:参照不可,1:参照のみ関係者,2:編集できる関係者）

    'フォームオブジェクト
    Private ppGrpLoginUser As GroupControlEx            'ログイン：ログイン情報グループボックス

    Private ppGrpIncCD As GroupBox                      'ヘッダ：インシデント管理グループボックス
    Private ppTxtIncCD As TextBox                       'ヘッダ：インシデント番号
    Private ppLblRegInfo As Label                       'ヘッダ：登録者ラベル
    Private ppLblUpdateInfo As Label                    'ヘッダ：最終更新者ラベル
    Private ppLblRegInfo_out As Label                   'ヘッダ：登録者出力用ラベル
    Private ppLblUpdateInfo_out As Label                'ヘッダ：最終更新者出力用ラベル
    Private ppLblkanryoMsg As Label                     'ヘッダ：完了メッセージ

    Private ppTbInput As TabControl                     'タブ
    Private ppCmbUkeKbn As ComboBox                     '基本情報：受付手段コンボボックス
    Private ppDtpHasseiDT As DateTimePickerEx           '基本情報：発生日時
    Private ppTxtHasseiDT_HM As TextBoxEx_IoTime        '基本情報：発生日時時分表示テキストボックス
    Private ppBtnHasseiDT_HM As Button                  '基本情報：発生日時（時間入力）ボタン
    Private ppCmbIncKbnCD As ComboBox                   '基本情報：インシデント種別コンボボックス
    Private ppCmbprocessStateCD As ComboBox             '基本情報：ステータスコンボボックス
    Private ppCmbDomainCD As ComboBox                   '基本情報：ドメインコンボボックス
    Private ppCmbSystemNmb As ComboBoxEx                '基本情報：対象システム階層表示コンボボックス
    '[ADD] 2012/10/24 s.yamaguchi START
    Private ppBtnSearchTaisyouSystem As Button          '基本情報：対象システム検索ボタン
    '[ADD] 2012/10/24 s.yamaguchi END
    Private ppBtnKnowHow As Button                      '基本情報：ノウハウボタン
    Private ppTxtOutSideToolNmb As TextBox              '基本情報：外部ツール番号テキストボックス
    Private ppChkShijisyoFlg As CheckBox                '基本情報：指示書チェックボックス

    Private ppTxtTitle As TextBox                       '基本情報：タイトルテキストボックス
    Private ppTbNaiyo As TabControl                     'タブ
    Private ppTxtUkeNaiyo As TextBox                    '基本情報：受付内容テキストボックス
    Private ppTxtPriority As TextBox                    '基本情報：重要度テキストボックス
    Private ppTxtErrlevel As TextBox                    '基本情報：障害レベルテキストボックス
    Private ppTxtEventID As TextBox                     '基本情報：イベントIDテキストボックス
    Private ppTxtSource As TextBox                      '基本情報：ソーステキストボックス
    Private ppTxtOPCEventID As TextBox                  '基本情報：OPCイベントIDテキストボックス
    Private ppTxtEventClass As TextBox                  '基本情報：イベントクラステキストボックス
    Private ppTxtTaioKekka As TextBox                   '基本情報：対応結果テキストボックス
    Private ppDtpKaitoDT As DateTimePickerEx            '基本情報：回答日時
    Private ppTxtKaitoDT_HM As TextBoxEx_IoTime         '基本情報：回答日時時分表示テキストボックス
    Private ppBtnKaitoDT_HM As Button                   '基本情報：回答日時（時間入力）ボタン
    Private ppDtpKanryoDT As DateTimePickerEx           '基本情報：完了日時
    Private ppTxtKanryoDT_HM As TextBoxEx_IoTime        '基本情報：完了日時時分表示テキストボックス
    Private ppBtnKanryoDT_HM As Button                  '基本情報：完了日時（時間入力）ボタン

    Private ppTxtPartnerID As TextBox                   '基本情報：相手IDテキストボックス
    Private ppTxtPartnerNM As TextBox                   '基本情報：相手氏名テキストボックス
    Private ppBtnPartnerSearch As Button                '基本情報：相手検索ボタン
    Private ppTxtPartnerKana As TextBox                 '基本情報：相手シメイテキストボックス
    Private ppTxtPartnerCompany As TextBox              '基本情報：相手会社テキストボックス
    Private ppTxtPartnerKyokuNM As TextBox              '基本情報：相手局テキストボックス
    Private ppTxtPartnerBusyoNM As TextBox              '基本情報：相手部署テキストボックス
    Private ppTxtPartnerTel As TextBox                  '基本情報：相手電話番号テキストボックス
    Private ppTxtPartnerMailAdd As TextBox              '基本情報：相手メールアドレステキストボックス
    Private ppTxtPartnerContact As TextBox              '基本情報：相手連絡先テキストボックス
    Private ppTxtPartnerBase As TextBox                 '基本情報：相手拠点テキストボックス
    Private ppTxtPartnerRoom As TextBox                 '基本情報：相手番組・部屋テキストボックス
    Private ppTxtKengen As TextBox                      '基本情報：権限テキストボックス
    Private ppTxtRentalKiki As TextBox                  '基本情報：借用物テキストボックス
    Private ppBtnRentalKiki As Button                   '基本情報：取得ボタン

    Private ppCmbTantoGrpCD As ComboBox                 '基本情報：担当グループコンボボックス
    Private ppTxtIncTantoCD As TextBox                  '基本情報：担当IDテキストボックス
    Private ppBtnIncTantoMY As Button                   '基本情報：担当私ボタン
    Private ppTxtIncTantoNM As TextBox                  '基本情報：担当氏名テキストボックス
    Private ppBtnIncTantoSearch As Button               '基本情報：担当検索ボタン

    Private ppVwkikiInfo As FpSpread                    '基本情報：機器情報スプレッド
    Private ppBtnAddRow_kiki As Button                  '基本情報：機器情報行追加ボタン
    Private ppBtnRemoveRow_kiki As Button               '基本情報：機器情報行削除ボタン
    Private ppBtnWeb As Button                          '基本情報：機器情報Webボタン
    Private ppBtnSSCM As Button                         '基本情報：機器情報SSCMボタン
    Private ppBtnEnkaku As Button                       '基本情報：機器情報遠隔ボタン

    Private ppVwIncRireki As FpSpread                   '基本情報：作業履歴スプレッド

    Private ppCmbSpdkeika As CellType.ComboBoxCellType  '経過種別データ(combobox)
    Private ppCmbSpdsystem As CellType.MultiColumnComboBoxCellType '対象システムデータ(combobox)
    Private ppBtnSpdTanto As CellType.ButtonCellType    '担当者ボタン(button)
    Private ppBtnSpdkaishiji As CellType.ButtonCellType '開始時間ボタン(button)
    Private ppBtnSpdyoteiji As CellType.ButtonCellType  '予定時間ボタン(button)
    Private ppBtnSpdsyuryoji As CellType.ButtonCellType '終了時間ボタン(button)

    Private ppBtnAddRow_rireki As Button                '基本情報：作業履歴行追加ボタン
    Private ppBtnRemoveRow_rireki As Button             '基本情報：作業履歴行削除ボタン
    Private ppBtnkakudai As Button                      '基本情報：作業履歴拡大ボタン
    Private ppBtnRefresh As Button                      '基本情報：作業履歴リフレッシュボタン
    Private ppMcdRireki As MonthCalendar                '基本情報：作業履歴カレンダー

    '【ADD】2012/07/25 t.fukuo　サポセン機器情報タブ機能作成：START
    Private ppTxtPartnerID_Sap As TextBox               'サポセン機器情報：相手IDテキストボックス
    Private ppTxtPartnerNM_Sap As TextBox               'サポセン機器情報：相手氏名テキストボックス
    Private ppTxtPartnerKana_Sap As TextBox             'サポセン機器情報：相手シメイテキストボックス
    Private ppTxtPartnerCompany_Sap As TextBox          'サポセン機器情報：相手会社テキストボックス
    Private ppTxtPartnerKyokuNM_Sap As TextBox          'サポセン機器情報：相手局テキストボックス
    Private ppTxtPartnerBusyoNM_Sap As TextBox          'サポセン機器情報：相手部署テキストボックス
    Private ppTxtPartnerTel_Sap As TextBox              'サポセン機器情報：相手電話番号テキストボックス
    Private ppTxtPartnerMailAdd_Sap As TextBox          'サポセン機器情報：相手メールアドレステキストボックス
    Private ppTxtPartnerContact_Sap As TextBox          'サポセン機器情報：相手連絡先テキストボックス
    Private ppTxtPartnerBase_Sap As TextBox             'サポセン機器情報：相手拠点テキストボックス
    Private ppTxtPartnerRoom_Sap As TextBox             'サポセン機器情報：相手番組／部屋テキストボックス
    Private ppCmbWork As ComboBox                       'サポセン機器情報：作業コンボボックス
    Private ppBtnAddRow_SapMainte As Button             'サポセン機器情報：作業追加ボタン
    Private ppVwSapMainte As FpSpread                   'サポセン機器情報：サポセン機器メンテナンススプレッド
    Private ppBtnExchange As Button                     'サポセン機器情報：選択行を交換／解除ボタン
    Private ppBtnSetPair As Button                      'サポセン機器情報：選択行をセットにするボタン
    Private ppBtnAddPair As Button                      'サポセン機器情報：選択行を既存のセットまたは機器とセットにするボタン
    Private ppBtnCepalatePair As Button                 'サポセン機器情報：選択行をセットをバラすボタン
    Private ppBtnOutput_Kashidashi As Button            'サポセン機器情報：貸出誓約書出力ボタン
    Private ppBtnOutput_UpLimitDate As Button           'サポセン機器情報：期限更新誓約書出力ボタン
    Private ppBtnOutput_Azukari As Button               'サポセン機器情報：預かり確認書出力ボタン
    Private ppBtnOutput_Henkyaku As Button              'サポセン機器情報：返却確認書出力ボタン
    Private ppBtnOutput_Check As Button                 'サポセン機器情報：チェックシート出力ボタン
    Private ppMcdSapMainte As MonthCalendar             'サポセン機器情報：サポセン機器メンテナンスカレンダー
    '【ADD】2012/07/25 t.fukuo　サポセン機器情報タブ機能作成：END

    Private ppVwMeeting As FpSpread                     '会議情報：会議情報スプレッド
    Private ppBtnAddRow_meeting As Button               '会議情報：会議情報行追加ボタン
    Private ppBtnRemoveRow_meeting As Button            '会議情報：会議情報行削除ボタン

    Private ppTxtBIko1 As TextBox                       'フリー入力情報：テキスト１テキストボックス
    Private ppTxtBIko2 As TextBox                       'フリー入力情報：テキスト２テキストボックス
    Private ppTxtBIko3 As TextBox                       'フリー入力情報：テキスト３テキストボックス
    Private ppTxtBIko4 As TextBox                       'フリー入力情報：テキスト４テキストボックス
    Private ppTxtBIko5 As TextBox                       'フリー入力情報：テキスト５テキストボックス
    Private ppChkFreeFlg1 As CheckBox                   'フリー入力情報：フリーフラグ１チェックボックス
    Private ppChkFreeFlg2 As CheckBox                   'フリー入力情報：フリーフラグ２チェックボックス
    Private ppChkFreeFlg3 As CheckBox                   'フリー入力情報：フリーフラグ３チェックボックス
    Private ppChkFreeFlg4 As CheckBox                   'フリー入力情報：フリーフラグ４チェックボックス
    Private ppChkFreeFlg5 As CheckBox                   'フリー入力情報：フリーフラグ５チェックボックス

    'フッター共通
    Private ppVwRelation As FpSpread                    'フッタ：関係者情報スプレッド
    Private ppBtnAddRow_Grp As Button                   'フッタ：グループ行追加ボタン
    Private ppBtnAddRow_Usr As Button                   'フッタ：ユーザー行追加ボタン
    Private ppBtnRemoveRow_Relation As Button           'フッタ：関係者情報行削除ボタン

    Private ppTxtGrpHistory As TextBox                  'フッタ：グループ履歴
    Private ppTxtTantoHistory As TextBox                'フッタ：担当者履歴

    Private ppVwprocessLinkInfo As FpSpread             'フッタ：プロセスリンクスプレッド
    Private ppBtnAddRow_plink As Button                 'フッタ：プロセスリンク行追加ボタン
    Private ppBtnRemoveRow_plink As Button              'フッタ：プロセスリンク行削除ボタン

    Private ppVwFileInfo As FpSpread                    'フッタ：ファイル関連スプレッド
    Private ppBtnAddRow_File As Button                  'フッタ：ファイル関連行追加ボタン
    Private ppBtnRemoveRow_File As Button               'フッタ：ファイル関連行削除ボタン
    Private ppBtnOpenFile As Button                     'フッタ：ファイル関連開ボタン
    Private ppBtnSaveFile As Button                     'フッタ：ファイル関連ダボタン

    Private ppBtnReg As Button                          'フッタ：登録ボタン
    Private ppBtnCopy As Button                         'フッタ：複製ボタン
    Private ppBtnMail As Button                         'フッタ：メール作成ボタン
    Private ppBtnMondai As Button                       'フッタ：問題登録ボタン
    Private ppBtnPrint As Button                        'フッタ：単票出力ボタン
    Private ppBtnBack As Button                         'フッタ：戻るボタン

    'データ
    Private ppTxtkigencondcikbncd As String             '期限切れ条件CI種別
    Private ppTxtkigencondtypekbn As String             '期限切れ条件タイプ
    Private ppTxtkigencondkigen As String               '期限切れ条件期限
    Private ppTxtKigenCondUsrID As String               '期限切れ条件ユーザーID
    'メール用その２（ラベル分解）
    Private ppTxtRegGp As String                        '登録グループ名
    Private ppTxtRegUsr As String                       '登録ユーザー名    
    Private ppTxtRegDT As String                        '登録日時
    Private ppTxtUpdateGp As String                     '最終更新グループ名
    Private ppTxtUpdateUsr As String                    '最終更新ユーザー名
    Private ppTxtUpdateDT As String                     '最終更新日時
    Private ppblnKakudaiFlg As Boolean                  '拡大ボタン判定フラグ (False 通常,True 拡大状態)


    Private ppDtUketsukeMasta As DataTable              'コンボボックス用：受付手段マスタデータ
    Private ppDtKindMasta As DataTable                  'コンボボックス用：INC種別マスタデータ
    Private ppDtprocessStatusMasta As DataTable         'コンボボックス用：プロセスステータスマスタデータ
    Private ppDtDomeinMasta As DataTable                'コンボボックス用：ドメインマスタデータ
    Private ppDtSystemMasta As DataTable                'コンボボックス用：対象システムマスタデータ
    Private ppDtTantGrpMasta As DataTable               'コンボボックス用：担当グループマスタデータ
    Private ppDtKeikaMasta As DataTable                 'コンボボックス用：経過種別マスタデータ
    '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：START
    Private ppDtWorkMasta As DataTable                  'コンボボックス用：作業マスタデータ
    '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：END

    Private ppDtINCInfo As DataTable                    'メイン表示用：INC共通情報
    Private ppDtTantoRireki As DataTable                '担当履歴情報

    Private ppDtINCLock As DataTable                    'ロック情報：INC共通情報ロックデータ

    Private ppDtINCkiki As DataTable                    'スプレッド表示用：機器情報データ

    Private ppDtwkRireki As DataTable                   'スプレッド表示用：作業履歴データ
    Private ppDtINCRireki As DataTable                  'データ取得用：作業履歴データ
    Private ppDtINCTanto As DataTable                   'データ取得用：作業担当データ

    Private ppDtRelation As DataTable                   'スプレッド表示用：対応関係者情報データ
    Private ppDtprocessLink As DataTable                'スプレッド表示用：プロセスリンク管理番号データ
    Private ppDtFileInfo As DataTable                   'スプレッド表示用：関連ファイルデータ

    '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：START
    Private ppDtSapMainte As DataTable                  'スプレッド表示用：サポセン機器メンテナンスデータ
    Private ppDtTmp As DataTable                        '入力チェック用：一時保存データ
    '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：END

    Private ppDtMeeting As DataTable                    'スプレッド表示用：会議情報データ

    Private ppRowReg As DataRow                         'データ登録／更新用：登録／更新行

    Private ppIntRowSelect As Integer                   'スプレッド制御用：選択ROW_index
    Private ppIntColSelect As Integer                   'スプレッド制御用：選択Columns_index
    Private ppIntVwRirekiRowHeight As Integer           'スプレッド行の高さ

    'メッセージ
    Private ppStrBeLockedMsg As String                  'メッセージ：ロック画面表示時
    Private ppStrBeUnlockedMsg As String                'メッセージ：ロック解除時

    '別画面からの戻り値
    Private ppDtResultSub As DataTable                  'サブ検索戻り値：相手先、ユーザー、プロセスリンク、対応関係者、機器情報、会議情報
    Private ppDtResultkiki As DataTable                 '取得戻り値：機器情報項目用
    Private ppDtResultMtg As DataTable                  '取得戻り値：会議結果項目用
    Private ppTxtFileNaiyo As String                    'サブ検索戻り値：関連ファイル
    Private ppTxtFilePath As String                     'サブ検索戻り値：関連ファイル

    'ロック状況
    Private ppBlnBeLockedFlg As Boolean = False         'ロックフラグ（True：ロック／ロック解除されていない、False：ロック／ロック解除されていない）

    'システムエラー対応
    Private ppAryTsxCtlList As ArrayList                'トランザクション系コントロールリスト

    'その他
    Private ppDtmSysDate As DateTime                    'サーバー日付
    Private ppIntLogNo As Integer                       'ログNo
    Private ppIntLogNoSub As Integer                    'ログNo（会議用）
    Private ppStrSeaKey As String                       '汎用：検索キー(遠隔ボタン時のID,相手IDのEnter時のID,担当IDのEnter時のID,機器情報取得時のCI)
    Private ppStrLostFucs As String                     'ロストフォーカス時値保存用プロパティ

    'ファンクション用パラメータ
    Private ppIntSelectedRow As Integer                 '選択中の行番号
    Private ppStrSelectedFilePath As String             '選択中の会議ファイルパス
    Private ppRowTmp As DataRow                         '一時保存用データ行

    '【ADD】2012/07/28 t.fukuo　サポセン機器情報タブ機能作成：START
    Private ppIntExchangeKbn As Integer                 '交換／交換解除区分
    Private ppAryIntExchangePairIdx As ArrayList        '交換／交換解除行番号配列
    Private ppBlnExchangeEnable As Boolean              '交換／交換解除可否フラグ（True:可,False:不可）
    Private ppStrPlmCIStatusCD As String                '機器検索一覧画面へのパラメータ：CIステータスコード
    Private ppIntCIRirekiNo As Integer                  '更新値：CI履歴番号
    Private ppStrUpdCIStatusCD As String                '更新値：CIステータスコード
    Private ppStrUpdWorkKbnCD As String                 '更新値：作業区分コード
    Private ppBlnClearImageNmb As Boolean               '更新条件：イメージ番号クリアフラグ
    Private ppBlnClearSapData As Boolean                '更新条件：サポセンデータクリアフラグ
    Private ppIntExchangeCINmb As Integer               '更新条件：交換CI番号
    Private ppIntExchangeLastUpRirekiNo As Integer      '更新条件：交換最終更新履歴No
    Private ppIntExchangeWorkNmb As Integer             '更新条件：交換作業番号
    Private ppStrExchangeSetKikiID As String            '更新条件：交換セット機器番号（種別CD＋番号）
    Private ppStrSetKikiID As String                    '更新条件：セットID
    Private ppIntTargetSapRow As Integer                '入力チェック：サポセン機器メンテナンスチェック対象行
    Private ppIntTargetSapCol As Integer                '入力チェック：サポセン機器メンテナンスチェック対象列
    Private ppIntSelectedOutputSapRow As Integer        '出力制御：出力時選択行番号  
    Private ppIntSelectedSapRow As Integer              '入力制御：選択行番号 
    '【ADD】2012/07/28 t.fukuo　サポセン機器情報タブ機能作成：END
    Private ppStrRirekiStatus As String                 '作業履歴モードで開いたときのステータス

    '更新処理用
    Private ppIntCINmb As Integer                       '更新条件：CI番号
    Private ppStrCIKbnCD As String                      'メール作成時更新条件：対象機器CI種別CD

    Private ppBtnSMRenkei As Button                     'フッタ：連携処理実施ボタン
    Private ppBtnSMShow As Button                       'フッタ：連携最新情報を見るボタン

    Private ppDtIncidentSMtuti As DataTable             'データ取得用：インシデントSM通知データ

    '【ADD】2012/09/20 k.ueda　メッセージ出力判定用：START
    Private ppStrLogFilePath As String
    '【ADD】2012/09/20 k.ueda　メッセージ出力判定用：END
    Private ppBlnCheckSystemNmb As Boolean              'True：対象システム変更あり

    '【ADD】2014/04/03 e.okamura　取消時セット機器更新修正：START
    Private ppIntCINmbSetIDClear As Integer             '更新条件：CI番号(取消時セットIDクリア用)
    '【ADD】2014/04/03 e.okamura　取消時セット機器更新修正：END


    ''' <summary>
    ''' プロパティセット【前画面パラメータ：処理モード（1：新規登録、2：編集、3：参照、4：作業履歴）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProcMode</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrProcMode() As String
        Get
            Return ppStrProcMode
        End Get
        Set(ByVal value As String)
            ppStrProcMode = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：INC番号 ※新規モード時には新規INC番号がセットされる】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntINCNmb</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntINCNmb() As Integer
        Get
            Return ppIntINCNmb
        End Get
        Set(ByVal value As Integer)
            ppIntINCNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：履歴番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntRirekiNo</returns>
    ''' <remarks><para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntRirekiNo() As Integer
        Get
            Return ppIntRirekiNo
        End Get
        Set(ByVal value As Integer)
            ppIntRirekiNo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：会議番号 】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntMeetingNmb</returns>
    ''' <remarks><para>作成情報：2012/08/06 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntMeetingNmb() As Integer
        Get
            Return ppIntMeetingNmb
        End Get
        Set(ByVal value As Integer)
            ppIntMeetingNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業履歴モード遷移時パラメータ：編集開始日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrEdiTime</returns>
    ''' <remarks><para>作成情報：2012/07/22 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrEdiTime() As String
        Get
            Return ppStrEdiTime
        End Get
        Set(ByVal value As String)
            ppStrEdiTime = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：呼び元画面】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntOwner</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntOwner() As Integer
        Get
            Return ppIntOwner
        End Get
        Set(ByVal value As Integer)
            ppIntOwner = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【前画面パラメータ：呼び先画面】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppfrmInstance</returns>
    ''' <remarks><para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropfrmInstance() As Object
        Get
            Return ppfrmInstance
        End Get
        Set(ByVal value As Object)
            ppfrmInstance = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【前画面パラメータ：別画面制御系コントロールリスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryfrmCtlList</returns>
    ''' <remarks><para>作成情報：2012/08/05 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryfrmCtlList() As ArrayList
        Get
            Return ppAryfrmCtlList
        End Get
        Set(ByVal value As ArrayList)
            ppAryfrmCtlList = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ログイン：ログイン情報グループボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppGrpLoginUser</returns>
    ''' <remarks><para>作成情報：2012/07/22 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropGrpLoginUser() As GroupControlEx
        Get
            Return ppGrpLoginUser
        End Get
        Set(ByVal value As GroupControlEx)
            ppGrpLoginUser = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ヘッダ：インシデント管理グループボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppGrpIncCD</returns>
    ''' <remarks><para>作成情報：2012/07/20 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropGrpIncCD() As GroupBox
        Get
            Return ppGrpIncCD
        End Get
        Set(ByVal value As GroupBox)
            ppGrpIncCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ッダ：インシデント番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtIncCD</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtIncCD() As TextBox
        Get
            Return ppTxtIncCD
        End Get
        Set(ByVal value As TextBox)
            ppTxtIncCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ヘッダ：登録者ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblRegInfo</returns>
    ''' <remarks><para>作成情報：2012/07/20 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblRegInfo() As Label
        Get
            Return ppLblRegInfo
        End Get
        Set(ByVal value As Label)
            ppLblRegInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ヘッダ：最終更新者ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblUpdateInfo</returns>
    ''' <remarks><para>作成情報：2012/07/20 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblUpdateInfo() As Label
        Get
            Return ppLblUpdateInfo
        End Get
        Set(ByVal value As Label)
            ppLblUpdateInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ヘッダ：登録者出力用ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblRegInfo_out</returns>
    ''' <remarks><para>作成情報：2012/07/20 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblRegInfo_out() As Label
        Get
            Return ppLblRegInfo_out
        End Get
        Set(ByVal value As Label)
            ppLblRegInfo_out = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ヘッダ：最終更新者出力用ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblUpdateInfo_out</returns>
    ''' <remarks><para>作成情報：2012/07/20 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblUpdateInfo_out() As Label
        Get
            Return ppLblUpdateInfo_out
        End Get
        Set(ByVal value As Label)
            ppLblUpdateInfo_out = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ヘッダ：完了メッセージ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblkanryoMsg</returns>
    ''' <remarks><para>作成情報：2012/09/03 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblkanryoMsg() As Label
        Get
            Return ppLblkanryoMsg
        End Get
        Set(ByVal value As Label)
            ppLblkanryoMsg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【タブ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTbInput</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTbInput() As TabControl
        Get
            Return ppTbInput
        End Get
        Set(ByVal value As TabControl)
            ppTbInput = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：受付手段コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbUkeKbn</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbUkeKbn() As ComboBox
        Get
            Return ppCmbUkeKbn
        End Get
        Set(ByVal value As ComboBox)
            ppCmbUkeKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：発生日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpHasseiDT</returns>
    ''' <remarks><para>作成情報：2012/07/05 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpHasseiDT() As DateTimePickerEx
        Get
            Return ppDtpHasseiDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpHasseiDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：発生日時時分表示テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtHasseiDT_HM</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtHasseiDT_HM() As TextBoxEx_IoTime
        Get
            Return ppTxtHasseiDT_HM
        End Get
        Set(ByVal value As TextBoxEx_IoTime)
            ppTxtHasseiDT_HM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：発生日時（時間入力）ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnHasseiDT_HM</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnHasseiDT_HM() As Button
        Get
            Return ppBtnHasseiDT_HM
        End Get
        Set(ByVal value As Button)
            ppBtnHasseiDT_HM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：インシデント種別コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbIncKbnCD</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbIncKbnCD() As ComboBox
        Get
            Return ppCmbIncKbnCD
        End Get
        Set(ByVal value As ComboBox)
            ppCmbIncKbnCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：ステータスコンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbprocessStateCD</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbprocessStateCD() As ComboBox
        Get
            Return ppCmbprocessStateCD
        End Get
        Set(ByVal value As ComboBox)
            ppCmbprocessStateCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：ドメインコンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbDomainCD</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbDomainCD() As ComboBox
        Get
            Return ppCmbDomainCD
        End Get
        Set(ByVal value As ComboBox)
            ppCmbDomainCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：対象システム階層表示コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbSystemNmb</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbSystemNmb() As ComboBoxEx
        Get
            Return ppCmbSystemNmb
        End Get
        Set(ByVal value As ComboBoxEx)
            ppCmbSystemNmb = value
        End Set
    End Property

    '[ADD] 2012/10/24 s.yamaguchi START
    ''' <summary>
    ''' プロパティセット【基本情報：対象システム検索ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnSearchTaisyouSystem</returns>
    ''' <remarks><para>作成情報：2012/10/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnSearchTaisyouSystem() As Button
        Get
            Return ppBtnSearchTaisyouSystem
        End Get
        Set(ByVal value As Button)
            ppBtnSearchTaisyouSystem = value
        End Set
    End Property
    '[ADD] 2012/10/24 s.yamaguchi END

    ''' <summary>
    ''' プロパティセット【基本情報：ノウハウボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnKnowHow</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnKnowHow() As Button
        Get
            Return ppBtnKnowHow
        End Get
        Set(ByVal value As Button)
            ppBtnKnowHow = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：外部ツール番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtOutSideToolNmb</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtOutSideToolNmb() As TextBox
        Get
            Return ppTxtOutSideToolNmb
        End Get
        Set(ByVal value As TextBox)
            ppTxtOutSideToolNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：指示書チェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkShijisyoFlg</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropChkShijisyoFlg() As CheckBox
        Get
            Return ppChkShijisyoFlg
        End Get
        Set(ByVal value As CheckBox)
            ppChkShijisyoFlg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：タイトルテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtTitle</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtTitle() As TextBox
        Get
            Return ppTxtTitle
        End Get
        Set(ByVal value As TextBox)
            ppTxtTitle = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【タブ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTbNaiyo</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTbNaiyo() As TabControl
        Get
            Return ppTbNaiyo
        End Get
        Set(ByVal value As TabControl)
            ppTbNaiyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：受付内容テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtUkeNaiyo</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtUkeNaiyo() As TextBox
        Get
            Return ppTxtUkeNaiyo
        End Get
        Set(ByVal value As TextBox)
            ppTxtUkeNaiyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：重要度テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPriority</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPriority() As TextBox
        Get
            Return ppTxtPriority
        End Get
        Set(ByVal value As TextBox)
            ppTxtPriority = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：障害レベルテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtErrlevel</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtErrlevel() As TextBox
        Get
            Return ppTxtErrlevel
        End Get
        Set(ByVal value As TextBox)
            ppTxtErrlevel = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：イベントIDテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtEventID</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtEventID() As TextBox
        Get
            Return ppTxtEventID
        End Get
        Set(ByVal value As TextBox)
            ppTxtEventID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：ソーステキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSource</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSource() As TextBox
        Get
            Return ppTxtSource
        End Get
        Set(ByVal value As TextBox)
            ppTxtSource = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：OPCイベントIDテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtOPCEventID</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtOPCEventID() As TextBox
        Get
            Return ppTxtOPCEventID
        End Get
        Set(ByVal value As TextBox)
            ppTxtOPCEventID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：イベントクラステキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtEventClass</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtEventClass() As TextBox
        Get
            Return ppTxtEventClass
        End Get
        Set(ByVal value As TextBox)
            ppTxtEventClass = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：対応結果テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtTaioKekka</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtTaioKekka() As TextBox
        Get
            Return ppTxtTaioKekka
        End Get
        Set(ByVal value As TextBox)
            ppTxtTaioKekka = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：回答日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpKaitoDT</returns>
    ''' <remarks><para>作成情報：2012/07/05 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpKaitoDT() As DateTimePickerEx
        Get
            Return ppDtpKaitoDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpKaitoDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：回答日時時分表示テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtKaitoDT_HM</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtKaitoDT_HM() As TextBoxEx_IoTime
        Get
            Return ppTxtKaitoDT_HM
        End Get
        Set(ByVal value As TextBoxEx_IoTime)
            ppTxtKaitoDT_HM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：回答日時（時間入力）ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnKaitoDT_HM</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnKaitoDT_HM() As Button
        Get
            Return ppBtnKaitoDT_HM
        End Get
        Set(ByVal value As Button)
            ppBtnKaitoDT_HM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：完了日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpKanryoDT</returns>
    ''' <remarks><para>作成情報：2012/07/05 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpKanryoDT() As DateTimePickerEx
        Get
            Return ppDtpKanryoDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpKanryoDT = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【基本情報：完了日時時分表示テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtKanryoDT_HM</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtKanryoDT_HM() As TextBoxEx_IoTime
        Get
            Return ppTxtKanryoDT_HM
        End Get
        Set(ByVal value As TextBoxEx_IoTime)
            ppTxtKanryoDT_HM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：完了日時（時間入力）ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnKanryoDT_HM</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnKanryoDT_HM() As Button
        Get
            Return ppBtnKanryoDT_HM
        End Get
        Set(ByVal value As Button)
            ppBtnKanryoDT_HM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：相手IDテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPartnerID</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPartnerID() As TextBox
        Get
            Return ppTxtPartnerID
        End Get
        Set(ByVal value As TextBox)
            ppTxtPartnerID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：相手氏名テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPartnerNM</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPartnerNM() As TextBox
        Get
            Return ppTxtPartnerNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtPartnerNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：相手検索ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnPartnerSearch</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnPartnerSearch() As Button
        Get
            Return ppBtnPartnerSearch
        End Get
        Set(ByVal value As Button)
            ppBtnPartnerSearch = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：相手シメイテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPartnerID</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPartnerKana() As TextBox
        Get
            Return ppTxtPartnerKana
        End Get
        Set(ByVal value As TextBox)
            ppTxtPartnerKana = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：相手会社テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPartnerCompany</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPartnerCompany() As TextBox
        Get
            Return ppTxtPartnerCompany
        End Get
        Set(ByVal value As TextBox)
            ppTxtPartnerCompany = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：相手局テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPartnerKyokuNM</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPartnerKyokuNM() As TextBox
        Get
            Return ppTxtPartnerKyokuNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtPartnerKyokuNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：相手部署テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPartnerBusyoNM</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPartnerBusyoNM() As TextBox
        Get
            Return ppTxtPartnerBusyoNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtPartnerBusyoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：相手電話番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPartnerTel</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPartnerTel() As TextBox
        Get
            Return ppTxtPartnerTel
        End Get
        Set(ByVal value As TextBox)
            ppTxtPartnerTel = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：相手メールアドレステキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPartnerMailAdd</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPartnerMailAdd() As TextBox
        Get
            Return ppTxtPartnerMailAdd
        End Get
        Set(ByVal value As TextBox)
            ppTxtPartnerMailAdd = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：相手連絡先テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPartnerContact</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPartnerContact() As TextBox
        Get
            Return ppTxtPartnerContact
        End Get
        Set(ByVal value As TextBox)
            ppTxtPartnerContact = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：相手拠点テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPartnerBase</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPartnerBase() As TextBox
        Get
            Return ppTxtPartnerBase
        End Get
        Set(ByVal value As TextBox)
            ppTxtPartnerBase = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：相手番組・部屋テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPartnerRoom</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPartnerRoom() As TextBox
        Get
            Return ppTxtPartnerRoom
        End Get
        Set(ByVal value As TextBox)
            ppTxtPartnerRoom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：権限テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtKengen</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtKengen() As TextBox
        Get
            Return ppTxtKengen
        End Get
        Set(ByVal value As TextBox)
            ppTxtKengen = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：借用物テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtRentalKiki</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtRentalKiki() As TextBox
        Get
            Return ppTxtRentalKiki
        End Get
        Set(ByVal value As TextBox)
            ppTxtRentalKiki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：取得ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRentalKiki</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRentalKiki() As Button
        Get
            Return ppBtnRentalKiki
        End Get
        Set(ByVal value As Button)
            ppBtnRentalKiki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：担当グループコンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbTantoGrpCD</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbTantoGrpCD() As ComboBox
        Get
            Return ppCmbTantoGrpCD
        End Get
        Set(ByVal value As ComboBox)
            ppCmbTantoGrpCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：担当IDテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtIncTantoCD</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtIncTantoCD() As TextBox
        Get
            Return ppTxtIncTantoCD
        End Get
        Set(ByVal value As TextBox)
            ppTxtIncTantoCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：担当私ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnIncTantoMY</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnIncTantoMY() As Button
        Get
            Return ppBtnIncTantoMY
        End Get
        Set(ByVal value As Button)
            ppBtnIncTantoMY = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：担当氏名テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtIncTantoNM</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtIncTantoNM() As TextBox
        Get
            Return ppTxtIncTantoNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtIncTantoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：担当検索ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnIncTantoSearch</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnIncTantoSearch() As Button
        Get
            Return ppBtnIncTantoSearch
        End Get
        Set(ByVal value As Button)
            ppBtnIncTantoSearch = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：機器情報スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwkikiInfo</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwkikiInfo() As FpSpread
        Get
            Return ppVwkikiInfo
        End Get
        Set(ByVal value As FpSpread)
            ppVwkikiInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：機器情報行追加ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_kiki</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_kiki() As Button
        Get
            Return ppBtnAddRow_kiki
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_kiki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：機器情報行削除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRemoveRow_kiki</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRemoveRow_kiki() As Button
        Get
            Return ppBtnRemoveRow_kiki
        End Get
        Set(ByVal value As Button)
            ppBtnRemoveRow_kiki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：機器情報Webボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnWeb</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnWeb() As Button
        Get
            Return ppBtnWeb
        End Get
        Set(ByVal value As Button)
            ppBtnWeb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：機器情報SSCMボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnSSCM</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnSSCM() As Button
        Get
            Return ppBtnSSCM
        End Get
        Set(ByVal value As Button)
            ppBtnSSCM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：機器情報遠隔ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnEnkaku</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnEnkaku() As Button
        Get
            Return ppBtnEnkaku
        End Get
        Set(ByVal value As Button)
            ppBtnEnkaku = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：作業履歴スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwIncRireki</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwIncRireki() As FpSpread
        Get
            Return ppVwIncRireki
        End Get
        Set(ByVal value As FpSpread)
            ppVwIncRireki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【経過種別】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppCmbSpdkeika</returns>
    ''' <remarks><para>作成情報：2012/07/26 r.hoshino 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbSpdkeika() As CellType.ComboBoxCellType
        Get
            Return ppCmbSpdkeika
        End Get
        Set(ByVal value As CellType.ComboBoxCellType)
            ppCmbSpdkeika = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【対象システム】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppCmbSpdsystem</returns>
    ''' <remarks><para>作成情報：2012/07/26 r.hoshino 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbSpdSystem() As CellType.MultiColumnComboBoxCellType
        Get
            Return ppCmbSpdsystem
        End Get
        Set(ByVal value As CellType.MultiColumnComboBoxCellType)
            ppCmbSpdsystem = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【担当者】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnSpdTanto</returns>
    ''' <remarks><para>作成情報：2012/07/26 r.hoshino 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnSpdTanto() As CellType.ButtonCellType
        Get
            Return ppBtnSpdTanto
        End Get
        Set(ByVal value As CellType.ButtonCellType)
            ppBtnSpdTanto = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【開始時ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnSpdkaishiji</returns>
    ''' <remarks><para>作成情報：2012/07/26 r.hoshino 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnSpdKaishiji() As CellType.ButtonCellType
        Get
            Return ppBtnSpdkaishiji
        End Get
        Set(ByVal value As CellType.ButtonCellType)
            ppBtnSpdkaishiji = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【予定時ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnSpdyoteiji</returns>
    ''' <remarks><para>作成情報：2012/07/26 r.hoshino 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnSpdYoteiji() As CellType.ButtonCellType
        Get
            Return ppBtnSpdyoteiji
        End Get
        Set(ByVal value As CellType.ButtonCellType)
            ppBtnSpdyoteiji = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【終了時ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnSpdsyuryoji</returns>
    ''' <remarks><para>作成情報：2012/07/26 r.hoshino 
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnSpdSyuryoji() As CellType.ButtonCellType
        Get
            Return ppBtnSpdsyuryoji
        End Get
        Set(ByVal value As CellType.ButtonCellType)
            ppBtnSpdsyuryoji = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議情報：会議情報行削除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_rireki</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_rireki() As Button
        Get
            Return ppBtnAddRow_rireki
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_rireki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：作業履歴行削除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRemoveRow_rireki</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRemoveRow_rireki() As Button
        Get
            Return ppBtnRemoveRow_rireki
        End Get
        Set(ByVal value As Button)
            ppBtnRemoveRow_rireki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：作業履歴拡大ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnkakudai</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnkakudai() As Button
        Get
            Return ppBtnkakudai
        End Get
        Set(ByVal value As Button)
            ppBtnkakudai = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：作業履歴リフレッシュボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRefresh</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRefresh() As Button
        Get
            Return ppBtnRefresh
        End Get
        Set(ByVal value As Button)
            ppBtnRefresh = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：作業履歴カレンダー】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppMcdRireki</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropMcdRireki() As MonthCalendar
        Get
            Return ppMcdRireki
        End Get
        Set(ByVal value As MonthCalendar)
            ppMcdRireki = value
        End Set
    End Property


    '【ADD】2012/07/25 t.fukuo　サポセン機器情報タブ機能作成：START
    ''' <summary>
    ''' プロパティセット【サポセン機器情報：相手IDテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPartnerID_Sap</returns>
    ''' <remarks><para>作成情報：2012/07/25 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPartnerID_Sap() As TextBox
        Get
            Return ppTxtPartnerID_Sap
        End Get
        Set(ByVal value As TextBox)
            ppTxtPartnerID_Sap = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サポセン機器情報：相手氏名テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPartnerNM_Sap</returns>
    ''' <remarks><para>作成情報：2012/07/25 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPartnerNM_Sap() As TextBox
        Get
            Return ppTxtPartnerNM_Sap
        End Get
        Set(ByVal value As TextBox)
            ppTxtPartnerNM_Sap = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サポセン機器情報：相手シメイテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPartnerKana_Sap</returns>
    ''' <remarks><para>作成情報：2012/07/25 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPartnerKana_Sap() As TextBox
        Get
            Return ppTxtPartnerKana_Sap
        End Get
        Set(ByVal value As TextBox)
            ppTxtPartnerKana_Sap = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サポセン機器情報：相手会社テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPartnerCompany_Sap</returns>
    ''' <remarks><para>作成情報：2012/07/25 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPartnerCompany_Sap() As TextBox
        Get
            Return ppTxtPartnerCompany_Sap
        End Get
        Set(ByVal value As TextBox)
            ppTxtPartnerCompany_Sap = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サポセン機器情報：相手局テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPartnerKyokuNM_Sap</returns>
    ''' <remarks><para>作成情報：2012/07/25 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPartnerKyokuNM_Sap() As TextBox
        Get
            Return ppTxtPartnerKyokuNM_Sap
        End Get
        Set(ByVal value As TextBox)
            ppTxtPartnerKyokuNM_Sap = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サポセン機器情報：相手部署テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPartnerBusyoNM_Sap</returns>
    ''' <remarks><para>作成情報：2012/07/25 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPartnerBusyoNM_Sap() As TextBox
        Get
            Return ppTxtPartnerBusyoNM_Sap
        End Get
        Set(ByVal value As TextBox)
            ppTxtPartnerBusyoNM_Sap = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サポセン機器情報：相手電話番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPartnerTel_Sap</returns>
    ''' <remarks><para>作成情報：2012/07/25 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPartnerTel_Sap() As TextBox
        Get
            Return ppTxtPartnerTel_Sap
        End Get
        Set(ByVal value As TextBox)
            ppTxtPartnerTel_Sap = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サポセン機器情報：相手メールアドレステキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPartnerMailAdd_Sap</returns>
    ''' <remarks><para>作成情報：2012/07/25 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPartnerMailAdd_Sap() As TextBox
        Get
            Return ppTxtPartnerMailAdd_Sap
        End Get
        Set(ByVal value As TextBox)
            ppTxtPartnerMailAdd_Sap = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サポセン機器情報：相手連絡先テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPartnerContact_Sap</returns>
    ''' <remarks><para>作成情報：2012/07/25 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPartnerContact_Sap() As TextBox
        Get
            Return ppTxtPartnerContact_Sap
        End Get
        Set(ByVal value As TextBox)
            ppTxtPartnerContact_Sap = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サポセン機器情報：相手拠点テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPartnerBase_Sap</returns>
    ''' <remarks><para>作成情報：2012/07/25 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPartnerBase_Sap() As TextBox
        Get
            Return ppTxtPartnerBase_Sap
        End Get
        Set(ByVal value As TextBox)
            ppTxtPartnerBase_Sap = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サポセン機器情報：相手番組／部屋テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPartnerRoom_Sap</returns>
    ''' <remarks><para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPartnerRoom_Sap() As TextBox
        Get
            Return ppTxtPartnerRoom_Sap
        End Get
        Set(ByVal value As TextBox)
            ppTxtPartnerRoom_Sap = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サポセン機器情報：作業コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbWork</returns>
    ''' <remarks><para>作成情報：2012/07/25 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbWork() As ComboBox
        Get
            Return ppCmbWork
        End Get
        Set(ByVal value As ComboBox)
            ppCmbWork = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サポセン機器情報：作業追加ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_SapMainte</returns>
    ''' <remarks><para>作成情報：2012/07/25 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_SapMainte() As Button
        Get
            Return ppBtnAddRow_SapMainte
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_SapMainte = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サポセン機器情報：サポセン機器メンテナンススプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwSapMainte</returns>
    ''' <remarks><para>作成情報：2012/07/25 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwSapMainte() As FpSpread
        Get
            Return ppVwSapMainte
        End Get
        Set(ByVal value As FpSpread)
            ppVwSapMainte = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サポセン機器情報：選択行を交換／解除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnExchange</returns>
    ''' <remarks><para>作成情報：2012/09/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnExchange() As Button
        Get
            Return ppBtnExchange
        End Get
        Set(ByVal value As Button)
            ppBtnExchange = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サポセン機器情報：選択行をセットにするボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnSetPair</returns>
    ''' <remarks><para>作成情報：2012/09/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnSetPair() As Button
        Get
            Return ppBtnSetPair
        End Get
        Set(ByVal value As Button)
            ppBtnSetPair = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サポセン機器情報：選択行を既存のセットまたは機器とセットにするボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddPair</returns>
    ''' <remarks><para>作成情報：2012/09/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddPair() As Button
        Get
            Return ppBtnAddPair
        End Get
        Set(ByVal value As Button)
            ppBtnAddPair = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サポセン機器情報：選択行のセットをバラすボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnCepalatePair</returns>
    ''' <remarks><para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnCepalatePair() As Button
        Get
            Return ppBtnCepalatePair
        End Get
        Set(ByVal value As Button)
            ppBtnCepalatePair = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サポセン機器情報：貸出誓約書出力ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnOutput_Kashidashi</returns>
    ''' <remarks><para>作成情報：2012/07/25 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnOutput_Kashidashi() As Button
        Get
            Return ppBtnOutput_Kashidashi
        End Get
        Set(ByVal value As Button)
            ppBtnOutput_Kashidashi = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サポセン機器情報：期限更新誓約書出力ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnOutput_UpLimitDate</returns>
    ''' <remarks><para>作成情報：2012/07/25 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnOutput_UpLimitDate() As Button
        Get
            Return ppBtnOutput_UpLimitDate
        End Get
        Set(ByVal value As Button)
            ppBtnOutput_UpLimitDate = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サポセン機器情報：預かり確認書出力ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnOutput_Azukari</returns>
    ''' <remarks><para>作成情報：2012/07/25 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnOutput_Azukari() As Button
        Get
            Return ppBtnOutput_Azukari
        End Get
        Set(ByVal value As Button)
            ppBtnOutput_Azukari = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サポセン機器情報：返却確認書出力ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnOutput_Henkyaku</returns>
    ''' <remarks><para>作成情報：2012/07/25 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnOutput_Henkyaku() As Button
        Get
            Return ppBtnOutput_Henkyaku
        End Get
        Set(ByVal value As Button)
            ppBtnOutput_Henkyaku = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サポセン機器情報：チェックシート出力ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnOutput_Check</returns>
    ''' <remarks><para>作成情報：2012/07/25 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnOutput_Check() As Button
        Get
            Return ppBtnOutput_Check
        End Get
        Set(ByVal value As Button)
            ppBtnOutput_Check = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サポセン機器情報：サポセン機器メンテナンスカレンダー】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppMcdSapMainte</returns>
    ''' <remarks><para>作成情報：2012/07/25 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropMcdSapMainte() As MonthCalendar
        Get
            Return ppMcdSapMainte
        End Get
        Set(ByVal value As MonthCalendar)
            ppMcdSapMainte = value
        End Set
    End Property
    '【ADD】2012/07/25 t.fukuo　サポセン機器情報タブ機能作成：END

    ''' <summary>
    ''' プロパティセット【会議情報：会議情報スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwMeeting</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwMeeting() As FpSpread
        Get
            Return ppVwMeeting
        End Get
        Set(ByVal value As FpSpread)
            ppVwMeeting = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議情報：会議情報行追加ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_meeting</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_meeting() As Button
        Get
            Return ppBtnAddRow_meeting
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_meeting = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【会議情報：会議情報行削除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRemoveRow_meeting</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRemoveRow_meeting() As Button
        Get
            Return ppBtnRemoveRow_meeting
        End Get
        Set(ByVal value As Button)
            ppBtnRemoveRow_meeting = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：テキスト１テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBIko1</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtBIko1() As TextBox
        Get
            Return ppTxtBIko1
        End Get
        Set(ByVal value As TextBox)
            ppTxtBIko1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：テキスト２テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBIko2</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtBIko2() As TextBox
        Get
            Return ppTxtBIko2
        End Get
        Set(ByVal value As TextBox)
            ppTxtBIko2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：テキスト３テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBIko3</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtBIko3() As TextBox
        Get
            Return ppTxtBIko3
        End Get
        Set(ByVal value As TextBox)
            ppTxtBIko3 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：テキスト４テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBIko4</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtBIko4() As TextBox
        Get
            Return ppTxtBIko4
        End Get
        Set(ByVal value As TextBox)
            ppTxtBIko4 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：テキスト５テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtBIko5</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtBIko5() As TextBox
        Get
            Return ppTxtBIko5
        End Get
        Set(ByVal value As TextBox)
            ppTxtBIko5 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：フリーフラグ１チェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkFreeFlg1</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropChkFreeFlg1() As CheckBox
        Get
            Return ppChkFreeFlg1
        End Get
        Set(ByVal value As CheckBox)
            ppChkFreeFlg1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：フリーフラグ２チェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkFreeFlg2</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropChkFreeFlg2() As CheckBox
        Get
            Return ppChkFreeFlg2
        End Get
        Set(ByVal value As CheckBox)
            ppChkFreeFlg2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：フリーフラグ３チェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkFreeFlg3</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropChkFreeFlg3() As CheckBox
        Get
            Return ppChkFreeFlg3
        End Get
        Set(ByVal value As CheckBox)
            ppChkFreeFlg3 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：フリーフラグ４チェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkFreeFlg4</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropChkFreeFlg4() As CheckBox
        Get
            Return ppChkFreeFlg4
        End Get
        Set(ByVal value As CheckBox)
            ppChkFreeFlg4 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリー入力情報：フリーフラグ５チェックボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppChkFreeFlg5</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropChkFreeFlg5() As CheckBox
        Get
            Return ppChkFreeFlg5
        End Get
        Set(ByVal value As CheckBox)
            ppChkFreeFlg5 = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【関係情報：関係者情報スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwRelation</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwRelation() As FpSpread
        Get
            Return ppVwRelation
        End Get
        Set(ByVal value As FpSpread)
            ppVwRelation = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【関係情報：グループ行追加ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_Grp</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_Grp() As Button
        Get
            Return ppBtnAddRow_Grp
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_Grp = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【関係情報：ユーザー行追加ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_Usr</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_Usr() As Button
        Get
            Return ppBtnAddRow_Usr
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_Usr = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【関係情報：関係者情報行削除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRemoveRow_Relation</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRemoveRow_Relation() As Button
        Get
            Return ppBtnRemoveRow_Relation
        End Get
        Set(ByVal value As Button)
            ppBtnRemoveRow_Relation = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：グループ履歴】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtGrpHistory</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtGrpHistory() As TextBox
        Get
            Return ppTxtGrpHistory
        End Get
        Set(ByVal value As TextBox)
            ppTxtGrpHistory = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：担当者履歴】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtTantHistory</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtTantoHistory() As TextBox
        Get
            Return ppTxtTantoHistory
        End Get
        Set(ByVal value As TextBox)
            ppTxtTantoHistory = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：関連ファイルスプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwprocessLinkInfo</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwprocessLinkInfo() As FpSpread
        Get
            Return ppVwprocessLinkInfo
        End Get
        Set(ByVal value As FpSpread)
            ppVwprocessLinkInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：プロセスリンク行追加ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_plink</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_plink() As Button
        Get
            Return ppBtnAddRow_plink
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_plink = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：プロセスリンク行削除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRemoveRow_plink</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRemoveRow_plink() As Button
        Get
            Return ppBtnRemoveRow_plink
        End Get
        Set(ByVal value As Button)
            ppBtnRemoveRow_plink = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：関連ファイルスプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwFileInfo</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwFileInfo() As FpSpread
        Get
            Return ppVwFileInfo
        End Get
        Set(ByVal value As FpSpread)
            ppVwFileInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：関連ファイル行追加ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAddRow_File</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAddRow_File() As Button
        Get
            Return ppBtnAddRow_File
        End Get
        Set(ByVal value As Button)
            ppBtnAddRow_File = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：関連ファイル行削除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnRemoveRow_File</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnRemoveRow_File() As Button
        Get
            Return ppBtnRemoveRow_File
        End Get
        Set(ByVal value As Button)
            ppBtnRemoveRow_File = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：関連ファイルボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnOpenFile</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnOpenFile() As Button
        Get
            Return ppBtnOpenFile
        End Get
        Set(ByVal value As Button)
            ppBtnOpenFile = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：関連ファイルダボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnSaveFile</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnSaveFile() As Button
        Get
            Return ppBtnSaveFile
        End Get
        Set(ByVal value As Button)
            ppBtnSaveFile = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【フッタ：登録ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnReg</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnReg() As Button
        Get
            Return ppBtnReg
        End Get
        Set(ByVal value As Button)
            ppBtnReg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：複製ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnCopy</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnCopy() As Button
        Get
            Return ppBtnCopy
        End Get
        Set(ByVal value As Button)
            ppBtnCopy = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：メール作成ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnMail</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnMail() As Button
        Get
            Return ppBtnMail
        End Get
        Set(ByVal value As Button)
            ppBtnMail = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：問題登録ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnMondai</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnMondai() As Button
        Get
            Return ppBtnMondai
        End Get
        Set(ByVal value As Button)
            ppBtnMondai = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：単票出力ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnPrint</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnPrint() As Button
        Get
            Return ppBtnPrint
        End Get
        Set(ByVal value As Button)
            ppBtnPrint = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：戻るボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnBack</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnBack() As Button
        Get
            Return ppBtnBack
        End Get
        Set(ByVal value As Button)
            ppBtnBack = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【期限切れ条件CI種別】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtkigencondcikbncd </returns>
    ''' <remarks><para>作成情報：2012/08/03 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtkigencondcikbncd() As String
        Get
            Return ppTxtkigencondcikbncd
        End Get
        Set(ByVal value As String)
            ppTxtkigencondcikbncd = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【期限切れ条件タイプ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnBack</returns>
    ''' <remarks><para>作成情報：2012/08/03 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtkigencondtypekbn() As String
        Get
            Return ppTxtkigencondtypekbn
        End Get
        Set(ByVal value As String)
            ppTxtkigencondtypekbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【期限切れ条件期限】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnBack</returns>
    ''' <remarks><para>作成情報：2012/08/03 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtkigencondkigen() As String
        Get
            Return ppTxtkigencondkigen
        End Get
        Set(ByVal value As String)
            ppTxtkigencondkigen = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【期限切れ条件ユーザーID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtKigenCondUsrID</returns>
    ''' <remarks><para>作成情報：2012/08/06 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtKigenCondUsrID() As String
        Get
            Return ppTxtKigenCondUsrID
        End Get
        Set(ByVal value As String)
            ppTxtKigenCondUsrID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【登録グループ名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtRegGp </returns>
    ''' <remarks><para>作成情報：2012/08/06 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtRegGp() As String
        Get
            Return ppTxtRegGp
        End Get
        Set(ByVal value As String)
            ppTxtRegGp = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【登録ユーザー名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtRegUsr </returns>
    ''' <remarks><para>作成情報：2012/08/06 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtRegUsr() As String
        Get
            Return ppTxtRegUsr
        End Get
        Set(ByVal value As String)
            ppTxtRegUsr = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【登録日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtRegDT </returns>
    ''' <remarks><para>作成情報：2012/08/06 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtRegDT() As String
        Get
            Return ppTxtRegDT
        End Get
        Set(ByVal value As String)
            ppTxtRegDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【最終更新グループ名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtUpdateGp </returns>
    ''' <remarks><para>作成情報：2012/08/06 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtUpdateGp() As String
        Get
            Return ppTxtUpdateGp
        End Get
        Set(ByVal value As String)
            ppTxtUpdateGp = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【最終更新ユーザー名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtUpdateUsr </returns>
    ''' <remarks><para>作成情報：2012/08/06 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtUpdateUsr() As String
        Get
            Return ppTxtUpdateUsr
        End Get
        Set(ByVal value As String)
            ppTxtUpdateUsr = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【最終更新日時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtUpdateDT </returns>
    ''' <remarks><para>作成情報：2012/08/06 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtUpdateDT() As String
        Get
            Return ppTxtUpdateDT
        End Get
        Set(ByVal value As String)
            ppTxtUpdateDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【拡大判定フラグ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppblnKakudaiFlg </returns>
    ''' <remarks><para>作成情報：2012/08/07 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropblnKakudaiFlg() As Boolean
        Get
            Return ppblnKakudaiFlg
        End Get
        Set(ByVal value As Boolean)
            ppblnKakudaiFlg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス用：受付手段マスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtUketsukeMasta</returns>
    ''' <remarks><para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtUketsukeMasta() As DataTable
        Get
            Return ppDtUketsukeMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtUketsukeMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス用：INC種別マスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtKindMasta</returns>
    ''' <remarks><para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtKindMasta() As DataTable
        Get
            Return ppDtKindMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtKindMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス用：プロセスステータスマスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtprocessStatusMasta</returns>
    ''' <remarks><para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtprocessStatusMasta() As DataTable
        Get
            Return ppDtprocessStatusMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtprocessStatusMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ンボボックス用：ドメインマスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtDomeinMasta</returns>
    ''' <remarks><para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtDomeinMasta() As DataTable
        Get
            Return ppDtDomeinMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtDomeinMasta = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【コンボボックス用：担当グループマスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtTantGrpMasta</returns>
    ''' <remarks><para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtTantGrpMasta() As DataTable
        Get
            Return ppDtTantGrpMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtTantGrpMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス用：経過種別マスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtKeikaMasta</returns>
    ''' <remarks><para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtKeikaMasta() As DataTable
        Get
            Return ppDtKeikaMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtKeikaMasta = value
        End Set
    End Property

    '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：START
    ''' <summary>
    ''' プロパティセット【コンボボックス用：作業マスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtWorkMasta</returns>
    ''' <remarks><para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtWorkMasta() As DataTable
        Get
            Return ppDtWorkMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtWorkMasta = value
        End Set
    End Property
    '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：END

    ''' <summary>
    ''' プロパティセット【コンボボックス用：対象システムデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSystemMasta</returns>
    ''' <remarks><para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSystemMasta() As DataTable
        Get
            Return ppDtSystemMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtSystemMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【メイン表示用：INC共通情報】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtINCInfo</returns>
    ''' <remarks><para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtINCInfo() As DataTable
        Get
            Return ppDtINCInfo
        End Get
        Set(ByVal value As DataTable)
            ppDtINCInfo = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【ロック情報：INC共通情報ロックデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtINCLock</returns>
    ''' <remarks><para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtINCLock() As DataTable
        Get
            Return ppDtINCLock
        End Get
        Set(ByVal value As DataTable)
            ppDtINCLock = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド表示用：機器情報データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtINCkiki</returns>
    ''' <remarks><para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtINCkiki() As DataTable
        Get
            Return ppDtINCkiki
        End Get
        Set(ByVal value As DataTable)
            ppDtINCkiki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド表示用：作業履歴データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtINCRireki</returns>
    ''' <remarks><para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtINCRireki() As DataTable
        Get
            Return ppDtINCRireki
        End Get
        Set(ByVal value As DataTable)
            ppDtINCRireki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド表示用：作業担当データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtINCTanto</returns>
    ''' <remarks><para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtINCTanto() As DataTable
        Get
            Return ppDtINCTanto
        End Get
        Set(ByVal value As DataTable)
            ppDtINCTanto = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド表示用：対応関係者情報データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtRelation</returns>
    ''' <remarks><para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtRelation() As DataTable
        Get
            Return ppDtRelation
        End Get
        Set(ByVal value As DataTable)
            ppDtRelation = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド表示用：プロセスリンク管理番号データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtprocessLink</returns>
    ''' <remarks><para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtprocessLink() As DataTable
        Get
            Return ppDtprocessLink
        End Get
        Set(ByVal value As DataTable)
            ppDtprocessLink = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド表示用：関連ファイルデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtFileInfo</returns>
    ''' <remarks><para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtFileInfo() As DataTable
        Get
            Return ppDtFileInfo
        End Get
        Set(ByVal value As DataTable)
            ppDtFileInfo = value
        End Set
    End Property

    '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：START
    ''' <summary>
    ''' プロパティセット【スプレッド表示用：サポセン機器メンテナンスデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSapMainte</returns>
    ''' <remarks><para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSapMainte() As DataTable
        Get
            Return ppDtSapMainte
        End Get
        Set(ByVal value As DataTable)
            ppDtSapMainte = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【入力チェック用：サポセン機器メンテナンスデータ（更新後）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtTmp</returns>
    ''' <remarks><para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtTmp() As DataTable
        Get
            Return ppDtTmp
        End Get
        Set(ByVal value As DataTable)
            ppDtTmp = value
        End Set
    End Property
    '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：END

    ''' <summary>
    ''' プロパティセット【スプレッド表示用：会議情報データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtMeeting</returns>
    ''' <remarks><para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtMeeting() As DataTable
        Get
            Return ppDtMeeting
        End Get
        Set(ByVal value As DataTable)
            ppDtMeeting = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【データ登録／更新用：登録／更新行】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppRowReg</returns>
    ''' <remarks><para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropRowReg() As DataRow
        Get
            Return ppRowReg
        End Get
        Set(ByVal value As DataRow)
            ppRowReg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド制御用：選択ROW_index】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntRowSelect</returns>
    ''' <remarks><para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntRowSelect() As Integer
        Get
            Return ppIntRowSelect
        End Get
        Set(ByVal value As Integer)
            ppIntRowSelect = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【スプレッド制御用：選択Columns_index】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntRowSelect</returns>
    ''' <remarks><para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntColSelect() As Integer
        Get
            Return ppIntColSelect
        End Get
        Set(ByVal value As Integer)
            ppIntColSelect = value
        End Set
    End Property
    'ppIntVwRirekiRowHeight
    ''' <summary>
    ''' プロパティセット【スプレッド制御用：スプレッド行の高さ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntVwRirekiRowHeight</returns>
    ''' <remarks><para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntVwRirekiRowHeight() As Integer
        Get
            Return ppIntVwRirekiRowHeight
        End Get
        Set(ByVal value As Integer)
            ppIntVwRirekiRowHeight = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【メッセージ：ロック画面表示時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrBeLockedMsg</returns>
    ''' <remarks><para>作成情報：2012/07/22 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrBeLockedMsg() As String
        Get
            Return ppStrBeLockedMsg
        End Get
        Set(ByVal value As String)
            ppStrBeLockedMsg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【メッセージ：ロック解除時】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrBeUnlockedMsg</returns>
    ''' <remarks><para>作成情報：2012/07/22 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrBeUnlockedMsg() As String
        Get
            Return ppStrBeUnlockedMsg
        End Get
        Set(ByVal value As String)
            ppStrBeUnlockedMsg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サブ検索戻り値：検索データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtResultSub</returns>
    ''' <remarks><para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtResultSub() As DataTable
        Get
            Return ppDtResultSub
        End Get
        Set(ByVal value As DataTable)
            ppDtResultSub = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【取得戻り値：機器データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtResultkiki</returns>
    ''' <remarks><para>作成情報：2012/08/07 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtResultKiki() As DataTable
        Get
            Return ppDtResultKiki
        End Get
        Set(ByVal value As DataTable)
            ppDtResultkiki = value
        End Set
    End Property

    '
    ''' <summary>
    ''' プロパティセット【取得戻り値：会議結果データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtResultMtg</returns>
    ''' <remarks><para>作成情報：2012/08/14 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtResultMtg() As DataTable
        Get
            Return ppDtResultMtg
        End Get
        Set(ByVal value As DataTable)
            ppDtResultMtg = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【サブ検索戻り値：関連ファイルデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtFileNaiyo</returns>
    ''' <remarks><para>作成情報：2012/07/24 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtFileNaiyo() As String
        Get
            Return ppTxtFileNaiyo
        End Get
        Set(ByVal value As String)
            ppTxtFileNaiyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【サブ検索戻り値：関連ファイルデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtFilePath</returns>
    ''' <remarks><para>作成情報：2012/07/24 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtFilePath() As String
        Get
            Return ppTxtFilePath
        End Get
        Set(ByVal value As String)
            ppTxtFilePath = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ロック状況：ロックフラグ（0：ロックされていない、1：ロックされている）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnBeLockedFlg</returns>
    ''' <remarks><para>作成情報：2012/07/02 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnBeLockedFlg() As Boolean
        Get
            Return ppBlnBeLockedFlg
        End Get
        Set(ByVal value As Boolean)
            ppBlnBeLockedFlg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【システムエラー対応：トランザクション系コントロールリスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryTsxCtlList</returns>
    ''' <remarks><para>作成情報：2012/07/05 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryTsxCtlList() As ArrayList
        Get
            Return ppAryTsxCtlList
        End Get
        Set(ByVal value As ArrayList)
            ppAryTsxCtlList = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【その他：サーバー日付】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtmSysDate</returns>
    ''' <remarks><para>作成情報：2012/07/27 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtmSysDate() As DateTime
        Get
            Return ppDtmSysDate
        End Get
        Set(ByVal value As DateTime)
            ppDtmSysDate = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ログNo】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntLogNo</returns>
    ''' <remarks><para>作成情報：2012/07/23 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntLogNo() As Integer
        Get
            Return ppIntLogNo
        End Get
        Set(ByVal value As Integer)
            ppIntLogNo = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【ログNo(会議用)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntLogNoSub</returns>
    ''' <remarks><para>作成情報：2012/08/06 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntLogNoSub() As Integer
        Get
            Return ppIntLogNoSub
        End Get
        Set(ByVal value As Integer)
            ppIntLogNoSub = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【汎用：検索キー】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrSeaKey</returns>
    ''' <remarks><para>作成情報：2012/07/23 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrSeaKey() As String
        Get
            Return ppStrSeaKey
        End Get
        Set(ByVal value As String)
            ppStrSeaKey = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ロストフォーカス時値保存用プロパティ】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrLostFucs</returns>
    ''' <remarks><para>作成情報：2012/10/23 s.tsuruta
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrLostFucs() As String
        Get
            Return ppStrLostFucs
        End Get
        Set(ByVal value As String)
            ppStrLostFucs = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【チェック結果戻り値】 （0:参照不可,1:参照のみ関係者,2:関係者）
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntChkKankei</returns>
    ''' <remarks><para>作成情報：2012/08/28 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntChkKankei() As Integer
        Get
            Return ppIntChkKankei
        End Get
        Set(ByVal value As Integer)
            ppIntChkKankei = value
        End Set
    End Property

    '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：START
    ''' <summary>
    ''' プロパティセット【交換／交換解除区分】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntExchangeKbn</returns>
    ''' <remarks><para>作成情報：2012/09/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntExchangeKbn() As Integer
        Get
            Return ppIntExchangeKbn
        End Get
        Set(ByVal value As Integer)
            ppIntExchangeKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【交換／交換解除行番号配列】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppAryIntExchangePairIdx</returns>
    ''' <remarks><para>作成情報：2012/07/28 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryIntExchangePairIdx() As ArrayList
        Get
            Return ppAryIntExchangePairIdx
        End Get
        Set(ByVal value As ArrayList)
            ppAryIntExchangePairIdx = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【交換／交換解除コンテキストメニュー表示フラグ】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnExchangeEnable</returns>
    ''' <remarks><para>作成情報：2012/07/28 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnExchangeEnable() As Boolean
        Get
            Return ppBlnExchangeEnable
        End Get
        Set(ByVal value As Boolean)
            ppBlnExchangeEnable = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【機器検索一覧へのパラメータ：CIステータスコード】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrPlmCIStatusCD</returns>
    ''' <remarks><para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrPlmCIStatusCD() As String
        Get
            Return ppStrPlmCIStatusCD
        End Get
        Set(ByVal value As String)
            ppStrPlmCIStatusCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【更新値：CI（構成管理）履歴番号】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntCIRirekiNo</returns>
    ''' <remarks><para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntCIRirekiNo() As String
        Get
            Return ppIntCIRirekiNo
        End Get
        Set(ByVal value As String)
            ppIntCIRirekiNo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【更新値：CIステータスコード】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUpdCIStatusCD</returns>
    ''' <remarks><para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrUpdCIStatusCD() As String
        Get
            Return ppStrUpdCIStatusCD
        End Get
        Set(ByVal value As String)
            ppStrUpdCIStatusCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【更新値：作業区分コード】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUpdWorkKbnCD</returns>
    ''' <remarks><para>作成情報：2012/08/07 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrUpdWorkKbnCD() As String
        Get
            Return ppStrUpdWorkKbnCD
        End Get
        Set(ByVal value As String)
            ppStrUpdWorkKbnCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【更新条件：イメージ番号クリアフラグ】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnClearImageNmb</returns>
    ''' <remarks><para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnClearImageNmb() As Boolean
        Get
            Return ppBlnClearImageNmb
        End Get
        Set(ByVal value As Boolean)
            ppBlnClearImageNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【更新条件：サポセンデータクリアフラグ】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnClearSapData</returns>
    ''' <remarks><para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnClearSapData() As Boolean
        Get
            Return ppBlnClearSapData
        End Get
        Set(ByVal value As Boolean)
            ppBlnClearSapData = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【更新条件：交換撤去CI番号】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntExchangeCINmb</returns>
    ''' <remarks><para>作成情報：2012/08/02 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntExchangeCINmb() As Integer
        Get
            Return ppIntExchangeCINmb
        End Get
        Set(ByVal value As Integer)
            ppIntExchangeCINmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【更新条件：交換撤去最終更新履歴番号】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntExchangeLastUpRirekiNo</returns>
    ''' <remarks><para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntExchangeLastUpRirekiNo() As Integer
        Get
            Return ppIntExchangeLastUpRirekiNo
        End Get
        Set(ByVal value As Integer)
            ppIntExchangeLastUpRirekiNo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【更新条件：交換撤去作業番号】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntExchaneeWorkNmb</returns>
    ''' <remarks><para>作成情報：2012/08/02 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntExchangeWorkNmb() As Integer
        Get
            Return ppIntExchangeWorkNmb
        End Get
        Set(ByVal value As Integer)
            ppIntExchangeWorkNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【更新条件：交換撤去セットID】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrExchangeSetKikiNmb</returns>
    ''' <remarks><para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrExchangeSetKikiID() As String
        Get
            Return ppStrExchangeSetKikiID
        End Get
        Set(ByVal value As String)
            ppStrExchangeSetKikiID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【更新条件：セットID】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrSetKikiID</returns>
    ''' <remarks><para>作成情報：2012/09/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrSetKikiID() As String
        Get
            Return ppStrSetKikiID
        End Get
        Set(ByVal value As String)
            ppStrSetKikiID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【入力チェック：サポセン機器メンテナンスチェック対象行】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntTargetSapRow</returns>
    ''' <remarks><para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntTargetSapRow() As Integer
        Get
            Return ppIntTargetSapRow
        End Get
        Set(ByVal value As Integer)
            ppIntTargetSapRow = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【入力チェック：サポセン機器メンテナンスチェック対象列】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntTargetSapCol</returns>
    ''' <remarks><para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntTargetSapCol() As Integer
        Get
            Return ppIntTargetSapCol
        End Get
        Set(ByVal value As Integer)
            ppIntTargetSapCol = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【出力制御：サポセン機器メンテナンス選択行】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntSelectedOutputSapRow</returns>
    ''' <remarks><para>作成情報：2012/09/24 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntSelectedOutputSapRow() As Integer
        Get
            Return ppIntSelectedOutputSapRow
        End Get
        Set(ByVal value As Integer)
            ppIntSelectedOutputSapRow = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【入力制御：サポセン機器メンテナンス選択行】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntSelectedSapRow</returns>
    ''' <remarks><para>作成情報：2012/08/10 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntSelectedSapRow() As Integer
        Get
            Return ppIntSelectedSapRow
        End Get
        Set(ByVal value As Integer)
            ppIntSelectedSapRow = value
        End Set
    End Property
    '【ADD】2012/07/26 t.fukuo　サポセン機器情報タブ機能作成：END

    ''' <summary>
    ''' プロパティセット【作業履歴モードのステータスCD】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrRirekiStatus</returns>
    ''' <remarks><para>作成情報：2012/08/01 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrRirekiStatus() As String
        Get
            Return ppStrRirekiStatus
        End Get
        Set(ByVal value As String)
            ppStrRirekiStatus = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【更新条件：CI番号】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntCINmb</returns>
    ''' <remarks><para>作成情報：2012/08/29 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntCINmb() As Integer
        Get
            Return ppIntCINmb
        End Get
        Set(ByVal value As Integer)
            ppIntCINmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【メール作成時更新条件：対象機器CI種別CD】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrCIKbnCD</returns>
    ''' <remarks><para>作成情報：2012/08/29 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrCIKbnCD() As String
        Get
            Return ppStrCIKbnCD
        End Get
        Set(ByVal value As String)
            ppStrCIKbnCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【担当履歴情報】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtTantoRireki</returns>
    ''' <remarks><para>作成情報：2012/09/10 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtTantoRireki() As DataTable
        Get
            Return ppDtTantoRireki
        End Get
        Set(ByVal value As DataTable)
            ppDtTantoRireki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業担当履歴情報】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtwkRireki</returns>
    ''' <remarks><para>作成情報：2012/09/11 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtwkRireki() As DataTable
        Get
            Return ppDtwkRireki
        End Get
        Set(ByVal value As DataTable)
            ppDtwkRireki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ファンクション用パラメータ：選択中の行番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppIntSelectedRow</returns>
    ''' <remarks><para>作成情報：2012/08/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntSelectedRow() As Integer
        Get
            Return ppIntSelectedRow
        End Get
        Set(ByVal value As Integer)
            ppIntSelectedRow = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ファンクション用パラメータ：選択中の会議ファイルパス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrSelectedFilePath</returns>
    ''' <remarks><para>作成情報：2012/08/13 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrSelectedFilePath() As String
        Get
            Return ppStrSelectedFilePath
        End Get
        Set(ByVal value As String)
            ppStrSelectedFilePath = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【ファンクション用パラメータ：一時保存用データ行】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppRowTmp</returns>
    ''' <remarks><para>作成情報：2012/09/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropRowTmp() As DataRow
        Get
            Return ppRowTmp
        End Get
        Set(ByVal value As DataRow)
            ppRowTmp = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：連携処理実施ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnSMRenkei</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnSMRenkei() As Button
        Get
            Return ppBtnSMRenkei
        End Get
        Set(ByVal value As Button)
            ppBtnSMRenkei = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッタ：連携最新情報を見るボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnSMShow</returns>
    ''' <remarks><para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnSMShow() As Button
        Get
            Return ppBtnSMShow
        End Get
        Set(ByVal value As Button)
            ppBtnSMShow = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【データ取得用：インシデントSM通知データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtMeeting</returns>
    ''' <remarks><para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtIncidentSMtuti() As DataTable
        Get
            Return ppDtIncidentSMtuti
        End Get
        Set(ByVal value As DataTable)
            ppDtIncidentSMtuti = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【出力メッセージ判定用：ログファイルパス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrLogFilePath</returns>
    ''' <remarks><para>作成情報：2012/09/20 k.ueda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrLogFilePath() As String
        Get
            Return ppStrLogFilePath
        End Get
        Set(ByVal value As String)
            ppStrLogFilePath = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【更新判定用：対象システム】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnCheckSystemNmb</returns>
    ''' <remarks><para>作成情報：2012/10/11 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnCheckSystemNmb As Boolean
        Get
            Return ppBlnCheckSystemNmb
        End Get
        Set(ByVal value As Boolean)
            ppBlnCheckSystemNmb = value
        End Set
    End Property

    '【ADD】2014/04/03 e.okamura　取消時セット機器更新修正：START
    ''' <summary>
    ''' プロパティセット【更新条件：CI番号(取消時セットIDクリア用)】 
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntCINmbSetIDClear</returns>
    ''' <remarks><para>作成情報：2014/04/03 e.okamura
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntCINmbSetIDClear() As Integer
        Get
            Return ppIntCINmbSetIDClear
        End Get
        Set(ByVal value As Integer)
            ppIntCINmbSetIDClear = value
        End Set
    End Property
    '【ADD】2014/04/03 e.okamura　取消時セット機器更新修正：END

End Class
