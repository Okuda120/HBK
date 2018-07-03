Imports Common
Imports CommonHBK
Imports HBKZ
Imports FarPoint.Win.Spread

''' <summary>
''' 部所有機器登録画面Interfaceクラス
''' </summary>
''' <remarks>部所有機器登録画面の設定を行う
''' <para>作成情報：2012/07/11 s.tsuruta
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKB1301

    'インスタンス作成
    Public dataHBKB1301 As New DataHBKB1301
    Private logicHBKB1301 As New LogicHBKB1301
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    '変数宣言
    Private blnDoRollBack As Boolean    'ロールバック実行フラグ

    Public Overloads Function ShowDialog() As Boolean

        'ロールバック実行フラグ初期化
        blnDoRollBack = False

        '当画面をポップアップ表示
        MyBase.ShowDialog()

        'ロールバックフラグを返す
        Return blnDoRollBack

    End Function

    ''' <summary>
    ''' フォームロード時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>モードに応じて画面の初期設定を行う
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKB1301_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'XMLファイルから設定を読み込む
        Settings.LoadFromXmlFile()
        '最大化とか
        Me.WindowState = Settings.Instance.propHBKB1301_WindowState
        'サイズが0だった初期状態とみなし通常の表示を行う
        If Settings.Instance.propHBKB1301_Height <> 0 Then
            Me.Size = New Point(Settings.Instance.propHBKB1301_Width, Settings.Instance.propHBKB1301_Height)
            Me.Location = New Point(Settings.Instance.propHBKB1301_X, Settings.Instance.propHBKB1301_Y)
        End If

        'データクラスの初期設定を行う
        With dataHBKB1301
            .PropGrpLoginUser = Me.grpLoginUser                         'ログイン：ログイン情報グループボックス
            .PropGrpCIKhn = Me.grpCIKhn                                 'ヘッダ：CI基本情報グループボックス
            .PropLblCINmb = Me.lblCINmb                                 'ヘッダ：CI番号ラベル
            .PropLblCIKbnNM = Me.lblCIKbnNM                             'ヘッダ：CI種別名ラベル
            .PropLblTitleRirekiNo = Me.lblTitleRirekiNo                 'ヘッダ：履歴番号タイトルラベル
            .PropLblValueRirekiNo = Me.lblValueRirekiNo                 'ヘッダ：履歴番号値ラベル
            .PropTbInput = Me.tbInput                                   'タブ
            .PropCmbKind = Me.cmbKind                                   '基本情報：種別コンボボックス
            .PropTxtNum = Me.txtNum                                     '基本情報：番号コンボボックス
            .PropTxtClass1 = Me.txtClass1                               '基本情報：分類１テキストボックス
            .PropTxtClass2 = Me.txtClass2                               '基本情報：分類２テキストボックス
            .PropTxtCINM = Me.txtCINM                                   '基本情報：CI種別名称テキストボックス
            .ProptxtKataban = Me.txtKataban                             '基本情報：型番テキストボックス
            .PropCmbCIStatus = Me.cmbCIStatus                           '基本情報：ステータスコンボボックス
            .ProptxtAliau = Me.txtAliau                                 '基本情報：エイリアステキストボックス
            .PropTxtSerial = Me.txtSerial                               '基本情報：製造番号テキストボックス
            .ProptxtNIC1 = Me.txtNIC1                                   '基本情報：NIC1テキストボックス
            .ProptxtMacaddress1 = Me.txtMacAddress1                     '基本情報：MACアドレス1テキストボックス
            .ProptxtNIC2 = Me.txtNIC2                                   '基本情報：NIC2テキストボックス
            .ProptxtMacaddress2 = Me.txtMacAddress2                     '基本情報：MACアドレス2テキストボックス
            .PropcmbOSCD = Me.cmbOSCD                                   '基本情報：OSコンボボックス
            .PropcmbAntiVirusSoftCD = Me.cmbAntiVirusSoftCD             '基本情報：ウィルス対策ソフトコンボボックス
            .PropdtpConnectDT = Me.dtpConnectDT                         '基本情報：接続日
            .PropdtpExpirationDT = Me.dtpExpirationDT                   '基本情報：有効日
            .PropdtpLastInfoDT = Me.dtpLastInfoDT                       '基本情報：最終お知らせ日
            .PropdtpExpirationUPDT = Me.dtpExpirationUPDT               '基本情報：更新日
            .PropdtpInfoDT = Me.dtpInfoDT                               '基本情報：通知日
            .PropdtpDeletDT = Me.dtpDeletDT                             '基本情報：停止日
            .PropcmbDNSRegCD = Me.cmbDNSRegCD                           '基本情報：DNS登録コンボボックス
            .PropcmbZooKbn = Me.cmbZooKbn                               '基本情報：ZOO参加有無コンボボックス
            .PropcmbNumInfoKbn = Me.cmbNumInfoKbn                       '基本情報：番号通知コンボボックス
            .PropcmbSealSendkbn = Me.cmbSealSendkbn                     '基本情報：シール送付コンボボックス
            .PropcmbAntiVirusSofCheckKbn = Me.cmbAntiVirusSofCheckKbn   '基本情報：ウィルス対策ソフト確認コンボボックス
            .PropDtpAntiVirusSofCheckDT = Me.dtpAntiVirusSofCheckDT     '基本情報：ウィルス対策ソフトサーバー確認日
            .ProptxtConectReason = Me.txtConectReason                   '基本情報：接続理由テキストボックス
            .ProptxtBusyoKikiBiko = Me.txtBusyoKikiBiko                 '基本情報：部所有機器備考テキストボックス
            .ProptxtCINaiyo = Me.txtCINaiyo                             '基本情報：説明テキストボックス
            .ProplblCIKind = Me.lblCIKind                               '利用情報：種別ラベル
            .ProplblNum = Me.lblNum                                     '利用情報：番号ラベル
            .ProptxtUsrID = Me.txtUsrID                                 '利用情報：ユーザーIDテキストボックス
            .ProptxtUsrNM = Me.txtUsrNM                                 '利用情報：ユーザー氏名テキストボックス
            .ProptxtUsrMailAdd = Me.txtUsrMailAdd                       '利用情報：ユーザーメールアドレステキストボックス
            .ProptxtUsrTel = Me.txtUsrTel                               '利用情報：ユーザー電話暗号テキストボックス
            .ProptxtUsrKyokuNM = Me.txtUsrKyokuNM                       '利用情報：ユーザー所属局テキストボックス
            .ProptxtUsrBusyoNM = Me.txtUsrBusyoNM                       '利用情報：ユーザー所属部署テキストボックス
            .ProptxtUsrCompany = Me.txtUsrCompany                       '利用情報：ユーザー所属会社アドレステキストボックス
            .ProptxtUsrContact = Me.txtUsrContact                       '利用情報：ユーザー連絡先テキストボックス
            .ProptxtUsrRoom = Me.txtUsrRoom                             '利用情報：ユーザー番組/部屋テキストボックス
            .ProptxtManageKyokuNM = Me.txtManageKyokuNM                 '利用情報：管理局テキストボックス
            .ProptxtManageBusyoNM = Me.txtManageBusyoNM                 '利用情報：管理部署テキストボックス
            .ProptxtWorkFromNmb = Me.txtWorkFromNmb                     '利用情報：作業の元テキストボックス
            .ProptxtFixedIP = Me.txtFixedIP                             '利用情報：固定IPテキストボックス
            .PropcmbIPUseCD = Me.cmbIPUseCD                             '利用情報：IP割当種類コンボボックス
            .ProptxtSetKyokuNM = Me.txtSetKyokuNM                       '利用情報：設置局テキストボックス
            .ProptxtSetBusyoNM = Me.txtSetBusyoNM                       '利用情報：設置部署テキストボックス
            .ProptxtSetRoom = Me.txtSetRoom                             '利用情報：設置番組/部屋テキストボックス
            .ProptxtSetBuil = Me.txtSetBuil                             '利用情報：設置建物テキストボックス
            .ProptxtSetFloor = Me.txtSetFloor                           '利用情報：設置フロアテキストボックス
            .PropbtnUsr = Me.btnUsr                                     '利用情報：ユーザー検索ボタン
            .PropBtnSet = Me.btnSet                                     '利用情報：設置部署検索ボタン
            .PropTxtBIko1 = Me.txtBIko1                                 'フリー入力情報：テキスト１テキストボックス
            .PropTxtBIko2 = Me.txtBIko2                                 'フリー入力情報：テキスト２テキストボックス
            .PropTxtBIko3 = Me.txtBIko3                                 'フリー入力情報：テキスト３テキストボックス
            .PropTxtBIko4 = Me.txtBIko4                                 'フリー入力情報：テキスト４テキストボックス
            .PropTxtBIko5 = Me.txtBIko5                                 'フリー入力情報：テキスト５テキストボックス
            .PropChkFreeFlg1 = Me.chkFreeFlg1                           'フリー入力情報：フリーフラグ１チェックボックス
            .PropChkFreeFlg2 = Me.chkFreeFlg2                           'フリー入力情報：フリーフラグ２チェックボックス
            .PropChkFreeFlg3 = Me.chkFreeFlg3                           'フリー入力情報：フリーフラグ３チェックボックス
            .PropChkFreeFlg4 = Me.chkFreeFlg4                           'フリー入力情報：フリーフラグ４チェックボックス
            .PropChkFreeFlg5 = Me.chkFreeFlg5                           'フリー入力情報：フリーフラグ５チェックボックス
            .PropTxtCIOwnerNM = Me.txtCIOwnerNM                         '関係情報：CIオーナーコードテキストボックス
            .PropBtnSearchGrp = Me.btnOwnerSearch                       '関係情報：検索ボタン
            .PropLblCIOwnerCD = Me.lblCIOwerCD                          '関係情報：CIオーナーコードラベル
            .PropLblRirekiNo = Me.RirekiNo                              'フッタ：履歴番号（更新ID）ラベル
            .PropTxtRegReason = Me.txtRegReason                         'フッタ：理由テキストボックス
            .PropVwMngNmb = Me.vwMngNmb                                 'フッタ：原因リンク管理番号スプレッド
            .PropVwRegReason = Me.vwRegReason                           'フッタ：履歴情報スプレッド
            .PropBtnReg = Me.btnReg                                     'フッタ：登録ボタン
            .PropBtnRollBack = Me.btnRollback                           'フッタ：ロールバックボタン

        End With

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'システムエラー事前対応処理
        If logicHBKB1301.DoProcForErrorMain(dataHBKB1301) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        '処理モードに応じて画面初期表示を行う
        If dataHBKB1301.PropStrProcMode = PROCMODE_NEW Then             '新規モード

            '新規モード画面初期表示メイン処理
            If logicHBKB1301.InitFormNewModeMain(dataHBKB1301) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB1301.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

        ElseIf dataHBKB1301.PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード  

            'ロック設定メイン処理
            If logicHBKB1301.LockMain(dataHBKB1301) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB1301.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            If dataHBKB1301.PropBlnBeLockedFlg = False Then           '編集モード

                '編集モード画面初期表示メイン処理
                If logicHBKB1301.InitFormEditModeMain(dataHBKB1301) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB1301.PropAryTsxCtlList) = False Then
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            Exit Sub
                        End If
                    End If
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If

            ElseIf dataHBKB1301.PropBlnBeLockedFlg = True Then       '参照（ロック）モード

                '参照モード画面初期表示メイン処理
                If logicHBKB1301.InitFormRefModeMain(dataHBKB1301) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB1301.PropAryTsxCtlList) = False Then
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
                MsgBox(dataHBKB1301.PropStrBeLockedMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, TITLE_INFO)

            End If

        ElseIf dataHBKB1301.PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

            '画面名設定
            Me.Text = B1301_NAME_RIREKI

            '履歴モード画面初期表示メイン処理
            If logicHBKB1301.InitFormRirekiModeMain(dataHBKB1301) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB1301.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

        ElseIf dataHBKB1301.PropStrProcMode = PROCMODE_REF Then      '参照モード

            '参照モード画面初期表示メイン処理
            If logicHBKB1301.InitFormRefModeMain(dataHBKB1301) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB1301.PropAryTsxCtlList) = False Then
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
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnUnlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grpLoginUser.btnUnlockClick

        'ロック解除処理を行う
        If logicHBKB1301.UnlockWhenClickBtnUnlockMain(dataHBKB1301) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB1301.PropAryTsxCtlList) = False Then
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
        dataHBKB1301.PropStrProcMode = PROCMODE_EDIT
        HBKB1301_Load(Me, New EventArgs)

    End Sub

    ''' <summary>
    ''' 利用情報：ユーザーID[検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>エンドユーザー検索画面を表示し、選択されたユーザーを当画面にセットする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnUsr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUsr.Click

        Dim HBKZ0201 As New HBKZ0201

        'パラメータセット
        With HBKZ0201.dataHBKZ0201
            .PropMode = SELECT_MODE_SINGLE                  'モード：単一選択
            .PropArgs = dataHBKB1301.ProptxtUsrNM.Text      '検索条件：更新者名
            .PropSplitMode = SPLIT_MODE_AND                 '検索条件区切り：OR
        End With

        'エンドユーザー検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKB1301.PropDtResultSub = HBKZ0201.ShowDialog()

        'ユーザ情報をセット
        If dataHBKB1301.PropDtResultSub IsNot Nothing Then
            dataHBKB1301.ProptxtUsrID.Text = dataHBKB1301.PropDtResultSub.Rows(0).Item("EndUsrID")                'ユーザーID
            dataHBKB1301.ProptxtUsrNM.Text = dataHBKB1301.PropDtResultSub.Rows(0).Item("EndUsrNM")                'ユーザー氏名
            dataHBKB1301.ProptxtUsrMailAdd.Text = dataHBKB1301.PropDtResultSub.Rows(0).Item("EndUsrMailAdd")      'ユーザーメールアドレス
            'dataHBKB1301.ProptxtUsrTel.Text = dataHBKB1301.PropDtResultSub.Rows(0).Item("EndUsrContact")          'ユーザー電話番号
            dataHBKB1301.ProptxtUsrTel.Text = dataHBKB1301.PropDtResultSub.Rows(0).Item("EndUsrTel")          'ユーザー電話番号
            dataHBKB1301.ProptxtUsrKyokuNM.Text = ""                                                  'ユーザー所属局
            dataHBKB1301.ProptxtUsrBusyoNM.Text = dataHBKB1301.PropDtResultSub.Rows(0).Item("EndUsrBusyoNM")      'ユーザー所属部署
            dataHBKB1301.ProptxtUsrCompany.Text = dataHBKB1301.PropDtResultSub.Rows(0).Item("EndUsrCompany")      'ユーザー会社
            dataHBKB1301.ProptxtUsrContact.Text = ""                                                  'ユーザー連絡先
            dataHBKB1301.ProptxtUsrRoom.Text = ""                                                     'ユーザー番組／部屋
        End If

    End Sub

    ''' <summary>
    ''' 利用情報：設置情報[検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>設置情報検索画面を表示し、選択された設置情報を当画面にセットする
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSet.Click

        '設置情報検索画面インスタンス作成
        Dim HBKZ0501 As New HBKZ0501

        'パラメータセット
        With HBKZ0501.dataHBKZ0501
            .PropMode = SELECT_MODE_SINGLE              'モード：単一選択
            .PropArgs = Me.txtSetBusyoNM.Text           '検索条件：設置部署名
            .PropSplitMode = SPLIT_MODE_AND             '検索条件区切り：AND
        End With

        '設置情報検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKB1301.PropDtResultSub = HBKZ0501.ShowDialog()

        '設置情報をセット
        If logicHBKB1301.SetNewSetDataMain(dataHBKB1301) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB1301.PropAryTsxCtlList) = False Then
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
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSearchGrp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOwnerSearch.Click

        'グループ検索画面インスタンス作成
        Dim HBKZ0301 As New HBKZ0301

        'パラメータセット
        With HBKZ0301.dataHBKZ0301
            .PropMode = SELECT_MODE_SINGLE            'モード：単一選択
            .PropArgs = Me.txtCIOwnerNM.Text          '検索条件：CIオーナー名
            .PropSplitMode = SPLIT_MODE_AND           '検索条件区切り：AND
        End With

        'グループ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKB1301.PropDtResultSub = HBKZ0301.ShowDialog()

        'CIオーナー情報をセット
        If logicHBKB1301.SetNewCIOwnerDataMain(dataHBKB1301) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB1301.PropAryTsxCtlList) = False Then
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
    ''' 原因リンク一覧：セルダブルクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択した管理番号の登録画面へ参照モードで遷移する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwMngNmb_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwMngNmb.CellDoubleClick

        '列ヘッダーがクリックされた場合は処理しない
        'また、履歴モード時も処理しない
        If e.ColumnHeader = True Or e.RowHeader = True Or dataHBKB1301.PropStrProcMode = PROCMODE_RIREKI Then
            Exit Sub
        End If

        '変数宣言
        Dim strSelectKbn As String = _
            Me.vwMngNmb.Sheets(0).Cells(e.Row, logicHBKB1301.COL_CAUSELINK_KBN).Value                    '選択行の区分
        Dim strSelectNo As String = _
            Me.vwMngNmb.Sheets(0).Cells(e.Row, logicHBKB1301.COL_CAUSELINK_NO).Value                     '選択行の管理番号

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
                .PropIntPrbNmb = strSelectNo        '問題番号：管理番号
            End With
            '画面遷移
            Me.Hide()
            HBKD0201.ShowDialog()
            Me.Show()

        ElseIf strSelectKbn = PROCESS_TYPE_CHANGE Then

            '*********************************
            '* 区分が変更の場合
            '*********************************

            '変更登録画面インスタンス作成
            Dim HBKE0201 As New HBKE0201

            '変更登録画面データクラスにパラメータをセット
            With HBKE0201.dataHBKE0201
                .PropStrProcMode = PROCMODE_REF     '処理モード：参照
                .PropIntChgNmb = strSelectNo        '変更番号：管理番号
            End With
            '画面遷移
            Me.Hide()
            HBKE0201.ShowDialog()
            Me.Show()



        ElseIf strSelectKbn = PROCESS_TYPE_RELEASE Then

            '*********************************
            '* 区分がリリースの場合
            '*********************************

            'リリース登録画面インスタンス作成
            Dim HBKF0201 As New HBKF0201
            'リリース登録画面データクラスにパラメータをセット
            With HBKF0201.dataHBKF0201
                .PropStrProcMode = PROCMODE_REF     '処理モード：参照
                .PropIntRelNmb = strSelectNo        'リリース番号：管理番号
            End With
            '画面遷移
            Me.Hide()
            HBKF0201.ShowDialog()
            Me.Show()

        End If

    End Sub

    ''' <summary>
    ''' 履歴情報一覧：セルダブルクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択した行の部所有機器履歴画面へ遷移する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwRegReason_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwRegReason.CellDoubleClick

        '列ヘッダーがクリックされた場合は処理しない
        'また、履歴モード時も処理しない
        '１行目が選択されても処理を行わない
        If e.ColumnHeader = True Or e.RowHeader = True Or dataHBKB1301.PropStrProcMode = PROCMODE_RIREKI Or e.Row = 0 Then
            Exit Sub
        End If

        '変数宣言
        Dim HBKB1301_R As HBKB1301 = Nothing                                                                '部所有機器登録（履歴）画面
        Dim intSelectRirekiNo As Integer = _
            Integer.Parse(Me.vwRegReason.Sheets(0).Cells(e.Row, logicHBKB1301.COL_REGREASON_UPID).Value)    '選択行の履歴番号

        '部所有機器登録（履歴）画面のインスタンスを作成
        HBKB1301_R = New HBKB1301

        '部所有機器登録（履歴）画面のデータクラスにパラメータをセット
        With HBKB1301_R.dataHBKB1301
            .PropStrProcMode = PROCMODE_RIREKI                      '処理モード：履歴
            .PropIntCINmb = dataHBKB1301.PropIntCINmb               'CI番号
            .PropIntRirekiNo = intSelectRirekiNo                    '履歴番号
            .PropIntFromRegDocFlg = 1                               '部所有機器登録画面遷移フラグON
            .PropBlnBeLockedFlg = dataHBKB1301.PropBlnBeLockedFlg   'ロックフラグ
            .propStrEdiTime = Me.grpLoginUser.PropLockDate          '編集開始日時
        End With

        '部所有機器登録（履歴）画面へ遷移し、戻り値としてロールバック実行フラグを取得
        Me.Hide()
        Dim blnResult As Boolean = HBKB1301_R.ShowDialog()

        'ロールバック実行フラグがONの場合、編集モードで画面再描画
        If blnResult = True Then
            '編集モードで画面再描画
            dataHBKB1301.PropStrProcMode = PROCMODE_EDIT
            HBKB1301_Load(Me, New EventArgs)
        End If

        '画面表示
        Me.Show()

    End Sub

    ''' <summary>
    ''' [登録]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>入力チェックを行い、登録内容を保持して変更理由登録画面へ遷移する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click

        '変数宣言
        Dim HBKB0301 As HBKB0301 = Nothing  '変更理由登録画面

        '処理モードに応じた入力チェックを行う
        If dataHBKB1301.PropStrProcMode = PROCMODE_NEW Then                 '新規登録モード

            '入力チェック処理
            If logicHBKB1301.CheckInputValueMain(dataHBKB1301) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB1301.PropAryTsxCtlList) = False Then
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

        ElseIf dataHBKB1301.PropStrProcMode = PROCMODE_EDIT Then            '編集／参照（ロック）モード

            'ロック解除チェック
            If logicHBKB1301.CheckBeUnlockedMain(dataHBKB1301) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB1301.PropAryTsxCtlList) = False Then
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
            If dataHBKB1301.PropBlnBeLockedFlg = False Then             '編集モード

                'ロック解除されていない時、入力チェックを行う
                If logicHBKB1301.CheckInputValueMain(dataHBKB1301) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB1301.PropAryTsxCtlList) = False Then
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

            ElseIf dataHBKB1301.PropBlnBeLockedFlg = True Then         '参照（ロック）モード

                'フラグをON
                dataHBKB1301.PropBlnLockCompare = True

                'ロック解除時、ログ出力処理と画面の再描画を行う
                If logicHBKB1301.SetFormRefModeFromEditModeMain(dataHBKB1301) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB1301.PropAryTsxCtlList) = False Then
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
                MsgBox(dataHBKB1301.PropStrBeUnlockedMsg, MsgBoxStyle.Exclamation, TITLE_WARNING)
                Exit Sub

            End If

        End If

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
            dataHBKB1301.PropStrRegReason = .PropStrRegReason   '理由格納用
            dataHBKB1301.PropDtCauseLink = .PropDtCauseLink     '原因リンクデータ格納用
        End With
        Me.Show()

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        '処理モードに応じた登録処理を行う
        If dataHBKB1301.PropStrProcMode = PROCMODE_NEW Then                 '新規登録モード

            '新規登録処理
            If logicHBKB1301.RegistDataOnNewModeMain(dataHBKB1301) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB1301.PropAryTsxCtlList) = False Then
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
            dataHBKB1301.PropStrProcMode = PROCMODE_EDIT
            HBKB1301_Load(Me, New EventArgs)

        ElseIf dataHBKB1301.PropStrProcMode = PROCMODE_EDIT Then            '編集／参照（ロックモード）

            'ロック解除チェック
            If logicHBKB1301.CheckBeUnlockedMain(dataHBKB1301) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB1301.PropAryTsxCtlList) = False Then
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
            If dataHBKB1301.PropBlnBeLockedFlg = False Then             '編集モード

                'ロック解除されていない時、更新処理を行う
                If logicHBKB1301.RegistDataOnEditModeMain(dataHBKB1301) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB1301.PropAryTsxCtlList) = False Then
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
                dataHBKB1301.PropStrProcMode = PROCMODE_EDIT
                HBKB1301_Load(Me, New EventArgs)

            ElseIf dataHBKB1301.PropBlnBeLockedFlg = True Then         '参照（ロック）モード

                'フラグをON
                dataHBKB1301.PropBlnLockCompare = True

                'ロック解除時、ログ出力処理と画面の再描画を行う
                If logicHBKB1301.SetFormRefModeFromEditModeMain(dataHBKB1301) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB1301.PropAryTsxCtlList) = False Then
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
                MsgBox(dataHBKB1301.PropStrBeUnlockedMsg, MsgBoxStyle.Exclamation, TITLE_WARNING)
                Exit Sub

            End If

        End If


        'マウスポインタ変更(砂時計→通常)
        Me.Cursor = Windows.Forms.Cursors.Default

        '登録完了メッセージ表示
        MsgBox(B1301_I001, MsgBoxStyle.Information, TITLE_INFO)

    End Sub

    ''' <summary>
    ''' [ロールバック]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>変更理由登録画面へ遷移する
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRollback_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRollback.Click

        '変数宣言
        Dim HBKB0301 As HBKB0301 = Nothing  '変更理由登録画面

        '参照モード時（ロック解除時）はメッセージ表示して処理を抜ける
        If dataHBKB1301.PropStrProcMode = PROCMODE_REF Then
            MsgBox(dataHBKB1301.PropStrBeUnlockedMsg, MsgBoxStyle.Exclamation, TITLE_WARNING)
            Exit Sub
        End If

        '変更理由登録画面のインスタンス作成
        HBKB0301 = New HBKB0301

        '変更理由登録画面のデータクラスにパラメータをセット
        With HBKB0301.dataHBKB0301
            .PropStrRegMode = REG_MODE_HISTORY    '登録モード：ロールバック
        End With

        '変更理由登録画面へ遷移（確認メッセージなし）し、戻り値として決定フラグを取得
        Me.Hide()
        If HBKB0301.ShowDialog() = DIALOG_RETURN_CANCEL Then
            'キャンセルボタンクリック時は処理終了
            Exit Sub
        End If
        '変更理由登録画面からデータを取得
        With HBKB0301.dataHBKB0301
            dataHBKB1301.PropStrRegReason = .PropStrRegReason   '理由格納用
            dataHBKB1301.PropDtCauseLink = .PropDtCauseLink     '原因リンクデータ格納用
        End With
        Me.Show()

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        'ロールバック処理
        If logicHBKB1301.RollBackDataMain(dataHBKB1301) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB1301.PropAryTsxCtlList) = False Then
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

        'ロールバック完了メッセージ表示
        MsgBox(B1301_I002, MsgBoxStyle.Information, TITLE_INFO)

        'ロールバック実行フラグON
        blnDoRollBack = True

        '当画面を閉じる
        Me.Close()

    End Sub

    ''' <summary>
    ''' [戻る]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>当画面を閉じて呼び出し元画面へ遷移する
    ''' <para>作成情報：2012/07/11 s.tsuruta
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
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKB1301_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        '編集モードの場合はロック解除を行う
        If dataHBKB1301.PropStrProcMode = PROCMODE_EDIT And dataHBKB1301.PropBlnBeLockedFlg = False Then

            '画面クローズ時ロック解除処理
            If logicHBKB1301.UnlockWhenCloseMain(dataHBKB1301) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB1301.PropAryTsxCtlList) = False Then
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
    ''' ユーザーIDテキストボックスEnter時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>ユーザーIDテキストボックスEnter時にエンドユーザマスタを検索して、ユーザー氏名テキストボックスに氏名を入れる
    ''' <para>作成情報：2012/07/11 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub txtUsrID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUsrID.KeyPress

        '新規登録か編集モードでENTERキー押下時のみ処理を行う
        If ((dataHBKB1301.PropStrProcMode = PROCMODE_EDIT And dataHBKB1301.PropBlnBeLockedFlg = False) Or dataHBKB1301.PropStrProcMode = PROCMODE_NEW) AndAlso _
            e.KeyChar = ChrW(Keys.Enter) Then

            'マウスポインタ変更(通常→砂時計)
            Me.Cursor = Windows.Forms.Cursors.WaitCursor

            'エンドユーザーマスタを検索し、取得したユーザー情報を当画面にセットする
            If logicHBKB1301.EnterClickOnUsrIDMain(dataHBKB1301) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB1301.PropAryTsxCtlList) = False Then
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
    ''' フォーム終了時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>画面情報の保存を行う
    ''' <para>作成情報：2012/10/31 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKB1301_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

        '設定を変更する
        With Settings.Instance
            'ウィンドウが最大化、最小化以外は保存
            If Me.WindowState = FormWindowState.Normal Then
                .propHBKB1301_Height = Me.Size.Height
                .propHBKB1301_Width = Me.Size.Width
                .propHBKB1301_Y = Me.Location.Y
                .propHBKB1301_X = Me.Location.X
                .propHBKB1301_WindowState = Me.WindowState
            ElseIf Me.WindowState = FormWindowState.Maximized Then
                '最大化は状態を保存
                .propHBKB1301_WindowState = Me.WindowState
            End If
        End With
        '現在の設定をXMLファイルに保存する
        Settings.SaveToXmlFile()

    End Sub
End Class