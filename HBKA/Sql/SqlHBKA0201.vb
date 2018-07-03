Imports Npgsql
Imports System.Text
Imports Common
Imports CommonHBK
Imports System.Net

''' <summary>
''' グループ選択画面Sqlクラス
''' </summary>
''' <remarks>グループ選択画面のSQLの作成・設定を行う
''' <para>作成情報：2012/05/30 matsuoka
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKA0201

    'ログアウトログ書き込みSQL
    Private strInsertLogoutLog As String = "INSERT INTO " & _
                                           " LOGIN_LTB " & _
                                           " (HBKUsrID, LogInOutKbn, KindCD, ClientHostNM) " & _
                                           " VALUES " & _
                                           " ( :setUserID " & _
                                           " , :setLoginKbn " & _
                                           " , Now() " & _
                                           " , :setHostName ); "

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

End Class
