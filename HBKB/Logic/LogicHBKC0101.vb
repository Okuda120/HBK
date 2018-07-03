Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.Drawing
Imports FarPoint.Win.Spread

''' <summary>
''' インシデント検索一覧画面ロジッククラス
''' </summary>
''' <remarks>インシデント検索一覧画面Logicクラス
''' <para>作成情報：2012/07/24 s.yamaguchi
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKC0101

    'インスタンス作成
    Private sqlHBKC0101 As New SqlHBKC0101
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    '*************************************************************************
    '定数宣言
    Public Const COL_SEARCHLIST_INCNMB As Integer = 0           '番号
    Public Const COL_SEARCHLIST_INCKINDNM As Integer = 1        'インシデント種別
    Public Const COL_SEARCHLIST_PROCESSSTATENM As Integer = 2   'プロセスステータス名称
    Public Const COL_SEARCHLIST_HASSEIDT As Integer = 3         '発生日時
    Public Const COL_SEARCHLIST_TITLE As Integer = 4            'タイトル
    Public Const COL_SEARCHLIST_NUM As Integer = 5              '対象システム
    Public Const COL_SEARCHLIST_GROUPNM As Integer = 6          '担当者業務グループ名称
    Public Const COL_SEARCHLIST_HBKUSRNM As Integer = 7         'インシデント担当者
    Public Const COL_SEARCHLIST_DOMAINNM As Integer = 8         'ドメイン
    Public Const COL_SEARCHLIST_PARTNERNM As Integer = 9        '相手氏名
    Public Const COL_SEARCHLIST_USRBUSYONM As Integer = 10      '相手部署
    Public Const COL_SEARCHLIST_WORKSCEDT As Integer = 11       '作業予定日時
    Public Const COL_SEARCHLIST_PROCESSSTATECD As Integer = 12  'プロセスステータスCD
    Public Const COL_SEARCHLIST_INCTANTOID As Integer = 13      'インシデント担当者ID
    Public Const COL_SEARCHLIST_TANTOGRPCD As Integer = 14      '担当者業務グループCD
    Public Const COL_SEARCHLIST_SORTDT As Integer = 15          'ソート日付
    Public Const COL_SEARCHLIST_SORTNO As Integer = 16          'デフォルトソート
    '*************************************************************************

    '各項目リストボックス
    Private Const LIST_COLMUN_ZERO As Integer = 0               'リストボックスの0列目

    '作業予定日比較用定数
    Private Const WORKSCEDT_PAST As Integer = -1                '作業予定日：過去
    Private Const WORKSCEDT_TODAY As Integer = 0                '作業予定日：今日
    Private Const WORKSCEDT_FUTURE As Integer = 1               '作業予定日：未来


    ''' <summary>
    ''' 画面初期表示設定処理メイン
    ''' </summary>
    ''' <param name="dataHBKC0101">[IN/OUT]部所有機器検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデント検索一覧画面の初期表示に必要なデータを取得する
    ''' <para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '初期データ取得処理
        If GetInitData(dataHBKC0101) = False Then
            Return False
        End If

        'コンボボックス作成処理
        If CreateCmbBox(dataHBKC0101) = False Then
            Return False
        End If

        'リストボックス作成処理
        If CreateLstBox(dataHBKC0101) = False Then
            Return False
        End If

        '検索条件フォームオブジェクト初期化処理
        If InitSearchForm(dataHBKC0101) = False Then
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
    ''' <param name="dataHBKC0101">[IN/OUT]インシデント検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデント検索一覧画面の初期表示に必要なデータを取得する
    ''' <para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try
            'コネクションを開く
            Cn.Open()

            '[ADD] 2012/10/24 s.yamaguchi START
            '受付手段マスター取得（コンボボックス用）
            If GetUketsukeWay(Adapter, Cn, dataHBKC0101) = False Then
                Return False
            End If
            '[ADD] 2012/10/24 s.yamaguchi END

            'インシデント種別マスター取得（コンボボックス用）
            If GetIncidentKind(Adapter, Cn, dataHBKC0101) = False Then
                Return False
            End If

            'ドメインマスター取得（コンボボックス用）
            If GetDomain(Adapter, Cn, dataHBKC0101) = False Then
                Return False
            End If

            'グループマスター取得（コンボボックス用）
            If GetGrp(Adapter, Cn, dataHBKC0101) = False Then
                Return False
            End If

            '種別マスター取得（コンボボックス用）
            If GetKind(Adapter, Cn, dataHBKC0101) = False Then
                Return False
            End If

            'プロセスステータスマスター取得（リストボックス用）
            If GetProcessState(Adapter, Cn, dataHBKC0101) = False Then
                Return False
            End If

            '対象システム取得（リストボックス用）
            If GetTargetSystem(Adapter, Cn, dataHBKC0101) = False Then
                Return False
            End If

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
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

    '[ADD] 2012/10/24 s.yamaguchi START
    ''' <summary>
    ''' コンボボックス用受付手段マスター取得
    ''' </summary>
    ''' <param name="Adapter">[IN]データアダプター</param>
    ''' <param name="Cn">[IN]コネクション</param>
    ''' <param name="dataHBKC0101">[IN/OUT]インシデント検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>受付手段コンボボックスの初期表示に必要なデータを取得する
    ''' <para>作成情報：2012/10/24 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetUketsukeWay(ByVal Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtUketsukeWay As New DataTable '受付手段

        Try

            'SQLの作成・設定
            If sqlHBKC0101.SetSelectUketsukeWaySql(Adapter, Cn, dataHBKC0101) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "受付手段マスター", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtUketsukeWay)

            '取得データをデータクラスにセット
            dataHBKC0101.PropDtUketsukeWay = dtUketsukeWay

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
            'リソースの解放
            dtUketsukeWay.Dispose()
        End Try

    End Function
    '[ADD] 2012/10/24 s.yamaguchi START

    ''' <summary>
    ''' コンボボックス用インシデント種別マスター取得
    ''' </summary>
    ''' <param name="Adapter">[IN]データアダプター</param>
    ''' <param name="Cn">[IN]コネクション</param>
    ''' <param name="dataHBKC0101">[IN/OUT]インシデント検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデント種別コンボボックスの初期表示に必要なデータを取得する
    ''' <para>作成情報：2012/07/25 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetIncidentKind(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtIncidentKind As New DataTable 'インシデント種別データ

        Try

            'SQLの作成・設定
            If sqlHBKC0101.SetSelectIncidentKindSql(Adapter, Cn, dataHBKC0101) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "インシデント種別マスター", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtIncidentKind)

            '取得データをデータクラスにセット
            dataHBKC0101.PropDtIncidentKind = dtIncidentKind

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
            'リソースの解放
            dtIncidentKind.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' コンボボックス用ドメインマスター取得
    ''' </summary>
    ''' <param name="Adapter">[IN]データアダプター</param>
    ''' <param name="Cn">[IN]コネクション</param>
    ''' <param name="dataHBKC0101">[IN/OUT]インシデント検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ドメインコンボボックスの初期表示に必要なデータを取得する
    ''' <para>作成情報：2012/07/25 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetDomain(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtDomain As New DataTable 'ドメインデータ

        Try

            'SQLの作成・設定
            If sqlHBKC0101.SetSelectDomainSql(Adapter, Cn, dataHBKC0101) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ドメインマスター", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtDomain)

            '取得データをデータクラスにセット
            dataHBKC0101.PropDtDomain = dtDomain

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
            'リソースの解放
            dtDomain.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' コンボボックス用グループマスター取得
    ''' </summary>
    ''' <param name="Adapter">[IN]データアダプター</param>
    ''' <param name="Cn">[IN]コネクション</param>
    ''' <param name="dataHBKC0101">[IN/OUT]インシデント検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>担当者グループコンボボックスの初期表示に必要なデータを取得する
    ''' <para>作成情報：2012/07/25 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetGrp(ByVal Adapter As NpgsqlDataAdapter, _
                            ByVal Cn As NpgsqlConnection, _
                            ByRef dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtGrp As New DataTable 'グループデータ

        Try

            'SQLの作成・設定
            If sqlHBKC0101.SetSelectGrpSql(Adapter, Cn, dataHBKC0101) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "グループマスター", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtGrp)

            '取得データをデータクラスにセット
            dataHBKC0101.PropDtGrp = dtGrp

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
            'リソースの解放
            dtGrp.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' コンボボックス用種別マスター取得
    ''' </summary>
    ''' <param name="Adapter">[IN]データアダプター</param>
    ''' <param name="Cn">[IN]コネクション</param>
    ''' <param name="dataHBKC0101">[IN/OUT]インシデント検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>機器種別コンボボックスの初期表示に必要なデータを取得する
    ''' <para>作成情報：2012/07/25 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetKind(ByVal Adapter As NpgsqlDataAdapter, _
                             ByVal Cn As NpgsqlConnection, _
                             ByRef dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtKind As New DataTable '種別データ

        Try

            'SQLの作成・設定
            If sqlHBKC0101.SetSelectKindSql(Adapter, Cn, dataHBKC0101) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "種別マスター", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtKind)

            '取得データをデータクラスにセット
            dataHBKC0101.PropDtKind = dtKind

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
            'リソースの解放
            dtKind.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' リストボックス用プロセスステータスマスター取得
    ''' </summary>
    ''' <param name="Adapter">[IN]データアダプター</param>
    ''' <param name="Cn">[IN]コネクション</param>
    ''' <param name="dataHBKC0101">[IN/OUT]インシデント検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ステータスリストボックスの初期表示に必要なデータを取得する
    ''' <para>作成情報：2012/07/25 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetProcessState(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtProcessState As New DataTable 'プロセスステータスデータ

        Try

            'SQLの作成・設定
            If sqlHBKC0101.SetSelectProcessStateSql(Adapter, Cn, dataHBKC0101) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "プロセスステータスマスター", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtProcessState)

            '取得データをデータクラスにセット
            dataHBKC0101.PropDtProcessState = dtProcessState

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
            'リソースの解放
            dtProcessState.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' リストボックス用対象システムデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]データアダプター</param>
    ''' <param name="Cn">[IN]コネクション</param>
    ''' <param name="dataHBKC0101">[IN/OUT]インシデント検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対象システムリストボックスの初期表示に必要なデータを取得する
    ''' <para>作成情報：2012/07/25 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetTargetSystem(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtTargetSystem As New DataTable '対象システムデータ

        Try

            'SQLの作成・設定
            If sqlHBKC0101.SetSelectTargetSystemSql(Adapter, Cn, dataHBKC0101) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対象システム", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtTargetSystem)

            '取得データをデータクラスにセット
            dataHBKC0101.PropDtTargetSystem = dtTargetSystem

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
            'リソースの解放
            dtTargetSystem.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' コンボボックス作成処理
    ''' </summary>
    ''' <param name="dataHBKC0101">[IN/OUT]インシデント検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のコンボボックスを作成する
    ''' <para>作成情報：2012/07/25 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateCmbBox(ByRef dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0101

                '[ADD] 2012/10/24 s.yamaguchi START
                '受付手段
                If commonLogic.SetCmbBox(.PropDtUketsukeWay, .PropCmbUketsukeWay, True, "", "") = False Then
                    Return False
                End If
                '[ADD] 2012/10/24 s.yamaguchi END

                'インシデント種別
                If commonLogic.SetCmbBox(.PropDtIncidentKind, .PropCmbIncidentKind, True, "", "") = False Then
                    Return False
                End If

                'ドメイン
                If commonLogic.SetCmbBox(.PropDtDomain, .PropCmbDomain, True, "", "") = False Then
                    Return False
                End If

                '担当者グループ
                If commonLogic.SetCmbBox(.PropDtGrp, .PropCmbTantoGrp, True, "", "") = False Then
                    Return False
                End If

                '機器種別
                If commonLogic.SetCmbBox(.PropDtKind, .PropCmbKikiKind, True, "", "") = False Then
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
    ''' <param name="dataHBKC0101">[IN/OUT]インシデント検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のリストボックスを作成する
    ''' <para>作成情報：2012/07/25 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateLstBox(ByRef dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0101

                'ステータス
                .PropLstStatus.ValueMember = "ProcessStateCD"
                .PropLstStatus.DisplayMember = "ProcessStateNM"
                .PropLstStatus.DataSource = dataHBKC0101.PropDtProcessState

                '対象システム
                .PropLstTargetSystem.ValueMember = "CINmb"
                .PropLstTargetSystem.DisplayMember = "SystemNM"
                .PropLstTargetSystem.DataSource = dataHBKC0101.PropDtTargetSystem

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

    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    '検索一連処理

    Public Function SearchIncidentMain(ByRef dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '閾値チェックパラメータ初期化
        dataHBKC0101.PropBlnIndicationFlg = False

        'Excel出力用パラメータ設定処理
        If SetExcelOutPutParameter(dataHBKC0101) = False Then
            Return False
        End If

        '件数取得処理
        If GetResultCount(dataHBKC0101) = False Then
            Return False
        End If

        '閾値チェック処理
        If CheckThresholdValue(dataHBKC0101) = False Then
            Return False
        End If

        '閾値が件数を超えた際の表示判定
        If dataHBKC0101.PropBlnIndicationFlg = True Then
            '[mod] 2012/09/06 y.ikushima Excel出力対応 START
            dataHBKC0101.PropBtnMakeExcel.Enabled = False
            '[mod] 2012/09/06 y.ikushima Excel出力対応 END
            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True
        Else
            '[mod] 2012/09/06 y.ikushima Excel出力対応 START
            dataHBKC0101.PropBtnMakeExcel.Enabled = True
            '[mod] 2012/09/06 y.ikushima Excel出力対応 END
        End If

        'スプレッド用データテーブル作成処理
        If CreateDataTable(dataHBKC0101) = False Then
            Return False
        End If

        '検索結果取得処理
        If GetSearchData(dataHBKC0101) = False Then
            Return False
        End If

        '検索結果表示処理設定
        If SetResultIndication(dataHBKC0101) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' Excel出力用パラメーター設定処理
    ''' </summary>
    ''' <param name="dataHBKC0101">[IN/OUT]インシデント検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>Excel出力用のパラメータをセットする
    ''' <para>作成情報：2012/07/26 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function SetExcelOutPutParameter(ByRef dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0101

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
                'インシデント番号
                If .PropTxtNum.Text.Trim <> "" Then
                    .PropBlnIncNumInputFlg = False                                      '入力判定結果:入力
                    '入力結果をセット(数値外文字が入力された場合0がセットされる)
                    Integer.TryParse(.PropTxtNum.Text, .PropIntNum)
                Else
                    .PropBlnIncNumInputFlg = True                                       '入力判定結果:未入力
                End If
                '[ADD] 2012/10/24 s.yamaguchi START
                .PropStrUketsukeWay = .PropCmbUketsukeWay.SelectedValue                 'インシデント基本情報：受付手段
                '[ADD] 2012/10/24 s.yamaguchi END
                .PropStrIncidentKind = .PropCmbIncidentKind.SelectedValue               'インシデント基本情報：インシデント種別
                .PropStrDomain = .PropCmbDomain.SelectedValue                           'インシデント基本情報：ドメイン
                .PropStrOutsideToolNum = .PropTxtOutsideToolNum.Text                    'インシデント基本情報：外部ツール番号
                'インシデント基本情報：ステータス
                .PropStrStatus = Nothing
                For i As Integer = 0 To .PropLstStatus.SelectedItems.Count - 1
                    If .PropStrStatus = "" Then
                        .PropStrStatus = "'" & .PropLstStatus.SelectedItems(i)(LIST_COLMUN_ZERO) & "'"
                    Else
                        .PropStrStatus = .PropStrStatus & ",'" & .PropLstStatus.SelectedItems(i)(LIST_COLMUN_ZERO) & "'"
                    End If
                Next
                'インシデント基本情報：対象システム
                .PropStrTargetSystem = Nothing
                For i As Integer = 0 To .PropLstTargetSystem.SelectedItems.Count - 1
                    If .PropStrTargetSystem = "" Then
                        .PropStrTargetSystem = "'" & .PropLstTargetSystem.SelectedItems(i)(LIST_COLMUN_ZERO) & "'"
                    Else
                        .PropStrTargetSystem = .PropStrTargetSystem & ",'" & .PropLstTargetSystem.SelectedItems(i)(LIST_COLMUN_ZERO) & "'"
                    End If
                Next
                .PropStrTitle = .PropTxtTitle.Text                                      'インシデント基本情報：タイトル
                .PropStrUkeNaiyo = .PropTxtUkeNaiyo.Text                                'インシデント基本情報：受付内容
                .PropStrTaioKekka = .PropTxtTaioKekka.Text                              'インシデント基本情報：対応結果
                .PropStrHasseiDTFrom = .PropDtpHasseiDTFrom.txtDate.Text                'インシデント基本情報：発生日(From)
                .PropStrHasseiDTTo = .PropDtpHasseiDTTo.txtDate.Text                    'インシデント基本情報：発生日(To)
                '*********************************************************************************************************************
                '日付と時刻はセットにする
                .PropStrUpdateDTFrom = .PropDtpUpdateDTFrom.txtDate.Text                'インシデント基本情報：最終更新日時(日付From)
                .PropStrExUpdateTimeFrom = .PropTxtExUpdateTimeFrom.PropTxtTime.Text    'インシデント基本情報：最終更新日時(時刻From)
                .PropStrUpdateDTFrom = (.PropDtpUpdateDTFrom.txtDate.Text & _
                                       " " & _
                                       .PropTxtExUpdateTimeFrom.PropTxtTime.Text).Trim        'インシデント基本情報：最終更新日時(日付To)
                .PropStrUpdateDTTo = (.PropDtpUpdateDTTo.txtDate.Text & _
                                     " " & _
                                     .PropTxtExUpdateTimeTo.PropTxtTime.Text).Trim             'インシデント基本情報：最終更新日時(日付To)
                '[Add]2014/11/19 e.okamura 問題要望114 Start
                .PropStrExUpdateTimeTo = .PropTxtExUpdateTimeTo.PropTxtTime.Text        'インシデント基本情報：最終更新日時(時刻To)
                '[Add]2014/11/19 e.okamura 問題要望114 End
                .PropStrFreeText = .PropTxtFreeText.Text                                'インシデント基本情報：フリーテキスト
                .PropStrFreeFlg1 = .PropCmbFreeFlg1.SelectedValue                       'インシデント基本情報：フリーフラグ1
                .PropStrFreeFlg2 = .PropCmbFreeFlg2.SelectedValue                       'インシデント基本情報：フリーフラグ2
                .PropStrFreeFlg3 = .PropCmbFreeFlg3.SelectedValue                       'インシデント基本情報：フリーフラグ3
                .PropStrFreeFlg4 = .PropCmbFreeFlg4.SelectedValue                       'インシデント基本情報：フリーフラグ4
                .PropStrFreeFlg5 = .PropCmbFreeFlg5.SelectedValue                       'インシデント基本情報：フリーフラグ5
                .PropStrPartnerID = .PropTxtPartnerID.Text                              '相手情報：相手ID
                .PropStrPartnerNM = .PropTxtPartnerNM.Text                              '相手情報：相手氏名
                .PropStrUsrBusyoNM = .PropTxtUsrBusyoNM.Text                            '相手情報：相手部署
                .PropStrEventID = .PropTxtEventID.Text                                  'イベント情報：イベントID
                .PropStrOPCEventID = .PropTxtOPCEventID.Text                            'イベント情報：OPCイベントID
                .PropStrSource = .PropTxtSource.Text                                    'イベント情報：ソース
                .PropStrEventClass = .PropTxtEventClass.Text                            'イベント情報：イベントクラス
                .PropBlnChokusetsu = .PropRdoChokusetsu.Checked                         '担当者情報情報：直接
                .PropBlnKanyo = .PropRdoKanyo.Checked                                   '担当者情報情報：間接
                .PropStrTantoGrp = .PropCmbTantoGrp.SelectedValue                       '担当者情報情報：担当者グループ
                .PropStrIncTantoID = .PropTxtIncTantoID.Text                            '担当者情報情報：担当者ID
                .PropStrIncTantoNM = .PropTxtIncTantoNM.Text                            '担当者情報情報：担当者氏名
                .PropStrWorkSceDTFrom = (.PropDtpWorkSceDTFrom.txtDate.Text & _
                                        " " & _
                                        .PropTxtExWorkSceTimeFrom.PropTxtTime.Text).Trim       '作業情報：作業予定日時(From)
                '[Add]2014/11/19 e.okamura 問題要望114 Start
                .PropStrExWorkSceTimeFrom = .PropTxtExWorkSceTimeFrom.PropTxtTime.Text  '作業情報：作業予定日時(時刻From)
                '[Add]2014/11/19 e.okamura 問題要望114 End
                .PropStrWorkSceDTTo = (.PropDtpWorkSceDTTo.txtDate.Text & _
                                      " " & _
                                      .PropTxtExWorkSceTimeTo.PropTxtTime.Text).Trim           '作業情報：作業予定日時(To)
                '[Add]2014/11/19 e.okamura 問題要望114 Start
                .PropStrExWorkSceTimeTo = .PropTxtExWorkSceTimeTo.PropTxtTime.Text      '作業情報：作業予定日時(時刻To)
                '[Add]2014/11/19 e.okamura 問題要望114 End
                .PropStrWorkNaiyo = .PropTxtWorkNaiyo.Text                              '作業情報：作業内容
                .PropStrKikiKind = .PropCmbKikiKind.SelectedValue                       '機器情報：機器種別
                If .PropTxtKikiNum.Text = "" Then
                    .PropStrKikiNum = .PropTxtKikiNum.Text                              '機器情報：番号
                Else
                    .PropStrKikiNum = (.PropTxtKikiNum.Text).PadLeft(5, "0")            '機器情報：番号
                End If
                .PropStrProccesLinkKind = .PropCmbProccesLinkKind.SelectedValue         'プロセスリンク情報：種別
                .PropStrProcessLinkNum = .PropTxtProcessLinkNum.Text                    'プロセスリンク情報：番号

                '関与チェックボックスによってフラグを分岐させる
                If .PropRdoChokusetsu.Checked = True Then
                    .PropStrTantoRdoCheck = C0102_RDO_CHOKUSETSU
                ElseIf .PropRdoKanyo.Checked = True Then
                    .PropStrTantoRdoCheck = C0102_RDO_KANYO
                End If

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
    ''' <para>作成情報：2012/08/03 y.ikushima
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
            If sqlHBKC0101.SetProccesLinkSql(Adapter, Cn, StrProccesLinkKind, StrProcessLinkNum) = False Then
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
    ''' 検索件数取得処理
    ''' </summary>
    ''' <param name="dataHBKC0101">[IN/OUT]インシデント検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果の件数を取得する
    ''' <para>作成情報：2012/07/26 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetResultCount(ByRef dataHBKC0101 As DataHBKC0101) As Boolean

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
            If sqlHBKC0101.SetResultCountSql(Adapter, Cn, dataHBKC0101) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "検索件数", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtResultCount)

            '取得データをデータクラスにセット
            dataHBKC0101.PropDtResultCount = dtResultCount

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
    ''' 閾値チェック処理
    ''' </summary>
    ''' <param name="dataHBKC0101">[IN/OUT]インシデント検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索件すの判定を行う
    ''' <para>作成情報：2012/07/26 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function CheckThresholdValue(ByRef dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0101

                '件数チェック
                If .PropDtResultCount.Rows(0).Item(0) = 0 Then
                    '[mod] 2012/09/06 y.ikushima Excel出力対応 START

                    'データソースを空に設定
                    If CreateDataTable(dataHBKC0101) = False Then
                        Return False
                    End If
                    'Spread描写
                    If SetVwData(dataHBKC0101) = False Then
                        Return False
                    End If
                    dataHBKC0101.PropBtnMakeExcel.Enabled = False
                    '件数の表示
                    .PropLblResultCounter.Text = "0件"
                    'puErrorを空白にする
                    puErrMsg = ""
                    Return False
                    '[mod] 2012/09/06 y.ikushima Excel出力対応 END
                End If

                '件数の判定
                If dataHBKC0101.PropDtResultCount.Rows(0).Item(0) > PropSearchMsgCount Then
                    '件数が閾値以上で、表示しない(NO)を選択した場合処理を抜ける
                    If MsgBox(String.Format(C0101_W001, PropSearchMsgCount), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
                        '出力しない場合表示判定フラグをTrueにセット
                        dataHBKC0101.PropBlnIndicationFlg = True

                        '[mod] 2012/09/06 y.ikushima Excel出力対応 START
                        'データソースを空に設定
                        If CreateDataTable(dataHBKC0101) = False Then
                            Return False
                        End If
                        'Spread描写
                        If SetVwData(dataHBKC0101) = False Then
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
    ''' <param name="dataHBKC0101">[IN/OUT]インシデント検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドに出力するためのデータテーブルの作成を行う
    ''' <para>作成情報：2012/07/26 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateDataTable(ByRef dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtIncidentInfo As New DataTable             'インシデント検索結果用データテーブル

        Try

            With dtIncidentInfo

                .Columns.Add("IncNmb", Type.GetType("System.Int32"))            'インシデント番号
                .Columns.Add("IncKindNM", Type.GetType("System.String"))        'インシデント種別
                .Columns.Add("ProcessStateNM", Type.GetType("System.String"))   'ステータス
                .Columns.Add("HasseiDT", Type.GetType("System.String"))         '発生日時
                .Columns.Add("Title", Type.GetType("System.String"))            'タイトル
                .Columns.Add("CINM", Type.GetType("System.String"))             '対象システム
                .Columns.Add("GroupNM", Type.GetType("System.String"))          '担当者業務グループ
                .Columns.Add("HBKUsrNM", Type.GetType("System.String"))         'インシデント担当者
                .Columns.Add("DomainNM", Type.GetType("System.String"))         'ドメイン
                .Columns.Add("PartnerNM", Type.GetType("System.String"))        '相手氏名
                .Columns.Add("UsrBusyoNM", Type.GetType("System.String"))       '相手部署
                .Columns.Add("WorkSceDT", Type.GetType("System.String"))        '作業予定日時
                .Columns.Add("ProcessStateCD", Type.GetType("System.String"))   'プロセスステータスCD
                .Columns.Add("IncTantoID", Type.GetType("System.String"))       'インシデント担当者ID
                .Columns.Add("TantoGrpCD", Type.GetType("System.String"))       '担当者業務グループCD
                .Columns.Add("SortDT", Type.GetType("System.String"))           '担当者業務グループCD

                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'データクラスをデータテーブルにセット
            dataHBKC0101.PropDtIncidentInfo = dtIncidentInfo                    'インシデント検索結果

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
            dtIncidentInfo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' インシデント検索結果取得処理
    ''' </summary>
    ''' <param name="dataHBKC0101">[IN/OUT]インシデント検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデント検索結果の取得を行う
    ''' <para>作成情報：2012/07/26 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetSearchData(ByRef dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try
            'コネクションを開く
            Cn.Open()

            'インシデント検索結果取得（コンボボックス用）
            If GetIncidentInfo(Adapter, Cn, dataHBKC0101) = False Then
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
    ''' スプレッド用インシデント検索結果取得
    ''' </summary>
    ''' <param name="Adapter">[IN]データアダプター</param>
    ''' <param name="Cn">[IN]コネクション</param>
    ''' <param name="dataHBKC0101">[IN/OUT]インシデント検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデント検索結果表示用スプレッドに必要なデータを取得する
    ''' <para>作成情報：2012/07/26 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetIncidentInfo(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtIncidentKind As New DataTable 'インシデント種別データ

        Try

            'データテーブルの初期化
            dataHBKC0101.PropDtIncidentInfo.Clear()

            'SQLの作成・設定
            If sqlHBKC0101.SetSelectIncidentInfoSql(Adapter, Cn, dataHBKC0101) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "インシデント検索結果", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKC0101.PropDtIncidentInfo)

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
            'リソースの解放
            dtIncidentKind.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 検索結果の表示処理
    ''' </summary>
    ''' <param name="dataHBKC0101">[IN/OUT]インシデント検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果の表示設定を行う
    ''' <para>作成情報：2012/07/26 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetResultIndication(ByRef dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'スプレッド出力データ設定処理
            If SetVwData(dataHBKC0101) = False Then
                Return False
            End If

            '件数判定
            With dataHBKC0101

                '件数の表示
                .PropLblResultCounter.Text = .PropDtResultCount.Rows(0).Item(0) & "件"

                ''件数チェック
                'If .PropDtResultCount.Rows(0).Item(0) = 0 Then
                '    puErrMsg = C0101_E001
                '    Return False
                'End If
            End With

            '検索結果の背景色設定
            If SetBGColor(dataHBKC0101) = False Then
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
    ''' <param name="dataHBKC0101">[IN/OUT]インシデント検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>テーブルから取得した内容をスプレッドに設定する
    ''' <para>作成情報：2012/07/26 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetVwData(ByRef dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0101

                '検索結果
                With .PropVwIncidentList.Sheets(0)

                    .Rows.Clear()
                    .DataSource = Nothing
                    .DataSource = dataHBKC0101.PropDtIncidentInfo
                    .Columns(COL_SEARCHLIST_INCNMB).DataField = "IncNmb"                    '番号
                    .Columns(COL_SEARCHLIST_INCKINDNM).DataField = "IncKindNM"              'インシデント種別
                    .Columns(COL_SEARCHLIST_PROCESSSTATENM).DataField = "ProcessStateNM"    'ステータス
                    .Columns(COL_SEARCHLIST_HASSEIDT).DataField = "HasseiDT"                '発生日時
                    .Columns(COL_SEARCHLIST_TITLE).DataField = "Title"                      'タイトル
                    .Columns(COL_SEARCHLIST_NUM).DataField = "CINM"                         '対象システム
                    .Columns(COL_SEARCHLIST_GROUPNM).DataField = "GroupNM"                  '担当者業務グループ
                    .Columns(COL_SEARCHLIST_HBKUSRNM).DataField = "HBKUsrNM"                'インシデント担当者
                    .Columns(COL_SEARCHLIST_DOMAINNM).DataField = "DomainNM"                'ドメイン
                    .Columns(COL_SEARCHLIST_PARTNERNM).DataField = "PartnerNM"              '相手氏名
                    .Columns(COL_SEARCHLIST_USRBUSYONM).DataField = "UsrBusyoNM"            '相手部署
                    .Columns(COL_SEARCHLIST_WORKSCEDT).DataField = "WorkSceDT"              '作業予定日時
                    .Columns(COL_SEARCHLIST_PROCESSSTATECD).DataField = "ProcessStateCD"    'プロセスステータスCD
                    .Columns(COL_SEARCHLIST_INCTANTOID).DataField = "IncTantoID"            'インシデント担当者ID
                    .Columns(COL_SEARCHLIST_TANTOGRPCD).DataField = "TantoGrpCD"            '担当者業務グループCD
                    .Columns(COL_SEARCHLIST_SORTDT).DataField = "SortDT"                    'ソート日時
                    .Columns(COL_SEARCHLIST_SORTNO).DataField = "SortNo"                    'ソート番号
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
    ''' <param name="dataHBKC0101">[IN/OUT]インシデント検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドのセルの値を判定して背景色を変更する
    ''' <para>作成情報：2012/07/26 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetBGColor(ByRef dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnPStateMikakuninFlg As Boolean = False 'ステータス判定用フラグ

        Try

            With dataHBKC0101.PropVwIncidentList.Sheets(0)

                '表示件数分ループ
                For i = 0 To .RowCount - 1

                    'ステータス
                    Select Case .Cells(i, COL_SEARCHLIST_PROCESSSTATECD).Value
                        Case PROCESS_STATUS_INCIDENT_MIKAKUNIN
                            '背景色の設定：ピンク
                            .Rows(i).BackColor = Color.Pink
                            '未確認の場合、行全体の色設定なのでフラグを立てる
                            blnPStateMikakuninFlg = True
                        Case PROCESS_STATUS_INCIDENT_MUSI
                            '背景色の設定：グレー
                            .Cells(i, COL_SEARCHLIST_PROCESSSTATENM).BackColor = Color.Silver
                        Case PROCESS_STATUS_INCIDENT_KEIZOKU
                            '背景色の設定：黄色
                            .Cells(i, COL_SEARCHLIST_PROCESSSTATENM).BackColor = Color.Yellow
                        Case PROCESS_STATUS_INCIDENT_GAIBUIRAICHU
                            '背景色の設定：黄緑
                            .Cells(i, COL_SEARCHLIST_PROCESSSTATENM).BackColor = Color.GreenYellow
                        Case PROCESS_STATUS_INCIDENT_KANRYOU
                            '背景色の設定：ライトブルー
                            .Cells(i, COL_SEARCHLIST_PROCESSSTATENM).BackColor = Color.Cyan
                        Case Else
                            .Cells(i, COL_SEARCHLIST_PROCESSSTATENM).BackColor = Color.White
                    End Select

                    'ステータス判定(ステータスが未確認の場合を次行に移る)
                    If blnPStateMikakuninFlg = False Then

                        '作業者業務グループ
                        If .Cells(i, COL_SEARCHLIST_INCTANTOID).Value = PropUserId Then
                            '背景色の設定：黄色
                            .Cells(i, COL_SEARCHLIST_GROUPNM).BackColor = Color.Yellow
                        ElseIf .Cells(i, COL_SEARCHLIST_TANTOGRPCD).Value = PropWorkGroupCD Then
                            '背景色の設定：オレンジ
                            .Cells(i, COL_SEARCHLIST_GROUPNM).BackColor = Color.Orange
                        Else
                            ''背景色の設定：黄緑
                            .Cells(i, COL_SEARCHLIST_GROUPNM).BackColor = Color.LawnGreen
                        End If


                        '作業予定日時
                        If .Cells(i, COL_SEARCHLIST_WORKSCEDT).Value <> Nothing Then

                            Select Case DateTime.Compare(FormatDateTime(.Cells(i, COL_SEARCHLIST_WORKSCEDT).Value, DateFormat.ShortDate), Now().Date)
                                Case WORKSCEDT_PAST
                                    '背景色の設定：ピンク
                                    .Cells(i, COL_SEARCHLIST_WORKSCEDT).BackColor = Color.Pink
                                Case WORKSCEDT_TODAY
                                    '背景色の設定：黄色
                                    .Cells(i, COL_SEARCHLIST_WORKSCEDT).BackColor = Color.Yellow
                                Case WORKSCEDT_FUTURE
                                    '背景色の設定：オレンジ
                                    .Cells(i, COL_SEARCHLIST_WORKSCEDT).BackColor = Color.Orange
                            End Select
                        Else
                            .Cells(i, COL_SEARCHLIST_WORKSCEDT).BackColor = Color.White
                        End If

                    End If

                    'フラグを元に戻す
                    blnPStateMikakuninFlg = False

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
    ''' <param name="dataHBKC0101">[IN/OUT]インシデント検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のリストボックスを作成する
    ''' <para>作成情報：2012/07/26 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ClearSearchFormMain(ByRef dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '検索条件フォームオブジェクト初期化処理
        If InitSearchForm(dataHBKC0101) = False Then
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
    ''' <param name="dataHBKC0101">[IN/OUT]インシデント索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>デフォルトソートメイン処理
    ''' <para>作成情報：2012/08/01 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SortDefaultMain(ByRef dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        If SortSearchData(dataHBKC0101) = False Then
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
    ''' <param name="dataHBKC0101">[IN/OUT]インシデント検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果を検索時のソート順に並べ替える
    ''' <para>作成情報：2012/08/01 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function SortSearchData(ByRef dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0101.PropVwIncidentList.Sheets(0)
                '【EDIT】 2012/08/15 r.hoshino START
           
                'ソート列(ソートNo)の昇順にソートする
                .SortRows(COL_SEARCHLIST_SORTNO, True, False)

                'ソートインジケーターの初期化
                For i = 0 To .Columns.Count - 1
                    .Columns(i).ResetSortIndicator()
                Next

                '【EDIT】 2012/08/15 r.hoshino END
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
    ''' <param name="dataHBKC0101">[IN/OUT]インシデント検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のリストボックスを作成する
    ''' <para>作成情報：2012/07/25 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InitSearchForm(ByRef dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0101

                '***********************************
                '検索条件：インシデント基本情報
                '***********************************
                '検索条件(フォームオブジェクト)
                .PropTxtNum.Text = ""                               'インシデント基本情報：番号
                '[ADD] 2012/10/24 s.yamaguchi START
                .PropCmbUketsukeWay.SelectedValue = ""              'インシデント基本情報：受付手段
                '[ADD] 2012/10/24 s.yamaguchi END
                .PropCmbIncidentKind.SelectedValue = ""             'インシデント基本情報：インシデント種別
                .PropCmbDomain.SelectedValue = ""                   'インシデント基本情報：ドメイン
                .PropTxtOutsideToolNum.Text = ""                    'インシデント基本情報：外部ツール番号

                'インシデント基本情報：ステータス
                For i As Integer = 0 To .PropLstStatus.Items.Count - 1
                    'デフォルト選択フラグが"0"以外を選択状態にする。
                    If .PropLstStatus.Items(i)("DefaultSelectFlg") = DEFAULTSELECT_FLG_OFF Then
                        '項目を未選択状態に設定
                        .PropLstStatus.SetSelected(i, False)
                    Else
                        '項目を選択状態に設定
                        .PropLstStatus.SetSelected(i, True)
                    End If
                Next
                '[Add] 2012/08/15 y.ikushima START
                .PropLstStatus.TopIndex = 0
                '[Add] 2012/08/15 y.ikushima END
                .PropLstTargetSystem.ClearSelected()                'インシデント基本情報：対象システム
                '[Add] 2012/08/15 y.ikushima START
                .PropLstTargetSystem.TopIndex = 0
                '[Add] 2012/08/15 y.ikushima END
                .PropTxtTitle.Text = ""                             'インシデント基本情報：タイトル
                .PropTxtUkeNaiyo.Text = ""                          'インシデント基本情報：受付内容
                .PropTxtTaioKekka.Text = ""                         'インシデント基本情報：対応結果
                .PropDtpHasseiDTFrom.txtDate.Text = ""              'インシデント基本情報：発生日(From)
                .PropDtpHasseiDTTo.txtDate.Text = ""                'インシデント基本情報：発生日(To)
                .PropDtpUpdateDTFrom.txtDate.Text = ""              'インシデント基本情報：最終更新日時(日付From)
                .PropTxtExUpdateTimeFrom.PropTxtTime.Text = ""      'インシデント基本情報：最終更新日時(時刻From)
                '.PropTxtExUpdateTimeFrom.PropTxtTime.Enabled = False
                .PropDtpUpdateDTTo.txtDate.Text = ""                'インシデント基本情報：最終更新日時(日付To)
                .PropTxtExUpdateTimeTo.PropTxtTime.Text = ""        'インシデント基本情報：最終更新日時(時刻To)
                '.PropTxtExUpdateTimeTo.PropTxtTime.Enabled = False
                .PropTxtFreeText.Text = ""                          'インシデント基本情報：フリーテキスト
                .PropCmbFreeFlg1.SelectedValue = ""                 'インシデント基本情報：フリーフラグ1
                .PropCmbFreeFlg2.SelectedValue = ""                 'インシデント基本情報：フリーフラグ2
                .PropCmbFreeFlg3.SelectedValue = ""                 'インシデント基本情報：フリーフラグ3
                .PropCmbFreeFlg4.SelectedValue = ""                 'インシデント基本情報：フリーフラグ4
                .PropCmbFreeFlg5.SelectedValue = ""                 'インシデント基本情報：フリーフラグ5

                '***********************************
                '検索条件：相手情報
                '***********************************
                .PropTxtPartnerID.Text = ""                         '相手情報：相手ID
                .PropTxtPartnerNM.Text = ""                         '相手情報：相手氏名
                .PropTxtUsrBusyoNM.Text = ""                        '相手情報：相手部署

                '***********************************
                '検索条件：イベント情報
                '***********************************
                .PropTxtEventID.Text = ""                           'イベント情報：イベントID
                .PropTxtOPCEventID.Text = ""                        'イベント情報：OPCイベントID
                .PropTxtSource.Text = ""                            'イベント情報：ソース
                .PropTxtEventClass.Text = ""                        'イベント情報：イベントクラス

                '***********************************
                '検索条件：担当者情報
                '***********************************
                .PropRdoChokusetsu.Checked = True                   '担当者情報情報：直接
                .PropRdoKanyo.Checked = False                       '担当者情報情報：間接
                .PropCmbTantoGrp.SelectedValue = PropWorkGroupCD    '担当者情報情報：担当者グループ
                .PropTxtIncTantoID.Text = ""                        '担当者情報情報：担当者ID
                .PropTxtIncTantoNM.Text = ""                        '担当者情報情報：担当者氏名

                '***********************************
                '検索条件：作業情報
                '***********************************
                .PropDtpWorkSceDTFrom.txtDate.Text = ""             '作業情報：作業予定日時(日付From)
                .PropTxtExWorkSceTimeFrom.PropTxtTime.Text = ""     '作業情報：作業予定日時(時刻From)
                '.PropTxtExWorkSceTimeFrom.PropTxtTime.Enabled = False
                .PropDtpWorkSceDTTo.txtDate.Text = ""               '作業情報：作業予定日時(日付To)
                .PropTxtExWorkSceTimeTo.PropTxtTime.Text = ""       '作業情報：作業予定日時(時刻To)
                '.PropTxtExWorkSceTimeTo.PropTxtTime.Enabled = False
                .PropTxtWorkNaiyo.Text = ""                         '作業情報：作業内容

                '***********************************
                '検索条件：機器情報
                '***********************************
                .PropCmbKikiKind.SelectedValue = ""                 '機器情報：機器種別
                .PropTxtKikiNum.Text = ""                           '機器情報：番号

                '***********************************
                '検索条件：プロセスリンク情報
                '***********************************
                .PropCmbProccesLinkKind.SelectedValue = ""          'プロセスリンク情報：種別
                .PropTxtProcessLinkNum.Text = ""                    'プロセスリンク情報：番号

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
    ''' 【共通】相手先マスタデータ取得メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0101">[IN/OUT]インシデント検索画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetPartnerDataMain(ByRef dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try

            'コネクションを開く
            Cn.Open()

            'マスタデータ取得
            If GetPartnerData(Adapter, Cn, dataHBKC0101) = False Then
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
    ''' 【共通】相手先マスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0101">[IN/OUT]インシデント検索画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>マスタデータを取得する
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetPartnerData(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try


            '取得用SQLの作成・設定
            If sqlHBKC0101.GetPartnerInfoData(Adapter, Cn, dataHBKC0101) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "相手先マスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            '取得データをデータクラスにセット
            dataHBKC0101.PropDtResultSub = dtmst


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


    ''' <summary>
    ''' 【共通】担当マスタデータ取得メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0101">[IN/OUT]インシデント検索画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetIncTantoDataMain(ByRef dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try

            'コネクションを開く
            Cn.Open()

            'マスタデータ取得
            If GetIncTantoData(Adapter, Cn, DataHBKC0101) = False Then
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
    ''' 【共通】担当マスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0101">[IN/OUT]インシデント検索画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>マスタデータを取得する
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetIncTantoData(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKC0101.GetIncTantoInfoData(Adapter, Cn, DataHBKC0101) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "担当マスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            '取得データをデータクラスにセット
            dataHBKC0101.PropDtResultSub = dtmst


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

    ''' <summary>
    ''' 【共通】コントロール入力チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKC0101">[IN/OUT]インシデント検索画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索項目の入力チェックを行う
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckInputValueMain(ByRef dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'コントロール入力チェック
        If CheckInputValue(DataHBKC0101) = False Then
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
    ''' <param name="dataHBKC0101">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録項目の入力チェックを行う
    ''' <para>作成情報：2012/07/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckInputValue(ByRef dataHBKC0101 As DataHBKC0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0101

                '[最終更新日時(FROM)]時間に入力があって日付が未入力
                With .PropDtpUpdateDTFrom
                    '未入力の場合、エラー
                    If .txtDate.Text.Trim() = "" AndAlso dataHBKC0101.PropTxtExUpdateTimeFrom.PropTxtTime.Text <> "" Then
                        'エラーメッセージ設定
                        puErrMsg = String.Format(C0101_E002, "最終更新日時(FROM)")
                        'フォーカス設定
                        .Focus()
                        'エラーを返す
                        Return False
                    End If
                End With

                '[最終更新日時(TO)]時間に入力があって日付が未入力
                With .PropDtpUpdateDTTo
                    '未入力の場合、エラー
                    If .txtDate.Text.Trim() = "" AndAlso dataHBKC0101.PropTxtExUpdateTimeTo.PropTxtTime.Text <> "" Then
                        'エラーメッセージ設定
                        puErrMsg = String.Format(C0101_E002, "最終更新日時(TO)")
                        'フォーカス設定
                        .Focus()
                        'エラーを返す
                        Return False
                    End If
                End With

                '[作業予定日時(FROM)]時間に入力があって日付が未入力
                With .PropDtpWorkSceDTFrom
                    '未入力の場合、エラー
                    If .txtDate.Text.Trim() = "" AndAlso dataHBKC0101.PropTxtExWorkSceTimeFrom.PropTxtTime.Text <> "" Then
                        'エラーメッセージ設定
                        puErrMsg = String.Format(C0101_E002, "作業予定日時(FROM)")
                        'フォーカス設定
                        .Focus()
                        'エラーを返す
                        Return False
                    End If
                End With

                '[作業予定日時(TO)]時間に入力があって日付が未入力
                With .PropDtpWorkSceDTTo
                    '未入力の場合、エラー
                    If .txtDate.Text.Trim() = "" AndAlso dataHBKC0101.PropTxtExWorkSceTimeTo.PropTxtTime.Text <> "" Then
                        'エラーメッセージ設定
                        puErrMsg = String.Format(C0101_E002, "作業予定日時(TO)")
                        'フォーカス設定
                        .Focus()
                        'エラーを返す
                        Return False
                    End If
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

    '[ADD] 2012/10/24 s.yamaguchi START
    ''' <summary>
    ''' コンボボックスリサイズメイン処理
    ''' </summary>
    ''' <param name="sender">[IN/OUT]コンボボックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>コンボボックスサイズ変換処理
    ''' <para>作成情報：2012/10/24 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ComboBoxResizeMain(ByRef sender As Object) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'コンボボックスサイズ変換処理
        If ComboBoxResize(sender) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function
    '[ADD] 2012/10/24 s.yamaguchi END

    '[ADD] 2012/10/24 s.yamaguchi START
    ''' <summary>
    ''' コンボボックスサイズ変換
    ''' </summary>
    ''' <param name="sender">[IN/OUT]コンボボックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>コンボボックスのサイズを変換する
    ''' <para>作成情報：2012/10/24 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ComboBoxResize(ByRef sender As Object) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '変数宣言
            Dim cmbtmp As ComboBox = DirectCast(sender, ComboBox)
            Dim dttmp As DataTable
            Dim bLineX As Single

            'コンボボックスにデータソースが設定されている場合はデータソースをデータテーブルに変換
            If cmbtmp.DataSource IsNot Nothing Then
                dttmp = DirectCast(cmbtmp.DataSource, DataTable)
            Else
                'データソース未設定時は処理を抜ける
                Exit Function
            End If

            'コンボボックスのサイズを計算する

            '最大バイト数を取得

            Dim maxLenB = Aggregate row As DataRow In dttmp.Rows Where IsDBNull(row.Item(1)) = False Select commonLogic.LenB(row.Item(1)) Into Max()

            '次の描画位置計算
            Dim g As Graphics = cmbtmp.CreateGraphics()
            Dim sf As SizeF = g.MeasureString(New String("0"c, maxLenB), cmbtmp.Font)
            bLineX += sf.Width

            '最終項目の場合、ドロップダウンリストのサイズを設定
            If dttmp.Rows.Count >= 2 Then

                cmbtmp.DropDownWidth = bLineX
            End If
            'メモリ解放
            g.Dispose()

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
    '[ADD] 2012/10/24 s.yamaguchi END

End Class
