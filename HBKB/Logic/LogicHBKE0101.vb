Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.Drawing
Imports FarPoint.Win.Spread

''' <summary>
''' 変更検索一覧画面ロジッククラス
''' </summary>
''' <remarks>変更検索一覧画面Logicクラス
''' <para>作成情報：2012/08/21 k.imayama
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKE0101

    'インスタンス作成
    Private sqlHBKE0101 As New SqlHBKE0101
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    '*************************************************************************
    '定数宣言
    Public Const COL_SEARCHLIST_CHGNMB As Integer = 0           '番号
    Public Const COL_SEARCHLIST_PROCESSSTATENM As Integer = 1   'プロセスステータス名称
    Public Const COL_SEARCHLIST_KAISIDT As Integer = 2         '開始日時
    Public Const COL_SEARCHLIST_TITLE As Integer = 3            'タイトル
    Public Const COL_SEARCHLIST_NUM As Integer = 4              '対象システム
    Public Const COL_SEARCHLIST_GROUPNM As Integer = 5          '担当者業務グループ名称
    Public Const COL_SEARCHLIST_CHGTANTONM As Integer = 6       '変更担当者
    Public Const COL_SEARCHLIST_PROCESSSTATECD As Integer = 7   'プロセスステータスCD
    Public Const COL_SEARCHLIST_TANTOGRPCD As Integer = 8       '担当者業務グループCD
    Public Const COL_SEARCHLIST_CHGTANTOID As Integer = 9       '変更担当者ID
    Public Const COL_SEARCHLIST_SORTDT As Integer = 10          'ソート日付
    '*************************************************************************

    '各項目リストボックス
    Private Const LIST_COLMUN_ZERO As Integer = 0               'リストボックスの0列目

    ''' <summary>
    ''' 画面初期表示設定処理メイン
    ''' </summary>
    ''' <param name="dataHBKE0101">[IN/OUT]変更検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>変更検索一覧画面の初期表示に必要なデータを取得する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKE0101 As DataHBKE0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '初期データ取得処理
        If GetInitData(dataHBKE0101) = False Then
            Return False
        End If

        'コンボボックス作成処理
        If CreateCmbBox(dataHBKE0101) = False Then
            Return False
        End If

        'リストボックス作成処理
        If CreateLstBox(dataHBKE0101) = False Then
            Return False
        End If

        '検索条件フォームオブジェクト初期化処理
        If InitSearchForm(dataHBKE0101) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 初期表示データ取得処理
    ''' </summary>
    ''' <param name="dataHBKE0101">[IN/OUT]変更検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>変更検索一覧画面の初期表示に必要なデータを取得する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKE0101 As DataHBKE0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try
            'コネクションを開く
            Cn.Open()

            'グループマスター取得（コンボボックス用）
            If GetGrp(Adapter, Cn, dataHBKE0101) = False Then
                Return False
            End If

            'プロセスステータスマスター取得（リストボックス用）
            If GetProcessState(Adapter, Cn, dataHBKE0101) = False Then
                Return False
            End If

            '対象システム取得（リストボックス用）
            If GetTargetSystem(Adapter, Cn, dataHBKE0101) = False Then
                Return False
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            Adapter.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' コンボボックス用グループマスター取得
    ''' </summary>
    ''' <param name="Adapter">[IN]データアダプター</param>
    ''' <param name="Cn">[IN]コネクション</param>
    ''' <param name="dataHBKE0101">[IN/OUT]変更検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>担当者グループコンボボックスの初期表示に必要なデータを取得する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetGrp(ByVal Adapter As NpgsqlDataAdapter, _
                            ByVal Cn As NpgsqlConnection, _
                            ByRef dataHBKE0101 As DataHBKE0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtGrp As New DataTable 'グループデータ

        Try

            'SQLの作成・設定
            If sqlHBKE0101.SetSelectGrpSql(Adapter, Cn, dataHBKE0101) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "グループマスター", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtGrp)

            '取得データをデータクラスにセット
            dataHBKE0101.PropDtGrp = dtGrp

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
            dtGrp.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' リストボックス用プロセスステータスマスター取得
    ''' </summary>
    ''' <param name="Adapter">[IN]データアダプター</param>
    ''' <param name="Cn">[IN]コネクション</param>
    ''' <param name="dataHBKE0101">[IN/OUT]変更検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ステータスリストボックスの初期表示に必要なデータを取得する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetProcessState(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef dataHBKE0101 As DataHBKE0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtProcessState As New DataTable 'プロセスステータスデータ

        Try

            'SQLの作成・設定
            If sqlHBKE0101.SetSelectProcessStateSql(Adapter, Cn, dataHBKE0101) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "プロセスステータスマスター", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtProcessState)

            '取得データをデータクラスにセット
            dataHBKE0101.PropDtProcessState = dtProcessState

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
            dtProcessState.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' リストボックス用対象システムデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]データアダプター</param>
    ''' <param name="Cn">[IN]コネクション</param>
    ''' <param name="dataHBKE0101">[IN/OUT]変更検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対象システムリストボックスの初期表示に必要なデータを取得する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetTargetSystem(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef dataHBKE0101 As DataHBKE0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtTargetSystem As New DataTable '対象システムデータ

        Try

            'SQLの作成・設定
            If sqlHBKE0101.SetSelectTargetSystemSql(Adapter, Cn, dataHBKE0101) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対象システム", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtTargetSystem)

            '取得データをデータクラスにセット
            dataHBKE0101.PropDtTargetSystem = dtTargetSystem

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
            dtTargetSystem.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' コンボボックス作成処理
    ''' </summary>
    ''' <param name="dataHBKE0101">[IN/OUT]変更検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のコンボボックスを作成する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateCmbBox(ByRef dataHBKE0101 As DataHBKE0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKE0101

                '担当者グループ
                If commonLogic.SetCmbBox(.PropDtGrp, .PropCmbTantoGrp, True, "", "") = False Then
                    Return False
                End If

                'プロセスリンク種別
                If commonLogic.SetCmbBox(ProcessType, .PropCmbProccesLinkKind) = False Then
                    Return False
                End If

                'フリーフラグ1
                If commonLogic.SetCmbBox(FreeFlg, .PropCmbFreeFlg1) = False Then
                    Return False
                End If

                'フリーフラグ2
                If commonLogic.SetCmbBox(FreeFlg, .PropCmbFreeFlg2) = False Then
                    Return False
                End If

                'フリーフラグ3
                If commonLogic.SetCmbBox(FreeFlg, .PropCmbFreeFlg3) = False Then
                    Return False
                End If

                'フリーフラグ4
                If commonLogic.SetCmbBox(FreeFlg, .PropCmbFreeFlg4) = False Then
                    Return False
                End If

                'フリーフラグ5
                If commonLogic.SetCmbBox(FreeFlg, .PropCmbFreeFlg5) = False Then
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
    ''' リストボックス作成処理
    ''' </summary>
    ''' <param name="dataHBKE0101">[IN/OUT]変更検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のリストボックスを作成する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateLstBox(ByRef dataHBKE0101 As DataHBKE0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKE0101

                'ステータス
                .PropLstStatus.ValueMember = "ProcessStateCD"
                .PropLstStatus.DisplayMember = "ProcessStateNM"
                .PropLstStatus.DataSource = dataHBKE0101.PropDtProcessState

                '対象システム
                .PropLstTargetSystem.ValueMember = "CINmb"
                .PropLstTargetSystem.DisplayMember = "SystemNM"
                .PropLstTargetSystem.DataSource = dataHBKE0101.PropDtTargetSystem

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
    ''' <param name="dataHBKE0101">[IN/OUT]変更検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>Spreadに表示するデータの取得処理を行う
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SearchIncidentMain(ByRef dataHBKE0101 As DataHBKE0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '閾値チェックパラメータ初期化
        dataHBKE0101.PropBlnIndicationFlg = False

        '画面入力チェック処理
        If CheckInputControl(dataHBKE0101) = False Then
            Return False
        End If

        'Excel出力用パラメータ設定処理
        If SetExcelOutPutParameter(dataHBKE0101) = False Then
            Return False
        End If

        '件数取得処理
        If GetResultCount(dataHBKE0101) = False Then
            Return False
        End If

        '閾値チェック処理
        If CheckThresholdValue(dataHBKE0101) = False Then
            Return False
        End If

        '閾値が件数を超えた際の表示判定
        If dataHBKE0101.PropBlnIndicationFlg = True Then
            '[mod] 2012/09/06 y.ikushima Excel出力対応 START
            dataHBKE0101.PropBtnMakeExcel.Enabled = False
            '[mod] 2012/09/06 y.ikushima Excel出力対応 END
            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True
        Else
            '[mod] 2012/09/06 y.ikushima Excel出力対応 START
            dataHBKE0101.PropBtnMakeExcel.Enabled = True
            '[mod] 2012/09/06 y.ikushima Excel出力対応 END
        End If

        'スプレッド用データテーブル作成処理
        If CreateDataTable(dataHBKE0101) = False Then
            Return False
        End If

        '検索結果取得処理
        If GetSearchData(dataHBKE0101) = False Then
            Return False
        End If

        '検索結果表示処理設定
        If SetResultIndication(dataHBKE0101) = False Then
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
    ''' <param name="dataHBKE0101">[IN/OUT]変更検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面コントロールに対する入力チェック処理を行う
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckInputControl(ByRef dataHBKE0101 As DataHBKE0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKE0101

                '最終更新時刻(FROM)に入力があり、日付が未入力
                If .PropDtpUpdateDTFrom.txtDate.Text.Trim = "" AndAlso .PropTxtExUpdateTimeFrom.PropTxtTime.Text <> "" Then
                    'エラーメッセージ設定
                    puErrMsg = String.Format(E0101_E002, "最終更新日時(FROM)")
                    'フォーカス設定
                    .PropDtpUpdateDTFrom.Focus()
                    'エラーを返す
                    Return False
                End If

                '最終更新時刻(TO)に入力があり、日付が未入力
                If .PropDtpUpdateDTTo.txtDate.Text.Trim = "" AndAlso .PropTxtExUpdateTimeTo.PropTxtTime.Text <> "" Then
                    'エラーメッセージ設定
                    puErrMsg = String.Format(E0101_E002, "最終更新日時(TO)")
                    'フォーカス設定
                    .PropDtpUpdateDTTo.Focus()
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
    ''' Excel出力用パラメーター設定処理
    ''' </summary>
    ''' <param name="dataHBKE0101">[IN/OUT]変更検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>Excel出力用のパラメータをセットする
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function SetExcelOutPutParameter(ByRef dataHBKE0101 As DataHBKE0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKE0101

                'ログイン者所属グループ
                .PropStrLoginUserGrp = Nothing
                For i = 0 To .PropGrpLoginUser.cmbGroup.Items.Count - 1
                    If .PropStrLoginUserGrp = "" Then
                        .PropStrLoginUserGrp = "'" & .PropGrpLoginUser.cmbGroup.Items(i)(LIST_COLMUN_ZERO) & "'"
                    Else
                        .PropStrLoginUserGrp = .PropStrLoginUserGrp & ",'" & .PropGrpLoginUser.cmbGroup.Items(i)(LIST_COLMUN_ZERO) & "'"
                    End If
                Next
                .PropStrLoginUserId = PropUserId                                        'ログイン者ID

                '変更番号
                If .PropTxtNum.Text.Trim <> "" Then
                    .PropBlnChgNumInputFlg = False                                      '入力判定結果:入力
                    '入力結果をセット(数値外文字が入力された場合0がセットされる)
                    Integer.TryParse(.PropTxtNum.Text, .PropStrChgNmb)
                Else
                    .PropBlnChgNumInputFlg = True                                       '入力判定結果:未入力
                End If

                '基本情報：ステータス
                .PropStrStatus = Nothing
                For i As Integer = 0 To .PropLstStatus.SelectedItems.Count - 1
                    If .PropStrStatus = "" Then
                        .PropStrStatus = "'" & .PropLstStatus.SelectedItems(i)(LIST_COLMUN_ZERO) & "'"
                    Else
                        .PropStrStatus = .PropStrStatus & ",'" & .PropLstStatus.SelectedItems(i)(LIST_COLMUN_ZERO) & "'"
                    End If
                Next

                '基本情報：対象システム
                .PropStrTargetSystem = Nothing
                For i As Integer = 0 To .PropLstTargetSystem.SelectedItems.Count - 1
                    If .PropStrTargetSystem = "" Then
                        .PropStrTargetSystem = "'" & .PropLstTargetSystem.SelectedItems(i)(LIST_COLMUN_ZERO) & "'"
                    Else
                        .PropStrTargetSystem = .PropStrTargetSystem & ",'" & .PropLstTargetSystem.SelectedItems(i)(LIST_COLMUN_ZERO) & "'"
                    End If
                Next

                .PropStrTitle = .PropTxtTitle.Text                                      '基本情報：タイトル
                .PropStrNaiyo = .PropTxtNaiyo.Text                                      '基本情報：内容
                .PropStrTaisyo = .PropTxtTaiosyo.Text                                   '基本情報：対処
                .PropStrCyspr = .PropTxtCyspr.Text                                      '基本情報：Cyspr
                .PropStrkaisidtFrom = .PropDtpkaisidtFrom.txtDate.Text                '基本情報：開始日(From)
                .PropStrkaisidtTo = .PropDtpkaisidtTo.txtDate.Text                    '基本情報：開始日(To)
                .PropStrKanryoDTFrom = .PropDtpkanryoDTFrom.txtDate.Text                '基本情報：完了日(From)
                .PropStrKanryoDTTo = .PropDtpkanryoDTTo.txtDate.Text                    '基本情報：完了日(To)
                .PropStrTorokuDTFrom = .PropDtpTorokuDTFrom.txtDate.Text                '基本情報：登録日(From)
                .PropStrTorokuDTTo = .PropDtpTorokuDTTo.txtDate.Text                    '基本情報：登録日(To)
                '[Mod]2015/01/23 e.okamura 問題要望114 Start
                '.PropStrUpdateDTFrom = .PropDtpUpdateDTFrom.txtDate.Text & " " & _
                '                       .PropTxtExUpdateTimeFrom.PropTxtTime.Text        '基本情報：最終更新日時(日付From)
                '.PropStrUpdateDTTo = .PropDtpUpdateDTTo.txtDate.Text & " " & _
                '                     .PropTxtExUpdateTimeTo.PropTxtTime.Text            '基本情報：最終更新日時(日付To)
                .PropStrUpdateDTFrom = (.PropDtpUpdateDTFrom.txtDate.Text & " " & _
                                       .PropTxtExUpdateTimeFrom.PropTxtTime.Text).Trim  '基本情報：最終更新日時(日付From)
                .PropStrUpdateDTTo = (.PropDtpUpdateDTTo.txtDate.Text & " " & _
                                     .PropTxtExUpdateTimeTo.PropTxtTime.Text).Trim      '基本情報：最終更新日時(日付To)
                '[Mod]2015/01/23 e.okamura 問題要望114 End
                '[Add]2014/11/19 e.okamura 問題要望114 Start
                .PropStrExUpdateTimeFrom = .PropTxtExUpdateTimeFrom.PropTxtTime.Text    '基本情報：最終更新日時(時刻From)
                .PropStrExUpdateTimeTo = .PropTxtExUpdateTimeTo.PropTxtTime.Text        '基本情報：最終更新日時(時刻To)
                '[Add]2014/11/19 e.okamura 問題要望114 End
                .PropStrFreeText = .PropTxtFreeText.Text                                '基本情報：フリーテキスト
                .PropStrFreeFlg1 = .PropCmbFreeFlg1.SelectedValue                       '基本情報：フリーフラグ1
                .PropStrFreeFlg2 = .PropCmbFreeFlg2.SelectedValue                       '基本情報：フリーフラグ2
                .PropStrFreeFlg3 = .PropCmbFreeFlg3.SelectedValue                       '基本情報：フリーフラグ3
                .PropStrFreeFlg4 = .PropCmbFreeFlg4.SelectedValue                       '基本情報：フリーフラグ4
                .PropStrFreeFlg5 = .PropCmbFreeFlg5.SelectedValue                       '基本情報：フリーフラグ5

                .PropStrTantoGrp = .PropCmbTantoGrp.SelectedValue                       '担当者情報情報：担当者グループ
                .PropStrTantoID = .PropTxtTantoID.Text                                  '担当者情報情報：担当者ID
                .PropStrTantoNM = .PropTxtTantoNM.Text                                  '担当者情報情報：担当者氏名

                .PropStrProccesLinkKind = .PropCmbProccesLinkKind.SelectedValue         'プロセスリンク情報：種別
                .PropStrProcessLinkNum = .PropTxtProcessLinkNum.Text                    'プロセスリンク情報：番号

                'プロセスリンク情報取得
                .PropStrProcessLinkNumAry = ""
                If .PropStrProccesLinkKind <> "" Or .PropStrProcessLinkNum <> "" Then
                    If GetProccesLink(.PropStrProccesLinkKind, .PropStrProcessLinkNum, .PropStrProcessLinkNumAry) = False Then
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
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetProccesLink(ByVal StrProccesLinkKind As String, ByVal StrProcessLinkNum As String, ByRef StrProcessLinkNumAry As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        '変数宣言
        Dim dtResultCount As New DataTable '検索件数

        Try

            'コネクションを開く
            Cn.Open()

            'SQLの作成・設定
            If sqlHBKE0101.SetProccesLinkSql(Adapter, Cn, StrProccesLinkKind, StrProcessLinkNum) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "プロセスリンク取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtResultCount)

            If dtResultCount.Rows.Count <> 0 Then
                StrProcessLinkNumAry = dtResultCount.Rows(0).Item(0).ToString
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
            Cn.Dispose()
            Adapter.Dispose()
            dtResultCount.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 検索件数取得処理
    ''' </summary>
    ''' <param name="dataHBKE0101">[IN/OUT]変更検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果の件数を取得する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetResultCount(ByRef dataHBKE0101 As DataHBKE0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        '変数宣言
        Dim dtResultCount As New DataTable '検索件数

        Try

            'コネクションを開く
            Cn.Open()

            'SQLの作成・設定
            If sqlHBKE0101.SetResultCountSql(Adapter, Cn, dataHBKE0101) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "検索件数", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtResultCount)

            '取得データをデータクラスにセット
            dataHBKE0101.PropDtResultCount = dtResultCount

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
            Cn.Dispose()
            Adapter.Dispose()
            dtResultCount.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 閾値チェック処理
    ''' </summary>
    ''' <param name="dataHBKE0101">[IN/OUT]変更検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索件すの判定を行う
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function CheckThresholdValue(ByRef dataHBKE0101 As DataHBKE0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKE0101

                '件数チェック
                If .PropDtResultCount.Rows(0).Item(0) = 0 Then
                                    '[mod] 2012/09/06 y.ikushima Excel出力対応 START
                    'データソースを空に設定
                    If CreateDataTable(dataHBKE0101) = False Then
                        Return False
                    End If
                    'Spread描写
                    If SetVwData(dataHBKE0101) = False Then
                        Return False
                    End If
                    '[mod] 2012/09/06 y.ikushima Excel出力対応 START
                    .PropBtnMakeExcel.Enabled = False
                    '[mod] 2012/09/06 y.ikushima Excel出力対応 END
                    '件数の表示
                    .PropLblResultCounter.Text = "0件"
                    'メッセージに空白を設定
                    puErrMsg = ""
                    Return False
                End If

                '件数の判定
                If dataHBKE0101.PropDtResultCount.Rows(0).Item(0) > PropSearchMsgCount Then
                    '件数が閾値以上で、表示しない(NO)を選択した場合処理を抜ける
                    If MsgBox(String.Format(E0101_W001, PropSearchMsgCount), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
                        '出力しない場合表示判定フラグをTrueにセット
                        dataHBKE0101.PropBlnIndicationFlg = True

                        '[mod] 2012/09/06 y.ikushima Excel出力対応 START
                        'データソースを空に設定
                        If CreateDataTable(dataHBKE0101) = False Then
                            Return False
                        End If
                        'Spread描写
                        If SetVwData(dataHBKE0101) = False Then
                            Return False
                        End If
                        '件数の表示
                        .PropLblResultCounter.Text = "0件"
                        '[mod] 2012/09/06 y.ikushima Excel出力対応 END
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
    ''' スプレッド用データテーブル作成処理
    ''' </summary>
    ''' <param name="dataHBKE0101">[IN/OUT]変更検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドに出力するためのデータテーブルの作成を行う
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateDataTable(ByRef dataHBKE0101 As DataHBKE0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtChangeInfo As New DataTable             '変更検索結果用データテーブル

        Try

            With dtChangeInfo

                .Columns.Add("ChgNmb", Type.GetType("System.Int32"))            '変更番号
                .Columns.Add("ProcessStateNM", Type.GetType("System.String"))   'ステータス
                .Columns.Add("kaisidt", Type.GetType("System.String"))         '開始日時
                .Columns.Add("Title", Type.GetType("System.String"))            'タイトル
                .Columns.Add("CINM", Type.GetType("System.String"))             '対象システム
                .Columns.Add("GroupNM", Type.GetType("System.String"))          '担当者業務グループ
                .Columns.Add("ChgTantoNM", Type.GetType("System.String"))       '変更担当者
                .Columns.Add("ProcessStateCD", Type.GetType("System.String"))   'プロセスステータスCD
                .Columns.Add("ChgTantoID", Type.GetType("System.String"))       '変更担当者ID
                .Columns.Add("TantoGrpCD", Type.GetType("System.String"))       '担当者業務グループCD

                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'データクラスをデータテーブルにセット
            dataHBKE0101.PropDtChangeInfo = dtChangeInfo              '変更検索結果

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
    ''' 変更検索結果取得処理
    ''' </summary>
    ''' <param name="dataHBKE0101">[IN/OUT変更検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>変更検索結果の取得を行う
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetSearchData(ByRef dataHBKE0101 As DataHBKE0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try
            'コネクションを開く
            Cn.Open()

            '変更検索結果取得（コンボボックス用）
            If GetChangeInfo(Adapter, Cn, dataHBKE0101) = False Then
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
            Cn.Dispose()
            Adapter.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' スプレッド用変更検索結果取得
    ''' </summary>
    ''' <param name="Adapter">[IN]データアダプター</param>
    ''' <param name="Cn">[IN]コネクション</param>
    ''' <param name="dataHBKE0101">[IN/OUT]変更検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>変更検索結果表示用スプレッドに必要なデータを取得する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetChangeInfo(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef dataHBKE0101 As DataHBKE0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'データテーブルの初期化
            dataHBKE0101.PropDtChangeInfo.Clear()

            'SQLの作成・設定
            If sqlHBKE0101.SetSelectChangeInfoSql(Adapter, Cn, dataHBKE0101) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "変更検索結果", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKE0101.PropDtChangeInfo)

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
    ''' 検索結果の表示処理
    ''' </summary>
    ''' <param name="dataHBKE0101">[IN/OUT]変更検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果の表示設定を行う
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetResultIndication(ByRef dataHBKE0101 As DataHBKE0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'スプレッド出力データ設定処理
            If SetVwData(DataHBKE0101) = False Then
                Return False
            End If

            '件数判定
            With DataHBKE0101

                '件数の表示
                .PropLblResultCounter.Text = .PropDtResultCount.Rows(0).Item(0) & "件"

                ''件数チェック
                'If .PropDtResultCount.Rows(0).Item(0) = 0 Then
                '    puErrMsg = C0101_E001
                '    Return False
                'End If

            End With

            '検索結果の背景色設定
            If SetBGColor(DataHBKE0101) = False Then
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
    ''' <param name="dataHBKE0101">[IN/OUT]変更検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>テーブルから取得した内容をスプレッドに設定する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetVwData(ByRef dataHBKE0101 As DataHBKE0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With DataHBKE0101

                '検索結果
                With .PropVwChangeList.Sheets(0)

                    .Rows.Clear()
                    .DataSource = Nothing
                    .DataSource = dataHBKE0101.PropDtChangeInfo
                    .Columns(COL_SEARCHLIST_CHGNMB).DataField = "ChgNmb"                    '番号
                    .Columns(COL_SEARCHLIST_PROCESSSTATENM).DataField = "ProcessStateNM"    'ステータス
                    .Columns(COL_SEARCHLIST_KAISIDT).DataField = "kaisidt"                '開始日時
                    .Columns(COL_SEARCHLIST_TITLE).DataField = "Title"                      'タイトル
                    .Columns(COL_SEARCHLIST_NUM).DataField = "CINM"                         '対象システム
                    .Columns(COL_SEARCHLIST_GROUPNM).DataField = "GroupNM"                  '担当者業務グループ
                    .Columns(COL_SEARCHLIST_CHGTANTONM).DataField = "ChgTantoNM"            '変更担当者
                    .Columns(COL_SEARCHLIST_PROCESSSTATECD).DataField = "ProcessStateCD"    'プロセスステータスCD
                    .Columns(COL_SEARCHLIST_CHGTANTOID).DataField = "ChgTantoID"            '変更担当者ID
                    .Columns(COL_SEARCHLIST_TANTOGRPCD).DataField = "TantoGrpCD"            '担当者業務グループCD

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
    ''' スプレッドのセルの背景色設定処理
    ''' </summary>
    ''' <param name="dataHBKE0101">[IN/OUT]変更検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドのセルの値を判定して背景色を変更する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetBGColor(ByRef dataHBKE0101 As DataHBKE0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKE0101.PropVwChangeList.Sheets(0)

                '表示件数分ループ
                For i = 0 To .RowCount - 1

                    'ステータス
                    Select Case .Cells(i, COL_SEARCHLIST_PROCESSSTATECD).Value
                        Case PROCESS_STATUS_CHANGE_MICHAKUSYU
                            '背景色の設定：白
                            .Cells(i, COL_SEARCHLIST_PROCESSSTATENM).BackColor = Color.White
                        Case PROCESS_STATUS_CHANGE_MIJISHIKANRYOU
                            '背景色の設定：グレー
                            .Cells(i, COL_SEARCHLIST_PROCESSSTATENM).BackColor = Color.Silver
                        Case PROCESS_STATUS_CHANGE_JUNBICHU
                            '背景色の設定：黄色
                            .Cells(i, COL_SEARCHLIST_PROCESSSTATENM).BackColor = Color.Yellow
                        Case PROCESS_STATUS_CHANGE_SHONINIRAICHU
                            '背景色の設定：黄緑
                            .Cells(i, COL_SEARCHLIST_PROCESSSTATENM).BackColor = Color.GreenYellow
                        Case PROCESS_STATUS_CHANGE_RELEASEMACHI
                            '背景色の設定：黄緑
                            .Cells(i, COL_SEARCHLIST_PROCESSSTATENM).BackColor = Color.GreenYellow
                        Case PROCESS_STATUS_CHANGE_KANRYOU
                            '背景色の設定：ライトブルー
                            .Cells(i, COL_SEARCHLIST_PROCESSSTATENM).BackColor = Color.Cyan
                        Case Else
                            .Cells(i, COL_SEARCHLIST_PROCESSSTATENM).BackColor = Color.White
                    End Select

                    '担当者業務グループ
                    If .Cells(i, COL_SEARCHLIST_CHGTANTOID).Value = PropUserId Then
                        '背景色の設定：黄色
                        .Cells(i, COL_SEARCHLIST_GROUPNM).BackColor = Color.Yellow
                    ElseIf .Cells(i, COL_SEARCHLIST_TANTOGRPCD).Value = PropWorkGroupCD Then
                        '背景色の設定：オレンジ
                        .Cells(i, COL_SEARCHLIST_GROUPNM).BackColor = Color.Orange
                    Else
                        '背景色の設定：黄緑
                        .Cells(i, COL_SEARCHLIST_GROUPNM).BackColor = Color.LawnGreen
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
    ''' 検索条件フォームオブジェクト初期化処理メイン
    ''' </summary>
    ''' <param name="dataHBKE0101">[IN/OUT]変更検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のリストボックスを作成する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ClearSearchFormMain(ByRef dataHBKE0101 As DataHBKE0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '検索条件フォームオブジェクト初期化処理
        If InitSearchForm(DataHBKE0101) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' デフォルトソート処理
    ''' </summary>
    ''' <param name="dataHBKE0101">[IN/OUT]変更検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>デフォルトソートメイン処理
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SortDefaultMain(ByRef dataHBKE0101 As DataHBKE0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ソート設定
        If SortSearchData(DataHBKE0101) = False Then
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
    ''' <param name="dataHBKE0101">[IN/OUT]変更検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果を検索時のソート順に並べ替える
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function SortSearchData(ByRef dataHBKE0101 As DataHBKE0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKE0101.PropVwChangeList.Sheets(0)
                Dim Si(0) As SortInfo 'ソート対象配列

                '変更番号の降順に変更する
                'ソート対象列をソートする順番で指定
                Si(0) = New SortInfo(COL_SEARCHLIST_CHGNMB, False) '変更番号

                '変更番号の昇順でソートを行う
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
    ''' 【共通】検索条件フォームオブジェクト初期化処理
    ''' </summary>
    ''' <param name="dataHBKE0101">[IN/OUT]変更検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のリストボックスを作成する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InitSearchForm(ByRef dataHBKE0101 As DataHBKE0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With DataHBKE0101

                '検索条件(フォームオブジェクト)
                .PropTxtNum.Text = ""                               '基本情報：番号

                '基本情報：ステータス
                For i As Integer = 0 To .PropLstStatus.Items.Count - 1
                    ''プロセスステータスCD="305"(完了)、306(未実施完了)以外のものを選択状態にする。
                    'If .PropLstStatus.Items(i)("ProcessStateCD") = PROCESS_STATUS_CHANGE_KANRYOU Or _
                    '   .PropLstStatus.Items(i)("ProcessStateCD") = PROCESS_STATUS_CHANGE_MIJISHIKANRYOU Then
                    'デフォルト選択フラグが"0"以外を選択状態にする。
                    If .PropLstStatus.Items(i)("Defaultselectflg") = DEFAULTSELECT_FLG_OFF Then
                        '項目を未選択状態に設定
                        .PropLstStatus.SetSelected(i, False)
                    Else
                        '項目を選択状態に設定
                        .PropLstStatus.SetSelected(i, True)
                    End If
                Next
                .PropLstStatus.TopIndex = 0

                .PropLstTargetSystem.ClearSelected()                '基本情報：対象システム
                .PropLstTargetSystem.TopIndex = 0
                .PropTxtTitle.Text = ""                             '基本情報：タイトル
                .PropTxtNaiyo.Text = ""                             '基本情報：内容
                .PropTxtTaiosyo.Text = ""                           '基本情報：対処
                .ProptxtCyspr.Text = ""                             '基本情報：CYSPR
                .PropDtpkaisidtFrom.txtDate.Text = ""              '基本情報：開始日(From)
                .PropDtpkaisidtTo.txtDate.Text = ""                '基本情報：開始日(To)
                .PropDtpkanryoDTFrom.txtDate.Text = ""              '基本情報：完了日(From)
                .PropDtpkanryoDTTo.txtDate.Text = ""                '基本情報：完了日(To)
                .PropDtpTorokuDTFrom.txtDate.Text = ""              '基本情報：登録日(From)
                .PropDtpTorokuDTTo.txtDate.Text = ""                '基本情報：登録日(To)
                .PropDtpUpdateDTFrom.txtDate.Text = ""              '基本情報：最終更新日時(日付From)
                .PropTxtExUpdateTimeFrom.PropTxtTime.Text = ""      '基本情報：最終更新日時(時刻From)
                .PropDtpUpdateDTTo.txtDate.Text = ""                '基本情報：最終更新日時(日付To)
                .PropTxtExUpdateTimeTo.PropTxtTime.Text = ""        '基本情報：最終更新日時(時刻To)
                .PropCmbTantoGrp.SelectedValue = PropWorkGroupCD    '担当者情報情報：担当者グループ
                .PropTxtTantoID.Text = ""                           '担当者情報情報：担当者ID
                .PropTxtTantoNM.Text = ""                           '担当者情報情報：担当者氏名
                .PropCmbProccesLinkKind.SelectedValue = ""          'プロセスリンク情報：種別
                .PropTxtProcessLinkNum.Text = ""                    'プロセスリンク情報：番号
                .PropTxtFreeText.Text = ""                          '基本情報：フリーテキスト
                .PropCmbFreeFlg1.SelectedValue = ""                 '基本情報：フリーフラグ1
                .PropCmbFreeFlg2.SelectedValue = ""                 '基本情報：フリーフラグ2
                .PropCmbFreeFlg3.SelectedValue = ""                 '基本情報：フリーフラグ3
                .PropCmbFreeFlg4.SelectedValue = ""                 '基本情報：フリーフラグ4
                .PropCmbFreeFlg5.SelectedValue = ""                 '基本情報：フリーフラグ5

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
    ''' 担当マスタデータ取得メイン処理
    ''' </summary>
    ''' <param name="dataHBKE0101">[IN/OUT]変更検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetIncTantoDataMain(ByRef dataHBKE0101 As DataHBKE0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try

            'コネクションを開く
            Cn.Open()

            'マスタデータ取得
            If GetIncTantoData(Adapter, Cn, dataHBKE0101) = False Then
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
            Cn.Dispose()
            Adapter.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 担当マスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKE0101">[IN/OUT]変更検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>マスタデータを取得する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetIncTantoData(ByVal Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKE0101 As DataHBKE0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKE0101.GetIncTantoInfoData(Adapter, Cn, dataHBKE0101) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "担当マスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            '取得データをデータクラスにセット
            dataHBKE0101.PropDtResultSub = dtmst

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
