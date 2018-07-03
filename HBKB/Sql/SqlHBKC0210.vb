Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' 最新連携情報表示画面Sqlクラス
''' </summary>
''' <remarks>最新連携情報表示画面のSQLの作成・設定を行う
''' <para>作成情報：2012/09/12 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKC0210

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK

    'インシデントSM通知テーブル取得（SELECT）SQL
    Private strSelectIncidentSMtutiSql As String = "SELECT " & vbCrLf & _
                                                " ist.SMNmb " & vbCrLf & _
                                                ",ist.IncNmb " & vbCrLf & _
                                                ",ist.IncState " & vbCrLf & _
                                                ",ist.UsrBusyoNM " & vbCrLf & _
                                                ",ist.IraiUsr " & vbCrLf & _
                                                ",ist.Tel " & vbCrLf & _
                                                ",ist.MailAdd " & vbCrLf & _
                                                ",ist.Title " & vbCrLf & _
                                                ",ist.UkeNaiyo " & vbCrLf & _
                                                ",ist.IncTantoNM " & vbCrLf & _
                                                ",ist.Kind " & vbCrLf & _
                                                ",ist.Category " & vbCrLf & _
                                                ",ist.SubCategory " & vbCrLf & _
                                                ",ist.Impact " & vbCrLf & _
                                                ",ist.UsrSyutiClass " & vbCrLf & _
                                                ",ist.Genin " & vbCrLf & _
                                                ",ist.ZanteisyotiNaiyo " & vbCrLf & _
                                                ",ist.Solution " & vbCrLf & _
                                                ",ist.BikoS1 " & vbCrLf & _
                                                ",ist.BikoS2 " & vbCrLf & _
                                                ",ist.BikoM1 " & vbCrLf & _
                                                ",ist.BikoM2 " & vbCrLf & _
                                                ",ist.BikoL1 " & vbCrLf & _
                                                ",ist.BikoL2 " & vbCrLf & _
                                                ",CASE WHEN ist.YobiDT1 IS NULL THEN '' " & vbCrLf & _
                                                " ELSE TO_CHAR(ist.YobiDT1,'yyyy/mm/dd hh24:mi:ss') END AS YobiDT1 " & vbCrLf & _
                                                ",CASE WHEN ist.YobiDT2 IS NULL THEN '' " & vbCrLf & _
                                                " ELSE TO_CHAR(ist.YobiDT2,'yyyy/mm/dd hh24:mi:ss') END AS YobiDT2 " & vbCrLf & _
                                                ",CASE WHEN ist.RenkeiDT IS NULL THEN '' " & vbCrLf & _
                                                " ELSE TO_CHAR(ist.RenkeiDT,'yyyy/mm/dd hh24:mi:ss') END AS RenkeiDT " & vbCrLf & _
                                                ",CASE ist.RenkeiKbn " & vbCrLf & _
                                                " WHEN :RenkeiKbnTOSM THEN :RenkeiKbnTOSM_NM " & vbCrLf & _
                                                " WHEN :RenkeiKbnTOHBK THEN :RenkeiKbnTOHBK_NM " & vbCrLf & _
                                                " ELSE '' END AS RenkeiKbn " & vbCrLf & _
                                                "FROM incident_sm_tuti_tb ist " & vbCrLf & _
                                                "WHERE ist.IncNmb = :IncNmb "

    ''' <summary>
    ''' インシデントSM通知テーブル取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0210">[IN]最新連携情報表示画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>インシデントSM通知テーブル取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectIncidentSMtutiSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKC0210 As DataHBKC0210) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            'SQL文(SELECT)
            strSQL = strSelectIncidentSMtutiSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("RenkeiKbnTOSM", NpgsqlTypes.NpgsqlDbType.Varchar))        '連携区分：1
                .Add(New NpgsqlParameter("RenkeiKbnTOSM_NM", NpgsqlTypes.NpgsqlDbType.Varchar))     '連携区分名称：ひびき⇒SM
                .Add(New NpgsqlParameter("RenkeiKbnTOHBK", NpgsqlTypes.NpgsqlDbType.Varchar))       '連携区分：2
                .Add(New NpgsqlParameter("RenkeiKbnTOHBK_NM", NpgsqlTypes.NpgsqlDbType.Varchar))    '連携区分名称：SM⇒ひびき
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))               'インシデント番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("RenkeiKbnTOSM").Value = RENKEIKBN_HBKTOSM             '連携区分：1
                .Parameters("RenkeiKbnTOSM_NM").Value = RENKEIKBN_HBKTOSM_NAME     '連携区分名称：ひびき⇒SM
                .Parameters("RenkeiKbnTOHBK").Value = RENKEIKBN_SMTOHBK            '連携区分：2
                .Parameters("RenkeiKbnTOHBK_NM").Value = RENKEIKBN_SMTOHBK_NAME    '連携区分名称：SM⇒ひびき
                .Parameters("IncNmb").Value = dataHBKC0210.PropIntINCNmb           'インシデント番号
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
