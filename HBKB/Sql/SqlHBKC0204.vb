Imports Common
Imports CommonHBK
Imports Npgsql
''' <summary>
''' インシデント登録画面(預かり誓約書出力)SQLクラス
''' </summary>
''' <remarks>インシデント登録画面(預かり誓約書出力)のSQLを定義したクラス
''' <para>作成情報：2012/07/24 k.ueda
''' <p>改定情報：</p>
''' </para></remarks>
Public Class SqlHBKC0204

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK

    'CIサポセン機器履歴取得（SELECT）SQL
    Private strSelectCISapSql As String = "SELECT " & vbCrLf & _
                                          " CASE COALESCE(csr.RentalStDT,'') WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(csr.RentalStDT,'YYYYMMDD'),'YYYY/MM/DD') END AS RentalStDT" & vbCrLf & _
                                          ",csr.UsrID" & vbCrLf & _
                                          ",csr.UsrNM" & vbCrLf & _
                                          ",csr.UsrBusyoNM" & vbCrLf & _
                                          ",csr.UsrRoom" & vbCrLf & _
                                          ",csr.Fuzokuhin" & vbCrLf & _
                                          ",CASE COALESCE(csr.RentalEdDT,'') WHEN '' THEN '' ELSE TO_CHAR(TO_DATE(csr.RentalEdDT,'YYYYMMDD'),'YYYY/MM/DD') END AS RentalEdDT" & vbCrLf & _
                                          "FROM ci_sap_rtb csr " & vbCrLf & _
                                          "WHERE csr.CINmb = :CINmb " & vbCrLf & _
                                          "and csr.RirekiNo = :RirekiNo "

    ''' <summary>
    ''' CIサポセン機器履歴取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0204">[IN]インシデント登録（預かり誓約書）データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIサポセン機器履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/24 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCISupportSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                   ByVal Cn As NpgsqlConnection, _
                                                   ByVal dataHBKC0204 As DataHBKC0204) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectCISapSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'CI番号
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))
            Adapter.SelectCommand.Parameters("CINmb").Value = dataHBKC0204.PropIntCINmb
            '履歴番号
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))
            Adapter.SelectCommand.Parameters("RirekiNo").Value = dataHBKC0204.PropIntRirekiNo

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
