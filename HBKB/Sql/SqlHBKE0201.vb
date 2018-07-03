Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' 変更登録画面Sqlクラス
''' </summary>
''' <remarks>変更登録画面のSQLの作成・設定を行う
''' <para>作成情報：2012/08/13 r.hoshino
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKE0201

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK

    '[SELECT]対応関係者SZKチェック用SQL[インシデント]
    Private strCheckIncKankeiSZKSql As String = "SELECT " & vbCrLf & _
                                                " Count(*) " & vbCrLf & _
                                                "FROM szk_mtb szk" & vbCrLf & _
                                                "INNER JOIN incident_kankei_tb kt" & vbCrLf & _
                                                " ON szk.groupcd=kt.RelationID" & vbCrLf & _
                                                " AND kt.RelationKbn = :KbnGrp " & vbCrLf & _
                                                " AND kt.incnmb = :Nmb " & vbCrLf & _
                                                "WHERE  szk.hbkusrid= :UsrID  " & vbCrLf & _
                                                " AND COALESCE(szk.jtiFlg,'0') <>'1'" & vbCrLf

    '[SELECT]対応関係者G権限チェック用SQL[インシデント]
    Private strCheckIncKankeiGSql As String = "SELECT " & vbCrLf & _
                                              " Count(*) " & vbCrLf & _
                                              "FROM incident_kankei_tb kt " & vbCrLf & _
                                              "WHERE kt.incnmb= :Nmb  " & vbCrLf & _
                                              " AND kt.RelationID = :GrpID " & vbCrLf & _
                                              " AND kt.RelationKbn = :KbnGrp "

    '[SELECT]対応関係者U権限チェック用SQL[インシデント]
    Private strCheckIncKankeiUSql As String = "SELECT " & vbCrLf & _
                                              " Count(*) " & vbCrLf & _
                                              "FROM incident_kankei_tb kt " & vbCrLf & _
                                              "WHERE kt.incnmb= :Nmb  " & vbCrLf & _
                                              " AND kt.RelationID = :UsrID " & vbCrLf & _
                                              " AND kt.RelationKbn = :KbnUsr "

    '[SELECT]対応関係者SZKチェック用SQL[問題]
    Private strCheckPrbKankeiSZKSql As String = "SELECT " & vbCrLf & _
                                                " Count(*) " & vbCrLf & _
                                                "FROM szk_mtb szk" & vbCrLf & _
                                                "INNER JOIN problem_kankei_tb kt" & vbCrLf & _
                                                " ON szk.groupcd=kt.RelationID" & vbCrLf & _
                                                " AND kt.RelationKbn = :KbnGrp " & vbCrLf & _
                                                " AND kt.prbnmb = :Nmb " & vbCrLf & _
                                                "WHERE  szk.hbkusrid= :UsrID  " & vbCrLf & _
                                                " AND COALESCE(szk.jtiFlg,'0') <>'1'" & vbCrLf

    '[SELECT]対応関係者G権限チェック用SQL[問題]
    Private strCheckPrbKankeiGSql As String = "SELECT " & vbCrLf & _
                                              " Count(*) " & vbCrLf & _
                                              "FROM problem_kankei_tb kt " & vbCrLf & _
                                              "WHERE kt.prbnmb= :Nmb  " & vbCrLf & _
                                              " AND kt.RelationID = :GrpID " & vbCrLf & _
                                              " AND kt.RelationKbn = :KbnGrp "

    '[SELECT]対応関係者U権限チェック用SQL[問題]
    Private strCheckPrbKankeiUSql As String = "SELECT " & vbCrLf & _
                                              " Count(*) " & vbCrLf & _
                                              "FROM problem_kankei_tb kt " & vbCrLf & _
                                              "WHERE kt.prbnmb= :Nmb  " & vbCrLf & _
                                              " AND kt.RelationID = :UsrID " & vbCrLf & _
                                              " AND kt.RelationKbn = :KbnUsr "

    '[SELECT]対応関係者SZKチェック用SQL[変更]
    Private strCheckChgKankeiSZKSql As String = "SELECT " & vbCrLf & _
                                                " Count(*) " & vbCrLf & _
                                                "FROM szk_mtb szk" & vbCrLf & _
                                                "INNER JOIN change_kankei_tb kt" & vbCrLf & _
                                                " ON szk.groupcd=kt.RelationID" & vbCrLf & _
                                                " AND kt.RelationKbn = :KbnGrp " & vbCrLf & _
                                                " AND kt.chgnmb = :Nmb " & vbCrLf & _
                                                "WHERE  szk.hbkusrid= :UsrID  " & vbCrLf & _
                                                " AND COALESCE(szk.jtiFlg,'0') <>'1'" & vbCrLf

    '[SELECT]対応関係者G権限チェック用SQL[変更]
    Private strCheckChgKankeiGSql As String = "SELECT " & vbCrLf & _
                                              " Count(*) " & vbCrLf & _
                                              "FROM change_kankei_tb kt " & vbCrLf & _
                                              "WHERE kt.chgnmb= :Nmb  " & vbCrLf & _
                                              " AND kt.RelationID = :GrpID " & vbCrLf & _
                                              " AND kt.RelationKbn = :KbnGrp "

    '[SELECT]対応関係者U権限チェック用SQL[変更]
    Private strCheckChgKankeiUSql As String = "SELECT " & vbCrLf & _
                                              " Count(*) " & vbCrLf & _
                                              "FROM change_kankei_tb kt " & vbCrLf & _
                                              "WHERE kt.chgnmb= :Nmb  " & vbCrLf & _
                                              " AND kt.RelationID = :UsrID " & vbCrLf & _
                                              " AND kt.RelationKbn = :KbnUsr "

    '[SELECT]対応関係者SZKチェック用SQL[リリース]
    Private strCheckRelKankeiSZKSql As String = "SELECT " & vbCrLf & _
                                                " Count(*) " & vbCrLf & _
                                                "FROM szk_mtb szk" & vbCrLf & _
                                                "INNER JOIN release_kankei_tb kt" & vbCrLf & _
                                                " ON szk.groupcd=kt.RelationID" & vbCrLf & _
                                                " AND kt.RelationKbn = :KbnGrp " & vbCrLf & _
                                                " AND kt.relnmb = :Nmb " & vbCrLf & _
                                                "WHERE  szk.hbkusrid= :UsrID  " & vbCrLf & _
                                                " AND COALESCE(szk.jtiFlg,'0') <>'1'" & vbCrLf

    '[SELECT]対応関係者G権限チェック用SQL[リリース]
    Private strCheckRelKankeiGSql As String = "SELECT " & vbCrLf & _
                                              " Count(*) " & vbCrLf & _
                                              "FROM release_kankei_tb kt " & vbCrLf & _
                                              "WHERE kt.relnmb= :Nmb  " & vbCrLf & _
                                              " AND kt.RelationID = :GrpID " & vbCrLf & _
                                              " AND kt.RelationKbn = :KbnGrp "

    '[SELECT]対応関係者U権限チェック用SQL[リリース]
    Private strCheckRelKankeiUSql As String = "SELECT " & vbCrLf & _
                                              " Count(*) " & vbCrLf & _
                                              "FROM release_kankei_tb kt " & vbCrLf & _
                                              "WHERE kt.relnmb= :Nmb  " & vbCrLf & _
                                              " AND kt.RelationID = :UsrID " & vbCrLf & _
                                              " AND kt.RelationKbn = :KbnUsr "

    '[SELECT]対応関係者対象システム変更チェック用SQL[変更]
    Private strCheckChgSystemNmbSql As String = "SELECT " & vbCrLf & _
                                                " systemnmb " & vbCrLf & _
                                                "FROM change_info_tb ct " & vbCrLf & _
                                                "WHERE ct.Chgnmb= :Nmb  "

    'SQL文宣言
    '共通情報ロックテーブル取得用SQL
    Dim strSelectInfoSql As String = "SELECT" & vbCrLf & _
                                       "   NULL AS EdiTime" & vbCrLf & _
                                       "  ,'' AS EdiGrpCD" & vbCrLf & _
                                       "  ,'' AS EdiID" & vbCrLf & _
                                       "  ,'' AS EdiGroupNM" & vbCrLf & _
                                       "  ,'' AS EdiUsrNM" & vbCrLf & _
                                       "  ,Now() AS SysTime" & vbCrLf & _
                                       "UNION ALL" & vbCrLf & _
                                       "SELECT" & vbCrLf & _
                                       "  crt.EdiTime" & vbCrLf & _
                                       " ,crt.EdiGrpCD" & vbCrLf & _
                                       " ,crt.EdiID" & vbCrLf & _
                                       " ,gm.GroupNM" & vbCrLf & _
                                       " ,hm.HBKUsrNM" & vbCrLf & _
                                       " ,NULL" & vbCrLf & _
                                       "FROM change_info_lock_tb crt" & vbCrLf & _
                                       "LEFT JOIN GRP_MTB gm ON crt.EdiGrpCD=gm.GroupCD" & vbCrLf & _
                                       "LEFT JOIN HBKUSR_MTB hm ON crt.EdiID=hm.HBKUsrID" & vbCrLf & _
                                       "WHERE chgnmb=:Nmb"

    '共通情報ロックテーブル登録（INSERT）用SQL
    Dim strInsertLockSql As String = "INSERT INTO change_info_lock_tb" & vbCrLf & _
                                     "(chgnmb,  EdiTime, EdiGrpCD, EdiID)" & vbCrLf & _
                                     "SELECT" & vbCrLf & _
                                     " ct.chgnmb,  Now(), :EdiGrpCD, :EdiID" & vbCrLf & _
                                     "FROM change_info_tb ct" & vbCrLf & _
                                     "WHERE" & vbCrLf & _
                                     " ct.chgnmb = :Nmb"

    '共通情報ロック解除（DELETE）用SQL
    Dim strDeleteLockSql As String = "DELETE FROM change_info_lock_tb WHERE chgnmb=:Nmb"






    '[SELECT]ステータスマスタ
    Private strSelectprocessStateMstSql As String = "SELECT " & vbCrLf & _
                                                    " processstatecd " & vbCrLf & _
                                                    ",processstatenm " & vbCrLf & _
                                                    "FROM  processstate_mtb " & vbCrLf & _
                                                    "WHERE COALESCE(jtiFlg,'0') <>'1' " & vbCrLf & _
                                                    "AND processkbn = :processkbn " & vbCrLf & _
                                                    "ORDER BY Sort "

    '[SELECT]グループマスタ
    '[mod] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 START
    'Private strSelectTantoGpMstSql As String = "SELECT " & vbCrLf & _
    '                                           " groupcd " & vbCrLf & _
    '                                           ",groupnm " & vbCrLf & _
    '                                           "FROM  grp_mtb " & vbCrLf & _
    '                                           "WHERE COALESCE(jtiFlg,'0') <>'1' " & vbCrLf & _
    '                                           "ORDER BY Sort "
    Private strSelectTantoGpMstSql As String = "SELECT " & vbCrLf & _
                                           " groupcd " & vbCrLf & _
                                           ",groupnm " & vbCrLf & _
                                           "FROM  grp_mtb " & vbCrLf & _
                                           "WHERE COALESCE(jtiFlg,'0') <>'1' OR groupcd IN (SELECT TantoGrpCD FROM change_info_tb WHERE ChgNmb = :ChgNmb) " & vbCrLf & _
                                           "ORDER BY jtiFlg,Sort "
    '[mod] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 END

    '[SELECT]対象システム
    Private strSelectsystemMstSql As String = "SELECT " & vbCrLf & _
                                              " cinmb " & vbCrLf & _
                                              ",cinm AS Txt" & vbCrLf & _
                                              ",class1 " & vbCrLf & _
                                              ",class2 " & vbCrLf & _
                                              ",cinm " & vbCrLf & _
                                              "FROM (" & vbCrLf & _
                                              "SELECT cinmb,kindcd,class1,class2,cinm,'1' as sort0,sort " & vbCrLf & _
                                              "FROM  ci_info_tb " & vbCrLf & _
                                              "WHERE cistatuscd <> :cistatuscd AND cikbncd= :cikbncd" & vbCrLf & _
                                              "UNION " & vbCrLf & _
                                              "SELECT cinmb,kindcd,class1,class2,cinm,'2' as sort0,sort " & vbCrLf & _
                                              "FROM  ci_info_tb " & vbCrLf & _
                                              "WHERE cistatuscd = :cistatuscd  AND cikbncd= :cikbncd" & vbCrLf & _
                                              ") AS C" & vbCrLf & _
                                              "ORDER BY Sort0,Sort "


    '[SELECT]エンドユーザマスタ(相手IDEnter取得用)
    Private strSelectEndUsrMstSql As String = "SELECT " & vbCrLf & _
                                              " endusrnm " & vbCrLf & _
                                              ",endusrnmkana " & vbCrLf & _
                                              "FROM  endusr_mtb " & vbCrLf & _
                                              "WHERE endusrid = :endusrid "

    '[SELECT]ユーザマスタ(担当IDEnter取得用)
    Private strSelectHbkUsrMstSql As String = "SELECT " & vbCrLf & _
                                              " hbkusrnm " & vbCrLf & _
                                              ",hbkusrnmkana " & vbCrLf & _
                                              ",groupcd " & vbCrLf & _
                                              "FROM  hbkusr_mtb m1" & vbCrLf & _
                                              "LEFT JOIN szk_mtb m2 ON m1.hbkusrid=m2.hbkusrid " & vbCrLf & _
                                              "WHERE m1.hbkusrid = :hbkusrid "



    '[SELECT]共通情報取得SQL
    Private strSelectMainInfoSql As String = "SELECT " & vbCrLf & _
                                             " ct.chgnmb " & vbCrLf & _
                                             ",ct.processkbn " & vbCrLf & _
                                             ",ct.processstatecd " & vbCrLf & _
                                             ",ct.kaisidt " & vbCrLf & _
                                             ",ct.kanryodt " & vbCrLf & _
                                             ",ct.title " & vbCrLf & _
                                             ",ct.naiyo " & vbCrLf & _
                                             ",ct.taisyo " & vbCrLf & _
                                             ",ct.systemnmb " & vbCrLf & _
                                             ",ct.approverid " & vbCrLf & _
                                             ",ct.approvernm " & vbCrLf & _
                                             ",ct.recorderid " & vbCrLf & _
                                             ",ct.recordernm " & vbCrLf & _
                                             ",ct.tantogrpcd " & vbCrLf & _
                                             ",ct.chgtantoid " & vbCrLf & _
                                             ",ct.chgtantonm " & vbCrLf & _
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
                                             ",ct.titleaimai " & vbCrLf & _
                                             ",ct.naiyoaimai " & vbCrLf & _
                                             ",ct.taisyoaimai " & vbCrLf & _
                                             ",ct.bikoaimai " & vbCrLf & _
                                             ",ct.ChgTantIDAimai " & vbCrLf & _
                                             ",ct.chgtantnmaimai " & vbCrLf & _
                                             ",ct.RegDT " & vbCrLf & _
                                             ",ct.RegGrpCD " & vbCrLf & _
                                             ",ct.RegID " & vbCrLf & _
                                             ",ct.UpdateDT " & vbCrLf & _
                                             ",ct.UpGrpCD " & vbCrLf & _
                                             ",ct.UpdateID " & vbCrLf & _
                                             ",m1.groupnm ||' '||m2.hbkusrnm||' '||to_char(ct.RegDT,'YYYY/MM/DD HH24:MI') AS LblRegInfo" & vbCrLf & _
                                             ",m3.groupnm ||' '||m4.hbkusrnm||' '||to_char(ct.UpdateDT,'YYYY/MM/DD HH24:MI') AS LblUpdateInfo" & vbCrLf & _
                                             ",m1.groupnm  AS mail_RegGp " & vbCrLf & _
                                             ",m2.hbkusrnm AS mail_RegUsr " & vbCrLf & _
                                             ",to_char(ct.RegDT,'YYYY/MM/DD HH24:MI') AS mail_RegDt" & vbCrLf & _
                                             ",m3.groupnm  AS mail_UpdateGp" & vbCrLf & _
                                             ",m4.hbkusrnm AS mail_UpdateUsr" & vbCrLf & _
                                             ",to_char(ct.UpdateDT,'YYYY/MM/DD HH24:MI') AS mail_UpdateDt" & vbCrLf & _
                                             "FROM change_info_tb ct " & vbCrLf & _
                                             "LEFT JOIN grp_mtb    m1 ON m1.groupcd  = ct.RegGrpCD " & vbCrLf & _
                                             "LEFT JOIN hbkusr_mtb m2 ON m2.hbkusrid = ct.RegID " & vbCrLf & _
                                             "LEFT JOIN grp_mtb    m3 ON m3.groupcd  = ct.UpGrpCD " & vbCrLf & _
                                             "LEFT JOIN hbkusr_mtb m4 ON m4.hbkusrid = ct.UpdateID " & vbCrLf & _
                                             "WHERE ct.chgnmb = :chgnmb "


    '[SELECT]対応関係者取得SQL
    Private strSelectKankeiSql As String = "SELECT " & vbCrLf & _
                                              " t.RelationKbn " & vbCrLf & _
                                              ",t.RelationID " & vbCrLf & _
                                              ",t.GroupNM " & vbCrLf & _
                                              ",t.HBKUsrNM " & vbCrLf & _
                                              ",t.EntryNmb" & vbCrLf & _
                                              ",t.RegDT " & vbCrLf & _
                                              ",t.RegGrpCD " & vbCrLf & _
                                              ",t.RegID " & vbCrLf & _
                                              "FROM " & vbCrLf & _
                                              "( " & vbCrLf & _
                                              "  SELECT " & vbCrLf & _
                                              "    kt.RelationKbn " & vbCrLf & _
                                              "   ,kt.RelationID " & vbCrLf & _
                                              "   ,gm.GroupNM	AS GroupNM " & vbCrLf & _
                                              "   ,''		AS HBKUsrNM " & vbCrLf & _
                                              "   ,kt.RegDT " & vbCrLf & _
                                              "   ,kt.RegGrpCD " & vbCrLf & _
                                              "   ,kt.RegID " & vbCrLf & _
                                              "   ,kt.entrynmb " & vbCrLf & _
                                              "  FROM change_kankei_tb kt " & vbCrLf & _
                                              "   INNER JOIN GRP_MTB gm ON kt.RelationID = gm.GroupCD " & vbCrLf & _
                                              "  WHERE kt.chgnmb = :chgnmb " & vbCrLf & _
                                              "    AND kt.RelationKbn = :KbnGrp " & vbCrLf & _
                                              "  UNION ALL " & vbCrLf & _
                                              "  SELECT " & vbCrLf & _
                                              "    kt.RelationKbn " & vbCrLf & _
                                              "   ,kt.RelationID " & vbCrLf & _
                                              "   ,'' " & vbCrLf & _
                                              "   ,hm.HBKUsrNM " & vbCrLf & _
                                              "   ,kt.RegDT " & vbCrLf & _
                                              "   ,kt.RegGrpCD " & vbCrLf & _
                                              "   ,kt.RegID " & vbCrLf & _
                                              "   ,kt.entrynmb " & vbCrLf & _
                                              "  FROM change_kankei_tb kt " & vbCrLf & _
                                              "   INNER JOIN HBKUSR_MTB hm ON hm.HBKUsrID = kt.RelationID " & vbCrLf & _
                                              "  WHERE kt.chgnmb= :chgnmb  " & vbCrLf & _
                                              "   AND kt.RelationKbn = :KbnUsr " & vbCrLf & _
                                              ") t  " & vbCrLf & _
                                              "ORDER BY t.entrynmb  "


    '[SELECT]プロセスリンク取得SQL
    Private strSelectPLinkSql As String = "SELECT " & vbCrLf & _
                                          "CASE t.ProcessKbn " & vbCrLf & _
                                          " WHEN :Kbn_Incident THEN :Kbn_Incident_NMR " & vbCrLf & _
                                          " WHEN :Kbn_Question THEN :Kbn_Question_NMR " & vbCrLf & _
                                          " WHEN :Kbn_Change   THEN :Kbn_Change_NMR " & vbCrLf & _
                                          " WHEN :Kbn_Release  THEN :Kbn_Release_NMR " & vbCrLf & _
                                          "ELSE '' END AS ProcessKbnNMR " & vbCrLf & _
                                          ",MngNmb" & vbCrLf & _
                                          ",ProcessKbn" & vbCrLf & _
                                          ",EntryDt" & vbCrLf & _
                                          ",RegDT " & vbCrLf & _
                                          ",RegGrpCD " & vbCrLf & _
                                          ",RegID " & vbCrLf & _
                                          "FROM ( " & vbCrLf & _
                                          "SELECT  " & vbCrLf & _
                                          " kt1.LinkSakiNmb AS MngNmb " & vbCrLf & _
                                          ",kt1.LinkSakiProcesskbn AS ProcessKbn " & vbCrLf & _
                                          ",kt1.RegDT " & vbCrLf & _
                                          ",kt1.RegGrpCD " & vbCrLf & _
                                          ",kt1.RegID " & vbCrLf & _
                                          ",kt1.EntryDT " & vbCrLf & _
                                          "FROM process_link_tb kt1 " & vbCrLf & _
                                          "WHERE kt1.LinkMotoNmb = :chgnmb " & vbCrLf & _
                                          "AND   kt1.LinkMotoProcesskbn = :LinkMotoProcesskbn " & vbCrLf & _
                                          "UNION " & vbCrLf & _
                                          "SELECT " & vbCrLf & _
                                          " kt2.LinkMotoNmb AS MngNmb " & vbCrLf & _
                                          ",kt2.LinkMotoProcesskbn AS ProcessKbn " & vbCrLf & _
                                          ",kt2.RegDT " & vbCrLf & _
                                          ",kt2.RegGrpCD " & vbCrLf & _
                                          ",kt2.RegID " & vbCrLf & _
                                          ",kt2.EntryDT " & vbCrLf & _
                                          "FROM process_link_tb kt2 " & vbCrLf & _
                                          "WHERE kt2.LinkSakiNmb = :chgnmb " & vbCrLf & _
                                          "AND   kt2.LinkSakiProcesskbn = :LinkMotoProcesskbn " & vbCrLf & _
                                          ") t " & vbCrLf & _
                                          "ORDER BY t.entryDT  "

    '[SELECT]関連ファイル情報取得SQL
    Private strSelectFileSql As String = "SELECT " & vbCrLf & _
                                            " st.filenaiyo AS FileNaiyo" & vbCrLf & _
                                            ",st.regdt " & vbCrLf & _
                                            ",st.filemngnmb AS FileMngNmb " & vbCrLf & _
                                            ",m1.filepath||E'\\'||m1.filenm||m1.ext AS FilePath" & vbCrLf & _
                                            "FROM change_file_tb st " & vbCrLf & _
                                            "INNER JOIN file_mng_tb m1 ON m1.filemngnmb=st.filemngnmb " & vbCrLf & _
                                            "WHERE st.chgnmb = :chgnmb " & vbCrLf & _
                                            "ORDER BY st.EntryNmb  "

    '[SELECT]CYSPR情報取得SQL
    Private strSelectCysprSql As String = "SELECT " & vbCrLf & _
                                          " st.cysprnmb " & vbCrLf & _
                                          ",st.cysprnmb AS bef" & vbCrLf & _
                                          ",st.EntryNmb" & vbCrLf & _
                                          ",st.RegDT" & vbCrLf & _
                                          ",st.RegGrpCD " & vbCrLf & _
                                          ",st.RegID " & vbCrLf & _
                                          ",st.UpdateDT" & vbCrLf & _
                                          ",st.UpGrpCD " & vbCrLf & _
                                          ",st.UpdateID " & vbCrLf & _
                                          "FROM change_cyspr_tb st " & vbCrLf & _
                                          "WHERE st.chgnmb = :chgnmb " & vbCrLf & _
                                          "ORDER BY st.EntryNmb  "

    '[SELECT]会議情報取得SQL
    Private strSelectMeetingTableSql As String = "SELECT " & vbCrLf & _
                                                 " mt.MeetingNmb  " & vbCrLf & _
                                                 ",TO_CHAR(mt.JisiSTDT,'YYYY/MM/DD') AS JisiDT " & vbCrLf & _
                                                 ",CASE mrt.ResultKbn " & vbCrLf & _
                                                 " WHEN :Kbn_NO THEN :Kbn_NO_NM " & vbCrLf & _
                                                 " WHEN :Kbn_OK THEN :Kbn_OK_NM " & vbCrLf & _
                                                 " WHEN :Kbn_NG THEN :Kbn_NG_NM " & vbCrLf & _
                                                 " ELSE '' END AS ResultKbnNM " & vbCrLf & _
                                                 ",mt.Title " & vbCrLf & _
                                                 ",mrt.ResultKbn " & vbCrLf & _
                                                 ",mrt.RegDT " & vbCrLf & _
                                                 ",mrt.ReGGrpCD " & vbCrLf & _
                                                 ",mrt.ReGID " & vbCrLf & _
                                                 "FROM MEETING_RESULT_TB mrt" & vbCrLf & _
                                                 "LEFT JOIN MEETING_TB mt ON mt.MeetingNmb = mrt.MeetingNmb " & vbCrLf & _
                                                 "WHERE  mrt.processkbn = :processkbn " & vbCrLf & _
                                                 "AND  mrt.processnmb = :processnmb " & vbCrLf & _
                                                 "ORDER BY mt.JisiSTDT DESC ,mt.Title"


    '[INSERT]共通情報取得SQL
    Private strInsertMainInfoSql As String = "INSERT INTO change_info_tb (" & vbCrLf & _
                                             " chgnmb " & vbCrLf & _
                                             ",processkbn " & vbCrLf & _
                                             ",processstatecd " & vbCrLf & _
                                             ",kaisidt " & vbCrLf & _
                                             ",kanryodt " & vbCrLf & _
                                             ",title " & vbCrLf & _
                                             ",naiyo " & vbCrLf & _
                                             ",taisyo " & vbCrLf & _
                                             ",systemnmb " & vbCrLf & _
                                             ",approverid " & vbCrLf & _
                                             ",approvernm " & vbCrLf & _
                                             ",recorderid " & vbCrLf & _
                                             ",recordernm " & vbCrLf & _
                                             ",tantogrpcd " & vbCrLf & _
                                             ",chgtantoid " & vbCrLf & _
                                             ",chgtantonm " & vbCrLf & _
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
                                             ",naiyoaimai " & vbCrLf & _
                                             ",taisyoaimai " & vbCrLf & _
                                             ",bikoaimai " & vbCrLf & _
                                             ",ChgTantIDAimai " & vbCrLf & _
                                             ",chgtantnmaimai " & vbCrLf & _
                                             ",RegDT " & vbCrLf & _
                                             ",RegGrpCD " & vbCrLf & _
                                             ",RegID " & vbCrLf & _
                                             ",UpdateDT " & vbCrLf & _
                                             ",UpGrpCD " & vbCrLf & _
                                             ",UpdateID " & vbCrLf & _
                                              ") VALUES (" & vbCrLf & _
                                             " :chgnmb " & vbCrLf & _
                                             ",:processkbn " & vbCrLf & _
                                             ",:processstatecd " & vbCrLf & _
                                             ",:kaisidt " & vbCrLf & _
                                             ",:kanryodt " & vbCrLf & _
                                             ",:title " & vbCrLf & _
                                             ",:naiyo " & vbCrLf & _
                                             ",:taisyo " & vbCrLf & _
                                             ",:systemnmb " & vbCrLf & _
                                             ",:approverid " & vbCrLf & _
                                             ",:approvernm " & vbCrLf & _
                                             ",:recorderid " & vbCrLf & _
                                             ",:recordernm " & vbCrLf & _
                                             ",:tantogrpcd " & vbCrLf & _
                                             ",:chgtantoid " & vbCrLf & _
                                             ",:chgtantonm " & vbCrLf & _
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
                                             ",:titleaimai " & vbCrLf & _
                                             ",:naiyoaimai " & vbCrLf & _
                                             ",:taisyoaimai " & vbCrLf & _
                                             ",:bikoaimai " & vbCrLf & _
                                             ",:ChgTantIDAimai " & vbCrLf & _
                                             ",:chgtantnmaimai " & vbCrLf & _
                                             ",:RegDT " & vbCrLf & _
                                             ",:RegGrpCD " & vbCrLf & _
                                             ",:RegID " & vbCrLf & _
                                             ",:UpdateDT " & vbCrLf & _
                                             ",:UpGrpCD " & vbCrLf & _
                                             ",:UpdateID " & vbCrLf & _
                                             ") "


    '[INSERT]対応関係者SQL
    Private strInsertKankeiSql As String = "INSERT INTO change_kankei_tb ( " & vbCrLf & _
                                           " chgnmb " & vbCrLf & _
                                           ",RelationKbn " & vbCrLf & _
                                           ",RelationID " & vbCrLf & _
                                           ",EntryNmb " & vbCrLf & _
                                           ",RegDT " & vbCrLf & _
                                           ",RegGrpCD " & vbCrLf & _
                                           ",RegID " & vbCrLf & _
                                           ",UpdateDT " & vbCrLf & _
                                           ",UpGrpCD " & vbCrLf & _
                                           ",UpdateID " & vbCrLf & _
                                           ") VALUES (" & vbCrLf & _
                                           " :chgnmb " & vbCrLf & _
                                           ",:RelationKbn " & vbCrLf & _
                                           ",:RelationID " & vbCrLf & _
                                           ",(SELECT COALESCE(MAX(EntryNmb),0)+1 FROM change_kankei_tb WHERE chgnmb=:chgnmb)" & vbCrLf & _
                                           ",:RegDT " & vbCrLf & _
                                           ",:RegGrpCD " & vbCrLf & _
                                           ",:RegID " & vbCrLf & _
                                           ",:UpdateDT " & vbCrLf & _
                                           ",:UpGrpCD " & vbCrLf & _
                                           ",:UpdateID " & vbCrLf & _
                                           ") "



    '[INSERT]プロセスリンク取得SQL
    Private strInsertProcessLinkSql As String = "INSERT INTO process_link_tb (" & vbCrLf & _
                                                " LinkMotoProcesskbn " & vbCrLf & _
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
                                                ") VALUES (" & vbCrLf & _
                                                " :LinkMotoProcesskbn " & vbCrLf & _
                                                ",:LinkMotoNmb " & vbCrLf & _
                                                ",:LinkSakiProcesskbn " & vbCrLf & _
                                                ",:LinkSakiNmb " & vbCrLf & _
                                                ",:EntryDT " & vbCrLf & _
                                                ",:RegDT " & vbCrLf & _
                                                ",:RegGrpCD " & vbCrLf & _
                                                ",:RegID " & vbCrLf & _
                                                ",:UpdateDT " & vbCrLf & _
                                                ",:UpGrpCD " & vbCrLf & _
                                                ",:UpdateID " & vbCrLf & _
                                                ") "

    '[INSERT]CYSPR取得SQL
    Private strInsertCysprSql As String = "INSERT INTO change_cyspr_tb (" & vbCrLf & _
                                                " chgnmb " & vbCrLf & _
                                                ",cysprnmb " & vbCrLf & _
                                                ",cysprnmbaimai " & vbCrLf & _
                                                ",EntryNmb " & vbCrLf & _
                                                ",RegDT " & vbCrLf & _
                                                ",RegGrpCD " & vbCrLf & _
                                                ",RegID " & vbCrLf & _
                                                ",UpdateDT " & vbCrLf & _
                                                ",UpGrpCD " & vbCrLf & _
                                                ",UpdateID " & vbCrLf & _
                                                ") VALUES (" & vbCrLf & _
                                                " :chgnmb " & vbCrLf & _
                                                ",:cysprnmb " & vbCrLf & _
                                                ",:cysprnmbaimai " & vbCrLf & _
                                                ",(SELECT COALESCE(MAX(EntryNmb),0)+1 FROM change_cyspr_tb WHERE chgnmb=:chgnmb ) " & vbCrLf & _
                                                ",:RegDT " & vbCrLf & _
                                                ",:RegGrpCD " & vbCrLf & _
                                                ",:RegID " & vbCrLf & _
                                                ",:UpdateDT " & vbCrLf & _
                                                ",:UpGrpCD " & vbCrLf & _
                                                ",:UpdateID " & vbCrLf & _
                                                ") "

    '[INSERT]会議結果情報取得SQL
    Private strInsertMtgResultSql As String = "INSERT INTO meeting_result_tb (" & vbCrLf & _
                                              " meetingnmb " & vbCrLf & _
                                              ",processkbn " & vbCrLf & _
                                              ",processnmb " & vbCrLf & _
                                              ",resultkbn " & vbCrLf & _
                                              ",EntryNmb " & vbCrLf & _
                                              ",RegDT " & vbCrLf & _
                                              ",RegGrpCD " & vbCrLf & _
                                              ",RegID " & vbCrLf & _
                                              ",UpdateDT " & vbCrLf & _
                                              ",UpGrpCD " & vbCrLf & _
                                              ",UpdateID " & vbCrLf & _
                                              ") VALUES (" & vbCrLf & _
                                              " :meetingnmb " & vbCrLf & _
                                              ",:processkbn " & vbCrLf & _
                                              ",:processnmb " & vbCrLf & _
                                              ",:resultkbn  " & vbCrLf & _
                                              ",(SELECT COALESCE(MAX(EntryNmb),0)+1 FROM meeting_result_tb WHERE meetingnmb=:meetingnmb)" & vbCrLf & _
                                              ",:RegDT " & vbCrLf & _
                                              ",:RegGrpCD " & vbCrLf & _
                                              ",:RegID " & vbCrLf & _
                                              ",:UpdateDT " & vbCrLf & _
                                              ",:UpGrpCD " & vbCrLf & _
                                              ",:UpdateID " & vbCrLf & _
                                              ") "


    '[SELECT]システム日付取得SQL
    Private strSelectSysDateSql As String = "SELECT Now() AS SysDate "

    '[UPDATE]共通情報SQL
    Private strUpdateMainInfoSql As String = "UPDATE change_info_tb SET " & vbCrLf & _
                                             " processkbn =      :processkbn " & vbCrLf & _
                                             ",processstatecd =  :processstatecd " & vbCrLf & _
                                             ",kaisidt =        :kaisidt " & vbCrLf & _
                                             ",kanryodt =        :kanryodt " & vbCrLf & _
                                             ",title =           :title " & vbCrLf & _
                                             ",naiyo =           :naiyo " & vbCrLf & _
                                             ",taisyo =          :taisyo " & vbCrLf & _
                                             ",systemnmb =       :systemnmb " & vbCrLf & _
                                             ",approverid =      :approverid " & vbCrLf & _
                                             ",approvernm =      :approvernm " & vbCrLf & _
                                             ",recorderid =      :recorderid " & vbCrLf & _
                                             ",recordernm =      :recordernm " & vbCrLf & _
                                             ",tantogrpcd =      :tantogrpcd " & vbCrLf & _
                                             ",chgtantoid =      :chgtantoid " & vbCrLf & _
                                             ",chgtantonm =      :chgtantonm " & vbCrLf & _
                                             ",BIko1 =           :BIko1 " & vbCrLf & _
                                             ",Biko2 =           :Biko2 " & vbCrLf & _
                                             ",Biko3 =           :Biko3 " & vbCrLf & _
                                             ",Biko4 =           :Biko4 " & vbCrLf & _
                                             ",Biko5 =           :Biko5 " & vbCrLf & _
                                             ",FreeFlg1 =        :FreeFlg1 " & vbCrLf & _
                                             ",FreeFlg2 =        :FreeFlg2 " & vbCrLf & _
                                             ",FreeFlg3 =        :FreeFlg3 " & vbCrLf & _
                                             ",FreeFlg4 =        :FreeFlg4 " & vbCrLf & _
                                             ",FreeFlg5 =        :FreeFlg5 " & vbCrLf & _
                                             ",titleaimai =      :titleaimai " & vbCrLf & _
                                             ",naiyoaimai =      :naiyoaimai " & vbCrLf & _
                                             ",taisyoaimai =     :taisyoaimai " & vbCrLf & _
                                             ",bikoaimai =       :bikoaimai " & vbCrLf & _
                                             ",ChgTantIDAimai =  :ChgTantIDAimai " & vbCrLf & _
                                             ",chgtantnmaimai =  :chgtantnmaimai " & vbCrLf & _
                                             ",UpdateDT =        :UpdateDT " & vbCrLf & _
                                             ",UpGrpCD =         :UpGrpCD " & vbCrLf & _
                                             ",UpdateID =        :UpdateID " & vbCrLf & _
                                             "WHERE chgnmb =     :chgnmb "


    '[DELETE]対応関係者SQL
    Private strDeleteKankeiSql As String = "DELETE FROM change_kankei_tb " & vbCrLf & _
                                           "WHERE chgnmb=           :chgnmb "


    '[DELETE]会議結果情報ファイルSQL
    Private strDeleteMtgResultSql As String = "DELETE FROM meeting_result_tb " & vbCrLf & _
                                              "WHERE processnmb=           :processnmb "


    '[DELETE]プロセスリンクSQL
    Private strDeletePLinkSql As String = "DELETE FROM process_link_tb " & vbCrLf & _
                                          "WHERE LinkMotoNmb=       :LinkMotoNmb " & vbCrLf & _
                                          "AND LinkMotoProcesskbn=  :LinkMotoProcesskbn " & vbCrLf & _
                                          "AND LinkSakiNmb=         :LinkSakiNmb" & vbCrLf & _
                                          "AND LinkSakiProcesskbn=  :LinkSakiProcesskbn "

    '[DELETE]CysprSQL
    Private strDeleteCysprSql As String = "DELETE FROM change_cyspr_tb " & vbCrLf & _
                                          "WHERE chgnmb=           :chgnmb "


    '[SELECT]新規ログNo取得SQL
    Private strSelectNewLogNoSql As String = "SELECT " & vbCrLf & _
                                             "COALESCE(MAX(ct.logno),0)+1 AS LogNo " & vbCrLf & _
                                             "FROM change_info_ltb ct " & vbCrLf & _
                                             "WHERE ct.chgnmb=:chgnmb "

    '[SELECT]新規ログNo（会議用）取得SQL
    Private strSelectNewMeetingLogNoSql As String = "SELECT " & vbCrLf & _
                                                    "COALESCE(MAX(ML.LogNo),0)+1 AS LogNo " & vbCrLf & _
                                                    "FROM MEETING_LTB ML " & vbCrLf & _
                                                    "WHERE ML.MeetingNmb = :MeetingNmb "

    '[INSERT]共通情報ログSQL
    Private strInsertMainInfoLSql As String = "INSERT INTO  change_info_ltb (" & vbCrLf & _
                                              " chgnmb " & vbCrLf & _
                                              ",LogNo " & vbCrLf & _
                                              ",processkbn " & vbCrLf & _
                                              ",processstatecd " & vbCrLf & _
                                              ",kaisidt " & vbCrLf & _
                                              ",kanryodt " & vbCrLf & _
                                              ",title " & vbCrLf & _
                                              ",naiyo " & vbCrLf & _
                                              ",taisyo " & vbCrLf & _
                                              ",systemnmb " & vbCrLf & _
                                              ",approverid " & vbCrLf & _
                                              ",approvernm " & vbCrLf & _
                                              ",recorderid " & vbCrLf & _
                                              ",recordernm " & vbCrLf & _
                                              ",tantogrpcd " & vbCrLf & _
                                              ",chgtantoid " & vbCrLf & _
                                              ",chgtantonm " & vbCrLf & _
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
                                              ",naiyoaimai " & vbCrLf & _
                                              ",taisyoaimai " & vbCrLf & _
                                              ",bikoaimai " & vbCrLf & _
                                              ",ChgTantIDAimai " & vbCrLf & _
                                              ",chgtantnmaimai " & vbCrLf & _
                                              ",RegDT " & vbCrLf & _
                                              ",RegGrpCD " & vbCrLf & _
                                              ",RegID " & vbCrLf & _
                                              ",UpdateDT " & vbCrLf & _
                                              ",UpGrpCD " & vbCrLf & _
                                              ",UpdateID " & vbCrLf & _
                                              ") " & vbCrLf & _
                                              "SELECT " & vbCrLf & _
                                              " chgnmb " & vbCrLf & _
                                              ",:LogNo " & vbCrLf & _
                                              ",processkbn " & vbCrLf & _
                                              ",processstatecd " & vbCrLf & _
                                              ",kaisidt " & vbCrLf & _
                                              ",kanryodt " & vbCrLf & _
                                              ",title " & vbCrLf & _
                                              ",naiyo " & vbCrLf & _
                                              ",taisyo " & vbCrLf & _
                                              ",systemnmb " & vbCrLf & _
                                              ",approverid " & vbCrLf & _
                                              ",approvernm " & vbCrLf & _
                                              ",recorderid " & vbCrLf & _
                                              ",recordernm " & vbCrLf & _
                                              ",tantogrpcd " & vbCrLf & _
                                              ",chgtantoid " & vbCrLf & _
                                              ",chgtantonm " & vbCrLf & _
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
                                              ",naiyoaimai " & vbCrLf & _
                                              ",taisyoaimai " & vbCrLf & _
                                              ",bikoaimai " & vbCrLf & _
                                              ",ChgTantIDAimai " & vbCrLf & _
                                              ",chgtantnmaimai " & vbCrLf & _
                                              ",RegDT " & vbCrLf & _
                                              ",RegGrpCD " & vbCrLf & _
                                              ",RegID " & vbCrLf & _
                                              ",UpdateDT " & vbCrLf & _
                                              ",UpGrpCD " & vbCrLf & _
                                              ",UpdateID " & vbCrLf & _
                                              "FROM change_info_tb " & vbCrLf & _
                                              "WHERE chgnmb = :chgnmb "


    '[INSERT]対応関係者ログSQL
    Private strInsertKankeiLSql As String = "INSERT INTO change_kankei_ltb ( " & vbCrLf & _
                                            "SELECT " & vbCrLf & _
                                            " chgnmb " & vbCrLf & _
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
                                            "FROM change_kankei_tb " & vbCrLf & _
                                            "WHERE chgnmb = :chgnmb " & vbCrLf & _
                                            ") "

    '[INSERT]プロセスリンク(元)ログSQL
    Private strInsertPLinkMotoLSql As String = "INSERT INTO change_process_link_ltb (" & vbCrLf & _
                                               "SELECT " & vbCrLf & _
                                               " :chgnmb " & vbCrLf & _
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
                                               "WHERE LinkMotoNmb  = :chgnmb " & vbCrLf & _
                                               "AND   LinkMotoProcesskbn = :pkbn " & vbCrLf & _
                                               ") "



    '[INSERT]関連ファイル情報ログSQL
    Private strInsertFileLSql As String = "INSERT INTO change_file_ltb (" & vbCrLf & _
                                          "SELECT " & vbCrLf & _
                                          " chgnmb " & vbCrLf & _
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
                                          "FROM change_file_tb " & vbCrLf & _
                                          "WHERE chgnmb = :chgnmb " & vbCrLf & _
                                          ") "

    '[INSERT]CYSPR情報ログSQL
    Private strInsertCysprLSql As String = "INSERT INTO change_cyspr_ltb (" & vbCrLf & _
                                           "SELECT " & vbCrLf & _
                                           " chgnmb " & vbCrLf & _
                                           ",:LogNo " & vbCrLf & _
                                           ",cysprnmb " & vbCrLf & _
                                           ",cysprnmbaimai " & vbCrLf & _
                                           ",EntryNmb " & vbCrLf & _
                                           ",RegDT " & vbCrLf & _
                                           ",RegGrpCD " & vbCrLf & _
                                           ",RegID " & vbCrLf & _
                                           ",UpdateDT " & vbCrLf & _
                                           ",UpGrpCD " & vbCrLf & _
                                           ",UpdateID " & vbCrLf & _
                                           "FROM change_cyspr_tb " & vbCrLf & _
                                           "WHERE chgnmb = :chgnmb " & vbCrLf & _
                                           ") "


    '[INSERT]会議情報結果ログテーブル         
    Private strInsertMtgResultLSql As String = "INSERT INTO meeting_result_ltb (" & vbCrLf & _
                                               "SELECT " & vbCrLf & _
                                               " meetingnmb " & vbCrLf & _
                                               ",:LogNo " & vbCrLf & _
                                               ",ProcessKbn " & vbCrLf & _
                                               ",ProcessNmb " & vbCrLf & _
                                               ",:ProcessLogNo " & vbCrLf & _
                                               ",resultkbn " & vbCrLf & _
                                               ",EntryNmb " & vbCrLf & _
                                               ",RegDT " & vbCrLf & _
                                               ",RegGrpCD " & vbCrLf & _
                                               ",RegID " & vbCrLf & _
                                               ",UpdateDT " & vbCrLf & _
                                               ",UpGrpCD " & vbCrLf & _
                                               ",UpdateID " & vbCrLf & _
                                               "FROM MEETING_RESULT_TB " & vbCrLf & _
                                               "WHERE processnmb= :processnmb " & vbCrLf & _
                                               "AND processkbn=   :processkbn " & vbCrLf & _
                                               "AND meetingnmb=   :meetingnmb" & vbCrLf & _
                                               ") "


    '[INSERT]会議情報ログ新規登録SQL
    Private strInsertMeetingLSql As String = "INSERT INTO MEETING_LTB ( " & vbCrLf & _
                                             " MeetingNmb " & vbCrLf & _
                                             ",LogNo " & vbCrLf & _
                                             ",YoteiSTDT " & vbCrLf & _
                                             ",YoteiENDDT " & vbCrLf & _
                                             ",JisiSTDT " & vbCrLf & _
                                             ",JisiENDDT " & vbCrLf & _
                                             ",Title " & vbCrLf & _
                                             ",Proceedings " & vbCrLf & _
                                             ",HostGrpCD " & vbCrLf & _
                                             ",HostID " & vbCrLf & _
                                             ",HostNM " & vbCrLf & _
                                             ",TitleAimai " & vbCrLf & _
                                             ",HostIDAimai " & vbCrLf & _
                                             ",HostNMAimai " & vbCrLf & _
                                             ",RegDT " & vbCrLf & _
                                             ",RegGrpCD " & vbCrLf & _
                                             ",RegID " & vbCrLf & _
                                             ",UpdateDT " & vbCrLf & _
                                             ",UpGrpCD " & vbCrLf & _
                                             ",UpdateID " & vbCrLf & _
                                             ") " & vbCrLf & _
                                             "SELECT " & vbCrLf & _
                                             " MT.MeetingNmb " & vbCrLf & _
                                             ",:LogNo " & vbCrLf & _
                                             ",MT.YoteiSTDT " & vbCrLf & _
                                             ",MT.YoteiENDDT " & vbCrLf & _
                                             ",MT.JisiSTDT " & vbCrLf & _
                                             ",MT.JisiENDDT " & vbCrLf & _
                                             ",MT.Title " & vbCrLf & _
                                             ",MT.Proceedings " & vbCrLf & _
                                             ",MT.HostGrpCD " & vbCrLf & _
                                             ",MT.HostID " & vbCrLf & _
                                             ",MT.HostNM " & vbCrLf & _
                                             ",MT.TitleAimai " & vbCrLf & _
                                             ",MT.HostIDAimai " & vbCrLf & _
                                             ",MT.HostNMAimai " & vbCrLf & _
                                             ",MT.RegDT " & vbCrLf & _
                                             ",MT.RegGrpCD " & vbCrLf & _
                                             ",MT.RegID " & vbCrLf & _
                                             ",MT.UpdateDT " & vbCrLf & _
                                             ",MT.UpGrpCD " & vbCrLf & _
                                             ",MT.UpdateID " & vbCrLf & _
                                             "FROM MEETING_TB MT " & vbCrLf & _
                                             "WHERE MT.MeetingNmb = :MeetingNmb "

    '会議出席者情報ログ新規登録（INSERT）SQL
    Private strInsertMtgAttendLSql As String = "INSERT INTO MEETING_ATTEND_LTB ( " & vbCrLf & _
                                               " MeetingNmb " & vbCrLf & _
                                               ",LogNo " & vbCrLf & _
                                               ",AttendGrpCD " & vbCrLf & _
                                               ",AttendID " & vbCrLf & _
                                               ",EntryNmb " & vbCrLf & _
                                               ",RegDT " & vbCrLf & _
                                               ",RegGrpCD " & vbCrLf & _
                                               ",RegID " & vbCrLf & _
                                               ",UpdateDT " & vbCrLf & _
                                               ",UpGrpCD " & vbCrLf & _
                                               ",UpdateID " & vbCrLf & _
                                               ") " & vbCrLf & _
                                               "SELECT " & vbCrLf & _
                                               " MAT.MeetingNmb " & vbCrLf & _
                                               ",:LogNo " & vbCrLf & _
                                               ",MAT.AttendGrpCD " & vbCrLf & _
                                               ",MAT.AttendID " & vbCrLf & _
                                               ",MAT.EntryNmb " & vbCrLf & _
                                               ",MAT.RegDT " & vbCrLf & _
                                               ",MAT.RegGrpCD " & vbCrLf & _
                                               ",MAT.RegID " & vbCrLf & _
                                               ",MAT.UpdateDT " & vbCrLf & _
                                               ",MAT.UpGrpCD " & vbCrLf & _
                                               ",MAT.UpdateID " & vbCrLf & _
                                               "FROM MEETING_ATTEND_TB MAT " & vbCrLf & _
                                               "WHERE MAT.MeetingNmb = :MeetingNmb "

    '会議関連ファイル情報ログ新規登録（INSERT）SQL
    Private strInsertMtgFileLSql As String = "INSERT INTO MEETING_FILE_LTB ( " & vbCrLf & _
                                             " MeetingNmb " & vbCrLf & _
                                             ",LogNo " & vbCrLf & _
                                             ",FileMngNmb " & vbCrLf & _
                                             ",FileNaiyo " & vbCrLf & _
                                             ",EntryNmb " & vbCrLf & _
                                             ",RegDT " & vbCrLf & _
                                             ",RegGrpCD " & vbCrLf & _
                                             ",RegID " & vbCrLf & _
                                             ",UpdateDT " & vbCrLf & _
                                             ",UpGrpCD " & vbCrLf & _
                                             ",UpdateID " & vbCrLf & _
                                             ") " & vbCrLf & _
                                             "SELECT " & vbCrLf & _
                                             " MFT.MeetingNmb " & vbCrLf & _
                                             ",:LogNo " & vbCrLf & _
                                             ",MFT.FileMngNmb " & vbCrLf & _
                                             ",MFT.FileNaiyo " & vbCrLf & _
                                             ",MFT.EntryNmb " & vbCrLf & _
                                             ",MFT.RegDT " & vbCrLf & _
                                             ",MFT.RegGrpCD " & vbCrLf & _
                                             ",MFT.RegID " & vbCrLf & _
                                             ",MFT.UpdateDT " & vbCrLf & _
                                             ",MFT.UpGrpCD " & vbCrLf & _
                                             ",MFT.UpdateID " & vbCrLf & _
                                             "FROM MEETING_FILE_TB MFT " & vbCrLf & _
                                             "WHERE MFT.MeetingNmb = :MeetingNmb "

    '[SELECT]対応関係者G権限チェック用SQL
    Private strCheckKankeiGSql As String = "SELECT " & vbCrLf & _
                                             " Count(*) " & vbCrLf & _
                                              "FROM change_kankei_tb kt " & vbCrLf & _
                                              "WHERE kt.chgnmb= :chgnmb  " & vbCrLf & _
                                              " AND kt.RelationID = :GrpID " & vbCrLf & _
                                              " AND kt.RelationKbn = :KbnGrp "
    '[SELECT]対応関係者U権限チェック用SQL
    Private strCheckKankeiUSql As String = "SELECT " & vbCrLf & _
                                             " Count(*) " & vbCrLf & _
                                              "FROM change_kankei_tb kt " & vbCrLf & _
                                              "WHERE kt.chgnmb= :chgnmb  " & vbCrLf & _
                                              " AND kt.RelationID = :UsrID " & vbCrLf & _
                                              " AND kt.RelationKbn = :KbnUsr "

    '[SELECT]対象システムにおける対応関係者存在チェック用SQL
    Private strCheckSysKankeiUSql As String = "SELECT " & vbCrLf & _
                                              " kt.relationkbn " & vbCrLf & _
                                              ",kt.relationid " & vbCrLf & _
                                              "FROM kankei_tb kt " & vbCrLf & _
                                              "WHERE kt.CInmb = :chgnmb  "

    '[SELECT]担当履歴取得SQL
    Private strSelectTantoRirekiSql As String = "SELECT " & vbCrLf & _
                                              " ct.tantorirekinmb " & vbCrLf & _
                                              ",ct.tantogrpcd " & vbCrLf & _
                                              ",ct.tantogrpnm " & vbCrLf & _
                                              ",ct.chgtantoid " & vbCrLf & _
                                              ",ct.chgtantonm " & vbCrLf & _
                                              "FROM change_tanto_rireki_tb ct " & vbCrLf & _
                                              "WHERE ct.chgnmb = :chgnmb " & vbCrLf & _
                                              "ORDER BY ct.tantorirekinmb DESC"

    '[INSERT]担当履歴SQL
    Private strInsertTantoRirekiSql As String = "INSERT INTO change_tanto_rireki_tb (" & vbCrLf & _
                                             " chgnmb " & vbCrLf & _
                                             ",tantorirekinmb " & vbCrLf & _
                                             ",tantogrpcd " & vbCrLf & _
                                             ",tantogrpnm " & vbCrLf & _
                                             ",chgtantoid " & vbCrLf & _
                                             ",chgtantonm " & vbCrLf & _
                                             ",RegDT " & vbCrLf & _
                                             ",RegGrpCD " & vbCrLf & _
                                             ",RegID " & vbCrLf & _
                                             ",UpdateDT " & vbCrLf & _
                                             ",UpGrpCD " & vbCrLf & _
                                             ",UpdateID " & vbCrLf & _
                                             ") VALUES (" & vbCrLf & _
                                             " :chgnmb " & vbCrLf & _
                                             ",(SELECT COALESCE(MAX(tantorirekinmb),0)+1 FROM change_tanto_rireki_tb WHERE chgnmb=:chgnmb) " & vbCrLf & _
                                             ",:tantogrpcd " & vbCrLf & _
                                             ",:tantogrpnm " & vbCrLf & _
                                             ",:chgtantoid " & vbCrLf & _
                                             ",:chgtantonm " & vbCrLf & _
                                             ",:RegDT " & vbCrLf & _
                                             ",:RegGrpCD " & vbCrLf & _
                                             ",:RegID " & vbCrLf & _
                                             ",:UpdateDT " & vbCrLf & _
                                             ",:UpGrpCD " & vbCrLf & _
                                             ",:UpdateID " & vbCrLf & _
                                             ") "


    ''' <summary>
    ''' 【共通】マスタデータ取得：ステータス
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ステータスコンボボックス取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetCmbProcessStateMstData(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectprocessStateMstSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("processkbn", NpgsqlTypes.NpgsqlDbType.Varchar))        'プロセス区分
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("processkbn").Value = PROCESS_TYPE_CHANGE                        'プロセス区分：変更
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
    ''' 【共通】マスタデータ取得：担当グループ
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>グループコンボボックス取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetSTantoMastaData(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectTantoGpMstSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '[add] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 START
            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("chgnmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CHG番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("chgnmb").Value = dataHBKE0201.PropIntChgNmb
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
    ''' 【共通】データ取得：対象システム
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>対象システムコンボボックス取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetsystemMastaData(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectsystemMstSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット AND cikbncd= :cikbncd
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("cistatuscd", NpgsqlTypes.NpgsqlDbType.Varchar))       'ステータス
                .Add(New NpgsqlParameter("cikbncd", NpgsqlTypes.NpgsqlDbType.Varchar))          '区分
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("cistatuscd").Value = CI_STATUS_SYSTEM_HAISHIZUMI                   '廃止済
                .Parameters("cikbncd").Value = CI_TYPE_SYSTEM                                   'システム
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
    ''' 【共通】マスタデータ取得：エンドユーザー
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>指定したエンドユーザー取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetPartnerInfoData(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectEndUsrMstSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("endusrid", NpgsqlTypes.NpgsqlDbType.Varchar))
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("endusrid").Value = dataHBKE0201.PropStrSeaKey
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
    ''' 【共通】マスタデータ取得：ユーザー
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>指定したひびきユーザー取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetTantoInfoData(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectHbkUsrMstSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("hbkusrid", NpgsqlTypes.NpgsqlDbType.Varchar))
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("hbkusrid").Value = dataHBKE0201.PropStrSeaKey
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
    ''' 【表示用】共通情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>共通情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectMainInfoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectMainInfoSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("chgnmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CHG番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("chgnmb").Value = dataHBKE0201.PropIntChgNmb
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
    ''' 【表示用】対応関係者情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>対応関係者情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectKankeiSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectKankeiSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("chgnmb", NpgsqlTypes.NpgsqlDbType.Integer))           '番号
                .Add(New NpgsqlParameter("KbnGrp", NpgsqlTypes.NpgsqlDbType.Varchar))           '区分：グループ
                .Add(New NpgsqlParameter("KbnUsr", NpgsqlTypes.NpgsqlDbType.Varchar))           '区分：ユーザー
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("chgnmb").Value = dataHBKE0201.PropIntChgNmb                        '番号
                .Parameters("KbnGrp").Value = KBN_GROUP                                         '区分：グループ
                .Parameters("KbnUsr").Value = KBN_USER                                          '区分：ユーザー
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
    ''' 【表示用】プロセスリンク情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>プロセスリンク情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectPLinkSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectPLinkSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("Kbn_Incident", NpgsqlTypes.NpgsqlDbType.Varchar))      'プロセス区分
                .Add(New NpgsqlParameter("Kbn_Incident_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))  'プロセス区分名略称：インシデント
                .Add(New NpgsqlParameter("Kbn_Question", NpgsqlTypes.NpgsqlDbType.Varchar))      'プロセス区分：問題
                .Add(New NpgsqlParameter("Kbn_Question_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))  'プロセス区分名略称：問題
                .Add(New NpgsqlParameter("Kbn_Change", NpgsqlTypes.NpgsqlDbType.Varchar))        'プロセス区分：変更
                .Add(New NpgsqlParameter("Kbn_Change_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))    'プロセス区分名略称：変更
                .Add(New NpgsqlParameter("Kbn_Release", NpgsqlTypes.NpgsqlDbType.Varchar))       'プロセス区分：リリース
                .Add(New NpgsqlParameter("Kbn_Release_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))   'プロセス区分名略称：リリース
                .Add(New NpgsqlParameter("chgnmb", NpgsqlTypes.NpgsqlDbType.Integer))            'CHG番号
                .Add(New NpgsqlParameter("LinkMotoProcesskbn", NpgsqlTypes.NpgsqlDbType.Varchar))
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
                .Parameters("chgnmb").Value = dataHBKE0201.PropIntChgNmb                         'CHG番号LinkMotoProcesskbn
                .Parameters("LinkMotoProcesskbn").Value = PROCESS_TYPE_CHANGE                    'プロセス区分：変更
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
    ''' 【表示用】関連ファイル情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>関連ファイル情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectFileSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectFileSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("chgnmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CHG番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("chgnmb").Value = dataHBKE0201.PropIntChgNmb
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
    ''' 【表示用】CYSPR情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CYSPR情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCysprSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectCysprSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("chgnmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CHG番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("chgnmb").Value = dataHBKE0201.PropIntChgNmb
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
    ''' 【表示用】会議情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectMeetingSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectMeetingTableSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("processkbn", NpgsqlTypes.NpgsqlDbType.Varchar))        'プロセス区分
                .Add(New NpgsqlParameter("processnmb", NpgsqlTypes.NpgsqlDbType.Integer))        'プロセス番号

                .Add(New NpgsqlParameter("Kbn_NO", NpgsqlTypes.NpgsqlDbType.Varchar))       '結果区分：未入力
                .Add(New NpgsqlParameter("Kbn_NO_NM", NpgsqlTypes.NpgsqlDbType.Varchar))    '結果区分名略称：未入力
                .Add(New NpgsqlParameter("Kbn_OK", NpgsqlTypes.NpgsqlDbType.Varchar))       '結果区分：承認
                .Add(New NpgsqlParameter("Kbn_OK_NM", NpgsqlTypes.NpgsqlDbType.Varchar))    '結果区分名略称：承認
                .Add(New NpgsqlParameter("Kbn_NG", NpgsqlTypes.NpgsqlDbType.Varchar))       '結果区分：却下
                .Add(New NpgsqlParameter("Kbn_NG_NM", NpgsqlTypes.NpgsqlDbType.Varchar))    '結果区分名略称：却下
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("processkbn").Value = PROCESS_TYPE_CHANGE
                .Parameters("processnmb").Value = dataHBKE0201.PropIntChgNmb                      'CHG番号

                .Parameters("Kbn_NO").Value = SELECT_RESULTKBN_NO          '結果区分：未入力
                .Parameters("Kbn_NO_NM").Value = SELECT_RESULTKBNNM_NO     '結果区分名略称：未入力
                .Parameters("Kbn_OK").Value = SELECT_RESULTKBN_OK          '結果区分：承認
                .Parameters("Kbn_OK_NM").Value = SELECT_RESULTKBNNM_OK     '結果区分名略称：承認
                .Parameters("Kbn_NG").Value = SELECT_RESULTKBN_NG          '結果区分：却下
                .Parameters("Kbn_NG_NM").Value = SELECT_RESULTKBNNM_NG     '結果区分名略称：却下

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
    ''' 【新規登録モード】新規番号、サーバー日付取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規CHG番号、サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewNmbAndSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                   ByVal Cn As NpgsqlConnection, _
                                                   ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = GET_NEXTVAL_HENKOU_NO

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
    ''' 【新規登録モード】共通情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>共通情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertMainInfoSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Dim strBikoAimai As String = ""         'フリーテキスト（あいまい）


        Try

            'SQL文(INSERT)
            strSQL = strInsertMainInfoSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("chgnmb", NpgsqlTypes.NpgsqlDbType.Integer))            'CHG番号
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))        'プロセス区分
                .Add(New NpgsqlParameter("processstatecd", NpgsqlTypes.NpgsqlDbType.Varchar))    'プロセスステータスCD
                .Add(New NpgsqlParameter("kaisidt", NpgsqlTypes.NpgsqlDbType.Timestamp))        '開始日時
                .Add(New NpgsqlParameter("kanryodt", NpgsqlTypes.NpgsqlDbType.Timestamp))        '完了日時
                .Add(New NpgsqlParameter("Title", NpgsqlTypes.NpgsqlDbType.Varchar))             'タイトル
                .Add(New NpgsqlParameter("naiyo", NpgsqlTypes.NpgsqlDbType.Varchar))             '内容
                .Add(New NpgsqlParameter("taisyo", NpgsqlTypes.NpgsqlDbType.Varchar))            '対処
                .Add(New NpgsqlParameter("SystemNmb", NpgsqlTypes.NpgsqlDbType.Integer))         '対象システム番号
                .Add(New NpgsqlParameter("approverid", NpgsqlTypes.NpgsqlDbType.Varchar))        '変更承認者ID
                .Add(New NpgsqlParameter("approvernm", NpgsqlTypes.NpgsqlDbType.Varchar))        '変更承認者氏名
                .Add(New NpgsqlParameter("recorderid", NpgsqlTypes.NpgsqlDbType.Varchar))        '承認記録者ID
                .Add(New NpgsqlParameter("recordernm", NpgsqlTypes.NpgsqlDbType.Varchar))        '承認記録者氏名
                .Add(New NpgsqlParameter("TantoGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))        '担当グループCD
                .Add(New NpgsqlParameter("chgtantoid", NpgsqlTypes.NpgsqlDbType.Varchar))        '担当者ID
                .Add(New NpgsqlParameter("chgtantonm", NpgsqlTypes.NpgsqlDbType.Varchar))        '担当者氏名
                .Add(New NpgsqlParameter("GroupRireki", NpgsqlTypes.NpgsqlDbType.Varchar))       'グループ履歴
                .Add(New NpgsqlParameter("TantoRireki", NpgsqlTypes.NpgsqlDbType.Varchar))       '担当者履歴
                .Add(New NpgsqlParameter("BIko1", NpgsqlTypes.NpgsqlDbType.Varchar))             'フリーテキスト１
                .Add(New NpgsqlParameter("Biko2", NpgsqlTypes.NpgsqlDbType.Varchar))             'フリーテキスト２
                .Add(New NpgsqlParameter("Biko3", NpgsqlTypes.NpgsqlDbType.Varchar))             'フリーテキスト３
                .Add(New NpgsqlParameter("Biko4", NpgsqlTypes.NpgsqlDbType.Varchar))             'フリーテキスト４
                .Add(New NpgsqlParameter("Biko5", NpgsqlTypes.NpgsqlDbType.Varchar))             'フリーテキスト５
                .Add(New NpgsqlParameter("FreeFlg1", NpgsqlTypes.NpgsqlDbType.Varchar))          'フリーフラグ１
                .Add(New NpgsqlParameter("FreeFlg2", NpgsqlTypes.NpgsqlDbType.Varchar))          'フリーフラグ２
                .Add(New NpgsqlParameter("FreeFlg3", NpgsqlTypes.NpgsqlDbType.Varchar))          'フリーフラグ３
                .Add(New NpgsqlParameter("FreeFlg4", NpgsqlTypes.NpgsqlDbType.Varchar))          'フリーフラグ４
                .Add(New NpgsqlParameter("FreeFlg5", NpgsqlTypes.NpgsqlDbType.Varchar))          'フリーフラグ５
                .Add(New NpgsqlParameter("titleaimai", NpgsqlTypes.NpgsqlDbType.Varchar))        'タイトル（あいまい）
                .Add(New NpgsqlParameter("naiyoaimai", NpgsqlTypes.NpgsqlDbType.Varchar))        '内容（あいまい）
                .Add(New NpgsqlParameter("taisyoaimai", NpgsqlTypes.NpgsqlDbType.Varchar))       '対処(あいまい)
                .Add(New NpgsqlParameter("bikoaimai", NpgsqlTypes.NpgsqlDbType.Varchar))         'フリーテキスト（あいまい）
                .Add(New NpgsqlParameter("ChgTantIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))    '担当者ID(あいまい)
                .Add(New NpgsqlParameter("chgtantnmaimai", NpgsqlTypes.NpgsqlDbType.Varchar))    '担当者氏名(あいまい)
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))        '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))           '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者ID
            End With
            'バインド変数に値をセット

            With Cmd
                .Parameters("chgnmb").Value = dataHBKE0201.PropIntChgNmb                                        '新規CHG番号
                .Parameters("ProcessKbn").Value = PROCESS_TYPE_CHANGE                                           'プロセス区分
                .Parameters("ProcessStateCD").Value = dataHBKE0201.PropCmbprocessStateCD.SelectedValue          'ステータスCD(ComboBox)
                '開始日時
                If dataHBKE0201.PropDtpKaisiDT.txtDate.Text.Equals("") Then
                    .Parameters("kaisidt").Value = Nothing
                Else
                    .Parameters("kaisidt").Value = _
                        CDate(dataHBKE0201.PropDtpKaisiDT.txtDate.Text & " " & dataHBKE0201.PropTxtKaisiDT_HM.PropTxtTime.Text)
                End If
                '完了日時
                If dataHBKE0201.PropDtpKanryoDT.txtDate.Text.Equals("") Then
                    'ステータスが完了ならばシステム日付を設定する
                    If dataHBKE0201.PropCmbprocessStateCD.SelectedValue = PROCESS_STATUS_CHANGE_KANRYOU Then
                        .Parameters("kanryodt").Value = dataHBKE0201.PropDtmSysDate
                    Else
                        .Parameters("kanryodt").Value = Nothing
                    End If
                Else
                    .Parameters("kanryodt").Value = _
                        CDate(dataHBKE0201.PropDtpKanryoDT.txtDate.Text & " " & dataHBKE0201.PropTxtKanryoDT_HM.PropTxtTime.Text)
                End If
                .Parameters("Title").Value = dataHBKE0201.PropTxtTitle.Text                                     'タイトル
                .Parameters("naiyo").Value = dataHBKE0201.PropTxtNaiyo.Text                                     '内容
                .Parameters("taisyo").Value = dataHBKE0201.PropTxtTaisyo.Text                                   '対処
                .Parameters("SystemNmb").Value = dataHBKE0201.PropCmbSystemNmb.PropCmbColumns.SelectedValue     '対象システム番号(ComboBoxEx)

                .Parameters("approverid").Value = dataHBKE0201.PropTxthenkouID.Text                             '変更承認者ID
                .Parameters("approvernm").Value = dataHBKE0201.PropTxthenkouNM.Text                             '変更承認者氏名
                .Parameters("recorderid").Value = dataHBKE0201.PropTxtsyoninID.Text                             '承認記録者ID
                .Parameters("recordernm").Value = dataHBKE0201.PropTxtsyoninNM.Text                             '承認記録者氏名
                .Parameters("tantogrpcd").Value = dataHBKE0201.PropCmbTantoGrpCD.SelectedValue                  '担当グループCD(ComboBox)
                .Parameters("chgtantoid").Value = dataHBKE0201.PropTxtTantoID.Text                              '担当者ID
                .Parameters("chgtantonm").Value = dataHBKE0201.PropTxtTantoNM.Text                              '担当者氏名

                .Parameters("GroupRireki").Value = dataHBKE0201.PropCmbTantoGrpCD.Text                          'グループ履歴(グループ名）
                .Parameters("TantoRireki").Value = dataHBKE0201.PropTxtTantoNM.Text                             '担当者履歴（ユーザ名）

                .Parameters("BIko1").Value = dataHBKE0201.PropTxtBIko1.Text                                     'フリーテキスト１
                .Parameters("Biko2").Value = dataHBKE0201.PropTxtBIko2.Text                                     'フリーテキスト２
                .Parameters("BIko3").Value = dataHBKE0201.PropTxtBIko3.Text                                     'フリーテキスト３
                .Parameters("Biko4").Value = dataHBKE0201.PropTxtBIko4.Text                                     'フリーテキスト４
                .Parameters("Biko5").Value = dataHBKE0201.PropTxtBIko5.Text                                     'フリーテキスト５

                'フリーフラグ１～５
                If dataHBKE0201.PropChkFreeFlg1.Checked = True Then
                    .Parameters("FreeFlg1").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg1").Value = FREE_FLG_OFF
                End If
                If dataHBKE0201.PropChkFreeFlg2.Checked = True Then
                    .Parameters("FreeFlg2").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg2").Value = FREE_FLG_OFF
                End If
                If dataHBKE0201.PropChkFreeFlg3.Checked = True Then
                    .Parameters("FreeFlg3").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg3").Value = FREE_FLG_OFF
                End If
                If dataHBKE0201.PropChkFreeFlg4.Checked = True Then
                    .Parameters("FreeFlg4").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg4").Value = FREE_FLG_OFF
                End If
                If dataHBKE0201.PropChkFreeFlg5.Checked = True Then
                    .Parameters("FreeFlg5").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg5").Value = FREE_FLG_OFF
                End If

                'あいまい検索文字列設定
                '※フリーテキスト（あいまい）：フリーテキスト１～５を連結
                strBikoAimai = commonLogicHBK.ChangeStringForSearch(dataHBKE0201.PropTxtBIko1.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKE0201.PropTxtBIko2.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKE0201.PropTxtBIko3.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKE0201.PropTxtBIko4.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKE0201.PropTxtBIko5.Text)
                .Parameters("TitleAimai").Value = _
                    commonLogicHBK.ChangeStringForSearch(dataHBKE0201.PropTxtTitle.Text)                        'タイトル（あいまい）
                .Parameters("naiyoaimai").Value = _
                    commonLogicHBK.ChangeStringForSearch(dataHBKE0201.PropTxtNaiyo.Text)                        '内容（あいまい）
                .Parameters("taisyoaimai").Value = _
                    commonLogicHBK.ChangeStringForSearch(dataHBKE0201.PropTxtTaisyo.Text)                       '対処(あいまい)
                .Parameters("BikoAimai").Value = strBikoAimai                                                   'フリーテキスト（あいまい）
                .Parameters("ChgTantIDAimai").Value = _
                    commonLogicHBK.ChangeStringForSearch(dataHBKE0201.PropTxtTantoID.Text)                      '担当者ID(あいまい)
                .Parameters("chgtantnmaimai").Value = _
                    commonLogicHBK.ChangeStringForSearch(dataHBKE0201.PropTxtTantoNM.Text)                      '担当者氏名(あいまい)

                .Parameters("RegDT").Value = dataHBKE0201.PropDtmSysDate                                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKE0201.PropDtmSysDate                                     '最終更新日時
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
    ''' 【新規登録／編集モード】対応関係者情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>対応関係者情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertKankeiSql(ByRef Cmd As NpgsqlCommand, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertKankeiSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("chgnmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'CHG番号
                .Add(New NpgsqlParameter("RelationKbn", NpgsqlTypes.NpgsqlDbType.Varchar))              '関係区分
                .Add(New NpgsqlParameter("RelationID", NpgsqlTypes.NpgsqlDbType.Varchar))               '関係ID
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                  '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                 '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                    '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("chgnmb").Value = dataHBKE0201.PropIntChgNmb                                'CHG番号
                .Parameters("RelationKbn").Value = dataHBKE0201.PropRowReg.Item("RelationKbn")          '関係区分
                .Parameters("RelationID").Value = dataHBKE0201.PropRowReg.Item("RelationID")            '関係ID

                If dataHBKE0201.PropRowReg.Item("RegDT").ToString.Length > 0 Then
                    .Parameters("RegDT").Value = dataHBKE0201.PropRowReg.Item("RegDT")
                    .Parameters("RegGrpCD").Value = dataHBKE0201.PropRowReg.Item("RegGrpCD")
                    .Parameters("RegID").Value = dataHBKE0201.PropRowReg.Item("RegID")
                Else
                    .Parameters("RegDT").Value = dataHBKE0201.PropDtmSysDate                         '登録日時
                    .Parameters("RegGrpCD").Value = PropWorkGroupCD                                  '登録者グループCD
                    .Parameters("RegID").Value = PropUserId                                          '登録者ID
                End If
                .Parameters("UpdateDT").Value = dataHBKE0201.PropDtmSysDate                             '最終更新日時
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
    ''' 【新規登録／編集モード】プロセスリンク(元)情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <param name="intAddMilliSec">[IN]ミリ秒数カウンタ</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>プロセスリンク情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InsertPLinkMoto(ByRef Cmd As NpgsqlCommand, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKE0201 As DataHBKE0201, _
                                         ByVal intAddMilliSec As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertProcessLinkSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LinkMotoprocesskbn", NpgsqlTypes.NpgsqlDbType.Varchar))       '元P区分
                .Add(New NpgsqlParameter("LinkMotoNmb", NpgsqlTypes.NpgsqlDbType.Integer))              '元CHG番号
                .Add(New NpgsqlParameter("LinkSakiprocesskbn", NpgsqlTypes.NpgsqlDbType.Varchar))       '先P区分
                .Add(New NpgsqlParameter("LinkSakiNmb", NpgsqlTypes.NpgsqlDbType.Integer))              '先CHG番号
                .Add(New NpgsqlParameter("EntryDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                '登録順
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                  '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                 '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                    '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("LinkMotoprocesskbn").Value = PROCESS_TYPE_CHANGE                                   '元P区分
                .Parameters("LinkMotoNmb").Value = dataHBKE0201.PropIntChgNmb                                   '元CHG番号
                .Parameters("LinkSakiprocesskbn").Value = dataHBKE0201.PropRowReg.Item("processkbn")            '参照先P区分
                .Parameters("LinkSakiNmb").Value = dataHBKE0201.PropRowReg.Item("MngNmb")                       '参照先CHG番号
                .Parameters("RegDT").Value = dataHBKE0201.PropDtmSysDate                                        '登録日時
                .Parameters("EntryDT").Value = dataHBKE0201.PropDtmSysDate.AddMilliseconds(intAddMilliSec)      'カウンタ
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKE0201.PropDtmSysDate                                     '更新日時
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
    ''' 【新規登録／編集モード】CYSPR情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CYSPR情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCysprSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Dim strBikoAimai As String = ""         'フリーテキスト（あいまい）


        Try

            'SQL文(INSERT)
            strSQL = strInsertCysprSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("chgnmb", NpgsqlTypes.NpgsqlDbType.Integer))            'CHG番号
                .Add(New NpgsqlParameter("cysprnmb", NpgsqlTypes.NpgsqlDbType.Varchar))          'CYSPR番号
                .Add(New NpgsqlParameter("cysprnmbaimai", NpgsqlTypes.NpgsqlDbType.Varchar))     'CYSPR番号（あいまい）
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))        '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))           '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("chgnmb").Value = dataHBKE0201.PropIntChgNmb                                                       '新規CHG番号
                .Parameters("cysprnmb").Value = dataHBKE0201.PropRowReg.Item("cysprnmb")                                       'CYSPR番号
                .Parameters("cysprnmbaimai").Value = _
                    commonLogicHBK.ChangeStringForSearch(dataHBKE0201.PropRowReg.Item("cysprnmb"))                             'CYSPR番号（あいまい）
                Dim no_update_flg As Boolean = False
                If dataHBKE0201.PropRowReg.Item("RegDT").ToString.Length > 0 Then
                    .Parameters("RegDT").Value = dataHBKE0201.PropRowReg.Item("RegDT")
                    .Parameters("RegGrpCD").Value = dataHBKE0201.PropRowReg.Item("RegGrpCD")
                    .Parameters("RegID").Value = dataHBKE0201.PropRowReg.Item("RegID")
                    If dataHBKE0201.PropRowReg.Item("cysprnmb").ToString.Equals(dataHBKE0201.PropRowReg.Item("bef").ToString) Then
                        '更新箇所が１つもない
                        no_update_flg = True
                    End If

                Else
                    .Parameters("RegDT").Value = dataHBKE0201.PropDtmSysDate                         '登録日時
                    .Parameters("RegGrpCD").Value = PropWorkGroupCD                                  '登録者グループCD
                    .Parameters("RegID").Value = PropUserId                                          '登録者ID
                End If

                If no_update_flg = True Then
                    .Parameters("UpdateDT").Value = dataHBKE0201.PropRowReg.Item("UpdateDT")
                    .Parameters("UpGrpCD").Value = dataHBKE0201.PropRowReg.Item("UpGrpCD")
                    .Parameters("UpdateID").Value = dataHBKE0201.PropRowReg.Item("UpdateID")
                Else
                    .Parameters("UpdateDT").Value = dataHBKE0201.PropDtmSysDate                      '最終更新日時
                    .Parameters("UpGrpCD").Value = PropWorkGroupCD                                   '最終更新者グループCD
                    .Parameters("UpdateID").Value = PropUserId                                       '最終更新者ID
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
    ''' 【新規登録／編集モード】会議結果情報更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議結果情報更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/30 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertMtgResultSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertMtgResultSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("MeetingNmb", NpgsqlTypes.NpgsqlDbType.Integer))       '会議番号
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))       'プロセス区分
                .Add(New NpgsqlParameter("ProcessNmb", NpgsqlTypes.NpgsqlDbType.Integer))       'プロセス番号
                .Add(New NpgsqlParameter("ResultKbn", NpgsqlTypes.NpgsqlDbType.Varchar))        '結果区分
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))          '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))         '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))            '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("MeetingNmb").Value = dataHBKE0201.PropRowReg.Item("MeetingNmb")    '会議番号
                .Parameters("ProcessKbn").Value = PROCESS_TYPE_CHANGE                           'プロセス区分：変更
                .Parameters("ProcessNmb").Value = dataHBKE0201.PropIntChgNmb                    'プロセス番号
                '結果区分がブランクの場合は0を設定
                If dataHBKE0201.PropRowReg.Item("ResultKbn").ToString.Equals("") Then
                    .Parameters("ResultKbn").Value = SELECT_RESULTKBN_NO                        'ブランク
                Else
                    .Parameters("ResultKbn").Value = dataHBKE0201.PropRowReg.Item("ResultKbn")
                End If
                If dataHBKE0201.PropRowReg.Item("RegDT").ToString.Length > 0 Then
                    .Parameters("RegDT").Value = dataHBKE0201.PropRowReg.Item("RegDT")
                    .Parameters("RegGrpCD").Value = dataHBKE0201.PropRowReg.Item("RegGrpCD")
                    .Parameters("RegID").Value = dataHBKE0201.PropRowReg.Item("RegID")
                Else
                    .Parameters("RegDT").Value = dataHBKE0201.PropDtmSysDate                         '登録日時
                    .Parameters("RegGrpCD").Value = PropWorkGroupCD                                  '登録者グループCD
                    .Parameters("RegID").Value = PropUserId                                          '登録者ID
                End If
                .Parameters("UpdateDT").Value = dataHBKE0201.PropDtmSysDate                     '最終更新日時
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
    ''' 【編集モード】サーバー日付取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/27 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKE0201 As DataHBKE0201) As Boolean

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
    ''' 【編集モード】共通情報更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>共通情報更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateMainInfoSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Dim strBikoAimai As String = ""         'フリーテキスト（あいまい）

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateMainInfoSql

            'データアダプタに、SQLのUPDATE文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("chgnmb", NpgsqlTypes.NpgsqlDbType.Integer))            'CHG番号
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))        'プロセス区分
                .Add(New NpgsqlParameter("processstatecd", NpgsqlTypes.NpgsqlDbType.Varchar))    'プロセスステータスCD
                .Add(New NpgsqlParameter("kaisidt", NpgsqlTypes.NpgsqlDbType.Timestamp))        '開始日時
                .Add(New NpgsqlParameter("kanryodt", NpgsqlTypes.NpgsqlDbType.Timestamp))        '完了日時
                .Add(New NpgsqlParameter("Title", NpgsqlTypes.NpgsqlDbType.Varchar))             'タイトル
                .Add(New NpgsqlParameter("naiyo", NpgsqlTypes.NpgsqlDbType.Varchar))             '内容
                .Add(New NpgsqlParameter("taisyo", NpgsqlTypes.NpgsqlDbType.Varchar))            '対処
                .Add(New NpgsqlParameter("SystemNmb", NpgsqlTypes.NpgsqlDbType.Integer))         '対象システム番号
                .Add(New NpgsqlParameter("approverid", NpgsqlTypes.NpgsqlDbType.Varchar))        '変更承認者ID
                .Add(New NpgsqlParameter("approvernm", NpgsqlTypes.NpgsqlDbType.Varchar))        '変更承認者氏名
                .Add(New NpgsqlParameter("recorderid", NpgsqlTypes.NpgsqlDbType.Varchar))        '承認記録者ID
                .Add(New NpgsqlParameter("recordernm", NpgsqlTypes.NpgsqlDbType.Varchar))        '承認記録者氏名
                .Add(New NpgsqlParameter("TantoGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))        '担当グループCD
                .Add(New NpgsqlParameter("chgtantoid", NpgsqlTypes.NpgsqlDbType.Varchar))        '担当者ID
                .Add(New NpgsqlParameter("chgtantonm", NpgsqlTypes.NpgsqlDbType.Varchar))        '担当者氏名
                .Add(New NpgsqlParameter("GroupRireki", NpgsqlTypes.NpgsqlDbType.Varchar))       'グループ履歴
                .Add(New NpgsqlParameter("TantoRireki", NpgsqlTypes.NpgsqlDbType.Varchar))       '担当者履歴
                .Add(New NpgsqlParameter("BIko1", NpgsqlTypes.NpgsqlDbType.Varchar))             'フリーテキスト１
                .Add(New NpgsqlParameter("Biko2", NpgsqlTypes.NpgsqlDbType.Varchar))             'フリーテキスト２
                .Add(New NpgsqlParameter("Biko3", NpgsqlTypes.NpgsqlDbType.Varchar))             'フリーテキスト３
                .Add(New NpgsqlParameter("Biko4", NpgsqlTypes.NpgsqlDbType.Varchar))             'フリーテキスト４
                .Add(New NpgsqlParameter("Biko5", NpgsqlTypes.NpgsqlDbType.Varchar))             'フリーテキスト５
                .Add(New NpgsqlParameter("FreeFlg1", NpgsqlTypes.NpgsqlDbType.Varchar))          'フリーフラグ１
                .Add(New NpgsqlParameter("FreeFlg2", NpgsqlTypes.NpgsqlDbType.Varchar))          'フリーフラグ２
                .Add(New NpgsqlParameter("FreeFlg3", NpgsqlTypes.NpgsqlDbType.Varchar))          'フリーフラグ３
                .Add(New NpgsqlParameter("FreeFlg4", NpgsqlTypes.NpgsqlDbType.Varchar))          'フリーフラグ４
                .Add(New NpgsqlParameter("FreeFlg5", NpgsqlTypes.NpgsqlDbType.Varchar))          'フリーフラグ５
                .Add(New NpgsqlParameter("titleaimai", NpgsqlTypes.NpgsqlDbType.Varchar))        'タイトル（あいまい）
                .Add(New NpgsqlParameter("naiyoaimai", NpgsqlTypes.NpgsqlDbType.Varchar))        '内容（あいまい）
                .Add(New NpgsqlParameter("taisyoaimai", NpgsqlTypes.NpgsqlDbType.Varchar))       '対処(あいまい)
                .Add(New NpgsqlParameter("bikoaimai", NpgsqlTypes.NpgsqlDbType.Varchar))         'フリーテキスト（あいまい）
                .Add(New NpgsqlParameter("ChgTantIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))    '担当者ID(あいまい)
                .Add(New NpgsqlParameter("chgtantnmaimai", NpgsqlTypes.NpgsqlDbType.Varchar))    '担当者氏名(あいまい)
                '.Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '登録日時
                '.Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '登録者グループCD
                '.Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))        '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))           '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者ID
            End With
            'バインド変数に値をセット

            With Cmd
                .Parameters("chgnmb").Value = dataHBKE0201.PropIntChgNmb                                        '新規CHG番号
                .Parameters("ProcessKbn").Value = PROCESS_TYPE_CHANGE                                           'プロセス区分
                .Parameters("ProcessStateCD").Value = dataHBKE0201.PropCmbprocessStateCD.SelectedValue          'ステータスCD(ComboBox)
                '開始日時
                If dataHBKE0201.PropDtpKaisiDT.txtDate.Text.Equals("") Then
                    .Parameters("kaisidt").Value = Nothing
                Else
                    .Parameters("kaisidt").Value = _
                        CDate(dataHBKE0201.PropDtpKaisiDT.txtDate.Text & " " & dataHBKE0201.PropTxtKaisiDT_HM.PropTxtTime.Text)
                End If
                '完了日時
                If dataHBKE0201.PropDtpKanryoDT.txtDate.Text.Equals("") Then
                    'ステータスが完了ならばシステム日付を設定する
                    If dataHBKE0201.PropCmbprocessStateCD.SelectedValue = PROCESS_STATUS_CHANGE_KANRYOU Then
                        .Parameters("kanryodt").Value = dataHBKE0201.PropDtmSysDate
                    Else
                        .Parameters("kanryodt").Value = Nothing
                    End If
                Else
                    .Parameters("kanryodt").Value = _
                        CDate(dataHBKE0201.PropDtpKanryoDT.txtDate.Text & " " & dataHBKE0201.PropTxtKanryoDT_HM.PropTxtTime.Text)
                End If
                .Parameters("Title").Value = dataHBKE0201.PropTxtTitle.Text                                     'タイトル
                .Parameters("naiyo").Value = dataHBKE0201.PropTxtNaiyo.Text                                     '内容
                .Parameters("taisyo").Value = dataHBKE0201.PropTxtTaisyo.Text                                   '対処
                .Parameters("SystemNmb").Value = dataHBKE0201.PropCmbSystemNmb.PropCmbColumns.SelectedValue     '対象システム番号(ComboBoxEx)

                .Parameters("approverid").Value = dataHBKE0201.PropTxthenkouID.Text                             '変更承認者ID
                .Parameters("approvernm").Value = dataHBKE0201.PropTxthenkouNM.Text                             '変更承認者氏名
                .Parameters("recorderid").Value = dataHBKE0201.PropTxtsyoninID.Text                             '承認記録者ID
                .Parameters("recordernm").Value = dataHBKE0201.PropTxtsyoninNM.Text                             '承認記録者氏名
                .Parameters("tantogrpcd").Value = dataHBKE0201.PropCmbTantoGrpCD.SelectedValue                  '担当グループCD(ComboBox)
                .Parameters("chgtantoid").Value = dataHBKE0201.PropTxtTantoID.Text                              '担当者ID
                .Parameters("chgtantonm").Value = dataHBKE0201.PropTxtTantoNM.Text                              '担当者氏名

                .Parameters("GroupRireki").Value = dataHBKE0201.PropTxtGrpHistory.Text                          'グループ履歴(グループ名）
                .Parameters("TantoRireki").Value = dataHBKE0201.PropTxtTantoHistory.Text                        '担当者履歴（ユーザ名）

                .Parameters("BIko1").Value = dataHBKE0201.PropTxtBIko1.Text                                     'フリーテキスト１
                .Parameters("Biko2").Value = dataHBKE0201.PropTxtBIko2.Text                                     'フリーテキスト２
                .Parameters("BIko3").Value = dataHBKE0201.PropTxtBIko3.Text                                     'フリーテキスト３
                .Parameters("Biko4").Value = dataHBKE0201.PropTxtBIko4.Text                                     'フリーテキスト４
                .Parameters("Biko5").Value = dataHBKE0201.PropTxtBIko5.Text                                     'フリーテキスト５

                'フリーフラグ１～５
                If dataHBKE0201.PropChkFreeFlg1.Checked = True Then
                    .Parameters("FreeFlg1").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg1").Value = FREE_FLG_OFF
                End If
                If dataHBKE0201.PropChkFreeFlg2.Checked = True Then
                    .Parameters("FreeFlg2").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg2").Value = FREE_FLG_OFF
                End If
                If dataHBKE0201.PropChkFreeFlg3.Checked = True Then
                    .Parameters("FreeFlg3").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg3").Value = FREE_FLG_OFF
                End If
                If dataHBKE0201.PropChkFreeFlg4.Checked = True Then
                    .Parameters("FreeFlg4").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg4").Value = FREE_FLG_OFF
                End If
                If dataHBKE0201.PropChkFreeFlg5.Checked = True Then
                    .Parameters("FreeFlg5").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg5").Value = FREE_FLG_OFF
                End If

                'あいまい検索文字列設定
                '※フリーテキスト（あいまい）：フリーテキスト１～５を連結
                strBikoAimai = commonLogicHBK.ChangeStringForSearch(dataHBKE0201.PropTxtBIko1.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKE0201.PropTxtBIko2.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKE0201.PropTxtBIko3.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKE0201.PropTxtBIko4.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKE0201.PropTxtBIko5.Text)
                .Parameters("TitleAimai").Value = _
                    commonLogicHBK.ChangeStringForSearch(dataHBKE0201.PropTxtTitle.Text)                        'タイトル（あいまい）
                .Parameters("naiyoaimai").Value = _
                    commonLogicHBK.ChangeStringForSearch(dataHBKE0201.PropTxtNaiyo.Text)                        '内容（あいまい）
                .Parameters("taisyoaimai").Value = _
                    commonLogicHBK.ChangeStringForSearch(dataHBKE0201.PropTxtTaisyo.Text)                       '対処(あいまい)
                .Parameters("BikoAimai").Value = strBikoAimai                                                   'フリーテキスト（あいまい）
                .Parameters("ChgTantIDAimai").Value = _
                    commonLogicHBK.ChangeStringForSearch(dataHBKE0201.PropTxtTantoID.Text)                      '担当者ID(あいまい)
                .Parameters("chgtantnmaimai").Value = _
                    commonLogicHBK.ChangeStringForSearch(dataHBKE0201.PropTxtTantoNM.Text)                      '担当者氏名(あいまい)

                '.Parameters("RegDT").Value = dataHBKE0201.PropDtmSysDate                                        '登録日時
                '.Parameters("RegGrpCD").Value = PropWorkGroupCD                                                 '登録者グループCD
                '.Parameters("RegID").Value = PropUserId                                                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKE0201.PropDtmSysDate                                     '最終更新日時
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
    ''' 【編集モード】対応関係者情報更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>対応関係者情報更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeletekankeiSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Try

            'SQL文(Delete)
            strSQL = strDeleteKankeiSql

            'データアダプタに、SQLのDelete文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            With Cmd.Parameters
                .Add(New NpgsqlParameter("chgnmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'CHG番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("chgnmb").Value = dataHBKE0201.PropIntChgNmb
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
    ''' 【編集モード】プロセスリンク(元)情報更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>プロセスリンク情報更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function DeletePLinkMoto(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Try

            'SQL文(Delete)
            strSQL = strDeletePLinkSql

            'データアダプタに、SQLのDelete文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LinkMotoNmb", NpgsqlTypes.NpgsqlDbType.Integer))               'CHG番号
                .Add(New NpgsqlParameter("LinkMotoProcesskbn", NpgsqlTypes.NpgsqlDbType.Varchar))        '区分
                .Add(New NpgsqlParameter("LinkSakiNmb", NpgsqlTypes.NpgsqlDbType.Integer))               'CHG番号
                .Add(New NpgsqlParameter("LinkSakiProcesskbn", NpgsqlTypes.NpgsqlDbType.Varchar))        '区分
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("LinkMotoNmb").Value = dataHBKE0201.PropIntChgNmb                            'CHG番号
                .Parameters("LinkMotoProcesskbn").Value = PROCESS_TYPE_CHANGE
                .Parameters("LinkSakiNmb").Value = dataHBKE0201.PropRowReg("MngNmb", DataRowVersion.Original)
                .Parameters("LinkSakiProcesskbn").Value = dataHBKE0201.PropRowReg("processkbn", DataRowVersion.Original)
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
    ''' 【編集モード】プロセスリンク(先)情報更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>プロセスリンク情報更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function DeletePLinkSaki(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Try

            'SQL文(Delete)
            strSQL = strDeletePLinkSql

            'データアダプタに、SQLのDelete文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LinkMotoNmb", NpgsqlTypes.NpgsqlDbType.Integer))               'CHG番号
                .Add(New NpgsqlParameter("LinkMotoProcesskbn", NpgsqlTypes.NpgsqlDbType.Varchar))        '区分
                .Add(New NpgsqlParameter("LinkSakiNmb", NpgsqlTypes.NpgsqlDbType.Integer))               'CHG番号
                .Add(New NpgsqlParameter("LinkSakiProcesskbn", NpgsqlTypes.NpgsqlDbType.Varchar))        '区分
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("LinkMotoNmb").Value = dataHBKE0201.PropRowReg("MngNmb", DataRowVersion.Original)
                .Parameters("LinkMotoProcesskbn").Value = dataHBKE0201.PropRowReg("processkbn", DataRowVersion.Original)
                .Parameters("LinkSakiNmb").Value = dataHBKE0201.PropIntChgNmb                            'CHG番号
                .Parameters("LinkSakiProcesskbn").Value = PROCESS_TYPE_CHANGE
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
    ''' 【編集モード】CYSPR情報更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CYSPR情報更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteCysprSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Try

            'SQL文(Delete)
            strSQL = strDeleteCysprSql

            'データアダプタに、SQLのDelete文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("chgnmb", NpgsqlTypes.NpgsqlDbType.Integer))               '管理番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("chgnmb").Value = dataHBKE0201.PropIntChgNmb
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
    ''' 【編集モード】会議結果情報更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議結果情報更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteMtgResultSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Try

            'SQL文(Delete)
            strSQL = strDeleteMtgResultSql

            'データアダプタに、SQLのDelete文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("processnmb", NpgsqlTypes.NpgsqlDbType.Integer))               'プロセス番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("processnmb").Value = dataHBKE0201.PropIntChgNmb
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
    ''' 【共通】新規ログNo取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規ログNo取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/23 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewLogNoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectNewLogNoSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("chgnmb", NpgsqlTypes.NpgsqlDbType.Integer))   'CHG番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("chgnmb").Value = dataHBKE0201.PropIntChgNmb
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
    ''' 【共通】新規ログNo（会議用）取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規ログNo（会議用）取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/23 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewMeetingLogNoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectNewMeetingLogNoSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("MeetingNmb", NpgsqlTypes.NpgsqlDbType.Integer))   '会議番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("MeetingNmb").Value = dataHBKE0201.PropIntMeetingNmb
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
    ''' 【共通】共通情報ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>共通情報ログ新規登録用のdataHBKE0201SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/23 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertMainInfoLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertMainInfoLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
                .Add(New NpgsqlParameter("chgnmb", NpgsqlTypes.NpgsqlDbType.Integer))       'CHG番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKE0201.PropIntLogNo
                .Parameters("chgnmb").Value = dataHBKE0201.PropIntChgNmb
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
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>対応関係情報ログ新規登録用のdataHBKE0201SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/23 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertKankeiLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertKankeiLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
                .Add(New NpgsqlParameter("chgnmb", NpgsqlTypes.NpgsqlDbType.Integer))       'CHG番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKE0201.PropIntLogNo
                .Parameters("chgnmb").Value = dataHBKE0201.PropIntChgNmb
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
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>プロセスリンク情報ログ新規登録用のdataHBKE0201SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/23 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertPLinkmotoLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKE0201 As DataHBKE0201) As Boolean

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
                .Add(New NpgsqlParameter("chgnmb", NpgsqlTypes.NpgsqlDbType.Integer))       'CHG番号
                .Add(New NpgsqlParameter("pkbn", NpgsqlTypes.NpgsqlDbType.Varchar))         'プロセス区分
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKE0201.PropIntLogNo
                .Parameters("chgnmb").Value = dataHBKE0201.PropIntChgNmb
                .Parameters("pkbn").Value = PROCESS_TYPE_CHANGE
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
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>関連ファイル情報ログ新規登録用のdataHBKE0201SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/23 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertFileLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertFileLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
                .Add(New NpgsqlParameter("chgnmb", NpgsqlTypes.NpgsqlDbType.Integer))       'CHG番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKE0201.PropIntLogNo
                .Parameters("chgnmb").Value = dataHBKE0201.PropIntChgNmb
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
    ''' 【共通】CYSPR情報ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CYSPR情報ログ新規登録用のdataHBKE0201SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/23 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCYSPRLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertCysprLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
                .Add(New NpgsqlParameter("chgnmb", NpgsqlTypes.NpgsqlDbType.Integer))       'CHG番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKE0201.PropIntLogNo
                .Parameters("chgnmb").Value = dataHBKE0201.PropIntChgNmb
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
    ''' 【共通】会議結果情報ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議情報ログ新規登録用のdataHBKE0201SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/23 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertMtgResultLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertMtgResultLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))            'ログNo
                .Add(New NpgsqlParameter("meetingnmb", NpgsqlTypes.NpgsqlDbType.Integer))       '会議No
                .Add(New NpgsqlParameter("processnmb", NpgsqlTypes.NpgsqlDbType.Integer))       'プロセス番号
                .Add(New NpgsqlParameter("processkbn", NpgsqlTypes.NpgsqlDbType.Varchar))       'プロセス区分
                .Add(New NpgsqlParameter("ProcessLogNo", NpgsqlTypes.NpgsqlDbType.Integer))     'プロセスログ番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKE0201.PropIntLogNoSub
                .Parameters("meetingnmb").Value = dataHBKE0201.PropIntMeetingNmb
                .Parameters("processnmb").Value = dataHBKE0201.PropIntChgNmb
                .Parameters("processkbn").Value = PROCESS_TYPE_CHANGE
                .Parameters("ProcessLogNo").Value = dataHBKE0201.PropIntLogNo
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
    ''' 【共通】会議情報ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議情報ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/06 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertMeetingLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertMeetingLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
                .Add(New NpgsqlParameter("MeetingNmb", NpgsqlTypes.NpgsqlDbType.Integer))   '会議番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKE0201.PropIntLogNoSub
                .Parameters("MeetingNmb").Value = dataHBKE0201.PropIntMeetingNmb
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
    ''' 【共通】会議出席者情報ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議出席者情報ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertMtgAttendLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertMtgAttendLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
                .Add(New NpgsqlParameter("MeetingNmb", NpgsqlTypes.NpgsqlDbType.Integer))   '会議番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKE0201.PropIntLogNoSub
                .Parameters("MeetingNmb").Value = dataHBKE0201.PropIntMeetingNmb
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
    ''' 【共通】会議関連ファイル情報ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議関連ファイル情報ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertMtgFileLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertMtgFileLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
                .Add(New NpgsqlParameter("MeetingNmb", NpgsqlTypes.NpgsqlDbType.Integer))   '会議番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKE0201.PropIntLogNoSub
                .Parameters("MeetingNmb").Value = dataHBKE0201.PropIntMeetingNmb
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
    ''' 【共通】対応関係者取得：対象システム
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>対象システムにおける対応関係者存在チェック用SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetChkKankeiSysData(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strCheckSysKankeiUSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("chgnmb", NpgsqlTypes.NpgsqlDbType.Integer))                           '対象システム
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("chgnmb").Value = dataHBKE0201.PropCmbSystemNmb.PropCmbColumns.SelectedValue        '対象システム
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
    ''' L-1-1-1-1.共通情報ロックテーブル、サーバー日付取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>指定された管理番号の共通情報ロックテーブルおよびサーバー日付を返す
    ''' <para>作成情報：2012/08/28 r.hoshino
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function SelectLock(ByVal Adapter As NpgsqlDataAdapter, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByVal intNmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'SQL変数宣言
        Dim strSQL As String = ""

        Try

            strSQL = strSelectInfoSql

            ' データアダプタに、共通情報ロックテーブル取得用SQLを設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("Nmb", NpgsqlTypes.NpgsqlDbType.Integer))       '管理番号

            'バインド変数に値をセット
            Adapter.SelectCommand.Parameters("Nmb").Value = intNmb                                                   '管理番号

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Adapter.SelectCommand)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' L-2-1-1.共通情報ロックテーブル登録処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>共通情報ロックテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/28 r.hoshino
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function InsertLockSql(ByRef Cmd As NpgsqlCommand, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByVal intNmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        '変数宣言
        Dim strSQL As String = ""

        Try

            strSQL = strInsertLockSql

            'データアダプタに、SQLのDelete文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            Cmd.Parameters.Add(New NpgsqlParameter("EdiGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))   '編集者グループコード
            Cmd.Parameters.Add(New NpgsqlParameter("EdiID", NpgsqlTypes.NpgsqlDbType.Varchar))      '編集者ID
            Cmd.Parameters.Add(New NpgsqlParameter("Nmb", NpgsqlTypes.NpgsqlDbType.Integer))        '管理番号

            'バインド変数に値をセット
            Cmd.Parameters("EdiGrpCD").Value = PropWorkGroupCD                                      '編集者グループコード
            Cmd.Parameters("EdiID").Value = PropUserId                                              '編集者ID
            Cmd.Parameters("Nmb").Value = intNmb                                                    '管理番号

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Cmd)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' J-1-1.共通情報ロックテーブル削除処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>番号をキーに共通情報ロックテーブルのデータを物理削除（DELETE）する
    ''' <para>作成情報：2012/08/28 r.hoshino
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function DeleteLockSql(ByRef Cmd As NpgsqlCommand, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByVal intNmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        '変数宣言
        Dim strSQL As String = ""

        Try

            strSQL = strDeleteLockSql

            'コマンドに、削除用SQLを設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            Cmd.Parameters.Add(New NpgsqlParameter("Nmb", NpgsqlTypes.NpgsqlDbType.Integer))         '管理番号

            'バインド変数に値をセット
            Cmd.Parameters("Nmb").Value = intNmb                                                     '管理番号

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Cmd)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function





    ''' <summary>
    ''' 【共通】対応関係者取得：所属グループ
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <param name="StrKbn">[IN]プロセス区分</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>関係者テーブル情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetChkKankeiSZKData(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal intNmb As Integer, _
                                       ByVal StrKbn As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'プロセス区分によりSQLを分ける
            Select Case StrKbn
                Case PROCESS_TYPE_INCIDENT
                    strSQL = strCheckIncKankeiSZKSql
                Case PROCESS_TYPE_QUESTION
                    strSQL = strCheckPrbKankeiSZKSql
                Case PROCESS_TYPE_CHANGE
                    strSQL = strCheckChgKankeiSZKSql
                Case PROCESS_TYPE_RELEASE
                    strSQL = strCheckRelKankeiSZKSql

            End Select


            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("Nmb", NpgsqlTypes.NpgsqlDbType.Integer))             '管理番号
                .Add(New NpgsqlParameter("KbnGrp", NpgsqlTypes.NpgsqlDbType.Varchar))          '区分：グループ
                .Add(New NpgsqlParameter("UsrID", NpgsqlTypes.NpgsqlDbType.Varchar))           'ユーザーID
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("Nmb").Value = intNmb
                .Parameters("KbnGrp").Value = KBN_GROUP
                .Parameters("UsrID").Value = PropUserId
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
    ''' 【共通】対応関係者取得：区分G
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <param name="StrKbn">[IN]プロセス区分</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>関係者テーブル情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetChkKankeiGData(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal intNmb As Integer, _
                                       ByVal StrKbn As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'プロセス区分によりSQLを分ける
            Select Case StrKbn
                Case PROCESS_TYPE_INCIDENT
                    strSQL = strCheckIncKankeiGSql
                Case PROCESS_TYPE_QUESTION
                    strSQL = strCheckPrbKankeiGSql
                Case PROCESS_TYPE_CHANGE
                    strSQL = strCheckChgKankeiGSql
                Case PROCESS_TYPE_RELEASE
                    strSQL = strCheckRelKankeiGSql

            End Select

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("Nmb", NpgsqlTypes.NpgsqlDbType.Integer))              '管理番号
                .Add(New NpgsqlParameter("KbnGrp", NpgsqlTypes.NpgsqlDbType.Varchar))           '区分：グループ
                .Add(New NpgsqlParameter("GrpID", NpgsqlTypes.NpgsqlDbType.Varchar))            'グループID
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("Nmb").Value = intNmb
                .Parameters("KbnGrp").Value = KBN_GROUP
                .Parameters("GrpID").Value = PropWorkGroupCD
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
    ''' 【共通】対応関係者取得：区分U
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <param name="StrKbn">[IN]プロセス区分</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>関係者テーブル情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetChkKankeiUData(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal intNmb As Integer, _
                                       ByVal StrKbn As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'プロセス区分によりSQLを分ける
            Select Case StrKbn
                Case PROCESS_TYPE_INCIDENT
                    strSQL = strCheckIncKankeiUSql
                Case PROCESS_TYPE_QUESTION
                    strSQL = strCheckPrbKankeiUSql
                Case PROCESS_TYPE_CHANGE
                    strSQL = strCheckChgKankeiUSql
                Case PROCESS_TYPE_RELEASE
                    strSQL = strCheckRelKankeiUSql

            End Select

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("Nmb", NpgsqlTypes.NpgsqlDbType.Integer))              '管理番号
                .Add(New NpgsqlParameter("KbnUsr", NpgsqlTypes.NpgsqlDbType.Varchar))           '区分：ユーザー
                .Add(New NpgsqlParameter("UsrID", NpgsqlTypes.NpgsqlDbType.Varchar))            'ユーザーID
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("Nmb").Value = intNmb
                .Parameters("KbnUsr").Value = KBN_USER
                .Parameters("UsrID").Value = PropUserId
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
    ''' 【新規登録／編集モード】担当履歴情報　新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>担当履歴情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/10 r.hoshino
    ''' <p>改訂情報</p>
    ''' </para></remarks>
    Public Function SetInsertTantoRirekiSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Dim strworknaiyoaimai As String = ""   '作業内容(あいまい)

        Try

            'SQL文(INSERT)
            strSQL = strInsertTantoRirekiSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("chgNmb", NpgsqlTypes.NpgsqlDbType.Integer))           '管理番号
                .Add(New NpgsqlParameter("tantogrpcd", NpgsqlTypes.NpgsqlDbType.Varchar))       'グループcd
                .Add(New NpgsqlParameter("tantogrpnm", NpgsqlTypes.NpgsqlDbType.Varchar))       'グループ名
                .Add(New NpgsqlParameter("chgtantoid", NpgsqlTypes.NpgsqlDbType.Varchar))       '担当ＩＤ
                .Add(New NpgsqlParameter("chgtantonm", NpgsqlTypes.NpgsqlDbType.Varchar))       '担当名

                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))        '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))           '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("chgNmb").Value = dataHBKE0201.PropIntChgNmb
                .Parameters("tantogrpcd").Value = dataHBKE0201.PropCmbTantoGrpCD.SelectedValue
                .Parameters("tantogrpnm").Value = dataHBKE0201.PropCmbTantoGrpCD.Text
                .Parameters("chgtantoid").Value = dataHBKE0201.PropTxtTantoID.Text
                .Parameters("chgtantonm").Value = dataHBKE0201.PropTxtTantoNM.Text

                .Parameters("RegDT").Value = dataHBKE0201.PropDtmSysDate                         '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                  '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                          '登録者ID
                .Parameters("UpdateDT").Value = dataHBKE0201.PropDtmSysDate                      '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                   '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                       '最終更新者ID


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
    ''' 【編集／参照モード】担当履歴取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>担当履歴情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/10 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectTantoRirekiSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectTantoRirekiSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("chgNmb", NpgsqlTypes.NpgsqlDbType.Integer))        '管理番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("chgNmb").Value = dataHBKE0201.PropIntChgNmb                      '管理番号
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
    ''' 【共通】チェック用対応関係者：対象システム
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>INC共通情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/10/11 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetChkSysNmbData(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strCheckChgSystemNmbSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("Nmb", NpgsqlTypes.NpgsqlDbType.Integer))                           '管理番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("Nmb").Value = DataHBKE0201.PropIntChgNmb
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
