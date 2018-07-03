Imports Npgsql
Imports Common
Imports CommonHBK
Imports System.Net

''' <summary>
''' 特権ユーザーログイン（エンドユーザ検索）画面Sqlクラス
''' </summary>
''' <remarks>特権ユーザーログイン（エンドユーザ検索）画面のSQLの作成・設定を行う
''' <para>作成情報：2012/08/30 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKX0102

    '特権ユーザーマスター（エンドユーザ検索）取得SQL
    Private strSelectSuperUser As String = "SELECT" & _
                                            " sm.SuperUsrID " & _
                                            ",sm.Password " & _
                                            "FROM superusr_mtb sm " & _
                                            "WHERE sm.JtiFlg <> '1' " & _
                                            " AND sm.EndUsrModFlg = '1' " & _
                                            " AND sm.SuperUsrID = :SuperUsrID " & _
                                            " AND sm.Password = HBKF0012(:Password, :EncryptPass) "

    '特権ログインログデータ書き込みSQL
    Private strInsertSuperLoginLog As String = "INSERT INTO SUPERLOGIN_LTB (" & _
                                            " SuperUsrID " & _
                                            ",HBKUsrID " & _
                                            ",LogInOutKbn " & _
                                            ",KindCD " & _
                                            ",ClientHostNM " & _
                                            ",ProgramID ) " & _
                                            " VALUES (" & _
                                            " :SuperUsrID " & _
                                            ",:HBKUsrID " & _
                                            ",:LogInOutKbn " & _
                                            ",Now() " & _
                                            ",:ClientHostNM " & _
                                            ",:ProgramID ) "

    ''' <summary>
    ''' 特権ユーザーマスター（エンドユーザ検索）取得SQL作成
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0102">[IN]特権ユーザーログイン（エンドユーザ検索）画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>特権ユーザーマスターから該当IDの取得用SQLをアダプタに設定する
    ''' <para>作成情報：2012/08/30 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSuperUserSql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByVal dataHBKX0102 As DataHBKX0102) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String

        Try
            'SQL文(SELECT)
            strSQL = strSelectSuperUser

            'データアダプタに、SQLを設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SuperUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("Password", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("EncryptPass", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("SuperUsrID").Value = dataHBKX0102.PropTxtUserId.Text
            Adapter.SelectCommand.Parameters("Password").Value = dataHBKX0102.PropTxtPassword.Text
            Adapter.SelectCommand.Parameters("EncryptPass").Value = ENCRYPT_PASSWORD

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
    ''' 特権ログインログを書き込むSQL作成
    ''' <param name="Adapter">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0102">[IN]特権ユーザーログイン（エンドユーザ検索）画面データクラス</param>
    ''' </summary> 
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>特権ログインログテーブルへログ情報を書き込むSQLをアダプタに設定する
    ''' <para>作成情報：2012/08/30 k.imayama
    ''' </para></remarks>
    Public Function SetInsertSuperLoginLogSql(ByRef Cmd As NpgsqlCommand, ByVal Cn As NpgsqlConnection, ByVal dataHBKX0102 As DataHBKX0102) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String

        Try
            'SQL文(INSERT)
            strSQL = strInsertSuperLoginLog

            'データアダプタに、SQLを設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("SuperUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))
                .Add(New NpgsqlParameter("HBKUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))
                .Add(New NpgsqlParameter("LogInOutKbn", NpgsqlTypes.NpgsqlDbType.Varchar))
                .Add(New NpgsqlParameter("ClientHostNM", NpgsqlTypes.NpgsqlDbType.Varchar))
                .Add(New NpgsqlParameter("ProgramID", NpgsqlTypes.NpgsqlDbType.Varchar))
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("SuperUsrID").Value = dataHBKX0102.PropTxtUserId.Text
                .Parameters("HBKUsrID").Value = CommonHBK.CommonDeclareHBK.PropUserId
                .Parameters("LogInOutKbn").Value = dataHBKX0102.PropStrLogInOutKbn
                .Parameters("ClientHostNM").Value = Dns.GetHostName()
                .Parameters("ProgramID").Value = dataHBKX0102.PropStrProgramID
            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

End Class
