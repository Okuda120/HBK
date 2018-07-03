Public Module CommonDeclareHBKE

    'フォーマットファイル名【変更検索一覧Excel出力】
    Public Const FORMAT_CHANGE_SEARCH As String = "変更検索一覧.xlsx"
    '出力ファイル名【変更検索一覧Excel出力】
    Public Const FILENM_CHANGE_SEARCH As String = "変更検索一覧"


    '変更検索画面(HBKE0101)

    Public Const E0101_E001 As String = "検索結果から1行選択してください。"
    Public Const E0101_E002 As String = "{0}の日付を入力してください。"
    Public Const E0101_W001 As String = "検索結果の件数が{0}件を超えているため、表示に時間がかかる場合があります。" & vbCrLf & "検索結果の表示を行いますか？"
    Public Const E0101_I001 As String = "該当する結果はありません。"
    Public Const E0101_I002 As String = "出力が完了しました。"

    '変更検索検索一覧EXCEL出力(HBKE0102)
    Public Const E0102_FILE_KIND As String = "Excel Files (*.xlsx)|*.xlsx"

    '変更登録画面（HBKE201）
    Public Const E0201_E001 As String = "{0}のデータが取得できませんでした。"
    Public Const E0201_E002 As String = "別画面が表示中のため、この画面を閉じることはできません。"
    Public Const E0201_E003 As String = "ステータスを選択してください。"
    Public Const E0201_E004 As String = "ステータスを完了にする場合、開始日時を選択してください。"
    Public Const E0201_E005 As String = "ステータスを完了にする場合、対象システムを選択してください。"
    Public Const E0201_E006 As String = "ステータスを完了にする場合、タイトルを入力してください。"
    Public Const E0201_E007 As String = "ステータスを完了にする場合、内容を入力してください。"
    Public Const E0201_E008 As String = "ステータスを完了にする場合、担当グループを選択してください。"
    Public Const E0201_E009 As String = "ステータスを完了にする場合、担当IDを入力してください。"
    Public Const E0201_E010 As String = "ステータスを完了にする場合、担当氏名を入力してください。"
    Public Const E0201_E011 As String = "ログインユーザーのIDは削除できません。"
    Public Const E0201_E012 As String = "作業中のグループは削除できません。"
    Public Const E0201_E013 As String = "ログNoを新規に採番できませんでした。"
    Public Const E0201_E014 As String = "データ更新中にエラーとなりました。入力内容を以下に出力しています。" & vbCrLf & "{0}"
    Public Const E0201_E015 As String = "CYSPR情報で重複している行があります。"
    Public Const E0201_E016 As String = "対応関係者でないため、参照できません。"
    Public Const E0201_E017 As String = "検索結果から1行選択してください。"
    Public Const E0201_E018 As String = "開始日時の日付を入力してください。"
    Public Const E0201_E019 As String = "開始日時の時刻を入力してください。"
    Public Const E0201_E020 As String = "完了日時の日付を入力してください。"
    Public Const E0201_E021 As String = "完了日時の時刻を入力してください。"
    Public Const E0201_E022 As String = "ファイルが見つかりません。"
    Public Const E0201_W001 As String = "ロックを解除しました。入力内容を以下に出力しています。" & vbCrLf & "{0}"
    Public Const E0201_I001 As String = "登録が完了しました。"

    '変更登録メール作成(HBKE0202)
    Public Const CHANGE_PERMUTATION_NORMAL As String = "0"
    Public Const CHANGE_PERMUTATION_DATE As String = "1"
    Public Const CHANGE_PERMUTATION_MULTILINE As String = "2"
    Public Const CHANGE_PERMUTATION_MULTDATE As String = "3"
    '変更登録メール作成置換え配列
    '[ADD] 2015/08/24 y.naganuma [ログイン置換：ユーザー姓のみ]を追加 
    Public ReadOnly CHANGE_PERMUTATION(,) As String = _
        {{"[HBK共通：NOW：", CHANGE_PERMUTATION_DATE}, {"[ログイン置換：グループ名]", CHANGE_PERMUTATION_NORMAL}, {"[ログイン置換：ユーザーID]", CHANGE_PERMUTATION_NORMAL}, {"[ログイン置換：ユーザー名]", CHANGE_PERMUTATION_NORMAL}, _
         {"[CHG置換：変更管理番号]", CHANGE_PERMUTATION_NORMAL}, {"[CHG置換：ステータス]", CHANGE_PERMUTATION_NORMAL}, {"[CHG置換：開始日時：", CHANGE_PERMUTATION_DATE}, {"[CHG置換：完了日時：", CHANGE_PERMUTATION_DATE}, {"[CHG置換：タイトル]", CHANGE_PERMUTATION_NORMAL}, _
         {"[CHG置換：内容]", CHANGE_PERMUTATION_NORMAL}, {"[CHG置換：登録日時：", CHANGE_PERMUTATION_DATE}, {"[CHG置換：登録者業務チーム]", CHANGE_PERMUTATION_NORMAL}, {"[CHG置換：登録者]", CHANGE_PERMUTATION_NORMAL}, {"[CHG置換：最終更新日時：", CHANGE_PERMUTATION_DATE}, _
         {"[CHG置換：最終更新者業務チーム]", CHANGE_PERMUTATION_NORMAL}, {"[CHG置換：最終更新者]", CHANGE_PERMUTATION_NORMAL}, {"[CHG置換：対象システム]", CHANGE_PERMUTATION_NORMAL}, {"[CHG置換：担当者業務チーム]", CHANGE_PERMUTATION_NORMAL}, {"[CHG置換：変更担当者]", CHANGE_PERMUTATION_NORMAL}, {"[CHG置換：対処]", CHANGE_PERMUTATION_NORMAL}, _
         {"[CHG置換：変更の承認者]", CHANGE_PERMUTATION_NORMAL}, {"[CHG置換：承認記録者]", CHANGE_PERMUTATION_NORMAL}, {"[CHG置換：関連ファイル情報：", CHANGE_PERMUTATION_MULTDATE}, {"[CHG置換：CYSPR]", CHANGE_PERMUTATION_MULTILINE}, {"[CHG置換：会議情報：", CHANGE_PERMUTATION_MULTDATE}, _
         {"[CHG置換：テキスト1]", CHANGE_PERMUTATION_NORMAL}, {"[CHG置換：テキスト2]", CHANGE_PERMUTATION_NORMAL}, {"[CHG置換：テキスト3]", CHANGE_PERMUTATION_NORMAL}, {"[CHG置換：テキスト4]", CHANGE_PERMUTATION_NORMAL}, {"[CHG置換：テキスト5]", CHANGE_PERMUTATION_NORMAL}, _
         {"[CHG置換：フラグ1]", CHANGE_PERMUTATION_NORMAL}, {"[CHG置換：フラグ2]", CHANGE_PERMUTATION_NORMAL}, {"[CHG置換：フラグ3]", CHANGE_PERMUTATION_NORMAL}, {"[CHG置換：フラグ4]", CHANGE_PERMUTATION_NORMAL}, {"[CHG置換：フラグ5]", CHANGE_PERMUTATION_NORMAL}, _
         {"[CHG置換：対応関係者情報]", CHANGE_PERMUTATION_MULTILINE}, {"[CHG置換：グループ履歴]", CHANGE_PERMUTATION_NORMAL}, {"[CHG置換：担当者履歴]", CHANGE_PERMUTATION_NORMAL}, {"[CHG置換：プロセスリンク情報]", CHANGE_PERMUTATION_MULTILINE}, {"[CHG置換：担当者氏]", INCIDENT_PERMUTATION_NORMAL}, {"[ログイン置換：ユーザー姓のみ]", RELEASE_PERMUTATION_NORMAL}}


End Module

