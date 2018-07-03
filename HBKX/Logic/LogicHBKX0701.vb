Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.IO
Imports System.Windows.Forms

''' <summary>
'''メールテンプレートマスター登録画面ロジッククラス
''' </summary>
''' <remarks>メールテンプレートマスター登録画面のロジックを定義したクラス
''' <para>作成情報：2012/08/21 k.imayama
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKX0701

    'インスタンス作成
    Private sqlHBKX0701 As New SqlHBKX0701
    Private commonLogic As New CommonLogic
    Private commonValidation As New CommonValidation
    Private commonLogicHBK As New CommonLogicHBK

    'CI種別コードリスト（マスタデータ取得に使用）
    Public Const CIKBNCD_LIST As String = CI_TYPE_SUPORT & "," & CI_TYPE_KIKI

    ''' <summary>
    ''' 【新規登録モード】画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて画面の初期表示設定を行う
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormNewModeMain(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'フォームコントロール設定
        If InitFormControl(dataHBKX0701) = False Then
            Return False
        End If

        '初期表示用データ取得
        If GetInitData(dataHBKX0701) = False Then
            Return False
        End If

        '初期表示用データセット
        If SetInitDataToControl(dataHBKX0701) = False Then
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
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで画面の初期表示設定を行う
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormEditModeMain(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'フォームコントロール設定
        If InitFormControl(DataHBKX0701) = False Then
            Return False
        End If

        '初期表示用データ取得
        If GetInitData(dataHBKX0701) = False Then
            Return False
        End If

        '初期表示用データセット
        If SetInitDataToControl(DataHBKX0701) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】フォームコントロール設定
    ''' </summary>
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InitFormControl(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '処理モードに応じたフォームコントロール設定
            If SetFormControlPerProcMode(dataHBKX0701) = False Then
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
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフォームコントロールの設定を行う
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormControlPerProcMode(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'ログイン／ロックコントロール設定
            If SetLoginAndLockControl(DataHBKX0701) = False Then
                Return False
            End If

            'フッタ設定
            If SetFooterControl(DataHBKX0701) = False Then
                Return False
            End If

            '基本情報設定
            If SetControlKhn(DataHBKX0701) = False Then
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
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControl(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX0701.PropGrpLoginUser

                'グループコンボボックス非活性
                .cmbGroup.Enabled = False
                '変更ボタン活性
                .btnChange.Enabled = False
                'ロック情報非表示
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
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControl(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX0701

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetFooterControlForNew(dataHBKX0701) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード

                    '編集モード用設定
                    If SetFooterControlForEdit(dataHBKX0701) = False Then
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
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForNew(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX0701

                'ボタン表示、非表示
                .PropBtnMailFromSearch.Enabled = True                   '基本情報：差出人選択ボタン
                .PropBtnMailToSearch.Enabled = True                     '基本情報：TO追加ボタン
                .PropBtnCCSearch.Enabled = True                         '基本情報：CC追加ボタン
                .PropBtnBccSearch.Enabled = True                        '基本情報：BCC追加ボタン

                .PropBtnReg.Visible = True                              'フッタ：登録ボタン
                .PropBtnDelete.Visible = False                          'フッタ：削除ボタン
                .PropBtnDeleteKaijyo.Visible = False                    'フッタ：削除解除ボタン

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForEdit(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX0701

                'ボタン表示、非表示
                .PropBtnMailFromSearch.Enabled = True                   '基本情報：差出人選択ボタン
                .PropBtnMailToSearch.Enabled = True                     '基本情報：TO追加ボタン
                .PropBtnCCSearch.Enabled = True                         '基本情報：CC追加ボタン
                .PropBtnBccSearch.Enabled = True                        '基本情報：BCC追加ボタン

                .PropBtnReg.Visible = True                              'フッタ：登録ボタン
                .PropBtnDelete.Visible = True                           'フッタ：削除ボタン
                .PropBtnDeleteKaijyo.Visible = False                    'フッタ：削除解除ボタン

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード（削除データ）】フッタコントロール設定
    ''' </summary>
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForRef(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX0701

                'ボタン表示、非表示
                .PropBtnMailFromSearch.Enabled = False                  '基本情報：差出人選択ボタン
                .PropBtnMailToSearch.Enabled = False                    '基本情報：TO追加ボタン
                .PropBtnCCSearch.Enabled = False                        '基本情報：CC追加ボタン
                .PropBtnBccSearch.Enabled = False                       '基本情報：BCC追加ボタン

                .PropBtnReg.Visible = False                             'フッタ：登録ボタン
                .PropBtnDelete.Visible = False                          'フッタ：削除ボタン
                .PropBtnDeleteKaijyo.Visible = True                     'フッタ：削除解除ボタン

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
    ''' 【共通】基本情報コントロール設定
    ''' </summary>
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetControlKhn(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX0701

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetControlKhnForNew(dataHBKX0701) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード

                    '編集モード用設定
                    If SetControlKhnForEdit(dataHBKX0701) = False Then
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
    ''' 【新規登録モード】基本情報コントロール設定
    ''' </summary>
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードに応じて基本情報コントロールの初期設定を行う
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetControlKhnForNew(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX0701

                .ProptxtTemplateNM.ReadOnly = False         'テンプレート名テキストボックス
                .PropcmbPriorityKbn.Enabled = True          '重要度コンボボックス 
                .PropcmbProcessKbn.Enabled = True           'プロセス区分コンボボックス
                .PropgrpKigenCond.Enabled = False           'グループボックス
                .PropcmbKigenCondTypeKbn.Enabled = False    '期限切れ条件タイプコンボボックス
                .ProptxtTitle.ReadOnly = False              '件名テキストボックス
                .ProptxtMailFrom.ReadOnly = False           '差出人テキストボックス
                .ProptxtMailTo.ReadOnly = False             'TOテキストボックス
                .ProptxtCC.ReadOnly = False                 'CCテキストボックス
                .ProptxtBcc.ReadOnly = False                'Bccテキストボックス
                .ProptxtText.ReadOnly = False               'タイプコンボボックス

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードに応じて基本情報コントロールの初期設定を行う
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetControlKhnForEdit(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX0701

                .ProptxtTemplateNM.ReadOnly = False         'テンプレート名テキストボックス
                .PropcmbPriorityKbn.Enabled = True          '重要度コンボボックス 
                .PropcmbProcessKbn.Enabled = True           'プロセス区分コンボボックス
                .PropgrpKigenCond.Enabled = False           'グループボックス
                .PropcmbKigenCondTypeKbn.Enabled = False    '期限切れタイプコンボボックス
                .ProptxtTitle.ReadOnly = False              '件名テキストボックス
                .ProptxtMailFrom.ReadOnly = False           '差出人テキストボックス
                .ProptxtMailTo.ReadOnly = False             'TOテキストボックス
                .ProptxtCC.ReadOnly = False                 'CCテキストボックス
                .ProptxtBcc.ReadOnly = False                'Bccテキストボックス
                .ProptxtText.ReadOnly = False               'タイプコンボボックス

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード（削除データ）】基本情報コントロール設定
    ''' </summary>
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードに応じて基本情報コントロールの初期設定を行う
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetControlKhnForRef(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX0701

                .ProptxtTemplateNM.ReadOnly = True          'テンプレート名テキストボックス
                .PropcmbPriorityKbn.Enabled = False         '重要度コンボボックス 
                .PropcmbProcessKbn.Enabled = False          'プロセス区分コンボボックス
                .PropgrpKigenCond.Enabled = False           'グループボックス 
                .ProptxtTitle.ReadOnly = True               '件名テキストボックス
                .ProptxtMailFrom.ReadOnly = True            '差出人テキストボックス
                .ProptxtMailTo.ReadOnly = True              'TOテキストボックス
                .ProptxtCC.ReadOnly = True                  'CCテキストボックス
                .ProptxtBcc.ReadOnly = True                 'Bccテキストボックス
                .ProptxtText.ReadOnly = True                'タイプコンボボックス

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            'マスタデータ取得
            If GetMastaData(Adapter, Cn, dataHBKX0701) = False Then
                Return False
            End If

            'メイン表示データ取得
            If GetMainData(Adapter, Cn, dataHBKX0701) = False Then
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
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            Adapter.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】マスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>マスタデータを取得する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMastaData(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 START
        Dim dtTable As New DataTable
        '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 END

        Try
            'CI種別マスタ取得
            If commonLogicHBK.GetCIKindMastaData(Adapter, Cn, CIKBNCD_LIST, dataHBKX0701.PropDtKindMasta) = False Then
                Return False
            End If

            'サポセン機器タイプマスター取得

            'If commonLogicHBK.GetSapKikiTypeMastaData(Adapter, Cn, dataHBKX0701.PropDtSapKikiTypeMasta) = False Then
            '    Return False
            'End If

            '取得用SQLの作成・設定
            If sqlHBKX0701.SetSelectSapKikiTypeMastaDataSql(Adapter, Cn, dataHBKX0701) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サポセン機器タイプマスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtTable)
            dataHBKX0701.PropDtSapKikiTypeMasta = dtTable

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
            '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 START
            dtTable.Dispose()
            '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 END
        End Try

    End Function

    ''' <summary>
    ''' 【共通】初期表示用メインデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainData(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX0701

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '処理なし

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード

                    '編集モード用データ取得
                    If GetMainDataForEdit(Adapter, Cn, dataHBKX0701) = False Then
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
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集／参照モードで初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainDataForEdit(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'メールテンプレートマスター取得
            If GetMailTemplateMtb(Adapter, Cn, dataHBKX0701) = False Then
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
    ''' 【編集モード】メールテンプレートマスター取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>メールテンプレートマスターデータを取得する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMailTemplateMtb(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtTemplate As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKX0701.SetSelectMailTemplateSql(Adapter, Cn, dataHBKX0701) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "メールテンプレートマスター取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtTemplate)

            'データが取得できなかった場合、エラー
            If dtTemplate.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & X0701_E001, TBNM_MAIL_TEMPLATE_MTB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKX0701.PropDtTemplateMtb = dtTemplate

            '終了ログ出力
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
            dtTemplate.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】初期データ設定処理
    ''' </summary>
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームコントロールに初期表示データを設定する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitDataToControl(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'コントロールデータ設定
            If SetDataToTabControl(dataHBKX0701) = False Then
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
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>コントロールデータを初期設定する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabControl(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '基本情報データ設定
            If SetDataToKhn(DataHBKX0701) = False Then
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
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて基本情報データを初期設定する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToKhn(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With DataHBKX0701

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '新規登録モード用設定
                    If SetDataToKhnForNew(DataHBKX0701) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード

                    '編集モード用設定
                    If SetDataToKhnForEdit(dataHBKX0701) = False Then
                        Return False
                    End If

                    '編集モード（削除データ）用設定
                    If .PropStrJtiFlg = DELETE_MODE_MUKO Then
                        'フッタコントロール設定
                        If SetFooterControlForRef(dataHBKX0701) = False Then
                            Return False
                        End If
                        '基本情報コントロール設定
                        If SetControlKhnForRef(dataHBKX0701) = False Then
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
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードで基本情報データを初期設定する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToKhnForNew(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'コンボボックス作成
            If CreateCmb(DataHBKX0701) = False Then
                Return False
            End If

            With DataHBKX0701

                .ProptxtTemplateNmb.Text = ""                               'テンプレート番号
                .ProptxtTemplateNM.Text = ""                                'テンプレート名
                .PropcmbPriorityKbn.SelectedValue = PRIORITY_TYPE_NORMAL    '重要度

                .PropcmbProcessKbn.SelectedValue = ""                       'プロセス区分
                .PropcmbKigenCondCIKbnCD.SelectedValue = ""                 '期限切れ条件CI種別
                .PropcmbKigenCondTypeKbn.SelectedValue = ""                 '期限切れ条件タイプ
                .ProprdoKigenCondKbn.Checked = False                        '期限切れ条件区分
                .PropcmbKigenCondKigen.SelectedValue = LIMIT_THISMONTH_ONLY '期限切れ条件期限
                .ProprdoKigenCondUsrID.Checked = False                      '期限切れ条件区分ユーザーID

                .ProptxtTitle.Text = ""                                     'タイトル
                .ProptxtMailFrom.Text = ""                                  '差出人
                .ProptxtMailTo.Text = ""                                    '宛先
                .ProptxtCC.Text = ""                                        'CC
                .ProptxtBcc.Text = ""                                       'BCC
                .ProptxtText.Text = ""                                      '本文
                .PropStrJtiFlg = DELETE_MODE_YUKO                           '削除フラグ

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

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
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで基本情報データを初期設定する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToKhnForEdit(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'コンボボックス作成
            If CreateCmb(DataHBKX0701) = False Then
                Return False
            End If

            With DataHBKX0701

                .ProptxtTemplateNmb.Text = .PropDtTemplateMtb.Rows(0).Item("TemplateNmb")               'テンプレート番号
                .ProptxtTemplateNM.Text = .PropDtTemplateMtb.Rows(0).Item("TemplateNM")                 'テンプレート名
                .PropcmbPriorityKbn.SelectedValue = .PropDtTemplateMtb.Rows(0).Item("PriorityKbn")      '重要度

                .PropcmbProcessKbn.SelectedValue = .PropDtTemplateMtb.Rows(0).Item("ProcessKbn")        'プロセス区分
                .PropcmbKigenCondCIKbnCD.SelectedValue = .PropDtTemplateMtb.Rows(0).Item("KigenCondCIKbnCD")    '期限切れ条件CI種別
                .PropcmbKigenCondTypeKbn.SelectedValue = .PropDtTemplateMtb.Rows(0).Item("KigenCondTypeKbn")    '期限切れ条件タイプ
                .PropcmbKigenCondKigen.SelectedValue = .PropDtTemplateMtb.Rows(0).Item("KigenCondKigen")        '期限切れ条件期限

                If .PropDtTemplateMtb.Rows(0).Item("KigenCondKbn").Equals(DBNull.Value) Then
                    .ProprdoKigenCondKbn.Checked = True
                    .ProprdoKigenCondUsrID.Checked = False
                ElseIf .PropDtTemplateMtb.Rows(0).Item("KigenCondKbn") = KIGEN_KBN_ON Then
                    .ProprdoKigenCondKbn.Checked = False
                    .ProprdoKigenCondUsrID.Checked = True
                ElseIf .PropDtTemplateMtb.Rows(0).Item("KigenCondKbn") = KIGEN_KBN_OFF Then
                    .ProprdoKigenCondKbn.Checked = True
                    .ProprdoKigenCondUsrID.Checked = False
                Else
                    .ProprdoKigenCondKbn.Checked = False
                    .ProprdoKigenCondUsrID.Checked = False
                End If

                .ProptxtMailFrom.Text = .PropDtTemplateMtb.Rows(0).Item("MailFrom")         '差出人
                .ProptxtMailTo.Text = .PropDtTemplateMtb.Rows(0).Item("MailTo")             '宛先
                .ProptxtCC.Text = .PropDtTemplateMtb.Rows(0).Item("CC")                     'CC
                .ProptxtBcc.Text = .PropDtTemplateMtb.Rows(0).Item("Bcc")                   'BCC
                .ProptxtTitle.Text = .PropDtTemplateMtb.Rows(0).Item("Title")               'タイトル
                .ProptxtText.Text = .PropDtTemplateMtb.Rows(0).Item("Text")                 '本文

                .PropStrJtiFlg = .PropDtTemplateMtb.Rows(0).Item("JtiFlg")                  '削除フラグ

                'インシデントの場合、期限切れお知らせ条件は非活性
                If .PropcmbProcessKbn.SelectedValue = PROCESS_TYPE_INCIDENT Then
                    .PropgrpKigenCond.Enabled = True
                    If .PropcmbKigenCondCIKbnCD.SelectedValue = CI_TYPE_SUPORT Then
                        .PropcmbKigenCondTypeKbn.Enabled = True
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
    ''' 【共通】コンボボックス作成処理
    ''' </summary>
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のコンボボックスを作成する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateCmb(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX0701

                '重要度
                If commonLogic.SetCmbBox(PriorityType, dataHBKX0701.PropcmbPriorityKbn) = False Then
                    Return False
                End If

                'プロセス区分
                If commonLogic.SetCmbBox(ProcessType, dataHBKX0701.PropcmbProcessKbn) = False Then
                    Return False
                End If

                'CI種別コンボボックス作成
                If commonLogic.SetCmbBox(.PropDtKindMasta, .PropcmbKigenCondCIKbnCD, True, "", "") = False Then
                    Return False
                End If

                'タイプコンボボックス作成
                If commonLogic.SetCmbBox(.PropDtSapKikiTypeMasta, .PropcmbKigenCondTypeKbn, True, "", "") = False Then
                    Return False
                End If

                '期限コンボボックス作成
                If commonLogic.SetCmbBox(strCmbLimit, dataHBKX0701.PropcmbKigenCondKigen) = False Then
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
    ''' 【共通】コントロール入力チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録項目の入力チェックを行う
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckInputValueMain(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'コントロール入力チェック
        If CheckInputValue(dataHBKX0701) = False Then
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
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録項目の入力チェックを行う
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckInputValue(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim strArrayData As String()
        'Dim index As Integer
        Dim strMailAdd As String

        Try

            With dataHBKX0701

                'テンプレート名テキストボックス
                With .ProptxtTemplateNM
                    '未入力の場合、エラー
                    If .Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = X0701_E002
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With

                'プロセス区分コンボボックス
                With .PropcmbProcessKbn
                    '未入力の場合、エラー
                    If .SelectedValue = "" Then
                        'エラーメッセージ設定
                        puErrMsg = X0701_E003
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With

                'インシデント選択の場合
                If .PropcmbProcessKbn.SelectedValue = PROCESS_TYPE_INCIDENT Then

                    ''CI種別未選択の場合、エラー
                    'If .PropcmbKigenCondCIKbnCD.SelectedValue = "" Then

                    '    'タイプ、期限、ユーザーIDのいずれか選択されている場合    
                    '    If .PropcmbKigenCondTypeKbn.SelectedValue <> "" Or _
                    '       .PropcmbKigenCondKigen.SelectedValue <> "" Or _
                    '       .ProprdoKigenCondKbn.Checked = True Then
                    '        'エラーメッセージ設定
                    '        puErrMsg = X0701_E004
                    '        'フォーカス設定
                    '        .PropcmbKigenCondCIKbnCD.Focus()
                    '        .PropcmbKigenCondCIKbnCD.SelectAll()
                    '        'エラーを返す
                    '        Return False
                    '    End If
                    'End If

                    '期限未選択の場合
                    If .PropcmbKigenCondKigen.SelectedValue = "" And _
                        .ProprdoKigenCondKbn.Checked = True Then

                        'CI種別、タイプのいずれか選択されている場合    
                        If .PropcmbKigenCondCIKbnCD.SelectedValue <> "" Or _
                           .PropcmbKigenCondTypeKbn.SelectedValue <> "" Then
                            'エラーメッセージ設定
                            puErrMsg = X0701_E004
                            'フォーカス設定
                            .PropcmbKigenCondTypeKbn.Focus()
                            .PropcmbKigenCondTypeKbn.SelectAll()
                            'エラーを返す
                            Return False
                        End If
                    End If

                End If

                '[Del] 2012/09/25 m.ibuki 形式チェック削除START
                ''差出人テキストボックス
                'With .ProptxtMailFrom
                '    '未入力の場合、エラー
                '    If .Text.Trim() <> "" Then
                '        'メールアドレス書式チェック
                '        If commonLogicHBK.IsMailAddress(.Text.Trim) = False Then
                '            'エラーメッセージ設定
                '            puErrMsg = X0701_E006
                '            'フォーカス設定
                '            .Focus()
                '            .SelectAll()
                '            'エラーを返す
                '            Return False
                '        End If
                '    End If
                'End With
                '[Del] 2012/09/25 m.ibuki 形式チェック削除END

                'TOテキストボックス
                With .ProptxtMailTo
                    '未入力の場合、エラー
                    If .Text.Trim() <> "" Then
                        '改行コードを除去
                        strMailAdd = commonLogicHBK.RemoveVbCr(.Text.Trim())
                        'セミコロン区切りで分割して配列に格納する
                        strArrayData = strMailAdd.Split(";"c)

                        '[Del] 2012/09/25 m.ibuki 形式チェック削除START
                        ''データを確認する
                        'index = 0
                        'For Each strData As String In strArrayData
                        '    index = index + 1
                        '    'メールアドレス書式チェック
                        '    If commonLogicHBK.IsMailAddress(strData.Trim) = False Then
                        '        'エラーメッセージ設定
                        '        puErrMsg = String.Format(X0701_E007, index.ToString)
                        '        'フォーカス設定
                        '        .Focus()
                        '        .SelectAll()
                        '        'エラーを返す
                        '        Return False
                        '    End If
                        'Next strData
                        '[Del] 2012/09/25 m.ibuki 形式チェック削除END

                    End If
                End With

                'CCテキストボックス
                With .ProptxtCC
                    '未入力の場合、エラー
                    If .Text.Trim() <> "" Then
                        '改行コードを除去
                        strMailAdd = commonLogicHBK.RemoveVbCr(.Text.Trim())
                        'セミコロン区切りで分割して配列に格納する
                        strArrayData = strMailAdd.Split(";"c)

                        '[Del] 2012/09/25 m.ibuki 形式チェック削除START
                        'データを確認する
                        'index = 0
                        'For Each strData As String In strArrayData
                        '    index = index + 1
                        '    'メールアドレス書式チェック
                        '    If commonLogicHBK.IsMailAddress(strData.Trim) = False Then
                        '        'エラーメッセージ設定
                        '        puErrMsg = String.Format(X0701_E008, index.ToString)
                        '        'フォーカス設定
                        '        .Focus()
                        '        .SelectAll()
                        '        'エラーを返す
                        '        Return False
                        '    End If
                        'Next strData
                        '[Del] 2012/09/25 m.ibuki 形式チェック削除END

                    End If
                End With

                'BCCテキストボックス
                With .ProptxtBcc
                    '未入力の場合、エラー
                    If .Text.Trim() <> "" Then
                        '改行コードを除去
                        strMailAdd = commonLogicHBK.RemoveVbCr(.Text.Trim())
                        'セミコロン区切りで分割して配列に格納する
                        strArrayData = strMailAdd.Split(";"c)


                        '[Del] 2012/09/25 m.ibuki 形式チェック削除START
                        'データを確認する
                        'index = 0
                        'For Each strData As String In strArrayData
                        '    index = index + 1
                        '    'メールアドレス書式チェック
                        '    If commonLogicHBK.IsMailAddress(strData.Trim) = False Then
                        '        'エラーメッセージ設定
                        '        puErrMsg = String.Format(X0701_E009, index.ToString)
                        '        'フォーカス設定
                        '        .Focus()
                        '        .SelectAll()
                        '        'エラーを返す
                        '        Return False
                        '    End If
                        'Next strData
                        '[Del] 2012/09/25 m.ibuki 形式チェック削除END

                    End If
                End With

                '本文テキストボックス
                With .ProptxtText
                    '未入力の場合、エラー
                    If .Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = X0701_E009
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
    ''' 【新規登録モード】データ登録メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0701">[IN]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をデータベースに反映する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RegistDataOnNewModeMain(ByVal dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'システム日付取得
        If SelectSysDate(dataHBKX0701) = False Then
            Return False
        End If

        '新規登録処理
        If InsertNewData(DataHBKX0701) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【新規登録モード】データ新規登録処理
    ''' </summary>
    ''' <param name="dataHBKX0701">[IN]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をDBに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertNewData(ByVal dataHBKX0701 As DataHBKX0701) As Boolean

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

            '新規テンプレート番号取得
            If SelectNewTemplateNmbAndSysDate(Cn, dataHBKX0701) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            'メールテンプレート新規登録
            If InsertMailTemplate(Tsx, Cn, dataHBKX0701) = False Then
                'ロールバック
                Tsx.Rollback()
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
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            Tsx.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【新規登録モード】新規テンプレート番号取得処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKX0701">[IN]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番した導入番号を取得（SELECT）する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectNewTemplateNmbAndSysDate(ByVal Cn As NpgsqlConnection, _
                                                    ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtResult As New DataTable

        Try
            '新規テンプレート番号取得（SELECT）用SQLを作成
            If sqlHBKX0701.SetSelectNewTemplateNmbAndSysDateSql(Adapter, Cn, dataHBKX0701) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規テンプレート番号取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            'データが取得できた場合、データクラスに取得データをセット
            If dtResult.Rows.Count > 0 Then
                dataHBKX0701.PropIntTemplateNmb = dtResult.Rows(0).Item("TemplateNmb")      'テンプレート番号
            Else
                '取得できなかったときはエラー
                puErrMsg = X0701_E010
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
    ''' 【新規登録モード】メールテンプレートマスター新規登録処理
    ''' </summary>
    ''' <param name="Tsx">[IN/OUT]NpgsqlTransaction</param>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKX0701">[IN]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容を導入テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertMailTemplate(ByRef Tsx As NpgsqlTransaction, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByVal dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'メールテンプレートマスター新規登録（INSERT）用SQLを作成
            If sqlHBKX0701.SetInsertMailTemplateSql(Cmd, Cn, dataHBKX0701) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "メールテンプレート新規登録", Nothing, Cmd)

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
    ''' 【編集モード】データ登録メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0701">[IN]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をデータベースに反映する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RegistDataOnEditModeMain(ByVal dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'システム日付取得
        If SelectSysDate(dataHBKX0701) = False Then
            Return False
        End If

        '更新処理
        If UpdateData(DataHBKX0701) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【編集モード】データ更新処理
    ''' </summary>
    ''' <param name="dataHBKX0701">[IN]メールテンプレートマスター登録入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をDBに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateData(ByVal dataHBKX0701 As DataHBKX0701) As Boolean

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

            'メールテンプレートマスター更新（UPDATE）
            If UpdateMailTemplate(Tsx, Cn, dataHBKX0701) = False Then
                'ロールバック
                Tsx.Rollback()
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
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            Tsx.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード】サーバー日付取得処理
    ''' </summary>
    ''' <param name="dataHBKX0701">[IN]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>更新用のサーバー日付を取得する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectSysDate(ByVal dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim dtSysDate As New DataTable

        Try
            'システム日付取得
            If sqlHBKX0701.SetSelectSysDateSql(Adapter, Cn) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サーバー日付取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtSysDate)

            'データが取得できた場合、データクラスに履歴番号をセット
            If dtSysDate.Rows.Count > 0 Then
                dataHBKX0701.PropDtmSysDate = dtSysDate.Rows(0).Item("SysDate")
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
    ''' 【編集モード】メールテンプレートマスター更新処理
    ''' </summary>
    ''' <param name="Tsx">[IN/OUT]NpgsqlTransaction</param>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKX0701">[IN]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でメールテンプレートマスターを更新（UPDATE）する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateMailTemplate(ByRef Tsx As NpgsqlTransaction, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'メールテンプレートマスター更新（UPDATE）用SQLを作成
            If sqlHBKX0701.SetUpdateMailtemplateSql(Cmd, Cn, dataHBKX0701) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "メールテンプレート更新", Nothing, Cmd)

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
    ''' 【編集モード（削除・削除解除モード）】データ登録メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0701">[IN]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をデータベースに反映する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RegistDataOnDelModeMain(ByVal dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'システム日付取得
        If SelectSysDate(dataHBKX0701) = False Then
            Return False
        End If

        '更新処理
        If DeleteData(dataHBKX0701) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【編集モード（削除・削除解除モード）】データ更新処理
    ''' </summary>
    ''' <param name="dataHBKX0701">[IN]メールテンプレートマスター登録入画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をDBに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DeleteData(ByVal dataHBKX0701 As DataHBKX0701) As Boolean

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

            'メールテンプレートマスター更新（UPDATE）
            If DeleteMailTemplate(Tsx, Cn, dataHBKX0701) = False Then
                'ロールバック
                Tsx.Rollback()
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
            Cn.Dispose()
            Tsx.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集モード（削除・削除解除モード）】メールテンプレートマスター更新処理
    ''' </summary>
    ''' <param name="Tsx">[IN/OUT]NpgsqlTransaction</param>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKX0701">[IN]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でメールテンプレートマスターを更新（UPDATE）する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DeleteMailTemplate(ByRef Tsx As NpgsqlTransaction, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'メールテンプレートマスター更新（UPDATE）用SQLを作成
            If sqlHBKX0701.SetDeleteMailTemplateSql(Cmd, Cn, dataHBKX0701) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "メールテンプレート更新（削除・削除解除）", Nothing, Cmd)

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
    ''' プロセス区分選択時活性、非活性化メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセス区分が選択された場合の活性、非活性を行う
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function rdoAbleMain(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        With dataHBKX0701

            'インシデントが選択された場合
            If .PropcmbProcessKbn.SelectedValue = PROCESS_TYPE_INCIDENT Then
                .PropgrpKigenCond.Enabled = True
                'CI種別にサポセン機器が選択された場合
                If .PropcmbKigenCondCIKbnCD.SelectedValue = CI_TYPE_SUPORT Then
                    .PropcmbKigenCondTypeKbn.Enabled = True
                    .PropcmbKigenCondTypeKbn.SelectedValue = SAP_TYPE_NORMAL
                Else
                    .PropcmbKigenCondTypeKbn.Enabled = False
                    .PropcmbKigenCondTypeKbn.SelectedValue = ""
                End If
            Else
                .PropgrpKigenCond.Enabled = False
            End If

        End With

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 差出人ユーザー追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>差出人にサブ検索画面から取得したユーザーデータを設定する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetUserFromMailMain(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ひびきユーザーデータ取得処理
        If GetUserFromMail(dataHBKX0701) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' ひびきユーザーマスター取得処理
    ''' </summary>
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetUserFromMail(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            'ひびきユーザーデータ取得
            If GetHBKUsrMtb(Adapter, Cn, dataHBKX0701) = False Then
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
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            Adapter.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' ひびきユーザーマスター取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ひびきユーザーマスターデータを取得する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetHBKUsrMtb(ByVal Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtHBKUsr As New DataTable

        Try
            'ひびきユーザー取得用SQLの作成・設定
            If sqlHBKX0701.SetSelectHBKUsrSql(Adapter, Cn, dataHBKX0701) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ひびきユーザーマスター取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtHBKUsr)

            'データが取得できなかった場合、エラー
            If dtHBKUsr.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & X0701_E001, TBNM_HBKUSR_MTB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKX0701.ProptxtMailFrom.Text = dtHBKUsr.Rows(0).Item(0)

            '終了ログ出力
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
            dtHBKUsr.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 宛先ユーザー追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>宛先にサブ検索画面から取得したユーザーデータを設定する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetUserToMailToMain(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ユーザーデータ設定処理
        If SetUserToMailTO(dataHBKX0701) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】宛先情報ユーザー設定処理
    ''' </summary>
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>宛先にサブ検索で選択されたユーザーを設定する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetUserToMailTO(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX0701

                'サブ検索画面で1件以上選択された場合に値を設定
                If .PropDtResultSub IsNot Nothing Then
                    '選択データ数分繰り返し、宛先に追加する
                    For i As Integer = 0 To .PropDtResultSub.Rows.Count - 1
                        'ユーザーが既に設定されている場合は追加しない
                        If .ProptxtMailTo.Text.Contains(.PropDtResultSub.Rows(i).Item(4)) = False Then
                            If .ProptxtMailTo.Text = "" Then
                                .ProptxtMailTo.Text = .PropDtResultSub.Rows(i).Item(4)
                            Else
                                .ProptxtMailTo.Text &= "; " & .PropDtResultSub.Rows(i).Item(4)
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
    ''' CCユーザー追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CCにサブ検索画面から取得したユーザーデータを設定する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetUserToCCMain(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ユーザーデータ設定処理
        If SetUserToCC(dataHBKX0701) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】CC情報ユーザー設定処理
    ''' </summary>
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CCにサブ検索で選択されたユーザーを設定する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetUserToCC(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX0701

                'サブ検索画面で1件以上選択された場合に値を設定
                If .PropDtResultSub IsNot Nothing Then
                    '選択データ数分繰り返し、宛先に追加する
                    For i As Integer = 0 To .PropDtResultSub.Rows.Count - 1
                        'ユーザーが既に設定されている場合は追加しない
                        If .ProptxtCC.Text.Contains(.PropDtResultSub.Rows(i).Item(4)) = False Then
                            If .ProptxtCC.Text = "" Then
                                .ProptxtCC.Text = .PropDtResultSub.Rows(i).Item(4)
                            Else
                                .ProptxtCC.Text &= "; " & .PropDtResultSub.Rows(i).Item(4)
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
    ''' BCCユーザー追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>BCCにサブ検索画面から取得したユーザーデータを設定する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetUserToBCCMain(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ユーザーデータ設定処理
        If SetUserToBCC(dataHBKX0701) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】BCC情報ユーザー設定処理
    ''' </summary>
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CCにサブ検索で選択されたユーザーを設定する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetUserToBCC(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX0701

                'サブ検索画面で1件以上選択された場合に値を設定
                If .PropDtResultSub IsNot Nothing Then
                    '選択データ数分繰り返し、宛先に追加する
                    For i As Integer = 0 To .PropDtResultSub.Rows.Count - 1
                        'ユーザーが既に設定されている場合は追加しない
                        If .ProptxtBcc.Text.Contains(.PropDtResultSub.Rows(i).Item(4)) = False Then
                            If .ProptxtBcc.Text = "" Then
                                .ProptxtBcc.Text = .PropDtResultSub.Rows(i).Item(4)
                            Else
                                .ProptxtBcc.Text &= "; " & .PropDtResultSub.Rows(i).Item(4)
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
    ''' 【共通】システムエラー事前対応処理
    ''' </summary>
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システムエラー発生時に非活性にするコントロールリストを作成する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DoProcForErrorMain(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'トランザクション系コントロールリスト作成
        If CreateTsxCtlList(dataHBKX0701) = False Then
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
    ''' <param name="dataHBKX0701">[IN/OUT]メールテンプレートマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>トランザクション系コントロールのリストを作成する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateTsxCtlList(ByRef dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim aryCtlList As New ArrayList

        Try

            With dataHBKX0701

                'トランザクション系のコントロールをリストに追加
                aryCtlList.Add(.PropBtnReg)              '登録ボタン
                aryCtlList.Add(.PropBtnDelete)           '削除ボタン
                aryCtlList.Add(.PropBtnDeleteKaijyo)     '削除解除ボタン
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

End Class
