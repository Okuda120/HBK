Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' メールテンプレート選択画面Sqlクラス
''' </summary>
''' <remarks>メールテンプレート選択画面のSQLの作成・設定を行う
''' <para>作成情報：2012/07/24 t.fukuo
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKZ1001

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK


    ''メールテンプレートマスタ取得（SELECT）用SQL
    'Private strSelectMailTemplateMasta As String = _
    '    "SELECT" & vbCrLf & _
    '    "  TemplateNmb" & vbCrLf & _
    '    " ,TemplateNM" & vbCrLf & _
    '    " ,ProcessKbn" & vbCrLf & _
    '    " ,GroupCD" & vbCrLf & _
    '    " ,MailFrom" & vbCrLf & _
    '    " ,MailTo" & vbCrLf & _
    '    " ,CC" & vbCrLf & _
    '    " ,Bcc" & vbCrLf & _
    '    " ,PriorityKbn" & vbCrLf & _
    '    " ,Title" & vbCrLf & _
    '    " ,Text AS MailText" & vbCrLf & _
    '    " ,KigenCondCIKbnCD" & vbCrLf & _
    '    " ,KigenCondTypeKbn" & vbCrLf & _
    '    " ,KigenCondKigen" & vbCrLf & _
    '    " ,KigenCondKbn" & vbCrLf & _
    '    "FROM MAIL_TEMPLATE_MTB" & vbCrLf & _
    '    "WHERE ProcessKbn = :ProcessKbn" & vbCrLf & _
    '    "  AND GroupCD = :GroupCD" & vbCrLf & _
    '    "  AND COALESCE(KigenCondCIKbnCD,'') = :KigenCondCIKbnCD" & vbCrLf & _
    '    "  AND COALESCE(KigenCondTypeKbn,'') = :KigenCondTypeKbn" & vbCrLf & _
    '    "  AND COALESCE(KigenCondKigen,'') = :KigenCondKigen" & vbCrLf & _
    '    "  AND COALESCE(KigenCondKbn,'') = :KigenCondKbn" & vbCrLf & _
    '    "  AND JtiFlg = '0'" & vbCrLf & _
    '    "ORDER BY Sort"

    'メールテンプレートマスタ取得（SELECT）用SQL
    Private strSelectMailTemplateMasta As String = _
        "SELECT" & vbCrLf & _
        "  TemplateNmb" & vbCrLf & _
        " ,TemplateNM" & vbCrLf & _
        " ,ProcessKbn" & vbCrLf & _
        " ,GroupCD" & vbCrLf & _
        " ,MailFrom" & vbCrLf & _
        " ,MailTo" & vbCrLf & _
        " ,CC" & vbCrLf & _
        " ,Bcc" & vbCrLf & _
        " ,PriorityKbn" & vbCrLf & _
        " ,Title" & vbCrLf & _
        " ,Text AS MailText" & vbCrLf & _
        " ,KigenCondCIKbnCD" & vbCrLf & _
        " ,KigenCondTypeKbn" & vbCrLf & _
        " ,KigenCondKigen" & vbCrLf & _
        " ,KigenCondKbn" & vbCrLf & _
        "FROM MAIL_TEMPLATE_MTB" & vbCrLf & _
        "WHERE ProcessKbn = :ProcessKbn" & vbCrLf & _
        "  AND GroupCD = :GroupCD" & vbCrLf & _
        "  AND JtiFlg = '0'" & vbCrLf & _
        "  AND ((COALESCE(KigenCondCIKbnCD,'') = :KigenCondCIKbnCD" & vbCrLf & _
        "  AND COALESCE(KigenCondTypeKbn,'') = :KigenCondTypeKbn" & vbCrLf & _
        "  AND COALESCE(KigenCondKigen,'') = :KigenCondKigen" & vbCrLf & _
        "  AND COALESCE(KigenCondKbn,'') = :KigenCondKbn)" & vbCrLf & _
        "  OR (COALESCE(KigenCondCIKbnCD,'') = ''" & vbCrLf & _
        "  AND COALESCE(KigenCondTypeKbn,'') = ''" & vbCrLf & _
        "  AND COALESCE(KigenCondKigen,'') = ''" & vbCrLf & _
        "  AND COALESCE(KigenCondKbn,'') = ''))" & vbCrLf & _
        "ORDER BY Sort"

    'メールテンプレートマスタ取得（SELECT）用SQL
    Private strSelectMailTemplateMasta_kigen As String = _
        "SELECT" & vbCrLf & _
        "  TemplateNmb" & vbCrLf & _
        " ,TemplateNM" & vbCrLf & _
        " ,ProcessKbn" & vbCrLf & _
        " ,GroupCD" & vbCrLf & _
        " ,MailFrom" & vbCrLf & _
        " ,MailTo" & vbCrLf & _
        " ,CC" & vbCrLf & _
        " ,Bcc" & vbCrLf & _
        " ,PriorityKbn" & vbCrLf & _
        " ,Title" & vbCrLf & _
        " ,Text AS MailText" & vbCrLf & _
        " ,KigenCondCIKbnCD" & vbCrLf & _
        " ,KigenCondTypeKbn" & vbCrLf & _
        " ,KigenCondKigen" & vbCrLf & _
        " ,KigenCondKbn" & vbCrLf & _
        "FROM MAIL_TEMPLATE_MTB" & vbCrLf & _
        "WHERE ProcessKbn = :ProcessKbn" & vbCrLf & _
        "  AND GroupCD = :GroupCD" & vbCrLf & _
        "  AND JtiFlg = '0'" & vbCrLf & _
        "  AND COALESCE(KigenCondCIKbnCD,'') = ''" & vbCrLf & _
        "  AND COALESCE(KigenCondTypeKbn,'') = ''" & vbCrLf & _
        "  AND COALESCE(KigenCondKigen,'') = ''" & vbCrLf & _
        "  AND COALESCE(KigenCondKbn,'') = ''" & vbCrLf & _
        "ORDER BY Sort"

    ''' <summary>
    ''' メールテンプレートマスタ取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKZ1001">[IN]システム登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>メールテンプレートマスタ取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/24 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectMailTemplateMastaSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                  ByVal Cn As NpgsqlConnection, _
                                                  ByVal dataHBKZ1001 As DataHBKZ1001) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            If dataHBKZ1001.PropStrProcMode = PROCMODE_EDIT Then
                strSQL = strSelectMailTemplateMasta
            Else
                strSQL = strSelectMailTemplateMasta_kigen
            End If

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))           'プロセス区分
                .Add(New NpgsqlParameter("GroupCD", NpgsqlTypes.NpgsqlDbType.Varchar))              'グループCD
                .Add(New NpgsqlParameter("KigenCondCIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))     '期限切れ条件CI種別
                .Add(New NpgsqlParameter("KigenCondTypeKbn", NpgsqlTypes.NpgsqlDbType.Varchar))     '期限切れ条件タイプ
                .Add(New NpgsqlParameter("KigenCondKigen", NpgsqlTypes.NpgsqlDbType.Varchar))       '期限切れ条件期限
                .Add(New NpgsqlParameter("KigenCondKbn", NpgsqlTypes.NpgsqlDbType.Varchar))         '期限切れ条件区分
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("ProcessKbn").Value = dataHBKZ1001.PropStrProcessKbn                    'プロセス区分
                .Parameters("GroupCD").Value = dataHBKZ1001.PropStrGroupCD                          'グループCD
                .Parameters("KigenCondCIKbnCD").Value = dataHBKZ1001.PropStrKigenCondCIKbnCD        '期限切れ条件CI種別
                .Parameters("KigenCondTypeKbn").Value = dataHBKZ1001.PropStrKigenCondTypeKbn        '期限切れ条件タイプ
                .Parameters("KigenCondKigen").Value = dataHBKZ1001.PropStrKigenCondKigen            '期限切れ条件期限
                .Parameters("KigenCondKbn").Value = dataHBKZ1001.PropStrKigenCondKbn                '期限切れ条件区分
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
