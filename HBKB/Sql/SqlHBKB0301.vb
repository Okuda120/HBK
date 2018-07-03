Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' 変更理由画面画面Sqlクラス
''' </summary>
''' <remarks>変更理由画面のSQLの作成・設定を行う
''' <para>作成情報：2012/07/02 y.ikushima
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKB0301

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK

    '理由データ取得SQL
    Private SetSelectReasonSql As String = "SELECT"

    '原因リンクデータ取得SQL
    Private SetSelectCauseSql As String = "SELECT"

    'プロセスタイトル取得SQL
    Private SetSelectProcessTitleSql As String = "SELECT Title "

    'プロセスタイトル取得SQL(FROM句-インシデント用)
    Private SetFromincidentSql As String = "FROM incident_info_tb " & vbCrLf & _
                                            "WHERE ProcessKbn = :ProcessKbn " & vbCrLf & _
                                            "And IncNmb = :ProcessNmb "
    'プロセスタイトル取得SQL(FROM句-問題用)
    Private SetFromproblemSql As String = "FROM problem_info_tb " & vbCrLf & _
                                            "WHERE ProcessKbn = :ProcessKbn " & vbCrLf & _
                                            "And PrbNmb = :ProcessNmb "
    'プロセスタイトル取得SQL(FROM句-変更用)
    Private SetFromchangeSql As String = "FROM change_info_tb " & vbCrLf & _
                                            "WHERE ProcessKbn = :ProcessKbn " & vbCrLf & _
                                            "And ChgNmb = :ProcessNmb "
    'プロセスタイトル取得SQL(FROM句-リリース用)
    Private SetFromreleaseSql As String = "FROM release_info_tb " & vbCrLf & _
                                            "WHERE ProcessKbn = :ProcessKbn " & vbCrLf & _
                                            "And RelNmb = :ProcessNmb "



    'プロセス名称取得SQL
    Private SetSelectProcessKbnSql As String = "SELECT" & vbCrLf & _
                                                                    "DISTINCT" & vbCrLf & _
                                                                    "CASE ct.ProcessKbn " & vbCrLf & _
                                                                    " WHEN :Kbn_Incident THEN :Kbn_Incident_NMR " & vbCrLf & _
                                                                    " WHEN :Kbn_Question THEN :Kbn_Question_NMR " & vbCrLf & _
                                                                    " WHEN :Kbn_Change THEN :Kbn_Change_NMR " & vbCrLf & _
                                                                    " WHEN :Kbn_Release THEN :Kbn_Release_NMR " & vbCrLf & _
                                                                " ELSE '' END AS ProcessKbnNMR " & vbCrLf & _
                                                                "FROM CAUSELINK_RTB ct " & vbCrLf & _
                                                                "WHERE ct.ProcessKbn = :ProcessKbn"

    ''' <summary>
    ''' 理由データ取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0301">[IN/OUT]変更理由登録画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>理由データ取得のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/02 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectReason(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal DataHBKB0301 As DataHBKB0301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try
            '**********************************
            '* SQL文設定
            '**********************************
            'SQL文(SELECT)
            strSQL = SetSelectReasonSql
            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '**********************************
            '* バインド変数に型と値をセット
            '**********************************


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
    ''' 原因リンクデータ取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0301">[IN/OUT]変更理由登録画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>原因リンクデータ取得のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/02 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCause(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB0301 As DataHBKB0301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try
            '**********************************
            '* SQL文設定
            '**********************************
            'SQL文(SELECT)
            strSQL = SetSelectCauseSql
            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '**********************************
            '* バインド変数に型と値をセット
            '**********************************


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
    ''' 最終管理番号タイトル取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0301">[IN/OUT]変更理由登録画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>最終管理番号タイトル取得のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/07 m.ibuki
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectLastManageTitle(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB0301 As DataHBKB0301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try
            '**********************************
            '* SQL文設定
            '**********************************
            'SQL文(SELECT)
            strSQL = SetSelectProcessTitleSql

            If CommonDeclareHBK.PropLastProcessKbn = PROCESS_TYPE_INCIDENT Then
                strSQL += SetFromincidentSql
            ElseIf CommonDeclareHBK.PropLastProcessKbn = PROCESS_TYPE_QUESTION Then
                strSQL += SetFromproblemSql
            ElseIf CommonDeclareHBK.PropLastProcessKbn = PROCESS_TYPE_CHANGE Then
                strSQL += SetFromchangeSql
            ElseIf CommonDeclareHBK.PropLastProcessKbn = PROCESS_TYPE_RELEASE Then
                strSQL += SetFromreleaseSql
            End If

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))           'プロセス区分コード
                .Add(New NpgsqlParameter("ProcessNmb", NpgsqlTypes.NpgsqlDbType.Integer))           'プロセス番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("ProcessKbn").Value = CommonDeclareHBK.PropLastProcessKbn                   'プロセス区分コード
                .Parameters("ProcessNmb").Value = Int(CommonDeclareHBK.PropLastProcessNmb)              'プロセス番号
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
    ''' 【編集／参照モード】原因リンク取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0301">[IN/OUT]変更理由登録画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>原因リンク取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報：2012/07/03 y.ikushima 変更理由登録の仕様へ変更</p>
    ''' </para></remarks>
    Public Function SetSelectCauseLinkSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0301 As DataHBKB0301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = SetSelectProcessKbnSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("Kbn_Incident", NpgsqlTypes.NpgsqlDbType.Varchar))      'プロセス区分：インシデント
                .Add(New NpgsqlParameter("Kbn_Incident_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))  'プロセス区分名略称：インシデント
                .Add(New NpgsqlParameter("Kbn_Question", NpgsqlTypes.NpgsqlDbType.Varchar))      'プロセス区分：問題
                .Add(New NpgsqlParameter("Kbn_Question_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))  'プロセス区分名略称：問題
                .Add(New NpgsqlParameter("Kbn_Change", NpgsqlTypes.NpgsqlDbType.Varchar))        'プロセス区分：変更
                .Add(New NpgsqlParameter("Kbn_Change_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))    'プロセス区分名略称：変更
                .Add(New NpgsqlParameter("Kbn_Release", NpgsqlTypes.NpgsqlDbType.Varchar))       'プロセス区分：リリース
                .Add(New NpgsqlParameter("Kbn_Release_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))   'プロセス区分名略称：リリース
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))             'プロセス区分コード
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("Kbn_Incident").Value = PROCESS_TYPE_INCIDENT                        'プロセス区分：インシデント
                .Parameters("Kbn_Incident_NMR").Value = PROCESS_TYPE_INCIDENT_NAME_R             'プロセス区分名略称：インシデント
                .Parameters("Kbn_Question").Value = PROCESS_TYPE_QUESTION                        'プロセス区分：問題
                .Parameters("Kbn_Question_NMR").Value = PROCESS_TYPE_QUESTION_NAME_R             'プロセス区分名略称：問題
                .Parameters("Kbn_Change").Value = PROCESS_TYPE_CHANGE                            'プロセス区分：変更
                .Parameters("Kbn_Change_NMR").Value = PROCESS_TYPE_CHANGE_NAME_R                 'プロセス区分名略称：変更
                .Parameters("Kbn_Release").Value = PROCESS_TYPE_RELEASE                          'プロセス区分：リリース
                .Parameters("Kbn_Release_NMR").Value = PROCESS_TYPE_RELEASE_NAME_R               'プロセス区分名略称：リリース
                .Parameters("ProcessKbn").Value = dataHBKB0301.PropStrProcessKbn                            'プロセス区分コード
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
