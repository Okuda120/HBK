Imports Common
Imports CommonHBK
Imports Npgsql

Public Class LogicHBKZ0401
    '変数宣言
    Private sqlHBKZ0401 As New SqlHBKZ0401    'プロセス検索SQL
    Private commonLogic As New CommonLogic

    ''' <summary>
    ''' スプレッドシート列表示処理
    ''' </summary>
    ''' <param name="dataHBKZ0401">DataHBKZ0401型オブジェクト</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>スプレッドシートに表示する列を制御する
    ''' <para>作成情報：202/06/15 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Public Function ViewColumn(ByRef dataHBKZ0401 As DataHBKZ0401) As Boolean

        ' 開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            ' スプレッドの描画を停止
            dataHBKZ0401.PropVwList.SuspendLayout()

            ' 全ての列を非表示にする
            For Each col As FarPoint.Win.Spread.Column In dataHBKZ0401.PropVwList.ActiveSheet.Columns
                col.Visible = False
            Next

            '' 列を表示する
            'For i As Integer = 0 To 7
            '    With dataHBKZ0401.PropVwList.ActiveSheet
            '        If .Columns.Count < i Then
            '            Exit For
            '        End If

            '        .Columns(i).Visible = True
            '    End With
            'Next
            Dim ColStart As Integer = 0
            If dataHBKZ0401.PropMode = CommonDeclareHBKZ.SELECT_MODE_SINGLE Then
                ColStart = 1
            Else
                ColStart = 0
            End If

            ' 列を表示する
            For i As Integer = ColStart To 7
                With dataHBKZ0401.PropVwList.ActiveSheet
                    If .Columns.Count < i Then
                        Exit For
                    End If

                    .Columns(i).Visible = True
                End With
            Next

            ' 開始ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True
        Catch ex As Exception
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            ' スプレッドの描画を再開
            dataHBKZ0401.PropVwList.ResumeLayout(True)
        End Try
    End Function

    ''' <summary>
    ''' フォームロード時のメイン処理
    ''' </summary>
    ''' <param name="dataHBKZ0401"></param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期化処理を行う
    ''' <para>作成情報：2012/06/13 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Public Function InitFormMain(ByRef dataHBKZ0401 As DataHBKZ0401) As Boolean
        ' 開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim Cn As New NpgsqlConnection(DbString)
        Dim Adapter As New NpgsqlDataAdapter
        Dim Table As New DataTable

        Try
            Cn.Open()

            '' 単一選択の場合
            'If dataHBKZ0401.PropMode = CommonDeclareHBKZ.SELECT_MODE_SINGLE Then
            '    dataHBKZ0401.PropVwList.Sheets(0).Columns(0).Locked = True
            '    dataHBKZ0401.PropBtnAllCheck.Enabled = False
            '    dataHBKZ0401.PropBtnAllUnCheck.Enabled = False
            'Else
            '    dataHBKZ0401.PropVwList.Sheets(0).Columns(0).Locked = False
            '    dataHBKZ0401.PropBtnAllCheck.Enabled = True
            '    dataHBKZ0401.PropBtnAllUnCheck.Enabled = True
            'End If

            ' 単一選択の場合
            If dataHBKZ0401.PropMode = CommonDeclareHBKZ.SELECT_MODE_SINGLE Then
                dataHBKZ0401.PropVwList.Sheets(0).Columns(0).Locked = True
                dataHBKZ0401.PropBtnAllCheck.Enabled = False
                dataHBKZ0401.PropBtnAllUnCheck.Enabled = False
                dataHBKZ0401.PropBtnAllCheck.Visible = False
                dataHBKZ0401.PropBtnAllUnCheck.Visible = False
            Else
                dataHBKZ0401.PropVwList.Sheets(0).Columns(0).Locked = False
                dataHBKZ0401.PropBtnAllCheck.Enabled = True
                dataHBKZ0401.PropBtnAllUnCheck.Enabled = True
                dataHBKZ0401.PropBtnAllCheck.Visible = True
                dataHBKZ0401.PropBtnAllUnCheck.Visible = True
            End If

            ' CI共通情報の取得
            If GetCIInfoData(Cn, dataHBKZ0401) = False Then
                Return False
            End If

            ' グループマスタの取得
            If GetGroupMasterData(Cn, dataHBKZ0401) = False Then
                Return False
            End If

            ' コンボボックスの初期化処理
            If InitCombo(dataHBKZ0401) = False Then
                Return False
            End If

            ' スプレッドの初期化
            With Nothing
                Dim dataTable As New DataTable
                dataTable.Columns.Add("CHK", GetType(Boolean))
                dataTable.Columns.Add("ProcessNM", GetType(String))
                dataTable.Columns.Add("KanriCD", GetType(Integer))
                dataTable.Columns.Add("StateNM", GetType(String))
                dataTable.Columns.Add("Title", GetType(String))
                dataTable.Columns.Add("Naiyo", GetType(String))
                dataTable.Columns.Add("RegDT", GetType(DateTime))
                dataTable.Columns.Add("GroupNM", GetType(String))

                dataHBKZ0401.PropVwList.DataSource = dataTable
            End With
            '[mod] 2012/08/24 y.ikushima START
            '検索条件（ログインユーザ情報の保存）
            'ログイン者所属グループ
            With dataHBKZ0401
                .PropStrLoginUserGrp = ""
                For i = 0 To PropGroupDataList.Count - 1
                    If .PropStrLoginUserGrp = "" Then
                        .PropStrLoginUserGrp = "'" & PropGroupDataList.Item(i).strGroupCd & "'"
                    Else
                        .PropStrLoginUserGrp = .PropStrLoginUserGrp & ",'" & PropGroupDataList.Item(i).strGroupCd & "'"
                    End If
                Next
                'ログイン者ID
                .PropStrLoginUserId = PropUserId
            End With
            '[mod] 2012/08/24 y.ikushima END

            ' 登録日の初期化
            dataHBKZ0401.PropDtpRegFrom.txtDate.Text = String.Empty
            dataHBKZ0401.PropDtpRegTo.txtDate.Text = String.Empty

            ' 件数の初期化
            dataHBKZ0401.PropLblCount.Text = dataHBKZ0401.PropVwList.Sheets(0).RowCount & "件"

            ' 終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True
        Catch ex As Exception
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Table.Dispose()
            Adapter.Dispose()
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' CI共通情報取得
    ''' </summary>
    ''' <param name="Cn"></param>
    ''' <param name="dataHBKZ0401"></param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了</returns>
    ''' <remarks>CI共通情報から初期表示用のデータを取得する
    ''' <para>作成情報：2012/06/15 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Private Function GetCIInfoData(ByVal Cn As NpgsqlConnection, ByRef dataHBKZ0401 As DataHBKZ0401) As Boolean
        ' 開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        ' 変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim Table As New DataTable

        Try
            ' CI共通情報テーブルデータ用SQLの作成・設定
            If sqlHBKZ0401.SetSelectSystemSql(Adapter, Cn, dataHBKZ0401) = False Then
                Return False
            End If

            ' ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "CI共通情報テーブル取得", Nothing, Adapter.SelectCommand)

            ' データ取得
            Adapter.Fill(Table)

            ' 取得データをデータクラスへ保存
            dataHBKZ0401.PropDtSystem = Table

            ' 終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True

        Catch ex As Exception
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Adapter.Dispose()
            Table.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' グループマスタデータ取得
    ''' </summary>
    ''' <param name="Cn"></param>
    ''' <param name="dataHBKZ0401"></param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了</returns>
    ''' <remarks>グループマスタから初期表示用のデータを取得する
    ''' <para>作成情報：2012/06/15 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Private Function GetGroupMasterData(ByVal Cn As NpgsqlConnection, ByRef dataHBKZ0401 As DataHBKZ0401) As Boolean
        ' 開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        ' 変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim Table As New DataTable

        Try
            ' グループマスタ用SQLの作成・設定
            If sqlHBKZ0401.SetSelectGroupSql(Adapter, Cn, dataHBKZ0401) = False Then
                Return False
            End If

            ' ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "グループマスタ取得", Nothing, Adapter.SelectCommand)

            ' データ取得
            Adapter.Fill(Table)

            ' 取得データをデータクラスへ保存
            dataHBKZ0401.PropDtChargeGrp = Table

            ' 終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True

        Catch ex As Exception
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Adapter.Dispose()
            Table.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' ステータスコンボボックス取得
    ''' </summary>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>コンボボックスの内容を取得する
    ''' <para>作成情報：2012/05/30 abe
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ComboStatusSet(ByVal dataHBKZ0401 As DataHBKZ0401)
        ' 開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数を宣言
        Dim Cn As New NpgsqlConnection(DbString)
        Dim dataTable As New DataTable
        Try
            Cn.Open()

            ' プロセスが選択されていない場合
            If dataHBKZ0401.PropCmbProcess.SelectedValue.ToString = String.Empty Then
                'Dim dataTable As New DataTable
                ' 空のデータテーブルを作成する
                dataTable.Columns.Add("ProsKbn", GetType(String))
                dataTable.Columns.Add("ProsStateNM", GetType(String))

                ' 作成したデータテーブルを返却する

                dataHBKZ0401.PropDtStatus = dataTable

                ' ステータスコンボボックスを初期化する
                If commonLogic.SetCmbBox(dataHBKZ0401.PropDtStatus, dataHBKZ0401.PropCmbStatus, True, String.Empty, String.Empty) = False Then
                    Return False
                End If

                ' 終了ログ出力
                commonLogic.WriteLog(LogLevel.None, "END", Nothing, Nothing)

                Return True
            End If

            ' ステータスマスタ取得処理
            If GetStatusMasterData(Cn, dataHBKZ0401) = False Then
                Return False
            End If

            ' ステータスコンボボックスを初期化する
            If commonLogic.SetCmbBox(dataHBKZ0401.PropDtStatus, dataHBKZ0401.PropCmbStatus, True, String.Empty, String.Empty) = False Then
                Return False
            End If

            ' 先頭を選択状態にする
            dataHBKZ0401.PropCmbStatus.SelectedIndex = 0

            ' 終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            dataTable.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' ステータスマスタ取得処理
    ''' </summary>
    ''' <param name="Cn"></param>
    ''' <param name="dataHBKZ0401"></param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了</returns>
    ''' <remarks>グループマスタから初期表示用のデータを取得する
    ''' <para>作成情報：2012/06/15 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Private Function GetStatusMasterData(ByVal Cn As NpgsqlConnection, ByRef dataHBKZ0401 As DataHBKZ0401) As Boolean
        ' 開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        ' 変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim Table As New DataTable
        Try
            ' ステータスマスタ取得用SQLの作成・設定
            If sqlHBKZ0401.setSelectStatusSql(Adapter, Cn, dataHBKZ0401) = False Then
                Return False
            End If

            ' ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "プロセスステータスマスタ取得", Nothing, Adapter.SelectCommand)

            ' データ取得
            Adapter.Fill(Table)

            ' 取得したデータをデータクラスに保存する
            dataHBKZ0401.PropDtStatus = Table

            ' 終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True
        Catch ex As Exception
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Adapter.Dispose()
            Table.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 検索一覧画面取得
    ''' </summary>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>検索条件より一覧を取得する
    ''' <para>作成情報：2012/05/30 abe
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SearchListMain(ByVal dataHBKZ0401 As DataHBKZ0401) As Boolean

        ' 開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数を宣言
        Dim Cn As New NpgsqlConnection(DbString)
        Dim Adapter As New NpgsqlDataAdapter
        Dim Table As New DataTable

        Try
            Cn.Open()

            ' プロセス検索用SQLの作成・設定を行う
            If sqlHBKZ0401.SetSelectProcessSql(Adapter, Cn, dataHBKZ0401) = False Then
                Return False
            End If

            ' ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "プロセス一覧取得", Nothing, Adapter.SelectCommand)

            ' データを取得
            Adapter.Fill(Table)

            ' 取得クラスをデータクラスへ保存
            dataHBKZ0401.PropVwList.DataSource = Table

            ' 検索件数を設定する
            dataHBKZ0401.PropLblCount.Text = Table.Rows.Count & "件"

            ' 終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
            Table.Dispose()
            Adapter.Dispose()
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 検索ボタン押下時検索件数取得処理
    ''' </summary>
    ''' <param name="dataHBKZ0401"></param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了</returns>
    ''' <remarks>検索条件に基づいてプロセス一覧を取得する
    ''' <para>作成情報：2012/06/14 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Public Function SearchCountMain(ByRef dataHBKZ0401 As DataHBKZ0401) As Boolean
        ' 開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        ' 変数宣言
        Dim Cn As New NpgsqlConnection(DbString)
        Dim Adapter As New NpgsqlDataAdapter
        Dim Table As New DataTable

        Try

            Cn.Open()

            ' プロセス件数検索用SQLの作成・設定
            If sqlHBKZ0401.SetSelectProcessCountSql(Adapter, Cn, dataHBKZ0401) = False Then
                Return False
            End If

            ' ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "プロセス一覧件数取得", Nothing, Adapter.SelectCommand)

            ' データを取得
            Adapter.Fill(Table)

            ' 取得データをデータクラスへ保存
            dataHBKZ0401.PropCount = DirectCast(Table.Rows(0)(0), Long)

            '0件の場合
            If dataHBKZ0401.PropCount = 0L Then
                If dataHBKZ0401.PropVwList.ActiveSheet.RowCount > 0 Then
                    dataHBKZ0401.PropVwList.ActiveSheet.RemoveRows(0, dataHBKZ0401.PropVwList.ActiveSheet.RowCount)
                End If
                dataHBKZ0401.PropLblCount.Text = "0件"
            End If

            ' 終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True

        Catch ex As Exception
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Table.Dispose()
            Adapter.Dispose()
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' コンボボックス初期化処理
    ''' </summary>
    ''' <param name="dataHBKZ0401"></param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了</returns>
    ''' <remarks>コンボボックスの初期化を行う
    ''' <para>作成情報：2012/06/14 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Private Function InitCombo(ByRef dataHBKZ0401 As DataHBKZ0401) As Boolean

        ' 開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        ' 変数宣言
        Dim dataTable As New DataTable

        Try

            dataTable.Columns.Add("ID", GetType(String))
            dataTable.Columns.Add("NAME", GetType(String))

            ' プロセスコンボボックスを初期化する
            Dim list As New List(Of DictionaryEntry)
            list.Add(New DictionaryEntry(String.Empty, String.Empty))
            list.Add(New DictionaryEntry(PROCESS_TYPE_INCIDENT, PROCESS_TYPE_INCIDENT_NAME))
            list.Add(New DictionaryEntry(PROCESS_TYPE_QUESTION, PROCESS_TYPE_QUESTION_NAME))
            list.Add(New DictionaryEntry(PROCESS_TYPE_CHANGE, PROCESS_TYPE_CHANGE_NAME))
            list.Add(New DictionaryEntry(PROCESS_TYPE_RELEASE, PROCESS_TYPE_RELEASE_NAME))

            dataHBKZ0401.PropCmbProcess.DataSource = list

            dataHBKZ0401.PropCmbProcess.DisplayMember = "Value"
            dataHBKZ0401.PropCmbProcess.ValueMember = "Key"

            ' ステータスコンボボックスを初期化する
            If commonLogic.SetCmbBox(dataTable, dataHBKZ0401.PropCmbStatus, True, String.Empty, String.Empty) = False Then
                Return False
            End If

            ' 対象システムコンボボックスにデータを設定する
            dataHBKZ0401.PropCmbObjSys.PropIntStartCol = 1
            If commonLogic.SetCmbBoxEx(dataHBKZ0401.PropDtSystem, dataHBKZ0401.PropCmbObjSys, "cinmb", "CINM", True, String.Empty, String.Empty) = False Then
                Return False
            End If

            'dataHBKZ0401.PropCmbObjSys.PropIntStartCol = 1

            ' 担当グループコンボボックスにデータを設定する。
            If commonLogic.SetCmbBox(dataHBKZ0401.PropDtChargeGrp, dataHBKZ0401.PropCmbChargeGrp, True, String.Empty, String.Empty) = False Then
                Return False
            End If

            ' 終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True
        Catch ex As Exception
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            dataTable.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' スプレッドシートのすべての行を削除する
    ''' </summary>
    ''' <param name="dataHBKZ0401">DataHBKZ0501型オブジェクト</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks></remarks>
    Public Function ClearSpreadRow(ByRef dataHBKZ0401 As DataHBKZ0401) As Boolean
        ' 開始ログ出力
        Common.CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            If dataHBKZ0401.PropVwList.ActiveSheet.RowCount > 0 Then
                dataHBKZ0401.PropVwList.ActiveSheet.RemoveRows(0, dataHBKZ0401.PropVwList.ActiveSheet.RowCount)
            End If
            dataHBKZ0401.PropLblCount.Text = "0件"

            ' 終了ログ出力
            Common.CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
        Catch ex As Exception
            Common.CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try
    End Function


    ''' <summary>
    ''' 検索結果表示制御メイン処理
    ''' </summary>
    ''' <param name="dataHBKZ0401">[IN/OUT]プロセス検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>呼び出し元画面のプロセスコードを選択不可にする。
    ''' <para>作成情報：2012/08/07 m.ibuki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SearchDataControlMain(ByRef dataHBKZ0401 As DataHBKZ0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '呼び出し元のプロセス番号を選択不可にする
        If SearchDataControl(dataHBKZ0401) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 検索結果表示制御処理
    ''' </summary>
    ''' <param name="dataHBKZ0401">[IN/OUT]プロセス検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>呼び出し元のプロセス番号を選択不可にする
    ''' <para>作成情報：2012/08/07 m.ibuki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SearchDataControl(ByRef dataHBKZ0401 As DataHBKZ0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strProcess As String = ""   'プロセス区分+プロセス番号

        Try
            '呼び出し元のプロセス区分、番号が空ではない場合処理を行う
            If dataHBKZ0401.PropStrFromProcessKbn <> "" And dataHBKZ0401.PropStrFromProcessNmb <> "" Then

                If dataHBKZ0401.PropStrFromProcessKbn = CommonDeclareHBK.PROCESS_TYPE_INCIDENT Then

                    strProcess = CommonDeclareHBK.PROCESS_TYPE_INCIDENT_NAME

                ElseIf dataHBKZ0401.PropStrFromProcessKbn = CommonDeclareHBK.PROCESS_TYPE_QUESTION Then
                    strProcess = CommonDeclareHBK.PROCESS_TYPE_QUESTION_NAME
                ElseIf dataHBKZ0401.PropStrFromProcessKbn = CommonDeclareHBK.PROCESS_TYPE_CHANGE Then
                    strProcess = CommonDeclareHBK.PROCESS_TYPE_CHANGE_NAME
                ElseIf dataHBKZ0401.PropStrFromProcessKbn = CommonDeclareHBK.PROCESS_TYPE_RELEASE Then
                    strProcess = CommonDeclareHBK.PROCESS_TYPE_RELEASE_NAME
                End If

                strProcess = strProcess & dataHBKZ0401.PropStrFromProcessNmb
                For i As Integer = 0 To dataHBKZ0401.PropVwList.Sheets(0).Rows.Count - 1
                    If dataHBKZ0401.PropVwList.Sheets(0).Cells(i, 1).Value & dataHBKZ0401.PropVwList.Sheets(0).Cells(i, 2).Value = strProcess Then
                        dataHBKZ0401.PropVwList.Sheets(0).Cells(i, 0).Locked = True
                        '[mod] 2012/08/24 y.ikushima START
                    Else
                        dataHBKZ0401.PropVwList.Sheets(0).Cells(i, 0).Locked = False
                        '[mod] 2012/08/24 y.ikushima END
                    End If


                Next
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
        End Try

    End Function
End Class
