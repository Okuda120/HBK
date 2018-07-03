Imports Common
Imports CommonHBK
Imports Npgsql
Public Class LogicHBKZ0201
    '変数宣言
    Public SqlHBKZ0201 As New SqlHBKZ0201    'エンドユーザー検索SQL
    'Spread列宣言
    Public Const SEARCH_CHECK As Integer = 0            '選択
    Public Const SEARCH_ID As Integer = 1               'ID
    Public Const SEARCH_COMPANY As Integer = 2          '会社名
    Public Const SEARCH_BUSYO As Integer = 3            '部署
    Public Const SEARCH_USRNM As Integer = 4            '氏名
    Public Const SEARCH_MAILADD As Integer = 5          'メールアドレス
    Public Const SEARCH_STATE As Integer = 6            '状態内容
    Public Const SEARCH_TEL As Integer = 7              '電話番号
    Public Const SEARCH_CONNECT As Integer = 8          '連絡先
    Public Const SEARCH_JTIFLG_SORT As Integer = 9      '状態フラグ並び順
    Public Const SEARCH_ENDUSRNMKANA As Integer = 10           'エンドユーザーカナ

    '状態内容セット
    Public Const DELDATA_DISPLAY As String = "削除"

    ''' <summary>
    ''' 初期表示一覧メイン処理
    ''' </summary>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>初期表示で検索一覧を取得する
    ''' <para>作成情報：2012/05/30 abe
    ''' <p>改訂情報 : 2012/09/10 y.ikushima</p>
    ''' </para></remarks>
    Public Function LoadNoSearchMain(ByVal dataHBKZ0201 As DataHBKZ0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '列を非表示
        If ViewColumn(dataHBKZ0201) = False Then
            Return False
        End If

        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常終了
        Return True

    End Function


    ''' <summary>
    ''' 初期表示一覧メイン処理
    ''' </summary>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>初期表示で検索一覧を取得する
    ''' <para>作成情報：2012/05/30 abe
    ''' <p>改訂情報 : 2012/09/10 y.ikushima</p>
    ''' </para></remarks>
    Public Function LoadListMain(ByVal dataHBKZ0201 As DataHBKZ0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '画面コントロール処理
        If CreateDataTableForVw(dataHBKZ0201) = False Then
            Return False
        End If

        '初期表示一覧取得処理
        If LoadList(dataHBKZ0201) = False Then
            Return False
        End If

        '列を非表示
        If ViewColumn(dataHBKZ0201) = False Then
            Return False
        End If

        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常終了
        Return True

    End Function

    ''' <summary>
    ''' スプレッド用データテーブル作成処理
    ''' </summary>
    ''' <param name="dataHBKZ0201">[IN/OUT]エンドユーザ検索画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドのデータソースとして設定するデータテーブルを作成する
    ''' <para>作成情報：2012/09/10 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateDataTableForVw(ByRef dataHBKZ0201 As DataHBKZ0201) As Boolean

        '変数宣言
        Dim dtSearch As New DataTable
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'マスター検索データテーブル作成
            With dtSearch
                .Columns.Add("Check", Type.GetType("System.Boolean"))         '選択
                .Columns.Add("EndUsrID", Type.GetType("System.String"))            'ＩＤ
                .Columns.Add("EndUsrCompany", Type.GetType("System.String"))         '会社名
                .Columns.Add("EndUsrBusyoNM", Type.GetType("System.String"))         '部署名
                .Columns.Add("EndUsrNM", Type.GetType("System.String"))             'エンドユーザ氏名
                .Columns.Add("EndUsrMailAdd", Type.GetType("System.String"))        'メールアドレス
                .Columns.Add("StateNaiyo", Type.GetType("System.String"))           '状態説明
                .Columns.Add("EndUsrTel", Type.GetType("System.String"))            '電話番号
                .Columns.Add("Conect", Type.GetType("System.String"))               '連絡先
                .Columns.Add("JtiFlg_Sort", Type.GetType("System.Int32"))           '状態フラグ並び順
                .Columns.Add("EndUsrNMkana", Type.GetType("System.String"))         'エンドユーザーカナ
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            dataHBKZ0201.PropDtResultTable = dtSearch

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True


        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try


    End Function

    ''' <summary>
    ''' 初期表示一覧取得
    ''' </summary>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>初期表示で検索一覧を取得する
    ''' <para>作成情報：2012/09/10 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function LoadList(ByVal dataHBKZ0201 As DataHBKZ0201) As Boolean

        '変数を宣言
        Dim boolFlg As Boolean

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter
        Dim table As New DataTable()

        '開始ログ出力()
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'コネクションを開く
            Cn.Open()

            dataHBKZ0201.PropCount = ""


            boolFlg = SqlHBKZ0201.setEndUsr_Load(Adapter, Cn, dataHBKZ0201)

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "検索結果一覧取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(table)
            dataHBKZ0201.PropDtResultTable = table
            dataHBKZ0201.PropVwList.DataSource = dataHBKZ0201.PropDtResultTable

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True


        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            Adapter.Dispose()
            table.Dispose()
        End Try

    End Function



    ''' <summary>
    ''' 検索結果件数取得作成
    ''' </summary>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>検索ボタン押下後の一覧取得処理
    ''' <para>作成情報：2012/06/01 abe
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function LoadCount(ByVal dataHBKZ0201 As DataHBKZ0201) As Boolean

        Dim bolResult As Boolean

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter
        Dim table As New DataTable()

        '開始ログ出力()
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'コネクションを開く
            Cn.Open()

            dataHBKZ0201.PropCount = "COUNT"

            'カウントSQLの発行
            bolResult = SqlHBKZ0201.setEndUsr_Load(Adapter, Cn, dataHBKZ0201)

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "検索結果件数取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(table)

            ' 取得したデータをデータクラスへ保存
            dataHBKZ0201.PropSearchCount = DirectCast(table.Rows(0)(0), Long)

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            Adapter.Dispose()
            table.Dispose()
        End Try

    End Function
    ''' <summary>
    ''' 検索結果件数取得作成
    ''' </summary>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>検索ボタン押下後の一覧取得処理
    ''' <para>作成情報：2012/06/01 abe
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SearchCount(ByVal dataHBKZ0201 As DataHBKZ0201) As Boolean

        Dim bolResult As Boolean

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter
        Dim table As New DataTable()

        '開始ログ出力()
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'コネクションを開く
            Cn.Open()

            dataHBKZ0201.PropCount = "COUNT"

            'カウントSQLの発行
            bolResult = SqlHBKZ0201.setEndUsr_Search(Adapter, Cn, dataHBKZ0201)

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "検索結果件数取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(table)

            ' 取得したデータをデータクラスへ保存
            dataHBKZ0201.PropSearchCount = DirectCast(table.Rows(0)(0), Long)

            ''列を非表示
            'ViewColumn(dataHBKZ0201)

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            Adapter.Dispose()
            table.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 検索取得メイン処理
    ''' </summary>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>エンドユーザー検索で検索一覧を取得する
    ''' <para>作成情報：2012/05/30 abe
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SearchListMain(ByVal dataHBKZ0201 As DataHBKZ0201) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '画面コントロール処理
        If CreateDataTableForVw(dataHBKZ0201) = False Then
            Return False
        End If

        '検索データ取得
        If SearchList(dataHBKZ0201) = False Then
            Return False
        End If

        '列を非表示
        If ViewColumn(dataHBKZ0201) = False Then
            Return False
        End If

        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常終了
        Return True

    End Function

    ''' <summary>
    ''' 検索一覧画面取得
    ''' </summary>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>エンドユーザー検索で検索一覧を取得する
    ''' <para>作成情報：2012/05/30 abe
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SearchList(ByVal dataHBKZ0201 As DataHBKZ0201) As Boolean

        '変数を宣言
        Dim boolFlg As Boolean

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter
        Dim table As New DataTable()

        '開始ログ出力()
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'コネクションを開く
            Cn.Open()

            dataHBKZ0201.PropCount = ""
            boolFlg = SqlHBKZ0201.setEndUsr_Search(Adapter, Cn, dataHBKZ0201)

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "検索結果一覧取得", Nothing, Adapter.SelectCommand)

            Adapter.Fill(dataHBKZ0201.PropDtResultTable)
            dataHBKZ0201.PropVwList.DataSource = dataHBKZ0201.PropDtResultTable

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            Adapter.Dispose()
            table.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' スプレッドシート列表示処理
    ''' </summary>
    ''' <param name="dataHBKZ0201">DataHBKZ0201型オブジェクト</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>スプレッドシートに表示する列を制御する
    ''' <para>作成情報：202/06/15 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Public Function ViewColumn(ByRef dataHBKZ0201 As DataHBKZ0201) As Boolean

        Try

            If dataHBKZ0201.PropMode = SELECT_MODE_SINGLE Then
                dataHBKZ0201.PropVwList.Sheets(0).Columns(SEARCH_CHECK).Visible = False
            Else
                dataHBKZ0201.PropVwList.Sheets(0).Columns(SEARCH_CHECK).Visible = True
            End If
            dataHBKZ0201.PropVwList.Sheets(0).Columns(SEARCH_TEL).Visible = False
            dataHBKZ0201.PropVwList.Sheets(0).Columns(SEARCH_CONNECT).Visible = False

            '削除フラグ並び順非表示
            dataHBKZ0201.PropVwList.Sheets(0).Columns(SEARCH_JTIFLG_SORT).Visible = False

            'エンドユーザーカナ非表示
            dataHBKZ0201.PropVwList.Sheets(0).Columns(SEARCH_ENDUSRNMKANA).Visible = False

            '削除があるデータは背景色を灰色に
            With dataHBKZ0201.PropVwList.Sheets(0)
                For i As Integer = 0 To .Rows.Count - 1 Step 1
                    If .GetValue(i, SEARCH_STATE).ToString.Contains(DELDATA_DISPLAY) = True Then
                        '削除データ行はグレーに変更
                        .Rows(i).BackColor = Color.Silver
                    Else
                        .Rows(i).BackColor = Color.White
                    End If
                Next
            End With

            '' スプレッドの描画を停止
            'dataHBKZ0201.PropVwList.SuspendLayout()

            '' 全ての列を非表示にする
            'For Each col As FarPoint.Win.Spread.Column In dataHBKZ0201.PropVwList.ActiveSheet.Columns
            '    col.Visible = False
            'Next

            ''[mod] 2012/09/11 y.ikushima 単一、複数処理分岐のため修正 START
            'Dim intMode As Integer
            'If dataHBKZ0201.PropMode = SELECT_MODE_SINGLE Then
            '    intMode = 1
            'Else
            '    intMode = 0
            'End If
            '' 列を表示する
            'For i As Integer = intMode To 5
            '    With dataHBKZ0201.PropVwList.ActiveSheet
            '        If .Columns.Count < i Then
            '            Exit For
            '        End If

            '        .Columns(i).Visible = True
            '    End With
            'Next
            '[mod] 2012/09/11 y.ikushima 単一、複数処理分岐のため修正 END

            '' 列を表示する
            'For i As Integer = 0 To 5
            '    With dataHBKZ0201.PropVwList.ActiveSheet
            '        If .Columns.Count < i Then
            '            Exit For
            '        End If

            '        .Columns(i).Visible = True
            '    End With
            'Next

            Return True
        Catch ex As Exception
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '' スプレッドの描画を再開
            'dataHBKZ0201.PropVwList.ResumeLayout(True)
        End Try


    End Function

    ''' <summary>
    ''' スプレッドシートのすべての行を削除する
    ''' </summary>
    ''' <param name="dataHBKZ0201">DataHBKZ0201型オブジェクト</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks></remarks>
    Public Function ClearSpreadRow(ByRef dataHBKZ0201 As DataHBKZ0201) As Boolean
        ' 開始ログ出力
        Common.CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            If dataHBKZ0201.PropVwList.ActiveSheet.RowCount > 0 Then
                dataHBKZ0201.PropVwList.ActiveSheet.RemoveRows(0, dataHBKZ0201.PropVwList.ActiveSheet.RowCount)
            End If
            'dataHBKZ0201.PropCount.Text = "0件"


            ' 終了ログ出力
            Common.CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '[mod] 2012/09/11 y.ikushima 単一、複数処理分岐のため修正 START
            Return True
            '[mod] 2012/09/11 y.ikushima 単一、複数処理分岐のため修正 END
        Catch ex As Exception
            Common.CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try
    End Function
End Class
