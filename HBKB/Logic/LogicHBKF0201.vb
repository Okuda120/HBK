Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.IO
Imports System.Windows.Forms

''' <summary>
''' リリース登録画面ロジッククラス
''' </summary>
''' <remarks>リリース登録画面のロジックを定義したクラス
''' <para>作成情報：2012/08/31 s.tsuruta
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKF0201

    'インスタンス作成
    Private sqlHBKF0201 As New SqlHBKF0201
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    'Public定数宣言==============================================

    'リリース依頼受領システム
    Public Const COL_IRAI As Integer = 0
    Public Const COL_IRAI_REGDT As Integer = 1
    Public Const COL_IRAI_REGGRPCD As Integer = 2
    Public Const COL_IRAI_REGID As Integer = 3
    Public Const COL_IRAI_ENTRY As Integer = 4
    'リリース実施対象システム
    Public Const COL_JISSI As Integer = 0
    Public Const COL_JISSI_REGDT As Integer = 1
    Public Const COL_JISSI_REGGRPCD As Integer = 2
    Public Const COL_JISSI_REGID As Integer = 3
    Public Const COL_JISSI_ENTRY As Integer = 4
    '関連ファイル情報列番号
    Public Const COL_RELFILE_NAIYO As Integer = 0           '説明
    Public Const COL_RELFILE_REGDT As Integer = 1           '登録日時
    Public Const COL_RELFILE_MNGNMB As Integer = 2          '隠し：番号
    Public Const COL_RELFILE_PATH As Integer = 3            '隠し：ファイルパス
    Public Const COL_RELFILE_REGDT_ As Integer = 4          '隠し：登録日時
    Public Const COL_RELFILE_REGGRPCD As Integer = 5        '隠し：登録GP
    Public Const COL_RELFILE_REGID As Integer = 6           '隠し：登録ID
    '会議情報列番号
    Public Const COL_MEETING_NMB As Integer = 0             '番号
    Public Const COL_MEETING_JISISTDT As Integer = 1        '実施日
    Public Const COL_MEETING_RESULTKBN_NM As Integer = 2    '承認
    Public Const COL_MEETING_TITLE As Integer = 3           'タイトル
    Public Const COL_MEETING_RESULTKBN As Integer = 4       '承認コード
    Public Const COL_MEETING_REGDT As Integer = 5           '隠し：登録日時
    Public Const COL_MEETING_REGGRPCD As Integer = 6        '隠し：登録GP
    Public Const COL_MEETING_REGID As Integer = 7           '隠し：登録ID
    '対応関係者情報列番号
    Public Const COL_RELATION_KBN As Integer = 0            '区分
    Public Const COL_RELATION_ID As Integer = 1             'ID
    Public Const COL_RELATION_GROUPNM As Integer = 2        'グループ名
    Public Const COL_RELATION_USERNM As Integer = 3         'ユーザー名
    Public Const COL_RELATION_REGDT As Integer = 4          '隠し：登録日時
    Public Const COL_RELATION_REGGRPCD As Integer = 5       '隠し：登録GP
    Public Const COL_RELATION_REGID As Integer = 6          '隠し：登録ID
    'プロセスリンク情報列番号
    Public Const COL_PLINK_KBN_NMR As Integer = 0           '区分
    Public Const COL_PLINK_NO As Integer = 1                '番号
    Public Const COL_PLINK_KBN As Integer = 2               '隠し：区分CD
    Public Const COL_PLINK_REGDT As Integer = 3             '隠し：登録日時
    Public Const COL_PLINK_REGGRPCD As Integer = 4          '隠し：登録GP
    Public Const COL_PLINK_REGID As Integer = 5             '隠し：登録ID


    'Private定数宣言==============================================

    'タブページ
    Private Const TAB_KHN As Integer = 0                    '基本情報
    Private Const TAB_MEETING As Integer = 1                '会議情報
    Private Const TAB_FREE As Integer = 2                   'フリー入力情報

    Private Const OUTPUT_LOG_TITLE As String = "リリース"   'ログ出力用

    'MaxDrop
    Private MaxDrop_systemnmb As Integer = 21


    ''' <summary>
    ''' 【共通】システムエラー事前対応処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システムエラー発生時に非活性にするコントロールリストを作成する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DoProcForErrorMain(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'トランザクション系コントロールリスト作成
        If CreateTsxCtlList(DataHBKF0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【新規登録モード】画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて画面の初期表示設定を行う
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormNewModeMain(ByRef dataHBKF0201 As DataHBKF0201) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'スプレッド表示用データテーブル作成
        If CreateDataTableForVw(DataHBKF0201) = False Then
            Return False
        End If

        'フォームコントロール設定
        If InitFormControl(DataHBKF0201) = False Then
            Return False
        End If

        '初期表示用データ取得
        If GetInitData(DataHBKF0201) = False Then
            Return False
        End If

        '初期表示用データセット
        If SetInitDataToControl(DataHBKF0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True
    End Function

    ''' <summary>
    ''' 【初期表示】ロックメイン処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>データのロック状況をチェックし、状況に応じてロックまたは処理モードの切替を行う
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function LockMain(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロック設定
        If SetLockWhenLoad(dataHBKF0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' フォームロード時ロック設定処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>共通情報テーブルをロックする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLock(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnDoUnlock As Boolean = False                   'ロック解除実行フラグ

        Try

            With dataHBKF0201

                '共通情報ロックテーブルデータがある場合、ロック解除実行フラグON
                If .PropDtRelLock.Rows.Count > 0 Then
                    blnDoUnlock = True
                End If

                '共通情報ロック
                If LockInfo(.PropIntRelNmb, .PropDtRelLock, blnDoUnlock) = False Then
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
    ''' ロック処理
    ''' </summary>
    ''' <param name="intNmb">[IN]番号</param>
    ''' <param name="dtLock">[IN/OUT]共通情報ロックテーブルデータ格納テーブル</param>
    ''' <param name="blnDoUnlock">[IN]解除実行フラグ（True：解除してからロックする）※省略可</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>番号をキーに共通情報ロックテーブルのデータをINSERTする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function LockInfo(ByVal intNmb As Integer, _
                               ByRef dtLock As DataTable, _
                               Optional ByVal blnDoUnlock As Boolean = False) As Boolean

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

            'ロック解除実行フラグがONの場合、共通情報ロックテーブルデータを削除
            If blnDoUnlock = True Then
                If DeleteLock(Cn, intNmb) = False Then
                    'ロールバック
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    Return False
                End If
            End If

            '共通情報ロックテーブル登録
            If InsertLock(Cn, intNmb) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'データ格納用テーブル初期化
            dtLock = New DataTable

            '共通情報ロックテーブル取得
            If sqlHBKF0201.SelectLock(Adapter, Cn, intNmb) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, OUTPUT_LOG_TITLE & "共通情報ロックテーブル取得", Nothing, Adapter.SelectCommand)

            'SQLを実行し、結果を取得
            Adapter.Fill(dtLock)

            'コミット
            Tsx.Commit()

            '2行取得できた場合（ロック情報がある場合）、ロック情報にサーバー日付を不可
            If dtLock.Rows.Count > 1 Then
                'ロック情報にサーバー日付を設定
                dtLock.Rows(1).Item("SysTime") = dtLock.Rows(0).Item("SysTime")
                '1行目のデータを削除
                dtLock.Rows(0).Delete()
                '変更をコミット
                dtLock.AcceptChanges()
            End If

            'ログ出力
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
            If Tsx IsNot Nothing Then
                Tsx.Dispose()
            End If
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Adapter.Dispose()
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【プロセスリンク】対応関係者チェックメイン処理　[.PropintChkKankei = 0:参照不可,1:参照のみ関係者,2:編集できる関係者]
    ''' </summary>
    ''' <param name="intResult">[IN/OUT]関係者チェック情報</param>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <param name="strKbn">[IN]プロセス区分</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者をチェックし、処理モードの切替を行う。
    ''' <para>作成情報：2012/09/04 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function PlinkKankeiCheckMain(ByRef intResult As Integer, ByVal intNmb As Integer, strKbn As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ
        Try
            'コネクションを開く
            Cn.Open()

            'k-2ユーザーチェック処理
            If ChkKankeiU(Adapter, Cn, intNmb, strKbn, intResult) = False Then
                Return False
            End If

            '関係者なら次のチェックは不要
            If intResult <> KANKEI_CHECK_EDIT Then
                'k-3所属グループチェック処理
                If ChkKankeiSZK(Adapter, Cn, intNmb, strKbn, intResult) = False Then
                    Return False
                End If

                '関係者でないなら次のチェックは不要
                If intResult <> KANKEI_CHECK_NONE Then
                    'k-1グループチェック処理
                    If ChkKankeiG(Adapter, Cn, intNmb, strKbn, intResult) = False Then
                        Return False
                    End If
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
        Finally
            Adapter.Dispose()
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' k【共通】対応関係者チェックメイン処理　[.PropintChkKankei = 0:参照不可,1:参照のみ関係者,2:編集できる関係者]
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者をチェックし、処理モードの切替を行う。
    ''' <para>作成情報：2012/09/04 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function KankeiCheckMain(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ
        Try
            'コネクションを開く
            Cn.Open()

            With dataHBKF0201
                'k-2ユーザーチェック処理
                If ChkKankeiU(Adapter, Cn, .PropIntRelNmb, PROCESS_TYPE_RELEASE, .PropIntChkKankei) = False Then
                    Return False
                End If

                '関係者なら次のチェックは不要
                If .PropIntChkKankei <> KANKEI_CHECK_EDIT Then
                    'k-3所属グループチェック処理
                    If ChkKankeiSZK(Adapter, Cn, .PropIntRelNmb, PROCESS_TYPE_RELEASE, .PropIntChkKankei) = False Then
                        Return False
                    End If

                    '関係者でないなら次のチェックは不要
                    If .PropIntChkKankei <> KANKEI_CHECK_NONE Then
                        'k-1グループチェック処理
                        If ChkKankeiG(Adapter, Cn, .PropIntRelNmb, PROCESS_TYPE_RELEASE, .PropIntChkKankei) = False Then
                            Return False
                        End If
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
        Finally
            Adapter.Dispose()
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】対応関係者所属チェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="IntNmb">[IN]管理番号</param>
    ''' <param name="StrKbn">[IN]プロセス区分</param>
    ''' <param name="IntResult">[IN/OUT]結果戻り値</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>チェックデータを取得する
    ''' <para>作成情報：2012/08/28 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ChkKankeiSZK(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByVal IntNmb As Integer, _
                                  ByVal StrKbn As String, _
                                  ByRef IntResult As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try


            '取得用SQLの作成・設定
            If sqlHBKF0201.GetChkKankeiSZKData(Adapter, Cn, IntNmb, StrKbn) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対応関係者所属グループチェック", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            '判定結果をデータクラスにセット
            If dtmst.Rows.Count > 0 Then
                If CLng(dtmst.Rows(0).Item(0)) > 0 Then
                    IntResult = KANKEI_CHECK_REF
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
        Finally
            dtmst.Dispose()
        End Try

    End Function



    ''' <summary>
    ''' k-1.【共通】対応関係者グループチェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="IntNmb">[IN]管理番号</param>
    ''' <param name="StrKbn">[IN]プロセス区分</param>
    ''' <param name="IntResult">[IN/OUT]結果戻り値</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>チェックデータを取得する
    ''' <para>作成情報：2012/09/04 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ChkKankeiG(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByVal IntNmb As Integer, _
                                  ByVal StrKbn As String, _
                                  ByRef IntResult As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try


            '取得用SQLの作成・設定
            If sqlHBKF0201.GetChkKankeiGData(Adapter, Cn, IntNmb, StrKbn) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対応関係者グループチェック", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            '判定結果をデータクラスにセット
            If dtmst.Rows.Count > 0 Then
                If CLng(dtmst.Rows(0).Item(0)) > 0 Then
                    IntResult = KANKEI_CHECK_EDIT
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
        Finally
            dtmst.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' k-2.【共通】対応関係者ユーザーチェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="IntNmb">[IN]管理番号</param>
    ''' <param name="StrKbn">[IN]プロセス区分</param>
    ''' <param name="IntResult">[IN/OUT]結果戻り値</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>チェックデータを取得する
    ''' <para>作成情報：2012/09/04 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ChkKankeiU(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByVal IntNmb As Integer, _
                                  ByVal StrKbn As String, _
                                  ByRef IntResult As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKF0201.GetChkKankeiUData(Adapter, Cn, IntNmb, StrKbn) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対応関係者ユーザーチェック", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            '判定結果をデータクラスにセット
            If dtmst.Rows.Count > 0 Then
                If CLng(dtmst.Rows(0).Item(0)) > 0 Then
                    IntResult = KANKEI_CHECK_EDIT
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
        Finally
            dtmst.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【編集モード】画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで画面の初期表示設定を行う
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormEditModeMain(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'スプレッド表示用データテーブル作成
        If CreateDataTableForVw(dataHBKF0201) = False Then
            Return False
        End If

        'フォームコントロール設定
        If InitFormControl(dataHBKF0201) = False Then
            Return False
        End If

        '初期表示用データ取得
        If GetInitData(dataHBKF0201) = False Then
            Return False
        End If

        '初期表示用データセット
        If SetInitDataToControl(dataHBKF0201) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【参照モード】画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで画面の初期表示設定を行う
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormRefModeMain(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'スプレッド表示用データテーブル作成
        If CreateDataTableForVw(dataHBKF0201) = False Then
            Return False
        End If

        'フォームコントロール設定
        If InitFormControl(dataHBKF0201) = False Then
            Return False
        End If

        '初期表示用データ取得
        If GetInitData(dataHBKF0201) = False Then
            Return False
        End If

        '初期表示用データセット
        If SetInitDataToControl(dataHBKF0201) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 解除ボタンクリック時ロック解除メイン処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータをログインユーザでロックし、フォームのロックを解除する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function UnlockWhenClickBtnUnlockMain(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'モード変更
        dataHBKF0201.PropStrProcMode = PROCMODE_EDIT

        'ロックフラグOFF
        dataHBKF0201.PropBlnBeLockedFlg = False

        'ロック処理
        If SetLockWhenUnlock(dataHBKF0201) = False Then
            Return False
        End If

        'フォームコントロールのロックを解除する
        If SetFormControlPerProcMode(dataHBKF0201) = False Then
            Return False
        End If
        'ログイン/ロックデータ設定処理
        If SetDataToLoginAndLock(dataHBKF0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' ロック解除チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>他のユーザによりロックが解除されていないかチェックする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckBeUnlockedMain(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロック解除チェック
        If CheckUnlock(dataHBKF0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 画面クローズ時ロック解除メイン処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータのロックを解除する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function UnlockWhenCloseMain(ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロック解除処理
        If UnlockData(dataHBKF0201) = False Then
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
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>トランザクション系コントロールのリストを作成する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateTsxCtlList(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim aryCtlList As New ArrayList

        Try
            With dataHBKF0201

                'トランザクション系のコントロールをリストに追加
                aryCtlList.Add(.PropBtnReg)              '登録ボタン
                aryCtlList.Add(.PropGrpLoginUser)        'ログイン／ロックグループ

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

    ''' <summary>
    ''' 【共通】スプレッド用データテーブル作成処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドのデータソースとして設定するデータテーブルを作成する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateDataTableForVw(ByRef dataHBKF0201 As DataHBKF0201) As Boolean


        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtRelIrai As New DataTable       'リリース依頼受領システムデータテーブル
        Dim dtRelJissi As New DataTable      'リリース実施対象システムデータテーブル
        Dim dtRelFile As New DataTable       'リリース関連ファイル情報データテーブル
        Dim dtMeeting As New DataTable       '会議情報データテーブル
        Dim dtRelInfo As New DataTable       '対応関係情報データテーブル
        Dim dtPlinkInfo As New DataTable     'プロセスリンク情報データテーブル

        Try

            'リリース依頼受領システム
            With dtRelJissi
                .Columns.Add("CINmb", Type.GetType("System.String"))                '対象システム
                '[mod] 2012/09/07 y.ikushima 表示不具合対応START
                .Columns.Add("RegDt", Type.GetType("System.DateTime"))              '登録日時
                '.Columns.Add("RegDt", Type.GetType("System.String"))               '登録日時
                '[mod] 2012/09/07 y.ikushima 表示不具合対応END
                .Columns.Add("RegGrpCD", Type.GetType("System.String"))             '登録者グループCD
                .Columns.Add("RegID", Type.GetType("System.String"))                '登録者ID
                .Columns.Add("EntryNmb", Type.GetType("System.String"))             '登録順
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'リリース実施対象システム
            With dtRelIrai
                .Columns.Add("CINmb", Type.GetType("System.String"))                '対象システム
                '[mod] 2012/09/07 y.ikushima 表示不具合対応START
                '.Columns.Add("RegDt", Type.GetType("System.String"))               '登録日時
                .Columns.Add("RegDt", Type.GetType("System.DateTime"))              '登録日時
                '[mod] 2012/09/07 y.ikushima 表示不具合対応END
                .Columns.Add("RegGrpCD", Type.GetType("System.String"))             '登録者グループCD
                .Columns.Add("RegID", Type.GetType("System.String"))                '登録者ID
                .Columns.Add("EntryNmb", Type.GetType("System.String"))             '登録順
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'リリース関連ファイル用テーブル作成
            With dtRelFile
                .Columns.Add("FileNaiyo", Type.GetType("System.String"))          '説明
                .Columns.Add("RegDT", Type.GetType("System.String"))              '登録日時
                .Columns.Add("MngNmb", Type.GetType("System.String"))             'ファイル管理番号
                .Columns.Add("FilePath", Type.GetType("System.String"))           'ファイルパス
                .Columns.Add("RegDt_", Type.GetType("System.String"))               '登録日時
                .Columns.Add("RegGrpCD", Type.GetType("System.String"))            '登録者グループCD
                .Columns.Add("RegID", Type.GetType("System.String"))               '登録者ID

                'テーブルの変更を確定
                .AcceptChanges()
            End With

            '会議情報テーブル作成
            With dtMeeting
                .Columns.Add("MeetingNmb", Type.GetType("System.String"))         'ファイル管理番号
                .Columns.Add("JisiSTDT", Type.GetType("System.String"))           '実施日
                .Columns.Add("ResultKbnNM", Type.GetType("System.String"))        '承認
                .Columns.Add("Title", Type.GetType("System.String"))              'タイトル
                .Columns.Add("ResultKbn", Type.GetType("System.String"))          '承認CD：隠し
                .Columns.Add("RegDt", Type.GetType("System.String"))               '登録日時
                .Columns.Add("RegGrpCD", Type.GetType("System.String"))            '登録者グループCD
                .Columns.Add("RegID", Type.GetType("System.String"))               '登録者ID
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            '対応関係情報用テーブル作成
            With dtRelInfo
                .Columns.Add("RelationKbn", Type.GetType("System.String"))         '区分
                .Columns.Add("RelationID", Type.GetType("System.String"))          'ID
                .Columns.Add("GroupNM", Type.GetType("System.String"))             'グループ名
                .Columns.Add("HBKUsrNM", Type.GetType("System.String"))            'ユーザー名
                .Columns.Add("RegDt", Type.GetType("System.String"))               '登録日時
                .Columns.Add("RegGrpCD", Type.GetType("System.String"))            '登録者グループCD
                .Columns.Add("RegID", Type.GetType("System.String"))               '登録者ID
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'プロセスリンクテーブル作成
            With dtPlinkInfo
                .Columns.Add("ProcessKbnNMR", Type.GetType("System.String"))       '区分
                .Columns.Add("MngNmb", Type.GetType("System.String"))              '管理番号
                .Columns.Add("ProcessKbn", Type.GetType("System.String"))          'プロセス区分_隠し
                .Columns.Add("RegDt", Type.GetType("System.String"))               '登録日時
                .Columns.Add("RegGrpCD", Type.GetType("System.String"))            '登録者グループCD
                .Columns.Add("RegID", Type.GetType("System.String"))               '登録者ID
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'データクラスに作成テーブルを格納
            With dataHBKF0201
                .PropDtRelIrai = dtRelIrai                                         'スプレッド表示用：リリース依頼受領システム
                .PropDtRelJissi = dtRelJissi                                       'スプレッド表示用：リリース実施対象システム
                .PropDtRelFileInfo = dtRelFile                                     'スプレッド表示用：原因リンクデータ
                .PropDtMeeting = dtMeeting                                         'スプレッド表示用：会議情報 
                .PropDtRelation = dtRelInfo                                        'スプレッド表示用：履歴情報データ
                .PropDtprocessLink = dtPlinkInfo                                   'スプレッド表示用：プロセスリンク情報データ
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
    ''' 【共通】初期表示用データ取得処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            'マスタデータ取得
            If GetMastaData(Adapter, Cn, dataHBKF0201) = False Then
                Return False
            End If

            'メイン表示データ取得
            If GetMainData(Adapter, Cn, dataHBKF0201) = False Then
                Return False
            End If

            'コネクションを閉じる
            Cn.Close()


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
    ''' 【編集モード】解除ボタンクリック時ロック設定処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLockWhenUnlock(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'リリース共通情報テーブルロック解除
            If UnlockRelInfo(dataHBKF0201.PropIntRelNmb) = False Then
                Return False
            End If

            'リリース共通情報テーブルロック
            If LockRelInfo(dataHBKF0201.PropIntRelNmb, dataHBKF0201.PropDtRelLock) = False Then
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
    ''' リリース ロック解除処理
    ''' </summary>
    ''' <param name="intNmb">[IN]リリース番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>リリース共通情報のロックを解除する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function UnlockRelInfo(ByVal intNmb As Integer) As Boolean

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

            'リリース共通情報ロックテーブル削除処理
            If DeleteLock(Cn, intNmb) = False Then
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'コミット
            Tsx.Commit()

            'ログ出力
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
    ''' 【担当ID入力時】ユーザーマスタデータ取得メイン処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>指定したひびきユーザーのマスタデータを取得する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetTantoDataMain(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try

            'コネクションを開く
            Cn.Open()

            'マスタデータ取得
            If GetTantoData(Adapter, Cn, dataHBKF0201) = False Then
                Return False
            End If

            'コネクションを閉じる
            Cn.Close()


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
    ''' 【ID入力時】ユーザーマスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>マスタデータを取得する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetTantoData(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKF0201.GetTantoInfoData(Adapter, Cn, dataHBKF0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ユーザーマスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            '取得データをデータクラスにセット
            dataHBKF0201.PropDtResultSub = dtmst


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
    ''' リリース ロック処理
    ''' </summary>
    ''' <param name="intNmb">[IN]リリース番号</param>
    ''' <param name="dtRelLock">[IN/OUT]リリース共通情報ロックテーブルデータ格納テーブル</param>
    ''' <param name="blnDoUnlock">[IN]解除実行フラグ（True：解除してからロックする）※省略可</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>リリース番号をキーにリリース共通情報ロックテーブルのデータをINSERTする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function LockRelInfo(ByVal intNmb As Integer, _
                               ByRef dtRelLock As DataTable, _
                               Optional ByVal blnDoUnlock As Boolean = False) As Boolean

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

            'ロック解除実行フラグがONの場合、リリース共通情報ロックテーブルデータを削除
            If blnDoUnlock = True Then
                If DeleteLock(Cn, intNmb) = False Then
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    Return False
                End If
            End If

            'リリース共通情報ロックテーブル登録
            If InsertLock(Cn, intNmb) = False Then
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'データ格納用テーブル初期化
            dtRelLock = New DataTable

            'リリース共通情報ロックテーブル取得
            If sqlHBKF0201.SelectLock(Adapter, Cn, intNmb) = False Then
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, OUTPUT_LOG_TITLE & "共通情報ロックテーブル取得", Nothing, Adapter.SelectCommand)

            'SQLを実行し、結果を取得
            Adapter.Fill(dtRelLock)

            'コミット
            Tsx.Commit()

            '2行取得できた場合（ロック情報がある場合）、ロック情報にサーバー日付を不可
            If dtRelLock.Rows.Count > 1 Then
                'ロック情報にサーバー日付を設定
                dtRelLock.Rows(1).Item("SysTime") = dtRelLock.Rows(0).Item("SysTime")
                '1行目のデータを削除
                dtRelLock.Rows(0).Delete()
                '変更をコミット
                dtRelLock.AcceptChanges()
            End If

            'ログ出力
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
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Adapter.Dispose()
            If Tsx IsNot Nothing Then
                Tsx.Dispose()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 共通情報ロックテーブル削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>管理番号をキーに共通情報ロックテーブルのデータを物理削除（DELETE）する
    ''' <para>作成情報：2012/09/04 s.tsuruta
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function DeleteLock(ByVal Cn As NpgsqlConnection, _
                                  ByVal intNmb As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '共通情報ロックテーブル削除処理
        Dim Cmd As New NpgsqlCommand          'SQLコマンド

        Try

            'DeleteLockSql
            If sqlHBKF0201.DeleteLockSql(Cmd, Cn, intNmb) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, OUTPUT_LOG_TITLE & "共通情報ロックテーブル削除", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

            'ログ出力
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
    ''' 共通情報ロックテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>共通情報ロックテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/09/04 s.tsuruta
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function InsertLock(ByVal Cn As NpgsqlConnection, _
                                   ByVal intNmb As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '共通情報ロック用変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            '共通情報ロックテーブル登録
            If sqlHBKF0201.InsertLockSql(Cmd, Cn, intNmb) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, OUTPUT_LOG_TITLE & "共通情報ロックテーブル登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Cmd)

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
    ''' 【新規登録モード】データ登録メイン処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をデータベースに反映する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RegistDataOnNewModeMain(ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '登録前対応関係者メイン
        If GetDtSystem(dataHBKF0201) = False Then
            Return False
        End If

        '新規登録処理
        If InsertNewData(dataHBKF0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【編集モード】データ登録メイン処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をデータベースに反映する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RegistDataOnEditModeMain(ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '登録前対応関係者メイン
        If GetDtSystem(dataHBKF0201) = False Then
            Return False
        End If

        '更新処理
        If UpdateData(dataHBKF0201) = False Then
            Return False
        End If

        'ロック解除処理
        If UnlockData(dataHBKF0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】登録前対応関係者処理メイン
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対応関係者情報を確認する
    ''' <para>作成情報：2012/09/05 s.tsuruta
    ''' <p>改訂情報 : ：2012/10/12 r.hoshino</p>
    ''' </para></remarks>
    Private Function GetDtSystem(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '登録行作成
            Dim row As DataRow = dataHBKF0201.PropDtRelIrai.NewRow
            Dim EntryNmb As Integer = 0
            Dim convTable As New DataTable
            convTable.Columns.Add("RelationKbn", Type.GetType("System.String"))
            convTable.Columns.Add("RelationID", Type.GetType("System.String"))
            convTable.Columns.Add("UpdateFlg", Type.GetType("System.Boolean"))
            dataHBKF0201.PropDtIrai = convTable.Clone

            For i As Integer = 0 To dataHBKF0201.PropVwIrai.Sheets(0).Rows.Count - 1

                If dataHBKF0201.PropVwIrai.Sheets(0).GetValue(i, COL_IRAI) IsNot Nothing Then

                    '対象システム（icnmb)
                    row.Item(0) = dataHBKF0201.PropVwIrai.Sheets(0).GetValue(i, COL_IRAI)
                    dataHBKF0201.PropRowReg = row
                    '登録順（Entrynmb)
                    EntryNmb = dataHBKF0201.PropVwIrai.Sheets(0).GetValue(i, COL_IRAI_ENTRY)
                    '登録前対応関係者処理
                    If GetDtSysKankei(dataHBKF0201, RELSYSTEM_KBN_IRAI, EntryNmb) = False Then
                        Return False
                    End If

                    '取得できれば行追加
                    If dataHBKF0201.PropDtResultSub.Rows.Count > 0 Then
                        For x As Integer = 0 To dataHBKF0201.PropDtResultSub.Rows.Count - 1
                            Dim row2 As DataRow = dataHBKF0201.PropDtIrai.NewRow
                            row2.Item("RelationKbn") = dataHBKF0201.PropDtResultSub.Rows(x).Item("RelationKbn")
                            row2.Item("RelationID") = dataHBKF0201.PropDtResultSub.Rows(x).Item("RelationID")
                            row2.Item("UpdateFlg") = dataHBKF0201.PropBlnCheckSystemNmb
                            dataHBKF0201.PropDtIrai.Rows.Add(row2)
                        Next
                    End If
                End If

            Next

            For i As Integer = 0 To dataHBKF0201.PropVwJissi.Sheets(0).Rows.Count - 1

                If dataHBKF0201.PropVwJissi.Sheets(0).GetValue(i, COL_IRAI) IsNot Nothing Then
                    '対象システム（icnmb)
                    row.Item(0) = dataHBKF0201.PropVwJissi.Sheets(0).GetValue(i, COL_JISSI)
                    dataHBKF0201.PropRowReg = row
                    '登録順（Entrynmb)
                    EntryNmb = dataHBKF0201.PropVwJissi.Sheets(0).GetValue(i, COL_JISSI_ENTRY)
                    '登録前対応関係者処理
                    If GetDtSysKankei(dataHBKF0201, RELSYSTEM_KBN_TAISYO, EntryNmb) = False Then
                        Return False
                    End If

                    '取得できれば行追加
                    If dataHBKF0201.PropDtResultSub.Rows.Count > 0 Then
                        For x As Integer = 0 To dataHBKF0201.PropDtResultSub.Rows.Count - 1
                            Dim row2 As DataRow = dataHBKF0201.PropDtIrai.NewRow
                            row2.Item("RelationKbn") = dataHBKF0201.PropDtResultSub.Rows(x).Item("RelationKbn")
                            row2.Item("RelationID") = dataHBKF0201.PropDtResultSub.Rows(x).Item("RelationID")
                            row2.Item("UpdateFlg") = dataHBKF0201.PropBlnCheckSystemNmb
                            dataHBKF0201.PropDtIrai.Rows.Add(row2)
                        Next
                    End If
                End If

            Next



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
    ''' 【共通】登録前対応関係者処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対応関係者情報を確認する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetDtSysKankei(ByRef dataHBKF0201 As DataHBKF0201, ByVal kbn As String, ByVal EntryNmb As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            '対象システム関係者データ取得
            If GetSysKankei(Adapter, Cn, dataHBKF0201) = False Then
                Return False
            End If

            If EntryNmb <> 0 Then
                '対象システム変更チェック
                If CheckSysNmb(Adapter, Cn, dataHBKF0201, kbn, EntryNmb) = False Then
                    Return False
                End If
            Else
                'EntryNmbが0＝新規追加
                dataHBKF0201.PropBlnCheckSystemNmb = True
            End If

            'コネクションを閉じる
            Cn.Close()

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
    ''' 【共通】対象システム関係者データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対象システムのCI番号から関係データを取得する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetSysKankei(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try


            '取得用SQLの作成・設定
            If sqlHBKF0201.GetChkKankeiSysData(Adapter, Cn, dataHBKF0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対応関係者取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            dataHBKF0201.PropDtResultSub = dtmst


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
    ''' 【編集モード】フォームロード時ロック設定処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLockWhenLoad(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnBeLocked As Boolean = False                   'ロックフラグ  

        Try

            With dataHBKF0201

                'ロック解除チェック処理
                If CheckDataBeLocked(.PropIntRelNmb, blnBeLocked, .PropStrBeLockedMsg, .PropDtRelLock) = False Then
                    Return False
                End If


                'ロックされている（別のユーザが編集中）場合、 ロックフラグをON
                If blnBeLocked = True Then

                    dataHBKF0201.PropBlnBeLockedFlg = True

                Else

                    'ロックされていない場合、問題共通情報をロックする
                    If SetLock(dataHBKF0201) = False Then
                        Return False
                    End If

                    'ロックフラグをOFF
                    dataHBKF0201.PropBlnBeLockedFlg = False

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
    ''' L-1-1.ロック状況チェック処理
    ''' </summary>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <param name="blnBeLocked">[IN/OUT]ロックフラグ（ロック時：True）</param>
    ''' <param name="strBeLockedMsg">ロック時メッセージ</param>
    ''' <param name="dtLock">共通情報ロックテーブル</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>指定された管理番号の共通情報がロックされているかチェックする。
    ''' また、ロックされている場合はエラーメッセージも返す
    ''' <para>作成情報：2012/09/04 s.tsuruta
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function CheckDataBeLocked(ByVal intNmb As Integer, _
                                         ByRef blnBeLocked As Boolean, _
                                         ByRef strBeLockedMsg As String, _
                                         ByRef dtLock As DataTable) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '問題共通情報ロックテーブル項目格納用変数宣言
        Dim dtResult As New DataTable           'SELECT結果格納テーブル

        'ロックチェック用変数宣言
        Dim strEdiID As String = ""             '編集者ID
        Dim strEdiTime As String = ""           '編集開始日時
        Dim strSysTime As String                '現在日時（サーバー）
        Dim tsDiff As TimeSpan = Nothing        '編集開始日時と現在日時の差
        Dim tsUnlock As TimeSpan = Nothing      'ロック解除時間  

        Try
            'ロックフラグ、共通情報ロックデータ数初期化
            blnBeLocked = False

            '共通情報ロックテーブル取得
            If GetLockTb(intNmb, dtResult) = False Then
                Return False
            End If

            '現在日時を取得
            strSysTime = dtResult.Rows(0).Item("SysTime").ToString()

            '共通情報ロックデータが取得できた場合、チェックを行う
            If dtResult.Rows.Count > 0 AndAlso dtResult.Rows(0).Item("EdiID") <> "" Then

                '編集者IDを取得
                strEdiID = dtResult.Rows(0).Item("EdiID")

                '編集者IDがログインユーザIDと異なるかチェック
                'If strEdiID <> PropUserId Then

                '編集者IDがログインユーザIDと異なる場合、サーバーの編集開始日時を取得
                strEdiTime = dtResult.Rows(0).Item("EdiTime").ToString()

                '編集開始日時がセットされている場合、現在日時と編集開始日時の差異がシステム管理マスタ.ロック解除時間以内かチェック
                If strEdiTime <> "" Then

                    '現在日時と編集開始日時の差を取得し、その差がロック解除時間を下回る場合はロックされている
                    tsDiff = New TimeSpan(DateTime.Parse(strSysTime).Subtract(DateTime.Parse(strEdiTime)).Ticks)
                    tsUnlock = TimeSpan.Parse(PropUnlockTime)
                    If tsDiff < tsUnlock Then

                        'ロックフラグON
                        blnBeLocked = True

                    End If

                End If

                'End If

                'ロックフラグがONの場合、ロック画面表示メッセージセット
                If blnBeLocked = True Then
                    'ロック画面表示メッセージセット
                    strBeLockedMsg = String.Format(HBK_I001, dtResult.Rows(0).Item("EdiGroupNM"), dtResult.Rows(0).Item("EdiUsrNM"))
                End If

            End If

            '取得データを戻り値セット
            dtLock = dtResult

            'ログ出力
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
        End Try

    End Function

    ''' <summary>
    ''' 【共通】マスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>マスタデータを取得する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMastaData(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'プロセスステータスマスタ取得
            If GetProcessStateMasta(Adapter, Cn, dataHBKF0201) = False Then
                Return False
            End If

            'グループマスタ取得
            If GetTantoMastaData(Adapter, Cn, dataHBKF0201) = False Then
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
    ''' 【共通】プロセスステータスマスタ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスステータスマスタデータを取得する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetProcessStateMasta(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKF0201.GetCmbProcessStateMstData(Adapter, Cn, dataHBKF0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "プロセスステータスマスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            'データが取得できなかった場合、エラー
            If dtmst.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & F0201_E001, TBNM_PROCESSSTATE_MTB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKF0201.PropDtStateMasta = dtmst


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
    ''' 【共通】グループマスタ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>リグループマスタデータを取得する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetTantoMastaData(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKF0201.GetTantoMastaData(Adapter, Cn, dataHBKF0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "グループマスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            'データが取得できなかった場合、エラー
            If dtmst.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & F0201_E001, TBNM_GRP_MTB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKF0201.PropDtTantoGrpMasta = dtmst


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
    ''' 【共通】初期表示用メインデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainData(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKF0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用データ取得
                    If GetMainDataForNew(Adapter, Cn, dataHBKF0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード

                    '編集モード用データ取得
                    If GetMainDataForEdit(Adapter, Cn, dataHBKF0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then    '参照（ロック）モード

                    '参照モード用データ取得　※編集モードと同じ
                    If GetMainDataForEdit(Adapter, Cn, dataHBKF0201) = False Then
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
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードで初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainDataForNew(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'CI共通情報データ取得
            If GetCIInfo(Adapter, Cn, dataHBKF0201) = False Then
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
    ''' 【編集／参照モード】初期表示用メインデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集／参照モードで初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainDataForEdit(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'リリース共通情報データ取得
            If GetReleaseInfo(Adapter, Cn, dataHBKF0201) = False Then
                Return False
            End If

            'CI共通情報データ取得
            If GetCIInfo(Adapter, Cn, dataHBKF0201) = False Then
                Return False
            End If

            '担当履歴情報データ取得
            If GetTantoRireki(Adapter, Cn, dataHBKF0201) = False Then
                Return False
            End If

            'リリース依頼受領システムデータ取得
            If GetReleaseIraiSystem(Adapter, Cn, dataHBKF0201) = False Then
                Return False
            End If

            'リリース実施対象システムデータ取得
            If GetReleaseJissiSystem(Adapter, Cn, dataHBKF0201) = False Then
                Return False
            End If

            'リリース関連ファイルデータ取得
            If GetReleaseFile(Adapter, Cn, dataHBKF0201) = False Then
                Return False
            End If

            '会議結果情報データ取得
            If GetMeeting(Adapter, Cn, dataHBKF0201) = False Then
                Return False
            End If

            'リリース対応関係データ取得
            If GetReleaseKankei(Adapter, Cn, dataHBKF0201) = False Then
                Return False
            End If

            'プロセスリンク情報取得
            If GetPLink(Adapter, Cn, dataHBKF0201) = False Then
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
    ''' 【編集／参照モード】リリース共通情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>リリース共通情報データを取得する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetReleaseInfo(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtRelInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKF0201.SetSelectGetReleaseInfoSql(Adapter, Cn, DataHBKF0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "リリース共通情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtRelInfo)

            '取得データをデータクラスにセット
            DataHBKF0201.PropDtReleaseInfo = dtRelInfo


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
            dtRelInfo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【新規登録/編集/参照モード】CI共通情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報データを取得する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetCIInfo(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtCIInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKF0201.SetSelectGetCIInfoSql(Adapter, Cn, DataHBKF0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtCIInfo)

            'データが取得できなかった場合、エラー
            If dtCIInfo.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & F0201_E001, TBNM_CI_INFO_TB)
                Return False
            End If

            '取得データをデータクラスにセット
            DataHBKF0201.PropDtCIInfo = dtCIInfo


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
    ''' 【編集／参照モード】リリース依頼受領システムデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>リリース依頼受領システムデータを取得する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetReleaseIraiSystem(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtRelSystem As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKF0201.SetSelectGetReleaseIraiSystemSql(Adapter, Cn, DataHBKF0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "リリース依頼受領システムデータ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtRelSystem)

            '取得データをデータクラスにセット
            DataHBKF0201.PropDtRelIrai = dtRelSystem

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
            dtRelSystem.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集／参照モード】リリース実施対象システムデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>リリース実施対象システムを取得する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetReleaseJissiSystem(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtRelSystem As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKF0201.SetSelectGetReleaseJissiSystemSql(Adapter, Cn, DataHBKF0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "リリース実施対象システム取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtRelSystem)

            '取得データをデータクラスにセット
            DataHBKF0201.PropDtRelJissi = dtRelSystem

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
            dtRelSystem.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【編集／参照モード】リリ－ス関連ファイル情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>リリ－ス関連ファイルデータを取得する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetReleaseFile(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtRelFile As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKF0201.SetSelectGetReleaseFileSql(Adapter, Cn, DataHBKF0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "リリ－ス関連ファイルデータ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtRelFile)

            '取得データをデータクラスにセット
            DataHBKF0201.PropDtRelFileInfo = dtRelFile

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
            dtRelFile.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【編集／参照モード】会議情報/会議結果情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報/会議結果情報データを取得する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMeeting(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtMeeting As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKF0201.SetSelectGetMeetingSql(Adapter, Cn, DataHBKF0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議情報/会議結果情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtMeeting)

            '取得データをデータクラスにセット
            DataHBKF0201.PropDtMeeting = dtMeeting


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
    ''' 【編集／参照モード】リリース対応関係情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>リリース対応関係情報データを取得する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetReleaseKankei(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtRelKankei As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKF0201.SetSelectGetReleaseKankeiSql(Adapter, Cn, DataHBKF0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "リリ－ス対応関係情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtRelKankei)

            '取得データをデータクラスにセット
            DataHBKF0201.PropDtRelation = dtRelKankei


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
            dtRelKankei.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集／参照モード】プロセスリンク情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンク情報データを取得する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetPLink(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtPLink As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKF0201.SetSelectPLinkSql(Adapter, Cn, DataHBKF0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "プロセスリンク情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtPLink)

            '取得データをデータクラスにセット
            DataHBKF0201.PropDtprocessLink = dtPLink


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
            dtPLink.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】フォームコントロール設定
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InitFormControl(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'スプレッド設定
            If SetVwControl(dataHBKF0201) = False Then
                Return False
            End If

            '処理モードに応じたフォームコントロール設定
            If SetFormControlPerProcMode(dataHBKF0201) = False Then
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
    ''' 【共通】処理モード毎のフォームコントロール設定
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフォームコントロールの設定を行う
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormControlPerProcMode(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'ログイン／ロックコントロール設定
            If SetLoginAndLockControl(dataHBKF0201) = False Then
                Return False
            End If

            'ヘッダ設定
            If SetHeaderControl(dataHBKF0201) = False Then
                Return False
            End If

            'フッタ設定
            If SetFooterControl(dataHBKF0201) = False Then
                Return False
            End If

            'タブページ設定
            If SetTabControl(dataHBKF0201) = False Then
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
    ''' 【共通】ログイン／ロックコントロール設定
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControl(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKF0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetLoginAndLockControlForNew(dataHBKF0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード

                    '編集モード用設定
                    If SetLoginAndLockControlForEdit(dataHBKF0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then    '参照（ロック）モード

                    '参照（ロック）モード用設定
                    If SetLoginAndLockControlForRef(dataHBKF0201) = False Then
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
    ''' 【新規登録モード】ログイン／ロックコントロール設定
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForNew(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKF0201.PropGrpLoginUser

                'グループコンボボックス非活性
                .cmbGroup.Enabled = False

                '変更ボタン非活性
                .btnChange.Enabled = False

                'ロック情報表示
                .PropLockInfoVisible = True

                '解除ボタン非表示
                .PropBtnUnlockVisible = False


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
    ''' 【編集モード】ログイン／ロックコントロール設定
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForEdit(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKF0201.PropGrpLoginUser

                'グループコンボボックス非活性
                .cmbGroup.Enabled = False

                '変更ボタン非活性
                .btnChange.Enabled = False

                'ロック情報表示
                .PropLockInfoVisible = True

                '解除ボタン表示
                .PropBtnUnlockVisible = True

                '解除ボタン非活性
                .PropBtnUnlockEnabled = False

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
    ''' 【参照モード】ログイン／ロックコントロール設定
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForRef(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKF0201.PropGrpLoginUser

                'グループコンボボックス非活性
                .cmbGroup.Enabled = False

                '変更ボタン非活性
                .btnChange.Enabled = False

                'ロック情報表示
                .PropLockInfoVisible = True

                '解除ボタン表示
                .PropBtnUnlockVisible = True


                '関係者か？
                If dataHBKF0201.PropIntChkKankei <> KANKEI_CHECK_NONE Then
                    '解除ボタン表示
                    .PropBtnUnlockVisible = True

                    'ロックされているか？同じグループか？
                    If dataHBKF0201.PropBlnBeLockedFlg = True AndAlso dataHBKF0201.PropDtRelLock.Rows.Count > 0 AndAlso _
                       dataHBKF0201.PropDtRelLock.Rows(0).Item("EdiGrpCD").ToString.Equals(PropWorkGroupCD) Then
                        '解除ボタン活性
                        .PropBtnUnlockEnabled = True
                    Else
                        '解除ボタン非活性
                        .PropBtnUnlockEnabled = False
                    End If

                Else
                    '解除ボタン非表示
                    .PropBtnUnlockVisible = False
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
    ''' 【共通】ヘッダコントロール設定
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてヘッダコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/31 s.turuta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetHeaderControl(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKF0201


                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetHeaderControlForNew(dataHBKF0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード

                    '編集モード用設定
                    If SetHeaderControlForEdit(dataHBKF0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then    '参照（ロック）モード

                    '参照モード用設定
                    If SetHeaderControlForRef(dataHBKF0201) = False Then
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
    ''' 【新規登録モード】ヘッダコントロール設定
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでヘッダコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetHeaderControlForNew(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKF0201


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
    ''' 【編集モード】ヘッダコントロール設定
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでヘッダコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetHeaderControlForEdit(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKF0201


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
    ''' 【参照モード】ヘッダコントロール設定
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでヘッダコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetHeaderControlForRef(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKF0201


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
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControl(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKF0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetFooterControlForNew(dataHBKF0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード



                    '編集モード用設定
                    If SetFooterControlForEdit(dataHBKF0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then    '参照（ロック）モード

                    '参照モード用設定
                    If SetFooterControlForRef(dataHBKF0201) = False Then
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
    ''' 【新規登録モード】フッタコントロール設定
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForNew(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKF0201

                .PropBtnAddRow_relaU.Enabled = True           '対応関係者U
                .PropBtnAddRow_relaU.Enabled = True           '対応関係者G
                .PropBtnRemoveRow_rela.Enabled = True         '対応関係者ー
                .PropBtnAddRow_Plink.Enabled = True           'プロセスリンク＋
                .PropBtnRemoveRow_Plink.Enabled = True        'プロセスリンクー

                .PropBtnReg.Enabled = True                    '登録
                .PropBtnMail.Enabled = True                   'メール作成

                '呼び出し元に応じて変更
                If .PropIntOwner = SCR_CALLMOTO_REG Then
                    .PropBtnBack.Text = "閉じる"
                Else
                    '.PropBtnBack.Text = "戻る"
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
    ''' 【編集モード】フッタコントロール設定
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForEdit(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKF0201

                .PropBtnAddRow_relaU.Enabled = True         '対応関係者U
                .PropBtnAddRow_relaG.Enabled = True         '対応関係者G
                .PropBtnRemoveRow_rela.Enabled = True       '対応関係者ー
                .PropBtnAddRow_Plink.Enabled = True         'プロセスリンク＋
                .PropBtnRemoveRow_Plink.Enabled = True      'プロセスリンクー

                .PropBtnReg.Enabled = True                  '登録
                .PropBtnMail.Enabled = True                 'メール作成

                '呼び出し元に応じて変更
                If .PropIntOwner = SCR_CALLMOTO_REG Then
                    .PropBtnBack.Text = "閉じる"
                Else
                    '.PropBtnBack.Text = "戻る"
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
    ''' 【参照モード】フッタコントロール設定
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForRef(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKF0201

                '対応関係者グループ行追加ボタン
                .PropBtnAddRow_relaG.Enabled = False
                '対応関係者ユーザー行追加ボタン
                .PropBtnAddRow_relaU.Enabled = False
                '対応関係者行削除ボタン
                .PropBtnRemoveRow_rela.Enabled = False
                'プロセスリンク行追加ボタン
                .PropBtnAddRow_Plink.Enabled = False
                'プロセスリンク行削除ボタン
                .PropBtnRemoveRow_Plink.Enabled = False
                '登録ボタン非活性
                .PropBtnReg.Enabled = False

                '呼び出し元に応じて変更
                If .PropIntOwner = SCR_CALLMOTO_REG Then
                    .PropBtnBack.Text = "閉じる"
                Else
                    '.PropBtnBack.Text = "戻る"
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
    ''' 【共通】タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてタブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControl(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '基本情報タブ設定
            If SetTabControlKhn(dataHBKF0201) = False Then
                Return False
            End If

            '会議情報タブ設定
            If SetTabControlMeeting(dataHBKF0201) = False Then
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
    ''' 【共通】基本情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhn(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKF0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetTabControlKhnForNew(dataHBKF0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード

                    '編集モード用設定
                    If SetTabControlKhnForEdit(dataHBKF0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then    '参照（ロック）モード

                    '参照（ロック）モード用設定
                    If SetTabControlKhnForRef(dataHBKF0201) = False Then
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
    ''' 【新規登録モード】基本情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードで応じて基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForNew(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKF0201

                '開くボタン
                .PropBtnRelationFileOpen.Enabled = False
                'ダウンロードボタン
                .PropBtnRelationFileDownLoad.Enabled = False

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
    ''' 【編集モード】基本情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForEdit(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKF0201

                'リリース依頼受領システム
                .PropBtnAddRow_Irai.Enabled = True
                .PropBtnRemoveRow_Irai.Enabled = True
                'リリース実施対象システム
                .PropBtnAddRow_Jissi.Enabled = True
                .PropBtnRemoveRow_Jissi.Enabled = True
                'リリース予定日時（目安）時ボタン
                .PropBtnRelSceDT_HM.Enabled = True
                '担当(検索)ボタン
                .PropBtnSearch.Enabled = True
                '担当(私)ボタン
                .PropBtnMy.Enabled = True
                'リリース着手日時、時ボタン
                .PropBtnRelStDT_HM.Enabled = True
                'リリース終了日時、時ボタン
                .PropBtnRelEdDT_HM.Enabled = True
                '関連ファイル情報行追加ボタン
                .PropBtnAddRow_RelationFile.Enabled = True
                '関連ファイル情報行削除ボタン
                .PropBtnRemoveRow_RelationFile.Enabled = True
                '関連ファイル情報行「ダ」ボタン
                .PropBtnRelationFileDownLoad.Enabled = True
                '関連ファイル情報行「開」ボタン
                .PropBtnRelationFileOpen.Enabled = True

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
    ''' 【参照モード】基本情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForRef(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKF0201

                'リリース依頼受領システム
                .PropBtnAddRow_Irai.Enabled = False
                .PropBtnRemoveRow_Irai.Enabled = False
                'リリース実施対象システム
                .PropBtnAddRow_Jissi.Enabled = False
                .PropBtnRemoveRow_Jissi.Enabled = False
                'リリース予定日時（目安）時ボタン
                .PropBtnRelSceDT_HM.Enabled = False
                '担当(検索)ボタン
                .PropBtnSearch.Enabled = False
                '担当(私)ボタン
                .PropBtnMy.Enabled = False
                'リリース着手日時、時ボタン
                .PropBtnRelStDT_HM.Enabled = False
                'リリース終了日時、時ボタン
                .PropBtnRelEdDT_HM.Enabled = False
                '関連ファイル情報行追加ボタン
                .PropBtnAddRow_RelationFile.Enabled = False
                '関連ファイル情報行削除ボタン
                .PropBtnRemoveRow_RelationFile.Enabled = False
                '関連ファイル情報行「ダ」ボタン
                .PropBtnRelationFileDownLoad.Enabled = True
                '関連ファイル情報行「開」ボタン
                .PropBtnRelationFileOpen.Enabled = True

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
    ''' 【共通】会議情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて会議情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlMeeting(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKF0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetTabControlMeetingForNew(dataHBKF0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    '編集モード用設定
                    If SetTabControlMeetingForEdit(dataHBKF0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then    '参照（ロック）モード

                    '参照（ロック）モード用設定
                    If SetTabControlMeetingForRef(dataHBKF0201) = False Then
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
    ''' 【新規登録モード】会議情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードで会議情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlMeetingForNew(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKF0201

                '会議情報スプレッド行追加ボタン
                .PropBtnAddRow_Meeting.Enabled = False
                '会議情報スプレッド行削除ボタン
                .PropBtnRemoveRow_Meeting.Enabled = False

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
    ''' 【編集モード】会議情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで会議情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlMeetingForEdit(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKF0201

                '会議情報スプレッド行追加ボタン
                .PropBtnAddRow_Meeting.Enabled = True
                '会議情報スプレッド行削除ボタン
                .PropBtnRemoveRow_Meeting.Enabled = True

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
    ''' 【参照モード】会議情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで会議情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlMeetingForRef(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKF0201

                '会議情報スプレッド行追加ボタン
                .PropBtnAddRow_Meeting.Enabled = False
                '会議情報スプレッド行削除ボタン
                .PropBtnRemoveRow_Meeting.Enabled = False

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
    ''' 【共通】初期データ設定処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームコントロールに初期表示データを設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitDataToControl(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'ログイン／ロックデータ設定
            If SetDataToLoginAndLock(dataHBKF0201) = False Then
                Return False
            End If

            'ヘッダデータ設定
            If SetDataToHeader(dataHBKF0201) = False Then
                Return False
            End If

            'フッタデータ設定
            If SetDataToFooter(dataHBKF0201) = False Then
                Return False
            End If

            'タブコントロールデータ設定
            If SetDataToTabControl(dataHBKF0201) = False Then
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
    ''' 【共通】ログイン／ロックデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLock(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKF0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToLoginAndLockForNew(dataHBKF0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード

                    '編集モード用設定
                    If SetDataToLoginAndLockForEdit(dataHBKF0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then    '参照（ロック）モード

                    '参照モード用設定
                    If SetDataToLoginAndLockForRef(dataHBKF0201) = False Then
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
    ''' 【新規登録モード】ログイン／ロックデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForNew(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKF0201.PropGrpLoginUser

                'ロック開始日時
                .PropLockDate = Nothing

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
    ''' 【編集モード】ログイン／ロックデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForEdit(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKF0201.PropGrpLoginUser

                'ロック情報が取得できた場合
                If dataHBKF0201.PropDtRelLock.Rows.Count > 0 Then
                    'ロック開始日時
                    Dim dtmLockTime As DateTime = Nothing  '編集開始日時
                    If dataHBKF0201.PropDtRelLock.Rows(0).Item("EdiTime").ToString() <> "" Then
                        dtmLockTime = dataHBKF0201.PropDtRelLock.Rows(0).Item("EdiTime")
                        dataHBKF0201.PropStrEdiTime = dtmLockTime
                    End If
                    .PropLockDate = dtmLockTime
                Else
                    'ロック開始日時
                    .PropLockDate = Nothing
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
    ''' 【参照モード】ログイン／ロックデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForRef(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKF0201.PropGrpLoginUser

                'ロック情報が取得できた場合
                If dataHBKF0201.PropDtRelLock IsNot Nothing AndAlso dataHBKF0201.PropDtRelLock.Rows.Count > 0 Then
                    'ロック開始日時
                    Dim dtmLockTime As DateTime = Nothing
                    If dataHBKF0201.PropDtRelLock.Rows(0).Item("EdiTime").ToString() <> "" Then
                        dtmLockTime = dataHBKF0201.PropDtRelLock.Rows(0).Item("EdiTime")
                    End If
                    .PropLockDate = dtmLockTime
                Else
                    'ロック開始日時
                    .PropLockDate = Nothing
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
    ''' 【共通】ヘッダデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ヘッダデータを初期設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToHeader(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKF0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToHeaderForNew(dataHBKF0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード

                    '編集モード用設定
                    If SetDataToHeaderForEdit(dataHBKF0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then    '参照（ロック）モード

                    '参照モード用設定
                    If SetDataToHeaderForRef(dataHBKF0201) = False Then
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
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでヘッダデータを初期設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToHeaderForNew(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKF0201

                'リリース管理番号
                .PropTxtRelNmb.Text = ""
                '登録情報
                .PropLblRegInfo.Text = ""
                '最終更新情報
                .PropLblFinalUpdateInfo.Text = ""

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
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでヘッダデータを初期設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToHeaderForEdit(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKF0201

                'リリース管理番号
                .PropTxtRelNmb.Text = .PropDtReleaseInfo.Rows(0).Item("RelNmb")
                '登録情報
                .PropLblRegInfo.Text = .PropDtReleaseInfo.Rows(0).Item("RegGrpNM") & " " & .PropDtReleaseInfo.Rows(0).Item("RegHbkUsrNM") & " " & .PropDtReleaseInfo.Rows(0).Item("RegDT")
                '最終更新情報
                .PropLblFinalUpdateInfo.Text = .PropDtReleaseInfo.Rows(0).Item("UpGrpNM") & " " & .PropDtReleaseInfo.Rows(0).Item("UpHbkUsrNM") & " " & .PropDtReleaseInfo.Rows(0).Item("UpdateDT")

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
    ''' 【参照モード】ヘッダデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでヘッダデータを初期設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToHeaderForRef(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '編集モードヘッダデータ設定処理と同じ
            If SetDataToHeaderForEdit(dataHBKF0201) = False Then
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
    ''' 【共通】フッタデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フッタデータを初期設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToFooter(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKF0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード
                    If SetDataToFooterForNew(dataHBKF0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    '編集モード用設定
                    If SetDataToFooterForEdit(dataHBKF0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then    '参照（ロック）モード

                    '参照モード用設定
                    If SetDataToFooterForRef(dataHBKF0201) = False Then
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
    ''' 【新規登録モード】フッタデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでフッタデータを初期設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToFooterForNew(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKF0201

                '対応関係者情報スプレッド
                .PropVwRelationInfo.Sheets(0).DataSource = .PropDtRelation
                'プロセスリンク情報スプレッド
                '呼び出し元に応じて変更
                If .PropIntOwner = SCR_CALLMOTO_REG Then
                    '問題画面の情報を挿入する
                    '問題登録画面のプロセスリンク情報を挿入する
                    Dim drProcessLink As DataRow
                    drProcessLink = .PropDtprocessLink.NewRow()
                    drProcessLink(COL_PLINK_KBN_NMR) = PROCESS_TYPE_CHANGE_NAME_R
                    drProcessLink(COL_PLINK_NO) = .PropIntChgNmb
                    drProcessLink(COL_PLINK_KBN) = PROCESS_TYPE_CHANGE
                    'DataTableに保存
                    .PropDtprocessLink.Rows.Add(drProcessLink)
                    For i As Integer = 0 To .PropVwProcessLinkInfo_Save.Sheets(0).Rows.Count - 1 Step 1
                        drProcessLink = .PropDtprocessLink.NewRow()
                        drProcessLink(COL_PLINK_KBN_NMR) = .PropVwProcessLinkInfo_Save.Sheets(0).GetText(i, COL_PLINK_KBN_NMR)
                        drProcessLink(COL_PLINK_NO) = .PropVwProcessLinkInfo_Save.Sheets(0).GetText(i, COL_PLINK_NO)
                        drProcessLink(COL_PLINK_KBN) = .PropVwProcessLinkInfo_Save.Sheets(0).GetText(i, COL_PLINK_KBN)
                        'DataTableに保存
                        .PropDtprocessLink.Rows.Add(drProcessLink)
                    Next
                    .PropVwProcessLinkInfo.DataSource = .PropDtprocessLink 'プロセスリンク情報：プロセスリンク情報スプレッド
                Else
                    .PropVwProcessLinkInfo.Sheets(0).DataSource = .PropDtprocessLink
                End If

                'グループ履歴
                .PropTxtGroupRireki.Text = ""
                '担当者履歴
                .PropTxtTantoRireki.Text = ""

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
    ''' 【編集モード】フッタデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでフッタデータを初期設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToFooterForEdit(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKF0201

                '対応関係者情報スプレッド
                .PropVwRelationInfo.Sheets(0).DataSource = .PropDtRelation

                'プロセスリンク情報スプレッド
                .PropVwProcessLinkInfo.Sheets(0).DataSource = .PropDtprocessLink

                ''グループ履歴
                '.PropTxtGroupRireki.Text = .PropDtReleaseInfo.Rows(0).Item("GroupRireki")
                ''担当者履歴
                '.PropTxtTantoRireki.Text = .PropDtReleaseInfo.Rows(0).Item("TantoRireki")

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
    ''' 【参照モード】フッタデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでフッタデータを初期設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToFooterForRef(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '※編集モードフッタデータ設定処理
            If SetDataToFooterForEdit(dataHBKF0201) = False Then
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
    ''' 【共通】タブコントロールデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>タブコントロールデータを初期設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabControl(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '基本情報タブデータ設定
            If SetDataToTabKhn(dataHBKF0201) = False Then
                Return False
            End If

            '会議情報タブデータ設定
            If SetDataToTabMeeting(dataHBKF0201) = False Then
                Return False
            End If

            'フリー入力タブデータ設定
            If SetDataToTabFree(dataHBKF0201) = False Then
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
    ''' 【共通】基本情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhn(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKF0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToTabKhnForNew(dataHBKF0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    '編集モード用設定
                    If SetDataToTabKhnForEdit(dataHBKF0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then    '参照（ロック）モード

                    '参照モード用設定
                    If SetDataToTabKhnForRef(dataHBKF0201) = False Then
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
    ''' 【新規登録モード】基本情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードで基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhnForNew(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'コンボボックス作成
            If CreateCmb(dataHBKF0201) = False Then
                Return False
            End If

            'スプレッドセルタイプ作成
            If CreateSpreadCtype(dataHBKF0201) = False Then
                Return False
            End If

            With dataHBKF0201

                'リリース受付番号
                .PropTxtRelUkeNmb.Text = ""
                'ステ－タス
                .PropCmbProcessState.SelectedValue = ""
                '依頼日（起票日）
                .PropDtpIraiDT.txtDate.Text = ""
                '通常・緊急
                .PropCmbTujyoKinkyuKbn.SelectedValue = ""
                'ユーザー周知必要区分
                .PropCmbUsrSyutiKbn.SelectedValue = ""
                'リリース依頼受領システム
                .PropVwIrai.DataSource = ""
                'リリース実施対象システム
                .PropVwJissi.DataSource = ""
                'リリース予定日時（目安）datetimepicker
                .PropDtpRelSceDT.txtDate.Text = ""
                'リリース予定日時（目安）テキストボックス
                .PropTxtRelSceDT_HM.PropTxtTime.Text = ""
                '担当グループコンボボックス
                .PropCmbTantoGrpCD.SelectedValue = ""
                '担当IDテキストボックス
                .PropTxtRelTantoID.Text = ""
                '担当氏名テキストボックス
                .PropTxtRelTantoNM.Text = ""
                'リリース着手日時datetimepicker
                .PropDtpRelStDT.txtDate.Text = ""
                'リリース着手日時テキストボックス
                .PropTxtRelStDT_HM.PropTxtTime.Text = ""
                'リリース終了日時datetimepicker
                .PropDtpRelEdDT.txtDate.Text = ""
                'リリース終了日時テキストボックス
                .PropTxtRelEdDT_HM.PropTxtTime.Text = ""
                '関連ファイル情報スプレッド
                .PropVwRelationFileInfo.DataSource = dataHBKF0201.PropDtRelFileInfo
                'タイトルテキストボックス
                .PropTxtTitle.Text = ""
                '概要テキストボックス
                '.PropTxtGaiyo.Text = ""


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
    ''' 【編集モード】基本情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhnForEdit(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'コンボボックス作成
            If CreateCmb(dataHBKF0201) = False Then
                Return False
            End If

            'スプレッドセルタイプ作成
            If CreateSpreadCtype(dataHBKF0201) = False Then
                Return False
            End If

            With dataHBKF0201

                'リリース受付番号
                .PropTxtRelUkeNmb.Text = .PropDtReleaseInfo.Rows(0).Item("RelUkeNmb")
                'ステ－タス
                .PropCmbProcessState.SelectedValue = .PropDtReleaseInfo.Rows(0).Item("ProcessStateCD")
                '依頼日（起票日）
                If .PropDtReleaseInfo.Rows(0).Item("IraiDT").ToString.Equals("") Then
                    .PropDtpIraiDT.txtDate.Text = ""
                Else
                    .PropDtpIraiDT.txtDate.Text = .PropDtReleaseInfo.Rows(0).Item("IraiDT").ToString.Substring(0, 10)
                End If
                '通常・緊急
                .PropCmbTujyoKinkyuKbn.SelectedValue = .PropDtReleaseInfo.Rows(0).Item("TujyoKinkyuKbn").ToString
                'ユーザー周知必要区分
                .PropCmbUsrSyutiKbn.SelectedValue = .PropDtReleaseInfo.Rows(0).Item("UsrSyutiKbn").ToString

                'リリース依頼受領システムスプレッド
                If dataHBKF0201.PropDtRelIrai.Rows.Count > 0 Then
                    With dataHBKF0201.PropVwIrai.Sheets(0)
                        .DataSource = dataHBKF0201.PropDtRelIrai
                        ''表示行追加
                        'dataHBKF0201.PropVwIrai.Sheets(0).AddRows(0, dataHBKF0201.PropDtRelIrai.Rows.Count)
                        'For i As Integer = 0 To dataHBKF0201.PropVwIrai.Sheets(0).RowCount - 1
                        '    dataHBKF0201.PropVwIrai.Sheets(0).SetValue(i, COL_IRAI, dataHBKF0201.PropDtRelIrai.Rows(i).Item("CINmb"))
                        'Next

                    End With
                End If

                'リリース実施対象システムスプレッド
                If dataHBKF0201.PropDtRelJissi.Rows.Count > 0 Then
                    With dataHBKF0201.PropVwJissi.Sheets(0)
                        .DataSource = dataHBKF0201.PropDtRelJissi
                        ''表示行追加
                        'dataHBKF0201.PropVwJissi.Sheets(0).AddRows(0, dataHBKF0201.PropDtRelJissi.Rows.Count)
                        'For i As Integer = 0 To dataHBKF0201.PropVwJissi.Sheets(0).RowCount - 1
                        '    dataHBKF0201.PropVwJissi.Sheets(0).SetValue(i, COL_JISSI, dataHBKF0201.PropDtRelJissi.Rows(i).Item("CINmb"))
                        'Next

                    End With
                End If

                'リリース予定日時（目安）datetimepicker
                If .PropDtReleaseInfo.Rows(0).Item("RelSceDT").ToString.Equals("") Then
                    .PropDtpRelSceDT.txtDate.Text = ""
                    .PropTxtRelSceDT_HM.PropTxtTime.Text = ""
                Else
                    .PropDtpRelSceDT.txtDate.Text = .PropDtReleaseInfo.Rows(0).Item("RelSceDT").ToString.Substring(0, 10)
                    .PropTxtRelSceDT_HM.PropTxtTime.Text = dataHBKF0201.PropDtReleaseInfo.Rows(0).Item("RelSceDT").ToString.Substring(11, 5)
                End If
                '担当グループコンボボックス
                .PropCmbTantoGrpCD.SelectedValue = .PropDtReleaseInfo.Rows(0).Item("TantoGrpCD")
                '担当IDテキストボックス
                .PropTxtRelTantoID.Text = .PropDtReleaseInfo.Rows(0).Item("RelTantoID")
                '担当氏名テキストボックス
                .PropTxtRelTantoNM.Text = .PropDtReleaseInfo.Rows(0).Item("RelTantoNM")
                'リリース着手日時datetimepicker
                If .PropDtReleaseInfo.Rows(0).Item("RelStDT").ToString.Equals("") Then
                    .PropDtpRelStDT.txtDate.Text = ""
                    .PropTxtRelStDT_HM.PropTxtTime.Text = ""
                Else
                    .PropDtpRelStDT.txtDate.Text = .PropDtReleaseInfo.Rows(0).Item("RelStDT").ToString.Substring(0, 10)
                    .PropTxtRelStDT_HM.PropTxtTime.Text = dataHBKF0201.PropDtReleaseInfo.Rows(0).Item("RelStDT").ToString.Substring(11, 5)
                End If

                'リリース終了日時datetimepicker
                If .PropDtReleaseInfo.Rows(0).Item("RelEdDT").ToString.Equals("") Then
                    .PropDtpRelEdDT.txtDate.Text = ""
                    .PropTxtRelEdDT_HM.PropTxtTime.Text = ""
                Else
                    .PropDtpRelEdDT.txtDate.Text = .PropDtReleaseInfo.Rows(0).Item("RelEdDT").ToString.Substring(0, 10)
                    .PropTxtRelEdDT_HM.PropTxtTime.Text = dataHBKF0201.PropDtReleaseInfo.Rows(0).Item("RelEdDT").ToString.Substring(11, 5)
                End If

                '関連ファイル情報スプレッド
                .PropVwRelationFileInfo.Sheets(0).DataSource = .PropDtRelFileInfo
                'タイトルテキストボックス
                .PropTxtTitle.Text = .PropDtReleaseInfo.Rows(0).Item("Title")
                '概要テキストボックス
                .PropTxtGaiyo.Text = .PropDtReleaseInfo.Rows(0).Item("Gaiyo")

                'ユーザ名の背景色を濃灰色にする
                With .PropVwRelationInfo.Sheets(0)
                    For i As Integer = 0 To dataHBKF0201.PropDtRelation.Rows.Count - 1
                        If .GetText(i, COL_RELATION_USERNM) = "" Then
                            .Cells(i, COL_RELATION_USERNM).BackColor = PropCellBackColorDARKGRAY
                        End If
                        'グループ名の背景色を濃灰色にする
                        If .GetText(i, COL_RELATION_GROUPNM) = "" Then
                            .Cells(i, COL_RELATION_GROUPNM).BackColor = PropCellBackColorDARKGRAY
                        End If
                    Next
                End With

                '担当履歴 
                If CreateTantoRireki(dataHBKF0201) = False Then
                    Return False
                End If

                '関連ファイルデータが無い場合、ボタン制御を行う
                If .PropVwRelationFileInfo.Sheets(0).RowCount > 0 Then
                    .PropBtnRelationFileOpen.Enabled = True
                    .PropBtnRelationFileDownLoad.Enabled = True
                Else
                    .PropBtnRelationFileOpen.Enabled = False
                    .PropBtnRelationFileDownLoad.Enabled = False
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
    ''' 【参照モード】基本情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhnForRef(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '編集モード基本情報タブデータ設定処理と同じ
            If SetDataToTabKhnForEdit(dataHBKF0201) = False Then
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
    ''' 【共通】コンボボックス作成処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のコンボボックスを作成する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateCmb(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKF0201

                'プロセスステータスコンボボックス作成
                If commonLogic.SetCmbBox(dataHBKF0201.PropDtStateMasta, dataHBKF0201.PropCmbProcessState, True, "", "") = False Then
                    Return False
                End If

                '通常・緊急コンボボックス作成
                If commonLogic.SetCmbBox(TujyoKinkyuKbn, .PropCmbTujyoKinkyuKbn) = False Then
                    Return False
                End If

                'ユーザー必要周知コンボボックス作成
                If commonLogic.SetCmbBox(UsrSyutiKbn, .PropCmbUsrSyutiKbn) = False Then
                    Return False
                End If

                '担当グループコンボボックス作成
                If commonLogic.SetCmbBox(.PropDtTantoGrpMasta, .PropCmbTantoGrpCD, True, "", "") = False Then
                    Return False
                End If

                '対象システムコンボボックス作成（この処理で対象システムのコンボボックスにブランク行の設定をする）
                Dim dummy As New ComboBoxEx
                dummy.PropIntStartCol = 2 'testで2を0にする
                If commonLogic.SetCmbBoxEx(.PropDtCIInfo, dummy, "cinmb", "txt", True, 0, "") = False Then
                    Return False
                End If
                dummy.Dispose()

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
    ''' 【共通】スプレッドセルタイプ作成処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のコンボボックスを作成する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateSpreadCtype(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKF0201

                '描画用オブジェクト生成
                Dim objtmp As New ComboBox
                Dim g As Graphics = objtmp.CreateGraphics()
                Dim intHosei As Integer = 3

                'リリース依頼受領システムセル用コンボボックス作成 
                Dim tmpLength2_1 As Integer = 0
                Dim tmpLength2_2 As Integer = 0
                Dim tmpLength2_3 As Integer = 0
                'For i As Integer = 0 To dataHBKF0201.PropDtCIInfo.Rows.Count - 1
                '    '設定した最大文字数を取得
                '    Dim strwk1 As String = dataHBKF0201.PropDtCIInfo.Rows(i).Item(1).ToString
                '    If tmpLength2_1 < commonLogic.LenB(strwk1) Then
                '        tmpLength2_1 = commonLogic.LenB(strwk1)
                '    End If
                '    Dim strwk2 As String = dataHBKF0201.PropDtCIInfo.Rows(i).Item(2).ToString
                '    If tmpLength2_2 < commonLogic.LenB(strwk2) Then
                '        tmpLength2_2 = commonLogic.LenB(strwk2)
                '    End If
                '    Dim strwk3 As String = dataHBKF0201.PropDtCIInfo.Rows(i).Item(3).ToString
                '    If tmpLength2_3 < commonLogic.LenB(strwk3) Then
                '        tmpLength2_3 = commonLogic.LenB(strwk3)
                '    End If
                'Next
                '[mod] 2012/09/07 y.ikushima 表示対応 START
                For i As Integer = 0 To dataHBKF0201.PropDtCIInfo.Rows.Count - 1
                    '設定した最大文字数を取得
                    Dim strwk1 As String = dataHBKF0201.PropDtCIInfo.Rows(i).Item(2).ToString
                    If tmpLength2_1 < commonLogic.LenB(strwk1) Then
                        tmpLength2_1 = commonLogic.LenB(strwk1)
                    End If
                    Dim strwk2 As String = dataHBKF0201.PropDtCIInfo.Rows(i).Item(3).ToString
                    If tmpLength2_2 < commonLogic.LenB(strwk2) Then
                        tmpLength2_2 = commonLogic.LenB(strwk2)
                    End If
                    Dim strwk3 As String = dataHBKF0201.PropDtCIInfo.Rows(i).Item(4).ToString
                    If tmpLength2_3 < commonLogic.LenB(strwk3) Then
                        tmpLength2_3 = commonLogic.LenB(strwk3)
                    End If
                Next
                Dim sf2 As SizeF = g.MeasureString(New String("0"c, tmpLength2_1 + tmpLength2_2 + tmpLength2_3 + (intHosei * 3)), dataHBKF0201.PropVwIrai.Font)
                '[mod] 2012/09/07 y.ikushima 表示対応 END

                Dim combosystem As New FarPoint.Win.Spread.CellType.MultiColumnComboBoxCellType
                With combosystem
                    .DataSourceList = dataHBKF0201.PropDtCIInfo
                    .ColumnEdit = 1
                    .DataColumn = 0
                    .ListResizeColumns = FarPoint.Win.Spread.CellType.ListResizeColumns.FitWidestItem
                    .ListBorderStyle = BorderStyle.FixedSingle
                    .ShowColumnHeaders = False
                    .ListWidth = sf2.Width
                    .MaxDrop = MaxDrop_systemnmb
                End With

                'Spread設定(データソース設定後にセルタイプを修正）
                dataHBKF0201.PropVwIrai.Sheets(0).Columns(COL_IRAI).CellType = combosystem
                dataHBKF0201.PropVwJissi.Sheets(0).Columns(COL_JISSI).CellType = combosystem

                'リソースを解放する
                g.Dispose()

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
    ''' 【共通】会議情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて会議情報タブデータを初期設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabMeeting(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKF0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToTabMeetingForNew(dataHBKF0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    '編集モード用設定
                    If SetDataToTabMeetingForEdit(dataHBKF0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then    '参照（ロック）モード

                    '参照モード用設定
                    If SetDataToTabMeetingForRef(dataHBKF0201) = False Then
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
    ''' 【新規登録モード】会議情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードで会議情報タブデータを初期設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabMeetingForNew(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKF0201

                '会議情報スプレッド
                .PropVwMeeting.DataSource = .PropDtMeeting

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
    ''' 【編集モード】会議情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで会議情報タブデータを初期設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabMeetingForEdit(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKF0201
                '会議情報スプレッド
                .PropVwMeeting.DataSource = .PropDtMeeting
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
    ''' 【参照モード】会議情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで会議情報タブデータを初期設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabMeetingForRef(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '編集モード会議情報タブデータ設定処理と同じ
            If SetDataToTabMeetingForEdit(dataHBKF0201) = False Then
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
    ''' 【共通】フリー入力タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFree(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKF0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToTabFreeForNew(dataHBKF0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    '編集モード用設定
                    If SetDataToTabFreeForEdit(dataHBKF0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then    '参照（ロック）モード

                    '参照モード用設定
                    If SetDataToTabFreeForRef(dataHBKF0201) = False Then
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
    ''' 【新規登録モード】フリー入力タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFreeForNew(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKF0201

                'フリーテキスト１～５テキストボックス
                .PropTxtBIko1.Text = ""
                .PropTxtBIko2.Text = ""
                .PropTxtBIko3.Text = ""
                .PropTxtBIko4.Text = ""
                .PropTxtBIko5.Text = ""

                'フリーフラグ１～５チェックボックス
                .PropChkFreeFlg1.Checked = False
                .PropChkFreeFlg2.Checked = False
                .PropChkFreeFlg3.Checked = False
                .PropChkFreeFlg4.Checked = False
                .PropChkFreeFlg5.Checked = False

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
    ''' 【編集モード】フリー入力タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFreeForEdit(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKF0201

                'フリーテキスト１～５テキストボックス
                .PropTxtBIko1.Text = .PropDtReleaseInfo.Rows(0).Item("BIko1")
                .PropTxtBIko2.Text = .PropDtReleaseInfo.Rows(0).Item("BIko2")
                .PropTxtBIko3.Text = .PropDtReleaseInfo.Rows(0).Item("BIko3")
                .PropTxtBIko4.Text = .PropDtReleaseInfo.Rows(0).Item("BIko4")
                .PropTxtBIko5.Text = .PropDtReleaseInfo.Rows(0).Item("BIko5")

                'フリーフラグ１～５チェックボックス
                If .PropDtReleaseInfo.Rows(0).Item("FreeFlg1") = FREE_FLG_ON Then
                    .PropChkFreeFlg1.Checked = True
                ElseIf .PropDtReleaseInfo.Rows(0).Item("FreeFlg1") = FREE_FLG_OFF Then
                    .PropChkFreeFlg1.Checked = False
                End If
                If .PropDtReleaseInfo.Rows(0).Item("FreeFlg2") = FREE_FLG_ON Then
                    .PropChkFreeFlg2.Checked = True
                ElseIf .PropDtReleaseInfo.Rows(0).Item("FreeFlg2") = FREE_FLG_OFF Then
                    .PropChkFreeFlg2.Checked = False
                End If
                If .PropDtReleaseInfo.Rows(0).Item("FreeFlg3") = FREE_FLG_ON Then
                    .PropChkFreeFlg3.Checked = True
                ElseIf .PropDtReleaseInfo.Rows(0).Item("FreeFlg3") = FREE_FLG_OFF Then
                    .PropChkFreeFlg3.Checked = False
                End If
                If .PropDtReleaseInfo.Rows(0).Item("FreeFlg4") = FREE_FLG_ON Then
                    .PropChkFreeFlg4.Checked = True
                ElseIf .PropDtReleaseInfo.Rows(0).Item("FreeFlg4") = FREE_FLG_OFF Then
                    .PropChkFreeFlg4.Checked = False
                End If
                If .PropDtReleaseInfo.Rows(0).Item("FreeFlg5") = FREE_FLG_ON Then
                    .PropChkFreeFlg5.Checked = True
                ElseIf .PropDtReleaseInfo.Rows(0).Item("FreeFlg5") = FREE_FLG_OFF Then
                    .PropChkFreeFlg5.Checked = False
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
    ''' 【参照モード】フリー入力タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFreeForRef(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '編集モードフリー入力タブデータ設定処理と同じ
            If SetDataToTabFreeForEdit(dataHBKF0201) = False Then
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
    ''' 【共通】スプレッド初期設定処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>各スプレッド（一覧）を初期設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetVwControl(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKF0201
                '依頼
                With .PropVwIrai.Sheets(0)
                    .DataAutoCellTypes = False
                    .DataAutoSizeColumns = False
                    .DataAutoHeadings = False
                    .Columns(COL_IRAI).DataField = "CINmb"
                    .Columns(COL_IRAI_REGDT).DataField = "RegDt"
                    .Columns(COL_IRAI_REGGRPCD).DataField = "RegGrpCD"
                    .Columns(COL_IRAI_REGID).DataField = "RegID"
                    .Columns(COL_IRAI_ENTRY).DataField = "EntryNmb"
                    .Columns(COL_IRAI_REGDT).Visible = False
                    .Columns(COL_IRAI_REGGRPCD).Visible = False
                    .Columns(COL_IRAI_REGID).Visible = False
                    .Columns(COL_IRAI_ENTRY).Visible = False
                End With

                '実施
                With .PropVwJissi.Sheets(0)
                    .DataAutoCellTypes = False
                    .DataAutoSizeColumns = False
                    .DataAutoHeadings = False
                    .Columns(COL_JISSI).DataField = "CINmb"
                    .Columns(COL_JISSI_REGDT).DataField = "RegDt"
                    .Columns(COL_JISSI_REGGRPCD).DataField = "RegGrpCD"
                    .Columns(COL_JISSI_REGID).DataField = "RegID"
                    .Columns(COL_JISSI_ENTRY).DataField = "EntryNmb"
                    .Columns(COL_JISSI_REGDT).Visible = False
                    .Columns(COL_JISSI_REGGRPCD).Visible = False
                    .Columns(COL_JISSI_REGID).Visible = False
                    .Columns(COL_JISSI_ENTRY).Visible = False
                End With

                '関連ファイル情報システム
                With .PropVwRelationFileInfo.Sheets(0)
                    .DataSource = dataHBKF0201.PropDtRelFileInfo
                    .Columns(COL_RELFILE_NAIYO).DataField = "FileNaiyo"

                    .Columns(COL_RELFILE_REGDT).DataField = "RegDT"
                    .Columns(COL_RELFILE_MNGNMB).DataField = "FileMngNmb"      'ファイル番号　※隠し列
                    .Columns(COL_RELFILE_PATH).DataField = "FilePath"          'ファイルパス　※隠し列

                    '[mod] 2012/09/07 y.ikushima 表示不具合対応START
                    .Columns(COL_RELFILE_MNGNMB).Visible = False
                    .Columns(COL_RELFILE_PATH).Visible = False
                    '[mod] 2012/09/07 y.ikushima 表示不具合対応END

                End With

                '会議情報スプレッド
                With .PropVwMeeting.Sheets(0)
                    .DataSource = dataHBKF0201.PropDtMeeting
                    .Columns(COL_MEETING_NMB).DataField = "MeetingNmb"
                    .Columns(COL_MEETING_JISISTDT).DataField = "JisiDT"
                    .Columns(COL_MEETING_RESULTKBN_NM).DataField = "ResultKbnNM"
                    .Columns(COL_MEETING_TITLE).DataField = "Title"

                    .Columns(COL_MEETING_RESULTKBN).DataField = "ResultKbn"
                    .Columns(COL_MEETING_REGDT).DataField = "RegDt"
                    .Columns(COL_MEETING_REGGRPCD).DataField = "RegGrpCD"
                    .Columns(COL_MEETING_REGID).DataField = "RegID"

                    '[mod] 2012/09/07 y.ikushima 表示不具合対応START
                    .Columns(COL_MEETING_RESULTKBN).Visible = False
                    .Columns(COL_MEETING_REGDT).Visible = False
                    .Columns(COL_MEETING_REGGRPCD).Visible = False
                    .Columns(COL_MEETING_REGID).Visible = False
                    '[mod] 2012/09/07 y.ikushima 表示不具合対応END
                End With

                '対応関係者情報
                With .PropVwRelationInfo.Sheets(0)
                    .DataSource = dataHBKF0201.PropDtRelation
                    .Columns(COL_RELATION_KBN).DataField = "RelationKbn"
                    .Columns(COL_RELATION_ID).DataField = "RelationID"
                    .Columns(COL_RELATION_GROUPNM).DataField = "GroupNM"
                    .Columns(COL_RELATION_USERNM).DataField = "HBKUsrNM"

                    .Columns(COL_RELATION_REGDT).DataField = "RegDt"
                    .Columns(COL_RELATION_REGGRPCD).DataField = "RegGrpCD"
                    .Columns(COL_RELATION_REGID).DataField = "RegID"

                    '[mod] 2012/09/07 y.ikushima 表示不具合対応START
                    .Columns(COL_RELATION_REGDT).Visible = False
                    .Columns(COL_RELATION_REGGRPCD).Visible = False
                    .Columns(COL_RELATION_REGID).Visible = False
                    '[mod] 2012/09/07 y.ikushima 表示不具合対応END

                End With

                'プロセスリンク情報
                With .PropVwProcessLinkInfo.Sheets(0)
                    .DataSource = dataHBKF0201.PropDtprocessLink
                    .Columns(COL_PLINK_KBN_NMR).DataField = "ProcessKbnNMR"
                    .Columns(COL_PLINK_NO).DataField = "MngNmb"
                    .Columns(COL_PLINK_KBN).DataField = "ProcessKbn"
                    .Columns(COL_PLINK_REGDT).DataField = "RegDt"
                    .Columns(COL_PLINK_REGGRPCD).DataField = "RegGrpCD"
                    .Columns(COL_PLINK_REGID).DataField = "RegID"

                    '[mod] 2012/09/07 y.ikushima 表示不具合対応START
                    .Columns(COL_PLINK_KBN).Visible = False
                    .Columns(COL_PLINK_REGDT).Visible = False
                    .Columns(COL_PLINK_REGGRPCD).Visible = False
                    .Columns(COL_PLINK_REGID).Visible = False
                    '[mod] 2012/09/07 y.ikushima 表示不具合対応END
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
    ''' 【編集モード】ロック解除チェック処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータのロックが解除されていないかチェックする
    ''' <para>作成情報：2012/08/31 t.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckUnlock(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnBeUnocked As Boolean = False       'ロックフラグ

        Try

            With dataHBKF0201

                'ロック解除チェック
                If CheckDataBeUnlocked(dataHBKF0201.PropIntRelNmb, dataHBKF0201.PropStrEdiTime, _
                                                      blnBeUnocked, dataHBKF0201.PropDtRelLock) = False Then
                    Return False
                End If

                'ロック解除されている場合、ロックフラグON
                If blnBeUnocked = True Then

                    .PropBlnBeLockedFlg = True

                Else

                    .PropBlnBeLockedFlg = False

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
    ''' D-1-1.ロック解除状況チェック処理
    ''' </summary>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <param name="strEdiTime_Bef">[IN]既に設定済の編集開始日時</param>
    ''' <param name="blnBeUnocked">[IN/OUT]ロック解除フラグ（True：ロック解除されている）</param>
    ''' <param name="dtLock">[IN/OUT]共通情報ロックデータ</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>指定された管理番号の共通情報のロック解除状況をチェックする。
    ''' <para>作成情報：2012/09/04 s.tsuruta
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function CheckDataBeUnlocked(ByVal intNmb As Integer, _
                                           ByVal strEdiTime_Bef As String, _
                                           ByRef blnBeUnocked As Boolean, _
                                           ByRef dtLock As DataTable) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '共通情報ロックテーブル項目格納用変数宣言
        Dim dtResult As New DataTable           'SELECT結果格納テーブル

        'ロック解除チェック用変数宣言
        Dim strEdiID As String = ""             '編集者ID
        Dim strEdiTime As String = ""           '編集開始日時
        Dim strSysTime As String                '現在日時（サーバー）
        Dim tsDiff As TimeSpan = Nothing        '編集開始日時と現在日時の差
        Dim tsUnlock As TimeSpan = Nothing      'ロック解除時間    

        '定数宣言
        Const DATE_FORMAT As String = "yyyy/MM/dd HH:mm:ss" '日付型フォーマット形式

        Try
            'ロック解除フラグ初期化
            blnBeUnocked = False

            '********************************
            '* 共通情報ロックテーブル取得
            '********************************
            If GetLockTb(intNmb, dtResult) = False Then
                Return False
            End If

            '********************************
            '* ロック解除チェック
            '********************************

            '現在日時を取得
            strSysTime = dtResult.Rows(0).Item("SysTime").ToString()

            '共通情報ロックデータが取得できた場合、チェックを行う
            If dtResult.Rows.Count > 0 AndAlso dtResult.Rows(0).Item("EdiID") <> "" Then

                '設定済の編集開始日時を取得
                strEdiTime = strEdiTime_Bef

                '編集開始日時がセットされている場合、現在日時と編集開始日時の差異がシステム管理マスタ.ロック解除時間以内かチェック
                If strEdiTime <> "" Then

                    'ロック時の編集開始日時と、現在ロックテーブルに登録されている編集開始日時が異なる場合、ロック解除されている
                    If Format(DateTime.Parse(strEdiTime), DATE_FORMAT) <> Format(DateTime.Parse(dtResult.Rows(0).Item("EdiTime")), DATE_FORMAT) Then
                        'ロック解除フラグON
                        blnBeUnocked = True
                    Else
                        '現在日時と編集開始日時の差を取得し、その差がロック解除時間を上回る場合はロック解除されている
                        tsDiff = New TimeSpan(DateTime.Parse(strSysTime).Subtract(DateTime.Parse(strEdiTime)).Ticks)
                        tsUnlock = TimeSpan.Parse(PropUnlockTime)
                        If tsDiff >= tsUnlock Then
                            'ロック解除フラグON
                            blnBeUnocked = True
                        End If
                    End If

                End If

            Else
                '共通情報ロックデータが取得できなかった場合

                'ロック解除フラグON
                blnBeUnocked = True

            End If

            '取得データを戻り値にセット
            dtLock = dtResult

            'ログ出力
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
        End Try

    End Function

    ''' <summary>
    '''  L-1-1-1.共通情報ロック情報取得処理
    ''' </summary>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <param name="dtLock">[IN/OUT]共通情報ロックデータ</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>指定された管理番号の共通情報ロックテーブルおよびサーバー日付を返す
    ''' <para>作成情報：2012/09/04 s.tsuruta
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function GetLockTb(ByVal intNmb As Integer, _
                                 ByRef dtLock As DataTable) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'DB接続用変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        'SQL変数宣言--------------------------------------

        Try
            'データ格納用テーブル初期化
            dtLock = New DataTable

            'コネクションを開く
            Cn.Open()

            '共通情報ロックテーブル、サーバー日付取得
            If sqlHBKF0201.SelectLock(Adapter, Cn, intNmb) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, OUTPUT_LOG_TITLE & "共通情報ロックテーブル取得", Nothing, Adapter.SelectCommand)

            'SQLを実行し、結果を取得
            Adapter.Fill(dtLock)

            '2行取得できた場合（ロック情報がある場合）、ロック情報にサーバー日付を不可
            If dtLock.Rows.Count > 1 Then
                'ロック情報にサーバー日付を設定
                dtLock.Rows(1).Item("SysTime") = dtLock.Rows(0).Item("SysTime")
                '1行目のデータを削除
                dtLock.Rows(0).Delete()
                '変更をコミット
                dtLock.AcceptChanges()
            End If

            'ログ出力
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
            dtLock.Dispose()
            Adapter.Dispose()
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【新規登録モード】データ新規登録処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をDBに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertNewData(ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Tsx As NpgsqlTransaction = Nothing    'トランザクション
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ
        Dim DtVwKankei As New DataTable

        Try
            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            '新規リリース番号、システム日付取得（SELECT）
            If SelectNewRelNmbAndSysDate(Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'リリース共通情報新規登録処理
            If InsertRelInfo(Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '担当履歴情報チェック
            If InsertTantoRireki(Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'リリース依頼受領システム新規登録処理
            If InsertRelIraiSystem(Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'リリース実施対象システム新規登録処理
            If InsertRelJisiSystem(Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '対応関係者情報新規登録（INSERT）
            If InsertKankei(Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'プロセスリンク新規登録（INSERT）
            If InsertRelPlink(Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '関連ファイル情報新規登録（INSERT）
            If InsertRelFile(Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '新規ログNo取得
            If GetNewLogNo(Adapter, Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If

                Return False
            End If

            'リリース共通情報ログテーブル登録
            If InsertRelInfoL(Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'リリース対象システムログ新規登録処理
            If InsertRelSystemL(Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '対応者情報ログテーブル登録
            If InsertRelKankeiL(Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'プロセスリンク(元)ログテーブル登録
            If InsertPLinkmotoL(Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'プロセスリンク(先)ログテーブル登録
            'If InsertPLinksakiL(Cn, dataHBKF0201) = False Then
            '    'ロールバック
            '    If Tsx IsNot Nothing Then
            '        Tsx.Rollback()
            '    End If
            '    Return False
            'End If

            '関連ファイルログテーブル登録
            If InsertRelFileL(Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'コミット
            Tsx.Commit()

            'コネクションを閉じる
            Cn.Close()

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
            Cn.Dispose()
            Tsx.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【新規登録モード】リリース共通情報　新規登録処理  
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でリリース共通情報テーブルを更新（Insert）する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRelInfo(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            With dataHBKF0201


                'リリース共通情報追加（insert）用SQLを作成
                If sqlHBKF0201.SetInsertRelInfoSql(Cmd, Cn, dataHBKF0201) = False Then
                    Return False
                End If

                'ログ出力
                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "リリース共通情報新規登録", Nothing, Cmd)

                'SQL実行
                Cmd.ExecuteNonQuery()


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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【新規登録】リリース依頼受領システム　新規登録処理  
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でリリースシステムテーブルを更新（Insert）する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRelIraiSystem(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            With dataHBKF0201

                'リリース実施情報の行数分繰り返し、更新処理を行う
                For i As Integer = 0 To dataHBKF0201.PropVwIrai.Sheets(0).RowCount - 1

                    '登録行作成
                    Dim row As DataRow = .PropDtRelIrai.NewRow
                    Dim dt As Date
                    If .PropVwIrai.Sheets(0).GetValue(i, COL_IRAI) <> 0 Then
                        'If .PropVwIrai.Sheets(0).GetValue(i, COL_IRAI) IsNot DBNull.Value And .PropVwIrai.Sheets(0).GetValue(i, COL_IRAI) IsNot Nothing Then

                        row.Item(0) = dataHBKF0201.PropVwIrai.Sheets(0).GetValue(i, COL_IRAI)
                        '入力値取得
                        Dim strProcessKbn As String = commonLogicHBK.ChangeNothingToStr(.PropVwIrai.Sheets(0).Cells(i, COL_IRAI), "").Trim()
                        Dim strRegDt As String = commonLogicHBK.ChangeNothingToStr(.PropVwIrai.Sheets(0).Cells(i, COL_IRAI_REGDT), "")
                        Dim strRegGrpCD As String = commonLogicHBK.ChangeNothingToStr(.PropVwIrai.Sheets(0).Cells(i, COL_IRAI_REGGRPCD), "").Trim()
                        Dim strRegID As String = commonLogicHBK.ChangeNothingToStr(.PropVwIrai.Sheets(0).Cells(i, COL_IRAI_REGID), "").Trim()

                        row.Item("CINmb") = dataHBKF0201.PropVwIrai.Sheets(0).GetValue(i, COL_IRAI)
                        '[mod] 2012/09/07 y.ikushima 表示対応 START
                        If Date.TryParse(strRegDt, dt) = True Then
                            row.Item("RegDT") = strRegDt
                        Else
                            row.Item("RegDT") = DBNull.Value
                        End If
                        '[mod] 2012/09/07 y.ikushima 表示対応 END
                        row.Item("RegGrpCD") = strRegGrpCD
                        row.Item("RegID") = strRegID

                        '作成した行をデータクラスにセット
                        .PropRowReg = row

                        'リリース依頼受領システム情報（INSERT）用SQLを作成
                        If sqlHBKF0201.SetInsertRelIraiSystemSql(Cmd, Cn, dataHBKF0201) = False Then
                            Return False
                        End If

                        'ログ出力
                        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "リリース依頼受領システム情報新規登録", Nothing, Cmd)

                        'SQL実行
                        Cmd.ExecuteNonQuery()

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
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【新規登録】リリース実施対象システム　新規登録処理  
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でリリースシステムテーブルを更新（Insert）する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRelJisiSystem(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            With dataHBKF0201

                'リリース実施情報の行数分繰り返し、更新処理を行う
                For i As Integer = 0 To .PropVwJissi.Sheets(0).RowCount - 1



                    '登録行作成
                    Dim row As DataRow = .PropDtRelJissi.NewRow

                    'If .PropVwJissi.Sheets(0).GetValue(i, COL_IRAI) IsNot DBNull.Value And .PropVwJissi.Sheets(0).GetValue(i, COL_IRAI) IsNot Nothing Then
                    If .PropVwJissi.Sheets(0).GetValue(i, COL_IRAI) <> 0 Then

                        row.Item(0) = dataHBKF0201.PropVwJissi.Sheets(0).GetValue(i, COL_IRAI)

                        '作成した行をデータクラスにセット
                        .PropRowReg = row

                        'リリース実施対象システム情報追加（insert）用SQLを作成
                        If sqlHBKF0201.SetInsertRelJisiSystemSql(Cmd, Cn, dataHBKF0201) = False Then
                            Return False
                        End If

                        'ログ出力
                        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "リリース実施対象システム情報新規登録", Nothing, Cmd)

                        'SQL実行
                        Cmd.ExecuteNonQuery()

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
        Finally
            Cmd.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【新規登録／編集モード】新規リリース番号取得処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番したリリース番号を取得（SELECT）する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectNewRelNmbAndSysDate(ByVal Cn As NpgsqlConnection, _
                                              ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtResult As New DataTable

        Try
            '新規INC番号取得（SELECT）用SQLを作成
            If sqlHBKF0201.SetSelectNewRelNmbAndSysDateSql(Adapter, Cn, dataHBKF0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規リリース番号、システム日付取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            'データが取得できた場合、データクラスに取得データをセット
            If dtResult.Rows.Count > 0 Then
                dataHBKF0201.PropIntRelNmb = dtResult.Rows(0).Item("RelNmb")      '新規inc番号
                dataHBKF0201.PropDtmSysDate = dtResult.Rows(0).Item("SysDate")  'サーバー日付
            Else
                '取得できなかったときはエラー
                puErrMsg = F0201_E010
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
    ''' 【新規登録／編集モード】プロセスリンク登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をプロセスリンク情報テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRelPlink(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim Cmd As New NpgsqlCommand            'SQLコマンド
        Dim row As DataRow
        Dim cnt As Integer

        Try
            With dataHBKF0201

                'データテーブルを取得
                .PropDtprocessLink = DirectCast(.PropVwProcessLinkInfo.Sheets(0).DataSource, DataTable)

                If .PropDtprocessLink IsNot Nothing Then

                    If .PropDtprocessLink.Rows.Count > 0 Then

                        'データ数分繰り返し、登録処理を行う 
                        For i As Integer = 0 To .PropDtprocessLink.Rows.Count - 1

                            row = .PropDtprocessLink.Rows(i)

                            .PropRowReg = row


                            'データの追加／削除状況に応じて新規登録／削除処理を行う
                            If row.RowState = DataRowState.Added Then           '追加時

                                '登録順カウンタ
                                cnt += 1

                                '新規登録
                                If sqlHBKF0201.InsertPLinkMoto(Cmd, Cn, dataHBKF0201, cnt) = False Then
                                    Return False
                                End If

                                'SQL実行
                                Cmd.ExecuteNonQuery()

                                'ログ出力
                                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "プロセスリンク(元)情報新規登録", Nothing, Cmd)



                            ElseIf row.RowState = DataRowState.Deleted Then     '削除時

                                '削除
                                If sqlHBKF0201.DeletePLinkMoto(Cmd, Cn, dataHBKF0201) = False Then
                                    Return False
                                End If

                                'SQL実行
                                Cmd.ExecuteNonQuery()

                                'ログ出力
                                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "プロセスリンク(元)情報削除", Nothing, Cmd)

                                '削除
                                If sqlHBKF0201.DeletePLinkSaki(Cmd, Cn, dataHBKF0201) = False Then
                                    Return False
                                End If

                                'SQL実行
                                Cmd.ExecuteNonQuery()

                                'ログ出力
                                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "プロセスリンク(先)情報削除", Nothing, Cmd)

                            End If


                            '行の変更をコミット
                            'row.AcceptChanges()

                        Next

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
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【新規登録／編集モード】リリース関連ファイル新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容を関連ファイルテーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRelFile(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            With dataHBKF0201

                '最新のファイル情報データテーブルを取得
                dataHBKF0201.PropDtRelFileInfo = DirectCast(.PropVwRelationFileInfo.Sheets(0).DataSource, DataTable)

                If .PropDtRelFileInfo IsNot Nothing Then

                    '関連ファイルアップロード／登録
                    Dim aryStrNewDirPath As New ArrayList
                    If commonLogicHBK.UploadAndRegFile(Adapter, Cn, _
                                                    .PropIntRelNmb, _
                                                    .PropDtRelFileInfo, _
                                                    .PropDtmSysDate, _
                                                    UPLOAD_FILE_RELEASE, _
                                                    aryStrNewDirPath) = False Then
                        Return False
                    End If

                End If

            End With

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "関係ファイル登録", Nothing, Cmd)


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
    ''' 【新規登録／編集モード】ログ情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴情報を各ログテーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRireki(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter    'アダプタ
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try




            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'ロールバック
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】新規ログNo取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapter</param>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番したログNoを取得する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetNewLogNo(ByVal Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dLogNo As New DataTable         'ログNo格納用テーブル

        Try

            'SQLを作成
            If sqlHBKF0201.SetSelectNewRirekiNoSql(Adapter, Cn, dataHBKF0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規ログNo取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dLogNo)

            'データが取得できた場合、データクラスにログNoをセット
            If dLogNo.Rows.Count > 0 Then
                dataHBKF0201.PropIntLogNo = dLogNo.Rows(0).Item("LogNo")
            Else
                '取得できなかった場合はエラー
                puErrMsg = F0201_E010
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
    ''' 【共通】新規ログNo（会議用）取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapter</param>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番したログNoを取得する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetNewMeetingRirekiNo(ByVal Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dLogNo As New DataTable         'ログNo格納用テーブル

        Try

            'SQLを作成
            If sqlHBKF0201.SetSelectNewMeetingRirekiNoSql(Adapter, Cn, dataHBKF0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規ログNo（会議用）取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dLogNo)

            'データが取得できた場合、データクラスにログNoをセット
            If dLogNo.Rows.Count > 0 Then
                dataHBKF0201.PropIntLogNoSub = dLogNo.Rows(0).Item("LogNo")
            Else
                '取得できなかった場合はエラー
                puErrMsg = F0201_E010
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
    ''' 【共通】リリース共通情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>リリース共通情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRelInfoL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKF0201.SetInsertRelInfoLSql(Cmd, Cn, dataHBKF0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "リリース共通情報ログ新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'ロールバック
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【新規登録】リリース対象システムログ　新規登録処理  
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でリリース対象システムログテーブルを新規登録（Insert）する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRelSystemL(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try


            'リリース対象システムログ情報追加（INSERT）用SQLを作成
            If sqlHBKF0201.SetInsertRelSystemLSql(Cmd, Cn, dataHBKF0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "リリース対象システムログ情報新規登録", Nothing, Cmd)

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
    ''' 【共通】対応関係情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対応関係情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRelKankeiL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKF0201.SetInsertRelKankeiLSql(Cmd, Cn, dataHBKF0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対応関係情報ログ新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'ロールバック
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【共通】プロセスリンク情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンク情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertPLinkmotoL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKF0201.SetInsertPLinkmotoLSql(Cmd, Cn, dataHBKF0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "プロセスリンク(元)情報ログ新規登録", Nothing, Cmd)

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
    ''' 【共通】関連ファイル情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関連ファイル情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRelFileL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKF0201.SetInsertRelFileLSql(Cmd, Cn, dataHBKF0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "関連ファイル情報ログ新規登録", Nothing, Cmd)

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
    ''' .【共通】新規ログNo（会議用）取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapter</param>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番したログNoを取得する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetNewMeetingLogNo(ByVal Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dLogNo As New DataTable         'ログNo格納用テーブル

        Try

            'SQLを作成
            If sqlHBKF0201.SetSelectNewMeetingLogNoSql(Adapter, Cn, dataHBKF0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規ログNo（会議用）取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dLogNo)

            'データが取得できた場合、データクラスにログNoをセット
            If dLogNo.Rows.Count > 0 Then
                dataHBKF0201.PropIntLogNoSub = dLogNo.Rows(0).Item("LogNo")
            Else
                '取得できなかった場合はエラー
                puErrMsg = F0201_E010
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
    ''' .【共通】会議情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InserMeetingL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKF0201.SetInsertMeetingLSql(Cmd, Cn, dataHBKF0201) = False Then
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
    '''【共通】会議結果情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議結果情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertMtgResultL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKF0201.SetInsertMtgResultLSql(Cmd, Cn, dataHBKF0201) = False Then
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
    '''【共通】会議出席者情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議結果情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInsertMtgAttendL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKF0201.SetInsertMtgAttendLSql(Cmd, Cn, dataHBKF0201) = False Then
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
    '''【共通】会議関連ファイル情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議結果情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInsertMtgFileL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKF0201.SetInsertMtgFileLSql(Cmd, Cn, dataHBKF0201) = False Then
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
    ''' 【編集／履歴モード】データ更新処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をDBに新規登録（DELETE/INSERT,UPDATE）する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateData(ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter()    'アダプタ
        Dim Tsx As NpgsqlTransaction = Nothing    'トランザクション

        Dim DtVwKankei As New DataTable       'スプレッドデータ一時保存用

        Try
            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            'システム日付取得
            If SelectSysDate(Adapter, Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'リリース共通情報更新（UPDATE）
            If UpdateRelInfo(Tsx, Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '担当履歴情報チェック
            If InsertTantoRireki(Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'リリース対象システム 削除（DELETE）
            If DeleteSystem(Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'リリース依頼受領システム新規登録処理
            If InsertRelIraiSystem(Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If


            'リリース実施対象システム新規登録処理
            If InsertRelJisiSystem(Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '対応関係者情報 削除（DELETE）
            If Deletekankei(Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '対応関係者情報新規登録（INSERT）
            If InsertKankei(Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'プロセスリンク新規登録（DELETE/INSERT）
            If InsertRelPlink(Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '関連ファイル情報登録（DELETE/INSERT）
            If InsertRelFile(Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'B-2-8会議結果情報 削除（DELETE）
            If DeleteMtgResult(Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If
            'B-2-9会議結果情報新規登録（INSERT）
            If InsertMtgResult(Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '新規ログNo取得
            If GetNewLogNo(Adapter, Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If

                Return False
            End If

            'リリース共通情報ログテーブル登録
            If InsertRelInfoL(Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'リリース対象システムログ新規登録処理
            If InsertRelSystemL(Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '対応者情報ログテーブル登録
            If InsertRelKankeiL(Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'プロセスリンク(元)ログテーブル登録
            If InsertPLinkmotoL(Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '関連ファイルログテーブル登録
            If InsertRelFileL(Cn, dataHBKF0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            For i As Integer = 0 To dataHBKF0201.PropVwMeeting.Sheets(0).Rows.Count - 1
                '会議番号
                dataHBKF0201.PropIntMeetingNmb = dataHBKF0201.PropVwMeeting.Sheets(0).GetText(i, COL_MEETING_NMB)

                '新規ログNo(会議用)取得
                If GetNewMeetingLogNo(Adapter, Cn, dataHBKF0201) = False Then
                    'ロールバック
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    Return False
                End If

                '会議情報ログテーブル登録
                If InserMeetingL(Cn, dataHBKF0201) = False Then
                    'ロールバック
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    Return False
                End If

                '会議結果ログテーブル登録
                If InsertMtgResultL(Cn, dataHBKF0201) = False Then
                    'ロールバック
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    Return False
                End If

                '会議出席者ログテーブル登録
                If SetInsertMtgAttendL(Cn, dataHBKF0201) = False Then
                    'ロールバック
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    Return False
                End If

                '会議関連ファイルログテーブル登録
                If SetInsertMtgFileL(Cn, dataHBKF0201) = False Then
                    'ロールバック
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    Return False
                End If
            Next



            'コミット
            Tsx.Commit()

            'コネクションを閉じる
            Cn.Close()

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
            Tsx.Dispose()
            Cn.Dispose()
        End Try

    End Function



    ''' <summary>
    ''' 【新規登録／編集モード】関係者情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容を関係者情報テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertKankei(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド
        Dim blnAddFlg As Boolean = True
        Dim DtVwKankei As New DataTable       'スプレッドデータ一時保存用

        Try

            With dataHBKF0201

                'スプレッドのデータソースを取得
                DtVwKankei = .PropVwRelationInfo.DataSource
                DtVwKankei.AcceptChanges()

                '★新規登録時のみ
                If .PropStrProcMode = PROCMODE_NEW Then
                    'ログインユーザのグループがあるかチェック
                    For i As Integer = 0 To DtVwKankei.Rows.Count - 1
                        If DtVwKankei.Rows(i).Item("RelationID").Equals(PropWorkGroupCD) Then
                            blnAddFlg = False
                        End If
                    Next
                    'ない場合追加
                    If blnAddFlg = True Then
                        Dim row As DataRow = DtVwKankei.NewRow
                        row.Item("RelationKbn") = KBN_GROUP
                        row.Item("RelationID") = PropWorkGroupCD
                        DtVwKankei.Rows.Add(row)
                    End If
                End If

                '取得した関係テーブルがあればチェックする
                If dataHBKF0201.PropDtIrai IsNot Nothing Then
                    For i As Integer = 0 To .PropDtIrai.Rows.Count - 1

                        '追加フラグ初期化
                        blnAddFlg = True

                        '★対象システム更新フラグがある場合
                        If .PropStrProcMode = PROCMODE_NEW OrElse .PropDtIrai.Rows(i).Item("UpdateFlg").Equals(True) Then

                            '関係テーブルのグループがあるかチェック
                            If .PropDtIrai.Rows(i).Item("relationkbn").Equals(KBN_GROUP) Then
                                For j As Integer = 0 To DtVwKankei.Rows.Count - 1
                                    If DtVwKankei.Rows(j).Item("relationkbn") = KBN_GROUP Then
                                        If DtVwKankei.Rows(j).Item("RelationID").Equals(.PropDtIrai.Rows(i).Item("RelationID")) Then
                                            blnAddFlg = False
                                            Exit For
                                        End If
                                    End If
                                Next

                                'ない場合追加
                                If blnAddFlg = True Then
                                    Dim row As DataRow = DtVwKankei.NewRow
                                    row.Item("RelationKbn") = KBN_GROUP
                                    row.Item("RelationID") = .PropDtIrai.Rows(i).Item("RelationID")
                                    DtVwKankei.Rows.Add(row)
                                End If

                            ElseIf .PropDtIrai.Rows(i).Item("relationkbn").Equals(KBN_USER) Then
                                '関係テーブルのユーザがあるかチェック
                                For j As Integer = 0 To DtVwKankei.Rows.Count - 1
                                    If DtVwKankei.Rows(j).Item("relationkbn") = KBN_USER Then
                                        If DtVwKankei.Rows(j).Item("RelationID").Equals(.PropDtIrai.Rows(i).Item("RelationID")) Then
                                            blnAddFlg = False
                                            Exit For
                                        End If
                                    End If
                                Next

                                'ない場合追加
                                If blnAddFlg = True Then
                                    Dim row As DataRow = DtVwKankei.NewRow
                                    row.Item("RelationKbn") = KBN_USER
                                    row.Item("RelationID") = .PropDtIrai.Rows(i).Item("RelationID")
                                    DtVwKankei.Rows.Add(row)
                                End If
                            End If
                        End If
                    Next

                End If


                '修正した関係者のテーブルにて
                For i As Integer = 0 To DtVwKankei.Rows.Count - 1

                    '登録行作成
                    Dim row As DataRow = DtVwKankei.NewRow
                    row.Item("RelationKbn") = DtVwKankei.Rows(i).Item(0)        'G,U(KBN_GROUP,KBN_USER)
                    row.Item("RelationID") = DtVwKankei.Rows(i).Item(1)         '3ケタ,7ケタ
                    row.Item("RegDt") = DtVwKankei.Rows(i).Item(4)              '登録日時
                    row.Item("RegGrpCD") = DtVwKankei.Rows(i).Item(5)           '登録者グループCD
                    row.Item("RegID") = DtVwKankei.Rows(i).Item(6)              '登録者ID


                    '作成した行をデータクラスにセット
                    .PropRowReg = row

                    '関係者情報新規登録（INSERT）用SQLを作成
                    If sqlHBKF0201.SetInsertRelKankeiSql(Cmd, Cn, dataHBKF0201) = False Then
                        Return False
                    End If

                    'ログ出力
                    commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "関係者情報新規登録", Nothing, Cmd)

                    'SQL実行
                    Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】リリース対象システム情報　削除処理  
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容で対応関係者情報テーブルを削除（delete）する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DeleteSystem(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            '共通情報更新（UPDATE）用SQLを作成
            If sqlHBKF0201.SetDeleteRelSystemSql(Cmd, Cn, dataHBKF0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "リリース対象システム物理削除", Nothing, Cmd)

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
    ''' 【編集モード】対応関係者情報　削除処理  
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容で対応関係者情報テーブルを削除（delete）する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function Deletekankei(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            '共通情報更新（UPDATE）用SQLを作成
            If sqlHBKF0201.SetDeleteRelkankeiSql(Cmd, Cn, dataHBKF0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対応関係者情報物理削除", Nothing, Cmd)

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
    ''' 【編集モード】リリース共通情報 更新処理
    ''' </summary>
    ''' <param name="Tsx">[IN/OUT]NpgsqlTransaction</param>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でリリース共通情報テーブルを更新（UPDATE）する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateRelInfo(ByRef Tsx As NpgsqlTransaction, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'リリース共通情報更新（UPDATE）用SQLを作成
            If sqlHBKF0201.SetUpdateRelInfoSql(Cmd, Cn, dataHBKF0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "リリース共通情報更新", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function




    ''' <summary>
    ''' 【編集モード】ロック解除処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータのロックを解除する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UnlockData(ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'リリース共通情報ロック解除（DELETE）
            If UnlockRelInfo(dataHBKF0201.PropIntRelNmb) = False Then
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
    ''' 【編集モード】サーバー日付取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>更新用のサーバー日付を取得する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectSysDate(ByRef Adapter As NpgsqlDataAdapter, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSysDate As New DataTable         '履歴番号格納用テーブル

        Try
            'SQLを作成
            If sqlHBKF0201.SetSelectSysDateSql(Adapter, Cn, dataHBKF0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サーバー日付取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtSysDate)

            'データが取得できた場合、データクラスに履歴番号をセット
            If dtSysDate.Rows.Count > 0 Then
                dataHBKF0201.PropDtmSysDate = dtSysDate.Rows(0).Item("SysDate")
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
    ''' 【共通】エラー時コントロール非活性処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録系ボタンを非活性にする
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetUnabledWhenError(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKF0201

                '登録系ボタンコントロールを非活性にする
                .PropBtnReg.Enabled = False                 '登録ボタン

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
    ''' 登録時入力チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録項目の入力チェックを行う
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckInputValueMain(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '入力チェック処理
        If CheckInputValue(dataHBKF0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】対応関係者情報グループ追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者情報一覧にサブ検索画面から取得したグループデータを設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetGroupToVwRelationMain(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'グループデータ設定処理
        If SetGroupToVwRelation(dataHBKF0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】対応関係者情報グループ設定処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対応関係者情報一覧にサブ検索で選択されたグループを設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetGroupToVwRelation(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intNewRowNo As Integer          '新規追加行番号
        Dim blnAddFlg As Boolean = True     '追加フラグ

        Try
            With dataHBKF0201

                'サブ検索画面で1件以上選択された場合に値を設定
                If .PropDtResultSub IsNot Nothing Then

                    '選択データ数分繰り返し、関係者情報一覧に行を追加する
                    For i As Integer = 0 To .PropDtResultSub.Rows.Count - 1

                        '追加フラグ初期化
                        blnAddFlg = True

                        'グループが既に設定済でない場合のみ追加
                        For j As Integer = 0 To .PropVwRelationInfo.Sheets(0).RowCount - 1

                            '既に設定済みの場合は追加フラグをOFFにして処理を抜ける
                            If .PropDtResultSub.Rows(i).Item("グループCD") = _
                                .PropVwRelationInfo.Sheets(0).Cells(j, COL_RELATION_ID).Value Then
                                blnAddFlg = False
                                Exit For
                            End If

                        Next

                        '追加フラグがONの場合のみ追加処理を行う
                        If blnAddFlg = True Then

                            '追加行番号取得
                            intNewRowNo = .PropVwRelationInfo.Sheets(0).Rows.Count

                            '新規行追加
                            .PropVwRelationInfo.Sheets(0).Rows.Add(intNewRowNo, 1)

                            'サブ検索画面での選択値を設定
                            .PropVwRelationInfo.Sheets(0).Cells(intNewRowNo, COL_RELATION_KBN).Value = KBN_GROUP      '区分：グループ
                            .PropVwRelationInfo.Sheets(0).Cells(intNewRowNo, COL_RELATION_ID).Value = _
                                .PropDtResultSub.Rows(i).Item("グループCD")                                       'ID
                            .PropVwRelationInfo.Sheets(0).Cells(intNewRowNo, COL_RELATION_GROUPNM).Value = _
                                .PropDtResultSub.Rows(i).Item("グループ名")                                       'グループ名

                            'ユーザ名の背景色を濃灰色にする
                            .PropVwRelationInfo.Sheets(0).Cells(intNewRowNo, COL_RELATION_USERNM).BackColor = PropCellBackColorDARKGRAY

                        End If

                    Next

                    '最終追加行にフォーカスをセット
                    If commonLogicHBK.SetFocusOnVwRow(.PropVwRelationInfo, _
                                                      0, .PropVwRelationInfo.Sheets(0).RowCount, 0, _
                                                      1, .PropVwRelationInfo.Sheets(0).ColumnCount) = False Then
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
    ''' 【共通】対応関係者情報ユーザー追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対応関係者情報一覧にサブ検索画面から取得したユーザーデータを設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetUserToVwRelationMain(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ユーザーデータ設定処理
        If SetUserToVwRelation(dataHBKF0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】対応関係者情報ユーザー設定処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対応関係者情報一覧にサブ検索で選択されたユーザーを設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetUserToVwRelation(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intNewRowNo As Integer          '新規追加行番号
        Dim blnAddFlg As Boolean = True     '追加フラグ


        Try
            With dataHBKF0201

                'サブ検索画面で1件以上選択された場合に値を設定
                If .PropDtResultSub IsNot Nothing Then

                    '選択データ数分繰り返し、関係者情報一覧に行を追加する
                    For i As Integer = 0 To .PropDtResultSub.Rows.Count - 1

                        '追加フラグ初期化
                        blnAddFlg = True

                        'ユーザーが既に設定済でない場合のみ追加
                        For j As Integer = 0 To .PropVwRelationInfo.Sheets(0).RowCount - 1

                            '既に設定済みの場合は追加フラグをOFFにして処理を抜ける
                            If .PropDtResultSub.Rows(i).Item("ユーザーID") = _
                                .PropVwRelationInfo.Sheets(0).Cells(j, COL_RELATION_ID).Value Then
                                blnAddFlg = False
                                Exit For
                            End If

                        Next

                        '追加フラグがONの場合のみ追加処理を行う
                        If blnAddFlg = True Then

                            '追加行番号取得
                            intNewRowNo = .PropVwRelationInfo.Sheets(0).Rows.Count

                            '新規行追加
                            .PropVwRelationInfo.Sheets(0).Rows.Add(intNewRowNo, 1)

                            'サブ検索画面での選択値を設定
                            .PropVwRelationInfo.Sheets(0).Cells(intNewRowNo, COL_RELATION_KBN).Value = KBN_USER       '区分：ユーザー
                            .PropVwRelationInfo.Sheets(0).Cells(intNewRowNo, COL_RELATION_ID).Value = _
                                .PropDtResultSub.Rows(i).Item("ユーザーID")                                       'ID
                            '.PropVwRelationInfo.Sheets(0).Cells(intNewRowNo, COL_RELATION_GROUPNM).Value = _
                            '    .PropDtResultSub.Rows(i).Item("グループ名")                                       'グループ名
                            .PropVwRelationInfo.Sheets(0).Cells(intNewRowNo, COL_RELATION_USERNM).Value = _
                                .PropDtResultSub.Rows(i).Item("ユーザー氏名")                                     'ユーザー名

                            'グループ名の背景色を濃灰色にする
                            .PropVwRelationInfo.Sheets(0).Cells(intNewRowNo, COL_RELATION_GROUPNM).BackColor = PropCellBackColorDARKGRAY

                        End If

                    Next

                    '最終追加行にフォーカスをセット
                    If commonLogicHBK.SetFocusOnVwRow(.PropVwRelationInfo, _
                                                      0, .PropVwRelationInfo.Sheets(0).RowCount, 0, _
                                                      1, .PropVwRelationInfo.Sheets(0).ColumnCount) = False Then
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
    ''' 対応関係者情報行削除時メイン処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対応関係者情報一覧の選択行を削除する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowRelationMain(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowRelation(dataHBKF0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】対応関係者情報選択行削除処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対応関係者情報の選択行を削除する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowRelation(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号
        Dim blnAddFlg As Boolean = True
        Try
            With dataHBKF0201.PropVwRelationInfo.Sheets(0)

                '選択開始行、終了行取得
                If .Models.Selection.AnchorRow < .Models.Selection.LeadRow Then
                    intSelectedRowFrom = .Models.Selection.AnchorRow
                    intSelectedRowTo = .Models.Selection.LeadRow
                Else
                    intSelectedRowTo = .Models.Selection.AnchorRow
                    intSelectedRowFrom = .Models.Selection.LeadRow
                End If

                '一覧に行があり、かつ選択行番号が最大行数未満の場合削除処理を行う
                If .RowCount > 0 AndAlso intSelectedRowFrom < .RowCount AndAlso intSelectedRowTo < .RowCount Then

                    '開始行から終了行まで選択行を削除する（逆回し）
                    For i As Integer = intSelectedRowTo To intSelectedRowFrom Step -1
                        '初期化
                        blnAddFlg = True

                        '★削除対象がログイン時のグループだった場合
                        If .GetText(i, COL_RELATION_KBN) = KBN_GROUP Then
                            If .GetText(i, COL_RELATION_ID).Equals(PropWorkGroupCD) Then
                                'ログインユーザのIDがあるかチェック
                                For j As Integer = 0 To .Rows.Count - 1
                                    If .GetText(j, COL_RELATION_KBN).Equals(KBN_USER) AndAlso _
                                        .GetText(j, COL_RELATION_ID).Equals(PropUserId) Then
                                        blnAddFlg = False
                                    End If
                                Next
                                'ない場合
                                If blnAddFlg = True Then
                                    'エラーメッセージ設定
                                    puErrMsg = F0201_E008
                                    Return False
                                End If
                            End If
                        End If

                        '★削除対象がログイン時のユーザーだった場合
                        If .GetText(i, COL_RELATION_KBN) = KBN_USER Then
                            If .GetText(i, COL_RELATION_ID).Equals(PropUserId) Then
                                'ログインユーザのグループがあるかチェック
                                For j As Integer = 0 To .Rows.Count - 1
                                    If .GetText(j, COL_RELATION_KBN).Equals(KBN_GROUP) AndAlso _
                                        .GetText(j, COL_RELATION_ID).Equals(PropWorkGroupCD) Then
                                        blnAddFlg = False
                                    End If
                                Next
                                'ない場合
                                If blnAddFlg = True Then
                                    'エラーメッセージ設定
                                    puErrMsg = F0201_E007
                                    Return False
                                End If
                            End If
                        End If

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
    ''' プロセスリンク行追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンク一覧に空行を1行追加する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function AddRowpLinkMain(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '空行追加処理
        If AddRowPlink(dataHBKF0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】プロセスリンク空行追加処理
    ''' </summary>
    ''' <param name="DataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンクに空行を1行追加する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function AddRowPlink(ByRef DataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intNewRowNo As Integer          '新規追加行番号
        Dim blnAddFlg As Boolean = True     '追加フラグ

        Try
            With DataHBKF0201

                'サブ検索画面で1件以上選択された場合に値を設定
                If .PropDtResultSub IsNot Nothing Then

                    '選択データ数分繰り返し、関係者情報一覧に行を追加する
                    For i As Integer = 0 To .PropDtResultSub.Rows.Count - 1

                        '追加フラグ初期化
                        blnAddFlg = True

                        '番号が既に設定済でない場合のみ追加
                        For j As Integer = 0 To .PropVwProcessLinkInfo.Sheets(0).RowCount - 1

                            '既に設定済みの場合は追加フラグをOFFにして処理を抜ける
                            If .PropDtResultSub.Rows(i).Item("mngnmb") = _
                                .PropVwProcessLinkInfo.Sheets(0).Cells(j, COL_PLINK_NO).Value Then
                                blnAddFlg = False
                                Exit For
                            End If

                        Next

                        '追加フラグがONの場合のみ追加処理を行う
                        If blnAddFlg = True Then

                            '追加行番号取得
                            intNewRowNo = .PropVwProcessLinkInfo.Sheets(0).Rows.Count

                            '新規行追加
                            .PropVwProcessLinkInfo.Sheets(0).Rows.Add(intNewRowNo, 1)

                            'サブ検索画面での選択値を設定

                            '取得した区分を略名で表示
                            Dim setKbn As String = ""
                            Select Case .PropDtResultSub.Rows(i).Item("processnm")
                                Case PROCESS_TYPE_INCIDENT_NAME
                                    setKbn = PROCESS_TYPE_INCIDENT_NAME_R
                                Case PROCESS_TYPE_QUESTION_NAME
                                    setKbn = PROCESS_TYPE_QUESTION_NAME_R
                                Case PROCESS_TYPE_CHANGE_NAME
                                    setKbn = PROCESS_TYPE_CHANGE_NAME_R
                                Case PROCESS_TYPE_RELEASE_NAME
                                    setKbn = PROCESS_TYPE_RELEASE_NAME_R
                            End Select

                            .PropVwProcessLinkInfo.Sheets(0).Cells(intNewRowNo, COL_PLINK_KBN_NMR).Value = _
                               setKbn                                                                                   '区分(略名）
                            .PropVwProcessLinkInfo.Sheets(0).Cells(intNewRowNo, COL_PLINK_NO).Value = _
                                .PropDtResultSub.Rows(i).Item("mngnmb")                                                 '番号
                            .PropVwProcessLinkInfo.Sheets(0).Cells(intNewRowNo, COL_PLINK_KBN).Value = _
                                .PropDtResultSub.Rows(i).Item("processkbn")                                             '区分CD


                        End If

                    Next

                    '最終追加行にフォーカスをセット
                    If commonLogicHBK.SetFocusOnVwRow(.PropVwProcessLinkInfo, _
                                                      0, .PropVwProcessLinkInfo.Sheets(0).RowCount, 0, _
                                                      1, .PropVwProcessLinkInfo.Sheets(0).ColumnCount) = False Then
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
    ''' プロセスリンク行削除時メイン処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンク一覧の選択行を削除する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowpLinkMain(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowplink(dataHBKF0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】プロセスリンク選択行削除処理
    ''' </summary>
    ''' <param name="DataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンクの選択行を削除（Remove）する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowplink(ByRef DataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号

        Try
            With DataHBKF0201.PropVwProcessLinkInfo.Sheets(0)

                '選択開始行、終了行取得
                intSelectedRowFrom = .Models.Selection.AnchorRow
                intSelectedRowTo = .Models.Selection.LeadRow

                '一覧に行があり、かつ選択行番号が最大行数未満の場合削除処理を行う
                If .RowCount > 0 AndAlso intSelectedRowFrom < .RowCount AndAlso intSelectedRowTo < .RowCount Then

                    If intSelectedRowFrom < intSelectedRowTo Then

                        '削除行を上から下へ範囲選択した場合
                        '開始行から終了行まで選択行を削除する（逆回し）
                        For i As Integer = intSelectedRowTo To intSelectedRowFrom Step -1
                            .Rows(i).Remove()
                        Next

                    Else

                        '削除行を下から上へ範囲選択した場合、もしくは1行選択の場合
                        '開始行から終了行まで選択行を削除する（逆回し）
                        For i As Integer = intSelectedRowFrom To intSelectedRowTo Step -1
                            .Rows(i).Remove()
                        Next

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
    ''' 関連ファイル行追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関連ファイル一覧に空行を1行追加する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function AddRowFileinfoMain(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '空行追加処理
        If AddRowFileinfo(dataHBKF0201) = False Then
            Return False
        End If
        '[mod] 2012/09/07 y.ikushima 表示不具合対応START
        'データが無い場合、ボタン制御を行う
        With dataHBKF0201.PropVwRelationFileInfo.Sheets(0)
            If .RowCount > 0 Then
                dataHBKF0201.PropBtnRelationFileOpen.Enabled = True
                dataHBKF0201.PropBtnRelationFileDownLoad.Enabled = True
            Else
                dataHBKF0201.PropBtnRelationFileOpen.Enabled = False
                dataHBKF0201.PropBtnRelationFileDownLoad.Enabled = False
            End If
        End With
        '[mod] 2012/09/07 y.ikushima 表示不具合対応END

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function


    ''' <summary>
    ''' 【共通】関連ファイル空行追加処理
    ''' </summary>
    ''' <param name="DataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関連ファイルに空行を1行追加する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function AddRowFileinfo(ByRef DataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intNewRowNo As Integer          '新規追加行番号
        Dim blnAddFlg As Boolean = True     '追加フラグ

        Try
            With DataHBKF0201



                '追加フラグ初期化
                blnAddFlg = True

                'pathと説明が既に設定済でない場合のみ追加
                For j As Integer = 0 To .PropVwRelationFileInfo.Sheets(0).RowCount - 1

                    '既に設定済みの場合は追加フラグをOFFにして処理を抜ける
                    If .PropStrFilePath = .PropVwRelationFileInfo.Sheets(0).GetText(j, COL_RELFILE_PATH) AndAlso _
                       .PropStrFileRegDT = .PropVwRelationFileInfo.Sheets(0).GetText(j, COL_RELFILE_REGDT) AndAlso _
                       .PropStrFileNaiyo = .PropVwRelationFileInfo.Sheets(0).GetText(j, COL_RELFILE_NAIYO) Then
                        blnAddFlg = False
                        Exit For
                    End If

                Next

                '追加フラグがONの場合のみ追加処理を行う
                If blnAddFlg = True Then

                    '追加行番号取得
                    intNewRowNo = .PropVwRelationFileInfo.Sheets(0).Rows.Count

                    '新規行追加
                    .PropVwRelationFileInfo.Sheets(0).Rows.Add(intNewRowNo, 1)

                    'サブ検索画面での選択値を設定
                    .PropVwRelationFileInfo.Sheets(0).Cells(intNewRowNo, COL_RELFILE_NAIYO).Value = .PropStrFileNaiyo         '説明
                    .PropVwRelationFileInfo.Sheets(0).Cells(intNewRowNo, COL_RELFILE_REGDT).Value = .PropStrFileRegDT         '説明
                    .PropVwRelationFileInfo.Sheets(0).Cells(intNewRowNo, COL_RELFILE_PATH).Value = .PropStrFilePath              'パス

                End If



                '最終追加行にフォーカスをセット
                If commonLogicHBK.SetFocusOnVwRow(.PropVwRelationFileInfo, _
                                                  0, .PropVwRelationFileInfo.Sheets(0).RowCount, 0, _
                                                  1, .PropVwRelationFileInfo.Sheets(0).ColumnCount) = False Then
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
    ''' 関連ファイル行削除時メイン処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関連ファイル一覧の選択行を削除する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowFileInfoMain(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowFileinfo(dataHBKF0201) = False Then
            Return False
        End If

        '[mod] 2012/09/07 y.ikushima 表示不具合対応START
        'データが無い場合、ボタン制御を行う
        With dataHBKF0201.PropVwRelationFileInfo.Sheets(0)
            If .RowCount > 0 Then
                dataHBKF0201.PropBtnRelationFileOpen.Enabled = True
                dataHBKF0201.PropBtnRelationFileDownLoad.Enabled = True
            Else
                dataHBKF0201.PropBtnRelationFileOpen.Enabled = False
                dataHBKF0201.PropBtnRelationFileDownLoad.Enabled = False
            End If
        End With
        '[mod] 2012/09/07 y.ikushima 表示不具合対応END
        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function


    ''' <summary>
    ''' 【共通】関連ファイル選択行削除処理
    ''' </summary>
    ''' <param name="DataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関連ファイルの選択行を削除（Remove）する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowFileinfo(ByRef DataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号

        Try
            With DataHBKF0201.PropVwRelationFileInfo.Sheets(0)

                '選択開始行、終了行取得
                intSelectedRowFrom = .Models.Selection.AnchorRow
                intSelectedRowTo = .Models.Selection.LeadRow

                '一覧に行があり、かつ選択行番号が最大行数未満の場合削除処理を行う
                If .RowCount > 0 AndAlso intSelectedRowFrom < .RowCount AndAlso intSelectedRowTo < .RowCount Then

                    If intSelectedRowFrom < intSelectedRowTo Then

                        '削除行を上から下へ範囲選択した場合
                        '開始行から終了行まで選択行を削除する（逆回し）
                        For i As Integer = intSelectedRowTo To intSelectedRowFrom Step -1
                            .Rows(i).Remove()
                        Next

                    Else

                        '削除行を下から上へ範囲選択した場合、もしくは1行選択の場合
                        '開始行から終了行まで選択行を削除する（逆回し）
                        For i As Integer = intSelectedRowFrom To intSelectedRowTo Step -1
                            .Rows(i).Remove()
                        Next

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
    ''' リリース依頼受領システム行削除時メイン処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>リリース依頼受領システムの選択行を削除する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowIraiMain(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowIrai(dataHBKF0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function


    ''' <summary>
    ''' 【共通】リリース依頼受領システム選択行削除処理
    ''' </summary>
    ''' <param name="DataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>リリース依頼受領システムの選択行を削除（Remove）する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowIrai(ByRef DataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号

        Try
            With DataHBKF0201.PropVwIrai.Sheets(0)

                '選択開始行、終了行取得
                intSelectedRowFrom = .Models.Selection.AnchorRow
                intSelectedRowTo = .Models.Selection.LeadRow

                '一覧に行があり、かつ選択行番号が最大行数未満の場合削除処理を行う
                If .RowCount > 0 AndAlso intSelectedRowFrom < .RowCount AndAlso intSelectedRowTo < .RowCount Then

                    If intSelectedRowFrom < intSelectedRowTo Then

                        '削除行を上から下へ範囲選択した場合
                        '開始行から終了行まで選択行を削除する（逆回し）
                        For i As Integer = intSelectedRowTo To intSelectedRowFrom Step -1
                            .Rows(i).Remove()
                        Next

                    Else

                        '削除行を下から上へ範囲選択した場合、もしくは1行選択の場合
                        '開始行から終了行まで選択行を削除する（逆回し）
                        For i As Integer = intSelectedRowFrom To intSelectedRowTo Step -1
                            .Rows(i).Remove()
                        Next

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
    ''' リリース実施対象システム行削除時メイン処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>リリース実施対象システムの選択行を削除する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowJissiMain(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowJissi(dataHBKF0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function


    ''' <summary>
    ''' 【共通】リリース実施対象システム選択行削除処理
    ''' </summary>
    ''' <param name="DataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>リリース実施対象システムの選択行を削除（Remove）する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowJissi(ByRef DataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号

        Try
            With DataHBKF0201.PropVwJissi.Sheets(0)

                '選択開始行、終了行取得
                intSelectedRowFrom = .Models.Selection.AnchorRow
                intSelectedRowTo = .Models.Selection.LeadRow

                '一覧に行があり、かつ選択行番号が最大行数未満の場合削除処理を行う
                If .RowCount > 0 AndAlso intSelectedRowFrom < .RowCount AndAlso intSelectedRowTo < .RowCount Then

                    If intSelectedRowFrom < intSelectedRowTo Then

                        '削除行を上から下へ範囲選択した場合
                        '開始行から終了行まで選択行を削除する（逆回し）
                        For i As Integer = intSelectedRowTo To intSelectedRowFrom Step -1
                            .Rows(i).Remove()
                        Next

                    Else

                        '削除行を下から上へ範囲選択した場合、もしくは1行選択の場合
                        '開始行から終了行まで選択行を削除する（逆回し）
                        For i As Integer = intSelectedRowFrom To intSelectedRowTo Step -1
                            .Rows(i).Remove()
                        Next

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
    ''' 会議情報行追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報一覧に空行を1行追加する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function AddRowMeetingMain(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '空行追加処理
        If AddRowMeeting(dataHBKF0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】会議情報空行追加処理
    ''' </summary>
    ''' <param name="DataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報に空行を1行追加する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function AddRowMeeting(ByRef DataHBKF0201 As DataHBKF0201) As Boolean

        '変数宣言
        Dim intNewRowNo As Integer          '新規追加行番号
        Dim blnAddFlg As Boolean = True     '追加フラグ

        Try
            With DataHBKF0201

                '会議結果情報を取得する
                If GetMeetingResultData(DataHBKF0201) = False Then
                    Return False
                End If

                'サブ検索画面で1件以上選択された場合に値を設定
                If .PropDtResultSub IsNot Nothing Then

                    '選択データ数分繰り返し、会議情報一覧に行を追加する
                    For i As Integer = 0 To .PropDtResultSub.Rows.Count - 1

                        '追加フラグ初期化
                        blnAddFlg = True

                        '番号が既に設定済でない場合のみ追加
                        For j As Integer = 0 To .PropVwMeeting.Sheets(0).RowCount - 1

                            '既に設定済みの場合は追加フラグをOFFにして処理を抜ける
                            If .PropDtResultSub.Rows(i).Item("MeetingNmb").ToString.Equals(.PropVwMeeting.Sheets(0).GetText(j, COL_MEETING_NMB)) Then
                                blnAddFlg = False
                                Exit For
                            End If

                        Next

                        '追加フラグがONの場合のみ追加処理を行う
                        If blnAddFlg = True Then

                            '追加行番号取得
                            intNewRowNo = .PropVwMeeting.Sheets(0).Rows.Count

                            '新規行追加
                            .PropVwMeeting.Sheets(0).Rows.Add(intNewRowNo, 1)

                            'サブ検索画面での選択値を設定
                            .PropVwMeeting.Sheets(0).Cells(intNewRowNo, COL_MEETING_NMB).Value = _
                                .PropDtResultSub.Rows(i).Item("MeetingNmb")                                 '番号
                            .PropVwMeeting.Sheets(0).Cells(intNewRowNo, COL_MEETING_JISISTDT).Value = _
                                .PropDtResultSub.Rows(i).Item("jisiDT")                                     '実施日
                            .PropVwMeeting.Sheets(0).Cells(intNewRowNo, COL_MEETING_TITLE).Value = _
                                .PropDtResultSub.Rows(i).Item("Title")                                      'タイトル

                            Dim dr() As DataRow = .PropDtMeeting.Select("MeetingNmb='" & .PropDtResultSub.Rows(i).Item("MeetingNmb") & "'")
                            If dr.Count > 0 Then
                                '設定済みがアリ
                                .PropVwMeeting.Sheets(0).Cells(intNewRowNo, COL_MEETING_RESULTKBN_NM).Value = _
                                    dr(0).Item("ResultKbnNM") '.PropDtResultSub.Rows(i).Item("ResultKbnNM")                           　'承認　
                                .PropVwMeeting.Sheets(0).Cells(intNewRowNo, COL_MEETING_RESULTKBN).Value = _
                                    dr(0).Item("ResultKbn") '.PropDtResultSub.Rows(i).Item("ResultKbn")                                '承認コード
                            Else
                                '新規紐付け
                                .PropVwMeeting.Sheets(0).Cells(intNewRowNo, COL_MEETING_RESULTKBN).Value = ""                                '承認　
                                .PropVwMeeting.Sheets(0).Cells(intNewRowNo, COL_MEETING_RESULTKBN).Value = "0"                             '承認コード
                            End If


                        End If

                    Next

                    '最終追加行にフォーカスをセット
                    If commonLogicHBK.SetFocusOnVwRow(.PropVwMeeting, _
                                                      0, .PropVwMeeting.Sheets(0).RowCount, 0, _
                                                      1, .PropVwMeeting.Sheets(0).ColumnCount) = False Then
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
    ''' 会議情報行削除時メイン処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報一覧の選択行を削除する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowMeetingMain(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowMeeting(dataHBKF0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】会議情報データ取得処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議番号をキーに会議結果情報を取得する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMeetingResultData(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            '会議情報データ取得
            If GetMeetingResult(Adapter, Cn, dataHBKF0201) = False Then
                Return False
            End If

            'コネクションを閉じる
            Cn.Close()


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
            Adapter.Dispose()
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集／参照モード】会議情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報データを取得する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMeetingResult(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtMeeting As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKF0201.SetSelectGetMeetingSql(Adapter, Cn, DataHBKF0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議結果情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtMeeting)

            '取得データをデータクラスにセット
            DataHBKF0201.PropDtMeeting = dtMeeting


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
    ''' 【共通】会議情報選択行削除処理
    ''' </summary>
    ''' <param name="DataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報の選択行を削除（Remove）する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowMeeting(ByRef DataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号

        Try
            With DataHBKF0201.PropVwMeeting.Sheets(0)

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
    ''' 【共通】入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録項目の入力チェックを行う
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckInputValue(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKF0201

                '?:.ステータスの入力チェック(必須)
                With .PropCmbProcessState
                    '未入力の場合、エラー
                    If .SelectedValue = "" Then
                        'エラーメッセージ設定
                        puErrMsg = F0201_E003
                        'タブを基本情報タブに設定
                        dataHBKF0201.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False

                    End If
                End With

                'ステータスが完了でユーザー周知必要有無（必須）
                With .PropCmbProcessState
                    '未入力の場合、エラー
                    If .SelectedValue = PROCESS_STATUS_RELEASE_KANRYO Then
                        If dataHBKF0201.PropCmbUsrSyutiKbn.SelectedValue = "" Then
                            'エラーメッセージ設定
                            puErrMsg = F0201_E004
                            'タブを基本情報タブに設定
                            dataHBKF0201.PropTbInput.SelectedIndex = TAB_KHN
                            'フォーカス設定
                            .Focus()
                            .SelectAll()
                            'エラーを返す\
                            Return False
                        End If
                    End If
                End With

                'リリース着手日時の時分入力チェック
                If .PropDtpRelStDT.txtDate.Text.Trim() <> "" And .PropTxtRelStDT_HM.PropTxtTime.Text = "" Then
                    'エラーメッセージ設定
                    puErrMsg = F0201_E014
                    'タブを基本情報タブに設定
                    dataHBKF0201.PropTbInput.SelectedIndex = TAB_KHN
                    'フォーカス設定
                    .PropTxtRelStDT_HM.Focus()
                    'エラーを返す
                    Return False
                End If

                '開始日の日付入力チェック
                If .PropDtpRelStDT.txtDate.Text.Trim() = "" And .PropTxtRelStDT_HM.PropTxtTime.Text <> "" Then
                    'エラーメッセージ設定
                    puErrMsg = F0201_E013
                    'タブを基本情報タブに設定
                    dataHBKF0201.PropTbInput.SelectedIndex = TAB_KHN
                    'フォーカス設定
                    .PropDtpRelStDT.Focus()
                    'エラーを返す
                    Return False
                End If

                'リリース終了日時の時分入力チェック
                If .PropDtpRelEdDT.txtDate.Text.Trim() <> "" And .PropTxtRelEdDT_HM.PropTxtTime.Text = "" Then
                    'エラーメッセージ設定
                    puErrMsg = F0201_E016
                    'タブを基本情報タブに設定
                    dataHBKF0201.PropTbInput.SelectedIndex = TAB_KHN
                    'フォーカス設定
                    .PropTxtRelEdDT_HM.Focus()
                    'エラーを返す
                    Return False
                End If

                'リリース終了日時の日付入力チェック
                If .PropDtpRelEdDT.txtDate.Text.Trim() = "" And .PropTxtRelEdDT_HM.PropTxtTime.Text <> "" Then
                    'エラーメッセージ設定
                    puErrMsg = F0201_E015
                    'タブを基本情報タブに設定
                    dataHBKF0201.PropTbInput.SelectedIndex = TAB_KHN
                    'フォーカス設定
                    .PropDtpRelEdDT.Focus()
                    'エラーを返す
                    Return False
                End If


                'リリース依頼受領システム（重複チェック）
                With .PropVwIrai.Sheets(0)

                    '1行以上ある場合、チェックを行う
                    If .RowCount > 0 Then

                        '一覧の行数分繰り返し
                        For i As Integer = 0 To .RowCount - 1

                            '変数宣言
                            Dim strIrai As String = ""       '値

                            '各値を取得
                            strIrai = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_IRAI), "").Trim()

                            '値が入力されている場合のみチェック
                            If strIrai <> "" Then

                                '値が重複している場合、エラー
                                For j As Integer = 0 To .RowCount - 1

                                    If i <> j AndAlso _
                                        strIrai = commonLogicHBK.ChangeNothingToStr(.Cells(j, COL_IRAI), "").Trim() Then
                                        'エラーメッセージ設定
                                        puErrMsg = F0201_E005
                                        'タブを基本情報タブに設定
                                        dataHBKF0201.PropTbInput.SelectedIndex = TAB_KHN
                                        'フォーカス設定
                                        If commonLogicHBK.SetFocusOnVwRow(dataHBKF0201.PropVwIrai, _
                                                                          0, j, COL_IRAI, 1, .ColumnCount) = False Then
                                            Return False
                                        End If
                                        'エラーを返す
                                        Return False

                                    End If

                                Next

                            End If

                        Next

                    End If

                End With

                'リリース実施対象システム（重複チェック）
                With .PropVwJissi.Sheets(0)
                    '1行以上ある場合、チェックを行う
                    If .RowCount > 0 Then

                        '一覧の行数分繰り返し
                        For i As Integer = 0 To .RowCount - 1

                            '変数宣言
                            Dim strJissi As String = ""       '値

                            '各値を取得
                            strJissi = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_JISSI), "").Trim()

                            '値が入力されている場合のみチェック
                            If strJissi <> "" Then

                                '値が重複している場合、エラー
                                For j As Integer = 0 To .RowCount - 1

                                    If i <> j AndAlso _
                                        strJissi = commonLogicHBK.ChangeNothingToStr(.Cells(j, COL_JISSI), "").Trim() Then
                                        'エラーメッセージ設定
                                        puErrMsg = F0201_E006
                                        'タブを基本情報タブに設定
                                        dataHBKF0201.PropTbInput.SelectedIndex = TAB_KHN
                                        'フォーカス設定
                                        If commonLogicHBK.SetFocusOnVwRow(dataHBKF0201.PropVwJissi, _
                                                                          0, j, COL_JISSI, 1, .ColumnCount) = False Then
                                            Return False
                                        End If
                                        'エラーを返す
                                        Return False

                                    End If

                                Next

                            End If

                        Next

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

    ''' <summary>
    ''' 【DB更新中断時】メイン処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面の内容をログに出力し、フォームコントロールを再設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function OutputUnlockLogMain(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '入力内容ログ出力
        If OutputUnlockLog(dataHBKF0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【DB更新中断時】ログ出力処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>他のユーザによりロックが解除された場合に編集中の入力内容をログに出力する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function OutputUnlockLog(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '定数宣言
        Const SEP_HF_SPC As String = " "      '半角スペース

        ''変数宣言
        Dim strPlmList As New List(Of String)       'フォーマット埋込用パラメータ配列
        Dim strLogFilePath As String = ""           'ログファイルパス
        Dim strLogFileName As String                'ログファイル名
        Dim strOutputDir As String                  'ログ出力フォルダ

        Dim strText_Irai As String = ""             'リリース依頼受領文
        Dim strText_Jissi As String = ""            'リリース実施対象文
        Dim strText_Meeting As String = ""          '会議情報パラメータ文
        Dim strText_Relation As String = ""         '関係者情報パラメータ文
        Dim strText_PLink As String = ""            'プロセスリンクパラメータ文
        Dim strText_File As String = ""             '関連ファイルパラメータ文

        Dim sw As StreamWriter = Nothing            'ファイル書込用クラス

        Try
            With dataHBKF0201

                '書込用テキスト作成

                '【リリース基本情報--
                strPlmList.Add(.PropTxtRelNmb.Text)                             '0:リリース番号

                '【基本情報----
                strPlmList.Add(.PropTxtRelUkeNmb.Text)                          '1:リリース受付番号
                strPlmList.Add(.PropCmbProcessState.Text)               '2:ステータス
                strPlmList.Add(.PropDtpIraiDT.txtDate.Text)                     '3:依頼日(起票日)

                strPlmList.Add(.PropCmbTujyoKinkyuKbn.Text)             '4:通常・緊急区分
                strPlmList.Add(.PropCmbUsrSyutiKbn.Text)                '5:ユーザー周知必要有無

                '6:【リリース依頼受領システム----
                If .PropVwIrai.Sheets(0).RowCount > 0 Then
                    With .PropVwIrai.Sheets(0)
                        '一覧行数分繰り返し、パラメータ文を作成
                        For i As Integer = 0 To .RowCount - 1
                            '以下の書式で出力
                            '「リリース依頼受領」
                            strText_Irai &= .GetText(i, COL_IRAI)
                            '最終行以外は改行コード追加
                            If i < .RowCount - 1 Then
                                strText_Irai &= vbCrLf
                            End If
                        Next
                    End With
                End If
                strPlmList.Add(strText_Irai)

                '7:【リリース実施対象システム----
                If .PropVwJissi.Sheets(0).RowCount > 0 Then
                    With .PropVwJissi.Sheets(0)
                        '一覧行数分繰り返し、パラメータ文を作成
                        For i As Integer = 0 To .RowCount - 1
                            '以下の書式で出力
                            '「リリース依頼受領」
                            strText_Jissi &= .GetText(i, COL_IRAI)
                            '最終行以外は改行コード追加
                            If i < .RowCount - 1 Then
                                strText_Jissi &= vbCrLf
                            End If
                        Next
                    End With
                End If
                strPlmList.Add(strText_Jissi)

                strPlmList.Add(.PropDtpRelSceDT.txtDate.Text)                   '8:リリース予定日時（目安）
                strPlmList.Add(.PropCmbTantoGrpCD.Text)                         '9:担当グループ
                strPlmList.Add(.PropTxtRelTantoID.Text)                         '10:担当ID
                strPlmList.Add(.PropTxtRelTantoNM.Text)                         '11:担当氏名
                strPlmList.Add(.PropTxtTitle.Text)                              '12:タイトル
                strPlmList.Add(.PropTxtGaiyo.Text)                              '13:概要
                strPlmList.Add(.PropDtpRelStDT.txtDate.Text)                    '14:リリース着手日時
                strPlmList.Add(.PropDtpRelEdDT.txtDate.Text)                    '15:リリース終了日時

                '16:【会議情報----
                If .PropVwMeeting.Sheets(0).RowCount > 0 Then
                    With .PropVwMeeting.Sheets(0)
                        '一覧行数分繰り返し、パラメータ文を作成
                        For i As Integer = 0 To .RowCount - 1
                            '以下の書式で出力
                            '「番号」
                            '「実施日
                            '「タイトル」
                            '「承認」
                            strText_Meeting &= (i + 1).ToString() & ":" & .GetText(i, COL_MEETING_NMB)
                            strText_Meeting &= SEP_HF_SPC & .GetText(i, COL_MEETING_JISISTDT)
                            strText_Meeting &= SEP_HF_SPC & .GetText(i, COL_MEETING_TITLE)
                            strText_Meeting &= SEP_HF_SPC & .GetText(i, COL_MEETING_RESULTKBN)
                            '最終行以外は改行コード追加
                            If i < .RowCount - 1 Then
                                strText_Meeting &= vbCrLf
                            End If
                        Next
                    End With
                End If
                strPlmList.Add(strText_Meeting)

                '【フリー入力情報-----
                strPlmList.Add(.PropTxtBIko1.Text)            '17:フリーテキスト１
                strPlmList.Add(.PropTxtBIko2.Text)            '18:フリーテキスト２
                strPlmList.Add(.PropTxtBIko3.Text)            '19:フリーテキスト３
                strPlmList.Add(.PropTxtBIko4.Text)            '20:フリーテキスト４
                strPlmList.Add(.PropTxtBIko5.Text)            '21:フリーテキスト５

                '22～26:フリーフラグ１～５
                If .PropChkFreeFlg1.Checked = True Then
                    strPlmList.Add(FREE_FLG_ON_NM)
                Else
                    strPlmList.Add(FREE_FLG_OFF_NM)
                End If
                If .PropChkFreeFlg2.Checked = True Then
                    strPlmList.Add(FREE_FLG_ON_NM)
                Else
                    strPlmList.Add(FREE_FLG_OFF_NM)
                End If
                If .PropChkFreeFlg3.Checked = True Then
                    strPlmList.Add(FREE_FLG_ON_NM)
                Else
                    strPlmList.Add(FREE_FLG_OFF_NM)
                End If
                If .PropChkFreeFlg4.Checked = True Then
                    strPlmList.Add(FREE_FLG_ON_NM)
                Else
                    strPlmList.Add(FREE_FLG_OFF_NM)
                End If
                If .PropChkFreeFlg5.Checked = True Then
                    strPlmList.Add(FREE_FLG_ON_NM)
                Else
                    strPlmList.Add(FREE_FLG_OFF_NM)
                End If

                '27:【対応関係者情報-----
                If .PropVwRelationInfo.Sheets(0).RowCount > 0 Then
                    With .PropVwRelationInfo.Sheets(0)
                        '一覧行数分繰り返し、パラメータ文を作成
                        For i As Integer = 0 To .RowCount - 1
                            '以下の書式で出力
                            '「区分」
                            '「ID」
                            '「グループ名」
                            '「ユーザー名」
                            Dim strKbn As String = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_RELATION_KBN), "")
                            Dim strID As String = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_RELATION_ID), "")
                            Dim strNM As String = ""
                            If strKbn = KBN_GROUP Then
                                strNM = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_RELATION_GROUPNM), "")
                            ElseIf strKbn = KBN_USER Then
                                strNM = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_RELATION_USERNM), "")
                            End If
                            strText_Relation &= (i + 1).ToString() & "." & strKbn & " " & strID & " " & strNM
                            '最終行以外は改行コード追加
                            If i < .RowCount - 1 Then
                                strText_Relation &= vbCrLf
                            End If
                        Next
                    End With
                End If
                strPlmList.Add(strText_Relation)

                '28:【プロセスリンク情報-----
                If .PropVwProcessLinkInfo.Sheets(0).RowCount > 0 Then
                    With .PropVwProcessLinkInfo.Sheets(0)
                        '一覧行数分繰り返し、パラメータ文を作成
                        For i As Integer = 0 To .RowCount - 1
                            '以下の書式で出力
                            '「区分」
                            '「番号」
                            Dim strKbn As String = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_PLINK_KBN_NMR), "")
                            Dim strID As String = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_PLINK_NO), "")
                            strText_PLink &= (i + 1).ToString() & "." & strKbn & " " & strID
                            '最終行以外は改行コード追加
                            If i < .RowCount - 1 Then
                                strText_PLink &= vbCrLf
                            End If
                        Next
                    End With
                End If
                strPlmList.Add(strText_PLink)

                '29:【関連ファイル情報-----
                If .PropVwRelationFileInfo.Sheets(0).RowCount > 0 Then
                    With .PropVwRelationFileInfo.Sheets(0)
                        '一覧行数分繰り返し、パラメータ文を作成
                        For i As Integer = 0 To .RowCount - 1
                            '以下の書式で出力
                            '「説明」
                            '「登録日時」
                            Dim strNaiyo As String = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_RELFILE_NAIYO), "")
                            Dim strRegdt As String = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_RELFILE_REGDT), "")
                            strText_File &= (i + 1).ToString() & "." & strNaiyo & " " & strRegdt
                            '最終行以外は改行コード追加
                            If i < .RowCount - 1 Then
                                strText_File &= vbCrLf
                            End If
                        Next
                    End With
                End If
                strPlmList.Add(strText_File)


                'ログ出力フォルダ設定
                strOutputDir = Path.Combine(Application.StartupPath, OUTPUT_DIR_UNLOCKEDLOG)

                'システム日付を取得
                If GetSysdate(dataHBKF0201) = False Then
                    Return False
                End If

                'ログファイル名設定
                strLogFileName = Format(.PropDtmSysDate, "yyyyMMddHHmmss") & ".log"
                'strLogFileName = Format(DateTime.Parse(.PropDtRelLock.Rows(0).Item("SysTime")), "yyyyMMddHHmmss") & ".log"

                'ファイル出力を実行し、出力ファイルパスを取得
                If commonLogicHBK.OutputLogFromTextFormat(strPlmList, strLogFileName, _
                                                          FORMAT_DIR_UNLOCKEDLOG, _
                                                          FILE_UNLOCKLOG_RELEASE, _
                                                          strOutputDir, _
                                                          strLogFilePath) = False Then
                    Return False
                End If


                'データクラスにメッセージをセット
                dataHBKF0201.PropStrBeUnlockedMsg = String.Format(F0201_E009, strLogFilePath)

                'システムエラー時は以下を設定
                If puErrMsg.StartsWith(HBK_E001) Then
                    dataHBKF0201.PropStrBeUnlockedMsg = String.Format(F0201_E011, strLogFilePath)
                End If

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            If sw IsNot Nothing Then
                sw.Close()
            End If
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            If sw IsNot Nothing Then
                sw.Dispose()
            End If
        End Try

    End Function

    ''' <summary>
    ''' システム日付取得
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システム日付を取得する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetSysdate(ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter()    'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            'B-2-1システム日付取得（SELECT）
            If SelectSysDate(Adapter, Cn, dataHBKF0201) = False Then
                Return False
            End If

            'コネクションを閉じる
            Cn.Close()

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
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Adapter.Dispose()
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' B-2-8.【編集モード】会議結果情報　削除処理  
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKF0201">[IN]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容で会議結果情報テーブルを削除（delete）する
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DeleteMtgResult(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            '会議結果情報更新（Update）用SQLを作成
            If sqlHBKF0201.SetDeleteMtgResultSql(Cmd, Cn, DataHBKF0201) = False Then
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
    ''' B-2-9.【編集モード】会議情報　登録処理  
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKF0201">[IN]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容で会議結果情報テーブルを更新（Update）する
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertMtgResult(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            With DataHBKF0201
                '会議情報一覧の行数分繰り返し、更新処理を行う
                For i As Integer = 0 To .PropVwMeeting.Sheets(0).RowCount - 1

                    '登録行作成
                    Dim row As DataRow = .PropDtMeeting.NewRow
                    row.Item("MeetingNmb") = .PropVwMeeting.Sheets(0).GetValue(i, COL_MEETING_NMB)
                    row.Item("ResultKbn") = .PropVwMeeting.Sheets(0).GetValue(i, COL_MEETING_RESULTKBN)
                    If .PropVwMeeting.Sheets(0).GetValue(i, COL_MEETING_REGDT) Is Nothing Then
                        row.Item("RegDt") = DBNull.Value
                    Else
                        row.Item("RegDt") = .PropVwMeeting.Sheets(0).GetValue(i, COL_MEETING_REGDT)
                    End If

                    row.Item("RegGrpCD") = .PropVwMeeting.Sheets(0).GetValue(i, COL_MEETING_REGGRPCD)
                    row.Item("RegID") = .PropVwMeeting.Sheets(0).GetValue(i, COL_MEETING_REGID)
                    '作成した行をデータクラスにセット
                    .PropRowReg = row

                    '会議結果情報追加（insert）用SQLを作成
                    If sqlHBKF0201.SetInsertMtgResultSql(Cmd, Cn, DataHBKF0201) = False Then
                        Return False
                    End If

                    'ログ出力
                    commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議結果情報新規登録", Nothing, Cmd)

                    'SQL実行
                    Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【Combobox共通】コンボボックスリサイズメイン処理
    ''' </summary>
    ''' <param name="sender">[IN/OUT]コンボボックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>コンボボックスサイズ変換処理
    ''' <para>作成情報：2012/09/07 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ComboBoxResizeMain(ByRef sender As Object) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'コンボボックスサイズ変換処理
        If commonLogicHBK.ComboBoxResize(sender) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True


    End Function


    ''' <summary>
    ''' 【編集／参照／作業履歴モード】担当履歴情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKF0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>担当履歴情報データを取得する
    ''' <para>作成情報：2012/09/10 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetTantoRireki(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtINCInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKF0201.SetSelectTantoRirekiSql(Adapter, Cn, DataHBKF0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "担当履歴データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtINCInfo)

            '取得データをデータクラスにセット
            DataHBKF0201.PropDtTantoRireki = dtINCInfo


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
            dtINCInfo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集／参照モード】担当履歴作成処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴データを作成する
    ''' <para>作成情報：2012/09/10 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateTantoRireki(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '初期化
            Dim strTantoRirekiSplit As String = "←"
            dataHBKF0201.PropTxtGroupRireki.Text = ""
            dataHBKF0201.PropTxtTantoRireki.Text = ""

            '担当履歴
            With dataHBKF0201.PropDtTantoRireki
                If .Rows.Count > 0 Then
                    For i As Integer = 0 To .Rows.Count - 1
                        If i = 0 Then
                            dataHBKF0201.PropTxtGroupRireki.Text &= .Rows(i).Item("tantogrpnm")
                            dataHBKF0201.PropTxtTantoRireki.Text &= .Rows(i).Item("reltantonm")
                        Else
                            'ＧＰ
                            If Not .Rows(i - 1).Item("tantogrpnm").Equals(.Rows(i).Item("tantogrpnm")) Then
                                dataHBKF0201.PropTxtGroupRireki.Text &= strTantoRirekiSplit & .Rows(i).Item("tantogrpnm")
                            End If
                            'ＩＤ
                            If Not .Rows(i - 1).Item("reltantonm").Equals(.Rows(i).Item("reltantonm")) Then
                                dataHBKF0201.PropTxtTantoRireki.Text &= strTantoRirekiSplit & .Rows(i).Item("reltantonm")
                            End If
                        End If
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
    ''' 【新規／編集モード】担当履歴情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKF0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>担当履歴判定チェックをする
    ''' <para>作成情報：2012/09/10 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertTantoRireki(ByVal Cn As NpgsqlConnection, ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド
        Dim bln_chk_flg As Boolean = False

        Try
            '担当履歴、担当グループチェック処理
            'PropDtTantoRirekiは履歴を降順にしているのでROWは0を設定する

            '最終更新GPを取得 (tantorirekinmb Max)
            With dataHBKF0201

                If .PropDtTantoRireki IsNot Nothing AndAlso .PropDtTantoRireki.Rows.Count > 0 Then
                    '現更新者と同じかチェック
                    If .PropDtTantoRireki.Rows(0).Item("tantogrpnm").ToString.Equals(.PropCmbTantoGrpCD.Text) = False Then
                        bln_chk_flg = True
                    End If
                Else
                    'まだ登録がない
                    If .PropCmbTantoGrpCD.Text.Length > 0 Then
                        bln_chk_flg = True
                    End If
                End If



                If .PropDtTantoRireki IsNot Nothing AndAlso .PropDtTantoRireki.Rows.Count > 0 Then
                    '現更新者と同じかチェック
                    If .PropDtTantoRireki.Rows(0).Item("reltantonm").ToString.Equals(.PropTxtRelTantoNM.Text) = False Then
                        bln_chk_flg = True
                    End If
                Else
                    'まだ登録がない
                    If .PropTxtRelTantoNM.Text.Length > 0 Then
                        bln_chk_flg = True
                    End If
                End If

            End With

            '変更があった場合は登録する。
            If bln_chk_flg = True Then
                '担当履歴報新規登録（INSERT）用SQLを作成
                If sqlHBKF0201.SetInsertTantoRirekiSql(Cmd, Cn, dataHBKF0201) = False Then
                    Return False
                End If

                'ログ出力
                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "担当履歴情報 新規登録", Nothing, Cmd)

                'SQL実行
                Cmd.ExecuteNonQuery()

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
        End Try

    End Function

    ''' <summary>
    ''' 【共通】開くボタン押下時の処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ファイル管理テーブルからファイルパスを取得し、ファイルを開く
    ''' <para>作成情報：2012/09/12 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function FileOpenMain(ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ファイルパス取得処理
        If GetOpenFilePath(dataHBKF0201) = False Then
            Return False
        End If

        'ファイル表示処理
        If FileLoad(dataHBKF0201) = False Then
            Return False
        End If

        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        Return True

    End Function

    ''' <summary>
    ''' 【共通】ダウンロードボタン押下時の処理
    ''' </summary>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ファイル管理テーブルからファイルパスを取得し、ファイルをダウンロードする
    ''' <para>作成情報：2012/09/12 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function FileDownLoadMain(ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ファイルパス取得処理
        If GetOpenFilePath(dataHBKF0201) = False Then
            Return False
        End If

        'ファイルダウンロード処理
        If FileDownLoad(dataHBKF0201) = False Then
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
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択中の会議ファイルパスを習得する
    ''' <para>作成情報：2012/09/12 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetOpenFilePath(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKF0201

                '選択行のファイルパスを取得し、データクラスにセット
                .PropStrSelectedFilePath = .PropVwRelationFileInfo.Sheets(0).Cells(.PropIntSelectedRow, COL_RELFILE_PATH).Value

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
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ファイル管理テーブルからファイルパスを取得し、ファイルを開く
    ''' <para>作成情報：2012/09/12 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function FileLoad(ByVal dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strFilePath As String
        Dim intFileMngNmb As Integer

        Dim strCmd As String = ""                                   'コマンド文字列
        Dim strDriveName As String = ""                             '使用論理ドライブ名

        Try

            With dataHBKF0201

                '選択行のファイルパス、ファイル管理番号取得
                strFilePath = dataHBKF0201.PropStrSelectedFilePath
                intFileMngNmb = .PropVwRelationFileInfo.Sheets(0).Cells(.PropIntSelectedRow, COL_RELFILE_MNGNMB).Value

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
            puErrMsg = HBK_E001 & F0201_E017
            Return False
        Catch ex As System.IO.FileNotFoundException
            'ファイルが見つからなかった場合
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & F0201_E017
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
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ファイル管理テーブルからファイルパスを取得し、ファイルをダウンロードする
    ''' <para>作成情報：2012/09/12 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function FileDownLoad(ByVal dataHBKF0201 As DataHBKF0201) As Boolean

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
            With dataHBKF0201

                '選択行のファイルパスを取得
                strFilePath = dataHBKF0201.PropStrSelectedFilePath

                'ファイルダウンロード処理
                sfd.FileName = Path.GetFileName(strFilePath)
                sfd.InitialDirectory = ""
                sfd.Filter = "すべてのファイル(*.*)|*.*"
                sfd.FilterIndex = 1
                sfd.Title = "保存先を指定してください"


                '選択行のファイルパス、ファイル管理番号取得
                strFilePath = dataHBKF0201.PropStrSelectedFilePath
                intFileMngNmb = .PropVwRelationFileInfo.Sheets(0).Cells(.PropIntSelectedRow, COL_RELFILE_MNGNMB).Value

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
            puErrMsg = HBK_E001 & F0201_E017
            Return False
        Catch ex As System.IO.FileNotFoundException
            'ファイルが見つからなかった場合
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & F0201_E017
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
    ''' 【会議一覧表示後】会議情報再取得メイン処理
    ''' </summary>
    ''' <param name="DataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議結果情報データの再取得を行う。
    ''' <para>作成情報：2012/09/11 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RefreshMeetingMain(ByRef dataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try

            '会議結果情報データ取得戻り値用(PropDtResultMtg)
            If GetMeetingResult_Close(Adapter, Cn, dataHBKF0201) = False Then
                Return False
            End If

            With dataHBKF0201
                'データテーブルを取得
                .PropDtMeeting = DirectCast(.PropVwMeeting.Sheets(0).DataSource, DataTable)

                '退避用データテーブル作成
                Dim dtAdd As DataTable = .PropDtMeeting.Clone
                Dim dtDel As DataTable = .PropDtMeeting.Clone
                If .PropDtMeeting IsNot Nothing AndAlso .PropDtMeeting.Rows.Count > 0 Then
                    '追加された情報で未登録のものを取得 
                    For i As Integer = 0 To .PropDtMeeting.Rows.Count - 1
                        'Addされたデータのみ取得
                        Select Case .PropDtMeeting.Rows(i).RowState
                            Case DataRowState.Added '画面で追加されたデータ
                                dtAdd.Rows.Add(.PropDtMeeting.Rows(i).Item("MeetingNmb"), _
                                               .PropDtMeeting.Rows(i).Item("JisiDT"), _
                                               .PropDtMeeting.Rows(i).Item("Title"), _
                                               .PropDtMeeting.Rows(i).Item("ResultKbnNM"), _
                                               .PropDtMeeting.Rows(i).Item("ResultKbn"))

                            Case DataRowState.Deleted '画面で削除されたデータ
                                dtDel.Rows.Add(.PropDtMeeting.Rows(i).Item("MeetingNmb", DataRowVersion.Original))

                        End Select
                    Next
                End If

                '会議一覧スプレッド再取得データを設定
                .PropDtMeeting = .PropDtResultMtg.Copy
                .PropDtMeeting.AcceptChanges()
                .PropVwMeeting.DataSource = .PropDtMeeting


                '画面上で追加且つＤＢ未更新のデータを反映
                If dtAdd.Rows.Count > 0 Then
                    For i As Integer = 0 To dtAdd.Rows.Count - 1
                        .PropDtMeeting.Rows.Add(dtAdd.Rows(i).Item("MeetingNmb"), _
                                                  dtAdd.Rows(i).Item("JisiDT"), _
                                                  dtAdd.Rows(i).Item("Title"), _
                                                  dtAdd.Rows(i).Item("ResultKbnNM"), _
                                                  dtAdd.Rows(i).Item("ResultKbn"))
                    Next
                End If

                '画面上で削除且つＤＢ未更新のデータを反映
                If dtDel.Rows.Count > 0 Then
                    For i As Integer = 0 To dtDel.Rows.Count - 1
                        For j As Integer = 0 To .PropDtMeeting.Rows.Count - 1
                            Select Case .PropDtMeeting.Rows(j).RowState
                                Case DataRowState.Deleted
                                    If .PropDtMeeting.Rows(j).Item("MeetingNmb", DataRowVersion.Original).ToString.Equals(dtDel.Rows(i).Item("MeetingNmb").ToString) Then
                                        .PropDtMeeting.Rows(j).Delete()
                                    End If
                                Case Else
                                    If .PropDtMeeting.Rows(j).Item("MeetingNmb").ToString.Equals(dtDel.Rows(i).Item("MeetingNmb").ToString) Then
                                        .PropDtMeeting.Rows(j).Delete()
                                    End If
                            End Select
                        Next
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
        Finally
            Adapter.Dispose()
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【編集／参照モード】会議情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報データを取得する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMeetingResult_Close(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKF0201 As DataHBKF0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtMeeting As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKF0201.SetSelectGetMeetingSql(Adapter, Cn, DataHBKF0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議結果情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtMeeting)

            '取得データをデータクラスにセット
            DataHBKF0201.PropDtResultMtg = dtMeeting


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
    ''' A-1-2.【共通】対象システム変更チェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKF0201">[IN/OUT]リリース登録画面Dataクラス</param>
    ''' <param name="kbn">[IN]システム区分</param>
    ''' <param name="EntryNmb">[IN]登録順</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対象システムが変更されたかチェックする
    ''' <para>作成情報：2012/10/11 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckSysNmb(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef dataHBKF0201 As DataHBKF0201, ByVal kbn As String, ByVal EntryNmb As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKF0201.GetChkSysNmbData(Adapter, Cn, dataHBKF0201, kbn, EntryNmb) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対象システムの変更有無情報取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)


            If dtmst IsNot Nothing AndAlso dtmst.Rows.Count > 0 Then
                If dtmst.Rows(0).Item(0).ToString.Equals(dataHBKF0201.PropRowReg.Item(0).ToString) Then
                    dataHBKF0201.PropBlnCheckSystemNmb = False
                Else
                    '更新前と対象システムが違う場合True
                    dataHBKF0201.PropBlnCheckSystemNmb = True
                End If
            Else
                dataHBKF0201.PropBlnCheckSystemNmb = False
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
            dtmst.Dispose()
        End Try

    End Function


End Class
