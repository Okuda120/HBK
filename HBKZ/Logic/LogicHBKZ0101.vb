Imports Common
Imports CommonHBK
Imports System.Text
Imports Npgsql
Imports FarPoint.Win.Spread

''' <summary>
''' ひびきユーザー検索画面Logicクラス
''' </summary>
''' <remarks>ひびきユーザー検索画面のロジックを定義する
''' <para>作成情報：2012/06/04 matsuoka
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class LogicHBKZ0101

    Private sqlHBKZ0101 As New SqlHBKZ0101

    Private Const CHECK_BOX As Integer = 0
    Private Const USER_ID As Integer = 1
    Private Const GROUP_NM As Integer = 2
    Private Const USER_NM As Integer = 3
    Private Const GROUP_ID As Integer = 4
    Private Const SORT As Integer = 5
    Private Const DELETE_DATA As Integer = 6

    ''' <summary>
    ''' フォーム情報の初期化
    ''' <paramref name="dataHBKZ0101">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>フォームの情報を初期化する
    ''' <para>作成情報：2012/06/15 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitForm(ByRef dataHBKZ0101 As DataHBKZ0101) As Boolean

        '開始ログ出力()
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKZ0101
                '.PropTxtSearchUserID.Text = System.String.Empty
                '.PropTxtSearchUserName.Text = System.String.Empty
                '.PropTxtSearchGroupCD.Text = System.String.Empty
                '.PropTxtSearchGroupName.Text = System.String.Empty
                .PropLblCount.Text = "0件"
                SheetAllClear(.PropVwList)

                '【ADD】2012/08/03 r.hoshino　インシデント登録用：START
                '初期表示に受け渡ししたデータを設定する
                If .PropInitMode = 1 Then
                    dataHBKZ0101.PropDtHbkUsrMasta = dataHBKZ0101.PropDataTable
                End If
                '【ADD】2012/08/03 r.hoshino　インシデント登録用：END

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
    ''' <paramref name="dataHBKZ0101">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>親画面から取得した引数をもとに行われる検索の取得件数を取得する
    ''' <para>作成情報：2012/06/12 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetHbkUsrCountInit(ByRef dataHBKZ0101 As DataHBKZ0101) As Boolean

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
            If sqlHBKZ0101.SetSelectCountInitHBKUserSearchSql(Adapter, Cn, dataHBKZ0101) = False Then
                Return False
            End If

            'SQLログ
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ひびきユーザー初期検索件数取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(Table)
            '取得データをデータクラスへ保存
            dataHBKZ0101.PropIntGroupCount = Table.Rows(0)(0)

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
    ''' <paramref name="dataHBKZ0101">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>親画面から取得した引数をもとに検索を行う
    ''' <para>作成情報：2012/06/4 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitSearch(ByRef dataHBKZ0101 As DataHBKZ0101) As Boolean

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
            If sqlHBKZ0101.SetSelectInitHBKUserSearchSql(Adapter, Cn, dataHBKZ0101) = False Then
                Return False
            End If

            'SQLログ
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ひびきユーザー初期検索", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(Table)
            '取得データをデータクラスへ保存
            dataHBKZ0101.PropDtHbkUsrMasta = ConvertDataTable(Table)

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
    ''' <paramref name="dataHBKZ0101">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>フォームから取得した値をもとに検索を行う
    ''' <para>作成情報：2012/06/12 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetHbkUserCount(ByRef dataHBKZ0101 As DataHBKZ0101) As Boolean

        '開始ログ出力()
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)    'コネクション
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ
        Dim Table As New DataTable()                'テーブル

        Try
            'コネクションを開く
            Cn.Open()

            '検索SQLの作成・設定
            If sqlHBKZ0101.SetSelectCountHBKUserSearchSql(Adapter, Cn, dataHBKZ0101) = False Then
                Return False
            End If

            'SQLログ
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ひびきユーザー検索件数取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(Table)
            '取得データをデータクラスへ保存
            dataHBKZ0101.PropIntGroupCount = Table.Rows(0)(0)

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
    ''' <paramref name="dataHBKZ0101">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>フォームから取得した値をもとに検索を行う
    ''' <para>作成情報：2012/06/04 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function Search(ByRef dataHBKZ0101 As DataHBKZ0101) As Boolean

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
            If sqlHBKZ0101.SetSelectHBKUserSearchSql(Adapter, Cn, dataHBKZ0101) = False Then
                Return False
            End If


            'SQLログ
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ひびきユーザー検索", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(Table)

            '【ADD】2012/08/03 r.hoshino　インシデント登録用：START
            '取得データをデータクラスへ保存
            If dataHBKZ0101.PropInitMode = 1 Then
                'データテーブル作成処理変更
                dataHBKZ0101.PropDtHbkUsrMasta = ConvertDataTable(Table, 1)
            Else
                '【ADD】2012/08/03 r.hoshino　インシデント登録用：END
                dataHBKZ0101.PropDtHbkUsrMasta = ConvertDataTable(Table)
            End If


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
    ''' 初期検索_作業履歴担当者用
    ''' <paramref name="dataHBKZ0101">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>親画面から取得した引数をもとに検索を行う
    ''' <para>作成情報：2012/09/04 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitSearch_initMode1(ByRef dataHBKZ0101 As DataHBKZ0101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)    'コネクション
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ
        Dim Table As New DataTable()                'テーブル

        Try
            'コネクションを開く
            Cn.Open()

            '初期検索用SQLの作成・設定
            If sqlHBKZ0101.SetSelectInitHBKUserSearch_initMode1Sql(Adapter, Cn, dataHBKZ0101) = False Then
                Return False
            End If

            'SQLログ
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ひびきユーザー初期検索_InitMode1", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(Table)

            'データテーブル作成処理変更
            dataHBKZ0101.PropDtHbkUsrMasta = ConvertDataTable(Table, 1)


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
    ''' <p>改訂情報 :2012/09/04 r.hoshino 作業履歴用の処理を追加 </p>
    ''' </para></remarks>
    Public Function SetSheet(ByRef dataHBKZ0101 As DataHBKZ0101) As Boolean

        '開始ログ出力()
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKZ0101
                '【ADD】2012/08/03 r.hoshino　インシデント登録用：START
                If .PropInitMode = 1 Then

                    'データがあればCOPY
                    Dim workdt As New DataTable
                    If .PropDtHbkUsrMasta IsNot Nothing Then
                        workdt = .PropDtHbkUsrMasta.Copy

                        '検索結果に受け渡しデータをマージする
                        .PropDtHbkUsrMasta.Merge(.PropDataTable)

                        'ソートして作業テーブルに格納
                        Dim Rows As Object = .PropDtHbkUsrMasta.Select(String.Empty, "順番 Asc")
                        Dim DtSortResult As DataTable = .PropDtHbkUsrMasta.Clone()
                        For Each row As DataRow In Rows

                            '【ADD】2012/09/04 r.hoshino　インシデント登録用：START
                            'キーが一致し、削除データであれば削除設定をする
                            For r As Integer = 0 To workdt.Rows.Count - 1
                                If workdt.Rows(r).Item("ユーザーID").ToString.Equals(row.Item("ユーザーID").ToString) AndAlso _
                                    workdt.Rows(r).Item("グループ名").ToString.Equals(row.Item("グループ名").ToString) AndAlso _
                                    workdt.Rows(r).Item("ユーザー氏名").ToString.Equals(row.Item("ユーザー氏名").ToString) AndAlso _
                                    workdt.Rows(r).Item("グループID").ToString.Equals(row.Item("グループID").ToString) AndAlso _
                                    workdt.Rows(r).Item("削除").ToString.Equals("○") Then
                                    row.Item("削除") = "○"
                                    Exit For
                                End If
                            Next
                            '【ADD】2012/09/04 r.hoshino　インシデント登録用：END

                            DtSortResult.ImportRow(row)
                        Next

                        .PropDtHbkUsrMasta = DtSortResult
                    End If
                    workdt.Dispose()

                End If
                '【ADD】2012/08/03 r.hoshino　インシデント登録用：END

                'シートにデータをセット
                .PropVwList.DataSource = .PropDtHbkUsrMasta
                '件数設定
                .PropLblCount.Text = .PropIntGroupCount.ToString & "件"
                'プロパティの設定
                SetSheetPropaty(.PropVwList, .PropMode)
            End With

            With dataHBKZ0101.PropVwList
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
    ''' <paramref name="initMode">[IN]設定モード(0:デフォルト,1:インシデント登録用)</paramref>
    ''' </summary>
    ''' <remarks>ＤＢから取得したデータテーブルをSpreadシートの項目に合わせる(チェックボックスの追加)
    ''' <returns>DataTable 変換後データテーブル</returns>
    ''' <para>作成情報：2012/06/04 matsuoka
    ''' <p>改訂情報 :2012/08/03 r.hoshino 引数initModeを追加 </p>
    ''' </para></remarks>
    Private Function ConvertDataTable(ByVal dataTable As DataTable, Optional ByVal initMode As Integer = 0) As DataTable
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        Dim convTable As New DataTable

        '【ADD】2012/08/03 r.hoshino　インシデント登録用：START
        If initMode = 1 Then
            Dim keys(3) As DataColumn
            Dim column As DataColumn

            column = New DataColumn()
            column.DataType = System.Type.GetType("System.Boolean")
            column.ColumnName = "選択"
            convTable.Columns.Add(column)

            column = New DataColumn()
            column.DataType = System.Type.GetType("System.String")
            column.ColumnName = "ユーザーID"
            convTable.Columns.Add(column)
            keys(0) = column

            column = New DataColumn()
            column.DataType = System.Type.GetType("System.String")
            column.ColumnName = "グループ名"
            convTable.Columns.Add(column)
            keys(1) = column

            column = New DataColumn()
            column.DataType = System.Type.GetType("System.String")
            column.ColumnName = "ユーザー氏名"
            convTable.Columns.Add(column)
            keys(2) = column

            column = New DataColumn()
            column.DataType = System.Type.GetType("System.String")
            column.ColumnName = "グループID"
            convTable.Columns.Add(column)
            keys(3) = column

            column = New DataColumn()
            column.DataType = System.Type.GetType("System.Decimal")
            column.ColumnName = "順番"
            convTable.Columns.Add(column)

            column = New DataColumn()
            column.DataType = System.Type.GetType("System.String")
            column.ColumnName = "削除"
            convTable.Columns.Add(column)

            convTable.PrimaryKey = keys

            For Each setRow As DataRow In dataTable.Rows
                '値の格納
                'convTable.Rows.Add(False, setRow(0), setRow(1), setRow(2), setRow(3), 99)
                convTable.Rows.Add(False, setRow(0), setRow(1), setRow(2), setRow(3), 99, setRow(4))
            Next

        Else
            '【ADD】2012/08/03 r.hoshino　インシデント登録用：END
            convTable.Columns.Add("選択", Type.GetType("System.Boolean"))
            convTable.Columns.Add("ユーザーID", Type.GetType("System.String"))
            convTable.Columns.Add("グループ名", Type.GetType("System.String"))
            convTable.Columns.Add("ユーザー氏名", Type.GetType("System.String"))
            convTable.Columns.Add("グループID", Type.GetType("System.String"))
            convTable.Columns.Add("順番", Type.GetType("System.Decimal"))
            convTable.Columns.Add("削除", Type.GetType("System.String"))

            For Each setRow As DataRow In dataTable.Rows
                '値の格納
                'convTable.Rows.Add(False, setRow(0), setRow(1), setRow(2), setRow(3))
                convTable.Rows.Add(False, setRow(0), setRow(1), setRow(2), setRow(3), 0, setRow(4))
            Next

        End If


        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
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
        'sheet.Columns(0).Width = 39
        ''ユーザーID
        'sheet.Columns(1).Locked = True
        'sheet.Columns(1).Width = 68
        ''グループ名
        'sheet.Columns(2).Locked = True
        'sheet.Columns(2).Width = 134
        ''ユーザー氏名
        'sheet.Columns(3).Locked = True
        'sheet.Columns(3).Width = 217
        'sheet.Columns(4).Visible = True
        'sheet.Columns(5).Visible = True

        'チェックボックス
        If mode = CommonDeclareHBKZ.SELECT_MODE_MULTI Then
            sheet.Columns(CHECK_BOX).Locked = False
            sheet.Columns(CHECK_BOX).Visible = True
        Else
            sheet.Columns(CHECK_BOX).Locked = True
            sheet.Columns(CHECK_BOX).Visible = False
        End If
        sheet.Columns(CHECK_BOX).Width = 39

        'ユーザーID
        sheet.Columns(USER_ID).Locked = True
        sheet.Columns(USER_ID).Width = 68
        'グループ名
        sheet.Columns(GROUP_NM).Locked = True
        sheet.Columns(GROUP_NM).Width = 134
        'ユーザー氏名
        sheet.Columns(USER_NM).Locked = True
        sheet.Columns(USER_NM).Width = 217
        '削除
        sheet.Columns(DELETE_DATA).Locked = True
        sheet.Columns(DELETE_DATA).Width = 45

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
