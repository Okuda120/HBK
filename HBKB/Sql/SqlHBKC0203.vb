Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' インシデント登録（期限更新誓約書出力）Sqlクラス
''' </summary>
''' <remarks>インシデント登録（期限更新誓約書出力）のSQLの作成・設定を行う
''' <para>作成情報：2012/07/23 s.tsuruta
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKC0203

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

    '複数人利用履歴取得（SELECT）SQL
    Private strSelectShareSql As String = "SELECT " & vbCrLf & _
                                          " sr.UsrID" & vbCrLf & _
                                          ",sr.UsrNM" & vbCrLf & _
                                          ",em.EndUsrBusyoNM" & vbCrLf & _
                                          "FROM share_rtb sr " & vbCrLf & _
                                          "LEFT JOIN ENDUSR_MTB em " & vbCrLf & _
                                          "ON sr.UsrID = em.EndUsrID " & vbCrLf & _
                                          "WHERE sr.CINmb = :CINmb " & vbCrLf & _
                                          "and sr.RirekiNo = :RirekiNo " & vbCrLf &
                                          "ORDER BY em.Sort"

    ''' <summary>
    ''' CIサポセン機器履歴取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0203">[IN]インシデント登録（貸出誓約書）データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIサポセン機器履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/23 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCISupportSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                   ByVal Cn As NpgsqlConnection, _
                                                   ByVal dataHBKC0203 As DataHBKC0203) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectCISapSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CI番号
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))     '履歴番号


            Adapter.SelectCommand.Parameters("CINmb").Value = dataHBKC0203.PropIntCINmb                                 'CI番号
            Adapter.SelectCommand.Parameters("RirekiNo").Value = dataHBKC0203.PropIntRirekiNo                           '履歴番号

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
    ''' 複数人利用履歴取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0203">[IN]インシデント登録（貸出誓約書）データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>複数人利用履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/23 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectShareSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                   ByVal Cn As NpgsqlConnection, _
                                                   ByVal dataHBKC0203 As DataHBKC0203) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectShareSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CI番号
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))     '履歴番号


            Adapter.SelectCommand.Parameters("CINmb").Value = dataHBKC0203.PropIntCINmb                                 'CI番号
            Adapter.SelectCommand.Parameters("RirekiNo").Value = dataHBKC0203.PropIntRirekiNo                           '履歴番号

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
