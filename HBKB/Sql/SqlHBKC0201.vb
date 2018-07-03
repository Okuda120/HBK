Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' インシデント登録画面Sqlクラス
''' </summary>
''' <remarks>インシデント登録画面のSQLの作成・設定を行う
''' <para>作成情報：2012/07/13 r.hoshino
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKC0201

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
    Private strCheckIncSystemNmbSql As String = "SELECT " & vbCrLf & _
                                                " SystemNmb " & vbCrLf & _
                                                "FROM incident_info_tb ct " & vbCrLf & _
                                                "WHERE ct.Incnmb= :Nmb  "

    '[SELECT]共通情報ロックテーブル取得用SQL
    Dim strSelectINCInfoSql As String = "SELECT" & vbCrLf & _
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
                                        "FROM incident_info_lock_tb crt" & vbCrLf & _
                                        "LEFT JOIN GRP_MTB gm ON crt.EdiGrpCD=gm.GroupCD" & vbCrLf & _
                                        "LEFT JOIN HBKUSR_MTB hm ON crt.EdiID=hm.HBKUsrID" & vbCrLf & _
                                        "WHERE incnmb=:Nmb"

    '[INSERT]共通情報ロックテーブル登録用SQL
    Dim strInsertINCLockSql As String = "INSERT INTO incident_info_lock_tb" & vbCrLf & _
                                        "(incNmb,  EdiTime, EdiGrpCD, EdiID)" & vbCrLf & _
                                        "SELECT" & vbCrLf & _
                                        " ct.incNmb,  Now(), :EdiGrpCD, :EdiID" & vbCrLf & _
                                        "FROM incident_info_tb ct" & vbCrLf & _
                                        "WHERE" & vbCrLf & _
                                        " ct.incNmb = :Nmb"

    '[DELETE]共通情報ロック解除用SQL
    Dim strDeleteINCLockSql As String = "DELETE FROM incident_info_lock_tb WHERE incnmb=:Nmb"


    '[SELECT]受付手段マスタ
    '[mod] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 START
    'Private strSelectUketsukeMstSql As String = "SELECT " & vbCrLf & _
    '                                            " UketsukeWayCD " & vbCrLf & _
    '                                            ",UketsukeWayNM " & vbCrLf & _
    '                                            "FROM  uketsukeway_mtb " & vbCrLf & _
    '                                            "WHERE COALESCE(jtiFlg,'0') <>'1' " & vbCrLf & _
    '                                            "ORDER BY Sort "
    Private strSelectUketsukeMstSql As String = "SELECT " & vbCrLf & _
                                                " UketsukeWayCD " & vbCrLf & _
                                                ",UketsukeWayNM " & vbCrLf & _
                                                "FROM  uketsukeway_mtb " & vbCrLf & _
                                                "WHERE COALESCE(jtiFlg,'0') <>'1' OR UketsukeWayCD IN (SELECT UkeKbnCD FROM incident_info_tb WHERE IncNmb = :IncNmb)" & vbCrLf & _
                                                "ORDER BY jtiFlg,Sort "
    '[mod] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 END

    '[SELECT]インシデント種別マスタ
    '[mod] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 START
    'Private strSelectINCKindMstSql As String = "SELECT " & vbCrLf & _
    '                                            " inckindcd " & vbCrLf & _
    '                                            ",inckindnm " & vbCrLf & _
    '                                            "FROM  incident_kind_mtb " & vbCrLf & _
    '                                            "WHERE COALESCE(jtiFlg,'0') <>'1' " & vbCrLf & _
    '                                            "ORDER BY Sort"
    Private strSelectINCKindMstSql As String = "SELECT " & vbCrLf & _
                                                " inckindcd " & vbCrLf & _
                                                ",inckindnm " & vbCrLf & _
                                                "FROM  incident_kind_mtb " & vbCrLf & _
                                                "WHERE COALESCE(jtiFlg,'0') <>'1' OR inckindcd IN (SELECT IncKbnCD FROM incident_info_tb WHERE IncNmb = :IncNmb)" & vbCrLf & _
                                                "ORDER BY jtiFlg,Sort"
    '[mod] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 END

    '[SELECT]プロセスステータスマスタ
    Private strSelectprocessStateMstSql As String = "SELECT " & vbCrLf & _
                                                    " processstatecd " & vbCrLf & _
                                                    ",processstatenm " & vbCrLf & _
                                                    "FROM  processstate_mtb " & vbCrLf & _
                                                    "WHERE COALESCE(jtiFlg,'0') <>'1' " & vbCrLf & _
                                                    "AND processkbn = :processkbn " & vbCrLf & _
                                                    "ORDER BY Sort "

    '[SELECT]ドメインマスタ
    '[mod] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 START
    'Private strSelectDomeinMstSql As String = "SELECT " & vbCrLf & _
    '                                          " domaincd " & vbCrLf & _
    '                                          ",domainnm " & vbCrLf & _
    '                                          "FROM  domain_mtb " & vbCrLf & _
    '                                          "WHERE COALESCE(jtiFlg,'0') <>'1' " & vbCrLf & _
    '                                          "ORDER BY Sort "
    Private strSelectDomeinMstSql As String = "SELECT " & vbCrLf & _
                                              " domaincd " & vbCrLf & _
                                              ",domainnm " & vbCrLf & _
                                              "FROM  domain_mtb " & vbCrLf & _
                                              "WHERE COALESCE(jtiFlg,'0') <>'1' OR domaincd IN (SELECT DomainCD FROM incident_info_tb WHERE IncNmb = :IncNmb) " & vbCrLf & _
                                              "ORDER BY jtiFlg,Sort"
    '[mod] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 END
    '[mod] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 START
    '[SELECT]グループマスタ
    'Private strSelectTantoGpMstSql As String = "SELECT " & vbCrLf & _
    '                                         " groupcd " & vbCrLf & _
    '                                         ",groupnm " & vbCrLf & _
    '                                         "FROM  grp_mtb " & vbCrLf & _
    '                                         "WHERE COALESCE(jtiFlg,'0') <>'1' " & vbCrLf & _
    '                                         "ORDER BY Sort "
    Private strSelectTantoGpMstSql As String = "SELECT " & vbCrLf & _
                                         " groupcd " & vbCrLf & _
                                         ",groupnm " & vbCrLf & _
                                         "FROM  grp_mtb " & vbCrLf & _
                                         "WHERE COALESCE(jtiFlg,'0') <>'1' OR groupcd IN (SELECT TantoGrpCD FROM incident_info_tb WHERE IncNmb = :IncNmb) " & vbCrLf & _
                                         "ORDER BY jtiFlg,Sort"
    '[mod] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 END
    '[mod] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 START
    '[SELECT]経過種別マスタ(スプレッド)
    'Private strSelectKeikaKindMstSql As String = "SELECT " & vbCrLf & _
    '                                             " keikakindcd " & vbCrLf & _
    '                                             ",keikakindnm " & vbCrLf & _
    '                                             "FROM  keika_kind_mtb " & vbCrLf & _
    '                                             "WHERE COALESCE(jtiFlg,'0') <>'1' " & vbCrLf & _
    '                                             "ORDER BY Sort "
    Private strSelectKeikaKindMstSql As String = "SELECT " & vbCrLf & _
                                                 " keikakindcd " & vbCrLf & _
                                                 ",keikakindnm " & vbCrLf & _
                                                 "FROM  keika_kind_mtb " & vbCrLf & _
                                                 "WHERE COALESCE(jtiFlg,'0') <>'1' OR keikakindcd IN (SELECT KeikaKbnCD FROM incident_wk_rireki_tb WHERE IncNmb = :IncNmb) " & vbCrLf & _
                                                 "ORDER BY jtiFlg,Sort"
    '[mod] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 END
    '[SELECT]対象システム(スプレッドも)
    Private strSelectsystemMstSql As String = "SELECT " & vbCrLf & _
                                              " cinmb " & vbCrLf & _
                                              ",cinm AS Txt" & vbCrLf & _
                                              ",cinm " & vbCrLf & _
                                              ",class1 " & vbCrLf & _
                                              ",class2 " & vbCrLf & _
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
    Private strSelectPartnerMstSql As String = "SELECT " & vbCrLf & _
                                                 " endusrnm " & vbCrLf & _
                                                 ",endusrnmkana " & vbCrLf & _
                                                 ",endusrcompany " & vbCrLf & _
                                                 ",endusrbusyonm " & vbCrLf & _
                                                 ",endusrtel " & vbCrLf & _
                                                 ",endusrmailadd " & vbCrLf & _
                                                 "FROM  endusr_mtb " & vbCrLf & _
                                                 "WHERE endusrid = :endusrid " & vbCrLf

    '[SELECT]ユーザマスタ(担当IDEnter取得用)
    Private strSelectTantoMstSql As String = "SELECT " & vbCrLf & _
                                                 " hbkusrnm " & vbCrLf & _
                                                 ",hbkusrnmkana " & vbCrLf & _
                                                 ",hbkusrmailadd " & vbCrLf & _
                                                 ",hbkusrnmaimai " & vbCrLf & _
                                                 ",groupcd " & vbCrLf & _
                                                 "FROM  hbkusr_mtb m1 " & vbCrLf & _
                                                 "LEFT JOIN szk_mtb m2 ON m1.hbkusrid=m2.hbkusrid " & vbCrLf & _
                                                 "WHERE m1.hbkusrid = :hbkusrid " & vbCrLf
    '[SELECT]機器情報(機器情報_取得用)
    '[mod] 2016/11/09 e.okamura 設置番組/部屋文字数変更対応 START
    'Private strSelectKikiInfoSql As String = "SELECT " & vbCrLf & _
    '                                         "COALESCE(t1.SetBuil,'')  ||'/'||" & vbCrLf & _
    '                                         "COALESCE(t1.SetFloor,'')  ||'/'||" & vbCrLf & _
    '                                         "COALESCE(SubString(t1.SetBusyoNM ,1,10),'')  ||'/'||" & vbCrLf & _
    '                                         "COALESCE(SubString(t1.SetRoom,1,10),'')  ||'/'||" & vbCrLf & _
    '                                         "COALESCE(m1.cistatenm,'')  ||'/'||" & vbCrLf & _
    '                                         "CASE COALESCE(t1.kikistate,'') WHEN '' Then :kikistate1 ELSE :kikistate2 END ||'/'|| " & vbCrLf & _
    '                                         "COALESCE(t1.imageNmb,'')  ||'/'||" & vbCrLf & _
    '                                         "COALESCE('IP'||substring(m2.kikistatenm,1,1),'') ||'/'||" & vbCrLf & _
    '                                         "CASE (Select Count(*) From optsoft_tb t4 Where t1.cinmb=t4.cinmb) WHEN 0 THEN 'OP無' ELSE 'OP有' END ||'/' " & vbCrLf & _
    '                                         "FROM ci_info_tb t0 " & vbCrLf & _
    '                                         "INNER JOIN ci_sap_tb t1 ON t0.cinmb=t1.cinmb " & vbCrLf & _
    '                                         "LEFT JOIN cistate_mtb m1 ON t0.cistatuscd = m1.cistatecd " & vbCrLf & _
    '                                         "LEFT JOIN kikistate_mtb m2 ON t1.IPuseCD = m2.kikistateCD  " & vbCrLf & _
    '                                         "WHERE t0.cinmb=:cinmb" & vbCrLf & _
    '                                         "UNION " & vbCrLf & _
    '                                         "SELECT " & vbCrLf & _
    '                                         "COALESCE(t1.SetBuil,'')  ||'/'||" & vbCrLf & _
    '                                         "COALESCE(t1.SetFloor,'')  ||'/'||" & vbCrLf & _
    '                                         "COALESCE(SUBSTRING(t1.SetBusyoNM ,1,10),'') ||'/'||" & vbCrLf & _
    '                                         "COALESCE(SUBSTRING(t1.SetRoom,1,10),'')  ||'/'||" & vbCrLf & _
    '                                         "COALESCE(m1.cistatenm,'') ||'/'||" & vbCrLf & _
    '                                         "COALESCE('IP'||substring(m2.kikistatenm,1,1),'') ||'/'" & vbCrLf & _
    '                                         "FROM ci_info_tb t0 " & vbCrLf & _
    '                                         "INNER JOIN ci_buy_tb t1 ON t0.cinmb=t1.cinmb " & vbCrLf & _
    '                                         "LEFT JOIN cistate_mtb m1 ON t0.cistatuscd = m1.cistatecd " & vbCrLf & _
    '                                         "LEFT JOIN kikistate_mtb m2 ON t1.IPuseCD = m2.kikistateCD  " & vbCrLf & _
    '                                         "WHERE t0.cinmb=:cinmb"
    Private strSelectKikiInfoSql As String = "SELECT " & vbCrLf & _
                                             "COALESCE(t1.SetBuil,'')  ||'/'||" & vbCrLf & _
                                             "COALESCE(t1.SetFloor,'')  ||'/'||" & vbCrLf & _
                                             "COALESCE(SubString(t1.SetBusyoNM ,1,10),'')  ||'/'||" & vbCrLf & _
                                             "COALESCE(SubString(t1.SetRoom,1,20),'')  ||'/'||" & vbCrLf & _
                                             "COALESCE(m1.cistatenm,'')  ||'/'||" & vbCrLf & _
                                             "CASE COALESCE(t1.kikistate,'') WHEN '' Then :kikistate1 ELSE :kikistate2 END ||'/'|| " & vbCrLf & _
                                             "COALESCE(t1.imageNmb,'')  ||'/'||" & vbCrLf & _
                                             "COALESCE('IP'||substring(m2.kikistatenm,1,1),'') ||'/'||" & vbCrLf & _
                                             "CASE (Select Count(*) From optsoft_tb t4 Where t1.cinmb=t4.cinmb) WHEN 0 THEN 'OP無' ELSE 'OP有' END ||'/' " & vbCrLf & _
                                             "FROM ci_info_tb t0 " & vbCrLf & _
                                             "INNER JOIN ci_sap_tb t1 ON t0.cinmb=t1.cinmb " & vbCrLf & _
                                             "LEFT JOIN cistate_mtb m1 ON t0.cistatuscd = m1.cistatecd " & vbCrLf & _
                                             "LEFT JOIN kikistate_mtb m2 ON t1.IPuseCD = m2.kikistateCD  " & vbCrLf & _
                                             "WHERE t0.cinmb=:cinmb" & vbCrLf & _
                                             "UNION " & vbCrLf & _
                                             "SELECT " & vbCrLf & _
                                             "COALESCE(t1.SetBuil,'')  ||'/'||" & vbCrLf & _
                                             "COALESCE(t1.SetFloor,'')  ||'/'||" & vbCrLf & _
                                             "COALESCE(SUBSTRING(t1.SetBusyoNM ,1,10),'') ||'/'||" & vbCrLf & _
                                             "COALESCE(SUBSTRING(t1.SetRoom,1,20),'')  ||'/'||" & vbCrLf & _
                                             "COALESCE(m1.cistatenm,'') ||'/'||" & vbCrLf & _
                                             "COALESCE('IP'||substring(m2.kikistatenm,1,1),'') ||'/'" & vbCrLf & _
                                             "FROM ci_info_tb t0 " & vbCrLf & _
                                             "INNER JOIN ci_buy_tb t1 ON t0.cinmb=t1.cinmb " & vbCrLf & _
                                             "LEFT JOIN cistate_mtb m1 ON t0.cistatuscd = m1.cistatecd " & vbCrLf & _
                                             "LEFT JOIN kikistate_mtb m2 ON t1.IPuseCD = m2.kikistateCD  " & vbCrLf & _
                                             "WHERE t0.cinmb=:cinmb"
    '[mod] 2016/11/09 e.okamura 設置番組/部屋文字数変更対応 END

    '[SELECT]共通情報取得SQL
    Private strSelectIncMainSql As String = "SELECT " & vbCrLf & _
                                           " ct.incnmb " & vbCrLf & _
                                           ",ct.processkbn " & vbCrLf & _
                                           ",ct.ukekbncd " & vbCrLf & _
                                           ",ct.inckbncd " & vbCrLf & _
                                           ",ct.processstatecd " & vbCrLf & _
                                           ",ct.hasseidt " & vbCrLf & _
                                           ",ct.kaitodt " & vbCrLf & _
                                           ",ct.kanryodt " & vbCrLf & _
                                           ",ct.priority " & vbCrLf & _
                                           ",ct.errlevel " & vbCrLf & _
                                           ",ct.title " & vbCrLf & _
                                           ",ct.ukenaiyo " & vbCrLf & _
                                           ",ct.taiokekka " & vbCrLf & _
                                           ",ct.systemnmb " & vbCrLf & _
                                           ",ct.outsidetoolnmb " & vbCrLf & _
                                           ",ct.eventid " & vbCrLf & _
                                           ",ct.source " & vbCrLf & _
                                           ",ct.opceventid " & vbCrLf & _
                                           ",ct.eventclass " & vbCrLf & _
                                           ",ct.tantogrpcd " & vbCrLf & _
                                           ",ct.inctantoid " & vbCrLf & _
                                           ",ct.inctantonm " & vbCrLf & _
                                           ",ct.domaincd " & vbCrLf & _
                                           ",ct.partnercompany " & vbCrLf & _
                                           ",ct.partnerid " & vbCrLf & _
                                           ",ct.partnernm " & vbCrLf & _
                                           ",ct.partnerkana " & vbCrLf & _
                                           ",ct.partnerkyokunm " & vbCrLf & _
                                           ",ct.usrbusyonm " & vbCrLf & _
                                           ",ct.partnertel " & vbCrLf & _
                                           ",ct.partnermailadd " & vbCrLf & _
                                           ",ct.partnercontact " & vbCrLf & _
                                           ",ct.partnerBase " & vbCrLf & _
                                           ",ct.partnerroom " & vbCrLf & _
                                           ",ct.shijisyoflg " & vbCrLf & _
                                           ",ct.kengen " & vbCrLf & _
                                           ",ct.rentalkiki " & vbCrLf & _
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
                                           ",ct.ukenaiyoaimai " & vbCrLf & _
                                           ",ct.bikoaimai " & vbCrLf & _
                                           ",ct.taiokekkaaimai " & vbCrLf & _
                                           ",ct.eventidaimai " & vbCrLf & _
                                           ",ct.sourceaimai " & vbCrLf & _
                                           ",ct.opceventidaimai " & vbCrLf & _
                                           ",ct.eventclassaimai " & vbCrLf & _
                                           ",ct.IncTantIDAimai " & vbCrLf & _
                                           ",ct.inctantnmaimai " & vbCrLf & _
                                           ",ct.partneridaimai " & vbCrLf & _
                                           ",ct.partnernmaimai " & vbCrLf & _
                                           ",ct.usrbusyonmaimai " & vbCrLf & _
                                           ",ct.kigencondcikbncd " & vbCrLf & _
                                           ",ct.kigencondtypekbn " & vbCrLf & _
                                           ",ct.kigencondkigen " & vbCrLf & _
                                           ",ct.KigenCondUsrID " & vbCrLf & _
                                           ",ct.RegDT " & vbCrLf & _
                                           ",ct.RegGrpCD " & vbCrLf & _
                                           ",ct.RegID " & vbCrLf & _
                                           ",ct.UpdateDT " & vbCrLf & _
                                           ",ct.UpGrpCD " & vbCrLf & _
                                           ",ct.UpdateID " & vbCrLf & _
                                           ",m1.groupnm ||' '||m2.hbkusrnm||' '||to_char(ct.RegDT,'YYYY/MM/DD HH24:MI') AS LblRegInfo" & vbCrLf & _
                                           ",m3.groupnm ||' '||m4.hbkusrnm||' '||to_char(ct.UpdateDT,'YYYY/MM/DD HH24:MI') AS LblUpdateInfo" & vbCrLf & _
                                           ",m1.groupnm AS mail_RegGp " & vbCrLf & _
                                           ",m2.hbkusrnm AS mail_RegUsr " & vbCrLf & _
                                           ",to_char(ct.RegDT,'YYYY/MM/DD HH24:MI') AS mail_RegDt" & vbCrLf & _
                                           ",m3.groupnm AS mail_UpdateGp" & vbCrLf & _
                                           ",m4.hbkusrnm AS mail_UpdateUsr" & vbCrLf & _
                                           ",to_char(ct.UpdateDT,'YYYY/MM/DD HH24:MI') AS mail_UpdateDt" & vbCrLf & _
                                           "FROM incident_info_tb ct " & vbCrLf & _
                                           "LEFT JOIN  (SELECT groupnm,groupcd FROM grp_mtb UNION ALL SELECT '" & SYS_GROUPCD & "','" & SYS_GROUPCD & "') m1 ON m1.groupcd=ct.RegGrpCD " & vbCrLf & _
                                           "LEFT JOIN  (SELECT hbkusrnm,hbkusrid FROM hbkusr_mtb UNION ALL SELECT '" & SYS_USERID & "','" & SYS_USERID & "') m2 ON m2.hbkusrid=ct.RegID " & vbCrLf & _
                                           "LEFT JOIN  (SELECT groupnm,groupcd FROM grp_mtb UNION ALL SELECT '" & SYS_GROUPCD & "','" & SYS_GROUPCD & "') m3 ON m3.groupcd=ct.UpGrpCD " & vbCrLf & _
                                           "LEFT JOIN  (SELECT hbkusrnm,hbkusrid FROM hbkusr_mtb UNION ALL SELECT '" & SYS_USERID & "','" & SYS_USERID & "') m4 ON m4.hbkusrid=ct.UpdateID " & vbCrLf & _
                                           "WHERE ct.incnmb = :incnmb "

    '[SELECT]担当履歴取得SQL
    Private strSelectTantoRirekiSql As String = "SELECT " & vbCrLf & _
                                              " ct.tantorirekinmb " & vbCrLf & _
                                              ",ct.tantogrpcd " & vbCrLf & _
                                              ",ct.tantogrpnm " & vbCrLf & _
                                              ",ct.inctantoid " & vbCrLf & _
                                              ",ct.inctantonm " & vbCrLf & _
                                              "FROM incident_tanto_rireki_tb ct " & vbCrLf & _
                                              "WHERE ct.incnmb = :incnmb " & vbCrLf & _
                                              "ORDER BY ct.tantorirekinmb DESC"

    '[SELECT]作業履歴取得SQL
    Private strSelectIncRirekiSql As String = "SELECT " & vbCrLf & _
                                              " ct.workrirekinmb " & vbCrLf & _
                                              ",ct.keikakbncd " & vbCrLf & _
                                              ",ct.systemnmb " & vbCrLf & _
                                              ",ct.worknaiyo " & vbCrLf & _
                                              ",ct.workscedt " & vbCrLf & _
                                              ",ct.workstdt " & vbCrLf & _
                                              ",ct.workeddt " & vbCrLf & _
                                              "FROM incident_wk_rireki_tb ct " & vbCrLf & _
                                              "WHERE ct.incnmb = :incnmb " & vbCrLf & _
                                              "ORDER BY ct.workstdt DESC, ct.workeddt DESC, ct.workrirekinmb"



    '[SELECT]作業担当取得SQL
    Private strSelectIncTantoSql As String = "SELECT " & vbCrLf & _
                                             " t.workrirekinmb " & vbCrLf & _
                                             ",b.cnt " & vbCrLf & _
                                             ",t.worktantonmb " & vbCrLf & _
                                             ",t.worktantogrpnm " & vbCrLf & _
                                             ",t.worktantogrpcd " & vbCrLf & _
                                             ",t.worktantonm " & vbCrLf & _
                                             ",t.worktantoid " & vbCrLf & _
                                             ",t.RegDT " & vbCrLf & _
                                             ",t.RegGrpCD " & vbCrLf & _
                                             ",t.RegID " & vbCrLf & _
                                             ",t.updatedt " & vbCrLf & _
                                             ",t.upgrpcd " & vbCrLf & _
                                             ",t.updateid " & vbCrLf & _
                                             "FROM  incident_wk_tanto_tb t " & vbCrLf & _
                                             "LEFT JOIN  grp_mtb m1 ON m1.groupcd=t.worktantogrpcd " & vbCrLf & _
                                             "LEFT JOIN  hbkusr_mtb m2 ON m2.hbkusrid=t.worktantoid " & vbCrLf & _
                                             "INNER JOIN  (SELECT  workrirekinmb,COUNT(*) AS cnt  " & vbCrLf & _
                                             "  FROM incident_wk_tanto_tb t " & vbCrLf & _
                                             "  WHERE t.incnmb = :incnmb " & vbCrLf & _
                                             "  GROUP BY workrirekinmb " & vbCrLf & _
                                             ") b ON b.workrirekinmb=t.workrirekinmb " & vbCrLf & _
                                             "WHERE t.incnmb = :incnmb " & vbCrLf & _
                                             "ORDER BY t.workrirekinmb,t.worktantonmb "


    '[SELECT]機器情報取得SQL
    Private strSelectIncKikiSql As String = "SELECT " & vbCrLf & _
                                            " m1.kindnm " & vbCrLf & _
                                            ",kt.num " & vbCrLf & _
                                            ",kt.kikiinf " & vbCrLf & _
                                            ",kt.kindcd " & vbCrLf & _
                                            ",ct.CINmb" & vbCrLf & _
                                            ",ct.CIKbnCD" & vbCrLf & _
                                            ",kt.EntryNmb" & vbCrLf & _
                                            ",kt.RegDT " & vbCrLf & _
                                            ",kt.RegGrpCD " & vbCrLf & _
                                            ",kt.RegID " & vbCrLf & _
                                            ",ct.SetKikiID " & vbCrLf & _
                                            "FROM incident_kiki_tb kt " & vbCrLf & _
                                            "INNER JOIN kind_mtb m1 ON m1.kindcd=kt.kindcd " & vbCrLf & _
                                            "INNER JOIN CI_INFO_TB ct ON kt.kindcd=ct.kindcd AND kt.Num=ct.Num" & vbCrLf & _
                                            "WHERE kt.incnmb = :incnmb " & vbCrLf & _
                                            "ORDER BY kt.entrynmb  "

    '[SELECT]対応関係者取得SQL
    Private strSelectIncKankeiSql As String = "SELECT " & vbCrLf & _
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
                                              "  FROM incident_kankei_tb kt " & vbCrLf & _
                                              "   INNER JOIN GRP_MTB gm ON kt.RelationID = gm.GroupCD " & vbCrLf & _
                                              "  WHERE kt.incnmb = :incnmb " & vbCrLf & _
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
                                              "  FROM incident_kankei_tb kt " & vbCrLf & _
                                              "   INNER JOIN HBKUSR_MTB hm ON hm.HBKUsrID = kt.RelationID " & vbCrLf & _
                                              "  WHERE kt.incnmb= :incnmb  " & vbCrLf & _
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
                                          "WHERE kt1.LinkMotoNmb = :incNmb " & vbCrLf & _
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
                                          "WHERE kt2.LinkSakiNmb = :incNmb " & vbCrLf & _
                                          "AND   kt2.LinkSakiProcesskbn = :LinkMotoProcesskbn " & vbCrLf & _
                                          ") t " & vbCrLf & _
                                          "ORDER BY t.entryDT  "

    '[SELECT]関連ファイル情報取得SQL
    Private strSelectIncFileSql As String = "SELECT " & vbCrLf & _
                                            " st.filenaiyo AS FileNaiyo" & vbCrLf & _
                                            ",st.filemngnmb AS FileMngNmb " & vbCrLf & _
                                            ",m1.filepath||E'\\'||m1.filenm||m1.ext AS FilePath" & vbCrLf & _
                                            ",st.EntryNmb" & vbCrLf & _
                                            ",st.RegDT" & vbCrLf & _
                                            ",st.RegGrpCD " & vbCrLf & _
                                            ",st.RegID " & vbCrLf & _
                                            "FROM incident_file_tb st " & vbCrLf & _
                                            "INNER JOIN file_mng_tb m1 ON m1.filemngnmb=st.filemngnmb " & vbCrLf & _
                                            "WHERE st.incnmb = :incnmb " & vbCrLf & _
                                            "ORDER BY st.entrynmb "

    '[SELECT]借用物取得SQL
    Private strSelectSyakuyouSql As String = "SELECT " & vbCrLf & _
                                           " m.KindNM||t.num " & vbCrLf & _
                                           "FROM ci_info_tb t " & vbCrLf & _
                                           "INNER JOIN kind_mtb m ON t.kindcd=m.kindcd " & vbCrLf & _
                                           " AND COALESCE(m.jtiflg,'0')<>'1' " & vbCrLf & _
                                           "INNER JOIN ci_sap_tb s ON t.cinmb=s.cinmb " & vbCrLf & _
                                           " AND s.KikiUseCD = :KikiUseCD " & vbCrLf & _
                                           " AND s.UsrID = :UsrID " & vbCrLf & _
                                           "WHERE t.cikbncd = :cikbncd " & vbCrLf & _
                                           "ORDER BY t.cinmb "

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


    '[INSERT]共通情報SQL
    Private strInsertIncInfoSql As String = "INSERT INTO  incident_info_tb (" & vbCrLf & _
                                            " incnmb " & vbCrLf & _
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
                                             ") VALUES (" & vbCrLf & _
                                            " :incnmb " & vbCrLf & _
                                            ",:processkbn " & vbCrLf & _
                                            ",:ukekbncd " & vbCrLf & _
                                            ",:inckbncd " & vbCrLf & _
                                            ",:processstatecd " & vbCrLf & _
                                            ",:hasseidt " & vbCrLf & _
                                            ",:kaitodt " & vbCrLf & _
                                            ",:kanryodt " & vbCrLf & _
                                            ",:priority " & vbCrLf & _
                                            ",:errlevel " & vbCrLf & _
                                            ",:title " & vbCrLf & _
                                            ",:ukenaiyo " & vbCrLf & _
                                            ",:taiokekka " & vbCrLf & _
                                            ",:systemnmb " & vbCrLf & _
                                            ",:outsidetoolnmb " & vbCrLf & _
                                            ",:eventid " & vbCrLf & _
                                            ",:source " & vbCrLf & _
                                            ",:opceventid " & vbCrLf & _
                                            ",:eventclass " & vbCrLf & _
                                            ",:tantogrpcd " & vbCrLf & _
                                            ",:inctantoid " & vbCrLf & _
                                            ",:inctantonm " & vbCrLf & _
                                            ",:domaincd " & vbCrLf & _
                                            ",:partnercompany " & vbCrLf & _
                                            ",:partnerid " & vbCrLf & _
                                            ",:partnernm " & vbCrLf & _
                                            ",:partnerkana " & vbCrLf & _
                                            ",:partnerkyokunm " & vbCrLf & _
                                            ",:usrbusyonm " & vbCrLf & _
                                            ",:partnertel " & vbCrLf & _
                                            ",:partnermailadd " & vbCrLf & _
                                            ",:partnercontact " & vbCrLf & _
                                            ",:partnerBase " & vbCrLf & _
                                            ",:partnerroom " & vbCrLf & _
                                            ",:shijisyoflg " & vbCrLf & _
                                            ",:kengen " & vbCrLf & _
                                            ",:rentalkiki " & vbCrLf & _
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
                                            ",:ukenaiyoaimai " & vbCrLf & _
                                            ",:bikoaimai " & vbCrLf & _
                                            ",:taiokekkaaimai " & vbCrLf & _
                                            ",:eventidaimai " & vbCrLf & _
                                            ",:sourceaimai " & vbCrLf & _
                                            ",:opceventidaimai " & vbCrLf & _
                                            ",:eventclassaimai " & vbCrLf & _
                                            ",:IncTantIDAimai " & vbCrLf & _
                                            ",:inctantnmaimai " & vbCrLf & _
                                            ",:partneridaimai " & vbCrLf & _
                                            ",:partnernmaimai " & vbCrLf & _
                                            ",:usrbusyonmaimai " & vbCrLf & _
                                            ",:kigencondcikbncd " & vbCrLf & _
                                            ",:kigencondtypekbn " & vbCrLf & _
                                            ",:kigencondkigen " & vbCrLf & _
                                            ",:KigenCondUsrID " & vbCrLf & _
                                            ",:RegDT " & vbCrLf & _
                                            ",:RegGrpCD " & vbCrLf & _
                                            ",:RegID " & vbCrLf & _
                                            ",:UpdateDT " & vbCrLf & _
                                            ",:UpGrpCD " & vbCrLf & _
                                            ",:UpdateID " & vbCrLf & _
                                            ") "

    '[INSERT]担当履歴SQL
    Private strInsertTantoRirekiSql As String = "INSERT INTO incident_tanto_rireki_tb (" & vbCrLf & _
                                             " incnmb " & vbCrLf & _
                                             ",tantorirekinmb " & vbCrLf & _
                                             ",tantogrpcd " & vbCrLf & _
                                             ",tantogrpnm " & vbCrLf & _
                                             ",inctantoid " & vbCrLf & _
                                             ",inctantonm " & vbCrLf & _
                                             ",RegDT " & vbCrLf & _
                                             ",RegGrpCD " & vbCrLf & _
                                             ",RegID " & vbCrLf & _
                                             ",UpdateDT " & vbCrLf & _
                                             ",UpGrpCD " & vbCrLf & _
                                             ",UpdateID " & vbCrLf & _
                                             ") VALUES (" & vbCrLf & _
                                             " :incnmb " & vbCrLf & _
                                             ",(SELECT COALESCE(MAX(tantorirekinmb),0)+1 FROM incident_tanto_rireki_tb WHERE incnmb=:incnmb) " & vbCrLf & _
                                             ",:tantogrpcd " & vbCrLf & _
                                             ",:tantogrpnm " & vbCrLf & _
                                             ",:inctantoid " & vbCrLf & _
                                             ",:inctantonm " & vbCrLf & _
                                             ",:RegDT " & vbCrLf & _
                                             ",:RegGrpCD " & vbCrLf & _
                                             ",:RegID " & vbCrLf & _
                                             ",:UpdateDT " & vbCrLf & _
                                             ",:UpGrpCD " & vbCrLf & _
                                             ",:UpdateID " & vbCrLf & _
                                             ") "

    '[INSERT]作業履歴SQL
    Private strInsertIncRirekiSql As String = "INSERT INTO incident_wk_rireki_tb (" & vbCrLf & _
                                              " incnmb " & vbCrLf & _
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
                                              ") VALUES (" & vbCrLf & _
                                              " :incnmb " & vbCrLf & _
                                              ",(SELECT COALESCE(MAX(workrirekinmb),0)+1 FROM incident_wk_rireki_tb WHERE incnmb=:incnmb) " & vbCrLf & _
                                              ",:keikakbncd " & vbCrLf & _
                                              ",:worknaiyo " & vbCrLf & _
                                              ",:workscedt " & vbCrLf & _
                                              ",:workstdt " & vbCrLf & _
                                              ",:workeddt " & vbCrLf & _
                                              ",:systemnmb " & vbCrLf & _
                                              ",:worknaiyoaimai " & vbCrLf & _
                                              ",:RegDT " & vbCrLf & _
                                              ",:RegGrpCD " & vbCrLf & _
                                              ",:RegID " & vbCrLf & _
                                              ",:UpdateDT " & vbCrLf & _
                                              ",:UpGrpCD " & vbCrLf & _
                                              ",:UpdateID " & vbCrLf & _
                                              ") "
    '[INSERT]作業担当SQL
    Private strInsertIncTantoSql As String = "INSERT INTO incident_wk_tanto_tb (" & vbCrLf & _
                                             " incnmb " & vbCrLf & _
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
                                             " :incnmb " & vbCrLf & _
                                             ",(SELECT COALESCE(MAX(workrirekinmb),0) FROM incident_wk_rireki_tb WHERE IncNmb=:IncNmb) " & vbCrLf & _
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
    Private strUpdateIncTantoSql As String = "INSERT INTO incident_wk_tanto_tb (" & vbCrLf & _
                                             " incnmb " & vbCrLf & _
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
                                             " :incnmb " & vbCrLf & _
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

    '[INSERT]機器情報SQL
    Private strInsertInckikiSql As String = "INSERT INTO incident_kiki_tb (" & vbCrLf & _
                                            " incnmb " & vbCrLf & _
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
                                            ") VALUES (" & vbCrLf & _
                                            " :incnmb " & vbCrLf & _
                                            ",:kindcd " & vbCrLf & _
                                            ",:num " & vbCrLf & _
                                            ",:kikiinf " & vbCrLf & _
                                            ",(SELECT COALESCE(MAX(EntryNmb),0)+1 FROM incident_kiki_tb WHERE IncNmb=:IncNmb) " & vbCrLf & _
                                            ",:RegDT " & vbCrLf & _
                                            ",:RegGrpCD " & vbCrLf & _
                                            ",:RegID " & vbCrLf & _
                                            ",:UpdateDT " & vbCrLf & _
                                            ",:UpGrpCD " & vbCrLf & _
                                            ",:UpdateID " & vbCrLf & _
                                            ") "


    '[INSERT]対応関係者SQL
    Private strInsertIncKankeiSql As String = "INSERT INTO incident_kankei_tb ( " & vbCrLf & _
                                              " incnmb " & vbCrLf & _
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
                                              " :incnmb " & vbCrLf & _
                                              ",:RelationKbn " & vbCrLf & _
                                              ",:RelationID " & vbCrLf & _
                                              ",(SELECT COALESCE(MAX(EntryNmb),0)+1 FROM incident_kankei_tb WHERE IncNmb=:IncNmb) " & vbCrLf & _
                                              ",:RegDT " & vbCrLf & _
                                              ",:RegGrpCD " & vbCrLf & _
                                              ",:RegID " & vbCrLf & _
                                              ",:UpdateDT " & vbCrLf & _
                                              ",:UpGrpCD " & vbCrLf & _
                                              ",:UpdateID " & vbCrLf & _
                                              ") "



    '[INSERT]プロセスリンクSQL
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
                                                " :LinkmotoProcesskbn " & vbCrLf & _
                                                ",:LinkMotoNmb " & vbCrLf & _
                                                ",:LinkSakiProcesskbn " & vbCrLf & _
                                                ",:LinkSakiNmb " & vbCrLf & _
                                                ",:EntryDT" & vbCrLf & _
                                                ",:RegDT " & vbCrLf & _
                                                ",:RegGrpCD " & vbCrLf & _
                                                ",:RegID " & vbCrLf & _
                                                ",:UpdateDT " & vbCrLf & _
                                                ",:UpGrpCD " & vbCrLf & _
                                                ",:UpdateID " & vbCrLf & _
                                                ") "



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


    '[SELECT]システム日付取得SQL
    Private strSelectSysDateSql As String = "SELECT Now() AS SysDate "

    '[UPDATE]共通情報SQL
    Private strUpdateIncInfoSql As String = "UPDATE incident_info_tb SET " & vbCrLf & _
                                            " processkbn =       :processkbn " & vbCrLf & _
                                            ",ukekbncd =         :ukekbncd " & vbCrLf & _
                                            ",inckbncd =         :inckbncd " & vbCrLf & _
                                            ",processstatecd =   :processstatecd " & vbCrLf & _
                                            ",hasseidt =         :hasseidt " & vbCrLf & _
                                            ",kaitodt =          :kaitodt " & vbCrLf & _
                                            ",kanryodt =         :kanryodt " & vbCrLf & _
                                            ",priority =         :priority " & vbCrLf & _
                                            ",errlevel =         :errlevel " & vbCrLf & _
                                            ",title =            :title " & vbCrLf & _
                                            ",ukenaiyo =         :ukenaiyo " & vbCrLf & _
                                            ",taiokekka =        :taiokekka " & vbCrLf & _
                                            ",systemnmb =        :systemnmb " & vbCrLf & _
                                            ",outsidetoolnmb =   :outsidetoolnmb " & vbCrLf & _
                                            ",eventid =          :eventid " & vbCrLf & _
                                            ",source =           :source " & vbCrLf & _
                                            ",opceventid =       :opceventid " & vbCrLf & _
                                            ",eventclass =       :eventclass " & vbCrLf & _
                                            ",tantogrpcd =       :tantogrpcd " & vbCrLf & _
                                            ",inctantoid =       :inctantoid " & vbCrLf & _
                                            ",inctantonm =       :inctantonm " & vbCrLf & _
                                            ",domaincd =         :domaincd " & vbCrLf & _
                                            ",partnercompany =   :partnercompany " & vbCrLf & _
                                            ",partnerid =        :partnerid " & vbCrLf & _
                                            ",partnernm =        :partnernm " & vbCrLf & _
                                            ",partnerkana =      :partnerkana " & vbCrLf & _
                                            ",partnerkyokunm =   :partnerkyokunm " & vbCrLf & _
                                            ",usrbusyonm =       :usrbusyonm " & vbCrLf & _
                                            ",partnertel =       :partnertel " & vbCrLf & _
                                            ",partnermailadd =   :partnermailadd " & vbCrLf & _
                                            ",partnercontact =   :partnercontact " & vbCrLf & _
                                            ",partnerBase =      :partnerBase " & vbCrLf & _
                                            ",partnerroom =      :partnerroom " & vbCrLf & _
                                            ",shijisyoflg =      :shijisyoflg " & vbCrLf & _
                                            ",kengen =           :kengen " & vbCrLf & _
                                            ",rentalkiki =       :rentalkiki " & vbCrLf & _
                                            ",BIko1 =            :BIko1 " & vbCrLf & _
                                            ",Biko2 =            :Biko2 " & vbCrLf & _
                                            ",Biko3 =            :Biko3 " & vbCrLf & _
                                            ",Biko4 =            :Biko4 " & vbCrLf & _
                                            ",Biko5 =            :Biko5 " & vbCrLf & _
                                            ",FreeFlg1 =         :FreeFlg1 " & vbCrLf & _
                                            ",FreeFlg2 =         :FreeFlg2 " & vbCrLf & _
                                            ",FreeFlg3 =         :FreeFlg3 " & vbCrLf & _
                                            ",FreeFlg4 =         :FreeFlg4 " & vbCrLf & _
                                            ",FreeFlg5 =         :FreeFlg5 " & vbCrLf & _
                                            ",titleaimai =       :titleaimai " & vbCrLf & _
                                            ",ukenaiyoaimai =    :ukenaiyoaimai " & vbCrLf & _
                                            ",bikoaimai =        :bikoaimai " & vbCrLf & _
                                            ",taiokekkaaimai =   :taiokekkaaimai " & vbCrLf & _
                                            ",eventidaimai =     :eventidaimai " & vbCrLf & _
                                            ",sourceaimai =      :sourceaimai " & vbCrLf & _
                                            ",opceventidaimai =  :opceventidaimai " & vbCrLf & _
                                            ",eventclassaimai =  :eventclassaimai " & vbCrLf & _
                                            ",IncTantIDAimai =   :IncTantIDAimai " & vbCrLf & _
                                            ",inctantnmaimai =   :inctantnmaimai " & vbCrLf & _
                                            ",partneridaimai =   :partneridaimai " & vbCrLf & _
                                            ",partnernmaimai =   :partnernmaimai " & vbCrLf & _
                                            ",usrbusyonmaimai =  :usrbusyonmaimai " & vbCrLf & _
                                            ",kigencondcikbncd = :kigencondcikbncd " & vbCrLf & _
                                            ",kigencondtypekbn = :kigencondtypekbn " & vbCrLf & _
                                            ",kigencondkigen =   :kigencondkigen " & vbCrLf & _
                                            ",KigenCondUsrID =   :KigenCondUsrID " & vbCrLf & _
                                            ",UpdateDT =         :UpdateDT " & vbCrLf & _
                                            ",UpGrpCD =          :UpGrpCD " & vbCrLf & _
                                            ",UpdateID =         :UpdateID " & vbCrLf & _
                                            "WHERE incnmb =:incnmb "

    '[UPDATE]作業履歴SQL
    Private strUpdateIncRirekiSql As String = "UPDATE incident_wk_rireki_tb SET" & vbCrLf & _
                                              " keikakbncd    =:keikakbncd     " & vbCrLf & _
                                              ",worknaiyo     =:worknaiyo      " & vbCrLf & _
                                              ",workscedt     =:workscedt      " & vbCrLf & _
                                              ",workstdt      =:workstdt       " & vbCrLf & _
                                              ",workeddt      =:workeddt       " & vbCrLf & _
                                              ",systemnmb     =:systemnmb      " & vbCrLf & _
                                              ",worknaiyoaimai=:worknaiyoaimai " & vbCrLf & _
                                              ",UpdateDT      =:UpdateDT       " & vbCrLf & _
                                              ",UpGrpCD       =:UpGrpCD        " & vbCrLf & _
                                              ",UpdateID      =:UpdateID       " & vbCrLf & _
                                              "WHERE incnmb =:incnmb " & vbCrLf & _
                                              "AND workrirekinmb=:workrirekinmb "


    '[DELETE]作業担当SQL
    Private strDeleteIncTantoSql As String = "DELETE FROM incident_wk_tanto_tb " & vbCrLf & _
                                             "WHERE incnmb=:incnmb " & vbCrLf & _
                                             "AND workrirekinmb=:workrirekinmb "
 
    '[DELETE]機器情報SQL
    Private strDeleteIncKikiSql As String = "DELETE FROM incident_kiki_tb " & vbCrLf & _
                                            "WHERE incnmb=:incnmb " 
    '[DELETE]対応関係者SQL
    Private strDeleteIncKankeiSql As String = "DELETE FROM incident_kankei_tb " & vbCrLf & _
                                              "WHERE incnmb=:incnmb "

    '[DELETE]会議結果情報ファイルSQL
    Private strDeleteMtgResultSql As String = "DELETE FROM meeting_result_tb " & vbCrLf & _
                                              "WHERE processnmb=:processnmb " & vbCrLf & _
                                              "AND processkbn=:processkbn " & vbCrLf & _
                                              "AND meetingnmb=:meetingnmb "

    '[DELETE]プロセスリンクSQL
    Private strDeletePLinkSql As String = "DELETE FROM process_link_tb " & vbCrLf & _
                                          "WHERE LinkMotoNmb=       :LinkMotoNmb " & vbCrLf & _
                                          "AND LinkMotoProcesskbn=  :LinkMotoProcesskbn " & vbCrLf & _
                                          "AND LinkSakiNmb=         :LinkSakiNmb" & vbCrLf & _
                                          "AND LinkSakiProcesskbn=  :LinkSakiProcesskbn "

    '新規ログNo取得（SELECT）SQL
    Private strSelectNewRirekiNoSql As String = "SELECT " & vbCrLf & _
                                                "COALESCE(MAX(ct.logno),0)+1 AS LogNo " & vbCrLf & _
                                                "FROM incident_info_ltb ct " & vbCrLf & _
                                                "WHERE ct.incnmb=:incnmb "
    '新規ログNo（会議用）取得（SELECT）SQL
    Private strSelectNewMeetingRirekiNoSql As String = "SELECT " & vbCrLf & _
                                                       " COALESCE(MAX(ML.LogNo),0)+1 AS LogNo " & vbCrLf & _
                                                       "FROM MEETING_LTB ML " & vbCrLf & _
                                                       "WHERE ML.MeetingNmb = :MeetingNmb "

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


    '【MOD】2012/07/31 t.fukuo サポセン機器情報機能組込：START
    ''⑧	サポセン機器メンテナンスログテーブル
    'Private strInsertSapMainteLSql As String = "INSERT INTO SAP_MAINTE_LTB ( " & vbCrLf & _
    '                                           " IncNmb " & vbCrLf & _
    '                                           ",LogNo " & vbCrLf & _
    '                                           ",WorkNmb " & vbCrLf & _
    '                                           ",WorkCD " & vbCrLf & _
    '                                           ",CINmb " & vbCrLf & _
    '                                           ",KindCD " & vbCrLf & _
    '                                           ",Num " & vbCrLf & _
    '                                           ",ChgFlg " & vbCrLf & _
    '                                           ",WorkBiko " & vbCrLf & _
    '                                           ",WorkSceDT " & vbCrLf & _
    '                                           ",WorkCompDT " & vbCrLf & _
    '                                           ",CompFlg " & vbCrLf & _
    '                                           ",CancelFLg " & vbCrLf & _
    '                                           ",RegDT " & vbCrLf & _
    '                                           ",RegGrpCD " & vbCrLf & _
    '                                           ",RegID " & vbCrLf & _
    '                                           ",UpdateDT " & vbCrLf & _
    '                                           ",UpGrpCD " & vbCrLf & _
    '                                           ",UpdateID " & vbCrLf & _
    '                                           ") " & vbCrLf & _
    '                                           "SELECT" & vbCrLf & _
    '                                           " st.IncNmb " & vbCrLf & _
    '                                           ",:LogNo " & vbCrLf & _
    '                                           ",st.WorkNmb " & vbCrLf & _
    '                                           ",st.WorkCD " & vbCrLf & _
    '                                           ",st.CINmb " & vbCrLf & _
    '                                           ",st.KindCD " & vbCrLf & _
    '                                           ",st.Num " & vbCrLf & _
    '                                           ",st.ChgFlg " & vbCrLf & _
    '                                           ",st.WorkBiko " & vbCrLf & _
    '                                           ",st.WorkSceDT " & vbCrLf & _
    '                                           ",st.WorkCompDT " & vbCrLf & _
    '                                           ",st.CompFlg " & vbCrLf & _
    '                                           ",st.CancelFLg " & vbCrLf & _
    '                                           ",:RegDT " & vbCrLf & _
    '                                           ",:RegGrpCD " & vbCrLf & _
    '                                           ",:RegID " & vbCrLf & _
    '                                           ",:UpdateDT " & vbCrLf & _
    '                                           ",:UpGrpCD " & vbCrLf & _
    '                                           ",:UpdateID " & vbCrLf & _
    '                                           "FROM SAP_MAINTE_TB st" & vbCrLf & _
    '                                           "WHERE st.IncNmb = :IncNmb" & vbCrLf

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
    '【MOD】2012/07/31 t.fukuo サポセン機器情報機能組込：END

    '会議情報ログ新規登録（INSERT）SQL
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

    '会議情報結果ログテーブル         
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


    '[SELECT]対象システムにおける対応関係者存在チェック用SQL
    Private strCheckSysKankeiUSql As String = "SELECT " & vbCrLf & _
                                              " kt.relationkbn " & vbCrLf & _
                                              ",kt.relationid " & vbCrLf & _
                                              "FROM kankei_tb kt " & vbCrLf & _
                                              "WHERE kt.CInmb = :incNmb  "



    '作業マスタ取得 ※導入・一括更新は除く
    Private strSelectWorkMtbSql As String = "SELECT" & vbCrLf & _
                                            "  WorkCD" & vbCrLf & _
                                            " ,WorkNM" & vbCrLf & _
                                            "FROM WORK_MTB" & vbCrLf & _
                                            "WHERE COALESCE(JtiFlg,'0') <>'1'" & vbCrLf & _
                                            "ORDER BY Sort" & vbCrLf & _
                                            "OFFSET 2" & vbCrLf


    ''サポセン機器メンテナンス取得
    'Private strSelectSapMainteSql As String = "SELECT *" & vbCrLf & _
    '                                          "      ,ROW_NUMBER() OVER(PARTITION BY t.WorkCD ORDER BY CASE WHEN t.SetKikiID IS NULL THEN t.WorkNmb ELSE t.SetKikiID END, t.Sort Desc, t.RowNmb) AS WorkGroupNo" & vbCrLf & _
    '                                          "FROM (" & vbCrLf & _
    '                                              "SELECT" & vbCrLf & _
    '                                              "  'False' AS Select" & vbCrLf & _
    '                                              " ,wm.WorkNM" & vbCrLf & _
    '                                              " ,skt.ChgNmb" & vbCrLf & _
    '                                              " ,km.KindNM" & vbCrLf & _
    '                                              " ,ct.Num" & vbCrLf & _
    '                                              " ,ct.Class2" & vbCrLf & _
    '                                              " ,ct.CINM" & vbCrLf & _
    '                                              " ,CASE skt.CepalateFlg WHEN '" & CEPALATEFLG_ON & "' THEN '" & CEPALATEFLG_ON_VW & "' ELSE '" & CEPALATEFLG_OFF_VW & "' END AS CepalateFlg" & vbCrLf & _
    '                                              " ,swt.WorkBiko" & vbCrLf & _
    '                                              " ,CASE COALESCE(swt.WorkSceDT,'') WHEN '' THEN NULL ELSE TO_DATE(swt.WorkSceDT,'YYYYMMDD') END AS WorkSceDT" & vbCrLf & _
    '                                              " ,CASE COALESCE(swt.WorkCompDT,'') WHEN '' THEN NULL ELSE TO_DATE(swt.WorkCompDT,'YYYYMMDD') END AS WorkCompDT" & vbCrLf & _
    '                                              " ,CASE swt.CompFlg WHEN '" & COMP_FLG_ON & "' THEN 'True' ELSE 'False' END AS CompFlg" & vbCrLf & _
    '                                              " ,CASE swt.CancelFLg WHEN '" & CANCEL_FLG_ON & "' THEN 'True' ELSE 'False' END AS CancelFlg" & vbCrLf & _
    '                                              " ,ct.KindCD" & vbCrLf & _
    '                                              " ,swt.WorkNmb" & vbCrLf & _
    '                                              " ,skt.CINmb" & vbCrLf & _
    '                                              " ,swt.WorkCD" & vbCrLf & _
    '                                              " ,km.SetupFlg" & vbCrLf & _
    '                                              " ,'' AS DoExchangeFlg" & vbCrLf & _
    '                                              " ,ct.SetKikiID AS SetKikiID" & vbCrLf & _
    '                                              " ,CASE WHEN swt.CompFlg = '" & COMP_FLG_ON & "' OR swt.CancelFLg = '" & CANCEL_FLG_ON & "' THEN 'True' ELSE 'False' END AS CompCancelZumiFlg" & vbCrLf & _
    '                                              " ,skt.RegRirekiNo" & vbCrLf & _
    '                                              " ,skt.LastUpRirekiNo" & vbCrLf & _
    '                                              " ,skt.RowNmb" & vbCrLf & _
    '                                              " ,0 AS SetRegMode" & vbCrLf & _
    '                                              " ,'False' AS ChgFlg" & vbCrLf & _
    '                                              " ,'' AS DoSetPairFlg" & vbCrLf & _
    '                                              " ,'' AS DoAddPairFlg" & vbCrLf & _
    '                                              " ,'' AS DoCepalateThisFlg" & vbCrLf & _
    '                                              " ,'' AS DoCepalatePairFlg" & vbCrLf & _
    '                                              " ,True AS Sort" & vbCrLf & _
    '                                              "FROM SAP_MAINTE_WORK_TB swt" & vbCrLf & _
    '                                              "LEFT JOIN SAP_MAINTE_KIKI_TB skt ON swt.IncNmb = skt.IncNmb AND swt.WorkNmb = skt.WorkNmb" & vbCrLf & _
    '                                              "LEFT JOIN CI_INFO_RTB ct ON skt.CINmb = ct.CINmb AND skt.LastUpRirekiNo = ct.RirekiNo" & vbCrLf & _
    '                                              "LEFT JOIN WORK_MTB wm ON swt.WorkCD = wm.WorkCD" & vbCrLf & _
    '                                              "LEFT JOIN KIND_MTB km ON ct.KindCD = km.KindCD" & vbCrLf & _
    '                                              "WHERE swt.IncNmb = :IncNmb" & vbCrLf & _
    '                                              "UNION ALL" & vbCrLf & _
    '                                              "SELECT" & vbCrLf & _
    '                                              "  'False' AS Select" & vbCrLf & _
    '                                              " ,'' AS WorkNM" & vbCrLf & _
    '                                              " ,NULL AS ChgNmb" & vbCrLf & _
    '                                              " ,km.KindNM" & vbCrLf & _
    '                                              " ,ct.Num" & vbCrLf & _
    '                                              " ,ct.Class2" & vbCrLf & _
    '                                              " ,ct.CINM" & vbCrLf & _
    '                                              " ,'' AS CepalateFlg" & vbCrLf & _
    '                                              " ,'' AS WorkBiko" & vbCrLf & _
    '                                              " ,NULL AS WorkSceDT" & vbCrLf & _
    '                                              " ,NULL AS WorkCompDT" & vbCrLf & _
    '                                              " ,'False' AS CompFlg" & vbCrLf & _
    '                                              " ,'False' AS CancelFlg" & vbCrLf & _
    '                                              " ,ct.KindCD" & vbCrLf & _
    '                                              " ,NULL AS WorkNmb" & vbCrLf & _
    '                                              " ,ct.CINmb" & vbCrLf & _
    '                                              " ,'' AS WorkCD" & vbCrLf & _
    '                                              " ,km.SetupFlg" & vbCrLf & _
    '                                              " ,'' AS DoExchangeFlg" & vbCrLf & _
    '                                              " ,ct.SetKikiID AS SetKikiID" & vbCrLf & _
    '                                              " ,'False' AS CompCancelZumiFlg" & vbCrLf & _
    '                                              " ,NULL AS RegRirekiNo" & vbCrLf & _
    '                                              " ,NULL AS LastUpRirekiNo" & vbCrLf & _
    '                                              " ,NULL AS RowNmb" & vbCrLf & _
    '                                              " ,0 AS SetRegMode" & vbCrLf & _
    '                                              " ,'False' AS ChgFlg" & vbCrLf & _
    '                                              " ,'' AS DoSetPairFlg" & vbCrLf & _
    '                                              " ,'' AS DoAddPairFlg" & vbCrLf & _
    '                                              " ,'' AS DoCepalateThisFlg" & vbCrLf & _
    '                                              " ,'' AS DoCepalatePairFlg" & vbCrLf & _
    '                                              " ,False AS Sort" & vbCrLf & _
    '                                              "FROM CI_INFO_TB ct" & vbCrLf & _
    '                                              "LEFT JOIN KIND_MTB km ON ct.KindCD = km.KindCD" & vbCrLf & _
    '                                              "WHERE ct.CINmb IN (" & vbCrLf & _
    '                                              "        SELECT CINmb FROM SET_KIKI_MNG_TB WHERE SetKikiID IN (" & vbCrLf & _
    '                                              "          (SELECT DISTINCT SetKikiID" & vbCrLf & _
    '                                              "           FROM set_kiki_mng_tb skm" & vbCrLf & _
    '                                              "           LEFT JOIN SAP_MAINTE_KIKI_TB smk ON skm.CINmb = smk.CINmb" & vbCrLf & _
    '                                              "           LEFT JOIN SAP_MAINTE_WORK_TB smw ON smk.IncNmb = smw.IncNmb AND smk.WorkNmb = smw.WorkNmb" & vbCrLf & _
    '                                              "           WHERE smk.IncNmb = :IncNmb" & vbCrLf & _
    '                                              "             AND smw.WorkCD IN ('" & WORK_CD_SET & "','" & WORK_CD_ADDCONFIG & "')" & vbCrLf & _
    '                                              "          )" & vbCrLf & _
    '                                              "        )" & vbCrLf & _
    '                                              "EXCEPT" & vbCrLf & _
    '                                              "SELECT t.CINmb FROM SET_KIKI_MNG_TB t LEFT JOIN SAP_MAINTE_KIKI_TB t2 ON t.CINmb = t2.CINmb WHERE SetKikiID IN (" & vbCrLf & _
    '                                              "          (SELECT DISTINCT SetKikiID FROM set_kiki_mng_tb skm LEFT JOIN SAP_MAINTE_KIKI_TB smk ON skm.CINmb = smk.CINmb WHERE smk.IncNmb = :IncNmb)" & vbCrLf & _
    '                                              "        )" & vbCrLf & _
    '                                              "        AND t2.IncNmb = :IncNmb" & vbCrLf & _
    '                                              " )" & vbCrLf & _
    '                                          ")t" & vbCrLf & _
    '                                          "ORDER BY CASE WHEN t.SetKikiID IS NULL THEN t.WorkNmb ELSE t.SetKikiID END, t.Sort Desc, t.RowNmb" & vbCrLf

    '★サポセン機器メンテナンス取得
    '【MOD】2014/04/07 e.okamura 作業取消時セット機器更新修正(SELECT句にBefCIStateCDの取得追加)
    Private strSelectSapMainteSql As String = "SELECT *" & vbCrLf & _
                                              "      ,ROW_NUMBER() OVER(PARTITION BY t.WorkCD ORDER BY CASE WHEN t.SetKikiID IS NULL THEN t.WorkNmb ELSE t.SetKikiID END, t.Sort Desc, t.RowNmb) AS WorkGroupNo" & vbCrLf & _
                                              "FROM (" & vbCrLf & _
                                                  "SELECT" & vbCrLf & _
                                                  "  'False' AS Select" & vbCrLf & _
                                                  " ,wm.WorkNM" & vbCrLf & _
                                                  " ,skt.ChgNmb" & vbCrLf & _
                                                  " ,km.KindNM" & vbCrLf & _
                                                  " ,ct.Num" & vbCrLf & _
                                                  " ,ct.Class2" & vbCrLf & _
                                                  " ,ct.CINM" & vbCrLf & _
                                                  " ,CASE skt.CepalateFlg WHEN '" & CEPALATEFLG_ON & "' THEN '" & CEPALATEFLG_ON_VW & "' ELSE '" & CEPALATEFLG_OFF_VW & "' END AS CepalateFlg" & vbCrLf & _
                                                  " ,swt.WorkBiko" & vbCrLf & _
                                                  " ,CASE COALESCE(swt.WorkSceDT,'') WHEN '' THEN NULL ELSE TO_DATE(swt.WorkSceDT,'YYYYMMDD') END AS WorkSceDT" & vbCrLf & _
                                                  " ,CASE COALESCE(swt.WorkCompDT,'') WHEN '' THEN NULL ELSE TO_DATE(swt.WorkCompDT,'YYYYMMDD') END AS WorkCompDT" & vbCrLf & _
                                                  " ,CASE swt.CompFlg WHEN '" & COMP_FLG_ON & "' THEN 'True' ELSE 'False' END AS CompFlg" & vbCrLf & _
                                                  " ,CASE swt.CancelFLg WHEN '" & CANCEL_FLG_ON & "' THEN 'True' ELSE 'False' END AS CancelFlg" & vbCrLf & _
                                                  " ,ct.KindCD" & vbCrLf & _
                                                  " ,swt.WorkNmb" & vbCrLf & _
                                                  " ,skt.CINmb" & vbCrLf & _
                                                  " ,swt.WorkCD" & vbCrLf & _
                                                  " ,km.SetupFlg" & vbCrLf & _
                                                  " ,(SELECT crt2.CIStatusCD" & vbCrLf & _
                                                  "   FROM hbk.SAP_MAINTE_KIKI_TB skt2" & vbCrLf & _
                                                  "   LEFT JOIN hbk.CI_INFO_RTB crt2 ON skt2.CINmb = crt2.CINmb AND skt2.RegRirekiNo = crt2.RirekiNo" & vbCrLf & _
                                                  "   WHERE skt2.IncNmb = skt.IncNmb" & vbCrLf & _
                                                  "     AND skt2.WorkNmb = skt.WorkNmb" & vbCrLf & _
                                                  "     AND skt2.CINmb = skt.CINmb" & vbCrLf & _
                                                  "  ) AS BefCIStateCD" & vbCrLf & _
                                                  " ,'' AS DoExchangeFlg" & vbCrLf & _
                                                  " ,ct.SetKikiID AS SetKikiID" & vbCrLf & _
                                                  " ,CASE WHEN swt.CompFlg = '" & COMP_FLG_ON & "' OR swt.CancelFLg = '" & CANCEL_FLG_ON & "' THEN 'True' ELSE 'False' END AS CompCancelZumiFlg" & vbCrLf & _
                                                  " ,skt.RegRirekiNo" & vbCrLf & _
                                                  " ,skt.LastUpRirekiNo" & vbCrLf & _
                                                  " ,skt.RowNmb" & vbCrLf & _
                                                  " ,0 AS SetRegMode" & vbCrLf & _
                                                  " ,'False' AS ChgFlg" & vbCrLf & _
                                                  " ,'' AS DoSetPairFlg" & vbCrLf & _
                                                  " ,'' AS DoAddPairFlg" & vbCrLf & _
                                                  " ,'' AS DoCepalateThisFlg" & vbCrLf & _
                                                  " ,'' AS DoCepalatePairFlg" & vbCrLf & _
                                                  " ,True AS Sort" & vbCrLf & _
                                                  " ,r1.setkikiid As setkikiid_1" & vbCrLf & _
                                                  " ,ct.setkikiid As setkikiid_2" & vbCrLf & _
                                                  "FROM SAP_MAINTE_WORK_TB swt" & vbCrLf & _
                                                  "LEFT JOIN SAP_MAINTE_KIKI_TB skt ON swt.IncNmb = skt.IncNmb AND swt.WorkNmb = skt.WorkNmb" & vbCrLf & _
                                                  "LEFT JOIN CI_INFO_RTB ct ON skt.CINmb = ct.CINmb AND skt.LastUpRirekiNo = ct.RirekiNo" & vbCrLf & _
                                                  "LEFT JOIN WORK_MTB wm ON swt.WorkCD = wm.WorkCD" & vbCrLf & _
                                                  "LEFT JOIN KIND_MTB km ON ct.KindCD = km.KindCD" & vbCrLf & _
                                                  "LEFT JOIN CI_INFO_RTB r1 on r1.cinmb=skt.cinmb AND skt.RegRirekiNo+1 = r1.RirekiNo" & vbCrLf & _
                                                  "WHERE swt.IncNmb = :IncNmb" & vbCrLf & _
                                              ")t" & vbCrLf & _
                                              "ORDER BY CASE WHEN t.SetKikiID IS NULL THEN t.WorkNmb ELSE t.SetKikiID END, t.Sort Desc, t.RowNmb" & vbCrLf


    '新規CI履歴番号取得 
    Private strSelectNewCIRirekiNoSql As String = "SELECT" & vbCrLf & _
                                                  " COALESCE(MAX(ct.RirekiNo),0)+1 AS RirekiNo" & vbCrLf & _
                                                  "FROM CI_INFO_RTB ct " & vbCrLf & _
                                                  "WHERE ct.CINmb = :CINmb"

    '入力チェック用：セット機器件数取得
    Private strCountSetKikiSql As String = "SELECT COUNT(1) AS Count" & vbCrLf & _
                                           "FROM CI_INFO_TB ct " & vbCrLf & _
                                           "WHERE ct.SetKikiID IN (SELECT ct2.SetKikiID FROM CI_INFO_TB ct2 WHERE ct2.CINmb = :CINmb)"

    '入力チェック用：CIサポセン機器.イメージ番号未入力データ件数取得
    Private strCountImgNmbIsNotNullSql As String = "SELECT 1 AS Count" & vbCrLf & _
                                                   "FROM CI_SAP_TB ct " & vbCrLf & _
                                                   "WHERE ct.CINmb   = :CINmb" & vbCrLf & _
                                                   "  AND COALESCE(ct.ImageNmb,'') <> ''" & vbCrLf

    '入力チェック用：CIサポセン機器.機器状態未入力データ件数取得
    Private strCountKikiStateIsNotNullSql As String = "SELECT COUNT(1) AS Count" & vbCrLf & _
                                                      "FROM CI_SAP_TB ct " & vbCrLf & _
                                                      "WHERE ct.CINmb   = :CINmb" & vbCrLf & _
                                                      "  AND COALESCE(ct.KikiState,'') <> ''" & vbCrLf

    '入力チェック用：CIサポセン機器入力チェック項目取得
    Private strSelectCheckCISapKmkSql As String = "SELECT" & vbCrLf & _
                                                  "  ct.CIStatusCD AS TmpCIStateCD" & vbCrLf & _
                                                  " ,cst.KikiUseCD" & vbCrLf & _
                                                  " ,cst.UsrID" & vbCrLf & _
                                                  " ,cst.UsrNM" & vbCrLf & _
                                                  " ,cst.UsrMailAdd" & vbCrLf & _
                                                  " ,cst.UsrBusyoNM" & vbCrLf & _
                                                  " ,ct.KindCD" & vbCrLf & _
                                                  " ,cst.RentalStDT" & vbCrLf & _
                                                  " ,cst.RentalEdDT" & vbCrLf & _
                                                  " ,cst.WorkFromNmb" & vbCrLf & _
                                                  " ,cst.IPUseCD" & vbCrLf & _
                                                  " ,cst.ManageBusyoNM" & vbCrLf & _
                                                  " ,cst.SetBusyoNM" & vbCrLf & _
                                                  "FROM CI_SAP_TB cst " & vbCrLf & _
                                                  "JOIN CI_INFO_TB ct ON cst.CINmb = ct.CINmb" & vbCrLf & _
                                                  "WHERE ct.CINmb = :CINmb" & vbCrLf

    '【ADD】2014/04/07 e.okamura 作業取消時セット機器更新修正 START
    '警告メッセージ用：現在のセット機器取得
    Private strSelectCurrentSetKikiSql As String = "SELECT" & vbCrLf & _
                                                   "  t.CINmb" & vbCrLf & _
                                                   "FROM (" & vbCrLf & _
                                                   "  SELECT" & vbCrLf & _
                                                   "   skt.SetKikiID" & vbCrLf & _
                                                   "  ,ct.CINmb" & vbCrLf & _
                                                   "  FROM SET_KIKI_MNG_TB skt " & vbCrLf & _
                                                   "  LEFT JOIN CI_INFO_TB ct ON ct.SetKikiID = skt.SetKikiID" & vbCrLf & _
                                                   "  WHERE skt.SetKikiID = (SELECT SetKikiID From CI_INFO_TB WHERE CINmb = :CINmb) " & vbCrLf & _
                                                   ") t" & vbCrLf & _
                                                   "GROUP BY t.CINmb" & vbCrLf & _
                                                   "ORDER BY t.CINmb"

    '警告メッセージ用：作業追加時のセット機器取得
    Private strSelectPastSetKikiSql As String = "SELECT skt.setCINmb" & vbCrLf & _
                                                "FROM SETKIKI_RTB skt " & vbCrLf & _
                                                "WHERE skt.CINmb = :CINmb AND skt.RirekiNo = :RirekiNo " & vbCrLf & _
                                                "GROUP BY skt.setCINmb " & vbCrLf & _
                                                "ORDER BY skt.setCINmb"
    '【ADD】2014/04/07 e.okamura 作業取消時セット機器更新修正 END

    '更新値設定用：CIステータス取得
    Private strSelectCIStateCDSql As String = "SELECT" & vbCrLf & _
                                              "  ct.CIStatusCD AS TmpCIStateCD" & vbCrLf & _
                                              "FROM SAP_MAINTE_KIKI_TB smt" & vbCrLf & _
                                              "JOIN CI_INFO_TB ct ON smt.CINmb = ct.CINmb" & vbCrLf & _
                                              "WHERE smt.IncNmb = :IncNmb" & vbCrLf & _
                                              "  AND smt.WorkNmb = :WorkNmb" & vbCrLf & _
                                              "  AND smt.CINmb = :CINmb"

    '更新値設定用：登録時履歴テーブルのCIステータス取得
    Private strSelectBefCIStateCDSql As String = "SELECT crt.CIStatusCD AS BefCIStateCD" & vbCrLf & _
                                                 "FROM SAP_MAINTE_KIKI_TB skt" & vbCrLf & _
                                                 "LEFT JOIN CI_INFO_RTB crt ON skt.CINmb = crt.CINmb AND skt.RegRirekiNo = crt.RirekiNo" & vbCrLf & _
                                                 "WHERE skt.IncNmb = :IncNmb" & vbCrLf & _
                                                 "  AND skt.WorkNmb = :WorkNmb" & vbCrLf & _
                                                 "  AND skt.CINmb = :CINmb"

    'CI共通情報.CIステータス更新（UPDATE）用SQL
    Private strUpdateCIStatusSql As String = "UPDATE CI_INFO_TB" & vbCrLf & _
                                             "SET CIStatusCD = CASE WHEN :CIStatusCD = '' THEN CIStatusCD ELSE :CIStatusCD END " & vbCrLf & _
                                             "   ,UpdateDT   = :UpdateDT" & vbCrLf & _
                                             "   ,UpGrpCD    = :UpGrpCD" & vbCrLf & _
                                             "   ,UpdateID   = :UpdateID" & vbCrLf & _
                                             "WHERE CINmb = :CINmb"

    'CI共通情報更新（UPDATE）用SQL：作業完了
    Private strUpdateCIInfoSql_Complete As String = _
                                                    "UPDATE CI_INFO_TB ct" & vbCrLf & _
                                                    "SET CIStatusCD         = :CIStatusCD" & vbCrLf & _
                                                    "   ,UpdateDT           = :UpdateDT" & vbCrLf & _
                                                    "   ,UpGrpCD            = :UpGrpCD" & vbCrLf & _
                                                    "   ,UpdateID           = :UpdateID" & vbCrLf & _
                                                    "WHERE ct.CINmb = :CINmb" & vbCrLf

    'CI共通情報更新（UPDATE）用SQL：作業取消
    Private strUpdateCIInfoSql_Cancel As String = strUpdateCIInfoSql_Complete

    '[mod]2013/02/08 t.fukuo セット機器IDがクリアされない不具合対応：START
    'CI共通情報更新（UPDATE）用SQL：セット作成
    'Private strUpdateCIInfoSql_SetPair As String = "UPDATE CI_INFO_TB t1" & vbCrLf & _
    '                                               "SET SetKikiID = :SetKikiID" & vbCrLf & _
    '                                               "   ,UpdateDT  = :UpdateDT" & vbCrLf & _
    '                                               "   ,UpGrpCD   = :UpGrpCD" & vbCrLf & _
    '                                               "   ,UpdateID  = :UpdateID" & vbCrLf & _
    '                                               "WHERE t1.CINmb = :CINmb" & vbCrLf & _
    '                                               "      OR t1.CINmb = CASE WHEN :SetKikiID IS NULL" & vbCrLf & _
    '                                               "                         THEN CASE (SELECT COUNT(1) FROM CI_INFO_TB t2 WHERE t2.SetKikiID = :SetKikiID AND t2.CINmb <> :CINmb)" & vbCrLf & _
    '                                               "                              WHEN 1 THEN (SELECT t2.CINmb FROM CI_INFO_TB t2 WHERE t2.SetKikiID = :SetKikiID AND t2.CINmb <> :CINmb)" & vbCrLf & _
    '                                               "                              ELSE :CINmb END" & vbCrLf & _
    '                                               "                         ELSE :CINmb END" & vbCrLf
    Private strUpdateCIInfoSql_SetPair As String = "UPDATE CI_INFO_TB t1" & vbCrLf & _
                                                   "SET SetKikiID = :SetKikiID" & vbCrLf & _
                                                   "   ,UpdateDT  = :UpdateDT" & vbCrLf & _
                                                   "   ,UpGrpCD   = :UpGrpCD" & vbCrLf & _
                                                   "   ,UpdateID  = :UpdateID" & vbCrLf & _
                                                   "WHERE t1.CINmb = :CINmb" & vbCrLf & _
                                                   "      OR t1.CINmb = CASE WHEN :SetKikiID IS NULL" & vbCrLf & _
                                                   "                         THEN CASE (SELECT COUNT(1) FROM CI_INFO_TB t2 WHERE t2.SetKikiID = (SELECT SetKikiID FROM CI_INFO_TB WHERE CINmb = :CINmb) AND t2.CINmb <> :CINmb)" & vbCrLf & _
                                                   "                              WHEN 1 THEN (SELECT t2.CINmb FROM CI_INFO_TB t2 WHERE t2.SetKikiID = (SELECT SetKikiID FROM CI_INFO_TB WHERE CINmb = :CINmb) AND t2.CINmb <> :CINmb)" & vbCrLf & _
                                                   "                              ELSE :CINmb END" & vbCrLf & _
                                                   "                         ELSE :CINmb END" & vbCrLf
    '[mod]2012/12/13 t.fukuo セット機器IDがクリアされない不具合対応：END

    'CIサポセン機器.作業の元更新（UPDATE）用SQL
    Private strUpdateWorkFromNmbSql As String = "UPDATE CI_SAP_TB" & vbCrLf & _
                                                "SET WorkFromNmb = :WorkFromNmb" & vbCrLf & _
                                                "   ,UpdateDT    = :UpdateDT" & vbCrLf & _
                                                "   ,UpGrpCD     = :UpGrpCD" & vbCrLf & _
                                                "   ,UpdateID    = :UpdateID" & vbCrLf & _
                                                "WHERE CINmb  = :CINmb"

    'CIサポセン機器更新（UPDATE）用SQL：クリア共通SET句
    Private strUpdateCISapSql_Clear_BaseSet As String = _
                                                 "UPDATE CI_SAP_TB ct SET" & vbCrLf & _
                                                 "    UpdateDT           = :UpdateDT" & vbCrLf & _
                                                 "   ,UpGrpCD            = :UpGrpCD" & vbCrLf & _
                                                 "   ,UpdateID           = :UpdateID" & vbCrLf
    'CIサポセン機器更新（UPDATE）用SQL：クリア（イメージ番号）
    Private strUpdateCISapSql_ClearImageNmb As String = _
                                                 "   ,ImageNmb      = ''" & vbCrLf & _
                                                 "   ,ImageNmbAimai = ''" & vbCrLf
    'CIサポセン機器更新（UPDATE）用SQL：クリア（その他）
    Private strUpdateCISapSql_ClearOther As String = _
                                                 "   ,LastInfoDT         = ''" & vbCrLf & _
                                                 "   ,ManageKyokuNM      = ''" & vbCrLf & _
                                                 "   ,ManageBusyoNM      = ''" & vbCrLf & _
                                                 "   ,WorkFromNmb        = ''" & vbCrLf & _
                                                 "   ,KikiUseCD          = ''" & vbCrLf & _
                                                 "   ,IPUseCD            = ''" & vbCrLf & _
                                                 "   ,FixedIP            = ''" & vbCrLf & _
                                                 "   ,UsrID              = ''" & vbCrLf & _
                                                 "   ,UsrNM              = ''" & vbCrLf & _
                                                 "   ,UsrCompany         = ''" & vbCrLf & _
                                                 "   ,UsrKyokuNM         = ''" & vbCrLf & _
                                                 "   ,UsrBusyoNM         = ''" & vbCrLf & _
                                                 "   ,UsrTel             = ''" & vbCrLf & _
                                                 "   ,UsrMailAdd         = ''" & vbCrLf & _
                                                 "   ,UsrContact         = ''" & vbCrLf & _
                                                 "   ,UsrRoom            = ''" & vbCrLf & _
                                                 "   ,RentalStDT         = ''" & vbCrLf & _
                                                 "   ,RentalEdDT         = ''" & vbCrLf & _
                                                 "   ,SetKyokuNM         = ''" & vbCrLf & _
                                                 "   ,SetBusyoNM         = ''" & vbCrLf & _
                                                 "   ,SetRoom            = ''" & vbCrLf & _
                                                 "   ,SetBuil            = ''" & vbCrLf & _
                                                 "   ,SetFloor           = ''" & vbCrLf & _
                                                 "   ,SetDeskNo          = ''" & vbCrLf & _
                                                 "   ,SetLANLength       = ''" & vbCrLf & _
                                                 "   ,SetLANNum          = ''" & vbCrLf & _
                                                 "   ,SetSocket          = ''" & vbCrLf & _
                                                 "   ,ManageBusyoNMAimai = ''" & vbCrLf & _
                                                 "   ,UsrIDAimai         = ''" & vbCrLf & _
                                                 "   ,SetBusyoNMAimai    = ''" & vbCrLf & _
                                                 "   ,SetRoomAimai       = ''" & vbCrLf & _
                                                 "   ,SetBuilAimai       = ''" & vbCrLf & _
                                                 "   ,SetFloorAimai      = ''" & vbCrLf
    'CIサポセン機器更新（UPDATE）用SQL：クリア共通WHERE句
    Private strUpdateCISapSql_Clear_BaseWhere As String = _
                                                 "WHERE ct.CINmb = :CINmb" & vbCrLf

    'CIサポセン機器更新（UPDATE）用SQL：交換設置
    Private strUpdateCISapSql_DoExchange As String = _
                                                 "UPDATE CI_SAP_TB cst" & vbCrLf & _
                                                 "SET ManageKyokuNM      = cst2.ManageKyokuNM" & vbCrLf & _
                                                 "   ,ManageBusyoNM      = cst2.ManageBusyoNM" & vbCrLf & _
                                                 "   ,KikiUseCD          = cst2.KikiUseCD" & vbCrLf & _
                                                 "   ,UsrID              = cst2.UsrID" & vbCrLf & _
                                                 "   ,UsrNM              = cst2.UsrNM" & vbCrLf & _
                                                 "   ,UsrCompany         = cst2.UsrCompany" & vbCrLf & _
                                                 "   ,UsrKyokuNM         = cst2.UsrKyokuNM" & vbCrLf & _
                                                 "   ,UsrBusyoNM         = cst2.UsrBusyoNM" & vbCrLf & _
                                                 "   ,UsrTel             = cst2.UsrTel" & vbCrLf & _
                                                 "   ,UsrMailAdd         = cst2.UsrMailAdd" & vbCrLf & _
                                                 "   ,UsrContact         = cst2.UsrContact" & vbCrLf & _
                                                 "   ,UsrRoom            = cst2.UsrRoom" & vbCrLf & _
                                                 "   ,SetKyokuNM         = cst2.SetKyokuNM" & vbCrLf & _
                                                 "   ,SetBusyoNM         = cst2.SetBusyoNM" & vbCrLf & _
                                                 "   ,SetRoom            = cst2.SetRoom" & vbCrLf & _
                                                 "   ,SetBuil            = cst2.SetBuil" & vbCrLf & _
                                                 "   ,SetFloor           = cst2.SetFloor" & vbCrLf & _
                                                 "   ,SetDeskNo          = cst2.SetDeskNo" & vbCrLf & _
                                                 "   ,SetLANLength       = cst2.SetLANLength" & vbCrLf & _
                                                 "   ,SetLANNum          = cst2.SetLANNum" & vbCrLf & _
                                                 "   ,SetSocket          = cst2.SetSocket" & vbCrLf & _
                                                 "   ,ManageBusyoNMAimai = cst2.ManageBusyoNMAimai " & vbCrLf & _
                                                 "   ,UsrIDAimai         = cst2.UsrIDAimai " & vbCrLf & _
                                                 "   ,SetBusyoNMAimai    = cst2.SetBusyoNMAimai " & vbCrLf & _
                                                 "   ,SetRoomAimai       = cst2.SetRoomAimai " & vbCrLf & _
                                                 "   ,SetBuilAimai       = cst2.SetBuilAimai " & vbCrLf & _
                                                 "   ,SetFloorAimai      = cst2.SetFloorAimai " & vbCrLf & _
                                                 "   ,UpdateDT           = :UpdateDT" & vbCrLf & _
                                                 "   ,UpGrpCD            = :UpGrpCD" & vbCrLf & _
                                                 "   ,UpdateID           = :UpdateID" & vbCrLf & _
                                                 "FROM (SELECT * " & vbCrLf & _
                                                 "      FROM CI_SAP_TB cst1" & vbCrLf & _
                                                 "      WHERE cst1.CINmb = :CINmb_Remove" & vbCrLf & _
                                                 "     ) cst2" & vbCrLf & _
                                                 "WHERE cst.CINmb = :CINmb_Set"

    'CIサポセン機器更新（UPDATE）用SQL：種別＝「継続利用」
    Private strUpdateCISapSql_Continue As String = "UPDATE CI_SAP_TB" & vbCrLf & _
                                                   "SET WorkFromNmb        = :WorkFromNmb" & vbCrLf & _
                                                   "   ,KikiUseCD          = :KikiUseCD" & vbCrLf & _
                                                   "   ,ManageKyokuNM      = :ManageKyokuNM" & vbCrLf & _
                                                   "   ,ManageBusyoNM      = :ManageBusyoNM" & vbCrLf & _
                                                   "   ,SetKyokuNM         = :SetKyokuNM" & vbCrLf & _
                                                   "   ,SetBusyoNM         = :SetBusyoNM" & vbCrLf & _
                                                   "   ,SetRoom            = :SetRoom" & vbCrLf & _
                                                   "   ,ManageBusyoNMAimai = :ManageBusyoNMAimai" & vbCrLf & _
                                                   "   ,SetBusyoNMAimai    = :SetBusyoNMAimai" & vbCrLf & _
                                                   "   ,SetRoomAimai       = :SetRoomAimai" & vbCrLf & _
                                                   "   ,UpdateDT           = :UpdateDT" & vbCrLf & _
                                                   "   ,UpGrpCD            = :UpGrpCD" & vbCrLf & _
                                                   "   ,UpdateID           = :UpdateID" & vbCrLf & _
                                                   "WHERE CINmb = :CINmb"


    'CIサポセン機器更新（UPDATE）用SQL：種別＝「一時利用（貸出）」
    Private strUpdateCISapSql_Rental As String = "UPDATE CI_SAP_TB" & vbCrLf & _
                                                 "SET WorkFromNmb        = :WorkFromNmb" & vbCrLf & _
                                                 "   ,KikiUseCD          = :KikiUseCD" & vbCrLf & _
                                                 "   ,IPUseCD            = :IPUseCD" & vbCrLf & _
                                                 "   ,UsrID              = :UsrID" & vbCrLf & _
                                                 "   ,UsrNM              = :UsrNM" & vbCrLf & _
                                                 "   ,UsrCompany         = :UsrCompany" & vbCrLf & _
                                                 "   ,UsrKyokuNM         = :UsrKyokuNM" & vbCrLf & _
                                                 "   ,UsrBusyoNM         = :UsrBusyoNM" & vbCrLf & _
                                                 "   ,UsrTel             = :UsrTel" & vbCrLf & _
                                                 "   ,UsrMailAdd         = :UsrMailAdd" & vbCrLf & _
                                                 "   ,UsrContact         = :UsrContact" & vbCrLf & _
                                                 "   ,UsrRoom            = :UsrRoom" & vbCrLf & _
                                                 "   ,ManageKyokuNM      = :ManageKyokuNM" & vbCrLf & _
                                                 "   ,ManageBusyoNM      = :ManageBusyoNM" & vbCrLf & _
                                                 "   ,SetKyokuNM         = :SetKyokuNM" & vbCrLf & _
                                                 "   ,SetBusyoNM         = :SetBusyoNM" & vbCrLf & _
                                                 "   ,SetRoom            = :SetRoom" & vbCrLf & _
                                                 "   ,ManageBusyoNMAimai = :ManageBusyoNMAimai" & vbCrLf & _
                                                 "   ,UsrIDAimai         = :UsrIDAimai" & vbCrLf & _
                                                 "   ,SetBusyoNMAimai    = :SetBusyoNMAimai" & vbCrLf & _
                                                 "   ,SetRoomAimai       = :SetRoomAimai" & vbCrLf & _
                                                 "   ,UpdateDT           = :UpdateDT" & vbCrLf & _
                                                 "   ,UpGrpCD            = :UpGrpCD" & vbCrLf & _
                                                 "   ,UpdateID           = :UpdateID" & vbCrLf & _
                                                 "WHERE CINmb = :CINmb"

    'CIサポセン機器更新（UPDATE）用SQL：作業取消
    Private strUpdateCISapSql_Cancel As String = _
                                           "UPDATE CI_SAP_TB ct SET" & vbCrLf & _
                                           " MemorySize         = crt.MemorySize " & vbCrLf & _
                                           ",Serial             = crt.Serial " & vbCrLf & _
                                           ",MacAddress1        = crt.MacAddress1 " & vbCrLf & _
                                           ",MacAddress2        = crt.MacAddress2 " & vbCrLf & _
                                           ",Fuzokuhin          = crt.Fuzokuhin " & vbCrLf & _
                                           ",TypeKbn            = crt.TypeKbn " & vbCrLf & _
                                           ",SCKikiFixNmb       = crt.SCKikiFixNmb " & vbCrLf & _
                                           ",KikiState          = crt.KikiState " & vbCrLf & _
                                           ",ImageNmb           = crt.ImageNmb " & vbCrLf & _
                                           ",LeaseUpDT          = crt.LeaseUpDT" & vbCrLf & _
                                           ",SCHokanKbn         = crt.SCHokanKbn " & vbCrLf & _
                                           ",LastInfoDT         = crt.LastInfoDT" & vbCrLf & _
                                           ",ManageKyokuNM      = crt.ManageKyokuNM " & vbCrLf & _
                                           ",ManageBusyoNM      = crt.ManageBusyoNM " & vbCrLf & _
                                           ",WorkFromNmb        = crt.WorkFromNmb " & vbCrLf & _
                                           ",KikiUseCD          = crt.KikiUseCD " & vbCrLf & _
                                           ",IPUseCD            = crt.IPUseCD " & vbCrLf & _
                                           ",FixedIP            = crt.FixedIP " & vbCrLf & _
                                           ",UsrID              = crt.UsrID " & vbCrLf & _
                                           ",UsrNM              = crt.UsrNM " & vbCrLf & _
                                           ",UsrCompany         = crt.UsrCompany " & vbCrLf & _
                                           ",UsrKyokuNM         = crt.UsrKyokuNM " & vbCrLf & _
                                           ",UsrBusyoNM         = crt.UsrBusyoNM " & vbCrLf & _
                                           ",UsrTel             = crt.UsrTel " & vbCrLf & _
                                           ",UsrMailAdd         = crt.UsrMailAdd " & vbCrLf & _
                                           ",UsrContact         = crt.UsrContact " & vbCrLf & _
                                           ",UsrRoom            = crt.UsrRoom " & vbCrLf & _
                                           ",RentalStDT         = crt.RentalStDT" & vbCrLf & _
                                           ",RentalEdDT         = crt.RentalEdDT" & vbCrLf & _
                                           ",SetKyokuNM         = crt.SetKyokuNM " & vbCrLf & _
                                           ",SetBusyoNM         = crt.SetBusyoNM " & vbCrLf & _
                                           ",SetRoom            = crt.SetRoom " & vbCrLf & _
                                           ",SetBuil            = crt.SetBuil " & vbCrLf & _
                                           ",SetFloor           = crt.SetFloor " & vbCrLf & _
                                           ",SetDeskNo          = crt.SetDeskNo " & vbCrLf & _
                                           ",SetLANLength       = crt.SetLANLength " & vbCrLf & _
                                           ",SetLANNum          = crt.SetLANNum " & vbCrLf & _
                                           ",SetSocket          = crt.SetSocket " & vbCrLf & _
                                           ",SerialAimai        = crt.SerialAimai " & vbCrLf & _
                                           ",ImageNmbAimai      = crt.ImageNmbAimai " & vbCrLf & _
                                           ",ManageBusyoNMAimai = crt.ManageBusyoNMAimai " & vbCrLf & _
                                           ",UsrIDAimai         = crt.UsrIDAimai " & vbCrLf & _
                                           ",SetBusyoNMAimai    = crt.SetBusyoNMAimai " & vbCrLf & _
                                           ",SetRoomAimai       = crt.SetRoomAimai " & vbCrLf & _
                                           ",SetBuilAimai       = crt.SetBuilAimai " & vbCrLf & _
                                           ",SetFloorAimai      = crt.SetFloorAimai " & vbCrLf & _
                                           ",UpdateDT           = :UpdateDT" & vbCrLf & _
                                           ",UpGrpCD            = :UpGrpCD" & vbCrLf & _
                                           ",UpdateID           = :UpdateID" & vbCrLf & _
                                           "FROM CI_SAP_RTB crt" & vbCrLf & _
                                           "WHERE (crt.CINmb, crt.RirekiNo) = " & vbCrLf & _
                                           "           (" & vbCrLf & _
                                           "             SELECT skt.CINmb, skt.RegRirekiNo" & vbCrLf & _
                                           "             FROM SAP_MAINTE_KIKI_TB skt" & vbCrLf & _
                                           "             WHERE skt.IncNmb = :IncNmb" & vbCrLf & _
                                           "               AND skt.WorkNmb = :WorkNmb" & vbCrLf & _
                                           "               AND skt.CINmb = :CINmb" & vbCrLf & _
                                           "           )" & vbCrLf & _
                                           "  AND ct.CINmb = :CINmb" & vbCrLf 


    'CIサポセン機器メンテナンス作業新規登録（INSERT）用SQL
    Private strInsertSapMainteWorkSql As String = _
                                              "INSERT INTO SAP_MAINTE_WORK_TB ( " & vbCrLf & _
                                              " IncNmb " & vbCrLf & _
                                              ",WorkNmb " & vbCrLf & _
                                              ",WorkCD " & vbCrLf & _
                                              ",WorkBiko " & vbCrLf & _
                                              ",WorkSceDT " & vbCrLf & _
                                              ",WorkCompDT " & vbCrLf & _
                                              ",CompFlg " & vbCrLf & _
                                              ",CancelFlg " & vbCrLf & _
                                              ",RegDT " & vbCrLf & _
                                              ",RegGrpCD " & vbCrLf & _
                                              ",RegID " & vbCrLf & _
                                              ",UpdateDT " & vbCrLf & _
                                              ",UpGrpCD " & vbCrLf & _
                                              ",UpdateID " & vbCrLf & _
                                              ") " & vbCrLf & _
                                              "VALUES (" & vbCrLf & _
                                              " :IncNmb " & vbCrLf & _
                                              ",(SELECT COALESCE(MAX(t.WorkNmb),0)+1 FROM SAP_MAINTE_WORK_TB t WHERE t.IncNmb = :IncNmb)" & vbCrLf & _
                                              ",:WorkCD " & vbCrLf & _
                                              ",:WorkBiko " & vbCrLf & _
                                              ",:WorkSceDT " & vbCrLf & _
                                              ",:WorkCompDT " & vbCrLf & _
                                              ",:CompFlg " & vbCrLf & _
                                              ",:CancelFLg " & vbCrLf & _
                                              ",:RegDT " & vbCrLf & _
                                              ",:RegGrpCD " & vbCrLf & _
                                              ",:RegID " & vbCrLf & _
                                              ",:UpdateDT " & vbCrLf & _
                                              ",:UpGrpCD " & vbCrLf & _
                                              ",:UpdateID " & vbCrLf & _
                                              ") " & vbCrLf

    'CIサポセン機器メンテナンス機器新規登録（INSERT）用SQL
    Private strInsertSapMainteKikiSql As String = _
                                              "INSERT INTO SAP_MAINTE_KIKI_TB ( " & vbCrLf & _
                                              " IncNmb " & vbCrLf & _
                                              ",WorkNmb " & vbCrLf & _
                                              ",RowNmb " & vbCrLf & _
                                              ",CINmb " & vbCrLf & _
                                              ",ChgFlg " & vbCrLf & _
                                              ",ChgNmb " & vbCrLf & _
                                              ",RegRirekiNo " & vbCrLf & _
                                              ",LastUpRirekiNo " & vbCrLf & _
                                              ",RegDT " & vbCrLf & _
                                              ",RegGrpCD " & vbCrLf & _
                                              ",RegID " & vbCrLf & _
                                              ",UpdateDT " & vbCrLf & _
                                              ",UpGrpCD " & vbCrLf & _
                                              ",UpdateID " & vbCrLf & _
                                              ") " & vbCrLf & _
                                              "VALUES (" & vbCrLf & _
                                              " :IncNmb " & vbCrLf & _
                                              ",(SELECT MAX(t.WorkNmb) FROM SAP_MAINTE_WORK_TB t WHERE t.IncNmb = :IncNmb)" & vbCrLf & _
                                              ",:RowNmb" & vbCrLf & _
                                              ",:CINmb " & vbCrLf & _
                                              ",:ChgFlg " & vbCrLf & _
                                              ",NULL " & vbCrLf & _
                                              ",:RegRirekiNo " & vbCrLf & _
                                              ",:LastUpRirekiNo " & vbCrLf & _
                                              ",:RegDT " & vbCrLf & _
                                              ",:RegGrpCD " & vbCrLf & _
                                              ",:RegID " & vbCrLf & _
                                              ",:UpdateDT " & vbCrLf & _
                                              ",:UpGrpCD " & vbCrLf & _
                                              ",:UpdateID " & vbCrLf & _
                                              ")" & vbCrLf

    'CIサポセン機器メンテナンス作業更新（UPDATE）用SQL
    Private strUpdateSapMainteWorkSql As String = _
                                              "UPDATE SAP_MAINTE_WORK_TB" & vbCrLf & _
                                              "SET" & vbCrLf & _
                                              " WorkBiko   = :WorkBiko" & vbCrLf & _
                                              ",WorkSceDT  = CASE :WorkSceDT WHEN NULL THEN '' ELSE TO_CHAR(:WorkSceDT,'YYYYMMDD') END" & vbCrLf & _
                                              ",WorkCompDT = CASE :WorkCompDT WHEN NULL THEN '' ELSE TO_CHAR(:WorkCompDT,'YYYYMMDD') END" & vbCrLf & _
                                              ",CompFlg    = :CompFlg" & vbCrLf & _
                                              ",CancelFLg  = :CancelFLg" & vbCrLf & _
                                              ",UpdateDT   = :UpdateDT" & vbCrLf & _
                                              ",UpGrpCD    = :UpGrpCD" & vbCrLf & _
                                              ",UpdateID   = :UpdateID" & vbCrLf & _
                                              "WHERE IncNmb  = :IncNmb" & vbCrLf & _
                                              "  AND WorkNmb = :WorkNmb"

    'CIサポセン機器メンテナンス機器更新（UPDATE）用SQL
    Private strUpdateSapMainteKikiSql As String = _
                                              "UPDATE SAP_MAINTE_KIKI_TB" & vbCrLf & _
                                              "SET" & vbCrLf & _
                                              " ChgFlg         = :ChgFlg" & vbCrLf & _
                                              ",ChgNmb         = :ChgNmb" & vbCrLf & _
                                              ",CepalateFlg    = :CepalateFlg" & vbCrLf & _
                                              ",UpdateDT   = :UpdateDT" & vbCrLf & _
                                              ",UpGrpCD    = :UpGrpCD" & vbCrLf & _
                                              ",UpdateID   = :UpdateID" & vbCrLf & _
                                              "WHERE IncNmb  = :IncNmb" & vbCrLf & _
                                              "  AND WorkNmb = :WorkNmb" & vbCrLf & _
                                              "  AND CINmb   = :CINmb" & vbCrLf


    'CIサポセン機器メンテナンス機器.最終更新時履歴No更新（UPDATE）用SQL
    Private strUpdateSapMainteKikiLastUpRirekiNoSql As String = _
                                              "UPDATE SAP_MAINTE_KIKI_TB" & vbCrLf & _
                                              "SET" & vbCrLf & _
                                              " LastUpRirekiNo = :LastUpRirekiNo" & vbCrLf & _
                                              "WHERE IncNmb  = :IncNmb" & vbCrLf & _
                                              "  AND WorkNmb = :WorkNmb" & vbCrLf & _
                                              "  AND CINmb   = :CINmb" & vbCrLf

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

    'オプションソフト物理削除（DELETE）用SQL
    Private strDeleteOptSoftSql As String = "DELETE FROM OPTSOFT_TB" & vbCrLf & _
                                            "WHERE CINmb = :CINmb"

    '交換撤去データよりオプションソフト新規登録（INSERT）用SQL
    Private strInsertOptSoftWhenExchangeSql As String = _
                                                   "INSERT INTO OPTSOFT_TB (" & vbCrLf & _
                                                   "  CINmb" & vbCrLf & _
                                                   " ,RowNmb" & vbCrLf & _
                                                   " ,SoftCD" & vbCrLf & _
                                                   " ,RegDT" & vbCrLf & _
                                                   " ,RegGrpCD" & vbCrLf & _
                                                   " ,RegID" & vbCrLf & _
                                                   " ,UpdateDT" & vbCrLf & _
                                                   " ,UpGrpCD" & vbCrLf & _
                                                   " ,UpdateID" & vbCrLf & _
                                                   ")" & vbCrLf & _
                                                   "SELECT" & vbCrLf & _
                                                   "  :CINmb_Set" & vbCrLf & _
                                                   " ,ot.RowNmb" & vbCrLf & _
                                                   " ,ot.SoftCD" & vbCrLf & _
                                                   " ,:RegDT" & vbCrLf & _
                                                   " ,:RegGrpCD" & vbCrLf & _
                                                   " ,:RegID" & vbCrLf & _
                                                   " ,:UpdateDT" & vbCrLf & _
                                                   " ,:UpGrpCD" & vbCrLf & _
                                                   " ,:UpdateID" & vbCrLf & _
                                                   "FROM OPTSOFT_TB ot" & vbCrLf & _
                                                   "WHERE ot.CINmb = :CINmb_Remove"

    '作業登録前データよりオプションソフト新規登録（INSERT）用SQL：作業取消
    Private strInsertOptSoftFromBefSql As String = _
                                                   "INSERT INTO OPTSOFT_TB (" & vbCrLf & _
                                                   "  CINmb" & vbCrLf & _
                                                   " ,RowNmb" & vbCrLf & _
                                                   " ,SoftCD" & vbCrLf & _
                                                   " ,RegDT" & vbCrLf & _
                                                   " ,RegGrpCD" & vbCrLf & _
                                                   " ,RegID" & vbCrLf & _
                                                   " ,UpdateDT" & vbCrLf & _
                                                   " ,UpGrpCD" & vbCrLf & _
                                                   " ,UpdateID" & vbCrLf & _
                                                   ")" & vbCrLf & _
                                                   "SELECT" & vbCrLf & _
                                                   "  ort.CINmb" & vbCrLf & _
                                                   " ,ort.RowNmb" & vbCrLf & _
                                                   " ,ort.SoftCD" & vbCrLf & _
                                                   " ,ort.RegDT" & vbCrLf & _
                                                   " ,ort.RegGrpCD" & vbCrLf & _
                                                   " ,ort.RegID" & vbCrLf & _
                                                   " ,:UpdateDT" & vbCrLf & _
                                                   " ,:UpGrpCD" & vbCrLf & _
                                                   " ,:UpdateID" & vbCrLf & _
                                                   "FROM OPTSOFT_RTB ort" & vbCrLf & _
                                                   "WHERE (ort.CINmb, ort.RirekiNo) = " & vbCrLf & _
                                                   "           (" & vbCrLf & _
                                                   "             SELECT skt.CINmb, skt.RegRirekiNo" & vbCrLf & _
                                                   "             FROM SAP_MAINTE_KIKI_TB skt" & vbCrLf & _
                                                   "             WHERE skt.IncNmb = :IncNmb" & vbCrLf & _
                                                   "               AND skt.WorkNmb = :WorkNmb" & vbCrLf & _
                                                   "               AND skt.CINmb = :CINmb" & vbCrLf & _
                                                   "           )" & vbCrLf

    ''保存用テーブルよりオプションソフト（保存用）新規登録（INSERT）用SQL
    'Private strInsertTmpOptSoftFromTmpSql As String = _
    '                                               "INSERT INTO OPTSOFT_TMP (" & vbCrLf & _
    '                                               "  IncNmb" & vbCrLf & _
    '                                               " ,WorkNmb" & vbCrLf & _
    '                                               " ,CINmb" & vbCrLf & _
    '                                               " ,RowNmb" & vbCrLf & _
    '                                               " ,SoftCD" & vbCrLf & _
    '                                               " ,RegDT" & vbCrLf & _
    '                                               " ,RegGrpCD" & vbCrLf & _
    '                                               " ,RegID" & vbCrLf & _
    '                                               " ,UpdateDT" & vbCrLf & _
    '                                               " ,UpGrpCD" & vbCrLf & _
    '                                               " ,UpdateID" & vbCrLf & _
    '                                               ")" & vbCrLf & _
    '                                               "SELECT" & vbCrLf & _
    '                                               "  :IncNmb_Set" & vbCrLf & _
    '                                               " ,:WorkNmb_Set" & vbCrLf & _
    '                                               " ,:CINmb_Set" & vbCrLf & _
    '                                               " ,tmp.RowNmb" & vbCrLf & _
    '                                               " ,tmp.SoftCD" & vbCrLf & _
    '                                               " ,:RegDT" & vbCrLf & _
    '                                               " ,:RegGrpCD" & vbCrLf & _
    '                                               " ,:RegID" & vbCrLf & _
    '                                               " ,:UpdateDT" & vbCrLf & _
    '                                               " ,:UpGrpCD" & vbCrLf & _
    '                                               " ,:UpdateID" & vbCrLf & _
    '                                               "FROM OPTSOFT_TMP tmp" & vbCrLf & _
    '                                               "WHERE tmp.IncNmb = :IncNmb_Remove" & vbCrLf & _
    '                                               "  AND tmp.WorkNmb = :WorkNmb_Remove" & vbCrLf & _
    '                                               "  AND tmp.CINmb = :CINmb_Remove"

    ''オプションソフトより保存用テーブル新規登録（INSERT）用SQL
    'Private strInsertTmpOptSoftFromOrgSql As String = _
    '                                               "INSERT INTO OPTSOFT_TMP (" & vbCrLf & _
    '                                               "  IncNmb" & vbCrLf & _
    '                                               " ,WorkNmb" & vbCrLf & _
    '                                               " ,CINmb" & vbCrLf & _
    '                                               " ,RowNmb" & vbCrLf & _
    '                                               " ,SoftCD" & vbCrLf & _
    '                                               " ,RegDT" & vbCrLf & _
    '                                               " ,RegGrpCD" & vbCrLf & _
    '                                               " ,RegID" & vbCrLf & _
    '                                               " ,UpdateDT" & vbCrLf & _
    '                                               " ,UpGrpCD" & vbCrLf & _
    '                                               " ,UpdateID" & vbCrLf & _
    '                                               ")" & vbCrLf & _
    '                                               "SELECT" & vbCrLf & _
    '                                               "  :IncNmb" & vbCrLf & _
    '                                               " ,:WorkNmb" & vbCrLf & _
    '                                               " ,ot.CINmb" & vbCrLf & _
    '                                               " ,ot.RowNmb" & vbCrLf & _
    '                                               " ,ot.SoftCD" & vbCrLf & _
    '                                               " ,:RegDT" & vbCrLf & _
    '                                               " ,:RegGrpCD" & vbCrLf & _
    '                                               " ,:RegID" & vbCrLf & _
    '                                               " ,:UpdateDT" & vbCrLf & _
    '                                               " ,:UpGrpCD" & vbCrLf & _
    '                                               " ,:UpdateID" & vbCrLf & _
    '                                               "FROM OPTSOFT_TB ot" & vbCrLf & _
    '                                               "WHERE ot.CINmb = :CINmb" & vbCrLf

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

    'セット機器管理削除　※機器が2台しかない場合は2台とも削除
    Private strDeleteSetKikiMngSql As String = "DELETE FROM SET_KIKI_MNG_TB t1" & vbCrLf & _
                                               "WHERE t1.SetKikiID = :SetKikiID" & vbCrLf & _
                                               "  AND (t1.CINmb = :CINmb" & vbCrLf & _
                                               "       OR t1.CINmb = CASE (SELECT COUNT(1) FROM SET_KIKI_MNG_TB t2 WHERE t1.SetKikiID = t2.SetKikiID AND t2.CINmb <> :CINmb)" & vbCrLf & _
                                               "                     WHEN 1 THEN (SELECT t2.CINmb FROM SET_KIKI_MNG_TB t2 WHERE t1.SetKikiID = t2.SetKikiID AND t2.CINmb <> :CINmb)" & vbCrLf & _
                                               "                     ELSE :CINmb END" & vbCrLf & _
                                               "      )"

    'セット機器管理削除：交換撤去時　※自分のみ
    Private strDeleteSetKikiMngWhenExchangeRemoveSql As String = _
                                               "DELETE FROM SET_KIKI_MNG_TB t1" & vbCrLf & _
                                               "WHERE t1.CINmb = :CINmb"


    'セット機器管理削除：作業取消時
    Private strDeleteSetKikiMngForCancelSql As String = _
                                               "DELETE FROM SET_KIKI_MNG_TB t1" & vbCrLf & _
                                               "WHERE t1.SetKikiID = (" & vbCrLf & _
                                               "  SELECT srt.SetKikiID" & vbCrLf & _
                                               "  FROM SETKIKI_RTB srt" & vbCrLf & _
                                               "  WHERE (srt.CINmb, srt.RirekiNo, srt.SetCINmb) =" & vbCrLf & _
                                               "           (" & vbCrLf & _
                                               "             SELECT skt.CINmb, skt.RegRirekiNo + 1, skt.CINmb" & vbCrLf & _
                                               "             FROM SAP_MAINTE_KIKI_TB skt" & vbCrLf & _
                                               "             WHERE skt.IncNmb = :IncNmb" & vbCrLf & _
                                               "               AND skt.WorkNmb = :WorkNmb" & vbCrLf & _
                                               "               AND skt.CINmb = :CINmb" & vbCrLf & _
                                               "           )" & vbCrLf & _
                                               " )" & vbCrLf


    'セット機器管理新規登録（INSERT）用SQL
    Private strInsertSetKikiMngSql As String = "INSERT INTO SET_KIKI_MNG_TB ( " & vbCrLf & _
                                               " SetKikiMngNmb " & vbCrLf & _
                                               ",SetKikiID " & vbCrLf & _
                                               ",CINmb " & vbCrLf & _
                                               ",EndUsrID " & vbCrLf & _
                                               ",RegDT " & vbCrLf & _
                                               ",RegGrpCD " & vbCrLf & _
                                               ",RegID " & vbCrLf & _
                                               ",UpdateDT " & vbCrLf & _
                                               ",UpGrpCD " & vbCrLf & _
                                               ",UpdateID " & vbCrLf & _
                                               ") " & vbCrLf & _
                                               "VALUES (" & vbCrLf & _
                                               " (SELECT t.SetKikiMngNmb FROM (" & GET_NEXTVAL_SETKIKIMNGNMB & ") t )" & vbCrLf & _
                                               ",:SetKikiID " & vbCrLf & _
                                               ",:CINmb " & vbCrLf & _
                                               ",(SELECT t.UsrID FROM CI_SAP_TB t WHERE t.CINmb = :CINmb) " & vbCrLf & _
                                               ",:RegDT " & vbCrLf & _
                                               ",:RegGrpCD " & vbCrLf & _
                                               ",:RegID " & vbCrLf & _
                                               ",:UpdateDT " & vbCrLf & _
                                               ",:UpGrpCD " & vbCrLf & _
                                               ",:UpdateID " & vbCrLf & _
                                               ")" & vbCrLf

    'セット機器管理登録（INSERT）用SQL：交換撤去→交換設置
    Private strInsertSetKikiMngExchangeSql As String = _
                                               "INSERT INTO SET_KIKI_MNG_TB (" & vbCrLf & _
                                               " SetKikiMngNmb " & vbCrLf & _
                                               ",SetKikiID " & vbCrLf & _
                                               ",CINmb " & vbCrLf & _
                                               ",EndUsrID " & vbCrLf & _
                                               ",RegDT " & vbCrLf & _
                                               ",RegGrpCD " & vbCrLf & _
                                               ",RegID " & vbCrLf & _
                                               ",UpdateDT " & vbCrLf & _
                                               ",UpGrpCD " & vbCrLf & _
                                               ",UpdateID " & vbCrLf & _
                                               ") " & vbCrLf & _
                                               "SELECT" & vbCrLf & _
                                               " (SELECT t.SetKikiMngNmb FROM (" & GET_NEXTVAL_SETKIKIMNGNMB & ") t )" & vbCrLf & _
                                               ",ct.SetKikiID" & vbCrLf & _
                                               ",:CINmb_Set" & vbCrLf & _
                                               ",(SELECT UsrID FROM CI_SAP_TB t WHERE t.CINmb = ct.CINmb)" & vbCrLf & _
                                               ",:RegDT" & vbCrLf & _
                                               ",:RegGrpCD" & vbCrLf & _
                                               ",:RegID" & vbCrLf & _
                                               ",:UpdateDT" & vbCrLf & _
                                               ",:UpGrpCD " & vbCrLf & _
                                               ",:UpdateID" & vbCrLf & _
                                               "FROM CI_INFO_TB ct" & vbCrLf & _
                                               "WHERE ct.SetKikiID = :SetKikiID_Remove" & vbCrLf & _
                                               "  AND ct.CINmb = :CINmb_Set" & vbCrLf

    '作業登録時データでセット機器管理登録（INSERT）用SQL：作業取消
    Private strInsertSetKikiMngFromRegSql As String = _
                                               "INSERT INTO SET_KIKI_MNG_TB (" & vbCrLf & _
                                               " SetKikiMngNmb " & vbCrLf & _
                                               ",SetKikiID " & vbCrLf & _
                                               ",CINmb " & vbCrLf & _
                                               ",EndUsrID " & vbCrLf & _
                                               ",RegDT " & vbCrLf & _
                                               ",RegGrpCD " & vbCrLf & _
                                               ",RegID " & vbCrLf & _
                                               ",UpdateDT " & vbCrLf & _
                                               ",UpGrpCD " & vbCrLf & _
                                               ",UpdateID " & vbCrLf & _
                                               ") " & vbCrLf & _
                                               "SELECT" & vbCrLf & _
                                               " srt.SetKikiMngNmb" & vbCrLf & _
                                               ",srt.SetKikiID" & vbCrLf & _
                                               ",srt.SetCINmb" & vbCrLf & _
                                               ",srt.EndUsrID" & vbCrLf & _
                                               ",srt.RegDT" & vbCrLf & _
                                               ",srt.RegGrpCD" & vbCrLf & _
                                               ",srt.RegID" & vbCrLf & _
                                               ",:UpdateDT" & vbCrLf & _
                                               ",:UpGrpCD " & vbCrLf & _
                                               ",:UpdateID" & vbCrLf & _
                                               "FROM SETKIKI_RTB srt" & vbCrLf & _
                                               "WHERE (srt.CINmb, srt.RirekiNo) = " & vbCrLf & _
                                               "           (" & vbCrLf & _
                                               "             SELECT skt.CINmb, skt.RegRirekiNo + 1" & vbCrLf & _
                                               "             FROM SAP_MAINTE_KIKI_TB skt" & vbCrLf & _
                                               "             WHERE skt.IncNmb = :IncNmb" & vbCrLf & _
                                               "               AND skt.WorkNmb = :WorkNmb" & vbCrLf & _
                                               "               AND skt.CINmb = :CINmb" & vbCrLf & _
                                               "           )" & vbCrLf


    'CI共通情報セットID更新（UPDATE）用SQL：交換設置／撤去
    Private strUpdateCIInfoSetKikiIDExchangeSql As String = _
                                                "UPDATE CI_INFO_TB" & vbCrLf & _
                                                "SET SetKikiID = CASE CINmb WHEN :CINmb_Set THEN :SetKikiID_Remove ELSE NULL END" & vbCrLf & _
                                                 "  ,UpdateDT = :UpdateDT" & vbCrLf & _
                                                "   ,UpGrpCD  = :UpGrpCD " & vbCrLf & _
                                                "   ,UpdateID = :UpdateID" & vbCrLf & _
                                                "WHERE CINmb IN(:CINmb_Set, :CINmb_Remove) "

    '作業登録前データでCI共通情報セットID更新：作業取消
    'Private strUpdateCIInfoSetKikiIDFromBefSql As String = _
    '                                            "UPDATE CI_INFO_TB ct" & vbCrLf & _
    '                                            "SET SetKikiID = (SELECT srt.SetKikiID" & vbCrLf & _
    '                                            "                 FROM SETKIKI_RTB srt" & vbCrLf & _
    '                                            "                 WHERE (srt.CINmb, srt.RirekiNo, srt.SetCINmb) =" & vbCrLf & _
    '                                            "                         (" & vbCrLf & _
    '                                            "                           SELECT skt.CINmb, skt.RegRirekiNo, skt.CINmb" & vbCrLf & _
    '                                            "                           FROM SAP_MAINTE_KIKI_TB skt" & vbCrLf & _
    '                                            "                           WHERE skt.IncNmb = :IncNmb" & vbCrLf & _
    '                                            "                             AND skt.WorkNmb = :WorkNmb" & vbCrLf & _
    '                                            "                             AND skt.CINmb = :CINmb" & vbCrLf & _
    '                                            "                         )" & vbCrLf & _
    '                                            "                )" & vbCrLf & _
    '                                            "   ,UpdateDT = :UpdateDT" & vbCrLf & _
    '                                            "   ,UpGrpCD  = :UpGrpCD " & vbCrLf & _
    '                                            "   ,UpdateID = :UpdateID" & vbCrLf & _
    '                                            "WHERE ct.CINmb IN (SELECT srt.SetCINmb" & vbCrLf & _
    '                                            "                   FROM SETKIKI_RTB srt" & vbCrLf & _
    '                                            "                   WHERE srt.CINmb = :CINmb" & vbCrLf & _
    '                                            "                     AND srt.RirekiNo = :RirekiNo" & vbCrLf & _
    '                                            "                  )"

    'Private strUpdateCIInfoSetKikiIDFromBefSql As String = _
    '                                            "UPDATE CI_INFO_TB ct" & vbCrLf & _
    '                                            "SET SetKikiID = crt.SetKikiID" & vbCrLf & _
    '                                            "   ,UpdateDT = :UpdateDT" & vbCrLf & _
    '                                            "   ,UpGrpCD  = :UpGrpCD " & vbCrLf & _
    '                                            "   ,UpdateID = :UpdateID" & vbCrLf & _
    '                                            "FROM CI_INFO_RTB crt" & vbCrLf & _
    '                                            "WHERE (" & vbCrLf & _
    '                                            "       (crt.CINmb, crt.RirekiNo) IN" & vbCrLf & _
    '                                            "        (SELECT srt.SetCINmb, srt.SetRirekiNo" & vbCrLf & _
    '                                            "         FROM SETKIKI_RTB srt" & vbCrLf & _
    '                                            "         WHERE srt.CINmb = :CINmb" & vbCrLf & _
    '                                            "           AND srt.RirekiNo = :RirekiNo" & vbCrLf & _
    '                                            "        )" & vbCrLf & _
    '                                            "       AND ct.CINmb = crt.CINmb" & vbCrLf & _
    '                                            "     )" & vbCrLf & _
    '                                            "     OR ct.CINmb = :CINmb" & vbCrLf

    Private strUpdateCIInfoSetKikiIDFromRegSql As String = _
                                               "UPDATE CI_INFO_TB ct" & vbCrLf & _
                                               "SET SetKikiID = t.SetKikiID" & vbCrLf & _
                                               "   ,UpdateDT = :UpdateDT" & vbCrLf & _
                                               "   ,UpGrpCD  = :UpGrpCD " & vbCrLf & _
                                               "   ,UpdateID = :UpdateID" & vbCrLf & _
                                               "FROM (SELECT srt.SetCINmb, srt.SetKikiID" & vbCrLf & _
                                               "      FROM SETKIKI_RTB srt" & vbCrLf & _
                                               "      WHERE srt.CINmb = :CINmb" & vbCrLf & _
                                               "        AND srt.RirekiNo = :RirekiNo" & vbCrLf & _
                                               "     ) t" & vbCrLf & _
                                               "WHERE ct.CINmb = t.SetCINmb" & vbCrLf

    'CI共通情報.セットIDクリア
    Private strUpdateCIInfoSetKikiIDClearSql As String = _
                                               "UPDATE CI_INFO_TB ct" & vbCrLf & _
                                               "SET SetKikiID = NULL" & vbCrLf & _
                                               "   ,UpdateDT = :UpdateDT" & vbCrLf & _
                                               "   ,UpGrpCD  = :UpGrpCD " & vbCrLf & _
                                               "   ,UpdateID = :UpdateID" & vbCrLf & _
                                               "FROM CI_INFO_RTB crt" & vbCrLf & _
                                               "WHERE (" & vbCrLf & _
                                               "       (crt.CINmb, crt.RirekiNo) IN" & vbCrLf & _
                                               "        (SELECT srt.SetCINmb, srt.SetRirekiNo" & vbCrLf & _
                                               "         FROM SETKIKI_RTB srt" & vbCrLf & _
                                               "         WHERE srt.CINmb = :CINmb" & vbCrLf & _
                                               "           AND srt.RirekiNo IN (:RirekiNo_Reg, :RirekiNo_Last)" & vbCrLf & _
                                               "        )" & vbCrLf & _
                                               "       AND ct.CINmb = crt.CINmb" & vbCrLf & _
                                               "     )" & vbCrLf

    '【ADD】2014/04/07 e.okamura 作業取消時セット機器更新修正 START
    'CI共通情報.セットIDクリア　※対象機器のみ
    Private strUpdateCIInfoSetKikiIDClearTargetOnlySql As String = _
                                               "UPDATE CI_INFO_TB" & vbCrLf & _
                                               "SET SetKikiID = NULL" & vbCrLf & _
                                               "   ,UpdateDT = :UpdateDT" & vbCrLf & _
                                               "   ,UpGrpCD  = :UpGrpCD " & vbCrLf & _
                                               "   ,UpdateID = :UpdateID" & vbCrLf & _
                                               "WHERE CINmb = :CINmb"
    '【ADD】2014/04/07 e.okamura 作業取消時セット機器更新修正 END

    'セット機器履歴新規登録（INSERT）用SQL
    Private strInsertSetKikiRirekiSql As String = "INSERT INTO SETKIKI_RTB ( " & vbCrLf & _
                                                  " CINmb " & vbCrLf & _
                                                  ",RirekiNo " & vbCrLf & _
                                                  ",SetKikiMngNmb " & vbCrLf & _
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
                                                  ",(SELECT MAX(RirekiNo) FROM CI_INFO_RTB WHERE CINmb = :CINmb ) " & vbCrLf & _
                                                  ",st.SetKikiMngNmb " & vbCrLf & _
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
                                                  "WHERE st.SetKikiID = (SELECT SetKikiID FROM CI_INFO_TB WHERE CINmb = :CINmb)" & vbCrLf 

    '複数人利用物理削除（DELETE）用SQL
    Private strDeleteShareSql As String = "DELETE FROM SHARE_TB" & vbCrLf & _
                                          "WHERE CINmb = :CINmb"

    '作業登録前データで複数人利用新規登録（INSERT）用SQL
    Private strInsertShareFromBefSql As String = "INSERT INTO SHARE_TB (" & vbCrLf & _
                                                 "  CINmb" & vbCrLf & _
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
                                                 "SELECT" & vbCrLf & _
                                                 "  srt.CINmb" & vbCrLf & _
                                                 " ,srt.RowNmb" & vbCrLf & _
                                                 " ,srt.UsrID" & vbCrLf & _
                                                 " ,srt.UsrNM" & vbCrLf & _
                                                 " ,srt.RegDT" & vbCrLf & _
                                                 " ,srt.RegGrpCD" & vbCrLf & _
                                                 " ,srt.RegID" & vbCrLf & _
                                                 " ,:UpdateDT" & vbCrLf & _
                                                 " ,:UpGrpCD" & vbCrLf & _
                                                 " ,:UpdateID" & vbCrLf & _
                                                 "FROM SHARE_RTB srt" & vbCrLf & _
                                                 "WHERE (srt.CINmb, srt.RirekiNo) = " & vbCrLf & _
                                                 "           (" & vbCrLf & _
                                                 "             SELECT skt.CINmb, skt.RegRirekiNo" & vbCrLf & _
                                                 "             FROM SAP_MAINTE_KIKI_TB skt" & vbCrLf & _
                                                 "             WHERE skt.IncNmb = :IncNmb" & vbCrLf & _
                                                 "               AND skt.WorkNmb = :WorkNmb" & vbCrLf & _
                                                 "               AND skt.CINmb = :CINmb" & vbCrLf & _
                                                 "           )" & vbCrLf

    '交換設置時複数人利用新規登録（INSERT）用SQL
    Private strInsertShareWhenExchangeSql As String = _
                                                 "INSERT INTO SHARE_TB (" & vbCrLf & _
                                                 "  CINmb" & vbCrLf & _
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
                                                 "SELECT" & vbCrLf & _
                                                 "  :CINmb_Set" & vbCrLf & _
                                                 " ,st2.RowNmb" & vbCrLf & _
                                                 " ,st2.UsrID" & vbCrLf & _
                                                 " ,st2.UsrNM" & vbCrLf & _
                                                 " ,:RegDT" & vbCrLf & _
                                                 " ,:RegGrpCD" & vbCrLf & _
                                                 " ,:RegID" & vbCrLf & _
                                                 " ,:UpdateDT" & vbCrLf & _
                                                 " ,:UpGrpCD" & vbCrLf & _
                                                 " ,:UpdateID" & vbCrLf & _
                                                 "FROM SHARE_TB st2" & vbCrLf & _
                                                 "WHERE st2.CINmb = :CINmb_Remove"

    ''複数人利用より保存用テーブル新規登録（INSERT）用SQL
    'Private strInsertTmpShareFromOrgSql As String = _
    '                                             "INSERT INTO SHARE_TMP (" & vbCrLf & _
    '                                             "  IncNmb" & vbCrLf & _
    '                                             " ,WorkNmb" & vbCrLf & _
    '                                             " ,CINmb" & vbCrLf & _
    '                                             " ,RowNmb" & vbCrLf & _
    '                                             " ,UsrID" & vbCrLf & _
    '                                             " ,UsrNM" & vbCrLf & _
    '                                             " ,RegDT" & vbCrLf & _
    '                                             " ,RegGrpCD" & vbCrLf & _
    '                                             " ,RegID" & vbCrLf & _
    '                                             " ,UpdateDT" & vbCrLf & _
    '                                             " ,UpGrpCD" & vbCrLf & _
    '                                             " ,UpdateID" & vbCrLf & _
    '                                             ")" & vbCrLf & _
    '                                             "SELECT" & vbCrLf & _
    '                                             "  :IncNmb" & vbCrLf & _
    '                                             " ,:WorkNmb" & vbCrLf & _
    '                                             " ,st.CINmb" & vbCrLf & _
    '                                             " ,st.RowNmb" & vbCrLf & _
    '                                             " ,st.UsrID" & vbCrLf & _
    '                                             " ,st.UsrNM" & vbCrLf & _
    '                                             " ,:RegDT" & vbCrLf & _
    '                                             " ,:RegGrpCD" & vbCrLf & _
    '                                             " ,:RegID" & vbCrLf & _
    '                                             " ,:UpdateDT" & vbCrLf & _
    '                                             " ,:UpGrpCD" & vbCrLf & _
    '                                             " ,:UpdateID" & vbCrLf & _
    '                                             "FROM SHARE_TB st" & vbCrLf & _
    '                                             "WHERE st.CINmb = :CINmb" & vbCrLf

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

    '登録理由履歴新規登録（INSERT）用SQL：汎用
    Private strInsertRegReasonSql As String = "INSERT INTO REGREASON_RTB ( " & vbCrLf & _
                                              " CINmb " & vbCrLf & _
                                              ",RirekiNo " & vbCrLf & _
                                              ",RegReason " & vbCrLf & _
                                              ",WorkCD " & vbCrLf & _
                                              ",WorkKbnCD " & vbCrLf & _
                                              ",ChgFlg " & vbCrLf & _
                                              ",ChgCINmb " & vbCrLf & _
                                              ",WorkBiko " & vbCrLf & _
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
                                              ",:WorkCD" & vbCrLf & _
                                              ",:WorkKbnCD" & vbCrLf & _
                                              ",:ChgFlg" & vbCrLf & _
                                              ",:ChgCINmb" & vbCrLf & _
                                              ",:WorkBiko" & vbCrLf & _
                                              ",:RegDT " & vbCrLf & _
                                              ",:RegGrpCD " & vbCrLf & _
                                              ",:RegID " & vbCrLf & _
                                              ",:UpdateDT " & vbCrLf & _
                                              ",:UpGrpCD " & vbCrLf & _
                                              ",:UpdateID " & vbCrLf & _
                                              ") "

    '登録時履歴より登録理由履歴新規登録（INSERT）用SQL：作業完了／取消前
    Private strInsertBefRegReasonSql As String = _
                                              "INSERT INTO REGREASON_RTB ( " & vbCrLf & _
                                              " CINmb " & vbCrLf & _
                                              ",RirekiNo " & vbCrLf & _
                                              ",RegReason " & vbCrLf & _
                                              ",WorkCD " & vbCrLf & _
                                              ",WorkKbnCD " & vbCrLf & _
                                              ",ChgFlg " & vbCrLf & _
                                              ",ChgCINmb " & vbCrLf & _
                                              ",WorkBiko " & vbCrLf & _
                                              ",RegDT " & vbCrLf & _
                                              ",RegGrpCD " & vbCrLf & _
                                              ",RegID " & vbCrLf & _
                                              ",UpdateDT " & vbCrLf & _
                                              ",UpGrpCD " & vbCrLf & _
                                              ",UpdateID " & vbCrLf & _
                                              ") " & vbCrLf & _
                                              "SELECT" & vbCrLf & _
                                              " rt.CINmb " & vbCrLf & _
                                              ",:RirekiNo " & vbCrLf & _
                                              ",rt.RegReason " & vbCrLf & _
                                              ",rt.WorkCD" & vbCrLf & _
                                              ",rt.WorkKbnCD" & vbCrLf & _
                                              ",rt.ChgFlg" & vbCrLf & _
                                              ",rt.ChgCINmb" & vbCrLf & _
                                              ",rt.WorkBiko" & vbCrLf & _
                                              ",:RegDT " & vbCrLf & _
                                              ",:RegGrpCD " & vbCrLf & _
                                              ",:RegID " & vbCrLf & _
                                              ",:UpdateDT " & vbCrLf & _
                                              ",:UpGrpCD " & vbCrLf & _
                                              ",:UpdateID " & vbCrLf & _
                                              "FROM REGREASON_RTB rt" & vbCrLf & _
                                              "WHERE rt.CINmb = :CINmb" & vbCrLf & _
                                              "  AND rt.RirekiNo = :RegRirekiNo" & vbCrLf

    '【編集モード】登録理由履歴更新（UPDATE）用SQL：交換設置時
    '★Private strUpdateRegReasonFromPairSql As String = _
    '                                          "UPDATE REGREASON_RTB rt1 SET" & vbCrLf & _
    '                                          " RegReason  = t.RegReason" & vbCrLf & _
    '                                          ",WorkCD     = t.WorkCD" & vbCrLf & _
    '                                          ",WorkKbnCD  = t.WorkKbnCD" & vbCrLf & _
    '                                          ",ChgFlg     = :ChgFlg" & vbCrLf & _
    '                                          ",ChgCINmb   = :ChgCINmb" & vbCrLf & _
    '                                          ",UpdateDT   = :UpdateDT" & vbCrLf & _
    '                                          ",UpGrpCD    = :UpGrpCD" & vbCrLf & _
    '                                          ",UpdateID   = :UpdateID" & vbCrLf & _
    '                                          "FROM (SELECT *" & vbCrLf & _
    '                                          "      FROM REGREASON_RTB rt2" & vbCrLf & _
    '                                          "      WHERE rt2.CINmb = :CINmb_From" & vbCrLf & _
    '                                          "        AND rt2.RirekiNo = :LastUpRirekiNo_From" & vbCrLf & _
    '                                          "     ) t" & vbCrLf & _
    '                                          "WHERE rt1.CINmb = :CINmb_To" & vbCrLf & _
    '                                          "  AND rt1.RirekiNo = :LastUpRirekiNo_To" & vbCrLf
    Private strUpdateRegReasonFromPairSql As String = _
                                              "UPDATE REGREASON_RTB rt1 SET" & vbCrLf & _
                                              " ChgFlg     = :ChgFlg" & vbCrLf & _
                                              ",ChgCINmb   = :ChgCINmb" & vbCrLf & _
                                              ",WorkBiko   = :WorkBiko" & vbCrLf & _
                                              "WHERE rt1.CINmb = :CINmb_To" & vbCrLf & _
                                              "  AND rt1.RirekiNo = :LastUpRirekiNo_To + 1" & vbCrLf

    '【編集モード】登録理由履歴更新（UPDATE）用SQL：作業完了
    Private strUpdateRegReasonSql_Complete As String = _
                                              "UPDATE REGREASON_RTB rt SET" & vbCrLf & _
                                              " WorkKbnCD  = '" & WORK_KBN_CD_COMPLETE & "'" & vbCrLf & _
                                              ",ChgFlg     = :ChgFlg" & vbCrLf & _
                                              ",ChgCINmb   = :ChgCINmb" & vbCrLf & _
                                              ",WorkBiko   = :WorkBiko" & vbCrLf & _
                                              ",UpdateDT   = :UpdateDT" & vbCrLf & _
                                              ",UpGrpCD    = :UpGrpCD" & vbCrLf & _
                                              ",UpdateID   = :UpdateID" & vbCrLf & _
                                              "WHERE rt.CINmb = :CINmb" & vbCrLf & _
                                              "  AND rt.RirekiNo = (SELECT MAX(ct.RirekiNo) FROM CI_INFO_RTB ct WHERE ct.CINmb = rt.CINmb)" & vbCrLf

    '【編集モード】登録理由履歴更新（UPDATE）用SQL：作業取消
    Private strUpdateRegReasonSql_Cancel As String = _
                                              "UPDATE REGREASON_RTB rt SET" & vbCrLf & _
                                              " WorkKbnCD  = '" & WORK_KBN_CD_CANCEL & "'" & vbCrLf & _
                                              ",ChgFlg     = :ChgFlg" & vbCrLf & _
                                              ",ChgCINmb   = :ChgCINmb" & vbCrLf & _
                                              ",WorkBiko   = :WorkBiko" & vbCrLf & _
                                              ",UpdateDT   = :UpdateDT" & vbCrLf & _
                                              ",UpGrpCD    = :UpGrpCD" & vbCrLf & _
                                              ",UpdateID   = :UpdateID" & vbCrLf & _
                                              "WHERE rt.CINmb = :CINmb" & vbCrLf & _
                                              "  AND rt.RirekiNo = (SELECT MAX(ct.RirekiNo) FROM CI_INFO_RTB ct WHERE ct.CINmb = rt.CINmb)" & vbCrLf

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

    '登録時履歴より原因リンク新規登録（INSERT）用SQL
    Private strInsertBefCauseLinkSql As String = _
                                              "INSERT INTO CAUSELINK_RTB ( " & vbCrLf & _
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
                                              "SELECT" & vbCrLf & _
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
                                              "FROM CAUSELINK_RTB ct" & vbCrLf & _
                                              "WHERE ct.CINmb = :CINmb" & vbCrLf & _
                                              "  AND ct.RirekiNo = :RegRirekiNo" & vbCrLf

    'CIサポセン機器の最終お知らせ日更新（UPDATE）用SQL：メール作成時
    Private strUpdateLastInfoDtForSapSql As String = _
                                        "UPDATE CI_SAP_TB SET" & vbCrLf & _
                                        "  LastInfoDT = TO_CHAR(Now(),'YYYYMMDD')" & vbCrLf & _
                                        " ,UpdateDT   = :UpdateDT " & vbCrLf & _
                                        " ,UpGrpCD    = :UpGrpCD " & vbCrLf & _
                                        " ,UpdateID   = :UpdateID " & vbCrLf & _
                                        "WHERE CINmb = :CINmb "

    'CI部所有機器の最終お知らせ日更新（UPDATE）用SQL：メール作成時
    Private strUpdateLastInfoDtForBuySql As String = _
                                        "UPDATE CI_BUY_TB SET" & vbCrLf & _
                                        "  LastInfoDT = TO_CHAR(Now(),'YYYYMMDD')" & vbCrLf & _
                                        " ,UpdateDT   = :UpdateDT " & vbCrLf & _
                                        " ,UpGrpCD    = :UpGrpCD " & vbCrLf & _
                                        " ,UpdateID   = :UpdateID " & vbCrLf & _
                                        "WHERE CINmb = :CINmb "

    'CI部所有機器履歴新規登録（INSERT）用SQL：メール作成時
    Private strInsertCIBuyRirekiSql As String = _
                                           "INSERT INTO CI_BUY_RTB ( " & vbCrLf & _
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

    '担当履歴情報取得SQL
    Private strSelectTantoHistorySql As String = _
                                          "SELECT Incdent_tanto_rireki_tb ( " & vbCrLf & _
                                          " IncNmb " & vbCrLf & _
                                          ",TantoRirekiNmb " & vbCrLf & _
                                          ",TantoGrpCD " & vbCrLf & _
                                          ",TantoGrpNM " & vbCrLf & _
                                          ",IncTantoID " & vbCrLf & _
                                          ",IncTantoNM" & vbCrLf & _
                                          ",RegDT " & vbCrLf & _
                                          ",RegGrpCD " & vbCrLf & _
                                          ",RegID " & vbCrLf & _
                                          ",UpdateDT " & vbCrLf & _
                                          ",UpGrpCD " & vbCrLf & _
                                          ",UpdateID "

    'インシデント_相手連絡先直近取得SQL
    Private strSelectIncdentPartnerContactSql As String = _
                                          "SELECT partnercontact " & vbCrLf & _
                                          "FROM incident_info_tb " & vbCrLf & _
                                          "WHERE COALESCE(partnercontact,'')<>'' " & vbCrLf & _
                                          "AND partnerid=:endusrid " & vbCrLf & _
                                          "ORDER BY updatedt DESC " & vbCrLf & _
                                          "LIMIT 1 OFFSET 0 "

    'インシデントSM通知テーブル取得（SELECT）SQL	
    Private strSelectIncidentSMtutiTableSql As String = "SELECT " & vbCrLf & _
                                                        " Count(ist.IncNmb) " & vbCrLf & _
                                                        "FROM incident_sm_tuti_tb ist " & vbCrLf & _
                                                        "WHERE ist.IncNmb = :IncNmb "

    'インシデントSM通知ログテーブル登録（INSERT）SQL
    Private strInsertIncidentSMtutiLsql As String = "INSERT INTO incident_sm_tuti_ltb ( " & vbCrLf & _
                                                    " smnmb " & vbCrLf & _
                                                    ",incnmb " & vbCrLf & _
                                                    ",logno " & vbCrLf & _
                                                    ",incstate " & vbCrLf & _
                                                    ",usrbusyonm " & vbCrLf & _
                                                    ",iraiusr " & vbCrLf & _
                                                    ",tel " & vbCrLf & _
                                                    ",mailadd " & vbCrLf & _
                                                    ",title " & vbCrLf & _
                                                    ",ukenaiyo " & vbCrLf & _
                                                    ",inctantonm " & vbCrLf & _
                                                    ",kind " & vbCrLf & _
                                                    ",category " & vbCrLf & _
                                                    ",subcategory " & vbCrLf & _
                                                    ",impact " & vbCrLf & _
                                                    ",usrsyuticlass " & vbCrLf & _
                                                    ",genin " & vbCrLf & _
                                                    ",zanteisyotinaiyo " & vbCrLf & _
                                                    ",solution " & vbCrLf & _
                                                    ",bikos1 " & vbCrLf & _
                                                    ",bikos2 " & vbCrLf & _
                                                    ",bikom1 " & vbCrLf & _
                                                    ",bikom2 " & vbCrLf & _
                                                    ",bikol1 " & vbCrLf & _
                                                    ",bikol2 " & vbCrLf & _
                                                    ",yobidt1 " & vbCrLf & _
                                                    ",yobidt2 " & vbCrLf & _
                                                    ",renkeidt " & vbCrLf & _
                                                    ",renkeikbn " & vbCrLf & _
                                                    ",RegDT " & vbCrLf & _
                                                    ",RegGrpCD " & vbCrLf & _
                                                    ",RegID " & vbCrLf & _
                                                    ",UpdateDT " & vbCrLf & _
                                                    ",UpGrpCD " & vbCrLf & _
                                                    ",UpdateID " & vbCrLf & _
                                                    ") " & vbCrLf & _
                                                    "SELECT  " & vbCrLf & _
                                                     " smnmb " & vbCrLf & _
                                                    ",incnmb " & vbCrLf & _
                                                    ",:logno " & vbCrLf & _
                                                    ",incstate " & vbCrLf & _
                                                    ",usrbusyonm " & vbCrLf & _
                                                    ",iraiusr " & vbCrLf & _
                                                    ",tel " & vbCrLf & _
                                                    ",mailadd " & vbCrLf & _
                                                    ",title " & vbCrLf & _
                                                    ",ukenaiyo " & vbCrLf & _
                                                    ",inctantonm " & vbCrLf & _
                                                    ",kind " & vbCrLf & _
                                                    ",category " & vbCrLf & _
                                                    ",subcategory " & vbCrLf & _
                                                    ",impact " & vbCrLf & _
                                                    ",usrsyuticlass " & vbCrLf & _
                                                    ",genin " & vbCrLf & _
                                                    ",zanteisyotinaiyo " & vbCrLf & _
                                                    ",solution " & vbCrLf & _
                                                    ",bikos1 " & vbCrLf & _
                                                    ",bikos2 " & vbCrLf & _
                                                    ",bikom1 " & vbCrLf & _
                                                    ",bikom2 " & vbCrLf & _
                                                    ",bikol1 " & vbCrLf & _
                                                    ",bikol2 " & vbCrLf & _
                                                    ",yobidt1 " & vbCrLf & _
                                                    ",yobidt2 " & vbCrLf & _
                                                    ",renkeidt " & vbCrLf & _
                                                    ",renkeikbn " & vbCrLf & _
                                                    ",RegDT " & vbCrLf & _
                                                    ",RegGrpCD " & vbCrLf & _
                                                    ",RegID " & vbCrLf & _
                                                    ",UpdateDT " & vbCrLf & _
                                                    ",UpGrpCD " & vbCrLf & _
                                                    ",UpdateID " & vbCrLf & _
                                                    "FROM incident_sm_tuti_tb " & vbCrLf & _
                                                    "WHERE incnmb=:incnmb "

    ''' <summary>
    ''' 【共通】マスタデータ取得：受付手段
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>受付手段コンボボックス取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetCmbKindMstData(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectUketsukeMstSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '[add] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 START
            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))        'インシデント番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                      'インシデント番号
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
    ''' 【共通】マスタデータ取得：インシデント種別
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>インシデント種別コンボボックス取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetCmbIncKbnMstData(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectINCKindMstSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '[add] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 START
            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))        'インシデント番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                      'インシデント番号
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
    ''' 【共通】マスタデータ取得：プロセスステータス
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ステータスコンボボックス取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetCmbProcessStateMstData(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Parameters("processkbn").Value = "001"                      'プロセス区分
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
    ''' 【共通】マスタデータ取得：ドメイン
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ドメインコンボボックス取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetCmbDomeinMstData(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectDomeinMstSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '[add] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 START
            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))        'インシデント番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                      'インシデント番号
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
    ''' 【共通】マスタデータ取得：グループ
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>グループコンボボックス取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetINCSTantoMastaData(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))        'インシデント番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                      'インシデント番号
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
    ''' 【共通】マスタデータ取得：経過種別
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>経過種別コンボボックス取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetINCkeikaMastaData(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectKeikaKindMstSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '[add] 2013/03/18 y.ikushima マスタデータ削除フラグ対応 START
            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))        'インシデント番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                      'インシデント番号
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
    ''' 【共通】データ取得：対象システム
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>対象システムコンボボックス取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetINCsystemMastaData(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Add(New NpgsqlParameter("cistatuscd", NpgsqlTypes.NpgsqlDbType.Varchar))       '廃止済
                .Add(New NpgsqlParameter("cikbncd", NpgsqlTypes.NpgsqlDbType.Varchar))          'システム
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
    ''' 【共通】マスタデータ取得：相手先
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>指定相手先取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetPartnerInfoData(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectPartnerMstSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("endusrid", NpgsqlTypes.NpgsqlDbType.Varchar))        '相手ID
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("endusrid").Value = dataHBKC0201.PropStrSeaKey                      '相手ID
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
    ''' 【共通】マスタデータ取得：担当
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>指定ユーザー取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetIncTantoInfoData(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectTantoMstSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("hbkusrid", NpgsqlTypes.NpgsqlDbType.Varchar))        '担当ID
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("hbkusrid").Value = dataHBKC0201.PropStrSeaKey                      '担当ID
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
    ''' 【共通】機器情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>指定ユーザー取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/07 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetKikiInfoData(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectKikiInfoSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("cinmb", NpgsqlTypes.NpgsqlDbType.Integer))             'CI番号
                .Add(New NpgsqlParameter("kikistate1", NpgsqlTypes.NpgsqlDbType.Varchar))        '機器ステータス
                .Add(New NpgsqlParameter("kikistate2", NpgsqlTypes.NpgsqlDbType.Varchar))        '機器ステータス
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("cinmb").Value = dataHBKC0201.PropStrSeaKey                     'CI番号
                .Parameters("kikistate1").Value = KIKISTATE_NO_INPUT                        '機器状態なし
                .Parameters("kikistate2").Value = KIKISTATE_INPUT                           '機器状態あり
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
    ''' 【編集／参照／作業履歴モード】共通情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>共通報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectIncMainSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectIncMainSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("INCNmb", NpgsqlTypes.NpgsqlDbType.Integer))        'INC番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("INCNmb").Value = dataHBKC0201.PropIntINCNmb                      'INC番号
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
    ''' 【編集／参照モード】担当履歴取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>担当履歴情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/10 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectTantoRirekiSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Add(New NpgsqlParameter("INCNmb", NpgsqlTypes.NpgsqlDbType.Integer))        'INC番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("INCNmb").Value = dataHBKC0201.PropIntINCNmb                      'INC番号
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
    ''' 【編集／参照モード】作業履歴取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>INC共通情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectIncRirekiSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectIncRirekiSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("INCNmb", NpgsqlTypes.NpgsqlDbType.Integer))        'INC番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("INCNmb").Value = dataHBKC0201.PropIntINCNmb                      'INC番号
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
    ''' 【編集／参照モード】作業担当情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>作業担当情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectIncTantoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectIncTantoSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("INCNmb", NpgsqlTypes.NpgsqlDbType.Integer))        'INC番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("INCNmb").Value = dataHBKC0201.PropIntINCNmb                      'INC番号
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
    ''' 【編集／参照モード】機器情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>機器情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectIncKikiSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectIncKikiSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("INCNmb", NpgsqlTypes.NpgsqlDbType.Integer))        'INC番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("INCNmb").Value = dataHBKC0201.PropIntINCNmb                      'INC番号
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
    ''' 【編集／参照モード】対応関係者情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>対応関係者通情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectIncKankeiSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectIncKankeiSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("INCNmb", NpgsqlTypes.NpgsqlDbType.Integer))           'INC番号
                .Add(New NpgsqlParameter("KbnGrp", NpgsqlTypes.NpgsqlDbType.Varchar))           '区分：グループ
                .Add(New NpgsqlParameter("KbnUsr", NpgsqlTypes.NpgsqlDbType.Varchar))           '区分：ユーザー
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("INCNmb").Value = dataHBKC0201.PropIntINCNmb                        'INC番号
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
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>プロセスリンク情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectPLinkSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Add(New NpgsqlParameter("INCNmb", NpgsqlTypes.NpgsqlDbType.Integer))            'INC番号
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
                .Parameters("INCNmb").Value = dataHBKC0201.PropIntINCNmb                         'INC番号
                .Parameters("LinkMotoProcesskbn").Value = PROCESS_TYPE_INCIDENT                  'プロセス区分：インシデント
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
    ''' 【編集／参照モード】関連ファイル情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>関連ファイル情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectIncFileSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectIncFileSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("INCNmb", NpgsqlTypes.NpgsqlDbType.Integer))        'INC番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("INCNmb").Value = dataHBKC0201.PropIntINCNmb                      'INC番号
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
    ''' 【編集／参照モード】借用物取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>借用物取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetSelectSyakuyouSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectSyakuyouSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("UsrID", NpgsqlTypes.NpgsqlDbType.Varchar))         '相手ID
                .Add(New NpgsqlParameter("KikiUseCD", NpgsqlTypes.NpgsqlDbType.Varchar))     '稼働中
                .Add(New NpgsqlParameter("cikbncd", NpgsqlTypes.NpgsqlDbType.Varchar))       'サポセン機器
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("UsrID").Value = dataHBKC0201.PropTxtPartnerID.Text         '相手ID
                .Parameters("KikiUseCD").Value = CI_STATUS_SYSTEM_KADOUCHU              '稼働中
                .Parameters("cikbncd").Value = CI_TYPE_SUPORT                           'サポセン機器
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
    ''' 【編集／参照モード】会議情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectMeetingSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Parameters("processkbn").Value = PROCESS_TYPE_INCIDENT
                .Parameters("processnmb").Value = dataHBKC0201.PropIntINCNmb                      'INC番号

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
    ''' 【新規登録モード】新規INC番号、サーバー日付取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規INC番号、サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewINCNmbAndSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                   ByVal Cn As NpgsqlConnection, _
                                                   ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = GET_NEXTVAL_INCIDENT_NO

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
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>共通情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertINCInfoSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Dim strBikoAimai As String = ""         'フリーテキスト（あいまい）


        Try

            'SQL文(INSERT)
            strSQL = strInsertIncInfoSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("incnmb", NpgsqlTypes.NpgsqlDbType.Integer))            'INC番号
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))        'プロセス区分
                .Add(New NpgsqlParameter("UkeKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '受付手段CD
                .Add(New NpgsqlParameter("IncKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))          'インシデント種別CD
                .Add(New NpgsqlParameter("ProcessStateCD", NpgsqlTypes.NpgsqlDbType.Varchar))    'プロセスステータスCD
                .Add(New NpgsqlParameter("HasseiDT", NpgsqlTypes.NpgsqlDbType.Timestamp))        '発生日時
                .Add(New NpgsqlParameter("KaitoDT", NpgsqlTypes.NpgsqlDbType.Timestamp))         '回答日時
                .Add(New NpgsqlParameter("KanryoDT", NpgsqlTypes.NpgsqlDbType.Timestamp))        '完了日時
                .Add(New NpgsqlParameter("Priority", NpgsqlTypes.NpgsqlDbType.Varchar))          '重要度
                .Add(New NpgsqlParameter("Errlevel", NpgsqlTypes.NpgsqlDbType.Varchar))          '障害レベル
                .Add(New NpgsqlParameter("Title", NpgsqlTypes.NpgsqlDbType.Varchar))             'タイトル
                .Add(New NpgsqlParameter("UkeNaiyo", NpgsqlTypes.NpgsqlDbType.Varchar))          '受付内容
                .Add(New NpgsqlParameter("TaioKekka", NpgsqlTypes.NpgsqlDbType.Varchar))         '対応結果
                .Add(New NpgsqlParameter("SystemNmb", NpgsqlTypes.NpgsqlDbType.Integer))         '対象システム番号
                .Add(New NpgsqlParameter("OutSideToolNmb", NpgsqlTypes.NpgsqlDbType.Varchar))    '外部ツール番号
                .Add(New NpgsqlParameter("EventID", NpgsqlTypes.NpgsqlDbType.Varchar))           'イベントID
                .Add(New NpgsqlParameter("Source", NpgsqlTypes.NpgsqlDbType.Varchar))            'ソース
                .Add(New NpgsqlParameter("OPCEventID", NpgsqlTypes.NpgsqlDbType.Varchar))        'OPCイベントID
                .Add(New NpgsqlParameter("EventClass", NpgsqlTypes.NpgsqlDbType.Varchar))        'イベントクラス
                .Add(New NpgsqlParameter("TantoGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))        '担当グループCD
                .Add(New NpgsqlParameter("IncTantoID", NpgsqlTypes.NpgsqlDbType.Varchar))        'インシデント担当者ID
                .Add(New NpgsqlParameter("IncTantoNM", NpgsqlTypes.NpgsqlDbType.Varchar))        'インシデント担当者氏名
                .Add(New NpgsqlParameter("DomainCD", NpgsqlTypes.NpgsqlDbType.Varchar))          'ドメインCD
                .Add(New NpgsqlParameter("PartnerCompany", NpgsqlTypes.NpgsqlDbType.Varchar))    '相手会社名
                .Add(New NpgsqlParameter("PartnerID", NpgsqlTypes.NpgsqlDbType.Varchar))         '相手ID
                .Add(New NpgsqlParameter("PartnerNM", NpgsqlTypes.NpgsqlDbType.Varchar))         '相手氏名
                .Add(New NpgsqlParameter("PartnerKana", NpgsqlTypes.NpgsqlDbType.Varchar))       '相手シメイ
                .Add(New NpgsqlParameter("PartnerKyokuNM", NpgsqlTypes.NpgsqlDbType.Varchar))    '相手局
                .Add(New NpgsqlParameter("UsrBusyoNM", NpgsqlTypes.NpgsqlDbType.Varchar))        '相手部署
                .Add(New NpgsqlParameter("PartnerTel", NpgsqlTypes.NpgsqlDbType.Varchar))        '相手電話番号
                .Add(New NpgsqlParameter("PartnerMailAdd", NpgsqlTypes.NpgsqlDbType.Varchar))    '相手メールアドレス
                .Add(New NpgsqlParameter("PartnerContact", NpgsqlTypes.NpgsqlDbType.Varchar))    '相手連絡先
                .Add(New NpgsqlParameter("PartnerBase", NpgsqlTypes.NpgsqlDbType.Varchar))       '相手拠点
                .Add(New NpgsqlParameter("PartnerRoom", NpgsqlTypes.NpgsqlDbType.Varchar))       '相手番組/部屋
                .Add(New NpgsqlParameter("ShijisyoFlg", NpgsqlTypes.NpgsqlDbType.Varchar))       '指示書フラグ
                '.Add(New NpgsqlParameter("GroupRireki", NpgsqlTypes.NpgsqlDbType.Varchar))       'グループ履歴
                '.Add(New NpgsqlParameter("TantoRireki", NpgsqlTypes.NpgsqlDbType.Varchar))       '担当者履歴
                .Add(New NpgsqlParameter("Kengen", NpgsqlTypes.NpgsqlDbType.Varchar))            '権限
                .Add(New NpgsqlParameter("RentalKiki", NpgsqlTypes.NpgsqlDbType.Varchar))        '借用物
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
                .Add(New NpgsqlParameter("TitleAimai", NpgsqlTypes.NpgsqlDbType.Varchar))        'タイトル（あいまい）
                .Add(New NpgsqlParameter("UkeNaiyoAimai", NpgsqlTypes.NpgsqlDbType.Varchar))     '受付内容（あいまい）
                .Add(New NpgsqlParameter("BikoAimai", NpgsqlTypes.NpgsqlDbType.Varchar))         'フリーテキスト（あいまい）
                .Add(New NpgsqlParameter("TaioKekkaAimai", NpgsqlTypes.NpgsqlDbType.Varchar))    '対応結果(あいまい)
                .Add(New NpgsqlParameter("EventIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))      'イベントID(あいまい)
                .Add(New NpgsqlParameter("SourceAimai", NpgsqlTypes.NpgsqlDbType.Varchar))       'ソース(あいまい)
                .Add(New NpgsqlParameter("OPCEventIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))   'OPCイベントID(あいまい)
                .Add(New NpgsqlParameter("EventClassAimai", NpgsqlTypes.NpgsqlDbType.Varchar))   'イベントクラス(あいまい)
                .Add(New NpgsqlParameter("IncTantIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))    'インシデント担当者ID(あいまい)
                .Add(New NpgsqlParameter("IncTantNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))    'インシデント担当者氏名(あいまい)
                .Add(New NpgsqlParameter("PartnerIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))    '相手ID(あいまい)
                .Add(New NpgsqlParameter("PartnerNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))    '相手氏名(あいまい)
                .Add(New NpgsqlParameter("UsrBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))   '相手部署(あいまい)

                .Add(New NpgsqlParameter("kigencondcikbncd", NpgsqlTypes.NpgsqlDbType.Varchar))   '期限切れ条件CI種別
                .Add(New NpgsqlParameter("kigencondtypekbn", NpgsqlTypes.NpgsqlDbType.Varchar))   '期限切れ条件タイプ
                .Add(New NpgsqlParameter("kigencondkigen", NpgsqlTypes.NpgsqlDbType.Varchar))     '期限切れ条件期限
                .Add(New NpgsqlParameter("KigenCondUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))     '期限切れ条件ユーザID

                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))        '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))           '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者ID
            End With
            'バインド変数に値をセット

            With Cmd
                .Parameters("incnmb").Value = dataHBKC0201.PropIntINCNmb                                                  '新規IN番号
                .Parameters("ProcessKbn").Value = PROCESS_TYPE_INCIDENT                                                   'プロセス区分
                .Parameters("UkeKbnCD").Value = dataHBKC0201.PropCmbUkeKbn.SelectedValue                                  '受付手段CD(ComboBox)
                .Parameters("IncKbnCD").Value = dataHBKC0201.PropCmbIncKbnCD.SelectedValue                                'インシデント種別CD(ComboBox)
                .Parameters("ProcessStateCD").Value = dataHBKC0201.PropCmbprocessStateCD.SelectedValue                    'プロセスステータスCD(ComboBox)
                '発生日時
                If dataHBKC0201.PropDtpHasseiDT.txtDate.Text.Equals("") Then
                    .Parameters("HasseiDT").Value = Nothing
                Else
                    .Parameters("HasseiDT").Value = _
                        CDate(dataHBKC0201.PropDtpHasseiDT.txtDate.Text & " " & dataHBKC0201.PropTxtHasseiDT_HM.PropTxtTime.Text)
                End If
                '回答日時
                If dataHBKC0201.PropDtpKaitoDT.txtDate.Text.Equals("") Then
                    .Parameters("KaitoDT").Value = Nothing
                Else
                    .Parameters("KaitoDT").Value = _
                        CDate(dataHBKC0201.PropDtpKaitoDT.txtDate.Text & " " & dataHBKC0201.PropTxtKaitoDT_HM.PropTxtTime.Text)
                End If
                '完了日時
                If dataHBKC0201.PropDtpKanryoDT.txtDate.Text.Equals("") Then
                    'ステータスが完了ならばシステム日付を設定する
                    If dataHBKC0201.PropCmbprocessStateCD.SelectedValue = PROCESS_STATUS_INCIDENT_KANRYOU Then
                        .Parameters("KanryoDT").Value = dataHBKC0201.PropDtmSysDate
                    Else
                        .Parameters("KanryoDT").Value = Nothing
                    End If
                Else
                    .Parameters("KanryoDT").Value = _
                        CDate(dataHBKC0201.PropDtpKanryoDT.txtDate.Text & " " & dataHBKC0201.PropTxtKanryoDT_HM.PropTxtTime.Text)
                End If
                .Parameters("Priority").Value = dataHBKC0201.PropTxtPriority.Text                                         '重要度
                .Parameters("Errlevel").Value = dataHBKC0201.PropTxtErrlevel.Text                                         '障害レベル
                .Parameters("Title").Value = dataHBKC0201.PropTxtTitle.Text                                               'タイトル
                .Parameters("UkeNaiyo").Value = dataHBKC0201.PropTxtUkeNaiyo.Text                                         '受付内容
                .Parameters("TaioKekka").Value = dataHBKC0201.PropTxtTaioKekka.Text                                       '対応結果
                .Parameters("SystemNmb").Value = dataHBKC0201.PropCmbSystemNmb.PropCmbColumns.SelectedValue               '対象システム番号(ComboBoxEx)
                .Parameters("OutSideToolNmb").Value = dataHBKC0201.PropTxtOutSideToolNmb.Text                             '外部ツール番号
                .Parameters("EventID").Value = dataHBKC0201.PropTxtEventID.Text                                           'イベントID
                .Parameters("Source").Value = dataHBKC0201.PropTxtSource.Text                                             'ソース
                .Parameters("OPCEventID").Value = dataHBKC0201.PropTxtOPCEventID.Text                                     'OPCイベントID
                .Parameters("EventClass").Value = dataHBKC0201.PropTxtEventClass.Text                                     'イベントクラス
                .Parameters("TantoGrpCD").Value = dataHBKC0201.PropCmbTantoGrpCD.SelectedValue                            '担当グループCD(ComboBox)
                .Parameters("IncTantoID").Value = dataHBKC0201.PropTxtIncTantoCD.Text                                     'インシデント担当者ID
                .Parameters("IncTantoNM").Value = dataHBKC0201.PropTxtIncTantoNM.Text                                     'インシデント担当者氏名
                .Parameters("DomainCD").Value = dataHBKC0201.PropCmbDomainCD.SelectedValue                                'ドメインCD(ComboBox)
                .Parameters("PartnerCompany").Value = dataHBKC0201.PropTxtPartnerCompany.Text                             '相手会社名
                .Parameters("PartnerID").Value = dataHBKC0201.PropTxtPartnerID.Text                                       '相手ID
                .Parameters("PartnerNM").Value = dataHBKC0201.PropTxtPartnerNM.Text                                       '相手氏名
                .Parameters("PartnerKana").Value = dataHBKC0201.PropTxtPartnerKana.Text                                   '相手シメイ
                .Parameters("PartnerKyokuNM").Value = dataHBKC0201.PropTxtPartnerKyokuNM.Text                             '相手局
                .Parameters("UsrBusyoNM").Value = dataHBKC0201.PropTxtPartnerBusyoNM.Text                                 '相手部署
                .Parameters("PartnerTel").Value = dataHBKC0201.PropTxtPartnerTel.Text                                     '相手電話番号
                .Parameters("PartnerMailAdd").Value = dataHBKC0201.PropTxtPartnerMailAdd.Text                             '相手メールアドレス
                .Parameters("PartnerContact").Value = dataHBKC0201.PropTxtPartnerContact.Text                             '相手連絡先
                .Parameters("PartnerBase").Value = dataHBKC0201.PropTxtPartnerBase.Text                                   '相手拠点
                .Parameters("PartnerRoom").Value = dataHBKC0201.PropTxtPartnerRoom.Text                                   '相手番組/部屋
                '指示書フラグ
                If dataHBKC0201.PropChkShijisyoFlg.Checked = True Then
                    .Parameters("ShijisyoFlg").Value = FREE_FLG_ON
                Else
                    .Parameters("ShijisyoFlg").Value = FREE_FLG_OFF
                End If
                '.Parameters("GroupRireki").Value = dataHBKC0201.PropCmbTantoGrpCD.Text                                    'グループ履歴(グループ名）
                '.Parameters("TantoRireki").Value = dataHBKC0201.PropTxtIncTantoNM.Text                                    '担当者履歴（ユーザ名）
                .Parameters("Kengen").Value = dataHBKC0201.PropTxtKengen.Text                                             '権限
                .Parameters("RentalKiki").Value = dataHBKC0201.PropTxtRentalKiki.Text                                     '借用物

                .Parameters("BIko1").Value = dataHBKC0201.PropTxtBIko1.Text                     'フリーテキスト１
                .Parameters("Biko2").Value = dataHBKC0201.PropTxtBIko2.Text                     'フリーテキスト２
                .Parameters("BIko3").Value = dataHBKC0201.PropTxtBIko3.Text                     'フリーテキスト３
                .Parameters("Biko4").Value = dataHBKC0201.PropTxtBIko4.Text                     'フリーテキスト４
                .Parameters("Biko5").Value = dataHBKC0201.PropTxtBIko5.Text                     'フリーテキスト５

                'フリーフラグ１～５
                If dataHBKC0201.PropChkFreeFlg1.Checked = True Then
                    .Parameters("FreeFlg1").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg1").Value = FREE_FLG_OFF
                End If
                If dataHBKC0201.PropChkFreeFlg2.Checked = True Then
                    .Parameters("FreeFlg2").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg2").Value = FREE_FLG_OFF
                End If
                If dataHBKC0201.PropChkFreeFlg3.Checked = True Then
                    .Parameters("FreeFlg3").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg3").Value = FREE_FLG_OFF
                End If
                If dataHBKC0201.PropChkFreeFlg4.Checked = True Then
                    .Parameters("FreeFlg4").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg4").Value = FREE_FLG_OFF
                End If
                If dataHBKC0201.PropChkFreeFlg5.Checked = True Then
                    .Parameters("FreeFlg5").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg5").Value = FREE_FLG_OFF
                End If

                'あいまい検索文字列設定
                '※フリーテキスト（あいまい）：フリーテキスト１～５を連結
                strBikoAimai = commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtBIko1.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtBIko2.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtBIko3.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtBIko4.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtBIko5.Text)
                .Parameters("TitleAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtTitle.Text)                  'タイトル（あいまい）
                .Parameters("UkeNaiyoAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtUkeNaiyo.Text)            '受付内容（あいまい）
                .Parameters("BikoAimai").Value = strBikoAimai                                                                           'フリーテキスト（あいまい）
                .Parameters("TaioKekkaAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtTaioKekka.Text)          '対応結果(あいまい)
                .Parameters("EventIDAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtEventID.Text)              'イベントID(あいまい)
                .Parameters("SourceAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtSource.Text)                'ソース(あいまい)
                .Parameters("OPCEventIDAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtOPCEventID.Text)        'OPCイベントID(あいまい)
                .Parameters("EventClassAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtEventClass.Text)        'イベントクラス(あいまい)
                .Parameters("IncTantIDAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtIncTantoCD.Text)         'インシデント担当者ID(あいまい)
                .Parameters("IncTantNMAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtIncTantoNM.Text)         'インシデント担当者氏名(あいまい)
                .Parameters("PartnerIDAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtPartnerID.Text)          '相手ID(あいまい)
                .Parameters("PartnerNMAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtPartnerNM.Text)          '相手氏名(あいまい)
                .Parameters("UsrBusyoNMAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtPartnerBusyoNM.Text)    '相手部署(あいまい)

                .Parameters("kigencondcikbncd").Value = dataHBKC0201.PropTxtkigencondcikbncd                                            '期限切れ条件CI種別
                .Parameters("kigencondtypekbn").Value = dataHBKC0201.PropTxtkigencondtypekbn                                            '期限切れ条件タイプ
                .Parameters("kigencondkigen").Value = dataHBKC0201.PropTxtkigencondkigen                                                '期限切れ条件期限
                .Parameters("KigenCondUsrID").Value = dataHBKC0201.PropTxtKigenCondUsrID                                                '期限切れ条件ユーザID

                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                                                                '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                                                         '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                                                 '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                                                             '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                                                          '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                                                              '最終更新者ID
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
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>担当履歴情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/10 r.hoshino
    ''' <p>改訂情報</p>
    ''' </para></remarks>
    Public Function SetInsertTantoRirekiSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))           'inc番号
                .Add(New NpgsqlParameter("tantogrpcd", NpgsqlTypes.NpgsqlDbType.Varchar))       'グループcd
                .Add(New NpgsqlParameter("tantogrpnm", NpgsqlTypes.NpgsqlDbType.Varchar))       'グループ名
                .Add(New NpgsqlParameter("inctantoid", NpgsqlTypes.NpgsqlDbType.Varchar))       '担当ＩＤ
                .Add(New NpgsqlParameter("inctantonm", NpgsqlTypes.NpgsqlDbType.Varchar))       '担当名

                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))        '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))           '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb
                .Parameters("tantogrpcd").Value = dataHBKC0201.PropCmbTantoGrpCD.SelectedValue
                .Parameters("tantogrpnm").Value = dataHBKC0201.PropCmbTantoGrpCD.Text
                .Parameters("inctantoid").Value = dataHBKC0201.PropTxtIncTantoCD.Text
                .Parameters("inctantonm").Value = dataHBKC0201.PropTxtIncTantoNM.Text

                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                         '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                  '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                          '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                      '最終更新日時
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
    ''' 【新規登録／編集／作業履歴モード】作業履歴情報 新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>作業履歴情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertINCRirekiSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Dim strworknaiyoaimai As String = ""   '作業内容(あいまい)

        Try

            'SQL文(INSERT)
            strSQL = strInsertIncRirekiSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))            'inc番号

                .Add(New NpgsqlParameter("keikakbncd", NpgsqlTypes.NpgsqlDbType.Varchar))        '経過種別
                .Add(New NpgsqlParameter("worknaiyo", NpgsqlTypes.NpgsqlDbType.Varchar))         '作業内容
                .Add(New NpgsqlParameter("workscedt", NpgsqlTypes.NpgsqlDbType.Timestamp))       '予定日時
                .Add(New NpgsqlParameter("workstdt", NpgsqlTypes.NpgsqlDbType.Timestamp))        '開始日時
                .Add(New NpgsqlParameter("workeddt", NpgsqlTypes.NpgsqlDbType.Timestamp))        '終了日時
                .Add(New NpgsqlParameter("systemnmb", NpgsqlTypes.NpgsqlDbType.Integer))         '対象システム
                .Add(New NpgsqlParameter("worknaiyoaimai", NpgsqlTypes.NpgsqlDbType.Varchar))    '相手部署(あいまい)

                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))        '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))           '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb

                .Parameters("keikakbncd").Value = dataHBKC0201.PropRowReg.Item("keikakbncd")                    '経過種別
                .Parameters("worknaiyo").Value = dataHBKC0201.PropRowReg.Item("worknaiyo")                      '作業内容
                '予定日時
                If dataHBKC0201.PropRowReg.Item("workscedt").ToString.Equals("") Then

                Else
                    .Parameters("workscedt").Value = DateTime.Parse(dataHBKC0201.PropRowReg.Item("workscedt"))
                End If
                '開始日時
                If dataHBKC0201.PropRowReg.Item("workstdt").ToString.Equals("") Then

                Else
                    .Parameters("workstdt").Value = DateTime.Parse(dataHBKC0201.PropRowReg.Item("workstdt"))
                End If
                '終了日時
                If dataHBKC0201.PropRowReg.Item("workeddt").ToString.Equals("") Then

                Else
                    .Parameters("workeddt").Value = DateTime.Parse(dataHBKC0201.PropRowReg.Item("workeddt"))
                End If

                .Parameters("systemnmb").Value = dataHBKC0201.PropRowReg.Item("systemnmb")                      '対象システム
                'あいまい検索文字列設定
                .Parameters("worknaiyoaimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropRowReg.Item("worknaiyo").ToString)    '作業内容(あいまい)

                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                         '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                  '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                          '登録者ID

                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                      '最終更新日時
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
    ''' 【新規登録／編集／作業履歴モード】作業履歴情報 更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>作業履歴情報更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/11 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateINCRirekiSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Dim strworknaiyoaimai As String = ""   '作業内容(あいまい)

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateIncRirekiSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))            'inc番号
                .Add(New NpgsqlParameter("workrirekinmb", NpgsqlTypes.NpgsqlDbType.Integer))     '履歴番号
                .Add(New NpgsqlParameter("keikakbncd", NpgsqlTypes.NpgsqlDbType.Varchar))        '経過種別
                .Add(New NpgsqlParameter("worknaiyo", NpgsqlTypes.NpgsqlDbType.Varchar))         '作業内容
                .Add(New NpgsqlParameter("workscedt", NpgsqlTypes.NpgsqlDbType.Timestamp))       '予定日時
                .Add(New NpgsqlParameter("workstdt", NpgsqlTypes.NpgsqlDbType.Timestamp))        '開始日時
                .Add(New NpgsqlParameter("workeddt", NpgsqlTypes.NpgsqlDbType.Timestamp))        '終了日時
                .Add(New NpgsqlParameter("systemnmb", NpgsqlTypes.NpgsqlDbType.Integer))         '対象システム
                .Add(New NpgsqlParameter("worknaiyoaimai", NpgsqlTypes.NpgsqlDbType.Varchar))    '相手部署(あいまい)

                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))        '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))           '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb
                .Parameters("workrirekinmb").Value = dataHBKC0201.PropRowReg.Item("workrirekinmb")
                .Parameters("keikakbncd").Value = dataHBKC0201.PropRowReg.Item("keikakbncd")                    '経過種別
                .Parameters("worknaiyo").Value = dataHBKC0201.PropRowReg.Item("worknaiyo")                      '作業内容
                '予定日時
                If dataHBKC0201.PropRowReg.Item("workscedt").ToString.Equals("") Then

                Else
                    .Parameters("workscedt").Value = DateTime.Parse(dataHBKC0201.PropRowReg.Item("workscedt"))
                End If
                '開始日時
                If dataHBKC0201.PropRowReg.Item("workstdt").ToString.Equals("") Then

                Else
                    .Parameters("workstdt").Value = DateTime.Parse(dataHBKC0201.PropRowReg.Item("workstdt"))
                End If
                '終了日時
                If dataHBKC0201.PropRowReg.Item("workeddt").ToString.Equals("") Then

                Else
                    .Parameters("workeddt").Value = DateTime.Parse(dataHBKC0201.PropRowReg.Item("workeddt"))
                End If

                .Parameters("systemnmb").Value = dataHBKC0201.PropRowReg.Item("systemnmb")                      '対象システム
                'あいまい検索文字列設定
                .Parameters("worknaiyoaimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropRowReg.Item("worknaiyo").ToString)    '作業内容(あいまい)

                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                      '最終更新日時
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
    ''' 【新規登録／編集／作業履歴モード】作業担当情報　新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>作業担当情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報：2012/07/31 r.hoshino 名称を追加</p>
    ''' </para></remarks>
    Public Function SetInsertINCTantoSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201, ByVal ColCnt As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Dim strworknaiyoaimai As String = ""   '作業内容(あいまい)

        Try

            'SQL文(INSERT)
            strSQL = strInsertIncTantoSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))            'inc番号

                .Add(New NpgsqlParameter("worktantonmb", NpgsqlTypes.NpgsqlDbType.Integer))      '担当番号
                .Add(New NpgsqlParameter("worktantogrpcd", NpgsqlTypes.NpgsqlDbType.Varchar))    '担当G
                .Add(New NpgsqlParameter("worktantoid", NpgsqlTypes.NpgsqlDbType.Varchar))       '担当U
                .Add(New NpgsqlParameter("worktantogrpnm", NpgsqlTypes.NpgsqlDbType.Varchar))    '担当GNM
                .Add(New NpgsqlParameter("worktantonm", NpgsqlTypes.NpgsqlDbType.Varchar))       '担当UNM

                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))        '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))           '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb

                .Parameters("worktantonmb").Value = ColCnt
                .Parameters("worktantogrpcd").Value = dataHBKC0201.PropRowReg.Item("worktantogrpcd" & ColCnt)
                .Parameters("worktantoid").Value = dataHBKC0201.PropRowReg.Item("worktantoid" & ColCnt)
                .Parameters("worktantogrpnm").Value = dataHBKC0201.PropRowReg.Item("worktantogrpnm" & ColCnt)
                .Parameters("worktantonm").Value = dataHBKC0201.PropRowReg.Item("worktantonm" & ColCnt)

                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                         '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                  '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                          '登録者ID

                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                      '最終更新日時
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
    ''' 【新規登録／編集／作業履歴モード】作業担当情報　更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>作業担当情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/11 r.hoshino
    ''' <p>改訂情報：2012/07/31 r.hoshino 名称を追加</p>
    ''' </para></remarks>
    Public Function SetUpdateINCTantoSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201, ByVal ColCnt As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Dim strworknaiyoaimai As String = ""   '作業内容(あいまい)

        Try

            'SQL文(INSERT)
            strSQL = strUpdateIncTantoSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))            'inc番号
                .Add(New NpgsqlParameter("workrirekinmb", NpgsqlTypes.NpgsqlDbType.Integer))     '履歴番号
                .Add(New NpgsqlParameter("worktantonmb", NpgsqlTypes.NpgsqlDbType.Integer))      '担当番号
                .Add(New NpgsqlParameter("worktantogrpcd", NpgsqlTypes.NpgsqlDbType.Varchar))    '担当G
                .Add(New NpgsqlParameter("worktantoid", NpgsqlTypes.NpgsqlDbType.Varchar))       '担当U
                .Add(New NpgsqlParameter("worktantogrpnm", NpgsqlTypes.NpgsqlDbType.Varchar))    '担当GNM
                .Add(New NpgsqlParameter("worktantonm", NpgsqlTypes.NpgsqlDbType.Varchar))       '担当UNM

                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))        '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))           '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb
                .Parameters("workrirekinmb").Value = dataHBKC0201.PropRowReg.Item("workrirekinmb")
                .Parameters("worktantonmb").Value = ColCnt
                .Parameters("worktantogrpcd").Value = dataHBKC0201.PropRowReg.Item("worktantogrpcd" & ColCnt)
                .Parameters("worktantoid").Value = dataHBKC0201.PropRowReg.Item("worktantoid" & ColCnt)
                .Parameters("worktantogrpnm").Value = dataHBKC0201.PropRowReg.Item("worktantogrpnm" & ColCnt)
                .Parameters("worktantonm").Value = dataHBKC0201.PropRowReg.Item("worktantonm" & ColCnt)

                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                         '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                  '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                          '登録者ID

                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                      '最終更新日時
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
    ''' 【新規登録／編集／参照履歴モード】機器情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>機器情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertINCkikiSql(ByRef Cmd As NpgsqlCommand, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertInckikiSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("incNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'INC番号
                .Add(New NpgsqlParameter("kindcd", NpgsqlTypes.NpgsqlDbType.Varchar))                   '種別CD
                .Add(New NpgsqlParameter("num", NpgsqlTypes.NpgsqlDbType.Varchar))                      '番号
                .Add(New NpgsqlParameter("kikiinf", NpgsqlTypes.NpgsqlDbType.Varchar))                  '機器情報

                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                  '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                 '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                    '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("incNmb").Value = dataHBKC0201.PropIntINCNmb                                'INC番号
                .Parameters("kindcd").Value = dataHBKC0201.PropRowReg.Item("kindcd")                    '種別CD
                .Parameters("num").Value = dataHBKC0201.PropRowReg.Item("num")                          '番号
                .Parameters("kikiinf").Value = dataHBKC0201.PropRowReg.Item("kikiinf")                  '機器情報

                If dataHBKC0201.PropRowReg.Item("RegDT").ToString.Length > 0 Then
                    .Parameters("RegDT").Value = dataHBKC0201.PropRowReg.Item("RegDT")
                    .Parameters("RegGrpCD").Value = dataHBKC0201.PropRowReg.Item("RegGrpCD")
                    .Parameters("RegID").Value = dataHBKC0201.PropRowReg.Item("RegID")
                Else
                    .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                         '登録日時
                    .Parameters("RegGrpCD").Value = PropWorkGroupCD                                  '登録者グループCD
                    .Parameters("RegID").Value = PropUserId                                          '登録者ID
                End If

                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                             '最終更新日時
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
    ''' 【新規登録／編集モード】対応関係者情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>対応関係者情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertINCKankeiSql(ByRef Cmd As NpgsqlCommand, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertIncKankeiSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("incNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'INC番号
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
                .Parameters("incNmb").Value = dataHBKC0201.PropIntINCNmb                                'INC番号
                .Parameters("RelationKbn").Value = dataHBKC0201.PropRowReg.Item("RelationKbn")          '関係区分
                .Parameters("RelationID").Value = dataHBKC0201.PropRowReg.Item("RelationID")            '関係ID

                If dataHBKC0201.PropRowReg.Item("RegDT").ToString.Length > 0 Then
                    .Parameters("RegDT").Value = dataHBKC0201.PropRowReg.Item("RegDT")
                    .Parameters("RegGrpCD").Value = dataHBKC0201.PropRowReg.Item("RegGrpCD")
                    .Parameters("RegID").Value = dataHBKC0201.PropRowReg.Item("RegID")
                Else
                    .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                         '登録日時
                    .Parameters("RegGrpCD").Value = PropWorkGroupCD                                  '登録者グループCD
                    .Parameters("RegID").Value = PropUserId                                          '登録者ID
                End If
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                             '最終更新日時
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
    ''' 【新規登録モード】プロセスリンク(元)情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>プロセスリンク情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InsertPLinkMoto(ByRef Cmd As NpgsqlCommand, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKC0201 As DataHBKC0201, ByVal count As Integer) As Boolean

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
                .Add(New NpgsqlParameter("LinkMotoNmb", NpgsqlTypes.NpgsqlDbType.Integer))              '元INC番号
                .Add(New NpgsqlParameter("LinkSakiprocesskbn", NpgsqlTypes.NpgsqlDbType.Varchar))       '先P区分
                .Add(New NpgsqlParameter("LinkSakiNmb", NpgsqlTypes.NpgsqlDbType.Integer))              '先INC番号
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
                .Parameters("LinkMotoprocesskbn").Value = PROCESS_TYPE_INCIDENT                                 '元P区分
                .Parameters("LinkMotoNmb").Value = dataHBKC0201.PropIntINCNmb                                   '元INC番号
                .Parameters("LinkSakiprocesskbn").Value = dataHBKC0201.PropRowReg.Item("processkbn")            '参照先P区分
                .Parameters("LinkSakiNmb").Value = dataHBKC0201.PropRowReg.Item("MngNmb")                       '参照先INC番号
                .Parameters("EntryDT").Value = dataHBKC0201.PropDtmSysDate.AddMilliseconds(count)               '登録順
                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                                     '更新日時
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
    ''' 【編集モード】プロセスリンク(元)情報削除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>プロセスリンク情報削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function DeletePLinkMoto(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Add(New NpgsqlParameter("LinkMotoNmb", NpgsqlTypes.NpgsqlDbType.Integer))               'INC番号
                .Add(New NpgsqlParameter("LinkMotoProcesskbn", NpgsqlTypes.NpgsqlDbType.Varchar))        '区分
                .Add(New NpgsqlParameter("LinkSakiNmb", NpgsqlTypes.NpgsqlDbType.Integer))               'INC番号
                .Add(New NpgsqlParameter("LinkSakiProcesskbn", NpgsqlTypes.NpgsqlDbType.Varchar))        '区分
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("LinkMotoNmb").Value = dataHBKC0201.PropIntINCNmb                            'INC番号
                .Parameters("LinkMotoProcesskbn").Value = PROCESS_TYPE_INCIDENT
                .Parameters("LinkSakiNmb").Value = dataHBKC0201.PropRowReg("MngNmb", DataRowVersion.Original)
                .Parameters("LinkSakiProcesskbn").Value = dataHBKC0201.PropRowReg("processkbn", DataRowVersion.Original)
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
    ''' 【編集モード】プロセスリンク(先)情報削除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>プロセスリンク(先)情報削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function DeletePLinkSaki(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Add(New NpgsqlParameter("LinkMotoNmb", NpgsqlTypes.NpgsqlDbType.Integer))               'INC番号
                .Add(New NpgsqlParameter("LinkMotoProcesskbn", NpgsqlTypes.NpgsqlDbType.Varchar))        '区分
                .Add(New NpgsqlParameter("LinkSakiNmb", NpgsqlTypes.NpgsqlDbType.Integer))               'INC番号
                .Add(New NpgsqlParameter("LinkSakiProcesskbn", NpgsqlTypes.NpgsqlDbType.Varchar))        '区分
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("LinkMotoNmb").Value = dataHBKC0201.PropRowReg("MngNmb", DataRowVersion.Original)
                .Parameters("LinkMotoProcesskbn").Value = dataHBKC0201.PropRowReg("processkbn", DataRowVersion.Original)
                .Parameters("LinkSakiNmb").Value = dataHBKC0201.PropIntINCNmb
                .Parameters("LinkSakiProcesskbn").Value = PROCESS_TYPE_INCIDENT
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
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議結果情報更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/30 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertMtgResultSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Parameters("MeetingNmb").Value = dataHBKC0201.PropRowReg.Item("MeetingNmb")    '会議番号
                .Parameters("ProcessKbn").Value = PROCESS_TYPE_INCIDENT                         'プロセス区分
                .Parameters("ProcessNmb").Value = dataHBKC0201.PropIntINCNmb                    'プロセス番号
                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                         '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                  '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                          '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                     '最終更新日時
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
    ''' 【編集モード】会議結果情報更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議結果情報更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteMtgResultSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Add(New NpgsqlParameter("processkbn", NpgsqlTypes.NpgsqlDbType.Varchar))               'プロセス区分
                .Add(New NpgsqlParameter("meetingnmb", NpgsqlTypes.NpgsqlDbType.Integer))               '会議番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("processnmb").Value = dataHBKC0201.PropIntINCNmb
                .Parameters("processkbn").Value = PROCESS_TYPE_INCIDENT
                .Parameters("meetingnmb").Value = dataHBKC0201.PropRowReg.Item("meetingnmb", DataRowVersion.Original)
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
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/27 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
    ''' 【編集モード】INC共通情報更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>INC共通情報更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateINCInfoSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Dim strBikoAimai As String = ""         'フリーテキスト（あいまい）

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateIncInfoSql

            'データアダプタに、SQLのUPDATE文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("incnmb", NpgsqlTypes.NpgsqlDbType.Integer))            'INC番号
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))        'プロセス区分
                .Add(New NpgsqlParameter("UkeKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '受付手段CD
                .Add(New NpgsqlParameter("IncKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))          'インシデント種別CD
                .Add(New NpgsqlParameter("ProcessStateCD", NpgsqlTypes.NpgsqlDbType.Varchar))    'プロセスステータスCD
                .Add(New NpgsqlParameter("HasseiDT", NpgsqlTypes.NpgsqlDbType.Timestamp))        '発生日時
                .Add(New NpgsqlParameter("KaitoDT", NpgsqlTypes.NpgsqlDbType.Timestamp))         '回答日時
                .Add(New NpgsqlParameter("KanryoDT", NpgsqlTypes.NpgsqlDbType.Timestamp))        '完了日時
                .Add(New NpgsqlParameter("Priority", NpgsqlTypes.NpgsqlDbType.Varchar))          '重要度
                .Add(New NpgsqlParameter("Errlevel", NpgsqlTypes.NpgsqlDbType.Varchar))          '障害レベル
                .Add(New NpgsqlParameter("Title", NpgsqlTypes.NpgsqlDbType.Varchar))             'タイトル
                .Add(New NpgsqlParameter("UkeNaiyo", NpgsqlTypes.NpgsqlDbType.Varchar))          '受付内容
                .Add(New NpgsqlParameter("TaioKekka", NpgsqlTypes.NpgsqlDbType.Varchar))         '対応結果
                .Add(New NpgsqlParameter("SystemNmb", NpgsqlTypes.NpgsqlDbType.Integer))         '対象システム番号
                .Add(New NpgsqlParameter("OutSideToolNmb", NpgsqlTypes.NpgsqlDbType.Varchar))    '外部ツール番号
                .Add(New NpgsqlParameter("EventID", NpgsqlTypes.NpgsqlDbType.Varchar))           'イベントID
                .Add(New NpgsqlParameter("Source", NpgsqlTypes.NpgsqlDbType.Varchar))            'ソース
                .Add(New NpgsqlParameter("OPCEventID", NpgsqlTypes.NpgsqlDbType.Varchar))        'OPCイベントID
                .Add(New NpgsqlParameter("EventClass", NpgsqlTypes.NpgsqlDbType.Varchar))        'イベントクラス
                .Add(New NpgsqlParameter("TantoGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))        '担当グループCD
                .Add(New NpgsqlParameter("IncTantoID", NpgsqlTypes.NpgsqlDbType.Varchar))        'インシデント担当者ID
                .Add(New NpgsqlParameter("IncTantoNM", NpgsqlTypes.NpgsqlDbType.Varchar))        'インシデント担当者氏名
                .Add(New NpgsqlParameter("DomainCD", NpgsqlTypes.NpgsqlDbType.Varchar))          'ドメインCD
                .Add(New NpgsqlParameter("PartnerCompany", NpgsqlTypes.NpgsqlDbType.Varchar))    '相手会社名
                .Add(New NpgsqlParameter("PartnerID", NpgsqlTypes.NpgsqlDbType.Varchar))         '相手ID
                .Add(New NpgsqlParameter("PartnerNM", NpgsqlTypes.NpgsqlDbType.Varchar))         '相手氏名
                .Add(New NpgsqlParameter("PartnerKana", NpgsqlTypes.NpgsqlDbType.Varchar))       '相手シメイ
                .Add(New NpgsqlParameter("PartnerKyokuNM", NpgsqlTypes.NpgsqlDbType.Varchar))    '相手局
                .Add(New NpgsqlParameter("UsrBusyoNM", NpgsqlTypes.NpgsqlDbType.Varchar))        '相手部署
                .Add(New NpgsqlParameter("PartnerTel", NpgsqlTypes.NpgsqlDbType.Varchar))        '相手電話番号
                .Add(New NpgsqlParameter("PartnerMailAdd", NpgsqlTypes.NpgsqlDbType.Varchar))    '相手メールアドレス
                .Add(New NpgsqlParameter("PartnerContact", NpgsqlTypes.NpgsqlDbType.Varchar))    '相手連絡先
                .Add(New NpgsqlParameter("PartnerBase", NpgsqlTypes.NpgsqlDbType.Varchar))       '相手拠点
                .Add(New NpgsqlParameter("PartnerRoom", NpgsqlTypes.NpgsqlDbType.Varchar))       '相手番組/部屋
                .Add(New NpgsqlParameter("ShijisyoFlg", NpgsqlTypes.NpgsqlDbType.Varchar))       '指示書フラグ
                '.Add(New NpgsqlParameter("GroupRireki", NpgsqlTypes.NpgsqlDbType.Varchar))       'グループ履歴
                '.Add(New NpgsqlParameter("TantoRireki", NpgsqlTypes.NpgsqlDbType.Varchar))       '担当者履歴
                .Add(New NpgsqlParameter("Kengen", NpgsqlTypes.NpgsqlDbType.Varchar))            '権限
                .Add(New NpgsqlParameter("RentalKiki", NpgsqlTypes.NpgsqlDbType.Varchar))        '借用物
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
                .Add(New NpgsqlParameter("TitleAimai", NpgsqlTypes.NpgsqlDbType.Varchar))        'タイトル（あいまい）
                .Add(New NpgsqlParameter("UkeNaiyoAimai", NpgsqlTypes.NpgsqlDbType.Varchar))     '受付内容（あいまい）
                .Add(New NpgsqlParameter("BikoAimai", NpgsqlTypes.NpgsqlDbType.Varchar))         'フリーテキスト（あいまい）
                .Add(New NpgsqlParameter("TaioKekkaAimai", NpgsqlTypes.NpgsqlDbType.Varchar))    '対応結果(あいまい)
                .Add(New NpgsqlParameter("EventIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))      'イベントID(あいまい)
                .Add(New NpgsqlParameter("SourceAimai", NpgsqlTypes.NpgsqlDbType.Varchar))       'ソース(あいまい)
                .Add(New NpgsqlParameter("OPCEventIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))   'OPCイベントID(あいまい)
                .Add(New NpgsqlParameter("EventClassAimai", NpgsqlTypes.NpgsqlDbType.Varchar))   'イベントクラス(あいまい)
                .Add(New NpgsqlParameter("IncTantIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))    'インシデント担当者ID(あいまい)
                .Add(New NpgsqlParameter("IncTantNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))    'インシデント担当者氏名(あいまい)
                .Add(New NpgsqlParameter("PartnerIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))    '相手ID(あいまい)
                .Add(New NpgsqlParameter("PartnerNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))    '相手氏名(あいまい)
                .Add(New NpgsqlParameter("UsrBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))   '相手部署(あいまい)

                .Add(New NpgsqlParameter("kigencondcikbncd", NpgsqlTypes.NpgsqlDbType.Varchar))   '期限切れ条件CI種別
                .Add(New NpgsqlParameter("kigencondtypekbn", NpgsqlTypes.NpgsqlDbType.Varchar))   '期限切れ条件タイプ
                .Add(New NpgsqlParameter("kigencondkigen", NpgsqlTypes.NpgsqlDbType.Varchar))     '期限切れ条件期限
                .Add(New NpgsqlParameter("KigenCondUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))     '期限切れ条件ユーザID

                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))        '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))           '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("incnmb").Value = dataHBKC0201.PropIntINCNmb                                                  'INC番号
                .Parameters("ProcessKbn").Value = PROCESS_TYPE_INCIDENT                                                   'プロセス区分
                .Parameters("UkeKbnCD").Value = dataHBKC0201.PropCmbUkeKbn.SelectedValue                                  '受付手段CD(ComboBox)
                .Parameters("IncKbnCD").Value = dataHBKC0201.PropCmbIncKbnCD.SelectedValue                                'インシデント種別CD(ComboBox)
                .Parameters("ProcessStateCD").Value = dataHBKC0201.PropCmbprocessStateCD.SelectedValue                    'プロセスステータスCD(ComboBox)
                '発生日時
                If dataHBKC0201.PropDtpHasseiDT.txtDate.Text.Equals("") Then
                    .Parameters("HasseiDT").Value = Nothing
                Else
                    .Parameters("HasseiDT").Value = _
                        CDate(dataHBKC0201.PropDtpHasseiDT.txtDate.Text & " " & dataHBKC0201.PropTxtHasseiDT_HM.PropTxtTime.Text)
                End If
                '回答日時
                If dataHBKC0201.PropDtpKaitoDT.txtDate.Text.Equals("") Then
                    .Parameters("KaitoDT").Value = Nothing
                Else
                    .Parameters("KaitoDT").Value = _
                        CDate(dataHBKC0201.PropDtpKaitoDT.txtDate.Text & " " & dataHBKC0201.PropTxtKaitoDT_HM.PropTxtTime.Text)
                End If
                '完了日時
                If dataHBKC0201.PropDtpKanryoDT.txtDate.Text.Equals("") Then
                    'ステータスが完了ならばシステム日付を設定する
                    If dataHBKC0201.PropCmbprocessStateCD.SelectedValue = PROCESS_STATUS_INCIDENT_KANRYOU Then
                        .Parameters("KanryoDT").Value = dataHBKC0201.PropDtmSysDate
                    Else
                        .Parameters("KanryoDT").Value = Nothing
                    End If
                Else
                    .Parameters("KanryoDT").Value = _
                        CDate(dataHBKC0201.PropDtpKanryoDT.txtDate.Text & " " & dataHBKC0201.PropTxtKanryoDT_HM.PropTxtTime.Text)
                End If
                .Parameters("Priority").Value = dataHBKC0201.PropTxtPriority.Text                                         '重要度
                .Parameters("Errlevel").Value = dataHBKC0201.PropTxtErrlevel.Text                                         '障害レベル
                .Parameters("Title").Value = dataHBKC0201.PropTxtTitle.Text                                               'タイトル
                .Parameters("UkeNaiyo").Value = dataHBKC0201.PropTxtUkeNaiyo.Text                                         '受付内容
                .Parameters("TaioKekka").Value = dataHBKC0201.PropTxtTaioKekka.Text                                       '対応結果
                .Parameters("SystemNmb").Value = dataHBKC0201.PropCmbSystemNmb.PropCmbColumns.SelectedValue               '対象システム番号(ComboBoxEx)
                .Parameters("OutSideToolNmb").Value = dataHBKC0201.PropTxtOutSideToolNmb.Text                             '外部ツール番号
                .Parameters("EventID").Value = dataHBKC0201.PropTxtEventID.Text                                           'イベントID
                .Parameters("Source").Value = dataHBKC0201.PropTxtSource.Text                                             'ソース
                .Parameters("OPCEventID").Value = dataHBKC0201.PropTxtOPCEventID.Text                                     'OPCイベントID
                .Parameters("EventClass").Value = dataHBKC0201.PropTxtEventClass.Text                                     'イベントクラス
                .Parameters("TantoGrpCD").Value = dataHBKC0201.PropCmbTantoGrpCD.SelectedValue                            '担当グループCD(ComboBox)
                .Parameters("IncTantoID").Value = dataHBKC0201.PropTxtIncTantoCD.Text                                     'インシデント担当者ID
                .Parameters("IncTantoNM").Value = dataHBKC0201.PropTxtIncTantoNM.Text                                     'インシデント担当者氏名
                .Parameters("DomainCD").Value = dataHBKC0201.PropCmbDomainCD.SelectedValue                                'ドメインCD
                .Parameters("PartnerCompany").Value = dataHBKC0201.PropTxtPartnerCompany.Text                             '相手会社名
                .Parameters("PartnerID").Value = dataHBKC0201.PropTxtPartnerID.Text                                       '相手ID
                .Parameters("PartnerNM").Value = dataHBKC0201.PropTxtPartnerNM.Text                                       '相手氏名
                .Parameters("PartnerKana").Value = dataHBKC0201.PropTxtPartnerKana.Text                                   '相手シメイ
                .Parameters("PartnerKyokuNM").Value = dataHBKC0201.PropTxtPartnerKyokuNM.Text                             '相手局
                .Parameters("UsrBusyoNM").Value = dataHBKC0201.PropTxtPartnerBusyoNM.Text                                 '相手部署
                .Parameters("PartnerTel").Value = dataHBKC0201.PropTxtPartnerTel.Text                                     '相手電話番号
                .Parameters("PartnerMailAdd").Value = dataHBKC0201.PropTxtPartnerMailAdd.Text                             '相手メールアドレス
                .Parameters("PartnerContact").Value = dataHBKC0201.PropTxtPartnerContact.Text                             '相手連絡先
                .Parameters("PartnerBase").Value = dataHBKC0201.PropTxtPartnerBase.Text                                   '相手拠点
                .Parameters("PartnerRoom").Value = dataHBKC0201.PropTxtPartnerRoom.Text                                   '相手番組/部屋
                If dataHBKC0201.PropChkShijisyoFlg.Checked = True Then                                                    '指示書フラグ
                    .Parameters("ShijisyoFlg").Value = FREE_FLG_ON
                Else
                    .Parameters("ShijisyoFlg").Value = FREE_FLG_OFF
                End If
                '.Parameters("GroupRireki").Value = dataHBKC0201.PropTxtGrpHistory.Text                                    'グループ履歴
                '.Parameters("TantoRireki").Value = dataHBKC0201.PropTxtTantoHistory.Text                                  '担当者履歴
                .Parameters("Kengen").Value = dataHBKC0201.PropTxtKengen.Text                                             '権限
                .Parameters("RentalKiki").Value = dataHBKC0201.PropTxtRentalKiki.Text                                     '借用物

                .Parameters("BIko1").Value = dataHBKC0201.PropTxtBIko1.Text                     'フリーテキスト１
                .Parameters("Biko2").Value = dataHBKC0201.PropTxtBIko2.Text                     'フリーテキスト２
                .Parameters("BIko3").Value = dataHBKC0201.PropTxtBIko3.Text                     'フリーテキスト３
                .Parameters("Biko4").Value = dataHBKC0201.PropTxtBIko4.Text                     'フリーテキスト４
                .Parameters("Biko5").Value = dataHBKC0201.PropTxtBIko5.Text                     'フリーテキスト５

                'フリーフラグ１～５
                If dataHBKC0201.PropChkFreeFlg1.Checked = True Then
                    .Parameters("FreeFlg1").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg1").Value = FREE_FLG_OFF
                End If
                If dataHBKC0201.PropChkFreeFlg2.Checked = True Then
                    .Parameters("FreeFlg2").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg2").Value = FREE_FLG_OFF
                End If
                If dataHBKC0201.PropChkFreeFlg3.Checked = True Then
                    .Parameters("FreeFlg3").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg3").Value = FREE_FLG_OFF
                End If
                If dataHBKC0201.PropChkFreeFlg4.Checked = True Then
                    .Parameters("FreeFlg4").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg4").Value = FREE_FLG_OFF
                End If
                If dataHBKC0201.PropChkFreeFlg5.Checked = True Then
                    .Parameters("FreeFlg5").Value = FREE_FLG_ON
                Else
                    .Parameters("FreeFlg5").Value = FREE_FLG_OFF
                End If

                'あいまい検索文字列設定
                '※フリーテキスト（あいまい）：フリーテキスト１～５を連結
                strBikoAimai = commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtBIko1.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtBIko2.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtBIko3.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtBIko4.Text) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtBIko5.Text)
                .Parameters("TitleAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtTitle.Text)                  'タイトル（あいまい）
                .Parameters("UkeNaiyoAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtUkeNaiyo.Text)            '受付内容（あいまい）
                .Parameters("BikoAimai").Value = strBikoAimai                                                                           'フリーテキスト（あいまい）
                .Parameters("TaioKekkaAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtTaioKekka.Text)          '対応結果(あいまい)
                .Parameters("EventIDAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtEventID.Text)              'イベントID(あいまい)
                .Parameters("SourceAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtSource.Text)                'ソース(あいまい)
                .Parameters("OPCEventIDAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtOPCEventID.Text)        'OPCイベントID(あいまい)
                .Parameters("EventClassAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtEventClass.Text)        'イベントクラス(あいまい)
                .Parameters("IncTantIDAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtIncTantoCD.Text)         'インシデント担当者ID(あいまい)
                .Parameters("IncTantNMAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtIncTantoNM.Text)         'インシデント担当者氏名(あいまい)
                .Parameters("PartnerIDAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtPartnerID.Text)          '相手ID(あいまい)
                .Parameters("PartnerNMAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtPartnerNM.Text)          '相手氏名(あいまい)
                .Parameters("UsrBusyoNMAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKC0201.PropTxtPartnerBusyoNM.Text)    '相手部署(あいまい)

                .Parameters("kigencondcikbncd").Value = dataHBKC0201.PropTxtkigencondcikbncd                    '期限切れ条件CI種別
                .Parameters("kigencondtypekbn").Value = dataHBKC0201.PropTxtkigencondtypekbn                    '期限切れ条件タイプ
                .Parameters("kigencondkigen").Value = dataHBKC0201.PropTxtkigencondkigen                        '期限切れ条件期限
                .Parameters("KigenCondUsrID").Value = dataHBKC0201.PropTxtKigenCondUsrID                        '期限切れ条件ユーザID

                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                                     '最終更新日時
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
    ''' 【編集／作業履歴モード】作業担当削除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>作業担当情報削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteINCTantoSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Try

            'SQL文(UPDATE)
            strSQL = strDeleteIncTantoSql

            'データアダプタに、SQLのDELETE文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))            '管理番号 
                .Add(New NpgsqlParameter("workrirekinmb", NpgsqlTypes.NpgsqlDbType.Integer))     '履歴番号 
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb
                .Parameters("workrirekinmb").Value = dataHBKC0201.PropRowReg.Item("workrirekinmb")
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
    ''' 【編集モード】機器情報削除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>機器情報削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteINCkikiSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Try

            'SQL文(Delete)
            strSQL = strDeleteIncKikiSql

            'データアダプタに、SQLのDelete文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("incNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'INC番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("incNmb").Value = dataHBKC0201.PropIntINCNmb                                'INC番号
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
    ''' 【編集モード】対応関係者情報削除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>対応関係者情報削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteINCkankeiSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Try

            'SQL文(Delete)
            strSQL = strDeleteIncKankeiSql

            'データアダプタに、SQLのDelete文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            With Cmd.Parameters
                .Add(New NpgsqlParameter("incNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'INC番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("incNmb").Value = dataHBKC0201.PropIntINCNmb                                'INC番号
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
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規ログNo取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/23 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewRirekiNoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Parameters("incNmb").Value = dataHBKC0201.PropIntINCNmb                'INC番号
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
    ''' 【共通】新規会議ログNo取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規会議ログNo取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/23 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewMeetingRirekiNoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Parameters("MeetingNmb").Value = dataHBKC0201.PropIntMeetingNmb            '会議番号
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
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>共通情報ログ新規登録用のdataHBKC0201SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/23 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncInfoLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Parameters("LogNo").Value = dataHBKC0201.PropIntLogNo                       'ログNo
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                     'INC番号
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
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>作業履歴ログ新規登録用のdataHBKC0201SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/23 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncRirekiLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Parameters("LogNo").Value = dataHBKC0201.PropIntLogNo                  'ログNo
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                'INC番号
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
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>作業担当ログ新規登録用のdataHBKC0201SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/23 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncTantoLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Parameters("LogNo").Value = dataHBKC0201.PropIntLogNo                  'ログNo
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                'INC番号
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
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>機器情報ログ新規登録用のdataHBKC0201SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/23 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncKikiLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Parameters("LogNo").Value = dataHBKC0201.PropIntLogNo                  'ログNo
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                'INC番号
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
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>対応関係情報ログ新規登録用のdataHBKC0201SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/23 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncKankeiLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Parameters("LogNo").Value = dataHBKC0201.PropIntLogNo                  'ログNo
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                'INC番号
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
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>プロセスリンク情報ログ新規登録用のdataHBKC0201SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/23 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertPLinkmotoLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Parameters("LogNo").Value = dataHBKC0201.PropIntLogNo                  'ログNo
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                'INC番号
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
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>関連ファイル情報ログ新規登録用のdataHBKC0201SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/23 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncFileLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Parameters("LogNo").Value = dataHBKC0201.PropIntLogNo                  'ログNo
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                'INC番号
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
    ' ''' 【共通】サポセン機器メンテナンスログ新規登録用SQLの作成・設定処理
    ' ''' </summary>
    ' ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ' ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ' ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ' ''' <remarks>サポセン機器メンテナンスログ新規登録用のdataHBKC0201SQLを作成し、アダプタにセットする
    ' ''' <para>作成情報：2012/07/30 t.fukuo
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Public Function SetInsertSapMainteLSql(ByRef Cmd As NpgsqlCommand, _
    '                                       ByVal Cn As NpgsqlConnection, _
    '                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数の宣言
    '    Dim strSQL As String = ""               'SQL文

    '    Try

    '        'SQL文(INSERT)
    '        strSQL = strInsertSapMainteLSql

    '        'データアダプタに、SQLのINSERT文を設定
    '        Cmd = New NpgsqlCommand(strSQL, Cn)


    '        'バインド変数に型をセット
    '        With Cmd.Parameters
    '            .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
    '            .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))      '登録日時
    '            .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))     '登録者グループCD
    '            .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))        '登録者ID
    '            .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))   '更新日時
    '            .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))      '最終更新者グループCD
    '            .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))     '最終更新者ID
    '            .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))       'INC番号
    '        End With

    '        'バインド変数に値をセット
    '        With Cmd
    '            .Parameters("LogNo").Value = dataHBKC0201.PropIntLogNo                      'ログNo
    '            .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                    '登録日時
    '            .Parameters("RegGrpCD").Value = PropWorkGroupCD                             '登録者グループCD
    '            .Parameters("RegID").Value = PropUserId                                     '登録者ID
    '            .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                 '更新日時
    '            .Parameters("UpGrpCD").Value = PropWorkGroupCD                              '最終更新者グループCD
    '            .Parameters("UpdateID").Value = PropUserId                                  '最終更新者ID
    '            .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                    'INC番号
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
    ''' 【共通】サポセン機器メンテナンス作業ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サポセン機器メンテナンス作業ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertSapMainteWorkLSql(ByRef Cmd As NpgsqlCommand, _
                                               ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Parameters("LogNo").Value = dataHBKC0201.PropIntLogNo                      'ログNo
                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                    '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                             '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                     '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                 '更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                              '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                  '最終更新者ID
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                    'INC番号
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
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サポセン機器メンテナンス機器ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertSapMainteKikiLSql(ByRef Cmd As NpgsqlCommand, _
                                               ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Parameters("LogNo").Value = dataHBKC0201.PropIntLogNo                      'ログNo
                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                    '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                             '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                     '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                 '更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                              '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                  '最終更新者ID
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                    'INC番号
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
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議情報ログ新規登録用のdataHBKC0201SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/23 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertMeetingLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Parameters("LogNo").Value = dataHBKC0201.PropIntLogNoSub                  'ログNo
                .Parameters("MeetingNmb").Value = dataHBKC0201.PropIntMeetingNmb            '会議番号
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
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議結果情報ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/06 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertMtgResultLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Parameters("LogNo").Value = dataHBKC0201.PropIntLogNoSub                   'ログNo
                .Parameters("meetingnmb").Value = dataHBKC0201.PropIntMeetingNmb            '会議No
                .Parameters("processnmb").Value = dataHBKC0201.PropIntINCNmb                'プロセス番号
                .Parameters("processkbn").Value = PROCESS_TYPE_INCIDENT                     'プロセス区分
                .Parameters("ProcessLogNo").Value = dataHBKC0201.PropIntLogNo               'プロセスログ番号
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
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議出席者情報ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertMtgAttendLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Parameters("LogNo").Value = dataHBKC0201.PropIntLogNoSub
                .Parameters("MeetingNmb").Value = dataHBKC0201.PropIntMeetingNmb
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
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議関連ファイル情報ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertMtgFileLSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Parameters("LogNo").Value = dataHBKC0201.PropIntLogNoSub
                .Parameters("MeetingNmb").Value = dataHBKC0201.PropIntMeetingNmb
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
    ''' 【共通】対応関係者取得：対象システム
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>INC共通情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetChkKankeiSysData(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Add(New NpgsqlParameter("INCNmb", NpgsqlTypes.NpgsqlDbType.Integer))                           '対象システム
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("INCNmb").Value = dataHBKC0201.PropCmbSystemNmb.PropCmbColumns.SelectedValue        '対象システム
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
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>INC共通情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/10/11 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetChkSysNmbData(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strCheckIncSystemNmbSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("Nmb", NpgsqlTypes.NpgsqlDbType.Integer))                           '管理番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("Nmb").Value = dataHBKC0201.PropIntINCNmb
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
    ''' 【共通】マスタデータ取得用SQLの作成・設定：作業
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>作業マスタ取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetCmbWorkMstData(ByRef Adapter As NpgsqlDataAdapter, _
                                      ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectWorkMtbSql

            'データアダプタに、SQL文を設定
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
    ''' 【編集／参照／作業履歴モード】サポセン機器メンテナンスデータ取得用SQLの作成・設定
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サポセン機器メンテナンス取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/26 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSapMainteData(ByRef Adapter As NpgsqlDataAdapter, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectSapMainteSql

            'データアダプタに、SQL文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))        'インシデント番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                     'インシデント番号
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
    ''' 【編集モード】CI共通情報.セット機器件数取得用SQLの作成・設定
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報.セット機器の件数取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/27 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetCountSetKikiSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strCountSetKikiSql

            'データアダプタに、SQL文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'CI番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))      'CI番号
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
    ''' 【編集モード】CIサポセン機器.イメージ番号入力件数取得用SQLの作成・設定
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIサポセン機器.イメージ番号の入力件数取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetCountImageNmbIsNotNullSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                 ByVal Cn As NpgsqlConnection, _
                                                 ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strCountImgNmbIsNotNullSql

            'データアダプタに、SQL文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                  'インシデント番号
                .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))                 '作業番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'CI番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                               'インシデント番号
                .Parameters("WorkNmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("WorkNmb"))  '作業番号
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))      'CI番号
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
    ''' 【編集モード】CIサポセン機器.機器状態入力件数取得用SQLの作成・設定
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIサポセン機器.機器状態の入力件数取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetCountKikiStateIsNotNullSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                  ByVal Cn As NpgsqlConnection, _
                                                  ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strCountKikiStateIsNotNullSql

            'データアダプタに、SQL文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                  'インシデント番号
                .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))                 '作業番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'CI番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                               'インシデント番号
                .Parameters("WorkNmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("WorkNmb"))  '作業番号
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))      'CI番号
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
    ''' 【編集モード】CIサポセン機器入力チェック用データ取得用SQLの作成・設定
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIサポセン機器の入力チェック用データ取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCISapSql(ByRef Adapter As NpgsqlDataAdapter, _
                                      ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectCheckCISapKmkSql

            'データアダプタに、SQL文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'CI番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))      'CI番号
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

    '【ADD】2014/04/07 e.okamura 作業取消時セット機器更新修正 START
    ''' <summary>
    ''' 警告メッセージ用現在のセット機器取得用SQLの作成・設定
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>警告メッセージ用の現在のセット機器取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2014/04/07 e.okamura
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCurrentSetKikiSql(ByRef Adapter As NpgsqlDataAdapter, _
                                               ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectCurrentSetKikiSql

            'データアダプタに、SQL文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))               '対象作業のCI番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))  '対象作業のCI番号
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
    ''' 警告メッセージ用作業追加時のセット機器取得用SQLの作成・設定
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>警告メッセージ用の作業追加時セット機器取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2014/04/02 e.okamura
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectPastSetKikiSql(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectPastSetKikiSql

            'データアダプタに、SQL文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))            '対象作業のCI番号
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))         '対象作業の作業追加時履歴No
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))               '対象作業のCI番号
                .Parameters("RirekiNo").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("RegRirekiNo")) + 1  '対象作業の作業追加時履歴No
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
    '【ADD】2014/04/07 e.okamura 作業取消時セット機器更新修正 END

    ''' <summary>
    ''' 【編集モード】CIステータス取得用SQLの作成・設定
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIステータス取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCIStatusSql(ByRef Adapter As NpgsqlDataAdapter, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectCIStateCDSql

            'データアダプタに、SQL文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'インシデント番号
                .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))                  '作業番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                                'インシデント番号
                .Parameters("WorkNmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("WorkNmb"))   '作業番号
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号
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
    ''' 【編集モード】作業前CIステータス取得用SQLの作成・設定
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>作業前（前回履歴）のCIステータス取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/01 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectBefCIStatusSql(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectBefCIStateCDSql

            'データアダプタに、SQL文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'インシデント番号
                .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))                  '作業番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                                'インシデント番号
                .Parameters("WorkNmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("WorkNmb"))   '作業番号
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号
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
    ''' 【編集モード】新規セットID取得用SQLの作成・設定
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規セットID取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewSetKikiIDSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = GET_NEXTVAL_SETKIKI_ID

            'データアダプタに、SQL文を設定
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
    ''' 【編集モード】新規CI（構成管理）履歴番号取得用SQLの作成・設定
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規CI履歴番号取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewCIRirekiNoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                              ByVal Cn As NpgsqlConnection, _
                                              ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))   'CI番号
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
    ''' 【編集モード】CI共通情報.CIステータス更新用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報.CIステータス更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateCIInfo_CIStatusSql(ByRef Cmd As NpgsqlCommand, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateCIStatusSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CIStatusCD", NpgsqlTypes.NpgsqlDbType.Varchar))           'CIステータスコード
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                If dataHBKC0201.PropStrUpdCIStatusCD Is Nothing Then                                'CIステータスコード
                    .Parameters("CIStatusCD").Value = ""
                Else
                    .Parameters("CIStatusCD").Value = dataHBKC0201.PropStrUpdCIStatusCD
                End If
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                         '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))   'CI番号
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
    ' ''' 【編集モード】作業完了時：CI共通情報（保存用）更新用SQLの作成・設定
    ' ''' </summary>
    ' ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ' ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ' ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ' ''' <remarks>作業完了時のCI共通情報（保存用）更新用のSQLを作成し、アダプタにセットする
    ' ''' <para>作成情報：2012/08/07 t.fukuo
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Public Function SetUpdateTmpCIInfoCompleteSql(ByRef Cmd As NpgsqlCommand, _
    '                                              ByVal Cn As NpgsqlConnection, _
    '                                              ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数の宣言
    '    Dim strSQL As String = ""

    '    Try

    '        'SQL文(UPDATE)
    '        strSQL = strUpdateTmpCIInfoSql_Complete

    '        'データアダプタに、SQL文を設定
    '        Cmd = New NpgsqlCommand(strSQL, Cn)


    '        'バインド変数に型をセット
    '        With Cmd.Parameters
    '            .Add(New NpgsqlParameter("CIStatusCD", NpgsqlTypes.NpgsqlDbType.Varchar))               'CIステータスコード
    '            .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
    '            .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
    '            .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
    '            .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'インシデント番号
    '            .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))                  '作業番号
    '            .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
    '        End With

    '        'バインド変数に値をセット
    '        With Cmd
    '            .Parameters("CIStatusCD").Value = dataHBKC0201.PropStrUpdCIStatusCD                     'CIステータスコード
    '            .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                             '最終更新日時
    '            .Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
    '            .Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
    '            .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                                'インシデント番号
    '            .Parameters("WorkNmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("WorkNmb"))   '作業番号
    '            .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号
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
    ''' 【編集モード】作業完了時：CI共通情報更新用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>作業完了時のCI共通情報更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateCIInfoCompleteSql(ByRef Cmd As NpgsqlCommand, _
                                               ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateCIInfoSql_Complete

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CIStatusCD", NpgsqlTypes.NpgsqlDbType.Varchar))               'CIステータスコード
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("CIStatusCD").Value = dataHBKC0201.PropStrUpdCIStatusCD                     'CIステータスコード
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                             '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号
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
    ' ''' 【編集モード】作業取消時：CI共通情報（保存用）更新用SQLの作成・設定
    ' ''' </summary>
    ' ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ' ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ' ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ' ''' <remarks>作業取消時のCI共通情報（保存用）更新用のSQLを作成し、アダプタにセットする
    ' ''' <para>作成情報：2012/08/07 t.fukuo
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Public Function SetUpdateTmpCIInfoCancelSql(ByRef Cmd As NpgsqlCommand, _
    '                                            ByVal Cn As NpgsqlConnection, _
    '                                            ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数の宣言
    '    Dim strSQL As String = ""

    '    Try

    '        'SQL文(UPDATE)
    '        strSQL = strUpdateTmpCIInfoSql_Cancel

    '        'データアダプタに、SQL文を設定
    '        Cmd = New NpgsqlCommand(strSQL, Cn)


    '        'バインド変数に型をセット
    '        With Cmd.Parameters
    '            .Add(New NpgsqlParameter("CIStatusCD", NpgsqlTypes.NpgsqlDbType.Varchar))               'CIステータスCD
    '            .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
    '            .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
    '            .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
    '            .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
    '            .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'インシデント番号
    '            .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))                  '作業番号
    '        End With

    '        'バインド変数に値をセット
    '        With Cmd
    '            .Parameters("CIStatusCD").Value = dataHBKC0201.PropStrUpdCIStatusCD                     'CIステータスCD
    '            .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                             '最終更新日時
    '            .Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
    '            .Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
    '            .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号
    '            .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                                'インシデント番号
    '            .Parameters("WorkNmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("WorkNmb"))   '作業番号
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
    ''' 【編集モード】作業取消時：CI共通情報更新用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>作業取消時のCI共通情報更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateCIInfoCancelSql(ByRef Cmd As NpgsqlCommand, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateCIInfoSql_Cancel

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CIStatusCD", NpgsqlTypes.NpgsqlDbType.Varchar))               'CIステータスCD
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("CIStatusCD").Value = dataHBKC0201.PropStrUpdCIStatusCD                     'CIステータスCD
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                             '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号
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
    ''' 【編集モード】セット作成時：CI共通情報更新用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>セット作成時のCI共通情報更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateCIInfoForSetPairSql(ByRef Cmd As NpgsqlCommand, _
                                                 ByVal Cn As NpgsqlConnection, _
                                                 ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateCIInfoSql_SetPair

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("SetKikiID", NpgsqlTypes.NpgsqlDbType.Integer))                'セットID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                If dataHBKC0201.PropRowReg.Item("SetKikiID").ToString <> "" Then
                    .Parameters("SetKikiID").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("SetKikiID"))   'セットID
                Else
                    .Parameters("SetKikiID").Value = DBNull.Value
                End If
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                                     '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                                  '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                                      '最終更新者ID
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))               'CI番号
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
    ' ''' 【編集モード】保存用テーブルからのCI共通情報更新用SQLの作成・設定
    ' ''' </summary>
    ' ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ' ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ' ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ' ''' <remarks>保存用テーブルからのCI共通情報更新用のSQLを作成し、アダプタにセットする
    ' ''' <para>作成情報：2012/07/30 t.fukuo
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Public Function SetUpdateCIInfoFromTmpSql(ByRef Cmd As NpgsqlCommand, _
    '                                          ByVal Cn As NpgsqlConnection, _
    '                                          ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数の宣言
    '    Dim strSQL As String = ""

    '    Try

    '        'SQL文(UPDATE)
    '        strSQL = strUpdateCIInfoFromTmpSql

    '        'データアダプタに、SQL文を設定
    '        Cmd = New NpgsqlCommand(strSQL, Cn)


    '        'バインド変数に型をセット
    '        With Cmd.Parameters
    '            .Add(New NpgsqlParameter("CIStatusCD", NpgsqlTypes.NpgsqlDbType.Varchar))               'CIステータスコード
    '            .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
    '            .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
    '            .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
    '            .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
    '            .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'インシデント番号
    '            .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))                  '作業番号
    '        End With

    '        'バインド変数に値をセット
    '        With Cmd
    '            .Parameters("CIStatusCD").Value = dataHBKC0201.PropStrUpdCIStatusCD                     'CIステータスコード
    '            .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                             '最終更新日時
    '            .Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
    '            .Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
    '            .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号
    '            .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                                'インシデント番号
    '            .Parameters("WorkNmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("WorkNmb"))   '作業番号
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
    ' ''' 【編集モード】本テーブルからのCI共通情報（保存用）登録用SQLの作成・設定
    ' ''' </summary>
    ' ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ' ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ' ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ' ''' <remarks>本テーブルからのCI共通情報（保存用）登録用のSQLを作成し、アダプタにセットする
    ' ''' <para>作成情報：2012/08/01 t.fukuo
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Public Function SetInsertTmpCIInfoFromOrgSql(ByRef Cmd As NpgsqlCommand, _
    '                                             ByVal Cn As NpgsqlConnection, _
    '                                             ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数の宣言
    '    Dim strSQL As String = ""

    '    Try

    '        'SQL文(UPDATE)
    '        strSQL = strInsertTmpCIInfoFromOrg

    '        'データアダプタに、SQL文を設定
    '        Cmd = New NpgsqlCommand(strSQL, Cn)


    '        'バインド変数に型をセット
    '        With Cmd.Parameters
    '            .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))               'インシデント番号
    '            .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))              '作業番号
    '            .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))              '登録日時
    '            .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者グループCD
    '            .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                '登録者ID
    '            .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
    '            .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
    '            .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
    '            .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
    '        End With

    '        'バインド変数に値をセット
    '        With Cmd
    '            .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                            'インシデント番号
    '            .Parameters("WorkNmb").Value = dataHBKC0201.PropVwSapMainte.Sheets(0).RowCount + 1  '作業番号
    '            .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                            '登録日時
    '            .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
    '            .Parameters("RegID").Value = PropUserId                                             '登録者ID
    '            .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                         '更新日時
    '            .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
    '            .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
    '            .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))   'CI番号
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
    ' ''' 【編集モード】本テーブルからのCIサポセン機器（保存用）登録用SQLの作成・設定
    ' ''' </summary>
    ' ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ' ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ' ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ' ''' <remarks>本テーブルからのCIサポセン機器（保存用）登録用のSQLを作成し、アダプタにセットする
    ' ''' <para>作成情報：2012/08/01 t.fukuo
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Public Function SetInsertTmpCISapFromOrgSql(ByRef Cmd As NpgsqlCommand, _
    '                                            ByVal Cn As NpgsqlConnection, _
    '                                            ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数の宣言
    '    Dim strSQL As String = ""

    '    Try

    '        'SQL文(UPDATE)
    '        strSQL = strInsertTmpCISapFromOrgSql

    '        'データアダプタに、SQL文を設定
    '        Cmd = New NpgsqlCommand(strSQL, Cn)


    '        'バインド変数に型をセット
    '        With Cmd.Parameters
    '            .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))               'インシデント番号
    '            .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))              '作業番号
    '            .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))              '登録日時
    '            .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者グループCD
    '            .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                '登録者ID
    '            .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
    '            .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
    '            .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
    '            .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
    '        End With

    '        'バインド変数に値をセット
    '        With Cmd
    '            .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                            'インシデント番号
    '            .Parameters("WorkNmb").Value = dataHBKC0201.PropVwSapMainte.Sheets(0).RowCount + 1  '作業番号
    '            .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                            '登録日時
    '            .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
    '            .Parameters("RegID").Value = PropUserId                                             '登録者ID
    '            .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                         '更新日時
    '            .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
    '            .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
    '            .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))   'CI番号
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
    ' ''' 【編集モード】本テーブルからのオプションソフト（保存用）登録用SQLの作成・設定
    ' ''' </summary>
    ' ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ' ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ' ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ' ''' <remarks>本テーブルからのオプションソフト（保存用）登録用のSQLを作成し、アダプタにセットする
    ' ''' <para>作成情報：2012/08/01 t.fukuo
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Public Function SetInsertTmpOptSoftFromOrgSql(ByRef Cmd As NpgsqlCommand, _
    '                                              ByVal Cn As NpgsqlConnection, _
    '                                              ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数の宣言
    '    Dim strSQL As String = ""

    '    Try

    '        'SQL文(UPDATE)
    '        strSQL = strInsertTmpOptSoftFromOrgSql

    '        'データアダプタに、SQL文を設定
    '        Cmd = New NpgsqlCommand(strSQL, Cn)


    '        'バインド変数に型をセット
    '        With Cmd.Parameters
    '            .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))               'インシデント番号
    '            .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))              '作業番号
    '            .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))              '登録日時
    '            .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者グループCD
    '            .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                '登録者ID
    '            .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
    '            .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
    '            .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
    '            .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
    '        End With

    '        'バインド変数に値をセット
    '        With Cmd
    '            .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                            'インシデント番号
    '            .Parameters("WorkNmb").Value = dataHBKC0201.PropVwSapMainte.Sheets(0).RowCount + 1  '作業番号
    '            .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                            '登録日時
    '            .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
    '            .Parameters("RegID").Value = PropUserId                                             '登録者ID
    '            .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                         '更新日時
    '            .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
    '            .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
    '            .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))   'CI番号
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
    ' ''' 【編集モード】本テーブルからの複数人利用（保存用）登録用SQLの作成・設定
    ' ''' </summary>
    ' ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ' ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ' ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ' ''' <remarks>本テーブルからの複数人利用（保存用）登録用のSQLを作成し、アダプタにセットする
    ' ''' <para>作成情報：2012/08/01 t.fukuo
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Public Function SetInsertTmpShareFromOrgSql(ByRef Cmd As NpgsqlCommand, _
    '                                            ByVal Cn As NpgsqlConnection, _
    '                                            ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数の宣言
    '    Dim strSQL As String = ""

    '    Try

    '        'SQL文(UPDATE)
    '        strSQL = strInsertTmpShareFromOrgSql

    '        'データアダプタに、SQL文を設定
    '        Cmd = New NpgsqlCommand(strSQL, Cn)


    '        'バインド変数に型をセット
    '        With Cmd.Parameters
    '            .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))               'インシデント番号
    '            .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))              '作業番号
    '            .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))              '登録日時
    '            .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者グループCD
    '            .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                '登録者ID
    '            .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
    '            .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
    '            .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
    '            .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
    '        End With

    '        'バインド変数に値をセット
    '        With Cmd
    '            .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                            'インシデント番号
    '            .Parameters("WorkNmb").Value = dataHBKC0201.PropVwSapMainte.Sheets(0).RowCount + 1  '作業番号
    '            .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                            '登録日時
    '            .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
    '            .Parameters("RegID").Value = PropUserId                                             '登録者ID
    '            .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                         '更新日時
    '            .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
    '            .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
    '            .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))   'CI番号
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
    ' ''' 【編集モード】本テーブルからのセット機器管理（保存用）登録用SQLの作成・設定
    ' ''' </summary>
    ' ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ' ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ' ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ' ''' <remarks>本テーブルからのセット機器管理（保存用）登録用のSQLを作成し、アダプタにセットする
    ' ''' <para>作成情報：2012/08/01 t.fukuo
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Public Function SetInsertTmpSetKikiFromOrgSql(ByRef Cmd As NpgsqlCommand, _
    '                                              ByVal Cn As NpgsqlConnection, _
    '                                              ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数の宣言
    '    Dim strSQL As String = ""

    '    Try

    '        'SQL文(UPDATE)
    '        strSQL = strInsertTmpSetKikiFromOrgSql

    '        'データアダプタに、SQL文を設定
    '        Cmd = New NpgsqlCommand(strSQL, Cn)


    '        'バインド変数に型をセット
    '        With Cmd.Parameters
    '            .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))               'インシデント番号
    '            .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))              '作業番号
    '            .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
    '            .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))              '登録日時
    '            .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者グループCD
    '            .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                '登録者ID
    '            .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
    '            .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
    '            .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
    '            .Add(New NpgsqlParameter("KindCD", NpgsqlTypes.NpgsqlDbType.Varchar))               '種別コード
    '            .Add(New NpgsqlParameter("Num", NpgsqlTypes.NpgsqlDbType.Varchar))                  '番号
    '        End With

    '        'バインド変数に値をセット
    '        With Cmd
    '            .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                            'インシデント番号
    '            .Parameters("WorkNmb").Value = dataHBKC0201.PropVwSapMainte.Sheets(0).RowCount + 1  '作業番号
    '            .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))   'CI番号
    '            .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                            '登録日時
    '            .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
    '            .Parameters("RegID").Value = PropUserId                                             '登録者ID
    '            .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                         '更新日時
    '            .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
    '            .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
    '            .Parameters("KindCD").Value = dataHBKC0201.PropRowReg.Item("KindCD")                '種別コード
    '            .Parameters("Num").Value = dataHBKC0201.PropRowReg.Item("Num")                      '番号
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
    ''' 【編集モード】CIサポセン機器.作業の元更新用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIサポセン機器.作業の元更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateCISap_WorkFromNmbSql(ByRef Cmd As NpgsqlCommand, _
                                                  ByVal Cn As NpgsqlConnection, _
                                                  ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateWorkFromNmbSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("WorkFromNmb", NpgsqlTypes.NpgsqlDbType.Varchar))          '作業の元
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("WorkFromNmb").Value = dataHBKC0201.PropIntINCNmb.ToString()            '作業の元：インシデント管理番号
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                         '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))   'CI番号
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

    ' '''<summary>
    ' ''' 【編集モード】クリア時：CIサポセン機器（保存用）更新用SQLの作成・設定
    ' ''' </summary>
    ' ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ' ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ' ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ' ''' <remarks>クリア時：CIサポセン機器（保存用）更新用のSQLを作成し、アダプタにセットする
    ' ''' <para>作成情報：2012/08/07 t.fukuo
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Public Function SetUpdateTmpCISapClearSql(ByRef Cmd As NpgsqlCommand, _
    '                                          ByVal Cn As NpgsqlConnection, _
    '                                          ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数の宣言
    '    Dim strSQL As String = ""

    '    Try
    '        With dataHBKC0201

    '            'SQL文(UPDATE)
    '            strSQL = strUpdateTmpCISapSql_Clear_BaseSet

    '            'イメージ番号クリアフラグに応じてイメージ番号のSET句を設定
    '            If .PropBlnClearSapData = True Or .PropBlnClearImageNmb = True Then
    '                strSQL &= strUpdateTmpCISapSql_ClearImageNmb
    '            End If

    '            'サポセンデータクリアフラグに応じてその他項目のSET句を設定
    '            If .PropBlnClearSapData = True Then
    '                strSQL &= strUpdateTmpCISapSql_ClearOther
    '            End If

    '            'WHERE句を追加
    '            strSQL &= strUpdateTmpCISapSql_Clear_BaseWhere

    '        End With


    '        'データアダプタに、SQL文を設定
    '        Cmd = New NpgsqlCommand(strSQL, Cn)


    '        'バインド変数に型をセット
    '        With Cmd.Parameters
    '            .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
    '            .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
    '            .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
    '            .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'インシデント番号
    '            .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))                  '作業番号
    '            .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
    '        End With

    '        'バインド変数に値をセット
    '        With Cmd
    '            .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                             '最終更新日時
    '            .Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
    '            .Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
    '            .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                                'インシデント番号
    '            .Parameters("WorkNmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("WorkNmb"))   '作業番号
    '            .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号
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

    '''<summary>
    ''' 【編集モード】クリア時：CIサポセン機器更新用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>クリア時：CIサポセン機器更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateCISapClearSql(ByRef Cmd As NpgsqlCommand, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            With dataHBKC0201

                'SQL文(UPDATE)
                strSQL = strUpdateCISapSql_Clear_BaseSet

                'イメージ番号クリアフラグに応じてイメージ番号のSET句を設定
                If .PropBlnClearSapData = True Or .PropBlnClearImageNmb = True Then
                    strSQL &= strUpdateCISapSql_ClearImageNmb
                End If

                'サポセンデータクリアフラグに応じてその他項目のSET句を設定
                If .PropBlnClearSapData = True Then
                    strSQL &= strUpdateCISapSql_ClearOther
                End If

                'WHERE句を追加
                strSQL &= strUpdateCISapSql_Clear_BaseWhere

            End With


            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                             '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号
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
    ''' 【編集モード】CIサポセン機器更新用SQLの作成・設定：交換設置
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>交換設置された場合のCIサポセン機器更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateCISapSql_DoExchange(ByRef Cmd As NpgsqlCommand, _
                                                 ByVal Cn As NpgsqlConnection, _
                                                 ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateCISapSql_DoExchange

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                   '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                      '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                     '最終更新者ID
                .Add(New NpgsqlParameter("CINmb_Set", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号（交換設置）
                .Add(New NpgsqlParameter("CINmb_Remove", NpgsqlTypes.NpgsqlDbType.Integer))                 'CI番号（交換撤去）
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                                 '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                              '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                                  '最終更新者ID
                .Parameters("CINmb_Set").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号（交換設置）
                .Parameters("CINmb_Remove").Value = dataHBKC0201.PropIntExchangeCINmb                       'CI番号（交換撤去）
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
    ''' 【編集モード】CIサポセン機器更新用SQLの作成・設定：種別＝「継続利用」
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>種別が「継続利用」のCIサポセン機器更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateCISapSql_Continue(ByRef Cmd As NpgsqlCommand, _
                                               ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateCISapSql_Continue

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("WorkFromNmb", NpgsqlTypes.NpgsqlDbType.Varchar))              '作業の元
                .Add(New NpgsqlParameter("KikiUseCD", NpgsqlTypes.NpgsqlDbType.Varchar))                '機器利用形態コード
                .Add(New NpgsqlParameter("ManageKyokuNM", NpgsqlTypes.NpgsqlDbType.Varchar))            '管理局
                .Add(New NpgsqlParameter("ManageBusyoNM", NpgsqlTypes.NpgsqlDbType.Varchar))            '管理部署
                .Add(New NpgsqlParameter("SetKyokuNM", NpgsqlTypes.NpgsqlDbType.Varchar))               '設置局
                .Add(New NpgsqlParameter("SetBusyoNM", NpgsqlTypes.NpgsqlDbType.Varchar))               '設置部署
                .Add(New NpgsqlParameter("SetRoom", NpgsqlTypes.NpgsqlDbType.Varchar))                  '設置番組／部屋
                .Add(New NpgsqlParameter("ManageBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))       '管理部署（あいまい）
                .Add(New NpgsqlParameter("SetBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))          '設置部署（あいまい）
                .Add(New NpgsqlParameter("SetRoomAimai", NpgsqlTypes.NpgsqlDbType.Varchar))             '設置番組／部屋（あいまい）
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("WorkFromNmb").Value = dataHBKC0201.PropIntINCNmb.ToString()                '作業の元：インシデント管理番号
                .Parameters("KikiUseCD").Value = KIKI_RIYOKEITAI_KEIZOKU                                '機器利用形態コード：継続利用
                .Parameters("ManageKyokuNM").Value = dataHBKC0201.PropTxtPartnerKyokuNM.Text            '管理局：相手局
                .Parameters("ManageBusyoNM").Value = dataHBKC0201.PropTxtPartnerBusyoNM.Text            '管理部署：相手部署
                .Parameters("SetKyokuNM").Value = dataHBKC0201.PropTxtPartnerKyokuNM.Text               '設置局：相手局
                .Parameters("SetBusyoNM").Value = dataHBKC0201.PropTxtPartnerBusyoNM.Text               '設置部署：相手部署
                .Parameters("SetRoom").Value = dataHBKC0201.PropTxtPartnerRoom.Text                     '設置番組／部屋：相手番組／部屋
                .Parameters("ManageBusyoNMAimai").Value = _
                    commonLogicHBK.ChangeStringForSearch(.Parameters("ManageBusyoNM").Value)            '管理部署（あいまい）：相手部署
                .Parameters("SetBusyoNMAimai").Value = _
                    commonLogicHBK.ChangeStringForSearch(.Parameters("SetBusyoNM").Value)               '設置部署（あいまい）：相手部署
                .Parameters("SetRoomAimai").Value = _
                    commonLogicHBK.ChangeStringForSearch(.Parameters("SetRoom").Value)                  '設置番組／部屋（あいまい）：相手番組／部屋
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                             '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号
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
    ''' 【編集モード】CIサポセン機器更新用SQLの作成・設定：種別＝「一時利用（貸出）」
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>種別が「一時利用（貸出）」のCIサポセン機器更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateCISapSql_Rental(ByRef Cmd As NpgsqlCommand, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateCISapSql_Rental

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("WorkFromNmb", NpgsqlTypes.NpgsqlDbType.Varchar))              '作業の元
                .Add(New NpgsqlParameter("KikiUseCD", NpgsqlTypes.NpgsqlDbType.Varchar))                '機器利用形態コード
                .Add(New NpgsqlParameter("IPUseCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  'IP割当種類コード
                .Add(New NpgsqlParameter("UsrID", NpgsqlTypes.NpgsqlDbType.Varchar))                    'ユーザーID
                .Add(New NpgsqlParameter("UsrNM", NpgsqlTypes.NpgsqlDbType.Varchar))                    'ユーザー氏名
                .Add(New NpgsqlParameter("UsrCompany", NpgsqlTypes.NpgsqlDbType.Varchar))               'ユーザー所属会社
                .Add(New NpgsqlParameter("UsrKyokuNM", NpgsqlTypes.NpgsqlDbType.Varchar))               'ユーザー所属局
                .Add(New NpgsqlParameter("UsrBusyoNM", NpgsqlTypes.NpgsqlDbType.Varchar))               'ユーザー所属部署
                .Add(New NpgsqlParameter("UsrTel", NpgsqlTypes.NpgsqlDbType.Varchar))                   'ユーザー電話番号
                .Add(New NpgsqlParameter("UsrMailAdd", NpgsqlTypes.NpgsqlDbType.Varchar))               'ユーザーメールアドレス
                .Add(New NpgsqlParameter("UsrContact", NpgsqlTypes.NpgsqlDbType.Varchar))               'ユーザー連絡先
                .Add(New NpgsqlParameter("UsrRoom", NpgsqlTypes.NpgsqlDbType.Varchar))                  'ユーザー番組／部屋
                .Add(New NpgsqlParameter("ManageKyokuNM", NpgsqlTypes.NpgsqlDbType.Varchar))            '管理局
                .Add(New NpgsqlParameter("ManageBusyoNM", NpgsqlTypes.NpgsqlDbType.Varchar))            '管理部署
                .Add(New NpgsqlParameter("SetKyokuNM", NpgsqlTypes.NpgsqlDbType.Varchar))               '設置局
                .Add(New NpgsqlParameter("SetBusyoNM", NpgsqlTypes.NpgsqlDbType.Varchar))               '設置部署
                .Add(New NpgsqlParameter("SetRoom", NpgsqlTypes.NpgsqlDbType.Varchar))                  '設置番組／部屋
                .Add(New NpgsqlParameter("ManageBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))       '管理部署（あいまい）
                .Add(New NpgsqlParameter("UsrIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))               '管理部署（あいまい）
                .Add(New NpgsqlParameter("SetBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))          '設置部署（あいまい）
                .Add(New NpgsqlParameter("SetRoomAimai", NpgsqlTypes.NpgsqlDbType.Varchar))             '設置番組／部屋（あいまい）
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("WorkFromNmb").Value = dataHBKC0201.PropIntINCNmb.ToString()                '作業の元：インシデント管理番号
                .Parameters("KikiUseCD").Value = KIKI_RIYOKEITAI_ICHIJI_RIYO                            '機器利用形態コード：一時利用（貸出）
                .Parameters("IPUseCD").Value = IP_DYNAMIC_DHCP                                          'IP割当種類コード：変動（DHCP自動取得）
                .Parameters("UsrID").Value = dataHBKC0201.PropTxtPartnerID.Text                         'ユーザーID：相手ID
                .Parameters("UsrNM").Value = dataHBKC0201.PropTxtPartnerNM.Text                         'ユーザー氏名：相手氏名
                .Parameters("UsrCompany").Value = dataHBKC0201.PropTxtPartnerCompany.Text               'ユーザー所属会社：相手会社
                .Parameters("UsrKyokuNM").Value = dataHBKC0201.PropTxtPartnerKyokuNM.Text               'ユーザー所属局：相手局
                .Parameters("UsrBusyoNM").Value = dataHBKC0201.PropTxtPartnerBusyoNM.Text               'ユーザー所属部署：相手部署
                .Parameters("UsrTel").Value = dataHBKC0201.PropTxtPartnerTel.Text                       'ユーザー電話番号：相手電話番号
                .Parameters("UsrMailAdd").Value = dataHBKC0201.PropTxtPartnerMailAdd.Text               'ユーザーメールアドレス：相手メールアドレス
                .Parameters("UsrContact").Value = dataHBKC0201.PropTxtPartnerContact.Text               'ユーザー連絡先：相手連絡先
                .Parameters("UsrRoom").Value = dataHBKC0201.PropTxtPartnerRoom.Text                     'ユーザー番組／部屋：相手番組／部屋
                .Parameters("ManageKyokuNM").Value = dataHBKC0201.PropTxtPartnerKyokuNM.Text            '管理局：相手局
                .Parameters("ManageBusyoNM").Value = dataHBKC0201.PropTxtPartnerBusyoNM.Text            '管理部署：相手部署
                .Parameters("SetKyokuNM").Value = dataHBKC0201.PropTxtPartnerKyokuNM.Text               '設置局：相手局
                .Parameters("SetBusyoNM").Value = dataHBKC0201.PropTxtPartnerBusyoNM.Text               '設置部署：相手部署
                .Parameters("SetRoom").Value = dataHBKC0201.PropTxtPartnerRoom.Text                     '設置番組／部屋：相手番組／部屋
                .Parameters("ManageBusyoNMAimai").Value = _
                    commonLogicHBK.ChangeStringForSearch(.Parameters("ManageBusyoNM").Value)            '管理部署（あいまい）：相手部署
                .Parameters("UsrIDAimai").Value = _
                    commonLogicHBK.ChangeStringForSearch(.Parameters("UsrID").Value)                    'ユーザーID（あいまい）：相手ID
                .Parameters("SetBusyoNMAimai").Value = _
                    commonLogicHBK.ChangeStringForSearch(.Parameters("SetBusyoNM").Value)               '設置部署（あいまい）：相手部署
                .Parameters("SetRoomAimai").Value = _
                    commonLogicHBK.ChangeStringForSearch(.Parameters("SetRoom").Value)                  '設置番組／部屋（あいまい）：相手番組／部屋
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                             '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号
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
    ''' 【編集モード】作業取消時：CIサポセン機器更新用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>作業取消時のCIサポセン機器更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateCISapCancelSql(ByRef Cmd As NpgsqlCommand, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateCISapSql_Cancel

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'インシデント番号
                .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))                  '作業番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                             '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                                'インシデント番号
                .Parameters("WorkNmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("WorkNmb"))   '作業番号
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号
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
    ''' 【編集モード】CIサポセン機器メンテナンス作業新規登録用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIサポセン機器メンテナンス作業新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertSapMainteWorkSql(ByRef Cmd As NpgsqlCommand, _
                                              ByVal Cn As NpgsqlConnection, _
                                              ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(INSERT)
            strSQL = strInsertSapMainteWorkSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'インシデント番号
                .Add(New NpgsqlParameter("WorkCD", NpgsqlTypes.NpgsqlDbType.Varchar))                   '作業コード
                .Add(New NpgsqlParameter("WorkBiko", NpgsqlTypes.NpgsqlDbType.Varchar))                 '作業備考
                .Add(New NpgsqlParameter("WorkSceDT", NpgsqlTypes.NpgsqlDbType.Varchar))                '作業予定日
                .Add(New NpgsqlParameter("WorkCompDT", NpgsqlTypes.NpgsqlDbType.Varchar))               '作業開始日
                .Add(New NpgsqlParameter("CompFlg", NpgsqlTypes.NpgsqlDbType.Varchar))                  '完了フラグ
                .Add(New NpgsqlParameter("CancelFLg", NpgsqlTypes.NpgsqlDbType.Varchar))                '取消フラグ
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                  '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                 '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                    '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                                'インシデント番号
                .Parameters("WorkCD").Value = dataHBKC0201.PropCmbWork.SelectedValue                    '作業コード
                .Parameters("WorkBiko").Value = ""                                                      '作業備考：空白
                .Parameters("WorkSceDT").Value = ""                                                     '作業予定日：空白
                .Parameters("WorkCompDT").Value = ""                                                    '作業完了日：空白
                .Parameters("CompFlg").Value = COMP_FLG_OFF                                             '完了フラグ：OFF
                .Parameters("CancelFLg").Value = CANCEL_FLG_OFF                                         '取消フラグ：OFF
                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                                '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                         '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                 '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                             '最終更新日時
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
    ''' 【編集モード】CIサポセン機器メンテナンス機器新規登録用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIサポセン機器メンテナンス機器新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertSapMainteKikiSql(ByRef Cmd As NpgsqlCommand, _
                                              ByVal Cn As NpgsqlConnection, _
                                              ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(INSERT)
            strSQL = strInsertSapMainteKikiSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'インシデント番号
                .Add(New NpgsqlParameter("WorkCD", NpgsqlTypes.NpgsqlDbType.Varchar))                   '作業コード
                .Add(New NpgsqlParameter("RowNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   '行番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
                .Add(New NpgsqlParameter("KindCD", NpgsqlTypes.NpgsqlDbType.Varchar))                   '種別コード
                .Add(New NpgsqlParameter("Num", NpgsqlTypes.NpgsqlDbType.Varchar))                      '番号
                .Add(New NpgsqlParameter("ChgFlg", NpgsqlTypes.NpgsqlDbType.Varchar))                   '交換フラグ
                .Add(New NpgsqlParameter("RegRirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))              '登録前履歴No
                .Add(New NpgsqlParameter("LastUpRirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))           '最終更新時履歴No
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                  '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                 '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                    '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                                'インシデント番号
                .Parameters("WorkCD").Value = dataHBKC0201.PropCmbWork.SelectedValue                    '作業コード
                .Parameters("RowNmb").Value = dataHBKC0201.PropRowReg.Item("RowNmb")                    '行番号
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号
                .Parameters("KindCD").Value = dataHBKC0201.PropRowReg.Item("KindCD")                    '種別コード
                .Parameters("Num").Value = dataHBKC0201.PropRowReg.Item("Num")                          '番号
                .Parameters("ChgFlg").Value = CHANGE_FLG_OFF                                            '交換フラグ：OFF
                .Parameters("RegRirekiNo").Value = dataHBKC0201.PropIntCIRirekiNo - 1                   '登録前履歴No：履歴No－１
                .Parameters("LastUpRirekiNo").Value = dataHBKC0201.PropIntCIRirekiNo                    '最終更新時履歴No
                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                                '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                         '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                 '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                             '最終更新日時
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
    ''' 【編集モード】CIサポセン機器メンテナンス作業更新用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIサポセン機器メンテナンス作業更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateSapMainteWorkSql(ByRef Cmd As NpgsqlCommand, _
                                              ByVal Cn As NpgsqlConnection, _
                                              ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateSapMainteWorkSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("WorkBiko", NpgsqlTypes.NpgsqlDbType.Varchar))                 '作業備考
                .Add(New NpgsqlParameter("WorkSceDT", NpgsqlTypes.NpgsqlDbType.Date))                   '作業予定日
                .Add(New NpgsqlParameter("WorkCompDT", NpgsqlTypes.NpgsqlDbType.Date))                  '作業開始日
                .Add(New NpgsqlParameter("CompFlg", NpgsqlTypes.NpgsqlDbType.Varchar))                  '完了フラグ
                .Add(New NpgsqlParameter("CancelFLg", NpgsqlTypes.NpgsqlDbType.Varchar))                '取消フラグ
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'インシデント番号
                .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))                  '作業番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("WorkBiko").Value = dataHBKC0201.PropRowReg.Item("WorkBiko")                '作業備考
                .Parameters("WorkSceDT").Value = dataHBKC0201.PropRowReg.Item("WorkSceDT")              '作業予定日
                .Parameters("WorkCompDT").Value = dataHBKC0201.PropRowReg.Item("WorkCompDT")            '作業完了日
                If dataHBKC0201.PropRowReg.Item("CompFlg") = True Then                                  '完了フラグ
                    .Parameters("CompFlg").Value = COMP_FLG_ON
                Else
                    .Parameters("CompFlg").Value = COMP_FLG_OFF
                End If
                If dataHBKC0201.PropRowReg.Item("CancelFLg") = True Then                                '取消フラグ
                    .Parameters("CancelFLg").Value = CANCEL_FLG_ON
                Else
                    .Parameters("CancelFLg").Value = CANCEL_FLG_OFF
                End If
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                             '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                                'インシデント番号
                .Parameters("WorkNmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("WorkNmb"))   '作業番号
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
    ''' 【編集モード】CIサポセン機器メンテナンス機器更新用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIサポセン機器メンテナンス機器更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateSapMainteKikiSql(ByRef Cmd As NpgsqlCommand, _
                                              ByVal Cn As NpgsqlConnection, _
                                              ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Add(New NpgsqlParameter("ChgFlg", NpgsqlTypes.NpgsqlDbType.Varchar))                   '交換フラグ
                .Add(New NpgsqlParameter("ChgNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   '交換番号
                .Add(New NpgsqlParameter("LastUpRirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))           '最終更新履歴No
                .Add(New NpgsqlParameter("CepalateFlg", NpgsqlTypes.NpgsqlDbType.Varchar))              'バラすフラグ
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'インシデント番号
                .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))                  '作業番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    '行番号
            End With

            'バインド変数に値をセット
            With Cmd
                If dataHBKC0201.PropRowReg.Item("ChgNmb").ToString() <> "" Then                         '交換フラグ
                    .Parameters("ChgFlg").Value = CHANGE_FLG_ON
                Else
                    .Parameters("ChgFlg").Value = CHANGE_FLG_OFF
                End If
                If dataHBKC0201.PropRowReg.Item("ChgNmb").ToString() <> "" Then                         '交換番号
                    .Parameters("ChgNmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("ChgNmb"))
                Else
                    .Parameters("ChgNmb").Value = DBNull.Value
                End If
                .Parameters("LastUpRirekiNo").Value = dataHBKC0201.PropIntCIRirekiNo                    '最終更新履歴No
                If dataHBKC0201.PropRowReg.Item("CepalateFlg").ToString() = CEPALATEFLG_ON_VW Then      'バラすフラグ
                    .Parameters("CepalateFlg").Value = CEPALATEFLG_ON
                Else
                    .Parameters("CepalateFlg").Value = CEPALATEFLG_OFF
                End If
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                             '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                                'インシデント番号
                .Parameters("WorkNmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("WorkNmb"))   '作業番号
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号
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
    ''' 【編集モード】CIサポセン機器メンテナンス機器最終更新時履歴No更新用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIサポセン機器メンテナンス機器最終更新時履歴No更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateSapMainteKikiLastUpRirekiNoSql(ByRef Cmd As NpgsqlCommand, _
                                                            ByVal Cn As NpgsqlConnection, _
                                                            ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateSapMainteKikiLastUpRirekiNoSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LastUpRirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))           '最終更新履歴No
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'インシデント番号
                .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))                  '作業番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    '行番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LastUpRirekiNo").Value = dataHBKC0201.PropIntCIRirekiNo                    '最終更新履歴No
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                                'インシデント番号
                .Parameters("WorkNmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("WorkNmb"))   '作業番号
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号
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
    ''' 【編集モード】CI共通情報履歴新規登録用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCIInfoRirekiSql(ByRef Cmd As NpgsqlCommand, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Parameters("RirekiNo").Value = dataHBKC0201.PropIntCIRirekiNo                      '履歴番号：CI履歴番号
                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                            '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                             '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                         '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))   'CI番号
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
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIサポセン機器履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCISapRirekiSql(ByRef Cmd As NpgsqlCommand, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Parameters("RirekiNo").Value = dataHBKC0201.PropIntCIRirekiNo                      '履歴番号：CI履歴番号
                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                            '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                             '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                         '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))   'CI番号
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
    ''' 【編集モード】オプションソフト（保存用）物理削除用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>オプションソフト（保存用）物理削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/07 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteTmpOptSoftSql(ByRef Cmd As NpgsqlCommand, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(DELETE)
            strSQL = strDeleteOptSoftSql

            'データアダプタに、SQLを設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号
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
    ''' 【編集モード】オプションソフト物理削除用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>オプションソフト物理削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/01 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteOptSoftSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(DELETE)
            strSQL = strDeleteOptSoftSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))   'CI番号
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
    ''' 【編集モード】交換撤去のオプションソフト登録用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>交換撤去のオプションソフトデータ登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertOptSoftWhenExchangeSql(ByRef Cmd As NpgsqlCommand, _
                                                    ByVal Cn As NpgsqlConnection, _
                                                    ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(INSERT)
            strSQL = strInsertOptSoftWhenExchangeSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb_Set", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号（交換設置）
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                      '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                     '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                        '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                   '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                      '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                     '最終更新者ID
                .Add(New NpgsqlParameter("CINmb_Remove", NpgsqlTypes.NpgsqlDbType.Integer))                 'CI番号（交換撤去）
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb_Set").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号（交換設置）
                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                                    '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                             '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                     '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                                 '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                              '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                                  '最終更新者ID
                .Parameters("CINmb_Remove").Value = dataHBKC0201.PropIntExchangeCINmb                       'CI番号（交換撤去）
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
    ' ''' 【編集モード】保存用テーブルからのオプションソフト（保存用）登録用SQLの作成・設定
    ' ''' </summary>
    ' ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ' ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ' ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ' ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ' ''' <remarks>保存用テーブルからのオプションソフト（保存用）登録用のSQLを作成し、アダプタにセットする
    ' ''' <para>作成情報：2012/08/12 t.fukuo
    ' ''' <p>改訂情報：</p>
    ' ''' </para></remarks>
    'Public Function SetInsertTmpOptSoftFromTmpSql(ByRef Cmd As NpgsqlCommand, _
    '                                              ByVal Cn As NpgsqlConnection, _
    '                                              ByVal dataHBKC0201 As DataHBKC0201) As Boolean

    '    '開始ログ出力
    '    CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数の宣言
    '    Dim strSQL As String = ""

    '    Try

    '        'SQL文(INSERT)
    '        strSQL = strInsertTmpOptSoftFromTmpSql

    '        'データアダプタに、SQL文を設定
    '        Cmd = New NpgsqlCommand(strSQL, Cn)


    '        'バインド変数に型をセット
    '        With Cmd.Parameters
    '            .Add(New NpgsqlParameter("IncNmb_Set", NpgsqlTypes.NpgsqlDbType.Integer))               'インシデント番号（交換設置）
    '            .Add(New NpgsqlParameter("WorkNmb_Set", NpgsqlTypes.NpgsqlDbType.Integer))              '作業番号（交換設置）
    '            .Add(New NpgsqlParameter("CINmb_Set", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号（交換設置）
    '            .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                  '登録日時
    '            .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                 '登録者グループCD
    '            .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                    '登録者ID
    '            .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
    '            .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
    '            .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
    '            .Add(New NpgsqlParameter("IncNmb_Remove", NpgsqlTypes.NpgsqlDbType.Integer))            'インシデント番号（交換撤去）
    '            .Add(New NpgsqlParameter("WorkNmb_Remove", NpgsqlTypes.NpgsqlDbType.Integer))           '作業番号（交換撤去）
    '            .Add(New NpgsqlParameter("CINmb_Remove", NpgsqlTypes.NpgsqlDbType.Integer))             'CI番号（交換撤去）
    '        End With

    '        'バインド変数に値をセット
    '        With Cmd
    '            .Parameters("IncNmb_Set").Value = dataHBKC0201.PropIntINCNmb                                'インシデント番号（交換設置）
    '            .Parameters("WorkNmb_Set").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("WorkNmb"))   '作業番号（交換設置）
    '            .Parameters("CINmb_Set").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号（交換設置）
    '            .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                                    '登録日時
    '            .Parameters("RegGrpCD").Value = PropWorkGroupCD                                             '登録者グループCD
    '            .Parameters("RegID").Value = PropUserId                                                     '登録者ID
    '            .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                                 '最終更新日時
    '            .Parameters("UpGrpCD").Value = PropWorkGroupCD                                              '最終更新者グループCD
    '            .Parameters("UpdateID").Value = PropUserId                                                  '最終更新者ID
    '            .Parameters("IncNmb_Remove").Value = dataHBKC0201.PropIntINCNmb                             'インシデント番号（交換撤去）
    '            .Parameters("WorkNmb_Remove").Value = dataHBKC0201.PropIntExchangeWorkNmb                   '作業番号（交換撤去）
    '            .Parameters("CINmb_Remove").Value = dataHBKC0201.PropIntExchangeCINmb                       'CI番号（交換撤去）
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
    ''' 【編集モード】オプションソフト履歴新規登録用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>オプションソフト履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertOptSoftRirekiSql(ByRef Cmd As NpgsqlCommand, _
                                              ByVal Cn As NpgsqlConnection, _
                                              ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Parameters("RirekiNo").Value = dataHBKC0201.PropIntCIRirekiNo                      '履歴番号：CI履歴番号
                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                            '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                             '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                         '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))   'CI番号
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
    ''' 【編集モード】交換設置時セット機器管理削除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>セット機器管理削除（DELETE）用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteSetKikiMngWhenExchangeSql(ByRef Cmd As NpgsqlCommand, _
                                                       ByVal Cn As NpgsqlConnection, _
                                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(UPDATE)
            strSQL = strDeleteSetKikiMngSql


            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'インシデント番号
                .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))                  '作業番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
                .Add(New NpgsqlParameter("SetKikiID", NpgsqlTypes.NpgsqlDbType.Integer))                'セットID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                             '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                                'インシデント番号
                .Parameters("WorkNmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("WorkNmb"))   '作業番号
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号
                If dataHBKC0201.PropRowReg.Item("SetKikiID").ToString() <> "" Then
                    .Parameters("SetKikiID").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("SetKikiID"))       'セットID
                Else
                    .Parameters("SetKikiID").Value = DBNull.Value
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
    ''' 【編集モード】セット機器管理削除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>セット機器管理削除（DELETE）用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteSetKikiMngSql(ByRef Cmd As NpgsqlCommand, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(UPDATE)
            strSQL = strDeleteSetKikiMngSql


            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'インシデント番号
                .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))                  '作業番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
                .Add(New NpgsqlParameter("SetKikiID", NpgsqlTypes.NpgsqlDbType.Integer))                'セットID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                             '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                                'インシデント番号
                .Parameters("WorkNmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("WorkNmb"))   '作業番号
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号
                If dataHBKC0201.PropRowReg.Item("SetKikiID").ToString() <> "" Then
                    .Parameters("SetKikiID").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("SetKikiID"))       'セットID
                Else
                    .Parameters("SetKikiID").Value = DBNull.Value
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
    ''' 【編集モード】作業取消時セット機器管理削除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>作業取消時のセット機器管理削除（DELETE）用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/10/10 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteSetKikiMngForCancelSql(ByRef Cmd As NpgsqlCommand, _
                                                    ByVal Cn As NpgsqlConnection, _
                                                    ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(UPDATE)
            strSQL = strDeleteSetKikiMngForCancelSql


            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'インシデント番号
                .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))                  '作業番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                                'インシデント番号
                .Parameters("WorkNmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("WorkNmb"))   '作業番号
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号：交換撤去
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
    ''' 【編集モード】作業登録時データからのセット機器管理登録用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>作業登録時データからのセット機器管理登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/10/11 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertSetKikiFromRegSql(ByRef Cmd As NpgsqlCommand, _
                                               ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(INSERT)
            strSQL = strInsertSetKikiMngFromRegSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))              '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))               'インシデント番号
                .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))              '作業番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                                '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                         '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                 '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                             '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                                'インシデント番号
                .Parameters("WorkNmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("WorkNmb"))   '作業番号
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号
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
    ''' 【編集モード】現在と作業登録時のCI共通情報.セットIDクリア用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>現在と作業登録時のCI共通情報.セットIDクリア用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/10/10 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateSetIDClearSql(ByRef Cmd As NpgsqlCommand, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(INSERT)
            strSQL = strUpdateCIInfoSetKikiIDClearSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
                .Add(New NpgsqlParameter("RirekiNo_Reg", NpgsqlTypes.NpgsqlDbType.Integer))         '履歴番号：作業登録時
                .Add(New NpgsqlParameter("RirekiNo_Last", NpgsqlTypes.NpgsqlDbType.Integer))        '履歴番号：最終更新時
                .Add(New NpgsqlParameter("SetKikiID", NpgsqlTypes.NpgsqlDbType.Integer))            'セットID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                                     '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                                  '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                                      '最終更新者ID
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))               'CI番号
                .Parameters("RirekiNo_Reg").Value = _
                    Integer.Parse(dataHBKC0201.PropRowReg.Item("RegRirekiNo")) + 1                              '履歴番号：作業登録時
                .Parameters("RirekiNo_Last").Value = _
                    Integer.Parse(dataHBKC0201.PropRowReg.Item("LastUpRirekiNo"))                               '履歴番号：最終更新時
                If dataHBKC0201.PropRowReg.Item("SetKikiID").ToString() <> "" Then
                    .Parameters("SetKikiID").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("SetKikiID"))   'セットID
                Else
                    .Parameters("SetKikiID").Value = DBNull.Value
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
    ''' 【編集モード】作業登録時データからのCI共通情報.セットID更新用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>作業登録時データからのCI共通情報.セットID更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/10/11 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateSetIDFromRegSql(ByRef Cmd As NpgsqlCommand, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(INSERT)
            strSQL = strUpdateCIInfoSetKikiIDFromRegSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))             '履歴番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                                     '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                                  '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                                      '最終更新者ID
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))               'CI番号
                .Parameters("RirekiNo").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("RegRirekiNo")) + 1  '履歴番号：作業登録時履歴番号
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

    '【ADD】2014/04/07 e.okamura 作業取消時セット機器更新修正 START
    ''' <summary>
    ''' 【編集モード】指定したCI共通情報.セットIDクリア用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>指定したCI共通情報.セットIDクリア用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2014/04/07 e.okamura
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateCIInfoSetKikiIDClearTargetOnlySql(ByRef Cmd As NpgsqlCommand, _
                                                               ByVal Cn As NpgsqlConnection, _
                                                               ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(INSERT)
            strSQL = strUpdateCIInfoSetKikiIDClearTargetOnlySql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                         '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropIntCINmbSetIDClear)     'CI番号
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
    '【ADD】2014/04/07 e.okamura 作業取消時セット機器更新修正 END

    ''' <summary>
    ''' 【編集モード】交換設置セット機器登録用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>画面表示中の交換設置セット機器登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertSetKikiExchangeSql(ByRef Cmd As NpgsqlCommand, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(INSERT)
            strSQL = strInsertSetKikiMngExchangeSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb_Set", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号（交換設置）
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                  '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                 '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                    '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
                .Add(New NpgsqlParameter("SetKikiID_Remove", NpgsqlTypes.NpgsqlDbType.Integer))         'セットID（交換撤去）
                .Add(New NpgsqlParameter("CINmb_Remove", NpgsqlTypes.NpgsqlDbType.Integer))             'CI番号（交換撤去）
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   '
                .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))                  '
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb_Set").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))           'CI番号（交換設置）
                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                                     '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                                  '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                                      '最終更新者ID   
                If dataHBKC0201.PropStrExchangeSetKikiID.ToString() <> "" Then
                    .Parameters("SetKikiID_Remove").Value = Integer.Parse(dataHBKC0201.PropStrExchangeSetKikiID)   'セットID（交換撤去）
                Else
                    .Parameters("SetKikiID_Remove").Value = DBNull.Value
                End If
                .Parameters("CINmb_Remove").Value = dataHBKC0201.PropIntExchangeCINmb                           'CI番号（交換撤去）
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                                        '
                .Parameters("WorkNmb").Value = dataHBKC0201.PropIntExchangeWorkNmb                              '
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
    ''' 【編集モード】交換設置CI共通情報セット機器ID更新用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>画面表示中の交換設置セット機器登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateCIInfoSetKikiIDExchangeSql(ByRef Cmd As NpgsqlCommand, _
                                                        ByVal Cn As NpgsqlConnection, _
                                                        ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateCIInfoSetKikiIDExchangeSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb_Set", NpgsqlTypes.NpgsqlDbType.Integer))                        'CI番号（交換設置）
                .Add(New NpgsqlParameter("SetKikiID_Remove", NpgsqlTypes.NpgsqlDbType.Integer))                 'セットID（交換撤去）
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                       '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                         '最終更新者ID
                .Add(New NpgsqlParameter("CINmb_Remove", NpgsqlTypes.NpgsqlDbType.Integer))                     'CI番号（交換撤去）
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb_Set").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))               'CI番号（交換設置）
                If dataHBKC0201.PropStrExchangeSetKikiID.ToString() <> "" Then
                    .Parameters("SetKikiID_Remove").Value = Integer.Parse(dataHBKC0201.PropStrExchangeSetKikiID)    'セットID（交換撤去）
                Else
                    .Parameters("SetKikiID_Remove").Value = DBNull.Value
                End If
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                                         '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                                      '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                                          '最終更新者ID    
                .Parameters("CINmb_Remove").Value = dataHBKC0201.PropIntExchangeCINmb                               'CI番号（交換撤去）
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
    ''' 【編集モード】セット機器管理削除用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>セット機器管理削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteSetKikiMngForCepalateSql(ByRef Cmd As NpgsqlCommand, _
                                                      ByVal Cn As NpgsqlConnection, _
                                                      ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(INSERT)
            strSQL = strDeleteSetKikiMngSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("SetKikiID", NpgsqlTypes.NpgsqlDbType.Integer))                    'セットID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                        'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                If dataHBKC0201.PropRowReg.Item("SetKikiID").ToString() <> "" Then
                    .Parameters("SetKikiID").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("SetKikiID"))   'セットID
                Else
                    .Parameters("SetKikiID").Value = DBNull.Value
                End If
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))               'CI番号
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
    ''' 【編集モード】交換撤去時、セット機器管理削除用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>交換撤去時のセット機器管理削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/10/10 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteSetKikiMngWhenExchangeRemoveSql(ByRef Cmd As NpgsqlCommand, _
                                                             ByVal Cn As NpgsqlConnection, _
                                                             ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(INSERT)
            strSQL = strDeleteSetKikiMngWhenExchangeRemoveSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                        'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = dataHBKC0201.PropIntExchangeCINmb                              'CI番号（交換撤去）
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
    ''' 【編集モード】セット機器管理新規登録用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>セット機器管理新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertSetKikiMngForSetPairSql(ByRef Cmd As NpgsqlCommand, _
                                                     ByVal Cn As NpgsqlConnection, _
                                                     ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(INSERT)
            strSQL = strInsertSetKikiMngSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("SetKikiID", NpgsqlTypes.NpgsqlDbType.Integer))            'セットID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
                .Add(New NpgsqlParameter("EndUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))             'エンドユーザーID
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))              '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("SetKikiID").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("SetKikiID"))   'セットID
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))           'CI番号
                .Parameters("EndUsrID").Value = ""                                                          'エンドユーザーID
                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                                    '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                             '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                     '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                                 '最終更新日時
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
    ''' 【編集モード】セット機器履歴新規登録用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>セット機器履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertSetKikiRirekiSql(ByRef Cmd As NpgsqlCommand, _
                                              ByVal Cn As NpgsqlConnection, _
                                              ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Add(New NpgsqlParameter("SetKikiID", NpgsqlTypes.NpgsqlDbType.Integer))            'セットID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("RirekiNo").Value = dataHBKC0201.PropIntCIRirekiNo                              '履歴番号
                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                                    '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                             '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                     '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                                 '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                              '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                                  '最終更新者ID
                If dataHBKC0201.PropRowReg.Item("SetKikiID").ToString <> "" Then
                    .Parameters("SetKikiID").Value = _
                        Integer.Parse(dataHBKC0201.PropRowReg.Item("SetKikiID"))                            'セットID
                Else
                    .Parameters("SetKikiID").Value = DBNull.Value
                End If
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))           'CI番号
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
    ''' 【編集モード】行追加時セット機器履歴新規登録用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>セット機器履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/10/11 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertSetKikiRirekiWhenAddSql(ByRef Cmd As NpgsqlCommand, _
                                                     ByVal Cn As NpgsqlConnection, _
                                                     ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Add(New NpgsqlParameter("SetKikiID", NpgsqlTypes.NpgsqlDbType.Integer))            'セットID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("RirekiNo").Value = dataHBKC0201.PropIntCIRirekiNo                              '履歴番号
                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                                    '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                             '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                     '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                                 '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                              '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                                  '最終更新者ID
                If dataHBKC0201.PropRowReg.Item("SetKikiID").ToString <> "" Then
                    .Parameters("SetKikiID").Value = _
                        Integer.Parse(dataHBKC0201.PropRowReg.Item("SetKikiID"))                            'セットID
                Else
                    .Parameters("SetKikiID").Value = DBNull.Value
                End If
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))           'CI番号
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
    ''' 【編集モード】複数人利用物理削除用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>複数人利用物理削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/01 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteShareSql(ByRef Cmd As NpgsqlCommand, _
                                      ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(DELETE)
            strSQL = strDeleteShareSql

            'データアダプタに、SQLを設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))   'CI番号
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
    ''' 【編集モード】作業登録前のオプションソフト登録用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>作業登録前のオプションソフト登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertOptSoftFromBefSql(ByRef Cmd As NpgsqlCommand, _
                                               ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(INSERT)
            strSQL = strInsertOptSoftFromBefSql

            'データアダプタに、SQLを設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'インシデント番号
                .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))                  '作業番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                             '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                                'インシデント番号
                .Parameters("WorkNmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("WorkNmb"))   '作業番号
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号
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
    ''' 【編集モード】作業登録前の複数人利用登録用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>作業登録前の複数人利用登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertShareFromBefSql(ByRef Cmd As NpgsqlCommand, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(INSERT)
            strSQL = strInsertShareFromBefSql

            'データアダプタに、SQLを設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


           'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'インシデント番号
                .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))                  '作業番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                             '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                                'インシデント番号
                .Parameters("WorkNmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("WorkNmb"))   '作業番号
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号
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
    ''' 【編集モード】交換設置：複数人利用登録用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>交換撤去データの複数人利用登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/12 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertShareWhenExchange(ByRef Cmd As NpgsqlCommand, _
                                               ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(INSERT)
            strSQL = strInsertShareWhenExchangeSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb_Set", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号（交換設置）
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                  '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                 '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                    '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
                .Add(New NpgsqlParameter("CINmb_Remove", NpgsqlTypes.NpgsqlDbType.Integer))             'CI番号（交換撤去）
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb_Set").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号（交換設置）
                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                                    '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                             '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                     '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                                 '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                              '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                                  '最終更新者ID
                .Parameters("CINmb_Remove").Value = dataHBKC0201.PropIntExchangeCINmb                       'CI番号（交換撤去）
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
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>複数人利用履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/31 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertShareRirekiSql(ByRef Cmd As NpgsqlCommand, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Parameters("RirekiNo").Value = dataHBKC0201.PropIntCIRirekiNo                      '履歴番号：CI履歴番号
                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                            '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                             '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                         '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))   'CI番号
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
    ''' 【編集モード】作業追加時：登録理由履歴新規登録用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>登録理由履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertRegReasonWhenWorkAddedSql(ByRef Cmd As NpgsqlCommand, _
                                                       ByVal Cn As NpgsqlConnection, _
                                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Add(New NpgsqlParameter("RegReason", NpgsqlTypes.NpgsqlDbType.Varchar))            '登録理由
                .Add(New NpgsqlParameter("WorkCD", NpgsqlTypes.NpgsqlDbType.Varchar))               '作業コード
                .Add(New NpgsqlParameter("WorkKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))            '作業区分コード
                .Add(New NpgsqlParameter("ChgFlg", NpgsqlTypes.NpgsqlDbType.Varchar))               '交換フラグ
                .Add(New NpgsqlParameter("ChgCINmb", NpgsqlTypes.NpgsqlDbType.Integer))             '交換CI番号
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))              '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
                .Add(New NpgsqlParameter("WorkBiko", NpgsqlTypes.NpgsqlDbType.Varchar))                 '作業備考
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))   'CI番号
                .Parameters("RirekiNo").Value = dataHBKC0201.PropIntCIRirekiNo                      '履歴番号：CI履歴番号
                .Parameters("RegReason").Value = ""                                                 '登録理由：空白
                .Parameters("WorkCD").Value = dataHBKC0201.PropCmbWork.SelectedValue                '作業コード
                .Parameters("WorkKbnCD").Value = WORK_KBN_CD_PREPAIR                                '作業区分コード：準備
                .Parameters("ChgFlg").Value = CHANGE_FLG_OFF                                        '交換フラグ
                .Parameters("ChgCINmb").Value = DBNull.Value                                        '交換CI番号
                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                            '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                             '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                         '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
                Dim dr() As DataRow = dataHBKC0201.PropDtSapMainte.Select("Num='" & dataHBKC0201.PropRowReg.Item("Num") & "' " _
                                                                       & " AND KindCD=" & Integer.Parse(dataHBKC0201.PropRowReg.Item("KindCD")))
                If dr.Length <> 0 Then
                    .Parameters("WorkBiko").Value = dr(0).Item("WorkBiko")            '作業備考
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
    ''' 【編集モード】交換設置時：登録理由履歴更新用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>交換設置時の登録理由履歴更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateRegReasonWhenExchangeSetSql(ByRef Cmd As NpgsqlCommand, _
                                                         ByVal Cn As NpgsqlConnection, _
                                                         ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateRegReasonFromPairSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("ChgFlg", NpgsqlTypes.NpgsqlDbType.Varchar))                   '交換フラグ
                .Add(New NpgsqlParameter("ChgCINmb", NpgsqlTypes.NpgsqlDbType.Integer))                 '交換CI番号
                '.Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                '.Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                '.Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
                '.Add(New NpgsqlParameter("CINmb_From", NpgsqlTypes.NpgsqlDbType.Integer))               'CI番号
                '.Add(New NpgsqlParameter("LastUpRirekiNo_From", NpgsqlTypes.NpgsqlDbType.Integer))      '最終更新履歴No：交換撤去
                .Add(New NpgsqlParameter("CINmb_To", NpgsqlTypes.NpgsqlDbType.Integer))                 'CI番号
                .Add(New NpgsqlParameter("LastUpRirekiNo_To", NpgsqlTypes.NpgsqlDbType.Integer))        '最終更新履歴No
                .Add(New NpgsqlParameter("WorkBiko", NpgsqlTypes.NpgsqlDbType.Varchar))                 '作業備考
            End With

            'バインド変数に値をセット
            With Cmd
                If dataHBKC0201.PropRowReg.Item("ChgNmb").ToString() <> "" Then
                    .Parameters("ChgFlg").Value = CHANGE_FLG_ON                                         '交換フラグ       
                    .Parameters("ChgCINmb").Value = dataHBKC0201.PropIntExchangeCINmb                   '交換CI番号
                Else
                    .Parameters("ChgFlg").Value = CHANGE_FLG_OFF
                    .Parameters("ChgCINmb").Value = DBNull.Value
                End If
                '.Parameters("ChgFlg").Value = CHANGE_FLG_ON                                             '交換フラグ：ON
                '.Parameters("ChgCINmb").Value = dataHBKC0201.PropIntExchangeCINmb                       '交換CI番号
                '.Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                             '最終更新日時
                '.Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                '.Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
                '.Parameters("CINmb_From").Value = dataHBKC0201.PropIntExchangeCINmb                     'CI番号：交換撤去
                '.Parameters("LastUpRirekiNo_From").Value = dataHBKC0201.PropIntExchangeLastUpRirekiNo   '最終更新履歴No：交換撤去
                .Parameters("CINmb_To").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))    'CI番号
                .Parameters("LastUpRirekiNo_To").Value = dataHBKC0201.PropRowReg.Item("LastUpRirekiNo") '最終更新履歴No
                .Parameters("WorkBiko").Value = dataHBKC0201.PropRowReg.Item("WorkBiko")                '作業備考
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
    ''' 【編集モード】作業完了時：登録理由履歴更新用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>作業完了時の登録理由履歴更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateRegReasonCompleteSql(ByRef Cmd As NpgsqlCommand, _
                                                  ByVal Cn As NpgsqlConnection, _
                                                  ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateRegReasonSql_Complete

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("ChgFlg", NpgsqlTypes.NpgsqlDbType.Varchar))                   '交換フラグ
                .Add(New NpgsqlParameter("ChgCINmb", NpgsqlTypes.NpgsqlDbType.Integer))                 '交換CI番号
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'インシデント番号
                .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))                  '作業番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
                .Add(New NpgsqlParameter("WorkBiko", NpgsqlTypes.NpgsqlDbType.Varchar))                 '作業備考
            End With

            'バインド変数に値をセット
            With Cmd
                If dataHBKC0201.PropRowReg.Item("ChgNmb").ToString() <> "" Then
                    .Parameters("ChgFlg").Value = CHANGE_FLG_ON                                             '交換フラグ       
                    .Parameters("ChgCINmb").Value = dataHBKC0201.PropIntExchangeCINmb                       '交換CI番号
                Else
                    .Parameters("ChgFlg").Value = CHANGE_FLG_OFF
                    .Parameters("ChgCINmb").Value = DBNull.Value
                End If
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                                 '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                              '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                                  '最終更新者ID
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                                    'インシデント番号
                .Parameters("WorkNmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("WorkNmb"))       '作業番号
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))           'CI番号
                .Parameters("WorkBiko").Value = dataHBKC0201.PropRowReg.Item("WorkBiko")                    '作業備考
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
    ''' 【編集モード】作業取消時：登録理由履歴更新用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>作業取消時の登録理由履歴更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateRegReasonCancelSql(ByRef Cmd As NpgsqlCommand, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateRegReasonSql_Cancel

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("ChgFlg", NpgsqlTypes.NpgsqlDbType.Varchar))                   '交換フラグ
                .Add(New NpgsqlParameter("ChgCINmb", NpgsqlTypes.NpgsqlDbType.Integer))                 '交換CI番号
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'インシデント番号
                .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))                  '作業番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
                .Add(New NpgsqlParameter("WorkBiko", NpgsqlTypes.NpgsqlDbType.Varchar))                 '作業備考
            End With

            'バインド変数に値をセット
            With Cmd
                If dataHBKC0201.PropRowReg.Item("ChgNmb").ToString() <> "" Then
                    .Parameters("ChgFlg").Value = CHANGE_FLG_ON                                         '交換フラグ       
                    .Parameters("ChgCINmb").Value = dataHBKC0201.PropIntExchangeCINmb                   '交換CI番号
                Else
                    .Parameters("ChgFlg").Value = CHANGE_FLG_OFF
                    .Parameters("ChgCINmb").Value = DBNull.Value
                End If
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                             '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                                'インシデント番号
                .Parameters("WorkNmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("WorkNmb"))   '作業番号
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号
                .Parameters("WorkBiko").Value = dataHBKC0201.PropRowReg.Item("WorkBiko")                '作業備考
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
    ''' 【編集モード】作業追加時：原因リンク履歴新規登録用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>原因リンク履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCauseLinkWhenWorkAddedSql(ByRef Cmd As NpgsqlCommand, _
                                                       ByVal Cn As NpgsqlConnection, _
                                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

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
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))           'プロセス区分
                .Add(New NpgsqlParameter("MngNmb", NpgsqlTypes.NpgsqlDbType.Integer))               '管理番号
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))              '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))   'CI番号
                .Parameters("RirekiNo").Value = dataHBKC0201.PropIntCIRirekiNo                      '履歴番号：CI履歴番号
                .Parameters("ProcessKbn").Value = PROCESS_TYPE_INCIDENT                             'プロセス区分：インシデント
                .Parameters("MngNmb").Value = dataHBKC0201.PropIntINCNmb                            '管理番号：インシデント番号
                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                            '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                             '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                         '最終更新日時
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
    ''' 【編集モード】作業完了／取消前：原因リンク履歴新規登録用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>登録時の原因リンク履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCauseLinkBefCompCancelSql(ByRef Cmd As NpgsqlCommand, _
                                                       ByVal Cn As NpgsqlConnection, _
                                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(INSERT)
            strSQL = strInsertBefCauseLinkSql

            'データアダプタに、SQL文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))             '履歴番号
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))           'プロセス区分
                .Add(New NpgsqlParameter("MngNmb", NpgsqlTypes.NpgsqlDbType.Integer))               '管理番号
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))              '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
                .Add(New NpgsqlParameter("RegRirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))          '登録時履歴番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))   'CI番号
                .Parameters("RirekiNo").Value = dataHBKC0201.PropIntCIRirekiNo                      '履歴番号：CI履歴番号
                .Parameters("ProcessKbn").Value = PROCESS_TYPE_INCIDENT                             'プロセス区分：インシデント
                .Parameters("MngNmb").Value = dataHBKC0201.PropIntINCNmb                            '管理番号：インシデント番号
                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                            '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                             '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                         '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
                .Parameters("RegRirekiNo").Value = dataHBKC0201.PropRowReg.Item("RegRirekiNo") + 1  '登録時履歴番号
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
    ''' 【編集モード】登録時：保存用テーブルからの原因リンク履歴新規登録用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>保存用テーブルからの原因リンク履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/01 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCauseLinkWhenRegSql(ByRef Cmd As NpgsqlCommand, _
                                                 ByVal Cn As NpgsqlConnection, _
                                                 ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(INSERT)
            strSQL = strInsertCauseLinkSql

            'データアダプタに、SQLを設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))                 '履歴番号
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                  '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                 '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                    '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   'インシデント番号
                .Add(New NpgsqlParameter("WorkNmb", NpgsqlTypes.NpgsqlDbType.Integer))                  '作業番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("RirekiNo").Value = dataHBKC0201.PropIntCIRirekiNo                          '履歴番号：CI履歴番号
                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                                '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                         '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                 '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                             '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                                'インシデント番号
                .Parameters("WorkNmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("WorkNmb"))   '作業番号
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号
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
    ''' 共通情報ロックテーブル、サーバー日付取得処理
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

            strSQL = strSelectINCInfoSql

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
    ''' 共通情報ロックテーブル登録処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>共通情報ロックテーブル登録
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

            strSQL = strInsertINCLockSql

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
    ''' <remarks>共通情報ロックテーブル削除する
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

            strSQL = strDeleteINCLockSql

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
    ''' 【共通】サポセン機器：メール作成時最終お知らせ日更新用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>メール作成時のCIサポセン機器の最終お知らせ日更新用SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/29 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function SetUpdateLastInfoDtForSapSql(ByRef Cmd As NpgsqlCommand, _
                                                 ByVal Cn As NpgsqlConnection, _
                                                 ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSQL As String = ""

        Try

            '最終お知らせ日更新用SQLをセット
            strSQL = strUpdateLastInfoDtForSapSql

            'コマンドに、SQLを設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                         '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))   'CI番号
            End With

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
    ''' 【共通】部所有機器：メール作成時最終お知らせ日更新用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>メール作成時のCI部所有機器の最終お知らせ日更新用SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/29 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function SetUpdateLastInfoDtForBuySql(ByRef Cmd As NpgsqlCommand, _
                                                 ByVal Cn As NpgsqlConnection, _
                                                 ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSQL As String = ""

        Try

            '最終お知らせ日更新用SQLをセット
            strSQL = strUpdateLastInfoDtForBuySql

            'コマンドに、SQLを設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                'CI番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                         '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))   'CI番号
            End With

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
    ''' 【共通】メール作成時：CI部所有機器履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI部所有機器履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/29 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsetCIBuyRirekiSql(ByRef Cmd As NpgsqlCommand, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertCIBuyRirekiSql

            'データアダプタに、SQLのINSERT文を設定
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
                .Parameters("RirekiNo").Value = dataHBKC0201.PropIntCIRirekiNo                      '履歴番号：CI履歴番号
                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                            '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                             '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                         '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))   'CI番号
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
    ''' 【編集モード】サポセン機器：作業登録時の登録理由履歴新規登録用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>作業登録時のCIサポセン機器の登録理由履歴新規登録用SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/26 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function SetInsertRegReasonBefCompCancelSql(ByRef Cmd As NpgsqlCommand, _
                                                       ByVal Cn As NpgsqlConnection, _
                                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSQL As String = ""

        Try

            '作業登録時の登録理由履歴登録用SQLをセット
            strSQL = strInsertBefRegReasonSql

            'コマンドに、SQLを設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))                 '履歴番号
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                  '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                 '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                    '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
                .Add(New NpgsqlParameter("RegRirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))              '登録時履歴番号
                .Add(New NpgsqlParameter("WorkBiko", NpgsqlTypes.NpgsqlDbType.Varchar))                 '作業備考
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("RirekiNo").Value = dataHBKC0201.PropIntCIRirekiNo                          '履歴番号：CI履歴番号
                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                                '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                         '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                 '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                             '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号
                .Parameters("RegRirekiNo").Value = _
                    Integer.Parse(dataHBKC0201.PropRowReg.Item("RegRirekiNo")) + 1                      '登録時履歴番号
                .Parameters("WorkBiko").Value = dataHBKC0201.PropRowReg.Item("WorkBiko")                '作業備考
            End With

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
    ''' 【共通】サポセン機器：メール作成時登録理由履歴新規登録用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>メール作成時のCIサポセン機器の登録理由履歴新規登録用SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/29 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function SetInsertRegReasonWhenCreateMailForSapSql(ByRef Cmd As NpgsqlCommand, _
                                                              ByVal Cn As NpgsqlConnection, _
                                                              ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSQL As String = ""

        Try

            '登録理由履歴登録用SQLをセット
            strSQL = strInsertRegReasonSql

            'コマンドに、SQLを設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))                 '履歴番号
                .Add(New NpgsqlParameter("RegReason", NpgsqlTypes.NpgsqlDbType.Varchar))                '登録理由
                .Add(New NpgsqlParameter("WorkCD", NpgsqlTypes.NpgsqlDbType.Varchar))                   '作業CD
                .Add(New NpgsqlParameter("WorkKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))                '作業種別CD
                .Add(New NpgsqlParameter("ChgFlg", NpgsqlTypes.NpgsqlDbType.Varchar))                   '交換フラグ
                .Add(New NpgsqlParameter("ChgCINmb", NpgsqlTypes.NpgsqlDbType.Integer))                 '交換CI番号
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                  '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                 '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                    '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID

                '2013/02/08 y.ikushima パラメータ不足修正 START
                .Add(New NpgsqlParameter("WorkBiko", NpgsqlTypes.NpgsqlDbType.Varchar))                 '作業備考
                '2013/02/08 y.ikushima パラメータ不足修正 END
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号
                .Parameters("RirekiNo").Value = dataHBKC0201.PropIntCIRirekiNo                          '履歴番号：CI履歴番号
                .Parameters("RegReason").Value = REGREASON_TEXT_UPDATE_LASTINFODT                       '登録理由：固定文言
                .Parameters("WorkCD").Value = WORK_CD_ADDCONFIG                                         '作業CD：追加設定
                .Parameters("WorkKbnCD").Value = WORK_KBN_CD_COMPLETE                                   '作業種別CD：完了
                .Parameters("ChgFlg").Value = DBNull.Value                                              '交換フラグ：設定なし
                .Parameters("ChgCINmb").Value = DBNull.Value                                            '交換CI番号：設定なし
                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                                '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                         '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                 '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                             '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID

                '2013/02/08 y.ikushima パラメータ不足修正 START
                .Parameters("WorkBiko").Value = DBNull.Value                 '作業備考(空文字)
                '2013/02/08 y.ikushima パラメータ不足修正 END
            End With

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
    ''' 【共通】部所有機器：メール作成時登録理由履歴新規登録用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>メール作成時のCI部所有機器の登録理由履歴新規登録用SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/29 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function SetInsertRegReasonWhenCreateMailForBuySql(ByRef Cmd As NpgsqlCommand, _
                                                              ByVal Cn As NpgsqlConnection, _
                                                              ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSQL As String = ""

        Try

            '登録理由履歴登録用SQLをセット
            strSQL = strInsertRegReasonSql

            'コマンドに、SQLを設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))                 '履歴番号
                .Add(New NpgsqlParameter("RegReason", NpgsqlTypes.NpgsqlDbType.Varchar))                '登録理由
                .Add(New NpgsqlParameter("WorkCD", NpgsqlTypes.NpgsqlDbType.Varchar))                   '作業CD
                .Add(New NpgsqlParameter("WorkKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))                '作業種別CD
                .Add(New NpgsqlParameter("ChgFlg", NpgsqlTypes.NpgsqlDbType.Varchar))                   '交換フラグ
                .Add(New NpgsqlParameter("ChgCINmb", NpgsqlTypes.NpgsqlDbType.Integer))                 '交換CI番号
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                  '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                 '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                    '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID

                '2013/02/08 y.ikushima パラメータ不足修正 START
                .Add(New NpgsqlParameter("WorkBiko", NpgsqlTypes.NpgsqlDbType.Varchar))                 '作業備考
                '2013/02/08 y.ikushima パラメータ不足修正 END
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号
                .Parameters("RirekiNo").Value = dataHBKC0201.PropIntCIRirekiNo                          '履歴番号：CI履歴番号
                .Parameters("RegReason").Value = REGREASON_TEXT_UPDATE_LASTINFODT                       '登録理由：固定文言
                .Parameters("WorkCD").Value = DBNull.Value                                              '作業CD：設定なし
                .Parameters("WorkKbnCD").Value = DBNull.Value                                           '作業種別CD：設定なし
                .Parameters("ChgFlg").Value = DBNull.Value                                              '交換フラグ：設定なし
                .Parameters("ChgCINmb").Value = DBNull.Value                                            '交換CI番号：設定なし
                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                                '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                         '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                 '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                             '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID

                '2013/02/08 y.ikushima パラメータ不足修正 START
                .Parameters("WorkBiko").Value = DBNull.Value                  '作業備考(空文字)
                '2013/02/08 y.ikushima パラメータ不足修正 END
            End With

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
    ''' 【共通】サポセン機器：メール作成時原因リンク履歴新規登録用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>メール作成時のCIサポセン機器の原因リンク履歴新規登録用SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/29 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function SetInsertCauseLinkWhenCreateMailForSapSql(ByRef Cmd As NpgsqlCommand, _
                                                              ByVal Cn As NpgsqlConnection, _
                                                              ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSQL As String = ""

        Try

            '原因リンク履歴登録用SQLをセット
            strSQL = strInsertCauseLinkSql

            'コマンドに、SQLを設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))                 '履歴番号
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))               'プロセス区分
                .Add(New NpgsqlParameter("MngNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   '管理番号
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                  '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                 '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                    '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号
                .Parameters("RirekiNo").Value = dataHBKC0201.PropIntCIRirekiNo                          '履歴番号：CI履歴番号
                .Parameters("ProcessKbn").Value = PROCESS_TYPE_INCIDENT                                 'プロセス区分：インシデント
                .Parameters("MngNmb").Value = dataHBKC0201.PropIntINCNmb                                '管理番号：インシデント番号
                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                                '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                         '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                 '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                             '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
            End With

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
    ''' 【共通】部所有機器：メール作成時原因リンク履歴新規登録用SQLの作成・設定
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>メール作成時のCI部所有機器の原因リンク履歴新規登録用SQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/29 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function SetInsertCauseLinkWhenCreateMailForBuySql(ByRef Cmd As NpgsqlCommand, _
                                                              ByVal Cn As NpgsqlConnection, _
                                                              ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSQL As String = ""

        Try

            '原因リンク履歴登録用SQLをセット
            strSQL = strInsertCauseLinkSql

            'コマンドに、SQLを設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))                 '履歴番号
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))               'プロセス区分
                .Add(New NpgsqlParameter("MngNmb", NpgsqlTypes.NpgsqlDbType.Integer))                   '管理番号
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                  '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                 '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                    '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = Integer.Parse(dataHBKC0201.PropRowReg.Item("CINmb"))       'CI番号
                .Parameters("RirekiNo").Value = dataHBKC0201.PropIntCIRirekiNo                          '履歴番号：CI履歴番号
                .Parameters("ProcessKbn").Value = PROCESS_TYPE_INCIDENT                                 'プロセス区分：インシデント
                .Parameters("MngNmb").Value = dataHBKC0201.PropIntINCNmb                                '管理番号：インシデント番号
                .Parameters("RegDT").Value = dataHBKC0201.PropDtmSysDate                                '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                         '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                 '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0201.PropDtmSysDate                             '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                          '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                              '最終更新者ID
            End With

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
    ''' 【共通】マスタデータ取得：相手先
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>指定相手先取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetPartnerContactData(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectIncdentPartnerContactSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("endusrid", NpgsqlTypes.NpgsqlDbType.Varchar))
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("endusrid").Value = dataHBKC0201.PropTxtPartnerID.Text
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
    ''' 【編集／参照モード】インシデントSM連携取得用SQLの作成・設定処理	
    ''' </summary>	
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>	
    ''' <param name="dataHBKC0201">[IN]インシデント登録画面データクラス</param>	
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>インシデントSM連携取得用のSQLを作成し、アダプタにセットする	
    ''' <para>作成情報：2012/07/18 r.hoshino	
    ''' <p>改訂情報：</p>	
    ''' </para></remarks>
    Public Function SetSelectIncidentSMtutiSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectIncidentSMtutiTableSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))       'インシデント番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                    'INC番号
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
    ''' インシデントSM通知ログテーブル新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0201">[IN]インシデント登録データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>インシデントSM通知ログテーブル新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/20 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncidentSMtutiLSql(ByRef Cmd As NpgsqlCommand, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKC0201 As DataHBKC0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String

        Try
            'インシデントSM連携指示テーブル新規登録用SQLを設定
            strSQL = strInsertIncidentSMtutiLsql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))       'INC番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKC0201.PropIntLogNo                  'ログNo
                .Parameters("IncNmb").Value = dataHBKC0201.PropIntINCNmb                'INC番号
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
