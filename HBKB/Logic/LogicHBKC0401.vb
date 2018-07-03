Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.IO

''' <summary>
''' 会議記録登録画面ロジッククラス
''' </summary>
''' <remarks>会議記録登録画面のロジックを定義したクラス
''' <para>作成情報：2012/07/09 k.imayama
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKC0401

    'インスタンス生成
    Private sqlHBKC0401 As New SqlHBKC0401
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    '定数宣言
    '対象プロセス一覧列番号
    Public Const COL_PROCESS_KBNNM As Integer = 0           '区分略称
    Public Const COL_PROCESS_NO As Integer = 1              '番号
    Public Const COL_PROCESS_TITLE As Integer = 2           'タイトル
    Public Const COL_PROCESS_KEKKA As Integer = 3           '結果（非表示）
    Public Const COL_PROCESS_KBN As Integer = 4             '区分
    '出席者情報一覧列番号
    Public Const COL_ATTEND_GROUPNM As Integer = 0          '所属グループ
    Public Const COL_ATTEND_USERNM As Integer = 1           '氏名
    Public Const COL_ATTEND_GRPCD As Integer = 2            'グループCD（非表示）
    Public Const COL_ATTEND_USRID As Integer = 3            'ユーザーID（非表示）
    '関連ファイル一覧列番号
    Public Const COL_FILE_NAIYO As Integer = 0              '説明
    Public Const COL_FILE_NO As Integer = 1                 'ファイル管理番号（表示）
    Public Const COL_FILE_PATH As Integer = 2               'ファイルパス（非表示）
    '会議結果一覧列番号
    Public Const COL_RESULT_KBNNM As Integer = 0            '区分略称
    Public Const COL_RESULT_NO As Integer = 1               '番号
    Public Const COL_RESULT_TITLE As Integer = 2            'タイトル（非表示）
    Public Const COL_RESULT_KEKKA As Integer = 3            '結果
    Public Const COL_RESULT_KBN As Integer = 4              '区分

    ''' <summary>
    ''' 【共通】画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて画面の初期表示設定を行う
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormNewModeMain(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'スプレッド表示用データテーブル作成
        If CreateDataTableForVw(dataHBKC0401) = False Then
            Return False
        End If

        'フォームコントロール設定
        If InitFormControl(dataHBKC0401) = False Then
            Return False
        End If

        '初期表示用データ取得
        If GetInitData(dataHBKC0401) = False Then
            Return False
        End If

        '初期表示用データセット
        If SetInitDataToControl(dataHBKC0401) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】スプレッド用データテーブル作成処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドのデータソースとして設定するデータテーブルを作成する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateDataTableForVw(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtProcess As New DataTable      '対象プロセス一覧用データテーブル
        Dim dtAttend As New DataTable       '出席者一覧用データテーブル
        Dim dtFile As New DataTable         '関連ファイル一覧用データテーブル
        Dim dtResult As New DataTable       '会議結果一覧用データテーブル

        Dim cmb As New FarPoint.Win.Spread.CellType.ComboBoxCellType()
        cmb.Items = CMB_RESULTKBN_STR
        cmb.ItemData = CMB_RESULTKBN_VAL

        Try
            '対象プロセス一覧用テーブル作成
            With dtProcess
                .Columns.Add("ProcessKbnNM", Type.GetType("System.String"))     'プロセス区分略称
                .Columns.Add("ProcessNmb", Type.GetType("System.Int32"))        'プロセス番号
                .Columns.Add("Title", Type.GetType("System.String"))            'タイトル
                .Columns.Add("ResultKbn", Type.GetType("System.String"))        '結果区分
                .Columns.Add("ProcessKbn", Type.GetType("System.String"))       'プロセス区分
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            '出席者一覧用テーブル作成
            With dtAttend
                .Columns.Add("AttendGrpNM", Type.GetType("System.String"))      '所属グループ
                .Columns.Add("AttendNM", Type.GetType("System.String"))         '氏名
                .Columns.Add("AttendGrpCD", Type.GetType("System.String"))      'グループCD
                .Columns.Add("AttendID", Type.GetType("System.String"))         'ユーザーID
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            '関連ファイル一覧用テーブル作成
            With dtFile
                .Columns.Add("FileNaiyo", Type.GetType("System.String"))        'ファイル説明
                .Columns.Add("FileMngNmb", Type.GetType("System.Int32"))        'ファイル管理番号
                .Columns.Add("FilePath", Type.GetType("System.String"))         'ファイルパス
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            '会議結果一覧用テーブル作成
            With dtResult
                .Columns.Add("ProcessKbnNM", Type.GetType("System.String"))     'プロセス区分略称
                .Columns.Add("ProcessNmb", Type.GetType("System.Int32"))        'プロセス番号
                .Columns.Add("Title", Type.GetType("System.String"))            'タイトル
                .Columns.Add("ResultKbn", Type.GetType("System.String"))        '結果区分
                .Columns.Add("ProcessKbn", Type.GetType("System.String"))       'プロセス区分
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'コンボボックス型セルの設定（結果区分）
            cmb.EditorValue = FarPoint.Win.Spread.CellType.EditorValue.ItemData
            dataHBKC0401.PropVwResultList.Sheets(0).Columns(COL_RESULT_KEKKA).CellType = cmb

            'データクラスに作成テーブルを格納
            With dataHBKC0401
                .PropDtProcess = dtProcess      'スプレッド表示用：対象プロセスデータ
                .PropDtAttend = dtAttend        'スプレッド表示用：出席者データ
                .PropDtFile = dtFile            'スプレッド表示用：関連ファイルデータ
                .PropDtResult = dtResult        'スプレッド表示用：会議結果データ
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
            dtProcess.Dispose()
            dtAttend.Dispose()
            dtFile.Dispose()
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】フォームコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InitFormControl(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'スプレッド設定
            If SetVwControl(dataHBKC0401) = False Then
                Return False
            End If

            '処理モードに応じたフォームコントロール設定
            If SetFormControlPerProcMode(dataHBKC0401) = False Then
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
    ''' 【共通】画面初期表示データ取得処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議記録登録画面の初期表示に必要なデータを取得する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try

            'コネクションを開く
            Cn.Open()

            'グループマスタデータ取得（コンボボックス用）
            If GetGroupMaster(Adapter, Cn, dataHBKC0401) = False Then
                Return False
            End If

            '主催者グループコンボボックス作成処理
            If InitGroupCmb(dataHBKC0401) = False Then
                Return False
            End If

            'メイン表示データ取得
            If GetMainData(Adapter, Cn, dataHBKC0401) = False Then
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
    ''' 【共通】初期データ設定処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームコントロールに初期表示データを設定する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitDataToControl(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'ヘッダデータ設定
            If SetDataToHeader(dataHBKC0401) = False Then
                Return False
            End If

            'フッタデータ設定
            If SetDataToFooter(dataHBKC0401) = False Then
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
    ''' グループマスタデータ取得（コンボボックス用）
    ''' </summary>
    ''' <param name="Adapter">[IN]データアダプター</param>
    ''' <param name="Cn">[IN]コネクション</param>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議記録登録画面の初期表示に必要なデータを取得する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetGroupMaster(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtGroup As New DataTable

        Try

            'SQLの作成・設定
            If sqlHBKC0401.SetSelectGroupMasterSql(Adapter, Cn, dataHBKC0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "グループマスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtGroup)

            '取得データをデータクラスにセット
            dataHBKC0401.PropDtGroup = dtGroup

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
            dtGroup.Dispose()
        End Try

    End Function

    ''' <summary>
    '''【検索用】主催者グループコンボボックス作成処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のコンボボックスを作成する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InitGroupCmb(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0401

                'グループコンボボックス作成
                If commonLogic.SetCmbBox(.PropDtGroup, .PropCmbHostGrpCD, True, "", "") = False Then
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
    ''' 【共通】初期表示用メインデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainData(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0401

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    If GetMainDataForNew(Adapter, Cn, dataHBKC0401) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード

                    '編集モード用データ取得
                    If GetMainDataForEdit(Adapter, Cn, dataHBKC0401) = False Then
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
    ''' 【新規登録モード】初期表示用メインデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集／参照モードで初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainDataForNew(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'インシデント共通情報データ取得
            If GetIncidentInfo(Adapter, Cn, dataHBKC0401) = False Then
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
    ''' 【新規登録モード】共通情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>共通情報データを取得する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetIncidentInfo(ByVal Adapter As NpgsqlDataAdapter, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'データ初期化
            dataHBKC0401.PropDtResult.Clear()
            dataHBKC0401.PropDtProcess.Clear()

            '取得用SQLの作成・設定
            If sqlHBKC0401.SetSelectIncidentInfoSql(Adapter, Cn, dataHBKC0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "共通情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKC0401.PropDtResult)
            Adapter.Fill(dataHBKC0401.PropDtProcess)

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
    ''' 【編集モード】初期表示用メインデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集／参照モードで初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainDataForEdit(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '会議情報データ取得
            If GetMeeting(Adapter, Cn, dataHBKC0401) = False Then
                Return False
            End If

            '会議結果情報データ取得
            If GetResult(Adapter, Cn, dataHBKC0401) = False Then
                Return False
            End If

            '会議出席者情報データ取得
            If GetAttened(Adapter, Cn, dataHBKC0401) = False Then
                Return False
            End If

            '会議関連ファイル会議結果データ取得
            If GetFile(Adapter, Cn, dataHBKC0401) = False Then
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
    ''' 【編集モード】会議情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報データを取得する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMeeting(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtMeeting As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKC0401.SetSelectMeetingSql(Adapter, Cn, dataHBKC0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtMeeting)

            'データが取得できなかった場合、エラー
            If dtMeeting.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & C0401_E001, TBNM_MEETING_TB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKC0401.PropDtMeeting = dtMeeting

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
            dtMeeting.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】会議結果データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議記録データを取得する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetResult(ByVal Adapter As NpgsqlDataAdapter, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'データ初期化
            dataHBKC0401.PropDtResult.Clear()
            dataHBKC0401.PropDtProcess.Clear()

            '取得用SQLの作成・設定
            If sqlHBKC0401.SetSelectResultSql(Adapter, Cn, dataHBKC0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議結果データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKC0401.PropDtResult)
            Adapter.Fill(dataHBKC0401.PropDtProcess)

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
    ''' 【編集モード】会議出席者情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議出席者情報データを取得する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetAttened(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'データ初期化
            dataHBKC0401.PropDtAttend.Clear()

            '取得用SQLの作成・設定
            If sqlHBKC0401.SetSelectAttendSql(Adapter, Cn, dataHBKC0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議出席者情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKC0401.PropDtAttend)

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
    ''' 【編集モード】会議関連ファイルデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議関連ファイルデータを取得する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetFile(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'データ初期化
            dataHBKC0401.PropDtFile.Clear()

            '取得用SQLの作成・設定
            If sqlHBKC0401.SetSelectFileSql(Adapter, Cn, dataHBKC0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議関連ファイルデータ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKC0401.PropDtFile)

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
    ''' 【共通】ヘッダデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録ム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ヘッダデータを初期設定する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToHeader(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0401

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToHeaderForNew(dataHBKC0401) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード

                    '編集モード用設定
                    If SetDataToHeaderForEdit(dataHBKC0401) = False Then
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
    ''' 【新規登録モード】ヘッダデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでヘッダデータを初期設定する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToHeaderForNew(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0401

                .PropTxtMeetingNmb.Text = ""                '会議番号
                .PropLblRegInfo.Text = ""                   '登録情報
                .PropLblUpInfo.Text = ""                    '最終更新情報
                .PropDtpYoteiSTDT.txtDate.Text = ""         '実施予定開始日付
                .PropTxtYoteiSTTM.PropTxtTime.Text = ""     '実施予定開始時刻
                .PropDtpYoteiENDDT.txtDate.Text = ""        '実施予定終了日付
                .PropTxtYoteiENDTM.PropTxtTime.Text = ""    '実施予定終了時刻
                .PropDtpJisiSTDT.txtDate.Text = ""          '実施開始日付
                .PropTxtJisiSTTM.PropTxtTime.Text = ""      '実施開始時刻
                .PropDtpJisiENDDT.txtDate.Text = ""         '実施終了日付
                .PropTxtJisiENDTM.PropTxtTime.Text = ""     '実施終了時刻
                .PropTxtTitle.Text = ""                     'タイトル
                .PropCmbHostGrpCD.SelectedValue = ""        '主催者グループCD
                .PropTxtHostID.Text = ""                    '主催者ID
                .PropTxtHostNM.Text = ""                    '主催者氏名
                .PropTxtProceedings.Text = ""               '議事録

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
    ''' 【編集モード】ヘッダデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでヘッダデータを初期設定する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToHeaderForEdit(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0401

                '該当データあり
                If .PropDtMeeting.Rows.Count > 0 Then

                    .PropTxtMeetingNmb.Text = .PropDtMeeting.Rows(0).Item("MeetingNmb")             '会議番号
                    .PropLblRegInfo.Text = .PropDtMeeting.Rows(0).Item("RegGrpNM") & " " & _
                                            .PropDtMeeting.Rows(0).Item("RegUsrNM") & " " & _
                                            .PropDtMeeting.Rows(0).Item("RegDT")                    '登録情報
                    .PropLblUpInfo.Text = .PropDtMeeting.Rows(0).Item("UpGrpNM") & " " & _
                                            .PropDtMeeting.Rows(0).Item("UpUsrNM") & " " & _
                                            .PropDtMeeting.Rows(0).Item("UpDT")                     '最終更新情報
                    .PropDtpYoteiSTDT.txtDate.Text = .PropDtMeeting.Rows(0).Item("YoteiSTDT")       '実施予定日時（FROM）日付
                    .PropTxtYoteiSTTM.PropTxtTime.Text = .PropDtMeeting.Rows(0).Item("YoteiSTTM")   '実施予定日時（FROM）時刻
                    .PropDtpYoteiENDDT.txtDate.Text = .PropDtMeeting.Rows(0).Item("YoteiENDDT")     '実施予定日時（TO）日付
                    .PropTxtYoteiENDTM.PropTxtTime.Text = .PropDtMeeting.Rows(0).Item("YoteiENDTM") '実施予定日時（TO）時刻
                    .PropDtpJisiSTDT.txtDate.Text = .PropDtMeeting.Rows(0).Item("JisiSTDT")         '実施日時（FROM）日付
                    .PropTxtJisiSTTM.PropTxtTime.Text = .PropDtMeeting.Rows(0).Item("JisiSTTM")     '実施日時（FROM）時刻
                    .PropDtpJisiENDDT.txtDate.Text = .PropDtMeeting.Rows(0).Item("JisiENDDT")       '実施日時（TO）日付
                    .PropTxtJisiENDTM.PropTxtTime.Text = .PropDtMeeting.Rows(0).Item("JisiENDTM")   '実施日時（TO）時刻
                    .PropTxtTitle.Text = .PropDtMeeting.Rows(0).Item("Title")                       'タイトル
                    .PropCmbHostGrpCD.SelectedValue = .PropDtMeeting.Rows(0).Item("HostGrpCD")      '主催者グループ
                    .PropTxtHostID.Text = .PropDtMeeting.Rows(0).Item("HostID")                     '主催者ID
                    .PropTxtHostNM.Text = .PropDtMeeting.Rows(0).Item("HostNM")                     '主催者氏名
                    .PropTxtProceedings.Text = .PropDtMeeting.Rows(0).Item("Proceedings")           '議事録

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
    ''' 【共通】フッタデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フッタデータを初期設定する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToFooter(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0401

                '対象プロセス一覧
                .PropVwProcessList.Sheets(0).DataSource = .PropDtProcess
                '出席者情報一覧
                .PropVwAttendList.Sheets(0).DataSource = .PropDtAttend
                '会議結果一覧
                .PropVwResultList.Sheets(0).DataSource = .PropDtResult
                '会議関連ファイル一覧
                .PropVwFileList.Sheets(0).DataSource = .PropDtFile

                'データ設定後のコントロール設定処理
                If SetFooterControlWhenAfterDataSet(dataHBKC0401) = False Then
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
    ''' 【共通】スプレッド初期設定処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>各スプレッド（一覧）を初期設定する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetVwControl(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0401

                '対象プロセス一覧
                With .PropVwProcessList.Sheets(0)
                    .Columns(COL_PROCESS_KBNNM).DataField = "ProcessKbnNM"
                    .Columns(COL_PROCESS_NO).DataField = "ProcessNmb"
                    .Columns(COL_PROCESS_TITLE).DataField = "Title"
                    .Columns(COL_PROCESS_KEKKA).DataField = "ResultKbn"
                    .Columns(COL_PROCESS_KBN).DataField = "ProcessKbn"

                End With

                '出席者一覧
                With .PropVwAttendList.Sheets(0)
                    .Columns(COL_ATTEND_GROUPNM).DataField = "AttendGrpNM"
                    .Columns(COL_ATTEND_USERNM).DataField = "AttendNM"
                    .Columns(COL_ATTEND_GRPCD).DataField = "AttendGrpCD"
                    .Columns(COL_ATTEND_USRID).DataField = "AttendID"
                End With

                '関連ファイル一覧
                With .PropVwFileList.Sheets(0)
                    .Columns(COL_FILE_NAIYO).DataField = "FileNaiyo"
                    .Columns(COL_FILE_NO).DataField = "FileMngNmb"
                    .Columns(COL_FILE_PATH).DataField = "FilePath"
                End With

                '会議結果一覧
                With .PropVwResultList.Sheets(0)
                    .Columns(COL_RESULT_KBNNM).DataField = "ProcessKbnNM"
                    .Columns(COL_RESULT_NO).DataField = "ProcessNmb"
                    .Columns(COL_RESULT_TITLE).DataField = "Title"
                    .Columns(COL_RESULT_KEKKA).DataField = "ResultKbn"
                    .Columns(COL_RESULT_KBN).DataField = "ProcessKbn"
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
    ''' 【共通】処理モード毎のフォームコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフォームコントロールの設定を行う
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormControlPerProcMode(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'ヘッダ設定
            If SetHeaderControl(dataHBKC0401) = False Then
                Return False
            End If

            'フッタ設定
            If SetFooterControl(dataHBKC0401) = False Then
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
    ''' 【共通】ヘッダコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてヘッダコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetHeaderControl(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0401

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    'メニュー以外から遷移の場合
                    If .PropBlnTranFlg = SELECT_MODE_NOTMENU Then

                        '対象プロセス「＋」ボタン活性化
                        .PropBtnAddRow_Prs.Enabled = False
                        .PropBtnRemoveRow_Prs.Enabled = False
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
    ''' 【共通】フッタコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControl(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0401

                '遷移元に応じて処理を行う
                Select Case .PropBlnTranFlg

                    Case SELECT_MODE_MENU       'メニューからの遷移時

                        '処理モードに応じて処理を行う
                        Select Case .PropStrProcMode

                            Case PROCMODE_NEW       '新規登録モード

                                'メニューから新規モード時の設定処理
                                If SetFooterControlForMenuNew(dataHBKC0401) = False Then
                                    Return False
                                End If

                            Case PROCMODE_EDIT      '編集モード

                                'メニューから編集モード時の設定処理
                                If SetFooterControlForMenuEdit(dataHBKC0401) = False Then
                                    Return False
                                End If

                        End Select

                    Case SELECT_MODE_NOTMENU    'メニュー以外からの遷移時

                        '処理モードに応じて処理を行う
                        Select Case .PropStrProcMode

                            Case PROCMODE_NEW       '新規登録モード

                                'メニュー以外から新規モード時の設定処理
                                If SetFooterControlForNotMenuNew(dataHBKC0401) = False Then
                                    Return False
                                End If

                            Case PROCMODE_EDIT      '編集モード

                                'メニュー以外から編集モード時の設定処理
                                If SetFooterControlForNotMenuEdit(dataHBKC0401) = False Then
                                    Return False
                                End If

                        End Select


                End Select



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
    ''' 【メニューから新規】フッタコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>メニューから新規モードで遷移時のフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/13 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForMenuNew(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0401

                '対象プロセスボタン活性
                .PropBtnAddRow_Prs.Enabled = True       '＋
                .PropBtnRemoveRow_Prs.Enabled = True    '－

                '関連ファイルボタン非活性
                .PropBtnFileOpen.Enabled = False        '開く（開）
                .PropBtnFileDown.Enabled = False        '保存（ダ）

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
    ''' 【メニューから編集】フッタコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>メニューから編集モードで遷移時のフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/13 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForMenuEdit(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0401

                '対象プロセスボタン活性
                .PropBtnAddRow_Prs.Enabled = True      '＋
                .PropBtnRemoveRow_Prs.Enabled = True   '－

                '※関連ファイルボタン活性設定はデータ設定後行うため、ここでは何もしない

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
    ''' 【メニュー以外から新規】フッタコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>メニュー以外から新規モードで遷移時のフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/13 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForNotMenuNew(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0401

                '対象プロセスボタン非活性
                .PropBtnAddRow_Prs.Enabled = False      '＋
                .PropBtnRemoveRow_Prs.Enabled = False   '－

                '関連ファイルボタン非活性
                .PropBtnFileOpen.Enabled = False        '開く（開）
                .PropBtnFileDown.Enabled = False        '保存（ダ）

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
    ''' 【メニュー以外から編集】フッタコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>メニュー以外から編集モードで遷移時のフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/13 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForNotMenuEdit(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0401

                'メニューから編集と同じ
                If SetFooterControlForMenuEdit(dataHBKC0401) = False Then
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
    ''' 【編集モード】データ設定後フッタコントロール設定
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モード時の、データ設定後のフッタコントロールの設定を行う
    ''' <para>作成情報：2012/08/13 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlWhenAfterDataSet(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnEnabled As Boolean = False       'ボタン活性フラグ

        Try

            With dataHBKC0401

                '編集モード時
                If .PropStrProcMode = PROCMODE_EDIT Then

                    '一覧にデータが1件以上あれば制御対象ボタン活性
                    If .PropVwFileList.Sheets(0).RowCount > 0 Then
                        blnEnabled = True
                    End If

                    .PropBtnFileOpen.Enabled = blnEnabled   '[開]
                    .PropBtnFileDown.Enabled = blnEnabled   '[ダ]

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
    ''' 主催者IDテキストボックスEnter時の処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作成者IDEnter時の処理
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateIDEnterMain(ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try

            'コネクションを開く
            Cn.Open()

            'テーブル取得
            If GetEndUsrMasta(Adapter, Cn, dataHBKC0401) = False Then
                Return False
            End If

            '主催者ID設定
            If SetNewCrateData(dataHBKC0401) = False Then
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
    ''' 【共通】ひびきユーザーマスタ取得処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ファイル管理テーブルからファイルパスを取得し、ファイルをダウンロードする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetEndUsrMasta(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtHbkUser As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKC0401.GetHbnUsrMastaData(Adapter, Cn, dataHBKC0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ひびきユーザーマスタデータ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtHbkUser)

            '取得データをデータクラスにセット
            dataHBKC0401.PropDtResultSub = dtHbkUser

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
            dtHbkUser.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 主催者IDテキストボックスEnter時の処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ＩＤテキストボックスにエンドユーザーマスタから取得した値を入力する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : 2012/09/19 k.ueda</p>
    ''' </para></remarks>
    Public Function SetNewCrateData(ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        With dataHBKC0401

            '選択データがある場合のみ値をセットする
            If .PropDtResultSub IsNot Nothing AndAlso .PropDtResultSub.Rows.Count > 0 Then

                '選択されたひびきユーザー情報を主催者情報にセットする
                .PropTxtHostNM.Text = .PropDtResultSub.Rows(0).Item("HbkUsrNM")                  'ユーザー氏名
                '検索したユーザーのグループが1件の場合のみグループを設定する
                If .PropDtResultSub.Rows.Count = 1 Then
                    .PropCmbHostGrpCD.SelectedValue = .PropDtResultSub.Rows(0).Item("GroupCD")   '主催者グループ
                Else
                    .PropCmbHostGrpCD.SelectedValue = ""                                         '主催者グループ
                End If
            Else

                '取得データがない場合（ENTERキーにて検索した場合）クリア
                .PropTxtHostNM.Text = ""                                            'ユーザー氏名
                .PropCmbHostGrpCD.SelectedValue = ""                                '主催者グループ
            End If

        End With

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 対象プロセス情報プロセス追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKc0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対象プロセス情報一覧にサブ検索画面から取得したプロセスを設定する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetProcessToVwProcessMain(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'プロセスデータ設定処理
        If SetProcessToVwProcess(dataHBKC0401) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】対象プロセス情報ユーザー設定処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対象プロセス情報一覧にサブ検索で選択されたプロセスを設定する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetProcessToVwProcess(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intNewRowNo As Integer          '新規追加行番号
        Dim blnAddFlg As Boolean = True     '追加フラグ

        Try

            With dataHBKC0401

                'サブ検索画面で1件以上選択された場合に値を設定
                If .PropDtResultSub IsNot Nothing Then

                    '選択データ数分繰り返し、対象プロセス情報一覧に行を追加する
                    For i As Integer = 0 To .PropDtResultSub.Rows.Count - 1

                        '追加フラグ初期化
                        blnAddFlg = True

                        'プロセス既に設定済でない場合のみ追加
                        For j As Integer = 0 To .PropVwProcessList.Sheets(0).RowCount - 1

                            '既に設定済みの場合は追加フラグをOFFにして処理を抜ける
                            If .PropDtResultSub.Rows(i).Item("ProcessKbn") = .PropVwProcessList.Sheets(0).Cells(j, COL_PROCESS_KBN).Value AndAlso _
                               .PropDtResultSub.Rows(i).Item("MngNmb") = .PropVwProcessList.Sheets(0).Cells(j, COL_PROCESS_NO).Value Then
                                blnAddFlg = False
                                Exit For
                            End If

                        Next

                        '追加フラグがONの場合のみ追加処理を行う
                        If blnAddFlg = True Then

                            '追加行番号取得
                            intNewRowNo = .PropVwProcessList.Sheets(0).Rows.Count

                            '取得した区分を略名で表示
                            Dim setKbn As String = ""
                            Select Case .PropDtResultSub.Rows(i).Item("ProcessKbn")
                                Case PROCESS_TYPE_INCIDENT
                                    setKbn = PROCESS_TYPE_INCIDENT_NAME_R
                                Case PROCESS_TYPE_QUESTION
                                    setKbn = PROCESS_TYPE_QUESTION_NAME_R
                                Case PROCESS_TYPE_CHANGE
                                    setKbn = PROCESS_TYPE_CHANGE_NAME_R
                                Case PROCESS_TYPE_RELEASE
                                    setKbn = PROCESS_TYPE_RELEASE_NAME_R
                            End Select

                            '対象プロセス一覧に新規行追加
                            .PropVwProcessList.Sheets(0).Rows.Add(intNewRowNo, 1)
                            'サブ検索画面での選択値を設定
                            .PropVwProcessList.Sheets(0).Cells(intNewRowNo, COL_PROCESS_KBNNM).Value = setKbn                                       '区分略称
                            .PropVwProcessList.Sheets(0).Cells(intNewRowNo, COL_PROCESS_NO).Value = .PropDtResultSub.Rows(i).Item("MngNmb")         '番号
                            .PropVwProcessList.Sheets(0).Cells(intNewRowNo, COL_PROCESS_TITLE).Value = .PropDtResultSub.Rows(i).Item("Title")       'タイトル
                            .PropVwProcessList.Sheets(0).Cells(intNewRowNo, COL_PROCESS_KEKKA).Value = SELECT_RESULTKBN_NO                          '結果
                            .PropVwProcessList.Sheets(0).Cells(intNewRowNo, COL_PROCESS_KBN).Value = .PropDtResultSub.Rows(i).Item("ProcessKbn")    '区分                                     '区分略称

                            '会議結果一覧に新規行追加
                            .PropVwResultList.Sheets(0).Rows.Add(intNewRowNo, 1)
                            'サブ検索画面での選択値を設定
                            .PropVwResultList.Sheets(0).Cells(intNewRowNo, COL_RESULT_KBNNM).Value = setKbn                                     '区分略称
                            .PropVwResultList.Sheets(0).Cells(intNewRowNo, COL_RESULT_NO).Value = .PropDtResultSub.Rows(i).Item("MngNmb")       '番号
                            .PropVwResultList.Sheets(0).Cells(intNewRowNo, COL_RESULT_TITLE).Value = .PropDtResultSub.Rows(i).Item("Title")     'タイトル
                            .PropVwResultList.Sheets(0).Cells(intNewRowNo, COL_RESULT_KEKKA).Value = SELECT_RESULTKBN_NO                        '結果
                            .PropVwResultList.Sheets(0).Cells(intNewRowNo, COL_RESULT_KBN).Value = .PropDtResultSub.Rows(i).Item("ProcessKbn")  '区分                                     '区分略称

                        End If

                    Next

                    '最終追加行にフォーカスをセット
                    If commonLogicHBK.SetFocusOnVwRow(.PropVwProcessList, _
                                                      0, .PropVwProcessList.Sheets(0).RowCount, 0, _
                                                      1, .PropVwProcessList.Sheets(0).ColumnCount) = False Then
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
    ''' 対象プロセス情報行削除時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対象プロセス情報一覧の選択行を削除する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowProcessMain(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowProcess(dataHBKC0401) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】対象プロセス情報選択行削除処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対象プロセス情報の選択行を削除する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowProcess(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号

        Try

            With dataHBKC0401

                '選択開始行、終了行取得
                intSelectedRowFrom = .PropVwProcessList.Sheets(0).Models.Selection.AnchorRow
                intSelectedRowTo = .PropVwProcessList.Sheets(0).Models.Selection.LeadRow

                '一覧に行があり、かつ選択行番号が最大行数未満の場合削除処理を行う
                If .PropVwProcessList.Sheets(0).RowCount > 0 AndAlso intSelectedRowFrom < .PropVwProcessList.Sheets(0).RowCount AndAlso intSelectedRowTo < .PropVwProcessList.Sheets(0).RowCount Then

                    '開始行から終了行まで選択行を削除する（逆回し）
                    For i As Integer = intSelectedRowTo To intSelectedRowFrom Step -1
                        .PropVwProcessList.Sheets(0).Rows(i).Remove()
                        .PropVwResultList.Sheets(0).Rows(i).Remove()
                    Next

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
    ''' 会議出席者情報ユーザー追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKc0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議出席者情報一覧にサブ検索画面から取得したユーザーデータを設定する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetUserToVwAttendMain(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ユーザーデータ設定処理
        If SetUserToVwAttend(dataHBKC0401) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】会議出席者情報ユーザー設定処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議出席者情報一覧にサブ検索で選択されたユーザーを設定する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetUserToVwAttend(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intNewRowNo As Integer          '新規追加行番号
        Dim blnAddFlg As Boolean = True     '追加フラグ

        Try

            With dataHBKC0401

                'サブ検索画面で1件以上選択された場合に値を設定
                If .PropDtResultSub IsNot Nothing Then

                    '選択データ数分繰り返し、関係者情報一覧に行を追加する
                    For i As Integer = 0 To .PropDtResultSub.Rows.Count - 1

                        '追加フラグ初期化
                        blnAddFlg = True

                        'ユーザーが既に設定済でない場合のみ追加
                        For j As Integer = 0 To .PropVwAttendList.Sheets(0).RowCount - 1

                            '既に設定済みの場合は追加フラグをOFFにして処理を抜ける
                            If .PropDtResultSub.Rows(i).Item("グループID") = .PropVwAttendList.Sheets(0).Cells(j, COL_ATTEND_GRPCD).Value AndAlso _
                               .PropDtResultSub.Rows(i).Item("ユーザーID") = .PropVwAttendList.Sheets(0).Cells(j, COL_ATTEND_USRID).Value Then
                                blnAddFlg = False
                                Exit For
                            End If

                        Next

                        '追加フラグがONの場合のみ追加処理を行う
                        If blnAddFlg = True Then

                            '追加行番号取得
                            intNewRowNo = .PropVwAttendList.Sheets(0).Rows.Count

                            '新規行追加
                            .PropVwAttendList.Sheets(0).Rows.Add(intNewRowNo, 1)

                            'サブ検索画面での選択値を設定
                            .PropVwAttendList.Sheets(0).Cells(intNewRowNo, COL_ATTEND_GROUPNM).Value = .PropDtResultSub.Rows(i).Item("グループ名")  'グループ名
                            .PropVwAttendList.Sheets(0).Cells(intNewRowNo, COL_ATTEND_USERNM).Value = .PropDtResultSub.Rows(i).Item("ユーザー氏名") 'ユーザー名
                            .PropVwAttendList.Sheets(0).Cells(intNewRowNo, COL_ATTEND_GRPCD).Value = .PropDtResultSub.Rows(i).Item("グループID")    '出席者グループCD
                            .PropVwAttendList.Sheets(0).Cells(intNewRowNo, COL_ATTEND_USRID).Value = .PropDtResultSub.Rows(i).Item("ユーザーID")    '出席者ユーザーID

                        End If

                    Next

                    '最終追加行にフォーカスをセット
                    If commonLogicHBK.SetFocusOnVwRow(.PropVwAttendList, _
                                                      0, .PropVwAttendList.Sheets(0).RowCount, 0, _
                                                      1, .PropVwAttendList.Sheets(0).ColumnCount) = False Then
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
    ''' 会議出席者情報行削除時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議出席情報一覧の選択行を削除する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowAttendMain(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowAttend(dataHBKC0401) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】会議出席者情報選択行削除処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議出席者情報の選択行を削除する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowAttend(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号

        Try

            With dataHBKC0401.PropVwAttendList.Sheets(0)

                '選択開始行、終了行取得
                intSelectedRowFrom = .Models.Selection.AnchorRow
                intSelectedRowTo = .Models.Selection.LeadRow

                '一覧に行があり、かつ選択行番号が最大行数未満の場合削除処理を行う
                If .RowCount > 0 AndAlso intSelectedRowFrom < .RowCount AndAlso intSelectedRowTo < .RowCount Then

                    '開始行から終了行まで選択行を削除する（逆回し）
                    For i As Integer = intSelectedRowTo To intSelectedRowFrom Step -1
                        .Rows(i).Remove()
                    Next

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
    ''' 会議関連ファイル情報ユーザー追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKc0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議関連ファイル情報一覧にサブ検索画面から取得したファイルデータを設定する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetFileToVwFileMain(ByRef dataHBKC0401 As DataHBKC0401, _
                                        ByVal strPath As String, _
                                        ByVal strNaiyo As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '関連ファイル設定処理
        If SetFileToVwFile(dataHBKC0401, strPath, strNaiyo) = False Then
            Return False
        End If

        '一覧の行数に合わせてフッタボタン制御
        If SetFooterControlWhenAfterDataSet(dataHBKC0401) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 会議関連ファイル追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKc0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議関連ファイル一覧にサブ検索画面から取得した関連ファイルデータを設定する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetFileToVwFile(ByRef dataHBKC0401 As DataHBKC0401, _
                                        ByVal strPath As String, _
                                        ByVal strNaiyo As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intNewRowNo As Integer          '新規追加行番号
        Dim blnAddFlg As Boolean = True     '追加フラグ

        Try

            With dataHBKC0401

                'サブ検索画面で1件以上選択された場合に値を設定
                If strPath IsNot Nothing Then

                    '追加フラグ初期化
                    blnAddFlg = True

                    'ユーザーが既に設定済でない場合のみ追加
                    For j As Integer = 0 To .PropVwFileList.Sheets(0).RowCount - 1

                        '既に設定済みの場合は追加フラグをOFFにして処理を抜ける
                        If Path.GetFileName(strPath) = Path.GetFileName(.PropVwFileList.Sheets(0).Cells(j, COL_FILE_PATH).Value) Then
                            blnAddFlg = False
                            Exit For
                        End If

                    Next

                    '追加フラグがONの場合のみ追加処理を行う
                    If blnAddFlg = True Then

                        '追加行番号取得
                        intNewRowNo = .PropVwFileList.Sheets(0).Rows.Count
                        '新規行追加
                        .PropVwFileList.Sheets(0).Rows.Add(intNewRowNo, 1)

                        'サブ検索画面での選択値を設定
                        .PropVwFileList.Sheets(0).Cells(intNewRowNo, COL_FILE_NAIYO).Value = strNaiyo   'ファイル説明
                        .PropVwFileList.Sheets(0).Cells(intNewRowNo, COL_FILE_NO).Value = DBNull.Value  'ファイル管理番号
                        .PropVwFileList.Sheets(0).Cells(intNewRowNo, COL_FILE_PATH).Value = strPath     'ファイルパス

                    End If

                    '最終追加行にフォーカスをセット
                    If commonLogicHBK.SetFocusOnVwRow(.PropVwFileList, _
                                                      0, .PropVwFileList.Sheets(0).RowCount, 0, _
                                                      1, .PropVwFileList.Sheets(0).ColumnCount) = False Then
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
    ''' 会議関連ファイル情報行削除時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議関連ファイル情報一覧の選択行を削除する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowFileMain(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowFile(dataHBKC0401) = False Then
            Return False
        End If

        '一覧の行数に合わせてフッタボタン制御
        If SetFooterControlWhenAfterDataSet(dataHBKC0401) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】会議関連ファイル情報選択行削除処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議関連ファイル情報の選択行を削除する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowFile(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号

        Try

            With dataHBKC0401.PropVwFileList.Sheets(0)

                '選択開始行、終了行取得
                intSelectedRowFrom = .Models.Selection.AnchorRow
                intSelectedRowTo = .Models.Selection.LeadRow

                '一覧に行があり、かつ選択行番号が最大行数未満の場合削除処理を行う
                If .RowCount > 0 AndAlso intSelectedRowFrom < .RowCount AndAlso intSelectedRowTo < .RowCount Then

                    '開始行から終了行まで選択行を削除する（逆回し）
                    For i As Integer = intSelectedRowTo To intSelectedRowFrom Step -1
                        .Rows(i).Remove()
                    Next

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
    ''' 【共通】開くボタン押下時の処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ファイル管理テーブルからファイルパスを取得し、ファイルを開く
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function FileOpenMain(ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ファイルパス取得処理
        If GetOpenFilePath(dataHBKC0401) = False Then
            Return False
        End If

        'ファイル表示処理
        If FileLoad(dataHBKC0401) = False Then
            Return False
        End If

        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        Return True

    End Function

    ''' <summary>
    ''' 【共通】ダウンロードボタン押下時の処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ファイル管理テーブルからファイルパスを取得し、ファイルをダウンロードする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function FileDownLoadMain(ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ファイルパス取得処理
        If GetOpenFilePath(dataHBKC0401) = False Then
            Return False
        End If

        'ファイルダウンロード処理
        If FileDownLoad(dataHBKC0401) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' ファイルパス取得処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択中の会議ファイルパスを習得する
    ''' <para>作成情報：2012/08/13 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetOpenFilePath(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0401

                '選択行のファイルパスを取得し、データクラスにセット
                .PropStrSelectedFilePath = .PropVwFileList.Sheets(0).Cells(.PropIntSelectedRow, COL_FILE_PATH).Value

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
        End Try

    End Function

    ''' <summary>
    ''' ファイルを開く処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ファイル管理テーブルからファイルパスを取得し、ファイルを開く
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function FileLoad(ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strFilePath As String
        Dim intFileMngNmb As Integer

        Dim strCmd As String = ""                                   'コマンド文字列
        Dim strDriveName As String = ""                             '使用論理ドライブ名

        Try

            With dataHBKC0401

                '選択行のファイルパス、ファイル管理番号取得
                strFilePath = dataHBKC0401.PropStrSelectedFilePath
                intFileMngNmb = .PropVwFileList.Sheets(0).Cells(.PropIntSelectedRow, COL_FILE_NO).Value

                '一時フォルダパス設定
                Dim strOutputDir As String = Path.Combine(Application.StartupPath, OUTPUT_DIR_TEMP)
                'ダウンロードファイル名設定
                Dim strDLFileName As String = Path.GetFileNameWithoutExtension(strFilePath) & _
                                "_" & Now().ToString("yyyyMMddmmss") & Path.GetExtension(strFilePath)

                'ダウンロードファイルパス設定
                Dim strDLFilePath As String = Path.Combine(strOutputDir, strDLFileName)


                'アップロード状況に応じて処理分岐
                If intFileMngNmb > 0 Then

                    '既にアップロード済みのファイルの場合（ファイル管理番号が振られている場合）、ネットワークドライブより開く

                    'PCの論理ドライブ名をすべて取得する
                    Dim strDrives As String() = System.IO.Directory.GetLogicalDrives()
                    '利用可能な論理ドライブ名を取得する
                    For Each strDrive As String In DRIVES
                        If strDrives.Contains(strDrive) = False Then
                            strDriveName = strDrive.Substring(0, 2)
                            Exit For
                        End If
                    Next

                'NetUse設定
                    If commonLogicHBK.NetUseConect(strDriveName) = False Then
                        Return False
                    End If

                End If


                'ファイルをネットワークドライブより一時フォルダにコピー
                Directory.CreateDirectory(strOutputDir)
                Microsoft.VisualBasic.FileIO.FileSystem.CopyFile(Path.Combine(strDriveName, strFilePath), strDLFilePath)


                'ファイル存在チェック
                If System.IO.File.Exists(strDLFilePath) Then

                    Dim fas As System.IO.FileAttributes = System.IO.File.GetAttributes(strDLFilePath)
                    ' ファイル属性に読み取り専用を追加
                    fas = fas Or System.IO.FileAttributes.ReadOnly
                    ' ファイル属性を設定
                    System.IO.File.SetAttributes(strDLFilePath, fas)
                    'プロセススタート
                    System.Diagnostics.Process.Start(strDLFilePath)

                End If

            End With
            

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As System.IO.DirectoryNotFoundException
            'ディレクトリが見つからなかった場合
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & C0401_E006
            Return False
        Catch ex As System.IO.FileNotFoundException
            'ファイルが見つからなかった場合
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & C0401_E006
            Return False
        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '接続した論理ドライブの削除
            commonLogicHBK.NetUseConectDel(strDriveName)
        End Try

    End Function

    ''' <summary>
    '''ファイルダウンロード処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ファイル管理テーブルからファイルパスを取得し、ファイルをダウンロードする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function FileDownLoad(ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strFilePath As String
        Dim intFileMngNmb As Integer
        Dim sfd As New SaveFileDialog()

        Dim strCmd As String = ""                                   'コマンド文字列
        Dim strDriveName As String = ""                             '使用論理ドライブ名
        Dim strDLFilePath As String = ""                            'ダウンロードファイルパス

        Try
            With dataHBKC0401

                '選択行のファイルパスを取得
                strFilePath = dataHBKC0401.PropStrSelectedFilePath

                'ファイルダウンロード処理
                sfd.FileName = Path.GetFileName(strFilePath)
                sfd.InitialDirectory = ""
                sfd.Filter = "すべてのファイル(*.*)|*.*"
                sfd.FilterIndex = 1
                sfd.Title = "保存先を指定してください"


                '選択行のファイルパス、ファイル管理番号取得
                strFilePath = dataHBKC0401.PropStrSelectedFilePath
                intFileMngNmb = .PropVwFileList.Sheets(0).Cells(.PropIntSelectedRow, COL_FILE_NO).Value

                'アップロード状況に応じて処理分岐
                If intFileMngNmb > 0 Then

                    '既にアップロード済みのファイルの場合（ファイル管理番号が振られている場合）、ネットワークドライブより開く

                    'PCの論理ドライブ名をすべて取得する
                    Dim strDrives As String() = System.IO.Directory.GetLogicalDrives()
                    '利用可能な論理ドライブ名を取得する
                    For Each strDrive As String In DRIVES
                        If strDrives.Contains(strDrive) = False Then
                            strDriveName = strDrive.Substring(0, 2)
                            Exit For
                        End If
                    Next

                    'NetUse設定
                    If commonLogicHBK.NetUseConect(strDriveName) = False Then
                        Return False
                    End If

                End If

                'ダウンロードファイルパス取得
                strDLFilePath = Path.Combine(strDriveName, strFilePath)

                'ファイルの存在チェック
                If System.IO.File.Exists(strDLFilePath) = False Then
                    'ファイルのコピー
                    Microsoft.VisualBasic.FileIO.FileSystem.CopyFile(strDLFilePath, sfd.FileName, True)
                End If

                'ファイルダイアログ表示
                If sfd.ShowDialog() = DialogResult.OK Then
                    'ファイルのコピー
                    Microsoft.VisualBasic.FileIO.FileSystem.CopyFile(strDLFilePath, sfd.FileName, True)
                End If

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As System.IO.DirectoryNotFoundException
            'ディレクトリが見つからなかった場合
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & C0401_E006
            Return False
        Catch ex As System.IO.FileNotFoundException
            'ファイルが見つからなかった場合
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & C0401_E006
            Return False
        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '接続した論理ドライブの削除
            commonLogicHBK.NetUseConectDel(strDriveName)
        End Try

    End Function

    ''' <summary>
    ''' 【共通】コントロール入力チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録項目の入力チェックを行う
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckInputValueMain(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'コントロール入力チェック
        If CheckInputValue(dataHBKC0401) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録項目の入力チェックを行う
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckInputValue(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数定義
        Dim dtFrom As DateTime
        Dim dtTo As DateTime
        Dim dtFromTM As DateTime
        Dim dtToTM As DateTime

        Try
            With dataHBKC0401

                '実施予定日時（FROM）------------------------------------'

                '時刻が入っていて日付が入っていない場合
                If .PropDtpYoteiSTDT.txtDate.Text = "" Then
                    If Not (.PropTxtYoteiSTTM.PropTxtTime.TextLength = 0) Then
                        'エラーメッセージ設定
                        puErrMsg = String.Format(C0401_E002, COL_YOTEIDT, COL_DATE)

                        'フォーカス設定
                        .PropDtpYoteiSTDT.Focus()
                        .PropDtpYoteiSTDT.txtDate.SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End If
                '日付が入っていて時刻が入っていない場合
                If .PropTxtYoteiSTTM.PropTxtTime.Text = "" Then
                    If Not (.PropDtpYoteiSTDT.txtDate.TextLength = 0) Then
                        'エラーメッセージ設定
                        puErrMsg = String.Format(C0401_E002, COL_YOTEIDT, COL_TIME)

                        'フォーカス設定
                        .PropTxtYoteiSTTM.Focus()
                        .PropTxtYoteiSTTM.PropTxtTime.SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End If

                '実施予定日時（TO）  ------------------------------------'

                '時刻が入っていて日付が入っていない場合
                If .PropDtpYoteiENDDT.txtDate.Text = "" Then
                    If Not (.PropTxtYoteiENDTM.PropTxtTime.TextLength = 0) Then
                        'エラーメッセージ設定
                        puErrMsg = String.Format(C0401_E002, COL_YOTEIDT, COL_DATE)

                        'フォーカス設定
                        .PropDtpYoteiENDDT.Focus()
                        .PropDtpYoteiENDDT.txtDate.SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End If
                '日付が入っていて時刻が入っていない場合
                If .PropTxtYoteiENDTM.PropTxtTime.Text = "" Then
                    If Not (.PropDtpYoteiENDDT.txtDate.TextLength = 0) Then
                        'エラーメッセージ設定
                        puErrMsg = String.Format(C0401_E002, COL_YOTEIDT, COL_TIME)

                        'フォーカス設定
                        .PropTxtYoteiENDTM.Focus()
                        .PropTxtYoteiENDTM.PropTxtTime.SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End If

                '実施予定日時の範囲チェック -----------------------------'
                If .PropDtpYoteiSTDT.txtDate.Text <> "" And .PropDtpYoteiENDDT.txtDate.Text <> "" Then

                    dtFrom = DateTime.Parse(.PropDtpYoteiSTDT.txtDate.Text)
                    dtTo = DateTime.Parse(.PropDtpYoteiENDDT.txtDate.Text)
                    'FROM～TOの範囲が正しくない場合、エラー
                    If dtFrom > dtTo Then
                        'エラーメッセージ設定
                        puErrMsg = String.Format(C0401_E003, COL_YOTEIDT)

                        'フォーカス設定（FROM）
                        .PropDtpYoteiSTDT.txtDate.Focus()
                        .PropDtpYoteiSTDT.txtDate.SelectAll()
                        'エラーを返す
                        Return False
                    End If

                    dtFromTM = DateTime.Parse(.PropTxtYoteiSTTM.PropTxtTime.Text)
                    dtToTM = DateTime.Parse(.PropTxtYoteiENDTM.PropTxtTime.Text)
                    'FROM～TOの範囲が正しくない場合、エラー
                    If dtFromTM > dtToTM And dtFrom = dtTo Then
                        'エラーメッセージ設定
                        puErrMsg = String.Format(C0401_E003, COL_YOTEIDT)

                        'フォーカス設定（FROM）
                        .PropTxtYoteiSTTM.PropTxtTime.Focus()
                        .PropTxtYoteiSTTM.PropTxtTime.SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End If


                '実施日時（FROM）----------------------------------------'

                '時刻が入っていて日付が入っていない場合
                If .PropDtpJisiSTDT.txtDate.Text = "" Then
                    If Not (.PropTxtJisiSTTM.PropTxtTime.TextLength = 0) Then
                        'エラーメッセージ設定
                        puErrMsg = String.Format(C0401_E002, COL_JISIDT, COL_DATE)

                        'フォーカス設定
                        .PropDtpJisiSTDT.Focus()
                        .PropDtpJisiSTDT.txtDate.SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End If
                '日付が入っていて時刻が入っていない場合
                If .PropTxtJisiSTTM.PropTxtTime.Text = "" Then
                    If Not (.PropDtpJisiSTDT.txtDate.TextLength = 0) Then
                        'エラーメッセージ設定
                        puErrMsg = String.Format(C0401_E002, COL_JISIDT, COL_TIME)

                        'フォーカス設定
                        .PropTxtJisiSTTM.Focus()
                        .PropTxtJisiSTTM.PropTxtTime.SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End If

                '実施日時（TO）------------------------------------------'

                '時刻が入っていて日付が入っていない場合
                If .PropDtpJisiENDDT.txtDate.Text = "" Then
                    If Not (.PropTxtJisiENDTM.PropTxtTime.TextLength = 0) Then
                        'エラーメッセージ設定
                        puErrMsg = String.Format(C0401_E002, COL_JISIDT, COL_DATE)

                        'フォーカス設定
                        .PropDtpJisiENDDT.Focus()
                        .PropDtpJisiENDDT.txtDate.SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End If
                '日付が入っていて時刻が入っていない場合
                If .PropTxtJisiENDTM.PropTxtTime.Text = "" Then
                    If Not (.PropDtpJisiENDDT.txtDate.TextLength = 0) Then
                        'エラーメッセージ設定
                        puErrMsg = String.Format(C0401_E002, COL_JISIDT, COL_TIME)

                        'フォーカス設定
                        .PropTxtJisiENDTM.Focus()
                        .PropTxtJisiENDTM.PropTxtTime.SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End If

                '実施日時の範囲チェック　--------------------------------'
                If .PropDtpJisiSTDT.txtDate.Text <> "" And .PropDtpJisiENDDT.txtDate.Text <> "" Then

                    dtFrom = DateTime.Parse(.PropDtpJisiSTDT.txtDate.Text)
                    dtTo = DateTime.Parse(.PropDtpJisiENDDT.txtDate.Text)
                    'FROM～TOの範囲が正しくない場合、エラー
                    If dtFrom > dtTo Then
                        'エラーメッセージ設定
                        puErrMsg = String.Format(C0401_E003, COL_JISIDT)

                        'フォーカス設定（FROM）
                        .PropDtpYoteiSTDT.txtDate.Focus()
                        .PropDtpYoteiSTDT.txtDate.SelectAll()
                        'エラーを返す
                        Return False
                    End If

                    dtFromTM = DateTime.Parse(.PropTxtJisiSTTM.PropTxtTime.Text)
                    dtToTM = DateTime.Parse(.PropTxtJisiENDTM.PropTxtTime.Text)
                    'FROM～TOの範囲が正しくない場合、エラー
                    If dtFromTM > dtToTM And dtFrom = dtTo Then
                        'エラーメッセージ設定
                        puErrMsg = String.Format(C0401_E003, COL_JISIDT)

                        'フォーカス設定（FROM）
                        .PropTxtYoteiSTTM.PropTxtTime.Focus()
                        .PropTxtYoteiSTTM.PropTxtTime.SelectAll()
                        'エラーを返す
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
    ''' 【新規登録モード】データ登録メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をデータベースに反映する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RegistDataOnNewModeMain(ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '新規登録処理
        If InsertNewData(dataHBKC0401) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【新規登録モード】データ新規登録処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をDBに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertNewData(ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Tsx As NpgsqlTransaction = Nothing    'トランザクション

        Try

            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            '新規会議番号、システム日付取得
            If SelectNewMeetingNmbAndSysDate(Cn, dataHBKC0401) = False Then
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '会議情報新規登録
            If InsertMeeting(Cn, dataHBKC0401) = False Then
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '会議結果情報新規登録
            If InsertResultForNew(Cn, dataHBKC0401) = False Then
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '会議出席者情報新規登録
            If InsertAttendForNew(Cn, dataHBKC0401) = False Then
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '会議関連ファイル情報新規登録
            If InsertFile(Cn, dataHBKC0401) = False Then
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'ログ情報新規登録（共通）
            If InsertRireki(Cn, dataHBKC0401) = False Then
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'コミット
            Tsx.Commit()

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'ロールバック
            If Tsx IsNot Nothing Then
                Tsx.Rollback()
            End If
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            If Tsx IsNot Nothing Then
                Tsx.Dispose()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【新規登録モード】新規会議番号取得処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0401">[IN会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番した会議番号を取得（SELECT）する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectNewMeetingNmbAndSysDate(ByVal Cn As NpgsqlConnection, _
                                              ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtResult As New DataTable

        Try

            '新規会議番号取得（SELECT）用SQLを作成
            If sqlHBKC0401.SetSelectNewMeetingNmbAndSysDateSql(Adapter, Cn, dataHBKC0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規会議番号、システム日付取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            'データが取得できた場合、データクラスに取得データをセット
            If dtResult.Rows.Count > 0 Then
                dataHBKC0401.PropIntMeetingNmb = dtResult.Rows(0).Item("MeetingNmb")    '会議番号
                dataHBKC0401.PropDtmSysDate = dtResult.Rows(0).Item("SysDate")          'サーバー日付
            Else
                '取得できなかったときはエラー
                puErrMsg = C0401_E004
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
            dtResult.Dispose()
            Adapter.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【新規登録モード】会議情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容を会議情報テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertMeeting(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            '会議情報新規登録（INSERT）用SQLを作成
            If sqlHBKC0401.SetInsertMeetingSql(Cmd, Cn, dataHBKC0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議情報新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【新規登録モード】会議結果情報テーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議結果情報テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/09/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertResultForNew(ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0401

                '会議結果一覧の行数分繰り返し、登録処理を行う
                For i As Integer = 0 To .PropVwResultList.Sheets(0).RowCount - 1

                    '入力値取得
                    Dim strProcessKbn As String = commonLogicHBK.ChangeNothingToStr(.PropVwResultList.Sheets(0).Cells(i, COL_RESULT_KBN), "").Trim()
                    Dim strProcessNmb As Integer = commonLogicHBK.ChangeNothingToStr(.PropVwResultList.Sheets(0).Cells(i, COL_RESULT_NO), 0)
                    Dim strResultKbn As String = commonLogicHBK.ChangeNothingToStr(.PropVwResultList.Sheets(0).Cells(i, COL_RESULT_KEKKA), "").Trim()

                    '登録行作成
                    Dim row As DataRow = .PropDtResult.NewRow
                    row.Item("ProcessKbn") = strProcessKbn
                    row.Item("ProcessNmb") = strProcessNmb
                    row.Item("ResultKbn") = strResultKbn

                    '作成した行をデータクラスにセット
                    .PropRowReg = row

                    '会議出席者情報新規登録（INSERT）用SQL実行
                    If InsertResult(Cn, dataHBKC0401) = False Then
                        Return False
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
    ''' 【新規登録／編集モード】会議結果情報テーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議結果情報テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/09/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertResult(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            '会議出席者情報新規登録（INSERT）用SQLを作成
            If sqlHBKC0401.SetInsertResultSql(Cmd, Cn, dataHBKC0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議結果情報新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【新規登録モード】会議出席者情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容を会議出席者情報テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/09/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertAttendForNew(ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0401

                '出席者一覧の行数分繰り返し、登録処理を行う
                For i As Integer = 0 To .PropVwAttendList.Sheets(0).RowCount - 1

                    '入力値取得
                    Dim strGrpCD As String = commonLogicHBK.ChangeNothingToStr(.PropVwAttendList.Sheets(0).Cells(i, COL_ATTEND_GRPCD), "").Trim()
                    Dim strID As String = commonLogicHBK.ChangeNothingToStr(.PropVwAttendList.Sheets(0).Cells(i, COL_ATTEND_USRID), "").Trim()

                    '登録行作成
                    Dim row As DataRow = .PropDtAttend.NewRow
                    row.Item("AttendGrpCD") = strGrpCD
                    row.Item("AttendID") = strID

                    '作成した行をデータクラスにセット
                    .PropRowReg = row

                    '会議出席者情報新規登録（INSERT）用SQLを実行
                    If InsertAttend(Cn, dataHBKC0401) = False Then
                        Return False
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
    ''' 【新規登録／編集モード】会議出席者情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容を会議出席者情報テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/09/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertAttend(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            '会議出席者情報新規登録（INSERT）用SQLを作成
            If sqlHBKC0401.SetInsertAttendSql(Cmd, Cn, dataHBKC0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議出席者情報新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()


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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【新規登録／編集モード】会議関連ファイル情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容を会議関連ファイル情報テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertFile(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            With dataHBKC0401

                '対象プロセス一覧よりインシデント管理番号取得
                If .PropVwProcessList.Sheets(0).RowCount > 0 Then
                    .PropIncNmb = commonLogicHBK.ChangeNothingToStr(.PropVwProcessList.Sheets(0).Cells(0, COL_PROCESS_NO), "").Trim()
                End If

                '最新のファイル情報データテーブルを取得
                .PropDtFile = DirectCast(.PropVwFileList.Sheets(0).DataSource, DataTable)

                If .PropDtFile IsNot Nothing Then

                    '関連ファイルアップロード／登録
                    Dim aryStrNewDirPath As New ArrayList
                    If commonLogicHBK.UploadAndRegFile(Adapter, Cn, _
                                                    .PropIntMeetingNmb, _
                                                    .PropDtFile, _
                                                    .PropDtmSysDate, _
                                                    UPLOAD_FILE_MEETING, _
                                                    aryStrNewDirPath) = False Then
                        Return False
                    End If

                End If

            End With

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議関連ファイル情報新規登録", Nothing, Cmd)

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
            Cmd.Dispose()
            Adapter.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】ログ情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴情報を各ログテーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRireki(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter    'アダプタ
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            '新規ログNo取得
            If GetNewRirekiNo(Adapter, Cn, dataHBKC0401) = False Then
                Return False
            End If

            '会議情報ログテーブル登録
            If InserMeetingL(Cn, dataHBKC0401) = False Then
                Return False
            End If

            '会議結果情報ログテーブル登録
            If InsertResultL(Cn, dataHBKC0401) = False Then
                Return False
            End If

            '会議出席者情報ログテーブル登録
            If InsertAttendL(Cn, dataHBKC0401) = False Then
                Return False
            End If

            '会議関連ファイルログテーブル登録
            If InsertFileL(Cn, dataHBKC0401) = False Then
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
            Cmd.Dispose()
            Adapter.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】新規ログNo取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapter</param>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番したログNoを取得する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetNewRirekiNo(ByVal Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dLogNo As New DataTable         'ログNo格納用テーブル

        Try

            'SQLを作成
            If sqlHBKC0401.SetSelectNewRirekiNoSql(Adapter, Cn, dataHBKC0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規ログNo取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dLogNo)

            'データが取得できた場合、データクラスにログNoをセット
            If dLogNo.Rows.Count > 0 Then
                dataHBKC0401.PropIntLogNo = dLogNo.Rows(0).Item("LogNo")
            Else
                '取得できなかった場合はエラー
                puErrMsg = C0401_E005
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
            dLogNo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】会議情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InserMeetingL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKC0401.SetInsertMeetingLSql(Cmd, Cn, dataHBKC0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議情報ログ新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】会議結果情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議結果情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertResultL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKC0401.SetInsertResultLSql(Cmd, Cn, dataHBKC0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議結果情報ログ新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】会議出席者情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議出席者情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertAttendL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKC0401.SetInsertAttendLSql(Cmd, Cn, dataHBKC0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議出席者情報ログ新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】会議関連ファイル情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議関連ファイル情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertFileL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKC0401.SetInsertFileLSql(Cmd, Cn, dataHBKC0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議関連ファイル情報ログ新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】データ登録メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をデータベースに反映する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RegistDataOnEditModeMain(ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '更新処理
        If UpdateData(dataHBKC0401) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【編集モード】データ更新処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をDBに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateData(ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter()    'アダプタ
        Dim Tsx As NpgsqlTransaction = Nothing    'トランザクション

        Try

            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            'システム日付取得
            If SelectSysDate(Adapter, Cn, dataHBKC0401) = False Then
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '会議情報更新（UPDATE）
            If UpdateMeeting(Cn, dataHBKC0401) = False Then
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '会議結果情報更新（UPDATE、DELETE、INSERT）
            If UpdateResult(Cn, dataHBKC0401) = False Then
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '会議出席者情報更新（DELETE、INSERT）
            If UpdateAttend(Cn, dataHBKC0401) = False Then
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '会議関連ファイル情報新規登録（DELETE→INSERT）
            If InsertFile(Cn, dataHBKC0401) = False Then
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'ログ情報新規登録（共通）
            If InsertRireki(Cn, dataHBKC0401) = False Then
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'コミット
            Tsx.Commit()

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'ロールバック
            If Tsx IsNot Nothing Then
                Tsx.Rollback()
            End If
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            If Tsx IsNot Nothing Then
                Tsx.Dispose()
            End If
            Cn.Dispose()
            Adapter.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】サーバー日付取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>更新用のサーバー日付を取得する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectSysDate(ByRef Adapter As NpgsqlDataAdapter, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSysDate As New DataTable         '履歴番号格納用テーブル

        Try

            'SQLを作成
            If sqlHBKC0401.SetSelectSysDateSql(Adapter, Cn, dataHBKC0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サーバー日付取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtSysDate)

            'データが取得できた場合、データクラスに履歴番号をセット
            If dtSysDate.Rows.Count > 0 Then
                dataHBKC0401.PropDtmSysDate = dtSysDate.Rows(0).Item("SysDate")
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
            dtSysDate.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】会議情報更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容で会議情報テーブルを更新（UPDATE）する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateMeeting(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            '会議情報更新（UPDATE）用SQLを作成
            If sqlHBKC0401.SetUpdateMeetingSql(Cmd, Cn, dataHBKC0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議情報更新", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】会議結果情報更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容で会議出席者情報テーブルを更新（UPDATE、DELETE、INSERT）する
    ''' <para>作成情報：2012/09/11 t.fukuo
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Private Function UpdateResult(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As DataTable = Nothing

        Try
            With dataHBKC0401

                '一覧のデータソースをデータテーブルに変換
                dtResult = DirectCast(.PropVwResultList.Sheets(0).DataSource, DataTable)

                'データ件数分繰り返し
                For i As Integer = 0 To dtResult.Rows.Count - 1

                    'データクラスに対象行をセット
                    .PropRowReg = dtResult.Rows(i)

                    Select Case .PropRowReg.RowState

                        Case DataRowState.Added     '新規追加行の場合

                            '会議結果情報登録（INSERT）
                            If InsertResult(Cn, dataHBKC0401) = False Then
                                Return False
                            End If

                        Case DataRowState.Deleted   '削除行の場合

                            '会議結果情報物理削除（DELETE）
                            If DeleteResult(Cn, dataHBKC0401) = False Then
                                Return False
                            End If

                        Case DataRowState.Modified  '更新行の場合

                            '会議結果情報更新（UPDATE）
                            If UpdateResultKbn(Cn, dataHBKC0401) = False Then
                                Return False
                            End If

                    End Select

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
        Finally
            If dtResult IsNot Nothing Then
                dtResult.Dispose()
            End If
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】会議結果情報削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議結果情報テーブルを物理削除（DELETE）する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DeleteResult(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            '会議結果情報物理削除（DELETE）用SQLを作成
            If sqlHBKC0401.SetDeleteResultSql(Cmd, Cn, dataHBKC0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議結果情報物理削除", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】会議結果情報更新処理（UPDATE）
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議結果情報の結果区分を更新（UPDATE）する
    ''' <para>作成情報：2012/09/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateResultKbn(ByVal Cn As NpgsqlConnection, _
                                     ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            '会議結果情報更新（UPDATE）用SQLを作成
            If sqlHBKC0401.SetUpdateResultSql(Cmd, Cn, dataHBKC0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議結果情報更新", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】会議出席者情報更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容で会議出席者情報テーブルを更新（DELETE、INSERT）する
    ''' <para>作成情報：2012/09/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateAttend(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtAttend As DataTable = Nothing

        Try
            With dataHBKC0401

                '一覧のデータソースをデータテーブルに変換
                dtAttend = DirectCast(.PropVwAttendList.Sheets(0).DataSource, DataTable)

                'データ件数分繰り返し
                For i As Integer = 0 To dtAttend.Rows.Count - 1

                    'データクラスに対象行をセット
                    .PropRowReg = dtAttend.Rows(i)

                    Select Case .PropRowReg.RowState

                        Case DataRowState.Added     '新規追加行の場合

                            '会議出席者情報登録（INSERT）
                            If InsertAttend(Cn, dataHBKC0401) = False Then
                                Return False
                            End If

                        Case DataRowState.Deleted   '削除行の場合

                            '会議出席者情報物理削除（DELETE）
                            If DeleteAttend(Cn, dataHBKC0401) = False Then
                                Return False
                            End If

                    End Select

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
        Finally
            If dtAttend IsNot Nothing Then
                dtAttend.Dispose()
            End If
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】会議出席者情報削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0401">[IN]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議出席者情報テーブルを物理削除（DELETE）する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DeleteAttend(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            '会議出席者情報物理削除（DELETE）用SQLを作成
            If sqlHBKC0401.SetDeleteAttendSql(Cmd, Cn, dataHBKC0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議出席者情報物理削除", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】システムエラー事前対応処理
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システムエラー発生時に非活性にするコントロールリストを作成する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DoProcForErrorMain(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'トランザクション系コントロールリスト作成
        If CreateTsxCtlList(dataHBKC0401) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】トランザクション系コントロールリスト作成処理（システムエラー対応）
    ''' </summary>
    ''' <param name="dataHBKC0401">[IN/OUT]会議記録登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>トランザクション系コントロールのリストを作成する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateTsxCtlList(ByRef dataHBKC0401 As DataHBKC0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim aryCtlList As New ArrayList

        Try
            With dataHBKC0401

                'トランザクション系のコントロールをリストに追加
                aryCtlList.Add(.PropBtnReg)              '登録ボタン
                aryCtlList.Add(.PropBtnFileOpen)         '開くボタン
                aryCtlList.Add(.PropBtnFileDown)         'ダウンロードボタン

                'データクラスに作成リストをセット
                .PropAryTsxCtlList = aryCtlList

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

End Class
