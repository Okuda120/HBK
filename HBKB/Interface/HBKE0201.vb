Imports Common
Imports CommonHBK
Imports HBKB
Imports HBKZ
Imports FarPoint.Win.Spread

''' <summary>
''' 変更登録画面Interfaceクラス
''' </summary>
''' <remarks>変更登録画面の設定を行う
''' <para>作成情報：2012/08/13 fukuo
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKE0201

    'インスタンス作成
    Public dataHBKE0201 As New DataHBKE0201
    Private logicHBKE0201 As New LogicHBKE0201
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    Dim AryNotfrmCtlList As New ArrayList                                           '非活性対象ボタンリスト


    ''' <summary>
    ''' フォーム終了時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>画面情報の保存を行う
    ''' <para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKE0201_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        '設定を変更する
        With Settings.Instance
            'ウィンドウが最大化、最小化以外は保存
            If Me.WindowState = FormWindowState.Normal Then
                .propHBKE0201_Height = Me.Size.Height
                .propHBKE0201_Width = Me.Size.Width
                .propHBKE0201_Y = Me.Location.Y
                .propHBKE0201_X = Me.Location.X
                .propHBKE0201_WindowState = Me.WindowState
            ElseIf Me.WindowState = FormWindowState.Maximized Then
                '最大化は状態を保存
                .propHBKE0201_WindowState = Me.WindowState
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
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : 2017/08/17 e.okuda</p>
    ''' </para></remarks>
    Private Sub HBKE0201_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '変更画面を表示するにあたって以下の設定を呼び元側で行うこと。
        'dataHBKE0201.PropIntOwner              呼び元画面(1:変更検索一覧,2:問題登録の変更登録ボタン,0:それ以外)
        'dataHBKE0201.PropStrProcMode           表示モード
        'ボタンコントロール非活性対象リスト作成
        AryNotfrmCtlList.Clear()
        AryNotfrmCtlList.Add(btnOpenFile.Name)
        AryNotfrmCtlList.Add(btnSaveFile.Name)


        'XMLファイルから設定を読み込む
        Settings.LoadFromXmlFile()
        '最大化とか
        Me.WindowState = Settings.Instance.propHBKE0201_WindowState
        'サイズが0だったら初期状態とみなし通常の表示をする。
        If Settings.Instance.propHBKE0201_Height <> 0 Then
            'Me.StartPosition = FormStartPosition.Manual
            'サイズ変更
            Me.Size = New Point(Settings.Instance.propHBKE0201_Width, Settings.Instance.propHBKE0201_Height)
            Me.Location = New Point(Settings.Instance.propHBKE0201_X, Settings.Instance.propHBKE0201_Y)
        End If

        'データクラスの初期設定を行う
        With dataHBKE0201

            .PropLblkanryoMsg = Me.LblkanryoMsg                     '完了メッセージ
            .PropGrpLoginUser = Me.grpLoginUser                     'ログイン：ログイン情報グループボックス
            .PropGrpCD = Me.grpIncCD                                'ヘッダー：グループ
            .PropTxtNmb = Me.txtIncCD                               'ヘッダー：ユーザー
            .PropLblRegInfo = Me.lblRegInfo                         'ヘッダー：登録情報
            .PropLblUpdateInfo = Me.lblUpdateInfo                   'ヘッダー：最終更新情報
            .PropLblRegInfo_out = Me.lblRegInfo_out                 'ヘッダー：登録情報_出力
            .PropLblUpdateInfo_out = Me.lblUpdateInfo_out           'ヘッダー：最終更新情報_出力

            .PropTbInput = Me.tbInput                               'タブ

            .PropCmbprocessStateCD = Me.cmbProcessStateCD           '基本情報：ステータス
            .PropDtpKaisiDT = Me.dtpKaisiDT                         '基本情報：開始日カレンダー
            .PropTxtKaisiDT_HM = Me.txtKaisiDT_HM                   '基本情報：開始日時
            .PropBtnKaisiDT_HM = Me.btnKaisi_HM                     '基本情報：開始日時ボタン
            .PropDtpKanryoDT = Me.dtpKanryoDT                       '基本情報：完了日
            .PropTxtKanryoDT_HM = Me.txtKanryoDT_HM                 '基本情報：完了日時
            .PropBtnKanryoDT_HM = Me.btnKanryo_HM                   '基本情報：完了日時ボタン

            .PropCmbSystemNmb = Me.cmbSystemNmb                     '基本情報：対象システム
            .PropCmbTantoGrpCD = Me.cmbTantoGrpCD                   '基本情報：グループ
            .PropTxtTantoID = Me.txtTantoID                         '基本情報：担当ID
            .PropTxtTantoNM = Me.txtTantoNM                         '基本情報：担当氏名
            .PropBtnTantoMY = Me.btnMytantoID                       '基本情報：担当私ボタン
            .PropBtnTantoSearch = Me.btnTantoSearch                 '基本情報：担当検索ボタン
            .PropTxthenkouID = Me.txthenkouID                       '基本情報：変更承認者ID
            .PropTxthenkouNM = Me.txthenkouNM                       '基本情報：変更承認者氏名
            .PropBtnhenkouMY = Me.btnMyhenkouID                     '基本情報：変更承認者私ボタン
            .PropBtnhenkouSearch = Me.btnhenkouSearch               '基本情報：変更承認者検索ボタン
            .PropTxtsyoninID = Me.txtsyoninID                       '基本情報：承認記録者ID
            .PropTxtsyoninNM = Me.txtsyoninNM                       '基本情報：承認記録者氏名
            .PropBtnsyoninMY = Me.btnMysyoninID                     '基本情報：承認記録者私ボタン
            .PropBtnsyoninSearch = Me.btnsyoninSearch               '基本情報：承認記録者検索ボタン

            .PropTxtTitle = Me.txtTitle                             '基本情報：タイトル
            .PropTxtNaiyo = Me.txtNaiyo                             '基本情報：内容
            .PropTxtTaisyo = Me.txtTaisyo                           '基本情報：対処

            .PropVwFileInfo = Me.vwFileInfo                         '基本情報：関連ファイル情報：関連ファイルスプレッド
            .PropBtnAddRow_File = Me.btnAddRow_File                 '基本情報：関連ファイル情報：＋
            .PropBtnRemoveRow_File = Me.btnRemoveRow_File           '基本情報：関連ファイル情報：ー
            .PropBtnOpenFile = Me.btnOpenFile                       '基本情報：関連ファイル情報：開
            .PropBtnSaveFile = Me.btnSaveFile                       '基本情報：関連ファイル情報：ダ

            .PropVwMeeting = Me.vwMeeting                           '会議情報：スプレッド
            .PropBtnAddRow_meeting = Me.btnAddRow_meeting           '会議情報：＋
            .PropBtnRemoveRow_meeting = Me.btnRemoveRow_meeting     '会議情報：－

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

            .PropVwKankei = Me.vwKankei                             '関係情報：関係者情報スプレッド
            .PropBtnAddRow_Grp = Me.btnAddRow_relaG                 '関係情報：グループ行追加ボタン
            .PropBtnAddRow_Usr = Me.btnAddRow_RelaU                 '関係情報：ユーザー行追加ボタン
            .PropBtnRemoveRow_Kankei = Me.btnRemoveRow_Kankei       '関係情報：関係者情報行削除ボタン

            .PropTxtGrpHistory = Me.txtGrpHistory                   '担当履歴情報：担当G
            .PropTxtTantoHistory = Me.txtTantoHistory               '担当履歴情報：担当U

            .PropVwprocessLinkInfo = Me.vwProcessLinkInfo           'プロセスリンク情報：プロセスリンクスプレッド
            .PropBtnAddRow_plink = Me.btnAddRow_plink               'プロセスリンク情報：＋
            .PropBtnRemoveRow_plink = Me.btnRemoveRow_Plink         'プロセスリンク情報：ー

            .PropVwCYSPR = Me.vwCyspr                               'CYSPR：プロセスリンクスプレッド
            .PropBtnAddRow_CYSPR = Me.btnAddRow_Cyspr               'CYSPR：＋
            .PropBtnRemoveRow_CYSPR = Me.btnRemoveRow_Cyspr         'CYSPR：ー

            .PropBtnReg = Me.btnReg                                 'フッタ：登録ボタン
            .PropBtnMail = Me.btnMail                               'フッタ：メール作成ボタン
            .PropBtnRelease = Me.BtnRelease                         'フッタ：リリース登録ボタン
            .PropBtnBack = Me.btnBack                               'フッタ：戻るボタン

            'システムエラー事前対応処理
            If logicHBKE0201.DoProcForErrorMain(dataHBKE0201) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            '★初期化設定

            'スプレッド行のクリアを行う
            ' 削除する行数が0でない条件付加
            With .PropVwFileInfo.Sheets(0)
                If .Rows.Count > 0 Then
                    .RemoveRows(0, .Rows.Count)
                End If
            End With
            With .PropVwKankei.Sheets(0)
                If .Rows.Count > 0 Then
                    .RemoveRows(0, .Rows.Count)
                End If
            End With
            With .PropVwprocessLinkInfo.Sheets(0)
                If .Rows.Count > 0 Then
                    .RemoveRows(0, .Rows.Count)
                End If
            End With
            With .PropVwCYSPR.Sheets(0)
                If .Rows.Count > 0 Then
                    .RemoveRows(0, .Rows.Count)
                End If
            End With
            With .PropVwMeeting.Sheets(0)
                If .Rows.Count > 0 Then
                    .RemoveRows(0, .Rows.Count)
                End If
            End With

            'データテーブル
            .PropDtTantoRireki = Nothing

            'メール関連
            .PropTxtRegGp = ""
            .PropTxtRegUsr = ""
            .PropTxtRegDT = ""
            .PropTxtUpdateGp = ""
            .PropTxtUpdateUsr = ""
            .PropTxtUpdateDT = ""

            '★プロパティ設定

            '[add] 2012/09/24 s.tsuruta 完了メッセージ表示修正 START
            'タイマーのインターバル設定
            Me.timKanryo.Interval = MSG_DISP_TIMER
            .PropLblkanryoMsg.Font = New Font(Me.Font.Name, Me.Font.Size, FontStyle.Bold)
            '[add] 2012/09/24 s.tsuruta 完了メッセージ表示修正 END

            'コンボボックスMaxDrop取得設定
            Dim intMaxdrop As Integer
            If commonLogicHBK.ChangeListSize(.PropCmbprocessStateCD.Font.Height, Screen.GetWorkingArea(Me).Height, intMaxdrop) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If
            .PropCmbSystemNmb.PropMaxDrop = intMaxdrop - 10      '対象システム

        End With

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)


        '新規登録モードの場合
        If dataHBKE0201.PropStrProcMode = PROCMODE_NEW Then
            '関係 -、ロック -
            Me.Text = "ひびき：変更登録"
            '新規モード画面初期表示メイン処理
            If logicHBKE0201.InitFormNewModeMain(dataHBKE0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

        Else

            '対応関係者チェック処理(dataHBKE0201.PropBlnChkKankei)
            If logicHBKE0201.KankeiCheckMain(dataHBKE0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            '1.	呼出し元画面が「変更検索一覧」画面の場合(問題登録から新規で開かれた場合、且つ登録→で編集モードで表示された場合含む）
            If dataHBKE0201.PropIntOwner <> SCR_CALLMOTO_HOKA Then

                '(ア)	編集モード
                If dataHBKE0201.PropIntChkKankei = KANKEI_CHECK_EDIT Then

                    'ロック設定メイン処理(dataHBKE0201.PropBlnBeLockedFlg )
                    If logicHBKE0201.LockMain(dataHBKE0201) = False Then
                        'システムエラー発生時はトランザクション系コントロールを非活性にする
                        If puErrMsg.StartsWith(HBK_E001) Then
                            If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
                                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                                Exit Sub
                            End If
                        End If
                        'エラーメッセージ表示
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        '処理終了
                        Exit Sub
                    End If

                    If dataHBKE0201.PropBlnBeLockedFlg = False Then
                        '関係○、ロック○
                        Me.Text = "ひびき：変更登録"
                        '編集モード画面初期表示メイン処理
                        If logicHBKE0201.InitFormEditModeMain(dataHBKE0201) = False Then
                            'システムエラー発生時はトランザクション系コントロールを非活性にする
                            If puErrMsg.StartsWith(HBK_E001) Then
                                If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
                                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                                    Exit Sub
                                End If
                            End If
                            'エラーメッセージ表示
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            '処理終了
                            Exit Sub
                        End If

                    Else
                        '関係○、ロック×
                        Me.Text = "ひびき：変更登録"
                        '参照モード変更
                        dataHBKE0201.PropStrProcMode = PROCMODE_REF

                        '参照モード画面初期表示メイン処理
                        If logicHBKE0201.InitFormRefModeMain(dataHBKE0201) = False Then
                            'システムエラー発生時はトランザクション系コントロールを非活性にする
                            If puErrMsg.StartsWith(HBK_E001) Then
                                If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
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
                        MsgBox(dataHBKE0201.PropStrBeLockedMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, TITLE_INFO)

                    End If

                Else
                    '(イ)	参照モード
                    Me.Text = "ひびき：変更登録"
                    '参照モード変更
                    dataHBKE0201.PropStrProcMode = PROCMODE_REF

                    '参照モード画面初期表示メイン処理
                    If logicHBKE0201.InitFormRefModeMain(dataHBKE0201) = False Then
                        'システムエラー発生時はトランザクション系コントロールを非活性にする
                        If puErrMsg.StartsWith(HBK_E001) Then
                            If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
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

            Else    '2.呼出し元画面が「変更検索一覧」画面以外の場合

                '(ア)	参照モード 
                If dataHBKE0201.PropIntChkKankei <> KANKEI_CHECK_NONE Then

                    Me.Text = "ひびき：変更登録"
                    'CLOSING処理の回避用に参照モードとする
                    dataHBKE0201.PropStrProcMode = PROCMODE_REF

                    '参照モード画面初期表示メイン処理
                    If logicHBKE0201.InitFormRefModeMain(dataHBKE0201) = False Then
                        'システムエラー発生時はトランザクション系コントロールを非活性にする
                        If puErrMsg.StartsWith(HBK_E001) Then
                            If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
                                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                                Exit Sub
                            End If
                        End If
                        'エラーメッセージ表示
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        '処理終了
                        Exit Sub
                    End If

                Else
                    '(イ)	参照不可

                    'CLOSING処理の回避用に参照モードとする
                    dataHBKE0201.PropStrProcMode = PROCMODE_REF
                    'エラーメッセージ設定
                    puErrMsg = E0201_E016
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '画面閉じる
                    Me.Close()
                    '処理終了
                    Exit Sub
                End If

            End If


        End If

        '初期カーソル位置
        'dataHBKE0201.PropCmbprocessStateCD.Focus()


    End Sub

    ''' <summary>
    ''' フォーム初期表示時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>カーソル位置の設定を行う
    ''' <para>作成情報：2012/09/24 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKC0201_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        '初期カーソル
        Me.dtpKaisiDT.txtDate.Focus()
        Me.dtpKaisiDT.txtDate.SelectAll()

    End Sub


    ''' <summary>
    ''' [解除]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>当画面のロックを解除し、編集モードで表示する
    ''' <para>作成情報：2012/08/22 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnUnlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grpLoginUser.btnUnlockClick

        'ロック解除処理を行う
        If logicHBKE0201.UnlockWhenClickBtnUnlockMain(dataHBKE0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
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
    ''' 基本情報：担当グループデータソース変更時の処理
    ''' </summary>
    ''' <param name="sender">引数sender</param>
    ''' <param name="e">引数e</param>
    ''' <remarks>担当グループコンボボックスのサイズを計算し、設定する
    ''' <para>作成情報：
    ''' <p>改定情報：2010/08/08 r.hoshino</p>
    ''' </para></remarks>
    Private Sub cmbTantoGrpCD_DataSourceChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTantoGrpCD.DataSourceChanged

        'コンボボックスサイズ変更メイン処理
        If logicHBKE0201.ComboBoxResizeMain(sender) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 基本情報：ステータスデータソース変更時の処理
    ''' </summary>
    ''' <param name="sender">引数sender</param>
    ''' <param name="e">引数e</param>
    ''' <remarks>ステータスコンボボックスのサイズを計算し、設定する
    ''' <para>作成情報：
    ''' <p>改定情報：2010/08/14 r.hoshino</p>
    ''' </para></remarks>
    Private Sub cmbProcessStateCD_DataSourceChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProcessStateCD.DataSourceChanged
        'コンボボックスサイズ変更メイン処理
        If logicHBKE0201.ComboBoxResizeMain(sender) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub




    ''' <summary>
    ''' 開始[時]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>時間入力画面を表示する
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnKaisi_HM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKaisi_HM.Click
        Dim HBKZ0801 As New HBKZ0801

        'パラメータセット
        With HBKZ0801.dataHBKZ0801
            .PropStrDate = dataHBKE0201.PropDtpKaisiDT.txtDate.Text
            .PropStrTime = dataHBKE0201.PropTxtKaisiDT_HM.PropTxtTime.Text
        End With

        '画面開く
        If HBKZ0801.ShowDialog Then
            dataHBKE0201.PropDtpKaisiDT.txtDate.Text = HBKZ0801.dataHBKZ0801.PropDtpSetDate.txtDate.Text
            dataHBKE0201.PropTxtKaisiDT_HM.PropTxtTime.Text = HBKZ0801.dataHBKZ0801.PropTxtSetTime.Text
        End If
    End Sub

    ''' <summary>
    ''' 完了[時]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>時間入力画面を表示する
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnKanryo_HM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKanryo_HM.Click
        Dim HBKZ0801 As New HBKZ0801

        'パラメータセット
        With HBKZ0801.dataHBKZ0801
            .PropStrDate = dataHBKE0201.PropDtpKanryoDT.txtDate.Text
            .PropStrTime = dataHBKE0201.PropTxtKanryoDT_HM.PropTxtTime.Text
        End With

        '画面開く
        If HBKZ0801.ShowDialog Then
            dataHBKE0201.PropDtpKanryoDT.txtDate.Text = HBKZ0801.dataHBKZ0801.PropDtpSetDate.txtDate.Text
            dataHBKE0201.PropTxtKanryoDT_HM.PropTxtTime.Text = HBKZ0801.dataHBKZ0801.PropTxtSetTime.Text
        End If
    End Sub



    ''' <summary>
    ''' 基本情報：担当IDの入力後Enter時
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>IDをキーに氏名を取得し設定する
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub txtTantoID_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtTantoID.PreviewKeyDown
        If e.KeyValue = Keys.Enter Then

            '検索用パラメータ設定
            dataHBKE0201.PropStrSeaKey = dataHBKE0201.PropTxtTantoID.Text         '担当ID

            If logicHBKE0201.GetTantoDataMain(dataHBKE0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            '取得できない場合ブランクをセット
            dataHBKE0201.PropTxtTantoNM.Text = ""
            dataHBKE0201.PropCmbTantoGrpCD.SelectedValue = ""
            If dataHBKE0201.PropDtResultSub IsNot Nothing Then
                If dataHBKE0201.PropDtResultSub.Rows.Count > 0 Then
                    dataHBKE0201.PropTxtTantoNM.Text = dataHBKE0201.PropDtResultSub.Rows(0).Item("hbkusrnm")
                End If
                If dataHBKE0201.PropDtResultSub.Rows.Count = 1 Then
                    dataHBKE0201.PropCmbTantoGrpCD.SelectedValue = dataHBKE0201.PropDtResultSub.Rows(0).Item("groupcd")
                End If
            End If

        End If
    End Sub

    ''' <summary>
    ''' 担当者：[私]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>ログインユーザーID、氏名、グループ名を設定する
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnMytantoID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMytantoID.Click

        'パラメータセット
        With dataHBKE0201
            .PropCmbTantoGrpCD.SelectedValue = PropWorkGroupCD
            .PropTxtTantoID.Text = PropUserId
            .PropTxtTantoNM.Text = PropUserName
        End With

    End Sub

    ''' <summary>
    ''' 担当者：[検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>「ひびきユーザー検索一覧」画面を表示する。
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnTantoSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTantoSearch.Click

        '「ひびきユーザー検索一覧」インスタンス作成
        Dim HBKZ0101 As New HBKZ0101

        'パラメータセット
        With HBKZ0101.dataHBKZ0101
            .PropMode = SELECT_MODE_SINGLE                           'モード：単一選択
            .PropArgs = dataHBKE0201.PropTxtTantoNM.Text             '検索条件：担当氏名
            .PropSplitMode = SPLIT_MODE_AND                          '検索条件区切り
        End With

        'グループ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKE0201.PropDtResultSub = HBKZ0101.ShowDialog()

        'グループ名、ID、NMを更新
        If dataHBKE0201.PropDtResultSub IsNot Nothing Then
            dataHBKE0201.PropCmbTantoGrpCD.SelectedValue = dataHBKE0201.PropDtResultSub.Rows(0).Item("グループID")
            dataHBKE0201.PropTxtTantoID.Text = dataHBKE0201.PropDtResultSub.Rows(0).Item("ユーザーID")
            dataHBKE0201.PropTxtTantoNM.Text = dataHBKE0201.PropDtResultSub.Rows(0).Item("ユーザー氏名")
        End If

    End Sub



    ''' <summary>
    ''' 基本情報：変更承認者IDの入力後Enter時
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>IDをキーに氏名を取得し設定する
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub txthenkouID_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txthenkouID.PreviewKeyDown
        If e.KeyValue = Keys.Enter Then

            '検索用パラメータ設定
            dataHBKE0201.PropStrSeaKey = dataHBKE0201.PropTxthenkouID.Text         'ID

            If logicHBKE0201.GetHenkouDataMain(dataHBKE0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            '取得できない場合ブランクをセット
            dataHBKE0201.PropTxthenkouNM.Text = ""
            If dataHBKE0201.PropDtResultSub IsNot Nothing Then
                If dataHBKE0201.PropDtResultSub.Rows.Count > 0 Then
                    dataHBKE0201.PropTxthenkouNM.Text = dataHBKE0201.PropDtResultSub.Rows(0).Item("endusrnm")
                End If
            End If

        End If
    End Sub

    ''' <summary>
    ''' 変更承認者：[私]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>ログインユーザーID、氏名を設定する
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnMyhenkouID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMyhenkouID.Click

        'パラメータセット
        With dataHBKE0201
            .PropTxthenkouID.Text = PropUserId
            .PropTxthenkouNM.Text = PropUserName
        End With

    End Sub

    ''' <summary>
    ''' 変更承認者：[検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>「エンドユーザー検索一覧」画面を表示する。
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnhenkouSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnhenkouSearch.Click

        Dim HBKZ0201 As New HBKZ0201

        'パラメータセット
        With HBKZ0201.dataHBKZ0201
            .PropMode = SELECT_MODE_SINGLE                             'モード：単一選択
            .PropArgs = dataHBKE0201.PropTxthenkouNM.Text              '検索条件：変更承認者氏名
            .PropSplitMode = SPLIT_MODE_AND                            '検索条件区切り：AND
        End With

        'エンドユーザ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKE0201.PropDtResultSub = HBKZ0201.ShowDialog()

        If dataHBKE0201.PropDtResultSub IsNot Nothing Then
            dataHBKE0201.PropTxthenkouID.Text = dataHBKE0201.PropDtResultSub.Rows(0).Item("endusrid")           '変更承認者ID
            dataHBKE0201.PropTxthenkouNM.Text = dataHBKE0201.PropDtResultSub.Rows(0).Item("endusrnm")           '変更承認者氏名
        End If

    End Sub



    ''' <summary>
    ''' 基本情報：承認記録者IDの入力後Enter時
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>IDをキーに氏名を取得し設定する
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub txtsyoninID_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtsyoninID.PreviewKeyDown
        If e.KeyValue = Keys.Enter Then

            '検索用パラメータ設定
            dataHBKE0201.PropStrSeaKey = dataHBKE0201.PropTxtsyoninID.Text         'ID

            If logicHBKE0201.GetSyoninDataMain(dataHBKE0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            '取得できない場合ブランクをセット
            dataHBKE0201.PropTxtsyoninNM.Text = ""
            If dataHBKE0201.PropDtResultSub IsNot Nothing Then
                If dataHBKE0201.PropDtResultSub.Rows.Count > 0 Then
                    dataHBKE0201.PropTxtsyoninNM.Text = dataHBKE0201.PropDtResultSub.Rows(0).Item("hbkusrnm")
                End If
            End If

        End If
    End Sub

    ''' <summary>
    ''' 承認記録者：[私]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>ログインユーザーID、氏名を設定する
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnMysyoninID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMysyoninID.Click

        'パラメータセット
        With dataHBKE0201
            .PropTxtsyoninID.Text = PropUserId
            .PropTxtsyoninNM.Text = PropUserName
        End With

    End Sub

    ''' <summary>
    ''' 承認記録者：[検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>「ひびきユーザー検索一覧」画面を表示する。
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnsyoninSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsyoninSearch.Click

        '「ひびきユーザー検索一覧」インスタンス作成
        Dim HBKZ0101 As New HBKZ0101

        'パラメータセット
        With HBKZ0101.dataHBKZ0101
            .PropMode = SELECT_MODE_SINGLE                           'モード：単一選択
            .PropArgs = dataHBKE0201.PropTxtsyoninNM.Text             '検索条件：担当氏名
            .PropSplitMode = SPLIT_MODE_AND                          '検索条件区切り
        End With

        '検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKE0201.PropDtResultSub = HBKZ0101.ShowDialog()

        'ID、NMを更新
        If dataHBKE0201.PropDtResultSub IsNot Nothing Then
            dataHBKE0201.PropTxtsyoninID.Text = dataHBKE0201.PropDtResultSub.Rows(0).Item("ユーザーID")
            dataHBKE0201.PropTxtsyoninNM.Text = dataHBKE0201.PropDtResultSub.Rows(0).Item("ユーザー氏名")
        End If

    End Sub




    ''' <summary>
    ''' 関係者情報：[＋グループ]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>グループ検索画面を表示し、選択されたグループを当画面にセットする
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_relaG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_relaG.Click

        'グループ検索画面インスタンス作成
        Dim HBKZ0301 As New HBKZ0301

        'パラメータセット
        With HBKZ0301.dataHBKZ0301
            .PropMode = SELECT_MODE_MULTI       'モード：複数選択
            .PropArgs = String.Empty            '検索条件：なし
            .PropSplitMode = SPLIT_MODE_AND     '検索条件区切り：AND
        End With

        'グループ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKE0201.PropDtResultSub = HBKZ0301.ShowDialog()

        '関係者情報一覧に取得データをセット
        If logicHBKE0201.SetGroupToVwRelationMain(dataHBKE0201) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 関係者情報：[＋ユーザー]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>ひびきユーザー検索画面を表示し、選択されたユーザーを当画面にセットする
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_RelaU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_RelaU.Click

        'ひびきユーザー検索画面インスタンス作成
        Dim HBKZ0101 As New HBKZ0101

        'パラメータセット
        With HBKZ0101.dataHBKZ0101
            .PropMode = SELECT_MODE_MULTI           'モード：複数選択
            .PropArgs = String.Empty                '検索条件：なし
            .PropSplitMode = SPLIT_MODE_AND         '検索条件区切り：AND
        End With

        'ひびきユーザー検索画面を表示し、戻り値としてデータテーブルを取得
        '
        dataHBKE0201.PropDtResultSub = HBKZ0101.ShowDialog()

        '関係者情報一覧に取得データをセット
        If logicHBKE0201.SetUserToVwRelationMain(dataHBKE0201) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 関係者情報：[－]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>関係者情報一覧の選択行を削除する
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_Kankei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveRow_Kankei.Click

        '関係者情報一覧選択行削除処理
        If logicHBKE0201.RemoveRowKankeiMain(dataHBKE0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
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
    ''' プロセスリンク情報：[＋]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>プロセス検索画面を表示し、選択されたプロセスをに当画面にセットする
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_plink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_plink.Click
        Dim HBKZ0401 As New HBKZ0401
        'パラメータセット
        With HBKZ0401.dataHBKZ0401
            .PropMode = SELECT_MODE_MULTI                               'モード：単一選択
            .PropArgs = String.Empty                                    '検索条件：なし
            .PropSplitMode = SPLIT_MODE_AND                             '検索条件区切り
            .PropStrFromProcessKbn = PROCESS_TYPE_CHANGE                'プロセス区分
            .PropStrFromProcessNmb = dataHBKE0201.PropIntChgNmb         'プロセス番号
        End With

        'グループ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKE0201.PropDtResultSub = HBKZ0401.ShowDialog()

        'プロセスリンク一覧に取得データをセット
        If logicHBKE0201.AddRowpLinkMain(dataHBKE0201) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' プロセスリンク情報：[－]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>プロセスリンク情報一覧の選択行を削除する
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_plink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveRow_Plink.Click

        'プロセスリンク情報一覧選択行削除処理
        If logicHBKE0201.RemoveRowpLinkMain(dataHBKE0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
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
    ''' プロセスリンク一覧：セルダブルクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択した管理番号の登録画面へ参照モードで遷移する
    ''' <para>作成情報：2012/08/09 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwProcessLinkInfo_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwProcessLinkInfo.CellDoubleClick

        '列ヘッダーがクリックされた場合は処理しない
        If e.ColumnHeader = True Or e.RowHeader = True Then
            Exit Sub
        End If

        '変数宣言
        Dim strSelectKbn As String = _
            Me.vwProcessLinkInfo.Sheets(0).Cells(e.Row, LogicHBKC0201.COL_processLINK_KBN).Value  '選択行の区分（コード）
        Dim strSelectNo As String = _
            Me.vwProcessLinkInfo.Sheets(0).Cells(e.Row, LogicHBKC0201.COL_processLINK_NO).Value   '選択行の管理番号

        '関係者チェック_結果格納用
        Dim intchkkankei As Integer = 0

        'プロセスリンク対応関係者チェック処理(dataHBKE0201.PropintChkKankei) 
        If logicHBKE0201.PlinkKankeiCheckMain(intchkkankei, strSelectNo, strSelectKbn) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        '関係者でない場合エラーメッセージを表示
        If intchkkankei = KANKEI_CHECK_NONE Then
            'エラーメッセージ設定
            puErrMsg = E0201_E016
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

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

            'MsgBox("リリース登録画面に遷移します")

        End If
    End Sub



    ''' <summary>
    ''' 関連ファイル：[+]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>関連ファイル設定画面を表示し、選択されたファイル情報をに当画面にセットする
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_File_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_File.Click
        Dim HBKZ1101 As New HBKZ1101
        'パラメータセット
        With HBKZ1101.dataHBKZ1101

        End With


        '関連ファイル検索画面を表示し、戻り値としてデータテーブルを取得
        If HBKZ1101.ShowDialog() Then

            dataHBKE0201.PropTxtFileNaiyo = HBKZ1101.dataHBKZ1101.PropTxtFileNaiyo.Text
            dataHBKE0201.PropTxtFilePath = HBKZ1101.dataHBKZ1101.PropTxtFilePath.Text

            '関係ファイル一覧に取得データをセット
            If logicHBKE0201.AddRowFileinfoMain(dataHBKE0201) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

        End If


    End Sub

    ''' <summary>
    ''' 関連ファイル：[-]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>関連ファイル情報一覧の選択行を削除する。
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_File_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveRow_File.Click
        '関係ファイル一覧選択行削除処理
        If logicHBKE0201.RemoveRowFileInfoMain(dataHBKE0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
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
    ''' 関連ファイル：[開]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>登録済みのファイルを読み取り専用で開く。
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnOpenFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenFile.Click

        If dataHBKE0201.PropStrProcMode = PROCMODE_EDIT OrElse _
             dataHBKE0201.PropStrProcMode = PROCMODE_REF Then        '編集モード  、参照モード

            '[Del] 2012/10/30 s.yamaguchi START
            'If (Me.vwFileInfo.Visible = True) AndAlso (Me.vwFileInfo.Sheets(0).Rows.Count <> 0) Then

            '    Dim cr() As FarPoint.Win.Spread.Model.CellRange
            '    cr = Me.vwFileInfo.ActiveSheet.GetSelections()

            '    ' 未選択の場合エラーメッセージを表示する
            '    If cr.Length = 0 Then
            '        'エラーメッセージ表示
            '        MsgBox(E0201_E017, MsgBoxStyle.Critical, TITLE_ERROR)
            '        Return
            '    End If

            '    'フォーカスが移動したときの行数を取得する（列ヘッダ＆全選択が選択されている場合はエラー）
            '    For i As Integer = 0 To cr.Length - 1

            '        '行数が１以外のときはエラー
            '        If (cr(i).RowCount() <> 1) Then
            '            'エラーメッセージ表示
            '            MsgBox(E0201_E017, MsgBoxStyle.Critical, TITLE_ERROR)
            '            Return
            '        ElseIf (cr(i).RowCount() = 1) Then

            '            '選択行番号をデータクラスにセット
            '            dataHBKE0201.PropIntSelectedRow = Me.vwFileInfo.ActiveSheet.ActiveRowIndex

            '            'ファイルオープン処理
            '            If logicHBKE0201.FileOpenMain(dataHBKE0201) = False Then
            '                'システムエラー発生時はトランザクション系コントロールを非活性にする
            '                If puErrMsg.StartsWith(HBK_E001) Then
            '                    If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
            '                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '                        Exit Sub
            '                    End If
            '                End If
            '                'エラーメッセージ表示
            '                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '                '処理終了
            '                Exit Sub
            '            End If
            '            Return
            '        End If
            '    Next
            'End If
            '[Del] 2012/10/30 s.yamaguchi END

            If (Me.vwFileInfo.Visible = True) AndAlso (Me.vwFileInfo.Sheets(0).Rows.Count <> 0) Then

                '[Add] 2012/10/30 s.yamaguchi START
                '変数宣言
                Dim intSelectedRowFrom As Integer                   '選択開始行番号
                Dim intSelectedRowTo As Integer                     '選択終了行番号

                '選択開始行、終了行取得
                intSelectedRowFrom = Me.vwFileInfo.Sheets(0).Models.Selection.AnchorRow
                intSelectedRowTo = Me.vwFileInfo.Sheets(0).Models.Selection.LeadRow

                '行選択を明示的に行う。
                With Me.vwFileInfo
                    .ActiveSheet.Models.Selection.AddSelection(.ActiveSheet.ActiveRowIndex, _
                                                               .ActiveSheet.ActiveColumnIndex, _
                                                               1, _
                                                               System.Math.Abs(intSelectedRowTo - intSelectedRowFrom) + 1)
                End With

                '検索結果の選択数が一件以外の時はエラーメッセージ出力
                If Me.vwFileInfo.Sheets(0).SelectionCount <> 1 _
                   Or intSelectedRowTo - intSelectedRowFrom <> 0 Then
                    puErrMsg = E0201_E017
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
                '[Add] 2012/10/30 s.yamaguchi END

                '選択行番号をデータクラスにセット
                dataHBKE0201.PropIntSelectedRow = Me.vwFileInfo.ActiveSheet.ActiveRowIndex

                'ファイルオープン処理
                If logicHBKE0201.FileOpenMain(dataHBKE0201) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
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
        End If
    End Sub

    ''' <summary>
    ''' 関連ファイル：[ダ]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>登録済みのファイルを指定先にダウンロードする。
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSaveFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveFile.Click

        If dataHBKE0201.PropStrProcMode = PROCMODE_EDIT OrElse _
              dataHBKE0201.PropStrProcMode = PROCMODE_REF Then        '編集モード  、参照モード

            '[Del] 2012/10/30 s.yamaguchi START
            'If (Me.vwFileInfo.Visible = True) AndAlso (Me.vwFileInfo.Sheets(0).Rows.Count <> 0) Then

            '    Dim cr() As FarPoint.Win.Spread.Model.CellRange
            '    cr = Me.vwFileInfo.ActiveSheet.GetSelections()

            '    ' 未選択の場合エラーメッセージを表示する
            '    If cr.Length = 0 Then
            '        'エラーメッセージ表示
            '        MsgBox(E0201_E017, MsgBoxStyle.Critical, TITLE_ERROR)
            '        Return
            '    End If

            '    'フォーカスが移動したときの行数を取得する（列ヘッダ＆全選択が選択されている場合はエラー）
            '    For i As Integer = 0 To cr.Length - 1

            '        '行数が１以外のときはエラー
            '        If (cr(i).RowCount() <> 1) Then
            '            'エラーメッセージ表示
            '            MsgBox(E0201_E017, MsgBoxStyle.Critical, TITLE_ERROR)
            '            Return
            '        ElseIf (cr(i).RowCount() = 1) Then

            '            '選択行番号をデータクラスにセット
            '            dataHBKE0201.PropIntSelectedRow = Me.vwFileInfo.ActiveSheet.ActiveRowIndex

            '            '編集モード画面処理
            '            If logicHBKE0201.FileDownLoadMain(dataHBKE0201) = False Then
            '                'システムエラー発生時はトランザクション系コントロールを非活性にする
            '                If puErrMsg.StartsWith(HBK_E001) Then
            '                    If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
            '                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '                        Exit Sub
            '                    End If
            '                End If
            '                'エラーメッセージ表示
            '                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '                '処理終了
            '                Exit Sub
            '            End If
            '            Return
            '        End If
            '    Next
            'End If
            '[Del] 2012/10/30 s.yamaguchi END

            If (Me.vwFileInfo.Visible = True) AndAlso (Me.vwFileInfo.Sheets(0).Rows.Count <> 0) Then

                '[Add] 2012/10/30 s.yamaguchi START
                '変数宣言
                Dim intSelectedRowFrom As Integer                   '選択開始行番号
                Dim intSelectedRowTo As Integer                     '選択終了行番号

                '選択開始行、終了行取得
                intSelectedRowFrom = Me.vwFileInfo.Sheets(0).Models.Selection.AnchorRow
                intSelectedRowTo = Me.vwFileInfo.Sheets(0).Models.Selection.LeadRow

                '行選択を明示的に行う。
                With Me.vwFileInfo
                    .ActiveSheet.Models.Selection.AddSelection(.ActiveSheet.ActiveRowIndex, _
                                                               .ActiveSheet.ActiveColumnIndex, _
                                                               1, _
                                                               System.Math.Abs(intSelectedRowTo - intSelectedRowFrom) + 1)
                End With

                '検索結果の選択数が一件以外の時はエラーメッセージ出力
                If Me.vwFileInfo.Sheets(0).SelectionCount <> 1 _
                   Or intSelectedRowTo - intSelectedRowFrom <> 0 Then
                    puErrMsg = E0201_E017
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
                '[Add] 2012/10/30 s.yamaguchi END

                '選択行番号をデータクラスにセット
                dataHBKE0201.PropIntSelectedRow = Me.vwFileInfo.ActiveSheet.ActiveRowIndex

                '編集モード画面処理
                If logicHBKE0201.FileDownLoadMain(dataHBKE0201) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
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
        End If
    End Sub

    ''' <summary>
    ''' CYSPR：[+]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>CYSPR情報一覧に空行を1行追加する。
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_CYSPR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_Cyspr.Click

        'CYSPR一覧に取得データをセット
        If logicHBKE0201.AddRowCYSPRMain(dataHBKE0201) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' CYSPR：[-]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>CYSPR情報一覧の選択行を削除する。
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_CYSPR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveRow_Cyspr.Click

        'CYSPR一覧選択行削除処理
        If logicHBKE0201.RemoveRowCYSPRMain(dataHBKE0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
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
    ''' 会議情報：[＋]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>会議検索一覧を表示し選択されたデータを画面に設定する
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : 2012/08/02 r.hoshino　会議情報変更に伴う修正</p>
    ''' </para></remarks>
    Private Sub btnAddRow_meeting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_meeting.Click

        Dim HBKC0301 As New HBKC0301
        'パラメータセット
        With HBKC0301.dataHBKC0301
            .PropBlnTranFlg = SELECT_MODE_NOTMENU                               '前画面パラメータ：メニュー遷移フラグ
            .PropProcessKbn = PROCESS_TYPE_CHANGE                               'プロセス区分
            .PropProcessNmb = dataHBKE0201.PropIntChgNmb                        'プロセス番号
            .PropTitle = dataHBKE0201.PropTxtTitle.Text                         'タイトル
        End With

        'クローズ処理の追加
        AddHandler CType(HBKC0301, Form).FormClosed, AddressOf Meeting_FormClosed

        '会議情報検索画面を表示
        If HBKC0301.ShowDialog = DIALOG_RETURN_OK Then

            '検索結果を取得
            dataHBKE0201.PropDtResultSub = HBKC0301.dataHBKC0301.PropDtReturnSub

            '会議情報一覧に取得データをセット
            If logicHBKE0201.AddRowMeetingMain(dataHBKE0201) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

        End If

    End Sub

    ''' <summary>
    ''' [会議一覧]クローズ後の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>閉じた後会議結果を再取得する
    ''' <para>作成情報：2012/09/12 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub Meeting_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs)

        '会議結果情報再取得処理
        If logicHBKE0201.RefreshMeetingMain(dataHBKE0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
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
    ''' 会議情報：[ー]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択したスプレッド行を削除する
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_meeting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveRow_meeting.Click
        '会議情報一覧選択行削除処理
        If logicHBKE0201.RemoveRowMeetingMain(dataHBKE0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
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
    ''' 会議一覧：セルダブルクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択した管理番号の登録画面へ遷移する
    ''' <para>作成情報：2012/08/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwMeeting_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwMeeting.CellDoubleClick

        '列ヘッダーがクリックされた場合は処理しない
        If e.ColumnHeader = True Or e.RowHeader = True Then
            Exit Sub
        End If

        '変数宣言
        Dim strSelectKbn As String = _
            Me.vwMeeting.Sheets(0).Cells(e.Row, logicHBKE0201.COL_MEETING_NINCD).Value  '選択行の区分（コード）
        Dim strSelectNo As String = _
            Me.vwMeeting.Sheets(0).Cells(e.Row, logicHBKE0201.COL_MEETING_NO).Value     '選択行の管理番号

        '会議記録登録
        Dim HBKC0401 As New HBKC0401

        '会議記録登録画面データクラスに対しプロパティ設定
        With HBKC0401.dataHBKC0401
            .PropBlnTranFlg = 0                             'メニュー遷移フラグ（0:メニュー以外から遷移、1:メニューから遷移）
            .PropProcessKbn = PROCESS_TYPE_CHANGE           'プロセス区分
            .PropProcessNmb = dataHBKE0201.PropIntChgNmb    'プロセス番号
            .PropStrProcMode = PROCMODE_EDIT                '処理モード：編集モード
            .PropIntMeetingNmb = strSelectNo                '会議番号
        End With

        'クローズ処理の追加
        AddHandler CType(HBKC0401, Form).FormClosed, AddressOf Meeting_FormClosed

        '当画面非表示
        Me.Hide()
        '会議記録登録画面表示
        HBKC0401.ShowDialog()
        '当画面表示
        Me.Show()

    End Sub




    ''' <summary>
    ''' [登録]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>入力チェックを行い、モードに応じて登録処理を行う。
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        dataHBKE0201.PropLblkanryoMsg.Text = ""
        Application.DoEvents()

        '処理モードに応じた入力チェックを行う
        If dataHBKE0201.PropStrProcMode = PROCMODE_NEW Then                 '新規登録モード

            '入力チェック処理      
            If logicHBKE0201.CheckInputValueMain(dataHBKE0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
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

        ElseIf dataHBKE0201.PropStrProcMode = PROCMODE_EDIT Then            '編集モード

            '【DB更新時】ロック解除チェック(dataHBKE0201.PropBlnBeLockedFlg)
            If logicHBKE0201.CheckBeUnlockedMain(dataHBKE0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
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
            If dataHBKE0201.PropBlnBeLockedFlg = False Then             '編集モード

                'ロック解除されていない時、入力チェックを行う
                If logicHBKE0201.CheckInputValueMain(dataHBKE0201) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
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

            ElseIf dataHBKE0201.PropBlnBeLockedFlg = True Then         '参照モード
                '※編集モードだったが、ロック解除されていた場合
                'ロック解除時、ログ出力処理と画面の再描画を行う
                If logicHBKE0201.OutputUnlockLogMain(dataHBKE0201) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
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


                'セットされているメッセージによってメッセージボックスのスタイルを変更する
                If dataHBKE0201.PropStrBeUnlockedMsg.StartsWith(String.Format(E0201_W001, dataHBKE0201.PropStrLogFilePath)) Then
                    'ロック解除メッセージ表示
                    MsgBox(dataHBKE0201.PropStrBeUnlockedMsg, MsgBoxStyle.Exclamation, TITLE_WARNING)

                ElseIf dataHBKE0201.PropStrBeUnlockedMsg.StartsWith(String.Format(E0201_E014, dataHBKE0201.PropStrLogFilePath)) Then
                    'データ更新エラーメッセージ
                    MsgBox(dataHBKE0201.PropStrBeUnlockedMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                End If
                '編集モードで画面再描画
                dataHBKE0201.PropStrProcMode = PROCMODE_EDIT
                HBKE0201_Load(Me, New EventArgs)
                Exit Sub
            End If

        End If



        '処理モードに応じた登録処理を行う
        If dataHBKE0201.PropStrProcMode = PROCMODE_NEW Then                 '新規登録モード

            '新規登録処理
            If logicHBKE0201.RegistDataOnNewModeMain(dataHBKE0201) = False Then

                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)

                'ログ出力を行う
                If logicHBKE0201.OutputUnlockLogMain(dataHBKE0201) = True Then

                    'セットされているメッセージによってメッセージボックスのスタイルを変更する
                    If dataHBKE0201.PropStrBeUnlockedMsg.StartsWith(String.Format(E0201_W001, dataHBKE0201.PropStrLogFilePath)) Then
                        'ロック解除メッセージ表示
                        MsgBox(dataHBKE0201.PropStrBeUnlockedMsg, MsgBoxStyle.Exclamation, TITLE_WARNING)

                    ElseIf dataHBKE0201.PropStrBeUnlockedMsg.StartsWith(String.Format(E0201_E014, dataHBKE0201.PropStrLogFilePath)) Then
                        'データ更新エラーメッセージ
                        MsgBox(dataHBKE0201.PropStrBeUnlockedMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    End If
                End If
                '処理終了
                Exit Sub
            End If

            '最終作業データ格納
            PropLastProcessKbn = PROCESS_TYPE_CHANGE
            PropLastProcessNmb = dataHBKE0201.PropIntChgNmb

            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default

            '登録完了メッセージ表示
            'MsgBox(E0201_I001, MsgBoxStyle.Information, TITLE_INFO)
            dataHBKE0201.PropLblkanryoMsg.Text = E0201_I001

            '呼び出し元画面を検索一覧にする
            dataHBKE0201.PropIntOwner = SCR_CALLMOTO_ICHIRAN

            '編集モードで画面再描画
            dataHBKE0201.PropStrProcMode = PROCMODE_EDIT
            HBKE0201_Load(Me, New EventArgs)

        ElseIf dataHBKE0201.PropStrProcMode = PROCMODE_EDIT Then            '編集モード

            '更新処理を行う
            If logicHBKE0201.RegistDataOnEditModeMain(dataHBKE0201) = False Then

                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)

                'ログ出力を行う
                If logicHBKE0201.OutputUnlockLogMain(dataHBKE0201) = True Then

                    'セットされているメッセージによってメッセージボックスのスタイルを変更する
                    If dataHBKE0201.PropStrBeUnlockedMsg.StartsWith(String.Format(E0201_W001, dataHBKE0201.PropStrLogFilePath)) Then
                        'ロック解除メッセージ表示
                        MsgBox(dataHBKE0201.PropStrBeUnlockedMsg, MsgBoxStyle.Exclamation, TITLE_WARNING)

                    ElseIf dataHBKE0201.PropStrBeUnlockedMsg.StartsWith(String.Format(E0201_E014, dataHBKE0201.PropStrLogFilePath)) Then
                        'データ更新エラーメッセージ
                        MsgBox(dataHBKE0201.PropStrBeUnlockedMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    End If
                End If

                '処理終了
                Exit Sub
            End If

            '最終作業データ格納
            PropLastProcessKbn = PROCESS_TYPE_CHANGE
            PropLastProcessNmb = dataHBKE0201.PropIntChgNmb

            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default

            '登録完了メッセージ表示
            'MsgBox(E0201_I001, MsgBoxStyle.Information, TITLE_INFO)
            dataHBKE0201.PropLblkanryoMsg.Text = E0201_I001

            '編集モードで画面再描画
            HBKE0201_Load(Me, New EventArgs)

        End If

        '[add] 2012/09/24 s.tsuruta 完了メッセージ表示修正 START
        'タイマーを開始する
        Me.timKanryo.Start()
        '[add] 2012/09/24 ss.tsuruta 完了メッセージ表示修正 END

    End Sub

    ''' <summary>
    ''' [メール作成]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>メール作成画面を開く
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnMail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMail.Click

        'メールテンプレート選択画面
        Dim HBKZ1001 As New HBKZ1001
        '変更登録（メール作成）処理クラスインスタンス化
        Dim logicHBKE0202 As New LogicHBKE0202
        Dim dataHBKE0202 As New DataHBKE0202

        'パラメータ設定
        With HBKZ1001.dataHBKZ1001
            .PropStrGroupCD = PropWorkGroupCD                    '前画面パラメータ：グループCD
            .PropStrGroupNM = PropWorkGroupName                  '前画面パラメータ：グループ名
            .PropStrProcessKbn = PROCESS_TYPE_CHANGE             '前画面パラメータ：プロセス区分
            .PropStrKigenCondCIKbnCD = ""                        '前画面パラメータ：期限切れ条件CI種別
            .PropStrKigenCondTypeKbn = ""                        '前画面パラメータ：期限切れ条件タイプ
            .PropStrKigenCondKigen = ""                          '前画面パラメータ：期限切れ条件期限
            .PropStrKigenCondKbn = ""                            '前画面パラメータ：期限切れ条件区分
        End With

        'メールフォーマット選択
        HBKZ1001.ShowDialog()

        'メールフォーマットデータ取得
        dataHBKE0202.PropDtReturnData = HBKZ1001.dataHBKZ1001.PropDtReturnData

        '当画面表示
        Me.Show()

        '戻り値のDataTableがNullだった場合、処理を中断
        If dataHBKE0202.PropDtReturnData Is Nothing Then
            Exit Sub
        End If

        '入力項目をデータクラスにセット
        With dataHBKE0202

            .PropStrNmb = dataHBKE0201.PropTxtNmb.Text                                      '変更管理番号

            .PropStrProcessStateCD = dataHBKE0201.PropCmbprocessStateCD.Text                'ステータス

            .PropStrKaisiDT = dataHBKE0201.PropDtpKaisiDT.txtDate.Text                      '開始日時
            .PropStrKaisiDT_HM = dataHBKE0201.PropTxtKaisiDT_HM.PropTxtTime.Text            '開始日時時分
            .PropStrKanryoDT = dataHBKE0201.PropDtpKanryoDT.txtDate.Text                    '完了日時
            .PropStrKanryoDT_HM = dataHBKE0201.PropTxtKanryoDT_HM.PropTxtTime.Text          '完了日時時分

            .PropStrTitle = dataHBKE0201.PropTxtTitle.Text                                  'タイトル
            .PropStrNaiyo = dataHBKE0201.PropTxtNaiyo.Text                                  '内容
            .PropStrRegGrpNM = dataHBKE0201.PropTxtRegGp                                    '登録者グループ名
            .PropStrRegNM = dataHBKE0201.PropTxtRegUsr                                      '登録者ユーザ名
            .PropStrRegDT = dataHBKE0201.PropTxtRegDT                                       '登録日時
            .PropStrUpdateGrpNM = dataHBKE0201.PropTxtUpdateGp                              '最終更新グループ名
            .PropStrUpdateNM = dataHBKE0201.PropTxtUpdateUsr                                '最終更新者
            .PropStrUpdateDT = dataHBKE0201.PropTxtUpdateDT                                 '最終更新日時
            .PropStrSystemNmb = dataHBKE0201.PropCmbSystemNmb.PropCmbColumns.SelectedValue  '対象システム

            .PropStrTantoGrpNM = dataHBKE0201.PropCmbTantoGrpCD.Text                        '担当グループ
            .PropStrTantoID = dataHBKE0201.PropTxtTantoID.Text                              '担当者ID
            .PropStrTantoNM = dataHBKE0201.PropTxtTantoNM.Text                              '担当者名

            .PropStrTaisyo = dataHBKE0201.PropTxtTaisyo.Text                                '対処

            .PropStrHenkouID = dataHBKE0201.PropTxthenkouID.Text                            '承認者ID
            .PropStrHenkouNM = dataHBKE0201.PropTxthenkouNM.Text                            '承認者名
            .PropStrSyoninID = dataHBKE0201.PropTxtsyoninID.Text                            '承認記録者ID
            .PropStrSyoninNM = dataHBKE0201.PropTxtsyoninNM.Text                            '承認記録者名

            .PropVwFileInfo = dataHBKE0201.PropVwFileInfo                                   '関連情報スプレッド(ファイル,ファイル説明)
            .PropVwCYSPR = dataHBKE0201.PropVwCYSPR                                         'CYSPRスプレッド
            .PropVwMeeting = dataHBKE0201.PropVwMeeting                                     '会議情報スプレッド

            .PropStrBIko1 = dataHBKE0201.PropTxtBIko1.Text                                  'テキスト１
            .PropStrBIko2 = dataHBKE0201.PropTxtBIko2.Text                                  'テキスト２
            .PropStrBIko3 = dataHBKE0201.PropTxtBIko3.Text                                  'テキスト３
            .PropStrBIko4 = dataHBKE0201.PropTxtBIko4.Text                                  'テキスト４
            .PropStrBIko5 = dataHBKE0201.PropTxtBIko5.Text                                  'テキスト５
            If dataHBKE0201.PropChkFreeFlg1.Checked = True Then                             'フリーフラグ1
                .PropStrFreeFlg1 = FLG_ON_NM
            Else
                .PropStrFreeFlg1 = FLG_OFF_NM
            End If
            If dataHBKE0201.PropChkFreeFlg2.Checked = True Then                             'フリーフラグ2
                .PropStrFreeFlg2 = FLG_ON_NM
            Else
                .PropStrFreeFlg2 = FLG_OFF_NM
            End If
            If dataHBKE0201.PropChkFreeFlg3.Checked = True Then                             'フリーフラグ3
                .PropStrFreeFlg3 = FLG_ON_NM
            Else
                .PropStrFreeFlg3 = FLG_OFF_NM
            End If
            If dataHBKE0201.PropChkFreeFlg4.Checked = True Then                             'フリーフラグ4
                .PropStrFreeFlg4 = FLG_ON_NM
            Else
                .PropStrFreeFlg4 = FLG_OFF_NM
            End If
            If dataHBKE0201.PropChkFreeFlg5.Checked = True Then                             'フリーフラグ5
                .PropStrFreeFlg5 = FLG_ON_NM
            Else
                .PropStrFreeFlg5 = FLG_OFF_NM
            End If

            .PropVwKankei = dataHBKE0201.PropVwKankei                                       '対応関係者情報データ(区分,ID,グループ名,ユーザー名)
            .PropStrGrpHistory = dataHBKE0201.PropTxtGrpHistory.Text                        'グループ履歴
            .PropStrTantoHistory = dataHBKE0201.PropTxtTantoHistory.Text                    '担当者履歴
            .PropVwProcessLinkInfo = dataHBKE0201.PropVwprocessLinkInfo                     'プロセスリンク管理番号(区分,番号)

        End With

        'メール作成処理呼び出し
        If logicHBKE0202.CreateIncidentMailMain(dataHBKE0202) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
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
    ''' [リリース登録]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>リリース登録画面を開く
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub BtnRelease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRelease.Click

        'リリース登録画面
        dataHBKE0201.PropfrmInstance = New HBKF0201

        'パラメータ設定
        With dataHBKE0201.PropfrmInstance.dataHBKF0201
            .PropStrProcMode = PROCMODE_NEW
            .PropIntChgNmb = dataHBKE0201.PropIntChgNmb
            .PropVwProcessLinkInfo_Save = dataHBKE0201.PropVwprocessLinkInfo
            '★閉じるボタン表示用のフラグを渡す
            .PropIntOwner = SCR_CALLMOTO_REG
        End With

        'クローズ処理の追加
        AddHandler CType(dataHBKE0201.PropfrmInstance, Form).FormClosed, AddressOf frm_FormClosed

        '画面制御開始
        Scr_Enabled_Start()

        '別画面として表示
        dataHBKE0201.PropfrmInstance.Show()


    End Sub

    ''' <summary>
    ''' [リリース登録]ボタン非活性処理
    ''' </summary>
    ''' <param name="Ctl">[IN]コントロール</param>
    ''' <param name="aryList">[IN/OUT]活性リスト</param>
    ''' <remarks>活性ボタンオブジェクトの活性リスト作成し非活性とする
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub SetButonEnabledFalse(ByVal Ctl As Control.ControlCollection, ByRef aryList As ArrayList)

        For Each c As Control In Ctl
            If c.HasChildren Then
                '再帰
                SetButonEnabledFalse(c.Controls, aryList)
            Else
                If TypeOf c Is Button Then
                    If CType(c, Button).Enabled = True Then
                        '非活性対象のボタンは除外
                        If AryNotfrmCtlList.IndexOf(c.Name) = -1 Then
                            '活性しているボタンを格納
                            aryList.Add(c.Name)
                            '非活性とする
                            c.Enabled = False
                        End If
                    End If
                End If
            End If
        Next

    End Sub

    ''' <summary>
    ''' [別画面のフォーム]クローズ後の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>閉じた後プロセスリンクを再取得する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub frm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs)

        'プロセスリンク再取得処理
        If logicHBKE0201.RefreshPLinkMain(dataHBKE0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        '画面制御終了
        Scr_Enabled_End()

    End Sub

    ''' <summary>
    ''' [別画面のフォーム]ボタン非活性解除処理
    ''' </summary>
    ''' <param name="Ctl">[IN]コントロール</param>
    ''' <param name="aryList">[IN/OUT]活性リスト</param>
    ''' <remarks>活性リストのボタンオブジェクトを活性にする
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub SetButonEnabledTrue(ByVal Ctl As Control.ControlCollection, ByRef aryList As ArrayList)

        For Each c As Control In Ctl
            If c.HasChildren Then
                '再帰
                SetButonEnabledTrue(c.Controls, aryList)
            Else
                If TypeOf c Is Button Then
                    '活性リストにあったものだけ活性とする
                    If CType(c, Button).Enabled = False AndAlso _
                       aryList.Contains(c.Name) Then
                        '活性とする
                        c.Enabled = True
                    End If
                End If
            End If
        Next

    End Sub


    ''' <summary>
    ''' [戻る]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>当画面を閉じて呼び出し元画面へ遷移する
    ''' <para>作成情報：2012/08/13 r.hoshino
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
    ''' <remarks>別画面が開いている場合、クローズ処理を行わない、関係者＋編集モードの場合はロック解除を行う
    ''' <para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKE0201_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        '別画面が開いている場合、クローズ処理を行わない
        If dataHBKE0201.PropfrmInstance IsNot Nothing Then
            'クローズ処理キャンセル
            e.Cancel = True
            'エラーメッセージ設定
            puErrMsg = E0201_E002
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If

        '編集モードの場合はロック解除を行う
        If dataHBKE0201.PropStrProcMode = PROCMODE_EDIT And _
            (dataHBKE0201.PropBlnBeLockedFlg = False And dataHBKE0201.PropIntChkKankei = KANKEI_CHECK_EDIT) Then

            '画面クローズ時ロック解除処理
            If logicHBKE0201.UnlockWhenCloseMain(dataHBKE0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKE0201.PropAryTsxCtlList) = False Then
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
    ''' 共通_画面制御処理_別画面表示前処理
    ''' </summary>
    ''' <remarks>ボタン非活性など画面制御を行う。
    ''' <para>作成情報：2012/09/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub Scr_Enabled_Start()
        '親画面のすべてのボタンを非活性とする
        dataHBKE0201.PropAryfrmCtlList = New ArrayList
        SetButonEnabledFalse(Me.Controls, dataHBKE0201.PropAryfrmCtlList)

        'イベント停止
        RemoveHandler vwMeeting.CellDoubleClick, AddressOf vwMeeting_CellDoubleClick
        RemoveHandler vwProcessLinkInfo.CellDoubleClick, AddressOf vwProcessLinkInfo_CellDoubleClick

    End Sub

    ''' <summary>
    ''' 共通_画面制御処理_別画面表示終了後処理
    ''' </summary>
    ''' <remarks>画面制御を元に戻す
    ''' <para>作成情報：2012/09/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub Scr_Enabled_End()

        '別画面破棄(FromClosingのチェックで使用）
        dataHBKE0201.PropfrmInstance = Nothing

        '親画面の非活性の状態を戻す
        SetButonEnabledTrue(Me.Controls, dataHBKE0201.PropAryfrmCtlList)
        dataHBKE0201.PropAryfrmCtlList = Nothing

        'イベント復活
        AddHandler vwMeeting.CellDoubleClick, AddressOf vwMeeting_CellDoubleClick
        AddHandler vwProcessLinkInfo.CellDoubleClick, AddressOf vwProcessLinkInfo_CellDoubleClick

    End Sub

    '[add] 2012/09/24 s.tsuruta 完了メッセージ表示修正 START
    ''' <summary>
    ''' インタバール経過後の処理の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>当画面を閉じて呼び出し元画面へ遷移する
    ''' <para>作成情報：2012/09/24 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub timKanryo_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timKanryo.Tick

        '登録完了ラベルを初期化する 
        dataHBKE0201.PropLblkanryoMsg.Text = ""

        'タイマーを停止する
        Me.timKanryo.Stop()

    End Sub
    '[add] 2012/09/24 s.tsuruta 完了メッセージ表示修正 END

End Class