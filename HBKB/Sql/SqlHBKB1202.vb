Imports Npgsql
Imports Common
Imports CommonHBK

''' <summary>
''' 部所有機器検索一覧(人事連絡用出力)Sqlクラス
''' </summary>
''' <remarks>部所有機器検索一覧(人事連絡用出力)のSQLの作成・設定を行う
''' <para>作成情報：2012/06/20 s.yamaguchi
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKB1202

    '[SELECT]CI部所有機器テーブル取得SQL
    Private strSelectCIBuyTable As String = "SELECT" & vbCrLf & _
                                            " km.KindNM || cit.Num AS HostNM," & vbCrLf & _
                                            " cit.Class2," & vbCrLf & _
                                            " cit.CINM," & vbCrLf & _
                                            " cbt.SetBuil," & vbCrLf & _
                                            " cbt.SetFloor," & vbCrLf & _
                                            " cbt.SetRoom," & vbCrLf & _
                                            " cbt.UsrID," & vbCrLf & _
                                            " cbt.UsrNM," & vbCrLf & _
                                            " cbt.UsrMailAdd," & vbCrLf & _
                                            " cbt.UsrKyokuNM," & vbCrLf & _
                                            " cbt.UsrBusyoNM" & vbCrLf & _
                                            " FROM CI_INFO_TB AS cit" & vbCrLf & _
                                            " LEFT OUTER JOIN KIND_MTB km ON cit.KindCD = km.KindCD" & vbCrLf & _
                                            " LEFT OUTER JOIN CI_BUY_TB cbt ON cit.CINmb = cbt.CINmb" & vbCrLf & _
                                            " WHERE cit.CIKbnCD = :CIKbnCD" & vbCrLf & _
                                            " AND cit.CIStatusCD = :CIStatusCD" & vbCrLf & _
                                            " ORDER BY cit.Num"

    ''' <summary>
    ''' CI部所有機器テーブル取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB1202">[IN]部所有機器検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI部所有機器テーブル取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/03 s.yaamguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCIBuyTableSql(ByRef Adapter As NpgsqlDataAdapter, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB1202 As DataHBKB1202) As Boolean

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
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CIStatusCD", NpgsqlTypes.NpgsqlDbType.Varchar))   'CIステータスCD

            Adapter.SelectCommand.Parameters("CIKbnCD").Value = CI_TYPE_KIKI                    'CI種別CD
            Adapter.SelectCommand.Parameters("CIStatusCD").Value = CI_STATUS_KIKI_RIYOUCHU      'CIステータスCD

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
