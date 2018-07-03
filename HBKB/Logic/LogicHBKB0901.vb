Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.IO
Imports System.Windows.Forms

''' <summary>
''' 導入画面ロジッククラス
''' </summary>
''' <remarks>導入画面のロジックを定義したクラス
''' <para>作成情報：2012/07/14 h.sasaki
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKB0901

        'インスタンス作成
    Private sqlHBKB0901 As New SqlHBKB0901
    Private commonLogic As New CommonLogic
    Private commonValidation As New CommonValidation
    Private commonLogicHBK As New CommonLogicHBK

    'Public定数宣言==============================================

    'Private定数宣言==============================================

    ''' <summary>
    ''' 【共通】システムエラー事前対応処理
    ''' </summary>
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システムエラー発生時に非活性にするコントロールリストを作成する
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DoProcForErrorMain(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'トランザクション系コントロールリスト作成
        If CreateTsxCtlList(dataHBKB0901) = False Then
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
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて画面の初期表示設定を行う
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormNewModeMain(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'フォームコントロール設定
        If InitFormControl(dataHBKB0901) = False Then
            Return False
        End If

        '初期表示用データ取得
        If GetInitData(dataHBKB0901) = False Then
            Return False
        End If

        '初期表示用データセット
        If SetInitDataToControl(dataHBKB0901) = False Then
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
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>データのロック状況をチェックし、状況に応じてロックまたは処理モードの切替を行う
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function LockMain(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロック設定
        If SetLockWhenLoad(dataHBKB0901) = False Then
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
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで画面の初期表示設定を行う
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormEditModeMain(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'フォームコントロール設定
        If InitFormControl(dataHBKB0901) = False Then
            Return False
        End If

        '初期表示用データ取得
        If GetInitData(dataHBKB0901) = False Then
            Return False
        End If

        '初期表示用データセット
        If SetInitDataToControl(dataHBKB0901) = False Then
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
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで画面の初期表示設定を行う
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormRefModeMain(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'フォームコントロール設定
        If InitFormControl(dataHBKB0901) = False Then
            Return False
        End If

        '初期表示用データ取得
        If GetInitData(dataHBKB0901) = False Then
            Return False
        End If

        '初期表示用データセット
        If SetInitDataToControl(dataHBKB0901) = False Then
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
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータをログインユーザでロックし、フォームのロックを解除する
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function UnlockWhenClickBtnUnlockMain(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロックフラグOFF
        dataHBKB0901.PropBlnBeLockedFlg = False

        'ロック処理
        If SetLockWhenUnlock(dataHBKB0901) = False Then
            Return False
        End If

        'フォームコントロールのロックを解除する
        If SetFormControlPerProcMode(dataHBKB0901) = False Then
            Return False
        End If
        If SetDataToLoginAndLock(dataHBKB0901) = False Then
            Return False
        End If
        '解除ボタンクリック時購入・リース情報コントロール設定
        If RefChangeEditControl(dataHBKB0901) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】購入・リース情報解除ボタンクリック時処理
    ''' </summary>
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>解除ボタンクリック時、購入・リース情報のコントロールを設定する
    ''' <para>作成情報：2012/09/03 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RefChangeEditControl(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0901


                '導入タイプラジオボタン：チェックの付いている側を編集可能にする
                If .ProprdoIntroductKbn0.Checked = True Then

                    .PropdtpDelScheduleDT.Enabled = True

                ElseIf .ProprdoIntroductKbn1.Checked = True Then

                    .ProptxtLeaseCompany.ReadOnly = False
                    .ProptxtLeaseNmb.ReadOnly = False
                    .PropdtpLeaseUpDT.Enabled = True

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

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function


    ''' <summary>
    ''' 【共通】コントロール入力チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録項目の入力チェックを行う
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckInputValueMain(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'コントロール入力チェック
        If CheckInputValue(dataHBKB0901) = False Then
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
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>他のユーザによりロックが解除されていないかチェックする
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckBeUnlockedMain(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロック解除チェック
        If CheckUnlock(dataHBKB0901) = False Then
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
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面の内容をログに出力し、フォームコントロールを再設定する
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetFormRefModeFromEditModeMain(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '入力内容ログ出力
        If OutputUnlockLog(dataHBKB0901) = False Then
            Return False
        End If

        '参照モードでフォームコントロール設定
        If SetFormControlPerProcMode(dataHBKB0901) = False Then
            Return False
        End If

        '参照モードでロック情報設定
        If SetDataToLoginAndLockForRef(dataHBKB0901) = False Then
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
    ''' <param name="dataHBKB0901">[IN]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をデータベースに反映する
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RegistDataOnNewModeMain(ByVal dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '新規登録処理
        If InsertNewData(dataHBKB0901) = False Then
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
    ''' <param name="dataHBKB0901">[IN]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をデータベースに反映する
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RegistDataOnEditModeMain(ByVal dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '更新処理
        If UpdateData(dataHBKB0901) = False Then
            Return False
        End If

        'ロック解除処理
        If UnlockData(dataHBKB0901) = False Then
            Return False
        End If

        '画面再描画
        If InitFormEditModeMain(dataHBKB0901) = False Then
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
    ''' <param name="dataHBKB0901">[IN]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータのロックを解除する
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function UnlockWhenCloseMain(ByVal dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロック解除処理
        If UnlockData(dataHBKB0901) = False Then
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
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>トランザクション系コントロールのリストを作成する
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateTsxCtlList(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim aryCtlList As New ArrayList

        Try
            With dataHBKB0901

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
    ''' 【共通】初期表示用データ取得処理
    ''' </summary>
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            'マスタデータ取得
            If GetMastaData(Adapter, Cn, dataHBKB0901) = False Then
                Return False
            End If

            'メイン表示データ取得
            If GetMainData(Adapter, Cn, dataHBKB0901) = False Then
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
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLockWhenUnlock(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'ロック解除
            If UnlockIntroduct(dataHBKB0901.PropIntIntroductNmb) = False Then
                Return False
            End If

            'ロック設定
            If SetLockWhenLoad(dataHBKB0901) = False Then
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
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLockWhenLoad(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnBeLocked As Boolean = False                   'ロックフラグ  

        Try

            With dataHBKB0901

                'ロック解除チェック
                If CheckDataBeLocked(.PropIntIntroductNmb, blnBeLocked, .PropStrBeLockedMsg, .PropDtIntroductLock) = False Then
                    Return False
                End If

                'ロックされている（別のユーザが編集中）場合、 ロックフラグをON
                If blnBeLocked = True Then

                    dataHBKB0901.PropBlnBeLockedFlg = True

                Else

                    'ロックされていない場合、導入をロックする
                    If SetLock(dataHBKB0901) = False Then
                        Return False
                    End If

                    'ロックフラグをOFF
                    dataHBKB0901.PropBlnBeLockedFlg = False

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
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集開始日時によりロック設定を行う
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckAndSetLock(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnBeLocked As Boolean = False                   'ロックフラグ  

        Try

            With dataHBKB0901

                'ロック解除チェック
                If CheckDataBeLocked(.PropIntIntroductNmb, blnBeLocked, .PropStrBeLockedMsg, .PropDtIntroductLock) = False Then
                    Return False
                End If

                'ロックされている（別のユーザが編集中）場合、 ロックフラグをON
                If blnBeLocked = True Then

                    dataHBKB0901.PropBlnBeLockedFlg = True

                Else

                    'ロックされていない場合、CI共通情報をロックする
                    If SetLock(dataHBKB0901) = False Then
                        Return False
                    End If

                    'ロックフラグをOFF
                    dataHBKB0901.PropBlnBeLockedFlg = False

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
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>導入テーブルをロックする
    ''' <para>作成情報：2012/06/21 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLock(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnDoUnlock As Boolean = False                   'ロック解除実行フラグ

        Try

            With dataHBKB0901

                '導入ロックテーブルデータがある場合、ロック解除実行フラグON
                If .PropDtIntroductLock.Rows.Count > 0 Then
                    blnDoUnlock = True
                End If

                '導入ロック
                If LockIntroduct(.PropIntIntroductNmb, .PropDtIntroductLock, blnDoUnlock) = False Then
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
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InitFormControl(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '処理モードに応じたフォームコントロール設定
            If SetFormControlPerProcMode(dataHBKB0901) = False Then
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
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフォームコントロールの設定を行う
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormControlPerProcMode(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'ログイン／ロックコントロール設定
            If SetLoginAndLockControl(dataHBKB0901) = False Then
                Return False
            End If

            'フッタ設定
            If SetFooterControl(dataHBKB0901) = False Then
                Return False
            End If

            '基本情報設定
            If SetControlKhn(dataHBKB0901) = False Then
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
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControl(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0901

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetLoginAndLockControlForNew(dataHBKB0901) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード

                    If .PropBlnBeLockedFlg = False Then             '編集モード
                        '編集モード用設定
                        If SetLoginAndLockControlForEdit(dataHBKB0901) = False Then
                            Return False
                        End If
                    ElseIf .PropBlnBeLockedFlg = True Then          '参照（ロック）モード
                        '参照（ロック）モード用設定
                        If SetLoginAndLockControlForRef(dataHBKB0901) = False Then
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
        End Try

    End Function

    ''' <summary>
    ''' 【新規登録モード】ログイン／ロックコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForNew(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0901.PropGrpLoginUser

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
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForEdit(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0901.PropGrpLoginUser

                'グループコンボボックス非活性
                .cmbGroup.Enabled = False

                '変更ボタン非活性
                .btnChange.Enabled = False

                'ロック情報表示
                .PropLockInfoVisible = True

                '解除ボタン表示
                .PropBtnUnlockVisible = True

                'ロック情報が取得できた場合
                If dataHBKB0901.PropDtIntroductLock.Rows.Count > 0 Then

                    '編集者と同じグループの場合は解除ボタン活性化
                    If .cmbGroup.SelectedValue = dataHBKB0901.PropDtIntroductLock.Rows(0).Item("EdiGrpCD") And _
                       PropUserId <> dataHBKB0901.PropDtIntroductLock.Rows(0).Item("EdiID") Then
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
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForRef(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0901.PropGrpLoginUser

                'グループコンボボックス非活性
                .cmbGroup.Enabled = False

                '変更ボタン非活性
                .btnChange.Enabled = False

                'ロック情報表示
                .PropLockInfoVisible = True

                '解除ボタン表示
                .PropBtnUnlockVisible = True

                'ロック情報が取得できた場合
                If dataHBKB0901.PropDtIntroductLock.Rows.Count > 0 Then

                    '編集者と同じグループの場合は解除ボタン活性化
                    If .cmbGroup.SelectedValue = dataHBKB0901.PropDtIntroductLock.Rows(0).Item("EdiGrpCD") Then
                        .PropBtnUnlockEnabled = True
                    Else
                        .PropBtnUnlockEnabled = False
                    End If

                Else

                    '解除ボタン非活性
                    .PropBtnUnlockEnabled = False

                End If

                'ロック解除から遷移してきた場合は解除ボタンを非活性
                If dataHBKB0901.PropBlnLockCompare = True Then
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
    ''' 【共通】フッタコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControl(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0901

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    '処理なし

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード

                    If .PropBlnBeLockedFlg = False Then             '編集モード

                        '編集モード用設定
                        If SetFooterControlForEdit(dataHBKB0901) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then          '参照（ロック）モード

                        '参照モード用設定
                        If SetFooterControlForRef(dataHBKB0901) = False Then
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
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】フッタコントロール設定
    ''' </summary>
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForEdit(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0901

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
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForRef(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0901

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
    ''' 【共通】基本情報コントロール設定
    ''' </summary>
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetControlKhn(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0901

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetControlKhnForNew(dataHBKB0901) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード

                    If .PropBlnBeLockedFlg = False Then             '編集モード

                        '編集モード用設定
                        If SetControlKhnForEdit(dataHBKB0901) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then          '参照（ロック）モード

                        '参照（ロック）モード用設定
                        If SetControlKhnForRef(dataHBKB0901) = False Then
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
        End Try

    End Function

    ''' <summary>
    ''' 【新規登録モード】基本情報コントロール設定
    ''' </summary>
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードに応じて基本情報コントロールの初期設定を行う
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetControlKhnForNew(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0901

                'リース会社テキストボックス
                .ProptxtLeaseCompany.Enabled = False

                'リース番号テキストボックス
                .ProptxtLeaseNmb.Enabled = False

                '期限日テキストボックス
                .PropdtpLeaseUpDT.Enabled = False

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】基本情報コントロール設定
    ''' </summary>
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードに応じて基本情報コントロールの初期設定を行う
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : 2012/09/04(購入・リースグループボックス活性化) s.tsuruta</p>
    ''' </para></remarks>
    Private Function SetControlKhnForEdit(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0901

                '種別コンボボックス
                .PropcmbKindNM.Enabled = False

                '台数テキストボックス
                .ProptxtSetNmb.ReadOnly = True

                '分類１テキストボックス
                .ProptxtClass1.ReadOnly = True

                '分類２（メーカー）テキストボックス
                .ProptxtClass2.ReadOnly = True

                '名称（機種）テキストボックス
                .ProptxtCINM.ReadOnly = True

                '型番テキストボックス
                .ProptxtKataban.ReadOnly = True

                '導入開始日
                .PropdtpIntroductStDT.Enabled = False

                'タイプコンボボックス
                .PropcmbSCKikiType.Enabled = False

                'サービスセンター保管機チェックボックス
                .PropchkSCHokanKbn.Enabled = False

                '付属品テキストボックス
                .ProptxtFuzokuhin.ReadOnly = True

                '導入タイプ「経費購入」ラジオボタン
                .ProprdoIntroductKbn0.Enabled = True

                '導入タイプ「リース」ラジオボタン
                .ProprdoIntroductKbn1.Enabled = True

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【参照モード】基本情報コントロール設定
    ''' </summary>
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードに応じて基本情報コントロールの初期設定を行う
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : 2012/09/04(購入・リースグループボックス活性化) s.tsuruta</p>
    ''' </para></remarks>
    Private Function SetControlKhnForRef(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0901

                '種別コンボボックス
                .PropcmbKindNM.Enabled = False

                '台数テキストボックス
                .ProptxtSetNmb.ReadOnly = True

                '分類１テキストボックス
                .ProptxtClass1.ReadOnly = True

                '分類２（メーカー）テキストボックス
                .ProptxtClass2.ReadOnly = True

                '名称（機種）テキストボックス
                .ProptxtCINM.ReadOnly = True

                '型番テキストボックス
                .ProptxtKataban.ReadOnly = True

                '導入開始日
                .PropdtpIntroductStDT.Enabled = False

                'タイプコンボボックス
                .PropcmbSCKikiType.Enabled = False

                'サービスセンター保管機チェックボックス
                .PropchkSCHokanKbn.Enabled = False

                '付属品テキストボックス
                .ProptxtFuzokuhin.ReadOnly = True

                '導入タイプ「経費購入」ラジオボタン
                .ProprdoIntroductKbn0.Enabled = True

                '導入タイプ「リース」ラジオボタン
                .ProprdoIntroductKbn1.Enabled = True

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>マスタデータを取得する
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMastaData(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '[add] 2013/03/29 y.ikushima マスタデータ削除フラグ対応 START
        Dim dtKindCD As New DataTable
        Dim dtTypeKbn As New DataTable
        '[add] 2013/03/29 y.ikushima マスタデータ削除フラグ対応 END

        Try

            '種別マスタ取得
            '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 START
            'If commonLogicHBK.GetKindMastaData(Adapter, Cn, CI_TYPE_SUPORT, dataHBKB0901.PropDtKindMasta) = False Then
            '    Return False
            'End If
            '取得用SQLの作成・設定
            If sqlHBKB0901.SetSelectKindMastaDataSql(Adapter, Cn, dataHBKB0901) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "種別マスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtKindCD)
            dataHBKB0901.PropDtKindMasta = dtKindCD
            '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 END

            'サポセン機器タイプマスター取得
            '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 START
            'If commonLogicHBK.GetSapKikiTypeMastaData(Adapter, Cn, dataHBKB0901.PropDtSapKikiTypeMasta) = False Then
            '    Return False
            'End If

            '取得用SQLの作成・設定
            If sqlHBKB0901.SetSelectSapKikiTypeMastaDataSql(Adapter, Cn, dataHBKB0901) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サポセン機器タイプマスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtTypeKbn)
            dataHBKB0901.PropDtSapKikiTypeMasta = dtTypeKbn
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
        Finally
            '[add] 2013/03/29 y.ikushima マスタデータ削除フラグ対応 START
            dtKindCD.Dispose()
            dtTypeKbn.Dispose()
            '[add] 2013/03/29 y.ikushima マスタデータ削除フラグ対応 END
        End Try

    End Function

    ''' <summary>
    ''' 【共通】初期表示用メインデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainData(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0901

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '処理なし

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード

                    If .PropBlnBeLockedFlg = False Then             '編集モード

                        '編集モード用データ取得
                        If GetMainDataForEdit(Adapter, Cn, dataHBKB0901) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then          '参照（ロック）モード

                        '参照モード用データ取得　※編集モードと同じ
                        If GetMainDataForEdit(Adapter, Cn, dataHBKB0901) = False Then
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
        End Try

    End Function

    ''' <summary>
    ''' 【編集／参照モード】初期表示用メインデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集／参照モードで初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainDataForEdit(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '導入データ取得
            If GetIntroductInfo(Adapter, Cn, dataHBKB0901) = False Then
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
    ''' 【編集／参照モード】導入データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報データを取得する
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetIntroductInfo(ByVal Adapter As NpgsqlDataAdapter, _
                                      ByVal Cn As NpgsqlConnection, _
                                      ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtIntroduct As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKB0901.SetSelectIntroductSql(Adapter, Cn, dataHBKB0901) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "導入データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtIntroduct)

            'データが取得できなかった場合、エラー
            If dtIntroduct.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & B0901_E001, TBNM_INTRODUCT_TB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKB0901.PropDtIntroductTb = dtIntroduct

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            dtIntroduct.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】初期データ設定処理
    ''' </summary>
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームコントロールに初期表示データを設定する
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitDataToControl(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'ログイン／ロックデータ設定
            If SetDataToLoginAndLock(dataHBKB0901) = False Then
                Return False
            End If

            'ヘッダデータ設定
            If SetDataToHeader(dataHBKB0901) = False Then
                Return False
            End If

            'コントロールデータ設定
            If SetDataToTabControl(dataHBKB0901) = False Then
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
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLock(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0901

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToLoginAndLockForNew(dataHBKB0901) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード

                    If .PropBlnBeLockedFlg = False Then             '編集モード

                        '編集モード用設定
                        If SetDataToLoginAndLockForEdit(dataHBKB0901) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then          '参照（ロック）モード

                        '参照モード用設定
                        If SetDataToLoginAndLockForRef(dataHBKB0901) = False Then
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
        End Try

    End Function

    ''' <summary>
    ''' 【新規登録モード】ログイン／ロックデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForNew(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0901.PropGrpLoginUser

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
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForEdit(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0901.PropGrpLoginUser

                'ロック情報が取得できた場合
                If dataHBKB0901.PropDtIntroductLock.Rows.Count > 0 Then
                    'ロック開始日時
                    Dim dtmLockTime As DateTime = Nothing  '編集開始日時
                    If dataHBKB0901.PropDtIntroductLock.Rows(0).Item("EdiTime").ToString() <> "" Then
                        dtmLockTime = dataHBKB0901.PropDtIntroductLock.Rows(0).Item("EdiTime")
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
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForRef(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0901.PropGrpLoginUser

                'ロック情報が取得できた場合
                If dataHBKB0901.PropDtIntroductLock.Rows.Count > 0 Then
                    'ロック開始日時
                    Dim dtmLockTime As DateTime = Nothing
                    If dataHBKB0901.PropDtIntroductLock.Rows(0).Item("EdiTime").ToString() <> "" Then
                        dtmLockTime = dataHBKB0901.PropDtIntroductLock.Rows(0).Item("EdiTime")
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
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForRireki(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0901.PropGrpLoginUser

                'ロック開始日時 ※空文字のときはNothingを指定しないと落ちる
                Dim strLockTime As String = dataHBKB0901.PropStrEdiTime
                If dataHBKB0901.PropDtIntroductLock IsNot Nothing AndAlso dataHBKB0901.PropDtIntroductLock.Rows.Count > 0 Then
                    .PropLockDate = dataHBKB0901.PropDtIntroductLock.Rows(0).Item("EdiTime")
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
    ''' 【共通】ヘッダデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ヘッダデータを初期設定する
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToHeader(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0901

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToHeaderForNew(dataHBKB0901) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード

                    If .PropBlnBeLockedFlg = False Then        '編集モード

                        '編集モード用設定
                        If SetDataToHeaderForEdit(dataHBKB0901) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then    '参照（ロック）モード

                        '参照モード用設定
                        If SetDataToHeaderForRef(dataHBKB0901) = False Then
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
        End Try

    End Function

    ''' <summary>
    ''' 【新規登録モード】ヘッダデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでヘッダデータを初期設定する
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToHeaderForNew(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0901

                '導入番号テキストボックス
                .ProptxtIntroductNmb.Text = ""

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでヘッダデータを初期設定する
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToHeaderForEdit(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0901

                '導入番号テキストボックス
                .ProptxtIntroductNmb.Text = .PropIntIntroductNmb.ToString()

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでヘッダデータを初期設定する
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToHeaderForRef(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '編集モードヘッダデータ設定処理と同じ
            If SetDataToHeaderForEdit(dataHBKB0901) = False Then
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
    ''' 【共通】コントロールデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>コントロールデータを初期設定する
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabControl(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '基本情報データ設定
            If SetDataToKhn(dataHBKB0901) = False Then
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
    ''' 【共通】基本情報データ設定処理
    ''' </summary>
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて基本情報データを初期設定する
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToKhn(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0901

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToKhnForNew(dataHBKB0901) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード

                    If .PropBlnBeLockedFlg = False Then             '編集モード

                        '編集モード用設定
                        If SetDataToKhnForEdit(dataHBKB0901) = False Then
                            Return False
                        End If

                    ElseIf .PropBlnBeLockedFlg = True Then          '参照（ロック）モード

                        '参照モード用設定
                        If SetDataToKhnForRef(dataHBKB0901) = False Then
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
        End Try

    End Function

    ''' <summary>
    ''' 【新規登録モード】基本情報データ設定処理
    ''' </summary>
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードで基本情報データを初期設定する
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToKhnForNew(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'コンボボックス作成
            If CreateCmb(dataHBKB0901) = False Then
                Return False
            End If

            With dataHBKB0901

                '種別コンボボックス
                .PropcmbKindNM.SelectedValue = ""

                '台数テキストボックス
                .ProptxtSetNmb.Text = ""

                '機器番号（From）テキストボックス
                .ProptxtKikiNmbFrom.Text = ""

                '機器番号（To）テキストボックス
                .ProptxtKikiNmbTo.Text = ""

                '分類１テキストボックス
                .ProptxtClass1.Text = ""

                '分類２（メーカー）テキストボックス
                .ProptxtClass2.Text = ""

                '名称（機種）テキストボックス
                .ProptxtCINM.Text = ""

                '型番テキストボックス
                .ProptxtKataban.Text = ""

                '導入開始日テキストボックス
                .PropdtpIntroductStDT.txtDate.Text = ""

                'タイプコンボボックス
                .PropcmbSCKikiType.SelectedValue = ""

                'サービスセンター保管機チェックボックス
                .PropchkSCHokanKbn.Checked = False

                '付属品テキストボックス
                .ProptxtFuzokuhin.Text = ""

                '導入備考テキストボックス
                .ProptxtIntroductBiko.Text = ""

                '導入廃棄完了チェックボックス
                .PropchkIntroductDelKbn.Checked = False

                '保証書有無ラジオボタン
                .ProprdoHosyoUmu0.Checked = False
                .ProprdoHosyoUmu1.Checked = True
                .ProprdoHosyoUmu2.Checked = False

                '保証書保管場所テキストボックス
                .ProptxtHosyoPlace.Text = ""

                '保証書廃棄日テキストボックス
                .PropdtpHosyoDelDT.txtDate.Text = ""

                'メーカー無償保証期間テキストボックス
                .ProptxtMakerHosyoTerm.Text = ""

                'EOSテキストボックス
                .ProptxtEOS.Text = ""

                '導入タイプラジオボタン
                .ProprdoIntroductKbn0.Checked = True
                .ProprdoIntroductKbn1.Checked = False

                '廃棄予定日
                .PropdtpDelScheduleDT.txtDate.Text = ""

                'リース会社テキストボックス
                .ProptxtLeaseCompany.Text = ""

                'リース番号テキストボックス
                .ProptxtLeaseNmb.Text = ""

                '期限日
                .PropdtpLeaseUpDT.txtDate.Text = ""

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】基本情報データ設定処理
    ''' </summary>
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで基本情報データを初期設定する
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToKhnForEdit(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'コンボボックス作成
            If CreateCmb(dataHBKB0901) = False Then
                Return False
            End If

            With dataHBKB0901

                '種別コンボボックス
                .PropcmbKindNM.SelectedValue = .PropDtIntroductTb.Rows(0).Item("KindCD")

                '台数テキストボックス
                .ProptxtSetNmb.Text = .PropDtIntroductTb.Rows(0).Item("SetNmb")

                '機器番号（From）テキストボックス
                .ProptxtKikiNmbFrom.Text = .PropDtIntroductTb.Rows(0).Item("KikiNmbFrom")

                '機器番号（To）テキストボックス
                .ProptxtKikiNmbTo.Text = .PropDtIntroductTb.Rows(0).Item("KikiNmbTo")

                '分類１テキストボックス
                .ProptxtClass1.Text = .PropDtIntroductTb.Rows(0).Item("Class1")

                '分類２（メーカー）テキストボックス
                .ProptxtClass2.Text = .PropDtIntroductTb.Rows(0).Item("Class2")

                '名称（機種）テキストボックス
                .ProptxtCINM.Text = .PropDtIntroductTb.Rows(0).Item("CINM")

                '型番テキストボックス
                .ProptxtKataban.Text = .PropDtIntroductTb.Rows(0).Item("Kataban")

                '導入開始日テキストボックス
                If .PropDtIntroductTb.Rows(0).Item("IntroductStDT").ToString <> "" Then
                    .PropdtpIntroductStDT.txtDate.Text = .PropDtIntroductTb.Rows(0).Item("IntroductStDT").ToString
                Else
                    .PropdtpIntroductStDT.txtDate.Text = ""
                End If

                'タイプコンボボックス
                .PropcmbSCKikiType.SelectedValue = .PropDtIntroductTb.Rows(0).Item("TypeKbn")

                'サービスセンター保管機チェックボックス
                If .PropDtIntroductTb.Rows(0).Item("SCHokanKbn") = SC_HOKANKBN_ON Then
                    .PropchkSCHokanKbn.Checked = True
                Else
                    .PropchkSCHokanKbn.Checked = False
                End If


                '付属品テキストボックス
                .ProptxtFuzokuhin.Text = .PropDtIntroductTb.Rows(0).Item("Fuzokuhin")

                '導入備考テキストボックス
                .ProptxtIntroductBiko.Text = .PropDtIntroductTb.Rows(0).Item("IntroductBiko")

                '導入廃棄完了チェックボックス
                If .PropDtIntroductTb.Rows(0).Item("IntroductDelKbn") = FLG_ON Then
                    .PropchkIntroductDelKbn.Checked = True
                ElseIf .PropDtIntroductTb.Rows(0).Item("IntroductDelKbn") = FLG_OFF Then
                    .PropchkIntroductDelKbn.Checked = False
                End If

                '保証書有無ラジオボタン
                If .PropDtIntroductTb.Rows(0).Item("HosyoUmu") = RADIO_ZERO Then
                    .ProprdoHosyoUmu0.Checked = True
                    .ProprdoHosyoUmu1.Checked = False
                    .ProprdoHosyoUmu2.Checked = False
                ElseIf .PropDtIntroductTb.Rows(0).Item("HosyoUmu") = RADIO_ONE Then
                    .ProprdoHosyoUmu0.Checked = False
                    .ProprdoHosyoUmu1.Checked = True
                    .ProprdoHosyoUmu2.Checked = False
                ElseIf .PropDtIntroductTb.Rows(0).Item("HosyoUmu") = RADIO_TWO Then
                    .ProprdoHosyoUmu0.Checked = False
                    .ProprdoHosyoUmu1.Checked = False
                    .ProprdoHosyoUmu2.Checked = True
                End If

                '保証書保管場所テキストボックス
                .ProptxtHosyoPlace.Text = .PropDtIntroductTb.Rows(0).Item("HosyoPlace")

                '保証書廃棄日テキストボックス
                If .PropDtIntroductTb.Rows(0).Item("HosyoDelDT").ToString <> "" Then
                    .PropdtpHosyoDelDT.txtDate.Text = .PropDtIntroductTb.Rows(0).Item("HosyoDelDT").ToString
                Else
                    .PropdtpHosyoDelDT.txtDate.Text = ""
                End If

                'メーカー無償保証期間テキストボックス
                .ProptxtMakerHosyoTerm.Text = .PropDtIntroductTb.Rows(0).Item("MakerHosyoTerm")

                'EOSテキストボックス
                .ProptxtEOS.Text = .PropDtIntroductTb.Rows(0).Item("EOS")

                '導入タイプラジオボタン
                If .PropDtIntroductTb.Rows(0).Item("IntroductKbn") = RADIO_ZERO Then
                    .ProprdoIntroductKbn0.Checked = True
                    .ProprdoIntroductKbn1.Checked = False
                    .ProptxtLeaseCompany.ReadOnly = True
                    .ProptxtLeaseNmb.ReadOnly = True
                    .PropdtpLeaseUpDT.Enabled = False
                ElseIf .PropDtIntroductTb.Rows(0).Item("IntroductKbn") = RADIO_ONE Then
                    .ProprdoIntroductKbn0.Checked = False
                    .ProprdoIntroductKbn1.Checked = True
                    .PropdtpDelScheduleDT.Enabled = False
                End If

                '廃棄予定日テキストボックス
                If .PropDtIntroductTb.Rows(0).Item("DelScheduleDT").ToString <> "" Then
                    .PropdtpDelScheduleDT.txtDate.Text = .PropDtIntroductTb.Rows(0).Item("DelScheduleDT").ToString
                Else
                    .PropdtpDelScheduleDT.txtDate.Text = ""
                End If

                'リース会社テキストボックス
                .ProptxtLeaseCompany.Text = .PropDtIntroductTb.Rows(0).Item("LeaseCompany")

                'リース番号テキストボックス
                .ProptxtLeaseNmb.Text = .PropDtIntroductTb.Rows(0).Item("LeaseNmb")

                '期限日テキストボックス
                If .PropDtIntroductTb.Rows(0).Item("LeaseUpDT").ToString <> "" Then
                    .PropdtpLeaseUpDT.txtDate.Text = .PropDtIntroductTb.Rows(0).Item("LeaseUpDT").ToString
                Else
                    .PropdtpLeaseUpDT.txtDate.Text = ""
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
    ''' 【参照モード】基本情報データ設定処理
    ''' </summary>
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで基本情報データを初期設定する
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToKhnForRef(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '編集モード基本情報タブデータ設定処理と同じ
            If SetDataToKhnForEdit(dataHBKB0901) = False Then
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
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のコンボボックスを作成する
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateCmb(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0901

                '種別コンボボックス作成
                If commonLogic.SetCmbBox(.PropDtKindMasta, .PropcmbKindNM, True, "", "") = False Then
                    Return False
                End If

                'タイプコンボボックス作成
                If commonLogic.SetCmbBox(.PropDtSapKikiTypeMasta, .PropcmbSCKikiType, True, "", "") = False Then
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
    ''' 【参照モード】ロック解除時ログ出力処理
    ''' </summary>
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>他のユーザによりロックが解除された場合に編集中の入力内容をログに出力する
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function OutputUnlockLog(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

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
            With dataHBKB0901

                '書込用テキスト作成
                strPlmList.Add(.ProptxtIntroductNmb.Text)           '導入番号
                strPlmList.Add(.PropcmbKindNM.Text)                 '種別
                strPlmList.Add(.ProptxtSetNmb.Text)                 '台数
                strPlmList.Add(.ProptxtKikiNmbFrom.Text)            '機器番号（From）
                strPlmList.Add(.ProptxtKikiNmbTo.Text)              '機器番号（To）
                strPlmList.Add(.ProptxtClass1.Text)                 '分類1
                strPlmList.Add(.ProptxtClass2.Text)                 '分類2
                strPlmList.Add(.ProptxtCINM.Text)                   '名称（機種）
                strPlmList.Add(.ProptxtKataban.Text)                '型番
                strPlmList.Add(.PropdtpIntroductStDT.txtDate.Text)  '導入開始日
                strPlmList.Add(.PropcmbSCKikiType.Text)             'タイプ

                'サービスセンター保管機
                If .PropchkSCHokanKbn.Checked = True Then
                    strPlmList.Add(FLG_ON_NM)
                Else
                    strPlmList.Add(FLG_OFF_NM)
                End If

                strPlmList.Add(.ProptxtFuzokuhin.Text)              '付属品
                strPlmList.Add(.ProptxtIntroductBiko.Text)          '導入備考

                '導入廃棄完了
                If .PropchkIntroductDelKbn.Checked = True Then
                    strPlmList.Add(FLG_ON_NM)
                Else
                    strPlmList.Add(FLG_OFF_NM)
                End If

                '保証書有無ラジオボタン
                If .ProprdoHosyoUmu0.Checked = True Then
                    strPlmList.Add(RADIO_HOSYO_HUMEI)
                ElseIf .ProprdoHosyoUmu1.Checked = True Then
                    strPlmList.Add(RADIO_HOSYO_ARI)
                ElseIf .ProprdoHosyoUmu2.Checked = True Then
                    strPlmList.Add(RADIO_HOSYO_NASI)
                End If

                strPlmList.Add(.ProptxtHosyoPlace.Text)             '保証書保管場所
                strPlmList.Add(.PropdtpHosyoDelDT.txtDate.Text)     '保証書廃棄日
                strPlmList.Add(.ProptxtMakerHosyoTerm.Text)         'メーカー無償保証期間
                strPlmList.Add(.ProptxtEOS.Text)                    'EOS

                '導入タイプラジオボタン
                If .ProprdoIntroductKbn0.Checked = True Then
                    strPlmList.Add(RADIO_KEIHI)
                ElseIf .ProprdoIntroductKbn1.Checked = True Then
                    strPlmList.Add(RADIO_LEASE)
                End If

                strPlmList.Add(.PropdtpDelScheduleDT.txtDate.Text)  '廃棄予定日
                strPlmList.Add(.ProptxtLeaseCompany.Text)           'リース会社
                strPlmList.Add(.ProptxtLeaseNmb.Text)               'リース番号
                strPlmList.Add(.PropdtpLeaseUpDT.txtDate.Text)      'リース期限日

                'ログ出力フォルダ設定
                strOutputDir = Path.Combine(Application.StartupPath, OUTPUT_DIR_UNLOCKEDLOG)

                'ログファイル名設定
                strLogFileName = Format(DateTime.Parse(.PropDtIntroductLock.Rows(0).Item("SysTime")), "yyyyMMddHHmmss") & ".log"

                'ファイル出力を実行し、出力ファイルパスを取得
                If commonLogicHBK.OutputLogFromTextFormat(strPlmList, strLogFileName, _
                                                          FORMAT_DIR_UNLOCKEDLOG, _
                                                          FILE_UNLOCKLOG_INTRODUCT, _
                                                          strOutputDir, _
                                                          strLogFilePath) = False Then
                    Return False
                End If

                'データクラスにメッセージをセット
                dataHBKB0901.PropStrBeUnlockedMsg = String.Format(HBK_W001, strLogFilePath)

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
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータのロックが解除されていないかチェックする
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckUnlock(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnBeUnocked As Boolean = False       'ロックフラグ

        Try

            'ロック解除チェック
            If CheckDataBeUnlocked(dataHBKB0901.PropIntIntroductNmb, dataHBKB0901.PropGrpLoginUser.PropLockDate.ToString(), _
                                                blnBeUnocked, dataHBKB0901.PropDtIntroductLock) = False Then
                Return False
            End If


            'ロック解除されている場合、ロックフラグON
            If blnBeUnocked = True Then

                dataHBKB0901.PropBlnBeLockedFlg = True

            Else

                dataHBKB0901.PropBlnBeLockedFlg = False

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
    ''' 【共通】入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録項目の入力チェックを行う
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckInputValue(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0901

                '種別コンボボックス
                With .PropcmbKindNM
                    '未入力の場合、エラー
                    If .SelectedValue = "" Then
                        'エラーメッセージ設定
                        puErrMsg = B0901_E002
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With

                '台数テキストボックス
                With .ProptxtSetNmb
                    '未入力の場合、エラー
                    If .Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = B0901_E003
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                    If commonValidation.IsHalfNmb(.Text.Trim()) = False Then
                        'エラーメッセージ設定
                        puErrMsg = B0901_E010
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                    '未入力の場合、エラー
                    If Integer.Parse(.Text.Trim()) = 0 Then
                        'エラーメッセージ設定
                        puErrMsg = B0901_E011
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With

                '分類１テキストボックス
                With .ProptxtClass1
                    '未入力の場合、エラー
                    If .Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = B0901_E004
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With

                '分類２（メーカー）テキストボックス
                With .ProptxtClass2
                    '未入力の場合、エラー
                    If .Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = B0901_E005
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With

                '名称（機種）テキストボックス
                With .ProptxtCINM
                    '未入力の場合、エラー
                    If .Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = B0901_E006
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With

                '型番テキストボックス
                With .ProptxtKataban
                    '未入力の場合、エラー
                    If .Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = B0901_E007
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With

                '導入開始日テキストボックス
                With .PropdtpIntroductStDT.txtDate
                    '未入力の場合、エラー
                    If .Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = B0901_E008
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With

                'タイプコンボボックス
                With .PropcmbSCKikiType
                    '未入力の場合、エラー
                    If .SelectedValue = "" Then
                        'エラーメッセージ設定
                        puErrMsg = B0901_E009
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
    ''' 【新規登録モード】データ新規登録処理
    ''' </summary>
    ''' <param name="dataHBKB0901">[IN]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をDBに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertNewData(ByVal dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Tsx As NpgsqlTransaction = Nothing    'トランザクション

        Try
            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            '新規導入番号、システム日付取得
            If SelectNewIntroductNmbAndSysDate(Cn, dataHBKB0901) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '機器番号取得
            If SelectNewKikiNmb(Cn, dataHBKB0901) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '機器番号更新
            If UpdateKikiNmb(Cn, dataHBKB0901) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '導入新規登録
            If InsertIntroduct(Cn, dataHBKB0901) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '新規ログNo号取得
            If GetNewlogNo(Adapter, Cn, dataHBKB0901) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '導入新規ログ登録
            If InsertIntroductLog(Cn, dataHBKB0901) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            For i As Integer = 1 To Integer.Parse(dataHBKB0901.ProptxtSetNmb.Text)

                '実行回数を設定
                dataHBKB0901.PropIntiNmb = i

                '新規CI番号取得
                If SelectNewCINmb(Cn, dataHBKB0901) = False Then
                    'ロールバック
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    Return False
                End If

                'CI共通情報新規登録
                If InsertCIInfo(Cn, dataHBKB0901) = False Then
                    'ロールバック
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    Return False
                End If

                'CIサポセン機器新規登録
                If InsertCISap(Cn, dataHBKB0901) = False Then
                    'ロールバック
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    Return False
                End If

                '履歴情報新規登録（共通）
                If InsertRireki(Cn, dataHBKB0901) = False Then
                    'ロールバック
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    Return False
                End If

            Next

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
    ''' 【編集／履歴モード】データ更新処理
    ''' </summary>
    ''' <param name="dataHBKB0901">[IN]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をDBに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateData(ByVal dataHBKB0901 As DataHBKB0901) As Boolean

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
            If SelectSysDate(Adapter, Cn, dataHBKB0901) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '導入更新（UPDATE）
            If UpdateIntroduct(Cn, dataHBKB0901) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '新規ログNo号取得
            If GetNewlogNo(Adapter, Cn, dataHBKB0901) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '導入新規ログ登録
            If InsertIntroductLog(Cn, dataHBKB0901) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'ロック解除
            If UnlockIntroduct(dataHBKB0901.PropIntIntroductNmb) = False Then
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
    ''' <param name="dataHBKB0901">[IN]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータのロックを解除する
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UnlockData(ByVal dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '導入ロック解除（DELETE）
            If UnlockIntroduct(dataHBKB0901.PropIntIntroductNmb) = False Then
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
    ''' ロック解除処理
    ''' </summary>
    ''' <param name="intIntroductNmb">[IN]導入番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>CI共通情報のロックを解除する
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function UnlockIntroduct(ByVal intIntroductNmb As Integer) As Boolean

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

            '導入ロックテーブル削除処理
            If DeleteIntroductLock(Cn, intIntroductNmb) = False Then
                'ロールバック
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
    ''' 【新規登録モード】新規導入番号取得処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0901">[IN]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番した導入番号を取得（SELECT）する
    ''' <para>作成情報：2012/07/17 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectNewIntroductNmbAndSysDate(ByVal Cn As NpgsqlConnection, _
                                              ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtResult As New DataTable

        Try
            '新規導入番号取得（SELECT）用SQLを作成
            If sqlHBKB0901.SetSelectNewIntroductNmbAndSysDateSql(Adapter, Cn, dataHBKB0901) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規導入番号、システム日付取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            'データが取得できた場合、データクラスに取得データをセット
            If dtResult.Rows.Count > 0 Then
                dataHBKB0901.PropIntIntroductNmb = dtResult.Rows(0).Item("IntroductNmb")      '新規導入番号
                dataHBKB0901.PropDtmSysDate = dtResult.Rows(0).Item("SysDate")                'サーバー日付
            Else
                '取得できなかったときはエラー
                puErrMsg = B0901_E012
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
    ''' 【新規登録モード】機器番号取得処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0901">[IN]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に種別ごとに採番した機器番号を取得（SELECT）する
    ''' <para>作成情報：2012/07/17 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectNewKikiNmb(ByVal Cn As NpgsqlConnection, _
                                              ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtResult As New DataTable

        Try
            '機器番号取得（SELECT）用SQLを作成
            If sqlHBKB0901.SetSelectKindSaibanMtbSql(Adapter, Cn, dataHBKB0901) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "機器番号取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            'データが取得できた場合、データクラスに取得データをセット
            If dtResult.Rows.Count > 0 Then
                dataHBKB0901.PropIntKikiNmbFrom = dtResult.Rows(0).Item("CurentNmb")        '新規導入番号
                dataHBKB0901.PropIntMinNmb = dtResult.Rows(0).Item("MinNmb")                '最小値
                dataHBKB0901.PropIntMaxNmb = dtResult.Rows(0).Item("MaxNmb")                '最大値
                dataHBKB0901.PropstrLoopFlg = dtResult.Rows(0).Item("LoopFlg")
            Else
                '取得できなかったときはエラー
                puErrMsg = B0901_E015
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
    ''' 【新規登録モード】機器番号更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0901">[IN]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>種別ごとに採番した機器番号を更新（UPDATE）する
    ''' <para>作成情報：2012/07/17 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateKikiNmb(ByVal Cn As NpgsqlConnection, _
                                   ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        With dataHBKB0901
            '採番可能な最大値を超えていないかチェックし、機器番号（To）を設定
            If .PropIntKikiNmbFrom + Integer.Parse(.ProptxtSetNmb.Text) > .PropIntMaxNmb Then
                '繰返しフラグが、繰返しなしとなっているかチェック
                If .PropstrLoopFlg = FLG_OFF Then
                    puErrMsg = String.Format(B0901_E016, dataHBKB0901.PropcmbKindNM.Text)
                    Return False
                ElseIf .PropstrLoopFlg = FLG_ON Then
                    .PropIntKikiNmbTo = .PropIntKikiNmbFrom + Integer.Parse(.ProptxtSetNmb.Text) - .PropIntMaxNmb
                End If
            Else
                .PropIntKikiNmbTo = .PropIntKikiNmbFrom + Integer.Parse(.ProptxtSetNmb.Text)
            End If

            '機器番号（From）を設定
            .PropIntKikiNmbFrom = .PropIntKikiNmbFrom + 1
        End With

        Try
            '機器番号更新（UPDATE）用SQLを作成
            If sqlHBKB0901.SetUpdateKindSaibanMtbSql(Cmd, Cn, dataHBKB0901) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "機器番号取得", Nothing, Cmd)

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
    ''' 【新規登録モード】新規CI番号取得処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0901">[IN]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番したCI番号を取得（SELECT）する
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectNewCINmb(ByVal Cn As NpgsqlConnection, _
                                              ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtResult As New DataTable

        Try
            '新規CI番号取得（SELECT）用SQLを作成
            If sqlHBKB0901.SetSelectNewCINmbAndSysDateSql(Adapter, Cn, dataHBKB0901) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規CI番号取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            'データが取得できた場合、データクラスに取得データをセット
            If dtResult.Rows.Count > 0 Then
                dataHBKB0901.PropIntCINmb = dtResult.Rows(0).Item("CINmb")      '新規CI番号
            Else
                '取得できなかったときはエラー
                puErrMsg = B0901_E013
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
    ''' 【新規登録モード】導入新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0901">[IN]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容を導入テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/17 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertIntroduct(ByVal Cn As NpgsqlConnection, _
                                     ByVal dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            '導入新規登録（INSERT）用SQLを作成
            If sqlHBKB0901.SetInsertIntroductSql(Cmd, Cn, dataHBKB0901) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "導入新規登録", Nothing, Cmd)

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
    ''' 【編集モード】導入更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0901">[IN]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容で導入テーブルを更新（UPDATE）する
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateIntroduct(ByVal Cn As NpgsqlConnection, _
                                     ByVal dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CI共通情報更新（UPDATE）用SQLを作成
            If sqlHBKB0901.SetUpdateIntroductSql(Cmd, Cn, dataHBKB0901) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "導入更新", Nothing, Cmd)

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
    ''' 【新規登録モード】導入ログ新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0901">[IN]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容を導入ログテーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/17 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertIntroductLog(ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            '導入ログ新規登録（INSERT）用SQLを作成
            If sqlHBKB0901.SetInsertIntroductLogSql(Cmd, Cn, dataHBKB0901) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "導入ログ新規登録", Nothing, Cmd)

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
    ''' 【新規登録モード】CI共通情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0901">[IN]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をCI共通情報テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/17 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCIInfo(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CI共通情報新規登録（INSERT）用SQLを作成
            If sqlHBKB0901.SetInsertCIInfoSql(Cmd, Cn, dataHBKB0901) = False Then
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
    ''' 【編集モード】サーバー日付取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0901">[IN]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>更新用のサーバー日付を取得する
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectSysDate(ByRef Adapter As NpgsqlDataAdapter, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSysDate As New DataTable         '履歴番号格納用テーブル

        Try
            '*************************************
            '* サーバー日付取得
            '*************************************

            'SQLを作成
            If sqlHBKB0901.SetSelectSysDateSql(Adapter, Cn) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サーバー日付取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtSysDate)

            'データが取得できた場合、データクラスに履歴番号をセット
            If dtSysDate.Rows.Count > 0 Then
                dataHBKB0901.PropDtmSysDate = dtSysDate.Rows(0).Item("SysDate")
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
    ''' 【新規登録モード】CIサポセン機器新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0901">[IN]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をCIサポセン機器テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCISap(ByVal Cn As NpgsqlConnection, _
                                 ByVal dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CIサポセン機器新規登録（INSERT）用SQLを作成
            If sqlHBKB0901.SetInsertCISapSql(Cmd, Cn, dataHBKB0901) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIサポセン機器新規登録", Nothing, Cmd)

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
    ''' 【共通】新規ログNo号取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapter</param>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0901">[IN]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番したログNoを取得する
    ''' <para>作成情報：2012/07/17 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetNewlogNo(ByVal Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtLogNo As New DataTable         'ログNo格納用テーブル

        Try

            'SQLを作成
            If sqlHBKB0901.SetSelectNewLogNoSql(Adapter, Cn, dataHBKB0901) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規ログNo取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtLogNo)

            'データが取得できた場合、データクラスに履歴番号をセット
            If dtLogNo.Rows.Count > 0 Then
                dataHBKB0901.PropIntLogNo = dtLogNo.Rows(0).Item("LogNo")
            Else
                '取得できなかった場合はエラー
                puErrMsg = B0901_E017
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
            dtLogNo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】履歴情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0901">[IN]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>履歴／変更理由を各テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRireki(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter    'アダプタ
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            '新規履歴番号取得
            If GetNewRirekiNo(Adapter, Cn, dataHBKB0901) = False Then
                Return False
            End If

            'CI共通情報履歴テーブル登録
            If InsertCIINfoR(Cn, dataHBKB0901) = False Then
                Return False
            End If

            'CIサポセン機器履歴テーブル登録
            If InsertCISapR(Cn, dataHBKB0901) = False Then
                Return False
            End If

            '登録理由履歴テーブル登録
            If InsertRegReasonR(Cn, dataHBKB0901) = False Then
                Return False
            End If

            '原因リンク履歴テーブル登録
            If InsertCauseLinkR(Cn, dataHBKB0901) = False Then
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
    ''' <param name="dataHBKB0901">[IN]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番した履歴番号を取得する
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetNewRirekiNo(ByVal Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtRirekiNo As New DataTable         '履歴番号格納用テーブル

        Try

            'SQLを作成
            If sqlHBKB0901.SetSelectNewRirekiNoSql(Adapter, Cn, dataHBKB0901) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規履歴番号取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtRirekiNo)

            'データが取得できた場合、データクラスに履歴番号をセット
            If dtRirekiNo.Rows.Count > 0 Then
                dataHBKB0901.PropIntRirekiNo = dtRirekiNo.Rows(0).Item("RirekiNo")
            Else
                '取得できなかった場合はエラー
                puErrMsg = B0901_E014
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
    ''' <param name="dataHBKB0901">[IN]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCIINfoR(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0901.SetInsertCIInfoRSql(Cmd, Cn, dataHBKB0901) = False Then
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
    ''' 【共通】CIサポセン機器履歴テーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0901">[IN]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CIシステム履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCISapR(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0901.SetInsertCISapRSql(Cmd, Cn, dataHBKB0901) = False Then
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
    ''' 【共通】登録理由履歴テーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0901">[IN]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録理由履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRegReasonR(ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0901.SetInsertRegReasonRSql(Cmd, Cn, dataHBKB0901) = False Then
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
    ''' <param name="dataHBKB0901">[IN]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>原因リンク履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCauseLinkR(ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'データ数分繰り返し、登録を行う
            For i As Integer = 0 To dataHBKB0901.PropDtCauseLink.Rows.Count - 1

                '登録行をデータクラスにセット
                dataHBKB0901.PropRowReg = dataHBKB0901.PropDtCauseLink.Rows(i)

                'SQLを作成
                If sqlHBKB0901.SetInsertCauseLinkRSql(Cmd, Cn, dataHBKB0901) = False Then
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
    ''' 導入タイプラジオボタン変更処理
    ''' </summary>
    ''' <param name="dataHBKB0901">[IN/OUT]導入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>導入タイプラジオボタン変更に応じて各項目を活性・非活性化るす
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckRadioIntroductKbn(ByRef dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        With dataHBKB0901

            If .ProprdoIntroductKbn0.Checked = True Then
                '*************************
                '* 経費購入ボタンを選択
                '*************************

                '廃棄予定日テキストボックス
                .PropdtpDelScheduleDT.Enabled = True

                'リース会社テキストボックス
                With .ProptxtLeaseCompany
                    .Text = ""
                    .Enabled = False
                End With

                'リース番号テキストボックス
                With .ProptxtLeaseNmb
                    .Text = ""
                    .Enabled = False
                End With

                '期限日
                With .PropdtpLeaseUpDT
                    .txtDate.Text = ""
                    .Enabled = False
                End With

            ElseIf .ProprdoIntroductKbn1.Checked = True Then
                '*************************
                '* リースボタンを選択
                '*************************

                '廃棄予定日テキストボックス
                With .PropdtpDelScheduleDT
                    .txtDate.Text = ""
                    .Enabled = False
                End With

                'リース会社テキストボックス
                .ProptxtLeaseCompany.Enabled = True
                .ProptxtLeaseCompany.ReadOnly = False

                'リース番号テキストボックス
                .ProptxtLeaseNmb.Enabled = True
                .ProptxtLeaseNmb.ReadOnly = False

                '期限日
                .PropdtpLeaseUpDT.Enabled = True

            End If

        End With

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' ロック状況チェック処理
    ''' </summary>
    ''' <param name="intIntroductNmb">[IN]導入番号</param>
    ''' <param name="blnBeLocked">[IN/OUT]ロックフラグ（ロック時：True）</param>
    ''' <param name="strBeLockedMsg">ロック時メッセージ</param>
    ''' <param name="dtIntroductLock">導入ロックテーブル</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>指定された導入番号の導入がロックされているかチェックする。
    ''' また、ロックされている場合はエラーメッセージも返す
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function CheckDataBeLocked(ByVal intIntroductNmb As Integer, _
                                      ByRef blnBeLocked As Boolean, _
                                      ByRef strBeLockedMsg As String, _
                                      ByRef dtIntroductLock As DataTable
                                      ) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '導入ロックテーブル項目格納用変数宣言
        Dim dtResult As New DataTable           'SELECT結果格納テーブル

        'ロックチェック用変数宣言
        Dim strEdiID As String = ""             '編集者ID
        Dim strEdiTime As String = ""           '編集開始日時
        Dim strSysTime As String                '現在日時（サーバー）
        Dim tsDiff As TimeSpan = Nothing        '編集開始日時と現在日時の差
        Dim tsUnlock As TimeSpan = Nothing      'ロック解除時間  

        Try
            'ロックフラグ、CI共通情報ロックデータ数初期化
            blnBeLocked = False

            '導入ロックテーブル取得
            If GetIntroductLockTb(intIntroductNmb, dtResult) = False Then
                Return False
            End If

            '現在日時を取得
            strSysTime = dtResult.Rows(0).Item("SysTime").ToString()

            '導入ロックデータが取得できた場合、チェックを行う
            If dtResult.Rows.Count > 0 AndAlso dtResult.Rows(0).Item("EdiID") <> "" Then

                '編集者IDを取得
                strEdiID = dtResult.Rows(0).Item("EdiID")

                ''編集者IDがログインユーザIDと異なるかチェック
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
            dtIntroductLock = dtResult

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
    ''' ロック解除状況チェック処理
    ''' </summary>
    ''' <param name="intIntroductNmb">[IN]導入番号</param>
    ''' <param name="strEdiTime_Bef">[IN]既に設定済の編集開始日時</param>
    ''' <param name="blnBeUnocked">[IN/OUT]ロック解除フラグ（True：ロック解除されている）</param>
    ''' <param name="dtCILock">[IN/OUT]CI共通情報ロックデータ</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>指定されたCI番号のCI共通情報のロック解除状況をチェックする。
    ''' <para>作成情報：2012/06/27 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function CheckDataBeUnlocked(ByVal intIntroductNmb As Integer, _
                                        ByVal strEdiTime_Bef As String, _
                                        ByRef blnBeUnocked As Boolean, _
                                        ByRef dtCILock As DataTable
                                        ) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '導入ロックテーブル項目格納用変数宣言
        Dim dtResult As New DataTable           'SELECT結果格納テーブル

        'ロック解除チェック用変数宣言
        Dim strEdiID As String = ""             '編集者ID
        Dim strEdiTime As String = ""           '編集開始日時
        Dim strSysTime As String                '現在日時（サーバー）
        Dim tsDiff As TimeSpan = Nothing        '編集開始日時と現在日時の差
        Dim tsUnlock As TimeSpan = Nothing      'ロック解除時間    

        Try
            'ロック解除フラグ初期化
            blnBeUnocked = False

            '********************************
            '* 導入ロックテーブル取得
            '********************************

            If GetIntroductLockTb(intIntroductNmb, dtResult) = False Then
                Return False
            End If


            '********************************
            '* ロック解除チェック
            '********************************

            '現在日時を取得
            strSysTime = dtResult.Rows(0).Item("SysTime").ToString()

            'CI共通情報ロックデータが取得できた場合、チェックを行う
            If dtResult.Rows.Count > 0 AndAlso dtResult.Rows(0).Item("EdiID") <> "" Then

                '編集者IDを取得
                strEdiID = dtResult.Rows(0).Item("EdiID")


                ''編集者IDがログインユーザIDと異なるかチェック
                'If strEdiID <> PropUserId Then

                '    '編集者IDがログインユーザIDと異なる場合、ロック解除フラグON
                '    blnBeUnocked = True

                'Else

                '設定済の編集開始日時を取得
                strEdiTime = strEdiTime_Bef

                '編集開始日時がセットされている場合、現在日時と編集開始日時の差異がシステム管理マスタ.ロック解除時間以内かチェック
                If strEdiTime <> "" Then

                    '現在日時と編集開始日時の差を取得し、その差がロック解除時間を上回る場合はロック解除されている
                    tsDiff = New TimeSpan(DateTime.Parse(strSysTime).Subtract(DateTime.Parse(strEdiTime)).Ticks)
                    tsUnlock = TimeSpan.Parse(PropUnlockTime)
                    If tsDiff >= tsUnlock Then

                        'ロック解除フラグON
                        blnBeUnocked = True

                    End If

                End If

                'End If

            Else
                'CI共通情報ロックデータが取得できなかった場合

                'ロック解除フラグON
                blnBeUnocked = True

            End If

            '取得データを戻り値にセット
            dtCILock = dtResult


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
    ''' 導入ロック情報取得処理
    ''' </summary>
    ''' <param name="intIntroductNmb">[IN]導入番号</param>
    ''' <param name="dtResult">[IN/OUT]</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>指定された導入番号の導入ロックテーブルおよびサーバー日付を返す
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function GetIntroductLockTb(ByVal intIntroductNmb As Integer, _
                                       ByRef dtResult As DataTable) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'DB接続用変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        'SQL変数宣言--------------------------------------

        Try
            'コネクションを開く
            Cn.Open()

            '導入ロックテーブル、サーバー日付取得
            If SelectIntroductLock(Adapter, Cn, intIntroductNmb, dtResult) = False Then
                Return False
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
            dtResult.Dispose()
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Adapter.Dispose()
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 導入ロックテーブル、サーバー日付取得処理
    ''' </summary>
    ''' <param name="intIntroductNmb">[IN]導入番号</param>
    ''' <param name="dtResult">[IN/OUT]</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>指定された導入番号の導入ロックテーブルおよびサーバー日付を返す
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function SelectIntroductLock(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal intIntroductNmb As Integer, _
                                        ByRef dtResult As DataTable) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim drResult As DataRow

        'データ格納用テーブル初期化
        dtResult = New DataTable
        Dim dtSysDate = New DataTable
        Dim dtLockInfo = New DataTable

        'DataRowを１行追加
        dtResult.Columns.Add("EdiTime", Type.GetType("System.DateTime"))
        dtResult.Columns.Add("EdiGrpCD", Type.GetType("System.String"))
        dtResult.Columns.Add("EdiID", Type.GetType("System.String"))
        dtResult.Columns.Add("EdiGroupNM", Type.GetType("System.String"))
        dtResult.Columns.Add("EdiUsrNM", Type.GetType("System.String"))
        dtResult.Columns.Add("SysTime", Type.GetType("System.DateTime"))

        '新しい行の作成
        drResult = dtResult.NewRow()
        drResult(0) = DBNull.Value
        drResult(1) = ""
        drResult(2) = ""
        drResult(3) = ""
        drResult(4) = ""
        drResult(5) = DBNull.Value

        'DataTableに保存
        dtResult.Rows.Add(drResult)

        Try

            'システム日付取得
            If sqlHBKB0901.SetSelectSysDateSql(Adapter, Cn) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "システム日付取得", Nothing, Adapter.SelectCommand)

            'SQLを実行し、結果を取得
            Adapter.Fill(dtSysDate)

            'SQLを作成
            If sqlHBKB0901.SelectIntroductLockSql(Adapter, Cn, intIntroductNmb) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "導入ロックテーブル取得", Nothing, Adapter.SelectCommand)

            'SQLを実行し、結果を取得
            Adapter.Fill(dtLockInfo)

            'ロック情報にサーバー日付を設定
            dtResult.Rows(0).Item("SysTime") = dtSysDate.Rows(0).Item("SysDate")
            '変更をコミット
            dtResult.AcceptChanges()

            '2行取得できた場合（ロック情報がある場合）、ロック情報にサーバー日付を不可
            If dtLockInfo.Rows.Count > 0 Then

                'ロック情報にサーバー日付を設定
                dtResult.Rows(0).Item("EdiTime") = dtLockInfo.Rows(0).Item("EdiTime")
                dtResult.Rows(0).Item("EdiGrpCD") = dtLockInfo.Rows(0).Item("EdiGrpCD")
                dtResult.Rows(0).Item("EdiID") = dtLockInfo.Rows(0).Item("EdiID")
                dtResult.Rows(0).Item("EdiGroupNM") = dtLockInfo.Rows(0).Item("GroupNM")
                dtResult.Rows(0).Item("EdiUsrNM") = dtLockInfo.Rows(0).Item("HBKUsrNM")

                '変更をコミット
                dtResult.AcceptChanges()
            End If

            ''2行取得できた場合（ロック情報がある場合）、ロック情報にサーバー日付を付加
            'If dtResult.Rows.Count > 1 Then
            '    'ロック情報にサーバー日付を設定
            '    dtResult.Rows(1).Item("SysTime") = dtResult.Rows(0).Item("SysTime")
            '    '1行目のデータを削除
            '    dtResult.Rows(0).Delete()
            '    '変更をコミット
            '    dtResult.AcceptChanges()
            'End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Adapter.SelectCommand)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            dtSysDate.Dispose()
            dtLockInfo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 導入ロック処理
    ''' </summary>
    ''' <param name="intIntroductNmb">[IN]導入番号</param>
    ''' <param name="dtIntroductLock">[IN/OUT]導入ロックテーブルデータ格納テーブル</param>
    ''' <param name="blnDoUnlock">[IN]解除実行フラグ（True：解除してからロックする）※省略可</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>導入番号をキーに導入ロックテーブルのデータをINSERTする
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function LockIntroduct(ByVal intIntroductNmb As Integer, _
                                  ByRef dtIntroductLock As DataTable, _
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

            'ロック解除実行フラグがONの場合、導入ロックテーブルデータを削除
            If blnDoUnlock = True Then
                If DeleteIntroductLock(Cn, intIntroductNmb) = False Then
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    Return False
                End If
            End If

            '導入ロックテーブル登録
            If InsertIntroductLock(Cn, intIntroductNmb) = False Then
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '導入ロックテーブル取得
            If SelectIntroductLock(Adapter, Cn, intIntroductNmb, dtIntroductLock) = False Then
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
    ''' ロック解除処理
    ''' </summary>
    ''' <param name="intIntroductNmb">[IN]導入番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>CI共通情報のロックを解除する
    ''' <para>作成情報：2012/06/27 t.fukuo
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function UnlockIntroductInfo(ByVal intIntroductNmb As Integer) As Boolean

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

            'CI共通情報ロックテーブル削除処理
            If DeleteIntroductLock(Cn, intIntroductNmb) = False Then
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
    ''' 導入ロックテーブル削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intIntroductNmb">[IN]導入番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>導入番号をキーに導入ロックテーブルのデータを物理削除（DELETE）する
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function DeleteIntroductLock(ByVal Cn As NpgsqlConnection, _
                                        ByVal intIntroductNmb As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0901.DeleteIntroductLockSql(Cmd, Cn, intIntroductNmb) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "導入ロックテーブル削除", Nothing, Cmd)

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
    ''' 導入ロックテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intIntroductNmb">[IN]導入番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>導入ロックテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function InsertIntroductLock(ByVal Cn As NpgsqlConnection, _
                                         ByVal intIntroductNmb As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0901.InsertIntroductLockSql(Cmd, Cn, intIntroductNmb) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "導入ロックテーブル登録", Nothing, Cmd)

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



End Class
