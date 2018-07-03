Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' 機器一括検索一覧Excel出力Sqlクラス
''' </summary>
''' <remarks>機器一括検索一覧Excel出力のSQLの作成・設定を行う
''' <para>作成情報：2012/07/16 y.ikushima
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKB0702

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK

    'SQL文宣言
    'Excel出力用マスターSQL(HBKF0005～HBKF0007の引数は0が通常,もうひとつは履歴番号【マスターでは使わない】）
    Private strSelectSearchMaster As String = "SELECT" & vbCrLf & _
                                                                    "CIT.CINmb AS CINmb," & vbCrLf & _
                                                                    "CKM.CIKbnNM AS CIKbnNM," & vbCrLf & _
                                                                    "KM.KindNM AS KindNM," & vbCrLf & _
                                                                    "CIT.Num AS Num," & vbCrLf & _
                                                                    "CSM.CIStateNM AS CIStateNM," & vbCrLf & _
                                                                    "CIT.Class1 AS Class1," & vbCrLf & _
                                                                    "CIT.Class2 AS Class2," & vbCrLf & _
                                                                    "CIT.CINM AS CINM," & vbCrLf & _
                                                                    "HBKF0003(CIT.CIOwnerCD) AS GroupNM," & vbCrLf & _
                                                                    "CIT.Sort AS Sort," & vbCrLf & _
                                                                    "CIT.CINaiyo AS CINaiyo," & vbCrLf & _
                                                                    "CIT.BIko1 AS BIko1," & vbCrLf & _
                                                                    "CIT.Biko2 AS BIko2," & vbCrLf & _
                                                                    "CIT.Biko3 AS BIko3," & vbCrLf & _
                                                                    "CIT.Biko4 AS BIko4," & vbCrLf & _
                                                                    "CIT.Biko5 AS BIko5," & vbCrLf & _
                                                                    "CASE WHEN CIT.FreeFlg1 = '" & FREE_FLG_OFF & "' THEN '" & FREE_FLG_OFF_NM & "' ELSE '" & FREE_FLG_ON_NM & "' END AS FreeFlg1," & vbCrLf & _
                                                                    "CASE WHEN CIT.FreeFlg2 = '" & FREE_FLG_OFF & "' THEN '" & FREE_FLG_OFF_NM & "' ELSE '" & FREE_FLG_ON_NM & "' END AS FreeFlg2," & vbCrLf & _
                                                                    "CASE WHEN CIT.FreeFlg3 = '" & FREE_FLG_OFF & "' THEN '" & FREE_FLG_OFF_NM & "' ELSE '" & FREE_FLG_ON_NM & "' END AS FreeFlg3," & vbCrLf & _
                                                                    "CASE WHEN CIT.FreeFlg4 = '" & FREE_FLG_OFF & "' THEN '" & FREE_FLG_OFF_NM & "' ELSE '" & FREE_FLG_ON_NM & "' END AS FreeFlg4," & vbCrLf & _
                                                                    "CASE WHEN CIT.FreeFlg5 = '" & FREE_FLG_OFF & "' THEN '" & FREE_FLG_OFF_NM & "' ELSE '" & FREE_FLG_ON_NM & "' END AS FreeFlg5," & vbCrLf & _
                                                                    "TO_CHAR(CIT.RegDT,'YYYY/MM/DD') AS RegDT," & vbCrLf & _
                                                                    "HBKF0003(CIT.RegGrpCD) AS RegGroupNM," & vbCrLf & _
                                                                    "CIT.RegID AS RegID," & vbCrLf & _
                                                                    "HBKF0004(CIT.RegID) AS RegHBKUsrNM," & vbCrLf & _
                                                                    "TO_CHAR(CIT.UpdateDT,'YYYY/MM/DD')  AS UpdateDT," & vbCrLf & _
                                                                    "HBKF0003(CIT.UpGrpCD) AS UpGroupNM," & vbCrLf & _
                                                                    "CIT.UpdateID AS UpdateID," & vbCrLf & _
                                                                    "HBKF0004(CIT.UpdateID) AS UpHBKUsrNM," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN '' ELSE CST.MemorySize END AS MemorySize," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBT.Kataban ELSE CST.Kataban END AS Kataban," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBT.Aliau ELSE '' END AS Aliau," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBT.Serial ELSE CST.Serial END AS Serial," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBT.MacAddress1 ELSE CST.MacAddress1 END AS MacAddress1," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBT.MacAddress2 ELSE CST.MacAddress2 END AS MacAddress2," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN '' ELSE CST.Fuzokuhin END AS Fuzokuhin," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN '' ELSE SKTM.SCKikiType END AS SCKikiType," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN NULL ELSE CST.SCKikiFixNmb END AS SCKikiFixNmb," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN '' ELSE CST.KikiState END AS KikiState," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN NULL ELSE CST.ImageNmb END AS ImageNmb," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN NULL ELSE CST.IntroductNmb END AS IntroductNmb," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN '' " & vbCrLf & _
                                                                    "ELSE " & vbCrLf & _
                                                                        "CASE WHEN CST.LeaseUpDT = '' THEN ''" & vbCrLf & _
                                                                        "ELSE TO_CHAR(TO_DATE(CST.LeaseUpDT,'YYYYMMDD'),'YYYY/MM/DD') END" & vbCrLf & _
                                                                    "END AS LeaseUpDT," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN '' ELSE CST.SCHokanKbn END AS SCHokanKbn," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN " & vbCrLf & _
                                                                        "CASE WHEN CBT.ZooKbn = '" & ZOO_KBN_FIN & "' THEN '" & ZOO_NM_FIN & "'" & vbCrLf & _
                                                                        "ELSE '" & ZOO_NM_UNFIN & "' END" & vbCrLf & _
                                                                    "ELSE '' END AS ZooKbn," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN OSNM ELSE '' END AS Soft_SoftNM," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN AntiVirusSoftNM ELSE '' END AS Vir_SoftNM," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN HBKF0008(CBT.DNSRegCD,'" & KIKISTATEKBN_DNS_REG & "') ELSE '' END AS DNS_KikiStateNM," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBT.NIC1 ELSE '' END AS NIC1," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBT.NIC2 ELSE '' END AS NIC2," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN " & vbCrLf & _
                                                                        "CASE WHEN CBT.ConnectDT = '' THEN ''" & vbCrLf & _
                                                                        "ELSE TO_CHAR(TO_DATE(CBT.ConnectDT,'YYYYMMDD'),'YYYY/MM/DD') END" & vbCrLf & _
                                                                    "ELSE " & vbCrLf & _
                                                                        "CASE WHEN CST.RentalStDT = '' THEN ''" & vbCrLf & _
                                                                        "ELSE TO_CHAR(TO_DATE(CST.RentalStDT,'YYYYMMDD'),'YYYY/MM/DD') END" & vbCrLf & _
                                                                    "END AS RentalStDT," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN " & vbCrLf & _
                                                                        "CASE WHEN CBT.ExpirationDT = '' THEN ''" & vbCrLf & _
                                                                        "ELSE TO_CHAR(TO_DATE(CBT.ExpirationDT,'YYYYMMDD'),'YYYY/MM/DD') END" & vbCrLf & _
                                                                    "ELSE " & vbCrLf & _
                                                                    "CASE WHEN CST.RentalEdDT = '' THEN ''" & vbCrLf & _
                                                                        "ELSE TO_CHAR(TO_DATE(CST.RentalEdDT,'YYYYMMDD'),'YYYY/MM/DD') END" & vbCrLf & _
                                                                    "END AS RentalEdDT," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN" & vbCrLf & _
                                                                        "CASE WHEN CBT.DeletDT ='' THEN '' " & vbCrLf & _
                                                                        "ELSE TO_CHAR(TO_DATE(CBT.DeletDT,'YYYYMMDD'),'YYYY/MM/DD') END" & vbCrLf & _
                                                                    "ELSE '' END AS DeletDT," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN" & vbCrLf & _
                                                                        "CASE WHEN CBT.LastInfoDT = '' THEN ''" & vbCrLf & _
                                                                        "ELSE TO_CHAR(TO_DATE(CBT.LastInfoDT,'YYYYMMDD'),'YYYY/MM/DD') END" & vbCrLf & _
                                                                    "ELSE" & vbCrLf & _
                                                                        "CASE WHEN CST.LastInfoDT = '' THEN '' " & vbCrLf & _
                                                                        "ELSE TO_CHAR(TO_DATE(CST.LastInfoDT,'YYYYMMDD'),'YYYY/MM/DD') END" & vbCrLf & _
                                                                    "END AS LastInfoDT," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBT.ConectReason ELSE '' END AS ConectReason," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN" & vbCrLf & _
                                                                        "CASE WHEN CBT.ExpirationUPDT = '' THEN ''" & vbCrLf & _
                                                                        "ELSE TO_CHAR(TO_DATE(CBT.ExpirationUPDT,'YYYYMMDD'),'YYYY/MM/DD') END " & vbCrLf & _
                                                                    "ELSE '' END AS ExpirationUPDT," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN " & vbCrLf & _
                                                                        "CASE WHEN CBT.InfoDT = '' THEN ''" & vbCrLf & _
                                                                        "ELSE TO_CHAR(TO_DATE(CBT.InfoDT,'YYYYMMDD'),'YYYY/MM/DD') END" & vbCrLf & _
                                                                    "ELSE '' END AS InfoDT," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN" & vbCrLf & _
                                                                        "CASE WHEN CBT.NumInfoKbn = '" & NUMINFO_KBN_UNFIN & "' THEN '" & NUMINFO_NM_UNFIN & "'" & vbCrLf & _
                                                                        "ELSE '" & NUMINFO_NM_FIN & "' END" & vbCrLf & _
                                                                    "ELSE '' END AS NumInfoKbn," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN" & vbCrLf & _
                                                                        "CASE WHEN CBT.SealSendkbn = '" & SEALSEND_KBN_UNFIN & "' THEN '" & SEALSEND_NM_UNFIN & "'" & vbCrLf & _
                                                                        "ELSE '" & SEALSEND_NM_FIN & "' END" & vbCrLf & _
                                                                    "ELSE '' END AS SealSendkbn," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN" & vbCrLf & _
                                                                        "CASE WHEN CBT.AntiVirusSofCheckKbn = '" & ANTIVIRUSSOFCHECK_KBN_UNFIN & "' THEN '" & ANTIVIRUSSOFCHECK_NM_UNFIN & "'" & vbCrLf & _
                                                                        "ELSE '" & ANTIVIRUSSOFCHECK_NM_FIN & "' END" & vbCrLf & _
                                                                    "ELSE '' END AS AntiVirusSofCheckKbn," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN " & vbCrLf & _
                                                                        "CASE WHEN CBT.AntiVirusSofCheckDT = '' THEN '' " & vbCrLf & _
                                                                        "ELSE TO_CHAR(TO_DATE(CBT.AntiVirusSofCheckDT,'YYYYMMDD'),'YYYY/MM/DD') END " & vbCrLf & _
                                                                    "ELSE '' END AS AntiVirusSofCheckDT," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBT.BusyoKikiBiko ELSE '' END AS BusyoKikiBiko," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBT.ManageKyokuNM ELSE CST.ManageKyokuNM END AS ManageKyokuNM," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBT.ManageBusyoNM ELSE CST.ManageBusyoNM END AS ManageBusyoNM," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBT.WorkFromNmb ELSE CST.WorkFromNmb END AS WorkFromNmb," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN '' ELSE HBKF0008(CST.KikiUseCD,'" & KIKISTATEKBN_KIKI_RIYOKEITAI & "') END AS Keitai_KikiStateNM," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN HBKF0008(CBT.IPUseCD,'" & KIKISTATEKBN_IP_WARIATE & "') ELSE HBKF0008(CST.IPUseCD,'" & KIKISTATEKBN_IP_WARIATE & "') END AS Keitai_KikiStateNM," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBT.FixedIP ELSE CST.FixedIP END AS FixedIP," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBT.UsrID ELSE CST.UsrID END AS UsrID," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBT.UsrNM ELSE CST.UsrNM END AS UsrNM," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBT.UsrCompany ELSE CST.UsrCompany END AS UsrCompany," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBT.UsrKyokuNM ELSE CST.UsrKyokuNM END AS UsrKyokuNM," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBT.UsrBusyoNM ELSE CST.UsrBusyoNM END AS UsrBusyoNM," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBT.UsrTel ELSE CST.UsrTel END AS UsrTel," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBT.UsrMailAdd ELSE CST.UsrMailAdd END AS UsrMailAdd," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBT.UsrContact ELSE CST.UsrContact END AS UsrContact," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBT.UsrRoom ELSE CST.UsrRoom END AS UsrRoom," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBT.SetKyokuNM ELSE CST.SetKyokuNM END AS SetKyokuNM," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBT.SetBusyoNM ELSE CST.SetBusyoNM END AS SetBusyoNM," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBT.SetRoom ELSE CST.SetRoom END AS SetRoom," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBT.SetBuil ELSE CST.SetBuil END AS SetBuil," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBT.SetFloor ELSE CST.SetFloor END AS SetFloor," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN '' ELSE CST.SetDeskNo END AS SetDeskNo," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN '' ELSE CST.SetLANLength END AS SetLANLength," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN '' ELSE CST.SetLANNum END AS SetLANNum," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN '' ELSE CST.SetSocket END AS SetSocket," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN '' ELSE OST.SoftNM END AS Op_SoftNM," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN '' ELSE HBKF0006(CIT.CINmb,0,0) END AS Op_SoftNM," & vbCrLf & _
                                                                    "CASE WHEN CIT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN '' ELSE HBKF0007(CIT.CINmb,0,0) END AS UsrInfo" & vbCrLf & _
                                                                "FROM" & vbCrLf & _
                                                                    "ci_info_tb CIT" & vbCrLf & _
                                                                    "LEFT OUTER JOIN ci_sap_tb CST ON CIT.CINmb = CST.CINmb" & vbCrLf & _
                                                                    "LEFT OUTER JOIN ci_buy_tb CBT ON CIT.CINmb = CBT.CINmb" & vbCrLf & _
                                                                    "LEFT OUTER JOIN ci_kind_mtb CKM ON CIT.CIKbnCD = CKM.CIKbnCD AND CKM.JtiFlg = '0' " & vbCrLf & _
                                                                    "LEFT OUTER JOIN kind_mtb KM ON CIT.KindCD = KM.KindCD AND KM.JtiFlg = '0'" & vbCrLf & _
                                                                    "LEFT OUTER JOIN cistate_mtb CSM ON CIT.CIStatusCD = CSM.CIStateCD AND CSM.JtiFlg = '0'" & vbCrLf & _
                                                                    "LEFT OUTER JOIN sap_kiki_type_mtb SKTM ON CST.TypeKbn = SKTM.SCKikiCD AND SKTM.JtiFlg = '0'" & vbCrLf & _
                                                                    "LEFT OUTER JOIN (SELECT CINmb,STRING_AGG(SoftNM, '／') AS SoftNM FROM optsoft_tb ot LEFT OUTER JOIN soft_mtb sm ON ot.SoftCD = sm.SoftCD GROUP BY CINmb) OST ON OST.CINmb = CST.CINmb " & vbCrLf & _
                                                                    "WHERE CIT.CIKbnCD IN  (:CIKbnCDSuport , :CIKbnCDKiki) "

    'Excel出力用導入SQL
    Private strSelcetSearchIntro As String = "SELECT" & vbCrLf & _
                                                                    "IT.IntroductNmb AS IntroductNmb," & vbCrLf & _
                                                                    "KM.KindNM AS KindNM," & vbCrLf & _
                                                                    "IT.KikiNmbFrom AS KikiNmbFrom," & vbCrLf & _
                                                                    "IT.KikiNmbTo AS KikiNmbTo," & vbCrLf & _
                                                                    "IT.Class1 AS Class1," & vbCrLf & _
                                                                    "IT.Class2 AS Class2," & vbCrLf & _
                                                                    "IT.CINM AS CINM," & vbCrLf & _
                                                                    "IT.Kataban AS Kataban," & vbCrLf & _
                                                                    "IT.Fuzokuhin AS Fuzokuhin," & vbCrLf & _
                                                                    "IT.SetNmb AS SetNmb," & vbCrLf & _
                                                                    "SKTM.SCKikiType AS SCKikiType," & vbCrLf & _
                                                                    "IT.IntroductBiko AS IntroductBiko," & vbCrLf & _
                                                                    "CASE WHEN IT.SCHokanKbn = '0' THEN 'OFF' ELSE 'ON' END AS SCHokanKbn," & vbCrLf & _
                                                                    "CASE WHEN IT.IntroductDelKbn = '0' THEN 'OFF' ELSE 'ON' END AS IntroductDelKbn," & vbCrLf & _
                                                                    "CASE WHEN IT.IntroductKbn = '" & INTRODUCT_KBN_KEIHI & "' THEN '" & INTRODUCT_KBN_KEIHI_NM & "' ELSE '" & INTRODUCT_KBN_LEASE_NM & "' END AS IntroductKbn," & vbCrLf & _
                                                                    "CASE WHEN IT.HosyoUmu = ' " & HOSYO_UMU_ARI & "' THEN '" & HOSYO_UMU_ARI_NM & "' WHEN IT.HosyoUmu = '" & HOSYO_UMU_NASHI & "' THEN '" & HOSYO_UMU_NASHI_NM & "' ELSE '" & HOSYO_UMU_FUMEI_NM & "' END AS HosyoUmu," & vbCrLf & _
                                                                    "IT.HosyoPlace AS HosyoPlace," & vbCrLf & _
                                                                    "CASE WHEN IT.HosyoDelDT = '' THEN ''" & vbCrLf & _
                                                                    "ELSE TO_CHAR(TO_DATE(IT.HosyoDelDT,'YYYYMMDD'),'YYYY/MM/DD') END AS HosyoDelDT," & vbCrLf & _
                                                                    "IT.LeaseNmb AS LeaseNmb," & vbCrLf & _
                                                                    "IT.LeaseCompany AS LeaseCompany," & vbCrLf & _
                                                                    "CASE WHEN IT.LeaseUpDT = '' THEN ''" & vbCrLf & _
                                                                    "ELSE TO_CHAR(TO_DATE(IT.LeaseUpDT,'YYYYMMDD'),'YYYY/MM/DD') END AS LeaseUpDT," & vbCrLf & _
                                                                    "IT.MakerHosyoTerm AS MakerHosyoTerm," & vbCrLf & _
                                                                    "IT.EOS AS EOS," & vbCrLf & _
                                                                    "CASE WHEN IT.DelScheduleDT = '' THEN ''" & vbCrLf & _
                                                                    "ELSE TO_CHAR(TO_DATE(IT.DelScheduleDT,'YYYYMMDD'),'YYYY/MM/DD') END AS DelScheduleDT," & vbCrLf & _
                                                                    "CASE WHEN IT.IntroductStDT = '' THEN ''" & vbCrLf & _
                                                                    "ELSE TO_CHAR(TO_DATE(IT.IntroductStDT,'YYYYMMDD'),'YYYY/MM/DD') END AS IntroductStDT," & vbCrLf & _
                                                                    "TO_CHAR(IT.RegDT,'YYYY/MM/DD') AS RegDT," & vbCrLf & _
                                                                    "HBKF0003(IT.RegGrpCD) AS RegGroupNM," & vbCrLf & _
                                                                    "IT.RegID AS RegID," & vbCrLf & _
                                                                    "HBKF0004(IT.RegID) AS RegHBKUsrNM," & vbCrLf & _
                                                                    "TO_CHAR(IT.UpdateDT,'YYYY/MM/DD')  AS UpdateDT," & vbCrLf & _
                                                                    "HBKF0003(IT.UpGrpCD) AS UpGroupNM," & vbCrLf & _
                                                                    "IT.UpdateID AS UpdateID," & vbCrLf & _
                                                                    "HBKF0004(IT.UpdateID) AS UpHBKUsrNM" & vbCrLf & _
                                                            "FROM introduct_tb IT" & vbCrLf & _
                                                            "LEFT OUTER JOIN sap_kiki_type_mtb SKTM ON IT.TypeKbn = SKTM.SCKikiCD " & vbCrLf & _
                                                            "LEFT OUTER JOIN kind_mtb KM ON IT.KindCD = KM.KindCD AND KM.JtiFlg = '0'"

    '[Mod] 2012/10/10 s.yamaguchi 作業、作業区分取得時のCI区分修正 START
    'Excel出力用履歴SQL(HBKF0005～HBKF0007の引数は0が通常、1が履歴からデータ取得、もうひとつの引数が履歴番号）
    Private strSelectSearchRireki As String = "SELECT" & vbCrLf & _
                                                                    "CIRT.CINmb AS CINmb," & vbCrLf & _
                                                                    "CIRT.RirekiNo AS RirekiNo," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_SUPORT & "' THEN WM.WorkNM ELSE '' END AS WorkNM," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_SUPORT & "' THEN WKM.WorkKbnNM ELSE '' END AS WorkKbnNM," & vbCrLf & _
                                                                    "RT.RegReason," & vbCrLf & _
                                                                    "HBKF0009(CIRT.CINmb,CIRT.RirekiNo) AS CauseLink," & vbCrLf & _
                                                                    "CASE rrt.ChgFlg" & vbCrLf &
                                                                    "WHEN '" & CHANGE_FLG_ON & "'" & vbCrLf &
                                                                    "THEN (SELECT km.KindNM || ct.Num FROM CI_INFO_TB ct JOIN KIND_MTB km ON ct.CIKbnCD = km.CIKbnCD AND ct.KindCD = km.KindCD AND ct.CINmb = RT.ChgCINmb)" & vbCrLf &
                                                                    "ELSE '' END AS ChgKiki," & vbCrLf &
                                                                    "RT.WorkBiko AS WorkBiko," & vbCrLf &
                                                                    "CKM.CIKbnNM AS CIKbnNM," & vbCrLf & _
                                                                    "KM.KindNM AS KindNM," & vbCrLf & _
                                                                    "CIRT.Num AS Num," & vbCrLf & _
                                                                    "CSM.CIStateNM AS CIStateNM," & vbCrLf & _
                                                                    "CIRT.Class1 AS Class1," & vbCrLf & _
                                                                    "CIRT.Class2 AS Class2," & vbCrLf & _
                                                                    "CIRT.CINM AS CINM," & vbCrLf & _
                                                                    "HBKF0003(CIRT.CIOwnerCD) AS GroupNM," & vbCrLf & _
                                                                    "CIRT.CINaiyo AS CINaiyo," & vbCrLf & _
                                                                    "CIRT.BIko1 AS BIko1," & vbCrLf & _
                                                                    "CIRT.Biko2 AS BIko2," & vbCrLf & _
                                                                    "CIRT.Biko3 AS BIko3," & vbCrLf & _
                                                                    "CIRT.Biko4 AS BIko4," & vbCrLf & _
                                                                    "CIRT.Biko5 AS BIko5," & vbCrLf & _
                                                                    "CASE WHEN CIRT.FreeFlg1 = '" & FREE_FLG_OFF & "' THEN '" & FREE_FLG_OFF_NM & "' ELSE '" & FREE_FLG_ON_NM & "' END AS FreeFlg1," & vbCrLf & _
                                                                    "CASE WHEN CIRT.FreeFlg2 = '" & FREE_FLG_OFF & "' THEN '" & FREE_FLG_OFF_NM & "' ELSE '" & FREE_FLG_ON_NM & "' END AS FreeFlg2," & vbCrLf & _
                                                                    "CASE WHEN CIRT.FreeFlg3 = '" & FREE_FLG_OFF & "' THEN '" & FREE_FLG_OFF_NM & "' ELSE '" & FREE_FLG_ON_NM & "' END AS FreeFlg3," & vbCrLf & _
                                                                    "CASE WHEN CIRT.FreeFlg4 = '" & FREE_FLG_OFF & "' THEN '" & FREE_FLG_OFF_NM & "' ELSE '" & FREE_FLG_ON_NM & "' END AS FreeFlg4," & vbCrLf & _
                                                                    "CASE WHEN CIRT.FreeFlg5 = '" & FREE_FLG_OFF & "' THEN '" & FREE_FLG_OFF_NM & "' ELSE '" & FREE_FLG_ON_NM & "' END AS FreeFlg5," & vbCrLf & _
                                                                    "TO_CHAR(CIRT.RegDT,'YYYY/MM/DD') AS RegDT," & vbCrLf & _
                                                                    "HBKF0003(CIRT.RegGrpCD) AS RegGroupNM," & vbCrLf & _
                                                                    "CIRT.RegID AS RegID," & vbCrLf & _
                                                                    "HBKF0004(CIRT.RegID) AS RegHBKUsrNM," & vbCrLf & _
                                                                    "TO_CHAR(CIRT.UpdateDT,'YYYY/MM/DD')  AS UpdateDT," & vbCrLf & _
                                                                    "HBKF0003(CIRT.UpGrpCD) AS UpGroupNM," & vbCrLf & _
                                                                    "CIRT.UpdateID AS UpdateID," & vbCrLf & _
                                                                    "HBKF0004(CIRT.UpdateID) AS UpHBKUsrNM," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN '' ELSE CSRT.MemorySize END AS MemorySize," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBRT.Kataban ELSE CSRT.Kataban END AS Kataban," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBRT.Aliau ELSE '' END AS Aliau," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBRT.Serial ELSE CSRT.Serial END AS Serial," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBRT.MacAddress1 ELSE CSRT.MacAddress1 END AS MacAddress1," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBRT.MacAddress2 ELSE CSRT.MacAddress2 END AS MacAddress2," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN '' ELSE CSRT.Fuzokuhin END AS Fuzokuhin," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN '' ELSE SKTM.SCKikiType END AS SCKikiType," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN '' ELSE CSRT.SCKikiFixNmb END AS SCKikiFixNmb," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN '' ELSE CSRT.KikiState END AS KikiState," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN '' ELSE CSRT.ImageNmb END AS ImageNmb," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN NULL ELSE CSRT.IntroductNmb END AS IntroductNmb," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN '' " & vbCrLf & _
                                                                    "ELSE " & vbCrLf & _
                                                                        "CASE WHEN CSRT.LeaseUpDT = '' THEN ''" & vbCrLf & _
                                                                        "ELSE TO_CHAR(TO_DATE(CSRT.LeaseUpDT,'YYYYMMDD'),'YYYY/MM/DD') END" & vbCrLf & _
                                                                    "END AS LeaseUpDT," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN '' ELSE CSRT.SCHokanKbn END AS SCHokanKbn," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN " & vbCrLf & _
                                                                        "CASE WHEN CBRT.ZooKbn = '" & ZOO_KBN_FIN & "' THEN '" & ZOO_NM_FIN & "'" & vbCrLf & _
                                                                        "ELSE '" & ZOO_NM_UNFIN & "' END " & vbCrLf & _
                                                                    "ELSE '' END AS ZooKbn," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN OSNM ELSE '' END AS Soft_SoftNM," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN AntiVirusSoftNM ELSE '' END AS Vir_SoftNM," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN HBKF0008(CBRT.DNSRegCD,'" & KIKISTATEKBN_DNS_REG & "')  ELSE '' END AS DNS_KikiStateNM," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBRT.NIC1 ELSE '' END AS NIC1," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBRT.NIC2 ELSE '' END AS NIC2," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN" & vbCrLf & _
                                                                        "CASE WHEN CBRT.ConnectDT = '' THEN ''" & vbCrLf & _
                                                                        "ELSE TO_CHAR(TO_DATE(CBRT.ConnectDT,'YYYYMMDD'),'YYYY/MM/DD') END" & vbCrLf & _
                                                                    "ELSE" & vbCrLf & _
                                                                        "CASE WHEN CSRT.RentalStDT = '' THEN ''" & vbCrLf & _
                                                                        "ELSE TO_CHAR(TO_DATE(CSRT.RentalStDT,'YYYYMMDD'),'YYYY/MM/DD') END " & vbCrLf & _
                                                                    "END AS RentalStDT," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN" & vbCrLf & _
                                                                        "CASE WHEN CBRT.ExpirationDT = '' THEN ''" & vbCrLf & _
                                                                        "ELSE TO_CHAR(TO_DATE(CBRT.ExpirationDT,'YYYYMMDD'),'YYYY/MM/DD') END " & vbCrLf & _
                                                                    "ELSE" & vbCrLf & _
                                                                        "CASE WHEN CSRT.RentalEdDT = '' THEN ''" & vbCrLf & _
                                                                        "ELSE TO_CHAR(TO_DATE(CSRT.RentalEdDT,'YYYYMMDD'),'YYYY/MM/DD')  END" & vbCrLf & _
                                                                    "END AS RentalEdDT," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN" & vbCrLf & _
                                                                        "CASE WHEN CBRT.DeletDT = '' THEN ''" & vbCrLf & _
                                                                        "ELSE TO_CHAR(TO_DATE(CBRT.DeletDT,'YYYYMMDD'),'YYYY/MM/DD') END" & vbCrLf & _
                                                                    "ELSE '' END AS DeletDT," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN" & vbCrLf & _
                                                                        "CASE WHEN CBRT.LastInfoDT = '' THEN ''" & vbCrLf & _
                                                                        "ELSE TO_CHAR(TO_DATE(CBRT.LastInfoDT,'YYYYMMDD'),'YYYY/MM/DD')  END" & vbCrLf & _
                                                                    "ELSE" & vbCrLf & _
                                                                        "CASE WHEN CSRT.LastInfoDT = '' THEN '' " & vbCrLf & _
                                                                        "ELSE TO_CHAR(TO_DATE(CSRT.LastInfoDT,'YYYYMMDD'),'YYYY/MM/DD')  END" & vbCrLf & _
                                                                    "END AS LastInfoDT," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBRT.ConectReason ELSE '' END AS ConectReason," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN" & vbCrLf & _
                                                                        "CASE WHEN CBRT.ExpirationUPDT = '' THEN ''" & vbCrLf & _
                                                                        "ELSE TO_CHAR(TO_DATE(CBRT.ExpirationUPDT,'YYYYMMDD'),'YYYY/MM/DD')  END" & vbCrLf & _
                                                                    "ELSE ''END AS ExpirationUPDT," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN" & vbCrLf & _
                                                                        "CASE WHEN CBRT.InfoDT = '' THEN ''" & vbCrLf & _
                                                                        "ELSE TO_CHAR(TO_DATE(CBRT.InfoDT,'YYYYMMDD'),'YYYY/MM/DD')  END" & vbCrLf & _
                                                                    "ELSE ''END AS InfoDT," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN " & vbCrLf & _
                                                                        "CASE WHEN CBRT.NumInfoKbn = '" & NUMINFO_KBN_UNFIN & "' THEN '" & NUMINFO_NM_UNFIN & "'" & vbCrLf & _
                                                                        "ELSE '" & NUMINFO_NM_FIN & "' END" & vbCrLf & _
                                                                    "END AS NumInfoKbn," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN " & vbCrLf & _
                                                                        "CASE WHEN CBRT.SealSendkbn = '" & SEALSEND_KBN_UNFIN & "' THEN '" & SEALSEND_NM_UNFIN & "'" & vbCrLf & _
                                                                        "ELSE '" & SEALSEND_NM_FIN & "' END" & vbCrLf & _
                                                                    "END AS SealSendkbn," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN " & vbCrLf & _
                                                                        "CASE WHEN CBRT.AntiVirusSofCheckKbn = '" & ANTIVIRUSSOFCHECK_KBN_UNFIN & "' THEN '" & ANTIVIRUSSOFCHECK_NM_UNFIN & "'" & vbCrLf & _
                                                                        "ELSE '" & ANTIVIRUSSOFCHECK_NM_FIN & "' END " & vbCrLf & _
                                                                    "END AS AntiVirusSofCheckKbn," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN " & vbCrLf & _
                                                                        "CASE WHEN CBRT.AntiVirusSofCheckDT = '' THEN '' " & vbCrLf & _
                                                                        "ELSE  TO_CHAR(TO_DATE(CBRT.ExpirationUPDT,'YYYYMMDD'),'YYYY/MM/DD')  END" & vbCrLf & _
                                                                    "ELSE '' END AS AntiVirusSofCheckDT," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBRT.BusyoKikiBiko ELSE '' END AS BusyoKikiBiko," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBRT.ManageKyokuNM ELSE CSRT.ManageKyokuNM END AS ManageKyokuNM," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBRT.ManageBusyoNM ELSE CSRT.ManageBusyoNM END AS ManageBusyoNM," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBRT.WorkFromNmb ELSE CSRT.WorkFromNmb END AS WorkFromNmb," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN '' ELSE HBKF0008(CSRT.KikiUseCD,'" & KIKISTATEKBN_KIKI_RIYOKEITAI & "')  END AS Keitai_KikiStateNM," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN HBKF0008(CBRT.IPUseCD,'" & KIKISTATEKBN_IP_WARIATE & "')  ELSE HBKF0008(CSRT.IPUseCD,'" & KIKISTATEKBN_IP_WARIATE & "') END AS Keitai_KikiStateNM," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBRT.FixedIP ELSE CSRT.FixedIP END AS FixedIP," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBRT.UsrID ELSE CSRT.UsrID END AS UsrID," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBRT.UsrNM ELSE CSRT.UsrNM END AS UsrNM," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBRT.UsrCompany ELSE CSRT.UsrCompany END AS UsrCompany," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBRT.UsrKyokuNM ELSE CSRT.UsrKyokuNM END AS UsrKyokuNM," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBRT.UsrBusyoNM ELSE CSRT.UsrBusyoNM END AS UsrBusyoNM," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBRT.UsrTel ELSE CSRT.UsrTel END AS UsrTel," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBRT.UsrMailAdd ELSE CSRT.UsrMailAdd END AS UsrMailAdd," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBRT.UsrContact ELSE CSRT.UsrContact END AS UsrContact," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBRT.UsrRoom ELSE CSRT.UsrRoom END AS UsrRoom," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBRT.SetKyokuNM ELSE CSRT.SetKyokuNM END AS SetKyokuNM," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBRT.SetBusyoNM ELSE CSRT.SetBusyoNM END AS SetBusyoNM," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBRT.SetRoom ELSE CSRT.SetRoom END AS SetRoom," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBRT.SetBuil ELSE CSRT.SetBuil END AS SetBuil," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN CBRT.SetFloor ELSE CSRT.SetFloor END AS SetFloor," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN '' ELSE CSRT.SetDeskNo END AS SetDeskNo," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN '' ELSE CSRT.SetLANLength END AS SetLANLength," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN '' ELSE CSRT.SetLANNum END AS SetLANNum," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN '' ELSE CSRT.SetSocket END AS SetSocket," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN '' ELSE OST.SoftNM END AS Op_SoftNM," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN '' ELSE HBKF0006(CIRT.CINmb,1,CIRT.RirekiNo) END AS Op_SoftNM," & vbCrLf & _
                                                                    "CASE WHEN CIRT.CIKbnCD = '" & CI_TYPE_KIKI & "' THEN '' ELSE HBKF0007(CIRT.CINmb,1,CIRT.RirekiNo) END AS UsrInfo" & vbCrLf & _
                                                                "FROM" & vbCrLf & _
                                                                "ci_info_rtb CIRT" & vbCrLf & _
                                                                "LEFT OUTER JOIN ci_sap_rtb CSRT ON CIRT.CINmb = CSRT.CINmb AND CIRT.RirekiNo = CSRT.RirekiNo" & vbCrLf & _
                                                                "LEFT OUTER JOIN ci_buy_rtb CBRT ON CIRT.CINmb = CBRT.CINmb AND CIRT.RirekiNo = CBRT.RirekiNo" & vbCrLf & _
                                                                "LEFT OUTER JOIN regreason_rtb RT ON CIRT.CINmb = RT.CINmb AND CIRT.RirekiNo = RT.RirekiNo" & vbCrLf & _
                                                                "LEFT OUTER JOIN work_mtb WM ON RT.WorkCD = WM.WorkCD" & vbCrLf & _
                                                                "LEFT OUTER JOIN workkbn_mtb WKM ON RT.WorkKbnCD = WKM.WorkKbnCD" & vbCrLf & _
                                                                "LEFT OUTER JOIN ci_kind_mtb CKM ON CIRT.CIKbnCD = CKM.CIKbnCD AND CKM.JtiFlg = '0'" & vbCrLf & _
                                                                "LEFT OUTER JOIN kind_mtb KM ON CIRT.KindCD = KM.KindCD AND KM.JtiFlg = '0'" & vbCrLf & _
                                                                "LEFT OUTER JOIN cistate_mtb CSM ON CIRT.CIStatusCD = CSM.CIStateCD AND CSM.JtiFlg = '0'" & vbCrLf & _
                                                                "LEFT OUTER JOIN sap_kiki_type_mtb SKTM ON CSRT.TypeKbn = SKTM.SCKikiCD AND SKTM.JtiFlg = '0'" & vbCrLf &
                                                                "LEFT OUTER JOIN regreason_rtb RRT ON CIRT.CINmb = RRT.CINmb AND CIRT.RirekiNo = RRT.RirekiNo " & vbCrLf & _
                                                                "LEFT OUTER JOIN (SELECT CINmb,RirekiNo,STRING_AGG(SoftNM, '／') AS SoftNM FROM optsoft_rtb ost LEFT OUTER JOIN soft_mtb sm ON ost.SoftCD = sm.SoftCD GROUP BY CINmb,RirekiNo) OST ON OST.CINmb = CSRT.CINmb AND OST.RirekiNo = CSRT.RirekiNo" & vbCrLf & _
                                                                "WHERE CIRT.CIKbnCD IN ( :CIKbnCDSuport , :CIKbnCDKiki ) "
    '[Mod] 2012/10/10 s.yamaguchi 作業、作業区分取得時のCI区分修正 END



    ''' <summary>
    ''' マスターデータ取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0702">[IN/OUT]機器一括検索一覧Excel出力Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>Excel出力用マスターデータ取得のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectMasterForExcel(ByRef Adapter As NpgsqlDataAdapter, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB0702 As DataHBKB0702) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""                   'SQL文
        Dim strSearch As String = ""                '検索条件
        Dim strIntroductNo() As String = Nothing    '導入番号検索用配列
        Dim strFreeText() As String = Nothing       'フリーテキスト検索用配列
        '[add] 2015/08/21 y.naganuma フリーワード追加対応 START
        Dim strFreeWord() As String = Nothing       'フリーワード検索用配列
        '[add] 2015/08/21 y.naganuma フリーワード追加対応 END

        Try
            'SQL文設定

            'SQL文(SELECT)
            strSQL = strSelectSearchMaster

            '検索条件設定
            With dataHBKB0702
                '種別
                If .PropStrKind <> Nothing Then
                    strSearch &= " AND CIT.KindCD IN ( " & .PropStrKind & ") "
                End If
                'ステータス
                If .PropStrStateNM <> Nothing Then
                    strSearch &= " AND CIT.CIStatusCD IN ( " & .PropStrStateNM & ") "
                End If
                '導入番号
                If .PropStrIntroductNo <> Nothing Then

                    ' 検索文字列の分割
                    strIntroductNo = CommonHBK.CommonLogicHBK.GetSearchStringList(.PropStrIntroductNo, SPLIT_MODE_OR)
                    strIntroductNo = CommonHBK.CommonLogicHBK.RemoveCharStringList(strIntroductNo)
                    If strIntroductNo.Length <> 0 Then
                        strSearch &= " AND  "
                        strSearch &= " ("
                        For intCnt = 0 To strIntroductNo.Count - 1
                            strSearch &= "CST.IntroductNmb = :IntroductNmb" + intCnt.ToString()
                            If intCnt <> strIntroductNo.Count - 1 Then
                                strSearch &= " OR "
                            End If
                        Next
                        strSearch &= ") "
                    End If
                End If

                '番号検索
                If .PropStrNum.Trim <> "" Then
                    strSearch &= " AND CIT.NUM = LPAD(:NUM, 5, '0') "
                End If
                'タイプ検索
                If .PropStrTypeKbn <> "" Then
                    strSearch &= " AND CST.TypeKbn = :TypeKbn "
                End If
                '機器利用形態検索
                If .PropStrKikiUse <> "" Then
                    strSearch &= " AND CST.KikiUseCD = :KikiUseCD "
                End If
                'イメージ番号検索
                If .PropStrImageNmb.Trim <> "" Then
                    strSearch &= " AND CST.ImageNmb = :ImageNmb "
                End If
                'オプションソフト検索
                If .PropStrOptionSoft <> Nothing Then
                    'strSearch &= " AND OT.SoftCD = :SoftCD "
                    '[EDIT] 2013/04/22 r.hoshino 課題No112対応 START
                    'strSearch &= " AND EXISTS (SELECT 1 FROM optsoft_tb os WHERE os.cinmb = CIT.cinmb AND os.softcd) = :softcd) "
                    strSearch &= " AND EXISTS (SELECT 1 FROM optsoft_tb os WHERE os.cinmb = CIT.cinmb AND os.softcd = :softcd) "
                    '[EDIT] 2013/04/22 r.hoshino 課題No112対応 END
                End If
                'ユーザーID検索
                If .PropStrUsrID.Trim <> "" Then
                    strSearch &= " AND (CST.UsrIDAimai = :UsrIDAimai OR CBT.UsrIDAimai = :UsrIDAimai) "
                End If
                'サービスセンター保管機検索
                If .PropStrSCHokanKbn <> "" Then
                    strSearch &= " AND CST.SCHokanKbn = :SCHokanKbn "
                End If
                'フリーフラグ1
                If .PropStrFreeFlg1 <> "" Then
                    strSearch &= " AND CIT.FreeFlg1 = :FreeFlg1 "
                End If
                'フリーフラグ2
                If .PropStrFreeFlg2 <> "" Then
                    strSearch &= " AND CIT.FreeFlg2 = :FreeFlg2 "
                End If
                'フリーフラグ3
                If .PropStrFreeFlg3 <> "" Then
                    strSearch &= " AND CIT.FreeFlg3 = :FreeFlg3 "
                End If
                'フリーフラグ4
                If .PropStrFreeFlg4 <> "" Then
                    strSearch &= " AND CIT.FreeFlg4 = :FreeFlg4 "
                End If
                'フリーフラグ5
                If .PropStrFreeFlg5 <> "" Then
                    strSearch &= " AND CIT.FreeFlg5 = :FreeFlg5 "
                End If

                '管理部署検索(あいまい検索)
                If .PropStrManageBusyoNM.Trim <> "" Then
                    strSearch &= " AND (CST.ManageBusyoNMAimai LIKE :ManageBusyoNMAimai OR CBT.ManageBusyoNMAimai LIKE :ManageBusyoNMAimai) "
                End If
                '設置部署検索(あいまい検索)
                If .PropStrSetBusyoNM.Trim <> "" Then
                    strSearch &= " AND (CST.SetBusyoNMAimai LIKE :SetBusyoNMAimai OR CBT.SetBusyoNMAimai LIKE :SetBusyoNMAimai) "
                End If
                '設置建物検索(あいまい検索)
                If .PropStrSetbuil.Trim <> "" Then
                    strSearch &= " AND (CST.SetBuilAimai LIKE :SetBuilAimai OR CBT.SetBuilAimai LIKE :SetBuilAimai) "
                End If
                '設置フロア検索(あいまい検索)
                If .PropStrSetFloor.Trim <> "" Then
                    strSearch &= " AND (CST.SetFloorAimai LIKE :SetFloorAimai OR CBT.SetFloorAimai LIKE :SetFloorAimai) "
                End If
                '設置番組/部屋検索(あいまい検索)
                If .PropStrSetRoom.Trim <> "" Then
                    strSearch &= " AND (CST.SetRoomAimai LIKE :SetRoomAimai OR CBT.SetRoomAimai LIKE :SetRoomAimai) "
                End If
                '製造番号検索(あいまい検索)
                If .PropStrSerial.Trim <> "" Then
                    strSearch &= " AND (CST.SerialAimai LIKE :SerialAimai OR CBT.SerialAimai LIKE :SerialAimai) "
                End If
                'フリーテキスト検索(あいまい検索)
                If .PropStrBIko.Trim <> "" Then
                    ' 検索文字列の分割
                    strFreeText = CommonHBK.CommonLogicHBK.GetSearchStringList(.PropStrBIko, SPLIT_MODE_AND)
                    If strFreeText.Length <> 0 Then
                        strSearch &= " AND "
                        strSearch &= " ("
                        For intCnt = 0 To strFreeText.Count - 1
                            strSearch &= "CIT.BikoAimai like :BikoAimai" + intCnt.ToString()
                            If intCnt <> strFreeText.Count - 1 Then
                                strSearch &= " AND "
                            End If
                        Next
                        strSearch &= ") "
                    End If
                End If

                '[add] 2015/08/21 y.naganuma フリーワード追加対応 START

                'フリーワード検索(あいまい検索)

                If .PropStrFreeWord.Trim <> "" Then
                    ' 検索文字列の分割
                    strFreeWord = CommonHBK.CommonLogicHBK.GetSearchStringList(.PropStrFreeWord, SPLIT_MODE_AND)
                    If strFreeWord.Length <> 0 Then
                        strSearch &= " AND "
                        strSearch &= " ("
                        For intCnt = 0 To strFreeWord.Count - 1
                            strSearch &= "CIT.FreeWordAimai like :FreeWordAimai" + intCnt.ToString()
                            If intCnt <> strFreeWord.Count - 1 Then
                                strSearch &= " AND "
                            End If
                        Next
                        strSearch &= ") "
                    End If
                End If
                '[add] 2015/08/21 y.naganuma フリーワード追加対応 END
            End With
            strSearch &= " ORDER BY KM.SORT ASC, CIT.NUM ASC"

            strSQL &= strSearch

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            With dataHBKB0702
                'CI種別CD(サポセン機器)
                Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CIKbnCDSuport", NpgsqlTypes.NpgsqlDbType.Varchar))
                Adapter.SelectCommand.Parameters("CIKbnCDSuport").Value = CommonDeclareHBK.CI_TYPE_SUPORT
                'CI種別CD(部所有機器)
                Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CIKbnCDKiki", NpgsqlTypes.NpgsqlDbType.Varchar))
                Adapter.SelectCommand.Parameters("CIKbnCDKiki").Value = CommonDeclareHBK.CI_TYPE_KIKI

                '導入番号(複数選択可)
                If .PropStrIntroductNo.Trim <> "" Then
                    For i As Integer = 0 To strIntroductNo.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("IntroductNmb" + i.ToString, NpgsqlTypes.NpgsqlDbType.Integer))
                        Adapter.SelectCommand.Parameters("IntroductNmb" + i.ToString).Value = strIntroductNo(i)
                    Next
                End If

                '番号検索
                If .PropStrNum.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("NUM", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("NUM").Value = .PropStrNum
                End If
                'タイプ
                If .PropStrTypeKbn <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("TypeKbn", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("TypeKbn").Value = .PropStrTypeKbn
                End If
                '機器利用形態
                If .PropStrKikiUse <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("KikiUseCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("KikiUseCD").Value = .PropStrKikiUse
                End If
                'イメージ番号
                If .PropStrImageNmb.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("ImageNmb", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("ImageNmb").Value = commonLogicHBK.ChangeStringForSearch(.PropStrImageNmb)
                End If
                'オプションソフト
                If .PropStrOptionSoft <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SoftCD", NpgsqlTypes.NpgsqlDbType.Integer))
                    Adapter.SelectCommand.Parameters("SoftCD").Value = .PropStrOptionSoft
                    'Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("softnm", NpgsqlTypes.NpgsqlDbType.Varchar))
                    'Adapter.SelectCommand.Parameters("softnm").Value = .PropStrOptionSoft
                End If
                'ユーザーID
                If .PropStrUsrID.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("UsrIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("UsrIDAimai").Value = commonLogicHBK.ChangeStringForSearch(.PropStrUsrID)
                End If
                'サービスセンター保管機
                If .PropStrSCHokanKbn <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SCHokanKbn", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("SCHokanKbn").Value = .PropStrSCHokanKbn
                End If
                'フリーフラグ1
                If .PropStrFreeFlg1 <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg1", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg1").Value = .PropStrFreeFlg1
                End If
                'フリーフラグ2
                If .PropStrFreeFlg2 <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg2", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg2").Value = .PropStrFreeFlg2
                End If
                'フリーフラグ3
                If .PropStrFreeFlg3 <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg3", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg3").Value = .PropStrFreeFlg3
                End If
                'フリーフラグ4
                If .PropStrFreeFlg4 <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg4", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg4").Value = .PropStrFreeFlg4
                End If
                'フリーフラグ5
                If .PropStrFreeFlg5 <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg5", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg5").Value = .PropStrFreeFlg5
                End If
                '管理部署(あいまい)
                If .PropStrManageBusyoNM.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("ManageBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("ManageBusyoNMAimai").Value = "%" + commonLogicHBK.ChangeStringForSearch(.PropStrManageBusyoNM) + "%"
                End If
                '設置部署(あいまい)
                If .PropStrSetBusyoNM.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SetBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("SetBusyoNMAimai").Value = "%" + commonLogicHBK.ChangeStringForSearch(.PropStrSetBusyoNM) + "%"
                End If
                '設置建物(あいまい)
                If .PropStrSetbuil.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SetBuilAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("SetBuilAimai").Value = "%" + commonLogicHBK.ChangeStringForSearch(.PropStrSetbuil) + "%"
                End If
                '設置フロア(あいまい)
                If .PropStrSetFloor.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SetFloorAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("SetFloorAimai").Value = "%" + commonLogicHBK.ChangeStringForSearch(.PropStrSetFloor) + "%"
                End If
                '設置番組/部屋検索(あいまい)
                If .PropStrSetRoom.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SetRoomAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("SetRoomAimai").Value = "%" + commonLogicHBK.ChangeStringForSearch(.PropStrSetRoom) + "%"
                End If
                '製造番号検索(あいまい検索)
                If .PropStrSerial.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SerialAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("SerialAimai").Value = "%" + commonLogicHBK.ChangeStringForSearch(.PropStrSerial) + "%"
                End If
                'フリーテキスト用のバインド変数設定
                If .PropStrBIko <> "" Then
                    '検索文字列をあいまい検索用に変換
                    For i As Integer = 0 To strFreeText.Count - 1
                        strFreeText(i) = commonLogicHBK.ChangeStringForSearch(strFreeText(i))
                    Next
                    'バインド変数を設定
                    For i As Integer = 0 To strFreeText.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("BikoAimai" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("BikoAimai" + i.ToString).Value = "%" + strFreeText(i) + "%"
                    Next
                End If
                '[add] 2015/08/21 y.naganuma フリーワード追加対応 START
                'フリーワード用のバインド変数設定
                If .PropStrFreeWord <> "" Then
                    '検索文字列をあいまい検索用に変換
                    For i As Integer = 0 To strFreeWord.Count - 1
                        strFreeWord(i) = commonLogicHBK.ChangeStringForSearch(strFreeWord(i))
                    Next
                    'バインド変数を設定
                    For i As Integer = 0 To strFreeWord.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeWordAimai" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("FreeWordAimai" + i.ToString).Value = "%" + strFreeWord(i) + "%"
                    Next
                End If
                '[add] 2015/08/21 y.naganuma フリーワード追加対応 END
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
    ''' 導入データ取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0702">[IN/OUT]機器一括検索一覧Excel出力Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>Excel出力用導入データ取得のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectIntroductForExcel(ByRef Adapter As NpgsqlDataAdapter, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB0702 As DataHBKB0702) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Dim strSearch As String = ""            '検索条件

        Try
            'SQL文設定

            'SQL文(SELECT)
            strSQL = strSelcetSearchIntro

            '検索条件設定
            '種別
            If dataHBKB0702.PropStrKind <> Nothing Then
                strSearch &= "WHERE IT.KindCD IN ( " & dataHBKB0702.PropStrKind & ") "
            End If

            '並び順設定
            strSearch &= " ORDER BY IT.IntroductNmb"

            strSQL &= strSearch

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
    ''' 履歴データ取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0702">[IN/OUT]機器一括検索一覧Excel出力Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>Excel出力用履歴データ取得のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectRirekiForExcel(ByRef Adapter As NpgsqlDataAdapter, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB0702 As DataHBKB0702) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""                   'SQL文
        Dim strSearch As String = ""                '検索条件
        Dim strIntroductNo() As String = Nothing    '導入番号検索用配列
        Dim strFreeText() As String = Nothing       'フリーテキスト検索用配列
        '[add] 2015/08/21 y.naganuma フリーワード追加対応 START
        Dim strFreeWord() As String = Nothing       'フリーワード検索用配列
        '[add] 2015/08/21 y.naganuma フリーワード追加対応 END

        Try
            'SQL文設定

            'SQL文(SELECT)
            strSQL = strSelectSearchRireki

            '検索条件設定
            With dataHBKB0702
                '種別
                If .PropStrKind <> Nothing Then
                    strSearch &= " AND CIRT.KindCD IN (" & .PropStrKind & ") "
                End If
                'ステータス
                If .PropStrStateNM <> Nothing Then
                    strSearch &= " AND CIRT.CIStatusCD IN (" & .PropStrStateNM & ") "
                End If
                '導入番号
                If .PropStrIntroductNo <> Nothing Then

                    ' 検索文字列の分割
                    strIntroductNo = CommonHBK.CommonLogicHBK.GetSearchStringList(.PropStrIntroductNo, SPLIT_MODE_OR)
                    strIntroductNo = CommonHBK.CommonLogicHBK.RemoveCharStringList(strIntroductNo)
                    If strIntroductNo.Length <> 0 Then
                        strSearch &= " AND  "
                        strSearch &= " ("
                        For intCnt = 0 To strIntroductNo.Count - 1
                            strSearch &= "CSRT.IntroductNmb = :IntroductNmb" + intCnt.ToString()
                            If intCnt <> strIntroductNo.Count - 1 Then
                                strSearch &= " OR "
                            End If
                        Next
                        strSearch &= ") "
                    End If
                End If
                '作業検索(複数選択可)
                If .PropStrWorkNM <> Nothing Then
                    strSearch &= " AND RRT.WorkCD IN ( " & .PropStrWorkNM & ") "
                End If
                '番号検索
                If .PropStrNum.Trim <> "" Then
                    strSearch &= " AND CIRT.NUM = LPAD(:NUM, 5, '0') "
                End If
                'タイプ検索
                If .PropStrTypeKbn <> "" Then
                    strSearch &= " AND CSRT.TypeKbn = :TypeKbn "
                End If
                '機器利用形態検索
                If .PropStrKikiUse <> "" Then
                    strSearch &= " AND CSRT.KikiUseCD = :KikiUseCD "
                End If
                'イメージ番号検索
                If .PropStrImageNmb.Trim <> "" Then
                    strSearch &= " AND CSRT.ImageNmb = :ImageNmb "
                End If
                'オプションソフト検索
                If .PropStrOptionSoft <> "" Then
                    'strSearch &= " AND ORT.SoftCD = :SoftCD "
                    '[EDIT] 2013/04/22 r.hoshino 課題No112対応 START
                    'strSearch &= " AND EXISTS (SELECT 1 FROM optsoft_rtb ost WHERE(ost.cinmb = rt.cinmb) AND ost.rirekino = CIRT.rirekino AND ost.SoftCD LIKE '%' || :SoftCD || '%') "
                    strSearch &= " AND EXISTS (SELECT 1 FROM optsoft_rtb ost WHERE(ost.cinmb = rt.cinmb) AND ost.rirekino = CIRT.rirekino AND ost.SoftCD= :SoftCD) "
                    '[EDIT] 2013/04/22 r.hoshino 課題No112対応 End
                End If
                'ユーザーID検索
                If .PropStrUsrID.Trim <> "" Then
                    strSearch &= " AND (CSRT.UsrIDAimai = :UsrIDAimai OR CBRT.UsrIDAimai = :UsrIDAimai) "
                End If
                'サービスセンター保管機検索
                If .PropStrSCHokanKbn <> "" Then
                    strSearch &= " AND CSRT.SCHokanKbn = :SCHokanKbn "
                End If
                'フリーフラグ1
                If .PropStrFreeFlg1 <> "" Then
                    strSearch &= " AND CIRT.FreeFlg1 = :FreeFlg1 "
                End If
                'フリーフラグ2
                If .PropStrFreeFlg2 <> "" Then
                    strSearch &= " AND CIRT.FreeFlg2 = :FreeFlg2 "
                End If
                'フリーフラグ3
                If .PropStrFreeFlg3 <> "" Then
                    strSearch &= " AND CIRT.FreeFlg3 = :FreeFlg3 "
                End If
                'フリーフラグ4
                If .PropStrFreeFlg4 <> "" Then
                    strSearch &= " AND CIRT.FreeFlg4 = :FreeFlg4 "
                End If
                'フリーフラグ5
                If .PropStrFreeFlg5 <> "" Then
                    strSearch &= " AND CIRT.FreeFlg5 = :FreeFlg5 "
                End If

                '管理部署検索(あいまい検索)
                If .PropStrManageBusyoNM.Trim <> "" Then
                    strSearch &= " AND (CSRT.ManageBusyoNMAimai LIKE :ManageBusyoNMAimai OR CBRT.ManageBusyoNMAimai LIKE :ManageBusyoNMAimai) "
                End If
                '設置部署検索(あいまい検索)
                If .PropStrSetBusyoNM.Trim <> "" Then
                    strSearch &= " AND (CSRT.SetBusyoNMAimai LIKE :SetBusyoNMAimai OR CBRT.SetBusyoNMAimai LIKE :SetBusyoNMAimai) "
                End If
                '設置建物検索(あいまい検索)
                If .PropStrSetbuil.Trim <> "" Then
                    strSearch &= " AND (CSRT.SetBuilAimai LIKE :SetBuilAimai OR CBRT.SetBuilAimai LIKE :SetBuilAimai) "
                End If
                '設置フロア検索(あいまい検索)
                If .PropStrSetFloor.Trim <> "" Then
                    strSearch &= " AND (CSRT.SetFloorAimai LIKE :SetFloorAimai OR CBRT.SetFloorAimai LIKE :SetFloorAimai) "
                End If
                '設置番組/部屋検索(あいまい検索)
                If .PropStrSetRoom.Trim <> "" Then
                    strSearch &= " AND (CSRT.SetRoomAimai LIKE :SetRoomAimai OR CBRT.SetRoomAimai LIKE :SetRoomAimai) "
                End If
                '製造番号検索(あいまい検索)
                If .PropStrSerial.Trim <> "" Then
                    strSearch &= " AND (CSRT.SerialAimai LIKE :SerialAimai OR CBRT.SerialAimai LIKE :SerialAimai) "
                End If
                'フリーテキスト検索(あいまい検索)
                If .PropStrBIko.Trim <> "" Then
                    ' 検索文字列の分割
                    strFreeText = CommonHBK.CommonLogicHBK.GetSearchStringList(.PropStrBIko, SPLIT_MODE_AND)

                    If strFreeText.Length <> 0 Then
                        strSearch &= " AND "
                        strSearch &= " ("
                        For intCnt = 0 To strFreeText.Count - 1
                            strSearch &= "CIRT.BikoAimai like :BikoAimai" + intCnt.ToString()
                            If intCnt <> strFreeText.Count - 1 Then
                                strSearch &= " AND "
                            End If
                        Next
                        strSearch &= ") "
                    End If
                End If

                '[add] 2015/08/21 y.naganuma フリーワード追加対応 START

                'フリーワード検索(あいまい検索)
                If .PropStrFreeWord.Trim <> "" Then
                    ' 検索文字列の分割
                    strFreeWord = CommonHBK.CommonLogicHBK.GetSearchStringList(.PropStrFreeWord, SPLIT_MODE_AND)

                    If strFreeWord.Length <> 0 Then
                        strSearch &= " AND "
                        strSearch &= " ("
                        For intCnt = 0 To strFreeWord.Count - 1
                            strSearch &= "CIRT.FreeWordAimai like :FreeWordAimai" + intCnt.ToString()
                            If intCnt <> strFreeWord.Count - 1 Then
                                strSearch &= " AND "
                            End If
                        Next
                        strSearch &= ") "
                    End If
                End If
                '[add] 2015/08/21 y.naganuma フリーワード追加対応 END

                '作業日(FROM)
                If .PropStrDayfrom.Trim <> "" Then
                    strSearch &= " AND TO_CHAR(RRT.regdt,'YYYY/MM/DD') >= :RegDTFrom "
                End If
                '作業日(TO)
                If .PropStrDayto.Trim <> "" Then
                    strSearch &= " AND TO_CHAR(RRT.regdt,'YYYY/MM/DD') <= :RegDTTo "
                End If
                '完了検索
                If .PropStrWorkKbnNM <> "" Then
                    strSearch &= " AND RRT.WorkKbnCD = :WorkKbnCD "
                End If

            End With

            '並び順設定
            '[mod] 2015/10/05 e.okamura フリーワード追加対応 START
            'strSearch &= " ORDER BY CIRT.RegDT DESC "
            strSearch &= " ORDER BY rrt.RegDT DESC, CIRT.CINmb, CIRT.RirekiNo DESC "
            '[mod] 2015/10/05 e.okamura フリーワード追加対応 END

            '検索条件セット
            strSQL &= strSearch

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            With dataHBKB0702
                'CI種別CD(サポセン機器)
                Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CIKbnCDSuport", NpgsqlTypes.NpgsqlDbType.Varchar))
                Adapter.SelectCommand.Parameters("CIKbnCDSuport").Value = CommonDeclareHBK.CI_TYPE_SUPORT
                'CI種別CD(部所有機器)
                Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CIKbnCDKiki", NpgsqlTypes.NpgsqlDbType.Varchar))
                Adapter.SelectCommand.Parameters("CIKbnCDKiki").Value = CommonDeclareHBK.CI_TYPE_KIKI

                '導入番号(複数選択可)
                If .PropStrIntroductNo.Trim <> "" Then
                    For i As Integer = 0 To strIntroductNo.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("IntroductNmb" + i.ToString, NpgsqlTypes.NpgsqlDbType.Integer))
                        Adapter.SelectCommand.Parameters("IntroductNmb" + i.ToString).Value = strIntroductNo(i)
                    Next
                End If

                '番号検索
                If .PropStrNum.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("NUM", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("NUM").Value = .PropStrNum
                End If
                'タイプ
                If .PropStrTypeKbn <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("TypeKbn", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("TypeKbn").Value = .PropStrTypeKbn
                End If
                '機器利用形態
                If .PropStrKikiUse <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("KikiUseCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("KikiUseCD").Value = .PropStrKikiUse
                End If
                'イメージ番号
                If .PropStrImageNmb.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("ImageNmb", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("ImageNmb").Value = commonLogicHBK.ChangeStringForSearch(.PropStrImageNmb)
                End If
                'オプションソフト
                If .PropStrOptionSoft <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SoftCD", NpgsqlTypes.NpgsqlDbType.Integer))
                    Adapter.SelectCommand.Parameters("SoftCD").Value = .PropStrOptionSoft
                    'Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("softnm", NpgsqlTypes.NpgsqlDbType.Varchar))
                    'Adapter.SelectCommand.Parameters("softnm").Value = .PropStrOptionSoft
                End If
                'ユーザーID
                If .PropStrUsrID.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("UsrIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("UsrIDAimai").Value = commonLogicHBK.ChangeStringForSearch(.PropStrUsrID)
                End If
                'サービスセンター保管機
                If .PropStrSCHokanKbn <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SCHokanKbn", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("SCHokanKbn").Value = .PropStrSCHokanKbn
                End If
                'フリーフラグ1
                If .PropStrFreeFlg1 <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg1", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg1").Value = .PropStrFreeFlg1
                End If
                'フリーフラグ2
                If .PropStrFreeFlg2 <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg2", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg2").Value = .PropStrFreeFlg2
                End If
                'フリーフラグ3
                If .PropStrFreeFlg3 <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg3", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg3").Value = .PropStrFreeFlg3
                End If
                'フリーフラグ4
                If .PropStrFreeFlg4 <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg4", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg4").Value = .PropStrFreeFlg4
                End If
                'フリーフラグ5
                If .PropStrFreeFlg5 <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg5", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg5").Value = .PropStrFreeFlg5
                End If
                '管理部署(あいまい)
                If .PropStrManageBusyoNM.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("ManageBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("ManageBusyoNMAimai").Value = "%" + commonLogicHBK.ChangeStringForSearch(.PropStrManageBusyoNM) + "%"
                End If
                '設置部署(あいまい)
                If .PropStrSetBusyoNM.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SetBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("SetBusyoNMAimai").Value = "%" + commonLogicHBK.ChangeStringForSearch(.PropStrSetBusyoNM) + "%"
                End If
                '設置建物(あいまい)
                If .PropStrSetbuil.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SetBuilAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("SetBuilAimai").Value = "%" + commonLogicHBK.ChangeStringForSearch(.PropStrSetbuil) + "%"
                End If
                '設置フロア(あいまい)
                If .PropStrSetFloor.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SetFloorAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("SetFloorAimai").Value = "%" + commonLogicHBK.ChangeStringForSearch(.PropStrSetFloor) + "%"
                End If
                '設置番組/部屋検索(あいまい)
                If .PropStrSetRoom.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SetRoomAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("SetRoomAimai").Value = "%" + commonLogicHBK.ChangeStringForSearch(.PropStrSetRoom) + "%"
                End If
                '製造番号検索(あいまい検索)
                If .PropStrSerial.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SerialAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("SerialAimai").Value = "%" + commonLogicHBK.ChangeStringForSearch(.PropStrSerial) + "%"
                End If
                'フリーテキスト用のバインド変数設定
                If .PropStrBIko <> "" Then
                    '検索文字列をあいまい検索用に変換
                    For i As Integer = 0 To strFreeText.Count - 1
                        strFreeText(i) = commonLogicHBK.ChangeStringForSearch(strFreeText(i))
                    Next
                    'バインド変数を設定
                    For i As Integer = 0 To strFreeText.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("BikoAimai" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("BikoAimai" + i.ToString).Value = "%" + strFreeText(i) + "%"
                    Next
                End If
                '[add] 2015/08/21 y.naganuma フリーワード追加対応 START
                If .PropStrFreeWord <> "" Then
                    '検索文字列をあいまい検索用に変換
                    For i As Integer = 0 To strFreeWord.Count - 1
                        strFreeWord(i) = commonLogicHBK.ChangeStringForSearch(strFreeWord(i))
                    Next
                    'バインド変数を設定
                    For i As Integer = 0 To strFreeWord.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeWordAimai" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("FreeWordAimai" + i.ToString).Value = "%" + strFreeWord(i) + "%"
                    Next
                End If
                'フリーワード用のバインド変数設定
                '[add] 2015/08/21 y.naganuma フリーワード追加対応 END

                '作業日(FROM)
                If .PropStrDayfrom.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RegDTFrom", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("RegDTFrom").Value = .PropStrDayfrom
                End If
                '作業日(TO)
                If .PropStrDayto.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RegDTTo", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("RegDTTo").Value = .PropStrDayto
                End If
                '完了
                If .PropStrWorkKbnNM <> Nothing Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("WorkKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("WorkKbnCD").Value = .PropStrWorkKbnNM
                End If
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
