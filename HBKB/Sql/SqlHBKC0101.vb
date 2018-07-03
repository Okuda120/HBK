Imports Npgsql
Imports Common
Imports CommonHBK
Imports System.Text

''' <summary>
''' インシデント検索一覧画面Sqlクラス
''' </summary>
''' <remarks>インシデント検索一覧画面のSQLの作成・設定を行う
''' <para>作成情報：2012/07/25 s.yamaguchi
''' <p>改訂情報:2012/07/25</p>
''' </para></remarks>
Public Class SqlHBKC0101

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK

    '定数宣言
    Private Const SEARCH_MODE_COUNT As String = "0" 'カウント
    Private Const SEARCH_MODE_SEARCH As String = "1" '検索

    '[ADD] 2012/10/24 s.yamaguchi START
    '[SELECT]受付手段マスター
    Private strSelectUketsukeMstSql As String = "SELECT" & vbCrLf & _
                                                " um.UketsukeWayCD," & vbCrLf & _
                                                " um.UketsukeWayNM" & vbCrLf & _
                                                " FROM uketsukeway_mtb AS um" & vbCrLf & _
                                                " ORDER BY um.Sort "
    '[ADD] 2012/10/24 s.yamaguchi END

    '[SELECT]インシデント種別マスター取得SQL
    Private strSelectIncidentKind As String = "SELECT" & vbCrLf & _
                                              " ikm.IncKindCD," & vbCrLf & _
                                              " ikm.IncKindNM" & vbCrLf & _
                                              " FROM incident_kind_mtb AS ikm" & vbCrLf & _
                                              " ORDER BY ikm.Sort"

    '[SELECT]ドメインマスター取得SQL
    Private strSelectDomain As String = "SELECT" & vbCrLf & _
                                        " dm.DomainCD," & vbCrLf & _
                                        " dm.DomainNM" & vbCrLf & _
                                        " FROM domain_mtb AS dm" & vbCrLf & _
                                        " ORDER BY dm.Sort"

    '[SELECT]グループマスター取得SQL
    Private strSelectGrp As String = "SELECT" & vbCrLf & _
                                     " gm.GroupCD," & vbCrLf & _
                                     " gm.GroupNM" & vbCrLf & _
                                     " FROM grp_mtb AS gm" & vbCrLf & _
                                     " ORDER BY gm.Sort"

    '[SELECT]種別マスター取得SQL
    Private strSelectKind As String = "SELECT" & vbCrLf & _
                                      " km.KindCD," & vbCrLf & _
                                      " km.KindNM" & vbCrLf & _
                                      " FROM kind_mtb AS km" & vbCrLf & _
                                      " WHERE km.cikbncd IN (:cikbncd1,:cikbncd2) " & vbCrLf & _
                                      " ORDER BY km.Sort"

    '[SELECT]プロセスステータスマスター取得SQL
    Private strSelectProcessState As String = "SELECT" & vbCrLf & _
                                              " psm.ProcessStateCD," & vbCrLf & _
                                              " psm.ProcessStateNM," & vbCrLf & _
                                              " psm.DefaultSelectFlg" & vbCrLf & _
                                              " FROM processstate_mtb AS psm" & vbCrLf & _
                                              " WHERE psm.ProcessKbn = :ProcessKbn" & vbCrLf & _
                                              " ORDER BY psm.Sort"

    '[SELECT]対象システム取得SQL
    Private strSelectTargetSystem As String = "SELECT" & vbCrLf & _
                                              " cit.CINmb," & vbCrLf & _
                                              " cit.CINM  || ' ' || cit.Class1 || ' ' || cit.Class2 AS SystemNM" & vbCrLf & _
                                              "FROM (" & vbCrLf & _
                                              "SELECT cinmb,kindcd,class1,class2,cinm,'1' as sort0,sort " & vbCrLf & _
                                              "FROM  ci_info_tb " & vbCrLf & _
                                              "WHERE cistatuscd <> :CIStatusCd AND cikbncd= :CIKbnCD" & vbCrLf & _
                                              "UNION " & vbCrLf & _
                                              "SELECT cinmb,kindcd,class1,class2,cinm,'2' as sort0,sort " & vbCrLf & _
                                              "FROM  ci_info_tb " & vbCrLf & _
                                              "WHERE cistatuscd = :CIStatusCd  AND cikbncd= :CIKbnCD" & vbCrLf & _
                                              ") AS cit " & vbCrLf & _
                                              "ORDER BY Sort0,Sort "

    '[SELECT]インシデント検索件数
    Private strSelectCountSql As String = "SELECT" & vbCrLf & _
                                          " COUNT(*)" & vbCrLf & _
                                          " FROM incident_info_tb AS iit" & vbCrLf & _
                                          " LEFT OUTER JOIN incident_kind_mtb AS ikm ON iit.IncKbnCD = ikm.IncKindCD" & vbCrLf & _
                                          " LEFT OUTER JOIN processstate_mtb pm ON iit.ProcessStateCD = pm.ProcessStateCD" & vbCrLf & _
                                          " LEFT OUTER JOIN ci_info_tb AS cit ON iit.SystemNmb = cit.CINmb" & vbCrLf & _
                                          " LEFT OUTER JOIN grp_mtb AS gm ON iit.TantoGrpCD = gm.GroupCD" & vbCrLf & _
                                          " LEFT OUTER JOIN hbkusr_mtb AS hm ON iit.IncTantoID = hm.HBKUsrID" & vbCrLf & _
                                          " LEFT OUTER JOIN domain_mtb AS dm ON iit.DomainCD = dm.DomainCD" & vbCrLf



    '[SELECT]インシデント検索結果
    Private strSelectIncidentInfoSql As String = "SELECT" & vbCrLf & _
                                                 " iit.IncNmb," & vbCrLf & _
                                                 " ikm.IncKindNM," & vbCrLf & _
                                                 " pm.ProcessStateNM," & vbCrLf & _
                                                 " iit.HasseiDT," & vbCrLf & _
                                                 " iit.Title," & vbCrLf & _
                                                 " cit.CINM," & vbCrLf & _
                                                 " gm.GroupNM," & vbCrLf & _
                                                 " hm.HBKUsrNM," & vbCrLf & _
                                                 " dm.DomainNM," & vbCrLf & _
                                                 " iit.PartnerNM," & vbCrLf & _
                                                 " iit.UsrBusyoNM," & vbCrLf & _
                                                 " iit.UsrBusyoNM," & vbCrLf & _
                                                 " (CASE WHEN HBKF0010(iit.IncNmb, '" & PROCESS_TYPE_INCIDENT & "') = '9999/12/31 59:59' OR HBKF0010(iit.IncNmb, '" & PROCESS_TYPE_INCIDENT & "') = '0000/00/00 00:00' THEN NULL" & vbCrLf & _
                                                 "       ELSE HBKF0010(iit.IncNmb, '" & PROCESS_TYPE_INCIDENT & "')" & vbCrLf & _
                                                 "       END) AS WorkSceDT," & vbCrLf & _
                                                 " iit.ProcessStateCD," & vbCrLf & _
                                                 " iit.IncTantoID," & vbCrLf & _
                                                 " iit.TantoGrpCD," & vbCrLf & _
                                                 " HBKF0010(iit.IncNmb, '" & PROCESS_TYPE_INCIDENT & "') AS SortDT," & vbCrLf & _
                                                 " row_number() over(ORDER BY iit.hasseidt DESC,iit.IncNmb) AS SortNo" & vbCrLf & _
                                                 " FROM incident_info_tb AS iit" & vbCrLf & _
                                                 " LEFT OUTER JOIN incident_kind_mtb AS ikm ON iit.IncKbnCD = ikm.IncKindCD" & vbCrLf & _
                                                 " LEFT OUTER JOIN processstate_mtb AS pm ON iit.ProcessStateCD = pm.ProcessStateCD" & vbCrLf & _
                                                 " LEFT OUTER JOIN ci_info_tb AS cit ON iit.SystemNmb = cit.CINmb" & vbCrLf & _
                                                 " LEFT OUTER JOIN grp_mtb AS gm ON iit.TantoGrpCD = gm.GroupCD" & vbCrLf & _
                                                 " LEFT OUTER JOIN hbkusr_mtb AS hm ON iit.IncTantoID = hm.HBKUsrID" & vbCrLf & _
                                                 " LEFT OUTER JOIN domain_mtb AS dm ON iit.DomainCD = dm.DomainCD"


    '[SELECT]インシデント機器情報取得SQL
    Private strSelectIncidentKikiSql As String = "SELECT" & vbCrLf & _
                                                 " ikit.IncNmb" & vbCrLf & _
                                                 " FROM incident_kiki_tb AS ikit" & vbCrLf

    '[SELECT]プロセス区分取得SQL
    Private strSelectProccesLinkSql As String = "SELECT COALESCE(HBKF0011(:ProccesLinkKind,:ProcessLinkNum,'" & PROCESS_TYPE_INCIDENT & "'),'0')"

    '[SELECT]エンドユーザマスタ(相手IDEnter取得用)
    Private strSelectPartnerMstSql As String = "SELECT " & vbCrLf & _
                                               " endusrnm " & vbCrLf & _
                                               ",endusrnmkana " & vbCrLf & _
                                               ",endusrbusyonm " & vbCrLf & _
                                               "FROM  endusr_mtb " & vbCrLf & _
                                               "WHERE endusrid = :endusrid " & vbCrLf

    '[SELECT]ユーザマスタ(担当IDEnter取得用)
    Private strSelectTantoMstSql As String = "SELECT " & vbCrLf & _
                                             " hbkusrnm " & vbCrLf & _
                                             ",hbkusrnmkana " & vbCrLf & _
                                             ",ts.groupcd " & vbCrLf & _
                                             "FROM  hbkusr_mtb tu" & vbCrLf & _
                                             "LEFT JOIN szk_mtb ts ON tu.hbkusrid=ts.hbkusrid  " & vbCrLf & _
                                             "INNER JOIN grp_mtb tg ON tg.groupcd=ts.groupcd  " & vbCrLf & _
                                             "WHERE tu.hbkusrid = :hbkusrid " & vbCrLf

    ''' <summary>
    ''' プロセスリンク取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="StrProccesLinkKind">プロセス区分種別</param>
    ''' <param name="StrProcessLinkNum">プロセス区分番号</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>プロセスリンク取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetProccesLinkSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal StrProccesLinkKind As String, ByVal StrProcessLinkNum As String) As Boolean


        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""
        Dim intNum As Integer

        Try
            '**********************************
            '* SQL文設定
            '**********************************
            'SQL文(SELECT)
            strSQL = strSelectProccesLinkSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("ProccesLinkKind", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("ProcessLinkNum", NpgsqlTypes.NpgsqlDbType.Integer))
            Adapter.SelectCommand.Parameters("ProccesLinkKind").Value = StrProccesLinkKind
            If StrProcessLinkNum <> "" Then
                If Integer.TryParse(StrProcessLinkNum, intNum) = True Then
                    '数値型の場合
                    Adapter.SelectCommand.Parameters("ProcessLinkNum").Value = Integer.Parse(StrProcessLinkNum)
                Else
                    ''数値型ではない場合
                    Adapter.SelectCommand.Parameters("ProcessLinkNum").Value = 0
                End If
            Else
                '未入力の場合
                Adapter.SelectCommand.Parameters("ProcessLinkNum").Value = DBNull.Value
            End If


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

    '[ADD] 2012/10/24 s.yamaguchi START
    ''' <summary>
    ''' 受付手段マスター取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0101">[IN]インシデント検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>受付手段マスター取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/10/24 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectUketsukeWaySql(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            '**********************************
            '* SQL文設定
            '**********************************
            'SQL文(SELECT)
            strSQL = strSelectUketsukeMstSql

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
    '[ADD] 2012/10/24 s.yamaguchi END

    ''' <summary>
    ''' インシデント種別マスター取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0101">[IN]インシデント検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>インシデント種別マスター取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/25 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectIncidentKindSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            '**********************************
            '* SQL文設定
            '**********************************
            'SQL文(SELECT)
            strSQL = strSelectIncidentKind

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
    ''' ドメインマスター取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0101">[IN]インシデント検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ドメインマスター取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/25 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectDomainSql(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            '**********************************
            '* SQL文設定
            '**********************************
            'SQL文(SELECT)
            strSQL = strSelectDomain

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
    ''' グループマスター取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0101">[IN]インシデント検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>グループマスター取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/25 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectGrpSql(ByRef Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            '**********************************
            '* SQL文設定
            '**********************************
            'SQL文(SELECT)
            strSQL = strSelectGrp

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
    ''' 種別マスター取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0101">[IN]インシデント検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>種別マスター取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/25 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectKindSql(ByRef Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByVal dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            '**********************************
            '* SQL文設定
            '**********************************
            'SQL文(SELECT)
            strSQL = strSelectKind

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            '種別CD(サポセン機器=003)
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("cikbncd1", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("cikbncd1").Value = CI_TYPE_SUPORT
            '種別CD(部所有機器=004)
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("cikbncd2", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("cikbncd2").Value = CI_TYPE_KIKI

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
    ''' プロセスステータスマスター取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0101">[IN]インシデント検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>プロセスステータスマスター取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/25 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectProcessStateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            '**********************************
            '* SQL文設定
            '**********************************
            'SQL文(SELECT)
            strSQL = strSelectProcessState

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            'プロセス区分(インシデント=001)
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("ProcessKbn").Value = PROCESS_TYPE_INCIDENT

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
    ''' 対象システム取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0101">[IN]インシデント検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>対象システム取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/25 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectTargetSystemSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            '**********************************
            '* SQL文設定
            '**********************************
            'SQL文(SELECT)
            strSQL = strSelectTargetSystem

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            'CI種別CD(システム=001)
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("CIKbnCD").Value = CI_TYPE_SYSTEM
            'CIステータスCD(廃棄済=103)
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CIStatusCd", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("CIStatusCd").Value = CI_STATUS_SYSTEM_HAISHIZUMI

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
    ''' インシデント検索件数取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0101">[IN]インシデント検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>インシデント検索件数取得用のSQLを作成し、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2012/07/27 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetResultCountSql(ByRef Adapter As NpgsqlDataAdapter, _
                                      ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKC0101 As DataHBKC0101) As Boolean

        'CI共通情報テーブル取得用SQLを設定
        Dim strSql As String = strSelectCountSql

        Try

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            '検索条件作成
            If CreateIncidentInfoSql(Adapter, Cn, dataHBKC0101, strSql, SEARCH_MODE_COUNT) = False Then
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
    ''' インシデント検索結果取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0101">[IN]インシデント検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>インシデント検索結果取得用のSQLを作成し、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2012/07/26 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectIncidentInfoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKC0101 As DataHBKC0101) As Boolean

        'CI共通情報テーブル取得用SQLを設定
        Dim strSql As String = strSelectIncidentInfoSql

        Try

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            '検索条件作成
            If CreateIncidentInfoSql(Adapter, Cn, dataHBKC0101, strSql, SEARCH_MODE_SEARCH) = False Then
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
    ''' SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0101">[IN]インシデント検索一覧画面データクラス</param>
    ''' <param name="strSql">[IN]基本SQL文</param>
    ''' <param name="strSearchMode">[IN]Sql判別モード</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>パラメータとして受け取ったSQL文をもとにSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/26 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function CreateIncidentInfoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKC0101 As DataHBKC0101, _
                                           ByVal strSql As String, _
                                           ByVal strSearchMode As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSelect As String = ""                    'SELECT文
        Dim strSearch As String = ""                    '検索条件
        Dim strKikiSearch As String = ""                'インシデント機器情報取得SQl
        Dim aryOutsideToolNum() As String = Nothing     '外部ツール番号
        Dim aryTitle() As String = Nothing              'タイトル
        Dim aryUkeNaiyo() As String = Nothing           '受付内容
        Dim aryTaioKekka() As String = Nothing          '対応結果
        Dim arySagyou() As String = Nothing          '作業内容
        Dim aryFreeText() As String = Nothing           'フリーテキスト

        Try

            With dataHBKC0101

                strSelect = strSql

                '***************************************************************************************************************
                'サンプル
                'WHERE it.IncNmb in (SELECT ki.IncNmb FROM incident_kiki_tb ki WHERE ki.KindCD = :KindCD AND ki.Num = :Num Group by ki.IncNmb)
                '***************************************************************************************************************

                '前提条件
                strSearch &= " WHERE iit.IncNmb IN (" & vbCrLf & _
                             "                      SELECT ikant.IncNmb FROM incident_kankei_tb AS ikant" & vbCrLf & _
                             "                      WHERE (ikant.RelationKbn = '" & KBN_GROUP & "' AND ikant.RelationID IN(" & dataHBKC0101.PropStrLoginUserGrp & "))" & vbCrLf & _
                             "                      OR (ikant.RelationKbn = '" & KBN_USER & "' AND ikant.RelationID = '" & dataHBKC0101.PropStrLoginUserId & "')" & vbCrLf & _
                             "                      GROUP BY ikant.IncNmb" & vbCrLf & _
                             "                     )"

                'インシデント番号(完全一致)
                If .PropBlnIncNumInputFlg = False Then
                    strSearch &= " AND iit.IncNmb = :IncNmb" & vbCrLf
                End If
                'インシデント基本情報：受付手段(完全一致)
                If .PropStrUketsukeWay.Trim <> "" Then
                    strSearch &= " AND iit.UkeKbnCD = :UkeKbnCD" & vbCrLf
                End If
                'インシデント基本情報：インシデント種別(完全一致)
                If .PropStrIncidentKind.Trim <> "" Then
                    strSearch &= " AND iit.IncKbnCD = :IncKbnCD" & vbCrLf
                End If
                'インシデント基本情報：ドメイン(完全一致)
                If .PropStrDomain.Trim <> "" Then
                    strSearch &= " AND iit.DomainCD = :DomainCD" & vbCrLf
                End If
                'インシデント基本情報：外部ツール番号(完全一致)
                If .PropStrOutsideToolNum.Trim <> "" Then
                    '検索文字列の分割
                    aryOutsideToolNum = commonLogicHBK.GetSearchStringList(.PropStrOutsideToolNum, SPLIT_MODE_OR)
                    '分割分だけ検索条件の設定
                    If aryOutsideToolNum.Length <> 0 Then
                        strSearch &= " AND "
                        strSearch &= " ("
                        For intCnt = 0 To aryOutsideToolNum.Count - 1
                            strSearch &= " iit.OutSideToolNmb = :OutSideToolNmb" + intCnt.ToString()
                            If intCnt <> aryOutsideToolNum.Count - 1 Then
                                strSearch &= " OR "
                            End If
                        Next
                        strSearch &= ") " & vbCrLf
                    End If
                End If
                'インシデント基本情報：ステータス(完全一致)
                If .PropStrStatus <> Nothing Then
                    strSearch &= " AND iit.ProcessStateCD IN (" & .PropStrStatus & ")" & vbCrLf
                End If
                'インシデント基本情報：対象システム(完全一致)
                If .PropStrTargetSystem <> Nothing Then
                    strSearch &= " AND cit.CINmb IN (" & .PropStrTargetSystem & ")" & vbCrLf
                End If
                'インシデント基本情報：タイトル(あいまい検索)
                If .PropStrTitle.Trim <> "" Then
                    '検索文字列の分割
                    aryTitle = commonLogicHBK.GetSearchStringList(.PropStrTitle, SPLIT_MODE_AND)
                    '分割分だけ検索条件の設定
                    If aryTitle.Length <> 0 Then
                        strSearch &= " AND "
                        strSearch &= " ("
                        For intCnt = 0 To aryTitle.Count - 1
                            strSearch &= " iit.TitleAimai LIKE :TitleAimai" + intCnt.ToString()
                            If intCnt <> aryTitle.Count - 1 Then
                                strSearch &= " AND "
                            End If
                        Next
                        strSearch &= ") " & vbCrLf
                    End If
                End If
                'インシデント基本情報：受付内容(あいまい検索)
                If .PropStrUkeNaiyo.Trim <> "" Then
                    '検索文字列の分割
                    aryUkeNaiyo = commonLogicHBK.GetSearchStringList(.PropStrUkeNaiyo, SPLIT_MODE_AND)
                    '分割分だけ検索条件の設定
                    If aryUkeNaiyo.Length <> 0 Then
                        strSearch &= " AND "
                        strSearch &= " ("
                        For intCnt = 0 To aryUkeNaiyo.Count - 1
                            strSearch &= " iit.UkeNaiyoAimai LIKE :UkeNaiyoAimai" + intCnt.ToString()
                            If intCnt <> aryUkeNaiyo.Count - 1 Then
                                strSearch &= " AND "
                            End If
                        Next
                        strSearch &= ") " & vbCrLf
                    End If
                End If
                'インシデント基本情報：対応結果(あいまい検索)
                If .PropStrTaioKekka.Trim <> "" Then
                    '検索文字列の分割
                    aryTaioKekka = commonLogicHBK.GetSearchStringList(.PropStrTaioKekka, SPLIT_MODE_AND)
                    '分割分だけ検索条件の設定
                    If aryTaioKekka.Length <> 0 Then
                        strSearch &= " AND "
                        strSearch &= " ("
                        For intCnt = 0 To aryTaioKekka.Count - 1
                            strSearch &= " iit.TaioKekkaAimai LIKE :TaioKekkaAimai" + intCnt.ToString()
                            If intCnt <> aryTaioKekka.Count - 1 Then
                                strSearch &= " AND "
                            End If
                        Next
                        strSearch &= ") " & vbCrLf
                    End If
                End If
                'インシデント基本情報：発生日(From)
                If .PropStrHasseiDTFrom.Trim <> "" Then
                    strSearch &= " AND"
                    strSearch &= " TO_CHAR(iit.HasseiDT,'YYYY/MM/DD') >= :HasseiDTFrom" & vbCrLf
                End If
                'インシデント基本情報：発生日(To)
                If .PropStrHasseiDTTo.Trim <> "" Then
                    strSearch &= " AND"
                    strSearch &= " TO_CHAR(iit.HasseiDT,'YYYY/MM/DD') <= :HasseiDTTo" & vbCrLf
                End If
                'インシデント基本情報：最終更新日時(From)
                If .PropStrUpdateDTFrom.Trim <> "" Then
                    If .PropTxtExUpdateTimeFrom.PropTxtTime.Text.Trim <> "" Then
                        strSearch &= " AND"
                        strSearch &= " TO_CHAR(iit.UpdateDT,'YYYY/MM/DD HH24:MI') >= :UpdateDTFrom" & vbCrLf
                    Else
                        '時間表記なし
                        strSearch &= " AND"
                        strSearch &= " TO_CHAR(iit.UpdateDT,'YYYY/MM/DD') >= :UpdateDTFrom" & vbCrLf
                    End If

                End If
                'インシデント基本情報：最終更新日時(To)
                If .PropStrUpdateDTTo.Trim <> "" Then
                    If .PropTxtExUpdateTimeTo.PropTxtTime.Text.Trim <> "" Then
                        strSearch &= " AND"
                        strSearch &= " TO_CHAR(iit.UpdateDT,'YYYY/MM/DD HH24:MI') <= :UpdateDTTo" & vbCrLf
                    Else
                        '時間表記なし
                        strSearch &= " AND"
                        strSearch &= " TO_CHAR(iit.UpdateDT,'YYYY/MM/DD') <= :UpdateDTTo" & vbCrLf
                    End If
                End If
                'フリーテキスト検索(あいまい検索)
                If .PropStrFreeText.Trim <> "" Then
                    '検索文字列の分割
                    aryFreeText = commonLogicHBK.GetSearchStringList(.PropStrFreeText, SPLIT_MODE_AND)
                    '分割分だけ検索条件の設定
                    If aryFreeText.Length <> 0 Then
                        strSearch &= " AND "
                        strSearch &= " ("
                        For intCnt = 0 To aryFreeText.Count - 1
                            strSearch &= " iit.BikoAimai LIKE :BikoAimai" + intCnt.ToString()
                            If intCnt <> aryFreeText.Count - 1 Then
                                strSearch &= " AND "
                            End If
                        Next
                        strSearch &= ") " & vbCrLf
                    End If
                End If
                'インシデント基本情報：フリーフラグ1(完全一致)
                If .PropStrFreeFlg1.Trim <> "" Then
                    strSearch &= " AND iit.FreeFlg1 = :FreeFlg1" & vbCrLf
                End If
                'インシデント基本情報：フリーフラグ2(完全一致)
                If .PropStrFreeFlg2.Trim <> "" Then
                    strSearch &= " AND iit.FreeFlg2 = :FreeFlg2" & vbCrLf
                End If
                'インシデント基本情報：フリーフラグ3(完全一致)
                If .PropStrFreeFlg3.Trim <> "" Then
                    strSearch &= " AND iit.FreeFlg3 = :FreeFlg3" & vbCrLf
                End If
                'インシデント基本情報：フリーフラグ4(完全一致)
                If .PropStrFreeFlg4.Trim <> "" Then
                    strSearch &= " AND iit.FreeFlg4 = :FreeFlg4" & vbCrLf
                End If
                'インシデント基本情報：フリーフラグ5(完全一致)
                If .PropStrFreeFlg5.Trim <> "" Then
                    strSearch &= " AND iit.FreeFlg5 = :FreeFlg5" & vbCrLf
                End If
                '相手情報：相手ID(完全一致)
                If .PropStrPartnerID.Trim <> "" Then
                    strSearch &= " AND iit.PartnerIDAimai = :PartnerIDAimai" & vbCrLf
                End If
                '相手情報：相手氏名(あいまい)
                If .PropStrPartnerNM.Trim <> "" Then
                    strSearch &= " AND iit.PartnerNMAimai LIKE :PartnerNMAimai" & vbCrLf
                End If
                '相手情報：相手部署(あいまい)
                If .PropStrUsrBusyoNM.Trim <> "" Then
                    strSearch &= " AND iit.UsrBusyoNMAimai LIKE :UsrBusyoNMAimai" & vbCrLf
                End If
                'イベント情報：イベントID(あいまい)
                If .PropStrEventID.Trim <> "" Then
                    strSearch &= " AND iit.EventIDAimai LIKE :EventIDAimai" & vbCrLf
                End If
                'イベント情報：OPCイベントID(あいまい)
                If .PropStrOPCEventID.Trim <> "" Then
                    strSearch &= " AND iit.OPCEventIDAimai LIKE :OPCEventIDAimai" & vbCrLf
                End If
                'イベント情報：ソース(あいまい)
                If .PropStrSource.Trim <> "" Then
                    strSearch &= " AND iit.SourceAimai LIKE :SourceAimai" & vbCrLf
                End If
                'イベント情報：イベントクラス(あいまい)
                If .PropStrEventClass.Trim <> "" Then
                    strSearch &= " AND iit.EventClassAimai LIKE :EventClassAimai" & vbCrLf
                End If
                '*******************************************************************************
                '担当者グループ,担当者ID,担当者氏名



                '担当者情報
                If .PropRdoChokusetsu.Checked = True Then
                    '直接選択時

                    '担当者グループ
                    If .PropStrTantoGrp.Trim <> "" Then
                        strSearch &= " AND iit.TantoGrpCD = :TantoGrpCD " & vbCrLf
                    End If
                    '担当者ID(あいまい)
                    If .PropStrIncTantoID.Trim <> "" Then
                        strSearch &= " AND iit.IncTantIDAimai = :IncTantoID " & vbCrLf
                    End If
                    '担当者氏名 インシデント担当者氏名(あいまい)

                    If .PropStrIncTantoNM.Trim <> "" Then
                        strSearch &= " AND iit.IncTantNMAimai LIKE :TantNMAimai " & vbCrLf
                    End If

                ElseIf .PropRdoKanyo.Checked = True Then
                    '関与選択時

                    '担当者グループ、担当者ID、担当者氏名が入力されているかチェック
                    If .PropStrTantoGrp.Trim <> "" Or .PropStrIncTantoID.Trim <> "" Or .PropStrIncTantoNM.Trim <> "" Then

                        strSearch &= " AND EXISTS (SELECT DISTINCT iwrt.IncNmb FROM incident_wk_rireki_tb AS iwrt " & vbCrLf
                        strSearch &= " LEFT OUTER JOIN incident_wk_tanto_tb AS iwtt ON iwrt.IncNmb = iwtt.IncNmb AND iwrt.WorkRirekiNmb = iwtt.WorkRirekiNmb" & vbCrLf
                        strSearch &= " LEFT OUTER JOIN hbkusr_mtb AS hm2 ON hm2.HBKUsrID = iwtt.WorkTantoID " & vbCrLf
                        strSearch &= " WHERE" & vbCrLf

                        '担当者グループ
                        If .PropStrTantoGrp.Trim <> "" Then
                            strSearch &= " iwtt.WorkTantoGrpCD = :TantoGrpCD" & vbCrLf
                        End If
                        '担当者ID
                        If .PropStrIncTantoID.Trim <> "" Then
                            If .PropStrTantoGrp.Trim <> "" Then
                                strSearch &= " AND" & vbCr
                            End If
                            strSearch &= " iwtt.WorkTantoID = :IncTantoID" & vbCrLf
                        End If
                        '担当者氏名 
                        If .PropStrIncTantoNM.Trim <> "" Then
                            If .PropStrTantoGrp.Trim <> "" Or .PropStrIncTantoID.Trim <> "" Then
                                strSearch &= " AND" & vbCr
                            End If
                            strSearch &= " hm2.HBKUsrNMAimai LIKE :TantNMAimai" & vbCrLf
                        End If

                        strSearch &= " AND iit.IncNmb = iwrt.IncNmb ) " & vbCrLf
                    End If
                End If

                '*****************************************************************************************
                '作業予定日時(From)、(To)か作業内容に入力があった場合
                If .PropStrWorkSceDTFrom.Trim <> "" Or .PropStrWorkSceDTTo.Trim <> "" Or .PropStrWorkNaiyo <> "" Then

                    strSearch &= " AND EXISTS (SELECT DISTINCT iwrt.IncNmb " & vbCrLf
                    strSearch &= " FROM incident_wk_rireki_tb iwrt " & vbCrLf
                    strSearch &= " WHERE " & vbCrLf
                    '作業予定日時(From)
                    If .PropStrWorkSceDTFrom.Trim <> "" Then
                        If .PropTxtExWorkSceTimeFrom.PropTxtTime.Text.Trim <> "" Then
                            strSearch &= " TO_CHAR(iwrt.WorkSceDT,'YYYY/MM/DD HH24:MI') >= :WorkSceDTFrom " & vbCrLf
                        Else
                            '時間表記なし
                            strSearch &= " TO_CHAR(iwrt.WorkSceDT,'YYYY/MM/DD') >= :WorkSceDTFrom " & vbCrLf
                        End If

                    End If
                    '作業予定日時(To)
                    If .PropStrWorkSceDTTo.Trim <> "" Then
                        If .PropStrWorkSceDTFrom.Trim <> "" Then
                            strSearch &= " AND " & vbCr
                        End If
                        If .PropTxtExWorkSceTimeTo.PropTxtTime.Text.Trim <> "" Then
                            strSearch &= " TO_CHAR(iwrt.WorkSceDT,'YYYY/MM/DD HH24:MI') <= :WorkSceDTTo " & vbCrLf
                        Else
                            '時間表記なし
                            strSearch &= " TO_CHAR(iwrt.WorkSceDT,'YYYY/MM/DD') <= :WorkSceDTTo " & vbCrLf
                        End If

                    End If
                    '作業内容
                    If .PropStrWorkNaiyo <> "" Then
                        If .PropStrWorkSceDTFrom.Trim <> "" Or _
                           .PropStrWorkSceDTTo.Trim <> "" Then
                            strSearch &= " OR " & vbCrLf
                        End If
                        '検索文字列の分割
                        arySagyou = commonLogicHBK.GetSearchStringList(.PropStrWorkNaiyo, SPLIT_MODE_AND)
                        '分割分だけ検索条件の設定
                        If arySagyou.Length <> 0 Then
                            strSearch &= " ("
                            For intCnt = 0 To arySagyou.Count - 1
                                strSearch &= " iwrt.WorkNaiyoAimai LIKE :WorkNaiyoAimai" + intCnt.ToString()
                                If intCnt <> arySagyou.Count - 1 Then
                                    strSearch &= " AND "
                                End If
                            Next
                            strSearch &= ") " & vbCrLf
                        End If
                    End If
                    strSearch &= "AND iit.IncNmb = iwrt.IncNmb ) " & vbCrLf
                End If
                '*****************************************************************************************

                'プロセスリンク
                If .PropStrProcessLinkNumAry <> "" Then
                    strSearch &= " AND iit.IncNmb IN ( " & .PropStrProcessLinkNumAry & ")" & vbCrLf
                End If


                'インシデント機器情報
                If .PropStrKikiKind <> "" And .PropStrKikiNum <> "" Then
                    '機器種別及び機器番号入力時
                    strSearch &= " AND iit.IncNmb IN ("
                    strSearch &= strSelectIncidentKikiSql
                    strSearch &= " WHERE ikit.KindCD = :KindCD AND ikit.Num = :Num Group by ikit.IncNmb)" & vbCrLf
                ElseIf .PropStrKikiKind <> "" Then
                    '機器種別のみ入力時
                    strSearch &= " AND iit.IncNmb IN ("
                    strSearch &= strSelectIncidentKikiSql
                    strSearch &= " WHERE ikit.KindCD = :KindCD Group by ikit.IncNmb )" & vbCrLf
                ElseIf .PropStrKikiNum <> "" Then
                    '機器番号のみ入力時
                    strSearch &= " AND iit.IncNmb IN ("
                    strSearch &= strSelectIncidentKikiSql
                    strSearch &= " WHERE ikit.Num = :Num Group by ikit.IncNmb )" & vbCrLf
                End If

                '+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-
                '検索時の処理
                If strSearchMode = SEARCH_MODE_SEARCH Then
                    'ORDER BY句を指定
                    'strSearch &= " ORDER BY SortDT, iit.IncNmb"

                    '【EDIT】 2012/08/15 r.hoshino START
                    'デフォルトソート用にROWNUMを取得
                    '【EDIT】 2012/09/12 m.ibuki START
                    'strSelect = "SELECT Tx.*,row_number()over() AS SortNo FROM (" & strSelect
                    '【EDIT】 2012/09/12 m.ibuki END
                    'strSearch &= "ORDER BY COALESCE(iit.hasseidt,to_date('0000/00/00 00:00','YYYY/MM/DD HH24:MI:SS')) DESC"
                    strSearch &= "ORDER BY iit.hasseidt DESC"
                    'strSearch &= ",iit.IncNmb) Tx"
                    '【EDIT】 2012/09/12 m.ibuki START
                    strSearch &= ",iit.IncNmb"
                    '【EDIT】 2012/09/12 m.ibuki END
                    '【EDIT】 2012/08/15 r.hoshino END

                End If

                '検索条件セット
                strSelect &= strSearch

                'データアダプタに、SQLのSELECT文を設定
                Adapter.SelectCommand = New NpgsqlCommand(strSelect, Cn)
                '+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-

                'インシデント番号(完全一致)
                If .PropBlnIncNumInputFlg = False Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))
                    Adapter.SelectCommand.Parameters("IncNmb").Value = .PropIntNum
                End If
                'インシデント基本情報：受付手段(完全一致)
                If .PropStrUketsukeWay.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("UkeKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("UkeKbnCD").Value = .PropStrUketsukeWay
                End If
                'インシデント基本情報：インシデント種別(完全一致)
                If .PropStrIncidentKind.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("IncKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("IncKbnCD").Value = .PropStrIncidentKind
                End If
                'インシデント基本情報：ドメイン(完全一致)
                If .PropStrDomain.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("DomainCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("DomainCD").Value = .PropStrDomain
                End If
                'インシデント基本情報：外部ツール番号(完全一致)
                If .PropStrOutsideToolNum.Trim <> "" Then
                    'バインド変数を設定
                    For i As Integer = 0 To aryOutsideToolNum.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("OutSideToolNmb" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("OutSideToolNmb" + i.ToString).Value = aryOutsideToolNum(i)
                    Next
                End If
                'インシデント基本情報：タイトル(あいまい検索)
                If .PropStrTitle.Trim <> "" Then
                    '検索文字列をあいまい検索用に変換
                    For i As Integer = 0 To aryTitle.Count - 1
                        aryTitle(i) = commonLogicHBK.ChangeStringForSearch(aryTitle(i))
                    Next
                    'バインド変数を設定
                    For i As Integer = 0 To aryTitle.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("TitleAimai" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("TitleAimai" + i.ToString).Value = "%" & aryTitle(i) & "%"
                    Next
                End If
                'インシデント基本情報：受付内容(あいまい検索)
                If .PropStrUkeNaiyo.Trim <> "" Then
                    '検索文字列をあいまい検索用に変換
                    For i As Integer = 0 To aryUkeNaiyo.Count - 1
                        aryUkeNaiyo(i) = commonLogicHBK.ChangeStringForSearch(aryUkeNaiyo(i))
                    Next
                    'バインド変数を設定
                    For i As Integer = 0 To aryUkeNaiyo.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("UkeNaiyoAimai" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("UkeNaiyoAimai" + i.ToString).Value = "%" & aryUkeNaiyo(i) & "%"
                    Next
                End If
                'インシデント基本情報：受付内容(あいまい検索)
                If .PropStrTaioKekka.Trim <> "" Then
                    '検索文字列をあいまい検索用に変換
                    For i As Integer = 0 To aryTaioKekka.Count - 1
                        aryTaioKekka(i) = commonLogicHBK.ChangeStringForSearch(aryTaioKekka(i))
                    Next
                    'バインド変数を設定
                    For i As Integer = 0 To aryTaioKekka.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("TaioKekkaAimai" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("TaioKekkaAimai" + i.ToString).Value = "%" & aryTaioKekka(i) & "%"
                    Next
                End If
                '作業日(FROM)
                If .PropStrHasseiDTFrom.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("HasseiDTFrom", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("HasseiDTFrom").Value = .PropStrHasseiDTFrom
                End If
                '作業日(To)
                If .PropStrHasseiDTTo.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("HasseiDTTo", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("HasseiDTTo").Value = .PropStrHasseiDTTo
                End If
                '最終更新日時(From)
                If .PropStrUpdateDTFrom.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("UpdateDTFrom", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("UpdateDTFrom").Value = .PropStrUpdateDTFrom
                End If
                '最終更新日時(To)
                If .PropStrUpdateDTTo.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("UpdateDTTo", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("UpdateDTTo").Value = .PropStrUpdateDTTo
                End If
                'フリーテキスト検索(あいまい検索)
                If .PropStrFreeText.Trim <> "" Then
                    '検索文字列をあいまい検索用に変換
                    For i As Integer = 0 To aryFreeText.Count - 1
                        aryFreeText(i) = commonLogicHBK.ChangeStringForSearch(aryFreeText(i))
                    Next
                    'バインド変数を設定
                    For i As Integer = 0 To aryFreeText.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("BikoAimai" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("BikoAimai" + i.ToString).Value = "%" & aryFreeText(i) & "%"
                    Next
                End If
                'インシデント基本情報：フリーフラグ1
                If .PropStrFreeFlg1.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg1", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg1").Value = .PropStrFreeFlg1
                End If
                'インシデント基本情報：フリーフラグ2
                If .PropStrFreeFlg2.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg2", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg2").Value = .PropStrFreeFlg2
                End If
                'インシデント基本情報：フリーフラグ3
                If .PropStrFreeFlg3.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg3", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg3").Value = .PropStrFreeFlg3
                End If
                'インシデント基本情報：フリーフラグ4
                If .PropStrFreeFlg4.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg4", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg4").Value = .PropStrFreeFlg4
                End If
                'インシデント基本情報：フリーフラグ5
                If .PropStrFreeFlg5.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg5", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg5").Value = .PropStrFreeFlg5
                End If
                '相手情報：相手ID(完全一致)
                If .PropStrPartnerID.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("PartnerIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("PartnerIDAimai").Value = commonLogicHBK.ChangeStringForSearch(.PropStrPartnerID)
                End If
                '相手情報：相手氏名(あいまい)
                If .PropStrPartnerNM.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("PartnerNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("PartnerNMAimai").Value = "%" & commonLogicHBK.ChangeStringForSearch(.PropStrPartnerNM) & "%"
                End If
                '相手情報：相手部署(あいまい)
                If .PropStrUsrBusyoNM.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("UsrBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("UsrBusyoNMAimai").Value = "%" & commonLogicHBK.ChangeStringForSearch(.PropStrUsrBusyoNM) & "%"
                End If
                'イベント情報：イベントID(あいまい)
                If .PropStrEventID.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("EventIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("EventIDAimai").Value = "%" & commonLogicHBK.ChangeStringForSearch(.PropStrEventID) & "%"
                End If
                'イベント情報：OPCイベントID(あいまい)
                If .PropStrOPCEventID.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("OPCEventIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("OPCEventIDAimai").Value = "%" & commonLogicHBK.ChangeStringForSearch(.PropStrOPCEventID) & "%"
                End If
                'イベント情報：ソース(あいまい)
                If .PropStrSource.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SourceAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("SourceAimai").Value = "%" & commonLogicHBK.ChangeStringForSearch(.PropStrSource) & "%"
                End If
                'イベント情報：イベントクラス(あいまい)
                If .PropStrEventClass.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("EventClassAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("EventClassAimai").Value = "%" & commonLogicHBK.ChangeStringForSearch(.PropStrEventClass) & "%"
                End If
                '担当者グループ
                If .PropStrTantoGrp.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("TantoGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("TantoGrpCD").Value = .PropStrTantoGrp
                End If
                '担当者ID
                If .PropStrIncTantoID.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("IncTantoID", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("IncTantoID").Value = commonLogicHBK.ChangeStringForSearch(.PropStrIncTantoID.Trim)
                End If
                '担当者氏名 
                If .PropStrIncTantoNM <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("TantNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("TantNMAimai").Value = "%" & commonLogicHBK.ChangeStringForSearch(.PropStrIncTantoNM.Trim) & "%"
                End If
                'インシデント機器情報
                If .PropStrKikiKind <> "" Or .PropStrKikiNum <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("KindCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("KindCD").Value = .PropStrKikiKind
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("Num", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("Num").Value = .PropStrKikiNum
                End If
                '作業予定日時(From)
                If .PropStrWorkSceDTFrom.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("WorkSceDTFrom", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("WorkSceDTFrom").Value = .PropStrWorkSceDTFrom
                End If
                '作業予定日時(To)
                If .PropStrWorkSceDTTo.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("WorkSceDTTo", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("WorkSceDTTo").Value = .PropStrWorkSceDTTo
                End If
                '作業内容
                If .PropStrWorkNaiyo <> "" Then
                    '検索文字列をあいまい検索用に変換
                    For i As Integer = 0 To arySagyou.Count - 1
                        arySagyou(i) = commonLogicHBK.ChangeStringForSearch(arySagyou(i))
                    Next
                    'バインド変数を設定
                    For i As Integer = 0 To arySagyou.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("WorkNaiyoAimai" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("WorkNaiyoAimai" + i.ToString).Value = "%" & arySagyou(i) & "%"
                    Next
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


    ''' <summary>
    ''' 【共通】マスタデータ取得：相手先
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0101">[IN]インシデント検索画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>指定相手先取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetPartnerInfoData(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0101 As DataHBKC0101) As Boolean

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
                .Parameters("endusrid").Value = dataHBKC0101.PropTxtPartnerID.Text              '相手ID
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
    ''' <param name="dataHBKC0101">[IN]インシデント検索画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>指定ユーザー取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetIncTantoInfoData(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0101 As DataHBKC0101) As Boolean

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
                .Parameters("hbkusrid").Value = dataHBKC0101.PropTxtIncTantoID.Text            '担当ID
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
