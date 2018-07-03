Imports Common
Imports CommonHBK
Imports Npgsql
Imports FarPoint.Win.Spread

''' <summary>
''' 部所有機器検索一覧画面ロジッククラス
''' </summary>
''' <remarks>部所有機器検索一覧画面のロジックを定義したクラス
''' <para>作成情報：2012/06/20 s.yamaguchi
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKB1201

    'インスタンス生成
    Private sqlHBKB1201 As New SqlHBKB1201
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    '定数宣言
    Public Const COL_SEARCHLIST_KINDNM As Integer = 0           '種別列
    Public Const COL_SEARCHLIST_NUM As Integer = 1              '番号列
    Public Const COL_SEARCHLIST_ALIAU As Integer = 2            'エイリアス列
    Public Const COL_SEARCHLIST_CLASS2 As Integer = 3           'メーカー列
    Public Const COL_SEARCHLIST_CINM As Integer = 4             '機種列
    Public Const COL_SEARCHLIST_CISTNM As Integer = 5           'ステータス列
    Public Const COL_SEARCHLIST_EXPDT As Integer = 6            '有効日列
    Public Const COL_SEARCHLIST_NUMINFOKBN As Integer = 7       '番号通知列
    Public Const COL_SEARCHLIST_SEALSENDKBN As Integer = 8      'シール送付列
    Public Const COL_SEARCHLIST_AVSCHECKKBN As Integer = 9      'ウィルス対策ソフト確認列
    Public Const COL_SEARCHLIST_AVSCHECKDT As Integer = 10      'ウィルス対策ソフトサーバ確認日列
    Public Const COL_SEARCHLIST_USRBUSYONM As Integer = 11      'ユーザ所属部署列
    Public Const COL_SEARCHLIST_USRID As Integer = 12           'ユーザID列
    Public Const COL_SEARCHLIST_USRNM As Integer = 13           'ユーザ氏名列
    Public Const COL_SEARCHLIST_MANAGEKYOKUNM As Integer = 14   '管理局列
    Public Const COL_SEARCHLIST_MANAGEBUSYONM As Integer = 15   '管理部署列
    Public Const COL_SEARCHLIST_SETBUSYONM As Integer = 16      '設置部署
    Public Const COL_SEARCHLIST_SORT As Integer = 17            'ソート順
    Public Const COL_SEARCHLIST_CINMB As Integer = 18           'CI番号

    ''' <summary>
    ''' 画面初期表示メイン処理
    ''' </summary>
    ''' <param name="dataHBKB1201">[IN/OUT]部所有機器検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>部所有検索一覧画面呼出時に初期データをセットする
    ''' <para>作成情報：2012/06/20 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKB1201 As DataHBKB1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '初期データ取得処理
        If GetInitData(dataHBKB1201) = False Then
            Return False
        End If

        'コンボボックス作成処理
        If CreateCmb(dataHBKB1201) = False Then
            Return False
        End If

        'データ初期化処理
        If InitData(dataHBKB1201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 画面初期表示データ取得処理
    ''' </summary>
    ''' <param name="dataHBKB1201">[IN/OUT]部所有機器検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>部所有機器検索一覧画面の初期表示に必要なデータを取得する
    ''' <para>作成情報：2012/06/20 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKB1201 As DataHBKB1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try
            'コネクションを開く
            Cn.Open()

            'マスタデータ取得（コンボボックス用）
            If GetStatusMaster(Adapter, Cn, dataHBKB1201) = False Then
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
    ''' コンボボックス用ステータスデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]データアダプター</param>
    ''' <param name="Cn">[IN]コネクション</param>
    ''' <param name="dataHBKB1201">[IN/OUT]部所有機器検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>部所有機器検索一覧画面の初期表示に必要なデータを取得する
    ''' <para>作成情報：2012/06/20 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetStatusMaster(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef dataHBKB1201 As DataHBKB1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtCIStatus As New DataTable 'CIステータス情報

        Try

            'SQLの作成・設定
            If sqlHBKB1201.SetSelectCIStatusMasterSql(Adapter, Cn, dataHBKB1201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIステータスマスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtCIStatus)

            '取得データをデータクラスにセット
            dataHBKB1201.PropDtCIStatus = dtCIStatus

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

            dtCIStatus.Dispose()

        End Try

    End Function

    ''' <summary>
    '''コンボボックス作成処理
    ''' </summary>
    ''' <param name="dataHBKB1201">[IN/OUT]部所有機器検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のコンボボックスを作成する
    ''' <para>作成情報：2012/06/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateCmb(ByRef dataHBKB1201 As DataHBKB1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1201

                'CIステータスコンボボックス作成
                If commonLogic.SetCmbBox(.PropDtCIStatus, .PropCmbStatus, True, "", "") = False Then
                    Return False
                End If

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
    ''' データ初期化処理
    ''' </summary>
    ''' <param name="dataHBKB1201">[IN/OUT]部所有機器検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>各初期化処理を呼び出しデータを初期化する
    ''' <para>作成情報：2012/07/19 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InitData(dataHBKB1201 As DataHBKB1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '検索フォームオブジェクト初期化処理
            If InitSearchControl(dataHBKB1201) = False Then
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
    ''' 検索フォームオブジェクト初期化処理
    ''' </summary>
    ''' <param name="dataHBKB1201">[IN/OUT]部所有機器検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索条件入力フォームに入力された内容を初期化する
    ''' <para>作成情報：2012/06/22 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InitSearchControl(ByRef dataHBKB1201 As DataHBKB1201) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '各検索用のコントロールを初期化する
            With dataHBKB1201

                .PropTxtNumber.Text = ""                    '【テキストボックス】：番号
                .PropCmbStatus.SelectedValue = ""           '【セレクトボックス】：ステータス
                .PropTxtUserId.Text = ""                    '【テキストボックス】：ユーザID
                .PropTxtSyozokuBusyo.Text = ""              '【テキストボックス】：所属部署
                .PropTxtKanriBusyo.Text = ""                '【テキストボックス】：管理部署
                .PropTxtSettiBusyo.Text = ""                '【テキストボックス】：設置部署
                .PropTxtFreeText.Text = ""                  '【テキストボックス】：フリーテキスト
                .PropCmbFreeFlg1.SelectedValue = ""         '【セレクトボックス】：フリーフラグ1
                .PropCmbFreeFlg2.SelectedValue = ""         '【セレクトボックス】：フリーフラグ2
                .PropCmbFreeFlg3.SelectedValue = ""         '【セレクトボックス】：フリーフラグ3
                .PropCmbFreeFlg4.SelectedValue = ""         '【セレクトボックス】：フリーフラグ4
                .PropCmbFreeFlg5.SelectedValue = ""         '【セレクトボックス】：フリーフラグ5

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
    ''' 部所有機器検索結果表示処理メイン
    ''' </summary>
    ''' <param name="dataHBKB1201">[IN/OUT]部所有機器検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>部所有機器検索を行い結果を表示する
    ''' <para>作成情報：2012/06/20 s.yamaguchi
    ''' <p>改訂情報：2012/07/05 s.yamaguchi</p>
    ''' </para></remarks>
    Public Function SearchDataMain(ByRef dataHBKB1201 As DataHBKB1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ボタン活性非活性フラグをTrueにセット
        dataHBKB1201.PropBlnEnabledFlg = True

        'Excel出力フラグをTrueに設定する(一度でも検索ボタンが押された場合のみ出力する)
        dataHBKB1201.PropBlnExcelOutputFlg = True

        'Excel出力用検索条件パラメータセット
        If SetOutputExcelParameter(dataHBKB1201) = False Then
            Return False
        End If

        '件数取得処理
        If GetResultCount(dataHBKB1201) = False Then
            Return False
        End If

        '件数判定(判定を行い表示しない場合処理を抜ける)
        If dataHBKB1201.PropResultCount.Rows(0).Item(0) = 0 Then

            '件数が0件の場合空白をセット
            puErrMsg = ""

            'ボタン活性非活性フラグをFalseにセット
            dataHBKB1201.PropBlnEnabledFlg = False

            '表示をクリアする
            If ClearResultSpread(dataHBKB1201) = False Then
                Return False
            End If

            '出力ボタン活性非活性処理
            If ChangeEnabled(dataHBKB1201) = False Then
                Return False
            End If

            Return False

        ElseIf dataHBKB1201.PropResultCount.Rows(0).Item(0) > PropSearchMsgCount Then

            '件数が20件以上で表示しない(NO)を選択した場合処理を抜ける
            If MsgBox(String.Format(B1201_W001, PropSearchMsgCount), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then

                'ボタン活性非活性フラグをFalseにセット
                dataHBKB1201.PropBlnEnabledFlg = False

                '表示をクリアする
                If ClearResultSpread(dataHBKB1201) = False Then
                    Return False
                End If

                '出力ボタン活性非活性処理
                If ChangeEnabled(dataHBKB1201) = False Then
                    Return False
                End If

                '終了ログ出力
                commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                '正常終了
                Return True

            End If

        End If

        'スプレッド用データテーブル作成処理
        If CreateDataTable(dataHBKB1201) = False Then
            Return False
        End If

        '検索結果取得処理
        If GetSearchData(dataHBKB1201) = False Then
            Return False
        End If

        'スプレッド出力データ設定処理
        If SetVwData(dataHBKB1201) = False Then
            Return False
        End If

        '件数表示処理
        If SetResultCount(dataHBKB1201) = False Then
            Return False
        End If

        '出力ボタン活性非活性処理
        If ChangeEnabled(dataHBKB1201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 検索結果件数データ取得
    ''' </summary>
    ''' <param name="dataHBKB1201">[IN/OUT]部所有機器検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果件数を取得する
    ''' <para>作成情報：2012/7/05 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetResultCount(ByRef dataHBKB1201 As DataHBKB1201) As Boolean

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
            If sqlHBKB1201.SetResultCountSql(Adapter, Cn, dataHBKB1201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "検索結果件数", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtResultCount)

            '取得データをデータクラスにセット
            dataHBKB1201.PropResultCount = dtResultCount

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
            'リソースの解放
            Adapter.Dispose()
            Cn.Dispose()
            dtResultCount.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' Excel出力用パラメータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB1201">[IN/OUT]部所有機器検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>Excel出力を行う際に必要なパラメータ（検索条件）をセットする
    ''' <para>作成情報：2012/07/12 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetOutputExcelParameter(dataHBKB1201 As DataHBKB1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1201
                'フォームオブジェクトに入力されている検索条件の値をプロパティにセット
                .PropStrNumber = .PropTxtNumber.Text                '検索条件:番号
                .PropStrStatus = .PropCmbStatus.SelectedValue       '検索条件:ステータス
                .PropStrUserId = .PropTxtUserId.Text                '検索条件:ユーザID
                .PropStrSyozokuBusyo = .PropTxtSyozokuBusyo.Text    '検索条件:ユーザ所属部署
                .PropStrKanriBusyo = .PropTxtKanriBusyo.Text        '検索条件:管理部署
                .PropStrSettiBusyo = .PropTxtSettiBusyo.Text        '検索条件:設置部署
                .PropStrFreeText = .PropTxtFreeText.Text            '検索条件:フリーテキスト
                .PropStrFreeFlg1 = .PropCmbFreeFlg1.SelectedValue   '検索条件:フリーフラグ1
                .PropStrFreeFlg2 = .PropCmbFreeFlg2.SelectedValue   '検索条件:フリーフラグ2
                .PropStrFreeFlg3 = .PropCmbFreeFlg3.SelectedValue   '検索条件:フリーフラグ3
                .PropStrFreeFlg4 = .PropCmbFreeFlg4.SelectedValue   '検索条件:フリーフラグ4
                .PropStrFreeFlg5 = .PropCmbFreeFlg5.SelectedValue   '検索条件:フリーフラグ5
            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 + ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    '''スプレッドの出力データ設定処理
    ''' </summary>
    ''' <param name="dataHBKB1201">[IN/OUT]部所有機器検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>テーブルから取得した内容をスプレッドに設定する
    ''' <para>作成情報：2012/06/21 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetVwData(ByRef dataHBKB1201 As DataHBKB1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1201

                '検索結果
                With .PropVwBusyoyuukikiList.Sheets(0)

                    .DataSource = Nothing
                    .DataSource = dataHBKB1201.PropDtCIInfo
                    .Columns(COL_SEARCHLIST_KINDNM).DataField = "KindNM"                        '種別
                    .Columns(COL_SEARCHLIST_NUM).DataField = "Num"                              '番号
                    .Columns(COL_SEARCHLIST_ALIAU).DataField = "Aliau"                          'エイリアス
                    .Columns(COL_SEARCHLIST_CLASS2).DataField = "Class2"                        'メーカー
                    .Columns(COL_SEARCHLIST_CINM).DataField = "CINM"                            '機種
                    .Columns(COL_SEARCHLIST_CISTNM).DataField = "CIStateNM"                     'ステータス
                    .Columns(COL_SEARCHLIST_EXPDT).DataField = "ExpirationDT"                   '有効日
                    .Columns(COL_SEARCHLIST_NUMINFOKBN).DataField = "NumInfoKbn"                '番号通知
                    .Columns(COL_SEARCHLIST_SEALSENDKBN).DataField = "SealSendkbn"              'シール送付
                    .Columns(COL_SEARCHLIST_AVSCHECKKBN).DataField = "AntiVirusSofCheckKbn"     'ウィルス対策ソフト確認
                    .Columns(COL_SEARCHLIST_AVSCHECKDT).DataField = "AntiVirusSofCheckDT"       'ウィルス対策ソフトサーバ確認日
                    .Columns(COL_SEARCHLIST_USRBUSYONM).DataField = "UsrBusyoNM"                'ユーザ所属部署
                    .Columns(COL_SEARCHLIST_USRID).DataField = "UsrID"                          'ユーザID
                    .Columns(COL_SEARCHLIST_USRNM).DataField = "UsrNM"                          'ユーザ氏名
                    .Columns(COL_SEARCHLIST_MANAGEKYOKUNM).DataField = "ManageKyokuNM"          '管理局
                    .Columns(COL_SEARCHLIST_MANAGEBUSYONM).DataField = "ManageBusyoNM"          '管理部署
                    .Columns(COL_SEARCHLIST_SETBUSYONM).DataField = "SetBusyoNM"                '設置部署
                    .Columns(COL_SEARCHLIST_SORT).DataField = "Sort"                            'ソート
                    .Columns(COL_SEARCHLIST_CINMB).DataField = "CINmb"                          'CI番号

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
    ''' スプレッド用データテーブル作成処理
    ''' </summary>
    ''' <param name="dataHBKB1201">[IN/OUT]部所有機器検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドに出力するためのデータテーブルの作成を行う
    ''' <para>作成情報：2012/06/21 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateDataTable(ByRef dataHBKB1201 As DataHBKB1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtCIInfo As New DataTable '部所有機器検索結果用データテーブル

        Try

            '部所有機器検索結果用データテーブル作成
            With dtCIInfo

                .Columns.Add("KindNM", Type.GetType("System.String"))                   '種別
                .Columns.Add("Num", Type.GetType("System.String"))                      '番号
                .Columns.Add("Aliau", Type.GetType("System.String"))                    'エイリアス
                .Columns.Add("Class2", Type.GetType("System.String"))                   'メーカー
                .Columns.Add("CINM", Type.GetType("System.String"))                     '機種
                .Columns.Add("CIStateNM", Type.GetType("System.String"))                'ステータス
                .Columns.Add("ExpirationDT", Type.GetType("System.String"))             '有効日
                .Columns.Add("NumInfoKbn", Type.GetType("System.String"))               '番号通知
                .Columns.Add("SealSendkbn", Type.GetType("System.String"))              'シール送付
                .Columns.Add("AntiVirusSofCheckKbn", Type.GetType("System.String"))     'ウィルス対策ソフト確認
                .Columns.Add("AntiVirusSofCheckDT", Type.GetType("System.String"))      'ウィルス対策ソフトサーバ確認日
                .Columns.Add("UsrBusyoNM", Type.GetType("System.String"))               'ユーザ所属部署
                .Columns.Add("UsrID", Type.GetType("System.String"))                    'ユーザID
                .Columns.Add("UsrNM", Type.GetType("System.String"))                    'ユーザ氏名
                .Columns.Add("ManageKyokuNM", Type.GetType("System.String"))            '管理局
                .Columns.Add("ManageBusyoNM", Type.GetType("System.String"))            '管理部署
                .Columns.Add("SetBusyoNM", Type.GetType("System.String"))               '設置部署
                .Columns.Add("Sort", Type.GetType("System.Double"))                     'ソート
                .Columns.Add("CINmb", Type.GetType("System.Int32"))                     'CI番号

                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'データクラスをデータテーブルにセット
            dataHBKB1201.PropDtCIInfo = dtCIInfo 'CI共通情報テーブル

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
            dtCIInfo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 検索結果取得処理
    ''' </summary>
    ''' <param name="dataHBKB1201">[IN/OUT]部所有機器検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>部所有機器検索一覧画面の初期表示に必要なデータを取得する
    ''' <para>作成情報：2012/06/20 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetSearchData(ByRef dataHBKB1201 As DataHBKB1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try
            'コネクションを開く
            Cn.Open()

            'CI共通情報取得（スプレッド用）
            If GetCIInfoTable(Adapter, Cn, dataHBKB1201) = False Then
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
    ''' スプレッド用CI共通情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]データアダプター</param>
    ''' <param name="Cn">[IN]コネクション</param>
    ''' <param name="dataHBKB1201">[IN/OUT]部所有機器検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>部所有機器検索一覧画面の初期表示に必要なデータを取得する
    ''' <para>作成情報：2012/06/20 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetCIInfoTable(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef dataHBKB1201 As DataHBKB1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSystemMtb As New DataTable

        Try

            'SQLの作成・設定
            If sqlHBKB1201.SetSelectCIInfoTableSql(Adapter, Cn, dataHBKB1201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB1201.PropDtCIInfo)

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

        Finally
            dtSystemMtb.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 件数表示処理
    ''' </summary>
    ''' <param name="dataHBKB1201">[IN/OUT]部所有機器検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果の件数を表示する
    ''' <para>作成情報：2012/06/25 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetResultCount(dataHBKB1201 As DataHBKB1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1201

                '件数をセット
                .PropLblItemCount.Text = .PropResultCount.Rows(0).Item(0) & "件"

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 + ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 検索条件初期化処理メイン
    ''' </summary>
    ''' <param name="dataHBKB1201">[IN/OUT]部所有機器検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索条件に入力された内容を初期状態に戻す
    ''' <para>作成情報：2012/06/22 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InitSearchControlMain(ByRef dataHBKB1201 As DataHBKB1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '検索条件初期化処理
        If InitSearchControl(dataHBKB1201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' デフォルトソート処理メイン
    ''' </summary>
    ''' <param name="dataHBKB1201">[IN/OUT]部所有機器検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>デフォルトソート処理メイン
    ''' <para>作成情報：2012/07/02 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SortDefaultMain(ByRef dataHBKB1201 As DataHBKB1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        If SortSearchData(dataHBKB1201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常終了
        Return True

    End Function

    ''' <summary>
    ''' 検索結果ソート処理
    ''' </summary>
    ''' <param name="dataHBKB1201">[IN/OUT]部所有機器検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果を検索時のソート順に並べ替える
    ''' <para>作成情報：2012/07/02 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function SortSearchData(ByRef dataHBKB1201 As DataHBKB1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '変数宣言
            Dim Si(0) As SortInfo 'ソート対象配列

            With dataHBKB1201.PropVwBusyoyuukikiList.Sheets(0)

                '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                ''ソート対象列をソートする順番で指定
                'Si(0) = New SortInfo(COL_SEARCHLIST_SORT, True) 'ソート列(隠し)の昇順にソートする

                'ソート対象列をソートする順番で指定
                Si(0) = New SortInfo(COL_SEARCHLIST_NUM, True) '番号の昇順にソートする
                '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                .SortRows(0, .RowCount, Si)

                '.SortRows(COL_SEARCHLIST_SORT, True, False)

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
    ''' 検索条結果期化処理
    ''' </summary>
    ''' <param name="dataHBKB1201">[IN/OUT]部所有機器検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果を初期化する
    ''' <para>作成情報：2012/09/04 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function ClearResultSpread(ByRef dataHBKB1201 As DataHBKB1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1201

                '件数の0件セット
                .PropLblItemCount.Text = "0件"

                'スプレッド検索結果行削除
                .PropVwBusyoyuukikiList.Sheets(0).RowCount = 0

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
    ''' ボタン活性・非活性切り替え処理
    ''' </summary>
    ''' <param name="dataHBKB1201">[IN/OUT]部所有機器検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索条件に入力された内容を初期状態に戻す
    ''' <para>作成情報：2012/09/04 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function ChangeEnabled(ByRef dataHBKB1201 As DataHBKB1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1201

                If .PropBlnEnabledFlg = True Then
                    'ボタンを活性状態にする
                    .PropBtnMakeExcel.Enabled = True
                Else
                    'ボタンを非活性状態にする
                    .PropBtnMakeExcel.Enabled = False
                End If

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

End Class
