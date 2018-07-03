Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' イメージマスター登録画面Sqlクラス
''' </summary>
''' <remarks>イメージマスター登録画面のSQLの作成・設定を行う
''' <para>作成情報：2012/09/04 k.ueda
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKX1201

    'イメージマスター取得(SELECT)SQL
    Private strSelectImageMastersql As String = "SELECT " & vbCrLf & _
                                                "im.ImageNmb, " & vbCrLf & _
                                                "im.ImageNM, " & vbCrLf & _
                                                "im.Kind, " & vbCrLf & _
                                                "im.Maker, " & vbCrLf & _
                                                "im.KisyuNM, " & vbCrLf & _
                                                "im.OSNM, " & vbCrLf & _
                                                "im.SP, " & vbCrLf & _
                                                "im.Type, " & vbCrLf & _
                                                "im.Notes, " & vbCrLf & _
                                                "im.JtiFlg " & vbCrLf & _
                                                "FROM IMAGE_MTB AS im " & vbCrLf & _
                                                "WHERE im.ImageNmb = :ImageNmb "

    'イメージマスター新規登録用(INSERT)SQL
    Private strInsertImageMastersql As String = "INSERT INTO IMAGE_MTB ( " & vbCrLf & _
                                                     "ImageNmb, " & vbCrLf & _
                                                     "Kind, " & vbCrLf & _
                                                     "Maker, " & vbCrLf & _
                                                     "KisyuNM, " & vbCrLf & _
                                                     "OSNM, " & vbCrLf & _
                                                     "SP, " & vbCrLf & _
                                                     "Type, " & vbCrLf & _
                                                     "ImageNM, " & vbCrLf & _
                                                     "Notes, " & vbCrLf & _
                                                     "JtiFlg, " & vbCrLf & _
                                                     "Sort, " & vbCrLf & _
                                                     "RegDT, " & vbCrLf & _
                                                     "RegGrpCD, " & vbCrLf & _
                                                     "RegID, " & vbCrLf & _
                                                     "UpdateDT, " & vbCrLf & _
                                                     "UpGrpCD, " & vbCrLf & _
                                                     "UpdateID " & vbCrLf & _
                                                 ") " & vbCrLf & _
                                                 "VALUES ( " & vbCrLf & _
                                                     ":ImageNmb, " & vbCrLf & _
                                                     ":Kind, " & vbCrLf & _
                                                     ":Maker, " & vbCrLf & _
                                                     ":KisyuNM, " & vbCrLf & _
                                                     ":OSNM, " & vbCrLf & _
                                                     ":SP, " & vbCrLf & _
                                                     ":Type, " & vbCrLf & _
                                                     ":ImageNM, " & vbCrLf & _
                                                     ":Notes, " & vbCrLf & _
                                                     ":JtiFlg, " & vbCrLf & _
                                                     ":Sort, " & vbCrLf & _
                                                     ":RegDT, " & vbCrLf & _
                                                     ":RegGrpCD, " & vbCrLf & _
                                                     ":RegID, " & vbCrLf & _
                                                     ":UpdateDT, " & vbCrLf & _
                                                     ":UpGrpCD, " & vbCrLf & _
                                                     ":UpdateID " & vbCrLf & _
                                                 ") "

    'イメージマスター更新用(UPDATE)SQL
    Private strUpdateImageMastersql As String = "UPDATE IMAGE_MTB SET " & vbCrLf & _
                                                "Kind = :Kind, " & vbCrLf & _
                                                "Maker = :Maker, " & vbCrLf & _
                                                "KisyuNM = :KisyuNM, " & vbCrLf & _
                                                "OSNM = :OSNM, " & vbCrLf & _
                                                "SP = :SP,  " & vbCrLf & _
                                                "Type = :Type, " & vbCrLf & _
                                                "ImageNM = :ImageNM, " & vbCrLf & _
                                                "Notes = :Notes, " & vbCrLf & _
                                                "UpdateDT = :UpdateDT, " & vbCrLf & _
                                                "UpGrpCD = :UpGrpCD, " & vbCrLf & _
                                                "UpdateID = :UpdateID " & vbCrLf & _
                                                "WHERE ImageNmb = :ImageNmb"

    'イメージマスター削除フラグ更新用(UPDATE)SQL
    Private strUpdateImageMasterJtiFlgsql As String = "UPDATE IMAGE_MTB SET " & vbCrLf & _
                                                       "JtiFlg = :JtiFlg, " & vbCrLf & _
                                                       "UpdateDT = :UpdateDT, " & vbCrLf & _
                                                       "UpGrpCD = :UpGrpCD, " & vbCrLf & _
                                                       "UpdateID = :UpdateID " & vbCrLf & _
                                                       "WHERE ImageNmb = :ImageNmb"

    'システム日付取得（SELECT）SQL
    Private strSelectSysDateSql As String = "SELECT Now() AS SysDate "

    ''' <summary>
    ''' イメージマスター取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX1201">[IN]イメージマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>イメージマスター取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectImageMasterSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKX1201 As DataHBKX1201) As Boolean



        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSQL As String

        Try
            '**********************************
            '* SQL文設定
            '**********************************

            'イメージマスターテーブル取得用SQLを設定
            strSQL = strSelectImageMastersql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            '**********************************
            '* バインド変数に型と値をセット
            '**********************************

            'イメージ番号
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("ImageNmb", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("ImageNmb").Value = dataHBKX1201.PropStrImageNmb


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
    ''' <param name="dataHBKX1201">[IN]イメージマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKX1201 As DataHBKX1201) As Boolean

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
    ''' 新規イメージ番号取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX1201">[IN]イメージマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>イメージ番号取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/05 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewImageNmbSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                        ByVal Cn As NpgsqlConnection, _
                                                        ByVal dataHBKX1201 As DataHBKX1201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String

        Try

            'SQL文(SELECT)
            strSQL = GET_NEXTVAL_IMAGE_NO

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
    ''' イメージマスター新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX1201">[IN]イメージマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>イメージマスター新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/31 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertImageMasterSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKX1201 As DataHBKX1201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String


        Try

            'イメージマスター新規登録用SQLを設定
            strSQL = strInsertImageMastersql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            With dataHBKX1201
                'イメージ番号
                Cmd.Parameters.Add(New NpgsqlParameter("ImageNmb", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("ImageNmb").Value = .PropStrImageNmb
                '種別
                Cmd.Parameters.Add(New NpgsqlParameter("Kind", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("Kind").Value = .PropTxtKind.Text
                'メーカー
                Cmd.Parameters.Add(New NpgsqlParameter("Maker", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("Maker").Value = .PropTxtMaker.Text
                '機種名
                Cmd.Parameters.Add(New NpgsqlParameter("KisyuNM", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("KisyuNM").Value = .PropTxtKisyuNM.Text
                'OS
                Cmd.Parameters.Add(New NpgsqlParameter("OSNM", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("OSNM").Value = .PropTxtOSNM.Text
                'SP
                Cmd.Parameters.Add(New NpgsqlParameter("SP", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("SP").Value = .PropTxtSP.Text
                'タイプ
                Cmd.Parameters.Add(New NpgsqlParameter("Type", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("Type").Value = .PropTxtType.Text
                'イメージ名称
                Cmd.Parameters.Add(New NpgsqlParameter("ImageNM", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("ImageNM").Value = .PropTxtImageNM.Text
                '注意
                Cmd.Parameters.Add(New NpgsqlParameter("Notes", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("Notes").Value = .PropTxtNotes.Text
                '削除フラグ(有効データ)
                Cmd.Parameters.Add(New NpgsqlParameter("JtiFlg", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("JtiFlg").Value = DATA_YUKO
                '表示順(空白)
                Cmd.Parameters.Add(New NpgsqlParameter("Sort", NpgsqlTypes.NpgsqlDbType.Integer))
                Cmd.Parameters("Sort").Value = DBNull.Value
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
    ''' イメージマスター編集用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX1201">[IN]イメージマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>イメージマスター編集用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateImageMasterSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKX1201 As DataHBKX1201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String

        Try

            'イメージマスター更新用SQLを設定
            strSQL = strUpdateImageMastersql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            With dataHBKX1201
                '種別
                Cmd.Parameters.Add(New NpgsqlParameter("Kind", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("Kind").Value = .PropTxtKind.Text
                'メーカー
                Cmd.Parameters.Add(New NpgsqlParameter("Maker", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("Maker").Value = .PropTxtMaker.Text
                '機種名
                Cmd.Parameters.Add(New NpgsqlParameter("KisyuNM", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("KisyuNM").Value = .PropTxtKisyuNM.Text
                'OS
                Cmd.Parameters.Add(New NpgsqlParameter("OSNM", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("OSNM").Value = .PropTxtOSNM.Text
                'SP
                Cmd.Parameters.Add(New NpgsqlParameter("SP", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("SP").Value = .PropTxtSP.Text
                'タイプ
                Cmd.Parameters.Add(New NpgsqlParameter("Type", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("Type").Value = .PropTxtType.Text
                'イメージ名称
                Cmd.Parameters.Add(New NpgsqlParameter("ImageNM", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("ImageNM").Value = .PropTxtImageNM.Text
                '注意
                Cmd.Parameters.Add(New NpgsqlParameter("Notes", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("Notes").Value = .PropTxtNotes.Text
                '最終更新日時
                Cmd.Parameters.Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))
                Cmd.Parameters("UpdateDT").Value = .PropDtmSysDate
                '最終更新者グループCD
                Cmd.Parameters.Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UpGrpCD").Value = PropWorkGroupCD
                '最終更新者ID
                Cmd.Parameters.Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UpdateID").Value = PropUserId
                'イメージ番号
                Cmd.Parameters.Add(New NpgsqlParameter("ImageNmb", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("ImageNmb").Value = .PropTxtImageNmb.Text
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
    ''' イメージマスター削除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX1201">[IN]イメージマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>イメージマスター削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteImageMasterSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKX1201 As DataHBKX1201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '変数の宣言
        Dim strSQL As String

        Try

            'イメージマスター削除用SQLを設定
            strSQL = strUpdateImageMasterJtiFlgsql


            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            With dataHBKX1201
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
                'イメージ番号
                Cmd.Parameters.Add(New NpgsqlParameter("ImageNmb", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("ImageNmb").Value = .PropTxtImageNmb.Text
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
    ''' イメージマスター削除解除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX1201">[IN]イメージマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>イメージマスター削除解除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUnDroppingImageMasterSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKX1201 As DataHBKX1201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String

        Try

            'イメージマスター削除解除用SQLを設定
            strSQL = strUpdateImageMasterJtiFlgsql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            With dataHBKX1201
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
                'イメージ番号
                Cmd.Parameters.Add(New NpgsqlParameter("ImageNmb", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("ImageNmb").Value = .PropTxtImageNmb.Text
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
