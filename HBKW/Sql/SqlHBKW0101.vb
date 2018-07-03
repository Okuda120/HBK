Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.Text

''' <summary>
''' ナレッジURL選択画面Sqlクラス
''' </summary>
''' <remarks>ナレッジURL選択画面のSQLの作成・設定を行う
''' <para>作成情報：2012/09/04 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKW0101

    Dim commonLogicHBK As New CommonLogicHBK

    'ナレッジURL情報取得SQL
    Private strSelectKnowhowSearch As String = "SELECT " & vbCrLf & _
                                                " km.UrlNaiyo " & vbCrLf &
                                                ",km.RegDT " & vbCrLf & _
                                                ",km.Url " & vbCrLf & _
                                                "FROM knowledge_url_mtb km " & vbCrLf & _
                                                "WHERE km.JtiFlg <> '1' " & vbCrLf & _
                                                "ORDER BY km.RegDT DESC "

    ''' <summary>
    ''' ナレッジURL情報テーブル取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKW0101">[IN]ナレッジURL選択画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>>ナレッジURL情報テーブル取得用のSQLを作成し、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2012/09/04 k.imayama
    ''' </para></remarks>
    Public Function SetSelectKnowhowSearchSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKW0101 As DataHBKW0101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectKnowhowSearch

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
