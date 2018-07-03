Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.IO
Imports System.Windows.Forms

''' <summary>
''' システム登録画面ロジッククラス
''' </summary>
''' <remarks>システム登録画面のロジックを定義したクラス
''' <para>作成情報：2012/06/13 t.fukuo
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKB0401

    'インスタンス作成
    Private sqlHBKB0401 As New SqlHBKB0401
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    'Public定数宣言==============================================
    'ノウハウURL一覧列番号
    Public Const COL_KNOWHOWURL_URL As Integer = 0          'URL
    Public Const COL_KNOWHOWURL_NAIYO As Integer = 1        '説明
    'サーバー管理番号一覧列番号
    Public Const COL_MNGSRV_NO As Integer = 0               'サーバー管理番号
    Public Const COL_MNGSRV_NAIYO As Integer = 1            '説明
    '関係者情報一覧列番号
    Public Const COL_RELATION_KBN As Integer = 0            '区分
    Public Const COL_RELATION_ID As Integer = 1             'ID
    Public Const COL_RELATION_GROUPNM As Integer = 2        'グループ名
    Public Const COL_RELATION_USERNM As Integer = 3         'ユーザー名
    '[Mod] 2012/08/02 y.ikushima 関係者情報DB定義変更START
    'Public Const COL_RELATION_GROUPID As Integer = 4         'グループID（ユーザ区分時、グループID保存用）
    '[Mod] 2012/08/02 y.ikushima 関係者情報DB定義変更END
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システムエラー発生時に非活性にするコントロールリストを作成する
    ''' <para>作成情報：2012/06/14 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DoProcForErrorMain(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'トランザクション系コントロールリスト作成
        If CreateTsxCtlList(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて画面の初期表示設定を行う
    ''' <para>作成情報：2012/06/14 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormNewModeMain(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'スプレッド表示用データテーブル作成
        If CreateDataTableForVw(dataHBKB0401) = False Then
            Return False
        End If

        'フォームコントロール設定
        If InitFormControl(dataHBKB0401) = False Then
            Return False
        End If

        '初期表示用データ取得
        If GetInitData(dataHBKB0401) = False Then
            Return False
        End If

        '初期表示用データセット
        If SetInitDataToControl(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>データのロック状況をチェックし、状況に応じてロックまたは処理モードの切替を行う
    ''' <para>作成情報：2012/06/14 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function LockMain(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'ロック設定
        If SetLockWhenLoad(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで画面の初期表示設定を行う
    ''' <para>作成情報：2012/06/14 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormEditModeMain(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'スプレッド表示用データテーブル作成
        If CreateDataTableForVw(dataHBKB0401) = False Then
            Return False
        End If

        'フォームコントロール設定
        If InitFormControl(dataHBKB0401) = False Then
            Return False
        End If

        '初期表示用データ取得
        If GetInitData(dataHBKB0401) = False Then
            Return False
        End If

        '初期表示用データセット
        If SetInitDataToControl(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで画面の初期表示設定を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormRefModeMain(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'スプレッド表示用データテーブル作成
        If CreateDataTableForVw(dataHBKB0401) = False Then
            Return False
        End If

        'フォームコントロール設定
        If InitFormControl(dataHBKB0401) = False Then
            Return False
        End If

        '初期表示用データ取得
        If GetInitData(dataHBKB0401) = False Then
            Return False
        End If

        '初期表示用データセット
        If SetInitDataToControl(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードで画面の初期表示設定を行う
    ''' <para>作成情報：2012/06/14 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormRirekiModeMain(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'スプレッド表示用データテーブル作成
        If CreateDataTableForVw(dataHBKB0401) = False Then
            Return False
        End If

        'フォームコントロール設定
        If InitFormControl(dataHBKB0401) = False Then
            Return False
        End If

        '初期表示用データ取得
        If GetInitData(dataHBKB0401) = False Then
            Return False
        End If

        '初期表示用データセット
        If SetInitDataToControl(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータをログインユーザでロックし、フォームのロックを解除する
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function UnlockWhenClickBtnUnlockMain(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロックフラグOFF
        dataHBKB0401.PropBlnBeLockedFlg = False

        'ロック処理
        If SetLockWhenUnlock(dataHBKB0401) = False Then
            Return False
        End If

        'フォームコントロールのロックを解除する
        If SetFormControlPerProcMode(dataHBKB0401) = False Then
            Return False
        End If
        If SetDataToLoginAndLock(dataHBKB0401) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' ノウハウURL行追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ノウハウURL一覧に空行を1行追加する
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function AddRowKnowHowUrlMain(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '空行追加処理
        If AddRowKnowHowUrl(dataHBKB0401) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' ノウハウURL行削除時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ノウハウURL一覧の選択行を削除する
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowKnowHowUrlMain(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowKnowHowUrl(dataHBKB0401) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' サーバー管理情報行追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ノウハウURL一覧に空行を1行追加する
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function AddRowKnowMngSrvMain(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '空行追加処理
        If AddRowMngSrv(dataHBKB0401) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' サーバー管理情報行削除時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サーバー管理情報一覧の選択行を削除する
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowMngSrvMain(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowMngSrv(dataHBKB0401) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 関係者情報グループ追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者情報一覧にサブ検索画面から取得したグループデータを設定する
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetGroupToVwRelationMain(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'グループデータ設定処理
        If SetGroupToVwRelation(dataHBKB0401) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 関係者情報ユーザー追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者情報一覧にサブ検索画面から取得したユーザーデータを設定する
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetUserToVwRelationMain(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ユーザーデータ設定処理
        If SetUserToVwRelation(dataHBKB0401) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 関係者情報行削除時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者情報一覧の選択行を削除する
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowRelationMain(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowRelation(dataHBKB0401) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】コントロール入力チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録項目の入力チェックを行う
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckInputValueMain(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'コントロール入力チェック
        If CheckInputValue(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>他のユーザによりロックが解除されていないかチェックする
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckBeUnlockedMain(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロック解除チェック
        If CheckUnlock(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面の内容をログに出力し、フォームコントロールを再設定する
    ''' <para>作成情報：2012/06/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetFormRefModeFromEditModeMain(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '入力内容ログ出力
        If OutputUnlockLog(dataHBKB0401) = False Then
            Return False
        End If

        '参照モードでフォームコントロール設定
        If SetFormControlPerProcMode(dataHBKB0401) = False Then
            Return False
        End If

        '参照モードでロック情報設定
        If SetDataToLoginAndLockForRef(dataHBKB0401) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【履歴モード】ロック解除された時のメイン処理
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面の内容をログに出力し、フォームコントロールを再設定する
    ''' <para>作成情報：2012/06/30 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetFormRirekiModeBeUnlockedMain(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '入力内容ログ出力
        If OutputUnlockLog(dataHBKB0401) = False Then
            Return False
        End If

        '履歴モードでフォームコントロール設定
        If SetFormControlPerProcMode(dataHBKB0401) = False Then
            Return False
        End If

        '履歴モードでロック情報設定
        If SetDataToLoginAndLockForRireki(dataHBKB0401) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【新規登録モード】データ登録メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をデータベースに反映する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RegistDataOnNewModeMain(ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '新規登録処理
        If InsertNewData(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をデータベースに反映する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RegistDataOnEditModeMain(ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '更新処理
        If UpdateData(dataHBKB0401) = False Then
            Return False
        End If

        'ロック解除処理
        If UnlockData(dataHBKB0401) = False Then
            Return False
        End If

        '画面再描画
        If InitFormEditModeMain(dataHBKB0401) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' ロールバック時データ登録メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をデータベースに反映する
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RollBackDataMain(ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロールバック処理を行う　※編集モード時の更新処理と同じ
        If UpdateData(dataHBKB0401) = False Then
            Return False
        End If

        'ロック解除処理
        If UnlockData(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータのロックを解除する
    ''' <para>作成情報：2012/06/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function UnlockWhenCloseMain(ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロック解除処理
        If UnLockData(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>トランザクション系コントロールのリストを作成する
    ''' <para>作成情報：2012/07/05 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateTsxCtlList(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim aryCtlList As New ArrayList

        Try
            With dataHBKB0401

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
    ''' 【共通】スプレッド用データテーブル作成処理
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドのデータソースとして設定するデータテーブルを作成する
    ''' <para>作成情報：2012/06/14 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateDataTableForVw(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtKnowHowUrl As New DataTable   'ノウハウURL用データテーブル
        Dim dtMngSrv As New DataTable       'サーバー管理情報用データテーブル
        Dim dtRelation As New DataTable     '関係者情報用データテーブル
        Dim dtCauseLink As New DataTable    '原因リンク用データテーブル
        Dim dtRireki As New DataTable       '履歴情報用データテーブル

        Try
            'ノウハウURL用テーブル作成
            With dtKnowHowUrl
                .Columns.Add("Url", Type.GetType("System.String"))                 'URL
                .Columns.Add("UrlNaiyo", Type.GetType("System.String"))            '説明
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'サーバー管理情報用テーブル作成
            With dtMngSrv
                .Columns.Add("ManageNmb", Type.GetType("System.String"))           'サーバー管理番号
                .Columns.Add("ManageNmbNaiyo", Type.GetType("System.String"))      '説明
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            '関係者情報用テーブル作成
            With dtRelation
                .Columns.Add("RelationKbn", Type.GetType("System.String"))         '区分
                '[Mod] 2012/08/02 y.ikushima 関係者情報DB定義変更START
                '.Columns.Add("RelationGrpCD", Type.GetType("System.String"))       'グループID
                '[Mod] 2012/08/02 y.ikushima 関係者情報DB定義変更END
                .Columns.Add("RelationID", Type.GetType("System.String"))       'ユーザID
                .Columns.Add("GroupNM", Type.GetType("System.String"))             'グループ名
                .Columns.Add("HBKUsrNM", Type.GetType("System.String"))            'ユーザー名
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            '原因リンク用テーブル作成
            With dtCauseLink
                .Columns.Add("ProcessKbnNMR", Type.GetType("System.String"))       'プロセス区分（略名称）
                .Columns.Add("MngNmb", Type.GetType("System.String"))              '番号
                .Columns.Add("ProcessKbn", Type.GetType("System.String"))          'プロセス区分（コード）
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
            With dataHBKB0401
                .PropDtKnowHowUrl = dtKnowHowUrl            'スプレッド表示用：ノウハウURLデータ
                .PropDtMngSrv = dtMngSrv                    'スプレッド表示用：サーバー管理情報データ
                .PropDtRelation = dtRelation                'スプレッド表示用：関係者情報データ
                .PropDtMyCauseLink = dtCauseLink            'スプレッド表示用：原因リンクデータ
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
            dtKnowHowUrl.Dispose()
            dtMngSrv.Dispose()
            dtRelation.Dispose()
            dtCauseLink.Dispose()
            dtRireki.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】初期表示用データ取得処理
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            'マスタデータ取得
            If GetMastaData(Adapter, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'メイン表示データ取得
            If GetMainData(Adapter, Cn, dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/06/14 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLockWhenUnlock(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'CI共通情報テーブルロック解除
            If commonLogicHBK.UnlockCIInfo(dataHBKB0401.PropIntCINmb) = False Then
                Return False
            End If

            'CI共通情報テーブルロック
            If commonLogicHBK.LockCIInfo(dataHBKB0401.PropIntCINmb, dataHBKB0401.PropDtCILock) = False Then
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
    ''' 【編集モード】フォームロード時ロック設定処理
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/06/14 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLockWhenLoad(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnBeLocked As Boolean = False                   'ロックフラグ  

        Try

            With dataHBKB0401

                'ロック解除チェック
                If commonLogicHBK.CheckDataBeLocked(.PropIntCINmb, blnBeLocked, .PropStrBeLockedMsg, .PropDtCILock) = False Then
                    Return False
                End If

                'ロックされている（別のユーザが編集中）場合、 ロックフラグをON
                If blnBeLocked = True Then

                    .PropBlnBeLockedFlg = True

                Else

                    'ロックされていない場合、CI共通情報をロックする
                    If SetLock(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集開始日時によりロック設定を行う
    ''' <para>作成情報：2012/06/21 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckAndSetLock(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnBeLocked As Boolean = False                   'ロックフラグ  

        Try

            With dataHBKB0401

                'ロック解除チェック
                If commonLogicHBK.CheckDataBeLocked(.PropIntCINmb, blnBeLocked, .PropStrBeLockedMsg, .PropDtCILock) = False Then
                    Return False
                End If

                'ロックされている（別のユーザが編集中）場合、 ロックフラグをON
                If blnBeLocked = True Then

                    .PropBlnBeLockedFlg = True

                Else

                    'ロックされていない場合、CI共通情報をロックする
                    If SetLock(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報テーブルをロックする
    ''' <para>作成情報：2012/06/21 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLock(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnDoUnlock As Boolean = False                   'ロック解除実行フラグ

        Try

            With dataHBKB0401

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
    ''' 【共通】フォームコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームコントロールの初期設定を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InitFormControl(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'スプレッド設定
            If SetVwControl(dataHBKB0401) = False Then
                Return False
            End If

            '隠しラベル非表示設定
            If SetHiddenLabelUnvisible(dataHBKB0401) = False Then
                Return False
            End If

            '処理モードに応じたフォームコントロール設定
            If SetFormControlPerProcMode(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフォームコントロールの設定を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormControlPerProcMode(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'ログイン／ロックコントロール設定
            If SetLoginAndLockControl(dataHBKB0401) = False Then
                Return False
            End If

            'ヘッダ設定
            If SetHeaderControl(dataHBKB0401) = False Then
                Return False
            End If

            'フッタ設定
            If SetFooterControl(dataHBKB0401) = False Then
                Return False
            End If

            'タブページ設定
            If SetTabControl(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControl(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetLoginAndLockControlForNew(dataHBKB0401) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetLoginAndLockControlForEdit(dataHBKB0401) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照（ロック）モード用設定
                        If SetLoginAndLockControlForRef(dataHBKB0401) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetLoginAndLockControlForRireki(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForNew(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401.PropGrpLoginUser

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForEdit(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401.PropGrpLoginUser

                'グループコンボボックス非活性
                .cmbGroup.Enabled = False

                '変更ボタン非活性
                .btnChange.Enabled = False

                'ロック情報表示
                .PropLockInfoVisible = True

                '解除ボタン表示
                .PropBtnUnlockVisible = True
                'ロック情報が取得できた場合
                If dataHBKB0401.PropDtCILock.Rows.Count > 0 Then

                    '編集者と同じグループの場合は解除ボタン活性化
                    If .cmbGroup.SelectedValue = dataHBKB0401.PropDtCILock.Rows(0).Item("EdiGrpCD") And _
                       PropUserId <> dataHBKB0401.PropDtCILock.Rows(0).Item("EdiID") Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForRef(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401.PropGrpLoginUser

                'グループコンボボックス非活性
                .cmbGroup.Enabled = False

                '変更ボタン非活性
                .btnChange.Enabled = False

                'ロック情報表示
                .PropLockInfoVisible = True

                '解除ボタン表示
                .PropBtnUnlockVisible = True

                'ロック情報が取得できた場合
                If dataHBKB0401.PropDtCILock.Rows.Count > 0 Then

                    '編集者と同じグループの場合は解除ボタン活性化
                    If .cmbGroup.SelectedValue = dataHBKB0401.PropDtCILock.Rows(0).Item("EdiGrpCD") Then
                        .PropBtnUnlockEnabled = True
                    Else
                        .PropBtnUnlockEnabled = False
                    End If

                Else

                    '解除ボタン非活性
                    .PropBtnUnlockEnabled = False

                End If

                'ロック解除から遷移してきた場合は解除ボタンを非活性
                If dataHBKB0401.PropBlnLockCompare = True Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForRireki(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401.PropGrpLoginUser

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてヘッダコントロールの初期設定を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetHeaderControl(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401


                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetHeaderControlForNew(dataHBKB0401) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetHeaderControlForEdit(dataHBKB0401) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetHeaderControlForRef(dataHBKB0401) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetHeaderControlForRireki(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでヘッダコントロールの初期設定を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetHeaderControlForNew(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでヘッダコントロールの初期設定を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetHeaderControlForEdit(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでヘッダコントロールの初期設定を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetHeaderControlForRef(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでヘッダコントロールの初期設定を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetHeaderControlForRireki(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControl(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetFooterControlForNew(dataHBKB0401) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetFooterControlForEdit(dataHBKB0401) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetFooterControlForRef(dataHBKB0401) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetFooterControlForRireki(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForNew(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForEdit(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForRef(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForRireki(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401

                '登録ボタン非表示
                .PropBtnReg.Visible = False

                '呼び出し元がシステム登録画面でないかロック／ロック解除されている場合、ロールバックボタン非活性
                If .PropIntFromRegSystemFlg = 0 Or .PropBlnBeLockedFlg = True Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてタブコントロールの初期設定を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControl(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '基本情報タブ設定
            If SetTabControlKhn(dataHBKB0401) = False Then
                Return False
            End If

            'フリー入力情報タブ設定
            If SetTabControlFree(dataHBKB0401) = False Then
                Return False
            End If

            '関係情報タブ設定
            If SetTabControlRelation(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhn(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetTabControlKhnForNew(dataHBKB0401) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetTabControlKhnForEdit(dataHBKB0401) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照（ロック）モード用設定
                        If SetTabControlKhnForRef(dataHBKB0401) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetTabControlKhnForRireki(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードに応じて基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForNew(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401


                '種別コンボボックス
                .PropCmbKind.Enabled = False

                '番号テキストボックス
                .PropTxtCINmb.ReadOnly = True

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForEdit(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401

                '種別コンボボックス
                .PropCmbKind.Enabled = False

                '番号テキストボックス
                .PropTxtCINmb.ReadOnly = True

                'ノウハウURL一覧
                .PropBtnAddRow_Url.Enabled = True       '＋ボタン
                .PropBtnRemoveRow_Url.Enabled = True    '－ボタン

                'サーバー管理情報一覧
                .PropBtnAddRow_Srv.Enabled = True       '＋ボタン
                .PropBtnRemoveRow_Srv.Enabled = True    '－ボタン

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForRef(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401

                '種別コンボボックス
                .PropCmbKind.Enabled = False

                '番号テキストボックス
                .PropTxtCINmb.ReadOnly = True

                'ノウハウURL一覧
                .PropBtnAddRow_Url.Enabled = False      '＋ボタン
                .PropBtnRemoveRow_Url.Enabled = False   '－ボタン

                'サーバー管理情報一覧
                .PropBtnAddRow_Srv.Enabled = False      '＋ボタン
                .PropBtnRemoveRow_Srv.Enabled = False   '－ボタン

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForRireki(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401


                '種別コンボボックス
                .PropCmbKind.Enabled = False

                '番号テキストボックス
                .PropTxtCINmb.ReadOnly = True

                '分類１～２テキストボックス
                .PropTxtClass1.ReadOnly = True
                .PropTxtClass2.ReadOnly = True

                '名称テキストボックス
                .PropTxtCINM.ReadOnly = True

                'ステータスコンボボックス
                .PropCmbCIStatus.Enabled = False

                '情報共有先テキストボックス
                .PropTxtInfShareteamNM.ReadOnly = True

                '説明テキストボックス
                .PropTxtCINaiyo.ReadOnly = True

                'ノウハウURL一覧
                .PropVwKnowHowUrl.Sheets(0).DataSource = .PropDtKnowHowUrl
                If commonLogicHBK.SetSpreadUnabled(.PropVwKnowHowUrl, 0) = False Then
                    Return False
                End If
                .PropBtnAddRow_Url.Enabled = False      '＋ボタン
                .PropBtnRemoveRow_Url.Enabled = False   '－ボタン

                'サーバー管理情報一覧
                .PropVwSrvMng.Sheets(0).DataSource = .PropDtMngSrv
                If commonLogicHBK.SetSpreadUnabled(.PropVwSrvMng, 0) = False Then
                    Return False
                End If
                .PropBtnAddRow_Srv.Enabled = False      '＋ボタン
                .PropBtnRemoveRow_Srv.Enabled = False   '－ボタン

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフリー入力情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlFree(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401


                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モードなし


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード

                    '編集／参照（ロック）モードなし


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetTabControlFreeForRireki(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフリー入力情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlFreeForRireki(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて関係情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRelation(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モードなし


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetTabControlRelationForEdit(dataHBKB0401) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照（ロック）モード用設定
                        If SetTabControlRelationForRef(dataHBKB0401) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    If SetTabControlRelationForRireki(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで関係情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRelationForEdit(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401

                '検索ボタン
                .PropBtnSearchGrp.Enabled = True

                '関係者情報一覧
                .PropBtnAddRow_Grp.Enabled = True           '＋グループボタン
                .PropBtnAddRow_Usr.Enabled = True           '＋ユーザーボタン
                .PropBtnRemoveRow_Relation.Enabled = True   '－ボタン

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで関係情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRelationForRef(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401

                '検索ボタン
                .PropBtnSearchGrp.Enabled = False

                '関係者情報一覧
                .PropBtnAddRow_Grp.Enabled = False          '＋グループボタン
                .PropBtnAddRow_Usr.Enabled = False          '＋ユーザーボタン
                .PropBtnRemoveRow_Relation.Enabled = False  '－ボタン

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードで関係情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRelationForRireki(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401

                'CIオーナー名テキストボックス
                .PropTxtCIOwnerNM.ReadOnly = True

                '検索ボタン
                .PropBtnSearchGrp.Enabled = False

                '関係者情報一覧
                .PropBtnAddRow_Grp.Enabled = False          '＋グループボタン
                .PropBtnAddRow_Usr.Enabled = False          '＋ユーザーボタン
                .PropBtnRemoveRow_Relation.Enabled = False  '－ボタン

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集／参照モード】関係者情報一覧初期化処理（データ設定後）
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>データ設定後の関係者情報一覧のセルのプロパティ設定を行う
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitVwRelation(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401.PropVwRelation.Sheets(0)

                '1件以上データおよび列がある場合のみ処理
                If .RowCount > 0 And .ColumnCount > 0 Then

                    '一覧のデータ件数分繰り返し、セルの背景色を設定する
                    For i As Integer = 0 To .RowCount - 1

                        '区分によってセルの背景色を設定
                        If .Cells(i, COL_RELATION_KBN).Value.ToString() = KBN_GROUP Then
                            'グループの場合、ユーザーセルの背景色を濃灰色、グループセルを白色にする
                            .Cells(i, COL_RELATION_USERNM).BackColor = PropCellBackColorDARKGRAY
                            .Cells(i, COL_RELATION_GROUPNM).BackColor = Color.White
                        ElseIf .Cells(i, COL_RELATION_KBN).Value.ToString() = KBN_USER Then
                            'ユーザーの場合、ユーザーセルの背景色を白色にし、グループセルの背景色を濃灰色にする
                            .Cells(i, COL_RELATION_USERNM).BackColor = Color.White
                            .Cells(i, COL_RELATION_GROUPNM).BackColor = PropCellBackColorDARKGRAY
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
    ''' 【共通】マスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>マスタデータを取得する
    ''' <para>作成情報：2012/06/14 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMastaData(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'CI種別マスタ取得
            If commonLogicHBK.GetCIKindMastaData(Adapter, Cn, CI_TYPE_SYSTEM, dataHBKB0401.PropDtCIKindMasta) = False Then
                Return False
            End If

            '種別マスタ取得
            '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 START
            'If commonLogicHBK.GetKindMastaData(Adapter, Cn, CI_TYPE_SYSTEM, dataHBKB0401.PropDtKindMasta) = False Then
            '    Return False
            'End If
            If commonLogicHBK.GetKindMastaData(Adapter, Cn, CI_TYPE_SYSTEM, dataHBKB0401.PropDtKindMasta, dataHBKB0401.PropIntCINmb) = False Then
                Return False
            End If
            '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 END

            'CIステータスマスタ取得
            If commonLogicHBK.GetCIStatusMastaData(Adapter, Cn, CI_TYPE_SYSTEM, dataHBKB0401.PropDtCIStatusMasta) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainData(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '取得しない


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用データ取得
                        If GetMainDataForEdit(Adapter, Cn, dataHBKB0401) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用データ取得　※編集モードと同じ
                        If GetMainDataForEdit(Adapter, Cn, dataHBKB0401) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用データ取得
                    If GetMainDataForRireki(Adapter, Cn, dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集／参照モードで初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/06/14 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainDataForEdit(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'CI共通情報データ取得
            If GetCIInfo(Adapter, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'ノウハウURLデータ取得
            If GetKnowHowUrl(Adapter, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'サーバー管理データ取得
            If GetMngSrv(Adapter, Cn, dataHBKB0401) = False Then
                Return False
            End If

            '関係者データ取得
            If GetRelation(Adapter, Cn, dataHBKB0401) = False Then
                Return False
            End If

            '登録理由履歴データ取得
            If GetRegReason(Adapter, Cn, dataHBKB0401) = False Then
                Return False
            End If

            '原因リンク履歴データ取得
            If GetCauseLink(Adapter, Cn, dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報データを取得する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetCIInfo(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtCIInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKB0401.SetSelectCIInfoSql(Adapter, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtCIInfo)

            'データが取得できなかった場合、エラー
            If dtCIInfo.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & B0401_E001, TBNM_CI_INFO_TB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKB0401.PropDtCIInfo = dtCIInfo


            '終了ログ出力
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
    ''' 【編集／参照モード】ノウハウURLデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ノウハウURLデータを取得する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetKnowhowURL(ByVal Adapter As NpgsqlDataAdapter, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'データ初期化
            dataHBKB0401.PropDtKnowHowUrl.Clear()

            '取得用SQLの作成・設定
            If sqlHBKB0401.SetSelectKnowHowUrlSql(Adapter, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ノウハウURLデータ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0401.PropDtKnowHowUrl)


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集／参照モード】サーバー管理データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サーバー管理データを取得する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMngSrv(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'データ初期化
            dataHBKB0401.PropDtMngSrv.Clear()

            '取得用SQLの作成・設定
            If sqlHBKB0401.SetSelectMngSrvSql(Adapter, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サーバー管理履歴情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0401.PropDtMngSrv)

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集／参照モード】関係者データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者データを取得する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetRelation(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'データ初期化
            dataHBKB0401.PropDtRelation.Clear()

            '取得用SQLの作成・設定
            If sqlHBKB0401.SetSelectRelationSql(Adapter, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "関係者履歴情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0401.PropDtRelation)


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集／参照モード】原因リンク履歴データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>原因リンク履歴データを取得する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetCauseLink(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'データ初期化
            dataHBKB0401.PropDtMyCauseLink.Clear()

            '取得用SQLの作成・設定
            If sqlHBKB0401.SetSelectCauseLinkSql(Adapter, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "原因リンク履歴取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0401.PropDtMyCauseLink)


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録理由履歴データを取得する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetRegReason(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'データ初期化
            dataHBKB0401.PropDtRireki.Clear()

            '取得用SQLの作成・設定
            If sqlHBKB0401.SetSelectRegReasonSql(Adapter, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "登録理由履歴データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0401.PropDtRireki)

            '最大履歴番号を取得
            If dataHBKB0401.PropDtRireki.Rows.Count > 0 Then
                dataHBKB0401.PropIntRirekiNo = dataHBKB0401.PropDtRireki.Rows(0).Item("RirekiNo")
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードで初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainDataForRireki(ByVal Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'CI共通情報履歴データ取得
            If GetCIInfoR(Adapter, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'ノウハウURL履歴データ取得
            If GetKnowhowURLR(Adapter, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'サーバー管理履歴データ取得
            If GetMngSrvR(Adapter, Cn, dataHBKB0401) = False Then
                Return False
            End If

            '関係者履歴データ取得
            If GetRelationR(Adapter, Cn, dataHBKB0401) = False Then
                Return False
            End If

            '登録理由履歴データ取得（履歴モード）
            If GetRegReasonR(Adapter, Cn, dataHBKB0401) = False Then
                Return False
            End If

            '原因リンク履歴データ取得（履歴モード）
            If GetCauseLinkR(Adapter, Cn, dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報データを取得する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetCIInfoR(ByVal Adapter As NpgsqlDataAdapter, _
                                ByVal Cn As NpgsqlConnection, _
                                ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtCIInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKB0401.SetSelectCIInfoRSql(Adapter, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報履歴データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtCIInfo)

            'データが取得できなかった場合、エラー
            If dtCIInfo.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & B0401_E001, TBNM_CI_INFO_RTB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKB0401.PropDtCIInfo = dtCIInfo

            '終了ログ出力
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
    ''' 【履歴モード】ノウハウURL履歴データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ノウハウURL履歴データを取得する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetKnowhowURLR(ByVal Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'データ初期化
            dataHBKB0401.PropDtKnowHowUrl.Clear()

            '取得用SQLの作成・設定
            If sqlHBKB0401.SetSelectKnowHowUrlRSql(Adapter, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ノウハウURL履歴データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0401.PropDtKnowHowUrl)


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【履歴モード】サーバー管理履歴データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サーバー管理履歴データを取得する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMngSrvR(ByVal Adapter As NpgsqlDataAdapter, _
                                ByVal Cn As NpgsqlConnection, _
                                ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'データ初期化
            dataHBKB0401.PropDtMngSrv.Clear()

            '取得用SQLの作成・設定
            If sqlHBKB0401.SetSelectMngSrvRSql(Adapter, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サーバー管理履歴データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0401.PropDtMngSrv)


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【履歴モード】関係者履歴データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者履歴データを取得する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetRelationR(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'データ初期化
            dataHBKB0401.PropDtRelation.Clear()

            '取得用SQLの作成・設定
            If sqlHBKB0401.SetSelectRelationRSql(Adapter, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "関係者履歴データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0401.PropDtRelation)


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【履歴モード】原因リンク履歴データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>原因リンク履歴データを取得する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetCauseLinkR(ByVal Adapter As NpgsqlDataAdapter, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'データ初期化
            dataHBKB0401.PropDtMyCauseLink.Clear()

            '取得用SQLの作成・設定
            If sqlHBKB0401.SetSelectCauseLinkRSql(Adapter, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "原因リンク履歴取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0401.PropDtMyCauseLink)


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録理由履歴データを取得する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetRegReasonR(ByVal Adapter As NpgsqlDataAdapter, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'データ初期化
            dataHBKB0401.PropDtRireki.Clear()

            '取得用SQLの作成・設定
            If sqlHBKB0401.SetSelectRegReasonRSql(Adapter, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "登録理由履歴データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0401.PropDtRireki)


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームコントロールに初期表示データを設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitDataToControl(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'ログイン／ロックデータ設定
            If SetDataToLoginAndLock(dataHBKB0401) = False Then
                Return False
            End If

            'ヘッダデータ設定
            If SetDataToHeader(dataHBKB0401) = False Then
                Return False
            End If

            'フッタデータ設定
            If SetDataToFooter(dataHBKB0401) = False Then
                Return False
            End If

            'タブコントロールデータ設定
            If SetDataToTabControl(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLock(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0401

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToLoginAndLockForNew(dataHBKB0401) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetDataToLoginAndLockForEdit(dataHBKB0401) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetDataToLoginAndLockForRef(dataHBKB0401) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetDataToLoginAndLockForRireki(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForNew(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0401.PropGrpLoginUser

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForEdit(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0401.PropGrpLoginUser

                'ロック情報が取得できた場合
                If dataHBKB0401.PropDtCILock.Rows.Count > 0 Then
                    'ロック開始日時
                    Dim dtmLockTime As DateTime = Nothing  '編集開始日時
                    If dataHBKB0401.PropDtCILock.Rows(0).Item("EdiTime").ToString() <> "" Then
                        dtmLockTime = dataHBKB0401.PropDtCILock.Rows(0).Item("EdiTime")
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForRef(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0401.PropGrpLoginUser

                'ロック情報が取得できた場合
                If dataHBKB0401.PropDtCILock.Rows.Count > 0 Then
                    'ロック開始日時
                    Dim dtmLockTime As DateTime = Nothing
                    If dataHBKB0401.PropDtCILock.Rows(0).Item("EdiTime").ToString() <> "" Then
                        dtmLockTime = dataHBKB0401.PropDtCILock.Rows(0).Item("EdiTime")
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForRireki(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0401.PropGrpLoginUser

                'ロック開始日時 ※空文字のときはNothingを指定しないと落ちる
                Dim strLockTime As String = dataHBKB0401.PropStrEdiTime
                If dataHBKB0401.PropDtCILock IsNot Nothing AndAlso dataHBKB0401.PropDtCILock.Rows.Count > 0 Then
                    .PropLockDate = dataHBKB0401.PropDtCILock.Rows(0).Item("EdiTime")
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ヘッダデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToHeader(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0401

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToHeaderForNew(dataHBKB0401) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetDataToHeaderForEdit(dataHBKB0401) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetDataToHeaderForRef(dataHBKB0401) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetDataToHeaderForRireki(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでヘッダデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToHeaderForNew(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0401

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでヘッダデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToHeaderForEdit(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0401

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでヘッダデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToHeaderForRef(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '編集モードヘッダデータ設定処理と同じ
            If SetDataToHeaderForEdit(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードでヘッダデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToHeaderForRireki(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0401

                'CI番号ラベル
                .PropLblCINmb.Text = .PropIntCINmb.ToString()

                'CI種別名ラベル
                If .PropDtCIKindMasta.Rows.Count > 0 Then
                    .PropLblCIKbnNM.Text = .PropDtCIKindMasta.Rows(0).Item("CIKbnNM")
                End If

                '履歴番号値ラベル
                .PropLblValueRirekiNo.Text = .PropIntRirekiNo.ToString()

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フッタデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToFooter(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0401

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード
                    If SetDataToFooterForNew(dataHBKB0401) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetDataToFooterForEdit(dataHBKB0401) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetDataToFooterForRef(dataHBKB0401) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetDataToFooterForRireki(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでフッタデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToFooterForNew(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0401

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでフッタデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToFooterForEdit(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0401

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでフッタデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToFooterForRef(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '※編集モードフッタデータ設定処理
            If SetDataToFooterForEdit(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードでフッタデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToFooterForRireki(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '※編集モードフッタデータ設定処理
            If SetDataToFooterForEdit(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>タブコントロールデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabControl(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '基本情報タブデータ設定
            If SetDataToTabKhn(dataHBKB0401) = False Then
                Return False
            End If

            'フリー入力タブデータ設定
            If SetDataToTabFree(dataHBKB0401) = False Then
                Return False
            End If

            '関係情報タブデータ設定
            If SetDataToTabRelation(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhn(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0401

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToTabKhnForNew(dataHBKB0401) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetDataToTabKhnForEdit(dataHBKB0401) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetDataToTabKhnForRef(dataHBKB0401) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetDataToTabKhnForRireki(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードで基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhnForNew(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'コンボボックス作成
            If CreateCmb(dataHBKB0401) = False Then
                Return False
            End If

            With dataHBKB0401

                '番号テキストボックス
                .PropTxtCINmb.Text = ""

                '分類１～２テキストボックス
                .PropTxtClass1.Text = ""
                .PropTxtClass2.Text = ""

                '名称テキストボックス
                .PropTxtCINM.Text = ""

                'ステータスコンボボックス
                .PropCmbCIStatus.SelectedValue = ""

                '情報共有先テキストボックス
                .PropTxtInfShareteamNM.Text = ""

                '説明テキストボックス
                .PropTxtCINaiyo.Text = ""

                'ノウハウURL一覧
                .PropVwKnowHowUrl.Sheets(0).DataSource = .PropDtKnowHowUrl

                'サーバー管理情報一覧
                .PropVwSrvMng.Sheets(0).DataSource = .PropDtMngSrv

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhnForEdit(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'コンボボックス作成
            If CreateCmb(dataHBKB0401) = False Then
                Return False
            End If

            With dataHBKB0401

                '番号テキストボックス
                .PropTxtCINmb.Text = dataHBKB0401.PropIntCINmb.ToString()

                '分類１～２テキストボックス
                .PropTxtClass1.Text = .PropDtCIInfo.Rows(0).Item("Class1")
                .PropTxtClass2.Text = .PropDtCIInfo.Rows(0).Item("Class2")

                '名称テキストボックス
                .PropTxtCINM.Text = .PropDtCIInfo.Rows(0).Item("CINM")

                'ステータスコンボボックス
                .PropCmbCIStatus.SelectedValue = .PropDtCIInfo.Rows(0).Item("CIStatusCD")

                '情報共有先テキストボックス
                .PropTxtInfShareteamNM.Text = .PropDtCIInfo.Rows(0).Item("InfShareteamNM")

                '説明テキストボックス
                .PropTxtCINaiyo.Text = .PropDtCIInfo.Rows(0).Item("CINaiyo")

                'ノウハウURL一覧
                .PropVwKnowHowUrl.Sheets(0).DataSource = .PropDtKnowHowUrl

                'サーバー管理情報一覧
                .PropVwSrvMng.Sheets(0).DataSource = .PropDtMngSrv

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhnForRef(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '編集モード基本情報タブデータ設定処理と同じ
            If SetDataToTabKhnForEdit(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードで基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhnForRireki(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'コンボボックス作成
            If CreateCmb(dataHBKB0401) = False Then
                Return False
            End If

            With dataHBKB0401

                '番号テキストボックス
                .PropTxtCINmb.Text = dataHBKB0401.PropIntCINmb.ToString()

                '分類１～２テキストボックス
                .PropTxtClass1.Text = .PropDtCIInfo.Rows(0).Item("Class1")
                .PropTxtClass2.Text = .PropDtCIInfo.Rows(0).Item("Class2")

                '名称テキストボックス
                .PropTxtCINM.Text = .PropDtCIInfo.Rows(0).Item("CINM")

                'ステータスコンボボックス
                .PropCmbCIStatus.SelectedValue = .PropDtCIInfo.Rows(0).Item("CIStatusCD")

                '情報共有先テキストボックス
                .PropTxtInfShareteamNM.Text = .PropDtCIInfo.Rows(0).Item("InfShareteamNM")

                '説明テキストボックス
                .PropTxtCINaiyo.Text = .PropDtCIInfo.Rows(0).Item("CINaiyo")

                'ノウハウURL一覧
                .PropVwKnowHowUrl.Sheets(0).DataSource = .PropDtKnowHowUrl
                '非活性化
                If commonLogicHBK.SetSpreadUnabled(.PropVwKnowHowUrl, 0) = False Then
                    Return False
                End If

                'サーバー管理情報一覧
                .PropVwSrvMng.Sheets(0).DataSource = .PropDtMngSrv
                '非活性化
                If commonLogicHBK.SetSpreadUnabled(.PropVwSrvMng, 0) = False Then
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
    ''' 【共通】フリー入力タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFree(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0401

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToTabFreeForNew(dataHBKB0401) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetDataToTabFreeForEdit(dataHBKB0401) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetDataToTabFreeForRef(dataHBKB0401) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetDataToTabFreeForRireki(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFreeForNew(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0401

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFreeForEdit(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0401

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFreeForRef(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '編集モードフリー入力タブデータ設定処理と同じ
            If SetDataToTabFreeForEdit(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードでフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFreeForRireki(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '編集モードフリー入力タブデータ設定処理と同じ
            If SetDataToTabFreeForEdit(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて関係情報タブデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabRelation(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0401

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToTabRelationForNew(dataHBKB0401) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetDataToTabRelationForEdit(dataHBKB0401) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetDataToTabRelationForRef(dataHBKB0401) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetDataToTabRelationForRireki(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードで関係情報タブデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabRelationForNew(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0401

                'CIオーナー名テキストボックス
                .PropTxtCIOwnerNM.Text = ""

                'CIオーナーCDラベル
                .PropLblCIOwnerCD.Text = ""

                '関係者情報一覧
                .PropVwRelation.Sheets(0).DataSource = .PropDtRelation

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで関係情報タブデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabRelationForEdit(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0401

                'CIオーナー名テキストボックス
                .PropTxtCIOwnerNM.Text = .PropDtCIInfo.Rows(0).Item("GroupNM").ToString()

                'CIオーナーCDラベル
                .PropLblCIOwnerCD.Text = .PropDtCIInfo.Rows(0).Item("CIOwnerCD")

                '関係者情報一覧
                .PropVwRelation.Sheets(0).DataSource = .PropDtRelation
                If SetInitVwRelation(dataHBKB0401) = False Then
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
    ''' 【参照モード】関係情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで関係情報タブデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabRelationForRef(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '編集モード関係情報タブデータ設定処理と同じ
            If SetDataToTabRelationForEdit(dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで関係情報タブデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabRelationForRireki(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0401

                'CIオーナー名テキストボックス
                .PropTxtCIOwnerNM.Text = .PropDtCIInfo.Rows(0).Item("GroupNM").ToString()

                'CIオーナーCDラベル
                .PropLblCIOwnerCD.Text = .PropDtCIInfo.Rows(0).Item("CIOwnerCD")

                '関係者情報一覧
                .PropVwRelation.Sheets(0).DataSource = .PropDtRelation
                If SetInitVwRelation(dataHBKB0401) = False Then
                    Return False
                End If
                '非活性
                If commonLogicHBK.SetSpreadUnabled(.PropVwRelation, 0) = False Then
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
    ''' 【共通】コンボボックス作成処理
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のコンボボックスを作成する
    ''' <para>作成情報：2012/06/14 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateCmb(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401

                '種別コンボボックス作成
                If commonLogic.SetCmbBox(.PropDtKindMasta, .PropCmbKind, False) = False Then
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
    ''' 【共通】スプレッド初期設定処理
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>各スプレッド（一覧）を初期設定する
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetVwControl(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0401

                'ノウハウURL一覧
                With .PropVwKnowHowUrl.Sheets(0)
                    .Columns(COL_KNOWHOWURL_URL).DataField = "Url"
                    .Columns(COL_KNOWHOWURL_NAIYO).DataField = "UrlNaiyo"
                End With

                'サーバー管理情報一覧
                With .PropVwSrvMng.Sheets(0)
                    .Columns(COL_MNGSRV_NO).DataField = "ManageNmb"
                    .Columns(COL_MNGSRV_NAIYO).DataField = "ManageNmbNaiyo"
                End With

                '関係者情報一覧
                With .PropVwRelation.Sheets(0)
                    .Columns(COL_RELATION_KBN).DataField = "RelationKbn"
                    .Columns(COL_RELATION_ID).DataField = "RelationID"
                    .Columns(COL_RELATION_GROUPNM).DataField = "GroupNM"
                    .Columns(COL_RELATION_USERNM).DataField = "HBKUsrNM"
                    '[Mod] 2012/08/02 y.ikushima 関係者情報DB定義変更START
                    '.Columns(COL_RELATION_GROUPID).DataField = "RelationGrpCD"
                    '[Mod] 2012/08/02 y.ikushima 関係者情報DB定義変更END
                End With

                '原因リンク一覧
                With .PropVwMngNmb.Sheets(0)
                    .Columns(COL_CAUSELINK_KBN_NMR).DataField = "ProcessKbnNMR"
                    .Columns(COL_CAUSELINK_NO).DataField = "MngNmb"
                    .Columns(COL_CAUSELINK_KBN).DataField = "ProcessKbn"
                    .Columns(COL_CAUSELINK_KBN).Visible = False
                End With

                '履歴情報一覧
                With .PropVwRegReason.Sheets(0)
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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システムコード保持用の隠しラベルを非表示にする
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetHiddenLabelUnvisible(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0401

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
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>他のユーザによりロックが解除された場合に編集中の入力内容をログに出力する
    ''' <para>作成情報：2012/06/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function OutputUnlockLog(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '定数宣言
        Const HEADER_URL As String = "URL"
        Const HEADER_URL_NAIYO As String = "説明"
        Const HEADER_SRV_NO As String = "サーバー管理番号"
        Const HEADER_SRV_NAIYO As String = "説明"

        '変数宣言
        Dim strPlmList As New List(Of String)       'フォーマット埋込用パラメータ配列
        Dim strLogFilePath As String = ""           'ログファイルパス
        Dim strLogFileName As String                'ログファイル名
        Dim strOutputDir As String                  'ログ出力フォルダ
        Dim strText_KnowhowUrl As String = ""       'ノウハウURLパラメータ文
        Dim strText_MngSrv As String = ""           'サーバー管理情報パラメータ文
        Dim strText_Relation As String = ""         '関係者情報パラメータ文
        Dim sw As StreamWriter = Nothing            'ファイル書込用クラス

        Try
            With dataHBKB0401

                '書込用テキスト作成
                strPlmList.Add(.PropLblCINmb.Text)            'CI番号
                strPlmList.Add(.PropLblCIKbnNM.Text)          'CI種別
                strPlmList.Add(.PropCmbKind.Text)             '種別
                strPlmList.Add(.PropTxtCINmb.Text)            '番号
                strPlmList.Add(.PropTxtClass1.Text)           '分類1
                strPlmList.Add(.PropTxtClass2.Text)           '分類2
                strPlmList.Add(.PropTxtCINM.Text)             '名称
                strPlmList.Add(.PropCmbCIStatus.Text)         'ステータス
                strPlmList.Add(.PropTxtInfShareteamNM.Text)   '情報共有先
                strPlmList.Add(.PropTxtCINaiyo.Text)          '説明

                'ノウハウURL
                If .PropVwKnowHowUrl.Sheets(0).RowCount > 0 Then
                    With .PropVwKnowHowUrl.Sheets(0)
                        '一覧行数分繰り返し、パラメータ文を作成
                        For i As Integer = 0 To .RowCount - 1
                            '以下の書式で出力
                            'URLn:<URL>
                            '説明n:<説明>
                            Dim strUrl As String = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_KNOWHOWURL_URL), "")
                            Dim strNaiyo As String = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_KNOWHOWURL_NAIYO), "")
                            strText_KnowhowUrl &= HEADER_URL & (i + 1).ToString() & ":" & strUrl & vbCrLf
                            strText_KnowhowUrl &= HEADER_URL_NAIYO & (i + 1).ToString() & ":" & strNaiyo
                            '最終行以外は改行コード追加
                            If i < .RowCount - 1 Then
                                strText_KnowhowUrl &= vbCrLf
                            End If
                        Next
                    End With
                End If
                strPlmList.Add(strText_KnowhowUrl)

                'サーバー管理情報
                If .PropVwSrvMng.Sheets(0).RowCount > 0 Then
                    With .PropVwSrvMng.Sheets(0)
                        '一覧行数分繰り返し、パラメータ文を作成
                        For i As Integer = 0 To .RowCount - 1
                            '以下の書式で出力
                            'サーバー管理番号n:<サーバー管理番号>
                            '説明n:<説明>
                            Dim strNo As String = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_MNGSRV_NO), "")
                            Dim strNaiyo As String = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_MNGSRV_NAIYO), "")
                            strText_MngSrv &= HEADER_SRV_NO & (i + 1).ToString() & ":" & strNo & vbCrLf
                            strText_MngSrv &= HEADER_SRV_NAIYO & (i + 1).ToString() & ":" & strNaiyo
                            '最終行以外は改行コード追加
                            If i < .RowCount - 1 Then
                                strText_MngSrv &= vbCrLf
                            End If
                        Next
                    End With
                End If
                strPlmList.Add(strText_MngSrv)

                strPlmList.Add(.PropTxtBIko1.Text)            'フリーテキスト１
                strPlmList.Add(.PropTxtBIko2.Text)            'フリーテキスト２
                strPlmList.Add(.PropTxtBIko3.Text)            'フリーテキスト３
                strPlmList.Add(.PropTxtBIko4.Text)            'フリーテキスト４
                strPlmList.Add(.PropTxtBIko5.Text)            'フリーテキスト５

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

                strPlmList.Add(.PropTxtCIOwnerNM.Text)        'CIオーナー名

                '関係者情報
                If .PropVwRelation.Sheets(0).RowCount > 0 Then
                    With .PropVwRelation.Sheets(0)
                        '一覧行数分繰り返し、パラメータ文を作成
                        For i As Integer = 0 To .RowCount - 1
                            '以下の書式で出力
                            'n:<区分> <ID> <グループ名またはユーザー名>
                            Dim strKbn As String = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_RELATION_KBN), "")
                            Dim strID As String = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_RELATION_ID), "")
                            Dim strNM As String = ""
                            If strKbn = KBN_GROUP Then
                                strNM = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_RELATION_GROUPNM), "")
                            ElseIf strKbn = KBN_USER Then
                                'strNM = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_RELATION_USERNM), "")
                                strNM = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_RELATION_GROUPNM), "") _
                                    & " " & commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_RELATION_USERNM), "")
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

                'ログ出力フォルダ設定
                strOutputDir = Path.Combine(Application.StartupPath, OUTPUT_DIR_UNLOCKEDLOG)

                'ログファイル名設定
                strLogFileName = Format(DateTime.Parse(.PropDtCILock.Rows(0).Item("SysTime")), "yyyyMMddHHmmss") & ".log"

                'ファイル出力を実行し、出力ファイルパスを取得
                If commonLogicHBK.OutputLogFromTextFormat(strPlmList, strLogFileName, _
                                                          FORMAT_DIR_UNLOCKEDLOG, _
                                                          FILE_UNLOCKLOG_SYSTEM, _
                                                          strOutputDir, _
                                                          strLogFilePath) = False Then
                    Return False
                End If

                'データクラスにメッセージをセット
                dataHBKB0401.PropStrBeUnlockedMsg = String.Format(HBK_W001, strLogFilePath)

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
    ''' 【共通】ノウハウURL空行追加処理
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ノウハウURLに空行を1行追加する
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function AddRowKnowHowUrl(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401.PropVwKnowHowUrl.Sheets(0)

                '最終行に空行を1行追加
                .Rows.Add(.RowCount, 1)
                '追加行にフォーカスをセット
                If commonLogicHBK.SetFocusOnVwRow(dataHBKB0401.PropVwKnowHowUrl, 0, .RowCount, 0, 1, .ColumnCount) = False Then
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
    ''' 【共通】ノウハウURL選択行削除処理
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ノウハウURLの選択行を削除（Remove）する
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowKnowHowUrl(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号

        Try
            With dataHBKB0401.PropVwKnowHowUrl.Sheets(0)

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
    ''' 【共通】サーバー管理情報空行追加処理
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サーバー管理情報に空行を1行追加する
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function AddRowMngSrv(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401.PropVwSrvMng.Sheets(0)

                '最終行に空行を1行追加
                .Rows.Add(.RowCount, 1)
                '追加行にフォーカスをセット
                If commonLogicHBK.SetFocusOnVwRow(dataHBKB0401.PropVwSrvMng, 0, .RowCount, 0, 1, .ColumnCount) = False Then
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
    ''' 【共通】サーバー管理情報選択行削除処理
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サーバー管理情報の選択行を削除（Remove）する
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowMngSrv(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号

        Try
            With dataHBKB0401.PropVwSrvMng.Sheets(0)

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
    ''' 【共通】関係者情報グループ設定処理
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者情報一覧にサブ検索で選択されたグループを設定する
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetGroupToVwRelation(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intNewRowNo As Integer          '新規追加行番号
        Dim blnAddFlg As Boolean = True     '追加フラグ

        Try
            With dataHBKB0401

                'サブ検索画面で1件以上選択された場合に値を設定
                If .PropDtResultSub IsNot Nothing Then

                    '選択データ数分繰り返し、関係者情報一覧に行を追加する
                    For i As Integer = 0 To .PropDtResultSub.Rows.Count - 1

                        '追加フラグ初期化
                        blnAddFlg = True

                        'グループが既に設定済でない場合のみ追加
                        For j As Integer = 0 To .PropVwRelation.Sheets(0).RowCount - 1

                            '既に設定済みの場合は追加フラグをOFFにして処理を抜ける
                            If .PropDtResultSub.Rows(i).Item("グループCD") = _
                                .PropVwRelation.Sheets(0).Cells(j, COL_RELATION_ID).Value Then
                                blnAddFlg = False
                                Exit For
                            End If

                        Next

                        '追加フラグがONの場合のみ追加処理を行う
                        If blnAddFlg = True Then

                            '追加行番号取得
                            intNewRowNo = .PropVwRelation.Sheets(0).Rows.Count

                            '新規行追加
                            .PropVwRelation.Sheets(0).Rows.Add(intNewRowNo, 1)

                            'サブ検索画面での選択値を設定
                            .PropVwRelation.Sheets(0).Cells(intNewRowNo, COL_RELATION_KBN).Value = KBN_GROUP      '区分：グループ
                            .PropVwRelation.Sheets(0).Cells(intNewRowNo, COL_RELATION_ID).Value = _
                                .PropDtResultSub.Rows(i).Item("グループCD")                                       'ID
                            .PropVwRelation.Sheets(0).Cells(intNewRowNo, COL_RELATION_GROUPNM).Value = _
                                .PropDtResultSub.Rows(i).Item("グループ名")                                       'グループ名

                            'ユーザ名の背景色を濃灰色にする
                            .PropVwRelation.Sheets(0).Cells(intNewRowNo, COL_RELATION_USERNM).BackColor = PropCellBackColorDARKGRAY

                        End If

                    Next

                    '最終追加行にフォーカスをセット
                    If commonLogicHBK.SetFocusOnVwRow(.PropVwRelation, _
                                                      0, .PropVwRelation.Sheets(0).RowCount, 0, _
                                                      1, .PropVwRelation.Sheets(0).ColumnCount) = False Then
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
    ''' 【共通】関係者情報ユーザー設定処理
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者情報一覧にサブ検索で選択されたユーザーを設定する
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetUserToVwRelation(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intNewRowNo As Integer          '新規追加行番号
        Dim blnAddFlg As Boolean = True     '追加フラグ


        Try
            With dataHBKB0401

                'サブ検索画面で1件以上選択された場合に値を設定
                If .PropDtResultSub IsNot Nothing Then

                    '選択データ数分繰り返し、関係者情報一覧に行を追加する
                    For i As Integer = 0 To .PropDtResultSub.Rows.Count - 1

                        '追加フラグ初期化
                        blnAddFlg = True

                        'ユーザーが既に設定済でない場合のみ追加
                        For j As Integer = 0 To .PropVwRelation.Sheets(0).RowCount - 1

                            '既に設定済みの場合は追加フラグをOFFにして処理を抜ける
                            If .PropDtResultSub.Rows(i).Item("ユーザーID") = _
                                .PropVwRelation.Sheets(0).Cells(j, COL_RELATION_ID).Value Then _
                                '[Mod] 2012/08/02 y.ikushima 関係者情報DB定義変更START
                                'And .PropDtResultSub.Rows(i).Item("グループID") = _
                                '.PropVwRelation.Sheets(0).Cells(j, COL_RELATION_GROUPID).Value Then
                                '[Mod] 2012/08/02 y.ikushima 関係者情報DB定義変更END
                                blnAddFlg = False
                                Exit For
                            End If

                        Next

                        '追加フラグがONの場合のみ追加処理を行う
                        If blnAddFlg = True Then

                            '追加行番号取得
                            intNewRowNo = .PropVwRelation.Sheets(0).Rows.Count

                            '新規行追加
                            .PropVwRelation.Sheets(0).Rows.Add(intNewRowNo, 1)

                            'サブ検索画面での選択値を設定
                            .PropVwRelation.Sheets(0).Cells(intNewRowNo, COL_RELATION_KBN).Value = KBN_USER       '区分：ユーザー
                            .PropVwRelation.Sheets(0).Cells(intNewRowNo, COL_RELATION_ID).Value = _
                                .PropDtResultSub.Rows(i).Item("ユーザーID")                                       'ID
                            '.PropVwRelation.Sheets(0).Cells(intNewRowNo, COL_RELATION_GROUPNM).Value = _
                            '    .PropDtResultSub.Rows(i).Item("グループ名")                                       'グループ名
                            .PropVwRelation.Sheets(0).Cells(intNewRowNo, COL_RELATION_USERNM).Value = _
                                .PropDtResultSub.Rows(i).Item("ユーザー氏名")                                     'ユーザー名
                            '[Mod] 2012/08/02 y.ikushima 関係者情報DB定義変更START
                            '.PropVwRelation.Sheets(0).Cells(intNewRowNo, COL_RELATION_GROUPID).Value = _
                            '    .PropDtResultSub.Rows(i).Item("グループID")                                     'グループID
                            '[Mod] 2012/08/02 y.ikushima 関係者情報DB定義変更END

                            'ユーザ名の背景色を濃灰色にする
                            .PropVwRelation.Sheets(0).Cells(intNewRowNo, COL_RELATION_GROUPNM).BackColor = PropCellBackColorDARKGRAY
                        End If

                    Next

                    '最終追加行にフォーカスをセット
                    If commonLogicHBK.SetFocusOnVwRow(.PropVwRelation, _
                                                      0, .PropVwRelation.Sheets(0).RowCount, 0, _
                                                      1, .PropVwRelation.Sheets(0).ColumnCount) = False Then
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
    ''' 【共通】関係者情報選択行削除処理
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者情報の選択行を削除する
    ''' <para>作成情報：2012/06/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowRelation(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号

        Try
            With dataHBKB0401.PropVwRelation.Sheets(0)

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
    ''' 【編集モード】ロック解除チェック処理
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータのロックが解除されていないかチェックする
    ''' <para>作成情報：2012/06/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckUnlock(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnBeUnocked As Boolean = False       'ロックフラグ

        Try

            With dataHBKB0401

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
    ''' 【共通】入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録項目の入力チェックを行う
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckInputValue(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0401

                '分類１
                With .PropTxtClass1
                    '未入力の場合、エラー
                    If .Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = B0401_E002
                        'タブを基本情報タブに設定
                        dataHBKB0401.PropTbInput.SelectedIndex = TAB_KHN
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
                        puErrMsg = B0401_E003
                        'タブを基本情報タブに設定
                        dataHBKB0401.PropTbInput.SelectedIndex = TAB_KHN
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
                        puErrMsg = B0401_E004
                        'タブを基本情報タブに設定
                        dataHBKB0401.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With

                'キー項目重複チェック
                If CheckIsSameKeyValue(dataHBKB0401) = False Then
                    Return False
                End If

                'ステータス
                With .PropCmbCIStatus
                    '未入力の場合、エラー
                    If .SelectedValue = "" Then
                        'エラーメッセージ設定
                        puErrMsg = B0401_E005
                        'タブを基本情報タブに設定
                        dataHBKB0401.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With

                'ノウハウURL一覧
                With .PropVwKnowHowUrl.Sheets(0)

                    '1行以上ある場合、チェックを行う
                    If .RowCount > 0 Then

                        '一覧の行数分繰り返し
                        For i As Integer = 0 To .RowCount - 1

                            '変数宣言
                            Dim strUrl As String = ""       'URL
                            Dim strNaiyo As String = ""     '内容

                            '各値を取得
                            strUrl = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_KNOWHOWURL_URL), "").Trim()
                            strNaiyo = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_KNOWHOWURL_NAIYO), "").Trim()

                            '内容に入力があってURLが未入力の場合、エラー
                            If strNaiyo <> "" And strUrl = "" Then
                                'エラーメッセージ設定
                                puErrMsg = B0401_E006
                                'タブを基本情報タブに設定
                                dataHBKB0401.PropTbInput.SelectedIndex = TAB_KHN
                                'フォーカス設定
                                If commonLogicHBK.SetFocusOnVwRow(dataHBKB0401.PropVwKnowHowUrl, _
                                                                  0, i, COL_KNOWHOWURL_URL, 1, .ColumnCount) = False Then
                                    Return False
                                End If
                                'エラーを返す
                                Return False
                            End If

                            '内容に入力があってURLが未入力の場合、エラー
                            If strNaiyo = "" And strUrl <> "" Then
                                'エラーメッセージ設定
                                puErrMsg = B0401_E006
                                'タブを基本情報タブに設定
                                dataHBKB0401.PropTbInput.SelectedIndex = TAB_KHN
                                'フォーカス設定
                                If commonLogicHBK.SetFocusOnVwRow(dataHBKB0401.PropVwKnowHowUrl, _
                                                                  0, i, COL_KNOWHOWURL_NAIYO, 1, .ColumnCount) = False Then
                                    Return False
                                End If
                                'エラーを返す
                                Return False
                            End If

                            'URLが入力されている場合のみチェック
                            If strUrl <> "" Then

                                'URLが重複している場合、エラー
                                For j As Integer = 0 To .RowCount - 1

                                    If i <> j AndAlso _
                                        strUrl = commonLogicHBK.ChangeNothingToStr(.Cells(j, COL_KNOWHOWURL_URL), "").Trim() Then
                                        'エラーメッセージ設定
                                        puErrMsg = B0401_E007
                                        'タブを基本情報タブに設定
                                        dataHBKB0401.PropTbInput.SelectedIndex = TAB_KHN
                                        'フォーカス設定
                                        If commonLogicHBK.SetFocusOnVwRow(dataHBKB0401.PropVwKnowHowUrl, _
                                                                          0, j, COL_KNOWHOWURL_URL, 1, .ColumnCount) = False Then
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

                'サーバー管理情報一覧
                With .PropVwSrvMng.Sheets(0)

                    '1行以上ある場合、チェックを行う
                    If .RowCount > 0 Then

                        '一覧の行数分繰り返し
                        For i As Integer = 0 To .RowCount - 1

                            '変数宣言
                            Dim strNo As String = ""        '番号
                            Dim strNaiyo As String = ""     '内容

                            strNo = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_MNGSRV_NO), "").Trim()
                            strNaiyo = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_MNGSRV_NAIYO), "").Trim()

                            '内容に入力があって番号が未入力の場合、エラー
                            If strNaiyo <> "" And strNo = "" Then
                                'エラーメッセージ設定
                                puErrMsg = B0401_E008
                                'タブを基本情報タブに設定
                                dataHBKB0401.PropTbInput.SelectedIndex = TAB_KHN
                                'フォーカス設定
                                If commonLogicHBK.SetFocusOnVwRow(dataHBKB0401.PropVwSrvMng, _
                                                                  0, i, COL_MNGSRV_NO, 1, .ColumnCount) = False Then
                                    Return False
                                End If
                                'エラーを返す
                                Return False
                            End If

                            '番号に入力があって内容が未入力の場合、エラー
                            If strNo <> "" And strNaiyo = "" Then
                                'エラーメッセージ設定
                                puErrMsg = B0401_E008
                                'タブを基本情報タブに設定
                                dataHBKB0401.PropTbInput.SelectedIndex = TAB_KHN
                                'フォーカス設定
                                If commonLogicHBK.SetFocusOnVwRow(dataHBKB0401.PropVwSrvMng, _
                                                                  0, i, COL_MNGSRV_NAIYO, 1, .ColumnCount) = False Then
                                    Return False
                                End If
                                'エラーを返す
                                Return False
                            End If

                            '番号が入力されている場合のみチェック
                            If strNo <> "" Then

                                '番号が重複している場合、エラー
                                For j As Integer = 0 To .RowCount - 1

                                    If i <> j AndAlso _
                                        strNo = commonLogicHBK.ChangeNothingToStr(.Cells(j, COL_MNGSRV_NO), "").Trim() Then
                                        'エラーメッセージ設定
                                        puErrMsg = B0401_E009
                                        'タブを基本情報タブに設定
                                        dataHBKB0401.PropTbInput.SelectedIndex = TAB_KHN
                                        'フォーカス設定
                                        If commonLogicHBK.SetFocusOnVwRow(dataHBKB0401.PropVwSrvMng, _
                                                                          0, j, COL_MNGSRV_NO, 1, .ColumnCount) = False Then
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

                'CIオーナー
                If .PropTxtCIOwnerNM.Text.Trim <> "" And _
                    .PropLblCIOwnerCD.Text = "" Then

                    'オーナー名に入力があってコードが未入力の場合（サブ検索にて選択していない場合）、エラー
                    puErrMsg = B0401_E010
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
    ''' 【共通】キー項目重複チェック処理
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>キー項目が重複しているかチェックする
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckIsSameKeyValue(ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)    'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ
        Dim dtResult As New DataTable

        Try

            'コネクションを開く
            Cn.Open()

            '同じキー項目（分類１、分類２、名称）のデータ有無取得（SELECT）用SQLを作成
            If sqlHBKB0401.SetSelectCountSameKeySql(Adapter, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "同じキー項目（分類１、分類２、名称）のデータ有無取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)


            '重複データがある場合、エラー
            If dtResult.Rows.Count > 0 Then

                'エラーメッセージ設定
                puErrMsg = B0401_E013
                'タブを基本情報タブに設定
                dataHBKB0401.PropTbInput.SelectedIndex = TAB_KHN
                'フォーカス設定（分類１）
                With dataHBKB0401.PropTxtClass1
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
    ''' <param name="dataHBKB0401">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をDBに新規登録（INSERT）する
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertNewData(ByVal dataHBKB0401 As DataHBKB0401) As Boolean

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
            If SelectNewCINmbAndSysDate(Cn, dataHBKB0401) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'CI共通情報新規登録
            If InsertCIInfo(Cn, dataHBKB0401) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'CIシステム新規登録
            If InsertCISystem(Cn, dataHBKB0401) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'ノウハウURL新規登録
            If InsertKnowHowUrl(Cn, dataHBKB0401) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'サーバー管理情報新規登録
            If InsertMngSrv(Cn, dataHBKB0401) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '関係者情報新規登録
            If InsertRelation(Cn, dataHBKB0401) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '履歴情報新規登録（共通）
            If InsertRireki(Cn, dataHBKB0401) = False Then
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
    ''' 【編集／履歴モード】データ更新処理
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をDBに新規登録（INSERT）する
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateData(ByVal dataHBKB0401 As DataHBKB0401) As Boolean

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
            If SelectSysDate(Adapter, Cn, dataHBKB0401) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'CI共通情報更新（UPDATE）
            If UpdateCIInfo(Cn, dataHBKB0401) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'CIシステム更新（UPDATE）
            If UpdateCISystem(Cn, dataHBKB0401) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'ノウハウURL更新（DELETE→INSERT）
            If UpdateKnowHowUrl(Cn, dataHBKB0401) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'サーバー管理情報更新（DELETE→INSERT）
            If UpdateMngSrv(Cn, dataHBKB0401) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '関係者情報更新（DELETE→INSERT）
            If UpdateRelation(Cn, dataHBKB0401) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '履歴情報新規登録（共通）
            If InsertRireki(Cn, dataHBKB0401) = False Then
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
            Adapter.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】ロック解除処理
    ''' </summary>
    ''' <param name="dataHBKB0401">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータのロックを解除する
    ''' <para>作成情報：2012/06/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UnlockData(ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'CI共通情報ロック解除（DELETE）
            If commonLogicHBK.UnlockCIInfo(dataHBKB0401.PropIntCINmb) = False Then
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
    ''' <param name="dataHBKB0401">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番したCI番号を取得（SELECT）する
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectNewCINmbAndSysDate(ByVal Cn As NpgsqlConnection, _
                                              ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtResult As New DataTable

        Try
            '新規CI番号取得（SELECT）用SQLを作成
            If sqlHBKB0401.SetSelectNewCINmbAndSysDateSql(Adapter, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規CI番号、システム日付取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            'データが取得できた場合、データクラスに取得データをセット
            If dtResult.Rows.Count > 0 Then
                dataHBKB0401.PropIntCINmb = dtResult.Rows(0).Item("CINmb")      '新規CI番号
                dataHBKB0401.PropDtmSysDate = dtResult.Rows(0).Item("SysDate")  'サーバー日付
            Else
                '取得できなかったときはエラー
                puErrMsg = B0401_E011
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
    ''' <param name="dataHBKB0401">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をCI共通情報テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCIInfo(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CI共通情報新規登録（INSERT）用SQLを作成
            If sqlHBKB0401.SetInsertCIInfoSql(Cmd, Cn, dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でCI共通情報テーブルを更新（UPDATE）する
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateCIInfo(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CI共通情報更新（UPDATE）用SQLを作成
            If sqlHBKB0401.SetUpdateCIInfoSql(Cmd, Cn, dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>更新用のサーバー日付を取得する
    ''' <para>作成情報：2012/06/27 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectSysDate(ByRef Adapter As NpgsqlDataAdapter, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSysDate As New DataTable         'サーバー日付格納用テーブル

        Try

            'SQLを作成
            If sqlHBKB0401.SetSelectSysDateSql(Adapter, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サーバー日付取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtSysDate)

            'データが取得できた場合、データクラスにサーバー日付をセット
            If dtSysDate.Rows.Count > 0 Then
                dataHBKB0401.PropDtmSysDate = dtSysDate.Rows(0).Item("SysDate")
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
    ''' 【新規登録モード】CIシステム新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をCIシステムテーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCISystem(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CIシステム新規登録（INSERT）用SQLを作成
            If sqlHBKB0401.SetInsertCISystemSql(Cmd, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIシステム新規登録", Nothing, Cmd)

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
    ''' 【編集／履歴モード】CIシステム更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でCIシステムテーブルを更新（UPDATE）する
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateCISystem(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CIシステム更新（UPDATE）用SQLを作成
            If sqlHBKB0401.SetUpdateCISystemSql(Cmd, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIシステム更新", Nothing, Cmd)

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
    ''' 【新規登録／編集／履歴モード】ノウハウURL新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をノウハウURLテーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertKnowHowUrl(ByVal Cn As NpgsqlConnection, _
                                      ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            With dataHBKB0401

                'ノウハウURL一覧の行数分繰り返し、登録処理を行う
                For i As Integer = 0 To .PropVwKnowHowUrl.Sheets(0).RowCount - 1

                    '入力値取得
                    Dim strUrl As String = commonLogicHBK.ChangeNothingToStr(.PropVwKnowHowUrl.Sheets(0).Cells(i, COL_KNOWHOWURL_URL), "").Trim()
                    Dim strUrlNaiyo As String = commonLogicHBK.ChangeNothingToStr(.PropVwKnowHowUrl.Sheets(0).Cells(i, COL_KNOWHOWURL_NAIYO), "").Trim()

                    '1項目でも入力されている場合、登録を行う
                    If strUrl <> "" Or strUrlNaiyo <> "" Then

                        '登録行作成
                        Dim row As DataRow = .PropDtKnowHowUrl.NewRow
                        row.Item("Url") = strUrl
                        row.Item("UrlNaiyo") = strUrlNaiyo

                        '作成した行をデータクラスにセット
                        .PropRowReg = row

                        'ノウハウURL新規登録（INSERT）用SQLを作成
                        If sqlHBKB0401.SetInsertKnowHowUrlSql(Cmd, Cn, dataHBKB0401) = False Then
                            Return False
                        End If

                        'ログ出力
                        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ノウハウURL新規登録", Nothing, Cmd)

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
    ''' 【編集／履歴モード】ノウハウURL更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でノウハウURLテーブルを更新（DELETE→INSERT）する
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateKnowHowUrl(ByVal Cn As NpgsqlConnection, _
                                      ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'ノウハウURL削除（DELETE）
            If DeleteKnowHowUrl(Cn, dataHBKB0401) = False Then
                Return False
            End If

            'ノウハウURL登録（INSERT）
            If InsertKnowHowUrl(Cn, dataHBKB0401) = False Then
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
    ''' 【編集／履歴モード】ノウハウURL削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0401">[IN/OUT]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ノウハウURLテーブルを物理削除（DELETE）する
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DeleteKnowHowUrl(ByVal Cn As NpgsqlConnection, _
                                      ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'ノウハウURL物理削除（DELETE）用SQLを作成
            If sqlHBKB0401.SetDeleteKnowHowUrlSql(Cmd, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ノウハウURL物理削除", Nothing, Cmd)

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
    ''' 【新規登録／編集／履歴モード】サーバー管理情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をサーバー管理情報テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertMngSrv(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            With dataHBKB0401

                'サーバー管理情報一覧の行数分繰り返し、登録処理を行う
                For i As Integer = 0 To .PropVwSrvMng.Sheets(0).RowCount - 1

                    '入力値取得
                    Dim strMngNmb As String = commonLogicHBK.ChangeNothingToStr(.PropVwSrvMng.Sheets(0).Cells(i, COL_MNGSRV_NO), "").Trim()
                    Dim strManageNmbNaiyo As String = commonLogicHBK.ChangeNothingToStr(.PropVwSrvMng.Sheets(0).Cells(i, COL_MNGSRV_NAIYO), "").Trim()

                    '1項目でも入力されている場合、登録処理を行う
                    If strMngNmb <> "" Or strManageNmbNaiyo <> "" Then

                        '登録行作成
                        Dim row As DataRow = .PropDtMngSrv.NewRow
                        row.Item("ManageNmb") = strMngNmb
                        row.Item("ManageNmbNaiyo") = strManageNmbNaiyo

                        '作成した行をデータクラスにセット
                        .PropRowReg = row

                        'サーバー管理情報新規登録（INSERT）用SQLを作成
                        If sqlHBKB0401.SetInsertMngSrvSql(Cmd, Cn, dataHBKB0401) = False Then
                            Return False
                        End If


                        'ログ出力
                        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サーバー管理情報新規登録", Nothing, Cmd)

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
    ''' 【編集／履歴モード】サーバー管理情報更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でサーバー管理情報テーブルを更新（DELETE→INSERT）する
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateMngSrv(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'サーバー管理情報削除（DELETE）
            If DeleteMngSrv(Cn, dataHBKB0401) = False Then
                Return False
            End If

            'サーバー管理情報登録（INSERT）
            If InsertMngSrv(Cn, dataHBKB0401) = False Then
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
    ''' 【編集／履歴モード】サーバー管理情報削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サーバー管理情報テーブルを物理削除（DELETE）する
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DeleteMngSrv(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'サーバー管理情報物理削除（DELETE）用SQLを作成
            If sqlHBKB0401.SetDeleteMngSrvSql(Cmd, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サーバー管理情報物理削除", Nothing, Cmd)

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
    ''' 【新規登録／編集／履歴モード】関係者情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容を関係者情報テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRelation(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            With dataHBKB0401

                '関係者情報一覧の行数分繰り返し、登録処理を行う
                For i As Integer = 0 To .PropVwRelation.Sheets(0).RowCount - 1

                    '登録行作成
                    Dim row As DataRow = .PropDtRelation.NewRow
                    row.Item("RelationKbn") = .PropVwRelation.Sheets(0).Cells(i, COL_RELATION_KBN).Value
                    row.Item("RelationID") = .PropVwRelation.Sheets(0).Cells(i, COL_RELATION_ID).Value
                    '[Mod] 2012/08/02 y.ikushima 関係者情報DB定義変更START
                    'row.Item("RelationGrpCD") = .PropVwRelation.Sheets(0).Cells(i, COL_RELATION_GROUPID).Value
                    '[Mod] 2012/08/02 y.ikushima 関係者情報DB定義変更END
                    '作成した行をデータクラスにセット
                    .PropRowReg = row

                    '関係者情報新規登録（INSERT）用SQLを作成
                    If sqlHBKB0401.SetInsertRelationSql(Cmd, Cn, dataHBKB0401) = False Then
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
            ''ロールバック
            'If Tsx IsNot Nothing Then
            '    Tsx.Rollback()
            'End If
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集／履歴モード】関係者情報更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容で関係者情報テーブルを更新（DELETE→INSERT）する
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateRelation(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '関係者情報削除（DELETE）
            If DeleteRelation(Cn, dataHBKB0401) = False Then
                Return False
            End If

            '関係者情報登録（INSERT）
            If InsertRelation(Cn, dataHBKB0401) = False Then
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
    ''' 【編集／履歴モード】関係者情報削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者情報テーブルを物理削除（DELETE）する
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DeleteRelation(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            '関係者情報物理削除（DELETE）用SQLを作成
            If sqlHBKB0401.SetDeleteRelationSql(Cmd, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "関係者情報物理削除", Nothing, Cmd)

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
    ''' <param name="dataHBKB0401">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴／変更理由を各テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRireki(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter    'アダプタ
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            '新規履歴番号取得
            If GetNewRirekiNo(Adapter, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'CI共通情報履歴テーブル登録
            If InsertCIINfoR(Cn, dataHBKB0401) = False Then
                Return False
            End If

            'CIシステム履歴テーブル登録
            If InsertCISystemR(Cn, dataHBKB0401) = False Then
                Return False
            End If

            'ノウハウURL履歴テーブル登録
            If InsertKnowHowUrlR(Cn, dataHBKB0401) = False Then
                Return False
            End If

            'サーバー管理情報履歴テーブル登録
            If InsertMngSrvR(Cn, dataHBKB0401) = False Then
                Return False
            End If

            '関係者履歴テーブル登録
            If InsertRelationR(Cn, dataHBKB0401) = False Then
                Return False
            End If

            '登録理由履歴テーブル登録
            If InsertRegReasonR(Cn, dataHBKB0401) = False Then
                Return False
            End If

            '原因リンク履歴テーブル登録
            If InsertCauseLinkR(Cn, dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番した履歴番号を取得する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetNewRirekiNo(ByVal Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtRirekiNo As New DataTable         '履歴番号格納用テーブル

        Try

            'SQLを作成
            If sqlHBKB0401.SetSelectNewRirekiNoSql(Adapter, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規履歴番号取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtRirekiNo)

            'データが取得できた場合、データクラスに履歴番号をセット
            If dtRirekiNo.Rows.Count > 0 Then
                dataHBKB0401.PropIntRirekiNo = dtRirekiNo.Rows(0).Item("RirekiNo")
            Else
                '取得できなかった場合はエラー
                puErrMsg = B0401_E012
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
    ''' <param name="dataHBKB0401">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCIINfoR(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0401.SetInsertCIInfoRSql(Cmd, Cn, dataHBKB0401) = False Then
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
    ''' 【共通】CIシステム履歴テーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CIシステム履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCISystemR(ByVal Cn As NpgsqlConnection, _
                                     ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0401.SetInsertCISystemRSql(Cmd, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIシステム履歴新規登録", Nothing, Cmd)

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
    ''' 【共通】ノウハウURL履歴テーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ノウハウURL履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertKnowHowUrlR(ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0401.SetInsertKnowHowUrlRSql(Cmd, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ノウハウURL履歴新規登録", Nothing, Cmd)

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
    ''' 【共通】サーバー管理情報履歴テーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サーバー管理情報履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertMngSrvR(ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0401.SetInsertMngSrvRSql(Cmd, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サーバー管理情報履歴新規登録", Nothing, Cmd)

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
    ''' 【共通】関係者履歴テーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0401">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRelationR(ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'SQLを作成
            If sqlHBKB0401.SetInsertRelationRSql(Cmd, Cn, dataHBKB0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "関係者履歴新規登録", Nothing, Cmd)

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
    ''' <param name="dataHBKB0401">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録理由履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRegReasonR(ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0401.SetInsertRegReasonRSql(Cmd, Cn, dataHBKB0401) = False Then
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
    ''' <param name="dataHBKB0401">[IN]システム登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>原因リンク履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCauseLinkR(ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKB0401 As DataHBKB0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'データ数分繰り返し、登録を行う
            For i As Integer = 0 To dataHBKB0401.PropDtCauseLink.Rows.Count - 1

                '登録行をデータクラスにセット
                dataHBKB0401.PropRowReg = dataHBKB0401.PropDtCauseLink.Rows(i)

                'SQLを作成
                If sqlHBKB0401.SetInsertCauseLinkRSql(Cmd, Cn, dataHBKB0401) = False Then
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

End Class
