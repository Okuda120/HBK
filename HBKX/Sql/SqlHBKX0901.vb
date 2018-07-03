Imports Npgsql
Imports Common
Imports CommonHBK
''' <summary>
''' ソフトマスター一覧画面Sqlクラス
''' </summary>
''' <remarks>ソフトマスター一覧画面のSQLの作成・設定を行う
''' <para>作成情報：2012/08/29 k.ueda
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKX0901

    Private strSelectSoftMastersql As String = "SELECT " & vbCrLf & _
                                               "sm.SoftCD, " & vbCrLf & _
                                               "CASE " & vbCrLf & _
                                                  "WHEN sm.SoftKbn = :SoftKbnOS " & vbCrLf & _
                                                  "THEN :SoftKbnNMOS " & vbCrLf & _
                                                  "WHEN sm.SoftKbn = :SoftKbnOptSoft " & vbCrLf & _
                                                  "THEN :SoftKbnNMOptSoft " & vbCrLf & _
                                                  "WHEN sm.SoftKbn = :SoftKbnAntiVirus " & vbCrLf & _
                                                  "THEN :SoftKbnNMAntiVirus " & vbCrLf & _
                                                  "ELSE '' " & vbCrLf & _
                                               "END AS SoftKbn, " & vbCrLf & _
                                               "sm.SoftNM, " & vbCrLf & _
                                               "CASE " & vbCrLf & _
                                                   "WHEN sm.JtiFlg = :JtiFlgON " & vbCrLf & _
                                                   "THEN '' " & vbCrLf & _
                                                   "WHEN sm.JtiFlg = :JtiFlgOFF " & vbCrLf & _
                                                   "THEN :JtiFlgOFFDisplay " & vbCrLf & _
                                                   "ELSE '' " & vbCrLf & _
                                               "END AS JtiFlg, " & vbCrLf & _
                                               "sm.JtiFlg AS JtiFlgKAKUSHI, " & vbCrLf & _
                                               "sm.SoftKbn AS SoftKbnKAKUSHI " & vbCrLf & _
                                               "FROM SOFT_MTB AS sm " & vbCrLf & _
                                               "ORDER BY sm.SoftCD ASC "


    ''' <summary>
    ''' ソフトマスター取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0901">[IN]ソフトマスター一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ソフトマスター取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/28 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSoftMasterDataSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKX0901 As DataHBKX0901) As Boolean



        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSQL As String



        Try
            '**********************************
            '* SQL文設定
            '**********************************

            'ソフトマスターテーブル取得用SQLを設定
            strSQL = strSelectSoftMastersql

            'データアダプタに、SQLのSELECT文を設定

            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '**********************************
            '* バインド変数に型と値をセット
            '**********************************

            'ソフト区分(OS)
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SoftKbnOS", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("SoftKbnOS").Value = SOFTKBN_OS
            'ソフト区分名(OS)
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SoftKbnNMOS", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("SoftKbnNMOS").Value = SOFTKBN_OS_NM
            'ソフト区分(オプションソフト)
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SoftKbnOptSoft", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("SoftKbnOptSoft").Value = SOFTKBN_OPTIONSOFT
            'ソフト区分名(オプションソフト)
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SoftKbnNMOptSoft", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("SoftKbnNMOptSoft").Value = SOFTKBN_OPTIONSOFT_NM
            'ソフト区分(ウイルス対策ソフト)
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SoftKbnAntiVirus", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("SoftKbnAntiVirus").Value = SOFTKBN_UNTIVIRUSSOFT
            'ソフト区分名(ウイルス対策ソフト)
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SoftKbnNMAntiVirus", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("SoftKbnNMAntiVirus").Value = SOFTKBN_UNTIVIRUSSOFT_NM
            '削除フラグ(データ有効)
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("JtiFlgON", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("JtiFlgON").Value = DATA_YUKO
            '削除フラグ(データ無効)
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("JtiFlgOFF", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("JtiFlgOFF").Value = DATA_MUKO
            '削除フラグ(データ無効時記号'○')
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("JtiFlgOFFDisplay", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("JtiFlgOFFDisplay").Value = DELDATA_DISPLAY_NM


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
