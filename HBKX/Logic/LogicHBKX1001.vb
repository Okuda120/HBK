Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' ソフトマスター登録画面ロジッククラス
''' </summary>
''' <remarks>ソフトマスター登録画面のロジックを定義したクラス
''' <para>作成情報：2012/08/30 k.ueda
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKX1001

    'インスタンス作成
    Private sqlHBKX1001 As New SqlHBKX1001
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK
    Private commonValidation As New CommonValidation


    'Public定数宣言==============================================

    '初期表示用ソフトマスター
    Public Const SOFT_SOFT_CD As Integer = 0                'ソフトCD
    Public Const SOFT_SOFT_KBN As Integer = 1               'ソフト区分
    Public Const SOFT_SOFT_NM As Integer = 2                'ソフト名称
    Public Const SOFT_JTI_FLG As Integer = 3                '削除フラグ

    ''' <summary>
    ''' 【共通】システムエラー事前対応処理
    ''' </summary>
    ''' <param name="dataHBKX1001">[IN/OUT]ソフトマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システムエラー発生時に非活性にするコントロールリストを作成する
    ''' <para>作成情報：2012/08/30 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DoProcForErrorMain(ByRef dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'トランザクション系コントロールリスト作成
        If CreateTsxCtlList(dataHBKX1001) = False Then
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
    ''' <param name="dataHBKX1001">[IN/OUT]ソフトマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>トランザクション系コントロールのリストを作成する
    ''' <para>作成情報：2012/08/30 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateTsxCtlList(ByRef dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim aryCtlList As New ArrayList

        Try
            With dataHBKX1001

                'トランザクション系のコントロールをリストに追加
                aryCtlList.Add(.PropBtnReg)              '登録ボタン
                aryCtlList.Add(.PropBtnDelete)           '削除ボタン
                aryCtlList.Add(.PropBtnDeleteKaijyo)     '削除解除ボタン

                ''データクラスに作成リストをセット
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
    ''' <param name="dataHBKX1001">[IN/OUT]ソフトマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ソフトマスター登録画面呼出時に初期データをセットする
    ''' <para>作成情報：2012/08/30 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '編集モードの場合は初期表示用データ取得
        If dataHBKX1001.PropStrProcMode = PROCMODE_EDIT Then
            '初期表示用データ取得
            If GetInitData(dataHBKX1001) = False Then
                Return False
            End If

        End If

        'フォームオブジェクト設定
        If SetFormObj(dataHBKX1001) = False Then
            Return False
        End If

        '編集モードの場合は初期表示用データ設定
        If dataHBKX1001.PropStrProcMode = PROCMODE_EDIT Then
            '初期表示用データ設定
            If SetInitData(dataHBKX1001) = False Then
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
    ''' <param name="dataHBKX1001">[IN/OUT]ソフトマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/08/30 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)



        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try
            'コネクションを開く
            Cn.Open()

            'ソフトマスターデータ取得
            If GetSoftMastarData(Adapter, Cn, dataHBKX1001) = False Then
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
    ''' ソフトマスターデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKX1001">[IN/OUT]ソフトマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ソフトマスタデータを取得する
    ''' <para>作成情報：2012/08/30 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetSoftMastarData(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言

        Dim dtSoftMtb As New DataTable


        Try


            'ソフトマスターデータ取得

            'ソフトマスターデータ取得用SQLの作成・設定
            If SqlHBKX1001.SetSelectSoftMasterSql(Adapter, Cn, dataHBKX1001) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ソフトマスターデータ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtSoftMtb)

            '取得データをデータクラスにセット
            dataHBKX1001.PropDtSoftMaster = dtSoftMtb

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            dtSoftMtb.Dispose()

        End Try

    End Function

    ''' <summary>
    ''' フォームオブジェクト設定処理
    ''' </summary>
    ''' <param name="dataHBKX1001">[IN/OUT]ソフトマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/08/30 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormObj(ByRef dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'フォームオブジェクト設定共通処理
            If CommonSetFormObj(dataHBKX1001) = False Then
                Return False
            End If

            'モードによって初期表示を判定
            With dataHBKX1001
                If .PropStrProcMode = PROCMODE_NEW Then

                    '新規モード
                    If SetFormObjNew(dataHBKX1001) = False Then
                        Return False
                    End If

                Else
                    '編集モード
                    If SetFormObjEdi(dataHBKX1001) = False Then
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
    ''' <param name="dataHBKX1001">[IN/OUT]ソフトマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>どのモードでも共通のフォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/08/30 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CommonSetFormObj(ByRef dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)



        Try

            'オブジェクトの活性非活性設定

            With dataHBKX1001.PropGrpLoginUser

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
    ''' <param name="dataHBKX1001">[IN/OUT]ソフトマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規モード時のフォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/08/30 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormObjNew(ByRef dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX1001

                'オブジェクトの活性非活性設定

                '登録ボタン活性化
                .PropBtnReg.Visible = True

                '削除ボタン非活性化
                .PropBtnDelete.Visible = False

                '削除解除ボタン非活性化
                .PropBtnDeleteKaijyo.Visible = False

                'ラジオボタンの設定
                'OSラジオボタンを選択
                .PropRdoOS.Checked = True
            
            End With
            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True


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
    ''' <param name="dataHBKX1001">[IN/OUT]ソフトマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モード時のフォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/08/30 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormObjEdi(ByRef dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try


            '未削除データか削除データかで表示する画面を切り替え

            With dataHBKX1001

                '未削除データの場合
                If .PropDtSoftMaster.Rows(0).Item(SOFT_JTI_FLG) = DELETE_MODE_YUKO Then

                    If SetFormObjEdiYUKO(dataHBKX1001) = False Then
                        Return False
                    End If


                    '削除データの場合
                ElseIf .PropDtSoftMaster.Rows(0).Item(SOFT_JTI_FLG) = DELETE_MODE_MUKO Then

                    If SetFormObjEdiMUKO(dataHBKX1001) = False Then
                        Return False
                    End If

                End If

                '編集モード共通設定処理
                If CommonSetFormObjEdi(dataHBKX1001) = False Then
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
    ''' 編集モードフォームオブジェクト設定処理(未削除データ)
    ''' </summary>
    ''' <param name="dataHBKX1001">[IN/OUT]ソフトマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モード時(未削除データ)のフォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/08/30 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormObjEdiYUKO(ByRef dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try

           
            With dataHBKX1001

                'テキストボックスの設定
                'ソフトコードテキストボックス非活性
                .PropTxtSoftCD.ReadOnly = True
                'ソフト名称テキストボックス活性
                .PropTxtSoftNM.ReadOnly = False

                'ボタンの設定
                '登録ボタン表示
                .PropBtnReg.Visible = True
                '削除ボタン表示
                .PropBtnDelete.Visible = True
                '削除解除ボタン非表示
                .PropBtnDeleteKaijyo.Visible = False

                'グループボックスの設定
                'ソフト区分グループボックス活性
                .PropGrpSoftKbn.Enabled = True

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True


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
    ''' <param name="dataHBKX1001">[IN/OUT]ソフトマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モード時(削除データ)のフォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/08/30 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormObjEdiMUKO(ByRef dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try

            With dataHBKX1001

                'テキストボックスの設定
                'ソフトコードテキストボックス非活性
                .PropTxtSoftCD.ReadOnly = True
                'ソフト名称テキストボックス非活性
                .PropTxtSoftNM.ReadOnly = True

                'ボタンの設定
                '登録ボタン表示
                .PropBtnReg.Visible = False
                '削除ボタン表示
                .PropBtnDelete.Visible = False
                '削除解除ボタン非表示
                .PropBtnDeleteKaijyo.Visible = True

                'グループボックスの設定
                'ソフト区分グループボックス活性
                .PropGrpSoftKbn.Enabled = False

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True


        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False

        End Try

    End Function

    ''' <summary>
    ''' 編集モードフォームオブジェクト共通設定処理
    ''' </summary>
    ''' <param name="dataHBKX1001">[IN/OUT]ソフトマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モード時の共通フォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/08/31 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CommonSetFormObjEdi(ByRef dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try

            With dataHBKX1001
                'ラジオボタンの設定
                '取得したソフト区分でどのラジオボタンにチェックするか判断する

                If .PropDtSoftMaster.Rows(0).Item(SOFT_SOFT_KBN) = SOFTKBN_OS Then
                    'OSを選択
                    .PropRdoOS.Checked = True
                ElseIf .PropDtSoftMaster.Rows(0).Item(SOFT_SOFT_KBN) = SOFTKBN_OPTIONSOFT Then
                    'オプションソフトを選択
                    .PropRdoOptSoft.Checked = True
                ElseIf .PropDtSoftMaster.Rows(0).Item(SOFT_SOFT_KBN) = SOFTKBN_UNTIVIRUSSOFT Then
                    'ウイルス対策ソフトを選択
                    .PropRdoAntiVirus.Checked = True
                End If
            End With
            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True


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
    ''' <param name="dataHBKX1001">[IN/OUT]ソフトマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示用データをフォームオブジェクトに設定する
    ''' <para>作成情報：2012/08/30 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitData(ByRef dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try

            'テキストボックスに初期表示用データを設定する
            With dataHBKX1001
                If .PropDtSoftMaster.Rows(0).Item(SOFT_SOFT_CD) IsNot Nothing Then
                    'ソフトコードテキストボックス
                    .PropTxtSoftCD.Text = .PropDtSoftMaster.Rows(0).Item(SOFT_SOFT_CD)
                End If
                If .PropDtSoftMaster.Rows(0).Item(SOFT_SOFT_NM) <> "" Then
                    'ソフト名称テキストボックス
                    .PropTxtSoftNM.Text = .PropDtSoftMaster.Rows(0).Item(SOFT_SOFT_NM)
                End If
               

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True


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
    ''' <param name="dataHBKX1001">[IN/OUT]ソフトマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力された値のチェックを行う
    ''' <para>作成情報：2012/08/30 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InputCheckMain(ByRef dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '入力エラーチェック
        If RegCheck(dataHBKX1001) = False Then
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
    ''' <param name="dataHBKX1001">[IN/OUT]ソフトマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたデータが正しいかチェックする
    ''' <para>作成情報：2012/08/30 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RegCheck(ByRef dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '新規モードと編集モードでチェックする項目をわける
            With dataHBKX1001
                If .PropStrProcMode = PROCMODE_NEW Then
                    '新規モード入力チェック
                    If RegCheckNew(dataHBKX1001) = False Then
                        Return False
                    End If
                ElseIf .PropStrProcMode = PROCMODE_EDIT Then
                    '編集モード入力チェック
                    If RegCheckEdi(dataHBKX1001) = False Then
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
    ''' 新規モード登録チェック処理
    ''' </summary>
    ''' <param name="dataHBKX1001">[IN/OUT]ソフトマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたデータが正しいかチェックする
    ''' <para>作成情報：2012/08/30 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RegCheckNew(ByRef dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'ソフトコードチェック
            If SoftCDCheck(dataHBKX1001) = False Then
                Return False
            End If

            'ソフト名称チェック
            If SoftNMCheck(dataHBKX1001) = False Then
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
    ''' 編集モード登録チェック処理
    ''' </summary>
    ''' <param name="dataHBKX1001">[IN/OUT]ソフトマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたデータが正しいかチェックする
    ''' <para>作成情報：2012/08/30 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RegCheckEdi(ByRef dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'ソフト名称チェック
            If SoftNMCheck(dataHBKX1001) = False Then
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
    ''' ソフトコードチェック処理
    ''' </summary>
    ''' <param name="dataHBKX1001">[IN/OUT]ソフトマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたデータが正しいかチェックする
    ''' <para>作成情報：2012/08/30 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SoftCDCheck(ByRef dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKX1001.PropTxtSoftCD
                '必須チェック
                If .Text.Trim = Nothing Then
                    'エラーメッセージ設定
                    puErrMsg = X1001_E001
                    'フォーカス設定
                    .Focus()
                    .SelectAll()
                    Return False
                End If


                '形式チェック
                If commonValidation.IsHalfNmb(.Text) = False Then
                    'エラーメッセージ設定
                    puErrMsg = X1001_E002
                    'フォーカス設定
                    .Focus()
                    .SelectAll()
                    Return False
                End If

                '存在チェック
                '存在チェック用ソフトCD取得
                If SonzaiCheck(dataHBKX1001) = False Then
                    Return False
                End If
                '存在チェック実行
                If dataHBKX1001.PropDtSoftCD.Rows(0).Item(0) <> 0 Then
                    'エラーメッセージ設定
                    puErrMsg = X1001_E003
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
    ''' ソフト名称チェック処理
    ''' </summary>
    ''' <param name="dataHBKX1001">[IN/OUT]ソフトマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたデータが正しいかチェックする
    ''' <para>作成情報：2012/08/30 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SoftNMCheck(ByRef dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX1001.PropTxtSoftNM
                '必須チェック
                If .Text.Trim = Nothing Then
                    'エラーメッセージ設定
                    puErrMsg = X1001_E004
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
    ''' 存在チェック用ソフトCD取得処理
    ''' </summary>
    ''' <param name="dataHBKX1001">[IN/OUT]ソフトマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたソフトコードがDBに存在するかチェックするためのデータを取得する
    ''' <para>作成情報：2012/08/31 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SonzaiCheck(ByRef dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)



        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtSoftCD As New DataTable

        Try


            'ソフトCD取得

            'ソフトCD取得用SQLの作成・設定
            If sqlHBKX1001.SetSelectSoftCDSql(Adapter, Cn, dataHBKX1001) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ソフトCD取得取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtSoftCD)

            '取得データをデータクラスにセット
            dataHBKX1001.PropDtSoftCD = dtSoftCD

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

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
            dtSoftCD.Dispose()
        End Try



    End Function

    ''' <summary>
    ''' 登録メイン処理
    ''' </summary>
    ''' <param name="dataHBKX1001">[IN/OUT]ソフトマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたデータを登録及び更新する
    ''' <para>作成情報：2012/08/30 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function RegisterMain(ByRef dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'システム日付取得
        If GetSysDate(dataHBKX1001) = False Then
            Return False
        End If

        '登録/編集実行
        If RegisterEdit(dataHBKX1001) = False Then
            Return False
        End If

        'ソフトマスター登録画面初期表示メイン呼出
        If InitFormMain(dataHBKX1001) = False Then
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
    ''' <param name="dataHBKX1001">[IN/OUT]ソフトマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システム日付を取得する
    ''' <para>作成情報：2012/08/30 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetSysDate(ByRef dataHBKX1001 As DataHBKX1001) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim dtSysDate As New DataTable

        Try

            'システム日付取得
            If sqlHBKX1001.SetSelectSysDateSql(Adapter, Cn, dataHBKX1001) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "システム日付取得", Nothing, Adapter.SelectCommand)
            'データを取得
            Adapter.Fill(dtSysDate)

            'データが取得できた場合、データクラスにサーバー日付をセット
            If dtSysDate.Rows.Count > 0 Then
                dataHBKX1001.PropDtmSysDate = dtSysDate.Rows(0).Item("SysDate")
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
    ''' <param name="dataHBKX1001">[IN/OUT]ソフトマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>モードごとに登録及び編集処理を行う
    ''' <para>作成情報：2012/08/31 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RegisterEdit(ByRef dataHBKX1001 As DataHBKX1001) As Boolean

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

            '新規登録/更新に必要なデータをセットする
            If SetData(dataHBKX1001) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            '新規登録モードなら登録、編集モードなら編集を実行
            If dataHBKX1001.PropStrProcMode = PROCMODE_NEW Then

                '登録処理
                If Register(Cn, dataHBKX1001) = False Then
                    'ロールバック
                    Tsx.Rollback()
                    Return False
                End If

            ElseIf dataHBKX1001.PropStrProcMode = PROCMODE_EDIT Then

                '編集処理
                If Edit(Cn, dataHBKX1001) = False Then
                    'ロールバック
                    Tsx.Rollback()
                    Return False
                End If

            End If

            'コミット
            Tsx.Commit()

            'コネクションを閉じる
            Cn.Close()

            'モードが新規登録の場合は編集モードに設定して、ソフトCDをプロパティにセットする
            With dataHBKX1001

                If .PropStrProcMode = PROCMODE_NEW Then
                    'モードを編集モードに設定する
                    .PropStrProcMode = PROCMODE_EDIT
                    .PropIntSoftCD = .PropTxtSoftCD.Text

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
    ''' データセット処理
    ''' </summary>
    ''' <param name="dataHBKX1001">[IN/OUT]ソフトマスター登録画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録/更新に必要なデータをセットする
    ''' <para>作成情報：2012/08/31 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetData(dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'プロパティに値をセットする。
            With dataHBKX1001
                '選択されたラジオボタンによってセットする値を変更する

                If .PropRdoOS.Checked = True Then
                    'ソフト区分'001'(OS)を設定
                    .PropStrSoftKbn = SOFTKBN_OS
                ElseIf .PropRdoOptSoft.Checked = True Then
                    'ソフト区分'002'(オプションソフト)を設定
                    .PropStrSoftKbn = SOFTKBN_OPTIONSOFT
                ElseIf .PropRdoAntiVirus.Checked = True Then
                    'ソフト区分'003'(ウイルス対策ソフト)を設定
                    .PropStrSoftKbn = SOFTKBN_UNTIVIRUSSOFT
                End If

            End With



            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 + ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKX1001">[IN]ソフトマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をソフトマスターテーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/30 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function Register(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'ソフトマスター新規登録（INSERT）用SQLを作成
            If sqlHBKX1001.SetInsertSoftMasterSql(Cmd, Cn, dataHBKX1001) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ソフトマスター新規登録", Nothing, Cmd)

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
    ''' <param name="dataHBKX1001">[IN]ソフトマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でソフトマスターテーブルを編集（UPDATE）する
    ''' <para>作成情報：2012/08/30 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function Edit(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'ソフトマスター編集（UPDATE）用SQLを作成
            If sqlHBKX1001.SetUpdateSoftMasterSql(Cmd, Cn, dataHBKX1001) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ソフトマスター編集", Nothing, Cmd)

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
    ''' <param name="dataHBKX1001">[IN/OUT]ソフトマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>指定されたデータを論理削除する
    ''' <para>作成情報：2012/08/30 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function DeleteMain(ByRef dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'システム日付取得
        If GetSysDate(dataHBKX1001) = False Then
            Return False
        End If


        '削除実行
        If Delete(dataHBKX1001) = False Then
            Return False
        End If

        'ソフトマスター登録画面初期表示メイン呼出
        If InitFormMain(dataHBKX1001) = False Then
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
    ''' <param name="dataHBKX1001">[IN/OUT]ソフトマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>指定されたデータの論理削除を行う
    ''' <para>作成情報：2012/08/31 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function Delete(ByRef dataHBKX1001 As DataHBKX1001) As Boolean

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

            'ソフトマスター論理削除（UPDATE）用SQLを作成
            If sqlHBKX1001.SetDeleteSoftMasterSql(Cmd, Cn, dataHBKX1001) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ソフトマスター削除", Nothing, Cmd)

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
    ''' <param name="dataHBKX1001">[IN/OUT]ソフトマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>指定されたデータの論理削除を解除する
    ''' <para>作成情報：2012/08/31 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function UnDroppingMain(ByRef dataHBKX1001 As DataHBKX1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'システム日付取得
        If GetSysDate(dataHBKX1001) = False Then
            Return False
        End If

        '削除解除実行
        If UnDropping(dataHBKX1001) = False Then
            Return False
        End If

        'ソフトマスター登録画面初期表示メイン呼出
        If InitFormMain(dataHBKX1001) = False Then
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
    ''' <param name="dataHBKX1001">[IN/OUT]ソフトマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>指定されたデータの論理削除を解除する
    ''' <para>作成情報：2012/08/31 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UnDropping(ByRef dataHBKX1001 As DataHBKX1001) As Boolean

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

            'ソフトマスター論理削除解除（UPDATE）用SQLを作成
            If sqlHBKX1001.SetUnDroppingSoftMasterSql(Cmd, Cn, dataHBKX1001) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ソフトマスター削除解除", Nothing, Cmd)

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
