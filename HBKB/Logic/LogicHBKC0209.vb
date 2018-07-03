Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' インシデント登録（メール作成）ロジッククラス
''' </summary>
''' <remarks>インシデント登録（メール作成）のロジックを定義したクラス
''' <para>作成情報：2012/07/27 y.ikushima
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKC0209

    'インスタンス作成
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK
    Private sqlHBKC0209 As New SqlHBKC0209
    'インシデント登録
    Private logicHBKC0201 As LogicHBKC0201

    'Public定数宣言

    '指示書
    Public Const SHIJISYO_ON_NM As String = "指示書あり"            '指示書あり
    Public Const SHIJISYO_OFF_NM As String = "指示書なし"            '指示書なし

    '機器情報用文字列
    Public Const KIKI_SBTNM As String = "機器："
    Public Const KIKI_INFONM As String = "機器情報："
    '機器情報一覧列番号
    Public Const COL_KIKI_SBT As Integer = logicHBKC0201.COL_KIKI_SBT                    '種別名
    Public Const COL_KIKI_NMB As Integer = logicHBKC0201.COL_KIKI_NMB                    '番号
    Public Const COL_KIKI_INFO As Integer = logicHBKC0201.COL_KIKI_INFO                   '機器情報
    Public Const COL_KIKI_SBTCD As Integer = logicHBKC0201.COL_KIKI_SBTCD                  '種別CD
    Public Const COL_KIKI_CINMB As Integer = logicHBKC0201.COL_KIKI_CINMB                   '隠し：CI番号

    '対応関係者情報用文字列
    Public Const RELATION_USERNM As String = "ユーザ："
    Public Const RELATION_GROUPNM As String = "グループ："
    '対応関係者情報一覧列番号
    Public Const COL_RELATION_KBN As Integer = logicHBKC0201.COL_RELATION_KBN                '区分
    Public Const COL_RELATION_ID As Integer = logicHBKC0201.COL_RELATION_ID                 'ID
    Public Const COL_RELATION_GROUPNM As Integer = logicHBKC0201.COL_RELATION_GROUPNM            'グループ名
    Public Const COL_RELATION_USERNM As Integer = logicHBKC0201.COL_RELATION_USERNM             'ユーザー名

    'プロセスリンク一覧列番号
    Public Const COL_processLINK_KBN_NMR As Integer = logicHBKC0201.COL_processLINK_KBN_NMR         '区分
    Public Const COL_processLINK_NO As Integer = logicHBKC0201.COL_processLINK_NO              '番号

    '関連ファイル一覧列番号
    Public Const COL_FILE_NAIYO As Integer = 0                  '説明

    '作業履歴用文字列
    Public Const RIREKI_SYUBETSUNM As String = "経過種別："
    Public Const RIREKI_SYSTEMNM As String = "対象システム："
    Public Const RIREKI_YOTEIDATENM As String = "作業予定日時："
    Public Const RIREKI_STARTDATENM As String = "作業開始日時："
    Public Const RIREKI_ENDDATENM As String = "作業終了日時："
    Public Const RIREKI_WORK_TANTONM As String = "作業担当者："
    Public Const RIREKI_WORK_NAIYONM As String = "作業内容："
    '作業履歴一覧列番号
    Public Const COL_RIREKI_KEIKA As Integer = logicHBKC0201.COL_RIREKI_KEIKA                '経過種別
    Public Const COL_RIREKI_NAIYOU As Integer = logicHBKC0201.COL_RIREKI_NAIYOU               '作業内容
    Public Const COL_RIREKI_YOTEIBI As Integer = logicHBKC0201.COL_RIREKI_YOTEIBI              '作業予定日
    Public Const COL_RIREKI_YOTEIJI As Integer = logicHBKC0201.COL_RIREKI_YOTEIJI              '作業予定時
    Public Const COL_RIREKI_KAISHIBI As Integer = logicHBKC0201.COL_RIREKI_KAISHIBI             '作業開始日
    Public Const COL_RIREKI_KAISHIJI As Integer = logicHBKC0201.COL_RIREKI_KAISHIJI             '作業開始時
    Public Const COL_RIREKI_SYURYOBI As Integer = logicHBKC0201.COL_RIREKI_SYURYOBI             '作業終了日
    Public Const COL_RIREKI_SYURYOJI As Integer = logicHBKC0201.COL_RIREKI_SYURYOJI             '作業終了時
    Public Const COL_RIREKI_SYSTEM As Integer = logicHBKC0201.COL_RIREKI_SYSTEM               '対象システム
    Public Const COL_RIREKI_TANTOGP1 As Integer = logicHBKC0201.COL_RIREKI_TANTOGP1            '担当グループ１名
    Public Const COL_RIREKI_TANTOID1 As Integer = logicHBKC0201.COL_RIREKI_TANTOID1            '担当ID１名
    Public Const COL_RIREKI_TANTO_COLCNT As Integer = logicHBKC0201.COL_RIREKI_TANTO_COLCNT         '1担当分カラム数（スプレッドループに使用）

    'サポセン機器メンテナス文字列
    Public Const SAP_WORKNM_NAME As String = "作業："
    Public Const SAP_CHGNMB_NAME As String = "機器："
    Public Const SAP_CLASS2_NAME As String = "メーカー："
    Public Const SAP_CINM_NAME As String = "機種："
    Public Const SAP_WORKBIKO_NAME As String = "作業備考："
    Public Const SAP_WORKSCEDT_NAME As String = "作業予定日："
    Public Const SAP_WORKCOMPDT_NAME As String = "作業完了日："
    'サポセン機器メンテナス一覧列番号
    Public Const COL_SAP_WORKNM As Integer = logicHBKC0201.COL_SAP_WORKNM                  '作業
    Public Const COL_SAP_CHGNMB As Integer = logicHBKC0201.COL_SAP_CHGNMB                  '交換
    Public Const COL_SAP_KINDNM As Integer = logicHBKC0201.COL_SAP_KINDNM                  '種別
    Public Const COL_SAP_NUM As Integer = logicHBKC0201.COL_SAP_NUM                     '番号
    Public Const COL_SAP_CLASS2 As Integer = logicHBKC0201.COL_SAP_CLASS2                  '分類２（メーカー）
    Public Const COL_SAP_CINM As Integer = logicHBKC0201.COL_SAP_CINM                    '名称（機種）
    Public Const COL_SAP_WORKBIKO As Integer = logicHBKC0201.COL_SAP_WORKBIKO                '作業備考
    Public Const COL_SAP_WORKSCEDT As Integer = logicHBKC0201.COL_SAP_WORKSCEDT               '作業予定日
    Public Const COL_SAP_WORKCOMPDT As Integer = logicHBKC0201.COL_SAP_WORKCOMPDT             '作業完了日
    Public Const COL_SAP_COMPFLG As Integer = logicHBKC0201.COL_SAP_COMPFLG                '完了チェックボックス
    Public Const COL_SAP_CANCELFLG As Integer = logicHBKC0201.COL_SAP_CANCELFLG              '取消チェックボックス

    '会議情報文字列
    Public Const MEETING_NONM As String = "番号："
    Public Const MEETING_JIBINM As String = "実施日："
    Public Const MEETING_TITLENM As String = "タイトル："
    Public Const MEETING_NINNM As String = "承認："
    '会議情報一覧列番号
    Public Const COL_MEETING_NO As Integer = logicHBKC0201.COL_MEETING_NO                  '番号
    Public Const COL_MEETING_JIBI As Integer = logicHBKC0201.COL_MEETING_JIBI                '実施日
    Public Const COL_MEETING_TITLE As Integer = logicHBKC0201.COL_MEETING_TITLE               'タイトル
    Public Const COL_MEETING_NIN As Integer = logicHBKC0201.COL_MEETING_NIN                 '承認

    'レンタル機器情報用文字列
    '[mod] 2013/06/19 y.ikushima 複数人利用文言修正 START
    'Public Const FUKUSU_MSG As String = "★複数人利用のため連絡票による延長申請必要機器"
    Public Const FUKUSU_MSG As String = "★複数人利用機器"
    '[mod] 2013/06/19 y.ikushima 複数人利用文言修正 END
    Public Const KASHIDASHIHN_NM As String = "・貸出品："
    Public Const FUZOKUHIN_NM As String = "・付属品："
    Public Const KASHIDASHIBI_NM As String = "・貸出日："
    Public Const RENTARU_KIGEN As String = "・レンタル期限："
    Public Const FUKUSU_RIYOH As String = "・複数利用者："
    Public Const SYOZOKU_BUSYO As String = "＜機器貸出時の所属部署＞"
    Public Const COL_CLASS2 As Integer = 0                '分類２
    Public Const COL_CINM As Integer = 1                '名称
    Public Const COL_FUZOKUHIN As Integer = 2               '付属品
    Public Const COL_RENTARU_STARDT As Integer = 3              'レンタル開始日
    Public Const COL_RENTARU_ENDDT As Integer = 4              'レンタル期限日
    Public Const COL_SETTING_BUSYO As Integer = 5             '設置部署
    Public Const COL_USER_NM As Integer = 6             'ユーザ氏名

    '部所有機器情報
    Public Const TANMATSU_INFO As String = "★端末情報"
    Public Const HOST_NM As String = "HOST名："
    Public Const ALIAS_NM As String = "エイリアス："
    Public Const DNS_NM As String = "DNS登録："
    Public Const IPWARIATE_NM As String = "IP割当種類："
    Public Const IPADDRESS_NM As String = "IPアドレス："
    Public Const ZOO_NM As String = "zoo参加："
    Public Const MAKER_NM As String = "メーカー名："
    Public Const KISYU_NM As String = "機種名："
    Public Const KISYUKIND_NM As String = "機種種別："
    Public Const OS_NM As String = "OS："
    Public Const NIC1_NM As String = "NIC1："
    Public Const MACADDRESS1_NM As String = "MACアドレス1："
    Public Const NIC2_NM As String = "NIC2："
    Public Const MACADDRESS2_NM As String = "MACアドレス2："
    Public Const KANRENKIKI_NM As String = "関連機器："
    Public Const ANTIVIRUSSOFT_NM As String = "ウイルス対策ソフト："
    Public Const ANTIVIRUSDT_NM As String = "ウイルス対策確認日："
    Public Const SETTING_INFO As String = "●設置場所情報"
    Public Const BUILD_NM As String = "建物："
    Public Const BUILDNAME_NM As String = "建物名："
    Public Const FLOOR_NM As String = "フロア："
    Public Const ROOM_NM As String = "部屋："
    Public Const ROOMNAME_NM As String = "部屋名："
    Public Const KANRI_INFO As String = "●管理者情報"
    Public Const SYAINNUM_NM As String = "社員番号："
    Public Const SHIMEI_NM As String = "氏名："
    Public Const ANOTHER_INFO As String = "●その他情報"
    Public Const CONNECTDT_NM As String = "接続日："
    Public Const EXPIRATIONDT_NM As String = "有効期限："
    Public Const CONNECTREASON_NM As String = "接続理由："
    Public Const BIKO_NM As String = "備考："

    Public Const COL_ALIAU As Integer = 0                'エイリアス
    Public Const COL_DNS As Integer = 1                'DNS
    Public Const COL_IPUSE As Integer = 2               'IPUSER
    Public Const COL_FIXEDIP As Integer = 3              '固定IP
    Public Const COL_ZOOKBN As Integer = 4              'Zoo有無
    Public Const COL_BUY_CLASS2 As Integer = 5                '分類２
    Public Const COL_BUY_CINM As Integer = 6                '名称
    Public Const COL_BUY_CLASS1 As Integer = 7                '分類1
    Public Const COL_SOFTNM_OS As Integer = 8                'OS
    Public Const COL_NIC1 As Integer = 9               'NIC1
    Public Const COL_MACADDRESS1 As Integer = 10              'MACアドレス1
    Public Const COL_NIC2 As Integer = 11              'NIC2
    Public Const COL_MACADDRESS2 As Integer = 12               'MACアドレス2
    Public Const COL_SOFTNM_VIRUS As Integer = 13             'ウイルス対策ソフト
    Public Const COL_ANTIVIRUSSOFCHECKDT As Integer = 14                'ウイルス対策確認日
    Public Const COL_SETBUIL As Integer = 15                '設置建物
    Public Const COL_SETFLOOR As Integer = 16               '設置フロア
    Public Const COL_SET_ROOM As Integer = 17              '設置部屋
    Public Const COL_USRID As Integer = 18              'ユーザID
    Public Const COL_USRNM As Integer = 19             'ユーザシメイ
    Public Const COL_CONNECTDT As Integer = 20             '接続日
    Public Const COL_EXPIRATIONDT As Integer = 21                '有効日
    Public Const COL_CONECTREASON As Integer = 22                '接続理由
    Public Const COL_BUSYOKIKIBIKO As Integer = 23               '部所有機器備考

    '機器情報
    Public Const KIKI_NM As String = "機器："
    Public Const KIKIINFO_NM As String = "機器情報："

    '部所有機器情報一覧
    Public Const COL_PAR_KINDNUM As Integer = 0         '種別＋番号
    Public Const COL_PAR_CONNECTDT As Integer = 1       '接続日
    Public Const COL_PAR_EXPIRATIONDT As Integer = 2        '有効日
    Public Const COL_PAR_CLASS2 As Integer = 3                '分類２
    Public Const COL_PAR_CINM As Integer = 4                '名称

    '置換用インデックス
    Public Const NOW As Integer = 0                     'NOW：変換日付
    Public Const GROUPNM As Integer = 1                 'グループ名：変換なし
    Public Const USERID As Integer = 2                  'ユーザーID：変換なし
    Public Const USERNM As Integer = 3                  'ユーザー名：変換なし
    Public Const INCIDENT_NMB As Integer = 4            'インシデント管理番号：変換なし
    Public Const UKETSUKE_STEP As Integer = 5           '受付手段：変換なし
    Public Const INCIDENT_KIND As Integer = 6           'インシデント種別：変換なし
    Public Const STATUS As Integer = 7                  'ステータス：変換なし
    Public Const HASSEI_DT As Integer = 8               '発生日時：変換日付
    Public Const KAITO_DT As Integer = 9                '回答日時：変換日付
    Public Const KANRYO_DT As Integer = 10              '完了日時：変換日付
    Public Const PRIORITY As Integer = 11               '重要度：変換なし
    Public Const ERRLEVEL As Integer = 12               '障害レベル：変換なし
    Public Const TITLE As Integer = 13                  'タイトル：変換なし
    Public Const UKETSUKE_NAIYO As Integer = 14         '受付手段：変換なし
    Public Const TAIOH_KEKKA As Integer = 15            '対応結果：変換なし
    Public Const REG_DT As Integer = 16                 '登録日時：変換日付
    Public Const REG_TEAM As Integer = 17               '登録者業務チーム：変換なし
    Public Const REG_USER As Integer = 18               '登録者：変換なし
    Public Const LASTREG_DT As Integer = 19             '最終更新日時：変換日付
    Public Const LASTREG_TEAM As Integer = 20           '最終更新者業務チーム：変換なし
    Public Const LASTREG_USER As Integer = 21           '最終更新者：変換なし
    Public Const SYSTEM_NMB As Integer = 22             '対象システム：3層出力
    Public Const OUTSIDETOOL_NMB As Integer = 23        '外部ツール番号：変換なし
    Public Const KENGEN As Integer = 24                 '権限：変換（データ）
    Public Const RENTALKIKI As Integer = 25             '借用物：変換なし
    Public Const EVENT_ID As Integer = 26               'イベントID：変換なし
    Public Const SOURCE As Integer = 27                 'ソース：変換なし
    Public Const OPC_EVENT_ID As Integer = 28           'OPCイベントID：変換なし
    Public Const EVENT_CLASS As Integer = 29            'イベントクラス：変換なし
    Public Const TANTO_GROUP As Integer = 30            '担当グループ：変換なし
    Public Const TANTO_SHIMEI As Integer = 31           '担当者氏名：変換なし
    Public Const DOMAIN As Integer = 32                 'ドメイン：変換なし
    Public Const PARTNER_COM As Integer = 33            '相手会社名：変換なし
    Public Const PARTNER_ID As Integer = 34             '相手ID：変換なし
    Public Const PARTNER_SHIMEI_NM As Integer = 35      '相手氏名：変換なし
    Public Const PARTNER_SHIMEI_KANA As Integer = 36    '相手シメイ：変換なし
    Public Const PARTNER_KYOKU As Integer = 37          '相手局：変換なし
    Public Const PARTNER_BUSYO As Integer = 38          '相手部署：変換なし
    Public Const PARTNER_TEL As Integer = 39            '相手電話番号：変換なし
    Public Const PARTNER_ADDRESS As Integer = 40         '相手メールアドレス：変換なし
    Public Const PARTNER_CONTACT As Integer = 41        '相手連絡先：変換なし
    Public Const PARTNER_KYOTEN As Integer = 42         '相手拠点：変換なし
    Public Const PARTNER_ROOM As Integer = 43           '相手番組/部屋：変換なし
    Public Const SHIJISYO As Integer = 44               '指示書：フォーマット変換
    Public Const KIKI_INFO As Integer = 45              '機器情報：N行変換
    Public Const KANRENFILE_INFO As Integer = 46              '関連ファイル情報：N行変換
    Public Const WORK_RIREKI As Integer = 47            '作業履歴：N行変換（日付）
    Public Const SAP_KIKI As Integer = 48               'サポセン機器メンテナンス：N行変換（日付）
    Public Const KAIGI_INFO As Integer = 49             '会議情報：N行変換（日付）
    Public Const TEXT_1 As Integer = 50                 'テキスト1：変換なし
    Public Const TEXT_2 As Integer = 51                 'テキスト2：変換なし
    Public Const TEXT_3 As Integer = 52                 'テキスト3：変換なし
    Public Const TEXT_4 As Integer = 53                 'テキスト4：変換なし
    Public Const TEXT_5 As Integer = 54                 'テキスト5：変換なし
    Public Const FLG_1 As Integer = 55                  'フラグ1：変換なし
    Public Const FLG_2 As Integer = 56                  'フラグ2：変換なし
    Public Const FLG_3 As Integer = 57                  'フラグ3：変換なし
    Public Const FLG_4 As Integer = 58                  'フラグ4：変換なし
    Public Const FLG_5 As Integer = 59                  'フラグ5：変換なし
    Public Const TAIOH_KANKEI As Integer = 60           '対応関係者情報：N行変換
    Public Const GROUP_RIREKI As Integer = 61           'グループ履歴：変換なし
    Public Const TANTOH_RIREKI As Integer = 62          '担当者履歴：変換なし
    Public Const PROCESSLINK_INFO As Integer = 63       'プロセスリンク情報：N行変換
    Public Const RENTAL_KIKI As Integer = 64            'レンタル機器情報：N行変換（日付）
    Public Const BUY_KIKI_S As Integer = 65             '部所有機器情報S：N行変換（日付）
    Public Const BUY_KIKI_L As Integer = 66             '部所有機器情報L：N行変換（日付）
    Public Const BUY_KIKI_ICHIRAN As Integer = 67       '部所有機器情報一覧：N行変換
    Public Const KIKI_INFO_S As Integer = 68            '機器情報S：N行変換
    Public Const RENTAL_KIKI_L As Integer = 69          'レンタル機器情報L：N行変換（日付）
    Public Const TANTOUSRSHI As Integer = 70            '担当者氏
    '[ADD] 2015/08/24 y.naganuma メールテンプレート条件追加対応 START
    Public Const COMMON_USERNMSEI As Integer = 71       '（共通）ユーザー名(姓)：変換なし
    '[ADD] 2015/08/24 y.naganuma メールテンプレート条件追加対応 END


    ''' <summary>
    ''' インシデント登録（メール作成）本文作成メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0209">[IN/OUT]インシデント登録（メール作成）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデント登録（メール作成）本文作成メイン処理を行う
    ''' <para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateIncidentMailMain(ByRef dataHBKC0209 As DataHBKC0209) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'インシデント登録（メール作成）本文作成処理
        If CreateIncidentMail(dataHBKC0209) = False Then
            Return False
        End If

        'メールソフト(outlook起動)処理
        If StartUpForMail(dataHBKC0209) = False Then
            Return False
        End If

        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' インシデント登録（メール作成）本文作成処理
    ''' </summary>
    ''' <param name="dataHBKC0209">[IN/OUT]インシデント登録（メール作成）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデント登録（メール作成）本文作成処理を行う
    ''' <para>作成情報：2012/07/27 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateIncidentMail(ByRef dataHBKC0209 As DataHBKC0209) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'メール本文作成処理

            Dim strCheck As String(,) = INCIDENT_PERMUTATION        '登録画面置換え

            With dataHBKC0209

                'メールフォーマット選択画面から受け取ったDataTableからメールフォーマット設定
                .PropStrMailto = .PropDtReturnData.Rows(0).Item("MailTo")
                .PropStrMailFrom = .PropDtReturnData.Rows(0).Item("MailFrom")
                .PropStrMailCc = .PropDtReturnData.Rows(0).Item("CC")
                .PropStrMailBcc = .PropDtReturnData.Rows(0).Item("Bcc")
                .PropIntMailPriority = Integer.Parse(.PropDtReturnData.Rows(0).Item("PriorityKbn"))
                .PropStrMailSubject = .PropDtReturnData.Rows(0).Item("Title")
                .PropStrMailText = .PropDtReturnData.Rows(0).Item("MailText")

                '宛先設定
                If CreateWritingsPermutation(dataHBKC0209, .PropStrMailto, strCheck) = False Then
                    Return False
                End If

                'CC設定
                If CreateWritingsPermutation(dataHBKC0209, .PropStrMailCc, strCheck) = False Then
                    Return False
                End If

                'Bcc設定
                If CreateWritingsPermutation(dataHBKC0209, .PropStrMailBcc, strCheck) = False Then
                    Return False
                End If

                'タイトル設定
                If CreateWritingsPermutation(dataHBKC0209, .PropStrMailSubject, strCheck) = False Then
                    Return False
                End If

                '本文設定
                If CreateWritingsPermutation(dataHBKC0209, .PropStrMailText, strCheck) = False Then
                    Return False
                End If

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True
        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
        End Try
    End Function

    ''' <summary>
    ''' メールソフト(outlook起動)処理
    ''' </summary>
    ''' <param name="dataHBKC0209">[IN/OUT]インシデント登録（メール作成）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作成されたメール本文、タイトルを用いてメールソフトを起動する
    ''' <para>作成情報：2012/07/30 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function StartUpForMail(ByRef dataHBKC0209 As DataHBKC0209) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0209

                Dim otlApp As Object = Nothing       'Applicationオブジェクト
                Dim otlMail As Object = Nothing 'メールのオブジェクト

                'outlook 起動
                otlApp = CreateObject("Outlook.Application")

                'メールアイテムの作成
                otlMail = otlApp.CreateItem(0)
                otlMail.SentOnBehalfOfName = .PropStrMailFrom           '差出人設定
                otlMail.To = .PropStrMailto                             '宛先設定
                otlMail.CC = .PropStrMailCc                             'Cc設定
                otlMail.BCC = .PropStrMailBcc                           'Bcc設定
                otlMail.Subject = .PropStrMailSubject                   'タイトル設定
                otlMail.Body = .PropStrMailText                         '本文設定
                otlMail.Importance = .PropIntMailPriority               '重要度設定

                otlMail.Display()                                       '画面に表示

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True
        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
        End Try
    End Function

    ''' <summary>
    ''' 日付変換処理処理
    ''' </summary>
    ''' <param name="strDateDT">[IN]日付</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="strFormat">[IN]フォーマット</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>２つの日付を繋げ、日付型に変換できる場合は"yyyy/MM/dd(ddd) HH:mm"の文字列に、変換できなければ空文字を返す
    ''' <para>作成情報：2012/07/30 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetConvertDate(ByVal strDateDT As String, ByRef strConvert As String, ByVal strFormat As String) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtConvertForInput As DateTime           '変換用日付型変数

        Try

            '日付と時間を連結
            strConvert = strDateDT

            If DateTime.TryParse(strConvert, dtConvertForInput) = False Then
                strConvert = strConvert
            Else
                strConvert = dtConvertForInput.ToString(strFormat)
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True
        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
        End Try
    End Function

    ''' <summary>
    ''' 文字置換処理
    ''' </summary>
    ''' <param name="dataHBKC0209">[IN/OUT]インシデント登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrCheck">[IN]置き換え一覧</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>受け取ったテンプレートをメール用に変換する
    ''' <para>作成情報：2012/08/07 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateWritingsPermutation(ByRef dataHBKC0209 As DataHBKC0209, ByRef StrConvert As String, ByVal StrCheck As String(,)) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '存在チェック用変数
        Dim intStartIndex As Integer = 0
        Dim intEndIndex As Integer = 0
        Dim strRetrunFormat As String = ""
        Dim strReturnPermutation As String = ""
        Dim intCount As Integer = 0

        Try

            '置き換え一覧ループ
            For i As Integer = 0 To (StrCheck.Length / StrCheck.Rank) - 1 Step 1
                '存在チェック
                intStartIndex = StrConvert.IndexOf(StrCheck(i, 0))
                intEndIndex = StrConvert.LastIndexOf(StrCheck(i, 0))
                intCount = 0

                '文字列に置換文字が存在する場合
                If intStartIndex <> -1 Or intEndIndex <> -1 Then

                    If StrCheck(i, 1) = INCIDENT_PERMUTATION_NORMAL Then
                        '置換
                        If SetPermutation_Normal(dataHBKC0209, StrConvert, StrCheck(i, 0), i) = False Then
                            Return False
                        End If
                    ElseIf StrCheck(i, 1) = INCIDENT_PERMUTATION_DATE Then
                        While (True)
                            If intCount <> 0 Then
                                '存在チェック
                                intStartIndex = StrConvert.IndexOf(StrCheck(i, 0), intStartIndex + 1)
                                If intStartIndex = -1 Then
                                    Exit While
                                End If
                            End If
                            '日付変換後、置換
                            If GetIndex_Format(dataHBKC0209, StrConvert, StrCheck(i, 0), intStartIndex, strRetrunFormat, strReturnPermutation) = False Then
                                Return False
                            End If
                            '置換
                            If SetPermutation_Date(dataHBKC0209, StrConvert, strReturnPermutation, i, strRetrunFormat) = False Then
                                Return False
                            End If
                            intCount = intCount + 1
                        End While
                    ElseIf StrCheck(i, 1) = INCIDENT_PERMUTATION_MULTILINE Then
                        '置換
                        If SetPermutation_Multiline(dataHBKC0209, StrConvert, StrCheck(i, 0), i) = False Then
                            Return False
                        End If
                    ElseIf StrCheck(i, 1) = INCIDENT_PERMUTATION_MULTDATE Then
                        While (True)
                            If intCount <> 0 Then
                                '存在チェック
                                intStartIndex = StrConvert.IndexOf(StrCheck(i, 0), intStartIndex + 1)
                                If intStartIndex = -1 Then
                                    Exit While
                                End If
                            End If
                            '複数行変換（日付）後、置換
                            If GetIndex_Format(dataHBKC0209, StrConvert, StrCheck(i, 0), intStartIndex, strRetrunFormat, strReturnPermutation) = False Then
                                Return False
                            End If
                            '置換処理
                            If SetPermutation_MultilineDate(dataHBKC0209, StrConvert, strReturnPermutation, i, strRetrunFormat) = False Then
                                Return False
                            End If
                            intCount = intCount + 1
                        End While
                    End If
                End If
            Next

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True
        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
        End Try

    End Function

    ''' <summary>
    ''' 日付フォーマット取得処理
    ''' </summary>
    ''' <param name="dataHBKC0209">[IN/OUT]インシデント登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrCheck">[IN]チェック文字列</param>
    ''' <param name="intStringIndex">[IN]置き換え開始インデックス</param>
    ''' <param name="StrRetrunFormat">[IN/OUT]日付型フォーマット</param>
    ''' <param name="StrReturnPermutation">[IN/OUT]置換用文字列</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>受け取った本文、置換用文字列から日付型のフォーマットを取得し、フォーマットと置換用文字列を返す
    ''' <para>作成情報：2012/08/07 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetIndex_Format(ByRef dataHBKC0209 As DataHBKC0209, ByRef StrConvert As String, ByVal StrCheck As String, _
            ByVal intStringIndex As Integer, ByRef StrRetrunFormat As String, ByRef StrReturnPermutation As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        '変数宣言
        Dim intStartIndex As Integer = intStringIndex
        Dim intLastIndex As Integer = 0
        Dim intCount As Integer = 0
        Dim blnIndex As Boolean = False

        Try
            '取得インデックスから本文をループ
            For i As Integer = intStartIndex + StrCheck.Length To StrConvert.Length - 1 Step 1
                '区切り文字の場合は、インデックスを保存して、ループを抜ける
                If StrConvert(i) = END_CHAR Then
                    intLastIndex = i
                    blnIndex = True
                    Exit For
                End If
                intCount = intCount + 1
            Next

            '取得したインデックスから文字列を取得
            If blnIndex = True Then
                StrRetrunFormat = StrConvert.Substring(intStringIndex + StrCheck.Length, intCount)
                StrReturnPermutation = StrConvert.Substring(intStringIndex, intLastIndex - intStringIndex + 1)
            Else

            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True
        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
        End Try
    End Function

    ''' <summary>
    ''' 置換処理
    ''' </summary>
    ''' <param name="dataHBKC0209">[IN/OUT]インシデント登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrCheck">[IN]置き換え文字</param>
    ''' <param name="IntCount">[IN]カウンタ</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>置換処理を行う
    ''' <para>作成情報：2012/08/07 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetPermutation_Normal(ByRef dataHBKC0209 As DataHBKC0209, ByRef StrConvert As String, _
                                          ByVal StrCheck As String, ByVal IntCount As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strPermutation As String = ""
        Dim strTemp As String = ""

        Try
            With dataHBKC0209
                If IntCount = GROUPNM Then
                    'グループ名
                    strPermutation = PropWorkGroupCD
                ElseIf IntCount = USERID Then
                    'ユーザーID
                    strPermutation = PropUserId
                ElseIf IntCount = USERNM Then
                    'ユーザー名
                    strPermutation = PropUserName
                ElseIf IntCount = INCIDENT_NMB Then
                    'インシデント管理番号
                    strPermutation = .PropStrIncCD
                ElseIf IntCount = UKETSUKE_STEP Then
                    '受付手段
                    strPermutation = .PropStrUkeKbn
                ElseIf IntCount = INCIDENT_KIND Then
                    'インシデント種別
                    strPermutation = .PropStrIncKbnCD
                ElseIf IntCount = STATUS Then
                    'ステータス
                    strPermutation = .PropStrProcessStateCD
                ElseIf IntCount = PRIORITY Then
                    '重要度
                    strPermutation = .PropStrPriority
                ElseIf IntCount = ERRLEVEL Then
                    '障害レベル
                    strPermutation = .PropStrErrlevel
                ElseIf IntCount = TITLE Then
                    'タイトル
                    strPermutation = .PropStrTitle
                ElseIf IntCount = UKETSUKE_NAIYO Then
                    '受付内容
                    strPermutation = .PropStrUkeNaiyo
                ElseIf IntCount = TAIOH_KEKKA Then
                    '対応結果
                    strPermutation = .PropStrTaioKekka
                ElseIf IntCount = REG_TEAM Then
                    '登録者業務チーム
                    strPermutation = .PropStrRegGrpNM
                ElseIf IntCount = REG_USER Then
                    '登録者
                    strPermutation = .PropStrRegNM
                ElseIf IntCount = LASTREG_TEAM Then
                    '最終更新者業務チーム
                    strPermutation = .PropStrUpdateGrpNM
                ElseIf IntCount = LASTREG_USER Then
                    '最終更新者
                    strPermutation = .PropStrUpdateNM
                ElseIf IntCount = SYSTEM_NMB Then
                    '対象システム
                    If GetCIInfoSystem(.PropStrSystemNmb, strPermutation) = False Then
                        Return False
                    End If
                ElseIf IntCount = OUTSIDETOOL_NMB Then
                    '外部ツール番号
                    strPermutation = .PropStrOutSideToolNmb
                ElseIf IntCount = KENGEN Then
                    '権限
                    strPermutation = .PropStrKengen
                ElseIf IntCount = RENTALKIKI Then
                    '借用物
                    strPermutation = .PropStrRentalKiki
                ElseIf IntCount = EVENT_ID Then
                    'イベントID
                    strPermutation = .PropStrEventID
                ElseIf IntCount = SOURCE Then
                    'ソース
                    strPermutation = .PropStrSource
                ElseIf IntCount = OPC_EVENT_ID Then
                    'OPCイベントID
                    strPermutation = .PropStrOPCEventID
                ElseIf IntCount = EVENT_CLASS Then
                    'イベントクラス
                    strPermutation = .PropStrEventClass
                ElseIf IntCount = TANTO_GROUP Then
                    '担当グループ
                    strPermutation = .PropStrTantoGrpCD
                ElseIf IntCount = TANTO_SHIMEI Then
                    '担当者氏名
                    strPermutation = .PropStrIncTantoNM
                ElseIf IntCount = DOMAIN Then
                    'ドメイン
                    strPermutation = .PropStrDomainCD
                ElseIf IntCount = PARTNER_COM Then
                    '相手会社名
                    strPermutation = .PropStrPartnerCompany
                ElseIf IntCount = PARTNER_ID Then
                    '相手ID
                    strPermutation = .PropStrPartnerID
                ElseIf IntCount = PARTNER_SHIMEI_NM Then
                    '相手氏名
                    strPermutation = .PropStrPartnerNM
                ElseIf IntCount = PARTNER_SHIMEI_KANA Then
                    '相手シメイ
                    strPermutation = .PropStrPartnerKana
                ElseIf IntCount = PARTNER_KYOKU Then
                    '相手局
                    strPermutation = .PropStrPartnerKyokuNM
                ElseIf IntCount = PARTNER_BUSYO Then
                    '相手部署
                    strPermutation = .PropStrPartnerBusyoNM
                ElseIf IntCount = PARTNER_TEL Then
                    '相手電話番号
                    strPermutation = .PropStrPartnerTel
                ElseIf IntCount = PARTNER_ADDRESS Then
                    '相手メールアドレス
                    strPermutation = .PropStrPartnerMailAdd
                ElseIf IntCount = PARTNER_CONTACT Then
                    '相手連絡先
                    strPermutation = .PropStrPartnerContact
                ElseIf IntCount = PARTNER_KYOTEN Then
                    '相手拠点
                    strPermutation = .PropStrPartnerBase
                ElseIf IntCount = PARTNER_ROOM Then
                    '相手番組/部屋
                    strPermutation = .PropStrPartnerRoom
                ElseIf IntCount = SHIJISYO Then
                    '指示書
                    If .PropStrShijisyoFlg = SHIJISYO_FLG_ON Then
                        strPermutation = SHIJISYO_ON_NM
                    Else
                        strPermutation = SHIJISYO_OFF_NM
                    End If
                ElseIf IntCount = TEXT_1 Then
                    'テキスト1
                    strPermutation = .PropStrBIko1
                ElseIf IntCount = TEXT_2 Then
                    'テキスト2
                    strPermutation = .PropStrBIko2
                ElseIf IntCount = TEXT_3 Then
                    'テキスト3
                    strPermutation = .PropStrBIko3
                ElseIf IntCount = TEXT_4 Then
                    'テキスト4
                    strPermutation = .PropStrBIko4
                ElseIf IntCount = TEXT_5 Then
                    'テキスト5
                    strPermutation = .PropStrBIko5
                ElseIf IntCount = FLG_1 Then
                    'フラグ1
                    strPermutation = .PropStrFreeFlg1
                ElseIf IntCount = FLG_2 Then
                    'フラグ2
                    strPermutation = .PropStrFreeFlg2
                ElseIf IntCount = FLG_3 Then
                    'フラグ3
                    strPermutation = .PropStrFreeFlg3
                ElseIf IntCount = FLG_4 Then
                    'フラグ4
                    strPermutation = .PropStrFreeFlg4
                ElseIf IntCount = FLG_5 Then
                    'フラグ5
                    strPermutation = .PropStrFreeFlg5
                ElseIf IntCount = GROUP_RIREKI Then
                    'グループ履歴
                    strPermutation = .PropStrGrpHistory
                ElseIf IntCount = TANTOH_RIREKI Then
                    '担当者履歴
                    strPermutation = .PropStrTantoHistory
                ElseIf IntCount = TANTOUSRSHI Then

                    '前後の空白を削除した氏名
                    strTemp = Trim(.PropStrIncTantoNM)
                    strPermutation = strTemp
                    If strTemp.IndexOf(" ") > 0 Then
                        '担当者氏
                        strPermutation = strTemp.Substring(0, strTemp.IndexOf(" "))
                    ElseIf strTemp.IndexOf("　") > 0 Then
                        '担当者氏
                        strPermutation = strTemp.Substring(0, strTemp.IndexOf("　"))
                    End If

                    '[ADD] 2015/08/24 y.naganuma メールテンプレート条件追加対応 START
                ElseIf IntCount = COMMON_USERNMSEI Then
                    '前後の空白を削除した氏名
                    strTemp = Trim(PropUserName)
                    strPermutation = strTemp
                    If strTemp.IndexOf(" ") > 0 Then
                        'ユーザー名(姓)
                        strPermutation = strTemp.Substring(0, strTemp.IndexOf(" "))
                    ElseIf strTemp.IndexOf("　") > 0 Then
                        'ユーザー名(姓)
                        strPermutation = strTemp.Substring(0, strTemp.IndexOf("　"))
                    End If
                    '[ADD] 2015/08/24 y.naganuma メールテンプレート条件追加対応 END
                End If
            End With

            '置換処理
            If SetPermutation(StrConvert, StrCheck, strPermutation) = False Then
                Return False
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True
        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
        End Try
    End Function

    ''' <summary>
    ''' 置換処理_日付
    ''' </summary>
    ''' <param name="dataHBKC0209">[IN/OUT]インシデント登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrCheck">[IN]置き換え一覧</param>
    ''' <param name="IntCount">[IN]カウンタ</param>
    ''' <param name="StrFormatForDate">[IN]日付フォーマット</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>受け取ったテンプレートをメール用に変換する（日付）
    ''' <para>作成情報：2012/08/07 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetPermutation_Date(ByRef dataHBKC0209 As DataHBKC0209, ByRef StrConvert As String, _
                                          ByVal StrCheck As String, ByVal IntCount As Integer, ByVal StrFormatForDate As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strPermutation As String = ""
        Try
            With dataHBKC0209
                If IntCount = NOW Then
                    'NOW
                    strPermutation = System.DateTime.Now.ToString(StrFormatForDate)
                ElseIf IntCount = HASSEI_DT Then
                    '発生日時
                    If SetConvertDate(.PropStrHasseiDT & " " & .PropStrHasseiDT_HM, strPermutation, StrFormatForDate) = False Then
                        Return False
                    End If
                ElseIf IntCount = KAITO_DT Then
                    '回答日時
                    If SetConvertDate(.PropStrKaitoDT & " " & .PropStrKaitoDT_HM, strPermutation, StrFormatForDate) = False Then
                        Return False
                    End If
                ElseIf IntCount = KANRYO_DT Then
                    '完了日時
                    If SetConvertDate(.PropStrKanryoDT & " " & .PropStrKanryoDT_HM, strPermutation, StrFormatForDate) = False Then
                        Return False
                    End If
                ElseIf IntCount = REG_DT Then
                    '登録日時
                    If SetConvertDate(.PropStrRegDT, strPermutation, StrFormatForDate) = False Then
                        Return False
                    End If
                ElseIf IntCount = LASTREG_DT Then
                    '最終登録日時
                    If SetConvertDate(.PropStrUpdateDT, strPermutation, StrFormatForDate) = False Then
                        Return False
                    End If
                End If

            End With

            '置換処理
            If SetPermutation(StrConvert, StrCheck, strPermutation) = False Then
                Return False
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True
        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
        End Try
    End Function

    ''' <summary>
    ''' 置換処理_複数行
    ''' </summary>
    ''' <param name="dataHBKC0209">[IN/OUT]インシデント登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrCheck">[IN]置き換え一覧</param>
    ''' <param name="IntCount">[IN]カウンタ</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>受け取ったテンプレートをメール用に変換する（複数行）
    ''' <para>作成情報：2012/08/07 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetPermutation_Multiline(ByRef dataHBKC0209 As DataHBKC0209, ByRef StrConvert As String, _
                                          ByVal StrCheck As String, ByVal IntCount As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter
        Dim BlnGetFlg As Boolean = False                'CI番号取得フラグ

        '変数宣言
        Dim strPermutation As String = ""
        Try
            'コネクションを開く
            Cn.Open()

            With dataHBKC0209
                If IntCount = KIKI_INFO Then
                    '機器情報
                    If SetConvertKikiInfo(dataHBKC0209, strPermutation) = False Then
                        Return False
                    End If
                ElseIf IntCount = KANRENFILE_INFO Then
                    '関連ファイル情報
                    If SetConvertFile(dataHBKC0209, strPermutation) = False Then
                        Return False
                    End If
                ElseIf IntCount = TAIOH_KANKEI Then
                    '対応関係者情報
                    If SetConvertRelation(dataHBKC0209, strPermutation) = False Then
                        Return False
                    End If
                ElseIf IntCount = PROCESSLINK_INFO Then
                    'プロセスリンク情報
                    If SetConvertProcessLink(dataHBKC0209, strPermutation) = False Then
                        Return False
                    End If
                ElseIf IntCount = KIKI_INFO_S Then
                    '機器情報S
                    'CI番号取得
                    If GetCInmb(Adapter, Cn, dataHBKC0209, BlnGetFlg) = False Then
                        Return False
                    End If
                    'CI番号が取得できた場合のみデータ取得
                    If BlnGetFlg = False Then
                        If SetConvertKikiInfoS(Adapter, Cn, dataHBKC0209, strPermutation) = False Then
                            Return False
                        End If
                    Else
                        strPermutation = ""
                    End If
                End If
            End With

            '置換処理
            If SetPermutation(StrConvert, StrCheck, strPermutation) = False Then
                Return False
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True
        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            'リソースの開放
            Adapter.Dispose()
            Cn.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 置換処理_複数行変換（日付）
    ''' </summary>
    ''' <param name="dataHBKC0209">[IN/OUT]インシデント登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrCheck">[IN]置き換え一覧</param>
    ''' <param name="IntCount">[IN]カウンタ</param>
    ''' <param name="StrFormatForDate">[IN]日付フォーマット</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>受け取ったテンプレートをメール用に変換する（複数行変換（日付））
    ''' <para>作成情報：2012/08/07 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetPermutation_MultilineDate(ByRef dataHBKC0209 As DataHBKC0209, ByRef StrConvert As String, _
                                          ByVal StrCheck As String, ByVal IntCount As Integer, ByVal StrFormatForDate As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter
        Dim BlnGetFlg As Boolean = False                'CI番号取得フラグ

        '変数宣言
        Dim strPermutation As String = ""
        Try
            'コネクションを開く
            Cn.Open()

            With dataHBKC0209
                If IntCount = WORK_RIREKI Then
                    '作業履歴
                    If SetConvertRireki(dataHBKC0209, strPermutation, StrFormatForDate) = False Then
                        Return False
                    End If
                ElseIf IntCount = SAP_KIKI Then
                    'サポセン機器メンテナンス
                    If SetConvertSapMainte(dataHBKC0209, strPermutation, StrFormatForDate) = False Then
                        Return False
                    End If
                ElseIf IntCount = KAIGI_INFO Then
                    '会議情報
                    If SetConvertMeeting(dataHBKC0209, strPermutation, StrFormatForDate) = False Then
                        Return False
                    End If
                ElseIf IntCount = RENTAL_KIKI Then
                    'レンタル機器情報
                    'CI番号取得
                    If GetCInmb(Adapter, Cn, dataHBKC0209, BlnGetFlg) = False Then
                        Return False
                    End If
                    'CI番号が取得できた場合のみデータ取得
                    If BlnGetFlg = False Then
                        If SetConvertRetalS(Adapter, Cn, dataHBKC0209, strPermutation, StrFormatForDate) = False Then
                            Return False
                        End If
                    Else
                        strPermutation = ""
                    End If
                ElseIf IntCount = BUY_KIKI_S Then
                    '部所有機器情報S
                    'CI番号取得
                    If GetCInmb(Adapter, Cn, dataHBKC0209, BlnGetFlg) = False Then
                        Return False
                    End If
                    'CI番号が取得できた場合のみデータ取得
                    If BlnGetFlg = False Then
                        If SetConvertBuyS(Adapter, Cn, dataHBKC0209, strPermutation, StrFormatForDate) = False Then
                            Return False
                        End If
                    Else
                        strPermutation = ""
                    End If
                ElseIf IntCount = BUY_KIKI_L Then
                    '部所有機器情報L
                    'CI番号取得
                    If GetCInmb(Adapter, Cn, dataHBKC0209, BlnGetFlg) = False Then
                        Return False
                    End If
                    'CI番号が取得できた場合のみデータ取得
                    If BlnGetFlg = False Then
                        If SetConvertBuyL(Adapter, Cn, dataHBKC0209, strPermutation, StrFormatForDate) = False Then
                            Return False
                        End If
                    Else
                        strPermutation = ""
                    End If
                ElseIf IntCount = BUY_KIKI_ICHIRAN Then
                    '部所有機器情報一覧
                    If dataHBKC0209.PropStrPartnerID <> "" Then
                        If SetConvertPartner(Adapter, Cn, dataHBKC0209, strPermutation, StrFormatForDate) = False Then
                            Return False
                        End If
                    Else
                        strPermutation = ""
                    End If
                ElseIf IntCount = RENTAL_KIKI_L Then
                    'レンタル機器情報L
                    'CI番号取得
                    If GetCInmb(Adapter, Cn, dataHBKC0209, BlnGetFlg) = False Then
                        Return False
                    End If
                    'CI番号が取得できた場合のみデータ取得
                    If BlnGetFlg = False Then
                        If SetConvertRetalL(Adapter, Cn, dataHBKC0209, strPermutation, StrFormatForDate) = False Then
                            Return False
                        End If
                    Else
                        strPermutation = ""
                    End If
                End If

            End With

            '置換処理
            If SetPermutation(StrConvert, StrCheck, strPermutation) = False Then
                Return False
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True
        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            'リソースの開放
            Adapter.Dispose()
            Cn.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 置換処理
    ''' </summary>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrFormat">[IN]置き換えフォーマット</param>
    ''' <param name="StrInput">[IN]置き換え文字</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>置換処理を行う
    ''' <para>作成情報：2012/08/07 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetPermutation(ByRef StrConvert As String, ByVal StrFormat As String, ByVal StrInput As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '置換
            StrConvert = StrConvert.Replace(StrFormat, StrInput)

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True
        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
        End Try
    End Function

    ''' <summary>
    ''' CI情報取得
    ''' </summary>
    ''' <param name="StrSystemNmb">[IN]対象システム番号</param>
    ''' <param name="StrPermutation">[IN/OUT]置き換え文字</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>置換処理を行う
    ''' <para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetCIInfoSystem(ByVal StrSystemNmb As String, ByRef StrPermutation As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter
        Dim intSysNmb As Integer
        Dim dtResult As New DataTable

        Try

            '数値変換できる場合のみ、取得
            If Integer.TryParse(StrSystemNmb, intSysNmb) = True Then
                'コネクションを開く
                Cn.Open()
                '分類１＋分類２＋名称を取得
                If sqlHBKC0209.SelectCIInfoSql(Adapter, Cn, intSysNmb) = False Then
                    Return False
                End If
                'ログ出力
                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI情報取得", Nothing, Adapter.SelectCommand)
                'データを取得
                Adapter.Fill(dtResult)

                If dtResult.Rows.Count > 0 Then
                    StrPermutation = dtResult.Rows(0).Item(0).ToString
                Else
                    StrPermutation = ""
                End If

            Else
                StrPermutation = ""
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True
        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            'リソースの開放
            Adapter.Dispose()
            Cn.Dispose()
            dtResult.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' CI番号取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0209">[IN/OUT]インシデント登録（メール作成）Dataクラス</param>
    ''' <param name="BlnGetFlg">[IN/OUT]CI番号取得フラグ</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI番号の取得処理を行う
    ''' <para>作成情報：2012/08/07 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetCInmb(ByVal Adapter As NpgsqlDataAdapter, _
                                                            ByVal Cn As NpgsqlConnection, _
                                                            ByRef dataHBKC0209 As DataHBKC0209, _
                                                            ByRef BlnGetFlg As Boolean) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable
        Dim strKindNum As String = ""          '種別CD+番号


        Try
            With dataHBKC0209.PropVwkikiInfo.Sheets(0)

                '0件の場合空文字を返す
                If .Rows.Count = 0 Then
                    BlnGetFlg = True
                    '終了ログ出力
                    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                    '正常処理終了
                    Return True
                End If

                '行数分ループを行い、検索用種別＋番号をセットする
                For i As Integer = 0 To .Rows.Count - 1 Step 1
                    '種別
                    If strKindNum = "" Then
                        strKindNum &= "'" & .GetText(i, COL_KIKI_SBTCD) & .GetText(i, COL_KIKI_NMB) & "'"
                    Else
                        strKindNum &= "," & "'" & .GetText(i, COL_KIKI_SBTCD) & .GetText(i, COL_KIKI_NMB) & "'"
                    End If
                Next

            End With

            'CI番号取得
            If sqlHBKC0209.SelectCINmbSql(Adapter, Cn, strKindNum) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI番号取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtResult)

            'データクラスに保存
            dataHBKC0209.PropDtCINmb = dtResult

            'データを取得できなかった場合フラグを立てる
            If dataHBKC0209.PropDtCINmb.Rows.Count = 0 Then
                BlnGetFlg = True
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True
        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
            dtResult.Dispose()
        End Try
    End Function

    '複数行変換処理-----------------------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' 機器情報データ加工処理
    ''' </summary>
    ''' <param name="dataHBKC0209">[IN/OUT]インシデント登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>機器情報データをメール用に変換する
    ''' <para>作成情報：2012/07/30 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetConvertKikiInfo(ByRef dataHBKC0209 As DataHBKC0209, ByRef StrConvert As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0209.PropVwkikiInfo.Sheets(0)

                '0件の場合空文字を返す
                If .Rows.Count = 0 Then
                    StrConvert = ""
                    '終了ログ出力
                    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                    '正常処理終了
                    Return True
                End If

                '行数分ループを行い、種別＋番号＋機器情報をセットする
                For i As Integer = 0 To .Rows.Count - 1 Step 1
                    StrConvert &= MAILPARTITION & vbCrLf
                    StrConvert &= KIKI_SBTNM & .GetText(i, COL_KIKI_SBT) & .GetText(i, COL_KIKI_NMB) & vbCrLf
                    StrConvert &= KIKI_INFONM & vbCrLf & .GetText(i, COL_KIKI_INFO) & vbCrLf
                Next

                StrConvert &= MAILPARTITION

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True
        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
        End Try
    End Function

    ''' <summary>
    ''' 関連ファイル情報データ加工処理
    ''' </summary>
    ''' <param name="dataHBKC0209">[IN/OUT]インシデント登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関連ファイル情報データをメール用に変換する
    ''' <para>作成情報：2012/08/08 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetConvertFile(ByRef dataHBKC0209 As DataHBKC0209, ByRef StrConvert As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0209.PropVwFileInfo.Sheets(0)

                '0件の場合空文字を返す
                If .Rows.Count = 0 Then
                    StrConvert = ""
                    '終了ログ出力
                    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                    '正常処理終了
                    Return True
                End If

                '行数分ループを行い、関連ファイル情報をセットする
                For i As Integer = 0 To .Rows.Count - 1 Step 1
                    StrConvert &= MAILPARTITION & vbCrLf
                    StrConvert &= .GetText(i, COL_FILE_NAIYO) & vbCrLf
                Next
                StrConvert &= MAILPARTITION

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True
        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
        End Try
    End Function

    ''' <summary>
    ''' 対応関係情報データ加工処理
    ''' </summary>
    ''' <param name="dataHBKC0209">[IN/OUT]インシデント登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対応関係情報データをメール用に変換する
    ''' <para>作成情報：2012/08/08 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetConvertRelation(ByRef dataHBKC0209 As DataHBKC0209, ByRef StrConvert As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0209.PropVwRelation.Sheets(0)

                '0件の場合空文字を返す
                If .Rows.Count = 0 Then
                    StrConvert = ""
                    '終了ログ出力
                    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                    '正常処理終了
                    Return True
                End If

                '行数分ループを行い、関係者区分・グループ名・ユーザID＋ユーザ名をセットする
                For i As Integer = 0 To .Rows.Count - 1 Step 1
                    StrConvert &= MAILPARTITION & vbCrLf
                    If .GetText(i, COL_RELATION_KBN) = KBN_GROUP Then
                        StrConvert &= RELATION_GROUPNM & .GetText(i, COL_RELATION_GROUPNM) & vbCrLf
                    Else
                        StrConvert &= RELATION_USERNM & .GetText(i, COL_RELATION_ID) & " " & .GetText(i, COL_RELATION_USERNM) & vbCrLf
                    End If
                Next

                StrConvert &= MAILPARTITION

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True
        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
        End Try
    End Function

    ''' <summary>
    ''' プロセスリンク情報データ加工処理
    ''' </summary>
    ''' <param name="dataHBKC0209">[IN/OUT]インシデント登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンク情報データをメール用に変換する
    ''' <para>作成情報：2012/08/08 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetConvertProcessLink(ByRef dataHBKC0209 As DataHBKC0209, ByRef StrConvert As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0209.PropVwprocessLinkInfo.Sheets(0)

                '0件の場合空文字を返す
                If .Rows.Count = 0 Then
                    StrConvert = ""
                    '終了ログ出力
                    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                    '正常処理終了
                    Return True
                End If

                '行数分ループを行い、行カンマ区切りのプロセスリンク情報をセットする
                For i As Integer = 0 To .Rows.Count - 1 Step 1

                    If StrConvert = "" Then
                        StrConvert &= .GetText(i, COL_processLINK_KBN_NMR) & " " & .GetText(i, COL_processLINK_NO)
                    Else
                        StrConvert &= " , " & .GetText(i, COL_processLINK_KBN_NMR) & " " & .GetText(i, COL_processLINK_NO)
                    End If

                Next
            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True
        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
        End Try
    End Function

    '複数行変換処理（日付アリ）-----------------------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' 作業履歴データ加工処理
    ''' </summary>
    ''' <param name="dataHBKC0209">[IN/OUT]インシデント登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrFormatForDate">[IN]日付フォーマット</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴データをメール用に変換する
    ''' <para>作成情報：2012/07/30 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetConvertRireki(ByRef dataHBKC0209 As DataHBKC0209, ByRef StrConvert As String, ByVal StrFormatForDate As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strYoteiDate As String = ""                 '作業予定日変換用文字列
        Dim strStartDate As String = ""                 '作業開始日変換用文字列
        Dim strEndDate As String = ""                   '作業終了日変換用文字列


        Try
            With dataHBKC0209.PropVwIncRireki.Sheets(0)

                '0件の場合空文字を返す
                If .Rows.Count = 0 Then
                    StrConvert = ""
                    '終了ログ出力
                    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                    '正常処理終了
                    Return True
                End If

                '行数分ループを行い、作業履歴情報をセットする
                For i As Integer = 0 To .Rows.Count - 1 Step 1

                    '作業予定日、作業予定時をセットし、日付型に変換
                    If SetConvertDate(.GetText(i, COL_RIREKI_YOTEIBI), strYoteiDate, StrFormatForDate) = False Then
                        Return False
                    End If

                    '作業開始日、作業開始時をセットし、日付型に変換
                    If SetConvertDate(.GetText(i, COL_RIREKI_KAISHIBI), strStartDate, StrFormatForDate) = False Then
                        Return False
                    End If

                    '作業終了日、作業終了時をセットし、日付型に変換
                    If SetConvertDate(.GetText(i, COL_RIREKI_SYURYOBI), strEndDate, StrFormatForDate) = False Then
                        Return False
                    End If

                    StrConvert &= MAILPARTITION & vbCrLf
                    StrConvert &= RIREKI_SYUBETSUNM & .GetText(i, COL_RIREKI_KEIKA) & vbCrLf
                    StrConvert &= RIREKI_SYSTEMNM & .GetText(i, COL_RIREKI_SYSTEM) & vbCrLf
                    StrConvert &= RIREKI_YOTEIDATENM & strYoteiDate & vbCrLf
                    StrConvert &= RIREKI_STARTDATENM & strStartDate & vbCrLf
                    StrConvert &= RIREKI_ENDDATENM & strEndDate & vbCrLf
                    StrConvert &= RIREKI_WORK_TANTONM
                    For j As Integer = 0 To 49  '列50固定
                        If j = 0 Then
                            StrConvert &= .GetText(i, COL_RIREKI_TANTOGP1 + (j * COL_RIREKI_TANTO_COLCNT)) & " " & .GetText(i, COL_RIREKI_TANTOID1 + (j * COL_RIREKI_TANTO_COLCNT))
                        Else
                            If .GetText(i, COL_RIREKI_TANTOGP1 + (j * COL_RIREKI_TANTO_COLCNT)) & .GetText(i, COL_RIREKI_TANTOID1 + (j * COL_RIREKI_TANTO_COLCNT)) <> "" Then
                                StrConvert &= "," & .GetText(i, COL_RIREKI_TANTOGP1 + (j * COL_RIREKI_TANTO_COLCNT)) & " " & .GetText(i, COL_RIREKI_TANTOID1 + (j * COL_RIREKI_TANTO_COLCNT))
                            End If
                        End If
                    Next
                    StrConvert &= vbCrLf
                    StrConvert &= RIREKI_WORK_NAIYONM & vbCrLf & .GetText(i, COL_RIREKI_NAIYOU) & vbCrLf

                Next

                StrConvert &= MAILPARTITION

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True
        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
        End Try
    End Function

    ''' <summary>
    ''' サポセン機器メンテナンスデータ加工処理
    ''' </summary>
    ''' <param name="dataHBKC0209">[IN/OUT]インシデント登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrFormatForDate">[IN]日付フォーマット</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サポセン機器メンテナンスデータをメール用に変換する
    ''' <para>作成情報：2012/08/08 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetConvertSapMainte(ByRef dataHBKC0209 As DataHBKC0209, ByRef StrConvert As String, ByVal StrFormatForDate As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strYoteiDate As String = ""                 '作業予定日変換用文字列
        Dim strEndDate As String = ""                   '作業完了日変換用文字列
        Dim strKoukan As String = ""                    '交換変換用

        Try
            With dataHBKC0209.PropVwSapMainte.Sheets(0)

                '0件の場合空文字を返す
                If .Rows.Count = 0 Then
                    StrConvert = ""
                    '終了ログ出力
                    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                    '正常処理終了
                    Return True
                End If

                '行数分ループを行い、機器情報をセットする
                For i As Integer = 0 To .Rows.Count - 1 Step 1

                    '作業予定日をセットし、日付型に変換
                    If SetConvertDate(.GetText(i, COL_SAP_WORKSCEDT), strYoteiDate, StrFormatForDate) = False Then
                        Return False
                    End If

                    '作業終了日をセットし、日付型に変換
                    If SetConvertDate(.GetText(i, COL_SAP_WORKCOMPDT), strEndDate, StrFormatForDate) = False Then
                        Return False
                    End If
                    '交換が入力されている場合のみ表示
                    If .GetText(i, COL_SAP_CHGNMB) = "" Then
                        strKoukan = ""
                    Else
                        strKoukan = "(" & .GetText(i, COL_SAP_CHGNMB) & ")"
                    End If

                    StrConvert &= MAILPARTITION & vbCrLf
                    StrConvert &= SAP_WORKNM_NAME & .GetText(i, COL_SAP_WORKNM) & strKoukan & vbCrLf
                    StrConvert &= SAP_CHGNMB_NAME & .GetText(i, COL_SAP_KINDNM) & .GetText(i, COL_SAP_NUM) & vbCrLf
                    StrConvert &= SAP_CLASS2_NAME & .GetText(i, COL_SAP_CLASS2) & vbCrLf
                    StrConvert &= SAP_CINM_NAME & .GetText(i, COL_SAP_CINM) & vbCrLf
                    StrConvert &= SAP_WORKSCEDT_NAME & strYoteiDate & vbCrLf
                    StrConvert &= SAP_WORKCOMPDT_NAME & strEndDate & vbCrLf

                Next

                StrConvert &= MAILPARTITION

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True
        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
        End Try
    End Function

    ''' <summary>
    ''' 会議情報データ加工処理
    ''' </summary>
    ''' <param name="dataHBKC0209">[IN/OUT]インシデント登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrFormatForDate">[IN]日付フォーマット</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報データをメール用に変換する
    ''' <para>作成情報：2012/08/08 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetConvertMeeting(ByRef dataHBKC0209 As DataHBKC0209, ByRef StrConvert As String, ByVal StrFormatForDate As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strJisshiDate As String = ""                 '実施日変換用文字列

        Try
            With dataHBKC0209.PropVwMeeting.Sheets(0)

                '0件の場合空文字を返す
                If .Rows.Count = 0 Then
                    StrConvert = ""
                    '終了ログ出力
                    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                    '正常処理終了
                    Return True
                End If

                '行数分ループを行い、会議情報をセットする
                For i As Integer = 0 To .Rows.Count - 1 Step 1

                    '実施日をセットし、日付型に変換
                    If SetConvertDate(.GetText(i, COL_MEETING_JIBI), strJisshiDate, StrFormatForDate) = False Then
                        Return False
                    End If

                    StrConvert &= MAILPARTITION & vbCrLf
                    StrConvert &= MEETING_NONM & .GetText(i, COL_MEETING_NO) & vbCrLf
                    StrConvert &= MEETING_JIBINM & strJisshiDate & vbCrLf
                    StrConvert &= MEETING_TITLENM & .GetText(i, COL_MEETING_TITLE) & vbCrLf
                    StrConvert &= MEETING_NINNM & .GetText(i, COL_MEETING_NIN) & vbCrLf
                Next

                StrConvert &= MAILPARTITION

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True
        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
        End Try
    End Function

    ''' <summary>
    ''' レンタル機器情報Sデータ加工処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0209">[IN/OUT]インシデント登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrFormatForDate">[IN]日付フォーマット</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>レンタル機器情報データをメール用に変換する
    ''' <para>作成情報：2012/08/08 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetConvertRetalS(ByVal Adapter As NpgsqlDataAdapter, _
                                                            ByVal Cn As NpgsqlConnection, _
                                                            ByRef dataHBKC0209 As DataHBKC0209, _
                                                            ByRef StrConvert As String, _
                                                            ByVal StrFormatForDate As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable
        Dim strRentaStartDate As String = ""                 'レンタル開始日変換用文字列
        Dim strRentaEndDate As String = ""                 'レンタル期限日変換用文字列

        Try
            With dataHBKC0209.PropVwkikiInfo.Sheets(0)

                '0件の場合空文字を返す
                If .Rows.Count = 0 Then
                    StrConvert = ""
                    '終了ログ出力
                    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                    '正常処理終了
                    Return True
                End If
            End With

            With dataHBKC0209
                'CI番号分ループを行う
                For i As Integer = 0 To .PropVwkikiInfo.Sheets(0).RowCount - 1 Step 1
                    dtResult = New DataTable
                    'レンタル機器情報取得
                    If sqlHBKC0209.SelectRentalKikiSql(Adapter, Cn, .PropVwkikiInfo.Sheets(0).GetText(i, COL_KIKI_CINMB)) = False Then
                        Return False
                    End If
                    'ログ出力
                    commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "レンタル情報取得", Nothing, Adapter.SelectCommand)

                    'データを取得
                    Adapter.Fill(dtResult)

                    If dtResult.Rows.Count > 0 Then

                        'レンタル開始日をセットし、日付型に変換
                        If SetConvertDate(dtResult.Rows(0).Item(COL_RENTARU_STARDT).ToString, strRentaStartDate, StrFormatForDate) = False Then
                            Return False
                        End If

                        'レンタル期限日をセットし、日付型に変換
                        If SetConvertDate(dtResult.Rows(0).Item(COL_RENTARU_ENDDT).ToString, strRentaEndDate, StrFormatForDate) = False Then
                            Return False
                        End If

                        '変換開始
                        StrConvert &= MAILPARTITION & vbCrLf
                        '複数人利用にデータがある場合は表示
                        If dtResult.Rows(0).Item(COL_USER_NM).ToString <> "" Then
                            StrConvert &= FUKUSU_MSG & vbCrLf
                        End If
                        StrConvert &= KASHIDASHIHN_NM & .PropVwkikiInfo.Sheets(0).GetValue(i, COL_KIKI_SBT) & .PropVwkikiInfo.Sheets(0).GetValue(i, COL_KIKI_NMB) & _
                           " " & dtResult.Rows(0).Item(COL_CLASS2).ToString & " " & dtResult.Rows(0).Item(COL_CINM).ToString & vbCrLf
                        StrConvert &= FUZOKUHIN_NM & dtResult.Rows(0).Item(COL_FUZOKUHIN).ToString & vbCrLf
                        StrConvert &= KASHIDASHIBI_NM & strRentaStartDate & vbCrLf
                        StrConvert &= RENTARU_KIGEN & strRentaEndDate & vbCrLf
                        '複数人利用にデータがある場合は表示
                        If dtResult.Rows(0).Item(COL_USER_NM).ToString <> "" Then
                            StrConvert &= FUKUSU_RIYOH & dtResult.Rows(0).Item(COL_USER_NM).ToString & vbCrLf
                        End If
                    End If

                Next
                StrConvert &= MAILPARTITION
            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True
        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
            dtResult.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' レンタル機器情報Lデータ加工処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0209">[IN/OUT]インシデント登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrFormatForDate">[IN]日付フォーマット</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>レンタル機器情報データをメール用に変換する
    ''' <para>作成情報：2012/08/08 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetConvertRetalL(ByVal Adapter As NpgsqlDataAdapter, _
                                                            ByVal Cn As NpgsqlConnection, _
                                                            ByRef dataHBKC0209 As DataHBKC0209, _
                                                            ByRef StrConvert As String, _
                                                            ByVal StrFormatForDate As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable
        Dim strRentaStartDate As String = ""                 'レンタル開始日変換用文字列
        Dim strRentaEndDate As String = ""                 'レンタル期限日変換用文字列
        Dim strByuSyoNm As String = ""                 '部署保存用
        Try
            With dataHBKC0209.PropVwkikiInfo.Sheets(0)

                '0件の場合空文字を返す
                If .Rows.Count = 0 Then
                    StrConvert = ""
                    '終了ログ出力
                    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                    '正常処理終了
                    Return True
                End If
            End With

            With dataHBKC0209
                'CI番号分ループを行う
                For i As Integer = 0 To .PropVwkikiInfo.Sheets(0).RowCount - 1 Step 1
                    dtResult = New DataTable
                    'レンタル機器情報取得
                    If sqlHBKC0209.SelectRentalKikiSql(Adapter, Cn, .PropVwkikiInfo.Sheets(0).GetText(i, COL_KIKI_CINMB)) = False Then
                        Return False
                    End If
                    'ログ出力
                    commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "レンタル情報取得", Nothing, Adapter.SelectCommand)

                    'データを取得
                    Adapter.Fill(dtResult)

                    If dtResult.Rows.Count > 0 Then

                        'レンタル開始日をセットし、日付型に変換
                        If SetConvertDate(dtResult.Rows(0).Item(COL_RENTARU_STARDT).ToString, strRentaStartDate, StrFormatForDate) = False Then
                            Return False
                        End If

                        'レンタル期限日をセットし、日付型に変換
                        If SetConvertDate(dtResult.Rows(0).Item(COL_RENTARU_ENDDT).ToString, strRentaEndDate, StrFormatForDate) = False Then
                            Return False
                        End If

                        '変換開始
                        StrConvert &= MAILPARTITION & vbCrLf
                        '複数人利用にデータがある場合は表示
                        If dtResult.Rows(0).Item(COL_USER_NM).ToString <> "" Then
                            StrConvert &= FUKUSU_MSG & vbCrLf
                        End If
                        StrConvert &= KASHIDASHIHN_NM & .PropVwkikiInfo.Sheets(0).GetValue(i, COL_KIKI_SBT) & .PropVwkikiInfo.Sheets(0).GetValue(i, COL_KIKI_NMB) & _
                           " " & dtResult.Rows(0).Item(COL_CLASS2).ToString & " " & dtResult.Rows(0).Item(COL_CINM).ToString & vbCrLf
                        StrConvert &= FUZOKUHIN_NM & dtResult.Rows(0).Item(COL_FUZOKUHIN).ToString & vbCrLf
                        StrConvert &= KASHIDASHIBI_NM & strRentaStartDate & vbCrLf
                        StrConvert &= RENTARU_KIGEN & strRentaEndDate & vbCrLf
                        '複数人利用にデータがある場合は表示
                        If dtResult.Rows(0).Item(COL_USER_NM).ToString <> "" Then
                            StrConvert &= FUKUSU_RIYOH & dtResult.Rows(0).Item(COL_USER_NM).ToString & vbCrLf
                        End If

                        strByuSyoNm = dtResult.Rows(0).Item(COL_SETTING_BUSYO).ToString

                    End If

                Next
                StrConvert &= SYOZOKU_BUSYO & vbCrLf & strByuSyoNm & vbCrLf
                StrConvert &= MAILPARTITION
            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True
        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
            dtResult.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 部所有機器情報Sデータ加工処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0209">[IN/OUT]インシデント登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrFormatForDate">[IN]日付フォーマット</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>部所有機器情報データをメール用に変換する
    ''' <para>作成情報：2012/08/08 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetConvertBuyS(ByVal Adapter As NpgsqlDataAdapter, _
                                                            ByVal Cn As NpgsqlConnection, _
                                                            ByRef dataHBKC0209 As DataHBKC0209, _
                                                            ByRef StrConvert As String, _
                                                            ByVal StrFormatForDate As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable
        Dim strConnectDate As String = ""                 '接続日変換用文字列
        Dim strExpDate As String = ""                 '有効日変換用文字列
        Try
            With dataHBKC0209.PropVwkikiInfo.Sheets(0)

                '0件の場合空文字を返す
                If .Rows.Count = 0 Then
                    StrConvert = ""
                    '終了ログ出力
                    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                    '正常処理終了
                    Return True
                End If
            End With

            With dataHBKC0209
                'CI番号分ループを行う
                For i As Integer = 0 To .PropVwkikiInfo.Sheets(0).RowCount - 1 Step 1
                    dtResult = New DataTable
                    'レンタル機器情報取得
                    If sqlHBKC0209.SelectByuKikiSql(Adapter, Cn, .PropVwkikiInfo.Sheets(0).GetText(i, COL_KIKI_CINMB)) = False Then
                        Return False
                    End If
                    'ログ出力
                    commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "部所有機器情報取得", Nothing, Adapter.SelectCommand)

                    'データを取得
                    Adapter.Fill(dtResult)

                    If dtResult.Rows.Count > 0 Then

                        '接続日をセットし、日付型に変換
                        If SetConvertDate(dtResult.Rows(0).Item(COL_CONNECTDT).ToString, strConnectDate, StrFormatForDate) = False Then
                            Return False
                        End If

                        '有効日をセットし、日付型に変換
                        If SetConvertDate(dtResult.Rows(0).Item(COL_EXPIRATIONDT).ToString, strExpDate, StrFormatForDate) = False Then
                            Return False
                        End If

                        '変換開始
                        StrConvert &= MAILPARTITION & vbCrLf
                        StrConvert &= TANMATSU_INFO & vbCrLf
                        StrConvert &= HOST_NM & .PropVwkikiInfo.Sheets(0).GetValue(i, COL_KIKI_SBT) & .PropVwkikiInfo.Sheets(0).GetValue(i, COL_KIKI_NMB) & vbCrLf
                        StrConvert &= MAKER_NM & dtResult.Rows(0).Item(COL_BUY_CLASS2).ToString & vbCrLf
                        StrConvert &= KISYU_NM & dtResult.Rows(0).Item(COL_BUY_CINM).ToString & vbCrLf
                        StrConvert &= KISYUKIND_NM & dtResult.Rows(0).Item(COL_BUY_CLASS1).ToString & vbCrLf
                        StrConvert &= OS_NM & dtResult.Rows(0).Item(COL_SOFTNM_OS).ToString & vbCrLf
                        StrConvert &= vbCrLf
                        StrConvert &= SETTING_INFO & vbCrLf
                        StrConvert &= BUILD_NM & dtResult.Rows(0).Item(COL_SETBUIL).ToString & vbCrLf
                        StrConvert &= FLOOR_NM & dtResult.Rows(0).Item(COL_SETFLOOR).ToString & vbCrLf
                        StrConvert &= ROOM_NM & dtResult.Rows(0).Item(COL_SET_ROOM).ToString & vbCrLf
                        StrConvert &= vbCrLf
                        StrConvert &= ANOTHER_INFO & vbCrLf
                        StrConvert &= CONNECTDT_NM & strConnectDate & vbCrLf
                        StrConvert &= EXPIRATIONDT_NM & strExpDate & vbCrLf
                    End If

                Next

                StrConvert &= MAILPARTITION
            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True
        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
            dtResult.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 部所有機器情報Lデータ加工処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0209">[IN/OUT]インシデント登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrFormatForDate">[IN]日付フォーマット</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>部所有機器情報データをメール用に変換する
    ''' <para>作成情報：2012/08/08 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetConvertBuyL(ByVal Adapter As NpgsqlDataAdapter, _
                                                            ByVal Cn As NpgsqlConnection, _
                                                            ByRef dataHBKC0209 As DataHBKC0209, _
                                                            ByRef StrConvert As String, _
                                                            ByVal StrFormatForDate As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable
        Dim strConnectDate As String = ""                 '接続日変換用文字列
        Dim strExpDate As String = ""                 '有効日変換用文字列
        Try
            With dataHBKC0209.PropVwkikiInfo.Sheets(0)

                '0件の場合空文字を返す
                If .Rows.Count = 0 Then
                    StrConvert = ""
                    '終了ログ出力
                    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                    '正常処理終了
                    Return True
                End If
            End With

            With dataHBKC0209
                'CI番号分ループを行う
                For i As Integer = 0 To .PropVwkikiInfo.Sheets(0).RowCount - 1 Step 1
                    dtResult = New DataTable
                    'レンタル機器情報取得
                    If sqlHBKC0209.SelectByuKikiSql(Adapter, Cn, .PropVwkikiInfo.Sheets(0).GetText(i, COL_KIKI_CINMB)) = False Then
                        Return False
                    End If
                    'ログ出力
                    commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "部所有機器情報取得", Nothing, Adapter.SelectCommand)

                    'データを取得
                    Adapter.Fill(dtResult)

                    If dtResult.Rows.Count > 0 Then

                        '接続日をセットし、日付型に変換
                        If SetConvertDate(dtResult.Rows(0).Item(COL_CONNECTDT).ToString, strConnectDate, StrFormatForDate) = False Then
                            Return False
                        End If

                        '有効日をセットし、日付型に変換
                        If SetConvertDate(dtResult.Rows(0).Item(COL_EXPIRATIONDT).ToString, strExpDate, StrFormatForDate) = False Then
                            Return False
                        End If

                        '変換開始
                        StrConvert &= MAILPARTITION & vbCrLf
                        StrConvert &= TANMATSU_INFO & vbCrLf
                        StrConvert &= HOST_NM & .PropVwkikiInfo.Sheets(0).GetValue(i, COL_KIKI_SBT) & .PropVwkikiInfo.Sheets(0).GetValue(i, COL_KIKI_NMB) & vbCrLf

                        StrConvert &= ALIAS_NM & dtResult.Rows(0).Item(COL_ALIAU).ToString & vbCrLf
                        StrConvert &= DNS_NM & dtResult.Rows(0).Item(COL_DNS).ToString & vbCrLf
                        StrConvert &= IPWARIATE_NM & dtResult.Rows(0).Item(COL_IPUSE).ToString & vbCrLf
                        StrConvert &= IPADDRESS_NM & dtResult.Rows(0).Item(COL_FIXEDIP).ToString & vbCrLf
                        StrConvert &= ZOO_NM & dtResult.Rows(0).Item(COL_ZOOKBN).ToString & vbCrLf
                        StrConvert &= MAKER_NM & dtResult.Rows(0).Item(COL_BUY_CLASS2).ToString & vbCrLf
                        StrConvert &= KISYU_NM & dtResult.Rows(0).Item(COL_BUY_CINM).ToString & vbCrLf
                        StrConvert &= KISYUKIND_NM & dtResult.Rows(0).Item(COL_BUY_CLASS1).ToString & vbCrLf
                        StrConvert &= OS_NM & dtResult.Rows(0).Item(COL_SOFTNM_OS).ToString & vbCrLf

                        StrConvert &= NIC1_NM & dtResult.Rows(0).Item(COL_NIC1).ToString & vbCrLf
                        StrConvert &= MACADDRESS1_NM & dtResult.Rows(0).Item(COL_MACADDRESS1).ToString & vbCrLf
                        StrConvert &= NIC2_NM & dtResult.Rows(0).Item(COL_NIC2).ToString & vbCrLf
                        StrConvert &= MACADDRESS2_NM & dtResult.Rows(0).Item(COL_MACADDRESS2).ToString & vbCrLf

                        StrConvert &= vbCrLf
                        StrConvert &= KANRENKIKI_NM & vbCrLf
                        StrConvert &= ANTIVIRUSSOFT_NM & dtResult.Rows(0).Item(COL_SOFTNM_VIRUS).ToString & vbCrLf
                        StrConvert &= ANTIVIRUSDT_NM & dtResult.Rows(0).Item(COL_ANTIVIRUSSOFCHECKDT).ToString & vbCrLf

                        StrConvert &= vbCrLf
                        StrConvert &= SETTING_INFO & vbCrLf
                        StrConvert &= BUILDNAME_NM & dtResult.Rows(0).Item(COL_SETBUIL).ToString & vbCrLf
                        StrConvert &= FLOOR_NM & dtResult.Rows(0).Item(COL_SETFLOOR).ToString & vbCrLf
                        StrConvert &= ROOMNAME_NM & dtResult.Rows(0).Item(COL_SET_ROOM).ToString & vbCrLf

                        StrConvert &= vbCrLf
                        StrConvert &= KANRI_INFO & vbCrLf
                        StrConvert &= SYAINNUM_NM & dtResult.Rows(0).Item(COL_USRID).ToString & vbCrLf
                        StrConvert &= SHIMEI_NM & dtResult.Rows(0).Item(COL_USRNM).ToString & vbCrLf

                        StrConvert &= vbCrLf
                        StrConvert &= ANOTHER_INFO & vbCrLf
                        StrConvert &= CONNECTDT_NM & strConnectDate & vbCrLf
                        StrConvert &= EXPIRATIONDT_NM & strExpDate & vbCrLf
                        StrConvert &= CONNECTREASON_NM & dtResult.Rows(0).Item(COL_CONECTREASON).ToString & vbCrLf
                        StrConvert &= BIKO_NM & dtResult.Rows(0).Item(COL_BUSYOKIKIBIKO).ToString & vbCrLf
                    End If

                Next

                StrConvert &= MAILPARTITION
            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True
        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
            dtResult.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 機器情報Sデータ加工処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0209">[IN/OUT]インシデント登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>機器情報データをメール用に変換する
    ''' <para>作成情報：2012/08/08 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetConvertKikiInfoS(ByVal Adapter As NpgsqlDataAdapter, _
                                                            ByVal Cn As NpgsqlConnection, _
                                                            ByRef dataHBKC0209 As DataHBKC0209, _
                                                            ByRef StrConvert As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable
        Try
            With dataHBKC0209.PropVwkikiInfo.Sheets(0)

                '0件の場合空文字を返す
                If .Rows.Count = 0 Then
                    StrConvert = ""
                    '終了ログ出力
                    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                    '正常処理終了
                    Return True
                End If
            End With

            With dataHBKC0209
                'CI番号分ループを行う
                For i As Integer = 0 To .PropVwkikiInfo.Sheets(0).RowCount - 1 Step 1
                    dtResult = New DataTable
                    'レンタル機器情報取得
                    If sqlHBKC0209.SelectByuKikiSql(Adapter, Cn, .PropVwkikiInfo.Sheets(0).GetText(i, COL_KIKI_CINMB)) = False Then
                        Return False
                    End If
                    'ログ出力
                    commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "部所有機器情報取得", Nothing, Adapter.SelectCommand)

                    'データを取得
                    Adapter.Fill(dtResult)

                    If dtResult.Rows.Count > 0 Then
                        '変換開始
                        StrConvert &= MAILPARTITION & vbCrLf
                        StrConvert &= KIKI_NM & .PropVwkikiInfo.Sheets(0).GetValue(i, COL_KIKI_SBT) & .PropVwkikiInfo.Sheets(0).GetValue(i, COL_KIKI_NMB) & vbCrLf
                        StrConvert &= KIKIINFO_NM & dtResult.Rows(0).Item(COL_BUY_CLASS2).ToString & " " & dtResult.Rows(0).Item(COL_BUY_CINM).ToString & vbCrLf
                    End If
                Next

                StrConvert &= MAILPARTITION
            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True
        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
            dtResult.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 部所有機器情報一覧データ加工処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0209">[IN/OUT]インシデント登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrFormatForDate">[IN]日付フォーマット</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>部所有機器情報一覧データをメール用に変換する
    ''' <para>作成情報：2012/08/08 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetConvertPartner(ByVal Adapter As NpgsqlDataAdapter, _
                                                            ByVal Cn As NpgsqlConnection, _
                                                            ByRef dataHBKC0209 As DataHBKC0209, _
                                                            ByRef StrConvert As String, _
                                                            ByVal StrFormatForDate As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable
        Dim strConnectDate As String = ""                 '接続日変換用文字列
        Dim strExpDate As String = ""                 '有効日変換用文字列
        Try

            With dataHBKC0209

                dtResult = New DataTable
                'レンタル機器情報取得
                If sqlHBKC0209.SelectAiteSql(Adapter, Cn, dataHBKC0209.PropStrPartnerID) = False Then
                    Return False
                End If
                'ログ出力
                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "機器情報取得", Nothing, Adapter.SelectCommand)

                'データを取得
                Adapter.Fill(dtResult)

                'CI番号分ループを行う
                For i As Integer = 0 To dtResult.Rows.Count - 1 Step 1

                    If dtResult.Rows.Count > 0 Then
                        '接続日をセットし、日付型に変換
                        If SetConvertDate(dtResult.Rows(i).Item(COL_PAR_CONNECTDT).ToString, strConnectDate, StrFormatForDate) = False Then
                            Return False
                        End If

                        '有効日をセットし、日付型に変換
                        If SetConvertDate(dtResult.Rows(i).Item(COL_PAR_EXPIRATIONDT).ToString, strExpDate, StrFormatForDate) = False Then
                            Return False
                        End If
                        '変換開始
                        StrConvert &= dtResult.Rows(i).Item(COL_PAR_KINDNUM).ToString & " " & strConnectDate & " " & strExpDate & " "
                        StrConvert &= dtResult.Rows(i).Item(COL_PAR_CLASS2).ToString & " " & dtResult.Rows(i).Item(COL_PAR_CINM).ToString & vbCrLf
                    End If
                Next
            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True
        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
            dtResult.Dispose()
        End Try
    End Function
End Class
