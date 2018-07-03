Imports Npgsql
Imports Common
Imports CommonHBK
Imports System.Text

''' <summary>
''' 問題（メール作成）Sqlクラス
''' </summary>
''' <remarks>問題（メール作成）のSQLの作成・設定を行う
''' <para>作成情報：2012/08/17 y.ikushima
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKD0203

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK

    'CI番号取得SQL
    Private strSelectCIInfoSql As String = " SELECT " & vbCrLf & _
                                                        " Class1 || '-' || Class2 || '-' || CINM " & vbCrLf & _
                                                    " FROM " & vbCrLf & _
                                                        " ci_info_tb " & vbCrLf & _
                                                    " WHERE CINmb = :CINmb"


    ''' <summary>
    ''' CI番号取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="IntSysNmb">[IN]システム番号</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI番号取得用SQLし、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2012/08/08 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SelectCIInfoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal IntSysNmb As Integer) As Boolean

        'CI共通情報テーブル取得用SQLを設定
        Dim strSql As String = strSelectCIInfoSql

        Try

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSql, Cn)
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))
            Adapter.SelectCommand.Parameters("CINmb").Value = IntSysNmb

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False

        End Try

    End Function


End Class
