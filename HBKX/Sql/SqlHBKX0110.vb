Imports Npgsql
Imports Common
Imports CommonHBK
''' <summary>
''' 特権ユーザパスワード変更画面Sqlクラス
''' </summary>
''' <remarks>特権ユーザパスワード変更画面のSQLの作成・設定を行う
''' <para>作成情報：2012/08/30 y.ikushima
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKX0110

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK

    '特権ユーザ存在チェック用SQL
    Private strSelectSuperUserSql As String = " SELECT COUNT(*) FROM superusr_mtb WHERE SuperUsrID = :SuperUsrID AND Password = HBKF0012(:Password, :EncryptPass) "

    '特権ユーザ更新SQL
    Private strUpdateSuprUserSql As String = " UPDATE superusr_mtb SET" & vbCrLf & _
                                                             " Password = HBKF0012(:PasswordNew, :EncryptPass) , " & vbCrLf & _
                                                             " UpdateDT = :UpdateDT, " & vbCrLf & _
                                                             " UpGrpCD = :UpGrpCD, " & vbCrLf & _
                                                             " UpdateID = :UpdateID " & vbCrLf & _
                                                             " WHERE SuperUsrID = :SuperUsrID AND Password = HBKF0012(:PasswordNow, :EncryptPass) "
    'システム日付取得用
    Private strSelectSysDateSql As String = "SELECT Now() AS SysDate "

    ''' <summary>
    ''' 特権ユーザ件数取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0110">[IN/OUT]特権ユーザパスワード変更画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>特権ユーザマスタを検索するSQLをセットする
    ''' <para>作成情報：2012/08/30 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSuperUserSql(ByRef Adapter As NpgsqlDataAdapter, _
                                      ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKX0110 As DataHBKX0110) As Boolean

        '変数宣言
        Dim strSql As String

        Try

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            ''特権ユーザ存在チェック用のSQLを設定
            strSql = strSelectSuperUserSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSql, Cn)

            With DataHBKX0110
                'ユーザーID
                Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SuperUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))
                Adapter.SelectCommand.Parameters("SuperUsrID").Value = .PropTxtID.Text
                'パスワード
                Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("Password", NpgsqlTypes.NpgsqlDbType.Varchar))
                Adapter.SelectCommand.Parameters("Password").Value = .PropTxtPassNow.Text
                '暗号化／復号化パスワード
                Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("EncryptPass", NpgsqlTypes.NpgsqlDbType.Varchar))
                Adapter.SelectCommand.Parameters("EncryptPass").Value = ENCRYPT_PASSWORD
            End With

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
    ''' サーバー日付取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0110">[IN/OUT]特権ユーザパスワード変更画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/30 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKX0110 As DataHBKX0110) As Boolean

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
    ''' 特権ユーザ更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0110">[IN/OUT]特権ユーザパスワード変更画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>特権ユーザ更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/30 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateSuprUserSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKX0110 As DataHBKX0110) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String

        Try

            'エンドユーザーマスター削除解除用SQLを設定
            strSQL = strUpdateSuprUserSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            With dataHBKX0110
                '新しいパスワード
                Cmd.Parameters.Add(New NpgsqlParameter("PasswordNew", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("PasswordNew").Value = .PropTxtPassNew.Text
                '暗号化／復号化パスワード
                Cmd.Parameters.Add(New NpgsqlParameter("EncryptPass", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("EncryptPass").Value = ENCRYPT_PASSWORD
                '最終更新日時
                Cmd.Parameters.Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))
                Cmd.Parameters("UpdateDT").Value = .PropDtmSysDate
                '最終更新者グループCD
                Cmd.Parameters.Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UpGrpCD").Value = PropWorkGroupCD
                '最終更新者ID
                Cmd.Parameters.Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UpdateID").Value = PropUserId
                'ユーザID
                Cmd.Parameters.Add(New NpgsqlParameter("SuperUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("SuperUsrID").Value = .PropTxtID.Text
                '現在のパスワード
                Cmd.Parameters.Add(New NpgsqlParameter("PasswordNow", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("PasswordNow").Value = .PropTxtPassNow.Text
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
