Imports Common
Imports CommonHBK
Imports Npgsql


''' <summary>
''' 部所有機器登録画面Sqlクラス
''' </summary>
''' <remarks>部所有機器登録画面のSQLの作成・設定を行う
''' <para>作成情報：2012/07/11 s.tsuruta
''' <p>改訂情報:</p>v
''' </para></remarks>
Public Class SqlHBKB1301

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK


    '*************************
    '* SQL文宣言
    '*************************
    'ソフトマスタ取得（SELECT）SQL
    Private strSelectSoftMastaSql As String = "SELECT " & vbCrLf & _
                                       " sm.SoftCD AS ID " & vbCrLf & _
                                       ",sm.SoftNM AS Text " & vbCrLf & _
                                       "FROM SOFT_MTB sm " & vbCrLf & _
                                       "WHERE sm.JtiFlg = '0' "

    '機器ステータスマスタ取得（SELECT）SQL
    Private strSelectKikiStateMastaSql As String = "SELECT " & vbCrLf & _
                                       " km.KikiStateCD AS ID " & vbCrLf & _
                                       ",km.KikiStateNM AS Text " & vbCrLf & _
                                       "FROM KIKISTATE_MTB km " & vbCrLf & _
                                       "WHERE km.JtiFlg = '0' "


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
                                                 "  AND ct.CIKbnCD = :CIKbnCD" & vbCrLf


    '新規履歴番号取得（SELECT）SQL
    Private strSelectNewRirekiNoSql As String = "SELECT " & vbCrLf & _
                                                " COALESCE(MAX(ct.RirekiNo),0)+1 AS RirekiNo " & vbCrLf & _
                                                "FROM CI_INFO_RTB ct " & vbCrLf & _
                                                "WHERE ct.CINmb = :CINmb "

    'CI部所有機器取得（SELECT）SQL
    Private strSelectCIBuySql As String = "SELECT " & vbCrLf & _
                                           " ct.CINmb " & vbCrLf & _
                                           ",ct.Kataban " & vbCrLf & _
                                           ",ct.Aliau " & vbCrLf & _
                                           ",ct.Serial " & vbCrLf & _
                                           ",ct.MacAddress1" & vbCrLf & _
                                           ",ct.MacAddress2 " & vbCrLf & _
                                           ",ct.ZooKbn " & vbCrLf & _
                                           ",ct.OSNM " & vbCrLf & _
                                           ",ct.AntiVirusSoftNM " & vbCrLf & _
                                           ",ct.DNSRegCD " & vbCrLf & _
                                           ",ct.NIC1 " & vbCrLf & _
                                           ",ct.NIC2 " & vbCrLf & _
                                           ",CASE COALESCE(ct.ConnectDT,'') WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(ct.ConnectDT,'yyyymmdd'),'yyyy/mm/dd') END AS ConnectDT " & vbCrLf & _
                                           ",CASE COALESCE(ct.ExpirationDT,'') WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(ct.ExpirationDT,'yyyymmdd'),'yyyy/mm/dd') END AS ExpirationDT " & vbCrLf & _
                                           ",CASE COALESCE(ct.DeletDT,'') WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(ct.DeletDT,'yyyymmdd'),'yyyy/mm/dd') END AS DeletDT " & vbCrLf & _
                                           ",CASE COALESCE(ct.LastInfoDT,'') WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(ct.LastInfoDT,'yyyymmdd'),'yyyy/mm/dd') END AS LastInfoDT " & vbCrLf & _
                                           ",ct.ConectReason " & vbCrLf & _
                                           ",CASE COALESCE(ct.ExpirationUPDT,'') WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(ct.ExpirationUPDT,'yyyymmdd'),'yyyy/mm/dd') END AS ExpirationUPDT " & vbCrLf & _
                                           ",CASE COALESCE(ct.InfoDT,'') WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(ct.InfoDT,'yyyymmdd'),'yyyy/mm/dd') END AS InfoDT " & vbCrLf & _
                                           ",ct.NumInfoKbn " & vbCrLf & _
                                           ",ct.SealSendkbn " & vbCrLf & _
                                           ",ct.AntiVirusSofCheckKbn " & vbCrLf & _
                                           ",CASE COALESCE(ct.AntiVirusSofCheckDT,'') WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(ct.AntiVirusSofCheckDT,'yyyymmdd'),'yyyy/mm/dd') END AS AntiVirusSofCheckDT " & vbCrLf & _
                                           ",ct.BusyoKikiBiko " & vbCrLf & _
                                           ",ct.ManageKyokuNM " & vbCrLf & _
                                           ",ct.ManageBusyoNM " & vbCrLf & _
                                           ",ct.WorkFromNmb " & vbCrLf & _
                                           ",ct.IPUseCD " & vbCrLf & _
                                           ",ct.FixedIP " & vbCrLf & _
                                           ",ct.UsrID " & vbCrLf & _
                                           ",ct.UsrNM " & vbCrLf & _
                                           ",ct.UsrCompany " & vbCrLf & _
                                           ",ct.UsrKyokuNM " & vbCrLf & _
                                           ",ct.UsrBusyoNM " & vbCrLf & _
                                           ",ct.UsrTel " & vbCrLf & _
                                           ",ct.UsrMailAdd " & vbCrLf & _
                                           ",ct.UsrContact " & vbCrLf & _
                                           ",ct.UsrRoom " & vbCrLf & _
                                           ",ct.SetKyokuNM " & vbCrLf & _
                                           ",ct.SetBusyoNM " & vbCrLf & _
                                           ",ct.SetRoom " & vbCrLf & _
                                           ",ct.SetBuil " & vbCrLf & _
                                           ",ct.SetFloor " & vbCrLf & _
                                           ",ct.SerialAimai " & vbCrLf & _
                                           ",ct.ManageBusyoNMAimai " & vbCrLf & _
                                           ",ct.UsrIDAimai " & vbCrLf & _
                                           ",ct.UsrBusyoNMAimai " & vbCrLf & _
                                           ",ct.SetBusyoNMAimai " & vbCrLf & _
                                           ",ct.SetRoomAimai " & vbCrLf & _
                                           ",ct.AntiVirusSofCheckDT " & vbCrLf & _
                                           ",ct.SetFloorAimai " & vbCrLf & _
                                           ",ct.RegDT " & vbCrLf & _
                                           ",ct.RegGrpCD " & vbCrLf & _
                                           ",ct.RegID " & vbCrLf & _
                                           ",ct.UpdateDT " & vbCrLf & _
                                           ",ct.UpGrpCD " & vbCrLf & _
                                           ",ct.UpdateID " & vbCrLf & _
                                           "FROM CI_BUY_TB ct " & vbCrLf & _
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
                                           ",LPAD(:Num, 5, '0') " & vbCrLf & _
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
                                           ",Num            = LPAD(:Num, 5, '0') " & vbCrLf & _
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

    'CI部所有機器履歴取得（SELECT）SQL
    Private strSelectCIBuyRSql As String = "SELECT " & vbCrLf & _
                                           " ct.CINmb " & vbCrLf & _
                                           ",ct.RirekiNo " & vbCrLf & _
                                           ",ct.Kataban " & vbCrLf & _
                                           ",ct.Aliau " & vbCrLf & _
                                           ",ct.Serial " & vbCrLf & _
                                           ",ct.MacAddress1" & vbCrLf & _
                                           ",ct.MacAddress2 " & vbCrLf & _
                                           ",ct.ZooKbn " & vbCrLf & _
                                           ",ct.OSNM " & vbCrLf & _
                                           ",ct.AntiVirusSoftNM " & vbCrLf & _
                                           ",ct.DNSRegCD " & vbCrLf & _
                                           ",ct.NIC1 " & vbCrLf & _
                                           ",ct.NIC2 " & vbCrLf & _
                                           ",CASE COALESCE(ct.ConnectDT,'') WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(ct.ConnectDT,'yyyymmdd'),'yyyy/mm/dd') END AS ConnectDT " & vbCrLf & _
                                           ",CASE COALESCE(ct.ExpirationDT,'') WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(ct.ExpirationDT,'yyyymmdd'),'yyyy/mm/dd') END AS ExpirationDT " & vbCrLf & _
                                           ",CASE COALESCE(ct.DeletDT,'') WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(ct.DeletDT,'yyyymmdd'),'yyyy/mm/dd') END AS DeletDT " & vbCrLf & _
                                           ",CASE COALESCE(ct.LastInfoDT,'') WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(ct.LastInfoDT,'yyyymmdd'),'yyyy/mm/dd') END AS LastInfoDT " & vbCrLf & _
                                           ",ct.ConectReason " & vbCrLf & _
                                           ",CASE COALESCE(ct.ExpirationUPDT,'') WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(ct.ExpirationUPDT,'yyyymmdd'),'yyyy/mm/dd') END AS ExpirationUPDT " & vbCrLf & _
                                           ",CASE COALESCE(ct.InfoDT,'') WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(ct.InfoDT,'yyyymmdd'),'yyyy/mm/dd') END AS InfoDT " & vbCrLf & _
                                           ",ct.NumInfoKbn " & vbCrLf & _
                                           ",ct.SealSendkbn " & vbCrLf & _
                                           ",ct.AntiVirusSofCheckKbn " & vbCrLf & _
                                           ",CASE COALESCE(ct.AntiVirusSofCheckDT,'') WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(ct.AntiVirusSofCheckDT,'yyyymmdd'),'yyyy/mm/dd') END AS AntiVirusSofCheckDT " & vbCrLf & _
                                           ",ct.BusyoKikiBiko " & vbCrLf & _
                                           ",ct.ManageKyokuNM " & vbCrLf & _
                                           ",ct.ManageBusyoNM " & vbCrLf & _
                                           ",ct.WorkFromNmb " & vbCrLf & _
                                           ",ct.IPUseCD " & vbCrLf & _
                                           ",ct.FixedIP " & vbCrLf & _
                                           ",ct.UsrID " & vbCrLf & _
                                           ",ct.UsrNM " & vbCrLf & _
                                           ",ct.UsrCompany " & vbCrLf & _
                                           ",ct.UsrKyokuNM " & vbCrLf & _
                                           ",ct.UsrBusyoNM " & vbCrLf & _
                                           ",ct.UsrTel " & vbCrLf & _
                                           ",ct.UsrMailAdd " & vbCrLf & _
                                           ",ct.UsrContact " & vbCrLf & _
                                           ",ct.UsrRoom " & vbCrLf & _
                                           ",ct.SetKyokuNM " & vbCrLf & _
                                           ",ct.SetBusyoNM " & vbCrLf & _
                                           ",ct.SetRoom " & vbCrLf & _
                                           ",ct.SetBuil " & vbCrLf & _
                                           ",ct.SetFloor " & vbCrLf & _
                                           ",ct.SerialAimai " & vbCrLf & _
                                           ",ct.ManageBusyoNMAimai " & vbCrLf & _
                                           ",ct.UsrIDAimai " & vbCrLf & _
                                           ",ct.UsrBusyoNMAimai " & vbCrLf & _
                                           ",ct.SetBusyoNMAimai " & vbCrLf & _
                                           ",ct.SetRoomAimai " & vbCrLf & _
                                           ",ct.AntiVirusSofCheckDT " & vbCrLf & _
                                           ",ct.SetFloorAimai " & vbCrLf & _
                                           ",ct.RegDT " & vbCrLf & _
                                           ",ct.RegGrpCD " & vbCrLf & _
                                           ",ct.RegID " & vbCrLf & _
                                           ",ct.UpdateDT " & vbCrLf & _
                                           ",ct.UpGrpCD " & vbCrLf & _
                                           ",ct.UpdateID " & vbCrLf & _
                                           "FROM CI_BUY_RTB ct " & vbCrLf & _
                                           "WHERE ct.CINmb = :CINmb " & vbCrLf & _
                                           "and ct.RirekiNo = :RirekiNo"

    'CI部所有機器新規登録（INSERT）SQL
    Private strInsertCIBuySql As String = "INSERT INTO CI_BUY_TB ( " & vbCrLf & _
                                           " CINmb " & vbCrLf & _
                                           ",Kataban " & vbCrLf & _
                                           ",Aliau " & vbCrLf & _
                                           ",Serial " & vbCrLf & _
                                           ",MacAddress1" & vbCrLf & _
                                           ",MacAddress2 " & vbCrLf & _
                                           ",ZooKbn " & vbCrLf & _
                                           ",OSNM " & vbCrLf & _
                                           ",AntiVirusSoftNM " & vbCrLf & _
                                           ",DNSRegCD " & vbCrLf & _
                                           ",NIC1 " & vbCrLf & _
                                           ",NIC2 " & vbCrLf & _
                                           ",ConnectDT " & vbCrLf & _
                                           ",ExpirationDT " & vbCrLf & _
                                           ",DeletDT " & vbCrLf & _
                                           ",LastInfoDT " & vbCrLf & _
                                           ",ConectReason " & vbCrLf & _
                                           ",ExpirationUPDT " & vbCrLf & _
                                           ",InfoDT " & vbCrLf & _
                                           ",NumInfoKbn " & vbCrLf & _
                                           ",SealSendkbn " & vbCrLf & _
                                           ",AntiVirusSofCheckKbn " & vbCrLf & _
                                           ",AntiVirusSofCheckDT " & vbCrLf & _
                                           ",BusyoKikiBiko " & vbCrLf & _
                                           ",ManageKyokuNM " & vbCrLf & _
                                           ",ManageBusyoNM " & vbCrLf & _
                                           ",WorkFromNmb " & vbCrLf & _
                                           ",IPUseCD " & vbCrLf & _
                                           ",FixedIP " & vbCrLf & _
                                           ",UsrID " & vbCrLf & _
                                           ",UsrNM " & vbCrLf & _
                                           ",UsrCompany " & vbCrLf & _
                                           ",UsrKyokuNM " & vbCrLf & _
                                           ",UsrBusyoNM " & vbCrLf & _
                                           ",UsrTel " & vbCrLf & _
                                           ",UsrMailAdd " & vbCrLf & _
                                           ",UsrContact " & vbCrLf & _
                                           ",UsrRoom " & vbCrLf & _
                                           ",SetKyokuNM " & vbCrLf & _
                                           ",SetBusyoNM " & vbCrLf & _
                                           ",SetRoom " & vbCrLf & _
                                           ",SetBuil " & vbCrLf & _
                                           ",SetFloor " & vbCrLf & _
                                           ",SerialAimai " & vbCrLf & _
                                           ",ManageBusyoNMAimai " & vbCrLf & _
                                           ",UsrIDAimai " & vbCrLf & _
                                           ",UsrBusyoNMAimai " & vbCrLf & _
                                           ",SetBusyoNMAimai " & vbCrLf & _
                                           ",SetRoomAimai " & vbCrLf & _
                                           ",SetFloorAimai " & vbCrLf & _
                                           ",RegDT " & vbCrLf & _
                                           ",RegGrpCD " & vbCrLf & _
                                           ",RegID " & vbCrLf & _
                                           ",UpdateDT " & vbCrLf & _
                                           ",UpGrpCD " & vbCrLf & _
                                           ",UpdateID " & vbCrLf & _
                                           ") " & vbCrLf & _
                                           "VALUES ( " & vbCrLf & _
                                           " :CINmb " & vbCrLf & _
                                           ",:Kataban " & vbCrLf & _
                                           ",:Aliau " & vbCrLf & _
                                           ",:Serial " & vbCrLf & _
                                           ",:MacAddress1" & vbCrLf & _
                                           ",:MacAddress2 " & vbCrLf & _
                                           ",:ZooKbn " & vbCrLf & _
                                           ",:OSNM " & vbCrLf & _
                                           ",:AntiVirusSoftNM " & vbCrLf & _
                                           ",:DNSRegCD " & vbCrLf & _
                                           ",:NIC1 " & vbCrLf & _
                                           ",:NIC2 " & vbCrLf & _
                                           ",CASE :ConnectDT WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(:ConnectDT, 'YYYY/MM/DD'),'YYYYMMDD') END " & vbCrLf & _
                                           ",CASE :ExpirationDT WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(:ExpirationDT, 'YYYY/MM/DD'),'YYYYMMDD') END " & vbCrLf & _
                                           ",CASE :DeletDT WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(:DeletDT, 'YYYY/MM/DD'),'YYYYMMDD') END " & vbCrLf & _
                                           ",CASE :LastInfoDT WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(:LastInfoDT, 'YYYY/MM/DD'),'YYYYMMDD') END " & vbCrLf & _
                                           ",:ConectReason " & vbCrLf & _
                                           ",CASE :ExpirationUPDT WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(:ExpirationUPDT, 'YYYY/MM/DD'),'YYYYMMDD') END " & vbCrLf & _
                                           ",CASE :InfoDT WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(:InfoDT, 'YYYY/MM/DD'),'YYYYMMDD') END " & vbCrLf & _
                                           ",:NumInfoKbn " & vbCrLf & _
                                           ",:SealSendkbn " & vbCrLf & _
                                           ",:AntiVirusSofCheckKbn " & vbCrLf & _
                                           ",CASE :AntiVirusSofCheckDT WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(:AntiVirusSofCheckDT, 'YYYY/MM/DD'),'YYYYMMDD') END " & vbCrLf & _
                                           ",:BusyoKikiBiko " & vbCrLf & _
                                           ",:ManageKyokuNM " & vbCrLf & _
                                           ",:ManageBusyoNM " & vbCrLf & _
                                           ",:WorkFromNmb " & vbCrLf & _
                                           ",:IPUseCD " & vbCrLf & _
                                           ",:FixedIP " & vbCrLf & _
                                           ",:UsrID " & vbCrLf & _
                                           ",:UsrNM " & vbCrLf & _
                                           ",:UsrCompany " & vbCrLf & _
                                           ",:UsrKyokuNM " & vbCrLf & _
                                           ",:UsrBusyoNM " & vbCrLf & _
                                           ",:UsrTel " & vbCrLf & _
                                           ",:UsrMailAdd " & vbCrLf & _
                                           ",:UsrContact " & vbCrLf & _
                                           ",:UsrRoom " & vbCrLf & _
                                           ",:SetKyokuNM " & vbCrLf & _
                                           ",:SetBusyoNM " & vbCrLf & _
                                           ",:SetRoom " & vbCrLf & _
                                           ",:SetBuil " & vbCrLf & _
                                           ",:SetFloor " & vbCrLf & _
                                           ",:SerialAimai " & vbCrLf & _
                                           ",:ManageBusyoNMAimai " & vbCrLf & _
                                           ",:UsrIDAimai " & vbCrLf & _
                                           ",:UsrBusyoNMAimai " & vbCrLf & _
                                           ",:SetBusyoNMAimai " & vbCrLf & _
                                           ",:SetRoomAimai " & vbCrLf & _
                                           ",:SetFloorAimai " & vbCrLf & _
                                           ",:RegDT " & vbCrLf & _
                                           ",:RegGrpCD " & vbCrLf & _
                                           ",:RegID " & vbCrLf & _
                                           ",:UpdateDT " & vbCrLf & _
                                           ",:UpGrpCD " & vbCrLf & _
                                           ",:UpdateID " & vbCrLf & _
                                           ") "

    'CI部所有機器履歴テーブルinsert
    Private strInsertCIBuyRSql As String = "INSERT INTO CI_BUY_RTB ( " & vbCrLf & _
                                           " CINmb " & vbCrLf & _
                                           ",RirekiNo " & vbCrLf & _
                                           ",Kataban " & vbCrLf & _
                                           ",Aliau " & vbCrLf & _
                                           ",Serial " & vbCrLf & _
                                           ",MacAddress1" & vbCrLf & _
                                           ",MacAddress2 " & vbCrLf & _
                                           ",ZooKbn " & vbCrLf & _
                                           ",OSNM " & vbCrLf & _
                                           ",AntiVirusSoftNM " & vbCrLf & _
                                           ",DNSRegCD " & vbCrLf & _
                                           ",NIC1 " & vbCrLf & _
                                           ",NIC2 " & vbCrLf & _
                                           ",ConnectDT " & vbCrLf & _
                                           ",ExpirationDT " & vbCrLf & _
                                           ",DeletDT " & vbCrLf & _
                                           ",LastInfoDT " & vbCrLf & _
                                           ",ConectReason " & vbCrLf & _
                                           ",ExpirationUPDT " & vbCrLf & _
                                           ",InfoDT " & vbCrLf & _
                                           ",NumInfoKbn " & vbCrLf & _
                                           ",SealSendkbn " & vbCrLf & _
                                           ",AntiVirusSofCheckKbn " & vbCrLf & _
                                           ",AntiVirusSofCheckDT " & vbCrLf & _
                                           ",BusyoKikiBiko " & vbCrLf & _
                                           ",ManageKyokuNM " & vbCrLf & _
                                           ",ManageBusyoNM " & vbCrLf & _
                                           ",WorkFromNmb " & vbCrLf & _
                                           ",IPUseCD " & vbCrLf & _
                                           ",FixedIP " & vbCrLf & _
                                           ",UsrID " & vbCrLf & _
                                           ",UsrNM " & vbCrLf & _
                                           ",UsrCompany " & vbCrLf & _
                                           ",UsrKyokuNM " & vbCrLf & _
                                           ",UsrBusyoNM " & vbCrLf & _
                                           ",UsrTel " & vbCrLf & _
                                           ",UsrMailAdd " & vbCrLf & _
                                           ",UsrContact " & vbCrLf & _
                                           ",UsrRoom " & vbCrLf & _
                                           ",SetKyokuNM " & vbCrLf & _
                                           ",SetBusyoNM " & vbCrLf & _
                                           ",SetRoom " & vbCrLf & _
                                           ",SetBuil " & vbCrLf & _
                                           ",SetFloor " & vbCrLf & _
                                           ",SerialAimai " & vbCrLf & _
                                           ",ManageBusyoNMAimai " & vbCrLf & _
                                           ",UsrIDAimai " & vbCrLf & _
                                           ",UsrBusyoNMAimai " & vbCrLf & _
                                           ",SetBusyoNMAimai " & vbCrLf & _
                                           ",SetRoomAimai " & vbCrLf & _
                                           ",SetFloorAimai " & vbCrLf & _
                                           ",RegDT " & vbCrLf & _
                                           ",RegGrpCD " & vbCrLf & _
                                           ",RegID " & vbCrLf & _
                                           ",UpdateDT " & vbCrLf & _
                                           ",UpGrpCD " & vbCrLf & _
                                           ",UpdateID " & vbCrLf & _
                                           ") " & vbCrLf & _
                                           "  SELECT " & vbCrLf & _
                                           " ct.CINmb " & vbCrLf & _
                                           ",:RirekiNo " & vbCrLf & _
                                           ",ct.Kataban " & vbCrLf & _
                                           ",ct.Aliau " & vbCrLf & _
                                           ",ct.Serial " & vbCrLf & _
                                           ",ct.MacAddress1" & vbCrLf & _
                                           ",ct.MacAddress2 " & vbCrLf & _
                                           ",ct.ZooKbn " & vbCrLf & _
                                           ",ct.OSNM " & vbCrLf & _
                                           ",ct.AntiVirusSoftNM " & vbCrLf & _
                                           ",ct.DNSRegCD " & vbCrLf & _
                                           ",ct.NIC1 " & vbCrLf & _
                                           ",ct.NIC2 " & vbCrLf & _
                                           ",ct.ConnectDT " & vbCrLf & _
                                           ",ct.ExpirationDT " & vbCrLf & _
                                           ",ct.DeletDT " & vbCrLf & _
                                           ",ct.LastInfoDT " & vbCrLf & _
                                           ",ct.ConectReason " & vbCrLf & _
                                           ",ct.ExpirationUPDT " & vbCrLf & _
                                           ",ct.InfoDT " & vbCrLf & _
                                           ",ct.NumInfoKbn " & vbCrLf & _
                                           ",ct.SealSendkbn " & vbCrLf & _
                                           ",ct.AntiVirusSofCheckKbn " & vbCrLf & _
                                           ",ct.AntiVirusSofCheckDT " & vbCrLf & _
                                           ",ct.BusyoKikiBiko " & vbCrLf & _
                                           ",ct.ManageKyokuNM " & vbCrLf & _
                                           ",ct.ManageBusyoNM " & vbCrLf & _
                                           ",ct.WorkFromNmb " & vbCrLf & _
                                           ",ct.IPUseCD " & vbCrLf & _
                                           ",ct.FixedIP " & vbCrLf & _
                                           ",ct.UsrID " & vbCrLf & _
                                           ",ct.UsrNM " & vbCrLf & _
                                           ",ct.UsrCompany " & vbCrLf & _
                                           ",ct.UsrKyokuNM " & vbCrLf & _
                                           ",ct.UsrBusyoNM " & vbCrLf & _
                                           ",ct.UsrTel " & vbCrLf & _
                                           ",ct.UsrMailAdd " & vbCrLf & _
                                           ",ct.UsrContact " & vbCrLf & _
                                           ",ct.UsrRoom " & vbCrLf & _
                                           ",ct.SetKyokuNM " & vbCrLf & _
                                           ",ct.SetBusyoNM " & vbCrLf & _
                                           ",ct.SetRoom " & vbCrLf & _
                                           ",ct.SetBuil " & vbCrLf & _
                                           ",ct.SetFloor " & vbCrLf & _
                                           ",ct.SerialAimai " & vbCrLf & _
                                           ",ct.ManageBusyoNMAimai " & vbCrLf & _
                                           ",ct.UsrIDAimai " & vbCrLf & _
                                           ",ct.UsrBusyoNMAimai " & vbCrLf & _
                                           ",ct.SetBusyoNMAimai " & vbCrLf & _
                                           ",ct.SetRoomAimai " & vbCrLf & _
                                           ",ct.SetFloorAimai " & vbCrLf & _
                                           ",ct.RegDT " & vbCrLf & _
                                           ",ct.RegGrpCD " & vbCrLf & _
                                           ",ct.RegID " & vbCrLf & _
                                           ",ct.UpdateDT " & vbCrLf & _
                                           ",ct.UpGrpCD " & vbCrLf & _
                                           ",ct.UpdateID " & vbCrLf & _
                                           "FROM CI_BUY_TB ct " & vbCrLf & _
                                           "WHERE ct.CINmb=:CINmb "

    'CI部所有機器更新（UPDATE）SQL
    Private strUpdateCIBuySql As String = "UPDATE CI_BUY_TB SET " & vbCrLf & _
                                           " Kataban = :Kataban" & vbCrLf & _
                                           ",Aliau = :Aliau" & vbCrLf & _
                                           ",Serial = :Serial" & vbCrLf & _
                                           ",MacAddress1 = :MacAddress1" & vbCrLf & _
                                           ",MacAddress2 = :MacAddress2" & vbCrLf & _
                                           ",ZooKbn = :ZooKbn" & vbCrLf & _
                                           ",OSNM = :OSNM" & vbCrLf & _
                                           ",AntiVirusSoftNM = :AntiVirusSoftNM " & vbCrLf & _
                                           ",DNSRegCD =:DNSRegCD" & vbCrLf & _
                                           ",NIC1 = :NIC1" & vbCrLf & _
                                           ",NIC2 = :NIC2" & vbCrLf & _
                                           ",ConnectDT = CASE COALESCE(:ConnectDT,'') WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(:ConnectDT, 'YYYY/MM/DD'),'YYYYMMDD') END " & vbCrLf & _
                                           ",ExpirationDT = CASE COALESCE(:ExpirationDT,'') WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(:ExpirationDT, 'YYYY/MM/DD'),'YYYYMMDD') END " & vbCrLf & _
                                           ",DeletDT = CASE COALESCE(:DeletDT,'') WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(:DeletDT, 'YYYY/MM/DD'),'YYYYMMDD') END " & vbCrLf & _
                                           ",LastInfoDT = CASE COALESCE(:LastInfoDT,'') WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(:LastInfoDT, 'YYYY/MM/DD'),'YYYYMMDD') END " & vbCrLf & _
                                           ",ConectReason = :ConectReason" & vbCrLf & _
                                           ",ExpirationUPDT = CASE COALESCE(:ExpirationUPDT,'') WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(:ExpirationUPDT, 'YYYY/MM/DD'),'YYYYMMDD') END " & vbCrLf & _
                                           ",InfoDT = CASE COALESCE(:InfoDT,'') WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(:InfoDT, 'YYYY/MM/DD'),'YYYYMMDD') END " & vbCrLf & _
                                           ",NumInfoKbn = :NumInfoKbn" & vbCrLf & _
                                           ",SealSendkbn = :SealSendkbn" & vbCrLf & _
                                           ",AntiVirusSofCheckKbn = :AntiVirusSofCheckKbn" & vbCrLf & _
                                           ",AntiVirusSofCheckDT = CASE COALESCE(:AntiVirusSofCheckDT,'') WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(:AntiVirusSofCheckDT, 'YYYY/MM/DD'),'YYYYMMDD') END " & vbCrLf & _
                                           ",BusyoKikiBiko = :BusyoKikiBiko" & vbCrLf & _
                                           ",ManageKyokuNM = :ManageKyokuNM" & vbCrLf & _
                                           ",ManageBusyoNM = :ManageBusyoNM" & vbCrLf & _
                                           ",WorkFromNmb = :WorkFromNmb" & vbCrLf & _
                                           ",IPUseCD = :IPUseCD" & vbCrLf & _
                                           ",FixedIP = :FixedIP" & vbCrLf & _
                                           ",UsrID = :UsrID" & vbCrLf & _
                                           ",UsrNM = :UsrNM" & vbCrLf & _
                                           ",UsrCompany = :UsrCompany" & vbCrLf & _
                                           ",UsrKyokuNM = :UsrKyokuNM" & vbCrLf & _
                                           ",UsrBusyoNM = :UsrBusyoNM" & vbCrLf & _
                                           ",UsrTel = :UsrTel" & vbCrLf & _
                                           ",UsrMailAdd = :UsrMailAdd" & vbCrLf & _
                                           ",UsrContact = :UsrContact" & vbCrLf & _
                                           ",UsrRoom = :UsrRoom" & vbCrLf & _
                                           ",SetKyokuNM = :SetKyokuNM" & vbCrLf & _
                                           ",SetBusyoNM = :SetBusyoNM" & vbCrLf & _
                                           ",SetRoom = :SetRoom" & vbCrLf & _
                                           ",SetBuil = :SetBuil" & vbCrLf & _
                                           ",SetFloor = :SetFloor" & vbCrLf & _
                                           ",SerialAimai = :SerialAimai" & vbCrLf & _
                                           ",ManageBusyoNMAimai = :ManageBusyoNMAimai" & vbCrLf & _
                                           ",UsrIDAimai = :UsrIDAimai" & vbCrLf & _
                                           ",UsrBusyoNMAimai = :UsrBusyoNMAimai" & vbCrLf & _
                                           ",SetBusyoNMAimai = :SetBusyoNMAimai" & vbCrLf & _
                                           ",SetRoomAimai = :SetRoomAimai" & vbCrLf & _
                                           ",SetFloorAimai = :SetFloorAimai" & vbCrLf & _
                                           ",RegDT = :RegDT" & vbCrLf & _
                                           ",RegGrpCD = :RegGrpCD" & vbCrLf & _
                                           ",RegID = :RegID" & vbCrLf & _
                                           ",UpdateDT = :UpdateDT" & vbCrLf & _
                                           ",UpGrpCD = :UpGrpCD" & vbCrLf & _
                                           ",UpdateID = :UpdateID" & vbCrLf & _
                                           "WHERE CINmb=:CINmb "

    '番号取得処理(SELECT)SQL
    Private strSelectNumSql As String = "SELECT COUNT(*) " & vbCrLf & _
                                        "FROM CI_INFO_TB " & vbCrLf & _
                                        "WHERE Num = LPAD(:Num, 5, '0')" & vbCrLf & _
                                        " AND CIKbnCD = :CIKbnCD " & vbCrLf & _
                                        " AND CINmb <> :CINmb "

    ''' <summary>
    ''' 【新規登録モード】新規CI番号、サーバー日付取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規CI番号、サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewCINmbAndSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                   ByVal Cn As NpgsqlConnection, _
                                                   ByVal dataHBKB1301 As DataHBKB1301) As Boolean

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
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB1301 As DataHBKB1301) As Boolean

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
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCIInfoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB1301 As DataHBKB1301) As Boolean

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
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))    'CI番号
            Adapter.SelectCommand.Parameters("CINmb").Value = dataHBKB1301.PropIntCINmb

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
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCIInfoRSql(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB1301 As DataHBKB1301) As Boolean

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
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))    'CI番号
            Adapter.SelectCommand.Parameters("CINmb").Value = dataHBKB1301.PropIntCINmb
            '履歴番号
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer)) '履歴番号
            Adapter.SelectCommand.Parameters("RirekiNo").Value = dataHBKB1301.PropIntRirekiNo

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
    ''' 【編集／参照モード】CI部所有機器取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI部所有機器取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCIBuySql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectCIBuySql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)



            'CI番号
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))    'CI番号
            Adapter.SelectCommand.Parameters("CINmb").Value = dataHBKB1301.PropIntCINmb

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
    ''' 【履歴モード】CI部所有機器履歴取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI部所有機器履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCIBuyRSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectCIBuyRSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)



            'CI番号
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))    'CI番号
            Adapter.SelectCommand.Parameters("CINmb").Value = dataHBKB1301.PropIntCINmb

            '履歴番号
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer)) '履歴番号
            Adapter.SelectCommand.Parameters("RirekiNo").Value = dataHBKB1301.PropIntRirekiNo

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
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>原因リンク取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCauseLinkSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB1301 As DataHBKB1301) As Boolean

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
                .Parameters("CINmb").Value = DataHBKB1301.PropIntCINmb                           'CI番号
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
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>原因リンク履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCauseLinkRSql(ByRef Adapter As NpgsqlDataAdapter, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB1301 As DataHBKB1301) As Boolean

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
                .Parameters("CINmb").Value = dataHBKB1301.PropIntCINmb                           'CI番号
                .Parameters("RirekiNo").Value = dataHBKB1301.PropIntRirekiNo                     '履歴番号
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
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>登録理由履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectRegReasonSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB1301 As DataHBKB1301) As Boolean

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
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))    'CI番号
            Adapter.SelectCommand.Parameters("CINmb").Value = dataHBKB1301.PropIntCINmb

            '履歴番号
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer)) '履歴番号
            Adapter.SelectCommand.Parameters("RirekiNo").Value = DataHBKB1301.PropIntRirekiNo


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
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>登録理由履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectRegReasonRSql(ByRef Adapter As NpgsqlDataAdapter, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB1301 As DataHBKB1301) As Boolean

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
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))    'CI番号
            Adapter.SelectCommand.Parameters("CINmb").Value = DataHBKB1301.PropIntCINmb
            '履歴番号
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer)) '履歴番号
            Adapter.SelectCommand.Parameters("RirekiNo").Value = dataHBKB1301.PropIntRirekiNo

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
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCIInfoSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB1301 As DataHBKB1301) As Boolean

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
                .Parameters("CINmb").Value = DataHBKB1301.PropIntCINmb                          'CI番号
                .Parameters("CIKbnCD").Value = CI_TYPE_KIKI                                     'CI種別CD
                .Parameters("KindCD").Value = DataHBKB1301.PropCmbKind.SelectedValue            '種別CD
                .Parameters("Num").Value = dataHBKB1301.PropTxtNum.Text                         '番号
                .Parameters("CIStatusCD").Value = DataHBKB1301.PropCmbCIStatus.SelectedValue    'ステータスCD
                .Parameters("Class1").Value = DataHBKB1301.PropTxtClass1.Text                   '分類１
                .Parameters("Class2").Value = DataHBKB1301.PropTxtClass2.Text                   '分類２
                .Parameters("CINM").Value = DataHBKB1301.PropTxtCINM.Text                       '名称

                'CIオーナー名が入力されている場合のみCIオーナーCDに値をセット
                If DataHBKB1301.PropTxtCIOwnerNM.Text.Trim() <> "" Then
                    .Parameters("CIOwnerCD").Value = DataHBKB1301.PropLblCIOwnerCD.Text         'CIオーナーCD
                Else
                    .Parameters("CIOwnerCD").Value = ""
                End If

                .Parameters("CINaiyo").Value = DataHBKB1301.PropTxtCINaiyo.Text                 '説明
                .Parameters("BIko1").Value = DataHBKB1301.PropTxtBIko1.Text                     'フリーテキスト１
                .Parameters("Biko2").Value = DataHBKB1301.PropTxtBIko2.Text                     'フリーテキスト２
                .Parameters("BIko3").Value = DataHBKB1301.PropTxtBIko3.Text                     'フリーテキスト３
                .Parameters("Biko4").Value = DataHBKB1301.PropTxtBIko4.Text                     'フリーテキスト４
                .Parameters("Biko5").Value = DataHBKB1301.PropTxtBIko5.Text                     'フリーテキスト５

                'フリーフラグ１～５
                If DataHBKB1301.PropChkFreeFlg1.Checked = True Then
                    .Parameters("FreeFlg1").Value = FREE_FLG_ON                                 'フリーフラグ１
                Else
                    .Parameters("FreeFlg1").Value = FREE_FLG_OFF                                'フリーフラグ１
                End If
                If DataHBKB1301.PropChkFreeFlg2.Checked = True Then
                    .Parameters("FreeFlg2").Value = FREE_FLG_ON                                 'フリーフラグ２
                Else
                    .Parameters("FreeFlg2").Value = FREE_FLG_OFF                                'フリーフラグ２
                End If
                If DataHBKB1301.PropChkFreeFlg3.Checked = True Then
                    .Parameters("FreeFlg3").Value = FREE_FLG_ON                                 'フリーフラグ１
                Else
                    .Parameters("FreeFlg3").Value = FREE_FLG_OFF                                'フリーフラグ２
                End If
                If DataHBKB1301.PropChkFreeFlg4.Checked = True Then
                    .Parameters("FreeFlg4").Value = FREE_FLG_ON                                 'フリーフラグ１
                Else
                    .Parameters("FreeFlg4").Value = FREE_FLG_OFF                                'フリーフラグ２
                End If
                If DataHBKB1301.PropChkFreeFlg5.Checked = True Then
                    .Parameters("FreeFlg5").Value = FREE_FLG_ON                                 'フリーフラグ１
                Else
                    .Parameters("FreeFlg5").Value = FREE_FLG_OFF                                'フリーフラグ２
                End If

                'あいまい検索文字列設定
                '※フリーワード（あいまい）　：分類１～２、名称、説明を連結
                '※フリーテキスト（あいまい）：フリーテキスト１～５を連結
                strClass1Aimai = commonLogicHBK.ChangeStringForSearch(DataHBKB1301.PropTxtClass1.Text)
                strClass2Aimai = commonLogicHBK.ChangeStringForSearch(DataHBKB1301.PropTxtClass2.Text)
                strCINMAimai = commonLogicHBK.ChangeStringForSearch(DataHBKB1301.PropTxtCINM.Text)
                strFreeWordAimai = strClass1Aimai & strClass2Aimai & strCINMAimai & _
                                   commonLogicHBK.ChangeStringForSearch(DataHBKB1301.PropTxtCINaiyo.Text)
                strBikoAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB1301.PropTxtBIko1.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKB1301.PropTxtBIko2.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKB1301.PropTxtBIko3.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKB1301.PropTxtBIko4.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKB1301.PropTxtBIko5.Text)

                .Parameters("Class1Aimai").Value = strClass1Aimai                               '分類１（あいまい）
                .Parameters("Class2Aimai").Value = strClass2Aimai                               '分類２（あいまい）
                .Parameters("CINMAimai").Value = strCINMAimai                                   '名称（あいまい）
                .Parameters("FreeWordAimai").Value = strFreeWordAimai                           'フリーワード（あいまい）
                .Parameters("BikoAimai").Value = strBikoAimai                                   'フリーテキスト（あいまい）

                .Parameters("RegDT").Value = dataHBKB1301.PropDtmSysDate                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB1301.PropDtmSysDate                     '最終更新日時
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
    ''' 【編集／参照／履歴モード】CI共通情報更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateCIInfoSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB1301 As DataHBKB1301) As Boolean

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
                .Parameters("CIKbnCD").Value = CI_TYPE_KIKI                                     'CI種別CD
                .Parameters("KindCD").Value = DataHBKB1301.PropCmbKind.SelectedValue            '種別CD
                .Parameters("Num").Value = dataHBKB1301.PropTxtNum.Text                         '番号
                .Parameters("CIStatusCD").Value = DataHBKB1301.PropCmbCIStatus.SelectedValue    'ステータスCD
                .Parameters("Class1").Value = DataHBKB1301.PropTxtClass1.Text                   '分類１
                .Parameters("Class2").Value = DataHBKB1301.PropTxtClass2.Text                   '分類２
                .Parameters("CINM").Value = DataHBKB1301.PropTxtCINM.Text                       '名称

                'CIオーナー名が入力されている場合のみCIオーナーCDに値をセット
                If DataHBKB1301.PropTxtCIOwnerNM.Text.Trim() <> "" Then
                    .Parameters("CIOwnerCD").Value = DataHBKB1301.PropLblCIOwnerCD.Text         'CIオーナーCD
                Else
                    .Parameters("CIOwnerCD").Value = ""
                End If

                .Parameters("CINaiyo").Value = DataHBKB1301.PropTxtCINaiyo.Text                 '説明
                .Parameters("BIko1").Value = DataHBKB1301.PropTxtBIko1.Text                     'フリーテキスト１
                .Parameters("Biko2").Value = DataHBKB1301.PropTxtBIko2.Text                     'フリーテキスト２
                .Parameters("BIko3").Value = DataHBKB1301.PropTxtBIko3.Text                     'フリーテキスト３
                .Parameters("Biko4").Value = DataHBKB1301.PropTxtBIko4.Text                     'フリーテキスト４
                .Parameters("Biko5").Value = DataHBKB1301.PropTxtBIko5.Text                     'フリーテキスト５

                'フリーフラグ１～５
                If DataHBKB1301.PropChkFreeFlg1.Checked = True Then
                    .Parameters("FreeFlg1").Value = FREE_FLG_ON                                 'フリーフラグ１
                Else
                    .Parameters("FreeFlg1").Value = FREE_FLG_OFF                                'フリーフラグ１
                End If
                If DataHBKB1301.PropChkFreeFlg2.Checked = True Then
                    .Parameters("FreeFlg2").Value = FREE_FLG_ON                                 'フリーフラグ２
                Else
                    .Parameters("FreeFlg2").Value = FREE_FLG_OFF                                'フリーフラグ２
                End If
                If DataHBKB1301.PropChkFreeFlg3.Checked = True Then
                    .Parameters("FreeFlg3").Value = FREE_FLG_ON                                 'フリーフラグ３
                Else
                    .Parameters("FreeFlg3").Value = FREE_FLG_OFF                                'フリーフラグ３
                End If
                If DataHBKB1301.PropChkFreeFlg4.Checked = True Then
                    .Parameters("FreeFlg4").Value = FREE_FLG_ON                                 'フリーフラグ４
                Else
                    .Parameters("FreeFlg4").Value = FREE_FLG_OFF                                'フリーフラグ４
                End If
                If DataHBKB1301.PropChkFreeFlg5.Checked = True Then
                    .Parameters("FreeFlg5").Value = FREE_FLG_ON                                 'フリーフラグ５
                Else
                    .Parameters("FreeFlg5").Value = FREE_FLG_OFF                                'フリーフラグ５
                End If

                'あいまい検索文字列設定
                '※フリーワード（あいまい）　：分類１～２、名称、説明を連結
                '※フリーテキスト（あいまい）：フリーテキスト１～５を連結
                strClass1Aimai = commonLogicHBK.ChangeStringForSearch(dataHBKB1301.PropTxtClass1.Text)      '分類１（あいまい）
                strClass2Aimai = commonLogicHBK.ChangeStringForSearch(dataHBKB1301.PropTxtClass2.Text)      '分類２（あいまい）
                strCINMAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB1301.PropTxtCINM.Text)          '名称（あいまい）
                strFreeWordAimai = strClass1Aimai & strClass2Aimai & strCINMAimai & _
                                   commonLogicHBK.ChangeStringForSearch(dataHBKB1301.ProptxtCINaiyo.Text)   'フリーワード（あいまい）
                strBikoAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB1301.PropTxtBIko1.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKB1301.PropTxtBIko2.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKB1301.PropTxtBIko3.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKB1301.PropTxtBIko4.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKB1301.PropTxtBIko5.Text)         'フリーテキスト（あいまい）

                .Parameters("Class1Aimai").Value = strClass1Aimai                               '分類１（あいまい）
                .Parameters("Class2Aimai").Value = strClass2Aimai                               '分類２（あいまい）
                .Parameters("CINMAimai").Value = strCINMAimai                                   '名称（あいまい）
                .Parameters("FreeWordAimai").Value = strFreeWordAimai                           'フリーワード（あいまい）
                .Parameters("BikoAimai").Value = strBikoAimai                                   'フリーテキスト（あいまい）

                .Parameters("UpdateDT").Value = dataHBKB1301.PropDtmSysDate                     '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                  '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                      '最終更新者ID
                .Parameters("CINmb").Value = dataHBKB1301.PropIntCINmb                          'CI番号
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
    ''' 【新規登録モード】CI部所有機器新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI部所有機器新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/27 s/tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCIBuySql(ByRef Cmd As NpgsqlCommand, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""                       'SQL文
        Dim strSerialAimai As String = ""               '製造番号（あいまい）
        Dim strManageBusyoAimai As String = ""          '管理部署（あいまい）
        Dim strUsrIDAimai As String = ""                'ユーザーID（あいまい）
        Dim strUsrBusyoNMAimai As String = ""           'ユーザー所属部署（あいまい）
        Dim strSetBusyoNMAimai As String = ""           '設置部署（あいまい）
        Dim strSetRoomAimai As String = ""              '設置番組/部屋    （あいまい）
        Dim strSetBuilAimai As String = ""              '設置建物（あいまい）
        Dim strSetFloorAimai As String = ""             '設置フロア（あいまい）


        Try
            'SQL文(INSERT)
            strSQL = strInsertCIBuySql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)



            '型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                                    'CI種別CD
                .Add(New NpgsqlParameter("Kataban", NpgsqlTypes.NpgsqlDbType.Varchar))                                  '型番
                .Add(New NpgsqlParameter("Aliau", NpgsqlTypes.NpgsqlDbType.Varchar))                                    'エイリアス
                .Add(New NpgsqlParameter("Serial", NpgsqlTypes.NpgsqlDbType.Varchar))                                   '製造番号
                .Add(New NpgsqlParameter("MacAddress1", NpgsqlTypes.NpgsqlDbType.Varchar))                              'MACアドレス1
                .Add(New NpgsqlParameter("MacAddress2", NpgsqlTypes.NpgsqlDbType.Varchar))                              'MACアドレス2
                .Add(New NpgsqlParameter("ZooKbn", NpgsqlTypes.NpgsqlDbType.Varchar))                                   'zoo参加有無
                .Add(New NpgsqlParameter("OSNM", NpgsqlTypes.NpgsqlDbType.Varchar))                                     'OS名
                .Add(New NpgsqlParameter("AntiVirusSoftNM", NpgsqlTypes.NpgsqlDbType.Varchar))                          'ウイルス対策ソフト名
                .Add(New NpgsqlParameter("DNSRegCD", NpgsqlTypes.NpgsqlDbType.Varchar))                                 'DNS登録CD
                .Add(New NpgsqlParameter("NIC1", NpgsqlTypes.NpgsqlDbType.Varchar))                                     'NIC1
                .Add(New NpgsqlParameter("NIC2", NpgsqlTypes.NpgsqlDbType.Varchar))                                     'NIC2
                .Add(New NpgsqlParameter("ConnectDT", NpgsqlTypes.NpgsqlDbType.Varchar))                                '接続日
                .Add(New NpgsqlParameter("ExpirationDT", NpgsqlTypes.NpgsqlDbType.Varchar))                             '有効日
                .Add(New NpgsqlParameter("DeletDT", NpgsqlTypes.NpgsqlDbType.Varchar))                                  '停止日
                .Add(New NpgsqlParameter("LastInfoDT", NpgsqlTypes.NpgsqlDbType.Varchar))                               '最終お知らせ日
                .Add(New NpgsqlParameter("ConectReason", NpgsqlTypes.NpgsqlDbType.Varchar))                             '接続理由
                .Add(New NpgsqlParameter("ExpirationUPDT", NpgsqlTypes.NpgsqlDbType.Varchar))                           '更新日
                .Add(New NpgsqlParameter("InfoDT", NpgsqlTypes.NpgsqlDbType.Varchar))                                   '通知日
                .Add(New NpgsqlParameter("NumInfoKbn", NpgsqlTypes.NpgsqlDbType.Varchar))                               '番号通知
                .Add(New NpgsqlParameter("SealSendkbn", NpgsqlTypes.NpgsqlDbType.Varchar))                              'シール送付
                .Add(New NpgsqlParameter("AntiVirusSofCheckKbn", NpgsqlTypes.NpgsqlDbType.Varchar))                     'ウイルス対策ソフト確認
                .Add(New NpgsqlParameter("AntiVirusSofCheckDT", NpgsqlTypes.NpgsqlDbType.Varchar))                      'ウイルス対策ソフトサーバー確認日
                .Add(New NpgsqlParameter("BusyoKikiBiko", NpgsqlTypes.NpgsqlDbType.Varchar))                            '部所有機器備考
                .Add(New NpgsqlParameter("ManageKyokuNM", NpgsqlTypes.NpgsqlDbType.Varchar))                            '管理局
                .Add(New NpgsqlParameter("ManageBusyoNM", NpgsqlTypes.NpgsqlDbType.Varchar))                            '管理部署
                .Add(New NpgsqlParameter("WorkFromNmb", NpgsqlTypes.NpgsqlDbType.Varchar))                              '作業の元
                .Add(New NpgsqlParameter("IPUseCD", NpgsqlTypes.NpgsqlDbType.Varchar))                                  'IP割当種類CD
                .Add(New NpgsqlParameter("FixedIP", NpgsqlTypes.NpgsqlDbType.Varchar))                                  '固定IP
                .Add(New NpgsqlParameter("UsrID", NpgsqlTypes.NpgsqlDbType.Varchar))                                    'ユーザーID
                .Add(New NpgsqlParameter("UsrNM", NpgsqlTypes.NpgsqlDbType.Varchar))                                    'ユーザー氏名
                .Add(New NpgsqlParameter("UsrCompany ", NpgsqlTypes.NpgsqlDbType.Varchar))                              'ユーザー所属会社
                .Add(New NpgsqlParameter("UsrKyokuNM", NpgsqlTypes.NpgsqlDbType.Varchar))                               'ユーザー所属局
                .Add(New NpgsqlParameter("UsrBusyoNM", NpgsqlTypes.NpgsqlDbType.Varchar))                               'ユーザー所属部署
                .Add(New NpgsqlParameter("UsrTel", NpgsqlTypes.NpgsqlDbType.Varchar))                                   'ユーザー電話番号
                .Add(New NpgsqlParameter("UsrMailAdd", NpgsqlTypes.NpgsqlDbType.Varchar))                               'ユーザーメールアドレス
                .Add(New NpgsqlParameter("UsrContact", NpgsqlTypes.NpgsqlDbType.Varchar))                               'ユーザー連絡先
                .Add(New NpgsqlParameter("UsrRoom", NpgsqlTypes.NpgsqlDbType.Varchar))                                  'ユーザー番組/部屋
                .Add(New NpgsqlParameter("SetKyokuNM", NpgsqlTypes.NpgsqlDbType.Varchar))                               '設置局
                .Add(New NpgsqlParameter("SetBusyoNM", NpgsqlTypes.NpgsqlDbType.Varchar))                               '設置部署
                .Add(New NpgsqlParameter("SetRoom", NpgsqlTypes.NpgsqlDbType.Varchar))                                  '設置番組/部屋
                .Add(New NpgsqlParameter("SetBuil", NpgsqlTypes.NpgsqlDbType.Varchar))                                  '設置建物
                .Add(New NpgsqlParameter("SetFloor", NpgsqlTypes.NpgsqlDbType.Varchar))                                 '設置フロア
                .Add(New NpgsqlParameter("SerialAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                              '製造番号（あいまい）
                .Add(New NpgsqlParameter("ManageBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                       '管理部署（あいまい）
                .Add(New NpgsqlParameter("UsrIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                               'ユーザーID（あいまい）
                .Add(New NpgsqlParameter("UsrBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                          'ユーザー所属部署（あいまい）
                .Add(New NpgsqlParameter("SetBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                          '設置部署（あいまい）
                .Add(New NpgsqlParameter("SetRoomAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                             '設置番組/部屋（あいまい）
                .Add(New NpgsqlParameter("SetBuilAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                             '設置建物（あいまい）
                .Add(New NpgsqlParameter("SetFloorAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                            '設置フロア（あいまい）
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                                  '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                                 '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                                    '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                               '最終更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                                 '最終更新者ID

            End With
            '値をセット
            With Cmd
                .Parameters("CINmb").Value = dataHBKB1301.PropIntCINmb                                                  'CI種別CD
                .Parameters("Kataban").Value = dataHBKB1301.ProptxtKataban.Text                                         '型番
                .Parameters("Aliau").Value = dataHBKB1301.ProptxtAliau.Text                                             'エイリアス
                .Parameters("Serial").Value = dataHBKB1301.PropTxtSerial.Text                                           '製造番号
                .Parameters("MacAddress1").Value = dataHBKB1301.ProptxtMacaddress1.Text                                 'MACアドレス1
                .Parameters("MacAddress2").Value = dataHBKB1301.ProptxtMacaddress2.Text                                 'MACアドレス2
                .Parameters("ZooKbn").Value = dataHBKB1301.PropcmbZooKbn.SelectedValue                                  'zoo参加有無
                .Parameters("OSNM").Value = dataHBKB1301.PropcmbOSCD.Text                                               'OS名
                .Parameters("AntiVirusSoftNM").Value = dataHBKB1301.PropcmbAntiVirusSoftCD.Text                         'ウイルス対策ソフト名
                .Parameters("DNSRegCD").Value = dataHBKB1301.PropcmbDNSRegCD.SelectedValue                              'DNS登録CD
                .Parameters("NIC1").Value = dataHBKB1301.ProptxtNIC1.Text                                               'NIC1
                .Parameters("NIC2").Value = dataHBKB1301.ProptxtNIC2.Text                                               'NIC2
                .Parameters("ConnectDT").Value = dataHBKB1301.PropdtpConnectDT.txtDate.Text                             '接続日
                .Parameters("ExpirationDT").Value = dataHBKB1301.PropdtpExpirationDT.txtDate.Text                       '有効日
                .Parameters("DeletDT").Value = dataHBKB1301.PropdtpDeletDT.txtDate.Text                                 '停止日
                .Parameters("LastInfoDT").Value = dataHBKB1301.PropdtpLastInfoDT.txtDate.Text                           '最終お知らせ日
                .Parameters("ConectReason").Value = dataHBKB1301.ProptxtConectReason.Text                               '接続理由
                .Parameters("ExpirationUPDT").Value = dataHBKB1301.PropdtpExpirationUPDT.txtDate.Text                   '更新日
                .Parameters("InfoDT").Value = dataHBKB1301.PropdtpInfoDT.txtDate.Text                                   '通知日
                .Parameters("NumInfoKbn").Value = dataHBKB1301.PropcmbNumInfoKbn.SelectedValue                          '番号通知
                .Parameters("SealSendkbn").Value = dataHBKB1301.PropcmbSealSendkbn.SelectedValue                        'シール送付
                .Parameters("AntiVirusSofCheckKbn").Value = dataHBKB1301.PropcmbAntiVirusSofCheckKbn.SelectedValue      'ウイルス対策ソフト確認
                .Parameters("AntiVirusSofCheckDT").Value = dataHBKB1301.PropDtpAntiVirusSofCheckDT.txtDate.Text         'ウイルス対策ソフトサーバー確認日
                .Parameters("BusyoKikiBiko").Value = dataHBKB1301.ProptxtBusyoKikiBiko.Text                             '部所有機器備考
                .Parameters("ManageKyokuNM").Value = dataHBKB1301.ProptxtManageKyokuNM.Text                             '管理局
                .Parameters("ManageBusyoNM").Value = dataHBKB1301.ProptxtManageBusyoNM.Text                             '管理部署
                .Parameters("WorkFromNmb").Value = dataHBKB1301.ProptxtWorkFromNmb.Text                                 '作業の元
                .Parameters("IPUseCD").Value = dataHBKB1301.PropcmbIPUseCD.SelectedValue                                'IP割当種類CD
                .Parameters("FixedIP").Value = dataHBKB1301.ProptxtFixedIP.Text                                         '固定IP
                .Parameters("UsrID").Value = dataHBKB1301.ProptxtUsrID.Text                                             'ユーザーID
                .Parameters("UsrNM").Value = dataHBKB1301.ProptxtUsrNM.Text                                             'ユーザー氏名
                .Parameters("UsrCompany").Value = dataHBKB1301.ProptxtUsrCompany.Text                                   'ユーザー所属会社
                .Parameters("UsrKyokuNM").Value = dataHBKB1301.ProptxtUsrKyokuNM.Text                                   'ユーザー所属局
                .Parameters("UsrBusyoNM").Value = dataHBKB1301.ProptxtUsrBusyoNM.Text                                   'ユーザー所属部署
                .Parameters("UsrTel").Value = dataHBKB1301.ProptxtUsrTel.Text                                           'ユーザー電話番号
                .Parameters("UsrMailAdd").Value = dataHBKB1301.ProptxtUsrMailAdd.Text                                   'ユーザーメールアドレス
                .Parameters("UsrContact").Value = dataHBKB1301.ProptxtUsrContact.Text                                   'ユーザー連絡先
                .Parameters("UsrRoom").Value = dataHBKB1301.ProptxtUsrRoom.Text                                         'ユーザールーム
                .Parameters("SetKyokuNM").Value = dataHBKB1301.ProptxtSetKyokuNM.Text                                   '設置局
                .Parameters("SetBusyoNM").Value = dataHBKB1301.ProptxtSetBusyoNM.Text                                   '設置部署
                .Parameters("SetRoom").Value = dataHBKB1301.ProptxtSetRoom.Text                                         '設置番組
                .Parameters("SetBuil").Value = dataHBKB1301.ProptxtSetBuil.Text                                         '設置建物
                .Parameters("SetFloor").Value = dataHBKB1301.ProptxtSetFloor.Text                                       '設置フロア

                'あいまい検索文字列設定
                strSerialAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB1301.PropTxtSerial.Text)                  '製造番号（あいまい）
                strManageBusyoAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB1301.ProptxtManageBusyoNM.Text)      '管理部署（あいまい）
                strUsrIDAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB1301.ProptxtUsrID.Text)                    'ユーザーID（あいまい）
                strUsrBusyoNMAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB1301.ProptxtUsrBusyoNM.Text)          'ユーザー所属部署（あいまい）
                strSetBusyoNMAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB1301.ProptxtSetBusyoNM.Text)          '設置部署（あいまい）
                strSetRoomAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB1301.ProptxtSetRoom.Text)                '設置番組/部屋    （あいまい）
                strSetBuilAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB1301.ProptxtSetBuil.Text)                '設置建物（あいまい）
                strSetFloorAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB1301.ProptxtSetFloor.Text)              '設置フロア（あいまい）

                .Parameters("SerialAimai").Value = strSerialAimai                                                       '製造番号（あいまい）
                .Parameters("ManageBusyoNMAimai").Value = strManageBusyoAimai                                           '管理部署（あいまい）
                .Parameters("UsrIDAimai").Value = strUsrIDAimai                                                         'ユーザーID（あいまい）
                .Parameters("UsrBusyoNMAimai").Value = strUsrBusyoNMAimai                                               'ユーザー所属部署（あいまい）
                .Parameters("SetBusyoNMAimai").Value = strSetBusyoNMAimai                                               '設置部署（あいまい）
                .Parameters("SetRoomAimai").Value = strSetRoomAimai                                                     '設置番組/部屋    （あいまい）
                .Parameters("SetBuilAimai").Value = strSetBuilAimai                                                     '設置建物（あいまい）
                .Parameters("SetFloorAimai").Value = strSetFloorAimai                                                   '設置フロア（あいまい）

                .Parameters("RegDT").Value = dataHBKB1301.PropDtmSysDate                                                '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                                         '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                                 '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB1301.PropDtmSysDate                                             '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                                          '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                                              '最終更新者ID

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
    ''' 【編集／参照／履歴モード】CI部所有機器更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI部所有機器更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateCIBuySql(ByRef Cmd As NpgsqlCommand, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""                       'SQL文
        Dim strSerialAimai As String = ""               '製造番号（あいまい）
        Dim strManageBusyoAimai As String = ""          '管理部署（あいまい）
        Dim strUsrIDAimai As String = ""                'ユーザーID（あいまい）
        Dim strUsrBusyoNMAimai As String = ""           'ユーザー所属部署（あいまい）
        Dim strSetBusyoNMAimai As String = ""           '設置部署（あいまい）
        Dim strSetRoomAimai As String = ""              '設置番組/部屋    （あいまい）
        Dim strSetBuilAimai As String = ""              '設置建物（あいまい）
        Dim strSetFloorAimai As String = ""             '設置フロア（あいまい

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateCIBuySql

            'データアダプタに、SQLのUPDATE文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            '型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("Kataban", NpgsqlTypes.NpgsqlDbType.Varchar))                                  '型番
                .Add(New NpgsqlParameter("Aliau", NpgsqlTypes.NpgsqlDbType.Varchar))                                    'エイリアス
                .Add(New NpgsqlParameter("Serial", NpgsqlTypes.NpgsqlDbType.Varchar))                                   '製造番号
                .Add(New NpgsqlParameter("MacAddress1", NpgsqlTypes.NpgsqlDbType.Varchar))                              'MACアドレス1
                .Add(New NpgsqlParameter("MacAddress2", NpgsqlTypes.NpgsqlDbType.Varchar))                              'MACアドレス2
                .Add(New NpgsqlParameter("ZooKbn", NpgsqlTypes.NpgsqlDbType.Varchar))                                   'zoo参加有無
                .Add(New NpgsqlParameter("OSNM", NpgsqlTypes.NpgsqlDbType.Varchar))                                     'OS名
                .Add(New NpgsqlParameter("AntiVirusSoftNM", NpgsqlTypes.NpgsqlDbType.Varchar))                          'ウイルス対策ソフト名
                .Add(New NpgsqlParameter("DNSRegCD", NpgsqlTypes.NpgsqlDbType.Varchar))                                 'DNS登録CD
                .Add(New NpgsqlParameter("NIC1", NpgsqlTypes.NpgsqlDbType.Varchar))                                     'NIC1
                .Add(New NpgsqlParameter("NIC2", NpgsqlTypes.NpgsqlDbType.Varchar))                                     'NIC2
                .Add(New NpgsqlParameter("ConnectDT", NpgsqlTypes.NpgsqlDbType.Varchar))                                '接続日
                .Add(New NpgsqlParameter("ExpirationDT", NpgsqlTypes.NpgsqlDbType.Varchar))                             '有効日
                .Add(New NpgsqlParameter("DeletDT", NpgsqlTypes.NpgsqlDbType.Varchar))                                  '停止日
                .Add(New NpgsqlParameter("LastInfoDT", NpgsqlTypes.NpgsqlDbType.Varchar))                               '最終お知らせ日
                .Add(New NpgsqlParameter("ConectReason", NpgsqlTypes.NpgsqlDbType.Varchar))                             '接続理由
                .Add(New NpgsqlParameter("ExpirationUPDT", NpgsqlTypes.NpgsqlDbType.Varchar))                           '更新日
                .Add(New NpgsqlParameter("InfoDT", NpgsqlTypes.NpgsqlDbType.Varchar))                                   '通知日
                .Add(New NpgsqlParameter("NumInfoKbn", NpgsqlTypes.NpgsqlDbType.Varchar))                               '番号通知
                .Add(New NpgsqlParameter("SealSendkbn", NpgsqlTypes.NpgsqlDbType.Varchar))                              'シール送付
                .Add(New NpgsqlParameter("AntiVirusSofCheckKbn", NpgsqlTypes.NpgsqlDbType.Varchar))                     'ウイルス対策ソフト確認
                .Add(New NpgsqlParameter("AntiVirusSofCheckDT", NpgsqlTypes.NpgsqlDbType.Varchar))                      'ウイルス対策ソフトサーバー確認日
                .Add(New NpgsqlParameter("BusyoKikiBiko", NpgsqlTypes.NpgsqlDbType.Varchar))                            '部所有機器備考
                .Add(New NpgsqlParameter("ManageKyokuNM", NpgsqlTypes.NpgsqlDbType.Varchar))                            '管理局
                .Add(New NpgsqlParameter("ManageBusyoNM", NpgsqlTypes.NpgsqlDbType.Varchar))                            '管理部署
                .Add(New NpgsqlParameter("WorkFromNmb", NpgsqlTypes.NpgsqlDbType.Varchar))                              '作業の元
                .Add(New NpgsqlParameter("IPUseCD", NpgsqlTypes.NpgsqlDbType.Varchar))                                  'IP割当種類CD
                .Add(New NpgsqlParameter("FixedIP", NpgsqlTypes.NpgsqlDbType.Varchar))                                  '固定IP
                .Add(New NpgsqlParameter("UsrID", NpgsqlTypes.NpgsqlDbType.Varchar))                                    'ユーザーID
                .Add(New NpgsqlParameter("UsrNM", NpgsqlTypes.NpgsqlDbType.Varchar))                                    'ユーザー氏名
                .Add(New NpgsqlParameter("UsrCompany ", NpgsqlTypes.NpgsqlDbType.Varchar))                              'ユーザー所属会社
                .Add(New NpgsqlParameter("UsrKyokuNM", NpgsqlTypes.NpgsqlDbType.Varchar))                               'ユーザー所属局
                .Add(New NpgsqlParameter("UsrBusyoNM", NpgsqlTypes.NpgsqlDbType.Varchar))                               'ユーザー所属部署
                .Add(New NpgsqlParameter("UsrTel", NpgsqlTypes.NpgsqlDbType.Varchar))                                   'ユーザー電話番号
                .Add(New NpgsqlParameter("UsrMailAdd", NpgsqlTypes.NpgsqlDbType.Varchar))                               'ユーザーメールアドレス
                .Add(New NpgsqlParameter("UsrContact", NpgsqlTypes.NpgsqlDbType.Varchar))                               'ユーザー連絡先
                .Add(New NpgsqlParameter("UsrRoom", NpgsqlTypes.NpgsqlDbType.Varchar))                                  'ユーザー番組/部屋
                .Add(New NpgsqlParameter("SetKyokuNM", NpgsqlTypes.NpgsqlDbType.Varchar))                               '設置局
                .Add(New NpgsqlParameter("SetBusyoNM", NpgsqlTypes.NpgsqlDbType.Varchar))                               '設置部署
                .Add(New NpgsqlParameter("SetRoom", NpgsqlTypes.NpgsqlDbType.Varchar))                                  '設置番組/部屋
                .Add(New NpgsqlParameter("SetBuil", NpgsqlTypes.NpgsqlDbType.Varchar))                                  '設置建物
                .Add(New NpgsqlParameter("SetFloor", NpgsqlTypes.NpgsqlDbType.Varchar))                                 '設置フロア
                .Add(New NpgsqlParameter("SerialAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                              '製造番号（あいまい）
                .Add(New NpgsqlParameter("ManageBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                       '管理部署（あいまい）
                .Add(New NpgsqlParameter("UsrIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                               'ユーザーID（あいまい）
                .Add(New NpgsqlParameter("UsrBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                          'ユーザー所属部署（あいまい）
                .Add(New NpgsqlParameter("SetBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                          '設置部署（あいまい）
                .Add(New NpgsqlParameter("SetRoomAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                             '設置番組/部屋（あいまい）
                .Add(New NpgsqlParameter("SetBuilAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                             '設置建物（あいまい）
                .Add(New NpgsqlParameter("SetFloorAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                            '設置フロア（あいまい）
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                                  '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                                 '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                                    '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                               '最終更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                                 '最終更新者ID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                                    'CI種別CD
            End With
            '値をセット
            With Cmd
                .Parameters("Kataban").Value = dataHBKB1301.ProptxtKataban.Text                                         '型番
                .Parameters("Aliau").Value = dataHBKB1301.ProptxtAliau.Text                                             'エイリアス
                .Parameters("Serial").Value = dataHBKB1301.PropTxtSerial.Text                                           '製造番号
                .Parameters("MacAddress1").Value = dataHBKB1301.ProptxtMacaddress1.Text                                 'MACアドレス1
                .Parameters("MacAddress2").Value = dataHBKB1301.ProptxtMacaddress2.Text                                 'MACアドレス2
                .Parameters("ZooKbn").Value = dataHBKB1301.PropcmbZooKbn.SelectedValue                                  'zoo参加有無
                .Parameters("OSNM").Value = dataHBKB1301.PropcmbOSCD.Text                                               'OS名
                .Parameters("AntiVirusSoftNM").Value = dataHBKB1301.PropcmbAntiVirusSoftCD.Text                         'ウイルス対策ソフト名
                .Parameters("DNSRegCD").Value = dataHBKB1301.PropcmbDNSRegCD.SelectedValue                              'DNS登録CD
                .Parameters("NIC1").Value = dataHBKB1301.ProptxtNIC1.Text                                               'NIC1
                .Parameters("NIC2").Value = dataHBKB1301.ProptxtNIC2.Text                                               'NIC2
                .Parameters("ConnectDT").Value = dataHBKB1301.PropdtpConnectDT.txtDate.Text                             '接続日
                .Parameters("ExpirationDT").Value = dataHBKB1301.PropdtpExpirationDT.txtDate.Text                       '有効日
                .Parameters("DeletDT").Value = dataHBKB1301.PropdtpDeletDT.txtDate.Text                                 '停止日
                .Parameters("LastInfoDT").Value = dataHBKB1301.PropdtpLastInfoDT.txtDate.Text                           '最終お知らせ日
                .Parameters("ConectReason").Value = dataHBKB1301.ProptxtConectReason.Text                               '接続理由
                .Parameters("ExpirationUPDT").Value = dataHBKB1301.PropdtpExpirationUPDT.txtDate.Text                   '更新日
                .Parameters("InfoDT").Value = dataHBKB1301.PropdtpInfoDT.txtDate.Text                                   '通知日
                .Parameters("NumInfoKbn").Value = dataHBKB1301.PropcmbNumInfoKbn.SelectedValue                          '番号通知
                .Parameters("SealSendkbn").Value = dataHBKB1301.PropcmbSealSendkbn.SelectedValue                        'シール送付
                .Parameters("AntiVirusSofCheckKbn").Value = dataHBKB1301.PropcmbAntiVirusSofCheckKbn.SelectedValue      'ウイルス対策ソフト確認
                .Parameters("AntiVirusSofCheckDT").Value = dataHBKB1301.PropDtpAntiVirusSofCheckDT.txtDate.Text         'ウイルス対策ソフトサーバー確認日
                .Parameters("BusyoKikiBiko").Value = dataHBKB1301.ProptxtBusyoKikiBiko.Text                             '部所有機器備考
                .Parameters("ManageKyokuNM").Value = dataHBKB1301.ProptxtManageKyokuNM.Text                             '管理局
                .Parameters("ManageBusyoNM").Value = dataHBKB1301.ProptxtManageBusyoNM.Text                             '管理部署
                .Parameters("WorkFromNmb").Value = dataHBKB1301.ProptxtWorkFromNmb.Text                                 '作業の元
                .Parameters("IPUseCD").Value = dataHBKB1301.PropcmbIPUseCD.SelectedValue                                'IP割当種類CD
                .Parameters("FixedIP").Value = dataHBKB1301.ProptxtFixedIP.Text                                         '固定IP
                .Parameters("UsrID").Value = dataHBKB1301.ProptxtUsrID.Text                                             'ユーザーID
                .Parameters("UsrNM").Value = dataHBKB1301.ProptxtUsrNM.Text                                             'ユーザー氏名
                .Parameters("UsrCompany").Value = dataHBKB1301.ProptxtUsrCompany.Text                                   'ユーザー所属会社
                .Parameters("UsrKyokuNM").Value = dataHBKB1301.ProptxtUsrKyokuNM.Text                                   'ユーザー所属局
                .Parameters("UsrBusyoNM").Value = dataHBKB1301.ProptxtUsrBusyoNM.Text                                   'ユーザー所属部署
                .Parameters("UsrTel").Value = dataHBKB1301.ProptxtUsrTel.Text                                           'ユーザー電話番号
                .Parameters("UsrMailAdd").Value = dataHBKB1301.ProptxtUsrMailAdd.Text                                   'ユーザーメールアドレス
                .Parameters("UsrContact").Value = dataHBKB1301.ProptxtUsrContact.Text                                   'ユーザー連絡先
                .Parameters("UsrRoom").Value = dataHBKB1301.ProptxtUsrRoom.Text                                         'ユーザールーム
                .Parameters("SetKyokuNM").Value = dataHBKB1301.ProptxtSetKyokuNM.Text                                   '設置局
                .Parameters("SetBusyoNM").Value = dataHBKB1301.ProptxtSetBusyoNM.Text                                   '設置部署
                .Parameters("SetRoom").Value = dataHBKB1301.ProptxtSetRoom.Text                                         '設置番組
                .Parameters("SetBuil").Value = dataHBKB1301.ProptxtSetBuil.Text                                         '設置建物
                .Parameters("SetFloor").Value = dataHBKB1301.ProptxtSetFloor.Text                                       '設置フロア

                'あいまい検索文字列設定
                strSerialAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB1301.PropTxtSerial.Text)                  '製造番号（あいまい）
                strManageBusyoAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB1301.ProptxtManageBusyoNM.Text)      '管理部署（あいまい）
                strUsrIDAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB1301.ProptxtUsrID.Text)                    'ユーザーID（あいまい）
                strUsrBusyoNMAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB1301.ProptxtUsrBusyoNM.Text)          'ユーザー所属部署（あいまい）
                strSetBusyoNMAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB1301.ProptxtSetBusyoNM.Text)          '設置部署（あいまい）
                strSetRoomAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB1301.ProptxtSetRoom.Text)                '設置番組/部屋    （あいまい）
                strSetBuilAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB1301.ProptxtSetBuil.Text)                '設置建物（あいまい）
                strSetFloorAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB1301.ProptxtSetFloor.Text)              '設置フロア（あいまい）

                .Parameters("SerialAimai").Value = strSerialAimai                                                       '製造番号（あいまい）
                .Parameters("ManageBusyoNMAimai").Value = strManageBusyoAimai                                           '管理部署（あいまい）
                .Parameters("UsrIDAimai").Value = strUsrIDAimai                                                         'ユーザーID（あいまい）
                .Parameters("UsrBusyoNMAimai").Value = strUsrBusyoNMAimai                                               'ユーザー所属部署（あいまい）
                .Parameters("SetBusyoNMAimai").Value = strSetBusyoNMAimai                                               '設置部署（あいまい）
                .Parameters("SetRoomAimai").Value = strSetRoomAimai                                                     '設置番組/部屋    （あいまい）
                .Parameters("SetBuilAimai").Value = strSetBuilAimai                                                     '設置建物（あいまい）
                .Parameters("SetFloorAimai").Value = strSetFloorAimai                                                   '設置フロア（あいまい）

                .Parameters("RegDT").Value = dataHBKB1301.PropDtmSysDate                                                '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                                         '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                                 '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB1301.PropDtmSysDate                                             '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                                          '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                                              '最終更新者ID
                .Parameters("CINmb").Value = dataHBKB1301.PropIntCINmb                                                  'CI種別CD

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
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規履歴番号取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewRirekiNoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKB1301 As DataHBKB1301) As Boolean

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
                .Parameters("CINmb").Value = DataHBKB1301.PropIntCINmb                                  'CI番号
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
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCIInfoRSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB1301 As DataHBKB1301) As Boolean

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
                .Parameters("RirekiNo").Value = DataHBKB1301.PropIntRirekiNo                            '履歴番号
                .Parameters("CINmb").Value = DataHBKB1301.PropIntCINmb                                  'CI番号
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
    ''' 【共通】CI部所有機器履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI部所有機器歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCIBuyRSql(ByRef Cmd As NpgsqlCommand, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertCIBuyRSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            '型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))                 '履歴番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With
            '値をセット
            With Cmd
                .Parameters("RirekiNo").Value = dataHBKB1301.PropIntRirekiNo                            '履歴番号
                .Parameters("CINmb").Value = dataHBKB1301.PropIntCINmb                                  'CI番号
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
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>登録理由履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertRegReasonRSql(ByRef Cmd As NpgsqlCommand, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB1301 As DataHBKB1301) As Boolean

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
                .Parameters("CINmb").Value = DataHBKB1301.PropIntCINmb                          'CI番号
                .Parameters("RirekiNo").Value = DataHBKB1301.PropIntRirekiNo                    '履歴番号
                .Parameters("RegReason").Value = DataHBKB1301.PropStrRegReason                  '登録理由
                .Parameters("RegDT").Value = DataHBKB1301.PropDtmSysDate                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                         '登録者ID
                .Parameters("UpdateDT").Value = DataHBKB1301.PropDtmSysDate                     '最終更新日時
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
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>原因リンク履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCauseLinkRSql(ByRef Cmd As NpgsqlCommand, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB1301 As DataHBKB1301) As Boolean

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
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))             '履歴番号
                .Add(New NpgsqlParameter("MngNmb", NpgsqlTypes.NpgsqlDbType.Integer))               '管理番号
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))           'プロセス区分
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))              '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = dataHBKB1301.PropIntCINmb                              'CI番号
                .Parameters("RirekiNo").Value = dataHBKB1301.PropIntRirekiNo                        '履歴番号
                .Parameters("MngNmb").Value = Integer.Parse(dataHBKB1301.PropRowReg.Item("MngNmb")) '管理番号
                .Parameters("ProcessKbn").Value = dataHBKB1301.PropRowReg.Item("ProcessKbn")        'プロセス区分
                .Parameters("RegDT").Value = dataHBKB1301.PropDtmSysDate                            '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                             '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB1301.PropDtmSysDate                         '最終更新日時
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
    ''' 番号のデータ有無取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <param name="strNum">[IN]番号</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>番号のデータ有無取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/02 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNumSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKB1301 As DataHBKB1301, ByRef strNum As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectNumSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("Num", NpgsqlTypes.NpgsqlDbType.Varchar))          '番号
                .Add(New NpgsqlParameter("CIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))      'CI種別コード
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))      'CI番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("Num").Value = strNum      '番号
                .Parameters("CIKbnCD").Value = CI_TYPE_KIKI                                 'CI種別コード：部所有機器
                .Parameters("CINmb").Value = dataHBKB1301.PropIntCINmb 'CI番号

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

End Class
