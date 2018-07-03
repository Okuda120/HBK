Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' エンドユーザーマスター登録画面Sqlクラス
''' </summary>
''' <remarks>エンドユーザーマスター登録画面のSQLの作成・設定を行う
''' <para>作成情報：2012/08/09 k.ueda
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKX0401

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK


    'エンドユーザーマスター検索結果取得用
    Private strSelectEndUsrMastersql As String = "SELECT " & vbCrLf & _
                                                 "em.EndUsrID, " & vbCrLf & _
                                                 "em.UsrKbn, " & vbCrLf & _
                                                 "em.EndUsrSei, " & vbCrLf & _
                                                 "em.EndUsrMei, " & vbCrLf & _
                                                 "em.EndUsrSeikana, " & vbCrLf & _
                                                 "em.EndUsrMeikana, " & vbCrLf & _
                                                 "em.EndUsrCompany, " & vbCrLf & _
                                                 "em.EndUsrBusyoNM, " & vbCrLf & _
                                                 "em.EndUsrTel, " & vbCrLf & _
                                                 "em.EndUsrMailAdd, " & vbCrLf & _
                                                 "em.StateNaiyo, " & vbCrLf & _
                                                 "CASE " & vbCrLf & _
                                                     "WHEN em.RegKbn = :RegKbnTorikomi " & vbCrLf & _
                                                     "THEN '" & REG_TORIKOMI_NM & "' " & vbCrLf & _
                                                     "WHEN em.RegKbn = :RegKbnGamen " & vbCrLf & _
                                                     "THEN '" & REG_GAMEN_NM & "' " & vbCrLf & _
                                                     "ELSE '' " & vbCrLf & _
                                                 "END AS RegKbn," & vbCrLf & _
                                                 "em.JtiFlg " & vbCrLf & _
                                                 "FROM ENDUSR_MTB AS em " & vbCrLf & _
                                                 "WHERE em.EndUsrID = :EndUsrID "


    'エンドユーザーID取得用
    Private strSelectEndUsrIDsql As String = "SELECT em.EndUsrID " & vbCrLf & _
                                             "FROM ENDUSR_MTB AS em " & vbCrLf & _
                                             "WHERE em.EndUsrID = :EndUsrID "

    'エンドユーザーマスター新規登録用SQL
    Private strInsertEndUsrMastersql As String = "INSERT INTO ENDUSR_MTB ( " & vbCrLf & _
                                                     "EndUsrID, " & vbCrLf & _
                                                     "EndUsrSei, " & vbCrLf & _
                                                     "EndUsrMei, " & vbCrLf & _
                                                     "EndUsrNM, " & vbCrLf & _
                                                     "EndUsrSeikana, " & vbCrLf & _
                                                     "EndUsrMeikana, " & vbCrLf & _
                                                     "EndUsrNMkana, " & vbCrLf & _
                                                     "EndUsrCompany, " & vbCrLf & _
                                                     "EndUsrBusyoNM, " & vbCrLf & _
                                                     "EndUsrTel, " & vbCrLf & _
                                                     "EndUsrMailAdd, " & vbCrLf & _
                                                     "UsrKbn, " & vbCrLf & _
                                                     "StateNaiyo," & vbCrLf & _
                                                     "RegKbn, " & vbCrLf & _
                                                     "EndUsrNMAimai, " & vbCrLf & _
                                                     "EndUsrBusyoNMAimai, " & vbCrLf & _
                                                     "EndUsrAimai, " & vbCrLf & _
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
                                                     ":EndUsrID, " & vbCrLf & _
                                                     ":EndUsrSei, " & vbCrLf & _
                                                     ":EndUsrMei, " & vbCrLf & _
                                                     ":EndUsrNM, " & vbCrLf & _
                                                     ":EndUsrSeikana, " & vbCrLf & _
                                                     ":EndUsrMeikana, " & vbCrLf & _
                                                     ":EndUsrNMkana, " & vbCrLf & _
                                                     ":EndUsrCompany, " & vbCrLf & _
                                                     ":EndUsrBusyoNM, " & vbCrLf & _
                                                     ":EndUsrTel, " & vbCrLf & _
                                                     ":EndUsrMailAdd, " & vbCrLf & _
                                                     ":UsrKbn, " & vbCrLf & _
                                                     ":StateNaiyo," & vbCrLf & _
                                                     ":RegKbn, " & vbCrLf & _
                                                     ":EndUsrNMAimai, " & vbCrLf & _
                                                     ":EndUsrBusyoNMAimai, " & vbCrLf & _
                                                     ":EndUsrAimai, " & vbCrLf & _
                                                     ":Sort, " & vbCrLf & _
                                                     ":JtiFlg, " & vbCrLf & _
                                                     ":RegDT, " & vbCrLf & _
                                                     ":RegGrpCD, " & vbCrLf & _
                                                     ":RegID, " & vbCrLf & _
                                                     ":UpdateDT, " & vbCrLf & _
                                                     ":UpGrpCD, " & vbCrLf & _
                                                     ":UpdateID " & vbCrLf & _
                                                 ") "


    'エンドユーザーマスター更新用SQL
    Private strUpdateEndUsrMastersql As String = "UPDATE ENDUSR_MTB SET " & vbCrLf & _
                                                 "EndUsrSei = :EndUsrSei, " & vbCrLf & _
                                                 "EndUsrMei = :EndUsrMei, " & vbCrLf & _
                                                 "EndUsrNM = :EndUsrNM, " & vbCrLf & _
                                                 "EndUsrSeikana = :EndUsrSeikana, " & vbCrLf & _
                                                 "EndUsrMeikana = :EndUsrMeikana, " & vbCrLf & _
                                                 "EndUsrNMkana = :EndUsrNMkana, " & vbCrLf & _
                                                 "EndUsrCompany = :EndUsrCompany, " & vbCrLf & _
                                                 "EndUsrBusyoNM = :EndUsrBusyoNM, " & vbCrLf & _
                                                 "EndUsrTel = :EndUsrTel, " & vbCrLf & _
                                                 "EndUsrMailAdd = :EndUsrMailAdd, " & vbCrLf & _
                                                 "UsrKbn = :UsrKbn, " & vbCrLf & _
                                                 "StateNaiyo = :StateNaiyo, " & vbCrLf & _
                                                 "EndUsrNMAimai = :EndUsrNMAimai, " & vbCrLf & _
                                                 "EndUsrBusyoNMAimai = :EndUsrBusyoNMAimai, " & vbCrLf & _
                                                 "EndUsrAimai = :EndUsrAimai, " & vbCrLf & _
                                                 "UpdateDT = :UpdateDT, " & vbCrLf & _
                                                 "UpGrpCD = :UpGrpCD, " & vbCrLf & _
                                                 "UpdateID = :UpdateID " & vbCrLf & _
                                                 "WHERE EndUsrID = :EndUsrID"


    'システム日付取得（SELECT）SQL
    Private strSelectSysDateSql As String = "SELECT Now() AS SysDate "


    ''' <summary>
    ''' エンドユーザーマスター取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0401">[IN]エンドユーザーマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>エンドユーザーマスター取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/09 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectEndUsrMasterSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKX0401 As DataHBKX0401) As Boolean



        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSQL As String

        Try
            '**********************************
            '* SQL文設定
            '**********************************

            'エンドユーザーマスターテーブル取得用SQLを設定
            strSQL = strSelectEndUsrMastersql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            '**********************************
            '* バインド変数に型と値をセット
            '**********************************

            '登録方法(取込)
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RegKbnTorikomi", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("RegKbnTorikomi").Value = REG_TORIKOMI
            '登録方法(画面入力)
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RegKbnGamen", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("RegKbnGamen").Value = REG_GAMEN
            'エンドユーザーID
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("EndUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("EndUsrID").Value = dataHBKX0401.PropStrEndUsrID


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
    ''' エンドユーザーID取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0401">[IN]エンドユーザーマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>エンドユーザーID取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/09 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectEndUsrIDSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKX0401 As DataHBKX0401) As Boolean

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
            'エンドユーザーID
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("EndUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("EndUsrID").Value = dataHBKX0401.PropTxtEndUsrID.Text

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
    ''' エンドユーザーマスター新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0401">[IN]エンドユーザーマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>エンドユーザーマスター新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/09 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertEndUsrMasterSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKX0401 As DataHBKX0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String


        Try

            'エンドユーザーマスター新規登録用SQLを設定
            strSQL = strInsertEndUsrMastersql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            With dataHBKX0401
                'ユーザーID
                Cmd.Parameters.Add(New NpgsqlParameter("EndUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("EndUsrID").Value = .PropTxtEndUsrID.Text
                '姓
                Cmd.Parameters.Add(New NpgsqlParameter("EndUsrSei", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("EndUsrSei").Value = .PropTxtEndUsrSei.Text
                '名
                Cmd.Parameters.Add(New NpgsqlParameter("EndUsrMei", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("EndUsrMei").Value = .PropTxtEndUsrMei.Text
                '氏名
                Cmd.Parameters.Add(New NpgsqlParameter("EndUsrNM", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("EndUsrNM").Value = .PropTxtEndUsrSei.Text + "　" + .PropTxtEndUsrMei.Text
                '姓カナ
                Cmd.Parameters.Add(New NpgsqlParameter("EndUsrSeikana", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("EndUsrSeikana").Value = .PropTxtEndUsrSeikana.Text
                '名カナ
                Cmd.Parameters.Add(New NpgsqlParameter("EndUsrMeikana", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("EndUsrMeikana").Value = .PropTxtEndUsrMeikana.Text
                '氏名カナ
                Cmd.Parameters.Add(New NpgsqlParameter("EndUsrNMkana", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("EndUsrNMkana").Value = .PropTxtEndUsrSeikana.Text + "　" + .PropTxtEndUsrMeikana.Text
                '所属会社
                Cmd.Parameters.Add(New NpgsqlParameter("EndUsrCompany", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("EndUsrCompany").Value = .PropTxtEndUsrCompany.Text
                '部署名
                Cmd.Parameters.Add(New NpgsqlParameter("EndUsrBusyoNM", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("EndUsrBusyoNM").Value = .PropTxtEndUsrBusyoNM.Text
                '電話番号
                Cmd.Parameters.Add(New NpgsqlParameter("EndUsrTel", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("EndUsrTel").Value = .PropTxtEndUsrTel.Text
                'メールアドレス
                Cmd.Parameters.Add(New NpgsqlParameter("EndUsrMailAdd", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("EndUsrMailAdd").Value = .PropTxtEndUsrMailAdd.Text
                'ユーザー区分
                Cmd.Parameters.Add(New NpgsqlParameter("UsrKbn", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UsrKbn").Value = .PropTxtUsrKbn.Text
                '状態説明
                Cmd.Parameters.Add(New NpgsqlParameter("StateNaiyo", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("StateNaiyo").Value = .PropTxtStateNaiyo.Text
                '登録方法(画面入力:1固定)
                Cmd.Parameters.Add(New NpgsqlParameter("RegKbn", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("RegKbn").Value = REG_GAMEN
                '氏名(あいまい)
                Cmd.Parameters.Add(New NpgsqlParameter("EndUsrNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("EndUsrNMAimai").Value = commonLogicHBK.ChangeStringForSearch(.PropTxtEndUsrSei.Text) +
                commonLogicHBK.ChangeStringForSearch(.PropTxtEndUsrMei.Text) + commonLogicHBK.ChangeStringForSearch(.PropTxtEndUsrSeikana.Text) +
                commonLogicHBK.ChangeStringForSearch(.PropTxtEndUsrMeikana.Text)
                '部署名(あいまい)
                Cmd.Parameters.Add(New NpgsqlParameter("EndUsrBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("EndUsrBusyoNMAimai").Value = commonLogicHBK.ChangeStringForSearch(.PropTxtEndUsrBusyoNM.Text)
                'エンドユーザー(あいまい)
                Cmd.Parameters.Add(New NpgsqlParameter("EndUsrAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("EndUsrAimai").Value = commonLogicHBK.ChangeStringForSearch(.PropTxtEndUsrID.Text) +
                commonLogicHBK.ChangeStringForSearch(.PropTxtEndUsrSei.Text) + commonLogicHBK.ChangeStringForSearch(.PropTxtEndUsrMei.Text) +
                commonLogicHBK.ChangeStringForSearch(.PropTxtEndUsrSeikana.Text) + commonLogicHBK.ChangeStringForSearch(.PropTxtEndUsrMeikana.Text) +
                commonLogicHBK.ChangeStringForSearch(.PropTxtEndUsrBusyoNM.Text) + commonLogicHBK.ChangeStringForSearch(.PropTxtEndUsrMailAdd.Text)
                '表示順(空白)
                Cmd.Parameters.Add(New NpgsqlParameter("Sort", NpgsqlTypes.NpgsqlDbType.Integer))
                Cmd.Parameters("Sort").Value = DBNull.Value
                '削除フラグ(0:有効データ固定)
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
    ''' エンドユーザーマスター編集用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0401">[IN]エンドユーザーマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>エンドユーザーマスター編集用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/09 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateEndUsrMasterSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKX0401 As DataHBKX0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'エンドユーザーマスター更新用SQLを設定
            strSQL = strUpdateEndUsrMastersql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            With dataHBKX0401
                '姓
                Cmd.Parameters.Add(New NpgsqlParameter("EndUsrSei", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("EndUsrSei").Value = .PropTxtEndUsrSei.Text
                '名
                Cmd.Parameters.Add(New NpgsqlParameter("EndUsrMei", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("EndUsrMei").Value = .PropTxtEndUsrMei.Text
                '氏名
                Cmd.Parameters.Add(New NpgsqlParameter("EndUsrNM", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("EndUsrNM").Value = .PropTxtEndUsrSei.Text + "　" + .PropTxtEndUsrMei.Text
                '姓カナ
                Cmd.Parameters.Add(New NpgsqlParameter("EndUsrSeikana", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("EndUsrSeikana").Value = .PropTxtEndUsrSeikana.Text
                '名カナ
                Cmd.Parameters.Add(New NpgsqlParameter("EndUsrMeikana", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("EndUsrMeikana").Value = .PropTxtEndUsrMeikana.Text
                '氏名カナ
                Cmd.Parameters.Add(New NpgsqlParameter("EndUsrNMkana", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("EndUsrNMkana").Value = .PropTxtEndUsrSeikana.Text + "　" + .PropTxtEndUsrMeikana.Text
                '所属会社
                Cmd.Parameters.Add(New NpgsqlParameter("EndUsrCompany", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("EndUsrCompany").Value = .PropTxtEndUsrCompany.Text
                '部署名
                Cmd.Parameters.Add(New NpgsqlParameter("EndUsrBusyoNM", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("EndUsrBusyoNM").Value = .PropTxtEndUsrBusyoNM.Text
                '電話番号
                Cmd.Parameters.Add(New NpgsqlParameter("EndUsrTel", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("EndUsrTel").Value = .PropTxtEndUsrTel.Text
                'メールアドレス
                Cmd.Parameters.Add(New NpgsqlParameter("EndUsrMailAdd", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("EndUsrMailAdd").Value = .PropTxtEndUsrMailAdd.Text
                'ユーザー区分
                Cmd.Parameters.Add(New NpgsqlParameter("UsrKbn", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UsrKbn").Value = .PropTxtUsrKbn.Text
                '状態説明
                Cmd.Parameters.Add(New NpgsqlParameter("StateNaiyo", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("StateNaiyo").Value = .PropTxtStateNaiyo.Text
                '氏名(あいまい)
                Cmd.Parameters.Add(New NpgsqlParameter("EndUsrNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("EndUsrNMAimai").Value = commonLogicHBK.ChangeStringForSearch(.PropTxtEndUsrSei.Text) +
                commonLogicHBK.ChangeStringForSearch(.PropTxtEndUsrMei.Text) + commonLogicHBK.ChangeStringForSearch(.PropTxtEndUsrSeikana.Text) +
                commonLogicHBK.ChangeStringForSearch(.PropTxtEndUsrMeikana.Text)
                '部署名(あいまい)
                Cmd.Parameters.Add(New NpgsqlParameter("EndUsrBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("EndUsrBusyoNMAimai").Value = commonLogicHBK.ChangeStringForSearch(.PropTxtEndUsrBusyoNM.Text)
                'エンドユーザー(あいまい)
                Cmd.Parameters.Add(New NpgsqlParameter("EndUsrAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("EndUsrAimai").Value = commonLogicHBK.ChangeStringForSearch(.PropTxtEndUsrID.Text) +
                commonLogicHBK.ChangeStringForSearch(.PropTxtEndUsrSei.Text) + commonLogicHBK.ChangeStringForSearch(.PropTxtEndUsrMei.Text) +
                commonLogicHBK.ChangeStringForSearch(.PropTxtEndUsrSeikana.Text) + commonLogicHBK.ChangeStringForSearch(.PropTxtEndUsrMeikana.Text) +
                commonLogicHBK.ChangeStringForSearch(.PropTxtEndUsrBusyoNM.Text) + commonLogicHBK.ChangeStringForSearch(.PropTxtEndUsrMailAdd.Text)
                '最終更新日時
                Cmd.Parameters.Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))
                Cmd.Parameters("UpdateDT").Value = .PropDtmSysDate
                '最終更新者グループCD
                Cmd.Parameters.Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UpGrpCD").Value = PropWorkGroupCD
                '最終更新者ID
                Cmd.Parameters.Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UpdateID").Value = PropUserId
                'エンドユーザーID
                Cmd.Parameters.Add(New NpgsqlParameter("EndUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("EndUsrID").Value = dataHBKX0401.PropStrEndUsrID

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
    ''' サーバー日付取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0401">[IN]エンドユーザーマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/14 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKX0401 As DataHBKX0401) As Boolean

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

End Class
