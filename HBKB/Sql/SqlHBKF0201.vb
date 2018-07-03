Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' リリース登録画面Sqlクラス
''' </summary>
''' <remarks>リリース登録画面のSQLの作成・設定を行う
''' <para>作成情報：2012/08/31 s.tsuruta
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKF0201

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
    Private strCheckRelSystemNmbSql As String = "SELECT " & vbCrLf & _
                                                " systemnmb " & vbCrLf & _
                                                "FROM release_system_tb ct " & vbCrLf & _
                                                "WHERE ct.Relnmb= :Nmb " & vbCrLf & _
                                                "AND ct.relsystemkbn= :systemkbn " & vbCrLf & _
                                                "AND ct.EntryNmb =:EntryNmb "


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
                                     "FROM release_info_lock_tb crt" & vbCrLf & _
                                     "LEFT JOIN GRP_MTB gm ON crt.EdiGrpCD=gm.GroupCD" & vbCrLf & _
                                     "LEFT JOIN HBKUSR_MTB hm ON crt.EdiID=hm.HBKUsrID" & vbCrLf & _
                                     "WHERE Relnmb=:Nmb"

    'プロセスステータスマスタ
    Private strSelectProcessStateMastaSql As String = "SELECT " & vbCrLf & _
                                                      " processstatecd " & vbCrLf & _
                                                      ",processstatenm " & vbCrLf & _
                                                      "FROM  processstate_mtb " & vbCrLf & _
                                                      "WHERE COALESCE(jtiFlg,'0') <>'1' " & vbCrLf & _
                                                      "AND processkbn = :processkbn " & vbCrLf & _
                                                      "ORDER BY Sort "

    'グループマスタ
    '[mod] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 START
    'Private strSelectTantoGroupMastaSql As String = "SELECT " & vbCrLf & _
    '                                                " groupcd " & vbCrLf & _
    '                                                ",groupnm " & vbCrLf & _
    '                                                "FROM  grp_mtb " & vbCrLf & _
    '                                                "WHERE COALESCE(jtiFlg,'0') <>'1' " & vbCrLf & _
    '                                                "ORDER BY Sort "
    Private strSelectTantoGroupMastaSql As String = "SELECT " & vbCrLf & _
                                                " groupcd " & vbCrLf & _
                                                ",groupnm " & vbCrLf & _
                                                "FROM  grp_mtb " & vbCrLf & _
                                                "WHERE COALESCE(jtiFlg,'0') <>'1' OR GroupCD IN (SELECT TantoGrpCD FROM release_info_tb WHERE RelNmb = :RelNmb) " & vbCrLf & _
                                                "ORDER BY jtiFlg,Sort "
    '[mod] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 END

    'リリース共通情報取得（SELECT）SQL
    Private strSelectReleaseInfoSql As String = "SELECT " & vbCrLf & _
                                                " rt.RelNmb " & vbCrLf & _
                                                ",rt.RelUkeNmb " & vbCrLf & _
                                                ",rt.ProcessStateCD " & vbCrLf & _
                                                ",CASE WHEN rt.IraiDT IS NULL" & vbCrLf & _
                                                " THEN '' " & vbCrLf & _
                                                " ELSE to_char(rt.IraiDT,'YYYY/MM/DD HH24:MI') END AS IraiDT" & vbCrLf & _
                                                ",rt.Title " & vbCrLf & _
                                                ",rt.Gaiyo " & vbCrLf & _
                                                ",rt.TujyoKinkyuKbn " & vbCrLf & _
                                                ",rt.UsrSyutiKbn " & vbCrLf & _
                                                ",CASE WHEN rt.RelSceDT IS NULL" & vbCrLf & _
                                                " THEN '' " & vbCrLf & _
                                                " ELSE to_char(rt.RelSceDT,'YYYY/MM/DD HH24:MI') END AS RelSceDT" & vbCrLf & _
                                                ",CASE WHEN rt.RelStDT IS NULL" & vbCrLf & _
                                                " THEN '' " & vbCrLf & _
                                                " ELSE to_char(rt.RelStDT,'YYYY/MM/DD HH24:MI') END AS RelStDT" & vbCrLf & _
                                                ",CASE WHEN rt.RelEdDT IS NULL" & vbCrLf & _
                                                " THEN '' " & vbCrLf & _
                                                " ELSE to_char(rt.RelEdDT,'YYYY/MM/DD HH24:MI') END AS RelEdDT" & vbCrLf & _
                                                ",rt.TantoGrpCD " & vbCrLf & _
                                                ",rt.RelTantoID " & vbCrLf & _
                                                ",rt.RelTantoNM " & vbCrLf & _
                                                ",rt.BIko1 " & vbCrLf & _
                                                ",rt.Biko2 " & vbCrLf & _
                                                ",rt.Biko3 " & vbCrLf & _
                                                ",rt.Biko4 " & vbCrLf & _
                                                ",rt.Biko5 " & vbCrLf & _
                                                ",rt.FreeFlg1 " & vbCrLf & _
                                                ",rt.FreeFlg2 " & vbCrLf & _
                                                ",rt.FreeFlg3 " & vbCrLf & _
                                                ",rt.FreeFlg4 " & vbCrLf & _
                                                ",rt.FreeFlg5 " & vbCrLf & _
                                                ",rt.RegDT " & vbCrLf & _
                                                ",rt.RegGrpCD " & vbCrLf & _
                                                ",gm1.GroupNM AS RegGrpNM " & vbCrLf & _
                                                ",rt.RegID " & vbCrLf & _
                                                ",gm2.HbkUsrNM AS RegHbkUsrNM " & vbCrLf & _
                                                ",rt.UpdateDT " & vbCrLf & _
                                                ",rt.UpGrpCD " & vbCrLf & _
                                                ",gm3.GroupNM AS UpGrpNM " & vbCrLf & _
                                                ",rt.UpdateID " & vbCrLf & _
                                                ",gm4.HbkUsrNM AS UpHbkUsrNM " & vbCrLf & _
                                                "FROM RELEASE_INFO_TB rt " & vbCrLf & _
                                                "LEFT JOIN GRP_MTB    gm1 ON gm1.groupcd  = rt.RegGrpCD " & vbCrLf & _
                                                "LEFT JOIN HBKUSR_MTB gm2 ON gm2.hbkusrid = rt.RegID " & vbCrLf & _
                                                "LEFT JOIN GRP_MTB    gm3 ON gm3.groupcd  = rt.UpGrpCD " & vbCrLf & _
                                                "LEFT JOIN HBKUSR_MTB gm4 ON gm4.hbkusrid = rt.UpdateID " & vbCrLf & _
                                                "WHERE rt.RelNmb = :RelNmb"

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


    'CI共通情報取得（SELECT）SQL
    Private strSelectCIInfoSql As String = "SELECT " & vbCrLf & _
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

    'リリースシステム取得（SELECT）SQL
    Private strSelectReleaseSystemSql As String = "SELECT " & vbCrLf & _
                                                  " ct.CINmb " & vbCrLf & _
                                                  ",rt.EntryNmb " & vbCrLf & _
                                                  ",rt.RegDt " & vbCrLf & _
                                                  ",rt.RegGrpCD " & vbCrLf & _
                                                  ",rt.RegID " & vbCrLf & _
                                                  "FROM RELEASE_SYSTEM_TB rt " & vbCrLf & _
                                                  "LEFT JOIN CI_INFO_TB ct ON ct.CINmb = rt.SystemNmb " & vbCrLf & _
                                                  "WHERE rt.RelNmb = :RelNmb " & vbCrLf & _
                                                  "and rt.RelSystemKbn = :RelSystemKbn" & vbCrLf & _
                                                  "ORDER BY EntryNmb"
    '"ORDER BY rt.RegDT"

    'リリース関連ファイル取得（SELECT）SQL
    Private strSelectReleaseFileSql As String = "SELECT " & vbCrLf & _
                                                " rt.filenaiyo AS FileNaiyo" & vbCrLf & _
                                                ",rt.filemngnmb AS FileMngNmb " & vbCrLf & _
                                                ",m1.filepath||E'\\'||m1.filenm||m1.ext AS FilePath" & vbCrLf & _
                                                ",rt.RegDT" & vbCrLf & _
                                                ",rt.RegGrpCD " & vbCrLf & _
                                                ",rt.RegID " & vbCrLf & _
                                                "FROM RELEASE_FILE_TB rt " & vbCrLf & _
                                                "INNER JOIN file_mng_tb m1 ON m1.filemngnmb=rt.filemngnmb " & vbCrLf & _
                                                "WHERE rt.RelNmb = :RelNmb " & vbCrLf & _
                                                "ORDER BY rt.entrynmb "

    '[SELECT]新規ログNo（会議用）取得SQL
    Private strSelectNewMeetingLogNoSql As String = "SELECT " & vbCrLf & _
                                                    "COALESCE(MAX(ML.LogNo),0)+1 AS LogNo " & vbCrLf & _
                                                    "FROM MEETING_LTB ML " & vbCrLf & _
                                                    "WHERE ML.MeetingNmb = :MeetingNmb "

    '会議情報/会議結果情報取得（SELECT）SQL
    Private strSelectMeetingSql As String = "SELECT " & vbCrLf & _
                                            " mt.MeetingNmb  " & vbCrLf & _
                                            ",TO_CHAR(mt.JisiSTDT,'YYYY/MM/DD') AS JisiDT " & vbCrLf & _
                                            ",CASE mrt.ResultKbn " & vbCrLf & _
                                            " WHEN :Kbn_NO THEN :Kbn_NO_NM " & vbCrLf & _
                                            " WHEN :Kbn_OK THEN :Kbn_OK_NM " & vbCrLf & _
                                            " WHEN :Kbn_NG THEN :Kbn_NG_NM " & vbCrLf & _
                                            " ELSE '' END AS ResultKbnNM " & vbCrLf & _
                                            ",mt.Title " & vbCrLf & _
                                            ",mrt.ResultKbn " & vbCrLf & _
                                            ",mrt.RegDt " & vbCrLf & _
                                            ",mrt.RegGrpCD " & vbCrLf & _
                                            ",mrt.RegID " & vbCrLf & _
                                            "FROM MEETING_RESULT_TB mrt" & vbCrLf & _
                                            "LEFT JOIN MEETING_TB mt ON mt.MeetingNmb = mrt.MeetingNmb " & vbCrLf & _
                                            "WHERE  mrt.processkbn = :processkbn " & vbCrLf & _
                                            "AND  mrt.processnmb = :processnmb " & vbCrLf & _
                                            "ORDER BY mt.JisiSTDT DESC ,mt.Title"

    'リリース対応関係者取得（SELECT）SQL
    Private strSelectReleaseKankeiSql As String = "SELECT " & vbCrLf & _
                                                " t.RelationKbn " & vbCrLf & _
                                                ",t.RelationID " & vbCrLf & _
                                                ",t.GroupNM " & vbCrLf & _
                                                ",t.HBKUsrNM " & vbCrLf & _
                                                ",t.RegDt " & vbCrLf & _
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
                                                "  FROM RELEASE_KANKEI_TB kt " & vbCrLf & _
                                                "   INNER JOIN GRP_MTB gm ON kt.RelationID = gm.GroupCD " & vbCrLf & _
                                                "  WHERE kt.RelNmb = :RelNmb " & vbCrLf & _
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
                                                "  FROM RELEASE_KANKEI_TB kt " & vbCrLf & _
                                                "   INNER JOIN HBKUSR_MTB hm ON hm.HBKUsrID = kt.RelationID " & vbCrLf & _
                                                "  WHERE kt.RelNmb= :RelNmb  " & vbCrLf & _
                                                "   AND kt.RelationKbn = :KbnUsr " & vbCrLf & _
                                                ") t  " & vbCrLf & _
                                                "ORDER BY t.entrynmb  "

    'プロセスリンク取得（SELECT）SQL
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
                                          "WHERE kt1.LinkMotoNmb = :RelNmb " & vbCrLf & _
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
                                          "WHERE kt2.LinkSakiNmb = :RelNmb " & vbCrLf & _
                                          "AND   kt2.LinkSakiProcesskbn = :LinkMotoProcesskbn " & vbCrLf & _
                                          ") t " & vbCrLf & _
                                          "ORDER BY t.entryDT  "

    ''[SELECT]ユーザマスタ(担当IDEnter取得用)
    'Private strSelectHbkUsrMstSql As String = "SELECT " & vbCrLf & _
    '                                          " hbkusrnm " & vbCrLf & _
    '                                          ",hbkusrnmkana " & vbCrLf & _
    '                                          "FROM  hbkusr_mtb " & vbCrLf & _
    '                                          "WHERE hbkusrid = :hbkusrid " 

    '[SELECT]ユーザマスタ(担当IDEnter取得用)
    Private strSelectHbkUsrMstSql As String = "SELECT " & vbCrLf & _
                                              " hbkusrnm " & vbCrLf & _
                                              ",hbkusrnmkana " & vbCrLf & _
                                              ",groupcd " & vbCrLf & _
                                              "FROM  hbkusr_mtb m1" & vbCrLf & _
                                              "LEFT JOIN szk_mtb m2 ON m1.hbkusrid=m2.hbkusrid " & vbCrLf & _
                                              "WHERE m1.hbkusrid = :hbkusrid "




    'リリース共通情報新規登録（INSERT）SQL
    Private strInsertRelInfoSql As String = "INSERT INTO RELEASE_INFO_TB ( " & vbCrLf & _
                                            " RelNmb " & vbCrLf & _
                                            ",ProcessKbn " & vbCrLf & _
                                            ",ProcessStateCD " & vbCrLf & _
                                            ",RelUkeNmb " & vbCrLf & _
                                            ",IraiDT " & vbCrLf & _
                                            ",Title " & vbCrLf & _
                                            ",Gaiyo " & vbCrLf & _
                                            ",TujyoKinkyuKbn " & vbCrLf & _
                                            ",UsrSyutiKbn " & vbCrLf & _
                                            ",RelSceDT " & vbCrLf & _
                                            ",RelStDT " & vbCrLf & _
                                            ",RelEdDT " & vbCrLf & _
                                            ",TantoGrpCD " & vbCrLf & _
                                            ",RelTantoID " & vbCrLf & _
                                            ",RelTantoNM " & vbCrLf & _
                                            ",Biko1 " & vbCrLf & _
                                            ",Biko2 " & vbCrLf & _
                                            ",Biko3 " & vbCrLf & _
                                            ",Biko4 " & vbCrLf & _
                                            ",Biko5 " & vbCrLf & _
                                            ",FreeFlg1 " & vbCrLf & _
                                            ",FreeFlg2 " & vbCrLf & _
                                            ",FreeFlg3 " & vbCrLf & _
                                            ",FreeFlg4 " & vbCrLf & _
                                            ",FreeFlg5 " & vbCrLf & _
                                            ",RelUkeNmbAimai " & vbCrLf & _
                                            ",TitleAimai " & vbCrLf & _
                                            ",GaiyoAimai " & vbCrLf & _
                                            ",BikoAimai " & vbCrLf & _
                                            ",RelTantIDAimai " & vbCrLf &
                                            ",RelTantNMAimai " & vbCrLf & _
                                            ",RegDT " & vbCrLf & _
                                            ",RegGrpCD " & vbCrLf & _
                                            ",RegID " & vbCrLf & _
                                            ",UpdateDT " & vbCrLf & _
                                            ",UpGrpCD " & vbCrLf & _
                                            ",UpdateID " & vbCrLf & _
                                            ") VALUES (" & vbCrLf & _
                                            " :RelNmb " & vbCrLf & _
                                            ",:ProcessKbn " & vbCrLf & _
                                            ",:ProcessStateCD " & vbCrLf & _
                                            ",:RelUkeNmb " & vbCrLf & _
                                            ",:IraiDT " & vbCrLf & _
                                            ",:Title " & vbCrLf & _
                                            ",:Gaiyo " & vbCrLf & _
                                            ",:TujyoKinkyuKbn " & vbCrLf & _
                                            ",:UsrSyutiKbn " & vbCrLf & _
                                            ",:RelSceDT " & vbCrLf & _
                                            ",:RelStDT " & vbCrLf & _
                                            ",:RelEdDT " & vbCrLf & _
                                            ",:TantoGrpCD " & vbCrLf & _
                                            ",:RelTantoID " & vbCrLf & _
                                            ",:RelTantoNM " & vbCrLf & _
                                            ",:Biko1 " & vbCrLf & _
                                            ",:Biko2 " & vbCrLf & _
                                            ",:Biko3 " & vbCrLf & _
                                            ",:Biko4 " & vbCrLf & _
                                            ",:Biko5 " & vbCrLf & _
                                            ",:FreeFlg1 " & vbCrLf & _
                                            ",:FreeFlg2 " & vbCrLf & _
                                            ",:FreeFlg3 " & vbCrLf & _
                                            ",:FreeFlg4 " & vbCrLf & _
                                            ",:FreeFlg5 " & vbCrLf & _
                                            ",:RelUkeNmbAimai " & vbCrLf & _
                                            ",:TitleAimai " & vbCrLf & _
                                            ",:GaiyoAimai " & vbCrLf & _
                                            ",:BikoAimai " & vbCrLf & _
                                            ",:RelTantIDAimai " & vbCrLf &
                                            ",:RelTantNMAimai " & vbCrLf & _
                                            ",:RegDT " & vbCrLf & _
                                            ",:RegGrpCD " & vbCrLf & _
                                            ",:RegID " & vbCrLf & _
                                            ",:UpdateDT " & vbCrLf & _
                                            ",:UpGrpCD " & vbCrLf & _
                                            ",:UpdateID " & vbCrLf & _
                                            ") "

    'リリース共通情報ログ新規登録（INSERT）SQL
    Private strInsertRelInfoLSql As String = "INSERT INTO RELEASE_INFO_LTB ( " & vbCrLf & _
                                             " RelNmb " & vbCrLf & _
                                             ",LogNo " & vbCrLf & _
                                             ",ProcessKbn " & vbCrLf & _
                                             ",ProcessStateCD " & vbCrLf & _
                                             ",RelUkeNmb " & vbCrLf & _
                                             ",IraiDT " & vbCrLf & _
                                             ",Title " & vbCrLf & _
                                             ",Gaiyo " & vbCrLf & _
                                             ",TujyoKinkyuKbn " & vbCrLf & _
                                             ",UsrSyutiKbn " & vbCrLf & _
                                             ",RelSceDT " & vbCrLf & _
                                             ",RelStDT " & vbCrLf & _
                                             ",RelEdDT " & vbCrLf & _
                                             ",TantoGrpCD " & vbCrLf & _
                                             ",RelTantoID " & vbCrLf & _
                                             ",RelTantoNM " & vbCrLf & _
                                             ",Biko1 " & vbCrLf & _
                                             ",Biko2 " & vbCrLf & _
                                             ",Biko3 " & vbCrLf & _
                                             ",Biko4 " & vbCrLf & _
                                             ",Biko5 " & vbCrLf & _
                                             ",FreeFlg1 " & vbCrLf & _
                                             ",FreeFlg2 " & vbCrLf & _
                                             ",FreeFlg3 " & vbCrLf & _
                                             ",FreeFlg4 " & vbCrLf & _
                                             ",FreeFlg5 " & vbCrLf & _
                                             ",RelUkeNmbAimai " & vbCrLf & _
                                             ",TitleAimai " & vbCrLf & _
                                             ",GaiyoAimai " & vbCrLf & _
                                             ",BikoAimai " & vbCrLf & _
                                             ",RelTantIDAimai " & vbCrLf &
                                             ",RelTantNMAimai " & vbCrLf & _
                                             ",RegDT " & vbCrLf & _
                                             ",RegGrpCD " & vbCrLf & _
                                             ",RegID " & vbCrLf & _
                                             ",UpdateDT " & vbCrLf & _
                                             ",UpGrpCD " & vbCrLf & _
                                             ",UpdateID " & vbCrLf & _
                                             ") " & vbCrLf & _
                                             "SELECT " & vbCrLf & _
                                             " RelNmb " & vbCrLf & _
                                             ",:LogNo " & vbCrLf & _
                                             ",ProcessKbn " & vbCrLf & _
                                             ",ProcessStateCD " & vbCrLf & _
                                             ",RelUkeNmb " & vbCrLf & _
                                             ",IraiDT " & vbCrLf & _
                                             ",Title " & vbCrLf & _
                                             ",Gaiyo " & vbCrLf & _
                                             ",TujyoKinkyuKbn " & vbCrLf & _
                                             ",UsrSyutiKbn " & vbCrLf & _
                                             ",RelSceDT " & vbCrLf & _
                                             ",RelStDT " & vbCrLf & _
                                             ",RelEdDT " & vbCrLf & _
                                             ",TantoGrpCD " & vbCrLf & _
                                             ",RelTantoID " & vbCrLf & _
                                             ",RelTantoNM " & vbCrLf & _
                                             ",Biko1 " & vbCrLf & _
                                             ",Biko2 " & vbCrLf & _
                                             ",Biko3 " & vbCrLf & _
                                             ",Biko4 " & vbCrLf & _
                                             ",Biko5 " & vbCrLf & _
                                             ",FreeFlg1 " & vbCrLf & _
                                             ",FreeFlg2 " & vbCrLf & _
                                             ",FreeFlg3 " & vbCrLf & _
                                             ",FreeFlg4 " & vbCrLf & _
                                             ",FreeFlg5 " & vbCrLf & _
                                             ",RelUkeNmbAimai " & vbCrLf & _
                                             ",TitleAimai " & vbCrLf & _
                                             ",GaiyoAimai " & vbCrLf & _
                                             ",BikoAimai " & vbCrLf & _
                                             ",RelTantIDAimai " & vbCrLf &
                                             ",RelTantNMAimai " & vbCrLf & _
                                             ",RegDT " & vbCrLf & _
                                             ",RegGrpCD " & vbCrLf & _
                                             ",RegID " & vbCrLf & _
                                             ",UpdateDT " & vbCrLf & _
                                             ",UpGrpCD " & vbCrLf & _
                                             ",UpdateID " & vbCrLf & _
                                             "FROM RELEASE_INFO_TB " & vbCrLf & _
                                             "WHERE RelNmb = :RelNmb "
 

    'リリース共通情報更新（UPDATE）SQL
    Private strUpdateRelInfoSql As String = "UPDATE RELEASE_INFO_TB SET " & vbCrLf & _
                                            " ProcessKbn = :ProcessKbn " & vbCrLf & _
                                            ",ProcessStateCD = :ProcessStateCD " & vbCrLf & _
                                            ",RelUkeNmb = :RelUkeNmb " & vbCrLf & _
                                            ",IraiDT = :IraiDT " & vbCrLf & _
                                            ",Title = :Title " & vbCrLf & _
                                            ",Gaiyo = :Gaiyo " & vbCrLf & _
                                            ",TujyoKinkyuKbn = :TujyoKinkyuKbn " & vbCrLf & _
                                            ",UsrSyutiKbn = :UsrSyutiKbn " & vbCrLf & _
                                            ",RelSceDT = :RelSceDT " & vbCrLf & _
                                            ",RelStDT = :RelStDT " & vbCrLf & _
                                            ",RelEdDT = :RelEdDT " & vbCrLf & _
                                            ",TantoGrpCD = :TantoGrpCD " & vbCrLf & _
                                            ",RelTantoID = :RelTantoID " & vbCrLf & _
                                            ",RelTantoNM = :RelTantoNM " & vbCrLf & _
                                            ",Biko1 = :Biko1 " & vbCrLf & _
                                            ",Biko2 = :Biko2 " & vbCrLf & _
                                            ",Biko3 = :Biko3 " & vbCrLf & _
                                            ",Biko4 = :Biko4 " & vbCrLf & _
                                            ",Biko5 = :Biko5 " & vbCrLf & _
                                            ",FreeFlg1 = :FreeFlg1 " & vbCrLf & _
                                            ",FreeFlg2 = :FreeFlg2 " & vbCrLf & _
                                            ",FreeFlg3 = :FreeFlg3 " & vbCrLf & _
                                            ",FreeFlg4 = :FreeFlg4 " & vbCrLf & _
                                            ",FreeFlg5 = :FreeFlg5 " & vbCrLf & _
                                            ",RelUkeNmbAimai = :RelUkeNmbAimai " & vbCrLf & _
                                            ",TitleAimai = :TitleAimai " & vbCrLf & _
                                            ",GaiyoAimai = :GaiyoAimai " & vbCrLf & _
                                            ",BikoAimai = :BikoAimai " & vbCrLf & _
                                            ",RelTantIDAimai = :RelTantIDAimai " & vbCrLf &
                                            ",RelTantNMAimai = :RelTantNMAimai " & vbCrLf & _
                                            ",UpdateDT = :UpdateDT " & vbCrLf & _
                                            ",UpGrpCD = :UpGrpCD " & vbCrLf & _
                                            ",UpdateID = :UpdateID " & vbCrLf & _
                                            "WHERE RelNmb =:RelNmb "



    'リリース対象システム（INSERT）SQL
    Private strInsertRelSystemSql As String = "INSERT INTO RELEASE_SYSTEM_TB ( " & vbCrLf & _
                                              " RelNmb " & vbCrLf & _
                                              ",RelSystemKbn " & vbCrLf & _
                                              ",SystemNmb " & vbCrLf & _
                                              ",EntryNmb " & vbCrLf & _
                                              ",RegDT " & vbCrLf & _
                                              ",RegGrpCD " & vbCrLf & _
                                              ",RegID " & vbCrLf & _
                                              ",UpdateDT " & vbCrLf & _
                                              ",UpGrpCD " & vbCrLf & _
                                              ",UpdateID " & vbCrLf & _
                                              ") VALUES (" & vbCrLf & _
                                              " :RelNmb " & vbCrLf & _
                                              ",:RelSystemKbn " & vbCrLf & _
                                              ",:SystemNmb " & vbCrLf & _
                                              ",(SELECT COALESCE(MAX(EntryNmb),0)+1 FROM RELEASE_SYSTEM_TB WHERE RelNmb=:RelNmb) " & vbCrLf & _
                                              ",:RegDT " & vbCrLf & _
                                              ",:RegGrpCD " & vbCrLf & _
                                              ",:RegID " & vbCrLf & _
                                              ",:UpdateDT " & vbCrLf & _
                                              ",:UpGrpCD " & vbCrLf & _
                                              ",:UpdateID " & vbCrLf & _
                                              ") "

    'リリース対象システムログ（INSERT）SQL
    Private strInsertRelSystemLSql As String = "INSERT INTO RELEASE_SYSTEM_LTB ( " & vbCrLf & _
                                               "SELECT " & vbCrLf & _
                                               " RelNmb " & vbCrLf & _
                                               ",:LogNo" & vbCrLf & _
                                               ",RelSystemKbn " & vbCrLf & _
                                               ",SystemNmb " & vbCrLf & _
                                               ",EntryNmb " & vbCrLf & _
                                               ",RegDT " & vbCrLf & _
                                               ",RegGrpCD " & vbCrLf & _
                                               ",RegID " & vbCrLf & _
                                               ",UpdateDT " & vbCrLf & _
                                               ",UpGrpCD " & vbCrLf & _
                                               ",UpdateID " & vbCrLf & _
                                               "FROM RELEASE_SYSTEM_TB " & vbCrLf & _
                                               "WHERE RelNmb = :RelNmb " & vbCrLf & _
                                               ") "


    'リリース対応関係者（INSERT）SQL
    Private strInsertRelKankeiSql As String = "INSERT INTO release_kankei_tb ( " & vbCrLf & _
                                              " RelNmb " & vbCrLf & _
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
                                              " :RelNmb " & vbCrLf & _
                                              ",:RelationKbn " & vbCrLf & _
                                              ",:RelationID " & vbCrLf & _
                                              ",(SELECT COALESCE(MAX(EntryNmb),0)+1 FROM release_kankei_tb WHERE RelNmb=:RelNmb) " & vbCrLf & _
                                              ",:RegDT " & vbCrLf & _
                                              ",:RegGrpCD " & vbCrLf & _
                                              ",:RegID " & vbCrLf & _
                                              ",:UpdateDT " & vbCrLf & _
                                              ",:UpGrpCD " & vbCrLf & _
                                              ",:UpdateID " & vbCrLf & _
                                              ") "





    'プロセスリンク（insert）SQL
    Private strInsertProcessLinkSql As String = "INSERT INTO process_link_tb (" & vbCrLf & _
                                                " linkmotoprocesskbn " & vbCrLf & _
                                                ",LinkMotoNmb " & vbCrLf & _
                                                ",linksakiprocesskbn " & vbCrLf & _
                                                ",LinkSakiNmb " & vbCrLf & _
                                                ",EntryDT " & vbCrLf & _
                                                ",RegDT " & vbCrLf & _
                                                ",RegGrpCD " & vbCrLf & _
                                                ",RegID " & vbCrLf & _
                                                ",UpdateDT " & vbCrLf & _
                                                ",UpGrpCD " & vbCrLf & _
                                                ",UpdateID " & vbCrLf & _
                                                ") VALUES (" & vbCrLf & _
                                                " :linkmotoprocesskbn " & vbCrLf & _
                                                ",:LinkMotoNmb " & vbCrLf & _
                                                ",:linksakiprocesskbn " & vbCrLf & _
                                                ",:LinkSakiNmb " & vbCrLf & _
                                                ",:EntryDT " & vbCrLf & _
                                                ",:RegDT " & vbCrLf & _
                                                ",:RegGrpCD " & vbCrLf & _
                                                ",:RegID " & vbCrLf & _
                                                ",:UpdateDT " & vbCrLf & _
                                                ",:UpGrpCD " & vbCrLf & _
                                                ",:UpdateID " & vbCrLf & _
                                                ") "





    'システム日付取得（SELECT）SQL
    Private strSelectSysDateSql As String = "SELECT Now() AS SysDate "

    'リリース対象システム削除（DELETE）SQL
    Private strDeleteRelSystemSql As String = "DELETE FROM release_system_tb " & vbCrLf & _
                                              "WHERE RelNmb=           :RelNmb "

    'リリース対応関係者（DELETE）SQL
    Private strDeleteRelKankeiSql As String = "DELETE FROM release_kankei_tb " & vbCrLf & _
                                              "WHERE RelNmb=           :RelNmb "

    '[DELETE]会議結果情報ファイルSQL
    Private strDeleteMtgResultSql As String = "DELETE FROM meeting_result_tb " & vbCrLf & _
                                              "WHERE processnmb=           :processnmb "


    'プロセスリンク（DELETE）SQL
    Private strDeletePLinkSql As String = "DELETE FROM process_link_tb " & vbCrLf & _
                                          "WHERE LinkMotoNmb=       :LinkMotoNmb " & vbCrLf & _
                                          "AND LinkMotoProcesskbn=  :LinkMotoProcesskbn " & vbCrLf & _
                                          "AND LinkSakiNmb=         :LinkSakiNmb" & vbCrLf & _
                                          "AND LinkSakiProcesskbn=  :LinkSakiProcesskbn "



    '新規ログNo取得（SELECT）SQL
    Private strSelectNewRirekiNoSql As String = "SELECT " & vbCrLf & _
                                                "COALESCE(MAX(ct.logno),0)+1 AS LogNo " & vbCrLf & _
                                                "FROM RELEASE_INFO_LTB ct " & vbCrLf & _
                                                "WHERE ct.RelNmb=:RelNmb "
    '新規ログNo（会議用）取得（SELECT）SQL
    Private strSelectNewMeetingRirekiNoSql As String = "SELECT " & vbCrLf & _
                                                       " COALESCE(MAX(ML.LogNo),0)+1 AS LogNo " & vbCrLf & _
                                                       "FROM MEETING_LTB ML " & vbCrLf & _
                                                       "WHERE ML.MeetingNmb = :MeetingNmb "



    'リリース対応関係者ログ（insert）SQL
    Private strInsertRelKankeiLSql As String = "INSERT INTO RELEASE_KANKEI_LTB ( " & vbCrLf & _
                                               "SELECT " & vbCrLf & _
                                               " RelNmb " & vbCrLf & _
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
                                               "FROM RELEASE_KANKEI_TB " & vbCrLf & _
                                               "WHERE RelNmb = :RelNmb " & vbCrLf & _
                                               ") "



    'プロセスリンク(元)ログ（insert）SQL
    Private strInsertPLinkMotoLSql As String = "INSERT INTO release_process_link_ltb (" & vbCrLf & _
                                               "SELECT " & vbCrLf & _
                                               " :RelNmb " & vbCrLf & _
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
                                               "WHERE LinkMotoNmb  = :RelNmb " & vbCrLf & _
                                               "AND   LinkMotoProcesskbn = :pkbn " & vbCrLf & _
                                               ") "


    'リリース関連ファイル情報ログ（insert）SQL
    Private strInsertRelFileLSql As String = "INSERT INTO release_file_ltb (" & vbCrLf & _
                                             "SELECT " & vbCrLf & _
                                             " RelNmb " & vbCrLf & _
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
                                             "FROM release_file_tb " & vbCrLf & _
                                             "WHERE RelNmb = :RelNmb " & vbCrLf & _
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

    '対象システムにおける対応関係者存在チェック用（SELECT）SQL
    Private strCheckSysKankeiSql As String = "SELECT " & vbCrLf & _
                                             " kt.relationkbn " & vbCrLf & _
                                             ",kt.relationid " & vbCrLf & _
                                             "FROM kankei_tb kt " & vbCrLf & _
                                             "WHERE kt.CInmb = :CINmb  "

    '共通情報ロックテーブル登録（INSERT）用SQL
    Dim strInsertLockSql As String = "INSERT INTO RELEASE_INFO_LOCK_TB" & vbCrLf & _
                                     "(RelNmb,  EdiTime, EdiGrpCD, EdiID)" & vbCrLf & _
                                     "SELECT" & vbCrLf & _
                                     " rt.RelNmb,  Now(), :EdiGrpCD, :EdiID" & vbCrLf & _
                                     "FROM RELEASE_INFO_TB rt" & vbCrLf & _
                                     "WHERE" & vbCrLf & _
                                     " rt.Relnmb = :Nmb"

    '共通情報ロック解除（DELETE）用SQL
    Dim strDeleteLockSql As String = "DELETE FROM RELEASE_INFO_LOCK_TB WHERE RelNmb=:Nmb"


    '[SELECT]担当履歴取得SQL
    Private strSelectTantoRirekiSql As String = "SELECT " & vbCrLf & _
                                              " ct.tantorirekinmb " & vbCrLf & _
                                              ",ct.tantogrpcd " & vbCrLf & _
                                              ",ct.tantogrpnm " & vbCrLf & _
                                              ",ct.Reltantoid " & vbCrLf & _
                                              ",ct.Reltantonm " & vbCrLf & _
                                              "FROM RELEASE_tanto_rireki_tb ct " & vbCrLf & _
                                              "WHERE ct.Relnmb = :Relnmb " & vbCrLf & _
                                              "ORDER BY ct.tantorirekinmb DESC"

    '[INSERT]担当履歴SQL
    Private strInsertTantoRirekiSql As String = "INSERT INTO RELEASE_tanto_rireki_tb (" & vbCrLf & _
                                             " Relnmb " & vbCrLf & _
                                             ",tantorirekinmb " & vbCrLf & _
                                             ",tantogrpcd " & vbCrLf & _
                                             ",tantogrpnm " & vbCrLf & _
                                             ",Reltantoid " & vbCrLf & _
                                             ",Reltantonm " & vbCrLf & _
                                             ",RegDT " & vbCrLf & _
                                             ",RegGrpCD " & vbCrLf & _
                                             ",RegID " & vbCrLf & _
                                             ",UpdateDT " & vbCrLf & _
                                             ",UpGrpCD " & vbCrLf & _
                                             ",UpdateID " & vbCrLf & _
                                             ") VALUES (" & vbCrLf & _
                                             " :Relnmb " & vbCrLf & _
                                             ",(SELECT COALESCE(MAX(tantorirekinmb),0)+1 FROM RELEASE_tanto_rireki_tb WHERE Relnmb=:Relnmb) " & vbCrLf & _
                                             ",:tantogrpcd " & vbCrLf & _
                                             ",:tantogrpnm " & vbCrLf & _
                                             ",:Reltantoid " & vbCrLf & _
                                             ",:Reltantonm " & vbCrLf & _
                                             ",:RegDT " & vbCrLf & _
                                             ",:RegGrpCD " & vbCrLf & _
                                             ",:RegID " & vbCrLf & _
                                             ",:UpdateDT " & vbCrLf & _
                                             ",:UpGrpCD " & vbCrLf & _
                                             ",:UpdateID " & vbCrLf & _
                                             ") "




    ''' <summary>
    ''' 【共通】マスタデータ取得：プロセスステータス
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ステータスコンボボックス取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetCmbProcessStateMstData(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectProcessStateMastaSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("processkbn", NpgsqlTypes.NpgsqlDbType.Varchar))        'プロセス区分
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("processkbn").Value = "004"                      'プロセス区分
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
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>グループコンボボックス取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetTantoMastaData(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectTantoGroupMastaSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '[add] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 START
            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("RelNmb", NpgsqlTypes.NpgsqlDbType.Integer))         'リリース番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("RelNmb").Value = dataHBKF0201.PropIntRelNmb                          'リリース番号
            End With
            '[add] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 END


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
    ''' 【編集／参照モード】リリース共通情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>リリース共通情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectGetReleaseInfoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectReleaseInfoSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("RelNmb", NpgsqlTypes.NpgsqlDbType.Integer))             'リリース番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("RelNmb").Value = dataHBKF0201.PropIntRelNmb                          'リリース番号
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
    ''' 【新規/編集／参照モード】CI共通情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectGetCIInfoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectCIInfoSql

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
    ''' 【編集／参照モード】リリース対象システム情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>リリース対象システム取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectGetReleaseIraiSystemSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectReleaseSystemSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("RelNmb", NpgsqlTypes.NpgsqlDbType.Integer))             'リリース番号
                .Add(New NpgsqlParameter("RelSystemKbn", NpgsqlTypes.NpgsqlDbType.Varchar))       'リリースシステム区分
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("RelNmb").Value = dataHBKF0201.PropIntRelNmb                          'リリース番号
                .Parameters("RelSystemKbn").Value = RELSYSTEM_KBN_IRAI                            'リリースシステム区分
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
    ''' 【共通】対応関係者取得：リリース依頼受領/実施対象システム
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>リリース依頼受領/実施対象システムにおける対応関係者存在チェック用SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetChkKankeiSysData(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strCheckSysKankeiSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                           'CI番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CINmb").Value = dataHBKF0201.PropRowReg(0)                                 'CI番号
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
    ''' 【編集／参照モード】リリース対象システム情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>リリース対象システム情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectGetReleaseJissiSystemSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectReleaseSystemSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("RelNmb", NpgsqlTypes.NpgsqlDbType.Integer))            'リリース番号
                .Add(New NpgsqlParameter("RelSystemKbn", NpgsqlTypes.NpgsqlDbType.Varchar))      'リリースシステム区分
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("RelNmb").Value = dataHBKF0201.PropIntRelNmb                         'リリース番号
                .Parameters("RelSystemKbn").Value = RELSYSTEM_KBN_TAISYO                         'リリースシステム区分
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
    ''' 【編集／参照モード】リリ－ス関連ファイル情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>リリ－ス関連ファイル情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectGetReleaseFileSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectReleaseFileSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters

                .Add(New NpgsqlParameter("RelNmb", NpgsqlTypes.NpgsqlDbType.Integer))            'リリース番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand

                .Parameters("RelNmb").Value = dataHBKF0201.PropIntRelNmb                         'リリース番号
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
    ''' 【編集／参照モード】会議情報/会議結果情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議情報/会議結果情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectGetMeetingSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectMeetingSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))   'プロセス区分
                .Add(New NpgsqlParameter("ProcessNmb", NpgsqlTypes.NpgsqlDbType.Integer))   'プロセス番号

                .Add(New NpgsqlParameter("Kbn_NO", NpgsqlTypes.NpgsqlDbType.Varchar))       '結果区分：未入力
                .Add(New NpgsqlParameter("Kbn_NO_NM", NpgsqlTypes.NpgsqlDbType.Varchar))    '結果区分名略称：未入力
                .Add(New NpgsqlParameter("Kbn_OK", NpgsqlTypes.NpgsqlDbType.Varchar))       '結果区分：承認
                .Add(New NpgsqlParameter("Kbn_OK_NM", NpgsqlTypes.NpgsqlDbType.Varchar))    '結果区分名略称：承認
                .Add(New NpgsqlParameter("Kbn_NG", NpgsqlTypes.NpgsqlDbType.Varchar))       '結果区分：却下
                .Add(New NpgsqlParameter("Kbn_NG_NM", NpgsqlTypes.NpgsqlDbType.Varchar))    '結果区分名略称：却下
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("ProcessKbn").Value = PROCESS_TYPE_RELEASE                      'プロセス区分
                .Parameters("ProcessNmb").Value = dataHBKF0201.PropIntRelNmb                'リリース管理番号

                .Parameters("Kbn_NO").Value = SELECT_RESULTKBN_NO                           '結果区分：未入力
                .Parameters("Kbn_NO_NM").Value = SELECT_RESULTKBNNM_NO                      '結果区分名略称：未入力
                .Parameters("Kbn_OK").Value = SELECT_RESULTKBN_OK                           '結果区分：承認
                .Parameters("Kbn_OK_NM").Value = SELECT_RESULTKBNNM_OK                      '結果区分名略称：承認
                .Parameters("Kbn_NG").Value = SELECT_RESULTKBN_NG                           '結果区分：却下
                .Parameters("Kbn_NG_NM").Value = SELECT_RESULTKBNNM_NG                      '結果区分名略称：却下

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
    ''' 【編集／参照モード】リリース関係情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>リース関係情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectGetReleaseKankeiSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectReleaseKankeiSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("RelNmb", NpgsqlTypes.NpgsqlDbType.Integer))           'リリース番号
                .Add(New NpgsqlParameter("KbnGrp", NpgsqlTypes.NpgsqlDbType.Varchar))           '区分：グループ
                .Add(New NpgsqlParameter("KbnUsr", NpgsqlTypes.NpgsqlDbType.Varchar))           '区分：ユーザー
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("RelNmb").Value = dataHBKF0201.PropIntRelNmb                        'リリース番号
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
    ''' 【編集／参照モード】プロセスリンク情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>プロセスリンク情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectPLinkSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKF0201 As DataHBKF0201) As Boolean

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
                .Add(New NpgsqlParameter("Kbn_Incident", NpgsqlTypes.NpgsqlDbType.Varchar))      'プロセス区分：インシデント
                .Add(New NpgsqlParameter("Kbn_Incident_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))  'プロセス区分名略称：インシデント
                .Add(New NpgsqlParameter("Kbn_Question", NpgsqlTypes.NpgsqlDbType.Varchar))      'プロセス区分：問題
                .Add(New NpgsqlParameter("Kbn_Question_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))  'プロセス区分名略称：問題
                .Add(New NpgsqlParameter("Kbn_Change", NpgsqlTypes.NpgsqlDbType.Varchar))        'プロセス区分：変更
                .Add(New NpgsqlParameter("Kbn_Change_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))    'プロセス区分名略称：変更
                .Add(New NpgsqlParameter("Kbn_Release", NpgsqlTypes.NpgsqlDbType.Varchar))       'プロセス区分：リリース
                .Add(New NpgsqlParameter("Kbn_Release_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))   'プロセス区分名略称：リリース
                .Add(New NpgsqlParameter("RelNmb", NpgsqlTypes.NpgsqlDbType.Integer))            'リリース番号
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
                .Parameters("RelNmb").Value = dataHBKF0201.PropIntRelNmb                         'リリース番号
                .Parameters("LinkMotoProcesskbn").Value = PROCESS_TYPE_RELEASE                  'プロセス区分：リリース
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
    ''' 【編集／参照モード】リリース共通情報登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>リリース共通情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertRelInfoSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""
        Dim strBikoAimai As String = ""         'フリーテキスト（あいまい）


        Try

            'SQL文(SELECT)
            strSQL = strInsertRelInfoSql

            'データアダプタに、SQLのSELECT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("RelNmb", NpgsqlTypes.NpgsqlDbType.Integer))                       'リリース番号
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))                   'プロセス区分
                .Add(New NpgsqlParameter("ProcessStateCD", NpgsqlTypes.NpgsqlDbType.Varchar))               'プロセスステータスコード
                .Add(New NpgsqlParameter("RelUkeNmb", NpgsqlTypes.NpgsqlDbType.Varchar))                    'リリース受付番号
                .Add(New NpgsqlParameter("IraiDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                     '依頼日（起票日）
                .Add(New NpgsqlParameter("Title", NpgsqlTypes.NpgsqlDbType.Varchar))                        'タイトル
                .Add(New NpgsqlParameter("Gaiyo", NpgsqlTypes.NpgsqlDbType.Varchar))                        '概要
                .Add(New NpgsqlParameter("TujyoKinkyuKbn", NpgsqlTypes.NpgsqlDbType.Varchar))               '通常・緊急区分
                .Add(New NpgsqlParameter("UsrSyutiKbn", NpgsqlTypes.NpgsqlDbType.Varchar))                  'ユーザー周知必要有無区分
                .Add(New NpgsqlParameter("RelSceDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                   'リリース予定日時（目安）
                .Add(New NpgsqlParameter("RelStDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                    'リリース着手日時
                .Add(New NpgsqlParameter("RelEdDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                    'リリース終了日時
                .Add(New NpgsqlParameter("TantoGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                   '担当グループCD
                .Add(New NpgsqlParameter("RelTantoID", NpgsqlTypes.NpgsqlDbType.Varchar))                   'リリース担当者ID
                .Add(New NpgsqlParameter("RelTantoNM", NpgsqlTypes.NpgsqlDbType.Varchar))                   'リリース担当者氏名
                .Add(New NpgsqlParameter("GroupRireki", NpgsqlTypes.NpgsqlDbType.Varchar))                  'グループ履歴
                .Add(New NpgsqlParameter("TantoRireki", NpgsqlTypes.NpgsqlDbType.Varchar))                  '担当者履歴
                .Add(New NpgsqlParameter("BIko1", NpgsqlTypes.NpgsqlDbType.Varchar))                        'フリーテキスト１
                .Add(New NpgsqlParameter("Biko2", NpgsqlTypes.NpgsqlDbType.Varchar))                        'フリーテキスト２
                .Add(New NpgsqlParameter("Biko3", NpgsqlTypes.NpgsqlDbType.Varchar))                        'フリーテキスト３
                .Add(New NpgsqlParameter("Biko4", NpgsqlTypes.NpgsqlDbType.Varchar))                        'フリーテキスト４
                .Add(New NpgsqlParameter("Biko5", NpgsqlTypes.NpgsqlDbType.Varchar))                        'フリーテキスト５
                .Add(New NpgsqlParameter("FreeFlg1", NpgsqlTypes.NpgsqlDbType.Varchar))                     'フリーフラグ１
                .Add(New NpgsqlParameter("FreeFlg2", NpgsqlTypes.NpgsqlDbType.Varchar))                     'フリーフラグ２
                .Add(New NpgsqlParameter("FreeFlg3", NpgsqlTypes.NpgsqlDbType.Varchar))                     'フリーフラグ３
                .Add(New NpgsqlParameter("FreeFlg4", NpgsqlTypes.NpgsqlDbType.Varchar))                     'フリーフラグ４
                .Add(New NpgsqlParameter("FreeFlg5", NpgsqlTypes.NpgsqlDbType.Varchar))                     'フリーフラグ５
                .Add(New NpgsqlParameter("RelUkeNmbAimai", NpgsqlTypes.NpgsqlDbType.Varchar))               'リリース受付番号(あいまい)
                .Add(New NpgsqlParameter("TitleAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                   'タイトル(あいまい)
                .Add(New NpgsqlParameter("GaiyoAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                   '概要(あいまい)
                .Add(New NpgsqlParameter("BikoAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                    'フリーテキスト（あいまい）
                .Add(New NpgsqlParameter("RelTantIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))               'リリース担当者ID(あいまい)
                .Add(New NpgsqlParameter("RelTantNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))               'リリース担当者氏名(あいまい)
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                      '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                     '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                        '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                   '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                      '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                     '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("RelNmb").Value = dataHBKF0201.PropIntRelNmb                                    'リリース番号
                .Parameters("ProcessKbn").Value = PROCESS_TYPE_RELEASE                                      'プロセス区分
                .Parameters("ProcessStateCD").Value = dataHBKF0201.PropCmbProcessState.SelectedValue        'プロセスステータスCD
                .Parameters("RelUkeNmb").Value = dataHBKF0201.PropTxtRelUkeNmb.Text                         'リリース受付番号

                If dataHBKF0201.PropDtpIraiDT.txtDate.Text.Equals("") Then
                    .Parameters("IraiDT").Value = Nothing                                                   '依頼日（起票日）
                Else
                    .Parameters("IraiDT").Value = _
                        CDate(dataHBKF0201.PropDtpIraiDT.txtDate.Text)
                End If

                .Parameters("Title").Value = dataHBKF0201.PropTxtTitle.Text                                 'タイトル
                .Parameters("Gaiyo").Value = dataHBKF0201.PropTxtGaiyo.Text                                 '概要
                .Parameters("TujyoKinkyuKbn").Value = dataHBKF0201.PropCmbTujyoKinkyuKbn.SelectedValue      '通常・緊急区分
                .Parameters("UsrSyutiKbn").Value = dataHBKF0201.PropCmbUsrSyutiKbn.SelectedValue            'ユーザー周知必要有無区分
                If dataHBKF0201.PropDtpRelSceDT.txtDate.Text.Equals("") Then
                    .Parameters("RelSceDT").Value = Nothing                                                 'リリース予定日時（目安）
                Else
                    .Parameters("RelSceDT").Value = _
                        CDate(dataHBKF0201.PropDtpRelSceDT.txtDate.Text & " " & dataHBKF0201.PropTxtRelSceDT_HM.PropTxtTime.Text) 'リリース予定日時（目安）
                End If
                If dataHBKF0201.PropDtpRelStDT.txtDate.Text.Equals("") Then
                    .Parameters("RelStDT").Value = Nothing
                Else
                    .Parameters("RelStDT").Value = _
                        CDate(dataHBKF0201.PropDtpRelStDT.txtDate.Text & " " & dataHBKF0201.PropTxtRelStDT_HM.PropTxtTime.Text) 'リリース着手日時（目安）
                End If
                If dataHBKF0201.PropDtpRelEdDT.txtDate.Text.Equals("") Then
                    .Parameters("RelEdDT").Value = Nothing
                Else
                    .Parameters("RelEdDT").Value = _
                        CDate(dataHBKF0201.PropDtpRelEdDT.txtDate.Text & " " & dataHBKF0201.PropTxtRelEdDT_HM.PropTxtTime.Text) 'リリース終了日時（目安）
                End If
                .Parameters("TantoGrpCD").Value = dataHBKF0201.PropCmbTantoGrpCD.SelectedValue               '担当グループCD
                .Parameters("RelTantoID").Value = dataHBKF0201.PropTxtRelTantoID.Text                        'リリース担当者ID
                .Parameters("RelTantoNM").Value = dataHBKF0201.PropTxtRelTantoNM.Text                        'リリース担当者氏名
                .Parameters("GroupRireki").Value = dataHBKF0201.PropTxtGroupRireki.Text                      'グループ履歴
                .Parameters("TantoRireki").Value = dataHBKF0201.PropTxtTantoRireki.Text                      '担当者履歴
                .Parameters("BIko1").Value = dataHBKF0201.PropTxtBIko1.Text                                  'フリーテキスト１
                .Parameters("BIko2").Value = dataHBKF0201.PropTxtBIko2.Text                                  'フリーテキスト２
                .Parameters("BIko3").Value = dataHBKF0201.PropTxtBIko3.Text                                  'フリーテキスト３
                .Parameters("BIko4").Value = dataHBKF0201.PropTxtBIko4.Text                                  'フリーテキスト４
                .Parameters("BIko5").Value = dataHBKF0201.PropTxtBIko5.Text                                  'フリーテキスト５
                'フリーフラグ１～５
                If dataHBKF0201.PropChkFreeFlg1.Checked = True Then
                    .Parameters("FreeFlg1").Value = FREE_FLG_ON                                              'フリーフラグ１
                Else
                    .Parameters("FreeFlg1").Value = FREE_FLG_OFF                                             'フリーフラグ１
                End If
                If dataHBKF0201.PropChkFreeFlg2.Checked = True Then
                    .Parameters("FreeFlg2").Value = FREE_FLG_ON                                              'フリーフラグ２
                Else
                    .Parameters("FreeFlg2").Value = FREE_FLG_OFF                                             'フリーフラグ２
                End If
                If dataHBKF0201.PropChkFreeFlg3.Checked = True Then
                    .Parameters("FreeFlg3").Value = FREE_FLG_ON                                              'フリーフラグ３
                Else
                    .Parameters("FreeFlg3").Value = FREE_FLG_OFF                                             'フリーフラグ３
                End If
                If dataHBKF0201.PropChkFreeFlg4.Checked = True Then
                    .Parameters("FreeFlg4").Value = FREE_FLG_ON                                              'フリーフラグ４
                Else
                    .Parameters("FreeFlg4").Value = FREE_FLG_OFF                                             'フリーフラグ４
                End If
                If dataHBKF0201.PropChkFreeFlg5.Checked = True Then
                    .Parameters("FreeFlg5").Value = FREE_FLG_ON                                              'フリーフラグ５
                Else
                    .Parameters("FreeFlg5").Value = FREE_FLG_OFF                                             'フリーフラグ５
                End If

                'あいまい変換
                strBikoAimai = commonLogicHBK.ChangeStringForSearch(dataHBKF0201.PropTxtBIko1.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKF0201.PropTxtBIko2.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKF0201.PropTxtBIko3.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKF0201.PropTxtBIko4.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKF0201.PropTxtBIko5.Text)
                .Parameters("RelUkeNmbAimai").Value = _
                                    commonLogicHBK.ChangeStringForSearch(dataHBKF0201.PropTxtRelUkeNmb.Text) '受付番号あいまい
                .Parameters("TitleAimai").Value = _
                                    commonLogicHBK.ChangeStringForSearch(dataHBKF0201.PropTxtTitle.Text)     'タイトルあいまい
                .Parameters("GaiyoAimai").Value = _
                                    commonLogicHBK.ChangeStringForSearch(dataHBKF0201.PropTxtGaiyo.Text)
                .Parameters("BikoAimai").Value = strBikoAimai                                                '備考あいまい                
                .Parameters("RelTantIDAimai").Value = _
                                    commonLogicHBK.ChangeStringForSearch(dataHBKF0201.PropTxtRelTantoID.Text) '担当者IDあいまい
                .Parameters("RelTantNMAimai").Value = _
                                    commonLogicHBK.ChangeStringForSearch(dataHBKF0201.PropTxtRelTantoNM.Text) '担当者氏名あいまい

                .Parameters("RegDT").Value = dataHBKF0201.PropDtmSysDate                                    '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                             '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                     '登録者ID
                .Parameters("UpdateDT").Value = dataHBKF0201.PropDtmSysDate                                 '最終更新日時
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
    ''' 【編集／参照モード】リリース共通情報ログ登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>リリース共通情報ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertRelInfoLSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""
        Dim strBikoAimai As String = ""         'フリーテキスト（あいまい）


        Try

            'SQL文(SELECT)
            strSQL = strInsertRelInfoLSql

            'データアダプタに、SQLのSELECT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters

                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))                        'ログ番号
                .Add(New NpgsqlParameter("RelNmb", NpgsqlTypes.NpgsqlDbType.Integer))                       'リリース番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKF0201.PropIntLogNo                                      'ログ番号
                .Parameters("RelNmb").Value = dataHBKF0201.PropIntRelNmb                                    'リリース番号
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
    ''' 【新規登録モード】新規リリース番号、サーバー日付取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規リリース番号、サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewRelNmbAndSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                   ByVal Cn As NpgsqlConnection, _
                                                   ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = GET_NEXTVAL_RELEASE_NO

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
    ''' 【新規登録】リリース依頼受領システム新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>リリース対象システム情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertRelIraiSystemSql(ByRef Cmd As NpgsqlCommand, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertRelSystemSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("RelNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'リリース番号
                .Add(New NpgsqlParameter("RelSystemKbn", NpgsqlTypes.NpgsqlDbType.Varchar))             'リリースシステム区分
                .Add(New NpgsqlParameter("SystemNmb", NpgsqlTypes.NpgsqlDbType.Integer))                '対象システム番号
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                  '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                 '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                    '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("RelNmb").Value = dataHBKF0201.PropIntRelNmb                                'リリース番号
                .Parameters("RelSystemKbn").Value = RELSYSTEM_KBN_IRAI                                  'リリースシステム区分
                .Parameters("SystemNmb").Value = dataHBKF0201.PropRowReg.Item("CINmb")                  '対象システム番号
                '.Parameters("RegDT").Value = dataHBKF0201.PropDtmSysDate                                '登録日時
                '.Parameters("RegGrpCD").Value = PropWorkGroupCD                                         '登録者グループCD
                '.Parameters("RegID").Value = PropUserId                                                 '登録者ID
                '.Parameters("UpdateDT").Value = dataHBKF0201.PropDtmSysDate                             '最終更新日時
                '.Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                '.Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID

                If dataHBKF0201.PropRowReg.Item("RegDt").ToString = "" Then
                    .Parameters("RegDt").Value = dataHBKF0201.PropDtmSysDate                            '登録日時
                    .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
                    .Parameters("RegID").Value = PropUserId                                             '登録者ID
                Else
                    .Parameters("RegDt").Value = dataHBKF0201.PropRowReg.Item("RegDt")                  '登録日時
                    .Parameters("RegGrpCD").Value = dataHBKF0201.PropRowReg.Item("RegGrpCD")            '登録者グループCD
                    .Parameters("RegID").Value = dataHBKF0201.PropRowReg.Item("RegID")                  '登録者ID
                End If

                If dataHBKF0201.PropRowReg.Item("RegDt").ToString = "" Then
                    .Parameters("UpdateDT").Value = dataHBKF0201.PropDtmSysDate                         '最終更新日時
                    .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
                    .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
                Else
                    .Parameters("UpdateDT").Value = dataHBKF0201.PropRowReg.Item("RegDt")               '最終更新日時
                    .Parameters("UpGrpCD").Value = dataHBKF0201.PropRowReg.Item("RegGrpCD")             '最終更新者グループCD
                    .Parameters("UpdateID").Value = dataHBKF0201.PropRowReg.Item("RegID")               '最終更新者ID
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
    ''' 【新規登録】リリース実施対象システム新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>リリース実施対象システム情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertRelJisiSystemSql(ByRef Cmd As NpgsqlCommand, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertRelSystemSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("RelNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'リリース番号
                .Add(New NpgsqlParameter("RelSystemKbn", NpgsqlTypes.NpgsqlDbType.Varchar))             'リリースシステム区分
                .Add(New NpgsqlParameter("SystemNmb", NpgsqlTypes.NpgsqlDbType.Integer))                '対象システム番号
                .Add(New NpgsqlParameter("EntryNmb", NpgsqlTypes.NpgsqlDbType.Integer))                 '登録番号
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                  '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                 '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                    '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("RelNmb").Value = dataHBKF0201.PropIntRelNmb                                'リリース番号
                .Parameters("RelSystemKbn").Value = RELSYSTEM_KBN_TAISYO                                'リリースシステム区分
                .Parameters("SystemNmb").Value = dataHBKF0201.PropRowReg.Item("CINmb")                        '対象システム番号
                .Parameters("EntryNmb").Value = 1                                                       '登録番号
                .Parameters("RegDT").Value = dataHBKF0201.PropDtmSysDate                                '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                         '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                 '登録者ID
                .Parameters("UpdateDT").Value = dataHBKF0201.PropDtmSysDate                             '最終更新日時
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
    ''' 【新規登録】リリースシステムログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>リリース対象システム情報ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertRelSystemLSql(ByRef Cmd As NpgsqlCommand, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertRelSystemLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))                   'ログ番号
                .Add(New NpgsqlParameter("RelNmb", NpgsqlTypes.NpgsqlDbType.Integer))                  'リリース番号

            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKF0201.PropIntLogNo                                 'ログ番号
                .Parameters("RelNmb").Value = dataHBKF0201.PropIntRelNmb                               'リリース番号

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
    ''' 【新規登録モード】対応関係者情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>対応関係者情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertRelKankeiNewSql(ByRef Cmd As NpgsqlCommand, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertRelKankeiSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("RelNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'リリース番号
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
                .Parameters("RelNmb").Value = dataHBKF0201.PropIntRelNmb                                'リリース番号
                .Parameters("RelationKbn").Value = dataHBKF0201.PropRowReg.Item("RelationKbn")          '関係区分
                .Parameters("RelationID").Value = dataHBKF0201.PropRowReg.Item("RelationID")            '関係ID
                .Parameters("RegDT").Value = dataHBKF0201.PropDtmSysDate                                '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                         '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                 '登録者ID
                .Parameters("UpdateDT").Value = dataHBKF0201.PropDtmSysDate                             '最終更新日時
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
    ''' 【編集モード】対応関係者情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>対応関係者情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertRelKankeiSql(ByRef Cmd As NpgsqlCommand, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertRelKankeiSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters

                .Add(New NpgsqlParameter("RelNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'リリース番号
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

                .Parameters("RelNmb").Value = dataHBKF0201.PropIntRelNmb                                'リリース番号
                .Parameters("RelationKbn").Value = dataHBKF0201.PropRowReg.Item("RelationKbn")          '関係区分
                .Parameters("RelationID").Value = dataHBKF0201.PropRowReg.Item("RelationID")            '関係ID

                If dataHBKF0201.PropRowReg.Item("RegDt").ToString = "" Then
                    .Parameters("RegDt").Value = dataHBKF0201.PropDtmSysDate                            '登録日時
                    .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
                    .Parameters("RegID").Value = PropUserId                                             '登録者ID
                Else
                    .Parameters("RegDt").Value = dataHBKF0201.PropRowReg.Item("RegDt")                  '登録日時
                    .Parameters("RegGrpCD").Value = dataHBKF0201.PropRowReg.Item("RegGrpCD")            '登録者グループCD
                    .Parameters("RegID").Value = dataHBKF0201.PropRowReg.Item("RegID")                  '登録者ID
                End If

                If dataHBKF0201.PropRowReg.Item("RegDt").ToString = "" Then
                    .Parameters("UpdateDT").Value = dataHBKF0201.PropDtmSysDate                         '最終更新日時
                    .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
                    .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
                Else
                    .Parameters("UpdateDT").Value = dataHBKF0201.PropRowReg.Item("RegDt")               '最終更新日時
                    .Parameters("UpGrpCD").Value = dataHBKF0201.PropRowReg.Item("RegGrpCD")             '最終更新者グループCD
                    .Parameters("UpdateID").Value = dataHBKF0201.PropRowReg.Item("RegID")               '最終更新者ID
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
    ''' 【新規登録モード】リリースプロセスリンク(元)情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <param name="intAddMilliSec">[IN]ミリ秒数カウンタ</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>プロセスリンク情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InsertPLinkMoto(ByRef Cmd As NpgsqlCommand, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKF0201 As DataHBKF0201, _
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
                .Add(New NpgsqlParameter("LinkMotoNmb", NpgsqlTypes.NpgsqlDbType.Integer))              '元リリース番号
                .Add(New NpgsqlParameter("LinkSakiprocesskbn", NpgsqlTypes.NpgsqlDbType.Varchar))       '先P区分
                .Add(New NpgsqlParameter("LinkSakiNmb", NpgsqlTypes.NpgsqlDbType.Integer))              '先リリース番号
                .Add(New NpgsqlParameter("EntryDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                '登録順用
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                  '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                 '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                    '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("LinkMotoprocesskbn").Value = PROCESS_TYPE_RELEASE                          '元P区分
                .Parameters("LinkMotoNmb").Value = dataHBKF0201.PropIntRelNmb                           '元リリース番号
                .Parameters("LinkSakiprocesskbn").Value = dataHBKF0201.PropRowReg.Item("processkbn")    '参照先P区分
                .Parameters("LinkSakiNmb").Value = dataHBKF0201.PropRowReg.Item("MngNmb")               '参照先リリース番号
                .Parameters("EntryDT").Value = dataHBKF0201.PropDtmSysDate.AddMilliseconds(intAddMilliSec)      'カウンタ
                .Parameters("RegDT").Value = dataHBKF0201.PropDtmSysDate                                '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                         '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                 '登録者ID
                .Parameters("UpdateDT").Value = dataHBKF0201.PropDtmSysDate                             '更新日時
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
    ''' 【編集モード】リリース共通情報更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>リリース共通情報更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateRelInfoSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Dim strBikoAimai As String = ""         'フリーテキスト（あいまい）

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateRelInfoSql

            'データアダプタに、SQLのUPDATE文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("RelNmb", NpgsqlTypes.NpgsqlDbType.Integer))                       'リリース番号
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))                   'プロセス区分
                .Add(New NpgsqlParameter("ProcessStateCD", NpgsqlTypes.NpgsqlDbType.Varchar))               'プロセスステータスコード
                .Add(New NpgsqlParameter("RelUkeNmb", NpgsqlTypes.NpgsqlDbType.Varchar))                    'リリース受付番号
                .Add(New NpgsqlParameter("IraiDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                       '依頼日（起票日）
                .Add(New NpgsqlParameter("Title", NpgsqlTypes.NpgsqlDbType.Varchar))                        'タイトル
                .Add(New NpgsqlParameter("Gaiyo", NpgsqlTypes.NpgsqlDbType.Varchar))                        '概要
                .Add(New NpgsqlParameter("TujyoKinkyuKbn", NpgsqlTypes.NpgsqlDbType.Varchar))               '通常・緊急区分
                .Add(New NpgsqlParameter("UsrSyutiKbn", NpgsqlTypes.NpgsqlDbType.Varchar))                  'ユーザー周知必要有無区分
                .Add(New NpgsqlParameter("RelSceDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                     'リリース予定日時（目安）
                .Add(New NpgsqlParameter("RelStDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                      'リリース着手日時
                .Add(New NpgsqlParameter("RelEdDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                      'リリース終了日時
                .Add(New NpgsqlParameter("TantoGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                   '担当グループCD
                .Add(New NpgsqlParameter("RelTantoID", NpgsqlTypes.NpgsqlDbType.Varchar))                   'リリース担当者ID
                .Add(New NpgsqlParameter("RelTantoNM", NpgsqlTypes.NpgsqlDbType.Varchar))                   'リリース担当者氏名
                .Add(New NpgsqlParameter("GroupRireki", NpgsqlTypes.NpgsqlDbType.Varchar))                  'グループ履歴
                .Add(New NpgsqlParameter("TantoRireki", NpgsqlTypes.NpgsqlDbType.Varchar))                  '担当者履歴
                .Add(New NpgsqlParameter("BIko1", NpgsqlTypes.NpgsqlDbType.Varchar))                        'フリーテキスト１
                .Add(New NpgsqlParameter("Biko2", NpgsqlTypes.NpgsqlDbType.Varchar))                        'フリーテキスト２
                .Add(New NpgsqlParameter("Biko3", NpgsqlTypes.NpgsqlDbType.Varchar))                        'フリーテキスト３
                .Add(New NpgsqlParameter("Biko4", NpgsqlTypes.NpgsqlDbType.Varchar))                        'フリーテキスト４
                .Add(New NpgsqlParameter("Biko5", NpgsqlTypes.NpgsqlDbType.Varchar))                        'フリーテキスト５
                .Add(New NpgsqlParameter("FreeFlg1", NpgsqlTypes.NpgsqlDbType.Varchar))                     'フリーフラグ１
                .Add(New NpgsqlParameter("FreeFlg2", NpgsqlTypes.NpgsqlDbType.Varchar))                     'フリーフラグ２
                .Add(New NpgsqlParameter("FreeFlg3", NpgsqlTypes.NpgsqlDbType.Varchar))                     'フリーフラグ３
                .Add(New NpgsqlParameter("FreeFlg4", NpgsqlTypes.NpgsqlDbType.Varchar))                     'フリーフラグ４
                .Add(New NpgsqlParameter("FreeFlg5", NpgsqlTypes.NpgsqlDbType.Varchar))                     'フリーフラグ５
                .Add(New NpgsqlParameter("RelUkeNmbAimai", NpgsqlTypes.NpgsqlDbType.Varchar))               'リリース受付番号(あいまい)
                .Add(New NpgsqlParameter("TitleAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                   'タイトル(あいまい)
                .Add(New NpgsqlParameter("GaiyoAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                   '概要(あいまい)
                .Add(New NpgsqlParameter("BikoAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                    'フリーテキスト（あいまい）
                .Add(New NpgsqlParameter("RelTantIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))               'リリース担当者ID(あいまい)
                .Add(New NpgsqlParameter("RelTantNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))               'リリース担当者氏名(あいまい)

                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                   '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                      '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                     '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("RelNmb").Value = dataHBKF0201.PropIntRelNmb                                    'リリース番号
                .Parameters("ProcessKbn").Value = PROCESS_TYPE_RELEASE                                      'プロセス区分
                .Parameters("ProcessStateCD").Value = dataHBKF0201.PropCmbProcessState.SelectedValue        'プロセスステータスCD
                .Parameters("RelUkeNmb").Value = dataHBKF0201.PropTxtRelUkeNmb.Text                         'リリース受付番号

                If dataHBKF0201.PropDtpIraiDT.txtDate.Text.Equals("") Then
                    .Parameters("IraiDT").Value = Nothing                                                   '依頼日（起票日）
                Else
                    .Parameters("IraiDT").Value = _
                        CDate(dataHBKF0201.PropDtpIraiDT.txtDate.Text)
                End If

                .Parameters("Title").Value = dataHBKF0201.PropTxtTitle.Text                                 'タイトル
                .Parameters("Gaiyo").Value = dataHBKF0201.PropTxtGaiyo.Text                                 '概要
                .Parameters("TujyoKinkyuKbn").Value = dataHBKF0201.PropCmbTujyoKinkyuKbn.SelectedValue      '通常・緊急区分
                .Parameters("UsrSyutiKbn").Value = dataHBKF0201.PropCmbUsrSyutiKbn.SelectedValue            'ユーザー周知必要有無区分
                If dataHBKF0201.PropDtpRelSceDT.txtDate.Text.Equals("") Then
                    .Parameters("RelSceDT").Value = Nothing                                                 'リリース予定日時（目安）
                Else
                    .Parameters("RelSceDT").Value = _
                        CDate(dataHBKF0201.PropDtpRelSceDT.txtDate.Text & " " & dataHBKF0201.PropTxtRelSceDT_HM.PropTxtTime.Text) 'リリース予定日時（目安）
                End If
                If dataHBKF0201.PropDtpRelStDT.txtDate.Text.Equals("") Then
                    .Parameters("RelStDT").Value = Nothing
                Else
                    .Parameters("RelStDT").Value = _
                        CDate(dataHBKF0201.PropDtpRelStDT.txtDate.Text & " " & dataHBKF0201.PropTxtRelStDT_HM.PropTxtTime.Text) 'リリース着手日時（目安）
                End If
                If dataHBKF0201.PropDtpRelEdDT.txtDate.Text.Equals("") Then
                    .Parameters("RelEdDT").Value = Nothing
                Else
                    .Parameters("RelEdDT").Value = _
                        CDate(dataHBKF0201.PropDtpRelEdDT.txtDate.Text & " " & dataHBKF0201.PropTxtRelEdDT_HM.PropTxtTime.Text) 'リリース終了日時（目安）
                End If
                .Parameters("TantoGrpCD").Value = dataHBKF0201.PropCmbTantoGrpCD.SelectedValue               '担当グループCD
                .Parameters("RelTantoID").Value = dataHBKF0201.PropTxtRelTantoID.Text                        'リリース担当者ID
                .Parameters("RelTantoNM").Value = dataHBKF0201.PropTxtRelTantoNM.Text                        'リリース担当者氏名
                .Parameters("GroupRireki").Value = dataHBKF0201.PropTxtGroupRireki.Text                      'グループ履歴
                .Parameters("TantoRireki").Value = dataHBKF0201.PropTxtTantoRireki.Text                      '担当者履歴
                .Parameters("BIko1").Value = dataHBKF0201.PropTxtBIko1.Text                                  'フリーテキスト１
                .Parameters("BIko2").Value = dataHBKF0201.PropTxtBIko2.Text                                  'フリーテキスト２
                .Parameters("BIko3").Value = dataHBKF0201.PropTxtBIko3.Text                                  'フリーテキスト３
                .Parameters("BIko4").Value = dataHBKF0201.PropTxtBIko4.Text                                  'フリーテキスト４
                .Parameters("BIko5").Value = dataHBKF0201.PropTxtBIko5.Text                                  'フリーテキスト５
                'フリーフラグ１～５
                If dataHBKF0201.PropChkFreeFlg1.Checked = True Then
                    .Parameters("FreeFlg1").Value = FREE_FLG_ON                                              'フリーフラグ１
                Else
                    .Parameters("FreeFlg1").Value = FREE_FLG_OFF                                             'フリーフラグ１
                End If
                If dataHBKF0201.PropChkFreeFlg2.Checked = True Then
                    .Parameters("FreeFlg2").Value = FREE_FLG_ON                                              'フリーフラグ２
                Else
                    .Parameters("FreeFlg2").Value = FREE_FLG_OFF                                             'フリーフラグ２
                End If
                If dataHBKF0201.PropChkFreeFlg3.Checked = True Then
                    .Parameters("FreeFlg3").Value = FREE_FLG_ON                                              'フリーフラグ３
                Else
                    .Parameters("FreeFlg3").Value = FREE_FLG_OFF                                             'フリーフラグ３
                End If
                If dataHBKF0201.PropChkFreeFlg4.Checked = True Then
                    .Parameters("FreeFlg4").Value = FREE_FLG_ON                                              'フリーフラグ４
                Else
                    .Parameters("FreeFlg4").Value = FREE_FLG_OFF                                             'フリーフラグ４
                End If
                If dataHBKF0201.PropChkFreeFlg5.Checked = True Then
                    .Parameters("FreeFlg5").Value = FREE_FLG_ON                                              'フリーフラグ５
                Else
                    .Parameters("FreeFlg5").Value = FREE_FLG_OFF                                             'フリーフラグ５
                End If

                'あいまい変換
                strBikoAimai = commonLogicHBK.ChangeStringForSearch(dataHBKF0201.PropTxtBIko1.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKF0201.PropTxtBIko2.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKF0201.PropTxtBIko3.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKF0201.PropTxtBIko4.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKF0201.PropTxtBIko5.Text)
                .Parameters("RelUkeNmbAimai").Value = _
                                commonLogicHBK.ChangeStringForSearch(dataHBKF0201.PropTxtRelUkeNmb.Text)    '受付番号あいまい
                .Parameters("TitleAimai").Value = _
                                commonLogicHBK.ChangeStringForSearch(dataHBKF0201.PropTxtTitle.Text)        'タイトルあいまい
                .Parameters("GaiyoAimai").Value = _
                                commonLogicHBK.ChangeStringForSearch(dataHBKF0201.PropTxtGaiyo.Text)
                .Parameters("BikoAimai").Value = strBikoAimai                                               '備考あいまい                
                .Parameters("RelTantIDAimai").Value = _
                                commonLogicHBK.ChangeStringForSearch(dataHBKF0201.PropTxtRelTantoID.Text)   '担当者IDあいまい
                .Parameters("RelTantNMAimai").Value = _
                                commonLogicHBK.ChangeStringForSearch(dataHBKF0201.PropTxtRelTantoNM.Text)   '担当者氏名あいまい

                .Parameters("UpdateDT").Value = dataHBKF0201.PropDtmSysDate                                 '最終更新日時
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
    ''' 【編集モード】リリースプロセスリンク(元)情報更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>リリースプロセスリンク情報更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function DeletePLinkMoto(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKF0201 As DataHBKF0201) As Boolean

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
                .Add(New NpgsqlParameter("LinkMotoNmb", NpgsqlTypes.NpgsqlDbType.Integer))               'リリース番号
                .Add(New NpgsqlParameter("LinkMotoProcesskbn", NpgsqlTypes.NpgsqlDbType.Varchar))        '区分
                .Add(New NpgsqlParameter("LinkSakiNmb", NpgsqlTypes.NpgsqlDbType.Integer))               'リリース番号
                .Add(New NpgsqlParameter("LinkSakiProcesskbn", NpgsqlTypes.NpgsqlDbType.Varchar))        '区分
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("LinkMotoNmb").Value = dataHBKF0201.PropIntRelNmb                            'リリース番号
                .Parameters("LinkMotoProcesskbn").Value = PROCESS_TYPE_RELEASE
                .Parameters("LinkSakiNmb").Value = dataHBKF0201.PropRowReg("MngNmb", DataRowVersion.Original)
                .Parameters("LinkSakiProcesskbn").Value = dataHBKF0201.PropRowReg("processkbn", DataRowVersion.Original)
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
    ''' 【編集モード】リリースプロセスリンク(先)情報更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>リリースプロセスリンク(先)情報更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function DeletePLinkSaki(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKF0201 As DataHBKF0201) As Boolean

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
                .Add(New NpgsqlParameter("LinkMotoNmb", NpgsqlTypes.NpgsqlDbType.Integer))               'リリース番号
                .Add(New NpgsqlParameter("LinkMotoProcesskbn", NpgsqlTypes.NpgsqlDbType.Varchar))        '区分
                .Add(New NpgsqlParameter("LinkSakiNmb", NpgsqlTypes.NpgsqlDbType.Integer))               'リリース番号
                .Add(New NpgsqlParameter("LinkSakiProcesskbn", NpgsqlTypes.NpgsqlDbType.Varchar))        '区分
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("LinkMotoNmb").Value = dataHBKF0201.PropRowReg("MngNmb", DataRowVersion.Original)
                .Parameters("LinkMotoProcesskbn").Value = dataHBKF0201.PropRowReg("processkbn", DataRowVersion.Original)
                .Parameters("LinkSakiNmb").Value = dataHBKF0201.PropIntRelNmb
                .Parameters("LinkSakiProcesskbn").Value = PROCESS_TYPE_RELEASE
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
    ''' <param name="dataHBKF0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議結果情報更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/30 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertMtgResultSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKF0201 As DataHBKF0201) As Boolean

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
                .Parameters("MeetingNmb").Value = DataHBKF0201.PropRowReg.Item("MeetingNmb")    '会議番号
                .Parameters("ProcessKbn").Value = PROCESS_TYPE_RELEASE                           'プロセス区分：変更
                .Parameters("ProcessNmb").Value = dataHBKF0201.PropIntRelNmb
                'プロセス番号
                '結果区分がブランクの場合は0を設定
                If DataHBKF0201.PropRowReg.Item("ResultKbn").ToString.Equals("") Then
                    .Parameters("ResultKbn").Value = SELECT_RESULTKBN_NO                        'ブランク
                Else
                    .Parameters("ResultKbn").Value = DataHBKF0201.PropRowReg.Item("ResultKbn")
                End If
                .Parameters("RegDT").Value = DataHBKF0201.PropDtmSysDate                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                         '登録者ID
                .Parameters("UpdateDT").Value = DataHBKF0201.PropDtmSysDate                     '最終更新日時
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
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKF0201 As DataHBKF0201) As Boolean

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
    ''' 【編集モード】リリース対象システム情報削除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>リリース対象システム情報削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteRelSystemSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Try

            'SQL文(Delete)
            strSQL = strDeleteRelSystemSql

            'データアダプタに、SQLのDelete文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            With Cmd.Parameters
                .Add(New NpgsqlParameter("RelNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'リリース番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("RelNmb").Value = dataHBKF0201.PropIntRelNmb                                'リリース番号
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
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>対応関係者情報更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteRelkankeiSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Try

            'SQL文(Delete)
            strSQL = strDeleteRelKankeiSql

            'データアダプタに、SQLのDelete文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            With Cmd.Parameters
                .Add(New NpgsqlParameter("RelNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'リリース番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("RelNmb").Value = dataHBKF0201.PropIntRelNmb                                'リリース番号
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
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規ログNo取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewRirekiNoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKF0201 As DataHBKF0201) As Boolean

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
                .Add(New NpgsqlParameter("RelNmb", NpgsqlTypes.NpgsqlDbType.Integer))   'リリース番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("RelNmb").Value = dataHBKF0201.PropIntRelNmb                'リリース番号
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
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規ログNo（会議用）取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewMeetingRirekiNoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectNewMeetingRirekiNoSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("MeetingNmb", NpgsqlTypes.NpgsqlDbType.Integer))   '会議番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("MeetingNmb").Value = dataHBKF0201.PropIntMeetingNmb            '会議番号
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
    ''' 【共通】リリース対応関係情報ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>リリース対応関係情報ログ新規登録用のdataHBKF0201SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertRelKankeiLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertRelKankeiLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
                .Add(New NpgsqlParameter("RelNmb", NpgsqlTypes.NpgsqlDbType.Integer))       'リリース番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKF0201.PropIntLogNo                      'ログNo
                .Parameters("RelNmb").Value = dataHBKF0201.PropIntRelNmb                    'リリース番号
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
    ''' 【共通】リリースプロセスリンク情報ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>リリースプロセスリンク情報ログ新規登録用のdataHBKF0201SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertPLinkmotoLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKF0201 As DataHBKF0201) As Boolean

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
                .Add(New NpgsqlParameter("RelNmb", NpgsqlTypes.NpgsqlDbType.Integer))       'リリース番号
                .Add(New NpgsqlParameter("pkbn", NpgsqlTypes.NpgsqlDbType.Varchar))         'プロセス区分
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKF0201.PropIntLogNo                      'ログNo
                .Parameters("RelNmb").Value = dataHBKF0201.PropIntRelNmb                    'リリース番号
                .Parameters("pkbn").Value = PROCESS_TYPE_RELEASE                            'プロセス区分
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
    ''' 【共通】リリース関連ファイル情報ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>リリース関連ファイル情報ログ新規登録用のdataHBKF0201SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertRelFileLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertRelFileLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
                .Add(New NpgsqlParameter("RelNmb", NpgsqlTypes.NpgsqlDbType.Integer))       'リリース番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKF0201.PropIntLogNo                      'ログNo
                .Parameters("RelNmb").Value = dataHBKF0201.PropIntRelNmb                    'リリース番号
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
    ''' 【共通】新規ログNo（会議用）取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規ログNo（会議用）取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewMeetingLogNoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKF0201 As DataHBKF0201) As Boolean

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
                .Parameters("MeetingNmb").Value = dataHBKF0201.PropIntMeetingNmb            '会議番号
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
    ''' 【共通】対応関係者取得：所属グループ
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <param name="StrKbn">[IN]プロセス区分</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>関係者テーブル情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/04 s.tsuruta
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
    ''' <para>作成情報：2012/09/04 s.tsuruta
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
    ''' <para>作成情報：2012/09/04 s.tsuruta
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
    ''' 共通情報ロックテーブル、サーバー日付取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>指定された管理番号の共通情報ロックテーブルおよびサーバー日付を返す
    ''' <para>作成情報：2012/08/28 s.tsuruta
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
    ''' 【共通】マスタデータ取得：ユーザー
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>指定したひびきユーザー取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetTantoInfoData(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKF0201 As DataHBKF0201) As Boolean

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
                .Add(New NpgsqlParameter("hbkusrid", NpgsqlTypes.NpgsqlDbType.Varchar))     'ひびきユーザーID
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("hbkusrid").Value = dataHBKF0201.PropTxtRelTantoID.Text         'ひびきユーザーID
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
    ''' 共通情報ロックテーブル登録処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>共通情報ロックテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/28 s.tsuruta
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
    ''' 共通情報ロックテーブル削除処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>番号をキーに共通情報ロックテーブルのデータを物理削除（DELETE）する
    ''' <para>作成情報：2012/08/28 s.tsuruta
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
    ''' 【共通】会議結果情報ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議情報ログ新規登録用のdataHBKF0201SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertMtgResultLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKF0201 As DataHBKF0201) As Boolean

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
                .Parameters("LogNo").Value = dataHBKF0201.PropIntLogNoSub                       'ログNo
                .Parameters("meetingnmb").Value = dataHBKF0201.PropIntMeetingNmb                '会議No
                .Parameters("processnmb").Value = dataHBKF0201.PropIntRelNmb                    'プロセス番号
                .Parameters("processkbn").Value = PROCESS_TYPE_RELEASE                          'プロセス区分
                .Parameters("ProcessLogNo").Value = dataHBKF0201.PropIntLogNo                   'プロセスログ番号
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
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議情報ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertMeetingLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKF0201 As DataHBKF0201) As Boolean

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
                .Parameters("LogNo").Value = dataHBKF0201.PropIntLogNoSub                   'ログNo
                .Parameters("meetingnmb").Value = dataHBKF0201.PropIntMeetingNmb            '会議No
                .Parameters("processnmb").Value = dataHBKF0201.PropIntRelNmb                'プロセス番号
                .Parameters("processkbn").Value = PROCESS_TYPE_RELEASE                      'プロセス区分
                .Parameters("ProcessLogNo").Value = dataHBKF0201.PropIntLogNo               'プロセスログ番号
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
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議出席者情報ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertMtgAttendLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKF0201 As DataHBKF0201) As Boolean

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
                .Parameters("LogNo").Value = dataHBKF0201.PropIntLogNoSub                   'ログNo
                .Parameters("MeetingNmb").Value = dataHBKF0201.PropIntMeetingNmb            '会議番号
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
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議関連ファイル情報ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertMtgFileLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKF0201 As DataHBKF0201) As Boolean

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
                .Parameters("LogNo").Value = dataHBKF0201.PropIntLogNoSub                   'ログNo
                .Parameters("MeetingNmb").Value = dataHBKF0201.PropIntMeetingNmb            '会議番号
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
    ''' <param name="dataHBKF0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議結果情報更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteMtgResultSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKF0201 As DataHBKF0201) As Boolean

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
                .Parameters("processnmb").Value = DataHBKF0201.PropIntRelNmb
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
    ''' 【新規登録／編集モード】担当履歴情報　新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>担当履歴情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/10 r.hoshino
    ''' <p>改訂情報</p>
    ''' </para></remarks>
    Public Function SetInsertTantoRirekiSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKF0201 As DataHBKF0201) As Boolean

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
                .Add(New NpgsqlParameter("RelNmb", NpgsqlTypes.NpgsqlDbType.Integer))           'inc番号
                .Add(New NpgsqlParameter("tantogrpcd", NpgsqlTypes.NpgsqlDbType.Varchar))       'グループcd
                .Add(New NpgsqlParameter("tantogrpnm", NpgsqlTypes.NpgsqlDbType.Varchar))       'グループ名
                .Add(New NpgsqlParameter("reltantoid", NpgsqlTypes.NpgsqlDbType.Varchar))       '担当ＩＤ
                .Add(New NpgsqlParameter("reltantonm", NpgsqlTypes.NpgsqlDbType.Varchar))       '担当名

                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))        '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))           '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("RelNmb").Value = dataHBKF0201.PropIntRelNmb
                .Parameters("tantogrpcd").Value = dataHBKF0201.PropCmbTantoGrpCD.SelectedValue
                .Parameters("tantogrpnm").Value = dataHBKF0201.PropCmbTantoGrpCD.Text
                .Parameters("reltantoid").Value = dataHBKF0201.PropTxtRelTantoID.Text
                .Parameters("reltantonm").Value = dataHBKF0201.PropTxtRelTantoNM.Text

                .Parameters("RegDT").Value = dataHBKF0201.PropDtmSysDate                         '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                  '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                          '登録者ID
                .Parameters("UpdateDT").Value = dataHBKF0201.PropDtmSysDate                      '最終更新日時
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
    ''' <param name="dataHBKF0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>担当履歴情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/10 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectTantoRirekiSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKF0201 As DataHBKF0201) As Boolean

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
                .Add(New NpgsqlParameter("RelNmb", NpgsqlTypes.NpgsqlDbType.Integer))        '管理番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("RelNmb").Value = dataHBKF0201.PropIntRelNmb                      '管理番号
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
    ''' <param name="dataHBKF0201">[IN]リリース登録画面データクラス</param>
    ''' <param name="kbn">[IN]システム区分</param>
    ''' <param name="EntryNmb">[IN]登録順</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>INC共通情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/10/11 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetChkSysNmbData(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKF0201 As DataHBKF0201, ByVal kbn As String, EntryNmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strCheckRelSystemNmbSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("Nmb", NpgsqlTypes.NpgsqlDbType.Integer))                            '管理番号
                .Add(New NpgsqlParameter("SystemKbn", NpgsqlTypes.NpgsqlDbType.Varchar))                      'システム区分
                .Add(New NpgsqlParameter("EntryNmb", NpgsqlTypes.NpgsqlDbType.Integer))                       '登録順
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("Nmb").Value = dataHBKF0201.PropIntRelNmb
                .Parameters("SystemKbn").Value = kbn
                .Parameters("EntryNmb").Value = EntryNmb
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
