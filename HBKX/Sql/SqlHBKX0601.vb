Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' メールテンプレートマスター一覧画面
''' </summary>
''' <remarks>メールテンプレートマスター一覧画面のSQLの作成・設定を行う
''' <para>作成情報：2012/08/10 s.tsuruta
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKX0601

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK

    '受付手段CD取得処理（SELECT）SQL
    Private strSelectMailTemplateMastaSql As String = "SELECT " & vbCrLf & _
                                                      " mm.TemplateNmb" & vbCrLf & _
                                                      ",mm.TemplateNM" & vbCrLf & _
                                                      ",CASE mm.ProcessKbn " & vbCrLf & _
                                                      " WHEN :Kbn_Incident THEN :Kbn_Incident_NMR " & vbCrLf & _
                                                      " WHEN :Kbn_Question THEN :Kbn_Question_NMR " & vbCrLf & _
                                                      " WHEN :Kbn_Change THEN :Kbn_Change_NMR " & vbCrLf & _
                                                      " WHEN :Kbn_Release THEN :Kbn_Release_NMR " & vbCrLf & _
                                                      " ELSE '' END AS ProcessKbn " & vbCrLf & _
                                                      ",CASE mm.JtiFlg " & vbCrLf & _
                                                      " WHEN :JtiFlg_Enable THEN :JtiFlg_Enable_NMR " & vbCrLf & _
                                                      " WHEN :JtiFlg_Invaild THEN :JtiFlg_Invaild_NMR " & vbCrLf & _
                                                      " ELSE '' END AS JtiFlg " & vbCrLf & _
                                                      "FROM MAIL_TEMPLATE_MTB mm Where mm.GroupCD = :GroupCD"

    'and句()
    Private strJtiFlg As String = " and mm.JtiFlg = '0' "

    'Order By句
    Private strOrderByMail As String = " ORDER BY mm.TemplateNmb asc"


    ''' <summary>
    '''メールテンプレートマスター（全件取得）取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0601">[IN/OUT]メールテンプレートマスター一覧画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>メールテンプレートマスター取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/10 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectMailTemplateMastaAllSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKX0601 As DataHBKX0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectMailTemplateMastaSql & strOrderByMail

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'パラメータを設定
            With Adapter.SelectCommand.Parameters

                .Add(New NpgsqlParameter("Kbn_Incident", NpgsqlTypes.NpgsqlDbType.Varchar))         'プロセス区分：インシデント
                .Add(New NpgsqlParameter("Kbn_Incident_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))     'プロセス区分名：インシデント
                .Add(New NpgsqlParameter("Kbn_Question", NpgsqlTypes.NpgsqlDbType.Varchar))         'プロセス区分：問題
                .Add(New NpgsqlParameter("Kbn_Question_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))     'プロセス区分名：問題
                .Add(New NpgsqlParameter("Kbn_Change", NpgsqlTypes.NpgsqlDbType.Varchar))           'プロセス区分：変更
                .Add(New NpgsqlParameter("Kbn_Change_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))       'プロセス区分名：変更
                .Add(New NpgsqlParameter("Kbn_Release", NpgsqlTypes.NpgsqlDbType.Varchar))          'プロセス区分：リリース
                .Add(New NpgsqlParameter("Kbn_Release_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))      'プロセス区分名：リリース
                .Add(New NpgsqlParameter("JtiFlg_Enable", NpgsqlTypes.NpgsqlDbType.Varchar))        '削除フラグ：有効
                .Add(New NpgsqlParameter("JtiFlg_Enable_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))    '削除フラグ名：有効
                .Add(New NpgsqlParameter("JtiFlg_Invaild", NpgsqlTypes.NpgsqlDbType.Varchar))       '削除フラグ:無効
                .Add(New NpgsqlParameter("JtiFlg_Invaild_NMR", NpgsqlTypes.NpgsqlDbType.Varchar))   '削除フラグ名：無効
                .Add(New NpgsqlParameter("GroupCD", NpgsqlTypes.NpgsqlDbType.Varchar))              'グループコード

            End With


            '値を入れる
            With Adapter.SelectCommand

                .Parameters("Kbn_Incident").Value = PROCESS_TYPE_INCIDENT                           'プロセス区分：インシデント
                .Parameters("Kbn_Incident_NMR").Value = PROCESS_TYPE_INCIDENT_NAME                  'プロセス区分名：インシデント
                .Parameters("Kbn_Question").Value = PROCESS_TYPE_QUESTION                           'プロセス区分：問題
                .Parameters("Kbn_Question_NMR").Value = PROCESS_TYPE_QUESTION_NAME                  'プロセス区分名：問題
                .Parameters("Kbn_Change").Value = PROCESS_TYPE_CHANGE                               'プロセス区分：変更
                .Parameters("Kbn_Change_NMR").Value = PROCESS_TYPE_CHANGE_NAME                      'プロセス区分名：変更
                .Parameters("Kbn_Release").Value = PROCESS_TYPE_RELEASE                             'プロセス区分：リリース
                .Parameters("Kbn_Release_NMR").Value = PROCESS_TYPE_RELEASE_NAME                    'プロセス区分名：リリース
                .Parameters("JtiFlg_Enable").Value = DATA_YUKO                                      '削除フラグ：有効
                .Parameters("JtiFlg_Enable_NMR").Value = DATA_YUKO_NM                               '削除フラグ名：有効
                .Parameters("JtiFlg_Invaild").Value = DATA_MUKO                                     '削除フラグ:無効
                .Parameters("JtiFlg_Invaild_NMR").Value = DATA_MUKO_NM                              '削除フラグ名：無効
                .Parameters("GroupCD").Value = PropWorkGroupCD                                      'グループコード

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
