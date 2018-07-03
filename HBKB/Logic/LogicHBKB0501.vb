Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.IO
Imports System.Windows.Forms

''' <summary>
''' 文書登録画面ロジッククラス
''' </summary>
''' <remarks>文書登録画面のロジックを定義したクラス
''' <para>作成情報：2012/07/11 s.tsuruta
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKB0501

    'インスタンス作成
    Private sqlHBKB0501 As New SqlHBKB0501
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

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
    Private Const TAB_FREE As Integer = 1                   'フリー入力情報
    Private Const TAB_RELATION As Integer = 2               '関係情報


    ''' <summary>
    ''' 【共通】システムエラー事前対応処理
    ''' </summary>
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システムエラー発生時に非活性にするコントロールリストを作成する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DoProcForErrorMain(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'トランザクション系コントロールリスト作成
        If CreateTsxCtlList(DataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて画面の初期表示設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormNewModeMain(ByRef dataHBKB0501 As DataHBKB0501) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'スプレッド表示用データテーブル作成
        If CreateDataTableForVw(dataHBKB0501) = False Then
            Return False
        End If

        'フォームコントロール設定
        If InitFormControl(dataHBKB0501) = False Then
            Return False
        End If

        '初期表示用データ取得
        If GetInitData(dataHBKB0501) = False Then
            Return False
        End If

        '初期表示用データセット
        If SetInitDataToControl(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>データのロック状況をチェックし、状況に応じてロックまたは処理モードの切替を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function LockMain(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロック設定
        If SetLockWhenLoad(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで画面の初期表示設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormEditModeMain(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'スプレッド表示用データテーブル作成
        If CreateDataTableForVw(dataHBKB0501) = False Then
            Return False
        End If

        'フォームコントロール設定
        If InitFormControl(dataHBKB0501) = False Then
            Return False
        End If

        '初期表示用データ取得
        If GetInitData(dataHBKB0501) = False Then
            Return False
        End If

        '初期表示用データセット
        If SetInitDataToControl(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで画面の初期表示設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormRefModeMain(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'スプレッド表示用データテーブル作成
        If CreateDataTableForVw(dataHBKB0501) = False Then
            Return False
        End If

        'フォームコントロール設定
        If InitFormControl(dataHBKB0501) = False Then
            Return False
        End If

        '初期表示用データ取得
        If GetInitData(dataHBKB0501) = False Then
            Return False
        End If

        '初期表示用データセット
        If SetInitDataToControl(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードで画面の初期表示設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormRirekiModeMain(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'スプレッド表示用データテーブル作成
        If CreateDataTableForVw(dataHBKB0501) = False Then
            Return False
        End If

        'フォームコントロール設定
        If InitFormControl(dataHBKB0501) = False Then
            Return False
        End If

        '初期表示用データ取得
        If GetInitData(dataHBKB0501) = False Then
            Return False
        End If

        '初期表示用データセット
        If SetInitDataToControl(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータをログインユーザでロックし、フォームのロックを解除する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function UnlockWhenClickBtnUnlockMain(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロックフラグOFF
        dataHBKB0501.PropBlnBeLockedFlg = False

        'ロック処理
        If SetLockWhenUnlock(dataHBKB0501) = False Then
            Return False
        End If

        'フォームコントロールのロックを解除する
        If SetFormControlPerProcMode(dataHBKB0501) = False Then
            Return False
        End If
        If SetDataToLoginAndLock(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>他のユーザによりロックが解除されていないかチェックする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckBeUnlockedMain(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロック解除チェック
        If CheckUnlock(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面の内容をログに出力し、フォームコントロールを再設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetFormRefModeFromEditModeMain(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '入力内容ログ出力
        If OutputUnlockLog(dataHBKB0501) = False Then
            Return False
        End If

        '参照モードでフォームコントロール設定
        If SetFormControlPerProcMode(dataHBKB0501) = False Then
            Return False
        End If

        '参照モードでロック情報設定
        If SetDataToLoginAndLockForRef(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータのロックを解除する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function UnlockWhenCloseMain(ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロック解除処理
        If UnlockData(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>トランザクション系コントロールのリストを作成する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateTsxCtlList(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim aryCtlList As New ArrayList

        Try
            With dataHBKB0501

                'トランザクション系のコントロールをリストに追加
                aryCtlList.Add(.PropBtnReg)              '登録ボタン
                aryCtlList.Add(.PropBtnRollBack)         '解除ボタン
                aryCtlList.Add(.PropGrpLoginUser)        'ログイン／ロックグループ

                '[mod] y.ikushima ファイルダウンロードエラー対応 START
                aryCtlList.Add(.PropBtnFilePathOpen)         '開く
                aryCtlList.Add(.PropBtnFilePathDownload)        'ダウンロード
                '[mod] y.ikushima ファイルダウンロードエラー対応 END

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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録系ボタンを非活性にする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetFormWhenErrorMain(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '登録系ボタンを非活性にする
        If SetUnabledWhenError(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドのデータソースとして設定するデータテーブルを作成する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateDataTableForVw(ByRef dataHBKB0501 As DataHBKB0501) As Boolean


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
            With dataHBKB0501
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            'マスタデータ取得
            If GetMastaData(Adapter, Cn, dataHBKB0501) = False Then
                Return False
            End If

            'メイン表示データ取得
            If GetMainData(Adapter, Cn, dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLockWhenUnlock(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'CI共通情報テーブルロック解除
            If commonLogicHBK.UnlockCIInfo(dataHBKB0501.PropIntCINmb) = False Then
                Return False
            End If

            'CI共通情報テーブルロック
            If commonLogicHBK.LockCIInfo(dataHBKB0501.PropIntCINmb, dataHBKB0501.PropDtCILock) = False Then
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
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をデータベースに反映する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RegistDataOnNewModeMain(ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '新規登録処理
        If InsertNewData(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をデータベースに反映する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RegistDataOnEditModeMain(ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '更新処理
        If UpdateData(dataHBKB0501) = False Then
            Return False
        End If

        'ロック解除処理
        If UnlockData(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLockWhenLoad(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnBeLocked As Boolean = False                   'ロックフラグ  

        Try

            With dataHBKB0501

                'ロック解除チェック
                If commonLogicHBK.CheckDataBeLocked(.PropIntCINmb, blnBeLocked, .PropStrBeLockedMsg, .PropDtCILock) = False Then
                    Return False
                End If

                'ロックされている（別のユーザが編集中）場合、 ロックフラグをON
                If blnBeLocked = True Then

                    .PropBlnBeLockedFlg = True

                Else

                    'ロックされていない場合、CI共通情報をロックする
                    If SetLock(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集開始日時によりロック設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckAndSetLock(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnBeLocked As Boolean = False                   'ロックフラグ  

        Try

            With dataHBKB0501

                'ロック解除チェック
                If commonLogicHBK.CheckDataBeLocked(.PropIntCINmb, blnBeLocked, .PropStrBeLockedMsg, .PropDtCILock) = False Then
                    Return False
                End If

                'ロックされている（別のユーザが編集中）場合、 ロックフラグをON
                If blnBeLocked = True Then

                    .PropBlnBeLockedFlg = True

                Else

                    'ロックされていない場合、CI共通情報をロックする
                    If SetLock(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報テーブルをロックする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLock(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnDoUnlock As Boolean = False                   'ロック解除実行フラグ

        Try

            With dataHBKB0501

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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>マスタデータを取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMastaData(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'CI種別マスタ取得
            If commonLogicHBK.GetCIKindMastaData(Adapter, Cn, CI_TYPE_DOC, dataHBKB0501.PropDtCIKindMasta) = False Then
                Return False
            End If

            '種別マスタ取得
            '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 START
            'If commonLogicHBK.GetKindMastaData(Adapter, Cn, CI_TYPE_DOC, dataHBKB0501.PropDtKindMasta) = False Then
            '    Return False
            'End If
            If commonLogicHBK.GetKindMastaData(Adapter, Cn, CI_TYPE_DOC, dataHBKB0501.PropDtKindMasta, dataHBKB0501.PropIntCINmb) = False Then
                Return False
            End If
            '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 START

            'CIステータスマスタ取得
            If commonLogicHBK.GetCIStatusMastaData(Adapter, Cn, CI_TYPE_DOC, dataHBKB0501.PropDtCIStatusMasta) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainData(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '取得しない


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用データ取得
                        If GetMainDataForEdit(Adapter, Cn, dataHBKB0501) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用データ取得　※編集モードと同じ
                        If GetMainDataForEdit(Adapter, Cn, dataHBKB0501) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用データ取得
                    If GetMainDataForRireki(Adapter, Cn, dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集／参照モードで初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainDataForEdit(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        'Dim dtCIdoc As DataTable = Nothing

        Try
            'CI共通情報データ取得
            If GetCIInfo(Adapter, Cn, dataHBKB0501) = False Then
                Return False
            End If

            'CI部所有機器データ取得
            If GetCIDoc(Adapter, Cn, dataHBKB0501) = False Then
                Return False
            End If

            '登録理由履歴データ取得
            If GetRegReason(Adapter, Cn, dataHBKB0501) = False Then
                Return False
            End If

            '原因リンク履歴データ取得
            If GetCauseLink(Adapter, Cn, dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報データを取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetCIInfo(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtCIInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKB0501.SetSelectCIInfoSql(Adapter, Cn, dataHBKB0501) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtCIInfo)

            'データが取得できなかった場合、エラー
            If dtCIInfo.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & B0501_E001, TBNM_CI_INFO_TB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKB0501.PropDtCIInfo = dtCIInfo


            '終了ログ出力
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI文書データを取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetCIDoc(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtCIDoc As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKB0501.SetSelectCIDocSql(Adapter, Cn, dataHBKB0501) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI文書データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtCIDoc)

            'データが取得できなかった場合、エラー
            If dtCIDoc.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & B0501_E001, TBNM_CI_DOC_TB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKB0501.PropDtCIDoc = dtCIDoc


            '終了ログ出力
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
            dtCIDoc.Dispose()
        End Try

    End Function

    ''' <summary>
    '''【編集／参照モード】原因リンク履歴データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>原因リンク履歴データを取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetCauseLink(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '取得用SQLの作成・設定
            If sqlHBKB0501.SetSelectCauseLinkSql(Adapter, Cn, dataHBKB0501) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "原因リンク履歴取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0501.PropDtMyCauseLink)

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録理由履歴データを取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetRegReason(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '取得用SQLの作成・設定
            If sqlHBKB0501.SetSelectRegReasonSql(Adapter, Cn, dataHBKB0501) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "登録理由履歴データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0501.PropDtRireki)

            '最大履歴番号を取得
            If dataHBKB0501.PropDtRireki.Rows.Count > 0 Then
                dataHBKB0501.PropIntRirekiNo = dataHBKB0501.PropDtRireki.Rows(0).Item("RirekiNo")
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードで初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainDataForRireki(ByVal Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'CI共通情報履歴データ取得
            If GetCIInfoR(Adapter, Cn, dataHBKB0501) = False Then
                Return False
            End If

            'CI文書履歴データ取得
            If GetCIDocR(Adapter, Cn, dataHBKB0501) = False Then
                Return False
            End If

            '登録理由履歴データ取得（履歴モード）
            If GetRegReasonR(Adapter, Cn, dataHBKB0501) = False Then
                Return False
            End If

            '原因リンク履歴データ取得（履歴モード）
            If GetCauseLinkR(Adapter, Cn, dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報データを取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetCIInfoR(ByVal Adapter As NpgsqlDataAdapter, _
                                ByVal Cn As NpgsqlConnection, _
                                ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtCIInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKB0501.SetSelectCIInfoRSql(Adapter, Cn, dataHBKB0501) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報履歴データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtCIInfo)

            'データが取得できなかった場合、エラー
            If dtCIInfo.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & B0501_E001, TBNM_CI_INFO_RTB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKB0501.PropDtCIInfo = dtCIInfo

            '終了ログ出力
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
    ''' 【履歴モード】CI文書履歴データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI文書履歴データを取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetCIDocR(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtCIDocR As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKB0501.SetSelectCIDocRSql(Adapter, Cn, dataHBKB0501) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI文書履歴データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtCIDocR)

            'データが取得できなかった場合、エラー
            If dtCIDocR.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & B0501_E001, TBNM_CI_DOC_RTB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKB0501.PropDtCIDoc = dtCIDocR


            '終了ログ出力
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
            dtCIDocR.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【履歴モード】原因リンク履歴データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>原因リンク履歴データを取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetCauseLinkR(ByVal Adapter As NpgsqlDataAdapter, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '取得用SQLの作成・設定
            If sqlHBKB0501.SetSelectCauseLinkRSql(Adapter, Cn, dataHBKB0501) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "原因リンク履歴取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0501.PropDtMyCauseLink)


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録理由履歴データを取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetRegReasonR(ByVal Adapter As NpgsqlDataAdapter, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '取得用SQLの作成・設定
            If sqlHBKB0501.SetSelectRegReasonRSql(Adapter, Cn, dataHBKB0501) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "登録理由履歴データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0501.PropDtRireki)


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InitFormControl(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'スプレッド設定
            If SetVwControl(dataHBKB0501) = False Then
                Return False
            End If

            '隠しラベル非表示設定
            If SetHiddenLabelUnvisible(dataHBKB0501) = False Then
                Return False
            End If

            '処理モードに応じたフォームコントロール設定
            If SetFormControlPerProcMode(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフォームコントロールの設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormControlPerProcMode(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'ログイン／ロックコントロール設定
            If SetLoginAndLockControl(dataHBKB0501) = False Then
                Return False
            End If

            'ヘッダ設定
            If SetHeaderControl(dataHBKB0501) = False Then
                Return False
            End If

            'フッタ設定
            If SetFooterControl(dataHBKB0501) = False Then
                Return False
            End If

            'タブページ設定
            If SetTabControl(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームコントロールに初期表示データを設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitDataToControl(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'ログイン／ロックデータ設定
            If SetDataToLoginAndLock(dataHBKB0501) = False Then
                Return False
            End If

            'ヘッダデータ設定
            If SetDataToHeader(dataHBKB0501) = False Then
                Return False
            End If

            'フッタデータ設定
            If SetDataToFooter(dataHBKB0501) = False Then
                Return False
            End If

            'タブコントロールデータ設定
            If SetDataToTabControl(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLock(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0501

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToLoginAndLockForNew(dataHBKB0501) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetDataToLoginAndLockForEdit(dataHBKB0501) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetDataToLoginAndLockForRef(dataHBKB0501) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetDataToLoginAndLockForRireki(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForNew(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0501.PropGrpLoginUser

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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForEdit(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0501.PropGrpLoginUser

                'ロック情報が取得できた場合
                If dataHBKB0501.PropDtCILock.Rows.Count > 0 Then
                    'ロック開始日時
                    Dim dtmLockTime As DateTime = Nothing  '編集開始日時
                    If dataHBKB0501.PropDtCILock.Rows(0).Item("EdiTime").ToString() <> "" Then
                        dtmLockTime = dataHBKB0501.PropDtCILock.Rows(0).Item("EdiTime")
                        dataHBKB0501.propStrEdiTime = dataHBKB0501.PropDtCILock.Rows(0).Item("EdiTime")
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForRef(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0501.PropGrpLoginUser

                'ロック情報が取得できた場合
                If dataHBKB0501.PropDtCILock.Rows.Count > 0 Then
                    'ロック開始日時
                    Dim dtmLockTime As DateTime = Nothing
                    If dataHBKB0501.PropDtCILock.Rows(0).Item("EdiTime").ToString() <> "" Then
                        dtmLockTime = dataHBKB0501.PropDtCILock.Rows(0).Item("EdiTime")
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForRireki(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0501.PropGrpLoginUser

                'ロック開始日時 ※空文字のときはNothingを指定しないと落ちる
                Dim strLockTime As String = dataHBKB0501.propStrEdiTime
                If dataHBKB0501.PropDtCILock IsNot Nothing AndAlso dataHBKB0501.PropDtCILock.Rows.Count > 0 Then
                    .PropLockDate = dataHBKB0501.PropDtCILock.Rows(0).Item("EdiTime")
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ヘッダデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToHeader(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0501

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToHeaderForNew(dataHBKB0501) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetDataToHeaderForEdit(dataHBKB0501) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetDataToHeaderForRef(dataHBKB0501) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetDataToHeaderForRireki(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでヘッダデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToHeaderForNew(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0501

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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでヘッダデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToHeaderForEdit(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0501



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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでヘッダデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToHeaderForRef(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '編集モードヘッダデータ設定処理と同じ
            If SetDataToHeaderForEdit(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードでヘッダデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToHeaderForRireki(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try


            dataHBKB0501.PropLblValueRirekiNo.Text = dataHBKB0501.PropDtCIInfo.Rows(0).Item("rirekino")

            '編集モードヘッダデータ設定処理と同じ
            If SetDataToHeaderForEdit(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フッタデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToFooter(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0501

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード
                    If SetDataToFooterForNew(dataHBKB0501) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetDataToFooterForEdit(dataHBKB0501) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetDataToFooterForRef(dataHBKB0501) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetDataToFooterForRireki(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでフッタデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToFooterForNew(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0501

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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでフッタデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToFooterForEdit(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0501

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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでフッタデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToFooterForRef(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '※編集モードフッタデータ設定処理
            If SetDataToFooterForEdit(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードでフッタデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToFooterForRireki(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '※編集モードフッタデータ設定処理
            If SetDataToFooterForEdit(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControl(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetLoginAndLockControlForNew(dataHBKB0501) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetLoginAndLockControlForEdit(dataHBKB0501) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照（ロック）モード用設定
                        If SetLoginAndLockControlForRef(dataHBKB0501) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetLoginAndLockControlForRireki(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForNew(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501.PropGrpLoginUser

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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForEdit(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501.PropGrpLoginUser

                'グループコンボボックス非活性
                .cmbGroup.Enabled = False

                '変更ボタン非活性
                .btnChange.Enabled = False

                'ロック情報表示
                .PropLockInfoVisible = True

                '解除ボタン表示
                .PropBtnUnlockVisible = True
                'ロック情報が取得できた場合
                If dataHBKB0501.PropDtCILock.Rows.Count > 0 Then

                    '編集者と同じグループの場合は解除ボタン活性化
                    If .cmbGroup.SelectedValue = dataHBKB0501.PropDtCILock.Rows(0).Item("EdiGrpCD") And _
                       PropUserId <> dataHBKB0501.PropDtCILock.Rows(0).Item("EdiID") Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForRef(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501.PropGrpLoginUser

                'グループコンボボックス非活性
                .cmbGroup.Enabled = False

                '変更ボタン非活性
                .btnChange.Enabled = False

                'ロック情報表示
                .PropLockInfoVisible = True

                '解除ボタン表示
                .PropBtnUnlockVisible = True

                'ロック情報が取得できた場合
                If dataHBKB0501.PropDtCILock.Rows.Count > 0 Then
                    '編集者と同じグループの場合は解除ボタン活性化
                    If .cmbGroup.SelectedValue = dataHBKB0501.PropDtCILock.Rows(0).Item("EdiGrpCD") Then
                        .PropBtnUnlockEnabled = True
                    Else
                        .PropBtnUnlockEnabled = False
                    End If
                Else
                    '解除ボタン非活性
                    .PropBtnUnlockEnabled = False

                End If

                'ロック解除から遷移してきた場合は解除ボタンを非活性
                If dataHBKB0501.PropBlnLockCompare = True Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForRireki(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501.PropGrpLoginUser

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
    ''' 【共通】ヘッダコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてヘッダコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/03 s.turuta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetHeaderControl(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501


                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetHeaderControlForNew(dataHBKB0501) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetHeaderControlForEdit(dataHBKB0501) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetHeaderControlForRef(dataHBKB0501) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetHeaderControlForRireki(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでヘッダコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetHeaderControlForNew(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501

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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでヘッダコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetHeaderControlForEdit(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501

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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでヘッダコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetHeaderControlForRef(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501

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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでヘッダコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetHeaderControlForRireki(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501

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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControl(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetFooterControlForNew(dataHBKB0501) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetFooterControlForEdit(dataHBKB0501) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetFooterControlForRef(dataHBKB0501) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetFooterControlForRireki(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForNew(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501

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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForEdit(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501

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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForRef(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501

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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForRireki(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501

                '登録ボタン非表示
                .PropBtnReg.Visible = False

                '呼び出し元が文書登録画面で編集モードでない場合、ロールバックボタン非活性
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてタブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControl(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '基本情報タブ設定
            If SetTabControlKhn(dataHBKB0501) = False Then
                Return False
            End If

            'フリー入力情報タブ設定
            If SetTabControlFree(dataHBKB0501) = False Then
                Return False
            End If


            '関係情報タブ設定
            If SetTabControlRelation(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhn(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetTabControlKhnForNew(dataHBKB0501) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetTabControlKhnForEdit(dataHBKB0501) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照（ロック）モード用設定
                        If SetTabControlKhnForRef(dataHBKB0501) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetTabControlKhnForRireki(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードで応じて基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForNew(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501

                '種別コンボボックス
                .PropCmbKind.Enabled = False
                '開くボタン
                .PropBtnFilePathOpen.Enabled = False
                'ダウンロードボタン
                .PropBtnFilePathDownload.Enabled = False

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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForEdit(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501

                '種別コンボボックス
                .PropCmbKind.Enabled = False

                '参照ボタン
                .PropbtnSansyou.Enabled = True

                'クリアボタン
                .PropbtnClear.Enabled = True

                '作成者検索ボタン
                .PropbtnCrateSearch.Enabled = True

                '文書責任者検索ボタン
                .PropbtnChargeSearch.Enabled = True

                '更新者検索ボタン
                .PropbtnLastUpSearch.Enabled = True

                '現在時刻ボタン
                .PropBtnDateTime.Enabled = True

            End With

            'ファイルが登録されていない場合「開く」、「ダウンロード」ボタンを非活性にする


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForRef(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501

                '種別コンボボックス
                .PropCmbKind.Enabled = False
                '作成者検索ボタン
                .PropbtnCrateSearch.Enabled = False
                '最終更新者検索ボタン
                .PropbtnLastUpSearch.Enabled = False
                '文書責任者検索ボタン
                .PropbtnChargeSearch.Enabled = False
                '現在時刻ボタン
                .PropBtnDateTime.Enabled = False
                '参照ボタン
                .PropbtnSansyou.Enabled = False
                'クリアボタン
                .PropbtnClear.Enabled = False

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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForRireki(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501

                '種別コンボボックス
                .PropCmbKind.Enabled = False

                '番号(手動)テキストボックス
                .PropTxtNum.ReadOnly = True

                '版(手動)テキストボックス
                .PropTxtVersion.ReadOnly = True

                '分類１～２テキストボックス
                .PropTxtClass1.ReadOnly = True
                .PropTxtClass2.ReadOnly = True

                '名称テキストボックス
                .PropTxtCINM.ReadOnly = True

                'ステータスコンボボックス
                .PropCmbCIStatus.Enabled = False

                '説明テキストボックス
                .PropTxtCINaiyo.ReadOnly = True

                '作成者IDテキストボックス
                .ProptxtCrateID.ReadOnly = True

                '作成者氏名テキストボックス
                .ProptxtCrateNM.ReadOnly = True

                '作成者検索ボタン
                .PropbtnCrateSearch.Enabled = False

                '作成者年月日DateTimePickerEx
                .PropDtpCreateDT.Enabled = False

                '最終更新者IDテキストボックス
                .ProptxtLastUpID.ReadOnly = True

                '最終更新者氏名テキストボックス
                .ProptxtLastUpNM.ReadOnly = True

                .PropbtnLastUpSearch.Enabled = False

                '最終更新者年月日DateTimePickerEx
                .PropDtpLastUpDT.Enabled = False

                '現在時刻テキストボックス
                .PropTxtDateTime.PropTxtTime.ReadOnly = True

                '現在時刻ボタン
                .PropBtnDateTime.Enabled = False

                '文書責任者IDテキストボックス
                .ProptxtChargeID.ReadOnly = True

                '文書責任者氏名テキストボックス
                .ProptxtChargeNM.ReadOnly = True

                '文書責任者検索ボタン
                .PropbtnChargeSearch.Enabled = False

                '文書提供者
                .ProptxtOfferNM.ReadOnly = True

                '文書配布先
                .ProptxtShareteamNM.ReadOnly = True

                '文書格納パス
                .ProptxtFilePath.Text = ""

                '参照ボタン
                .PropbtnSansyou.Enabled = False

                'クリアボタン
                .PropbtnClear.Enabled = False

                '文書廃棄年月日DateTimePickerEx
                .PropDtpDelDT.Enabled = False

                '文書廃棄理由テキストボックス
                .ProptxtDelReason.ReadOnly = True



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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフリー入力情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlFree(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501


                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モードなし


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード

                    '編集／参照（ロック）モードなし


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetTabControlFreeForRireki(dataHBKB0501) = False Then
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
    ''' 【履歴モード】フリー入力情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフリー入力情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlFreeForRireki(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501

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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて関係情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRelation(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モードなし


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetTabControlRelationForEdit(dataHBKB0501) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照（ロック）モード用設定
                        If SetTabControlRelationForRef(dataHBKB0501) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    If SetTabControlRelationForRireki(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで関係情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRelationForEdit(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501

                '検索ボタン
                .PropBtnSearchGrp.Enabled = True

                'CIオーナーラベル非表示
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで関係情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRelationForRef(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501

                '検索ボタン
                .PropBtnSearchGrp.Enabled = False

                'CIオーナーラベル非表示
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードで関係情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRelationForRireki(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501

                'CIオーナー名テキストボックス
                .PropTxtCIOwnerNM.ReadOnly = True

                'CIオーナーラベル非表示
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>タブコントロールデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabControl(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '基本情報タブデータ設定
            If SetDataToTabKhn(dataHBKB0501) = False Then
                Return False
            End If

            'フリー入力タブデータ設定
            If SetDataToTabFree(dataHBKB0501) = False Then
                Return False
            End If

            '関係情報タブデータ設定
            If SetDataToTabRelation(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhn(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0501

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToTabKhnForNew(dataHBKB0501) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetDataToTabKhnForEdit(dataHBKB0501) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetDataToTabKhnForRef(dataHBKB0501) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetDataToTabKhnForRireki(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードで基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhnForNew(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'コンボボックス作成
            If CreateCmb(dataHBKB0501) = False Then
                Return False
            End If

            With dataHBKB0501

                '種別コンボボックス
                .PropCmbKind.Enabled = False

                '番号(手動)テキストボックス
                .PropTxtNum.Text = ""

                '版(手動)
                .PropTxtVersion.Text = ""

                '分類１～２テキストボックス
                .PropTxtClass1.Text = ""
                .PropTxtClass2.Text = ""

                '名称テキストボックス
                .PropTxtCINM.Text = ""

                'ステータスコンボボックス
                .PropCmbCIStatus.SelectedValue = ""

                '説明テキストボックス
                .PropTxtCINaiyo.Text = ""

                '作成者IDテキストボックス
                .ProptxtCrateID.Text = ""

                '作成者氏名テキストボックス
                .ProptxtCrateNM.Text = ""

                '作成者年月日DateTimePickerEx
                .PropDtpCreateDT.txtDate.Text = ""

                '最終更新者IDテキストボックス
                .ProptxtLastUpID.Text = ""

                '最終更新者氏名テキストボックス
                .ProptxtLastUpNM.Text = ""

                '最終更新者年月日DateTimePickerEx
                .PropDtpLastUpDT.txtDate.Text = ""

                '文書責任者IDテキストボックス
                .ProptxtChargeID.Text = ""

                '文書責任者氏名テキストボックス
                .ProptxtChargeNM.Text = ""

                '文書提供者テキストボックス
                .ProptxtOfferNM.Text = ""

                '文書配布先テキストボックス
                .ProptxtShareteamNM.Text = ""

                '文書格納パステキストボックス
                .ProptxtFilePath.Text = ""

                '文書廃棄年月日DateTimePickerEx
                .PropDtpDelDT.txtDate.Text = ""

                '文書廃棄理由テキストボックス
                .ProptxtDelReason.Text = ""



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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhnForEdit(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'コンボボックス作成
            If CreateCmb(dataHBKB0501) = False Then
                Return False
            End If

            With dataHBKB0501

                '種別コンボボックス
                .PropCmbKind.Enabled = False

                '番号(手動)テキストボックス
                .PropTxtNum.Text = .PropDtCIInfo.Rows(0).Item("Num")

                '版(手動)テキストボックス
                .PropTxtVersion.Text = .PropDtCIDoc.Rows(0).Item("Version")

                '分類１～２テキストボックス
                .PropTxtClass1.Text = .PropDtCIInfo.Rows(0).Item("Class1")
                .PropTxtClass2.Text = .PropDtCIInfo.Rows(0).Item("Class2")

                '名称テキストボックス
                .PropTxtCINM.Text = .PropDtCIInfo.Rows(0).Item("CINM")

                'ステータスコンボボックス
                .PropCmbCIStatus.SelectedValue = .PropDtCIInfo.Rows(0).Item("CIStatusCD")

                '説明テキストボックス
                .PropTxtCINaiyo.Text = .PropDtCIInfo.Rows(0).Item("CINaiyo")

                '作成者IDテキストボックス
                .ProptxtCrateID.Text = .PropDtCIDoc.Rows(0).Item("CrateID")

                '作成者氏名テキストボックス
                .ProptxtCrateNM.Text = .PropDtCIDoc.Rows(0).Item("CrateNM")

                '作成者年月日DateTimePickerEx
                .PropDtpCreateDT.txtDate.Text = .PropDtCIDoc.Rows(0).Item("CreateDT")

                '最終更新者IDテキストボックス
                .ProptxtLastUpID.Text = .PropDtCIDoc.Rows(0).Item("LastUpID")

                '最終更新者氏名テキストボックス
                .ProptxtLastUpNM.Text = .PropDtCIDoc.Rows(0).Item("LastUpNM")

                '最終更新日時が入っているかチェック
                If dataHBKB0501.PropDtCIDoc.Rows(0).Item("LastUpDT").ToString = "" Then
                    '[ADD] 2013/04/22 r.hoshino 課題No108対応 START
                    .PropDtpLastUpDT.txtDate.Text = ""
                    '[ADD] 2013/04/22 r.hoshino 課題No108対応 END
                Else

                    '最終更新者年月日DateTimePickerEx
                    .PropDtpLastUpDT.txtDate.Text = dataHBKB0501.PropDtCIDoc.Rows(0).Item("LastUpDT").ToString.Substring(0, 10)

                    '最終更新時刻
                    .PropTxtDateTime.PropTxtTime.Text = dataHBKB0501.PropDtCIDoc.Rows(0).Item("LastUpDT").ToString.Substring(11, 5)

                End If

                '文書責任者IDテキストボックス
                .ProptxtChargeID.Text = .PropDtCIDoc.Rows(0).Item("ChargeID")

                '文書責任者氏名テキストボックス
                .ProptxtChargeNM.Text = .PropDtCIDoc.Rows(0).Item("ChargeNM")

                '文書提供者
                .ProptxtOfferNM.Text = .PropDtCIDoc.Rows(0).Item("OfferNM")

                '文書配布先
                .ProptxtShareteamNM.Text = .PropDtCIDoc.Rows(0).Item("shareteamnm")

                '文書格納パス
                .ProptxtFilePath.Text = ""

                '文書廃棄年月日DateTimePickerEx
                If dataHBKB0501.PropDtCIDoc.Rows(0).Item("DelDT") <> "" Then

                    .PropDtpDelDT.txtDate.Text = .PropDtCIDoc.Rows(0).Item("DelDT")
                Else
                    .PropDtpDelDT.txtDate.Text = ""
                End If

                '文書廃棄理由テキストボックス
                .ProptxtDelReason.Text = .PropDtCIDoc.Rows(0).Item("DelReason")

                '文書管理番号が空の場合ボタンを非活性化させる。
                If dataHBKB0501.PropDtCIDoc.Rows(0).Item("FileMngNmb").ToString = "" Then
                    .PropBtnFilePathOpen.Enabled = False
                    .PropBtnFilePathDownload.Enabled = False
                Else
                    .PropBtnFilePathOpen.Enabled = True
                    .PropBtnFilePathDownload.Enabled = True
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhnForRef(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '編集モード基本情報タブデータ設定処理と同じ
            If SetDataToTabKhnForEdit(dataHBKB0501) = False Then
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
    ''' 【履歴モード】履歴情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードで基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhnForRireki(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '編集モード基本情報タブデータ設定処理と同じ
            If SetDataToTabKhnForEdit(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFree(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0501

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToTabFreeForNew(dataHBKB0501) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetDataToTabFreeForEdit(dataHBKB0501) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetDataToTabFreeForRef(dataHBKB0501) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetDataToTabFreeForRireki(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFreeForNew(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0501

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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFreeForEdit(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0501

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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFreeForRef(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '編集モードフリー入力タブデータ設定処理と同じ
            If SetDataToTabFreeForEdit(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードでフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFreeForRireki(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '編集モードフリー入力タブデータ設定処理と同じ
            If SetDataToTabFreeForEdit(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて関係情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabRelation(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0501

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToTabRelationForNew(dataHBKB0501) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetDataToTabRelationForEdit(dataHBKB0501) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetDataToTabRelationForRef(dataHBKB0501) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetDataToTabRelationForRireki(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードで関係情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabRelationForNew(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0501

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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで関係情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabRelationForEdit(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0501

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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで関係情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabRelationForRef(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '編集モード関係情報タブデータ設定処理と同じ
            If SetDataToTabRelationForEdit(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで関係情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabRelationForRireki(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '編集モード関係情報タブデータ設定処理と同じ
            If SetDataToTabRelationForEdit(dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>各スプレッド（一覧）を初期設定する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetVwControl(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0501


                '原因リンク一覧
                With .PropVwMngNmb.Sheets(0)
                    .DataSource = dataHBKB0501.PropDtMyCauseLink
                    .Columns(COL_CAUSELINK_KBN_NMR).DataField = "ProcessKbnNMR"
                    .Columns(COL_CAUSELINK_NO).DataField = "MngNmb"
                    .Columns(COL_CAUSELINK_KBN).DataField = "ProcessKbn"
                    .Columns(COL_CAUSELINK_KBN).Visible = False
                End With

                '履歴情報一覧
                With .PropVwRegReason.Sheets(0)
                    .DataSource = dataHBKB0501.PropDtRireki
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システムコード保持用の隠しラベルを非表示にする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetHiddenLabelUnvisible(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0501

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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>他のユーザによりロックが解除された場合に編集中の入力内容をログに出力する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function OutputUnlockLog(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strPlmList As New List(Of String)       'フォーマット埋込用パラメータ配列
        Dim strLogFilePath As String = ""           'ログファイルパス
        Dim strLogFileName As String                'ログファイル名
        Dim strOutputDir As String                  'ログ出力フォルダ
        Dim sw As StreamWriter = Nothing            'ファイル書込用クラス

        Try
            With dataHBKB0501

                '書込用テキスト作成
                strPlmList.Add(.PropLblCINmb.Text)                                          'CI番号
                strPlmList.Add(.PropLblCIKbnNM.Text)                                        'CI種別
                strPlmList.Add(.PropCmbKind.Text)                                           '種別
                strPlmList.Add(.PropTxtNum.Text)                                            '番号（手動）
                strPlmList.Add(.PropTxtVersion.Text)                                        '版（手動）
                strPlmList.Add(.PropTxtClass1.Text)                                         '分類1
                strPlmList.Add(.PropTxtClass2.Text)                                         '分類2
                strPlmList.Add(.PropTxtCINM.Text)                                           '名称
                strPlmList.Add(.PropCmbCIStatus.Text)                                       'ステータス
                strPlmList.Add(.PropTxtCINaiyo.Text)                                        '説明
                strPlmList.Add(.ProptxtCrateID.Text & " " & .ProptxtCrateNM.Text)           '作成者ID
                strPlmList.Add(.PropDtpCreateDT.txtDate.Text)                               '作成年月日
                strPlmList.Add(.ProptxtLastUpID.Text & " " & .ProptxtLastUpNM.Text)         '更新者ID
                strPlmList.Add(.PropDtpLastUpDT.txtDate.Text)                               '最終更新日時
                strPlmList.Add(.ProptxtChargeID.Text & " " & .ProptxtChargeNM.Text)         '文書責任者ID
                strPlmList.Add(.ProptxtOfferNM.Text)                                        '文書提供者
                strPlmList.Add(.ProptxtShareteamNM.Text)                                    '文書配布先
                strPlmList.Add(.ProptxtFilePath.Text)                                       '文書格納パス
                strPlmList.Add(.PropDtpDelDT.txtDate.Text)                                  '文書廃棄年月日
                strPlmList.Add(.ProptxtDelReason.Text)                                      '文書廃棄理由

                strPlmList.Add(.PropTxtBIko1.Text)                                          'フリーテキスト１
                strPlmList.Add(.PropTxtBIko2.Text)                                          'フリーテキスト２
                strPlmList.Add(.PropTxtBIko3.Text)                                          'フリーテキスト３
                strPlmList.Add(.PropTxtBIko4.Text)                                          'フリーテキスト４
                strPlmList.Add(.PropTxtBIko5.Text)                                          'フリーテキスト５

                'フリーフラグ１～５
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

                strPlmList.Add(.PropTxtCIOwnerNM.Text)                                      'CIオーナー名

                'ログ出力フォルダ設定
                strOutputDir = Path.Combine(Application.StartupPath, OUTPUT_DIR_UNLOCKEDLOG)

                'ログファイル名設定
                strLogFileName = Format(DateTime.Parse(.PropDtCILock.Rows(0).Item("EdiTime")), "yyyyMMddHHmmss") & ".log"

                'ファイル出力を実行し、出力ファイルパスを取得
                If commonLogicHBK.OutputLogFromTextFormat(strPlmList, strLogFileName, _
                                                          FORMAT_DIR_UNLOCKEDLOG, _
                                                          FILE_UNLOCKLOG_DOC, _
                                                          strOutputDir, _
                                                          strLogFilePath) = False Then
                    Return False
                End If

                'データクラスにメッセージをセット
                dataHBKB0501.PropStrBeUnlockedMsg = String.Format(HBK_W001, strLogFilePath)

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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータのロックが解除されていないかチェックする
    ''' <para>作成情報：2012/07/09 t.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckUnlock(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnBeUnocked As Boolean = False       'ロックフラグ

        Try

            With dataHBKB0501

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
    ''' 【共通】キー項目重複チェック処理
    ''' </summary>
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>キー項目が重複しているかチェックする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckIsSameKeyValue(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter()    'アダプタ
        Dim dtResult As New DataTable

        Try
            'コネクションを開く
            Cn.Open()

            '同じキー項目（分類１、分類２、名称）のデータ有無取得（SELECT）用SQLを作成
            If sqlHBKB0501.SetSelectCountSameKeySql(Adapter, Cn, dataHBKB0501) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "同じキー項目（分類１、分類２、名称）のデータ有無取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)


            '重複データがある場合、エラー
            If dtResult.Rows.Count > 0 Then

                'エラーメッセージ設定
                puErrMsg = B0501_E007
                'タブを基本情報タブに設定
                dataHBKB0501.PropTbInput.SelectedIndex = TAB_KHN
                'フォーカス設定（分類１）
                With dataHBKB0501.PropTxtClass1
                    .Focus()
                    .SelectAll()
                End With
                'エラーを返す
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
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            Adapter.Dispose()
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【新規登録モード】データ新規登録処理
    ''' </summary>
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をDBに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertNewData(ByVal dataHBKB0501 As DataHBKB0501) As Boolean

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
            If SelectNewCINmbAndSysDate(Cn, dataHBKB0501) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'CI共通情報新規登録
            If InsertCIInfo(Cn, dataHBKB0501) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'ファイルパスが空で無い場合新規登録
            If dataHBKB0501.ProptxtFilePath.Text <> "" Then
                '新規ファイル番号採番
                If SelectNewFileMngNmb(Cn, dataHBKB0501) = False Then
                    'ロールバック
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    Return False
                End If

                'ファイルアップロード処理
                If FileUpLoad(dataHBKB0501) = False Then
                    'ロールバック
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    'ファイル削除処理
                    If FileDelete(dataHBKB0501) = False Then
                        'ロールバック
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        Return False
                    End If
                    Return False
                End If

                'ファイル管理テーブル新規登録
                If InsertFileMng(Cn, dataHBKB0501) = False Then
                    'ロールバック
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    Return False
                End If
            End If

            'CI文書新規登録
            If InsertCIDoc(Cn, dataHBKB0501) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '履歴情報新規登録（共通）
            If InsertRireki(Cn, dataHBKB0501) = False Then
                'ロールバック
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
    ''' 【新規登録モード】新規ファイル管理番号取得処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番したファイル管理番号を取得（SELECT）する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectNewFileMngNmb(ByVal Cn As NpgsqlConnection, _
                                              ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtResult As New DataTable

        Try
            '新規ファイル管理番号取得（SELECT）用SQLを作成
            If sqlHBKB0501.SetSelectNewFileMngNmbSql(Adapter, Cn, dataHBKB0501) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規ファイル番号取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            'データが取得できた場合、データクラスに取得データをセット
            If dtResult.Rows.Count > 0 Then
                dataHBKB0501.PropIntFileMngNmb = dtResult.Rows(0).Item("FileMngNmb")      '新規ファイル番号
            Else
                '取得できなかったときはエラー
                puErrMsg = B0501_E009
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
    ''' 【編集／履歴モード】データ更新処理
    ''' </summary>
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をDBに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateData(ByVal dataHBKB0501 As DataHBKB0501) As Boolean

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
            If SelectSysDate(Adapter, Cn, dataHBKB0501) = False Then
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'CI共通情報更新（UPDATE）
            If UpdateCIInfo(Cn, dataHBKB0501) = False Then
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '編集モードのときのみ更新する
            If dataHBKB0501.PropStrProcMode = PROCMODE_EDIT Then
                'ファイルパスが入力されて時のみ採番、登録
                If dataHBKB0501.ProptxtFilePath.Text <> "" Then
                    '新規ファイル番号採番
                    If SelectNewFileMngNmb(Cn, dataHBKB0501) = False Then
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        Return False
                    End If
                    'ファイルアップロード処理
                    If FileUpLoad(dataHBKB0501) = False Then
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        'ファイル削除処理
                        If FileDelete(dataHBKB0501) = False Then
                            If Tsx IsNot Nothing Then
                                Tsx.Rollback()
                            End If
                            Return False
                        End If
                        Return False
                    End If

                    'ファイル管理テーブル登録(Update)
                    If InsertFileMng(Cn, dataHBKB0501) = False Then
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        Return False
                    End If
                End If
            End If

            'CI文書更新（UPDATE）
            If UpdateCIDoc(Cn, dataHBKB0501) = False Then
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '履歴情報新規登録（共通）
            If InsertRireki(Cn, dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータのロックを解除する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UnlockData(ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'CI共通情報ロック解除（DELETE）
            If commonLogicHBK.UnlockCIInfo(dataHBKB0501.PropIntCINmb) = False Then
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
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番したCI番号を取得（SELECT）する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectNewCINmbAndSysDate(ByVal Cn As NpgsqlConnection, _
                                              ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtResult As New DataTable

        Try
            '新規CI番号取得（SELECT）用SQLを作成
            If sqlHBKB0501.SetSelectNewCINmbAndSysDateSql(Adapter, Cn, dataHBKB0501) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規CI番号、システム日付取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            'データが取得できた場合、データクラスに取得データをセット
            If dtResult.Rows.Count > 0 Then
                dataHBKB0501.PropIntCINmb = dtResult.Rows(0).Item("CINmb")      '新規CI番号
                dataHBKB0501.PropDtmSysDate = dtResult.Rows(0).Item("SysDate")  'サーバー日付
            Else
                '取得できなかったときはエラー
                puErrMsg = B0501_E009
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
    ''' 【新規登録モード】新規CI番号取得処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番したCI番号を取得（SELECT）する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectNewCINmb(ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtCINmb As New DataTable

        Try
            '新規CI番号取得（SELECT）用SQLを作成
            If sqlHBKB0501.SetSelectNewCINmbSql(Adapter, Cn, dataHBKB0501) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規CI番号取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtCINmb)

            'データが取得できた場合、データクラスに新規CI番号をセット
            If dtCINmb.Rows.Count > 0 Then
                dataHBKB0501.PropIntCINmb = dtCINmb.Rows(0).Item("CINmb")
            Else
                '取得できなかったときはエラー
                puErrMsg = B0501_E009
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
            dtCINmb.Dispose()
            Adapter.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【新規登録モード】CI共通情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をCI共通情報テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCIInfo(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CI共通情報新規登録（INSERT）用SQLを作成
            If sqlHBKB0501.SetInsertCIInfoSql(Cmd, Cn, dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でCI共通情報テーブルを更新（UPDATE）する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateCIInfo(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CI共通情報更新（UPDATE）用SQLを作成
            If sqlHBKB0501.SetUpdateCIInfoSql(Cmd, Cn, dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>更新用のサーバー日付を取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectSysDate(ByRef Adapter As NpgsqlDataAdapter, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSysDate As New DataTable         '履歴番号格納用テーブル

        Try
            'SQLを作成
            If sqlHBKB0501.SetSelectSysDateSql(Adapter, Cn, dataHBKB0501) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サーバー日付取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtSysDate)

            'データが取得できた場合、データクラスに履歴番号をセット
            If dtSysDate.Rows.Count > 0 Then
                dataHBKB0501.PropDtmSysDate = dtSysDate.Rows(0).Item("SysDate")
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
    ''' 【新規登録モード】CI文書新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をCI文書テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCIDoc(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CI文書新規登録（INSERT）用SQLを作成
            If sqlHBKB0501.SetInsertCIDocSql(Cmd, Cn, dataHBKB0501) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI文書新規登録", Nothing, Cmd)

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
    ''' 【編集／履歴モード】CI文書更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でCI文書テーブルを更新（UPDATE）する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateCIDoc(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CI文書更新（UPDATE）用SQLを作成
            If sqlHBKB0501.SetUpdateCIDocSql(Cmd, Cn, dataHBKB0501) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI文書更新", Nothing, Cmd)

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
    ''' 作成者IDテキストボックスEnter時の処理
    ''' </summary>
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作成者IDEnter時の処理
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateIDEnterMain(ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'テーブル取得
        If GetEndUsrMasta(dataHBKB0501) = False Then
            Return False
        End If

        '作成者ID設定
        If SetNewCrateData(dataHBKB0501) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 更新者IDテキストボックスEnter時の処理
    ''' </summary>
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>最終更新者IDEnter時の処理
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function LastUpIDEnterMain(ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'テーブル取得
        If GetEndUsrMasta(dataHBKB0501) = False Then
            Return False
        End If

        '最終更新者ID設定
        If SetNewLastUpData(dataHBKB0501) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 文書責任者IDテキストボックスEnter時の処理
    ''' </summary>
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>文責任者IDEnter時の処理
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ChargeIDEnterMain(ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'テーブル取得
        If GetEndUsrMasta(dataHBKB0501) = False Then
            Return False
        End If

        '文書責任者ID設定
        If SetNewChargeData(dataHBKB0501) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 作成者[検索]ボタン押下時作成者情報セットメイン処理
    ''' </summary>
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サブ検索画面で選択されたエンドユーザーデータを当画面にセットする
    ''' <para>作成情報：2012/07/17 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetNewCrateDataMain(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'サブ検索画面で選択されたユーザー情報を利用者情報にセットする
        If SetNewCrateData(dataHBKB0501) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 作成者IDテキストボックスEnter時の処理
    ''' </summary>
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ＩＤテキストボックスにエンドユーザーマスタから取得した値を入力する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetNewCrateData(ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        With dataHBKB0501

            '選択データがある場合のみ値をセットする

            If .PropDtResultSub IsNot Nothing AndAlso .PropDtResultSub.Rows.Count = 1 Then

                '選択されたエンドユーザー情報を利用者情報にセットする
                '※連絡先、所属局、番組／部屋はクリア
                .ProptxtCrateID.Text = .PropDtResultSub.Rows(0).Item("EndUsrID")                'ユーザーID
                .ProptxtCrateNM.Text = .PropDtResultSub.Rows(0).Item("EndUsrNM")                'ユーザー氏名

            Else

                '取得データがない場合（ENTERキーにて検索した場合）クリア
                .ProptxtCrateID.Text = ""                                                         'ユーザーID
                .ProptxtCrateNM.Text = ""                                                         'ユーザー氏名

            End If
        End With

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 最終更新者[検索]ボタン押下時最終更新者情報セットメイン処理
    ''' </summary>
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サブ検索画面で選択されたエンドユーザーデータを当画面にセットする
    ''' <para>作成情報：2012/07/17 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetNewLastUpDataMain(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'サブ検索画面で選択されたユーザー情報を利用者情報にセットする
        If SetNewLastUpData(dataHBKB0501) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 最終更新者IDテキストボックスEnter時の処理
    ''' </summary>
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ＩＤテキストボックスにエンドユーザーマスタから取得した値を入力する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetNewLastUpData(ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        With dataHBKB0501

            '選択データがある場合のみ値をセットする
            If .PropDtResultSub IsNot Nothing AndAlso .PropDtResultSub.Rows.Count = 1 Then

                '選択されたエンドユーザー情報を利用者情報にセットする
                '※連絡先、所属局、番組／部屋はクリア
                .ProptxtLastUpID.Text = .PropDtResultSub.Rows(0).Item("EndUsrID")                'ユーザーID
                .ProptxtLastUpNM.Text = .PropDtResultSub.Rows(0).Item("EndUsrNM")                'ユーザー氏名

            Else

                '取得データがない場合（ENTERキーにて検索した場合）クリア
                .ProptxtLastUpID.Text = ""                                                         'ユーザーID
                .ProptxtLastUpNM.Text = ""                                                         'ユーザー氏名

            End If
        End With

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    '''　文書更新者[検索]ボタン押下時文書更新者情報セットメイン処理
    ''' </summary>
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サブ検索画面で選択されたエンドユーザーデータを当画面にセットする
    ''' <para>作成情報：2012/07/17 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetNewUsrDataMain(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'サブ検索画面で選択されたユーザー情報を利用者情報にセットする
        If SetNewChargeData(dataHBKB0501) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 文書責任者IDテキストボックスEnter時の処理
    ''' </summary>
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ＩＤテキストボックスにエンドユーザーマスタから取得した値を入力する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetNewChargeData(ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        With dataHBKB0501

            '選択データがある場合のみ値をセットする
            If .PropDtResultSub IsNot Nothing AndAlso .PropDtResultSub.Rows.Count = 1 Then

                '選択されたエンドユーザー情報を利用者情報にセットする
                '※連絡先、所属局、番組／部屋はクリア
                .ProptxtChargeID.Text = .PropDtResultSub.Rows(0).Item("EndUsrID")                'ユーザーID
                .ProptxtChargeNM.Text = .PropDtResultSub.Rows(0).Item("EndUsrNM")                'ユーザー氏名

            Else

                '取得データがない場合（ENTERキーにて検索した場合）クリア
                .ProptxtChargeID.Text = ""                                                         'ユーザーID
                .ProptxtChargeNM.Text = ""                                                         'ユーザー氏名

            End If
        End With

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' [検索]ボタン押下時CIオーナー情報セットメイン処理
    ''' </summary>
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サブ検索画面で選択されたCIオーナー情報データを当画面にセットする
    ''' <para>作成情報：2012/07/17 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetNewCIOwnerDataMain(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'サブ検索画面で選択されたCIオーナー情報データを設置情報にセットする
        If SetNewCIOwnerData(dataHBKB0501) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】CIオーナー設定
    ''' </summary>
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サブ検索にて選択された設置機器データを設置情報にセットする
    ''' <para>作成情報：2012/07/17 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetNewCIOwnerData(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501

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
    ''' 【共通】エンドユーザーマスタ取得処理
    ''' </summary>
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ファイル管理テーブルからファイルパスを取得し、ファイルをダウンロードする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetEndUsrMasta(ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        '変数宣言
        Dim dtEndUser As New DataTable

        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try
            '取得用SQLの作成・設定
            If commonLogicHBK.GetEndUsrMastaData(Adapter, Cn, dataHBKB0501.PropStrID, dataHBKB0501.PropDtResultSub) = False Then
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKB0501.PropDtEndUsrMasta = dataHBKB0501.PropDtResultSub

            '終了ログ出力
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
            dtEndUser.Dispose()
        End Try


    End Function

    ''' <summary>
    ''' 【共通】開くボタン押下時の処理
    ''' </summary>
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ファイル管理テーブルからファイルパスを取得し、ファイルを開く
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function FileOpenMain(ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'テーブル取得
        If SelectFileMng(dataHBKB0501) = False Then
            Return False
        End If

        'ファイル表示処理
        If FileLoad(dataHBKB0501) = False Then
            Return False
        End If

        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        Return True

    End Function

    ''' <summary>
    ''' ダウンロードボタン押下時の処理
    ''' </summary>
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ファイル管理テーブルからファイルパスを取得し、ファイルをダウンロードする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function FileDownLoadMain(ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'テーブル取得
        If SelectFileMng(dataHBKB0501) = False Then
            Return False
        End If

        'ファイルダウンロード処理
        If FileDownLoad(dataHBKB0501) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 登録時入力チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録項目の入力チェックを行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckInputValueMain(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '入力チェック処理
        If CheckInputValue(dataHBKB0501) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】ファイル管理テーブル取得処理
    ''' </summary>
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ファイル管理テーブルからファイルパスを取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectFileMng(ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ
        Dim Cn As New NpgsqlConnection(DbString)    'サーバーとクライアントをつなげる
        Dim dtFileMng As New DataTable

        Try

            Cn.Open()

            'SQLを作成
            If sqlHBKB0501.SetSelectFileMngNmbSql(Adapter, Cn, dataHBKB0501) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ファイル管理テーブル取得", Nothing, Adapter.SelectCommand)

            Adapter.Fill(dtFileMng)

            dataHBKB0501.PropDtFileMng = dtFileMng

            '終了ログ出力
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
            dtFileMng.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' ファイルを開く処理
    ''' </summary>
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ファイル管理テーブルからファイルパスを取得し、ファイルを開く
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function FileLoad(ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strCmd As String = ""               'コマンド文字列
        Dim strDriveName As String = ""         '使用論理ドライブ名
        Dim strOutputDir As String = Path.Combine(Application.StartupPath, OUTPUT_DIR_TEMP)         'ログ出力フォルダ設定
        Dim strDLFileName As String = dataHBKB0501.PropDtFileMng.Rows(0).Item("FileNM") & _
                                            "_" & Now().ToString("yyyyMMddmmss") & dataHBKB0501.PropDtFileMng.Rows(0).Item("Ext")           'TempファイルにDLするファイル名

        'ファイル管理テーブルに入っているファイルパスとファイル名と拡張子を結合する
        Dim strFilePath As String = dataHBKB0501.PropDtFileMng.Rows(0).Item("FilePath") & "\" & _
                                    dataHBKB0501.PropDtFileMng.Rows(0).Item("FileNM") & dataHBKB0501.PropDtFileMng.Rows(0).Item("Ext")

        Try

            ''ファイルを開く処理
            'If System.IO.File.Exists(strFilePath) Then

            'Dim fas As FileAttributes = File.GetAttributes(strFilePath)
            '' ファイル属性に読み取り専用を追加
            'fas = fas Or FileAttributes.ReadOnly
            '' ファイル属性を設定
            'File.SetAttributes(strFilePath, fas)

            'System.Diagnostics.Process.Start(strFilePath)
            ''System.Diagnostics.Process.Start(strFilePath).WaitForExit()

            '' ファイル属性から読み取り専用を削除
            'fas = fas And Not FileAttributes.ReadOnly
            '' ファイル属性を設定
            'File.SetAttributes(strFilePath, fas)

            'ファイルを開く修正-------------------------------------------------------------------

            '★★★--------------------------------------------------------
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

            'ファイルのコピー
            With dataHBKB0501
                Directory.CreateDirectory(strOutputDir)
                FileCopy(strDriveName & "\\" & strFilePath, strOutputDir & "\\" & strDLFileName)
            End With

            'ファイル存在チェック
            If System.IO.File.Exists(strOutputDir & "\\" & strDLFileName) Then

                Dim fas As System.IO.FileAttributes = System.IO.File.GetAttributes(strOutputDir & "\\" & strDLFileName)
                ' ファイル属性に読み取り専用を追加
                fas = fas Or System.IO.FileAttributes.ReadOnly
                ' ファイル属性を設定
                System.IO.File.SetAttributes(strOutputDir & "\\" & strDLFileName, fas)
                'プロセススタート
                System.Diagnostics.Process.Start(strOutputDir & "\\" & strDLFileName)

            End If
            '★★★--------------------------------------------------------
            'End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True
        Catch ex As System.IO.DirectoryNotFoundException
            'ディレクトリが見つからなかった場合
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)

            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & B0501_E013
            Return False

        Catch ex As System.IO.FileNotFoundException
            'ファイルが見つからなかった場合
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)

            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & B0501_E013
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
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ファイル管理テーブルからファイルパスを取得し、ファイルをダウンロードする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function FileDownLoad(ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strCmd As String = ""               'コマンド文字列
        Dim strDriveName As String = ""         '使用論理ドライブ名

        Dim sfd As New SaveFileDialog()     'ダイアログ

        'ファイル管理テーブルに入っているファイルパスとファイル名と拡張子を結合する
        Dim strFilePath As String = dataHBKB0501.PropDtFileMng.Rows(0).Item("FilePath") & "\" & _
                                    dataHBKB0501.PropDtFileMng.Rows(0).Item("FileNM") & dataHBKB0501.PropDtFileMng.Rows(0).Item("Ext")

        Try

            'ファイルダウンロード処理
            sfd.FileName = dataHBKB0501.PropDtCIInfo.Rows(0).Item("CINM") & dataHBKB0501.PropDtFileMng.Rows(0).Item("Ext")
            sfd.InitialDirectory = ""
            sfd.Filter = "すべてのファイル(*.*)|*.*"
            sfd.FilterIndex = 1
            sfd.Title = "保存先を指定してください"


            'If System.IO.File.Exists(strFilePath) Then
            '[mod] 2012/09/10 y.ikushima ファイルダウンロード処理修正 START
            ''ダイアログを表示する
            'If sfd.ShowDialog() = DialogResult.OK Then
            '    'OKボタンがクリックされたとき
            '    System.IO.File.Copy(strFilePath, sfd.FileName, True)
            'End If

            ''ダイアログを表示する
            'If sfd.ShowDialog() = DialogResult.OK Then

            '★★★--------------------------------------------------------

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

            'ファイルの存在チェック
            If System.IO.File.Exists(strDriveName & "\\" & strFilePath) = False Then
                'ファイルのコピー
                With dataHBKB0501
                    FileCopy(strDriveName & "\\" & strFilePath, sfd.FileName)
                End With
            End If

            'ファイルダイアログ表示
            If sfd.ShowDialog() = DialogResult.OK Then
                'ファイルのコピー
                With dataHBKB0501
                    FileCopy(strDriveName & "\\" & strFilePath, sfd.FileName)
                End With
            End If

            '★★★--------------------------------------------------------

            'End If
            'End If
            '[mod] 2012/09/10 y.ikushima ファイルダウンロード処理修正 END

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As System.IO.FileNotFoundException
            'ファイルが見つからなかった場合
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)

            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & B0501_E013
            Return False

        Catch ex As System.IO.DirectoryNotFoundException
            'ディレクトリが見つからなかった場合

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)

            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & B0501_E013
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
    ''' 【共通】コンボボックス作成処理
    ''' </summary>
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のコンボボックスを作成する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateCmb(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501

                '種別コンボボックス作成
                If commonLogic.SetCmbBox(.PropDtKindMasta, .PropCmbKind, False, "", "") = False Then
                    Return False
                End If

                'CIステータスコンボボックス作成
                If commonLogic.SetCmbBox(.PropDtCIStatusMasta, .PropCmbCIStatus, True, "", "") = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録項目の入力チェックを行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckInputValue(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501

                '分類１
                With .PropTxtClass1
                    '未入力の場合、エラー
                    If .Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = B0501_E002
                        'タブを基本情報タブに設定
                        dataHBKB0501.PropTbInput.SelectedIndex = TAB_KHN
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
                        puErrMsg = B0501_E003
                        'タブを基本情報タブに設定
                        dataHBKB0501.PropTbInput.SelectedIndex = TAB_KHN
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
                        puErrMsg = B0501_E004
                        'タブを基本情報タブに設定
                        dataHBKB0501.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With

                'キー項目重複チェック
                If CheckIsSameKeyValue(dataHBKB0501) = False Then
                    Return False
                End If

                'ステータス
                With .PropCmbCIStatus
                    '未入力の場合、エラー
                    If .SelectedValue = "" Then
                        'エラーメッセージ設定
                        puErrMsg = B0501_E005
                        'タブを基本情報タブに設定
                        dataHBKB0501.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With

                '現在時刻が入っていて日付が入っていない場合
                If .PropDtpLastUpDT.txtDate.Text = "" Then
                    If Not (.PropTxtDateTime.PropTxtTime.TextLength = 0) Then
                        'エラーメッセージ設定
                        puErrMsg = B0501_E011
                        'タブを基本情報タブに設定
                        dataHBKB0501.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .PropTxtDateTime.Focus()
                        .PropTxtDateTime.PropTxtTime.SelectAll()
                        'エラーを返す
                        Return False
                    End If

                End If

                '日付が入っていて現在時刻が入っていない場合
                If .PropTxtDateTime.PropTxtTime.Text = "" Then
                    If Not (.PropDtpLastUpDT.txtDate.TextLength = 0) Then
                        'エラーメッセージ設定
                        puErrMsg = B0501_E012
                        'タブを基本情報タブに設定
                        dataHBKB0501.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .PropTxtDateTime.Focus()
                        .PropTxtDateTime.PropTxtTime.SelectAll()
                        'エラーを返す
                        Return False
                    End If

                End If

                'ファイル格納パス
                With .ProptxtFilePath
                    '入力があった場合、エラー
                    If .Text <> "" Then
                        'ファイルが存在しなかった場合、エラー
                        If System.IO.File.Exists(.Text) = False Then
                            'エラーメッセージ設定
                            puErrMsg = B0501_E006
                            'タブを基本情報タブに設定
                            dataHBKB0501.PropTbInput.SelectedIndex = TAB_KHN
                            'フォーカス設定
                            .Focus()
                            .SelectAll()
                            'エラーを返す
                            Return False
                        End If
                    End If
                End With

                'CIオーナー
                If .PropTxtCIOwnerNM.Text.Trim <> "" And _
                    .PropLblCIOwnerCD.Text = "" Then

                    'オーナー名に入力があってコードが未入力の場合（サブ検索にて選択していない場合）、エラー
                    puErrMsg = B0501_E008
                    'タブを関係情報タブに設定
                    .PropTbInput.SelectedIndex = TAB_RELATION
                    'フォーカス設定
                    .PropBtnSearchGrp.Focus()
                    'エラーを返す
                    Return False

                End If

                'ファイル格納パス桁数チェック
                With .ProptxtFilePath
                    '桁数が多すぎた場合、エラー
                    If Path.GetFileName(.Text).Length > 174 Then
                        'エラーメッセージ設定
                        puErrMsg = B0501_E014
                        'タブを基本情報タブに設定
                        dataHBKB0501.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
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

    ''' <summary>
    ''' 【共通モード】ファイル管理テーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をファイル管理テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertFileMng(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'ファイル管理テーブル新規登録（INSERT）用SQLを作成
            If sqlHBKB0501.SetInsertFileMngSql(Cmd, Cn, dataHBKB0501) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ファイル管理テーブル新規登録", Nothing, Cmd)

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
    ''' 【共通】履歴情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴／変更理由を各テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRireki(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter    'アダプタ
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            '新規履歴番号取得
            If GetNewRirekiNo(Adapter, Cn, dataHBKB0501) = False Then
                Return False
            End If

            'CI共通情報履歴テーブル登録
            If InsertCIInfoR(Cn, dataHBKB0501) = False Then
                Return False
            End If

            'CI文書履歴テーブル登録
            If InsertCIDocR(Cn, dataHBKB0501) = False Then
                Return False
            End If

            '登録理由履歴テーブル登録
            If InsertRegReasonR(Cn, dataHBKB0501) = False Then
                Return False
            End If

            '原因リンク履歴テーブル登録
            If InsertCauseLinkR(Cn, dataHBKB0501) = False Then
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
    ''' 【共通】新規履歴番号取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapter</param>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番した履歴番号を取得する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetNewRirekiNo(ByVal Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtRirekiNo As New DataTable         '履歴番号格納用テーブル

        Try

            'SQLを作成
            If sqlHBKB0501.SetSelectNewRirekiNoSql(Adapter, Cn, dataHBKB0501) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規履歴番号取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtRirekiNo)

            'データが取得できた場合、データクラスに履歴番号をセット
            If dtRirekiNo.Rows.Count > 0 Then
                dataHBKB0501.PropIntRirekiNo = dtRirekiNo.Rows(0).Item("RirekiNo")
            Else
                '取得できなかった場合はエラー
                puErrMsg = B0501_E010
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
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCIInfoR(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0501.SetInsertCIInfoRSql(Cmd, Cn, dataHBKB0501) = False Then
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
    ''' 【共通】CI文書履歴テーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI文書履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCIDocR(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0501.SetInsertCIDocRSql(Cmd, Cn, dataHBKB0501) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI文書履歴新規登録", Nothing, Cmd)

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
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録理由履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRegReasonR(ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0501.SetInsertRegReasonRSql(Cmd, Cn, dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>原因リンク履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCauseLinkR(ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'データ数分繰り返し、登録を行う
            For i As Integer = 0 To dataHBKB0501.PropDtCauseLink.Rows.Count - 1

                '登録行をデータクラスにセット
                dataHBKB0501.PropRowReg = dataHBKB0501.PropDtCauseLink.Rows(i)

                'SQLを作成
                If sqlHBKB0501.SetInsertCauseLinkRSql(Cmd, Cn, dataHBKB0501) = False Then
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
    ''' <param name="dataHBKB0501">[IN/OUT]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録系ボタンを非活性にする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetUnabledWhenError(ByRef dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0501

                '登録系ボタンコントロールを非活性にする
                .PropBtnReg.Enabled = False                 '登録ボタン
                .PropBtnRollBack.Enabled = False            'ロールバックボタン

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
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をデータベースに反映する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RollBackDataMain(ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロールバック処理を行う　※編集モード時の更新処理と同じ
        If UpdateData(dataHBKB0501) = False Then
            Return False
        End If

        'ロック解除処理
        If UnlockData(dataHBKB0501) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' ファイルアップロード処理
    ''' </summary>
    ''' <param name="dataHBKB0501">[IN]dataHBKB0501クラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/07/18 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function FileUpLoad(ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strFilePath As String
        Dim strSystemDirpath As String
        Dim strCmd As String = ""               'コマンド文字列
        Dim strDriveName As String = ""         '使用論理ドライブ名
        Dim strFileName As String = ""           '出力

        Try
            ''登録ファイルパス取得
            'strFilePath = dataHBKB0501.ProptxtFilePath.Text

            ''登録先パス
            'strSystemDirpath = Path.Combine(PropFileStorageRootPath, PropFileManagePath, "構成管理")
            'strSystemDirpath = strSystemDirpath & "\" & dataHBKB0501.PropIntCINmb & "\" & dataHBKB0501.PropIntFileMngNmb

            ''コピー先ディレクトリ存在チェック
            'If Directory.Exists(strSystemDirpath) = False Then
            '    'コピー先ディレクトリが見つからない場合は作成
            '    Directory.CreateDirectory(strSystemDirpath)
            'End If

            'strSystemDirpath = strSystemDirpath & "\" & Path.GetFileName(strFilePath)

            ''ファイルコピー　※同名のファイルがあった場合は上書きする
            'System.IO.File.Copy(strFilePath, strSystemDirpath, True)


            '★★★--------------------------------------------------------
            '登録ファイルパス取得
            strFilePath = dataHBKB0501.ProptxtFilePath.Text

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

            'アップロード先のディレクトリセット
            strSystemDirpath = Path.Combine(strDriveName, PropFileManagePath, OUTPUT_FILE_DIR_DOC)
            strSystemDirpath = strSystemDirpath & "\" & dataHBKB0501.PropIntCINmb & "\" & dataHBKB0501.PropIntFileMngNmb

            'コピー先ディレクトリ存在チェック
            If Directory.Exists(strSystemDirpath) = False Then
                'コピー先ディレクトリが見つからない場合は作成
                Directory.CreateDirectory(strSystemDirpath)
            End If

            'ファイル存在チェック
            If System.IO.File.Exists(strFilePath) Then
                'ファイルのコピー
                With dataHBKB0501
                    FileCopy(strFilePath, strSystemDirpath & "\" & Path.GetFileName(strFilePath))
                End With
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
            '接続した論理ドライブの削除
            commonLogicHBK.NetUseConectDel(strDriveName)
        End Try

    End Function


    ''' <summary>
    ''' フォルダ削除処理
    ''' </summary>
    ''' <param name="dataHBKB0501">[IN]文書登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ファイルアップロード失敗時に登録したフォルダを削除する。
    ''' <para>作成情報：2012/07/23 m.ibuki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function FileDelete(ByVal dataHBKB0501 As DataHBKB0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strCmd As String = ""               'コマンド文字列
        Dim strDriveName As String = ""         '使用論理ドライブ名
        Dim strOutputDir As String = Path.Combine(Application.StartupPath, OUTPUT_DIR_TEMP)         '出力フォルダ設定
        Dim strFilePath As String = ""

        Try

            ''アップロードファイル登録先フォルダパス
            'strSystemDirpath = Path.Combine(PropFileStorageRootPath, PropFileManagePath, OUTPUT_FILE_DIR_DOC)
            'strSystemDirpath = strSystemDirpath & "\" & dataHBKB0501.PropIntCINmb & "\" & dataHBKB0501.PropIntFileMngNmb

            ''コピー先ディレクトリ存在チェック
            'If Directory.Exists(strSystemDirpath) = True Then
            '    'ディレクトリを削除する。（ファイルの中身が空の場合）
            '    System.IO.Directory.Delete(strSystemDirpath, False)
            'End If

            '★★★--------------------------------------------------------

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

            strFilePath = Path.Combine(strDriveName, PropFileManagePath, OUTPUT_FILE_DIR_DOC)
            strFilePath = strFilePath & "\" & dataHBKB0501.PropIntCINmb & "\" & dataHBKB0501.PropIntFileMngNmb

            'ファイル存在チェック
            If System.IO.File.Exists(strFilePath) Then
                'ディレクトリを削除する。（ファイルの中身が空の場合）
                System.IO.Directory.Delete(strFilePath, False)
            End If
            '★★★--------------------------------------------------------

            '終了ログ出力
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
            '接続した論理ドライブの削除
            commonLogicHBK.NetUseConectDel(strDriveName)
        End Try

    End Function


End Class
