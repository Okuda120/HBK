Imports Npgsql
Imports Common
Imports System.Text
Imports System.Text.RegularExpressions
Imports CommonHBK

''' <summary>
''' 共通検索一覧(出力)画面Sqlクラス
''' </summary>
''' <remarks>共通検索一覧(出力)画面のSQLの作成・設定を行う
''' <para>作成情報：2012/06/14 kuga
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKB0102

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK

    '定数宣言
    Private Const FORMAT_NUM As Integer = 5 '番号フォーマット（00000）


    '【共通】EXCEL出力用データ取得SQL：SELECT句
    Private strSelectSql As String = _
           " SELECT" & vbCrLf & _
           "   A.CINmb" & vbCrLf & _
           "  ,B.CIKbnNM" & vbCrLf & _
           "  ,C.KindNM" & vbCrLf & _
           "  ,A.Num " & vbCrLf & _
           "  ,D.CIStateNM" & vbCrLf & _
           "  ,A.Class1" & vbCrLf & _
           "  ,A.Class2" & vbCrLf & _
           "  ,A.CINM" & vbCrLf & _
           "  ,E.GroupNM AS CIOwnerNM" & vbCrLf & _
           "  ,A.Sort AS CISort" & vbCrLf & _
           "  ,A.CINaiyo" & vbCrLf & _
           "  ,A.Biko1" & vbCrLf & _
           "  ,A.Biko2" & vbCrLf & _
           "  ,A.Biko3" & vbCrLf & _
           "  ,A.Biko4" & vbCrLf & _
           "  ,A.Biko5" & vbCrLf & _
           "  ,CASE WHEN A.FreeFlg1='" & FREE_FLG_ON & "' THEN '" & FREE_FLG_ON_NM & "' ELSE '" & FREE_FLG_OFF_NM & "' END AS FreeFlg1" & vbCrLf & _
           "  ,CASE WHEN A.FreeFlg2='" & FREE_FLG_ON & "' THEN '" & FREE_FLG_ON_NM & "' ELSE '" & FREE_FLG_OFF_NM & "' END AS FreeFlg2" & vbCrLf & _
           "  ,CASE WHEN A.FreeFlg3='" & FREE_FLG_ON & "' THEN '" & FREE_FLG_ON_NM & "' ELSE '" & FREE_FLG_OFF_NM & "' END AS FreeFlg3" & vbCrLf & _
           "  ,CASE WHEN A.FreeFlg4='" & FREE_FLG_ON & "' THEN '" & FREE_FLG_ON_NM & "' ELSE '" & FREE_FLG_OFF_NM & "' END AS FreeFlg4" & vbCrLf & _
           "  ,CASE WHEN A.FreeFlg5='" & FREE_FLG_ON & "' THEN '" & FREE_FLG_ON_NM & "' ELSE '" & FREE_FLG_OFF_NM & "' END AS FreeFlg5" & vbCrLf & _
           "  ,TO_CHAR(A.RegDT,'FMYYYY/FMMM/FMDD FMHH24:FMMI') AS RegDT" & vbCrLf & _
           "  ,F.GroupNM AS RegGrpNM" & vbCrLf & _
           "  ,A.RegID" & vbCrLf & _
           "  ,G.HBKUsrNM AS RegNM" & vbCrLf & _
           "  ,TO_CHAR(A.UpdateDT,'FMYYYY/FMMM/FMDD FMHH24:FMMI') AS UpdateDT" & vbCrLf & _
           "  ,H.GroupNM AS UpGrpNM" & vbCrLf & _
           "  ,A.UpdateID" & vbCrLf & _
           "  ,I.HBKUsrNM AS UpdateNM" & vbCrLf

    '【システム】EXCEL出力用データ取得SQL：SELECT句
    'Private strSelectSqlForSys As String = _
    '       "  ,J.InfShareteamNm" & vbCrLf & _
    '       "  ,K.Url" & vbCrLf & _
    '       "  ,L.SrvMng" & vbCrLf & _
    '       "  ,M.Relation" & vbCrLf
    Private strSelectSqlForSys As String = _
       "  , J.InfShareteamNm" & vbCrLf & _
       "  , ( SELECT STRING_AGG(T.Url || '　' || T.UrlNaiyo,'／' ORDER BY T.RowNmb) AS Url " & vbCrLf & _
       "        FROM KNOWHOWURL_TB T WHERE T.CINmb = A.CINmb GROUP BY T.CINmb ) AS Url " & vbCrLf & _
       "  , ( SELECT STRING_AGG(T.ManageNmb || '　' || T.ManageNmbNaiyo,'／' ORDER BY T.RowNmb) AS SrvMng " & vbCrLf & _
       "        FROM SRVMNG_TB T WHERE T.CINmb = A.CINmb GROUP BY T.CINmb ) AS SrvMng " & vbCrLf & _
       "  , ( SELECT STRING_AGG(T.RelationKbn || '　' || T.RelationID || '　' || " & vbCrLf & _
       "            CASE T.RelationKbn WHEN 'G' THEN (SELECT T2.GroupNM FROM GRP_MTB T2 WHERE T.RelationID = T2.GroupCD) " & vbCrLf & _
       "            ELSE (SELECT T2.HBKUsrNM FROM HBKUSR_MTB T2 WHERE T.RelationID = T2.HBKUsrID) " & vbCrLf & _
       "            END " & vbCrLf & _
       "            ,'／' ORDER BY " & vbCrLf & _
       "            CASE T.RelationKbn WHEN 'G' THEN (SELECT T2.Sort FROM GRP_MTB T2 WHERE T.RelationID = T2.GroupCD) " & vbCrLf & _
       "            ELSE (SELECT T2.Sort FROM HBKUSR_MTB T2 WHERE T.RelationID = T2.HBKUsrID) " & vbCrLf & _
       "            END ) AS Relation " & vbCrLf & _
       "        FROM KANKEI_TB T WHERE T.CINmb = A.CINmb GROUP BY T.CINmb ) AS Relation "

    '【文書】EXCEL出力用データ取得SQL：SELECT句
    Private strSelectSqlForDoc As String = _
           "  ,J.Version" & vbCrLf & _
           "  ,J.CrateID" & vbCrLf & _
           "  ,J.CrateNM" & vbCrLf & _
           "  ,CASE WHEN COALESCE(J.CreateDT,'') = '' THEN '' ELSE TO_CHAR(TO_DATE(J.CreateDT, 'YYYYMMDD'),'FMYYYY/FMMM/FMDD') END AS CreateDT" & vbCrLf & _
           "  ,J.LastUpID" & vbCrLf & _
           "  ,J.LastUpNM" & vbCrLf & _
           "  ,CASE WHEN J.LastUpDT IS NULL THEN '' ELSE TO_CHAR(J.LastUpDT,'FMYYYY/FMMM/FMDD') END AS LastUpDT" & vbCrLf & _
           "  ,J.ChargeID" & vbCrLf & _
           "  ,J.ChargeNM" & vbCrLf & _
           "  ,J.ShareteamNM" & vbCrLf & _
           "  ,J.OfferNM" & vbCrLf & _
           "  ,CASE WHEN COALESCE(J.DelDT,'') = '' THEN '' ELSE TO_CHAR(TO_DATE(J.DelDT, 'YYYYMMDD'),'FMYYYY/FMMM/FMDD') END AS DelDT" & vbCrLf & _
           "  ,J.DelReason" & vbCrLf

    '【サポセン】EXCEL出力用データ取得SQL：SELECT句
    Private strSelectSqlForSap As String = _
           "  ,J.MemorySize" & vbCrLf & _
           "  ,J.Kataban" & vbCrLf & _
           "  ,J.Serial" & vbCrLf & _
           "  ,J.MacAddress1" & vbCrLf & _
           "  ,J.MacAddress2" & vbCrLf & _
           "  ,J.Fuzokuhin" & vbCrLf & _
           "  ,K.SCKikiType" & vbCrLf & _
           "  ,J.SCkikiFixNmb" & vbCrLf & _
           "  ,J.KikiState" & vbCrLf & _
           "  ,J.ImageNmb" & vbCrLf & _
           "  ,J.IntroductNmb" & vbCrLf & _
           "  ,CASE WHEN J.LeaseUpDT = '' THEN '' ELSE TO_CHAR(TO_DATE(J.LeaseUpDT, 'YYYYMMDD'),'FMYYYY/FMMM/FMDD') END AS LeaseUpDT" & vbCrLf & _
           "  ,J.SCHokanKbn" & vbCrLf & _
           "  ,CASE WHEN J.LastInfoDT = '' THEN '' ELSE TO_CHAR(TO_DATE(J.LastInfoDT, 'YYYYMMDD'),'FMYYYY/FMMM/FMDD') END AS LastInfoDT" & vbCrLf & _
           "  ,J.ManageKyokuNM" & vbCrLf & _
           "  ,J.ManageBusyoNM" & vbCrLf & _
           "  ,J.WorkFromNmb" & vbCrLf & _
           "  ,L.KikiStateNm AS KikiRiyoKeitai" & vbCrLf & _
           "  ,M.KikiStateNm AS IPWariate" & vbCrLf & _
           "  ,J.FixedIP" & vbCrLf & _
           "  ,J.UsrID" & vbCrLf & _
           "  ,J.UsrNM" & vbCrLf & _
           "  ,J.UsrCompany" & vbCrLf & _
           "  ,J.UsrKyokuNM" & vbCrLf & _
           "  ,J.UsrBusyoNM" & vbCrLf & _
           "  ,J.UsrTel" & vbCrLf & _
           "  ,J.UsrMailAdd" & vbCrLf & _
           "  ,J.UsrContact" & vbCrLf & _
           "  ,J.UsrRoom" & vbCrLf & _
           "  ,CASE WHEN J.RentalStDT = '' THEN '' ELSE TO_CHAR(TO_DATE(J.RentalStDT, 'YYYYMMDD'),'FMYYYY/FMMM/FMDD') END AS RentalStDT " & vbCrLf & _
           "  ,CASE WHEN J.RentalEdDT = '' THEN '' ELSE TO_CHAR(TO_DATE(J.RentalEdDT, 'YYYYMMDD'),'FMYYYY/FMMM/FMDD') END AS RentalEdDT " & vbCrLf & _
           "  ,J.SetKyokuNM" & vbCrLf & _
           "  ,J.SetBusyoNM" & vbCrLf & _
           "  ,J.SetRoom" & vbCrLf & _
           "  ,J.SetBuil" & vbCrLf & _
           "  ,J.SetFloor" & vbCrLf & _
           "  ,J.SetDeskNo" & vbCrLf & _
           "  ,J.SetLANLength" & vbCrLf & _
           "  ,J.SetLANNum" & vbCrLf & _
           "  ,J.SetSocket" & vbCrLf & _
           "  ,N.OptSoft" & vbCrLf & _
           "  ,O.SetKikiNo" & vbCrLf & _
           "  ,P.ShareUsr" & vbCrLf

    '[Mod] 2013/11/07 e.okamura 未設定日付項目出力対応 START
    ''【部所有機器】EXCEL出力用データ取得SQL：SELECT句
    'Private strSelectSqlForBuy As String = _
    '       "  ,J.Kataban" & vbCrLf & _
    '       "  ,J.Aliau" & vbCrLf & _
    '       "  ,J.Serial" & vbCrLf & _
    '       "  ,J.MacAddress1" & vbCrLf & _
    '       "  ,J.MacAddress2" & vbCrLf & _
    '       "  ,CASE WHEN J.ZooKbn='" & ZOO_KBN_FIN & "' THEN '" & ZOO_NM_FIN & "' ELSE '" & ZOO_NM_UNFIN & "' END AS ZooKbn" & vbCrLf & _
    '       "  ,K.SoftNM AS OsNM" & vbCrLf & _
    '       "  ,L.SoftNM AS AntiVirusSoftNM" & vbCrLf & _
    '       "  ,M.KikiStateNM" & vbCrLf & _
    '       "  ,J.NIC1" & vbCrLf & _
    '       "  ,J.NIC2" & vbCrLf & _
    '       "  ,TO_CHAR(TO_DATE(J.ConnectDT, 'YYYYMMDD'),'FMYYYY/FMMM/FMDD') AS ConnectDT" & vbCrLf & _
    '       "  ,TO_CHAR(TO_DATE(J.ExpirationDT, 'YYYYMMDD'),'FMYYYY/FMMM/FMDD') AS ExpirationDT" & vbCrLf & _
    '       "  ,TO_CHAR(TO_DATE(J.DeletDT, 'YYYYMMDD'),'FMYYYY/FMMM/FMDD') AS DeletDT" & vbCrLf & _
    '       "  ,TO_CHAR(TO_DATE(J.LastInfoDT, 'YYYYMMDD'),'FMYYYY/FMMM/FMDD') AS LastInfoDT" & vbCrLf & _
    '       "  ,J.ConectReason" & vbCrLf & _
    '       "  ,TO_CHAR(TO_DATE(J.ExpirationUPDT, 'YYYYMMDD'),'FMYYYY/FMMM/FMDD') AS ExpirationUPDT" & vbCrLf & _
    '       "  ,TO_CHAR(TO_DATE(J.InfoDT, 'YYYYMMDD'),'FMYYYY/FMMM/FMDD') AS InfoDT" & vbCrLf & _
    '       "  ,CASE WHEN J.NumInfoKbn='" & NUMINFO_KBN_FIN & "' THEN '" & NUMINFO_NM_FIN & "' ELSE '" & NUMINFO_NM_UNFIN & "' END AS NumInfoKbn" & vbCrLf & _
    '       "  ,CASE WHEN J.SealSendKbn='" & SEALSEND_KBN_FIN & "' THEN '" & SEALSEND_NM_FIN & "' ELSE '" & SEALSEND_NM_UNFIN & "' END AS SealSendKbn" & vbCrLf & _
    '       "  ,CASE WHEN J.AntiVirusSofCheckKbn='" & ANTIVIRUSSOFCHECK_KBN_FIN & "' THEN '" & ANTIVIRUSSOFCHECK_NM_FIN & "' ELSE '" & ANTIVIRUSSOFCHECK_NM_UNFIN & "' END AS AntiVirusSoftCheckKbn" & vbCrLf & _
    '       "  ,TO_CHAR(TO_DATE(J.AntiVirusSofCheckDT, 'YYYYMMDD'),'FMYYYY/FMMM/FMDD') AS AntiVirusSofCheckDT" & vbCrLf & _
    '       "  ,J.BusyoKikiBIko" & vbCrLf & _
    '       "  ,J.ManageKyokuNM" & vbCrLf & _
    '       "  ,J.ManageBusyoNM" & vbCrLf & _
    '       "  ,J.WorkFromNmb" & vbCrLf & _
    '       "  ,N.KikiStateNM" & vbCrLf & _
    '       "  ,J.FixedIP" & vbCrLf & _
    '       "  ,J.UsrID" & vbCrLf & _
    '       "  ,J.UsrNM" & vbCrLf & _
    '       "  ,J.UsrCompany" & vbCrLf & _
    '       "  ,J.UsrKyokuNM" & vbCrLf & _
    '       "  ,J.UsrBusyoNM" & vbCrLf & _
    '       "  ,J.UsrTel" & vbCrLf & _
    '       "  ,J.UsrMailAdd" & vbCrLf & _
    '       "  ,J.UsrContact" & vbCrLf & _
    '       "  ,J.UsrRoom" & vbCrLf & _
    '       "  ,J.SetKyokuNM" & vbCrLf & _
    '       "  ,J.SetBusyoNM" & vbCrLf & _
    '       "  ,J.SetRoom" & vbCrLf & _
    '       "  ,J.SetBuil" & vbCrLf & _
    '       "  ,J.SetFloor" & vbCrLf

    '【部所有機器】EXCEL出力用データ取得SQL：SELECT句
    Private strSelectSqlForBuy As String = _
           "  ,J.Kataban" & vbCrLf & _
           "  ,J.Aliau" & vbCrLf & _
           "  ,J.Serial" & vbCrLf & _
           "  ,J.MacAddress1" & vbCrLf & _
           "  ,J.MacAddress2" & vbCrLf & _
           "  ,CASE WHEN J.ZooKbn='" & ZOO_KBN_FIN & "' THEN '" & ZOO_NM_FIN & "' ELSE '" & ZOO_NM_UNFIN & "' END AS ZooKbn" & vbCrLf & _
           "  ,K.SoftNM AS OsNM" & vbCrLf & _
           "  ,L.SoftNM AS AntiVirusSoftNM" & vbCrLf & _
           "  ,M.KikiStateNM" & vbCrLf & _
           "  ,J.NIC1" & vbCrLf & _
           "  ,J.NIC2" & vbCrLf & _
           "  ,CASE WHEN J.ConnectDT = '' THEN '' ELSE TO_CHAR(TO_DATE(J.ConnectDT, 'YYYYMMDD'),'FMYYYY/FMMM/FMDD') END AS ConnectDT" & vbCrLf & _
           "  ,CASE WHEN J.ExpirationDT = '' THEN '' ELSE TO_CHAR(TO_DATE(J.ExpirationDT, 'YYYYMMDD'),'FMYYYY/FMMM/FMDD') END AS ExpirationDT" & vbCrLf & _
           "  ,CASE WHEN J.DeletDT = '' THEN '' ELSE TO_CHAR(TO_DATE(J.DeletDT, 'YYYYMMDD'),'FMYYYY/FMMM/FMDD') END AS DeletDT" & vbCrLf & _
           "  ,CASE WHEN J.LastInfoDT = '' THEN '' ELSE TO_CHAR(TO_DATE(J.LastInfoDT, 'YYYYMMDD'),'FMYYYY/FMMM/FMDD') END AS LastInfoDT" & vbCrLf & _
           "  ,J.ConectReason" & vbCrLf & _
           "  ,CASE WHEN J.ExpirationUPDT = '' THEN '' ELSE TO_CHAR(TO_DATE(J.ExpirationUPDT, 'YYYYMMDD'),'FMYYYY/FMMM/FMDD') END AS ExpirationUPDT" & vbCrLf & _
           "  ,CASE WHEN J.InfoDT = '' THEN '' ELSE TO_CHAR(TO_DATE(J.InfoDT, 'YYYYMMDD'),'FMYYYY/FMMM/FMDD') END AS InfoDT" & vbCrLf & _
           "  ,CASE WHEN J.NumInfoKbn='" & NUMINFO_KBN_FIN & "' THEN '" & NUMINFO_NM_FIN & "' ELSE '" & NUMINFO_NM_UNFIN & "' END AS NumInfoKbn" & vbCrLf & _
           "  ,CASE WHEN J.SealSendKbn='" & SEALSEND_KBN_FIN & "' THEN '" & SEALSEND_NM_FIN & "' ELSE '" & SEALSEND_NM_UNFIN & "' END AS SealSendKbn" & vbCrLf & _
           "  ,CASE WHEN J.AntiVirusSofCheckKbn='" & ANTIVIRUSSOFCHECK_KBN_FIN & "' THEN '" & ANTIVIRUSSOFCHECK_NM_FIN & "' ELSE '" & ANTIVIRUSSOFCHECK_NM_UNFIN & "' END AS AntiVirusSoftCheckKbn" & vbCrLf & _
           "  ,CASE WHEN J.AntiVirusSofCheckDT = '' THEN '' ELSE TO_CHAR(TO_DATE(J.AntiVirusSofCheckDT, 'YYYYMMDD'),'FMYYYY/FMMM/FMDD') END AS AntiVirusSofCheckDT" & vbCrLf & _
           "  ,J.BusyoKikiBIko" & vbCrLf & _
           "  ,J.ManageKyokuNM" & vbCrLf & _
           "  ,J.ManageBusyoNM" & vbCrLf & _
           "  ,J.WorkFromNmb" & vbCrLf & _
           "  ,N.KikiStateNM" & vbCrLf & _
           "  ,J.FixedIP" & vbCrLf & _
           "  ,J.UsrID" & vbCrLf & _
           "  ,J.UsrNM" & vbCrLf & _
           "  ,J.UsrCompany" & vbCrLf & _
           "  ,J.UsrKyokuNM" & vbCrLf & _
           "  ,J.UsrBusyoNM" & vbCrLf & _
           "  ,J.UsrTel" & vbCrLf & _
           "  ,J.UsrMailAdd" & vbCrLf & _
           "  ,J.UsrContact" & vbCrLf & _
           "  ,J.UsrRoom" & vbCrLf & _
           "  ,J.SetKyokuNM" & vbCrLf & _
           "  ,J.SetBusyoNM" & vbCrLf & _
           "  ,J.SetRoom" & vbCrLf & _
           "  ,J.SetBuil" & vbCrLf & _
           "  ,J.SetFloor" & vbCrLf
    '[Mod] 2013/11/07 e.okamura 未設定日付項目出力対応 END

    '【共通】EXCEL出力用データ取得SQL：FROM句
    Private strFromSqlExcel As String = _
           " FROM " & _
           " CI_INFO_TB A " & _
           " LEFT OUTER JOIN CI_KIND_MTB B  ON A.CIKbnCD = B.CIKbnCD " & _
           " LEFT OUTER JOIN KIND_MTB C  ON A.KindCD = C.KindCD " & _
           " LEFT OUTER JOIN CISTATE_MTB D ON  A.CIStatusCD = D.CIStateCD " & _
           " LEFT OUTER JOIN GRP_MTB E  ON A.CIOwnerCD = E.GroupCD " & _
           " LEFT OUTER JOIN GRP_MTB F  ON A.RegGrpCD = F.GroupCD " & _
           " LEFT OUTER JOIN HBKUSR_MTB G ON A.RegID = G.HBKUsrID " & _
           " LEFT OUTER JOIN GRP_MTB H  ON A.UpGrpCD = H.GroupCD " & _
           " LEFT OUTER JOIN HBKUSR_MTB I ON A.UpdateID = I.HBKUsrID "

    '[Add] 2012/08/02 y.ikushima START
    ''【システム】EXCEL出力用データ取得SQL：FROM句
    'Private strFromSqlForSys As String = _
    '    " LEFT OUTER JOIN CI_SYS_TB J ON A.CINmb = J.CINmb " & vbCrLf & _
    '    " LEFT OUTER JOIN (SELECT T.CINmb " & vbCrLf & _
    '    "                        ,STRING_AGG(T.Url || '　' || T.UrlNaiyo,'／' ORDER BY T.RowNmb) AS Url " & vbCrLf & _
    '    "                  FROM KNOWHOWURL_TB T " & vbCrLf & _
    '    "                  GROUP BY T.CINmb" & vbCrLf & _
    '    "                 ) K ON A.CINmb = K.CINmb " & vbCrLf & _
    '    " LEFT OUTER JOIN (SELECT T.CINmb " & vbCrLf & _
    '    "                        ,STRING_AGG(T.ManageNmb || '　' || T.ManageNmbNaiyo,'／' ORDER BY T.RowNmb) AS SrvMng " & vbCrLf & _
    '    "                  FROM SRVMNG_TB T " & vbCrLf & _
    '    "                  GROUP BY T.CINmb" & vbCrLf & _
    '    "                 ) L ON A.CINmb = L.CINmb " & vbCrLf & _
    '    " LEFT OUTER JOIN (SELECT T.CINmb " & vbCrLf & _
    '    "                        ,STRING_AGG(T.RelationKbn || '　' || T.RelationGrpCD || '　' ||" & vbCrLf & _
    '    "                           CASE T.RelationKbn WHEN '" & KBN_GROUP & "' THEN (SELECT T2.GroupNM FROM GRP_MTB T2 WHERE T.RelationGrpCD = T2.GroupCD)" & vbCrLf & _
    '    "                                              ELSE (SELECT T2.GroupNM FROM GRP_MTB T2 WHERE T.RelationGrpCD = T2.GroupCD)|| '　' || T.RelationUsrID || '　' || (SELECT T2.HBKUsrNM FROM HBKUSR_MTB T2 WHERE T.RelationUsrID = T2.HBKUsrID) " & vbCrLf & _
    '    "                                              END" & vbCrLf & _
    '    "                          ,'／' ORDER BY" & vbCrLf & _
    '    "                           CASE T.RelationKbn WHEN '" & KBN_GROUP & "' THEN (SELECT T2.Sort FROM GRP_MTB T2 WHERE T.RelationGrpCD = T2.GroupCD)" & vbCrLf & _
    '    "                                              ELSE (SELECT T2.Sort FROM HBKUSR_MTB T2 WHERE T.RelationUsrID = T2.HBKUsrID)" & vbCrLf & _
    '    "                                              END" & vbCrLf & _
    '    "                         ) AS Relation" & vbCrLf & _
    '    "                  FROM KANKEI_TB T " & vbCrLf & _
    '    "                  GROUP BY T.CINmb " & vbCrLf & _
    '    "                 ) M ON A.CINmb = M.CINmb " & vbCrLf
    '[Add] 2012/08/02 y.ikushima END

    '【システム】EXCEL出力用データ取得SQL：FROM句
    'Private strFromSqlForSys As String = _
    '    " LEFT OUTER JOIN CI_SYS_TB J ON A.CINmb = J.CINmb " & vbCrLf & _
    '    " LEFT OUTER JOIN (SELECT T.CINmb " & vbCrLf & _
    '    "                        ,STRING_AGG(T.Url || '　' || T.UrlNaiyo,'／' ORDER BY T.RowNmb) AS Url " & vbCrLf & _
    '    "                  FROM KNOWHOWURL_TB T " & vbCrLf & _
    '    "                  GROUP BY T.CINmb" & vbCrLf & _
    '    "                 ) K ON A.CINmb = K.CINmb " & vbCrLf & _
    '    " LEFT OUTER JOIN (SELECT T.CINmb " & vbCrLf & _
    '    "                        ,STRING_AGG(T.ManageNmb || '　' || T.ManageNmbNaiyo,'／' ORDER BY T.RowNmb) AS SrvMng " & vbCrLf & _
    '    "                  FROM SRVMNG_TB T " & vbCrLf & _
    '    "                  GROUP BY T.CINmb" & vbCrLf & _
    '    "                 ) L ON A.CINmb = L.CINmb " & vbCrLf & _
    '    " LEFT OUTER JOIN (SELECT T.CINmb " & vbCrLf & _
    '    "                        ,STRING_AGG(T.RelationKbn || '　' || T.RelationID || '　' ||" & vbCrLf & _
    '    "                           CASE T.RelationKbn WHEN '" & KBN_GROUP & "' THEN (SELECT T2.GroupNM FROM GRP_MTB T2 WHERE T.RelationID = T2.GroupCD)" & vbCrLf & _
    '    "                                              ELSE (SELECT T2.HBKUsrNM FROM HBKUSR_MTB T2 WHERE T.RelationID = T2.HBKUsrID)" & vbCrLf & _
    '    "                                              END" & vbCrLf & _
    '    "                          ,'／' ORDER BY" & vbCrLf & _
    '    "                           CASE T.RelationKbn WHEN '" & KBN_GROUP & "' THEN (SELECT T2.Sort FROM GRP_MTB T2 WHERE T.RelationID = T2.GroupCD)" & vbCrLf & _
    '    "                                              ELSE (SELECT T2.Sort FROM HBKUSR_MTB T2 WHERE T.RelationID = T2.HBKUsrID)" & vbCrLf & _
    '    "                                              END" & vbCrLf & _
    '    "                         ) AS Relation" & vbCrLf & _
    '    "                  FROM KANKEI_TB T " & vbCrLf & _
    '    "                  GROUP BY T.CINmb " & vbCrLf & _
    '    "                 ) M ON A.CINmb = M.CINmb " & vbCrLf
    Private strFromSqlForSys As String = _
    " LEFT OUTER JOIN CI_SYS_TB J ON A.CINmb = J.CINmb " & vbCrLf

    '【文書】EXCEL出力用データ取得SQL：FROM句
    Private strFromSqlForDoc As String = _
        " LEFT OUTER JOIN CI_DOC_TB J ON A.CINmb = J.CINmb" & vbCrLf

    ''【サポセン】EXCEL出力用データ取得SQL：FROM句
    'Private strFromSqlForSap As String = _
    '    " LEFT OUTER JOIN CI_SAP_TB J ON A.CINmb = J.CINmb" & vbCrLf & _
    '    " LEFT OUTER JOIN SAP_KIKI_TYPE_MTB K ON J.TypeKbn = K.SCKikiCD" & vbCrLf & _
    '    " LEFT OUTER JOIN KiKiState_Mtb L ON J.KikiUsecd = L.KikiStateCd AND L.KikiStateKbn = '" & KIKISTATEKBN_KIKI_RIYOKEITAI & "'" & vbCrLf & _
    '    " LEFT OUTER JOIN KiKiState_Mtb M ON J.KikiUsecd = M.KikiStateCd AND M.KikiStateKbn = '" & KIKISTATEKBN_IP_WARIATE & "'" & vbCrLf & _
    '    " LEFT OUTER JOIN (SELECT CINmb " & vbCrLf & _
    '    "                        ,STRING_AGG(SoftNM,'／' ORDER BY RowNmb) AS OptSoft " & vbCrLf & _
    '    "                  FROM OPTSOFT_TB T LEFT OUTER JOIN soft_mtb sm ON T.SoftCD = sm.SoftCD " & vbCrLf & _
    '    "                  GROUP BY T.CINmb" & vbCrLf & _
    '    "                 ) N ON A.CINmb = N.CINmb " & vbCrLf & _
    '    " LEFT OUTER JOIN (SELECT T.CINmb " & vbCrLf & _
    '    "                        ,STRING_AGG(T4.KindNM || SUBSTR(T3.SetKikiNo,4),'／' ORDER BY T4.Sort, SUBSTR(T3.SetKikiNo,4)) AS SetKikiNo" & vbCrLf & _
    '    "                  FROM CI_INFO_TB T" & vbCrLf & _
    '    "                  LEFT OUTER JOIN SET_KIKI_MNG_TB T2 ON (T.KindCD || T.Num) = T2.SetKikiNo" & vbCrLf & _
    '    "                  LEFT OUTER JOIN SET_KIKI_MNG_TB T3 ON T2.SetKikiGrpNo = T3.SetKikiGrpNo AND T2.SetKikiNo <> T3.SetKikiNo" & vbCrLf & _
    '    "                  LEFT OUTER JOIN KIND_MTB T4 ON SUBSTR(T3.SetKikiNo,1,3) = T4.KindCD" & vbCrLf & _
    '    "                  WHERE T2.JtiFlg = '0'" & vbCrLf & _
    '    "                    AND T3.JtiFlg = '0'" & vbCrLf & _
    '    "                  GROUP BY T.CINmb" & vbCrLf & _
    '    "                 ) O ON A.CINmb = O.CINmb " & vbCrLf & _
    '    " LEFT OUTER JOIN (SELECT T.CINmb " & vbCrLf & _
    '    "                        ,STRING_AGG(T.UsrID || '　' || T.UsrNM,'／' ORDER BY T.RowNmb) AS ShareUsr " & vbCrLf & _
    '    "                  FROM SHARE_TB T " & vbCrLf & _
    '    "                  GROUP BY T.CINmb" & vbCrLf & _
    '    "                 ) P ON A.CINmb = P.CINmb " & vbCrLf

    '【サポセン】EXCEL出力用データ取得SQL：FROM句
    Private strFromSqlForSap As String = _
        " LEFT OUTER JOIN CI_SAP_TB J ON A.CINmb = J.CINmb" & vbCrLf & _
        " LEFT OUTER JOIN SAP_KIKI_TYPE_MTB K ON J.TypeKbn = K.SCKikiCD" & vbCrLf & _
        " LEFT OUTER JOIN KiKiState_Mtb L ON J.KikiUsecd = L.KikiStateCd AND L.KikiStateKbn = '" & KIKISTATEKBN_KIKI_RIYOKEITAI & "'" & vbCrLf & _
        " LEFT OUTER JOIN KiKiState_Mtb M ON J.KikiUsecd = M.KikiStateCd AND M.KikiStateKbn = '" & KIKISTATEKBN_IP_WARIATE & "'" & vbCrLf & _
        " LEFT OUTER JOIN (SELECT CINmb " & vbCrLf & _
        "                        ,STRING_AGG(SoftNM,'／' ORDER BY RowNmb) AS OptSoft " & vbCrLf & _
        "                  FROM OPTSOFT_TB T LEFT OUTER JOIN soft_mtb sm ON T.SoftCD = sm.SoftCD " & vbCrLf & _
        "                  GROUP BY T.CINmb" & vbCrLf & _
        "                 ) N ON A.CINmb = N.CINmb " & vbCrLf & _
        " LEFT OUTER JOIN (SELECT T.CINmb, STRING_AGG(T4.KindNM || T3.Num,'／' ORDER BY T3.Sort) AS SetKikiNo " & vbCrLf & _
        "                  FROM CI_INFO_TB T LEFT OUTER JOIN SET_KIKI_MNG_TB T2 ON T.SetKikiID = T2.SetKikiID AND " & vbCrLf & _
        "                  T.CINmb <> T2.CINmb LEFT OUTER JOIN CI_INFO_TB T3 ON " & vbCrLf & _
        "                  T2.CINmb = T3.CINmb LEFT OUTER JOIN KIND_MTB T4 ON T3.KindCD = T4.KindCD " & vbCrLf & _
        "                  GROUP BY T.CINmb " & vbCrLf & _
        "                  ) O ON A.CINmb = O.CINmb " & vbCrLf & _
        " LEFT OUTER JOIN (SELECT T.CINmb " & vbCrLf & _
        "                        ,STRING_AGG(T.UsrID || '　' || T.UsrNM,'／' ORDER BY T.RowNmb) AS ShareUsr " & vbCrLf & _
        "                  FROM SHARE_TB T " & vbCrLf & _
        "                  GROUP BY T.CINmb" & vbCrLf & _
        "                 ) P ON A.CINmb = P.CINmb " & vbCrLf



    '【部所有機器】EXCEL出力用データ取得SQL：FROM句
    Private strFromSqlForBuy As String = _
        " LEFT OUTER JOIN CI_BUY_TB J ON A.CINmb = J.CINmb " & vbCrLf & _
        " LEFT OUTER JOIN SOFT_MTB K ON J.OsNM = K.SoftNM" & vbCrLf & _
        " LEFT OUTER JOIN SOFT_MTB L ON J.AntiVirusSoftNM = L.SoftNM" & vbCrLf & _
        " LEFT OUTER JOIN KIKISTATE_MTB M ON J.DNSRegCd = M.KikiStateCd" & vbCrLf & _
        " LEFT OUTER JOIN KIKISTATE_MTB N ON J.IPUseCD = N.KikiStateCd" & vbCrLf



    '【システム】EXCEL出力：ORDER BY句
    Private strOrderBySqlForSys As String = _
        " ORDER BY A.Sort ASC" & vbCrLf

    '【文書】EXCEL出力：ORDER BY句
    Private strOrderBySqlForDoc As String = _
        " ORDER BY A.Class1 ASC, A.Class2 ASC, A.CINM ASC" & vbCrLf

    '【サポセン】EXCEL出力：ORDER BY句
    Private strOrderBySqlForSap As String = _
        " ORDER BY B.Sort ASC,A.Num ASC" & vbCrLf

    '【部所有機器】EXCEL出力：ORDER BY句
    Private strOrderBySqlForBuy As String = _
        " ORDER BY B.Sort ASC,A.Num ASC" & vbCrLf



    ''' <summary>
    ''' 【システム】EXCEL出力データ取得SQLの作成・設定
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0102">[IN]共通検索EXCEL出力ロジックデータクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI種別がシステムのEXCEL出力データ取得SQLの作成・設定
    ''' <para>作成情報：2012/07/20 t.fukuo
    ''' </para></remarks>
    Public Function SetSelectSystemSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB0102 As DataHBKB0102) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '一覧取得SQL
        Dim sbSql As New StringBuilder()

        Try
            'SQL作成

            'SELECT句をセット
            sbSql.Append(strSelectSql)          '共通SQL
            sbSql.Append(strSelectSqlForSys)    'システム用SQL

            'FROM句をセット
            sbSql.Append(strFromSqlExcel)       '共通SQL
            sbSql.Append(strFromSqlForSys)      'システム用SQL

            'WHERE句セット
            dataHBKB0102.PropSbStrSQL = sbSql
            If SetWhereCmd(Adapter, dataHBKB0102) = False Then
                Return False
            End If
            sbSql = dataHBKB0102.PropSbStrSQL
            
            'ORDER BY句をセット
            sbSql.Append(strOrderBySqlForSys)   'システム用SQL


            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(sbSql.ToString, Cn)

            'バインド変数に型と値をセット
            If SetBind(Adapter, dataHBKB0102) = False Then
                Return False
            End If


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常値を返す
            Return True


        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【文書】EXCEL出力データ取得SQLの作成・設定
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0102">[IN]共通検索EXCEL出力ロジックデータクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI種別が文書のEXCEL出力データ取得SQLの作成・設定
    ''' <para>作成情報：2012/07/21 t.fukuo
    ''' </para></remarks>
    Public Function SetSelectDocSql(ByRef Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKB0102 As DataHBKB0102) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '一覧取得SQL
        Dim sbSql As New StringBuilder()

        Try
            'SQL作成

            'SELECT句をセット
            sbSql.Append(strSelectSql)          '共通SQL
            sbSql.Append(strSelectSqlForDoc)    '文書用SQL

            'FROM句をセット
            sbSql.Append(strFromSqlExcel)       '共通SQL
            sbSql.Append(strFromSqlForDoc)      '文書用SQL

            'WHERE句セット
            dataHBKB0102.PropSbStrSQL = sbSql
            If SetWhereCmd(Adapter, dataHBKB0102) = False Then
                Return False
            End If
            sbSql = dataHBKB0102.PropSbStrSQL

            'ORDER BY句をセット
            sbSql.Append(strOrderBySqlForDoc)   '文書用SQL


            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(sbSql.ToString, Cn)

            'バインド変数に型と値をセット
            If SetBind(Adapter, dataHBKB0102) = False Then
                Return False
            End If


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常値を返す
            Return True


        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【サポセン】EXCEL出力データ取得SQLの作成・設定
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0102">[IN]共通検索EXCEL出力ロジックデータクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI種別がサポセンのEXCEL出力データ取得SQLの作成・設定
    ''' <para>作成情報：2012/07/21 t.fukuo
    ''' </para></remarks>
    Public Function SetSelectSapSql(ByRef Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKB0102 As DataHBKB0102) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '一覧取得SQL
        Dim sbSql As New StringBuilder()

        Try
            'SQL作成

            'SELECT句をセット
            sbSql.Append(strSelectSql)          '共通SQL
            sbSql.Append(strSelectSqlForSap)    'サポセン用SQL

            'FROM句をセット
            sbSql.Append(strFromSqlExcel)       '共通SQL
            sbSql.Append(strFromSqlForSap)      'サポセン用SQL

            'WHERE句セット
            dataHBKB0102.PropSbStrSQL = sbSql
            If SetWhereCmd(Adapter, dataHBKB0102) = False Then
                Return False
            End If
            sbSql = dataHBKB0102.PropSbStrSQL

            'ORDER BY句をセット
            sbSql.Append(strOrderBySqlForSap)   'サポセン用SQL


            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(sbSql.ToString, Cn)

            'バインド変数に型と値をセット
            If SetBind(Adapter, dataHBKB0102) = False Then
                Return False
            End If


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常値を返す
            Return True


        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【部所有機器】EXCEL出力データ取得SQLの作成・設定
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0102">[IN]共通検索EXCEL出力ロジックデータクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI種別が部所有機器のEXCEL出力データ取得SQLの作成・設定
    ''' <para>作成情報：2012/07/21 t.fukuo
    ''' </para></remarks>
    Public Function SetSelectBuySql(ByRef Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKB0102 As DataHBKB0102) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '一覧取得SQL
        Dim sbSql As New StringBuilder()

        Try
            'SQL作成

            'SELECT句をセット
            sbSql.Append(strSelectSql)          '共通SQL
            sbSql.Append(strSelectSqlForBuy)    '部所有機器用SQL

            'FROM句をセット
            sbSql.Append(strFromSqlExcel)       '共通SQL
            sbSql.Append(strFromSqlForBuy)      '部所有機器用SQL

            'WHERE句セット
            dataHBKB0102.PropSbStrSQL = sbSql
            If SetWhereCmd(Adapter, dataHBKB0102) = False Then
                Return False
            End If
            sbSql = dataHBKB0102.PropSbStrSQL

            'ORDER BY句をセット
            sbSql.Append(strOrderBySqlForBuy)   '部所有機器用SQL


            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(sbSql.ToString, Cn)

            'バインド変数に型と値をセット
            If SetBind(Adapter, dataHBKB0102) = False Then
                Return False
            End If


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常値を返す
            Return True


        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' WHERE句作成
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgsqlDataAdapter</param>
    ''' <param name="DataHBKB0102">[IN/OUT]EXCEL出力ロジックデータクラス</param>
    ''' <returns>boolean  取得状況 　true  正常終了  false  異常終了</returns>
    ''' <remarks>WHERE句を作成し、SQLに設定する
    ''' <para>作成情報：2012/07/18 t.fukuo
    ''' </para></remarks>
    Public Function SetWhereCmd(ByRef Adapter As NpgsqlDataAdapter, _
                                ByRef DataHBKB0102 As DataHBKB0102) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intRowCount As Integer = 0


        Try
            With DataHBKB0102

                Dim strCIKbnCD As String = .PropStrCiKbnCD_Search                                               'CI種別CD
                Dim strKindCD As String = .PropStrKindCD_Search                                                 '種別CD
                Dim strNo As String = .PropStrNum_Search                                                        '番号
                If Trim(.PropStrNum_Search) <> String.Empty Then
                    'サポセンまたは部所有機器の場合は番号0埋め
                    Select Case .PropStrCiKbnCD_Search
                        Case CI_TYPE_SUPORT
                            strNo = .PropStrNum_Search.PadLeft(FORMAT_NUM, "0"c)
                        Case CI_TYPE_KIKI
                            strNo = .PropStrNum_Search.PadLeft(FORMAT_NUM, "0"c)
                    End Select
                End If
                Dim strStatus As String = .PropStrStatusCD_Search                                               'CIステータスCD
                Dim strCIOner As String = .PropStrCiOwnerCD_Search                                              'CIオーナーCD
                Dim strBunrui1 As String = commonLogicHBK.ChangeStringForSearch(Trim(.PropStrClass1_Search))    '分類１　※あいまい
                Dim strBunrui2 As String = commonLogicHBK.ChangeStringForSearch(Trim(.PropStrClass2_Search))    '分類２　※あいまい
                Dim strName As String = commonLogicHBK.ChangeStringForSearch(Trim(.PropStrCINM_Search))         '名称　　※あいまい
                Dim strFreeword As String = Trim(.PropStrFreeWordAimai_Search)                                  'フリーワード
                Dim strUpdateFrom As String = .PropStrUpdateDTFrom_Search                                       '最終更新日FROM
                Dim strUpdateTo As String = .PropStrUpdateDTTo_Search                                           '最終更新日TO
                Dim strFreeText As String = Trim(.PropStrBikoAimai_Search)                                      'フリーテキスト
                Dim strFreeFlg1 As String = .PropStrFreeFlg1_Search                                             'フリーフラグ１
                Dim strFreeFlg2 As String = .PropStrFreeFlg2_Search                                             'フリーフラグ２
                Dim strFreeFlg3 As String = .PropStrFreeFlg3_Search                                             'フリーフラグ３
                Dim strFreeFlg4 As String = .PropStrFreeFlg4_Search                                             'フリーフラグ４
                Dim strFreeFlg5 As String = .PropStrFreeFlg5_Search                                             'フリーフラグ５
                Dim strDocAdd As String = Trim(.PropStrShareteamNM_Search)                                      '文書配布先

                .PropSbStrSQL.Append(" WHERE 1 = 1 " & vbCrLf)

                If Not (strCIKbnCD = String.Empty) Then
                    .PropSbStrSQL.Append(" AND A.CIKbnCD = :CIKbnCD" & vbCrLf)
                End If

                If Not (strKindCD = String.Empty) Then
                    .PropSbStrSQL.Append(" AND A.KindCD = :KindCD" & vbCrLf)
                End If

                If Not (strNo = String.Empty) Then
                    .PropSbStrSQL.Append(" AND A.Num = :Num" & vbCrLf)
                End If

                If Not (strCIOner = String.Empty) Then
                    .PropSbStrSQL.Append(" AND A.CIOwnerCD = :CIOwnerCD" & vbCrLf)
                End If

                If Not (strStatus = String.Empty) Then
                    .PropSbStrSQL.Append(" AND A.CIStatusCD = :CIStatusCD" & vbCrLf)
                End If

                If Not (strBunrui1 = String.Empty) Then
                    .PropSbStrSQL.Append(" AND A.Class1Aimai like :Class1" & vbCrLf)
                End If

                If Not (strBunrui2 = String.Empty) Then
                    .PropSbStrSQL.Append(" AND A.Class2Aimai like :Class2" & vbCrLf)
                End If

                If Not (strName = String.Empty) Then
                    .PropSbStrSQL.Append(" AND A.CINMAimai like :CINM" & vbCrLf)
                End If

                'フリーワード
                If Not (strFreeword = String.Empty) Then
                    CreateSqlFreeWord(Adapter, strFreeword, .PropSbStrSQL)
                End If

                'フリーテキスト
                If Not (strFreeText = String.Empty) Then
                    CreateSqlFreeText(Adapter, strFreeText, .PropSbStrSQL)
                End If

                ''最終更新日　FROM TO
                '.PropSbStrSQL.Append(" AND TO_CHAR(A.UpdateDT,'YYYY/MM/DD') BETWEEN")

                'If Not (strUpdateFrom = String.Empty) Then
                '    .PropSbStrSQL.Append("  :UpdateDTFrom AND ")
                'Else
                '    .PropSbStrSQL.Append(" '0001/01/01' AND ")
                'End If

                'If Not (strUpdateTo = String.Empty) Then
                '    .PropSbStrSQL.Append(" :UpdateDTTo " & vbCrLf)
                'Else
                '    .PropSbStrSQL.Append(" '9999/12/31' " & vbCrLf)
                'End If

                '最終更新日　FROM TO
                .PropSbStrSQL.Append(" AND A.UpdateDT BETWEEN")

                If Not (strUpdateFrom = String.Empty) Then
                    .PropSbStrSQL.Append("  TO_DATE(:UpdateDTFrom,'YYYY/MM/DD') AND ")
                Else
                    .PropSbStrSQL.Append(" TO_DATE('0001/01/01','YYYY/MM/DD') AND ")
                End If

                If Not (strUpdateTo = String.Empty) Then
                    .PropSbStrSQL.Append(" TO_DATE(:UpdateDTTo,'YYYY/MM/DD') " & vbCrLf)
                Else
                    .PropSbStrSQL.Append(" TO_DATE('9999/12/31','YYYY/MM/DD') " & vbCrLf)
                End If

                If Not (strFreeFlg1 = String.Empty) Then
                    .PropSbStrSQL.Append(" AND A.FreeFlg1 = :FreeFlg1" & vbCrLf)
                End If

                If Not (strFreeFlg2 = String.Empty) Then
                    .PropSbStrSQL.Append(" AND A.FreeFlg2 = :FreeFlg2" & vbCrLf)
                End If

                If Not (strFreeFlg3 = String.Empty) Then
                    .PropSbStrSQL.Append(" AND A.FreeFlg3 = :FreeFlg3" & vbCrLf)
                End If

                If Not (strFreeFlg4 = String.Empty) Then
                    .PropSbStrSQL.Append(" AND A.FreeFlg4 = :FreeFlg4" & vbCrLf)
                End If

                If Not (strFreeFlg5 = String.Empty) Then
                    .PropSbStrSQL.Append(" AND A.FreeFlg5 = :FreeFlg5" & vbCrLf)
                End If

                '文書配布先
                If Not (strDocAdd = String.Empty) Then
                    If (strCIKbnCD = CommonDeclareHBK.CI_TYPE_DOC) Then
                        CreateSqlDocAdd(Adapter, strDocAdd, .PropSbStrSQL)
                    End If
                End If

            End With


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常値を返す
            Return True


        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try


    End Function

    ''' <summary>
    ''' バインド変数設定
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgsqlDataAdapter</param>
    ''' <param name="DataHBKB0102">[IN]EXCEL出力ロジックデータクラス</param>
    ''' <returns>boolean  取得状況 　true  正常終了  false  異常終了</returns>
    ''' <remarks>バインド変数に型と値を設定する
    ''' <para>作成情報：2012/07/18 t.fukuo
    ''' </para></remarks>
    Public Function SetBind(ByRef Adapter As NpgsqlDataAdapter, _
                            ByVal DataHBKB0102 As DataHBKB0102) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim intRowCount As Integer = 0
        Dim strAry() As String

        Try
            With DataHBKB0102

                '番号
                Dim strNo As String = .PropStrNum_Search
                If Trim(.PropStrNum_Search) <> String.Empty Then
                    'サポセンまたは部所有機器の場合は番号0埋め
                    Select Case .PropStrCiKbnCD_Search
                        Case CI_TYPE_SUPORT
                            strNo = .PropStrNum_Search.PadLeft(FORMAT_NUM, "0"c)
                        Case CI_TYPE_KIKI
                            strNo = .PropStrNum_Search.PadLeft(FORMAT_NUM, "0"c)
                    End Select
                End If

                '分類１～２、名称
                Dim strBunrui1 As String = commonLogicHBK.ChangeStringForSearch(Trim(.PropStrClass1_Search))
                Dim strBunrui2 As String = commonLogicHBK.ChangeStringForSearch(Trim(.PropStrClass2_Search))
                Dim strName As String = commonLogicHBK.ChangeStringForSearch(Trim(.PropStrCINM_Search))

                'フリーワード、フリーテキスト
                Dim strFreeword As String = Trim(.PropStrFreeWordAimai_Search)
                Dim strFreeText As String = Trim(.PropStrBikoAimai_Search)

                '文書配布先
                Dim strDocAdd As String = Trim(.PropStrShareteamNM_Search)

                'CI種別CD
                If .PropStrCiKbnCD_Search <> String.Empty Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("CIKbnCD").Value = .PropStrCiKbnCD_Search
                End If
                '種別CD
                If .PropStrKindCD_Search <> String.Empty Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("KindCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("KindCD").Value = .PropStrKindCD_Search
                End If
                '番号
                If .PropStrNum_Search <> String.Empty Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("Num", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("Num").Value = .PropStrNum_Search
                End If
                'CIステータスCD
                If .PropStrStatusCD_Search <> String.Empty Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CIStatusCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("CIStatusCD").Value = .PropStrStatusCD_Search
                End If
                'CIオーナーCD
                If .PropStrCiOwnerCD_Search <> String.Empty Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CIOwnerCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("CIOwnerCD").Value = .PropStrCiOwnerCD_Search
                End If
                '分類１
                If strBunrui1 <> String.Empty Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("Class1", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("Class1").Value = "%" & strBunrui1 & "%"
                End If
                '分類２
                If strBunrui2 <> String.Empty Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("Class2", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("Class2").Value = "%" & strBunrui2 & "%"
                End If
                '名称
                If strName <> String.Empty Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINM", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("CINM").Value = "%" & strName & "%"
                End If
                'フリーワード
                If strFreeword <> String.Empty Then
                    strAry = CommonHBK.CommonLogicHBK.GetSearchStringList(strFreeword, CommonDeclareHBK.SPLIT_MODE_AND)
                    For loopIndex As Integer = 0 To strAry.Length - 1 Step 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeWord" & loopIndex, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("FreeWord" & loopIndex).Value = "%" & commonLogicHBK.ChangeStringForSearch(strAry(loopIndex).ToString) & "%"
                    Next
                End If
                '最終更新日（FROM）
                If .PropStrUpdateDTFrom_Search <> String.Empty Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("UpdateDTFrom", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("UpdateDTFrom").Value = .PropStrUpdateDTFrom_Search
                End If
                '最終更新日（TO）
                If .PropStrUpdateDTTo_Search <> String.Empty Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("UpdateDTTo", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("UpdateDTTo").Value = .PropStrUpdateDTTo_Search
                End If
                'フリーテキスト
                If strFreeText <> String.Empty Then
                    strAry = CommonHBK.CommonLogicHBK.GetSearchStringList(strFreeText, CommonDeclareHBK.SPLIT_MODE_AND)
                    For loopIndex As Integer = 0 To strAry.Length - 1 Step 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeText" & loopIndex, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("FreeText" & loopIndex).Value = "%" & commonLogicHBK.ChangeStringForSearch(strAry(loopIndex).ToString) & "%"
                    Next
                End If
                'フリーフラグ１～５
                If .PropStrFreeFlg1_Search <> String.Empty Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg1", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg1").Value = .PropStrFreeFlg1_Search
                End If
                If .PropStrFreeFlg2_Search <> String.Empty Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg2", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg2").Value = .PropStrFreeFlg2_Search
                End If
                If .PropStrFreeFlg3_Search <> String.Empty Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg3", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg3").Value = .PropStrFreeFlg3_Search
                End If
                If .PropStrFreeFlg4_Search <> String.Empty Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg4", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg4").Value = .PropStrFreeFlg4_Search
                End If
                If .PropStrFreeFlg5_Search <> String.Empty Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg5", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg5").Value = .PropStrFreeFlg5_Search
                End If
                '文書配布先
                If strDocAdd <> String.Empty Then
                    strAry = CommonHBK.CommonLogicHBK.GetSearchStringList(strDocAdd, CommonDeclareHBK.SPLIT_MODE_AND)
                    For loopIndex As Integer = 0 To strAry.Length - 1 Step 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("ShareteamNM" & loopIndex, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("ShareteamNM" & loopIndex).Value = "%" & commonLogicHBK.ChangeStringForSearch(strAry(loopIndex).ToString) & "%"
                    Next
                End If


            End With


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常値を返す
            Return True


        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try


    End Function


    ''' <summary>
    ''' フリーワード条件作成処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgsqlDataAdapter</param>
    ''' <param name="strSearch">[IN]フリーワード</param>
    ''' <param name="sbSql">[IN]SQL文字列配列</param>
    ''' <returns>boolean  取得状況 　true  正常終了  false  異常終了</returns>
    ''' <remarks>フリーワード条件をSQL用に作成して返す
    ''' <para>作成情報：2012/07/18 t.fukuo
    ''' </para></remarks>
    Private Function CreateSqlFreeWord(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal strSearch As String, _
                                       ByVal sbSql As StringBuilder) As StringBuilder

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim strAry() As String

        Try
            ' 検索文字列の分割
            strAry = CommonHBK.CommonLogicHBK.GetSearchStringList(strSearch, CommonDeclareHBK.SPLIT_MODE_AND)

            For loopIndex As Integer = 0 To strAry.Length - 1 Step 1
                If (loopIndex = 0) Then
                    sbSql.Append(" AND ( ")
                Else
                    sbSql.Append(" AND ")
                End If
                sbSql.Append(" A.FreeWordAimai ")
                sbSql.Append(" like ").Append(":FreeWord" & loopIndex)
            Next

            If strAry.Length > 0 Then
                sbSql.Append(" ) " & vbCrLf)
            End If

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return sbSql

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            puErrMsg = HBK_E001 & ex.Message
            Return sbSql
        End Try

    End Function

    ''' <summary>
    ''' フリーテキスト条件作成処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgsqlDataAdapter</param>
    ''' <param name="strSearch">[IN]フリーテキスト</param>
    ''' <param name="sbSql">[IN]SQL文字列配列</param>
    ''' <returns>boolean  取得状況 　true  正常終了  false  異常終了</returns>
    ''' <remarks>フリーテキスト条件をSQL用に作成して返す
    ''' <para>作成情報：2012/07/18 t.fukuo
    ''' </para></remarks>
    Private Function CreateSqlFreeText(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal strSearch As String, _
                                       ByVal sbSql As StringBuilder) As StringBuilder

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        Dim commonLogicHBK As New CommonLogicHBK

        Dim strAry() As String

        Try
            ' 検索文字列の分割
            strAry = CommonHBK.CommonLogicHBK.GetSearchStringList(strSearch, CommonDeclareHBK.SPLIT_MODE_AND)

            For loopIndex As Integer = 0 To strAry.Length - 1 Step 1
                If (loopIndex = 0) Then
                    sbSql.Append(" AND ( ")
                Else
                    sbSql.Append(" AND ")
                End If
                sbSql.Append(" A.BikoAimai ")
                sbSql.Append(" like ").Append(":FreeText" & loopIndex)
            Next
            If (strAry.Length > 0) Then
                sbSql.Append(" ) " & vbCrLf)
            End If

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return sbSql

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            puErrMsg = HBK_E001 & ex.Message
            Return sbSql
        End Try

    End Function

    ''' <summary>
    ''' 文書配布先条件作成処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgsqlDataAdapter</param>
    ''' <param name="strSearch">[IN]文書配布先</param>
    ''' <param name="sbSql">[IN]SQL文字列配列</param>
    ''' <returns>boolean  取得状況 　true  正常終了  false  異常終了</returns>
    ''' <remarks>文書配布先条件をSQL用に作成して返す
    ''' <para>作成情報：2012/07/18 t.fukuo
    ''' </para></remarks>
    Private Function CreateSqlDocAdd(ByRef Adapter As NpgsqlDataAdapter, _
                                     ByVal strSearch As String, _
                                     ByVal sbSql As StringBuilder) As StringBuilder

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim strAry() As String

        Try
            ' 検索文字列の分割
            strAry = CommonHBK.CommonLogicHBK.GetSearchStringList(strSearch, CommonDeclareHBK.SPLIT_MODE_AND)

            For loopIndex As Integer = 0 To strAry.Length - 1 Step 1

                If (loopIndex = 0) Then
                    sbSql.Append(" AND ( ")
                Else
                    sbSql.Append(" AND ")
                End If

                sbSql.Append(" D.ShareteamNMAimai ")
                sbSql.Append(" like ").Append(":ShareteamNM" & loopIndex)

            Next

            If strAry.Length > 0 Then
                sbSql.Append(" ) " & vbCrLf)
            End If

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return sbSql

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            puErrMsg = HBK_E001 & ex.Message
            Return sbSql
        End Try

    End Function

End Class
