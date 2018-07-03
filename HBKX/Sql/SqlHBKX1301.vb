Imports Npgsql
Imports Common
Imports CommonHBK
''' <summary>
''' 設置情報マスター一覧画面Sqlクラス
''' </summary>
''' <remarks>設置情報マスター一覧画面のSQLの作成・設定を行う
''' <para>作成情報：2012/09/03 y.ikushima
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKX1301
    '設置情報マスター一覧取得SQL
    Private strSelectSetInfoMasterSql As String = " SELECT " & vbCrLf & _
                                                                        " SPM.SetBusyoCD AS SetBusyoCD, " & vbCrLf & _
                                                                        " SPM.SetKyokuNM AS SetKyokuNM, " & vbCrLf & _
                                                                        " SPM.SetBusyoNM AS SetBusyoNM, " & vbCrLf & _
                                                                        " SPM.SetRoom AS SetRoom, " & vbCrLf & _
                                                                        " SPM.SetBuil AS SetBuil, " & vbCrLf & _
                                                                        " SPM.SetFloor AS SetFloor, " & vbCrLf & _
                                                                        " CASE WHEN SPM.JtiFlg = '" & JTIFLG_ON & "' THEN '" & DELDATA_DISPLAY_NM & "' " & vbCrLf & _
                                                                        " ELSE '' END AS JtiFlgDisp, " & vbCrLf & _
                                                                        " SPM.JtiFlg AS JtiFlg " & vbCrLf & _
                                                                    " FROM " & vbCrLf & _
                                                                        " setpos_mtb SPM " & vbCrLf & _
                                                                        " ORDER BY SPM.SetBusyoCD "


    ''' <summary>
    ''' 設置情報マスター取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>設置情報マスター取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSoftMasterDataSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection) As Boolean



        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSQL As String

        Try
            'SQL文設定

            '設置情報マスターテーブル取得用SQLを設定
            strSQL = strSelectSetInfoMasterSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

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
