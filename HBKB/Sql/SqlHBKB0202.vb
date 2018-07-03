Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' 一括登録　システムクラスクラス
''' </summary>
''' <remarks>一括登録　システムのSQLの作成・設定を行う
''' <para>作成情報：2012/07/09 y.ikushima
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKB0202

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK

    '種別（SYS固定）
    Private Const KINDCD_SYS = "101"

    'SQL文宣言

    '重複キー項目データ数取得（SELECT）SQL('[Add] 2012/08/02 y.ikushima CI種別を検索条件に追加)
    Private strSelectCountSameKeySql As String = "SELECT COUNT(*) " & vbCrLf & _
                                                                    "FROM CI_INFO_TB ct " & vbCrLf & _
                                                                    "WHERE ct.Class1 = :Class1 " & vbCrLf & _
                                                                        "AND ct.Class2 = :Class2 " & vbCrLf & _
                                                                        "AND ct.CINM = :CINM " & vbCrLf & _
                                                                        " AND ct.CIKbnCD = :CIKbnCD "

    'ステータスコード取得処理（SELECT）SQL
    Private strSelectCouvertStatusSql As String = "  SELECT CIStateCD " & vbCrLf & _
                                                                    "FROM CISTATE_MTB" & vbCrLf & _
                                                                    "WHERE JtiFlg = '0' " & vbCrLf & _
                                                                        "AND CIKbnCD = :CIKbnCD " & vbCrLf & _
                                                                        "AND CIStateNM = :CIStateNM"

    'グループマスタから関係者IDデータ取得処理（SELECT）SQL
    Private strSelectUsrIDForGroupMSql As String = " SELECT COUNT(GroupCD) " & vbCrLf & _
                                                                        "FROM GRP_MTB" & vbCrLf & _
                                                                        "WHERE JtiFlg = '0' " & vbCrLf & _
                                                                            "AND GroupCD = :GroupCD"

    'ユーザマスタから関係者IDのデータ取得（SELECT）SQL
    Private strSelectUsrIDForUserMSql As String = "SELECT COUNT(HBKUsrID) " & vbCrLf & _
                                                                    "FROM HBKUSR_MTB" & vbCrLf & _
                                                                    "WHERE JtiFlg = '0' " & vbCrLf & _
                                                                        "AND HBKUsrID = :HBKUsrID"

    '[Mod] 2012/08/02 y.ikushima 関係者情報DB定義変更START
    ''所属マスタからデータ取得（SELECT）SQL
    'Private strSelectCountForSzkMSql As String = "SELECT COUNT(*) " & vbCrLf & _
    '                                                                "FROM szk_mtb" & vbCrLf & _
    '                                                                "WHERE HBKUsrID = :HBKUsrID AND GroupCD = :GroupCD"
    '[Mod] 2012/08/02 y.ikushima 関係者情報DB定義変更END

    'CI共通情報新規登録（INSERT）SQL
    Private strInsertCIInfoSql As String = "INSERT INTO CI_INFO_TB" & vbCrLf & _
                                                            "(" & vbCrLf & _
                                                                " CINmb " & vbCrLf & _
                                                                ",CIKbnCD " & vbCrLf & _
                                                                ",KindCD " & vbCrLf & _
                                                                ",Num " & vbCrLf & _
                                                                ",CIStatusCD " & vbCrLf & _
                                                                ",Class1 " & vbCrLf & _
                                                                ",Class2 " & vbCrLf & _
                                                                ",CINM " & vbCrLf & _
                                                                ",CIOwnerCD " & vbCrLf & _
                                                                ",Sort " & vbCrLf & _
                                                                ",CINaiyo " & vbCrLf & _
                                                                ",BIko1 " & vbCrLf & _
                                                                ",Biko2 " & vbCrLf & _
                                                                ",Biko3 " & vbCrLf & _
                                                                ",Biko4 " & vbCrLf & _
                                                                ",Biko5 " & vbCrLf & _
                                                                ",FreeFlg1 " & vbCrLf & _
                                                                ",FreeFlg2 " & vbCrLf & _
                                                                ",FreeFlg3 " & vbCrLf & _
                                                                ",FreeFlg4 " & vbCrLf & _
                                                                ",FreeFlg5 " & vbCrLf & _
                                                                ",Class1Aimai " & vbCrLf & _
                                                                ",Class2Aimai " & vbCrLf & _
                                                                ",CINMAimai " & vbCrLf & _
                                                                ",FreeWordAimai " & vbCrLf & _
                                                                ",BikoAimai " & vbCrLf & _
                                                                ",RegDT " & vbCrLf & _
                                                                ",RegGrpCD " & vbCrLf & _
                                                                ",RegID " & vbCrLf & _
                                                                ",UpdateDT " & vbCrLf & _
                                                                ",UpGrpCD " & vbCrLf & _
                                                                ",UpdateID " & vbCrLf & _
                                                            ") " & vbCrLf & _
                                                            "VALUES " & vbCrLf & _
                                                            "(" & vbCrLf & _
                                                               " :CINmb " & vbCrLf & _
                                                               ",:CIKbnCD " & vbCrLf & _
                                                               ",:KindCD " & vbCrLf & _
                                                               ",:Num " & vbCrLf & _
                                                               ",:CIStatusCD " & vbCrLf & _
                                                               ",:Class1 " & vbCrLf & _
                                                               ",:Class2 " & vbCrLf & _
                                                               ",:CINM " & vbCrLf & _
                                                               ",:CIOwnerCD " & vbCrLf & _
                                                               ",(SELECT COALESCE(MAX(ct.Sort),0)+1 FROM CI_INFO_TB ct WHERE ct.CIKbnCD=:CIKbnCD) " & vbCrLf & _
                                                               ",:CINaiyo " & vbCrLf & _
                                                               ",:BIko1 " & vbCrLf & _
                                                               ",:Biko2 " & vbCrLf & _
                                                               ",:Biko3 " & vbCrLf & _
                                                               ",:Biko4 " & vbCrLf & _
                                                               ",:Biko5 " & vbCrLf & _
                                                               ",:FreeFlg1 " & vbCrLf & _
                                                               ",:FreeFlg2 " & vbCrLf & _
                                                               ",:FreeFlg3 " & vbCrLf & _
                                                               ",:FreeFlg4 " & vbCrLf & _
                                                               ",:FreeFlg5 " & vbCrLf & _
                                                               ",:Class1Aimai " & vbCrLf & _
                                                               ",:Class2Aimai " & vbCrLf & _
                                                               ",:CINMAimai " & vbCrLf & _
                                                               ",:FreeWordAimai " & vbCrLf & _
                                                               ",:BikoAimai " & vbCrLf & _
                                                               ",:RegDT " & vbCrLf & _
                                                               ",:RegGrpCD " & vbCrLf & _
                                                               ",:RegID " & vbCrLf & _
                                                               ",:UpdateDT " & vbCrLf & _
                                                               ",:UpGrpCD " & vbCrLf & _
                                                               ",:UpdateID " & vbCrLf & _
                                                            ") "

    'CIシステム新規登録（INSERT）SQL
    Private strInsertCISystemSql As String = "INSERT INTO CI_SYS_TB " & vbCrLf & _
                                                                "( " & vbCrLf & _
                                                                    " CINmb " & vbCrLf & _
                                                                    ",InfShareteamNM " & vbCrLf & _
                                                                    ",RegDT " & vbCrLf & _
                                                                    ",RegGrpCD " & vbCrLf & _
                                                                    ",RegID " & vbCrLf & _
                                                                    ",UpdateDT " & vbCrLf & _
                                                                    ",UpGrpCD " & vbCrLf & _
                                                                    ",UpdateID " & vbCrLf & _
                                                                ") " & vbCrLf & _
                                                                "VALUES " & vbCrLf & _
                                                                "( " & vbCrLf & _
                                                                    " :CINmb " & vbCrLf & _
                                                                    ",:InfShareteamNM " & vbCrLf & _
                                                                    ",:RegDT " & vbCrLf & _
                                                                    ",:RegGrpCD " & vbCrLf & _
                                                                    ",:RegID " & vbCrLf & _
                                                                    ",:UpdateDT " & vbCrLf & _
                                                                    ",:UpGrpCD " & vbCrLf & _
                                                                    ",:UpdateID " & vbCrLf & _
                                                                ") "

    'ノウハウURL新規登録（INSERT）SQL
    Private strInsertKnowHowUrlSql As String = "INSERT INTO KNOWHOWURL_TB " & vbCrLf & _
                                                                        "( " & vbCrLf & _
                                                                            " CINmb " & vbCrLf & _
                                                                            ",RowNmb " & vbCrLf & _
                                                                            ",Url " & vbCrLf & _
                                                                            ",UrlNaiyo " & vbCrLf & _
                                                                            ",RegDT " & vbCrLf & _
                                                                            ",RegGrpCD " & vbCrLf & _
                                                                            ",RegID " & vbCrLf & _
                                                                            ",UpdateDT " & vbCrLf & _
                                                                            ",UpGrpCD " & vbCrLf & _
                                                                            ",UpdateID " & vbCrLf & _
                                                                        ") " & vbCrLf & _
                                                                        "VALUES  " & vbCrLf & _
                                                                        "( " & vbCrLf & _
                                                                            " :CINmb " & vbCrLf & _
                                                                            ",(SELECT COALESCE(MAX(kt.RowNmb),0)+1 FROM KNOWHOWURL_TB kt WHERE kt.CINmb=:CINmb) " & vbCrLf & _
                                                                            ",:Url " & vbCrLf & _
                                                                            ",:UrlNaiyo " & vbCrLf & _
                                                                            ",:RegDT " & vbCrLf & _
                                                                            ",:RegGrpCD " & vbCrLf & _
                                                                            ",:RegID " & vbCrLf & _
                                                                            ",:UpdateDT " & vbCrLf & _
                                                                            ",:UpGrpCD " & vbCrLf & _
                                                                            ",:UpdateID " & vbCrLf & _
                                                                        ") "

    'サーバー管理情報新規登録（INSERT）SQL
    Private strInsertMngSrvSql As String = "INSERT INTO SRVMNG_TB " & vbCrLf & _
                                                                "( " & vbCrLf & _
                                                                    " CINmb " & vbCrLf & _
                                                                    ",RowNmb " & vbCrLf & _
                                                                    ",ManageNmb " & vbCrLf & _
                                                                    ",ManageNmbNaiyo " & vbCrLf & _
                                                                    ",RegDT " & vbCrLf & _
                                                                    ",RegGrpCD " & vbCrLf & _
                                                                    ",RegID " & vbCrLf & _
                                                                    ",UpdateDT " & vbCrLf & _
                                                                    ",UpGrpCD " & vbCrLf & _
                                                                    ",UpdateID " & vbCrLf & _
                                                                ") " & vbCrLf & _
                                                                "VALUES " & vbCrLf & _
                                                                "( " & vbCrLf & _
                                                                   " :CINmb " & vbCrLf & _
                                                                   ",(SELECT COALESCE(MAX(st.RowNmb),0)+1 FROM SRVMNG_TB st WHERE st.CINmb=:CINmb) " & vbCrLf & _
                                                                   ",:ManageNmb " & vbCrLf & _
                                                                   ",:ManageNmbNaiyo " & vbCrLf & _
                                                                   ",:RegDT " & vbCrLf & _
                                                                   ",:RegGrpCD " & vbCrLf & _
                                                                   ",:RegID " & vbCrLf & _
                                                                   ",:UpdateDT " & vbCrLf & _
                                                                   ",:UpGrpCD " & vbCrLf & _
                                                                   ",:UpdateID " & vbCrLf & _
                                                                ") "

    '[Mod] 2012/08/02 y.ikushima 関係者情報DB定義変更START
    ''関係者新規登録（INSERT）SQL
    'Private strInsertRelationSql As String = "INSERT INTO KANKEI_TB " & vbCrLf & _
    '                                                                "( " & vbCrLf & _
    '                                                                     " CINmb " & vbCrLf & _
    '                                                                     ",RelationKbn " & vbCrLf & _
    '                                                                     ",RelationGrpCD " & vbCrLf & _
    '                                                                     ",RelationUsrID " & vbCrLf & _
    '                                                                     ",RegDT " & vbCrLf & _
    '                                                                     ",RegGrpCD " & vbCrLf & _
    '                                                                     ",RegID " & vbCrLf & _
    '                                                                     ",UpdateDT " & vbCrLf & _
    '                                                                     ",UpGrpCD " & vbCrLf & _
    '                                                                     ",UpdateID " & vbCrLf & _
    '                                                                ") " & vbCrLf & _
    '                                                                "VALUES " & vbCrLf & _
    '                                                                "( " & vbCrLf & _
    '                                                                     " :CINmb " & vbCrLf & _
    '                                                                     ",:RelationKbn " & vbCrLf & _
    '                                                                     ",:RelationGrpCD " & vbCrLf & _
    '                                                                     ",:RelationUsrID " & vbCrLf & _
    '                                                                     ",:RegDT " & vbCrLf & _
    '                                                                     ",:RegGrpCD " & vbCrLf & _
    '                                                                     ",:RegID " & vbCrLf & _
    '                                                                     ",:UpdateDT " & vbCrLf & _
    '                                                                     ",:UpGrpCD " & vbCrLf & _
    '                                                                     ",:UpdateID " & vbCrLf & _
    '                                                                ") "
    '[Mod] 2012/08/02 y.ikushima 関係者情報DB定義変更END


    '関係者新規登録（INSERT）SQL
    Private strInsertRelationSql As String = "INSERT INTO KANKEI_TB " & vbCrLf & _
                                                                    "( " & vbCrLf & _
                                                                         " CINmb " & vbCrLf & _
                                                                         ",RelationKbn " & vbCrLf & _
                                                                         ",RelationID " & vbCrLf & _
                                                                         ",RegDT " & vbCrLf & _
                                                                         ",RegGrpCD " & vbCrLf & _
                                                                         ",RegID " & vbCrLf & _
                                                                         ",UpdateDT " & vbCrLf & _
                                                                         ",UpGrpCD " & vbCrLf & _
                                                                         ",UpdateID " & vbCrLf & _
                                                                    ") " & vbCrLf & _
                                                                    "VALUES " & vbCrLf & _
                                                                    "( " & vbCrLf & _
                                                                         " :CINmb " & vbCrLf & _
                                                                         ",:RelationKbn " & vbCrLf & _
                                                                         ",:RelationID " & vbCrLf & _
                                                                         ",:RegDT " & vbCrLf & _
                                                                         ",:RegGrpCD " & vbCrLf & _
                                                                         ",:RegID " & vbCrLf & _
                                                                         ",:UpdateDT " & vbCrLf & _
                                                                         ",:UpGrpCD " & vbCrLf & _
                                                                         ",:UpdateID " & vbCrLf & _
                                                                    ") "
    '[mod] y.ikushima 2012/08/30 y.ikushima作業CD、作業区分CD修正 START
    ''登録理由履歴新規登録（INSERT）SQL
    'Private strInsertRegReasonRSql As String = "INSERT INTO REGREASON_RTB " & vbCrLf & _
    '                                                                "( " & vbCrLf & _
    '                                                                       " CINmb " & vbCrLf & _
    '                                                                       ",RirekiNo " & vbCrLf & _
    '                                                                       ",RegReason " & vbCrLf & _
    '                                                                       ",WorkCD " & vbCrLf & _
    '                                                                       ",WorkKbnCD " & vbCrLf & _
    '                                                                       ",RegDT " & vbCrLf & _
    '                                                                       ",RegGrpCD " & vbCrLf & _
    '                                                                       ",RegID " & vbCrLf & _
    '                                                                       ",UpdateDT " & vbCrLf & _
    '                                                                       ",UpGrpCD " & vbCrLf & _
    '                                                                       ",UpdateID " & vbCrLf & _
    '                                                                ") " & vbCrLf & _
    '                                                                "VALUES " & vbCrLf & _
    '                                                                "( " & vbCrLf & _
    '                                                                       " :CINmb " & vbCrLf & _
    '                                                                       ",:RirekiNo " & vbCrLf & _
    '                                                                       ",:RegReason " & vbCrLf & _
    '                                                                       ", '" & WORK_CD_PACKAGE & "'" & vbCrLf & _
    '                                                                       ", '" & WORK_KBN_CD_COMPLETE & "'" & vbCrLf & _
    '                                                                       ",:RegDT " & vbCrLf & _
    '                                                                       ",:RegGrpCD " & vbCrLf & _
    '                                                                       ",:RegID " & vbCrLf & _
    '                                                                       ",:UpdateDT " & vbCrLf & _
    '                                                                       ",:UpGrpCD " & vbCrLf & _
    '                                                                       ",:UpdateID " & vbCrLf & _
    '                                                                ") "
    '登録理由履歴新規登録（INSERT）SQL
    Private strInsertRegReasonRSql As String = "INSERT INTO REGREASON_RTB " & vbCrLf & _
                                                                    "( " & vbCrLf & _
                                                                           " CINmb " & vbCrLf & _
                                                                           ",RirekiNo " & vbCrLf & _
                                                                           ",RegReason " & vbCrLf & _
                                                                           ",WorkCD " & vbCrLf & _
                                                                           ",WorkKbnCD " & vbCrLf & _
                                                                           ",RegDT " & vbCrLf & _
                                                                           ",RegGrpCD " & vbCrLf & _
                                                                           ",RegID " & vbCrLf & _
                                                                           ",UpdateDT " & vbCrLf & _
                                                                           ",UpGrpCD " & vbCrLf & _
                                                                           ",UpdateID " & vbCrLf & _
                                                                    ") " & vbCrLf & _
                                                                    "VALUES " & vbCrLf & _
                                                                    "( " & vbCrLf & _
                                                                           " :CINmb " & vbCrLf & _
                                                                           ",:RirekiNo " & vbCrLf & _
                                                                           ",:RegReason " & vbCrLf & _
                                                                           ", ''" & vbCrLf & _
                                                                           ", ''" & vbCrLf & _
                                                                           ",:RegDT " & vbCrLf & _
                                                                           ",:RegGrpCD " & vbCrLf & _
                                                                           ",:RegID " & vbCrLf & _
                                                                           ",:UpdateDT " & vbCrLf & _
                                                                           ",:UpGrpCD " & vbCrLf & _
                                                                           ",:UpdateID " & vbCrLf & _
                                                                    ") "

    '原因リンク履歴新規登録（INSERT）SQL
    Private strInsertCauseLinkRSql As String = "INSERT INTO CAUSELINK_RTB " & vbCrLf & _
                                                                        "( " & vbCrLf & _
                                                                               " CINmb " & vbCrLf & _
                                                                               ",RirekiNo " & vbCrLf & _
                                                                               ",ProcessKbn " & vbCrLf & _
                                                                               ",MngNmb " & vbCrLf & _
                                                                               ",RegDT " & vbCrLf & _
                                                                               ",RegGrpCD " & vbCrLf & _
                                                                               ",RegID " & vbCrLf & _
                                                                               ",UpdateDT " & vbCrLf & _
                                                                               ",UpGrpCD " & vbCrLf & _
                                                                               ",UpdateID " & vbCrLf & _
                                                                        ") " & vbCrLf & _
                                                                        "VALUES " & vbCrLf & _
                                                                        "( " & vbCrLf & _
                                                                               " :CINmb " & vbCrLf & _
                                                                               ",:RirekiNo " & vbCrLf & _
                                                                               ",:ProcessKbn " & vbCrLf & _
                                                                               ",:MngNmb " & vbCrLf & _
                                                                               ",:RegDT " & vbCrLf & _
                                                                               ",:RegGrpCD " & vbCrLf & _
                                                                               ",:RegID " & vbCrLf & _
                                                                               ",:UpdateDT " & vbCrLf & _
                                                                               ",:UpGrpCD " & vbCrLf & _
                                                                               ",:UpdateID " & vbCrLf & _
                                                                        ") "

    'CI共通情報履歴新規登録（INSERT）SQL
    Private strInsertCIInfoRSql As String = "INSERT INTO CI_INFO_RTB " & vbCrLf & _
                                                                "( " & vbCrLf & _
                                                                       " CINmb " & vbCrLf & _
                                                                       ",RirekiNo " & vbCrLf & _
                                                                       ",CIKbnCD " & vbCrLf & _
                                                                       ",KindCD " & vbCrLf & _
                                                                       ",Num " & vbCrLf & _
                                                                       ",CIStatusCD " & vbCrLf & _
                                                                       ",Class1 " & vbCrLf & _
                                                                       ",Class2 " & vbCrLf & _
                                                                       ",CINM " & vbCrLf & _
                                                                       ",CIOwnerCD " & vbCrLf & _
                                                                       ",Sort " & vbCrLf & _
                                                                       ",CINaiyo " & vbCrLf & _
                                                                       ",BIko1 " & vbCrLf & _
                                                                       ",Biko2 " & vbCrLf & _
                                                                       ",Biko3 " & vbCrLf & _
                                                                       ",Biko4 " & vbCrLf & _
                                                                       ",Biko5 " & vbCrLf & _
                                                                       ",FreeFlg1 " & vbCrLf & _
                                                                       ",FreeFlg2 " & vbCrLf & _
                                                                       ",FreeFlg3 " & vbCrLf & _
                                                                       ",FreeFlg4 " & vbCrLf & _
                                                                       ",FreeFlg5 " & vbCrLf & _
                                                                       ",Class1Aimai " & vbCrLf & _
                                                                       ",Class2Aimai " & vbCrLf & _
                                                                       ",CINMAimai " & vbCrLf & _
                                                                       ",FreeWordAimai " & vbCrLf & _
                                                                       ",BikoAimai " & vbCrLf & _
                                                                       ",RegDT " & vbCrLf & _
                                                                       ",RegGrpCD " & vbCrLf & _
                                                                       ",RegID " & vbCrLf & _
                                                                       ",UpdateDT " & vbCrLf & _
                                                                       ",UpGrpCD " & vbCrLf & _
                                                                       ",UpdateID " & vbCrLf & _
                                                                    ") " & vbCrLf & _
                                                                       "SELECT " & vbCrLf & _
                                                                           " ct.CINmb " & vbCrLf & _
                                                                           ",:RirekiNo " & vbCrLf & _
                                                                           ",ct.CIKbnCD " & vbCrLf & _
                                                                           ",ct.KindCD " & vbCrLf & _
                                                                           ",ct.Num " & vbCrLf & _
                                                                           ",ct.CIStatusCD " & vbCrLf & _
                                                                           ",ct.Class1 " & vbCrLf & _
                                                                           ",ct.Class2 " & vbCrLf & _
                                                                           ",ct.CINM " & vbCrLf & _
                                                                           ",ct.CIOwnerCD " & vbCrLf & _
                                                                           ",ct.Sort " & vbCrLf & _
                                                                           ",ct.CINaiyo " & vbCrLf & _
                                                                           ",ct.BIko1 " & vbCrLf & _
                                                                           ",ct.Biko2 " & vbCrLf & _
                                                                           ",ct.Biko3 " & vbCrLf & _
                                                                           ",ct.Biko4 " & vbCrLf & _
                                                                           ",ct.Biko5 " & vbCrLf & _
                                                                           ",ct.FreeFlg1 " & vbCrLf & _
                                                                           ",ct.FreeFlg2 " & vbCrLf & _
                                                                           ",ct.FreeFlg3 " & vbCrLf & _
                                                                           ",ct.FreeFlg4 " & vbCrLf & _
                                                                           ",ct.FreeFlg5 " & vbCrLf & _
                                                                           ",ct.Class1Aimai " & vbCrLf & _
                                                                           ",ct.Class2Aimai " & vbCrLf & _
                                                                           ",ct.CINMAimai " & vbCrLf & _
                                                                           ",ct.FreeWordAimai " & vbCrLf & _
                                                                           ",ct.BikoAimai " & vbCrLf & _
                                                                           ",ct.UpdateDT " & vbCrLf & _
                                                                           ",ct.UpGrpCD " & vbCrLf & _
                                                                           ",ct.UpdateID " & vbCrLf & _
                                                                           ",ct.UpdateDT " & vbCrLf & _
                                                                           ",ct.UpGrpCD " & vbCrLf & _
                                                                           ",ct.UpdateID " & vbCrLf & _
                                                                       "FROM CI_INFO_TB ct " & vbCrLf & _
                                                                       "WHERE ct.CINmb=:CINmb "

    'CIシステム履歴新規登録（INSERT）SQL
    Private strInsertCISystemRSql As String = "INSERT INTO CI_SYS_RTB " & vbCrLf & _
                                                                    "( " & vbCrLf & _
                                                                            " CINmb " & vbCrLf & _
                                                                            ",RirekiNo " & vbCrLf & _
                                                                            ",InfShareteamNM " & vbCrLf & _
                                                                            ",RegDT " & vbCrLf & _
                                                                            ",RegGrpCD " & vbCrLf & _
                                                                            ",RegID " & vbCrLf & _
                                                                            ",UpdateDT " & vbCrLf & _
                                                                            ",UpGrpCD " & vbCrLf & _
                                                                            ",UpdateID " & vbCrLf & _
                                                                    ") " & vbCrLf & _
                                                                        "SELECT " & vbCrLf & _
                                                                             " ct.CINmb " & vbCrLf & _
                                                                             ",:RirekiNo " & vbCrLf & _
                                                                             ",ct.InfShareteamNM " & vbCrLf & _
                                                                             ",ct.UpdateDT " & vbCrLf & _
                                                                             ",ct.UpGrpCD " & vbCrLf & _
                                                                             ",ct.UpdateID " & vbCrLf & _
                                                                             ",ct.UpdateDT " & vbCrLf & _
                                                                             ",ct.UpGrpCD " & vbCrLf & _
                                                                             ",ct.UpdateID " & vbCrLf & _
                                                                        "FROM CI_SYS_TB ct " & vbCrLf & _
                                                                        "WHERE ct.CINmb=:CINmb "


    'ノウハウURL履歴新規登録（INSERT）SQL
    Private strInsertKnowHowUrlRSql As String = "INSERT INTO KNOWHOWURL_RTB " & vbCrLf & _
                                                                            "( " & vbCrLf & _
                                                                                   " CINmb " & vbCrLf & _
                                                                                   ",RirekiNo " & vbCrLf & _
                                                                                   ",RowNmb " & vbCrLf & _
                                                                                   ",Url " & vbCrLf & _
                                                                                   ",UrlNaiyo " & vbCrLf & _
                                                                                   ",RegDT " & vbCrLf & _
                                                                                   ",RegGrpCD " & vbCrLf & _
                                                                                   ",RegID " & vbCrLf & _
                                                                                   ",UpdateDT " & vbCrLf & _
                                                                                   ",UpGrpCD " & vbCrLf & _
                                                                                   ",UpdateID " & vbCrLf & _
                                                                            ") " & vbCrLf & _
                                                                               "SELECT " & vbCrLf & _
                                                                                   " kt.CINmb " & vbCrLf & _
                                                                                   ",:RirekiNo " & vbCrLf & _
                                                                                   ",kt.RowNmb " & vbCrLf & _
                                                                                   ",kt.Url " & vbCrLf & _
                                                                                   ",kt.UrlNaiyo " & vbCrLf & _
                                                                                   ",kt.RegDT " & vbCrLf & _
                                                                                   ",kt.RegGrpCD " & vbCrLf & _
                                                                                   ",kt.RegID " & vbCrLf & _
                                                                                   ",kt.UpdateDT " & vbCrLf & _
                                                                                   ",kt.UpGrpCD " & vbCrLf & _
                                                                                   ",kt.UpdateID " & vbCrLf & _
                                                                               "FROM KNOWHOWURL_TB kt " & vbCrLf & _
                                                                               "WHERE kt.CINmb=:CINmb "

    'サーバー管理情報履歴新規登録（INSERT）SQL
    Private strInsertMngSrvRSql As String = "INSERT INTO SRVMNG_RTB " & vbCrLf & _
                                                                    "( " & vbCrLf & _
                                                                           " CINmb " & vbCrLf & _
                                                                           ",RirekiNo " & vbCrLf & _
                                                                           ",RowNmb " & vbCrLf & _
                                                                           ",ManageNmb " & vbCrLf & _
                                                                           ",ManageNmbNaiyo " & vbCrLf & _
                                                                           ",RegDT " & vbCrLf & _
                                                                           ",RegGrpCD " & vbCrLf & _
                                                                           ",RegID " & vbCrLf & _
                                                                           ",UpdateDT " & vbCrLf & _
                                                                           ",UpGrpCD " & vbCrLf & _
                                                                           ",UpdateID " & vbCrLf & _
                                                                        ") " & vbCrLf & _
                                                                           "SELECT " & vbCrLf & _
                                                                               " st.CINmb " & vbCrLf & _
                                                                               ",:RirekiNo " & vbCrLf & _
                                                                               ",st.RowNmb " & vbCrLf & _
                                                                               ",st.ManageNmb " & vbCrLf & _
                                                                               ",st.ManageNmbNaiyo " & vbCrLf & _
                                                                               ",st.RegDT " & vbCrLf & _
                                                                               ",st.RegGrpCD " & vbCrLf & _
                                                                               ",st.RegID " & vbCrLf & _
                                                                               ",st.UpdateDT " & vbCrLf & _
                                                                               ",st.UpGrpCD " & vbCrLf & _
                                                                               ",st.UpdateID " & vbCrLf & _
                                                                           "FROM SRVMNG_TB st " & vbCrLf & _
                                                                           "WHERE st.CINmb=:CINmb "

    ''関係者履歴新規登録（INSERT）SQL
    'Private strInsertRelationRSql As String = "INSERT INTO KANKEI_RTB " & vbCrLf & _
    '                                                                "( " & vbCrLf & _
    '                                                                     " CINmb " & vbCrLf & _
    '                                                                     ",RirekiNo " & vbCrLf & _
    '                                                                     ",RelationKbn " & vbCrLf & _
    '                                                                     ",RelationGrpCD " & vbCrLf & _
    '                                                                     ",RelationUsrID " & vbCrLf & _
    '                                                                     ",RegDT " & vbCrLf & _
    '                                                                     ",RegGrpCD " & vbCrLf & _
    '                                                                     ",RegID " & vbCrLf & _
    '                                                                     ",UpdateDT " & vbCrLf & _
    '                                                                     ",UpGrpCD " & vbCrLf & _
    '                                                                     ",UpdateID " & vbCrLf & _
    '                                                                ") " & vbCrLf & _
    '                                                                     "SELECT " & vbCrLf & _
    '                                                                         " kt.CINmb " & vbCrLf & _
    '                                                                         ",:RirekiNo " & vbCrLf & _
    '                                                                         ",kt.RelationKbn " & vbCrLf & _
    '                                                                         ",kt.RelationKbn " & vbCrLf & _
    '                                                                         ",kt.RelationGrpCD " & vbCrLf & _
    '                                                                         ",kt.RegDT " & vbCrLf & _
    '                                                                         ",kt.RegGrpCD " & vbCrLf & _
    '                                                                         ",kt.RegID " & vbCrLf & _
    '                                                                         ",kt.UpdateDT " & vbCrLf & _
    '                                                                         ",kt.UpGrpCD " & vbCrLf & _
    '                                                                         ",kt.UpdateID " & vbCrLf & _
    '                                                                     "FROM KANKEI_TB kt " & vbCrLf & _
    '                                                                     "WHERE kt.CINmb=:CINmb "

    '関係者履歴新規登録（INSERT）SQL
    Private strInsertRelationRSql As String = "INSERT INTO KANKEI_RTB " & vbCrLf & _
                                                                    "( " & vbCrLf & _
                                                                         " CINmb " & vbCrLf & _
                                                                         ",RirekiNo " & vbCrLf & _
                                                                         ",RelationKbn " & vbCrLf & _
                                                                         ",RelationID " & vbCrLf & _
                                                                         ",RegDT " & vbCrLf & _
                                                                         ",RegGrpCD " & vbCrLf & _
                                                                         ",RegID " & vbCrLf & _
                                                                         ",UpdateDT " & vbCrLf & _
                                                                         ",UpGrpCD " & vbCrLf & _
                                                                         ",UpdateID " & vbCrLf & _
                                                                    ") " & vbCrLf & _
                                                                         "SELECT " & vbCrLf & _
                                                                             " kt.CINmb " & vbCrLf & _
                                                                             ",:RirekiNo " & vbCrLf & _
                                                                             ",kt.RelationKbn " & vbCrLf & _
                                                                             ",kt.RelationID " & vbCrLf & _
                                                                             ",kt.RegDT " & vbCrLf & _
                                                                             ",kt.RegGrpCD " & vbCrLf & _
                                                                             ",kt.RegID " & vbCrLf & _
                                                                             ",kt.UpdateDT " & vbCrLf & _
                                                                             ",kt.UpGrpCD " & vbCrLf & _
                                                                             ",kt.UpdateID " & vbCrLf & _
                                                                         "FROM KANKEI_TB kt " & vbCrLf & _
                                                                         "WHERE kt.CINmb=:CINmb "

    'サーバー管理履歴情報削除（DELETE）SQL
    Private strDeleteMngSrvSql As String = "DELETE FROM SRVMNG_RTB " & vbCrLf & _
                                                                "WHERE CINmb=:CINmb "

    'ノウハウURL履歴削除（DELETE）SQL
    Private strDeleteKnowHowUrlSql As String = "DELETE FROM KNOWHOWURL_RTB " & vbCrLf & _
                                                                    "WHERE CINmb=:CINmb "

    '関係履歴削除（DELETE）SQL
    Private strDeleteKankeiUrlSql As String = "DELETE FROM KANKEI_RTB " & vbCrLf & _
                                                                    "WHERE CINmb=:CINmb "


    ''' <summary>
    ''' 分類１、分類２、名称のデータ有無取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="strGroup1">[IN]分類１</param>
    ''' <param name="strGroup2">[IN]分類２</param>
    ''' <param name="strName">[IN]名称</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>分類１、分類２、名称のデータ有無取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCountSameKeySql(ByRef Adapter As NpgsqlDataAdapter, _
                                                                ByVal Cn As NpgsqlConnection, _
                                                                ByVal dataHBKB0202 As DataHBKB0202, _
                                                                ByRef strGroup1 As String, _
                                                                ByRef strGroup2 As String, ByRef strName As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectCountSameKeySql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("Class1", NpgsqlTypes.NpgsqlDbType.Varchar))       '分類１
                .Add(New NpgsqlParameter("Class2", NpgsqlTypes.NpgsqlDbType.Varchar))       '分類２
                .Add(New NpgsqlParameter("CINM", NpgsqlTypes.NpgsqlDbType.Varchar))         '名称
                '[Add] 2012/08/02 y.ikushima START
                .Add(New NpgsqlParameter("CIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))      'CI種別
                '[Add] 2012/08/02 y.ikushima END
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("Class1").Value = strGroup1                 '分類１
                .Parameters("Class2").Value = strGroup2                 '分類２
                .Parameters("CINM").Value = strName                     '名称
                '[Add] 2012/08/02 y.ikushima START
                .Parameters("CIKbnCD").Value = CI_TYPE_SYSTEM              'CI種別(システム）
                '[Add] 2012/08/02 y.ikushima END
            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' CIステータスコードのデータ有無取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="strStatus">入力ステータス文字列</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIステータスコードのデータ有無取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCountCIStateCDSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                                ByVal Cn As NpgsqlConnection, _
                                                                ByVal dataHBKB0202 As DataHBKB0202, _
                                                                ByRef strStatus As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectCouvertStatusSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))          'CI種別CD
                .Add(New NpgsqlParameter("CIStateNM", NpgsqlTypes.NpgsqlDbType.Varchar))        'ステータス名
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CIKbnCD").Value = CI_TYPE_SYSTEM                                                       'CI種別CD
                .Parameters("CIStateNM").Value = strStatus                  'ステータス名
            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' グループマスタから関係者IDのデータ有無取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>グループマスタから関係者IDのデータ有無取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectRelationIDForGroup(ByRef Adapter As NpgsqlDataAdapter, _
                                                                  ByVal Cn As NpgsqlConnection, _
                                                                  ByVal dataHBKB0202 As DataHBKB0202) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectUsrIDForGroupMSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("GroupCD", NpgsqlTypes.NpgsqlDbType.Varchar))          'グループコード
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("GroupCD").Value = dataHBKB0202.PropStrGroupCD           'グループコード
            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' ユーザマスタから関係者IDのデータ有無取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="strRelationID">関係者ID</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ユーザマスタから関係者IDのデータ有無取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectRelationIDForUser(ByRef Adapter As NpgsqlDataAdapter, _
                                                                ByVal Cn As NpgsqlConnection, _
                                                                ByVal dataHBKB0202 As DataHBKB0202, _
                                                                ByRef strRelationID As String) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectUsrIDForUserMSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("HBKUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))          '関係者ID
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("HBKUsrID").Value = strRelationID   '関係者ID
            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 新規CI番号、サーバー日付取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規CI番号、サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewCINmbAndSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                                          ByVal Cn As NpgsqlConnection, _
                                                                          ByVal dataHBKB0202 As DataHBKB0202) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = GET_NEXTVAL_CI_NO

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' CI共通情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCIInfoSql(ByRef Cmd As NpgsqlCommand, _
                                                    ByVal Cn As NpgsqlConnection, _
                                                    ByVal dataHBKB0202 As DataHBKB0202, _
                                                    ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Dim strClass1Aimai As String = ""       '分類１（あいまい）
        Dim strClass2Aimai As String = ""       '分類２（あいまい）
        Dim strCINMAimai As String = ""         '名称（あいまい）
        Dim strFreeWordAimai As String = ""     'フリーワード（あいまい）
        Dim strBikoAimai As String = ""         'フリーテキスト（あいまい）

        Try

            'SQL文(INSERT)
            strSQL = strInsertCIInfoSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))            'CI番号
                .Add(New NpgsqlParameter("CIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))          'CI種別CD
                .Add(New NpgsqlParameter("KindCD", NpgsqlTypes.NpgsqlDbType.Varchar))           '種別CD
                .Add(New NpgsqlParameter("Num", NpgsqlTypes.NpgsqlDbType.Varchar))              '番号
                .Add(New NpgsqlParameter("CIStatusCD", NpgsqlTypes.NpgsqlDbType.Varchar))       'ステータスCD
                .Add(New NpgsqlParameter("Class1", NpgsqlTypes.NpgsqlDbType.Varchar))           '分類１
                .Add(New NpgsqlParameter("Class2", NpgsqlTypes.NpgsqlDbType.Varchar))           '分類２
                .Add(New NpgsqlParameter("CINM", NpgsqlTypes.NpgsqlDbType.Varchar))             '名称
                .Add(New NpgsqlParameter("CIOwnerCD", NpgsqlTypes.NpgsqlDbType.Varchar))        'CIオーナーCD
                .Add(New NpgsqlParameter("CINaiyo", NpgsqlTypes.NpgsqlDbType.Varchar))          '説明
                .Add(New NpgsqlParameter("BIko1", NpgsqlTypes.NpgsqlDbType.Varchar))            'フリーテキスト１
                .Add(New NpgsqlParameter("Biko2", NpgsqlTypes.NpgsqlDbType.Varchar))            'フリーテキスト２
                .Add(New NpgsqlParameter("Biko3", NpgsqlTypes.NpgsqlDbType.Varchar))            'フリーテキスト３
                .Add(New NpgsqlParameter("Biko4", NpgsqlTypes.NpgsqlDbType.Varchar))            'フリーテキスト４
                .Add(New NpgsqlParameter("Biko5", NpgsqlTypes.NpgsqlDbType.Varchar))            'フリーテキスト５
                .Add(New NpgsqlParameter("FreeFlg1", NpgsqlTypes.NpgsqlDbType.Varchar))         'フリーフラグ１
                .Add(New NpgsqlParameter("FreeFlg2", NpgsqlTypes.NpgsqlDbType.Varchar))         'フリーフラグ２
                .Add(New NpgsqlParameter("FreeFlg3", NpgsqlTypes.NpgsqlDbType.Varchar))         'フリーフラグ３
                .Add(New NpgsqlParameter("FreeFlg4", NpgsqlTypes.NpgsqlDbType.Varchar))         'フリーフラグ４
                .Add(New NpgsqlParameter("FreeFlg5", NpgsqlTypes.NpgsqlDbType.Varchar))         'フリーフラグ５
                .Add(New NpgsqlParameter("Class1Aimai", NpgsqlTypes.NpgsqlDbType.Varchar))      '分類１（あいまい）
                .Add(New NpgsqlParameter("Class2Aimai", NpgsqlTypes.NpgsqlDbType.Varchar))      '分類２（あいまい）
                .Add(New NpgsqlParameter("CINMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))        '名称（あいまい）
                .Add(New NpgsqlParameter("FreeWordAimai", NpgsqlTypes.NpgsqlDbType.Varchar))    'フリーワード（あいまい）
                .Add(New NpgsqlParameter("BikoAimai", NpgsqlTypes.NpgsqlDbType.Varchar))        'フリーテキスト（あいまい）
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))          '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))         '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))            '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = dataHBKB0202.PropIntCINmb                                  'CI番号
                .Parameters("CIKbnCD").Value = CI_TYPE_SYSTEM                                           'CI種別CD
                .Parameters("KindCD").Value = KINDCD_SYS                                                        '種別CD（SYS固定）
                .Parameters("Num").Value = dataHBKB0202.PropIntCINmb                                    '番号
                .Parameters("CIStatusCD").Value = dataHBKB0202.PropAryCIStatusCD(intIndex).ToString     'ステータスCD
                .Parameters("Class1").Value = dataHBKB0202.PropAryClass1(intIndex).ToString             '分類１
                .Parameters("Class2").Value = dataHBKB0202.PropAryClass2(intIndex).ToString             '分類２
                .Parameters("CINM").Value = dataHBKB0202.PropAryCINM(intIndex).ToString                 '名称
                .Parameters("CIOwnerCD").Value = dataHBKB0202.PropAryCIOwnerCD(intIndex).ToString       'CIオーナーCD
                .Parameters("CINaiyo").Value = dataHBKB0202.PropAryCINaiyo(intIndex).ToString           '説明

                'フリーテキスト１～５
                .Parameters("BIko1").Value = dataHBKB0202.PropAryBIko1(intIndex).ToString
                .Parameters("Biko2").Value = dataHBKB0202.PropAryBIko2(intIndex).ToString
                .Parameters("BIko3").Value = dataHBKB0202.PropAryBIko3(intIndex).ToString
                .Parameters("Biko4").Value = dataHBKB0202.PropAryBIko4(intIndex).ToString
                .Parameters("Biko5").Value = dataHBKB0202.PropAryBIko5(intIndex).ToString

                'フリーフラグ１～５
                .Parameters("FreeFlg1").Value = dataHBKB0202.PropAryFreeFlg1(intIndex).ToString
                .Parameters("FreeFlg2").Value = dataHBKB0202.PropAryFreeFlg2(intIndex).ToString
                .Parameters("FreeFlg3").Value = dataHBKB0202.PropAryFreeFlg3(intIndex).ToString
                .Parameters("FreeFlg4").Value = dataHBKB0202.PropAryFreeFlg4(intIndex).ToString
                .Parameters("FreeFlg5").Value = dataHBKB0202.PropAryFreeFlg5(intIndex).ToString

                'あいまい検索文字列設定
                '※フリーワード（あいまい）　：分類１～２、名称、説明を連結
                '※フリーテキスト（あいまい）：フリーテキスト１～５を連結
                strClass1Aimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0202.PropAryClass1(intIndex).ToString)
                strClass2Aimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0202.PropAryClass2(intIndex).ToString)
                strCINMAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0202.PropAryCINM(intIndex).ToString)
                strFreeWordAimai = strClass1Aimai & strClass2Aimai & strCINMAimai & commonLogicHBK.ChangeStringForSearch(dataHBKB0202.PropAryCINaiyo(intIndex).ToString)
                strBikoAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0202.PropAryBIko1(intIndex).ToString) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKB0202.PropAryBIko2(intIndex).ToString) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKB0202.PropAryBIko3(intIndex).ToString) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKB0202.PropAryBIko4(intIndex).ToString) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKB0202.PropAryBIko5(intIndex).ToString)
                .Parameters("Class1Aimai").Value = strClass1Aimai           '分類１（あいまい）
                .Parameters("Class2Aimai").Value = strClass2Aimai           '分類２（あいまい）
                .Parameters("CINMAimai").Value = strCINMAimai               '名称（あいまい）
                .Parameters("FreeWordAimai").Value = strFreeWordAimai       'フリーワード（あいまい）
                .Parameters("BikoAimai").Value = strBikoAimai               'フリーテキスト（あいまい）

                .Parameters("RegDT").Value = dataHBKB0202.PropDtmSysDate    '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD             '登録者グループCD
                .Parameters("RegID").Value = PropUserId                     '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0202.PropDtmSysDate '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD              '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                  '最終更新者ID

            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' CIシステム新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIシステム新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCISystemSql(ByRef Cmd As NpgsqlCommand, _
                                                        ByVal Cn As NpgsqlConnection, _
                                                        ByVal dataHBKB0202 As DataHBKB0202, _
                                                        ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertCISystemSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))            'CI番号
                .Add(New NpgsqlParameter("InfShareteamNM", NpgsqlTypes.NpgsqlDbType.Varchar))   '情報共有先
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))          '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))         '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))            '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = dataHBKB0202.PropIntCINmb                                          'CI番号
                .Parameters("InfShareteamNM").Value = dataHBKB0202.PropAryInfShareteamNM(intIndex).ToString     '情報共有先
                .Parameters("RegDT").Value = dataHBKB0202.PropDtmSysDate                                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0202.PropDtmSysDate                                     '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                                  '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                                      '最終更新者ID
            End With


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' ノウハウURL新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ノウハウURL新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertKnowHowUrlSql(ByRef Cmd As NpgsqlCommand, _
                                                             ByVal Cn As NpgsqlConnection, _
                                                             ByVal dataHBKB0202 As DataHBKB0202, _
                                                             ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertKnowHowUrlSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))            'CI番号
                .Add(New NpgsqlParameter("Url", NpgsqlTypes.NpgsqlDbType.Varchar))              'ノウハウURL
                .Add(New NpgsqlParameter("UrlNaiyo", NpgsqlTypes.NpgsqlDbType.Varchar))         'ノウハウURL説明
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))          '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))         '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))            '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = dataHBKB0202.PropIntCINmb                          'CI番号
                .Parameters("Url").Value = dataHBKB0202.PropAryUrl(intIndex).ToString           'ノウハウURL
                .Parameters("UrlNaiyo").Value = dataHBKB0202.PropAryUrlNaiyo(intIndex).ToString 'ノウハウURL説明
                .Parameters("RegDT").Value = dataHBKB0202.PropDtmSysDate                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0202.PropDtmSysDate                     '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                  '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                      '最終更新者ID
            End With


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 登録理由履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>登録理由履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertRegReasonRSql(ByRef Cmd As NpgsqlCommand, _
                                                            ByVal Cn As NpgsqlConnection, _
                                                            ByVal dataHBKB0202 As DataHBKB0202) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertRegReasonRSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))            'CI番号
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))         '履歴番号
                .Add(New NpgsqlParameter("RegReason", NpgsqlTypes.NpgsqlDbType.Varchar))        '登録理由
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))          '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))         '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))            '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = dataHBKB0202.PropIntCINmb                          'CI番号
                .Parameters("RirekiNo").Value = dataHBKB0202.PropIntRirekiNo                    '履歴番号
                .Parameters("RegReason").Value = dataHBKB0202.PropStrRegReason                  '登録理由
                .Parameters("RegDT").Value = dataHBKB0202.PropDtmSysDate                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0202.PropDtmSysDate                     '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                  '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                      '最終更新者ID
            End With


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 原因リンク履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>原因リンク履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCauseLinkRSql(ByRef Cmd As NpgsqlCommand, _
                                                           ByVal Cn As NpgsqlConnection, _
                                                           ByVal dataHBKB0202 As DataHBKB0202) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertCauseLinkRSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))            'CI番号
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))         '履歴番号
                .Add(New NpgsqlParameter("MngNmb", NpgsqlTypes.NpgsqlDbType.Integer))           '管理番号
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))       'プロセス区分
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))          '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))         '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))            '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = dataHBKB0202.PropIntCINmb                          'CI番号
                .Parameters("RirekiNo").Value = dataHBKB0202.PropIntRirekiNo                    '履歴番号
                .Parameters("MngNmb").Value = CInt(dataHBKB0202.PropStrMngNmb)                  '管理番号
                .Parameters("ProcessKbn").Value = dataHBKB0202.PropStrProcessKbn                'プロセス区分
                .Parameters("RegDT").Value = dataHBKB0202.PropDtmSysDate                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0202.PropDtmSysDate                     '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                  '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                      '最終更新者ID
            End With


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' サーバー管理情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サーバー管理情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertMngSrvSql(ByRef Cmd As NpgsqlCommand, _
                                                     ByVal Cn As NpgsqlConnection, _
                                                     ByVal dataHBKB0202 As DataHBKB0202, _
                                                     ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertMngSrvSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
                .Add(New NpgsqlParameter("ManageNmb", NpgsqlTypes.NpgsqlDbType.Varchar))                'サーバー管理番号
                .Add(New NpgsqlParameter("ManageNmbNaiyo", NpgsqlTypes.NpgsqlDbType.Varchar))           'サーバー管理番号説明
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                  '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                 '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                    '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = dataHBKB0202.PropIntCINmb                                          'CI番号
                .Parameters("ManageNmb").Value = dataHBKB0202.PropAryManageNmb(intIndex).ToString               'サーバー管理番号
                .Parameters("ManageNmbNaiyo").Value = dataHBKB0202.PropAryManageNmbNaiyo(intIndex).ToString     'サーバー管理番号説明
                .Parameters("RegDT").Value = dataHBKB0202.PropDtmSysDate                                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0202.PropDtmSysDate                                     '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                                  '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                                      '最終更新者ID
            End With


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 関係者情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>関係者情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertRelationSql(ByRef Cmd As NpgsqlCommand, _
                                                      ByVal Cn As NpgsqlConnection, _
                                                      ByVal dataHBKB0202 As DataHBKB0202, _
                                                      ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertRelationSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
                .Add(New NpgsqlParameter("RelationKbn", NpgsqlTypes.NpgsqlDbType.Varchar))              '関係区分
                '[Mod] 2012/08/02 y.ikushima 関係者情報DB定義変更START
                .Add(New NpgsqlParameter("RelationID", NpgsqlTypes.NpgsqlDbType.Varchar))               '関係ID
                '.Add(New NpgsqlParameter("RelationGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))           '関係グループID
                '.Add(New NpgsqlParameter("RelationUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))           '関係ユーザID
                '[Mod] 2012/08/02 y.ikushima 関係者情報DB定義変更END
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                  '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                 '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                    '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = dataHBKB0202.PropIntCINmb                                  'CI番号
                .Parameters("RelationKbn").Value = dataHBKB0202.PropAryRelationKbn(intIndex).ToString   '関係区分
                .Parameters("RelationID").Value = dataHBKB0202.PropAryRelationID(intIndex).ToString             '関係ID
                '[Mod] 2012/08/02 y.ikushima 関係者情報DB定義変更START
                ''関係区分がユーザの際は、ユーザIDを登録する
                'If dataHBKB0202.PropAryRelationKbn(intIndex).ToString = KBN_USER Then
                '    .Parameters("RelationGrpCD").Value = dataHBKB0202.PropAryRelationGrpCD(intIndex).ToString             '関係グループID
                '    .Parameters("RelationUsrID").Value = dataHBKB0202.PropAryRelationUsrID(intIndex).ToString             '関係ユーザID
                'Else
                '    .Parameters("RelationGrpCD").Value = dataHBKB0202.PropAryRelationGrpCD(intIndex).ToString             '関係グループID
                '    .Parameters("RelationUsrID").Value = ""
                'End If
                '[Mod] 2012/08/02 y.ikushima 関係者情報DB定義変更END
                .Parameters("RegDT").Value = dataHBKB0202.PropDtmSysDate                                '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                         '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                 '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0202.PropDtmSysDate                             '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
            End With


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' CI共通情報履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCIInfoRSql(ByRef Cmd As NpgsqlCommand, _
                                                     ByVal Cn As NpgsqlConnection, _
                                                     ByVal dataHBKB0202 As DataHBKB0202) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertCIInfoRSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))                 '履歴番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("RirekiNo").Value = dataHBKB0202.PropIntRirekiNo                            '履歴番号
                .Parameters("CINmb").Value = dataHBKB0202.PropIntCINmb                                  'CI番号
            End With


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' CIシステム履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIシステム履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCISystemRSql(ByRef Cmd As NpgsqlCommand, _
                                                          ByVal Cn As NpgsqlConnection, _
                                                          ByVal dataHBKB0202 As DataHBKB0202) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertCISystemRSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))                 '履歴番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("RirekiNo").Value = dataHBKB0202.PropIntRirekiNo                            '履歴番号
                .Parameters("CINmb").Value = dataHBKB0202.PropIntCINmb                                  'CI番号
            End With


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' ノウハウURL履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ノウハウURL履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertKnowHowUrlRSql(ByRef Cmd As NpgsqlCommand, _
                                                               ByVal Cn As NpgsqlConnection, _
                                                               ByVal dataHBKB0202 As DataHBKB0202) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertKnowHowUrlRSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))                 '履歴番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("RirekiNo").Value = dataHBKB0202.PropIntRirekiNo                            '履歴番号
                .Parameters("CINmb").Value = dataHBKB0202.PropIntCINmb                                  'CI番号
            End With


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' サーバー管理情報履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サーバー管理情報履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertMngSrvRSql(ByRef Cmd As NpgsqlCommand, _
                                                       ByVal Cn As NpgsqlConnection, _
                                                       ByVal dataHBKB0202 As DataHBKB0202) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try
            'SQL文(INSERT)
            strSQL = strInsertMngSrvRSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))                 '履歴番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("RirekiNo").Value = dataHBKB0202.PropIntRirekiNo                            '履歴番号
                .Parameters("CINmb").Value = dataHBKB0202.PropIntCINmb                                  'CI番号
            End With


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 関係者履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>関係者履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertRelationRSql(ByRef Cmd As NpgsqlCommand, _
                                                        ByVal Cn As NpgsqlConnection, _
                                                        ByVal dataHBKB0202 As DataHBKB0202) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertRelationRSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))                 '履歴番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("RirekiNo").Value = dataHBKB0202.PropIntRirekiNo                            '履歴番号
                .Parameters("CINmb").Value = dataHBKB0202.PropIntCINmb                                  'CI番号
            End With


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function


    ''' <summary>
    ''' サーバー管理履歴情報削除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サーバー管理履歴情報削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteMngSrvSqlR(ByRef Cmd As NpgsqlCommand, _
                                                        ByVal Cn As NpgsqlConnection, _
                                                        ByVal dataHBKB0202 As DataHBKB0202) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(DELETE)
            strSQL = strDeleteMngSrvSql

            'データアダプタに、SQLのDELETE文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = dataHBKB0202.PropIntCINmb                                  'CI番号
            End With


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' ノウハウURL履歴削除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ノウハウURL履歴削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteKnowHowUrlSqlR(ByRef Cmd As NpgsqlCommand, _
                                                               ByVal Cn As NpgsqlConnection, _
                                                               ByVal dataHBKB0202 As DataHBKB0202) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(DELETE)
            strSQL = strDeleteKnowHowUrlSql

            'データアダプタに、SQLのDELETE文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))            'CI番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = dataHBKB0202.PropIntCINmb                          'CI番号
            End With


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 関係履歴削除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>関係履歴削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteKankeiSqlR(ByRef Cmd As NpgsqlCommand, _
                                                      ByVal Cn As NpgsqlConnection, _
                                                      ByVal dataHBKB0202 As DataHBKB0202) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(DELETE)
            strSQL = strDeleteKankeiUrlSql

            'データアダプタに、SQLのDELETE文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))            'CI番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = dataHBKB0202.PropIntCINmb                          'CI番号
            End With


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    '[Mod] 2012/08/02 y.ikushima 関係者情報DB定義変更START
    ' ''' <summary>
    ' ''' 所属マスタからのデータ有無取得用SQLの作成・設定処理
    ' ''' </summary>
    ' ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ' ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ' ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ' ''' <param name="strRelationGrpCD">関係者グループコード</param>
    ' ''' <param name="strRelationUsrID">関係者ユーザコード</param>
    ' ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ' ''' <remarks>所属マスタからのデータ有無取得用のSQLを作成し、アダプタにセットする
    ' ''' <para>作成情報：2012/07/23 y.ikushima
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Public Function SetSelectRelationIDForSzk(ByRef Adapter As NpgsqlDataAdapter, _
    '                                                          ByVal Cn As NpgsqlConnection, _
    '                                                          ByVal dataHBKB0202 As DataHBKB0202, _
    '                                                          ByRef strRelationGrpCD As String, ByRef strRelationUsrID As String) As Boolean
    '    '開始ログ出力
    '    CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数の宣言
    '    Dim strSQL As String = ""

    '    Try

    '        'SQL文(SELECT)
    '        strSQL = strSelectUsrIDForUserMSql

    '        'データアダプタに、SQLのSELECT文を設定
    '        Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

    '        'バインド変数に型をセット
    '        With Adapter.SelectCommand.Parameters
    '            .Add(New NpgsqlParameter("HBKUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))          'ひびきユーザーID
    '            .Add(New NpgsqlParameter("GroupCD", NpgsqlTypes.NpgsqlDbType.Varchar))          'グループCD
    '        End With

    '        'バインド変数に値をセット
    '        With Adapter.SelectCommand
    '            .Parameters("HBKUsrID").Value = strRelationUsrID   'ひびきユーザーID
    '            .Parameters("GroupCD").Value = strRelationGrpCD          'グループCD
    '        End With

    '        '終了ログ出力
    '        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
    '        '例外処理
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    End Try

    'End Function
    '[Mod] 2012/08/02 y.ikushima 関係者情報DB定義変更END
End Class
