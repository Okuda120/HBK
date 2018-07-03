Imports Common
Imports CommonHBK
Imports HBKZ
Imports FarPoint.Win.Spread

''' <summary>
''' サポセン機器登録画面Interfaceクラス
''' </summary>
''' <remarks>サポセン機器登録画面の設定を行う
''' <para>作成情報：2012/07/10 t.fukuo
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKB0601

    'インスタンス作成
    Public dataHBKB0601 As New DataHBKB0601
    Private logicHBKB0601 As New LogicHBKB0601
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    ''' <summary>
    ''' フォーム終了時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>画面情報の保存を行う
    ''' <para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKB0601_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        '設定を変更する
        With Settings.Instance
            'ウィンドウが最大化、最小化以外は保存
            If Me.WindowState = FormWindowState.Normal Then
                .propHBKB0601_Height = Me.Size.Height
                .propHBKB0601_Width = Me.Size.Width
                .propHBKB0601_Y = Me.Location.Y
                .propHBKB0601_X = Me.Location.X
                .propHBKB0601_WindowState = Me.WindowState
            ElseIf Me.WindowState = FormWindowState.Maximized Then
                '最大化は状態を保存
                .propHBKB0601_WindowState = Me.WindowState
            End If
        End With
        '現在の設定をXMLファイルに保存する
        Settings.SaveToXmlFile()
    End Sub

    ''' <summary>
    ''' フォームロード時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>モードに応じて画面の初期設定を行う
    ''' <para>作成情報：2012/07/10 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKB0601_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'XMLファイルから設定を読み込む
        Settings.LoadFromXmlFile()
        '最大化とか
        Me.WindowState = Settings.Instance.propHBKB0601_WindowState
        'サイズが0だったら初期状態とみなし通常の表示をする。
        If Settings.Instance.propHBKB0601_Height <> 0 Then
            'Me.StartPosition = FormStartPosition.Manual
            'サイズ変更
            Me.Size = New Point(Settings.Instance.propHBKB0601_Width, Settings.Instance.propHBKB0601_Height)
            Me.Location = New Point(Settings.Instance.propHBKB0601_X, Settings.Instance.propHBKB0601_Y)
        End If

        'データクラスの初期設定を行う
        With dataHBKB0601

            'フォームオブジェクト
            .PropLblkanryoMsg = Me.LblkanryoMsg                 '完了メッセージ
            .PropGrpLoginUser = Me.grpLoginUser                 'ログイン：ログイン情報グループボックス
            .PropGrpCIKhn = Me.grpCIKhn                         'ヘッダ：CI基本情報グループボックス
            .PropLblCINmb = Me.lblCINmb                         'ヘッダ：CI番号ラベル
            .PropLblCIKbnNM = Me.lblCIKbnNM                     'ヘッダ：CI種別名ラベル
            .PropLblTitleRirekiNo = Me.lblTitleRirekiNo         'ヘッダ：履歴番号タイトルラベル
            .PropLblValueRirekiNo = Me.lblValueRirekiNo         'ヘッダ：履歴番号値ラベル
            .PropTbInput = Me.tbInput                           'タブ
            .PropCmbKind = Me.cmbKind                           '基本情報：種別コンボボックス
            .PropTxtNum = Me.txtNum                             '基本情報：CI番号テキストボックス
            .PropTxtClass1 = Me.txtClass1                       '基本情報：分類１テキストボックス
            .PropTxtClass2 = Me.txtClass2                       '基本情報：分類２テキストボックス
            .PropTxtCINM = Me.txtCINM                           '基本情報：CI種別名称テキストボックス
            .PropTxtKataban = Me.txtKataban                     '基本情報：型番テキストボックス
            .PropCmbType = Me.cmbType                           '基本情報：タイプコンボボックス
            .PropCmbCIStatus = Me.cmbCIStatus                   '基本情報：ステータスコンボボックス
            .PropChkSCHokanKbn = Me.chkSCHokanKbn               '基本情報：サービスセンター保管機チェックボックス
            .PropTxtSerial = Me.txtSerial                       '基本情報：製造番号テキストボックス
            .PropTxtMacAddress1 = Me.txtMacAddress1             '基本情報：MACアドレス１テキストボックス
            .PropTxtMacAddress2 = Me.txtMacAddress2             '基本情報：MACアドレス２テキストボックス
            .PropTxtImageNmb = Me.txtImageNmb                   '基本情報：イメージ番号テキストボックス
            .PropTxtMemorySize = Me.txtMemorySize               '基本情報：メモリ容量テキストボックス
            .PropTxtSCKikiFixNmb = Me.txtSCKikiFixNmb           '基本情報：サポセン固定資産番号テキストボックス
            .PropDtpLeaseUpDT_Kiki = Me.dtpLeaseUpDT_Kiki       '基本情報：リース期限日（機器）
            .PropTxtFuzokuhin = Me.txtFuzokuhin                 '基本情報：付属品テキストボックス
            .PropTxtKikiState = Me.txtKikiState                 '基本情報：機器状態テキストボックス
            .PropTxtCINaiyo = Me.txtCINaiyo                     '基本情報：説明テキストボックス
            .PropTxtIntroductNmb = Me.txtIntroductNmb           '基本情報：導入番号テキストボックス
            .PropDtpIntroductStDT = Me.dtpIntroductStDT         '基本情報：導入開始日
            .PropTxtMakerHosyoTerm = Me.txtMakerHosyoTerm       '基本情報：メーカー無償（保証期間）
            .PropTxtEOS = Me.txtEOS                             '基本情報：EOSテキストボックス
            .PropCmbIntroductKbn = Me.cmbIntroductKbn           '基本情報：導入タイプコンボボックス
            .PropTxtLeaseCompany = Me.txtLeaseCompany           '基本情報：リース会社テキストボックス
            .PropDtpDelScheduleDT = Me.dtpDelScheduleDT         '基本情報：廃棄予定日
            .PropDtpLeaseUpDT_Int = Me.dtpLeaseUpDT_Int         '基本情報：リース期限日（導入）
            .PropCmbHosyoUmu = Me.cmbHosyoUmu                   '基本情報：保証書コンボボックス
            .PropChkIntroductDelKbn = Me.chkIntroductDelKbn     '基本情報：導入廃棄完了チェックボックス
            .PropLblKindNM = Me.lblKindNM                       '利用情報：種別ラベル
            .PropLblNum_Riyo = Me.lblNum_Riyo                   '利用情報：番号ラベル
            .PropTxtUsrID = Me.txtUsrID                         '利用情報：ユーザーIDテキストボックス
            .PropTxtUsrNM = Me.txtUsrNM                         '利用情報：ユーザー氏名テキストボックス
            .PropBtnSearch_Usr = Me.btnSearch_Usr               '利用情報：ユーザー検索ボタン
            .PropTxtUsrMailAdd = Me.txtUsrMailAdd               '利用情報：ユーザーメールアドレステキストボックス
            .PropTxtUsrTel = Me.txtUsrTel                       '利用情報：ユーザー電話番号テキストボックス
            .PropTxtUsrKyokuNM = Me.txtUsrKyokuNM               '利用情報：ユーザー所属局テキストボックス
            .PropTxtUsrBusyoNM = Me.txtUsrBusyoNM               '利用情報：ユーザー所属部署テキストボックス
            .PropTxtUsrCompany = Me.txtUsrCompany               '利用情報：ユーザー所属会社テキストボックス
            .PropTxtUsrContact = Me.txtUsrContact               '利用情報：ユーザー連絡先テキストボックス
            .PropTxtUsrRoom = Me.txtUsrRoom                     '利用情報：ユーザー番組/部屋テキストボックス
            .PropVwShare = Me.vwShare                           '利用情報：複数人利用スプレッド
            .PropBtnAddRow_Share = Me.btnAddRow_Share           '利用情報：複数人利用スプレッド行追加ボタン
            .PropBtnRemoveRow_Share = Me.btnRemoveRow_Share     '利用情報：複数人利用スプレッド行削除ボタン
            .PropDtpRentalStDT = Me.dtpRentalStDT               '利用情報：レンタル期間（開始日）
            .PropDtpRentalEdDT = Me.dtpRentalEdDT               '利用情報：レンタル期間（終了日）
            '[Add] 2012/10/24 s.yamaguchi START
            .PropBtnGetOneYearLater_CMonth = Me.btnGetOneYearLater_CMonth   '利用情報：1年後当月末設定ボタン
            .PropBtnGetOneYearLater_LMonth = Me.btnGetOneYearLater_LMonth   '利用情報：1年後先月末設定ボタン
            '[Add] 2012/10/24 s.yamaguchi END
            .PropDtpLastInfoDT = Me.dtpLastInfoDT               '利用情報：最終お知らせ日
            .PropTxtWorkFromNmb = Me.txtWorkFromNmb             '利用情報：作業の元テキストボックス
            .PropCmbKikiUse = Me.cmbKikiUse                     '利用情報：機器利用形態コンボボックス
            .PropCmbIPUse = Me.cmbIPUse                         '利用情報：IP割当種類コンボボックス
            .PropTxtFixedIP = Me.txtFixedIP                     '利用情報：固定IPテキストボックス
            .PropVwOptSoft = Me.vwOptSoft                       '利用情報：オプションソフトスプレッド
            .PropBtnAddRow_OptSoft = Me.btnAddRow_OptSoft       '利用情報：オプションソフト行追加ボタン
            .PropBtnRemoveRow_OptSoft = Me.btnRemoveRow_OptSoft '利用情報：オプションソフト行削除ボタン
            .PropVwSetKiki = Me.vwSetKiki                       '利用情報：セット機器スプレッド
            .PropTxtManageKyokuNM = Me.txtManageKyokuNM         '利用情報：管理局テキストボックス
            .PropTxtManageBusyoNM = Me.txtManageBusyoNM         '利用情報：管理部署テキストボックス
            .PropTxtSetKyokuNM = Me.txtSetKyokuNM               '利用情報：設置局テキストボックス
            .PropTxtSetBusyoNM = Me.txtSetBusyoNM               '利用情報：設置部署テキストボックス
            .PropBtnSearch_Set = Me.btnSearch_Set               '利用情報：設置機器検索ボタン
            .PropTxtSetRoom = Me.txtSetRoom                     '利用情報：設置番組/部屋テキストボックス
            .PropTxtSetBuil = Me.txtSetBuil                     '利用情報：設置建物テキストボックス
            .PropTxtSetFloor = Me.txtSetFloor                   '利用情報：設置フロアテキストボックス
            .PropTxtSetDeskNo = Me.txtSetDeskNo                 '利用情報：設置デスクNoテキストボックス
            .PropTxtSetLANLength = Me.txtSetLANLength           '利用情報：設置LANケーブル長さテキストボックス
            .PropTxtSetLANNum = Me.txtSetLANNum                 '利用情報：設置LANケーブル番号テキストボックス
            .PropTxtSetSocket = Me.txtSetSocket                 '利用情報：情報コンセント・SWテキストボックス
            .PropTxtBIko1 = Me.txtBiko1                         'フリー入力情報：テキスト１テキストボックス
            .PropTxtBIko2 = Me.txtBiko2                         'フリー入力情報：テキスト２テキストボックス
            .PropTxtBIko3 = Me.txtBiko3                         'フリー入力情報：テキスト３テキストボックス
            .PropTxtBIko4 = Me.txtBiko4                         'フリー入力情報：テキスト４テキストボックス
            .PropTxtBIko5 = Me.txtBiko5                         'フリー入力情報：テキスト５テキストボックス
            .PropChkFreeFlg1 = Me.chkFreeFlg1                   'フリー入力情報：フリーフラグ１チェックボックス
            .PropChkFreeFlg2 = Me.chkFreeFlg2                   'フリー入力情報：フリーフラグ２チェックボックス
            .PropChkFreeFlg3 = Me.chkFreeFlg3                   'フリー入力情報：フリーフラグ３チェックボックス
            .PropChkFreeFlg4 = Me.chkFreeFlg4                   'フリー入力情報：フリーフラグ４チェックボックス
            .PropChkFreeFlg5 = Me.chkFreeFlg5                   'フリー入力情報：フリーフラグ５チェックボックス
            .PropTxtCIOwnerNM = Me.txtCIOwnerNM                 '関係情報：CIオーナー名テキストボックス
            .PropLblCIOwnerCD = Me.lblCIOwnerCD                 '関係情報：CIオーナーコードラベル（非表示）
            .PropBtnSearch_Grp = Me.btnSearch_Grp               '関係情報：検索ボタン
            .PropLblRirekiNo = Me.lblRirekiNo                   '変更情報：履歴番号ラベル
            .PropTxtRegReason = Me.txtRegReason                 '変更情報：理由テキストボックス
            .PropVwCauseLink = Me.vwCauseLink                   '変更情報：原因リンクスプレッド
            .PropVwRegReason = Me.vwRegReason                   '履歴情報：履歴情報スプレッド
            .PropBtnReg = Me.btnReg                             'フッタ：登録ボタン

            'フラグ初期値
            .PropBlnTabRiyoVwAllUnabled = False                 '利用情報タブ全スプレッド非活性フラグ：OFF

            '[add] 2012/09/24 y.ikushima 完了メッセージ表示修正 START
            'タイマーのインターバル設定
            Me.timKanryo.Interval = MSG_DISP_TIMER
            .PropLblkanryoMsg.Font = New Font(Me.Font.Name, Me.Font.Size, FontStyle.Bold)
            '[add] 2012/09/24 y.ikushima 完了メッセージ表示修正 END

            '登録時コピーチェックボックス
            .PropChkCopyToIncident = Me.chkCopyToIncident
            .PropChkCopyToSetKiki = Me.chkCopyToSetKiki
            .PropLblSetKiki = Me.lblSetKiki
            .PropLblIncident = Me.lblIncident

        End With

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'システムエラー事前対応処理
        If logicHBKB0601.DoProcForErrorMain(dataHBKB0601) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        If dataHBKB0601.PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード  

            'ロック設定メイン処理
            If logicHBKB0601.LockForEditMain(dataHBKB0601) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0601.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            If dataHBKB0601.PropBlnBeLockedFlg = False Then           '編集モード

                '編集モード画面初期表示メイン処理
                If logicHBKB0601.InitFormEditModeMain(dataHBKB0601) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB0601.PropAryTsxCtlList) = False Then
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            Exit Sub
                        End If
                    End If
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If

            ElseIf dataHBKB0601.PropBlnBeLockedFlg = True Then       '参照（ロック）モード

                '参照モード画面初期表示メイン処理
                If logicHBKB0601.InitFormRefModeMain(dataHBKB0601) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB0601.PropAryTsxCtlList) = False Then
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
                MsgBox(dataHBKB0601.PropStrBeLockedMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, TITLE_INFO)

            End If

        ElseIf dataHBKB0601.PropStrProcMode = PROCMODE_REF Then      '参照モード

            'ロック設定メイン処理
            If logicHBKB0601.GetLockDataForRefMain(dataHBKB0601) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0601.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            '参照モード画面初期表示メイン処理
            If logicHBKB0601.InitFormRefModeMain(dataHBKB0601) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0601.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

        ElseIf dataHBKB0601.PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

            '画面名設定
            Me.Text = B0601_NAME_RIREKI

            '履歴モード画面初期表示メイン処理
            If logicHBKB0601.InitFormRirekiModeMain(dataHBKB0601) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0601.PropAryTsxCtlList) = False Then
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


    End Sub

    ''' <summary>
    ''' [解除]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>当画面のロックを解除し、編集モードで表示する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnUnlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grpLoginUser.btnUnlockClick

        'ロック解除処理を行う
        If logicHBKB0601.UnlockWhenClickBtnUnlockMain(dataHBKB0601) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0601.PropAryTsxCtlList) = False Then
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

    ''' <summary>
    ''' 利用者情報：ユーザーIDテキストボックスキー押下時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>ENTERキー押下時、エンドユーザーマスタを検索し、取得したユーザー情報を当画面にセットする
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub txtUsrID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUsrID.KeyPress

        '編集モードでENTERキー押下時のみ処理を行う
        If (dataHBKB0601.PropStrProcMode = PROCMODE_EDIT And dataHBKB0601.PropBlnBeLockedFlg = False) AndAlso _
            e.KeyChar = ChrW(Keys.Enter) Then

            'マウスポインタ変更(通常→砂時計)
            Me.Cursor = Windows.Forms.Cursors.WaitCursor

            'エンドユーザーマスタを検索し、取得したユーザー情報を当画面にセットする
            If logicHBKB0601.EnterClickOnUsrIDMain(dataHBKB0601) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0601.PropAryTsxCtlList) = False Then
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

        End If

    End Sub

    ''' <summary>
    ''' 利用者情報：[検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>エンドユーザー検索画面を表示し、選択されたユーザー情報を当画面にセットする
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSearch_Usr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch_Usr.Click

        'エンドユーザー検索画面インスタンス作成
        Dim HBKZ0201 As New HBKZ0201

        'パラメータセット
        With HBKZ0201.dataHBKZ0201
            .PropMode = SELECT_MODE_SINGLE              'モード：単一選択
            .PropArgs = Me.txtUsrNM.Text                '検索条件：ユーザー氏名
            .PropSplitMode = SPLIT_MODE_AND             '検索条件区切り：AND
        End With

        'グループ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKB0601.PropDtResultSub = HBKZ0201.ShowDialog()

        'ユーザー情報をセット
        If logicHBKB0601.SetNewUsrDataMain(dataHBKB0601) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0601.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 複数人利用：[＋]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>エンドユーザー検索画面を表示し、選択されたユーザー情報を複数人利用一覧にセットする
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_Share_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_Share.Click

        'エンドユーザー検索画面インスタンス作成
        Dim HBKZ0201 As New HBKZ0201

        'パラメータセット
        With HBKZ0201.dataHBKZ0201
            .PropMode = SELECT_MODE_MULTI           'モード：複数選択
            .PropArgs = String.Empty                '検索条件：なし
            .PropSplitMode = SPLIT_MODE_AND         '検索条件区切り：AND
        End With

        'エンドユーザー検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKB0601.PropDtResultSub = HBKZ0201.ShowDialog()

        '複数人利用一覧に取得データをセット
        If logicHBKB0601.SetUserToVwShareMain(dataHBKB0601) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0601.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 複数人利用：[－]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>複数人利用一覧の選択行を削除する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_Share_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveRow_Share.Click

        '複数人利用一覧選択行削除処理
        If logicHBKB0601.RemoveRowShareMain(dataHBKB0601) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0601.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    '[Add] 2012/10/24 s.yamaguchi START
    ''' <summary>
    ''' [1年後当月末]ボタンクリック時処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>当月の1年後の月末を設定する
    ''' <para>作成情報：2012/10/24 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnGetOneYearLater_CMonth_Click(sender As System.Object, e As System.EventArgs) Handles btnGetOneYearLater_CMonth.Click

        '[DELETE]2013/03/21 t.fukuo システム日付から1年後を設定するよう修正 START
        ''レンタル期間（開始日）が未入力の場合処理を抜ける
        'If Me.dtpRentalStDT.txtDate.Text = "" Then
        '    Exit Sub
        'End If
        '[DELETE]2013/03/21 t.fukuo システム日付から1年後を設定するよう修正 END

        '日付加算処理メイン
        If logicHBKB0601.SetOneYearLaterForCMonthMain(dataHBKB0601) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0601.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub
    '[Add] 2012/10/24 s.yamaguchi END

    '[Add] 2012/10/24 s.yamaguchi START
    ''' <summary>
    ''' [1年後先月末]ボタンクリック時処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>先月の1年後の月末を設定する
    ''' <para>作成情報：2012/10/24 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnGetOneYearLater_LMonth_Click(sender As System.Object, e As System.EventArgs) Handles btnGetOneYearLater_LMonth.Click

        '[DELETE]2013/03/21 t.fukuo システム日付から1年後を設定するよう修正 START
        ''レンタル期間（開始日）が未入力の場合処理を抜ける
        'If Me.dtpRentalStDT.txtDate.Text = "" Then
        '    Exit Sub
        'End If
        '[DELETE]2013/03/21 t.fukuo システム日付から1年後を設定するよう修正 END

        '日付加算処理メイン
        If logicHBKB0601.SetOneYearLaterForLMonthMain(dataHBKB0601) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0601.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub
    '[Add] 2012/10/24 s.yamaguchi END

    ''' <summary>
    ''' オプションソフト：[＋]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>オプションソフト一覧に空行を1行追加する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_OptSoft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_OptSoft.Click

        'オプションソフト一覧空行追加処理
        If logicHBKB0601.AddRowOptSoftMain(dataHBKB0601) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0601.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' オプションソフト：[－]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>オプションソフト一覧の選択行を削除する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_OptSoft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveRow_OptSoft.Click

        'オプションソフト一覧選択行削除処理
        If logicHBKB0601.RemoveRowOptSoftMain(dataHBKB0601) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0601.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ' ''' <summary>
    ' ''' セット機器：[＋]ボタンクリック時の処理
    ' ''' </summary>
    ' ''' <param name="sender">[IN]</param>
    ' ''' <param name="e">[IN]</param>
    ' ''' <remarks>セット機器一覧に空行を1行追加する
    ' ''' <para>作成情報：2012/07/11 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Sub btnAddRow_SetKiki_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_SetKiki.Click

    '    'セット機器一覧空行追加処理
    '    If logicHBKB0601.AddRowSetKikiMain(dataHBKB0601) = False Then
    '        'システムエラー発生時はトランザクション系コントロールを非活性にする
    '        If puErrMsg.StartsWith(HBK_E001) Then
    '            If commonLogicHBK.SetCtlUnabled(dataHBKB0601.PropAryTsxCtlList) = False Then
    '                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
    '                Exit Sub
    '            End If
    '        End If
    '        'エラーメッセージ表示
    '        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
    '        '処理終了
    '        Exit Sub
    '    End If

    'End Sub

    ' ''' <summary>
    ' ''' セット機器：[－]ボタンクリック時の処理
    ' ''' </summary>
    ' ''' <param name="sender">[IN]</param>
    ' ''' <param name="e">[IN]</param>
    ' ''' <remarks>セット機器一覧の選択行を削除する
    ' ''' <para>作成情報：2012/07/11 t.fukuo
    ' ''' <p>改訂情報 : </p>
    ' ''' </para></remarks>
    'Private Sub btnRemoveRow_SetKiki_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveRow_SetKiki.Click

    '    'セット機器一覧選択行削除処理
    '    If logicHBKB0601.RemoveRowSetKikiMain(dataHBKB0601) = False Then
    '        'システムエラー発生時はトランザクション系コントロールを非活性にする
    '        If puErrMsg.StartsWith(HBK_E001) Then
    '            If commonLogicHBK.SetCtlUnabled(dataHBKB0601.PropAryTsxCtlList) = False Then
    '                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
    '                Exit Sub
    '            End If
    '        End If
    '        'エラーメッセージ表示
    '        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
    '        '処理終了
    '        Exit Sub
    '    End If

    'End Sub

    ''' <summary>
    ''' 設置情報：[検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>設置情報検索画面を表示し、選択された設置情報を当画面にセットする
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSearch_Set_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch_Set.Click

        '設置情報検索画面インスタンス作成
        Dim HBKZ0501 As New HBKZ0501

        'パラメータセット
        With HBKZ0501.dataHBKZ0501
            .PropMode = SELECT_MODE_SINGLE              'モード：単一選択
            .PropArgs = Me.txtSetBusyoNM.Text           '検索条件：設置部署
            .PropSplitMode = SPLIT_MODE_AND             '検索条件区切り：AND
        End With

        '設置情報検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKB0601.PropDtResultSub = HBKZ0501.ShowDialog()

        '設置情報をセット
        If logicHBKB0601.SetNewSetDataMain(dataHBKB0601) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0601.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' CIオーナー：[検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>グループ検索画面を表示し、選択されたグループを当画面にセットする
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSearch_Grp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch_Grp.Click

        'グループ検索画面インスタンス作成
        Dim HBKZ0301 As New HBKZ0301

        'パラメータセット
        With HBKZ0301.dataHBKZ0301
            .PropMode = SELECT_MODE_SINGLE            'モード：単一選択
            .PropArgs = Me.txtCIOwnerNM.Text          '検索条件：CIオーナー名
            .PropSplitMode = SPLIT_MODE_AND           '検索条件区切り：AND
        End With

        'グループ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKB0601.PropDtResultSub = HBKZ0301.ShowDialog()

        'CIオーナー名、コードを更新
        If dataHBKB0601.PropDtResultSub IsNot Nothing Then
            Me.txtCIOwnerNM.Text = dataHBKB0601.PropDtResultSub.Rows(0).Item("グループ名")
            Me.lblCIOwnerCD.Text = dataHBKB0601.PropDtResultSub.Rows(0).Item("グループCD")
        End If

    End Sub

    ''' <summary>
    ''' 原因リンク一覧：セルダブルクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択した管理番号の登録画面へ参照モードで遷移する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwCauseLink_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwCauseLink.CellDoubleClick

        '列ヘッダーがクリックされた場合は処理しない
        'また、履歴モード時も処理しない
        If e.ColumnHeader = True Or e.RowHeader = True Or dataHBKB0601.PropStrProcMode = PROCMODE_RIREKI Then
            Exit Sub
        End If

        '変数宣言
        Dim strSelectKbn As String = _
            Me.vwCauseLink.Sheets(0).Cells(e.Row, logicHBKB0601.COL_CAUSELINK_KBN).Value    '選択行の区分（コード）
        Dim strSelectNo As String = _
            Me.vwCauseLink.Sheets(0).Cells(e.Row, logicHBKB0601.COL_CAUSELINK_NO).Value     '選択行の管理番号

        '区分に応じた登録画面へ参照モードで遷移する
        If strSelectKbn = PROCESS_TYPE_INCIDENT Then    '区分がインシデントの場合

            'インシデント登録画面インスタンス作成
            Dim HBKC0201 As New HBKC0201
            'インシデント登録画面データクラスにパラメータをセット
            With HBKC0201.dataHBKC0201
                .PropStrProcMode = PROCMODE_REF     '処理モード：参照
                .PropIntINCNmb = strSelectNo        'インシデント番号：管理番号
            End With
            '画面遷移
            Me.Hide()
            HBKC0201.ShowDialog()
            Me.Show()

        ElseIf strSelectKbn = PROCESS_TYPE_QUESTION Then

            '*********************************
            '* 区分が問題の場合
            '*********************************

            '問題登録画面インスタンス作成
            Dim HBKD0201 As New HBKD0201
            '問題登録画面データクラスにパラメータをセット
            With HBKD0201.dataHBKD0201
                .PropStrProcMode = PROCMODE_REF     '処理モード：参照
                .PropIntPrbNmb = strSelectNo        '管理番号
            End With
            '画面遷移
            Me.Hide()
            HBKD0201.ShowDialog()
            Me.Show()

            '★★★DEBUG★★★
            'MsgBox("問題登録画面に遷移します")

        ElseIf strSelectKbn = PROCESS_TYPE_CHANGE Then

            '*********************************
            '* 区分が変更の場合
            '*********************************

            '変更登録画面インスタンス作成
            Dim HBKE0201 As New HBKE0201
            '変更登録画面データクラスにパラメータをセット
            With HBKE0201.dataHBKE0201
                .PropStrProcMode = PROCMODE_REF     '処理モード：参照
                .PropIntChgNmb = strSelectNo        '管理番号
            End With
            '画面遷移
            Me.Hide()
            HBKE0201.ShowDialog()
            Me.Show()

            '★★★DEBUG★★★
            'MsgBox("変更登録画面に遷移します")

        ElseIf strSelectKbn = PROCESS_TYPE_RELEASE Then

            '*********************************
            '* 区分がリリースの場合
            '*********************************

            'リリース登録画面インスタンス作成
            Dim HBKF0201 As New HBKF0201
            'リリース登録画面データクラスにパラメータをセット
            With HBKF0201.dataHBKF0201
                .PropStrProcMode = PROCMODE_REF     '処理モード：参照
                .PropIntRelNmb = strSelectNo        '管理番号
            End With
            '画面遷移
            Me.Hide()
            HBKF0201.ShowDialog()
            Me.Show()

            '★★★DEBUG★★★
            'MsgBox("リリース登録画面に遷移します")

        End If

    End Sub

    ''' <summary>
    ''' 履歴情報一覧：セルダブルクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択した行のサポセン履歴画面へ遷移する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwRegReason_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwRegReason.CellDoubleClick

        '列ヘッダーがクリックされた場合は処理しない
        'また、履歴モード時も処理しない
        '１行目が選択されても処理を行わない
        If e.ColumnHeader = True Or e.RowHeader = True Or dataHBKB0601.PropStrProcMode = PROCMODE_RIREKI Or e.Row = 0 Then
            Exit Sub
        End If

        '変数宣言
        Dim HBKB0601_R As HBKB0601 = Nothing                                                                'システム登録（履歴）画面
        Dim intSelectRirekiNo As Integer = _
            Integer.Parse(Me.vwRegReason.Sheets(0).Cells(e.Row, logicHBKB0601.COL_REGREASON_UPID).Value)    '選択行の履歴番号

        'システム登録（履歴）画面のインスタンスを作成
        HBKB0601_R = New HBKB0601

        'システム登録（履歴）画面のデータクラスにパラメータをセット
        With HBKB0601_R.dataHBKB0601
            .PropStrProcMode = PROCMODE_RIREKI                      '処理モード：履歴
            .PropStrProcModeFromSap = dataHBKB0601.PropStrProcMode  '呼び出し元（当画面）処理モード
            .PropIntCINmb = dataHBKB0601.PropIntCINmb               'CI番号
            .PropIntRirekiNo = intSelectRirekiNo                    '履歴番号
            .PropIntFromRegSystemFlg = 1                            'システム登録画面遷移フラグON
            .PropBlnBeLockedFlg = dataHBKB0601.PropBlnBeLockedFlg   'ロックフラグ
            .PropStrEdiTime = Me.grpLoginUser.PropLockDate          '編集開始日時      
        End With


        'システム登録（履歴）画面へ遷移
        Me.Hide()
        HBKB0601_R.ShowDialog()

        '画面表示
        Me.Show()

    End Sub

    ''' <summary>
    ''' [登録]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>入力チェックを行い、入力内容を保持して変更理由登録画面へ遷移する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        dataHBKB0601.PropLblkanryoMsg.Text = ""
        Application.DoEvents()

        '処理モードに応じた入力チェックを行う
        If dataHBKB0601.PropStrProcMode = PROCMODE_EDIT Then            '編集／参照（ロック）モード

            'ロック解除チェック
            If logicHBKB0601.CheckBeUnlockedMain(dataHBKB0601) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0601.PropAryTsxCtlList) = False Then
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
            If dataHBKB0601.PropBlnBeLockedFlg = False Then             '編集モード

                'ロック解除されていない時、入力チェックを行う
                If logicHBKB0601.CheckInputValueMain(dataHBKB0601) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB0601.PropAryTsxCtlList) = False Then
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

            ElseIf dataHBKB0601.PropBlnBeLockedFlg = True Then         '参照（ロック）モード

                'フラグをON
                dataHBKB0601.PropBlnLockCompare = True

                'ロック解除時、ログ出力処理と画面の再描画を行う
                If logicHBKB0601.SetFormRefModeFromEditModeMain(dataHBKB0601) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB0601.PropAryTsxCtlList) = False Then
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
                MsgBox(dataHBKB0601.PropStrBeUnlockedMsg, MsgBoxStyle.Exclamation, TITLE_WARNING)
                Exit Sub

            End If

        End If

        '処理モードに応じた登録処理を行う
        If dataHBKB0601.PropStrProcMode = PROCMODE_EDIT Then            '編集／参照（ロックモード）

            'ロック解除チェック
            If logicHBKB0601.CheckBeUnlockedMain(dataHBKB0601) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0601.PropAryTsxCtlList) = False Then
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
            If dataHBKB0601.PropBlnBeLockedFlg = False Then             '編集モード

                'ロック解除されていない時、更新処理を行う
                If logicHBKB0601.RegistDataOnEditModeMain(dataHBKB0601) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB0601.PropAryTsxCtlList) = False Then
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

                'タブ制御を行わないフラグを立てる
                dataHBKB0601.PropBlnkanryoFlg = True

                '編集モードで画面再描画
                dataHBKB0601.PropStrProcMode = PROCMODE_EDIT
                HBKB0601_Load(Me, New EventArgs)

            ElseIf dataHBKB0601.PropBlnBeLockedFlg = True Then         '参照（ロック）モード

                'フラグをON
                dataHBKB0601.PropBlnLockCompare = True

                'ロック解除時、ログ出力処理と画面の再描画を行う
                If logicHBKB0601.SetFormRefModeFromEditModeMain(dataHBKB0601) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB0601.PropAryTsxCtlList) = False Then
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
                MsgBox(dataHBKB0601.PropStrBeUnlockedMsg, MsgBoxStyle.Exclamation, TITLE_WARNING)
                Exit Sub

            End If

        End If


        'マウスポインタ変更(砂時計→通常)
        Me.Cursor = Windows.Forms.Cursors.Default

        '登録完了メッセージ表示
        'MsgBox(B0601_I001, MsgBoxStyle.Information, TITLE_INFO)
        dataHBKB0601.PropLblkanryoMsg.Text = B0601_I001

        '[add] 2012/09/24 y.ikushima 完了メッセージ表示修正 START
        'タイマーを開始する
        Me.timKanryo.Start()
        '[add] 2012/09/24 y.ikushima 完了メッセージ表示修正 END


    End Sub

    ''' <summary>
    ''' IP割当種類コンボボックスValueMember変更時の処理
    ''' </summary>
    ''' <param name="sender">引数sender</param>
    ''' <param name="e">引数e</param>
    ''' <remarks>IP割当種類コンボボックスリストのサイズを計算し、設定する
    ''' <para>作成情報：
    ''' <p>改定情報：2012/08/14 t.fukuo</p>
    ''' </para></remarks>
    Private Sub cmbIPUse_ValueMemberChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbIPUse.ValueMemberChanged

        'コンボボックスリストサイズ変更
        If logicHBKB0601.ResizeCmbListMain(sender) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0601.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [戻る]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>当画面を閉じて呼び出し元画面へ遷移する
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click

        '当画面を閉じる
        Me.Close()

    End Sub

    ''' <summary>
    ''' フォームクローズ時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>編集モードの場合はロック解除を行う
    ''' <para>作成情報：2012/07/11 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKB0601_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        '編集モードの場合はロック解除を行う
        If dataHBKB0601.PropStrProcMode = PROCMODE_EDIT And _
            dataHBKB0601.PropBlnBeLockedFlg = False Then

            '画面クローズ時ロック解除処理
            If logicHBKB0601.UnlockWhenCloseMain(dataHBKB0601) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0601.PropAryTsxCtlList) = False Then
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

    '[add] 2012/09/24 s.yamaguchi 完了メッセージ表示修正 START
    ''' <summary>
    ''' インタバール経過後の処理の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>当画面を閉じて呼び出し元画面へ遷移する
    ''' <para>作成情報：2012/09/24 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timKanryo.Tick
        '登録完了ラベルを初期化する 
        dataHBKB0601.PropLblkanryoMsg.Text = ""

        'タイマーを停止する
        Me.timKanryo.Stop()

    End Sub
    '[add] 2012/09/24 s.yamaguchi 完了メッセージ表示修正 END

End Class