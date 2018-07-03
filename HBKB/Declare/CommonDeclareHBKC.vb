Public Module CommonDeclareHBKC


    'バラすフラグ
    Public Const CEPALATEFLG_ON As String = "1"     'ON
    Public Const CEPALATEFLG_OFF As String = "0"    'OFF
    Public Const CEPALATEFLG_ON_VW As String = "○" 'ON：一覧表示
    Public Const CEPALATEFLG_OFF_VW As String = ""  'OFF：一覧表示

    'フォーマットファイル名【インシデント検索一覧Excel出力】
    Public Const FORMAT_INCIDENT_SEARCH As String = "インシデント検索一覧.xlsx"
    '出力ファイル名【インシデント検索一覧Excel出力】
    Public Const FILENM_INCIDENT_SEARCH As String = "インシデント検索一覧"

    'インシデント検索一覧画面（HBKC0101）
    Public Const C0101_I001 As String = "出力が完了しました。"
    Public Const C0101_I002 As String = "該当する結果はありません。"
    Public Const C0101_E001 As String = "検索結果から1行選択してください。"
    Public Const C0101_E002 As String = "{0}の日付を入力してください。"
    Public Const C0101_W001 As String = "検索結果の件数が{0}件を超えているため、表示に時間がかかる場合があります。" & vbCrLf & "検索結果の表示を行いますか？"

    'インシデント検索一覧EXCEL出力(HBKC0102)
    Public Const C0102_FILE_KIND As String = "Excel Files (*.xlsx)|*.xlsx"
    Public Const C0102_RDO_CHOKUSETSU As String = "C"
    Public Const C0102_RDO_KANYO As String = "K"

    'インシデント登録画面（HBKC0201）
    'Public Const INC_SCR_CALLMOTO_HOKA As Integer = 0                   '呼び元画面：検索一覧以外
    'Public Const INC_SCR_CALLMOTO_ICHIRAN As Integer = 1                '呼び元画面：検索一覧
    'Public Const INC_SCR_CALLMOTO_REG As Integer = 2                    '呼び元画面：問題登録ボタン
    'Public Const KANKEI_CHECK_NONE As Integer = 0                   '対応関係者チェック：参照不可
    'Public Const KANKEI_CHECK_REF As Integer = 1                    '対応関係者チェック：参照
    'Public Const KANKEI_CHECK_EDIT As Integer = 2                   '対応関係者チェック：編集
    Public Const PATH_WEB As String = "http://"
    Public Const PATH_SCCM As String = "http://vms00061/SMSReporting_EXC/Report.asp?ReportID=1&variable="
    Public Const PCANY_CHF_MOTO_NAME As String = "c_auto_moto.CHF"
    Public Const PCANY_CMD_NAME As String = "pcanyrun.cmd"
    Public Const PCANY_VBS_NAME As String = "pcanycall.vbs"
    '[ADD]2016/03/08 e.okamura LAPLINK遠隔ボタン追加 START
    Public Const LAPLINK_CMD_PATH As String = "LAPLINK.cmd"
    '[ADD]2016/03/08 e.okamura LAPLINK遠隔ボタン追加 END

    Public Const C0201_E001 As String = "{0}のデータが取得できませんでした。"
    Public Const C0201_E002 As String = "別画面が表示中のため、この画面を閉じることはできません。"
    Public Const C0201_E003 As String = "選択した機器は、交換機器ではありません。" & vbCrLf & "同じ交換番号の機器を選択してください。"
    Public Const C0201_E004 As String = "相手メールアドレスが正しくありません。"
    Public Const C0201_E005 As String = "ステータスを選択してください。"
    Public Const C0201_E006 As String = "ステータスを完了にする場合、受付手段を選択してください。"
    Public Const C0201_E007 As String = "ステータスを完了にする場合、発生日時を入力してください。"
    Public Const C0201_E008 As String = "ステータスを完了にする場合、インシデント種別を選択してください。"
    Public Const C0201_E009 As String = "ステータスを完了にする場合、ドメインを選択してください。"
    Public Const C0201_E010 As String = "ステータスを完了にする場合、対象システムを選択してください。"
    Public Const C0201_E011 As String = "ステータスを完了にする場合、タイトルを入力してください。"
    Public Const C0201_E012 As String = "ステータスを完了にする場合、受付内容を入力してください。"
    Public Const C0201_E013 As String = "ステータスを完了にする場合、対応結果を入力してください。"
    Public Const C0201_E014 As String = "登録済みの作業履歴は削除できません。"
    Public Const C0201_E015 As String = "ステータスを完了にする場合、担当グループを選択してください。"
    Public Const C0201_E016 As String = "ステータスを完了にする場合、担当IDを入力してください。"
    Public Const C0201_E017 As String = "ステータスを完了にする場合、担当氏名を入力してください。"
    Public Const C0201_E018 As String = "ステータスを完了にする場合、作業履歴の経過種別を全て選択してください。"
    Public Const C0201_E019 As String = "ステータスを完了にする場合、作業履歴の作業内容を全て入力してください。"
    Public Const C0201_E020 As String = "ステータスを完了にする場合、作業履歴の作業開始日時を全て入力してください。"
    Public Const C0201_E021 As String = "ステータスを完了にする場合、作業履歴の対象システムを全て選択してください。"
    Public Const C0201_E022 As String = "ステータスを完了にする場合、作業履歴の作業担当を全て1人以上設定してください。"
    Public Const C0201_E023 As String = "作業履歴の作業開始日時と作業完了日時の範囲が正しくありません。"
    Public Const C0201_E024 As String = "作業中のグループは削除できません。"
    Public Const C0201_E025 As String = "ログインユーザーのIDは削除できません。"
    Public Const C0201_E026 As String = "ログNoを新規に採番できませんでした。"
    Public Const C0201_E027 As String = "履歴番号を新規に採番できませんでした。"
    Public Const C0201_E028 As String = "設置作業を完了にする場合は利用者情報の{0}を入力してください。"
    Public Const C0201_E029 As String = "設置作業を完了にする場合はレンタル期間（FROM、TO）を入力してください。"
    Public Const C0201_E030 As String = "設置作業を完了にする場合は機器利用情報の{0}を入力してください。"
    Public Const C0201_E031 As String = "設置作業を完了にする場合は管理者情報の{0}を入力してください。"
    Public Const C0201_E032 As String = "廃棄作業を完了にする場合は機器状態を入力してください。"
    Public Const C0201_E033 As String = "廃棄作業を完了にする場合はステータスを「廃棄」または「リユース」にしてください。"
    Public Const C0201_E034 As String = "作業履歴の追加または変更を実施していない為、登録することはできません。"
    Public Const C0201_E035 As String = "データ更新中にエラーとなりました。入力内容を以下に出力しています。" & vbCrLf & "{0}"
    Public Const C0201_E036 As String = "対応関係者でないため、参照できません。"
    Public Const C0201_E037 As String = "検索結果から1行選択してください。"
    Public Const C0201_E038 As String = "ファイルが見つかりません。"
    Public Const C0201_E039 As String = "{0}の日付を入力してください。"
    Public Const C0201_E040 As String = "{0}の時刻を入力してください。"
    Public Const C0201_E041 As String = "作業履歴の作業担当を選択できません。" & vbCrLf & "（最大：{0}名）"
    Public Const C0201_E042 As String = "設定ファイル(CHF)が取得できませんでした。"         'コピー元CHFファイル存在チェック
    Public Const C0201_E043 As String = "設定ファイル(CHF)の初期化に失敗しました。"         'CHF削除処理
    Public Const C0201_E044 As String = "実行ファイル(CMD)が取得できませんでした。"         '起動アプリケーションCMD存在チェック
    Public Const C0201_E045 As String = "実行ファイル(VBS)が取得できませんでした。"         '起動アプリケーションVBS存在チェック
    Public Const C0201_E046 As String = "セットIDを新規に採番できませんでした。"
    Public Const C0201_E047 As String = "セットが既に登録済のため、セットまたは機器を追加できません。"
    Public Const C0201_E048 As String = "設置作業を完了にする場合は設置情報の{0}を入力してください。"
    Public Const C0201_E049 As String = "交換された機器の作業を取消す場合は、交換対象の機器の作業も併せて取消してください。"
    '[ADD]2014/04/07 e.okamura 作業取消時セット機器更新修正 START
    Public Const C0201_E050 As String = "作業の「完了」と「取消」は同時に登録できません。" & vbCrLf & "「完了」のみ、または「取消」のみチェックを付けて登録してください。"
    '[ADD]2014/04/07 e.okamura 作業取消時セット機器更新修正 END
    '[ADD]2015/08/21 y.naganuma 完了反映時のチェックロジック追加対応 START
    Public Const C0201_E051 As String = "ステータスを完了にする場合、作業の「完了」、または「取消」にチェックを付けて登録してください。"
    '[ADD]2015/08/21 y.naganuma 完了反映時のチェックロジック追加対応 END
    Public Const C0201_W001 As String = "作業履歴を再取得します。" & vbCrLf & "宜しいですか？"
    Public Const C0201_W002 As String = "ServiceManagerにインシデント情報を連携します。" & vbCrLf & "宜しいですか？"
    Public Const C0201_W003 As String = "ロックを解除しました。入力内容を以下に出力しています。" & vbCrLf & "{0}"
    '[MOD]2013/03/21 t.fukuo 閾値超過の場合文字削除しないよう修正 START
    'Public Const C0201_W004 As String = "入力内容が3000文字を超えたため、以降の文字を削除しました。"
    Public Const C0201_W004 As String = "入力内容が3000文字を超えています。" & vbCrLf & "3000文字以内になるまで編集してください。"
    '[MOD]2013/03/21 t.fukuo 閾値超過の場合文字削除しないよう修正 END
    '[ADD]2014/04/07 e.okamura 作業取消時セット機器更新修正 START
    Public Const C0201_W005 As String = "下記の機器に対し、作業取消を行います。" & vbCrLf & _
                                        "{0}" & vbCrLf & _
                                        "作業取消後は、上記機器のセット情報を確認し、実機と齟齬がある場合は、" & vbCrLf & _
                                        "別途「追加設定」作業にて修正してください。"
    '[ADD]2014/04/07 e.okamura 作業取消時セット機器更新修正 END
    Public Const C0201_I001 As String = "登録が完了しました。"
    Public Const INC_WKRIREKI_MAXTANTO As Integer = 50                   '作業履歴の担当者の最大値

    'インシデント登録（出力）共通
    Public Const CELLNAME_RENTALSTDT As String = "RentalStDT"                                       '項目名:貸出開始日（申請日）
    Public Const CELLNAME_INCNMB As String = "IncNmb"                                               '項目名:管理番号
    Public Const CELLNAME_USRBUSYONM As String = "UsrBusyoNM"                                       '項目名:所属部署
    Public Const CELLNAME_PARTNERROOM As String = "PartnerRoom"                                     '項目名:番組名／所属班
    Public Const CELLNAME_PERTNERID As String = "PartnerID"                                         '項目名:PrismID
    Public Const CELLNAME_PERTNERNM As String = "PartnerNM"                                         '項目名:氏名
    Public Const CELLNAME_KINDCD_KIKINMB As String = "KindCD_KikiNmb"                               '項目名:機器管理番号
    Public Const CELLNAME_MAKER_KISYUNM As String = "Maker_KisyuNM"                                 '項目名:貸出品名
    Public Const CELLNAME_FUZOKUHIN As String = "Fuzokuhin"                                         '項目名:付属品
    Public Const CELLNAME_RENTALEDDT As String = "RentalEdDT"                                       '項目名:期限日
    Public Const CELLNAME_SHARE As String = "ShareUsr"                                              '項目名:複数人利用署名欄

    'インシデント登録（貸出誓約書出力）（HBKC0202）
    Public Const FORMAT_INCIDENT_KASHIDASHI As String = "インシデント登録_受領確認書フォーマット.xlsx"   'フォマットファイル名
    Public Const SHEETNAME_KASHIDASHI_PC As String = "agree_lend_pc"                                     'シート名:PC
    Public Const SHEETNAME_KASHIDASHI_TOKEN As String = "agree_lend_token"                               'シート名:USBトークン
    Public Const SHEETNAME_KASHIDASHI_OTHER As String = "agree_lend_other"                               'シート名:PC、USBトークン以外

    'インシデント登録（期限更新誓約書出力）（HBKC0203）
    Public Const FORMAT_INCIDENT_KIGEN As String = "インシデント登録_期限更新確認書フォーマット.xlsx"    'フォマットファイル名
    Public Const SHEETNAME_KIGEN_PC As String = "agree_extend_pc"                                        'シート名:PC
    Public Const SHEETNAME_KIGEN_OTHER As String = "agree_extend_other"                                  'シート名:Other

    'インシデント登録(預かり誓約書出力)(HBKC0204)
    Public Const FORMAT_INCIDENT_AZUKARI = "インシデント登録_一時預託確認書フォーマット.xlsx"            'フォーマット名
    Public Const SHEETNAME_AZUKARI = "agree_keep"                                                        'シート名

    'インシデント登録(返却確認書出力)(HBKC0205)
    Public Const FORMAT_INCIDENT_HENKYAKU = "インシデント登録_返却確認書フォーマット.xlsx"               'フォーマット名
    Public Const SHEETNAME_HENKYAKU = "agree_return"                                                     'シート名

    'インシデント登録(チェックリスト出力)(HBKC0206)
    Public Const FORMAT_INCIDENT_CHECK As String = "インシデント登録_サポセン機器チェックシートフォーマット.xlsx"   'フォマットファイル名
    Public Const SHEETNAME_CHECK_LEND_MOB As String = "check_lend_mob"                                              'シート名:（貸出チェックシート）MOB
    Public Const SHEETNAME_CHECK_LEND_PRESS As String = "check_lend_press"                                          'シート名:（貸出チェックシート）Press
    Public Const SHEETNAME_CHECK_LEND_TOKEN_NORMAL As String = "check_lend_token_normal"                            'シート名:（貸出チェックシート）USBトークン（通常）
    Public Const SHEETNAME_CHECK_LEND_TOKEN_SEND As String = "check_lend_token_send"                                'シート名:（貸出チェックシート）USBトークン（発送）
    Public Const SHEETNAME_CHECK_SET_PC As String = "check_set_pc"                                                  'シート名:（設置チェックシート）PC
    Public Const SHEETNAME_CHECK_SET_DIS As String = "check_set_dis"                                                'シート名:（設置チェックシート）ディスプレイ
    Public Const SHEETNAME_CHECK_REMOVE_PC As String = "check_remove_pc"                                            'シート名:（撤去チェックシート）PC
    Public Const SHEETNAME_CHECK_REMOVE_DIS As String = "check_remove_dis"                                          'シート名:（撤去チェックシート）ディスプレイ
    Public Const SHEETNAME_CHECK_RETURN_MOB As String = "check_return_mob"                                          'シート名:（返却チェックシート）MOB
    Public Const SHEETNAME_CHECK_RETURN_TOKEN As String = "check_return_token"                                      'シート名:（返却チェックシート）USBトークン
    Public Const SHEETNAME_CHECK_EXTEND As String = "check_extend"                                                  'シート名:（借用期間延長チェックシート）
    'セル名
    Public Const CELLNAM_CHECK_INCNMB = "IncNmb"                                                    'インシデント管理番号
    Public Const CELLNAM_CHECK_TITLE = "Title"                                                      'タイトル
    Public Const CELLNAM_CHECK_SPPERTNERID = "SpPartnerID"                                          '相手ID
    Public Const CELLNAM_CHECK_SPPERTNERNM = "SpPartnerNM"                                          '相手氏名
    Public Const CELLNAM_CHECK_SPPERTNERICOMPANY = "SpPartnerCompany"                               '相手会社名
    Public Const CELLNAM_CHECK_SPPERTNERBUSYONM = "SpPartnerBusyoNM"                                '相手部署
    Public Const CELLNAM_CHECK_SPPERTNERMAILADD = "SpPartnerMailAdd"                                '相手メールアドレス
    Public Const CELLNAM_CHECK_SPPERTNERCONTACT = "SpPartnerContact"                                '相手連絡先
    Public Const CELLNAM_CHECK_SPPERTNERROOM = "SpPartnerRoom"                                      '相手番組/部屋
    Public Const CELLNAM_CHECK_KIKIKIND_KIKINMB = "KindCD_KikiNmb"                                  '機器種別+機器番号
    Public Const CELLNAM_CHECK_MAKER = "SpClass2"                                                   'メーカー
    Public Const CELLNAM_CHECK_KISYU = "SpCINM"                                                     '機種
    Public Const CELLNAM_CHECK_SETKIKI = "SetKikiNo"                                                'セット機器
    Public Const CELLNAM_CHECK_OPTIONSOFT = "OptSoftNM"                                             'オプションソフト
    Public Const CELLNAM_CHECK_SPFIXEDIP = "SpFixedIP"                                              '固定IP
    Public Const CELLNAM_CHECK_SPSERIAL = "SpSerial"                                                '製造番号（シリアル）
    Public Const CELLNAM_CHECK_SPSETBUIL = "SpSetBuil"                                              '設置建物
    Public Const CELLNAM_CHECK_SPSETFLOOR = "SpSetFloor"                                            '設置フロア
    Public Const CELLNAM_CHECK_SPSETDESKNO = "SpSetDeskNo"                                          '設置デスクNo
    Public Const CELLNAM_CHECK_SPSETKYOKU = "SpSetKyokuNM"                                          '設置局
    Public Const CELLNAM_CHECK_SPSETBUSYO = "SpSetBusyoNM"                                          '設置部署
    Public Const CELLNAM_CHECK_SPSETROOM = "SpSetRoom"                                              '設置番組/部屋

    'インシデントメール作成(HBKC0209)
    Public Const INCIDENT_PERMUTATION_NORMAL As String = "0"
    Public Const INCIDENT_PERMUTATION_DATE As String = "1"
    Public Const INCIDENT_PERMUTATION_MULTILINE As String = "2"
    Public Const INCIDENT_PERMUTATION_MULTDATE As String = "3"
    'メール作成(インシデント登録画面置換え)
    '[ADD] 2015/08/24 y.naganuma [ログイン置換：ユーザー姓のみ]を追加 
    Public ReadOnly INCIDENT_PERMUTATION(,) As String = {{"[HBK共通：NOW：", INCIDENT_PERMUTATION_DATE}, {"[ログイン置換：グループ名]", INCIDENT_PERMUTATION_NORMAL}, {"[ログイン置換：ユーザーID]", INCIDENT_PERMUTATION_NORMAL}, {"[ログイン置換：ユーザー名]", INCIDENT_PERMUTATION_NORMAL}, _
                                                {"[INC置換：インシデント管理番号]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：受付手段]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：インシデント種別]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：ステータス]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：発生日時：", INCIDENT_PERMUTATION_DATE}, _
                                                {"[INC置換：回答日時：", INCIDENT_PERMUTATION_DATE}, {"[INC置換：完了日時：", INCIDENT_PERMUTATION_DATE}, {"[INC置換：重要度]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：障害レベル]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：タイトル]", INCIDENT_PERMUTATION_NORMAL}, _
                                                {"[INC置換：受付内容]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：対応結果]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：登録日時：", INCIDENT_PERMUTATION_DATE}, {"[INC置換：登録者業務チーム]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：登録者]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：最終更新日時：", INCIDENT_PERMUTATION_DATE}, _
                                                {"[INC置換：最終更新者業務チーム]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：最終更新者]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：対象システム]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：外部ツール番号]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：権限]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：借用物]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：イベントID]", INCIDENT_PERMUTATION_NORMAL}, _
                                                {"[INC置換：ソース]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：OPCイベントID]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：イベントクラス]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：担当グループ]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：担当者氏名]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：ドメイン]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：相手会社名]", INCIDENT_PERMUTATION_NORMAL}, _
                                                {"[INC置換：相手ID]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：相手氏名]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：相手シメイ]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：相手局]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：相手部署]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：相手電話番号]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：相手メールアドレス]", INCIDENT_PERMUTATION_NORMAL}, _
                                                {"[INC置換：相手連絡先]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：相手拠点]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：相手番組/部屋]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：指示書]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：機器情報]", INCIDENT_PERMUTATION_MULTILINE}, {"[INC置換：関連ファイル情報]", INCIDENT_PERMUTATION_MULTILINE}, _
                                                {"[INC置換：作業履歴：", INCIDENT_PERMUTATION_MULTDATE}, {"[INC置換：サポセン機器メンテナンス：", INCIDENT_PERMUTATION_MULTDATE}, {"[INC置換：会議情報：", INCIDENT_PERMUTATION_MULTDATE}, {"[INC置換：テキスト1]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：テキスト2]", INCIDENT_PERMUTATION_NORMAL}, _
                                                {"[INC置換：テキスト3]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：テキスト4]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：テキスト5]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：フラグ1]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：フラグ2]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：フラグ3]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：フラグ4]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：フラグ5]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：対応関係者情報]", INCIDENT_PERMUTATION_MULTILINE}, _
                                                {"[INC置換：グループ履歴]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：担当者履歴]", INCIDENT_PERMUTATION_NORMAL}, {"[INC置換：プロセスリンク情報]", INCIDENT_PERMUTATION_MULTILINE}, {"[INC置換：レンタル機器情報：", INCIDENT_PERMUTATION_MULTDATE}, {"[INC置換：部所有機器情報S：", INCIDENT_PERMUTATION_MULTDATE}, {"[INC置換：部所有機器情報L：", INCIDENT_PERMUTATION_MULTDATE}, _
                                                {"[INC置換：部所有機器情報一覧：", INCIDENT_PERMUTATION_MULTDATE}, {"[INC置換：機器情報S]", INCIDENT_PERMUTATION_MULTILINE}, {"[INC置換：レンタル機器情報L：", INCIDENT_PERMUTATION_MULTDATE}, {"[INC置換：担当者氏]", INCIDENT_PERMUTATION_NORMAL}, {"[ログイン置換：ユーザー姓のみ]", RELEASE_PERMUTATION_NORMAL}}

    'インシデント登録（インシデント情報（単票）出力）（HBKC0207）
    Public Const FORMAT_INCIDENT_TANPYO As String = "インシデント単票フォーマット.xlsx"    'フォマットファイル名
    Public Const SHEETNAME_INCTANPYO As String = "agree_inctanpyo"                        'シート名
    Public Const CELLNAME_AI_INCNMB As String = "ai_IncNmb"                         'セル名（ヘッダ）:インシデント
    Public Const CELLNAME_AI_TITLE As String = "ai_Title"                           'セル名（ヘッダ）:タイトル
    Public Const CELLNAME_AI_HASSEIDT As String = "ai_HasseiDT"                     'セル名（ヘッダ）:発生日時
    Public Const CELLNAME_AI_KANRYODT As String = "ai_KanryoDT"                     'セル名（ヘッダ）:完了日時
    Public Const CELLNAME_AI_UKEKBN As String = "ai_UkeKbn"                         'セル名（ヘッダ）:受付区分
    Public Const CELLNAME_AI_SYSTEM As String = "ai_System"                         'セル名（ヘッダ）:対象システム
    Public Const CELLNAME_AI_INCKBN As String = "ai_IncKbn"                         'セル名（ヘッダ）:インシデント種別
    Public Const CELLNAME_AI_OUTSIDETOOLNMB As String = "ai_OutSideToolNmb"         'セル名（ヘッダ）:外部ツール番号
    Public Const CELLNAME_AI_PROCESSSTATE As String = "ai_ProcessState"             'セル名（ヘッダ）:プロセスステータス
    Public Const CELLNAME_AI_PARTNERID As String = "ai_PartnerID"                   'セル名（相手情報）:相手ID
    Public Const CELLNAME_AI_PARTNERNM As String = "ai_PartnerNM"                   'セル名（相手情報）:相手氏名
    Public Const CELLNAME_AI_PARTNERKANA As String = "ai_PartnerKana"               'セル名（相手情報）:相手氏名（シメイ）
    Public Const CELLNAME_AI_PARTNERCOMPANY As String = "ai_PartnerCompany"         'セル名（相手情報）:相手会社
    Public Const CELLNAME_AI_PARTNERKYOKUNM As String = "ai_PartnerKyokuNM"         'セル名（相手情報）:相手局
    Public Const CELLNAME_AI_PARTNERBUSYONM As String = "ai_PartnerBusyoNM"         'セル名（相手情報）:相手部署
    Public Const CELLNAME_AI_PARTNERTEL As String = "ai_PartnerTel"                 'セル名（相手情報）:相手電話番号
    Public Const CELLNAME_AI_PARTNERMAILADD As String = "ai_PartnerMailAdd"         'セル名（相手情報）:相手メールアドレス
    Public Const CELLNAME_AI_PARTNERCONTACT As String = "ai_PartnerContact"         'セル名（相手情報）:相手連絡先
    Public Const CELLNAME_AI_PARTNERBASE As String = "ai_PartnerBase"               'セル名（相手情報）:相手拠点
    Public Const CELLNAME_AI_PARTNERROOM As String = "ai_PartnerRoom"               'セル名（相手情報）:相手番組/部屋
    Public Const CELLNAME_AI_KENGEN As String = "ai_Kengen"                         'セル名（相手情報）:権限
    Public Const CELLNAME_AI_RENTALKIKI As String = "ai_RentalKiki"                 'セル名（相手情報）:借用物
    Public Const CELLNAME_AI_UKENAIYO As String = "ai_UkeNaiyo"                     'セル名（相手情報の下）:受付内容
    Public Const CELLNAME_AI_TAIOKEKKA As String = "ai_TaioKekka"                   'セル名（相手情報の下）:対応結果
    Public Const CELLNAME_AI_TANTOGRP As String = "ai_TantoGrp"                     'セル名（担当情報）:担当グループ
    Public Const CELLNAME_AI_INCTANTOID As String = "ai_IncTantoID"                 'セル名（担当情報）:担当ID
    Public Const CELLNAME_AI_INCTANTONM As String = "ai_IncTantoNM"                 'セル名（担当情報）:担当者氏名
    Public Const CELLNAME_AI_KIKIKBN As String = "ai_KikiKbn"                       'セル名（機器情報）:種別
    Public Const CELLNAME_AI_KIKINUM As String = "ai_KikiNum"                       'セル名（機器情報）:番号
    Public Const CELLNAME_AI_KIKIINF As String = "ai_KikiInf"                       'セル名（機器情報）:機器情報
    Public Const CELLNAME_AI_RELATIONKBN As String = "ai_RelationKbn"               'セル名（対応関係者情報）:区分
    Public Const CELLNAME_AI_RELATIONID As String = "ai_RelationID"                 'セル名（対応関係者情報）:ID
    Public Const CELLNAME_AI_RELATIONGRPNM As String = "ai_RelationGrpNM"           'セル名（対応関係者情報）:グループ名
    Public Const CELLNAME_AI_RELATIONUSRNM As String = "ai_RelationUsrNM"           'セル名（対応関係者情報）:ユーザー名
    Public Const CELLNAME_AI_GROUPRIREKI As String = "ai_GroupRireki"               'セル名（担当履歴情報）:グループ履歴
    Public Const CELLNAME_AI_TANTORIREKI As String = "ai_TantoRireki"               'セル名（担当履歴情報）:担当者履歴
    Public Const CELLNAME_AI_LINKNMB As String = "ai_LinkNmb"                       'セル名（プロセスリンク情報）:番号
    Public Const CELLNAME_AI_SPWORK As String = "ai_SpWork"                         'セル名（サポセン機器）:作業
    Public Const CELLNAME_AI_SPCHGNMB As String = "ai_SpChgNmb"                     'セル名（サポセン機器）:交換
    Public Const CELLNAME_AI_SPKIND As String = "ai_SpKind"                         'セル名（サポセン機器）:種別
    Public Const CELLNAME_AI_SPNMB As String = "ai_SpNmb"                           'セル名（サポセン機器）:番号
    Public Const CELLNAME_AI_SPCLASS2 As String = "ai_SpClass2"                     'セル名（サポセン機器）:分類2(メーカー)
    Public Const CELLNAME_AI_SPCINM As String = "ai_SpCINM"                         'セル名（サポセン機器）:名称(機種)
    Public Const CELLNAME_AI_SPWORKBIKO As String = "ai_SpWorkBiko"                 'セル名（サポセン機器）:作業備考
    Public Const CELLNAME_AI_SPWORKSCEDT As String = "ai_SpWorkSceDT"               'セル名（サポセン機器）:作業予定日
    Public Const CELLNAME_AI_SPWORKCOMPDT As String = "ai_SpWorkCompDT"             'セル名（サポセン機器）:作業完了日
    Public Const CELLNAME_AI_SPCOMP As String = "ai_SpComp"                         'セル名（サポセン機器）:完了
    Public Const CELLNAME_AI_SPCANCEL As String = "ai_SpCancel"                     'セル名（サポセン機器）:取消
    Public Const CELLNAME_AI_MEETINGNMB As String = "ai_MeetingNmb"                 'セル名（会議情報）:番号
    Public Const CELLNAME_AI_MEETINGTITLE As String = "ai_MeetingTitle"             'セル名（会議情報）:タイトル
    Public Const CELLNAME_AI_MEETINGRESULTKBN As String = "ai_MeetingResultKbn"     'セル名（会議情報）:承認
    Public Const CELLNAME_AI_FREEBIKO1 As String = "ai_FreeBIko1"                   'セル名（フリー入力情報）:フリーワード1
    Public Const CELLNAME_AI_FREEBIKO2 As String = "ai_FreeBIko2"                   'セル名（フリー入力情報）:フリーワード2
    Public Const CELLNAME_AI_FREEBIKO3 As String = "ai_FreeBIko3"                   'セル名（フリー入力情報）:フリーワード3
    Public Const CELLNAME_AI_FREEBIKO4 As String = "ai_FreeBIko4"                   'セル名（フリー入力情報）:フリーワード4
    Public Const CELLNAME_AI_FREEBIKO5 As String = "ai_FreeBIko5"                   'セル名（フリー入力情報）:フリーワード5
    Public Const CELLNAME_AI_FREEFLG1 As String = "ai_FreeFlg1"                     'セル名（フリー入力情報）:フリーフラグ1
    Public Const CELLNAME_AI_FREEFLG2 As String = "ai_FreeFlg2"                     'セル名（フリー入力情報）:フリーフラグ2
    Public Const CELLNAME_AI_FREEFLG3 As String = "ai_FreeFlg3"                     'セル名（フリー入力情報）:フリーフラグ3
    Public Const CELLNAME_AI_FREEFLG4 As String = "ai_FreeFlg4"                     'セル名（フリー入力情報）:フリーフラグ4
    Public Const CELLNAME_AI_FREEFLG5 As String = "ai_FreeFlg5"                     'セル名（フリー入力情報）:フリーフラグ5
    Public Const CELLNAME_AI_WORKKEIKAKBN As String = "ai_WorkKeikaKbn"             'セル名（作業履歴報）:経過種別
    Public Const CELLNAME_AI_WORKSCEDT As String = "ai_WorkSceDT"                   'セル名（作業履歴報）:作業予定日時
    Public Const CELLNAME_AI_WORKSTDT As String = "ai_WorkStDT"                     'セル名（作業履歴報）:作業開始日時
    Public Const CELLNAME_AI_WORKEDDT As String = "ai_WorkEdDT"                     'セル名（作業履歴報）:作業終了日時
    Public Const CELLNAME_AI_WORKSYSTEM As String = "ai_WorkSystem"                 'セル名（作業履歴報）:対象システム
    Public Const CELLNAME_AI_WORKTANTONM As String = "ai_WorkTantoNM"               'セル名（作業履歴報）:作業担当者
    Public Const CELLNAME_AI_WORKNAIYO As String = "ai_WorkNaiyo"                   'セル名（作業履歴報）:作業内容
    'フリーフラグ
    Public Const FREEFLG_FLG_ON As String = "1"
    Public Const FREEFLG_FLG_OFF As String = "0"
    Public Const FREEFLG_FLG_ON_NM As String = "ON"
    Public Const FREEFLG_FLG_OFF_NM As String = "OFF"
    Public Const SAMPSEN_SUMI As String = "済"

    '最新連携情報表示画面(HBKC0210)
    '連携区分
    Public Const RENKEIKBN_HBKTOSM As String = "1"          'ひびき⇒SM
    Public Const RENKEIKBN_SMTOHBK As String = "2"          'SM⇒ひびき
    '連携区分名
    Public Const RENKEIKBN_HBKTOSM_NAME As String = "ひびき⇒SM"
    Public Const RENKEIKBN_SMTOHBK_NAME As String = "SM⇒ひびき"

    '連携処理実施(HBKC0211)
    Public Const C0211_E001 As String = "連携処理中です。"
    Public Const C0211_E002 As String = "SEQを新規に採番できませんでした。"
    Public Const C0211_I001 As String = "ServiceManagerへのインシデント情報連携を指示しました。"
    '連携状況フラグ
    Public Const RENKEIFLG_WAIT As String = "0"             '連携待ち

    '会議検索一覧画面(HBKC0301)
    Public Const SELECT_MODE_MENU As String = "1"
    Public Const SELECT_MODE_NOTMENU As String = "0"

    Public Const C0301_E001 As String = "検索結果から1行選択してください。"
    Public Const C0301_W001 As String = "検索結果の件数が{0}件を超えているため、表示に時間がかかる場合があります。" & vbCrLf & "検索結果の表示を行いますか？"
    Public Const C0301_I001 As String = "該当する結果はありません。"

    '会議記録登録画面(HBKC0401)
    Public Const SELECT_RESULTKBN_NO As String = "0"
    Public Const SELECT_RESULTKBN_OK As String = "1"
    Public Const SELECT_RESULTKBN_NG As String = "2"
    Public Const SELECT_RESULTKBNNM_NO As String = ""
    Public Const SELECT_RESULTKBNNM_OK As String = "承認"
    Public Const SELECT_RESULTKBNNM_NG As String = "却下"
    Public CMB_RESULTKBN_STR As String() = New String() {SELECT_RESULTKBNNM_NO, SELECT_RESULTKBNNM_OK, SELECT_RESULTKBNNM_NG}
    Public CMB_RESULTKBN_VAL As String() = New String() {SELECT_RESULTKBN_NO, SELECT_RESULTKBN_OK, SELECT_RESULTKBN_NG}

    Public Const C0401_E001 As String = "{0}のデータが取得できませんでした。"
    Public Const C0401_E002 As String = "{0}の{1}を入力してください。"
    Public Const C0401_E003 As String = "{0}の範囲が正しくありません。"
    Public Const C0401_E004 As String = "会議番号を新規に採番できませんでした。"
    Public Const C0401_E005 As String = "ログNoを新規に採番できませんでした。"
    Public Const C0401_E006 As String = "ファイルが見つかりません。"
    Public Const C0401_E007 As String = "検索結果から1行選択してください。"
    Public Const C0401_I001 As String = "登録が完了しました。"

    'エラーメッセージ定数
    Public Const COL_YOTEIDT As String = "実施予定日時"
    Public Const COL_JISIDT As String = "実施日時"
    Public Const COL_DATE As String = "日付"
    Public Const COL_TIME As String = "時刻"

    'ノウハウUrl選択画面(HBKC0501)
    Public Const C0501_E001 As String = "ノウハウURLの説明を選択してください。"
    Public Const C0501_E002 As String = "指定されたUrlが正しくありません。"


    '一括登録画面(HBKC0601)
    Public Const C0601_E001 As String = "登録するファイルを選択してください。"
    Public Const C0601_E002 As String = "取込ファイルがCsvファイルではありません。"
    Public Const C0601_E003 As String = "取込ファイルパスのファイルがありません。"
    Public Const C0601_E004 As String = "取込ファイルの値に誤りがあります。エラーの内容は以下のファイルを確認してください。" & vbCrLf & "{0}"
    Public Const C0601_E005 As String = "{0}：「{1}」を入力してください。"
    Public Const C0601_E006 As String = "{0}：「{1}」の桁数が不正です。"
    Public Const C0601_E007 As String = "{0}：「{1}」が正しくありません。"
    Public Const C0601_E008 As String = "{0}：「{1}」はマスターに存在しません。"
    Public Const C0601_E009 As String = "インシデント番号を新規に採番できませんでした。"
    Public Const C0601_E010 As String = "{0}：「{1}」はテーブルに存在しません。"
    Public Const C0601_E011 As String = "{0}：「作業終了日時」には「作業完了日時」より後の日時を入力してください。"
    Public Const C0601_I001 As String = "登録が完了しました。"
    Public ReadOnly COLUMNNAME_INC() As String = New String() {"No", "受付手段", "インシデント種別", "ステータス", "発生日時", "回答日時", "完了日時", "重要度", "障害レベル", _
                                    "タイトル", "受付内容", "対応結果", "対象システム", "外部ツール番号", "イベントID", "ソース", "OPCイベントID", "イベントクラス", _
                                    "担当者業務チーム", "担当者ID", "インシデント担当者", "ドメイン", "相手会社名", "相手ID", "相手氏名", "相手シメイ", "相手局", "相手部署", _
                                    "相手電話番号", "相手メールアドレス", "相手連絡先", "相手拠点", "相手番組/部屋", "指示書", "機器種別", "機器番号", "経過種別", _
                                    "対象システム(作業内容)", "作業予定日時", "作業開始日時", "作業終了日時", "作業内容", "作業担当者業務チーム1", "作業担当者ID1", "作業担当者1", _
                                    "作業担当者業務チーム2", "作業担当者ID2", "作業担当者2", "作業担当者業務チーム3", "作業担当者ID3", "作業担当者3", _
                                    "作業担当者業務チーム4", "作業担当者ID4", "作業担当者4", "作業担当者業務チーム5", "作業担当者ID5", "作業担当者5"}

    'セット選択画面（HBKC0701）
    Public Const C0701_E001 As String = "検索結果を選択してください。"
    Public Const C0701_E002 As String = "複数行選択することはできません。"
    Public Const C0701_E003 As String = "該当する結果はありません。"
    Public Const C0701_W001 As String = "検索結果の件数が{0}件を超えているため、表示に時間がかかる場合があります。" & vbCrLf & "検索結果の表示を行いますか？"


    '受付手段(メール自動発報)
    Public Const MAIL_AUTO_ALARM As String = "018"
    '指示書フラグ
    Public Const SHIJISYO_FLG_ON As String = "1"
    Public Const SHIJISYO_FLG_OFF As String = "0"
    Public Const SHIJISYO_FLG_ON_NM As String = "有"
    Public Const SHIJISYO_FLG_OFF_NM As String = "無"

    'デフォルト選択フラグ（プロセスステータス）
    Public Const DEFAULTSELECT_FLG_ON As String = "1"
    Public Const DEFAULTSELECT_FLG_OFF As String = "0"

End Module

