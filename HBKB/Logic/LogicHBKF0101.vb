Imports Common
Imports CommonHBK
Imports Npgsql
Imports FarPoint.Win.Spread

''' <summary>
''' リリース検索一覧画面ロジッククラス
''' </summary>
''' <remarks>リリース検索一覧画面のロジックを定義したクラス
''' <para>作成情報：2012/08/20 y.ikushima
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKF0101

    'インスタンス作成
    Private sqlHBKF0101 As New SqlHBKF0101
    Private commonLogic As New CommonLogic

    'Public定数宣言
    'Spreadの行をセット
    Public Const COL_RELMB As Integer = 0                           'リリース番号
    Public Const COL_RELUKENMB As Integer = 1                       'リリース受付番号
    Public Const COL_PROCESSSTATENM As Integer = 2                  'ステータス
    Public Const COL_IRAIDT As Integer = 3                          '依頼日
    Public Const COL_TITLE As Integer = 4                           'タイトル
    Public Const COL_TUJYOKINKYU As Integer = 5                     '通常・受付
    Public Const COL_USRSYUTI As Integer = 6                        'ユーザ周知
    Public Const COL_RELSCEDT As Integer = 7                        'リリース予定日
    Public Const COL_RELSTDT As Integer = 8                         'リリース着手日
    Public Const COL_RELEDDT As Integer = 9                         'リリース完了日
    Public Const COL_GROUPNM As Integer = 10                        '担当者業務グループ
    Public Const COL_HBKUSR As Integer = 11                         'リリース担当者
    Public Const COL_PROCESSSTATECD As Integer = 12                 'プロセスステータスCD
    Public Const COL_GROUPCD As Integer = 13                        '担当者業務グループCD
    Public Const COL_HBKUSRCD As Integer = 14                       'リリース担当者CD
    Public Const COL_SORT As Integer = 15                           'ソート

    '各項目リストボックス
    Private Const LIST_COLMUN_ZERO As Integer = 0               'リストボックスの0列目

    ''' <summary>
    ''' 画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKF0101">[IN/OUT]リリース検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面の初期表示設定を行う
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKF0101 As DataHBKF0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '画面初期表示時処理
        If InitialControl(DataHBKF0101) = False Then
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
    ''' <param name="dataHBKF0101">[IN/OUT]リリース検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面の初期表示コントロール設定を行う
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitialControl(ByRef dataHBKF0101 As DataHBKF0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'コントロール用データ取得処理
            If GetDataForControl(DataHBKF0101) = False Then
                Return False
            End If

            'コントロールデータ設定処理
            If SetFormControlData(DataHBKF0101) = False Then
                Return False
            End If

            'コントロール初期設定処理
            If SetFormControlInitial(DataHBKF0101) = False Then
                Return False
            End If

            'スプレッド用データテーブル作成処理
            If CreateDataTable(DataHBKF0101) = False Then
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
    ''' <param name="dataHBKF0101">[IN/OUT]リリース検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面の初期表示コントロール用データ取得を行う
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetDataForControl(ByRef dataHBKF0101 As DataHBKF0101) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)            'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter                'アダプタ
        Dim dtProcessState As New DataTable
        Dim dtGrpCD As New DataTable

        'DataTable初期化
        With DataHBKF0101
            .PropDtProcessState = New DataTable
            .PropDtGrpCD = New DataTable
        End With

        Try
            'コネクションを開く
            Cn.Open()

            'ステータスリストボックス用データ取得（SELECT）用SQLを作成
            If sqlHBKF0101.SetSelectProcessStateSql(dataHBKF0101, Adapter, Cn) = False Then
                Return False
            End If
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ステータスリストボックス用データ取得", Nothing, Adapter.SelectCommand)
            'SQL実行
            Adapter.Fill(dtProcessState)

            '担当者グループコンボボックス用データ取得（SELECT）用SQLを作成
            If sqlHBKF0101.SetSelectGrpCDSql(dataHBKF0101, Adapter, Cn) = False Then
                Return False
            End If
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "担当者グループコンボボックス用データ取得", Nothing, Adapter.SelectCommand)
            'SQL実行
            Adapter.Fill(dtGrpCD)

            'Dataクラスに保存
            With dataHBKF0101
                .PropDtProcessState = dtProcessState
                .PropDtGrpCD = dtGrpCD
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
            dtGrpCD.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' コントロールデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKF0101">[IN/OUT]リリース検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面のコントロールにデータの設定を行う
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetFormControlData(ByRef dataHBKF0101 As DataHBKF0101) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'リストボックス作成処理
            If CreateListBox(dataHBKF0101) = False Then
                Return False
            End If

            'コンボボックス作成処理
            If CreateComboBox(dataHBKF0101) = False Then
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
    ''' <param name="dataHBKF0101">[IN/OUT]リリース検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面のコンボボックスにデータの設定を行い、作成する
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateComboBox(ByRef dataHBKF0101 As DataHBKF0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKF0101

                'ステータスリストボックス作成
                .PropLstProcessState.ValueMember = "ProcessStateCD"
                .PropLstProcessState.DisplayMember = "ProcessStateNM"
                .PropLstProcessState.DataSource = .PropDtProcessState

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
    ''' <param name="dataHBKF0101">[IN/OUT]リリース検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面のリストボックスにデータの設定を行い、作成する
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateListBox(ByRef dataHBKF0101 As DataHBKF0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKF0101

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

                'ユーザー周知必要有無区分コンボボックス作成
                If commonLogic.SetCmbBox(UsrSyutiKbn, .PropCmbUsrSyutiKbn) = False Then
                    Return False
                End If

                'プロセスリンク種別コンボボックス作成
                If commonLogic.SetCmbBox(ProcessType, .PropCmbKindCD) = False Then
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
    ''' スプレッド用データテーブル作成処理
    ''' </summary>
    ''' <param name="dataHBKF0101">[IN/OUT]リリース検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドに出力するためのデータテーブルの作成を行う
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateDataTable(ByRef dataHBKF0101 As DataHBKF0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtRelInfo As New DataTable             'リリース検索結果用データテーブル

        Try

            With dtRelInfo

                .Columns.Add("RelNmb", Type.GetType("System.Int32"))                        'リリース番号
                .Columns.Add("RelUkeNmb", Type.GetType("System.String"))                    'リリース受付番号
                .Columns.Add("ProcessStateNM", Type.GetType("System.String"))               'プロセスステータス
                .Columns.Add("IraiDT", Type.GetType("System.String"))                       '依頼日
                .Columns.Add("TujyoKinkyuKbn", Type.GetType("System.String"))               '通常・緊急区分
                .Columns.Add("UsrSyutiKbn", Type.GetType("System.String"))                  'ユーザー周知必要有無区分
                .Columns.Add("RelSceDT", Type.GetType("System.String"))                     'リリース予定日時（目安）
                .Columns.Add("RelStDT", Type.GetType("System.String"))                      'リリース着手日時
                .Columns.Add("RelEdDT", Type.GetType("System.String"))                      'リリース終了日時
                .Columns.Add("TantoGrpNM", Type.GetType("System.String"))                   '担当グループ名
                .Columns.Add("RelTantoNM", Type.GetType("System.String"))                   'リリース担当者名
                .Columns.Add("ProcessStateCD", Type.GetType("System.String"))               'プロセスステータスCD
                .Columns.Add("TantoGrpCD", Type.GetType("System.String"))                   '担当グループCD
                .Columns.Add("RelTantoID", Type.GetType("System.String"))                   'リリース担当者ID
                .Columns.Add("Sort", Type.GetType("System.Int32"))                          'ソート順

                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'データクラスをデータテーブルにセット
            dataHBKF0101.PropDtSearchResult = dtRelInfo

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
    ''' <param name="dataHBKF0101">[IN/OUT]リリース検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面のコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetFormControlInitial(ByRef dataHBKF0101 As DataHBKF0101) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKF0101
                '画面の検索条件コントロールの初期化を行う

                'リリース基本情報----------------------------------------------------------------------
                .PropTxtRelNmb.Text = ""                                    'リリース番号テキストボックス
                .PropTxtRelUkeNmb.Text = ""                                 'リリース受付番号テキストボックス
                'プロセスステータスリストボックス
                For i As Integer = 0 To .PropLstProcessState.Items.Count - 1
                    ''プロセスステータスCD「407：完了」、「408：未実施完了」以外のものを選択状態にする。
                    'If .PropLstProcessState.Items(i)("ProcessStateCD") = PROCESS_STATUS_RELEASE_KANRYO Or _
                    '   .PropLstProcessState.Items(i)("ProcessStateCD") = PROCESS_STATUS_RELEASE_MIJISSHIKANRYO Then
                    'デフォルト選択フラグが"0"以外を選択状態にする。
                    If .PropLstProcessState.Items(i)("Defaultselectflg") = DEFAULTSELECT_FLG_OFF Then
                        '項目を未選択状態に設定
                        .PropLstProcessState.SetSelected(i, False)
                    Else
                        '項目を選択状態に設定
                        .PropLstProcessState.SetSelected(i, True)
                    End If
                Next
                .PropLstProcessState.TopIndex = 0
                .PropTxtTitle.Text = ""                                     'タイトルテキストボックス
                .PropTxtGaiyo.Text = ""                                     '概要テキストボックス
                .PropCmbUsrSyutiKbn.SelectedValue = ""                      'ユーザ周知必要有無コンボボックス
                .PropDtpIraiDTFrom.txtDate.Text = ""                        '依頼日(FROM)DateTimePickerEx
                .PropDtpIraiDTTo.txtDate.Text = ""                          '依頼日(TO)DateTimePickerEx
                .PropDtpRelSceDTFrom.txtDate.Text = ""                      'リリース予定日(FROM)DateTimePickerEx
                .PropDtpRelSceDTto.txtDate.Text = ""                        'リリース予定日(TO)DateTimePickerEx
                .PropDtpRelStDTFrom.txtDate.Text = ""                       'リリース着手日(FROM)DateTimePickerEx
                .PropDtpRelStDTTo.txtDate.Text = ""                         'リリース着手日(TO)DateTimePickerEx

                .PropTxtBiko.Text = ""                                      'フリーテキストボックス
                .PropCmbFreeFlg1.SelectedValue = ""                         'フリーフラグ１
                .PropCmbFreeFlg2.SelectedValue = ""                         'フリーフラグ２
                .PropCmbFreeFlg3.SelectedValue = ""                         'フリーフラグ３
                .PropCmbFreeFlg4.SelectedValue = ""                         'フリーフラグ４
                .PropCmbFreeFlg5.SelectedValue = ""                         'フリーフラグ５

                '担当者情報-------------------------------------------------------------------------------
                .PropCmbTantoGrpCD.SelectedValue = PropWorkGroupCD          '担当者グループコンボボックス
                .PropTxtTantoID.Text = ""                                   '担当者IDテキストボックス
                .PropTxtTantoNM.Text = ""                                   '担当者氏名テキストボックス

                'プロセスリンク情報-----------------------------------------------------------------------
                .PropCmbKindCD.SelectedValue = ""                           '種別コンボボックス
                .PropTxtNum.Text = ""                                       '番号テキストボックス

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
    ''' <param name="dataHBKF0101">[IN/OUT]リリース検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>Spreadに表示するデータの取得処理を行う
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SearchMain(ByRef dataHBKF0101 As DataHBKF0101) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '[mod] 2012/09/06 y.ikushima Excel出力対応 START
        ''画面コントロール設定処理
        'If SetPicControl(dataHBKF0101) = False Then
        '    Return False
        'End If
        '[mod] 2012/09/06 y.ikushima Excel出力対応 END

        '検索条件保存処理
        If SetSreachConditionSave(dataHBKF0101) = False Then
            Return False
        End If

        '表示データ検索処理
        If SearchInfoForSpread(dataHBKF0101) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True
    End Function


    ''' <summary>
    ''' 画面コントロール設定処理
    ''' </summary>
    ''' <param name="dataHBKF0101">[IN/OUT]リリース検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面のコントロールを設定する
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetPicControl(ByRef dataHBKF0101 As DataHBKF0101) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'Excel出力ボタン活性
            dataHBKF0101.PropBtnOutput.Enabled = True

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
    ''' <param name="dataHBKF0101">[IN/OUT]リリース検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>Spreadに表示するデータの取得処理、設定を行う
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SearchInfoForSpread(ByRef dataHBKF0101 As DataHBKF0101) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        '変数宣言
        Dim blnCheckFlg As Boolean = True

        Try
            '表示データ取得処理
            If GetRelDataFroSpread(dataHBKF0101, blnCheckFlg) = False Then
                Return False
            End If

            If blnCheckFlg = True Then
                'Excel出力ボタン活性
                dataHBKF0101.PropBtnOutput.Enabled = True
                '検索結果の表示処理
                If SetResultRel(dataHBKF0101) = False Then
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
    ''' <param name="dataHBKF0101">[IN/OUT]リリース検索一覧画面Dataクラス</param>
    ''' <param name="blnCheckFlg">[IN/OUT]メッセージチェックフラグ</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>Spreadに表示するデータの取得処理を行う
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetRelDataFroSpread(ByRef dataHBKF0101 As DataHBKF0101, ByRef blnCheckFlg As Boolean) As Boolean
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
            If sqlHBKF0101.SetSelectRelCountSql(dataHBKF0101, Adapter, Cn) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "リリース一覧件数取得", Nothing, Adapter.SelectCommand)
            'SQL実行
            Adapter.Fill(dtResultCount)

            '件数チェック
            If dtResultCount.Rows(0).Item(0) = 0 Then
                'データソースを空に設定
                If CreateDataTable(dataHBKF0101) = False Then
                    Return False
                End If
                'Spread描写
                If SetVwData(dataHBKF0101) = False Then
                    Return False
                End If
                '[mod] 2012/09/06 y.ikushima Excel出力対応 START
                dataHBKF0101.PropBtnOutput.Enabled = False
                '[mod] 2012/09/06 y.ikushima Excel出力対応 END
                '件数の表示
                dataHBKF0101.PropLblKensu.Text = "0件"
                puErrMsg = ""
                Return False
            End If

            '件数判定(判定を行い表示しない場合処理を抜ける)
            If dtResultCount.Rows(0).Item(0) > PropSearchMsgCount Then
                If MsgBox(String.Format(F0101_W001, PropSearchMsgCount), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
                    'コネクションを閉じる
                    Cn.Close()
                    '[mod] 2012/09/06 y.ikushima Excel出力対応 START
                    blnCheckFlg = False
                    'データソースを空に設定
                    If CreateDataTable(dataHBKF0101) = False Then
                        Return False
                    End If
                    'Spread描写
                    If SetVwData(dataHBKF0101) = False Then
                        Return False
                    End If
                    '件数の表示
                    dataHBKF0101.PropLblKensu.Text = "0件"
                    dataHBKF0101.PropBtnOutput.Enabled = False
                    '[mod] 2012/09/06 y.ikushima Excel出力対応 END
                    '終了ログ出力
                    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                    '正常終了
                    Return True
                End If
            End If

            '表示データ取得処理
            If sqlHBKF0101.SetSelectRelInfoSql(dataHBKF0101, Adapter, Cn) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "リリース一覧データ取得", Nothing, Adapter.SelectCommand)
            'SQL実行
            Adapter.Fill(dtResultInfo)

            'Dataクラスに格納
            dataHBKF0101.PropDtSearchResultCount = dtResultCount
            dataHBKF0101.PropDtSearchResult = dtResultInfo

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
    ''' <param name="dataHBKF0101">[IN/OUT]リリース検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果の表示設定を行う
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetResultRel(ByRef dataHBKF0101 As DataHBKF0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'スプレッド出力データ設定処理
            If SetVwData(dataHBKF0101) = False Then
                Return False
            End If

            '件数判定
            With dataHBKF0101

                '件数の表示
                .PropLblKensu.Text = String.Format("{0}件", .PropDtSearchResultCount.Rows(0).Item(0))

                ''件数チェック
                'If .PropDtSearchResultCount.Rows(0).Item(0) = 0 Then
                '    puErrMsg = F0101_E001
                '    Return False
                'End If
            End With

            '検索結果の背景色設定
            If SetBGColor(dataHBKF0101) = False Then
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
    ''' <param name="dataHBKF0101">[IN/OUT]リリース検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>テーブルから取得した内容をスプレッドに設定する
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetVwData(ByRef dataHBKF0101 As DataHBKF0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '検索結果
            With dataHBKF0101.PropVwReleaseSearch.Sheets(0)

                .DataSource = dataHBKF0101.PropDtSearchResult
                .Columns(COL_RELMB).DataField = "RelNmb"                        'リリース番号
                .Columns(COL_RELUKENMB).DataField = "RelUkeNmb"                 'リリース受付番号
                .Columns(COL_PROCESSSTATENM).DataField = "ProcessStateNM"       'プロセスステータス名
                .Columns(COL_IRAIDT).DataField = "IraiDT"                       '依頼日
                .Columns(COL_TITLE).DataField = "Title"                         'タイトル
                .Columns(COL_TUJYOKINKYU).DataField = "TujyoKinkyuKbn"          '通常・受付
                .Columns(COL_USRSYUTI).DataField = "UsrSyutiKbn"                'ユーザ周知
                .Columns(COL_RELSCEDT).DataField = "RelSceDT"                  'リリース予定日
                .Columns(COL_RELSTDT).DataField = "RelStDT"                     'リリース着手日
                .Columns(COL_RELEDDT).DataField = "RelEdDT"                     'リリース完了日
                .Columns(COL_GROUPNM).DataField = "TantoGrpNM"                  '担当者業務グループ
                .Columns(COL_HBKUSR).DataField = "RelTantoNM"                   'リリース担当者
                .Columns(COL_PROCESSSTATECD).DataField = "ProcessStateCD"       'プロセスステータスCD
                .Columns(COL_HBKUSRCD).DataField = "RelTantoID"                 'リリース担当者CD
                .Columns(COL_GROUPCD).DataField = "TantoGrpCD"                  '担当者業務グループCD
                .Columns(COL_SORT).DataField = "Sort"                           'ソート

                '非表示処理
                .Columns(COL_PROCESSSTATECD).Visible = False
                .Columns(COL_HBKUSRCD).Visible = False
                .Columns(COL_GROUPCD).Visible = False
                .Columns(COL_SORT).Visible = False
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
    ''' <param name="dataHBKF0101">[IN/OUT]リリース検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドのセルの値を判定して背景色を変更する
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetBGColor(ByRef dataHBKF0101 As DataHBKF0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '検索結果
            With dataHBKF0101.PropVwReleaseSearch.Sheets(0)

                '表示件数分ループ
                For i = 0 To .RowCount - 1
                    'ステータス
                    If .Cells(i, COL_PROCESSSTATECD).Value = PROCESS_STATUS_RELEASE_MICHAKUSYU Then
                        '未着手(白)
                        .Cells(i, COL_PROCESSSTATENM).BackColor = Color.White
                    ElseIf .Cells(i, COL_PROCESSSTATECD).Value = PROCESS_STATUS_RELEASE_MIJISSHIKANRYO Then
                        '未実施完了(灰色)
                        .Cells(i, COL_PROCESSSTATENM).BackColor = Color.Silver
                    ElseIf .Cells(i, COL_PROCESSSTATECD).Value = PROCESS_STATUS_RELEASE_CHOSACHU Or _
                            .Cells(i, COL_PROCESSSTATECD).Value = PROCESS_STATUS_RELEASE_SAGYOUMACHI Or _
                            .Cells(i, COL_PROCESSSTATECD).Value = PROCESS_STATUS_RELEASE_SAGYOUCHU Then
                        '調整中、リリース作業待、リリース作業中(黄色)
                        .Cells(i, COL_PROCESSSTATENM).BackColor = Color.Yellow
                    ElseIf .Cells(i, COL_PROCESSSTATECD).Value = PROCESS_STATUS_RELEASE_SYONINIRAICHU Or _
                            .Cells(i, COL_PROCESSSTATECD).Value = PROCESS_STATUS_RELEASE_KANRYOSYONINMACHI Then
                        '承認依頼中、完了承認待(黄緑)
                        .Cells(i, COL_PROCESSSTATENM).BackColor = Color.GreenYellow
                    ElseIf .Cells(i, COL_PROCESSSTATECD).Value = PROCESS_STATUS_RELEASE_KANRYO Then
                        '完了(青)
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
    ''' <param name="dataHBKF0101">[IN/OUT]リリース検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面コントロールクリア処理を行う
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ClearFormMain(ByRef dataHBKF0101 As DataHBKF0101) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'コントロール初期設定処理
        If SetFormControlInitial(dataHBKF0101) = False Then
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
    ''' <param name="dataHBKF0101">[IN/OUT]リリース検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索ボタン押下時、画面コントロールの検索条件を保存する
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetSreachConditionSave(ByRef dataHBKF0101 As DataHBKF0101) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        Dim blnTryNum As Boolean

        Try
            With dataHBKF0101
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

                'リリース基本情報---------------------------------------------------------------------------------------------
                'リリース番号
                .PropStrRelNmb = ""
                If .PropTxtRelNmb.Text <> "" Then
                    If Integer.TryParse(.PropTxtRelNmb.Text, blnTryNum) = False Then
                        '数値変換に失敗した場合は空白設定
                        .PropStrRelNmb = "0"
                    Else
                        .PropStrRelNmb = .PropTxtRelNmb.Text
                    End If
                End If
                .PropStrRelUkeNmb = .PropTxtRelUkeNmb.Text                                      'リリース受付番号
                'ステータス
                .PropStrProcessState = ""
                For i As Integer = 0 To .PropLstProcessState.SelectedItems.Count - 1
                    If .PropStrProcessState = "" Then
                        .PropStrProcessState = "'" & .PropLstProcessState.SelectedItems(i)("ProcessStateCD") & "'"
                    Else
                        .PropStrProcessState = .PropStrProcessState & ",'" & .PropLstProcessState.SelectedItems(i)("ProcessStateCD") & "'"
                    End If
                Next

                .PropStrTitle = .PropTxtTitle.Text                                              'タイトル
                .PropStrGaiyo = .PropTxtGaiyo.Text                                              '概要
                .PropStrUsrSyutiKbn = .PropCmbUsrSyutiKbn.SelectedValue                         'ユーザ周知必要有無
                .PropStrIraiDTFrom = .PropDtpIraiDTFrom.txtDate.Text                            '依頼日(FROM)
                .PropStrIraiDTTo = .PropDtpIraiDTTo.txtDate.Text                                '依頼日(TO)
                .PropStrRelSceDTFrom = .PropDtpRelSceDTFrom.txtDate.Text                        'リリース予定日(FROM)
                .PropStrRelSceDTTo = .PropDtpRelSceDTto.txtDate.Text                            'リリース予定日(TO)
                .PropStrRelStDTFrom = .PropDtpRelStDTFrom.txtDate.Text                          'リリース着手日(FROM)
                .PropStrRelStDTTo = .PropDtpRelStDTTo.txtDate.Text                              'リリース着手日(TO)

                .PropStrBiko = .PropTxtBiko.Text                                                'フリーテキスト
                .PropStrFreeFlg1 = .PropCmbFreeFlg1.SelectedValue                               'フリーフラグ1
                .PropStrFreeFlg2 = .PropCmbFreeFlg2.SelectedValue                               'フリーフラグ2
                .PropStrFreeFlg3 = .PropCmbFreeFlg3.SelectedValue                               'フリーフラグ3
                .PropStrFreeFlg4 = .PropCmbFreeFlg4.SelectedValue                               'フリーフラグ4
                .PropStrFreeFlg5 = .PropCmbFreeFlg5.SelectedValue                               'フリーフラグ5

                '担当者情報-------------------------------------------------------------------------------------------------
                .PropStrTantoGrpCD = .PropCmbTantoGrpCD.SelectedValue                           '担当者グループ
                .PropStrTantoID = .PropTxtTantoID.Text                                          '担当者ID
                .PropStrTantoNM = .PropTxtTantoNM.Text                                          '担当者名

                'プロセスリンク情報------------------------------------------------------------------------------------------------
                .PropStrKindCD = .PropCmbKindCD.SelectedValue                                   '種別
                .PropStrNum = .PropTxtNum.Text                                                  '番号

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
    ''' <para>作成情報：2012/08/20 y.ikushima
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
            If sqlHBKF0101.SetProccesLinkSql(Adapter, Cn, StrProccesLinkKind, StrProcessLinkNum) = False Then
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
    ''' デフォルトソートメイン処理
    ''' </summary>
    ''' <param name="dataHBKF0101">[IN/OUT]リリース検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>デフォルトソートメイン処理
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SortDefaultMain(ByRef dataHBKF0101 As DataHBKF0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ソート設定
        If SortSearchData(dataHBKF0101) = False Then
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
    ''' <param name="dataHBKF0101">[IN/OUT]リリース検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果を検索時のソート順に並べ替える
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function SortSearchData(ByRef dataHBKF0101 As DataHBKF0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKF0101.PropVwReleaseSearch.Sheets(0)
                Dim Si(0) As SortInfo 'ソート対象配列

                'リリース番号の降順に変更する
                'ソート対象列をソートする順番で指定
                Si(0) = New SortInfo(COL_SORT, True) 'ソート順(リリース予定日昇順【NULLが最上段】リリース番号降順)

                'ソート順(リリース予定日昇順【NULLが最上段】リリース番号降順)でソートを行う
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
    ''' <param name="dataHBKF0101">[IN/OUT]リリース検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetIncTantoDataMain(ByRef dataHBKF0101 As DataHBKF0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            'マスタデータ取得
            If GetIncTantoData(Adapter, Cn, dataHBKF0101) = False Then
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
    ''' <param name="dataHBKF0101">[IN/OUT]リリース検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>マスタデータを取得する
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetIncTantoData(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKF0101 As DataHBKF0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKF0101.GetIncTantoInfoData(Adapter, Cn, dataHBKF0101) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "担当マスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            '取得データをデータクラスにセット
            dataHBKF0101.PropDtResultSub = dtmst

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
