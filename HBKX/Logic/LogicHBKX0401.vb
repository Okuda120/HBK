Imports Common
Imports CommonHBK
Imports Npgsql
''' <summary>
''' エンドユーザーマスター登録画面ロジッククラス
''' </summary>
''' <remarks>エンドユーザーマスター登録画面のロジックを定義したクラス
''' <para>作成情報：2012/08/09 k.ueda
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKX0401

    'インスタンス作成
    Private sqlHBKX0401 As New SqlHBKX0401
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    'Public定数宣言==============================================

    '初期表示用エンドユーザーマスター
    Public Const END_USR_ID As Integer = 0                'ユーザーID
    Public Const USR_KBN As Integer = 1                   'ユーザー区分
    Public Const END_USR_SEI As Integer = 2               '姓
    Public Const END_USR_MEI As Integer = 3               '名
    Public Const END_USR_SEI_KANA As Integer = 4          '姓(カナ)
    Public Const END_USR_MEI_KANA As Integer = 5          '名(カナ)
    Public Const END_USR_COMPANY As Integer = 6           '所属会社
    Public Const END_USR_BUSYO_NM As Integer = 7          '部署名
    Public Const END_USR_TEL As Integer = 8               '電話番号
    Public Const END_USR_MAIL_ADD As Integer = 9          'メールアドレス
    Public Const STATE_NAIYO As Integer = 10              '有効/無効
    Public Const REG_KBN As Integer = 11                  '登録方法
    Public Const JTI_FLG As Integer = 12                  '削除フラグ



    ''' <summary>
    ''' 【共通】システムエラー事前対応処理
    ''' </summary>
    ''' <param name="dataHBKX0401">[IN/OUT]エンドユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システムエラー発生時に非活性にするコントロールリストを作成する
    ''' <para>作成情報：2012/08/09 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DoProcForErrorMain(ByRef dataHBKX0401 As DataHBKX0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'トランザクション系コントロールリスト作成
        If CreateTsxCtlList(dataHBKX0401) = False Then
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
    ''' <param name="dataHBKX0401">[IN/OUT]エンドユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>トランザクション系コントロールのリストを作成する
    ''' <para>作成情報：2012/08/09 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateTsxCtlList(ByRef dataHBKX0401 As DataHBKX0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim aryCtlList As New ArrayList

        Try
            With dataHBKX0401

                'トランザクション系のコントロールをリストに追加
                aryCtlList.Add(.PropBtnReg)              '登録ボタン

                'データクラスに作成リストをセット
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
    ''' <param name="dataHBKX0401">[IN/OUT]エンドユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>エンドユーザーマスター登録画面呼出時に初期データをセットする
    ''' <para>作成情報：2012/08/09 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKX0401 As DataHBKX0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '編集モードの場合は初期表示用データ取得
        If dataHBKX0401.PropStrProcMode = PROCMODE_EDIT Then
            '初期表示用データ取得
            If GetInitData(dataHBKX0401) = False Then
                Return False
            End If

        End If

        'フォームオブジェクト設定
        If SetFormObj(dataHBKX0401) = False Then
            Return False
        End If

        '編集モードの場合は初期表示用データ設定
        If dataHBKX0401.PropStrProcMode = PROCMODE_EDIT Then
            '初期表示用データ設定
            If SetInitData(dataHBKX0401) = False Then
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
    ''' <param name="dataHBKX0401">[IN/OUT]エンドユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/08/09 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKX0401 As DataHBKX0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)



        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try
            'コネクションを開く
            Cn.Open()

            'エンドユーザーマスターデータ取得
            If GetEndUsrMastarData(Adapter, Cn, dataHBKX0401) = False Then
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
    ''' エンドユーザーマスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKX0401">[IN/OUT]エンドユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>エンドユーザーマスタデータを取得する
    ''' <para>作成情報：2012/08/09 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetEndUsrMastarData(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKX0401 As DataHBKX0401) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言

        Dim dtEndUsrMtb As New DataTable

       
        Try
            

            'エンドユーザーマスターデータ取得

            'エンドユーザーマスターデータ取得用SQLの作成・設定
            If SqlHBKX0401.SetSelectEndUsrMasterSql(Adapter, Cn, dataHBKX0401) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "エンドユーザーマスターデータ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtEndUsrMtb)

            '取得データをデータクラスにセット
            dataHBKX0401.PropDtEndUsrMaster = dtEndUsrMtb

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
            dtEndUsrMtb.Dispose()

        End Try


    End Function

    ''' <summary>
    ''' フォームオブジェクト設定処理
    ''' </summary>
    ''' <param name="dataHBKX0401">[IN/OUT]エンドユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/08/09 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormObj(ByRef dataHBKX0401 As DataHBKX0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'フォームオブジェクト設定共通処理
            If CommonSetFormObj(dataHBKX0401) = False Then
                Return False
            End If

            'モードによって初期表示を判定
            With dataHBKX0401
                If .PropStrProcMode = PROCMODE_NEW Then

                    '新規モード
                    If SetFormObjNew(dataHBKX0401) = False Then
                        Return False
                    End If

                Else
                    '編集モード
                    If SetFormObjEdi(dataHBKX0401) = False Then
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
    ''' <param name="dataHBKX0401">[IN/OUT]エンドユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>どのモードでも共通のフォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/08/10 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CommonSetFormObj(ByRef dataHBKX0401 As DataHBKX0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'オブジェクトの活性非活性設定

            With dataHBKX0401.PropGrpLoginUser

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
    ''' <param name="dataHBKX0401">[IN/OUT]エンドユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規モード時のフォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/08/09 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormObjNew(ByRef dataHBKX0401 As DataHBKX0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX0401

                'オブジェクトの活性非活性設定

                '登録ボタン活性化
                .PropBtnReg.Visible = True

                'テキストボックスの文字設定


                '登録方法を｢画面入力｣に設定(固定)
                .PropTxtRegKbn.Text = REG_GAMEN_NM

            End With
            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True


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
    ''' <param name="dataHBKX0401">[IN/OUT]エンドユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モード時のフォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/08/09 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormObjEdi(ByRef dataHBKX0401 As DataHBKX0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '登録方法が画面入力か取込かで初期表示を切り替え(削除フラグによる判定は0:有効で固定)

            With dataHBKX0401

                '画面入力の場合
                If .PropDtEndUsrMaster.Rows(0).Item(REG_KBN) = REG_GAMEN_NM _
                    And .PropDtEndUsrMaster.Rows(0).Item(JTI_FLG) = JTIFLG_OFF Then

                    If SetFormObjEdiGamenUnDropping(dataHBKX0401) = False Then
                        Return False
                    End If


                    '取込の場合
                ElseIf .PropDtEndUsrMaster.Rows(0).Item(REG_KBN) = REG_TORIKOMI_NM _
                    And .PropDtEndUsrMaster.Rows(0).Item(JTI_FLG) = JTIFLG_OFF Then

                    If SetFormObjEdiTorikomiUnDropping(dataHBKX0401) = False Then

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
    ''' 編集モードフォームオブジェクト設定処理(登録方法：画面入力・削除されていないデータ)
    ''' </summary>
    ''' <param name="dataHBKX0401">[IN/OUT]エンドユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モード時(登録方法：画面入力・削除されていないデータ)のフォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/08/09 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormObjEdiGamenUnDropping(ByRef dataHBKX0401 As DataHBKX0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'オブジェクト活性非活性処理

            With dataHBKX0401

                '登録ボタン活性化
                .PropBtnReg.Visible = True

                'ユーザーIDテキストボックス非活性化
                .PropTxtEndUsrID.ReadOnly = True

                'ユーザー区分テキストボックス活性化
                .PropTxtUsrKbn.ReadOnly = False

                '姓テキストボックス活性化
                .PropTxtEndUsrSei.ReadOnly = False

                '名テキストボックス活性化
                .PropTxtEndUsrMei.ReadOnly = False

                '姓(カナ)テキストボックス活性化
                .PropTxtEndUsrSeikana.ReadOnly = False

                '名(カナ)テキストボックス活性化
                .PropTxtEndUsrMeikana.ReadOnly = False

                '所属会社テキストボックス活性化
                .PropTxtEndUsrCompany.ReadOnly = False

                '部署名テキストボックス活性化
                .PropTxtEndUsrBusyoNM.ReadOnly = False

                '電話番号テキストボックス活性化
                .PropTxtEndUsrTel.ReadOnly = False

                'メールアドレステキストボックス活性化
                .PropTxtEndUsrMailAdd.ReadOnly = False

                '状態説明テキストボックス
                .PropTxtStateNaiyo.ReadOnly = False


            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True


        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False

        End Try

    End Function

    
    ''' <summary>
    ''' 編集モードフォームオブジェクト設定処理(登録方法：取込・削除されていないデータ)
    ''' </summary>
    ''' <param name="dataHBKX0401">[IN/OUT]エンドユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モード時(登録方法：取込・削除されていないデータ)のフォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/08/13 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormObjEdiTorikomiUnDropping(ByRef dataHBKX0401 As DataHBKX0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

           

            'オブジェクト活性非活性処理

            With dataHBKX0401

                '登録ボタン非活性化
                .PropBtnReg.Visible = False


                'ユーザーID テキストボックス非活性化
                .PropTxtEndUsrID.ReadOnly = True

                'ユーザー区分テキストボックス非活性化
                .PropTxtUsrKbn.ReadOnly = True

                '姓テキストボックス非活性化
                .PropTxtEndUsrSei.ReadOnly = True

                '名テキストボックス非活性化
                .PropTxtEndUsrMei.ReadOnly = True

                '姓(カナ)テキストボックス非活性化
                .PropTxtEndUsrSeikana.ReadOnly = True

                '名(カナ)テキストボックス非活性化
                .PropTxtEndUsrMeikana.ReadOnly = True

                '所属会社テキストボックス非活性化
                .PropTxtEndUsrCompany.ReadOnly = True

                '部署名テキストボックス非活性化
                .PropTxtEndUsrBusyoNM.ReadOnly = True

                '電話番号テキストボックス非活性化
                .PropTxtEndUsrTel.ReadOnly = True

                'メールアドレステキストボックス非活性化
                .PropTxtEndUsrMailAdd.ReadOnly = True

                '状態説明テキストボックス非活性化
                .PropTxtStateNaiyo.ReadOnly = True

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True


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
    ''' <param name="dataHBKX0401">[IN/OUT]エンドユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示用データをフォームオブジェクトに設定する
    ''' <para>作成情報：2012/08/09 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitData(ByRef dataHBKX0401 As DataHBKX0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try

            'テキストボックスに初期表示用データを設定する
            With dataHBKX0401
                If .PropDtEndUsrMaster.Rows(0).Item(END_USR_ID).ToString <> "" Then
                    'ユーザーIDテキストボックス
                    .PropTxtEndUsrID.Text = .PropDtEndUsrMaster.Rows(0).Item(END_USR_ID)
                Else
                    'ユーザーIDテキストボックス
                    .PropTxtEndUsrID.Text = ""
                End If
                If .PropDtEndUsrMaster.Rows(0).Item(USR_KBN).ToString <> "" Then
                    'ユーザー区分テキストボックス
                    .PropTxtUsrKbn.Text = .PropDtEndUsrMaster.Rows(0).Item(USR_KBN)
                Else
                    'ユーザー区分テキストボックス
                    .PropTxtUsrKbn.Text = ""
                End If
                If .PropDtEndUsrMaster.Rows(0).Item(END_USR_SEI).ToString <> "" Then
                    '姓テキストボックス
                    .PropTxtEndUsrSei.Text = .PropDtEndUsrMaster.Rows(0).Item(END_USR_SEI)
                Else
                    '姓テキストボックス
                    .PropTxtEndUsrSei.Text = ""
                End If
                If .PropDtEndUsrMaster.Rows(0).Item(END_USR_MEI).ToString <> "" Then
                    '名テキストボックス
                    .PropTxtEndUsrMei.Text = .PropDtEndUsrMaster.Rows(0).Item(END_USR_MEI)
                Else
                    '名テキストボックス
                    .PropTxtEndUsrMei.Text = ""
                End If
                If .PropDtEndUsrMaster.Rows(0).Item(END_USR_SEI_KANA).ToString <> "" Then
                    '姓(カナ)テキストボックス
                    .PropTxtEndUsrSeikana.Text = .PropDtEndUsrMaster.Rows(0).Item(END_USR_SEI_KANA)
                Else
                    '姓(カナ)テキストボックス
                    .PropTxtEndUsrSeikana.Text = ""
                End If
                If .PropDtEndUsrMaster.Rows(0).Item(END_USR_MEI_KANA).ToString <> "" Then
                    '名(カナ)テキストボックス
                    .PropTxtEndUsrMeikana.Text = .PropDtEndUsrMaster.Rows(0).Item(END_USR_MEI_KANA)
                Else
                    '名(カナ)テキストボックス
                    .PropTxtEndUsrMeikana.Text = ""
                End If
                If .PropDtEndUsrMaster.Rows(0).Item(END_USR_COMPANY).ToString <> "" Then
                    '所属会社テキストボックス
                    .PropTxtEndUsrCompany.Text = .PropDtEndUsrMaster.Rows(0).Item(END_USR_COMPANY)
                Else
                    '所属会社テキストボックス
                    .PropTxtEndUsrCompany.Text = ""
                End If
                If .PropDtEndUsrMaster.Rows(0).Item(END_USR_BUSYO_NM).ToString <> "" Then
                    '部署名テキストボックス
                    .PropTxtEndUsrBusyoNM.Text = .PropDtEndUsrMaster.Rows(0).Item(END_USR_BUSYO_NM)
                Else
                    '部署名テキストボックス
                    .PropTxtEndUsrBusyoNM.Text = ""
                End If
                If .PropDtEndUsrMaster.Rows(0).Item(END_USR_TEL).ToString <> "" Then
                    '電話番号テキストボックス
                    .PropTxtEndUsrTel.Text = .PropDtEndUsrMaster.Rows(0).Item(END_USR_TEL)
                Else
                    '電話番号テキストボックス
                    .PropTxtEndUsrTel.Text = ""
                End If
                If .PropDtEndUsrMaster.Rows(0).Item(END_USR_MAIL_ADD).ToString <> "" Then
                    'メールアドレステキストボックス
                    .PropTxtEndUsrMailAdd.Text = .PropDtEndUsrMaster.Rows(0).Item(END_USR_MAIL_ADD)
                Else
                    'メールアドレステキストボックス
                    .PropTxtEndUsrMailAdd.Text = ""
                End If
                If .PropDtEndUsrMaster.Rows(0).Item(STATE_NAIYO).ToString <> "" Then
                    '状態説明テキストボックス
                    .PropTxtStateNaiyo.Text = .PropDtEndUsrMaster.Rows(0).Item(STATE_NAIYO)
                Else
                    '状態説明テキストボックス
                    .PropTxtStateNaiyo.Text = ""
                End If
                If .PropDtEndUsrMaster.Rows(0).Item(REG_KBN).ToString <> "" Then
                    '登録方法テキストボックス
                    .PropTxtRegKbn.Text = .PropDtEndUsrMaster.Rows(0).Item(REG_KBN)
                Else
                    '登録方法テキストボックス
                    .PropTxtRegKbn.Text = ""
                End If

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True


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
    ''' <param name="dataHBKX0401">[IN/OUT]エンドユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力された値のチェックを行う
    ''' <para>作成情報：2012/08/09 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InputCheckMain(ByRef dataHBKX0401 As DataHBKX0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '各種チェック
        If RegCheck(dataHBKX0401) = False Then
            Return False
        End If

       

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 登録メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0401">[IN/OUT]エンドユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたデータを登録及び更新する
    ''' <para>作成情報：2012/08/09 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function RegisterMain(ByRef dataHBKX0401 As DataHBKX0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'システム日付取得
        If GetSysDate(dataHBKX0401) = False Then
            Return False
        End If

        '登録/編集実行
        If RegisterEdit(dataHBKX0401) = False Then
            Return False
        End If

        'エンドユーザーマスター登録画面初期表示メイン呼出
        If InitFormMain(dataHBKX0401) = False Then
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
    ''' <param name="dataHBKX0401">[IN/OUT]エンドユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたデータが正しいかチェックする
    ''' <para>作成情報：2012/08/09 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RegCheck(ByRef dataHBKX0401 As DataHBKX0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try


            With dataHBKX0401
                'ユーザーIDテキストボックス
                '入力チェック
                If .PropTxtEndUsrID.Text.Trim = Nothing Then
                    puErrMsg = X0401_E001
                    'フォーカス設定
                    .PropTxtEndUsrID.Focus()
                    .PropTxtEndUsrID.SelectAll()
                    Return False
                End If

                '新規登録モード時だけ存在チェックを行う
                If dataHBKX0401.PropStrProcMode = PROCMODE_NEW Then

                    '存在チェック
                    If SonzaiCheck(dataHBKX0401) = False Then
                        Return False
                    End If

                End If

                'ユーザー区分テキストボックス
                '入力チェック
                If .PropTxtUsrKbn.Text.Trim = Nothing Then
                    puErrMsg = X0401_E003
                    'フォーカス設定
                    .PropTxtUsrKbn.Focus()
                    .PropTxtUsrKbn.SelectAll()
                    Return False
                End If

                '姓テキストボックス
                '入力チェック
                If .PropTxtEndUsrSei.Text.Trim = Nothing Then
                    puErrMsg = X0401_E004
                    'フォーカス設定
                    .PropTxtEndUsrSei.Focus()
                    .PropTxtEndUsrSei.SelectAll()
                    Return False
                End If

                '名テキストボックス
                '入力チェック
                If .PropTxtEndUsrMei.Text.Trim = Nothing Then
                    puErrMsg = X0401_E005
                    'フォーカス設定
                    .PropTxtEndUsrMei.Focus()
                    .PropTxtEndUsrMei.SelectAll()
                    Return False
                End If

                '姓(カナ)テキストボックス
                '入力チェック
                If .PropTxtEndUsrSeikana.Text.Trim = Nothing Then
                    puErrMsg = X0401_E006
                    'フォーカス設定
                    .PropTxtEndUsrSeikana.Focus()
                    .PropTxtEndUsrSeikana.SelectAll()
                    Return False
                End If

                '名(カナ)テキストボックス
                '入力チェック
                If .PropTxtEndUsrMeikana.Text.Trim = Nothing Then
                    puErrMsg = X0401_E007
                    'フォーカス設定
                    .PropTxtEndUsrMeikana.Focus()
                    .PropTxtEndUsrMeikana.SelectAll()
                    Return False
                End If

            End With

            '[Del] 2012/09/25 m.ibuki 形式チェック削除START
            'With dataHBKX0401

            '    'メールアドレスの形式チェック
            '    '入力されている場合チェックする
            '    If .PropTxtEndUsrMailAdd.Text <> "" Then
            '        'メールアドレス形式ではない場合、エラー
            '        If commonLogicHBK.IsMailAddress(.PropTxtEndUsrMailAdd.Text) = False Then
            '            'エラーメッセージ設定
            '            puErrMsg = X0401_E008
            '            'フォーカス設定
            '            .PropTxtEndUsrMailAdd.Focus()
            '            .PropTxtEndUsrMailAdd.SelectAll()
            '            'エラーを返す
            '            Return False
            '        End If
            '    End If

            'End With
            '[Del] 2012/09/25 m.ibuki 形式チェック削除END

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True


        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False

        End Try

    End Function

    ''' <summary>
    ''' 存在チェック
    ''' </summary>
    ''' <param name="dataHBKX0401">[IN/OUT]エンドユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたユーザーIDがDBに存在するかチェックする
    ''' <para>作成情報：2012/08/09 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SonzaiCheck(ByRef dataHBKX0401 As DataHBKX0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)



        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try
            'コネクションを開く
            Cn.Open()

            'ユーザーID取得処理
            If GetEndUsrID(Adapter, Cn, dataHBKX0401) = False Then
                Return False
            End If

            '存在チェック実施
            '入力したユーザーIDでデータを取得できた場合存在エラー
            If dataHBKX0401.PropDtUsrID.Rows.Count <> 0 Then
                puErrMsg = X0401_E002
                'フォーカス設定
                dataHBKX0401.PropTxtEndUsrID.Focus()
                dataHBKX0401.PropTxtEndUsrID.SelectAll()
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
    ''' エンドユーザーID取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKX0401">[IN/OUT]エンドユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>エンドユーザーIDを取得する
    ''' <para>作成情報：2012/08/09 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetEndUsrID(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKX0401 As DataHBKX0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtEndUsrID As New DataTable

        Try


            'エンドユーザーID取得

            'エンドユーザーID取得用SQLの作成・設定
            If sqlHBKX0401.SetSelectEndUsrIDSql(Adapter, Cn, dataHBKX0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "エンドユーザーID取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtEndUsrID)

            '取得データをデータクラスにセット
            dataHBKX0401.PropDtUsrID = dtEndUsrID

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False

        Finally
            dtEndUsrID.Dispose()
        End Try


    End Function

    ''' <summary>
    '''　登録/編集処理
    ''' </summary>
    ''' <param name="dataHBKX0401">[IN/OUT]エンドユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>モードごとに登録及び編集処理を行う
    ''' <para>作成情報：2012/08/09 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RegisterEdit(ByRef dataHBKX0401 As DataHBKX0401) As Boolean

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
            If dataHBKX0401.PropStrProcMode = PROCMODE_NEW Then

                '登録処理
                If Register(Cn, dataHBKX0401) = False Then
                    'ロールバック
                    Tsx.Rollback()
                    Return False
                End If

            ElseIf dataHBKX0401.PropStrProcMode = PROCMODE_EDIT Then

                '編集処理
                If Edit(Cn, dataHBKX0401) = False Then
                    'ロールバック
                    Tsx.Rollback()
                    Return False
                End If

            End If

            'コミット
            Tsx.Commit()

            'コネクションを閉じる
            Cn.Close()

            'モードが新規登録の場合は編集モードに設定して、ユーザーIDをプロパティにセットする
            With dataHBKX0401

                If .PropStrProcMode = PROCMODE_NEW Then
                    'モードを編集モードに設定する
                    .PropStrProcMode = PROCMODE_EDIT
                    .PropStrEndUsrID = .PropTxtEndUsrID.Text

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
    ''' <param name="dataHBKX0401">[IN]エンドユーザー登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をエンドユーザーマスターテーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/09 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function Register( ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKX0401 As DataHBKX0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'エンドユーザーマスター新規登録（INSERT）用SQLを作成
            If sqlHBKX0401.SetInsertEndUsrMasterSql(Cmd, Cn, dataHBKX0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "エンドユーザーマスター新規登録", Nothing, Cmd)

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
    ''' <param name="dataHBKX0401">[IN]エンドユーザー登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でエンドユーザーマスターテーブルを編集（UPDATE）する
    ''' <para>作成情報：2012/08/09 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function Edit( ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKX0401 As DataHBKX0401) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'エンドユーザーマスター編集（UPDATE）用SQLを作成
            If sqlHBKX0401.SetUpdateEndUsrMasterSql(Cmd, Cn, dataHBKX0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "エンドユーザーマスター編集", Nothing, Cmd)

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
    ''' システム日付取得処理
    ''' </summary>
    ''' <param name="dataHBKX0401">[IN/OUT]エンドユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システム日付取得する
    ''' <para>作成情報：2012/08/14 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetSysDate(ByRef dataHBKX0401 As DataHBKX0401) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim dtSysDate As New DataTable

        Try

            'システム日付取得
            If sqlHBKX0401.SetSelectSysDateSql(Adapter, Cn, DataHBKX0401) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "システム日付取得", Nothing, Adapter.SelectCommand)
            'データを取得
            Adapter.Fill(dtSysDate)

            'データが取得できた場合、データクラスにサーバー日付をセット
            If dtSysDate.Rows.Count > 0 Then
                DataHBKX0401.PropDtmSysDate = dtSysDate.Rows(0).Item("SysDate")
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

End Class
