Imports Npgsql
Imports Common
Imports Common.CommonLogic
Imports System.Text
Imports System.Data.DataRow
Imports CommonHBK
Public Class SqlHBKZ0401
    Dim commonLogicHBK As New CommonLogicHBK
    '[mod] 2012/08/24 y.ikushima START
    'Dim strSqlSearchCount As String = "SELECT" & _
    '                                  "    COUNT(*) COUNT" & _
    '                                  " FROM" & _
    '                                  " (" & _
    '                                  "    SELECT" & _
    '                                  "        INCTB.ProcessKbn" & _
    '                                  "       ,'" & PROCESS_TYPE_INCIDENT_NAME & "' ProcessNM" & _
    '                                  "       ,INCTB.IncNmb MngNmb" & _
    '                                  "       ,INCTB.ProcessStateCD StateCD" & _
    '                                  "       ,PROMTB.ProcessStateNM StateNM" & _
    '                                  "       ,INCTB.Title" & _
    '                                  "       ,INCTB.UkeNaiyo Naiyo" & _
    '                                  "       ,INCTB.SystemNmb" & _
    '                                  "       ,INCTB.TantoGrpCD GroupCD" & _
    '                                  "       ,GRPMTB.GroupNM" & _
    '                                  "       ,INCTB.RegDT" & _
    '                                  "       ,INCTB.titleaimai" & _
    '                                  "       ,INCTB.ukenaiyoaimai" & _
    '                                  "    FROM" & _
    '                                  "        INCIDENT_INFO_TB INCTB" & _
    '                                  "        LEFT OUTER JOIN PROCESSSTATE_MTB PROMTB" & _
    '                                  "            ON INCTB.ProcessStateCD = PROMTB.ProcessStateCD" & _
    '                                  "        LEFT OUTER JOIN GRP_MTB GRPMTB" & _
    '                                  "            ON INCTB.TantoGrpCD = GRPMTB.GroupCD" & _
    '                                  "    WHERE" & _
    '                                  "        INCTB.ProcessKbn = '" & PROCESS_TYPE_INCIDENT & "'" & _
    '                                  "    UNION ALL" & _
    '                                  "    SELECT" & _
    '                                  "        INCTB.ProcessKbn" & _
    '                                  "       ,'" & PROCESS_TYPE_QUESTION_NAME & "' ProcessNM" & _
    '                                  "       ,INCTB.IncNmb MngNmb" & _
    '                                  "       ,INCTB.ProcessStateCD StateCD" & _
    '                                  "       ,PROMTB.ProcessStateNM StateNM" & _
    '                                  "       ,INCTB.Title" & _
    '                                  "       ,INCTB.UkeNaiyo Naiyo" & _
    '                                  "       ,INCTB.SystemNmb" & _
    '                                  "       ,INCTB.TantoGrpCD GroupCD" & _
    '                                  "       ,GRPMTB.GroupNM" & _
    '                                  "       ,INCTB.RegDT" & _
    '                                  "       ,INCTB.titleaimai" & _
    '                                  "       ,INCTB.ukenaiyoaimai" & _
    '                                  "    FROM" & _
    '                                  "        INCIDENT_INFO_TB INCTB" & _
    '                                  "        LEFT OUTER JOIN PROCESSSTATE_MTB PROMTB" & _
    '                                  "            ON INCTB.ProcessStateCD = PROMTB.ProcessStateCD" & _
    '                                  "        LEFT OUTER JOIN GRP_MTB GRPMTB" & _
    '                                  "            ON INCTB.TantoGrpCD = GRPMTB.GroupCD" & _
    '                                  "    WHERE" & _
    '                                  "        INCTB.ProcessKbn = '" & PROCESS_TYPE_QUESTION & "'" & _
    '                                  "    UNION ALL" & _
    '                                  "    SELECT" & _
    '                                  "        INCTB.ProcessKbn" & _
    '                                  "       ,'" & PROCESS_TYPE_CHANGE_NAME & "' ProcessNM" & _
    '                                  "       ,INCTB.IncNmb MngNmb" & _
    '                                  "       ,INCTB.ProcessStateCD StateCD" & _
    '                                  "       ,PROMTB.ProcessStateNM StateNM" & _
    '                                  "       ,INCTB.Title" & _
    '                                  "       ,INCTB.UkeNaiyo Naiyo" & _
    '                                  "       ,INCTB.SystemNmb" & _
    '                                  "       ,INCTB.TantoGrpCD GroupCD" & _
    '                                  "       ,GRPMTB.GroupNM" & _
    '                                  "       ,INCTB.RegDT" & _
    '                                  "       ,INCTB.titleaimai" & _
    '                                  "       ,INCTB.ukenaiyoaimai" & _
    '                                  "    FROM" & _
    '                                  "        INCIDENT_INFO_TB INCTB" & _
    '                                  "        LEFT OUTER JOIN PROCESSSTATE_MTB PROMTB" & _
    '                                  "            ON INCTB.ProcessStateCD = PROMTB.ProcessStateCD" & _
    '                                  "        LEFT OUTER JOIN GRP_MTB GRPMTB" & _
    '                                  "            ON INCTB.TantoGrpCD = GRPMTB.GroupCD" & _
    '                                  "    WHERE" & _
    '                                  "        INCTB.ProcessKbn = '" & PROCESS_TYPE_CHANGE & "'" & _
    '                                  "    UNION ALL" & _
    '                                  "    SELECT" & _
    '                                  "        INCTB.ProcessKbn" & _
    '                                  "       ,'" & PROCESS_TYPE_RELEASE_NAME & "' ProcessNM" & _
    '                                  "       ,INCTB.IncNmb MngNmb" & _
    '                                  "       ,INCTB.ProcessStateCD StateCD" & _
    '                                  "       ,PROMTB.ProcessStateNM StateNM" & _
    '                                  "       ,INCTB.Title" & _
    '                                  "       ,INCTB.UkeNaiyo Naiyo" & _
    '                                  "       ,INCTB.SystemNmb" & _
    '                                  "       ,INCTB.TantoGrpCD GroupCD" & _
    '                                  "       ,GRPMTB.GroupNM" & _
    '                                  "       ,INCTB.RegDT" & _
    '                                  "       ,INCTB.titleaimai" & _
    '                                  "       ,INCTB.ukenaiyoaimai" & _
    '                                  "    FROM" & _
    '                                  "        INCIDENT_INFO_TB INCTB" & _
    '                                  "        LEFT OUTER JOIN PROCESSSTATE_MTB PROMTB" & _
    '                                  "            ON INCTB.ProcessStateCD = PROMTB.ProcessStateCD" & _
    '                                  "        LEFT OUTER JOIN GRP_MTB GRPMTB" & _
    '                                  "            ON INCTB.TantoGrpCD = GRPMTB.GroupCD" & _
    '                                  "    WHERE" & _
    '                                  "        INCTB.ProcessKbn = '" & PROCESS_TYPE_RELEASE & "'" & _
    '                                  " ) TB01" & _
    '                                  " WHERE" & _
    '                                  "    1 = 1"

    'Dim strSqlSearch As String = "SELECT" & _
    '                             "    FALSE CHK" & _
    '                             "   ,TB01.ProcessNM" & _
    '                             "   ,TB01.MngNmb" & _
    '                             "   ,TB01.StateNM" & _
    '                             "   ,TB01.Title" & _
    '                             "   ,TB01.Naiyo" & _
    '                             "   ,TO_CHAR(TB01.RegDT, 'yyyy-mm-dd hh:mm:ss') RegDT" & _
    '                             "   ,TB01.GroupNM" & _
    '                             "   ,TB01.ProcessKbn" & _
    '                             " FROM" & _
    '                             " (" & _
    '                             "    SELECT" & _
    '                             "        INCTB.ProcessKbn ProcessKbn" & _
    '                             "       ,'" & PROCESS_TYPE_INCIDENT_NAME & "' ProcessNM" & _
    '                             "       ,INCTB.IncNmb MngNmb" & _
    '                             "       ,INCTB.ProcessStateCD StateCD" & _
    '                             "       ,PROMTB.ProcessStateNM StateNM" & _
    '                             "       ,INCTB.Title" & _
    '                             "       ,INCTB.UkeNaiyo Naiyo" & _
    '                             "       ,INCTB.SystemNmb" & _
    '                             "       ,INCTB.TantoGrpCD GroupCD" & _
    '                             "       ,GRPMTB.GroupNM" & _
    '                             "       ,INCTB.RegDT" & _
    '                             "       ,INCTB.titleaimai" & _
    '                             "       ,INCTB.ukenaiyoaimai" & _
    '                             "    FROM" & _
    '                             "        INCIDENT_INFO_TB INCTB" & _
    '                             "        LEFT OUTER JOIN PROCESSSTATE_MTB PROMTB" & _
    '                             "            ON INCTB.ProcessStateCD = PROMTB.ProcessStateCD" & _
    '                             "        LEFT OUTER JOIN GRP_MTB GRPMTB" & _
    '                             "            ON INCTB.TantoGrpCD = GRPMTB.GroupCD" & _
    '                             "    WHERE" & _
    '                             "        INCTB.ProcessKbn = '" & PROCESS_TYPE_INCIDENT & "'" & _
    '                             "    UNION ALL" & _
    '                             "    SELECT" & _
    '                             "        INCTB.ProcessKbn ProcessKbn" & _
    '                             "       ,'" & PROCESS_TYPE_QUESTION_NAME & "' ProcessNM" & _
    '                             "       ,INCTB.IncNmb MngNmb" & _
    '                             "       ,INCTB.ProcessStateCD StateCD" & _
    '                             "       ,PROMTB.ProcessStateNM StateNM" & _
    '                             "       ,INCTB.Title" & _
    '                             "       ,INCTB.UkeNaiyo Naiyo" & _
    '                             "       ,INCTB.SystemNmb" & _
    '                             "       ,INCTB.TantoGrpCD GroupCD" & _
    '                             "       ,GRPMTB.GroupNM" & _
    '                             "       ,INCTB.RegDT" & _
    '                             "       ,INCTB.titleaimai" & _
    '                             "       ,INCTB.ukenaiyoaimai" & _
    '                             "    FROM" & _
    '                             "        INCIDENT_INFO_TB INCTB" & _
    '                             "        LEFT OUTER JOIN PROCESSSTATE_MTB PROMTB" & _
    '                             "            ON INCTB.ProcessStateCD = PROMTB.ProcessStateCD" & _
    '                             "        LEFT OUTER JOIN GRP_MTB GRPMTB" & _
    '                             "            ON INCTB.TantoGrpCD = GRPMTB.GroupCD" & _
    '                             "    WHERE" & _
    '                             "        INCTB.ProcessKbn = '" & PROCESS_TYPE_QUESTION & "'" & _
    '                             "    UNION ALL" & _
    '                             "    SELECT" & _
    '                             "        INCTB.ProcessKbn ProcessKbn" & _
    '                             "       ,'" & PROCESS_TYPE_CHANGE_NAME & "' ProcessNM" & _
    '                             "       ,INCTB.IncNmb MngNmb" & _
    '                             "       ,INCTB.ProcessStateCD StateCD" & _
    '                             "       ,PROMTB.ProcessStateNM StateNM" & _
    '                             "       ,INCTB.Title" & _
    '                             "       ,INCTB.UkeNaiyo Naiyo" & _
    '                             "       ,INCTB.SystemNmb" & _
    '                             "       ,INCTB.TantoGrpCD GroupCD" & _
    '                             "       ,GRPMTB.GroupNM" & _
    '                             "       ,INCTB.RegDT" & _
    '                             "       ,INCTB.titleaimai" & _
    '                             "       ,INCTB.ukenaiyoaimai" & _
    '                             "    FROM" & _
    '                             "        INCIDENT_INFO_TB INCTB" & _
    '                             "        LEFT OUTER JOIN PROCESSSTATE_MTB PROMTB" & _
    '                             "            ON INCTB.ProcessStateCD = PROMTB.ProcessStateCD" & _
    '                             "        LEFT OUTER JOIN GRP_MTB GRPMTB" & _
    '                             "            ON INCTB.TantoGrpCD = GRPMTB.GroupCD" & _
    '                             "    WHERE" & _
    '                             "        INCTB.ProcessKbn = '" & PROCESS_TYPE_CHANGE & "'" & _
    '                             "    UNION ALL" & _
    '                             "    SELECT" & _
    '                             "        INCTB.ProcessKbn ProcessKbn" & _
    '                             "       ,'" & PROCESS_TYPE_RELEASE_NAME & "' ProcessNM" & _
    '                             "       ,INCTB.IncNmb MngNmb" & _
    '                             "       ,INCTB.ProcessStateCD StateCD" & _
    '                             "       ,PROMTB.ProcessStateNM StateNM" & _
    '                             "       ,INCTB.Title" & _
    '                             "       ,INCTB.UkeNaiyo Naiyo" & _
    '                             "       ,INCTB.SystemNmb" & _
    '                             "       ,INCTB.TantoGrpCD GroupCD" & _
    '                             "       ,GRPMTB.GroupNM" & _
    '                             "       ,INCTB.RegDT" & _
    '                             "       ,INCTB.titleaimai" & _
    '                             "       ,INCTB.ukenaiyoaimai" & _
    '                             "    FROM" & _
    '                             "        INCIDENT_INFO_TB INCTB" & _
    '                             "        LEFT OUTER JOIN PROCESSSTATE_MTB PROMTB" & _
    '                             "            ON INCTB.ProcessStateCD = PROMTB.ProcessStateCD" & _
    '                             "        LEFT OUTER JOIN GRP_MTB GRPMTB" & _
    '                             "            ON INCTB.TantoGrpCD = GRPMTB.GroupCD" & _
    '                             "    WHERE" & _
    '                             "        INCTB.ProcessKbn = '" & PROCESS_TYPE_RELEASE & "'" & _
    '                             " ) TB01" & _
    '                             " WHERE" & _
    '                             "    1 = 1"
    '[mod] 2012/08/24 y.ikushima END

    'インシデント情報検索
    Private strSqlSearchIncident As String = " SELECT " & vbCrLf & _
                                                                " IIT.ProcessKbn AS ProcessKbn, " & vbCrLf & _
                                                                " IIT.IncNmb AS MngNmb, " & vbCrLf & _
                                                                " PSM.ProcessStateNM AS StateNM, " & vbCrLf & _
                                                                " IIT.Title AS Title, " & vbCrLf & _
                                                                " IIT.UkeNaiyo AS Naiyo, " & vbCrLf & _
                                                                " GPM.GroupNM AS GroupNM, " & vbCrLf & _
                                                                " TO_CHAR(IIT.RegDT,'YYYY/MM/DD HH24:MI') AS RegDT " & vbCrLf & _
                                                            " FROM " & vbCrLf & _
                                                                " incident_info_tb IIT " & vbCrLf & _
                                                                " LEFT OUTER JOIN processstate_mtb PSM ON IIT.ProcessStateCD = PSM.ProcessStateCD " & vbCrLf & _
                                                                " LEFT OUTER JOIN grp_mtb GPM ON IIT.TantoGrpCD = GPM.GroupCD " & vbCrLf

    '問題情報検索
    Private strSqlSearchProblem As String = " SELECT " & vbCrLf & _
                                                                " PIT.ProcessKbn AS ProcessKbn, " & vbCrLf & _
                                                                " PIT.PrbNmb AS MngNmb, " & vbCrLf & _
                                                                " PSM.ProcessStateNM AS StateNM, " & vbCrLf & _
                                                                " PIT.Title AS Title, " & vbCrLf & _
                                                                " PIT.Naiyo AS Naiyo, " & vbCrLf & _
                                                                " GPM.GroupNM AS GroupNM," & vbCrLf & _
                                                                " TO_CHAR(PIT.RegDT,'YYYY/MM/DD HH24:MI') AS RegDT " & vbCrLf & _
                                                            " FROM " & vbCrLf & _
                                                                " problem_info_tb PIT " & vbCrLf & _
                                                                " LEFT OUTER JOIN processstate_mtb PSM ON PIT.ProcessStateCD = PSM.ProcessStateCD " & vbCrLf & _
                                                                " LEFT OUTER JOIN grp_mtb GPM ON PIT.TantoGrpCD = GPM.GroupCD " & vbCrLf

    '変更情報検索
    Private strSqlSearchChange As String = " SELECT " & vbCrLf & _
                                                            " CIT.ProcessKbn AS ProcessKbn, " & vbCrLf & _
                                                            " CIT.ChgNmb AS MngNmb, " & vbCrLf & _
                                                            " PSM.ProcessStateNM AS StateNM, " & vbCrLf & _
                                                            " CIT.Title AS Title, " & vbCrLf & _
                                                            " CIT.Naiyo AS Naiyo, " & vbCrLf & _
                                                            " GPM.GroupNM AS GroupNM, " & vbCrLf & _
                                                            " TO_CHAR(CIT.RegDT,'YYYY/MM/DD HH24:MI') AS RegDT " & vbCrLf & _
                                                        " FROM " & vbCrLf & _
                                                            " change_info_tb CIT " & vbCrLf & _
                                                            " LEFT OUTER JOIN processstate_mtb PSM ON CIT.ProcessStateCD = PSM.ProcessStateCD " & vbCrLf & _
                                                            " LEFT OUTER JOIN grp_mtb GPM ON CIT.TantoGrpCD = GPM.GroupCD " & vbCrLf

    'リリース情報検索
    Private strSqlSearchRelease As String = " SELECT " & vbCrLf & _
                                                                " RIT.ProcessKbn AS ProcessKbn, " & vbCrLf & _
                                                                " RIT.RelNmb AS MngNmb, " & vbCrLf & _
                                                                " PSM.ProcessStateNM AS StateNM, " & vbCrLf & _
                                                                " RIT.Title AS Title, " & vbCrLf & _
                                                                " RIT.Gaiyo AS Naiyo, " & vbCrLf & _
                                                                " GPM.GroupNM AS GroupNM," & vbCrLf & _
                                                                " TO_CHAR(RIT.RegDT,'YYYY/MM/DD HH24:MI') AS RegDT " & vbCrLf & _
                                                            " FROM " & vbCrLf & _
                                                                " release_info_tb RIT " & vbCrLf & _
                                                                " LEFT OUTER JOIN processstate_mtb PSM ON RIT.ProcessStateCD = PSM.ProcessStateCD " & vbCrLf & _
                                                                " LEFT OUTER JOIN grp_mtb GPM ON RIT.TantoGrpCD = GPM.GroupCD " & vbCrLf

    '件数取得SQL
    Private strSqlSearchCount As String = " SELECT COUNT(*) AS COUNT FROM " & vbCrLf

    '検索SQL
    Private strSqlSearch As String = " SELECT " & vbCrLf & _
                                                    " FALSE CHK" & vbCrLf & _
                                                    " , CASE WHEN T1.ProcessKbn = '" & PROCESS_TYPE_INCIDENT & "' THEN '" & PROCESS_TYPE_INCIDENT_NAME & "' " & vbCrLf & _
                                                    " WHEN T1.ProcessKbn = '" & PROCESS_TYPE_QUESTION & "' THEN '" & PROCESS_TYPE_QUESTION_NAME & "' " & vbCrLf & _
                                                    " WHEN T1.ProcessKbn = '" & PROCESS_TYPE_CHANGE & "' THEN '" & PROCESS_TYPE_CHANGE_NAME & "' " & vbCrLf & _
                                                    " WHEN T1.ProcessKbn = '" & PROCESS_TYPE_RELEASE & "' THEN '" & PROCESS_TYPE_RELEASE_NAME & "' " & vbCrLf & _
                                                    " ELSE '' END AS ProcessNM " & vbCrLf & _
                                                    " ,T1.MngNmb" & vbCrLf & _
                                                    " ,T1.StateNM" & vbCrLf & _
                                                    " ,T1.Title" & vbCrLf & _
                                                    " ,T1.Naiyo " & vbCrLf & _
                                                    " ,T1.RegDT " & vbCrLf & _
                                                    " ,T1.GroupNM" & vbCrLf & _
                                                    " ,T1.ProcessKbn" & vbCrLf & _
                                                    " FROM " & vbCrLf


    Dim strSqlGroup As String = "SELECT GroupCD, GroupNM FROM GRP_MTB WHERE COALESCE(JtiFlg,'0') <> '1' ORDER BY Sort ASC"
    Dim strSqlStatus As String = "SELECT ProcessStateCD,ProcessStateNM FROM PROCESSSTATE_MTB WHERE COALESCE(JtiFlg,'0') <> '1' AND ProcessKbn = :"
    Dim strSqlSystem As String = "SELECT CAST(CINmb AS VARCHAR) CINmb, Class1,Class2,CINM FROM CI_INFO_TB WHERE CIKbnCD = '" & CommonHBK.CommonDeclareHBK.CI_TYPE_SYSTEM & "' ORDER BY Sort"

    ''' <summary>
    ''' 対象システム取得用SQLの作成、実行
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgsqlDataAdapter型オブジェクト</param>
    ''' <param name="Cn">[IN]NpgsqlConnection型オブジェクト</param>
    ''' <param name="dataHBKZ0401">[IN]プロセス一覧検索画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false 異常終了</returns>
    ''' <remarks>対象システム一覧用SQLを作成し、データアダプタにセットする。
    ''' <para>作成情報：2012/06/13 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Public Function SetSelectSystemSql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByVal dataHBKZ0401 As DataHBKZ0401) As Boolean
        ' 開始ログ出力
        CommonLogic.WriteLog(LogLevel.None, "START", Nothing, Nothing)

        Try
            ' SQL文の設定
            Adapter.SelectCommand = New NpgsqlCommand(strSqlSystem, Cn)

            ' 終了ログ出力
            CommonLogic.WriteLog(LogLevel.None, "END", Nothing, Nothing)

            Return True

        Catch ex As Exception
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 対象グループ取得用SQLの作成、実行
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgsqlDataAdapter型オブジェクト</param>
    ''' <param name="Cn">[IN]NpgsqlConnection型オブジェクト</param>
    ''' <param name="dataHBKZ0401">[IN/OUT]プロセス一覧検索画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false 異常終了</returns>
    ''' <remarks>対象グループ一覧用SQLを作成し、データアダプタにセットする。
    ''' <para>作成情報：2012/06/13 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    ''' 
    Public Function SetSelectGroupSql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByVal dataHBKZ0401 As DataHBKZ0401) As Boolean
        ' 開始ログ出力
        CommonLogic.WriteLog(LogLevel.None, "START", Nothing, Nothing)

        Try
            ' SQL文の設定
            Adapter.SelectCommand = New NpgsqlCommand(strSqlGroup, Cn)

            ' 終了ログ出力
            CommonLogic.WriteLog(LogLevel.None, "END", Nothing, Nothing)

            Return True
        Catch ex As Exception
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' ステータス取得用SQLの作成、実行
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]DBController型オブジェクト</param>
    ''' <param name="Cn">[IN]NpgsqlConnection型オブジェクト</param>
    ''' <param name="DataHBKZ0401">[IN/OUT]プロセス一覧検索画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false 異常終了</returns>
    ''' <remarks>ステータス一覧用SQLを作成し、実行する
    ''' </remarks>
    Public Function setSelectStatusSql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByVal dataHBKZ0401 As DataHBKZ0401) As Boolean

        ' 開始ログ出力
        CommonLogic.WriteLog(LogLevel.None, "START", Nothing, Nothing)

        Try
            ' パラメータの作成
            Dim param As NpgsqlParameter = New NpgsqlParameter("ProcessKbnKbn", NpgsqlTypes.NpgsqlDbType.Varchar)
            param.Value = dataHBKZ0401.PropCmbProcess.SelectedValue.ToString

            ' SQLの作成
            Dim sb As New StringBuilder(strSqlStatus)
            sb.Append(param.ParameterName)
            sb.Append(" ORDER BY")
            sb.Append(" Sort")

            ' SQL文の設定
            Adapter.SelectCommand = New NpgsqlCommand(sb.ToString, Cn)

            ' パラメータの設定
            Adapter.SelectCommand.Parameters.Add(param)

            ' 終了ログ出力
            CommonLogic.WriteLog(LogLevel.None, "END", Nothing, Nothing)

            Return True
        Catch ex As Exception
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 検索用SQLの作成、実行
    ''' </summary>
    ''' <param name="dataHBKZ0401">[IN/OUT]プロセス一覧検索画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false 異常終了</returns>
    ''' <remarks>プロセス関連テーブルからレコードを取得する
    ''' <para>作成情報：2012/06/13 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Public Function SetSelectProcessSql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByVal dataHBKZ0401 As DataHBKZ0401) As Boolean

        ' 開始ログ出力
        CommonLogic.WriteLog(LogLevel.None, "START", Nothing, Nothing)

        Try

            'Spread表示SQL取得
            Dim StrSql As String = strSqlSearch

            'WHERE句設定
            If SetSelectProcessWhereSql(Adapter, Cn, dataHBKZ0401, StrSql, True) = False Then
                Return False
            End If

            '[mod] 2012/08/24 y.ikushima START
            '' パラメータの作成
            'Dim paramList As New List(Of NpgsqlParameter)
            'With dataHBKZ0401
            '    Dim index As Integer = 0
            '    ' プロセス
            '    If .PropCmbProcess.SelectedValue <> String.Empty Then
            '        paramList.Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))
            '        paramList(index).Value = .PropCmbProcess.SelectedValue
            '        index += 1
            '    End If

            '    ' 管理番号
            '    If .PropTxtManageNo.Text.Trim() <> String.Empty Then
            '        paramList.Add(New NpgsqlParameter("MngNmb", NpgsqlTypes.NpgsqlDbType.Integer))
            '        paramList(index).Value = commonLogicHBK.ChangeStringForSearch(.PropTxtManageNo.Text.Trim)
            '        index += 1
            '    End If

            '    ' ステータス
            '    If .PropCmbStatus.SelectedValue <> String.Empty Then
            '        paramList.Add(New NpgsqlParameter("StateCD", NpgsqlTypes.NpgsqlDbType.Varchar))
            '        paramList(index).Value = .PropCmbStatus.SelectedValue
            '        index += 1
            '    End If

            '    ' タイトル
            '    If .PropTxtTitle.Text.Trim() <> String.Empty Then
            '        Dim strArray() As String = commonLogicHBK.GetSearchStringList(.PropTxtTitle.Text.Trim, SPLIT_MODE_AND)
            '        For i As Integer = 0 To strArray.Length - 1
            '            paramList.Add(New NpgsqlParameter("Title" & i, NpgsqlTypes.NpgsqlDbType.Varchar))
            '            paramList(index).Value = "%" & commonLogicHBK.ChangeStringForSearch(strArray(i)) & "%"
            '            index += 1
            '        Next
            '    End If

            '    ' 内容
            '    If .PropTxtContents.Text.Trim() <> String.Empty Then
            '        Dim strArray() As String = commonLogicHBK.GetSearchStringList(.PropTxtContents.Text.Trim, SPLIT_MODE_AND)
            '        For i As Integer = 0 To strArray.Length - 1
            '            paramList.Add(New NpgsqlParameter("Naiyo" & i, NpgsqlTypes.NpgsqlDbType.Varchar))
            '            paramList(index).Value = "%" & commonLogicHBK.ChangeStringForSearch(strArray(i)) & "%"
            '            index += 1
            '        Next
            '    End If

            '    ' 対象システム
            '    If .PropCmbObjSys.PropCmbColumns.SelectedValue <> String.Empty Then
            '        paramList.Add(New NpgsqlParameter("SystemNmb", NpgsqlTypes.NpgsqlDbType.Integer))
            '        paramList(index).Value = .PropCmbObjSys.PropCmbColumns.SelectedValue
            '        index += 1
            '    End If

            '    ' 対象グループ
            '    If .PropCmbChargeGrp.SelectedValue <> String.Empty Then
            '        paramList.Add(New NpgsqlParameter("GroupCD", NpgsqlTypes.NpgsqlDbType.Varchar))
            '        paramList(index).Value = .PropCmbChargeGrp.SelectedValue
            '        index += 1
            '    End If

            '    ' 登録日From
            '    If .PropDtpRegFrom.txtDate.Text <> String.Empty Then
            '        paramList.Add(New NpgsqlParameter("RegDTFrom", NpgsqlTypes.NpgsqlDbType.Varchar))
            '        paramList(index).Value = .PropDtpRegFrom.txtDate.Text.Replace("/", String.Empty)
            '        index += 1
            '    End If

            '    ' 登録日To
            '    If .PropDtpRegTo.txtDate.Text <> String.Empty Then
            '        paramList.Add(New NpgsqlParameter("RegDTTo", NpgsqlTypes.NpgsqlDbType.Varchar))
            '        paramList(index).Value = .PropDtpRegTo.txtDate.Text.Replace("/", String.Empty)
            '    End If
            'End With

            '' SQL文作成
            'Dim sb As New StringBuilder(strSqlSearch)
            'With dataHBKZ0401
            '    Dim index As Integer = 0

            '    ' プロセス
            '    If .PropCmbProcess.SelectedValue <> String.Empty Then
            '        sb.Append("    AND TB01.ProcessKbn = :").Append(paramList(index).ParameterName)
            '        index += 1
            '    End If

            '    ' 管理番号
            '    If .PropTxtManageNo.Text.Trim() <> String.Empty Then
            '        sb.Append("    AND TB01.MngNmb = :").Append(paramList(index).ParameterName)
            '        index += 1
            '    End If

            '    ' ステータス
            '    If .PropCmbStatus.SelectedValue <> String.Empty Then
            '        sb.Append("    AND TB01.StateCD = :").Append(paramList(index).ParameterName)
            '        index += 1
            '    End If

            '    ' タイトル
            '    If .PropTxtTitle.Text.Trim() <> String.Empty Then
            '        Dim intIndexSize As Integer = commonLogicHBK.GetSearchStringList(.PropTxtTitle.Text.Trim, SPLIT_MODE_AND).Length - 1
            '        For i As Integer = 0 To intIndexSize
            '            sb.Append("    AND TB01.titleaimai LIKE :").Append(paramList(index).ParameterName)
            '            index += 1
            '        Next
            '    End If

            '    ' 内容
            '    If .PropTxtContents.Text.Trim() <> String.Empty Then
            '        Dim intIndexSize As Integer = commonLogicHBK.GetSearchStringList(.PropTxtContents.Text.Trim, SPLIT_MODE_AND).Length - 1
            '        For i As Integer = 0 To intIndexSize
            '            sb.Append("    AND TB01.ukenaiyoaimai LIKE :").Append(paramList(index).ParameterName)
            '            index += 1
            '        Next
            '    End If

            '    ' 対象システム
            '    If .PropCmbObjSys.PropCmbColumns.SelectedValue <> String.Empty Then
            '        sb.Append("    AND TB01.SystemNmb = :").Append(paramList(index).ParameterName)
            '        index += 1
            '    End If

            '    ' 対象グループ
            '    If .PropCmbChargeGrp.SelectedValue <> String.Empty Then
            '        sb.Append("    AND TB01.GroupCD = :").Append(paramList(index).ParameterName)
            '        index += 1
            '    End If

            '    ' 登録日From
            '    If .PropDtpRegFrom.txtDate.Text <> String.Empty Then
            '        sb.Append("    AND TO_CHAR(TB01.RegDT, 'YYYYMMDD') >= :").Append(paramList(index).ParameterName)
            '        index += 1
            '    End If

            '    ' 登録日To
            '    If .PropDtpRegTo.txtDate.Text <> String.Empty Then
            '        sb.Append("    AND TO_CHAR(TB01.RegDT, 'YYYYMMDD') <= :").Append(paramList(index).ParameterName)
            '    End If
            'End With

            'sb.Append(" ORDER BY")
            'sb.Append("    TB01.ProcessKbn")
            'sb.Append("   ,TB01.MngNmb")

            '' SQL文の設定
            'Adapter.SelectCommand = New NpgsqlCommand(sb.ToString(), Cn)

            '' パラメータの設定
            'Adapter.SelectCommand.Parameters.AddRange(paramList.ToArray)
            '[mod] 2012/08/24 y.ikushima END

            ' 終了ログ出力
            CommonLogic.WriteLog(LogLevel.None, "END", Nothing, Nothing)

            Return True
        Catch ex As Exception
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 検索用SQLの作成、実行
    ''' </summary>
    ''' <param name="dataHBKZ0401">[IN/OUT]プロセス一覧検索画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false 異常終了</returns>
    ''' <remarks>プロセス関連テーブルからレコードを取得する
    ''' <para>作成情報：2012/06/13 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Public Function SetSelectProcessCountSql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByVal dataHBKZ0401 As DataHBKZ0401) As Boolean

        ' 開始ログ出力
        CommonLogic.WriteLog(LogLevel.None, "START", Nothing, Nothing)

        Try
            '件数取得SQL設定
            Dim StrSql As String = strSqlSearchCount

            'WHERE句設定
            If SetSelectProcessWhereSql(Adapter, Cn, dataHBKZ0401, StrSql, False) = False Then
                Return False
            End If

            '[mod] 2012/08/24 y.ikushima START
            '' パラメータの作成
            'Dim paramList As New List(Of NpgsqlParameter)
            'With dataHBKZ0401
            '    Dim index As Integer = 0
            '    ' プロセス
            '    If .PropCmbProcess.SelectedValue <> String.Empty Then
            '        paramList.Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))
            '        paramList(index).Value = .PropCmbProcess.SelectedValue
            '        index += 1
            '    End If

            '    ' 管理番号
            '    If .PropTxtManageNo.Text.Trim() <> String.Empty Then
            '        paramList.Add(New NpgsqlParameter("MngNmb", NpgsqlTypes.NpgsqlDbType.Integer))
            '        paramList(index).Value = commonLogicHBK.ChangeStringForSearch(.PropTxtManageNo.Text.Trim)
            '        index += 1
            '    End If

            '    ' ステータス
            '    If .PropCmbStatus.SelectedValue <> String.Empty Then
            '        paramList.Add(New NpgsqlParameter("StateCD", NpgsqlTypes.NpgsqlDbType.Varchar))
            '        paramList(index).Value = .PropCmbStatus.SelectedValue
            '        index += 1
            '    End If

            '    ' タイトル
            '    If .PropTxtTitle.Text.Trim() <> String.Empty Then
            '        Dim strArray() As String = commonLogicHBK.GetSearchStringList(.PropTxtTitle.Text.Trim, SPLIT_MODE_AND)
            '        For i As Integer = 0 To strArray.Length - 1
            '            paramList.Add(New NpgsqlParameter("Title" & i, NpgsqlTypes.NpgsqlDbType.Varchar))
            '            paramList(index).Value = "%" & commonLogicHBK.ChangeStringForSearch(strArray(i)) & "%"
            '            index += 1
            '        Next
            '    End If

            '    ' 内容
            '    If .PropTxtContents.Text.Trim() <> String.Empty Then
            '        Dim strArray() As String = commonLogicHBK.GetSearchStringList(.PropTxtContents.Text.Trim, SPLIT_MODE_AND)
            '        For i As Integer = 0 To strArray.Length - 1
            '            paramList.Add(New NpgsqlParameter("Naiyo" & i, NpgsqlTypes.NpgsqlDbType.Varchar))
            '            paramList(index).Value = "%" & commonLogicHBK.ChangeStringForSearch(strArray(i)) & "%"
            '            index += 1
            '        Next
            '    End If

            '    ' 対象システム
            '    If .PropCmbObjSys.PropCmbColumns.SelectedValue <> String.Empty Then
            '        paramList.Add(New NpgsqlParameter("SystemNmb", NpgsqlTypes.NpgsqlDbType.Integer))
            '        paramList(index).Value = .PropCmbObjSys.PropCmbColumns.SelectedValue
            '        index += 1
            '    End If

            '    ' 対象グループ
            '    If .PropCmbChargeGrp.SelectedValue <> String.Empty Then
            '        paramList.Add(New NpgsqlParameter("GroupCD", NpgsqlTypes.NpgsqlDbType.Varchar))
            '        paramList(index).Value = .PropCmbChargeGrp.SelectedValue
            '        index += 1
            '    End If

            '    ' 登録日From
            '    If .PropDtpRegFrom.txtDate.Text <> String.Empty Then
            '        paramList.Add(New NpgsqlParameter("RegDTFrom", NpgsqlTypes.NpgsqlDbType.Varchar))
            '        paramList(index).Value = .PropDtpRegFrom.txtDate.Text.Replace("/", String.Empty)
            '        index += 1
            '    End If

            '    ' 登録日To
            '    If .PropDtpRegTo.txtDate.Text <> String.Empty Then
            '        paramList.Add(New NpgsqlParameter("RegDTTo", NpgsqlTypes.NpgsqlDbType.Varchar))
            '        paramList(index).Value = .PropDtpRegTo.txtDate.Text.Replace("/", String.Empty)
            '    End If
            'End With

            '' SQL文作成
            'Dim sb As New StringBuilder(strSqlSearchCount)
            'With dataHBKZ0401
            '    Dim index As Integer = 0

            '    ' プロセス
            '    If .PropCmbProcess.SelectedValue <> String.Empty Then
            '        sb.Append("    AND TB01.ProcessKbn = :").Append(paramList(index).ParameterName)
            '        index += 1
            '    End If

            '    ' 管理番号
            '    If .PropTxtManageNo.Text.Trim() <> String.Empty Then
            '        sb.Append("    AND TB01.MngNmb = :").Append(paramList(index).ParameterName)
            '        index += 1
            '    End If

            '    ' ステータス
            '    If .PropCmbStatus.SelectedValue <> String.Empty Then
            '        sb.Append("    AND TB01.StateCD = :").Append(paramList(index).ParameterName)
            '        index += 1
            '    End If

            '    ' タイトル
            '    If .PropTxtTitle.Text.Trim() <> String.Empty Then
            '        Dim intIndexSize As Integer = commonLogicHBK.GetSearchStringList(.PropTxtTitle.Text.Trim, SPLIT_MODE_AND).Length - 1
            '        For i As Integer = 0 To intIndexSize
            '            sb.Append("    AND TB01.titleaimai LIKE :").Append(paramList(index).ParameterName)
            '            index += 1
            '        Next
            '    End If

            '    ' 内容
            '    If .PropTxtContents.Text.Trim() <> String.Empty Then
            '        Dim intIndexSize As Integer = commonLogicHBK.GetSearchStringList(.PropTxtContents.Text.Trim, SPLIT_MODE_AND).Length - 1
            '        For i As Integer = 0 To intIndexSize
            '            sb.Append("    AND TB01.ukenaiyoaimai LIKE :").Append(paramList(index).ParameterName)
            '            index += 1
            '        Next
            '    End If

            '    ' 対象システム
            '    If .PropCmbObjSys.PropCmbColumns.SelectedValue <> String.Empty Then
            '        sb.Append("    AND TB01.SystemNmb = :").Append(paramList(index).ParameterName)
            '        index += 1
            '    End If

            '    ' 対象グループ
            '    If .PropCmbChargeGrp.SelectedValue <> String.Empty Then
            '        sb.Append("    AND TB01.GroupCD = :").Append(paramList(index).ParameterName)
            '        index += 1
            '    End If

            '    ' 登録日From
            '    If .PropDtpRegFrom.txtDate.Text <> String.Empty Then
            '        sb.Append("    AND TO_CHAR(TB01.RegDT, 'YYYYMMDD') >= :").Append(paramList(index).ParameterName)
            '        index += 1
            '    End If

            '    ' 登録日To
            '    If .PropDtpRegTo.txtDate.Text <> String.Empty Then
            '        sb.Append("    AND TO_CHAR(TB01.RegDT, 'YYYYMMDD') <= :").Append(paramList(index).ParameterName)
            '    End If
            'End With

            '' SQL文の設定
            'Adapter.SelectCommand = New NpgsqlCommand(sb.ToString(), Cn)

            '' パラメータの設定
            'Adapter.SelectCommand.Parameters.AddRange(paramList.ToArray)
            '[mod] 2012/08/24 y.ikushima END

            ' 終了ログ出力
            CommonLogic.WriteLog(LogLevel.None, "END", Nothing, Nothing)

            Return True
        Catch ex As Exception
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function


    ''' <summary>
    ''' 検索用SQLのWHERE句作成、実行
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKZ0401">[IN/OUT]プロセス一覧検索画面データクラス</param>
    ''' <param name="StrSQL">[IN/OUT]SQL文</param>
    ''' <param name="blnSearchflg">[IN]検索モードフラグ</param>
    ''' <returns>boolean エラーコード  true 正常終了  false 異常終了</returns>
    ''' <remarks>プロセスリンク情報のWHERE句を設定し、SQLを実行する
    ''' <para>作成情報：2012/08/24 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Public Function SetSelectProcessWhereSql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByVal dataHBKZ0401 As DataHBKZ0401, _
                                                               ByRef StrSQL As String, ByVal blnSearchflg As Boolean) As Boolean

        ' 開始ログ出力
        CommonLogic.WriteLog(LogLevel.None, "START", Nothing, Nothing)
        '変数宣言
        Dim IntMngNmb As Integer = 0

        Try
            With dataHBKZ0401
                'タイトルに入力がある場合、変換
                Dim aryTitle() As String = commonLogicHBK.GetSearchStringList(.PropTxtTitle.Text.Trim, SPLIT_MODE_AND)

                '内容に変更がある場合、変換
                Dim aryNaiyo() As String = commonLogicHBK.GetSearchStringList(.PropTxtContents.Text.Trim, SPLIT_MODE_AND)

                StrSQL &= " ( " & vbCrLf

                'コンボボックスの選択値によってSQLを分岐させる
                If .PropCmbProcess.SelectedValue = PROCESS_TYPE_INCIDENT Then
                    'インシデント選択時
                    If SetSelectProcessIncidentSql(Adapter, Cn, dataHBKZ0401, StrSQL, aryTitle, aryNaiyo) = False Then
                        Return False
                    End If
                ElseIf .PropCmbProcess.SelectedValue = PROCESS_TYPE_QUESTION Then
                    '問題選択時
                    If SetSelectProcessProblemSql(Adapter, Cn, dataHBKZ0401, StrSQL, aryTitle, aryNaiyo) = False Then
                        Return False
                    End If
                ElseIf .PropCmbProcess.SelectedValue = PROCESS_TYPE_CHANGE Then
                    '変更選択時
                    If SetSelectProcessChangeSql(Adapter, Cn, dataHBKZ0401, StrSQL, aryTitle, aryNaiyo) = False Then
                        Return False
                    End If
                ElseIf .PropCmbProcess.SelectedValue = PROCESS_TYPE_RELEASE Then
                    'リリース選択時
                    If SetSelectProcessReleaseSql(Adapter, Cn, dataHBKZ0401, StrSQL, aryTitle, aryNaiyo) = False Then
                        Return False
                    End If
                Else
                    '未選択時(インシデント～リリースのSQL文を設定する)
                    'インシデント
                    If SetSelectProcessIncidentSql(Adapter, Cn, dataHBKZ0401, StrSQL, aryTitle, aryNaiyo) = False Then
                        Return False
                    End If
                    StrSQL &= vbCrLf & " UNION ALL " & vbCrLf
                    '問題
                    If SetSelectProcessProblemSql(Adapter, Cn, dataHBKZ0401, StrSQL, aryTitle, aryNaiyo) = False Then
                        Return False
                    End If
                    StrSQL &= vbCrLf & " UNION ALL " & vbCrLf
                    '変更
                    If SetSelectProcessChangeSql(Adapter, Cn, dataHBKZ0401, StrSQL, aryTitle, aryNaiyo) = False Then
                        Return False
                    End If
                    StrSQL &= vbCrLf & " UNION ALL " & vbCrLf
                    'リリース
                    If SetSelectProcessReleaseSql(Adapter, Cn, dataHBKZ0401, StrSQL, aryTitle, aryNaiyo) = False Then
                        Return False
                    End If
                End If

                StrSQL &= " ) T1 WHERE 1 = 1 " & vbCrLf

                '呼び出し元のプロセス区分、番号が空ではない場合処理を行う
                If dataHBKZ0401.PropStrFromProcessKbn <> "" And dataHBKZ0401.PropStrFromProcessNmb <> "" Then
                    StrSQL &= " AND (T1.ProcessKbn || T1.MngNmb) != :ProcessNmb " & vbCrLf
                End If

                'データ検索時のみ並び順設定
                If blnSearchflg = True Then
                    StrSQL &= " ORDER BY T1.ProcessKbn , T1.MngNmb "
                End If

                'データアダプタに、SQLのSELECT文を設定
                Adapter.SelectCommand = New NpgsqlCommand(StrSQL, Cn)

                '管理番号
                If .PropTxtManageNo.Text.Trim() <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("MngNmb", NpgsqlTypes.NpgsqlDbType.Integer))
                    '文字が入力された際は、0を設定
                    If Integer.TryParse(.PropTxtManageNo.Text.Trim, IntMngNmb) = False Then
                        Adapter.SelectCommand.Parameters("MngNmb").Value = 0
                    Else
                        Adapter.SelectCommand.Parameters("MngNmb").Value = IntMngNmb
                    End If
                End If

                'ステータス
                If .PropCmbStatus.SelectedValue <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("ProcessStateCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("ProcessStateCD").Value = .PropCmbStatus.SelectedValue
                End If

                'タイトル
                If .PropTxtTitle.Text.Trim() <> "" Then
                    'バインド変数を設定
                    For i As Integer = 0 To aryTitle.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("TitleAimai" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("TitleAimai" + i.ToString).Value = "%" & commonLogicHBK.ChangeStringForSearch(aryTitle(i)) & "%"
                    Next
                End If

                '内容
                If .PropTxtContents.Text.Trim() <> "" Then
                    'バインド変数を設定
                    For i As Integer = 0 To aryNaiyo.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("NaiyoAimai" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("NaiyoAimai" + i.ToString).Value = "%" & commonLogicHBK.ChangeStringForSearch(aryNaiyo(i)) & "%"
                    Next
                End If

                '対象システム
                If .PropCmbObjSys.PropCmbColumns.SelectedValue <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SystemNmb", NpgsqlTypes.NpgsqlDbType.Integer))
                    Adapter.SelectCommand.Parameters("SystemNmb").Value = .PropCmbObjSys.PropCmbColumns.SelectedValue
                End If

                '対象グループ
                If .PropCmbChargeGrp.SelectedValue <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("TantoGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("TantoGrpCD").Value = .PropCmbChargeGrp.SelectedValue
                End If

                '登録日From
                If .PropDtpRegFrom.txtDate.Text <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RegDTFrom", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("RegDTFrom").Value = .PropDtpRegFrom.txtDate.Text
                End If

                '登録日To
                If .PropDtpRegTo.txtDate.Text <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RegDTTo", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("RegDTTo").Value = .PropDtpRegFrom.txtDate.Text
                End If

                '呼び出し元のプロセス区分、番号が空ではない場合処理を行う
                If dataHBKZ0401.PropStrFromProcessKbn <> "" And dataHBKZ0401.PropStrFromProcessNmb <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("ProcessNmb", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("ProcessNmb").Value = dataHBKZ0401.PropStrFromProcessKbn & dataHBKZ0401.PropStrFromProcessNmb
                End If
            End With

            ' 終了ログ出力
            CommonLogic.WriteLog(LogLevel.None, "END", Nothing, Nothing)

            Return True
        Catch ex As Exception
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 検索用SQL(インシデント用)の作成
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKZ0401">[IN]プロセス一覧検索画面データクラス</param>
    ''' <param name="StrIncidentSql">[IN/OUT]インシデントプロセス情報検索SQL文</param>
    ''' <param name="aryTitle">[IN]タイトル検索用配列</param>
    ''' <param name="aryNaiyo">[IN]内容検索用配列</param>
    ''' <returns>boolean エラーコード  true 正常終了  false 異常終了</returns>
    ''' <remarks>インシデント共通情報プロセス情報のレコードを取得する
    ''' <para>作成情報：2012/08/24 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectProcessIncidentSql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, _
                                                                ByVal dataHBKZ0401 As DataHBKZ0401, ByRef StrIncidentSql As String, _
                                                                ByVal aryTitle() As String, ByVal aryNaiyo() As String) As Boolean
        ' 開始ログ出力
        CommonLogic.WriteLog(LogLevel.None, "START", Nothing, Nothing)
        Try
            With dataHBKZ0401
                'インシデント検索SQL設定
                StrIncidentSql &= strSqlSearchIncident

                'Where句設定
                StrIncidentSql &= " WHERE " & vbCrLf & _
                                            " ( EXISTS (SELECT DISTINCT IKTG.IncNmb FROM incident_kankei_tb IKTG WHERE " & vbCrLf & _
                                            " IKTG.RelationKbn = '" & KBN_GROUP & "' AND IKTG.RelationID  IN(" & .PropStrLoginUserGrp & ") " & vbCrLf & _
                                            " AND IKTG.IncNmb = IIT.IncNmb) " & vbCrLf & _
                                            " OR EXISTS (SELECT DISTINCT IKTG.IncNmb FROM incident_kankei_tb IKTG " & vbCrLf & _
                                            " WHERE IKTG.RelationKbn = '" & KBN_USER & "' AND " & vbCrLf & _
                                            " IKTG.RelationID = '" & .PropStrLoginUserId & "' AND IKTG.IncNmb = IIT.IncNmb) ) " & vbCrLf

                'インシデント番号
                If .PropTxtManageNo.Text.Trim() <> "" Then
                    StrIncidentSql &= "AND IIT.IncNmb = :MngNmb " & vbCrLf
                End If

                'プロセスステータス
                If .PropCmbStatus.SelectedValue <> "" Then
                    StrIncidentSql &= " AND IIT.ProcessStateCD = :ProcessStateCD " & vbCrLf
                End If

                'タイトル(あいまい検索)
                If .PropTxtTitle.Text.Trim() <> "" Then
                    '分割分だけ検索条件の設定
                    If aryTitle.Length <> 0 Then
                        StrIncidentSql &= " AND "
                        StrIncidentSql &= " ("
                        For intCnt = 0 To aryTitle.Count - 1
                            StrIncidentSql &= " IIT.TitleAimai LIKE :TitleAimai" + intCnt.ToString()
                            If intCnt <> aryTitle.Count - 1 Then
                                StrIncidentSql &= " AND "
                            End If
                        Next
                        StrIncidentSql &= ") " & vbCrLf
                    End If
                End If

                '受付内容(あいまい検索)
                If .PropTxtContents.Text.Trim() <> "" Then
                    '分割分だけ検索条件の設定
                    If aryNaiyo.Length <> 0 Then
                        StrIncidentSql &= " AND "
                        StrIncidentSql &= " ("
                        For intCnt = 0 To aryNaiyo.Count - 1
                            StrIncidentSql &= " IIT.UkeNaiyoAimai LIKE :NaiyoAimai" + intCnt.ToString()
                            If intCnt <> aryNaiyo.Count - 1 Then
                                StrIncidentSql &= " AND "
                            End If
                        Next
                        StrIncidentSql &= ") " & vbCrLf
                    End If
                End If

                '対象システム
                If .PropCmbObjSys.PropCmbColumns.SelectedValue <> "" Then
                    StrIncidentSql &= " AND IIT.SystemNmb = :SystemNmb " & vbCrLf
                End If

                '担当グループ
                If .PropCmbChargeGrp.SelectedValue <> "" Then
                    StrIncidentSql &= " AND IIT.TantoGrpCD = :TantoGrpCD " & vbCrLf
                End If

                ' 登録日From
                If .PropDtpRegFrom.txtDate.Text <> "" Then
                    StrIncidentSql &= " AND TO_CHAR(IIT.RegDT,'YYYY/MM/DD') >= :RegDTFrom" & vbCrLf
                End If

                ' 登録日To
                If .PropDtpRegTo.txtDate.Text <> "" Then
                    StrIncidentSql &= " AND TO_CHAR(IIT.RegDT,'YYYY/MM/DD') <= :RegDTTo" & vbCrLf
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
    ''' 検索用SQL(問題用)の作成
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKZ0401">[IN]プロセス一覧検索画面データクラス</param>
    ''' <param name="StrProblemSql">[IN/OUT]問題プロセス情報検索SQL文</param>
    ''' <param name="aryTitle">[IN]タイトル検索用配列</param>
    ''' <param name="aryNaiyo">[IN]内容検索用配列</param>
    ''' <returns>boolean エラーコード  true 正常終了  false 異常終了</returns>
    ''' <remarks>問題共通情報プロセス情報のレコードを取得する
    ''' <para>作成情報：2012/08/24 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectProcessProblemSql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, _
                                                                ByVal dataHBKZ0401 As DataHBKZ0401, ByRef StrProblemSql As String, _
                                                                ByVal aryTitle() As String, ByVal aryNaiyo() As String) As Boolean
        ' 開始ログ出力
        CommonLogic.WriteLog(LogLevel.None, "START", Nothing, Nothing)
        Try
            With dataHBKZ0401
                '問題検索SQL設定
                StrProblemSql &= strSqlSearchProblem

                'Where句設定
                StrProblemSql &= " WHERE " & vbCrLf & _
                                    " ( EXISTS (SELECT DISTINCT PKTG.PrbNmb FROM problem_kankei_tb PKTG WHERE " & vbCrLf & _
                                    " PKTG.RelationKbn = '" & KBN_GROUP & "' AND PKTG.RelationID  IN(" & .PropStrLoginUserGrp & ") " & vbCrLf & _
                                    " AND PKTG.PrbNmb = PIT.PrbNmb) " & vbCrLf & _
                                    " OR EXISTS (SELECT DISTINCT PKTG.PrbNmb FROM problem_kankei_tb PKTG " & vbCrLf & _
                                    " WHERE PKTG.RelationKbn = '" & KBN_USER & "' AND " & vbCrLf & _
                                    " PKTG.RelationID = '" & .PropStrLoginUserId & "' AND PKTG.PrbNmb = PIT.PrbNmb) ) " & vbCrLf

                '問題番号
                If .PropTxtManageNo.Text.Trim() <> "" Then
                    StrProblemSql &= "AND PIT.PrbNmb = :MngNmb " & vbCrLf
                End If

                'プロセスステータス
                If .PropCmbStatus.SelectedValue <> "" Then
                    StrProblemSql &= " AND PIT.ProcessStateCD = :ProcessStateCD " & vbCrLf
                End If

                'タイトル(あいまい検索)
                If .PropTxtTitle.Text.Trim() <> "" Then
                    '分割分だけ検索条件の設定
                    If aryTitle.Length <> 0 Then
                        StrProblemSql &= " AND "
                        StrProblemSql &= " ("
                        For intCnt = 0 To aryTitle.Count - 1
                            StrProblemSql &= " PIT.TitleAimai LIKE :TitleAimai" + intCnt.ToString()
                            If intCnt <> aryTitle.Count - 1 Then
                                StrProblemSql &= " AND "
                            End If
                        Next
                        StrProblemSql &= ") " & vbCrLf
                    End If
                End If

                '内容(あいまい検索)
                If .PropTxtContents.Text.Trim() <> "" Then
                    '分割分だけ検索条件の設定
                    If aryNaiyo.Length <> 0 Then
                        StrProblemSql &= " AND "
                        StrProblemSql &= " ("
                        For intCnt = 0 To aryNaiyo.Count - 1
                            StrProblemSql &= " PIT.NaiyoAimai LIKE :NaiyoAimai" + intCnt.ToString()
                            If intCnt <> aryNaiyo.Count - 1 Then
                                StrProblemSql &= " AND "
                            End If
                        Next
                        StrProblemSql &= ") " & vbCrLf
                    End If
                End If

                '対象システム
                If .PropCmbObjSys.PropCmbColumns.SelectedValue <> "" Then
                    StrProblemSql &= " AND PIT.SystemNmb = :SystemNmb " & vbCrLf
                End If

                '担当グループ
                If .PropCmbChargeGrp.SelectedValue <> "" Then
                    StrProblemSql &= " AND PIT.TantoGrpCD = :TantoGrpCD " & vbCrLf
                End If

                ' 登録日From
                If .PropDtpRegFrom.txtDate.Text <> "" Then
                    StrProblemSql &= " AND TO_CHAR(PIT.RegDT,'YYYY/MM/DD') >= :RegDTFrom" & vbCrLf
                End If

                ' 登録日To
                If .PropDtpRegTo.txtDate.Text <> "" Then
                    StrProblemSql &= " AND TO_CHAR(PIT.RegDT,'YYYY/MM/DD') <= :RegDTTo" & vbCrLf
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
    ''' 検索用SQL(変更用)の作成
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKZ0401">[IN]プロセス一覧検索画面データクラス</param>
    ''' <param name="StrChangeSql">[IN/OUT]変更プロセス情報検索SQL文</param>
    ''' <param name="aryTitle">[IN]タイトル検索用配列</param>
    ''' <param name="aryNaiyo">[IN]内容検索用配列</param>
    ''' <returns>boolean エラーコード  true 正常終了  false 異常終了</returns>
    ''' <remarks>変更共通情報プロセス情報のレコードを取得する
    ''' <para>作成情報：2012/08/24 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectProcessChangeSql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, _
                                                                ByVal dataHBKZ0401 As DataHBKZ0401, ByRef StrChangeSql As String, _
                                                                ByVal aryTitle() As String, ByVal aryNaiyo() As String) As Boolean
        ' 開始ログ出力
        CommonLogic.WriteLog(LogLevel.None, "START", Nothing, Nothing)
        Try
            With dataHBKZ0401
                '問題検索SQL設定
                StrChangeSql &= strSqlSearchChange

                'Where句設定
                StrChangeSql &= " WHERE " & vbCrLf & _
                                            " ( EXISTS (SELECT DISTINCT CKTG.ChgNmb FROM Change_kankei_tb CKTG WHERE " & vbCrLf & _
                                            " CKTG.RelationKbn = '" & KBN_GROUP & "' AND CKTG.RelationID  IN(" & .PropStrLoginUserGrp & ") " & vbCrLf & _
                                            " AND CKTG.ChgNmb = CIT.ChgNmb) " & vbCrLf & _
                                            " OR EXISTS (SELECT DISTINCT CKTG.ChgNmb FROM Change_kankei_tb CKTG " & vbCrLf & _
                                            " WHERE CKTG.RelationKbn = '" & KBN_USER & "' AND " & vbCrLf & _
                                            " CKTG.RelationID = '" & .PropStrLoginUserId & "' AND CKTG.ChgNmb = CIT.ChgNmb) ) " & vbCrLf

                '変更番号
                If .PropTxtManageNo.Text.Trim() <> "" Then
                    StrChangeSql &= "AND CIT.ChgNmb = :MngNmb " & vbCrLf
                End If

                'プロセスステータス
                If .PropCmbStatus.SelectedValue <> "" Then
                    StrChangeSql &= " AND CIT.ProcessStateCD = :ProcessStateCD " & vbCrLf
                End If

                'タイトル(あいまい検索)
                If .PropTxtTitle.Text.Trim() <> "" Then
                    '分割分だけ検索条件の設定
                    If aryTitle.Length <> 0 Then
                        StrChangeSql &= " AND "
                        StrChangeSql &= " ("
                        For intCnt = 0 To aryTitle.Count - 1
                            StrChangeSql &= " CIT.TitleAimai LIKE :TitleAimai" + intCnt.ToString()
                            If intCnt <> aryTitle.Count - 1 Then
                                StrChangeSql &= " AND "
                            End If
                        Next
                        StrChangeSql &= ") " & vbCrLf
                    End If
                End If

                '内容(あいまい検索)
                If .PropTxtContents.Text.Trim() <> "" Then
                    '分割分だけ検索条件の設定
                    If aryNaiyo.Length <> 0 Then
                        StrChangeSql &= " AND "
                        StrChangeSql &= " ("
                        For intCnt = 0 To aryNaiyo.Count - 1
                            StrChangeSql &= " CIT.NaiyoAimai LIKE :NaiyoAimai" + intCnt.ToString()
                            If intCnt <> aryNaiyo.Count - 1 Then
                                StrChangeSql &= " AND "
                            End If
                        Next
                        StrChangeSql &= ") " & vbCrLf
                    End If
                End If

                '対象システム
                If .PropCmbObjSys.PropCmbColumns.SelectedValue <> "" Then
                    StrChangeSql &= " AND CIT.SystemNmb = :SystemNmb " & vbCrLf
                End If

                '担当グループ
                If .PropCmbChargeGrp.SelectedValue <> "" Then
                    StrChangeSql &= " AND CIT.TantoGrpCD = :TantoGrpCD " & vbCrLf
                End If

                ' 登録日From
                If .PropDtpRegFrom.txtDate.Text <> "" Then
                    StrChangeSql &= " AND TO_CHAR(CIT.RegDT,'YYYY/MM/DD') >= :RegDTFrom" & vbCrLf
                End If

                ' 登録日To
                If .PropDtpRegTo.txtDate.Text <> "" Then
                    StrChangeSql &= " AND TO_CHAR(CIT.RegDT,'YYYY/MM/DD') <= :RegDTTo" & vbCrLf
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
    ''' 検索用SQL(リリース用)の作成
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKZ0401">[IN]プロセス一覧検索画面データクラス</param>
    ''' <param name="StrReleaseSql">[IN/OUT]リリースプロセス情報検索SQL文</param>
    ''' <param name="aryTitle">[IN]タイトル検索用配列</param>
    ''' <param name="aryNaiyo">[IN]内容検索用配列</param>
    ''' <returns>boolean エラーコード  true 正常終了  false 異常終了</returns>
    ''' <remarks>リリース共通情報プロセス情報のレコードを取得する
    ''' <para>作成情報：2012/08/24 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectProcessReleaseSql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, _
                                                                ByVal dataHBKZ0401 As DataHBKZ0401, ByRef StrReleaseSql As String, _
                                                                ByVal aryTitle() As String, ByVal aryNaiyo() As String) As Boolean
        ' 開始ログ出力
        CommonLogic.WriteLog(LogLevel.None, "START", Nothing, Nothing)
        Try
            With dataHBKZ0401
                '問題検索SQL設定
                StrReleaseSql &= strSqlSearchRelease

                'Where句設定
                StrReleaseSql &= " WHERE " & vbCrLf & _
                                            " ( EXISTS (SELECT DISTINCT RKTG.RelNmb FROM release_kankei_tb RKTG WHERE " & vbCrLf & _
                                            " RKTG.RelationKbn = '" & KBN_GROUP & "' AND RKTG.RelationID  IN(" & .PropStrLoginUserGrp & ") " & vbCrLf & _
                                            " AND RKTG.RelNmb = RIT.RelNmb) " & vbCrLf & _
                                            " OR EXISTS (SELECT DISTINCT RKTG.RelNmb FROM release_kankei_tb RKTG  " & vbCrLf & _
                                            " WHERE RKTG.RelationKbn = '" & KBN_USER & "' AND " & vbCrLf & _
                                            " RKTG.RelationID = '" & .PropStrLoginUserId & "' AND RKTG.RelNmb = RIT.RelNmb) ) " & vbCrLf

                'リリース番号
                If .PropTxtManageNo.Text.Trim() <> "" Then
                    StrReleaseSql &= "AND RIT.RelNmb = :MngNmb " & vbCrLf
                End If

                'プロセスステータス
                If .PropCmbStatus.SelectedValue <> "" Then
                    StrReleaseSql &= " AND RIT.ProcessStateCD = :ProcessStateCD " & vbCrLf
                End If

                'タイトル(あいまい検索)
                If .PropTxtTitle.Text.Trim() <> "" Then
                    '分割分だけ検索条件の設定
                    If aryTitle.Length <> 0 Then
                        StrReleaseSql &= " AND "
                        StrReleaseSql &= " ("
                        For intCnt = 0 To aryTitle.Count - 1
                            StrReleaseSql &= " RIT.TitleAimai LIKE :TitleAimai" + intCnt.ToString()
                            If intCnt <> aryTitle.Count - 1 Then
                                StrReleaseSql &= " AND "
                            End If
                        Next
                        StrReleaseSql &= ") " & vbCrLf
                    End If
                End If

                '概要(あいまい検索)
                If .PropTxtContents.Text.Trim() <> "" Then
                    '分割分だけ検索条件の設定
                    If aryNaiyo.Length <> 0 Then
                        StrReleaseSql &= " AND "
                        StrReleaseSql &= " ("
                        For intCnt = 0 To aryNaiyo.Count - 1
                            StrReleaseSql &= " RIT.GaiyoAimai LIKE :NaiyoAimai" + intCnt.ToString()
                            If intCnt <> aryNaiyo.Count - 1 Then
                                StrReleaseSql &= " AND "
                            End If
                        Next
                        StrReleaseSql &= ") " & vbCrLf
                    End If
                End If

                '対象システム
                If .PropCmbObjSys.PropCmbColumns.SelectedValue <> "" Then
                    StrReleaseSql &= " AND RIT.RelNmb IN (SELECT RelNmb FROM release_system_tb WHERE SystemNmb = :SystemNmb ) " & vbCrLf
                End If

                '担当グループ
                If .PropCmbChargeGrp.SelectedValue <> "" Then
                    StrReleaseSql &= " AND RIT.TantoGrpCD = :TantoGrpCD " & vbCrLf
                End If

                ' 登録日From
                If .PropDtpRegFrom.txtDate.Text <> "" Then
                    StrReleaseSql &= " AND TO_CHAR(RIT.RegDT,'YYYY/MM/DD') >= :RegDTFrom" & vbCrLf
                End If

                ' 登録日To
                If .PropDtpRegTo.txtDate.Text <> "" Then
                    StrReleaseSql &= " AND TO_CHAR(RIT.RegDT,'YYYY/MM/DD') <= :RegDTTo" & vbCrLf
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
