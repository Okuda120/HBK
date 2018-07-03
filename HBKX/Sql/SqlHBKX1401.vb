Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' 設置情報マスター登録画面Sqlクラス
''' </summary>
''' <remarks>設置情報マスター登録画面のSQLの作成・設定を行う
''' <para>作成情報：2012/09/05 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKX1401

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK

    '設置情報マスター取得(SELECT)SQL
    Private strSelectSetPosMastersql As String = "SELECT " & vbCrLf & _
                                                "sm.SetBusyoCD, " & vbCrLf & _
                                                "sm.SetKyokuNM, " & vbCrLf & _
                                                "sm.SetBusyoNM, " & vbCrLf & _
                                                "sm.SetRoom, " & vbCrLf & _
                                                "sm.SetBuil, " & vbCrLf & _
                                                "sm.SetFloor, " & vbCrLf & _
                                                "sm.JtiFlg " & vbCrLf & _
                                                "From SETPOS_MTB AS sm " & vbCrLf & _
                                                "WHERE sm.SetBusyoCD = :SetBusyoCD "

    '設置情報マスター新規登録用SQL
    Private strInsertSetPosMastersql As String = "INSERT INTO SETPOS_MTB ( " & vbCrLf & _
                                                "SetBusyoCD, " & vbCrLf & _
                                                "SetKyokuNM, " & vbCrLf & _
                                                "SetBusyoNM, " & vbCrLf & _
                                                "SetRoom, " & vbCrLf & _
                                                "SetBuil, " & vbCrLf & _
                                                "SetFloor, " & vbCrLf & _
                                                "SetKyokuNMAimai, " & vbCrLf & _
                                                "SetBusyoNMAimai, " & vbCrLf & _
                                                "SetRoomAimai, " & vbCrLf & _
                                                "SetBuilAimai, " & vbCrLf & _
                                                "SetFloorAimai, " & vbCrLf & _
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
                                                ":SetBusyoCD, " & vbCrLf & _
                                                ":SetKyokuNM, " & vbCrLf & _
                                                ":SetBusyoNM, " & vbCrLf & _
                                                ":SetRoom, " & vbCrLf & _
                                                ":SetBuil, " & vbCrLf & _
                                                ":SetFloor, " & vbCrLf & _
                                                ":SetKyokuNMAimai, " & vbCrLf & _
                                                ":SetBusyoNMAimai, " & vbCrLf & _
                                                ":SetRoomAimai, " & vbCrLf & _
                                                ":SetBuilAimai, " & vbCrLf & _
                                                ":SetFloorAimai, " & vbCrLf & _
                                                ":Sort, " & vbCrLf & _
                                                ":JtiFlg, " & vbCrLf & _
                                                ":RegDT, " & vbCrLf & _
                                                ":RegGrpCD, " & vbCrLf & _
                                                ":RegID, " & vbCrLf & _
                                                ":UpdateDT, " & vbCrLf & _
                                                ":UpGrpCD, " & vbCrLf & _
                                                ":UpdateID " & vbCrLf & _
                                                ") "

    '設置情報マスター更新用SQL
    Private strUpdateSetPosMastersql As String = "UPDATE SETPOS_MTB SET " & vbCrLf & _
                                                "SetKyokuNM = :SetKyokuNM, " & vbCrLf & _
                                                "SetBusyoNM = :SetBusyoNM, " & vbCrLf & _
                                                "SetRoom = :SetRoom, " & vbCrLf & _
                                                "SetBuil = :SetBuil, " & vbCrLf & _
                                                "SetFloor = :SetFloor, " & vbCrLf & _
                                                "SetKyokuNMAimai = :SetKyokuNMAimai, " & vbCrLf & _
                                                "SetBusyoNMAimai = :SetBusyoNMAimai, " & vbCrLf & _
                                                "SetRoomAimai = :SetRoomAimai, " & vbCrLf & _
                                                "SetBuilAimai = :SetBuilAimai, " & vbCrLf & _
                                                "SetFloorAimai = :SetFloorAimai, " & vbCrLf & _
                                                "UpdateDT = :UpdateDT, " & vbCrLf & _
                                                "UpGrpCD = :UpGrpCD, " & vbCrLf & _
                                                "UpdateID = :UpdateID " & vbCrLf & _
                                                "WHERE SetBusyoCD = :SetBusyoCD"

    '設置情報マスター削除フラグ更新用SQL
    Private strUpdateSetPosMasterJtiFlgsql As String = "UPDATE SETPOS_MTB SET " & vbCrLf & _
                                                        "JtiFlg = :JtiFlg, " & vbCrLf & _
                                                        "UpdateDT = :UpdateDT, " & vbCrLf & _
                                                        "UpGrpCD = :UpGrpCD, " & vbCrLf & _
                                                        "UpdateID = :UpdateID " & vbCrLf & _
                                                        "WHERE SetBusyoCD = :SetBusyoCD"

    'システム日付取得（SELECT）SQL
    Private strSelectSysDateSql As String = "SELECT Now() AS SysDate "

    ''' <summary>
    ''' 設置情報マスター取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX1401">[IN]設置情報マスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>設置情報マスター取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSetPosMasterSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKX1401 As DataHBKX1401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSQL As String

        Try
            '設置情報マスターテーブル取得用SQLを設定
            strSQL = strSelectSetPosMastersql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SetBusyoCD", NpgsqlTypes.NpgsqlDbType.Integer))
            Adapter.SelectCommand.Parameters("SetBusyoCD").Value = dataHBKX1401.PropIntSetBusyoCD

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
    ''' 【新規登録モード】新規設置所属コード、サーバー日付取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX1401">[IN]設置情報マスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>設置所属コード、サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewSetBusyoCDAndSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                        ByVal Cn As NpgsqlConnection, _
                                                        ByVal dataHBKX1401 As DataHBKX1401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            'SQL文(SELECT)
            strSQL = GET_NEXTVAL_SETBUSYO_CD

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
    ''' サーバー日付取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX1401">[IN]設置情報マスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKX1401 As DataHBKX1401) As Boolean

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
    ''' 設置情報マスター新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX1401">[IN]設置情報マスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>設置情報マスター新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertSoftMasterSql(ByRef Cmd As NpgsqlCommand, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKX1401 As DataHBKX1401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String
        Dim strSetKyokuNMAimai As String = ""       '局名（あいまい）
        Dim strSetBusyoNMAimai As String = ""       '部署名（あいまい）
        Dim strSetRoomAimai As String = ""          '番組/部屋名（あいまい）
        Dim strSetBuilAimai As String = ""          '建物（あいまい）
        Dim strSetFloorAimai As String = ""         'フロア（あいまい）

        Try
            '設置情報マスター新規登録用SQLを設定
            strSQL = strInsertSetPosMastersql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("SetBusyoCD", NpgsqlTypes.NpgsqlDbType.Integer))       '設置部署CD
                .Add(New NpgsqlParameter("SetKyokuNM", NpgsqlTypes.NpgsqlDbType.Varchar))       '局名
                .Add(New NpgsqlParameter("SetBusyoNM", NpgsqlTypes.NpgsqlDbType.Varchar))       '部署名
                .Add(New NpgsqlParameter("SetRoom", NpgsqlTypes.NpgsqlDbType.Varchar))          '番組/部屋名
                .Add(New NpgsqlParameter("SetBuil", NpgsqlTypes.NpgsqlDbType.Varchar))          '建物
                .Add(New NpgsqlParameter("SetFloor", NpgsqlTypes.NpgsqlDbType.Varchar))         'フロア
                .Add(New NpgsqlParameter("SetKyokuNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))  '局名（あいまい）
                .Add(New NpgsqlParameter("SetBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))  '部署名（あいまい）
                .Add(New NpgsqlParameter("SetRoomAimai", NpgsqlTypes.NpgsqlDbType.Varchar))     '番組/部屋名（あいまい）
                .Add(New NpgsqlParameter("SetBuilAimai", NpgsqlTypes.NpgsqlDbType.Varchar))     '建物（あいまい）
                .Add(New NpgsqlParameter("SetFloorAimai", NpgsqlTypes.NpgsqlDbType.Varchar))    'フロア（あいまい）
                .Add(New NpgsqlParameter("Sort", NpgsqlTypes.NpgsqlDbType.Integer))             '表示順(空白)
                .Add(New NpgsqlParameter("JtiFlg", NpgsqlTypes.NpgsqlDbType.Varchar))           '削除フラグ(有効)
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))          '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))         '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))            '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '最終更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("SetBusyoCD").Value = dataHBKX1401.PropTxtSetBusyoCD.Text
                .Parameters("SetKyokuNM").Value = dataHBKX1401.PropTxtSetKyokuNM.Text
                .Parameters("SetBusyoNM").Value = dataHBKX1401.PropTxtSetBusyoNM.Text
                .Parameters("SetRoom").Value = dataHBKX1401.PropTxtSetRoom.Text
                .Parameters("SetBuil").Value = dataHBKX1401.PropTxtSetBuil.Text
                .Parameters("SetFloor").Value = dataHBKX1401.PropTxtSetFloor.Text

                '局名（あいまい）
                strSetKyokuNMAimai = commonLogicHBK.ChangeStringForSearch(dataHBKX1401.PropTxtSetKyokuNM.Text)
                .Parameters("SetKyokuNMAimai").Value = strSetKyokuNMAimai
                '部署名（あいまい）
                strSetBusyoNMAimai = commonLogicHBK.ChangeStringForSearch(dataHBKX1401.PropTxtSetBusyoNM.Text)
                .Parameters("SetBusyoNMAimai").Value = strSetBusyoNMAimai
                '番組/部屋名（あいまい）
                strSetRoomAimai = commonLogicHBK.ChangeStringForSearch(dataHBKX1401.PropTxtSetRoom.Text)
                .Parameters("SetRoomAimai").Value = strSetRoomAimai
                '建物（あいまい）
                strSetBuilAimai = commonLogicHBK.ChangeStringForSearch(dataHBKX1401.PropTxtSetBuil.Text)
                .Parameters("SetBuilAimai").Value = strSetBuilAimai
                'フロア（あいまい）
                strSetFloorAimai = commonLogicHBK.ChangeStringForSearch(dataHBKX1401.PropTxtSetFloor.Text)
                .Parameters("SetFloorAimai").Value = strSetFloorAimai

                .Parameters("Sort").Value = DBNull.Value
                .Parameters("JtiFlg").Value = DATA_YUKO
                .Parameters("RegDT").Value = dataHBKX1401.PropDtmSysDate
                .Parameters("RegGrpCD").Value = PropWorkGroupCD
                .Parameters("RegID").Value = PropUserId
                .Parameters("UpdateDT").Value = dataHBKX1401.PropDtmSysDate
                .Parameters("UpGrpCD").Value = PropWorkGroupCD
                .Parameters("UpdateID").Value = PropUserId
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
    ''' 設置情報マスター編集用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX1401">[IN]設置情報マスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>設置情報マスター編集用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateSoftMasterSql(ByRef Cmd As NpgsqlCommand, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKX1401 As DataHBKX1401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String
        Dim strSetKyokuNMAimai As String = ""       '局名（あいまい）
        Dim strSetBusyoNMAimai As String = ""       '部署名（あいまい）
        Dim strSetRoomAimai As String = ""          '番組/部屋名（あいまい）
        Dim strSetBuilAimai As String = ""          '建物（あいまい）
        Dim strSetFloorAimai As String = ""         'フロア（あいまい）

        Try
            '設置情報マスター更新用SQLを設定
            strSQL = strUpdateSetPosMastersql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("SetKyokuNM", NpgsqlTypes.NpgsqlDbType.Varchar))       '局名
                .Add(New NpgsqlParameter("SetBusyoNM", NpgsqlTypes.NpgsqlDbType.Varchar))       '部署名
                .Add(New NpgsqlParameter("SetRoom", NpgsqlTypes.NpgsqlDbType.Varchar))          '番組/部屋名
                .Add(New NpgsqlParameter("SetBuil", NpgsqlTypes.NpgsqlDbType.Varchar))          '建物
                .Add(New NpgsqlParameter("SetFloor", NpgsqlTypes.NpgsqlDbType.Varchar))         'フロア
                .Add(New NpgsqlParameter("SetKyokuNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))  '局名（あいまい）
                .Add(New NpgsqlParameter("SetBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))  '部署名（あいまい）
                .Add(New NpgsqlParameter("SetRoomAimai", NpgsqlTypes.NpgsqlDbType.Varchar))     '番組/部屋名（あいまい）
                .Add(New NpgsqlParameter("SetBuilAimai", NpgsqlTypes.NpgsqlDbType.Varchar))     '建物（あいまい）
                .Add(New NpgsqlParameter("SetFloorAimai", NpgsqlTypes.NpgsqlDbType.Varchar))    'フロア（あいまい）
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '最終更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
                .Add(New NpgsqlParameter("SetBusyoCD", NpgsqlTypes.NpgsqlDbType.Integer))       '設置部署CD
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("SetKyokuNM").Value = dataHBKX1401.PropTxtSetKyokuNM.Text
                .Parameters("SetBusyoNM").Value = dataHBKX1401.PropTxtSetBusyoNM.Text
                .Parameters("SetRoom").Value = dataHBKX1401.PropTxtSetRoom.Text
                .Parameters("SetBuil").Value = dataHBKX1401.PropTxtSetBuil.Text
                .Parameters("SetFloor").Value = dataHBKX1401.PropTxtSetFloor.Text

                '局名（あいまい）
                strSetKyokuNMAimai = commonLogicHBK.ChangeStringForSearch(dataHBKX1401.PropTxtSetKyokuNM.Text)
                .Parameters("SetKyokuNMAimai").Value = strSetKyokuNMAimai
                '部署名（あいまい）
                strSetBusyoNMAimai = commonLogicHBK.ChangeStringForSearch(dataHBKX1401.PropTxtSetBusyoNM.Text)
                .Parameters("SetBusyoNMAimai").Value = strSetBusyoNMAimai
                '番組/部屋名（あいまい）
                strSetRoomAimai = commonLogicHBK.ChangeStringForSearch(dataHBKX1401.PropTxtSetRoom.Text)
                .Parameters("SetRoomAimai").Value = strSetRoomAimai
                '建物（あいまい）
                strSetBuilAimai = commonLogicHBK.ChangeStringForSearch(dataHBKX1401.PropTxtSetBuil.Text)
                .Parameters("SetBuilAimai").Value = strSetBuilAimai
                'フロア（あいまい）
                strSetFloorAimai = commonLogicHBK.ChangeStringForSearch(dataHBKX1401.PropTxtSetFloor.Text)
                .Parameters("SetFloorAimai").Value = strSetFloorAimai

                .Parameters("UpdateDT").Value = dataHBKX1401.PropDtmSysDate
                .Parameters("UpGrpCD").Value = PropWorkGroupCD
                .Parameters("UpdateID").Value = PropUserId
                .Parameters("SetBusyoCD").Value = dataHBKX1401.PropTxtSetBusyoCD.Text
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
    ''' 設置情報マスター削除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX1401">[IN]設置情報マスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>設置情報マスター削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteEndUsrMasterSql(ByRef Cmd As NpgsqlCommand, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKX1401 As DataHBKX1401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String

        Try
            '設置情報マスター削除用SQLを設定
            strSQL = strUpdateSetPosMasterJtiFlgsql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("JtiFlg", NpgsqlTypes.NpgsqlDbType.Varchar))     '削除フラグ(無効)
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp)) '最終更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))    '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))   '最終更新者ID
                .Add(New NpgsqlParameter("SetBusyoCD", NpgsqlTypes.NpgsqlDbType.Integer)) '設置部署CD
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("JtiFlg").Value = DATA_MUKO
                .Parameters("UpdateDT").Value = dataHBKX1401.PropDtmSysDate
                .Parameters("UpGrpCD").Value = PropWorkGroupCD
                .Parameters("UpdateID").Value = PropUserId
                .Parameters("SetBusyoCD").Value = dataHBKX1401.PropTxtSetBusyoCD.Text
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
    ''' 設置情報マスター削除解除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX1401">[IN]設置情報マスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>設置情報マスター削除解除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUnDroppingSoftMasterSql(ByRef Cmd As NpgsqlCommand, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKX1401 As DataHBKX1401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String

        Try
            '設置情報マスター削除解除用SQLを設定
            strSQL = strUpdateSetPosMasterJtiFlgsql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("JtiFlg", NpgsqlTypes.NpgsqlDbType.Varchar))     '削除フラグ(有効)
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp)) '最終更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))    '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))   '最終更新者ID
                .Add(New NpgsqlParameter("SetBusyoCD", NpgsqlTypes.NpgsqlDbType.Integer)) '設置部署CD
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("JtiFlg").Value = DATA_YUKO
                .Parameters("UpdateDT").Value = dataHBKX1401.PropDtmSysDate
                .Parameters("UpGrpCD").Value = PropWorkGroupCD
                .Parameters("UpdateID").Value = PropUserId
                .Parameters("SetBusyoCD").Value = dataHBKX1401.PropTxtSetBusyoCD.Text
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
