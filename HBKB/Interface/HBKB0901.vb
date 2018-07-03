Imports Common
Imports CommonHBK

''' <summary>
''' 導入画面Interfaceクラス
''' </summary>
''' <remarks>導入画面の設定を行う
''' <para>作成情報：2012/07/13 h.sasaki
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKB0901

    'インスタンス作成
    Public dataHBKB0901 As New DataHBKB0901
    Private logicHBKB0901 As New LogicHBKB0901
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    '変数宣言
    Private blnrdoIntroductKbn As Boolean    'ラジオボタンLoad時フラグ

    ''' <summary>
    ''' フォームロード時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>モードに応じて画面の初期設定を行う
    ''' <para>作成情報：2012/07/13 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKB0901_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'ラジオボタンイベント処理制御初期処理
        blnrdoIntroductKbn = True

        'データクラスの初期設定を行う
        With dataHBKB0901
            .PropGrpLoginUser = Me.grpLoginUser             'ログイン：ログイン情報グループボックス
            .ProptxtIntroductNmb = Me.txtIntroductNmb       'ヘッダ：導入番号テキストボックス
            .PropcmbKindNM = Me.cmbKindNM                   '基本情報：種別コンボボックス
            .ProptxtSetNmb = Me.txtSetNmb                   '基本情報：台数テキストボックス
            .ProptxtKikiNmbFrom = Me.txtKikiNmbFrom         '基本情報：機器番号（From）テキストボックス
            .ProptxtKikiNmbTo = Me.txtKikiNmbTo             '基本情報：機器番号（To）テキストボックス
            .ProptxtClass1 = Me.txtClass1                   '基本情報：分類１テキストボックス
            .ProptxtClass2 = Me.txtClass2                   '基本情報：分類２（メーカー）テキストボックス
            .ProptxtCINM = Me.txtCINM                       '基本情報：名称（機種）テキストボックス
            .ProptxtKataban = Me.txtKataban                 '基本情報：型番テキストボックス
            .PropdtpIntroductStDT = Me.dtpIntroductStDT     '基本情報：導入開始日テキストボックス
            .PropcmbSCKikiType = Me.cmbSCKikiType           '基本情報：タイプコンボボックス
            .PropchkSCHokanKbn = Me.chkSCHokanKbn           '基本情報：サービスセンター保管機チェックボックス
            .ProptxtFuzokuhin = Me.txtFuzokuhin             '基本情報：付属品テキストボックス
            .ProptxtIntroductBiko = Me.txtIntroductBiko     '基本情報：導入備考テキストボックス
            .PropchkIntroductDelKbn = Me.chkIntroductDelKbn '基本情報：導入廃棄完了チェックボックス
            .ProprdoHosyoUmu0 = Me.rdoHosyoUmu0             '保証情報：保証書有無「無」ラジオボタン
            .ProprdoHosyoUmu1 = Me.rdoHosyoUmu1             '保証情報：保証書有無「有」ラジオボタン
            .ProprdoHosyoUmu2 = Me.rdoHosyoUmu2             '保証情報：保証書有無「不明」ラジオボタン
            .ProptxtHosyoPlace = Me.txtHosyoPlace           '保証情報：保証書保管場所テキストボックス
            .PropdtpHosyoDelDT = Me.dtpHosyoDelDT           '保証情報：保証書廃棄日テキストボックス
            .ProptxtMakerHosyoTerm = Me.txtMakerHosyoTerm   '保証情報：メーカー無償保証期間テキストボックス
            .ProptxtEOS = Me.txtEOS                         '保証情報：EOSテキストボックス
            .ProprdoIntroductKbn0 = Me.rdoIntroductKbn0     '購入・リース情報：導入タイプ「経費購入」ラジオボタン
            .ProprdoIntroductKbn1 = Me.rdoIntroductKbn1     '購入・リース情報：導入タイプ「リース」ラジオボタン
            .PropdtpDelScheduleDT = Me.dtpDelScheduleDT     '購入・リース情報：廃棄予定日テキストボックス
            .ProptxtLeaseCompany = Me.txtLeaseCompany       '購入・リース情報：リース会社テキストボックス
            .ProptxtLeaseNmb = Me.txtLeaseNmb               '購入・リース情報：リース番号テキストボックス
            .PropdtpLeaseUpDT = Me.dtpLeaseUpDT             '購入・リース情報：期限日テキストボックス
            .PropBtnReg = Me.btnReg                         'フッタ：登録ボタン
            .PropBtnBack = Me.btnBack                       'フッタ：戻るボタン
        End With

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'システムエラー事前対応処理
        If LogicHBKB0901.DoProcForErrorMain(dataHBKB0901) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        '処理モードに応じて画面初期表示を行う
        If dataHBKB0901.PropStrProcMode = PROCMODE_NEW Then             '新規モード

            '新規モード画面初期表示メイン処理
            If LogicHBKB0901.InitFormNewModeMain(dataHBKB0901) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0901.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

        ElseIf dataHBKB0901.PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード  

            'ロック設定メイン処理
            If logicHBKB0901.LockMain(dataHBKB0901) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0901.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            If dataHBKB0901.PropBlnBeLockedFlg = False Then           '編集モード
                '編集モード画面初期表示メイン処理
                If logicHBKB0901.InitFormEditModeMain(dataHBKB0901) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB0901.PropAryTsxCtlList) = False Then
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            Exit Sub
                        End If
                    End If
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If
            ElseIf dataHBKB0901.PropBlnBeLockedFlg = True Then       '参照（ロック）モード
                '参照モード画面初期表示メイン処理
                If logicHBKB0901.InitFormRefModeMain(dataHBKB0901) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB0901.PropAryTsxCtlList) = False Then
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            Exit Sub
                        End If
                    End If
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If

                'ロックメッセージ表示
                MsgBox(dataHBKB0901.PropStrBeLockedMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, TITLE_INFO)
            End If
        End If

        'ラジオボタンイベント処理制御後処理
        blnrdoIntroductKbn = False

    End Sub

    ''' <summary>
    ''' [登録]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>入力チェックを行い、新規の場合は、登録内容を保持して変更理由登録画面へ遷移する　編集の場合は、直接登録する
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        '処理モードに応じた入力チェックを行う
        If dataHBKB0901.PropStrProcMode = PROCMODE_NEW Then                 '新規登録モード

            '変数宣言
            Dim HBKB0301 As HBKB0301 = Nothing  '変更理由登録画面

            '入力チェック処理
            If logicHBKB0901.CheckInputValueMain(dataHBKB0901) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0901.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default

            '変更理由登録画面のインスタンス作成
            HBKB0301 = New HBKB0301

            '変更理由登録画面のデータクラスにパラメータをセット
            With HBKB0301.dataHBKB0301
                .PropStrRegMode = REG_MODE_BLANK    '登録モード：なし
            End With

            '変更理由登録画面へ遷移（確認メッセージなし）
            Me.Hide()
            If HBKB0301.ShowDialog() = DIALOG_RETURN_CANCEL Then
                'キャンセルボタンクリック時は画面を表示して処理終了
                Me.Show()
                Exit Sub
            End If
            '変更理由登録画面からデータを取得
            With HBKB0301.dataHBKB0301
                dataHBKB0901.PropStrRegReason = .PropStrRegReason   '理由格納用
                dataHBKB0901.PropDtCauseLink = .PropDtCauseLink     '原因リンクデータ格納用
            End With
            Me.Show()

            'マウスポインタ変更(通常→砂時計)
            Me.Cursor = Windows.Forms.Cursors.WaitCursor

        ElseIf dataHBKB0901.PropStrProcMode = PROCMODE_EDIT Then            '編集／参照（ロック）モード

            'ロック解除チェック
            If logicHBKB0901.CheckBeUnlockedMain(dataHBKB0901) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0901.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            'ロック解除フラグに応じて処理を行う
            If dataHBKB0901.PropBlnBeLockedFlg = False Then             '編集モード

                'ロック解除されていない時、入力チェックを行う
                If logicHBKB0901.CheckInputValueMain(dataHBKB0901) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB0901.PropAryTsxCtlList) = False Then
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            Exit Sub
                        End If
                    End If
                    'マウスポインタ変更(砂時計→通常)
                    Me.Cursor = Windows.Forms.Cursors.Default
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If

            ElseIf dataHBKB0901.PropBlnBeLockedFlg = True Then         '参照（ロック）モード

                'フラグをON
                dataHBKB0901.PropBlnLockCompare = True

                'ロック解除時、ログ出力処理と画面の再描画を行う
                If logicHBKB0901.SetFormRefModeFromEditModeMain(dataHBKB0901) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB0901.PropAryTsxCtlList) = False Then
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            Exit Sub
                        End If
                    End If
                    'マウスポインタ変更(砂時計→通常)
                    Me.Cursor = Windows.Forms.Cursors.Default
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If

                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default

                'ロック解除メッセージ表示
                MsgBox(dataHBKB0901.PropStrBeUnlockedMsg, MsgBoxStyle.Exclamation, TITLE_WARNING)
                Exit Sub

            End If

        End If

        '処理モードに応じた登録処理を行う
        If dataHBKB0901.PropStrProcMode = PROCMODE_NEW Then                 '新規登録モード

            '新規登録処理
            If logicHBKB0901.RegistDataOnNewModeMain(dataHBKB0901) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0901.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            '編集モードで画面再描画
            dataHBKB0901.PropStrProcMode = PROCMODE_EDIT
            HBKB0901_Load(Me, New EventArgs)

        ElseIf dataHBKB0901.PropStrProcMode = PROCMODE_EDIT Then            '編集／参照（ロックモード）

            'ロック解除チェック
            If logicHBKB0901.CheckBeUnlockedMain(dataHBKB0901) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0901.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            'ロック解除フラグに応じて処理を行う
            If dataHBKB0901.PropBlnBeLockedFlg = False Then             '編集モード

                'ロック解除されていない時、更新処理を行う
                If logicHBKB0901.RegistDataOnEditModeMain(dataHBKB0901) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB0901.PropAryTsxCtlList) = False Then
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            Exit Sub
                        End If
                    End If
                    'マウスポインタ変更(砂時計→通常)
                    Me.Cursor = Windows.Forms.Cursors.Default
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If

                '編集モードで画面再描画
                dataHBKB0901.PropStrProcMode = PROCMODE_EDIT
                HBKB0901_Load(Me, New EventArgs)

            ElseIf dataHBKB0901.PropBlnBeLockedFlg = True Then         '参照（ロック）モード

                'フラグをON
                dataHBKB0901.PropBlnLockCompare = True

                'ロック解除時、ログ出力処理と画面の再描画を行う
                If logicHBKB0901.SetFormRefModeFromEditModeMain(dataHBKB0901) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB0901.PropAryTsxCtlList) = False Then
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            Exit Sub
                        End If
                    End If
                    'マウスポインタ変更(砂時計→通常)
                    Me.Cursor = Windows.Forms.Cursors.Default
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If

                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default

                'ロック解除メッセージ表示
                MsgBox(dataHBKB0901.PropStrBeUnlockedMsg, MsgBoxStyle.Exclamation, TITLE_WARNING)
                Exit Sub

            End If

        End If

        'マウスポインタ変更(砂時計→通常)
        Me.Cursor = Windows.Forms.Cursors.Default

        '登録完了メッセージ表示
        MsgBox(B0901_I001, MsgBoxStyle.Information, TITLE_INFO)

    End Sub

    ''' <summary>
    ''' [戻る]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>当画面を閉じて呼び出し元画面へ遷移する
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click

        Me.Close()

    End Sub

    ''' <summary>
    ''' [導入タイプ]ラジオボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>「経費購入」ラジオボタンクリック時
    '''            [廃棄予定日]テキストボックスを活性化
    '''            [リース会社]テキストボックスを非活性化
    '''            [リース番号]テキストボックスを非活性化
    '''            [期限日]テキストボックスを非活性化
    '''          「リース」ラジオボタンクリック時
    '''            [廃棄予定日]テキストボックスを非活性化
    '''            [リース会社]テキストボックスを活性化
    '''            [リース番号]テキストボックスを活性化
    '''            [期限日]テキストボックスを活性化
    ''' <para>作成情報：2012/07/14 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub rdoIntroductKbn_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoIntroductKbn0.CheckedChanged, rdoIntroductKbn1.CheckedChanged

        Dim rb As RadioButton = TryCast(sender, RadioButton)

        If rb IsNot Nothing AndAlso rb.Checked Then
            'Load処理時かチェックする
            If blnrdoIntroductKbn = False Then
                '導入タイプラジオボタンの値をチェックする
                If logicHBKB0901.CheckRadioIntroductKbn(dataHBKB0901) = False Then
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If
            End If
        End If

    End Sub


    ''' <summary>
    ''' フォームクローズ時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>編集モードの場合はロック解除を行う
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKB0901_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        '編集モードの場合はロック解除を行う
        If dataHBKB0901.PropStrProcMode = PROCMODE_EDIT And _
            (dataHBKB0901.PropBlnBeLockedFlg = False And dataHBKB0901.PropBlnBeLockedFlg = False) Then

            '画面クローズ時ロック解除処理
            If logicHBKB0901.UnlockWhenCloseMain(dataHBKB0901) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0901.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

        End If

    End Sub


    ''' <summary>
    ''' [解除]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>当画面のロックを解除し、編集モードで表示する
    ''' <para>作成情報：2012/07/23 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnUnlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grpLoginUser.btnUnlockClick

        'ロック解除処理を行う
        If logicHBKB0901.UnlockWhenClickBtnUnlockMain(dataHBKB0901) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0901.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub
End Class