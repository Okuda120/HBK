Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.Text

''' <summary>
''' ノウハウURL選択画面Sqlクラス
''' </summary>
''' <remarks>ノウハウURL選択画面のSQLの作成・設定を行う
''' <para>作成情報：2012/07/23 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKC0501

    Dim commonLogicHBK As New CommonLogicHBK

    'ノウハウURL情報取得SQL
    Private strSelectKnowhowSearch As String = "SELECT " & vbCrLf & _
                                                " kt.UrlNaiyo " & vbCrLf &
                                                ",kt.Url " & vbCrLf & _
                                                "FROM knowhowurl_tb kt " & vbCrLf & _
                                                "WHERE kt.CINmb = :CINmb " & vbCrLf

    ''' <summary>
    ''' ノウハウURL情報テーブル取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0501">[IN]ノウハウURL選択画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>>ノウハウURL情報テーブル取得用のSQLを作成し、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2012/07/23 k.imayama
    ''' </para></remarks>
    Public Function SetSelectKnowhowSearchSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKC0501 As DataHBKC0501) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectKnowhowSearch

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))
            Adapter.SelectCommand.Parameters("CINmb").Value = dataHBKC0501.PropCINmb

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
