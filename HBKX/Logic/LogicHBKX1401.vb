Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' 設置情報マスター登録画面ロジッククラス
''' </summary>
''' <remarks>設置情報マスター登録画面のロジックを定義したクラス
''' <para>作成情報：2012/09/05 k.imayama
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKX1401

    'インスタンス作成
    Private sqlHBKX1401 As New SqlHBKX1401
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK
    Private commonValidation As New CommonValidation

    ''' <summary>
    ''' 【共通】システムエラー事前対応処理
    ''' </summary>
    ''' <param name="dataHBKX1401">[IN/OUT]設置情報マスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システムエラー発生時に非活性にするコントロールリストを作成する
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DoProcForErrorMain(ByRef dataHBKX1401 As DataHBKX1401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'トランザクション系コントロールリスト作成
        If CreateTsxCtlList(dataHBKX1401) = False Then
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
    ''' <param name="dataHBKX1401">[IN/OUT]設置情報マスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>トランザクション系コントロールのリストを作成する
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateTsxCtlList(ByRef dataHBKX1401 As DataHBKX1401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim aryCtlList As New ArrayList

        Try
            With dataHBKX1401

                'トランザクション系のコントロールをリストに追加
                aryCtlList.Add(.PropBtnReg)              '登録ボタン
                aryCtlList.Add(.PropBtnDelete)           '削除ボタン
                aryCtlList.Add(.PropBtnDeleteKaijyo)     '削除解除ボタン

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
    ''' 画面初期表示メイン処理
    ''' </summary>
    ''' <param name="dataHBKX1401">[IN/OUT]設置情報マスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>設置情報マスター登録画面呼出時に初期データをセットする
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKX1401 As DataHBKX1401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '編集モードの場合は初期表示用データ取得
        If dataHBKX1401.PropStrProcMode = PROCMODE_EDIT Then
            '初期表示用データ取得
            If GetInitData(dataHBKX1401) = False Then
                Return False
            End If
        End If

        'フォームオブジェクト設定
        If SetFormObj(dataHBKX1401) = False Then
            Return False
        End If

        '編集モードの場合は初期表示用データ設定
        If dataHBKX1401.PropStrProcMode = PROCMODE_EDIT Then
            '初期表示用データ設定
            If SetInitData(dataHBKX1401) = False Then
                Return False
            End If
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 初期表示用データ取得処理
    ''' </summary>
    ''' <param name="dataHBKX1401">[IN/OUT]設置情報マスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKX1401 As DataHBKX1401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try
            'コネクションを開く
            Cn.Open()

            '設置情報マスターデータ取得
            If GetSetPosMastarData(Adapter, Cn, dataHBKX1401) = False Then
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
    ''' 設置情報マスターデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKX1401">[IN/OUT]設置情報マスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>設置情報マスタデータを取得する
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetSetPosMastarData(ByVal Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByRef dataHBKX1401 As DataHBKX1401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSetPosMtb As New DataTable

        Try
            '設置情報マスターデータ取得用SQLの作成・設定
            If sqlHBKX1401.SetSelectSetPosMasterSql(Adapter, Cn, dataHBKX1401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "設置情報マスターデータ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtSetPosMtb)

            '取得データをデータクラスにセット
            dataHBKX1401.PropDtSetPosMaster = dtSetPosMtb

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            dtSetPosMtb.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' フォームオブジェクト設定処理
    ''' </summary>
    ''' <param name="dataHBKX1401">[IN/OUT]設置情報マスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormObj(ByRef dataHBKX1401 As DataHBKX1401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'フォームオブジェクト設定共通処理
            If CommonSetFormObj(dataHBKX1401) = False Then
                Return False
            End If

            'モードによって初期表示を判定
            With dataHBKX1401

                If .PropStrProcMode = PROCMODE_NEW Then
                    '新規モード
                    If SetFormObjNew(dataHBKX1401) = False Then
                        Return False
                    End If
                Else
                    '編集モード
                    If SetFormObjEdi(dataHBKX1401) = False Then
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
    ''' フォームオブジェクト設定共通処理
    ''' </summary>
    ''' <param name="dataHBKX1401">[IN/OUT]設置情報マスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>どのモードでも共通のフォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CommonSetFormObj(ByRef dataHBKX1401 As DataHBKX1401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'オブジェクトの活性非活性設定
            With dataHBKX1401.PropGrpLoginUser

                'グループコンボボックス非活性
                .cmbGroup.Enabled = False

                '変更ボタン非活性
                .btnChange.Enabled = False

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 新規モードフォームオブジェクト設定処理
    ''' </summary>
    ''' <param name="dataHBKX1401">[IN/OUT]設置情報マスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規モード時のフォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormObjNew(ByRef dataHBKX1401 As DataHBKX1401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX1401

                'ボタンの設定
                .PropBtnReg.Visible = True
                .PropBtnDelete.Visible = False
                .PropBtnDeleteKaijyo.Visible = False

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 編集モードフォームオブジェクト設定処理
    ''' </summary>
    ''' <param name="dataHBKX1401">[IN/OUT]設置情報マスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モード時のフォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormObjEdi(ByRef dataHBKX1401 As DataHBKX1401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '有効データか削除データかで表示する画面を切り替え
            With dataHBKX1401

                '有効データの場合
                If .PropDtSetPosMaster.Rows(0).Item("JtiFlg") = DELETE_MODE_YUKO Then
                    If SetFormObjEdiYUKO(dataHBKX1401) = False Then
                        Return False
                    End If

                    '削除データの場合
                ElseIf .PropDtSetPosMaster.Rows(0).Item("JtiFlg") = DELETE_MODE_MUKO Then
                    If SetFormObjEdiMUKO(dataHBKX1401) = False Then
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
    ''' 編集モードフォームオブジェクト設定処理(未削除データ)
    ''' </summary>
    ''' <param name="dataHBKX1401">[IN/OUT]設置情報マスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モード時(未削除データ)のフォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormObjEdiYUKO(ByRef dataHBKX1401 As DataHBKX1401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX1401

                'テキストボックスの設定
                .PropTxtSetKyokuNM.ReadOnly = False
                .PropTxtSetBusyoNM.ReadOnly = False
                .PropTxtSetRoom.ReadOnly = False
                .PropTxtSetBuil.ReadOnly = False
                .PropTxtSetFloor.ReadOnly = False

                'ボタンの設定
                .PropBtnReg.Visible = True
                .PropBtnDelete.Visible = True
                .PropBtnDeleteKaijyo.Visible = False

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 編集モードフォームオブジェクト設定処理(削除データ)
    ''' </summary>
    ''' <param name="dataHBKX1401">[IN/OUT]設置情報マスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モード時(削除データ)のフォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormObjEdiMUKO(ByRef dataHBKX1401 As DataHBKX1401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX1401

                'テキストボックスの設定
                .PropTxtSetKyokuNM.ReadOnly = True
                .PropTxtSetBusyoNM.ReadOnly = True
                .PropTxtSetRoom.ReadOnly = True
                .PropTxtSetBuil.ReadOnly = True
                .PropTxtSetFloor.ReadOnly = True

                'ボタンの設定
                .PropBtnReg.Visible = False
                .PropBtnDelete.Visible = False
                .PropBtnDeleteKaijyo.Visible = True

                '非表示に合わせ、表示ボタンの位置を左に移動
                .PropBtnDeleteKaijyo.Location = .PropBtnReg.Location    '削除解除ボタンを登録ボタンの位置に

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 初期表示用データ設定処理
    ''' </summary>
    ''' <param name="dataHBKX1401">[IN/OUT]設置情報マスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示用データをフォームオブジェクトに設定する
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitData(ByRef dataHBKX1401 As DataHBKX1401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX1401

                .PropTxtSetBusyoCD.Text = .PropDtSetPosMaster.Rows(0).Item("SetBusyoCD")    '設置部署コード
                .PropTxtSetKyokuNM.Text = .PropDtSetPosMaster.Rows(0).Item("SetKyokuNM")    '局名
                .PropTxtSetBusyoNM.Text = .PropDtSetPosMaster.Rows(0).Item("SetBusyoNM")    '部署名
                .PropTxtSetRoom.Text = .PropDtSetPosMaster.Rows(0).Item("SetRoom")          '番組/部屋名
                .PropTxtSetBuil.Text = .PropDtSetPosMaster.Rows(0).Item("SetBuil")          '建物
                .PropTxtSetFloor.Text = .PropDtSetPosMaster.Rows(0).Item("SetFloor")        'フロア

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 入力チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKX1401">[IN/OUT]設置情報マスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力された値のチェックを行う
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InputCheckMain(ByRef dataHBKX1401 As DataHBKX1401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '入力エラーチェック
        If RegCheck(dataHBKX1401) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 登録チェック処理
    ''' </summary>
    ''' <param name="dataHBKX1401">[IN/OUT]設置情報マスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたデータが正しいかチェックする
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RegCheck(ByRef dataHBKX1401 As DataHBKX1401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '建物（必須）
            With dataHBKX1401.PropTxtSetBuil
                '必須チェック
                If .Text.Trim = Nothing Then
                    'エラーメッセージ設定
                    puErrMsg = X1401_E001
                    'フォーカス設定
                    .Focus()
                    .SelectAll()
                    Return False
                End If
            End With

            'フロア（必須）
            With dataHBKX1401.PropTxtSetFloor
                '必須チェック
                If .Text.Trim = Nothing Then
                    'エラーメッセージ設定
                    puErrMsg = X1401_E002
                    'フォーカス設定
                    .Focus()
                    .SelectAll()
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
    ''' 登録メイン処理
    ''' </summary>
    ''' <param name="dataHBKX1401">[IN/OUT]設置情報マスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたデータを登録及び更新する
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function RegisterMain(ByRef dataHBKX1401 As DataHBKX1401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'システム日付取得
        If GetSysDate(dataHBKX1401) = False Then
            Return False
        End If

        '登録/編集実行
        If RegisterEdit(dataHBKX1401) = False Then
            Return False
        End If

        '設置情報マスター登録画面初期表示メイン呼出
        If InitFormMain(dataHBKX1401) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【新規登録モード】新規設置所属コード取得処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKX1401">[IN]設置情報マスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番した設置所属コードを取得（SELECT）する
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetNewSetBusyoCD(ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKX1401 As DataHBKX1401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtResult As New DataTable

        Try
            '新規設置所属コード取得（SELECT）用SQLを作成
            If sqlHBKX1401.SetSelectNewSetBusyoCDAndSysDateSql(Adapter, Cn, dataHBKX1401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規設置所属コード取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            'データが取得できた場合、データクラスに取得データをセット
            If dtResult.Rows.Count > 0 Then
                dataHBKX1401.PropTxtSetBusyoCD.Text = dtResult.Rows(0).Item("SetBusyoCD")
            Else
                '取得できなかったときはエラー
                puErrMsg = X1401_E003
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
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' システム日付取得処理
    ''' </summary>
    ''' <param name="dataHBKX1401">[IN/OUT]設置情報マスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システム日付を取得する
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetSysDate(ByRef dataHBKX1401 As DataHBKX1401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim dtSysDate As New DataTable

        Try
            'システム日付取得
            If sqlHBKX1401.SetSelectSysDateSql(Adapter, Cn, dataHBKX1401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "システム日付取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtSysDate)

            'データが取得できた場合、データクラスにサーバー日付をセット
            If dtSysDate.Rows.Count > 0 Then
                dataHBKX1401.PropDtmSysDate = dtSysDate.Rows(0).Item("SysDate")
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
            dtSysDate.Dispose()
        End Try

    End Function

    ''' <summary>
    '''　登録/編集処理
    ''' </summary>
    ''' <param name="dataHBKX1401">[IN/OUT]設置情報マスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>モードごとに登録及び編集処理を行う
    ''' <para>作成情報：2012/08/31 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RegisterEdit(ByRef dataHBKX1401 As DataHBKX1401) As Boolean

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

            '新規登録モードなら登録、編集モードなら編集を実行
            If dataHBKX1401.PropStrProcMode = PROCMODE_NEW Then
                '新規設置所属コード取得
                If GetNewSetBusyoCD(Cn, dataHBKX1401) = False Then
                    'ロールバック
                    Tsx.Rollback()
                    Return False
                End If
                '登録処理
                If Register(Cn, dataHBKX1401) = False Then
                    'ロールバック
                    Tsx.Rollback()
                    Return False
                End If

            ElseIf dataHBKX1401.PropStrProcMode = PROCMODE_EDIT Then
                '編集処理
                If Edit(Cn, dataHBKX1401) = False Then
                    'ロールバック
                    Tsx.Rollback()
                    Return False
                End If
            End If

            'コミット
            Tsx.Commit()

            'モードが新規登録の場合は編集モードに設定して、設置部署CDをプロパティにセットする
            With dataHBKX1401
                If .PropStrProcMode = PROCMODE_NEW Then
                    'モードを編集モードに設定する
                    .PropStrProcMode = PROCMODE_EDIT
                    .PropIntSetBusyoCD = .PropTxtSetBusyoCD.Text
                End If
            End With

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
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Tsx.Dispose()
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKX1401">[IN]設置情報マスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容を設置情報マスターテーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function Register(ByVal Cn As NpgsqlConnection, _
                                ByVal dataHBKX1401 As DataHBKX1401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            '設置情報マスター新規登録（INSERT）用SQLを作成
            If sqlHBKX1401.SetInsertSoftMasterSql(Cmd, Cn, dataHBKX1401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "設置情報マスター新規登録", Nothing, Cmd)

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
    ''' 編集処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKX1401">[IN]設置情報マスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容で設置情報マスターテーブルを編集（UPDATE）する
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function Edit(ByVal Cn As NpgsqlConnection, _
                            ByVal dataHBKX1401 As DataHBKX1401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            '設置情報マスター編集（UPDATE）用SQLを作成
            If sqlHBKX1401.SetUpdateSoftMasterSql(Cmd, Cn, dataHBKX1401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "設置情報マスター編集", Nothing, Cmd)

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
    ''' 削除メイン処理
    ''' </summary>
    ''' <param name="dataHBKX1401">[IN/OUT]設置情報マスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>指定されたデータを論理削除する
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function DeleteMain(ByRef dataHBKX1401 As DataHBKX1401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'システム日付取得
        If GetSysDate(dataHBKX1401) = False Then
            Return False
        End If

        '削除実行
        If Delete(dataHBKX1401) = False Then
            Return False
        End If

        '設置情報マスター登録画面初期表示メイン呼出
        If InitFormMain(dataHBKX1401) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    '''　削除処理
    ''' </summary>
    ''' <param name="dataHBKX1401">[IN/OUT]設置情報マスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>指定されたデータの論理削除を行う
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function Delete(ByRef dataHBKX1401 As DataHBKX1401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Tsx As NpgsqlTransaction = Nothing    'トランザクション
        Dim Cmd As New NpgsqlCommand              'SQLコマンド

        Try
            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            '設置情報マスター論理削除（UPDATE）用SQLを作成
            If sqlHBKX1401.SetDeleteEndUsrMasterSql(Cmd, Cn, dataHBKX1401) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "設置情報マスター削除", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Tsx.Dispose()
            Cmd.Dispose()
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 削除解除メイン処理
    ''' </summary>
    ''' <param name="dataHBKX1401">[IN/OUT]設置情報マスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>指定されたデータの論理削除を解除する
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function UnDroppingMain(ByRef dataHBKX1401 As DataHBKX1401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'システム日付取得
        If GetSysDate(dataHBKX1401) = False Then
            Return False
        End If

        '削除解除実行
        If UnDropping(dataHBKX1401) = False Then
            Return False
        End If

        '設置情報マスター登録画面初期表示メイン呼出
        If InitFormMain(dataHBKX1401) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    '''　削除解除処理
    ''' </summary>
    ''' <param name="dataHBKX1401">[IN/OUT]設置情報マスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>指定されたデータの論理削除を解除する
    ''' <para>作成情報：2012/09/05 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UnDropping(ByRef dataHBKX1401 As DataHBKX1401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Tsx As NpgsqlTransaction = Nothing    'トランザクション
        Dim Cmd As New NpgsqlCommand              'SQLコマンド

        Try
            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            '設置情報マスター論理削除解除（UPDATE）用SQLを作成
            If sqlHBKX1401.SetUnDroppingSoftMasterSql(Cmd, Cn, dataHBKX1401) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "設置情報マスター削除解除", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Tsx.Dispose()
            Cmd.Dispose()
            Cn.Dispose()
        End Try

    End Function

End Class
