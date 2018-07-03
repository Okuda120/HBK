Imports Common
Imports CommonHBK
Imports HBKZ
Imports FarPoint.Win.Spread

''' <summary>
''' 文書登録画面Interfaceクラス
''' </summary>
''' <remarks>文書登録画面の設定を行う
''' <para>作成情報：2012/06/20 s.tsuruta
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKB0501

    'インスタンス作成
    Public dataHBKB0501 As New DataHBKB0501
    Private logicHBKB0501 As New LogicHBKB0501
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
    ''' <para>作成情報：2012/06/22 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKB0501_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'データクラスの初期設定を行う
        With dataHBKB0501
            .PropGrpLoginUser = Me.grpLoginUser                     'ログイン：ログイン情報グループボックス
            .PropGrpCIKhn = Me.grpCIKhn                             'ヘッダ：CI基本情報グループボックス
            .PropLblCINmb = Me.lblCINmb                             'ヘッダ：CI番号ラベル
            .PropLblCIKbnNM = Me.lblCIKbnNM                         'ヘッダ：CI種別名ラベル
            .PropLblTitleRirekiNo = Me.lblTitleRirekiNo             'ヘッダ：履歴番号タイトルラベル
            .PropLblValueRirekiNo = Me.lblValueRirekiNo             'ヘッダ：履歴番号値ラベル
            .PropTbInput = Me.tbInput                               'タブ
            .PropCmbKind = Me.cmbKind                               '基本情報：種別コンボボックス
            .PropTxtNum = Me.txtNum                                 '基本情報：番号(手動)コンボボックス
            .PropTxtVersion = Me.txtVersion                         '基本情報：版(手動)コンボボックス
            .PropTxtClass1 = Me.txtClass1                           '基本情報：分類１テキストボックス
            .PropTxtClass2 = Me.txtClass2                           '基本情報：分類２テキストボックス
            .PropTxtCINM = Me.txtCINM                               '基本情報：CI種別名称テキストボックス
            .PropBtnFilePathOpen = Me.btnFilePathOpen               '基本情報：開くボタン
            .PropBtnFilePathDownload = Me.btnFilePathDownload       '基本情報：ダウンロードボタン
            .PropCmbCIStatus = Me.cmbCIStatus                       '基本情報：ステータスコンボボックス
            .PropTxtCINaiyo = Me.txtCINaiyo                         '基本情報：説明テキストボックス
            .ProptxtCrateID = Me.txtCrateID                         '基本情報：作成者IDテキストボックス
            .ProptxtCrateNM = Me.txtCrateNM                         '基本情報：作成者氏名テキストボックス
            .PropbtnCrateSearch = Me.btnCrateSearch                 '基本情報：作成者検索ボタン
            .PropDtpCreateDT = Me.dtpCreateDT                       '基本情報：作成年月日
            .ProptxtLastUpID = Me.txtLastUpID                       '基本情報：最終更新者IDテキストボックス
            .ProptxtLastUpNM = Me.txtLastUpNM                       '基本情報：最終更新者氏名テキストボックス
            .PropbtnLastUpSearch = Me.btnLastUpSearch               '基本情報：更新者検索ボタン
            .PropDtpLastUpDT = Me.dtpLastUpDT                       '基本情報：最終更新日時年月日
            .PropTxtDateTime = Me.txtDatetime                       '基本情報：現在時刻テキストボックス
            .PropBtnDateTime = Me.btnDatetime                       '基本情報：現在時刻ボタン
            .ProptxtChargeID = Me.txtChargeID                       '基本情報：文書責任者IDテキストボックス
            .ProptxtChargeNM = Me.txtChargeNM                       '基本情報：文書責任者氏名テキストボックス
            .PropbtnChargeSearch = Me.btnChargeSearch               '基本情報：文書責任者検索ボタン
            .ProptxtOfferNM = Me.txtOfferNM                         '基本情報：文書提供者テキストボックス
            .ProptxtShareteamNM = Me.txtShareteamNM                 '基本情報：文書配布先テキストボックス
            .ProptxtFilePath = Me.txtFilePath                       '基本情報：文書格納パステキストボックス
            .PropbtnSansyou = Me.btnSansyou                         '基本情報：参照ボタン
            .PropbtnClear = Me.btnClear                             '基本情報：クリアボタン
            .PropDtpDelDT = Me.dtpDelDT                             '基本情報：文書廃棄年月日
            .ProptxtDelReason = Me.txtDelReason                     '基本情報：文書廃棄理由テキストボックス
            .PropTxtBIko1 = Me.txtBIko1                             'フリー入力情報：テキスト１テキストボックス
            .PropTxtBIko2 = Me.txtBIko2                             'フリー入力情報：テキスト２テキストボックス
            .PropTxtBIko3 = Me.txtBIko3                             'フリー入力情報：テキスト３テキストボックス
            .PropTxtBIko4 = Me.txtBIko4                             'フリー入力情報：テキスト４テキストボックス
            .PropTxtBIko5 = Me.txtBIko5                             'フリー入力情報：テキスト５テキストボックス
            .PropChkFreeFlg1 = Me.chkFreeFlg1                       'フリー入力情報：フリーフラグ１チェックボックス
            .PropChkFreeFlg2 = Me.chkFreeFlg2                       'フリー入力情報：フリーフラグ２チェックボックス
            .PropChkFreeFlg3 = Me.chkFreeFlg3                       'フリー入力情報：フリーフラグ３チェックボックス
            .PropChkFreeFlg4 = Me.chkFreeFlg4                       'フリー入力情報：フリーフラグ４チェックボックス
            .PropChkFreeFlg5 = Me.chkFreeFlg5                       'フリー入力情報：フリーフラグ５チェックボックス
            .PropTxtCIOwnerNM = Me.txtCIOwnerNM                     '関係情報：CIオーナーコードテキストボックス
            .PropBtnSearchGrp = Me.btnOwnerSearch                   '関係情報：検索ボタン
            .PropLblCIOwnerCD = Me.lblCIOwerCD                      '関係情報：CIオーナーコードラベル
            .PropLblRirekiNo = Me.RirekiNo                          'フッタ：履歴番号（更新ID）ラベル
            .PropTxtRegReason = Me.txtRegReason                     'フッタ：理由テキストボックス
            .PropVwMngNmb = Me.vwMngNmb                             'フッタ：原因リンク管理番号スプレッド
            .PropVwRegReason = Me.vwRegReason                       'フッタ：履歴情報スプレッド
            .PropBtnReg = Me.btnReg                                 'フッタ：登録ボタン
            .PropBtnRollBack = Me.btnRollback                       'フッタ：ロールバックボタン

        End With

        'フォーム背景色設定
        Me.BackColor = CommonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'システムエラー事前対応処理
        If logicHBKB0501.DoProcForErrorMain(dataHBKB0501) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        '処理モードに応じて画面初期表示を行う
        If dataHBKB0501.PropStrProcMode = PROCMODE_NEW Then             '新規モード

            '新規モード画面初期表示メイン処理
            If logicHBKB0501.InitFormNewModeMain(dataHBKB0501) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0501.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

        ElseIf dataHBKB0501.PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード  

            'ロック設定メイン処理
            If logicHBKB0501.LockMain(dataHBKB0501) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0501.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            If dataHBKB0501.PropBlnBeLockedFlg = False Then           '編集モード

                '編集モード画面初期表示メイン処理
                If logicHBKB0501.InitFormEditModeMain(dataHBKB0501) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB0501.PropAryTsxCtlList) = False Then
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            Exit Sub
                        End If
                    End If
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If

            ElseIf dataHBKB0501.PropBlnBeLockedFlg = True Then       '参照（ロック）モード

                '参照モード画面初期表示メイン処理
                If logicHBKB0501.InitFormRefModeMain(dataHBKB0501) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB0501.PropAryTsxCtlList) = False Then
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
                MsgBox(dataHBKB0501.PropStrBeLockedMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, TITLE_INFO)

            End If

        ElseIf dataHBKB0501.PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

            '画面名設定
            Me.Text = B0501_NAME_RIREKI

            '履歴モード画面初期表示メイン処理
            If logicHBKB0501.InitFormRirekiModeMain(dataHBKB0501) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0501.PropAryTsxCtlList) = False Then
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
    ''' <para>作成情報：2012/07/09 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnUnlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grpLoginUser.btnUnlockClick

        'ロック解除処理を行う
        If logicHBKB0501.UnlockWhenClickBtnUnlockMain(dataHBKB0501) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0501.PropAryTsxCtlList) = False Then
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
    ''' 原因リンク一覧：セルダブルクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択した管理番号の登録画面へ参照モードで遷移する
    ''' <para>作成情報：2012/06/21 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwMngNmb_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwMngNmb.CellDoubleClick

        '列ヘッダーがクリックされた場合は処理しない
        'また、履歴モード時も処理しない
        If e.ColumnHeader = True Or e.RowHeader = True Or dataHBKB0501.PropStrProcMode = PROCMODE_RIREKI Then
            Exit Sub
        End If

        '変数宣言
        Dim strSelectKbn As String = _
            Me.vwMngNmb.Sheets(0).Cells(e.Row, logicHBKB0501.COL_CAUSELINK_KBN).Value                    '選択行の区分
        Dim strSelectNo As String = _
            Me.vwMngNmb.Sheets(0).Cells(e.Row, logicHBKB0501.COL_CAUSELINK_NO).Value                     '選択行の管理番号

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
    ''' <remarks>選択した行の文書履歴画面へ遷移する
    ''' <para>作成情報：2012/07/09 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwRegReason_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwRegReason.CellDoubleClick

        '列ヘッダーがクリックされた場合は処理しない
        'また、履歴モード時も処理しない
        '１行目が選択されても処理を行わない
        If e.ColumnHeader = True Or e.RowHeader = True Or dataHBKB0501.PropStrProcMode = PROCMODE_RIREKI Or e.Row = 0 Then
            Exit Sub
        End If

        '変数宣言
        Dim HBKB0501_R As HBKB0501 = Nothing                                                                '文書登録（履歴）画面
        Dim intSelectRirekiNo As Integer = _
            Integer.Parse(Me.vwRegReason.Sheets(0).Cells(e.Row, logicHBKB0501.COL_REGREASON_UPID).Value)    '選択行の履歴番号

        '文書登録（履歴）画面のインスタンスを作成
        HBKB0501_R = New HBKB0501

        '文書登録（履歴）画面のデータクラスにパラメータをセット
        With HBKB0501_R.dataHBKB0501
            .PropStrProcMode = PROCMODE_RIREKI                      '処理モード：履歴
            .PropIntCINmb = dataHBKB0501.PropIntCINmb               'CI番号
            .PropIntRirekiNo = intSelectRirekiNo                    '履歴番号
            .PropIntFromRegDocFlg = 1                               '文書登録画面遷移フラグON
            .PropBlnBeLockedFlg = dataHBKB0501.PropBlnBeLockedFlg   'ロックフラグ
            .propStrEdiTime = Me.grpLoginUser.PropLockDate          '編集開始日時
        End With

        '文書登録（履歴）画面へ遷移し、戻り値としてロールバック実行フラグを取得
        Me.Hide()
        Dim blnResult As Boolean = HBKB0501_R.ShowDialog()

        'ロールバック実行フラグがONの場合、編集モードで画面再描画
        If blnResult = True Then
            '編集モードで画面再描画
            dataHBKB0501.PropStrProcMode = PROCMODE_EDIT
            HBKB0501_Load(Me, New EventArgs)
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
    ''' <para>作成情報：2012/06/21 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click

        '変数宣言
        Dim HBKB0301 As HBKB0301 = Nothing  '変更理由登録画面

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        '処理モードに応じた入力チェックを行う
        If dataHBKB0501.PropStrProcMode = PROCMODE_NEW Then                 '新規登録モード

            '入力チェック処理
            If logicHBKB0501.CheckInputValueMain(dataHBKB0501) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0501.PropAryTsxCtlList) = False Then
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

        ElseIf dataHBKB0501.PropStrProcMode = PROCMODE_EDIT Then            '編集／参照（ロック）モード

            'ロック解除チェック
            If logicHBKB0501.CheckBeUnlockedMain(dataHBKB0501) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0501.PropAryTsxCtlList) = False Then
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
            If dataHBKB0501.PropBlnBeLockedFlg = False Then             '編集モード

                'ロック解除されていない時、入力チェックを行う
                If logicHBKB0501.CheckInputValueMain(dataHBKB0501) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB0501.PropAryTsxCtlList) = False Then
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

            ElseIf dataHBKB0501.PropBlnBeLockedFlg = True Then         '参照（ロック）モード

                'フラグをON
                dataHBKB0501.PropBlnLockCompare = True

                'ロック解除時、ログ出力処理と画面の再描画を行う
                If logicHBKB0501.SetFormRefModeFromEditModeMain(dataHBKB0501) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB0501.PropAryTsxCtlList) = False Then
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
                MsgBox(dataHBKB0501.PropStrBeUnlockedMsg, MsgBoxStyle.Exclamation, TITLE_WARNING)
                Exit Sub

            End If

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
            dataHBKB0501.PropStrRegReason = .PropStrRegReason   '理由格納用
            dataHBKB0501.PropDtCauseLink = .PropDtCauseLink     '原因リンクデータ格納用
        End With
        Me.Show()

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor


        '処理モードに応じた登録処理を行う
        If dataHBKB0501.PropStrProcMode = PROCMODE_NEW Then                 '新規登録モード

            '新規登録処理
            If logicHBKB0501.RegistDataOnNewModeMain(dataHBKB0501) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0501.PropAryTsxCtlList) = False Then
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
            dataHBKB0501.PropStrProcMode = PROCMODE_EDIT
            HBKB0501_Load(Me, New EventArgs)

        ElseIf dataHBKB0501.PropStrProcMode = PROCMODE_EDIT Then            '編集／参照（ロックモード）

            'ロック解除チェック
            If logicHBKB0501.CheckBeUnlockedMain(dataHBKB0501) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0501.PropAryTsxCtlList) = False Then
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
            If dataHBKB0501.PropBlnBeLockedFlg = False Then             '編集モード

                'ロック解除されていない時、更新処理を行う
                If logicHBKB0501.RegistDataOnEditModeMain(dataHBKB0501) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB0501.PropAryTsxCtlList) = False Then
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
                dataHBKB0501.PropStrProcMode = PROCMODE_EDIT
                HBKB0501_Load(Me, New EventArgs)

            ElseIf dataHBKB0501.PropBlnBeLockedFlg = True Then         '参照（ロック）モード

                'ロック解除時、ログ出力処理と画面の再描画を行う
                If logicHBKB0501.SetFormRefModeFromEditModeMain(dataHBKB0501) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB0501.PropAryTsxCtlList) = False Then
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
                MsgBox(dataHBKB0501.PropStrBeUnlockedMsg, MsgBoxStyle.Exclamation, TITLE_WARNING)
                Exit Sub

            End If

        End If


        'マウスポインタ変更(砂時計→通常)
        Me.Cursor = Windows.Forms.Cursors.Default

        '登録完了メッセージ表示
        MsgBox(B0501_I001, MsgBoxStyle.Information, TITLE_INFO)

    End Sub

    ''' <summary>
    ''' [ロールバック]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>変更理由登録画面へ遷移する
    ''' <para>作成情報：2012/06/27 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRollback_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRollback.Click

        '変数宣言
        Dim HBKB0301 As HBKB0301 = Nothing  '変更理由登録画面

        '参照モード時（ロック解除時）はメッセージ表示して処理を抜ける
        If dataHBKB0501.PropStrProcMode = PROCMODE_REF Then
            MsgBox(dataHBKB0501.PropStrBeUnlockedMsg, MsgBoxStyle.Exclamation, TITLE_WARNING)
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
            dataHBKB0501.PropStrRegReason = .PropStrRegReason   '理由格納用
            dataHBKB0501.PropDtCauseLink = .PropDtCauseLink     '原因リンクデータ格納用
        End With
        Me.Show()

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        'ロールバック処理
        If logicHBKB0501.RollBackDataMain(dataHBKB0501) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0501.PropAryTsxCtlList) = False Then
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
        MsgBox(B0501_I002, MsgBoxStyle.Information, TITLE_INFO)

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
    ''' <para>作成情報：2012/06/21 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click

        '当画面を閉じる
        Me.Close()

    End Sub

    ''' <summary>
    ''' [開く]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>
    ''' <para>作成情報：2012/06/21 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnCINmb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilePathOpen.Click

        '処理モードに応じて処理を行う
        If dataHBKB0501.PropStrProcMode = PROCMODE_NEW Then             '新規モード

            '何もしない

        ElseIf dataHBKB0501.PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード  

            If dataHBKB0501.PropBlnBeLockedFlg = False Then           '編集モード

                '編集モード画面処理
                If logicHBKB0501.FileOpenMain(dataHBKB0501) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB0501.PropAryTsxCtlList) = False Then
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            Exit Sub
                        End If
                    End If
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If

            ElseIf dataHBKB0501.PropBlnBeLockedFlg = True Then       '参照（ロック）モード

                '参照モード画面処理
                If logicHBKB0501.FileOpenMain(dataHBKB0501) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB0501.PropAryTsxCtlList) = False Then
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
                'MsgBox(dataHBKB0501.PropStrBeLockedMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, TITLE_INFO)

            End If

        ElseIf dataHBKB0501.PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

            '履歴モード画面処理
            If logicHBKB0501.FileOpenMain(dataHBKB0501) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0501.PropAryTsxCtlList) = False Then
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
    ''' [ダウンロード]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>
    ''' <para>作成情報：2012/06/21 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilePathDownload.Click

        '処理モードに応じて処理を行う
        If dataHBKB0501.PropStrProcMode = PROCMODE_NEW Then             '新規モード

            '何もしない

        ElseIf dataHBKB0501.PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード  

            If dataHBKB0501.PropBlnBeLockedFlg = False Then           '編集モード

                '編集モード画面処理
                If logicHBKB0501.FileDownLoadMain(dataHBKB0501) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB0501.PropAryTsxCtlList) = False Then
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            Exit Sub
                        End If
                    End If
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If

            ElseIf dataHBKB0501.PropBlnBeLockedFlg = True Then       '参照（ロック）モード

                '参照モード画面処理
                If logicHBKB0501.FileDownLoadMain(dataHBKB0501) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB0501.PropAryTsxCtlList) = False Then
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
                'MsgBox(dataHBKB0501.PropStrBeLockedMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, TITLE_INFO)

            End If

        ElseIf dataHBKB0501.PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

            '履歴モード画面処理
            If logicHBKB0501.FileDownLoadMain(dataHBKB0501) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0501.PropAryTsxCtlList) = False Then
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
    ''' 作成者[検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>エンドユーザーマスタ検索画面を表示し、選択されたID、氏名を当画面にセットする
    ''' <para>作成情報：2012/06/21 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnCrateSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCrateSearch.Click

        Dim HBKZ0201 As New HBKZ0201

        'パラメータセット
        With HBKZ0201.dataHBKZ0201
            .PropMode = SELECT_MODE_SINGLE                  'モード：単一選択
            .PropArgs = dataHBKB0501.ProptxtCrateNM.Text    '検索条件：作成者名
            .PropSplitMode = SPLIT_MODE_AND                 '検索条件区切り：AND
        End With

        'グループ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKB0501.PropDtResultSub = HBKZ0201.ShowDialog()

        'ユーザ情報をセット
        If dataHBKB0501.PropDtResultSub IsNot Nothing Then
            dataHBKB0501.ProptxtCrateID.Text = dataHBKB0501.PropDtResultSub.Rows(0).Item("EndUsrID")                'ユーザーID
            dataHBKB0501.ProptxtCrateNM.Text = dataHBKB0501.PropDtResultSub.Rows(0).Item("EndUsrNM")                'ユーザー氏名
        End If

    End Sub

    ''' <summary>
    ''' 最終更新者[検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>エンドユーザーマスタ検索画面を表示し、選択されたID、氏名を当画面にセットする
    ''' <para>作成情報：2012/06/21 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnLastUpSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLastUpSearch.Click
        Dim HBKZ0201 As New HBKZ0201

        'パラメータセット
        With HBKZ0201.dataHBKZ0201
            .PropMode = SELECT_MODE_SINGLE                  'モード：単一選択
            .PropArgs = dataHBKB0501.ProptxtLastUpNM.Text   '検索条件：最終更新者名
            .PropSplitMode = SPLIT_MODE_AND                 '検索条件区切り：AND
        End With

        'グループ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKB0501.PropDtResultSub = HBKZ0201.ShowDialog()

        'ユーザ情報をセット
        If dataHBKB0501.PropDtResultSub IsNot Nothing Then
            dataHBKB0501.ProptxtLastUpID.Text = dataHBKB0501.PropDtResultSub.Rows(0).Item("EndUsrID")                'ユーザーID
            dataHBKB0501.ProptxtLastUpNM.Text = dataHBKB0501.PropDtResultSub.Rows(0).Item("EndUsrNM")                'ユーザー氏名
        End If

    End Sub

    ''' <summary>
    ''' [現在時刻]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>
    ''' <para>作成情報：2012/06/21 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnDatetime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDatetime.Click

        txtDatetime.PropTxtTime.Text = System.DateTime.Now.ToString("HH:mm")

    End Sub

    ''' <summary>
    ''' 責任者[検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>エンドユーザーマスタ検索画面を表示し、選択されたID、氏名を当画面にセットする
    ''' <para>作成情報：2012/06/21 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnChargeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChargeSearch.Click
        Dim HBKZ0201 As New HBKZ0201

        'パラメータセット
        With HBKZ0201.dataHBKZ0201
            .PropMode = SELECT_MODE_SINGLE                      'モード：単一選択
            .PropArgs = dataHBKB0501.ProptxtChargeNM.Text       '検索条件：文書責任者氏名
            .PropSplitMode = SPLIT_MODE_AND                     '検索条件区切り：AND
        End With

        'グループ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKB0501.PropDtResultSub = HBKZ0201.ShowDialog()

        'ユーザ情報をセット
        If dataHBKB0501.PropDtResultSub IsNot Nothing Then
            dataHBKB0501.ProptxtChargeID.Text = dataHBKB0501.PropDtResultSub.Rows(0).Item("EndUsrID")                'ユーザーID
            dataHBKB0501.ProptxtChargeNM.Text = dataHBKB0501.PropDtResultSub.Rows(0).Item("EndUsrNM")                'ユーザー氏名
        End If

    End Sub

    ''' <summary>
    ''' [参照]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>文書格納パスにファイルダイアログで選択したファイルのパスを入力する
    ''' <para>作成情報：2012/06/21 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSansyou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSansyou.Click

        'ファイルのダイアログを表示する
        Dim OpenFileDialog As New OpenFileDialog()

        'デフォルトで表示されるフォルダを指定
        OpenFileDialog.InitialDirectory = ""

        'ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする(ここの記述は後で調べておく)
        OpenFileDialog.RestoreDirectory = True

        If OpenFileDialog.ShowDialog() = DialogResult.OK Then
            '選択したファイルパスをセットする
            txtFilePath.Text = OpenFileDialog.FileName.ToString
        End If

    End Sub

    ''' <summary>
    ''' [クリア]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>文書格納パスの文字列を削除する。
    ''' <para>作成情報：2012/06/21 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'クリアボタン押下時参照設定の値を削除する
        txtFilePath.Text = ""
    End Sub

    ''' <summary>
    ''' 関係情報タブ：[検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>グループ検索画面を表示し、選択されたグループを当画面にセットする
    ''' <para>作成情報：2012/07/09 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnOwnerSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOwnerSearch.Click

        'グループ検索画面インスタンス作成
        Dim HBKZ0301 As New HBKZ0301

        'パラメータセット
        With HBKZ0301.dataHBKZ0301
            .PropMode = SELECT_MODE_SINGLE            'モード：単一選択
            .PropArgs = Me.txtCIOwnerNM.Text          '検索条件：CIオーナー名
            .PropSplitMode = SPLIT_MODE_AND           '検索条件区切り：AND
        End With

        'グループ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKB0501.PropDtResultSub = HBKZ0301.ShowDialog()

        'CIオーナー情報をセット
        If logicHBKB0501.SetNewCIOwnerDataMain(dataHBKB0501) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0501.PropAryTsxCtlList) = False Then
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
    ''' 作成者IDテキストボックスEnter時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>作成者IDテキストボックスEnter時にエンドユーザマスタを検索して、作成者氏名テキストボックスに氏名を入れる
    ''' <para>作成情報：2012/06/26 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub txtCrateID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCrateID.KeyPress


        '新規登録か編集モードでENTERキー押下時のみ処理を行う
        If ((dataHBKB0501.PropStrProcMode = PROCMODE_EDIT And dataHBKB0501.PropBlnBeLockedFlg = False) Or dataHBKB0501.PropStrProcMode = PROCMODE_NEW) AndAlso _
            e.KeyChar = ChrW(Keys.Enter) Then

            'マウスポインタ変更(通常→砂時計)
            Me.Cursor = Windows.Forms.Cursors.WaitCursor

            dataHBKB0501.PropStrID = dataHBKB0501.ProptxtCrateID.Text

            'エンドユーザーマスタを検索し、取得したユーザー情報を当画面にセットする
            If logicHBKB0501.CreateIDEnterMain(dataHBKB0501) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0501.PropAryTsxCtlList) = False Then
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
    ''' 最終更新者IDテキストボックスEnter時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>最終更新者IDテキストボックスEnter時にエンドユーザマスタを検索して、最終更新者氏名テキストボックスに氏名を入れる
    ''' <para>作成情報：2012/06/26 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub txtLastUpID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLastUpID.KeyPress

        '新規登録か編集モードでENTERキー押下時のみ処理を行う
        If ((dataHBKB0501.PropStrProcMode = PROCMODE_EDIT And dataHBKB0501.PropBlnBeLockedFlg = False) Or dataHBKB0501.PropStrProcMode = PROCMODE_NEW) AndAlso _
            e.KeyChar = ChrW(Keys.Enter) Then

            'マウスポインタ変更(通常→砂時計)
            Me.Cursor = Windows.Forms.Cursors.WaitCursor

            dataHBKB0501.PropStrID = dataHBKB0501.ProptxtLastUpID.Text

            'エンドユーザーマスタを検索し、取得したユーザー情報を当画面にセットする
            If logicHBKB0501.LastUpIDEnterMain(dataHBKB0501) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0501.PropAryTsxCtlList) = False Then
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
    ''' 文書責任者IDテキストボックスEnter時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>文書責任者IDテキストボックスEnter時にエンドユーザマスタを検索して、文書責任者氏名テキストボックスに氏名を入れる
    ''' <para>作成情報：2012/06/26 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub txtChargeID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtChargeID.KeyPress

        '新規登録か編集モードでENTERキー押下時のみ処理を行う
        If ((dataHBKB0501.PropStrProcMode = PROCMODE_EDIT And dataHBKB0501.PropBlnBeLockedFlg = False) Or dataHBKB0501.PropStrProcMode = PROCMODE_NEW) AndAlso _
            e.KeyChar = ChrW(Keys.Enter) Then

            '検索するIDをデータクラスに代入する
            dataHBKB0501.PropStrID = dataHBKB0501.ProptxtChargeID.Text

            'マウスポインタ変更(通常→砂時計)
            Me.Cursor = Windows.Forms.Cursors.WaitCursor

            'エンドユーザーマスタを検索し、取得したユーザー情報を当画面にセットする
            If logicHBKB0501.ChargeIDEnterMain(dataHBKB0501) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0501.PropAryTsxCtlList) = False Then
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
    ''' フォームクローズ時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>編集モードの場合はロック解除を行う
    ''' <para>作成情報：2012/07/09 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKB0501_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        '編集モードの場合はロック解除を行う
        If dataHBKB0501.PropStrProcMode = PROCMODE_EDIT And dataHBKB0501.PropBlnBeLockedFlg = False Then

            '画面クローズ時ロック解除処理
            If logicHBKB0501.UnlockWhenCloseMain(dataHBKB0501) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0501.PropAryTsxCtlList) = False Then
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


    Private Sub Label86_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label86.Click

    End Sub
End Class