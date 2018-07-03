Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.IO
Imports System.Windows.Forms
Imports FarPoint.Win.Spread

''' <summary>
''' 問題登録画面ロジッククラス
''' </summary>
''' <remarks>問題登録画面のロジックを定義したクラス
''' <para>作成情報：2012/08/13 s.yamaguchi
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKD0201

    'インスタンス作成
    Private sqlHBKD0201 As New SqlHBKD0201
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    '定数宣言
    '作業履歴データ
    Public Const COL_YOJITSU_PRBSEQ As Integer = 0              '連番（隠し）
    Public Const COL_YOJITSU_WORKSTATENM As Integer = 1         '作業ステータス
    Public Const COL_YOJITSU_SYSTEM As Integer = 2              '対象システム
    Public Const COL_YOJITSU_WORKNAIYO As Integer = 3           '作業内容
    Public Const COL_YOJITSU_WORKSCEDT As Integer = 4           '作業予定日時
    Public Const COL_YOJITSU_WORKSCEDT_BTN As Integer = 5       '作業予定日時ボタン
    Public Const COL_YOJITSU_WORKSTDT As Integer = 6            '作業開始日時
    Public Const COL_YOJITSU_WORKSTDT_BTN As Integer = 7        '作業開始日時ボタン
    Public Const COL_YOJITSU_WORKEDDT As Integer = 8            '作業終了日時
    Public Const COL_YOJITSU_WORKEDDT_BTN As Integer = 9        '作業終了日時ボタン
    Public Const COL_YOJITSU_TANTOGRP1 As Integer = 10          '作業担当G1
    Public Const COL_YOJITSU_PRBTANTONM1 As Integer = 11        '作業担当1
    Public Const COL_YOJITSU_TANTOGRPCD1 As Integer = 12        '作業担当G1CD（隠し）
    Public Const COL_YOJITSU_PRBTANTOID1 As Integer = 13        '作業担当1ID（隠し）
    Public Const COL_YOJITSU_PRBTANTO_BTN As Integer = 210      '担当者ボタン
    Public Const YOJITSU_TANTO_COLCNT As Integer = 4            '1担当分カラム数（スプレッドループに使用）

    '会議情報データ
    Public Const COL_MEETING_NMB As Integer = 0                 '会議番号
    Public Const COL_MEETING_JISISTDT As Integer = 1            '実施日
    Public Const COL_MEETING_RESULTKBN As Integer = 2           '承認
    Public Const COL_MEETING_TITLE As Integer = 3               'タイトル
    Public Const COL_MEETING_RESULTKBNCD As Integer = 4         '承認コード（隠し）

    '対応関係者情報データ
    Public Const COL_PBMKANKEI_RELATIONKBN As Integer = 0       '区分
    Public Const COL_PBMKANKEI_RELATIONID As Integer = 1        'ID
    Public Const COL_PBMKANKEI_GRPNM As Integer = 2             'グループ名
    Public Const COL_PBMKANKEI_HBKUSRNM As Integer = 3          'ユーザー名
    Public Const COL_PBMKANKEI_REGDT As Integer = 4             '登録日時
    Public Const COL_PBMKANKEI_REGGRPCD As Integer = 5          '登録者グループCD
    Public Const COL_PBMKANKEI_REGID As Integer = 6             '登録者ID
    Public Const COL_PBMKANKEI_UPDATEDT As Integer = 7          '更新日時
    Public Const COL_PBMKANKEI_UPGRPCD As Integer = 8           '更新者グループCD
    Public Const COL_PBMKANKEI_UPDATEID As Integer = 9          '更新者ID

    'プロセスリンク情報データ
    Public Const COL_PLINK_PLINKKBN As Integer = 0              'プロセス区分（略名称）
    Public Const COL_PLINK_PLINKNO As Integer = 1               '番号
    Public Const COL_PLINK_PLINKKBNCD As Integer = 2            'プロセス区分（隠し）
    Public Const COL_PLINK_REGDT As Integer = 3                 '登録日時
    Public Const COL_PLINK_REGGRPCD As Integer = 4              '登録者グループCD
    Public Const COL_PLINK_REGID As Integer = 5                 '登録者ID
    Public Const COL_PLINK_UPDATEDT As Integer = 6              '更新日時
    Public Const COL_PLINK_UPGRPCD As Integer = 7               '更新者グループCD
    Public Const COL_PLINK_UPDATEID As Integer = 8              '更新者ID

    'CYSPR情報データ
    Public Const COL_CYSPR_CYSPRNMB As Integer = 0              '番号
    Public Const COL_CYSPR_REGDT As Integer = 1                 '登録日時
    Public Const COL_CYSPR_REGGRPCD As Integer = 2              '登録者グループCD
    Public Const COL_CYSPR_REGID As Integer = 3                 '登録者ID
    Public Const COL_CYSPR_UPDATEDT As Integer = 4              '更新日時
    Public Const COL_CYSPR_UPGRPCD As Integer = 5               '更新者グループCD
    Public Const COL_CYSPR_UPDATEID As Integer = 6              '更新者ID

    '関連ファイル情報データ
    Public Const COL_PRBFILE_NAIYO As Integer = 0               '説明
    Public Const COL_PRBFILE_MNGNMB As Integer = 1              'ファイル番号（隠し）
    Public Const COL_PRBFILE_FILEPATH As Integer = 2            'ファイルパス（隠し）

    'タブインデックス
    Public Const TAB_KHN As Integer = 0                         '基本情報
    Public Const TAB_MEETING As Integer = 1                     '会議情報
    Public Const TAB_FREE As Integer = 2                        'フリー入力情報

    'MaxDrop
    Private MaxDrop_systemnmb As Integer = 18


    ''' <summary>
    ''' 【共通】システムエラー事前対応処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システムエラー発生時に非活性にするコントロールリストを作成する
    ''' <para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DoProcForErrorMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'トランザクション系コントロールリスト作成
        If CreateTsxCtlList(dataHBKD0201) = False Then
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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>トランザクション系コントロールのリストを作成する
    ''' <para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateTsxCtlList(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim aryCtlList As New ArrayList

        Try
            'トランザクション系のコントロールをリストに追加
            With dataHBKD0201

                'ヘッダ
                aryCtlList.Add(.PropGrpLoginUser)           'ログイン／ロックグループ
                'フッタ
                aryCtlList.Add(.PropBtnReg)                 '登録／作業予実登録ボタン
                aryCtlList.Add(.PropBtnMail)                'メール作成ボタン
                aryCtlList.Add(.PropBtnHenkou)              '変更登録ボタン
                aryCtlList.Add(.PropBtnPrint)               '単票出力ボタン
                '基本情報タブ
                aryCtlList.Add(.PropBtnStartDT_HM)          '時（開始日時）
                aryCtlList.Add(.PropBtnKanryoDT_HM)         '時（完了日時）
                aryCtlList.Add(.PropBtnTantoSearch)         '検索（担当者）
                aryCtlList.Add(.PropBtnTantoMe)             '私（担当者）
                aryCtlList.Add(.PropBtnApproverSearch)      '検索（対処承認者）
                aryCtlList.Add(.PropBtnApproverMe)          '私（対処承認者）
                aryCtlList.Add(.PropBtnRecorder)            '検索（承認記録者）
                aryCtlList.Add(.PropBtnRecorderMe)          '私（承認記録者）
                aryCtlList.Add(.PropBtnAddRow_Yojitsu)      '（作業予実）「+」
                aryCtlList.Add(.PropBtnRemoveRow_Yojitsu)   '（作業予実）「-」
                '会議情報タブ
                aryCtlList.Add(.PropBtnAddRow_Meeting)      '（会議情報）「+」
                aryCtlList.Add(.PropBtnRemoveRow_Meeting)   '（会議情報）「-」
                '対応関係者情報
                aryCtlList.Add(.PropBtnAddRow_RelaG)        '「+G」
                aryCtlList.Add(.PropBtnAddRow_RelaU)        '「+U」
                aryCtlList.Add(.PropBtnRemoveRow_Rela)      '「-」
                'プロセスリンク情報
                aryCtlList.Add(.PropBtnAddRow_Plink)        '「+」
                aryCtlList.Add(.PropBtnRemoveRow_Plink)     '「-」
                'CYSPR情報
                aryCtlList.Add(.PropBtnAddRow_Cyspr)        '「+」
                aryCtlList.Add(.PropBtnRemoveRow_Cyspr)     '「-」
                '関連ファイル情報
                aryCtlList.Add(.PropBtnAddRow_File)         '「+」
                aryCtlList.Add(.PropBtnRemoveRow_File)      '「-」
                aryCtlList.Add(.PropBtnOpenFile)            '「開」
                aryCtlList.Add(.PropBtnSaveFile)            '「ダ」

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
    ''' 【新規登録モード（インシデント登録画面呼出時）】画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて画面の初期表示設定を行う
    ''' <para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormNewModeFromIncMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '1スプレッド表示用データテーブル作成
        If CreateDataTableForVw(dataHBKD0201) = False Then
            Return False
        End If

        '2フォームコントロール設定
        If InitFormControl(dataHBKD0201) = False Then
            Return False
        End If

        '3初期表示用データ取得
        If GetInitData(dataHBKD0201) = False Then
            Return False
        End If

        '4初期表示用データセット
        If SetInitDataToControl(dataHBKD0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【新規登録モード（インシデント登録画面以外呼出時）】画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて画面の初期表示設定を行う
    ''' <para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormNewModeMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '1スプレッド表示用データテーブル作成
        If CreateDataTableForVw(dataHBKD0201) = False Then
            Return False
        End If

        '2フォームコントロール設定
        If InitFormControl(dataHBKD0201) = False Then
            Return False
        End If

        '3初期表示用データ取得
        If GetInitData(dataHBKD0201) = False Then
            Return False
        End If

        '4初期表示用データセット
        If SetInitDataToControl(dataHBKD0201) = False Then
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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで画面の初期表示設定を行う
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormEditModeMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '1スプレッド表示用データテーブル作成
        If CreateDataTableForVw(dataHBKD0201) = False Then
            Return False
        End If

        '2フォームコントロール設定
        If InitFormControl(dataHBKD0201) = False Then
            Return False
        End If

        '3初期表示用データ取得
        If GetInitData(dataHBKD0201) = False Then
            Return False
        End If

        '4初期表示用データセット
        If SetInitDataToControl(dataHBKD0201) = False Then
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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで画面の初期表示設定を行う
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormRefModeMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '1スプレッド表示用データテーブル作成
        If CreateDataTableForVw(dataHBKD0201) = False Then
            Return False
        End If

        '2フォームコントロール設定
        If InitFormControl(dataHBKD0201) = False Then
            Return False
        End If

        '3初期表示用データ取得
        If GetInitData(dataHBKD0201) = False Then
            Return False
        End If

        '4初期表示用データセット
        If SetInitDataToControl(dataHBKD0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【作業予実モード】画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業予実モードで画面の初期表示設定を行う
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormRirekiModeMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '1スプレッド表示用データテーブル作成
        If CreateDataTableForVw(dataHBKD0201) = False Then
            Return False
        End If

        '2フォームコントロール設定
        If InitFormControl(dataHBKD0201) = False Then
            Return False
        End If

        '3初期表示用データ取得
        If GetInitData(dataHBKD0201) = False Then
            Return False
        End If

        '4初期表示用データセット
        If SetInitDataToControl(dataHBKD0201) = False Then
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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>データのロック状況をチェックし、状況に応じてロックまたは処理モードの切替を行う
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function LockMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロック設定
        If SetLockWhenLoad(dataHBKD0201) = False Then
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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLockWhenLoad(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnBeLocked As Boolean = False                   'ロックフラグ  

        Try

            With dataHBKD0201

                'ロック解除チェック
                If CheckPrbDataBeLocked(.PropIntPrbNmb, blnBeLocked, .PropStrBeLockedMsg, .PropDtPrbInfoLock) = False Then
                    Return False
                End If

                'ロックされている（別のユーザが編集中）場合、 ロックフラグをON
                If blnBeLocked = True Then

                    dataHBKD0201.PropBlnBeLockedFlg = True

                Else

                    'ロックされていない場合、問題共通情報をロックする
                    If SetLock(dataHBKD0201) = False Then
                        Return False
                    End If

                    'ロックフラグをOFF
                    dataHBKD0201.PropBlnBeLockedFlg = False

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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>問題共通情報テーブルをロックする
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLock(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnDoUnlock As Boolean = False                   'ロック解除実行フラグ

        Try

            With dataHBKD0201

                '問題共通情報ロックテーブルデータがある場合、ロック解除実行フラグON
                If .PropDtPrbInfoLock.Rows.Count > 0 Then
                    blnDoUnlock = True
                End If

                '問題共通情報ロック
                If LockPrbInfo(.PropIntPrbNmb, .PropDtPrbInfoLock, blnDoUnlock) = False Then
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
    ''' 【プロセスリンク】対応関係者チェックメイン処理　[.PropintChkKankei = 0:参照不可,1:参照のみ関係者,2:編集できる関係者]
    ''' </summary>
    ''' <param name="intResult">[IN/OUT]関係者チェック情報</param>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <param name="strKbn">[IN]プロセス区分</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者をチェックし、処理モードの切替を行う。
    ''' <para>作成情報：2012/08/29 y.ikushima
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

            'ユーザーチェック処理
            If ChkKankeiU(Adapter, Cn, intNmb, strKbn, intResult) = False Then
                Return False
            End If

            '関係者なら次のチェックは不要
            If intResult <> KANKEI_CHECK_EDIT Then
                '所属グループチェック処理
                If ChkKankeiSZK(Adapter, Cn, intNmb, strKbn, intResult) = False Then
                    Return False
                End If

                '関係者でないなら次のチェックは不要
                If intResult <> KANKEI_CHECK_NONE Then
                    'グループチェック処理
                    If ChkKankeiG(Adapter, Cn, intNmb, strKbn, intResult) = False Then
                        Return False
                    End If
                End If
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
            Adapter.Dispose()
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function



    ''' <summary>
    ''' 【共通】対応関係者チェックメイン処理　[.PropintChkKankei = 0:参照不可,1:参照のみ関係者,2:編集できる関係者]
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者をチェックし、処理モードの切替を行う。
    ''' <para>作成情報：2012/08/29 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function KankeiCheckMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ
        Try
            'コネクションを開く
            Cn.Open()

            With dataHBKD0201
                'ユーザーチェック処理
                If ChkKankeiU(Adapter, Cn, .PropIntPrbNmb, PROCESS_TYPE_QUESTION, .PropIntChkKankei) = False Then
                    Return False
                End If

                '関係者なら次のチェックは不要
                If .PropIntChkKankei <> KANKEI_CHECK_EDIT Then
                    '所属グループチェック処理
                    If ChkKankeiSZK(Adapter, Cn, .PropIntPrbNmb, PROCESS_TYPE_QUESTION, .PropIntChkKankei) = False Then
                        Return False
                    End If

                    '関係者でないなら次のチェックは不要
                    If .PropIntChkKankei <> KANKEI_CHECK_NONE Then
                        'グループチェック処理
                        If ChkKankeiG(Adapter, Cn, .PropIntPrbNmb, PROCESS_TYPE_QUESTION, .PropIntChkKankei) = False Then
                            Return False
                        End If
                    End If
                End If
            End With

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
            Adapter.Dispose()
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【共通】対応関連者所属チェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="IntNmb">[IN]管理番号</param>
    ''' <param name="StrKbn">[IN]プロセス区分</param>
    ''' <param name="IntResult">[IN/OUT]結果戻り値</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>チェックデータを取得する
    ''' <para>作成情報：2012/08/29 y.ikushima
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
            If sqlHBKD0201.GetChkKankeiSZKData(Adapter, Cn, IntNmb, StrKbn) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対応関連者所属グループチェック", Nothing, Adapter.SelectCommand)

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
    ''' 【共通】対応関連者グループチェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="IntNmb">[IN]管理番号</param>
    ''' <param name="StrKbn">[IN]プロセス区分</param>
    ''' <param name="IntResult">[IN/OUT]結果戻り値</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>チェックデータを取得する
    ''' <para>作成情報：2012/08/29 y.ikushima
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
            If sqlHBKD0201.GetChkKankeiGData(Adapter, Cn, IntNmb, StrKbn) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対応関連者グループチェック", Nothing, Adapter.SelectCommand)

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
    ''' 【共通】対応関連者ユーザーチェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="IntNmb">[IN]管理番号</param>
    ''' <param name="StrKbn">[IN]プロセス区分</param>
    ''' <param name="IntResult">[IN/OUT]結果戻り値</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>チェックデータを取得する
    ''' <para>作成情報：2012/08/29 y.ikushima
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
            If sqlHBKD0201.GetChkKankeiUData(Adapter, Cn, IntNmb, StrKbn) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対応関連者ユーザーチェック", Nothing, Adapter.SelectCommand)

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
    ''' 【共通】スプレッド用データテーブル作成処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドのデータソースとして設定するデータテーブルを作成する
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateDataTableForVw(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtProblemWkRireki As New DataTable      'スプレッド表示用：作業履歴データ
        Dim dtMeeting As New DataTable              'スプレッド表示用：会議情報データ
        Dim dtProblmKankei As New DataTable         'スプレッド表示用：対応関係者情報データ
        Dim dtProcessLink As New DataTable          'スプレッド表示用：プロセスリンク情報データ
        Dim dtProblemCyspr As New DataTable         'スプレッド表示用：CSYPR情報データ
        Dim dtProblemFile As New DataTable          'スプレッド表示用：関連ファイル情報データ

        Try

            '作業予実データ
            With dtProblemWkRireki
                .Columns.Add("workrirekinmb", Type.GetType("System.Int32"))             '連番（隠し）
                .Columns.Add("workstatecd", Type.GetType("System.String"))              '作業ステータス
                .Columns.Add("systemnmb", Type.GetType("System.Int32"))                 '対象システム
                .Columns.Add("worknaiyo", Type.GetType("System.String"))                '作業内容
                .Columns.Add("workscedt", Type.GetType("System.DateTime"))              '作業予定日時
                .Columns.Add("workscedt_HM", Type.GetType("System.String"))             '作業予定日時ボタン
                .Columns.Add("workstdt", Type.GetType("System.DateTime"))               '作業開始日時
                .Columns.Add("workstdt_HM", Type.GetType("System.String"))              '作業開始日時ボタン
                .Columns.Add("workeddt", Type.GetType("System.DateTime"))               '作業終了日時
                .Columns.Add("workeddt_HM", Type.GetType("System.String"))              '作業終了日時ボタン
                For i As Integer = 1 To 50
                    .Columns.Add("worktantogrpnm" & i, Type.GetType("System.String"))   '担当者G
                    .Columns.Add("worktantonm" & i, Type.GetType("System.String"))      '担当者U
                    .Columns.Add("worktantogrpcd" & i, Type.GetType("System.String"))   '担当者Gコード（隠し）
                    .Columns.Add("worktantoid" & i, Type.GetType("System.String"))      '担当者UID（隠し）
                Next
                .Columns.Add("worktantoid_BTN", Type.GetType("System.String"))          '担当者ボタン
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            '対応関係者情報データ
            With dtProblmKankei
                .Columns.Add("RelationKbn", Type.GetType("System.String"))              '区分
                .Columns.Add("RelationID", Type.GetType("System.String"))               'ID
                .Columns.Add("GroupNM", Type.GetType("System.String"))                  'グループ名
                .Columns.Add("HBKUsrNM", Type.GetType("System.String"))                 'ユーザー名

                .Columns.Add("EntryNmb", Type.GetType("System.Int32"))
                .Columns.Add("RegDT", Type.GetType("System.DateTime"))                  '登録日時
                .Columns.Add("RegGrpCD", Type.GetType("System.String"))                 '登録者グループCD
                .Columns.Add("RegID", Type.GetType("System.String"))                    '登録者ID
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'プロセスリンク情報データ
            With dtProcessLink
                .Columns.Add("ProcessKbnNMR", Type.GetType("System.String"))            'プロセス区分（略名称）
                .Columns.Add("MngNmb", Type.GetType("System.String"))                   '番号
                .Columns.Add("ProcessKbn", Type.GetType("System.String"))               'プロセス区分（隠し）

                .Columns.Add("EntryDT", Type.GetType("System.DateTime"))
                .Columns.Add("RegDT", Type.GetType("System.DateTime"))                  '登録日時
                .Columns.Add("RegGrpCD", Type.GetType("System.String"))                 '登録者グループCD
                .Columns.Add("RegID", Type.GetType("System.String"))                    '登録者ID
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'CSYPR情報データ
            With dtProblemCyspr
                .Columns.Add("CysprNmb", Type.GetType("System.String"))                 'CYSPR番号

                .Columns.Add("RegDT", Type.GetType("System.DateTime"))                  '登録日時
                .Columns.Add("RegGrpCD", Type.GetType("System.String"))                 '登録者グループCD
                .Columns.Add("RegID", Type.GetType("System.String"))                    '登録者ID
                .Columns.Add("UpdateDT", Type.GetType("System.DateTime"))               '最終更新日時
                .Columns.Add("UpGrpCD", Type.GetType("System.String"))                  '最終更新者グループCD
                .Columns.Add("UpdateID", Type.GetType("System.String"))                 '最終更新者ID
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            '関連ファイル情報データ
            With dtProblemFile
                .Columns.Add("FileNaiyo", Type.GetType("System.String"))                '説明
                .Columns.Add("FileMngNmb", Type.GetType("System.String"))               'ファイル番号（隠し）
                .Columns.Add("FilePath", Type.GetType("System.String"))                 'ファイルパス（隠し）
                'テーブルの変更を確定
                .AcceptChanges()
            End With


            '会議情報データ
            With dtMeeting
                .Columns.Add("MeetingNmb", Type.GetType("System.String"))               '会議番号
                .Columns.Add("JisiDT", Type.GetType("System.String"))                   '実施日
                .Columns.Add("ResultKbnNM", Type.GetType("System.String"))              '承認
                .Columns.Add("Title", Type.GetType("System.String"))                    'タイトル
                .Columns.Add("ResultKbn", Type.GetType("System.String"))                '承認コード（隠し）
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'データクラスへデータテーブルをセット
            With dataHBKD0201
                .PropDtwkRireki = dtProblemWkRireki                                     'スプレッド表示用：作業履歴＋担当データ
                .PropDtMeeting = dtMeeting                                              'スプレッド表示用：会議情報データ
                .PropDtProblmKankei = dtProblmKankei                                    'スプレッド表示用：対応関係者情報データ
                .PropDtProcessLink = dtProcessLink                                      'スプレッド表示用：プロセスリンク情報データ
                .PropDtProblemCyspr = dtProblemCyspr                                    'スプレッド表示用：CSYPR情報データ
                .PropDtProblemFile = dtProblemFile                                      'スプレッド表示用：関連ファイル情報データ
            End With

            '終了ログ出力
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
            dtProblemWkRireki.Dispose()
            dtMeeting.Dispose()
            dtProblmKankei.Dispose()
            dtProcessLink.Dispose()
            dtProblemCyspr.Dispose()
            dtProblemFile.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】フォームコントロール設定
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InitFormControl(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'スプレッド設定
            If SetVwControl(dataHBKD0201) = False Then
                Return False
            End If

            '処理モードに応じたフォームコントロール設定
            If SetFormControlPerProcMode(dataHBKD0201) = False Then
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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>各スプレッド（一覧）を初期設定する
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetVwControl(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKD0201

                '作業予実
                With .PropVwPrbYojitsu.Sheets(0)
                    .DataAutoCellTypes = False
                    .DataAutoSizeColumns = False
                    .DataAutoHeadings = False

                    .Columns(COL_YOJITSU_PRBSEQ).DataField = "WorkRirekiNmb"                        '連番（隠し）
                    .Columns(COL_YOJITSU_WORKSTATENM).DataField = "workstatecd"                     '作業ステータス
                    .Columns(COL_YOJITSU_SYSTEM).DataField = "SystemNmb"                            '対象システム
                    .Columns(COL_YOJITSU_WORKNAIYO).DataField = "WorkNaiyo"                         '作業内容
                    .Columns(COL_YOJITSU_WORKSCEDT).DataField = "WorkSceDT"                         '作業予定日時
                    '.Columns(COL_YOJITSU_WORKSCEDT_BTN).DataField = "WorkSceDTBTN"                  '作業予定日時ボタン
                    .Columns(COL_YOJITSU_WORKSTDT).DataField = "WorkStDT"                           '作業開始日時
                    '.Columns(COL_YOJITSU_WORKSTDT_BTN).DataField = "WorkStDTBTN"                    '作業開始日時ボタン
                    .Columns(COL_YOJITSU_WORKEDDT).DataField = "WorkEdDT"                           '作業終了日時
                    '.Columns(COL_YOJITSU_WORKEDDT_BTN).DataField = "WorkEdDTBTN"                    '作業終了日時ボタン
                    For i As Integer = 0 To 49  '列50固定
                        .Columns(COL_YOJITSU_TANTOGRP1 + (i * YOJITSU_TANTO_COLCNT)).DataField = "TantoGpNM" & i + 1            '担当グループ名
                        .Columns(COL_YOJITSU_PRBTANTONM1 + (i * YOJITSU_TANTO_COLCNT)).DataField = "TantoUsrNM" & i + 1         '担当氏名
                        .Columns(COL_YOJITSU_TANTOGRPCD1 + (i * YOJITSU_TANTO_COLCNT)).DataField = "TantoGpCD" & i + 1          '担当グループCD　※隠し列
                        .Columns(COL_YOJITSU_PRBTANTOID1 + (i * YOJITSU_TANTO_COLCNT)).DataField = "TantoUsrID" & i + 1         '担当ID　　　　　※隠し列
                    Next
                    '.Columns(COL_YOJITSU_PRBTANTO_BTN).DataField = "TantoBTN"                       '担当者ボタン
                    '隠し列非表示
                    .Columns(COL_YOJITSU_PRBSEQ).Visible = False
                End With

                '対応関係情報
                With .PropVwRelationInfo.Sheets(0)
                    .ColumnCount = COL_PBMKANKEI_REGID + 1
                    .DataAutoCellTypes = False
                    .DataAutoSizeColumns = False
                    .DataAutoHeadings = False

                    .Columns(COL_PBMKANKEI_RELATIONKBN).DataField = "RelationKbn"                   '区分
                    .Columns(COL_PBMKANKEI_RELATIONID).DataField = "RelationID"                     'ID
                    .Columns(COL_PBMKANKEI_GRPNM).DataField = "GroupNM"                             'グループ名
                    .Columns(COL_PBMKANKEI_HBKUSRNM).DataField = "HBKUsrNM"
                    '隠し列非表示
                    .Columns(COL_PBMKANKEI_REGDT).Visible = False
                    .Columns(COL_PBMKANKEI_REGGRPCD).Visible = False
                    .Columns(COL_PBMKANKEI_REGID).Visible = False
                End With

                'プロセスリンク情報
                With .PropVwProcessLinkInfo.Sheets(0)
                    .ColumnCount = COL_PLINK_REGID + 1
                    .DataAutoCellTypes = False
                    .DataAutoSizeColumns = False
                    .DataAutoHeadings = False

                    .Columns(COL_PLINK_PLINKKBN).DataField = "ProcessKbnNMR"                        'プロセス区分（略名称）
                    .Columns(COL_PLINK_PLINKNO).DataField = "MngNmb"                                '番号
                    .Columns(COL_PLINK_PLINKKBNCD).DataField = "ProcessKbn"                         'プロセス区分（隠し）

                    .Columns(COL_PLINK_PLINKKBNCD).Visible = False
                    .Columns(COL_PLINK_REGDT).Visible = False
                    .Columns(COL_PLINK_REGGRPCD).Visible = False
                    .Columns(COL_PLINK_REGID).Visible = False
                End With

                'CSYPR情報
                With .PropVwCysprInfo.Sheets(0)
                    .ColumnCount = COL_CYSPR_REGID + 1
                    .DataAutoCellTypes = False
                    .DataAutoSizeColumns = False
                    .DataAutoHeadings = False

                    .Columns(COL_CYSPR_CYSPRNMB).DataField = "CysprNmb"                             '番号
                    .Columns(COL_CYSPR_REGDT).DataField = "RegDT"                                   '登録日時
                    .Columns(COL_CYSPR_REGGRPCD).DataField = "RegGrpCD"                             '登録者グループCD
                    .Columns(COL_CYSPR_REGID).DataField = "RegID"                                   '登録者ID

                    .Columns(COL_CYSPR_REGDT).Visible = False
                    .Columns(COL_CYSPR_REGGRPCD).Visible = False
                    .Columns(COL_CYSPR_REGID).Visible = False

                End With

                '関連ファイル情報
                With .PropVwPrbFileInfo.Sheets(0)
                    .ColumnCount = COL_PRBFILE_FILEPATH + 1
                    .DataAutoCellTypes = False
                    .DataAutoSizeColumns = False
                    .DataAutoHeadings = False

                    .Columns(COL_PRBFILE_NAIYO).DataField = "FileNaiyo"                             '説明
                    .Columns(COL_PRBFILE_MNGNMB).DataField = "FileMngNmb"                           'ファイル番号_隠し
                    .Columns(COL_PRBFILE_FILEPATH).DataField = "FilePath"                           'ファイルパス_隠し

                    .Columns(COL_PRBFILE_MNGNMB).Visible = False
                    .Columns(COL_PRBFILE_FILEPATH).Visible = False
                End With


                '会議情報
                With .PropVwMeeting.Sheets(0)
                    .ColumnCount = COL_MEETING_RESULTKBNCD + 1
                    .DataAutoCellTypes = False
                    .DataAutoSizeColumns = False
                    .DataAutoHeadings = False

                    .Columns(COL_MEETING_NMB).DataField = "MeetingNmb"                            '会議番号
                    .Columns(COL_MEETING_JISISTDT).DataField = "JisiDT"                           '実施日
                    .Columns(COL_MEETING_RESULTKBN).DataField = "ResultKbnNM"                     '承認
                    .Columns(COL_MEETING_TITLE).DataField = "Title"                               'タイトル
                    .Columns(COL_MEETING_RESULTKBNCD).DataField = "ResultKbn"                     '承認コード（隠し）
                    '隠し列非表示
                    .Columns(COL_MEETING_RESULTKBNCD).Visible = False
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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフォームコントロールの設定を行う
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormControlPerProcMode(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'ログイン／ロックコントロール設定
            If SetLoginAndLockControl(dataHBKD0201) = False Then
                Return False
            End If

            'フッタ設定
            If SetFooterControl(dataHBKD0201) = False Then
                Return False
            End If

            'タブページ設定
            If SetTabControl(dataHBKD0201) = False Then
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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControl(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKD0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetLoginAndLockControlForNew(dataHBKD0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード

                    '編集モード用設定
                    If SetLoginAndLockControlForEdit(dataHBKD0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '作業予実モード

                    '作業予実モード用設定
                    If SetLoginAndLockControlForYojitsu(dataHBKD0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then        '参照モード

                    '参照モード用設定
                    If SetLoginAndLockControlForRef(dataHBKD0201) = False Then
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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForNew(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0201.PropGrpLoginUser

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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForEdit(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0201.PropGrpLoginUser

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

                'End If

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【作業予実モード】ログイン／ロックコントロール設定
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/21 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForYojitsu(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0201.PropGrpLoginUser

                'グループコンボボックス非活性
                .cmbGroup.Enabled = False

                '変更ボタン非活性
                .btnChange.Enabled = False

                'ロック情報表示
                .PropLockInfoVisible = True

                '解除ボタン表示
                .PropBtnUnlockVisible = True

                '関係者か？
                If dataHBKD0201.PropIntChkKankei <> KANKEI_CHECK_NONE Then
                    '解除ボタン表示
                    .PropBtnUnlockVisible = True

                    'ロックされているか？同じグループか？
                    If dataHBKD0201.PropBlnBeLockedFlg = True AndAlso dataHBKD0201.PropDtPrbInfoLock.Rows.Count > 0 AndAlso _
                       dataHBKD0201.PropDtPrbInfoLock.Rows(0).Item("EdiGrpCD").ToString.Equals(PropWorkGroupCD) Then
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
    ''' 【参照モード】ログイン／ロックコントロール設定
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/16 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForRef(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0201.PropGrpLoginUser

                'グループコンボボックス非活性
                .cmbGroup.Enabled = False

                '変更ボタン非活性
                .btnChange.Enabled = False

                'ロック情報表示
                .PropLockInfoVisible = False

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
    ''' 【共通】フッタコントロール設定
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControl(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKD0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    If .PropBlnFromCheckFlg = True Then

                        '新規登録モード(インシデント登録呼出時)用設定
                        If SetFooterControlForNew(dataHBKD0201) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnFromCheckFlg = False Then

                        '新規登録モード(インシデント外呼出時)用設定
                        If SetFooterControlForNew(dataHBKD0201) = False Then
                            Return False
                        End If

                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード

                    '編集モード用設定
                    If SetFooterControlForEdit(dataHBKD0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '作業予実モード

                    '作業予実モード用設定
                    If SetFooterControlForYojitsu(dataHBKD0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then         '参照モード

                    '参照モード用設定
                    If SetFooterControlForRef(dataHBKD0201) = False Then
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
    ''' 【新規登録モード（インシデント登録呼出時）】フッタコントロール設定
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデント登録画面から呼ばれた際の新規登録モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForNewFromInc(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0201

                '活性／非活性設定
                '対応関係者情報
                .PropBtnAddRow_RelaG.Enabled = True         '「+G」
                .PropBtnAddRow_RelaU.Enabled = True         '「+U」
                .PropBtnRemoveRow_Rela.Enabled = True       '「-」
                'プロセスリンク情報
                .PropBtnAddRow_Plink.Enabled = True         '「+」
                .PropBtnRemoveRow_Plink.Enabled = True      '「-」
                'CYSPR情報
                .PropBtnAddRow_Cyspr.Enabled = True         '「+」
                .PropBtnRemoveRow_Cyspr.Enabled = True      '「-」
                '関連ファイル情報
                .PropBtnAddRow_File.Enabled = True          '「+」
                .PropBtnRemoveRow_File.Enabled = True       '「-」
                .PropBtnOpenFile.Enabled = False            '「開」
                .PropBtnSaveFile.Enabled = False            '「ダ」
                'フッタ
                .PropBtnReg.Enabled = True                  '登録／作業予実登録ボタン
                .PropBtnMail.Enabled = True                 'メール作成ボタン
                '★リリース対応 2012/08/29
                .PropBtnHenkou.Enabled = False              '変更登録ボタン
                .PropBtnPrint.Enabled = True                '単票出力ボタン

                'ボタン表示切り替え
                .PropBtnReg.Text = "登録"                   '登録／作業予実登録ボタン
                .PropBtnReturn.Text = "閉じる"              '戻る／閉じるボタン

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForNew(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0201

                '活性／非活性設定
                '対応関係者情報
                .PropBtnAddRow_RelaG.Enabled = True         '「+G」
                .PropBtnAddRow_RelaU.Enabled = True         '「+U」
                .PropBtnRemoveRow_Rela.Enabled = True       '「-」
                'プロセスリンク情報
                .PropBtnAddRow_Plink.Enabled = True         '「+」
                .PropBtnRemoveRow_Plink.Enabled = True      '「-」
                'CYSPR情報
                .PropBtnAddRow_Cyspr.Enabled = True         '「+」
                .PropBtnRemoveRow_Cyspr.Enabled = True      '「-」
                '関連ファイル情報
                .PropBtnAddRow_File.Enabled = True          '「+」
                .PropBtnRemoveRow_File.Enabled = True       '「-」
                .PropBtnOpenFile.Enabled = False            '「開」
                .PropBtnSaveFile.Enabled = False            '「ダ」
                'フッタ
                .PropBtnReg.Enabled = True                  '登録／作業予実登録ボタン
                .PropBtnMail.Enabled = True                 'メール作成ボタン
                '★リリース対応 2012/08/29
                .PropBtnHenkou.Enabled = False                '変更登録ボタン
                .PropBtnPrint.Enabled = True                '単票出力ボタン

                'ボタン表示切り替え
                .PropBtnReg.Text = "登録"                   '登録／作業予実登録ボタン

                '呼び出し元に応じて変更
                If .PropIntOwner = SCR_CALLMOTO_REG Then
                    .PropBtnReturn.Text = "閉じる"
                Else
                    '.PropBtnReturn.Text = "戻る"
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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForEdit(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0201

                '活性／非活性設定
                '対応関係者情報
                .PropBtnAddRow_RelaG.Enabled = True         '「+G」
                .PropBtnAddRow_RelaU.Enabled = True         '「+U」
                .PropBtnRemoveRow_Rela.Enabled = True       '「-」
                'プロセスリンク情報
                .PropBtnAddRow_Plink.Enabled = True         '「+」
                .PropBtnRemoveRow_Plink.Enabled = True      '「-」
                'CYSPR情報
                .PropBtnAddRow_Cyspr.Enabled = True         '「+」
                .PropBtnRemoveRow_Cyspr.Enabled = True      '「-」
                '関連ファイル情報
                .PropBtnAddRow_File.Enabled = True          '「+」
                .PropBtnRemoveRow_File.Enabled = True       '「-」
                .PropBtnOpenFile.Enabled = True             '「開」
                .PropBtnSaveFile.Enabled = True             '「ダ」
                'フッタ
                .PropBtnReg.Enabled = True                  '登録／作業予実登録ボタン
                .PropBtnMail.Enabled = True                 'メール作成ボタン
                '★リリース対応 2012/08/29
                .PropBtnHenkou.Enabled = True               '変更登録ボタン
                .PropBtnPrint.Enabled = True                '単票出力ボタン

                'ボタン表示切り替え
                .PropBtnReg.Text = "登録"                   '登録／作業予実登録ボタン

                '呼び出し元に応じて変更
                If .PropIntOwner = SCR_CALLMOTO_REG Then
                    .PropBtnReturn.Text = "閉じる"
                Else
                    '.PropBtnReturn.Text = "戻る"
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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForRef(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0201

                '活性／非活性設定
                '対応関係者情報
                .PropBtnAddRow_RelaG.Enabled = False         '「+G」
                .PropBtnAddRow_RelaU.Enabled = False         '「+U」
                .PropBtnRemoveRow_Rela.Enabled = False       '「-」
                'プロセスリンク情報
                .PropBtnAddRow_Plink.Enabled = False         '「+」
                .PropBtnRemoveRow_Plink.Enabled = False      '「-」
                'CYSPR情報
                .PropBtnAddRow_Cyspr.Enabled = False         '「+」
                .PropBtnRemoveRow_Cyspr.Enabled = False      '「-」
                '関連ファイル情報
                .PropBtnAddRow_File.Enabled = False          '「+」
                .PropBtnRemoveRow_File.Enabled = False       '「-」
                .PropBtnOpenFile.Enabled = True              '「開」
                .PropBtnSaveFile.Enabled = True              '「ダ」
                'フッタ
                .PropBtnReg.Enabled = False                  '登録／作業予実登録ボタン
                .PropBtnMail.Enabled = False                  'メール作成ボタン
                '★リリース対応 2012/08/29
                .PropBtnHenkou.Enabled = False                  '変更登録ボタン
                .PropBtnPrint.Enabled = False                 '単票出力ボタン

                'ボタン表示切り替え
                .PropBtnReg.Text = "登録"                    '登録／作業予実登録ボタン

                '呼び出し元に応じて変更
                If .PropIntOwner = SCR_CALLMOTO_REG Then
                    .PropBtnReturn.Text = "閉じる"
                Else
                    '.PropBtnReturn.Text = "戻る"
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
    ''' 【作業予実モード】フッタコントロール設定
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業予実モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForYojitsu(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0201

                '活性／非活性設定
                '対応関係者情報
                .PropBtnAddRow_RelaG.Enabled = False         '「+G」
                .PropBtnAddRow_RelaU.Enabled = False         '「+U」
                .PropBtnRemoveRow_Rela.Enabled = False       '「-」
                'プロセスリンク情報
                .PropBtnAddRow_Plink.Enabled = False         '「+」
                .PropBtnRemoveRow_Plink.Enabled = False      '「-」
                'CYSPR情報
                .PropBtnAddRow_Cyspr.Enabled = False         '「+」
                .PropBtnRemoveRow_Cyspr.Enabled = False      '「-」
                '関連ファイル情報
                .PropBtnAddRow_File.Enabled = False          '「+」
                .PropBtnRemoveRow_File.Enabled = False       '「-」
                .PropBtnOpenFile.Enabled = True              '「開」
                .PropBtnSaveFile.Enabled = True              '「ダ」
                'フッタ
                .PropBtnReg.Enabled = True                  '登録／作業予実登録ボタン
                .PropBtnMail.Enabled = False                  'メール作成ボタン
                '★リリース対応 2012/08/29
                .PropBtnHenkou.Enabled = False                '変更登録ボタン
                .PropBtnPrint.Enabled = False                 '単票出力ボタン

                'ボタン表示切り替え
                .PropBtnReg.Text = "作業予実登録"            '登録／作業予実登録ボタン

                '呼び出し元に応じて変更
                If .PropIntOwner = SCR_CALLMOTO_REG Then
                    .PropBtnReturn.Text = "閉じる"
                Else
                    '.PropBtnReturn.Text = "戻る"
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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてタブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControl(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '基本情報タブ設定
            If SetTabControlKihon(dataHBKD0201) = False Then
                Return False
            End If

            '会議情報タブ設定
            If SetTabControlMeeting(dataHBKD0201) = False Then
                Return False
            End If

            'フリー入力情報タブ設定
            If SetTabControlFree(dataHBKD0201) = False Then
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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKihon(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    If .PropBlnFromCheckFlg = True Then

                        '初期設定無し

                    ElseIf .PropBlnFromCheckFlg = False Then

                        '初期設定無し

                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード

                    '編集モード用設定
                    If SetTabControlKhnForEdit(dataHBKD0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '作業予実モード

                    '作業予実モード用設定
                    If SetTabControlKhnForYojitsu(dataHBKD0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then         '参照モード

                    '参照モード用設定
                    If SetTabControlKhnForRef(dataHBKD0201) = False Then
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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForEdit(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0201


                '基本情報タブ
                .PropBtnStartDT_HM.Enabled = True               '時（開始日時）
                .PropBtnKanryoDT_HM.Enabled = True              '時（完了日時）
                .PropBtnTantoSearch.Enabled = True              '検索（担当者）
                .PropBtnTantoMe.Enabled = True                  '私（担当者）
                .PropBtnApproverSearch.Enabled = True           '検索（対処承認者）
                .PropBtnApproverMe.Enabled = True               '私（対処承認者）
                .PropBtnRecorder.Enabled = True                 '検索（承認記録者）
                .PropBtnRecorderMe.Enabled = True               '私（承認記録者）
                .PropBtnKakudai.Enabled = True                  '拡大
                .PropBtnRefresh.Enabled = True                  'リフレッシュ
                .PropBtnAddRow_Yojitsu.Enabled = True           '（作業予実）「+」
                .PropBtnRemoveRow_Yojitsu.Enabled = True        '（作業予実）「-」

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【作業予実モード】基本情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業予実モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForYojitsu(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0201

                '基本情報タブ
                .PropBtnStartDT_HM.Enabled = False               '時（開始日時）
                .PropBtnKanryoDT_HM.Enabled = False              '時（完了日時）
                .PropBtnTantoSearch.Enabled = False              '検索（担当者）
                .PropBtnTantoMe.Enabled = False                  '私（担当者）
                .PropBtnApproverSearch.Enabled = False           '検索（対処承認者）
                .PropBtnApproverMe.Enabled = False               '私（対処承認者）
                .PropBtnRecorder.Enabled = False                 '検索（承認記録者）
                .PropBtnRecorderMe.Enabled = False               '私（承認記録者）
                .PropBtnKakudai.Enabled = True                   '拡大
                .PropBtnRefresh.Enabled = True                   'リフレッシュ
                .PropBtnAddRow_Yojitsu.Enabled = True            '（作業予実）「+」
                .PropBtnRemoveRow_Yojitsu.Enabled = True         '（作業予実）「-」

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForRef(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0201

                '基本情報タブ
                .PropBtnStartDT_HM.Enabled = False               '時（開始日時）
                .PropBtnKanryoDT_HM.Enabled = False              '時（完了日時）
                .PropBtnTantoSearch.Enabled = False              '検索（担当者）
                .PropBtnTantoMe.Enabled = False                  '私（担当者）
                .PropBtnApproverSearch.Enabled = False           '検索（対処承認者）
                .PropBtnApproverMe.Enabled = False               '私（対処承認者）
                .PropBtnRecorder.Enabled = False                 '検索（承認記録者）
                .PropBtnRecorderMe.Enabled = False               '私（承認記録者）
                '[Mod] 2012/10/18 s.yamaguchi 非活性解除(False→True) START
                .PropBtnKakudai.Enabled = True                   '拡大
                '[Mod] 2012/10/18 s.yamaguchi 非活性解除(False→True) END
                .PropBtnRefresh.Enabled = False                  'リフレッシュ
                .PropBtnAddRow_Yojitsu.Enabled = False           '（作業予実）「+」
                .PropBtnRemoveRow_Yojitsu.Enabled = False        '（作業予実）「-」

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて会議情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlMeeting(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    If .PropBlnFromCheckFlg = True Then

                        '初期設定無し
                        If SetTabControlMeetingForNew(dataHBKD0201) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnFromCheckFlg = False Then

                        '初期設定無し
                        If SetTabControlMeetingForNew(dataHBKD0201) = False Then
                            Return False
                        End If

                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード

                    '※新規モード用設定と同じ
                    If SetTabControlMeetingForEdit(dataHBKD0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '作業予実モード

                    '※新規モード用設定と同じ
                    If SetTabControlMeetingForNew(dataHBKD0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then         '参照モード

                    '※新規モード用設定と同じ
                    If SetTabControlMeetingForNew(dataHBKD0201) = False Then
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
    ''' 【新規モード／作業予実／参照モード】会議情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて会議情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlMeetingForNew(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0201

                .PropBtnAddRow_Meeting.Enabled = False          '「+」ボタン
                .PropBtnRemoveRow_Meeting.Enabled = False       '「-」ボタン

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて会議情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlMeetingForEdit(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0201

                .PropBtnAddRow_Meeting.Enabled = True          '「+」ボタン
                .PropBtnRemoveRow_Meeting.Enabled = True       '「-」ボタン

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフリー入力情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlFree(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    If .PropBlnFromCheckFlg = True Then

                        '※編集モード用設定と同じ
                        If SetTabControlFreeForEdit(dataHBKD0201) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnFromCheckFlg = False Then

                        '※編集モード用設定と同じ
                        If SetTabControlFreeForEdit(dataHBKD0201) = False Then
                            Return False
                        End If

                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード

                    '編集モード用設定
                    If SetTabControlFreeForEdit(dataHBKD0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '作業予実モード

                    '※参照モード用設定と同じ
                    If SetTabControlFreeForRef(dataHBKD0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then         '参照モード

                    '参照モード用設定
                    If SetTabControlFreeForRef(dataHBKD0201) = False Then
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
    ''' 【新規／編集モード】フリー入力情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフリー入力情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlFreeForEdit(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0201

                'フリーテキスト１～５テキストボックス
                .PropTxtFreeText1.ReadOnly = False
                .PropTxtFreeText2.ReadOnly = False
                .PropTxtFreeText3.ReadOnly = False
                .PropTxtFreeText4.ReadOnly = False
                .PropTxtFreeText5.ReadOnly = False

                'フリーフラグ１～５チェックボックス
                .PropChkFreeFlg1.Enabled = True
                .PropChkFreeFlg2.Enabled = True
                .PropChkFreeFlg3.Enabled = True
                .PropChkFreeFlg4.Enabled = True
                .PropChkFreeFlg5.Enabled = True

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【作業予実／参照モード】フリー入力情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフリー入力情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlFreeForRef(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0201

                'フリーテキスト１～５テキストボックス
                .PropTxtFreeText1.ReadOnly = True
                .PropTxtFreeText2.ReadOnly = True
                .PropTxtFreeText3.ReadOnly = True
                .PropTxtFreeText4.ReadOnly = True
                .PropTxtFreeText5.ReadOnly = True

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
    ''' 【共通】初期表示用データ取得処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            'マスタデータ取得
            If GetMastaData(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'メイン表示データ取得
            If GetMainData(Adapter, Cn, dataHBKD0201) = False Then
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
    ''' 【共通】マスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>マスタデータを取得する
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMastaData(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'プロセスステータスマスタ取得
            If GetProcessStateMst(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            '問題発生原因マスタ取得
            If GetProblemCaseMst(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'グループマスタ取得
            If GetTantoGrpMst(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            '作業ステータスマスタ
            If GetWorkStateMst(Adapter, Cn, dataHBKD0201) = False Then
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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスステータスマスタデータを取得する
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetProcessStateMst(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtMaster As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKD0201.SetSelectProcessStateMst(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "プロセスステータスマスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtMaster)

            'データが取得できなかった場合、エラー
            If dtMaster.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & D0201_E001, TBNM_PROCESSSTATE_MTB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKD0201.PropDtProcessState = dtMaster

            '終了ログ出力
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
            dtMaster.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】問題発生原因マスタ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>問題発生原因マスタデータを取得する
    ''' <para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetProblemCaseMst(ByVal Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtMaster As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKD0201.SetSelectProblemCaseMst(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "問題発生原因マスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtMaster)

            'データが取得できなかった場合、エラー
            If dtMaster.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & D0201_E001, TBNM_PROBLEM_CASE_MTB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKD0201.PropDtProblemCase = dtMaster

            '終了ログ出力
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
            dtMaster.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】グループマスタ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>グループマスタデータを取得する
    ''' <para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetTantoGrpMst(ByVal Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtMaster As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKD0201.SetSelectTantoGrpMst(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "グループマスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtMaster)

            'データが取得できなかった場合、エラー
            If dtMaster.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & D0201_E001, TBNM_GRP_MTB)
                'puErrMsg = D0201_E001
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKD0201.PropDtTantoGrp = dtMaster

            '終了ログ出力
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
            dtMaster.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】作業ステータスマスタ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業ステータスマスタデータを取得する
    ''' <para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetWorkStateMst(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtMaster As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKD0201.SetSelectWorkStateMst(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "作業ステータスマスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtMaster)

            'データが取得できなかった場合、エラー
            If dtMaster.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & D0201_E001, TBNM_WORKSTATE_MTB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKD0201.PropDtWorkState = dtMaster

            '終了ログ出力
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
            dtMaster.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】初期表示用メインデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainData(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    If .PropBlnFromCheckFlg = True Then

                        'インシデント登録画面からの呼出

                        '新規モード用データ取得
                        If GetMainDataForNewFromInc(Adapter, Cn, dataHBKD0201) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnFromCheckFlg = False Then

                        'インシデント登録画面外からの呼出

                        '新規モード用データ取得
                        If GetMainDataForNew(Adapter, Cn, dataHBKD0201) = False Then
                            Return False
                        End If

                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード

                    '編集モード用データ取得
                    If GetMainDataForEdit(Adapter, Cn, dataHBKD0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '作業予実モード

                    '作業予実モード用データ取得　※編集モードと同じ
                    If GetMainDataForEdit(Adapter, Cn, dataHBKD0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then         '参照モード

                    '参照モード用データ取得　※編集モードと同じ
                    If GetMainDataForEdit(Adapter, Cn, dataHBKD0201) = False Then
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
    ''' 【新規モード(インシデント登録呼出時)】初期表示用メインデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規モードで初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainDataForNewFromInc(ByVal Adapter As NpgsqlDataAdapter, _
                                              ByVal Cn As NpgsqlConnection, _
                                              ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '対象システムデータ取得
            If GetTargetSystem(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            ''プロセスリンクデータ取得
            'If GetProcessLink(Adapter, Cn, dataHBKD0201) = False Then
            '    Return False
            'End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【新規モード】初期表示用メインデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規モードで初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainDataForNew(ByVal Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '対象システムデータ取得
            If GetTargetSystem(Adapter, Cn, dataHBKD0201) = False Then
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
    ''' 【編集／参照／作業予実モード】初期表示用メインデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集／参照モードで初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainDataForEdit(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '対象システムデータ取得
            If GetTargetSystem(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            '問題共通情報データ取得
            If GetProblemInfo(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            '担当履歴情報データ取得
            If GetTantoRireki(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            '問題履歴データ取得
            If GetProblemWkRireki(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            '作業担当データ取得
            If GetProblemWkTanto(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            '対応関係者データ取得
            If GetProblemKankei(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'プロセスリンク情報データ取得
            If GetProcessLink(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            '問題CYSPR情報データ取得
            If GetProblemCyspr(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            '関連ファイルデータ取得
            If GetProblemFile(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            '会議情報データ取得
            If GetMeeting(Adapter, Cn, dataHBKD0201) = False Then
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
    ''' 【共通】リフレッシュ時用作業履歴データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>リフレッシュ用の作業履歴データを取得する
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetRirekiDataForRefrash(ByVal Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '作業履歴データ取得
            If GetProblemWkRireki(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            '作業担当データ取得
            If GetProblemWkTanto(Adapter, Cn, dataHBKD0201) = False Then
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
    ''' 【共通】対象システムデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対象システムデータを取得する
    ''' <para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetTargetSystem(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtTableData As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKD0201.SetSelectTargetSystemData(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対象システムデータ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtTableData)

            '取得データをデータクラスにセット
            dataHBKD0201.PropDtTargetSystem = dtTableData

            '終了ログ出力
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
            dtTableData.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【モード】問題基本情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>問題基本情報データを取得する
    ''' <para>作成情報：2012/08/16 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetProblemInfo(ByVal Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtTableData As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKD0201.SetSelectProblemInfoData(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "問題共通情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtTableData)

            '取得データをデータクラスにセット
            dataHBKD0201.PropDtProblemInfo = dtTableData

            '終了ログ出力
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
            dtTableData.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【モード】問題作業履歴データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>問題作業履歴データを取得する
    ''' <para>作成情報：2012/08/16 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetProblemWkRireki(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtTableData As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKD0201.SetSelectProblemWkRirekiData(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "問題作業履歴データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtTableData)

            '取得データをデータクラスにセット
            dataHBKD0201.PropDtProblemWkRireki = dtTableData

            '終了ログ出力
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
            dtTableData.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【モード】作業担当データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業担当データを取得する
    ''' <para>作成情報：2012/08/16 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetProblemWkTanto(ByVal Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtTableData As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKD0201.SetSelectProblemWkTantoData(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "作業担当データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtTableData)

            '取得データをデータクラスにセット
            dataHBKD0201.PropDtProblemWkTanto = dtTableData

            '終了ログ出力
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
            dtTableData.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【モード】問題対応関係データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>問題対応関係データを取得する
    ''' <para>作成情報：2012/08/16 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetProblemKankei(ByVal Adapter As NpgsqlDataAdapter, _
                                      ByVal Cn As NpgsqlConnection, _
                                      ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtTableData As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKD0201.SetSelectProblemKankeiData(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "問題対応関係データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtTableData)

            '取得データをデータクラスにセット
            dataHBKD0201.PropDtProblmKankei = dtTableData

            '終了ログ出力
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
            dtTableData.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【モード】プロセスリンク情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンク情報データを取得する
    ''' <para>作成情報：2012/08/16 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetProcessLink(ByVal Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtTableData As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKD0201.SetSelectProcessLinkData(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "プロセスリンク情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtTableData)

            '取得データをデータクラスにセット
            dataHBKD0201.PropDtProcessLink = dtTableData

            '終了ログ出力
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
            dtTableData.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【モード】問題CYSPR情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CYSPR情報データを取得する
    ''' <para>作成情報：2012/08/16 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetProblemCyspr(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtTableData As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKD0201.SetSelectProblemCysprData(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "問題CYSPR情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtTableData)

            '取得データをデータクラスにセット
            dataHBKD0201.PropDtProblemCyspr = dtTableData

            '終了ログ出力
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
            dtTableData.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【モード】問題関連ファイル情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関連ファイル情報データを取得する
    ''' <para>作成情報：2012/08/16 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetProblemFile(ByVal Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtTableData As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKD0201.SetSelectProblemFileData(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "問題関連ファイル情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtTableData)

            '取得データをデータクラスにセット
            dataHBKD0201.PropDtProblemFile = dtTableData

            '終了ログ出力
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
            dtTableData.Dispose()
        End Try

    End Function


    ''' <summary> 
    ''' 【共通】初期データ設定処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームコントロールに初期表示データを設定する
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitDataToControl(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'ログイン／ロックデータ設定
            If SetDataToLoginAndLock(dataHBKD0201) = False Then
                Return False
            End If

            'タブコントロールデータ設定
            If SetDataToTabControl(dataHBKD0201) = False Then
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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLock(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKD0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    If .PropBlnFromCheckFlg = True Then

                        '新規登録モード用設定 ※新規登録画面と同じ
                        If SetDataToLoginAndLockForNew(dataHBKD0201) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnFromCheckFlg = False Then

                        '新規登録モード用設定
                        If SetDataToLoginAndLockForNew(dataHBKD0201) = False Then
                            Return False
                        End If

                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード

                    '編集モード用設定
                    If SetDataToLoginAndLockForEdit(dataHBKD0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '作業予実モード

                    '作業予実モード用設定
                    If SetDataToLoginAndLockForYojitsu(dataHBKD0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '参照モード

                    '参照モード用設定
                    If SetDataToLoginAndLockForRef(dataHBKD0201) = False Then
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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForNew(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKD0201.PropGrpLoginUser

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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForEdit(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKD0201.PropGrpLoginUser

                'ロック情報が取得できた場合
                If dataHBKD0201.PropDtPrbInfoLock.Rows.Count > 0 Then
                    'ロック開始日時
                    Dim dtmLockTime As DateTime = Nothing  '編集開始日時
                    If dataHBKD0201.PropDtPrbInfoLock.Rows(0).Item("EdiTime").ToString() <> "" Then
                        dtmLockTime = dataHBKD0201.PropDtPrbInfoLock.Rows(0).Item("EdiTime")
                        dataHBKD0201.PropStrEdiTime = dtmLockTime
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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForRef(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKD0201.PropGrpLoginUser

                'ロック情報が取得できた場合
                If dataHBKD0201.PropDtPrbInfoLock.Rows.Count > 0 Then
                    'ロック開始日時
                    Dim dtmLockTime As DateTime = Nothing
                    If dataHBKD0201.PropDtPrbInfoLock.Rows(0).Item("EdiTime").ToString() <> "" Then
                        dtmLockTime = dataHBKD0201.PropDtPrbInfoLock.Rows(0).Item("EdiTime")
                        dataHBKD0201.PropStrEdiTime = dtmLockTime
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
    ''' 【作業予実モード】ログイン／ロックデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForYojitsu(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKD0201.PropGrpLoginUser

                'ロック開始日時 ※空文字のときはNothingを指定しないと落ちる
                Dim strLockTime As String = dataHBKD0201.PropStrEdiTime
                If dataHBKD0201.PropDtPrbInfoLock IsNot Nothing AndAlso dataHBKD0201.PropDtPrbInfoLock.Rows.Count > 0 Then
                    .PropLockDate = dataHBKD0201.PropDtPrbInfoLock.Rows(0).Item("EdiTime")
                ElseIf strLockTime = "" Then
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
    ''' 【共通】タブコントロールデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>タブコントロールデータを初期設定する
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabControl(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '基本情報タブデータ設定
            If SetDataToTabKhn(dataHBKD0201) = False Then
                Return False
            End If

            '会議情報タブデータ設定
            If SetDataToTabMeeting(dataHBKD0201) = False Then
                Return False
            End If

            'フリー入力タブデータ設定
            If SetDataToTabFree(dataHBKD0201) = False Then
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
    ''' <param name="dataHBKD0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhn(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKD0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    If .PropBlnFromCheckFlg = True Then

                        'インシデント登録画面からの呼出
                        If SetDataToTabKhnForNewFromInc(dataHBKD0201) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnFromCheckFlg = False Then

                        '新規登録モード用設定
                        If SetDataToTabKhnForNew(dataHBKD0201) = False Then
                            Return False
                        End If

                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード

                    '編集モード用設定
                    If SetDataToTabKhnForEdit(dataHBKD0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '作業履歴モード

                    '作業履歴モード用設定（編集モードと同じ）
                    If SetDataToTabKhnForEdit(dataHBKD0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then         '参照モード

                    '参照モード用設定（編集モードと同じ）
                    If SetDataToTabKhnForEdit(dataHBKD0201) = False Then
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
    ''' 【新規登録モード（インシデント登録画面呼出）】基本情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデント登録画面からの新規登録モードで基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhnForNewFromInc(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'コンボボックス作成
            If CreateCmb(dataHBKD0201) = False Then
                Return False
            End If

            'スプレッドセルタイプ作成
            If CreateSpreadCtype(dataHBKD0201) = False Then
                Return False
            End If

            With dataHBKD0201

                .PropCmbStatus.SelectedValue = ""                               '基本情報タブ：ステータス
                .PropDtpStartDT.txtDate.Text = ""                               '基本情報タブ：開始日時（日付）
                .PropTxtStartDT_HM.PropTxtTime.Text = ""                        '基本情報タブ：開始日時（時刻）
                .PropDtpKanryoDT.txtDate.Text = ""                              '基本情報タブ：完了日時（日付）
                .PropTxtKanryoDT_HM.PropTxtTime.Text = ""                       '基本情報タブ：完了日時（時刻）
                .PropCmbTargetSystem.PropCmbColumns.SelectedValue = .PropIntTSystemNmb.ToString()       '基本情報タブ：対象システム
                .PropCmbPrbCase.SelectedValue = ""                              '基本情報タブ：発生原因
                .PropTxtTitle.Text = ""                                         '基本情報タブ：タイトル
                .PropTxtNaiyo.Text = ""                                         '基本情報タブ：内容
                .PropTxtTaisyo.Text = ""                                        '基本情報タブ：対処
                .PropCmbTantoGrp.SelectedValue = ""                             '基本情報タブ：担当グループ
                .PropTxtPrbTantoID.Text = ""                                    '基本情報タブ：担当ID
                .PropTxtPrbTantoNM.Text = ""                                    '基本情報タブ：担当氏名
                .PropTxtApproverID.Text = ""                                    '基本情報タブ：対処承認者ID
                .PropTxtApproverNM.Text = ""                                    '基本情報タブ：対処承認者氏名
                .PropTxtRecorderID.Text = ""                                    '基本情報タブ：承認記録者ID
                .PropTxtRecorderNM.Text = ""                                    '基本情報タブ：承認記録者氏名
                .PropTxtGrpRireki.Text = ""                                     '対応履歴情報：グループ履歴
                .PropTxtTantoRireki.Text = ""                                   '対応履歴情報：担当者履歴

                .PropVwPrbYojitsu.DataSource = .PropDtwkRireki                  '作業履歴スプレッド
                .PropVwRelationInfo.DataSource = .PropDtProblmKankei            '対応関係者情報：対応関係者情報スプレッド

                'インシデント画面の情報を挿入する
                Dim drProcessLink As DataRow
                drProcessLink = .PropDtProcessLink.NewRow()
                drProcessLink(COL_PLINK_PLINKKBN) = PROCESS_TYPE_INCIDENT_NAME_R
                drProcessLink(COL_PLINK_PLINKNO) = .PropIntIncNmb
                drProcessLink(COL_PLINK_PLINKKBNCD) = PROCESS_TYPE_INCIDENT
                'DataTableに保存
                .PropDtProcessLink.Rows.Add(drProcessLink)

                'インシデント登録画面のプロセスリンク情報を挿入する
                For i As Integer = 0 To .PropVwProcessLinkInfo_Save.Sheets(0).Rows.Count - 1 Step 1
                    drProcessLink = .PropDtProcessLink.NewRow()
                    drProcessLink(COL_PLINK_PLINKKBN) = .PropVwProcessLinkInfo_Save.Sheets(0).GetText(i, COL_PLINK_PLINKKBN)
                    drProcessLink(COL_PLINK_PLINKNO) = .PropVwProcessLinkInfo_Save.Sheets(0).GetText(i, COL_PLINK_PLINKNO)
                    drProcessLink(COL_PLINK_PLINKKBNCD) = .PropVwProcessLinkInfo_Save.Sheets(0).GetText(i, COL_PLINK_PLINKKBNCD)
                    'DataTableに保存
                    .PropDtProcessLink.Rows.Add(drProcessLink)
                Next
                .PropVwProcessLinkInfo.DataSource = .PropDtProcessLink 'プロセスリンク情報：プロセスリンク情報スプレッド
                .PropVwCysprInfo.DataSource = .PropDtProblemCyspr               'CYSPR情報：CYSPR情報スプレッド
                .PropVwPrbFileInfo.DataSource = .PropDtProblemFile              '関連ファイル情報：関連ファイル情報スプレッド

                'メール
                .PropStrRegGp = ""
                .PropStrRegUsr = ""
                .PropStrRegDT = ""
                .PropStrUpdateGp = ""
                .PropStrUpdateUsr = ""
                .PropStrUpdateDT = ""

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードで基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhnForNew(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'コンボボックス作成
            If CreateCmb(dataHBKD0201) = False Then
                Return False
            End If

            'スプレッドセルタイプ作成
            If CreateSpreadCtype(dataHBKD0201) = False Then
                Return False
            End If

            With dataHBKD0201

                .PropCmbStatus.SelectedValue = ""                               '基本情報タブ：ステータス
                .PropDtpStartDT.txtDate.Text = ""                               '基本情報タブ：開始日時（日付）
                .PropTxtStartDT_HM.PropTxtTime.Text = ""                        '基本情報タブ：開始日時（時刻）
                .PropDtpKanryoDT.txtDate.Text = ""                              '基本情報タブ：完了日時（日付）
                .PropTxtKanryoDT_HM.PropTxtTime.Text = ""                       '基本情報タブ：完了日時（時刻）
                .PropCmbTargetSystem.PropCmbColumns.Text = ""                   '基本情報タブ：対象システム
                .PropCmbPrbCase.SelectedValue = ""                              '基本情報タブ：発生原因
                .PropTxtTitle.Text = ""                                         '基本情報タブ：タイトル
                .PropTxtNaiyo.Text = ""                                         '基本情報タブ：内容
                .PropTxtTaisyo.Text = ""                                        '基本情報タブ：対処
                .PropCmbTantoGrp.SelectedValue = ""                             '基本情報タブ：担当グループ
                .PropTxtPrbTantoID.Text = ""                                    '基本情報タブ：担当ID
                .PropTxtPrbTantoNM.Text = ""                                    '基本情報タブ：担当氏名
                .PropTxtApproverID.Text = ""                                    '基本情報タブ：対処承認者ID
                .PropTxtApproverNM.Text = ""                                    '基本情報タブ：対処承認者氏名
                .PropTxtRecorderID.Text = ""                                    '基本情報タブ：承認記録者ID
                .PropTxtRecorderNM.Text = ""                                    '基本情報タブ：承認記録者氏名
                .PropTxtGrpRireki.Text = ""                                     '対応履歴情報：グループ履歴
                .PropTxtTantoRireki.Text = ""                                   '対応履歴情報：担当者履歴

                .PropVwPrbYojitsu.DataSource = .PropDtwkRireki                  '作業履歴スプレッド
                .PropVwRelationInfo.DataSource = .PropDtProblmKankei            '対応関係者情報：対応関係者情報スプレッド
                .PropVwProcessLinkInfo.DataSource = .PropDtProcessLink          'プロセスリンク情報：プロセスリンク情報スプレッド
                .PropVwCysprInfo.DataSource = .PropDtProblemCyspr               'CYSPR情報：CYSPR情報スプレッド
                .PropVwPrbFileInfo.DataSource = .PropDtProblemFile              '関連ファイル情報：関連ファイル情報スプレッド

                'メール
                .PropStrRegGp = ""
                .PropStrRegUsr = ""
                .PropStrRegDT = ""
                .PropStrUpdateGp = ""
                .PropStrUpdateUsr = ""
                .PropStrUpdateDT = ""

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集／参照／作業履歴モード】基本情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhnForEdit(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'コンボボックス作成
            If CreateCmb(dataHBKD0201) = False Then
                Return False
            End If

            'スプレッドセルタイプ作成
            If CreateSpreadCtype(dataHBKD0201) = False Then
                Return False
            End If

            With dataHBKD0201

                '基本情報
                .PropTxtPrbNmb.Text = .PropIntPrbNmb.ToString()                                             '問題番号
                .PropLblRegInfo_out.Text = .PropDtProblemInfo.Rows(0).Item("LblRegInfo")                    '登録情報
                .PropLblUpdateInfo_out.Text = .PropDtProblemInfo.Rows(0).Item("LblUpdateInfo")              '最終更新情報

                .PropCmbStatus.SelectedValue = .PropDtProblemInfo.Rows(0).Item("ProcessStateCD").ToString   'ステータス

                '開始日時
                If .PropDtProblemInfo.Rows(0).Item("KaisiDT").ToString.Equals("") Then
                    .PropDtpStartDT.txtDate.Text = ""
                    .PropTxtStartDT_HM.PropTxtTime.Text = ""
                Else
                    .PropDtpStartDT.txtDate.Text = .PropDtProblemInfo.Rows(0).Item("KaisiDT").ToString.Substring(0, 10)
                    .PropTxtStartDT_HM.PropTxtTime.Text = .PropDtProblemInfo.Rows(0).Item("KaisiDT").ToString.Substring(11, 5)
                End If

                '完了日時
                If .PropDtProblemInfo.Rows(0).Item("KanryoDT").ToString.Equals("") Then
                    .PropDtpKanryoDT.txtDate.Text = ""
                    .PropTxtKanryoDT_HM.PropTxtTime.Text = ""
                Else
                    .PropDtpKanryoDT.txtDate.Text = .PropDtProblemInfo.Rows(0).Item("KanryoDT").ToString.Substring(0, 10)
                    .PropTxtKanryoDT_HM.PropTxtTime.Text = .PropDtProblemInfo.Rows(0).Item("KanryoDT").ToString.Substring(11, 5)
                End If

                .PropCmbTargetSystem.PropCmbColumns.SelectedValue = .PropDtProblemInfo.Rows(0).Item("SystemNmb").ToString()         '基本情報タブ：対象システム
                .PropCmbPrbCase.SelectedValue = .PropDtProblemInfo.Rows(0).Item("PrbCaseCD").ToString                               '基本情報タブ：発生原因
                .PropTxtTitle.Text = .PropDtProblemInfo.Rows(0).Item("Title").ToString                                              '基本情報タブ：タイトル
                .PropTxtNaiyo.Text = .PropDtProblemInfo.Rows(0).Item("Naiyo").ToString                                              '基本情報タブ：内容
                .PropTxtTaisyo.Text = .PropDtProblemInfo.Rows(0).Item("Taisyo").ToString                                            '基本情報タブ：対処
                .PropCmbTantoGrp.SelectedValue = .PropDtProblemInfo.Rows(0).Item("TantoGrpCD").ToString                             '基本情報タブ：担当グループ
                .PropTxtPrbTantoID.Text = .PropDtProblemInfo.Rows(0).Item("PrbTantoID").ToString                                    '基本情報タブ：担当ID
                .PropTxtPrbTantoNM.Text = .PropDtProblemInfo.Rows(0).Item("PrbTantoNM").ToString                                    '基本情報タブ：担当氏名
                .PropTxtApproverID.Text = .PropDtProblemInfo.Rows(0).Item("ApproverID").ToString                                    '基本情報タブ：対処承認者ID
                .PropTxtApproverNM.Text = .PropDtProblemInfo.Rows(0).Item("ApproverNM").ToString                                    '基本情報タブ：対処承認者氏名
                .PropTxtRecorderID.Text = .PropDtProblemInfo.Rows(0).Item("RecorderID").ToString                                    '基本情報タブ：承認記録者ID
                .PropTxtRecorderNM.Text = .PropDtProblemInfo.Rows(0).Item("RecorderNM").ToString                                    '基本情報タブ：承認記録者氏名
                '.PropTxtGrpRireki.Text = .PropDtProblemInfo.Rows(0).Item("GroupRireki").ToString                                    '対応履歴情報：グループ履歴
                '.PropTxtTantoRireki.Text = .PropDtProblemInfo.Rows(0).Item("TantoRireki").ToString                                  '対応履歴情報：担当者履歴

                'メール関連
                .PropStrRegGp = .PropDtProblemInfo.Rows(0).Item("mail_RegGp").ToString
                .PropStrRegUsr = .PropDtProblemInfo.Rows(0).Item("mail_RegUsr").ToString
                .PropStrRegDT = .PropDtProblemInfo.Rows(0).Item("mail_RegDT").ToString
                .PropStrUpdateGp = .PropDtProblemInfo.Rows(0).Item("mail_UpdateGp").ToString
                .PropStrUpdateUsr = .PropDtProblemInfo.Rows(0).Item("mail_UpdateUsr").ToString
                .PropStrUpdateDT = .PropDtProblemInfo.Rows(0).Item("mail_UpdateDT").ToString
            End With

            '担当履歴 
            If CreateTantoRireki(dataHBKD0201) = False Then
                Return False
            End If

            '作業予実スプレッド 
            If CreateYojitsu(dataHBKD0201) = False Then
                Return False
            End If

            '作業予実担当者表示制御
            If VisibleRirekiTanto(dataHBKD0201) = False Then
                Return False
            End If

            '作業履歴担当者ロック制御
            If LockedRirekiTanto(dataHBKD0201) = False Then
                Return False
            End If

            '対応関係者スプレッド
            dataHBKD0201.PropVwRelationInfo.DataSource = dataHBKD0201.PropDtProblmKankei

            'ユーザ名の背景色を濃灰色にする
            For i As Integer = 0 To dataHBKD0201.PropDtProblmKankei.Rows.Count - 1
                If dataHBKD0201.PropVwRelationInfo.Sheets(0).GetText(i, COL_PBMKANKEI_HBKUSRNM) = "" Then
                    dataHBKD0201.PropVwRelationInfo.Sheets(0).Cells(i, COL_PBMKANKEI_HBKUSRNM).BackColor = PropCellBackColorDARKGRAY
                End If
                'グループ名の背景色を濃灰色にする
                If dataHBKD0201.PropVwRelationInfo.Sheets(0).GetText(i, COL_PBMKANKEI_GRPNM) = "" Then
                    dataHBKD0201.PropVwRelationInfo.Sheets(0).Cells(i, COL_PBMKANKEI_GRPNM).BackColor = PropCellBackColorDARKGRAY
                End If
            Next

            'プロセスリンクスプレッド
            dataHBKD0201.PropVwProcessLinkInfo.DataSource = dataHBKD0201.PropDtProcessLink

            'CYSPR情報
            dataHBKD0201.PropVwCysprInfo.DataSource = dataHBKD0201.PropDtProblemCyspr

            '関連ファイル情報スプレッド
            dataHBKD0201.PropVwPrbFileInfo.DataSource = dataHBKD0201.PropDtProblemFile

            'データが無い場合、ボタン制御を行う
            With dataHBKD0201.PropVwPrbFileInfo.Sheets(0)
                If .RowCount > 0 Then
                    dataHBKD0201.PropBtnOpenFile.Enabled = True
                    dataHBKD0201.PropBtnSaveFile.Enabled = True
                Else
                    dataHBKD0201.PropBtnOpenFile.Enabled = False
                    dataHBKD0201.PropBtnSaveFile.Enabled = False
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
    ''' 【共通】作業予実作成処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業予実データを作成する
    ''' <para>作成情報：2012/08/16 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateYojitsu(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0201

                '取得した作業履歴データ
                If .PropDtProblemWkTanto.Rows.Count > 0 Then

                    'クリア処理
                    .PropDtwkRireki.Clear()

                    '作業履歴を土台に設定
                    .PropDtwkRireki.Merge(.PropDtProblemWkRireki)

                    For i As Integer = 0 To .PropDtwkRireki.Rows.Count - 1

                        For j As Integer = 0 To .PropDtProblemWkTanto.Rows.Count - 1
                            '作業履歴番号が一致した場合
                            If .PropDtwkRireki.Rows(i).Item("WorkRirekiNmb").Equals(.PropDtProblemWkTanto.Rows(j).Item("WorkRirekiNmb")) Then
                                '存在する担当者の数だけループ
                                For k As Integer = 0 To .PropDtProblemWkTanto.Rows(j).Item("cnt") - 1
                                    .PropDtwkRireki.Rows(i).Item("WorkTantoGrpNM" & k + 1) = .PropDtProblemWkTanto.Rows(j + k).Item("WorkTantoGrpNM")
                                    .PropDtwkRireki.Rows(i).Item("WorkTantoNM" & k + 1) = .PropDtProblemWkTanto.Rows(j + k).Item("WorkTantoNM")
                                    .PropDtwkRireki.Rows(i).Item("WorkTantoGrpCD" & k + 1) = .PropDtProblemWkTanto.Rows(j + k).Item("WorkTantoGrpCD")
                                    .PropDtwkRireki.Rows(i).Item("WorkTantoID" & k + 1) = .PropDtProblemWkTanto.Rows(j + k).Item("WorkTantoID")
                                Next
                                Exit For
                            End If
                        Next

                    Next
                    'コミット
                    .PropDtwkRireki.AcceptChanges()
                End If
                'データソース設定
                .PropVwPrbYojitsu.DataSource = .PropDtwkRireki
            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のコンボボックスを作成する
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateCmb(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0201

                'プロセスステータスコンボボックス作成
                If commonLogic.SetCmbBox(.PropDtProcessState, .PropCmbStatus, True, "", "") = False Then
                    Return False
                End If

                '発生原因コンボボックス作成
                If commonLogic.SetCmbBox(.PropDtProblemCase, .PropCmbPrbCase, True, "", "") = False Then
                    Return False
                End If

                '担当グループコンボボックス作成
                If commonLogic.SetCmbBox(.PropDtTantoGrp, .PropCmbTantoGrp, True, "", "") = False Then
                    Return False
                End If

                '対象システムコンボボックス作成
                .PropCmbTargetSystem.PropIntStartCol = 2
                If commonLogic.SetCmbBoxEx(.PropDtTargetSystem, .PropCmbTargetSystem, "CINmb", "CINM1", True, 0, "") = False Then
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
    ''' 【コンボボックス共通】コンボボックスリサイズ処理メイン
    ''' </summary>
    ''' <param name="sender">[IN/OUT]コンボボックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>コンボボックスサイズ変換処理
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ComboBoxResizeMain(ByRef sender As Object) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'コンボボックスサイズ変換処理
        If ResizeComboBox(sender) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' コンボボックスサイズ変換処理
    ''' </summary>
    ''' <param name="sender">[IN/OUT]コンボボックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>コンボボックスのサイズを変換する
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ResizeComboBox(ByRef sender As Object) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '変数宣言
            Dim cmbTemp As ComboBox = DirectCast(sender, ComboBox)
            Dim dtTemp As DataTable
            Dim bLineX As Single

            'コンボボックスにデータソースが設定されている場合はデータソースをデータテーブルに変換
            If cmbTemp.DataSource IsNot Nothing Then
                dtTemp = DirectCast(cmbTemp.DataSource, DataTable)
            Else
                'データソース未設定時は処理を抜ける
                Exit Function
            End If

            'コンボボックスのサイズを計算する

            '最大バイト数を取得

            Dim maxLenB = Aggregate row As DataRow In dtTemp.Rows Where IsDBNull(row.Item(1)) = False Select commonLogic.LenB(row.Item(1)) Into Max()

            '次の描画位置計算
            Dim g As Graphics = cmbTemp.CreateGraphics()
            Dim sf As SizeF = g.MeasureString(New String("0"c, maxLenB), cmbTemp.Font)
            bLineX += sf.Width

            '最終項目の場合、ドロップダウンリストのサイズを設定
            If dtTemp.Rows.Count >= 2 Then
                cmbTemp.DropDownWidth = bLineX
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

    ''' <summary>
    ''' 【共通】スプレッドセルタイプ作成処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のコンボボックスを作成する
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateSpreadCtype(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0201

                '描画用オブジェクト生成
                Dim cmbTemp As New ComboBox
                Dim grpComboBox As Graphics = cmbTemp.CreateGraphics()
                Dim intHosei As Integer = 3

                '作業ステータスセル用コンボボックス作成
                Dim aryComboVal1 As New ArrayList
                Dim aryComboTxt1 As New ArrayList
                Dim intTempLength1 As Integer = 0

                For i As Integer = 0 To .PropDtWorkState.Rows.Count - 1
                    aryComboVal1.Add(.PropDtWorkState.Rows(i).Item(0))
                    aryComboTxt1.Add(.PropDtWorkState.Rows(i).Item(1))
                    '設定した最大文字数を取得
                    If intTempLength1 < commonLogic.LenB(.PropDtWorkState.Rows(i).Item(1).ToString) Then
                        intTempLength1 = commonLogic.LenB(.PropDtWorkState.Rows(i).Item(1).ToString)
                    End If
                Next
                '最大幅取得
                Dim sf1 As SizeF = grpComboBox.MeasureString(New String("0"c, intTempLength1 + intHosei), .PropVwPrbYojitsu.Font)
                Dim cmbWkState As New FarPoint.Win.Spread.CellType.ComboBoxCellType()
                With cmbWkState
                    .ItemData = CType(aryComboVal1.ToArray(Type.GetType("System.String")), String())
                    .Items = CType(aryComboTxt1.ToArray(Type.GetType("System.String")), String())
                    .EditorValue = FarPoint.Win.Spread.CellType.EditorValue.ItemData
                    .Editable = True
                    .ListWidth = sf1.Width
                End With

                '★対象システム種別セル用コンボボックス作成 
                Dim intTempLength2_1 As Integer = 0
                Dim intTempLength2_2 As Integer = 0
                Dim intTempLength2_3 As Integer = 0
                For i As Integer = 0 To .PropDtTargetSystem.Rows.Count - 1
                    '設定した最大文字数を取得
                    Dim strWk1 As String = .PropDtTargetSystem.Rows(i).Item(2).ToString
                    If intTempLength2_1 < commonLogic.LenB(strWk1) Then
                        intTempLength2_1 = commonLogic.LenB(strWk1)
                    End If
                    Dim strWk2 As String = .PropDtTargetSystem.Rows(i).Item(3).ToString
                    If intTempLength2_2 < commonLogic.LenB(strWk2) Then
                        intTempLength2_2 = commonLogic.LenB(strWk2)
                    End If
                    Dim strWk3 As String = .PropDtTargetSystem.Rows(i).Item(4).ToString
                    If intTempLength2_3 < commonLogic.LenB(strWk3) Then
                        intTempLength2_3 = commonLogic.LenB(strWk3)
                    End If
                Next
                Dim sf2 As SizeF = grpComboBox.MeasureString(New String("0"c, intTempLength2_1 + intTempLength2_2 + intTempLength2_3 + (intHosei * 3)), .PropVwPrbYojitsu.Font)
                Dim cmbTSystem As New FarPoint.Win.Spread.CellType.MultiColumnComboBoxCellType
                With cmbTSystem
                    .DataSourceList = dataHBKD0201.PropDtTargetSystem
                    .ColumnEdit = 1
                    .DataColumn = 0
                    .ListResizeColumns = FarPoint.Win.Spread.CellType.ListResizeColumns.FitWidestItem
                    .ListBorderStyle = BorderStyle.FixedSingle
                    .ShowColumnHeaders = False
                    .ListWidth = sf2.Width
                    .MaxDrop = MaxDrop_systemnmb
                End With

                'データクラスへセット
                .PropCmbWkState = cmbWkState            '作業予実スプレッド：作業ステータスコンボボックス
                .PropCmbTSystem = cmbTSystem            '作業予実スプレッド：対象システムコンボボックス

                With .PropVwPrbYojitsu.Sheets(0)
                    .Columns(COL_YOJITSU_WORKSTATENM).CellType = dataHBKD0201.PropCmbWkState
                    .Columns(COL_YOJITSU_SYSTEM).CellType = dataHBKD0201.PropCmbTSystem
                End With

                'リソースを解放する
                grpComboBox.Dispose()

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて会議情報タブデータを初期設定する
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabMeeting(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKD0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    If .PropBlnFromCheckFlg = True Then

                        '処理なし

                    ElseIf .PropBlnFromCheckFlg = False Then

                        '処理なし

                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード

                    '編集モード用設定
                    If SetDataToTabMeetingForEdit(dataHBKD0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '作業予実モード

                    '作業予実モード用設定 ※編集とおなじ
                    If SetDataToTabMeetingForEdit(dataHBKD0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then         '参照モード

                    '参照モード用設定　※編集とおなじ
                    If SetDataToTabMeetingForEdit(dataHBKD0201) = False Then
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
    ''' 【編集／参照／作業予実モード】会議情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報データを初期設定する
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabMeetingForEdit(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKD0201

                '会議情報スプレッド
                dataHBKD0201.PropVwMeeting.DataSource = dataHBKD0201.PropDtMeeting
                'If dataHBKD0201.PropDtMeeting.Rows.Count > 0 Then
                '    dataHBKD0201.PropVwMeeting.DataSource = dataHBKD0201.PropDtMeeting
                'End If

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFree(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKD0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    If .PropBlnFromCheckFlg = True Then

                        'インシデント登録からの呼出

                        '新規登録モード用(インシデント登録画面からの呼出)設定
                        If SetDataToTabFreeForNew(dataHBKD0201) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnFromCheckFlg = False Then

                        'インシデント登録外からの呼出

                        '新規登録モード用設定
                        If SetDataToTabFreeForNew(dataHBKD0201) = False Then
                            Return False
                        End If

                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード

                    '編集モード用設定
                    If SetDataToTabFreeForEdit(dataHBKD0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_RIREKI Then      '作業予実モード

                    '作業予実モード用設定　※編集と同じ
                    If SetDataToTabFreeForEdit(dataHBKD0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then         '参照モード

                    '参照モード用設定　　※編集と同じ
                    If SetDataToTabFreeForEdit(dataHBKD0201) = False Then
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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFreeForNew(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKD0201

                'フリーテキスト１～５テキストボックス
                .PropTxtFreeText1.Text = ""
                .PropTxtFreeText2.Text = ""
                .PropTxtFreeText3.Text = ""
                .PropTxtFreeText4.Text = ""
                .PropTxtFreeText5.Text = ""

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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/08/14 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFreeForEdit(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKD0201

                'フリーテキスト１～５テキストボックス
                .PropTxtFreeText1.Text = .PropDtProblemInfo.Rows(0).Item("BIko1")
                .PropTxtFreeText2.Text = .PropDtProblemInfo.Rows(0).Item("BIko2")
                .PropTxtFreeText3.Text = .PropDtProblemInfo.Rows(0).Item("BIko3")
                .PropTxtFreeText4.Text = .PropDtProblemInfo.Rows(0).Item("BIko4")
                .PropTxtFreeText5.Text = .PropDtProblemInfo.Rows(0).Item("BIko5")

                'フリーフラグ１～５チェックボックス
                If .PropDtProblemInfo.Rows(0).Item("FreeFlg1") = FREE_FLG_ON Then
                    .PropChkFreeFlg1.Checked = True
                ElseIf .PropDtProblemInfo.Rows(0).Item("FreeFlg1") = FREE_FLG_OFF Then
                    .PropChkFreeFlg1.Checked = False
                End If
                If .PropDtProblemInfo.Rows(0).Item("FreeFlg2") = FREE_FLG_ON Then
                    .PropChkFreeFlg2.Checked = True
                ElseIf .PropDtProblemInfo.Rows(0).Item("FreeFlg2") = FREE_FLG_OFF Then
                    .PropChkFreeFlg2.Checked = False
                End If
                If .PropDtProblemInfo.Rows(0).Item("FreeFlg3") = FREE_FLG_ON Then
                    .PropChkFreeFlg3.Checked = True
                ElseIf .PropDtProblemInfo.Rows(0).Item("FreeFlg3") = FREE_FLG_OFF Then
                    .PropChkFreeFlg3.Checked = False
                End If
                If .PropDtProblemInfo.Rows(0).Item("FreeFlg4") = FREE_FLG_ON Then
                    .PropChkFreeFlg4.Checked = True
                ElseIf .PropDtProblemInfo.Rows(0).Item("FreeFlg4") = FREE_FLG_OFF Then
                    .PropChkFreeFlg4.Checked = False
                End If
                If .PropDtProblemInfo.Rows(0).Item("FreeFlg5") = FREE_FLG_ON Then
                    .PropChkFreeFlg5.Checked = True
                ElseIf .PropDtProblemInfo.Rows(0).Item("FreeFlg5") = FREE_FLG_OFF Then
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
    ''' 【共通】作業予実担当者表示処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴の担当者の表示をする(編集モードの初期、リフレッシュ時、担当者ボタン処理後に呼ぶ）
    ''' <para>作成情報：2012/08/16 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function VisibleRirekiTanto(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '作業予実スプレッド
            With dataHBKD0201.PropVwPrbYojitsu.Sheets(0)

                If .Rows.Count > 0 Then

                    'スプレッド内の全体のデータを検索し表示用最大を取得
                    Dim intSpdCnt As Integer = 0
                    Dim intSpdMax As Integer = 0
                    For i As Integer = 0 To .Rows.Count - 1
                        '高さ設定
                        .Rows(i).Height = dataHBKD0201.PropIntVwYojitsuRowHeight
                        'Rowヘッダーの番号を非表示
                        .RowHeader.Cells(i, 0).Text = " "

                        'カウンタ初期化
                        intSpdCnt = COL_YOJITSU_TANTOGRP1
                        For j As Integer = COL_YOJITSU_TANTOGRP1 To COL_YOJITSU_PRBTANTO_BTN - 1 Step YOJITSU_TANTO_COLCNT
                            '担当GPを確認
                            If .GetText(i, j).Equals("") Then
                                Exit For
                            End If
                            intSpdCnt = j
                        Next
                        '最大カラム数を取得
                        If intSpdMax < intSpdCnt Then
                            intSpdMax = intSpdCnt
                        End If
                    Next

                    '入力されている担当者の数だけ表示する
                    For i As Integer = COL_YOJITSU_TANTOGRP1 To intSpdMax Step YOJITSU_TANTO_COLCNT
                        .Columns(i).Visible = True              '担当GP名
                        .Columns(i + 1).Visible = True          '担当ID名
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
    ''' 【共通】作業予実担当者ロック処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業予実の担当者の表示をする(編集モードの初期、リフレッシュ時に呼ぶ）
    ''' <para>作成情報：2012/08/16 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function LockedRirekiTanto(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '作業履歴スプレッド
            If dataHBKD0201.PropDtProblemWkRireki.Rows.Count > 0 Then

                With dataHBKD0201.PropVwPrbYojitsu.Sheets(0)

                    'スプレッド内の全体のデータを検索し表示用最大を取得
                    Dim blnChkFlg As Boolean

                    For i As Integer = 0 To .Rows.Count - 1
                        blnChkFlg = False
                        For j As Integer = COL_YOJITSU_TANTOGRP1 To COL_YOJITSU_PRBTANTO_BTN - 1 Step YOJITSU_TANTO_COLCNT
                            '担当GPを確認
                            If .GetText(i, j + 2).Equals(PropWorkGroupCD) Then
                                blnChkFlg = True
                                Exit For
                            End If

                            '担当IDを確認
                            If .GetText(i, j + 3).Equals(PropUserId) Then
                                blnChkFlg = True
                                Exit For
                            End If
                        Next

                        '担当者の枠にログイン者データがない場合ロック制御を行う
                        If blnChkFlg = False Then
                            .Rows(i).Locked = True
                        End If

                    Next

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
        End Try

    End Function


    ''' <summary>
    ''' 【共通】担当マスタデータ取得メイン処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>担当ID入力後Enter押下時にセットするデータを取得する
    ''' <para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetPrbTantoDataMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try

            'コネクションを開く
            Cn.Open()

            'マスタデータ取得
            If GetPrbTantoData(Adapter, Cn, dataHBKD0201) = False Then
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
    ''' 【共通】担当マスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>マスタデータを取得する
    ''' <para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetPrbTantoData(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtMaster As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKD0201.SetSelectPrbTantoData(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ひびきユーザマスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtMaster)

            '取得データをデータクラスにセット
            dataHBKD0201.PropDtResultTanto = dtMaster

            '終了ログ出力
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
            dtMaster.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【共通】対処承認者用マスタデータ取得メイン処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対処承認者ID入力後Enter押下時にセットするデータを取得する
    ''' <para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetPrbApproverDataMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try

            'コネクションを開く
            Cn.Open()

            'マスタデータ取得
            If GetPrbApproverData(Adapter, Cn, dataHBKD0201) = False Then
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
    ''' 【共通】対処承認者用マスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対処承認者用マスタデータを取得する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetPrbApproverData(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtMaster As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKD0201.SetSelectPrbApproverData(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "エンドユーザマスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtMaster)

            '取得データをデータクラスにセット
            dataHBKD0201.PropDtResultApprover = dtMaster

            '終了ログ出力
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
            dtMaster.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【共通】承認記録者用マスタデータ取得メイン処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>承認記録者ID入力後Enter押下時にセットするデータを取得する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetPrbRecorderDataMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try

            'コネクションを開く
            Cn.Open()

            'マスタデータ取得
            If GetPrbRecorderData(Adapter, Cn, dataHBKD0201) = False Then
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
    ''' 【共通】承認記録者用マスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>承認記録者者用マスタデータを取得する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetPrbRecorderData(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtMaster As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKD0201.SetSelectPrbRecorderData(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ひびきユーザマスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtMaster)

            '取得データをデータクラスにセット
            dataHBKD0201.PropDtResultRecorder = dtMaster

            '終了ログ出力
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
            dtMaster.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 作業予実リフレッシュ時メイン処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業予実一覧の新規取得を行う
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RefrashPrbWkYojitsuMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            '新規登録モード用設定
            If GetRirekiDataForRefrash(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            '作業履歴スプレッド 
            If CreateYojitsu(dataHBKD0201) = False Then
                Return False
            End If

            '作業履歴担当者表示制御
            If VisibleRirekiTanto(dataHBKD0201) = False Then
                Return False
            End If

            '作業履歴担当者ロック制御
            If LockedRirekiTanto(dataHBKD0201) = False Then
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

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 作業予実行追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業予実一覧に空行を1行追加する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function AddRowPrbWkYojitsuMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '空行追加処理
        If AddRowPrbWkYojitsu(dataHBKD0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】作業予実情報空行追加処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業予実に空行を1行追加する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function AddRowPrbWkYojitsu(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0201.PropVwPrbYojitsu.Sheets(0)

                '一番上に空行を1行追加
                .Rows.Add(0, 1)
                .Rows(0).Height = dataHBKD0201.PropIntVwYojitsuRowHeight

                '追加行にフォーカスをセット
                If commonLogicHBK.SetFocusOnVwRow(dataHBKD0201.PropVwPrbYojitsu, 0, 0, 0, 1, .ColumnCount) = False Then
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
    ''' 作業予実行削除時メイン処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業予実一覧の選択行を削除する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowPrbWkYojitsuMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowPrbWkYojitsu(dataHBKD0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】作業予実情報選択行削除処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業予実の選択行を削除（Remove）する
    ''' <para>作成情報：2012/08/20 s..yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowPrbWkYojitsu(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号
        Dim blnFlg As Boolean               'エラーフラグ

        Try
            With dataHBKD0201.PropVwPrbYojitsu.Sheets(0)

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
                        '新規追加行のみ削除をする
                        If .GetText(i, COL_YOJITSU_PRBSEQ) = "" Then
                            .Rows(i).Remove()
                        Else
                            blnFlg = True
                        End If
                    Next

                End If

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '******************************************************************
            'インシデント登録と記述が違うので確認を行う
            If blnFlg = True Then
                'エラーメッセージ設定
                puErrMsg = D0201_E018
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            End If
            '******************************************************************

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 作業予実スプレッド内担当者追加_前検索データ作成
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業予実一覧の担当者列をデータテーブルに変換する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateDtPrbYojitsuTantoMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '受け渡し用データ作成
        If CreateDtPrbYojitsuTanto(dataHBKD0201) = False Then
            Return False
        End If

        '作業履歴担当者表示制御
        If VisibleRirekiTanto(DataHBKD0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】作業予実スプレッド内担当者追加_前検索データ作成
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業予実一覧の担当者列をデータテーブルに変換する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateDtPrbYojitsuTanto(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0201.PropVwPrbYojitsu.Sheets(0)

                '検索一覧受け渡し用データ作成
                Dim dtTemp As New DataTable
                dtTemp.Columns.Add("選択", Type.GetType("System.Boolean"))
                dtTemp.Columns.Add("ユーザーID", Type.GetType("System.String"))
                dtTemp.Columns.Add("グループ名", Type.GetType("System.String"))
                dtTemp.Columns.Add("ユーザー氏名", Type.GetType("System.String"))
                dtTemp.Columns.Add("グループID", Type.GetType("System.String"))
                dtTemp.Columns.Add("順番", Type.GetType("System.Decimal"))
                '【EDIT】2012/10/09 r.hoshino　課題No33障害対応：START
                dtTemp.Columns.Add("削除", Type.GetType("System.String"))
                '【EDIT】2012/10/09 r.hoshino　課題No33障害対応：END

                '入力値取得
                Dim intLoopCnt As Integer = 0
                For j As Integer = COL_YOJITSU_TANTOGRP1 To COL_YOJITSU_PRBTANTO_BTN - 1 Step YOJITSU_TANTO_COLCNT
                    '登録行作成
                    Dim row As DataRow = dtTemp.NewRow
                    row.Item("選択") = True
                    row.Item("グループ名") = .GetText(dataHBKD0201.PropIntRowSelect, j + 0)
                    row.Item("ユーザー氏名") = .GetText(dataHBKD0201.PropIntRowSelect, j + 1)
                    row.Item("グループID") = .GetText(dataHBKD0201.PropIntRowSelect, j + 2)
                    row.Item("ユーザーID") = .GetText(dataHBKD0201.PropIntRowSelect, j + 3)
                    row.Item("順番") = intLoopCnt
                    '入力のあるデータのみを登録
                    If Not row.Item("グループ名").Equals("") Then
                        '作成した行をデータクラスにセット
                        dtTemp.Rows.Add(row)
                    End If
                Next

                dataHBKD0201.PropDtResultWkTanto = dtTemp

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 作業予実スプレッド内担当者追加処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業予実一覧の担当者列を追加する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function AddPrbYojitsuTantoMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '担当者追加処理
        If AddPrbYojitsuTanto(dataHBKD0201) = False Then
            Return False
        End If

        '作業履歴担当者表示制御
        If VisibleRirekiTanto(dataHBKD0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】作業予実スプレッド内担当者追加処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業予実一覧の担当者列を追加し、表示制御をする。
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function AddPrbYojitsuTanto(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0201.PropVwPrbYojitsu.Sheets(0)

                '選択された分のみ設定
                If dataHBKD0201.PropDtResultWkTanto IsNot Nothing Then

                    '表示初期化（担当GP1の以降をクリア）
                    For j As Integer = COL_YOJITSU_TANTOGRP1 To COL_YOJITSU_PRBTANTO_BTN - 1
                        .Columns(j).Visible = False
                        .SetValue(dataHBKD0201.PropIntRowSelect, j, "")
                    Next

                    'ソートして作業テーブルに格納
                    Dim Rows As Object = dataHBKD0201.PropDtResultWkTanto.Select(String.Empty, "順番 Asc")
                    Dim DtSortResult As DataTable = dataHBKD0201.PropDtResultWkTanto.Clone()
                    For Each row As DataRow In Rows
                        DtSortResult.ImportRow(row)
                    Next

                    'For i As Integer = 0 To dataHBKD0201.PropDtResultWkTanto.Rows.Count - 1
                    '    'グループ名,ユーザ名,グループCD,ユーザIDを設定
                    '    .SetText(dataHBKD0201.PropIntRowSelect, COL_YOJITSU_TANTOGRP1 + (i * YOJITSU_TANTO_COLCNT), dataHBKD0201.PropDtResultWkTanto.Rows(i).Item(1))
                    '    .SetText(dataHBKD0201.PropIntRowSelect, COL_YOJITSU_PRBTANTONM1 + (i * YOJITSU_TANTO_COLCNT), dataHBKD0201.PropDtResultWkTanto.Rows(i).Item(2))
                    '    .SetText(dataHBKD0201.PropIntRowSelect, COL_YOJITSU_TANTOGRPCD1 + (i * YOJITSU_TANTO_COLCNT), dataHBKD0201.PropDtResultWkTanto.Rows(i).Item(3))
                    '    .SetText(dataHBKD0201.PropIntRowSelect, COL_YOJITSU_PRBTANTOID1 + (i * YOJITSU_TANTO_COLCNT), dataHBKD0201.PropDtResultWkTanto.Rows(i).Item(0))
                    'Next
                    For i As Integer = 0 To DtSortResult.Rows.Count - 1
                        'グループ名,ユーザ名,グループCD,ユーザIDを設定
                        .SetText(dataHBKD0201.PropIntRowSelect, COL_YOJITSU_TANTOGRP1 + (i * YOJITSU_TANTO_COLCNT), DtSortResult.Rows(i).Item(1))
                        .SetText(dataHBKD0201.PropIntRowSelect, COL_YOJITSU_PRBTANTONM1 + (i * YOJITSU_TANTO_COLCNT), DtSortResult.Rows(i).Item(2))
                        .SetText(dataHBKD0201.PropIntRowSelect, COL_YOJITSU_TANTOGRPCD1 + (i * YOJITSU_TANTO_COLCNT), DtSortResult.Rows(i).Item(3))
                        .SetText(dataHBKD0201.PropIntRowSelect, COL_YOJITSU_PRBTANTOID1 + (i * YOJITSU_TANTO_COLCNT), DtSortResult.Rows(i).Item(0))
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
    ''' 会議情報行追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報一覧に空行を1行追加する
    ''' <para>作成情報：2012/08/20 s..yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function AddRowMeetingMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '空行追加処理
        If AddRowMeeting(DataHBKD0201) = False Then
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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報に空行を1行追加する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function AddRowMeeting(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '変数宣言
        Dim intNewRowNo As Integer          '新規追加行番号
        Dim blnAddFlg As Boolean = True     '追加フラグ

        Try
            With dataHBKD0201


                'サブ検索画面で1件以上選択された場合に値を設定
                If .PropDtResultTemp IsNot Nothing Then

                    '選択データ数分繰り返し、会議情報一覧に行を追加する
                    For i As Integer = 0 To .PropDtResultTemp.Rows.Count - 1

                        '追加フラグ初期化
                        blnAddFlg = True

                        '番号が既に設定済でない場合のみ追加
                        For j As Integer = 0 To .PropVwMeeting.Sheets(0).RowCount - 1

                            '既に設定済みの場合は追加フラグをOFFにして処理を抜ける
                            If .PropDtResultTemp.Rows(i).Item("MeetingNmb").ToString.Equals(.PropVwMeeting.Sheets(0).GetText(j, COL_MEETING_NMB)) Then
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
                                .PropDtResultTemp.Rows(i).Item("MeetingNmb")                                 '番号
                            .PropVwMeeting.Sheets(0).Cells(intNewRowNo, COL_MEETING_JISISTDT).Value = _
                                .PropDtResultTemp.Rows(i).Item("jisiDT")                                     '実施日
                            .PropVwMeeting.Sheets(0).Cells(intNewRowNo, COL_MEETING_TITLE).Value = _
                                .PropDtResultTemp.Rows(i).Item("Title")                                      'タイトル

                            'Dim dr() As DataRow = .PropDtResultMeeting.Select("MeetingNmb='" & .PropDtResultTemp.Rows(i).Item("MeetingNmb") & "'")
                            'If dr.Count > 0 Then
                            '    '設定済みがアリ
                            '    .PropVwMeeting.Sheets(0).Cells(intNewRowNo, COL_MEETING_RESULTKBN).Value = _
                            '        dr(0).Item("ResultKbnNM") '.PropDtResultSub.Rows(i).Item("ResultKbnNM")                           　'承認　
                            '    .PropVwMeeting.Sheets(0).Cells(intNewRowNo, COL_MEETING_RESULTKBNCD).Value = _
                            '        dr(0).Item("ResultKbn") '.PropDtResultSub.Rows(i).Item("ResultKbn")                                 '承認コード
                            'Else
                            '    '新規紐付け
                            '    .PropVwMeeting.Sheets(0).Cells(intNewRowNo, COL_MEETING_RESULTKBN).Value = ""                           '承認　
                            '    .PropVwMeeting.Sheets(0).Cells(intNewRowNo, COL_MEETING_RESULTKBNCD).Value = "0"                        '承認コード
                            'End If


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
    ''' 【編集／参照モード】会議情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報データを取得する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMeeting(ByVal Adapter As NpgsqlDataAdapter, _
                                      ByVal Cn As NpgsqlConnection, _
                                      ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtTableData As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKD0201.SetSelectMeetingSql(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議結果情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtTableData)

            '取得データをデータクラスにセット
            dataHBKD0201.PropDtMeeting = dtTableData

            '終了ログ出力
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
            dtTableData.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 会議情報行削除時メイン処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報一覧の選択行を削除する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowMeetingMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowMeeting(dataHBKD0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】会議情報選択行削除処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報の選択行を削除（Remove）する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowMeeting(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号

        Try
            With dataHBKD0201.PropVwMeeting.Sheets(0)

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
    ''' 問題対応関係情報グループ追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対応関係者情報一覧にサブ検索画面から取得したグループデータを設定する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetGroupToVwRelationMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'グループデータ設定処理
        If SetGroupToVwRelation(dataHBKD0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】問題対応関係情報グループ設定処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者情報一覧にサブ検索で選択されたグループを設定する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetGroupToVwRelation(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intNewRowNo As Integer          '新規追加行番号
        Dim blnAddFlg As Boolean = True     '追加フラグ

        Try
            With dataHBKD0201

                'サブ検索画面で1件以上選択された場合に値を設定
                If .PropDtResultPrbKankei IsNot Nothing Then

                    '選択データ数分繰り返し、関係者情報一覧に行を追加する
                    For i As Integer = 0 To .PropDtResultPrbKankei.Rows.Count - 1

                        '追加フラグ初期化
                        blnAddFlg = True

                        'グループが既に設定済でない場合のみ追加
                        For j As Integer = 0 To .PropVwRelationInfo.Sheets(0).RowCount - 1

                            '既に設定済みの場合は追加フラグをOFFにして処理を抜ける
                            If .PropDtResultPrbKankei.Rows(i).Item("グループCD") = _
                                .PropVwRelationInfo.Sheets(0).Cells(j, COL_PBMKANKEI_RELATIONID).Value Then
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
                            .PropVwRelationInfo.Sheets(0).Cells(intNewRowNo, COL_PBMKANKEI_RELATIONKBN).Value = KBN_GROUP      '区分：グループ
                            .PropVwRelationInfo.Sheets(0).Cells(intNewRowNo, COL_PBMKANKEI_RELATIONID).Value = _
                                .PropDtResultPrbKankei.Rows(i).Item("グループCD")                                       'ID
                            .PropVwRelationInfo.Sheets(0).Cells(intNewRowNo, COL_PBMKANKEI_GRPNM).Value = _
                                .PropDtResultPrbKankei.Rows(i).Item("グループ名")                                       'グループ名

                            'ユーザ名の背景色を濃灰色にする
                            .PropVwRelationInfo.Sheets(0).Cells(intNewRowNo, COL_PBMKANKEI_HBKUSRNM).BackColor = PropCellBackColorDARKGRAY

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
    ''' 問題対応関係者情報ユーザー追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>問題対応関係者情報一覧にサブ検索画面から取得したユーザーデータを設定する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetUserToVwRelationMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ユーザーデータ設定処理
        If SetUserToVwRelation(dataHBKD0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】関係者情報ユーザー設定処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>問題対応関係者情報一覧にサブ検索で選択されたユーザーを設定する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetUserToVwRelation(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intNewRowNo As Integer          '新規追加行番号
        Dim blnAddFlg As Boolean = True     '追加フラグ

        Try
            With dataHBKD0201

                'サブ検索画面で1件以上選択された場合に値を設定
                If .PropDtResultPrbKankei IsNot Nothing Then

                    '選択データ数分繰り返し、関係者情報一覧に行を追加する
                    For i As Integer = 0 To .PropDtResultPrbKankei.Rows.Count - 1

                        '追加フラグ初期化
                        blnAddFlg = True

                        'ユーザーが既に設定済でない場合のみ追加
                        For j As Integer = 0 To .PropVwRelationInfo.Sheets(0).RowCount - 1

                            '既に設定済みの場合は追加フラグをOFFにして処理を抜ける
                            If .PropDtResultPrbKankei.Rows(i).Item("ユーザーID") = _
                                .PropVwRelationInfo.Sheets(0).Cells(j, COL_PBMKANKEI_RELATIONID).Value Then
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
                            .PropVwRelationInfo.Sheets(0).Cells(intNewRowNo, COL_PBMKANKEI_RELATIONKBN).Value = KBN_USER       '区分：ユーザー
                            .PropVwRelationInfo.Sheets(0).Cells(intNewRowNo, COL_PBMKANKEI_RELATIONID).Value = _
                                .PropDtResultPrbKankei.Rows(i).Item("ユーザーID")                                       'ID
                            '.PropVwRelation.Sheets(0).Cells(intNewRowNo, COL_RELATION_GROUPNM).Value = _
                            '    .PropDtResultSub.Rows(i).Item("グループ名")                                       'グループ名
                            .PropVwRelationInfo.Sheets(0).Cells(intNewRowNo, COL_PBMKANKEI_HBKUSRNM).Value = _
                                .PropDtResultPrbKankei.Rows(i).Item("ユーザー氏名")                                     'ユーザー名

                            'グループ名の背景色を濃灰色にする
                            .PropVwRelationInfo.Sheets(0).Cells(intNewRowNo, COL_PBMKANKEI_GRPNM).BackColor = PropCellBackColorDARKGRAY

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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対応関係者情報一覧の選択行を削除する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowRelationMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowRelation(DataHBKD0201) = False Then
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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者情報の選択行を削除する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowRelation(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号
        Dim blnAddFlg As Boolean = True
        Try
            With dataHBKD0201.PropVwRelationInfo.Sheets(0)

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
                        If .GetText(i, COL_PBMKANKEI_RELATIONKBN) = KBN_GROUP Then
                            If .GetText(i, COL_PBMKANKEI_RELATIONID).Equals(PropWorkGroupCD) Then
                                'ログインユーザのIDがあるかチェック
                                For j As Integer = 0 To .Rows.Count - 1
                                    If .GetText(j, COL_PBMKANKEI_RELATIONKBN).Equals(KBN_USER) AndAlso _
                                        .GetText(j, COL_PBMKANKEI_RELATIONID).Equals(PropUserId) Then
                                        blnAddFlg = False
                                    End If
                                Next
                                'ない場合
                                If blnAddFlg = True Then
                                    'エラーメッセージ設定
                                    puErrMsg = D0201_E019
                                    Return False
                                End If
                            End If
                        End If

                        '★削除対象がログイン時のユーザーだった場合
                        If .GetText(i, COL_PBMKANKEI_RELATIONKBN) = KBN_USER Then
                            If .GetText(i, COL_PBMKANKEI_RELATIONID).Equals(PropUserId) Then
                                'ログインユーザのグループがあるかチェック
                                For j As Integer = 0 To .Rows.Count - 1
                                    If .GetText(j, COL_PBMKANKEI_RELATIONKBN).Equals(KBN_GROUP) AndAlso _
                                        .GetText(j, COL_PBMKANKEI_RELATIONID).Equals(PropWorkGroupCD) Then
                                        blnAddFlg = False
                                    End If
                                Next
                                'ない場合
                                If blnAddFlg = True Then
                                    '***************************************
                                    'メッセージを変更する
                                    'エラーメッセージ設定
                                    puErrMsg = D0201_E020
                                    '***************************************
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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンク一覧に空行を1行追加する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function AddRowpLinkMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '空行追加処理
        If AddRowplink(dataHBKD0201) = False Then
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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンクに空行を1行追加する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function AddRowplink(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intNewRowNo As Integer          '新規追加行番号
        Dim blnAddFlg As Boolean = True     '追加フラグ

        Try
            With dataHBKD0201

                'サブ検索画面で1件以上選択された場合に値を設定
                If .PropDtResultPLink IsNot Nothing Then

                    '選択データ数分繰り返し、関係者情報一覧に行を追加する
                    For i As Integer = 0 To .PropDtResultPLink.Rows.Count - 1

                        '追加フラグ初期化
                        blnAddFlg = True

                        '番号が既に設定済でない場合のみ追加
                        For j As Integer = 0 To .PropVwProcessLinkInfo.Sheets(0).RowCount - 1

                            '既に設定済みの場合は追加フラグをOFFにして処理を抜ける
                            If .PropDtResultPLink.Rows(i).Item("mngnmb") = _
                                .PropVwProcessLinkInfo.Sheets(0).Cells(j, COL_PLINK_PLINKNO).Value AndAlso _
                                .PropDtResultPLink.Rows(i).Item("ProcessKbn") = _
                                .PropVwProcessLinkInfo.Sheets(0).Cells(j, COL_PLINK_PLINKKBNCD).Value Then
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
                            Dim strSetKbn As String = ""
                            Select Case .PropDtResultPLink.Rows(i).Item("processnm")
                                Case PROCESS_TYPE_INCIDENT_NAME
                                    strSetKbn = PROCESS_TYPE_INCIDENT_NAME_R
                                Case PROCESS_TYPE_QUESTION_NAME
                                    strSetKbn = PROCESS_TYPE_QUESTION_NAME_R
                                Case PROCESS_TYPE_CHANGE_NAME
                                    strSetKbn = PROCESS_TYPE_CHANGE_NAME_R
                                Case PROCESS_TYPE_RELEASE_NAME
                                    strSetKbn = PROCESS_TYPE_RELEASE_NAME_R
                            End Select

                            .PropVwProcessLinkInfo.Sheets(0).Cells(intNewRowNo, COL_PLINK_PLINKKBN).Value = strSetKbn                               '区分(略名）
                            .PropVwProcessLinkInfo.Sheets(0).Cells(intNewRowNo, COL_PLINK_PLINKNO).Value = _
                                .PropDtResultPLink.Rows(i).Item("mngnmb")                                                 '番号
                            .PropVwProcessLinkInfo.Sheets(0).Cells(intNewRowNo, COL_PLINK_PLINKKBNCD).Value = _
                                .PropDtResultPLink.Rows(i).Item("processkbn")                                             '区分CD


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
    ''' <param name="dataHBKD0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンク一覧の選択行を削除する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowpLinkMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowplink(dataHBKD0201) = False Then
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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンクの選択行を削除（Remove）する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowplink(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号

        Try
            With dataHBKD0201.PropVwProcessLinkInfo.Sheets(0)

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
    ''' CYSPR行追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CYSPR一覧に空行を1行追加する
    ''' <para>作成情報：2012/08/21 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function AddRowCysprInfoMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '空行追加処理
        If AddRowCysprInfo(dataHBKD0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】CYSPR空行追加処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CYSPRに空行を1行追加する
    ''' <para>作成情報：2012/08/21 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function AddRowCysprInfo(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKD0201.PropVwCysprInfo.Sheets(0)

                '一番下に空行を1行追加
                .Rows.Add(.Rows.Count, 1)
                '追加行にフォーカスをセット
                If commonLogicHBK.SetFocusOnVwRow(dataHBKD0201.PropVwCysprInfo, 0, .RowCount, 0, 1, .ColumnCount) = False Then
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
    ''' CYSPR行削除時メイン処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CYSPR一覧の選択行を削除する
    ''' <para>作成情報：2012/08/21 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowCysprInfoMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowCysprInfo(dataHBKD0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】CYSPR選択行削除処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CYSPR情報の選択行を削除（Remove）する
    ''' <para>作成情報：2012/08/21 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowCysprInfo(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号

        Try
            With dataHBKD0201.PropVwCysprInfo.Sheets(0)

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
    ''' 関連ファイル行追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関連ファイル一覧に空行を1行追加する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function AddRowFileInfoMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '空行追加処理
        If AddRowFileInfo(dataHBKD0201) = False Then
            Return False
        End If

        'データが無い場合、ボタン制御を行う
        With dataHBKD0201.PropVwPrbFileInfo.Sheets(0)
            If .RowCount > 0 Then
                dataHBKD0201.PropBtnOpenFile.Enabled = True
                dataHBKD0201.PropBtnSaveFile.Enabled = True
            Else
                dataHBKD0201.PropBtnOpenFile.Enabled = False
                dataHBKD0201.PropBtnSaveFile.Enabled = False
            End If
        End With

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】関連ファイル空行追加処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関連ファイルに空行を1行追加する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function AddRowFileInfo(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intNewRowNo As Integer          '新規追加行番号
        Dim blnAddFlg As Boolean = True     '追加フラグ

        Try
            With dataHBKD0201

                '追加フラグ初期化
                blnAddFlg = True

                'pathと説明が既に設定済でない場合のみ追加
                For j As Integer = 0 To .PropVwPrbFileInfo.Sheets(0).RowCount - 1

                    '既に設定済みの場合は追加フラグをOFFにして処理を抜ける
                    If .PropStrFilePath = .PropVwPrbFileInfo.Sheets(0).GetText(j, COL_PRBFILE_FILEPATH) AndAlso _
                       .PropStrFileNaiyo = .PropVwPrbFileInfo.Sheets(0).GetText(j, COL_PRBFILE_NAIYO) Then
                        blnAddFlg = False
                        Exit For
                    End If

                Next

                '追加フラグがONの場合のみ追加処理を行う
                If blnAddFlg = True Then

                    '追加行番号取得
                    intNewRowNo = .PropVwPrbFileInfo.Sheets(0).Rows.Count

                    '新規行追加
                    .PropVwPrbFileInfo.Sheets(0).Rows.Add(intNewRowNo, 1)

                    'サブ検索画面での選択値を設定
                    .PropVwPrbFileInfo.Sheets(0).Cells(intNewRowNo, COL_PRBFILE_NAIYO).Value = .PropStrFileNaiyo       '説明
                    .PropVwPrbFileInfo.Sheets(0).Cells(intNewRowNo, COL_PRBFILE_FILEPATH).Value = .PropStrFilePath         'パス

                End If

                '最終追加行にフォーカスをセット
                If commonLogicHBK.SetFocusOnVwRow(.PropVwPrbFileInfo, _
                                                  0, .PropVwPrbFileInfo.Sheets(0).RowCount, 0, _
                                                  1, .PropVwPrbFileInfo.Sheets(0).ColumnCount) = False Then
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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関連ファイル一覧の選択行を削除する
    ''' <para>作成情報：2012/08/21 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowFileInfoMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowFileInfo(dataHBKD0201) = False Then
            Return False
        End If

        'データが無い場合、ボタン制御を行う
        With dataHBKD0201.PropVwPrbFileInfo.Sheets(0)
            If .RowCount > 0 Then
                dataHBKD0201.PropBtnOpenFile.Enabled = True
                dataHBKD0201.PropBtnSaveFile.Enabled = True
            Else
                dataHBKD0201.PropBtnOpenFile.Enabled = False
                dataHBKD0201.PropBtnSaveFile.Enabled = False
            End If
        End With

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】関連ファイル選択行削除処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関連ファイルの選択行を削除（Remove）する
    ''' <para>作成情報：2012/08/21 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowFileInfo(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号

        Try
            With dataHBKD0201.PropVwPrbFileInfo.Sheets(0)

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
    ''' 【共通】コントロール入力チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録項目の入力チェックを行う
    ''' <para>作成情報：2012/08/21 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckInputValueMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'コントロール入力チェック
        If CheckInputValue(DataHBKD0201) = False Then
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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録項目の入力チェックを行う
    ''' <para>作成情報：2012/08/21 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckInputValue(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnStateKanryo As Boolean '完了フラグ

        Try
            With dataHBKD0201

                '回答日時の確認
                If .PropDtpStartDT.txtDate.Text = "" AndAlso .PropTxtStartDT_HM.PropTxtTime.Text <> "" Then
                    'エラーメッセージ設定
                    puErrMsg = String.Format(D0201_E028, "開始日時")
                    'タブを基本情報タブに設定
                    .PropTbInput.SelectedIndex = TAB_KHN
                    'フォーカス設定
                    .PropDtpStartDT.Focus()
                    'エラーを返す
                    Return False
                End If

                If .PropDtpStartDT.txtDate.Text <> "" AndAlso .PropTxtStartDT_HM.PropTxtTime.Text = "" Then
                    'エラーメッセージ設定
                    puErrMsg = String.Format(D0201_E029, "開始日時")
                    'タブを基本情報タブに設定
                    .PropTbInput.SelectedIndex = TAB_KHN
                    'フォーカス設定
                    .PropTxtStartDT_HM.Focus()
                    'エラーを返す
                    Return False
                End If

                '完了日時の確認
                If .PropDtpKanryoDT.txtDate.Text = "" AndAlso .PropTxtKanryoDT_HM.PropTxtTime.Text <> "" Then
                    'エラーメッセージ設定
                    puErrMsg = String.Format(D0201_E028, "完了日時")
                    'タブを基本情報タブに設定
                    .PropTbInput.SelectedIndex = TAB_KHN
                    'フォーカス設定
                    .PropDtpKanryoDT.Focus()
                    'エラーを返す
                    Return False
                End If

                If .PropDtpKanryoDT.txtDate.Text <> "" AndAlso .PropTxtKanryoDT_HM.PropTxtTime.Text = "" Then
                    'エラーメッセージ設定
                    puErrMsg = String.Format(D0201_E029, "完了日時")
                    'タブを基本情報タブに設定
                    .PropTbInput.SelectedIndex = TAB_KHN
                    'フォーカス設定
                    .PropTxtKanryoDT_HM.Focus()
                    'エラーを返す
                    Return False
                End If


                'ステータスの確認
                With .PropCmbStatus
                    '完了の場合
                    If .SelectedValue = PROCESS_STATUS_QUESTION_KANRYOH Then
                        '完了フラグ
                        blnStateKanryo = True
                    End If
                End With

                '入力チェック-ステータス（必須）
                With .PropCmbStatus
                    '未入力の場合、エラー
                    If .SelectedValue = "" Then
                        'エラーメッセージ設定
                        puErrMsg = D0201_E002
                        'タブを基本情報タブに設定
                        dataHBKD0201.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With

                '入力チェック-開始日時(必須)
                With .PropDtpStartDT
                    '未入力の場合エラー
                    If blnStateKanryo AndAlso .txtDate.Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = D0201_E003
                        'タブを基本情報タブに設定
                        dataHBKD0201.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        'エラーを返す
                        Return False
                    End If
                End With

                ''入力チェック-完了日時(必須)
                'With .PropDtpKanryoDT
                '    '未入力の場合エラー
                '    If blnStateKanryo AndAlso .txtDate.Text.Trim() = "" Then
                '        'エラーメッセージ設定
                '        puErrMsg = D0201_E003
                '        'タブを基本情報タブに設定
                '        dataHBKD0201.PropTbInput.SelectedIndex = TAB_KHN
                '        'フォーカス設定
                '        .Focus()
                '        'エラーを返す
                '        Return False
                '    End If
                'End With

                '入力チェック-タイトル(必須)
                With .PropTxtTitle
                    '未入力の場合エラー
                    If blnStateKanryo AndAlso .Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = D0201_E006
                        'タブを基本情報タブに設定
                        dataHBKD0201.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        'エラーを返す
                        Return False
                    End If
                End With

                '入力チェック-内容(必須)
                With .PropTxtNaiyo
                    '未入力の場合エラー
                    If blnStateKanryo AndAlso .Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = D0201_E007
                        'タブを基本情報タブに設定
                        dataHBKD0201.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        'エラーを返す
                        Return False
                    End If
                End With

                '入力チェック-対象システム(必須)
                With .PropCmbTargetSystem
                    '未入力の場合、エラー
                    If blnStateKanryo AndAlso .PropTxtDisplay.Text = "" Then
                        'エラーメッセージ設定
                        puErrMsg = D0201_E005
                        'タブを基本情報タブに設定
                        dataHBKD0201.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        'エラーを返す
                        Return False
                    End If
                End With

                '入力チェック-発生原因(必須)
                With .PropCmbPrbCase
                    '未入力の場合、エラー
                    If blnStateKanryo AndAlso .SelectedValue = "" Then
                        'エラーメッセージ設定
                        puErrMsg = D0201_E004
                        'タブを基本情報タブに設定
                        dataHBKD0201.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With

                '入力チェック-担当グループ(必須)
                With .PropCmbTantoGrp
                    '未入力の場合、エラー
                    If blnStateKanryo AndAlso .SelectedValue = "" Then
                        'エラーメッセージ設定
                        puErrMsg = D0201_E008
                        'タブを基本情報タブに設定
                        dataHBKD0201.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With

                '入力チェック-担当ID(必須)
                With .PropTxtPrbTantoID
                    '未入力の場合、エラー
                    If blnStateKanryo AndAlso .Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = D0201_E009
                        'タブを基本情報タブに設定
                        dataHBKD0201.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        'エラーを返す
                        Return False
                    End If
                End With

                '入力チェック-担当氏名(必須)
                With .PropTxtPrbTantoNM
                    '未入力の場合、エラー
                    If blnStateKanryo AndAlso .Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = D0201_E010
                        'タブを基本情報タブに設定
                        dataHBKD0201.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        'エラーを返す
                        Return False
                    End If
                End With

                '作業予実の入力チェック
                With .PropVwPrbYojitsu.Sheets(0)

                    '1行以上ある場合、チェックを行う
                    If .RowCount > 0 Then

                        '一覧の行数分繰り返し
                        For i As Integer = 0 To .RowCount - 1

                            '変数宣言
                            Dim strWkState As String = ""       '作業ステータス
                            Dim strSystem As String = ""        '対象システム
                            Dim strKaishi As String = ""        '作業開始日時
                            Dim strNaiyo As String = ""         '作業内容
                            Dim strTantoG As String = ""        '作業担当G
                            Dim strTantoU As String = ""        '作業担当U
                            Dim strSyuryo As String = ""        '作業終了日時

                            '各値を取得
                            If .GetText(i, COL_YOJITSU_WORKSTATENM) = "" Then
                                strWkState = ""
                            Else
                                strWkState = .GetValue(i, COL_YOJITSU_WORKSTATENM)
                            End If
                            If .GetText(i, COL_YOJITSU_SYSTEM) = "" Then
                                strSystem = ""
                            Else
                                strSystem = .GetValue(i, COL_YOJITSU_SYSTEM)
                            End If
                            strKaishi = .GetText(i, COL_YOJITSU_WORKSTDT)
                            strNaiyo = .GetText(i, COL_YOJITSU_WORKNAIYO)
                            strTantoG = .GetText(i, COL_YOJITSU_TANTOGRP1)
                            strTantoU = .GetText(i, COL_YOJITSU_PRBTANTONM1)
                            strSyuryo = .GetText(i, COL_YOJITSU_WORKEDDT)

                            '入力チェック-作業予実：作業ステータス(必須)
                            If blnStateKanryo AndAlso strWkState = "" Then
                                'エラーメッセージ設定
                                puErrMsg = D0201_E011
                                'タブを基本情報タブに設定
                                dataHBKD0201.PropTbInput.SelectedIndex = TAB_KHN
                                'フォーカス設定
                                If commonLogicHBK.SetFocusOnVwRow(dataHBKD0201.PropVwPrbYojitsu, _
                                                                  0, i, COL_YOJITSU_WORKSTATENM, 1, .ColumnCount) = False Then
                                    Return False
                                End If
                                'エラーを返す
                                Return False
                            End If

                            '入力チェック-作業予実：作業内容(必須)
                            If blnStateKanryo AndAlso strNaiyo = "" Then
                                'エラーメッセージ設定
                                puErrMsg = D0201_E012
                                'タブを基本情報タブに設定
                                dataHBKD0201.PropTbInput.SelectedIndex = TAB_KHN
                                'フォーカス設定
                                If commonLogicHBK.SetFocusOnVwRow(dataHBKD0201.PropVwPrbYojitsu, _
                                                                  0, i, COL_YOJITSU_WORKNAIYO, 1, .ColumnCount) = False Then
                                    Return False
                                End If
                                'エラーを返す
                                Return False
                            End If

                            '入力チェック-作業予実：作業開始日時(必須)
                            If blnStateKanryo AndAlso strKaishi = "" Then
                                'エラーメッセージ設定
                                puErrMsg = D0201_E013
                                'タブを基本情報タブに設定
                                dataHBKD0201.PropTbInput.SelectedIndex = TAB_KHN
                                'フォーカス設定
                                If commonLogicHBK.SetFocusOnVwRow(dataHBKD0201.PropVwPrbYojitsu, _
                                                                  0, i, COL_YOJITSU_WORKSTDT, 1, .ColumnCount) = False Then
                                    Return False
                                End If
                                'エラーを返す
                                Return False
                            End If

                            '入力チェック-作業予実：対象システム(必須)
                            If blnStateKanryo AndAlso strSystem = "" Then
                                'エラーメッセージ設定
                                puErrMsg = D0201_E014
                                'タブを基本情報タブに設定
                                dataHBKD0201.PropTbInput.SelectedIndex = TAB_KHN
                                'フォーカス設定
                                If commonLogicHBK.SetFocusOnVwRow(dataHBKD0201.PropVwPrbYojitsu, _
                                                                  0, i, COL_YOJITSU_SYSTEM, 1, .ColumnCount) = False Then
                                    Return False
                                End If
                                'エラーを返す
                                Return False
                            End If

                            '入力チェック-作業予実：作業担当G(必須)
                            If blnStateKanryo AndAlso strTantoG = "" Then
                                'エラーメッセージ設定
                                puErrMsg = D0201_E015
                                'タブを基本情報タブに設定
                                dataHBKD0201.PropTbInput.SelectedIndex = TAB_KHN
                                'フォーカス設定
                                If commonLogicHBK.SetFocusOnVwRow(dataHBKD0201.PropVwPrbYojitsu, _
                                                                  0, i, COL_YOJITSU_TANTOGRP1, 1, .ColumnCount) = False Then
                                    Return False
                                End If
                                'エラーを返す
                                Return False
                            End If

                            '入力チェック-作業予実：作業担当(必須)
                            If blnStateKanryo AndAlso strTantoU = "" Then
                                'エラーメッセージ設定
                                puErrMsg = D0201_E015
                                'タブを基本情報タブに設定
                                dataHBKD0201.PropTbInput.SelectedIndex = TAB_KHN
                                'フォーカス設定
                                If commonLogicHBK.SetFocusOnVwRow(dataHBKD0201.PropVwPrbYojitsu, _
                                                                  0, i, COL_YOJITSU_PRBTANTONM1, 1, .ColumnCount) = False Then
                                    Return False
                                End If
                                'エラーを返す
                                Return False
                            End If

                            '範囲チェック-作業予実：作業開始日時と作業終了日時の範囲チェック（作業開始日時＝作業完了日時の場合OK）
                            If (strKaishi <> "" And strSyuryo <> "") AndAlso strKaishi > strSyuryo Then
                                'エラーメッセージ設定
                                puErrMsg = D0201_E016
                                'タブを基本情報タブに設定
                                dataHBKD0201.PropTbInput.SelectedIndex = TAB_KHN
                                'フォーカス設定
                                If commonLogicHBK.SetFocusOnVwRow(dataHBKD0201.PropVwPrbYojitsu, _
                                                                  0, i, COL_YOJITSU_WORKEDDT, 1, .ColumnCount) = False Then
                                    Return False
                                End If
                                'エラーを返す
                                Return False
                            End If

                        Next i

                    End If

                End With

                'CYSPR情報重複チェック
                With .PropVwCysprInfo.Sheets(0)
                    Dim dt As DataTable = .DataSource
                    '削除情報などはコミットしておく
                    dt.AcceptChanges()

                    '1行以上ある場合、チェックを行う
                    If dt.Rows.Count > 0 Then

                        '一覧の行数分繰り返し
                        For i As Integer = 0 To dt.Rows.Count - 1
                            Dim ct As Integer = 0
                            'ブランク以外のデータで
                            If dt.Rows(i).Item(0).ToString <> "" Then
                                For j As Integer = 0 To dt.Rows.Count - 1
                                    If dt.Rows(i).Item(0).Equals(dt.Rows(j).Item(0)) Then
                                        ct += 1
                                    End If
                                Next
                                '?:.重複チェック
                                If ct > 1 Then
                                    'エラーメッセージ設定
                                    puErrMsg = D0201_E024
                                    'タブを基本情報タブに設定
                                    dataHBKD0201.PropTbInput.SelectedIndex = TAB_KHN
                                    'フォーカス設定
                                    If commonLogicHBK.SetFocusOnVwRow(dataHBKD0201.PropVwCysprInfo, _
                                                                      0, i, COL_CYSPR_CYSPRNMB, 1, .ColumnCount) = False Then
                                        Return False
                                    End If
                                    'エラーを返す
                                    Return False
                                End If
                            End If
                        Next i

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
    ''' ロック解除チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>他のユーザによりロックが解除されていないかチェックする
    ''' <para>作成情報：2012/08/21 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckBeUnlockedMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロック解除チェック
        If CheckUnlock(DataHBKD0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【編集モード】ロック解除チェック処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータのロックが解除されていないかチェックする
    ''' <para>作成情報：2012/08/21 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckUnlock(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnBeUnocked As Boolean = False       'ロックフラグ

        Try

            'ロック解除チェック
            If CheckPrbDataBeUnlocked(dataHBKD0201.PropIntPrbNmb, dataHBKD0201.PropStrEdiTime, _
                                                     blnBeUnocked, dataHBKD0201.PropDtPrbInfoLock) = False Then
                Return False
            End If

            'ロック解除されている場合、ロックフラグON
            If blnBeUnocked = True Then

                dataHBKD0201.PropBlnBeLockedFlg = True

            Else

                dataHBKD0201.PropBlnBeLockedFlg = False

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
    ''' 【参照モード】編集モードから作業履歴編集モードへ変更時のメイン処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面の内容をログに出力し、フォームコントロールを再設定する
    ''' <para>作成情報：2012/08/21 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetFormRefModeFromEditModeMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '入力内容ログ出力
        If OutputUnlockLog(dataHBKD0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】ロック解除され時ログ出力処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>他のユーザによりロックが解除された場合に編集中の入力内容をログに出力する
    ''' <para>作成情報：2012/08/21 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function OutputUnlockLog(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '定数宣言
        Const SEP_HF_SPC As String = " "            '半角スペース

        '変数宣言
        Dim strPlmList As New List(Of String)       'フォーマット埋込用パラメータ配列
        Dim strLogFilePath As String = ""           'ログファイルパス
        Dim strLogFileName As String                'ログファイル名
        Dim strOutputDir As String                  'ログ出力フォルダ

        Dim strText_Yojitsu As String = ""          '作業予実パラメータ文
        Dim strText_Meeting As String = ""          '会議情報パラメータ文
        Dim strText_Relation As String = ""         '関係者情報パラメータ文
        Dim strText_PLink As String = ""            'プロセスリンクパラメータ文
        Dim strText_Cyspr As String = ""            '問題CYSPRパラメータ文
        Dim strText_File As String = ""             '関連ファイルパラメータ文

        Dim Sw As StreamWriter = Nothing            'ファイル書込用クラス

        Try

            With dataHBKD0201

                strPlmList.Add(.PropTxtPrbNmb.Text)                     '0:問題番号
                strPlmList.Add(.PropCmbStatus.Text)                     '1:ステータス
                strPlmList.Add(.PropDtpStartDT.txtDate.Text)            '2:開始日時
                strPlmList.Add(.PropDtpKanryoDT.txtDate.Text)           '3:開始日時
                strPlmList.Add(.PropCmbTargetSystem.txtDisplay.Text)    '4:対象システム
                strPlmList.Add(.PropCmbPrbCase.Text)                    '5:発生原因
                strPlmList.Add(.PropCmbTantoGrp.Text)                   '6:担当グループ
                strPlmList.Add(.PropTxtPrbTantoID.Text)                 '7:担当ID
                strPlmList.Add(.PropTxtPrbTantoNM.Text)                 '8:担当氏名
                strPlmList.Add(.PropTxtTitle.Text)                      '9:タイトル
                strPlmList.Add(.PropTxtNaiyo.Text)                      '10:内容
                strPlmList.Add(.PropTxtTaisyo.Text)                     '11:対処
                strPlmList.Add(.PropTxtApproverID.Text)                 '12:対処承認者ID
                strPlmList.Add(.PropTxtApproverNM.Text)                 '13:対処承認者氏名
                strPlmList.Add(.PropTxtRecorderID.Text)                 '14:承認記録者ID
                strPlmList.Add(.PropTxtRecorderNM.Text)                 '15:承認記録者氏名

                '16:作業予実
                If .PropVwPrbYojitsu.Sheets(0).RowCount > 0 Then
                    With .PropVwPrbYojitsu.Sheets(0)
                        '一覧行数分繰り返し、パラメータ文を作成
                        For i As Integer = 0 To .RowCount - 1
                            strText_Yojitsu &= (i + 1).ToString() & ":" & .GetText(i, COL_YOJITSU_WORKSTATENM)
                            strText_Yojitsu &= SEP_HF_SPC & .GetText(i, COL_YOJITSU_WORKNAIYO)
                            strText_Yojitsu &= SEP_HF_SPC & .GetText(i, COL_YOJITSU_WORKSCEDT)
                            strText_Yojitsu &= SEP_HF_SPC & .GetText(i, COL_YOJITSU_WORKSTDT)
                            strText_Yojitsu &= SEP_HF_SPC & .GetText(i, COL_YOJITSU_WORKEDDT)
                            strText_Yojitsu &= SEP_HF_SPC & .GetText(i, COL_YOJITSU_SYSTEM)
                            For j As Integer = 0 To 49
                                strText_Yojitsu &= SEP_HF_SPC & .GetText(i, COL_YOJITSU_TANTOGRP1 + (j * YOJITSU_TANTO_COLCNT))
                                strText_Yojitsu &= SEP_HF_SPC & .GetText(i, COL_YOJITSU_PRBTANTONM1 + (j * YOJITSU_TANTO_COLCNT))
                            Next
                            '最終行以外は改行コード追加
                            If i < .RowCount - 1 Then
                                strText_Yojitsu &= vbCrLf
                            End If
                        Next
                    End With
                End If
                strPlmList.Add(strText_Yojitsu)

                '17:会議情報
                If .PropVwMeeting.Sheets(0).RowCount > 0 Then
                    With .PropVwMeeting.Sheets(0)
                        '一覧行数分繰り返し、パラメータ文を作成
                        For i As Integer = 0 To .RowCount - 1

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

                strPlmList.Add(.PropTxtFreeText1.Text)                  '18:フリーテキスト1
                strPlmList.Add(.PropTxtFreeText2.Text)                  '19:フリーテキスト2
                strPlmList.Add(.PropTxtFreeText3.Text)                  '20:フリーテキスト3
                strPlmList.Add(.PropTxtFreeText4.Text)                  '21:フリーテキスト4
                strPlmList.Add(.PropTxtFreeText5.Text)                  '22:フリーテキスト5

                '23:フリーフラグ1
                If .PropChkFreeFlg1.Checked = True Then
                    strPlmList.Add(FREE_FLG_ON_NM)
                Else
                    strPlmList.Add(FREE_FLG_OFF_NM)
                End If
                '24:フリーフラグ2
                If .PropChkFreeFlg2.Checked = True Then
                    strPlmList.Add(FREE_FLG_ON_NM)
                Else
                    strPlmList.Add(FREE_FLG_OFF_NM)
                End If
                '25:フリーフラグ3
                If .PropChkFreeFlg3.Checked = True Then
                    strPlmList.Add(FREE_FLG_ON_NM)
                Else
                    strPlmList.Add(FREE_FLG_OFF_NM)
                End If
                '26:フリーフラグ4
                If .PropChkFreeFlg4.Checked = True Then
                    strPlmList.Add(FREE_FLG_ON_NM)
                Else
                    strPlmList.Add(FREE_FLG_OFF_NM)
                End If
                '27:フリーフラグ5
                If .PropChkFreeFlg5.Checked = True Then
                    strPlmList.Add(FREE_FLG_ON_NM)
                Else
                    strPlmList.Add(FREE_FLG_OFF_NM)
                End If

                '28:対応関係者情報
                If .PropVwRelationInfo.Sheets(0).RowCount > 0 Then
                    With .PropVwRelationInfo.Sheets(0)
                        '一覧行数分繰り返し、パラメータ文を作成
                        For i As Integer = 0 To .RowCount - 1

                            Dim strKbn As String = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_PBMKANKEI_RELATIONKBN), "")
                            Dim strID As String = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_PBMKANKEI_RELATIONID), "")
                            Dim strNM As String = ""

                            If strKbn = KBN_GROUP Then
                                strNM = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_PBMKANKEI_GRPNM), "")
                            ElseIf strKbn = KBN_USER Then
                                strNM = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_PBMKANKEI_HBKUSRNM), "")
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
                
                '29:プロセスリンク情報
                If .PropVwProcessLinkInfo.Sheets(0).RowCount > 0 Then
                    With .PropVwProcessLinkInfo.Sheets(0)
                        '一覧行数分繰り返し、パラメータ文を作成
                        For i As Integer = 0 To .RowCount - 1

                            Dim strKbn As String = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_PLINK_PLINKKBN), "")
                            Dim strID As String = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_PLINK_PLINKNO), "")

                            strText_PLink &= (i + 1).ToString() & "." & strKbn & " " & strID
                            '最終行以外は改行コード追加
                            If i < .RowCount - 1 Then
                                strText_PLink &= vbCrLf
                            End If

                        Next
                    End With
                End If
                strPlmList.Add(strText_PLink)
                
                '30:CYSPR情報
                If .PropVwCysprInfo.Sheets(0).RowCount > 0 Then
                    With .PropVwCysprInfo.Sheets(0)
                        '一覧行数分繰り返し、パラメータ文を作成
                        For i As Integer = 0 To .RowCount - 1

                            Dim strNaiyo As String = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_CYSPR_CYSPRNMB), "")

                            strText_Cyspr &= (i + 1).ToString() & "." & strNaiyo
                            '最終行以外は改行コード追加
                            If i < .RowCount - 1 Then
                                strText_Cyspr &= vbCrLf
                            End If

                        Next
                    End With
                End If
                strPlmList.Add(strText_Cyspr)

                '31:関連ファイル情報
                If .PropVwPrbFileInfo.Sheets(0).RowCount > 0 Then
                    With .PropVwPrbFileInfo.Sheets(0)
                        '一覧行数分繰り返し、パラメータ文を作成
                        For i As Integer = 0 To .RowCount - 1

                            Dim strNaiyo As String = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_PRBFILE_NAIYO), "")

                            strText_File &= (i + 1).ToString() & "." & strNaiyo
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
                If GetSysdate(dataHBKD0201) = False Then
                    Return False
                End If

                'ログファイル名設定
                strLogFileName = Format(.PropDtmSysDate, "yyyyMMddHHmmss") & ".log"
                'strLogFileName = Format(DateTime.Parse(.PropDtPrbInfoLock.Rows(0).Item("SysTime")), "yyyyMMddHHmmss") & ".log"

                'ファイル出力を実行し、出力ファイルパスを取得
                If commonLogicHBK.OutputLogFromTextFormat(strPlmList, strLogFileName, _
                                                          FORMAT_DIR_UNLOCKEDLOG, _
                                                          FILE_UNLOCKLOG_QUESTION, _
                                                          strOutputDir, _
                                                          strLogFilePath) = False Then
                    Return False
                End If

                'データクラスにメッセージをセット
                dataHBKD0201.PropStrBeUnlockedMsg = String.Format(D0201_W001, strLogFilePath)

                'システムエラー時は以下を設定
                If puErrMsg.StartsWith(HBK_E001) Then
                    dataHBKD0201.PropStrBeUnlockedMsg = String.Format(D0201_E021, strLogFilePath)
                End If

                'ログファイルパスをプロパティにセット(出力メッセージのメッセージボックススタイル判定用)
                dataHBKD0201.PropStrLogFilePath = strLogFilePath

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            If Sw IsNot Nothing Then
                Sw.Close()
            End If
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            If Sw IsNot Nothing Then
                Sw.Dispose()
            End If
        End Try

    End Function


    ''' <summary>
    ''' システム日付取得
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システム日付を取得する
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetSysDate(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter()    'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            'システム日付取得（SELECT）
            If SelectSysDate(Adapter, Cn, dataHBKD0201) = False Then
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
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【新規登録モード】データ登録メイン処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をデータベースに反映する
    ''' <para>作成情報：2012/08/21 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RegistDataOnNewModeMain(ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '登録前対応関係者処理
        If GetDtSysKankei(dataHBKD0201) = False Then
            Return False
        End If

        '新規登録処理
        If InsertNewData(dataHBKD0201) = False Then
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
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をデータベースに反映する
    ''' <para>作成情報：2012/08/23 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RegistDataOnEditModeMain(ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '登録前対応関係者処理
        If GetDtSysKankei(dataHBKD0201) = False Then
            Return False
        End If

        '更新処理
        If UpdateData(DataHBKD0201) = False Then
            Return False
        End If

        'ロック解除処理
        If UnlockData(dataHBKD0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【作業予実モード】データ登録メイン処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をデータベースに反映する
    ''' <para>作成情報：2012/08/23 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RegistDataOnYojitsuModeMain(ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '登録前対応関係者処理
        If GetDtSysKankei(dataHBKD0201) = False Then
            Return False
        End If

        '更新処理
        If UpdateData_Yojitsu(dataHBKD0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】登録前対応関係者処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対応関係者情報を確認する
    ''' <para>作成情報：2012/08/21 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetDtSysKankei(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            '対象システム関係者データ取得
            If GetSysKankei(Adapter, Cn, DataHBKD0201) = False Then
                Return False
            End If

            '対象システム変更チェック
            If CheckSysNmb(Adapter, Cn, dataHBKD0201) = False Then
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
            Cn.Dispose()
            Adapter.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】対象システム変更チェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対象システムが変更されたかチェックする
    ''' <para>作成情報：2012/10/11 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckSysNmb(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKD0201.GetChkSysNmbData(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対象システムの変更有無情報取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)


            If dtmst IsNot Nothing AndAlso dtmst.Rows.Count > 0 Then
                If dtmst.Rows(0).Item(0).ToString.Equals(dataHBKD0201.PropCmbTargetSystem.PropCmbColumns.SelectedValue.ToString) Then
                    dataHBKD0201.PropBlnCheckSystemNmb = False
                Else
                    '更新前と対象システムが違う場合True
                    dataHBKD0201.PropBlnCheckSystemNmb = True
                End If
            Else
                dataHBKD0201.PropBlnCheckSystemNmb = False
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
    ''' 【共通】対象システム関係者データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対象システムのCI番号から関係データを取得する
    ''' <para>作成情報：2012/08/21 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetSysKankei(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtTableData As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKD0201.GetChkKankeiSysData(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対象システム関係取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtTableData)

            '取得データをデータクラスにセット
            dataHBKD0201.PropDtResultPLink = dtTableData

            '終了ログ出力
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
            dtTableData.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【新規登録モード】データ新規登録処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をDBに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/21 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertNewData(ByVal dataHBKD0201 As DataHBKD0201) As Boolean

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

            '新規問題番号、システム日付取得（SELECT）
            If SelectNewPrbNmbAndSysDate(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            '問題共通情報新規登録（INSERT）
            If InsertProblemInfo(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            '担当履歴情報チェック
            If InsertTantoRireki(Cn, dataHBKD0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If


            '問題作業履歴＋作業担当  新規登録（INSERT）
            If InsertProblemWkRireki(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            ''問題作業担当 新規登録（INSERT）
            'If InsertProblemWkTanto(Cn, dataHBKD0201) = False Then
            '    'ロールバック
            '    Tsx.Rollback()
            '    Return False
            'End If

            '問題対応関係情報新規登録（INSERT）
            If InsertProblemKankei(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            'プロセスリンク新規登録（INSERT）
            If InsertPrbPLink(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            'CYSPR情報登録
            If InsertProblemCyspr(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            '関連ファイル情報新規登録（INSERT）
            If InsertPrbFile(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            '新規ログNo取得
            If GetNewRirekiNo(Cn, dataHBKD0201) = False Then
                Return False
            End If

            '問題共通ログテーブル登録
            If InserProblemInfoL(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            '問題作業履歴ログテーブル登録
            If InserProblemWkRirekiL(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            '問題作業担当ログテーブル登録
            If InsertProblemWkTantoL(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            '問題対応関係情報ログテーブル登録
            If InsertProblemKankeiL(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            '問題プロセスリンク(元)ログテーブル登録
            If InsertPLinkMotoL(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            '問題CYSPR情報ログ登録
            If InsertProblemCysprL(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            '問題関連ファイルログテーブル登録
            If InsertProblemFileL(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
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
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            Tsx.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【新規登録／編集モード】新規問題番号取得処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番した問題番号を取得（SELECT）する
    ''' <para>作成情報：2012/08/21 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectNewPrbNmbAndSysDate(ByVal Cn As NpgsqlConnection, _
                                               ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtResult As New DataTable

        Try

            '新規問題番号取得（SELECT）用SQLを作成
            If sqlHBKD0201.SetSelectNewPrbNmbAndSysDateSql(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規問題番号、システム日付取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            'データが取得できた場合、データクラスに取得データをセット
            If dtResult.Rows.Count > 0 Then
                dataHBKD0201.PropIntPrbNmb = dtResult.Rows(0).Item("PrbNmb")      '新規inc番号
                dataHBKD0201.PropDtmSysDate = dtResult.Rows(0).Item("SysDate")  'サーバー日付
            Else
                '取得できなかったときはエラー
                'puErrMsg = C0201_E013
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
    ''' 【新規登録／編集モード】問題共通情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容を問題共通情報テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/21 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertProblemInfo(ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            '問題共通情報新規登録（INSERT）用SQLを作成
            If sqlHBKD0201.SetInsertProblemInfoSql(Cmd, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "問題共通情報新規登録", Nothing, Cmd)

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
    ''' 【新規登録／編集モード】問題作業履歴情報 新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容を作業履歴テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertProblemWkRireki(ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド
        Dim row As DataRow

        Try
            With dataHBKD0201

                'データテーブルを取得
                .PropDtwkRireki = DirectCast(.PropVwPrbYojitsu.Sheets(0).DataSource, DataTable)

                If .PropDtwkRireki IsNot Nothing AndAlso .PropDtwkRireki.Rows.Count > 0 Then
                    'データ数分繰り返し、登録処理を行う 
                    For i As Integer = 0 To .PropDtwkRireki.Rows.Count - 1

                        row = .PropDtwkRireki.Rows(i)

                        .PropDrRegRow = row

                        'データの追加／削除状況に応じて新規登録／削除処理を行う
                        If row.RowState = DataRowState.Added Then           '追加時


                            '新規登録
                            If sqlHBKD0201.SetInsertProblemWkRirekiSql(Cmd, Cn, dataHBKD0201) = False Then
                                Return False
                            End If

                            'SQL実行
                            Cmd.ExecuteNonQuery()

                            'ログ出力
                            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "作業履歴情報　新規登録", Nothing, Cmd)


                            '削除
                            If sqlHBKD0201.SetDeleteProblemWkTantoSql(Cmd, Cn, dataHBKD0201) = False Then
                                Return False
                            End If

                            'ログ出力
                            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "作業担当情報 削除", Nothing, Cmd)

                            'SQL実行
                            Cmd.ExecuteNonQuery()

                            '担当者１～５０
                            For j As Integer = 1 To 50

                                If row.Item("worktantogrpnm" & j).ToString.Equals("") = False Then
                                    '新規登録
                                    If sqlHBKD0201.SetInsertProblemWkTantoSql(Cmd, Cn, dataHBKD0201, j) = False Then
                                        Return False
                                    End If

                                    'ログ出力
                                    commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "作業担当情報 新規登録", Nothing, Cmd)

                                    'SQL実行
                                    Cmd.ExecuteNonQuery()
                                Else
                                    'ブランクあったら抜ける
                                    Exit For
                                End If
                            Next

                        ElseIf row.RowState = DataRowState.Modified Then           '更新時

                            '更新
                            If sqlHBKD0201.SetUpdateProblemWkRirekiSql(Cmd, Cn, dataHBKD0201) = False Then
                                Return False
                            End If

                            'SQL実行
                            Cmd.ExecuteNonQuery()

                            'ログ出力
                            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "作業履歴情報　更新", Nothing, Cmd)

                            '削除
                            If sqlHBKD0201.SetDeleteProblemWkTantoSql(Cmd, Cn, dataHBKD0201) = False Then
                                Return False
                            End If

                            'ログ出力
                            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "作業担当情報 削除", Nothing, Cmd)

                            'SQL実行
                            Cmd.ExecuteNonQuery()

                            '担当者１～５０
                            For j As Integer = 1 To 50

                                If row.Item("worktantogrpnm" & j).ToString.Equals("") = False Then
                                    '新規登録
                                    If sqlHBKD0201.SetUpdateProblemWkTantoSql(Cmd, Cn, dataHBKD0201, j) = False Then
                                        Return False
                                    End If

                                    'ログ出力
                                    commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "作業担当情報 新規登録", Nothing, Cmd)

                                    'SQL実行
                                    Cmd.ExecuteNonQuery()
                                Else
                                    'ブランクあったら抜ける
                                    Exit For
                                End If
                            Next

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
            Cmd.Dispose()
        End Try

    End Function

    ' ''' <summary>
    ' ''' 【新規登録／編集モード】問題作業担当履歴情報 新規登録処理
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>入力内容を作業担当テーブルに新規登録（INSERT）する
    ' ''' <para>作成情報：2012/08/22 s.yamaguchi
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function InsertProblemWkTanto(ByVal Cn As NpgsqlConnection, _
    '                                      ByVal dataHBKD0201 As DataHBKD0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try
    '        With dataHBKD0201

    '            '作業履歴一覧の行数分繰り返し、登録処理を行う
    '            For i As Integer = 0 To .PropVwPrbYojitsu.Sheets(0).RowCount - 1

    '                '入力値取得
    '                Dim intWktantNmbCnt As Integer = 0
    '                For j As Integer = COL_YOJITSU_TANTOGRP1 To COL_YOJITSU_PRBTANTO_BTN - 1 Step YOJITSU_TANTO_COLCNT

    '                    '登録行作成strTantoGp
    '                    Dim row As DataRow = .PropDtProblemWkTanto.NewRow
    '                    row.Item("WorkRirekiNmb") = i + 1
    '                    row.Item("WorkTantoGrpNM") = .PropVwPrbYojitsu.Sheets(0).GetText(i, j + 0)
    '                    row.Item("WorkTantoNM") = .PropVwPrbYojitsu.Sheets(0).GetText(i, j + 1)
    '                    row.Item("WorkTantoGrpCD") = .PropVwPrbYojitsu.Sheets(0).GetText(i, j + 2)
    '                    row.Item("TantoID") = .PropVwPrbYojitsu.Sheets(0).GetText(i, j + 3)

    '                    '入力のあるデータのみを登録
    '                    If Not row.Item("WorkTantoGrpCD").Equals("") Then

    '                        '作業連番
    '                        intWktantNmbCnt += 1
    '                        row.Item("WorkTantoNmb") = intWktantNmbCnt

    '                        '作成した行をデータクラスにセット
    '                        .PropDrRegRow = row

    '                        '問題作業担当新規登録（INSERT）用SQLを作成
    '                        If sqlHBKD0201.SetInsertProblemWkTantoSql(Cmd, Cn, dataHBKD0201) = False Then
    '                            Return False
    '                        End If

    '                        'ログ出力
    '                        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "問題作業担当情報 新規登録", Nothing, Cmd)

    '                        'SQL実行
    '                        Cmd.ExecuteNonQuery()

    '                    End If

    '                Next

    '            Next

    '        End With

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

    ''' <summary>
    ''' 【新規登録／編集モード】問題対応関係新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容を関係者情報テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertProblemKankei(ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド
        Dim blnAddFlg As Boolean = True
        Dim dtRelationTemp As New DataTable       'スプレッドデータ一時保存用

        Try

            With dataHBKD0201
                '★GetSysKankeiにてPropDtResultPLinkに関係者チェック用の情報を取得している。
                'スプレッドのデータソースを取得
                dtRelationTemp = .PropVwRelationInfo.DataSource
                dtRelationTemp.AcceptChanges()

                '★新規登録時のみ
                If .PropStrProcMode = PROCMODE_NEW Then
                    'ログインユーザのグループがあるかチェック
                    For i As Integer = 0 To dtRelationTemp.Rows.Count - 1
                        If dtRelationTemp.Rows(i).Item("RelationID").Equals(PropWorkGroupCD) Then
                            blnAddFlg = False
                        End If
                    Next
                    'ない場合追加
                    If blnAddFlg = True Then
                        Dim row As DataRow = dtRelationTemp.NewRow
                        row.Item("RelationKbn") = KBN_GROUP
                        row.Item("RelationID") = PropWorkGroupCD
                        dtRelationTemp.Rows.Add(row)
                    End If
                End If

                '★新規登録時、または対象システムに変更があった場合
                If .PropStrProcMode = PROCMODE_NEW Or .PropBlnCheckSystemNmb = True Then
                    '取得した関係テーブルがあればチェックする
                    If .PropDtResultPLink IsNot Nothing Then
                        For i As Integer = 0 To .PropDtResultPLink.Rows.Count - 1

                            '追加フラグ初期化
                            blnAddFlg = True

                            '関係テーブルのグループがあるかチェック
                            If .PropDtResultPLink.Rows(i).Item("relationkbn").Equals(KBN_GROUP) Then
                                For j As Integer = 0 To dtRelationTemp.Rows.Count - 1
                                    If dtRelationTemp.Rows(j).Item("relationkbn") = KBN_GROUP Then
                                        If dtRelationTemp.Rows(j).Item("RelationID").Equals(.PropDtResultPLink.Rows(i).Item("RelationID")) Then
                                            blnAddFlg = False
                                            Exit For
                                        End If
                                    End If
                                Next

                                'ない場合追加
                                If blnAddFlg = True Then
                                    Dim row As DataRow = dtRelationTemp.NewRow
                                    row.Item("RelationKbn") = KBN_GROUP
                                    row.Item("RelationID") = .PropDtResultPLink.Rows(i).Item("RelationID")
                                    dtRelationTemp.Rows.Add(row)
                                End If

                            ElseIf .PropDtResultPLink.Rows(i).Item("RelationKbn").Equals(KBN_USER) Then
                                '関係テーブルのユーザがあるかチェック
                                For j As Integer = 0 To dtRelationTemp.Rows.Count - 1
                                    If dtRelationTemp.Rows(j).Item("RelationKbn") = KBN_USER Then
                                        If dtRelationTemp.Rows(j).Item("RelationID").Equals(.PropDtResultPLink.Rows(i).Item("RelationID")) Then
                                            blnAddFlg = False
                                            Exit For
                                        End If
                                    End If
                                Next

                                'ない場合追加
                                If blnAddFlg = True Then
                                    Dim row As DataRow = dtRelationTemp.NewRow
                                    row.Item("RelationKbn") = KBN_USER
                                    row.Item("RelationID") = .PropDtResultPLink.Rows(i).Item("RelationID")
                                    dtRelationTemp.Rows.Add(row)
                                End If
                            End If
                        Next

                    End If
                End If


                '修正した関係者のテーブルにて
                For i As Integer = 0 To dtRelationTemp.Rows.Count - 1

                    ''登録行作成
                    'Dim row As DataRow = dtRelationTemp.NewRow
                    'row.Item("RelationKbn") = dtRelationTemp.Rows(i).Item(0)        'G,U(KBN_GROUP,KBN_USER)
                    'row.Item("RelationID") = dtRelationTemp.Rows(i).Item(1)         '3ケタ,7ケタ
                    'row.Item("RegDT") = dtRelationTemp.Rows(i).Item(4)
                    'row.Item("RegGrpCD") = dtRelationTemp.Rows(i).Item(5)
                    'row.Item("RegID") = dtRelationTemp.Rows(i).Item(6)
                    'row.Item("UpdateDT") = dtRelationTemp.Rows(i).Item(7)
                    'row.Item("UpGrpCD") = dtRelationTemp.Rows(i).Item(8)
                    'row.Item("UpdateID") = dtRelationTemp.Rows(i).Item(9)
                    '登録行作成
                    Dim row As DataRow = dtRelationTemp.Rows(i)

                    '作成した行をデータクラスにセット
                    .PropDrRegRow = row

                    '関係者情報新規登録（INSERT）用SQLを作成
                    If sqlHBKD0201.SetInsertProblemKankeiSql(Cmd, Cn, dataHBKD0201) = False Then
                        Return False
                    End If

                    'ログ出力
                    commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "問題対応関係情報新規登録", Nothing, Cmd)

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
    ''' 【新規登録／編集モード】プロセスリンク登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をプロセスリンク情報テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/23 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertPrbPLink(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim Cmd As New NpgsqlCommand            'SQLコマンド
        Dim row As DataRow
        Dim cnt As Integer

        Try
            With dataHBKD0201

                'データテーブルを取得
                .PropDtProcessLink = DirectCast(.PropVwProcessLinkInfo.Sheets(0).DataSource, DataTable)

                If .PropDtProcessLink IsNot Nothing Then

                    If .PropDtProcessLink.Rows.Count > 0 Then

                        'データ数分繰り返し、登録処理を行う
                        For i As Integer = 0 To .PropDtProcessLink.Rows.Count - 1

                            row = .PropDtProcessLink.Rows(i)

                            .PropDrRegRow = row

                            'データの追加／削除状況に応じて新規登録／削除処理を行う
                            If row.RowState = DataRowState.Added Then           '追加時

                                '登録順カウンタ
                                cnt += 1

                                '新規登録
                                If sqlHBKD0201.InsertPLinkMoto(Cmd, Cn, dataHBKD0201, cnt) = False Then
                                    Return False
                                End If

                                'SQL実行
                                Cmd.ExecuteNonQuery()

                                'ログ出力
                                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "プロセスリンク(元)情報新規登録", Nothing, Cmd)

                            ElseIf row.RowState = DataRowState.Deleted Then     '削除時

                                '削除
                                If sqlHBKD0201.DeletePLinkMoto(Cmd, Cn, dataHBKD0201) = False Then
                                    Return False
                                End If

                                'SQL実行
                                Cmd.ExecuteNonQuery()

                                'ログ出力
                                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "プロセスリンク(元)情報削除", Nothing, Cmd)

                                '削除
                                If sqlHBKD0201.DeletePLinkSaki(Cmd, Cn, dataHBKD0201) = False Then
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

    ' ''' <summary>
    ' ''' 【新規登録／編集モード】CYSPR情報新規登録処理
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>入力内容を問題CYSPR情報テーブルに新規登録（INSERT）する
    ' ''' <para>作成情報：2012/08/23 s.yamaguchi
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function InsertProblemCyspr(ByVal Cn As NpgsqlConnection, _
    '                                    ByVal dataHBKD0201 As DataHBKD0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand                'SQLコマンド
    '    Dim dtRelationTemp As New DataTable         'スプレッドデータ一時保存用

    '    Try

    '        With dataHBKD0201

    '            'スプレッドのデータソースを取得
    '            dtRelationTemp = .PropVwCysprInfo.DataSource
    '            dtRelationTemp.AcceptChanges()

    '            '修正した関係者のテーブルにて
    '            For i As Integer = 0 To dtRelationTemp.Rows.Count - 1

    '                If .PropVwCysprInfo.Sheets(0).GetText(i, COL_CYSPR_CYSPRNMB) <> "" Then
    '                    '登録行作成
    '                    Dim row As DataRow = .PropDtProblemCyspr.NewRow
    '                    row.Item("CysprNmb") = dtRelationTemp.Rows(i).Item(0)
    '                    row.Item("RegDT") = dtRelationTemp.Rows(i).Item(1)
    '                    row.Item("RegGrpCD") = dtRelationTemp.Rows(i).Item(2)
    '                    row.Item("RegID") = dtRelationTemp.Rows(i).Item(3)
    '                    row.Item("UpdateDT") = dtRelationTemp.Rows(i).Item(4)
    '                    row.Item("UpGrpCD") = dtRelationTemp.Rows(i).Item(5)
    '                    row.Item("UpdateID") = dtRelationTemp.Rows(i).Item(6)

    '                    '作成した行をデータクラスにセット
    '                    .PropDrRegRow = row

    '                    '問題CYSPR情報新規登録（INSERT）用SQLを作成
    '                    If sqlHBKD0201.SetInsertProblemCysprSql(Cmd, Cn, dataHBKD0201) = False Then
    '                        Return False
    '                    End If

    '                    'ログ出力
    '                    commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "問題CYSPR情報新規登録", Nothing, Cmd)

    '                    'SQL実行
    '                    Cmd.ExecuteNonQuery()
    '                End If

    '            Next

    '        End With

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

    ''' <summary>
    ''' A-2-7.【新規登録／編集モード】CYSPR新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容を関連ファイルテーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertProblemCyspr(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            With dataHBKD0201
                'データテーブルを取得
                '入力チェックでコミットしているので注意
                .PropDtProblemCyspr = DirectCast(.PropVwCysprInfo.Sheets(0).DataSource, DataTable)

                For i As Integer = 0 To .PropDtProblemCyspr.Rows.Count - 1

                    'ブランクは除外する
                    If .PropDtProblemCyspr.Rows(i).Item(0).ToString <> "" Then
                        '登録行作成
                        Dim row As DataRow = .PropDtProblemCyspr.Rows(i)

                        '作成した行をデータクラスにセット
                        .PropDrRegRow = row

                        'CYSPR情報新規登録（INSERT）用SQLを作成
                        If sqlHBKD0201.SetInsertProblemCysprSql(Cmd, Cn, dataHBKD0201) = False Then
                            Return False
                        End If

                        'ログ出力
                        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CYSPR情報新規登録", Nothing, Cmd)

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
            Adapter.Dispose()
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【新規登録／編集モード】問題関連ファイル新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容を関連ファイルテーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertPrbFile(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            With dataHBKD0201

                '最新のファイル情報データテーブルを取得
                .PropDtProblemFile = DirectCast(.PropVwPrbFileInfo.Sheets(0).DataSource, DataTable)

                If .PropDtProblemFile IsNot Nothing Then

                    '関連ファイルアップロード／登録
                    Dim aryStrNewDirPath As New ArrayList
                    If commonLogicHBK.UploadAndRegFile(Adapter, _
                                                       Cn, _
                                                       .PropIntPrbNmb, _
                                                       .PropDtProblemFile, _
                                                       .PropDtmSysDate, _
                                                       UPLOAD_FILE_PROBLEM, _
                                                       aryStrNewDirPath) = False Then
                        Return False
                    End If

                End If

            End With

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "関連ファイル登録", Nothing, Cmd)

            '終了ログ出力
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
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容で会議結果情報テーブルを更新（Update）する
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertMtgResult(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim Cmd As New NpgsqlCommand            'SQLコマンド
        Dim row As DataRow

        Try
            With dataHBKD0201

                'データテーブルを取得
                .PropDtMeeting = DirectCast(.PropVwMeeting.Sheets(0).DataSource, DataTable)

                If .PropDtMeeting IsNot Nothing Then

                    If .PropDtMeeting.Rows.Count > 0 Then

                        'データ数分繰り返し、登録処理を行う 
                        For i As Integer = 0 To .PropDtMeeting.Rows.Count - 1

                            row = .PropDtMeeting.Rows(i)

                            .PropDrRegRow = row


                            'データの追加／削除状況に応じて新規登録／削除処理を行う
                            If row.RowState = DataRowState.Added Then           '追加時


                                '新規登録
                                If sqlHBKD0201.SetInsertMtgResultSql(Cmd, Cn, dataHBKD0201) = False Then
                                    Return False
                                End If

                                'SQL実行
                                Cmd.ExecuteNonQuery()

                                'ログ出力
                                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議結果情報新規登録", Nothing, Cmd)



                            ElseIf row.RowState = DataRowState.Deleted Then     '削除時

                                '削除
                                If sqlHBKD0201.SetDeleteMtgResultSql(Cmd, Cn, dataHBKD0201) = False Then
                                    Return False
                                End If

                                'SQL実行
                                Cmd.ExecuteNonQuery()

                                'ログ出力
                                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議結果情報削除", Nothing, Cmd)


                            End If

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
            Adapter.Dispose()
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】新規ログNo取得
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番したログNoを取得する
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetNewRirekiNo(ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter    'アダプタ
        Dim dtLogNo As New DataTable            'ログNo格納用テーブル

        Try

            'SQLを作成
            If sqlHBKD0201.SetSelectNewRirekiNoSql(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規ログNo取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtLogNo)

            'データが取得できた場合、データクラスにログNoをセット
            If dtLogNo.Rows.Count > 0 Then
                dataHBKD0201.PropIntLogNo = dtLogNo.Rows(0).Item("LogNo")
            Else
                '取得できなかった場合はエラー
                '**********************************
                'メッセージは変更する
                puErrMsg = D0201_E022
                '**********************************
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
            dtLogNo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】問題共通情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>問題共通情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InserProblemInfoL(ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKD0201.SetInsertProblemInfoLSql(Cmd, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "問題共通情報ログ新規登録", Nothing, Cmd)

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
    ''' 【共通】問題作業履歴ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>問題作業履歴ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InserProblemWkRirekiL(ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKD0201.SetInsertProblemWkRirekiLSql(Cmd, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "問題作業履歴ログ新規登録", Nothing, Cmd)

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
    ''' 【共通】問題作業担当ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>問題作業担当ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertProblemWkTantoL(ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKD0201.SetInsertProblemWkTantoLSql(Cmd, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "問題作業担当ログ新規登録", Nothing, Cmd)

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
    ''' 【共通】問題対応関係ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>問題対応関係情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertProblemKankeiL(ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKD0201.SetInsertProblemKankeiLSql(Cmd, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "問題対応関係ログ新規登録", Nothing, Cmd)

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
    ''' 【共通】問題プロセスリンク情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンク情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertPLinkMotoL(ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKD0201.SetInsertPLinkMotoLSql(Cmd, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "問題プロセスリンク(元)情報ログ新規登録", Nothing, Cmd)

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
    ''' 【共通】問題CYSPR情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>問題CYSPR情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/23 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertProblemCysprL(ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKD0201.SetInsertProblemCysprLSql(Cmd, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "問題CYSPR情報ログ新規登録", Nothing, Cmd)

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
    ''' 【共通】問題関連ファイル情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>問題関連ファイル情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertProblemFileL(ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKD0201.SetInsertProblemFileLSql(Cmd, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "問題関連ファイル情報ログ新規登録", Nothing, Cmd)

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
    ''' 【共通】新規ログNo（会議用）取得
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番したログNoを取得する
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetNewMeetingRirekiNo(ByVal Cn As NpgsqlConnection, _
                                           ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter    'アダプタ
        Dim dLogNo As New DataTable             'ログNo格納用テーブル

        Try

            'SQLを作成
            If sqlHBKD0201.SetSelectNewMeetingRirekiNoSql(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規ログNo（会議用）取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dLogNo)

            'データが取得できた場合、データクラスにログNoをセット
            If dLogNo.Rows.Count > 0 Then
                dataHBKD0201.PropIntLogNoSub = dLogNo.Rows(0).Item("LogNo")
            Else
                '***************************************
                'メッセージを変更する
                '取得できなかった場合はエラー
                puErrMsg = D0201_E022
                '***************************************
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
            dLogNo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】会議情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InserMeetingL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKD0201.SetInsertMeetingLSql(Cmd, Cn, dataHBKD0201) = False Then
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
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議結果情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertMtgResultL(ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKD0201.SetInsertMtgResultLSql(Cmd, Cn, dataHBKD0201) = False Then
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
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議結果情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertMtgAttendL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKD0201.SetInsertMtgAttendLSql(Cmd, Cn, dataHBKD0201) = False Then
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
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議結果情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertMtgFileL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKD0201.SetInsertMtgFileLSql(Cmd, Cn, dataHBKD0201) = False Then
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
    ''' 【編集モード】データ更新処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をDBに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateData(ByVal dataHBKD0201 As DataHBKD0201) As Boolean

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

            'システム日付取得（SELECT）
            If SelectSysDate(Adapter, Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            '問題共通情報更新（UPDATE）
            If UpdateProblemInfo(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            '担当履歴情報チェック
            If InsertTantoRireki(Cn, dataHBKD0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If


            ''問題作業履歴 削除（DELETE）
            'If DeleteProblemWkRireki(Cn, dataHBKD0201) = False Then
            '    'ロールバック
            '    Tsx.Rollback()
            '    Return False
            'End If
            '問題作業履歴 新規登録（INSERT）
            If InsertProblemWkRireki(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            ''問題作業担当履歴 削除（DELETE）
            'If DeleteProblemWkTanto(Cn, dataHBKD0201) = False Then
            '    'ロールバック
            '    Tsx.Rollback()
            '    Return False
            'End If
            ''問題作業担当履歴 新規登録（INSERT）
            'If InsertProblemWkTanto(Cn, dataHBKD0201) = False Then
            '    'ロールバック
            '    Tsx.Rollback()
            '    Return False
            'End If

            '問題対応関係情報 削除（DELETE）
            If DeleteProblemKankei(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If
            '問題対応関係情報新規登録（INSERT）
            If InsertProblemKankei(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            'プロセスリンク新規登録（DELETE/INSERT）
            If InsertPrbPLink(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            '問題CYSPR情報削除（DELETE）
            If DeleteProblemCyspr(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If
            '問題CYSPR情報新規登録（INSERT）
            If InsertProblemCyspr(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            '関連ファイル情報登録（DELETE/INSERT）
            If InsertPrbFile(Cn, dataHBKD0201) = False Then
                Return False
            End If

            ''会議情報 削除（DELETE）
            'If DeleteMeetingResult(Cn, dataHBKD0201) = False Then
            '    'ロールバック
            '    Tsx.Rollback()
            '    Return False
            'End If
            '会議情報新規登録(INSERT)
            If InsertMtgResult(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            '新規ログNo取得
            If GetNewRirekiNo(Cn, dataHBKD0201) = False Then
                Return False
            End If

            '問題共通ログテーブル登録
            If InserProblemInfoL(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            '問題作業履歴ログテーブル登録
            If InserProblemWkRirekiL(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            '問題作業担当ログテーブル登録
            If InsertProblemWkTantoL(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            '問題対応関係情報ログテーブル登録
            If InsertProblemKankeiL(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            '問題プロセスリンク(元)ログテーブル登録
            If InsertPLinkMotoL(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            '問題CYSPR情報ログ登録
            If InsertProblemCysprL(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            '問題関連ファイルログテーブル登録
            If InsertProblemFileL(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            For i As Integer = 0 To dataHBKD0201.PropVwMeeting.Sheets(0).Rows.Count - 1
                '会議番号
                dataHBKD0201.PropIntMeetingNmb = dataHBKD0201.PropVwMeeting.Sheets(0).GetText(i, COL_MEETING_NMB)

                '新規ログNo(会議用)取得
                If GetNewMeetingRirekiNo(Cn, dataHBKD0201) = False Then
                    'ロールバック
                    Tsx.Rollback()
                    Return False
                End If

                '会議情報ログテーブル登録
                If InserMeetingL(Cn, dataHBKD0201) = False Then
                    'ロールバック
                    Tsx.Rollback()
                    Return False
                End If

                '会議結果ログテーブル登録
                If InsertMtgResultL(Cn, dataHBKD0201) = False Then
                    'ロールバック
                    Tsx.Rollback()
                    Return False
                End If

                '会議出席者ログテーブル登録
                If InsertMtgAttendL(Cn, dataHBKD0201) = False Then
                    'ロールバック
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    Return False
                End If

                '会議関連ファイルログテーブル登録
                If InsertMtgFileL(Cn, dataHBKD0201) = False Then
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
    ''' 【編集／作業履歴モード】サーバー日付取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>更新用のサーバー日付を取得する
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectSysDate(ByRef Adapter As NpgsqlDataAdapter, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSysDate As New DataTable         '履歴番号格納用テーブル

        Try
            '*************************************
            '* サーバー日付取得
            '*************************************

            'SQLを作成
            If sqlHBKD0201.SetSelectSysDateSql(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サーバー日付取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtSysDate)

            'データが取得できた場合、データクラスに履歴番号をセット
            If dtSysDate.Rows.Count > 0 Then
                dataHBKD0201.PropDtmSysDate = dtSysDate.Rows(0).Item("SysDate")
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
    ''' 【編集モード】問題共通情報 更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容で問題共通情報テーブルを更新（UPDATE）する
    ''' <para>作成情報：2012/08/22 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateProblemInfo(ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            '問題共通情報更新（UPDATE）用SQLを作成
            If sqlHBKD0201.SetUpdateProblemInfoSql(Cmd, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "問題共通情報更新", Nothing, Cmd)

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

    ' ''' <summary>
    ' ''' 【編集／作業履歴モード】問題作業履歴 削除処理
    ' ''' </summary>
    ' ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ' ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ' ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ' ''' <remarks>入力内容で問題作業履歴テーブルを削除（delete）する
    ' ''' <para>作成情報：2012/08/23 s.yamaguchi
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Function DeleteProblemWkRireki(ByVal Cn As NpgsqlConnection, _
    '                                       ByVal dataHBKD0201 As DataHBKD0201) As Boolean

    '    '開始ログ出力
    '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

    '    '変数宣言
    '    Dim Cmd As New NpgsqlCommand            'SQLコマンド

    '    Try

    '        '問題共通情報更新（UPDATE）用SQLを作成
    '        If sqlHBKD0201.SetDeleteProblemWkRirekiSql(Cmd, Cn, dataHBKD0201) = False Then
    '            Return False
    '        End If

    '        'ログ出力
    '        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "問題作業履歴物理削除", Nothing, Cmd)

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

    ''' <summary>
    ''' 【編集／作業履歴モード】問題作業担当　削除処理  
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容で問題作業担当テーブルを削除（delete）する
    ''' <para>作成情報：2012/08/23 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DeleteProblemWkTanto(ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            '問題共通情報更新（Delete）用SQLを作成
            If sqlHBKD0201.SetDeleteProblemWkTantoSql(Cmd, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "問題作業担当物理削除", Nothing, Cmd)

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
    ''' 【編集モード】問題対応関係情報　削除処理  
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容で問題対応関係者情報テーブルを削除（delete）する
    ''' <para>作成情報：2012/08/23 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DeleteProblemKankei(ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            '問題共通情報更新（DELETE）用SQLを作成
            If sqlHBKD0201.SetDeleteProblemKankeiSql(Cmd, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "問題対応関係情報物理削除", Nothing, Cmd)

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
    ''' 【編集モード】問題CYSPR情報　削除処理  
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容で問題CYSPR情報テーブルを削除（delete）する
    ''' <para>作成情報：2012/08/23 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DeleteProblemCyspr(ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            '問題共通情報更新（DELETE）用SQLを作成
            If sqlHBKD0201.SetDeleteProblemCysprSql(Cmd, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "問題CYSPR情報物理削除", Nothing, Cmd)

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
    ''' 【編集モード】ロック解除処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータのロックを解除する
    ''' <para>作成情報：2012/08/23 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UnlockData(ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '問題共通情報ロック解除
            If UnlockPrbInfo(dataHBKD0201.PropIntPrbNmb) = False Then
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
    ''' 【作業予実モード】データ更新処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をDB登録する
    ''' <para>作成情報：2012/08/23 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateData_Yojitsu(ByVal dataHBKD0201 As DataHBKD0201) As Boolean

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

            'システム日付取得（SELECT）
            If SelectSysDate(Adapter, Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            ''問題作業履歴 削除（DELETE）
            'If DeleteProblemWkRireki(Cn, dataHBKD0201) = False Then
            '    'ロールバック
            '    Tsx.Rollback()
            '    Return False
            'End If
            '問題作業履歴 新規登録（INSERT）
            If InsertProblemWkRireki(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            ''問題作業担当履歴 削除（DELETE）
            'If DeleteProblemWkTanto(Cn, dataHBKD0201) = False Then
            '    'ロールバック
            '    Tsx.Rollback()
            '    Return False
            'End If
            ''問題作業担当履歴 新規登録（INSERT）
            'If InsertProblemWkTanto(Cn, dataHBKD0201) = False Then
            '    'ロールバック
            '    Tsx.Rollback()
            '    Return False
            'End If

            'ログ情報新規登録（作業予実）
            If InsertRireki_Yojitsu(Tsx, Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
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
            Tsx.Dispose()
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【作業履歴モード】ログ情報新規登録処理
    ''' </summary>
    ''' <param name="Tsx">[IN/OUT]NpgsqlTransaction</param>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴情報を各ログテーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/23 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRireki_Yojitsu(ByRef Tsx As NpgsqlTransaction, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter    'アダプタ
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            '新規ログNo取得
            If GetNewRirekiNo(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            'INC共通ログテーブル登録
            If InserProblemInfoL(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            '作業履歴ログテーブル登録
            If InserProblemWkRirekiL(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            '作業担当ログテーブル登録
            If InsertProblemWkTantoL(Cn, dataHBKD0201) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

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
    ''' 画面クローズ時ロック解除メイン処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータのロックを解除する
    ''' <para>作成情報：2012/08/27 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function UnlockWhenCloseMain(ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロック解除処理
        If UnlockData(dataHBKd0201) = False Then
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
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータをログインユーザでロックし、フォームのロックを解除する
    ''' <para>作成情報：2012/08/27 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function UnlockWhenClickBtnUnlockMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'モード変更
        dataHBKD0201.PropStrProcMode = PROCMODE_EDIT

        'ロックフラグOFF
        dataHBKD0201.PropBlnBeLockedFlg = False

        'ロック処理
        If SetLockWhenUnlock(dataHBKD0201) = False Then
            Return False
        End If

        'フォームコントロールのロックを解除する
        If SetFormControlPerProcMode(dataHBKD0201) = False Then
            Return False
        End If

        'ログイン／ロックデータ設定
        If SetDataToLoginAndLock(dataHBKD0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【編集モード】解除ボタンクリック時ロック設定処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/08/27 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLockWhenUnlock(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '問題共通情報テーブルロック解除
            If UnlockPrbInfo(dataHBKD0201.PropIntPrbNmb) = False Then
                Return False
            End If

            '問題共通情報テーブルロック
            If LockPrbInfo(dataHBKD0201.PropIntPrbNmb, dataHBKD0201.PropDtPrbInfoLock, False) = False Then
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
    ''' 問題ロック処理
    ''' </summary>
    ''' <param name="intPrbNmb">[IN]問題番号</param>
    ''' <param name="dtPrbLock">[IN/OUT]問題共通情報ロックテーブルデータ格納テーブル</param>
    ''' <param name="blnDoUnlock">[IN]解除実行フラグ（True：解除してからロックする）※省略可</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>問題番号をキーに問題共通情報ロックテーブルのデータをINSERTする
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function LockPrbInfo(ByVal intPrbNmb As Integer, _
                                ByRef dtPrbLock As DataTable, _
                                Optional ByVal blnDoUnlock As Boolean = False) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter()    'アダプタ
        Dim Tsx As NpgsqlTransaction = Nothing    'トランザクション

        Try

            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            'ロック解除実行フラグがONの場合、問題共通情報ロックテーブルデータを削除
            If blnDoUnlock = True Then
                If DeletePrbLock(Cn, Tsx, intPrbNmb) = False Then
                    Return False
                End If
            End If

            '問題共通情報ロックテーブル登録
            If InsertPrbLock(Cn, Tsx, intPrbNmb) = False Then
                Return False
            End If

            'データ格納用テーブル初期化
            dtPrbLock = New DataTable

            '問題共通情報ロックテーブル取得
            If sqlHBKD0201.SelectPrbLock(Adapter, Cn, intPrbNmb) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "問題共通情報ロックテーブル取得", Nothing, Adapter.SelectCommand)

            'SQLを実行し、結果を取得
            Adapter.Fill(dtPrbLock)

            'コミット
            Tsx.Commit()

            'コネクションを閉じる
            Cn.Close()

            '2行取得できた場合（ロック情報がある場合）、ロック情報にサーバー日付を不可
            If dtPrbLock.Rows.Count > 1 Then
                'ロック情報にサーバー日付を設定
                dtPrbLock.Rows(1).Item("SysTime") = dtPrbLock.Rows(0).Item("SysTime")
                '1行目のデータを削除
                dtPrbLock.Rows(0).Delete()
                '変更をコミット
                dtPrbLock.AcceptChanges()
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'ロールバック
            If Tsx IsNot Nothing Then
                Tsx.Rollback()
            End If
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Adapter.Dispose()
            If Tsx IsNot Nothing Then
                Tsx.Dispose()
            End If
            Cn.Dispose()

        End Try

    End Function

    ''' <summary>
    ''' 問題ロック解除処理
    ''' </summary>
    ''' <param name="intPrbNmb">[IN]問題番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>問題共通情報のロックを解除する
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function UnlockPrbInfo(ByVal intPrbNmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Tsx As NpgsqlTransaction = Nothing    'トランザクション

        Try

            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            '問題共通情報ロックテーブル削除処理
            If DeletePrbLock(Cn, Tsx, intPrbNmb) = False Then
                Return False
            End If

            'コミット
            Tsx.Commit()

            'コネクションを閉じる
            Cn.Close()

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'ロールバック
            If Tsx IsNot Nothing Then
                Tsx.Rollback()
            End If
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            If Tsx IsNot Nothing Then
                Tsx.Dispose()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 問題共通情報ロックテーブル削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="Tsx">[IN]NpgsqlTransactionクラス</param>
    ''' <param name="intPrbNmb">[IN]問題番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>問題番号をキーに問題共通情報ロックテーブルのデータを物理削除（DELETE）する
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function DeletePrbLock(ByVal Cn As NpgsqlConnection, _
                                  ByVal Tsx As NpgsqlTransaction, _
                                  ByVal intPrbNmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'SQL変数宣言--------------------------------------



        Try
            '問題共通情報ロックテーブル削除処理
            Dim Cmd As New NpgsqlCommand            'SQLコマンド

            'DeletePrbLockSql
            If sqlHBKD0201.DeletePrbLockSql(Cmd, Cn, intPrbNmb) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "問題共通情報ロックテーブル削除", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            Tsx.Rollback()
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 問題共通情報ロックテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="Tsx">[IN]NpgsqlTransactionクラス</param>
    ''' <param name="intPrbNmb">[IN]問題番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>問題共通情報ロックテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function InsertPrbLock(ByVal Cn As NpgsqlConnection, _
                                   ByRef Tsx As NpgsqlTransaction, _
                                   ByVal intPrbNmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'SQL変数宣言--------------------------------------


        '問題共通情報ロック用変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            '問題共通情報ロックテーブル登録
            If sqlHBKD0201.InsertPrbLockSql(Cmd, Cn, intPrbNmb) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "問題共通情報ロックテーブル登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Cmd)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            Tsx.Rollback()
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 問題ロック状況チェック処理
    ''' </summary>
    ''' <param name="intPrbNmb">[IN]問題番号</param>
    ''' <param name="blnBeLocked">[IN/OUT]ロックフラグ（ロック時：True）</param>
    ''' <param name="strBeLockedMsg">ロック時メッセージ</param>
    ''' <param name="dtPrbLock">問題共通情報ロックテーブル</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>指定された問題番号の問題共通情報がロックされているかチェックする。
    ''' また、ロックされている場合はエラーメッセージも返す
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function CheckPrbDataBeLocked(ByVal intPrbNmb As Integer, _
                                         ByRef blnBeLocked As Boolean, _
                                         ByRef strBeLockedMsg As String, _
                                         ByRef dtPrbLock As DataTable) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '問題共通情報ロックテーブル項目格納用変数宣言
        Dim dtResult As New DataTable           'SELECT結果格納テーブル

        'ロックチェック用変数宣言
        Dim strEdiID As String = ""             '編集者ID
        Dim strEdiTime As String = ""           '編集開始日時
        Dim strSysTime As String                '現在日時（サーバー）
        Dim tsDiff As TimeSpan = Nothing        '編集開始日時と現在日時の差
        Dim tsUnlock As TimeSpan = Nothing      'ロック解除時間  

        Try
            'ロックフラグ、問題共通情報ロックデータ数初期化
            blnBeLocked = False

            '問題共通情報ロックテーブル取得
            If GetPrbLockTb(intPrbNmb, dtResult) = False Then
                Return False
            End If

            '現在日時を取得
            strSysTime = dtResult.Rows(0).Item("SysTime").ToString()

            '問題共通情報ロックデータが取得できた場合、チェックを行う
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
            dtPrbLock = dtResult

            'ログ出力
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
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 問題ロック解除状況チェック処理
    ''' </summary>
    ''' <param name="intPrbNmb">[IN]問題番号</param>
    ''' <param name="strEdiTime_Bef">[IN]既に設定済の編集開始日時</param>
    ''' <param name="blnBeUnocked">[IN/OUT]ロック解除フラグ（True：ロック解除されている）</param>
    ''' <param name="dtPrbLock">[IN/OUT]問題共通情報ロックデータ</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>指定された問題番号の問題共通情報のロック解除状況をチェックする。
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function CheckPrbDataBeUnlocked(ByVal intPrbNmb As Integer, _
                                           ByVal strEdiTime_Bef As String, _
                                           ByRef blnBeUnocked As Boolean, _
                                           ByRef dtPrbLock As DataTable) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '問題共通情報ロックテーブル項目格納用変数宣言
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
            '* 問題共通情報ロックテーブル取得
            '********************************
            If GetPrbLockTb(intPrbNmb, dtResult) = False Then
                Return False
            End If

            '********************************
            '* ロック解除チェック
            '********************************

            '現在日時を取得
            strSysTime = dtResult.Rows(0).Item("SysTime").ToString()

            '問題共通情報ロックデータが取得できた場合、チェックを行う
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
                '問題共通情報ロックデータが取得できなかった場合

                'ロック解除フラグON
                blnBeUnocked = True

            End If

            '取得データを戻り値にセット
            dtPrbLock = dtResult

            'ログ出力
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
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 問題共通情報ロック情報取得処理
    ''' </summary>
    ''' <param name="intPrbNmb">[IN]問題番号</param>
    ''' <param name="dtPrbLock">[IN/OUT]問題共通情報ロックデータ</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>指定された問題番号の問題共通情報ロックテーブルおよびサーバー日付を返す
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function GetPrbLockTb(ByVal intPrbNmb As Integer, _
                                 ByRef dtPrbLock As DataTable) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'DB接続用変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        'SQL変数宣言--------------------------------------

        Try
            'データ格納用テーブル初期化
            dtPrbLock = New DataTable

            'コネクションを開く
            Cn.Open()

            '問題共通情報ロックテーブル、サーバー日付取得
            If sqlHBKD0201.SelectPrbLock(Adapter, Cn, intPrbNmb) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "問題共通情報ロックテーブル取得", Nothing, Adapter.SelectCommand)

            'SQLを実行し、結果を取得
            Adapter.Fill(dtPrbLock)

            'コネクションを閉じる
            Cn.Close()

            '2行取得できた場合（ロック情報がある場合）、ロック情報にサーバー日付を不可
            If dtPrbLock.Rows.Count > 1 Then
                'ロック情報にサーバー日付を設定
                dtPrbLock.Rows(1).Item("SysTime") = dtPrbLock.Rows(0).Item("SysTime")
                '1行目のデータを削除
                dtPrbLock.Rows(0).Delete()
                '変更をコミット
                dtPrbLock.AcceptChanges()
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            dtPrbLock.Dispose()
            Adapter.Dispose()
            Cn.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【変更登録ボタン】プロセスリンク再取得メイン処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンクデータの再取得を行う。
    ''' <para>作成情報：2012/09/07 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RefreshPLinkMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            'プロセスリンク情報データ取得(PropDtResultMeeting)
            If GetProcessLinkRef(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            With dataHBKD0201
                'データテーブルを取得
                .PropDtProcessLink = DirectCast(.PropVwProcessLinkInfo.Sheets(0).DataSource, DataTable)

                '退避用データテーブル作成
                Dim dtAdd As DataTable = .PropDtProcessLink.Clone
                Dim dtDel As DataTable = .PropDtProcessLink.Clone
                If .PropDtProcessLink IsNot Nothing AndAlso .PropDtProcessLink.Rows.Count > 0 Then
                    '追加された情報で未登録のものを取得 
                    For i As Integer = 0 To .PropDtProcessLink.Rows.Count - 1
                        'Addされたデータのみ取得
                        Select Case .PropDtProcessLink.Rows(i).RowState
                            Case DataRowState.Added '画面で追加されたデータ
                                dtAdd.Rows.Add(.PropDtProcessLink.Rows(i).Item("processkbnnmr"), _
                                               .PropDtProcessLink.Rows(i).Item("mngnmb"), _
                                               .PropDtProcessLink.Rows(i).Item("processkbn"))

                            Case DataRowState.Deleted '画面で削除されたデータ
                                dtDel.Rows.Add(.PropDtProcessLink.Rows(i).Item("mngnmb", DataRowVersion.Original), _
                                               .PropDtProcessLink.Rows(i).Item("processkbn", DataRowVersion.Original))

                        End Select
                    Next
                End If

                'プロセスリンクスプレッド再取得データを設定
                .PropDtProcessLink = .PropDtResultMeeting.Copy
                .PropDtProcessLink.AcceptChanges()
                .PropVwProcessLinkInfo.DataSource = .PropDtProcessLink


                '画面上で追加且つＤＢ未更新のデータを反映
                If dtAdd.Rows.Count > 0 Then
                    For i As Integer = 0 To dtAdd.Rows.Count - 1
                        .PropDtProcessLink.Rows.Add(dtAdd.Rows(i).Item("processkbnnmr"), _
                                                  dtAdd.Rows(i).Item("mngnmb"), _
                                                  dtAdd.Rows(i).Item("processkbn"))
                    Next
                End If

                '画面上で削除且つＤＢ未更新のデータを反映
                If dtDel.Rows.Count > 0 Then
                    For i As Integer = 0 To dtDel.Rows.Count - 1
                        For j As Integer = 0 To .PropDtProcessLink.Rows.Count - 1
                            Select Case .PropDtProcessLink.Rows(j).RowState
                                Case DataRowState.Deleted
                                    If .PropDtProcessLink.Rows(j).Item("mngnmb", DataRowVersion.Original).ToString.Equals(dtDel.Rows(i).Item("mngnmb").ToString) AndAlso _
                                        .PropDtProcessLink.Rows(j).Item("processkbn", DataRowVersion.Original).ToString.Equals(dtDel.Rows(i).Item("processkbn").ToString) Then
                                        .PropDtProcessLink.Rows(j).Delete()
                                    End If
                                Case Else
                                    If .PropDtProcessLink.Rows(j).Item("mngnmb").ToString.Equals(dtDel.Rows(i).Item("mngnmb").ToString) AndAlso _
                                        .PropDtProcessLink.Rows(j).Item("processkbn").ToString.Equals(dtDel.Rows(i).Item("processkbn").ToString) Then
                                        .PropDtProcessLink.Rows(j).Delete()
                                    End If
                            End Select
                        Next
                    Next
                End If

            End With


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
    ''' 【編集／参照／作業履歴モード】担当履歴情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>担当履歴情報データを取得する
    ''' <para>作成情報：2012/09/10 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetTantoRireki(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtINCInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKD0201.SetSelectTantoRirekiSql(Adapter, Cn, DataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "担当履歴データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtINCInfo)

            '取得データをデータクラスにセット
            DataHBKD0201.PropDtTantoRireki = dtINCInfo


            '終了ログ出力
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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴データを作成する
    ''' <para>作成情報：2012/09/10 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateTantoRireki(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '初期化
            Dim strTantoRirekiSplit As String = "←"
            dataHBKD0201.PropTxtGrpRireki.Text = ""
            dataHBKD0201.PropTxtTantoRireki.Text = ""

            '担当履歴
            With dataHBKD0201.PropDtTantoRireki
                If .Rows.Count > 0 Then
                    For i As Integer = 0 To .Rows.Count - 1
                        If i = 0 Then
                            dataHBKD0201.PropTxtGrpRireki.Text &= .Rows(i).Item("tantogrpnm")
                            dataHBKD0201.PropTxtTantoRireki.Text &= .Rows(i).Item("prbtantonm")
                        Else
                            'ＧＰ
                            If Not .Rows(i - 1).Item("tantogrpnm").Equals(.Rows(i).Item("tantogrpnm")) Then
                                dataHBKD0201.PropTxtGrpRireki.Text &= strTantoRirekiSplit & .Rows(i).Item("tantogrpnm")
                            End If
                            'ＩＤ
                            If Not .Rows(i - 1).Item("prbtantonm").Equals(.Rows(i).Item("prbtantonm")) Then
                                dataHBKD0201.PropTxtTantoRireki.Text &= strTantoRirekiSplit & .Rows(i).Item("prbtantonm")
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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>担当履歴判定チェックをする
    ''' <para>作成情報：2012/09/10 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertTantoRireki(ByVal Cn As NpgsqlConnection, ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド
        Dim bln_chk_flg As Boolean = False

        Try
            '担当履歴、担当グループチェック処理
            'PropDtTantoRirekiは履歴を降順にしているのでROWは0を設定する

            '最終更新GPを取得 (tantorirekinmb Max)
            With dataHBKD0201

                If .PropDtTantoRireki IsNot Nothing AndAlso .PropDtTantoRireki.Rows.Count > 0 Then
                    '現更新者と同じかチェック
                    If .PropDtTantoRireki.Rows(0).Item("tantogrpnm").ToString.Equals(.PropCmbTantoGrp.Text) = False Then
                        bln_chk_flg = True
                    End If
                Else
                    'まだ登録がない
                    If .PropCmbTantoGrp.Text.Length > 0 Then
                        bln_chk_flg = True
                    End If
                End If


                If .PropDtTantoRireki IsNot Nothing AndAlso .PropDtTantoRireki.Rows.Count > 0 Then
                    '現更新者と同じかチェック
                    If .PropDtTantoRireki.Rows(0).Item("prbtantonm").ToString.Equals(.PropTxtPrbTantoNM.Text) = False Then
                        bln_chk_flg = True
                    End If
                Else
                    'まだ登録がない
                    If .PropTxtPrbTantoNM.Text.Length > 0 Then
                        bln_chk_flg = True
                    End If
                End If

            End With

            '変更があった場合は登録する。
            If bln_chk_flg = True Then
                '担当履歴報新規登録（INSERT）用SQLを作成
                If sqlHBKD0201.SetInsertTantoRirekiSql(Cmd, Cn, dataHBKD0201) = False Then
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
    ''' <param name="dataHBKD0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ファイル管理テーブルからファイルパスを取得し、ファイルを開く
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function FileOpenMain(ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ファイルパス取得処理
        If GetOpenFilePath(dataHBKD0201) = False Then
            Return False
        End If

        'ファイル表示処理
        If FileLoad(dataHBKD0201) = False Then
            Return False
        End If

        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        Return True

    End Function

    ''' <summary>
    ''' 【共通】ダウンロードボタン押下時の処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ファイル管理テーブルからファイルパスを取得し、ファイルをダウンロードする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function FileDownLoadMain(ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ファイルパス取得処理
        If GetOpenFilePath(dataHBKD0201) = False Then
            Return False
        End If

        'ファイルダウンロード処理
        If FileDownLoad(dataHBKD0201) = False Then
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
    ''' <param name="dataHBKD0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択中の会議ファイルパスを習得する
    ''' <para>作成情報：2012/08/13 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetOpenFilePath(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0201

                '選択行のファイルパスを取得し、データクラスにセット
                .PropStrSelectedFilePath = .PropVwPrbFileInfo.Sheets(0).Cells(.PropIntSelectedRow, COL_PRBFILE_FILEPATH).Value

            End With


            '終了ログ出力
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
    ''' <param name="dataHBKD0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ファイル管理テーブルからファイルパスを取得し、ファイルを開く
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function FileLoad(ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strFilePath As String
        Dim intFileMngNmb As Integer

        Dim strCmd As String = ""                                   'コマンド文字列
        Dim strDriveName As String = ""                             '使用論理ドライブ名

        Try

            With dataHBKD0201

                '選択行のファイルパス、ファイル管理番号取得
                strFilePath = dataHBKD0201.PropStrSelectedFilePath
                intFileMngNmb = .PropVwPrbFileInfo.Sheets(0).Cells(.PropIntSelectedRow, COL_PRBFILE_MNGNMB).Value

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
            puErrMsg = HBK_E001 & D0201_E027
            Return False
        Catch ex As System.IO.FileNotFoundException
            'ファイルが見つからなかった場合
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & D0201_E027
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
    ''' <param name="dataHBKD0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ファイル管理テーブルからファイルパスを取得し、ファイルをダウンロードする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function FileDownLoad(ByVal dataHBKD0201 As DataHBKD0201) As Boolean

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
            With dataHBKD0201

                '選択行のファイルパスを取得
                strFilePath = dataHBKD0201.PropStrSelectedFilePath

                'ファイルダウンロード処理
                sfd.FileName = Path.GetFileName(strFilePath)
                sfd.InitialDirectory = ""
                sfd.Filter = "すべてのファイル(*.*)|*.*"
                sfd.FilterIndex = 1
                sfd.Title = "保存先を指定してください"


                '選択行のファイルパス、ファイル管理番号取得
                strFilePath = dataHBKD0201.PropStrSelectedFilePath
                intFileMngNmb = .PropVwPrbFileInfo.Sheets(0).Cells(.PropIntSelectedRow, COL_PRBFILE_MNGNMB).Value

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
            puErrMsg = HBK_E001 & D0201_E027
            Return False
        Catch ex As System.IO.FileNotFoundException
            'ファイルが見つからなかった場合
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & D0201_E027
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
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議結果情報データの再取得を行う。
    ''' <para>作成情報：2012/09/11 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RefreshMeetingMain(ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try

            '会議結果情報データ取得(PropDtResultMeeting)
            If GetMeetingResult(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            With dataHBKD0201
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
                .PropDtMeeting = .PropDtResultMeeting.Copy
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
    ''' <param name="DataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報データを取得する
    ''' <para>作成情報：2012/08/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMeetingResult(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtINCInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKD0201.SetSelectMeetingSql(Adapter, Cn, DataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議結果情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtINCInfo)

            '取得データをデータクラスにセット
            DataHBKD0201.PropDtResultMeeting = dtINCInfo


            '終了ログ出力
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
    ''' 【モード】プロセスリンク情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKD0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンク情報データを取得する
    ''' <para>作成情報：2012/08/16 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetProcessLinkRef(ByVal Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtTableData As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKD0201.SetSelectProcessLinkData(Adapter, Cn, dataHBKD0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "プロセスリンク情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtTableData)

            '取得データをデータクラスにセット
            dataHBKD0201.PropDtResultMeeting = dtTableData

            '終了ログ出力
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
            dtTableData.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' フォーカス移動時桁数チェック処理
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN]インシデント登録Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーカス移動時桁数チェックをする
    ''' <para>作成情報：2012/10/23 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckLostFocus(ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言

        Try

            'ロストフォーカス時3000文字以上の場合メッセージの表示
            If dataHBKD0201.PropStrLostFucs <> Nothing Then
                If dataHBKD0201.PropStrLostFucs.ToString.Length > 3000 Then
                    'エラーメッセージ設定
                    puErrMsg = D0201_W003
                    'エラーを返す
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
    ''' フォーカス移動時桁数チェック処理(スプレッドExcelコピー時)
    ''' </summary>
    ''' <param name="dataHBKD0201">[IN]インシデント登録Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーカス移動時桁数チェックをする
    ''' <para>作成情報：2012/10/23 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckLostFocusSpread(ByVal dataHBKD0201 As DataHBKD0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim flg As Boolean = True
        Dim strCount As String = ""
        Try

            With dataHBKD0201.PropVwPrbYojitsu.Sheets(0)

                For index = 0 To .RowCount - 1
                    'ロストフォーカス時3000文字以上の場合メッセージの表示
                    If .Cells(index, COL_YOJITSU_WORKNAIYO).Value <> Nothing Then
                        If .Cells(index, COL_YOJITSU_WORKNAIYO).Value.ToString.Length > 3000 Then
                            '[DELETE]2013/03/21 t.fukuo 閾値超過の場合文字削除しないよう修正 START
                            '.Cells(index, COL_YOJITSU_WORKNAIYO).Value = .Cells(index, COL_YOJITSU_WORKNAIYO).Value.ToString.Substring(0, 3000)
                            '[DELETE]2013/03/21 t.fukuo 閾値超過の場合文字削除しないよう修正 END
                            '
                            If strCount = "" Then
                                strCount = (index + 1).ToString
                            Else
                                strCount = strCount & "," & (index + 1).ToString
                            End If

                            flg = False
                        End If
                    End If
                Next

            End With

            If flg = False Then
                puErrMsg = String.Format(C0201_W004 & vbCrLf & "(行：{0})", strCount)
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
        End Try

    End Function

End Class