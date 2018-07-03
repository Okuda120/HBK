Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' 問題登録画面Sqlクラス
''' </summary>
''' <remarks>問題登録画面のSQLの作成・設定を行う
''' <para>作成情報：2012/08/13 s.yamaguchi
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKD0201

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

    '[SELECT]対応関係者対象システム変更チェック用SQL[インシデント]
    Private strCheckPrbSystemNmbSql As String = "SELECT " & vbCrLf & _
                                                " systemnmb " & vbCrLf & _
                                                "FROM problem_info_tb ct " & vbCrLf & _
                                                "WHERE ct.Prbnmb= :Nmb  "


    '[SELECT]プロセスステータスマスタ取得SQL
    Private strSelectProcessStateMstSql As String = "SELECT" & vbCrLf & _
                                                    " ProcessStateCD," & vbCrLf & _
                                                    " ProcessStateNM" & vbCrLf & _
                                                    " FROM processstate_mtb" & vbCrLf & _
                                                    " WHERE COALESCE(JtiFlg,'0') <> '1' " & vbCrLf & _
                                                    " AND ProcessKbn = :ProcessKbn " & vbCrLf & _
                                                    " ORDER BY Sort"

    '[SELECT]問題発生原因マスタ取得SQL
    '[mod] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 START
    'Private strSelectProblemCaseMstSql As String = "SELECT" & vbCrLf & _
    '                                               " PrbCaseCD," & vbCrLf & _
    '                                               " PrbCaseNM" & vbCrLf & _
    '                                               " FROM problem_case_mtb" & vbCrLf & _
    '                                               " WHERE COALESCE(JtiFlg,'0') <> '1' " & vbCrLf & _
    '                                               " ORDER BY Sort"
    Private strSelectProblemCaseMstSql As String = "SELECT" & vbCrLf & _
                                                   " PrbCaseCD," & vbCrLf & _
                                                   " PrbCaseNM" & vbCrLf & _
                                                   " FROM problem_case_mtb" & vbCrLf & _
                                                   " WHERE COALESCE(JtiFlg,'0') <> '1' OR PrbCaseCD IN (SELECT PrbCaseCD FROM problem_info_tb WHERE PrbNmb = :PrbNmb) " & vbCrLf & _
                                                   " ORDER BY JtiFlg,Sort"
    '[mod] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 END

    '[SELECT]グループマスタ取得SQL
    '[mod] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 START
    'Private strSelectTantoGrpMstSql As String = "SELECT" & vbCrLf & _
    '                                            " GroupCD," & vbCrLf & _
    '                                            " GroupNM" & vbCrLf & _
    '                                            " FROM grp_mtb" & vbCrLf & _
    '                                            " WHERE COALESCE(JtiFlg,'0') <> '1'" & vbCrLf & _
    '                                            " ORDER BY Sort"
    Private strSelectTantoGrpMstSql As String = "SELECT" & vbCrLf & _
                                                " GroupCD," & vbCrLf & _
                                                " GroupNM" & vbCrLf & _
                                                " FROM grp_mtb" & vbCrLf & _
                                                " WHERE COALESCE(JtiFlg,'0') <> '1' OR GroupCD IN (SELECT TantoGrpCD FROM problem_info_tb WHERE PrbNmb = :PrbNmb) " & vbCrLf & _
                                                " ORDER BY JtiFlg,Sort"
    '[mod] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 END

    '[SELECT]作業ステータスマスタ取得SQL
    '[mod] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 START
    'Private strSelectWorkStateMstSql As String = "SELECT" & vbCrLf & _
    '                                             " workstatecd," & vbCrLf & _
    '                                             " WorkStateNM" & vbCrLf & _
    '                                             " FROM workstate_mtb" & vbCrLf & _
    '                                             " WHERE COALESCE(JtiFlg,'0') <> '1'" & vbCrLf & _
    '                                             " ORDER BY Sort"
    Private strSelectWorkStateMstSql As String = "SELECT" & vbCrLf & _
                                                 " workstatecd," & vbCrLf & _
                                                 " WorkStateNM" & vbCrLf & _
                                                 " FROM workstate_mtb" & vbCrLf & _
                                                 " WHERE COALESCE(JtiFlg,'0') <> '1' OR workstatecd IN (SELECT WorkStateCD FROM problem_wk_rireki_tb WHERE PrbNmb = :PrbNmb) " & vbCrLf & _
                                                 " ORDER BY JtiFlg,Sort"
    '[mod] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 END

    '[SELECT]対象システム取得SQL
    Private strSelectTargetSystemSql As String = "SELECT" & vbCrLf & _
                                                 " CINmb," & vbCrLf & _
                                                 " CINM AS CINM1," & vbCrLf & _
                                                 " CINM AS CINM2," & vbCrLf & _
                                                 " Class1," & vbCrLf & _
                                                 " Class2" & vbCrLf & _
                                                 " FROM (" & vbCrLf & _
                                                 "          SELECT CINmb, KindCD, Class1, Class2, CINM, '1' AS Sort0, Sort" & vbCrLf & _
                                                 "          FROM ci_info_tb" & vbCrLf & _
                                                 "          WHERE CIStatusCD <> :CIStatusCD" & vbCrLf & _
                                                 "          AND CIKbnCD = :CIKbnCD" & vbCrLf & _
                                                 "          UNION" & vbCrLf & _
                                                 "          SELECT CINmb, KindCD, Class1, Class2, CINM, '2' AS Sort0, Sort" & vbCrLf & _
                                                 "          FROM ci_info_tb" & vbCrLf & _
                                                 "          WHERE CIStatusCD = :CIStatusCD" & vbCrLf & _
                                                 "          AND CIKbnCD= :CIKbnCD" & vbCrLf & _
                                                 " ) AS cit" & vbCrLf & _
                                                 " ORDER BY Sort0, Sort"

    '[SELECT]担当者ユーザ情報取得SQL
    Private strSelectPrbTantoSql As String = "SELECT" & vbCrLf & _
                                             " HBKUsrNM," & vbCrLf & _
                                             " HBKUsrNmKana," & vbCrLf & _
                                             " HBKUsrMailAdd," & vbCrLf & _
                                             " HBKUsrNMAimai," & vbCrLf & _
                                             " GroupCD" & vbCrLf & _
                                             " FROM hbkusr_mtb m1" & vbCrLf & _
                                             " LEFT JOIN szk_mtb m2 ON m1.hbkusrid=m2.hbkusrid " & vbCrLf & _
                                             " WHERE m1.HBKUsrID = :HBKUsrID"

    '[SELECT]問題基本情報取得SQL
    Private strSelectProblemInfoSql As String = "SELECT" & vbCrLf & _
                                                " pit.PrbNmb," & vbCrLf & _
                                                " pit.ProcessKbn," & vbCrLf & _
                                                " pit.ProcessStateCD," & vbCrLf & _
                                                " pit.PrbCaseCD," & vbCrLf & _
                                                " CASE WHEN pit.KaisiDT IS NULL" & vbCrLf & _
                                                "      THEN ''" & vbCrLf & _
                                                "      ELSE TO_CHAR(pit.KaisiDT, 'YYYY/MM/DD HH24:MI')" & vbCrLf & _
                                                " END AS KaisiDT," & vbCrLf & _
                                                " CASE WHEN pit.KanryoDT IS NULL" & vbCrLf & _
                                                "      THEN ''" & vbCrLf & _
                                                "      ELSE TO_CHAR(pit.KanryoDT, 'YYYY/MM/DD HH24:MI')" & vbCrLf & _
                                                " END AS KanryoDT," & vbCrLf & _
                                                " pit.Title," & vbCrLf & _
                                                " pit.Naiyo," & vbCrLf & _
                                                " pit.Taisyo," & vbCrLf & _
                                                " pit.SystemNmb," & vbCrLf & _
                                                " pit.ApproverID," & vbCrLf & _
                                                " pit.ApproverNM," & vbCrLf & _
                                                " pit.RecorderID," & vbCrLf & _
                                                " pit.RecorderNM," & vbCrLf & _
                                                " pit.TantoGrpCD," & vbCrLf & _
                                                " pit.PrbTantoID," & vbCrLf & _
                                                " pit.PrbTantoNM," & vbCrLf & _
                                                " pit.BIko1," & vbCrLf & _
                                                " pit.Biko2," & vbCrLf & _
                                                " pit.Biko3," & vbCrLf & _
                                                " pit.Biko4," & vbCrLf & _
                                                " pit.Biko5," & vbCrLf & _
                                                " pit.FreeFlg1," & vbCrLf & _
                                                " pit.FreeFlg2," & vbCrLf & _
                                                " pit.FreeFlg3," & vbCrLf & _
                                                " pit.FreeFlg4," & vbCrLf & _
                                                " pit.FreeFlg5," & vbCrLf & _
                                                " pit.TitleAimai," & vbCrLf & _
                                                " pit.NaiyoAimai," & vbCrLf & _
                                                " pit.TaisyoAimai," & vbCrLf & _
                                                " pit.BikoAimai," & vbCrLf & _
                                                " pit.PrbTantIDAimai," & vbCrLf & _
                                                " pit.PrbTantNMAimai," & vbCrLf & _
                                                " pit.RegDT," & vbCrLf & _
                                                " pit.RegGrpCD," & vbCrLf & _
                                                " pit.RegID," & vbCrLf & _
                                                " pit.UpdateDT," & vbCrLf & _
                                                " pit.UpGrpCD," & vbCrLf & _
                                                " pit.UpdateID," & vbCrLf & _
                                                " gm1.GroupNM || ' ' || hm1.HBKUsrNM || ' ' || TO_CHAR(pit.RegDT, 'YYYY/MM/DD HH24:MI') AS LblRegInfo," & vbCrLf & _
                                                " gm2.GroupNM || ' ' || hm2.HBKUsrNM || ' ' || TO_CHAR(pit.UpdateDT, 'YYYY/MM/DD HH24:MI') AS LblUpdateInfo," & vbCrLf & _
                                                " gm1.GroupNM AS mail_RegGp," & vbCrLf & _
                                                " hm1.HBKUsrNM AS mail_RegUsr," & vbCrLf & _
                                                " TO_CHAR(pit.RegDT, 'YYYY/MM/DD HH24:MI') AS mail_RegDt," & vbCrLf & _
                                                " gm2.GroupNM AS mail_UpdateGp," & vbCrLf & _
                                                " hm2.HBKUsrNM AS mail_UpdateUsr," & vbCrLf & _
                                                " TO_CHAR(pit.UpdateDT, 'YYYY/MM/DD HH24:MI') AS mail_UpdateDt" & vbCrLf & _
                                                " FROM problem_info_tb AS pit" & vbCrLf & _
                                                " LEFT OUTER JOIN grp_mtb AS gm1 ON gm1.GroupCD = pit.RegGrpCD " & vbCrLf & _
                                                " LEFT OUTER JOIN hbkusr_mtb AS hm1 ON hm1.HBKUsrID = pit.RegID " & vbCrLf & _
                                                " LEFT OUTER JOIN grp_mtb AS gm2 ON gm2.GroupCD = pit.UpGrpCD " & vbCrLf & _
                                                " LEFT OUTER JOIN hbkusr_mtb AS hm2 ON hm2.HBKUsrID = pit.UpdateID " & vbCrLf & _
                                                " WHERE pit.PrbNmb = :PrbNmb"

    '[SELECT]問題作業履歴取得SQL
    'Private strSelectProblemWkRirekiSql As String = "SELECT" & vbCrLf & _
    '                                                " pwrt.WorkRirekiNmb," & vbCrLf & _
    '                                                " pwrt.WorkStateCD," & vbCrLf & _
    '                                                " pwrt.WorkNaiyo," & vbCrLf & _
    '                                                " CASE WHEN pwrt.WorkSceDT IS NULL" & vbCrLf & _
    '                                                "      THEN ''" & vbCrLf & _
    '                                                "      ELSE TO_CHAR(pwrt.WorkSceDT, 'YYYY/MM/DD HH24:MI')" & vbCrLf & _
    '                                                " END AS WorkSceDT," & vbCrLf & _
    '                                                " CASE WHEN pwrt.WorkStDT IS NULL" & vbCrLf & _
    '                                                "      THEN ''" & vbCrLf & _
    '                                                "      ELSE TO_CHAR(pwrt.WorkStDT, 'YYYY/MM/DD HH24:MI')" & vbCrLf & _
    '                                                " END AS WorkStDT," & vbCrLf & _
    '                                                " CASE WHEN pwrt.WorkEdDT IS NULL" & vbCrLf & _
    '                                                "      THEN ''" & vbCrLf & _
    '                                                "      ELSE TO_CHAR(pwrt.WorkEdDT, 'YYYY/MM/DD HH24:MI')" & vbCrLf & _
    '                                                " END AS WorkEdDT," & vbCrLf & _
    '                                                " pwrt.SystemNmb" & vbCrLf & _
    '                                                " FROM problem_wk_rireki_tb AS pwrt" & vbCrLf & _
    '                                                " WHERE pwrt.PrbNmb = :PrbNmb" & vbCrLf & _
    '" ORDER BY pwrt.WorkStDT DESC, pwrt.WorkEdDT DESC, pwrt.WorkRirekiNmb"

    Private strSelectProblemWkRirekiSql As String = "SELECT " & vbCrLf & _
                                                     " ct.WorkRirekiNmb " & vbCrLf & _
                                                     ",ct.workstatecd " & vbCrLf & _
                                                     ",ct.SystemNmb " & vbCrLf & _
                                                     ",ct.WorkNaiyo " & vbCrLf & _
                                                     ",ct.WorkSceDT " & vbCrLf & _
                                                     ",ct.WorkStDT " & vbCrLf & _
                                                     ",ct.WorkEdDT " & vbCrLf & _
                                                     "FROM problem_wk_rireki_tb ct " & vbCrLf & _
                                                     "WHERE ct.PrbNmb = :PrbNmb " & vbCrLf & _
                                                     "ORDER BY ct.WorkStDT DESC, ct.WorkEdDT DESC, ct.WorkRirekiNmb"

    '[SELECT]問題作業担当取得SQL
    Private strSelectProblemWkTantoSql As String = "SELECT" & vbCrLf & _
                                                   " pwtt.WorkRirekiNmb," & vbCrLf & _
                                                   " pwtt2.cnt," & vbCrLf & _
                                                   " pwtt.WorkTantoNmb," & vbCrLf & _
                                                   " pwtt.WorkTantoGrpNM," & vbCrLf & _
                                                   " pwtt.WorkTantoGrpCD," & vbCrLf & _
                                                   " pwtt.WorkTantoNM," & vbCrLf & _
                                                   " pwtt.WorkTantoID," & vbCrLf & _
                                                   " pwtt.RegDT," & vbCrLf & _
                                                   " pwtt.RegGrpCD," & vbCrLf & _
                                                   " pwtt.RegID " & vbCrLf & _
                                                   " FROM problem_wk_tanto_tb AS pwtt" & vbCrLf & _
                                                   " LEFT OUTER JOIN grp_mtb AS gm1 ON gm1.GroupCD = pwtt.WorkTantoGrpCD" & vbCrLf & _
                                                   " LEFT OUTER JOIN hbkusr_mtb hm1 ON hm1.HBKUsrID = pwtt.WorkTantoID" & vbCrLf & _
                                                   " INNER JOIN (" & vbCrLf & _
                                                   "               SELECT WorkRirekiNmb, COUNT(*) AS cnt" & vbCrLf & _
                                                   "               FROM problem_wk_tanto_tb AS a" & vbCrLf & _
                                                   "               WHERE a.PrbNmb = :PrbNmb" & vbCrLf & _
                                                   "               GROUP BY WorkRirekiNmb" & vbCrLf & _
                                                   " ) AS pwtt2 ON pwtt2.WorkRirekiNmb = pwtt.WorkRirekiNmb" & vbCrLf & _
                                                   " WHERE pwtt.PrbNmb = :PrbNmb" & vbCrLf & _
                                                   " ORDER BY pwtt.WorkRirekiNmb, pwtt.WorkTantoNmb"

    '[SELECT]問題対応関係取得SQL
    Private strSelectProblemKankeiSql As String = "SELECT" & vbCrLf & _
                                                  " a.RelationKbn," & vbCrLf & _
                                                  " a.RelationID," & vbCrLf & _
                                                  " a.GroupNM," & vbCrLf & _
                                                  " a.HBKUsrNM," & vbCrLf & _
                                                  " a.RegDT," & vbCrLf & _
                                                  " a.RegGrpCD," & vbCrLf & _
                                                  " a.RegID," & vbCrLf & _
                                                  " a.UpdateDT," & vbCrLf & _
                                                  " a.UpGrpCD," & vbCrLf & _
                                                  " a.UpdateID" & vbCrLf & _
                                                  " FROM (" & vbCrLf & _
                                                  "        SELECT" & vbCrLf & _
                                                  "        pkt.RelationKbn," & vbCrLf & _
                                                  "        pkt.RelationID," & vbCrLf & _
                                                  "        gm.GroupNM AS GroupNM," & vbCrLf & _
                                                  "        '' AS HBKUsrNM," & vbCrLf & _
                                                  "        pkt.RegDT," & vbCrLf & _
                                                  "        pkt.RegGrpCD," & vbCrLf & _
                                                  "        pkt.RegID," & vbCrLf & _
                                                  "        pkt.UpdateDT," & vbCrLf & _
                                                  "        pkt.UpGrpCD," & vbCrLf & _
                                                  "        pkt.UpdateID," & vbCrLf & _
                                                  "        pkt.EntryNmb" & vbCrLf & _
                                                  "        FROM problem_kankei_tb AS pkt" & vbCrLf & _
                                                  "        INNER JOIN grp_mtb AS gm ON pkt.RelationID = gm.GroupCD" & vbCrLf & _
                                                  "        WHERE pkt.PrbNmb = :PrbNmb" & vbCrLf & _
                                                  "        AND pkt.RelationKbn = :KbnGrp" & vbCrLf & _
                                                  "        UNION ALL" & vbCrLf & _
                                                  "        SELECT" & vbCrLf & _
                                                  "        pkt.RelationKbn," & vbCrLf & _
                                                  "        pkt.RelationID," & vbCrLf & _
                                                  "        ''," & vbCrLf & _
                                                  "        hm.HBKUsrNM," & vbCrLf & _
                                                  "        pkt.RegDT," & vbCrLf & _
                                                  "        pkt.RegGrpCD," & vbCrLf & _
                                                  "        pkt.RegID," & vbCrLf & _
                                                  "        pkt.UpdateDT," & vbCrLf & _
                                                  "        pkt.UpGrpCD," & vbCrLf & _
                                                  "        pkt.UpdateID," & vbCrLf & _
                                                  "        pkt.EntryNmb" & vbCrLf & _
                                                  "        FROM problem_kankei_tb AS pkt" & vbCrLf & _
                                                  "        INNER JOIN hbkusr_mtb AS hm ON hm.HBKUsrID = pkt.RelationID" & vbCrLf & _
                                                  "        WHERE pkt.PrbNmb = :PrbNmb" & vbCrLf & _
                                                  "        AND pkt.RelationKbn = :KbnUsr" & vbCrLf & _
                                                  " ) AS a" & vbCrLf & _
                                                  " ORDER BY a.EntryNmb"

    '[SELECT]プロセスリンク情報取得SQL
    Private strSelectProcessLinkSql As String = "SELECT" & vbCrLf & _
                                                " CASE kt.ProcessKbn" & vbCrLf & _
                                                "      WHEN :Kbn_Incident THEN :Kbn_Incident_NMR" & vbCrLf & _
                                                "      WHEN :Kbn_Question THEN :Kbn_Question_NMR" & vbCrLf & _
                                                "      WHEN :Kbn_Change   THEN :Kbn_Change_NMR" & vbCrLf & _
                                                "      WHEN :Kbn_Release  THEN :Kbn_Release_NMR" & vbCrLf & _
                                                "      ELSE ''" & vbCrLf & _
                                                " END AS ProcessKbnNMR," & vbCrLf & _
                                                " MngNmb," & vbCrLf & _
                                                " ProcessKbn," & vbCrLf & _
                                                " kt.RegDT," & vbCrLf & _
                                                " kt.RegGrpCD," & vbCrLf & _
                                                " kt.RegID," & vbCrLf & _
                                                " kt.UpdateDT," & vbCrLf & _
                                                " kt.UpGrpCD," & vbCrLf & _
                                                " kt.UpdateID" & vbCrLf & _
                                                " FROM (" & vbCrLf & _
                                                "         SELECT" & vbCrLf & _
                                                "         kt1.LinkSakiNmb AS MngNmb," & vbCrLf & _
                                                "         kt1.LinkSakiProcesskbn AS ProcessKbn," & vbCrLf & _
                                                "         kt1.RegDT," & vbCrLf & _
                                                "         kt1.RegGrpCD," & vbCrLf & _
                                                "         kt1.RegID," & vbCrLf & _
                                                "         kt1.UpdateDT," & vbCrLf & _
                                                "         kt1.UpGrpCD," & vbCrLf & _
                                                "         kt1.UpdateID," & vbCrLf & _
                                                "         kt1.EntryDt" & vbCrLf & _
                                                "         FROM process_link_tb AS kt1" & vbCrLf & _
                                                "         WHERE kt1.LinkMotoNmb = :Nmb" & vbCrLf & _
                                                "         AND kt1.LinkMotoProcesskbn = :LinkMotoProcesskbn" & vbCrLf & _
                                                "         UNION" & vbCrLf & _
                                                "         SELECT" & vbCrLf & _
                                                "         kt2.LinkMotoNmb AS MngNmb," & vbCrLf & _
                                                "         kt2.LinkMotoProcesskbn AS ProcessKbn," & vbCrLf & _
                                                "         kt2.RegDT," & vbCrLf & _
                                                "         kt2.RegGrpCD," & vbCrLf & _
                                                "         kt2.RegID," & vbCrLf & _
                                                "         kt2.UpdateDT," & vbCrLf & _
                                                "         kt2.UpGrpCD," & vbCrLf & _
                                                "         kt2.UpdateID," & vbCrLf & _
                                                "         kt2.EntryDt" & vbCrLf & _
                                                "         FROM process_link_tb AS kt2 " & vbCrLf & _
                                                "         WHERE kt2.LinkSakiNmb = :Nmb " & vbCrLf & _
                                                "         AND kt2.LinkSakiProcesskbn = :LinkMotoProcesskbn" & vbCrLf & _
                                                " ) AS kt" & vbCrLf & _
                                                " ORDER BY kt.EntryDt"

    '********************************************************************
    'ソート順対応を行う
    '[SELECT]問題CYSPR情報取得SQL
    Private strSelectProblemCysprSql As String = "SELECT" & vbCrLf & _
                                                 " CysprNmb," & vbCrLf & _
                                                 " cysprnmb AS bef," & vbCrLf & _
                                                 " RegDT," & vbCrLf & _
                                                 " RegGrpCD," & vbCrLf & _
                                                 " RegID," & vbCrLf & _
                                                 " UpdateDT," & vbCrLf & _
                                                 " UpGrpCD," & vbCrLf & _
                                                 " UpdateID" & vbCrLf & _
                                                 " FROM" & vbCrLf & _
                                                 " problem_cyspr_tb" & vbCrLf & _
                                                 " WHERE PrbNmb = :PrbNmb" & vbCrLf & _
                                                 " ORDER BY EntryNmb"
    '********************************************************************

    '[SELECT]問題関連ファイル情報取得SQL
    Private strSelectProblemFileSql As String = "SELECT" & vbCrLf & _
                                                " pft.FileNaiyo AS FileNaiyo," & vbCrLf & _
                                                " pft.FileMngNmb AS FileMngNmb," & vbCrLf & _
                                                " fmt.FilePath || E'\\' || fmt.FileNM || fmt.Ext AS FilePath," & vbCrLf & _
                                                " pft.EntryNmb," & vbCrLf & _
                                                " pft.RegDT," & vbCrLf & _
                                                " pft.RegGrpCD," & vbCrLf & _
                                                " pft.RegID" & vbCrLf & _
                                                " FROM problem_file_tb AS pft" & vbCrLf & _
                                                " INNER JOIN file_mng_tb AS fmt ON fmt.FileMngNmb = pft.FileMngNmb" & vbCrLf & _
                                                " WHERE pft.PrbNmb = :PrbNmb" & vbCrLf & _
                                                " ORDER BY pft.RegDT"

    ''[SELECT]会議結果情報取得SQL
    'Private strSelectMtgResultSql As String = "SELECT" & vbCrLf & _
    '                                        " mt.MeetingNmb AS MeetingNmb," & vbCrLf & _
    '                                        " TO_CHAR(mt.JisiSTDT,'YYYY/MM/DD') AS JisiDT," & vbCrLf & _
    '                                        " mt.Title," & vbCrLf & _
    '                                        " CASE mrt.ResultKbn" & vbCrLf & _
    '                                        "      WHEN :Kbn_NO THEN :Kbn_NO_NM" & vbCrLf & _
    '                                        "      WHEN :Kbn_OK THEN :Kbn_OK_NM" & vbCrLf & _
    '                                        "      WHEN :Kbn_NG THEN :Kbn_NG_NM" & vbCrLf & _
    '                                        "      ELSE ''" & vbCrLf & _
    '                                        " END AS ResultKbnNM," & vbCrLf & _
    '                                        " mrt.ResultKbn" & vbCrLf & _
    '                                        " FROM meeting_result_tb AS mrt" & vbCrLf & _
    '                                        " LEFT OUTER JOIN meeting_tb AS mt ON mt.MeetingNmb = mrt.MeetingNmb" & vbCrLf & _
    '                                        " WHERE mrt.ProcessKbn = :ProcessKbn" & vbCrLf & _
    '                                        " AND mrt.ProcessNmb = :ProcessNmb" & vbCrLf & _
    '                                        " ORDER BY mt.JisiSTDT DESC, mt.Title"
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
                                                 "FROM MEETING_RESULT_TB mrt" & vbCrLf & _
                                                 "LEFT JOIN MEETING_TB mt ON mt.MeetingNmb = mrt.MeetingNmb " & vbCrLf & _
                                                 "WHERE  mrt.processkbn = :processkbn " & vbCrLf & _
                                                 "AND  mrt.processnmb = :processnmb " & vbCrLf & _
                                                 "ORDER BY mt.JisiSTDT DESC ,mt.Title  "


    '[SELECT]対応関係者権限チェック用G取得SQL
    Private strSelectCheckPrbKankeiGSql As String = "SELECT" & vbCrLf & _
                                                    " Count(*)" & vbCrLf & _
                                                    " FROM problem_kankei_tb AS pkt" & vbCrLf & _
                                                    " WHERE pkt.PrbNmb = :PrbNmb" & vbCrLf & _
                                                    " AND pkt.RelationID = :GrpID" & vbCrLf & _
                                                    " AND pkt.RelationKbn = :KbnGrp"

    '[SELECT]対応関係者権限チェック用G取得SQL
    Private strSelectCheckPrbKankeiUSql As String = "SELECT" & vbCrLf & _
                                                    " Count(*)" & vbCrLf & _
                                                    " FROM problem_kankei_tb AS pkt" & vbCrLf & _
                                                    " WHERE pkt.PrbNmb = :PrbNmb" & vbCrLf & _
                                                    " AND pkt.RelationID = :UsrID " & vbCrLf & _
                                                    " AND pkt.RelationKbn = :KbnUsr"

    '[SELECT]対処承認者ユーザ情報取得SQL
    Private strSelectPrbApproverSql As String = "SELECT" & vbCrLf & _
                                                " EndUsrNM" & vbCrLf & _
                                                " FROM endusr_mtb" & vbCrLf & _
                                                " WHERE EndUsrID = :EndUsrID"

    '[SELECT]承認記録者ユーザ情報取得SQL
    Private strSelectPrbRecorderSql As String = "SELECT" & vbCrLf & _
                                                " HBKUsrNM" & vbCrLf & _
                                                " FROM hbkusr_mtb" & vbCrLf & _
                                                " WHERE HBKUsrID = :HBKUsrID"

    '[SELECT]対応関係情報(対象システムおける対応関係存在チェック用)取得SQL
    Private strCheckSysKankeiUSql As String = "SELECT" & vbCrLf & _
                                              " kt.relationkbn," & vbCrLf & _
                                              " kt.relationid " & vbCrLf & _
                                              " FROM kankei_tb AS kt" & vbCrLf & _
                                              " WHERE kt.CInmb = :SystemNmb"

    '[INSERT]問題共通情報登録SQL
    Private strInsertProblemInfoSql As String = "INSERT INTO problem_info_tb (" & vbCrLf & _
                                                " PrbNmb," & vbCrLf & _
                                                " ProcessKbn," & vbCrLf & _
                                                " ProcessStateCD," & vbCrLf & _
                                                " PrbCaseCD," & vbCrLf & _
                                                " KaisiDT," & vbCrLf & _
                                                " KanryoDT," & vbCrLf & _
                                                " Title," & vbCrLf & _
                                                " Naiyo," & vbCrLf & _
                                                " Taisyo," & vbCrLf & _
                                                " SystemNmb," & vbCrLf & _
                                                " ApproverID," & vbCrLf & _
                                                " ApproverNM," & vbCrLf & _
                                                " RecorderID," & vbCrLf & _
                                                " RecorderNM," & vbCrLf & _
                                                " TantoGrpCD," & vbCrLf & _
                                                " PrbTantoID," & vbCrLf & _
                                                " PrbTantoNM," & vbCrLf & _
                                                " BIko1," & vbCrLf & _
                                                " Biko2," & vbCrLf & _
                                                " Biko3," & vbCrLf & _
                                                " Biko4," & vbCrLf & _
                                                " Biko5," & vbCrLf & _
                                                " FreeFlg1," & vbCrLf & _
                                                " FreeFlg2," & vbCrLf & _
                                                " FreeFlg3," & vbCrLf & _
                                                " FreeFlg4," & vbCrLf & _
                                                " FreeFlg5," & vbCrLf & _
                                                " TitleAimai," & vbCrLf & _
                                                " NaiyoAimai," & vbCrLf & _
                                                " TaisyoAimai," & vbCrLf & _
                                                " BikoAimai," & vbCrLf & _
                                                " PrbTantIDAimai," & vbCrLf & _
                                                " PrbTantNMAimai," & vbCrLf & _
                                                " RegDT," & vbCrLf & _
                                                " RegGrpCD," & vbCrLf & _
                                                " RegID," & vbCrLf & _
                                                " UpdateDT," & vbCrLf & _
                                                " UpGrpCD," & vbCrLf & _
                                                " UpdateID" & vbCrLf & _
                                                " ) VALUES (" & vbCrLf & _
                                                " :PrbNmb," & vbCrLf & _
                                                " :ProcessKbn," & vbCrLf & _
                                                " :ProcessStateCD," & vbCrLf & _
                                                " :PrbCaseCD," & vbCrLf & _
                                                " :KaisiDT," & vbCrLf & _
                                                " :KanryoDT," & vbCrLf & _
                                                " :Title," & vbCrLf & _
                                                " :Naiyo," & vbCrLf & _
                                                " :Taisyo," & vbCrLf & _
                                                " :SystemNmb," & vbCrLf & _
                                                " :ApproverID," & vbCrLf & _
                                                " :ApproverNM," & vbCrLf & _
                                                " :RecorderID," & vbCrLf & _
                                                " :RecorderNM," & vbCrLf & _
                                                " :TantoGrpCD," & vbCrLf & _
                                                " :PrbTantoID," & vbCrLf & _
                                                " :PrbTantoNM," & vbCrLf & _
                                                " :BIko1," & vbCrLf & _
                                                " :Biko2," & vbCrLf & _
                                                " :Biko3," & vbCrLf & _
                                                " :Biko4," & vbCrLf & _
                                                " :Biko5," & vbCrLf & _
                                                " :FreeFlg1," & vbCrLf & _
                                                " :FreeFlg2," & vbCrLf & _
                                                " :FreeFlg3," & vbCrLf & _
                                                " :FreeFlg4," & vbCrLf & _
                                                " :FreeFlg5," & vbCrLf & _
                                                " :TitleAimai," & vbCrLf & _
                                                " :NaiyoAimai," & vbCrLf & _
                                                " :TaisyoAimai," & vbCrLf & _
                                                " :BikoAimai," & vbCrLf & _
                                                " :PrbTantIDAimai," & vbCrLf & _
                                                " :PrbTantNMAimai," & vbCrLf & _
                                                " :RegDT," & vbCrLf & _
                                                " :RegGrpCD," & vbCrLf & _
                                                " :RegID," & vbCrLf & _
                                                " :UpdateDT," & vbCrLf & _
                                                " :UpGrpCD," & vbCrLf & _
                                                " :UpdateID" & vbCrLf & _
                                                " )"

    '[INSERT]問題作業履歴登録SQL  
    'Private strInsertProblemWkRirekiSql As String = "INSERT INTO problem_wk_rireki_tb (" & vbCrLf & _
    '                                                " PrbNmb," & vbCrLf & _
    '                                                " WorkRirekiNmb," & vbCrLf & _
    '                                                " WorkStateCD," & vbCrLf & _
    '                                                " WorkNaiyo," & vbCrLf & _
    '                                                " WorkSceDT," & vbCrLf & _
    '                                                " WorkStDT," & vbCrLf & _
    '                                                " WorkEdDT," & vbCrLf & _
    '                                                " SystemNmb," & vbCrLf & _
    '                                                " RegDT," & vbCrLf & _
    '                                                " RegGrpCD," & vbCrLf & _
    '                                                " RegID," & vbCrLf & _
    '                                                " UpdateDT," & vbCrLf & _
    '                                                " UpGrpCD," & vbCrLf & _
    '                                                " UpdateID" & vbCrLf & _
    '                                                " ) VALUES (" & vbCrLf & _
    '                                                " :PrbNmb," & vbCrLf & _
    '                                                " :WorkRirekiNmb," & vbCrLf & _
    '                                                " :WorkStateCD," & vbCrLf & _
    '                                                " :WorkNaiyo," & vbCrLf & _
    '                                                " :WorkSceDT," & vbCrLf & _
    '                                                " :WorkStDT," & vbCrLf & _
    '                                                " :WorkEdDT," & vbCrLf & _
    '                                                " :SystemNmb," & vbCrLf & _
    '                                                " :RegDT," & vbCrLf & _
    '                                                " :RegGrpCD," & vbCrLf & _
    '                                                " :RegID," & vbCrLf & _
    '                                                " :UpdateDT," & vbCrLf & _
    '                                                " :UpGrpCD," & vbCrLf & _
    '                                                " :UpdateID" & vbCrLf & _
    '                                                " )"
    Private strInsertProblemWkRirekiSql As String = "INSERT INTO problem_wk_rireki_tb (" & vbCrLf & _
                                          " PrbNmb " & vbCrLf & _
                                          ",workrirekinmb " & vbCrLf & _
                                          ",workstatecd " & vbCrLf & _
                                          ",worknaiyo " & vbCrLf & _
                                          ",workscedt " & vbCrLf & _
                                          ",workstdt " & vbCrLf & _
                                          ",workeddt " & vbCrLf & _
                                          ",systemnmb " & vbCrLf & _
                                          ",RegDT " & vbCrLf & _
                                          ",RegGrpCD " & vbCrLf & _
                                          ",RegID " & vbCrLf & _
                                          ",UpdateDT " & vbCrLf & _
                                          ",UpGrpCD " & vbCrLf & _
                                          ",UpdateID " & vbCrLf & _
                                          ") VALUES (" & vbCrLf & _
                                          " :PrbNmb " & vbCrLf & _
                                          ",(SELECT COALESCE(MAX(workrirekinmb),0)+1 FROM problem_wk_rireki_tb WHERE PrbNmb=:PrbNmb) " & vbCrLf & _
                                          ",:workstatecd " & vbCrLf & _
                                          ",:worknaiyo " & vbCrLf & _
                                          ",:workscedt " & vbCrLf & _
                                          ",:workstdt " & vbCrLf & _
                                          ",:workeddt " & vbCrLf & _
                                          ",:systemnmb " & vbCrLf & _
                                          ",:RegDT " & vbCrLf & _
                                          ",:RegGrpCD " & vbCrLf & _
                                          ",:RegID " & vbCrLf & _
                                          ",:UpdateDT " & vbCrLf & _
                                          ",:UpGrpCD " & vbCrLf & _
                                          ",:UpdateID " & vbCrLf & _
                                          ") "
    '[UPDATE]作業履歴SQL
    Private strUpdateProblemWkRirekiSql As String = "UPDATE problem_wk_rireki_tb SET" & vbCrLf & _
                                                    " workstatecd    =:workstatecd     " & vbCrLf & _
                                                    ",worknaiyo     =:worknaiyo      " & vbCrLf & _
                                                    ",workscedt     =:workscedt      " & vbCrLf & _
                                                    ",workstdt      =:workstdt       " & vbCrLf & _
                                                    ",workeddt      =:workeddt       " & vbCrLf & _
                                                    ",systemnmb     =:systemnmb      " & vbCrLf & _
                                                    ",UpdateDT      =:UpdateDT       " & vbCrLf & _
                                                    ",UpGrpCD       =:UpGrpCD        " & vbCrLf & _
                                                    ",UpdateID      =:UpdateID       " & vbCrLf & _
                                                    "WHERE PrbNmb =:PrbNmb " & vbCrLf & _
                                                    "AND workrirekinmb=:workrirekinmb "



    '[INSERT]問題作業担当登録SQL
    'Private strInsertProblemWkTantoSql As String = "INSERT INTO problem_wk_tanto_tb (" & vbCrLf & _
    '                                               " PrbNmb," & vbCrLf & _
    '                                               " WorkRirekiNmb," & vbCrLf & _
    '                                               " WorkTantoNmb," & vbCrLf & _
    '                                               " WorkTantoGrpCD," & vbCrLf & _
    '                                               " WorkTantoID," & vbCrLf & _
    '                                               " WorkTantoGrpNM," & vbCrLf & _
    '                                               " WorkTantoNM," & vbCrLf & _
    '                                               " RegDT," & vbCrLf & _
    '                                               " RegGrpCD," & vbCrLf & _
    '                                               " RegID," & vbCrLf & _
    '                                               " UpdateDT," & vbCrLf & _
    '                                               " UpGrpCD," & vbCrLf & _
    '                                               " UpdateID" & vbCrLf & _
    '                                               " ) VALUES (" & vbCrLf & _
    '                                               " :PrbNmb," & vbCrLf & _
    '                                               " :WorkRirekiNmb," & vbCrLf & _
    '                                               " :WorkTantoNmb," & vbCrLf & _
    '                                               " :WorkTantoGrpCD," & vbCrLf & _
    '                                               " :TantoID," & vbCrLf & _
    '                                               " :WorkTantoGrpNM," & vbCrLf & _
    '                                               " :WorkTantoNM," & vbCrLf & _
    '                                               " :RegDT," & vbCrLf & _
    '                                               " :RegGrpCD," & vbCrLf & _
    '                                               " :RegID," & vbCrLf & _
    '                                               " :UpdateDT," & vbCrLf & _
    '                                               " :UpGrpCD," & vbCrLf & _
    '                                               " :UpdateID" & vbCrLf & _
    '                                               " )"
    '[INSERT]作業担当SQL
    Private strInsertProblemWkTantoSql As String = "INSERT INTO problem_wk_tanto_tb (" & vbCrLf & _
                                             " PrbNmb " & vbCrLf & _
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
                                             ") VALUES (" & vbCrLf & _
                                             " :PrbNmb " & vbCrLf & _
                                             ",(SELECT COALESCE(MAX(workrirekinmb),0) FROM problem_wk_rireki_tb WHERE PrbNmb=:PrbNmb) " & vbCrLf & _
                                             ",:worktantonmb " & vbCrLf & _
                                             ",:worktantogrpcd " & vbCrLf & _
                                             ",:worktantoid " & vbCrLf & _
                                             ",:worktantogrpnm " & vbCrLf & _
                                             ",:worktantonm " & vbCrLf & _
                                             ",:RegDT " & vbCrLf & _
                                             ",:RegGrpCD " & vbCrLf & _
                                             ",:RegID " & vbCrLf & _
                                             ",:UpdateDT " & vbCrLf & _
                                             ",:UpGrpCD " & vbCrLf & _
                                             ",:UpdateID " & vbCrLf & _
                                             ") "

    '[INSERT]作業担当SQL
    Private strUpdateProblemWkTantoSql As String = "INSERT INTO problem_wk_tanto_tb (" & vbCrLf & _
                                             " PrbNmb " & vbCrLf & _
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
                                             ") VALUES (" & vbCrLf & _
                                             " :PrbNmb " & vbCrLf & _
                                             ",:workrirekinmb " & vbCrLf & _
                                             ",:worktantonmb " & vbCrLf & _
                                             ",:worktantogrpcd " & vbCrLf & _
                                             ",:worktantoid " & vbCrLf & _
                                             ",:worktantogrpnm " & vbCrLf & _
                                             ",:worktantonm " & vbCrLf & _
                                             ",:RegDT " & vbCrLf & _
                                             ",:RegGrpCD " & vbCrLf & _
                                             ",:RegID " & vbCrLf & _
                                             ",:UpdateDT " & vbCrLf & _
                                             ",:UpGrpCD " & vbCrLf & _
                                             ",:UpdateID " & vbCrLf & _
                                             ") "
    '[INSERT]問題対応関係登録SQL
    Private strInsertProblemKankeiSql As String = "INSERT INTO problem_kankei_tb ( " & vbCrLf & _
                                                  " PrbNmb," & vbCrLf & _
                                                  " RelationKbn," & vbCrLf & _
                                                  " RelationID," & vbCrLf & _
                                                  " EntryNmb," & vbCrLf & _
                                                  " RegDT," & vbCrLf & _
                                                  " RegGrpCD," & vbCrLf & _
                                                  " RegID," & vbCrLf & _
                                                  " UpdateDT," & vbCrLf & _
                                                  " UpGrpCD," & vbCrLf & _
                                                  " UpdateID" & vbCrLf & _
                                                  " ) VALUES (" & vbCrLf & _
                                                  " :PrbNmb," & vbCrLf & _
                                                  " :RelationKbn," & vbCrLf & _
                                                  " :RelationID," & vbCrLf & _
                                                  " (SELECT COALESCE(MAX(EntryNmb),0)+1 FROM problem_kankei_tb WHERE PrbNmb =:PrbNmb)," & vbCrLf & _
                                                  " :RegDT," & vbCrLf & _
                                                  " :RegGrpCD," & vbCrLf & _
                                                  " :RegID," & vbCrLf & _
                                                  " :UpdateDT," & vbCrLf & _
                                                  " :UpGrpCD," & vbCrLf & _
                                                  " :UpdateID" & vbCrLf & _
                                                  " )"

    '[INSERT]プロセスリンク登録SQL
    Private strInsertProcessLinkSql As String = "INSERT INTO process_link_tb (" & vbCrLf & _
                                                " LinkMotoProcessKbn," & vbCrLf & _
                                                " LinkMotoNmb," & vbCrLf & _
                                                " LinkSakiProcessKbn," & vbCrLf & _
                                                " LinkSakiNmb," & vbCrLf & _
                                                " EntryDT," & vbCrLf & _
                                                " RegDT," & vbCrLf & _
                                                " RegGrpCD," & vbCrLf & _
                                                " RegID," & vbCrLf & _
                                                " UpdateDT," & vbCrLf & _
                                                " UpGrpCD," & vbCrLf & _
                                                " UpdateID" & vbCrLf & _
                                                " ) VALUES (" & vbCrLf & _
                                                " :LinkMotoProcessKbn," & vbCrLf & _
                                                " :LinkMotoNmb," & vbCrLf & _
                                                " :LinkSakiProcessKbn," & vbCrLf & _
                                                " :LinkSakiNmb," & vbCrLf & _
                                                " :EntryDT," & vbCrLf & _
                                                " :RegDT," & vbCrLf & _
                                                " :RegGrpCD," & vbCrLf & _
                                                " :RegID," & vbCrLf & _
                                                " :UpdateDT," & vbCrLf & _
                                                " :UpGrpCD," & vbCrLf & _
                                                " :UpdateID" & vbCrLf & _
                                                " )"

    ''[INSERT]会議結果情報取得SQL
    'Private strInsertMtgResultSql As String = "INSERT INTO meeting_result_tb (" & vbCrLf & _
    '                                          " meetingnmb " & vbCrLf & _
    '                                          ",processkbn " & vbCrLf & _
    '                                          ",processnmb " & vbCrLf & _
    '                                          ",resultkbn " & vbCrLf & _
    '                                          ",EntryNmb " & vbCrLf & _
    '                                          ",RegDT " & vbCrLf & _
    '                                          ",RegGrpCD " & vbCrLf & _
    '                                          ",RegID " & vbCrLf & _
    '                                          ",UpdateDT " & vbCrLf & _
    '                                          ",UpGrpCD " & vbCrLf & _
    '                                          ",UpdateID " & vbCrLf & _
    '                                          ") VALUES (" & vbCrLf & _
    '                                          " :meetingnmb " & vbCrLf & _
    '                                          ",:processkbn " & vbCrLf & _
    '                                          ",:processnmb " & vbCrLf & _
    '                                          ",:resultkbn  " & vbCrLf & _
    '                                          ",(SELECT COALESCE(MAX(EntryNmb),0)+1 FROM meeting_result_tb WHERE meetingnmb=:meetingnmb)" & vbCrLf & _
    '                                          ",:RegDT " & vbCrLf & _
    '                                          ",:RegGrpCD " & vbCrLf & _
    '                                          ",:RegID " & vbCrLf & _
    '                                          ",:UpdateDT " & vbCrLf & _
    '                                          ",:UpGrpCD " & vbCrLf & _
    '                                          ",:UpdateID " & vbCrLf & _
    '                                          ") "
    '[INSERT]会議結果情報SQL
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
                                              ") " & vbCrLf & _
                                              "SELECT " & vbCrLf & _
                                              " :meetingnmb " & vbCrLf & _
                                              ",:processkbn " & vbCrLf & _
                                              ",:processnmb " & vbCrLf & _
                                              ",0  " & vbCrLf & _
                                              ",(SELECT COALESCE(MAX(EntryNmb),0)+1 FROM meeting_result_tb WHERE meetingnmb=:meetingnmb) " & vbCrLf & _
                                              ",:RegDT " & vbCrLf & _
                                              ",:RegGrpCD " & vbCrLf & _
                                              ",:RegID " & vbCrLf & _
                                              ",:UpdateDT " & vbCrLf & _
                                              ",:UpGrpCD " & vbCrLf & _
                                              ",:UpdateID " & vbCrLf & _
                                              " WHERE NOT EXISTS (SELECT DISTINCT 1 FROM meeting_result_tb" & vbCrLf & _
                                              " WHERE meetingnmb=:meetingnmb " & vbCrLf & _
                                              " AND processkbn = :processkbn " & vbCrLf & _
                                              " AND processnmb = :processnmb " & vbCrLf & _
                                              ") "


    '[DELETE]プロセスリンク削除SQL
    Private strDeleteProcessLinkSql As String = "DELETE FROM process_link_tb" & vbCrLf & _
                                                " WHERE LinkMotoNmb =       :LinkMotoNmb" & vbCrLf & _
                                                " AND LinkMotoProcessKbn =  :LinkMotoProcessKbn" & vbCrLf & _
                                                " AND LinkSakiNmb =         :LinkSakiNmb" & vbCrLf & _
                                                " AND LinkSakiProcessKbn =  :LinkSakiProcessKbn"

    '************************************************************************************
    'ソート順対応をする
    '[INSERT]問題CYSPR情報登録SQL
    Private strInsertProblemCyspr As String = "INSERT INTO problem_cyspr_tb (" & vbCrLf & _
                                              " PrbNmb," & vbCrLf & _
                                              " CysprNmb," & vbCrLf & _
                                              " CysprNmbAimai," & vbCrLf & _
                                              " EntryNmb," & vbCrLf & _
                                              " RegDT," & vbCrLf & _
                                              " RegGrpCD," & vbCrLf & _
                                              " RegID," & vbCrLf & _
                                              " UpdateDT," & vbCrLf & _
                                              " UpGrpCD," & vbCrLf & _
                                              " UpdateID" & vbCrLf & _
                                              " ) VALUES (" & vbCrLf & _
                                              " :PrbNmb," & vbCrLf & _
                                              " :CysprNmb," & vbCrLf & _
                                              " :CysprNmbAimai," & vbCrLf & _
                                              " (SELECT COALESCE(MAX(EntryNmb),0)+1 FROM problem_cyspr_tb WHERE PrbNmb = :PrbNmb)," & vbCrLf & _
                                              " :RegDT," & vbCrLf & _
                                              " :RegGrpCD," & vbCrLf & _
                                              " :RegID," & vbCrLf & _
                                              " :UpdateDT," & vbCrLf & _
                                              " :UpGrpCD," & vbCrLf & _
                                              " :UpdateID" & vbCrLf & _
                                              " )"
    '************************************************************************************

    '[SELECT]新規ログNo取得SQL
    Private strSelectNewRirekiNoSql As String = "SELECT" & vbCrLf & _
                                                " COALESCE(MAX(pilt.logno),0)+1 AS LogNo" & vbCrLf & _
                                                " FROM problem_info_ltb AS pilt" & vbCrLf & _
                                                " WHERE pilt.PrbNmb = :PrbNmb"

    '[INSERT]問題共通情報ログ登録SQL
    Private strInsertProblemInfoLSql As String = "INSERT INTO problem_info_ltb (" & vbCrLf & _
                                                 " PrbNmb," & vbCrLf & _
                                                 " LogNo," & vbCrLf & _
                                                 " ProcessKbn," & vbCrLf & _
                                                 " ProcessStateCD," & vbCrLf & _
                                                 " PrbCaseCD," & vbCrLf & _
                                                 " KaisiDT," & vbCrLf & _
                                                 " KanryoDT," & vbCrLf & _
                                                 " Title," & vbCrLf & _
                                                 " Naiyo," & vbCrLf & _
                                                 " Taisyo," & vbCrLf & _
                                                 " SystemNmb," & vbCrLf & _
                                                 " ApproverID," & vbCrLf & _
                                                 " ApproverNM," & vbCrLf & _
                                                 " RecorderID," & vbCrLf & _
                                                 " RecorderNM," & vbCrLf & _
                                                 " TantoGrpCD," & vbCrLf & _
                                                 " PrbTantoID," & vbCrLf & _
                                                 " PrbTantoNM," & vbCrLf & _
                                                 " BIko1," & vbCrLf & _
                                                 " Biko2," & vbCrLf & _
                                                 " Biko3," & vbCrLf & _
                                                 " Biko4," & vbCrLf & _
                                                 " Biko5," & vbCrLf & _
                                                 " FreeFlg1," & vbCrLf & _
                                                 " FreeFlg2," & vbCrLf & _
                                                 " FreeFlg3," & vbCrLf & _
                                                 " FreeFlg4," & vbCrLf & _
                                                 " FreeFlg5," & vbCrLf & _
                                                 " TitleAimai," & vbCrLf & _
                                                 " NaiyoAimai," & vbCrLf & _
                                                 " TaisyoAimai," & vbCrLf & _
                                                 " BikoAimai," & vbCrLf & _
                                                 " PrbTantIDAimai," & vbCrLf & _
                                                 " PrbTantNMAimai," & vbCrLf & _
                                                 " RegDT," & vbCrLf & _
                                                 " RegGrpCD," & vbCrLf & _
                                                 " RegID," & vbCrLf & _
                                                 " UpdateDT," & vbCrLf & _
                                                 " UpGrpCD," & vbCrLf & _
                                                 " UpdateID" & vbCrLf & _
                                                 " )" & vbCrLf & _
                                                 " SELECT" & vbCrLf & _
                                                 " PrbNmb," & vbCrLf & _
                                                 " :LogNo," & vbCrLf & _
                                                 " ProcessKbn," & vbCrLf & _
                                                 " ProcessStateCD," & vbCrLf & _
                                                 " PrbCaseCD," & vbCrLf & _
                                                 " KaisiDT," & vbCrLf & _
                                                 " KanryoDT," & vbCrLf & _
                                                 " Title," & vbCrLf & _
                                                 " Naiyo," & vbCrLf & _
                                                 " Taisyo," & vbCrLf & _
                                                 " SystemNmb," & vbCrLf & _
                                                 " ApproverID," & vbCrLf & _
                                                 " ApproverNM," & vbCrLf & _
                                                 " RecorderID," & vbCrLf & _
                                                 " RecorderNM," & vbCrLf & _
                                                 " TantoGrpCD," & vbCrLf & _
                                                 " PrbTantoID," & vbCrLf & _
                                                 " PrbTantoNM," & vbCrLf & _
                                                 " BIko1," & vbCrLf & _
                                                 " Biko2," & vbCrLf & _
                                                 " Biko3," & vbCrLf & _
                                                 " Biko4," & vbCrLf & _
                                                 " Biko5," & vbCrLf & _
                                                 " FreeFlg1," & vbCrLf & _
                                                 " FreeFlg2," & vbCrLf & _
                                                 " FreeFlg3," & vbCrLf & _
                                                 " FreeFlg4," & vbCrLf & _
                                                 " FreeFlg5," & vbCrLf & _
                                                 " TitleAimai," & vbCrLf & _
                                                 " NaiyoAimai," & vbCrLf & _
                                                 " TaisyoAimai," & vbCrLf & _
                                                 " BikoAimai," & vbCrLf & _
                                                 " PrbTantIDAimai," & vbCrLf & _
                                                 " PrbTantNMAimai," & vbCrLf & _
                                                 " RegDT," & vbCrLf & _
                                                 " RegGrpCD," & vbCrLf & _
                                                 " RegID," & vbCrLf & _
                                                 " UpdateDT," & vbCrLf & _
                                                 " UpGrpCD," & vbCrLf & _
                                                 " UpdateID" & vbCrLf & _
                                                 " FROM problem_info_tb" & vbCrLf & _
                                                 " WHERE PrbNmb = :PrbNmb"


    '[INSERT]問題作業履歴ログ登録SQL
    Private strInsertProblemWkRirekiLSql As String = "INSERT INTO problem_wk_rireki_ltb (" & vbCrLf & _
                                                     " SELECT" & vbCrLf & _
                                                     " PrbNmb," & vbCrLf & _
                                                     " :LogNo, " & vbCrLf & _
                                                     " WorkRirekiNmb," & vbCrLf & _
                                                     " workstatecd," & vbCrLf & _
                                                     " WorkNaiyo," & vbCrLf & _
                                                     " WorkSceDT," & vbCrLf & _
                                                     " WorkStDT," & vbCrLf & _
                                                     " WorkEdDT," & vbCrLf & _
                                                     " SystemNmb," & vbCrLf & _
                                                     " RegDT," & vbCrLf & _
                                                     " RegGrpCD," & vbCrLf & _
                                                     " RegID," & vbCrLf & _
                                                     " UpdateDT," & vbCrLf & _
                                                     " UpGrpCD," & vbCrLf & _
                                                     " UpdateID" & vbCrLf & _
                                                     " FROM problem_wk_rireki_tb" & vbCrLf & _
                                                     " WHERE PrbNmb = :PrbNmb" & vbCrLf & _
                                                     " )"

    '[INSERT]問題作業担当ログ登録SQL 
    Private strInsertProblemWkTantoLSql As String = "INSERT INTO problem_wk_tanto_ltb (" & vbCrLf & _
                                                    " SELECT" & vbCrLf & _
                                                    " PrbNmb," & vbCrLf & _
                                                    " :LogNo," & vbCrLf & _
                                                    " WorkRirekiNmb," & vbCrLf & _
                                                    " WorkTantoNmb," & vbCrLf & _
                                                    " WorkTantoGrpCD," & vbCrLf & _
                                                    " WorkTantoID," & vbCrLf & _
                                                    " WorkTantoGrpNM," & vbCrLf & _
                                                    " WorkTantoNM," & vbCrLf & _
                                                    " RegDT," & vbCrLf & _
                                                    " RegGrpCD," & vbCrLf & _
                                                    " RegID," & vbCrLf & _
                                                    " UpdateDT," & vbCrLf & _
                                                    " UpGrpCD," & vbCrLf & _
                                                    " UpdateID" & vbCrLf & _
                                                    " FROM problem_wk_tanto_tb" & vbCrLf & _
                                                    " WHERE PrbNmb = :PrbNmb" & vbCrLf & _
                                                    " )"

    '[INSERT]問題対応関係ログ登録SQL
    Private strInsertProblemKankeiLSql As String = "INSERT INTO problem_kankei_ltb (" & vbCrLf & _
                                                   " SELECT" & vbCrLf & _
                                                   " PrbNmb," & vbCrLf & _
                                                   " :LogNo," & vbCrLf & _
                                                   " RelationKbn," & vbCrLf & _
                                                   " RelationID," & vbCrLf & _
                                                   " EntryNmb," & vbCrLf & _
                                                   " RegDT," & vbCrLf & _
                                                   " RegGrpCD," & vbCrLf & _
                                                   " RegID," & vbCrLf & _
                                                   " UpdateDT," & vbCrLf & _
                                                   " UpGrpCD," & vbCrLf & _
                                                   " UpdateID" & vbCrLf & _
                                                   " FROM problem_kankei_tb" & vbCrLf & _
                                                   " WHERE PrbNmb = :PrbNmb" & vbCrLf & _
                                                   " )"

    '[INSERT]問題プロセスリンク情報ログ登録SQL（リンク元）
    Private strInsertPLinkMotoLSql As String = "INSERT INTO problem_process_link_ltb (" & vbCrLf & _
                                               " SELECT" & vbCrLf & _
                                               " :PrbNmb," & vbCrLf & _
                                               " :LogNo," & vbCrLf & _
                                               " LinkMotoProcessKbn," & vbCrLf & _
                                               " LinkMotoNmb," & vbCrLf & _
                                               " LinkSakiProcessKbn," & vbCrLf & _
                                               " LinkSakiNmb," & vbCrLf & _
                                               " EntryDT," & vbCrLf & _
                                               " RegDT," & vbCrLf & _
                                               " RegGrpCD," & vbCrLf & _
                                               " RegID," & vbCrLf & _
                                               " UpdateDT," & vbCrLf & _
                                               " UpGrpCD," & vbCrLf & _
                                               " UpdateID" & vbCrLf & _
                                               " FROM process_link_tb" & vbCrLf & _
                                               " WHERE LinkMotoNmb = :PrbNmb" & vbCrLf & _
                                               " AND LinkMotoProcesskbn = :PKbn" & vbCrLf & _
                                               " )"

    '***************************************************************************
    '確認後削除
    '[INSERT]問題プロセスリンク情報ログ登録SQL（リンク先）
    'Private strInsertPLinkSakiLSql As String = "INSERT INTO problem_process_link_ltb (" & vbCrLf & _
    '                                           " SELECT" & vbCrLf & _
    '                                           " :PrbNmb," & vbCrLf & _
    '                                           " :LogNo," & vbCrLf & _
    '                                           " LinkMotoProcessKbn," & vbCrLf & _
    '                                           " LinkMotoNmb," & vbCrLf & _
    '                                           " LinkSakiProcessKbn," & vbCrLf & _
    '                                           " LinkSakiNmb," & vbCrLf & _
    '                                           " RegDT," & vbCrLf & _
    '                                           " RegGrpCD," & vbCrLf & _
    '                                           " RegID," & vbCrLf & _
    '                                           " UpdateDT," & vbCrLf & _
    '                                           " UpGrpCD," & vbCrLf & _
    '                                           " UpdateID" & vbCrLf & _
    '                                           " FROM process_link_tb" & vbCrLf & _
    '                                           " WHERE LinkSakiNmb = :PrbNmb" & vbCrLf & _
    '                                           " AND LinkSakiProcesskbn = :PKbn" & vbCrLf & _
    '                                           " )"
    '***************************************************************************

    '***************************************************************************
    'ソート順対応を行う
    '[INSERT]問題CYSPR情報ログ登録SQL
    Private strInsertProblemCysprLSql As String = "INSERT INTO problem_cyspr_ltb (" & vbCrLf & _
                                                  " SELECT" & vbCrLf & _
                                                  " :PrbNmb," & vbCrLf & _
                                                  " :LogNo," & vbCrLf & _
                                                  " CysprNmb," & vbCrLf & _
                                                  " CysprNmbAimai," & vbCrLf & _
                                                  " EntryNmb," & vbCrLf & _
                                                  " RegDT," & vbCrLf & _
                                                  " RegGrpCD," & vbCrLf & _
                                                  " RegID," & vbCrLf & _
                                                  " UpdateDT," & vbCrLf & _
                                                  " UpGrpCD," & vbCrLf & _
                                                  " UpdateID" & vbCrLf & _
                                                  " FROM problem_cyspr_tb" & vbCrLf & _
                                                  " WHERE PrbNmb = :PrbNmb" & vbCrLf & _
                                                  " )"
    '***************************************************************************

    '[INSERT]問題関連ファイル情報ログ登録SQL
    Private strInsertProblemFileLSql As String = "INSERT INTO problem_file_ltb (" & vbCrLf & _
                                                 " SELECT" & vbCrLf & _
                                                 " PrbNmb," & vbCrLf & _
                                                 " :LogNo," & vbCrLf & _
                                                 " FileMngNmb," & vbCrLf & _
                                                 " FileNaiyo," & vbCrLf & _
                                                 " EntryNmb," & vbCrLf & _
                                                 " RegDT," & vbCrLf & _
                                                 " RegGrpCD," & vbCrLf & _
                                                 " RegID," & vbCrLf & _
                                                 " UpdateDT," & vbCrLf & _
                                                 " UpGrpCD," & vbCrLf & _
                                                 " UpdateID" & vbCrLf & _
                                                 " FROM problem_file_tb" & vbCrLf & _
                                                 " WHERE PrbNmb = :PrbNmb" & vbCrLf & _
                                                 " )"

    '[SELECT]新規ログNo（会議用）取得SQL
    Private strSelectNewMeetingRirekiNoSql As String = "SELECT " & vbCrLf & _
                                                       " COALESCE(MAX(ml.LogNo),0)+1 AS LogNo" & vbCrLf & _
                                                       " FROM meeting_ltb AS ml" & vbCrLf & _
                                                       " WHERE ML.MeetingNmb = :MeetingNmb"

    '[INSERT]会議情報ログ新規登録SQL
    Private strInsertMeetingLSql As String = "INSERT INTO meeting_ltb (" & vbCrLf & _
                                             " MeetingNmb," & vbCrLf & _
                                             " LogNo," & vbCrLf & _
                                             " YoteiSTDT," & vbCrLf & _
                                             " YoteiENDDT," & vbCrLf & _
                                             " JisiSTDT," & vbCrLf & _
                                             " JisiENDDT," & vbCrLf & _
                                             " Title," & vbCrLf & _
                                             " Proceedings," & vbCrLf & _
                                             " HostGrpCD," & vbCrLf & _
                                             " HostID," & vbCrLf & _
                                             " HostNM," & vbCrLf & _
                                             " TitleAimai," & vbCrLf & _
                                             " HostIDAimai," & vbCrLf & _
                                             " HostNMAimai," & vbCrLf & _
                                             " RegDT," & vbCrLf & _
                                             " RegGrpCD," & vbCrLf & _
                                             " RegID," & vbCrLf & _
                                             " UpdateDT," & vbCrLf & _
                                             " UpGrpCD," & vbCrLf & _
                                             " UpdateID" & vbCrLf & _
                                             " )" & vbCrLf & _
                                             " SELECT" & vbCrLf & _
                                             " mt.MeetingNmb," & vbCrLf & _
                                             " :LogNo," & vbCrLf & _
                                             " mt.YoteiSTDT," & vbCrLf & _
                                             " mt.YoteiENDDT," & vbCrLf & _
                                             " mt.JisiSTDT," & vbCrLf & _
                                             " mt.JisiENDDT," & vbCrLf & _
                                             " mt.Title," & vbCrLf & _
                                             " mt.Proceedings," & vbCrLf & _
                                             " mt.HostGrpCD," & vbCrLf & _
                                             " mt.HostID," & vbCrLf & _
                                             " mt.HostNM," & vbCrLf & _
                                             " mt.TitleAimai," & vbCrLf & _
                                             " mt.HostIDAimai," & vbCrLf & _
                                             " mt.HostNMAimai," & vbCrLf & _
                                             " mt.RegDT," & vbCrLf & _
                                             " mt.RegGrpCD," & vbCrLf & _
                                             " mt.RegID," & vbCrLf & _
                                             " mt.UpdateDT," & vbCrLf & _
                                             " mt.UpGrpCD," & vbCrLf & _
                                             " mt.UpdateID" & vbCrLf & _
                                             " FROM meeting_tb AS mt" & vbCrLf & _
                                             " WHERE mt.MeetingNmb = :MeetingNmb"

    '[INSERT]会議結果情報ログ登録SQL 
    Private strInsertMtgResultLSql As String = "INSERT INTO meeting_result_ltb (" & vbCrLf & _
                                                   " SELECT" & vbCrLf & _
                                                   " MeetingNmb," & vbCrLf & _
                                                   " :LogNo," & vbCrLf & _
                                                   " ProcessKbn," & vbCrLf & _
                                                   " ProcessNmb," & vbCrLf & _
                                                   " :ProcessLogNo," & vbCrLf & _
                                                   " ResultKbn," & vbCrLf & _
                                                   " EntryNmb," & vbCrLf & _
                                                   " RegDT," & vbCrLf & _
                                                   " RegGrpCD," & vbCrLf & _
                                                   " RegID," & vbCrLf & _
                                                   " UpdateDT," & vbCrLf & _
                                                   " UpGrpCD," & vbCrLf & _
                                                   " UpdateID" & vbCrLf & _
                                                   " FROM meeting_result_tb" & vbCrLf & _
                                                   " WHERE ProcessNmb = :ProcessNmb" & vbCrLf & _
                                                   " AND ProcessKbn = :ProcessKbn" & vbCrLf & _
                                                   " AND MeetingNmb = :MeetingNmb" & vbCrLf & _
                                                   " )"

    '[INSERT]会議出席者情報ログ新規登録SQL
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

    '[INSERT]会議関連ファイル情報ログ新規登録SQL
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

    '[SELECT]システム日付取得SQL
    Private strSelectSysDateSql As String = "SELECT" & vbCrLf & _
                                            " Now() AS SysDate"

    '[UPDATE]問題共通情報更新SQL
    Private strUpdateProblemInfoSql As String = "UPDATE problem_info_tb SET" & vbCrLf & _
                                                " ProcessStateCD = :ProcessStateCD," & vbCrLf & _
                                                " PrbCaseCD = :PrbCaseCD," & vbCrLf & _
                                                " KaisiDT = :KaisiDT," & vbCrLf & _
                                                " KanryoDT = :KanryoDT," & vbCrLf & _
                                                " Title = :Title," & vbCrLf & _
                                                " Naiyo = :Naiyo," & vbCrLf & _
                                                " Taisyo = :Taisyo," & vbCrLf & _
                                                " SystemNmb = :SystemNmb," & vbCrLf & _
                                                " ApproverID = :ApproverID," & vbCrLf & _
                                                " ApproverNM = :ApproverNM," & vbCrLf & _
                                                " RecorderID = :RecorderID," & vbCrLf & _
                                                " RecorderNM = :RecorderNM," & vbCrLf & _
                                                " TantoGrpCD = :TantoGrpCD," & vbCrLf & _
                                                " PrbTantoID = :PrbTantoID," & vbCrLf & _
                                                " PrbTantoNM = :PrbTantoNM," & vbCrLf & _
                                                " BIko1 = :BIko1," & vbCrLf & _
                                                " Biko2 = :Biko2," & vbCrLf & _
                                                " Biko3 = :Biko3," & vbCrLf & _
                                                " Biko4 = :Biko4," & vbCrLf & _
                                                " Biko5 = :Biko5," & vbCrLf & _
                                                " FreeFlg1 = :FreeFlg1," & vbCrLf & _
                                                " FreeFlg2 = :FreeFlg2," & vbCrLf & _
                                                " FreeFlg3 = :FreeFlg3," & vbCrLf & _
                                                " FreeFlg4 = :FreeFlg4," & vbCrLf & _
                                                " FreeFlg5 = :FreeFlg5," & vbCrLf & _
                                                " TitleAimai = :TitleAimai," & vbCrLf & _
                                                " NaiyoAimai = :NaiyoAimai," & vbCrLf & _
                                                " TaisyoAimai = :TaisyoAimai," & vbCrLf & _
                                                " BikoAimai = :BikoAimai," & vbCrLf & _
                                                " PrbTantIDAimai = :PrbTantIDAimai," & vbCrLf & _
                                                " PrbTantNMAimai = :PrbTantNMAimai," & vbCrLf & _
                                                " UpdateDT = :UpdateDT," & vbCrLf & _
                                                " UpGrpCD = :UpGrpCD," & vbCrLf & _
                                                " UpdateID = :UpdateID" & vbCrLf & _
                                                " WHERE PrbNmb = :PrbNmb"

    '[DELETE]問題作業履歴削除SQL
    'Private strDeleteProblemWkRirekiSql As String = "DELETE FROM problem_wk_rireki_tb" & vbCrLf & _
    '                                                " WHERE PrbNmb = :PrbNmb"


    '[DELETE]問題作業担当削除SQL
    'Private strDeleteProblemWkTantoSql As String = "DELETE FROM problem_wk_tanto_tb" & vbCrLf & _
    '                                               " WHERE PrbNmb = :PrbNmb"
    Private strDeleteProblemWkTantoSql As String = "DELETE FROM problem_wk_tanto_tb " & vbCrLf & _
                                                   "WHERE PrbNmb=:PrbNmb " & vbCrLf & _
                                                   "AND workrirekinmb=:workrirekinmb "

    '[DELETE]問題対応関係削除SQL
    Private strDeleteProblemKankeiSql As String = "DELETE FROM problem_kankei_tb" & vbCrLf & _
                                                  " WHERE PrbNmb = :PrbNmb"

    '[DELETE]問題CYSPR削除SQL problem_cyspr_tb
    Private strDeleteProblemCysprSql As String = "DELETE FROM problem_cyspr_tb" & vbCrLf & _
                                                 " WHERE PrbNmb = :PrbNmb"

    '[DELETE]会議結果情報削除SQL
    Private strDeleteMeetingResultSql As String = "DELETE FROM meeting_result_tb" & vbCrLf & _
                                                  " WHERE ProcessNmb = :ProcessNmb"

    '[INSERT]会議結果情報登録SQL
    Private strInsertMeetingResultSql As String = "INSERT INTO meeting_result_tb (" & vbCrLf & _
                                                  " MeetingNmb," & vbCrLf & _
                                                  " ProcessKbn," & vbCrLf & _
                                                  " ProcessNmb," & vbCrLf & _
                                                  " ResultKbn," & vbCrLf & _
                                                  " EntryNmb," & vbCrLf & _
                                                  " RegDT," & vbCrLf & _
                                                  " RegGrpCD," & vbCrLf & _
                                                  " RegID," & vbCrLf & _
                                                  " UpdateDT," & vbCrLf & _
                                                  " UpGrpCD," & vbCrLf & _
                                                  " UpdateID" & vbCrLf & _
                                                  " ) VALUES (" & vbCrLf & _
                                                  " :MeetingNmb," & vbCrLf & _
                                                  " :ProcessKbn," & vbCrLf & _
                                                  " :ProcessNmb," & vbCrLf & _
                                                  " :ResultKbn," & vbCrLf & _
                                                  " (SELECT COALESCE(MAX(EntryNmb),0)+1 FROM meeting_result_tb WHERE MeetingNmb = :MeetingNmb)," & vbCrLf & _
                                                  " :RegDT," & vbCrLf & _
                                                  " :RegGrpCD," & vbCrLf & _
                                                  " :RegID," & vbCrLf & _
                                                  " :UpdateDT," & vbCrLf & _
                                                  " :UpGrpCD," & vbCrLf & _
                                                  " :UpdateID" & vbCrLf & _
                                                  " )"

    '問題共通情報ロックテーブル登録（INSERT）用SQL
    Private strInsertPrbLockSql As String = "INSERT INTO problem_info_lock_tb" & vbCrLf & _
                                    "(PrbNmb, EdiTime, EdiGrpCD, EdiID)" & vbCrLf & _
                                    "SELECT" & vbCrLf & _
                                    " pit.PrbNmb, Now(), :EdiGrpCD, :EdiID" & vbCrLf & _
                                    " FROM problem_info_tb AS pit" & vbCrLf & _
                                    " WHERE" & vbCrLf & _
                                    " pit.PrbNmb = :PrbNmb"

    '問題共通情報ロックテーブル取得用SQL
    Private strSelectPrbInfoSql As String = "SELECT" & vbCrLf & _
                                       "   NULL AS EdiTime" & vbCrLf & _
                                       "  ,'' AS EdiGrpCD" & vbCrLf & _
                                       "  ,'' AS EdiID" & vbCrLf & _
                                       "  ,'' AS EdiGroupNM" & vbCrLf & _
                                       "  ,'' AS EdiUsrNM" & vbCrLf & _
                                       "  ,Now() AS SysTime" & vbCrLf & _
                                       "UNION ALL" & vbCrLf & _
                                       "SELECT" & vbCrLf & _
                                        "  pilt.EdiTime" & vbCrLf & _
                                        " ,pilt.EdiGrpCD" & vbCrLf & _
                                        " ,pilt.EdiID" & vbCrLf & _
                                        " ,gm.GroupNM" & vbCrLf & _
                                        " ,hm.HBKUsrNM" & vbCrLf & _
                                        " ,NULL" & vbCrLf & _
                                        "FROM problem_info_lock_tb AS pilt" & vbCrLf & _
                                        "LEFT JOIN grp_mtb AS gm ON pilt.EdiGrpCD = gm.GroupCD" & vbCrLf & _
                                        "LEFT JOIN hbkusr_mtb AS hm ON pilt.EdiID = hm.HBKUsrID" & vbCrLf & _
                                        "WHERE PrbNmb = :PrbNmb"

    '問題共通情報ロック解除（DELETE）用SQL
    Private strDeletePrbLockSql As String = "DELETE FROM problem_info_lock_tb WHERE PrbNmb = :PrbNmb"


    '[SELECT]担当履歴取得SQL
    Private strSelectTantoRirekiSql As String = "SELECT " & vbCrLf & _
                                              " ct.tantorirekinmb " & vbCrLf & _
                                              ",ct.tantogrpcd " & vbCrLf & _
                                              ",ct.tantogrpnm " & vbCrLf & _
                                              ",ct.prbtantoid " & vbCrLf & _
                                              ",ct.prbtantonm " & vbCrLf & _
                                              "FROM problem_tanto_rireki_tb ct " & vbCrLf & _
                                              "WHERE ct.PrbNmb = :PrbNmb " & vbCrLf & _
                                              "ORDER BY ct.tantorirekinmb DESC"

    '[INSERT]担当履歴SQL
    Private strInsertTantoRirekiSql As String = "INSERT INTO problem_tanto_rireki_tb (" & vbCrLf & _
                                             " PrbNmb " & vbCrLf & _
                                             ",tantorirekinmb " & vbCrLf & _
                                             ",tantogrpcd " & vbCrLf & _
                                             ",tantogrpnm " & vbCrLf & _
                                             ",prbtantoid " & vbCrLf & _
                                             ",prbtantonm " & vbCrLf & _
                                             ",RegDT " & vbCrLf & _
                                             ",RegGrpCD " & vbCrLf & _
                                             ",RegID " & vbCrLf & _
                                             ",UpdateDT " & vbCrLf & _
                                             ",UpGrpCD " & vbCrLf & _
                                             ",UpdateID " & vbCrLf & _
                                             ") VALUES (" & vbCrLf & _
                                             " :PrbNmb " & vbCrLf & _
                                             ",(SELECT COALESCE(MAX(tantorirekinmb),0)+1 FROM problem_tanto_rireki_tb WHERE PrbNmb=:PrbNmb) " & vbCrLf & _
                                             ",:tantogrpcd " & vbCrLf & _
                                             ",:tantogrpnm " & vbCrLf & _
                                             ",:prbtantoid " & vbCrLf & _
                                             ",:prbtantonm " & vbCrLf & _
                                             ",:RegDT " & vbCrLf & _
                                             ",:RegGrpCD " & vbCrLf & _
                                             ",:RegID " & vbCrLf & _
                                             ",:UpdateDT " & vbCrLf & _
                                             ",:UpGrpCD " & vbCrLf & _
                                             ",:UpdateID " & vbCrLf & _
                                             ") "

    ''' <summary>
    ''' プロセスステータスマスタ取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ステータスコンボボックス取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectProcessStateMst(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectProcessStateMstSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))   'プロセス区分
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("ProcessKbn").Value = PROCESS_TYPE_QUESTION                     'プロセス区分：問題
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
    ''' 問題発生原因マスタ取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>発生原因コンボボックス取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectProblemCaseMst(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectProblemCaseMstSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '[add] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 START
            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))         '問題番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb                      '問題番号
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
    ''' グループマスタ取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>担当グループコンボボックス取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectTantoGrpMst(ByRef Adapter As NpgsqlDataAdapter, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectTantoGrpMstSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '[add] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 START
            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))         '問題番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb                      '問題番号
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
    ''' 作業ステータスマスタ取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>スプレッド内作業ステータスコンボボックス取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectWorkStateMst(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectWorkStateMstSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '[add] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 START
            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))         '問題番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb                      '問題番号
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
    ''' 対象システムデータ取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>対象システムコンボボックス取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectTargetSystemData(ByRef Adapter As NpgsqlDataAdapter, _
                                              ByVal Cn As NpgsqlConnection, _
                                              ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectTargetSystemSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CIStatusCD", NpgsqlTypes.NpgsqlDbType.Varchar))       '廃止済
                .Add(New NpgsqlParameter("cikbncd", NpgsqlTypes.NpgsqlDbType.Varchar))          'システム
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CIStatusCD").Value = CI_STATUS_SYSTEM_HAISHIZUMI                   '廃止済
                .Parameters("CIKbnCD").Value = CI_TYPE_SYSTEM                                   'システム
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
    ''' 担当者情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>担当情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/16 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectPrbTantoData(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectPrbTantoSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("HBKUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))         '担当ID
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("HBKUsrID").Value = dataHBKD0201.PropStrTantoIdForSearch            '担当ID
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
    ''' 問題共通情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>問題共通情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/16 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectProblemInfoData(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectProblemInfoSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))         '問題番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb                      '問題番号
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
    ''' 問題作業履歴取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>問題作業履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/16 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectProblemWkRirekiData(ByRef Adapter As NpgsqlDataAdapter, _
                                                 ByVal Cn As NpgsqlConnection, _
                                                 ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectProblemWkRirekiSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))         '問題番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb                      '問題番号
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
    ''' 問題作業担当取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>問題作業担当取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/16 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectProblemWkTantoData(ByRef Adapter As NpgsqlDataAdapter, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectProblemWkTantoSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))         '問題番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb                      '問題番号
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
    ''' 問題対応関係取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>問題対応関係取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/16 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectProblemKankeiData(ByRef Adapter As NpgsqlDataAdapter, _
                                               ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectProblemKankeiSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))   '問題番号
                .Add(New NpgsqlParameter("KbnGrp", NpgsqlTypes.NpgsqlDbType.Varchar))   '区分：グループ
                .Add(New NpgsqlParameter("KbnUsr", NpgsqlTypes.NpgsqlDbType.Varchar))   '区分：ユーザー
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb                '問題番号
                .Parameters("KbnGrp").Value = KBN_GROUP                                 '区分：グループ
                .Parameters("KbnUsr").Value = KBN_USER                                  '区分：ユーザー
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
    ''' プロセスリンク情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>プロセスリンク情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/16 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectProcessLinkData(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectProcessLinkSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("Nmb", NpgsqlTypes.NpgsqlDbType.Integer))                  '問題番号
                .Add(New NpgsqlParameter("LinkMotoProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))   'リンク元プロセス区分
                .Add(New NpgsqlParameter("Kbn_Incident", NpgsqlTypes.NpgsqlDbType.Varchar))         'プロセス区分：インシデント
                .Add(New NpgsqlParameter("Kbn_Incident_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))     'プロセス区分名略称：インシデント
                .Add(New NpgsqlParameter("Kbn_Question", NpgsqlTypes.NpgsqlDbType.Varchar))         'プロセス区分：問題
                .Add(New NpgsqlParameter("Kbn_Question_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))     'プロセス区分名略称：問題
                .Add(New NpgsqlParameter("Kbn_Change", NpgsqlTypes.NpgsqlDbType.Varchar))           'プロセス区分：変更
                .Add(New NpgsqlParameter("Kbn_Change_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))       'プロセス区分名略称：変更
                .Add(New NpgsqlParameter("Kbn_Release", NpgsqlTypes.NpgsqlDbType.Varchar))          'プロセス区分：リリース
                .Add(New NpgsqlParameter("Kbn_Release_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))      'プロセス区分名略称：リリース
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                'If dataHBKD0201.PropBlnFromCheckFlg = False Then
                '    'インシデント登録画面外からの呼出時
                .Parameters("Nmb").Value = dataHBKD0201.PropIntPrbNmb                        '問題番号
                'Else
                '    'インシデント登録画面からの呼出時
                '    .Parameters("Nmb").Value = dataHBKD0201.PropIntIncNmb                        'インシデント番号
                'End If
                .Parameters("LinkMotoProcessKbn").Value = PROCESS_TYPE_QUESTION
                .Parameters("Kbn_Incident").Value = PROCESS_TYPE_INCIDENT                        'プロセス区分：インシデント
                .Parameters("Kbn_Incident_NMR").Value = PROCESS_TYPE_INCIDENT_NAME_R             'プロセス区分名略称：インシデント
                .Parameters("Kbn_Question").Value = PROCESS_TYPE_QUESTION                        'プロセス区分：問題
                .Parameters("Kbn_Question_NMR").Value = PROCESS_TYPE_QUESTION_NAME_R             'プロセス区分名略称：問題
                .Parameters("Kbn_Change").Value = PROCESS_TYPE_CHANGE                            'プロセス区分：変更
                .Parameters("Kbn_Change_NMR").Value = PROCESS_TYPE_CHANGE_NAME_R                 'プロセス区分名略称：変更
                .Parameters("Kbn_Release").Value = PROCESS_TYPE_RELEASE                          'プロセス区分：リリース
                .Parameters("Kbn_Release_NMR").Value = PROCESS_TYPE_RELEASE_NAME_R               'プロセス区分名略称：リリース
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
    ''' 問題CYSPR情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>問題CYSPR情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/16 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectProblemCysprData(ByRef Adapter As NpgsqlDataAdapter, _
                                              ByVal Cn As NpgsqlConnection, _
                                              ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectProblemCysprSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))            '問題番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb                         '問題番号
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
    ''' 関連ファイル情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>関連ファイル情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/16 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectProblemFileData(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectProblemFileSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))            '問題番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb                         '問題番号
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
    ''' 会議情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/16 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectMeetingSql(ByRef Adapter As NpgsqlDataAdapter, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKD0201 As DataHBKD0201) As Boolean

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
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))       'プロセス区分
                .Add(New NpgsqlParameter("ProcessNmb", NpgsqlTypes.NpgsqlDbType.Integer))       'プロセス番号(問題番号)
                .Add(New NpgsqlParameter("Kbn_NO", NpgsqlTypes.NpgsqlDbType.Varchar))           '結果区分：未入力
                .Add(New NpgsqlParameter("Kbn_NO_NM", NpgsqlTypes.NpgsqlDbType.Varchar))        '結果区分名略称：未入力
                .Add(New NpgsqlParameter("Kbn_OK", NpgsqlTypes.NpgsqlDbType.Varchar))           '結果区分：承認
                .Add(New NpgsqlParameter("Kbn_OK_NM", NpgsqlTypes.NpgsqlDbType.Varchar))        '結果区分名略称：承認
                .Add(New NpgsqlParameter("Kbn_NG", NpgsqlTypes.NpgsqlDbType.Varchar))           '結果区分：却下
                .Add(New NpgsqlParameter("Kbn_NG_NM", NpgsqlTypes.NpgsqlDbType.Varchar))        '結果区分名略称：却下
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("ProcessKbn").Value = PROCESS_TYPE_QUESTION                         'プロセス区分
                .Parameters("ProcessNmb").Value = dataHBKD0201.PropIntPrbNmb                    'プロセス番号(問題番号)
                .Parameters("Kbn_NO").Value = SELECT_RESULTKBN_NO                               '結果区分：未入力
                .Parameters("Kbn_NO_NM").Value = SELECT_RESULTKBNNM_NO                          '結果区分名略称：未入力
                .Parameters("Kbn_OK").Value = SELECT_RESULTKBN_OK                               '結果区分：承認
                .Parameters("Kbn_OK_NM").Value = SELECT_RESULTKBNNM_OK                          '結果区分名略称：承認
                .Parameters("Kbn_NG").Value = SELECT_RESULTKBN_NG                               '結果区分：却下
                .Parameters("Kbn_NG_NM").Value = SELECT_RESULTKBNNM_NG                          '結果区分名略称：却下
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
    ''' 対応関係者チェック用区分Gデータ取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>対応関係者チェック用区分Gデータ取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/16 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCheckPrbKankeiGData(ByRef Adapter As NpgsqlDataAdapter, _
                                                 ByVal Cn As NpgsqlConnection, _
                                                 ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectCheckPrbKankeiGSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))           '問題番号
                .Add(New NpgsqlParameter("KbnGrp", NpgsqlTypes.NpgsqlDbType.Varchar))           '区分：グループ
                .Add(New NpgsqlParameter("GrpID", NpgsqlTypes.NpgsqlDbType.Varchar))            'グループID
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb
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
    ''' 対応関係者チェック用区分Uデータ取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>対応関係者チェック用区分Uデータ取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/16 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCheckPrbKankeiUData(ByRef Adapter As NpgsqlDataAdapter, _
                                                 ByVal Cn As NpgsqlConnection, _
                                                 ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectCheckPrbKankeiUSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))           '問題番号
                .Add(New NpgsqlParameter("KbnUsr", NpgsqlTypes.NpgsqlDbType.Varchar))           '区分：ユーザー
                .Add(New NpgsqlParameter("UsrID", NpgsqlTypes.NpgsqlDbType.Varchar))            'ユーザーID
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb
                .Parameters("KbnUsr").Value = KBN_GROUP
                .Parameters("UsrID").Value = PropWorkGroupCD
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
    ''' 対処承認者情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>対処承者報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectPrbApproverData(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectPrbApproverSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("EndUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))         '対処承認者ID
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("EndUsrID").Value = dataHBKD0201.PropStrTSyouninSyaIdForSearch      '対処承認者ID
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
    ''' 承認記録者情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>承認記録者情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectPrbRecorderData(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectPrbRecorderSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("HBKUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))         '承認記録者ID
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("HBKUsrID").Value = dataHBKD0201.PropStrRecorderIdForSearch         '承認記録者ID
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
    ''' 【共通】対応関係者取得：対象システム
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>INC共通情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetChkKankeiSysData(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKD0201 As DataHBKD0201) As Boolean

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
                .Add(New NpgsqlParameter("SystemNmb", NpgsqlTypes.NpgsqlDbType.Integer))                                 '対象システム
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("SystemNmb").Value = dataHBKD0201.PropCmbTargetSystem.PropCmbColumns.SelectedValue        '対象システム
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
    ''' 【新規登録モード】新規問題番号、サーバー日付取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規問題番号、サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/21 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewPrbNmbAndSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                    ByVal Cn As NpgsqlConnection, _
                                                    ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = GET_NEXTVAL_MONDAI_NO

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
    ''' 【新規登録モード】問題共通情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>問題共通情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/21 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertProblemInfoSql(ByRef Cmd As NpgsqlCommand, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Dim strBikoAimai As String = ""         'フリーテキスト（あいまい）

        Try

            'SQL文(INSERT)
            strSQL = strInsertProblemInfoSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))           '問題番号
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))       'プロセス区分
                .Add(New NpgsqlParameter("ProcessStateCD", NpgsqlTypes.NpgsqlDbType.Varchar))   'プロセスステータスCD
                .Add(New NpgsqlParameter("PrbCaseCD", NpgsqlTypes.NpgsqlDbType.Varchar))        '問題発生原因CD
                .Add(New NpgsqlParameter("KaisiDT", NpgsqlTypes.NpgsqlDbType.Timestamp))        '開始日時
                .Add(New NpgsqlParameter("KanryoDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '完了日時
                .Add(New NpgsqlParameter("Title", NpgsqlTypes.NpgsqlDbType.Varchar))            'タイトル
                .Add(New NpgsqlParameter("Naiyo", NpgsqlTypes.NpgsqlDbType.Varchar))            '内容
                .Add(New NpgsqlParameter("Taisyo", NpgsqlTypes.NpgsqlDbType.Varchar))           '対処
                .Add(New NpgsqlParameter("SystemNmb", NpgsqlTypes.NpgsqlDbType.Integer))        '対象システム番号
                .Add(New NpgsqlParameter("ApproverID", NpgsqlTypes.NpgsqlDbType.Varchar))       '対処承認者ID
                .Add(New NpgsqlParameter("ApproverNM", NpgsqlTypes.NpgsqlDbType.Varchar))       '対処承認者氏名
                .Add(New NpgsqlParameter("RecorderID", NpgsqlTypes.NpgsqlDbType.Varchar))       '承認記録者ID
                .Add(New NpgsqlParameter("RecorderNM", NpgsqlTypes.NpgsqlDbType.Varchar))       '承認記録者氏名
                .Add(New NpgsqlParameter("TantoGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))       '担当グループCD
                .Add(New NpgsqlParameter("PrbTantoID", NpgsqlTypes.NpgsqlDbType.Varchar))       '問題担当者ID
                .Add(New NpgsqlParameter("PrbTantoNM", NpgsqlTypes.NpgsqlDbType.Varchar))       '問題担当者氏名
                .Add(New NpgsqlParameter("BIko1", NpgsqlTypes.NpgsqlDbType.Varchar))            'フリーテキスト
                .Add(New NpgsqlParameter("Biko2", NpgsqlTypes.NpgsqlDbType.Varchar))            'フリーテキスト
                .Add(New NpgsqlParameter("Biko3", NpgsqlTypes.NpgsqlDbType.Varchar))            'フリーテキスト
                .Add(New NpgsqlParameter("Biko4", NpgsqlTypes.NpgsqlDbType.Varchar))            'フリーテキスト
                .Add(New NpgsqlParameter("Biko5", NpgsqlTypes.NpgsqlDbType.Varchar))            'フリーテキスト
                .Add(New NpgsqlParameter("FreeFlg1", NpgsqlTypes.NpgsqlDbType.Varchar))         'フリーフラグ
                .Add(New NpgsqlParameter("FreeFlg2", NpgsqlTypes.NpgsqlDbType.Varchar))         'フリーフラグ
                .Add(New NpgsqlParameter("FreeFlg3", NpgsqlTypes.NpgsqlDbType.Varchar))         'フリーフラグ
                .Add(New NpgsqlParameter("FreeFlg4", NpgsqlTypes.NpgsqlDbType.Varchar))         'フリーフラグ
                .Add(New NpgsqlParameter("FreeFlg5", NpgsqlTypes.NpgsqlDbType.Varchar))         'フリーフラグ
                .Add(New NpgsqlParameter("TitleAimai", NpgsqlTypes.NpgsqlDbType.Varchar))       'タイトル(あいまい)
                .Add(New NpgsqlParameter("NaiyoAimai", NpgsqlTypes.NpgsqlDbType.Varchar))       '内容(あいまい)
                .Add(New NpgsqlParameter("TaisyoAimai", NpgsqlTypes.NpgsqlDbType.Varchar))      '対処(あいまい)
                .Add(New NpgsqlParameter("BikoAimai", NpgsqlTypes.NpgsqlDbType.Varchar))        'フリーテキスト(あいまい)
                .Add(New NpgsqlParameter("PrbTantIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))   '問題担当者ID(あいまい)
                .Add(New NpgsqlParameter("PrbTantNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))   '問題担当者氏名(あいまい)
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))          '登録日時()
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))         '登録者グループCD()
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))            '登録者ID()
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '最終更新日時()
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD()
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID()
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb           '問題番号
                .Parameters("ProcessKbn").Value = PROCESS_TYPE_QUESTION       'プロセス区分
                .Parameters("ProcessStateCD").Value = dataHBKD0201.PropCmbStatus.SelectedValue   'プロセスステータスCD
                .Parameters("PrbCaseCD").Value = dataHBKD0201.PropCmbPrbCase.SelectedValue        '問題発生原因CD
                '開始日時
                If dataHBKD0201.PropDtpStartDT.txtDate.Text.Equals("") Then
                    .Parameters("KaisiDT").Value = Nothing
                Else
                    .Parameters("KaisiDT").Value = _
                        CDate(dataHBKD0201.PropDtpStartDT.txtDate.Text & " " & dataHBKD0201.PropTxtStartDT_HM.PropTxtTime.Text)
                End If
                '完了日時
                If dataHBKD0201.PropDtpKanryoDT.txtDate.Text.Equals("") Then
                    'ステータスが完了ならばシステム日付を設定する
                    If dataHBKD0201.PropCmbStatus.SelectedValue = PROCESS_STATUS_QUESTION_KANRYOH Then
                        .Parameters("KanryoDT").Value = dataHBKD0201.PropDtmSysDate
                    Else
                        .Parameters("KanryoDT").Value = Nothing
                    End If
                Else
                    .Parameters("KanryoDT").Value = _
                        CDate(dataHBKD0201.PropDtpKanryoDT.txtDate.Text & " " & dataHBKD0201.PropTxtKanryoDT_HM.PropTxtTime.Text)
                End If
                .Parameters("Title").Value = dataHBKD0201.PropTxtTitle.Text                         'タイトル
                .Parameters("Naiyo").Value = dataHBKD0201.PropTxtNaiyo.Text                         '内容
                .Parameters("Taisyo").Value = dataHBKD0201.PropTxtTaisyo.Text                       '対処
                .Parameters("SystemNmb").Value = dataHBKD0201.PropCmbTargetSystem.PropCmbColumns.SelectedValue      '対象システム番号
                .Parameters("ApproverID").Value = dataHBKD0201.PropTxtApproverID.Text               '対処承認者ID
                .Parameters("ApproverNM").Value = dataHBKD0201.PropTxtApproverNM.Text               '対処承認者氏名
                .Parameters("RecorderID").Value = dataHBKD0201.PropTxtRecorderID.Text               '承認記録者ID
                .Parameters("RecorderNM").Value = dataHBKD0201.PropTxtRecorderNM.Text               '承認記録者氏名
                .Parameters("TantoGrpCD").Value = dataHBKD0201.PropCmbTantoGrp.SelectedValue        '担当グループCD
                .Parameters("PrbTantoID").Value = dataHBKD0201.PropTxtPrbTantoID.Text               '問題担当者ID
                .Parameters("PrbTantoNM").Value = dataHBKD0201.PropTxtPrbTantoNM.Text               '問題担当者氏名
                .Parameters("BIko1").Value = dataHBKD0201.PropTxtFreeText1.Text                     'フリーテキスト1
                .Parameters("Biko2").Value = dataHBKD0201.PropTxtFreeText2.Text                     'フリーテキスト2
                .Parameters("Biko3").Value = dataHBKD0201.PropTxtFreeText3.Text                     'フリーテキスト3
                .Parameters("Biko4").Value = dataHBKD0201.PropTxtFreeText4.Text                     'フリーテキスト4
                .Parameters("Biko5").Value = dataHBKD0201.PropTxtFreeText5.Text                     'フリーテキスト5
                'フリーフラグ1
                If dataHBKD0201.PropChkFreeFlg1.Checked = True Then
                    .Parameters("FreeFlg1").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg1").Value = FREE_FLG_OFF
                End If
                'フリーフラグ2
                If dataHBKD0201.PropChkFreeFlg2.Checked = True Then
                    .Parameters("FreeFlg2").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg2").Value = FREE_FLG_OFF
                End If
                'フリーフラグ3
                If dataHBKD0201.PropChkFreeFlg3.Checked = True Then
                    .Parameters("FreeFlg3").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg3").Value = FREE_FLG_OFF
                End If
                'フリーフラグ4
                If dataHBKD0201.PropChkFreeFlg4.Checked = True Then
                    .Parameters("FreeFlg4").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg4").Value = FREE_FLG_OFF
                End If
                'フリーフラグ5
                If dataHBKD0201.PropChkFreeFlg5.Checked = True Then
                    .Parameters("FreeFlg5").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg5").Value = FREE_FLG_OFF
                End If
                .Parameters("TitleAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKD0201.PropTxtTitle.Text)              'タイトル(あいまい)
                .Parameters("NaiyoAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKD0201.PropTxtNaiyo.Text)              '内容(あいまい)
                .Parameters("TaisyoAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKD0201.PropTxtTaisyo.Text)            '対処(あいまい)
                .Parameters("BikoAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKD0201.PropTxtFreeText1.Text) & _
                                                 commonLogicHBK.ChangeStringForSearch(dataHBKD0201.PropTxtFreeText2.Text) & _
                                                 commonLogicHBK.ChangeStringForSearch(dataHBKD0201.PropTxtFreeText3.Text) & _
                                                 commonLogicHBK.ChangeStringForSearch(dataHBKD0201.PropTxtFreeText4.Text) & _
                                                 commonLogicHBK.ChangeStringForSearch(dataHBKD0201.PropTxtFreeText5.Text)           'フリーテキスト(あいまい)
                .Parameters("PrbTantIDAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKD0201.PropTxtPrbTantoID.Text)     '問題担当者ID(あいまい)
                .Parameters("PrbTantNMAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKD0201.PropTxtPrbTantoNM.Text)     '問題担当者氏名(あいまい)
                .Parameters("RegDT").Value = dataHBKD0201.PropDtmSysDate            '登録日時()
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                     '登録者グループCD()
                .Parameters("RegID").Value = PropUserId                             '登録者ID()
                .Parameters("UpdateDT").Value = dataHBKD0201.PropDtmSysDate         '最終更新日時()
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                      '最終更新者グループCD()
                .Parameters("UpdateID").Value = PropUserId                          '最終更新者ID()
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
    ''' 【新規登録／編集／作業履歴モード】問題作業履歴情報 新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>問題作業履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertProblemWkRirekiSql(ByRef Cmd As NpgsqlCommand, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Dim strworknaiyoaimai As String = ""   '作業内容(あいまい)

        Try

            'SQL文(INSERT)
            strSQL = strInsertProblemWkRirekiSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))         '問題作業履歴：問題番号
                '.Add(New NpgsqlParameter("WorkRirekiNmb", NpgsqlTypes.NpgsqlDbType.Integer))  '問題作業履歴：作業履歴番号
                .Add(New NpgsqlParameter("workstatecd", NpgsqlTypes.NpgsqlDbType.Varchar))    '問題作業履歴：作業ステータスCD
                .Add(New NpgsqlParameter("WorkNaiyo", NpgsqlTypes.NpgsqlDbType.Varchar))      '問題作業履歴：作業内容
                .Add(New NpgsqlParameter("WorkSceDT", NpgsqlTypes.NpgsqlDbType.Timestamp))    '問題作業履歴：作業予定日時
                .Add(New NpgsqlParameter("WorkStDT", NpgsqlTypes.NpgsqlDbType.Timestamp))     '問題作業履歴：作業開始日時
                .Add(New NpgsqlParameter("WorkEdDT", NpgsqlTypes.NpgsqlDbType.Timestamp))     '問題作業履歴：作業終了日時
                .Add(New NpgsqlParameter("SystemNmb", NpgsqlTypes.NpgsqlDbType.Integer))      '問題作業履歴：対象システム番号
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))        '問題作業履歴：登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))       '問題作業履歴：登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))          '問題作業履歴：登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))     '問題作業履歴：最終更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))        '問題作業履歴：最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))       '問題作業履歴：最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb                                '問題作業履歴：問題番号
                '.Parameters("WorkRirekiNmb").Value = dataHBKD0201.PropDrRegRow.Item("WorkRirekiNmb")    '問題作業履歴：作業履歴番号
                .Parameters("workstatecd").Value = dataHBKD0201.PropDrRegRow.Item("workstatecd")        '問題作業履歴：作業ステータスCD
                .Parameters("WorkNaiyo").Value = dataHBKD0201.PropDrRegRow.Item("WorkNaiyo")            '問題作業履歴：作業内容
                '問題作業履歴：作業予定日時
                If dataHBKD0201.PropDrRegRow.Item("WorkSceDT").ToString.Equals("") Then

                Else
                    .Parameters("WorkSceDT").Value = DateTime.Parse(dataHBKD0201.PropDrRegRow.Item("WorkSceDT"))
                End If
                '問題作業履歴：作業開始日時
                If dataHBKD0201.PropDrRegRow.Item("WorkStDT").ToString.Equals("") Then

                Else
                    .Parameters("WorkStDT").Value = DateTime.Parse(dataHBKD0201.PropDrRegRow.Item("WorkStDT"))
                End If
                '問題作業履歴：作業予定日時
                If dataHBKD0201.PropDrRegRow.Item("WorkEdDT").ToString.Equals("") Then

                Else
                    .Parameters("WorkEdDT").Value = DateTime.Parse(dataHBKD0201.PropDrRegRow.Item("WorkEdDT"))
                End If
                .Parameters("SystemNmb").Value = dataHBKD0201.PropDrRegRow.Item("SystemNmb") '問題作業履歴：対象システム番号
                .Parameters("RegDT").Value = dataHBKD0201.PropDtmSysDate                     '問題作業履歴：登録日時()
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                              '問題作業履歴：登録者グループCD()
                .Parameters("RegID").Value = PropUserId                                      '問題作業履歴：登録者ID()
                .Parameters("UpdateDT").Value = dataHBKD0201.PropDtmSysDate                  '問題作業履歴：最終更新日時()
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                               '問題作業履歴：最終更新者グループCD()
                .Parameters("UpdateID").Value = PropUserId                                   '問題作業履歴：最終更新者ID()
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
    ''' 【新規登録／編集／作業履歴モード】問題作業履歴情報 新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>問題作業履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateProblemWkRirekiSql(ByRef Cmd As NpgsqlCommand, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Dim strworknaiyoaimai As String = ""   '作業内容(あいまい)

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateProblemWkRirekiSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))         '問題作業履歴：問題番号
                .Add(New NpgsqlParameter("WorkRirekiNmb", NpgsqlTypes.NpgsqlDbType.Integer))  '問題作業履歴：作業履歴番号
                .Add(New NpgsqlParameter("workstatecd", NpgsqlTypes.NpgsqlDbType.Varchar))    '問題作業履歴：作業ステータスCD
                .Add(New NpgsqlParameter("WorkNaiyo", NpgsqlTypes.NpgsqlDbType.Varchar))      '問題作業履歴：作業内容
                .Add(New NpgsqlParameter("WorkSceDT", NpgsqlTypes.NpgsqlDbType.Timestamp))    '問題作業履歴：作業予定日時
                .Add(New NpgsqlParameter("WorkStDT", NpgsqlTypes.NpgsqlDbType.Timestamp))     '問題作業履歴：作業開始日時
                .Add(New NpgsqlParameter("WorkEdDT", NpgsqlTypes.NpgsqlDbType.Timestamp))     '問題作業履歴：作業終了日時
                .Add(New NpgsqlParameter("SystemNmb", NpgsqlTypes.NpgsqlDbType.Integer))      '問題作業履歴：対象システム番号
                '.Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))        '問題作業履歴：登録日時
                '.Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))       '問題作業履歴：登録者グループCD
                '.Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))          '問題作業履歴：登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))     '問題作業履歴：最終更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))        '問題作業履歴：最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))       '問題作業履歴：最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb                                '問題作業履歴：問題番号
                .Parameters("WorkRirekiNmb").Value = dataHBKD0201.PropDrRegRow.Item("WorkRirekiNmb")    '問題作業履歴：作業履歴番号
                .Parameters("workstatecd").Value = dataHBKD0201.PropDrRegRow.Item("workstatecd")        '問題作業履歴：作業ステータスCD
                .Parameters("WorkNaiyo").Value = dataHBKD0201.PropDrRegRow.Item("WorkNaiyo")            '問題作業履歴：作業内容
                '問題作業履歴：作業予定日時
                If dataHBKD0201.PropDrRegRow.Item("WorkSceDT").ToString.Equals("") Then

                Else
                    .Parameters("WorkSceDT").Value = DateTime.Parse(dataHBKD0201.PropDrRegRow.Item("WorkSceDT"))
                End If
                '問題作業履歴：作業開始日時
                If dataHBKD0201.PropDrRegRow.Item("WorkStDT").ToString.Equals("") Then

                Else
                    .Parameters("WorkStDT").Value = DateTime.Parse(dataHBKD0201.PropDrRegRow.Item("WorkStDT"))
                End If
                '問題作業履歴：作業予定日時
                If dataHBKD0201.PropDrRegRow.Item("WorkEdDT").ToString.Equals("") Then

                Else
                    .Parameters("WorkEdDT").Value = DateTime.Parse(dataHBKD0201.PropDrRegRow.Item("WorkEdDT"))
                End If
                .Parameters("SystemNmb").Value = dataHBKD0201.PropDrRegRow.Item("SystemNmb") '問題作業履歴：対象システム番号
                '.Parameters("RegDT").Value = dataHBKD0201.PropDtmSysDate                     '問題作業履歴：登録日時()
                '.Parameters("RegGrpCD").Value = PropWorkGroupCD                              '問題作業履歴：登録者グループCD()
                '.Parameters("RegID").Value = PropUserId                                      '問題作業履歴：登録者ID()
                .Parameters("UpdateDT").Value = dataHBKD0201.PropDtmSysDate                  '問題作業履歴：最終更新日時()
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                               '問題作業履歴：最終更新者グループCD()
                .Parameters("UpdateID").Value = PropUserId                                   '問題作業履歴：最終更新者ID()
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
    ''' 【新規登録／編集／作業履歴モード】問題作業担当情報　新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>問題作業担当新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertProblemWkTantoSql(ByRef Cmd As NpgsqlCommand, _
                                               ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKD0201 As DataHBKD0201, ByVal ColCnt As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertProblemWkTantoSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))           '問題番号
                '.Add(New NpgsqlParameter("WorkRirekiNmb", NpgsqlTypes.NpgsqlDbType.Integer))    '作業履歴番号
                .Add(New NpgsqlParameter("WorkTantoNmb", NpgsqlTypes.NpgsqlDbType.Integer))    '作業担当番号
                .Add(New NpgsqlParameter("WorkTantoGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))   '作業担当グループCD
                .Add(New NpgsqlParameter("WorkTantoID", NpgsqlTypes.NpgsqlDbType.Varchar))          '作業担当者ID
                .Add(New NpgsqlParameter("WorkTantoGrpNM", NpgsqlTypes.NpgsqlDbType.Varchar))   '作業担当グループ名
                .Add(New NpgsqlParameter("WorkTantoNM", NpgsqlTypes.NpgsqlDbType.Varchar))      '作業担当者名
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))          '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))         '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))            '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '最終更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb
                '.Parameters("WorkRirekiNmb").Value = dataHBKD0201.PropDrRegRow.Item("WorkRirekiNmb")
                .Parameters("WorkTantoNmb").Value = ColCnt
                .Parameters("WorkTantoGrpCD").Value = dataHBKD0201.PropDrRegRow.Item("WorkTantoGrpCD" & ColCnt)
                .Parameters("WorkTantoID").Value = dataHBKD0201.PropDrRegRow.Item("WorkTantoID" & ColCnt)
                .Parameters("WorkTantoGrpNM").Value = dataHBKD0201.PropDrRegRow.Item("WorkTantoGrpNM" & ColCnt)
                .Parameters("WorkTantoNM").Value = dataHBKD0201.PropDrRegRow.Item("WorkTantoNM" & ColCnt)
                .Parameters("RegDT").Value = dataHBKD0201.PropDtmSysDate                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKD0201.PropDtmSysDate                     '最終更新日時
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
    ''' 【新規登録／編集／作業履歴モード】問題作業担当情報　新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>問題作業担当新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateProblemWkTantoSql(ByRef Cmd As NpgsqlCommand, _
                                               ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKD0201 As DataHBKD0201, ByVal ColCnt As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strUpdateProblemWkTantoSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))           '問題番号
                .Add(New NpgsqlParameter("WorkRirekiNmb", NpgsqlTypes.NpgsqlDbType.Integer))    '作業履歴番号
                .Add(New NpgsqlParameter("WorkTantoNmb", NpgsqlTypes.NpgsqlDbType.Integer))    '作業担当番号
                .Add(New NpgsqlParameter("WorkTantoGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))   '作業担当グループCD
                .Add(New NpgsqlParameter("WorkTantoID", NpgsqlTypes.NpgsqlDbType.Varchar))          '作業担当者ID
                .Add(New NpgsqlParameter("WorkTantoGrpNM", NpgsqlTypes.NpgsqlDbType.Varchar))   '作業担当グループ名
                .Add(New NpgsqlParameter("WorkTantoNM", NpgsqlTypes.NpgsqlDbType.Varchar))      '作業担当者名
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))          '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))         '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))            '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '最終更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb
                .Parameters("WorkRirekiNmb").Value = dataHBKD0201.PropDrRegRow.Item("WorkRirekiNmb")
                .Parameters("WorkTantoNmb").Value = ColCnt
                .Parameters("WorkTantoGrpCD").Value = dataHBKD0201.PropDrRegRow.Item("WorkTantoGrpCD" & ColCnt)
                .Parameters("WorkTantoID").Value = dataHBKD0201.PropDrRegRow.Item("WorkTantoID" & ColCnt)
                .Parameters("WorkTantoGrpNM").Value = dataHBKD0201.PropDrRegRow.Item("WorkTantoGrpNM" & ColCnt)
                .Parameters("WorkTantoNM").Value = dataHBKD0201.PropDrRegRow.Item("WorkTantoNM" & ColCnt)
                .Parameters("RegDT").Value = dataHBKD0201.PropDtmSysDate                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKD0201.PropDtmSysDate                     '最終更新日時
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
    ''' 【新規登録／編集モード】問題対応関係情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>問題対応関係情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertProblemKankeiSql(ByRef Cmd As NpgsqlCommand, _
                                              ByVal Cn As NpgsqlConnection, _
                                              ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertProblemKankeiSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   '問題番号
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
                .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb                                '問題番号
                .Parameters("RelationKbn").Value = dataHBKD0201.PropDrRegRow.Item("RelationKbn")        '関係区分
                .Parameters("RelationID").Value = dataHBKD0201.PropDrRegRow.Item("RelationID")          '関係ID
                If dataHBKD0201.PropDrRegRow.Item("RegDT").ToString.Length > 0 Then
                    .Parameters("RegDT").Value = dataHBKD0201.PropDrRegRow.Item("RegDT")                '登録日時
                    .Parameters("RegGrpCD").Value = dataHBKD0201.PropDrRegRow.Item("RegGrpCD")          '登録者グループCD
                    .Parameters("RegID").Value = dataHBKD0201.PropDrRegRow.Item("RegID")                '登録者ID
                Else
                    .Parameters("RegDT").Value = dataHBKD0201.PropDtmSysDate                            '登録日時
                    .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
                    .Parameters("RegID").Value = PropUserId                                             '登録者ID
                End If
                .Parameters("UpdateDT").Value = dataHBKD0201.PropDtmSysDate                         '最終更新日時
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
    ''' 【新規登録モード】問題プロセスリンク(元)情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <param name="intAddMilliSec">[IN]ミリ秒数カウンタ</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>プロセスリンク情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/23 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InsertPLinkMoto(ByRef Cmd As NpgsqlCommand, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKD0201 As DataHBKD0201, _
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
                .Add(New NpgsqlParameter("LinkMotoProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))       '元P区分
                .Add(New NpgsqlParameter("LinkMotoNmb", NpgsqlTypes.NpgsqlDbType.Integer))              '元問題番号
                .Add(New NpgsqlParameter("LinkSakiProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))       '先P区分
                .Add(New NpgsqlParameter("LinkSakiNmb", NpgsqlTypes.NpgsqlDbType.Integer))              '先問題番号
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
                .Parameters("LinkMotoProcessKbn").Value = PROCESS_TYPE_QUESTION                             '元P区分
                .Parameters("LinkMotoNmb").Value = dataHBKD0201.PropIntPrbNmb                               '元問題番号
                .Parameters("LinkSakiProcessKbn").Value = dataHBKD0201.PropDrRegRow.Item("ProcessKbn")      '参照先P区分
                .Parameters("LinkSakiNmb").Value = dataHBKD0201.PropDrRegRow.Item("MngNmb")                 '参照先問題番号

                '****************************************************************************************************************
                .Parameters("EntryDT").Value = dataHBKD0201.PropDtmSysDate.AddMilliseconds(intAddMilliSec)  'カウンタ
                '.Parameters("EntryDT").Value = dataHBKD0201.PropDtmSysDate  'カウンタ
                '****************************************************************************************************************

                If dataHBKD0201.PropDrRegRow.Item("RegDT").ToString.Equals("") Then
                    .Parameters("RegDT").Value = dataHBKD0201.PropDtmSysDate                                '登録日時
                    .Parameters("RegGrpCD").Value = PropWorkGroupCD                                         '登録者グループCD
                    .Parameters("RegID").Value = PropUserId                                                 '登録者ID
                    .Parameters("UpdateDT").Value = dataHBKD0201.PropDtmSysDate                             '最終更新日時
                    .Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                    .Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
                Else
                    .Parameters("RegDT").Value = dataHBKD0201.PropDrRegRow.Item("RegDT")                    '登録日時
                    .Parameters("RegGrpCD").Value = dataHBKD0201.PropDrRegRow.Item("RegGrpCD")              '登録者グループCD
                    .Parameters("RegID").Value = dataHBKD0201.PropDrRegRow.Item("RegID")                    '登録者ID
                    .Parameters("UpdateDT").Value = dataHBKD0201.PropDrRegRow.Item("UpdateDt")              '最終更新日時
                    .Parameters("UpGrpCD").Value = dataHBKD0201.PropDrRegRow.Item("UpGrpCD")                '最終更新者グループCD
                    .Parameters("UpdateID").Value = dataHBKD0201.PropDrRegRow.Item("UpdateID")              '最終更新者ID
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
    ''' 【編集モード】問題プロセスリンク(元)情報更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>プロセスりインク情報削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/23 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function DeletePLinkMoto(ByRef Cmd As NpgsqlCommand, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(Delete)
            strSQL = strDeleteProcessLinkSql

            'データアダプタに、SQLのDelete文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LinkMotoNmb", NpgsqlTypes.NpgsqlDbType.Integer))               'リンク元番号
                .Add(New NpgsqlParameter("LinkMotoProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))        'リンク元プロセス区分
                .Add(New NpgsqlParameter("LinkSakiNmb", NpgsqlTypes.NpgsqlDbType.Integer))               'リンク先番号
                .Add(New NpgsqlParameter("LinkSakiProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))        'リンク先プロセス区分
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LinkMotoNmb").Value = dataHBKD0201.PropIntPrbNmb                                                   'リンク元番号
                .Parameters("LinkMotoProcessKbn").Value = PROCESS_TYPE_QUESTION                                                 'リンク元プロセス区分
                .Parameters("LinkSakiNmb").Value = dataHBKD0201.PropDrRegRow("MngNmb", DataRowVersion.Original)                 'リンク先番号
                .Parameters("LinkSakiProcessKbn").Value = dataHBKD0201.PropDrRegRow("ProcessKbn", DataRowVersion.Original)      'リンク先プロセス区分
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
    ''' 【編集モード】問題プロセスリンク(先)情報更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>プロセスリンク削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/23 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function DeletePLinkSaki(ByRef Cmd As NpgsqlCommand, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Try

            'SQL文(Delete)
            strSQL = strDeleteProcessLinkSql

            'データアダプタに、SQLのDelete文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LinkMotoNmb", NpgsqlTypes.NpgsqlDbType.Integer))               'リンク元番号
                .Add(New NpgsqlParameter("LinkMotoProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))        'リンク元プロセス区分
                .Add(New NpgsqlParameter("LinkSakiNmb", NpgsqlTypes.NpgsqlDbType.Integer))               'リンク先番号
                .Add(New NpgsqlParameter("LinkSakiProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))        'リンク先プロセス区分
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LinkMotoNmb").Value = dataHBKD0201.PropDrRegRow("MngNmb", DataRowVersion.Original)             'リンク元番号
                .Parameters("LinkMotoProcessKbn").Value = dataHBKD0201.PropDrRegRow("ProcessKbn", DataRowVersion.Original)  'リンク元プロセス区分
                .Parameters("LinkSakiNmb").Value = dataHBKD0201.PropIntPrbNmb                                               'リンク先番号
                .Parameters("LinkSakiProcessKbn").Value = PROCESS_TYPE_QUESTION                                             'リンク先プロセス区分
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
    ''' 【新規登録モード】問題CYSPR情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>問題CYSPR情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/23 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertProblemCysprSql(ByRef Cmd As NpgsqlCommand, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Dim strBikoAimai As String = ""         'フリーテキスト（あいまい）

        Try

            'SQL文(INSERT)
            strSQL = strInsertProblemCyspr

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))           '問題番号
                .Add(New NpgsqlParameter("CysprNmb", NpgsqlTypes.NpgsqlDbType.Varchar))         'CYSPR番号
                .Add(New NpgsqlParameter("CysprNmbAimai", NpgsqlTypes.NpgsqlDbType.Varchar))    'CYSPR番号(あいまい)
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))          '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))         '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))            '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '最終更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb                        '問題番号
                .Parameters("CysprNmb").Value = dataHBKD0201.PropDrRegRow.Item("CysprNmb")      'CYSPR番号
                .Parameters("CysprNmbAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKD0201.PropDrRegRow.Item("CysprNmb")) 'CYSPR番号(あいまい)

                Dim no_update_flg As Boolean = False
                If dataHBKD0201.PropDrRegRow.Item("RegDT").ToString.Length > 0 Then
                    .Parameters("RegDT").Value = dataHBKD0201.PropDrRegRow.Item("RegDT")
                    .Parameters("RegGrpCD").Value = dataHBKD0201.PropDrRegRow.Item("RegGrpCD")
                    .Parameters("RegID").Value = dataHBKD0201.PropDrRegRow.Item("RegID")
                    If dataHBKD0201.PropDrRegRow.Item("cysprnmb").ToString.Equals(dataHBKD0201.PropDrRegRow.Item("bef").ToString) Then
                        '更新箇所が１つもない
                        no_update_flg = True
                    End If

                Else
                    .Parameters("RegDT").Value = dataHBKD0201.PropDtmSysDate                         '登録日時
                    .Parameters("RegGrpCD").Value = PropWorkGroupCD                                  '登録者グループCD
                    .Parameters("RegID").Value = PropUserId                                          '登録者ID
                End If

                If no_update_flg = True Then
                    .Parameters("UpdateDT").Value = dataHBKD0201.PropDrRegRow.Item("UpdateDT")
                    .Parameters("UpGrpCD").Value = dataHBKD0201.PropDrRegRow.Item("UpGrpCD")
                    .Parameters("UpdateID").Value = dataHBKD0201.PropDrRegRow.Item("UpdateID")
                Else
                    .Parameters("UpdateDT").Value = dataHBKD0201.PropDtmSysDate                      '最終更新日時
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
    ''' <param name="dataHBKD0201">[IN]変更登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議結果情報更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/30 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertMtgResultSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKD0201 As DataHBKD0201) As Boolean

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

                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))          '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))         '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))            '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("MeetingNmb").Value = dataHBKD0201.PropDrRegRow.Item("MeetingNmb")    '会議番号
                .Parameters("ProcessKbn").Value = PROCESS_TYPE_QUESTION                          'プロセス区分
                .Parameters("ProcessNmb").Value = dataHBKD0201.PropIntPrbNmb                    'プロセス番号
                .Parameters("RegDT").Value = dataHBKD0201.PropDtmSysDate                         '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                  '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                          '登録者ID
                .Parameters("UpdateDT").Value = dataHBKD0201.PropDtmSysDate                     '最終更新日時
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
    ''' 【共通】新規ログNo取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規ログNo取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewRirekiNoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKD0201 As DataHBKD0201) As Boolean

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
                .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))   '問題番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb                '問題番号
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
    ''' 【共通】問題共通情報ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>問題共通情報ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertProblemInfoLSql(ByRef Cmd As NpgsqlCommand, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertProblemInfoLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
                .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))       '問題番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKD0201.PropIntLogNo                      'ログNo
                .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb                    '問題番号
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
    ''' 【共通】問題作業履歴ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>問題作業履歴ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertProblemWkRirekiLSql(ByRef Cmd As NpgsqlCommand, _
                                                 ByVal Cn As NpgsqlConnection, _
                                                 ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertProblemWkRirekiLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
                .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))       '問題番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKD0201.PropIntLogNo                      'ログNo
                .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb                    '問題番号
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
    ''' 【共通】問題作業担当ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>作業担当ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertProblemWkTantoLSql(ByRef Cmd As NpgsqlCommand, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertProblemWkTantoLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
                .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))       '問題番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKD0201.PropIntLogNo                      'ログNo
                .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb                    '問題番号
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
    ''' 【共通】問題対応関係情報ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>問題対応関係情報ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertProblemKankeiLSql(ByRef Cmd As NpgsqlCommand, _
                                               ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertProblemKankeiLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
                .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))       '問題番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKD0201.PropIntLogNo                      'ログNo
                .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb                    '問題番号
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
    ''' 【共通】問題プロセスリンク情報ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>問題プロセスリンク情報ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertPLinkMotoLSql(ByRef Cmd As NpgsqlCommand, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKD0201 As DataHBKD0201) As Boolean

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
                .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))       '問題番号
                .Add(New NpgsqlParameter("PKbn", NpgsqlTypes.NpgsqlDbType.Varchar))         'プロセス区分
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKD0201.PropIntLogNo                      'ログNo
                .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb                    '問題番号
                .Parameters("PKbn").Value = PROCESS_TYPE_QUESTION                           'プロセス区分
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
    ''' 【共通】問題CYSPR情報ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>問題CYSPR情報ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/23 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertProblemCysprLSql(ByRef Cmd As NpgsqlCommand, _
                                              ByVal Cn As NpgsqlConnection, _
                                              ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertProblemCysprLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
                .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))       '問題番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKD0201.PropIntLogNo                      'ログNo
                .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb                    '問題番号
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
    ''' 【共通】問題関連ファイル情報ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>問題関連ファイル情報ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertProblemFileLSql(ByRef Cmd As NpgsqlCommand, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertProblemFileLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
                .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))       '問題番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKD0201.PropIntLogNo                      'ログNo
                .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb                    '問題番号
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
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規ログNo取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewMeetingRirekiNoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                   ByVal Cn As NpgsqlConnection, _
                                                   ByVal dataHBKD0201 As DataHBKD0201) As Boolean

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
                .Parameters("MeetingNmb").Value = dataHBKD0201.PropIntMeetingNmb            '会議番号
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
    ''' 【共通】会議情報ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議情報ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertMeetingLSql(ByRef Cmd As NpgsqlCommand, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKD0201 As DataHBKD0201) As Boolean

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
                .Parameters("LogNo").Value = dataHBKD0201.PropIntLogNoSub                   'ログNo
                .Parameters("MeetingNmb").Value = dataHBKD0201.PropIntMeetingNmb            '会議番号
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
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議結果情報ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertMtgResultLSql(ByRef Cmd As NpgsqlCommand, _
                                               ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKD0201 As DataHBKD0201) As Boolean

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
                .Add(New NpgsqlParameter("MeetingNmb", NpgsqlTypes.NpgsqlDbType.Integer))       '会議No
                .Add(New NpgsqlParameter("ProcessNmb", NpgsqlTypes.NpgsqlDbType.Integer))       'プロセス番号
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))       'プロセス区分
                .Add(New NpgsqlParameter("ProcessLogNo", NpgsqlTypes.NpgsqlDbType.Integer))     'プロセスログ番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKD0201.PropIntLogNoSub                       'ログNo
                .Parameters("MeetingNmb").Value = dataHBKD0201.PropIntMeetingNmb                '会議No
                .Parameters("ProcessNmb").Value = dataHBKD0201.PropIntPrbNmb                    'プロセス番号
                .Parameters("ProcessKbn").Value = PROCESS_TYPE_QUESTION                         'プロセス区分
                .Parameters("ProcessLogNo").Value = dataHBKD0201.PropIntLogNo                   'プロセスログ番号
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
    ''' 【編集／作業履歴モード】サーバー日付取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKD0201 As DataHBKD0201) As Boolean

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
    ''' 【編集モード】問題共通情報更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>問題共通情報更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/23 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateProblemInfoSql(ByRef Cmd As NpgsqlCommand, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Dim strBikoAimai As String = ""         'フリーテキスト（あいまい）

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateProblemInfoSql

            'データアダプタに、SQLのUPDATE文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))               '問題番号
                .Add(New NpgsqlParameter("ProcessStateCD", NpgsqlTypes.NpgsqlDbType.Varchar))       'プロセスステータスCD
                .Add(New NpgsqlParameter("PrbCaseCD", NpgsqlTypes.NpgsqlDbType.Varchar))            '問題発生原因CD
                .Add(New NpgsqlParameter("KaisiDT", NpgsqlTypes.NpgsqlDbType.Timestamp))            '開始日時
                .Add(New NpgsqlParameter("KanryoDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '完了日時
                .Add(New NpgsqlParameter("Title", NpgsqlTypes.NpgsqlDbType.Varchar))                'タイトル
                .Add(New NpgsqlParameter("Naiyo", NpgsqlTypes.NpgsqlDbType.Varchar))                '内容
                .Add(New NpgsqlParameter("Taisyo", NpgsqlTypes.NpgsqlDbType.Varchar))               '対処
                .Add(New NpgsqlParameter("SystemNmb", NpgsqlTypes.NpgsqlDbType.Integer))            '対象システム番号
                .Add(New NpgsqlParameter("ApproverID", NpgsqlTypes.NpgsqlDbType.Varchar))           '対処承認者ID
                .Add(New NpgsqlParameter("ApproverNM", NpgsqlTypes.NpgsqlDbType.Varchar))           '対処承認者氏名
                .Add(New NpgsqlParameter("RecorderID", NpgsqlTypes.NpgsqlDbType.Varchar))           '承認記録者ID
                .Add(New NpgsqlParameter("RecorderNM", NpgsqlTypes.NpgsqlDbType.Varchar))           '承認記録者氏名
                .Add(New NpgsqlParameter("TantoGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))           '担当グループCD
                .Add(New NpgsqlParameter("PrbTantoID", NpgsqlTypes.NpgsqlDbType.Varchar))           '問題担当者ID
                .Add(New NpgsqlParameter("PrbTantoNM", NpgsqlTypes.NpgsqlDbType.Varchar))           '問題担当者氏名
                .Add(New NpgsqlParameter("BIko1", NpgsqlTypes.NpgsqlDbType.Varchar))                'フリーテキスト１
                .Add(New NpgsqlParameter("Biko2", NpgsqlTypes.NpgsqlDbType.Varchar))                'フリーテキスト２
                .Add(New NpgsqlParameter("Biko3", NpgsqlTypes.NpgsqlDbType.Varchar))                'フリーテキスト３
                .Add(New NpgsqlParameter("Biko4", NpgsqlTypes.NpgsqlDbType.Varchar))                'フリーテキスト４
                .Add(New NpgsqlParameter("Biko5", NpgsqlTypes.NpgsqlDbType.Varchar))                'フリーテキスト５
                .Add(New NpgsqlParameter("FreeFlg1", NpgsqlTypes.NpgsqlDbType.Varchar))             'フリーフラグ１
                .Add(New NpgsqlParameter("FreeFlg2", NpgsqlTypes.NpgsqlDbType.Varchar))             'フリーフラグ２
                .Add(New NpgsqlParameter("FreeFlg3", NpgsqlTypes.NpgsqlDbType.Varchar))             'フリーフラグ３
                .Add(New NpgsqlParameter("FreeFlg4", NpgsqlTypes.NpgsqlDbType.Varchar))             'フリーフラグ４
                .Add(New NpgsqlParameter("FreeFlg5", NpgsqlTypes.NpgsqlDbType.Varchar))             'フリーフラグ５
                .Add(New NpgsqlParameter("TitleAimai", NpgsqlTypes.NpgsqlDbType.Varchar))           'タイトル(あいまい)
                .Add(New NpgsqlParameter("NaiyoAimai", NpgsqlTypes.NpgsqlDbType.Varchar))           '内容(あいまい)
                .Add(New NpgsqlParameter("TaisyoAimai", NpgsqlTypes.NpgsqlDbType.Varchar))          '対処(あいまい)
                .Add(New NpgsqlParameter("BikoAimai", NpgsqlTypes.NpgsqlDbType.Varchar))            'フリーテキスト(あいまい)
                .Add(New NpgsqlParameter("PrbTantIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))       '問題担当者ID(あいまい)
                .Add(New NpgsqlParameter("PrbTantNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))       '問題担当者氏名(あいまい)
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '最終更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb                            '問題番号
                .Parameters("ProcessStateCD").Value = dataHBKD0201.PropCmbStatus.SelectedValue      'プロセスステータスCD
                .Parameters("PrbCaseCD").Value = dataHBKD0201.PropCmbPrbCase.SelectedValue          '問題発生原因CD
                '開始日時
                If dataHBKD0201.PropDtpStartDT.txtDate.Text.Equals("") Then
                    .Parameters("KaisiDT").Value = Nothing
                Else
                    .Parameters("KaisiDT").Value = _
                        CDate(dataHBKD0201.PropDtpStartDT.txtDate.Text & " " & dataHBKD0201.PropTxtStartDT_HM.PropTxtTime.Text)
                End If
                '完了日時
                If dataHBKD0201.PropDtpKanryoDT.txtDate.Text.Equals("") Then
                    'ステータスが完了ならばシステム日付を設定する
                    If dataHBKD0201.PropCmbStatus.SelectedValue = PROCESS_STATUS_QUESTION_KANRYOH Then
                        .Parameters("KanryoDT").Value = dataHBKD0201.PropDtmSysDate
                    Else
                        .Parameters("KanryoDT").Value = Nothing
                    End If
                Else
                    .Parameters("KanryoDT").Value = _
                        CDate(dataHBKD0201.PropDtpKanryoDT.txtDate.Text & " " & dataHBKD0201.PropTxtKanryoDT_HM.PropTxtTime.Text)
                End If
                .Parameters("Title").Value = dataHBKD0201.PropTxtTitle.Text                         'タイトル
                .Parameters("Naiyo").Value = dataHBKD0201.PropTxtNaiyo.Text                         '内容
                .Parameters("Taisyo").Value = dataHBKD0201.PropTxtTaisyo.Text                       '対処
                .Parameters("SystemNmb").Value = dataHBKD0201.PropCmbTargetSystem.PropCmbColumns.SelectedValue      '対象システム番号
                .Parameters("ApproverID").Value = dataHBKD0201.PropTxtApproverID.Text               '対処承認者ID
                .Parameters("ApproverNM").Value = dataHBKD0201.PropTxtApproverNM.Text               '対処承認者氏名
                .Parameters("RecorderID").Value = dataHBKD0201.PropTxtRecorderID.Text               '承認記録者ID
                .Parameters("RecorderNM").Value = dataHBKD0201.PropTxtRecorderNM.Text               '承認記録者氏名
                .Parameters("TantoGrpCD").Value = dataHBKD0201.PropCmbTantoGrp.SelectedValue        '担当グループCD
                .Parameters("PrbTantoID").Value = dataHBKD0201.PropTxtPrbTantoID.Text               '問題担当者ID
                .Parameters("PrbTantoNM").Value = dataHBKD0201.PropTxtPrbTantoNM.Text               '問題担当者氏名
                .Parameters("BIko1").Value = dataHBKD0201.PropTxtFreeText1.Text                     'フリーテキスト1
                .Parameters("Biko2").Value = dataHBKD0201.PropTxtFreeText2.Text                     'フリーテキスト2
                .Parameters("Biko3").Value = dataHBKD0201.PropTxtFreeText3.Text                     'フリーテキスト3
                .Parameters("Biko4").Value = dataHBKD0201.PropTxtFreeText4.Text                     'フリーテキスト4
                .Parameters("Biko5").Value = dataHBKD0201.PropTxtFreeText5.Text                     'フリーテキスト5
                'フリーフラグ1
                If dataHBKD0201.PropChkFreeFlg1.Checked = True Then
                    .Parameters("FreeFlg1").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg1").Value = FREE_FLG_OFF
                End If
                'フリーフラグ2
                If dataHBKD0201.PropChkFreeFlg2.Checked = True Then
                    .Parameters("FreeFlg2").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg2").Value = FREE_FLG_OFF
                End If
                'フリーフラグ3
                If dataHBKD0201.PropChkFreeFlg3.Checked = True Then
                    .Parameters("FreeFlg3").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg3").Value = FREE_FLG_OFF
                End If
                'フリーフラグ4
                If dataHBKD0201.PropChkFreeFlg4.Checked = True Then
                    .Parameters("FreeFlg4").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg4").Value = FREE_FLG_OFF
                End If
                'フリーフラグ5
                If dataHBKD0201.PropChkFreeFlg5.Checked = True Then
                    .Parameters("FreeFlg5").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg5").Value = FREE_FLG_OFF
                End If
                .Parameters("TitleAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKD0201.PropTxtTitle.Text)              'タイトル(あいまい)
                .Parameters("NaiyoAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKD0201.PropTxtNaiyo.Text)              '内容(あいまい)
                .Parameters("TaisyoAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKD0201.PropTxtTaisyo.Text)            '対処(あいまい)
                .Parameters("BikoAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKD0201.PropTxtFreeText1.Text) & _
                                                 commonLogicHBK.ChangeStringForSearch(dataHBKD0201.PropTxtFreeText2.Text) & _
                                                 commonLogicHBK.ChangeStringForSearch(dataHBKD0201.PropTxtFreeText3.Text) & _
                                                 commonLogicHBK.ChangeStringForSearch(dataHBKD0201.PropTxtFreeText4.Text) & _
                                                 commonLogicHBK.ChangeStringForSearch(dataHBKD0201.PropTxtFreeText5.Text)           'フリーテキスト(あいまい)
                .Parameters("PrbTantIDAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKD0201.PropTxtPrbTantoID.Text)     '問題担当者ID(あいまい)
                .Parameters("PrbTantNMAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKD0201.PropTxtPrbTantoNM.Text)     '問題担当者氏名(あいまい)
                .Parameters("UpdateDT").Value = dataHBKD0201.PropDtmSysDate         '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                      '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                          '最終更新者ID
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

    ' ''' <summary>
    ' ''' 【編集／作業履歴モード】問題作業履歴更新用SQLの作成・設定処理
    ' ''' </summary>
    ' ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ' ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ' ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ' ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ' ''' <remarks>問題作業履歴削除用のSQLを作成し、アダプタにセットする
    ' ''' <para>作成情報：2012/08/23 s.yamaguchi
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Public Function SetDeleteProblemWkRirekiSql(ByRef Cmd As NpgsqlCommand, _
    '                                            ByVal Cn As NpgsqlConnection, _
    '                                            ByVal dataHBKD0201 As DataHBKD0201) As Boolean

    '    '開始ログ出力
    '    CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数の宣言
    '    Dim strSQL As String = ""               'SQL文

    '    Try

    '        'SQL文(UPDATE)
    '        strSQL = strDeleteProblemWkRirekiSql

    '        'データアダプタに、SQLのUPDATE文を設定
    '        Cmd = New NpgsqlCommand(strSQL, Cn)

    '        'バインド変数に型をセット
    '        With Cmd.Parameters
    '            .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))       '問題番号
    '        End With
    '        'バインド変数に値をセット
    '        With Cmd
    '            .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb                    '問題番号
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

    ''' <summary>
    ''' 【編集／作業履歴モード】問題作業担当削除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>問題作業担当削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/23 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteProblemWkTantoSql(ByRef Cmd As NpgsqlCommand, _
                                               ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Try

            'SQL文(UPDATE)
            strSQL = strDeleteProblemWkTantoSql

            'データアダプタに、SQLのDELETE文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))            '問題番号
                .Add(New NpgsqlParameter("workrirekinmb", NpgsqlTypes.NpgsqlDbType.Integer))     '履歴番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb
                .Parameters("workrirekinmb").Value = dataHBKD0201.PropDrRegRow.Item("workrirekinmb")
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
    ''' 【編集モード】問題対応関係情報削除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>問題対応関係削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/23 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteProblemKankeiSql(ByRef Cmd As NpgsqlCommand, _
                                              ByVal Cn As NpgsqlConnection, _
                                              ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Try

            'SQL文(Delete)
            strSQL = strDeleteProblemKankeiSql

            'データアダプタに、SQLのDelete文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            With Cmd.Parameters
                .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   '問題番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb                                '問題番号
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
    ''' 【編集モード】問題CYSPR情報削除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>問題CYSPR情報削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/23 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteProblemCysprSql(ByRef Cmd As NpgsqlCommand, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Try

            'SQL文(Delete)
            strSQL = strDeleteProblemCysprSql

            'データアダプタに、SQLのDelete文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            With Cmd.Parameters
                .Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   '問題番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("PrbNmb").Value = dataHBKD0201.PropIntPrbNmb                                '問題番号
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
    ''' 【編集モード】会議結果情報削除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議結果情報削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/23 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteMtgResultSql(ByRef Cmd As NpgsqlCommand, _
                                              ByVal Cn As NpgsqlConnection, _
                                              ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(Delete)
            strSQL = strDeleteMeetingResultSql

            'データアダプタに、SQLのDelete文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("ProcessNmb", NpgsqlTypes.NpgsqlDbType.Integer))               'プロセス番号
                .Add(New NpgsqlParameter("processkbn", NpgsqlTypes.NpgsqlDbType.Varchar))               'プロセス区分
                .Add(New NpgsqlParameter("meetingnmb", NpgsqlTypes.NpgsqlDbType.Integer))               '会議番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("ProcessNmb").Value = dataHBKD0201.PropIntPrbNmb                            'プロセス番号
                .Parameters("processkbn").Value = PROCESS_TYPE_QUESTION
                .Parameters("meetingnmb").Value = dataHBKD0201.PropDrRegRow.Item("meetingnmb", DataRowVersion.Original)
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
    ''' 【新規登録／編集モード】会議結果情報登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議結果情報登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/23 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertMeetingResultSql(ByRef Cmd As NpgsqlCommand, _
                                              ByVal Cn As NpgsqlConnection, _
                                              ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertMeetingResultSql

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
                .Parameters("MeetingNmb").Value = dataHBKD0201.PropDrRegRow.Item("MeetingNmb")  '会議番号
                .Parameters("ProcessKbn").Value = PROCESS_TYPE_QUESTION                          'プロセス区分
                .Parameters("ProcessNmb").Value = dataHBKD0201.PropIntPrbNmb                    'プロセス番号
                '結果区分がブランクの場合は0を設定
                If dataHBKD0201.PropDrRegRow.Item("ResultKbn").ToString.Equals("") Then
                    .Parameters("ResultKbn").Value = SELECT_RESULTKBN_NO                        'ブランク
                Else
                    .Parameters("ResultKbn").Value = dataHBKD0201.PropDrRegRow.Item("ResultKbn")
                End If




                '編集の判定を行う
                If dataHBKD0201.PropDrRegRow.Item("RegDT").ToString.Equals("") Then
                    .Parameters("RegDT").Value = dataHBKD0201.PropDtmSysDate                                '登録日時
                    .Parameters("RegGrpCD").Value = PropWorkGroupCD                                         '登録者グループCD
                    .Parameters("RegID").Value = PropUserId                                                 '登録者ID
                    .Parameters("UpdateDT").Value = dataHBKD0201.PropDtmSysDate                             '最終更新日時
                    .Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                    .Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
                Else
                    .Parameters("RegDT").Value = dataHBKD0201.PropDrRegRow.Item("RegDT")                    '登録日時
                    .Parameters("RegGrpCD").Value = dataHBKD0201.PropDrRegRow.Item("RegGrpCD")              '登録者グループCD
                    .Parameters("RegID").Value = dataHBKD0201.PropDrRegRow.Item("RegID")                    '登録者ID
                    .Parameters("UpdateDT").Value = dataHBKD0201.PropDrRegRow.Item("UpdateDt")              '最終更新日時
                    .Parameters("UpGrpCD").Value = dataHBKD0201.PropDrRegRow.Item("UpGrpCD")                '最終更新者グループCD
                    .Parameters("UpdateID").Value = dataHBKD0201.PropDrRegRow.Item("UpdateID")              '最終更新者ID
                End If


                '.Parameters("RegDT").Value = dataHBKD0201.PropDtmSysDate                        '登録日時
                '.Parameters("RegGrpCD").Value = PropWorkGroupCD                                 '登録者グループCD
                '.Parameters("RegID").Value = PropUserId                                         '登録者ID
                '.Parameters("UpdateDT").Value = dataHBKD0201.PropDtmSysDate                     '最終更新日時
                '.Parameters("UpGrpCD").Value = PropWorkGroupCD                                  '最終更新者グループCD
                '.Parameters("UpdateID").Value = PropUserId                                      '最終更新者ID
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
    ''' 問題共通情報ロックテーブル、サーバー日付取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intPrbNmb">[IN]問題番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>指定された問題番号の問題共通情報ロックテーブルおよびサーバー日付を返す
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function SelectPrbLock(ByVal Adapter As NpgsqlDataAdapter, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByVal intPrbNmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'SQL変数宣言
        Dim strSQL As String = ""

        Try

            strSQL = strSelectPrbInfoSql

            ' データアダプタに、問題共通情報ロックテーブル取得用SQLを設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))       '問題番号

            'バインド変数に値をセット
            Adapter.SelectCommand.Parameters("PrbNmb").Value = intPrbNmb                                                '問題番号

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
    ''' 問題共通情報ロックテーブル登録処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intPrbNmb">[IN]問題番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>問題共通情報ロックテーブル登録
    ''' <para>作成情報：2012/08/27 y.ikushima
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function InsertPrbLockSql(ByRef Cmd As NpgsqlCommand, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByVal intPrbNmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        '変数宣言
        Dim strSQL As String = ""

        Try

            strSQL = strInsertPrbLockSql

            'データアダプタに、SQLのDelete文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            Cmd.Parameters.Add(New NpgsqlParameter("EdiGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))   '編集者グループコード
            Cmd.Parameters.Add(New NpgsqlParameter("EdiID", NpgsqlTypes.NpgsqlDbType.Varchar))      '編集者ID
            Cmd.Parameters.Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))     '問題番号

            'バインド変数に値をセット
            Cmd.Parameters("EdiGrpCD").Value = PropWorkGroupCD                                      '編集者グループコード
            Cmd.Parameters("EdiID").Value = PropUserId                                              '編集者ID
            Cmd.Parameters("PrbNmb").Value = intPrbNmb                                              '問題番号

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
    ''' 問題共通情報ロックテーブル削除処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intPrbNmb">[IN]問題番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>問題共通情報ロックテーブル削除する
    ''' <para>作成情報：2012/08/27 y.ikushima
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function DeletePrbLockSql(ByRef Cmd As NpgsqlCommand, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByVal intPrbNmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        '変数宣言
        Dim strSQL As String = ""

        Try

            strSQL = strDeletePrbLockSql

            'コマンドに、削除用SQLを設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            Cmd.Parameters.Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))      '問題番号

            'バインド変数に値をセット
            Cmd.Parameters("PrbNmb").Value = intPrbNmb                                               '問題番号

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
    ''' <para>作成情報：2012/08/29 y.ikushima
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
    ''' <para>作成情報：2012/08/29 y.ikushima
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
    ''' <para>作成情報：2012/08/29 y.ikushima
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
    ''' 【共通】会議出席者情報ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議出席者情報ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertMtgAttendLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKD0201 As DataHBKD0201) As Boolean

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
                .Parameters("LogNo").Value = dataHBKD0201.PropIntLogNoSub                   'ログNo
                .Parameters("MeetingNmb").Value = dataHBKD0201.PropIntMeetingNmb            '会議番号
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
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議関連ファイル情報ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertMtgFileLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKD0201 As DataHBKD0201) As Boolean

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
                .Parameters("LogNo").Value = dataHBKD0201.PropIntLogNoSub                   'ログNo
                .Parameters("MeetingNmb").Value = dataHBKD0201.PropIntMeetingNmb            '会議番号
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
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>担当履歴情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/10 r.hoshino
    ''' <p>改訂情報</p>
    ''' </para></remarks>
    Public Function SetInsertTantoRirekiSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKD0201 As DataHBKD0201) As Boolean

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
                .Add(New NpgsqlParameter("prbNmb", NpgsqlTypes.NpgsqlDbType.Integer))           '意管理番号
                .Add(New NpgsqlParameter("tantogrpcd", NpgsqlTypes.NpgsqlDbType.Varchar))       'グループcd
                .Add(New NpgsqlParameter("tantogrpnm", NpgsqlTypes.NpgsqlDbType.Varchar))       'グループ名
                .Add(New NpgsqlParameter("prbtantoid", NpgsqlTypes.NpgsqlDbType.Varchar))       '担当ＩＤ
                .Add(New NpgsqlParameter("prbtantonm", NpgsqlTypes.NpgsqlDbType.Varchar))       '担当名

                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))        '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))           '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("prbNmb").Value = dataHBKD0201.PropIntPrbNmb
                .Parameters("tantogrpcd").Value = dataHBKD0201.PropCmbTantoGrp.SelectedValue
                .Parameters("tantogrpnm").Value = dataHBKD0201.PropCmbTantoGrp.Text
                .Parameters("prbtantoid").Value = dataHBKD0201.PropTxtPrbTantoID.Text
                .Parameters("prbtantonm").Value = dataHBKD0201.PropTxtPrbTantoNM.Text

                .Parameters("RegDT").Value = dataHBKD0201.PropDtmSysDate                         '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                  '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                          '登録者ID
                .Parameters("UpdateDT").Value = dataHBKD0201.PropDtmSysDate                      '最終更新日時
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
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>担当履歴情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/10 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectTantoRirekiSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKD0201 As DataHBKD0201) As Boolean

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
                .Add(New NpgsqlParameter("prbNmb", NpgsqlTypes.NpgsqlDbType.Integer))        '管理番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("prbNmb").Value = dataHBKD0201.PropIntPrbNmb                      '管理番号
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
    ''' <param name="dataHBKD0201">[IN]問題登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>INC共通情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/10/11 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetChkSysNmbData(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strCheckPrbSystemNmbSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("Nmb", NpgsqlTypes.NpgsqlDbType.Integer))                           '管理番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("Nmb").Value = dataHBKD0201.PropIntPrbNmb
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