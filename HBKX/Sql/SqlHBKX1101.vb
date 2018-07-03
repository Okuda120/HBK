Imports Npgsql
Imports Common
Imports CommonHBK
''' <summary>
''' イメージマスター一覧画面Sqlクラス
''' </summary>
''' <remarks>イメージマスター一覧画面のSQLの作成・設定を行う
''' <para>作成情報：2012/09/03 k.ueda
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKX1101


    Private strSelectImageMastersql As String = "SELECT " & vbCrLf & _
                                                "im.ImageNmb, " & vbCrLf & _
                                                "im.ImageNM, " & vbCrLf & _
                                                "im.Kind, " & vbCrLf & _
                                                "im.Maker, " & vbCrLf & _
                                                "im.KisyuNM, " & vbCrLf & _
                                                "im.OSNM, " & vbCrLf & _
                                                "im.SP, " & vbCrLf & _
                                                "im.Type, " & vbCrLf & _
                                                "im.Notes, " & vbCrLf & _
                                                "CASE " & vbCrLf & _
                                                    "WHEN im.JtiFlg = :JtiFlgON " & vbCrLf & _
                                                    "THEN '' " & vbCrLf & _
                                                    "WHEN im.JtiFlg = :JtiFlgOFF " & vbCrLf & _
                                                    "THEN :JtiFlgOFFDisplay " & vbCrLf & _
                                                    "ELSE '' " & vbCrLf & _
                                                "END AS JtiFlg " & vbCrLf & _
                                                "FROM IMAGE_MTB AS im " & vbCrLf & _
                                                "ORDER BY CAST(im.ImageNmb AS INTEGER) ASC "


    ''' <summary>
    ''' イメージマスター取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX1101">[IN]イメージマスター一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>イメージマスター取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/03 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectImageMasterDataSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKX1101 As DataHBKX1101) As Boolean



        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSQL As String



        Try
            '**********************************
            '* SQL文設定
            '**********************************

            'イメージマスターテーブル取得用SQLを設定
            strSQL = strSelectImageMastersql

            'データアダプタに、SQLのSELECT文を設定

            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '**********************************
            '* バインド変数に型と値をセット
            '**********************************

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
