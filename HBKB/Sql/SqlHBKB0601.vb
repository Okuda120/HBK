Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' サポセン機器登録画面Sqlクラス
''' </summary>
''' <remarks>サポセン機器登録画面のSQLの作成・設定を行う
''' <para>作成情報：2012/06/13 t.fukuo
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKB0601

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK


    'SQL文宣言

    '【共通】CI共通情報／履歴取得：SELECT句
    Private strSelectCIInfo As String = "SELECT " & vbCrLf & _
                                        " ct.CINmb " & vbCrLf & _
                                        ",ct.Num " & vbCrLf & _
                                        ",ct.CIKbnCD " & vbCrLf & _
                                        ",ct.KindCD " & vbCrLf & _
                                        ",ct.CIStatusCD " & vbCrLf & _
                                        ",ct.SetKikiID " & vbCrLf & _
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
                                        ",st.MemorySize " & vbCrLf & _
                                        ",st.Kataban " & vbCrLf & _
                                        ",st.Serial " & vbCrLf & _
                                        ",st.MacAddress1 " & vbCrLf & _
                                        ",st.MacAddress2 " & vbCrLf & _
                                        ",st.Fuzokuhin " & vbCrLf & _
                                        ",st.TypeKbn" & vbCrLf & _
                                        ",st.SCKikiFixNmb" & vbCrLf & _
                                        ",st.KikiState" & vbCrLf & _
                                        ",st.ImageNmb " & vbCrLf & _
                                        ",st.IntroductNmb " & vbCrLf & _
                                        ",CASE " & vbCrLf & _
                                        " WHEN st.LeaseUpDT = '' THEN '' " & vbCrLf & _
                                        " ELSE TO_CHAR(TO_DATE(st.LeaseUpDT,'YYYYMMDD'),'YYYY/MM/DD') " & vbCrLf & _
                                        " END AS LeaseUpDT_Kiki " & vbCrLf & _
                                        ",st.SCHokanKbn " & vbCrLf & _
                                        ",CASE " & vbCrLf & _
                                        " WHEN st.LastInfoDT = '' THEN '' " & vbCrLf & _
                                        " ELSE TO_CHAR(TO_DATE(st.LastInfoDT,'YYYYMMDD'),'YYYY/MM/DD') " & vbCrLf & _
                                        " END AS LastInfoDT " & vbCrLf & _
                                        ",st.ManageKyokuNM " & vbCrLf & _
                                        ",st.ManageBusyoNM " & vbCrLf & _
                                        ",st.WorkFromNmb " & vbCrLf & _
                                        ",st.KikiUseCD " & vbCrLf & _
                                        ",st.IPUseCD " & vbCrLf & _
                                        ",st.FixedIP " & vbCrLf & _
                                        ",st.UsrID " & vbCrLf & _
                                        ",st.UsrNM " & vbCrLf & _
                                        ",st.UsrCompany " & vbCrLf & _
                                        ",st.UsrKyokuNM " & vbCrLf & _
                                        ",st.UsrBusyoNM " & vbCrLf & _
                                        ",st.UsrTel " & vbCrLf & _
                                        ",st.UsrMailAdd " & vbCrLf & _
                                        ",st.UsrContact " & vbCrLf & _
                                        ",st.UsrRoom " & vbCrLf & _
                                        ",CASE " & vbCrLf & _
                                        " WHEN st.RentalStDT = '' THEN '' " & vbCrLf & _
                                        " ELSE TO_CHAR(TO_DATE(st.RentalStDT,'YYYYMMDD'),'YYYY/MM/DD') " & vbCrLf & _
                                        " END AS RentalStDT " & vbCrLf & _
                                        ",CASE " & vbCrLf & _
                                        " WHEN st.RentalEdDT = '' THEN '' " & vbCrLf & _
                                        " ELSE TO_CHAR(TO_DATE(st.RentalEdDT,'YYYYMMDD'),'YYYY/MM/DD') " & vbCrLf & _
                                        " END AS RentalEdDT " & vbCrLf & _
                                        ",st.SetKyokuNM " & vbCrLf & _
                                        ",st.SetBusyoNM " & vbCrLf & _
                                        ",st.SetRoom " & vbCrLf & _
                                        ",st.SetBuil " & vbCrLf & _
                                        ",st.SetFloor " & vbCrLf & _
                                        ",st.SetDeskNo " & vbCrLf & _
                                        ",st.SetLANLength " & vbCrLf & _
                                        ",st.SetLANNum " & vbCrLf & _
                                        ",st.SetSocket " & vbCrLf & _
                                        ",it.IntroductDelKbn " & vbCrLf & _
                                        ",it.IntroductKbn " & vbCrLf & _
                                        ",it.HosyoUmu " & vbCrLf & _
                                        ",it.LeaseCompany " & vbCrLf & _
                                        ",CASE " & vbCrLf & _
                                        " WHEN it.LeaseUpDT = '' THEN '' " & vbCrLf & _
                                        " ELSE TO_CHAR(TO_DATE(it.LeaseUpDT,'YYYYMMDD'),'YYYY/MM/DD') " & vbCrLf & _
                                        " END AS LeaseUpDT_Int " & vbCrLf & _
                                        ",it.MakerHosyoTerm " & vbCrLf & _
                                        ",it.EOS " & vbCrLf & _
                                        ",CASE " & vbCrLf & _
                                        " WHEN it.DelScheduleDT = '' THEN '' " & vbCrLf & _
                                        " ELSE TO_CHAR(TO_DATE(it.DelScheduleDT,'YYYYMMDD'),'YYYY/MM/DD') " & vbCrLf & _
                                        " END AS DelScheduleDT " & vbCrLf & _
                                        ",CASE " & vbCrLf & _
                                        " WHEN it.IntroductStDT = '' THEN '' " & vbCrLf & _
                                        " ELSE TO_CHAR(TO_DATE(it.IntroductStDT,'YYYYMMDD'),'YYYY/MM/DD') " & vbCrLf & _
                                        " END AS IntroductStDT " & vbCrLf

    '【編集モード】CI共通情報取得：FROM～WHERE句
    Private strFromWhereCIInfoForEdit As String = "FROM SAP_MAINTE_TB smt" & vbCrLf & _
                                                  "LEFT JOIN CI_INFO_TMP ct ON smt.IncNmb = ct.IncNmb AND smt.WorkNmb = ct.WorkNmb AND smt.CINmb = ct.CINmb" & vbCrLf & _
                                                  "LEFT JOIN CI_SAP_TMP st ON ct.IncNmb = st.IncNmb AND ct.WorkNmb = st.WorkNmb AND ct.CINmb = st.CINmb" & vbCrLf & _
                                                  "LEFT JOIN INTRODUCT_TB it ON st.IntroductNmb = it.IntroductNmb" & vbCrLf & _
                                                  "LEFT JOIN GRP_MTB gm ON ct.CIOwnerCD = gm.GroupCD" & vbCrLf & _
                                                  "WHERE" & vbCrLf & _
                                                  "      smt.IncNmb = :IncNmb" & vbCrLf & _
                                                  "  AND smt.WorkNmb = :WorkNmb" & vbCrLf & _
                                                  "  AND smt.CINmb = :CINmb" & vbCrLf

    '【参照モード】CI共通情報取得：FROM～WHERE句
    Private strFromWhereCIInfoForRef As String = "FROM CI_INFO_TB ct" & vbCrLf & _
                                                 "LEFT JOIN CI_SAP_TB st ON ct.CINmb = st.CINmb" & vbCrLf & _
                                                 "LEFT JOIN INTRODUCT_TB it ON st.IntroductNmb = it.IntroductNmb" & vbCrLf & _
                                                 "LEFT JOIN GRP_MTB gm ON ct.CIOwnerCD = gm.GroupCD" & vbCrLf & _
                                                 "WHERE" & vbCrLf & _
                                                  "      ct.CINmb = :CINmb" & vbCrLf

    '【履歴モード】CI共通情報履歴取得：FROM～WHERE句
    Private strFromWhereCIInfoForRireki As String = "FROM CI_INFO_RTB ct" & vbCrLf & _
                                                    "LEFT JOIN CI_SAP_RTB st ON ct.CINmb = st.CINmb AND ct.RirekiNo = st.RirekiNo" & vbCrLf & _
                                                    "LEFT JOIN INTRODUCT_TB it ON st.IntroductNmb = it.IntroductNmb" & vbCrLf & _
                                                    "LEFT JOIN GRP_MTB gm ON ct.CIOwnerCD = gm.GroupCD" & vbCrLf & _
                                                    "WHERE" & vbCrLf & _
                                                    "      ct.CINmb = :CINmb" & vbCrLf & _
                                                    "  AND ct.RirekiNo = :RirekiNo" & vbCrLf



    '【共通】複数人利用情報／履歴取得：SELECT句
    Private strSelectShareSql As String = "SELECT " & vbCrLf & _
                                          " sht.UsrID " & vbCrLf & _
                                          ",sht.UsrNM " & vbCrLf & _
                                          ",sht.RegDT " & vbCrLf & _
                                          ",sht.RegGrpCD " & vbCrLf & _
                                          ",sht.RegID " & vbCrLf 


    '【参照モード】複数人利用情報取得：FROM～WHERE句
    Private strFromWhereShareForRef As String = "FROM SHARE_TB sht" & vbCrLf & _
                                                "WHERE" & vbCrLf & _
                                                "      sht.CINMb = :CINMb" & vbCrLf

    '【履歴モード】複数人利用情報履歴取得：FROM～WHERE句
    Private strFromWhereShareForRireki As String = "FROM SHARE_RTB sht" & vbCrLf & _
                                                   "WHERE" & vbCrLf & _
                                                   "      sht.CINmb = :CINmb" & vbCrLf & _
                                                   "  AND sht.RirekiNo = :RirekiNo" & vbCrLf

    '【共通】複数人利用情報／履歴取得：ORDER BY句
    Private strOrderByShare As String = "ORDER BY sht.RowNmb"



    '【共通】オプションソフト／履歴取得：SELECT句
    Private strSelectOptSoftSql As String = "SELECT " & vbCrLf & _
                                            " ot.SoftCD " & vbCrLf & _
                                            ",ot.RegDT " & vbCrLf & _
                                            ",ot.RegGrpCD " & vbCrLf & _
                                            ",ot.RegID " & vbCrLf


    '【参照モード】オプションソフト取得：FROM～WHERE句
    Private strFromWhereOptSoftForRef As String = "FROM OPTSOFT_TB ot" & vbCrLf & _
                                                  "WHERE" & vbCrLf & _
                                                  "      ot.CINMb = :CINMb" & vbCrLf

    '【履歴モード】オプションソフト履歴取得：FROM～WHERE句
    Private strFromWhereOptSoftForRireki As String = "FROM OPTSOFT_RTB ot" & vbCrLf & _
                                                     "WHERE" & vbCrLf & _
                                                     "      ot.CINmb = :CINmb" & vbCrLf & _
                                                     "  AND ot.RirekiNo = :RirekiNo" & vbCrLf

    '【共通】オプションソフト／履歴取得：ORDER BY句
    Private strOrderByOptSoft As String = "ORDER BY ot.RowNmb"


    '【共通】セット機器／履歴取得：SELECT句
    Private strSelectSetKikiSql As String = "SELECT" & vbCrLf & _
                                            "  t.SetKikiNo" & vbCrLf & _
                                            " ,t.SetKikiID" & vbCrLf & _
                                            " ,MAX(t.RegDT) AS Sort" & vbCrLf & _
                                            "FROM (" & vbCrLf & _
                                            "  SELECT" & vbCrLf & _
                                            "   ( SELECT km.KindNM" & vbCrLf & _
                                            "     FROM KIND_MTB km" & vbCrLf & _
                                            "     WHERE CIKbnCD = :CIKbnCD AND km.KindCD = ct.KindCD" & vbCrLf & _
                                            "   ) || ct.Num AS SetKikiNo" & vbCrLf & _
                                            "  ,skt.SetKikiID" & vbCrLf & _
                                            "  ,skt.RegDT" & vbCrLf & _
                                            "  {0}" & vbCrLf & _
                                            ") t" & vbCrLf & _
                                            "GROUP BY t.SetKikiNo, t.SetKikiID" & vbCrLf & _
                                            "ORDER BY Sort"


    '[mod] 2012/09/25 y.ikushima 編集モード時、データ取得先を保存用テーブル→標準テーブルへ変更
    '【編集モード・参照モード】セット機器取得：FROM～WHERE句
    Private strFromWhereSetKikiForRef As String = "FROM SET_KIKI_MNG_TB skt " & vbCrLf & _
                                                  "LEFT JOIN CI_INFO_TB ct ON ct.SetKikiID = skt.SetKikiID" & vbCrLf & _
                                                  "WHERE  " & vbCrLf & _
                                                  "      skt.SetKikiID = :SetKikiID " & vbCrLf

    '[mod] 2012/12/12 t.fukuo セット機器が重複して表示される不具合修正：START
    ''【履歴モード】セット機器履歴取得：FROM～WHERE句
    'Private strFromWhereSetKikiForRireki As String = "FROM CI_INFO_RTB ct " & vbCrLf & _
    '                                                 "LEFT OUTER JOIN setkiki_rtb skt ON ct.CINmb = skt.SetCINmb AND ct.RirekiNo = skt.SetRirekiNo" & vbCrLf & _
    '                                                 "WHERE (ct.CINmb,ct.RirekiNo) IN (SELECT SetCINmb,SetRirekiNo FROM SETKIKI_RTB WHERE CINmb = :CINmb AND RirekiNo = :RirekiNo) " & vbCrLf
    Private strFromWhereSetKikiForRireki As String = "FROM CI_INFO_RTB ct " & vbCrLf & _
                                                     "LEFT OUTER JOIN setkiki_rtb skt ON ct.CINmb = skt.SetCINmb AND ct.RirekiNo = skt.SetRirekiNo" & vbCrLf & _
                                                     "WHERE (ct.CINmb,ct.RirekiNo) IN (SELECT SetCINmb,SetRirekiNo FROM SETKIKI_RTB WHERE CINmb = skt.CINmb AND RirekiNo = skt.RirekiNo) " & vbCrLf & _
                                                     "  AND skt.CINmb = :CINmb" & vbCrLf & _
                                                     "  AND skt.RirekiNo = :RirekiNo" & vbCrLf & _
                                                     "  AND skt.SetKikiID = (SELECT crt.SetKikiID FROM CI_INFO_RTB crt WHERE crt.CINmb = :CINmb AND crt.RirekiNo = :RirekiNo)"
    '[mod] 2012/12/12 t.fukuo セット機器が重複して表示される不具合修正：END

    '【共通】原因リンク取得：SELECT句
    Private strSelectCauseLink As String = "SELECT " & vbCrLf & _
                                           " clt.RirekiNo " & vbCrLf & _
                                           ",CASE clt.ProcessKbn " & vbCrLf & _
                                           " WHEN :Kbn_Incident THEN :Kbn_Incident_NMR " & vbCrLf & _
                                           " WHEN :Kbn_Question THEN :Kbn_Question_NMR " & vbCrLf & _
                                           " WHEN :Kbn_Change THEN :Kbn_Change_NMR " & vbCrLf & _
                                           " WHEN :Kbn_Release THEN :Kbn_Release_NMR " & vbCrLf & _
                                           " ELSE '' END AS ProcessKbnNMR " & vbCrLf & _
                                           ",clt.ProcessKbn " & vbCrLf & _
                                           ",clt.MngNmb " & vbCrLf

    '【編集／参照モード】原因リンク履歴取得：FROM～WHERE句
    Private strFromWhereCauseLinkForRef As String = "FROM REGREASON_RTB rt " & vbCrLf & _
                                                    "JOIN CAUSELINK_RTB clt ON rt.CINmb = clt.CINmb AND rt.RirekiNo = clt.RirekiNo " & vbCrLf & _
                                                    "WHERE clt.CINmb = :CINmb " & vbCrLf & _
                                                    "  AND clt.RirekiNo = (SELECT MAX(rt2.RirekiNo) FROM REGREASON_RTB rt2 WHERE rt2.CINmb = clt.CINmb) " & vbCrLf


    '【履歴モード】原因リンク履歴取得：FROM～WHERE句
    Private strFromWhereCauseLinkForRireki As String = "FROM REGREASON_RTB rt " & vbCrLf & _
                                                       "JOIN CAUSELINK_RTB clt ON rt.CINmb = clt.CINmb AND rt.RirekiNo = clt.RirekiNo " & vbCrLf & _
                                                       "WHERE rt.CINmb = :CINmb " & vbCrLf & _
                                                       "  AND rt.RirekiNo = :RirekiNo " & vbCrLf

    '【共通】原因リンク履歴取得：ORDER BY句
    Private strOrderByCauseLink As String = "ORDER BY clt.ProcessKbn, clt.MngNmb "

    '【共通】登録理由履歴取得：SELECT句
    Private strSelectRegReason As String = "SELECT " & vbCrLf & _
                                           " rt.RirekiNo " & vbCrLf & _
                                           ",TO_CHAR(rt.RegDT,'YYYY/MM/DD HH24:MI') AS RegDT " & vbCrLf & _
                                           ",gm.GroupNM " & vbCrLf & _
                                           ",hm.HBKUsrNM " & vbCrLf & _
                                           ",rt.RegReason " & vbCrLf

    '【編集／参照モード】登録理由履歴取得：FROM～WHERE句
    Private strFromWhereRegReasonForRef As String = "FROM REGREASON_RTB rt " & vbCrLf & _
                                                    "LEFT JOIN GRP_MTB gm ON rt.RegGrpCD = gm.GroupCD " & vbCrLf & _
                                                    "LEFT JOIN HBKUSR_MTB hm ON rt.RegID = hm.HBKUsrID " & vbCrLf & _
                                                    "WHERE rt.CINmb = :CINmb " & vbCrLf

    '【履歴モード】登録理由履歴取得：FROM～WHERE句
    Private strFromWhereRegReasonForRireki As String = "FROM REGREASON_RTB rt " & vbCrLf & _
                                                    "LEFT JOIN GRP_MTB gm ON rt.RegGrpCD = gm.GroupCD " & vbCrLf & _
                                                    "LEFT JOIN HBKUSR_MTB hm ON rt.RegID = hm.HBKUsrID " & vbCrLf & _
                                                    "WHERE rt.CINmb = :CINmb " & vbCrLf & _
                                                    "  AND rt.RirekiNo <= :RirekiNo " & vbCrLf


    '【共通】登録理由履歴取得：ORDER BY句
    Private strOrderByRegReason As String = "ORDER BY rt.RirekiNo DESC "




    '【共通】イメージ番号存在チェック用（SELECT）SQL
    Private strSelectSameImageNmbCntSql As String = "SELECT COUNT(1)" & vbCrLf & _
                                                    "FROM IMAGE_MTB" & vbCrLf & _
                                                    "WHERE ImageNmb = :ImageNmb"

    '【共通】セット機器グループ番号チェック用（SELECT）SQL
    Private strSelectSetKikiGrpNoSql As String = "SELECT DISTINCT st2.SetKikiGrpNo" & vbCrLf & _
                                                 "FROM CI_INFO_TB ct" & vbCrLf & _
                                                 "JOIN SETKIKI_TMP st ON (ct.KindCD || ct.Num) = st.SetKikiNo AND st.JtiFlg = '0'" & vbCrLf & _
                                                 "JOIN SETKIKI_TMP st2 ON st.SetKikiGrpNo = st2.SetKikiGrpNo AND st2.JtiFlg = '0'" & vbCrLf & _
                                                 "JOIN KIND_MTB km ON SUBSTR(st2.SetKikiNo,1,3) = km.KindCD AND km.CIKbnCD = :CIKbnCD AND km.JtiFlg = '0'" & vbCrLf & _
                                                 "WHERE ct.CIKbnCD = :CIKbnCD" & vbCrLf

    '【共通】セット機器No存在チェック用（SELECT）SQL
    Private strSelectSameSetKikiCntSql As String = "SELECT COUNT(1) " & vbCrLf & _
                                                   "FROM CI_INFO_TB ct" & vbCrLf & _
                                                   "JOIN KIND_MTB km ON ct.CIKbnCD = km.CIKbnCD AND ct.KindCD = km.KIndCD" & vbCrLf & _
                                                   "WHERE ct.CIKbnCD = :CIKbnCD" & vbCrLf & _
                                                   "  AND km.KindNM = SUBSTR(:SetKikiNo, 1, LENGTH(:SetKikiNo)-5)" & vbCrLf & _
                                                   "  AND ct.Num = SUBSTR(:SetKikiNo, LENGTH(:SetKikiNo)-4, 5)" & vbCrLf & _
                                                   "  AND km.JtiFlg = '0'" & vbCrLf


    '【編集モード】システム日付取得（SELECT）SQL
    Private strSelectSysDateSql As String = "SELECT Now() AS SysDate "

    '[mod] 2012/09/25 y.ikushima 編集モード時、データ登録先を保存用テーブル→標準テーブルへ変更
    '【編集モード】CI共通情報更新（UPDATE）SQL
    Private strUpdateTmpCIInfoSql As String = _
                                       "UPDATE CI_INFO_TB SET " & vbCrLf & _
                                       " CIStatusCD     = :CIStatusCD " & vbCrLf & _
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
                                       ",BikoAimai      = :BikoAimai " & vbCrLf & _
                                       ",UpdateDT       = :UpdateDT " & vbCrLf & _
                                       ",UpGrpCD        = :UpGrpCD " & vbCrLf & _
                                       ",UpdateID       = :UpdateID " & vbCrLf & _
                                       "WHERE CINmb=:CINmb  " & vbCrLf

    '【編集モード】CIサポセン機器更新（UPDATE）SQL
    Private strUpdateTmpCIsapSql As String = _
                                           "UPDATE CI_SAP_TB SET " & vbCrLf & _
                                           " MemorySize         = :MemorySize " & vbCrLf & _
                                           ",Serial             = :Serial " & vbCrLf & _
                                           ",MacAddress1        = :MacAddress1 " & vbCrLf & _
                                           ",MacAddress2        = :MacAddress2 " & vbCrLf & _
                                           ",Fuzokuhin          = :Fuzokuhin " & vbCrLf & _
                                           ",TypeKbn            = :TypeKbn " & vbCrLf & _
                                           ",SCKikiFixNmb       = :SCKikiFixNmb " & vbCrLf & _
                                           ",KikiState          = :KikiState " & vbCrLf & _
                                           ",ImageNmb           = :ImageNmb " & vbCrLf & _
                                           ",LeaseUpDT          = CASE :LeaseUpDT WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(:LeaseUpDT, 'YYYY/MM/DD'),'YYYYMMDD') END " & vbCrLf & _
                                           ",SCHokanKbn         = :SCHokanKbn " & vbCrLf & _
                                           ",LastInfoDT         = CASE :LastInfoDT WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(:LastInfoDT, 'YYYY/MM/DD'),'YYYYMMDD') END " & vbCrLf & _
                                           ",ManageKyokuNM      = :ManageKyokuNM " & vbCrLf & _
                                           ",ManageBusyoNM      = :ManageBusyoNM " & vbCrLf & _
                                           ",WorkFromNmb        = :WorkFromNmb " & vbCrLf & _
                                           ",KikiUseCD          = :KikiUseCD " & vbCrLf & _
                                           ",IPUseCD            = :IPUseCD " & vbCrLf & _
                                           ",FixedIP            = :FixedIP " & vbCrLf & _
                                           ",UsrID              = :UsrID " & vbCrLf & _
                                           ",UsrNM              = :UsrNM " & vbCrLf & _
                                           ",UsrCompany         = :UsrCompany " & vbCrLf & _
                                           ",UsrKyokuNM         = :UsrKyokuNM " & vbCrLf & _
                                           ",UsrBusyoNM         = :UsrBusyoNM " & vbCrLf & _
                                           ",UsrTel             = :UsrTel " & vbCrLf & _
                                           ",UsrMailAdd         = :UsrMailAdd " & vbCrLf & _
                                           ",UsrContact         = :UsrContact " & vbCrLf & _
                                           ",UsrRoom            = :UsrRoom " & vbCrLf & _
                                           ",RentalStDT         = CASE :RentalStDT WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(:RentalStDT, 'YYYY/MM/DD'),'YYYYMMDD') END " & vbCrLf & _
                                           ",RentalEdDT         = CASE :RentalEdDT WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(:RentalEdDT, 'YYYY/MM/DD'),'YYYYMMDD') END " & vbCrLf & _
                                           ",SetKyokuNM         = :SetKyokuNM " & vbCrLf & _
                                           ",SetBusyoNM         = :SetBusyoNM " & vbCrLf & _
                                           ",SetRoom            = :SetRoom " & vbCrLf & _
                                           ",SetBuil            = :SetBuil " & vbCrLf & _
                                           ",SetFloor           = :SetFloor " & vbCrLf & _
                                           ",SetDeskNo          = :SetDeskNo " & vbCrLf & _
                                           ",SetLANLength       = :SetLANLength " & vbCrLf & _
                                           ",SetLANNum          = :SetLANNum " & vbCrLf & _
                                           ",SetSocket          = :SetSocket " & vbCrLf & _
                                           ",SerialAimai        = :SerialAimai " & vbCrLf & _
                                           ",ImageNmbAimai      = :ImageNmbAimai " & vbCrLf & _
                                           ",ManageBusyoNMAimai = :ManageBusyoNMAimai " & vbCrLf & _
                                           ",UsrIDAimai         = :UsrIDAimai " & vbCrLf & _
                                           ",SetBusyoNMAimai    = :SetBusyoNMAimai " & vbCrLf & _
                                           ",SetRoomAimai       = :SetRoomAimai " & vbCrLf & _
                                           ",SetBuilAimai       = :SetBuilAimai " & vbCrLf & _
                                           ",SetFloorAimai      = :SetFloorAimai " & vbCrLf & _
                                           ",UpdateDT           = :UpdateDT " & vbCrLf & _
                                           ",UpGrpCD            = :UpGrpCD " & vbCrLf & _
                                           ",UpdateID           = :UpdateID " & vbCrLf & _
                                           "WHERE CINmb=:CINmb " & vbCrLf


    '【編集モード】複数人利用物理削除（DELETE）用SQL
    Private strDeleteTmpShareSql As String = _
                                        "DELETE FROM SHARE_TB" & vbCrLf & _
                                        "WHERE CINmb=:CINmb "

    '【編集モード】複数人利用新規登録（INSERT）用SQL
    Private strInsertTmpShareSql As String = _
                                        "INSERT INTO SHARE_TB (" & vbCrLf & _
                                        " CINmb" & vbCrLf & _
                                        " ,RowNmb" & vbCrLf & _
                                        " ,UsrID" & vbCrLf & _
                                        " ,UsrNM" & vbCrLf & _
                                        " ,RegDT" & vbCrLf & _
                                        " ,RegGrpCD" & vbCrLf & _
                                        " ,RegID" & vbCrLf & _
                                        " ,UpdateDT" & vbCrLf & _
                                        " ,UpGrpCD" & vbCrLf & _
                                        " ,UpdateID" & vbCrLf & _
                                        ")" & vbCrLf & _
                                        "VALUES (" & vbCrLf & _
                                        " :CINmb" & vbCrLf & _
                                        " ,(SELECT COALESCE(MAX(st.RowNmb),0)+1 FROM SHARE_TB st WHERE st.CINmb=:CINmb)" & vbCrLf & _
                                        " ,:UsrID" & vbCrLf & _
                                        " ,:UsrNM" & vbCrLf & _
                                        " ,:RegDT" & vbCrLf & _
                                        " ,:RegGrpCD" & vbCrLf & _
                                        " ,:RegID" & vbCrLf & _
                                        " ,:UpdateDT" & vbCrLf & _
                                        " ,:UpGrpCD" & vbCrLf & _
                                        " ,:UpdateID" & vbCrLf & _
                                        ")"


    '【編集モード】オプションソフト物理削除（DELETE）用SQL
    Private strDeleteTmpOptSoftSql As String = _
                                            "DELETE FROM OPTSOFT_TB" & vbCrLf & _
                                            "WHERE CINmb=:CINmb "

    '【編集モード】オプションソフト新規登録（INSERT）用SQL
    Private strInsertTmpOptSoftSql As String = _
                                            "INSERT INTO OPTSOFT_TB (" & vbCrLf & _
                                            " CINmb" & vbCrLf & _
                                            " ,RowNmb" & vbCrLf & _
                                            " ,SoftCD" & vbCrLf & _
                                            " ,RegDT" & vbCrLf & _
                                            " ,RegGrpCD" & vbCrLf & _
                                            " ,RegID" & vbCrLf & _
                                            " ,UpdateDT" & vbCrLf & _
                                            " ,UpGrpCD" & vbCrLf & _
                                            " ,UpdateID" & vbCrLf & _
                                            ")" & vbCrLf & _
                                            "VALUES (" & vbCrLf & _
                                            " :CINmb" & vbCrLf & _
                                            " ,(SELECT COALESCE(MAX(ot.RowNmb),0)+1 FROM OPTSOFT_TB ot WHERE ot.CINmb=:CINmb)" & vbCrLf & _
                                            " ,:SoftCD" & vbCrLf & _
                                            " ,:RegDT" & vbCrLf & _
                                            " ,:RegGrpCD" & vbCrLf & _
                                            " ,:RegID" & vbCrLf & _
                                            " ,:UpdateDT" & vbCrLf & _
                                            " ,:UpGrpCD" & vbCrLf & _
                                            " ,:UpdateID" & vbCrLf & _
                                            ")"

    '【編集モード】セット機器管理グループ番号取得（SELECT）用SQL
    Private strSelectNewSetKikiGrpNoSql As String = _
                                            "SELECT COALESCE(MAX(st.SetKikiGrpNo),0) + 1 AS SetKikiGrpNo" & vbCrLf & _
                                            "FROM SETKIKI_TB st" & vbCrLf & _
                                            "WHERE st.JtiFlg = '0'" & vbCrLf

    '【編集モード】セット機器管理論理削除（UPDATE）用SQL ※インシデント登録画面と同じ
    Private strPhygDeleteTmpSetKikiSql As String = _
                                        "UPDATE SETKIKI_TB st1 SET" & vbCrLf & _
                                        "  JtiFlg = '1'" & vbCrLf & _
                                        " ,UpdateDT = :UpdateDT" & vbCrLf & _
                                        " ,UpGrpCD = :UpGrpCD" & vbCrLf & _
                                        " ,UpdateID = :UpdateID" & vbCrLf & _
                                        "WHERE st1.JtiFlg = '0'" & vbCrLf & _
                                        "  AND st1.CINmb = :CINmb" & vbCrLf & _
                                        "  AND (st1.SetKikiNo = :SetKikiNo" & vbCrLf & _
                                        "       OR st1.SetKikiNo = CASE (SELECT COUNT(1) FROM SETKIKI_TB st2 WHERE st1.CINmb = st2.CINmb AND st1.SetKikiGrpNo = st2.SetKikiGrpNo AND st2.JtiFlg = '0')" & vbCrLf & _
                                        "                          WHEN 2 THEN (SELECT st1.SetKikiNo FROM SETKIKI_TB st2 WHERE st1.CINmb = st2.CINmb AND st1.SetKikiGrpNo = st2.SetKikiGrpNo AND st2.JtiFlg = '0' AND st1.SetKikiID <> st2.SetKikiID)" & vbCrLf & _
                                        "                          ELSE :SetKikiNo END" & vbCrLf & _
                                        "      )"


    '【編集モード】セット機器管理新規登録（INSERT）用SQL
    Private strInsertTmpSetKikiSql As String = _
                                               "INSERT INTO SETKIKI_TB (" & vbCrLf & _
                                               " CINmb" & vbCrLf & _
                                               " ,SetKikiID" & vbCrLf & _
                                               " ,SetKikiGrpNo" & vbCrLf & _
                                               " ,SetKikiNo" & vbCrLf & _
                                               " ,JtiFlg" & vbCrLf & _
                                               " ,RegDT" & vbCrLf & _
                                               " ,RegGrpCD" & vbCrLf & _
                                               " ,RegID" & vbCrLf & _
                                               " ,UpdateDT" & vbCrLf & _
                                               " ,UpGrpCD" & vbCrLf & _
                                               " ,UpdateID" & vbCrLf & _
                                               ")" & vbCrLf & _
                                               "SELECT" & vbCrLf & _
                                               " :CINmb" & vbCrLf & _
                                               " ,(SELECT t.SetKikiID FROM (" & GET_NEXTVAL_SETKIKI_ID & ") t)" & vbCrLf & _
                                               " ,:SetKikiGrpNo" & vbCrLf & _
                                               " ,ct.KindCD || ct.Num" & vbCrLf & _
                                               " ,'0'" & vbCrLf & _
                                               " ,:RegDT" & vbCrLf & _
                                               " ,:RegGrpCD" & vbCrLf & _
                                               " ,:RegID" & vbCrLf & _
                                               " ,:UpdateDT" & vbCrLf & _
                                               " ,:UpGrpCD" & vbCrLf & _
                                               " ,:UpdateID" & vbCrLf & _
                                               "FROM CI_INFO_TB ct" & vbCrLf & _
                                               "JOIN KIND_MTB km ON ct.KindCD = km.KindCD AND ct.CIKbnCD = km.CIKbnCD" & vbCrLf & _
                                               "WHERE ct.CIKbnCD = '" & CI_TYPE_SUPORT & "'" & vbCrLf & _
                                               "  AND km.JtiFlg = '0'" & vbCrLf & _
                                               "  AND km.KindNM = SUBSTR(:SetKikiNo, 1, LENGTH(:SetKikiNo)-5)" & vbCrLf & _
                                               "  AND ct.Num = SUBSTR(:SetKikiNo, LENGTH(:SetKikiNo)-4, 5)" & vbCrLf & _
                                               "  AND NOT EXISTS (SELECT * FROM SETKIKI_TMP st2 WHERE st2.IncNmb = :IncNmb AND st2.WorkNmb = :WorkNmb AND st2.CINmb = :CINmb AND ct.KindCD || ct.Num = st2.SetKikiNo AND st2.SetKikiGrpNo = :SetKikiGrpNo AND st2.JtiFlg = '0')"

    'CI共通情報履歴新規登録（INSERT）用SQL
    Private strInsertCIInfoRirekiSql As String = "INSERT INTO CI_INFO_RTB ( " & vbCrLf & _
                                                 " CINmb " & vbCrLf & _
                                                 ",RirekiNo " & vbCrLf & _
                                                 ",CIKbnCD " & vbCrLf & _
                                                 ",KindCD " & vbCrLf & _
                                                 ",Num " & vbCrLf & _
                                                 ",CIStatusCD " & vbCrLf & _
                                                 ",SetKikiID " & vbCrLf & _
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
                                                 ",ct.SetKikiID " & vbCrLf & _
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
                                                 ",:RegDT " & vbCrLf & _
                                                 ",:RegGrpCD " & vbCrLf & _
                                                 ",:RegID " & vbCrLf & _
                                                 ",:UpdateDT " & vbCrLf & _
                                                 ",:UpGrpCD " & vbCrLf & _
                                                 ",:UpdateID " & vbCrLf & _
                                                 "FROM CI_INFO_TB ct " & vbCrLf & _
                                                 "WHERE ct.CINmb=:CINmb "

    'CIサポセン機器履歴新規登録（INSERT）用SQL
    Private strInsertCISapRirekiSql As String = "INSERT INTO CI_SAP_RTB ( " & vbCrLf & _
                                                " CINmb " & vbCrLf & _
                                                ",RirekiNo " & vbCrLf & _
                                                ",MemorySize " & vbCrLf & _
                                                ",Kataban " & vbCrLf & _
                                                ",Serial " & vbCrLf & _
                                                ",MacAddress1 " & vbCrLf & _
                                                ",MacAddress2 " & vbCrLf & _
                                                ",Fuzokuhin " & vbCrLf & _
                                                ",TypeKbn " & vbCrLf & _
                                                ",SCKikiFixNmb " & vbCrLf & _
                                                ",KikiState " & vbCrLf & _
                                                ",ImageNmb " & vbCrLf & _
                                                ",IntroductNmb " & vbCrLf & _
                                                ",LeaseUpDT " & vbCrLf & _
                                                ",SCHokanKbn " & vbCrLf & _
                                                ",LastInfoDT " & vbCrLf & _
                                                ",ManageKyokuNM " & vbCrLf & _
                                                ",ManageBusyoNM " & vbCrLf & _
                                                ",WorkFromNmb " & vbCrLf & _
                                                ",KikiUseCD " & vbCrLf & _
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
                                                ",RentalStDT " & vbCrLf & _
                                                ",RentalEdDT " & vbCrLf & _
                                                ",SetKyokuNM " & vbCrLf & _
                                                ",SetBusyoNM " & vbCrLf & _
                                                ",SetRoom " & vbCrLf & _
                                                ",SetBuil " & vbCrLf & _
                                                ",SetFloor " & vbCrLf & _
                                                ",SetDeskNo " & vbCrLf & _
                                                ",SetLANLength " & vbCrLf & _
                                                ",SetLANNum " & vbCrLf & _
                                                ",SetSocket " & vbCrLf & _
                                                ",SerialAimai " & vbCrLf & _
                                                ",ImageNmbAimai " & vbCrLf & _
                                                ",ManageBusyoNMAimai " & vbCrLf & _
                                                ",UsrIDAimai " & vbCrLf & _
                                                ",SetBusyoNMAimai " & vbCrLf & _
                                                ",SetRoomAimai " & vbCrLf & _
                                                ",SetBuilAimai " & vbCrLf & _
                                                ",SetFloorAimai " & vbCrLf & _
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
                                                ",ct.MemorySize " & vbCrLf & _
                                                ",ct.Kataban " & vbCrLf & _
                                                ",ct.Serial " & vbCrLf & _
                                                ",ct.MacAddress1 " & vbCrLf & _
                                                ",ct.MacAddress2 " & vbCrLf & _
                                                ",ct.Fuzokuhin " & vbCrLf & _
                                                ",ct.TypeKbn " & vbCrLf & _
                                                ",ct.SCKikiFixNmb " & vbCrLf & _
                                                ",ct.KikiState " & vbCrLf & _
                                                ",ct.ImageNmb " & vbCrLf & _
                                                ",ct.IntroductNmb " & vbCrLf & _
                                                ",ct.LeaseUpDT " & vbCrLf & _
                                                ",ct.SCHokanKbn " & vbCrLf & _
                                                ",ct.LastInfoDT " & vbCrLf & _
                                                ",ct.ManageKyokuNM " & vbCrLf & _
                                                ",ct.ManageBusyoNM " & vbCrLf & _
                                                ",ct.WorkFromNmb " & vbCrLf & _
                                                ",ct.KikiUseCD " & vbCrLf & _
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
                                                ",ct.RentalStDT " & vbCrLf & _
                                                ",ct.RentalEdDT " & vbCrLf & _
                                                ",ct.SetKyokuNM " & vbCrLf & _
                                                ",ct.SetBusyoNM " & vbCrLf & _
                                                ",ct.SetRoom " & vbCrLf & _
                                                ",ct.SetBuil " & vbCrLf & _
                                                ",ct.SetFloor " & vbCrLf & _
                                                ",ct.SetDeskNo " & vbCrLf & _
                                                ",ct.SetLANLength " & vbCrLf & _
                                                ",ct.SetLANNum " & vbCrLf & _
                                                ",ct.SetSocket " & vbCrLf & _
                                                ",ct.SerialAimai " & vbCrLf & _
                                                ",ct.ImageNmbAimai " & vbCrLf & _
                                                ",ct.ManageBusyoNMAimai " & vbCrLf & _
                                                ",ct.UsrIDAimai " & vbCrLf & _
                                                ",ct.SetBusyoNMAimai " & vbCrLf & _
                                                ",ct.SetRoomAimai " & vbCrLf & _
                                                ",ct.SetBuilAimai " & vbCrLf & _
                                                ",ct.SetFloorAimai " & vbCrLf & _
                                                ",:RegDT " & vbCrLf & _
                                                ",:RegGrpCD " & vbCrLf & _
                                                ",:RegID " & vbCrLf & _
                                                ",:UpdateDT " & vbCrLf & _
                                                ",:UpGrpCD " & vbCrLf & _
                                                ",:UpdateID " & vbCrLf & _
                                                "FROM CI_SAP_TB ct " & vbCrLf & _
                                                "WHERE ct.CINmb=:CINmb"

    'セット機器履歴新規登録（INSERT）用SQL
    Private strInsertSetKikiRirekiSql As String = "INSERT INTO SETKIKI_RTB ( " & vbCrLf & _
                                                  " CINmb " & vbCrLf & _
                                                  ",SetKikiMngNmb " & vbCrLf & _
                                                  ",RirekiNo " & vbCrLf & _
                                                  ",SetKikiID " & vbCrLf & _
                                                  ",EndUsrID " & vbCrLf & _
                                                  ",SetCINmb " & vbCrLf & _
                                                  ",SetRirekiNo " & vbCrLf & _
                                                  ",RegDT " & vbCrLf & _
                                                  ",RegGrpCD " & vbCrLf & _
                                                  ",RegID " & vbCrLf & _
                                                  ",UpdateDT " & vbCrLf & _
                                                  ",UpGrpCD " & vbCrLf & _
                                                  ",UpdateID " & vbCrLf & _
                                                  ") " & vbCrLf & _
                                                  "SELECT " & vbCrLf & _
                                                  " :CINmb " & vbCrLf & _
                                                  ",SetKikiMngNmb " & vbCrLf & _
                                                  ",:RirekiNo " & vbCrLf & _
                                                  ",st.SetKikiID " & vbCrLf & _
                                                  ",st.EndUsrID " & vbCrLf & _
                                                  ",st.CINmb " & vbCrLf & _
                                                  ",(SELECT MAX(RirekiNo) FROM CI_INFO_RTB WHERE CINmb = st.CINmb ) " & vbCrLf & _
                                                  ",:RegDT " & vbCrLf & _
                                                  ",:RegGrpCD " & vbCrLf & _
                                                  ",:RegID " & vbCrLf & _
                                                  ",:UpdateDT " & vbCrLf & _
                                                  ",:UpGrpCD " & vbCrLf & _
                                                  ",:UpdateID " & vbCrLf & _
                                                  "FROM SET_KIKI_MNG_TB st" & vbCrLf & _
                                                  "WHERE st.SetKikiID = :SetKikiID" & vbCrLf

    '複数人利用履歴新規登録（INSERT）用SQL
    Private strInsertShareRirekiSql As String = "INSERT INTO SHARE_RTB ( " & vbCrLf & _
                                                " CINmb " & vbCrLf & _
                                                ",RirekiNo " & vbCrLf & _
                                                ",RowNmb " & vbCrLf & _
                                                ",UsrID " & vbCrLf & _
                                                ",UsrNM " & vbCrLf & _
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
                                                ",st.UsrID " & vbCrLf & _
                                                ",st.UsrNM " & vbCrLf & _
                                                ",:RegDT " & vbCrLf & _
                                                ",:RegGrpCD " & vbCrLf & _
                                                ",:RegID " & vbCrLf & _
                                                ",:UpdateDT " & vbCrLf & _
                                                ",:UpGrpCD " & vbCrLf & _
                                                ",:UpdateID " & vbCrLf & _
                                                "FROM SHARE_TB st " & vbCrLf & _
                                                "WHERE st.CINmb=:CINmb"

    'オプションソフト履歴新規登録（INSERT）用SQL
    Private strInsertOptSoftRirekiSql As String = "INSERT INTO OPTSOFT_RTB ( " & vbCrLf & _
                                                  " CINmb " & vbCrLf & _
                                                  ",RirekiNo " & vbCrLf & _
                                                  ",RowNmb " & vbCrLf & _
                                                  ",SoftCD " & vbCrLf & _
                                                  ",RegDT " & vbCrLf & _
                                                  ",RegGrpCD " & vbCrLf & _
                                                  ",RegID " & vbCrLf & _
                                                  ",UpdateDT " & vbCrLf & _
                                                  ",UpGrpCD " & vbCrLf & _
                                                  ",UpdateID " & vbCrLf & _
                                                  ") " & vbCrLf & _
                                                  "SELECT " & vbCrLf & _
                                                  " ot.CINmb " & vbCrLf & _
                                                  ",:RirekiNo " & vbCrLf & _
                                                  ",ot.RowNmb " & vbCrLf & _
                                                  ",ot.SoftCD " & vbCrLf & _
                                                  ",:RegDT " & vbCrLf & _
                                                  ",:RegGrpCD " & vbCrLf & _
                                                  ",:RegID " & vbCrLf & _
                                                  ",:UpdateDT " & vbCrLf & _
                                                  ",:UpGrpCD " & vbCrLf & _
                                                  ",:UpdateID " & vbCrLf & _
                                                  "FROM OPTSOFT_TB ot " & vbCrLf & _
                                                  "WHERE ot.CINmb=:CINmb"

    '新規CI履歴番号取得 
    Private strSelectNewCIRirekiNoSql As String = "SELECT" & vbCrLf & _
                                                  " COALESCE(MAX(ct.RirekiNo),0)+1 AS RirekiNo" & vbCrLf & _
                                                  "FROM CI_INFO_RTB ct " & vbCrLf & _
                                                  "WHERE ct.CINmb = :CINmb"

    '登録理由履歴新規登録（INSERT）用SQL：汎用
    Private strInsertRegReasonSql As String = "INSERT INTO REGREASON_RTB ( " & vbCrLf & _
                                              " CINmb " & vbCrLf & _
                                              ",RirekiNo " & vbCrLf & _
                                              ",RegReason " & vbCrLf & _
                                              ",WorkCD " & vbCrLf & _
                                              ",WorkKbnCD " & vbCrLf & _
                                              ",ChgFlg " & vbCrLf & _
                                              ",ChgCINmb " & vbCrLf & _
                                              ",RegDT " & vbCrLf & _
                                              ",RegGrpCD " & vbCrLf & _
                                              ",RegID " & vbCrLf & _
                                              ",UpdateDT " & vbCrLf & _
                                              ",UpGrpCD " & vbCrLf & _
                                              ",UpdateID " & vbCrLf & _
                                              ") " & vbCrLf & _
                                              " SELECT " & vbCrLf & _
                                              " :CINmb " & vbCrLf & _
                                              ",:RirekiNo " & vbCrLf & _
                                              ",rt.RegReason " & vbCrLf & _
                                              ",rt.WorkCD " & vbCrLf & _
                                              ",rt.WorkKbnCD " & vbCrLf & _
                                              ",rt.ChgFlg " & vbCrLf & _
                                              ",rt.ChgCINmb " & vbCrLf & _
                                              ",:RegDT " & vbCrLf & _
                                              ",:RegGrpCD " & vbCrLf & _
                                              ",:RegID " & vbCrLf & _
                                              ",:UpdateDT " & vbCrLf & _
                                              ",:UpGrpCD " & vbCrLf & _
                                              ",:UpdateID " & vbCrLf & _
                                              " FROM regreason_rtb rt " & vbCrLf & _
                                              " WHERE rt.CINmb = :CINmb " & vbCrLf & _
                                              "   AND rt.RirekiNo = (SELECT MAX(RirekiNo) From regreason_rtb WHERE CINmb = :CINmb) "

    '原因リンク新規登録（INSERT）用SQL：汎用
    Private strInsertCauseLinkSql As String = "INSERT INTO CAUSELINK_RTB ( " & vbCrLf & _
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
                                              " SELECT " & vbCrLf & _
                                              " :CINmb " & vbCrLf & _
                                              ",:RirekiNo " & vbCrLf & _
                                              ",ct.ProcessKbn " & vbCrLf & _
                                              ",ct.MngNmb " & vbCrLf & _
                                              ",:RegDT " & vbCrLf & _
                                              ",:RegGrpCD " & vbCrLf & _
                                              ",:RegID " & vbCrLf & _
                                              ",:UpdateDT " & vbCrLf & _
                                              ",:UpGrpCD " & vbCrLf & _
                                              ",:UpdateID " & vbCrLf & _
                                              " FROM CAUSELINK_RTB ct " & vbCrLf & _
                                              " WHERE ct.CINmb = :CINmb " & vbCrLf & _
                                              "   AND ct.RirekiNo = (SELECT MAX(RirekiNo) - 1 From regreason_rtb WHERE CINmb = :CINmb) "

    '[add] 2014/06/09 e.okamura コピー不具合修正 Start
    'サポセン機器情報更新(コピー)用SQL
    Private strUpdateTmpCIInfoCopySql As String = " UPDATE ci_sap_tb AS cst1 " & vbCrLf & _
                                                  " SET UpdateDT       = :UpdateDT, " & vbCrLf & _
                                                  " UpGrpCD            = :UpGrpCD, " & vbCrLf & _
                                                  " UpdateID           = :UpdateID " & vbCrLf & _
                                                  " WHERE cst1.CINmb = :CINmb "

    'サポセン機器情報更新(コピー)用SQL
    Private strUpdateTmpCISapCopySql As String = " UPDATE ci_sap_tb AS cst1 " & vbCrLf & _
                                                 " SET UsrID          = cst2.UsrID, " & vbCrLf & _
                                                 " UsrNM              = cst2.UsrNM, " & vbCrLf & _
                                                 " UsrMailAdd         = cst2.UsrMailAdd, " & vbCrLf & _
                                                 " UsrTel             = cst2.UsrTel, " & vbCrLf & _
                                                 " UsrKyokuNM         = cst2.UsrKyokuNM, " & vbCrLf & _
                                                 " UsrBusyoNM         = cst2.UsrBusyoNM, " & vbCrLf & _
                                                 " UsrCompany         = cst2.UsrCompany, " & vbCrLf & _
                                                 " UsrContact         = cst2.UsrContact, " & vbCrLf & _
                                                 " UsrRoom            = cst2.UsrRoom, " & vbCrLf & _
                                                 " RentalStDT         = cst2.RentalStDT, " & vbCrLf & _
                                                 " RentalEdDT         = cst2.RentalEdDT, " & vbCrLf & _
                                                 " KikiUseCD          = cst2.KikiUseCD, " & vbCrLf & _
                                                 " ManageKyokuNM      = cst2.ManageKyokuNM, " & vbCrLf & _
                                                 " ManageBusyoNM      = cst2.ManageBusyoNM, " & vbCrLf & _
                                                 " SetKyokuNM         = cst2.SetKyokuNM, " & vbCrLf & _
                                                 " SetBusyoNM         = cst2.SetBusyoNM, " & vbCrLf & _
                                                 " SetRoom            = cst2.SetRoom, " & vbCrLf & _
                                                 " SetBuil            = cst2.SetBuil, " & vbCrLf & _
                                                 " SetFloor           = cst2.SetFloor, " & vbCrLf & _
                                                 " ManageBusyoNMAimai = cst2.ManageBusyoNMAimai, " & vbCrLf & _
                                                 " UsrIDAimai         = cst2.UsrIDAimai, " & vbCrLf & _
                                                 " SetBusyoNMAimai    = cst2.SetBusyoNMAimai, " & vbCrLf & _
                                                 " SetRoomAimai       = cst2.SetRoomAimai, " & vbCrLf & _
                                                 " SetBuilAimai       = cst2.SetBuilAimai, " & vbCrLf & _
                                                 " SetFloorAimai      = cst2.SetFloorAimai, " & vbCrLf & _
                                                 " UpdateDT           = :UpdateDT, " & vbCrLf & _
                                                 " UpGrpCD            = :UpGrpCD, " & vbCrLf & _
                                                 " UpdateID           = :UpdateID " & vbCrLf & _
                                                 " FROM ( " & vbCrLf & _
                                                 " SELECT " & vbCrLf & _
                                                 " CINmb, " & vbCrLf & _
                                                 " UsrID, " & vbCrLf & _
                                                 " UsrNM, " & vbCrLf & _
                                                 " UsrMailAdd, " & vbCrLf & _
                                                 " UsrTel, " & vbCrLf & _
                                                 " UsrKyokuNM, " & vbCrLf & _
                                                 " UsrBusyoNM, " & vbCrLf & _
                                                 " UsrCompany, " & vbCrLf & _
                                                 " UsrContact, " & vbCrLf & _
                                                 " UsrRoom, " & vbCrLf & _
                                                 " RentalStDT, " & vbCrLf & _
                                                 " RentalEdDT, " & vbCrLf & _
                                                 " KikiUseCD, " & vbCrLf & _
                                                 " ManageKyokuNM, " & vbCrLf & _
                                                 " ManageBusyoNM, " & vbCrLf & _
                                                 " SetKyokuNM, " & vbCrLf & _
                                                 " SetBusyoNM, " & vbCrLf & _
                                                 " SetRoom, " & vbCrLf & _
                                                 " SetBuil, " & vbCrLf & _
                                                 " SetFloor, " & vbCrLf & _
                                                 " ManageBusyoNMAimai, " & vbCrLf & _
                                                 " UsrIDAimai, " & vbCrLf & _
                                                 " SetBusyoNMAimai, " & vbCrLf & _
                                                 " SetRoomAimai, " & vbCrLf & _
                                                 " SetBuilAimai, " & vbCrLf & _
                                                 " SetFloorAimai " & vbCrLf & _
                                                 " FROM ci_sap_tb " & vbCrLf & _
                                                 " WHERE CINmb = :CINmb " & vbCrLf & _
                                                 " ) AS cst2 " & vbCrLf & _
                                                 " WHERE cst1.CINmb = :CINmb2 "
    '[add] 2014/06/09 e.okamura コピー不具合修正 End

    '[del] 2014/06/09 e.okamura コピー不具合修正 Start
    ''セット機器サポセン情報更新用SQL
    'Private strUpdateSetKikiSql As String = " UPDATE ci_sap_tb AS cst1 " & vbCrLf & _
    '                                        " SET UsrID          = cst2.UsrID, " & vbCrLf & _
    '                                        " UsrNM              = cst2.UsrNM, " & vbCrLf & _
    '                                        " UsrMailAdd         = cst2.UsrMailAdd, " & vbCrLf & _
    '                                        " UsrTel             = cst2.UsrTel, " & vbCrLf & _
    '                                        " UsrKyokuNM         = cst2.UsrKyokuNM, " & vbCrLf & _
    '                                        " UsrBusyoNM         = cst2.UsrBusyoNM, " & vbCrLf & _
    '                                        " UsrCompany         = cst2.UsrCompany, " & vbCrLf & _
    '                                        " UsrContact         = cst2.UsrContact, " & vbCrLf & _
    '                                        " UsrRoom            = cst2.UsrRoom, " & vbCrLf & _
    '                                        " RentalStDT         = cst2.RentalStDT, " & vbCrLf & _
    '                                        " RentalEdDT         = cst2.RentalEdDT, " & vbCrLf & _
    '                                        " KikiUseCD          = cst2.KikiUseCD, " & vbCrLf & _
    '                                        " ManageKyokuNM      = cst2.ManageKyokuNM, " & vbCrLf & _
    '                                        " ManageBusyoNM      = cst2.ManageBusyoNM, " & vbCrLf & _
    '                                        " SetKyokuNM         = cst2.SetKyokuNM, " & vbCrLf & _
    '                                        " SetBusyoNM         = cst2.SetBusyoNM, " & vbCrLf & _
    '                                        " SetRoom            = cst2.SetRoom, " & vbCrLf & _
    '                                        " SetBuil            = cst2.SetBuil, " & vbCrLf & _
    '                                        " SetFloor           = cst2.SetFloor, " & vbCrLf & _
    '                                        " ManageBusyoNMAimai = cst2.ManageBusyoNMAimai, " & vbCrLf & _
    '                                        " UsrIDAimai         = cst2.UsrIDAimai, " & vbCrLf & _
    '                                        " SetBusyoNMAimai    = cst2.SetBusyoNMAimai, " & vbCrLf & _
    '                                        " SetRoomAimai       = cst2.SetRoomAimai, " & vbCrLf & _
    '                                        " SetBuilAimai       = cst2.SetBuilAimai, " & vbCrLf & _
    '                                        " SetFloorAimai      = cst2.SetFloorAimai, " & vbCrLf & _
    '                                        " UpdateDT           = :UpdateDT, " & vbCrLf & _
    '                                        " UpGrpCD            = :UpGrpCD, " & vbCrLf & _
    '                                        " UpdateID           = :UpdateID " & vbCrLf & _
    '                                        " FROM ( " & vbCrLf & _
    '                                        " SELECT " & vbCrLf & _
    '                                        " CINmb, " & vbCrLf & _
    '                                        " UsrID, " & vbCrLf & _
    '                                        " UsrNM, " & vbCrLf & _
    '                                        " UsrMailAdd, " & vbCrLf & _
    '                                        " UsrTel, " & vbCrLf & _
    '                                        " UsrKyokuNM, " & vbCrLf & _
    '                                        " UsrBusyoNM, " & vbCrLf & _
    '                                        " UsrCompany, " & vbCrLf & _
    '                                        " UsrContact, " & vbCrLf & _
    '                                        " UsrRoom, " & vbCrLf & _
    '                                        " RentalStDT, " & vbCrLf & _
    '                                        " RentalEdDT, " & vbCrLf & _
    '                                        " KikiUseCD, " & vbCrLf & _
    '                                        " ManageKyokuNM, " & vbCrLf & _
    '                                        " ManageBusyoNM, " & vbCrLf & _
    '                                        " SetKyokuNM, " & vbCrLf & _
    '                                        " SetBusyoNM, " & vbCrLf & _
    '                                        " SetRoom, " & vbCrLf & _
    '                                        " SetBuil, " & vbCrLf & _
    '                                        " SetFloor, " & vbCrLf & _
    '                                        " ManageBusyoNMAimai, " & vbCrLf & _
    '                                        " UsrIDAimai, " & vbCrLf & _
    '                                        " SetBusyoNMAimai, " & vbCrLf & _
    '                                        " SetRoomAimai, " & vbCrLf & _
    '                                        " SetBuilAimai, " & vbCrLf & _
    '                                        " SetFloorAimai " & vbCrLf & _
    '                                        " FROM ci_sap_tb " & vbCrLf & _
    '                                        " WHERE CINmb = :CINmb " & vbCrLf & _
    '                                        " ) AS cst2 " & vbCrLf & _
    '                                        " WHERE cst1.CINmb IN ({0}) "
    '[del] 2014/06/09 e.okamura コピー不具合修正 End
    '[mod] 2013/10/11 t.fukuo セット機器履歴が余計に登録される不具合修正：END

    'CI共通情報履歴新規登録（INSERT）用SQL
    Private strInsertCIInfoRirekiSetKikiSql As String = "INSERT INTO CI_INFO_RTB ( " & vbCrLf & _
                                                 " CINmb " & vbCrLf & _
                                                 ",RirekiNo " & vbCrLf & _
                                                 ",CIKbnCD " & vbCrLf & _
                                                 ",KindCD " & vbCrLf & _
                                                 ",Num " & vbCrLf & _
                                                 ",CIStatusCD " & vbCrLf & _
                                                 ",SetKikiID " & vbCrLf & _
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
                                                 ",HBKF0002(ct.CINmb,1) AS RirekiNo " & vbCrLf & _
                                                 ",ct.CIKbnCD " & vbCrLf & _
                                                 ",ct.KindCD " & vbCrLf & _
                                                 ",ct.Num " & vbCrLf & _
                                                 ",ct.CIStatusCD " & vbCrLf & _
                                                 ",ct.SetKikiID " & vbCrLf & _
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
                                                 ",:RegDT " & vbCrLf & _
                                                 ",:RegGrpCD " & vbCrLf & _
                                                 ",:RegID " & vbCrLf & _
                                                 ",:UpdateDT " & vbCrLf & _
                                                 ",:UpGrpCD " & vbCrLf & _
                                                 ",:UpdateID " & vbCrLf & _
                                                 "FROM CI_INFO_TB ct " & vbCrLf & _
                                                 "WHERE ct.CINmb <> :CINmb AND ct.CINmb IN ({0})"

    'CIサポセン機器履歴新規登録（INSERT）用SQL
    Private strInsertCISapRirekiSetKikiSql As String = "INSERT INTO CI_SAP_RTB ( " & vbCrLf & _
                                                " CINmb " & vbCrLf & _
                                                ",RirekiNo " & vbCrLf & _
                                                ",MemorySize " & vbCrLf & _
                                                ",Kataban " & vbCrLf & _
                                                ",Serial " & vbCrLf & _
                                                ",MacAddress1 " & vbCrLf & _
                                                ",MacAddress2 " & vbCrLf & _
                                                ",Fuzokuhin " & vbCrLf & _
                                                ",TypeKbn " & vbCrLf & _
                                                ",SCKikiFixNmb " & vbCrLf & _
                                                ",KikiState " & vbCrLf & _
                                                ",ImageNmb " & vbCrLf & _
                                                ",IntroductNmb " & vbCrLf & _
                                                ",LeaseUpDT " & vbCrLf & _
                                                ",SCHokanKbn " & vbCrLf & _
                                                ",LastInfoDT " & vbCrLf & _
                                                ",ManageKyokuNM " & vbCrLf & _
                                                ",ManageBusyoNM " & vbCrLf & _
                                                ",WorkFromNmb " & vbCrLf & _
                                                ",KikiUseCD " & vbCrLf & _
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
                                                ",RentalStDT " & vbCrLf & _
                                                ",RentalEdDT " & vbCrLf & _
                                                ",SetKyokuNM " & vbCrLf & _
                                                ",SetBusyoNM " & vbCrLf & _
                                                ",SetRoom " & vbCrLf & _
                                                ",SetBuil " & vbCrLf & _
                                                ",SetFloor " & vbCrLf & _
                                                ",SetDeskNo " & vbCrLf & _
                                                ",SetLANLength " & vbCrLf & _
                                                ",SetLANNum " & vbCrLf & _
                                                ",SetSocket " & vbCrLf & _
                                                ",SerialAimai " & vbCrLf & _
                                                ",ImageNmbAimai " & vbCrLf & _
                                                ",ManageBusyoNMAimai " & vbCrLf & _
                                                ",UsrIDAimai " & vbCrLf & _
                                                ",SetBusyoNMAimai " & vbCrLf & _
                                                ",SetRoomAimai " & vbCrLf & _
                                                ",SetBuilAimai " & vbCrLf & _
                                                ",SetFloorAimai " & vbCrLf & _
                                                ",RegDT " & vbCrLf & _
                                                ",RegGrpCD " & vbCrLf & _
                                                ",RegID " & vbCrLf & _
                                                ",UpdateDT " & vbCrLf & _
                                                ",UpGrpCD " & vbCrLf & _
                                                ",UpdateID " & vbCrLf & _
                                                ") " & vbCrLf & _
                                                "SELECT " & vbCrLf & _
                                                " ct.CINmb " & vbCrLf & _
                                                ",HBKF0002(ct.CINmb,1) AS RirekiNo " & vbCrLf & _
                                                ",ct.MemorySize " & vbCrLf & _
                                                ",ct.Kataban " & vbCrLf & _
                                                ",ct.Serial " & vbCrLf & _
                                                ",ct.MacAddress1 " & vbCrLf & _
                                                ",ct.MacAddress2 " & vbCrLf & _
                                                ",ct.Fuzokuhin " & vbCrLf & _
                                                ",ct.TypeKbn " & vbCrLf & _
                                                ",ct.SCKikiFixNmb " & vbCrLf & _
                                                ",ct.KikiState " & vbCrLf & _
                                                ",ct.ImageNmb " & vbCrLf & _
                                                ",ct.IntroductNmb " & vbCrLf & _
                                                ",ct.LeaseUpDT " & vbCrLf & _
                                                ",ct.SCHokanKbn " & vbCrLf & _
                                                ",ct.LastInfoDT " & vbCrLf & _
                                                ",ct.ManageKyokuNM " & vbCrLf & _
                                                ",ct.ManageBusyoNM " & vbCrLf & _
                                                ",ct.WorkFromNmb " & vbCrLf & _
                                                ",ct.KikiUseCD " & vbCrLf & _
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
                                                ",ct.RentalStDT " & vbCrLf & _
                                                ",ct.RentalEdDT " & vbCrLf & _
                                                ",ct.SetKyokuNM " & vbCrLf & _
                                                ",ct.SetBusyoNM " & vbCrLf & _
                                                ",ct.SetRoom " & vbCrLf & _
                                                ",ct.SetBuil " & vbCrLf & _
                                                ",ct.SetFloor " & vbCrLf & _
                                                ",ct.SetDeskNo " & vbCrLf & _
                                                ",ct.SetLANLength " & vbCrLf & _
                                                ",ct.SetLANNum " & vbCrLf & _
                                                ",ct.SetSocket " & vbCrLf & _
                                                ",ct.SerialAimai " & vbCrLf & _
                                                ",ct.ImageNmbAimai " & vbCrLf & _
                                                ",ct.ManageBusyoNMAimai " & vbCrLf & _
                                                ",ct.UsrIDAimai " & vbCrLf & _
                                                ",ct.SetBusyoNMAimai " & vbCrLf & _
                                                ",ct.SetRoomAimai " & vbCrLf & _
                                                ",ct.SetBuilAimai " & vbCrLf & _
                                                ",ct.SetFloorAimai " & vbCrLf & _
                                                ",:RegDT " & vbCrLf & _
                                                ",:RegGrpCD " & vbCrLf & _
                                                ",:RegID " & vbCrLf & _
                                                ",:UpdateDT " & vbCrLf & _
                                                ",:UpGrpCD " & vbCrLf & _
                                                ",:UpdateID " & vbCrLf & _
                                                "FROM CI_SAP_TB ct " & vbCrLf & _
                                                "WHERE ct.CINmb <> :CINmb AND ct.CINmb IN ({0})"

    'セット機器履歴新規登録（INSERT）用SQL
    Private strInsertSetKikiRirekiSetKikiSql As String = "INSERT INTO SETKIKI_RTB ( " & vbCrLf & _
                                                  " CINmb " & vbCrLf & _
                                                  ",SetKikiMngNmb " & vbCrLf & _
                                                  ",RirekiNo " & vbCrLf & _
                                                  ",SetKikiID " & vbCrLf & _
                                                  ",EndUsrID " & vbCrLf & _
                                                  ",SetCINmb " & vbCrLf & _
                                                  ",SetRirekiNo " & vbCrLf & _
                                                  ",RegDT " & vbCrLf & _
                                                  ",RegGrpCD " & vbCrLf & _
                                                  ",RegID " & vbCrLf & _
                                                  ",UpdateDT " & vbCrLf & _
                                                  ",UpGrpCD " & vbCrLf & _
                                                  ",UpdateID " & vbCrLf & _
                                                  ") " & vbCrLf & _
                                                  "SELECT " & vbCrLf & _
                                                  " st.CINmb " & vbCrLf & _
                                                  ",SetKikiMngNmb " & vbCrLf & _
                                                  ",HBKF0002(st.CINmb,1) AS RirekiNo  " & vbCrLf & _
                                                  ",st.SetKikiID " & vbCrLf & _
                                                  ",st.EndUsrID " & vbCrLf & _
                                                  ",st.CINmb " & vbCrLf & _
                                                  ",(SELECT MAX(RirekiNo) FROM CI_INFO_RTB WHERE CINmb = st.CINmb ) " & vbCrLf & _
                                                  ",:RegDT " & vbCrLf & _
                                                  ",:RegGrpCD " & vbCrLf & _
                                                  ",:RegID " & vbCrLf & _
                                                  ",:UpdateDT " & vbCrLf & _
                                                  ",:UpGrpCD " & vbCrLf & _
                                                  ",:UpdateID " & vbCrLf & _
                                                  "FROM SET_KIKI_MNG_TB st" & vbCrLf & _
                                                  "WHERE st.CINmb <> :CINmb AND st.SetKikiID IN (SELECT SetKikiID FROM SET_KIKI_MNG_TB WHERE CINmb IN ({0}))" & vbCrLf

    '複数人利用履歴新規登録（INSERT）用SQL
    Private strInsertShareRirekiSetKikiSql As String = "INSERT INTO SHARE_RTB ( " & vbCrLf & _
                                                " CINmb " & vbCrLf & _
                                                ",RirekiNo " & vbCrLf & _
                                                ",RowNmb " & vbCrLf & _
                                                ",UsrID " & vbCrLf & _
                                                ",UsrNM " & vbCrLf & _
                                                ",RegDT " & vbCrLf & _
                                                ",RegGrpCD " & vbCrLf & _
                                                ",RegID " & vbCrLf & _
                                                ",UpdateDT " & vbCrLf & _
                                                ",UpGrpCD " & vbCrLf & _
                                                ",UpdateID " & vbCrLf & _
                                                ") " & vbCrLf & _
                                                "SELECT " & vbCrLf & _
                                                " st.CINmb " & vbCrLf & _
                                                ",HBKF0002(st.CINmb,1) AS RirekiNo " & vbCrLf & _
                                                ",st.RowNmb " & vbCrLf & _
                                                ",st.UsrID " & vbCrLf & _
                                                ",st.UsrNM " & vbCrLf & _
                                                ",:RegDT " & vbCrLf & _
                                                ",:RegGrpCD " & vbCrLf & _
                                                ",:RegID " & vbCrLf & _
                                                ",:UpdateDT " & vbCrLf & _
                                                ",:UpGrpCD " & vbCrLf & _
                                                ",:UpdateID " & vbCrLf & _
                                                "FROM SHARE_TB st " & vbCrLf & _
                                                "WHERE st.CINmb <> :CINmb AND st.CINmb IN ({0})"

    'オプションソフト履歴新規登録（INSERT）用SQL
    Private strInsertOptSoftRirekiSetKikiSql As String = "INSERT INTO OPTSOFT_RTB ( " & vbCrLf & _
                                                  " CINmb " & vbCrLf & _
                                                  ",RirekiNo " & vbCrLf & _
                                                  ",RowNmb " & vbCrLf & _
                                                  ",SoftCD " & vbCrLf & _
                                                  ",RegDT " & vbCrLf & _
                                                  ",RegGrpCD " & vbCrLf & _
                                                  ",RegID " & vbCrLf & _
                                                  ",UpdateDT " & vbCrLf & _
                                                  ",UpGrpCD " & vbCrLf & _
                                                  ",UpdateID " & vbCrLf & _
                                                  ") " & vbCrLf & _
                                                  "SELECT " & vbCrLf & _
                                                  " ot.CINmb " & vbCrLf & _
                                                  ",HBKF0002(ot.CINmb,1) AS RirekiNo " & vbCrLf & _
                                                  ",ot.RowNmb " & vbCrLf & _
                                                  ",ot.SoftCD " & vbCrLf & _
                                                  ",:RegDT " & vbCrLf & _
                                                  ",:RegGrpCD " & vbCrLf & _
                                                  ",:RegID " & vbCrLf & _
                                                  ",:UpdateDT " & vbCrLf & _
                                                  ",:UpGrpCD " & vbCrLf & _
                                                  ",:UpdateID " & vbCrLf & _
                                                  "FROM OPTSOFT_TB ot " & vbCrLf & _
                                                  "WHERE ot.CINmb <> :CINmb AND ot.CINmb IN ({0})"

    '登録理由履歴新規登録（INSERT）用SQL
    Private strInsertRegReasonSetKikiSql As String = "INSERT INTO REGREASON_RTB ( " & vbCrLf & _
                                              " CINmb " & vbCrLf & _
                                              ",RirekiNo " & vbCrLf & _
                                              ",RegReason " & vbCrLf & _
                                              ",WorkCD " & vbCrLf & _
                                              ",WorkKbnCD " & vbCrLf & _
                                              ",ChgFlg " & vbCrLf & _
                                              ",ChgCINmb " & vbCrLf & _
                                              ",RegDT " & vbCrLf & _
                                              ",RegGrpCD " & vbCrLf & _
                                              ",RegID " & vbCrLf & _
                                              ",UpdateDT " & vbCrLf & _
                                              ",UpGrpCD " & vbCrLf & _
                                              ",UpdateID " & vbCrLf & _
                                              ") " & vbCrLf & _
                                              " SELECT " & vbCrLf & _
                                              " rt.CINmb " & vbCrLf & _
                                              ",HBKF0002(rt.CINmb,1) AS RirekiNo  " & vbCrLf & _
                                              ",rt.RegReason " & vbCrLf & _
                                              ",rt.WorkCD " & vbCrLf & _
                                              ",rt.WorkKbnCD " & vbCrLf & _
                                              ",rt.ChgFlg " & vbCrLf & _
                                              ",rt.ChgCINmb " & vbCrLf & _
                                              ",:RegDT " & vbCrLf & _
                                              ",:RegGrpCD " & vbCrLf & _
                                              ",:RegID " & vbCrLf & _
                                              ",:UpdateDT " & vbCrLf & _
                                              ",:UpGrpCD " & vbCrLf & _
                                              ",:UpdateID " & vbCrLf & _
                                              " FROM regreason_rtb rt " & vbCrLf & _
                                              " WHERE (rt.CINmb,rt.RirekiNo) IN " & vbCrLf & _
                                              "(SELECT skt.CINmb,skt.LastUpRirekiNo FROM sap_mainte_kiki_tb skt WHERE skt.IncNmb = :IncNmb " & _
                                              "AND (skt.CINmb,skt.WorkNmb,skt.RowNmb) IN ({0}) ) "

    '原因リンク新規登録（INSERT）用SQL
    Private strInsertCauseLinkSetKikiSql As String = "INSERT INTO CAUSELINK_RTB ( " & vbCrLf & _
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
                                              " SELECT " & vbCrLf & _
                                              " ct.CINmb " & vbCrLf & _
                                              ",HBKF0002(ct.CINmb,1) AS RirekiNo " & vbCrLf & _
                                              ",ct.ProcessKbn " & vbCrLf & _
                                              ",ct.MngNmb " & vbCrLf & _
                                              ",:RegDT " & vbCrLf & _
                                              ",:RegGrpCD " & vbCrLf & _
                                              ",:RegID " & vbCrLf & _
                                              ",:UpdateDT " & vbCrLf & _
                                              ",:UpGrpCD " & vbCrLf & _
                                              ",:UpdateID " & vbCrLf & _
                                              " FROM CAUSELINK_RTB ct " & vbCrLf & _
                                              " WHERE (ct.CINmb,ct.RirekiNo) IN " & vbCrLf & _
                                              "(SELECT skt.CINmb,skt.LastUpRirekiNo FROM sap_mainte_kiki_tb skt WHERE skt.IncNmb = :IncNmb " & _
                                              "AND (skt.CINmb,skt.WorkNmb,skt.RowNmb) IN ({0})) "



    'セット機器CI番号検索用SQL
    '[mod] 2013/10/11 t.fukuo セット機器履歴が余計に登録される不具合修正：START
    'Private strSelectCINmbSetKiki As String = "SELECT CINmb FROM set_kiki_mng_tb WHERE CINmb <> :CINmb AND SetKikiID = (SELECT SetKikiID FROM set_kiki_mng_tb WHERE CINmb = :CINmb)"
    Private strSelectCINmbSetKiki As String = "SELECT t1.CINmb " & vbCrLf & _
                                              "FROM set_kiki_mng_tb t1 " & vbCrLf & _
                                              "JOIN sap_mainte_kiki_tb t2 ON t1.CINmb = t2.CINmb " & vbCrLf & _
                                              "JOIN sap_mainte_work_tb t3 ON t2.IncNmb = t3.IncNmb AND t2.WorkNmb = t3.WorkNmb " & vbCrLf & _
                                              "WHERE t1.CINmb <> :CINmb " & vbCrLf & _
                                              "  AND t1.SetKikiID = (SELECT SetKikiID FROM set_kiki_mng_tb WHERE CINmb = :CINmb) " & vbCrLf & _
                                              "  AND t2.IncNmb = :IncNmb " & vbCrLf & _
                                              "  AND t3.CompFlg = '0' " & vbCrLf & _
                                              "  AND t3.CancelFlg = '0' " & vbCrLf & _
                                              "  AND t3.WorkCD IN (:WorkCD_Set, :WorkCD_AddConfig) "

    '[mod] 2013/10/11 t.fukuo セット機器履歴が余計に登録される不具合修正：END

    'インシデント内のほかの機器CI番号検索用SQL
    '[mod] 2013/10/11 t.fukuo セット機器履歴が余計に登録される不具合修正：START
    'Private strSelectCINmbIncident As String = " SELECT SMK.CINmb " & vbCrLf & _
    '                                                            " FROM sap_mainte_kiki_tb SMK " & vbCrLf & _
    '                                                            " LEFT OUTER JOIN sap_mainte_work_tb SMKW ON SMK.IncNmb = SMKW.IncNmb AND SMK.WorkNmb = SMKW.WorkNmb " & vbCrLf & _
    '                                                            " AND SMKW.CompFlg = '0' AND SMKW.CancelFLg = '0' " & vbCrLf & _
    '                                                            " WHERE SMK.IncNmb = :IncNmb AND SMK.CINmb <> :CINmb AND SMKW.WorkCD = :WorkCD"
    Private strSelectCINmbIncident As String = " SELECT SMK.CINmb " & vbCrLf & _
                                               " FROM sap_mainte_kiki_tb SMK " & vbCrLf & _
                                               " JOIN sap_mainte_work_tb SMKW ON SMK.IncNmb = SMKW.IncNmb AND SMK.WorkNmb = SMKW.WorkNmb " & vbCrLf & _
                                               " AND SMKW.CompFlg = '0' AND SMKW.CancelFLg = '0' " & vbCrLf & _
                                               " WHERE SMK.IncNmb = :IncNmb AND SMK.CINmb <> :CINmb AND SMKW.WorkCD IN (:WorkCD_Set, :WorkCD_AddConfig) "
    '[mod] 2013/10/11 t.fukuo セット機器履歴が余計に登録される不具合修正：END

    'セット機器CI番号検索用SQL(登録理由、原因リンク用）
    Private strSelectCINmbSetKikiReason As String = "SELECT ct.CINmb,skt.WorkNmb,skt.RowNmb FROM ci_info_rtb ct WHERE ct.CINmb <> :CINmb AND ct.RirekiNo = skt.LastUpRirekiNo AND ct.SetKikiID = (SELECT SetKikiID FROM set_kiki_mng_tb WHERE CINmb = :CINmb)"

    'インシデント内のほかの機器CI番号検索用SQL(登録理由、原因リンク用）
    Private strSelectCINmbIncidentReason As String = " SELECT SMK.CINmb,SMK.WorkNmb,SMK.RowNmb " & vbCrLf & _
                                                                " FROM sap_mainte_kiki_tb SMK " & vbCrLf & _
                                                                " LEFT OUTER JOIN sap_mainte_work_tb SMKW ON SMK.IncNmb = SMKW.IncNmb AND SMK.WorkNmb = SMKW.WorkNmb " & vbCrLf & _
                                                                " AND SMKW.CompFlg = '0' AND SMKW.CancelFLg = '0' " & vbCrLf & _
                                                                " WHERE SMK.IncNmb = :IncNmb AND SMK.CINmb <> :CINmb AND SMKW.WorkCD = :WorkCD"

    '新規ログNo取得（SELECT）SQL
    Private strSelectNewRirekiNoSql As String = "SELECT " & vbCrLf & _
                                                "COALESCE(MAX(ct.logno),0)+1 AS LogNo " & vbCrLf & _
                                                "FROM incident_info_ltb ct " & vbCrLf & _
                                                "WHERE ct.incnmb=:incnmb "


    'INC共通情報ログ（Insert）SQL
    Private strInsertIncInfoLSql As String = "INSERT INTO  incident_info_ltb (" & vbCrLf & _
                                            " incnmb " & vbCrLf & _
                                            ",LogNo " & vbCrLf & _
                                            ",processkbn " & vbCrLf & _
                                            ",ukekbncd " & vbCrLf & _
                                            ",inckbncd " & vbCrLf & _
                                            ",processstatecd " & vbCrLf & _
                                            ",hasseidt " & vbCrLf & _
                                            ",kaitodt " & vbCrLf & _
                                            ",kanryodt " & vbCrLf & _
                                            ",priority " & vbCrLf & _
                                            ",errlevel " & vbCrLf & _
                                            ",title " & vbCrLf & _
                                            ",ukenaiyo " & vbCrLf & _
                                            ",taiokekka " & vbCrLf & _
                                            ",systemnmb " & vbCrLf & _
                                            ",outsidetoolnmb " & vbCrLf & _
                                            ",eventid " & vbCrLf & _
                                            ",source " & vbCrLf & _
                                            ",opceventid " & vbCrLf & _
                                            ",eventclass " & vbCrLf & _
                                            ",tantogrpcd " & vbCrLf & _
                                            ",inctantoid " & vbCrLf & _
                                            ",inctantonm " & vbCrLf & _
                                            ",domaincd " & vbCrLf & _
                                            ",partnercompany " & vbCrLf & _
                                            ",partnerid " & vbCrLf & _
                                            ",partnernm " & vbCrLf & _
                                            ",partnerkana " & vbCrLf & _
                                            ",partnerkyokunm " & vbCrLf & _
                                            ",usrbusyonm " & vbCrLf & _
                                            ",partnertel " & vbCrLf & _
                                            ",partnermailadd " & vbCrLf & _
                                            ",partnercontact " & vbCrLf & _
                                            ",partnerBase " & vbCrLf & _
                                            ",partnerroom " & vbCrLf & _
                                            ",shijisyoflg " & vbCrLf & _
                                            ",kengen " & vbCrLf & _
                                            ",rentalkiki " & vbCrLf & _
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
                                            ",titleaimai " & vbCrLf & _
                                            ",ukenaiyoaimai " & vbCrLf & _
                                            ",bikoaimai " & vbCrLf & _
                                            ",taiokekkaaimai " & vbCrLf & _
                                            ",eventidaimai " & vbCrLf & _
                                            ",sourceaimai " & vbCrLf & _
                                            ",opceventidaimai " & vbCrLf & _
                                            ",eventclassaimai " & vbCrLf & _
                                            ",IncTantIDAimai " & vbCrLf & _
                                            ",inctantnmaimai " & vbCrLf & _
                                            ",partneridaimai " & vbCrLf & _
                                            ",partnernmaimai " & vbCrLf & _
                                            ",usrbusyonmaimai " & vbCrLf & _
                                            ",kigencondcikbncd " & vbCrLf & _
                                            ",kigencondtypekbn " & vbCrLf & _
                                            ",kigencondkigen " & vbCrLf & _
                                            ",KigenCondUsrID " & vbCrLf & _
                                            ",RegDT " & vbCrLf & _
                                            ",RegGrpCD " & vbCrLf & _
                                            ",RegID " & vbCrLf & _
                                            ",UpdateDT " & vbCrLf & _
                                            ",UpGrpCD " & vbCrLf & _
                                            ",UpdateID " & vbCrLf & _
                                             ")" & vbCrLf & _
                                             "SELECT " & vbCrLf & _
                                             " incnmb " & vbCrLf & _
                                             ",:LogNo " & vbCrLf & _
                                             ",processkbn " & vbCrLf & _
                                             ",ukekbncd " & vbCrLf & _
                                             ",inckbncd " & vbCrLf & _
                                             ",processstatecd " & vbCrLf & _
                                             ",hasseidt " & vbCrLf & _
                                             ",kaitodt " & vbCrLf & _
                                             ",kanryodt " & vbCrLf & _
                                             ",priority " & vbCrLf & _
                                             ",errlevel " & vbCrLf & _
                                             ",title " & vbCrLf & _
                                             ",ukenaiyo " & vbCrLf & _
                                             ",taiokekka " & vbCrLf & _
                                             ",systemnmb " & vbCrLf & _
                                             ",outsidetoolnmb " & vbCrLf & _
                                             ",eventid " & vbCrLf & _
                                             ",source " & vbCrLf & _
                                             ",opceventid " & vbCrLf & _
                                             ",eventclass " & vbCrLf & _
                                             ",tantogrpcd " & vbCrLf & _
                                             ",inctantoid " & vbCrLf & _
                                             ",inctantonm " & vbCrLf & _
                                             ",domaincd " & vbCrLf & _
                                             ",partnercompany " & vbCrLf & _
                                             ",partnerid " & vbCrLf & _
                                             ",partnernm " & vbCrLf & _
                                             ",partnerkana " & vbCrLf & _
                                             ",partnerkyokunm " & vbCrLf & _
                                             ",usrbusyonm " & vbCrLf & _
                                             ",partnertel " & vbCrLf & _
                                             ",partnermailadd " & vbCrLf & _
                                             ",partnercontact " & vbCrLf & _
                                             ",partnerBase " & vbCrLf & _
                                             ",partnerroom " & vbCrLf & _
                                             ",shijisyoflg " & vbCrLf & _
                                             ",kengen " & vbCrLf & _
                                             ",rentalkiki " & vbCrLf & _
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
                                             ",titleaimai " & vbCrLf & _
                                             ",ukenaiyoaimai " & vbCrLf & _
                                             ",bikoaimai " & vbCrLf & _
                                             ",taiokekkaaimai " & vbCrLf & _
                                             ",eventidaimai " & vbCrLf & _
                                             ",sourceaimai " & vbCrLf & _
                                             ",opceventidaimai " & vbCrLf & _
                                             ",eventclassaimai " & vbCrLf & _
                                             ",IncTantIDAimai " & vbCrLf & _
                                             ",inctantnmaimai " & vbCrLf & _
                                             ",partneridaimai " & vbCrLf & _
                                             ",partnernmaimai " & vbCrLf & _
                                             ",usrbusyonmaimai " & vbCrLf & _
                                             ",kigencondcikbncd " & vbCrLf & _
                                             ",kigencondtypekbn " & vbCrLf & _
                                             ",kigencondkigen " & vbCrLf & _
                                             ",KigenCondUsrID " & vbCrLf & _
                                             ",RegDT " & vbCrLf & _
                                             ",RegGrpCD " & vbCrLf & _
                                             ",RegID " & vbCrLf & _
                                             ",UpdateDT " & vbCrLf & _
                                             ",UpGrpCD " & vbCrLf & _
                                             ",UpdateID " & vbCrLf & _
                                             "FROM incident_info_tb " & vbCrLf & _
                                             "WHERE incnmb = :incnmb "

    'INC作業履歴ログ（insert）SQL
    Private strInsertIncRirekiLSql As String = "INSERT INTO incident_wk_rireki_ltb (" & vbCrLf & _
                                               "SELECT " & vbCrLf & _
                                               " incnmb " & vbCrLf & _
                                               ",:LogNo " & vbCrLf & _
                                               ",workrirekinmb " & vbCrLf & _
                                               ",keikakbncd " & vbCrLf & _
                                               ",worknaiyo " & vbCrLf & _
                                               ",workscedt " & vbCrLf & _
                                               ",workstdt " & vbCrLf & _
                                               ",workeddt " & vbCrLf & _
                                               ",systemnmb " & vbCrLf & _
                                               ",worknaiyoaimai " & vbCrLf & _
                                               ",RegDT " & vbCrLf & _
                                               ",RegGrpCD " & vbCrLf & _
                                               ",RegID " & vbCrLf & _
                                               ",UpdateDT " & vbCrLf & _
                                               ",UpGrpCD " & vbCrLf & _
                                               ",UpdateID " & vbCrLf & _
                                               "FROM incident_wk_rireki_tb " & vbCrLf & _
                                               "WHERE incnmb = :incnmb " & vbCrLf & _
                                               ") "
    'INC作業担当ログ（insert）SQL
    Private strInsertIncTantoLSql As String = "INSERT INTO incident_wk_tanto_ltb (" & vbCrLf & _
                                              "SELECT " & vbCrLf & _
                                              " incnmb " & vbCrLf & _
                                              ",:LogNo " & vbCrLf & _
                                              ",workrirekinmb " & vbCrLf & _
                                              ",worktantonmb " & vbCrLf & _
                                              ",worktantogrpcd " & vbCrLf & _
                                              ",worktantoid " & vbCrLf & _
                                              ",worktantogrpnm " & vbCrLf & _
                                              ",worktantonm " & vbCrLf & _
                                              ",RegDT " & vbCrLf & _
                                              ",RegGrpCD " & vbCrLf & _
                                              ",RegID " & vbCrLf & _
                                              ",UpdateDT " & vbCrLf & _
                                              ",UpGrpCD " & vbCrLf & _
                                              ",UpdateID " & vbCrLf & _
                                              "FROM incident_wk_tanto_tb " & vbCrLf & _
                                              "WHERE incnmb = :incnmb " & vbCrLf & _
                                              ") "

    'INC機器情報ログ（insert）SQL
    Private strInsertInckikiLSql As String = "INSERT INTO incident_kiki_ltb (" & vbCrLf & _
                                             "SELECT " & vbCrLf & _
                                             " incnmb " & vbCrLf & _
                                             ",:LogNo " & vbCrLf & _
                                             ",kindcd " & vbCrLf & _
                                             ",num " & vbCrLf & _
                                             ",kikiinf " & vbCrLf & _
                                             ",EntryNmb " & vbCrLf & _
                                             ",RegDT " & vbCrLf & _
                                             ",RegGrpCD " & vbCrLf & _
                                             ",RegID " & vbCrLf & _
                                             ",UpdateDT " & vbCrLf & _
                                             ",UpGrpCD " & vbCrLf & _
                                             ",UpdateID " & vbCrLf & _
                                             "FROM incident_kiki_tb " & vbCrLf & _
                                             "WHERE incnmb = :incnmb " & vbCrLf & _
                                             ") "


    'INC対応関係者ログ（insert）SQL
    Private strInsertIncKankeiLSql As String = "INSERT INTO incident_kankei_ltb ( " & vbCrLf & _
                                               "SELECT " & vbCrLf & _
                                               " incnmb " & vbCrLf & _
                                               ",:LogNo " & vbCrLf & _
                                               ",RelationKbn " & vbCrLf & _
                                               ",RelationID " & vbCrLf & _
                                               ",EntryNmb " & vbCrLf & _
                                               ",RegDT " & vbCrLf & _
                                               ",RegGrpCD " & vbCrLf & _
                                               ",RegID " & vbCrLf & _
                                               ",UpdateDT " & vbCrLf & _
                                               ",UpGrpCD " & vbCrLf & _
                                               ",UpdateID " & vbCrLf & _
                                               "FROM incident_kankei_tb " & vbCrLf & _
                                               "WHERE incnmb = :incnmb " & vbCrLf & _
                                               ") "



    'プロセスリンク(元)ログ（insert）SQL
    Private strInsertPLinkMotoLSql As String = "INSERT INTO incident_process_link_ltb (" & vbCrLf & _
                                               "SELECT " & vbCrLf & _
                                               " :incnmb " & vbCrLf & _
                                               ",:LogNo " & vbCrLf & _
                                               ",LinkMotoProcesskbn " & vbCrLf & _
                                               ",LinkMotoNmb " & vbCrLf & _
                                               ",LinkSakiProcesskbn " & vbCrLf & _
                                               ",LinkSakiNmb " & vbCrLf & _
                                               ",EntryDT " & vbCrLf & _
                                               ",RegDT " & vbCrLf & _
                                               ",RegGrpCD " & vbCrLf & _
                                               ",RegID " & vbCrLf & _
                                               ",UpdateDT " & vbCrLf & _
                                               ",UpGrpCD " & vbCrLf & _
                                               ",UpdateID " & vbCrLf & _
                                               "FROM process_link_tb " & vbCrLf & _
                                               "WHERE LinkMotoNmb  = :incnmb " & vbCrLf & _
                                               "AND   LinkMotoProcesskbn = :pkbn " & vbCrLf & _
                                               ") "


    'INC関連ファイル情報ログ（insert）SQL
    Private strInsertIncFileLSql As String = "INSERT INTO incident_file_ltb (" & vbCrLf & _
                                             "SELECT " & vbCrLf & _
                                             " incnmb " & vbCrLf & _
                                             ",:LogNo " & vbCrLf & _
                                             ",filemngnmb " & vbCrLf & _
                                             ",filenaiyo " & vbCrLf & _
                                             ",EntryNmb " & vbCrLf & _
                                             ",RegDT " & vbCrLf & _
                                             ",RegGrpCD " & vbCrLf & _
                                             ",RegID " & vbCrLf & _
                                             ",UpdateDT " & vbCrLf & _
                                             ",UpGrpCD " & vbCrLf & _
                                             ",UpdateID " & vbCrLf & _
                                             "FROM incident_file_tb " & vbCrLf & _
                                             "WHERE incnmb = :incnmb " & vbCrLf & _
                                             ") "

    '⑧	サポセン機器メンテナンス作業ログテーブル
    Private strInsertSapMainteWorkLSql As String = _
                                               "INSERT INTO SAP_MAINTE_WORK_LTB ( " & vbCrLf & _
                                               " IncNmb " & vbCrLf & _
                                               ",LogNo " & vbCrLf & _
                                               ",WorkNmb " & vbCrLf & _
                                               ",WorkCD " & vbCrLf & _
                                               ",WorkBiko " & vbCrLf & _
                                               ",WorkSceDT " & vbCrLf & _
                                               ",WorkCompDT " & vbCrLf & _
                                               ",CompFlg " & vbCrLf & _
                                               ",CancelFLg " & vbCrLf & _
                                               ",RegDT " & vbCrLf & _
                                               ",RegGrpCD " & vbCrLf & _
                                               ",RegID " & vbCrLf & _
                                               ",UpdateDT " & vbCrLf & _
                                               ",UpGrpCD " & vbCrLf & _
                                               ",UpdateID " & vbCrLf & _
                                               ") " & vbCrLf & _
                                               "SELECT" & vbCrLf & _
                                               " st.IncNmb " & vbCrLf & _
                                               ",:LogNo " & vbCrLf & _
                                               ",st.WorkNmb " & vbCrLf & _
                                               ",st.WorkCD " & vbCrLf & _
                                               ",st.WorkBiko " & vbCrLf & _
                                               ",st.WorkSceDT " & vbCrLf & _
                                               ",st.WorkCompDT " & vbCrLf & _
                                               ",st.CompFlg " & vbCrLf & _
                                               ",st.CancelFLg " & vbCrLf & _
                                               ",:RegDT " & vbCrLf & _
                                               ",:RegGrpCD " & vbCrLf & _
                                               ",:RegID " & vbCrLf & _
                                               ",:UpdateDT " & vbCrLf & _
                                               ",:UpGrpCD " & vbCrLf & _
                                               ",:UpdateID " & vbCrLf & _
                                               "FROM SAP_MAINTE_WORK_TB st" & vbCrLf & _
                                               "WHERE st.IncNmb = :IncNmb" & vbCrLf


    '⑨	サポセン機器メンテナンス機器ログテーブル
    Private strInsertSapMainteKikiLSql As String = _
                                               "INSERT INTO SAP_MAINTE_KIKI_LTB ( " & vbCrLf & _
                                               " IncNmb " & vbCrLf & _
                                               ",LogNo " & vbCrLf & _
                                               ",WorkNmb " & vbCrLf & _
                                               ",RowNmb " & vbCrLf & _
                                               ",CINmb " & vbCrLf & _
                                               ",ChgFlg " & vbCrLf & _
                                               ",ChgNmb " & vbCrLf & _
                                               ",CepalateFlg " & vbCrLf & _
                                               ",RegRirekiNo " & vbCrLf & _
                                               ",LastUpRirekiNo " & vbCrLf & _
                                               ",RegDT " & vbCrLf & _
                                               ",RegGrpCD " & vbCrLf & _
                                               ",RegID " & vbCrLf & _
                                               ",UpdateDT " & vbCrLf & _
                                               ",UpGrpCD " & vbCrLf & _
                                               ",UpdateID " & vbCrLf & _
                                               ") " & vbCrLf & _
                                               "SELECT" & vbCrLf & _
                                               " st.IncNmb " & vbCrLf & _
                                               ",:LogNo " & vbCrLf & _
                                               ",st.WorkNmb " & vbCrLf & _
                                               ",st.RowNmb " & vbCrLf & _
                                               ",st.CINmb " & vbCrLf & _
                                               ",st.ChgFlg " & vbCrLf & _
                                               ",st.ChgNmb " & vbCrLf & _
                                               ",st.CepalateFlg " & vbCrLf & _
                                               ",st.RegRirekiNo " & vbCrLf & _
                                               ",st.LastUpRirekiNo " & vbCrLf & _
                                               ",:RegDT " & vbCrLf & _
                                               ",:RegGrpCD " & vbCrLf & _
                                               ",:RegID " & vbCrLf & _
                                               ",:UpdateDT " & vbCrLf & _
                                               ",:UpGrpCD " & vbCrLf & _
                                               ",:UpdateID " & vbCrLf & _
                                               "FROM SAP_MAINTE_KIKI_TB st" & vbCrLf & _
                                               "WHERE st.IncNmb = :IncNmb" & vbCrLf

    'CIサポセン機器メンテナンス機器更新（UPDATE）用SQL
    Private strUpdateSapMainteKikiSql As String = _
                                              "UPDATE SAP_MAINTE_KIKI_TB" & vbCrLf & _
                                              "SET" & vbCrLf & _
                                              "LastUpRirekiNo = :LastUpRirekiNo" & vbCrLf & _
                                              ",UpdateDT   = :UpdateDT" & vbCrLf & _
                                              ",UpGrpCD    = :UpGrpCD" & vbCrLf & _
                                              ",UpdateID   = :UpdateID" & vbCrLf & _
                                              "WHERE IncNmb  = :IncNmb" & vbCrLf & _
                                              "  AND WorkNmb = :WorkNmb" & vbCrLf & _
                                              "  AND CINmb   = :CINmb" & vbCrLf

    ''' <summary>
    ''' 【編集モード】CI共通情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/13 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ取得先を保存用テーブル→標準テーブルへ変更（参照モードと同様にする）</p>
    ''' </para></remarks>
    Public Function SetSelectCIInfoSqlForEdit(ByRef Adapter As NpgsqlDataAdapter, _
                                              ByVal Cn As NpgsqlConnection, _
                                              ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectCIInfo & strFromWhereCIInfoForRef

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CI番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb                      'CI番号
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
    ''' 【参照モード】CI共通情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/13 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCIInfoSqlForRef(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectCIInfo & strFromWhereCIInfoForRef

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CI番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb                      'CI番号
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
    ''' 【履歴モード】CI共通情報履歴取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/13 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCIInfoRSql(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectCIInfo & strFromWhereCIInfoForRireki

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CI番号
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))     '履歴番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb                      'CI番号
                .Parameters("RirekiNo").Value = dataHBKB0601.PropIntRirekiNo                '履歴番号
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
    ''' 【編集モード】複数人利用取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>複数人利用取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/13 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ取得先を保存用テーブル→標準テーブルへ変更（参照モードと同様にする）</p>
    ''' </para></remarks>
    Public Function SetSelectShareSqlForEdit(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectShareSql & strFromWhereShareForRef & strOrderByShare

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CI番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb                      'CI番号
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
    ''' 【参照モード】複数人利用取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>複数人利用取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/13 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectShareSqlForRef(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectShareSql & strFromWhereShareForRef & strOrderByShare

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CI番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb                      'CI番号
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
    ''' 【履歴モード】複数人利用履歴情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>複数人利用履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/13 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectShareSqlForRireki(ByRef Adapter As NpgsqlDataAdapter, _
                                               ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectShareSql & strFromWhereShareForRireki & strOrderByShare

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CI番号
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))     '履歴番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb                      'CI番号
                .Parameters("RirekiNo").Value = dataHBKB0601.PropIntRirekiNo                '履歴番号
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
    ''' 【編集モード】オプションソフト取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>オプションソフト取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/13 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ取得先を保存用テーブル→標準テーブルへ変更（参照モードと同様にする）</p>
    ''' </para></remarks>
    Public Function SetSelectOptSoftSqlForEdit(ByRef Adapter As NpgsqlDataAdapter, _
                                               ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectOptSoftSql & strFromWhereOptSoftForRef & strOrderByOptSoft

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CI番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb                      'CI番号
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
    ''' 【参照モード】オプションソフト取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>オプションソフト取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/13 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectOptSoftSqlForRef(ByRef Adapter As NpgsqlDataAdapter, _
                                              ByVal Cn As NpgsqlConnection, _
                                              ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectOptSoftSql & strFromWhereOptSoftForRef & strOrderByOptSoft

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CI番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb                      'CI番号
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
    ''' 【参照モード】オプションソフト履歴取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>オプションソフト履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/13 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectOptSoftSqlForRireki(ByRef Adapter As NpgsqlDataAdapter, _
                                                 ByVal Cn As NpgsqlConnection, _
                                                 ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectOptSoftSql & strFromWhereOptSoftForRireki & strOrderByOptSoft

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CI番号
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))     '履歴番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb                      'CI番号
                .Parameters("RirekiNo").Value = dataHBKB0601.PropIntRirekiNo                '履歴番号
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
    ''' 【編集モード】セット機器取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>セット機器取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/13 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ取得先を保存用テーブル→標準テーブルへ変更（参照モードと同様にする）</p>
    ''' </para></remarks>
    Public Function SetSelectSetKikiSqlForEdit(ByRef Adapter As NpgsqlDataAdapter, _
                                               ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = String.Format(strSelectSetKikiSql, strFromWhereSetKikiForRef)

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CI番号
                .Add(New NpgsqlParameter("CIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))      'CI種別CD
                .Add(New NpgsqlParameter("SetKikiID", NpgsqlTypes.NpgsqlDbType.Integer))        'セット機器ID
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb                      'CI番号
                .Parameters("CIKbnCD").Value = CI_TYPE_SUPORT                               'CI種別CD
                .Parameters("SetKikiID").Value = dataHBKB0601.PropIntSetKikiID                      'セット機器ID
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
    ''' 【参照モード】セット機器取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>セット機器取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/13 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ取得先を保存用テーブル→標準テーブルへ変更（参照モードと同様にする）</p>
    ''' </para></remarks>
    Public Function SetSelectSetKikiSqlForRef(ByRef Adapter As NpgsqlDataAdapter, _
                                              ByVal Cn As NpgsqlConnection, _
                                              ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = String.Format(strSelectSetKikiSql, strFromWhereSetKikiForRef)

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))      'CI種別CD
                .Add(New NpgsqlParameter("SetKikiID", NpgsqlTypes.NpgsqlDbType.Integer))        'セット機器ID
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CIKbnCD").Value = CI_TYPE_SUPORT                               'CI種別CD
                .Parameters("SetKikiID").Value = dataHBKB0601.PropIntSetKikiID                       'セット機器ID
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
    ''' 【履歴モード】セット機器履歴取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>セット機器履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/13 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ取得先を保存用テーブル→標準テーブルへ変更（参照モードと同様にする）</p>
    ''' </para></remarks>
    Public Function SetSelectSetKikiSqlForRireki(ByRef Adapter As NpgsqlDataAdapter, _
                                                 ByVal Cn As NpgsqlConnection, _
                                                 ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = String.Format(strSelectSetKikiSql, strFromWhereSetKikiForRireki)

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))      'CI種別CD
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CI番号
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))     '履歴番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CIKbnCD").Value = CI_TYPE_SUPORT                               'CI種別CD
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb                      'CI番号
                .Parameters("RirekiNo").Value = dataHBKB0601.PropIntRirekiNo                '履歴番号
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
    ''' 【編集／参照モード】原因リンク履歴取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>原因リンク履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/13 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCauseLinkSqlForRef(ByRef Adapter As NpgsqlDataAdapter, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)　
            strSQL = strSelectCauseLink & strFromWhereCauseLinkForRef & strOrderByCauseLink

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
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb                           'CI番号
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
    ''' 【履歴モード】原因リンク履歴取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>原因リンク履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/13 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCauseLinkSqlForRireki(ByRef Adapter As NpgsqlDataAdapter, _
                                                   ByVal Cn As NpgsqlConnection, _
                                                   ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectCauseLink & strFromWhereCauseLinkForRireki & strOrderByCauseLink

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
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb                           'CI番号
                .Parameters("RirekiNo").Value = dataHBKB0601.PropIntRirekiNo                     '履歴番号
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
    ''' 【編集／参照モード】登録理由履歴取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>登録理由履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/13 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectRegReasonSqlForRef(ByRef Adapter As NpgsqlDataAdapter, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectRegReason & strFromWhereRegReasonForRef & strOrderByRegReason

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CI番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb                      'CI番号
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
    ''' 【履歴モード】登録理由履歴取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>登録理由履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/13 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectRegReasonSqlForRireki(ByRef Adapter As NpgsqlDataAdapter, _
                                                   ByVal Cn As NpgsqlConnection, _
                                                   ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectRegReason & strFromWhereRegReasonForRireki & strOrderByRegReason

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CI番号
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))     '履歴番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb                      'CI番号
                .Parameters("RirekiNo").Value = dataHBKB0601.PropIntRirekiNo                '履歴番号
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
    ''' 【編集モード】イメージ番号存在チェック用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>指定されたイメージ番号のマスタ存在件数を取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/13 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSameImageNmbCntSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectSameImageNmbCntSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("ImageNmb", NpgsqlTypes.NpgsqlDbType.Varchar))        'イメージ番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("ImageNmb").Value = dataHBKB0601.PropTxtImageNmb.Text              'イメージ番号
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
    ''' 【編集モード】セット機器グループ番号チェック用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>指定されたセット機器Noのセット機器グループ番号を取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/13 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSetKikiGrpNoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Dim strSetKikiNo As String

        '定数宣言
        Const BIND_SETKIKINO As String = ":SetKikiNo"

        Try

            'SQL文(SELECT)
            strSQL = strSelectSetKikiGrpNoSql

            'WHERE句作成
            With dataHBKB0601

                'セット機器番号を条件に追加
                strWhere &= "AND "
                strWhere &= "(km.KindNM || ct.Num) IN ("
                For i As Integer = 0 To .PropAryStrSetKikiNo.Count - 1

                    If i > 0 Then
                        strWhere &= ","
                    End If
                    strSetKikiNo = BIND_SETKIKINO & i.ToString()
                    strWhere &= "SUBSTR(" & strSetKikiNo & ", 1, LENGTH(" & strSetKikiNo & ")-5) || SUBSTR(" & strSetKikiNo & ", LENGTH(" & strSetKikiNo & ")-4, 5)" & vbCrLf

                Next
                strWhere &= ")" & vbCrLf

            End With

            '作成したWHERE句をセット
            strSQL &= strWhere

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型と値をセット
            With Adapter.SelectCommand

                'CI種別コード
                .Parameters.Add(New NpgsqlParameter("CIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                .Parameters("CIKbnCD").Value = CI_TYPE_SUPORT

                'セット機器番号
                For i As Integer = 0 To dataHBKB0601.PropAryStrSetKikiNo.Count - 1
                    .Parameters.Add(New NpgsqlParameter("SetKikiNo" & i.ToString(), NpgsqlTypes.NpgsqlDbType.Varchar))
                    .Parameters("SetKikiNo" & i.ToString()).Value = dataHBKB0601.PropAryStrSetKikiNo(i).ToString()
                Next

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
    ''' 【編集モード】セット機器No存在チェック用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>指定されたセット機器Noのテーブル存在件数を取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/13 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSameSetKikiCntSql(ByRef Adapter As NpgsqlDataAdapter, _
                                               ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectSameSetKikiCntSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))        'イメージ番号
                .Add(New NpgsqlParameter("SetKikiNo", NpgsqlTypes.NpgsqlDbType.Varchar))      'セット機器No
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CIKbnCD").Value = CI_TYPE_SUPORT                                 'イメージ番号
                .Parameters("SetKikiNo").Value = dataHBKB0601.PropStrSetKikiNo                'セット機器No
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
    ''' 【編集モード】サーバー日付取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/13 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB0601 As DataHBKB0601) As Boolean

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
    ''' 【編集モード】CI共通情報更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/02 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ登録先を保存用テーブル→標準テーブルへ変更</p>
    ''' </para></remarks>
    Public Function SetUpdateTmpCIInfoSql(ByRef Cmd As NpgsqlCommand, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Dim strBikoAimai As String = ""         'フリーテキスト（あいまい）

        Try

            'SQL文(INSERT)
            strSQL = strUpdateTmpCIInfoSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CIStatusCD", NpgsqlTypes.NpgsqlDbType.Varchar))       'ステータスCD
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
                .Add(New NpgsqlParameter("BikoAimai", NpgsqlTypes.NpgsqlDbType.Varchar))        'フリーテキスト（あいまい）
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))            'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("CIStatusCD").Value = dataHBKB0601.PropCmbCIStatus.SelectedValue    'ステータスCD

                'CIオーナー名が入力されている場合のみCIオーナーCDに値をセット
                If dataHBKB0601.PropTxtCIOwnerNM.Text.Trim() <> "" Then
                    .Parameters("CIOwnerCD").Value = dataHBKB0601.PropLblCIOwnerCD.Text         'CIオーナーCD
                Else
                    .Parameters("CIOwnerCD").Value = ""
                End If

                .Parameters("CINaiyo").Value = dataHBKB0601.PropTxtCINaiyo.Text                 '説明
                .Parameters("BIko1").Value = dataHBKB0601.PropTxtBIko1.Text                     'フリーテキスト１
                .Parameters("Biko2").Value = dataHBKB0601.PropTxtBIko2.Text                     'フリーテキスト２
                .Parameters("BIko3").Value = dataHBKB0601.PropTxtBIko3.Text                     'フリーテキスト３
                .Parameters("Biko4").Value = dataHBKB0601.PropTxtBIko4.Text                     'フリーテキスト４
                .Parameters("Biko5").Value = dataHBKB0601.PropTxtBIko5.Text                     'フリーテキスト５

                'フリーフラグ１～５
                If dataHBKB0601.PropChkFreeFlg1.Checked = True Then
                    .Parameters("FreeFlg1").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg1").Value = FREE_FLG_OFF
                End If
                If dataHBKB0601.PropChkFreeFlg2.Checked = True Then
                    .Parameters("FreeFlg2").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg2").Value = FREE_FLG_OFF
                End If
                If dataHBKB0601.PropChkFreeFlg3.Checked = True Then
                    .Parameters("FreeFlg3").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg3").Value = FREE_FLG_OFF
                End If
                If dataHBKB0601.PropChkFreeFlg4.Checked = True Then
                    .Parameters("FreeFlg4").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg4").Value = FREE_FLG_OFF
                End If
                If dataHBKB0601.PropChkFreeFlg5.Checked = True Then
                    .Parameters("FreeFlg5").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg5").Value = FREE_FLG_OFF
                End If

                'あいまい検索文字列設定
                '※フリーワード（あいまい）　：分類１～２、名称、説明を連結
                '※フリーテキスト（あいまい）：フリーテキスト１～５を連結
                strBikoAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0601.PropTxtBIko1.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKB0601.PropTxtBIko2.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKB0601.PropTxtBIko3.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKB0601.PropTxtBIko4.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKB0601.PropTxtBIko5.Text)
                .Parameters("BikoAimai").Value = strBikoAimai               'フリーテキスト（あいまい）

                .Parameters("UpdateDT").Value = dataHBKB0601.PropDtmSysDate '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD              '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                  '最終更新者ID
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb      'CI番号
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
    ''' 【編集モード】CIサポセン機器更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIサポセン機器更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/02 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ登録先を保存用テーブル→標準テーブルへ変更</p>
    ''' </para></remarks>
    Public Function SetUpdateTmpCISapSql(ByRef Cmd As NpgsqlCommand, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""                   'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strUpdateTmpCIsapSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("MemorySize", NpgsqlTypes.NpgsqlDbType.Varchar))                   'メモリ容量
                .Add(New NpgsqlParameter("Serial", NpgsqlTypes.NpgsqlDbType.Varchar))                       '製造番号（シリアル）
                .Add(New NpgsqlParameter("MacAddress1", NpgsqlTypes.NpgsqlDbType.Varchar))                  'MACアドレス1
                .Add(New NpgsqlParameter("MacAddress2", NpgsqlTypes.NpgsqlDbType.Varchar))                  'MACアドレス2
                .Add(New NpgsqlParameter("Fuzokuhin", NpgsqlTypes.NpgsqlDbType.Varchar))                    '付属品
                .Add(New NpgsqlParameter("TypeKbn", NpgsqlTypes.NpgsqlDbType.Varchar))                      'タイプ
                .Add(New NpgsqlParameter("SCKikiFixNmb", NpgsqlTypes.NpgsqlDbType.Varchar))                 'サポセン機器固定資産番号
                .Add(New NpgsqlParameter("KikiState", NpgsqlTypes.NpgsqlDbType.Varchar))                    '機器状態
                .Add(New NpgsqlParameter("ImageNmb", NpgsqlTypes.NpgsqlDbType.Varchar))                     'イメージ番号
                .Add(New NpgsqlParameter("LeaseUpDT", NpgsqlTypes.NpgsqlDbType.Varchar))                    'リース期限日（機器）
                .Add(New NpgsqlParameter("SCHokanKbn", NpgsqlTypes.NpgsqlDbType.Varchar))                   'サービスセンター保管機
                .Add(New NpgsqlParameter("LastInfoDT", NpgsqlTypes.NpgsqlDbType.Varchar))                   '最終お知らせ日
                .Add(New NpgsqlParameter("ManageKyokuNM", NpgsqlTypes.NpgsqlDbType.Varchar))                '管理局
                .Add(New NpgsqlParameter("ManageBusyoNM", NpgsqlTypes.NpgsqlDbType.Varchar))                '管理部署
                .Add(New NpgsqlParameter("WorkFromNmb", NpgsqlTypes.NpgsqlDbType.Varchar))                  '作業の元
                .Add(New NpgsqlParameter("KikiUseCD", NpgsqlTypes.NpgsqlDbType.Varchar))                    '機器利用形態CD
                .Add(New NpgsqlParameter("IPUseCD", NpgsqlTypes.NpgsqlDbType.Varchar))                      'IP割当種類CD
                .Add(New NpgsqlParameter("FixedIP", NpgsqlTypes.NpgsqlDbType.Varchar))                      '固定IP
                .Add(New NpgsqlParameter("UsrID", NpgsqlTypes.NpgsqlDbType.Varchar))                        'ユーザーID
                .Add(New NpgsqlParameter("UsrNM", NpgsqlTypes.NpgsqlDbType.Varchar))                        'ユーザー氏名
                .Add(New NpgsqlParameter("UsrCompany", NpgsqlTypes.NpgsqlDbType.Varchar))                   'ユーザー所属会社
                .Add(New NpgsqlParameter("UsrKyokuNM", NpgsqlTypes.NpgsqlDbType.Varchar))                   'ユーザー所属局
                .Add(New NpgsqlParameter("UsrBusyoNM", NpgsqlTypes.NpgsqlDbType.Varchar))                   'ユーザー所属部署
                .Add(New NpgsqlParameter("UsrTel", NpgsqlTypes.NpgsqlDbType.Varchar))                       'ユーザー電話番号
                .Add(New NpgsqlParameter("UsrMailAdd", NpgsqlTypes.NpgsqlDbType.Varchar))                   'ユーザーメールアドレス
                .Add(New NpgsqlParameter("UsrContact", NpgsqlTypes.NpgsqlDbType.Varchar))                   'ユーザー連絡先
                .Add(New NpgsqlParameter("UsrRoom", NpgsqlTypes.NpgsqlDbType.Varchar))                      'ユーザー番組/部屋
                .Add(New NpgsqlParameter("RentalStDT", NpgsqlTypes.NpgsqlDbType.Varchar))                   'レンタル開始日
                .Add(New NpgsqlParameter("RentalEdDT", NpgsqlTypes.NpgsqlDbType.Varchar))                   'レンタル期限日
                .Add(New NpgsqlParameter("SetKyokuNM", NpgsqlTypes.NpgsqlDbType.Varchar))                   '設置局
                .Add(New NpgsqlParameter("SetBusyoNM", NpgsqlTypes.NpgsqlDbType.Varchar))                   '設置部署
                .Add(New NpgsqlParameter("SetRoom", NpgsqlTypes.NpgsqlDbType.Varchar))                      '設置番組/部屋
                .Add(New NpgsqlParameter("SetBuil", NpgsqlTypes.NpgsqlDbType.Varchar))                      '設置建物
                .Add(New NpgsqlParameter("SetFloor", NpgsqlTypes.NpgsqlDbType.Varchar))                     '設置フロア
                .Add(New NpgsqlParameter("SetDeskNo", NpgsqlTypes.NpgsqlDbType.Varchar))                    '設置デスクNo
                .Add(New NpgsqlParameter("SetLANLength", NpgsqlTypes.NpgsqlDbType.Varchar))                 '設置LANケーブル長さ
                .Add(New NpgsqlParameter("SetLANNum", NpgsqlTypes.NpgsqlDbType.Varchar))                    '設置LANケーブル番号
                .Add(New NpgsqlParameter("SetSocket", NpgsqlTypes.NpgsqlDbType.Varchar))                    '情報コンセント・SW
                .Add(New NpgsqlParameter("SerialAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                  '製造番号（あいまい）
                .Add(New NpgsqlParameter("ImageNmbAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                'イメージ番号（あいまい）
                .Add(New NpgsqlParameter("ManageBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))           '管理部署（あいまい）
                .Add(New NpgsqlParameter("UsrIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                   'ユーザーID（あいまい）
                .Add(New NpgsqlParameter("SetBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))              '設置部署（あいまい）
                .Add(New NpgsqlParameter("SetRoomAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                 '設置番組/部屋（あいまい）
                .Add(New NpgsqlParameter("SetBuilAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                 '設置建物（あいまい）
                .Add(New NpgsqlParameter("SetFloorAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                '設置フロア（あいまい）
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                   '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                      '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                     '最終更新者ID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                        'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("MemorySize").Value = dataHBKB0601.PropTxtMemorySize.Text                       'メモリ容量
                .Parameters("Serial").Value = dataHBKB0601.PropTxtSerial.Text                               '製造番号（シリアル）
                .Parameters("MacAddress1").Value = dataHBKB0601.PropTxtMacAddress1.Text                     'MACアドレス1
                .Parameters("MacAddress2").Value = dataHBKB0601.PropTxtMacAddress2.Text                     'MACアドレス2
                .Parameters("Fuzokuhin").Value = dataHBKB0601.PropTxtFuzokuhin.Text                         '付属品
                .Parameters("TypeKbn").Value = dataHBKB0601.PropCmbType.SelectedValue                       'タイプ
                .Parameters("SCKikiFixNmb").Value = dataHBKB0601.PropTxtSCKikiFixNmb.Text                   'サポセン機器固定資産番号
                .Parameters("KikiState").Value = dataHBKB0601.PropTxtKikiState.Text                         '機器状態
                .Parameters("ImageNmb").Value = dataHBKB0601.PropTxtImageNmb.Text                           'イメージ番号
                .Parameters("LeaseUpDT").Value = dataHBKB0601.PropDtpLeaseUpDT_Kiki.txtDate.Text            'リース期限日（機器）
                If dataHBKB0601.PropChkSCHokanKbn.Checked = True Then                                       'サービスセンター保管機
                    .Parameters("SCHokanKbn").Value = SC_HOKANKBN_ON
                Else
                    .Parameters("SCHokanKbn").Value = SC_HOKANKBN_OFF
                End If
                .Parameters("LastInfoDT").Value = dataHBKB0601.PropDtpLastInfoDT.txtDate.Text               '最終お知らせ日
                .Parameters("ManageKyokuNM").Value = dataHBKB0601.PropTxtManageKyokuNM.Text                 '管理局
                .Parameters("ManageBusyoNM").Value = dataHBKB0601.PropTxtManageBusyoNM.Text                 '管理部署
                .Parameters("WorkFromNmb").Value = dataHBKB0601.PropTxtWorkFromNmb.Text                     '作業の元
                .Parameters("KikiUseCD").Value = dataHBKB0601.PropCmbKikiUse.SelectedValue                  '機器利用形態CD
                .Parameters("IPUseCD").Value = dataHBKB0601.PropCmbIPUse.SelectedValue                      'IP割当種類CD
                .Parameters("FixedIP").Value = dataHBKB0601.PropTxtFixedIP.Text                             '固定IP
                .Parameters("UsrID").Value = dataHBKB0601.PropTxtUsrID.Text                                 'ユーザーID
                .Parameters("UsrNM").Value = dataHBKB0601.PropTxtUsrNM.Text                                 'ユーザー氏名
                .Parameters("UsrCompany").Value = dataHBKB0601.PropTxtUsrCompany.Text                       'ユーザー所属会社
                .Parameters("UsrKyokuNM").Value = dataHBKB0601.PropTxtUsrKyokuNM.Text                       'ユーザー所属局
                .Parameters("UsrBusyoNM").Value = dataHBKB0601.PropTxtUsrBusyoNM.Text                       'ユーザー所属部署
                .Parameters("UsrTel").Value = dataHBKB0601.PropTxtUsrTel.Text                               'ユーザー電話番号
                .Parameters("UsrMailAdd").Value = dataHBKB0601.PropTxtUsrMailAdd.Text                       'ユーザーメールアドレス
                .Parameters("UsrContact").Value = dataHBKB0601.PropTxtUsrContact.Text                       'ユーザー連絡先
                .Parameters("UsrRoom").Value = dataHBKB0601.PropTxtUsrRoom.Text                             'ユーザー番組／部屋
                .Parameters("RentalStDT").Value = dataHBKB0601.PropDtpRentalStDT.txtDate.Text               'レンタル開始日
                .Parameters("RentalEdDT").Value = dataHBKB0601.PropDtpRentalEdDT.txtDate.Text               'レンタル期限日
                .Parameters("SetKyokuNM").Value = dataHBKB0601.PropTxtSetKyokuNM.Text                       '設置局
                .Parameters("SetBusyoNM").Value = dataHBKB0601.PropTxtSetBusyoNM.Text                       '設置部署
                .Parameters("SetRoom").Value = dataHBKB0601.PropTxtSetRoom.Text                             '設置番組／部屋
                .Parameters("SetBuil").Value = dataHBKB0601.PropTxtSetBuil.Text                             '設置建物
                .Parameters("SetFloor").Value = dataHBKB0601.PropTxtSetFloor.Text                           '設置フロア
                .Parameters("SetDeskNo").Value = dataHBKB0601.PropTxtSetDeskNo.Text                         '設置デスクNo
                .Parameters("SetLANLength").Value = dataHBKB0601.PropTxtSetLANLength.Text                   '設置LANケーブル長さ
                .Parameters("SetLANNum").Value = dataHBKB0601.PropTxtSetLANNum.Text                         '設置LANケーブル番号
                .Parameters("SetSocket").Value = dataHBKB0601.PropTxtSetSocket.Text                         '情報コンセント・SW
                .Parameters("SerialAimai").Value = _
                    commonLogicHBK.ChangeStringForSearch(.Parameters("Serial").Value)                       '製造番号（あいまい）
                .Parameters("ImageNmbAimai").Value = _
                    commonLogicHBK.ChangeStringForSearch(.Parameters("ImageNmb").Value)                     'イメージ番号（あいまい）
                .Parameters("ManageBusyoNMAimai").Value = _
                    commonLogicHBK.ChangeStringForSearch(.Parameters("ManageBusyoNM").Value)                '管理部署（あいまい）

                'Add--start 20121001 s.yamaguchi
                .Parameters("UsrIDAimai").Value = _
                    commonLogicHBK.ChangeStringForSearch(.Parameters("UsrID").Value)                        'ユーザーID（あいまい）
                'Add--END

                .Parameters("SetBusyoNMAimai").Value = _
                    commonLogicHBK.ChangeStringForSearch(.Parameters("SetBusyoNM").Value)                   '設置部署（あいまい）
                .Parameters("SetRoomAimai").Value = _
                   commonLogicHBK.ChangeStringForSearch(.Parameters("SetRoom").Value)                       '設置番組／部屋（あいまい）
                .Parameters("SetBuilAimai").Value = _
                    commonLogicHBK.ChangeStringForSearch(.Parameters("SetBuil").Value)                      '設置建物（あいまい）
                .Parameters("SetFloorAimai").Value = _
                    commonLogicHBK.ChangeStringForSearch(.Parameters("SetFloor").Value)                     '設置フロア（あいまい）
                .Parameters("UpdateDT").Value = dataHBKB0601.PropDtmSysDate                                 '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                              '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                                  '最終更新者ID
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb                                      'CI番号
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
    ''' 【編集モード】複数人利用物理削除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>複数人利用物理削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/02 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ登録先を保存用テーブル→標準テーブルへ変更</p>
    ''' </para></remarks>
    Public Function SetDeleteTmpShareSql(ByRef Cmd As NpgsqlCommand, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(DELETE)
            strSQL = strDeleteTmpShareSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb                                  'CI番号
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
    ''' 【編集モード】複数人利用新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>複数人利用新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/02 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ登録先を保存用テーブル→標準テーブルへ変更</p>
    ''' </para></remarks>
    Public Function SetInsertTmpShareSql(ByRef Cmd As NpgsqlCommand, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertTmpShareSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
                .Add(New NpgsqlParameter("UsrID", NpgsqlTypes.NpgsqlDbType.Varchar))                    'ユーザーID
                .Add(New NpgsqlParameter("UsrNM", NpgsqlTypes.NpgsqlDbType.Varchar))                    'ユーザー名
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                  '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                 '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                    '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb                                  'CI番号
                .Parameters("UsrID").Value = dataHBKB0601.PropRowReg.Item("UsrID")                      'ユーザーID
                .Parameters("UsrNM").Value = dataHBKB0601.PropRowReg.Item("UsrNM")                      'ユーザー名

                If dataHBKB0601.PropRowReg.Item("RegDT").ToString.Length > 0 Then
                    .Parameters("RegDT").Value = dataHBKB0601.PropRowReg.Item("RegDT")                                '登録日時
                    .Parameters("RegGrpCD").Value = dataHBKB0601.PropRowReg.Item("RegGrpCD")                                         '登録者グループCD
                    .Parameters("RegID").Value = dataHBKB0601.PropRowReg.Item("RegID")                                                 '登録者ID
                Else
                    .Parameters("RegDT").Value = dataHBKB0601.PropDtmSysDate                                '登録日時
                    .Parameters("RegGrpCD").Value = PropWorkGroupCD                                         '登録者グループCD
                    .Parameters("RegID").Value = PropUserId                                                 '登録者ID
                End If

                .Parameters("UpdateDT").Value = dataHBKB0601.PropDtmSysDate                             '最終更新日時
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
    ''' 【編集モード】オプションソフト物理削除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>オプションソフト物理削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/02 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ登録先を保存用テーブル→標準テーブルへ変更</p>
    ''' </para></remarks>
    Public Function SetDeleteTmpOptSoftSql(ByRef Cmd As NpgsqlCommand, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(DELETE)
            strSQL = strDeleteTmpOptSoftSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb                                  'CI番号
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
    ''' 【編集モード】オプションソフト新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>オプションソフト新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/02 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ登録先を保存用テーブル→標準テーブルへ変更</p>
    ''' </para></remarks>
    Public Function SetInsertTmpOptSoftSql(ByRef Cmd As NpgsqlCommand, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertTmpOptSoftSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
                '★★-----------------------
                '.Add(New NpgsqlParameter("SoftNM", NpgsqlTypes.NpgsqlDbType.Varchar))                   'ソフト名
                .Add(New NpgsqlParameter("SoftCD", NpgsqlTypes.NpgsqlDbType.Integer))                   'ソフトCD
                '★★-----------------------
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                  '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                 '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                    '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb                                  'CI番号
                '★★--------------
                '.Parameters("SoftNM").Value = dataHBKB0601.PropRowReg.Item("SoftNM")                    'ソフト名
                .Parameters("SoftCD").Value = Integer.Parse(dataHBKB0601.PropRowReg.Item("SoftCD"))     'ソフトCD
                '★★--------------
                If dataHBKB0601.PropRowReg.Item("RegDT").ToString.Length > 0 Then
                    .Parameters("RegDT").Value = dataHBKB0601.PropRowReg.Item("RegDT")                                '登録日時
                    .Parameters("RegGrpCD").Value = dataHBKB0601.PropRowReg.Item("RegGrpCD")                                         '登録者グループCD
                    .Parameters("RegID").Value = dataHBKB0601.PropRowReg.Item("RegID")                                                 '登録者ID
                Else
                    .Parameters("RegDT").Value = dataHBKB0601.PropDtmSysDate                                '登録日時
                    .Parameters("RegGrpCD").Value = PropWorkGroupCD                                         '登録者グループCD
                    .Parameters("RegID").Value = PropUserId                                                 '登録者ID
                End If
                .Parameters("UpdateDT").Value = dataHBKB0601.PropDtmSysDate                             '最終更新日時
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
    ''' 【編集モード】新規セット機器グループ番号取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規セット機器グループ番号取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/07 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ登録先を保存用テーブル→標準テーブルへ変更</p>
    ''' </para></remarks>
    Public Function SetSelectNewSetKikiGrpNoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectNewSetKikiGrpNoSql

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
    ''' 【編集モード】セット機器管理論理削除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>セット機器管理論理削除（UPDATE）用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/07 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ登録先を保存用テーブル→標準テーブルへ変更</p>
    ''' </para></remarks>
    Public Function SetPhygDeleteTmpSetKikiSql(ByRef Cmd As NpgsqlCommand, _
                                               ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(UPDATE)
            strSQL = strPhygDeleteTmpSetKikiSql


            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                           '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                              '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                             '最終更新者ID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                                'CI番号
                .Add(New NpgsqlParameter("SetKikiNo", NpgsqlTypes.NpgsqlDbType.Varchar))                            'セット機器番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("UpdateDT").Value = dataHBKB0601.PropDtmSysDate                                         '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                                      '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                                          '最終更新者ID
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb                                              'CI番号
                'セット機器番号　
                If dataHBKB0601.PropBlnCtlSelfSetKiki = True Then
                    '自セット機器操作フラグがONの場合は自画面のセット機器番号（種別＋番号）をセット
                    .Parameters("SetKikiNo").Value = _
                        dataHBKB0601.PropCmbKind.SelectedValue & dataHBKB0601.PropTxtNum.Text
                Else
                    .Parameters("SetKikiNo").Value = _
                        dataHBKB0601.PropRowReg("SetKikiNo_Org", DataRowVersion.Original)
                End If

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
    ''' 【編集モード】セット機器管理新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>セット機器管理新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/07 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ登録先を保存用テーブル→標準テーブルへ変更</p>
    ''' </para></remarks>
    Public Function SetInsertTmpSetKikiSql(ByRef Cmd As NpgsqlCommand, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertTmpSetKikiSql


            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
                .Add(New NpgsqlParameter("SetKikiGrpNo", NpgsqlTypes.NpgsqlDbType.Integer))             'セット機器グループ番号
                .Add(New NpgsqlParameter("SetKikiNo", NpgsqlTypes.NpgsqlDbType.Varchar))                'セット機器No
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                  '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                 '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                    '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb                                  'CI番号
                .Parameters("SetKikiGrpNo").Value = dataHBKB0601.PropIntSetKikiGrpNo                    'セット機器グループ番号
                .Parameters("SetKikiNo").Value = dataHBKB0601.PropRowReg.Item("SetKikiNo")              'セット機器No
                .Parameters("RegDT").Value = dataHBKB0601.PropDtmSysDate                                '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                         '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                 '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0601.PropDtmSysDate                             '最終更新日時
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
    ''' 【編集モード】新規CI（構成管理）履歴番号取得用SQLの作成・設定
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規CI履歴番号取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/25 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewCIRirekiNoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                              ByVal Cn As NpgsqlConnection, _
                                              ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectNewCIRirekiNoSql

            'データアダプタに、SQL文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb   'CI番号
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
    ''' 【編集モード】CI共通情報履歴新規登録用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/25 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCIInfoRirekiSql(ByRef Cmd As NpgsqlCommand, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(INSERT)
            strSQL = strInsertCIInfoRirekiSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))             '履歴番号
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))              '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("RirekiNo").Value = dataHBKB0601.PropIntRirekiNo                      '履歴番号：CI履歴番号
                .Parameters("RegDT").Value = dataHBKB0601.PropDtmSysDate                            '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                             '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0601.PropDtmSysDate                         '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb   'CI番号
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
    ''' 【編集モード】CIサポセン機器履歴新規登録用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIサポセン機器履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/25 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCISapRirekiSql(ByRef Cmd As NpgsqlCommand, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(INSERT)
            strSQL = strInsertCISapRirekiSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))             '履歴番号
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))              '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("RirekiNo").Value = dataHBKB0601.PropIntRirekiNo                      '履歴番号：CI履歴番号
                .Parameters("RegDT").Value = dataHBKB0601.PropDtmSysDate                            '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                             '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0601.PropDtmSysDate                         '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb   'CI番号
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
    ''' 【編集モード】オプションソフト履歴新規登録用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>オプションソフト履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/25 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertOptSoftRirekiSql(ByRef Cmd As NpgsqlCommand, _
                                              ByVal Cn As NpgsqlConnection, _
                                              ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(INSERT)
            strSQL = strInsertOptSoftRirekiSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))             '履歴番号
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))              '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("RirekiNo").Value = dataHBKB0601.PropIntRirekiNo                      '履歴番号：CI履歴番号
                .Parameters("RegDT").Value = dataHBKB0601.PropDtmSysDate                            '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                             '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0601.PropDtmSysDate                         '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb    'CI番号
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
    ''' 【編集モード】セット機器履歴新規登録用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>セット機器履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/25 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertSetKikiRirekiSql(ByRef Cmd As NpgsqlCommand, _
                                              ByVal Cn As NpgsqlConnection, _
                                              ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(INSERT)
            strSQL = strInsertSetKikiRirekiSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))             '履歴番号
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))              '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
                .Add(New NpgsqlParameter("SetKikiID", NpgsqlTypes.NpgsqlDbType.Integer))                'セット機器ID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("RirekiNo").Value = dataHBKB0601.PropIntRirekiNo                      '履歴番号：CI履歴番号
                .Parameters("RegDT").Value = dataHBKB0601.PropDtmSysDate                            '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                             '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0601.PropDtmSysDate                         '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
                .Parameters("SetKikiID").Value = dataHBKB0601.PropIntSetKikiID    'セット機器ID
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb    'CI番号
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
    ''' 【編集モード】複数人利用履歴新規登録用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>複数人利用履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/25 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertShareRirekiSql(ByRef Cmd As NpgsqlCommand, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(INSERT)
            strSQL = strInsertShareRirekiSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))             '履歴番号
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))              '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("RirekiNo").Value = dataHBKB0601.PropIntRirekiNo                      '履歴番号：CI履歴番号
                .Parameters("RegDT").Value = dataHBKB0601.PropDtmSysDate                            '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                             '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0601.PropDtmSysDate                         '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb   'CI番号
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
    ''' 【編集モード】登録理由履歴新規登録用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>登録理由履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/25 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertRegReasonWhenWorkAddedSql(ByRef Cmd As NpgsqlCommand, _
                                                       ByVal Cn As NpgsqlConnection, _
                                                       ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(INSERT)
            strSQL = strInsertRegReasonSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))             '履歴番号
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))              '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID

                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                'インシデント番号
                .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))             '作業番号
                .Add(New NpgsqlParameter("RowNmb", NpgsqlTypes.NpgsqlDbType.Timestamp))              '行番号

            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb   'CI番号
                .Parameters("RirekiNo").Value = dataHBKB0601.PropIntRirekiNo                      '履歴番号：CI履歴番号
                .Parameters("RegDT").Value = dataHBKB0601.PropDtmSysDate                            '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                             '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0601.PropDtmSysDate                         '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID

                .Parameters("IncNmb").Value = dataHBKB0601.PropIntIncNmb                        'インシデント番号
                .Parameters("WorkNmb").Value = dataHBKB0601.PropIntWorkNmb                      '作業番号
                .Parameters("RowNmb").Value = Nothing                             '行番号
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
    ''' 【編集モード】原因リンク履歴新規登録用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>原因リンク履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/25 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCauseLinkWhenWorkAddedSql(ByRef Cmd As NpgsqlCommand, _
                                                       ByVal Cn As NpgsqlConnection, _
                                                       ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            'SQL文(INSERT)
            strSQL = strInsertCauseLinkSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))             '履歴番号
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))              '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID

                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                'インシデント番号
                .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))             '作業番号
                .Add(New NpgsqlParameter("RowNmb", NpgsqlTypes.NpgsqlDbType.Timestamp))              '行番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb   'CI番号
                .Parameters("RirekiNo").Value = dataHBKB0601.PropIntRirekiNo                      '履歴番号：CI履歴番号
                .Parameters("RegDT").Value = dataHBKB0601.PropDtmSysDate                            '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                             '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0601.PropDtmSysDate                         '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID

                .Parameters("IncNmb").Value = dataHBKB0601.PropIntIncNmb                        'インシデント番号
                .Parameters("WorkNmb").Value = dataHBKB0601.PropIntWorkNmb                      '作業番号
                .Parameters("RowNmb").Value = Nothing                             '行番号
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

    '[add] 2014/06/09 e.okamura コピー不具合修正 Start
    ''' <summary>
    ''' 【編集モード】CI共通情報更新用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報更新用(コピー)のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2014/06/09 e.okamura
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetTmpCIInfoCopy(ByRef Cmd As NpgsqlCommand, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateTmpCIInfoCopySql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))    '登録日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))       '登録者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))      '登録者ID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))         'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("UpdateDT").Value = dataHBKB0601.PropDtmSysDate                  '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                               '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                   '最終更新者ID
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb                       'CI番号
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
    ''' 【編集モード】サポセン機器情報更新用SQL(コピー)の作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サポセン機器情報更新用(コピー)のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2014/06/09 e.okamura
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetTmpCISapCopy(ByRef Cmd As NpgsqlCommand, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateTmpCISapCopySql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))    '登録日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))       '登録者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))      '登録者ID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))         'CI番号  ※コピー元
                .Add(New NpgsqlParameter("CINmb2", NpgsqlTypes.NpgsqlDbType.Integer))        'CI番号2 ※コピー先
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("UpdateDT").Value = dataHBKB0601.PropDtmSysDate                  '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                               '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                   '最終更新者ID
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmbStc                    'CI番号  ※コピー元
                .Parameters("CINmb2").Value = dataHBKB0601.PropIntCINmb                      'CI番号2 ※コピー先
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
    '[add] 2014/06/09 e.okamura コピー不具合修正 End

    '[del] 2013/10/11 t.fukuo セット機器履歴が余計に登録される不具合修正：START
    ' ''' <summary>
    ' ''' 【編集モード】セット機器サポセン情報更新用SQLの作成・設定
    ' ''' </summary>
    ' ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ' ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ' ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ' ''' <param name="blnRegModeFlg">[IN]更新モードフラグ</param>
    ' ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ' ''' <remarks>更新フラグがTrue：セット機器サポセン情報更新用、False：インシデント内のサポセン機器情報更新用のSQLを作成し、アダプタにセットする
    ' ''' <para>作成情報：2012/09/25 y.ikushima
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Public Function SetUpdateSetKiki(ByRef Cmd As NpgsqlCommand, _
    '                                                   ByVal Cn As NpgsqlConnection, _
    '                                                   ByVal dataHBKB0601 As DataHBKB0601, _
    '                                                   ByVal blnRegModeFlg As Boolean) As Boolean

    '    '開始ログ出力
    '    CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数の宣言
    '    Dim strSQL As String = ""
    '    Dim strSQLCINmb As String = ""

    '    Try
    '        'フラグによって分岐させる
    '        If blnRegModeFlg = True Then
    '            strSQLCINmb = strSelectCINmbSetKiki
    '        Else
    '            strSQLCINmb = strSelectCINmbIncident
    '        End If

    '        'SQL文(INSERT)
    '        strSQL = String.Format(strUpdateSetKikiSql, strSQLCINmb)

    '        'データアダプタに、SQL文を設定
    '        Cmd = New NpgsqlCommand(strSQL, Cn)


    '        'バインド変数に型をセット
    '        With Cmd.Parameters
    '            .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
    '            .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))              '登録日時
    '            .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者グループCD
    '            .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                '登録者ID

    '            'フラグがインシデント内機器更新の場合、パラメータを追加
    '            If blnRegModeFlg = False Then
    '                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                'インシデント番号
    '                .Add(New NpgsqlParameter("WorkCD", NpgsqlTypes.NpgsqlDbType.Varchar))                '作業コード
    '            End If

    '        End With

    '        'バインド変数に値をセット
    '        With Cmd
    '            .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb   'CI番号
    '            .Parameters("UpdateDT").Value = dataHBKB0601.PropDtmSysDate                         '最終更新日時
    '            .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
    '            .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID

    '            'フラグがインシデント内機器更新の場合、パラメータを追加
    '            If blnRegModeFlg = False Then
    '                .Parameters("IncNmb").Value = dataHBKB0601.PropIntIncNmb                                           'インシデント番号
    '                .Parameters("WorkCD").Value = dataHBKB0601.PropStrWorkCD    '作業コード
    '            End If
    '        End With


    '        '終了ログ出力
    '        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
    '        '例外処理
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    End Try

    'End Function
    '[del] 2013/10/11 t.fukuo セット機器履歴が余計に登録される不具合修正：END

    '[add] 2013/10/11 t.fukuo セット機器履歴が余計に登録される不具合修正：START
    ''' <summary>
    ''' コピー対象機器CI番号取得用SQLの作成・設定
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <param name="blnCopyMode">[IN]コピーモード（True：セット機器にコピー、False：インシデントにコピー）</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>コピー対象機器のCI番号取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2013/10/11 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCopyCINmbsSql(ByRef Adapter As NpgsqlDataAdapter, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB0601 As DataHBKB0601, _
                                           ByVal blnCopyMode As Boolean) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            'コピーモードによりSQL文(SELECT)セット
            If blnCopyMode = True Then
                strSQL = strSelectCINmbSetKiki
            Else
                strSQL = strSelectCINmbIncident
            End If

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer)).Value = dataHBKB0601.PropIntCINmb      'CI番号
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer)).Value = dataHBKB0601.PropIntIncNmb    'インシデント番号
                .Add(New NpgsqlParameter("WorkCD_Set", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = WORK_CD_SET               '作業CD：設置
                .Add(New NpgsqlParameter("WorkCD_AddConfig", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = WORK_CD_ADDCONFIG   '作業CD：追加設定
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
    '[add] 2013/10/11 t.fukuo セット機器履歴が余計に登録される不具合修正：END


    '[del] 2013/10/11 t.fukuo セット機器履歴が余計に登録される不具合修正：START
    ' ''' <summary>
    ' ''' 【編集モード】CI共通情報履歴新規登録用SQLの作成・設定
    ' ''' </summary>
    ' ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ' ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ' ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ' ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ' ''' <remarks>CI共通情報履歴新規登録用のSQLを作成し、アダプタにセットする
    ' ''' <para>作成情報：2012/09/25 y.ikushima
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Public Function SetInsertCIInfoRirekiSqlCopy(ByRef Cmd As NpgsqlCommand, _
    '                                         ByVal Cn As NpgsqlConnection, _
    '                                        ByVal dataHBKB0601 As DataHBKB0601, _
    '                                        ByVal blnRegModeFlg As Boolean) As Boolean

    '    '開始ログ出力
    '    CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数の宣言
    '    Dim strSQL As String = ""
    '    Dim strSQLCINmb As String = ""

    '    Try
    '        'フラグによって分岐させる
    '        If blnRegModeFlg = True Then
    '            strSQLCINmb = strSelectCINmbSetKiki
    '        Else
    '            strSQLCINmb = strSelectCINmbIncident
    '        End If

    '        'SQL文(INSERT)
    '        strSQL = String.Format(strInsertCIInfoRirekiSetKikiSql, strSQLCINmb)

    '        'データアダプタに、SQL文を設定
    '        Cmd = New NpgsqlCommand(strSQL, Cn)


    '        'バインド変数に型をセット
    '        With Cmd.Parameters
    '            .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))              '登録日時
    '            .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者グループCD
    '            .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                '登録者ID
    '            .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
    '            .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
    '            .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
    '            .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号

    '            'フラグがインシデント内機器更新の場合、パラメータを追加
    '            If blnRegModeFlg = False Then
    '                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                'インシデント番号
    '                .Add(New NpgsqlParameter("WorkCD", NpgsqlTypes.NpgsqlDbType.Varchar))                '作業コード
    '            End If

    '        End With

    '        'バインド変数に値をセット
    '        With Cmd
    '            .Parameters("RegDT").Value = dataHBKB0601.PropDtmSysDate                            '登録日時
    '            .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
    '            .Parameters("RegID").Value = PropUserId                                             '登録者ID
    '            .Parameters("UpdateDT").Value = dataHBKB0601.PropDtmSysDate                         '最終更新日時
    '            .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
    '            .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
    '            .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb   'CI番号

    '            'フラグがインシデント内機器更新の場合、パラメータを追加
    '            If blnRegModeFlg = False Then
    '                .Parameters("IncNmb").Value = dataHBKB0601.PropIntIncNmb                                           'インシデント番号
    '                .Parameters("WorkCD").Value = dataHBKB0601.PropStrWorkCD    '作業コード
    '            End If
    '        End With


    '        '終了ログ出力
    '        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
    '        '例外処理
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    End Try

    'End Function

    ' ''' <summary>
    ' ''' 【編集モード】CIサポセン機器履歴新規登録用SQLの作成・設定
    ' ''' </summary>
    ' ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ' ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ' ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ' ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ' ''' <remarks>CIサポセン機器履歴新規登録用のSQLを作成し、アダプタにセットする
    ' ''' <para>作成情報：2012/09/25 y.ikushima
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Public Function SetInsertCISapRirekiSqlCopy(ByRef Cmd As NpgsqlCommand, _
    '                                        ByVal Cn As NpgsqlConnection, _
    '                                        ByVal dataHBKB0601 As DataHBKB0601, _
    '                                        ByVal blnRegModeFlg As Boolean) As Boolean

    '    '開始ログ出力
    '    CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数の宣言
    '    Dim strSQL As String = ""
    '    Dim strSQLCINmb As String = ""

    '    Try
    '        'フラグによって分岐させる
    '        If blnRegModeFlg = True Then
    '            strSQLCINmb = strSelectCINmbSetKiki
    '        Else
    '            strSQLCINmb = strSelectCINmbIncident
    '        End If

    '        'SQL文(INSERT)
    '        strSQL = String.Format(strInsertCISapRirekiSetKikiSql, strSQLCINmb)

    '        'データアダプタに、SQL文を設定
    '        Cmd = New NpgsqlCommand(strSQL, Cn)


    '        'バインド変数に型をセット
    '        With Cmd.Parameters
    '            .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))              '登録日時
    '            .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者グループCD
    '            .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                '登録者ID
    '            .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
    '            .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
    '            .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
    '            .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号

    '            'フラグがインシデント内機器更新の場合、パラメータを追加
    '            If blnRegModeFlg = False Then
    '                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                'インシデント番号
    '                .Add(New NpgsqlParameter("WorkCD", NpgsqlTypes.NpgsqlDbType.Varchar))                '作業コード
    '            End If
    '        End With

    '        'バインド変数に値をセット
    '        With Cmd
    '            .Parameters("RegDT").Value = dataHBKB0601.PropDtmSysDate                            '登録日時
    '            .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
    '            .Parameters("RegID").Value = PropUserId                                             '登録者ID
    '            .Parameters("UpdateDT").Value = dataHBKB0601.PropDtmSysDate                         '最終更新日時
    '            .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
    '            .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
    '            .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb   'CI番号

    '            'フラグがインシデント内機器更新の場合、パラメータを追加
    '            If blnRegModeFlg = False Then
    '                .Parameters("IncNmb").Value = dataHBKB0601.PropIntIncNmb                                           'インシデント番号
    '                .Parameters("WorkCD").Value = dataHBKB0601.PropStrWorkCD    '作業コード
    '            End If
    '        End With


    '        '終了ログ出力
    '        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
    '        '例外処理
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    End Try

    'End Function

    ' ''' <summary>
    ' ''' 【編集モード】オプションソフト履歴新規登録用SQLの作成・設定
    ' ''' </summary>
    ' ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ' ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ' ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ' ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ' ''' <remarks>オプションソフト履歴新規登録用のSQLを作成し、アダプタにセットする
    ' ''' <para>作成情報：2012/09/25 y.ikushima
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Public Function SetInsertOptSoftRirekiSqlCopy(ByRef Cmd As NpgsqlCommand, _
    '                                          ByVal Cn As NpgsqlConnection, _
    '                                        ByVal dataHBKB0601 As DataHBKB0601, _
    '                                        ByVal blnRegModeFlg As Boolean) As Boolean

    '    '開始ログ出力
    '    CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数の宣言
    '    Dim strSQL As String = ""
    '    Dim strSQLCINmb As String = ""

    '    Try
    '        'フラグによって分岐させる
    '        If blnRegModeFlg = True Then
    '            strSQLCINmb = strSelectCINmbSetKiki
    '        Else
    '            strSQLCINmb = strSelectCINmbIncident
    '        End If

    '        'SQL文(INSERT)
    '        strSQL = String.Format(strInsertOptSoftRirekiSetKikiSql, strSQLCINmb)

    '        'データアダプタに、SQL文を設定
    '        Cmd = New NpgsqlCommand(strSQL, Cn)


    '        'バインド変数に型をセット
    '        With Cmd.Parameters
    '            .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))              '登録日時
    '            .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者グループCD
    '            .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                '登録者ID
    '            .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
    '            .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
    '            .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
    '            .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号

    '            'フラグがインシデント内機器更新の場合、パラメータを追加
    '            If blnRegModeFlg = False Then
    '                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                'インシデント番号
    '                .Add(New NpgsqlParameter("WorkCD", NpgsqlTypes.NpgsqlDbType.Varchar))                '作業コード
    '            End If

    '        End With

    '        'バインド変数に値をセット
    '        With Cmd
    '            .Parameters("RegDT").Value = dataHBKB0601.PropDtmSysDate                            '登録日時
    '            .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
    '            .Parameters("RegID").Value = PropUserId                                             '登録者ID
    '            .Parameters("UpdateDT").Value = dataHBKB0601.PropDtmSysDate                         '最終更新日時
    '            .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
    '            .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
    '            .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb    'CI番号

    '            'フラグがインシデント内機器更新の場合、パラメータを追加
    '            If blnRegModeFlg = False Then
    '                .Parameters("IncNmb").Value = dataHBKB0601.PropIntIncNmb                                           'インシデント番号
    '                .Parameters("WorkCD").Value = dataHBKB0601.PropStrWorkCD    '作業コード
    '            End If
    '        End With


    '        '終了ログ出力
    '        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
    '        '例外処理
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    End Try

    'End Function

    ' ''' <summary>
    ' ''' 【編集モード】セット機器履歴新規登録用SQLの作成・設定
    ' ''' </summary>
    ' ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ' ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ' ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ' ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ' ''' <remarks>セット機器履歴新規登録用のSQLを作成し、アダプタにセットする
    ' ''' <para>作成情報：2012/09/25 y.ikushima
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Public Function SetInsertSetKikiRirekiSqlCopy(ByRef Cmd As NpgsqlCommand, _
    '                                          ByVal Cn As NpgsqlConnection, _
    '                                        ByVal dataHBKB0601 As DataHBKB0601, _
    '                                        ByVal blnRegModeFlg As Boolean) As Boolean

    '    '開始ログ出力
    '    CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数の宣言
    '    Dim strSQL As String = ""
    '    Dim strSQLCINmb As String = ""

    '    Try
    '        'フラグによって分岐させる
    '        If blnRegModeFlg = True Then
    '            strSQLCINmb = strSelectCINmbSetKiki
    '        Else
    '            strSQLCINmb = strSelectCINmbIncident
    '        End If

    '        'SQL文(INSERT)
    '        strSQL = String.Format(strInsertSetKikiRirekiSetKikiSql, strSQLCINmb)

    '        'データアダプタに、SQL文を設定
    '        Cmd = New NpgsqlCommand(strSQL, Cn)


    '        'バインド変数に型をセット
    '        With Cmd.Parameters
    '            .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))              '登録日時
    '            .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者グループCD
    '            .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                '登録者ID
    '            .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
    '            .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
    '            .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
    '            .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号

    '            'フラグがインシデント内機器更新の場合、パラメータを追加
    '            If blnRegModeFlg = False Then
    '                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                'インシデント番号
    '                .Add(New NpgsqlParameter("WorkCD", NpgsqlTypes.NpgsqlDbType.Varchar))                '作業コード
    '            End If

    '        End With

    '        'バインド変数に値をセット
    '        With Cmd
    '            .Parameters("RegDT").Value = dataHBKB0601.PropDtmSysDate                            '登録日時
    '            .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
    '            .Parameters("RegID").Value = PropUserId                                             '登録者ID
    '            .Parameters("UpdateDT").Value = dataHBKB0601.PropDtmSysDate                         '最終更新日時
    '            .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
    '            .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
    '            .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb    'CI番号

    '            'フラグがインシデント内機器更新の場合、パラメータを追加
    '            If blnRegModeFlg = False Then
    '                .Parameters("IncNmb").Value = dataHBKB0601.PropIntIncNmb                                           'インシデント番号
    '                .Parameters("WorkCD").Value = dataHBKB0601.PropStrWorkCD    '作業コード
    '            End If

    '        End With


    '        '終了ログ出力
    '        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
    '        '例外処理
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    End Try

    'End Function

    ' ''' <summary>
    ' ''' 【編集モード】複数人利用履歴新規登録用SQLの作成・設定
    ' ''' </summary>
    ' ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ' ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ' ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ' ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ' ''' <remarks>複数人利用履歴新規登録用のSQLを作成し、アダプタにセットする
    ' ''' <para>作成情報：2012/09/25 y.ikushima
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Public Function SetInsertShareRirekiSqlCopy(ByRef Cmd As NpgsqlCommand, _
    '                                        ByVal Cn As NpgsqlConnection, _
    '                                        ByVal dataHBKB0601 As DataHBKB0601, _
    '                                        ByVal blnRegModeFlg As Boolean) As Boolean

    '    '開始ログ出力
    '    CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数の宣言
    '    Dim strSQL As String = ""
    '    Dim strSQLCINmb As String = ""

    '    Try
    '        'フラグによって分岐させる
    '        If blnRegModeFlg = True Then
    '            strSQLCINmb = strSelectCINmbSetKiki
    '        Else
    '            strSQLCINmb = strSelectCINmbIncident
    '        End If

    '        'SQL文(INSERT)
    '        strSQL = String.Format(strInsertShareRirekiSetKikiSql, strSQLCINmb)

    '        'データアダプタに、SQL文を設定
    '        Cmd = New NpgsqlCommand(strSQL, Cn)


    '        'バインド変数に型をセット
    '        With Cmd.Parameters
    '            .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))              '登録日時
    '            .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者グループCD
    '            .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                '登録者ID
    '            .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
    '            .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
    '            .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
    '            .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号

    '            'フラグがインシデント内機器更新の場合、パラメータを追加
    '            If blnRegModeFlg = False Then
    '                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                'インシデント番号
    '                .Add(New NpgsqlParameter("WorkCD", NpgsqlTypes.NpgsqlDbType.Varchar))                '作業コード
    '            End If

    '        End With

    '        'バインド変数に値をセット
    '        With Cmd
    '            .Parameters("RegDT").Value = dataHBKB0601.PropDtmSysDate                            '登録日時
    '            .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
    '            .Parameters("RegID").Value = PropUserId                                             '登録者ID
    '            .Parameters("UpdateDT").Value = dataHBKB0601.PropDtmSysDate                         '最終更新日時
    '            .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
    '            .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
    '            .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb   'CI番号

    '            'フラグがインシデント内機器更新の場合、パラメータを追加
    '            If blnRegModeFlg = False Then
    '                .Parameters("IncNmb").Value = dataHBKB0601.PropIntIncNmb                                           'インシデント番号
    '                .Parameters("WorkCD").Value = dataHBKB0601.PropStrWorkCD    '作業コード
    '            End If

    '        End With


    '        '終了ログ出力
    '        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
    '        '例外処理
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    End Try

    'End Function

    ' ''' <summary>
    ' ''' 【編集モード】登録理由履歴新規登録用SQLの作成・設定
    ' ''' </summary>
    ' ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ' ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ' ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ' ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ' ''' <remarks>登録理由履歴新規登録用のSQLを作成し、アダプタにセットする
    ' ''' <para>作成情報：2012/09/25 y.ikushima
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Public Function SetInsertRegReasonWhenWorkAddedSqlCopy(ByRef Cmd As NpgsqlCommand, _
    '                                                   ByVal Cn As NpgsqlConnection, _
    '                                                   ByVal dataHBKB0601 As DataHBKB0601, _
    '                                                   ByVal blnRegModeFlg As Boolean) As Boolean

    '    '開始ログ出力
    '    CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数の宣言
    '    Dim strSQL As String = ""
    '    Dim strSQLCINmb As String = ""

    '    Try
    '        'フラグによって分岐させる
    '        If blnRegModeFlg = True Then
    '            strSQLCINmb = strSelectCINmbSetKikiReason
    '        Else
    '            strSQLCINmb = strSelectCINmbIncidentReason
    '        End If

    '        'SQL文(INSERT)
    '        strSQL = String.Format(strInsertRegReasonSetKikiSql, strSQLCINmb)

    '        'データアダプタに、SQL文を設定
    '        Cmd = New NpgsqlCommand(strSQL, Cn)


    '        'バインド変数に型をセット
    '        With Cmd.Parameters
    '            .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
    '            .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))             '履歴番号
    '            .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))              '登録日時
    '            .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者グループCD
    '            .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                '登録者ID
    '            .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
    '            .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
    '            .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID

    '            .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                'インシデント番号
    '            .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))             '作業番号
    '            .Add(New NpgsqlParameter("WorkCD", NpgsqlTypes.NpgsqlDbType.Varchar))                '作業コード

    '        End With

    '        'バインド変数に値をセット
    '        With Cmd
    '            .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb   'CI番号
    '            .Parameters("RirekiNo").Value = dataHBKB0601.PropIntRirekiNo                      '履歴番号：CI履歴番号
    '            .Parameters("RegDT").Value = dataHBKB0601.PropDtmSysDate                            '登録日時
    '            .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
    '            .Parameters("RegID").Value = PropUserId                                             '登録者ID
    '            .Parameters("UpdateDT").Value = dataHBKB0601.PropDtmSysDate                         '最終更新日時
    '            .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
    '            .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID

    '            .Parameters("IncNmb").Value = dataHBKB0601.PropIntIncNmb                        'インシデント番号
    '            .Parameters("WorkCD").Value = dataHBKB0601.PropStrWorkCD    '作業コード
    '        End With


    '        '終了ログ出力
    '        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
    '        '例外処理
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    End Try

    'End Function

    ' ''' <summary>
    ' ''' 【編集モード】原因リンク履歴新規登録用SQLの作成・設定
    ' ''' </summary>
    ' ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ' ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ' ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ' ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ' ''' <remarks>原因リンク履歴新規登録用のSQLを作成し、アダプタにセットする
    ' ''' <para>作成情報：2012/09/25 y.ikushima
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Public Function SetInsertCauseLinkWhenWorkAddedSqlCopy(ByRef Cmd As NpgsqlCommand, _
    '                                                   ByVal Cn As NpgsqlConnection, _
    '                                                   ByVal dataHBKB0601 As DataHBKB0601, _
    '                                                   ByVal blnRegModeFlg As Boolean) As Boolean

    '    '開始ログ出力
    '    CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数の宣言
    '    Dim strSQL As String = ""
    '    Dim strSQLCINmb As String = ""

    '    Try
    '        'フラグによって分岐させる
    '        If blnRegModeFlg = True Then
    '            strSQLCINmb = strSelectCINmbSetKikiReason
    '        Else
    '            strSQLCINmb = strSelectCINmbIncidentReason
    '        End If

    '        'SQL文(INSERT)
    '        strSQL = String.Format(strInsertCauseLinkSetKikiSql, strSQLCINmb)

    '        'データアダプタに、SQL文を設定
    '        Cmd = New NpgsqlCommand(strSQL, Cn)


    '        'バインド変数に型をセット
    '        With Cmd.Parameters
    '            .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
    '            .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))             '履歴番号
    '            .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))              '登録日時
    '            .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者グループCD
    '            .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                '登録者ID
    '            .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
    '            .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
    '            .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID

    '            .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                'インシデント番号
    '            .Add(New NpgsqlParameter("WorkCD", NpgsqlTypes.NpgsqlDbType.Varchar))                '作業コード
    '        End With

    '        'バインド変数に値をセット
    '        With Cmd
    '            .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb   'CI番号
    '            .Parameters("RirekiNo").Value = dataHBKB0601.PropIntRirekiNo                      '履歴番号：CI履歴番号
    '            .Parameters("RegDT").Value = dataHBKB0601.PropDtmSysDate                            '登録日時
    '            .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
    '            .Parameters("RegID").Value = PropUserId                                             '登録者ID
    '            .Parameters("UpdateDT").Value = dataHBKB0601.PropDtmSysDate                         '最終更新日時
    '            .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
    '            .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID

    '            .Parameters("IncNmb").Value = dataHBKB0601.PropIntIncNmb                        'インシデント番号
    '            .Parameters("WorkCD").Value = dataHBKB0601.PropStrWorkCD    '作業コード
    '        End With


    '        '終了ログ出力
    '        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
    '        '例外処理
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    End Try

    'End Function

    ' ''' <summary>
    ' ''' 【編集モード】セット機器CI番号取得用SQLの作成・設定
    ' ''' </summary>
    ' ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ' ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ' ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ' ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ' ''' <remarks>セット機器CI番号取得用のSQLを作成し、アダプタにセットする
    ' ''' <para>作成情報：2012/09/26 y.ikushima
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Public Function SetSelectCINmbSetKiki(ByRef Adapter As NpgsqlDataAdapter, _
    '                                          ByVal Cn As NpgsqlConnection, _
    '                                          ByVal dataHBKB0601 As DataHBKB0601) As Boolean

    '    '開始ログ出力
    '    CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数の宣言
    '    Dim strSQL As String = ""

    '    Try

    '        'SQL文(SELECT)
    '        strSQL = strSelectCINmbSetKiki

    '        'データアダプタに、SQL文を設定
    '        Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


    '        'バインド変数に型をセット
    '        With Adapter.SelectCommand.Parameters
    '            .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
    '        End With

    '        'バインド変数に値をセット
    '        With Adapter.SelectCommand
    '            .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb   'CI番号
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
    '[del] 2013/10/11 t.fukuo セット機器履歴が余計に登録される不具合修正：END

    ''' <summary>
    ''' 【共通】新規ログNo取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規ログNo取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/26 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewRirekiNoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectNewRirekiNoSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("incNmb", NpgsqlTypes.NpgsqlDbType.Integer))   'INC番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("incNmb").Value = dataHBKB0601.PropIntIncNmb                'INC番号
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

    '[del] 2013/10/11 t.fukuo セット機器履歴が余計に登録される不具合修正：START
    ' ''' <summary>
    ' ''' 【編集モード】インシデント内のセット機器CI番号取得用SQLの作成・設定
    ' ''' </summary>
    ' ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ' ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ' ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ' ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ' ''' <remarks>インシデント内のセット機器CI番号取得用のSQLを作成し、アダプタにセットする
    ' ''' <para>作成情報：2012/09/26 y.ikushima
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Public Function SetSelectCINmbIncident(ByRef Adapter As NpgsqlDataAdapter, _
    '                                          ByVal Cn As NpgsqlConnection, _
    '                                          ByVal dataHBKB0601 As DataHBKB0601) As Boolean

    '    '開始ログ出力
    '    CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数の宣言
    '    Dim strSQL As String = ""

    '    Try

    '        'SQL文(SELECT)
    '        strSQL = strSelectCINmbIncident

    '        'データアダプタに、SQL文を設定
    '        Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


    '        'バインド変数に型をセット
    '        With Adapter.SelectCommand.Parameters
    '            .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
    '            .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                'インシデント番号
    '            .Add(New NpgsqlParameter("WorkCD", NpgsqlTypes.NpgsqlDbType.Varchar))                '作業コード
    '        End With

    '        'バインド変数に値をセット
    '        With Adapter.SelectCommand
    '            .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb   'CI番号
    '            .Parameters("IncNmb").Value = dataHBKB0601.PropIntIncNmb                        'インシデント番号
    '            .Parameters("WorkCD").Value = dataHBKB0601.PropStrWorkCD    '作業コード
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
    '[del] 2013/10/11 t.fukuo セット機器履歴が余計に登録される不具合修正：END


    ''' <summary>
    ''' 【編集モード】CIサポセン機器メンテナンス機器更新用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIサポセン機器メンテナンス機器更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/28 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateSapMainteKikiSql(ByRef Cmd As NpgsqlCommand, _
                                              ByVal Cn As NpgsqlConnection, _
                                              ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateSapMainteKikiSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LastUpRirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))           '最終更新履歴No
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'インシデント番号
                .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))                  '作業番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    '行番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LastUpRirekiNo").Value = dataHBKB0601.PropIntRirekiNo                     '最終更新履歴No
                .Parameters("UpdateDT").Value = dataHBKB0601.PropDtmSysDate                             '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
                .Parameters("IncNmb").Value = dataHBKB0601.PropIntIncNmb                        'インシデント番号
                .Parameters("WorkNmb").Value = dataHBKB0601.PropIntWorkNmb    '作業番号
                .Parameters("CINmb").Value = dataHBKB0601.PropIntCINmb         'CI番号
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
    ''' 【共通】共通情報ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>共通情報ログ新規登録用のdataHBKC0201SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/28 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncInfoLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertIncInfoLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))       'INC番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKB0601.PropIntLogNo                       'ログNo
                .Parameters("IncNmb").Value = dataHBKB0601.PropIntIncNmb                     'INC番号
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
    ''' 【共通】作業履歴ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>作業履歴ログ新規登録用のdataHBKC0201SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/28 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncRirekiLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertIncRirekiLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))       'INC番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKB0601.PropIntLogNo                  'ログNo
                .Parameters("IncNmb").Value = dataHBKB0601.PropIntIncNmb                'INC番号
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
    ''' 【共通】作業担当ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>作業担当ログ新規登録用のdataHBKC0201SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/28 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncTantoLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertIncTantoLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))       'INC番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKB0601.PropIntLogNo                  'ログNo
                .Parameters("IncNmb").Value = dataHBKB0601.PropIntIncNmb                'INC番号
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
    ''' 【共通】機器情報ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>機器情報ログ新規登録用のdataHBKC0201SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/28 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncKikiLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertInckikiLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))       'INC番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKB0601.PropIntLogNo                  'ログNo
                .Parameters("IncNmb").Value = dataHBKB0601.PropIntIncNmb                'INC番号
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
    ''' 【共通】対応関係情報ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>対応関係情報ログ新規登録用のdataHBKC0201SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/28 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncKankeiLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertIncKankeiLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))       'INC番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKB0601.PropIntLogNo                  'ログNo
                .Parameters("IncNmb").Value = dataHBKB0601.PropIntIncNmb                'INC番号
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
    ''' 【共通】プロセスリンク情報ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>プロセスリンク情報ログ新規登録用のdataHBKC0201SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/28 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertPLinkmotoLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertPLinkMotoLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))       'INC番号
                .Add(New NpgsqlParameter("pkbn", NpgsqlTypes.NpgsqlDbType.Varchar))         'プロセス区分
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKB0601.PropIntLogNo                  'ログNo
                .Parameters("IncNmb").Value = dataHBKB0601.PropIntIncNmb                'INC番号
                .Parameters("pkbn").Value = PROCESS_TYPE_INCIDENT                       'プロセス区分
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
    ''' 【共通】関連ファイル情報ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>関連ファイル情報ログ新規登録用のdataHBKC0201SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/28 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncFileLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertIncFileLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))       'INC番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKB0601.PropIntLogNo                  'ログNo
                .Parameters("IncNmb").Value = dataHBKB0601.PropIntIncNmb                'INC番号
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
    ''' 【共通】サポセン機器メンテナンス作業ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サポセン機器メンテナンス作業ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/28 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertSapMainteWorkLSql(ByRef Cmd As NpgsqlCommand, _
                                               ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertSapMainteWorkLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))      '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))     '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))        '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))   '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))      '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))     '最終更新者ID
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))       'INC番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKB0601.PropIntLogNo                      'ログNo
                .Parameters("RegDT").Value = dataHBKB0601.PropDtmSysDate                    '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                             '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                     '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0601.PropDtmSysDate                 '更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                              '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                  '最終更新者ID
                .Parameters("IncNmb").Value = dataHBKB0601.PropIntIncNmb                    'INC番号
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
    ''' 【共通】サポセン機器メンテナンス機器ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サポセン機器メンテナンス機器ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/28 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertSapMainteKikiLSql(ByRef Cmd As NpgsqlCommand, _
                                               ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertSapMainteKikiLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))      '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))     '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))        '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))   '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))      '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))     '最終更新者ID
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))       'INC番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKB0601.PropIntLogNo                      'ログNo
                .Parameters("RegDT").Value = dataHBKB0601.PropDtmSysDate                    '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                             '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                     '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0601.PropDtmSysDate                 '更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                              '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                  '最終更新者ID
                .Parameters("IncNmb").Value = dataHBKB0601.PropIntIncNmb                    'INC番号
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
