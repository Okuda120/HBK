Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.IO
Imports System.Windows.Forms
Imports FarPoint.Win.Spread

''' <summary>
''' サポセン機器登録画面ロジッククラス
''' </summary>
''' <remarks>サポセン機器登録画面のロジックを定義したクラス
''' <para>作成情報：2012/07/11 t.fukuo
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKB0601

    'インスタンス作成
    Private sqlHBKB0601 As New SqlHBKB0601
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK
    Private commonValidation As New CommonValidation

    'Public定数宣言==============================================
    '複数人利用一覧列番号
    Public Const COL_SHARE_USERID As Integer = 0            'ユーザーID
    Public Const COL_SHARE_USERNM As Integer = 1            'ユーザー氏名
    Public Const COL_SHARE_REGDT As Integer = 2             '登録日時
    Public Const COL_SHARE_REGGPCD As Integer = 3           '登録グループコード
    Public Const COL_SHARE_REGUSERID As Integer = 4         '登録ユーザID
    '★★-------------
    'オプションソフト一覧列番号
    'Public Const COL_OPTSOFT_SOFTNM As Integer = 0          'ソフト名
    'オプションソフト一覧列番号
    Public Const COL_OPTSOFT_SOFTCD As Integer = 0          'ソフトCD
    '★★-------------
    Public Const COL_OPTSOFT_REGDT As Integer = 1           '登録日時
    Public Const COL_OPTSOFT_REGGPCD As Integer = 2         '登録グループコード
    Public Const COL_OPTSOFT_REGUSERID As Integer = 3       '登録ユーザID
    'セット機器一覧列番号
    Public Const COL_SETKIKI_SETKIKINO As Integer = 0       'セット機器No
    Public Const COL_SETKIKI_ID As Integer = 1              'セット機器ID　　　　　　　※非表示
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システムエラー発生時に非活性にするコントロールリストを作成する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DoProcForErrorMain(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'トランザクション系コントロールリスト作成
        If CreateTsxCtlList(dataHBKB0601) = False Then
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>データのロック状況をチェックし、状況に応じてロックまたは処理モードの切替を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function LockForEditMain(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'ロック設定
        If SetLockWhenLoadForEdit(dataHBKB0601) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【参照モード】ロック情報取得メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>データのロック情報を取得する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetLockDataForRefMain(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'ロック情報取得
        If GetLockDataForRef(dataHBKB0601) = False Then
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで画面の初期表示設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormEditModeMain(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'スプレッド表示用オブジェクト作成
        If CreateObjectForVw(dataHBKB0601) = False Then
            Return False
        End If

        'フォームコントロール設定
        If InitFormControl(dataHBKB0601) = False Then
            Return False
        End If

        '初期表示用データ取得
        If GetInitData(dataHBKB0601) = False Then
            Return False
        End If

        '初期表示用データセット
        If SetInitDataToControl(dataHBKB0601) = False Then
            Return False
        End If

        'モードとフラグによってサポセン情報コピーチェックボックスの表示・非表示を設定
        With dataHBKB0601
            .PropChkCopyToIncident.Visible = .PropBlnIncident
            .PropChkCopyToSetKiki.Visible = .PropBlnSetKiki
            .PropLblIncident.Visible = .PropBlnIncident
            .PropLblSetKiki.Visible = .PropBlnSetKiki
        End With

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【参照モード】画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで画面の初期表示設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormRefModeMain(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'スプレッド表示用オブジェクト作成
        If CreateObjectForVw(dataHBKB0601) = False Then
            Return False
        End If

        'フォームコントロール設定
        If InitFormControl(dataHBKB0601) = False Then
            Return False
        End If

        '初期表示用データ取得
        If GetInitData(dataHBKB0601) = False Then
            Return False
        End If

        '初期表示用データセット
        If SetInitDataToControl(dataHBKB0601) = False Then
            Return False
        End If

        'モードとフラグによってサポセン情報コピーチェックボックスの表示・非表示を設定
        With dataHBKB0601
            .PropChkCopyToIncident.Visible = False
            .PropChkCopyToSetKiki.Visible = False
            .PropLblIncident.Visible = False
            .PropLblSetKiki.Visible = False
        End With

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【履歴モード】画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードで画面の初期表示設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormRirekiModeMain(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'スプレッド表示用オブジェクト作成
        If CreateObjectForVw(dataHBKB0601) = False Then
            Return False
        End If

        'フォームコントロール設定
        If InitFormControl(dataHBKB0601) = False Then
            Return False
        End If

        '初期表示用データ取得
        If GetInitData(dataHBKB0601) = False Then
            Return False
        End If

        '初期表示用データセット
        If SetInitDataToControl(dataHBKB0601) = False Then
            Return False
        End If

        'モードとフラグによってサポセン情報コピーチェックボックスの表示・非表示を設定
        With dataHBKB0601
            .PropChkCopyToIncident.Visible = False
            .PropChkCopyToSetKiki.Visible = False
            .PropLblIncident.Visible = False
            .PropLblSetKiki.Visible = False
        End With

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 解除ボタンクリック時ロック解除メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータをログインユーザでロックし、フォームのロックを解除する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function UnlockWhenClickBtnUnlockMain(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロックフラグOFF
        dataHBKB0601.PropBlnBeLockedFlg = False

        'ロック処理
        If SetLockWhenUnlock(dataHBKB0601) = False Then
            Return False
        End If

        'フォームコントロールのロックを解除する
        If SetFormControlPerProcMode(dataHBKB0601) = False Then
            Return False
        End If
        If SetDataToLoginAndLock(dataHBKB0601) = False Then
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたユーザーIDをキーにエンドユーザーマスタを検索し、取得データを利用者情報にセットする
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function EnterClickOnUsrIDMain(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'エンドユーザーマスタ検索
        If GetEndUsrMasta(dataHBKB0601) = False Then
            Return False
        End If

        '取得データを利用者情報にセット
        If SetNewUsrData(dataHBKB0601) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' [検索]ボタン押下時利用者情報セットメイン処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サブ検索画面で選択されたエンドユーザーデータを当画面にセットする
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetNewUsrDataMain(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'サブ検索画面で選択されたユーザー情報を利用者情報にセットする
        If SetNewUsrData(dataHBKB0601) = False Then
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サブ検索画面で選択された設置機器データを当画面にセットする
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetNewSetDataMain(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'サブ検索画面で選択された設置機器データを設置情報にセットする
        If SetNewSetData(dataHBKB0601) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 複数人利用行追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>複数人利用一覧にサブ検索画面から取得したグループデータを設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetUserToVwShareMain(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '複数人利用データ設定処理
        If SetUserToVwShare(dataHBKB0601) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 複数人利用行削除時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>複数人利用一覧の選択行を削除する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowShareMain(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowShare(dataHBKB0601) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' オプションソフト行追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>オプションソフト一覧に空行を1行追加する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function AddRowOptSoftMain(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '空行追加処理
        If AddRowOptSoft(dataHBKB0601) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' オプションソフト行削除時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>オプションソフト一覧の選択行を削除する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowOptSoftMain(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowOptSoft(dataHBKB0601) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' セット機器行追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>セット機器一覧に空行を1行追加する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function AddRowSetKikiMain(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '空行追加処理
        If AddRowSetKiki(dataHBKB0601) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' セット機器行削除時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>セット機器一覧の選択行を削除する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowSetKikiMain(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowSetKiki(dataHBKB0601) = False Then
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録項目の入力チェックを行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckInputValueMain(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'コントロール入力チェック
        If CheckInputValue(dataHBKB0601) = False Then
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>他のユーザによりロックが解除されていないかチェックする
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckBeUnlockedMain(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロック解除チェック
        If CheckUnlock(dataHBKB0601) = False Then
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面の内容をログに出力し、フォームコントロールを再設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetFormRefModeFromEditModeMain(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '入力内容ログ出力
        If OutputUnlockLog(dataHBKB0601) = False Then
            Return False
        End If

        '参照モードでフォームコントロール設定
        If SetFormControlPerProcMode(dataHBKB0601) = False Then
            Return False
        End If

        '参照モードでロック情報設定
        If SetDataToLoginAndLockForRef(dataHBKB0601) = False Then
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面の内容をログに出力し、フォームコントロールを再設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetFormRirekiModeBeUnlockedMain(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '入力内容ログ出力
        If OutputUnlockLog(dataHBKB0601) = False Then
            Return False
        End If

        '履歴モードでフォームコントロール設定
        If SetFormControlPerProcMode(dataHBKB0601) = False Then
            Return False
        End If

        '履歴モードでロック情報設定
        If SetDataToLoginAndLockForRireki(dataHBKB0601) = False Then
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
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をデータベースに反映する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RegistDataOnEditModeMain(ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '更新処理
        If UpdateData(dataHBKB0601) = False Then
            Return False
        End If

        'ロック解除処理
        If UnlockData(dataHBKB0601) = False Then
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
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータのロックを解除する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function UnlockWhenCloseMain(ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロック解除処理
        If UnlockData(dataHBKB0601) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【コンボボックス共通】コンボボックスリストサイズ変更メイン処理
    ''' </summary>
    ''' <param name="sender">[IN/OUT]コンボボックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>コンボボックスのリストサイズをデータの内容に合わせて変更する
    ''' <para>作成情報：2012/08/14 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ResizeCmbListMain(ByRef sender As Object) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'コンボボックスリストサイズ変更処理
        If ResizeCmbList(sender) = False Then
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>トランザクション系コントロールのリストを作成する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateTsxCtlList(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim aryCtlList As New ArrayList

        Try
            With dataHBKB0601

                'トランザクション系のコントロールをリストに追加
                aryCtlList.Add(.PropBtnReg)              '登録ボタン
                aryCtlList.Add(.PropGrpLoginUser)        'ログイン／ロックグループ

                aryCtlList.Add(.PropChkCopyToIncident)              'インシデントコピーチェックボックス
                aryCtlList.Add(.PropChkCopyToSetKiki)        'セット機器コピーチェックボックス

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
    ''' 【共通】スプレッド用オブジェクト作成処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドに設定するデータテーブルおよびセルを作成する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateObjectForVw(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtShare As New DataTable                        '複数人利用用データテーブル
        Dim dtOptSoft As New DataTable                      'オプションソフト用データテーブル
        Dim dtSetKiki As New DataTable                      'セット機器用データテーブル
        Dim dtCauseLink As New DataTable                    '原因リンク用データテーブル
        Dim dtRireki As New DataTable                       '履歴情報用データテーブル
        Dim celOptSoft As New CellType.ComboBoxCellType     'オプションソフト用コンボボックスセルタイプ

        Try
            '複数人利用用テーブル作成
            With dtShare
                .Columns.Add("UsrID", Type.GetType("System.String"))            'ユーザーID
                .Columns.Add("UsrNM", Type.GetType("System.String"))            'ユーザー氏名

                .Columns.Add("RegDT", Type.GetType("System.DateTime"))            '登録日付
                .Columns.Add("RegGrpCD", Type.GetType("System.String"))            '登録グループ
                .Columns.Add("RegID", Type.GetType("System.String"))            '登録ユーザ

                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'オプションソフト用テーブル作成
            With dtOptSoft
                '★★-------------
                '.Columns.Add("SoftNM", Type.GetType("System.String"))           'オプションソフト（ソフト名）
                .Columns.Add("SoftCD", Type.GetType("System.Int32"))            'オプションソフト（ソフトCD）
                '★★-------------
                .Columns.Add("RegDT", Type.GetType("System.DateTime"))             '登録日付
                .Columns.Add("RegGrpCD", Type.GetType("System.String"))             '登録グループ
                .Columns.Add("RegID", Type.GetType("System.String"))                '登録ユーザ
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'セット機器用テーブル作成
            With dtSetKiki
                .Columns.Add("SetKikiNo", Type.GetType("System.String"))        'セット機器（セット機器No）
                .Columns.Add("KindCD", Type.GetType("System.String"))           '種別CD
                .Columns.Add("Num", Type.GetType("System.String"))              '番号
                .Columns.Add("SetKikiID", Type.GetType("System.Int32"))         'セット機器ID
                .Columns.Add("SetKikiNo_Org", Type.GetType("System.String"))    'セット機器No（オリジナル）
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            '原因リンク用テーブル作成
            With dtCauseLink
                .Columns.Add("ProcessKbnNMR", Type.GetType("System.String"))    'プロセス区分（略名称）
                .Columns.Add("MngNmb", Type.GetType("System.String"))           '番号
                .Columns.Add("ProcessKbn", Type.GetType("System.String"))       'プロセス区分（コード）
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            '履歴情報用テーブル作成
            With dtRireki
                .Columns.Add("RirekiNo", Type.GetType("System.String"))         '更新ID
                .Columns.Add("RegDT", Type.GetType("System.String"))            '更新日時
                .Columns.Add("GroupNM", Type.GetType("System.String"))          '更新者グループ名
                .Columns.Add("HBKUsrNM", Type.GetType("System.String"))         '更新者名
                .Columns.Add("RegReason", Type.GetType("System.String"))        '理由
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'オプションソフト用コンボボックスセル作成
            With celOptSoft
                '★★-------------
                '.EditorValue = CellType.EditorValue.String
                .EditorValue = CellType.EditorValue.ItemData
                '★★-------------
                .Editable = True
                'リストサイズ設定
                .ListWidth = 0                                                      '最大文字数の幅に合わせる
                'オートコンプリート設定
                .AutoCompleteMode = AutoCompleteMode.SuggestAppend
                .AutoSearch = FarPoint.Win.AutoSearch.SingleGreaterThan
                .AutoCompleteSource = AutoCompleteSource.ListItems

                '★★-------------
                '↓コメントアウト
                '最大入力可能文字数設定
                '.MaxLength = 100
                '★★-------------
            End With

            'データクラスに作成オブジェクトを格納
            With dataHBKB0601
                .PropDtShare = dtShare                          'スプレッド表示用：複数人利用データ
                .PropDtOptSoft = dtOptSoft                      'スプレッド表示用：オプションソフトデータ
                .PropDtSetKiki = dtSetKiki                      'スプレッド表示用：セット機器データ
                .PropDtMyCauseLink = dtCauseLink                'スプレッド表示用：原因リンクデータ
                .PropDtRireki = dtRireki                        'スプレッド表示用：履歴情報データ
                .PropCelOptSoft = celOptSoft                    'スプレッド表示用：オプションソフトセルタイプ
            End With

            '終了ログ出力
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
            dtShare.Dispose()
            dtOptSoft.Dispose()
            dtSetKiki.Dispose()
            dtCauseLink.Dispose()
            dtRireki.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】初期表示用データ取得処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            'マスタデータ取得
            If GetMastaData(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'メイン表示データ取得
            If GetMainData(Adapter, Cn, dataHBKB0601) = False Then
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
            'コネクションが閉じられていない場合は閉じる
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLockWhenUnlock(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'CI共通情報テーブルロック解除
            If commonLogicHBK.UnlockCIInfo(dataHBKB0601.PropIntCINmb) = False Then
                Return False
            End If

            'CI共通情報テーブルロック
            If commonLogicHBK.LockCIInfo(dataHBKB0601.PropIntCINmb, dataHBKB0601.PropDtCILock) = False Then
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLockWhenLoadForEdit(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnBeLocked As Boolean = False                   'ロックフラグ  

        Try

            With dataHBKB0601

                'ロック解除チェック
                If commonLogicHBK.CheckDataBeLocked(.PropIntCINmb, blnBeLocked, .PropStrBeLockedMsg, .PropDtCILock) = False Then
                    Return False
                End If

                'ロックされている（別のユーザが編集中）場合、 ロックフラグをON
                If blnBeLocked = True Then

                    dataHBKB0601.PropBlnBeLockedFlg = True

                Else

                    'ロックされていない場合、CI共通情報をロックする
                    If SetLock(dataHBKB0601) = False Then
                        Return False
                    End If

                    'ロックフラグをOFF
                    dataHBKB0601.PropBlnBeLockedFlg = False

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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集開始日時によりロック設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckAndSetLock(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnBeLocked As Boolean = False                   'ロックフラグ  

        Try

            With dataHBKB0601

                'ロック解除チェック
                If commonLogicHBK.CheckDataBeLocked(.PropIntCINmb, blnBeLocked, .PropStrBeLockedMsg, .PropDtCILock) = False Then
                    Return False
                End If

                'ロックされている（別のユーザが編集中）場合、 ロックフラグをON
                If blnBeLocked = True Then

                    dataHBKB0601.PropBlnBeLockedFlg = True

                Else

                    'ロックされていない場合、CI共通情報をロックする
                    If SetLock(dataHBKB0601) = False Then
                        Return False
                    End If

                    'ロックフラグをOFF
                    dataHBKB0601.PropBlnBeLockedFlg = False

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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報テーブルをロックする
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLock(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnDoUnlock As Boolean = False                   'ロック解除実行フラグ

        Try

            With dataHBKB0601

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
    ''' 【参照モード】ロック情報取得処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ロック情報を取得し、データクラスにセットする
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetLockDataForRef(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnBeLocked As Boolean = False                   'ロックフラグ  

        Try

            With dataHBKB0601

                'ロック情報取得
                If commonLogicHBK.GetCILockTb(.PropIntCINmb, .PropDtCILock) = False Then
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InitFormControl(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'スプレッド設定
            If SetVwControl(dataHBKB0601) = False Then
                Return False
            End If

            '隠しラベル非表示設定
            If SetHiddenLabelUnvisible(dataHBKB0601) = False Then
                Return False
            End If

            '処理モードに応じたフォームコントロール設定
            If SetFormControlPerProcMode(dataHBKB0601) = False Then
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフォームコントロールの設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormControlPerProcMode(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'ログイン／ロックコントロール設定
            If SetLoginAndLockControl(dataHBKB0601) = False Then
                Return False
            End If

            'ヘッダ設定
            If SetHeaderControl(dataHBKB0601) = False Then
                Return False
            End If

            'フッタ設定
            If SetFooterControl(dataHBKB0601) = False Then
                Return False
            End If

            'タブページ設定
            If SetTabControl(dataHBKB0601) = False Then
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
    ''' 【共通】エンドユーザーマスタ取得処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたユーザーIDをキーにエンドユーザーマスタを検索する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetEndUsrMasta(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            'エンドユーザーマスタデータ取得
            If commonLogicHBK.GetEndUsrMastaData(Adapter, Cn, dataHBKB0601.PropTxtUsrID.Text, dataHBKB0601.PropDtResultSub) = False Then
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
    ''' 【共通】利用者情報設定
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サブ検索にて選択もしくはENTERキーにて取得したエンドユーザーデータを利用者情報にセットする
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetNewUsrData(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

                '選択データがある場合のみ値をセットする
                If .PropDtResultSub IsNot Nothing Then

                    If .PropDtResultSub.Rows.Count = 1 Then

                        '選択されたエンドユーザー情報を利用者情報にセットする
                        '※連絡先、所属局、番組／部屋はクリア
                        .PropTxtUsrID.Text = .PropDtResultSub.Rows(0).Item("EndUsrID")               'ユーザーID
                        .PropTxtUsrNM.Text = .PropDtResultSub.Rows(0).Item("EndUsrNM")               'ユーザー氏名
                        .PropTxtUsrMailAdd.Text = .PropDtResultSub.Rows(0).Item("EndUsrMailAdd")     'ユーザーメールアドレス
                        '.PropTxtUsrTel.Text = .PropDtResultSub.Rows(0).Item("EndUsrContact")         'ユーザー電話番号　※連絡先をセット
                        .PropTxtUsrTel.Text = .PropDtResultSub.Rows(0).Item("EndUsrTel")         'ユーザー電話番号　※連絡先をセット
                        .PropTxtUsrKyokuNM.Text = ""                                                 'ユーザー所属局
                        .PropTxtUsrBusyoNM.Text = .PropDtResultSub.Rows(0).Item("EndUsrBusyoNM")     'ユーザー所属部署
                        .PropTxtUsrCompany.Text = .PropDtResultSub.Rows(0).Item("EndUsrCompany")     'ユーザー所属会社
                        .PropTxtUsrContact.Text = ""                                                 'ユーザー連絡先
                        .PropTxtUsrRoom.Text = ""                                                    'ユーザー番組／部屋

                    ElseIf .PropDtResultSub.Rows.Count = 0 Then

                        '取得データがない場合（ENTERキーにて検索した場合）クリア
                        .PropTxtUsrID.Text = ""                                                      'ユーザーID
                        .PropTxtUsrNM.Text = ""                                                      'ユーザー氏名
                        .PropTxtUsrMailAdd.Text = ""                                                 'ユーザーメールアドレス
                        .PropTxtUsrTel.Text = ""                                                     'ユーザー電話番号
                        .PropTxtUsrKyokuNM.Text = ""                                                 'ユーザー所属局
                        .PropTxtUsrBusyoNM.Text = ""                                                 'ユーザー所属部署
                        .PropTxtUsrCompany.Text = ""                                                 'ユーザー所属会社
                        .PropTxtUsrContact.Text = ""                                                 'ユーザー連絡先
                        .PropTxtUsrRoom.Text = ""

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
    ''' 【共通】設置情報設定
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サブ検索にて選択された設置機器データを設置情報にセットする
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetNewSetData(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

                '選択データがある場合のみ値をセットする
                If .PropDtResultSub IsNot Nothing Then

                    '選択された設置機器情報を設置情報にセットする
                    '※設置デスクNo、設置LANケーブル長さ、設置LANケーブル番号、情報コンセント・SWはクリア
                    .PropTxtSetKyokuNM.Text = .PropDtResultSub.Rows(0).Item("SetKyokuNM")       '設置局
                    .PropTxtSetBusyoNM.Text = .PropDtResultSub.Rows(0).Item("SetBusyoNM")       '設置部署
                    .PropTxtSetRoom.Text = .PropDtResultSub.Rows(0).Item("SetRoom")             '設置番組／部屋
                    .PropTxtSetBuil.Text = .PropDtResultSub.Rows(0).Item("SetBuil")             '設置建物
                    .PropTxtSetFloor.Text = .PropDtResultSub.Rows(0).Item("SetFloor")           '設置フロア
                    .PropTxtSetDeskNo.Text = ""                                                 '設置デスクNo
                    .PropTxtSetLANLength.Text = ""                                              '設置LANケーブル長さ
                    .PropTxtSetLANNum.Text = ""                                                 '設置LANケーブル番号
                    .PropTxtSetSocket.Text = ""                                                 '情報コンセント・SW

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
    ''' 【共通】ログイン／ロックコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControl(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

                If .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetLoginAndLockControlForEdit(dataHBKB0601) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照（ロック）モード用設定
                        If SetLoginAndLockControlForRef(dataHBKB0601) = False Then
                            Return False
                        End If

                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then      '参照モード

                    '参照モード用設定
                    If SetLoginAndLockControlForRef(dataHBKB0601) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetLoginAndLockControlForRireki(dataHBKB0601) = False Then
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
    ''' 【編集モード】ログイン／ロックコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForEdit(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601.PropGrpLoginUser

                'グループコンボボックス非活性
                .cmbGroup.Enabled = False

                '変更ボタン非活性
                .btnChange.Enabled = False

                'ロック情報表示
                .PropLockInfoVisible = True

                '解除ボタン表示
                .PropBtnUnlockVisible = True
                'ロック情報が取得できた場合
                If dataHBKB0601.PropDtCILock.Rows.Count > 0 Then

                    '編集者と同じグループの場合は解除ボタン活性化
                    If .cmbGroup.SelectedValue = dataHBKB0601.PropDtCILock.Rows(0).Item("EdiGrpCD") And _
                       PropUserId <> dataHBKB0601.PropDtCILock.Rows(0).Item("EdiID") Then
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForRef(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601.PropGrpLoginUser

                'グループコンボボックス非活性
                .cmbGroup.Enabled = False

                '変更ボタン非活性
                .btnChange.Enabled = False

                'ロック情報表示
                .PropLockInfoVisible = True

                '解除ボタン表示
                .PropBtnUnlockVisible = True

                'ロック情報が取得できた場合
                If dataHBKB0601.PropDtCILock.Rows.Count > 0 Then

                    '編集者と同じグループの場合で、ロックされている場合（編集モードでロックフラグONの場合）、解除ボタン活性化
                    If .cmbGroup.SelectedValue = dataHBKB0601.PropDtCILock.Rows(0).Item("EdiGrpCD") AndAlso
                        dataHBKB0601.PropBlnBeLockedFlg = True Then
                        .PropBtnUnlockEnabled = True
                    Else
                        .PropBtnUnlockEnabled = False
                    End If

                Else

                    '解除ボタン非活性
                    .PropBtnUnlockEnabled = False

                End If

                'ロック解除から遷移してきた場合は解除ボタンを非活性
                If dataHBKB0601.PropBlnLockCompare = True Then
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForRireki(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601.PropGrpLoginUser

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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてヘッダコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetHeaderControl(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601


                If .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetHeaderControlForEdit(dataHBKB0601) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetHeaderControlForRef(dataHBKB0601) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_REF Then      '参照モード

                    '参照モード用設定
                    If SetHeaderControlForRef(dataHBKB0601) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetHeaderControlForRireki(dataHBKB0601) = False Then
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
    ''' 【編集モード】ヘッダコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでヘッダコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetHeaderControlForEdit(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでヘッダコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetHeaderControlForRef(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでヘッダコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetHeaderControlForRireki(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControl(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

                If .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetFooterControlForEdit(dataHBKB0601) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetFooterControlForRef(dataHBKB0601) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_REF Then      '参照モード

                    '参照モード用設定
                    If SetFooterControlForRef(dataHBKB0601) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetFooterControlForRireki(dataHBKB0601) = False Then
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
    ''' 【編集モード】フッタコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForEdit(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

                '登録ボタン活性化
                .PropBtnReg.Enabled = True



            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForRef(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

                '登録ボタン非活性
                .PropBtnReg.Enabled = False

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForRireki(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

                '登録ボタン非表示
                .PropBtnReg.Visible = False

                'サポセン情報コピーチェックボックス非表示
                .PropChkCopyToIncident.Visible = False
                .PropChkCopyToSetKiki.Visible = False
                .PropLblIncident.Visible = False
                .PropLblSetKiki.Visible = False
            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてタブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControl(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '登録完了後はタブ移動を行わない
            If dataHBKB0601.PropBlnkanryoFlg = False Then
                'デフォルトタブページ設定
                If SetDefaultTabPage(dataHBKB0601) = False Then
                    Return False
                End If
            End If

            '基本情報タブ設定
            If SetTabControlKhn(dataHBKB0601) = False Then
                Return False
            End If

            '利用情報タブ設定
            If SetTabControlRiyo(dataHBKB0601) = False Then
                Return False
            End If

            'フリー入力情報タブ設定
            If SetTabControlFree(dataHBKB0601) = False Then
                Return False
            End If

            '関係情報タブ設定
            If SetTabControlRelation(dataHBKB0601) = False Then
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
    ''' 【共通】デフォルトタブページ設定
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてデフォルトタブページを設定する
    ''' <para>作成情報：2012/09/04 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDefaultTabPage(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

                If .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        'デフォルト：利用情報タブ
                        .PropTbInput.SelectedIndex = TAB_RIYO

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        'デフォルト：利用情報タブ
                        .PropTbInput.SelectedIndex = TAB_RIYO

                    End If


                ElseIf .PropStrProcMode = PROCMODE_REF Then      '参照モード

                    'デフォルト：基本情報タブ
                    .PropTbInput.SelectedIndex = TAB_KHN


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '呼び出し元が編集モードの場合
                    If .PropStrProcModeFromSap = PROCMODE_EDIT Then

                        'デフォルト：利用情報タブ
                        .PropTbInput.SelectedIndex = TAB_RIYO

                    Else

                        'デフォルト：基本情報タブ
                        .PropTbInput.SelectedIndex = TAB_KHN

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
    ''' 【共通】基本情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhn(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

                If .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetTabControlKhnForEdit(dataHBKB0601) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照（ロック）モード用設定
                        If SetTabControlKhnForEditRef(dataHBKB0601) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_REF Then      '参照モード

                    '参照モード用設定
                    If SetTabControlKhnForRef(dataHBKB0601) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetTabControlKhnForRireki(dataHBKB0601) = False Then
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
    ''' 【編集モード】基本情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForEdit(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

                '作業に応じてコントロール設定
                If SetTabControlKhnPerWork(dataHBKB0601) = False Then
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
    ''' 【編集モード】基本情報タブコントロール設定：作業毎
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnPerWork(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

                '前画面から渡された作業に応じてパラメータの作成を行う
                Select Case .PropStrWorkCD

                    Case WORK_CD_SETUP          'セットアップ

                        'セットアップ用コントロール設定処理
                        If SetTabControlKhnForSetUp(dataHBKB0601) = False Then
                            Return False
                        End If

                    Case WORK_CD_OBSOLETE       '陳腐化

                        '陳腐化用コントロール設定処理
                        If SetTabControlKhnForObsolete(dataHBKB0601) = False Then
                            Return False
                        End If

                    Case WORK_CD_SET            '設置

                        '設置用コントロール設定処理
                        If SetTabControlKhnForSet(dataHBKB0601) = False Then
                            Return False
                        End If

                    Case WORK_CD_ADDCONFIG      '追加設定

                        '追加設定用コントロール設定処理
                        If SetTabControlKhnForAddConfig(dataHBKB0601) = False Then
                            Return False
                        End If

                    Case WORK_CD_REMOVE         '撤去

                        '撤去用コントロール設定処理
                        If SetTabControlKhnForRemove(dataHBKB0601) = False Then
                            Return False
                        End If

                    Case WORK_CD_BREAKDOWN      '故障

                        '故障用コントロール設定処理
                        If SetTabControlKhnForBreakDown(dataHBKB0601) = False Then
                            Return False
                        End If

                    Case WORK_CD_REPAIR         '修理

                        '修理用コントロール設定処理
                        If SetTabControlKhnForRepair(dataHBKB0601) = False Then
                            Return False
                        End If

                    Case WORK_CD_TIDYUP         '片付

                        '片付用コントロール設定処理
                        If SetTabControlKhnForTidyUp(dataHBKB0601) = False Then
                            Return False
                        End If

                    Case WORK_CD_PREDISPOSE     '廃棄準備

                        '廃棄準備用コントロール設定処理
                        If SetTabControlKhnForPreDispose(dataHBKB0601) = False Then
                            Return False
                        End If

                    Case WORK_CD_DISPOSE        '廃棄

                        '廃棄用コントロール設定処理
                        If SetTabControlKhnForDispose(dataHBKB0601) = False Then
                            Return False
                        End If

                    Case WORK_CD_BELOST         '紛失

                        '紛失用コントロール設定処理
                        If SetTabControlKhnForBeLost(dataHBKB0601) = False Then
                            Return False
                        End If

                    Case WORK_CD_REVERT         '復帰

                        '復帰用コントロール設定処理
                        If SetTabControlKhnForRevert(dataHBKB0601) = False Then
                            Return False
                        End If

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
    ''' 【編集モード】基本情報タブコントロール設定：セットアップ作業用
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>セットアップ作業時、編集モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForSetUp(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'タブ全コントロール非活性化
            If SetTabControlKhnUnabledAll(dataHBKB0601) = False Then
                Return False
            End If

            With dataHBKB0601

                'イメージ番号入力可
                .PropTxtImageNmb.ReadOnly = False

                '機器状態入力可
                .PropTxtKikiState.ReadOnly = False

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】基本情報タブコントロール設定：陳腐化作業用
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>陳腐化作業時、編集モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForObsolete(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'タブ全コントロール非活性化
            If SetTabControlKhnUnabledAll(dataHBKB0601) = False Then
                Return False
            End If

            With dataHBKB0601

                '機器状態入力可
                .PropTxtKikiState.ReadOnly = False

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】基本情報タブコントロール設定：設置作業用
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>設置作業時、編集モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForSet(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'タブ全コントロール非活性化
            If SetTabControlKhnUnabledAll(dataHBKB0601) = False Then
                Return False
            End If

            With dataHBKB0601

                '機器状態入力可
                .PropTxtKikiState.ReadOnly = False

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】基本情報タブコントロール設定：追加設定作業用
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>追加設定作業時、編集モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForAddConfig(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'タブ全コントロール非活性化
            If SetTabControlKhnUnabledAll(dataHBKB0601) = False Then
                Return False
            End If

            With dataHBKB0601

                '機器状態入力可
                .PropTxtKikiState.ReadOnly = False

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】基本情報タブコントロール設定：撤去作業用
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>撤去作業時、編集モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForRemove(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'タブ全コントロール非活性化
            If SetTabControlKhnUnabledAll(dataHBKB0601) = False Then
                Return False
            End If

            With dataHBKB0601

                '機器状態入力可
                .PropTxtKikiState.ReadOnly = False

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】基本情報タブコントロール設定：故障作業用
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>故障作業時、編集モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForBreakDown(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'タブ全コントロール非活性化
            If SetTabControlKhnUnabledAll(dataHBKB0601) = False Then
                Return False
            End If

            With dataHBKB0601

                '機器状態入力可
                .PropTxtKikiState.ReadOnly = False

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】基本情報タブコントロール設定：修理作業用
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>修理作業時、編集モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForRepair(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'タブ全コントロール非活性化
            If SetTabControlKhnUnabledAll(dataHBKB0601) = False Then
                Return False
            End If

            With dataHBKB0601

                '機器状態入力可
                .PropTxtKikiState.ReadOnly = False

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】基本情報タブコントロール設定：片付作業用
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>片付作業時、編集モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForTidyUp(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'タブ全コントロール非活性化
            If SetTabControlKhnUnabledAll(dataHBKB0601) = False Then
                Return False
            End If

            With dataHBKB0601

                '機器状態入力可
                .PropTxtKikiState.ReadOnly = False

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】基本情報タブコントロール設定：廃棄準備作業用
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>廃棄準備作業時、編集モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForPreDispose(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'タブ全コントロール非活性化
            If SetTabControlKhnUnabledAll(dataHBKB0601) = False Then
                Return False
            End If

            With dataHBKB0601

                '機器状態入力可
                .PropTxtKikiState.ReadOnly = False

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】基本情報タブコントロール設定：廃棄作業用
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>廃棄作業時、編集モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForDispose(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'タブ全コントロール非活性化
            If SetTabControlKhnUnabledAll(dataHBKB0601) = False Then
                Return False
            End If

            With dataHBKB0601

                '機器状態入力可
                .PropTxtKikiState.ReadOnly = False

                'ステータス入力可
                .PropCmbCIStatus.Enabled = True

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】基本情報タブコントロール設定：紛失作業用
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>紛失作業時、編集モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForBeLost(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'タブ全コントロール非活性化
            If SetTabControlKhnUnabledAll(dataHBKB0601) = False Then
                Return False
            End If

            With dataHBKB0601

                '機器状態入力可
                .PropTxtKikiState.ReadOnly = False

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】基本情報タブコントロール設定：復帰作業用
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>復帰作業時、編集モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForRevert(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'タブ全コントロール非活性化
            If SetTabControlKhnUnabledAll(dataHBKB0601) = False Then
                Return False
            End If

            With dataHBKB0601

                '機器状態入力可
                .PropTxtKikiState.ReadOnly = False

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【参照（ロック）モード】基本情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照（ロック）モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/07 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForEditRef(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

                '前画面からの作業に応じてコントロール設定
                If SetTabControlKhnPerWork(dataHBKB0601) = False Then
                    Return False
                End If

                '参照モード用設定
                If SetTabControlKhnForRef(dataHBKB0601) = False Then
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
    ''' 【参照モード】基本情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForRef(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

                '種別コンボボックス
                .PropCmbKind.Enabled = False

                '番号テキストボックス
                .PropTxtNum.ReadOnly = True

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForRireki(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

                'タブ内の全コントロールを非活性化
                If SetTabControlKhnUnabledAll(dataHBKB0601) = False Then
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
    ''' 【編集モード】基本情報タブ全コントロール非活性化
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>基本情報タブの全コントロールを非活性にする
    ''' <para>作成情報：2012/08/02 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnUnabledAll(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

                '種別コンボボックス
                .PropCmbKind.Enabled = False

                '番号テキストボックス
                .PropTxtNum.ReadOnly = True

                '分類１～２テキストボックス
                .PropTxtClass1.ReadOnly = True
                .PropTxtClass2.ReadOnly = True

                '名称テキストボックス
                .PropTxtCINM.ReadOnly = True

                '型番テキストボックス
                .PropTxtKataban.ReadOnly = True

                'タイプコンボボックス
                .PropCmbType.Enabled = False

                'ステータスコンボボックス
                .PropCmbCIStatus.Enabled = False

                'サービスセンター保管機チェックボックス
                .PropChkSCHokanKbn.Enabled = False

                '製造番号テキストボックス
                .PropTxtSerial.ReadOnly = True

                'MACアドレス１～２テキストボックス
                .PropTxtMacAddress1.ReadOnly = True
                .PropTxtMacAddress2.ReadOnly = True

                'イメージ番号テキストボックス
                .PropTxtImageNmb.ReadOnly = True

                'メモリ容量テキストボックス
                .PropTxtMemorySize.ReadOnly = True

                'サポセン固定資産番号テキストボックス
                .PropTxtSCKikiFixNmb.ReadOnly = True

                'リース期限日（機器）DateTimePicker
                .PropDtpLeaseUpDT_Kiki.Enabled = False

                '付属品テキストボックス
                .PropTxtFuzokuhin.ReadOnly = True

                '機器状態テキストボックス
                .PropTxtKikiState.ReadOnly = True

                '説明テキストボックス
                .PropTxtCINaiyo.ReadOnly = True

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて利用情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRiyo(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

                If .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetTabControlRiyoForEdit(dataHBKB0601) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then     '参照（ロック）モード

                        '参照（ロック）モード用設定
                        If SetTabControlRiyoForEditRef(dataHBKB0601) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_REF Then      '参照モード

                    '参照モード用設定
                    If SetTabControlRiyoForRef(dataHBKB0601) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetTabControlRiyoForRireki(dataHBKB0601) = False Then
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
    ''' 【編集モード】利用情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで利用情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRiyoForEdit(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

                '前画面からの作業に応じてコントロール設定
                If SetTabControlRiyoPerWork(dataHBKB0601) = False Then
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
    ''' 【編集モード】利用情報タブコントロール設定：作業毎
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで利用情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRiyoPerWork(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

                '前画面から渡された作業に応じてパラメータの作成を行う
                Select Case .PropStrWorkCD

                    Case WORK_CD_SETUP          'セットアップ

                        'セットアップ用コントロール設定処理
                        If SetTabControlRiyoForSetUp(dataHBKB0601) = False Then
                            Return False
                        End If

                    Case WORK_CD_OBSOLETE       '陳腐化

                        '陳腐化用コントロール設定処理
                        If SetTabControlRiyoForObsolete(dataHBKB0601) = False Then
                            Return False
                        End If

                    Case WORK_CD_SET            '設置

                        '設置用コントロール設定処理
                        If SetTabControlRiyoForSet(dataHBKB0601) = False Then
                            Return False
                        End If

                    Case WORK_CD_ADDCONFIG      '追加設定

                        '追加設定用コントロール設定処理
                        If SetTabControlRiyoForAddConfig(dataHBKB0601) = False Then
                            Return False
                        End If

                    Case WORK_CD_REMOVE         '撤去

                        '撤去用コントロール設定処理
                        If SetTabControlRiyoForRemove(dataHBKB0601) = False Then
                            Return False
                        End If

                    Case WORK_CD_BREAKDOWN      '故障

                        '故障用コントロール設定処理
                        If SetTabControlRiyoForBreakDown(dataHBKB0601) = False Then
                            Return False
                        End If

                    Case WORK_CD_REPAIR         '修理

                        '修理用コントロール設定処理
                        If SetTabControlRiyoForRepair(dataHBKB0601) = False Then
                            Return False
                        End If

                    Case WORK_CD_TIDYUP         '片付

                        '片付用コントロール設定処理
                        If SetTabControlRiyoForTidyUp(dataHBKB0601) = False Then
                            Return False
                        End If

                    Case WORK_CD_PREDISPOSE     '廃棄準備

                        '廃棄準備用コントロール設定処理
                        If SetTabControlRiyoForPreDispose(dataHBKB0601) = False Then
                            Return False
                        End If

                    Case WORK_CD_DISPOSE        '廃棄

                        '廃棄用コントロール設定処理
                        If SetTabControlRiyoForDispose(dataHBKB0601) = False Then
                            Return False
                        End If

                    Case WORK_CD_BELOST         '紛失

                        '紛失用コントロール設定処理
                        If SetTabControlRiyoForBeLost(dataHBKB0601) = False Then
                            Return False
                        End If

                    Case WORK_CD_REVERT         '復帰

                        '復帰用コントロール設定処理
                        If SetTabControlRiyoForRevert(dataHBKB0601) = False Then
                            Return False
                        End If

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
    ''' 【編集モード】利用情報タブコントロール設定：セットアップ作業用
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>セットアップ作業時、利用情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRiyoForSetUp(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'タブ内の全コントロールを非活性化
            If SetTabControlRiyoUnabledAll(dataHBKB0601) = False Then
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
    ''' 【編集モード】利用情報タブコントロール設定：陳腐化作業用
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>陳腐化作業時、利用情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRiyoForObsolete(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'タブ内の全コントロールを非活性化
            If SetTabControlRiyoUnabledAll(dataHBKB0601) = False Then
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
    ''' 【編集モード】利用情報タブコントロール設定：設置作業用
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>設置作業時、利用情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRiyoForSet(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'ボタンを全て活性化
            If SetTabControlRiyoEnabledAllBtn(dataHBKB0601) = False Then
                Return False
            End If

            With dataHBKB0601

                '最終お知らせ日入力不可
                .PropDtpLastInfoDT.Enabled = False

                '作業の元入力不可
                .PropTxtWorkFromNmb.ReadOnly = True

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】利用情報タブコントロール設定：追加設定作業用
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>追加設定作業時、利用情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRiyoForAddConfig(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'ボタンを全て活性化
            If SetTabControlRiyoEnabledAllBtn(dataHBKB0601) = False Then
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
    ''' 【編集モード】利用情報タブコントロール設定：撤去作業用
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>撤去作業時、利用情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRiyoForRemove(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'タブ内の全コントロールを非活性化
            If SetTabControlRiyoUnabledAll(dataHBKB0601) = False Then
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
    ''' 【編集モード】利用情報タブコントロール設定：故障作業用
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>故障作業時、利用情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRiyoForBreakDown(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'タブ内の全コントロールを非活性化
            If SetTabControlRiyoUnabledAll(dataHBKB0601) = False Then
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
    ''' 【編集モード】利用情報タブコントロール設定：修理作業用
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>修理作業時、利用情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRiyoForRepair(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'タブ内の全コントロールを非活性化
            If SetTabControlRiyoUnabledAll(dataHBKB0601) = False Then
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
    ''' 【編集モード】利用情報タブコントロール設定：片付作業用
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>片付作業時、利用情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRiyoForTidyUp(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'タブ内の全コントロールを非活性化
            If SetTabControlRiyoUnabledAll(dataHBKB0601) = False Then
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
    ''' 【編集モード】利用情報タブコントロール設定：廃棄準備作業用
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>廃棄準備作業時、利用情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRiyoForPreDispose(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'タブ内の全コントロールを非活性化
            If SetTabControlRiyoUnabledAll(dataHBKB0601) = False Then
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
    ''' 【編集モード】利用情報タブコントロール設定：廃棄作業用
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>廃棄作業時、利用情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRiyoForDispose(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'タブ内の全コントロールを非活性化
            If SetTabControlRiyoUnabledAll(dataHBKB0601) = False Then
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
    ''' 【編集モード】利用情報タブコントロール設定：紛失作業用
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>紛失作業時、利用情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRiyoForBeLost(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'タブ内の全コントロールを非活性化
            If SetTabControlRiyoUnabledAll(dataHBKB0601) = False Then
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
    ''' 【編集モード】利用情報タブコントロール設定：復帰作業用
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>復帰作業時、利用情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRiyoForRevert(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'タブ内の全コントロールを非活性化
            If SetTabControlRiyoUnabledAll(dataHBKB0601) = False Then
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
    ''' 【参照（ロック）モード】利用情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照（ロック）モードで利用情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/07 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRiyoForEditRef(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

                '前画面からの作業に応じてコントロール設定
                If SetTabControlRiyoPerWork(dataHBKB0601) = False Then
                    Return False
                End If

                '参照モード用設定
                If SetTabControlRiyoForRef(dataHBKB0601) = False Then
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
    ''' 【参照モード】利用情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで利用情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRiyoForRef(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

                '検索ボタン（ユーザー）
                .PropBtnSearch_Usr.Enabled = False

                '複数人利用ボタン
                .PropBtnAddRow_Share.Enabled = False
                .PropBtnRemoveRow_Share.Enabled = False

                '[Add] 2012/10/24 s.yamaguchi START
                '1年後当月末ボタン
                .PropBtnGetOneYearLater_CMonth.Enabled = False
                '1年後先月末ボタン
                .PropBtnGetOneYearLater_LMonth.Enabled = False
                '[Add] 2012/10/24 s.yamaguchi END

                'オプションソフトボタン
                .PropBtnAddRow_OptSoft.Enabled = False
                .PropBtnRemoveRow_OptSoft.Enabled = False

                '検索ボタン（設置）
                .PropBtnSearch_Set.Enabled = False

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードで利用情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRiyoForRireki(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'タブ内の全コントロールを非活性化
            If SetTabControlRiyoUnabledAll(dataHBKB0601) = False Then
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
    ''' 【編集モード】利用情報タブ全コントロール非活性化
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>利用情報タブの全コントロールを非活性にする
    ''' <para>作成情報：2012/08/02 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRiyoUnabledAll(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

                'ユーザーIDテキストボックス
                .PropTxtUsrID.ReadOnly = True

                'ユーザー氏名テキストボックス
                .PropTxtUsrNM.ReadOnly = True

                '検索ボタン（ユーザー）
                .PropBtnSearch_Usr.Enabled = False

                'ユーザーメールアドレステキストボックス
                .PropTxtUsrMailAdd.ReadOnly = True

                'ユーザー電話番号テキストボックス
                .PropTxtUsrTel.ReadOnly = True

                'ユーザー所属局テキストボックス
                .PropTxtUsrKyokuNM.ReadOnly = True

                'ユーザー所属部署テキストボックス
                .PropTxtUsrBusyoNM.ReadOnly = True

                'ユーザー所属会社テキストボックス
                .PropTxtUsrCompany.ReadOnly = True

                'ユーザー連絡先テキストボックス
                .PropTxtUsrContact.ReadOnly = True

                'ユーザー番組／部屋テキストボックス
                .PropTxtUsrRoom.ReadOnly = True

                '複数人利用ボタン
                .PropBtnAddRow_Share.Enabled = False
                .PropBtnRemoveRow_Share.Enabled = False

                'レンタル期間FROM～TO DateTimePicker
                .PropDtpRentalStDT.Enabled = False
                .PropDtpRentalEdDT.Enabled = False

                '[Add] 2012/10/24 s.yamaguchi START
                '1年後当月末ボタン
                .PropBtnGetOneYearLater_CMonth.Enabled = False
                '1年後先月末ボタン
                .PropBtnGetOneYearLater_LMonth.Enabled = False
                '[Add] 2012/10/24 s.yamaguchi END

                '最終お知らせ日DateTimePicker
                .PropDtpLastInfoDT.Enabled = False

                '作業の元テキストボックス
                .PropTxtWorkFromNmb.ReadOnly = True

                '機器利用形態コンボボックス
                .PropCmbKikiUse.Enabled = False

                'IP割当種類コンボボックス
                .PropCmbIPUse.Enabled = False

                '固定IPテキストボックス
                .PropTxtFixedIP.ReadOnly = True

                'オプションソフトボタン
                .PropBtnAddRow_OptSoft.Enabled = False
                .PropBtnRemoveRow_OptSoft.Enabled = False

                '管理局
                .PropTxtManageKyokuNM.ReadOnly = True

                '管理部署
                .PropTxtManageBusyoNM.ReadOnly = True

                '設置局
                .PropTxtSetKyokuNM.ReadOnly = True

                '設置部署
                .PropTxtSetBusyoNM.ReadOnly = True

                '検索ボタン（設置）
                .PropBtnSearch_Set.Enabled = False

                '設置番組／部屋
                .PropTxtSetRoom.ReadOnly = True

                '設置建物
                .PropTxtSetBuil.ReadOnly = True

                '設置フロア
                .PropTxtSetFloor.ReadOnly = True

                '設置デスクNo
                .PropTxtSetDeskNo.ReadOnly = True

                '設置LANケーブル長さ
                .PropTxtSetLANLength.ReadOnly = True

                '設置LANケーブル番号
                .PropTxtSetLANNum.ReadOnly = True

                '情報コンセント・SW
                .PropTxtSetSocket.ReadOnly = True

                '全スプレッド非活性フラグON
                .PropBlnTabRiyoVwAllUnabled = True

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】利用情報タブボタン活性化
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>利用情報タブのボタンコントロールを活性にする
    ''' <para>作成情報：2012/08/02 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRiyoEnabledAllBtn(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

                '検索ボタン（ユーザー）
                .PropBtnSearch_Usr.Enabled = True

                '複数人利用ボタン
                .PropBtnAddRow_Share.Enabled = True
                .PropBtnRemoveRow_Share.Enabled = True

                '[Add] 2012/10/24 s.yamaguchi START
                '1年後当月末ボタン
                .PropBtnGetOneYearLater_CMonth.Enabled = True
                '1年後先月末ボタン
                .PropBtnGetOneYearLater_LMonth.Enabled = True
                '[Add] 2012/10/24 s.yamaguchi END

                'オプションソフトボタン
                .PropBtnAddRow_OptSoft.Enabled = True
                .PropBtnRemoveRow_OptSoft.Enabled = True

                '検索ボタン（設置）
                .PropBtnSearch_Set.Enabled = True

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフリー入力情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlFree(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601


                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モードなし


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード

                    '編集／参照（ロック）モードなし


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetTabControlFreeForRireki(dataHBKB0601) = False Then
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフリー入力情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlFreeForRireki(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'タブ内の全コントロールを非活性化
            If SetTabControlFreeUnabledAll(dataHBKB0601) = False Then
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
    ''' 【編集モード】フリー入力情報タブ全コントロール非活性化
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フリー入力情報タブ内の全コントロールを非活性化する
    ''' <para>作成情報：2012/08/02 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlFreeUnabledAll(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて関係情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRelation(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モードなし


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetTabControlRelationForEdit(dataHBKB0601) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照（ロック）モード用設定 ※編集モードと同じ
                        If SetTabControlRelationForEdit(dataHBKB0601) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_REF Then        '参照モード


                    '参照モード用設定
                    If SetTabControlRelationForRef(dataHBKB0601) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    If SetTabControlRelationForRireki(dataHBKB0601) = False Then
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで関係情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRelationForEdit(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'タブ内の全コントロールを非活性化
            If SetTabControlRelationUnabledAll(dataHBKB0601) = False Then
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
    ''' 【参照モード】関係情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで関係情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRelationForRef(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

                '検索ボタン
                .PropBtnSearch_Grp.Enabled = False

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードで関係情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRelationForRireki(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

                'タブ内の全コントロールを非活性化する
                If SetTabControlRelationUnabledAll(dataHBKB0601) = False Then
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
    ''' 【履歴モード】関係情報タブ全コントロール非活性化
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係情報タブ内の全コントロールを非活性化する
    ''' <para>作成情報：2012/08/02 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlRelationUnabledAll(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

                'CIオーナー名テキストボックス
                .PropTxtCIOwnerNM.ReadOnly = True

                '検索ボタン
                .PropBtnSearch_Grp.Enabled = False

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>マスタデータを取得する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMastaData(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'CI種別マスタ取得
            If commonLogicHBK.GetCIKindMastaData(Adapter, Cn, CI_TYPE_SUPORT, dataHBKB0601.PropDtCIKindMasta) = False Then
                Return False
            End If

            '種別マスタ取得
            '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 START
            'If commonLogicHBK.GetKindMastaData(Adapter, Cn, CI_TYPE_SUPORT, dataHBKB0601.PropDtKindMasta) = False Then
            '    Return False
            'End If
            If commonLogicHBK.GetKindMastaData(Adapter, Cn, CI_TYPE_SUPORT, dataHBKB0601.PropDtKindMasta, dataHBKB0601.PropIntCINmb) = False Then
                Return False
            End If
            '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 END

            'サポセン機器タイプマスタ取得
            '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 START
            'If commonLogicHBK.GetSapKikiTypeMastaData(Adapter, Cn, dataHBKB0601.PropDtSapKikiTypeMasta) = False Then
            '    Return False
            'End If
            If commonLogicHBK.GetSapKikiTypeMastaData(Adapter, Cn, dataHBKB0601.PropDtSapKikiTypeMasta, dataHBKB0601.PropIntCINmb) = False Then
                Return False
            End If
            '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 END

            'CIステータスマスタ取得
            If commonLogicHBK.GetCIStatusMastaData(Adapter, Cn, CI_TYPE_SUPORT, dataHBKB0601.PropDtCIStatusMasta) = False Then
                Return False
            End If

            '機器ステータスマスタ：機器利用形態取得
            If commonLogicHBK.GetKikiStatusMastaData(Adapter, Cn, KIKISTATEKBN_KIKI_RIYOKEITAI, dataHBKB0601.PropDtKikiStatusMasta_Kiki) = False Then
                Return False
            End If

            '機器ステータスマスタ：IP割当種類取得
            If commonLogicHBK.GetKikiStatusMastaData(Adapter, Cn, KIKISTATEKBN_IP_WARIATE, dataHBKB0601.PropDtKikiStatusMasta_IP) = False Then
                Return False
            End If

            'ソフトマスタ取得（オプションソフト）
            '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 START
            ''If commonLogicHBK.GetSoftMastaData(Adapter, Cn, SOFTKBN_OPTIONSOFT, dataHBKB0601.PropDtSoftMasta) = False Then
            ''    Return False
            ''End If
            If commonLogicHBK.GetSoftMastaData(Adapter, Cn, SOFTKBN_OPTIONSOFT, dataHBKB0601.PropDtSoftMasta, dataHBKB0601.PropIntCINmb) = False Then
                Return False
            End If
            '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 END


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainData(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

                If .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用データ取得
                        If GetMainDataForEdit(Adapter, Cn, dataHBKB0601) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用データ取得　※編集モードと同じ
                        If GetMainDataForEdit(Adapter, Cn, dataHBKB0601) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_REF Then      '参照モード

                    '参照モード用データ取得
                    If GetMainDataForRef(Adapter, Cn, dataHBKB0601) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用データ取得
                    If GetMainDataForRireki(Adapter, Cn, dataHBKB0601) = False Then
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
    ''' 【編集モード】初期表示用メインデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ取得先を保存用テーブル→標準テーブルへ変更（参照モードと同様にする）</p>
    ''' </para></remarks>
    Private Function GetMainDataForEdit(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'CI共通情報データ取得
            If GetCIInfoForEdit(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            '複数人利用データ取得
            If GetShareForEdit(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'オプションソフトデータ取得
            If GetOptSoftForEdit(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'セット機器データ取得
            If GetSetKikiForEdit(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            '登録理由履歴データ取得
            If GetRegReasonForEdit(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            '原因リンク履歴データ取得
            If GetCauseLinkForEdit(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'セット機器、インシデント内の機器情報取得
            If GetCINmbKikiInfo(Adapter, Cn, dataHBKB0601) = False Then
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
    ''' 【参照モード】初期表示用メインデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainDataForRef(ByVal Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'CI共通情報データ取得
            If GetCIInfoForRef(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            '複数人利用データ取得
            If GetShareForRef(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'オプションソフトデータ取得
            If GetOptSoftForRef(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'セット機器データ取得
            If GetSetKikiForRef(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            '登録理由履歴データ取得
            If GetRegReasonForRef(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            '原因リンク履歴データ取得
            If GetCauseLinkForRef(Adapter, Cn, dataHBKB0601) = False Then
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
    ''' 【編集モード】CI共通情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報データを取得する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ取得先を保存用テーブル→標準テーブルへ変更（参照モードと同様にする）</p>
    ''' </para></remarks>
    Private Function GetCIInfoForEdit(ByVal Adapter As NpgsqlDataAdapter, _
                                      ByVal Cn As NpgsqlConnection, _
                                      ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtCIInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKB0601.SetSelectCIInfoSqlForEdit(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtCIInfo)

            'データが取得できなかった場合、エラー
            If dtCIInfo.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & B0601_E001, TBNM_CI_INFO_TMP)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKB0601.PropDtCIInfo = dtCIInfo

            'セット機器ID
            If IsDBNull(dataHBKB0601.PropDtCIInfo.Rows(0).Item("SetKikiID")) Then
            Else
                dataHBKB0601.PropIntSetKikiID = dataHBKB0601.PropDtCIInfo.Rows(0).Item("SetKikiID")
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
            dtCIInfo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【参照モード】CI共通情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報データを取得する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetCIInfoForRef(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtCIInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKB0601.SetSelectCIInfoSqlForRef(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtCIInfo)

            'データが取得できなかった場合、エラー
            If dtCIInfo.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & B0601_E001, TBNM_CI_INFO_TB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKB0601.PropDtCIInfo = dtCIInfo
            'セット機器ID
            If IsDBNull(dataHBKB0601.PropDtCIInfo.Rows(0).Item("SetKikiID")) Then
            Else
                dataHBKB0601.PropIntSetKikiID = dataHBKB0601.PropDtCIInfo.Rows(0).Item("SetKikiID")
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
            dtCIInfo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】複数人利用データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>複数人利用データを取得する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ取得先を保存用テーブル→標準テーブルへ変更（参照モードと同様にする）</p>
    ''' </para></remarks>
    Private Function GetShareForEdit(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'データ初期化
            dataHBKB0601.PropDtShare.Clear()

            '取得用SQLの作成・設定
            If sqlHBKB0601.SetSelectShareSqlForEdit(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "複数人利用データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0601.PropDtShare)


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【参照モード】複数人利用データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>複数人利用データを取得する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetShareForRef(ByVal Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'データ初期化
            dataHBKB0601.PropDtShare.Clear()

            '取得用SQLの作成・設定
            If sqlHBKB0601.SetSelectShareSqlForRef(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "複数人利用データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0601.PropDtShare)


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】オプションソフトデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>オプションソフトデータを取得する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ取得先を保存用テーブル→標準テーブルへ変更（参照モードと同様にする）</p>
    ''' </para></remarks>
    Private Function GetOptSoftForEdit(ByVal Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'データ初期化
            dataHBKB0601.PropDtOptSoft.Clear()

            '取得用SQLの作成・設定
            If sqlHBKB0601.SetSelectOptSoftSqlForEdit(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "オプションソフトデータ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0601.PropDtOptSoft)

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【参照モード】オプションソフトデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>オプションソフトデータを取得する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetOptSoftForRef(ByVal Adapter As NpgsqlDataAdapter, _
                                      ByVal Cn As NpgsqlConnection, _
                                      ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'データ初期化
            dataHBKB0601.PropDtOptSoft.Clear()

            '取得用SQLの作成・設定
            If sqlHBKB0601.SetSelectOptSoftSqlForRef(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "オプションソフトデータ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0601.PropDtOptSoft)

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】セット機器データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>セット機器データを取得する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ取得先を保存用テーブル→標準テーブルへ変更（参照モードと同様にする）</p>
    ''' </para></remarks>
    Private Function GetSetKikiForEdit(ByVal Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'データ初期化
            dataHBKB0601.PropDtSetKiki.Clear()

            '取得用SQLの作成・設定
            If sqlHBKB0601.SetSelectSetKikiSqlForEdit(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "セット機器データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0601.PropDtSetKiki)


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【参照モード】セット機器データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>セット機器データを取得する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetSetKikiForRef(ByVal Adapter As NpgsqlDataAdapter, _
                                      ByVal Cn As NpgsqlConnection, _
                                      ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'データ初期化
            dataHBKB0601.PropDtSetKiki.Clear()

            '取得用SQLの作成・設定
            If sqlHBKB0601.SetSelectSetKikiSqlForRef(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "セット機器データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0601.PropDtSetKiki)


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】原因リンク履歴データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>原因リンク履歴データを取得する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ取得先を保存用テーブル→標準テーブルへ変更（参照モードと同様にする）</p>
    ''' </para></remarks>
    Private Function GetCauseLinkForEdit(ByVal Adapter As NpgsqlDataAdapter, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'データ初期化
            dataHBKB0601.PropDtMyCauseLink.Clear()

            '取得用SQLの作成・設定　※参照モードと同じ
            If sqlHBKB0601.SetSelectCauseLinkSqlForRef(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "原因リンク履歴取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0601.PropDtMyCauseLink)


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【参照モード】原因リンク履歴データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>原因リンク履歴データを取得する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetCauseLinkForRef(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'データ初期化
            dataHBKB0601.PropDtMyCauseLink.Clear()

            '取得用SQLの作成・設定
            If sqlHBKB0601.SetSelectCauseLinkSqlForRef(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "原因リンク履歴取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0601.PropDtMyCauseLink)


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】登録理由履歴データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録理由履歴データを取得する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ取得先を保存用テーブル→標準テーブルへ変更（参照モードと同様にする）</p>
    ''' </para></remarks>
    Private Function GetRegReasonForEdit(ByVal Adapter As NpgsqlDataAdapter, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'データ初期化
            dataHBKB0601.PropDtRireki.Clear()

            '取得用SQLの作成・設定　※参照モードと同じ
            If sqlHBKB0601.SetSelectRegReasonSqlForRef(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "登録理由履歴データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0601.PropDtRireki)

            '最大履歴番号を取得
            If dataHBKB0601.PropDtRireki.Rows.Count > 0 Then
                dataHBKB0601.PropIntRirekiNo = dataHBKB0601.PropDtRireki.Rows(0).Item("RirekiNo")
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
    ''' 【参照モード】登録理由履歴データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録理由履歴データを取得する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetRegReasonForRef(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'データ初期化
            dataHBKB0601.PropDtRireki.Clear()

            '取得用SQLの作成・設定
            If sqlHBKB0601.SetSelectRegReasonSqlForRef(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "登録理由履歴データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0601.PropDtRireki)

            '最大履歴番号を取得
            If dataHBKB0601.PropDtRireki.Rows.Count > 0 Then
                dataHBKB0601.PropIntRirekiNo = dataHBKB0601.PropDtRireki.Rows(0).Item("RirekiNo")
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードで初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainDataForRireki(ByVal Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'CI共通情報履歴データ取得
            If GetCIInfoForRireki(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            '複数人利用履歴データ取得
            If GetShareForRireki(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'オプションソフト履歴データ取得
            If GetOptSoftForRireki(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'セット機器履歴データ取得
            If GetSetKikiForRireki(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            '登録理由履歴データ取得
            If GetRegReasonForRireki(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            '原因リンク履歴データ取得
            If GetCauseLinkForRireki(Adapter, Cn, dataHBKB0601) = False Then
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報データを取得する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetCIInfoForRireki(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtCIInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKB0601.SetSelectCIInfoRSql(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報履歴データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtCIInfo)

            'データが取得できなかった場合、エラー
            If dtCIInfo.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & B0601_E001, TBNM_CI_INFO_RTB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKB0601.PropDtCIInfo = dtCIInfo
            'セット機器ID
            If IsDBNull(dataHBKB0601.PropDtCIInfo.Rows(0).Item("SetKikiID")) Then
            Else
                dataHBKB0601.PropIntSetKikiID = dataHBKB0601.PropDtCIInfo.Rows(0).Item("SetKikiID")
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
            dtCIInfo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【履歴モード】複数人利用履歴データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>複数人利用履歴データを取得する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetShareForRireki(ByVal Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'データ初期化
            dataHBKB0601.PropDtShare.Clear()

            '取得用SQLの作成・設定
            If sqlHBKB0601.SetSelectShareSqlForRireki(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "複数人利用履歴データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0601.PropDtShare)


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【履歴モード】オプションソフト履歴データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>オプションソフト履歴データを取得する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetOptSoftForRireki(ByVal Adapter As NpgsqlDataAdapter, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'データ初期化
            dataHBKB0601.PropDtOptSoft.Clear()

            '取得用SQLの作成・設定
            If sqlHBKB0601.SetSelectOptSoftSqlForRireki(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "オプションソフト履歴データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0601.PropDtOptSoft)


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【履歴モード】セット機器履歴データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>セット機器履歴データを取得する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetSetKikiForRireki(ByVal Adapter As NpgsqlDataAdapter, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'データ初期化
            dataHBKB0601.PropDtSetKiki.Clear()

            '取得用SQLの作成・設定
            If sqlHBKB0601.SetSelectSetKikiSqlForRireki(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "セット機器履歴データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0601.PropDtSetKiki)


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>原因リンク履歴データを取得する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetCauseLinkForRireki(ByVal Adapter As NpgsqlDataAdapter, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'データ初期化
            dataHBKB0601.PropDtMyCauseLink.Clear()

            '取得用SQLの作成・設定
            If sqlHBKB0601.SetSelectCauseLinkSqlForRireki(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "原因リンク履歴取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0601.PropDtMyCauseLink)


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録理由履歴データを取得する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetRegReasonForRireki(ByVal Adapter As NpgsqlDataAdapter, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'データ初期化
            dataHBKB0601.PropDtRireki.Clear()

            '取得用SQLの作成・設定
            If sqlHBKB0601.SetSelectRegReasonSqlForRireki(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "登録理由履歴データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0601.PropDtRireki)


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームコントロールに初期表示データを設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitDataToControl(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'ログイン／ロックデータ設定
            If SetDataToLoginAndLock(dataHBKB0601) = False Then
                Return False
            End If

            'ヘッダデータ設定
            If SetDataToHeader(dataHBKB0601) = False Then
                Return False
            End If

            'フッタデータ設定
            If SetDataToFooter(dataHBKB0601) = False Then
                Return False
            End If

            'タブコントロールデータ設定
            If SetDataToTabControl(dataHBKB0601) = False Then
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLock(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0601

                If .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetDataToLoginAndLockForEdit(dataHBKB0601) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetDataToLoginAndLockForRef(dataHBKB0601) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_REF Then        '参照モード


                    '参照モード用設定
                    If SetDataToLoginAndLockForRef(dataHBKB0601) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetDataToLoginAndLockForRireki(dataHBKB0601) = False Then
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
    ''' 【編集モード】ログイン／ロックデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForEdit(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0601.PropGrpLoginUser

                'ロック情報が取得できた場合
                If dataHBKB0601.PropDtCILock.Rows.Count > 0 Then
                    'ロック開始日時
                    Dim dtmLockTime As DateTime = Nothing  '編集開始日時
                    If dataHBKB0601.PropDtCILock.Rows(0).Item("EdiTime").ToString() <> "" Then
                        dtmLockTime = dataHBKB0601.PropDtCILock.Rows(0).Item("EdiTime")
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForRef(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0601.PropGrpLoginUser

                'ロック情報が取得できた場合
                If dataHBKB0601.PropDtCILock.Rows.Count > 0 Then
                    'ロック開始日時
                    Dim dtmLockTime As DateTime = Nothing
                    If dataHBKB0601.PropDtCILock.Rows(0).Item("EdiTime").ToString() <> "" Then
                        dtmLockTime = dataHBKB0601.PropDtCILock.Rows(0).Item("EdiTime")
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForRireki(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0601.PropGrpLoginUser

                'ロック開始日時 ※空文字のときはNothingを指定しないと落ちる
                Dim strLockTime As String = dataHBKB0601.PropStrEdiTime
                If dataHBKB0601.PropDtCILock IsNot Nothing AndAlso dataHBKB0601.PropDtCILock.Rows.Count > 0 Then
                    .PropLockDate = dataHBKB0601.PropDtCILock.Rows(0).Item("EdiTime")
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ヘッダデータを初期設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToHeader(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0601

                If .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetDataToHeaderForEdit(dataHBKB0601) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetDataToHeaderForRef(dataHBKB0601) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_REF Then        '参照モード


                    '参照モード用設定
                    If SetDataToHeaderForRef(dataHBKB0601) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetDataToHeaderForRireki(dataHBKB0601) = False Then
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
    ''' 【編集モード】ヘッダデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでヘッダデータを初期設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToHeaderForEdit(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0601

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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでヘッダデータを初期設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToHeaderForRef(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '編集モードヘッダデータ設定処理と同じ
            If SetDataToHeaderForEdit(dataHBKB0601) = False Then
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードでヘッダデータを初期設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToHeaderForRireki(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0601

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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フッタデータを初期設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToFooter(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0601

                If .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetDataToFooterForEdit(dataHBKB0601) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetDataToFooterForRef(dataHBKB0601) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_REF Then        '参照モード


                    '参照モード用設定
                    If SetDataToFooterForRef(dataHBKB0601) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetDataToFooterForRireki(dataHBKB0601) = False Then
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
    ''' 【編集モード】フッタデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでフッタデータを初期設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToFooterForEdit(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0601

                '履歴番号（更新ID）ラベル ※最新の番号をセット
                .PropLblRirekiNo.Text = .PropDtRireki.Rows(0).Item("RirekiNo").ToString()

                '理由テキストボックス
                .PropTxtRegReason.Text = .PropDtRireki.Rows(0).Item("RegReason")

                '原因リンク一覧
                .PropVwCauseLink.Sheets(0).DataSource = .PropDtMyCauseLink

                '履歴情報一覧
                .PropVwRegReason.Sheets(0).DataSource = .PropDtRireki


                '履歴情報の一行目(最新行)を青色に設定
                .PropVwRegReason.Sheets(0).Rows(0).BackColor = Color.SteelBlue
                '履歴情報の一行目(最新行)の文字色を白色に設定
                .PropVwRegReason.Sheets(0).Rows(0).ForeColor = Color.White

                'フラグによってサポセン情報コピーチェックボックスの表示・非表示を設定
                .PropChkCopyToIncident.Visible = .PropBlnIncident
                .PropChkCopyToSetKiki.Visible = .PropBlnSetKiki
                .PropLblIncident.Visible = .PropBlnIncident
                .PropLblSetKiki.Visible = .PropBlnSetKiki

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでフッタデータを初期設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToFooterForRef(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '※編集モードフッタデータ設定処理
            If SetDataToFooterForEdit(dataHBKB0601) = False Then
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードでフッタデータを初期設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToFooterForRireki(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '※編集モードフッタデータ設定処理
            If SetDataToFooterForEdit(dataHBKB0601) = False Then
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>タブコントロールデータを初期設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabControl(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'コンボボックス作成
            If CreateCmb(dataHBKB0601) = False Then
                Return False
            End If

            '基本情報タブデータ設定
            If SetDataToTabKhn(dataHBKB0601) = False Then
                Return False
            End If

            '利用情報タブデータ設定
            If SetDataToTabRiyo(dataHBKB0601) = False Then
                Return False
            End If

            'フリー入力タブデータ設定
            If SetDataToTabFree(dataHBKB0601) = False Then
                Return False
            End If

            '関係情報タブデータ設定
            If SetDataToTabRelation(dataHBKB0601) = False Then
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhn(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0601

                If .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetDataToTabKhnForEdit(dataHBKB0601) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetDataToTabKhnForRef(dataHBKB0601) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_REF Then        '参照モード


                    '参照モード用設定
                    If SetDataToTabKhnForRef(dataHBKB0601) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetDataToTabKhnForRireki(dataHBKB0601) = False Then
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
    ''' 【編集モード】基本情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhnForEdit(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0601

                '種別コンボボックス
                .PropCmbKind.SelectedValue = .PropDtCIInfo.Rows(0).Item("KindCD")

                '番号テキストボックス
                .PropTxtNum.Text = .PropDtCIInfo.Rows(0).Item("Num")

                '分類１～２テキストボックス
                .PropTxtClass1.Text = .PropDtCIInfo.Rows(0).Item("Class1")
                .PropTxtClass2.Text = .PropDtCIInfo.Rows(0).Item("Class2")

                '名称テキストボックス
                .PropTxtCINM.Text = .PropDtCIInfo.Rows(0).Item("CINM")

                '型番テキストボックス
                .PropTxtKataban.Text = .PropDtCIInfo.Rows(0).Item("Kataban")

                'タイプコンボボックス
                .PropCmbType.SelectedValue = .PropDtCIInfo.Rows(0).Item("TypeKbn")

                'ステータスコンボボックス
                .PropCmbCIStatus.SelectedValue = .PropDtCIInfo.Rows(0).Item("CIStatusCD")

                'サービスセンター保管機チェックボックス
                If .PropDtCIInfo.Rows(0).Item("SCHokanKbn") = SC_HOKANKBN_ON Then
                    .PropChkSCHokanKbn.Checked = True
                ElseIf .PropDtCIInfo.Rows(0).Item("SCHokanKbn") = SC_HOKANKBN_OFF Then
                    .PropChkSCHokanKbn.Checked = False
                End If

                '製造番号テキストボックス
                .PropTxtSerial.Text = .PropDtCIInfo.Rows(0).Item("Serial")

                'MACアドレス１～２
                .PropTxtMacAddress1.Text = .PropDtCIInfo.Rows(0).Item("MacAddress1")
                .PropTxtMacAddress2.Text = .PropDtCIInfo.Rows(0).Item("MacAddress2")

                'イメージ番号テキストボックス
                .PropTxtImageNmb.Text = .PropDtCIInfo.Rows(0).Item("ImageNmb")

                'メモリー容量テキストボックス
                .PropTxtMemorySize.Text = .PropDtCIInfo.Rows(0).Item("MemorySize")

                'サポセン固定資産番号テキストボックス
                .PropTxtSCKikiFixNmb.Text = .PropDtCIInfo.Rows(0).Item("SCKikiFixNmb")

                'リース期限日（機器）DateTimePickerEx
                .PropDtpLeaseUpDT_Kiki.txtDate.Text = .PropDtCIInfo.Rows(0).Item("LeaseUpDT_Kiki")

                '付属品テキストボックス
                .PropTxtFuzokuhin.Text = .PropDtCIInfo.Rows(0).Item("Fuzokuhin")

                '機器状態テキストボックス
                .PropTxtKikiState.Text = .PropDtCIInfo.Rows(0).Item("KikiState")

                '説明テキストボックス
                .PropTxtCINaiyo.Text = .PropDtCIInfo.Rows(0).Item("CINaiyo")

                '導入番号テキストボックス
                .PropTxtIntroductNmb.Text = .PropDtCIInfo.Rows(0).Item("IntroductNmb").ToString()

                '導入開始日DateTimePickerEx
                .PropDtpIntroductStDT.txtDate.Text = .PropDtCIInfo.Rows(0).Item("IntroductStDT").ToString()

                'メーカー無償保証期間テキストボックス
                .PropTxtMakerHosyoTerm.Text = .PropDtCIInfo.Rows(0).Item("MakerHosyoTerm").ToString()

                'EOSテキストボックス
                .PropTxtEOS.Text = .PropDtCIInfo.Rows(0).Item("EOS").ToString()

                '導入タイプコンボボックス
                .PropCmbIntroductKbn.SelectedValue = .PropDtCIInfo.Rows(0).Item("IntroductKbn").ToString()

                'リース会社テキストボックス
                .PropTxtLeaseCompany.Text = .PropDtCIInfo.Rows(0).Item("LeaseCompany").ToString()

                '廃棄予定日DateTimePickerEx
                .PropDtpDelScheduleDT.txtDate.Text = .PropDtCIInfo.Rows(0).Item("DelScheduleDT").ToString()

                'リース期限日（導入）DateTimePickerEx
                .PropDtpLeaseUpDT_Int.txtDate.Text = .PropDtCIInfo.Rows(0).Item("LeaseUpDT_Int").ToString()

                '保証書コンボボックス
                .PropCmbHosyoUmu.SelectedValue = .PropDtCIInfo.Rows(0).Item("HosyoUmu").ToString()

                '導入廃棄完了チェックボックス
                If .PropDtCIInfo.Rows(0).Item("IntroductDelKbn").ToString() = INTRODUTDEL_KBN_ON Then
                    .PropChkIntroductDelKbn.Checked = True
                Else
                    .PropChkIntroductDelKbn.Checked = False
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhnForRef(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '編集モード基本情報タブデータ設定処理と同じ
            If SetDataToTabKhnForEdit(dataHBKB0601) = False Then
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードで基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhnForRireki(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0601

                '編集モード基本情報タブデータ設定処理と同じ
                If SetDataToTabKhnForEdit(dataHBKB0601) = False Then
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
    ''' 【共通】利用情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて利用情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabRiyo(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0601

                If .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetDataToTabRiyoForEdit(dataHBKB0601) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetDataToTabRiyoForRef(dataHBKB0601) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_REF Then        '参照モード


                    '参照モード用設定
                    If SetDataToTabRiyoForRef(dataHBKB0601) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetDataToTabRiyoForRireki(dataHBKB0601) = False Then
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
    ''' 【編集モード】利用情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで利用情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabRiyoForEdit(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

                '種別ラベル
                .PropLblKindNM.Text = .PropCmbKind.Text

                '番号ラベル
                .PropLblNum_Riyo.Text = .PropTxtNum.Text

                'ユーザーIDテキストボックス
                .PropTxtUsrID.Text = .PropDtCIInfo.Rows(0).Item("UsrID")

                'ユーザー氏名テキストボックス
                .PropTxtUsrNM.Text = .PropDtCIInfo.Rows(0).Item("UsrNM")

                'ユーザーメールアドレステキストボックス
                .PropTxtUsrMailAdd.Text = .PropDtCIInfo.Rows(0).Item("UsrMailAdd")

                'ユーザー電話番号テキストボックス
                .PropTxtUsrTel.Text = .PropDtCIInfo.Rows(0).Item("UsrTel")

                'ユーザー所属局テキストボックス
                .PropTxtUsrKyokuNM.Text = .PropDtCIInfo.Rows(0).Item("UsrKyokuNM")

                'ユーザー所属部署テキストボックス
                .PropTxtUsrBusyoNM.Text = .PropDtCIInfo.Rows(0).Item("UsrBusyoNM")

                'ユーザー所属会社テキストボックス
                .PropTxtUsrCompany.Text = .PropDtCIInfo.Rows(0).Item("UsrCompany")

                'ユーザー連絡先テキストボックス
                .PropTxtUsrContact.Text = .PropDtCIInfo.Rows(0).Item("UsrContact")

                'ユーザー番組／部屋テキストボックス
                .PropTxtUsrRoom.Text = .PropDtCIInfo.Rows(0).Item("UsrRoom")

                '複数人利用スプレッド
                .PropVwShare.DataSource = .PropDtShare

                'レンタル期間（FROM～TO）DateTimePickerEx
                .PropDtpRentalStDT.txtDate.Text = .PropDtCIInfo.Rows(0).Item("RentalStDT")
                .PropDtpRentalEdDT.txtDate.Text = .PropDtCIInfo.Rows(0).Item("RentalEdDT")

                '最終お知らせ日DateTimePickerEx
                .PropDtpLastInfoDT.txtDate.Text = .PropDtCIInfo.Rows(0).Item("LastInfoDT")

                '作業の元テキストボックス
                .PropTxtWorkFromNmb.Text = .PropDtCIInfo.Rows(0).Item("WorkFromNmb")

                '機器利用形態コンボボックス
                .PropCmbKikiUse.SelectedValue = .PropDtCIInfo.Rows(0).Item("KikiUseCD")

                'IP割当種類コンボボックス
                .PropCmbIPUse.SelectedValue = .PropDtCIInfo.Rows(0).Item("IPUseCD")

                '固定IPテキストボックス
                .PropTxtFixedIP.Text = .PropDtCIInfo.Rows(0).Item("FixedIP")

                'オプションソフトスプレッド
                .PropVwOptSoft.DataSource = .PropDtOptSoft

                'セット機器スプレッド
                .PropVwSetKiki.DataSource = .PropDtSetKiki

                '管理局テキストボックス
                .PropTxtManageKyokuNM.Text = .PropDtCIInfo.Rows(0).Item("ManageKyokuNM")

                '管理部署テキストボックス
                .PropTxtManageBusyoNM.Text = .PropDtCIInfo.Rows(0).Item("ManageBusyoNM")

                '設置局テキストボックス
                .PropTxtSetKyokuNM.Text = .PropDtCIInfo.Rows(0).Item("SetKyokuNM")

                '設置部署テキストボックス
                .PropTxtSetBusyoNM.Text = .PropDtCIInfo.Rows(0).Item("SetBusyoNM")

                '設置番組／部屋テキストボックス
                .PropTxtSetRoom.Text = .PropDtCIInfo.Rows(0).Item("SetRoom")

                '設置建物テキストボックス
                .PropTxtSetBuil.Text = .PropDtCIInfo.Rows(0).Item("SetBuil")

                '設置フロアテキストボックス
                .PropTxtSetFloor.Text = .PropDtCIInfo.Rows(0).Item("SetFloor")

                '設置デスクNoテキストボックス
                .PropTxtSetDeskNo.Text = .PropDtCIInfo.Rows(0).Item("SetDeskNo")

                '設置LANケーブル長さテキストボックス
                .PropTxtSetLANLength.Text = .PropDtCIInfo.Rows(0).Item("SetLANLength")

                '設置LANケーブル番号テキストボックス
                .PropTxtSetLANNum.Text = .PropDtCIInfo.Rows(0).Item("SetLANNum")

                '情報コンセント・SWテキストボックス
                .PropTxtSetSocket.Text = .PropDtCIInfo.Rows(0).Item("SetSocket")


                '利用情報タブ全スプレッド非活性フラグがONの場合、スプレッド非活性処理
                If .PropBlnTabRiyoVwAllUnabled = True Then
                    If SetVwUnabledOnTabRiyo(dataHBKB0601) = False Then
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
    ''' 【参照モード】利用情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで利用情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabRiyoForRef(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '編集モード利用情報タブデータ設定処理と同じ
            If SetDataToTabRiyoForEdit(dataHBKB0601) = False Then
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードで利用情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabRiyoForRireki(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '編集モード利用情報タブデータ設定処理と同じ
            If SetDataToTabRiyoForEdit(dataHBKB0601) = False Then
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
    ''' 【履歴モード】利用情報タブ一覧非活性処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>利用情報タブの各一覧を非活性にする
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetVwUnabledOnTabRiyo(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

                '複数人利用一覧
                If commonLogicHBK.SetSpreadUnabled(.PropVwShare, 0) = False Then
                    Return False
                End If

                'オプションソフト一覧
                If commonLogicHBK.SetSpreadUnabled(.PropVwOptSoft, 0) = False Then
                    Return False
                End If

                'セット機器一覧
                If commonLogicHBK.SetSpreadUnabled(.PropVwSetKiki, 0) = False Then
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFree(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0601

                If .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetDataToTabFreeForEdit(dataHBKB0601) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetDataToTabFreeForRef(dataHBKB0601) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_REF Then        '参照モード


                    '参照モード用設定
                    If SetDataToTabFreeForRef(dataHBKB0601) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetDataToTabFreeForRireki(dataHBKB0601) = False Then
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
    ''' 【編集モード】フリー入力タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFreeForEdit(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0601

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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFreeForRef(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '編集モードフリー入力タブデータ設定処理と同じ
            If SetDataToTabFreeForEdit(dataHBKB0601) = False Then
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードでフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFreeForRireki(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '編集モードフリー入力タブデータ設定処理と同じ
            If SetDataToTabFreeForEdit(dataHBKB0601) = False Then
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて関係情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabRelation(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0601

                If .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード


                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetDataToTabRelationForEdit(dataHBKB0601) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetDataToTabRelationForRef(dataHBKB0601) = False Then
                            Return False
                        End If

                    End If


                ElseIf .PropStrProcMode = PROCMODE_REF Then        '参照モード


                    '参照モード用設定
                    If SetDataToTabRelationForRef(dataHBKB0601) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

                    '履歴モード用設定
                    If SetDataToTabRelationForRireki(dataHBKB0601) = False Then
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
    ''' 【編集モード】関係情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで関係情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabRelationForEdit(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0601

                'CIオーナー名テキストボックス
                .PropTxtCIOwnerNM.Text = .PropDtCIInfo.Rows(0).Item("GroupNM").ToString()

                'CIオーナーCDラベル
                .PropLblCIOwnerCD.Text = .PropDtCIInfo.Rows(0).Item("CIOwnerCD")

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで関係情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabRelationForRef(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '編集モード関係情報タブデータ設定処理と同じ
            If SetDataToTabRelationForEdit(dataHBKB0601) = False Then
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで関係情報タブデータを初期設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabRelationForRireki(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0601

                'CIオーナー名テキストボックス
                .PropTxtCIOwnerNM.Text = .PropDtCIInfo.Rows(0).Item("GroupNM").ToString()

                'CIオーナーCDラベル
                .PropLblCIOwnerCD.Text = .PropDtCIInfo.Rows(0).Item("CIOwnerCD")

            End With



            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のコンボボックスを作成する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateCmb(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601

                '種別コンボボックス作成
                If commonLogic.SetCmbBox(.PropDtKindMasta, .PropCmbKind, False) = False Then
                    Return False
                End If

                'タイプコンボボックス作成
                If commonLogic.SetCmbBox(.PropDtSapKikiTypeMasta, .PropCmbType, False) = False Then
                    Return False
                End If

                'CIステータスコンボボックス作成
                If commonLogic.SetCmbBox(.PropDtCIStatusMasta, .PropCmbCIStatus, False) = False Then
                    Return False
                End If

                '導入タイプコンボボックス作成
                If commonLogic.SetCmbBox(IntroductKbn, .PropCmbIntroductKbn) = False Then
                    Return False
                End If

                '保証書コンボボックス作成
                If commonLogic.SetCmbBox(HosyoUmu, .PropCmbHosyoUmu) = False Then
                    Return False
                End If

                '機器利用形態コンボボックス作成
                If commonLogic.SetCmbBox(.PropDtKikiStatusMasta_Kiki, .PropCmbKikiUse, True, "", "") = False Then
                    Return False
                End If

                'IP割当種類コンボボックス作成
                If commonLogic.SetCmbBox(.PropDtKikiStatusMasta_IP, .PropCmbIPUse, True, "", "") = False Then
                    Return False
                End If

                'スプレッドコンボボックスセル作成
                If SetCmbCellForVw(dataHBKB0601) = False Then
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
    ''' 【共通】スプレッドコンボボックスセル作成処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>コンボボックスセルを作成し、スプレッドにセットする
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetCmbCellForVw(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intItemCnt As Integer   '配列データ数

        Try
            With dataHBKB0601

                'オプションソフトコンボボックスセル
                With .PropDtSoftMasta

                    '配列のデータ数を取得
                    intItemCnt = .Rows.Count

                    '値とテキスト格納用配列宣言
                    Dim strAryItemData_OptSoft(intItemCnt) As String                        '値（コード）の配列
                    Dim strAryItems_OptSoft(intItemCnt) As String                           'テキストの配列

                    'データ数分繰り返し、配列に値をセット
                    For i As Integer = 0 To .Rows.Count
                        If i = 0 Then
                            '1行目には空行をセット
                            strAryItemData_OptSoft(i) = ""
                            strAryItems_OptSoft(i) = ""
                        Else
                            '2行目以降データをセット
                            strAryItemData_OptSoft(i) = .Rows(i - 1).Item(0).ToString()     '値（コード）
                            strAryItems_OptSoft(i) = .Rows(i - 1).Item(1)                   'テキスト
                        End If
                    Next

                    'コンボボックスセルのデータをセット
                    With dataHBKB0601.PropCelOptSoft
                        .ItemData = strAryItemData_OptSoft                                  '値（コード）
                        .Items = strAryItems_OptSoft                                        'テキスト
                    End With

                End With

                '★★--------------
                '作成したセルをスプレッドにセット
                '.PropVwOptSoft.Sheets(0).Columns(COL_OPTSOFT_SOFTNM).CellType = .PropCelOptSoft 'オプションソフト

                '作成したセルをスプレッドにセット
                .PropVwOptSoft.Sheets(0).Columns(COL_OPTSOFT_SOFTCD).CellType = .PropCelOptSoft 'オプションソフト
                '★★--------------

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>各スプレッド（一覧）を初期設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetVwControl(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0601

                '複数人利用一覧
                With .PropVwShare.Sheets(0)
                    .Columns(COL_SHARE_USERID).DataField = "UsrID"
                    .Columns(COL_SHARE_USERNM).DataField = "UsrNM"

                    .Columns(COL_SHARE_REGDT).DataField = "RegDT"           '登録日時
                    .Columns(COL_SHARE_REGGPCD).DataField = "RegGrpCD"      '登録グループコード
                    .Columns(COL_SHARE_REGUSERID).DataField = "RegID"       '登録ユーザID
                    .Columns(COL_SHARE_REGDT).Visible = False               '登録日時
                    .Columns(COL_SHARE_REGGPCD).Visible = False             '登録グループコード
                    .Columns(COL_SHARE_REGUSERID).Visible = False           '登録ユーザID
                End With

                'オプションソフト一覧
                With .PropVwOptSoft.Sheets(0)
                    '★★--------------
                    '.Columns(COL_OPTSOFT_SOFTNM).DataField = "SoftNM"
                    .Columns(COL_OPTSOFT_SOFTCD).DataField = "SoftCD" 'オプションソフトコード
                    '★★--------------
                    .Columns(COL_OPTSOFT_REGDT).DataField = "RegDT"           '登録日時
                    .Columns(COL_OPTSOFT_REGGPCD).DataField = "RegGrpCD"      '登録グループコード
                    .Columns(COL_OPTSOFT_REGUSERID).DataField = "RegID"       '登録ユーザID
                    .Columns(COL_OPTSOFT_REGDT).Visible = False               '登録日時
                    .Columns(COL_OPTSOFT_REGGPCD).Visible = False             '登録グループコード
                    .Columns(COL_OPTSOFT_REGUSERID).Visible = False           '登録ユーザID
                End With

                'セット機器一覧
                With .PropVwSetKiki.Sheets(0)
                    .Columns(COL_SETKIKI_SETKIKINO).DataField = "SetKikiNo"
                    .Columns(COL_SETKIKI_ID).DataField = "SetKikiID"
                    '隠し列非表示
                    .Columns(COL_SETKIKI_ID).Visible = False
                End With

                '原因リンク一覧
                With .PropVwCauseLink.Sheets(0)
                    .Columns(COL_CAUSELINK_KBN_NMR).DataField = "ProcessKbnNMR"
                    .Columns(COL_CAUSELINK_NO).DataField = "MngNmb"
                    .Columns(COL_CAUSELINK_KBN).DataField = "ProcessKbn"
                    '隠し列非表示
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システムコード保持用の隠しラベルを非表示にする
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetHiddenLabelUnvisible(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0601

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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>他のユーザによりロックが解除された場合に編集中の入力内容をログに出力する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function OutputUnlockLog(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strPlmList As New List(Of String)       'フォーマット埋込用パラメータ配列
        Dim strLogFilePath As String = ""           'ログファイルパス
        Dim strLogFileName As String                'ログファイル名
        Dim strOutputDir As String                  'ログ出力フォルダ
        Dim strText_Share As String = ""            '複数人利用パラメータ文
        Dim strText_OptSoft As String = ""          'オプションソフトパラメータ文
        Dim strText_SetKiki As String = ""          'セット機器パラメータ文
        Dim sw As StreamWriter = Nothing            'ファイル書込用クラス

        Try
            With dataHBKB0601

                '書込用テキスト作成
                'CI基本情報
                strPlmList.Add(.PropLblCINmb.Text)                                                          'CI番号
                strPlmList.Add(.PropLblCIKbnNM.Text)                                                        'CI種別
                '基本情報
                strPlmList.Add(.PropCmbKind.Text)                                                           '種別
                strPlmList.Add(.PropTxtNum.Text)                                                            '番号
                strPlmList.Add(.PropTxtClass1.Text)                                                         '分類1
                strPlmList.Add(.PropTxtClass2.Text)                                                         '分類2（メーカー）
                strPlmList.Add(.PropTxtCINM.Text)                                                           '名称（機種）
                strPlmList.Add(.PropTxtKataban.Text)                                                        '型番
                strPlmList.Add(.PropCmbType.Text)                                                           'タイプ
                strPlmList.Add(.PropCmbCIStatus.Text)                                                       'ステータス
                If .PropChkSCHokanKbn.Checked = True Then                                                   'サービスセンター保管機
                    strPlmList.Add(SC_HOKANKBN_ON_NM)
                Else
                    strPlmList.Add(SC_HOKANKBN_OFF_NM)
                End If
                strPlmList.Add(.PropTxtSerial.Text)                                                         '製造番号
                strPlmList.Add(.PropTxtMacAddress1.Text)                                                    'MACアドレス1
                strPlmList.Add(.PropTxtMacAddress2.Text)                                                    'MACアドレス2
                strPlmList.Add(.PropTxtImageNmb.Text)                                                       'イメージ番号
                strPlmList.Add(.PropTxtMemorySize.Text)                                                     'メモリ容量
                strPlmList.Add(.PropTxtSCKikiFixNmb.Text)                                                   'サポセン固定資産番号
                strPlmList.Add(.PropDtpLeaseUpDT_Kiki.txtDate.Text)                                         'リース期限日(機器)
                strPlmList.Add(.PropTxtFuzokuhin.Text)                                                      '付属品
                strPlmList.Add(.PropTxtKikiState.Text)                                                      '機器状態
                strPlmList.Add(.PropTxtCINaiyo.Text)                                                        '説明
                '利用者情報
                strPlmList.Add(.PropTxtUsrID.Text)                                                          'ユーザーID
                strPlmList.Add(.PropTxtUsrNM.Text)                                                          'ユーザー氏名
                strPlmList.Add(.PropTxtUsrMailAdd.Text)                                                     'ユーザーメールアドレス
                strPlmList.Add(.PropTxtUsrTel.Text)                                                         'ユーザー電話番号
                strPlmList.Add(.PropTxtUsrKyokuNM.Text)                                                     'ユーザー所属局
                strPlmList.Add(.PropTxtUsrBusyoNM.Text)                                                     'ユーザー所属部署
                strPlmList.Add(.PropTxtUsrCompany.Text)                                                     'ユーザー所属会社
                strPlmList.Add(.PropTxtUsrContact.Text)                                                     'ユーザー連絡先
                strPlmList.Add(.PropTxtUsrRoom.Text)                                                        'ユーザー番組/部屋
                If .PropVwShare.Sheets(0).RowCount > 0 Then                                                 '複数人利用
                    With .PropVwShare.Sheets(0)
                        '一覧行数分繰り返し、パラメータ文を作成
                        For i As Integer = 0 To .RowCount - 1
                            '以下の書式で出力
                            'n.<ユーザーID> <ユーザー氏名>
                            Dim strUsrID As String = .Cells(i, COL_SHARE_USERID).Value
                            Dim strUsrNM As String = .Cells(i, COL_SHARE_USERNM).Value
                            strText_Share &= (i + 1).ToString() & "." & strUsrID & " " & strUsrNM
                            '最終行以外は改行コード追加
                            If i < .RowCount - 1 Then
                                strText_Share &= vbCrLf
                            End If
                        Next
                    End With
                End If
                strPlmList.Add(strText_Share)
                strPlmList.Add(.PropDtpRentalStDT.txtDate.Text & "～" & .PropDtpRentalEdDT.txtDate.Text)    'レンタル期間
                strPlmList.Add(.PropDtpLastInfoDT.txtDate.Text)                                             '最終お知らせ日
                '機器利用情報
                strPlmList.Add(.PropTxtWorkFromNmb.Text)                                                    '作業の元
                strPlmList.Add(.PropCmbKikiUse.Text)                                                        '機器利用形態
                strPlmList.Add(.PropTxtFixedIP.Text)                                                        '固定IP
                strPlmList.Add(.PropCmbIPUse.Text)                                                          'IP割当種類
                If .PropVwOptSoft.Sheets(0).RowCount > 0 Then                                               'オプションソフト
                    With .PropVwOptSoft.Sheets(0)
                        '一覧行数分繰り返し、パラメータ文を作成
                        For i As Integer = 0 To .RowCount - 1
                            '以下の書式で出力
                            'n.<オプションソフト>
                            '★★-------------------------
                            'Dim strOptSoft As String = .Cells(i, COL_OPTSOFT_SOFTNM).Text
                            Dim strOptSoft As String = .Cells(i, COL_OPTSOFT_SOFTCD).Text
                            '★★-------------------------
                            strText_OptSoft &= (i + 1).ToString() & "." & strOptSoft
                            '最終行以外は改行コード追加
                            If i < .RowCount - 1 Then
                                strText_OptSoft &= vbCrLf
                            End If
                        Next
                    End With
                End If
                strPlmList.Add(strText_OptSoft)
                If .PropVwSetKiki.Sheets(0).RowCount > 0 Then                                               'セット機器
                    With .PropVwSetKiki.Sheets(0)
                        '一覧行数分繰り返し、パラメータ文を作成
                        For i As Integer = 0 To .RowCount - 1
                            '以下の書式で出力
                            'n.<セット機器>
                            Dim strSetKiki As String = .Cells(i, COL_SETKIKI_SETKIKINO).Value
                            strText_SetKiki &= (i + 1).ToString() & "." & strSetKiki
                            '最終行以外は改行コード追加
                            If i < .RowCount - 1 Then
                                strText_SetKiki &= vbCrLf
                            End If
                        Next
                    End With
                End If
                strPlmList.Add(strText_SetKiki)
                '管理者情報
                strPlmList.Add(.PropTxtManageKyokuNM.Text)                                                  '管理局
                strPlmList.Add(.PropTxtManageBusyoNM.Text)                                                  '管理部署
                '設置情報
                strPlmList.Add(.PropTxtSetKyokuNM.Text)                                                     '設置局
                strPlmList.Add(.PropTxtSetBusyoNM.Text)                                                     '設置部署
                strPlmList.Add(.PropTxtSetRoom.Text)                                                        '設置番組／部屋
                strPlmList.Add(.PropTxtSetBuil.Text)                                                        '設置建物
                strPlmList.Add(.PropTxtSetFloor.Text)                                                       '設置フロア
                strPlmList.Add(.PropTxtSetDeskNo.Text)                                                      '設置デスクNo
                strPlmList.Add(.PropTxtSetLANLength.Text)                                                   '設置LANケーブル長さ
                strPlmList.Add(.PropTxtSetLANNum.Text)                                                      '設置LANケーブル番号
                strPlmList.Add(.PropTxtSetSocket.Text)                                                      '情報コンセント・SW
                'フリー入力情報
                strPlmList.Add(.PropTxtBIko1.Text)                                                          'フリーテキスト１
                strPlmList.Add(.PropTxtBIko2.Text)                                                          'フリーテキスト２
                strPlmList.Add(.PropTxtBIko3.Text)                                                          'フリーテキスト３
                strPlmList.Add(.PropTxtBIko4.Text)                                                          'フリーテキスト４
                strPlmList.Add(.PropTxtBIko5.Text)                                                          'フリーテキスト５
                If .PropChkFreeFlg1.Checked = True Then                                                     'フリーフラグ１
                    strPlmList.Add(FREE_FLG_ON_NM)
                Else
                    strPlmList.Add(FREE_FLG_OFF_NM)
                End If
                If .PropChkFreeFlg2.Checked = True Then                                                     'フリーフラグ２
                    strPlmList.Add(FREE_FLG_ON_NM)
                Else
                    strPlmList.Add(FREE_FLG_OFF_NM)
                End If
                If .PropChkFreeFlg3.Checked = True Then                                                     'フリーフラグ３
                    strPlmList.Add(FREE_FLG_ON_NM)
                Else
                    strPlmList.Add(FREE_FLG_OFF_NM)
                End If
                If .PropChkFreeFlg4.Checked = True Then                                                     'フリーフラグ４
                    strPlmList.Add(FREE_FLG_ON_NM)
                Else
                    strPlmList.Add(FREE_FLG_OFF_NM)
                End If
                If .PropChkFreeFlg5.Checked = True Then                                                     'フリーフラグ５
                    strPlmList.Add(FREE_FLG_ON_NM)
                Else
                    strPlmList.Add(FREE_FLG_OFF_NM)
                End If
                '関係情報
                strPlmList.Add(.PropTxtCIOwnerNM.Text)                                                      'CIオーナー名


                'ログ出力フォルダ設定
                strOutputDir = Path.Combine(Application.StartupPath, OUTPUT_DIR_UNLOCKEDLOG)

                'ログファイル名設定
                strLogFileName = Format(DateTime.Parse(.PropDtCILock.Rows(0).Item("SysTime")), "yyyyMMddHHmmss") & ".log"

                'ファイル出力を実行し、出力ファイルパスを取得
                If commonLogicHBK.OutputLogFromTextFormat(strPlmList, strLogFileName, _
                                                          FORMAT_DIR_UNLOCKEDLOG, _
                                                          FILE_UNLOCKLOG_SUPPORT, _
                                                          strOutputDir, _
                                                          strLogFilePath) = False Then
                    Return False
                End If

                'データクラスにメッセージをセット
                dataHBKB0601.PropStrBeUnlockedMsg = String.Format(HBK_W001, strLogFilePath)

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
    ''' 【共通】セット機器空行追加処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>セット機器一覧に空行を1行追加する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function AddRowSetKiki(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601.PropVwSetKiki.Sheets(0)

                '最終行に空行を1行追加
                .Rows.Add(.RowCount, 1)
                '追加行にフォーカスをセット
                If commonLogicHBK.SetFocusOnVwRow(dataHBKB0601.PropVwSetKiki, 0, .RowCount, 0, 1, .ColumnCount) = False Then
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
    ''' 【共通】セット機器選択行削除処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>セット機器の選択行を削除（Remove）する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowSetKiki(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号

        Try
            With dataHBKB0601.PropVwSetKiki.Sheets(0)

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
    ''' 【共通】オプションソフト空行追加処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>オプションソフトに空行を1行追加する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function AddRowOptSoft(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0601.PropVwOptSoft.Sheets(0)

                '最終行に空行を1行追加
                .Rows.Add(.RowCount, 1)
                '追加行にフォーカスをセット
                If commonLogicHBK.SetFocusOnVwRow(dataHBKB0601.PropVwOptSoft, 0, .RowCount, 0, 1, .ColumnCount) = False Then
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
    ''' 【共通】オプションソフト選択行削除処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>オプションソフト一覧の選択行を削除（Remove）する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowOptSoft(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号

        Try
            With dataHBKB0601.PropVwOptSoft.Sheets(0)

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
    ''' 【共通】複数人利用データ設定処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>複数人利用一覧にサブ検索で選択されたグループを設定する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetUserToVwShare(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intNewRowNo As Integer          '新規追加行番号
        Dim blnAddFlg As Boolean = True     '追加フラグ

        Try
            With dataHBKB0601

                'サブ検索画面で1件以上選択された場合に値を設定
                If .PropDtResultSub IsNot Nothing Then

                    '選択データ数分繰り返し、関係者情報一覧に行を追加する
                    For i As Integer = 0 To .PropDtResultSub.Rows.Count - 1

                        '追加フラグ初期化
                        blnAddFlg = True

                        'グループが既に設定済でない場合のみ追加
                        For j As Integer = 0 To .PropVwShare.Sheets(0).RowCount - 1

                            '既に設定済みの場合は追加フラグをOFFにして処理を抜ける
                            If .PropDtResultSub.Rows(i).Item("EndUsrID") = _
                                .PropVwShare.Sheets(0).Cells(j, COL_SHARE_USERID).Value Then
                                blnAddFlg = False
                                Exit For
                            End If

                        Next

                        '追加フラグがONの場合のみ追加処理を行う
                        If blnAddFlg = True Then

                            '追加行番号取得
                            intNewRowNo = .PropVwShare.Sheets(0).Rows.Count

                            '新規行追加
                            .PropVwShare.Sheets(0).Rows.Add(intNewRowNo, 1)

                            'サブ検索画面での選択値を設定
                            .PropVwShare.Sheets(0).Cells(intNewRowNo, COL_SHARE_USERID).Value = _
                                .PropDtResultSub.Rows(i).Item("EndUsrID")                                       'ユーザーID
                            .PropVwShare.Sheets(0).Cells(intNewRowNo, COL_SHARE_USERNM).Value = _
                                .PropDtResultSub.Rows(i).Item("EndUsrNM")                                       'ユーザー名

                        End If

                    Next

                    '最終追加行にフォーカスをセット
                    If commonLogicHBK.SetFocusOnVwRow(.PropVwShare, _
                                                      0, .PropVwShare.Sheets(0).RowCount, 0, _
                                                      1, .PropVwShare.Sheets(0).ColumnCount) = False Then
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
    ''' 【共通】複数人利用選択行削除処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>複数人利用一覧の選択行を削除する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowShare(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号

        Try
            With dataHBKB0601.PropVwShare.Sheets(0)

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
    ''' 【編集モード】ロック解除チェック処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータのロックが解除されていないかチェックする
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckUnlock(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnBeUnocked As Boolean = False       'ロックフラグ

        Try

            With dataHBKB0601

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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録項目の入力チェックを行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckInputValue(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strMacAddCheck As String    'MACアドレス書式チェック用変数

        Try
            With dataHBKB0601

                'タイプ
                With .PropCmbType
                    If .Enabled = True Then
                        '未入力の場合、エラー
                        If .SelectedValue = "" Then
                            'エラーメッセージ設定
                            puErrMsg = B0601_E002
                            'タブを基本情報タブに設定
                            dataHBKB0601.PropTbInput.SelectedIndex = TAB_KHN
                            'フォーカス設定
                            .Focus()
                            'エラーを返す
                            Return False
                        End If
                    End If
                End With

                'ステータス
                With .PropCmbCIStatus
                    If .Enabled = True Then
                        '未入力の場合、エラー
                        If .SelectedValue = "" Then
                            'エラーメッセージ設定
                            puErrMsg = B0601_E003
                            'タブを基本情報タブに設定
                            dataHBKB0601.PropTbInput.SelectedIndex = TAB_KHN
                            'フォーカス設定
                            .Focus()
                            'エラーを返す
                            Return False
                        End If
                    End If
                End With

                'MACアドレス１
                With .PropTxtMacAddress1
                    If .ReadOnly = False Then
                        '入力のある場合、チェックを行う
                        If .Text <> "" Then
                            ':と-を削除し、変数に格納する
                            strMacAddCheck = .Text.Replace("-", "").Replace(":", "")
                            '12桁以外の場合、または半角英数以外の場合エラー
                            If Len(strMacAddCheck) <> 12 Or commonValidation.IsHalfChar(strMacAddCheck) = False Then
                                'エラーメッセージ設定
                                puErrMsg = B0601_E004
                                'タブを利用情報タブに設定
                                dataHBKB0601.PropTbInput.SelectedIndex = TAB_KHN
                                'フォーカス設定
                                .Focus()
                                .SelectAll()
                                'エラーを返す
                                Return False
                            End If
                        End If
                    End If
                End With

                'MACアドレス２
                With .PropTxtMacAddress2
                    If .ReadOnly = False Then
                        '入力のある場合、チェックを行う
                        If .Text <> "" Then
                            ':と-を削除し、変数に格納する
                            strMacAddCheck = .Text.Replace("-", "").Replace(":", "")
                            '12桁以外の場合、または半角英数以外の場合エラー
                            If Len(strMacAddCheck) <> 12 Or commonValidation.IsHalfChar(strMacAddCheck) = False Then
                                'エラーメッセージ設定
                                puErrMsg = B0601_E005
                                'タブを利用情報タブに設定
                                dataHBKB0601.PropTbInput.SelectedIndex = TAB_KHN
                                'フォーカス設定
                                .Focus()
                                .SelectAll()
                                'エラーを返す
                                Return False
                            End If
                        End If
                    End If
                End With

                '[Del] 2012/09/25 m.ibuki 形式チェック削除START
                ''ユーザーメールアドレス
                'With .PropTxtUsrMailAdd
                '    If .ReadOnly = False Then
                '        '入力のある場合、チェックを行う
                '        If .Text <> "" Then
                '            'メールアドレス形式ではない場合、エラー
                '            If commonLogicHBK.IsMailAddress(.Text) = False Then
                '                'エラーメッセージ設定
                '                puErrMsg = B0601_E006
                '                'タブを利用情報タブに設定
                '                dataHBKB0601.PropTbInput.SelectedIndex = TAB_RIYO
                '                'フォーカス設定
                '                .Focus()
                '                .SelectAll()
                '                'エラーを返す
                '                Return False
                '            End If
                '        End If
                '    End If
                'End With
                '[Del] 2012/09/25 m.ibuki 形式チェック削除END

                'レンタル期間FROM～TO
                If .PropDtpRentalStDT.Enabled = True And .PropDtpRentalEdDT.Enabled = True Then
                    '両日付に入力のある場合、チェックを行う
                    If .PropDtpRentalStDT.txtDate.Text <> "" And .PropDtpRentalEdDT.txtDate.Text <> "" Then
                        Dim dtmFrom As DateTime = DateTime.Parse(.PropDtpRentalStDT.txtDate.Text)
                        Dim dtmTo As DateTime = DateTime.Parse(.PropDtpRentalEdDT.txtDate.Text)
                        'FROM～TOの範囲が正しくない場合、エラー
                        If dtmFrom > dtmTo Then
                            'エラーメッセージ設定
                            puErrMsg = B0601_E007
                            'タブを利用情報タブに設定
                            dataHBKB0601.PropTbInput.SelectedIndex = TAB_RIYO
                            'フォーカス設定（FROM）
                            .PropDtpRentalStDT.txtDate.Focus()
                            .PropDtpRentalStDT.txtDate.SelectAll()
                            'エラーを返す
                            Return False
                        End If
                    End If
                End If


                'オプションソフト
                With .PropVwOptSoft.Sheets(0)

                    '1行以上ある場合、チェックを行う
                    '★★--------------------
                    'If .RowCount > 0 AndAlso .Cells(0, COL_OPTSOFT_SOFTNM).Locked = False Then
                    If .RowCount > 0 AndAlso .Cells(0, COL_OPTSOFT_SOFTCD).Locked = False Then
                        '★★--------------------

                        '一覧の行数分繰り返し
                        For i As Integer = 0 To .RowCount - 1

                            '変数宣言
                            Dim strSoftText As String = ""     'オプションソフト入力テキスト
                            '★★----------------------
                            'Dim strSoftNM As String = ""       'オプションソフト名
                            Dim strSoftCD As String = ""       'オプションソフトCD
                            '★★----------------------

                            '★★----------------------
                            '値を取得
                            'strSoftText = .Cells(i, COL_OPTSOFT_SOFTNM).Text
                            'strSoftNM = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_OPTSOFT_SOFTNM), "").Trim()
                            ''値を取得
                            strSoftText = .Cells(i, COL_OPTSOFT_SOFTCD).Text
                            strSoftCD = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_OPTSOFT_SOFTCD), "").Trim()
                            '★★----------------------

                            'オプションソフトが入力されている場合のみチェック
                            If strSoftText <> "" Then

                                '★★------------------
                                ''入力テキストがリストにない場合、エラー
                                Dim intCntVal As Integer = Aggregate row As DataRow In dataHBKB0601.PropDtSoftMasta
                                                           Where strSoftText = row.Item("SoftNM")
                                                           Into Count()
                                If intCntVal = 0 Then
                                    'エラーメッセージ設定
                                    puErrMsg = B0601_E008
                                    'タブを利用情報タブに設定
                                    dataHBKB0601.PropTbInput.SelectedIndex = TAB_RIYO
                                    'フォーカス設定
                                    If commonLogicHBK.SetFocusOnVwRow(dataHBKB0601.PropVwOptSoft, _
                                                                      0, i, COL_OPTSOFT_SOFTCD, 1, .ColumnCount) = False Then
                                        Return False
                                    End If
                                    'エラーを返す
                                    Return False
                                End If
                                '★★------------------

                                'オプションソフトが重複している場合、エラー
                                For j As Integer = 0 To .RowCount - 1

                                    '★★------------------
                                    'If i <> j AndAlso _
                                    '    strSoftNM = commonLogicHBK.ChangeNothingToStr(.Cells(j, COL_OPTSOFT_SOFTNM), "").Trim() Then

                                    If i <> j AndAlso _
                                    strSoftCD = commonLogicHBK.ChangeNothingToStr(.Cells(j, COL_OPTSOFT_SOFTCD), "").Trim() Then
                                        '★★------------------

                                        'エラーメッセージ設定
                                        puErrMsg = B0601_E008
                                        'タブを利用情報タブに設定
                                        dataHBKB0601.PropTbInput.SelectedIndex = TAB_RIYO

                                        '★★-------------------
                                        'フォーカス設定
                                        'If commonLogicHBK.SetFocusOnVwRow(dataHBKB0601.PropVwOptSoft, _
                                        '                                  0, j, COL_OPTSOFT_SOFTNM, 1, .ColumnCount) = False Then
                                        '    Return False
                                        'End If

                                        'フォーカス設定
                                        If commonLogicHBK.SetFocusOnVwRow(dataHBKB0601.PropVwOptSoft, _
                                                                          0, j, COL_OPTSOFT_SOFTCD, 1, .ColumnCount) = False Then
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

                'DBとの値比較チェック
                If CheckInputValueCompareDB(dataHBKB0601) = False Then
                    Return False
                End If

                'CIオーナー
                If .PropBtnSearch_Grp.Enabled = True Then
                    If .PropTxtCIOwnerNM.Text.Trim <> "" And _
                    .PropLblCIOwnerCD.Text = "" Then
                        'オーナー名に入力があってコードが未入力の場合（サブ検索にて選択していない場合）、エラー
                        puErrMsg = B0601_E012
                        'タブを関係情報タブに設定
                        .PropTbInput.SelectedIndex = TAB_RELATION
                        'フォーカス設定
                        .PropBtnSearch_Grp.Focus()
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
    ''' 【編集モード】入力チェック処理：DB比較
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力値をDB情報と比較してチェックする
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckInputValueCompareDB(ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter()    'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            'イメージ番号のマスタ存在チェック
            If dataHBKB0601.PropTxtImageNmb.ReadOnly = False Then
                If IsExistsImageNmbOnMasta(Adapter, Cn, dataHBKB0601) = False Then
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
        Finally
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            Adapter.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】イメージ番号のマスタ存在チェック処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたイメージ番号がマスタに存在するかチェックする
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function IsExistsImageNmbOnMasta(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable         '合致データ件数格納用テーブル

        Try
            With dataHBKB0601

                'イメージ番号が入力されている場合、チェックを行う
                If .PropTxtImageNmb.Text <> "" Then

                    'SQLを作成
                    If sqlHBKB0601.SetSelectSameImageNmbCntSql(Adapter, Cn, dataHBKB0601) = False Then
                        Return False
                    End If

                    'ログ出力
                    commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "イメージ番号合致データ件数取得", Nothing, Adapter.SelectCommand)

                    'SQL実行
                    Adapter.Fill(dtResult)

                    '入力されたイメージ番号がマスタに存在しない場合、エラー
                    If dtResult.Rows(0).Item(0) = 0 Then
                        'エラーメッセージ設定
                        puErrMsg = B0601_E010
                        'タブを基本情報タブに設定
                        .PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .PropTxtImageNmb.Focus()
                        .PropTxtImageNmb.SelectAll()
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
        Finally
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】セット機器グループ番号チェック処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたセット機器のグループ番号が1つのみかチェックする
    ''' <para>作成情報：2012/08/07 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function IsOnlyOneSetKikiGrpNo(ByRef Adapter As NpgsqlDataAdapter, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable           'グループ番号格納用テーブル
        Dim aryStrSetKikiNo As New ArrayList    'セット機器No格納配列

        Try
            'セット機器
            With dataHBKB0601.PropVwSetKiki.Sheets(0)

                '1行以上ある場合、チェックを行う
                If .RowCount > 0 AndAlso .Cells(0, COL_SETKIKI_SETKIKINO).Locked = False Then

                    '一覧の行数分繰り返し
                    For i As Integer = 0 To .RowCount - 1

                        '変数宣言
                        Dim strSetKikiNo As String = ""       'セット機器No

                        '値を取得
                        strSetKikiNo = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_SETKIKI_SETKIKINO), "").Trim()

                        'セット機器Noが入力されている場合配列にセット機器番号をセット
                        If strSetKikiNo <> "" Then

                            aryStrSetKikiNo.Add(strSetKikiNo)

                        End If

                    Next

                    '作成した配列をデータクラスにセット
                    dataHBKB0601.PropAryStrSetKikiNo = aryStrSetKikiNo

                    'SQLを作成
                    If sqlHBKB0601.SetSelectSetKikiGrpNoSql(Adapter, Cn, dataHBKB0601) = False Then
                        Return False
                    End If

                    'ログ出力
                    commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "セット機器グループ番号取得", Nothing, Adapter.SelectCommand)

                    'SQL実行
                    Adapter.Fill(dtResult)

                    'セット機器グループ番号が複数ある場合、エラー
                    If dtResult.Rows.Count > 1 Then
                        'エラーメッセージ設定
                        puErrMsg = B0601_E013
                        'タブを利用情報タブに設定
                        dataHBKB0601.PropTbInput.SelectedIndex = TAB_RIYO
                        'フォーカス設定
                        dataHBKB0601.PropVwSetKiki.Focus()
                        'エラーを返す
                        Return False

                    ElseIf dtResult.Rows.Count = 1 Then

                        'セット機器グループ番号が1つのみの場合、取得したグループ番号をデータクラスにセット
                        dataHBKB0601.PropIntSetKikiGrpNo = dtResult.Rows(0).Item("SetKikiGrpNo")

                    Else

                        dataHBKB0601.PropIntSetKikiGrpNo = 0

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
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】セット機器のテーブル存在チェック処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたセット機器がテーブルに存在するかチェックする
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function IsExistsSetKikiOnTable(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As DataTable = Nothing       '合致データ件数格納用テーブル

        Try
            'セット機器
            With dataHBKB0601.PropVwSetKiki.Sheets(0)

                '1行以上ある場合、チェックを行う
                If .RowCount > 0 AndAlso .Cells(0, COL_SETKIKI_SETKIKINO).Locked = False Then

                    '一覧の行数分繰り返し
                    For i As Integer = 0 To .RowCount - 1

                        '変数宣言
                        Dim strSetKikiNo As String = ""       'セット機器No

                        '値を取得
                        strSetKikiNo = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_SETKIKI_SETKIKINO), "").Trim()

                        'セット機器Noが入力されている場合のみチェック
                        If strSetKikiNo <> "" Then

                            'テーブルクリア
                            dtResult = New DataTable

                            'セット機器Noをデータクラスにセット
                            dataHBKB0601.PropStrSetKikiNo = strSetKikiNo

                            'SQLを作成
                            If sqlHBKB0601.SetSelectSameSetKikiCntSql(Adapter, Cn, dataHBKB0601) = False Then
                                Return False
                            End If

                            'ログ出力
                            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "セット機器合致データ件数取得", Nothing, Adapter.SelectCommand)

                            'SQL実行
                            Adapter.Fill(dtResult)

                            '入力されたセット機器がテーブルに存在しない場合、エラー
                            If dtResult.Rows(0).Item(0) = 0 Then
                                'エラーメッセージ設定
                                puErrMsg = B0601_E011
                                'タブを利用情報タブに設定
                                dataHBKB0601.PropTbInput.SelectedIndex = TAB_RIYO
                                'フォーカス設定
                                If commonLogicHBK.SetFocusOnVwRow(dataHBKB0601.PropVwSetKiki, _
                                                                  0, i, COL_SETKIKI_SETKIKINO, 1, .ColumnCount) = False Then
                                    Return False
                                End If
                                'エラーを返す
                                Return False
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
        Finally
            If dtResult IsNot Nothing Then
                dtResult.Dispose()
            End If
        End Try

    End Function

    ''' <summary>
    ''' 【編集／履歴モード】データ更新処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をDBに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ登録先を保存用テーブル→標準テーブルへ変更</p>
    ''' </para></remarks>
    Private Function UpdateData(ByVal dataHBKB0601 As DataHBKB0601) As Boolean

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
            If SelectSysDate(Adapter, Cn, dataHBKB0601) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '[del] 2013/10/11 t.fukuo セット機器履歴が余計に登録される不具合修正：START
            ''CI共通情報更新（UPDATE）
            'If UpdateTmpCIInfo(Cn, dataHBKB0601) = False Then
            '    'ロールバック
            '    If Tsx IsNot Nothing Then
            '        Tsx.Rollback()
            '    End If
            '    Return False
            'End If

            ''CIサポセン機器更新（UPDATE）
            'If UpdateTmpCISap(Cn, dataHBKB0601) = False Then
            '    'ロールバック
            '    If Tsx IsNot Nothing Then
            '        Tsx.Rollback()
            '    End If
            '    Return False
            'End If

            ''複数人利用更新（DELETE→INSERT）
            'If UpdateTmpShare(Cn, dataHBKB0601) = False Then
            '    'ロールバック
            '    If Tsx IsNot Nothing Then
            '        Tsx.Rollback()
            '    End If
            '    Return False
            'End If

            ''オプションソフト更新（DELETE→INSERT）
            'If UpdateTmpOptSoft(Cn, dataHBKB0601) = False Then
            '    'ロールバック
            '    If Tsx IsNot Nothing Then
            '        Tsx.Rollback()
            '    End If
            '    Return False
            'End If

            ''履歴情報登録
            'If InsertCIRireki(Adapter, Cn, dataHBKB0601) = False Then
            '    'ロールバック
            '    If Tsx IsNot Nothing Then
            '        Tsx.Rollback()
            '    End If
            '    Return False
            'End If

            ''セット機器にコピーする～チェックボックスにチェックが入っている場合のみ更新を行う
            'If dataHBKB0601.PropChkCopyToSetKiki.Checked = True Then
            '    'セット機器に画面情報を更新
            '    If UpdateSetKiki(Cn, dataHBKB0601, True) = False Then
            '        'ロールバック
            '        If Tsx IsNot Nothing Then
            '            Tsx.Rollback()
            '        End If
            '        Return False
            '    End If
            '    '履歴情報を新規登録
            '    If InsertCIRirekiCopy(Adapter, Cn, dataHBKB0601, True) = False Then
            '        'ロールバック
            '        If Tsx IsNot Nothing Then
            '            Tsx.Rollback()
            '        End If
            '        Return False
            '    End If

            'End If

            ''インシデントの機器にコピーする～チェックボックスにチェックが入っている場合のみ登録を行う
            'If dataHBKB0601.PropChkCopyToIncident.Checked = True Then
            '    'インシデント内に他の機器に画面情報を更新
            '    If UpdateSetKiki(Cn, dataHBKB0601, False) = False Then
            '        'ロールバック
            '        If Tsx IsNot Nothing Then
            '            Tsx.Rollback()
            '        End If
            '        Return False
            '    End If
            '    '履歴情報を新規登録
            '    If InsertCIRirekiCopy(Adapter, Cn, dataHBKB0601, False) = False Then
            '        'ロールバック
            '        If Tsx IsNot Nothing Then
            '            Tsx.Rollback()
            '        End If
            '        Return False
            '    End If
            'End If
            '[del] 2013/10/11 t.fukuo セット機器履歴が余計に登録される不具合修正：END

            '[add] 2013/10/11 t.fukuo セット機器履歴が余計に登録される不具合修正：START
            Dim ciNmb As Integer
            Dim rirekiNo As Integer

            'データ更新および履歴データ登録
            If UpdateDataAndInsertRireki(Cn, Adapter, dataHBKB0601) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'CI番号、履歴番号保存
            ciNmb = dataHBKB0601.PropIntCINmb
            rirekiNo = dataHBKB0601.PropIntRirekiNo
            dataHBKB0601.PropIntCINmbStc = dataHBKB0601.PropIntCINmb    '[add] 2014/06/09 e.okamura コピー不具合修正

            '「セット機器にコピーする」選択時、同じインシデントで作業が「設置」または「追加」のセット機器に入力内容をコピーする
            If dataHBKB0601.PropChkCopyToSetKiki.Checked = True Then
                If CopyDataToOtherCI(Cn, Adapter, dataHBKB0601, True) = False Then
                    'ロールバック
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    Return False
                End If
            End If

            '「インシデントの機器にコピーする」選択時、同じインシデントで作業が「設置」または「追加」の機器に入力内容をコピーする
            If dataHBKB0601.PropChkCopyToIncident.Checked = True Then
                If CopyDataToOtherCI(Cn, Adapter, dataHBKB0601, False) = False Then
                    'ロールバック
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    Return False
                End If
            End If

            'コピーによりCI番号、履歴Noが変わっている可能性があるため、CI番号を元データに戻す
            dataHBKB0601.PropIntCINmb = ciNmb
            dataHBKB0601.PropIntRirekiNo = rirekiNo
            '[add] 2013/10/11 t.fukuo セット機器履歴が余計に登録される不具合修正：END

            'サポセン機器メンテナンス機器情報更新
            If UpdateSapMainteKiki(Cn, dataHBKB0601) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '新規ログNo取得
            If GetNewRirekiNo(Adapter, Cn, dataHBKB0601) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'INC共通ログテーブル登録
            If InserIncInfoL(Cn, dataHBKB0601) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '作業履歴ログテーブル登録
            If InserIncRirekiL(Cn, dataHBKB0601) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '作業担当ログテーブル登録
            If InsertIncTantoL(Cn, dataHBKB0601) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '機器情報ログテーブル登録
            If InsertIncKikiL(Cn, dataHBKB0601) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '対応者情報ログテーブル登録
            If InsertIncKankeiL(Cn, dataHBKB0601) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'プロセスリンク(元)ログテーブル登録
            If InsertPLinkmotoL(Cn, dataHBKB0601) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '関連ファイルログテーブル登録
            If InsertIncFileL(Cn, dataHBKB0601) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'サポセン機器メンテナンス作業ログテーブル登録
            If InsertSapMainteWorkL(Cn, dataHBKB0601) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'サポセン機器メンテナンス機器ログテーブル登録
            If InsertSapMainteKikiL(Cn, dataHBKB0601) = False Then
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

    '[add] 2013/10/11 t.fukuo セット機器履歴が余計に登録される不具合修正：START
    ''' <summary>
    ''' データ更新および履歴データ登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapter</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面Dataクラス</param>
    ''' <param name="isCopy">[I]コピーフラグ</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をDBに反映する。また、CI情報履歴データの登録を行う。
    ''' <para>作成情報：2013/10/10 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateDataAndInsertRireki(ByVal Cn As NpgsqlConnection, _
                                               ByVal Adapter As NpgsqlDataAdapter, _
                                               ByVal dataHBKB0601 As DataHBKB0601, _
                                               Optional ByVal isCopy As Boolean = False) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '2014/06/09 Add コピー不具合修正 Start
            'コピーじゃない場合
            If isCopy = False Then
                '2014/06/09 Add コピー不具合修正 End

                'CI共通情報更新（UPDATE）
                If UpdateTmpCIInfo(Cn, dataHBKB0601) = False Then
                    Return False
                End If

                'CIサポセン機器更新（UPDATE）
                If UpdateTmpCISap(Cn, dataHBKB0601) = False Then
                    Return False
                End If

                '2014/06/09 Del コピー不具合修正 Start
                ''コピーじゃない場合
                'If isCopy = False Then
                '2014/06/09 Del コピー不具合修正 End

                '複数人利用更新（DELETE→INSERT）
                If UpdateTmpShare(Cn, dataHBKB0601) = False Then
                    Return False
                End If

                'オプションソフト更新（DELETE→INSERT）
                If UpdateTmpOptSoft(Cn, dataHBKB0601) = False Then
                    Return False
                End If

                '2014/06/09 Add コピー不具合修正 Start
            Else
                'コピーの場合

                'CI共通情報更新（UPDATE）
                If UpdateTmpCIInfoCopy(Cn, dataHBKB0601) = False Then
                    Return False
                End If

                'CIサポセン機器更新（UPDATE）
                If UpdateTmpCISapCopy(Cn, dataHBKB0601) = False Then
                    Return False
                End If

                '2014/06/09 Add コピー不具合修正 End
            End If


            '履歴情報登録
            If InsertCIRireki(Adapter, Cn, dataHBKB0601) = False Then
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
    ''' 【編集モード】CI機器情報コピー処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapter</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面Dataクラス</param>
    ''' <param name="blnCopeMode">[IN]コピーモード（True：セット機器にコピー、False：インシデントにコピー）</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI機器情報を同じインシデントのセット機器またはインシデント機器にコピーする
    ''' <para>作成情報：2013/10/10 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CopyDataToOtherCI(ByVal Cn As NpgsqlConnection, _
                                       ByVal Adapter As NpgsqlDataAdapter, _
                                       ByVal dataHBKB0601 As DataHBKB0601, _
                                       ByVal blnCopeMode As Boolean) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtCINmb As DataTable = Nothing                      'コピー対象機器CI番号格納テーブル

        Try
            'コピー対象のCI番号取得
            If GetCopyCINmbs(Cn, Adapter, dataHBKB0601, blnCopeMode, dtCINmb) = False Then
                Return False
            End If

            '取得件数分ループし、コピーを行う
            For Each row As DataRow In dtCINmb.Rows

                'CI番号をデータクラスにセット
                dataHBKB0601.PropIntCINmb = row.Item(0)

                'データ更新および履歴データを登録する
                If UpdateDataAndInsertRireki(Cn, Adapter, dataHBKB0601, True) = False Then
                    Return False
                End If

                'サポセン機器メンテナンス機器情報更新（最終更新時履歴No更新）
                If UpdateSapMainteKiki(Cn, dataHBKB0601) = False Then
                    Return False
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
        Finally
            If dtCINmb IsNot Nothing Then
                dtCINmb.Dispose()
            End If
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapter</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面Dataクラス</param>
    ''' <param name="blnCopeMode">[IN]コピーモード（True：セット機器にコピー、False：インシデントにコピー）</param>
    ''' <param name="dtCINmbs">[IN/OUT]コピー対象CI番号格納テーブル</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>コピー対象機器のCI番号を取得し、テーブルに格納して返す
    ''' <para>作成情報：2013/10/10 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetCopyCINmbs(ByVal Cn As NpgsqlConnection, _
                                   ByVal Adapter As NpgsqlDataAdapter, _
                                   ByVal dataHBKB0601 As DataHBKB0601, _
                                   ByVal blnCopeMode As Boolean, _
                                   ByRef dtCINmbs As DataTable) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '戻り値初期化
        dtCINmbs = New DataTable

        Try
            'SQLを作成
            If sqlHBKB0601.SetSelectCopyCINmbsSql(Adapter, Cn, dataHBKB0601, blnCopeMode) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "コピー対象機器のCI番号取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtCINmbs)

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function
    '[add] 2013/10/11 t.fukuo セット機器履歴が余計に登録される不具合修正：END

    ''' <summary>
    ''' 【編集モード】ロック解除処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータのロックを解除する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UnlockData(ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'CI共通情報ロック解除（DELETE）
            If commonLogicHBK.UnlockCIInfo(dataHBKB0601.PropIntCINmb) = False Then
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
    ''' 【編集／履歴モード】サーバー日付取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>更新用のサーバー日付を取得する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectSysDate(ByRef Adapter As NpgsqlDataAdapter, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSysDate As New DataTable         'サーバー日付格納用テーブル

        Try
            'SQLを作成
            If sqlHBKB0601.SetSelectSysDateSql(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サーバー日付取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtSysDate)

            'データが取得できた場合、データクラスにサーバー日付をセット
            If dtSysDate.Rows.Count > 0 Then
                dataHBKB0601.PropDtmSysDate = dtSysDate.Rows(0).Item("SysDate")
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
    ''' 【編集モード】CI共通情報更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でCI共通情報を更新（UPDATE）する
    ''' <para>作成情報：2012/08/02 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ登録先を保存用テーブル→標準テーブルへ変更</p>
    ''' </para></remarks>
    Private Function UpdateTmpCIInfo(ByVal Cn As NpgsqlConnection, _
                                     ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CI共通情報更新（UPDATE）用SQLを作成
            If sqlHBKB0601.SetUpdateTmpCIInfoSql(Cmd, Cn, dataHBKB0601) = False Then
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
    ''' 【編集モード】CIサポセン機器更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0601">[IN]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でCIサポセン機器を更新（UPDATE）する
    ''' <para>作成情報：2012/08/02 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ登録先を保存用テーブル→標準テーブルへ変更</p>
    ''' </para></remarks>
    Private Function UpdateTmpCISap(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CIサポセン機器更新（UPDATE）用SQLを作成
            If sqlHBKB0601.SetUpdateTmpCISapSql(Cmd, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIサポセン機器更新", Nothing, Cmd)

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
    ''' 【編集モード】複数人利用更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容で複数人利用テーブルを更新（DELETE→INSERT）する
    ''' <para>作成情報：2012/08/02 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ登録先を保存用テーブル→標準テーブルへ変更</p>
    ''' </para></remarks>
    Private Function UpdateTmpShare(ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '複数人利用削除（DELETE）
            If DeleteTmpShare(Cn, dataHBKB0601) = False Then
                Return False
            End If

            '複数人利用登録（INSERT）
            If InsertTmpShare(Cn, dataHBKB0601) = False Then
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
    ''' 【編集モード】複数人利用削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>複数人利用テーブルを物理削除（DELETE）する
    ''' <para>作成情報：2012/08/02 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ登録先を保存用テーブル→標準テーブルへ変更</p>
    ''' </para></remarks>
    Private Function DeleteTmpShare(ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            '複数人利用物理削除（DELETE）用SQLを作成
            If sqlHBKB0601.SetDeleteTmpShareSql(Cmd, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "複数人利用物理削除", Nothing, Cmd)

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
    ''' 【編集モード】複数人利用新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容を複数人利用に新規登録（INSERT）する
    ''' <para>作成情報：2012/08/02 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ登録先を保存用テーブル→標準テーブルへ変更</p>
    ''' </para></remarks>
    Private Function InsertTmpShare(ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            With dataHBKB0601

                '最新の複数人利用情報データテーブルを取得
                .PropDtShare = DirectCast(.PropVwShare.Sheets(0).DataSource, DataTable)

                'テーブルの変更をコミット
                .PropDtShare.AcceptChanges()

                '複数人利用一覧の行数分繰り返し、登録処理を行う
                For i As Integer = 0 To .PropDtShare.Rows.Count - 1

                    '登録行作成
                    Dim row As DataRow = .PropDtShare.Rows(i)

                    '作成した行をデータクラスにセット
                    .PropRowReg = row

                    '複数人利用新規登録（INSERT）用SQLを作成
                    If sqlHBKB0601.SetInsertTmpShareSql(Cmd, Cn, dataHBKB0601) = False Then
                        Return False
                    End If

                    'ログ出力
                    commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "複数人利新規登録", Nothing, Cmd)

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
    ''' 【編集モード】オプションソフト更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でオプションソフトテーブルを更新（DELETE→INSERT）する
    ''' <para>作成情報：2012/08/02 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ登録先を保存用テーブル→標準テーブルへ変更</p>
    ''' </para></remarks>
    Private Function UpdateTmpOptSoft(ByVal Cn As NpgsqlConnection, _
                                      ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'オプションソフト削除（DELETE）
            If DeleteTmpOptSoft(Cn, dataHBKB0601) = False Then
                Return False
            End If

            'オプションソフト登録（INSERT）
            If InsertTmpOptSoft(Cn, dataHBKB0601) = False Then
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
    ''' 【編集モード】オプションソフト削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>オプションソフトテーブルを物理削除（DELETE）する
    ''' <para>作成情報：2012/08/02 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ登録先を保存用テーブル→標準テーブルへ変更</p>
    ''' </para></remarks>
    Private Function DeleteTmpOptSoft(ByVal Cn As NpgsqlConnection, _
                                      ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'オプションソフト物理削除（DELETE）用SQLを作成
            If sqlHBKB0601.SetDeleteTmpOptSoftSql(Cmd, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "オプションソフト物理削除", Nothing, Cmd)

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
    ''' 【編集モード】オプションソフト新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をオプションソフトに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/02 t.fukuo
    ''' <p>改訂情報 : 2012/09/25 y.ikushima データ登録先を保存用テーブル→標準テーブルへ変更</p>
    ''' </para></remarks>
    Private Function InsertTmpOptSoft(ByVal Cn As NpgsqlConnection, _
                                      ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            With dataHBKB0601

                '最新のファイル情報データテーブルを取得
                .PropDtOptSoft = DirectCast(.PropVwOptSoft.Sheets(0).DataSource, DataTable)

                'テーブルの変更をコミット
                .PropDtOptSoft.AcceptChanges()

                'オプションソフト一覧の行数分繰り返し、登録処理を行う
                For i As Integer = 0 To .PropDtOptSoft.Rows.Count - 1

                    '入力がある場合のみ登録
                    If IsDBNull(.PropDtOptSoft.Rows(i).Item("SoftCD")) = False Then

                        '登録行作成
                        Dim row As DataRow = .PropDtOptSoft.Rows(i)

                        '作成した行をデータクラスにセット
                        .PropRowReg = row

                        'オプションソフト新規登録（INSERT）用SQLを作成
                        If sqlHBKB0601.SetInsertTmpOptSoftSql(Cmd, Cn, dataHBKB0601) = False Then
                            Return False
                        End If

                        'ログ出力
                        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "オプションソフト新規登録", Nothing, Cmd)

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
    ''' コンボボックスリストサイズ変更
    ''' </summary>
    ''' <param name="sender">[IN/OUT]コンボボックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>コンボボックスのリストサイズを一番文字数の多いデータに合わせて設定する
    ''' <para>作成情報：2012/08/14 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ResizeCmbList(ByRef sender As Object) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言   
        Dim cmbTmp As ComboBox = DirectCast(sender, ComboBox)
        Dim dtTmp As DataTable = Nothing
        Dim g As Graphics = Nothing
        Dim sf As SizeF
        Dim bLineX As Single

        Try
            'コンボボックスにデータソースが設定されている場合はデータソースをデータテーブルに変換
            If cmbTmp.DataSource IsNot Nothing Then
                dtTmp = DirectCast(cmbTmp.DataSource, DataTable)
            Else
                'データソース未設定時は処理を抜ける
                Exit Function
            End If

            '最大バイト数を取得
            Dim maxLenB = Aggregate row As DataRow In dtTmp.Rows _
                          Where IsDBNull(row.Item(1)) = False _
                          Select commonLogic.LenB(row.Item(1)) Into Max()

            '次の描画位置計算
            g = cmbTmp.CreateGraphics()
            sf = g.MeasureString(New String("0"c, maxLenB), cmbTmp.Font)
            bLineX += sf.Width

            '最終項目の場合、ドロップダウンリストのサイズを設定
            If dtTmp.Rows.Count >= 2 Then
                cmbTmp.DropDownWidth = bLineX
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
        Finally
            If g IsNot Nothing Then
                g.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' 【編集モード】構成管理履歴新規登録処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgsqlDataAdapter</param>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>構成管理の履歴情報を新規登録する
    ''' <para>作成情報：2012/09/25 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCIRireki(ByRef Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            '構成管理新規履歴No取得
            If GetNewCIRirekiNo(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'CI共通情報履歴新規登録
            If InsertCIInfoRireki(Cn, dataHBKB0601) = False Then
                Return False
            End If

            'CIサポセン機器履歴新規登録
            If InsertCISapRireki(Cn, dataHBKB0601) = False Then
                Return False
            End If

            'オプションソフト履歴新規登録
            If InsertOptSoftRireki(Cn, dataHBKB0601) = False Then
                Return False
            End If

            'セット機器履歴新規登録
            If InsertSetKikiRireki(Cn, dataHBKB0601) = False Then
                Return False
            End If

            '複数人利用履歴新規登録
            If InsertShareRireki(Cn, dataHBKB0601) = False Then
                Return False
            End If

            '登録理由履歴新規登録
            If InsertRegReasonWhenWorkAdded(Cn, dataHBKB0601) = False Then
                Return False
            End If

            '原因リンク新規登録
            If InsertCauseLinkWhenWorkAdded(Cn, dataHBKB0601) = False Then
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
        End Try

    End Function

    ''' <summary>
    ''' 【共通】構成管理新規履歴No取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapter</param>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番した構成管理の履歴Noを取得する
    ''' <para>作成情報：2012/09/25 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetNewCIRirekiNo(ByVal Adapter As NpgsqlDataAdapter, _
                                      ByVal Cn As NpgsqlConnection, _
                                      ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtRirekiNo As New DataTable         '履歴No格納用テーブル

        Try

            'SQLを作成
            If sqlHBKB0601.SetSelectNewCIRirekiNoSql(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規CI履歴No取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtRirekiNo)

            'データが取得できた場合、データクラスにCI履歴Noをセット
            If dtRirekiNo.Rows.Count > 0 Then
                dataHBKB0601.PropIntRirekiNo = dtRirekiNo.Rows(0).Item("RirekiNo")
            Else
                '取得できなかった場合はエラー
                puErrMsg = C0201_E027
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
    ''' 【編集モード】CI共通情報履歴テーブル新規登録
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報履歴テーブルにデータを新規登録する
    ''' <para>作成情報：2012/09/25 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCIInfoRireki(ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CI共通情報履歴新規登録用SQLを作成
            If sqlHBKB0601.SetInsertCIInfoRirekiSql(Cmd, Cn, dataHBKB0601) = False Then
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
    ''' 【編集モード】CIサポセン機器履歴テーブル新規登録
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CIサポセン機器履歴テーブルにデータを新規登録する
    ''' <para>作成情報：2012/09/25 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCISapRireki(ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CIサポセン機器履歴新規登録用SQLを作成
            If sqlHBKB0601.SetInsertCISapRirekiSql(Cmd, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIサポセン機器履歴新規登録", Nothing, Cmd)

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
    ''' 【編集モード】オプションソフト履歴テーブル新規登録
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>オプションソフト履歴テーブルにデータを新規登録する
    ''' <para>作成情報：2012/09/25 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertOptSoftRireki(ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'オプションソフト履歴新規登録用SQLを作成
            If sqlHBKB0601.SetInsertOptSoftRirekiSql(Cmd, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "オプションソフト履歴新規登録", Nothing, Cmd)

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
    ''' 【編集モード】セット機器履歴テーブル新規登録
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>セット機器履歴テーブルにデータを新規登録する
    ''' <para>作成情報：2012/09/25 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertSetKikiRireki(ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'セット機器履歴新規登録用SQLを作成
            If sqlHBKB0601.SetInsertSetKikiRirekiSql(Cmd, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "セット機器履歴新規登録", Nothing, Cmd)

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
    ''' 【編集モード】複数人利用履歴テーブル新規登録
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>複数人利用履歴テーブルにデータを新規登録する
    ''' <para>作成情報：2012/09/25 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertShareRireki(ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            '複数人利用履歴新規登録用SQLを作成
            If sqlHBKB0601.SetInsertShareRirekiSql(Cmd, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "複数人利用履歴新規登録", Nothing, Cmd)

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
    ''' 【編集モード】作業追加時：登録理由履歴テーブル新規登録
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録理由履歴テーブルにデータを新規登録する
    ''' <para>作成情報：2012/09/25 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRegReasonWhenWorkAdded(ByVal Cn As NpgsqlConnection, _
                                                  ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            '作業追加時登録理由履歴新規登録用SQLを作成
            If sqlHBKB0601.SetInsertRegReasonWhenWorkAddedSql(Cmd, Cn, dataHBKB0601) = False Then
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
    ''' 【編集モード】作業追加時：原因リンク履歴テーブル新規登録
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>原因リンク履歴テーブルにデータを新規登録する
    ''' <para>作成情報：2012/09/25 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCauseLinkWhenWorkAdded(ByVal Cn As NpgsqlConnection, _
                                                  ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            '作業追加時原因リンク履歴履歴新規登録用SQLを作成
            If sqlHBKB0601.SetInsertCauseLinkWhenWorkAddedSql(Cmd, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "原因リンク履歴新規登録", Nothing, Cmd)

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

    '[add] 2014/06/09 e.okamura コピー不具合修正 Start
    ''' <summary>
    ''' 【編集モード】CI共通情報更新(コピー)
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報を更新する(コピー)
    ''' <para>作成情報：2014/06/09 e.okamura
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateTmpCIInfoCopy(ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CI共通情報更新用SQL(コピー)を作成　※最終更新情報のみ更新
            If sqlHBKB0601.SetTmpCIInfoCopy(Cmd, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報更新(コピー)", Nothing, Cmd)

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
    ''' 【編集モード】サポセン機器情報更新(コピー)
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サポセン機器情報更新する(コピー)
    ''' <para>作成情報：2014/06/09 e.okamura
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateTmpCISapCopy(ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'サポセン機器情報更新用SQL(コピー)を作成
            If sqlHBKB0601.SetTmpCISapCopy(Cmd, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サポセン機器情報更新(コピー)", Nothing, Cmd)

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
    '[add] 2014/06/09 e.okamura コピー不具合修正 End

    '[del] 2013/10/11 t.fukuo セット機器履歴が余計に登録される不具合修正：START
    ' ''' <summary>
    ' ''' 【編集モード】サポセン機器情報更新
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ' ''' <param name="blnRegModeFlg">[IN]更新モードフラグ</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>セット機器のサポセン機器情報更新する
    ' ''' <para>作成情報：2012/09/25 y.ikushima
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function UpdateSetKiki(ByVal Cn As NpgsqlConnection, _
    '                                              ByVal dataHBKB0601 As DataHBKB0601, _
    '                                              ByVal blnRegModeFlg As Boolean) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try
    '        'セット機器サポセン情報更新用SQLを作成
    '        If sqlHBKB0601.SetUpdateSetKiki(Cmd, Cn, dataHBKB0601, blnRegModeFlg) = False Then
    '            Return False
    '        End If

    '        'ログ出力
    '        If blnRegModeFlg = True Then
    '            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "セット機器サポセン情報更新", Nothing, Cmd)
    '        Else
    '            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "インシデント内機器サポセン情報更新", Nothing, Cmd)
    '        End If

    '        'SQL実行
    '        Cmd.ExecuteNonQuery()

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        Cmd.Dispose()
    '    End Try

    'End Function

    ' ''' <summary>
    ' ''' 【編集モード】構成管理履歴新規登録処理(コピー）
    ' ''' </summary>
    ' ''' <param name="Adapter">[IN/OUT]NpgsqlDataAdapter</param>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ' ''' <param name="blnRegModeFlg">[IN]更新モードフラグ</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>セット機器、インシデントの機器コピーの際の構成管理の履歴情報を新規登録する
    ' ''' <para>作成情報：2012/09/26 y.ikushima
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function InsertCIRirekiCopy(ByRef Adapter As NpgsqlDataAdapter, _
    '                                ByVal Cn As NpgsqlConnection, _
    '                                ByVal dataHBKB0601 As DataHBKB0601, _
    '                                ByVal blnRegModeFlg As Boolean) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try

    '        'CIサポセン機器履歴新規登録Copy
    '        If InsertCISapRirekiCopy(Cn, dataHBKB0601, blnRegModeFlg) = False Then
    '            Return False
    '        End If

    '        'オプションソフト履歴新規登録Copy
    '        If InsertOptSoftRirekiCopy(Cn, dataHBKB0601, blnRegModeFlg) = False Then
    '            Return False
    '        End If

    '        'セット機器履歴新規登録Copy
    '        If InsertSetKikiRirekiCopy(Cn, dataHBKB0601, blnRegModeFlg) = False Then
    '            Return False
    '        End If

    '        '複数人利用履歴新規登録Copy
    '        If InsertShareRirekiCopy(Cn, dataHBKB0601, blnRegModeFlg) = False Then
    '            Return False
    '        End If

    '        '登録理由履歴新規登録Copy
    '        If InsertRegReasonWhenWorkAddedCopy(Cn, dataHBKB0601, blnRegModeFlg) = False Then
    '            Return False
    '        End If

    '        '原因リンク新規登録Copy
    '        If InsertCauseLinkWhenWorkAddedCopy(Cn, dataHBKB0601, blnRegModeFlg) = False Then
    '            Return False
    '        End If

    '        'CI共通情報履歴新規登録Copy
    '        If InsertCIInfoRirekiCopy(Cn, dataHBKB0601, blnRegModeFlg) = False Then
    '            Return False
    '        End If

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        Cmd.Dispose()
    '    End Try

    'End Function

    ' ''' <summary>
    ' ''' 【編集モード】CI共通情報履歴テーブル新規登録Copy
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ' ''' <param name="blnRegModeFlg">[IN]更新モードフラグ</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>CI共通情報履歴テーブルにデータを新規登録する
    ' ''' <para>作成情報：2012/09/25 y.ikushima
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function InsertCIInfoRirekiCopy(ByVal Cn As NpgsqlConnection, _
    '                                ByVal dataHBKB0601 As DataHBKB0601, _
    '                                ByVal blnRegModeFlg As Boolean) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try
    '        'CI共通情報履歴新規登録用SQLを作成
    '        If sqlHBKB0601.SetInsertCIInfoRirekiSqlCopy(Cmd, Cn, dataHBKB0601, blnRegModeFlg) = False Then
    '            Return False
    '        End If

    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報履歴新規登録", Nothing, Cmd)

    '        'SQL実行
    '        Cmd.ExecuteNonQuery()

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        Cmd.Dispose()
    '    End Try

    'End Function

    ' ''' <summary>
    ' ''' 【編集モード】CIサポセン機器履歴テーブル新規登録Copy
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ' ''' <param name="blnRegModeFlg">[IN]更新モードフラグ</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>CIサポセン機器履歴テーブルにデータを新規登録する
    ' ''' <para>作成情報：2012/09/25 y.ikushima
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function InsertCISapRirekiCopy(ByVal Cn As NpgsqlConnection, _
    '                                ByVal dataHBKB0601 As DataHBKB0601, _
    '                                ByVal blnRegModeFlg As Boolean) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try
    '        'CIサポセン機器履歴新規登録用SQLを作成
    '        If sqlHBKB0601.SetInsertCISapRirekiSqlCopy(Cmd, Cn, dataHBKB0601, blnRegModeFlg) = False Then
    '            Return False
    '        End If

    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIサポセン機器履歴新規登録", Nothing, Cmd)

    '        'SQL実行
    '        Cmd.ExecuteNonQuery()

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        Cmd.Dispose()
    '    End Try

    'End Function

    ' ''' <summary>
    ' ''' 【編集モード】オプションソフト履歴テーブル新規登録
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ' ''' <param name="blnRegModeFlg">[IN]更新モードフラグ</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>オプションソフト履歴テーブルにデータを新規登録する
    ' ''' <para>作成情報：2012/09/25 y.ikushima
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function InsertOptSoftRirekiCopy(ByVal Cn As NpgsqlConnection, _
    '                                ByVal dataHBKB0601 As DataHBKB0601, _
    '                                ByVal blnRegModeFlg As Boolean) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try
    '        'オプションソフト履歴新規登録用SQLを作成
    '        If sqlHBKB0601.SetInsertOptSoftRirekiSqlCopy(Cmd, Cn, dataHBKB0601, blnRegModeFlg) = False Then
    '            Return False
    '        End If

    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "オプションソフト履歴新規登録", Nothing, Cmd)

    '        'SQL実行
    '        Cmd.ExecuteNonQuery()

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        Cmd.Dispose()
    '    End Try

    'End Function

    ' ''' <summary>
    ' ''' 【編集モード】セット機器履歴テーブル新規登録
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ' ''' <param name="blnRegModeFlg">[IN]更新モードフラグ</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>セット機器履歴テーブルにデータを新規登録する
    ' ''' <para>作成情報：2012/09/25 y.ikushima
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function InsertSetKikiRirekiCopy(ByVal Cn As NpgsqlConnection, _
    '                                ByVal dataHBKB0601 As DataHBKB0601, _
    '                                ByVal blnRegModeFlg As Boolean) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try
    '        'セット機器履歴新規登録用SQLを作成
    '        If sqlHBKB0601.SetInsertSetKikiRirekiSqlCopy(Cmd, Cn, dataHBKB0601, blnRegModeFlg) = False Then
    '            Return False
    '        End If

    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "セット機器履歴新規登録", Nothing, Cmd)

    '        'SQL実行
    '        Cmd.ExecuteNonQuery()

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        Cmd.Dispose()
    '    End Try

    'End Function

    ' ''' <summary>
    ' ''' 【編集モード】複数人利用履歴テーブル新規登録
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ' ''' <param name="blnRegModeFlg">[IN]更新モードフラグ</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>複数人利用履歴テーブルにデータを新規登録する
    ' ''' <para>作成情報：2012/09/25 y.ikushima
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function InsertShareRirekiCopy(ByVal Cn As NpgsqlConnection, _
    '                                ByVal dataHBKB0601 As DataHBKB0601, _
    '                                ByVal blnRegModeFlg As Boolean) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try
    '        '複数人利用履歴新規登録用SQLを作成
    '        If sqlHBKB0601.SetInsertShareRirekiSqlCopy(Cmd, Cn, dataHBKB0601, blnRegModeFlg) = False Then
    '            Return False
    '        End If

    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "複数人利用履歴新規登録", Nothing, Cmd)

    '        'SQL実行
    '        Cmd.ExecuteNonQuery()

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        Cmd.Dispose()
    '    End Try

    'End Function

    ' ''' <summary>
    ' ''' 【編集モード】作業追加時：登録理由履歴テーブル新規登録Copy
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ' ''' <param name="blnRegModeFlg">[IN]更新モードフラグ</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>登録理由履歴テーブルにデータを新規登録する
    ' ''' <para>作成情報：2012/09/25 y.ikushima
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function InsertRegReasonWhenWorkAddedCopy(ByVal Cn As NpgsqlConnection, _
    '                                ByVal dataHBKB0601 As DataHBKB0601, _
    '                                ByVal blnRegModeFlg As Boolean) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try
    '        '作業追加時登録理由履歴新規登録用SQLを作成
    '        If sqlHBKB0601.SetInsertRegReasonWhenWorkAddedSqlCopy(Cmd, Cn, dataHBKB0601, blnRegModeFlg) = False Then
    '            Return False
    '        End If

    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "登録理由履歴新規登録", Nothing, Cmd)

    '        'SQL実行
    '        Cmd.ExecuteNonQuery()

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        Cmd.Dispose()
    '    End Try

    'End Function

    ' ''' <summary>
    ' ''' 【編集モード】作業追加時：原因リンク履歴テーブル新規登録Copy
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ' ''' <param name="blnRegModeFlg">[IN]更新モードフラグ</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>原因リンク履歴テーブルにデータを新規登録する
    ' ''' <para>作成情報：2012/09/25 y.ikushima
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function InsertCauseLinkWhenWorkAddedCopy(ByVal Cn As NpgsqlConnection, _
    '                                ByVal dataHBKB0601 As DataHBKB0601, _
    '                                ByVal blnRegModeFlg As Boolean) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try
    '        '作業追加時原因リンク履歴履歴新規登録用SQLを作成
    '        If sqlHBKB0601.SetInsertCauseLinkWhenWorkAddedSqlCopy(Cmd, Cn, dataHBKB0601, blnRegModeFlg) = False Then
    '            Return False
    '        End If

    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "原因リンク履歴新規登録", Nothing, Cmd)

    '        'SQL実行
    '        Cmd.ExecuteNonQuery()

    '        '終了ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

    '        '正常処理終了
    '        Return True

    '    Catch ex As Exception
    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
    '        'メッセージ変数にエラーメッセージを格納
    '        puErrMsg = HBK_E001 & ex.Message
    '        Return False
    '    Finally
    '        Cmd.Dispose()
    '    End Try

    'End Function
    '[del] 2013/10/11 t.fukuo セット機器履歴が余計に登録される不具合修正：END

    ''' <summary>
    ''' 【編集モード】セット機器、インシデント内の機器情報取得
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>セット機器、インシデント内の機器情報を取得する
    ''' <para>作成情報：2012/09/26 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetCINmbKikiInfo(ByVal Adapter As NpgsqlDataAdapter, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSetKikiCINmbInfo As New DataTable         'セット機器CI番号格納用テーブル
        Dim dtSetKikiIncidentInfo As New DataTable         'インシデント内の機器CI番号格納用テーブル

        Try
            '[mod] 2013/10/11 t.fukuo セット機器履歴が余計に登録される不具合修正：START
            ''セット機器CI番号取得用SQLを作成
            'If sqlHBKB0601.SetSelectCINmbSetKiki(Adapter, Cn, dataHBKB0601) = False Then
            '    Return False
            'End If

            '同じインシデント・セット機器のCI番号取得用SQLを作成
            If sqlHBKB0601.SetSelectCopyCINmbsSql(Adapter, Cn, dataHBKB0601, True) = False Then
                Return False
            End If
            '[mod] 2013/10/11 t.fukuo セット機器履歴が余計に登録される不具合修正：END

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "セット機器CI番号取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtSetKikiCINmbInfo)

            'データが取得できた場合、データクラスにTrueをセット
            If dtSetKikiCINmbInfo.Rows.Count > 0 Then
                dataHBKB0601.PropBlnSetKiki = True
            Else
                '取得できなかった場合はFlase
                dataHBKB0601.PropBlnSetKiki = False
            End If


            '[mod] 2013/10/11 t.fukuo セット機器履歴が余計に登録される不具合修正：START
            ''インシデント内の機器CI番号取得用SQLを作成
            'If sqlHBKB0601.SetSelectCINmbIncident(Adapter, Cn, dataHBKB0601) = False Then
            '    Return False
            'End If

            '同じインシデント機器のCI番号取得用SQLを作成
            If sqlHBKB0601.SetSelectCopyCINmbsSql(Adapter, Cn, dataHBKB0601, False) = False Then
                Return False
            End If
            '[mod] 2013/10/11 t.fukuo セット機器履歴が余計に登録される不具合修正：END

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "インシデント内の機器CI番号取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtSetKikiIncidentInfo)

            'データが取得できた場合、データクラスにTrueをセット
            If dtSetKikiIncidentInfo.Rows.Count > 0 Then
                dataHBKB0601.PropBlnIncident = True
            Else
                '取得できなかった場合はFlase
                dataHBKB0601.PropBlnIncident = False
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
            dtSetKikiCINmbInfo.Dispose()
            dtSetKikiIncidentInfo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】サポセン機器メンテナンス機器更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でサポセン機器メンテナンス機器を更新（UPDATE）する
    ''' <para>作成情報：2012/09/26 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateSapMainteKiki(ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0601.SetUpdateSapMainteKikiSql(Cmd, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サポセン機器メンテナンス機器更新", Nothing, Cmd)

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
    ''' 【共通】新規ログNo取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapter</param>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番したログNoを取得する
    ''' <para>作成情報：2012/09/28 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetNewRirekiNo(ByVal Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dLogNo As New DataTable         'ログNo格納用テーブル

        Try

            'SQLを作成
            If sqlHBKB0601.SetSelectNewRirekiNoSql(Adapter, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規ログNo取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dLogNo)

            'データが取得できた場合、データクラスにログNoをセット
            If dLogNo.Rows.Count > 0 Then
                dataHBKB0601.PropIntLogNo = dLogNo.Rows(0).Item("LogNo")
            Else
                '取得できなかった場合はエラー
                puErrMsg = C0201_E026
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
    ''' 【共通】INC共通情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>INC共通情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/09/28 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InserIncInfoL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0601.SetInsertIncInfoLSql(Cmd, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "INC共通情報ログ新規登録", Nothing, Cmd)

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
    ''' 【共通】作業履歴ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/09/28 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InserIncRirekiL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0601.SetInsertIncRirekiLSql(Cmd, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "作業履歴ログ新規登録", Nothing, Cmd)

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
    ''' 【共通】作業担当ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデント管理ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/09/28 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertIncTantoL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0601.SetInsertIncTantoLSql(Cmd, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "作業担当ログ新規登録", Nothing, Cmd)

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
    ''' 【共通】機器情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>機器情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/09/28 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertIncKikiL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0601.SetInsertIncKikiLSql(Cmd, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "機器情報ログ新規登録", Nothing, Cmd)

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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対応関係情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/09/28 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertIncKankeiL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0601.SetInsertIncKankeiLSql(Cmd, Cn, dataHBKB0601) = False Then
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンク情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/09/28 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertPLinkmotoL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0601.SetInsertPLinkmotoLSql(Cmd, Cn, dataHBKB0601) = False Then
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
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関連ファイル情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/09/28 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertIncFileL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0601.SetInsertIncFileLSql(Cmd, Cn, dataHBKB0601) = False Then
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
    ''' 【共通】サポセン機器メンテナンス作業ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サポセン機器メンテナンス作業ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/09/28 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertSapMainteWorkL(ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0601.SetInsertSapMainteWorkLSql(Cmd, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サポセン機器メンテナンス作業ログ新規登録", Nothing, Cmd)


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
    ''' 【共通】サポセン機器メンテナンス機器ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サポセン機器メンテナンス機器ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/09/25 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertSapMainteKikiL(ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0601.SetInsertSapMainteKikiLSql(Cmd, Cn, dataHBKB0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サポセン機器メンテナンス機器ログ新規登録", Nothing, Cmd)


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

    '[Add] 2012/10/24 s.yamaguchi START
    ''' <summary>
    ''' 1年後当月末日設定メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>1年後の当月末日を設定する
    ''' <para>作成情報：2012/10/24 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetOneYearLaterForCMonthMain(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '日付設定処理
        If SetOneYearLaterForCMonth(dataHBKB0601) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 1年後当月末日設定処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>1年後の当月末日を設定する
    ''' <para>作成情報：2012/10/24 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetOneYearLaterForCMonth(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '日付取得
            '[MOD]2013/03/21 t.fukuo システム日付から1年後を設定するよう修正 START
            'Dim strDate As String = dataHBKB0601.PropDtpRentalStDT.txtDate.Text
            Dim strDate As String = Now.ToString("yyyy/MM/dd")
            '[MOD]2013/03/21 t.fukuo システム日付から1年後を設定するよう修正 END
            Dim dteDate As Date = Nothing                               '設定日付

            '取得した日付に1年加算する
            dteDate = Date.Parse(strDate).AddYears(1)

            dataHBKB0601.PropDtpRentalEdDT.txtDate.Text = _
                DateSerial(dteDate.Year, dteDate.Month + 1, 1).AddDays(-1).ToString("yyyy/MM/dd")

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function
    '[Add] 2012/10/24 s.yamaguchi END

    '[Add] 2012/10/24 s.yamaguchi START
    ''' <summary>
    ''' 1年後先月末日設定メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>1年後の先月末日を設定する
    ''' <para>作成情報：2012/10/24 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetOneYearLaterForLMonthMain(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '日付設定処理

        '日付設定処理
        If SetOneYearLaterForLMonth(dataHBKB0601) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 1年後先月末日設定処理
    ''' </summary>
    ''' <param name="dataHBKB0601">[IN/OUT]サポセン機器登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>1年後の先月末日を設定する
    ''' <para>作成情報：2012/10/24 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetOneYearLaterForLMonth(ByRef dataHBKB0601 As DataHBKB0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '日付取得
            '[MOD]2013/03/21 t.fukuo システム日付から1年後を設定するよう修正 START
            'Dim strDate As String = dataHBKB0601.PropDtpRentalStDT.txtDate.Text
            Dim strDate As String = Now.ToString("yyyy/MM/dd")
            '[MOD]2013/03/21 t.fukuo システム日付から1年後を設定するよう修正 END
            Dim dteDate As Date = Nothing                               '設定日付

            '取得した日付に1年加算する
            dteDate = Date.Parse(strDate).AddYears(1)

            dataHBKB0601.PropDtpRentalEdDT.txtDate.Text = _
                DateSerial(dteDate.Year, dteDate.Month, 1).AddDays(-1).ToString("yyyy/MM/dd")

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function
    '[Add] 2012/10/24 s.yamaguchi END

End Class
