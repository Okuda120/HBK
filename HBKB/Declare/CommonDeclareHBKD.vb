Public Module CommonDeclareHBKD

    'フォーマットファイル名【問題検索一覧Excel出力】
    Public Const FORMAT_PROBLEM_SEARCH As String = "問題検索一覧.xlsx"
    '出力ファイル名【問題検索一覧Excel出力】
    Public Const FILENM_PROBLEM_SEARCH As String = "問題検索一覧"

    '問題検索画面(HBKD0101)
    Public Const D0101_E001 As String = "検索結果から1行選択してください。"
    Public Const D0101_E002 As String = "{0}の日付を入力してください。"
    Public Const D0101_W001 As String = "検索結果の件数が{0}件を超えているため、表示に時間がかかる場合があります。" & vbCrLf & "検索結果の表示を行いますか？"
    Public Const D0101_I001 As String = "該当する結果はありません。"
    Public Const D0101_I002 As String = "出力が完了しました。"
    Public Const D0101_RDO_CHOKUSETSU As String = "C"
    Public Const D0101_RDO_KANYO As String = "K"

    '問題検索検索一覧EXCEL出力(HBKD0102)
    Public Const D0102_FILE_KIND As String = "Excel Files (*.xlsx)|*.xlsx"

    '問題登録画面(HBKD0201)
    'Public Const PRB_SCR_CALLMOTO_HOKA As Integer = 0                   '呼び元画面：検索一覧以外
    'Public Const PRB_SCR_CALLMOTO_ICHIRAN As Integer = 1                '呼び元画面：検索一覧
    'Public Const PRB_SCR_CALLMOTO_INC As Integer = 2                    '呼び元画面：インシデント登録
    Public Const D0201_E001 As String = "{0}のデータが取得できませんでした。"
    Public Const D0201_E002 As String = "ステータスを選択してください。"
    Public Const D0201_E003 As String = "ステータスを完了にする場合、開始日時を入力してください。"
    Public Const D0201_E004 As String = "ステータスを完了にする場合、発生原因を選択してください。"
    Public Const D0201_E005 As String = "ステータスを完了にする場合、対象システムを選択してください。"
    Public Const D0201_E006 As String = "ステータスを完了にする場合、タイトルを入力してください。"
    Public Const D0201_E007 As String = "ステータスを完了にする場合、内容を入力してください。"
    Public Const D0201_E008 As String = "ステータスを完了にする場合、担当グループを選択してください。"
    Public Const D0201_E009 As String = "ステータスを完了にする場合、担当IDを入力してください。"
    Public Const D0201_E010 As String = "ステータスを完了にする場合、担当氏名を入力してください。"
    Public Const D0201_E011 As String = "ステータスを完了にする場合、作業予実の作業ステータスを全て選択してください。"
    Public Const D0201_E012 As String = "ステータスを完了にする場合、作業予実の作業内容を全て入力してください。"
    Public Const D0201_E013 As String = "ステータスを完了にする場合、作業予実の作業開始日時を全て入力してください。"
    Public Const D0201_E014 As String = "ステータスを完了にする場合、作業予実の対象システムを全て選択してください。"
    Public Const D0201_E015 As String = "ステータスを完了にする場合、作業予実の作業担当を全て1人以上設定してください"
    Public Const D0201_E016 As String = "作業予実の作業開始日時と作業完了日時の範囲が正しくありません。"
    Public Const D0201_E017 As String = "対応関係者でないため、参照できません。"
    Public Const D0201_E018 As String = "登録済みの作業履歴は削除できません。"
    Public Const D0201_E019 As String = "作業中のグループは削除できません。"
    Public Const D0201_E020 As String = "ログインユーザーのIDは削除できません。"
    Public Const D0201_E021 As String = "データ更新中にエラーとなりました。入力内容を以下に出力しています。" & vbCrLf & "{0}"
    Public Const D0201_E022 As String = "ログNoを新規に採番できませんでした。"
    Public Const D0201_E023 As String = "作業予実の追加または変更を実施していない為、登録することはできません。"
    Public Const D0201_E024 As String = "CYSPR情報で重複している行があります。"
    Public Const D0201_E025 As String = "別画面が表示中のため、この画面を閉じることはできません。"
    Public Const D0201_E026 As String = "検索結果から1行選択してください。"
    Public Const D0201_E027 As String = "ファイルが見つかりません。"
    Public Const D0201_E028 As String = "{0}の日付を入力してください。"
    Public Const D0201_E029 As String = "{0}の時刻を入力してください。"
    Public Const D0201_E030 As String = "作業履歴の作業担当を選択できません。" & vbCrLf & "（最大：{0}名）"
    Public Const D0201_W001 As String = "ロックを解除しました。入力内容を以下に出力しています。" & vbCrLf & "{0}"
    Public Const D0201_W002 As String = "作業予実を編集した内容が登録されていません。" & vbCrLf & "編集内容が破棄されますがリフレッシュしますか？"
    '[MOD]2013/03/21 t.fukuo 閾値超過の場合文字削除しないよう修正 START
    'Public Const D0201_W003 As String = "入力内容が3000文字を超えたため、以降の文字を削除しました。"
    Public Const D0201_W003 As String = "入力内容が3000文字を超えています。" & vbCrLf & "3000文字以内になるまで編集してください。"
    '[MOD]2013/03/21 t.fukuo 閾値超過の場合文字削除しないよう修正 END
    Public Const D0201_I001 As String = "登録が完了しました。"
    Public Const PRB_WKRIREKI_MAXTANTO As Integer = 50                   '作業履歴の担当者の最大値

    '問題メール作成(HBKD0203)
    Public Const PROBLEM_PERMUTATION_NORMAL As String = "0"
    Public Const PROBLEM_PERMUTATION_DATE As String = "1"
    Public Const PROBLEM_PERMUTATION_MULTILINE As String = "2"
    Public Const PROBLEM_PERMUTATION_MULTDATE As String = "3"
    'メール作成(問題登録画面置換え)
    '[ADD] 2015/08/24 y.naganuma [ログイン置換：ユーザー姓のみ]を追加 
    Public ReadOnly PROBLEM_PERMUTATION(,) As String = {{"[HBK共通：NOW：", PROBLEM_PERMUTATION_DATE}, {"[ログイン置換：グループ名]", PROBLEM_PERMUTATION_NORMAL}, {"[ログイン置換：ユーザーID]", PROBLEM_PERMUTATION_NORMAL}, _
                {"[ログイン置換：ユーザー名]", PROBLEM_PERMUTATION_NORMAL}, {"[PRO置換：問題管理番号]", PROBLEM_PERMUTATION_NORMAL}, {"[PRO置換：ステータス]", PROBLEM_PERMUTATION_NORMAL}, {"[PRO置換：開始日時：", PROBLEM_PERMUTATION_DATE}, _
                {"[PRO置換：完了日時：", PROBLEM_PERMUTATION_DATE}, {"[PRO置換：発生原因]", PROBLEM_PERMUTATION_NORMAL}, {"[PRO置換：タイトル]", PROBLEM_PERMUTATION_NORMAL}, {"[PRO置換：内容]", PROBLEM_PERMUTATION_NORMAL}, _
                {"[PRO置換：登録日時：", PROBLEM_PERMUTATION_DATE}, {"[PRO置換：登録者業務チーム]", PROBLEM_PERMUTATION_NORMAL}, {"[PRO置換：登録者]", PROBLEM_PERMUTATION_NORMAL}, {"[PRO置換：最終更新日時：", PROBLEM_PERMUTATION_DATE}, _
                {"[PRO置換：最終更新者業務チーム]", PROBLEM_PERMUTATION_NORMAL}, {"[PRO置換：最終更新者]", PROBLEM_PERMUTATION_NORMAL}, {"[PRO置換：対象システム]", PROBLEM_PERMUTATION_NORMAL}, {"[PRO置換：担当者業務チーム]", PROBLEM_PERMUTATION_NORMAL}, _
                {"[PRO置換：問題担当者]", PROBLEM_PERMUTATION_NORMAL}, {"[PRO置換：対処]", PROBLEM_PERMUTATION_NORMAL}, {"[PRO置換：対処の承認者]", PROBLEM_PERMUTATION_NORMAL}, {"[PRO置換：承認記録者]", PROBLEM_PERMUTATION_NORMAL}, _
                {"[PRO置換：関連ファイル情報]", PROBLEM_PERMUTATION_MULTILINE}, {"[PRO置換：CYSPR]", PROBLEM_PERMUTATION_MULTILINE}, {"[PRO置換：作業履歴：", PROBLEM_PERMUTATION_MULTDATE}, {"[PRO置換：会議情報：", PROBLEM_PERMUTATION_MULTDATE}, _
                {"[PRO置換：テキスト1]", PROBLEM_PERMUTATION_NORMAL}, {"[PRO置換：テキスト2]", PROBLEM_PERMUTATION_NORMAL}, {"[PRO置換：テキスト3]", PROBLEM_PERMUTATION_NORMAL}, {"[PRO置換：テキスト4]", PROBLEM_PERMUTATION_NORMAL}, _
                {"[PRO置換：テキスト5]", PROBLEM_PERMUTATION_NORMAL}, {"[PRO置換：フラグ1]", PROBLEM_PERMUTATION_NORMAL}, {"[PRO置換：フラグ2]", PROBLEM_PERMUTATION_NORMAL}, {"[PRO置換：フラグ3]", PROBLEM_PERMUTATION_NORMAL}, _
                {"[PRO置換：フラグ4]", PROBLEM_PERMUTATION_NORMAL}, {"[PRO置換：フラグ5]", PROBLEM_PERMUTATION_NORMAL}, {"[PRO置換：対応関係者情報]", PROBLEM_PERMUTATION_MULTILINE}, {"[PRO置換：グループ履歴]", PROBLEM_PERMUTATION_NORMAL}, _
                {"[PRO置換：担当者履歴]", PROBLEM_PERMUTATION_NORMAL}, {"[PRO置換：プロセスリンク情報]", PROBLEM_PERMUTATION_MULTILINE}, {"[PRO置換：担当者氏]", INCIDENT_PERMUTATION_NORMAL}, {"[ログイン置換：ユーザー姓のみ]", RELEASE_PERMUTATION_NORMAL}}


    '問題登録（単票出力）（HBKD0202）
    Public Const FORMAT_PROBLEM_TANPYO As String = "問題単票フォーマット.xlsx"      'フォマットファイル名
    Public Const SHEETNAME_PROBLEM_TANPYO As String = "agree_prbtanpyo"                        'シート名
    Public Const CELLNAME_AP_PRBNMB As String = "PrbNmb"                            'セル名（ヘッダ）:問題
    Public Const CELLNAME_AP_TITLE As String = "Title"                              'セル名（ヘッダ）:タイトル
    Public Const CELLNAME_AP_STARTDT As String = "StartDT"                          'セル名（ヘッダ）:開始日時
    Public Const CELLNAME_AP_KANRYODT As String = "KanryoDT"                        'セル名（ヘッダ）:完了日時
    Public Const CELLNAME_AP_PROCESSSTATE As String = "ProcessState"                'セル名（ヘッダ）:ステータス
    Public Const CELLNAME_AP_SYSTEM As String = "System"                            'セル名（ヘッダ）:対象システム
    Public Const CELLNAME_AP_PRBCASE As String = "PrbCase"                          'セル名（ヘッダ）:発生原因
    Public Const CELLNAME_AP_NAIYO As String = "Naiyo"                              'セル名（ヘッダ）:内容
    Public Const CELLNAME_AP_TAISYO As String = "Taisyo"                            'セル名（ヘッダ）:対処
    Public Const CELLNAME_AP_TANTOGRPCD As String = "TantoGrpCD"                    'セル名（担当情報）:担当グループ
    Public Const CELLNAME_AP_TANTOID As String = "PrbTantoID"                       'セル名（担当情報）:担当ID
    Public Const CELLNAME_AP_TANTONM As String = "PrbTantoNM"                       'セル名（担当情報）:担当者氏名
    Public Const CELLNAME_AP_APPROVERID As String = "ApproverID"                    'セル名（対処承認者情報）:対処承認者ID
    Public Const CELLNAME_AP_APPROVERNM As String = "ApproverNM"                    'セル名（対処承認者情報）:対処承認者氏名
    Public Const CELLNAME_AP_RECORDERID As String = "RecorderID"                    'セル名（承認記録者情報）:承認記録者ID
    Public Const CELLNAME_AP_RECORDERNM As String = "RecorderNM"                    'セル名（承認記録者情報）:承認記録氏名
    Public Const CELLNAME_AP_RELATIONKBN As String = "RelationKbn"                  'セル名（対応関係者情報）:区分
    Public Const CELLNAME_AP_RELATIONID As String = "RelationID"                    'セル名（対応関係者情報）:ID
    Public Const CELLNAME_AP_RELATIONGRPNM As String = "RelationGrpNM"              'セル名（対応関係者情報）:グループ名
    Public Const CELLNAME_AP_RELATIONUSRNM As String = "RelationUsrNM"              'セル名（対応関係者情報）:ユーザー名
    Public Const CELLNAME_AP_GROUPRIREKI As String = "GroupRireki"                  'セル名（担当履歴情報）:グループ履歴
    Public Const CELLNAME_AP_TANTORIREKI As String = "TantoRireki"                  'セル名（担当履歴情報）:担当者履歴
    Public Const CELLNAME_AP_LINKNMB As String = "LinkNmb"                          'セル名（プロセスリンク情報）:番号
    Public Const CELLNAME_AP_CYSPRNMB As String = "CysprNmb"                        'セル名（CYSPR情報）:番号
    Public Const CELLNAME_AP_MEETINGNMB As String = "MeetingNmb"                    'セル名（会議情報）:番号
    Public Const CELLNAME_AP_MEETINGTITLE As String = "MeetingTitle"                'セル名（会議情報）:タイトル
    Public Const CELLNAME_AP_MEETINGRESULTKBN As String = "MeetingResultKbn"        'セル名（会議情報）:承認
    Public Const CELLNAME_AP_FREEBIKO1 As String = "FreeBIko1"                      'セル名（フリー入力情報）:フリーワード1
    Public Const CELLNAME_AP_FREEBIKO2 As String = "FreeBIko2"                      'セル名（フリー入力情報）:フリーワード2
    Public Const CELLNAME_AP_FREEBIKO3 As String = "FreeBIko3"                      'セル名（フリー入力情報）:フリーワード3
    Public Const CELLNAME_AP_FREEBIKO4 As String = "FreeBIko4"                      'セル名（フリー入力情報）:フリーワード4
    Public Const CELLNAME_AP_FREEBIKO5 As String = "FreeBIko5"                      'セル名（フリー入力情報）:フリーワード5
    Public Const CELLNAME_AP_FREEFLG1 As String = "FreeFlg1"                        'セル名（フリー入力情報）:フリーフラグ1
    Public Const CELLNAME_AP_FREEFLG2 As String = "FreeFlg2"                        'セル名（フリー入力情報）:フリーフラグ2
    Public Const CELLNAME_AP_FREEFLG3 As String = "FreeFlg3"                        'セル名（フリー入力情報）:フリーフラグ3
    Public Const CELLNAME_AP_FREEFLG4 As String = "FreeFlg4"                        'セル名（フリー入力情報）:フリーフラグ4
    Public Const CELLNAME_AP_FREEFLG5 As String = "FreeFlg5"                        'セル名（フリー入力情報）:フリーフラグ5
    Public Const CELLNAME_AP_WORKSTATE As String = "WorkState"                      'セル名（作業予実）:作業ステータス
    Public Const CELLNAME_AP_WORKSCEDT As String = "WorkSceDT"                      'セル名（作業予実）:作業予定日時
    Public Const CELLNAME_AP_WORKSTDT As String = "WorkStDT"                        'セル名（作業予実）:作業開始日時
    Public Const CELLNAME_AP_WORKEDDT As String = "WorkEdDT"                        'セル名（作業予実）:作業終了日時
    Public Const CELLNAME_AP_WORKSYSTEM As String = "WorkSystem"                    'セル名（作業予実）:対象システム
    Public Const CELLNAME_AP_WORKTANTONM As String = "WorkTantoNM"                  'セル名（作業予実）:作業担当者
    Public Const CELLNAME_AP_WORKNAIYO As String = "WorkNaiyo"                      'セル名（作業予実）:作業内容

End Module

