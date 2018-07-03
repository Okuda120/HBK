Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' システム登録画面Sqlクラス
''' </summary>
''' <remarks>システム登録画面のSQLの作成・設定を行う
''' <para>作成情報：2012/06/13 t.fukuo
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKB0401

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK


    'SQL文宣言

    'CI共通情報取得（SELECT）SQL
    Private strSelectCIInfoSql As String = "SELECT " & vbCrLf & _
                                           " ct.CINmb " & vbCrLf & _
                                           ",ct.CIKbnCD " & vbCrLf & _
                                           ",ct.KindCD " & vbCrLf & _
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
                                           ",ct.CIKbnCD " & vbCrLf & _
                                           ",ct.KindCD " & vbCrLf & _
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
                                           "FROM CI_INFO_RTB ct " & vbCrLf & _
                                           "LEFT JOIN CI_SYS_RTB cst ON ct.CINmb = cst.CINmb AND ct.RirekiNo = cst.RirekiNo " & vbCrLf & _
                                           "LEFT JOIN GRP_MTB gm ON ct.CIOwnerCD = gm.GroupCD " & vbCrLf & _
                                           "WHERE ct.CINmb = :CINmb " & vbCrLf & _
                                           "  AND ct.RirekiNo = :RirekiNo "


    'ノウハウURL取得（SELECT）SQL
    Private strSelectKnowHowUrlSql As String = "SELECT " & vbCrLf & _
                                               " kt.Url " & vbCrLf & _
                                               ",kt.UrlNaiyo " & vbCrLf & _
                                               "FROM KNOWHOWURL_TB kt " & vbCrLf & _
                                               "WHERE kt.CINmb = :CINmb " & vbCrLf & _
                                               "ORDER BY kt.RowNmb "


    'ノウハウURL履歴取得（SELECT）SQL
    Private strSelectKnowHowUrlRSql As String = "SELECT " & vbCrLf & _
                                               " kt.Url " & vbCrLf & _
                                               ",kt.UrlNaiyo " & vbCrLf & _
                                               "FROM KNOWHOWURL_RTB kt " & vbCrLf & _
                                               "WHERE kt.CINmb = :CINmb " & vbCrLf & _
                                               "  AND kt.RirekiNo = :RirekiNo " & vbCrLf & _
                                               "ORDER BY kt.RowNmb "

    'サーバー管理情報取得（SELECT）SQL
    Private strSelectMngSrvSql As String = "SELECT " & vbCrLf & _
                                           " st.ManageNmb " & vbCrLf & _
                                           ",st.ManageNmbNaiyo " & vbCrLf & _
                                           "FROM SRVMNG_TB st " & vbCrLf & _
                                           "WHERE st.CINmb = :CINmb " & vbCrLf & _
                                           "ORDER BY st.RowNmb "

    'サーバー管理情報履歴取得（SELECT）SQL
    Private strSelectMngSrvRSql As String = "SELECT " & vbCrLf & _
                                           " st.ManageNmb " & vbCrLf & _
                                           ",st.ManageNmbNaiyo " & vbCrLf & _
                                           "FROM SRVMNG_RTB st " & vbCrLf & _
                                           "WHERE st.CINmb = :CINmb " & vbCrLf & _
                                           "  AND st.RirekiNo = :RirekiNo " & vbCrLf & _
                                           "ORDER BY st.RowNmb "

    '関係者情報取得（SELECT）SQL
    'Private strSelectRelationSql As String = "SELECT " & vbCrLf & _
    '                                         " t.RelationKbn " & vbCrLf & _
    '                                         ",t.RelationID " & vbCrLf & _
    '                                         ",t.GroupNM " & vbCrLf & _
    '                                         ",t.HBKUsrNM " & vbCrLf & _
    '                                         ",t.RelationGrpCD " & vbCrLf & _
    '                                         "FROM " & vbCrLf & _
    '                                         "( " & vbCrLf & _
    '                                         "  SELECT " & vbCrLf & _
    '                                         "    kt.RelationKbn AS RelationKbn " & vbCrLf & _
    '                                         "   ,kt.RelationGrpCD  AS RelationID " & vbCrLf & _
    '                                         "   ,gm.GroupNM	AS GroupNM " & vbCrLf & _
    '                                         "   ,''		AS HBKUsrNM " & vbCrLf & _
    '                                         "   ,gm.Sort	AS Sort_Grp " & vbCrLf & _
    '                                         "   ,1		AS Sort_Usr " & vbCrLf & _
    '                                         "   ,'' AS RelationGrpCD " & vbCrLf & _
    '                                         "  FROM KANKEI_TB kt " & vbCrLf & _
    '                                         "  LEFT JOIN GRP_MTB gm ON kt.RelationGrpCD = gm.GroupCD " & vbCrLf & _
    '                                         "  WHERE kt.CINmb = :CINmb " & vbCrLf & _
    '                                         "    AND kt.RelationKbn = :KbnGrp " & vbCrLf & _
    '                                         "  UNION ALL " & vbCrLf & _
    '                                         "  SELECT " & vbCrLf & _
    '                                         "    kt.RelationKbn " & vbCrLf & _
    '                                         "   ,kt.RelationUsrID AS RelationID " & vbCrLf & _
    '                                         "   ,gm.GroupNM " & vbCrLf & _
    '                                         "   ,hm.HBKUsrNM " & vbCrLf & _
    '                                         "   ,gm.Sort " & vbCrLf & _
    '                                         "   ,hm.Sort " & vbCrLf & _
    '                                         "   ,kt.RelationGrpCD  AS RelationGrpCD " & vbCrLf & _
    '                                         "  FROM KANKEI_TB kt " & vbCrLf & _
    '                                         "  LEFT JOIN HBKUSR_MTB hm ON hm.HBKUsrID = kt.RelationUsrID " & vbCrLf & _
    '                                         "  LEFT JOIN SZK_MTB sm ON hm.HBKUsrID = sm.HBKUsrID AND sm.GroupCD = kt.RelationGrpCD " & vbCrLf & _
    '                                         "  LEFT JOIN GRP_MTB gm ON sm.GroupCD = gm.GroupCD " & vbCrLf & _
    '                                         "  WHERE kt.CINmb = :CINmb " & vbCrLf & _
    '                                         "    AND kt.RelationKbn = :KbnUsr " & vbCrLf & _
    '                                         ") t " & vbCrLf & _
    '                                         "ORDER BY t.Sort_Grp, t.Sort_Usr "

    '関係者情報取得（SELECT）SQL
    Private strSelectRelationSql As String = "SELECT " & vbCrLf & _
                                         " t.RelationKbn " & vbCrLf & _
                                         ",t.RelationID " & vbCrLf & _
                                         ",t.GroupNM " & vbCrLf & _
                                         ",t.HBKUsrNM " & vbCrLf & _
                                         "FROM " & vbCrLf & _
                                         "( " & vbCrLf & _
                                         "  SELECT " & vbCrLf & _
                                         "    kt.RelationKbn AS RelationKbn " & vbCrLf & _
                                         "   ,kt.RelationID  AS RelationID " & vbCrLf & _
                                         "   ,gm.GroupNM	AS GroupNM " & vbCrLf & _
                                         "   ,''		AS HBKUsrNM " & vbCrLf & _
                                         "   ,gm.Sort	AS Sort_Grp " & vbCrLf & _
                                         "   ,1		AS Sort_Usr " & vbCrLf & _
                                         "  FROM KANKEI_TB kt " & vbCrLf & _
                                         "  LEFT JOIN GRP_MTB gm ON kt.RelationID = gm.GroupCD " & vbCrLf & _
                                         "  WHERE kt.CINmb = :CINmb " & vbCrLf & _
                                         "    AND kt.RelationKbn = :KbnGrp " & vbCrLf & _
                                         "  UNION ALL " & vbCrLf & _
                                         "  SELECT " & vbCrLf & _
                                         "    kt.RelationKbn " & vbCrLf & _
                                         "   ,kt.RelationID " & vbCrLf & _
                                         "   ,'' " & vbCrLf & _
                                         "   ,hm.HBKUsrNM " & vbCrLf & _
                                         "   ,1 " & vbCrLf & _
                                         "   ,hm.Sort " & vbCrLf & _
                                         "  FROM KANKEI_TB kt " & vbCrLf & _
                                         "  LEFT JOIN HBKUSR_MTB hm ON hm.HBKUsrID = kt.RelationID " & vbCrLf & _
                                         "  WHERE kt.CINmb = :CINmb " & vbCrLf & _
                                         "    AND kt.RelationKbn = :KbnUsr " & vbCrLf & _
                                         ") t " & vbCrLf & _
                                         "ORDER BY t.Sort_Grp, t.Sort_Usr "


    ''関係者情報取得（SELECT）SQL
    'Private strSelectRelationSql As String = "SELECT " & vbCrLf & _
    '                                     " t.RelationKbn " & vbCrLf & _
    '                                     ",t.RelationID " & vbCrLf & _
    '                                     ",t.GroupNM " & vbCrLf & _
    '                                     ",t.HBKUsrNM " & vbCrLf & _
    '                                     "FROM " & vbCrLf & _
    '                                     "( " & vbCrLf & _
    '                                     "  SELECT " & vbCrLf & _
    '                                     "    kt.RelationKbn AS RelationKbn " & vbCrLf & _
    '                                     "   ,kt.RelationID  AS RelationID " & vbCrLf & _
    '                                     "   ,gm.GroupNM	AS GroupNM " & vbCrLf & _
    '                                     "   ,''		AS HBKUsrNM " & vbCrLf & _
    '                                     "   ,gm.Sort	AS Sort_Grp " & vbCrLf & _
    '                                     "   ,1		AS Sort_Usr " & vbCrLf & _
    '                                     "  FROM KANKEI_TB kt " & vbCrLf & _
    '                                     "  LEFT JOIN GRP_MTB gm ON kt.RelationID = gm.GroupCD " & vbCrLf & _
    '                                     "  WHERE kt.CINmb = :CINmb " & vbCrLf & _
    '                                     "    AND kt.RelationKbn = :KbnGrp " & vbCrLf & _
    '                                     "  UNION ALL " & vbCrLf & _
    '                                     "  SELECT " & vbCrLf & _
    '                                     "    kt.RelationKbn " & vbCrLf & _
    '                                     "   ,kt.RelationID " & vbCrLf & _
    '                                     "   ,gm.GroupNM " & vbCrLf & _
    '                                     "   ,hm.HBKUsrNM " & vbCrLf & _
    '                                     "   ,gm.Sort " & vbCrLf & _
    '                                     "   ,hm.Sort " & vbCrLf & _
    '                                     "  FROM KANKEI_TB kt " & vbCrLf & _
    '                                     "  LEFT JOIN HBKUSR_MTB hm ON hm.HBKUsrID = kt.RelationID " & vbCrLf & _
    '                                     "  LEFT JOIN SZK_MTB sm ON hm.HBKUsrID = sm.HBKUsrID " & vbCrLf & _
    '                                     "  LEFT JOIN GRP_MTB gm ON sm.GroupCD = gm.GroupCD " & vbCrLf & _
    '                                     "  WHERE kt.CINmb = :CINmb " & vbCrLf & _
    '                                     "    AND kt.RelationKbn = :KbnUsr " & vbCrLf & _
    '                                     ") t " & vbCrLf & _
    '                                     "ORDER BY t.Sort_Grp, t.Sort_Usr "

    ''関係者情報履歴取得（SELECT）SQL
    'Private strSelectRelationRSql As String = "SELECT " & vbCrLf & _
    '                                         " t.RelationKbn " & vbCrLf & _
    '                                         ",t.RelationID " & vbCrLf & _
    '                                         ",t.GroupNM " & vbCrLf & _
    '                                         ",t.HBKUsrNM " & vbCrLf & _
    '                                         ",t.RelationGrpCD " & vbCrLf & _
    '                                         "FROM " & vbCrLf & _
    '                                         "( " & vbCrLf & _
    '                                         "  SELECT " & vbCrLf & _
    '                                         "    kt.RelationKbn AS RelationKbn " & vbCrLf & _
    '                                         "   ,kt.RelationGrpCD  AS RelationID " & vbCrLf & _
    '                                         "   ,gm.GroupNM	AS GroupNM " & vbCrLf & _
    '                                         "   ,''		AS HBKUsrNM " & vbCrLf & _
    '                                         "   ,gm.Sort	AS Sort_Grp " & vbCrLf & _
    '                                         "   ,1		AS Sort_Usr " & vbCrLf & _
    '                                         "   ,'' AS RelationGrpCD " & vbCrLf & _
    '                                         "  FROM KANKEI_RTB kt " & vbCrLf & _
    '                                         "  LEFT JOIN GRP_MTB gm ON kt.RelationGrpCD = gm.GroupCD " & vbCrLf & _
    '                                         "  WHERE kt.CINmb = :CINmb " & vbCrLf & _
    '                                         "    AND kt.RirekiNo = :RirekiNo " & vbCrLf & _
    '                                         "    AND kt.RelationKbn = :KbnGrp " & vbCrLf & _
    '                                         "  UNION ALL " & vbCrLf & _
    '                                         "  SELECT " & vbCrLf & _
    '                                         "    kt.RelationKbn " & vbCrLf & _
    '                                         "   ,kt.RelationUsrID  AS RelationID" & vbCrLf & _
    '                                         "   ,gm.GroupNM " & vbCrLf & _
    '                                         "   ,hm.HBKUsrNM " & vbCrLf & _
    '                                         "   ,gm.Sort " & vbCrLf & _
    '                                         "   ,hm.Sort " & vbCrLf & _
    '                                         "   ,kt.RelationGrpCD  AS RelationGrpCD " & vbCrLf & _
    '                                         "  FROM KANKEI_RTB kt " & vbCrLf & _
    '                                         "  LEFT JOIN HBKUSR_MTB hm ON hm.HBKUsrID = kt.RelationUsrID " & vbCrLf & _
    '                                         "  LEFT JOIN SZK_MTB sm ON hm.HBKUsrID = sm.HBKUsrID AND sm.GroupCD = kt.RelationGrpCD " & vbCrLf & _
    '                                         "  LEFT JOIN GRP_MTB gm ON sm.GroupCD = gm.GroupCD " & vbCrLf & _
    '                                         "  WHERE kt.CINmb = :CINmb " & vbCrLf & _
    '                                         "    AND kt.RirekiNo = :RirekiNo " & vbCrLf & _
    '                                         "    AND kt.RelationKbn = :KbnUsr " & vbCrLf & _
    '                                         ") t " & vbCrLf & _
    '                                         "ORDER BY t.Sort_Grp, t.Sort_Usr "

    ''関係者情報履歴取得（SELECT）SQL
    'Private strSelectRelationRSql As String = "SELECT " & vbCrLf & _
    '                                         " t.RelationKbn " & vbCrLf & _
    '                                         ",t.RelationID " & vbCrLf & _
    '                                         ",t.GroupNM " & vbCrLf & _
    '                                         ",t.HBKUsrNM " & vbCrLf & _
    '                                         "FROM " & vbCrLf & _
    '                                         "( " & vbCrLf & _
    '                                         "  SELECT " & vbCrLf & _
    '                                         "    kt.RelationKbn AS RelationKbn " & vbCrLf & _
    '                                         "   ,kt.RelationID  AS RelationID " & vbCrLf & _
    '                                         "   ,gm.GroupNM	AS GroupNM " & vbCrLf & _
    '                                         "   ,''		AS HBKUsrNM " & vbCrLf & _
    '                                         "   ,gm.Sort	AS Sort_Grp " & vbCrLf & _
    '                                         "   ,1		AS Sort_Usr " & vbCrLf & _
    '                                         "  FROM KANKEI_RTB kt " & vbCrLf & _
    '                                         "  LEFT JOIN GRP_MTB gm ON kt.RelationID = gm.GroupCD " & vbCrLf & _
    '                                         "  WHERE kt.CINmb = :CINmb " & vbCrLf & _
    '                                         "    AND kt.RirekiNo = :RirekiNo " & vbCrLf & _
    '                                         "    AND kt.RelationKbn = :KbnGrp " & vbCrLf & _
    '                                         "  UNION ALL " & vbCrLf & _
    '                                         "  SELECT " & vbCrLf & _
    '                                         "    kt.RelationKbn " & vbCrLf & _
    '                                         "   ,kt.RelationID " & vbCrLf & _
    '                                         "   ,gm.GroupNM " & vbCrLf & _
    '                                         "   ,hm.HBKUsrNM " & vbCrLf & _
    '                                         "   ,gm.Sort " & vbCrLf & _
    '                                         "   ,hm.Sort " & vbCrLf & _
    '                                         "  FROM KANKEI_RTB kt " & vbCrLf & _
    '                                         "  LEFT JOIN HBKUSR_MTB hm ON hm.HBKUsrID = kt.RelationID " & vbCrLf & _
    '                                         "  LEFT JOIN SZK_MTB sm ON hm.HBKUsrID = sm.HBKUsrID " & vbCrLf & _
    '                                         "  LEFT JOIN GRP_MTB gm ON sm.GroupCD = gm.GroupCD " & vbCrLf & _
    '                                         "  WHERE kt.CINmb = :CINmb " & vbCrLf & _
    '                                         "    AND kt.RirekiNo = :RirekiNo " & vbCrLf & _
    '                                         "    AND kt.RelationKbn = :KbnUsr " & vbCrLf & _
    '                                         ") t " & vbCrLf & _
    '                                         "ORDER BY t.Sort_Grp, t.Sort_Usr "

    '関係者情報履歴取得（SELECT）SQL
    Private strSelectRelationRSql As String = "SELECT " & vbCrLf & _
                                             " t.RelationKbn " & vbCrLf & _
                                             ",t.RelationID " & vbCrLf & _
                                             ",t.GroupNM " & vbCrLf & _
                                             ",t.HBKUsrNM " & vbCrLf & _
                                             "FROM " & vbCrLf & _
                                             "( " & vbCrLf & _
                                             "  SELECT " & vbCrLf & _
                                             "    kt.RelationKbn AS RelationKbn " & vbCrLf & _
                                             "   ,kt.RelationID  AS RelationID " & vbCrLf & _
                                             "   ,gm.GroupNM	AS GroupNM " & vbCrLf & _
                                             "   ,''		AS HBKUsrNM " & vbCrLf & _
                                             "   ,gm.Sort	AS Sort_Grp " & vbCrLf & _
                                             "   ,1		AS Sort_Usr " & vbCrLf & _
                                             "  FROM KANKEI_RTB kt " & vbCrLf & _
                                             "  LEFT JOIN GRP_MTB gm ON kt.RelationID = gm.GroupCD " & vbCrLf & _
                                             "  WHERE kt.CINmb = :CINmb " & vbCrLf & _
                                             "    AND kt.RirekiNo = :RirekiNo " & vbCrLf & _
                                             "    AND kt.RelationKbn = :KbnGrp " & vbCrLf & _
                                             "  UNION ALL " & vbCrLf & _
                                             "  SELECT " & vbCrLf & _
                                             "    kt.RelationKbn " & vbCrLf & _
                                             "   ,kt.RelationID " & vbCrLf & _
                                             "   ,'' " & vbCrLf & _
                                             "   ,hm.HBKUsrNM " & vbCrLf & _
                                             "   ,1 " & vbCrLf & _
                                             "   ,hm.Sort " & vbCrLf & _
                                             "  FROM KANKEI_RTB kt " & vbCrLf & _
                                             "  LEFT JOIN HBKUSR_MTB hm ON hm.HBKUsrID = kt.RelationID " & vbCrLf & _
                                             "  WHERE kt.CINmb = :CINmb " & vbCrLf & _
                                             "    AND kt.RirekiNo = :RirekiNo " & vbCrLf & _
                                             "    AND kt.RelationKbn = :KbnUsr " & vbCrLf & _
                                             ") t " & vbCrLf & _
                                             "ORDER BY t.Sort_Grp, t.Sort_Usr "

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
                                                 "  AND ct.CIKbnCD = :CIKbnCD " & vbCrLf


    '新規履歴番号取得（SELECT）SQL
    Private strSelectNewRirekiNoSql As String = "SELECT " & vbCrLf & _
                                                " COALESCE(MAX(ct.RirekiNo),0)+1 AS RirekiNo " & vbCrLf & _
                                                "FROM CI_INFO_RTB ct " & vbCrLf & _
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

    'CIシステム新規登録（INSERT）SQL
    Private strInsertCISystemSql As String = "INSERT INTO CI_SYS_TB ( " & vbCrLf & _
                                             " CINmb " & vbCrLf & _
                                             ",InfShareteamNM " & vbCrLf & _
                                             ",RegDT " & vbCrLf & _
                                             ",RegGrpCD " & vbCrLf & _
                                             ",RegID " & vbCrLf & _
                                             ",UpdateDT " & vbCrLf & _
                                             ",UpGrpCD " & vbCrLf & _
                                             ",UpdateID " & vbCrLf & _
                                             ") " & vbCrLf & _
                                             "VALUES ( " & vbCrLf & _
                                             " :CINmb " & vbCrLf & _
                                             ",:InfShareteamNM " & vbCrLf & _
                                             ",:RegDT " & vbCrLf & _
                                             ",:RegGrpCD " & vbCrLf & _
                                             ",:RegID " & vbCrLf & _
                                             ",:UpdateDT " & vbCrLf & _
                                             ",:UpGrpCD " & vbCrLf & _
                                             ",:UpdateID " & vbCrLf & _
                                             ") "

    'CIシステム履歴新規登録（INSERT）SQL
    Private strInsertCISystemRSql As String = "INSERT INTO CI_SYS_RTB ( " & vbCrLf & _
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

    'CIシステム更新（UPDATE）SQL
    Private strUpdateCISystemSql As String = "UPDATE CI_SYS_TB SET " & vbCrLf & _
                                             " InfShareteamNM = :InfShareteamNM " & vbCrLf & _
                                             ",UpdateDT       = :UpdateDT " & vbCrLf & _
                                             ",UpGrpCD        = :UpGrpCD " & vbCrLf & _
                                             ",UpdateID       = :UpdateID " & vbCrLf & _
                                             "WHERE CINmb=:CINmb "

    'ノウハウURL新規登録（INSERT）SQL
    Private strInsertKnowHowUrlSql As String = "INSERT INTO KNOWHOWURL_TB ( " & vbCrLf & _
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
                                               "VALUES ( " & vbCrLf & _
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

    'ノウハウURL履歴新規登録（INSERT）SQL
    Private strInsertKnowHowUrlRSql As String = "INSERT INTO KNOWHOWURL_RTB ( " & vbCrLf & _
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

    'ノウハウURL削除（DELETE）SQL
    Private strDeleteKnowHowUrlSql As String = "DELETE FROM KNOWHOWURL_TB " & vbCrLf & _
                                               "WHERE CINmb=:CINmb "

    'サーバー管理情報新規登録（INSERT）SQL
    Private strInsertMngSrvSql As String = "INSERT INTO SRVMNG_TB ( " & vbCrLf & _
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
                                           "VALUES ( " & vbCrLf & _
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

    'サーバー管理情報履歴新規登録（INSERT）SQL
    Private strInsertMngSrvRSql As String = "INSERT INTO SRVMNG_RTB ( " & vbCrLf & _
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

    'サーバー管理情報削除（DELETE）SQL
    Private strDeleteMngSrvSql As String = "DELETE FROM SRVMNG_TB " & vbCrLf & _
                                           "WHERE CINmb=:CINmb "

    ''関係者新規登録（INSERT）SQL
    'Private strInsertRelationSql As String = "INSERT INTO KANKEI_TB ( " & vbCrLf & _
    '                                         " CINmb " & vbCrLf & _
    '                                         ",RelationKbn " & vbCrLf & _
    '                                         ",RelationGrpCD " & vbCrLf & _
    '                                         ",RelationUsrID " & vbCrLf & _
    '                                         ",RegDT " & vbCrLf & _
    '                                         ",RegGrpCD " & vbCrLf & _
    '                                         ",RegID " & vbCrLf & _
    '                                         ",UpdateDT " & vbCrLf & _
    '                                         ",UpGrpCD " & vbCrLf & _
    '                                         ",UpdateID " & vbCrLf & _
    '                                         ") " & vbCrLf & _
    '                                         "VALUES ( " & vbCrLf & _
    '                                         " :CINmb " & vbCrLf & _
    '                                         ",:RelationKbn " & vbCrLf & _
    '                                         ",:RelationGrpCD " & vbCrLf & _
    '                                         ",:RelationUsrID " & vbCrLf & _
    '                                         ",:RegDT " & vbCrLf & _
    '                                         ",:RegGrpCD " & vbCrLf & _
    '                                         ",:RegID " & vbCrLf & _
    '                                         ",:UpdateDT " & vbCrLf & _
    '                                         ",:UpGrpCD " & vbCrLf & _
    '                                         ",:UpdateID " & vbCrLf & _
    '                                         ") "

    ''関係者履歴新規登録（INSERT）SQL
    'Private strInsertRelationRSql As String = "INSERT INTO KANKEI_RTB ( " & vbCrLf & _
    '                                         " CINmb " & vbCrLf & _
    '                                         ",RirekiNo " & vbCrLf & _
    '                                         ",RelationKbn " & vbCrLf & _
    '                                         ",RelationGrpCD " & vbCrLf & _
    '                                         ",RelationUsrID " & vbCrLf & _
    '                                         ",RegDT " & vbCrLf & _
    '                                         ",RegGrpCD " & vbCrLf & _
    '                                         ",RegID " & vbCrLf & _
    '                                         ",UpdateDT " & vbCrLf & _
    '                                         ",UpGrpCD " & vbCrLf & _
    '                                         ",UpdateID " & vbCrLf & _
    '                                         ") " & vbCrLf & _
    '                                         "SELECT " & vbCrLf & _
    '                                         " kt.CINmb " & vbCrLf & _
    '                                         ",:RirekiNo " & vbCrLf & _
    '                                         ",kt.RelationKbn " & vbCrLf & _
    '                                         ",kt.RelationGrpCD " & vbCrLf & _
    '                                         ",kt.RelationUsrID " & vbCrLf & _
    '                                         ",kt.RegDT " & vbCrLf & _
    '                                         ",kt.RegGrpCD " & vbCrLf & _
    '                                         ",kt.RegID " & vbCrLf & _
    '                                         ",kt.UpdateDT " & vbCrLf & _
    '                                         ",kt.UpGrpCD " & vbCrLf & _
    '                                         ",kt.UpdateID " & vbCrLf & _
    '                                         "FROM KANKEI_TB kt " & vbCrLf & _
    '                                         "WHERE kt.CINmb=:CINmb "

    '関係者新規登録（INSERT）SQL
    Private strInsertRelationSql As String = "INSERT INTO KANKEI_TB ( " & vbCrLf & _
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
                                             "VALUES ( " & vbCrLf & _
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

    '関係者履歴新規登録（INSERT）SQL
    Private strInsertRelationRSql As String = "INSERT INTO KANKEI_RTB ( " & vbCrLf & _
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

    '関係者削除（DELETE）SQL
    Private strDeleteRelationSql As String = "DELETE FROM KANKEI_TB " & vbCrLf & _
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

    ''' <summary>
    ''' 【編集／参照モード】CI共通情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCIInfoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectCIInfoSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CI番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb                      'CI番号
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
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCIInfoRSql(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectCIInfoRSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CI番号
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))     '履歴番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb                      'CI番号
                .Parameters("RirekiNo").Value = dataHBKB0401.PropIntRirekiNo                '履歴番号
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
    ''' 【編集／参照モード】ノウハウURL情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ノウハウURL取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectKnowHowUrlSql(ByRef Adapter As NpgsqlDataAdapter, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectKnowHowUrlSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CI番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb                      'CI番号
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
    ''' 【履歴モード】ノウハウURL履歴情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ノウハウURL取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectKnowHowUrlRSql(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectKnowHowUrlRSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CI番号
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))     '履歴番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb                      'CI番号
                .Parameters("RirekiNo").Value = dataHBKB0401.PropIntRirekiNo                '履歴番号
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
    ''' 【編集／参照モード】サーバー管理情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サーバー管理情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectMngSrvSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectMngSrvSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CI番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb                      'CI番号
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
    ''' 【履歴モード】サーバー管理情報履歴取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サーバー管理情報履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectMngSrvRSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectMngSrvRSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CI番号
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))     '履歴番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb                      'CI番号
                .Parameters("RirekiNo").Value = dataHBKB0401.PropIntRirekiNo                '履歴番号
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
    ''' 【編集／参照モード】関係者情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>関係者情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectRelationSql(ByRef Adapter As NpgsqlDataAdapter, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectRelationSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CI番号
                .Add(New NpgsqlParameter("KbnGrp", NpgsqlTypes.NpgsqlDbType.Varchar))       '区分：グループ
                .Add(New NpgsqlParameter("KbnUsr", NpgsqlTypes.NpgsqlDbType.Varchar))       '区分：ユーザー
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb                      'CI番号
                .Parameters("KbnGrp").Value = KBN_GROUP                                     '区分：グループ
                .Parameters("KbnUsr").Value = KBN_USER                                      '区分：ユーザー
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
    ''' 【履歴モード】関係者履歴取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>関係者履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectRelationRSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectRelationRSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CI番号
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))     '履歴番号
                .Add(New NpgsqlParameter("KbnGrp", NpgsqlTypes.NpgsqlDbType.Varchar))       '区分：グループ
                .Add(New NpgsqlParameter("KbnUsr", NpgsqlTypes.NpgsqlDbType.Varchar))       '区分：ユーザー
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb                      'CI番号
                .Parameters("RirekiNo").Value = dataHBKB0401.PropIntRirekiNo                '履歴番号
                .Parameters("KbnGrp").Value = KBN_GROUP                                     '区分：グループ
                .Parameters("KbnUsr").Value = KBN_USER                                      '区分：ユーザー
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
    ''' 【編集／参照モード】原因リンク取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>原因リンク取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCauseLinkSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0401 As DataHBKB0401) As Boolean

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
                .Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb                           'CI番号
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
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>原因リンク履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCauseLinkRSql(ByRef Adapter As NpgsqlDataAdapter, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB0401 As DataHBKB0401) As Boolean

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
                .Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb                           'CI番号
                .Parameters("RirekiNo").Value = dataHBKB0401.PropIntRirekiNo                     '履歴番号
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
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>登録理由履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectRegReasonSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectRegReasonSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))

            'バインド変数に値をセット
            Adapter.SelectCommand.Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb


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
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>登録理由履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectRegReasonRSql(ByRef Adapter As NpgsqlDataAdapter, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectRegReasonRSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CI番号
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))     '履歴番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb                      'CI番号
                .Parameters("RirekiNo").Value = dataHBKB0401.PropIntRirekiNo                '履歴番号
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
    ''' 【共通】同じキー項目（分類１、分類２、名称）のデータ有無取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>同じキー項目（分類１、分類２、名称）のデータ有無取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCountSameKeySql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKB0401 As DataHBKB0401) As Boolean

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
                .Parameters("Class1").Value = dataHBKB0401.PropTxtClass1.Text               '分類１
                .Parameters("Class2").Value = dataHBKB0401.PropTxtClass2.Text               '分類２
                .Parameters("CINM").Value = dataHBKB0401.PropTxtCINM.Text                   '名称
                .Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb                      'CI番号
                '[Add] 2012/08/02 y.ikushima START
                .Parameters("CIKbnCD").Value = CI_TYPE_SYSTEM                               'CI種別(システム）
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
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規CI番号、サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewCINmbAndSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                   ByVal Cn As NpgsqlConnection, _
                                                   ByVal dataHBKB0401 As DataHBKB0401) As Boolean

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
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/27 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB0401 As DataHBKB0401) As Boolean

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
    ''' 【新規登録モード】CI共通情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCIInfoSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB0401 As DataHBKB0401) As Boolean

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
                .Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb                          'CI番号
                .Parameters("CIKbnCD").Value = CI_TYPE_SYSTEM                                   'CI種別CD
                .Parameters("KindCD").Value = dataHBKB0401.PropCmbKind.SelectedValue            '種別CD
                .Parameters("Num").Value = dataHBKB0401.PropIntCINmb                            '番号
                .Parameters("CIStatusCD").Value = dataHBKB0401.PropCmbCIStatus.SelectedValue    'ステータスCD
                .Parameters("Class1").Value = dataHBKB0401.PropTxtClass1.Text                   '分類１
                .Parameters("Class2").Value = dataHBKB0401.PropTxtClass2.Text                   '分類２
                .Parameters("CINM").Value = dataHBKB0401.PropTxtCINM.Text                       '名称

                'CIオーナー名が入力されている場合のみCIオーナーCDに値をセット
                If dataHBKB0401.PropTxtCIOwnerNM.Text.Trim() <> "" Then
                    .Parameters("CIOwnerCD").Value = dataHBKB0401.PropLblCIOwnerCD.Text         'CIオーナーCD
                Else
                    .Parameters("CIOwnerCD").Value = ""
                End If

                .Parameters("CINaiyo").Value = dataHBKB0401.PropTxtCINaiyo.Text                 '説明
                .Parameters("BIko1").Value = dataHBKB0401.PropTxtBIko1.Text                     'フリーテキスト１
                .Parameters("Biko2").Value = dataHBKB0401.PropTxtBIko2.Text                     'フリーテキスト２
                .Parameters("BIko3").Value = dataHBKB0401.PropTxtBIko3.Text                     'フリーテキスト３
                .Parameters("Biko4").Value = dataHBKB0401.PropTxtBIko4.Text                     'フリーテキスト４
                .Parameters("Biko5").Value = dataHBKB0401.PropTxtBIko5.Text                     'フリーテキスト５

                'フリーフラグ１～５
                If dataHBKB0401.PropChkFreeFlg1.Checked = True Then
                    .Parameters("FreeFlg1").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg1").Value = FREE_FLG_OFF
                End If
                If dataHBKB0401.PropChkFreeFlg2.Checked = True Then
                    .Parameters("FreeFlg2").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg2").Value = FREE_FLG_OFF
                End If
                If dataHBKB0401.PropChkFreeFlg3.Checked = True Then
                    .Parameters("FreeFlg3").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg3").Value = FREE_FLG_OFF
                End If
                If dataHBKB0401.PropChkFreeFlg4.Checked = True Then
                    .Parameters("FreeFlg4").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg4").Value = FREE_FLG_OFF
                End If
                If dataHBKB0401.PropChkFreeFlg5.Checked = True Then
                    .Parameters("FreeFlg5").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg5").Value = FREE_FLG_OFF
                End If

                'あいまい検索文字列設定
                '※フリーワード（あいまい）　：分類１～２、名称、説明を連結
                '※フリーテキスト（あいまい）：フリーテキスト１～５を連結
                strClass1Aimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0401.PropTxtClass1.Text)
                strClass2Aimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0401.PropTxtClass2.Text)
                strCINMAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0401.PropTxtCINM.Text)
                strFreeWordAimai = strClass1Aimai & strClass2Aimai & strCINMAimai & _
                                   commonLogicHBK.ChangeStringForSearch(dataHBKB0401.PropTxtCINaiyo.Text)
                strBikoAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0401.PropTxtBIko1.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKB0401.PropTxtBIko2.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKB0401.PropTxtBIko3.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKB0401.PropTxtBIko4.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKB0401.PropTxtBIko5.Text)
                .Parameters("Class1Aimai").Value = strClass1Aimai           '分類１（あいまい）
                .Parameters("Class2Aimai").Value = strClass2Aimai           '分類２（あいまい）
                .Parameters("CINMAimai").Value = strCINMAimai               '名称（あいまい）
                .Parameters("FreeWordAimai").Value = strFreeWordAimai       'フリーワード（あいまい）
                .Parameters("BikoAimai").Value = strBikoAimai               'フリーテキスト（あいまい）

                .Parameters("RegDT").Value = dataHBKB0401.PropDtmSysDate    '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD             '登録者グループCD
                .Parameters("RegID").Value = PropUserId                     '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0401.PropDtmSysDate '最終更新日時
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
    ''' 【編集／参照／履歴モード】CI共通情報更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateCIInfoSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB0401 As DataHBKB0401) As Boolean

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
                .Parameters("CIKbnCD").Value = CI_TYPE_SYSTEM                                   'CI種別CD
                .Parameters("KindCD").Value = dataHBKB0401.PropCmbKind.SelectedValue            '種別CD
                .Parameters("Num").Value = dataHBKB0401.PropIntCINmb                            '番号
                .Parameters("CIStatusCD").Value = dataHBKB0401.PropCmbCIStatus.SelectedValue    'ステータスCD
                .Parameters("Class1").Value = dataHBKB0401.PropTxtClass1.Text                   '分類１
                .Parameters("Class2").Value = dataHBKB0401.PropTxtClass2.Text                   '分類２
                .Parameters("CINM").Value = dataHBKB0401.PropTxtCINM.Text                       '名称

                'CIオーナー名が入力されている場合のみCIオーナーCDに値をセット
                If dataHBKB0401.PropTxtCIOwnerNM.Text.Trim() <> "" Then
                    .Parameters("CIOwnerCD").Value = dataHBKB0401.PropLblCIOwnerCD.Text         'CIオーナーCD
                Else
                    .Parameters("CIOwnerCD").Value = ""
                End If

                .Parameters("CINaiyo").Value = dataHBKB0401.PropTxtCINaiyo.Text                 '説明
                .Parameters("BIko1").Value = dataHBKB0401.PropTxtBIko1.Text                     'フリーテキスト１
                .Parameters("Biko2").Value = dataHBKB0401.PropTxtBIko2.Text                     'フリーテキスト２
                .Parameters("BIko3").Value = dataHBKB0401.PropTxtBIko3.Text                     'フリーテキスト３
                .Parameters("Biko4").Value = dataHBKB0401.PropTxtBIko4.Text                     'フリーテキスト４
                .Parameters("Biko5").Value = dataHBKB0401.PropTxtBIko5.Text                     'フリーテキスト５

                'フリーフラグ１～５
                If dataHBKB0401.PropChkFreeFlg1.Checked = True Then
                    .Parameters("FreeFlg1").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg1").Value = FREE_FLG_OFF
                End If
                If dataHBKB0401.PropChkFreeFlg2.Checked = True Then
                    .Parameters("FreeFlg2").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg2").Value = FREE_FLG_OFF
                End If
                If dataHBKB0401.PropChkFreeFlg3.Checked = True Then
                    .Parameters("FreeFlg3").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg3").Value = FREE_FLG_OFF
                End If
                If dataHBKB0401.PropChkFreeFlg4.Checked = True Then
                    .Parameters("FreeFlg4").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg4").Value = FREE_FLG_OFF
                End If
                If dataHBKB0401.PropChkFreeFlg5.Checked = True Then
                    .Parameters("FreeFlg5").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg5").Value = FREE_FLG_OFF
                End If

                'あいまい検索文字列設定
                '※フリーワード（あいまい）　：分類１～２、名称、説明を連結
                '※フリーテキスト（あいまい）：フリーテキスト１～５を連結
                strClass1Aimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0401.PropTxtClass1.Text)
                strClass2Aimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0401.PropTxtClass2.Text)
                strCINMAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0401.PropTxtCINM.Text)
                strFreeWordAimai = strClass1Aimai & strClass2Aimai & strCINMAimai & _
                                   commonLogicHBK.ChangeStringForSearch(dataHBKB0401.PropTxtCINaiyo.Text)
                strBikoAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0401.PropTxtBIko1.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKB0401.PropTxtBIko2.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKB0401.PropTxtBIko3.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKB0401.PropTxtBIko4.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKB0401.PropTxtBIko5.Text)
                .Parameters("Class1Aimai").Value = strClass1Aimai           '分類１（あいまい）
                .Parameters("Class2Aimai").Value = strClass2Aimai           '分類２（あいまい）
                .Parameters("CINMAimai").Value = strCINMAimai               '名称（あいまい）
                .Parameters("FreeWordAimai").Value = strFreeWordAimai       'フリーワード（あいまい）
                .Parameters("BikoAimai").Value = strBikoAimai               'フリーテキスト（あいまい）

                .Parameters("UpdateDT").Value = dataHBKB0401.PropDtmSysDate '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD              '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                  '最終更新者ID
                .Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb      'CI番号
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
    ''' 【新規登録モード】CIシステム新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIシステム新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCISystemSql(ByRef Cmd As NpgsqlCommand, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKB0401 As DataHBKB0401) As Boolean

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
                .Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb                          'CI番号
                .Parameters("InfShareteamNM").Value = dataHBKB0401.PropTxtInfShareteamNM.Text   '情報共有先
                .Parameters("RegDT").Value = dataHBKB0401.PropDtmSysDate                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0401.PropDtmSysDate                     '最終更新日時
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
    ''' 【編集／参照／履歴モード】CIシステム更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIシステム更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateCISystemSql(ByRef Cmd As NpgsqlCommand, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateCISystemSql

            'データアダプタに、SQLのUPDATE文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("InfShareteamNM", NpgsqlTypes.NpgsqlDbType.Varchar))   '情報共有先
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))            'CI番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("InfShareteamNM").Value = dataHBKB0401.PropTxtInfShareteamNM.Text   '情報共有先
                .Parameters("UpdateDT").Value = dataHBKB0401.PropDtmSysDate                     '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                  '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                      '最終更新者ID
                .Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb                          'CI番号
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
    ''' 【共通】ノウハウURL新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ノウハウURL新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertKnowHowUrlSql(ByRef Cmd As NpgsqlCommand, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB0401 As DataHBKB0401) As Boolean

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
                .Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb                          'CI番号
                .Parameters("Url").Value = dataHBKB0401.PropRowReg.Item("Url")                  'ノウハウURL
                .Parameters("UrlNaiyo").Value = dataHBKB0401.PropRowReg.Item("UrlNaiyo")        'ノウハウURL説明
                .Parameters("RegDT").Value = dataHBKB0401.PropDtmSysDate                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0401.PropDtmSysDate                     '最終更新日時
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
    ''' 【編集／参照／履歴モード】ノウハウURL削除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ノウハウURL削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteKnowHowUrlSql(ByRef Cmd As NpgsqlCommand, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB0401 As DataHBKB0401) As Boolean

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
                .Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb                          'CI番号
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
    ''' 【共通】サーバー管理情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サーバー管理情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertMngSrvSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB0401 As DataHBKB0401) As Boolean

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
                .Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb                                  'CI番号
                .Parameters("ManageNmb").Value = dataHBKB0401.PropRowReg.Item("ManageNmb")              'サーバー管理番号
                .Parameters("ManageNmbNaiyo").Value = dataHBKB0401.PropRowReg.Item("ManageNmbNaiyo")    'サーバー管理番号説明
                .Parameters("RegDT").Value = dataHBKB0401.PropDtmSysDate                                '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                         '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                 '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0401.PropDtmSysDate                             '最終更新日時
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
    ''' 【編集／参照／履歴モード】サーバー管理情報削除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サーバー管理情報削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteMngSrvSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB0401 As DataHBKB0401) As Boolean

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
                .Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb                                  'CI番号
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
    ''' 【共通】関係者情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>関係者情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertRelationSql(ByRef Cmd As NpgsqlCommand, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKB0401 As DataHBKB0401) As Boolean

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
                '.Add(New NpgsqlParameter("RelationGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))               '関係グループID
                '[Mod] 2012/08/02 y.ikushima 関係者情報DB定義変更END
                .Add(New NpgsqlParameter("RelationID", NpgsqlTypes.NpgsqlDbType.Varchar))               '関係ユーザID
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                  '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                 '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                    '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb                                  'CI番号
                .Parameters("RelationKbn").Value = dataHBKB0401.PropRowReg.Item("RelationKbn")          '関係区分
                '関係区分がユーザの際は、ユーザIDを登録する
                .Parameters("RelationID").Value = dataHBKB0401.PropRowReg.Item("RelationID")            '関係ID
                '[Mod] 2012/08/02 y.ikushima 関係者情報DB定義変更START
                'If dataHBKB0401.PropRowReg.Item("RelationKbn") = KBN_USER Then
                '    .Parameters("RelationGrpCD").Value = dataHBKB0401.PropRowReg.Item("RelationGrpCD")            '関係グループID
                '    .Parameters("RelationUsrID").Value = dataHBKB0401.PropRowReg.Item("RelationID")             '関係ユーザID
                'Else
                '    .Parameters("RelationGrpCD").Value = dataHBKB0401.PropRowReg.Item("RelationID")             '関係グループID
                '    .Parameters("RelationUsrID").Value = ""
                'End If
                '[Mod] 2012/08/02 y.ikushima 関係者情報DB定義変更END
                .Parameters("RegDT").Value = dataHBKB0401.PropDtmSysDate                                '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                         '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                 '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0401.PropDtmSysDate                             '最終更新日時
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
    ''' 【編集／参照／履歴モード】関係者情報削除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>関係者情報削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteRelationSql(ByRef Cmd As NpgsqlCommand, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(DELETE)
            strSQL = strDeleteRelationSql

            'データアダプタに、SQLのDELETE文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb                                  'CI番号
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
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規履歴番号取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewRirekiNoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKB0401 As DataHBKB0401) As Boolean

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
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With
            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb                                  'CI番号
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
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCIInfoRSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB0401 As DataHBKB0401) As Boolean

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
                '[Mod] 2012/08/02 y.ikushima 不要バインド変数削除START
                '.Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                  '登録日時
                '.Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                 '登録者グループCD
                '.Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                    '登録者ID
                '.Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                '.Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                '.Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
                '[Mod] 2012/08/02 y.ikushima 不要バインド変数削除END
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("RirekiNo").Value = dataHBKB0401.PropIntRirekiNo                            '履歴番号
                .Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb                                  'CI番号
                '[Mod] 2012/08/02 y.ikushima 不要バインド変数削除START
                '.Parameters("RegDT").Value = dataHBKB0401.PropDtmSysDate                                '登録日時
                '.Parameters("RegGrpCD").Value = PropWorkGroupCD                                         '登録者グループCD
                '.Parameters("RegID").Value = PropUserId                                                 '登録者ID
                '.Parameters("UpdateDT").Value = dataHBKB0401.PropDtmSysDate                             '最終更新日時
                '.Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                '.Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
                '[Mod] 2012/08/02 y.ikushima 不要バインド変数削除END
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
    ''' 【共通】CIシステム履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIシステム履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCISystemRSql(ByRef Cmd As NpgsqlCommand, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0401 As DataHBKB0401) As Boolean

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
                '[Mod] 2012/08/02 y.ikushima 不要バインド変数削除START
                '.Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                  '登録日時
                '.Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                 '登録者グループCD
                '.Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                    '登録者ID
                '.Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                '.Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                '.Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
                '[Mod] 2012/08/02 y.ikushima 不要バインド変数削除END
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("RirekiNo").Value = dataHBKB0401.PropIntRirekiNo                            '履歴番号
                '[Mod] 2012/08/02 y.ikushima 不要バインド変数削除START
                '.Parameters("RegDT").Value = dataHBKB0401.PropDtmSysDate                                '登録日時
                '.Parameters("RegGrpCD").Value = PropWorkGroupCD                                         '登録者グループCD
                '.Parameters("RegID").Value = PropUserId                                                 '登録者ID
                '.Parameters("UpdateDT").Value = dataHBKB0401.PropDtmSysDate                             '最終更新日時
                '.Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                '.Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
                '[Mod] 2012/08/02 y.ikushima 不要バインド変数削除END
                .Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb                                  'CI番号
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
    ''' 【共通】ノウハウURL履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ノウハウURL履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertKnowHowUrlRSql(ByRef Cmd As NpgsqlCommand, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKB0401 As DataHBKB0401) As Boolean

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
                '[Mod] 2012/08/02 y.ikushima 不要バインド変数削除START
                '.Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                  '登録日時
                '.Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                 '登録者グループCD
                '.Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                    '登録者ID
                '.Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                '.Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                '.Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
                '[Mod] 2012/08/02 y.ikushima 不要バインド変数削除END
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("RirekiNo").Value = dataHBKB0401.PropIntRirekiNo                            '履歴番号
                '[Mod] 2012/08/02 y.ikushima 不要バインド変数削除START
                '.Parameters("RegDT").Value = dataHBKB0401.PropDtmSysDate                                '登録日時
                '.Parameters("RegGrpCD").Value = PropWorkGroupCD                                         '登録者グループCD
                '.Parameters("RegID").Value = PropUserId                                                 '登録者ID
                '.Parameters("UpdateDT").Value = dataHBKB0401.PropDtmSysDate                             '最終更新日時
                '.Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                '.Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
                '[Mod] 2012/08/02 y.ikushima 不要バインド変数削除END
                .Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb                                  'CI番号
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
    ''' 【共通】サーバー管理情報履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サーバー管理情報履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertMngSrvRSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB0401 As DataHBKB0401) As Boolean

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
                '[Mod] 2012/08/02 y.ikushima 不要バインド変数削除START
                '.Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                  '登録日時
                '.Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                 '登録者グループCD
                '.Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                    '登録者ID
                '.Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                '.Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                '.Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
                '[Mod] 2012/08/02 y.ikushima 不要バインド変数削除END
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("RirekiNo").Value = dataHBKB0401.PropIntRirekiNo                            '履歴番号
                '[Mod] 2012/08/02 y.ikushima 不要バインド変数削除START
                '.Parameters("RegDT").Value = dataHBKB0401.PropDtmSysDate                                '登録日時
                '.Parameters("RegGrpCD").Value = PropWorkGroupCD                                         '登録者グループCD
                '.Parameters("RegID").Value = PropUserId                                                 '登録者ID
                '.Parameters("UpdateDT").Value = dataHBKB0401.PropDtmSysDate                             '最終更新日時
                '.Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                '.Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
                '[Mod] 2012/08/02 y.ikushima 不要バインド変数削除END
                .Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb                                  'CI番号
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
    ''' 【共通】関係者履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>関係者履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertRelationRSql(ByRef Cmd As NpgsqlCommand, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0401 As DataHBKB0401) As Boolean

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
                '[Mod] 2012/08/02 y.ikushima 不要バインド変数削除START
                '.Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                  '登録日時
                '.Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                 '登録者グループCD
                '.Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                    '登録者ID
                '.Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                '.Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                '.Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
                '[Mod] 2012/08/02 y.ikushima 不要バインド変数削除END
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("RirekiNo").Value = dataHBKB0401.PropIntRirekiNo                            '履歴番号
                '[Mod] 2012/08/02 y.ikushima 不要バインド変数削除START
                '.Parameters("RegDT").Value = dataHBKB0401.PropDtmSysDate                                '登録日時
                '.Parameters("RegGrpCD").Value = PropWorkGroupCD                                         '登録者グループCD
                '.Parameters("RegID").Value = PropUserId                                                 '登録者ID
                '.Parameters("UpdateDT").Value = dataHBKB0401.PropDtmSysDate                             '最終更新日時
                '.Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                '.Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
                '[Mod] 2012/08/02 y.ikushima 不要バインド変数削除END
                .Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb                                  'CI番号
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
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>登録理由履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertRegReasonRSql(ByRef Cmd As NpgsqlCommand, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB0401 As DataHBKB0401) As Boolean

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
                .Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb                          'CI番号
                .Parameters("RirekiNo").Value = dataHBKB0401.PropIntRirekiNo                    '履歴番号
                .Parameters("RegReason").Value = dataHBKB0401.PropStrRegReason                  '登録理由
                .Parameters("RegDT").Value = dataHBKB0401.PropDtmSysDate                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0401.PropDtmSysDate                     '最終更新日時
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
    ''' <param name="dataHBKB0401">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>原因リンク履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCauseLinkRSql(ByRef Cmd As NpgsqlCommand, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB0401 As DataHBKB0401) As Boolean

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
                .Parameters("CINmb").Value = dataHBKB0401.PropIntCINmb                          'CI番号
                .Parameters("RirekiNo").Value = dataHBKB0401.PropIntRirekiNo                    '履歴番号
                .Parameters("MngNmb").Value = dataHBKB0401.PropRowReg.Item("MngNmb")            '管理番号
                .Parameters("ProcessKbn").Value = dataHBKB0401.PropRowReg.Item("ProcessKbn")    'プロセス区分
                .Parameters("RegDT").Value = dataHBKB0401.PropDtmSysDate                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0401.PropDtmSysDate                     '最終更新日時
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
