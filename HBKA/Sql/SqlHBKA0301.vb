Imports System.Text
Imports Npgsql
Imports System.Net
Imports Common
Imports CommonHBK

''' <summary>
''' メニュー画面Sqlクラス
''' </summary>
''' <remarks>メニュー画面のSQLの作成・設定を行う
''' <para>作成情報：2012/06/08 matsuoka
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKA0301
    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK

    '定数宣言
    Private Const SEARCH_MODE_COUNT As String = "0" 'カウント

    'ログアウトログ書き込みSQL
    Private strInsertLogoutLog As String = "INSERT INTO " &
                                           " LOGIN_LTB " &
                                           " (HBKUsrID, LogInOutKbn, KindCD, ClientHostNM) " &
                                           " VALUES " &
                                           " ( :setUserID " &
                                           " , :setLoginKbn " &
                                           " , Now() " &
                                           " , :setHostName ); "
    '[SELECT]インシデント件数取得SQL
    Private strSelectIcdCountSql As String = "SELECT" & vbCrLf &
                                          " COUNT(*)" & vbCrLf &
                                          " FROM incident_info_tb AS iit" & vbCrLf &
                                          " LEFT OUTER JOIN incident_kind_mtb AS ikm ON iit.IncKbnCD = ikm.IncKindCD" & vbCrLf &
                                          " LEFT OUTER JOIN processstate_mtb pm ON iit.ProcessStateCD = pm.ProcessStateCD" & vbCrLf &
                                          " LEFT OUTER JOIN ci_info_tb AS cit ON iit.SystemNmb = cit.CINmb" & vbCrLf &
                                          " LEFT OUTER JOIN grp_mtb AS gm ON iit.TantoGrpCD = gm.GroupCD" & vbCrLf &
                                          " LEFT OUTER JOIN hbkusr_mtb AS hm ON iit.IncTantoID = hm.HBKUsrID" & vbCrLf &
                                          " LEFT OUTER JOIN domain_mtb AS dm ON iit.DomainCD = dm.DomainCD" & vbCrLf
    '[SELECT]問題件数取得SQL
    Private strSelectPrbCountSql As String = "SELECT " & vbCrLf &
                                             " COUNT(*) " & vbCrLf &
                                             " FROM " & vbCrLf &
                                             " problem_info_tb PIT " & vbCrLf &
                                             " LEFT OUTER JOIN (SELECT PrbNmb , MIN(WorkSceDT) AS WorkSceDT FROM problem_wk_rireki_tb " & vbCrLf &
                                             " WHERE WorkEdDT IS NULL GROUP BY PrbNmb) PWRT ON PWRT.PrbNmb = PIT.PrbNmb " & vbCrLf &
                                             " LEFT OUTER JOIN ci_info_tb CIT ON CIT.CINmb = PIT.SystemNmb " & vbCrLf &
                                             " LEFT OUTER JOIN processstate_mtb PSM ON PSM.ProcessStateCD = PIT.ProcessStateCD " & vbCrLf
    '[SELECT]変更件数取得SQL
    Private strSelectChgCountSql As String = "SELECT " & vbCrLf &
                                             " COUNT(CIT.ChgNmb) " & vbCrLf &
                                             " FROM change_info_tb AS CIT " & vbCrLf &
                                             " LEFT OUTER JOIN processstate_mtb PM ON CIT.ProcessStateCD = PM.ProcessStateCD " & vbCrLf &
                                             " LEFT OUTER JOIN ci_info_tb AS CI ON CIT.SystemNmb = CI.CINmb " & vbCrLf &
                                             " LEFT OUTER JOIN grp_mtb AS GM ON CIT.TantoGrpCD = GM.GroupCD " & vbCrLf

    '[SELECT]リリース件数取得SQL
    Private strSelectRelCountSql As String = " SELECT " & vbCrLf &
                                             " COUNT(*) " & vbCrLf


    ''' <summary>
    ''' ログアウトログを書き込むSQL作成
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' </summary> 
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ログインログテーブルへログ情報を書き込むSQLをアダプタに設定する
    ''' <para>作成情報：2012/06/14 matsuoka
    ''' </para></remarks>
    Public Function SetInsertLogOutLogSql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String

        Try
            'SQL文(INSERT)
            strSQL = strInsertLogoutLog

            'データアダプタに、SQLを設定
            Adapter.InsertCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            Adapter.InsertCommand.Parameters.Add(New NpgsqlParameter("setUserID", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.InsertCommand.Parameters.Add(New NpgsqlParameter("setLoginKbn", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.InsertCommand.Parameters.Add(New NpgsqlParameter("setHostName", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.InsertCommand.Parameters("setUserID").Value = CommonHBK.CommonDeclareHBK.PropUserId
            Adapter.InsertCommand.Parameters("setLoginKbn").Value = CommonHBK.CommonDeclareHBK.CONNECT_LOGOUT_KBN
            Adapter.InsertCommand.Parameters("setHostName").Value = Dns.GetHostName()

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            '正常終了
            Return True

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.InsertCommand)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' インシデント件数取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKA0301">[IN]メニュー画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>インシデント件数取得用のSQLを作成し、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2018/08/29 e.okuda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetResultIncidentCountSql(ByRef Adapter As NpgsqlDataAdapter,
                                      ByVal Cn As NpgsqlConnection,
                                      ByVal dataHBKA0301 As DataHBKA0301) As Boolean

        'インシデント件数取得SQLを設定
        Dim strSql As String = strSelectIcdCountSql

        Try

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            '検索条件作成
            If CreateIncidentInfoSql(Adapter, Cn, dataHBKA0301, strSql, SEARCH_MODE_COUNT) = False Then
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
    ''' インシデントSQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKA0301">[IN]メニュー画面データクラス</param>
    ''' <param name="strSql">[IN]基本SQL文</param>
    ''' <param name="strSearchMode">[IN]Sql判別モード</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>パラメータとして受け取ったSQL文をもとにSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2017/08/29 e.okuda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function CreateIncidentInfoSql(ByRef Adapter As NpgsqlDataAdapter,
                                           ByVal Cn As NpgsqlConnection,
                                           ByVal dataHBKA0301 As DataHBKA0301,
                                           ByVal strSql As String,
                                           ByVal strSearchMode As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSelect As String = ""                    'SELECT文
        Dim strSearch As String = ""                    '検索条件

        Try

            With dataHBKA0301

                strSelect = strSql

                '***************************************************************************************************************
                'サンプル
                'WHERE it.IncNmb in (SELECT ki.IncNmb FROM incident_kiki_tb ki WHERE ki.KindCD = :KindCD AND ki.Num = :Num Group by ki.IncNmb)
                '***************************************************************************************************************

                '前提条件
                strSearch &= " WHERE iit.IncNmb IN (" & vbCrLf &
                             "                      SELECT ikant.IncNmb FROM incident_kankei_tb AS ikant" & vbCrLf &
                             "                      WHERE (ikant.RelationKbn = '" & KBN_GROUP & "' AND ikant.RelationID IN(" & .PropStrLoginUserGrp & "))" & vbCrLf &
                             "                      OR (ikant.RelationKbn = '" & KBN_USER & "' AND ikant.RelationID = '" & .PropStrLoginUserId & "')" & vbCrLf &
                             "                      GROUP BY ikant.IncNmb" & vbCrLf &
                             "                     )"

                '入力管理番号条件追加
                strSearch &= " AND iit.IncNmb = :MngNmb"

                '検索条件セット
                strSelect &= strSearch

                'データアダプタに、SQLのSELECT文を設定
                Adapter.SelectCommand = New NpgsqlCommand(strSelect, Cn)
                '+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-

                ' パラメータに管理番号を設定
                Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("MngNmb", NpgsqlTypes.NpgsqlDbType.Integer))
                Adapter.SelectCommand.Parameters("MngNmb").Value = .PropIntMngNum

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
    ''' 問題件数取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKA0301">[IN]メニュー画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>問題件数取得用のSQLを作成し、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2018/08/29 e.okuda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetResultProblemCountSql(ByRef Adapter As NpgsqlDataAdapter,
                                      ByVal Cn As NpgsqlConnection,
                                      ByVal dataHBKA0301 As DataHBKA0301) As Boolean

        '問題件数取得SQLを設定
        Dim strSql As String = strSelectPrbCountSql

        Try

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            '検索条件作成
            If CreateProblemInfoSql(Adapter, Cn, dataHBKA0301, strSql, SEARCH_MODE_COUNT) = False Then
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
    ''' 問題SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKA0301">[IN]メニュー画面データクラス</param>
    ''' <param name="strSql">[IN]基本SQL文</param>
    ''' <param name="strSearchMode">[IN]Sql判別モード</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>パラメータとして受け取ったSQL文をもとにSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2017/08/29 e.okuda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>

    Private Function CreateProblemInfoSql(ByRef Adapter As NpgsqlDataAdapter,
                                           ByVal Cn As NpgsqlConnection,
                                           ByVal dataHBKA0301 As DataHBKA0301,
                                           ByVal strSql As String,
                                           ByVal strSearchMode As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSelect As String = ""                    'SELECT文
        Dim strSearch As String = ""                    '検索条件

        Try

            With dataHBKA0301

                strSelect = strSql

                '前提条件
                strSearch &= " WHERE " & vbCrLf &
                                    " ( EXISTS (SELECT DISTINCT PKTG.PrbNmb FROM problem_kankei_tb PKTG WHERE " & vbCrLf &
                                    " PKTG.RelationKbn = '" & KBN_GROUP & "' AND PKTG.RelationID  IN(" & .PropStrLoginUserGrp & ") " & vbCrLf &
                                    " AND PKTG.PrbNmb = PIT.PrbNmb) " & vbCrLf &
                                    " OR EXISTS (SELECT DISTINCT PKTG.PrbNmb FROM problem_kankei_tb PKTG " & vbCrLf &
                                    " WHERE PKTG.RelationKbn = '" & KBN_USER & "' AND " & vbCrLf &
                                    " PKTG.RelationID = '" & .PropStrLoginUserId & "' AND PKTG.PrbNmb = PIT.PrbNmb) ) " & vbCrLf

                '入力管理番号条件追加
                strSearch &= " AND PIT.PrbNmb = :MngNmb " & vbCrLf

                '検索条件セット
                strSelect &= strSearch

                'データアダプタに、SQLのSELECT文を設定
                Adapter.SelectCommand = New NpgsqlCommand(strSelect, Cn)
                '+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-

                ' パラメータに管理番号を設定
                Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("MngNmb", NpgsqlTypes.NpgsqlDbType.Integer))
                Adapter.SelectCommand.Parameters("MngNmb").Value = .PropIntMngNum

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
    ''' 変更件数取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKA0301">[IN]メニュー画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>変更検索件数取得用のSQLを作成し、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2018/08/29 e.okuda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetResultChangeCountSql(ByRef Adapter As NpgsqlDataAdapter,
                                      ByVal Cn As NpgsqlConnection,
                                      ByVal dataHBKA0301 As DataHBKA0301) As Boolean

        '変更件数取得SQLを設定
        Dim strSql As String = strSelectChgCountSql

        Try

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            '検索条件作成
            If CreateChangeInfoSql(Adapter, Cn, dataHBKA0301, strSql, SEARCH_MODE_COUNT) = False Then
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
    ''' 変更SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKA0301">[IN]メニュー画面データクラス</param>
    ''' <param name="strSql">[IN]基本SQL文</param>
    ''' <param name="strSearchMode">[IN]Sql判別モード</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>パラメータとして受け取ったSQL文をもとにSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2017/08/29 e.okuda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function CreateChangeInfoSql(ByRef Adapter As NpgsqlDataAdapter,
                                           ByVal Cn As NpgsqlConnection,
                                           ByVal dataHBKA0301 As DataHBKA0301,
                                           ByVal strSql As String,
                                           ByVal strSearchMode As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSelect As String = ""                    'SELECT文
        Dim strSearch As String = ""                    '検索条件

        Try

            With dataHBKA0301

                strSelect = strSql

                '前提条件
                strSearch &= " WHERE " & vbCrLf &
                             " ( EXISTS (SELECT DISTINCT CKTG.ChgNmb FROM Change_kankei_tb CKTG WHERE " & vbCrLf &
                             " CKTG.RelationKbn = '" & KBN_GROUP & "' AND CKTG.RelationID  IN(" & .PropStrLoginUserGrp & ") " & vbCrLf &
                             " AND CKTG.ChgNmb = CIT.ChgNmb) " & vbCrLf &
                             " OR EXISTS (SELECT DISTINCT CKTG.ChgNmb FROM Change_kankei_tb CKTG " & vbCrLf &
                             " WHERE CKTG.RelationKbn = '" & KBN_USER & "' AND " & vbCrLf &
                             " CKTG.RelationID = '" & .PropStrLoginUserId & "' AND CKTG.ChgNmb = CIT.ChgNmb) ) " & vbCrLf

                '入力管理番号条件追加
                strSearch &= " AND cit.ChgNmb = :MngNmb" & vbCrLf

                '検索条件セット
                strSelect &= strSearch

                'データアダプタに、SQLのSELECT文を設定
                Adapter.SelectCommand = New NpgsqlCommand(strSelect, Cn)
                '+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-

                ' パラメータに管理番号を設定
                Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("MngNmb", NpgsqlTypes.NpgsqlDbType.Integer))
                Adapter.SelectCommand.Parameters("MngNmb").Value = .PropIntMngNum

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
    ''' リリース件数取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKA0301">[IN]メニュー画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>リリース検索件数取得用のSQLを作成し、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2018/08/29 e.okuda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetResultReleaseCountSql(ByRef Adapter As NpgsqlDataAdapter,
                                      ByVal Cn As NpgsqlConnection,
                                      ByVal dataHBKA0301 As DataHBKA0301) As Boolean

        'リリース件数取得SQLを設定
        Dim strSql As String = strSelectRelCountSql

        Try

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            '検索条件作成
            If CreateReleaseInfoSql(Adapter, Cn, dataHBKA0301, strSql, SEARCH_MODE_COUNT) = False Then
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
    ''' リリースSQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKA0301">[IN]メニュー画面データクラス</param>
    ''' <param name="strSql">[IN]基本SQL文</param>
    ''' <param name="strSearchMode">[IN]Sql判別モード</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>パラメータとして受け取ったSQL文をもとにSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2017/08/29 e.okuda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function CreateReleaseInfoSql(ByRef Adapter As NpgsqlDataAdapter,
                                           ByVal Cn As NpgsqlConnection,
                                           ByVal dataHBKA0301 As DataHBKA0301,
                                           ByVal strSql As String,
                                           ByVal strSearchMode As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSelect As String = ""                    'SELECT文
        Dim strSearch As String = ""                    '検索条件

        Try

            With dataHBKA0301

                strSelect = strSql

                '前提条件
                'FROM句の設定
                strSearch = " FROM release_info_tb RIT " & vbCrLf &
                               " LEFT OUTER JOIN processstate_mtb PSM " & vbCrLf &
                               " ON PSM.ProcessStateCD = RIT.ProcessStateCD AND PSM.ProcessKbn = '" & PROCESS_TYPE_RELEASE & "' " & vbCrLf
                'WHERE句の設定
                strSearch &= " WHERE " & vbCrLf &
                                    " ( EXISTS (SELECT DISTINCT RKTG.RelNmb FROM release_kankei_tb RKTG WHERE  " & vbCrLf &
                                    " RKTG.RelationKbn = '" & KBN_GROUP & "' AND RKTG.RelationID  IN(" & .PropStrLoginUserGrp & ") " & vbCrLf &
                                    " AND RKTG.RelNmb = RIT.RelNmb) " & vbCrLf &
                                    " OR EXISTS (SELECT DISTINCT RKTG.RelNmb FROM release_kankei_tb RKTG  " & vbCrLf &
                                    " WHERE RKTG.RelationKbn = '" & KBN_USER & "' AND " & vbCrLf &
                                    " RKTG.RelationID = '" & .PropStrLoginUserId & "' AND RKTG.RelNmb = RIT.RelNmb) ) " & vbCrLf

                '入力管理番号条件追加
                strSearch &= " AND RIT.RelNmb = :MngNmb " & vbCrLf

                '検索条件セット
                strSelect &= strSearch

                'データアダプタに、SQLのSELECT文を設定
                Adapter.SelectCommand = New NpgsqlCommand(strSelect, Cn)
                '+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-

                ' パラメータに管理番号を設定
                Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("MngNmb", NpgsqlTypes.NpgsqlDbType.Integer))
                Adapter.SelectCommand.Parameters("MngNmb").Value = .PropIntMngNum

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
