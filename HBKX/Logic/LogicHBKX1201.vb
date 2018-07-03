Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' イメージマスター登録画面ロジッククラス
''' </summary>
''' <remarks>イメージマスター登録画面のロジックを定義したクラス
''' <para>作成情報：2012/09/04 k.ueda
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKX1201

    'インスタンス作成
    Private sqlHBKX1201 As New SqlHBKX1201
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    'Public定数宣言==============================================

    '初期表示用イメージマスター
    Public Const IMAGE_IMAGE_NMB As Integer = 0                'イメージ番号
    Public Const IMAGE_IMAGE_NM As Integer = 1                 'イメージ名称
    Public Const IMAGE_KIND As Integer = 2                     '種別
    Public Const IMAGE_MAKER As Integer = 3                    'メーカー
    Public Const IMAGE_KISYU_NM As Integer = 4                 '機種名
    Public Const IMAGE_OS_NM As Integer = 5                    'OS
    Public Const IMAGE_SP As Integer = 6                       'SP
    Public Const IMAGE_TYPE As Integer = 7                     'タイプ
    Public Const IMAGE_NOTES As Integer = 8                    '削除フラグ
    Public Const IMAGE_JTI_FLG As Integer = 9


    ''' <summary>
    ''' 【共通】システムエラー事前対応処理
    ''' </summary>
    ''' <param name="dataHBKX1201">[IN/OUT]イメージマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システムエラー発生時に非活性にするコントロールリストを作成する
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DoProcForErrorMain(ByRef dataHBKX1201 As DataHBKX1201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'トランザクション系コントロールリスト作成
        If CreateTsxCtlList(dataHBKX1201) = False Then
            Return False
        End If

        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】トランザクション系コントロールリスト作成処理（システムエラー対応）
    ''' </summary>
    ''' <param name="dataHBKX1201">[IN/OUT]イメージマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>トランザクション系コントロールのリストを作成する
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateTsxCtlList(ByRef dataHBKX1201 As DataHBKX1201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim aryCtlList As New ArrayList

        Try
            With dataHBKX1201

                'トランザクション系のコントロールをリストに追加
                aryCtlList.Add(.PropBtnReg)              '登録ボタン
                aryCtlList.Add(.PropBtnDelete)           '削除ボタン
                aryCtlList.Add(.PropBtnDeleteKaijyo)     '削除解除ボタン

                ''データクラスに作成リストをセット
                .PropAryTsxCtlList = aryCtlList

            End With


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
        End Try

    End Function

    ''' <summary>
    ''' 画面初期表示メイン処理
    ''' </summary>
    ''' <param name="dataHBKX1201">[IN/OUT]イメージマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>イメージマスター登録画面呼出時に初期データをセットする
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKX1201 As DataHBKX1201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '編集モードの場合は初期表示用データ取得
        If dataHBKX1201.PropStrProcMode = PROCMODE_EDIT Then
            '初期表示用データ取得
            If GetInitData(dataHBKX1201) = False Then
                Return False
            End If

        End If

        'フォームオブジェクト設定
        If SetFormObj(dataHBKX1201) = False Then
            Return False
        End If

        '編集モードの場合は初期表示用データ設定
        If dataHBKX1201.PropStrProcMode = PROCMODE_EDIT Then
            '初期表示用データ設定
            If SetInitData(dataHBKX1201) = False Then
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
    ''' <param name="dataHBKX1201">[IN/OUT]イメージマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKX1201 As DataHBKX1201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)



        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try
            'コネクションを開く
            Cn.Open()

            'イメージマスターデータ取得
            If GetImageMastarData(Adapter, Cn, dataHBKX1201) = False Then
                Return False
            End If

            'コネクションを閉じる
            Cn.Close()


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

    ''' <summary>
    ''' イメージマスターデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKX1201">[IN/OUT]イメージマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>イメージマスターデータを取得する
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetImageMastarData(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKX1201 As DataHBKX1201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言

        Dim dtImageMtb As New DataTable


        Try


            'イメージマスターデータ取得

            'イメージマスターデータ取得用SQLの作成・設定
            If SqlHBKX1201.SetSelectImageMasterSql(Adapter, Cn, dataHBKX1201) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "イメージマスターデータ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtImageMtb)

            ''取得データをデータクラスにセット
            dataHBKX1201.PropDtImageMaster = dtImageMtb

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
            dtImageMtb.Dispose()

        End Try

    End Function


    ''' <summary>
    ''' フォームオブジェクト設定処理
    ''' </summary>
    ''' <param name="dataHBKX1201">[IN/OUT]イメージマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/09/03 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormObj(ByRef dataHBKX1201 As DataHBKX1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'フォームオブジェクト設定共通処理
            If CommonSetFormObj(dataHBKX1201) = False Then
                Return False
            End If

            'モードによって初期表示を判定
            With dataHBKX1201
                If .PropStrProcMode = PROCMODE_NEW Then

                    '新規モード
                    If SetFormObjNew(dataHBKX1201) = False Then
                        Return False
                    End If

                Else
                    '編集モード
                    If SetFormObjEdi(dataHBKX1201) = False Then
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
    ''' <param name="dataHBKX1201">[IN/OUT]イメージマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>どのモードでも共通のフォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CommonSetFormObj(ByRef dataHBKX1201 As DataHBKX1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)



        Try

            'オブジェクトの活性非活性設定

            With dataHBKX1201.PropGrpLoginUser

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
    ''' <param name="dataHBKX1201">[IN/OUT]イメージマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規モード時のフォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormObjNew(ByRef dataHBKX1201 As DataHBKX1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX1201

                'オブジェクトの活性非活性設定

                '登録ボタン活性化
                .PropBtnReg.Visible = True

                '削除ボタン非活性化
                .PropBtnDelete.Visible = False

                '削除解除ボタン非活性化
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
    ''' <param name="dataHBKX1201">[IN/OUT]イメージマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モード時のフォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormObjEdi(ByRef dataHBKX1201 As DataHBKX1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try


            '未削除データか削除データかで表示する画面を切り替え

            With dataHBKX1201

                '未削除データの場合
                If .PropDtImageMaster.Rows(0).Item(IMAGE_JTI_FLG) = DELETE_MODE_YUKO Then

                    If SetFormObjEdiYUKO(dataHBKX1201) = False Then
                        Return False
                    End If


                    '削除データの場合
                ElseIf .PropDtImageMaster.Rows(0).Item(IMAGE_JTI_FLG) = DELETE_MODE_MUKO Then

                    If SetFormObjEdiMUKO(dataHBKX1201) = False Then
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
    ''' <param name="dataHBKX1201">[IN/OUT]イメージマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モード時(未削除データ)のフォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormObjEdiYUKO(ByRef dataHBKX1201 As DataHBKX1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try


            With dataHBKX1201

                'テキストボックスの設定
                'イメージ名称テキストボックス活性
                .PropTxtImageNM.ReadOnly = False
                '種別テキストボックス活性
                .PropTxtKind.ReadOnly = False
                'メーカーテキストボックス活性
                .PropTxtMaker.ReadOnly = False
                '機種名テキストボックス活性
                .PropTxtKisyuNM.ReadOnly = False
                'OSテキストボックス活性
                .PropTxtOSNM.ReadOnly = False
                'SPテキストボックス活性
                .PropTxtSP.ReadOnly = False
                'タイプテキストボックス活性
                .PropTxtType.ReadOnly = False
                '注意テキストボックス活性
                .PropTxtNotes.ReadOnly = False

                'ボタンの設定
                '登録ボタン表示
                .PropBtnReg.Visible = True
                '削除ボタン表示
                .PropBtnDelete.Visible = True
                '削除解除ボタン非表示
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
    ''' <param name="dataHBKX1201">[IN/OUT]イメージマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モード時(削除データ)のフォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormObjEdiMUKO(ByRef dataHBKX1201 As DataHBKX1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try

            With dataHBKX1201

                'テキストボックスの設定
                'イメージ名称テキストボックス非活性
                .PropTxtImageNM.ReadOnly = True
                '種別テキストボックス非活性
                .PropTxtKind.ReadOnly = True
                'メーカーテキストボックス非活性
                .PropTxtMaker.ReadOnly = True
                '機種名テキストボックス非活性
                .PropTxtKisyuNM.ReadOnly = True
                'OSテキストボックス非活性
                .PropTxtOSNM.ReadOnly = True
                'SPテキストボックス非活性
                .PropTxtSP.ReadOnly = True
                'タイプテキストボックス非活性
                .PropTxtType.ReadOnly = True
                '注意テキストボックス非活性
                .PropTxtNotes.ReadOnly = True

                'ボタンの設定
                '登録ボタン表示
                .PropBtnReg.Visible = False
                '削除ボタン表示
                .PropBtnDelete.Visible = False
                '削除解除ボタン非表示
                .PropBtnDeleteKaijyo.Visible = True

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True


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
    ''' <param name="dataHBKX1201">[IN/OUT]イメージマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示用データをフォームオブジェクトに設定する
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitData(ByRef dataHBKX1201 As DataHBKX1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try

            'テキストボックスに初期表示用データを設定する
            With dataHBKX1201


                'イメージ番号テキストボックス
                .PropTxtImageNmb.Text = .PropDtImageMaster.Rows(0).Item(IMAGE_IMAGE_NMB)

                'イメージ名称テキストボックス
                .PropTxtImageNM.Text = .PropDtImageMaster.Rows(0).Item(IMAGE_IMAGE_NM)

                '種別テキストボックス
                .PropTxtKind.Text = .PropDtImageMaster.Rows(0).Item(IMAGE_KIND)

                .PropTxtMaker.Text = .PropDtImageMaster.Rows(0).Item(IMAGE_MAKER)

                '機種名テキストボックス
                .PropTxtKisyuNM.Text = .PropDtImageMaster.Rows(0).Item(IMAGE_KISYU_NM)

                'OSテキストボックス
                .PropTxtOSNM.Text = .PropDtImageMaster.Rows(0).Item(IMAGE_OS_NM)

                'SPテキストボックス
                .PropTxtSP.Text = .PropDtImageMaster.Rows(0).Item(IMAGE_SP)

                'タイプテキストボックス
                .PropTxtType.Text = .PropDtImageMaster.Rows(0).Item(IMAGE_TYPE)

                '注意テキストボックス
                .PropTxtNotes.Text = .PropDtImageMaster.Rows(0).Item(IMAGE_NOTES)

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True


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
    ''' <param name="dataHBKX1201">[IN/OUT]イメージマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力された値のチェックを行う
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InputCheckMain(ByRef dataHBKX1201 As DataHBKX1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '入力エラーチェック
        If RegCheck(dataHBKX1201) = False Then
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
    ''' <param name="dataHBKX1201">[IN/OUT]イメージマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたデータが正しいかチェックする
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RegCheck(ByRef dataHBKX1201 As DataHBKX1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try


            'iイメージ名称入力チェック
            With dataHBKX1201.PropTxtImageNM
                '必須チェック
                If .Text.Trim = Nothing Then
                    'エラーメッセージ設定
                    puErrMsg = X1201_E001
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
    ''' <param name="dataHBKX1201">[IN/OUT]イメージマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたデータを登録及び更新する
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function RegisterMain(ByRef dataHBKX1201 As DataHBKX1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'システム日付取得
        If GetSysDate(dataHBKX1201) = False Then
            Return False
        End If

        '登録/編集実行
        If RegisterEdit(dataHBKX1201) = False Then
            Return False
        End If

        'イメージマスター登録画面初期表示メイン呼出
        If InitFormMain(dataHBKX1201) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' システム日付取得処理
    ''' </summary>
    ''' <param name="dataHBKX1201">[IN/OUT]イメージマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システム日付を取得する
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetSysDate(ByRef dataHBKX1201 As DataHBKX1201) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim dtSysDate As New DataTable

        Try

            'システム日付取得
            If sqlHBKX1201.SetSelectSysDateSql(Adapter, Cn, dataHBKX1201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "システム日付取得", Nothing, Adapter.SelectCommand)
            'データを取得
            Adapter.Fill(dtSysDate)

            'データが取得できた場合、データクラスにサーバー日付をセット
            If dtSysDate.Rows.Count > 0 Then
                dataHBKX1201.PropDtmSysDate = dtSysDate.Rows(0).Item("SysDate")
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
    ''' <param name="dataHBKX1201">[IN/OUT]イメージマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>モードごとに登録及び編集処理を行う
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RegisterEdit(ByRef dataHBKX1201 As DataHBKX1201) As Boolean

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
            If dataHBKX1201.PropStrProcMode = PROCMODE_NEW Then

                '新規イメージ番号取得処理
                If SelectNewImageNmb(Cn, dataHBKX1201) = False Then
                    'ロールバック
                    Tsx.Rollback()
                    Return False
                End If

                '登録処理
                If Register(Cn, dataHBKX1201) = False Then
                    'ロールバック
                    Tsx.Rollback()
                    Return False
                End If

            ElseIf dataHBKX1201.PropStrProcMode = PROCMODE_EDIT Then

                '編集処理
                If Edit(Cn, dataHBKX1201) = False Then
                    'ロールバック
                    Tsx.Rollback()
                    Return False
                End If

            End If

            'コミット
            Tsx.Commit()

            'コネクションを閉じる
            Cn.Close()

            'モードが新規登録の場合は編集モードに設定する
            With dataHBKX1201

                If .PropStrProcMode = PROCMODE_NEW Then
                    'モードを編集モードに設定する
                    .PropStrProcMode = PROCMODE_EDIT

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
    ''' 新規イメージ番号取得処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKX1201">[IN]イメージマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番したイメージ番号を取得（SELECT）する
    ''' <para>作成情報：2012/09/05 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectNewImageNmb(ByVal Cn As NpgsqlConnection, _
                                                    ByRef dataHBKX1201 As DataHBKX1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtResult As New DataTable

        Try

            '新規イメージ番号取得（SELECT）用SQLを作成
            If sqlHBKX1201.SetSelectNewImageNmbSql(Adapter, Cn, dataHBKX1201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規イメージ番号取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            'データが取得できた場合、データクラスに取得データをセット
            If dtResult.Rows.Count > 0 Then
                dataHBKX1201.PropStrImageNmb = dtResult.Rows(0).Item("ImageNmb")      'イメージ番号
            Else
                '取得できなかったときはエラー
                puErrMsg = X1201_E002
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
    ''' 新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKX1201">[IN]イメージマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をイメージマスターテーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function Register(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKX1201 As DataHBKX1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'イメージマスター新規登録（INSERT）用SQLを作成
            If sqlHBKX1201.SetInsertImageMasterSql(Cmd, Cn, dataHBKX1201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "イメージマスター新規登録", Nothing, Cmd)

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
    ''' <param name="dataHBKX1201">[IN]イメージマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でイメージマスターテーブルを編集（UPDATE）する
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function Edit(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKX1201 As DataHBKX1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'イメージマスター編集（UPDATE）用SQLを作成
            If sqlHBKX1201.SetUpdateImageMasterSql(Cmd, Cn, dataHBKX1201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "イメージマスター編集", Nothing, Cmd)

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
    ''' <param name="dataHBKX1201">[IN/OUT]イメージマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>指定されたデータを論理削除する
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function DeleteMain(ByRef dataHBKX1201 As DataHBKX1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'システム日付取得
        If GetSysDate(dataHBKX1201) = False Then
            Return False
        End If


        '削除実行
        If Delete(dataHBKX1201) = False Then
            Return False
        End If

        'イメージマスター登録画面初期表示メイン呼出
        If InitFormMain(dataHBKX1201) = False Then
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
    ''' <param name="dataHBKX1201">[IN/OUT]イメージマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>指定されたデータの論理削除を行う
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function Delete(ByRef dataHBKX1201 As DataHBKX1201) As Boolean

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

            'イメージマスター論理削除（UPDATE）用SQLを作成
            If sqlHBKX1201.SetDeleteImageMasterSql(Cmd, Cn, dataHBKX1201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "イメージマスター削除", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
    ''' <param name="dataHBKX1201">[IN/OUT]イメージマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>指定されたデータの論理削除を解除する
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function UnDroppingMain(ByRef dataHBKX1201 As DataHBKX1201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'システム日付取得
        If GetSysDate(dataHBKX1201) = False Then
            Return False
        End If

        '削除解除実行
        If UnDropping(dataHBKX1201) = False Then
            Return False
        End If

        'イメージマスター登録画面初期表示メイン呼出
        If InitFormMain(dataHBKX1201) = False Then
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
    ''' <param name="dataHBKX1201">[IN/OUT]イメージマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>指定されたデータの論理削除を解除する
    ''' <para>作成情報：2012/09/04 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UnDropping(ByRef dataHBKX1201 As DataHBKX1201) As Boolean

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

            'イメージマスター論理削除解除（UPDATE）用SQLを作成
            If sqlHBKX1201.SetUnDroppingImageMasterSql(Cmd, Cn, dataHBKX1201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "イメージマスター削除解除", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
