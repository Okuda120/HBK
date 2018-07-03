Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.Drawing
Imports FarPoint.Win.Spread

''' <summary>
''' 問題検索一覧画面ロジッククラス
''' </summary>
''' <remarks>問題検索一覧画面のロジックを定義したクラス
''' <para>作成情報：2012/07/31 y.ikushima
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKD0101

    'インスタンス作成
    Private sqlHBKD0101 As New SqlHBKD0101
    Private commonLogic As New CommonLogic

    'Public定数宣言
    'Spreadの行をセット
    Public Const COL_PRBNMB As Integer = 0                          '問題番号
    Public Const COL_PROCESSSTATENM As Integer = 1                  'プロセスステータス名
    Public Const COL_STARTDT As Integer = 2                         '開始日時
    Public Const COL_TITLE As Integer = 3                           'タイトル
    Public Const COL_TARGET_SYS As Integer = 4                      '対象システム
    Public Const COL_GROUPNM As Integer = 5                         '担当者業務グループ
    Public Const COL_HBKUSRNM As Integer = 6                        '問題担当者
    Public Const COL_WORKSCEDT As Integer = 7                       '作業予定日時
    Public Const COL_REGDT As Integer = 8                           '登録日時
    Public Const COL_PROCESSSTATECD As Integer = 9                  'プロセスステータスCD
    Public Const COL_HBKUSRCD As Integer = 10                       '問題担当者CD
    Public Const COL_GROUPCD As Integer = 11                        '担当者業務グループCD

    '各項目リストボックス
    Private Const LIST_COLMUN_ZERO As Integer = 0               'リストボックスの0列目

    '作業予定日比較用定数
    Private Const WORKSCEDT_PAST As Integer = -1                '作業予定日：過去
    Private Const WORKSCEDT_TODAY As Integer = 0                '作業予定日：今日
    Private Const WORKSCEDT_FUTURE As Integer = 1               '作業予定日：未来

    ''' <summary>
    ''' 画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKD0101">[IN/OUT]問題検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面の初期表示設定を行う
    ''' <para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKD0101 As DataHBKD0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '画面初期表示時処理
        If InitialControl(dataHBKD0101) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True
    End Function

    ''' <summary>
    ''' 画面初期表示時処理
    ''' </summary>
    ''' <param name="dataHBKD0101">[IN/OUT]問題検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面の初期表示コントロール設定を行う
    ''' <para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitialControl(ByRef dataHBKD0101 As DataHBKD0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'コントロール用データ取得処理
            If GetDataForControl(dataHBKD0101) = False Then
                Return False
            End If

            'コントロールデータ設定処理
            If SetFormControlData(dataHBKD0101) = False Then
                Return False
            End If

            'コントロール初期設定処理
            If SetFormControlInitial(dataHBKD0101) = False Then
                Return False
            End If

            'スプレッド用データテーブル作成処理
            If CreateDataTable(dataHBKD0101) = False Then
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
        End Try
    End Function

    ''' <summary>
    ''' コントロール表示用データ取得処理
    ''' </summary>
    ''' <param name="dataHBKD0101">[IN/OUT]問題検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面の初期表示コントロール用データ取得を行う
    ''' <para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetDataForControl(ByRef dataHBKD0101 As DataHBKD0101) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)            'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter                'アダプタ
        Dim dtProcessState As New DataTable
        Dim dtSystemList As New DataTable
        Dim dtSystemCombo As New DataTable
        Dim dtGrpCD As New DataTable
        Dim dtPrbCase As New DataTable

        'DataTable初期化
        With dataHBKD0101
            .PropDtProcessState = New DataTable
            .PropDtSystemList = New DataTable
            .PropDtSystemCombo = New DataTable
            .PropDtGrpCD = New DataTable
            .PropDtPrbCase = New DataTable
        End With

        Try
            'コネクションを開く
            Cn.Open()

            'ステータスリストボックス用データ取得（SELECT）用SQLを作成
            If sqlHBKD0101.SetSelectProcessStateSql(dataHBKD0101, Adapter, Cn) = False Then
                Return False
            End If
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ステータスリストボックス用データ取得", Nothing, Adapter.SelectCommand)
            'SQL実行
            Adapter.Fill(dtProcessState)


            '対象システムリストボックス用データ取得（SELECT）用SQLを作成
            If sqlHBKD0101.SetSelectSystemSql(dataHBKD0101, Adapter, Cn) = False Then
                Return False
            End If
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対象システムリストボックス用データ取得", Nothing, Adapter.SelectCommand)
            'SQL実行
            Adapter.Fill(dtSystemList)

            '対象システムコンボボックス用データ取得（SELECT）用SQLを作成
            If sqlHBKD0101.SetSelectSystemSql(dataHBKD0101, Adapter, Cn) = False Then
                Return False
            End If
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対象システムコンボボックス用データ取得", Nothing, Adapter.SelectCommand)
            'SQL実行
            Adapter.Fill(dtSystemCombo)


            '担当者グループコンボボックス用データ取得（SELECT）用SQLを作成
            If sqlHBKD0101.SetSelectGrpCDSql(dataHBKD0101, Adapter, Cn) = False Then
                Return False
            End If
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "担当者グループコンボボックス用データ取得", Nothing, Adapter.SelectCommand)
            'SQL実行
            Adapter.Fill(dtGrpCD)


            '発生原因コンボボックス用データ取得（SELECT）用SQLを作成
            If sqlHBKD0101.SetSelectPrbCaseSql(dataHBKD0101, Adapter, Cn) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "発生原因コンボボックス用データ取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtPrbCase)

            'Dataクラスに保存
            With dataHBKD0101
                .PropDtProcessState = dtProcessState
                .PropDtSystemList = dtSystemList
                .PropDtSystemCombo = dtSystemCombo
                .PropDtGrpCD = dtGrpCD
                .PropDtPrbCase = dtPrbCase
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
        Finally
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            'オブジェクト解放
            Adapter.Dispose()
            Cn.Dispose()
            'リソースの解放
            dtProcessState.Dispose()
            dtSystemList.Dispose()
            dtSystemCombo.Dispose()
            dtGrpCD.Dispose()
            dtPrbCase.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' コントロールデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKD0101">[IN/OUT]問題検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面のコントロールにデータの設定を行う
    ''' <para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetFormControlData(ByRef dataHBKD0101 As DataHBKD0101) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'リストボックス作成処理
            If CreateListBox(dataHBKD0101) = False Then
                Return False
            End If

            'コンボボックス作成処理
            If CreateComboBox(dataHBKD0101) = False Then
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
        End Try
    End Function

    ''' <summary>
    ''' コンボボックス作成処理
    ''' </summary>
    ''' <param name="dataHBKD0101">[IN/OUT]問題検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面のコンボボックスにデータの設定を行い、作成する
    ''' <para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateComboBox(ByRef dataHBKD0101 As DataHBKD0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKD0101

                'ステータスリストボックス作成
                .PropLstProcessState.ValueMember = "ProcessStateCD"
                .PropLstProcessState.DisplayMember = "ProcessStateNM"
                .PropLstProcessState.DataSource = .PropDtProcessState

                '対象システムリストボックス作成
                .PropLstTargetSys.ValueMember = "CINmb"
                .PropLstTargetSys.DisplayMember = "ClassNM"
                .PropLstTargetSys.DataSource = .PropDtSystemList

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
    ''' リストボックス作成処理
    ''' </summary>
    ''' <param name="dataHBKD0101">[IN/OUT]問題検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面のリストボックスにデータの設定を行い、作成する
    ''' <para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateListBox(ByRef dataHBKD0101 As DataHBKD0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKD0101

                'フリーフラグ1コンボボックス作成
                If commonLogic.SetCmbBox(FreeFlg, .PropCmbFreeFlg1) = False Then
                    Return False
                End If

                'フリーフラグ2コンボボックス作成
                If commonLogic.SetCmbBox(FreeFlg, .PropCmbFreeFlg2) = False Then
                    Return False
                End If

                'フリーフラグ3コンボボックス作成
                If commonLogic.SetCmbBox(FreeFlg, .PropCmbFreeFlg3) = False Then
                    Return False
                End If

                'フリーフラグ4コンボボックス作成
                If commonLogic.SetCmbBox(FreeFlg, .PropCmbFreeFlg4) = False Then
                    Return False
                End If

                'フリーフラグ5コンボボックス作成
                If commonLogic.SetCmbBox(FreeFlg, .PropCmbFreeFlg5) = False Then
                    Return False
                End If

                '担当者グループコンボボックス作成
                If commonLogic.SetCmbBox(.PropDtGrpCD, .PropCmbTantoGrpCD, True, "", "") = False Then
                    Return False
                End If

                '発生原因コンボボックス作成
                If commonLogic.SetCmbBox(.PropDtPrbCase, .PropCmbPrbCase, True, "", "") = False Then
                    Return False
                End If

                'プロセスリンク種別作成
                If commonLogic.SetCmbBox(ProcessType, .PropCmbKindCD) = False Then
                    Return False
                End If

                '対象システムコンボボックス作成
                .PropCmbSystemNmb.PropIntStartCol = 2
                commonLogic.SetCmbBoxEx(.PropDtSystemCombo, .PropCmbSystemNmb, "CINmb", "CINM", True, 0, "")

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
    ''' スプレッド用データテーブル作成処理
    ''' </summary>
    ''' <param name="dataHBKD0101">[IN/OUT]問題検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドに出力するためのデータテーブルの作成を行う
    ''' <para>作成情報：2012/08/01 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateDataTable(ByRef dataHBKD0101 As DataHBKD0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtPrbInfo As New DataTable             '問題検索結果用データテーブル

        Try

            With dtPrbInfo

                .Columns.Add("PrbNmb", Type.GetType("System.Int32"))                    '問題番号
                .Columns.Add("ProcessStateNM", Type.GetType("System.String"))           'プロセスステータス
                .Columns.Add("KaisiDT", Type.GetType("System.String"))                  '開始日時
                .Columns.Add("Title", Type.GetType("System.String"))                    'タイトル
                .Columns.Add("CINM", Type.GetType("System.String"))                     '対象システム
                .Columns.Add("TantoGrpNM", Type.GetType("System.String"))               '担当者業務グループ
                .Columns.Add("PrbTantoNM", Type.GetType("System.String"))               '問題担当者
                .Columns.Add("WorkSceDT", Type.GetType("System.String"))                '作業予定日時
                .Columns.Add("RegDT", Type.GetType("System.String"))                    '登録日時
                .Columns.Add("ProcessStateCD", Type.GetType("System.String"))           'プロセスステータスCD
                .Columns.Add("PrbTantoCD", Type.GetType("System.String"))               '問題担当者CD
                .Columns.Add("TantoGrpCD", Type.GetType("System.String"))               '担当者業務グループCD

                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'データクラスをデータテーブルにセット
            dataHBKD0101.PropDtSearchResult = dtPrbInfo

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
    ''' コントロール初期設定処理
    ''' </summary>
    ''' <param name="dataHBKD0101">[IN/OUT]問題検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面のコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetFormControlInitial(ByRef dataHBKD0101 As DataHBKD0101) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0101
                '画面の検索条件コントロールの初期化を行う

                '問題基本情報---------------------------------------------------------------------------------------------------
                .PropTxtPrbNmb.Text = ""                                '問題番号テキストボックス（空文字）

                'ステータスリストボックス「完了」「未解決完了」以外を選択
                For i As Integer = 0 To .PropLstProcessState.Items.Count - 1
                    'プロセスステータスCD=208(完了)、209(未解決完了)以外のものを選択状態にする。
                    'If .PropLstProcessState.Items(i)("ProcessStateCD") = PROCESS_STATUS_QUESTION_KANRYOH Or _
                    '    .PropLstProcessState.Items(i)("ProcessStateCD") = PROCESS_STATUS_QUESTION_MIKAIKETSUKANRYOH Then
                    'デフォルト選択フラグが"0"以外を選択状態にする。
                    If .PropLstProcessState.Items(i)("Defaultselectflg") = DEFAULTSELECT_FLG_OFF Then
                        '項目を未選択状態に設定
                        .PropLstProcessState.SetSelected(i, False)
                    Else
                        '項目を選択状態に設定
                        .PropLstProcessState.SetSelected(i, True)
                    End If
                Next
                .PropLstProcessState.TopIndex = 0                       'ステータスのスクロールをリセット
                .PropLstTargetSys.ClearSelected()                       '対象システムリストボックス（未選択）
                .PropLstTargetSys.TopIndex = 0                          '対象システムのスクロールをリセット
                .PropTxtTitle.Text = ""                                 'タイトルテキストボックス（空文字）
                .PropTxtNaiyo.Text = ""                                 '内容テキストボックス（空文字）
                .PropTxtTaisyo.Text = ""                                '対処テキストボックス（空文字）

                .PropTxtBiko.Text = ""                                  'フリーテキストテキストボックス（空文字）

                .PropDtpStartDTFrom.txtDate.Text = ""                   '開始日（From)DateTimePickerEx（空文字）
                .PropDtpStartDTTo.txtDate.Text = ""                     '開始日（To)DateTimePickerEx（空文字）
                .PropDtpKanryoDTFrom.txtDate.Text = ""                  '完了日（From)DateTimePickerEx（空文字）
                .PropDtpKanryoDTTo.txtDate.Text = ""                    '完了日（To)DateTimePickerEx（空文字）
                .PropDtpRegDTFrom.txtDate.Text = ""                     '登録日（From)DateTimePickerEx（空文字）
                .PropDtpRegDTTo.txtDate.Text = ""                       '登録日（To)DateTimePickerEx（空文字）
                .PropDtpLastRegDTFrom.txtDate.Text = ""                 '最終更新日時（From)DateTimePickerEx（空文字）
                .PropTxtLastRegTimeFrom.PropTxtTime.Text = ""           '最終更新日時時分（From)テキストボックス（空文字）
                .PropDtpLastRegDTTo.txtDate.Text = ""                   '最終更新日時（To)DateTimePickerEx（空文字）
                .PropTxtLastRegTimeTo.PropTxtTime.Text = ""             '最終更新日時時分（To)テキストボックス（空文字）
                .PropCmbPrbCase.SelectedValue = ""                      '発生原因コンボボックス（未選択）
                .PropTxtCysprNmb.Text = ""                              'CYSPRテキストボックス（空文字）
                .PropCmbFreeFlg1.SelectedValue = ""                     'フリーフラグコンボボックス１（未選択）
                .PropCmbFreeFlg2.SelectedValue = ""                     'フリーフラグコンボボックス２（未選択）
                .PropCmbFreeFlg3.SelectedValue = ""                     'フリーフラグコンボボックス３（未選択）
                .PropCmbFreeFlg4.SelectedValue = ""                     'フリーフラグコンボボックス４（未選択）
                .PropCmbFreeFlg5.SelectedValue = ""                     'フリーフラグコンボボックス５（未選択）

                '担当者情報------------------------------------------------------------------------------------------------------------
                .PropRdoDirect.Checked = True                           '直接ラジオボタン（選択）
                .PropRdoPartic.Checked = False                          '関与ラジオボタン（未選択）
                .PropCmbTantoGrpCD.SelectedValue = PropWorkGroupCD      '担当者グループコンボボックス（未選択）
                .PropTxtTantoID.Text = ""                               '担当者IDテキストボックス（空文字）
                .PropTxtTantoNM.Text = ""                               '担当者氏名テキストボックス（空文字）
                '作業情報------------------------------------------------------------------------------------------------------------------
                .PropDtpWorkSceDTFrom.txtDate.Text = ""                 '作業予定日時（From）DateTimePickerEx（空文字）
                .PropTxtWorkScetimeFrom.PropTxtTime.Text = ""           '作業予定日時時分（From）テキストボックス（空文字）
                .PropDtpWorkSceDTTo.txtDate.Text = ""                   '作業予定日時（To）DateTimePickerEx（空文字）
                .PropTxtWorkScetimeTo.PropTxtTime.Text = ""             '作業予定日時時分（To）テキストボックス（空文字）
                .PropCmbSystemNmb.PropCmbColumns.SelectedValue = 0                     '対象システムコンボボックス（未選択）

                'プロセスリンク情報----------------------------------------------------------------------------------------------------------
                .PropCmbKindCD.SelectedValue = ""                       '種別コンボボックス（未選択）
                .PropTxtNum.Text = ""                                   '番号テキストボックス（空文字）

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
    ''' 表示データ検索処理メイン処理
    ''' </summary>
    ''' <param name="dataHBKD0101">[IN/OUT]問題検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>Spreadに表示するデータの取得処理を行う
    ''' <para>作成情報：2012/08/01 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SearchMain(ByRef dataHBKD0101 As DataHBKD0101) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '画面入力チェック処理
        If CheckInputControl(dataHBKD0101) = False Then
            Return False
        End If

        '[mod] 2012/09/06 y.ikushima Excel出力対応 START
        ''画面コントロール設定処理
        'If SetPicControl(dataHBKD0101) = False Then
        '    Return False
        'End If
        '[mod] 2012/09/06 y.ikushima Excel出力対応 END

        '検索条件保存処理
        If SetSreachConditionSave(dataHBKD0101) = False Then
            Return False
        End If

        '表示データ検索処理
        If SearchInfoForSpread(dataHBKD0101) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True
    End Function

    ''' <summary>
    ''' コントロール入力チェック処理処理
    ''' </summary>
    ''' <param name="dataHBKD0101">[IN/OUT]問題検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面コントロールに対する入力チェック処理を行う
    ''' <para>作成情報：2012/08/14 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckInputControl(ByRef dataHBKD0101 As DataHBKD0101) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKD0101
                '最終更新時刻(FROM)に入力があり、日付が未入力
                If .PropDtpLastRegDTFrom.txtDate.Text.Trim = "" AndAlso .PropTxtLastRegTimeFrom.PropTxtTime.Text <> "" Then
                    'エラーメッセージ設定
                    puErrMsg = String.Format(D0101_E002, "最終更新日時(FROM)")
                    'フォーカス設定
                    .PropDtpLastRegDTFrom.Focus()
                    'エラーを返す
                    Return False
                End If

                '最終更新時刻(TO)に入力があり、日付が未入力
                If .PropDtpLastRegDTTo.txtDate.Text.Trim = "" AndAlso .PropTxtLastRegTimeTo.PropTxtTime.Text <> "" Then
                    'エラーメッセージ設定
                    puErrMsg = String.Format(D0101_E002, "最終更新日時(TO)")
                    'フォーカス設定
                    .PropDtpLastRegDTTo.Focus()
                    'エラーを返す
                    Return False
                End If

                '作業予定日時刻(FROM)に入力があり、日付が未入力
                If .PropDtpWorkSceDTFrom.txtDate.Text.Trim = "" AndAlso .PropTxtWorkScetimeFrom.PropTxtTime.Text <> "" Then
                    'エラーメッセージ設定
                    puErrMsg = String.Format(D0101_E002, "作業予定日(FROM)")
                    'フォーカス設定
                    .PropDtpWorkSceDTFrom.Focus()
                    'エラーを返す
                    Return False
                End If

                '作業予定日時刻(TO)に入力があり、日付が未入力
                If .PropDtpWorkSceDTTo.txtDate.Text.Trim = "" AndAlso .PropTxtWorkScetimeTo.PropTxtTime.Text <> "" Then
                    'エラーメッセージ設定
                    puErrMsg = String.Format(D0101_E002, "作業予定日(TO)")
                    'フォーカス設定
                    .PropDtpWorkSceDTTo.Focus()
                    'エラーを返す
                    Return False
                End If

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
    ''' 画面コントロール設定処理
    ''' </summary>
    ''' <param name="dataHBKD0101">[IN/OUT]問題検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面のコントロールを設定する
    ''' <para>作成情報：2012/08/10 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetPicControl(ByRef dataHBKD0101 As DataHBKD0101) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'Excel出力ボタン活性
            dataHBKD0101.PropBtnOutput.Enabled = True

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
    ''' 表示データ検索処理
    ''' </summary>
    ''' <param name="dataHBKD0101">[IN/OUT]問題検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>Spreadに表示するデータの取得処理、設定を行う
    ''' <para>作成情報：2012/08/01 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SearchInfoForSpread(ByRef dataHBKD0101 As DataHBKD0101) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnCheckFlg As Boolean = True

        Try

            '表示データ取得処理
            If GetPrbDataFroSpread(dataHBKD0101, blnCheckFlg) = False Then
                Return False
            End If

            If blnCheckFlg = True Then
                'Excel出力ボタン活性
                dataHBKD0101.PropBtnOutput.Enabled = True
                '検索結果の表示処理
                If SetResultPrb(dataHBKD0101) = False Then
                    Return False
                End If
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

    ''' <summary>
    ''' 表示データ取得処理
    ''' </summary>
    ''' <param name="dataHBKD0101">[IN/OUT]問題検索一覧画面Dataクラス</param>
    ''' <param name="blnCheckFlg">[IN/OUT]メッセージチェックフラグ</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>Spreadに表示するデータの取得処理を行う
    ''' <para>作成情報：2012/08/01 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetPrbDataFroSpread(ByRef dataHBKD0101 As DataHBKD0101, ByRef blnCheckFlg As Boolean) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)            'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter                'アダプタ
        Dim dtResultInfo As New DataTable
        Dim dtResultCount As New DataTable
        Try

            'コネクションを開く
            Cn.Open()

            'データ件数取得
            If sqlHBKD0101.SetSelectPrbCountSql(dataHBKD0101, Adapter, Cn) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "問題一覧件数取得", Nothing, Adapter.SelectCommand)
            'SQL実行
            Adapter.Fill(dtResultCount)

            '件数チェック
            If dtResultCount.Rows(0).Item(0) = 0 Then

                '件数が0件の場合エラーメッセージに空白をセット
                puErrMsg = ""

                'データソースを空に設定
                If CreateDataTable(dataHBKD0101) = False Then
                    Return False
                End If
                'Spread描写
                If SetVwData(dataHBKD0101) = False Then
                    Return False
                End If
                '件数の表示
                dataHBKD0101.PropLblKensu.Text = "0件"
                '[mod] 2012/09/06 y.ikushima Excel出力対応 START
                dataHBKD0101.PropBtnOutput.Enabled = False
                '[mod] 2012/09/06 y.ikushima Excel出力対応 END
                Return False
            End If

            '件数判定(判定を行い表示しない場合処理を抜ける)
            If dtResultCount.Rows(0).Item(0) > PropSearchMsgCount Then
                If MsgBox(String.Format(D0101_W001, PropSearchMsgCount), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
                    'コネクションを閉じる
                    Cn.Close()
                    '[mod] 2012/09/06 y.ikushima Excel出力対応 START
                    'データソースを空に設定
                    If CreateDataTable(dataHBKD0101) = False Then
                        Return False
                    End If
                    'Spread描写
                    If SetVwData(dataHBKD0101) = False Then
                        Return False
                    End If
                    '件数の表示
                    dataHBKD0101.PropLblKensu.Text = "0件"
                    dataHBKD0101.PropBtnOutput.Enabled = False
                    blnCheckFlg = False
                    '[mod] 2012/09/06 y.ikushima Excel出力対応 END
                    '終了ログ出力
                    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                    '正常終了
                    Return True
                End If
            End If

            '表示データ取得処理
            If sqlHBKD0101.SetSelectPrbInfoSql(dataHBKD0101, Adapter, Cn) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "問題一覧データ取得", Nothing, Adapter.SelectCommand)
            'SQL実行
            Adapter.Fill(dtResultInfo)

            'Dataクラスに格納
            dataHBKD0101.PropDtSearchResultCount = dtResultCount
            dataHBKD0101.PropDtSearchResult = dtResultInfo

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
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            'オブジェクト解放
            Adapter.Dispose()
            Cn.Dispose()
            'リソースの解放
            dtResultCount.Dispose()
            dtResultInfo.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 検索結果の表示処理
    ''' </summary>
    ''' <param name="dataHBKD0101">[IN/OUT]問題検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果の表示設定を行う
    ''' <para>作成情報：2012/08/01 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetResultPrb(ByRef dataHBKD0101 As DataHBKD0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'スプレッド出力データ設定処理
            If SetVwData(dataHBKD0101) = False Then
                Return False
            End If

            '件数判定
            With dataHBKD0101

                '件数の表示
                .PropLblKensu.Text = String.Format("{0}件", .PropDtSearchResultCount.Rows(0).Item(0))

                ''件数チェック
                'If .PropDtSearchResultCount.Rows(0).Item(0) = 0 Then
                '    puErrMsg = D0101_E001
                '    Return False
                'End If
            End With

            '検索結果の背景色設定
            If SetBGColor(dataHBKD0101) = False Then
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
        End Try

    End Function

    ''' <summary>
    '''スプレッドの出力データ設定処理
    ''' </summary>
    ''' <param name="dataHBKD0101">[IN/OUT]問題検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>テーブルから取得した内容をスプレッドに設定する
    ''' <para>作成情報：2012/08/01 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetVwData(ByRef dataHBKD0101 As DataHBKD0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '検索結果
            With dataHBKD0101.PropVwProblemSearch.Sheets(0)

                .DataSource = dataHBKD0101.PropDtSearchResult
                .Columns(COL_PRBNMB).DataField = "PrbNmb"                       '問題番号
                .Columns(COL_PROCESSSTATENM).DataField = "ProcessStateNM"       'プロセスステータス名
                .Columns(COL_STARTDT).DataField = "KaisiDT"                     '開始日時
                .Columns(COL_TITLE).DataField = "Title"                         'タイトル
                .Columns(COL_TARGET_SYS).DataField = "CINM"                     '対象システム
                .Columns(COL_GROUPNM).DataField = "TantoGrpNM"                  '担当者業務グループ
                .Columns(COL_HBKUSRNM).DataField = "PrbTantoNM"                 '問題担当者
                .Columns(COL_WORKSCEDT).DataField = "WorkSceDT"                 '作業予定日時
                .Columns(COL_REGDT).DataField = "RegDT"                         '登録日時
                .Columns(COL_PROCESSSTATECD).DataField = "ProcessStateCD"       'プロセスステータスCD
                .Columns(COL_HBKUSRCD).DataField = "PrbTantoID"                 '問題担当者CD
                .Columns(COL_GROUPCD).DataField = "TantoGrpCD"                  '担当者業務グループCD

                '非表示処理
                .Columns(COL_PROCESSSTATECD).Visible = False
                .Columns(COL_HBKUSRCD).Visible = False
                .Columns(COL_GROUPCD).Visible = False
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
    ''' スプレッドのセルの背景色設定処理
    ''' </summary>
    ''' <param name="dataHBKD0101">[IN/OUT]問題検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドのセルの値を判定して背景色を変更する
    ''' <para>作成情報：2012/08/02 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetBGColor(ByRef dataHBKD0101 As DataHBKD0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '検索結果
            With dataHBKD0101.PropVwProblemSearch.Sheets(0)

                '表示件数分ループ
                For i = 0 To .RowCount - 1

                    'ステータス
                    If .Cells(i, COL_PROCESSSTATECD).Value = PROCESS_STATUS_QUESTION_MIKAIKETSUKANRYOH Then
                        '未解決完了（灰色）
                        .Cells(i, COL_PROCESSSTATENM).BackColor = Color.Silver
                    ElseIf .Cells(i, COL_PROCESSSTATECD).Value = PROCESS_STATUS_QUESTION_CHOSACHU Or _
                        .Cells(i, COL_PROCESSSTATECD).Value = PROCESS_STATUS_QUESTION_HOUSHINKENTOHCHU Or _
                        .Cells(i, COL_PROCESSSTATECD).Value = PROCESS_STATUS_QUESTION_TAIOHCHU Then
                        '調査中、方針検討中、対応中（黄色）
                        .Cells(i, COL_PROCESSSTATENM).BackColor = Color.Yellow
                    ElseIf .Cells(i, COL_PROCESSSTATECD).Value = PROCESS_STATUS_QUESTION_GAIBUCHOUSAIRAICHU Or _
                        .Cells(i, COL_PROCESSSTATECD).Value = PROCESS_STATUS_QUESTION_GAIBUTAIOHIRAICHU Then
                        '外部調査依頼中、外部対応依頼中（黄緑）
                        .Cells(i, COL_PROCESSSTATENM).BackColor = Color.GreenYellow
                    ElseIf .Cells(i, COL_PROCESSSTATECD).Value = PROCESS_STATUS_QUESTION_KANRYOH Then
                        '完了（ライトブルー）
                        .Cells(i, COL_PROCESSSTATENM).BackColor = Color.Cyan
                    Else
                        .Cells(i, COL_PROCESSSTATENM).BackColor = Color.White
                    End If

                    '作業者業務グループ
                    If .Cells(i, COL_HBKUSRCD).Value = PropUserId Then
                        '背景色の設定（黄色）
                        .Cells(i, COL_GROUPNM).BackColor = Color.Yellow
                    ElseIf .Cells(i, COL_GROUPCD).Value = PropWorkGroupCD Then
                        '背景色の設定（オレンジ）
                        .Cells(i, COL_GROUPNM).BackColor = Color.Orange
                    Else
                        ''背景色の設定（黄緑）
                        .Cells(i, COL_GROUPNM).BackColor = Color.LawnGreen
                    End If

                    '作業予定日時
                    If .Cells(i, COL_WORKSCEDT).Value <> Nothing Then

                        Select Case DateTime.Compare(FormatDateTime(.Cells(i, COL_WORKSCEDT).Value, DateFormat.ShortDate), Now().Date)
                            Case WORKSCEDT_PAST
                                '背景色の設定（ピンク）
                                .Cells(i, COL_WORKSCEDT).BackColor = Color.Pink
                            Case WORKSCEDT_TODAY
                                '背景色の設定（黄色）
                                .Cells(i, COL_WORKSCEDT).BackColor = Color.Yellow
                            Case WORKSCEDT_FUTURE
                                '背景色の設定（オレンジ）
                                .Cells(i, COL_WORKSCEDT).BackColor = Color.Orange
                        End Select
                    Else
                        .Cells(i, COL_WORKSCEDT).BackColor = Color.White
                    End If
                Next

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
    ''' 画面コントロールクリアメイン処理
    ''' </summary>
    ''' <param name="dataHBKD0101">[IN/OUT]問題検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面コントロールクリア処理を行う
    ''' <para>作成情報：2012/08/01 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ClearFormMain(ByRef dataHBKD0101 As DataHBKD0101) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'コントロール初期設定処理
        If SetFormControlInitial(dataHBKD0101) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True
    End Function

    ''' <summary>
    ''' 画面コントロール検索条件保存処理
    ''' </summary>
    ''' <param name="dataHBKD0101">[IN/OUT]問題検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索ボタン押下時、画面コントロールの検索条件を保存する
    ''' <para>作成情報：2012/08/10 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetSreachConditionSave(ByRef dataHBKD0101 As DataHBKD0101) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        Dim blnTryNum As Boolean
        Dim strCyspr() As String = Nothing    'CYSPR検索用配列
        Try
            With dataHBKD0101

                '検索ボタン押下時、画面の検索条件コントロールの値の保存を行う

                'ログイン情報--------------------------------------------------------------------------------------------------
                'ログイン者所属グループ
                .PropStrLoginUserGrp = ""
                For i = 0 To .PropGrpLoginUser.cmbGroup.Items.Count - 1
                    If .PropStrLoginUserGrp = "" Then
                        .PropStrLoginUserGrp = "'" & .PropGrpLoginUser.cmbGroup.Items(i)(LIST_COLMUN_ZERO) & "'"
                    Else
                        .PropStrLoginUserGrp = .PropStrLoginUserGrp & ",'" & .PropGrpLoginUser.cmbGroup.Items(i)(LIST_COLMUN_ZERO) & "'"
                    End If
                Next
                'ログイン者ID
                .PropStrLoginUserId = PropUserId

                '問題基本情報---------------------------------------------------------------------------------------------------
                '問題番号
                .PropStrPrbNmb = ""
                If .PropTxtPrbNmb.Text <> "" Then
                    If Integer.TryParse(.PropTxtPrbNmb.Text, blnTryNum) = False Then
                        '数値変換に失敗した場合は空白設定
                        .PropStrPrbNmb = "0"
                    Else
                        .PropStrPrbNmb = .PropTxtPrbNmb.Text
                    End If
                End If

                'ステータス
                .PropStrProcessState = ""
                For i As Integer = 0 To .PropLstProcessState.SelectedItems.Count - 1
                    If .PropStrProcessState = "" Then
                        .PropStrProcessState = "'" & .PropLstProcessState.SelectedItems(i)("ProcessStateCD") & "'"
                    Else
                        .PropStrProcessState = .PropStrProcessState & ",'" & .PropLstProcessState.SelectedItems(i)("ProcessStateCD") & "'"
                    End If
                Next

                '対象システム
                .PropStrTargetSys = ""
                For i As Integer = 0 To .PropLstTargetSys.SelectedItems.Count - 1
                    If .PropStrTargetSys = "" Then
                        .PropStrTargetSys = "'" & .PropLstTargetSys.SelectedItems(i)("CINmb") & "'"
                    Else
                        .PropStrTargetSys = .PropStrTargetSys & ",'" & .PropLstTargetSys.SelectedItems(i)("CINmb") & "'"
                    End If
                Next

                .PropStrTitle = .PropTxtTitle.Text                                              'タイトル
                .PropStrNaiyo = .PropTxtNaiyo.Text                                              '内容
                .PropStrTaisyo = .PropTxtTaisyo.Text                                            '対処
                .PropStrBiko = .PropTxtBiko.Text                                                'フリーテキスト

                .PropStrStartDTFrom = .PropDtpStartDTFrom.txtDate.Text                          '開始日（From)
                .PropStrStartDTTo = .PropDtpStartDTTo.txtDate.Text                              '開始日（To)
                .PropStrKanryoDTFrom = .PropDtpKanryoDTFrom.txtDate.Text                        '完了日（From)
                .PropStrKanryoDTTo = .PropDtpKanryoDTTo.txtDate.Text                            '完了日（To)
                .PropStrRegDTFrom = .PropDtpRegDTFrom.txtDate.Text                              '登録日（From)
                .PropStrRegDTTo = .PropDtpRegDTTo.txtDate.Text                                  '登録日（To)
                '最終更新日時（From){YYYY/MM/DD HH24:MI}
                .PropStrLastRegDTFrom = .PropDtpLastRegDTFrom.txtDate.Text & " " & .PropTxtLastRegTimeFrom.PropTxtTime.Text
                '最終更新日時（To){YYYY/MM/DD HH24:MI}
                '[Mod]2015/01/23 e.okamura 問題要望114 Start
                '.PropStrLastRegDTTo = .PropDtpLastRegDTTo.txtDate.Text & "" & .PropTxtLastRegTimeTo.PropTxtTime.Text
                .PropStrLastRegDTTo = .PropDtpLastRegDTTo.txtDate.Text & " " & .PropTxtLastRegTimeTo.PropTxtTime.Text
                '[Mod]2015/01/23 e.okamura 問題要望114 End
                '[Add]2014/11/19 e.okamura 問題要望114 Start
                '最終更新日時（時刻From)
                .PropStrLastRegTimeFrom = .PropTxtLastRegTimeFrom.PropTxtTime.Text
                '最終更新日時（時刻To)
                .PropStrLastRegTimeTo = .PropTxtLastRegTimeTo.PropTxtTime.Text
                '[Add]2014/11/19 e.okamura 問題要望114 End
                .PropStrPrbCase = .PropCmbPrbCase.SelectedValue                                 '発生原因コンボボックス
                .PropStrCysprNmb = .PropTxtCysprNmb.Text                                        'CYSPRテキストボックス
                .PropStrFreeFlg1 = .PropCmbFreeFlg1.SelectedValue                               'フリーフラグコンボボックス１
                .PropStrFreeFlg2 = .PropCmbFreeFlg2.SelectedValue                               'フリーフラグコンボボックス２
                .PropStrFreeFlg3 = .PropCmbFreeFlg3.SelectedValue                               'フリーフラグコンボボックス３
                .PropStrFreeFlg4 = .PropCmbFreeFlg4.SelectedValue                               'フリーフラグコンボボックス４
                .PropStrFreeFlg5 = .PropCmbFreeFlg5.SelectedValue                               'フリーフラグコンボボックス５

                '担当者情報------------------------------------------------------------------------------------------------------------
                'チェックボックスによってフラグを立てる
                If .PropRdoDirect.Checked = True Then
                    '直接ラジオボタン選択時
                    .PropStrTantoRdoCheck = D0101_RDO_CHOKUSETSU
                ElseIf .PropRdoPartic.Checked = True Then
                    '関与ラジオボタン選択時
                    .PropStrTantoRdoCheck = D0101_RDO_KANYO
                End If
                .PropStrTantoGrpCD = .PropCmbTantoGrpCD.SelectedValue                           '担当者グループコンボボックス
                .PropStrTantoID = .PropTxtTantoID.Text                                          '担当者IDテキストボックス
                .PropStrTantoNM = .PropTxtTantoNM.Text                                          '担当者氏名テキストボックス

                '作業情報------------------------------------------------------------------------------------------------------------------
                '作業予定日時(From){YYYY/MM/DD HH24:MI}
                .PropStrWorkSceDTFrom = .PropDtpWorkSceDTFrom.txtDate.Text & " " & .PropTxtWorkScetimeFrom.PropTxtTime.Text
                '作業予定日時(To){YYYY/MM/DD HH24:MI}
                .PropStrWorkSceDTTo = .PropDtpWorkSceDTTo.txtDate.Text & " " & .PropTxtWorkScetimeTo.PropTxtTime.Text
                '[Add]2014/11/19 e.okamura 問題要望114 Start
                '作業予定日時（時刻From)
                .PropStrWorkSceTimeFrom = .PropTxtWorkScetimeFrom.PropTxtTime.Text
                '作業予定日時（時刻To)
                .PropStrWorkSceTimeTo = .PropTxtWorkScetimeTo.PropTxtTime.Text
                '[Add]2014/11/19 e.okamura 問題要望114 End
                .PropStrSystemNmb = .PropCmbSystemNmb.PropCmbColumns.SelectedValue.ToString     '対象システムコンボボックス

                'プロセスリンク情報----------------------------------------------------------------------------------------------------------
                .PropStrKindCD = .PropCmbKindCD.SelectedValue                                   '種別コンボボックス
                .PropStrNum = .PropTxtNum.Text                                                  '番号テキストボックス

                'プロセスリンク情報取得
                .PropStrProcessLinkNumAry = ""
                If .PropStrKindCD <> "" Or .PropStrNum <> "" Then
                    If GetProccesLink(.PropStrKindCD, .PropStrNum, .PropStrProcessLinkNumAry) = False Then
                        Return False
                    End If
                End If
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
    ''' プロセスリンク情報取得
    ''' </summary>
    ''' <param name="StrProccesLinkKind">[IN]プロセス区分種別</param>
    ''' <param name="StrProcessLinkNum">[IN]プロセス区分番号</param>
    ''' <param name="StrProcessLinkNumAry">[IN/OUT]プロセス区分番号（カンマ区切り文字列）</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンク情報を取得する
    ''' <para>作成情報：2012/08/10 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetProccesLink(ByVal StrProccesLinkKind As String, ByVal StrProcessLinkNum As String, ByRef StrProcessLinkNumAry As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        '変数宣言
        Dim dtResult As New DataTable '検索件数

        Try

            'コネクションを開く
            Cn.Open()

            'SQLの作成・設定
            If sqlHBKD0101.SetProccesLinkSql(Adapter, Cn, StrProccesLinkKind, StrProcessLinkNum) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "プロセスリンク取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtResult)

            If dtResult.Rows.Count <> 0 Then
                StrProcessLinkNumAry = dtResult.Rows(0).Item(0).ToString
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
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            'リソースの解放
            Adapter.Dispose()
            Cn.Dispose()
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' デフォルトソート処理
    ''' </summary>
    ''' <param name="dataHBKD0101">[IN/OUT]問題検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>デフォルトソートメイン処理
    ''' <para>作成情報：2012/08/10 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SortDefaultMain(ByRef dataHBKD0101 As DataHBKD0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ソート設定
        If SortSearchData(dataHBKD0101) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常終了
        Return True

    End Function

    ''' <summary>
    ''' デフォルトソート処理
    ''' </summary>
    ''' <param name="dataHBKD0101">[IN/OUT]問題検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果を検索時のソート順に並べ替える
    ''' <para>作成情報：2012/08/10 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function SortSearchData(ByRef dataHBKD0101 As DataHBKD0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKD0101.PropVwProblemSearch.Sheets(0)
                Dim Si(0) As SortInfo 'ソート対象配列

                '問題番号の降順に変更する
                'ソート対象列をソートする順番で指定
                Si(0) = New SortInfo(COL_PRBNMB, True) '問題番号

                '作業予定日 + インシデント番号の昇順でソートを行う
                .SortRows(0, .RowCount, Si)

                'ソートインジケーターの初期化
                For i = 0 To .Columns.Count - 1
                    .Columns(i).ResetSortIndicator()
                Next

            End With

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
    ''' 担当マスタデータ取得メイン処理
    ''' </summary>
    ''' <param name="dataHBKD0101">[IN/OUT]問題検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/08/14 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetIncTantoDataMain(ByRef dataHBKD0101 As DataHBKD0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try

            'コネクションを開く
            Cn.Open()

            'マスタデータ取得
            If GetIncTantoData(Adapter, Cn, dataHBKD0101) = False Then
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
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Adapter.Dispose()
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 担当マスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKD0101">[IN/OUT]問題検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>マスタデータを取得する
    ''' <para>作成情報：2012/08/14 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetIncTantoData(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKD0101 As DataHBKD0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKD0101.GetIncTantoInfoData(Adapter, Cn, dataHBKD0101) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "担当マスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            '取得データをデータクラスにセット
            dataHBKD0101.PropDtResultSub = dtmst

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
            dtmst.Dispose()
        End Try

    End Function

End Class
