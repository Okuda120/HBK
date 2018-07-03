Imports Npgsql
Imports Common
Imports CommonHBK

Public Class LogicHBKZ0501
    Private sqlHBKZ0501 As New SqlHBKZ0501

    ''' <summary>
    ''' スプレッドシート列表示処理
    ''' </summary>
    ''' <param name="dataHBKZ0501">DataHBKZ0501型オブジェクト</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>スプレッドシートに表示する列を制御する
    ''' <para>作成情報：202/06/15 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Public Function ViewColumn(ByRef dataHBKZ0501 As DataHBKZ0501) As Boolean

        Try

            ' スプレッドの描画を停止
            dataHBKZ0501.PropVwList.SuspendLayout()

            ' 全ての列を非表示にする
            For Each col As FarPoint.Win.Spread.Column In dataHBKZ0501.PropVwList.ActiveSheet.Columns
                col.Visible = False
            Next

            '' 列を表示する
            'For i As Integer = 1 To 5
            '    With dataHBKZ0501.PropVwList.ActiveSheet
            '        If .Columns.Count < i Then
            '            Exit For
            '        End If

            '        .Columns(i).Visible = True
            '    End With
            'Next

            ' 列を表示する
            For i As Integer = 1 To 7
                With dataHBKZ0501.PropVwList.ActiveSheet
                    If .Columns.Count < i Then
                        Exit For
                    End If
                    If i = 6 Then
                        .Columns(i).Visible = False
                    Else
                        .Columns(i).Visible = True
                    End If

                End With
            Next

            Return True
        Catch ex As Exception
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            ' スプレッドの描画を再開
            dataHBKZ0501.PropVwList.ResumeLayout(True)
        End Try


    End Function

    ''' <summary>
    ''' 検索ボタン押下時メイン処理
    ''' </summary>
    ''' <param name="dataHBKZ0501">DataHBKZ0501型オブジェクト</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>設置情報マスターからレコードを取得する
    ''' <para>作成情報：2012/06/11 nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Public Function SearchMain(ByRef dataHBKZ0501 As DataHBKZ0501) As Boolean

        ' 開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim Cn As New NpgsqlConnection(DbString)
        Dim Adapter As New NpgsqlDataAdapter
        Dim Table As New DataTable

        Try
            Cn.Open()

            ' 設置情報一覧用SQLの作成・設定
            If sqlHBKZ0501.SetSelectSetPosSql(Adapter, Cn, dataHBKZ0501) = False Then
                Return False
            End If

            ' 開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "設置情報マスタ取得", Nothing, Adapter.SelectCommand)

            ' データを取得
            Adapter.Fill(Table)

            ' 取得したデータをデータクラスへ保存
            dataHBKZ0501.PropVwList.DataSource = Table
            dataHBKZ0501.PropCount.Text = Table.Rows.Count & "件"


            With dataHBKZ0501.PropVwList.Sheets(0)
                For i As Integer = 0 To .Rows.Count - 1 Step 1
                    If .GetValue(i, 7) = DELDATA_DISPLAY_NM Then
                        '削除データ行はグレーに変更
                        .Rows(i).BackColor = Color.Silver
                    Else
                        .Rows(i).BackColor = Color.White
                    End If
                Next
            End With

            ' ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True
        Catch ex As Exception
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Table.Dispose()
            Adapter.Dispose()
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 検索ボタン押下時件数取得処理
    ''' </summary>
    ''' <param name="dataHBKZ0501">DataHBKZ0501型オブジェクト</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>設置情報マスターからレコードを取得する
    ''' <para>作成情報：2012/06/11 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Public Function SearchCountMain(ByVal dataHBKZ0501 As DataHBKZ0501) As Boolean

        ' 開始ログ出力
        Common.CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        ' 変数宣言
        Dim Cn As New NpgsqlConnection(DbString)
        Dim Adapter As New NpgsqlDataAdapter
        Dim Table As New DataTable

        Try
            Cn.Open()
            ' 設置情報件数取得用SQLの作成・設定
            If sqlHBKZ0501.SetSelectSetPosCountSql(Adapter, Cn, dataHBKZ0501) = False Then
                Return False
            End If

            ' ログ出力
            Common.CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "設置情報マスタ件数取得", Nothing, Adapter.SelectCommand)

            ' データを取得
            Adapter.Fill(Table)

            ' 取得したデータをデータクラスへ保存
            dataHBKZ0501.PropSearchCount = DirectCast(Table.Rows(0)(0), Long)

            ' 0件の場合
            If dataHBKZ0501.PropSearchCount = 0L Then
                ClearSpreadRow(dataHBKZ0501)
            End If

            ' ログ出力
            Common.CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True

        Catch ex As Exception
            Common.CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Table.Dispose()
            Adapter.Dispose()
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 初期表示検索件数取得処理
    ''' </summary>
    ''' <param name="dataHBKZ0501">DataHBKZ0501型オブジェクト</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>設置情報マスタからレコードを取得する
    ''' <para>作成情報：2012/06/11 nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Public Function GetListCount(ByRef dataHBKZ0501 As DataHBKZ0501) As Boolean
        ' 開始ログ出力
        Common.CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        ' 変数宣言
        Dim Cn As New NpgsqlConnection(DbString)
        Dim Adapter As New NpgsqlDataAdapter
        Dim Table As New DataTable

        Try
            Cn.Open()

            ' 設置情報件数取得用SQLの作成・設定
            If sqlHBKZ0501.SetInitSelectSetPosCountSql(Adapter, Cn, dataHBKZ0501) = False Then
                Return False
            End If

            ' ログ出力
            Common.CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "設置情報マスタ件数取得", Nothing, Adapter.SelectCommand)

            ' データを取得
            Adapter.Fill(Table)

            ' 取得したデータをデータクラスへ保存
            dataHBKZ0501.PropSearchCount = DirectCast(Table.Rows(0)(0), Long)

            ' 終了ログ出力
            Common.CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True
        Catch ex As Exception
            Common.CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            Table.Dispose()
            Adapter.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 初期表示検索処理
    ''' </summary>
    ''' <param name="dataHBKZ0501">DataHBKZ0501型オブジェクト</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>設置情報マスタからレコードを取得する
    ''' <para>作成情報：2012/06/11 nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Public Function InitFormMain(ByRef dataHBKZ0501 As DataHBKZ0501) As Boolean

        ' 開始ログ出力
        Common.CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        ' 変数宣言
        Dim Cn As New NpgsqlConnection(DbString)
        Dim Adapter As New NpgsqlDataAdapter()
        Dim Table As New DataTable()

        Try
            Cn.Open()

            ' 設置情報取得用SQLの作成・設定
            If sqlHBKZ0501.SetInitSelectSetPosSql(Adapter, Cn, dataHBKZ0501) = False Then
                Return False
            End If

            ' 終了ログ出力
            Common.CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "設置情報マスタ取得", Nothing, Adapter.SelectCommand)

            ' データを取得
            Adapter.Fill(Table)

            ' 取得したデータを設定する
            dataHBKZ0501.PropVwList.DataSource = Table

            ' 件数を設定する
            dataHBKZ0501.PropCount.Text = Table.Rows.Count & "件"

            ' 終了ログ出力
            Common.CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True

        Catch ex As Exception
            Common.CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Table.Dispose()
            Adapter.Dispose()
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' スプレッドシートのすべての行を削除する
    ''' </summary>
    ''' <param name="dataHBKZ0501">DataHBKZ0501型オブジェクト</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks></remarks>
    Public Function ClearSpreadRow(ByRef dataHBKZ0501 As DataHBKZ0501) As Boolean
        ' 開始ログ出力
        Common.CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            If dataHBKZ0501.PropVwList.ActiveSheet.RowCount > 0 Then
                dataHBKZ0501.PropVwList.ActiveSheet.RemoveRows(0, dataHBKZ0501.PropVwList.ActiveSheet.RowCount)
            End If
            dataHBKZ0501.PropCount.Text = "0件"

            ' 終了ログ出力
            Common.CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
        Catch ex As Exception
            Common.CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try
    End Function

End Class
