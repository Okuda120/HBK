Imports Common
Imports CommonHBK
Imports Npgsql
''' <summary>
''' ソフトマスター登録画面Sqlクラス
''' </summary>
''' <remarks>ソフトマスター登録画面のSQLの作成・設定を行う
''' <para>作成情報：2012/08/30 k.ueda
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKX1001

    'ソフトマスター取得(SELECT)SQL
    Private strSelectSoftMastersql As String = "SELECT " & vbCrLf & _
                                               "sm.SoftCD, " & vbCrLf & _
                                               "sm.SoftKbn, " & vbCrLf & _
                                               "sm.SoftNM, " & vbCrLf & _
                                               "sm.JtiFlg " & vbCrLf & _
                                               "From SOFT_MTB AS sm " & vbCrLf & _
                                               "WHERE sm.SoftCD = :SoftCD "

    'ソフトCD取得用
    Private strSelectEndUsrIDsql As String = "SELECT COUNT(*) " & vbCrLf & _
                                             "FROM SOFT_MTB AS sm " & vbCrLf & _
                                             "WHERE sm.SoftCD = :SoftCD "

    'ソフトマスター新規登録用SQL
    Private strInsertSoftMastersql As String = "INSERT INTO SOFT_MTB ( " & vbCrLf & _
                                                     "SoftCD, " & vbCrLf & _
                                                     "SoftKbn, " & vbCrLf & _
                                                     "SoftNM, " & vbCrLf & _
                                                     "Sort, " & vbCrLf & _
                                                     "JtiFlg, " & vbCrLf & _
                                                     "RegDT, " & vbCrLf & _
                                                     "RegGrpCD, " & vbCrLf & _
                                                     "RegID, " & vbCrLf & _
                                                     "UpdateDT, " & vbCrLf & _
                                                     "UpGrpCD, " & vbCrLf & _
                                                     "UpdateID " & vbCrLf & _
                                                 ") " & vbCrLf & _
                                                 "VALUES ( " & vbCrLf & _
                                                     ":SoftCD, " & vbCrLf & _
                                                     ":SoftKbn, " & vbCrLf & _
                                                     ":SoftNM, " & vbCrLf & _
                                                     ":Sort, " & vbCrLf & _
                                                     ":JtiFlg, " & vbCrLf & _
                                                     ":RegDT, " & vbCrLf & _
                                                     ":RegGrpCD, " & vbCrLf & _
                                                     ":RegID, " & vbCrLf & _
                                                     ":UpdateDT, " & vbCrLf & _
                                                     ":UpGrpCD, " & vbCrLf & _
                                                     ":UpdateID " & vbCrLf & _
                                                 ") "

    'ソフトマスター更新用SQL
    Private strUpdateSoftMastersql As String = "UPDATE SOFT_MTB SET " & vbCrLf & _
                                                "SoftKbn = :SoftKbn, " & vbCrLf & _
                                                "SoftNM = :SoftNM, " & vbCrLf & _
                                                 "UpdateDT = :UpdateDT, " & vbCrLf & _
                                                 "UpGrpCD = :UpGrpCD, " & vbCrLf & _
                                                 "UpdateID = :UpdateID " & vbCrLf & _
                                                 "WHERE SoftCD = :SoftCD"

    'ソフトマスター削除フラグ更新用SQL
    Private strUpdateSoftMasterJtiFlgsql As String = "UPDATE SOFT_MTB SET " & vbCrLf & _
                                                       "JtiFlg = :JtiFlg, " & vbCrLf & _
                                                       "UpdateDT = :UpdateDT, " & vbCrLf & _
                                                       "UpGrpCD = :UpGrpCD, " & vbCrLf & _
                                                       "UpdateID = :UpdateID " & vbCrLf & _
                                                       "WHERE SoftCD = :SoftCD"

    'システム日付取得（SELECT）SQL
    Private strSelectSysDateSql As String = "SELECT Now() AS SysDate "


    ''' <summary>
    ''' ソフトマスター取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX1001">[IN]ソフトマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ソフトマスター取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/30 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSoftMasterSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKX1001 As DataHBKX1001) As Boolean



        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSQL As String

        Try
            '**********************************
            '* SQL文設定
            '**********************************

            'ソフトマスターテーブル取得用SQLを設定
            strSQL = strSelectSoftMastersql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            '**********************************
            '* バインド変数に型と値をセット
            '**********************************

            'ソフトCD
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SoftCD", NpgsqlTypes.NpgsqlDbType.Integer))
            Adapter.SelectCommand.Parameters("SoftCD").Value = dataHBKX1001.PropIntSoftCD


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
    ''' ソフトCD取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX1001">[IN]ソフトマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ソフトCD取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSoftCDSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String



        Try

            'エンドユーザーID取得用SQLを設定
            strSQL = strSelectEndUsrIDsql
            '**********************************
            '* SQL文設定
            '**********************************


            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            'ソフトCD
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SoftCD", NpgsqlTypes.NpgsqlDbType.Integer))
            Adapter.SelectCommand.Parameters("SoftCD").Value = dataHBKX1001.PropTxtSoftCD.Text

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
    ''' サーバー日付取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX1001">[IN]ソフトマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/30 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String

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
    ''' ソフトマスター新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX1001">[IN]ソフトマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ソフトマスター新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertSoftMasterSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String


        Try

            'ソフトマスター新規登録用SQLを設定
            strSQL = strInsertSoftMastersql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            With dataHBKX1001
                'ソフトCD
                Cmd.Parameters.Add(New NpgsqlParameter("SoftCD", NpgsqlTypes.NpgsqlDbType.Integer))
                Cmd.Parameters("SoftCD").Value = .PropTxtSoftCD.Text
                'ソフト区分
                Cmd.Parameters.Add(New NpgsqlParameter("SoftKbn", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("SoftKbn").Value = .PropStrSoftKbn
                'ソフト名称
                Cmd.Parameters.Add(New NpgsqlParameter("SoftNM", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("SoftNM").Value = .PropTxtSoftNM.Text
                '表示順(空白)
                Cmd.Parameters.Add(New NpgsqlParameter("Sort", NpgsqlTypes.NpgsqlDbType.Integer))
                Cmd.Parameters("Sort").Value = DBNull.Value
                '削除フラグ(有効データ)
                Cmd.Parameters.Add(New NpgsqlParameter("JtiFlg", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("JtiFlg").Value = DATA_YUKO
                '登録日時
                Cmd.Parameters.Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))
                Cmd.Parameters("RegDT").Value = .PropDtmSysDate
                '登録者グループCD
                Cmd.Parameters.Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("RegGrpCD").Value = PropWorkGroupCD
                '登録者ID
                Cmd.Parameters.Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("RegID").Value = PropUserId
                '最終更新日時
                Cmd.Parameters.Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))
                Cmd.Parameters("UpdateDT").Value = .PropDtmSysDate
                '最終更新者グループCD
                Cmd.Parameters.Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UpGrpCD").Value = PropWorkGroupCD
                '最終更新者ID
                Cmd.Parameters.Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UpdateID").Value = PropUserId
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
    ''' ソフトマスター編集用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX1001">[IN]ソフトマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ソフトマスター編集用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateSoftMasterSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String

        Try

            'ソフトマスター更新用SQLを設定
            strSQL = strUpdateSoftMastersql
            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            With dataHBKX1001
                'ソフト区分
                Cmd.Parameters.Add(New NpgsqlParameter("SoftKbn", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("SoftKbn").Value = .PropStrSoftKbn
                'ソフト名称
                Cmd.Parameters.Add(New NpgsqlParameter("SoftNM", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("SoftNM").Value = .PropTxtSoftNM.Text
                '最終更新日時
                Cmd.Parameters.Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))
                Cmd.Parameters("UpdateDT").Value = .PropDtmSysDate
                '最終更新者グループCD
                Cmd.Parameters.Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UpGrpCD").Value = PropWorkGroupCD
                '最終更新者ID
                Cmd.Parameters.Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UpdateID").Value = PropUserId
                'ソフトCD
                Cmd.Parameters.Add(New NpgsqlParameter("SoftCD", NpgsqlTypes.NpgsqlDbType.Integer))
                Cmd.Parameters("SoftCD").Value = .PropTxtSoftCD.Text

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
    ''' ソフトマスター削除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX1001">[IN]ソフトマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ソフトマスター削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteSoftMasterSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '変数の宣言
        Dim strSQL As String

        Try

            'ソフトマスター削除用SQLを設定
            strSQL = strUpdateSoftMasterJtiFlgsql


            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            With dataHBKX1001
                '削除フラグ(データ無効)
                Cmd.Parameters.Add(New NpgsqlParameter("JtiFlg", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("JtiFlg").Value = DATA_MUKO
                '最終更新日時
                Cmd.Parameters.Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))
                Cmd.Parameters("UpdateDT").Value = .PropDtmSysDate
                '最終更新者グループCD
                Cmd.Parameters.Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UpGrpCD").Value = PropWorkGroupCD
                '最終更新者ID
                Cmd.Parameters.Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UpdateID").Value = PropUserId
                'ソフトCD
                Cmd.Parameters.Add(New NpgsqlParameter("SoftCD", NpgsqlTypes.NpgsqlDbType.Integer))
                Cmd.Parameters("SoftCD").Value = .PropTxtSoftCD.Text
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
    ''' ソフトマスター削除解除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX1001">[IN]ソフトマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ソフトマスター削除解除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUnDroppingSoftMasterSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String

        Try

            'ソフトマスター削除解除用SQLを設定
            strSQL = strUpdateSoftMasterJtiFlgsql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            With dataHBKX1001
                '削除フラグ(データ有効)
                Cmd.Parameters.Add(New NpgsqlParameter("JtiFlg", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("JtiFlg").Value = DATA_YUKO
                '最終更新日時
                Cmd.Parameters.Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))
                Cmd.Parameters("UpdateDT").Value = .PropDtmSysDate
                '最終更新者グループCD
                Cmd.Parameters.Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UpGrpCD").Value = PropWorkGroupCD
                '最終更新者ID
                Cmd.Parameters.Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UpdateID").Value = PropUserId
                'ソフトCD
                Cmd.Parameters.Add(New NpgsqlParameter("SoftCD", NpgsqlTypes.NpgsqlDbType.Integer))
                Cmd.Parameters("SoftCD").Value = .PropTxtSoftCD.Text
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
