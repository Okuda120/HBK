
''' <summary>
''' CommonDeclareHBK
''' </summary>
''' <remarks>HBK内の共通定数、変数定義モジュール
''' <para>作成情報：2012/06/08 t.fukuo
''' <p>改定情報：</p>
''' </para></remarks>
Public Module CommonDeclareHBK

    ''' <summary>
    ''' 汎用エラーメッセージ
    ''' </summary>
    ''' <remarks></remarks>
    Public Const HBK_E001 As String = "システムエラーが発生しました。" & vbCrLf & "システム管理者に連絡してください。" & vbCrLf

    ''' <summary>
    ''' サポセン番号採番時エラーメッセージ
    ''' </summary>
    ''' <remarks></remarks>
    Public Const HBK_E002 As String = "番号が上限値にまで達したため、新規番号を採番できません。"

    ''' <summary>
    ''' ロックされた画面表示時のメッセージ（メール作成）
    ''' </summary>
    ''' <remarks></remarks>
    Public Const HBK_E003 As String = "期限切れお知らせ対象の機器がロックされています。" & vbCrLf & _
                                      "最終お知らせ日の更新ができないため、メール作成できません。"

    ''' <summary>
    ''' ロック解除時エラーメッセージ
    ''' </summary>
    ''' <remarks></remarks>
    Public Const HBK_W001 As String = "ロックが解除されました。入力中の内容を以下に出力しています。" & vbCrLf & "{0}"

    ''' <summary>
    ''' 作業グループ変更確認メッセージ
    ''' </summary>
    ''' <remarks></remarks>
    Public Const HBK_W002 As String = "作業グループを変更します。よろしいですか？"

    ''' <summary>
    ''' ロック強制解除時の確認メッセージ
    ''' </summary>
    ''' <remarks></remarks>
    Public Const HBK_W003 As String = "ロックを解除しますか？"

    ''' <summary>
    ''' ロックされた画面表示時のメッセージ（CI共通）
    ''' </summary>
    ''' <remarks></remarks>
    Public Const HBK_I001 As String = "現在、{0}の{1}さんが編集中です。" & vbCrLf & "参照画面で表示します。"

    ''' <summary>
    ''' インシデント登録フォルダパス
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FORMAT_FOLDER_PATH As String = "Excel"

    ''' <summary>
    ''' INIファイルパス(相対パス)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const INI_FILE_PATH As String = "\Ver.ini"

    ''' <summary>
    ''' INIファイルセクション名
    ''' </summary>
    ''' <remarks></remarks>
    Public Const INI_FILE_SECTION_NAME As String = "VERSION" 'iniファイルセクション名

    ''' <summary>
    ''' INIファイルキー名
    ''' </summary>
    ''' <remarks></remarks>
    Public Const INI_FILE_KEY_NAME As String = "HBKVersion"  'iniファイルキー名

    '論理ドライブ
    Public ReadOnly DRIVES As String(,) = {{"Z:\"}, {"Y:\"}, {"X:\"}, {"W:\"}, {"V:\"}, {"U:\"}, {"T:\"}, {"S:\"}, {"R:\"}, {"Q:\"}, {"P:\"}, {"O:\"}, {"N:\"}, _
                                                            {"M:\"}, {"L:\"}, {"K:\"}, {"J:\"}, {"I:\"}, {"H:\"}, {"G:\"}, {"F:\"}, {"E:\"}, {"D:\"}, {"C:\"}, {"B:\"}, {"A:\"}}

    'NetUseユーザID
    Public ReadOnly NET_USE_USERID_LOCAL As String = "hbk_admin"
    'NetUseパスワード
    Public ReadOnly NET_USE_PASSWORD_LOCAL As String = "Hibiki2012"


    '登録完了メッセージ表示タイマー
    Public ReadOnly MSG_DISP_TIMER As Integer = System.Configuration.ConfigurationManager.AppSettings("MsgTimer")

    'フリーフラグ
    Public Const FREE_FLG_ON As String = "1"        'ON
    Public Const FREE_FLG_OFF As String = "0"       'OFF   
    Public Const FREE_FLG_ON_NM As String = "ON"
    Public Const FREE_FLG_OFF_NM As String = "OFF"

    'フリーフラグコンボボックス用配列
    Public FreeFlg(,) As String = {{"", ""}, {FREE_FLG_ON, FREE_FLG_ON_NM}, {FREE_FLG_OFF, FREE_FLG_OFF_NM}}

    'zoo参加有無
    Public Const ZOO_KBN_UNFIN As String = "0"          '未通知
    Public Const ZOO_KBN_FIN As String = "1"            '通知済
    Public Const ZOO_NM_UNFIN As String = "無"
    Public Const ZOO_NM_FIN As String = "有"
    Public ZOO_KBN(,) As String = {{"", ""}, {ZOO_KBN_UNFIN, ZOO_NM_UNFIN}, {ZOO_KBN_FIN, ZOO_NM_FIN}}

    'サービスセンター保管機
    Public Const SC_HOKANKBN_ON As String = "1"     'ON
    Public Const SC_HOKANKBN_OFF As String = "0"    'OFF   
    Public Const SC_HOKANKBN_ON_NM As String = "ON"
    Public Const SC_HOKANKBN_OFF_NM As String = "OFF"

    '導入廃棄完了
    Public Const INTRODUTDEL_KBN_ON As String = "1"     'ON
    Public Const INTRODUTDEL_KBN_OFF As String = "0"    'OFF   

    '導入タイプ
    Public Const INTRODUCT_KBN_LEASE As String = "1"            'リース
    Public Const INTRODUCT_KBN_KEIHI As String = "0"            '経費購入   
    Public Const INTRODUCT_KBN_LEASE_NM As String = "リース"
    Public Const INTRODUCT_KBN_KEIHI_NM As String = "経費購入"
    '導入タイプコンボボックス用配列
    Public IntroductKbn(,) As String = _
        { _
         {"", ""}, _
         {INTRODUCT_KBN_KEIHI, INTRODUCT_KBN_KEIHI_NM}, _
         {INTRODUCT_KBN_LEASE, INTRODUCT_KBN_LEASE_NM} _
        }

    '保証書有無
    Public Const HOSYO_UMU_ARI As String = "0"              '有
    Public Const HOSYO_UMU_NASHI As String = "1"            '無   
    Public Const HOSYO_UMU_FUMEI As String = "2"            '不明   
    Public Const HOSYO_UMU_ARI_NM As String = "有"
    Public Const HOSYO_UMU_NASHI_NM As String = "無"
    Public Const HOSYO_UMU_FUMEI_NM As String = "不明"
    '保証書コンボボックス用配列
    '[mod] 2013/03/14 y.ikushima 保証書定数統一のため修正 START
    'Public HosyoUmu(,) As String = _
    '    { _
    '     {"", ""}, _
    '     {HOSYO_UMU_ARI, HOSYO_UMU_ARI_NM}, _
    '     {HOSYO_UMU_NASHI, HOSYO_UMU_NASHI_NM}, _
    '     {HOSYO_UMU_FUMEI, HOSYO_UMU_FUMEI_NM} _
    '    }
    Public HosyoUmu(,) As String = _
    { _
     {"", ""}, _
     {HOSYO_UMU_ARI, HOSYO_UMU_NASHI_NM}, _
     {HOSYO_UMU_NASHI, HOSYO_UMU_ARI_NM}, _
     {HOSYO_UMU_FUMEI, HOSYO_UMU_FUMEI_NM} _
    }
    '[mod] 2013/03/14 y.ikushima 保証書定数統一のため修正 END

    '処理モード
    Public Const PROCMODE_NEW As String = "1"              '新規登録モード
    Public Const PROCMODE_EDIT As String = "2"             '編集モード
    Public Const PROCMODE_REF As String = "3"              '参照モード
    Public Const PROCMODE_RIREKI As String = "4"           '履歴モード

    '更新ファイル区分
    Public Const UPLOAD_FILE_INCIDENT As String = "1"      'インシデント関連
    Public Const UPLOAD_FILE_MEETING As String = "2"       '会議関連
    Public Const UPLOAD_FILE_PROBLEM As String = "3"       '問題関連
    Public Const UPLOAD_FILE_CHANGE As String = "4"        '変更関連
    Public Const UPLOAD_FILE_RELEASE As String = "5"       'リリース関連

    'フォーマット格納フォルダ名
    Public Const FORMAT_DIR_UNLOCKEDLOG As String = "UnLockedLogFormat"     'ロック解除時ログ出力用フォーマット格納用

    '出力フォルダ名
    Public Const OUTPUT_DIR_UNLOCKEDLOG As String = "UnlockLog"             'ロック解除時ログ出力用
    Public Const OUTPUT_DIR_IMPORTERRLOG As String = "ImportErrLog"         'インポートファイルエラーログ出力用
    Public Const OUTPUT_DIR_TEMP As String = "Temp"         'ファイルオープン一時保存用

    'ロック解除時ログフォーマットファイル名
    Public Const FILE_UNLOCKLOG_SUPPORT As String = "ロック解除_サポセン機器登録.log"      'サポセン
    Public Const FILE_UNLOCKLOG_SYSTEM As String = "ロック解除_システム登録.log"           'システム
    Public Const FILE_UNLOCKLOG_INTRODUCT As String = "ロック解除_導入.log"                '導入
    Public Const FILE_UNLOCKLOG_KIKI As String = "ロック解除_部所有機器登録.log"           '部所有機器登録
    Public Const FILE_UNLOCKLOG_DOC As String = "ロック解除_文書登録.log"                  '文書登録
    Public Const FILE_UNLOCKLOG_INCIDENT As String = "ロック解除_インシデント登録.log"     'インシデント
    Public Const FILE_UNLOCKLOG_QUESTION As String = "ロック解除_問題登録.log"             '問題登録
    Public Const FILE_UNLOCKLOG_CHANGE As String = "ロック解除_変更登録.log"               '変更登録
    Public Const FILE_UNLOCKLOG_RELEASE As String = "ロック解除_リリース登録.log"          'リリース登録

    'pcany_chfファイル名
    Public Const PCANY_CHF_NAME As String = "c_auto.CHF"
    Public Const OUTPUT_DIR_PCANY As String = "pcAnywhereBatch"         'pcAnywhere出力用

    '[ADD]2016/03/08 e.okamura LAPLINK遠隔ボタン追加 START
    'LAPLINKファイル名
    Public Const OUTPUT_DIR_LAPLINK As String = "LAPLINKBatch"          'LAPLINK出力用
    '[ADD]2016/03/08 e.okamura LAPLINK遠隔ボタン追加 END

    'Settingファイル名
    Public Const SETTING_CONFIGN_NAME As String = "setting.config"

    'フォーム背景色System.Drawing
    Private FORM_BACKCOLOR_HONBAN As Color = SystemColors.Control            '本番環境：灰色
    Private FORM_BACKCOLOR_KENSHOU As Color = Color.FromArgb(192, 255, 192)  '検証環境：緑

    'セル背景色
    Private CELL_BACKCOLOR_DARKGRAY As Color = Color.DarkGray                    '濃灰色
    Private CELL_BACKCOLOR_GRAY As Color = Color.FromArgb(244, 244, 244)         '灰色

    'ログイン保持情報
    Private ppUserId As String              'ユーザＩＤ
    Private ppUserName As String            'ユーザ氏名
    Private ppWorkGroupCD As String         '作業グループＣＤ
    Private ppWorkGroupName As String       '作業グループ名
    Private ppWorkUserGroupAuhority As String   '作業ユーザーグループ権限
    Private ppGroupDataLIst As New List(Of StructGroupData) 'グループ情報リスト
    Private ppConfigurationFlag As String   '環境設定フラグ
    Private ppUnlockTime As String          'ロック解除時間
    Private ppSearchMsgCount As Integer     '検索表示確認件数
    Private ppFileStorageRootPath As String 'ファイルストレージルートパス
    Private ppFileManagePath As String      'ファイル管理パス
    Private ppOutputLogSavePath As String   '出力ログ退避パス
    Private ppEditStartDate As DateTime     '編集開始日時
    Private ppEditId As String              '編集者ＩＤ
    Private ppEditGroupCd As String         '編集者グループＣＤ
    Private ppLastProcessKbn As String      '最終作業プロセス区分
    Private ppLastProcessNmb As String      '最終作業プロセス番号
    Private ppUserPass As String            'ユーザパスワード

    '[add] 2012/09/24 NetUse 仕様変更のため修正START
    'NetUseID・パスワード情報
    Private ppStrNetUseUserID As String         'NetUseUserID
    Private ppStrNetUsePassword As String         'NetUsePassword
    '[add] 2012/09/24 NetUse 仕様変更のため修正END

    '各グループ情報をまとめた構造体
    Public Structure StructGroupData
        Dim strGroupCd As String             'グループＣＤ
        Dim strGroupName As String           'グループ名称
        Dim strUserGroupAuhority As String   'ユーザグループ権限
    End Structure

    '削除データ表示
    Public Const JTIFLG_ON As String = "1"
    Public Const JTIFLG_OFF As String = "0"
    Public Const DELDATA_DISPLAY_NM As String = "○"

    ' 検索画面引数（検索条件）
    Public Const SPLIT_MODE_ONE As String = "0"
    Public Const SPLIT_MODE_AND As String = "1"
    Public Const SPLIT_MODE_OR As String = "2"

    '対応関係者チェック
    Public Const KANKEI_CHECK_NONE As Integer = 0                   '参照不可
    Public Const KANKEI_CHECK_REF As Integer = 1                    '参照
    Public Const KANKEI_CHECK_EDIT As Integer = 2                   '編集

    'CI種別
    Public Const CI_TYPE_SYSTEM As String = "001"                   'システム
    Public Const CI_TYPE_DOC As String = "002"                      '文書
    Public Const CI_TYPE_SUPORT As String = "003"                   'サポセン機器
    Public Const CI_TYPE_KIKI As String = "004"                     '部所有機器

    'CI種別名
    Public Const CI_TYPE_SYSTEM_NM As String = "システム"           'システム
    Public Const CI_TYPE_DOC_NM As String = "文書"                  '文書
    Public Const CI_TYPE_SUPORT_NM As String = "サポセン機器"       'サポセン機器
    Public Const CI_TYPE_KIKI_NM As String = "部所有機器"           '部所有機器

    'サポセン機器種別
    Public Const KIND_CD_SAP_USBTOKEN As String = "311"             'USBトークン（UKY）

    '接続区分
    Public Const CONNECT_LOGIN As String = "ログイン"               'ログイン
    Public Const CONNECT_LOGOUT As String = "ログアウト"            'ログアウト

    'メッセージボックスタイトル
    Public Const TITLE_INFO As String = "メッセージ"
    Public Const TITLE_WARNING As String = "警告"
    Public Const TITLE_ERROR As String = "エラー"

    'ファイル拡張子
    Public Const EXTENTION_XLS As String = ".xls"
    Public Const EXTENTION_XLSX As String = ".xlsx"
    Public Const EXTENTION_CSV As String = ".csv"

    '区分
    Public Const KBN_GROUP As String = "G"                          'グループ
    Public Const KBN_USER As String = "U"                           'ユーザー

    'フラグ
    Public Const FLG_ON As String = "1"                                 'オン
    Public Const FLG_OFF As String = "0"                                'オフ
    Public Const FLG_ON_NM As String = "ON"
    Public Const FLG_OFF_NM As String = "OFF"

    '指示書フラグ
    Public Const SHIJISYO_FLG_ON As String = "1"                        '指示書あり
    Public Const SHIJISYO_FLG_OFF As String = "0"                       '指示書なし

    '期限切れ条件ユーザーID（選択フラグ）
    Public Const KIGENCOND_USERID_ON As String = "1"                    'ユーザーID選択
    Public Const KIGENCOND_USERID_OFF As String = "0"                   'ユーザーID未選択

    'セットアップフラグ
    Public Const SETUP_FLG_ON As String = "1"                           'セットアップ有り
    Public Const SETUP_FLG_OFF As String = "0"                          'セットアップ無し

    '交換フラグ
    Public Const CHANGE_FLG_ON As String = "1"                          '交換
    Public Const CHANGE_FLG_OFF As String = "0"                         '未交換

    '完了フラグ
    Public Const COMP_FLG_ON As String = "1"                            '完了
    Public Const COMP_FLG_OFF As String = "0"                           '未完了

    '取消フラグ
    Public Const CANCEL_FLG_ON As String = "1"                          '取消
    Public Const CANCEL_FLG_OFF As String = "0"                         '未取消

    '廃棄区分
    Public Const HAIKIKBN_KADOU As String = "001"                       '稼動
    Public Const HAIKIKBN_TAIHI_YOTEI As String = "002"                 '退避予定
    Public Const HAIKIKBN_TAIHI_ZUMI As String = "003"                  '退避済

    '機器ステータス区分
    Public Const KIKISTATEKBN_KIKI_RIYOKEITAI As String = "001"         '機器利用形態
    Public Const KIKISTATEKBN_IP_WARIATE As String = "002"              'IP割当種類
    Public Const KIKISTATEKBN_DNS_REG As String = "003"                 'DNS登録

    '機器利用区分
    Public Const KIKIUSEKBN_SET As String = "0"                         '設置品
    Public Const KIKIUSEKBN_RENTAL As String = "1"                      '貸出品

    '機器利用形態
    Public Const KIKI_RIYOKEITAI_KEIZOKU As String = "101"              '継続利用
    Public Const KIKI_RIYOKEITAI_ICHIJI_RIYO As String = "102"          '一時利用（貸出）

    'IP割当種類
    Public Const IP_DYNAMIC_DHCP As String = "201"                      '「変動（DHCP自動取得）」
    Public Const IP_STATIC_AUTO As String = "202"                       '「自動固定（DHCP取得後固定）」
    Public Const IP_STATIC_MANUAL As String = "203"                     '「手動固定（スタティック固定）」

    'インシデント機器状態設定文言：機器状態
    Public Const KIKISTATE_INPUT As String = "機状有"                   '入力有
    Public Const KIKISTATE_NO_INPUT As String = "機状無"                '入力無

    'インシデント機器状態設定文言：オプションソフト
    Public Const OPTSOFT_INPUT As String = "OP有"                       '入力有（データ件数1件以上）
    Public Const OPTSOFT_NO_INPUT As String = "OP無"                    '入力無（データ件数0件）

    'ソフト区分
    Public Const SOFTKBN_OS As String = "001"                           'OS
    Public Const SOFTKBN_OPTIONSOFT As String = "002"                   'オプションソフト
    Public Const SOFTKBN_UNTIVIRUSSOFT As String = "003"                'ウイルス対策ソフト

    'CIステータスコード
    'システム
    Public Const CI_STATUS_SYSTEM_JUNBI As String = "101"               '準備（受入中）
    Public Const CI_STATUS_SYSTEM_KADOUCHU As String = "102"            '稼働中
    Public Const CI_STATUS_SYSTEM_HAISHIZUMI As String = "103"          '廃止済
    '文章
    Public Const CI_STATUS_DOC_KADOUCHU As String = "201"               '稼動中
    Public Const CI_STATUS_DOC_KAIBANCHU As String = "202"              '改版中
    Public Const CI_STATUS_DOC_HAIKIZUMI As String = "203"              '廃棄済
    'サポセン機器
    Public Const CI_STATUS_SUPORT_SYOKI As String = "301"               '初期
    Public Const CI_STATUS_SUPORT_MISETTEI As String = "302"            '未設定
    Public Const CI_STATUS_SUPORT_SETUPMACHI As String = "303"          'セットアップ待
    Public Const CI_STATUS_SUPORT_SYUKKOKA As String = "304"            '出庫可
    Public Const CI_STATUS_SUPORT_CHINPUKAMACHI As String = "305"       '陳腐化待
    Public Const CI_STATUS_SUPORT_SECCHIMACHI As String = "306"         '設置待
    Public Const CI_STATUS_SUPORT_KADOUCHU As String = "307"            '稼働中
    Public Const CI_STATUS_SUPORT_TSUIKASETTEIMACHI As String = "308"   '追加設定待
    Public Const CI_STATUS_SUPORT_TEKKYOMACHI As String = "309"         '撤去待
    Public Const CI_STATUS_SUPORT_KOSYO As String = "310"               '故障
    Public Const CI_STATUS_SUPORT_KOSYOMACHI As String = "311"          '故障待
    Public Const CI_STATUS_SUPORT_SYUURIMACHI As String = "312"         '修理待
    Public Const CI_STATUS_SUPORT_SHIZAIKO As String = "313"            '死在庫
    Public Const CI_STATUS_SUPORT_KATAZUKEMACHI As String = "314"       '片付待
    Public Const CI_STATUS_SUPORT_FUNSHITSU As String = "315"           '紛失
    Public Const CI_STATUS_SUPORT_FUNSHITSUMACHI As String = "316"      '紛失待
    Public Const CI_STATUS_SUPORT_HAIKIYOTEI As String = "317"          '廃棄予定
    Public Const CI_STATUS_SUPORT_HAIKIJUNBIMACHI As String = "318"     '廃棄準備待
    Public Const CI_STATUS_SUPORT_HAIKIZUMI As String = "319"           '廃棄済
    Public Const CI_STATUS_SUPORT_HAIKIMACHI As String = "320"          '廃棄待
    Public Const CI_STATUS_SUPORT_REUSE As String = "321"               'リユース
    Public Const CI_STATUS_SUPORT_FUKKIMACHI As String = "322"          '復帰待
    '部所有機器
    Public Const CI_STATUS_KIKI_RIYOUCHU As String = "401"              '利用中
    Public Const CI_STATUS_KIKI_TEISHI As String = "402"                '停止

    'プロセス区分コード
    Public Const PROCESS_TYPE_INCIDENT As String = "001"                'インシデント
    Public Const PROCESS_TYPE_QUESTION As String = "002"                '問題
    Public Const PROCESS_TYPE_CHANGE As String = "003"                  '変更
    Public Const PROCESS_TYPE_RELEASE As String = "004"                 'リリース

    'プロセス区分名
    Public Const PROCESS_TYPE_INCIDENT_NAME As String = "インシデント"
    Public Const PROCESS_TYPE_QUESTION_NAME As String = "問題"
    Public Const PROCESS_TYPE_CHANGE_NAME As String = "変更"
    Public Const PROCESS_TYPE_RELEASE_NAME As String = "リリース"

    'プロセス区分名略称
    Public Const PROCESS_TYPE_INCIDENT_NAME_R As String = "イ"
    Public Const PROCESS_TYPE_QUESTION_NAME_R As String = "問"
    Public Const PROCESS_TYPE_CHANGE_NAME_R As String = "変"
    Public Const PROCESS_TYPE_RELEASE_NAME_R As String = "リ"

    'プロセス区分コンボボックス用配列
    Public ProcessType(,) As String = _
        { _
         {"", ""}, _
         {PROCESS_TYPE_INCIDENT, PROCESS_TYPE_INCIDENT_NAME}, _
         {PROCESS_TYPE_QUESTION, PROCESS_TYPE_QUESTION_NAME}, _
         {PROCESS_TYPE_CHANGE, PROCESS_TYPE_CHANGE_NAME}, _
         {PROCESS_TYPE_RELEASE, PROCESS_TYPE_RELEASE_NAME} _
        }

    'プロセスステータスコード
    'インシデント
    Public Const PROCESS_STATUS_INCIDENT_MIKAKUNIN As String = "101"            '未確認
    Public Const PROCESS_STATUS_INCIDENT_MUSI As String = "102"                 '無視
    Public Const PROCESS_STATUS_INCIDENT_KEIZOKU As String = "103"              '継続
    Public Const PROCESS_STATUS_INCIDENT_GAIBUIRAICHU As String = "104"         '外部依頼中
    Public Const PROCESS_STATUS_INCIDENT_KANRYOU As String = "105"              '完了
    '問題
    Public Const PROCESS_STATUS_QUESTION_MICHAKUSYU As String = "201"           '未着手
    Public Const PROCESS_STATUS_QUESTION_CHOSACHU As String = "202"             '調査中
    Public Const PROCESS_STATUS_QUESTION_GAIBUCHOUSAIRAICHU As String = "203"   '外部調査依頼中
    Public Const PROCESS_STATUS_QUESTION_HOUSHINKENTOHCHU As String = "204"     '方針検討中
    Public Const PROCESS_STATUS_QUESTION_TAIOHCHU As String = "205"             '対応中
    Public Const PROCESS_STATUS_QUESTION_GAIBUTAIOHIRAICHU As String = "206"    '外部対応依頼中
    Public Const PROCESS_STATUS_QUESTION_KANRYOKAKUNINCHU As String = "207"     '完了確認中
    Public Const PROCESS_STATUS_QUESTION_KANRYOH As String = "208"              '完了
    Public Const PROCESS_STATUS_QUESTION_MIKAIKETSUKANRYOH As String = "209"    '未解決完了
    '変更
    Public Const PROCESS_STATUS_CHANGE_MICHAKUSYU As String = "301"             '未着手
    Public Const PROCESS_STATUS_CHANGE_JUNBICHU As String = "302"               '準備中(見積中)
    Public Const PROCESS_STATUS_CHANGE_SHONINIRAICHU As String = "303"          '承認依頼中
    Public Const PROCESS_STATUS_CHANGE_RELEASEMACHI As String = "304"           'リリース待
    Public Const PROCESS_STATUS_CHANGE_KANRYOU As String = "305"                '完了
    Public Const PROCESS_STATUS_CHANGE_MIJISHIKANRYOU As String = "306"         '未実施完了
    'リリース
    Public Const PROCESS_STATUS_RELEASE_MICHAKUSYU As String = "401"             '未着手
    Public Const PROCESS_STATUS_RELEASE_CHOSACHU As String = "402"               '調整中
    Public Const PROCESS_STATUS_RELEASE_SYONINIRAICHU As String = "403"          '承認依頼中
    Public Const PROCESS_STATUS_RELEASE_SAGYOUMACHI As String = "404"            'リリース作業待
    Public Const PROCESS_STATUS_RELEASE_SAGYOUCHU As String = "405"              'リリース作業中
    Public Const PROCESS_STATUS_RELEASE_KANRYOSYONINMACHI As String = "406"      '完了承認待
    Public Const PROCESS_STATUS_RELEASE_KANRYO As String = "407"                 '完了
    Public Const PROCESS_STATUS_RELEASE_MIJISSHIKANRYO As String = "408"         '未実施完了

    'サポセン機器タイプコード
    Public Const SAP_TYPE_NORMAL As String = "001"        'Normal（N）
    Public Const SAP_TYPE_PRISM As String = "002"         'PrismPress（P）
    Public Const SAP_TYPE_WINDOWS7 As String = "003"      'Windows7（7）
    Public Const SAP_TYPE_KAIKEI As String = "004"        '会計（K）


    '変更理由登録画面引数
    Public Const REG_MODE_BLANK As String = "0"           '引渡しなし
    Public Const REG_MODE_PACKAGE As String = "1"         '一括更新から遷移
    Public Const REG_MODE_HISTORY As String = "2"         '履歴モードから遷移
    Public Const REG_MODE_INCIDENT As String = "3"        'インシデントから引渡し

    '作業コード
    Public Const WORK_CD_INTRODUCT As String = "001"      '導入
    Public Const WORK_CD_PACKAGE As String = "002"        '一括更新
    Public Const WORK_CD_SETUP As String = "003"          'セットアップ
    Public Const WORK_CD_OBSOLETE As String = "004"       '陳腐化
    Public Const WORK_CD_SET As String = "005"            '設置
    Public Const WORK_CD_ADDCONFIG As String = "006"      '追加設定
    Public Const WORK_CD_REMOVE As String = "007"         '撤去
    Public Const WORK_CD_BREAKDOWN As String = "008"      '故障
    Public Const WORK_CD_REPAIR As String = "009"         '修理
    Public Const WORK_CD_TIDYUP As String = "010"         '片付
    Public Const WORK_CD_PREDISPOSE As String = "011"     '廃棄準備
    Public Const WORK_CD_DISPOSE As String = "012"        '廃棄
    Public Const WORK_CD_BELOST As String = "013"         '紛失
    Public Const WORK_CD_REVERT As String = "014"         '復帰

    '作業区分コード
    Public Const WORK_KBN_CD_COMPLETE As String = "001"   '完了
    Public Const WORK_KBN_CD_CANCEL As String = "002"     '取消
    Public Const WORK_KBN_CD_PREPAIR As String = "003"    '準備

    'ダイアログ戻り値
    Public Const DIALOG_RETURN_CANCEL As Integer = 0        'キャンセルボタン（×ボタン）
    Public Const DIALOG_RETURN_OK As Integer = 1            'OKボタン

    '出力形式選択ダイアログ戻り値
    Public Const OUTPUT_RETURN_CANCEL As Integer = 0        'キャンセルボタン（×ボタン）
    Public Const OUTPUT_RETURN_PRINTER As Integer = 1       'プリンター出力
    Public Const OUTPUT_RETURN_FILE As Integer = 2          'ファイル出力
    Public Const OUTPUT_RETURN_PRINTER_FILE As Integer = 3  'プリンター＆ファイル出力

    '最終お知らせ日更新区分（メールテンプレートマスタ選択画面戻り値）
    Public Const UPDATE_LASTINFODT_KBN_CANCEL As Integer = 0        '何も処理しない
    Public Const UPDATE_LASTINFODT_KBN_UPDATE As Integer = 1        '更新し、メール作成する
    Public Const UPDATE_LASTINFODT_KBN_NOTUPDATE As Integer = 2     '更新せず、メール作成のみ

    'ファイル出力先フォルダ名
    Public Const OUTPUT_FILE_DIR_DOC As String = "構成管理"                     '文書登録
    Public Const OUTPUT_FILE_DIR_INCIDENT As String = "インシデント管理"        'インシデント登録
    Public Const OUTPUT_FILE_DIR_PROBLEM As String = "問題管理"                 '問題登録
    Public Const OUTPUT_FILE_DIR_CHANGE As String = "変更管理"                  '変更登録
    Public Const OUTPUT_FILE_DIR_RELEASE As String = "リリース管理"             'リリース登録
    Public Const OUTPUT_FILE_DIR_MEETING As String = "会議管理"                 '会議登録

    'バッチ：登録者／更新者グループCD
    Public SYS_GROUPCD As String = "SYS"
    'バッチ：登録者／更新者ユーザーID
    Public SYS_USERID As String = "SYSTEM"

    'メール作成：パーティション文字
    Public Const MAILPARTITION As String = "----------------------------------------------"
    'メール作成：区切り文字（閉じ括弧）
    Public Const END_CHAR As String = "]"

    'メール作成で最終お知らせ日更新時登録理由
    Public Const REGREASON_TEXT_UPDATE_LASTINFODT As String = "期限切れメール送付による最終お知らせ日の更新"

    '機器情報
    Public Const KIKIINF_SPLIT_SIMBOL As String = "/"      '機器情報区切り文字
    Public Const KIKIINF_INITIAL_LENGTH As String = "10"   '機器情報切り出し頭文字数
    Public Const IPUSECD_STATIC_WORD As String = "IP"      'IP割当区分固定文言

    ''' <summary>
    ''' ログイン区分（LOGIN_LTB用）
    ''' </summary>
    Public Const CONNECT_LOGIN_KBN As String = "1"
    ''' <summary>
    ''' ログアウト区分(LOGIN_LTB用）
    ''' </summary>
    Public Const CONNECT_LOGOUT_KBN As String = "2"
    ''' <summary>
    ''' 環境設定フラグ＿検証環境
    ''' </summary>
    Public Const ENVIRONMENT_VALIDATION As String = "0"
    ''' <summary>
    ''' 環境設定フラグ＿本番環境
    ''' </summary>
    Public Const ENVIRONMENT_PRODUCTION As String = "1"


    '構成(CI)番号採番SQL
    Public Const GET_NEXTVAL_CI_NO As String = "SELECT NEXTVAL('HBKS0001') AS CINmb, Now() AS SysDate "

    'インシデント番号採番SQL
    Public Const GET_NEXTVAL_INCIDENT_NO As String = "SELECT NEXTVAL('HBKS0002') AS IncNmb, Now() AS SysDate "

    '問題番号採番SQL　※DB定義確定次第別名要設定
    Public Const GET_NEXTVAL_MONDAI_NO As String = "SELECT NEXTVAL('HBKS0003') AS PrbNmb, Now() AS SysDate "

    '変更番号採番SQL　※DB定義確定次第別名要設定
    Public Const GET_NEXTVAL_HENKOU_NO As String = "SELECT NEXTVAL('HBKS0004') AS ChgNmb, Now() AS SysDate"

    'リリース番号採番SQL　※DB定義確定次第別名要設定
    Public Const GET_NEXTVAL_RELEASE_NO As String = "SELECT NEXTVAL('HBKS0005') AS RelNmb, Now() AS SysDate"

    '導入番号採番SQL
    Public Const GET_NEXTVAL_INTRODUCT_NO As String = "SELECT NEXTVAL('HBKS0006') AS IntroductNmb, Now() AS SysDate "

    'ファイル管理番号採番SQL
    Public Const GET_NEXTVAL_FILEMNG_NO As String = "SELECT NEXTVAL('HBKS0007') AS FileMngNmb, Now() AS SysDate "

    '会議番号採番SQL
    Public Const GET_NEXTVAL_MEETING_NO As String = "SELECT NEXTVAL('HBKS0008') AS MeetingNmb, Now() AS SysDate "

    'セットID採番SQL
    Public Const GET_NEXTVAL_SETKIKI_ID As String = "SELECT NEXTVAL('HBKS0009') AS SetKikiID, Now() AS SysDate "

    'テンプレート番号採番SQL
    Public Const GET_NEXTVAL_TEMPLATE_NO As String = "SELECT NEXTVAL('HBKS0010') AS TemplateNmb, Now() AS SysDate "

    'イメージ番号採番SQL
    Public Const GET_NEXTVAL_IMAGE_NO As String = "SELECT NEXTVAL('HBKS0011') AS ImageNmb,Now() AS SysData "

    '設置部署コード採番SQL
    Public Const GET_NEXTVAL_SETBUSYO_CD As String = "SELECT NEXTVAL('HBKS0012') AS SetBusyoCD, Now() AS SysDate "

    'インシデントSM連携指示Seq採番SQL
    Public Const GET_NEXTVAL_INCIDENTSMRENKEI_SEQ As String = "SELECT NEXTVAL('HBKS0013') AS Seq, Now() AS SysDate "

    'セット機器管理番号採番SQL
    Public Const GET_NEXTVAL_SETKIKIMNGNMB As String = "SELECT NEXTVAL('HBKS0014') AS SetKikiMngNmb, Now() AS SysDate "

    '論理テーブル名
    Public Const TBNM_CI_INFO_TB As String = "CI共通情報"                           'CI共通情報
    Public Const TBNM_CI_INFO_RTB As String = "CI共通情報履歴"                      'CI共通情報履歴
    Public Const TBNM_CI_DOC_TB As String = "CI文書"                                'CI文書
    Public Const TBNM_CI_DOC_RTB As String = "CI文書履歴"                           'CI文書履歴
    Public Const TBNM_CI_INFO_TMP As String = "CI共通情報"                'CI共通情報
    Public Const TBNM_INTRODUCT_TB As String = "導入"                               '導入
    Public Const TBNM_CI_BUY_TB As String = "CI部所有機器"                          'CI部所有機器
    Public Const TBNM_CI_BUY_RTB As String = "CI部所有機器履歴"                     'CI部所有機器履歴
    Public Const TBNM_PROCESSSTATE_MTB As String = "プロセスステータスマスター"     'プロセスステータスマスター
    Public Const TBNM_PROBLEM_CASE_MTB As String = "問題発生原因マスター"           '問題発生原因マスター
    Public Const TBNM_GRP_MTB As String = "グループマスター"                        'グループマスター
    Public Const TBNM_WORKSTATE_MTB As String = "作業ステータスマスター"            '作業ステータスマスター
    Public Const TBNM_MEETING_TB As String = "会議情報"                             '会議情報
    Public Const TBNM_UKETSUKEWAY_MTB As String = "受付手段マスター"                '受付手段マスター
    Public Const TBNM_INCIDENT_KIND_MTB As String = "インシデント種別マスター"      'インシデント種別マスター
    Public Const TBNM_DOMAINMTB As String = "ドメインマスター"                      'ドメインマスター
    Public Const TBNM_KEIKA_KIND_MTB As String = "経過種別マスター"                 '経過種別マスター
    Public Const TBNM_WORK_MTB As String = "作業マスター"                           '作業マスター
    Public Const TBNM_MAIL_TEMPLATE_MTB As String = "メールテンプレートマスター"    'メールテンプレートマスター
    Public Const TBNM_HBKUSR_MTB As String = "ひびきユーザーマスター"               'ひびきユーザーマスター

    '画面呼び元情報
    Public Const SCR_CALLMOTO_HOKA As Integer = 0                                   '呼び元画面：検索一覧以外
    Public Const SCR_CALLMOTO_ICHIRAN As Integer = 1                                '呼び元画面：検索一覧
    Public Const SCR_CALLMOTO_REG As Integer = 2                                    '呼び元画面：画面遷移でない別の画面
    '-- 2017/8/30 e.okuda Add Strat --
    Public Const SCR_CALLMOTO_MENU As Integer = 99                                  '呼び元画面：メニュー画面
    '-- 2017/8/30 e.okuda Add End --

    ''' <summary>
    ''' プロパティ【フォーム背景色：検証環境】読取専用
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropBackColorGRAY</returns>
    ''' <remarks><para>作成情報：2012/06/07 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public ReadOnly Property PropBackColorHONBAN()
        Get
            Return FORM_BACKCOLOR_HONBAN
        End Get
    End Property

    ''' <summary>
    ''' プロパティ【フォーム背景色：緑】読取専用
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropBackColorGREEN</returns>
    ''' <remarks><para>作成情報：2012/06/07 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public ReadOnly Property PropBackColorKENSHOU()
        Get
            Return FORM_BACKCOLOR_KENSHOU
        End Get
    End Property

    ''' <summary>
    ''' プロパティ【セル背景色：濃灰色】読取専用
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropCellBackColorGRAY</returns>
    ''' <remarks><para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public ReadOnly Property PropCellBackColorDARKGRAY()
        Get
            Return CELL_BACKCOLOR_DARKGRAY
        End Get
    End Property

    ''' <summary>
    ''' プロパティ【セル背景色：灰色】読取専用
    ''' </summary>
    ''' <value></value>
    ''' <returns> PropCellBackColorGRAY</returns>
    ''' <remarks><para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public ReadOnly Property PropCellBackColorGRAY()
        Get
            Return CELL_BACKCOLOR_GRAY
        End Get
    End Property


    ''' <summary>
    ''' ユーザＩＤのプロパティ
    ''' </summary>
    ''' <remarks>ユーザＩＤへアクセスを行うプロパティ
    ''' <para>作成情報：2012/05/24 matsuoka
    ''' </para></remarks>
    Public Property PropUserId() As String
        Get
            Return ppUserId
        End Get
        Set(ByVal value As String)
            ppUserId = value
        End Set
    End Property
    ''' <summary>
    ''' ユーザ氏名のプロパティ
    ''' </summary>
    ''' <remarks>ユーザ氏名へアクセスを行うプロパティ
    ''' <para>作成情報：2012/05/24 matsuoka
    ''' </para></remarks>
    Public Property PropUserName() As String
        Get
            Return ppUserName
        End Get
        Set(ByVal value As String)
            ppUserName = value
        End Set
    End Property
    ''' <summary>
    ''' 作業グループＣＤのプロパティ
    ''' </summary>
    ''' <remarks>作業グループＣＤへのアクセスを行うプロパティ
    ''' <para>作成情報：2012/06/08 matsuoka
    ''' </para></remarks>
    Public Property PropWorkGroupCD() As String
        Get
            Return ppWorkGroupCD
        End Get
        Set(ByVal value As String)
            ppWorkGroupCD = value
        End Set
    End Property
    ''' <summary>
    ''' 作業グループ名へプロパティ
    ''' </summary>
    ''' <remarks>作業グループ名へのアクセスを行うプロパティ
    ''' <para>作成情報：2012/06/08 matsuoka
    ''' </para></remarks>
    Public Property PropWorkGroupName() As String
        Get
            Return ppWorkGroupName
        End Get
        Set(ByVal value As String)
            ppWorkGroupName = value
        End Set
    End Property
    ''' <summary>
    ''' 作業ユーザグループ権限のプロパティ
    ''' </summary>
    ''' <remarks>作業ユーザグループ権限へのアクセスを行うプロパティ
    ''' <para>作成情報：2012/06/08 matsuoka
    ''' </para></remarks>
    Public Property PropWorkUserGroupAuhority() As String
        Get
            Return ppWorkUserGroupAuhority
        End Get
        Set(ByVal value As String)
            ppWorkUserGroupAuhority = value
        End Set
    End Property
    ''' <summary>
    ''' グループ情報構造体リストのプロパティ
    ''' </summary>
    ''' <remarks>グループ情報構造体のリストへのアクセスを行うプロパティ
    ''' <para>作成情報：2012/05/24 matsuoka
    ''' </para></remarks>
    Public Property PropGroupDataList() As List(Of StructGroupData)
        Get
            Return ppGroupDataLIst
        End Get
        Set(ByVal value As List(Of StructGroupData))
            ppGroupDataLIst = value
        End Set
    End Property
    ''' <summary>
    ''' 環境設定フラグのプロパティ
    ''' </summary>
    ''' <remarks>環境設定フラグのアクセスを行うプロパティ
    ''' <para>作成情報：2012/05/24 matsuoka
    ''' </para></remarks>
    Public Property PropConfigrationFlag() As String
        Get
            Return ppConfigurationFlag
        End Get
        Set(ByVal value As String)
            ppConfigurationFlag = value
        End Set
    End Property
    ''' <summary>
    ''' ロック解除時間のプロパティ
    ''' </summary>
    ''' <remarks>ロック解除時間のアクセスを行うプロパティ
    ''' <para>作成情報：2012/06/08 matsuoka
    ''' </para></remarks>
    Public Property PropUnlockTime() As String
        Get
            Return ppUnlockTime
        End Get
        Set(ByVal value As String)
            ppUnlockTime = value
        End Set
    End Property
    ''' <summary>
    ''' 検索表示確認件数のプロパティ
    ''' </summary>
    ''' <remarks>検索表示確認件数のアクセスを行うプロパティ
    ''' <para>作成情報：2012/06/08 matsuoka
    ''' </para></remarks>
    Public Property PropSearchMsgCount() As Integer
        Get
            Return ppSearchMsgCount
        End Get
        Set(ByVal value As Integer)
            ppSearchMsgCount = value
        End Set
    End Property
    ''' <summary>
    ''' ファイルストレージルートパスのプロパティ
    ''' </summary>
    ''' <remarks>ファイルストレージルートパスのアクセスを行うプロパティ
    ''' <para>作成情報：2012/05/30 matsuoka
    ''' </para></remarks>
    Public Property PropFileStorageRootPath() As String
        Get
            Return ppFileStorageRootPath
        End Get
        Set(ByVal value As String)
            ppFileStorageRootPath = value
        End Set
    End Property
    ''' <summary>
    ''' ファイル管理パスのプロパティ
    ''' </summary>
    ''' <remarks>ファイル管理パスのアクセスを行うプロパティ
    ''' <para>作成情報：2012/05/30 matsuoka
    ''' </para></remarks>
    Public Property PropFileManagePath() As String
        Get
            Return ppFileManagePath
        End Get
        Set(ByVal value As String)
            ppFileManagePath = value
        End Set
    End Property
    ''' <summary>
    ''' 出力ログ退避パスのプロパティ
    ''' </summary>
    ''' <remarks>出力ログ退避パスのアクセスを行うプロパティ
    ''' <para>作成情報：2012/05/30 matsuoka
    ''' </para></remarks>
    Public Property PropOutputLogSavePath() As String
        Get
            Return ppOutputLogSavePath
        End Get
        Set(ByVal value As String)
            ppOutputLogSavePath = value
        End Set
    End Property
    ''' <summary>
    ''' 編集開始日時のプロパティ
    ''' </summary>
    ''' <remarks>編集開始日時のアクセスを行うプロパティ
    ''' <para>作成情報：2012/05/30 matsuoka
    ''' </para></remarks>
    Public Property PropEditStartDate() As DateTime
        Get
            Return ppEditStartDate
        End Get
        Set(ByVal value As DateTime)
            ppEditStartDate = value
        End Set
    End Property
    ''' <summary>
    ''' 編集者ＩＤのプロパティ
    ''' </summary>
    ''' <remarks>編集者ＩＤのアクセスを行うプロパティ
    ''' <para>作成情報：2012/05/30 matsuoka
    ''' </para></remarks>
    Public Property PropEditorId() As String
        Get
            Return ppEditId
        End Get
        Set(ByVal value As String)
            ppEditId = value
        End Set
    End Property
    ''' <summary>
    ''' 編集者グループＣＤのプロパティ
    ''' </summary>
    ''' <remarks>編集者グループＣＤのアクセスを行うプロパティ
    ''' <para>作成情報：2012/05/30 matsuoka
    ''' </para></remarks>
    Public Property PropEditorGroupCD() As String
        Get
            Return ppEditGroupCd
        End Get
        Set(ByVal value As String)
            ppEditGroupCd = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【最終作業プロセス区分】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLastProcessKbn</returns>
    ''' <remarks><para>作成情報：2012/08/07 m.ibuki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLastProcessKbn() As String
        Get
            Return ppLastProcessKbn
        End Get
        Set(ByVal value As String)
            ppLastProcessKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【最終作業プロセス番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLastProcessNmb</returns>
    ''' <remarks><para>作成情報：2012/08/07 m.ibuki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLastProcessNmb() As String
        Get
            Return ppLastProcessNmb
        End Get
        Set(ByVal value As String)
            ppLastProcessNmb = value
        End Set
    End Property

    ''' <summary>
    ''' ユーザパスワードのプロパティ
    ''' </summary>
    ''' <remarks>ユーザパスワードへアクセスを行うプロパティ
    ''' <para>作成情報：2012/08/31 r.hoshino
    ''' </para></remarks>
    Public Property PropUserPass() As String
        Get
            Return ppUserPass
        End Get
        Set(ByVal value As String)
            ppUserPass = value
        End Set
    End Property

    '[add] 2012/09/24 NetUse 仕様変更のため修正START
    ''' <summary>
    ''' プロパティセット【NetUseユーザID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrNetUseUserID</returns>
    ''' <remarks><para>作成情報：2012/09/24 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property NET_USE_USERID() As String
        Get
            Return ppStrNetUseUserID
        End Get
        Set(ByVal value As String)
            ppStrNetUseUserID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【NetUseパスワード】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrNetUsePassword</returns>
    ''' <remarks><para>作成情報：2012/09/24 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property NET_USE_PASSWORD() As String
        Get
            Return ppStrNetUsePassword
        End Get
        Set(ByVal value As String)
            ppStrNetUsePassword = value
        End Set
    End Property
    '[add] 2012/09/24 NetUse 仕様変更のため修正END
End Module
