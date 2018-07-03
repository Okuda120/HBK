Public Module CommonDeclareHBKF

    'フォーマットファイル名【リリース検索一覧Excel出力】
    Public Const FORMAT_RELEASE_SEARCH As String = "リリース検索一覧.xlsx"
    '出力ファイル名【リリース検索一覧Excel出力】
    Public Const FILENM_RELEASE_SEARCH As String = "リリース検索一覧"

    'ユーザー周知必要有無区分
    Public Const USRSYUTI_KBN_UNFIN As String = "0"             '無
    Public Const USRSYUTI_KBN_FIN As String = "1"               '有
    Public Const USRSYUTI_NM_UNFIN As String = "無"
    Public Const USRSYUTI_NM_FIN As String = "有"

    'ユーザー周知必要有無区分コンボボックス作成用
    Public UsrSyutiKbn(,) As String = {{"", ""}, {USRSYUTI_KBN_UNFIN, USRSYUTI_NM_UNFIN}, {USRSYUTI_KBN_FIN, USRSYUTI_NM_FIN}}

    '通常・緊急区分
    Public Const TUJYOKINKYU_KBN_NORMAL As String = "0"             '通常
    Public Const TUJYOKINKYU_KBN_EMERGENCY As String = "1"          '緊急
    Public Const TUJYOKINKYU_NM_NORMAL As String = "通常"
    Public Const TUJYOKINKYU_NM_EMERGENCY As String = "緊急"

    '通常・緊急コンボボックス作成用
    Public TujyoKinkyuKbn(,) As String = {{"", ""}, {TUJYOKINKYU_KBN_NORMAL, TUJYOKINKYU_NM_NORMAL}, {TUJYOKINKYU_KBN_EMERGENCY, TUJYOKINKYU_NM_EMERGENCY}}

    'リリースシステム区分
    Public Const RELSYSTEM_KBN_IRAI As String = "0"             'リリース依頼受領システム
    Public Const RELSYSTEM_KBN_TAISYO As String = "1"           'リリース実施対象システム

    'リリース検索画面(HBKF0101)
    Public Const F0101_E001 As String = "検索結果から1行選択してください。"
    Public Const F0101_W001 As String = "検索結果の件数が{0}件を超えているため、表示に時間がかかる場合があります。" & vbCrLf & "検索結果の表示を行いますか？"
    Public Const F0101_I001 As String = "該当する結果はありません。"
    Public Const F0101_I002 As String = "出力が完了しました。"
    'リリース検索検索一覧EXCEL出力(HBKF0102)
    Public Const F0102_FILE_KIND As String = "Excel Files (*.xlsx)|*.xlsx"

    'リリース登録画面(HBKF0201)
    'Public Const REL_SCR_CALLMOTO_HOKA As Integer = 0                                   '呼び元画面：検索一覧以外
    'Public Const REL_SCR_CALLMOTO_ICHIRAN As Integer = 1                                '呼び元画面：検索一覧
    'Public Const REL_SCR_CALLMOTO_CHG As Integer = 2                                    '呼び元画面：変更登録画面
    Public Const F0201_E001 As String = "{0}のデータが取得できませんでした。"
    Public Const F0201_E002 As String = "対応関係者でないため、参照できません。"
    Public Const F0201_E003 As String = "ステータスを選択してください。"
    Public Const F0201_E004 As String = "ステータスを完了にする場合、ユーザー周知必要有無を選択してください"
    Public Const F0201_E005 As String = "リリース依頼受領システムが重複しています。"
    Public Const F0201_E006 As String = "リリース実施対象システムが重複しています。"
    Public Const F0201_E007 As String = "ログインユーザーのIDは削除できません。"
    Public Const F0201_E008 As String = "作業中のグループは削除できません。"
    Public Const F0201_E009 As String = "ロックを解除しました。入力内容を以下に出力しています。" & vbCrLf & "{0}"
    Public Const F0201_E010 As String = "ログNoを新規に採番できませんでした。"
    Public Const F0201_E011 As String = "データ更新中にエラーとなりました。入力内容を以下に出力しています。" & vbCrLf & "{0}"
    Public Const F0201_E012 As String = "検索結果から1行選択してください。"
    Public Const F0201_E013 As String = "リリース着手日時の日付を入力してください。"
    Public Const F0201_E014 As String = "リリース着手日時の時刻を入力してください。"
    Public Const F0201_E015 As String = "リリース終了日時の日付を入力してください。"
    Public Const F0201_E016 As String = "リリース終了日時の時刻を入力してください。"
    Public Const F0201_E017 As String = "ファイルが見つかりません。"
    Public Const F0201_I001 As String = "登録が完了しました。"

    'リリース登録メール作成(HBKF0301)
    Public Const RELEASE_PERMUTATION_NORMAL As String = "0"
    Public Const RELEASE_PERMUTATION_DATE As String = "1"
    Public Const RELEASE_PERMUTATION_MULTILINE As String = "2"
    Public Const RELEASE_PERMUTATION_MULTDATE As String = "3"
    'リリース登録メール作成置換え配列
    '[ADD] 2015/08/24 y.naganuma [ログイン置換：ユーザー姓のみ]を追加 
    Public ReadOnly RELEASE_PERMUTATION(,) As String = _
        {{"[HBK共通：NOW：", RELEASE_PERMUTATION_DATE}, {"[ログイン置換：グループ名]", RELEASE_PERMUTATION_NORMAL}, {"[ログイン置換：ユーザーID]", RELEASE_PERMUTATION_NORMAL}, {"[ログイン置換：ユーザー名]", RELEASE_PERMUTATION_NORMAL}, _
         {"[REL置換：リリース管理番号]", RELEASE_PERMUTATION_NORMAL}, {"[REL置換：リリース受付番号]", RELEASE_PERMUTATION_NORMAL}, {"[REL置換：ステータス]", RELEASE_PERMUTATION_NORMAL}, {"[REL置換：タイトル]", RELEASE_PERMUTATION_NORMAL}, {"[REL置換：概要]", RELEASE_PERMUTATION_NORMAL}, _
         {"[REL置換：依頼日（起票日）：", RELEASE_PERMUTATION_DATE}, {"[REL置換：通常・緊急]", RELEASE_PERMUTATION_NORMAL}, {"[REL置換：ユーザー周知必要有無]", RELEASE_PERMUTATION_NORMAL}, {"[REL置換：リリース予定日時（目安）：", RELEASE_PERMUTATION_DATE}, _
         {"[REL置換：リリース依頼受領システム]", RELEASE_PERMUTATION_MULTILINE}, {"[REL置換：リリース実施対象システム]", RELEASE_PERMUTATION_MULTILINE}, _
         {"[REL置換：担当者業務チーム]", RELEASE_PERMUTATION_NORMAL}, {"[REL置換：リリース担当者]", RELEASE_PERMUTATION_NORMAL}, {"[REL置換：リリース着手日時：", RELEASE_PERMUTATION_DATE}, {"[REL置換：リリース終了日時：", RELEASE_PERMUTATION_DATE}, _
         {"[REL置換：登録日時：", RELEASE_PERMUTATION_DATE}, {"[REL置換：登録者業務チーム]", RELEASE_PERMUTATION_NORMAL}, {"[REL置換：登録者]", RELEASE_PERMUTATION_NORMAL}, _
         {"[REL置換：最終更新日時：", RELEASE_PERMUTATION_DATE}, {"[REL置換：最終更新者業務チーム]", RELEASE_PERMUTATION_NORMAL}, {"[REL置換：最終更新者]", RELEASE_PERMUTATION_NORMAL}, _
         {"[REL置換：関連ファイル情報：", RELEASE_PERMUTATION_MULTDATE}, {"[REL置換：会議情報：", RELEASE_PERMUTATION_MULTDATE}, _
         {"[REL置換：テキスト1]", RELEASE_PERMUTATION_NORMAL}, {"[REL置換：テキスト2]", RELEASE_PERMUTATION_NORMAL}, {"[REL置換：テキスト3]", RELEASE_PERMUTATION_NORMAL}, {"[REL置換：テキスト4]", RELEASE_PERMUTATION_NORMAL}, {"[REL置換：テキスト5]", RELEASE_PERMUTATION_NORMAL}, _
         {"[REL置換：フラグ1]", RELEASE_PERMUTATION_NORMAL}, {"[REL置換：フラグ2]", RELEASE_PERMUTATION_NORMAL}, {"[REL置換：フラグ3]", RELEASE_PERMUTATION_NORMAL}, {"[REL置換：フラグ4]", RELEASE_PERMUTATION_NORMAL}, {"[REL置換：フラグ5]", RELEASE_PERMUTATION_NORMAL}, _
         {"[REL置換：対応関係者情報]", RELEASE_PERMUTATION_MULTILINE}, {"[REL置換：グループ履歴]", RELEASE_PERMUTATION_NORMAL}, {"[REL置換：担当者履歴]", RELEASE_PERMUTATION_NORMAL}, {"[REL置換：プロセスリンク情報]", RELEASE_PERMUTATION_MULTILINE}, {"[REL置換：担当者氏]", INCIDENT_PERMUTATION_NORMAL}, {"[ログイン置換：ユーザー姓のみ]", RELEASE_PERMUTATION_NORMAL}}



End Module

