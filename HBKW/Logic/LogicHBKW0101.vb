Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.Net

''' <summary>
''' ナレッジURL選択画面ロジッククラス
''' </summary>
''' <remarks>ナレッジURL選択画面のロジックを定義したクラス
''' <para>作成情報：2012/09/04 k.imayama
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKW0101

    'インスタンス作成
    Private sqlHBKW0101 As New SqlHBKW0101
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    '定数宣言
    'ナレッジURL一覧列番号
    Public Const COL_KNOWHOWLIST_NAIYO As Integer = 0       '説明
    Public Const COL_KNOWHOWLIST_REGDT As Integer = 1       '登録日時
    Public Const COL_KNOWHOWLIST_URL As Integer = 2         'URL（非表示）

    ''' <summary>
    ''' 画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKW0101">[IN/OUT]ナレッジURL選択画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面の初期表示設定を行う
    ''' <para>作成情報：2012/09/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKW0101 As DataHBKW0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'スプレッド用データテーブル作成処理
        If CreateDataTable(dataHBKW0101) = False Then
            Return False
        End If

        '検索結果取得処理
        If GetSearchData(dataHBKW0101) = False Then
            Return False
        End If

        'スプレッド出力データ設定処理
        If SetVwData(dataHBKW0101) = False Then
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
    ''' <param name="dataHBKW0101">[IN/OUT]ナレッジURL選択画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドに出力するためのデータテーブルの作成を行う
    ''' <para>作成情報：2012/09/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateDataTable(ByRef dataHBKW0101 As DataHBKW0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtKnowledge As New DataTable 'ナレッジURL検索結果用データテーブル

        Try

            'ナレッジURL検索結果用データテーブル作成
            With dtKnowledge
                .Columns.Add("UrlNaiyo", Type.GetType("System.String"))     '説明
                .Columns.Add("RegDT", Type.GetType("System.String"))        '登録日時
                .Columns.Add("Url", Type.GetType("System.String"))          'URL

                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'データクラスをデータテーブルにセット
            dataHBKW0101.PropDtKnowledge = dtKnowledge

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
            dtKnowledge.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 検索結果取得処理
    ''' </summary>
    ''' <param name="dataHBKW0101">[IN/OUT]ナレッジURL選択画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ナレッジURL選択画面の初期表示に必要なデータを取得する
    ''' <para>作成情報：2012/09/04 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetSearchData(ByRef dataHBKW0101 As DataHBKW0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try
            'コネクションを開く
            Cn.Open()

            'ナレッジURL情報取得（スプレッド用）
            If GetKnowhowTable(Adapter, Cn, dataHBKW0101) = False Then
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
    ''' スプレッド用ナレッジURL情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]データアダプター</param>
    ''' <param name="Cn">[IN]コネクション</param>
    ''' <param name="dataHBKW0101">[IN/OUT]ナレッジURL選択画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ナレッジURL選択画面の初期表示に必要なデータを取得する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetKnowhowTable(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef dataHBKW0101 As DataHBKW0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'SQLの作成・設定
            If sqlHBKW0101.SetSelectKnowhowSearchSql(Adapter, Cn, dataHBKW0101) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ナレッジURL情報取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKW0101.PropDtKnowledge)

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
    ''' <param name="dataHBKW0101">[IN/OUT]ナレッジURL選択画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>テーブルから取得した内容をスプレッドに設定する
    ''' <para>作成情報：2012/09/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetVwData(ByRef dataHBKW0101 As DataHBKW0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKW0101

                '検索結果
                With .PropVwKnowledgeUrlList.Sheets(0)

                    '各列のデータフィールドを設定し、データを表示
                    .Columns(COL_KNOWHOWLIST_NAIYO).DataField = "UrlNaiyo"      '説明
                    .Columns(COL_KNOWHOWLIST_RegDT).DataField = "RegDT"         '登録日時
                    .Columns(COL_KNOWHOWLIST_URL).DataField = "Url"             'URL
                    .DataSource = dataHBKW0101.PropDtKnowledge

                    '隠し列をアクティブに設定　※行の初期選択状態を解除するため
                    .ActiveColumnIndex = COL_KNOWHOWLIST_URL

                End With

                .PropLblItemCount.Text = .PropVwKnowledgeUrlList.Sheets(0).RowCount & "件"
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
    ''' ナレッジURL一覧選択時メイン処理
    ''' </summary>
    ''' <param name="dataHBKW0101">[IN/OUT]ナレッジURL選択画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ナレッジURL一覧の選択行を削除する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SelectRowMain(ByRef dataHBKW0101 As DataHBKW0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行判定処理
        If SelectRowCheck(dataHBKW0101) = False Then
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
    ''' <param name="dataHBKW0101">[IN/OUT]ナレッジURL選択画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ナレッジURL一覧の選択有無を判定する
    ''' <para>作成情報：2012/09/04 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectRowCheck(ByRef dataHBKW0101 As DataHBKW0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRow As Integer   '選択行番号
        Dim intSelectedCol As Integer   '選択列番号

        Try

            With dataHBKW0101.PropVwKnowledgeUrlList.Sheets(0)

                '選択行・列取得
                intSelectedRow = .ActiveRowIndex
                intSelectedCol = .ActiveColumnIndex

                '選択行がある場合
                If .RowCount > 0 AndAlso intSelectedCol <= COL_KNOWHOWLIST_REGDT Then

                    ' URLデコード
                    Dim StrDecode As String = WebUtility.HtmlDecode(.Cells(intSelectedRow, COL_KNOWHOWLIST_URL).Value)
                    'ブラウザ起動
                    Process.Start(StrDecode)

                Else
                    'エラーメッセージ表示
                    puErrMsg = W0101_E001
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
            puErrMsg = W0101_E002
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
