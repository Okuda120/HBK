Imports Npgsql
Imports Common
Imports CommonHBK

''' <summary>
''' 部所有機器検索一覧(月次報告用出力)Sqlクラス
''' </summary>
''' <remarks>部所有機器検索一覧(月次報告用出力)のSQLの作成・設定を行う
''' <para>作成情報：2012/07/06 s.yamaguchi
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKB1203

    '定数
    'zoo参加有無
    Private Const CBT_ZOOKBN_MU As String = "0"     '無
    Private Const CBT_ZOOKBN_YUU As String = "1"    '有

    '[SELECT]CI部所有機器テーブル【人事連絡用】取得用SQL
    Private strSelectCIBuyTable As String = "SELECT" & vbCrLf & _
                                            " (CASE WHEN cit.CIStatusCD = '" & CI_STATUS_KIKI_RIYOUCHU & _
                                            "'           THEN '●'" & vbCrLf & _
                                            "       WHEN cit.CIStatusCD = '" & CI_STATUS_KIKI_TEISHI & _
                                            "'           THEN '×'" & vbCrLf & _
                                            "       ELSE ''" & vbCrLf & _
                                            "  END) AS CIStatus," & vbCrLf & _
                                            " km.KindNM || cit.Num AS HostNM," & vbCrLf & _
                                            " kim.KikiStateNM," & vbCrLf & _
                                            " kim2.KikiStateNM," & vbCrLf & _
                                            " cbt.FixedIP," & vbCrLf & _
                                            " (CASE WHEN cbt.ZooKbn = '" & CBT_ZOOKBN_MU & _
                                            "'           THEN '無'" & vbCrLf & _
                                            "       WHEN cbt.ZooKbn = '" & CBT_ZOOKBN_YUU & _
                                            "'           THEN '有'" & vbCrLf & _
                                            "       ELSE ''" & vbCrLf & _
                                            "  END) AS ZooKbn," & vbCrLf & _
                                            " cit.Class2," & vbCrLf & _
                                            " cit.CINM," & vbCrLf & _
                                            " cit.Class1," & vbCrLf & _
                                            " OSNM," & vbCrLf & _
                                            " cbt.NIC1," & vbCrLf & _
                                            " cbt.MacAddress1," & vbCrLf & _
                                            " cbt.NIC2," & vbCrLf & _
                                            " cbt.MacAddress2," & vbCrLf & _
                                            " cbt.SetBuil," & vbCrLf & _
                                            " cbt.SetFloor," & vbCrLf & _
                                            " cbt.SetRoom," & vbCrLf & _
                                            " (CASE WHEN cbt.ConnectDT <> ''" & vbCrLf & _
                                            "            THEN TO_CHAR(TO_DATE(cbt.ConnectDT,'YYYYMMDD'),'YYYY/MM/DD')" & vbCrLf & _
                                            "       ELSE ''" & vbCrLf & _
                                            "  END)  AS ConnectDT," & vbCrLf & _
                                            " (CASE WHEN cbt.ExpirationUPDT <> ''" & vbCrLf & _
                                            "            THEN TO_CHAR(TO_DATE(cbt.ExpirationUPDT,'YYYYMMDD'),'YYYY/MM/DD')" & vbCrLf & _
                                            "       ELSE ''" & vbCrLf & _
                                            "  END) AS ExpirationUPDT," & vbCrLf & _
                                            " (CASE WHEN cbt.ExpirationDT <> ''" & vbCrLf & _
                                            "            THEN TO_CHAR(TO_DATE(cbt.ExpirationDT,'YYYYMMDD'),'YYYY/MM/DD')" & vbCrLf & _
                                            "       ELSE ''" & vbCrLf & _
                                            "  END) AS ExpirationDT," & vbCrLf & _
                                            " (CASE WHEN cbt.DeletDT <> ''" & vbCrLf & _
                                            "            THEN TO_CHAR(TO_DATE(cbt.DeletDT,'YYYYMMDD'),'YYYY/MM/DD')" & vbCrLf & _
                                            "       ELSE ''" & vbCrLf & _
                                            "  END) AS DeletDT" & vbCrLf & _
                                            " FROM CI_INFO_TB cit" & vbCrLf & _
                                            " LEFT OUTER JOIN KIND_MTB km ON cit.KindCD = km.KindCD" & vbCrLf & _
                                            " LEFT OUTER JOIN CI_BUY_TB cbt ON cit.CINmb = cbt.CINmb" & vbCrLf & _
                                            " LEFT OUTER JOIN KIKISTATE_MTB kim ON cbt.DNSRegCD = kim.KikiStateCD AND kim.KikiStateKbn = '003'" & vbCrLf & _
                                            " LEFT OUTER JOIN KIKISTATE_MTB kim2 ON cbt.IPUseCD = kim2.KikiStateCD AND kim2.KikiStateKbn = '002'" & vbCrLf & _
                                            " WHERE cit.CIKbnCD = :CIKbnCD" & vbCrLf & _
                                            " ORDER BY cit.Num"

    ''' <summary>
    ''' CI部所有機器テーブル【人事連絡用】取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB1203">[IN]部所有機器検索(人事連絡用出力)データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI部所有機器テーブル【人事連絡用】取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/06 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCIBuyTableSql(ByRef Adapter As NpgsqlDataAdapter, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB1203 As DataHBKB1203) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            '**********************************
            '* SQL文設定
            '**********************************
            'SQL文(SELECT)
            strSQL = strSelectCIBuyTable

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))      'CI種別CD
            Adapter.SelectCommand.Parameters("CIKbnCD").Value = CI_TYPE_KIKI                    'CI種別CD

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