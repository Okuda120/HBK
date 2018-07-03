Imports Common
Imports CommonHBK
Imports Npgsql
''' <summary>
''' 並び順登録画面Sqlクラス
''' </summary>
''' <remarks>並び順登録画面のSQLの作成・設定を行う
''' <para>作成情報：2012/08/16 k.ueda
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKX0801





    'グループマスター取得用SQL
    Private StrSelectGroupMastersl As String = "SELECT " & vbCrLf & _
                                               "CAST(TO_CHAR(gm.Sort,'FM9999999D00')AS Float) AS Sort, " & vbCrLf & _
                                               "gm.GroupCD, " & vbCrLf & _
                                               "gm.GroupNM, " & vbCrLf & _
                                               "gm.JtiFlg " & vbCrLf & _
                                               "FROM GRP_MTB AS gm " & vbCrLf & _
                                               "ORDER BY gm.Sort ASC "


    'CI共通情報取得用SQL
    Private StrSelectCIInfosql As String = "SELECT " & vbCrLf & _
                                           "CAST(TO_CHAR(ct.sort,'FM9999999D00')AS Float) As Sort, " & vbCrLf & _
                                           "ct.CINmb, " & vbCrLf & _
                                           "ct.Class1 || ' ' || ct.Class2 || ' ' || ct.CINM AS CINM " & vbCrLf & _
                                           "FROM CI_INFO_TB AS ct " & vbCrLf & _
                                           "WHERE ct.CIKbnCD = :CIKbnCD " & vbCrLf & _
                                           "ORDER BY ct.Sort ASC"

    'メールテンプレートマスター取得用SQL
    Private StrSelectMailTempMastersl As String = "SELECT " & vbCrLf & _
                                                  "CAST(TO_CHAR(mt.Sort,'FM9999999D00')AS Float) AS Sort, " & vbCrLf & _
                                                  "mt.TemplateNmb, " & vbCrLf & _
                                                  "'[' || " & _
                                                  "CASE mt.ProcessKbn " & vbCrLf & _
                                                  " WHEN :Kbn_Incident THEN :Kbn_Incident_NM " & vbCrLf & _
                                                  " WHEN :Kbn_Question THEN :Kbn_Question_NM " & vbCrLf & _
                                                  " WHEN :Kbn_Change   THEN :Kbn_Change_NM " & vbCrLf & _
                                                  " WHEN :Kbn_Release  THEN :Kbn_Release_NM " & vbCrLf & _
                                                  "ELSE '' END " & vbCrLf & _
                                                  " || '] ' || mt.TemplateNM AS TemplateNM, " & vbCrLf & _
                                                  "mt.JtiFlg " & vbCrLf & _
                                                  "FROM MAIL_TEMPLATE_MTB AS mt " & vbCrLf & _
                                                  "WHERE mt.GroupCD = :GroupCD " & vbCrLf & _
                                                  "ORDER BY mt.Sort ASC "

    'グループマスター登録用SQL
    Private StrUpdateGroupMastersql As String = "UPDATE GRP_MTB SET " & vbCrLf & _
                                                "Sort = :Sort " & vbCrLf & _
                                                "WHERE GroupCD = :GroupCD"

    'CI共通情報登録用SQL
    Private StrUpdateCIInfosql As String = "UPDATE CI_INFO_TB SET " & vbCrLf & _
                                           "Sort = :Sort " & vbCrLf & _
                                           "WHERE CINmb = :CINmb"

    'メールテンプレートマスター登録用SQL
    Private StrUpdateMailTempMastersql As String = "UPDATE MAIL_TEMPLATE_MTB SET " & vbCrLf & _
                                                   "Sort = :Sort " & vbCrLf & _
                                                   "WHERE TemplateNmb = :TemplateNmb"

    ''' <summary>
    ''' グループマスター取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0801">[IN]並び順登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>グループマスター取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectGroupMasterSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKX0801 As DataHBKX0801) As Boolean



        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSQL As String

        

        Try
            '**********************************
            '* SQL文設定
            '**********************************

            'エンドユーザーマスターテーブル取得用SQLを設定
            strSQL = StrSelectGroupMastersl

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
    ''' CI共通情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0801">[IN]並び順登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>グループマスター取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCIInfoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKX0801 As DataHBKX0801) As Boolean



        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSQL As String

        Try
            '**********************************
            '* SQL文設定
            '**********************************

            'CI共通情報テーブル取得用SQLを設定
            strSQL = StrSelectCIInfosql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            '**********************************
            '* バインド変数に型と値をセット
            '**********************************

            'CI種別CD(001：システム)
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("CIKbnCD").Value = CI_TYPE_SYSTEM


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
    ''' メールテンプレートマスター取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0801">[IN]並び順登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>メールテンプレートマスター取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2015/08/18 e.okamura
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectMailTempMasterSql(ByRef Adapter As NpgsqlDataAdapter, _
                                               ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSQL As String

        Try
            '**********************************
            '* SQL文設定
            '**********************************

            'メールテンプレートマスターテーブル取得用SQLを設定
            strSQL = StrSelectMailTempMastersl

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '**********************************
            '* バインド変数に型と値をセット
            '**********************************

            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("Kbn_Incident", NpgsqlTypes.NpgsqlDbType.Varchar))     'プロセス区分：インシデント
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("Kbn_Incident_NM", NpgsqlTypes.NpgsqlDbType.Varchar))  'プロセス区分名：インシデント
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("Kbn_Question", NpgsqlTypes.NpgsqlDbType.Varchar))     'プロセス区分：問題
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("Kbn_Question_NM", NpgsqlTypes.NpgsqlDbType.Varchar))  'プロセス区分：問題
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("Kbn_Change", NpgsqlTypes.NpgsqlDbType.Varchar))       'プロセス区分：変更
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("Kbn_Change_NM", NpgsqlTypes.NpgsqlDbType.Varchar))    'プロセス区分名：変更
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("Kbn_Release", NpgsqlTypes.NpgsqlDbType.Varchar))      'プロセス区分：リリース
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("Kbn_Release_NM", NpgsqlTypes.NpgsqlDbType.Varchar))   'プロセス区分名：リリース
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("GroupCD", NpgsqlTypes.NpgsqlDbType.Varchar))          'グループCD(選択中のグループCD)

            Adapter.SelectCommand.Parameters("Kbn_Incident").Value = PROCESS_TYPE_INCIDENT                      'プロセス区分：インシデント
            Adapter.SelectCommand.Parameters("Kbn_Incident_NM").Value = PROCESS_TYPE_INCIDENT_NAME              'プロセス区分名：インシデント
            Adapter.SelectCommand.Parameters("Kbn_Question").Value = PROCESS_TYPE_QUESTION                      'プロセス区分：問題
            Adapter.SelectCommand.Parameters("Kbn_Question_NM").Value = PROCESS_TYPE_QUESTION_NAME              'プロセス区分名：問題
            Adapter.SelectCommand.Parameters("Kbn_Change").Value = PROCESS_TYPE_CHANGE                          'プロセス区分：変更
            Adapter.SelectCommand.Parameters("Kbn_Change_NM").Value = PROCESS_TYPE_CHANGE_NAME                  'プロセス区分名：変更
            Adapter.SelectCommand.Parameters("Kbn_Release").Value = PROCESS_TYPE_RELEASE                        'プロセス区分：リリース
            Adapter.SelectCommand.Parameters("Kbn_Release_NM").Value = PROCESS_TYPE_RELEASE_NAME                'プロセス区分名：リリース
            Adapter.SelectCommand.Parameters("GroupCD").Value = CommonHBK.CommonDeclareHBK.PropWorkGroupCD      'グループCD(選択中のグループCD)

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
    ''' グループマスター並び順登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0801">[IN]並び順登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>グループマスター表示順登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateGroupMasterSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String


        Try

            'グループマスター表示順登録用SQLを設定
            strSQL = StrUpdateGroupMastersql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            '**********************************
            '* バインド変数に型と値をセット
            '**********************************

            With dataHBKX0801
                '表示順
                Cmd.Parameters.Add(New NpgsqlParameter("Sort", NpgsqlTypes.NpgsqlDbType.Integer))
                Cmd.Parameters("Sort").Value = .PropIntSort
                'グループCD
                Cmd.Parameters.Add(New NpgsqlParameter("GroupCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("GroupCD").Value = .PropStrGrpCD
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
    ''' CI共通情報並び順登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0801">[IN]並び順登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報表示順登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateCIInfoSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String

        Try

            'CI共通情報表示順登録用SQLを設定
            strSQL = StrUpdateCIInfosql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            With dataHBKX0801
                '表示順
                Cmd.Parameters.Add(New NpgsqlParameter("Sort", NpgsqlTypes.NpgsqlDbType.Integer))
                Cmd.Parameters("Sort").Value = .PropIntSort
                'グループCD
                Cmd.Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))
                Cmd.Parameters("CINmb").Value = .PropIntCInmb
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
    ''' メールテンプレートマスター並び順登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0801">[IN]並び順登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>メールテンプレートマスター表示順登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2015/08/18 e.okamura
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateMailTempMasterSql(ByRef Cmd As NpgsqlCommand, _
                                               ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String

        Try

            'メールテンプレートマスター表示順登録用SQLを設定
            strSQL = StrUpdateMailTempMastersql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            With dataHBKX0801
                '表示順
                Cmd.Parameters.Add(New NpgsqlParameter("Sort", NpgsqlTypes.NpgsqlDbType.Integer))
                Cmd.Parameters("Sort").Value = .PropIntSort
                'テンプレート番号
                Cmd.Parameters.Add(New NpgsqlParameter("TemplateNmb", NpgsqlTypes.NpgsqlDbType.Integer))
                Cmd.Parameters("TemplateNmb").Value = .PropIntTemplateNmb
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
