Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.IO

''' <summary>
''' 一括登録　文書クラス
''' </summary>
''' <remarks>一括登録　文書のSQLの作成・設定を行う
''' <para>作成情報：2012/07/20 s.tsuruta
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKB0203

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK


    '* SQL文宣言

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

    'CI文書新規登録（INSERT）SQL
    Private strInsertCIDocSql As String = "INSERT INTO CI_DOC_TB ( " & vbCrLf & _
                                               " CINmb " & vbCrLf & _
                                               ",Version " & vbCrLf & _
                                               ",CrateID " & vbCrLf & _
                                               ",CrateNM " & vbCrLf & _
                                               ",CreateDT " & vbCrLf & _
                                               ",LastUpID " & vbCrLf & _
                                               ",LastUpNM " & vbCrLf & _
                                               ",LastUpDT " & vbCrLf & _
                                               ",FileMngNmb " & vbCrLf & _
                                               ",ChargeID " & vbCrLf & _
                                               ",ChargeNM " & vbCrLf & _
                                               ",ShareteamNM " & vbCrLf & _
                                               ",OfferNM " & vbCrLf & _
                                               ",DelDT " & vbCrLf & _
                                               ",DelReason " & vbCrLf & _
                                               ",ShareteamNMAimai " & vbCrLf & _
                                               ",RegDT " & vbCrLf & _
                                               ",RegGrpCD " & vbCrLf & _
                                               ",RegID " & vbCrLf & _
                                               ",UpdateDT " & vbCrLf & _
                                               ",UpGrpCD " & vbCrLf & _
                                               ",UpdateID " & vbCrLf & _
                                             ") " & vbCrLf & _
                                             "VALUES ( " & vbCrLf & _
                                               " :CINmb " & vbCrLf & _
                                               ",:Version " & vbCrLf & _
                                               ",:CrateID " & vbCrLf & _
                                               ",:CrateNM " & vbCrLf & _
                                               ",CASE WHEN :CreateDT = ''" & vbCrLf & _
                                               " THEN '' " & vbCrLf & _
                                               " ELSE TO_CHAR(TO_DATE(:CreateDT, 'YYYY/MM/DD'),'YYYYMMDD') END " & vbCrLf & _
                                               ",:LastUpID " & vbCrLf & _
                                               ",:LastUpNM " & vbCrLf & _
                                               ",CASE WHEN :LastUpDT = '' " & vbCrLf & _
                                               " THEN NULL " & vbCrLf & _
                                               " ELSE TO_TIMESTAMP(:LastUpDT,'YYYY/MM/DD HH24:MI') END " & vbCrLf & _
                                               ", CASE WHEN :FileMngNmb = 0 " & vbCrLf & _
                                               " THEN NULL " & vbCrLf & _
                                               " ELSE :FileMngNmb END " & vbCrLf & _
                                               ",:ChargeID " & vbCrLf & _
                                               ",:ChargeNM " & vbCrLf & _
                                               ",:ShareteamNM " & vbCrLf & _
                                               ",:OfferNM " & vbCrLf & _
                                               ",CASE WHEN :DelDT = ''" & vbCrLf & _
                                               " THEN '' " & vbCrLf & _
                                               " ELSE TO_CHAR(TO_DATE(:DelDT, 'YYYY/MM/DD'),'YYYYMMDD') END " & vbCrLf & _
                                               ",:DelReason " & vbCrLf & _
                                               ",:ShareteamNMAimai " & vbCrLf & _
                                               ",:RegDT " & vbCrLf & _
                                               ",:RegGrpCD " & vbCrLf & _
                                               ",:RegID " & vbCrLf & _
                                               ",:UpdateDT " & vbCrLf & _
                                               ",:UpGrpCD " & vbCrLf & _
                                               ",:UpdateID " & vbCrLf & _
                                             ") "

    'ファイル管理テーブル新規登録（INSERT）SQL
    Private strInsertFileMngSql As String = "INSERT INTO FILE_MNG_TB ( " & vbCrLf & _
                                           " FileMngNmb " & vbCrLf & _
                                           ",FilePath " & vbCrLf & _
                                           ",FileNM " & vbCrLf & _
                                           ",Ext " & vbCrLf & _
                                           ",HaikiKbn " & vbCrLf & _
                                           ",RegDT " & vbCrLf & _
                                           ",RegGrpCD " & vbCrLf & _
                                           ",RegID " & vbCrLf & _
                                           ",UpdateDT " & vbCrLf & _
                                           ",UpGrpCD " & vbCrLf & _
                                           ",UpdateID " & vbCrLf & _
                                           ") " & vbCrLf & _
                                           "VALUES ( " & vbCrLf & _
                                           " :FileMngNmb" & vbCrLf & _
                                           ",:FIlePath" & vbCrLf & _
                                           ",:FIleNM" & vbCrLf & _
                                           ",:Ext" & vbCrLf & _
                                           ",:HaikiKbn" & vbCrLf & _
                                           ",:RegDT" & vbCrLf & _
                                           ",:RegGrpCD" & vbCrLf & _
                                           ",:RegID" & vbCrLf & _
                                           ",:UpdateDT" & vbCrLf & _
                                           ",:UpGrpCD" & vbCrLf & _
                                           ",:UpdateID" & vbCrLf & _
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
    '                                                                       ",'" & WORK_CD_PACKAGE & "'" & vbCrLf & _
    '                                                                       ",'" & WORK_KBN_CD_COMPLETE & "'" & vbCrLf & _
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
                                                                           ",''" & vbCrLf & _
                                                                           ",''" & vbCrLf & _
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

    'CI文書履歴テーブル新規登録（INSERT）SQL
    Private strInsertCIDocRSql As String = "INSERT INTO CI_DOC_RTB ( " & vbCrLf & _
                                             " CINmb " & vbCrLf & _
                                             ",RirekiNo " & vbCrLf & _
                                             ",Version " & vbCrLf & _
                                             ",CrateID " & vbCrLf & _
                                             ",CrateNM " & vbCrLf & _
                                             ",CreateDT " & vbCrLf & _
                                             ",LastUpID " & vbCrLf & _
                                             ",LastUpNM " & vbCrLf & _
                                             ",LastUpDT " & vbCrLf & _
                                             ",FileMngNmb " & vbCrLf & _
                                             ",ChargeID " & vbCrLf & _
                                             ",ChargeNM " & vbCrLf & _
                                             ",ShareteamNM " & vbCrLf & _
                                             ",OfferNM " & vbCrLf & _
                                             ",DelDT " & vbCrLf & _
                                             ",DelReason " & vbCrLf & _
                                             ",ShareteamNMAimai " & vbCrLf & _
                                             ",RegDT " & vbCrLf & _
                                             ",RegGrpCD " & vbCrLf & _
                                             ",RegID " & vbCrLf & _
                                             ",UpdateDT " & vbCrLf & _
                                             ",UpGrpCD " & vbCrLf & _
                                             ",UpdateID " & vbCrLf & _
                                             ") " & vbCrLf & _
                                             "SELECT " & vbCrLf & _
                                             " :CINmb " & vbCrLf & _
                                             ",:RirekiNo " & vbCrLf & _
                                             ",ct.Version " & vbCrLf & _
                                             ",ct.CrateID " & vbCrLf & _
                                             ",ct.CrateNM " & vbCrLf & _
                                             ",ct.CreateDT " & vbCrLf & _
                                             ",ct.LastUpID " & vbCrLf & _
                                             ",ct.LastUpNM " & vbCrLf & _
                                             ",TO_TIMESTAMP(TO_CHAR(ct.LastUpDT,'YYYY/MM/DD HH24:MIN:SS'),'YYYY/MM/DD HH24:MIN') " & vbCrLf & _
                                             ",ct.FileMngNmb " & vbCrLf & _
                                             ",ct.ChargeID " & vbCrLf & _
                                             ",ct.ChargeNM " & vbCrLf & _
                                             ",ct.ShareteamNM " & vbCrLf & _
                                             ",ct.OfferNM " & vbCrLf & _
                                             ",ct.DelDT " & vbCrLf & _
                                             ",ct.DelReason " & vbCrLf & _
                                             ",ct.ShareteamNMAimai " & vbCrLf & _
                                             ",ct.UpdateDT " & vbCrLf & _
                                             ",ct.UpGrpCD " & vbCrLf & _
                                             ",ct.UpdateID " & vbCrLf & _
                                             ",ct.UpdateDT " & vbCrLf & _
                                             ",ct.UpGrpCD " & vbCrLf & _
                                             ",ct.UpdateID " & vbCrLf & _
                                             "FROM CI_DOC_TB ct " & vbCrLf & _
                                             "WHERE ct.CINmb=:CINmb "


    ''' <summary>
    ''' 分類１、分類２、名称のデータ有無取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="strGroup1">[IN]分類１</param>
    ''' <param name="strGroup2">[IN]分類２</param>
    ''' <param name="strName">[IN]名称</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>分類１、分類２、名称のデータ有無取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報：2012/07/30 y.ikushima</p>
    ''' </para></remarks>
    Public Function SetSelectCountSameKeySql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKB0203 As DataHBKB0203, _
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
                .Parameters("CIKbnCD").Value = CI_TYPE_DOC              'CI種別(文書）
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
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="strStatus">入力ステータス文字列</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIステータスコードのデータ有無取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報：2012/07/30 y.ikushima</p>
    ''' </para></remarks>
    Public Function SetSelectCountCIStateCDSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKB0203 As DataHBKB0203, _
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
                .Parameters("CIKbnCD").Value = CI_TYPE_DOC                                                       'CI種別CD
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
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>グループマスタから関係者IDのデータ有無取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectRelationIDForGroup(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKB0203 As DataHBKB0203, _
                                             ByVal intIndex As Integer) As Boolean

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
                .Parameters("GroupCD").Value = DataHBKB0203.PropStrGroupCD           'グループコード
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
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規CI番号、サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewCINmbAndSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                   ByVal Cn As NpgsqlConnection, _
                                                   ByVal dataHBKB0203 As DataHBKB0203) As Boolean
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
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCIInfoSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB0203 As DataHBKB0203, _
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
                .Parameters("CINmb").Value = DataHBKB0203.PropIntCINmb                                  'CI番号
                .Parameters("CIKbnCD").Value = CI_TYPE_DOC                                              'CI種別CD
                .Parameters("KindCD").Value = "201"                                                          '種別CD
                .Parameters("Num").Value = dataHBKB0203.PropAryNum(intIndex)                            '番号
                .Parameters("CIStatusCD").Value = DataHBKB0203.PropAryCIStatusCD(intIndex).ToString     'ステータスCD
                .Parameters("Class1").Value = DataHBKB0203.PropAryClass1(intIndex).ToString             '分類１
                .Parameters("Class2").Value = DataHBKB0203.PropAryClass2(intIndex).ToString             '分類２
                .Parameters("CINM").Value = DataHBKB0203.PropAryCINM(intIndex).ToString                 '名称
                .Parameters("CIOwnerCD").Value = DataHBKB0203.PropAryCIOwnerCD(intIndex).ToString       'CIオーナーCD
                .Parameters("CINaiyo").Value = DataHBKB0203.PropAryCINaiyo(intIndex).ToString           '説明

                'フリーテキスト１～５
                .Parameters("BIko1").Value = DataHBKB0203.PropAryBIko1(intIndex).ToString
                .Parameters("Biko2").Value = DataHBKB0203.PropAryBIko2(intIndex).ToString
                .Parameters("BIko3").Value = DataHBKB0203.PropAryBIko3(intIndex).ToString
                .Parameters("Biko4").Value = DataHBKB0203.PropAryBIko4(intIndex).ToString
                .Parameters("Biko5").Value = DataHBKB0203.PropAryBIko5(intIndex).ToString

                'フリーフラグ１～５
                .Parameters("FreeFlg1").Value = DataHBKB0203.PropAryFreeFlg1(intIndex).ToString
                .Parameters("FreeFlg2").Value = DataHBKB0203.PropAryFreeFlg2(intIndex).ToString
                .Parameters("FreeFlg3").Value = DataHBKB0203.PropAryFreeFlg3(intIndex).ToString
                .Parameters("FreeFlg4").Value = DataHBKB0203.PropAryFreeFlg4(intIndex).ToString
                .Parameters("FreeFlg5").Value = DataHBKB0203.PropAryFreeFlg5(intIndex).ToString

                'あいまい検索文字列設定
                '※フリーワード（あいまい）　：分類１～２、名称、説明を連結
                '※フリーテキスト（あいまい）：フリーテキスト１～５を連結
                strClass1Aimai = commonLogicHBK.ChangeStringForSearch(DataHBKB0203.PropAryClass1(intIndex).ToString)
                strClass2Aimai = commonLogicHBK.ChangeStringForSearch(DataHBKB0203.PropAryClass2(intIndex).ToString)
                strCINMAimai = commonLogicHBK.ChangeStringForSearch(DataHBKB0203.PropAryCINM(intIndex).ToString)
                strFreeWordAimai = strClass1Aimai & strClass2Aimai & strCINMAimai & commonLogicHBK.ChangeStringForSearch(DataHBKB0203.PropAryCINaiyo(intIndex).ToString)
                strBikoAimai = commonLogicHBK.ChangeStringForSearch(DataHBKB0203.PropAryBIko1(intIndex).ToString) & _
                               commonLogicHBK.ChangeStringForSearch(DataHBKB0203.PropAryBIko2(intIndex).ToString) & _
                               commonLogicHBK.ChangeStringForSearch(DataHBKB0203.PropAryBIko3(intIndex).ToString) & _
                               commonLogicHBK.ChangeStringForSearch(DataHBKB0203.PropAryBIko4(intIndex).ToString) & _
                               commonLogicHBK.ChangeStringForSearch(DataHBKB0203.PropAryBIko5(intIndex).ToString)
                .Parameters("Class1Aimai").Value = strClass1Aimai           '分類１（あいまい）
                .Parameters("Class2Aimai").Value = strClass2Aimai           '分類２（あいまい）
                .Parameters("CINMAimai").Value = strCINMAimai               '名称（あいまい）
                .Parameters("FreeWordAimai").Value = strFreeWordAimai       'フリーワード（あいまい）
                .Parameters("BikoAimai").Value = strBikoAimai               'フリーテキスト（あいまい）

                .Parameters("RegDT").Value = DataHBKB0203.PropDtmSysDate    '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD             '登録者グループCD
                .Parameters("RegID").Value = PropUserId                     '登録者ID
                .Parameters("UpdateDT").Value = DataHBKB0203.PropDtmSysDate '最終更新日時
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
    ''' 新規ファイル管理番号取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0203">[IN]一括登録データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規ファイル番号取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewFileMngNmbSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                   ByVal Cn As NpgsqlConnection, _
                                                   ByVal dataHBKB0203 As DataHBKB0203) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = GET_NEXTVAL_FILEMNG_NO

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
    ''' ファイル管理テーブル新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0203">[IN]一括登録 文書データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ファイル管理テーブル新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertFileMngSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB0203 As DataHBKB0203, _
                                       ByVal intindex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try
            'SQL文(INSERT)
            strSQL = strInsertFileMngSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            '型をセット

            With Cmd.Parameters
                .Add(New NpgsqlParameter("FileMngNmb", NpgsqlTypes.NpgsqlDbType.Integer))                           'ファイル管理番号
                .Add(New NpgsqlParameter("FilePath", NpgsqlTypes.NpgsqlDbType.Varchar))                             'ファイル名
                .Add(New NpgsqlParameter("FileNM", NpgsqlTypes.NpgsqlDbType.Varchar))                               'ファイル名
                .Add(New NpgsqlParameter("Ext", NpgsqlTypes.NpgsqlDbType.Varchar))                                  '拡張子
                .Add(New NpgsqlParameter("HaikiKbn", NpgsqlTypes.NpgsqlDbType.Varchar))                             '廃棄区分
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                              '廃棄区分
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                             '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                                '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                           '最終更新者ID
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                              '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                             '最終更新者ID
            End With

            With Cmd
                .Parameters("FileMngNmb").Value = dataHBKB0203.PropIntFileMngNmb                                    'ファイル管理番号
                '.Parameters("FilePath").Value = PropFileStorageRootPath & PropFileManagePath & "\構成管理\" & dataHBKB0203.PropIntCINmb & "\" & dataHBKB0203.PropIntFileMngNmb          'ファイルパス
                .Parameters("FilePath").Value = PropFileManagePath & "\構成管理\" & dataHBKB0203.PropIntCINmb & "\" & dataHBKB0203.PropIntFileMngNmb          'ファイルパス
                .Parameters("FileNM").Value = Path.GetFileNameWithoutExtension(dataHBKB0203.PropAryFilePath(intindex))   'ファイル名
                .Parameters("Ext").Value = Path.GetExtension(dataHBKB0203.PropAryFilePath(intindex))                     '拡張子
                .Parameters("HaikiKbn").Value = HAIKIKBN_KADOU                                                      '廃棄区分
                .Parameters("RegDT").Value = dataHBKB0203.PropDtmSysDate                                            '登録日時                
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                                     '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                             '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0203.PropDtmSysDate                                         '最終更新日時                
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                                      '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                                          '最終更新者ID 
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
    ''' CI文書新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIシステム新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCIDocSql(ByRef Cmd As NpgsqlCommand, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKB0203 As DataHBKB0203, _
                                            ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""                      'SQL文
        Dim strShareteamNMAimai As String = ""         'フリーテキスト（あいまい）


        Try

            'SQL文(INSERT)
            strSQL = strInsertCIDocSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                        'CI番号
                .Add(New NpgsqlParameter("Version", NpgsqlTypes.NpgsqlDbType.Varchar))                      '版(手動)
                .Add(New NpgsqlParameter("CrateID", NpgsqlTypes.NpgsqlDbType.Varchar))                      '作成者ID
                .Add(New NpgsqlParameter("CrateNM", NpgsqlTypes.NpgsqlDbType.Varchar))                      '作成者名
                .Add(New NpgsqlParameter("CreateDT", NpgsqlTypes.NpgsqlDbType.Varchar))                     '作成年月日
                .Add(New NpgsqlParameter("LastUpID", NpgsqlTypes.NpgsqlDbType.Varchar))                     '最終更新者ID
                .Add(New NpgsqlParameter("LastUpNM", NpgsqlTypes.NpgsqlDbType.Varchar))                     '最終更新者名
                .Add(New NpgsqlParameter("LastUpDT", NpgsqlTypes.NpgsqlDbType.Varchar))                     '最終更新日時
                .Add(New NpgsqlParameter("FileMngNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'ファイル管理番号
                .Add(New NpgsqlParameter("ChargeID", NpgsqlTypes.NpgsqlDbType.Varchar))                     '文書責任者ID
                .Add(New NpgsqlParameter("ChargeNM", NpgsqlTypes.NpgsqlDbType.Varchar))                     '文書責任者名
                .Add(New NpgsqlParameter("OfferNM", NpgsqlTypes.NpgsqlDbType.Varchar))                      '文書提供者
                .Add(New NpgsqlParameter("ShareteamNM", NpgsqlTypes.NpgsqlDbType.Varchar))                  '文書配布先
                .Add(New NpgsqlParameter("DelDT", NpgsqlTypes.NpgsqlDbType.Varchar))                        '文書廃棄年月日
                .Add(New NpgsqlParameter("DelReason", NpgsqlTypes.NpgsqlDbType.Varchar))                    '文書廃棄理由
                .Add(New NpgsqlParameter("ShareteamNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))             '文書廃棄先(あいまい)
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                      '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                     '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                        '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                   '最終更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                      '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                     '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd

                .Parameters("CINmb").Value = dataHBKB0203.PropIntCINmb                                      'CI番号
                .Parameters("Version").Value = dataHBKB0203.PropAryVersion(intIndex)                        '版(手動)
                .Parameters("CrateID").Value = dataHBKB0203.PropAryCrateID(intIndex)                        '作成者ID
                .Parameters("CrateNM").Value = dataHBKB0203.PropAryCrateNM(intIndex)                        '作成者名
                .Parameters("CreateDT").Value = dataHBKB0203.PropAryCreateDT(intIndex)                      '作成年月日
                .Parameters("LastUpID").Value = dataHBKB0203.PropAryLastUpID(intIndex)                      '最終更新者ID
                .Parameters("LastUpNM").Value = dataHBKB0203.PropAryLastUpNM(intIndex)                      '最終更新者名
                .Parameters("LastUpDT").Value = dataHBKB0203.PropAryLastUpDT(intIndex)                      '最終更新日時
                .Parameters("FileMngNmb").Value = dataHBKB0203.PropIntFileMngNmb                            'ファイル管理番号
                .Parameters("ChargeID").Value = dataHBKB0203.PropAryChargeID(intIndex)                      '文書責任者ID
                .Parameters("ChargeNM").Value = dataHBKB0203.PropAryChargeNM(intIndex)                      '文書責任者名
                .Parameters("OfferNM").Value = dataHBKB0203.PropAryOfferNM(intIndex)                        '文書提供者
                .Parameters("ShareteamNM").Value = dataHBKB0203.PropAryShareteamNM(intIndex)                '文書配布先
                .Parameters("DelDT").Value = dataHBKB0203.PropAryDelDT(intIndex)                            '文書廃棄日時
                .Parameters("DelReason").Value = dataHBKB0203.PropAryDelReason(intIndex)                    '文書廃棄理由

                'あいまい検索用に変換を行う
                strShareteamNMAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0203.PropAryShareteamNM(intIndex))

                .Parameters("ShareteamNMAimai").Value = strShareteamNMAimai                                 '文書廃棄先(あいまい)
                .Parameters("RegDT").Value = dataHBKB0203.PropDtmSysDate                                    '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                             '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                     '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0203.PropDtmSysDate                                 '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                              '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                                  '最終更新者ID

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
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>登録理由履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertRegReasonRSql(ByRef Cmd As NpgsqlCommand, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB0203 As DataHBKB0203) As Boolean

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
                .Parameters("CINmb").Value = DataHBKB0203.PropIntCINmb                          'CI番号
                .Parameters("RirekiNo").Value = DataHBKB0203.PropIntRirekiNo                    '履歴番号
                .Parameters("RegReason").Value = DataHBKB0203.PropStrRegReason                  '登録理由
                .Parameters("RegDT").Value = DataHBKB0203.PropDtmSysDate                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                         '登録者ID
                .Parameters("UpdateDT").Value = DataHBKB0203.PropDtmSysDate                     '最終更新日時
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
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>原因リンク履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCauseLinkRSql(ByRef Cmd As NpgsqlCommand, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB0203 As DataHBKB0203) As Boolean

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
                .Parameters("CINmb").Value = DataHBKB0203.PropIntCINmb                          'CI番号
                .Parameters("RirekiNo").Value = DataHBKB0203.PropIntRirekiNo                    '履歴番号
                .Parameters("MngNmb").Value = CInt(dataHBKB0203.PropStrMngNmb)                  '管理番号
                .Parameters("ProcessKbn").Value = DataHBKB0203.PropStrProcessKbn                'プロセス区分
                .Parameters("RegDT").Value = DataHBKB0203.PropDtmSysDate                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                         '登録者ID
                .Parameters("UpdateDT").Value = DataHBKB0203.PropDtmSysDate                     '最終更新日時
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
    ''' CI共通情報履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCIInfoRSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB0203 As DataHBKB0203) As Boolean

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
                .Parameters("RirekiNo").Value = DataHBKB0203.PropIntRirekiNo                            '履歴番号
                .Parameters("CINmb").Value = DataHBKB0203.PropIntCINmb                                  'CI番号
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
    ''' CI文書履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI文書履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCIDocRSql(ByRef Cmd As NpgsqlCommand, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0203 As DataHBKB0203) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertCIDocRSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))                 '履歴番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("RirekiNo").Value = dataHBKB0203.PropIntRirekiNo                            '履歴番号
                .Parameters("CINmb").Value = dataHBKB0203.PropIntCINmb                                  'CI番号
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

End Class
