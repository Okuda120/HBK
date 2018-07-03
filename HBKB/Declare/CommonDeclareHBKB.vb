Public Module CommonDeclareHBKB

    'DNS登録
    Public Const DNS_KBN_FIN As String = "301"          '有
    Public Const DNS_KBN_UNFIN As String = "302"        '無
    Public Const DNS_KBN_QIP As String = "303"          'QIP
    Public Const DNS_KBN_MANUAL As String = "304"       '手動
    Public Const DNS_KBN_AUTO As String = "305"         '自動
    Public Const DNS_KBN_DDNS As String = "306"         'DDNS

    '複数人利用有無
    Public Const SHARE_NASHI As String = "無"            '複数人利用テーブルにデータが1件もない
    Public Const SHARE_ARI As String = "有"              '複数人利用テーブルにデータが1件以上ある

    '期限
    Public Const LIMIT_THISMONTH_ONLY As String = "1"       '今月期限今月未お知らせ分
    Public Const LIMIT_THISMONTH_ALL As String = "2"        '今月期限全部
    Public Const LIMIT_LASTMONTH_ONLY As String = "3"       '前月期限今月未お知らせ分
    Public Const LIMIT_LASTMONTH_ALL As String = "4"        '前月期限全部
    Public Const LIMIT_BEF_LASTMONTH_ONLY As String = "5"   '前々月以前今月未お知らせ分
    Public Const LIMIT_BEF_LASTMONTH_ALL As String = "6"    '前々月以前期限全部
    '期限コンボボックス用配列
    Public strCmbLimit(,) As String = { _
                                       {LIMIT_THISMONTH_ONLY, "今月期限今月未お知らせ分"}, _
                                       {LIMIT_THISMONTH_ALL, "今月期限全部"}, _
                                       {LIMIT_LASTMONTH_ONLY, "前月期限今月未お知らせ分"}, _
                                       {LIMIT_LASTMONTH_ALL, "前月期限全部"}, _
                                       {LIMIT_BEF_LASTMONTH_ONLY, "前々月以前今月未お知らせ分"}, _
                                       {LIMIT_BEF_LASTMONTH_ALL, "前々月以前期限全部"} _
                                      }

    '番号通知
    Public Const NUMINFO_KBN_UNFIN As String = "0"          '未通知
    Public Const NUMINFO_KBN_FIN As String = "1"            '通知済
    Public Const NUMINFO_NM_UNFIN As String = "未通知"
    Public Const NUMINFO_NM_FIN As String = "通知済"

    Public NUMINFO_KBN(,) As String = {{"", ""}, {NUMINFO_KBN_UNFIN, NUMINFO_NM_UNFIN}, {NUMINFO_KBN_FIN, NUMINFO_NM_FIN}}

    'シール送付
    Public Const SEALSEND_KBN_UNFIN As String = "0"          '未送付
    Public Const SEALSEND_KBN_FIN As String = "1"            '送付済
    Public Const SEALSEND_NM_UNFIN As String = "未送付"
    Public Const SEALSEND_NM_FIN As String = "送付済"

    Public SEALSEND_KBN(,) As String = {{"", ""}, {SEALSEND_KBN_UNFIN, SEALSEND_NM_UNFIN}, {SEALSEND_KBN_FIN, SEALSEND_NM_FIN}}

    'ウイルス対策ソフト確認
    Public Const ANTIVIRUSSOFCHECK_KBN_UNFIN As String = "0"          '未対策
    Public Const ANTIVIRUSSOFCHECK_KBN_FIN As String = "1"            '対策済
    Public Const ANTIVIRUSSOFCHECK_NM_UNFIN As String = "未対策"
    Public Const ANTIVIRUSSOFCHECK_NM_FIN As String = "対策済"

    Public ANTIVIRUSSOFCHECK_KBN(,) As String = {{"", ""}, {ANTIVIRUSSOFCHECK_KBN_UNFIN, ANTIVIRUSSOFCHECK_NM_UNFIN}, {ANTIVIRUSSOFCHECK_KBN_FIN, ANTIVIRUSSOFCHECK_NM_FIN}}

    'ラジオボタン
    Public Const RADIO_ZERO As Integer = 0
    Public Const RADIO_ONE As Integer = 1
    Public Const RADIO_TWO As Integer = 2

    '保証書有無
    Public Const RADIO_HOSYO_ARI As String = "有"
    Public Const RADIO_HOSYO_NASI As String = "無"
    Public Const RADIO_HOSYO_HUMEI As String = "不明"

    '導入タイプ
    Public Const RADIO_KEIHI As String = "経費購入"
    Public Const RADIO_LEASE As String = "リース"

    ''' <summary>
    ''' 一括作業区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Const WORKKBN_IKKATSU_SETUP As String = "01"                 '一括セットアップ
    Public Const WORKKBN_IKKATSU_THINPUKA As String = "02"              '一括陳腐化
    Public Const WORKKBN_IKKATSU_HAIKIJYUNBI As String = "03"           '一括廃棄準備
    Public Const WORKKBN_IKKATSU_HAIKI As String = "04"                 '一括廃棄

    'リストボックスの定数
    Public WorkKbn(,) As String = {{WORKKBN_IKKATSU_SETUP, "一括セットアップ"}, {WORKKBN_IKKATSU_THINPUKA, "一括陳腐化"}, {WORKKBN_IKKATSU_HAIKIJYUNBI, "一括廃棄準備"}, {WORKKBN_IKKATSU_HAIKI, "一括廃棄"}}

    'フォーマット格納フォルダ名
    Public Const FORMAT_FOLDER_PATH As String = "Excel"

    '共通検索EXCEL出力フォーマットファイル名
    Public Const FORMAT_COMMON_SYSTEM As String = "共通検索_システム一覧.xlsx"          'システム
    Public Const FORMAT_COMMON_DOC As String = "共通検索_文書一覧.xlsx"                 '文書
    Public Const FORMAT_COMMON_SUPPORT As String = "共通検索_サポセン機器一覧.xlsx"     'サポセン
    Public Const FORMAT_COMMON_BUY As String = "共通検索_部所有機器一覧.xlsx"           '部所有

    'フォーマットファイル名【人事連絡用出力】
    Public Const FORMAT_BUY_JINJIRENRAKU As String = "部所有機器_人事連絡.xlsx"
    'フォーマットファイル名【月次報告用出力】
    Public Const FORMAT_BUY_GETUJIHOUKOKU As String = "部所有機器_月次報告.xlsx"
    'フォーマットファイル名【機器一括検索一覧EXCEL出力_マスター】
    Public Const FORMAT_BUY_KIKIIKKATSUKENSAKU_MASTER As String = "機器一括検索一覧_マスター.xlsx"
    'フォーマットファイル名【機器一括検索一覧EXCEL出力_導入】
    Public Const FORMAT_BUY_KIKIIKKATSUKENSAKU_DOUNYU As String = "機器一括検索一覧_導入一覧.xlsx"
    'フォーマットファイル名【機器一括検索一覧EXCEL出力_履歴】
    Public Const FORMAT_BUY_KIKIIKKATSUKENSAKU_RIREKI As String = "機器一括検索一覧_履歴.xlsx"


    '出力ファイル名【人事連絡用出力】
    Public Const FILENM_BUY_JINJIRENNRAKU As String = "人事連絡"
    '出力ファイル名【月次報告用出力】
    Public Const FILENM_BUY_GETUJIHOUKOKU As String = "月次報告"
    '出力ファイル名【部所有機器Excel出力】
    Public Const FILENM_BUY_BUSYOYUKIKIITIRAN As String = "部所有機器"
    '出力ファイル名【機器一括検索一覧EXCEL出力】
    Public Const FILENM_BUY_KIKIIKKATSUKENSAKU As String = "機器一覧"


    '共通検索画面（HBKB0101）
    Public Const B0101_E001 As String = "検索結果から1行選択してください。"
    Public Const B0101_E002 As String = "ファイルが見つかりません。"
    Public Const B0101_W001 As String = "検索結果の件数が{0}件を超えているため、表示に時間がかかる場合があります。" & vbCrLf & "検索結果の表示を行いますか？"
    Public Const B0101_I001 As String = "該当する結果はありません。"
    Public Const B0101_I002 As String = "出力が完了しました。"

    '一括登録画面（HBKB0201）
    Public Const B0201_E001 As String = "登録するファイルを選択してください。"
    Public Const B0201_E002 As String = "取込ファイルがExcelファイルではありません。"
    Public Const B0201_E003 As String = "取込ファイルパスのファイルがありません。"
    Public Const B0201_I001 As String = "登録が完了しました。"
    Public ReadOnly COLUMNNAME_SYS() As String = New String() {"取込番号", "分類1", "分類2", "名称", "ステータス", "CIオーナーCD", "説明", "フリーテキスト1", "フリーテキスト2", _
                                        "フリーテキスト3", "フリーテキスト4", "フリーテキスト5", "フリーフラグ1", "フリーフラグ2", "フリーフラグ3", "フリーフラグ4", _
                                        "フリーフラグ5", "情報共有先", "ノウハウURL", "ノウハウURL説明", "サーバー管理番号", "サーバー管理番号説明", "関係者区分", "関係者ID"}

    '一括登録（システム）（HBKB0202）
    Public Const B0202_E001 As String = "取込ファイルの値に誤りがあります。エラーの内容は以下のファイルを確認してください。" & vbCrLf & "{0}"
    Public Const B0202_E002 As String = "{0}：「{1}」を入力してください。"
    Public Const B0202_E003 As String = "{0}：「{1}」の桁数が不正です。"
    Public Const B0202_E004 As String = "{0}：「{1}」が正しくありません。"
    Public Const B0202_E005 As String = "{0}：「{1}」がファイル内で重複しています。"
    Public Const B0202_E006 As String = "{0}：「{1}」はマスターに存在しません。"
    Public Const B0202_E007 As String = "{0}：「{1}」は既に登録済みです。"
    Public Const B0202_E008 As String = "フォーマットの項目数が異なっています。"
    Public Const B0202_E009 As String = "CI番号を新規に採番できませんでした。"

    '一括登録（文書）（HBKB0203）
    Public Const B0203_E001 As String = "取込ファイルの値に誤りがあります。エラーの内容は以下のファイルを確認してください。" & vbCrLf & "{0}"
    Public Const B0203_E002 As String = "{0}：「{1}」を入力してください。"
    Public Const B0203_E003 As String = "{0}：「{1}」の桁数が不正です。"
    Public Const B0203_E004 As String = "{0}：「{1}」が正しくありません。"
    Public Const B0203_E005 As String = "{0}：「{1}」がファイル内で重複しています。"
    Public Const B0203_E006 As String = "{0}：「{1}」はマスターに存在しません。"
    Public Const B0203_E007 As String = "{0}：「{1}」は既に登録済みです。"
    Public Const B0203_E008 As String = "{0}：「{1}」のファイルが存在しません。"
    Public Const B0203_E009 As String = "フォーマットの項目数が異なっています。"
    Public Const B0203_E010 As String = "CI番号を新規に採番できませんでした。"
    Public ReadOnly COLUMNNAME_DOC() As String = New String() {"取込番号", "番号（手動）", "分類１", "分類２", "名称", "ステータス", "CIオーナーCD", "説明", "フリーテキスト1", "フリーテキスト2", _
                                    "フリーテキスト3", "フリーテキスト4", "フリーテキスト5", "フリーフラグ1", "フリーフラグ2", "フリーフラグ3", "フリーフラグ4", _
                                    "フリーフラグ5", "版（手動）", "作成者ID", "作成者名", "作成年月日", "最終更新者ID", "最終更新者", "最終更新日時", "取込ファイルパス", "文書責任者ID", "文書責任者名", "文書配付先", "文書提供者", "文書廃棄年月日", "文書廃棄理由"}

    '一括登録（部所有機器）（HBKB0204）
    Public Const B0204_E001 As String = "取込ファイルの値に誤りがあります。エラーの内容は以下のファイルを確認してください。" & vbCrLf & "{0}"
    Public Const B0204_E002 As String = "{0}：「{1}」を入力してください。"
    Public Const B0204_E003 As String = "{0}：「{1}」の桁数が不正です。"
    Public Const B0204_E004 As String = "{0}：「{1}」が正しくありません。"
    Public Const B0204_E005 As String = "{0}：「{1}」がファイル内で重複しています。"
    Public Const B0204_E006 As String = "{0}：「{1}」はマスターに存在しません"
    Public Const B0204_E007 As String = "{0}：「{1}」は既に登録済みです。"
    Public Const B0204_E008 As String = "フォーマットの項目数が異なっています。"
    Public Const B0204_E009 As String = "CI番号を新規に採番できませんでした。"
    Public ReadOnly COLUMNNAME_BUSYO() As String = New String() {"取込番号", "番号", "分類1", "分類2", "名称", "ステータス", "CIオーナーCD", "説明", "フリーテキスト1", "フリーテキスト2", _
                                            "フリーテキスト3", "フリーテキスト4", "フリーテキスト5", "フリーフラグ1", "フリーフラグ2", "フリーフラグ3", "フリーフラグ4", _
                                            "フリーフラグ5", "型番", "エイリアス", "製造番号", "MACアドレス1", "MACアドレス2", "zoo参加有無", "OS", "ウイルス対策ソフト", "DNS登録", "NIC1", _
                                            "NIC2", "接続日", "有効日", "停止日", "最終お知らせ日", "接続理由", "更新日", "通知日", "番号通知", "シール送付", "ウイルス対策ソフト確認", "ウイルス対策ソフトサーバー確認日", _
                                            "部所有機器備考", "管理局", "管理部署", "IP割当種類", "固定IP", "ユーザーID", "ユーザー氏名", "ユーザー所属会社", "ユーザー所属局", "ユーザー所属部署", _
                                            "ユーザー電話番号", "ユーザーメールアドレス", "ユーザー連絡先", "ユーザー番組/部屋", "設置局", "設置部署", "設置番組/部屋", "設置建物", "設置フロア"}

    '変更理由登録画面（HBKB0301）
    Public Const B0301_E001 As String = "「理由」又は、「原因リンク」を入力してください。"

    'システム登録画面（HBKB0401）
    Public Const B0401_NAME_DEFAULT As String = "ひびき：システム登録"
    Public Const B0401_NAME_RIREKI As String = "ひびき：システム履歴"
    Public Const B0401_E001 As String = "{0}のデータが取得できませんでした。"
    Public Const B0401_E002 As String = "分類1を入力してください。"
    Public Const B0401_E003 As String = "分類2を入力してください。"
    Public Const B0401_E004 As String = "名称を入力してください。"
    Public Const B0401_E005 As String = "ステータスを選択してください。"
    Public Const B0401_E006 As String = "ノウハウURLのURLと説明を入力してください。"
    Public Const B0401_E007 As String = "ノウハウURLのURLが重複しています。"
    Public Const B0401_E008 As String = "サーバー管理情報のサーバー管理番号と説明を入力してください。"
    Public Const B0401_E009 As String = "サーバー管理情報のサーバー管理番号が重複しています。"
    Public Const B0401_E010 As String = "CIオーナー名をサブ検索画面より選択してください。"
    Public Const B0401_E011 As String = "CI番号を新規に採番できませんでした。"
    Public Const B0401_E012 As String = "新規に履歴番号を採番できませんでした。"
    Public Const B0401_E013 As String = "既に「分類1」「分類2」「名称」のシステムは登録されています。"
    Public Const B0401_I001 As String = "登録が完了しました。"
    Public Const B0401_I002 As String = "ロールバックが完了しました。"

    '文書登録画面（HBKB0501）
    Public Const B0501_NAME_DEFAULT As String = "ひびき：文書登録"
    Public Const B0501_NAME_RIREKI As String = "ひびき：文書履歴"
    Public Const B0501_E001 As String = "{0}のデータが取得できませんでした。"
    Public Const B0501_E002 As String = "分類1を入力してください。"
    Public Const B0501_E003 As String = "分類2を入力してください。"
    Public Const B0501_E004 As String = "名称を入力してください。"
    Public Const B0501_E005 As String = "ステータスを選択してください。"
    Public Const B0501_E006 As String = "文書格納パスに入力されたファイルが存在しません。"
    Public Const B0501_E007 As String = "既に「分類1」「分類2」「名称」のシステムは登録されています。"
    Public Const B0501_E008 As String = "CIオーナー名をサブ検索画面より選択してください。"
    Public Const B0501_E009 As String = "CI番号を新規に採番できませんでした。"
    Public Const B0501_E010 As String = "新規に履歴番号を採番できませんでした。"
    Public Const B0501_E011 As String = "最終更新日時の日付を入力してください。"
    Public Const B0501_E012 As String = "最終更新日時の時刻を入力してください。"
    Public Const B0501_E013 As String = "ファイルが見つかりません。"
    Public Const B0501_E014 As String = "175文字以上のファイル名は登録できません。"
    Public Const B0501_I001 As String = "登録が完了しました。"
    Public Const B0501_I002 As String = "ロールバックが完了しました。"

    'サポセン機器登録画面（HBKB0601）
    Public Const B0601_NAME_DEFAULT As String = "ひびき：サポセン機器登録"
    Public Const B0601_NAME_RIREKI As String = "ひびき：サポセン機器履歴"
    Public Const B0601_E001 As String = "{0}のデータが取得できませんでした。"
    Public Const B0601_E002 As String = "タイプを選択してください。"
    Public Const B0601_E003 As String = "ステータスを選択してください。"
    Public Const B0601_E004 As String = "MACアドレス1が正しくありません。"
    Public Const B0601_E005 As String = "MACアドレス2が正しくありません。"
    Public Const B0601_E006 As String = "利用者情報のメールアドレスが正しくありません。"
    Public Const B0601_E007 As String = "レンタル期間の範囲が正しくありません。"
    Public Const B0601_E008 As String = "機器利用情報のオプションソフトが重複しています。"
    Public Const B0601_E009 As String = "機器利用情報のセット機器が重複しています。"
    Public Const B0601_E010 As String = "イメージ番号がマスターに存在しません。"
    Public Const B0601_E011 As String = "セット機器に存在しない機器が入力されています。"
    Public Const B0601_E012 As String = "CIオーナー名をサブ検索画面より選択してください。"
    Public Const B0601_E013 As String = "セット機器に入力した機器が、それぞれ別の機器とセットになっています。" & vbCrLf & "同じセットの機器のみ入力してください。"
    Public Const B0601_I001 As String = "登録が完了しました。"

    '機器一括検索画面（HBKB0701）
    Public Const B0701_E001 As String = "検索結果から1行選択してください。"
    Public Const B0701_W001 As String = "検索結果の件数が{0}件を超えているため、表示に時間がかかる場合があります。" & vbCrLf & "検索結果の表示を行いますか？"
    Public Const B0701_I001 As String = "該当する結果はありません。"
    Public Const B0701_I002 As String = "出力が完了しました。"

    '機器一括検索EXCEL出力（HBKB0702）
    Public Const B0702_FILE_KIND As String = "Excel Files (*.xlsx)|*.xlsx"

    'レンタル及び部所有機器の期限切れ検索一覧画面（HBKB0801）
    Public Const B0801_E001 As String = "登録する検索結果を選択してください。"
    Public Const B0801_W001 As String = "検索結果の件数が{0}件を超えているため、表示に時間がかかる場合があります。" & vbCrLf & "検索結果の表示を行いますか？"
    Public Const B0801_W002 As String = "インシデントを登録します。よろしいですか？"
    Public Const B0801_I001 As String = "該当する結果はありません。"
    Public Const B0801_I002 As String = "登録が完了しました。"

    '導入画面（HBKB0901）
    Public Const B0901_E001 As String = "{0}のデータが取得できませんでした。"
    Public Const B0901_E002 As String = "種別を選択してください。"
    Public Const B0901_E003 As String = "台数を入力してください。"
    Public Const B0901_E004 As String = "分類1を入力してください。"
    Public Const B0901_E005 As String = "分類2を入力してください。"
    Public Const B0901_E006 As String = "名称を入力してください。"
    Public Const B0901_E007 As String = "型番を入力してください。"
    Public Const B0901_E008 As String = "導入開始日を入力してください。"
    Public Const B0901_E009 As String = "タイプを選択してください。"
    Public Const B0901_E010 As String = "台数は半角数値で入力してください。"
    Public Const B0901_E011 As String = "台数は1以上を入力してください。"
    Public Const B0901_E012 As String = "導入番号を新規に採番できませんでした。"
    Public Const B0901_E013 As String = "CI番号を新規に採番できませんでした。"
    Public Const B0901_E014 As String = "新規に履歴番号を採番できませんでした。"
    Public Const B0901_E015 As String = "種別採番データが取得できませんでした。"
    Public Const B0901_E016 As String = "新規に種別「{0}」採番番号を採番できませんでした。"
    Public Const B0901_E017 As String = "新規にログNoを採番できませんでした。"
    Public Const B0901_I001 As String = "登録が完了しました。"

    '一括更新（HBKB1101）
    Public Const B1101_E001 As String = "更新する内容を入力してください。"
    Public Const B1101_E002 As String = "{0}：種別を入力してください。"
    Public Const B1101_E003 As String = "{0}：番号を入力してください。"
    Public Const B1101_E004 As String = "{0}：{1}が重複している行があります。"
    Public Const B1101_E005 As String = "{0}：{1}の機器は存在しません。"
    Public Const B1101_E006 As String = "{0}：{1}のステータスが不正です。"
    Public Const B1101_E007 As String = "{0}：{1}のMACアドレス1が正しくありません。"
    Public Const B1101_E008 As String = "{0}：{1}のMACアドレス2が正しくありません。"
    Public Const B1101_E009 As String = "{0}：{1}はロック状態の為、更新することができません。"
    Public Const B1101_E010 As String = "{0}：{1}のリース期限日が正しくありません。"
    Public Const B1101_E011 As String = "{0}：{1}はセットアップ不要です。"
    Public Const B1101_E012 As String = "{0}：イメージ番号がマスターに存在しません。"
    Public Const B1101_W001 As String = "更新対象の列を全て非表示にすることはできません。"
    Public Const B1101_I001 As String = "{0}件 登録が完了しました。"
    Public ReadOnly CHECK_STATUS_KIKI() As String = New String() {"301", "302", "304", "307", "310", "313", "315", "317"}

    '一括更新_一括セットアップ（HBKB1102）
    Public Const B1102_E001 As String = "更新する内容を入力してください。"
    Public Const B1102_E002 As String = "{0}：種別を入力してください。"
    Public Const B1102_E003 As String = "{0}：番号を入力してください。"
    Public Const B1102_E004 As String = "{0}：{1}が重複している行があります。"
    Public Const B1102_E005 As String = "{0}：{1}の機器は存在しません。"
    Public Const B1102_E006 As String = "{0}：{1}のステータスが不正です。" & vbCrLf & "（現在：{2}  設定可能：初期、未設定）"
    Public Const B1102_E007 As String = "{0}：{1}はセットアップ不要です。"
    Public Const B1102_E008 As String = "{0}：{1}のイメージ番号を入力してください。"
    Public Const B1102_E009 As String = "{0}：イメージ番号がマスターに存在しません。"
    Public Const B1102_E010 As String = "{0}：{1}はロック状態の為、更新することができません。"
    Public Const B1102_I001 As String = "{0}件 登録が完了しました。"
    Public ReadOnly CHECK_STATUS_SETUP() As String = New String() {"301", "302"}

    '一括陳腐化（HBKB1103）
    Public Const B1103_E001 As String = "更新する内容を入力してください。"
    Public Const B1103_E002 As String = "{0}：種別を選択してください。"
    Public Const B1103_E003 As String = "{0}：番号を入力してください。"
    Public Const B1103_E004 As String = "{0}：{1}が重複している行があります。"
    Public Const B1103_E005 As String = "{0}：{1}の機器は存在しません。"
    Public Const B1103_E006 As String = "{0}：{1}のステータスが不正です。" & vbCrLf & "（現在：{2}  設定可能：出庫可）"
    Public Const B1103_E007 As String = "{0}：{1}はロック状態の為、更新することができません。"
    Public Const B1103_E008 As String = "{0}：{1}はセットアップ不要の為、陳腐化できません。"
    Public Const B1103_I001 As String = "{0}件 登録が完了しました。"
    Public ReadOnly CHECK_STATUS_CHINPUKA() As String = New String() {"304"}

    '一括廃棄準備（HBKB1104）
    Public Const B1104_E001 As String = "更新する内容を入力してください。"
    Public Const B1104_E002 As String = "{0}：種別を入力してください。"
    Public Const B1104_E003 As String = "{0}：番号を入力してください。"
    Public Const B1104_E004 As String = "{0}：{1}が重複している行があります。"
    Public Const B1104_E005 As String = "{0}：{1}の機器は存在しません。"
    Public Const B1104_E006 As String = "{0}：{1}のステータスが不正です。" & vbCrLf & "（現在：{2}  設定可能：死在庫）"
    Public Const B1104_E007 As String = "{0}：{1}はロック状態の為、更新することができません。"
    Public Const B1104_I001 As String = "{0}件 登録が完了しました。"
    Public ReadOnly CHECK_STATUS_HAIKIJUNBI() As String = New String() {"313"}

    '一括廃棄（HBKB1105）
    Public Const B1105_E001 As String = "更新する内容を入力してください。"
    Public Const B1105_E002 As String = "{0}：種別を入力してください。"
    Public Const B1105_E003 As String = "{0}：番号を入力してください。"
    Public Const B1105_E004 As String = "{0}：{1}のステータスを選択してください。"
    Public Const B1105_E005 As String = "{0}：{1}の機器状態を入力してください。"
    Public Const B1105_E006 As String = "{0}：{1}が重複している行があります。"
    Public Const B1105_E007 As String = "{0}：{1}の機器は存在しません。"
    Public Const B1105_E008 As String = "{0}：{1}のステータスが不正です。" & vbCrLf & "（現在：{2}  設定可能：廃棄予定）"
    Public Const B1105_E009 As String = "{0}：{1}はロック状態の為、更新することができません。"
    Public Const B1105_I001 As String = "{0}件 登録が完了しました。"
    Public ReadOnly CHECK_STATUS_HAIKI() As String = New String() {"317"}

    '部所有機器検索一覧画面（HBKB1201）
    Public Const B1201_E001 As String = "検索結果から1行選択してください。"
    Public Const B1201_W001 As String = "検索結果の件数が{0}件を超えているため、表示に時間がかかる場合があります。" & vbCrLf & "検索結果の表示を行いますか？"
    Public Const B1201_I001 As String = "該当する結果はありません。"
    Public Const B1201_I002 As String = "出力が完了しました。"

    '部所有機器登録画面（HBKB1301）
    Public Const B1301_NAME_DEFAULT As String = "ひびき：部所有機器登録"
    Public Const B1301_NAME_RIREKI As String = "ひびき：部所有機器履歴"
    Public Const B1301_E001 As String = "{0}のデータが取得できませんでした。"
    Public Const B1301_E002 As String = "分類1を入力してください。"
    Public Const B1301_E003 As String = "分類2を入力してください。"
    Public Const B1301_E004 As String = "名称を入力してください。"
    Public Const B1301_E005 As String = "ステータスを選択してください。"
    Public Const B1301_E006 As String = "CIオーナー名をサブ検索画面より選択してください。"
    Public Const B1301_E007 As String = "CI番号を新規に採番できませんでした。"
    Public Const B1301_E008 As String = "新規に履歴番号を採番できませんでした。"
    Public Const B1301_E009 As String = "型番を入力してください。"
    Public Const B1301_E010 As String = "MACアドレス1が正しくありません。"
    Public Const B1301_E011 As String = "MACアドレス2が正しくありません。"
    Public Const B1301_E012 As String = "メールアドレスが正しくありません。"
    Public Const B1301_E013 As String = "番号を入力してください。"
    Public Const B1301_E014 As String = "番号は半角数値で入力してください。"
    Public Const B1301_E015 As String = "番号は既に登録済みです。"
    Public Const B1301_I001 As String = "登録が完了しました。"
    Public Const B1301_I002 As String = "ロールバックが完了しました。"

    ''' <summary>
    ''' Excelフォルダパス(相対パス)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const EXCEL_FOLDER_PATH As String = "\Excel"

    ''' <summary>
    ''' Excelファイルパス(相対パス)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const EXCEL_FILE_PATH As String = "\form.xlsx"


    Public ReadOnly SPREAD_COLUMN_BUNSYO() As String = New String() {" ", "種別", "番号", "分類1", "分類2", "名称", "ステータス", "説明", "最終更新日時", "最終更新者", "CIオーナー", "文書配付先", "CI番号"}

    Public ReadOnly SPREAD_COLUMN_SYSTEM() As String = New String() {"種別", "番号", "分類1", "分類2", "名称", "ステータス", "説明", "最終更新日時", "最終更新者", "CIオーナー", "CI番号"}



End Module

