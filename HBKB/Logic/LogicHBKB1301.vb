Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.IO


''' <summary>
''' 部所有機器登録画面ロジッククラス
''' </summary>
''' <remarks>部所有機器登録画面のロジックを定義したクラス
''' <para>作成情報：2012/07/11 s.tsuruta
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKB1301

    'インスタンス作成
    Private sqlHBKB1301 As New SqlHBKB1301
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK
    Private commonValidation As New CommonValidation

    'Public定数宣言==============================================
    '原因リンク一覧列番号
    Public Const COL_CAUSELINK_KBN_NMR As Integer = 0       '区分
    Public Const COL_CAUSELINK_NO As Integer = 1            '番号
    Public Const COL_CAUSELINK_KBN As Integer = 2           '区分（コード）　※非表示
    '履歴情報一覧列番号
    Public Const COL_REGREASON_UPID As Integer = 0          '更新ID
    Public Const COL_REGREASON_UPDT As Integer = 1          '更新日時
    Public Const COL_REGREASON_UPGROUPNM As Integer = 2     '更新者グループ名
    Public Const COL_REGREASON_UPUSERNM As Integer = 3      '更新者ユーザー名
    Public Const COL_REGREASON_REASON As Integer = 4        '理由

    'Private定数宣言==============================================
    'コントロールの幅
    Private Const WIDTH_GROUPBOX_CIKHN As Integer = 442     'CI基本情報グループボックス
    'タブページ
    Private Const TAB_KHN As Integer = 0                    '基本情報
    Private Const TAB_RIYO As Integer = 1                   '利用情報
    Private Const TAB_FREE As Integer = 2                   'フリー入力情報
    Private Const TAB_RELATION As Integer = 3               '関係情報


    ''' <summary>
    ''' 【共通】システムエラー事前対応処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システムエラー発生時に非活性にするコントロールリストを作成する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DoProcForErrorMain(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'トランザクション系コントロールリスト作成
        If CreateTsxCtlList(dataHBKB1301) = False Then
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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて画面の初期表示設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormNewModeMain(ByRef dataHBKB1301 As DataHBKB1301) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'スプレッド表示用データテーブル作成
        If CreateDataTableForVw(dataHBKB1301) = False Then
            Return False
        End If

        'フォームコントロール設定
        If InitFormControl(dataHBKB1301) = False Then
            Return False
        End If

        '初期表示用データ取得
        If GetInitData(dataHBKB1301) = False Then
            Return False
        End If

        '初期表示用データセット
        If SetInitDataToControl(dataHBKB1301) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True
    End Function

    ''' <summary>
    ''' 【編集モード】ロックメイン処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>データのロック状況をチェックし、状況に応じてロックまたは処理モードの切替を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function LockMain(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロック設定
        If SetLockWhenLoad(dataHBKB1301) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【編集モード】画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで画面の初期表示設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormEditModeMain(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'スプレッド表示用データテーブル作成
        If CreateDataTableForVw(dataHBKB1301) = False Then
            Return False
        End If

        'フォームコントロール設定
        If InitFormControl(dataHBKB1301) = False Then
            Return False
        End If

        '初期表示用データ取得
        If GetInitData(dataHBKB1301) = False Then
            Return False
        End If

        '初期表示用データセット
        If SetInitDataToControl(dataHBKB1301) = False Then
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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで画面の初期表示設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormRefModeMain(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'スプレッド表示用データテーブル作成
        If CreateDataTableForVw(dataHBKB1301) = False Then
            Return False
        End If

        'フォームコントロール設定
        If InitFormControl(dataHBKB1301) = False Then
            Return False
        End If

        '初期表示用データ取得
        If GetInitData(dataHBKB1301) = False Then
            Return False
        End If

        '初期表示用データセット
        If SetInitDataToControl(dataHBKB1301) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【履歴モード】画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードで画面の初期表示設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormRirekiModeMain(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'スプレッド表示用データテーブル作成
        If CreateDataTableForVw(dataHBKB1301) = False Then
            Return False
        End If

        'フォームコントロール設定
        If InitFormControl(dataHBKB1301) = False Then
            Return False
        End If

        '初期表示用データ取得
        If GetInitData(dataHBKB1301) = False Then
            Return False
        End If

        '初期表示用データセット
        If SetInitDataToControl(dataHBKB1301) = False Then
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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータをログインユーザでロックし、フォームのロックを解除する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function UnlockWhenClickBtnUnlockMain(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロックフラグOFF
        dataHBKB1301.PropBlnBeLockedFlg = False

        'ロック処理
        If SetLockWhenUnlock(dataHBKB1301) = False Then
            Return False
        End If
        If SetDataToLoginAndLock(dataHBKB1301) = False Then
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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>他のユーザによりロックが解除されていないかチェックする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckBeUnlockedMain(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロック解除チェック
        If CheckUnlock(dataHBKB1301) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【参照モード】編集モードから参照モードへ変更時のメイン処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面の内容をログに出力し、フォームコントロールを再設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetFormRefModeFromEditModeMain(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '入力内容ログ出力
        If OutputUnlockLog(dataHBKB1301) = False Then
            Return False
        End If

        '参照モードでフォームコントロール設定
        dataHBKB1301.PropBlnBeLockedFlg = True     'ロックする
        If SetFormControlPerProcMode(dataHBKB1301) = False Then
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
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータのロックを解除する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function UnlockWhenCloseMain(ByVal dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロック解除処理
        If UnlockData(dataHBKB1301) = False Then
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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>トランザクション系コントロールのリストを作成する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateTsxCtlList(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim aryCtlList As New ArrayList

        Try
            With dataHBKB1301

                'トランザクション系のコントロールをリストに追加
                aryCtlList.Add(.PropBtnReg)              '登録ボタン
                aryCtlList.Add(.PropBtnRollBack)         '解除ボタン
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
    ''' エラー時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録系ボタンを非活性にする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetFormWhenErrorMain(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '登録系ボタンを非活性にする
        If SetUnabledWhenError(dataHBKB1301) = False Then
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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドのデータソースとして設定するデータテーブルを作成する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateDataTableForVw(ByRef dataHBKB1301 As DataHBKB1301) As Boolean


        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtMyCauseLink As New DataTable       '原因リンク用データテーブル
        Dim dtRireki As New DataTable       '履歴情報用データテーブル


        Try

            '原因リンク用テーブル作成
            With dtMyCauseLink
                .Columns.Add("ProcessKbn", Type.GetType("System.String"))          'プロセス区分
                .Columns.Add("MngNmb", Type.GetType("System.String"))              '番号
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            '履歴情報用テーブル作成
            With dtRireki
                .Columns.Add("RirekiNo", Type.GetType("System.String"))            '更新ID
                .Columns.Add("RegDT", Type.GetType("System.String"))               '更新日時
                .Columns.Add("GroupNM", Type.GetType("System.String"))             '更新者グループ名
                .Columns.Add("HBKUsrNM", Type.GetType("System.String"))            '更新者名
                .Columns.Add("RegReason", Type.GetType("System.String"))           '理由
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'データクラスに作成テーブルを格納
            With dataHBKB1301
                .PropDtMyCauseLink = dtMyCauseLink                    'スプレッド表示用：原因リンクデータ
                .PropDtRireki = dtRireki                    'スプレッド表示用：履歴情報データ
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
            dtMyCauseLink.Dispose()
            dtRireki.Dispose()
        End Try


    End Function

    ''' <summary>
    ''' 【共通】初期表示用データ取得処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            'マスタデータ取得
            If GetMastaData(Adapter, Cn, dataHBKB1301) = False Then
                Return False
            End If

            'メイン表示データ取得
            If GetMainData(Adapter, Cn, dataHBKB1301) = False Then
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
    ''' 【編集モード】解除ボタンクリック時ロック設定処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLockWhenUnlock(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'CI共通情報テーブルロック解除
            If commonLogicHBK.UnlockCIInfo(dataHBKB1301.PropIntCINmb) = False Then
                Return False
            End If

            'CI共通情報テーブルロック
            If commonLogicHBK.LockCIInfo(dataHBKB1301.PropIntCINmb, dataHBKB1301.PropDtCILock) = False Then
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
    ''' 【新規登録モード】データ登録メイン処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をデータベースに反映する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RegistDataOnNewModeMain(ByVal dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '新規登録処理
        If InsertNewData(dataHBKB1301) = False Then
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
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をデータベースに反映する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RegistDataOnEditModeMain(ByVal dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '更新処理
        If UpdateData(dataHBKB1301) = False Then
            Return False
        End If

        'ロック解除処理
        If UnlockData(dataHBKB1301) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【編集モード】フォームロード時ロック設定処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLockWhenLoad(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnBeLocked As Boolean = False                   'ロックフラグ  

        Try

            With dataHBKB1301

                'ロック解除チェック
                If commonLogicHBK.CheckDataBeLocked(.PropIntCINmb, blnBeLocked, .PropStrBeLockedMsg, .PropDtCILock) = False Then
                    Return False
                End If

                'ロックされている（別のユーザが編集中）場合、 ロックフラグをON
                If blnBeLocked = True Then

                    .PropBlnBeLockedFlg = True

                Else

                    'ロックされていない場合、CI共通情報をロックする
                    If SetLock(dataHBKB1301) = False Then
                        Return False
                    End If

                    'ロックフラグをOFF
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
    ''' 【編集モード】ロック設定処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集開始日時によりロック設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckAndSetLock(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnBeLocked As Boolean = False                   'ロックフラグ  

        Try

            With dataHBKB1301

                'ロック解除チェック
                If commonLogicHBK.CheckDataBeLocked(.PropIntCINmb, blnBeLocked, .PropStrBeLockedMsg, .PropDtCILock) = False Then
                    Return False
                End If

                'ロックされている（別のユーザが編集中）場合、 ロックフラグをON
                If blnBeLocked = True Then

                    .PropBlnBeLockedFlg = True

                Else

                    'ロックされていない場合、CI共通情報をロックする
                    If SetLock(dataHBKB1301) = False Then
                        Return False
                    End If

                    'ロックフラグをOFF
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
    ''' 【編集モード】フォームロード時ロック設定処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報テーブルをロックする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLock(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnDoUnlock As Boolean = False                   'ロック解除実行フラグ

        Try

            With dataHBKB1301

                'CI共通情報ロックテーブルデータがある場合、ロック解除実行フラグON
                If .PropDtCILock.Rows.Count > 0 Then
                    blnDoUnlock = True
                End If

                'CI共通情報ロック
                If commonLogicHBK.LockCIInfo(.PropIntCINmb, .PropDtCILock, blnDoUnlock) = False Then
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
    ''' 【共通】マスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>マスタデータを取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMastaData(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'CI種別マスタ取得
            If commonLogicHBK.GetCIKindMastaData(Adapter, Cn, CI_TYPE_KIKI, dataHBKB1301.PropDtCIKindMasta) = False Then
                Return False
            End If

            '種別マスタ取得
            '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 START
            'If commonLogicHBK.GetKindMastaData(Adapter, Cn, CI_TYPE_KIKI, dataHBKB1301.PropDtKindMasta) = False Then
            '    Return False
            'End If
            If commonLogicHBK.GetKindMastaData(Adapter, Cn, CI_TYPE_KIKI, dataHBKB1301.PropDtKindMasta, dataHBKB1301.PropIntCINmb) = False Then
                Return False
            End If
            '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 END

            'CIステータスマスタ取得
            If commonLogicHBK.GetCIStatusMastaData(Adapter, Cn, CI_TYPE_KIKI, dataHBKB1301.PropDtCIStatusMasta) = False Then
                Return False
            End If

            '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 START
            ''OS名(ソフトデータマスタ)取得
            'If commonLogicHBK.GetSoftMastaData(Adapter, Cn, SOFTKBN_OS, dataHBKB1301.PropDtOSCD) = False Then
            '    Return False
            'End If

            ''ウィルス対策ソフト名(ソフトデータマスタ)取得
            'If commonLogicHBK.GetSoftMastaData(Adapter, Cn, SOFTKBN_UNTIVIRUSSOFT, dataHBKB1301.PropDtAntiVirusSoftCD) = False Then
            '    Return False
            'End If
            'OS名(ソフトデータマスタ)取得
            If commonLogicHBK.GetSoftMastaData(Adapter, Cn, SOFTKBN_OS, dataHBKB1301.PropDtOSCD, dataHBKB1301.PropIntCINmb) = False Then
                Return False
            End If

            'ウィルス対策ソフト名(ソフトデータマスタ)取得
            If commonLogicHBK.GetSoftMastaData(Adapter, Cn, SOFTKBN_UNTIVIRUSSOFT, dataHBKB1301.PropDtAntiVirusSoftCD, dataHBKB1301.PropIntCINmb) = False Then
                Return False
            End If
            '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 END

            'DNS登録（機器ステータスマスタ）取得
            If commonLogicHBK.GetKikiStatusMastaData(Adapter, Cn, KIKISTATEKBN_DNS_REG, dataHBKB1301.PropDtDNSRegCD) = False Then
                Return False
            End If

            'IP割当種類（機器ステータスマスタ）取得
            If commonLogicHBK.GetKikiStatusMastaData(Adapter, Cn, KIKISTATEKBN_IP_WARIATE, dataHBKB1301.PropDtIPUseCD) = False Then
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
    ''' 【共通】初期表示用メインデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainData(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '取得しない


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用データ取得
                        If GetMainDataForEdit(Adapter, Cn, dataHBKB1301) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用データ取得　※編集モードと同じ
                        If GetMainDataForEdit(Adapter, Cn, dataHBKB1301) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用データ取得
                    If GetMainDataForRireki(Adapter, Cn, dataHBKB1301) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then      '参照モード

                    '参照モード用データ取得　※編集モードと同じ
                    If GetMainDataForEdit(Adapter, Cn, dataHBKB1301) = False Then
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
    ''' 【編集／参照モード】初期表示用メインデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集／参照モードで初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainDataForEdit(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'CI共通情報データ取得
            If GetCIInfo(Adapter, Cn, dataHBKB1301) = False Then
                Return False
            End If

            'CI部所有機器データ取得
            If GetCIBuy(Adapter, Cn, dataHBKB1301) = False Then
                Return False
            End If

            '登録理由履歴データ取得
            If GetRegReason(Adapter, Cn, dataHBKB1301) = False Then
                Return False
            End If

            '原因リンク履歴データ取得
            If GetCauseLink(Adapter, Cn, dataHBKB1301) = False Then
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
    ''' 【編集／参照モード】CI共通情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報データを取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetCIInfo(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtCIInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKB1301.SetSelectCIInfoSql(Adapter, Cn, dataHBKB1301) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtCIInfo)

            'データが取得できなかった場合、エラー
            If dtCIInfo.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & B1301_E001, TBNM_CI_INFO_TB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKB1301.PropDtCIInfo = dtCIInfo


            '終了ログ出力
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
    ''' 【編集／参照モード】CI部所有機器データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI部所有機器データを取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetCIBuy(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtCIBuy As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKB1301.SetSelectCIBuySql(Adapter, Cn, dataHBKB1301) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI部所有機器データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtCIBuy)

            'データが取得できなかった場合、エラー
            If dtCIBuy.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & B1301_E001, TBNM_CI_BUY_TB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKB1301.PropDtCIBuy = dtCIBuy


            '終了ログ出力
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
            dtCIBuy.Dispose()
        End Try

    End Function

    ''' <summary>
    '''【編集／参照モード】原因リンク履歴データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>原因リンク履歴データを取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetCauseLink(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '取得用SQLの作成・設定
            If sqlHBKB1301.SetSelectCauseLinkSql(Adapter, Cn, dataHBKB1301) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "原因リンク履歴取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB1301.PropDtMyCauseLink)

            '終了ログ出力
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
    ''' 【編集／参照モード】登録理由履歴データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録理由履歴データを取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetRegReason(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '取得用SQLの作成・設定
            If sqlHBKB1301.SetSelectRegReasonSql(Adapter, Cn, dataHBKB1301) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "登録理由履歴データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB1301.PropDtRireki)


            '最大履歴番号を取得
            If dataHBKB1301.PropDtRireki.Rows.Count > 0 Then
                dataHBKB1301.PropIntRirekiNo = dataHBKB1301.PropDtRireki.Rows(0).Item("RirekiNo")
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
    ''' 【履歴モード】初期表示用メインデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードで初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainDataForRireki(ByVal Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'CI共通情報履歴データ取得
            If GetCIInfoR(Adapter, Cn, dataHBKB1301) = False Then
                Return False
            End If

            'CI部所有機器履歴データ取得
            If GetCIBuyR(Adapter, Cn, dataHBKB1301) = False Then
                Return False
            End If

            '登録理由履歴データ取得（履歴モード）
            If GetRegReasonR(Adapter, Cn, dataHBKB1301) = False Then
                Return False
            End If

            '原因リンク履歴データ取得（履歴モード）
            If GetCauseLinkR(Adapter, Cn, dataHBKB1301) = False Then
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
    ''' 【履歴モード】CI共通情報履歴データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報データを取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetCIInfoR(ByVal Adapter As NpgsqlDataAdapter, _
                                ByVal Cn As NpgsqlConnection, _
                                ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtCIInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKB1301.SetSelectCIInfoRSql(Adapter, Cn, dataHBKB1301) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報履歴データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtCIInfo)

            'データが取得できなかった場合、エラー
            If dtCIInfo.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & B1301_E001, TBNM_CI_INFO_RTB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKB1301.PropDtCIInfo = dtCIInfo


            '終了ログ出力
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
    ''' 【履歴モード】CI部所有機器履歴データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI部所有機器履歴データを取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetCIBuyR(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtCIBuyR As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKB1301.SetSelectCIBuyRSql(Adapter, Cn, dataHBKB1301) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI部所有機器履歴データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtCIBuyR)

            'データが取得できなかった場合、エラー
            If dtCIBuyR.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & B1301_E001, TBNM_CI_BUY_RTB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKB1301.PropDtCIBuy = dtCIBuyR

            '終了ログ出力
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
            dtCIBuyR.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【履歴モード】原因リンク履歴データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>原因リンク履歴データを取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetCauseLinkR(ByVal Adapter As NpgsqlDataAdapter, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '取得用SQLの作成・設定
            If sqlHBKB1301.SetSelectCauseLinkRSql(Adapter, Cn, dataHBKB1301) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "原因リンク履歴取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB1301.PropDtMyCauseLink)


            '終了ログ出力
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
    ''' 【履歴モード】登録理由履歴データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録理由履歴データを取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetRegReasonR(ByVal Adapter As NpgsqlDataAdapter, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '取得用SQLの作成・設定
            If sqlHBKB1301.SetSelectRegReasonRSql(Adapter, Cn, dataHBKB1301) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "登録理由履歴データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB1301.PropDtRireki)


            '終了ログ出力
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
    ''' 【共通】フォームコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InitFormControl(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'スプレッド設定
            If SetVwControl(dataHBKB1301) = False Then
                Return False
            End If

            '隠しラベル非表示設定
            If SetHiddenLabelUnvisible(dataHBKB1301) = False Then
                Return False
            End If

            '処理モードに応じたフォームコントロール設定
            If SetFormControlPerProcMode(dataHBKB1301) = False Then
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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフォームコントロールの設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormControlPerProcMode(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'ログイン／ロックコントロール設定
            If SetLoginAndLockControl(dataHBKB1301) = False Then
                Return False
            End If

            'ヘッダ設定
            If SetHeaderControl(dataHBKB1301) = False Then
                Return False
            End If

            'フッタ設定
            If SetFooterControl(dataHBKB1301) = False Then
                Return False
            End If

            'タブページ設定
            If SetTabControl(dataHBKB1301) = False Then
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
    ''' 【共通】初期データ設定処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームコントロールに初期表示データを設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitDataToControl(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'ログイン／ロックデータ設定
            If SetDataToLoginAndLock(dataHBKB1301) = False Then
                Return False
            End If

            'ヘッダデータ設定
            If SetDataToHeader(dataHBKB1301) = False Then
                Return False
            End If

            'フッタデータ設定
            If SetDataToFooter(dataHBKB1301) = False Then
                Return False
            End If

            'タブコントロールデータ設定
            If SetDataToTabControl(dataHBKB1301) = False Then
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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLock(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1301

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToLoginAndLockForNew(dataHBKB1301) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetDataToLoginAndLockForEdit(dataHBKB1301) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetDataToLoginAndLockForRef(dataHBKB1301) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetDataToLoginAndLockForRireki(dataHBKB1301) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then      '参照モード

                    '参照モード用設定
                    If SetDataToLoginAndLockForRef(dataHBKB1301) = False Then
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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForNew(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1301.PropGrpLoginUser

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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForEdit(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1301.PropGrpLoginUser

                'ロック情報が取得できた場合
                If dataHBKB1301.PropDtCILock IsNot Nothing AndAlso dataHBKB1301.PropDtCILock.Rows.Count > 0 Then
                    'ロック開始日時
                    Dim dtmLockTime As DateTime = Nothing  '編集開始日時
                    If dataHBKB1301.PropDtCILock.Rows(0).Item("EdiTime").ToString() <> "" Then
                        dtmLockTime = dataHBKB1301.PropDtCILock.Rows(0).Item("EdiTime")
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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForRef(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1301.PropGrpLoginUser

                'ロック情報が取得できた場合
                If dataHBKB1301.PropDtCILock IsNot Nothing AndAlso dataHBKB1301.PropDtCILock.Rows.Count > 0 Then
                    'ロック開始日時
                    Dim dtmLockTime As DateTime = Nothing
                    If dataHBKB1301.PropDtCILock.Rows(0).Item("EdiTime").ToString() <> "" Then
                        dtmLockTime = dataHBKB1301.PropDtCILock.Rows(0).Item("EdiTime")
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
    ''' 【履歴モード】ログイン／ロックデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForRireki(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1301.PropGrpLoginUser

                'ロック開始日時 ※空文字のときはNothingを指定しないと落ちる
                Dim strLockTime As String = dataHBKB1301.propStrEdiTime
                If dataHBKB1301.PropDtCILock IsNot Nothing AndAlso dataHBKB1301.PropDtCILock.Rows.Count > 0 Then
                    .PropLockDate = dataHBKB1301.PropDtCILock.Rows(0).Item("EdiTime")
                ElseIf strLockTime = "" Or strLockTime = "0:00:00" Then
                    .PropLockDate = Nothing
                Else
                    .PropLockDate = DateTime.Parse(strLockTime)
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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ヘッダデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToHeader(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1301

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToHeaderForNew(dataHBKB1301) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetDataToHeaderForEdit(dataHBKB1301) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetDataToHeaderForRef(dataHBKB1301) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetDataToHeaderForRireki(dataHBKB1301) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then      '参照モード

                    '参照モード用設定
                    If SetDataToHeaderForRef(dataHBKB1301) = False Then
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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでヘッダデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToHeaderForNew(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1301

                'CI番号ラベル
                .PropLblCINmb.Text = ""

                'CI種別名ラベル
                If .PropDtCIKindMasta.Rows.Count > 0 Then
                    .PropLblCIKbnNM.Text = .PropDtCIKindMasta.Rows(0).Item("CIKbnNM")
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
    ''' 【編集モード】ヘッダデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでヘッダデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToHeaderForEdit(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1301



                'CI番号ラベル
                .PropLblCINmb.Text = .PropIntCINmb.ToString()

                'CI種別名ラベル
                If .PropDtCIKindMasta.Rows.Count > 0 Then
                    .PropLblCIKbnNM.Text = .PropDtCIKindMasta.Rows(0).Item("CIKbnNM")
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
    ''' 【参照モード】ヘッダデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでヘッダデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToHeaderForRef(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '編集モードヘッダデータ設定処理と同じ
            If SetDataToHeaderForEdit(dataHBKB1301) = False Then
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
    ''' 【履歴モード】ヘッダデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードでヘッダデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToHeaderForRireki(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try


            dataHBKB1301.PropLblValueRirekiNo.Text = dataHBKB1301.PropDtCIInfo.Rows(0).Item("rirekino")

            '編集モードヘッダデータ設定処理と同じ
            If SetDataToHeaderForEdit(dataHBKB1301) = False Then
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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フッタデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToFooter(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1301

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード
                    If SetDataToFooterForNew(dataHBKB1301) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetDataToFooterForEdit(dataHBKB1301) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetDataToFooterForRef(dataHBKB1301) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetDataToFooterForRireki(dataHBKB1301) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then      '参照モード

                    '参照モード用設定
                    If SetDataToFooterForRef(dataHBKB1301) = False Then
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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでフッタデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToFooterForNew(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1301

                '履歴IDラベル
                .PropLblRirekiNo.Text = ""

                '理由テキストボックス
                .PropTxtRegReason.Text = ""


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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでフッタデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToFooterForEdit(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1301

                '履歴番号（更新ID）ラベル ※最新の番号をセット
                .PropLblRirekiNo.Text = .PropDtRireki.Rows(0).Item("RirekiNo").ToString()

                '理由テキストボックス
                If .PropDtRireki.Rows.Count > 0 Then
                    .PropTxtRegReason.Text = .PropDtRireki.Rows(0).Item("RegReason")
                End If

                '原因リンク一覧
                .PropVwMngNmb.Sheets(0).DataSource = .PropDtMyCauseLink

                '履歴情報一覧
                .PropVwRegReason.Sheets(0).DataSource = .PropDtRireki

                '履歴情報の一行目(最新行)を青色に設定
                .PropVwRegReason.Sheets(0).Rows(0).BackColor = Color.SteelBlue
                '履歴情報の一行目(最新行)の文字色を白色に設定
                .PropVwRegReason.Sheets(0).Rows(0).ForeColor = Color.White


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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでフッタデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToFooterForRef(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '※編集モードフッタデータ設定処理
            If SetDataToFooterForEdit(dataHBKB1301) = False Then
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
    ''' 【履歴モード】フッタデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードでフッタデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToFooterForRireki(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '※編集モードフッタデータ設定処理
            If SetDataToFooterForEdit(dataHBKB1301) = False Then
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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControl(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetLoginAndLockControlForNew(dataHBKB1301) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetLoginAndLockControlForEdit(dataHBKB1301) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照（ロック）モード用設定
                        If SetLoginAndLockControlForRef(dataHBKB1301) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetLoginAndLockControlForRireki(dataHBKB1301) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_REF Then      '参照モード

                    '参照（ロック）モード用設定
                    If SetLoginAndLockControlForRef(dataHBKB1301) = False Then
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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForNew(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301.PropGrpLoginUser

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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForEdit(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301.PropGrpLoginUser

                'グループコンボボックス非活性
                .cmbGroup.Enabled = False

                '変更ボタン非活性
                .btnChange.Enabled = False

                'ロック情報表示
                .PropLockInfoVisible = True

                '解除ボタン表示
                .PropBtnUnlockVisible = True
                'ロック情報が取得できた場合
                If dataHBKB1301.PropDtCILock IsNot Nothing AndAlso dataHBKB1301.PropDtCILock.Rows.Count > 0 Then

                    '編集者と同じグループの場合は解除ボタン活性化
                    If .cmbGroup.SelectedValue = dataHBKB1301.PropDtCILock.Rows(0).Item("EdiGrpCD") And _
                       PropUserId <> dataHBKB1301.PropDtCILock.Rows(0).Item("EdiID") Then
                        .PropBtnUnlockEnabled = True
                    Else
                        .PropBtnUnlockEnabled = False
                    End If

                Else

                    '解除ボタン非活性
                    .PropBtnUnlockEnabled = False

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
    ''' 【参照モード】ログイン／ロックコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForRef(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301.PropGrpLoginUser

                'グループコンボボックス非活性
                .cmbGroup.Enabled = False

                '変更ボタン非活性
                .btnChange.Enabled = False

                'ロック情報表示
                .PropLockInfoVisible = True

                '解除ボタン表示
                .PropBtnUnlockVisible = True

                'ロック情報が取得できた場合
                If dataHBKB1301.PropDtCILock IsNot Nothing AndAlso dataHBKB1301.PropDtCILock.Rows.Count > 0 Then

                    '編集者と同じグループの場合は解除ボタン活性化
                    If .cmbGroup.SelectedValue = dataHBKB1301.PropDtCILock.Rows(0).Item("EdiGrpCD") Then
                        .PropBtnUnlockEnabled = True
                    Else
                        .PropBtnUnlockEnabled = False
                    End If

                Else

                    '解除ボタン非活性
                    .PropBtnUnlockEnabled = False

                End If

                'ロック解除から遷移してきた場合は解除ボタンを非活性
                If dataHBKB1301.PropBlnLockCompare = True Then
                    '解除ボタン非活性
                    .PropBtnUnlockEnabled = False
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
    ''' 【履歴モード】ログイン／ロックコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForRireki(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301.PropGrpLoginUser

                'グループコンボボックス非活性
                .cmbGroup.Enabled = False

                '変更ボタン非活性
                .btnChange.Enabled = False

                'ロック情報表示
                .PropLockInfoVisible = True

                '解除ボタン非活性
                .PropBtnUnlockVisible = True
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
    ''' 【共通】ヘッダコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてヘッダコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/03 s.turuta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetHeaderControl(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301


                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetHeaderControlForNew(dataHBKB1301) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetHeaderControlForEdit(dataHBKB1301) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetHeaderControlForRef(dataHBKB1301) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetHeaderControlForRireki(dataHBKB1301) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then      '参照モード

                    '参照モード用設定
                    If SetHeaderControlForRef(dataHBKB1301) = False Then
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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでヘッダコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetHeaderControlForNew(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301

                '履歴Noタイトルラベル
                .PropLblTitleRirekiNo.Visible = False

                '履歴No値ラベル
                .PropLblValueRirekiNo.Visible = False

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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでヘッダコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetHeaderControlForEdit(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301

                '履歴Noタイトルラベル
                .PropLblTitleRirekiNo.Visible = False

                '履歴No値ラベル
                .PropLblValueRirekiNo.Visible = False

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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでヘッダコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetHeaderControlForRef(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301

                '履歴Noタイトルラベル
                .PropLblTitleRirekiNo.Visible = False

                '履歴No値ラベル
                .PropLblValueRirekiNo.Visible = False

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
    ''' 【履歴モード】ヘッダコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでヘッダコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetHeaderControlForRireki(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301

                'ヘッダグループボックスサイズ拡張
                .PropGrpCIKhn.Width = WIDTH_GROUPBOX_CIKHN

                '履歴Noタイトルラベル
                .PropLblTitleRirekiNo.Visible = True

                '履歴No値ラベル
                .PropLblValueRirekiNo.Visible = True

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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControl(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetFooterControlForNew(dataHBKB1301) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetFooterControlForEdit(dataHBKB1301) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetFooterControlForRef(dataHBKB1301) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetFooterControlForRireki(dataHBKB1301) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then      '参照モード

                    '参照モード用設定
                    If SetFooterControlForRef(dataHBKB1301) = False Then
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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForNew(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301

                'ロールバックボタン非表示
                .PropBtnRollBack.Visible = False

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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForEdit(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301

                '登録ボタン活性化
                .PropBtnReg.Enabled = True

                'ロールバックボタン非表示
                .PropBtnRollBack.Visible = False

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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForRef(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301

                '登録ボタン非活性
                .PropBtnReg.Enabled = False

                'ロールバックボタン非表示
                .PropBtnRollBack.Visible = False

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
    ''' 【履歴モード】フッタコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForRireki(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301

                '登録ボタン非表示
                .PropBtnReg.Visible = False

                '呼び出し元が部所有機器登録画面で編集モードでない場合、ロールバックボタン非活性
                If .PropIntFromRegDocFlg = 0 Or .PropBlnBeLockedFlg = True Then
                    .PropBtnRollBack.Enabled = False
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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてタブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControl(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '基本情報タブ設定
            If SetTabControlKhn(dataHBKB1301) = False Then
                Return False
            End If

            '利用情報タブ設定
            If SetTabControlRiyo(dataHBKB1301) = False Then
                Return False
            End If

            'フリー入力情報タブ設定
            If SetTabControlFree(dataHBKB1301) = False Then
                Return False
            End If

            '関係情報タブ設定
            If SetTabControlRelation(dataHBKB1301) = False Then
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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhn(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetTabControlKhnForNew(dataHBKB1301) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetTabControlKhnForEdit(dataHBKB1301) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照（ロック）モード用設定
                        If SetTabControlKhnForRef(dataHBKB1301) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetTabControlKhnForRireki(dataHBKB1301) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then      '参照モード

                    '参照（ロック）モード用設定
                    If SetTabControlKhnForRef(dataHBKB1301) = False Then
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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードで応じて基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForNew(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301

                '種別コンボボックス
                .PropCmbKind.Enabled = False


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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForEdit(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301

                '種別コンボボックス
                .PropCmbKind.Enabled = False

                '番号
                .PropTxtNum.ReadOnly = False

                '分類1
                .PropTxtClass1.ReadOnly = False

                '分類2(メーカー)
                .PropTxtClass2.ReadOnly = False

                '名称（機種）
                .PropTxtCINM.ReadOnly = False

                '型番
                .ProptxtKataban.ReadOnly = False

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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForRef(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301

                '種別コンボボックス
                .PropCmbKind.Enabled = False

                '番号
                .PropTxtNum.ReadOnly = False

                '分類1
                .PropTxtClass1.ReadOnly = False

                '分類2(メーカー)
                .PropTxtClass2.ReadOnly = False

                '名称（機種）
                .PropTxtCINM.ReadOnly = False

                '型番
                .ProptxtKataban.ReadOnly = False

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
    ''' 【履歴モード】基本情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForRireki(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301


                '種別コンボボックス
                .PropCmbKind.Enabled = False

                '番号テキストボックス
                .PropTxtNum.ReadOnly = True

                '分類１～２テキストボックス
                .PropTxtClass1.ReadOnly = True
                .PropTxtClass2.ReadOnly = True

                '名称テキストボックス
                .PropTxtCINM.ReadOnly = True

                '型番
                .ProptxtKataban.ReadOnly = True

                'ステータスコンボボックス
                .PropCmbCIStatus.Enabled = False

                'エイリアス
                .ProptxtAliau.ReadOnly = True

                '製造番号
                .PropTxtSerial.ReadOnly = True

                'NIC1
                .ProptxtNIC1.ReadOnly = True

                'MacAddress1
                .ProptxtMacaddress1.ReadOnly = True

                'NIC2
                .ProptxtNIC2.ReadOnly = True

                'MacAddress2
                .ProptxtMacaddress2.ReadOnly = True

                'OS名
                .PropcmbOSCD.Enabled = False

                'ウィルス対策ソフト
                .PropcmbAntiVirusSoftCD.Enabled = False

                '接続日
                .PropdtpConnectDT.Enabled = False

                '有効日
                .PropdtpExpirationDT.Enabled = False

                '最終お知らせ日
                .PropdtpLastInfoDT.Enabled = False

                '更新日
                .PropdtpExpirationUPDT.Enabled = False

                '通知日
                .PropdtpInfoDT.Enabled = False

                '停止日
                .PropdtpDeletDT.Enabled = False

                'DNS登録
                .PropcmbDNSRegCD.Enabled = False

                'zoo参加有無
                .PropcmbZooKbn.Enabled = False

                '番号通知
                .PropcmbNumInfoKbn.Enabled = False

                'シール送付
                .PropcmbSealSendkbn.Enabled = False

                'ウィルス対策ソフト確認
                .PropcmbAntiVirusSofCheckKbn.Enabled = False

                'ウィルス対策ソフトサーバー確認日
                .PropDtpAntiVirusSofCheckDT.Enabled = False

                '接続理由
                .ProptxtConectReason.ReadOnly = True

                '部所有機器備考
                .ProptxtBusyoKikiBiko.ReadOnly = True

                '説明テキストボックス
                .ProptxtCINaiyo.ReadOnly = True



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
    ''' 【共通】利用情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて利用情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRiyo(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetTabControlRiyoForNew(dataHBKB1301) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetTabControlRiyoForEdit(dataHBKB1301) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照（ロック）モード用設定
                        If SetTabControlRiyoForRef(dataHBKB1301) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetTabControlRiyoForRireki(dataHBKB1301) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then      '参照モード

                    '参照（ロック）モード用設定
                    If SetTabControlRiyoForRef(dataHBKB1301) = False Then
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
    ''' 【新規登録モード】利用情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードで応じて利用情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRiyoForNew(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301

                '種別コンボボックス
                .PropCmbKind.Enabled = False


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
    ''' 【編集モード】利用情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで利用情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRiyoForEdit(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301

                '種別コンボボックス
                .PropCmbKind.Enabled = False


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
    ''' 【参照モード】利用情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで利用情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRiyoForRef(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301

                'ユーザー検索ボタン
                .PropbtnUsr.Enabled = False

                '設置部署検索ボタン
                .PropBtnSet.Enabled = False

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
    ''' 【履歴モード】利用情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードで利用情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRiyoForRireki(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301

                'ユーザーID
                .ProptxtUsrID.ReadOnly = True

                'ユーザー氏名
                .ProptxtUsrNM.ReadOnly = True

                'ユーザーメールアドレス
                .ProptxtUsrMailAdd.ReadOnly = True

                'ユーザー電話番号
                .ProptxtUsrTel.ReadOnly = True

                'ユーザー所属局
                .ProptxtUsrKyokuNM.ReadOnly = True

                'ユーザー所属部署
                .ProptxtUsrBusyoNM.ReadOnly = True

                'ユーザー所属会社
                .ProptxtUsrCompany.ReadOnly = True

                'ユーザー連絡先
                .ProptxtUsrContact.ReadOnly = True

                'ユーザー番組/部屋
                .ProptxtUsrRoom.ReadOnly = True

                '管理局
                .ProptxtManageKyokuNM.ReadOnly = True

                '管理部署
                .ProptxtManageBusyoNM.ReadOnly = True

                '作業の元
                .ProptxtWorkFromNmb.ReadOnly = True

                '固定IP
                .ProptxtFixedIP.ReadOnly = True

                'IP割当種類
                .PropcmbIPUseCD.Enabled = False

                '設置局
                .ProptxtSetKyokuNM.ReadOnly = True

                '設置部署
                .ProptxtSetBusyoNM.ReadOnly = True

                '設置番組/部屋
                .ProptxtSetRoom.ReadOnly = True

                '設置建物
                .ProptxtSetBuil.ReadOnly = True

                '設置フロア
                .ProptxtSetFloor.ReadOnly = True

                'ユーザー検索ボタン
                .PropbtnUsr.Enabled = False

                '設置部署検索ボタン
                .PropBtnSet.Enabled = False

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
    ''' 【共通】フリー入力情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフリー入力情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlFree(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301


                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モードなし


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード

                    '編集／参照（ロック）モードなし


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetTabControlFreeForRireki(dataHBKB1301) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then      '参照モード

                    '編集／参照（ロック）モードなし

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
    ''' 【履歴モード】フリー入力情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフリー入力情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlFreeForRireki(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301

                'フリーテキスト１～５テキストボックス
                .PropTxtBIko1.ReadOnly = True
                .PropTxtBIko2.ReadOnly = True
                .PropTxtBIko3.ReadOnly = True
                .PropTxtBIko4.ReadOnly = True
                .PropTxtBIko5.ReadOnly = True

                'フリーフラグ１～５チェックボックス
                .PropChkFreeFlg1.Enabled = False
                .PropChkFreeFlg2.Enabled = False
                .PropChkFreeFlg3.Enabled = False
                .PropChkFreeFlg4.Enabled = False
                .PropChkFreeFlg5.Enabled = False

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
    ''' 【共通】関係情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて関係情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRelation(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モードなし


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetTabControlRelationForEdit(dataHBKB1301) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照（ロック）モード用設定
                        If SetTabControlRelationForRef(dataHBKB1301) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    If SetTabControlRelationForRireki(dataHBKB1301) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then      '参照モード

                    '参照（ロック）モード用設定
                    If SetTabControlRelationForRef(dataHBKB1301) = False Then
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
    ''' 【編集モード】関係情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで関係情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRelationForEdit(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301

                '検索ボタン
                .PropBtnSearchGrp.Enabled = True


                .PropLblCIOwnerCD.Visible = False

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
    ''' 【参照モード】関係情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで関係情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRelationForRef(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301

                '検索ボタン
                .PropBtnSearchGrp.Enabled = False

                .PropLblCIOwnerCD.Visible = False

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
    ''' 【履歴モード】関係情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードで関係情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRelationForRireki(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301

                'CIオーナー名テキストボックス
                .PropTxtCIOwnerNM.ReadOnly = True

                .PropLblCIOwnerCD.Visible = False

                '検索ボタン
                .PropBtnSearchGrp.Enabled = False

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
    ''' 【共通】タブコントロールデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>タブコントロールデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabControl(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '基本情報タブデータ設定
            If SetDataToTabKhn(dataHBKB1301) = False Then
                Return False
            End If

            '利用情報タブデータ設定
            If SetDataToTabRiyo(dataHBKB1301) = False Then
                Return False
            End If

            'フリー入力タブデータ設定
            If SetDataToTabFree(dataHBKB1301) = False Then
                Return False
            End If

            '関係情報タブデータ設定
            If SetDataToTabRelation(dataHBKB1301) = False Then
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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhn(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1301

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToTabKhnForNew(dataHBKB1301) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetDataToTabKhnForEdit(dataHBKB1301) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetDataToTabKhnForRef(dataHBKB1301) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetDataToTabKhnForRireki(dataHBKB1301) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then      '参照モード

                    '参照モード用設定
                    If SetDataToTabKhnForRef(dataHBKB1301) = False Then
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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードで基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhnForNew(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'コンボボックス作成
            If CreateCmb(dataHBKB1301) = False Then
                Return False
            End If

            With dataHBKB1301

                '番号テキストボックス
                .PropTxtNum.Text = ""

                '分類１～２テキストボックス
                .PropTxtClass1.Text = ""
                .PropTxtClass2.Text = ""

                '名称テキストボックス
                .PropTxtCINM.Text = ""

                '型番
                .ProptxtKataban.Text = ""

                'ステータスコンボボックス
                .PropCmbCIStatus.SelectedValue = ""

                'エイリアス
                .ProptxtAliau.Text = ""

                '製造番号
                .PropTxtSerial.Text = ""

                'NIC1
                .ProptxtNIC1.Text = ""

                'MacAddress1
                .ProptxtMacaddress1.Text = ""

                'NIC2
                .ProptxtNIC2.Text = ""

                'MacAddress2
                .ProptxtMacaddress2.Text = ""

                'OS名
                .PropcmbOSCD.SelectedValue = 0

                'ウィルス対策ソフト
                .PropcmbAntiVirusSofCheckKbn.SelectedValue = ""

                '接続日
                .PropdtpConnectDT.txtDate.Text = ""

                '有効日
                .PropdtpExpirationDT.txtDate.Text = ""

                '最終お知らせ日
                .PropdtpLastInfoDT.txtDate.Text = ""

                '更新日
                .PropdtpExpirationUPDT.txtDate.Text = ""

                '通知日
                .PropdtpInfoDT.txtDate.Text = ""

                '停止日
                .PropdtpDeletDT.txtDate.Text = ""

                'DNS登録：有
                .PropcmbDNSRegCD.SelectedValue = DNS_KBN_FIN

                'zoo参加有無
                .PropcmbZooKbn.SelectedValue = ZOO_KBN_FIN

                '番号通知
                .PropcmbNumInfoKbn.SelectedValue = 0

                'シール送付
                .PropcmbSealSendkbn.SelectedValue = 0

                'ウィルス対策ソフト確認
                .PropcmbAntiVirusSofCheckKbn.SelectedValue = 0

                'ウィルス対策ソフトサーバー確認日
                .PropDtpAntiVirusSofCheckDT.txtDate.Text = ""

                '接続理由
                .ProptxtConectReason.Text = ""

                '部所有機器備考
                .ProptxtBusyoKikiBiko.Text = ""

                '説明テキストボックス
                .ProptxtCINaiyo.Text = ""

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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhnForEdit(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'コンボボックス作成
            If CreateCmb(dataHBKB1301) = False Then
                Return False
            End If

            With dataHBKB1301

                '種別コンボボックス
                .PropCmbKind.Enabled = False

                '番号テキストボックス
                .PropTxtNum.Text = .PropDtCIInfo.Rows(0).Item("Num")

                '分類１～２テキストボックス
                .PropTxtClass1.Text = .PropDtCIInfo.Rows(0).Item("Class1")
                .PropTxtClass2.Text = .PropDtCIInfo.Rows(0).Item("Class2")

                '名称テキストボックス
                .PropTxtCINM.Text = .PropDtCIInfo.Rows(0).Item("CINM")

                '型番
                .ProptxtKataban.Text = .PropDtCIBuy.Rows(0).Item("Kataban")

                'ステータスコンボボックス
                .PropCmbCIStatus.SelectedValue = .PropDtCIInfo.Rows(0).Item("CIStatusCD")

                'エイリアス
                .ProptxtAliau.Text = .PropDtCIBuy.Rows(0).Item("Aliau")

                '製造番号
                .PropTxtSerial.Text = .PropDtCIBuy.Rows(0).Item("Serial")

                'NIC1
                .ProptxtNIC1.Text = .PropDtCIBuy.Rows(0).Item("NIC1")

                'MacAddress1
                .ProptxtMacaddress1.Text = .PropDtCIBuy.Rows(0).Item("MacAddress1")

                'NIC2
                .ProptxtNIC2.Text = .PropDtCIBuy.Rows(0).Item("NIC2")

                'MacAddress2
                .ProptxtMacaddress2.Text = .PropDtCIBuy.Rows(0).Item("MacAddress2")

                'OS名
                .PropcmbOSCD.Text = .PropDtCIBuy.Rows(0).Item("OSNM")

                'ウィルス対策ソフト
                .PropcmbAntiVirusSoftCD.Text = .PropDtCIBuy.Rows(0).Item("AntiVirusSoftNM")

                '接続日
                .PropdtpConnectDT.txtDate.Text = .PropDtCIBuy.Rows(0).Item("ConnectDT")

                '有効日
                .PropdtpExpirationDT.txtDate.Text = .PropDtCIBuy.Rows(0).Item("ExpirationDT")

                '最終お知らせ日
                .PropdtpLastInfoDT.txtDate.Text = .PropDtCIBuy.Rows(0).Item("LastInfoDT")

                '更新日
                .PropdtpExpirationUPDT.txtDate.Text = .PropDtCIBuy.Rows(0).Item("ExpirationUPDT")

                '通知日
                .PropdtpInfoDT.txtDate.Text = .PropDtCIBuy.Rows(0).Item("InfoDT")

                '停止日
                .PropdtpDeletDT.txtDate.Text = .PropDtCIBuy.Rows(0).Item("DeletDT")

                'DNS登録
                .PropcmbDNSRegCD.SelectedValue = .PropDtCIBuy.Rows(0).Item("DNSRegCD")

                'zoo参加有無
                .PropcmbZooKbn.SelectedValue = .PropDtCIBuy.Rows(0).Item("ZooKbn")

                '番号通知
                .PropcmbNumInfoKbn.SelectedValue = .PropDtCIBuy.Rows(0).Item("NumInfoKbn")

                'シール送付
                .PropcmbSealSendkbn.SelectedValue = .PropDtCIBuy.Rows(0).Item("SealSendkbn")

                'ウィルス対策ソフト確認
                .PropcmbAntiVirusSofCheckKbn.SelectedValue = .PropDtCIBuy.Rows(0).Item("AntiVirusSofCheckKbn")

                'ウィルス対策ソフトサーバー確認日
                .PropDtpAntiVirusSofCheckDT.txtDate.Text = .PropDtCIBuy.Rows(0).Item("AntiVirusSofCheckDT")

                '接続理由
                .ProptxtConectReason.Text = .PropDtCIBuy.Rows(0).Item("ConectReason")

                '部所有機器備考
                .ProptxtBusyoKikiBiko.Text = .PropDtCIBuy.Rows(0).Item("BusyoKikiBiko")

                '説明テキストボックス
                .ProptxtCINaiyo.Text = .PropDtCIInfo.Rows(0).Item("CINaiyo")

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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhnForRef(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '編集モード基本情報タブデータ設定処理と同じ
            If SetDataToTabKhnForEdit(dataHBKB1301) = False Then
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
    ''' 【履歴モード】基本情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードで基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhnForRireki(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '編集モード基本情報タブデータ設定処理と同じ
            If SetDataToTabKhnForEdit(dataHBKB1301) = False Then
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
    ''' 【共通】利用情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて利用情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabRiyo(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1301

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToTabRiyoForNew(dataHBKB1301) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetDataToTabRiyoForEdit(dataHBKB1301) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetDataToTabRiyoForRef(dataHBKB1301) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetDataToTabRiyoForRireki(dataHBKB1301) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then      '参照モード

                    '参照モード用設定
                    If SetDataToTabRiyoForRef(dataHBKB1301) = False Then
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
    ''' 【新規登録モード】利用情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードで利用情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabRiyoForNew(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1301

                '種別ラベル
                .ProplblCIKind.Text = ""

                '番号ラベル
                .ProplblNum.Text = ""

                'ユーザーID
                .ProptxtUsrID.Text = ""

                'ユーザー氏名
                .ProptxtUsrNM.Text = ""

                'ユーザーメールアドレス
                .ProptxtUsrMailAdd.Text = ""

                'ユーザー電話番号
                .ProptxtUsrTel.Text = ""

                'ユーザー所属局
                .ProptxtUsrKyokuNM.Text = ""

                'ユーザー所属部署
                .ProptxtUsrBusyoNM.Text = ""

                'ユーザー所属会社
                .ProptxtUsrCompany.Text = ""

                'ユーザー連絡先
                .ProptxtUsrContact.Text = ""

                'ユーザー番組/部屋
                .ProptxtUsrRoom.Text = ""

                '管理局
                .ProptxtManageKyokuNM.Text = ""

                '管理部署
                .ProptxtManageBusyoNM.Text = ""

                '作業の元
                .ProptxtWorkFromNmb.Text = ""

                '固定IP
                .ProptxtFixedIP.Text = ""

                'IP割当種類
                .PropcmbIPUseCD.SelectedValue = ""

                '設置局
                .ProptxtSetKyokuNM.Text = ""

                '設置部署
                .ProptxtSetBusyoNM.Text = ""

                '設置番組/部屋
                .ProptxtSetRoom.Text = ""

                '設置建物
                .ProptxtSetBuil.Text = ""

                '設置フロア
                .ProptxtSetFloor.Text = ""

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
    ''' 【編集モード】利用情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで利用情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabRiyoForEdit(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1301

                '種別ラベル
                .ProplblCIKind.Text = .PropCmbKind.Text

                '番号ラベル
                .ProplblNum.Text = .PropTxtNum.Text

                'ユーザーID
                .ProptxtUsrID.Text = .PropDtCIBuy.Rows(0).Item("UsrID")

                'ユーザー氏名
                .ProptxtUsrNM.Text = .PropDtCIBuy.Rows(0).Item("UsrNM")

                'ユーザーメールアドレス
                .ProptxtUsrMailAdd.Text = .PropDtCIBuy.Rows(0).Item("UsrMailAdd")

                'ユーザー電話番号
                .ProptxtUsrTel.Text = .PropDtCIBuy.Rows(0).Item("UsrTel")

                'ユーザー所属局
                .ProptxtUsrKyokuNM.Text = .PropDtCIBuy.Rows(0).Item("UsrKyokuNM")

                'ユーザー所属部署
                .ProptxtUsrBusyoNM.Text = .PropDtCIBuy.Rows(0).Item("UsrBusyoNM")

                'ユーザー所属会社
                .ProptxtUsrCompany.Text = .PropDtCIBuy.Rows(0).Item("UsrCompany")

                'ユーザー連絡先
                .ProptxtUsrContact.Text = .PropDtCIBuy.Rows(0).Item("UsrContact")

                'ユーザー番組/部屋
                .ProptxtUsrRoom.Text = .PropDtCIBuy.Rows(0).Item("UsrRoom")

                '管理局
                .ProptxtManageKyokuNM.Text = .PropDtCIBuy.Rows(0).Item("ManageKyokuNM")

                '管理部署
                .ProptxtManageBusyoNM.Text = .PropDtCIBuy.Rows(0).Item("ManageBusyoNM")

                '作業の元
                .ProptxtWorkFromNmb.Text = .PropDtCIBuy.Rows(0).Item("WorkFromNmb")

                '固定IP
                .ProptxtFixedIP.Text = .PropDtCIBuy.Rows(0).Item("FixedIP")

                'IP割当種類
                .PropcmbIPUseCD.SelectedValue = .PropDtCIBuy.Rows(0).Item("IPUseCD")

                '設置局
                .ProptxtSetKyokuNM.Text = .PropDtCIBuy.Rows(0).Item("SetKyokuNM")

                '設置部署
                .ProptxtSetBusyoNM.Text = .PropDtCIBuy.Rows(0).Item("SetBusyoNM")

                '設置番組/部屋
                .ProptxtSetRoom.Text = .PropDtCIBuy.Rows(0).Item("SetRoom")

                '設置建物
                .ProptxtSetBuil.Text = .PropDtCIBuy.Rows(0).Item("SetBuil")

                '設置フロア
                .ProptxtSetFloor.Text = .PropDtCIBuy.Rows(0).Item("SetFloor")



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
    ''' 【参照モード】利用情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで利用情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabRiyoForRef(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '編集モード利用情報タブデータ設定処理と同じ
            If SetDataToTabRiyoForEdit(dataHBKB1301) = False Then
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
    ''' 【履歴モード】利用情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードで利用情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabRiyoForRireki(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '編集モード利用情報タブデータ設定処理と同じ
            If SetDataToTabRiyoForEdit(dataHBKB1301) = False Then
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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFree(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1301

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToTabFreeForNew(dataHBKB1301) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetDataToTabFreeForEdit(dataHBKB1301) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetDataToTabFreeForRef(dataHBKB1301) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetDataToTabFreeForRireki(dataHBKB1301) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then      '参照モード

                    '参照モード用設定
                    If SetDataToTabFreeForRef(dataHBKB1301) = False Then
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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFreeForNew(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1301

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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFreeForEdit(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1301

                'フリーテキスト１～５テキストボックス
                .PropTxtBIko1.Text = .PropDtCIInfo.Rows(0).Item("BIko1")
                .PropTxtBIko2.Text = .PropDtCIInfo.Rows(0).Item("BIko2")
                .PropTxtBIko3.Text = .PropDtCIInfo.Rows(0).Item("BIko3")
                .PropTxtBIko4.Text = .PropDtCIInfo.Rows(0).Item("BIko4")
                .PropTxtBIko5.Text = .PropDtCIInfo.Rows(0).Item("BIko5")

                'フリーフラグ１～５チェックボックス
                If .PropDtCIInfo.Rows(0).Item("FreeFlg1") = FREE_FLG_ON Then
                    .PropChkFreeFlg1.Checked = True
                ElseIf .PropDtCIInfo.Rows(0).Item("FreeFlg1") = FREE_FLG_OFF Then
                    .PropChkFreeFlg1.Checked = False
                End If
                If .PropDtCIInfo.Rows(0).Item("FreeFlg2") = FREE_FLG_ON Then
                    .PropChkFreeFlg2.Checked = True
                ElseIf .PropDtCIInfo.Rows(0).Item("FreeFlg2") = FREE_FLG_OFF Then
                    .PropChkFreeFlg2.Checked = False
                End If
                If .PropDtCIInfo.Rows(0).Item("FreeFlg3") = FREE_FLG_ON Then
                    .PropChkFreeFlg3.Checked = True
                ElseIf .PropDtCIInfo.Rows(0).Item("FreeFlg3") = FREE_FLG_OFF Then
                    .PropChkFreeFlg3.Checked = False
                End If
                If .PropDtCIInfo.Rows(0).Item("FreeFlg4") = FREE_FLG_ON Then
                    .PropChkFreeFlg4.Checked = True
                ElseIf .PropDtCIInfo.Rows(0).Item("FreeFlg4") = FREE_FLG_OFF Then
                    .PropChkFreeFlg4.Checked = False
                End If
                If .PropDtCIInfo.Rows(0).Item("FreeFlg5") = FREE_FLG_ON Then
                    .PropChkFreeFlg5.Checked = True
                ElseIf .PropDtCIInfo.Rows(0).Item("FreeFlg5") = FREE_FLG_OFF Then
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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFreeForRef(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '編集モードフリー入力タブデータ設定処理と同じ
            If SetDataToTabFreeForEdit(dataHBKB1301) = False Then
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
    ''' 【履歴モード】フリー入力タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードでフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFreeForRireki(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '編集モードフリー入力タブデータ設定処理と同じ
            If SetDataToTabFreeForEdit(dataHBKB1301) = False Then
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
    ''' 【共通】関係情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて関係情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabRelation(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1301

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToTabRelationForNew(dataHBKB1301) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetDataToTabRelationForEdit(dataHBKB1301) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetDataToTabRelationForRef(dataHBKB1301) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetDataToTabRelationForRireki(dataHBKB1301) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then      '参照モード

                    '参照モード用設定
                    If SetDataToTabRelationForRef(dataHBKB1301) = False Then
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
    ''' 【新規登録モード】関係情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードで関係情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabRelationForNew(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1301

                'CIオーナー名テキストボックス
                .PropTxtCIOwnerNM.Text = ""

                'CIオーナーCDラベル
                .PropLblCIOwnerCD.Text = ""

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
    ''' 【編集モード】関係情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで関係情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabRelationForEdit(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1301

                'CIオーナー名テキストボックス
                .PropTxtCIOwnerNM.Text = .PropDtCIInfo.Rows(0).Item("GroupNM").ToString()

                'CIオーナーCDラベル
                .PropLblCIOwnerCD.Text = .PropDtCIInfo.Rows(0).Item("CIOwnerCD").ToString()

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
    ''' 【参照モード】関係情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで関係情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabRelationForRef(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '編集モード関係情報タブデータ設定処理と同じ
            If SetDataToTabRelationForEdit(dataHBKB1301) = False Then
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
    ''' 【履歴モード】関係情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで関係情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabRelationForRireki(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '編集モード関係情報タブデータ設定処理と同じ
            If SetDataToTabRelationForEdit(dataHBKB1301) = False Then
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
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>各スプレッド（一覧）を初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetVwControl(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1301


                '原因リンク一覧
                With .PropVwMngNmb.Sheets(0)
                    .DataSource = dataHBKB1301.PropDtMyCauseLink
                    .Columns(COL_CAUSELINK_KBN_NMR).DataField = "ProcessKbnNMR"
                    .Columns(COL_CAUSELINK_NO).DataField = "MngNmb"
                    .Columns(COL_CAUSELINK_KBN).DataField = "ProcessKbn"
                    .Columns(COL_CAUSELINK_KBN).Visible = False
                End With

                '履歴情報一覧
                With .PropVwRegReason.Sheets(0)
                    .DataSource = dataHBKB1301.PropDtRireki
                    .Columns(COL_REGREASON_UPID).DataField = "RirekiNo"
                    .Columns(COL_REGREASON_UPDT).DataField = "RegDT"
                    .Columns(COL_REGREASON_UPGROUPNM).DataField = "GroupNM"
                    .Columns(COL_REGREASON_UPUSERNM).DataField = "HBKUsrNM"
                    .Columns(COL_REGREASON_REASON).DataField = "RegReason"
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
    ''' 【共通】隠しラベル非表示処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]　部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システムコード保持用の隠しラベルを非表示にする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetHiddenLabelUnvisible(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB1301

                'CIオーナーコードラベル
                .PropLblCIOwnerCD.Visible = False

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
    ''' 【参照モード】ロック解除時ログ出力処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>他のユーザによりロックが解除された場合に編集中の入力内容をログに出力する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function OutputUnlockLog(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strPlmList As New List(Of String)       'フォーマット埋込用パラメータ配列
        Dim strLogFilePath As String = ""           'ログファイルパス
        Dim strLogFileName As String                'ログファイル名
        Dim strOutputDir As String                  'ログ出力フォルダ
        Dim sw As StreamWriter = Nothing            'ファイル書込用クラス

        Try
            With dataHBKB1301

                '書込用テキスト作成
                strPlmList.Add(.PropLblCINmb.Text)                                          'CI番号
                strPlmList.Add(.PropLblCIKbnNM.Text)                                        'CI種別
                strPlmList.Add(.PropCmbKind.Text)                                           '種別
                strPlmList.Add(.PropTxtNum.Text)                                            '番号（手動）
                strPlmList.Add(.PropTxtClass1.Text)                                         '分類1
                strPlmList.Add(.PropTxtClass2.Text)                                         '分類2
                strPlmList.Add(.PropTxtCINM.Text)                                           '名称
                strPlmList.Add(.ProptxtKataban.Text)                                        '型番
                strPlmList.Add(.PropCmbCIStatus.Text)                                       'ステータス
                strPlmList.Add(.ProptxtAliau.Text)                                          'エイリアス
                strPlmList.Add(.PropTxtSerial.Text)                                         '製造番号
                strPlmList.Add(.ProptxtNIC1.Text)                                           'NIC1
                strPlmList.Add(.ProptxtMacaddress1.Text)                                    'MACアドレス1
                strPlmList.Add(.ProptxtNIC2.Text)                                           'NIC2
                strPlmList.Add(.ProptxtMacaddress2.Text)                                    'MACアドレス2
                strPlmList.Add(.PropcmbOSCD.Text)                                           'OSCD
                strPlmList.Add(.PropcmbAntiVirusSoftCD.Text)                                'ウイルス対策ソフトCD
                strPlmList.Add(.PropdtpConnectDT.txtDate.Text)                              '接続日
                strPlmList.Add(.PropdtpExpirationDT.txtDate.Text)                           '有効日
                strPlmList.Add(.PropdtpLastInfoDT.txtDate.Text)                             '最終お知らせ日
                strPlmList.Add(.PropdtpExpirationUPDT.txtDate.Text)                         '更新日
                strPlmList.Add(.PropdtpInfoDT.txtDate.Text)                                 '通知日
                strPlmList.Add(.PropdtpDeletDT.txtDate.Text)                                '停止日
                strPlmList.Add(.PropcmbDNSRegCD.Text)                                       'DNS登録CD
                strPlmList.Add(.PropcmbZooKbn.Text)                                         'DNS登録CD
                strPlmList.Add(.PropcmbNumInfoKbn.Text)                                     '番号通知
                strPlmList.Add(.PropcmbSealSendkbn.Text)                                    'シール送付
                strPlmList.Add(.PropcmbAntiVirusSofCheckKbn.Text)                           'ウイルス対策ソフト確認
                strPlmList.Add(.PropDtpAntiVirusSofCheckDT.txtDate.Text)                    'ウイルス対策ソフトサーバー確認日
                strPlmList.Add(.ProptxtConectReason.Text)                                   '接続理由
                strPlmList.Add(.ProptxtBusyoKikiBiko.Text)                                  '部所有機器備考
                strPlmList.Add(.ProptxtCINaiyo.Text)                                        '説明
                strPlmList.Add(.ProptxtUsrID.Text)                                          'ユーザーID
                strPlmList.Add(.ProptxtUsrNM.Text)                                          'ユーザー氏名
                strPlmList.Add(.ProptxtUsrMailAdd.Text)                                     'ユーザーメールアドレス
                strPlmList.Add(.ProptxtUsrTel.Text)                                         'ユーザー電話番号
                strPlmList.Add(.ProptxtUsrKyokuNM.Text)                                     'ユーザー所属局
                strPlmList.Add(.ProptxtUsrBusyoNM.Text)                                     'ユーザー所属部署
                strPlmList.Add(.ProptxtUsrCompany.Text)                                     'ユーザー所属会社
                strPlmList.Add(.ProptxtUsrContact.Text)                                     'ユーザー連絡先
                strPlmList.Add(.ProptxtUsrRoom.Text)                                        'ユーザー番組/部屋
                strPlmList.Add(.ProptxtManageKyokuNM.Text)                                  '管理局
                strPlmList.Add(.ProptxtManageBusyoNM.Text)                                  '管理部署
                strPlmList.Add(.ProptxtWorkFromNmb.Text)                                    '作業の元
                strPlmList.Add(.ProptxtFixedIP.Text)                                        '固定IP
                strPlmList.Add(.PropcmbIPUseCD.Text)                                        'IP割当種類CD
                strPlmList.Add(.ProptxtSetKyokuNM.Text)                                     '設置局
                strPlmList.Add(.ProptxtSetBusyoNM.Text)                                     '設置部署
                strPlmList.Add(.ProptxtSetRoom.Text)                                        '設置番組/部屋
                strPlmList.Add(.ProptxtSetBuil.Text)                                        '設置建物
                strPlmList.Add(.ProptxtSetFloor.Text)                                       '設置フロア
                strPlmList.Add(.PropTxtBIko1.Text)                                          'フリーテキスト１
                strPlmList.Add(.PropTxtBIko2.Text)                                          'フリーテキスト２
                strPlmList.Add(.PropTxtBIko3.Text)                                          'フリーテキスト３
                strPlmList.Add(.PropTxtBIko4.Text)                                          'フリーテキスト４
                strPlmList.Add(.PropTxtBIko5.Text)                                          'フリーテキスト５

                'フリーフラグ１～５
                If .PropChkFreeFlg1.Checked = True Then
                    strPlmList.Add(FREE_FLG_ON_NM)                                          'フリーフラグ１
                Else
                    strPlmList.Add(FREE_FLG_OFF_NM)                                         'フリーフラグ１
                End If
                If .PropChkFreeFlg2.Checked = True Then
                    strPlmList.Add(FREE_FLG_ON_NM)                                          'フリーフラグ２
                Else
                    strPlmList.Add(FREE_FLG_OFF_NM)                                         'フリーフラグ２
                End If
                If .PropChkFreeFlg3.Checked = True Then
                    strPlmList.Add(FREE_FLG_ON_NM)                                          'フリーフラグ３
                Else
                    strPlmList.Add(FREE_FLG_OFF_NM)                                         'フリーフラグ３
                End If
                If .PropChkFreeFlg4.Checked = True Then
                    strPlmList.Add(FREE_FLG_ON_NM)                                          'フリーフラグ４
                Else
                    strPlmList.Add(FREE_FLG_OFF_NM)                                         'フリーフラグ４
                End If
                If .PropChkFreeFlg5.Checked = True Then
                    strPlmList.Add(FREE_FLG_ON_NM)                                          'フリーフラグ５
                Else
                    strPlmList.Add(FREE_FLG_OFF_NM)                                         'フリーフラグ５
                End If

                strPlmList.Add(.PropTxtCIOwnerNM.Text)                                      'CIオーナー名

                'ログ出力フォルダ設定
                strOutputDir = Path.Combine(Application.StartupPath, OUTPUT_DIR_UNLOCKEDLOG)

                'ログファイル名設定
                strLogFileName = Format(DateTime.Parse(.PropDtCILock.Rows(0).Item("EdiTime")), "yyyyMMddHHmmss") & ".log"

                'ファイル出力を実行し、出力ファイルパスを取得
                If commonLogicHBK.OutputLogFromTextFormat(strPlmList, strLogFileName, _
                                                          FORMAT_DIR_UNLOCKEDLOG, _
                                                          FILE_UNLOCKLOG_KIKI, _
                                                          strOutputDir, _
                                                          strLogFilePath) = False Then
                    Return False
                End If

                'データクラスにメッセージをセット
                dataHBKB1301.PropStrBeUnlockedMsg = String.Format(HBK_W001, strLogFilePath)

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
    ''' 【編集モード】ロック解除チェック処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータのロックが解除されていないかチェックする
    ''' <para>作成情報：2012/07/09 t.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckUnlock(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnBeUnocked As Boolean = False       'ロックフラグ

        Try

            With dataHBKB1301

                'ロック解除チェック
                If commonLogicHBK.CheckDataBeUnlocked(.PropIntCINmb, .PropGrpLoginUser.PropLockDate.ToString(), _
                                                      blnBeUnocked, .PropDtCILock) = False Then
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
    ''' 【新規登録モード】データ新規登録処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をDBに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertNewData(ByVal dataHBKB1301 As DataHBKB1301) As Boolean

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

            '新規CI番号、システム日付取得
            If SelectNewCINmbAndSysDate(Cn, dataHBKB1301) = False Then
                'ロールバック処理
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'CI共通情報新規登録
            If InsertCIInfo(Cn, dataHBKB1301) = False Then
                'ロールバック処理
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'CI部所有機器新規登録
            If InsertCIBuy(Cn, dataHBKB1301) = False Then
                'ロールバック処理
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '履歴情報新規登録（共通）
            If InsertRireki(Cn, dataHBKB1301) = False Then
                'ロールバック処理
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
    ''' 【編集／履歴モード】データ更新処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をDBに更新（Update）する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateData(ByVal dataHBKB1301 As DataHBKB1301) As Boolean

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
            If SelectSysDate(Adapter, Cn, dataHBKB1301) = False Then
                'ロールバック処理
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'CI共通情報更新（UPDATE）
            If UpdateCIInfo(Cn, dataHBKB1301) = False Then
                'ロールバック処理
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'CI部所有機器更新（UPDATE）
            If UpdateCIBuy(Cn, dataHBKB1301) = False Then
                'ロールバック処理
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '履歴情報新規登録（共通）
            If InsertRireki(Cn, dataHBKB1301) = False Then
                'ロールバック処理
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
    ''' 【編集モード】ロック解除処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータのロックを解除する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UnlockData(ByVal dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'CI共通情報ロック解除（DELETE）
            If commonLogicHBK.UnlockCIInfo(dataHBKB1301.PropIntCINmb) = False Then
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
    ''' 【新規登録モード】新規CI番号取得処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番したCI番号を取得（SELECT）する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectNewCINmbAndSysDate(ByVal Cn As NpgsqlConnection, _
                                              ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtResult As New DataTable

        Try
            '新規CI番号取得（SELECT）用SQLを作成
            If sqlHBKB1301.SetSelectNewCINmbAndSysDateSql(Adapter, Cn, dataHBKB1301) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規CI番号、システム日付取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            'データが取得できた場合、データクラスに取得データをセット
            If dtResult.Rows.Count > 0 Then
                dataHBKB1301.PropIntCINmb = dtResult.Rows(0).Item("CINmb")      '新規CI番号
                dataHBKB1301.PropDtmSysDate = dtResult.Rows(0).Item("SysDate")  'サーバー日付
            Else
                '取得できなかったときはエラー
                puErrMsg = B1301_E007
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
    ''' 【新規登録モード】CI共通情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をCI共通情報テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCIInfo(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CI共通情報新規登録（INSERT）用SQLを作成
            If sqlHBKB1301.SetInsertCIInfoSql(Cmd, Cn, dataHBKB1301) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報新規登録", Nothing, Cmd)

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
    ''' 【編集／履歴モード】CI共通情報更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でCI共通情報テーブルを更新（UPDATE）する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateCIInfo(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CI共通情報更新（UPDATE）用SQLを作成
            If sqlHBKB1301.SetUpdateCIInfoSql(Cmd, Cn, dataHBKB1301) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報更新", Nothing, Cmd)

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
    ''' 【編集／履歴モード】サーバー日付取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>更新用のサーバー日付を取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectSysDate(ByRef Adapter As NpgsqlDataAdapter, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSysDate As New DataTable         '履歴番号格納用テーブル

        Try

            'SQLを作成
            If sqlHBKB1301.SetSelectSysDateSql(Adapter, Cn, dataHBKB1301) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サーバー日付取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtSysDate)

            'データが取得できた場合、データクラスに履歴番号をセット
            If dtSysDate.Rows.Count > 0 Then
                dataHBKB1301.PropDtmSysDate = dtSysDate.Rows(0).Item("SysDate")
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
    ''' 【新規登録モード】CI部所有機器新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をCI部所有機器テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCIBuy(ByVal Cn As NpgsqlConnection, _
                                ByVal dataHBKB1301 As DataHBKB1301) As Boolean



        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CI部所有機器新規登録（INSERT）用SQLを作成
            If sqlHBKB1301.SetInsertCIBuySql(Cmd, Cn, dataHBKB1301) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI部所有機器新規登録", Nothing, Cmd)

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
    ''' 【編集／履歴モード】CI部所有機器更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でCI部所有機器テーブルを更新（UPDATE）する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateCIBuy(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CI部所有機器更新（UPDATE）用SQLを作成
            If sqlHBKB1301.SetUpdateCIBuySql(Cmd, Cn, dataHBKB1301) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI部所有機器更新", Nothing, Cmd)

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
    ''' 登録時入力チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録項目の入力チェックを行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckInputValueMain(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '入力チェック処理
        If CheckInputValue(dataHBKB1301) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】コンボボックス作成処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のコンボボックスを作成する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateCmb(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301

                '種別コンボボックス作成
                If commonLogic.SetCmbBox(.PropDtKindMasta, .PropCmbKind, False, "", "") = False Then
                    Return False
                End If

                'CIステータスコンボボックス作成
                If commonLogic.SetCmbBox(.PropDtCIStatusMasta, .PropCmbCIStatus, True, "", "") = False Then
                    Return False
                End If

                'OSコンボボックス作成
                If commonLogic.SetCmbBox(.PropDtOSCD, .PropcmbOSCD, True, 0) = False Then
                    Return False
                End If

                'ウィルス対策ソフトコンボボックス作成
                If commonLogic.SetCmbBox(.PropDtAntiVirusSoftCD, .PropcmbAntiVirusSoftCD, True, 0) = False Then
                    Return False
                End If

                'DNS登録コンボボックス作成
                If commonLogic.SetCmbBox(.PropDtDNSRegCD, .PropcmbDNSRegCD, True, "", "") = False Then
                    Return False
                End If

                'zoo参加有無コンボボックス作成
                If commonLogic.SetCmbBox(ZOO_KBN, .PropcmbZooKbn) = False Then
                    Return False
                End If

                '番号通知コンボボックス作成
                If commonLogic.SetCmbBox(NUMINFO_KBN, .PropcmbNumInfoKbn) = False Then
                    Return False
                End If

                'シール送付コンボボックス作成
                If commonLogic.SetCmbBox(SEALSEND_KBN, .PropcmbSealSendkbn) = False Then
                    Return False
                End If

                'ウィルス対策ソフト確認コンボボックス作成
                If commonLogic.SetCmbBox(ANTIVIRUSSOFCHECK_KBN, .PropcmbAntiVirusSofCheckKbn) = False Then
                    Return False
                End If

                'IP割当種類コンボボックス作成
                If commonLogic.SetCmbBox(.PropDtIPUseCD, .PropcmbIPUseCD, True, 0) = False Then
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
    ''' 【共通】入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録項目の入力チェックを行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckInputValue(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim strMacAddCheck As String

        Try
            With dataHBKB1301

                '番号
                With .PropTxtNum
                    '未入力の場合、エラー
                    If .Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = B1301_E013
                        'タブを基本情報タブに設定
                        dataHBKB1301.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                    '半角数値でない場合、エラー
                    If commonValidation.IsHalfNmb(.Text) = False Then
                        'エラーメッセージ設定
                        puErrMsg = B1301_E014
                        'タブを基本情報タブに設定
                        dataHBKB1301.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                    '番号のDB重複チェック
                    If CheckNumPrimary(dataHBKB1301, .Text) = False Then
                        Return False
                    End If
                End With

                '分類１
                With .PropTxtClass1
                    '未入力の場合、エラー
                    If .Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = B1301_E002
                        'タブを基本情報タブに設定
                        dataHBKB1301.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With

                '分類２
                With .PropTxtClass2
                    '未入力の場合、エラー
                    If .Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = B1301_E003
                        'タブを基本情報タブに設定
                        dataHBKB1301.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With

                '名称
                With .PropTxtCINM
                    '未入力の場合、エラー
                    If .Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = B1301_E004
                        'タブを基本情報タブに設定
                        dataHBKB1301.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With

                '型番
                With .ProptxtKataban
                    '未入力の場合、エラー
                    If .Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = B1301_E009
                        'タブを基本情報タブに設定
                        dataHBKB1301.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With

                'ステータス
                With .PropCmbCIStatus
                    '未入力の場合、エラー
                    If .SelectedValue = "" Then
                        'エラーメッセージ設定
                        puErrMsg = B1301_E005
                        'タブを基本情報タブに設定
                        dataHBKB1301.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With

                'MacAddress1
                With .ProptxtMacaddress1
                    '入力のある場合、チェックを行う
                    If .Text <> "" Then
                        ':と-を削除し、変数に格納する
                        strMacAddCheck = .Text.Replace("-", "").Replace(":", "")
                        '12桁以外の場合、エラー
                        If Len(strMacAddCheck) <> 12 Or commonValidation.IsHalfChar(strMacAddCheck) = False Then
                            '半角英数以外の場合エラー
                            'エラーメッセージ設定
                            puErrMsg = B1301_E010
                            'タブを利用情報タブに設定
                            dataHBKB1301.PropTbInput.SelectedIndex = TAB_KHN
                            'フォーカス設定
                            .Focus()
                            .SelectAll()
                            'エラーを返す
                            Return False
                        End If
                    End If
                End With

                'MacAddress1
                With .ProptxtMacaddress2
                    '入力のある場合、チェックを行う
                    If .Text <> "" Then
                        ':と-を削除し、変数に格納する
                        strMacAddCheck = .Text.Replace("-", "").Replace(":", "")
                        '12桁以外の場合、エラー
                        If Len(strMacAddCheck) <> 12 Or commonValidation.IsHalfChar(strMacAddCheck) = False Then
                            'エラーメッセージ設定
                            puErrMsg = B1301_E011
                            'タブを利用情報タブに設定
                            dataHBKB1301.PropTbInput.SelectedIndex = TAB_KHN
                            'フォーカス設定
                            .Focus()
                            .SelectAll()
                            'エラーを返す
                            Return False
                        End If
                    End If
                End With

                '[Del] 2012/09/25 m.ibuki 形式チェック削除START
                ''usrMailAdd
                'With .ProptxtUsrMailAdd
                '    '入力のある場合、チェックを行う
                '    If .Text <> "" Then
                '        'メールアドレス形式ではない場合、エラー
                '        If commonLogicHBK.IsMailAddress(.Text) = False Then
                '            'エラーメッセージ設定
                '            puErrMsg = B1301_E012
                '            'タブを利用情報タブに設定
                '            dataHBKB1301.PropTbInput.SelectedIndex = TAB_RIYO
                '            'フォーカス設定
                '            .Focus()
                '            .SelectAll()
                '            'エラーを返す
                '            Return False
                '        End If
                '    End If
                'End With
                '[Del] 2012/09/25 m.ibuki 形式チェック削除END

                'CIオーナー
                If .PropTxtCIOwnerNM.Text.Trim <> "" And _
                    .PropLblCIOwnerCD.Text = "" Then

                    'オーナー名に入力があってコードが未入力の場合（サブ検索にて選択していない場合）、エラー
                    puErrMsg = B1301_E006
                    'タブを関係情報タブに設定
                    .PropTbInput.SelectedIndex = TAB_RELATION
                    'フォーカス設定
                    .PropBtnSearchGrp.Focus()
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
    ''' 【共通】履歴情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴／変更理由を各テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRireki(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter    'アダプタ
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            '新規履歴番号取得
            If GetNewRirekiNo(Adapter, Cn, dataHBKB1301) = False Then
                Return False
            End If

            'CI共通情報履歴テーブル登録
            If InsertCIInfoR(Cn, dataHBKB1301) = False Then
                Return False
            End If

            'CI部所有機器履歴テーブル登録
            If InsertCIBuyR(Cn, dataHBKB1301) = False Then
                Return False
            End If

            '登録理由履歴テーブル登録
            If InsertRegReasonR(Cn, dataHBKB1301) = False Then
                Return False
            End If

            '原因リンク履歴テーブル登録
            If InsertCauseLinkR(Cn, dataHBKB1301) = False Then
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
            Adapter.Dispose()
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】新規履歴番号取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapter</param>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番した履歴番号を取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetNewRirekiNo(ByVal Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtRirekiNo As New DataTable         '履歴番号格納用テーブル

        Try

            'SQLを作成
            If sqlHBKB1301.SetSelectNewRirekiNoSql(Adapter, Cn, dataHBKB1301) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規履歴番号取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtRirekiNo)

            'データが取得できた場合、データクラスに履歴番号をセット
            If dtRirekiNo.Rows.Count > 0 Then
                dataHBKB1301.PropIntRirekiNo = dtRirekiNo.Rows(0).Item("RirekiNo")
            Else
                '取得できなかった場合はエラー
                puErrMsg = B1301_E008
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
            dtRirekiNo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】CI共通情報履歴テーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCIInfoR(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB1301.SetInsertCIInfoRSql(Cmd, Cn, dataHBKB1301) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報履歴新規登録", Nothing, Cmd)

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
    ''' 【共通】CI部所有機器履歴テーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI部所有機器履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCIBuyR(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB1301.SetInsertCIBuyRSql(Cmd, Cn, dataHBKB1301) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI部所有機器履歴新規登録", Nothing, Cmd)

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
    ''' 【共通】登録理由履歴テーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録理由履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRegReasonR(ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB1301.SetInsertRegReasonRSql(Cmd, Cn, dataHBKB1301) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "登録理由履歴新規登録", Nothing, Cmd)

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
    ''' 【共通】原因リンク履歴テーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>原因リンク履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCauseLinkR(ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'データ数分繰り返し、登録を行う
            For i As Integer = 0 To dataHBKB1301.PropDtCauseLink.Rows.Count - 1

                '登録行をデータクラスにセット
                dataHBKB1301.PropRowReg = dataHBKB1301.PropDtCauseLink.Rows(i)

                'SQLを作成
                If sqlHBKB1301.SetInsertCauseLinkRSql(Cmd, Cn, dataHBKB1301) = False Then
                    Return False
                End If

                'ログ出力
                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "原因リンク履歴新規登録", Nothing, Cmd)

                'SQL実行
                Cmd.ExecuteNonQuery()

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
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】エラー時コントロール非活性処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録系ボタンを非活性にする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetUnabledWhenError(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301

                '登録系ボタンコントロールを非活性にする
                .PropBtnReg.Enabled = False                         '登録ボタン
                .PropBtnRollBack.Enabled = False                    'ロールバックボタン
                .PropGrpLoginUser.PropBtnUnlockEnabled = False      'ロック解除ボタン

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
    ''' ロールバック時データ登録メイン処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をデータベースに反映する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RollBackDataMain(ByVal dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロールバック処理を行う　※編集モード時の更新処理と同じ
        If UpdateData(dataHBKB1301) = False Then
            Return False
        End If

        'ロック解除処理
        If UnlockData(dataHBKB1301) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' ユーザIDにてENTERキー押下時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたユーザーIDをキーにエンドユーザーマスタを検索し、取得データを利用者情報にセットする
    ''' <para>作成情報：2012/07/17 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function EnterClickOnUsrIDMain(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'エンドユーザーマスタ検索
        If GetEndUsrMasta(DataHBKB1301) = False Then
            Return False
        End If

        '取得データを利用者情報にセット
        If SetNewUsrData(dataHBKB1301) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' ユーザーIDテキストボックスEnter時の処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ＩＤテキストボックスにエンドユーザーマスタから取得した値を入力する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetNewUsrData(ByVal dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        With dataHBKB1301

            '選択データがある場合のみ値をセットする
            If .PropDtResultSub IsNot Nothing AndAlso .PropDtResultSub.Rows.Count = 1 Then

                '選択されたエンドユーザー情報を利用者情報にセットする
                '※連絡先、所属局、番組／部屋はクリア
                .ProptxtUsrID.Text = .PropDtResultSub.Rows(0).Item("EndUsrID")                'ユーザーID
                .ProptxtUsrNM.Text = .PropDtResultSub.Rows(0).Item("EndUsrNM")                'ユーザー氏名
                .ProptxtUsrMailAdd.Text = .PropDtResultSub.Rows(0).Item("EndUsrMailAdd")      'ユーザーメールアドレス
                '.ProptxtUsrTel.Text = .PropDtResultSub.Rows(0).Item("EndUsrContact")          'ユーザー電話番号
                .ProptxtUsrTel.Text = .PropDtResultSub.Rows(0).Item("EndUsrTel")          'ユーザー電話番号
                .ProptxtUsrKyokuNM.Text = ""                                                  'ユーザー所属局
                .ProptxtUsrBusyoNM.Text = .PropDtResultSub.Rows(0).Item("EndUsrBusyoNM")      'ユーザー所属部署
                .ProptxtUsrCompany.Text = .PropDtResultSub.Rows(0).Item("EndUsrCompany")      'ユーザー会社
                .ProptxtUsrContact.Text = ""                                                  'ユーザー連絡先
                .ProptxtUsrRoom.Text = ""                                                     'ユーザー番組／部屋

            Else

                '取得データがない場合（ENTERキーにて検索した場合）クリア
                .ProptxtUsrID.Text = ""                                                         'ユーザーID
                .ProptxtUsrNM.Text = ""                                                         'ユーザー氏名
                .ProptxtUsrMailAdd.Text = ""                                                    'ユーザーメールアドレス
                .ProptxtUsrTel.Text = ""                                                        'ユーザー電話番号
                .ProptxtUsrKyokuNM.Text = ""                                                    'ユーザー所属局
                .ProptxtUsrBusyoNM.Text = ""                                                    'ユーザー所属部署
                .ProptxtUsrCompany.Text = ""                                                    'ユーザー所属会社
                .ProptxtUsrContact.Text = ""                                                    'ユーザー連絡先
                .ProptxtUsrRoom.Text = ""                                                       'ユーザー番組／部屋

            End If
        End With

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】エンドユーザーマスタ取得処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>エンドユーザーマスタテーブルからエンドユーザーＩＤと氏名を取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetEndUsrMasta(ByVal dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        '変数宣言
        Dim dtEndUser As New DataTable

        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try
            'コネクションを開く
            Cn.Open()

            '取得用SQLの作成・設定
            If commonLogicHBK.GetEndUsrMastaData(Adapter, Cn, dataHBKB1301.ProptxtUsrID.Text, dataHBKB1301.PropDtResultSub) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "エンドユーザーマスタデータ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtEndUser)

            '取得データをデータクラスにセット
            dataHBKB1301.PropDtEndUsrMasta = dtEndUser

            '終了ログ出力
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
            Cn.Dispose()
            Adapter.Dispose()
            dtEndUser.Dispose()
        End Try


    End Function

    ''' <summary>
    ''' [検索]ボタン押下時利用者情報セットメイン処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サブ検索画面で選択されたエンドユーザーデータを当画面にセットする
    ''' <para>作成情報：2012/07/17 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetNewUsrDataMain(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'サブ検索画面で選択されたユーザー情報を利用者情報にセットする
        If SetNewUsrData(DataHBKB1301) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' [検索]ボタン押下時設置情報セットメイン処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サブ検索画面で選択された設置機器データを当画面にセットする
    ''' <para>作成情報：2012/07/17 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetNewSetDataMain(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'サブ検索画面で選択された設置機器データを設置情報にセットする
        If SetNewSetData(DataHBKB1301) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' [検索]ボタン押下時CIオーナー情報セットメイン処理
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サブ検索画面で選択されたCIオーナー情報データを当画面にセットする
    ''' <para>作成情報：2012/07/17 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetNewCIOwnerDataMain(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'サブ検索画面で選択されたCIオーナー情報データを設置情報にセットする
        If SetNewCIOwnerData(dataHBKB1301) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】設置情報設定
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サブ検索にて選択された設置機器データを設置情報にセットする
    ''' <para>作成情報：2012/07/17 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetNewSetData(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With DataHBKB1301

                '選択データがある場合のみ値をセットする
                If .PropDtResultSub IsNot Nothing Then

                    '選択された設置機器情報を設置情報にセットする
                    '※設置デスクNo、設置LANケーブル長さ、設置LANケーブル番号、情報コンセント・SWはクリア
                    .PropTxtSetKyokuNM.Text = .PropDtResultSub.Rows(0).Item("SetKyokuNM")       '設置局
                    .PropTxtSetBusyoNM.Text = .PropDtResultSub.Rows(0).Item("SetBusyoNM")       '設置部署
                    .PropTxtSetRoom.Text = .PropDtResultSub.Rows(0).Item("SetRoom")             '設置番組／部屋
                    .PropTxtSetBuil.Text = .PropDtResultSub.Rows(0).Item("SetBuil")             '設置建物
                    .PropTxtSetFloor.Text = .PropDtResultSub.Rows(0).Item("SetFloor")           '設置フロア

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
    ''' 【共通】CIオーナー設定
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サブ検索にて選択された設置機器データを設置情報にセットする
    ''' <para>作成情報：2012/07/17 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetNewCIOwnerData(ByRef dataHBKB1301 As DataHBKB1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB1301

                '選択データがある場合のみ値をセットする
                If .PropDtResultSub IsNot Nothing Then

                    'グループ名とグループCDを設定する
                    .PropTxtCIOwnerNM.Text = .PropDtResultSub.Rows(0).Item("グループ名")       'グループ名
                    .PropLblCIOwnerCD.Text = .PropDtResultSub.Rows(0).Item("グループCD")       'グループCD

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
    ''' 番号重複チェック
    ''' </summary>
    ''' <param name="dataHBKB1301">[IN/OUT]部所有機器登録画面Dataクラス</param>
    ''' <param name="strNum">[IN]番号</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力された番号をCI共通情報テーブルからデータを検索し存在するかチェックする
    ''' <para>作成情報：2012/08/02 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckNumPrimary(ByRef dataHBKB1301 As DataHBKB1301, ByRef strNum As String) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'DB接続用
        Dim Cn As New NpgsqlConnection(DbString)            'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter                'アダプタ
        Dim dtResult As New DataTable
        Try
            'コネクションを開く
            Cn.Open()

            '番号のデータ有無取得（SELECT）用SQLを作成
            If sqlHBKB1301.SetSelectNumSql(Adapter, Cn, dataHBKB1301, strNum) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "番号のデータ有無取得", Nothing, Adapter.SelectCommand)
            'SQL実行
            Adapter.Fill(dtResult)


            'すでにデータが存在する場合、エラー
            If dtResult.Rows(0).Item(0) > 0 Then
                puErrMsg = B1301_E015
                Return False
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            'オブジェクト解放
            Adapter.Dispose()
            Cn.Dispose()
            dtResult.Dispose()
        End Try
    End Function

End Class
