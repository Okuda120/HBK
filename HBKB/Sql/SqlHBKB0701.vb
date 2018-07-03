Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' 機器一括検索一覧画面Sqlクラス
''' </summary>
''' <remarks>機器一括検索一覧画面のSQLの作成・設定を行う
''' <para>作成情報：2012/06/21 k.ueda
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKB0701

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK


    '定数
    Private Const SEARCH_MODE_SEARCH As Integer = 0         '検索結果
    Private Const SEARCH_MODE_COUNT As Integer = 1          '検索件数


    '*************************
    '* SQL文宣言
    '*************************

    '種別マスタ／種別名取得（SELECT）SQL
    Private strSelectKindMastaSql As String = "SELECT km.kindcd, km.kindnm,cast (row_number() over (order by km.kindcd) as integer) AS index " & vbCrLf &
                                              "FROM kind_mtb AS km " & vbCrLf &
                                              "WHERE NOT EXISTS ( SELECT '1' " & vbCrLf &
                                                                  "FROM kind_mtb AS km2 " & vbCrLf &
                                                                  "WHERE km2.jtiflg = '1' " & vbCrLf &
                                                                  "AND km2.kindcd = km.kindcd " & vbCrLf &
                                                                ") " & vbCrLf &
                                              "AND   ( km.cikbncd = :CINmbSUPORT OR km.cikbncd = :CINmbKIKI )" & vbCrLf &
                                              "ORDER BY km.sort ASC"

    'CIステータスマスタ/ステータス名取得 
    Private strSelectCIStateMastasql As String = "SELECT" & vbCrLf & _
                                                 " cm.cistatecd," & vbCrLf & _
                                                 " (CASE WHEN cm.CIKbnCD ='" & CI_TYPE_KIKI & "' THEN '【部】' || cm.cistatenm" & _
                                                 "       ELSE cm.cistatenm" & _
                                                 "  END) AS cistatenm" & vbCrLf & _
                                                 " FROM cistate_mtb AS cm" & vbCrLf &
                                                 " WHERE NOT EXISTS ( SELECT '1'" & vbCrLf &
                                                                    " FROM cistate_mtb AS cm2" & vbCrLf &
                                                                    " WHERE cm2.jtiflg = '1'" & vbCrLf &
                                                                    " AND cm.cistatecd = cm2.cistatecd" & vbCrLf &
                                                                    " )" & vbCrLf &
                                                 " AND ( cm.cikbncd = :CINmbSUPORT OR cm.cikbncd = :CINmbKIKI )" & vbCrLf &
                                                 " ORDER BY cm.sort ASC"

    '作業マスタ/作業名取得
    Private strSelectWorkMastasql As String = "SELECT wm.workcd, wm.worknm,cast (row_number() over (order by wm.workcd) as integer) AS index,wm.jtiflg " & vbCrLf &
                                              "FROM work_mtb AS wm " & vbCrLf &
                                              "WHERE NOT EXISTS ( SELECT '1' " & vbCrLf &
                                                                 "FROM work_mtb AS wm2 " & vbCrLf &
                                                                 "WHERE wm2.jtiflg = '1' " & vbCrLf &
                                                                 "AND wm.workcd = wm2.workcd" & vbCrLf &
                                                                ") " & vbCrLf &
                                              "ORDER BY wm.sort ASC "

    'サポセン機器タイプ/サポセン機器タイプ取得
    Private strSelectSapKikiTypeMastasql As String = "SELECT tm.sckikicd, tm.sckikitype,cast (row_number() over (order by tm.sckikicd) as integer) AS index " & vbCrLf &
                                                     "FROM sap_kiki_type_mtb AS tm " & vbCrLf &
                                                     "WHERE NOT EXISTS ( SELECT '1' " & vbCrLf &
                                                                        "FROM sap_kiki_type_mtb AS tm2 " & vbCrLf &
                                                                        "WHERE tm2.jtiflg = '1' " & vbCrLf &
                                                                        "AND tm.sckikicd = tm2.sckikicd" & vbCrLf &
                                                                       ") " & vbCrLf &
                                                     "ORDER BY tm.sort ASC"
    'ソフトマスタ/ソフト名称取得
    Private strSelectSoftMastasql As String = "SELECT sm.softcd,sm.softnm " & vbCrLf &
                                              "FROM soft_mtb AS sm " & vbCrLf &
                                              "WHERE NOT EXISTS( SELECT '1' " & vbCrLf &
                                                                "FROM soft_mtb AS sm2 " & vbCrLf &
                                                                "WHERE sm2.jtiflg = '1' " & vbCrLf &
                                                                "AND sm.softcd = sm2.softcd " & vbCrLf &
                                                               ") " & vbCrLf &
                                              "ORDER BY sm.sort ASC"

    '機器ステータスマスタ/ステータス名称取得
    Private strSelectKikiStatemastasql As String = "SELECT km.kikistatecd, km.kikistatenm " & vbCrLf &
                                                   "FROM kikistate_mtb AS km " & vbCrLf &
                                                   "WHERE NOT EXISTS ( SELECT '1' " & vbCrLf &
                                                                      "FROM kikistate_mtb AS km2 " & vbCrLf &
                                                                      "WHERE km2.jtiflg = '1' " & vbCrLf &
                                                                      "AND km.kikistatecd = km2.kikistatecd " & vbCrLf &
                                                                     ") " & vbCrLf &
                                                   "AND km.kikistatekbn = :Kikistatekbn" & vbCrLf &
                                                   "ORDER BY km.sort ASC"

    '作業区分マスタ/作業区分名取得
    Private strSelectWorkKbnMastasql As String = "SELECT wkm.workkbncd, wkm.workkbnnm,cast (row_number() over (order by wkm.workkbncd) as integer) AS index " & vbCrLf &
                                                 "FROM workkbn_mtb As wkm " & vbCrLf &
                                                 "WHERE NOT EXISTS ( SELECT '1' " & vbCrLf &
                                                                    "FROM workkbn_mtb AS wkm2 " & vbCrLf &
                                                                    "WHERE wkm2.jtiflg = '1' " & vbCrLf &
                                                                    "AND wkm.workkbncd = wkm2.workkbncd " & vbCrLf &
                                                                   ") " & vbCrLf &
                                                 "ORDER BY wkm.sort ASC"

    'マスター検索結果取得
    Private strSetSelectGetMastasql As String = "SELECT km.kindnm, ci.num, ci.class1, ci.class2, ci.cinm, skt.sckikitype, csm.cistatenm," & vbCrLf &
                                             "CASE " & vbCrLf &
                                                "WHEN ci.cikbncd = :cikbncdsuport " & vbCrLf &
                                                "THEN ks.kikistatenm " & vbCrLf &
                                                "ELSE '' " & vbCrLf &
                                             "END AS kikistatenm, " & vbCrLf &
                                             "CASE " & vbCrLf &
                                                "WHEN ci.cikbncd = :cikbncdsuport " & vbCrLf &
                                                "THEN cs.usrid " & vbCrLf &
                                                "WHEN ci.cikbncd = :cikbncdkiki " & vbCrLf &
                                                "THEN cb.usrid  " & vbCrLf &
                                                "ELSE '' " & vbCrLf &
                                             "END AS usrid, " & vbCrLf &
                                             "CASE " & vbCrLf &
                                                "WHEN ci.cikbncd = :cikbncdsuport " & vbCrLf &
                                                "THEN cs.usrnm " & vbCrLf &
                                                "WHEN ci.cikbncd = :cikbncdkiki " & vbCrLf &
                                                "THEN cb.usrnm " & vbCrLf &
                                                "ELSE '' " & vbCrLf &
                                             "END AS usrnm, " & vbCrLf &
                                             "CASE " & vbCrLf &
                                                "WHEN ci.cikbncd = :cikbncdsuport " & vbCrLf &
                                                "AND cs.rentaleddt <> '' " & vbCrLf &
                                                "THEN TO_CHAR(TO_DATE(cs.rentaleddt, 'YYYYMMDD'), 'YYYY/MM/DD') " & vbCrLf &
                                                "WHEN ci.cikbncd = :cikbncdkiki " & vbCrLf &
                                                "AND cb.expirationdt <> '' " & vbCrLf &
                                                "THEN TO_CHAR(TO_DATE(cb.expirationdt, 'YYYYMMDD'), 'YYYY/MM/DD') " & vbCrLf &
                                                "ELSE '' " & vbCrLf &
                                             "END AS rentaleddt, " & vbCrLf &
                                             "CASE " & vbCrLf &
                                                "WHEN ci.cikbncd = :cikbncdsuport " & vbCrLf &
                                                "AND cs.leaseupdt <> '' " & vbCrLf &
                                                "THEN TO_CHAR(TO_DATE(cs.leaseupdt, 'YYYYMMDD'), 'YYYY/MM/DD') " & vbCrLf &
                                                "ELSE '' " & vbCrLf &
                                             "END AS leaseupdt, " & vbCrLf &
                                             "ci.cinmb,cs.introductnmb,ci.kindcd,km.sort,ci.cikbncd " & vbCrLf &
                                             "FROM ( ci_info_tb AS ci " & vbCrLf &
                                                "left outer join kind_mtb AS km " & vbCrLf &
                                                "on ci.kindcd = km.kindcd AND ci.cikbncd = km.cikbncd " & vbCrLf &
                                                "left outer join ci_sap_tb AS cs " & vbCrLf &
                                                "on ci.cinmb = cs.cinmb " & vbCrLf &
                                                "left outer join cistate_mtb AS csm " & vbCrLf &
                                                "on ci.cistatuscd = csm.cistatecd " & vbCrLf &
                                                "left outer join ci_buy_tb AS cb " & vbCrLf &
                                                "on ci.cinmb = cb.cinmb ) " & vbCrLf &
                                             "left outer join sap_kiki_type_mtb AS skt " & vbCrLf &
                                             "on cs.typekbn = skt.sckikicd " & vbCrLf &
                                             "left outer join kikistate_mtb AS ks " & vbCrLf &
                                             "on cs.kikiusecd = ks.kikistatecd " & vbCrLf &
                                             "WHERE (ci.cikbncd = :cikbncdsuport " & vbCrLf &
                                             "or ci.cikbncd = :cikbncdkiki) "

    '    "left outer join optsoft_tb AS os " & vbCrLf &
    '"on ci.cinmb = os.cinmb ) " & vbCrLf &


    'マスター検索件数取得用
    Private strSelectMastaCountsql As String = "SELECT COUNT(*) " & vbCrLf &
                                               "FROM ( ci_info_tb AS ci " & vbCrLf &
                                                "left outer join kind_mtb AS km " & vbCrLf &
                                                "on ci.kindcd = km.kindcd AND ci.cikbncd = km.cikbncd " & vbCrLf &
                                                "left outer join ci_sap_tb AS cs " & vbCrLf &
                                                "on ci.cinmb = cs.cinmb " & vbCrLf &
                                                "left outer join cistate_mtb AS csm " & vbCrLf &
                                                "on ci.cistatuscd = csm.cistatecd " & vbCrLf &
                                                "left outer join ci_buy_tb AS cb " & vbCrLf &
                                                "on ci.cinmb = cb.cinmb ) " & vbCrLf &
                                             "left outer join sap_kiki_type_mtb AS skt " & vbCrLf &
                                             "on cs.typekbn = skt.sckikicd " & vbCrLf &
                                             "left outer join kikistate_mtb AS ks " & vbCrLf &
                                             "on cs.kikiusecd = ks.kikistatecd " & vbCrLf &
                                             "WHERE (ci.cikbncd = :cikbncdsuport " & vbCrLf &
                                             "or ci.cikbncd = :cikbncdkiki) "

    '導入一覧検索結果取得
    Private strSetSelectGetIntroductsql As String = "SELECT it.introductnmb,km.kindnm,it.kikinmbfrom,'～' as aida,it.kikinmbto,it.setnmb,it.class1,it.class2," & vbCrLf &
                                                 "it.cinm," & vbCrLf &
                                                 "CASE " & vbCrLf &
                                                 "WHEN it.introductstdt = '' " & vbCrLf &
                                                 "THEN '' " & vbCrLf &
                                                 "WHEN it.introductstdt <> '' " & vbCrLf &
                                                 "THEN to_char(to_date(it.introductstdt,'yyyymmdd'),'yyyy/mm/dd') " & vbCrLf &
                                                 "END AS introductstdt," & vbCrLf &
                                                 "CASE " & vbCrLf &
                                                 "WHEN it.introductkbn = :introductkbnlease " & vbCrLf &
                                                 "THEN '" & INTRODUCT_KBN_LEASE_NM & "' " & vbCrLf &
                                                 "WHEN it.introductkbn = :introductkbnbuy " & vbCrLf &
                                                 "THEN '" & INTRODUCT_KBN_KEIHI_NM & "' " & vbCrLf &
                                                 "END AS introductkbn," & vbCrLf &
                                                 "CASE " & vbCrLf &
                                                 "WHEN it.leaseupdt = '' " & vbCrLf &
                                                 "THEN '' " & vbCrLf &
                                                 "WHEN it.leaseupdt <> '' " & vbCrLf &
                                                 "THEN to_char(to_date(it.leaseupdt,'yyyymmdd'),'yyyy/mm/dd') " & vbCrLf &
                                                 "END AS leaseupdt," & vbCrLf &
                                                 "CASE " & vbCrLf &
                                                 "WHEN it.delscheduledt = '' " & vbCrLf &
                                                 "THEN '' " & vbCrLf &
                                                 "WHEN it.delscheduledt <> '' " & vbCrLf &
                                                 "THEN to_char(to_date(it.delscheduledt,'yyyymmdd'),'yyyy/mm/dd') " & vbCrLf &
                                                 "END AS delscheduledt," & vbCrLf &
                                                 "it.introductbiko " & vbCrLf &
                                                 "FROM introduct_tb AS it " & vbCrLf &
                                                 "left join kind_mtb AS km " & vbCrLf &
                                                 "on it.kindcd = km.kindcd"


    '導入一覧検索件数取得用　　
    Private strSelectIntroductCountsql As String = "SELECT COUNT(*) " & vbCrLf &
                                                 "FROM introduct_tb AS it " & vbCrLf &
                                                 "left join kind_mtb AS km " & vbCrLf &
                                                 "on it.kindcd = km.kindcd "

    '履歴検索結果取得
    Private strSetSelectGetRirekisql As String = "SELECT km.kindnm,rt.num,rt.class1,rt.class2,rt.cinm,wm.worknm,wkm.workkbnnm," & vbCrLf &
                                               "CASE rrt.ChgFlg" & vbCrLf &
                                                  "WHEN '" & CHANGE_FLG_ON & "'" & vbCrLf &
                                                  "THEN (SELECT km.KindNM || ct.Num FROM CI_INFO_TB ct JOIN KIND_MTB km ON ct.CIKbnCD = km.CIKbnCD AND ct.KindCD = km.KindCD AND ct.CINmb = rrt.ChgCINmb)" & vbCrLf &
                                               "ELSE '' END AS ChgKiki," & vbCrLf &
                                               "rt.rirekino,rt.cikbncd, " & vbCrLf &
                                               "CASE " & vbCrLf &
                                                  "WHEN rt.cikbncd = :cikbncdsuport " & vbCrLf &
                                                  "THEN sr.workfromnmb " & vbCrLf &
                                                  "WHEN rt.cikbncd = :cikbncdkiki " & vbCrLf &
                                                  "THEN br.workfromnmb " & vbCrLf &
                                               "END AS workfromnmb," & vbCrLf &
                                               "sm.cistatenm,TO_CHAR(rrt.regdt,'YYYY/MM/DD HH24:MI') as regdt,hm.hbkusrnm,rrt.cinmb,rt.sort, " & vbCrLf &
                                               "rrt.workbiko " & vbCrLf &
                                               "FROM (ci_info_rtb AS rt " & vbCrLf &
                                                   "left outer join kind_mtb AS km " & vbCrLf &
                                                   "on rt.kindcd = km.kindcd AND rt.cikbncd = km.cikbncd " & vbCrLf &
                                                   "left outer join regreason_rtb AS rrt " & vbCrLf &
                                                   "on rt.cinmb = rrt.cinmb " & vbCrLf &
                                                   "and rt.rirekino = rrt.rirekino " & vbCrLf &
                                                   "left outer join cistate_mtb AS sm " & vbCrLf &
                                                   "on rt.cistatuscd = sm.cistatecd " & vbCrLf &
                                                   "left outer join ci_sap_rtb AS sr " & vbCrLf &
                                                   "on rt.cinmb = sr.cinmb " & vbCrLf &
                                                   "and rt.rirekino = sr.rirekino " & vbCrLf &
                                                   "left outer join ci_buy_rtb AS br " & vbCrLf &
                                                   "on rt.cinmb = br.cinmb " & vbCrLf &
                                                   "and rt.rirekino = br.rirekino ) " & vbCrLf &
                                                "left outer join work_mtb AS wm " & vbCrLf &
                                                "on rrt.workcd = wm.workcd " & vbCrLf &
                                                "left join workkbn_mtb AS wkm " & vbCrLf &
                                                "on rrt.workkbncd = wkm.workkbncd " & vbCrLf &
                                                "left join hbkusr_mtb AS hm " & vbCrLf &
                                                "on rrt.regid = hm.hbkusrid " & vbCrLf &
                                                "WHERE (rt.cikbncd = :cikbncdsuport " & vbCrLf &
                                                "or rt.cikbncd = :cikbncdkiki) "

    '履歴検索件数取得用
    Private strSelectRirekiCountsql As String = "SELECT COUNT(*) " & vbCrLf &
                                                "FROM (ci_info_rtb AS rt " & vbCrLf &
                                                   "left outer join kind_mtb AS km " & vbCrLf &
                                                   "on rt.kindcd = km.kindcd AND rt.cikbncd = km.cikbncd " & vbCrLf &
                                                   "left outer join regreason_rtb AS rrt " & vbCrLf &
                                                   "on rt.cinmb = rrt.cinmb " & vbCrLf &
                                                   "and rt.rirekino = rrt.rirekino " & vbCrLf &
                                                   "left outer join cistate_mtb AS sm " & vbCrLf &
                                                   "on rt.cistatuscd = sm.cistatecd " & vbCrLf &
                                                   "left outer join ci_sap_rtb AS sr " & vbCrLf &
                                                   "on rt.cinmb = sr.cinmb " & vbCrLf &
                                                   "and rt.rirekino = sr.rirekino " & vbCrLf &
                                                   "left outer join ci_buy_rtb AS br " & vbCrLf &
                                                   "on rt.cinmb = br.cinmb " & vbCrLf &
                                                   "and rt.rirekino = br.rirekino )" & vbCrLf &
                                                "left outer join work_mtb AS wm " & vbCrLf &
                                                "on rrt.workcd = wm.workcd " & vbCrLf &
                                                "left join workkbn_mtb AS wkm " & vbCrLf &
                                                "on rrt.workkbncd = wkm.workkbncd " & vbCrLf &
                                                "left join hbkusr_mtb AS hm " & vbCrLf &
                                                "on rrt.regid = hm.hbkusrid " & vbCrLf &
                                                "WHERE (rt.cikbncd = :cikbncdsuport " & vbCrLf &
                                                "or rt.cikbncd = :cikbncdkiki) "



    ''' <summary>
    ''' 種別マスタ／CI種別名取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0701">[IN]機器一括検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>種別マスタ／CI種別名取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/21 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectKindMastaSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""


        Try
            '**********************************
            '* SQL文設定
            '**********************************

            'SQL文(SELECT)79
            strSQL = strSelectKindMastaSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '**********************************
            '* バインド変数に型と値をセット
            '**********************************

            'CI番号
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINmbSUPORT", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("CINmbSUPORT").Value = CI_TYPE_SUPORT

            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINmbKIKI", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("CINmbKIKI").Value = CI_TYPE_KIKI


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
    ''' CIステータスマスタ／ステータス名取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0701">[IN]機器一括検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>種別マスタ／CI種別名取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/21 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCIStatusMastaSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            '**********************************
            '* SQL文設定
            '**********************************

            'SQL文(SELECT)
            strSQL = strSelectCIStateMastasql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '**********************************
            '* バインド変数に型と値をセット
            '**********************************

            'CI番号
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINmbSUPORT", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("CINmbSUPORT").Value = CI_TYPE_SUPORT

            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINmbKIKI", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("CINmbKIKI").Value = CI_TYPE_KIKI


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
    ''' 作業マスタ／作業名取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0701">[IN]機器一括検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>作業マスタ／作業名取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/21 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectWorkMastaSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            '**********************************
            '* SQL文設定
            '**********************************

            'SQL文(SELECT)
            strSQL = strSelectWorkMastasql

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
    ''' サポセン機器タイプマスタ／サポセン機器タイプ取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0701">[IN]機器一括検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サポセン機器タイプマスタ／サポセン機器タイプ取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/21 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSapKikitypeMastaSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            '**********************************
            '* SQL文設定
            '**********************************

            'SQL文(SELECT)
            strSQL = strSelectSapKikiTypeMastasql

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
    ''' ソフトマスタ／ソフト名称取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0701">[IN]機器一括検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ソフトマスタ／ソフト名称取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/21 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSoftMastaSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            '**********************************
            '* SQL文設定
            '**********************************

            'SQL文(SELECT)
            strSQL = strSelectSoftMastasql

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
    ''' 機器ステータスマスタ／機器利用形態(ステータス名)取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0701">[IN]機器一括検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>機器ステータスマスタ／機器利用形態(ステータス名)取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/21 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectKikiStateMastaSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            '**********************************
            '* SQL文設定
            '**********************************

            'SQL文(SELECT)
            strSQL = strSelectKikiStatemastasql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            '**********************************
            '* バインド変数に型と値をセット
            '**********************************

            'CI番号
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("kikiStateKbn", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("Kikistatekbn").Value = "001"


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
    ''' 作業区分マスタ／作業区分名取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0701">[IN]機器一括検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>作業区分マスタ／作業区分名取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/26 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectWorkKbnMastaSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0701 As DataHBKB0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            '**********************************
            '* SQL文設定
            '**********************************

            'SQL文(SELECT)
            strSQL = strSelectWorkKbnMastasql

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
    ''' マスター検索結果件数取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0701">[IN]機器一括検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>マスター検索結果件数取得用のSQLを作成し、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2012/07/06 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetResultMastaCountSql(ByRef Adapter As NpgsqlDataAdapter, _
                                      ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKB0701 As DataHBKB0701) As Boolean

        Try
            '検索結果件数取得用SQLを設定
            Dim strSql As String = strSelectMastaCountsql

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            '検索条件作成
            If SetSelectWhereSearchMastaSql(Adapter, Cn, dataHBKB0701, strSql, SEARCH_MODE_COUNT) = False Then
                Return False
            End If

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False

        End Try

    End Function

    ''' <summary>
    ''' 導入一覧検索結果件数取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0701">[IN]機器一括検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>導入一覧検索結果件数取得用のSQLを作成し、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2012/07/06 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetResultIntroductCountSql(ByRef Adapter As NpgsqlDataAdapter, _
                                      ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKB0701 As DataHBKB0701) As Boolean

        '検索結果件数取得用SQLを設定
        Dim strSql As String = strSelectIntroductCountsql

        Try

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            '検索条件作成
            If SetSelectWhereSearchIntroductSql(Adapter, Cn, dataHBKB0701, strSql, SEARCH_MODE_COUNT) = False Then
                Return False
            End If

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False

        End Try

    End Function

    ''' <summary>
    ''' 履歴検索結果件数取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0701">[IN]機器一括検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>履歴検索結果件数取得用のSQLを作成し、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2012/07/06 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetResultRirekiCountSql(ByRef Adapter As NpgsqlDataAdapter, _
                                      ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKB0701 As DataHBKB0701) As Boolean

        '検索結果件数取得用SQLを設定
        Dim strSql As String = strSelectRirekiCountsql

        Try

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            '検索条件作成
            If SetSelectWhereSearchRirekiSql(Adapter, Cn, dataHBKB0701, strSql, SEARCH_MODE_COUNT) = False Then
                Return False
            End If

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False

        End Try

    End Function

    ''' <summary>
    ''' マスター検索結果取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0701">[IN]機器一括検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>マスター検索結果取得用のSQLを作成し、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2012/07/06 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSearchMastaSql(ByRef Adapter As NpgsqlDataAdapter, _
                                      ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKB0701 As DataHBKB0701) As Boolean

        'マスター検索結果取得用SQLを設定
        Dim strSql As String = strSetSelectGetMastasql

        Try

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            '検索条件作成
            If SetSelectWhereSearchMastaSql(Adapter, Cn, dataHBKB0701, strSql, SEARCH_MODE_SEARCH) = False Then
                Return False
            End If

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False

        End Try

    End Function

    ''' <summary>
    ''' 導入一覧検索結果取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0701">[IN]機器一括検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>導入一覧検索結果取得用のSQLを作成し、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2012/07/06 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSearchIntroductSql(ByRef Adapter As NpgsqlDataAdapter, _
                                      ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKB0701 As DataHBKB0701) As Boolean

        '導入一覧検索結果件数取得用SQLを設定
        Dim strSql As String = strSetSelectGetIntroductsql

        Try

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            '検索条件作成
            If SetSelectWhereSearchIntroductSql(Adapter, Cn, dataHBKB0701, strSql, SEARCH_MODE_SEARCH) = False Then
                Return False
            End If

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False

        End Try

    End Function


    ''' <summary>
    ''' 履歴検索結果取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0701">[IN]機器一括検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>履歴検索結果取得用のSQLを作成し、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2012/07/06 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSearchRirekiSql(ByRef Adapter As NpgsqlDataAdapter, _
                                      ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKB0701 As DataHBKB0701) As Boolean

        '導入一覧検索結果件数取得用SQLを設定
        Dim strSql As String = strSetSelectGetRirekisql

        Try

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            '検索条件作成
            If SetSelectWhereSearchRirekiSql(Adapter, Cn, dataHBKB0701, strSql, SEARCH_MODE_SEARCH) = False Then
                Return False
            End If

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False

        End Try

    End Function


    ''' <summary>
    ''' マスター検索結果取得用SQLのWHERE句の作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0701">[IN]機器一括検索一覧画面データクラス</param>
    ''' <param name="strSql">[IN]基本SQL</param>
    ''' <param name="intSearchFlg">[IN]SQL判別フラグ</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>マスター検索結果取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/27 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectWhereSearchMastaSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0701 As DataHBKB0701, _
                                          ByRef strSql As String, _
                                          ByVal intSearchFlg As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strWHERE As String = ""                 'WHERE句のSQLを格納する場所
        Dim strIntroductNo() As String = Nothing    '導入番号検索用配列
        Dim strFreeText() As String = Nothing       'フリーテキスト検索用配列
        '[add] 2015/08/21 y.naganuma フリーワード追加対応 START
        Dim strFreeWord() As String = Nothing       'フリーワード検索用配列
        '[add] 2015/08/21 y.naganuma フリーワード追加対応 END
        Dim intCnt As Integer                       'カウント変数

        Try
            '**********************************
            '* SQL文設定
            '**********************************

            'SQL文(SELECT)


            '検索項目に設定されている情報をSQL文に追記する処理

            '種別検索(複数選択可)

            With dataHBKB0701

                If .PropLstKind.SelectedValue <> Nothing Then


                    strWHERE &= " and "

                    strWHERE &= "("
                    For intCnt = 0 To .PropLstKind.SelectedItems.Count - 1
                        strWHERE &= "ci.kindcd = :kindcd" + intCnt.ToString()
                        If intCnt <> .PropLstKind.SelectedItems.Count - 1 Then
                            strWHERE &= " or "
                        End If
                    Next
                    strWHERE &= ")"

                End If

                'ステータス検索(複数選択可)

                If .PropLstStateNM.SelectedValue <> Nothing Then


                    strWHERE &= " and "

                    strWHERE &= "("
                    For intCnt = 0 To .PropLstStateNM.SelectedItems.Count - 1
                        strWHERE &= "ci.cistatuscd = :cistatuscd" + intCnt.ToString()
                        If intCnt <> .PropLstStateNM.SelectedItems.Count - 1 Then
                            strWHERE &= " or "
                        End If
                    Next
                    strWHERE &= ")"

                End If

                '番号検索

                If .PropTxtNum.Text.Trim <> "" Then

                    strWHERE &= " and "

                    strWHERE &= "ci.num = LPAD(:num, 5, '0') "
                End If

                ''導入番号検索(複数入力可)

                If .PropTxtIntroductNo.Text.Trim <> "" Then

                    ' 検索文字列の分割
                    strIntroductNo = CommonHBK.CommonLogicHBK.GetSearchStringList(dataHBKB0701.PropTxtIntroductNo.Text, SPLIT_MODE_OR)
                    strIntroductNo = CommonHBK.CommonLogicHBK.RemoveCharStringList(strIntroductNo)

                    If strIntroductNo.Length <> 0 Then

                        strWHERE &= " and "

                        strWHERE &= " ("
                        For intCnt = 0 To strIntroductNo.Count - 1
                            strWHERE &= "cs.introductnmb = :introductnmb" + intCnt.ToString()
                            If intCnt <> strIntroductNo.Count - 1 Then
                                strWHERE &= " or "
                            End If
                        Next
                        strWHERE &= ") "

                    End If
                End If

                'タイプ検索

                If .PropCmbTypeKbn.SelectedValue <> Nothing Then

                    strWHERE &= " and "

                    strWHERE &= "cs.typekbn = :typekbn "
                End If

                '機器利用形態検索

                If .PropCmbkikiUse.SelectedValue <> Nothing Then

                    strWHERE &= " and "

                    strWHERE &= "cs.kikiusecd = :kikiusecd "
                End If

                '製造番号検索(あいまい検索)

                If .PropTxtSerial.Text.Trim <> "" Then

                    strWHERE &= " and "

                    strWHERE &= "(cs.serialaimai like :serialaimai or cb.serialaimai like :serialaimai)"
                End If

                'イメージ番号検索

                If .PropTxtImageNmb.Text.Trim <> "" Then

                    strWHERE &= " and "

                    strWHERE &= "cs.imagenmb = :imagenmb"
                End If

                'オプションソフト検索

                If .PropCmbOptionSoft.Text <> "" Then

                    'strWHERE &= " and "

                    'strWHERE &= "EXISTS (SELECT 1 FROM optsoft_tb os WHERE os.cinmb = ci.cinmb AND h2z(os.softnm) LIKE '%' || h2z(:softnm) || '%') "

                    strWHERE &= " and "
                    strWHERE &= "EXISTS (SELECT 1 FROM optsoft_tb os WHERE os.cinmb = ci.cinmb AND os.softcd = :softcd)"

                End If

                'ユーザーID検索

                If .PropTxtUsrID.Text.Trim <> "" Then

                    strWHERE &= " and "

                    strWHERE &= "(cs.usridaimai = :usridaimai or cb.usridaimai = :usridaimai)"
                End If

                '管理部署検索(あいまい検索)

                If .PropTxtManageBusyoNM.Text.Trim <> "" Then

                    strWHERE &= " and "

                    strWHERE &= "(cs.managebusyonmaimai like :managebusyonmaimai or cb.managebusyonmaimai like :managebusyonmaimai)"
                End If

                '設置部署検索(あいまい検索)

                If .PropTxtSetBusyoNM.Text.Trim <> "" Then

                    strWHERE &= " and "

                    strWHERE &= "(cs.setbusyonmaimai like :setbusyonmaimai or cb.setbusyonmaimai like :setbusyonmaimai)"
                End If

                '設置建物検索(あいまい検索)

                If .PropTxtSetbuil.Text.Trim <> "" Then

                    strWHERE &= " and "

                    strWHERE &= "(cs.setbuilaimai like :setbuilaimai or cb.setbuilaimai like :setbuilaimai)"
                End If

                '設置フロア検索(あいまい検索)

                If .PropTxtSetFloor.Text.Trim <> "" Then

                    strWHERE &= " and "

                    strWHERE &= "(cs.setflooraimai like :setflooraimai or cb.setflooraimai like :setflooraimai)"
                End If

                '設置番組/部屋検索(あいまい検索)

                If .PropTxtSetRoom.Text.Trim <> "" Then

                    strWHERE &= " and "

                    strWHERE &= "(cs.setroomaimai like :setroomaimai or cb.setroomaimai like :setroomaimai)"
                End If

                'サービスセンター保管機検索

                If .PropCmbSCHokanKbn.SelectedValue <> Nothing Then

                    strWHERE &= " and "

                    strWHERE &= "cs.schokankbn = :schokankbn "
                End If

                'フリーテキスト検索(あいまい検索)

                If .PropTxtBIko.Text.Trim <> "" Then

                    ' 検索文字列の分割
                    strFreeText = CommonHBK.CommonLogicHBK.GetSearchStringList(dataHBKB0701.PropTxtBIko.Text, SPLIT_MODE_AND)

                    If strFreeText.Length <> 0 Then
                        strWHERE &= " and "

                        strWHERE &= " ("
                        For intCnt = 0 To strFreeText.Count - 1
                            strWHERE &= "ci.bikoaimai like :bikoaimai" + intCnt.ToString()
                            If intCnt <> strFreeText.Count - 1 Then
                                strWHERE &= " and "
                            End If
                        Next
                        strWHERE &= ") "
                    End If
                End If

                '[add] 2015/08/21 y.naganuma フリーワード追加対応 START

                'フリーワード検索(あいまい検索)

                If .PropTxtFreeWord.Text.Trim <> "" Then

                    ' 検索文字列の分割
                    strFreeWord = CommonHBK.CommonLogicHBK.GetSearchStringList(dataHBKB0701.PropTxtFreeWord.Text, SPLIT_MODE_AND)

                    If strFreeWord.Length <> 0 Then
                        strWHERE &= " and "

                        strWHERE &= " ("
                        For intCnt = 0 To strFreeWord.Count - 1
                            strWHERE &= "ci.FreeWordAimai like :FreeWordAimai" + intCnt.ToString()
                            If intCnt <> strFreeWord.Count - 1 Then
                                strWHERE &= " and "
                            End If
                        Next
                        strWHERE &= ") "
                    End If
                End If
                '[add] 2015/08/21 y.naganuma フリーワード追加対応 END

                'フリーフラグ１～５検索
                'フリーフラグ1

                If .PropCmbFreeFlg1.SelectedValue <> Nothing Then

                    strWHERE &= " and "

                    strWHERE &= "ci.freeflg1 = :freeflg1 "
                End If

                'フリーフラグ2

                If .PropCmbFreeFlg2.SelectedValue <> Nothing Then

                    strWHERE &= " and "

                    strWHERE &= "ci.freeflg2 = :freeflg2 "
                End If

                'フリーフラグ3

                If .PropCmbFreeFlg3.SelectedValue <> Nothing Then

                    strWHERE &= " and "

                    strWHERE &= "ci.freeflg3 = :freeflg3 "
                End If

                'フリーフラグ4

                If .PropCmbFreeFlg4.SelectedValue <> Nothing Then

                    strWHERE &= " and "

                    strWHERE &= "ci.freeflg4 = :freeflg4 "
                End If

                'フリーフラグ5

                If .PropCmbFreeFlg5.SelectedValue <> Nothing Then

                    strWHERE &= " and "

                    strWHERE &= "ci.freeflg5 = :freeflg5 "
                End If

                'ソートをかける一文を追加
                If intSearchFlg = SEARCH_MODE_SEARCH Then
                    '種別＋番号の昇順でソートを行う
                    strWHERE &= " order by km.sort asc, ci.Num asc"
                End If
                'WHERE句を結合

                strSql &= strWHERE

                'データアダプタに、SQLのSELECT文を設定()


                Adapter.SelectCommand = New NpgsqlCommand(strSql, Cn)


                '**********************************
                '* バインド変数に型と値をセット
                '**********************************
                'CI種別CD(サポセン機器)
                Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("cikbncdsuport", NpgsqlTypes.NpgsqlDbType.Varchar))
                Adapter.SelectCommand.Parameters("cikbncdsuport").Value = CommonDeclareHBK.CI_TYPE_SUPORT
                'CI種別CD(部所有機器)
                Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("cikbncdkiki", NpgsqlTypes.NpgsqlDbType.Varchar))
                Adapter.SelectCommand.Parameters("cikbncdkiki").Value = CommonDeclareHBK.CI_TYPE_KIKI
                '種別(複数選択可)
                For i As Integer = 0 To .PropLstKind.SelectedItems.Count - 1
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("kindcd" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("kindcd" + i.ToString).Value = .PropLstKind.SelectedItems(i)(LogicHBKB0701.LIST_COLMUN)
                Next
                'ステータス(複数選択可)
                For i As Integer = 0 To .PropLstStateNM.SelectedItems.Count - 1
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("cistatuscd" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("cistatuscd" + i.ToString).Value = .PropLstStateNM.SelectedItems(i)(LogicHBKB0701.LIST_COLMUN)
                Next
                '導入番号(複数選択可)
                If .PropTxtIntroductNo.Text.Trim <> "" Then
                    For i As Integer = 0 To strIntroductNo.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("introductnmb" + i.ToString, NpgsqlTypes.NpgsqlDbType.Integer))
                        Adapter.SelectCommand.Parameters("introductnmb" + i.ToString).Value = strIntroductNo(i)
                    Next
                End If

                '番号
                If .PropTxtNum.Text.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("num", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("num").Value = .PropTxtNum.Text
                End If
                'タイプ
                If .PropCmbTypeKbn.SelectedValue <> Nothing Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("typekbn", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("typekbn").Value = .PropCmbTypeKbn.SelectedValue
                End If
                '機器利用形態
                If .PropCmbkikiUse.SelectedValue <> Nothing Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("kikiusecd", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("kikiusecd").Value = .PropCmbkikiUse.SelectedValue
                End If
                '製造番号(あいまい)
                If .PropTxtSerial.Text.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("serialaimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("serialaimai").Value = "%" + commonLogicHBK.ChangeStringForSearch(.PropTxtSerial.Text) + "%"
                End If
                'イメージ番号
                If .PropTxtImageNmb.Text.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("imagenmb", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("imagenmb").Value = commonLogicHBK.ChangeStringForSearch(.PropTxtImageNmb.Text)
                End If
                'オプションソフト
                If .PropCmbOptionSoft.SelectedValue <> Nothing Then
                    'Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("softnm", NpgsqlTypes.NpgsqlDbType.Varchar))
                    'Adapter.SelectCommand.Parameters("softnm").Value = .PropCmbOptionSoft.Text.Trim

                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("softcd", NpgsqlTypes.NpgsqlDbType.Integer))
                    Adapter.SelectCommand.Parameters("softcd").Value = .PropCmbOptionSoft.SelectedValue
                End If
                'ユーザーID
                If .PropTxtUsrID.Text.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("usridaimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("usridaimai").Value = commonLogicHBK.ChangeStringForSearch(.PropTxtUsrID.Text)
                End If
                '管理部署(あいまい)
                If .PropTxtManageBusyoNM.Text.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("managebusyonmaimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("managebusyonmaimai").Value = "%" + commonLogicHBK.ChangeStringForSearch(.PropTxtManageBusyoNM.Text) + "%"
                End If
                '設置部署(あいまい)
                If .PropTxtSetBusyoNM.Text.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("setbusyonmaimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("setbusyonmaimai").Value = "%" + commonLogicHBK.ChangeStringForSearch(.PropTxtSetBusyoNM.Text) + "%"
                End If
                '設置建物(あいまい)
                If .PropTxtSetbuil.Text.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("setbuilaimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("setbuilaimai").Value = "%" + commonLogicHBK.ChangeStringForSearch(.PropTxtSetbuil.Text) + "%"
                End If
                '設置フロア(あいまい)
                If .PropTxtSetFloor.Text.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("setflooraimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("setflooraimai").Value = "%" + commonLogicHBK.ChangeStringForSearch(.PropTxtSetFloor.Text) + "%"
                End If
                '設置番組/部屋検索(あいまい)
                If .PropTxtSetRoom.Text.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("setroomaimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("setroomaimai").Value = "%" + commonLogicHBK.ChangeStringForSearch(.PropTxtSetRoom.Text) + "%"
                End If
                'サービスセンター保管機
                If .PropCmbSCHokanKbn.SelectedValue <> Nothing Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("schokankbn", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("schokankbn").Value = .PropCmbSCHokanKbn.SelectedValue
                End If
                'フリーフラグ1
                If .PropCmbFreeFlg1.SelectedValue <> Nothing Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("freeflg1", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("freeflg1").Value = .PropCmbFreeFlg1.SelectedValue
                End If
                'フリーフラグ2
                If .PropCmbFreeFlg2.SelectedValue <> Nothing Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("freeflg2", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("freeflg2").Value = .PropCmbFreeFlg2.SelectedValue
                End If
                'フリーフラグ3
                If .PropCmbFreeFlg3.SelectedValue <> Nothing Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("freeflg3", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("freeflg3").Value = .PropCmbFreeFlg3.SelectedValue
                End If
                'フリーフラグ4
                If .PropCmbFreeFlg4.SelectedValue <> Nothing Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("freeflg4", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("freeflg4").Value = .PropCmbFreeFlg4.SelectedValue
                End If
                'フリーフラグ5
                If .PropCmbFreeFlg5.SelectedValue <> Nothing Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("freeflg5", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("freeflg5").Value = .PropCmbFreeFlg5.SelectedValue
                End If
                'フリーテキスト用のバインド変数設定
                If .PropTxtBIko.Text <> "" Then
                    '検索文字列をあいまい検索用に変換
                    For i As Integer = 0 To strFreeText.Count - 1
                        strFreeText(i) = commonLogicHBK.ChangeStringForSearch(strFreeText(i))
                    Next
                    'バインド変数を設定
                    For i As Integer = 0 To strFreeText.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("bikoaimai" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("bikoaimai" + i.ToString).Value = "%" + strFreeText(i) + "%"
                    Next
                End If
                '[add] 2015/08/21 y.naganuma フリーワード追加対応 START
                'フリーワード用のバインド変数設定
                If .PropTxtFreeWord.Text <> "" Then
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
    ''' 導入一覧検索結果取得用SQLWHERE句の作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0701">[IN]機器一括検索一覧画面データクラス</param>
    ''' <param name="strSql">[IN]基本SQL</param>
    ''' <param name="intSearchFlg">[IN]SQL判別フラグ</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>導入一覧検索結果取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/27 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectWhereSearchIntroductSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0701 As DataHBKB0701, _
                                          ByRef strSql As String, _
                                          ByVal intSearchFlg As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        'Dim strSQL As String = ""
        Dim strWHERE As String = ""    'WHERE句のSQLを格納する場所
        Dim intCnt As Integer 'カウント変数

        Try
            '**********************************
            '* SQL文設定
            '**********************************

            'SQL文(SELECT)
            'strSQL = setSelectGetIntroductsql

            '検索項目に設定されている情報をSQL文に追記する処理
            '種別検索

            With dataHBKB0701
                If .PropLstKind.SelectedValue <> Nothing Then

                    If strWHERE = "" Then
                        strWHERE = " where "
                    Else
                        strWHERE &= " and "
                    End If
                    strWHERE &= "("
                    For intCnt = 0 To dataHBKB0701.PropLstKind.SelectedItems.Count - 1
                        strWHERE &= "it.kindcd = :kindcd" + intCnt.ToString()
                        If intCnt <> dataHBKB0701.PropLstKind.SelectedItems.Count - 1 Then
                            strWHERE &= " or "
                        End If
                    Next
                    strWHERE &= ")"

                End If

                '導入番号昇順でソート
                If intSearchFlg = SEARCH_MODE_SEARCH Then
                    strWHERE &= " order by it.introductnmb"
                End If

                'WHERE句を結合
                strSql &= strWHERE

                'データアダプタに、SQLのSELECT文を設定()


                Adapter.SelectCommand = New NpgsqlCommand(strSql, Cn)


                '**********************************
                '* バインド変数に型と値をセット
                '**********************************
                '種別(複数選択可)
                For i As Integer = 0 To .PropLstKind.SelectedItems.Count - 1
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("kindcd" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("kindcd" + i.ToString).Value = dataHBKB0701.PropLstKind.SelectedItems(i)(LogicHBKB0701.LIST_COLMUN)
                Next
                '導入タイプ(リース)
                Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("introductkbnlease", NpgsqlTypes.NpgsqlDbType.Varchar))
                Adapter.SelectCommand.Parameters("introductkbnlease").Value = INTRODUCT_KBN_LEASE
                '導入タイプ(買取)
                Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("introductkbnbuy", NpgsqlTypes.NpgsqlDbType.Varchar))
                Adapter.SelectCommand.Parameters("introductkbnbuy").Value = INTRODUCT_KBN_KEIHI


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
    ''' 履歴検索結果取得用SQLWHERE句の作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0701">[IN]機器一括検索一覧画面データクラス</param>
    ''' <param name="strSql">[IN]基本SQL</param>
    ''' <param name="intSearchFlg">[IN]SQL判別フラグ</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>履歴検索結果取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/25 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectWhereSearchRirekiSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0701 As DataHBKB0701, _
                                          ByRef strSql As String, _
                                          ByVal intSearchFlg As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strWHERE As String = ""              'WHERE句のSQLを格納する場所
        Dim intCnt As Integer                    'カウント変数
        Dim strFreeText() As String = Nothing    'フリーテキスト検索用配列
        Dim strIntroductNo() As String = Nothing '導入番号検索用配列
        '[add] 2015/08/21 y.naganuma フリーワード追加対応 START
        Dim strFreeWord() As String = Nothing       'フリーワード検索用配列
        '[add] 2015/08/21 y.naganuma フリーワード追加対応 END
        Try
            '**********************************
            '* SQL文設定
            '**********************************

            'SQL文(SELECT)


            '検索項目に設定されている情報をSQL文に追記する処理

            '種別検索(複数選択可)

            With dataHBKB0701

                If .PropLstKind.SelectedValue <> Nothing Then


                    strWHERE &= " and "

                    strWHERE &= "("
                    For intCnt = 0 To .PropLstKind.SelectedItems.Count - 1
                        strWHERE &= "rt.kindcd = :kindcd" + intCnt.ToString()
                        If intCnt <> .PropLstKind.SelectedItems.Count - 1 Then
                            strWHERE &= " or "
                        End If
                    Next
                    strWHERE &= ")"

                End If


                '番号検索

                If .PropTxtNum.Text.Trim <> "" Then

                    strWHERE &= " and "

                    strWHERE &= "rt.num = LPAD(:Num, 5, '0') "

                End If

                '導入番号検索(複数入力可)

                If dataHBKB0701.PropTxtIntroductNo.Text.Trim <> "" Then

                    ' 検索文字列の分割
                    strIntroductNo = CommonHBK.CommonLogicHBK.GetSearchStringList(dataHBKB0701.PropTxtIntroductNo.Text, SPLIT_MODE_OR)
                    strIntroductNo = CommonHBK.CommonLogicHBK.RemoveCharStringList(strIntroductNo)

                    If strIntroductNo.Length <> 0 Then
                        strWHERE &= " and "

                        strWHERE &= " ("
                        For intCnt = 0 To strIntroductNo.Count - 1
                            strWHERE &= "sr.introductnmb = :introductnmb" + intCnt.ToString()
                            If intCnt <> strIntroductNo.Count - 1 Then
                                strWHERE &= " or "
                            End If
                        Next
                        strWHERE &= ") "
                    End If
                End If


                'タイプ検索

                If .PropCmbTypeKbn.SelectedValue <> Nothing Then

                    strWHERE &= " and "

                    strWHERE &= "sr.typekbn = :typekbn "
                End If
                '機器利用形態検索

                If .PropCmbkikiUse.SelectedValue <> Nothing Then

                    strWHERE &= " and "

                    strWHERE &= "sr.kikiusecd = :kikiusecd "
                End If



                '製造番号検索(あいまい検索)

                If .PropTxtSerial.Text.Trim <> "" Then


                    strWHERE &= " and "

                    strWHERE &= "(sr.serialaimai like :serialaimai or br.serialaimai like :serialaimai)"

                End If
                'イメージ番号検索

                If .PropTxtImageNmb.Text.Trim <> "" Then

                    strWHERE &= " and "

                    strWHERE &= "sr.imagenmb = :imagenmb"
                End If


                'オプションソフト検索

                If .PropCmbOptionSoft.Text.Trim <> "" Then

                    'strWHERE &= " and "

                    'strWHERE &= "EXISTS (SELECT 1 FROM optsoft_rtb os WHERE os.cinmb = rt.cinmb AND os.rirekino = rt.rirekino AND h2z(os.softnm) LIKE '%' || h2z(:softnm) || '%')"
                    strWHERE &= " and "
                    strWHERE &= "EXISTS (SELECT 1 FROM optsoft_rtb os WHERE os.cinmb = rt.cinmb AND os.softcd = :softcd AND os.rirekino = rt.rirekino)"
                End If

                'ユーザーID検索

                If .PropTxtUsrID.Text.Trim <> "" Then

                    strWHERE &= " and "

                    strWHERE &= "(sr.usridaimai = :usridaimai or br.usridaimai = :usridaimai)"
                End If

                '管理部署検索(あいまい検索)

                If .PropTxtManageBusyoNM.Text.Trim <> "" Then

                    strWHERE &= " and "

                    strWHERE &= "(sr.managebusyonmaimai like :managebusyonmaimai or br.managebusyonmaimai like :managebusyonmaimai)"
                End If

                '設置部署検索(あいまい検索)

                If .PropTxtSetBusyoNM.Text.Trim <> "" Then

                    strWHERE &= " and "

                    strWHERE &= "(sr.setbusyonmaimai like :setbusyonmaimai or br.setbusyonmaimai like :setbusyonmaimai)"
                End If

                '設置建物検索(あいまい検索)

                If .PropTxtSetbuil.Text.Trim <> "" Then

                    strWHERE &= " and "

                    strWHERE &= "(sr.setbuilaimai like :setbuilaimai or br.setbuilaimai like :setbuilaimai)"
                End If

                '設置フロア検索(あいまい検索)

                If .PropTxtSetFloor.Text.Trim <> "" Then

                    strWHERE &= " and "
                    'End If
                    strWHERE &= "(sr.setflooraimai like :setflooraimai or br.setflooraimai like :setflooraimai)"
                End If

                '設置番組/部屋検索(あいまい検索)

                If .PropTxtSetRoom.Text.Trim <> "" Then

                    strWHERE &= " and "

                    strWHERE &= "(sr.setroomaimai like :setroomaimai or br.setroomaimai like :setroomaimai)"
                End If

                'サービスセンター保管機検索

                If .PropCmbSCHokanKbn.SelectedValue <> Nothing Then

                    strWHERE &= " and "

                    strWHERE &= "sr.schokankbn = :schokankbn "
                End If

                'フリーテキスト検索(あいまい検索)

                If .PropTxtBIko.Text.Trim <> "" Then

                    ' 検索文字列の分割
                    strFreeText = CommonHBK.CommonLogicHBK.GetSearchStringList(dataHBKB0701.PropTxtBIko.Text, SPLIT_MODE_AND)

                    If strFreeText.Length <> 0 Then

                        strWHERE &= " and "

                        strWHERE &= " ("
                        For intCnt = 0 To strFreeText.Count - 1
                            strWHERE &= "rt.bikoaimai like :bikoaimai" + intCnt.ToString()
                            If intCnt <> strFreeText.Count - 1 Then
                                strWHERE &= " and "
                            End If
                        Next
                        strWHERE &= ") "
                    End If
                End If

                '[add] 2015/08/21 y.naganuma フリーワード追加対応 START

                'フリーワード検索(あいまい検索)

                If .PropTxtFreeWord.Text.Trim <> "" Then

                    ' 検索文字列の分割
                    strFreeWord = CommonHBK.CommonLogicHBK.GetSearchStringList(dataHBKB0701.PropTxtFreeWord.Text, SPLIT_MODE_AND)

                    If strFreeWord.Length <> 0 Then
                        strWHERE &= " and "

                        strWHERE &= " ("
                        For intCnt = 0 To strFreeWord.Count - 1
                            strWHERE &= "rt.FreeWordAimai like :FreeWordAimai" + intCnt.ToString()
                            If intCnt <> strFreeWord.Count - 1 Then
                                strWHERE &= " and "
                            End If
                        Next
                        strWHERE &= ") "
                    End If
                End If
                '[add] 2015/08/21 y.naganuma フリーワード追加対応 END


                'フリーフラグ１～５検索
                'フリーフラグ1

                If .PropCmbFreeFlg1.SelectedValue <> Nothing Then

                    strWHERE &= " and "

                    strWHERE &= "rt.freeflg1 = :freeflg1 "
                End If

                'フリーフラグ2

                If .PropCmbFreeFlg2.SelectedValue <> Nothing Then

                    strWHERE &= " and "

                    strWHERE &= "rt.freeflg2 = :freeflg2 "
                End If

                'フリーフラグ3

                If .PropCmbFreeFlg3.SelectedValue <> Nothing Then

                    strWHERE &= " and "

                    strWHERE &= "rt.freeflg3 = :freeflg3 "
                End If

                'フリーフラグ4

                If .PropCmbFreeFlg4.SelectedValue <> Nothing Then

                    strWHERE &= " and "

                    strWHERE &= "rt.freeflg4 = :freeflg4 "
                End If

                'フリーフラグ5

                If .PropCmbFreeFlg5.SelectedValue <> Nothing Then

                    strWHERE &= " and "

                    strWHERE &= "rt.freeflg5 = :freeflg5 "
                End If

                'ステータス検索(複数選択可)

                If .PropLstStateNM.SelectedValue <> Nothing Then


                    strWHERE &= " and "

                    strWHERE &= "("
                    For intCnt = 0 To dataHBKB0701.PropLstStateNM.SelectedItems.Count - 1
                        strWHERE &= "rt.cistatuscd = :cistatuscd" + intCnt.ToString()
                        If intCnt <> dataHBKB0701.PropLstStateNM.SelectedItems.Count - 1 Then
                            strWHERE &= " or "
                        End If
                    Next
                    strWHERE &= ")"

                End If

                '作業日検索
                '作業日(FROM)

                If .PropDtpDayfrom.txtDate.Text.Trim <> "" Then

                    strWHERE &= " and "

                    strWHERE &= "to_char(rrt.regdt,'yyyy/mm/dd') >= :regdtfrom"
                End If

                '作業日(TO)

                If .PropDtpDayto.txtDate.Text.Trim <> "" Then

                    strWHERE &= " and "

                    strWHERE &= "to_char(rrt.regdt,'yyyy/mm/dd') <= :regdtto"
                End If


                '作業検索(複数選択可)

                If .PropLstWorkNM.SelectedValue <> Nothing Then


                    strWHERE &= " and "

                    strWHERE &= "("
                    For intCnt = 0 To dataHBKB0701.PropLstWorkNM.SelectedItems.Count - 1
                        strWHERE &= "rrt.workcd = :workcd" + intCnt.ToString()
                        If intCnt <> dataHBKB0701.PropLstWorkNM.SelectedItems.Count - 1 Then
                            strWHERE &= " or "
                        End If
                    Next
                    strWHERE &= ")"

                End If


                '完了検索

                If .PropCmbWorkKbnNM.SelectedValue <> Nothing Then

                    strWHERE &= " and "

                    strWHERE &= "rrt.workkbncd = :workkbncd "
                End If

                If intSearchFlg = SEARCH_MODE_SEARCH Then
                    '作業日時の降順でソートを行う
                    '[mod] 2015/10/05 e.okamura フリーワード追加対応 START
                    'strWHERE &= " order by rrt.regdt desc "
                    strWHERE &= " order by rrt.regdt desc, rt.cinmb, rt.rirekino desc "
                    '[mod] 2015/10/05 e.okamura フリーワード追加対応 END
                End If

                'WHERE句を結合

                strSql &= strWHERE

                'データアダプタに、SQLのSELECT文を設定

                Adapter.SelectCommand = New NpgsqlCommand(strSql, Cn)


                '**********************************
                '* バインド変数に型と値をセット
                '**********************************


                'CI種別CD(サポセン機器)
                Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("cikbncdsuport", NpgsqlTypes.NpgsqlDbType.Varchar))
                Adapter.SelectCommand.Parameters("cikbncdsuport").Value = CommonDeclareHBK.CI_TYPE_SUPORT
                'CI種別CD(部所有機器)
                Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("cikbncdkiki", NpgsqlTypes.NpgsqlDbType.Varchar))
                Adapter.SelectCommand.Parameters("cikbncdkiki").Value = CommonDeclareHBK.CI_TYPE_KIKI
                '種別(複数選択可)
                For i As Integer = 0 To .PropLstKind.SelectedItems.Count - 1
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("kindcd" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("kindcd" + i.ToString).Value = .PropLstKind.SelectedItems(i)(LogicHBKB0701.LIST_COLMUN)
                Next
                'ステータス(複数選択可)
                For i As Integer = 0 To .PropLstStateNM.SelectedItems.Count - 1
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("cistatuscd" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("cistatuscd" + i.ToString).Value = .PropLstStateNM.SelectedItems(i)(LogicHBKB0701.LIST_COLMUN)
                Next
                '作業(複数選択可)
                For i As Integer = 0 To .PropLstWorkNM.SelectedItems.Count - 1
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("workcd" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("workcd" + i.ToString).Value = .PropLstWorkNM.SelectedItems(i)(LogicHBKB0701.LIST_COLMUN)
                Next
                '導入番号(複数選択可)
                If .PropTxtIntroductNo.Text.Trim <> "" Then
                    For i As Integer = 0 To strIntroductNo.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("introductnmb" + i.ToString, NpgsqlTypes.NpgsqlDbType.Integer))
                        Adapter.SelectCommand.Parameters("introductnmb" + i.ToString).Value = strIntroductNo(i)
                    Next
                End If
                '番号
                If .PropTxtNum.Text.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("num", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("num").Value = .PropTxtNum.Text
                End If
                'タイプ
                If .PropCmbTypeKbn.SelectedValue <> Nothing Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("typekbn", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("typekbn").Value = .PropCmbTypeKbn.SelectedValue
                End If
                '製造番号(あいまい)
                If .PropTxtSerial.Text.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("serialaimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("serialaimai").Value = "%" + commonLogicHBK.ChangeStringForSearch(.PropTxtSerial.Text) + "%"
                End If
                'イメージ番号
                If .PropTxtImageNmb.Text.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("imagenmb", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("imagenmb").Value = commonLogicHBK.ChangeStringForSearch(.PropTxtImageNmb.Text)
                End If
                '機器利用形態
                If .PropCmbkikiUse.SelectedValue <> Nothing Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("kikiusecd", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("kikiusecd").Value = .PropCmbkikiUse.SelectedValue
                End If
                'オプションソフト
                If .PropCmbOptionSoft.SelectedValue <> Nothing Then
                    'Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("softnm", NpgsqlTypes.NpgsqlDbType.Varchar))
                    'Adapter.SelectCommand.Parameters("softnm").Value = .PropCmbOptionSoft.Text.Trim
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("softCD", NpgsqlTypes.NpgsqlDbType.Integer))
                    Adapter.SelectCommand.Parameters("softCD").Value = .PropCmbOptionSoft.SelectedValue
                End If
                'ユーザーID
                If .PropTxtUsrID.Text.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("usridaimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("usridaimai").Value = commonLogicHBK.ChangeStringForSearch(.PropTxtUsrID.Text)
                End If
                '管理部署(あいまい)
                If .PropTxtManageBusyoNM.Text.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("managebusyonmaimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("managebusyonmaimai").Value = "%" + commonLogicHBK.ChangeStringForSearch(.PropTxtManageBusyoNM.Text) + "%"
                End If
                '設置部署(あいまい)
                If .PropTxtSetBusyoNM.Text.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("setbusyonmaimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("setbusyonmaimai").Value = "%" + commonLogicHBK.ChangeStringForSearch(.PropTxtSetBusyoNM.Text) + "%"
                End If
                '設置建物(あいまい)
                If .PropTxtSetbuil.Text.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("setbuilaimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("setbuilaimai").Value = "%" + commonLogicHBK.ChangeStringForSearch(.PropTxtSetbuil.Text) + "%"
                End If
                '設置フロア(あいまい)
                If .PropTxtSetFloor.Text.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("setflooraimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("setflooraimai").Value = "%" + commonLogicHBK.ChangeStringForSearch(.PropTxtSetFloor.Text) + "%"
                End If
                '設置番組/部屋検索(あいまい)
                If .PropTxtSetRoom.Text.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("setroomaimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("setroomaimai").Value = "%" + commonLogicHBK.ChangeStringForSearch(.PropTxtSetRoom.Text) + "%"
                End If
                'サービスセンター保管機
                If .PropCmbSCHokanKbn.SelectedValue <> Nothing Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("schokankbn", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("schokankbn").Value = .PropCmbSCHokanKbn.SelectedValue
                End If
                'フリーフラグ1
                If .PropCmbFreeFlg1.SelectedValue <> Nothing Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("freeflg1", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("freeflg1").Value = .PropCmbFreeFlg1.SelectedValue
                End If
                'フリーフラグ2
                If .PropCmbFreeFlg2.SelectedValue <> Nothing Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("freeflg2", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("freeflg2").Value = .PropCmbFreeFlg2.SelectedValue
                End If
                'フリーフラグ3
                If .PropCmbFreeFlg3.SelectedValue <> Nothing Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("freeflg3", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("freeflg3").Value = .PropCmbFreeFlg3.SelectedValue
                End If
                'フリーフラグ4
                If .PropCmbFreeFlg4.SelectedValue <> Nothing Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("freeflg4", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("freeflg4").Value = .PropCmbFreeFlg4.SelectedValue
                End If
                'フリーフラグ5
                If .PropCmbFreeFlg5.SelectedValue <> Nothing Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("freeflg5", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("freeflg5").Value = .PropCmbFreeFlg5.SelectedValue
                End If
                '完了
                If .PropCmbWorkKbnNM.SelectedValue <> Nothing Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("workkbncd", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("workkbncd").Value = .PropCmbWorkKbnNM.SelectedValue
                End If
                '作業日(FROM)
                If .PropDtpDayfrom.txtDate.Text <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("regdtfrom", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("regdtfrom").Value = .PropDtpDayfrom.txtDate.Text
                End If

                '作業日(TO)
                If .PropDtpDayto.txtDate.Text <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("regdtto", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("regdtto").Value = .PropDtpDayto.txtDate.Text
                End If

                'フリーテキスト用のバインド変数設定
                If dataHBKB0701.PropTxtBIko.Text.Trim <> "" Then
                    '検索文字列をあいまい検索用に変換
                    For i As Integer = 0 To strFreeText.Count - 1
                        strFreeText(i) = commonLogicHBK.ChangeStringForSearch(strFreeText(i))
                    Next
                    'バインド変数を設定
                    For i As Integer = 0 To strFreeText.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("bikoaimai" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("bikoaimai" + i.ToString).Value = "%" + strFreeText(i) + "%"
                    Next
                End If

                '[add] 2015/08/21 y.naganuma フリーワード追加対応 START
                'フリーワード用のバインド変数設定
                If dataHBKB0701.PropTxtFreeWord.Text <> "" Then
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


End Class
