Imports Common
Imports CommonHBK
Imports Npgsql
Imports FarPoint.Win.Spread

''' <summary>
''' グループ検索画面Logicクラス
''' </summary>
''' <remarks>グループ検索画面のロジックを定義する
''' <para>作成情報：2012/06/04 matsuoka
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class LogicHBKZ0301

    Private sqlHBKZ0301 As New SqlHBKZ0301

    Private Const CHECK_BOX As Integer = 0
    Private Const GROUP_ID As Integer = 1
    Private Const GROUP_NM As Integer = 2
    Private Const DELETE_DATA As Integer = 3

    ''' <summary>
    ''' フォーム情報の初期化
    ''' <paramref name="dataHBKZ0301">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>フォームの情報を初期化する
    ''' <para>作成情報：2012/06/15 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitForm(ByRef dataHBKZ0301 As DataHBKZ0301) As Boolean

        '開始ログ出力()
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKZ0301
                '.PropTxtSearchGroupCD.Text = System.String.Empty
                '.PropTxtSearchGroupName.Text = System.String.Empty
                .PropLblCount.Text = "0件"
                SheetAllClear(.PropVwList)
            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function
    ''' <summary>
    ''' 初期検索の件数取得
    ''' <paramref name="dataHBKZ0301">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>親画面から取得した引数をもとに行われる検索の取得件数を取得する
    ''' <para>作成情報：2012/06/12 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetGroupCountInit(ByRef dataHBKZ0301 As DataHBKZ0301) As Boolean

        '開始ログ出力()
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)    'コネクション
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ
        Dim Table As New DataTable()                'テーブル

        Try
            'コネクションを開く
            Cn.Open()

            '初期検索件数取得用SQLの作成・設定
            If sqlHBKZ0301.SetSelectCountInitGroupSearchSql(Adapter, Cn, dataHBKZ0301) = False Then
                Return False
            End If

            'SQLログ
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "グループ初期検索件数取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(Table)
            '取得データをデータクラスへ保存
            dataHBKZ0301.PropIntGroupCount = Table.Rows(0)(0)

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            Adapter.Dispose()
            Table.Dispose()
        End Try

    End Function
    ''' <summary>
    ''' 初期検索
    ''' <paramref name="dataHBKZ0301">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>親画面から取得した引数をもとに検索を行う
    ''' <para>作成情報：2012/05/31 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitSearch(ByRef dataHBKZ0301 As DataHBKZ0301) As Boolean

        '開始ログ出力()
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)    'コネクション
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ
        Dim Table As New DataTable()                'テーブル

        Try
            'コネクションを開く
            Cn.Open()

            '初期検索用SQLの作成・設定
            If sqlHBKZ0301.SetSelectInitGroupSearchSql(Adapter, Cn, dataHBKZ0301) = False Then
                Return False
            End If

            'SQLログ
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "グループ初期検索", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(Table)
            '取得データをデータクラスへ保存
            dataHBKZ0301.PropDtGroupMasta = ConvertDataTable(Table)

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            Adapter.Dispose()
            Table.Dispose()
        End Try

    End Function
    ''' <summary>
    ''' 検索の件数取得
    ''' <paramref name="dataHBKZ0301">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>フォームから取得した値をもとに行われる検索の件数
    ''' <para>作成情報：2012/06/12 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetGroupCount(ByRef dataHBKZ0301 As DataHBKZ0301) As Boolean

        '開始ログ出力()
        CommonLogic.WriteLog(Common.CommonDeclare.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)    'コネクション
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ
        Dim Table As New DataTable()                'テーブル

        Try
            'コネクションを開く
            Cn.Open()

            '検索用SQLの作成・設定
            If sqlHBKZ0301.SetSelectCountGroupSearchSql(Adapter, Cn, dataHBKZ0301) = False Then
                Return False
            End If

            'SQLログ
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "グループ検索件数取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(Table)
            '取得データをデータクラスへ保存
            dataHBKZ0301.PropIntGroupCount = Table.Rows(0)(0)

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            Adapter.Dispose()
            Table.Dispose()
        End Try

    End Function
    ''' <summary>
    ''' 検索
    ''' <paramref name=" dataHBKA0201">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>フォームから取得した値をもとに検索を行う
    ''' <para>作成情報：2012/05/31 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function Search(ByRef dataHBKZ0301 As DataHBKZ0301) As Boolean

        '開始ログ出力()
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)    'コネクション
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ
        Dim Table As New DataTable()                'テーブル

        Try
            'コネクションを開く
            Cn.Open()

            '検索用SQLの作成・設定
            If sqlHBKZ0301.SetSelectGroupSearchSql(Adapter, Cn, dataHBKZ0301) = False Then
                Return False
            End If

            'SQLログ
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "グループ検索", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(Table)
            '取得データをデータクラスへ保存
            dataHBKZ0301.PropDtGroupMasta = ConvertDataTable(Table)

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            Adapter.Dispose()
            Table.Dispose()
        End Try

    End Function
    ''' <summary>
    ''' シート情報セット
    ''' <paramref name="dataHBKZ0101">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>シートに情報をセットする
    ''' <para>作成情報：2012/06/15 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetSheet(ByRef dataHBKZ0301 As DataHBKZ0301) As Boolean

        '開始ログ出力()
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKZ0301
                'シートにデータをセット
                .PropVwList.DataSource = .PropDtGroupMasta
                '件数設定
                .PropLblCount.Text = .PropIntGroupCount.ToString & "件"
                'プロパティの設定
                SetSheetPropaty(.PropVwList, .PropMode)
            End With

            With dataHBKZ0301.PropVwList
                For i As Integer = 0 To .Rows.Count - 1 Step 1
                    If .GetValue(i, DELETE_DATA) = DELDATA_DISPLAY_NM Then
                        '削除データ行はグレーに変更
                        .Rows(i).BackColor = Color.Silver
                    Else
                        .Rows(i).BackColor = Color.White
                    End If
                Next
            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function
    ''' <summary>
    ''' データテーブルをSpreadシート用に変換
    ''' <paramref name="dataTable">[IN]変換前データテーブル</paramref>
    ''' </summary>
    ''' <remarks>ＤＢから取得したデータテーブルをSpreadシートの項目に合わせる(チェックボックスの追加)
    ''' <returns>DataTable 変換後データテーブル</returns>
    ''' <para>作成情報：2012/06/01 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ConvertDataTable(ByVal dataTable As DataTable) As DataTable

        CommonLogic.WriteLog(Common.CommonDeclare.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        Dim convTable As New DataTable

        convTable.Columns.Add("選択", Type.GetType("System.Boolean"))
        convTable.Columns.Add("グループCD", Type.GetType("System.String"))
        convTable.Columns.Add("グループ名", Type.GetType("System.String"))

        convTable.Columns.Add("削除", Type.GetType("System.String"))

        For Each setRow As DataRow In dataTable.Rows
            '値の格納
            convTable.Rows.Add(False, setRow(0), setRow(1))
        Next

        CommonLogic.WriteLog(Common.CommonDeclare.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
        Return convTable

    End Function
    ''' <summary>
    ''' Spreadシートのプロパティ設定
    ''' </summary>
    ''' <remarks>Spreadシートのプロパティを設定する。
    ''' <para>作成情報：2012/06/04 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub SetSheetPropaty(ByRef sheet As SheetView, ByVal mode As String)

        'If mode = CommonDeclareHBKZ.SELECT_MODE_MULTI Then
        '    sheet.Columns(0).Locked = False
        'Else
        '    sheet.Columns(0).Locked = True
        'End If

        ''チェックボックス
        'sheet.Columns(0).Width = 37
        ''グループCD
        'sheet.Columns(1).Width = 80
        'sheet.Columns(1).Locked = True
        ''グループ名
        'sheet.Columns(2).Width = 343
        'sheet.Columns(2).Locked = True

        If mode = CommonDeclareHBKZ.SELECT_MODE_MULTI Then
            sheet.Columns(CHECK_BOX).Locked = False
            sheet.Columns(CHECK_BOX).Visible = True
        Else
            sheet.Columns(CHECK_BOX).Locked = True
            sheet.Columns(CHECK_BOX).Visible = False
        End If

        'チェックボックス
        sheet.Columns(CHECK_BOX).Width = 37
        'グループCD
        sheet.Columns(GROUP_ID).Width = 80
        sheet.Columns(GROUP_ID).Locked = True
        'グループ名
        sheet.Columns(GROUP_NM).Width = 110
        sheet.Columns(GROUP_NM).Locked = True
        '削除
        sheet.Columns(DELETE_DATA).Width = 45
        sheet.Columns(DELETE_DATA).Locked = True
    End Sub
    ''' <summary>
    ''' Spreadシート全削除
    ''' </summary>
    ''' <remarks>Spreadシートの行をすべて削除する
    ''' <para>作成情報：2012/06/04 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub SheetAllClear(ByRef sheet As SheetView)

        If sheet.RowCount > 0 Then
            sheet.RemoveRows(0, sheet.RowCount)
        End If

    End Sub

End Class
