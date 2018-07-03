Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.Net

''' <summary>
''' ノウハウURL選択画面ロジッククラス
''' </summary>
''' <remarks>ノウハウURL選択画面のロジックを定義したクラス
''' <para>作成情報：2012/07/23 k.imayama
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKC0501

    'インスタンス作成
    Private sqlHBKC0501 As New SqlHBKC0501
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    '定数宣言
    'ノウハウURL一覧列番号
    Public Const COL_KNOWHOWLIST_NAIYO As Integer = 0       '説明
    Public Const COL_KNOWHOWLIST_URL As Integer = 1         'URL（非表示）

    ''' <summary>
    ''' 画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0501">[IN/OUT]ノウハウURL選択画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面の初期表示設定を行う
    ''' <para>作成情報：2012/07/23 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKC0501 As DataHBKC0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'スプレッド用データテーブル作成処理
        If CreateDataTable(dataHBKC0501) = False Then
            Return False
        End If

        '検索結果取得処理
        If GetSearchData(dataHBKC0501) = False Then
            Return False
        End If

        'スプレッド出力データ設定処理
        If SetVwData(dataHBKC0501) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' スプレッド用データテーブル作成処理
    ''' </summary>
    ''' <param name="dataHBKC0501">[IN/OUT]ノウハウURL選択画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドに出力するためのデータテーブルの作成を行う
    ''' <para>作成情報：2012/07/23 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateDataTable(ByRef dataHBKC0501 As DataHBKC0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtKnowhow As New DataTable 'ノウハウURL検索結果用データテーブル

        Try

            'ノウハウURL検索結果用データテーブル作成
            With dtKnowhow
                .Columns.Add("UrlNaiyo", Type.GetType("System.String"))     '説明
                .Columns.Add("Url", Type.GetType("System.String"))          'URL

                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'データクラスをデータテーブルにセット
            dataHBKC0501.PropDtKnowhow = dtKnowhow

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            dtKnowhow.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 検索結果取得処理
    ''' </summary>
    ''' <param name="dataHBKC0501">[IN/OUT]ノウハウURL選択画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ノウハウURL選択画面の初期表示に必要なデータを取得する
    ''' <para>作成情報：2012/07/23 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetSearchData(ByRef dataHBKC0501 As DataHBKC0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try
            'コネクションを開く
            Cn.Open()

            'ノウハウURL情報取得（スプレッド用）
            If GetKnowhowTable(Adapter, Cn, dataHBKC0501) = False Then
                Return False
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Adapter.Dispose()
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' スプレッド用ノウハウURL情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]データアダプター</param>
    ''' <param name="Cn">[IN]コネクション</param>
    ''' <param name="dataHBKC0501">[IN/OUT]ノウハウURL選択画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ノウハウURL選択画面の初期表示に必要なデータを取得する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetKnowhowTable(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef dataHBKC0501 As DataHBKC0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'SQLの作成・設定
            If sqlHBKC0501.SetSelectKnowhowSearchSql(Adapter, Cn, dataHBKC0501) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ノウハウURL情報取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKC0501.PropDtKnowhow)

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    '''スプレッドの出力データ設定処理
    ''' </summary>
    ''' <param name="dataHBKC0501">[IN/OUT]ノウハウURL選択画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>テーブルから取得した内容をスプレッドに設定する
    ''' <para>作成情報：2012/07/23 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetVwData(ByRef dataHBKC0501 As DataHBKC0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0501

                '検索結果
                With .PropVwKnowhowUrlList.Sheets(0)

                    '各列のデータフィールドを設定し、データを表示
                    .Columns(COL_KNOWHOWLIST_NAIYO).DataField = "UrlNaiyo"      '説明
                    .Columns(COL_KNOWHOWLIST_URL).DataField = "Url"             'URL
                    .DataSource = dataHBKC0501.PropDtKnowhow

                    '隠し列をアクティブに設定　※行の初期選択状態を解除するため
                    .ActiveColumnIndex = COL_KNOWHOWLIST_URL

                End With

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' ノウハウURL一覧選択時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0501">[IN/OUT]ノウハウURL選択画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ノウハウURL一覧の選択行を削除する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SelectRowMain(ByRef dataHBKC0501 As DataHBKC0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行判定処理
        If SelectRowCheck(dataHBKC0501) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 選択行判定処理
    ''' </summary>
    ''' <param name="dataHBKC0501">[IN/OUT]ノウハウURL選択画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ノウハウURL一覧の選択有無を判定する
    ''' <para>作成情報：2012/07/23 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectRowCheck(ByRef dataHBKC0501 As DataHBKC0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRow As Integer   '選択行番号
        Dim intSelectedCol As Integer   '選択列番号

        Try

            With dataHBKC0501.PropVwKnowhowUrlList.Sheets(0)

                '選択行・列取得
                intSelectedRow = .ActiveRowIndex
                intSelectedCol = .ActiveColumnIndex

                '選択行がある場合
                If .RowCount > 0 AndAlso intSelectedCol = COL_KNOWHOWLIST_NAIYO Then

                    ' URLデコード
                    Dim StrDecode As String = WebUtility.HtmlDecode(.Cells(intSelectedRow, COL_KNOWHOWLIST_URL).Value)
                    'ブラウザ起動
                    Process.Start(StrDecode)

                Else
                    'エラーメッセージ表示
                    puErrMsg = C0501_E001
                    Return False
                End If

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As System.ComponentModel.Win32Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = C0501_E002
            Return False
        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

End Class
