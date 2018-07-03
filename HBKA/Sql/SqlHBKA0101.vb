Imports System.Text
Imports Npgsql
Imports Common
Imports CommonHBK
Imports System.Net

''' <summary>
''' ログイン画面Sqlクラス
''' </summary>
''' <remarks>ログイン画面のSQLの作成・設定を行う
''' <para>作成情報：2012/05/30 matsuoka
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKA0101

    'ユーザーID取得SQL
    Private strSelectHbkUser As String = "SELECT" & _
                                        " HBKUsrID," & _
                                        " HBKUsrNM " & _
                                        "FROM " & _
                                        " HBKUSR_MTB " & _
                                        "WHERE" & _
                                        " JtiFlg <> '1' "

    '所属グループの取得SQL
    Private strSelectGroup As String = "SELECT" & _
                                       " m01.GroupCD," & _
                                       " m02.GroupNM, " & _
                                       " m01.UsrGroupFlg, " & _
                                       " m01.DefaultFlg " & _
                                       "FROM " & _
                                       " SZK_MTB m01, " & _
                                       " GRP_MTB m02 " & _
                                       "WHERE" & _
                                       " m01.GroupCD = m02.GroupCD " & _
                                       " AND m01.JtiFlg <> '1' " & _
                                       " AND m02.JtiFlg <> '1' "

    '所属グループの件数取得SQL
    Private strSelectCountGroup As String = "SELECT" & _
                                            " CASE WHEN sq01.SzkNum = sq02.GrpNum THEN FALSE " & _
                                            "      ELSE TRUE " & _
                                            " END ErrorFlg " & _
                                            "FROM " & _
                                            " ( SELECT Count(*) SzkNum " & _
                                            "   FROM SZK_MTB " & _
                                            "   WHERE HBKUsrID = :setUserID " & _
                                            "     AND JtiFlg <> '1' " & _
                                            " ) sq01 " & _
                                            ",( SELECT Count(*) GrpNum " & _
                                            "   FROM GRP_MTB m01 " & _
                                            "       ,SZK_MTB m02 " & _
                                            "   WHERE m02.HBKUsrID = :setUserID " & _
                                            "     AND m01.GroupCD = m02.GroupCD " & _
                                            "     AND m01.JtiFlg <> '1' " & _
                                            "     AND m02.JtiFlg <> '1' " & _
                                            " ) sq02 "

    '各システム情報の取得SQL
    Private strSelectSystemData As String = "SELECT" & _
                                            " SystemstateFlg , " & _
                                            " SystemConfFlg," & _
                                            " FileStrRootPath," & _
                                            " FileMNGPath," & _
                                            " OutputLogPath, " & _
                                            " UnlockTime, " & _
                                            " SearchMsgCnt, " & _
                                            " LdapFlg, " & _
                                            " LdapPath " & _
                                            "FROM " & _
                                            " SYSTEM_MTB "

    'ログインログデータ書き込みSQL
    Private strInsertLoginLog As String = "INSERT INTO " & _
                                          " LOGIN_LTB " & _
                                          " (HBKUsrID, LogInOutKbn, KindCD, ClientHostNM) " & _
                                          " VALUES " & _
                                          " ( :setUserID " & _
                                          " , :setLoginKbn " & _
                                          " , Now() " & _
                                          " , :setHostName ); "



    ''' <summary>
    ''' システム情報取得SQL作成
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKA0101">[IN]データクラス</param>
    ''' </summary> 
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>システム情報マスタから各種情報の取得用SQLをアダプタに設定する
    ''' <para>作成情報：2012/06/14 matsuoka
    ''' </para></remarks>
    Public Function SetSelectSystemDataSql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByVal dataHBKA0101 As DataHBKA0101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String

        Try
            'SQL文(SELECT)
            strSQL = strSelectSystemData

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            '正常終了
            Return True

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function
    ''' <summary>
    ''' ログインユーザー取得SQL作成
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKA0101">[IN]データクラス</param>
    ''' </summary> 
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ひびきユーザーマスターから該当IDの取得用SQLをアダプタに設定する
    ''' <para>作成情報：2012/06/14 matsuoka
    ''' </para></remarks>
    Public Function SetSelectHbkUserSql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByVal dataHBKA0101 As DataHBKA0101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String
        Dim strWhere As String

        Try
            'SQL文(SELECT)
            strSQL = strSelectHbkUser
            'Where句作成
            strWhere = " AND HBKUsrID = :setUserID "

            'データアダプタに、SQLを設定
            strSQL &= strWhere
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("setUserID", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("setUserID").Value = dataHBKA0101.PropTxtUserId.Text

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            '正常終了
            Return True

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function
    ''' <summary>
    ''' 所属グループ取得SQL作成
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKA0101">[IN]データクラス</param>
    ''' </summary> 
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>所属マスターから該当IDの所属グループを取得するSQLをアダプタに設定する
    ''' <para>作成情報：2012/06/14 matsuoka
    ''' </para></remarks>
    Public Function SetSelectGroupSql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByVal dataHBKA0101 As DataHBKA0101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String
        Dim strWhere As String

        Try
            'SQL文(SELECT)
            strSQL = strSelectGroup
            'Where句作成
            strWhere = " AND HBKUsrID = :setUserID "

            'データアダプタに、SQLを設定
            strSQL &= strWhere & " ORDER BY m01.Sort ASC "
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("setUserID", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("setUserID").Value = dataHBKA0101.PropTxtUserId.Text

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            '正常終了
            Return True

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function
    ''' <summary>
    ''' グループ数取得SQL作成
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKA0101">[IN]データクラス</param>
    ''' </summary> 
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>所属マスターのレコード数とグループ数を取得するSQLをアダプタに設定する
    ''' <para>作成情報：2012/06/14 matsuoka
    ''' </para></remarks>
    Public Function SetSelectCountGroupSql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByVal dataHBKA0101 As DataHBKA0101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String

        Try
            'SQL文(SELECT)
            strSQL = strSelectCountGroup

            'データアダプタに、SQLを設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("setUserID", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("setUserID").Value = dataHBKA0101.PropTxtUserId.Text

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            '正常終了
            Return True

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function
    ''' <summary>
    ''' ログインログを書き込むSQL作成
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' </summary> 
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ログインログテーブルへログ情報を書き込むSQLをアダプタに設定する
    ''' <para>作成情報：2012/06/14 matsuoka
    ''' </para></remarks>
    Public Function SetInsertLoginLogSql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String

        Try
            'SQL文(INSERT)
            strSQL = strInsertLoginLog

            'データアダプタに、SQLを設定
            Adapter.InsertCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            Adapter.InsertCommand.Parameters.Add(New NpgsqlParameter("setUserID", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.InsertCommand.Parameters.Add(New NpgsqlParameter("setLoginKbn", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.InsertCommand.Parameters.Add(New NpgsqlParameter("setHostName", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.InsertCommand.Parameters("setUserID").Value = CommonHBK.CommonDeclareHBK.PropUserId
            Adapter.InsertCommand.Parameters("setLoginKbn").Value = CommonHBK.CommonDeclareHBK.CONNECT_LOGIN_KBN
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
