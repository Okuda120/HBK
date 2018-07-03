Imports Common
Imports CommonHBK
Imports HBKZ

''' <summary>
''' メールテンプレートマスター登録画面Interfaceクラス
''' </summary>
''' <remarks>メールテンプレートマスター登録画面の設定を行う
''' <para>作成情報：2012/08/21 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKX0701

    'インスタンス生成
    Public dataHBKX0701 As New DataHBKX0701
    Private logicHBKX0701 As New LogicHBKX0701
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    '変数宣言
    Private blnLoadFlg As Boolean           'Load時フラグ

    ''' <summary>
    ''' フォームロード時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>メールテンプレートマスター登録画面の初期設定を行う
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKX0701_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'イベント処理制御初期処理
        blnLoadFlg = True

        'データクラスの初期設定を行う
        With dataHBKX0701

            .PropGrpLoginUser = Me.grpLoginUser                 'ログインユーザーグループボックス

            .ProptxtTemplateNmb = Me.txtTemplateNmb             '基本情報：テンプレート番号テキストボックス
            .ProptxtTemplateNM = Me.txtTemplateNM               '基本情報：テンプレート名テキストボックス
            .PropcmbPriorityKbn = Me.cmbPriorityKbn             '基本情報：重要度コンボボックス
            .PropcmbProcessKbn = Me.cmbProcessKbn               '基本情報：プロセス区分コンボボックス
            .PropgrpKigenCond = Me.grpKigenCond                 '基本情報：期限切れお知らせ条件グループボックス
            .PropcmbKigenCondCIKbnCD = Me.cmbKigenCondCIKbnCD   '基本情報：期限切れ条件CI種別コンボボックス
            .PropcmbKigenCondTypeKbn = Me.cmbKigenCondTypeKbn   '基本情報：期限切れ条件タイプコンボボックス
            .ProprdoKigenCondKbn = Me.rdoKigenCondKbn           '基本情報：期限切れ条件区分ラジオボタン
            .PropcmbKigenCondKigen = Me.cmbKigenCondKigen       '基本情報：期限切れ条件期限コンボボックス
            .ProprdoKigenCondUsrID = Me.rdoKigenCondUsrID       '基本情報：期限切れ条件区分ユーザーIDラジオボタン
            .ProptxtTitle = Me.txtTitle                         '基本情報：件名テキストボックス
            .ProptxtMailFrom = Me.txtMailFrom                   '基本情報：差出人テキストボックス
            .ProptxtMailTo = Me.txtMailTo                       '基本情報：TOテキストボックス
            .ProptxtCC = Me.txtCC                               '基本情報：CCテキストボックス
            .ProptxtBcc = Me.txtBcc                             '基本情報：Bccテキストボックス
            .ProptxtText = Me.txtText                           '基本情報：本文テキストボックス

            .PropBtnMailFromSearch = Me.btnMailFromSearch       '基本情報：差出人選択ボタン    
            .PropBtnMailToSearch = Me.btnMailToSearch           '基本情報：TO追加ボタン  
            .PropBtnCCSearch = Me.btnCCSearch                   '基本情報：CC追加ボタン  
            .PropBtnBccSearch = Me.btnBccSearch                 '基本情報：Bcc追加ボタン 

            .PropBtnReg = Me.btnReg                             'フッタ：登録ボタン
            .PropBtnDelete = Me.btnDelete                       'フッタ：削除ボタン
            .PropBtnDeleteKaijyo = Me.btnDeletekaijyo           'フッタ：削除解除ボタン

        End With

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'システムエラー事前対応処理
        If logicHBKX0701.DoProcForErrorMain(dataHBKX0701) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        '処理モードに応じて画面初期表示を行う
        If dataHBKX0701.PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

            '新規モード画面初期表示メイン処理
            If logicHBKX0701.InitFormNewModeMain(dataHBKX0701) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKX0701.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

        ElseIf dataHBKX0701.PropStrProcMode = PROCMODE_EDIT Then        '編集モード  

            '編集モード画面初期表示メイン処理
            If logicHBKX0701.InitFormEditModeMain(dataHBKX0701) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKX0701.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

        End If

        'イベント処理制御後処理
        blnLoadFlg = False

    End Sub

    ''' <summary>
    ''' [登録]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>入力チェックを行い、モードに応じて登録処理を行う。
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        '処理モードに応じた入力チェックを行う
        If dataHBKX0701.PropStrProcMode = PROCMODE_NEW Then                 '新規登録モード

            '入力チェック処理
            If logicHBKX0701.CheckInputValueMain(dataHBKX0701) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKX0701.PropAryTsxCtlList) = False Then
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

        ElseIf dataHBKX0701.PropStrProcMode = PROCMODE_EDIT Then            '編集モード

            '入力チェック処理
            If logicHBKX0701.CheckInputValueMain(dataHBKX0701) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKX0701.PropAryTsxCtlList) = False Then
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

        '確認ダイアログ
        If MsgBox(String.Format(X0701_W001), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default
            Exit Sub
        End If


        '処理モードに応じた登録処理を行う
        If dataHBKX0701.PropStrProcMode = PROCMODE_NEW Then                 '新規登録モード

            '新規登録処理
            If logicHBKX0701.RegistDataOnNewModeMain(dataHBKX0701) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKX0701.PropAryTsxCtlList) = False Then
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

        ElseIf dataHBKX0701.PropStrProcMode = PROCMODE_EDIT Then            '編集モード

            '更新処理
            If logicHBKX0701.RegistDataOnEditModeMain(dataHBKX0701) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKX0701.PropAryTsxCtlList) = False Then
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

        '編集モードで画面再描画
        dataHBKX0701.PropStrProcMode = PROCMODE_EDIT
        HBKX0701_Load(Me, New EventArgs)

        'マウスポインタ変更(砂時計→通常)
        Me.Cursor = Windows.Forms.Cursors.Default

        '登録完了メッセージ表示
        MsgBox(X0701_I001, MsgBoxStyle.Information, TITLE_INFO)

    End Sub

    ''' <summary>
    ''' [戻る]ボタン押下時処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>当画面を閉じて呼び出し元画面へ遷移する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReturn.Click
        'この画面を閉じる
        Me.Close()
    End Sub

    ''' <summary>
    ''' [削除]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>入力チェックを行い、モードに応じて登録処理を行う。
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        '確認ダイアログ
        If MsgBox(String.Format(X0701_W002), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
            Exit Sub
        End If

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        If dataHBKX0701.PropStrProcMode = PROCMODE_EDIT Then            '編集モード

            dataHBKX0701.PropStrJtiFlg = DELETE_MODE_MUKO               '削除フラグ

            '更新処理
            If logicHBKX0701.RegistDataOnDelModeMain(dataHBKX0701) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKX0701.PropAryTsxCtlList) = False Then
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

        '編集モードで画面再描画
        dataHBKX0701.PropStrProcMode = PROCMODE_EDIT
        HBKX0701_Load(Me, New EventArgs)

        'マウスポインタ変更(砂時計→通常)
        Me.Cursor = Windows.Forms.Cursors.Default

        '削除完了メッセージ表示
        MsgBox(X0701_I002, MsgBoxStyle.Information, TITLE_INFO)

    End Sub

    ''' <summary>
    ''' [削除解除]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>入力チェックを行い、モードに応じて登録処理を行う。
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnDeletekaijyo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeletekaijyo.Click

        '確認ダイアログ
        If MsgBox(String.Format(X0701_W003), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
            Exit Sub
        End If

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        If dataHBKX0701.PropStrProcMode = PROCMODE_EDIT Then            '編集モード

            dataHBKX0701.PropStrJtiFlg = DELETE_MODE_YUKO               '削除フラグ

            '更新処理
            If logicHBKX0701.RegistDataOnDelModeMain(dataHBKX0701) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKX0701.PropAryTsxCtlList) = False Then
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

        '編集モードで画面再描画
        dataHBKX0701.PropStrProcMode = PROCMODE_EDIT
        HBKX0701_Load(Me, New EventArgs)

        'マウスポインタ変更(砂時計→通常)
        Me.Cursor = Windows.Forms.Cursors.Default

        '削除解除完了メッセージ表示
        MsgBox(X0701_I003, MsgBoxStyle.Information, TITLE_INFO)

    End Sub

    ''' <summary>
    ''' 差出人の選択ボタンクリック時
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>エンドユーザー検索一覧画面へ遷移する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnMailFromSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMailFromSearch.Click
        'ひびきユーザ検索一覧画面のインスタンス
        Dim HBKZ0101 As New HBKZ0101

        'パラメータセット
        With HBKZ0101.dataHBKZ0101
            .PropMode = SELECT_MODE_SINGLE                  'モード：単一選択
            .PropArgs = ""                                  '検索条件：
            .PropSplitMode = SPLIT_MODE_AND                 '検索条件区切り：AND
        End With

        'ひびきユーザ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKX0701.PropDtResultSub = HBKZ0101.ShowDialog()

        '差出人に取得データをセット
        If dataHBKX0701.PropDtResultSub IsNot Nothing Then
            If logicHBKX0701.SetUserFromMailMain(dataHBKX0701) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If
        End If
    End Sub

    ''' <summary>
    ''' TOの追加ボタンクリック時
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>エンドユーザー検索一覧画面へ遷移する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnMailToSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMailToSearch.Click
        Dim HBKZ0201 As New HBKZ0201

        'パラメータセット
        With HBKZ0201.dataHBKZ0201
            .PropMode = SELECT_MODE_MULTI                   'モード：複数選択
            .PropArgs = ""                                  '検索条件：
            .PropSplitMode = SPLIT_MODE_AND                 '検索条件区切り：AND
        End With

        'エンドユーザ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKX0701.PropDtResultSub = HBKZ0201.ShowDialog()

        '宛先に取得データをセット
        If logicHBKX0701.SetUserToMailToMain(dataHBKX0701) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' CCの追加ボタンクリック時
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>エンドユーザー検索一覧画面へ遷移する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnCCSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCCSearch.Click
        Dim HBKZ0201 As New HBKZ0201

        'パラメータセット
        With HBKZ0201.dataHBKZ0201
            .PropMode = SELECT_MODE_MULTI                   'モード：複数選択
            .PropArgs = ""                                  '検索条件：
            .PropSplitMode = SPLIT_MODE_AND                 '検索条件区切り：AND
        End With

        'エンドユーザ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKX0701.PropDtResultSub = HBKZ0201.ShowDialog()

        'CCに取得データをセット
        If logicHBKX0701.SetUserToCCMain(dataHBKX0701) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' BCCの追加ボタンクリック時
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>エンドユーザー検索一覧画面へ遷移する
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnBccSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBccSearch.Click
        Dim HBKZ0201 As New HBKZ0201

        'パラメータセット
        With HBKZ0201.dataHBKZ0201
            .PropMode = SELECT_MODE_MULTI                   'モード：複数選択
            .PropArgs = ""                                  '検索条件：
            .PropSplitMode = SPLIT_MODE_AND                 '検索条件区切り：AND
        End With

        'エンドユーザ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKX0701.PropDtResultSub = HBKZ0201.ShowDialog()

        'BCCに取得データをセット
        If logicHBKX0701.SetUserToBccMain(dataHBKX0701) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' プロセス区分選択時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>期限切れお知らせ条件のコントロールの活性、非活性を行う。
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub cmbProcessKbn_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProcessKbn.SelectedIndexChanged
        'Load処理時かチェックする
        If blnLoadFlg = False Then
            'プロセス区分選択時
            If logicHBKX0701.rdoAbleMain(dataHBKX0701) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If
        End If
    End Sub

    ''' <summary>
    ''' CI種別選択時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>期限切れ条件タイプのコントロールの活性、非活性を行う。
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub cmbKigenCondCIKbnCD_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbKigenCondCIKbnCD.SelectedIndexChanged
        'Load処理時かチェックする
        If blnLoadFlg = False Then
            'CI種別選択時
            If logicHBKX0701.rdoAbleMain(dataHBKX0701) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If
        End If
    End Sub

End Class