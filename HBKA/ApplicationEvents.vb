Imports System.IO

Namespace My

    ' 次のイベントは MyApplication に対して利用できます:
    ' 
    ' Startup: アプリケーションが開始されたとき、スタートアップ フォームが作成される前に発生します。
    ' Shutdown: アプリケーション フォームがすべて閉じられた後に発生します。このイベントは、通常の終了以外の方法でアプリケーションが終了されたときには発生しません。
    ' UnhandledException: ハンドルされていない例外がアプリケーションで発生したときに発生するイベントです。
    ' StartupNextInstance: 単一インスタンス アプリケーションが起動され、それが既にアクティブであるときに発生します。 
    ' NetworkAvailabilityChanged: ネットワーク接続が接続されたとき、または切断されたときに発生します。
    Partial Friend Class MyApplication

        Private Sub MyApplication_Startup(sender As Object, e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            'CHF削除処理
            Try
                System.IO.File.Delete(System.IO.Path.Combine(Path.Combine(My.Application.Info.DirectoryPath, CommonHBK.OUTPUT_DIR_PCANY), CommonHBK.PCANY_CHF_NAME))
            Catch ex As Exception

            End Try

        End Sub

        Private Sub MyApplication_Shutdown(sender As Object, e As System.EventArgs) Handles Me.Shutdown
            'CHF削除処理
            Try
                System.IO.File.Delete(System.IO.Path.Combine(Path.Combine(My.Application.Info.DirectoryPath, CommonHBK.OUTPUT_DIR_PCANY), CommonHBK.PCANY_CHF_NAME))
            Catch ex As Exception

            End Try

        End Sub

        Private Sub MyApplication_UnhandledException(sender As Object, e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            'CHF削除処理
            Try
                System.IO.File.Delete(System.IO.Path.Combine(Path.Combine(My.Application.Info.DirectoryPath, CommonHBK.OUTPUT_DIR_PCANY), CommonHBK.PCANY_CHF_NAME))
            Catch ex As Exception

            End Try

        End Sub
    End Class


End Namespace

