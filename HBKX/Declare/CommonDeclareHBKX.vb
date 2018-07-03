Public Module CommonDeclareHBKX

    'ユーザー区分
    Public Const USR_KBN_SYANAI As String = "0"         '社内
    Public Const USR_KBN_GROUP As String = "1"          'グループ会社・系列局
    Public Const USR_KBN_KEIRETU As String = "2"        '系列局番販
    Public Const USR_KBN_SYANAI_NM As String = "社内"
    Public Const USR_KBN_GROUP_NM As String = "グループ会社・系列局"
    Public Const USR_KBN_KEIRETU_NM As String = "系列局番販"
    'ユーザー区分コンボボックスの定数
    Public UsrKbn(,) As String = {{"", ""}, {USR_KBN_SYANAI, USR_KBN_SYANAI_NM}, {USR_KBN_GROUP, USR_KBN_GROUP_NM}, {USR_KBN_KEIRETU, USR_KBN_KEIRETU_NM}}


    '登録方法
    Public Const REG_TORIKOMI As String = "0"         '取込
    Public Const REG_GAMEN As String = "1"            '画面入力   
    Public Const REG_TORIKOMI_NM As String = "取込"
    Public Const REG_GAMEN_NM As String = "画面入力"
    '登録方法コンボボックスの定数
    Public Regtype(,) As String = {{"", ""}, {REG_TORIKOMI, REG_TORIKOMI_NM}, {REG_GAMEN, REG_GAMEN_NM}}

    '有効/無効
    Public Const DATA_YUKO As String = "0"          '有効
    Public Const DATA_MUKO As String = "1"          '無効   
    Public Const DATA_YUKO_NM As String = "有効"
    Public Const DATA_MUKO_NM As String = "無効"

    '有効/無効コンボボックスの定数
    Public DataYukoMuko(,) As String = {{"", ""}, {DATA_YUKO, DATA_YUKO_NM}, {DATA_MUKO, DATA_MUKO_NM}}

    '削除有無判断
    Public Const STATE_NAIYO_DELETE As String = "削除"

    '並び順登録の種類
    Public Const SORT_GROUP_MTB As String = "grp_mtb"         'グループマスター
    Public Const SORT_CI_INFO_TB As String = "ci_info_tb"     'CI共通情報
    Public Const SORT_MAILTEMP_MTB As String = "mail_template_mtb"    'メールテンプレートマスター  2015/08/18 ADD

    'ユーザー区分
    Public Const USR_GROUP_ADMIN As String = "0"              'グループ管理者
    Public Const USR_SUPER_USER As String = "1"               'スーパーユーザー

    'ユーザーグループ権限
    Public Const USR_GROUP_ADMIN_NORMAL As String = "0"       '一般
    Public Const USR_GROUP_ADMIN_ADMIN As String = "1"        '管理者

    'デフォルト
    Public Const DEFAULT_OFF As String = "0"                  'デフォルト以外
    Public Const DEFAULT_ON As String = "1"                   'デフォルト

    'ソフト区分名
    Public Const SOFTKBN_OS_NM As String = "OS"                            'OS
    Public Const SOFTKBN_OPTIONSOFT_NM As String = "オプションソフト"      'オプションソフト
    Public Const SOFTKBN_UNTIVIRUSSOFT_NM As String = "ウイルス対策ソフト" 'ウイルス対策ソフト

    'ログインモード(エンドユーザーマスター検索一覧)
    Public Const LOGIN_MODE_END_USR_ETURAN As String = "0"                '閲覧
    Public Const LOGIN_MODE_END_USR_REG As String = "1"                   'エンドユーザーマスター編集ユーザー

    '特権ユーザーログイン画面（ひびきユーザー登録）(HBKX0101)
    Public Const X0101_E001 As String = "IDを入力してください。"
    Public Const X0101_E002 As String = "パスワードを入力してください。"
    Public Const X0101_E003 As String = "入力したユーザーIDもしくはパスワードが正しくありません。"
    Public Const X0101_E004 As String = "グループ管理者の権限がないためログインできません。"

    '特権ユーザーログイン画面（エンドユーザー検索）(HBKX0102)
    Public Const X0102_E001 As String = "IDを入力してください。"
    Public Const X0102_E002 As String = "パスワードを入力してください。"
    Public Const X0102_E003 As String = "入力したユーザーIDもしくはパスワードが正しくありません。"

    '特権ユーザーログイン画面（エンドユーザー取込）(HBKX0103)
    Public Const X0103_E001 As String = "IDを入力してください。"
    Public Const X0103_E002 As String = "パスワードを入力してください。"
    Public Const X0103_E003 As String = "入力したユーザーIDもしくはパスワードが正しくありません。"

    '特権ログインアウト区分
    Public Const SUPER_LOGINOK As String = "1"
    Public Const SUPER_LOGOUT As String = "2"
    Public Const SUPER_LOGINNG As String = "3"

    '暗号化／復号化パスワード
    Public Const ENCRYPT_PASSWORD As String = "pass"

    '特権ユーザパスワード変更画面(HBKX0110)
    Public Const X0110_E001 As String = "IDを入力してください。"
    Public Const X0110_E002 As String = "現在のパスワードを入力してください。"
    Public Const X0110_E003 As String = "新しいパスワードを入力してください。"
    Public Const X0110_E004 As String = "新しいパスワードが正しくありません。"
    Public Const X0110_E005 As String = "新しいパスワード（再入力）を入力してください。"
    Public Const X0110_E006 As String = "新しいパスワード（再入力）が正しくありません。"
    Public Const X0110_E007 As String = "入力したユーザIDもしくはパスワードが正しくありません。"
    Public Const X0110_I001 As String = "パスワード変更が完了しました。"

    'ひびきユーザーマスター登録画面(HBKX0201)
    Public Const X0201_E001 As String = "氏名を入力してください。"
    Public Const X0201_E002 As String = "氏名（カナ）を入力してください。"
    Public Const X0201_E003 As String = "メールアドレスが正しくありません。"
    Public Const X0201_E004 As String = "該当ユーザーIDに対するデフォルトグループを未選択にすることはできません。"
    Public Const X0201_E005 As String = "該当ユーザーは登録済みのため、行削除することはできません。"
    Public Const X0201_E006 As String = "検索結果から１行選択してください。"
    Public Const X0201_E007 As String = "管理者を設定してください。"
    Public Const X0201_W001 As String = "ひびきユーザーを登録します。よろしいですか？"
    Public Const X0201_I001 As String = "登録が完了しました。"

    'エンドユーザーマスター検索一覧画面(HBKX0301)\
    Public Const X0301_E001 As String = "検索結果から1行選択してください。"
    Public Const X0301_W001 As String = "検索結果の件数が{0}件を超えているため、表示に時間がかかる場合があります。" & vbCrLf & "検索結果の表示を行いますか？"
    Public Const X0301_I001 As String = "該当する結果はありません。"

    'エンドユーザーマスター登録画面(HBKX0401)
    Public Const X0401_E001 As String = "ユーザーIDを入力してください。"
    Public Const X0401_E002 As String = "入力したユーザーIDは、既にマスターに存在しています。"
    Public Const X0401_E003 As String = "ユーザー区分を入力してください。"
    Public Const X0401_E004 As String = "姓を入力してください。"
    Public Const X0401_E005 As String = "名を入力してください。"
    Public Const X0401_E006 As String = "姓(カナ)を入力してください。"
    Public Const X0401_E007 As String = "名(カナ)を入力してください。"
    Public Const X0401_E008 As String = "メールアドレスが正しくありません。"
    Public Const X0401_W001 As String = "エンドユーザーを登録します。よろしいですか？"
    Public Const X0401_I001 As String = "登録が完了しました。"

    'エンドユーザーマスター取込画面(HBKX0501)
    Public Const X0501_E001 As String = "登録するファイルを選択してください。"
    Public Const X0501_E002 As String = "取込ファイルがcsvファイルではありません。"
    Public Const X0501_E003 As String = "取込ファイルパスのファイルがありません。"
    Public Const X0501_E004 As String = "取込ファイルの値に誤りがあります。エラーの内容は以下のファイルを確認してください。" & vbCrLf & "{0}"
    Public Const X0501_E005 As String = "{0}：「{1}」を入力してください。"
    Public Const X0501_E006 As String = "{0}：「{1}」の桁数が不正です。"
    Public Const X0501_E007 As String = "{0}：「{1}」が正しくありません。"
    Public Const X0501_E008 As String = "{0}：「{1}」がファイル内で重複しています。"
    Public Const X0501_E009 As String = "{0}：「{1}」が既にマスターデータに存在しています。"
    '[add] 2015/08/21 y.naganuma 入力チェック追加対応 START
    Public Const X0501_E010 As String = "{0}：「{1}」(「{2}」＋「スペース」＋「{3}」)の文字数が50文字を超えています。"
    '[add] 2015/08/21 y.naganuma 入力チェック追加対応 END
    Public Const X0501_W001 As String = "エンドユーザーを登録します。よろしいですか？"
    Public Const X0501_I001 As String = "登録が完了しました。"
    Public ReadOnly COLUMNNAME_ENDUSR() As String = New String() {"エンドユーザーID", "姓", "名", "姓カナ", "名カナ", "所属会社", "部署名", "電話番号", "メールアドレス", "ユーザー区分", "状態説明"}
    '登録方法
    Public Const DATA_REG_UPLOAD As String = "0"        '取込
    Public Const DATA_REG_FROMENTRY As String = "1"     '画面入力

    'メールテンプレートマスター一覧画面(HBKX0601)
    Public Const X0601_E001 As String = "検索結果から1行選択してください。"

    'メールテンプレートマスター登録画面(HBKX0701)
    Public Const X0701_E001 As String = "{0}のデータが取得できませんでした。"
    Public Const X0701_E002 As String = "テンプレート名を入力してください。"
    Public Const X0701_E003 As String = "プロセス区分を選択してください。"
    Public Const X0701_E004 As String = "期限を選択してください。"
    Public Const X0701_E005 As String = "差出人のメールアドレスが正しくありません。"
    Public Const X0701_E006 As String = "TOの{0}番目のメールアドレスが正しくありません。"
    Public Const X0701_E007 As String = "CCの{0}番目のメールアドレスが正しくありません。"
    Public Const X0701_E008 As String = "BCCの{0}番目メールアドレスが正しくありません。"
    Public Const X0701_E009 As String = "本文を入力してください。"
    Public Const X0701_E010 As String = "テンプレート番号を新規に採番できませんでした。"
    Public Const X0701_W001 As String = "メールテンプレートを登録します。よろしいですか？"
    Public Const X0701_W002 As String = "メールテンプレートを削除します。よろしいですか？"
    Public Const X0701_W003 As String = "メールテンプレートの削除を解除します。よろしいですか？"
    Public Const X0701_I001 As String = "登録が完了しました。"
    Public Const X0701_I002 As String = "削除が完了しました。"
    Public Const X0701_I003 As String = "削除解除が完了しました。"
    '重要度コード
    Public Const PRIORITY_TYPE_LOW As String = "0"              '低
    Public Const PRIORITY_TYPE_NORMAL As String = "1"           '通常
    Public Const PRIORITY_TYPE_HIGH As String = "2"             '高
    '重要度名
    Public Const PRIORITY_TYPE_LOW_NAME As String = "低"
    Public Const PRIORITY_TYPE_NORMAL_NAME As String = "通常"
    Public Const PRIORITY_TYPE_HIGH_NAME As String = "高"
    '重要度コンボボックス用配列
    Public PriorityType(,) As String = _
        { _
         {PRIORITY_TYPE_LOW, PRIORITY_TYPE_LOW_NAME}, _
         {PRIORITY_TYPE_NORMAL, PRIORITY_TYPE_NORMAL_NAME}, _
         {PRIORITY_TYPE_HIGH, PRIORITY_TYPE_HIGH_NAME} _
        }
    '削除フラグ
    Public Const DELETE_MODE_YUKO As String = "0"               '有効
    Public Const DELETE_MODE_MUKO As String = "1"               '無効
    '期限切れ条件区分
    Public Const KIGEN_KBN_ON As String = "1"                   '選択
    Public Const KIGEN_KBN_OFF As String = "0"                  '未選択
    '期限
    Public Const LIMIT_THISMONTH_ONLY As String = "1"           '今月期限末お知らせ分
    Public Const LIMIT_THISMONTH_ALL As String = "2"            '今月期限全部
    Public Const LIMIT_LASTMONTH_ONLY As String = "3"           '前月期限今月末お知らせ分
    Public Const LIMIT_LASTMONTH_ALL As String = "4"            '前月期限全部
    Public Const LIMIT_BEF_LASTMONTH_ONLY As String = "5"       '前々月期限今月末お知らせ分
    Public Const LIMIT_BEF_LASTMONTH_ALL As String = "6"        '前々月までで期限切れ全部
    ''期限コンボボックス用配列
    'Public strCmbLimit(,) As String = { _
    '                                   {LIMIT_THISMONTH_ONLY, "今月期限末お知らせ分"}, _
    '                                   {LIMIT_THISMONTH_ALL, "今月期限全部"}, _
    '                                   {LIMIT_LASTMONTH_ONLY, "前月期限今月末お知らせ分"}, _
    '                                   {LIMIT_LASTMONTH_ALL, "前月期限全部"}, _
    '                                   {LIMIT_BEF_LASTMONTH_ONLY, "前々月期限今月末お知らせ分"}, _
    '                                   {LIMIT_BEF_LASTMONTH_ONLY, "前々月までで期限切れ全部"} _
    '                                  }
    '期限コンボボックス用配列
    Public strCmbLimit(,) As String = { _
                                       {LIMIT_THISMONTH_ONLY, "今月期限今月未お知らせ分"}, _
                                       {LIMIT_THISMONTH_ALL, "今月期限全部"}, _
                                       {LIMIT_LASTMONTH_ONLY, "前月期限今月未お知らせ分"}, _
                                       {LIMIT_LASTMONTH_ALL, "前月期限全部"}, _
                                       {LIMIT_BEF_LASTMONTH_ONLY, "前々月以前今月未お知らせ分"}, _
                                       {LIMIT_BEF_LASTMONTH_ALL, "前々月以前期限全部"} _
                                      }


    '並び順登録画面(HBKX0801)
    Public Const X0801_E001 As String = "表示順(0.01以上の数字)を入力してください。"
    Public Const X0801_W001 As String = "表示順を変更します。よろしいですか？"
    Public Const X0801_I001 As String = "登録が完了しました。"

    'ソフトマスター一覧画面(HBKX0901)
    Public Const X0901_E001 As String = "検索結果から1行選択してください。"

    'ソフトマスター登録画面(HBXK1001)
    Public Const X1001_E001 As String = "ソフトコードを入力してください。"
    Public Const X1001_E002 As String = "ソフトコードは半角数値で入力してください。"
    Public Const X1001_E003 As String = "入力したソフトコードは、既にマスターに存在しています。"
    Public Const X1001_E004 As String = "ソフト名称を入力してください。"
    Public Const X1001_W001 As String = "ソフトマスターを登録します。よろしいですか？"
    Public Const X1001_W002 As String = "ソフトマスターを削除します。よろしいですか？"
    Public Const X1001_W003 As String = "ソフトマスターの削除を解除します。よろしいですか？"
    Public Const X1001_I001 As String = "登録が完了しました。"
    Public Const X1001_I002 As String = "削除が完了しました。"
    Public Const X1001_I003 As String = "削除解除が完了しました。"

    'イメージマスター一覧画面(HBKX1101)
    Public Const X1101_E001 As String = "検索結果から1行選択してください。"

    'イメージマスター登録画面(HBKX1201)
    Public Const X1201_E001 As String = "イメージ名称を入力してください。"
    Public Const X1201_E002 As String = "イメージ番号を新規に採番できませんでした。"
    Public Const X1201_W001 As String = "イメージマスターを登録します。よろしいですか？"
    Public Const X1201_W002 As String = "イメージマスターを削除します。よろしいですか？"
    Public Const X1201_W003 As String = "イメージマスターの削除を解除します。よろしいですか？"
    Public Const X1201_I001 As String = "登録が完了しました。"
    Public Const X1201_I002 As String = "削除が完了しました。"
    Public Const X1201_I003 As String = "削除解除が完了しました。"

    '設置情報マスター一覧画面(HBKX1301)
    Public Const X1301_E001 As String = "検索結果から1行選択してください。"

    '設置情報マスター登録画面(HBXK1401)
    Public Const X1401_E001 As String = "建物を入力してください。"
    Public Const X1401_E002 As String = "フロアを入力してください。"
    Public Const X1401_E003 As String = "設置所属コードを新規に採番できませんでした。"
    Public Const X1401_W001 As String = "設置情報マスターを登録します。よろしいですか？"
    Public Const X1401_W002 As String = "設置情報マスターを削除します。よろしいですか？"
    Public Const X1401_W003 As String = "設置情報マスターの削除を解除します。よろしいですか？"
    Public Const X1401_I001 As String = "登録が完了しました。"
    Public Const X1401_I002 As String = "削除が完了しました。"
    Public Const X1401_I003 As String = "削除解除が完了しました。"
End Module
