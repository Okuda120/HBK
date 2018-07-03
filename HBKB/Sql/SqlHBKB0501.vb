Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.IO

''' <summary>
''' 文書登録画面Sqlクラス
''' </summary>
''' <remarks>文書登録画面のSQLの作成・設定を行う
''' <para>作成情報：2012/06/26 s.tsuruta
''' <p>改訂情報:</p>v
''' </para></remarks>
Public Class SqlHBKB0501

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK


    '*************************
    '* SQL文宣言
    '*************************

    'CI共通情報取得（SELECT）SQL
    Private strSelectCIInfoSql As String = "SELECT " & vbCrLf & _
                                       " ct.CINmb " & vbCrLf & _
                                       ",ct.CIKbnCD " & vbCrLf & _
                                       ",ct.KindCD " & vbCrLf & _
                                       ",ct.Num " & vbCrLf & _
                                       ",ct.CIStatusCD " & vbCrLf & _
                                       ",ct.Class1 " & vbCrLf & _
                                       ",ct.Class2 " & vbCrLf & _
                                       ",ct.CINM " & vbCrLf & _
                                       ",cst.InfShareteamNM " & vbCrLf & _
                                       ",ct.CIOwnerCD " & vbCrLf & _
                                       ",gm.GroupNM " & vbCrLf & _
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
                                       "FROM CI_INFO_TB ct " & vbCrLf & _
                                       "LEFT JOIN CI_SYS_TB cst ON ct.CINmb = cst.CINmb " & vbCrLf & _
                                       "LEFT JOIN GRP_MTB gm ON ct.CIOwnerCD = gm.GroupCD " & vbCrLf & _
                                       "WHERE ct.CINmb = :CINmb "

    'CI共通情報履歴取得（SELECT）SQL
    Private strSelectCIInfoRSql As String = "SELECT " & vbCrLf & _
                                           " ct.CINmb " & vbCrLf & _
                                           ",ct.RirekiNo" & vbCrLf & _
                                           ",ct.CIKbnCD " & vbCrLf & _
                                           ",ct.KindCD " & vbCrLf & _
                                           ",ct.Num " & vbCrLf & _
                                           ",ct.CIStatusCD " & vbCrLf & _
                                           ",ct.Class1 " & vbCrLf & _
                                           ",ct.Class2 " & vbCrLf & _
                                           ",ct.CINM " & vbCrLf & _
                                           ",ct.CIOwnerCD " & vbCrLf & _
                                           ",gm.GroupNM " & vbCrLf & _
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
                                           "FROM CI_INFO_RTB ct " & vbCrLf & _
                                           "LEFT JOIN GRP_MTB gm ON ct.CIOwnerCD = gm.GroupCD " & vbCrLf & _
                                           "WHERE ct.CINmb = :CINmb " & vbCrLf & _
                                           "  AND ct.RirekiNo = :RirekiNo "


    '原因リンク取得（SELECT：新規／編集／参照）SQL
    Private strSelectCauseLinkSql As String = "SELECT " & vbCrLf & _
                                              " ct.RirekiNo " & vbCrLf & _
                                              ",CASE ct.ProcessKbn " & vbCrLf & _
                                              " WHEN :Kbn_Incident THEN :Kbn_Incident_NMR " & vbCrLf & _
                                              " WHEN :Kbn_Question THEN :Kbn_Question_NMR " & vbCrLf & _
                                              " WHEN :Kbn_Change THEN :Kbn_Change_NMR " & vbCrLf & _
                                              " WHEN :Kbn_Release THEN :Kbn_Release_NMR " & vbCrLf & _
                                              " ELSE '' END AS ProcessKbnNMR " & vbCrLf & _
                                              ",ct.ProcessKbn " & vbCrLf & _
                                              ",ct.MngNmb " & vbCrLf & _
                                              "FROM REGREASON_RTB rt " & vbCrLf & _
                                              "JOIN CAUSELINK_RTB ct ON rt.CINmb = ct.CINmb AND rt.RirekiNo = ct.RirekiNo " & vbCrLf & _
                                              "WHERE ct.CINmb = :CINmb " & vbCrLf & _
                                              "  AND ct.RirekiNo = (SELECT MAX(rt2.RirekiNo) FROM REGREASON_RTB rt2 WHERE rt2.CINmb = ct.CINmb) " & vbCrLf & _
                                              "ORDER BY ct.ProcessKbn, ct.MngNmb "

    '原因リンク取得（SELECT：履歴）SQL
    Private strSelectCauseLinkRSql As String = "SELECT " & vbCrLf & _
                                              " ct.RirekiNo " & vbCrLf & _
                                              ",CASE ct.ProcessKbn " & vbCrLf & _
                                              " WHEN :Kbn_Incident THEN :Kbn_Incident_NMR " & vbCrLf & _
                                              " WHEN :Kbn_Question THEN :Kbn_Question_NMR " & vbCrLf & _
                                              " WHEN :Kbn_Change THEN :Kbn_Change_NMR " & vbCrLf & _
                                              " WHEN :Kbn_Release THEN :Kbn_Release_NMR " & vbCrLf & _
                                              " ELSE '' END AS ProcessKbnNMR " & vbCrLf & _
                                              ",ct.ProcessKbn " & vbCrLf & _
                                              ",ct.MngNmb " & vbCrLf & _
                                              "FROM REGREASON_RTB rt " & vbCrLf & _
                                              "JOIN CAUSELINK_RTB ct ON rt.CINmb = ct.CINmb AND rt.RirekiNo = ct.RirekiNo " & vbCrLf & _
                                              "WHERE rt.CINmb = :CINmb " & vbCrLf & _
                                              "  AND rt.RirekiNo = :RirekiNo " & vbCrLf & _
                                              "ORDER BY ct.ProcessKbn, ct.MngNmb "


    '登録理由履歴情報取得（SELECT：新規／編集／参照）SQL
    Private strSelectRegReasonSql As String = "SELECT " & vbCrLf & _
                                              " rt.RirekiNo " & vbCrLf & _
                                              ",TO_CHAR(rt.RegDT,'YYYY/MM/DD HH24:MI') AS RegDT " & vbCrLf & _
                                              ",gm.GroupNM " & vbCrLf & _
                                              ",hm.HBKUsrNM " & vbCrLf & _
                                              ",rt.RegReason " & vbCrLf & _
                                              "FROM REGREASON_RTB rt " & vbCrLf & _
                                              "LEFT JOIN GRP_MTB gm ON rt.RegGrpCD = gm.GroupCD " & vbCrLf & _
                                              "LEFT JOIN HBKUSR_MTB hm ON rt.RegID = hm.HBKUsrID " & vbCrLf & _
                                              "WHERE rt.CINmb = :CINmb " & vbCrLf & _
                                              "ORDER BY rt.RirekiNo DESC "


    '登録理由履歴情報取得（SELECT：履歴）SQL
    Private strSelectRegReasonRSql As String = "SELECT " & vbCrLf & _
                                              " rt.RirekiNo " & vbCrLf & _
                                              ",TO_CHAR(rt.RegDT,'YYYY/MM/DD HH24:MI') AS RegDT " & vbCrLf & _
                                              ",gm.GroupNM " & vbCrLf & _
                                              ",hm.HBKUsrNM " & vbCrLf & _
                                              ",rt.RegReason " & vbCrLf & _
                                              "FROM REGREASON_RTB rt " & vbCrLf & _
                                              "LEFT JOIN GRP_MTB gm ON rt.RegGrpCD = gm.GroupCD " & vbCrLf & _
                                              "LEFT JOIN HBKUSR_MTB hm ON rt.RegID = hm.HBKUsrID " & vbCrLf & _
                                              "WHERE rt.CINmb = :CINmb " & vbCrLf & _
                                              "  AND rt.RirekiNo <= :RirekiNo " & vbCrLf & _
                                              "ORDER BY rt.RirekiNo DESC "

    'システム日付取得（SELECT）SQL
    Private strSelectSysDateSql As String = "SELECT Now() AS SysDate "


    '重複キー項目データ数取得（SELECT）SQL
    Private strSelectCountSameKeySql As String = "SELECT 1 " & vbCrLf & _
                                                 "FROM CI_INFO_TB ct " & vbCrLf & _
                                                 "WHERE ct.Class1 = :Class1 " & vbCrLf & _
                                                 "  AND ct.Class2 = :Class2 " & vbCrLf & _
                                                 "  AND ct.CINM = :CINM " & vbCrLf & _
                                                 "  AND ct.CINmb <> :CINmb " & vbCrLf & _
                                                 " AND ct.CIKbnCD = :CIKbnCD "


    '新規履歴番号取得（SELECT）SQL
    Private strSelectNewRirekiNoSql As String = "SELECT " & vbCrLf & _
                                                " COALESCE(MAX(ct.RirekiNo),0)+1 AS RirekiNo " & vbCrLf & _
                                                "FROM CI_INFO_RTB ct " & vbCrLf & _
                                                "WHERE ct.CINmb = :CINmb "

    'CI文書取得（SELECT）SQL
    Private strSelectCIDocSql As String = "SELECT " & vbCrLf & _
                                           " ct.CINmb " & vbCrLf & _
                                           ",ct.Version " & vbCrLf & _
                                           ",ct.CrateID " & vbCrLf & _
                                           ",ct.CrateNM " & vbCrLf & _
                                           ",CASE COALESCE(ct.CreateDT,'') WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(ct.CreateDT,'yyyymmdd'),'yyyy/mm/dd') END AS CreateDT " & vbCrLf & _
                                           ",ct.LastUpID " & vbCrLf & _
                                           ",ct.LastUpNM " & vbCrLf & _
                                           ",CASE WHEN ct.LastUpDT IS NULL" & vbCrLf & _
                                           " THEN '' " & vbCrLf & _
                                           " ELSE to_char(ct.LastUpDT,'YYYY/MM/DD HH24:MI') END AS LastUpDT" & vbCrLf & _
                                           ",ct.FileMngNmb " & vbCrLf & _
                                           ",ct.ChargeID " & vbCrLf & _
                                           ",ct.ChargeNM " & vbCrLf & _
                                           ",ct.ShareteamNM " & vbCrLf & _
                                           ",ct.OfferNM " & vbCrLf & _
                                           ",CASE COALESCE(ct.DelDT,'') WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(ct.DelDT,'yyyymmdd'),'yyyy/mm/dd') END AS DelDT " & vbCrLf & _
                                           ",ct.DelReason " & vbCrLf & _
                                           ",ct.ShareteamNMAimai " & vbCrLf & _
                                           ",ct.RegDT " & vbCrLf & _
                                           ",ct.RegGrpCD " & vbCrLf & _
                                           ",ct.RegID " & vbCrLf & _
                                           ",ct.UpdateDT " & vbCrLf & _
                                           ",ct.UpGrpCD " & vbCrLf & _
                                           ",ct.UpdateID " & vbCrLf & _
                                           "FROM CI_DOC_TB ct " & vbCrLf & _
                                           "WHERE ct.CINmb = :CINmb "

    'CI共通情報新規登録（INSERT）SQL
    Private strInsertCIInfoSql As String = "INSERT INTO CI_INFO_TB ( " & vbCrLf & _
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
                                           "VALUES ( " & vbCrLf & _
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

    'CI共通情報履歴新規登録（INSERT）SQL
    Private strInsertCIInfoRSql As String = "INSERT INTO CI_INFO_RTB ( " & vbCrLf & _
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


    'CI共通情報更新（UPDATE）SQL
    Private strUpdateCIInfoSql As String = "UPDATE CI_INFO_TB SET " & vbCrLf & _
                                           " CIKbnCD        = :CIKbnCD " & vbCrLf & _
                                           ",KindCD         = :KindCD " & vbCrLf & _
                                           ",Num            = :Num " & vbCrLf & _
                                           ",CIStatusCD     = :CIStatusCD " & vbCrLf & _
                                           ",Class1         = :Class1 " & vbCrLf & _
                                           ",Class2         = :Class2 " & vbCrLf & _
                                           ",CINM           = :CINM " & vbCrLf & _
                                           ",CIOwnerCD      = :CIOwnerCD " & vbCrLf & _
                                           ",CINaiyo        = :CINaiyo " & vbCrLf & _
                                           ",BIko1          = :BIko1 " & vbCrLf & _
                                           ",Biko2          = :Biko2 " & vbCrLf & _
                                           ",Biko3          = :Biko3 " & vbCrLf & _
                                           ",Biko4          = :Biko4 " & vbCrLf & _
                                           ",Biko5          = :Biko5 " & vbCrLf & _
                                           ",FreeFlg1       = :FreeFlg1 " & vbCrLf & _
                                           ",FreeFlg2       = :FreeFlg2 " & vbCrLf & _
                                           ",FreeFlg3       = :FreeFlg3 " & vbCrLf & _
                                           ",FreeFlg4       = :FreeFlg4 " & vbCrLf & _
                                           ",FreeFlg5       = :FreeFlg5 " & vbCrLf & _
                                           ",Class1Aimai    = :Class1Aimai " & vbCrLf & _
                                           ",Class2Aimai    = :Class2Aimai " & vbCrLf & _
                                           ",CINMAimai      = :CINMAimai " & vbCrLf & _
                                           ",FreeWordAimai  = :FreeWordAimai " & vbCrLf & _
                                           ",BikoAimai      = :BikoAimai " & vbCrLf & _
                                           ",UpdateDT       = :UpdateDT " & vbCrLf & _
                                           ",UpGrpCD        = :UpGrpCD " & vbCrLf & _
                                           ",UpdateID       = :UpdateID " & vbCrLf & _
                                           "WHERE CINmb=:CINmb "


    '登録理由履歴新規登録（INSERT）SQL
    Private strInsertRegReasonRSql As String = "INSERT INTO REGREASON_RTB ( " & vbCrLf & _
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
                                               "VALUES ( " & vbCrLf & _
                                               " :CINmb " & vbCrLf & _
                                               ",:RirekiNo " & vbCrLf & _
                                               ",:RegReason " & vbCrLf & _
                                               ",NULL " & vbCrLf & _
                                               ",NULL " & vbCrLf & _
                                               ",:RegDT " & vbCrLf & _
                                               ",:RegGrpCD " & vbCrLf & _
                                               ",:RegID " & vbCrLf & _
                                               ",:UpdateDT " & vbCrLf & _
                                               ",:UpGrpCD " & vbCrLf & _
                                               ",:UpdateID " & vbCrLf & _
                                               ") "

    '原因リンク履歴新規登録（INSERT）SQL
    Private strInsertCauseLinkRSql As String = "INSERT INTO CAUSELINK_RTB ( " & vbCrLf & _
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
                                               "VALUES ( " & vbCrLf & _
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


    'CI文書履歴取得（SELECT）SQL
    Private strSelectCIDocRSql As String = "SELECT " & vbCrLf & _
                                       " ct.CINmb " & vbCrLf & _
                                       ",ct.RirekiNo " & vbCrLf & _
                                       ",ct.Version " & vbCrLf & _
                                       ",ct.CrateID " & vbCrLf & _
                                       ",ct.CrateNM " & vbCrLf & _
                                       ",CASE COALESCE(ct.CreateDT,'') WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(ct.CreateDT,'yyyymmdd'),'yyyy/mm/dd') END AS CreateDT " & vbCrLf & _
                                       ",ct.LastUpID " & vbCrLf & _
                                       ",ct.LastUpNM " & vbCrLf & _
                                       ",to_char(ct.LastUpDT,'YYYY/MM/DD HH24:MI') as LastUpDT " & vbCrLf & _
                                       ",ct.FileMngNmb " & vbCrLf & _
                                       ",ct.ChargeID " & vbCrLf & _
                                       ",ct.ChargeNM " & vbCrLf & _
                                       ",ct.ShareteamNM " & vbCrLf & _
                                       ",ct.OfferNM " & vbCrLf & _
                                       ",CASE COALESCE(ct.DelDT,'') WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(ct.DelDT,'yyyymmdd'),'yyyy/mm/dd') END AS DelDT " & vbCrLf & _
                                       ",ct.DelReason " & vbCrLf & _
                                       ",ct.ShareteamNMAimai " & vbCrLf & _
                                       ",ct.RegDT " & vbCrLf & _
                                       ",ct.RegGrpCD " & vbCrLf & _
                                       ",ct.RegID " & vbCrLf & _
                                       ",ct.UpdateDT " & vbCrLf & _
                                       ",ct.UpGrpCD " & vbCrLf & _
                                       ",ct.UpdateID " & vbCrLf & _
                                       "FROM CI_DOC_RTB ct " & vbCrLf & _
                                       "WHERE ct.CINmb = :CINmb " & vbCrLf & _
                                       "and ct.RirekiNo = :RirekiNo"

    'エンドユーザーマスタ取得（SELECT）SQL
    Private strSelectEndUserMastaSql As String = "SELECT " & vbCrLf & _
                                                " em.EndUsrID" & vbCrLf & _
                                                ",em.EndUsrNM " & vbCrLf & _
                                                "FROM ENDUSR_MTB em " & vbCrLf & _
                                                "WHERE em.EndUsrID = :EndUsrID"

    'ファイル管理テーブル取得（SELECT）SQL
    Private strSelectFileMngSql As String = "SELECT " & vbCrLf & _
                                            " ft.FileMngNmb" & vbCrLf & _
                                            ",ft.FilePath" & vbCrLf & _
                                            ",ft.FileNM" & vbCrLf & _
                                            ",ft.Ext" & vbCrLf & _
                                            ",ft.HaikiKbn" & vbCrLf & _
                                            ",ft.RegDT" & vbCrLf & _
                                            ",ft.RegGrpCD" & vbCrLf & _
                                            ",ft.RegID" & vbCrLf & _
                                            ",ft.UpdateDT" & vbCrLf & _
                                            ",ft.UpGrpCD" & vbCrLf & _
                                            ",ft.UpdateID" & vbCrLf & _
                                            "FROM file_mng_tb ft " & vbCrLf & _
                                            "WHERE ft.FileMngNmb = :FileMngNmb "


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
                                               ",CASE :CreateDT WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(:CreateDT, 'YYYY/MM/DD'),'YYYYMMDD') END " & vbCrLf & _
                                               ",:LastUpID " & vbCrLf & _
                                               ",:LastUpNM " & vbCrLf & _
                                               ",CASE WHEN :LastUpDT IS NULL" & vbCrLf & _
                                               " THEN NULL " & vbCrLf & _
                                               " ELSE TO_TIMESTAMP(:LastUpDT,'YYYY/MM/DD HH24:MI') END " & vbCrLf & _
                                               ",:FileMngNmb " & vbCrLf &
                                               ",:ChargeID " & vbCrLf & _
                                               ",:ChargeNM " & vbCrLf & _
                                               ",:ShareteamNM " & vbCrLf & _
                                               ",:OfferNM " & vbCrLf & _
                                               ",CASE :DelDT WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(:DelDT, 'YYYY/MM/DD'),'YYYYMMDD') END " & vbCrLf & _
                                               ",:DelReason " & vbCrLf & _
                                               ",:ShareteamNMAimai " & vbCrLf & _
                                               ",:RegDT " & vbCrLf & _
                                               ",:RegGrpCD " & vbCrLf & _
                                               ",:RegID " & vbCrLf & _
                                               ",:UpdateDT " & vbCrLf & _
                                               ",:UpGrpCD " & vbCrLf & _
                                               ",:UpdateID " & vbCrLf & _
                                             ") "

    'CI文書履歴テーブルinsert
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


    'CI文書更新（UPDATE）SQL
    Private strUpdateCIDocSql As String = "UPDATE CI_DOC_TB SET " & vbCrLf & _
                                               " CINmb = :CINmb" & vbCrLf & _
                                               ",Version = :Version" & vbCrLf & _
                                               ",CrateID = :CrateID" & vbCrLf & _
                                               ",CrateNM = :CrateNM" & vbCrLf & _
                                               ",CreateDT = CASE COALESCE(:CreateDT,'') WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(:CreateDT, 'YYYY/MM/DD'),'YYYYMMDD') END " & vbCrLf & _
                                               ",LastUpID = :LastUpID" & vbCrLf & _
                                               ",LastUpNM = :LastUpNM" & vbCrLf & _
                                               ",LastUpDT = TO_TIMESTAMP(:LastUpDT,'yyyy/mm/dd hh24:min')" & vbCrLf & _
                                               ",FileMngNmb = :FileMngNmb" & vbCrLf & _
                                               ",ChargeID = :ChargeID" & vbCrLf & _
                                               ",ChargeNM = :ChargeNM" & vbCrLf & _
                                               ",ShareteamNM = :ShareteamNM" & vbCrLf & _
                                               ",OfferNM = :OfferNM" & vbCrLf & _
                                               ",DelDT = CASE COALESCE(:DelDT,'') WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(:DelDT, 'YYYY/MM/DD'),'YYYYMMDD') END " & vbCrLf & _
                                               ",DelReason = :DelReason" & vbCrLf & _
                                               ",ShareteamNMAimai = :ShareteamNMAimai" & vbCrLf & _
                                               ",UpdateDT = :UpdateDT" & vbCrLf & _
                                               ",UpGrpCD = :UpGrpCD" & vbCrLf & _
                                               ",UpdateID = :UpdateID" & vbCrLf & _
                                               "WHERE CINmb=:CINmb "










    ''' <summary>
    ''' 【新規登録モード】新規ファイル管理番号取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0501">[IN]部所有機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規ファイル番号取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewFileMngNmbSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                   ByVal Cn As NpgsqlConnection, _
                                                   ByVal dataHBKB0501 As DataHBKB0501) As Boolean

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
    ''' 【共通】同じキー項目（分類１、分類２、名称）のデータ有無取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>同じキー項目（分類１、分類２、名称）のデータ有無取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCountSameKeySql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKB0501 As DataHBKB0501) As Boolean

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
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CI番号
                '[Add] 2012/08/02 y.ikushima START
                .Add(New NpgsqlParameter("CIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))      'CI種別
                '[Add] 2012/08/02 y.ikushima END
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("Class1").Value = DataHBKB0501.PropTxtClass1.Text               '分類１
                .Parameters("Class2").Value = DataHBKB0501.PropTxtClass2.Text               '分類２
                .Parameters("CINM").Value = DataHBKB0501.PropTxtCINM.Text                   '名称
                .Parameters("CINmb").Value = dataHBKB0501.PropIntCINmb                      'CI番号
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
    ''' 【新規登録モード】新規CI番号、サーバー日付取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規CI番号、サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewCINmbAndSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                   ByVal Cn As NpgsqlConnection, _
                                                   ByVal dataHBKB0501 As DataHBKB0501) As Boolean

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
    ''' 【編集／履歴モード】サーバー日付取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectSysDateSql

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
    ''' 【編集／参照モード】CI共通情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCIInfoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectCIInfoSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'CI番号
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))
            Adapter.SelectCommand.Parameters("CINmb").Value = DataHBKB0501.PropIntCINmb

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
    ''' 【履歴モード】CI共通情報履歴取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCIInfoRSql(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectCIInfoRSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'CI番号
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))
            Adapter.SelectCommand.Parameters("CINmb").Value = DataHBKB0501.PropIntCINmb
            '履歴番号
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))
            Adapter.SelectCommand.Parameters("RirekiNo").Value = dataHBKB0501.PropIntRirekiNo

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
    ''' 【編集／参照モード】CI文書取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI文書取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/22 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCIDocSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            'SQL文(SELECT)
            strSQL = strSelectCIDocSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'CI番号
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))
            Adapter.SelectCommand.Parameters("CINmb").Value = dataHBKB0501.PropIntCINmb

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
    ''' 【履歴モード】CI文書履歴取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI文書履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/22 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCIDocRSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            'SQL文(SELECT)
            strSQL = strSelectCIDocRSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'CI番号
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))
            Adapter.SelectCommand.Parameters("CINmb").Value = dataHBKB0501.PropIntCINmb

            '履歴番号
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))
            Adapter.SelectCommand.Parameters("RirekiNo").Value = dataHBKB0501.PropIntRirekiNo

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
    ''' 【編集／参照モード】原因リンク取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>原因リンク取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCauseLinkSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectCauseLinkSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("Kbn_Incident", NpgsqlTypes.NpgsqlDbType.Varchar))      'プロセス区分：インシデント
                .Add(New NpgsqlParameter("Kbn_Incident_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))  'プロセス区分名略称：インシデント
                .Add(New NpgsqlParameter("Kbn_Question", NpgsqlTypes.NpgsqlDbType.Varchar))      'プロセス区分：問題
                .Add(New NpgsqlParameter("Kbn_Question_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))  'プロセス区分名略称：問題
                .Add(New NpgsqlParameter("Kbn_Change", NpgsqlTypes.NpgsqlDbType.Varchar))        'プロセス区分：変更
                .Add(New NpgsqlParameter("Kbn_Change_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))    'プロセス区分名略称：変更
                .Add(New NpgsqlParameter("Kbn_Release", NpgsqlTypes.NpgsqlDbType.Varchar))       'プロセス区分：リリース
                .Add(New NpgsqlParameter("Kbn_Release_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))   'プロセス区分名略称：リリース
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))             'CI番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("Kbn_Incident").Value = PROCESS_TYPE_INCIDENT                        'プロセス区分：インシデント
                .Parameters("Kbn_Incident_NMR").Value = PROCESS_TYPE_INCIDENT_NAME_R             'プロセス区分名略称：インシデント
                .Parameters("Kbn_Question").Value = PROCESS_TYPE_QUESTION                        'プロセス区分：問題
                .Parameters("Kbn_Question_NMR").Value = PROCESS_TYPE_QUESTION_NAME_R             'プロセス区分名略称：問題
                .Parameters("Kbn_Change").Value = PROCESS_TYPE_CHANGE                            'プロセス区分：変更
                .Parameters("Kbn_Change_NMR").Value = PROCESS_TYPE_CHANGE_NAME_R                 'プロセス区分名略称：変更
                .Parameters("Kbn_Release").Value = PROCESS_TYPE_RELEASE                          'プロセス区分：リリース
                .Parameters("Kbn_Release_NMR").Value = PROCESS_TYPE_RELEASE_NAME_R               'プロセス区分名略称：リリース
                .Parameters("CINmb").Value = DataHBKB0501.PropIntCINmb                           'CI番号
            End With


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【履歴モード】原因リンク履歴取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>原因リンク履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCauseLinkRSql(ByRef Adapter As NpgsqlDataAdapter, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            'SQL文(SELECT)
            strSQL = strSelectCauseLinkRSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("Kbn_Incident", NpgsqlTypes.NpgsqlDbType.Varchar))      'プロセス区分：インシデント
                .Add(New NpgsqlParameter("Kbn_Incident_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))  'プロセス区分名略称：インシデント
                .Add(New NpgsqlParameter("Kbn_Question", NpgsqlTypes.NpgsqlDbType.Varchar))      'プロセス区分：問題
                .Add(New NpgsqlParameter("Kbn_Question_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))  'プロセス区分名略称：問題
                .Add(New NpgsqlParameter("Kbn_Change", NpgsqlTypes.NpgsqlDbType.Varchar))        'プロセス区分：変更
                .Add(New NpgsqlParameter("Kbn_Change_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))    'プロセス区分名略称：変更
                .Add(New NpgsqlParameter("Kbn_Release", NpgsqlTypes.NpgsqlDbType.Varchar))       'プロセス区分：リリース
                .Add(New NpgsqlParameter("Kbn_Release_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))   'プロセス区分名略称：リリース
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))             'CI番号
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))          '履歴番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("Kbn_Incident").Value = PROCESS_TYPE_INCIDENT                        'プロセス区分：インシデント
                .Parameters("Kbn_Incident_NMR").Value = PROCESS_TYPE_INCIDENT_NAME_R             'プロセス区分名略称：インシデント
                .Parameters("Kbn_Question").Value = PROCESS_TYPE_QUESTION                        'プロセス区分：問題
                .Parameters("Kbn_Question_NMR").Value = PROCESS_TYPE_QUESTION_NAME_R             'プロセス区分名略称：問題
                .Parameters("Kbn_Change").Value = PROCESS_TYPE_CHANGE                            'プロセス区分：変更
                .Parameters("Kbn_Change_NMR").Value = PROCESS_TYPE_CHANGE_NAME_R                 'プロセス区分名略称：変更
                .Parameters("Kbn_Release").Value = PROCESS_TYPE_RELEASE                          'プロセス区分：リリース
                .Parameters("Kbn_Release_NMR").Value = PROCESS_TYPE_RELEASE_NAME_R               'プロセス区分名略称：リリース
                .Parameters("CINmb").Value = DataHBKB0501.PropIntCINmb                           'CI番号
                .Parameters("RirekiNo").Value = DataHBKB0501.PropIntRirekiNo                     '履歴番号
            End With


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集／参照モード】登録理由履歴取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>登録理由履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectRegReasonSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            'SQL文(SELECT)
            strSQL = strSelectRegReasonSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'CI番号
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))
            Adapter.SelectCommand.Parameters("CINmb").Value = DataHBKB0501.PropIntCINmb

            '履歴番号
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))
            Adapter.SelectCommand.Parameters("RirekiNo").Value = DataHBKB0501.PropIntRirekiNo


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
    ''' 【履歴モード】登録理由履歴取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>登録理由履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectRegReasonRSql(ByRef Adapter As NpgsqlDataAdapter, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            'SQL文(SELECT)
            strSQL = strSelectRegReasonRSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'CI番号
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))
            Adapter.SelectCommand.Parameters("CINmb").Value = DataHBKB0501.PropIntCINmb
            '履歴番号
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))
            Adapter.SelectCommand.Parameters("RirekiNo").Value = dataHBKB0501.PropIntRirekiNo

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
    ''' 【新規登録モード】新規CI番号取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規CI番号取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/28 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewCINmbSql(ByRef Adapter As NpgsqlDataAdapter, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            'SQL文(SELECT)
            strSQL = GET_NEXTVAL_CI_NO & " AS CINmb "

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
    ''' 【新規登録モード】CI共通情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCIInfoSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB0501 As DataHBKB0501) As Boolean

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
                .Parameters("CINmb").Value = DataHBKB0501.PropIntCINmb                          'CI番号
                .Parameters("CIKbnCD").Value = CI_TYPE_DOC                                      'CI種別CD
                .Parameters("KindCD").Value = DataHBKB0501.PropCmbKind.SelectedValue            '種別CD
                .Parameters("Num").Value = dataHBKB0501.PropTxtNum.Text                         '番号
                .Parameters("CIStatusCD").Value = DataHBKB0501.PropCmbCIStatus.SelectedValue    'ステータスCD
                .Parameters("Class1").Value = DataHBKB0501.PropTxtClass1.Text                   '分類１
                .Parameters("Class2").Value = DataHBKB0501.PropTxtClass2.Text                   '分類２
                .Parameters("CINM").Value = DataHBKB0501.PropTxtCINM.Text                       '名称

                'CIオーナー名が入力されている場合のみCIオーナーCDに値をセット
                If DataHBKB0501.PropTxtCIOwnerNM.Text.Trim() <> "" Then
                    .Parameters("CIOwnerCD").Value = DataHBKB0501.PropLblCIOwnerCD.Text         'CIオーナーCD
                Else
                    .Parameters("CIOwnerCD").Value = ""
                End If

                .Parameters("CINaiyo").Value = DataHBKB0501.PropTxtCINaiyo.Text                 '説明
                .Parameters("BIko1").Value = DataHBKB0501.PropTxtBIko1.Text                     'フリーテキスト１
                .Parameters("Biko2").Value = DataHBKB0501.PropTxtBIko2.Text                     'フリーテキスト２
                .Parameters("BIko3").Value = DataHBKB0501.PropTxtBIko3.Text                     'フリーテキスト３
                .Parameters("Biko4").Value = DataHBKB0501.PropTxtBIko4.Text                     'フリーテキスト４
                .Parameters("Biko5").Value = DataHBKB0501.PropTxtBIko5.Text                     'フリーテキスト５

                'フリーフラグ１～５
                If DataHBKB0501.PropChkFreeFlg1.Checked = True Then
                    .Parameters("FreeFlg1").Value = FREE_FLG_ON                                 'フリーフラグ１
                Else
                    .Parameters("FreeFlg1").Value = FREE_FLG_OFF                                'フリーフラグ１
                End If
                If DataHBKB0501.PropChkFreeFlg2.Checked = True Then
                    .Parameters("FreeFlg2").Value = FREE_FLG_ON                                 'フリーフラグ２
                Else
                    .Parameters("FreeFlg2").Value = FREE_FLG_OFF                                'フリーフラグ２
                End If
                If DataHBKB0501.PropChkFreeFlg3.Checked = True Then
                    .Parameters("FreeFlg3").Value = FREE_FLG_ON                                 'フリーフラグ３
                Else
                    .Parameters("FreeFlg3").Value = FREE_FLG_OFF                                'フリーフラグ３
                End If
                If DataHBKB0501.PropChkFreeFlg4.Checked = True Then
                    .Parameters("FreeFlg4").Value = FREE_FLG_ON                                 'フリーフラグ４
                Else
                    .Parameters("FreeFlg4").Value = FREE_FLG_OFF                                'フリーフラグ４
                End If
                If DataHBKB0501.PropChkFreeFlg5.Checked = True Then
                    .Parameters("FreeFlg5").Value = FREE_FLG_ON                                 'フリーフラグ５
                Else
                    .Parameters("FreeFlg5").Value = FREE_FLG_OFF                                'フリーフラグ５
                End If

                'あいまい検索文字列設定
                '※フリーワード（あいまい）　：分類１～２、名称、説明を連結
                '※フリーテキスト（あいまい）：フリーテキスト１～５を連結
                strClass1Aimai = commonLogicHBK.ChangeStringForSearch(DataHBKB0501.PropTxtClass1.Text)
                strClass2Aimai = commonLogicHBK.ChangeStringForSearch(DataHBKB0501.PropTxtClass2.Text)
                strCINMAimai = commonLogicHBK.ChangeStringForSearch(DataHBKB0501.PropTxtCINM.Text)
                strFreeWordAimai = strClass1Aimai & strClass2Aimai & strCINMAimai & _
                                   commonLogicHBK.ChangeStringForSearch(DataHBKB0501.PropTxtCINaiyo.Text)
                strBikoAimai = commonLogicHBK.ChangeStringForSearch(DataHBKB0501.PropTxtBIko1.Text) & _
                               commonLogicHBK.ChangeStringForSearch(DataHBKB0501.PropTxtBIko2.Text) & _
                               commonLogicHBK.ChangeStringForSearch(DataHBKB0501.PropTxtBIko3.Text) & _
                               commonLogicHBK.ChangeStringForSearch(DataHBKB0501.PropTxtBIko4.Text) & _
                               commonLogicHBK.ChangeStringForSearch(DataHBKB0501.PropTxtBIko5.Text)
                .Parameters("Class1Aimai").Value = strClass1Aimai                               '分類１（あいまい）
                .Parameters("Class2Aimai").Value = strClass2Aimai                               '分類２（あいまい）
                .Parameters("CINMAimai").Value = strCINMAimai                                   '名称（あいまい）
                .Parameters("FreeWordAimai").Value = strFreeWordAimai                           'フリーワード（あいまい）
                .Parameters("BikoAimai").Value = strBikoAimai                                   'フリーテキスト（あいまい）

                .Parameters("RegDT").Value = dataHBKB0501.PropDtmSysDate                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0501.PropDtmSysDate                     '最終更新日時
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
    ''' 【新規登録モード】ファイル管理テーブル新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ファイル管理テーブル新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/28 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertFileMngSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB0501 As DataHBKB0501) As Boolean

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
                .Parameters("FileMngNmb").Value = dataHBKB0501.PropIntFileMngNmb                                    'ファイル管理番号
                '.Parameters("FilePath").Value = PropFileStorageRootPath & PropFileManagePath & "\構成管理\" & dataHBKB0501.PropIntCINmb & "\" & dataHBKB0501.PropIntFileMngNmb          'ファイルパス
                .Parameters("FilePath").Value = PropFileManagePath & "\構成管理\" & dataHBKB0501.PropIntCINmb & "\" & dataHBKB0501.PropIntFileMngNmb          'ファイルパス
                .Parameters("FileNM").Value = Path.GetFileNameWithoutExtension(dataHBKB0501.ProptxtFilePath.Text)   'ファイル名
                .Parameters("Ext").Value = Path.GetExtension(dataHBKB0501.ProptxtFilePath.Text)                     '拡張子
                .Parameters("HaikiKbn").Value = HAIKIKBN_KADOU                                                      '廃棄区分
                .Parameters("RegDT").Value = dataHBKB0501.PropDtmSysDate                                            '登録日時                
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                                     '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                             '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0501.PropDtmSysDate                                         '最終更新日時                
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
    ''' 【編集／参照／履歴モード】CI共通情報更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0501">[IN]文書ｓ登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateCIInfoSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB0501 As DataHBKB0501) As Boolean

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

            'SQL文(UPDATE)
            strSQL = strUpdateCIInfoSql

            'データアダプタに、SQLのUPDATE文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
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
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))            'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("CIKbnCD").Value = CI_TYPE_DOC                                      'CI種別CD
                .Parameters("KindCD").Value = DataHBKB0501.PropCmbKind.SelectedValue            '種別CD
                .Parameters("Num").Value = dataHBKB0501.PropTxtNum.Text                         '番号
                .Parameters("CIStatusCD").Value = DataHBKB0501.PropCmbCIStatus.SelectedValue    'ステータスCD
                .Parameters("Class1").Value = DataHBKB0501.PropTxtClass1.Text                   '分類１
                .Parameters("Class2").Value = DataHBKB0501.PropTxtClass2.Text                   '分類２
                .Parameters("CINM").Value = DataHBKB0501.PropTxtCINM.Text                       '名称

                'CIオーナー名が入力されている場合のみCIオーナーCDに値をセット
                If DataHBKB0501.PropTxtCIOwnerNM.Text.Trim() <> "" Then
                    .Parameters("CIOwnerCD").Value = DataHBKB0501.PropLblCIOwnerCD.Text         'CIオーナーCD
                Else
                    .Parameters("CIOwnerCD").Value = ""
                End If

                .Parameters("CINaiyo").Value = DataHBKB0501.PropTxtCINaiyo.Text                 '説明
                .Parameters("BIko1").Value = DataHBKB0501.PropTxtBIko1.Text                     'フリーテキスト１
                .Parameters("Biko2").Value = DataHBKB0501.PropTxtBIko2.Text                     'フリーテキスト２
                .Parameters("BIko3").Value = DataHBKB0501.PropTxtBIko3.Text                     'フリーテキスト３
                .Parameters("Biko4").Value = DataHBKB0501.PropTxtBIko4.Text                     'フリーテキスト４
                .Parameters("Biko5").Value = DataHBKB0501.PropTxtBIko5.Text                     'フリーテキスト５

                'フリーフラグ１～５
                If DataHBKB0501.PropChkFreeFlg1.Checked = True Then
                    .Parameters("FreeFlg1").Value = FREE_FLG_ON                                 'フリーフラグ１
                Else
                    .Parameters("FreeFlg1").Value = FREE_FLG_OFF                                'フリーフラグ１
                End If
                If DataHBKB0501.PropChkFreeFlg2.Checked = True Then
                    .Parameters("FreeFlg2").Value = FREE_FLG_ON                                 'フリーフラグ２
                Else
                    .Parameters("FreeFlg2").Value = FREE_FLG_OFF                                'フリーフラグ２
                End If
                If DataHBKB0501.PropChkFreeFlg3.Checked = True Then
                    .Parameters("FreeFlg3").Value = FREE_FLG_ON                                 'フリーフラグ３
                Else
                    .Parameters("FreeFlg3").Value = FREE_FLG_OFF                                'フリーフラグ３
                End If
                If DataHBKB0501.PropChkFreeFlg4.Checked = True Then
                    .Parameters("FreeFlg4").Value = FREE_FLG_ON                                 'フリーフラグ４
                Else
                    .Parameters("FreeFlg4").Value = FREE_FLG_OFF                                'フリーフラグ４
                End If
                If DataHBKB0501.PropChkFreeFlg5.Checked = True Then
                    .Parameters("FreeFlg5").Value = FREE_FLG_ON                                 'フリーフラグ５
                Else
                    .Parameters("FreeFlg5").Value = FREE_FLG_OFF                                'フリーフラグ５
                End If

                'あいまい検索文字列設定
                '※フリーワード（あいまい）　：分類１～２、名称、説明を連結
                '※フリーテキスト（あいまい）：フリーテキスト１～５を連結
                strClass1Aimai = commonLogicHBK.ChangeStringForSearch(DataHBKB0501.PropTxtClass1.Text)
                strClass2Aimai = commonLogicHBK.ChangeStringForSearch(DataHBKB0501.PropTxtClass2.Text)
                strCINMAimai = commonLogicHBK.ChangeStringForSearch(DataHBKB0501.PropTxtCINM.Text)
                strFreeWordAimai = strClass1Aimai & strClass2Aimai & strCINMAimai & _
                                   commonLogicHBK.ChangeStringForSearch(DataHBKB0501.PropTxtCINaiyo.Text)
                strBikoAimai = commonLogicHBK.ChangeStringForSearch(DataHBKB0501.PropTxtBIko1.Text) & _
                               commonLogicHBK.ChangeStringForSearch(DataHBKB0501.PropTxtBIko2.Text) & _
                               commonLogicHBK.ChangeStringForSearch(DataHBKB0501.PropTxtBIko3.Text) & _
                               commonLogicHBK.ChangeStringForSearch(DataHBKB0501.PropTxtBIko4.Text) & _
                               commonLogicHBK.ChangeStringForSearch(DataHBKB0501.PropTxtBIko5.Text)
                .Parameters("Class1Aimai").Value = strClass1Aimai                               '分類１（あいまい）
                .Parameters("Class2Aimai").Value = strClass2Aimai                               '分類２（あいまい）
                .Parameters("CINMAimai").Value = strCINMAimai                                   '名称（あいまい）
                .Parameters("FreeWordAimai").Value = strFreeWordAimai                           'フリーワード（あいまい）
                .Parameters("BikoAimai").Value = strBikoAimai                                   'フリーテキスト（あいまい）

                .Parameters("UpdateDT").Value = dataHBKB0501.PropDtmSysDate                     '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                  '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                      '最終更新者ID
                .Parameters("CINmb").Value = dataHBKB0501.PropIntCINmb                          'CI番号
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
    ''' 【新規登録モード】CI文書新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI文書新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/27 s/tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCIDocSql(ByRef Cmd As NpgsqlCommand, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Dim strLastUpTimeStamp As String = ""         '最終更新日時
        Dim strShareteamNMAimai As String = ""         'フリーテキスト（あいまい）


        Try
            'SQL文(INSERT)
            strSQL = strInsertCIDocSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            '型をセット
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
                .Add(New NpgsqlParameter("ShareteamNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))             '文書配布先(あいまい)
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                      '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                     '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                        '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                   '最終更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                      '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                     '最終更新者ID
            End With
            '値をセット
            With Cmd

                .Parameters("CINmb").Value = dataHBKB0501.PropIntCINmb                                      'CI番号
                .Parameters("Version").Value = dataHBKB0501.PropTxtVersion.Text                             '版(手動)
                .Parameters("CrateID").Value = dataHBKB0501.ProptxtCrateID.Text                             '作成者ID
                .Parameters("CrateNM").Value = dataHBKB0501.ProptxtCrateNM.Text                             '作成者名
                .Parameters("CreateDT").Value = dataHBKB0501.PropDtpCreateDT.txtDate.Text                   '作成年月日
                .Parameters("LastUpID").Value = dataHBKB0501.ProptxtLastUpID.Text                           '最終更新者ID
                .Parameters("LastUpNM").Value = dataHBKB0501.ProptxtLastUpNM.Text                           '最終更新者名

                strLastUpTimeStamp = dataHBKB0501.PropDtpLastUpDT.txtDate.Text & " " & dataHBKB0501.PropTxtDateTime.PropTxtTime.Text
                If strLastUpTimeStamp = " " Then
                    .Parameters("LastUpDT").Value = DBNull.Value
                Else
                    .Parameters("LastUpDT").Value = strLastUpTimeStamp                                      '最終更新日時
                End If

                If dataHBKB0501.ProptxtFilePath.Text <> "" Then
                    .Parameters("FileMngNmb").Value = dataHBKB0501.PropIntFileMngNmb                            'ファイル管理番号
                Else
                    .Parameters("FileMngNmb").Value = DBNull.Value
                End If

                .Parameters("ChargeID").Value = dataHBKB0501.ProptxtChargeID.Text                           '文書責任者ID
                .Parameters("ChargeNM").Value = dataHBKB0501.ProptxtChargeNM.Text                           '文書責任者名
                .Parameters("OfferNM").Value = dataHBKB0501.ProptxtOfferNM.Text                             '文書提供者
                .Parameters("ShareteamNM").Value = dataHBKB0501.ProptxtShareteamNM.Text                     '文書配布先
                .Parameters("DelDT").Value = dataHBKB0501.PropDtpDelDT.txtDate.Text                         '文書廃棄日時
                .Parameters("DelReason").Value = dataHBKB0501.ProptxtDelReason.Text                         '文書廃棄理由

                'あいまい検索用に変換を行う
                strShareteamNMAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0501.ProptxtShareteamNM.Text)

                .Parameters("ShareteamNMAimai").Value = strShareteamNMAimai                                 '文書配布先(あいまい)
                .Parameters("RegDT").Value = dataHBKB0501.PropDtmSysDate                                    '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                             '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                     '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0501.PropDtmSysDate                                 '最終更新日時
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
    ''' 【編集／参照／履歴モード】CI文書更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI文書更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/28 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateCIDocSql(ByRef Cmd As NpgsqlCommand, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Dim strLastUpTimeStamp As String = ""         '最終更新日時
        Dim strShareteamNMAimai As String = ""         'フリーテキスト（あいまい）

        Try
            'SQL文(UPDATE)
            strSQL = strUpdateCIDocSql

            'データアダプタに、SQLのUPDATE文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            '型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
                .Add(New NpgsqlParameter("Version", NpgsqlTypes.NpgsqlDbType.Varchar))              '版(手動)
                .Add(New NpgsqlParameter("CrateID", NpgsqlTypes.NpgsqlDbType.Varchar))              '作成者ID
                .Add(New NpgsqlParameter("CrateNM", NpgsqlTypes.NpgsqlDbType.Varchar))              '作成者名
                .Add(New NpgsqlParameter("CreateDT", NpgsqlTypes.NpgsqlDbType.Varchar))             '作成年月日
                .Add(New NpgsqlParameter("LastUpID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
                .Add(New NpgsqlParameter("LastUpNM", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者名
                .Add(New NpgsqlParameter("LastUpDT", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新日時
                .Add(New NpgsqlParameter("FileMngNmb", NpgsqlTypes.NpgsqlDbType.Integer))           'ファイル管理番号
                .Add(New NpgsqlParameter("ChargeID", NpgsqlTypes.NpgsqlDbType.Varchar))             '文書責任者ID
                .Add(New NpgsqlParameter("ChargeNM", NpgsqlTypes.NpgsqlDbType.Varchar))             '文書責任者名
                .Add(New NpgsqlParameter("OfferNM", NpgsqlTypes.NpgsqlDbType.Varchar))              '文書提供者
                .Add(New NpgsqlParameter("ShareteamNM", NpgsqlTypes.NpgsqlDbType.Varchar))          '文書配布先
                .Add(New NpgsqlParameter("DelDT", NpgsqlTypes.NpgsqlDbType.Varchar))                '文書廃棄年月日
                .Add(New NpgsqlParameter("DelReason", NpgsqlTypes.NpgsqlDbType.Varchar))            '文書廃棄理由
                .Add(New NpgsqlParameter("ShareteamNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))     '文書廃棄先(あいまい)
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '最終更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
            End With
            '値をセット
            With Cmd
                .Parameters("CINmb").Value = dataHBKB0501.PropIntCINmb                              'CI番号
                .Parameters("Version").Value = dataHBKB0501.PropTxtVersion.Text                     '版(手動)
                .Parameters("CrateID").Value = dataHBKB0501.ProptxtCrateID.Text                     '作成者ID
                .Parameters("CrateNM").Value = dataHBKB0501.ProptxtCrateNM.Text                     '作成者名
                .Parameters("CreateDT").Value = dataHBKB0501.PropDtpCreateDT.txtDate.Text           '作成年月日
                .Parameters("LastUpID").Value = dataHBKB0501.ProptxtLastUpID.Text                   '最終更新者ID
                .Parameters("LastUpNM").Value = dataHBKB0501.ProptxtLastUpNM.Text                   '最終更新者名

                strLastUpTimeStamp = dataHBKB0501.PropDtpLastUpDT.txtDate.Text & " " & dataHBKB0501.PropTxtDateTime.PropTxtTime.Text
                If strLastUpTimeStamp = " " Then
                    .Parameters("LastUpDT").Value = DBNull.Value
                Else
                    .Parameters("LastUpDT").Value = strLastUpTimeStamp                              '最終更新日時
                End If

                '履歴にファイル管理番号が登録されている場合その番号を引き継ぐ
                If dataHBKB0501.ProptxtFilePath.Text <> "" Then
                    .Parameters("FileMngNmb").Value = dataHBKB0501.PropIntFileMngNmb                    'ファイル管理番号
                Else
                    .Parameters("FileMngNmb").Value = dataHBKB0501.PropDtCIDoc.Rows(0).Item("FileMngNmb")
                End If

                .Parameters("ChargeID").Value = dataHBKB0501.ProptxtChargeID.Text                   '文書責任者ID
                .Parameters("ChargeNM").Value = dataHBKB0501.ProptxtChargeNM.Text                   '文書責任者名
                .Parameters("OfferNM").Value = dataHBKB0501.ProptxtOfferNM.Text                     '文書提供者
                .Parameters("ShareteamNM").Value = dataHBKB0501.ProptxtShareteamNM.Text             '文書配布先
                .Parameters("DelDT").Value = dataHBKB0501.PropDtpDelDT.txtDate.Text                 '文書廃棄年月日
                .Parameters("DelReason").Value = dataHBKB0501.ProptxtDelReason.Text                 '文書廃棄理由

                strShareteamNMAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0501.ProptxtShareteamNM.Text)
                .Parameters("ShareteamNMAimai").Value = strShareteamNMAimai                         '文書配布先(あいまい)
                .Parameters("UpdateDT").Value = dataHBKB0501.PropDtmSysDate                         '最終更新者ID
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID

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
    ''' 【共通】新規履歴番号取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規履歴番号取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/28 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewRirekiNoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            'SQL文(SELECT)
            strSQL = strSelectNewRirekiNoSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With
            '値をセット
            With Adapter.SelectCommand
                .Parameters("CINmb").Value = DataHBKB0501.PropIntCINmb                                  'CI番号
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
    ''' 【共通】ファイル管理テーブル取得SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規履歴番号取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/26 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectFileMngNmbSql(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            'SQL文(SELECT)
            strSQL = strSelectFileMngSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("FileMngNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'ファイル管理番号
            End With
            '値をセット
            With Adapter.SelectCommand
                .Parameters("FileMngNmb").Value = dataHBKB0501.PropDtCIDoc.Rows(0).Item("FileMngNmb")       'ファイル管理番号
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
    ''' 【共通】CI共通情報履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/28 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCIInfoRSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try
            'SQL文(INSERT)
            strSQL = strInsertCIInfoRSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            '型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))                 '履歴番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With
            '値をセット
            With Cmd
                .Parameters("RirekiNo").Value = DataHBKB0501.PropIntRirekiNo                            '履歴番号
                .Parameters("CINmb").Value = DataHBKB0501.PropIntCINmb                                  'CI番号
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
    ''' 【共通】CI文書履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI文書履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/28 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCIDocRSql(ByRef Cmd As NpgsqlCommand, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try
            'SQL文(INSERT)
            strSQL = strInsertCIDocRSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            '型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))                 '履歴番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With
            '値をセット
            With Cmd
                .Parameters("RirekiNo").Value = dataHBKB0501.PropIntRirekiNo                            '履歴番号
                .Parameters("CINmb").Value = dataHBKB0501.PropIntCINmb                                  'CI番号
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
    ''' 【共通】登録理由履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>登録理由履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertRegReasonRSql(ByRef Cmd As NpgsqlCommand, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB0501 As DataHBKB0501) As Boolean

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
                .Parameters("CINmb").Value = DataHBKB0501.PropIntCINmb                          'CI番号
                .Parameters("RirekiNo").Value = DataHBKB0501.PropIntRirekiNo                    '履歴番号
                .Parameters("RegReason").Value = DataHBKB0501.PropStrRegReason                  '登録理由
                .Parameters("RegDT").Value = DataHBKB0501.PropDtmSysDate                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                         '登録者ID
                .Parameters("UpdateDT").Value = DataHBKB0501.PropDtmSysDate                     '最終更新日時
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
    ''' 【共通】原因リンク履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>原因リンク履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCauseLinkRSql(ByRef Cmd As NpgsqlCommand, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB0501 As DataHBKB0501) As Boolean

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
                .Parameters("CINmb").Value = DataHBKB0501.PropIntCINmb                          'CI番号
                .Parameters("RirekiNo").Value = DataHBKB0501.PropIntRirekiNo                    '履歴番号
                .Parameters("MngNmb").Value = DataHBKB0501.PropRowReg.Item("MngNmb")            '管理番号
                .Parameters("ProcessKbn").Value = DataHBKB0501.PropRowReg.Item("ProcessKbn")    'プロセス区分
                .Parameters("RegDT").Value = DataHBKB0501.PropDtmSysDate                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                         '登録者ID
                .Parameters("UpdateDT").Value = DataHBKB0501.PropDtmSysDate                     '最終更新日時
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


End Class
